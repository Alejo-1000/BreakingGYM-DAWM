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
using Microsoft.Data.SqlClient;

namespace BreakingGymUI
{
    /// <summary>
    /// Lógica de interacción para Cliente.xaml
    /// </summary>
    public partial class Cliente : MetroWindow

    {
        ClienteBL _clienteBL = new ClienteBL();
        ClienteEN clienteEN = new ClienteEN();
        private TipoDocumentoBL _Documento = new TipoDocumentoBL();
        private RolBL _rolBL = new RolBL();

        public Cliente()
        {
            InitializeComponent();
            CargarGrid();
        }
        private void CargarGrid()
        {
           dgCliente.ItemsSource = _clienteBL.MostrarCliente();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            var cliente = new ClienteEN
            {
                IdRol = Convert.ToByte(CbxRol.SelectedValue),
                IdTipoDocumento = Convert.ToByte(Cbxdocumento.SelectedValue),
                Documento = TxtDocumento.Text.Trim(),
                Nombre = TxtNombre.Text.Trim(),
                Apellido = TxtApellido.Text.Trim(),
                Celular = TxtCelular.Text.Trim(),
                TarjetaRFID = TxtTarjetaRFID.Text.Trim() // Asignar un valor por defecto o dejarlo vacío si no es necesario
            };

            // Validar campos obligatorios
            if (cliente.IdRol <= 0 || cliente.IdTipoDocumento <= 0 ||
                string.IsNullOrEmpty(cliente.Documento) ||
                string.IsNullOrEmpty(cliente.Nombre) ||
                string.IsNullOrEmpty(cliente.Apellido) ||
               string .IsNullOrEmpty(cliente.TarjetaRFID) ||
                string.IsNullOrEmpty(cliente.Celular))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Obtener lista de clientes existentes
            var listaClientes = _clienteBL.MostrarCliente(); // Este método debe devolver la lista completa de clientes

            // Validar duplicado (solo por Documento, ignorando mayúsculas/minúsculas)
            bool yaExiste = listaClientes.Any(c =>
                c.Documento.Equals(cliente.Documento, StringComparison.OrdinalIgnoreCase)
            );

            if (yaExiste)
            {
                MessageBox.Show("Ya existe un cliente con ese documento. No se puede duplicar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Guardar cliente
            _clienteBL.GuardarCliente(cliente);

            // Limpiar campos
            TxtNombre.Clear();
            TxtApellido.Clear();
            TxtCelular.Clear();
            TxtDocumento.Clear();

            CargarGrid();

            MessageBox.Show("Cliente guardado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtId.Text))
            {
                btnEliminar.IsEnabled = false;
                MessageBox.Show("Por favor, Seleccione un Id.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var cli = new ClienteEN
            {
                Id = Convert.ToByte(TxtId.Text),
            };
            if (cli.Id <= 0)
            {
                MessageBox.Show("Por favor, Seleccione un Id.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var confirmResult = MessageBox.Show("¿Estás seguro que deseas eliminar este Cliente?",
                                               "Confirmar modificación",
                                               MessageBoxButton.YesNo,
                                               MessageBoxImage.Question);

            if (confirmResult == MessageBoxResult.No)
                return;
            else
            {
                _clienteBL.EliminarCliente(cli);
                TxtNombre.Clear();
                TxtCelular.Clear();
                TxtApellido.Clear();
                TxtId.Clear();
                TxtDocumento.Clear();
                CargarGrid();
                MessageBox.Show("Cliente Eliminado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtId.Text))
            {
                MessageBox.Show("Por favor, Seleccione un Id.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var cliente = new ClienteEN
            {
                Id = Convert.ToByte(TxtId.Text),
                IdRol = Convert.ToByte(CbxRol.SelectedValue),
                IdTipoDocumento = Convert.ToByte(Cbxdocumento.SelectedValue),
                Documento = TxtDocumento.Text.Trim(),
                Nombre = TxtNombre.Text,
                Apellido = TxtApellido.Text,
                Celular = TxtCelular.Text,
                TarjetaRFID = TxtTarjetaRFID.Text.Trim() // Asignar un valor por defecto o dejarlo vacío si no es necesario

            };
            if (string.IsNullOrEmpty(cliente.Nombre) || string.IsNullOrEmpty(cliente.Apellido) || string.IsNullOrEmpty(cliente.Celular) || cliente.IdRol <= 0 || cliente.IdTipoDocumento <= 0 ||string.IsNullOrEmpty(cliente.TarjetaRFID) || string.IsNullOrEmpty(cliente.Documento))
            {
                MessageBox.Show("Por favor, Complete todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var confirmResult = MessageBox.Show("¿Estás seguro que deseas modificar este Cliente?",
                                               "Confirmar modificación",
                                               MessageBoxButton.YesNo,
                                               MessageBoxImage.Question);

            if (confirmResult == MessageBoxResult.No)
                return;
            else
            {
                _clienteBL.ModificarCliente(cliente);
                TxtNombre.Clear();
                TxtCelular.Clear();
                TxtApellido.Clear();
                TxtId.Clear();
                TxtDocumento.Clear();
                TxtTarjetaRFID.Clear();
                CargarGrid();
                MessageBox.Show(" Cliente modificado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        private void dgCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (dgCliente.SelectedItem != null)
            {
                DataRowView row = (DataRowView)dgCliente.SelectedItem;

                TxtId.Text = row["Id"].ToString();
                CbxRol.Text = row["IdRol"].ToString();
                Cbxdocumento.Text = row["IdTipoDocumento"].ToString();
                TxtNombre.Text = row["Nombre"].ToString();
                TxtApellido.Text = row["Apellido"].ToString();
                TxtCelular.Text = row["Celular"].ToString();
                TxtDocumento.Text = row["Documento"].ToString();
                TxtTarjetaRFID.Text = row["TarjetaRFID"].ToString();

            }
            
        }

        private void txtId_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(TxtId.Text))
            {
                btnEliminar.Visibility = Visibility.Visible;
            }
            else
            {
                btnEliminar.Visibility = Visibility.Collapsed;
            }
            if (!string.IsNullOrWhiteSpace(TxtId.Text))
            {
                btnModificar.Visibility = Visibility.Visible;
            }
            else
            {
                btnModificar.Visibility = Visibility.Collapsed;
            }

        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            //Documentos
            Cbxdocumento.ItemsSource = _Documento.MostrarTipoDocumento(); // Cargar estados desde el BL
            Cbxdocumento.DisplayMemberPath = "Nombre";   // lo que se muestra al usuario
            Cbxdocumento.SelectedValuePath = "Id";
            // Rol
            CbxRol.ItemsSource = _rolBL.MostrarRol(); // Cargar roles desde el BL
            CbxRol.DisplayMemberPath = "Nombre";   // lo que se muestra al usuario
            CbxRol.SelectedValuePath = "Id";
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            TxtId.Text = string.Empty;
            TxtNombre.Text = string.Empty;
            TxtApellido.Text = string.Empty;
            CbxRol.Text = string.Empty;
            TxtCelular.Text = string.Empty;
            Cbxdocumento.Text = string.Empty;
            TxtDocumento.Text = string.Empty;

        }
    }
    
}
