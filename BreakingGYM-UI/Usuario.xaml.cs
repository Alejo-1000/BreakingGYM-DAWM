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
    /// Lógica de interacción para Usuario.xaml
    /// </summary>
    public partial class Usuario : MetroWindow
    {
        UsuarioBL _usuarioBL = new UsuarioBL();
        private RolBL _rolBL = new RolBL(); // Para cargar los roles
        public Usuario()
        {
            InitializeComponent();
            CargarGrid(); // Cargar los datos al iniciar
        }
        public void CargarGrid()
        {

            dgMostrarUsuario.ItemsSource = _usuarioBL.MostrarUsuario();

        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            var _usuario = new UsuarioEN
            {
                IdRol = Convert.ToByte(cbxIdRol.SelectedValue),
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Celular = txtCelular.Text,
                Cuenta = txtCuenta.Text,
                Contrasenia = txtContrasenia.Text
            };

            // Validación de campos vacíos
            if (_usuario.IdRol <= 0 || string.IsNullOrWhiteSpace(_usuario.Nombre) || string.IsNullOrWhiteSpace(_usuario.Apellido)
                || string.IsNullOrWhiteSpace(_usuario.Celular) || string.IsNullOrEmpty(_usuario.Cuenta) || string.IsNullOrEmpty(_usuario.Contrasenia))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // ✅ Validación para evitar duplicados por cuenta
            var listaUsuarios = _usuarioBL.MostrarUsuario(); // Este método devuelve todos los usuarios existentes

            bool yaExiste = listaUsuarios.Any(u => u.Cuenta.Equals(_usuario.Cuenta, StringComparison.OrdinalIgnoreCase));

            if (yaExiste)
            {
                MessageBox.Show("Ya existe un usuario con esa cuenta. No se puede duplicar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Guardar si no existe
            _usuarioBL.GuardarUsuario(_usuario);

            // Limpiar campos
            txtId.Clear();
            cbxIdRol.SelectedIndex = -1;   // 👈 limpia el combo en WPF
            txtCuenta.Clear();
            txtContrasenia.Clear();        // 👈 PasswordBox no tiene Clear(), usa esto
            txtNombre.Clear();
            txtApellido.Clear();
            txtCelular.Clear();
            CargarGrid();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Por favor, seleccione un Id.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string cuentaSeleccionada = txtCuenta.Text.Trim();

            // ✅ Validar que no sea la cuenta del usuario logueado
            if (cuentaSeleccionada.Equals(UsuarioActual.Cuenta, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("No puedes eliminar el usuario con el que estás logueado.", "Acción no permitida", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Confirmación de eliminación
            var confirmResult = MessageBox.Show("¿Estás seguro que deseas eliminar este usuario?",
                                                "Confirmar eliminación",
                                                MessageBoxButton.YesNo,
                                                MessageBoxImage.Question);

            if (confirmResult == MessageBoxResult.No)
                return;

            // Crear objeto usuario y eliminar
            var usuarioEliminar = new UsuarioEN { Id = Convert.ToByte(txtId.Text) };
            _usuarioBL.EliminarUsuario(usuarioEliminar);

            // Limpiar campos
            txtId.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtCelular.Clear();
            txtCuenta.Clear();
            txtContrasenia.Clear();

            // Recargar grid
            CargarGrid();

            MessageBox.Show("Usuario eliminado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Por favor, seleccione un Id.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var Usuario = new UsuarioEN
            {
                Id = Convert.ToByte(txtId.Text),
                IdRol = Convert.ToByte(cbxIdRol.SelectedValue),
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Celular = txtCelular.Text,
                Cuenta = txtCuenta.Text,
                Contrasenia = txtContrasenia.Text,
            };

            // Validar campos
            if (Usuario.IdRol <= 0 || string.IsNullOrWhiteSpace(Usuario.Nombre) ||
                string.IsNullOrWhiteSpace(Usuario.Apellido) || string.IsNullOrWhiteSpace(Usuario.Celular) ||
                string.IsNullOrWhiteSpace(Usuario.Cuenta) || string.IsNullOrWhiteSpace(Usuario.Contrasenia))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Confirmar modificación
            var confirmResult = MessageBox.Show("¿Estás seguro que deseas modificar este Usuario?",
                                                "Confirmar modificación",
                                                MessageBoxButton.YesNo,
                                                MessageBoxImage.Question);

            if (confirmResult == MessageBoxResult.No)
                return;

            // Modificar usuario
            _usuarioBL.ModificarUsuario(Usuario);

            // Limpiar campos
            txtId.Clear();
            txtContrasenia.Clear();
            txtCuenta.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtCelular.Clear();

            // Recargar grid
            CargarGrid();

            MessageBox.Show("Usuario modificado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void dgMostrarUsuario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgMostrarUsuario.SelectedItem != null)
            {
                var usuarioSeleccionado = dgMostrarUsuario.SelectedItem as UsuarioEN;
                if (usuarioSeleccionado != null)
                {
                    txtId.Text = usuarioSeleccionado.Id.ToString();
                    cbxIdRol.SelectedValue = usuarioSeleccionado.IdRol;
                    txtNombre.Text = usuarioSeleccionado.Nombre;
                    txtApellido.Text = usuarioSeleccionado.Apellido;
                    txtCelular.Text = usuarioSeleccionado.Celular;
                    txtCuenta.Text = usuarioSeleccionado.Cuenta;
                    txtContrasenia.Text = usuarioSeleccionado.Contrasenia;
                }
            }
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            cbxIdRol.ItemsSource = _rolBL.  MostrarRol(); // Cargar estados desde el BL
            cbxIdRol.DisplayMemberPath = "Nombre";   // lo que se muestra al usuario
            cbxIdRol.SelectedValuePath = "Id";
        }

        private void txtId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtId.Text))
            {
                btnModificar.Visibility = Visibility.Visible;
                btnEliminar.Visibility = Visibility.Visible;
            }
            else
            {
                btnModificar.Visibility = Visibility.Collapsed;
                btnEliminar.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtId.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtCelular.Text = string.Empty;
            cbxIdRol.Text = string.Empty;
            txtCuenta.Text = string.Empty;
            txtContrasenia.Text = string.Empty;
            txtBuscarUsuario.Text = string.Empty;
        }
    }
}
