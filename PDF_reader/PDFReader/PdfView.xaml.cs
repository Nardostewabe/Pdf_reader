using System;
using System.Collections.Generic;
using System.Drawing;
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
using PDF_reader.File_Handler;
using PDF_reader.Navigation_Manager;
using PDF_reader.Pdf_Manager;
using PDF_reader.Settings_manager;
using PdfiumViewer;

namespace PDF_reader.PDFReader
{
    /// <summary>
    /// Interaction logic for PdfView.xaml
    /// </summary>
    public partial class PdfView : Window
    {
        private PdfManager _pdfManager = new PdfManager();
        private NavigationManager _navHandler;
        private FileHandler _fileManager = new FileHandler();
        private SettingsManager _settingsManager = new SettingsManager();
        public PdfView(string filePath)
        {
            InitializeComponent();
            _navHandler = new NavigationManager(_pdfManager);
            LoadPdf(filePath);
        }

        private void LoadPdf(string filePath)
        {
            if (File.Exists(filePath))
            {
                _pdfManager.LoadPDF(filePath);
                DisplayPage();
            }
            else
            {
                MessageBox.Show("File not found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            _pdfManager.Close();
        }

        private void ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            float newZoom = _settingsManager.ZoomIn();
            DisplayPage(newZoom);
        }

        private void ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            float newZoom = _settingsManager.ZoomOut();
            DisplayPage(newZoom);

        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            _navHandler.NextPage();
            DisplayPage();
        }

        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            _navHandler.PreviousPage();
            DisplayPage();
        }
        private void DisplayPage(float scale = 1.0f)
        {
            Bitmap bitmap = _pdfManager.RenderPage(_navHandler.GetCurrentPage(), scale);
            PdfImage.Source = BitmapToImageSource(bitmap);
        }

        private ImageSource BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Position = 0;
                return BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }
        }
    }
}
