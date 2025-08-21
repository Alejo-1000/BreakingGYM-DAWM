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
    /// Lógica de interacción para Rol.xaml
    /// </summary>
    public partial class Rol : MetroWindow
    {
        RolBL _mostrarRol = new RolBL();
        RolEN _rolEN = new RolEN();
        public Rol()
        {
            InitializeComponent();
            CargarGrid();
        }
        public void CargarGrid()
        {
            dgMostrarRol.ItemsSource = _mostrarRol.MostrarRol();
        }
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var _rol = new RolEN
                {
                    Nombre = txtNombre.Text.Trim()
                };

                // Validar campo vacío
                if (string.IsNullOrEmpty(_rol.Nombre))
                {
                    MessageBox.Show("Por favor, ingrese un nombre de rol.",
                                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Obtener lista de roles existentes
                var listaRoles = _mostrarRol.MostrarRol(); // Método que devuelve todos los roles

                // Validar duplicado por nombre (sin distinguir mayúsculas/minúsculas)
                bool yaExiste = listaRoles.Any(r => r.Nombre.Equals(_rol.Nombre, StringComparison.OrdinalIgnoreCase));

                if (yaExiste)
                {
                    MessageBox.Show("Ya existe un rol con ese nombre. No se puede duplicar.",
                                    "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Guardar rol
                _mostrarRol.GuardarRol(_rol);

                // Limpiar campo
                txtNombre.Text = string.Empty;
                CargarGrid();

                MessageBox.Show("Rol guardado correctamente.",
                                "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al guardar el rol: " + ex.Message,
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtId.Text))
                {
                    MessageBox.Show("Por favor, ingrese un Id.",
                                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var Rol = new RolEN
                {
                    Id = Convert.ToByte(txtId.Text),
                };

                if (Rol.Id <= 0)
                {
                    MessageBox.Show("Por favor, ingrese un Id válido.",
                                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var confirmResult = MessageBox.Show("¿Estás seguro que deseas eliminar este Rol?",
                                                    "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (confirmResult == MessageBoxResult.No)
                    return;

                _mostrarRol.EliminarRol(Rol);

                txtId.Text = string.Empty;
                txtNombre.Text = string.Empty;
                CargarGrid();

                MessageBox.Show("Rol eliminado correctamente.",
                                "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al eliminar el rol: " + ex.Message,
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
                btnEliminar.Visibility = Visibility.Collapsed;
                btnModificar.Visibility = Visibility.Collapsed;
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtId.Text) || string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("Por favor, ingrese todos los campos.",
                                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var rol = new RolEN
                {
                    Id = Convert.ToByte(txtId.Text),
                    Nombre = txtNombre.Text.Trim(),
                };

                if (rol.Id <= 0 || string.IsNullOrEmpty(rol.Nombre))
                {
                    MessageBox.Show("Por favor, ingrese todos los campos válidos.",
                                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var confirmResult = MessageBox.Show("¿Estás seguro que deseas modificar este Rol?",
                                                    "Confirmar modificación", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (confirmResult == MessageBoxResult.No)
                    return;

                _mostrarRol.ModificarRol(rol);

                txtId.Text = string.Empty;
                txtNombre.Text = string.Empty;
                CargarGrid();

                MessageBox.Show("Rol modificado correctamente.",
                                "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al modificar el rol: " + ex.Message,
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtId.Clear();
            txtNombre.Clear();

        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dgMostrarRol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgMostrarRol.SelectedItem != null)
            {
                // Obtener el elemento seleccionado y asignar sus valores a los campos de texto
                RolEN Seleccionado = (RolEN)dgMostrarRol.SelectedItem;
                txtId.Text = Seleccionado.Id.ToString();
                txtNombre.Text = Seleccionado.Nombre;
            }
        }
    }
}
