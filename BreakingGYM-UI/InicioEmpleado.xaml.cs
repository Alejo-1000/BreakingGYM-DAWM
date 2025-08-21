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
using MahApps.Metro.Controls;
namespace BreakingGymUI
{
    /// <summary>
    /// Lógica de interacción para InicioEmpleado.xaml
    /// </summary>
    public partial class InicioEmpleado : MetroWindow
    {
        public InicioEmpleado()
        {
            InitializeComponent();
        }

        private void btnInscripcion_Click(object sender, RoutedEventArgs e)
        {
            Inscripcion inscripcion = new Inscripcion();
            inscripcion.Show();
        }

        private void btnAsistencia_Click(object sender, RoutedEventArgs e)
        {
            RegistroAsistencia asistencia = new RegistroAsistencia();
            asistencia.Show();
        }

        private void btnCliente_Click(object sender, RoutedEventArgs e)
        {
            Cliente cliente = new Cliente();
            cliente.Show();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            this.Close(); // Cierra la ventana actual
            login.Show();
        }
    }
}
