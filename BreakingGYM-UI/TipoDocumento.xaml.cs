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
    /// Lógica de interacción para TipoDocumento.xaml
    /// </summary>
    public partial class TipoDocumento : MetroWindow
    {
        TipoDocumentoBL _mostrarTipoDocumento = new TipoDocumentoBL();
        TipoDocumentoEN _tipoDocumentoEN = new TipoDocumentoEN();
        public TipoDocumento()
        {
            InitializeComponent();
            CargarGrid();
        }
        public void CargarGrid()
        {
            dgMostrarTipoDocumento.ItemsSource = _mostrarTipoDocumento.MostrarTipoDocumento();
        }
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            var tipoDocumento = new TipoDocumentoEN
            {
                Nombre = TxtNombre.Text.Trim(),
            };

            // Validar campo vacío
            if (string.IsNullOrEmpty(tipoDocumento.Nombre))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Obtener lista de documentos existentes
            var listaTipoDocumentos = _mostrarTipoDocumento.MostrarTipoDocumento(); // Debe devolver la lista completa

            // Validar duplicado por nombre (ignorando mayúsculas/minúsculas)
            bool yaExiste = listaTipoDocumentos.Any(n =>
                n.Nombre.Equals(tipoDocumento.Nombre, StringComparison.OrdinalIgnoreCase));

            if (yaExiste)
            {
                MessageBox.Show("Ya existe un Documento con ese nombre. No se puede duplicar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Guardar documento
            _mostrarTipoDocumento.GuardarTipoDocumento(tipoDocumento);

            // Limpiar campo
            TxtNombre.Clear();
            CargarGrid();

            MessageBox.Show("Tipo Documento guardado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Por favor, selecciona un ID.", "Validación",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                txtId.Focus();
                return;
            }

            var docu = new TipoDocumentoEN
            {
                Id = Convert.ToByte(txtId.Text),
            };

            if (docu.Id <= 0)
            {
                MessageBox.Show("Por favor, Seleccione un Id.", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var confirmResult = MessageBox.Show("¿Estás seguro que deseas eliminar este Estado?",
                                                "Confirmar eliminación",
                                                MessageBoxButton.YesNo,
                                                MessageBoxImage.Question);

            if (confirmResult == MessageBoxResult.No)
                return;
            else
            {
                _mostrarTipoDocumento.EliminarTipoDocumento(docu);
                txtId.Clear();
                TxtNombre.Clear();
                CargarGrid();

                MessageBox.Show("Estado eliminado correctamente.", "Éxito",
                                MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Por favor, selecciona un Id", "Validación",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                txtId.Focus();
                return;
            }

            var docu = new TipoDocumentoEN
            {
                Id = Convert.ToByte(txtId.Text),
                Nombre = TxtNombre.Text.Trim(),
            };

            if (docu.Id <= 0 || string.IsNullOrEmpty(docu.Nombre))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var confirmResult = MessageBox.Show("¿Estás seguro que deseas modificar este Estado?",
                                               "Confirmar modificación",
                                               MessageBoxButton.YesNo,
                                               MessageBoxImage.Question);

            if (confirmResult == MessageBoxResult.No)
                return;
            else
            {
                _mostrarTipoDocumento.ModificarTipoDocumento(docu);

                txtId.Clear();
                TxtNombre.Clear();
                CargarGrid();

                MessageBox.Show("Estado modificado correctamente.", "Éxito",
                                MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void txtId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtId.Text))
            {
                btnModificar.IsEnabled = true;
                btnEliminar.IsEnabled = true;
                btnLimpiar.IsEnabled = true;
            }
            else
            {
                btnModificar.IsEnabled = false;
                btnEliminar.IsEnabled = false;
                btnLimpiar.IsEnabled = false;
            }
        }
    }
}
