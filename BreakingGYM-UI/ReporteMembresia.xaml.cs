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
using BreakingGymEN;
using MahApps.Metro.Controls;
using BreakinGymBL;

namespace BreakingGymUI
{
    /// <summary>
    /// Lógica de interacción para ReporteMembresia.xaml
    /// </summary>
    public partial class ReporteMembresia : MetroWindow
    {
        private InscripcionBL _mostrarInscripcion = new InscripcionBL();
        public ReporteMembresia()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

      

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            dtMostrar.ItemsSource = _mostrarInscripcion.MostrarInscripcion(); // Cargar clientes desde el BL
        }
    }
}
