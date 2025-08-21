using BreakingGymEN;
using BreakinGymBL;
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
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : MetroWindow
    {
        private bool mostrando = false;
        public Login()
        {
            InitializeComponent();
        }

        private void btnIniciar_Click(object sender, RoutedEventArgs e)
        {
            string cuenta = txtCorreo.Text.Trim();
            string contrasenia = txtContrasenia.Password.Trim();

            // Validar campos vacíos
            if (string.IsNullOrEmpty(cuenta) || string.IsNullOrEmpty(contrasenia))
            {
                MessageBox.Show("Por favor, ingrese usuario y contraseña.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Intentar iniciar sesión
            UsuarioEN usuario = UsuarioBL.IniciarSesion(cuenta, contrasenia);
            if (usuario != null)
            {
                // ✅ Guardar cuenta del usuario logueado
                UsuarioActual.Cuenta = usuario.Cuenta;
                UsuarioActual.UsuarioLogueado = usuario;

                if (usuario.IdRol == 1) // Administrador
                {
                    InicioAdministrador ventanaAdmi = new InicioAdministrador();
                    ventanaAdmi.Show();
                    this.Close(); // Cerrar ventana de login
                }
                else if (usuario.IdRol == 2) // Empleado
                {
                    InicioEmpleado ventanaEmpleado = new InicioEmpleado();
                    ventanaEmpleado.Show();
                    this.Close(); // Cerrar ventana de login
                }
                else
                {
                    MessageBox.Show("Rol no reconocido.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            txtContrasenia.Clear(); // Limpiar contraseña después de intentar iniciar sesión
            txtCorreo.Clear();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            

        }

        private void btnMostrar_Click(object sender, RoutedEventArgs e)
        {
            if (mostrando)
            {
                // Ocultar contraseña
                txtContrasenia.Password = txtVisible.Text;
                txtContrasenia.Visibility = Visibility.Visible;
                txtVisible.Visibility = Visibility.Collapsed;

                // Cambiar a imagen de ojo abierto
                btnMostrar.Background = new ImageBrush(
                    new BitmapImage(new Uri("pack://application:,,,/ojo_abierto.png")));

                mostrando = false;
            }
            else
            {
                // Mostrar contraseña
                txtVisible.Text = txtContrasenia.Password;
                txtVisible.Visibility = Visibility.Visible;
                txtContrasenia.Visibility = Visibility.Collapsed;

                // Cambiar a imagen de ojo cerrado
                btnMostrar.Background = new ImageBrush(
                    new BitmapImage(new Uri("pack://application:,,,/ojo_cerrado.png")));

                mostrando = true;
            }
        }
    }
}
