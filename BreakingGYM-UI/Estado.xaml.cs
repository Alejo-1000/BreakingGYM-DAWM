using BreakingGymEN;
using BreakinGymBL;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Lógica de interacción para Estado.xaml
    /// </summary>
    public partial class Estado : MetroWindow
    {
        EstadoBL _mostrarEstado = new EstadoBL();
        EstadoEN _estadoEN = new EstadoEN();

        public Estado()
        {
            InitializeComponent();
            CargarGrid();
        }
        public void CargarGrid()
        {
            dgMostrarEstado.ItemsSource = _mostrarEstado.MostrarEstado();
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            var estado = new EstadoEN
            {
                Nombre = txtEstado.Text.Trim(),
            };

            // Validar campo vacío
            if (string.IsNullOrEmpty(estado.Nombre))
            {
                MessageBox.Show("Por favor, complete todos los campos.",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Obtener lista de estados existentes
            var listaEstados = _mostrarEstado.MostrarEstado(); // Debe devolver la lista completa de estados

            // Validar duplicado por nombre (ignorando mayúsculas/minúsculas)
            bool yaExiste = listaEstados.Any(n =>
                n.Nombre.Equals(estado.Nombre, StringComparison.OrdinalIgnoreCase));

            if (yaExiste)
            {
                MessageBox.Show("Ya existe un estado con ese nombre. No se puede duplicar.",
                    "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Guardar estado
            _mostrarEstado.GuardarEstado(estado);

            // Limpiar campo
            txtEstado.Text = string.Empty; // En WPF no existe Clear()
            CargarGrid();

            MessageBox.Show("Estado guardado correctamente.",
                "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            var est = new EstadoEN
            {
                Id = Convert.ToByte(TxtId.Text)
            };

            // Validar Id
            if (est.Id <= 0)
            {
                MessageBox.Show("Por favor, seleccione un Id.",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Confirmar eliminación
            var confirmResult = MessageBox.Show("¿Estás seguro que deseas eliminar este Estado?",
                                                "Confirmar eliminación",
                                                MessageBoxButton.YesNo,
                                                MessageBoxImage.Question);

            // En WPF se usa MessageBoxResult en lugar de DialogResult
            if (confirmResult == MessageBoxResult.No)
                return;

            _mostrarEstado.EliminarEstado(est);

            // Limpiar campos
            TxtId.Text = string.Empty;
            txtEstado.Text = string.Empty;

            // Recargar el grid
            CargarGrid();

            MessageBox.Show("Estado eliminado correctamente.",
                "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtId.Text))
            {
                MessageBox.Show("Por favor, selecciona un Id",
                    "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                TxtId.Focus();
                return;
            }

            var estado = new EstadoEN
            {
                Id = Convert.ToByte(TxtId.Text),
                Nombre = txtEstado.Text
            };

            if (estado.Id <= 0 || string.IsNullOrWhiteSpace(estado.Nombre))
            {
                MessageBox.Show("Por favor, complete todos los campos.",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Confirmación en WPF usa MessageBoxResult
            var confirmResult = MessageBox.Show("¿Estás seguro que deseas modificar este Estado?",
                                                "Confirmar modificación",
                                                MessageBoxButton.YesNo,
                                                MessageBoxImage.Question);

            if (confirmResult == MessageBoxResult.No)
                return;

            _mostrarEstado.ModificarEstado(estado);

            // Limpiar campos
            TxtId.Text = string.Empty;
            txtEstado.Text = string.Empty;

            // Refrescar grid
            CargarGrid();

            MessageBox.Show("Estado modificado correctamente.",
                "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }


        private void dgMostrarEstado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgMostrarEstado.SelectedItem != null)
            {
                // ✅ Casting correcto para objetos EstadoEN
                EstadoEN estadoSeleccionado = (EstadoEN)dgMostrarEstado.SelectedItem;
                TxtId.Text = estadoSeleccionado.Id.ToString();
                txtEstado.Text = estadoSeleccionado.Nombre;
            }
        }
    }
}
