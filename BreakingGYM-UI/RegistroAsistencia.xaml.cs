using BreakingGymDAL;
using BreakingGymEN;
using BreakinGymBL;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Ports;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;

namespace BreakingGymUI
{
    public partial class RegistroAsistencia : MetroWindow
    {
        private RegistroAsistenciaBL _asistenciaBL = new RegistroAsistenciaBL();
        private SerialPort _puerto;

        public RegistroAsistencia()
        {
            InitializeComponent();
            CargarAsistencia();

            // ⚠️ Ajusta el COM según tu Arduino
            _puerto = new SerialPort("COM3", 9600);
            _puerto.DataReceived += Puerto_DataReceived;
            _puerto.Open();
        }

        public void CargarAsistencia()
        {
            List<RegistroAsistenciaEN> lista = _asistenciaBL.MostrarAsistencia();
            DgAsistencia.ItemsSource = lista;
        }

        private void BtnRefrescar_Click(object sender, RoutedEventArgs e)
        {
            CargarAsistencia();
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (DtBuscar.SelectedDate.HasValue)
            {
                DateTime fechaAsistencia = DtBuscar.SelectedDate.Value;
                List<RegistroAsistenciaEN> lista = _asistenciaBL.BuscarAsistencia(fechaAsistencia);
                DgAsistencia.ItemsSource = lista;
            }
            else
            {
                MessageBox.Show("Seleccione una fecha para buscar.");
            }
        }

        private void Puerto_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string uid = _puerto.ReadLine().Trim();
                Dispatcher.Invoke(() => RegistrarAsistencia(uid));
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() =>
                {
                    MessageBox.Show("Error leyendo COM: " + ex.Message);
                });
            }
        }

        private void RegistrarAsistencia(string uid)
        {
            try
            {
                using (SqlConnection conn = (SqlConnection)ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (SqlCommand cmd = new SqlCommand("RegistrarAsistenciaPorTarjeta", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TarjetaRFID", uid);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string resultado = reader["Resultado"].ToString();

                                if (resultado == "OK")
                                {
                                    int idCliente = Convert.ToInt32(reader["ClienteId"]);
                                    MessageBox.Show($"✅ Asistencia registrada para Cliente {idCliente}");
                                    CargarAsistencia();
                                }
                                else if (resultado == "YA_REGISTRADO")
                                {
                                    int idCliente = Convert.ToInt32(reader["ClienteId"]);
                                    MessageBox.Show($"⚠️ El cliente {idCliente} ya registró asistencia hoy.");
                                }
                                else if (resultado == "NO_EXISTE")
                                {
                                    MessageBox.Show("❌ Tarjeta no registrada en el sistema");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() =>
                {
                    MessageBox.Show("❌ Error al registrar asistencia: " + ex.Message);
                });
            }
        }

        private void DgAsistencia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Evento de selección de filas en el DataGrid (si necesitas usarlo después)
        }
    }
}
