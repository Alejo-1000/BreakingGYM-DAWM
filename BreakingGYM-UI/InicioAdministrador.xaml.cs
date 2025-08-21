using BreakingGymUI;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BreakingGymUI
{
    /// <summary>
    /// Lógica de interacción para InicioAdministrador.xaml
    /// </summary>
    public partial class InicioAdministrador : MetroWindow
    {
        public InicioAdministrador()
        {
            InitializeComponent();
        }

        private void BtnInscripcion_Click(object sender, RoutedEventArgs e)
        {
            Inscripcion Inscripcion = new Inscripcion();// instacia 
            Inscripcion.Show();
        }

        private void btnAsistencia_Click(object sender, RoutedEventArgs e)
        {
            RegistroAsistencia RegistroAsistencia = new RegistroAsistencia();
            RegistroAsistencia.Show();
        }

        private void btnMembresia_Click(object sender, RoutedEventArgs e)
        {
            Membresia Membresia = new Membresia();
            Membresia.Show();
        }

        private void btnCliente_Click(object sender, RoutedEventArgs e)
        {
            Cliente Cliente = new Cliente();
            Cliente.Show();
        }

        private void btnServicio_Click(object sender, RoutedEventArgs e)
        {
            Servicio Servicio = new Servicio();
            Servicio.Show();
        }

        private void btnUsuarios_Click(object sender, RoutedEventArgs e)
        {
            Usuario Usuario = new Usuario();
            Usuario.Show();
        }

        private void btnReportes_Click(object sender, RoutedEventArgs e)
        {
            ReporteMembresia Reporte = new ReporteMembresia();
            Reporte.Show();
        }

        private void btnRoles_Click(object sender, RoutedEventArgs e)
        {
            Rol Rol = new Rol();
            Rol.Show();
        }

        private void btnEstado_Click(object sender, RoutedEventArgs e)
        {
            Estado estado = new Estado();
            estado.Show();
        }

        private void btnDocumentos_Click(object sender, RoutedEventArgs e)
        {
            TipoDocumento documento = new TipoDocumento();
            documento.Show();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            this.Close(); // Cierra la ventana actual
            login.Show();

        }
    }
}


