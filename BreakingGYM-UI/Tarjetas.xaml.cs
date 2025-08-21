using BreakingGymBL;
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
    /// Lógica de interacción para Tarjetas.xaml
    /// </summary>
    public partial class Tarjetas : MetroWindow
    {

        TarjetaBL tarjetaBL = new TarjetaBL();
        public Tarjetas()
        {
            InitializeComponent();
            CargarGrid();
        }

        private void CargarGrid()
        {
            dgMostrarTarjeta.ItemsSource = tarjetaBL.MostrarTarjetaRFID();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNumero.Text.Trim()))
            {
                MessageBox.Show("Ingrese el número de la tarjeta.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var tarjeta = new TarjetaEN { Numerorfid = txtNumero.Text.Trim() };

            // Validar duplicado
            var lista = tarjetaBL.MostrarTarjetaRFID();
            if (lista.Any(t => t.Numerorfid.Equals(tarjeta.Numerorfid, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("La tarjeta ya existe.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            tarjetaBL.GuardarTarjetaRFID(tarjeta);
            MessageBox.Show("Tarjeta guardada correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            txtNumero.Text = "";
            CargarGrid();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Ingrese un Id válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var tarjeta = new TarjetaEN { Id = id };

            var confirm = MessageBox.Show("¿Desea eliminar la tarjeta?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirm == MessageBoxResult.Yes)
            {
                tarjetaBL.EliminarTarjetaRFID(tarjeta);
                MessageBox.Show("Tarjeta eliminada.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                txtId.Text = txtNumero.Text = "";
                CargarGrid();
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Ingrese un Id válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(txtNumero.Text.Trim()))
            {
                MessageBox.Show("Ingrese el número de la tarjeta.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var tarjeta = new TarjetaEN { Id = id, Numerorfid = txtNumero.Text.Trim() };
            tarjetaBL.ModificarTarjetaRFID(tarjeta);
            MessageBox.Show("Tarjeta modificada.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            txtId.Text = txtNumero.Text = "";
            CargarGrid();
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtId.Text = txtNumero.Text = "";
        }

        private void dgMostrarTarjeta_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgMostrarTarjeta.SelectedItem is TarjetaEN selected)
            {
                txtId.Text = selected.Id.ToString();
                txtNumero.Text = selected.Numerorfid;
            }
        }
    }
}
