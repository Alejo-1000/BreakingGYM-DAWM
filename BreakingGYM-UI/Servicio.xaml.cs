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
    /// Lógica de interacción para Servicio.xaml
    /// </summary>
    public partial class Servicio : MetroWindow
    {
        ServicioBL _mostrarServicio = new ServicioBL();
        ServicioEN _servicioEN = new ServicioEN();
        public Servicio()
        {
            InitializeComponent();
            CargarGrid();
        }
        public void CargarGrid()
        {
            dgMostrarServicio.ItemsSource = _mostrarServicio.MostrarServicio();
        }
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            var servicio = new ServicioEN
            {
                Nombre = txtNombre.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim(),
            };

            // Validar campos vacíos
            if (string.IsNullOrEmpty(servicio.Nombre) || string.IsNullOrEmpty(servicio.Descripcion))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Obtener lista de servicios existentes
            var listaServicios = _mostrarServicio.MostrarServicio(); // Método que devuelve todos los servicios

            // Validar duplicado por nombre (ignorando mayúsculas/minúsculas)
            bool yaExiste = listaServicios.Any(s =>
                s.Nombre.Equals(servicio.Nombre, StringComparison.OrdinalIgnoreCase));

            if (yaExiste)
            {
                MessageBox.Show("Ya existe un servicio con ese nombre. No se puede duplicar.", "Advertencia",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Guardar servicio
            _mostrarServicio.GuardarServicio(servicio);

            // Limpiar campos
            txtNombre.Clear();
            txtDescripcion.Clear();
            CargarGrid();

            MessageBox.Show("Servicio guardado correctamente.", "Éxito",
                            MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Por favor, ingrese un Id.", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!byte.TryParse(txtId.Text, out byte id) || id <= 0)
            {
                MessageBox.Show("Por favor, ingrese un Id válido.", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var servicio = new ServicioEN
            {
                Id = id
            };

            var confirmResult = MessageBox.Show("¿Estás seguro que deseas Eliminar este Servicio?",
                                               "Confirmar eliminación",
                                               MessageBoxButton.YesNo,
                                               MessageBoxImage.Question);

            if (confirmResult == MessageBoxResult.No)
                return;
            else
            {
                _mostrarServicio.EliminarServicio(servicio);
                txtId.Clear();
                CargarGrid();

                MessageBox.Show("Servicio Eliminado correctamente.", "Éxito",
                                MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!byte.TryParse(txtId.Text, out byte id) || id <= 0)
            {
                MessageBox.Show("Por favor, ingrese un Id válido.", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var servi = new ServicioEN
            {
                Id = id,
                Nombre = txtNombre.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim(),
            };

            if (string.IsNullOrEmpty(servi.Nombre) || string.IsNullOrEmpty(servi.Descripcion))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var confirmResult = MessageBox.Show("¿Estás seguro que deseas modificar este Servicio?",
                                               "Confirmar modificación",
                                               MessageBoxButton.YesNo,
                                               MessageBoxImage.Question);

            if (confirmResult == MessageBoxResult.No)
                return;

            _mostrarServicio.ModificarServicio(servi);

            txtNombre.Clear();
            txtDescripcion.Clear();
            txtId.Clear();
            CargarGrid();

            MessageBox.Show("Servicio modificado correctamente.", "Éxito",
                            MessageBoxButton.OK, MessageBoxImage.Information);

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

        private void dgMostrarServicio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
              if (dgMostrarServicio.SelectedItem != null)
              {
                    // Obtener el elemento seleccionado y asignar sus valores a los campos de texto
                    ServicioEN Seleccionado = (ServicioEN)dgMostrarServicio.SelectedItem;
                    txtId.Text = Seleccionado.Id.ToString();
                    txtNombre.Text = Seleccionado.Nombre;
                    txtDescripcion.Text = Seleccionado.Descripcion;
              }
        }

        private void BtnLimpiar_Click_1(object sender, RoutedEventArgs e)
        {
            txtId.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
           
        }
    }
}
