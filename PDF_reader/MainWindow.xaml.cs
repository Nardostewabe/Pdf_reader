using System.Drawing;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PDF_reader.File_Handler;
using PDF_reader.Navigation_Manager;
using PDF_reader.Pdf_Manager;
using PDF_reader.SearchManager;

namespace PDF_reader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PdfManager _pdfManager = new PdfManager();
        private NavigationManager _navHandler;
        private FileHandler _fileManager = new FileHandler();
        private Setting_manager _settingsManager = new Setting_manager();
        public MainWindow()
        {
            InitializeComponent();
            _navHandler = new NavigationManager(_pdfManager);
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            string filePath = _fileManager.OpenFile();
            if (!string.IsNullOrEmpty(filePath))
            {
                _pdfManager.LoadPDF(filePath);
                DisplayPage();
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {

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

        private void Books_clicked(object sender, RoutedEventArgs e)
        {

        }

        private void About_Click(object sender, RoutedEventArgs e)
        {

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

        private void Search_Click(object sender, RoutedEventArgs e)
        {

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

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}