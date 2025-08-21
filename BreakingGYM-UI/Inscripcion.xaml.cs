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
    /// Lógica de interacción para Inscripcion.xaml
    /// </summary>
    public partial class Inscripcion : MetroWindow
    {
        InscripcionBL _mostrarInscripcion = new InscripcionBL();
        InscripcionEN _inscripcionEN = new InscripcionEN();
        public Inscripcion()
        {
            InitializeComponent();
            CargarGrid();
        }
        public void CargarGrid()
        {
            dgInscripcion.ItemsSource = _mostrarInscripcion.MostrarInscripcion();
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (cbCliente.SelectedValue == null || !byte.TryParse(cbCliente.SelectedValue.ToString(), out byte idCliente) || idCliente <= 0 ||
     cbMembresia.SelectedValue == null || !byte.TryParse(cbMembresia.SelectedValue.ToString(), out byte idMembresia) || idMembresia <= 0 ||
     cbEstado.SelectedValue == null || !byte.TryParse(cbEstado.SelectedValue.ToString(), out byte idEstado) || idEstado <= 0)
            {
                MessageBox.Show("Por favor, seleccione cliente, membresía y estado válidos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validar fechas
            if (!DateTime.TryParse(dpInicio.Text, out DateTime fechaInscripcion) ||
                !DateTime.TryParse(dpVencimiento.Text, out DateTime fechaVencimiento))
            {
                MessageBox.Show("Por favor, ingrese fechas válidas.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (fechaVencimiento <= fechaInscripcion)
            {
                MessageBox.Show("La fecha de vencimiento debe ser posterior a la fecha de inscripción.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Crear objeto inscripcion
            var inscripcion = new InscripcionEN
            {
                IdCliente = idCliente,
                IdMembresia = idMembresia,
                IdEstado = idEstado,
                FechaInscripcion = fechaInscripcion,
                FechaVencimiento = fechaVencimiento,
            };

            // Validar duplicados (por ejemplo, evitar que el mismo cliente tenga la misma membresía activa)
            // Para esto necesitas obtener la lista de inscripciones y chequear
            var listaInscripciones = _mostrarInscripcion.MostrarInscripcion(); // Debe devolver todas las inscripciones

            bool yaExiste = listaInscripciones.Any(i =>
                i.IdCliente == idCliente &&
                i.IdMembresia == idMembresia &&
                i.IdEstado == idEstado // o quizá solo estado activo, depende de tu lógica
            );

            if (yaExiste)
            {
                MessageBox.Show("Este cliente ya está inscrito en esta membresía con el mismo estado.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Guardar inscripción
            _mostrarInscripcion.GuardarInscripcion(inscripcion);

            MessageBox.Show("Inscripción realizada correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            CargarGrid();
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (!byte.TryParse(txtId.Text.Trim(), out byte id) || id <= 0)
            {
                MessageBox.Show("Por favor, seleccione un Id válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var ins = new InscripcionEN
            {
                Id = id
            };

            // Confirmar antes de eliminar
            var confirmResult = MessageBox.Show("¿Estás seguro que deseas eliminar esta membresía?",
                                                "Confirmar eliminación",
                                                MessageBoxButton.YesNo,
                                                MessageBoxImage.Warning);

            if (confirmResult == MessageBoxResult.Yes)
            {
                _mostrarInscripcion.EliminarInscripcion(ins);  // ✅ Ahora usas el objeto correcto
                CargarGrid();
                txtId.Clear();
                MessageBox.Show("Inscripcion eliminada correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {

                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var inscripcion = new InscripcionEN
            {
                Id = Convert.ToByte(txtId.Text),
                IdCliente = Convert.ToByte(cbCliente.SelectedValue),
                IdMembresia = Convert.ToByte(cbMembresia.SelectedValue),
                IdEstado = Convert.ToByte(cbEstado.SelectedValue),
                FechaInscripcion = Convert.ToDateTime(dpInicio.Text),
                FechaVencimiento = Convert.ToDateTime(dpVencimiento.Text),
            };
            if (inscripcion.Id <= 0 || inscripcion.IdCliente <= 0 || inscripcion.IdMembresia <= 0 || inscripcion.IdEstado <= 0)
            {

                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var confirmResult = MessageBox.Show("¿Estás seguro que deseas modificar esta Inscripcion?",
                                               "Confirmar modificación",
                                               MessageBoxButton.YesNo,
                                               MessageBoxImage.Question);

            if (confirmResult == MessageBoxResult.No)
                return;
            else
            {

                _mostrarInscripcion.ModificarInscripcion(_inscripcionEN);
                txtId.Clear();
                MessageBox.Show("Inscripcion Modificada correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                CargarGrid();
            }
        }

        private void dgInscripcion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgInscripcion.SelectedItem != null)
            {
                var inscripcion = dgInscripcion.SelectedItem as InscripcionEN;

                if (inscripcion != null)
                {
                    txtId.Text = inscripcion.Id.ToString();

                    // si los ComboBox están enlazados a listas, mejor usar SelectedValue
                    cbCliente.SelectedValue = inscripcion.IdCliente;
                    cbMembresia.SelectedValue = inscripcion.IdMembresia;
                    cbEstado.SelectedValue = inscripcion.IdEstado;

                    // con DatePicker es mejor usar SelectedDate
                    dpInicio.SelectedDate = inscripcion.FechaInscripcion;
                    dpVencimiento.SelectedDate = inscripcion.FechaVencimiento;
                }
            }
        }
    }
}
