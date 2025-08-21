using BreakingGymEN;
using BreakinGymBL;
using MahApps.Metro.Controls;
using QRCoder;
using System;
using System.Collections.Generic;
using System.IO;
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
using DrawingBitmap = System.Drawing.Bitmap;

namespace BreakingGymUI
{
    /// <summary>
    /// Lógica de interacción para Membresia.xaml
    /// </summary>
    public partial class Membresia : MetroWindow
    {
        MembresiaEN membresiaEN = new MembresiaEN();
        MembresiaBL _mostrarMembresia = new MembresiaBL();
        public MembresiaEN membresiaParaImprimir;
        MembresiaBL _membresiaBL = new MembresiaBL();
        private ServicioBL _servicioBL = new ServicioBL();
        public Membresia()
        {
            InitializeComponent();
            CargarGrid();
        }
        public void CargarGrid()
        {
            DataMembresia.ItemsSource = _mostrarMembresia.MostrarMembresia();
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (CbxIdServicio.SelectedValue == null || !byte.TryParse(CbxIdServicio.SelectedValue.ToString(), out byte idServicio))
            {
                MessageBox.Show("Seleccione un servicio válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validar y convertir Precio
            string precioTexto = txtPrecio.Text.Trim();

            if (!int.TryParse(precioTexto, out int precio) || precio <= 0)
            {
                MessageBox.Show("Ingrese un precio válido mayor a 0.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validar campos de texto
            string nombre = txtNombre.Text.Trim();
            string duracion = txtDuracion.Text.Trim();
            string descripcion = txtDescripcion.Text.Trim();

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(duracion) || string.IsNullOrEmpty(descripcion))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Obtener lista de membresías existentes para validar duplicados
            var listaMembresias = _mostrarMembresia.MostrarMembresia(); // Método que devuelve todas las membresías

            // Validar duplicado: que no exista ya una membresía con el mismo nombre y mismo servicio
            bool yaExiste = listaMembresias.Any(m =>
                m.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase) &&
                m.IdServicio == idServicio);

            if (yaExiste)
            {
                MessageBox.Show("Ya existe una membresía con ese nombre para el servicio seleccionado. No se puede duplicar.",
                                "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Crear objeto de membresía
            var membresia = new MembresiaEN
            {
                IdServicio = idServicio,
                Nombre = nombre,
                Duracion = duracion,
                Precio = precio,
                Descripcion = descripcion
            };

            // Guardar membresía
            _mostrarMembresia.GuardarMembresia(membresia);

            // Limpiar campos
            txtNombre.Clear();
            txtDuracion.Clear();
            txtPrecio.Clear();
            txtDescripcion.Clear();

            MessageBox.Show("Membresía guardada correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

            // Recargar el grid o lista en WPF
            CargarGrid();
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (!byte.TryParse(TxtId.Text.Trim(), out byte id) || id <= 0)
            {
                MessageBox.Show("Por favor, seleccione un Id válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var membre = new MembresiaEN
            {
                Id = id
            };

            // Confirmar antes de eliminar
            MessageBoxResult confirmResult = MessageBox.Show("¿Estás seguro que deseas eliminar esta membresía?",
                                                             "Confirmar eliminación",
                                                             MessageBoxButton.YesNo,
                                                             MessageBoxImage.Warning);

            if (confirmResult == MessageBoxResult.Yes)
            {
                _mostrarMembresia.EliminarMembresia(membre);  // ✅ Ahora usas el objeto correcto
                CargarGrid();
                TxtId.Clear();
                MessageBox.Show("Membresía eliminada correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            // Validar ID
            if (!byte.TryParse(TxtId.Text.Trim(), out byte id) || id <= 0)
            {
                MessageBox.Show("Por favor, ingrese un Id válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validar servicio seleccionado
            if (CbxIdServicio.SelectedValue == null || !byte.TryParse(CbxIdServicio.SelectedValue.ToString(), out byte idServicio))
            {
                MessageBox.Show("Seleccione un servicio válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validar precio
            if (!int.TryParse(txtPrecio.Text.Trim(), out int precio) || precio <= 0)
            {
                MessageBox.Show("Ingrese un precio válido mayor a 0.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validar campos de texto
            string nombre = txtNombre.Text.Trim();
            string duracion = txtDuracion.Text.Trim();
            string descripcion = txtDescripcion.Text.Trim();

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(duracion) || string.IsNullOrEmpty(descripcion))
            {
                MessageBox.Show("Por favor, complete todos los campos de texto.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Confirmar modificación
            MessageBoxResult confirmResult = MessageBox.Show("¿Estás seguro que deseas modificar esta membresía?",
                                                             "Confirmar modificación",
                                                             MessageBoxButton.YesNo,
                                                             MessageBoxImage.Question);

            if (confirmResult == MessageBoxResult.No)
                return;

            // Crear objeto
            var membresia = new MembresiaEN
            {
                Id = id,
                IdServicio = idServicio,
                Nombre = nombre,
                Duracion = duracion,
                Precio = precio,
                Descripcion = descripcion
            };

            // Guardar cambios
            _mostrarMembresia.ModificarMembresia(membresia);

            // Limpiar
            TxtId.Clear();
            txtNombre.Clear();
            txtDuracion.Clear();
            txtPrecio.Clear();
            txtDescripcion.Clear();

            CargarGrid();

            MessageBox.Show("Membresía modificada correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnRefrescar_Click(object sender, RoutedEventArgs e)
        {
            CargarGrid();
            TxtBuscarMembresia.Clear();
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string nombre = TxtBuscarMembresia.Text;
            List<MembresiaEN> membresias = MembresiaBL.BuscarMembresia(nombre);
            DataMembresia.ItemsSource = membresias;
        }

        private void BtnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private DrawingVisual CrearTicketVisual(MembresiaEN membresia)
        {
            double anchoTicket = 280; // <<-- define el ancho real de tu ticket
            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext g = dv.RenderOpen())
            {
                double y = 20;

                // --- LOGO ---
                BitmapImage logito = new BitmapImage(
                    new Uri("pack://application:,,,/BreakingGymUI;component/LogoGod.png")
                );
                double logoWidth = 100;
                double logoHeight = 100;
                double logoX = (anchoTicket - logoWidth) / 2;
                g.DrawImage(logito, new Rect(logoX, y, logoWidth, logoHeight));
                y += logoHeight + 10;

                // --- Títulos ---
                FormattedText titulo1 = new FormattedText(
                    "BREAKING GYM",
                    System.Globalization.CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    new Typeface("Times New Roman Bold"),
                    16,
                    Brushes.Black,
                    VisualTreeHelper.GetDpi(this).PixelsPerDip);
                double xTitulo1 = (anchoTicket - titulo1.Width) / 2;
                g.DrawText(titulo1, new Point(xTitulo1, y));
                y += titulo1.Height + 5;

                FormattedText titulo2 = new FormattedText(
                    "TICKET MEMBRESÍA",
                    System.Globalization.CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    new Typeface("Times New Roman Bold"),
                    12,
                    Brushes.Black,
                    VisualTreeHelper.GetDpi(this).PixelsPerDip);
                double xTitulo2 = (anchoTicket - titulo2.Width) / 2;
                g.DrawText(titulo2, new Point(xTitulo2, y));
                y += titulo2.Height + 20;

                // --- Datos ---
                string[] datos =
                {
            $"Numero de membresia: {membresia.Id}",
            $"Membresia: {membresia.Nombre}",
            $"Servicio ID: {membresia.IdServicio}",
            $"Precio: ${membresia.Precio}",
            $"Duración: {membresia.Duracion}",
            $"Descripción: {membresia.Descripcion}"
        };

                foreach (var texto in datos)
                {
                    FormattedText linea = new FormattedText(
                        texto,
                        System.Globalization.CultureInfo.CurrentCulture,
                        FlowDirection.LeftToRight,
                        new Typeface("Times New Roman"),
                        10,
                        Brushes.Black,
                        VisualTreeHelper.GetDpi(this).PixelsPerDip);

                    double x = (anchoTicket - linea.Width) / 2;
                    g.DrawText(linea, new Point(x, y));
                    y += linea.Height + 10;
                }

                // --- QR ---
                string contenidoQR = string.Join("\n", datos);
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(contenidoQR, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                System.Drawing.Bitmap qrCodeBitmap = qrCode.GetGraphic(5);
                BitmapImage qrWpfImage = BitmapToImageSource(qrCodeBitmap);

                double qrSize = 120;
                double qrX = (anchoTicket - qrSize) / 2;
                g.DrawImage(qrWpfImage, new Rect(qrX, y, qrSize, qrSize));
            }

            return dv;
        }
        private BitmapImage BitmapToImageSource(DrawingBitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();
                return bitmapimage;
            }
        }
        private void btnCargar_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(TxtId.Text, out int idMembresia))
            {
                MessageBox.Show("Ingrese un Id válido.");
                return;
            }

            var membresias = _membresiaBL.MostrarMembresia();
            var memb = membresias.FirstOrDefault(m => m.Id == idMembresia);

            if (memb == null)
            {
                MessageBox.Show("No se encontró la membresía.");
                return;
            }

            membresiaParaImprimir = memb;

            txtNombre.Text = memb.Nombre;
            txtDuracion.Text = memb.Duracion;
            txtPrecio.Text = memb.Precio.ToString();
            txtDescripcion.Text = memb.Descripcion;
            CbxIdServicio.SelectedValue = memb.IdServicio;

            MessageBox.Show("Membresía cargada. Ahora puede imprimir.");
        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            // 1️⃣ Validar que haya una membresía cargada
            if (membresiaParaImprimir == null)
            {
                MessageBox.Show("Cargue primero una membresía.");
                return;
            }

            // 2️⃣ Crear el diálogo de impresión de WPF
            PrintDialog pd = new PrintDialog();

            if (pd.ShowDialog() == true)
            {
                // 3️⃣ Crear el ticket como DrawingVisual
                DrawingVisual ticket = CrearTicketVisual(membresiaParaImprimir);

                // 4️⃣ Enviar a la impresora
                pd.PrintVisual(ticket, "Ticket Membresía");
            }

            // 5️⃣ Limpiar campos de la UI
            TxtId.Clear();
            txtNombre.Clear();
            txtDuracion.Clear();
            CbxIdServicio.SelectedIndex = -1;
            txtPrecio.Clear();
            txtDescripcion.Clear();

            // 6️⃣ Limpiar la variable de membresía para imprimir
            membresiaParaImprimir = null;
        }

        private void CbxIdServicio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            CbxIdServicio.ItemsSource = _servicioBL.MostrarServicio();
            CbxIdServicio.DisplayMemberPath = "Nombre";   // lo que se muestra al usuario
            CbxIdServicio.SelectedValuePath = "Id";       // lo que se usa internamente
        }
    }
}

