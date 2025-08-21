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

            _puerto = new SerialPort("COM3", 9600);
            _puerto.DataReceived += Puerto_DataReceived;

            try
            {
                if (!_puerto.IsOpen)
                    _puerto.Open();
            }
            catch (Exception ex)
            {
                // No bloquea el inicio, solo informa
                MessageBox.Show("No se pudo abrir el puerto COM. El Arduino no está conectado.\n\n" +
                                "Detalles: " + ex.Message,
                                "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
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
            if (CbTarjetas.SelectedItem != null)
            {
                string numeroRFID = CbTarjetas.SelectedItem.ToString();

                try
                {
                    using (SqlConnection conn = (SqlConnection)ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                    {
                        if (conn.State != ConnectionState.Open)
                            conn.Open();

                        using (SqlCommand cmd = new SqlCommand("MostrarAsistenciaPorRFID", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@NumeroRFID", numeroRFID);

                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                List<RegistroAsistenciaEN> lista = new List<RegistroAsistenciaEN>();

                                while (dr.Read())
                                {
                                    lista.Add(new RegistroAsistenciaEN
                                    {
                                        Id = Convert.ToInt32(dr["Id"]),
                                        IdCliente = Convert.ToInt32(dr["IdCliente"]),
                                        NombreCliente = dr["NombreCliente"].ToString(),
                                        ApellidoCliente = dr["ApellidoCliente"].ToString(),
                                        DocumentoCliente = dr["DocumentoCliente"].ToString(),
                                        NumeroRFID = dr["NumeroRFID"].ToString(),
                                        FechaAsistencia = Convert.ToDateTime(dr["FechaAsistencia"]),
                                        HoraAsistencia = TimeSpan.Parse(dr["HoraAsistencia"].ToString())
                                    });
                                }

                                DgAsistencia.ItemsSource = lista;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar asistencia: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Seleccione una tarjeta RFID.");
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

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            DtBuscar.Text = string.Empty;
        }

        private void btnCerrrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
