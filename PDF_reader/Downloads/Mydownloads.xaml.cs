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
using System.IO;
using PDF_reader.PDFReader;

namespace PDF_reader.Downloads
{
    /// <summary>
    /// Interaction logic for Mydownloads.xaml
    /// </summary>
    public partial class Mydownloads : Window
    {
        public class DownloadedPdf
        {
            public string FileName { get; set; }
            public DateTime DateAdded { get; set; }
        }
        public Mydownloads()
        {
            InitializeComponent();
            LoadDownloadedPdfs();
        }
        private void LoadDownloadedPdfs()
        {
           
            string downloadDirectory = @"D:/MyPdfs/";

            
            var pdfFiles = Directory.GetFiles(downloadDirectory, "*.pdf");

            
            var downloadedPdfs = pdfFiles.Select(file =>
            {
                return new DownloadedPdf
                {
                    FileName = Path.GetFileName(file),
                    DateAdded = File.GetCreationTime(file) // Or use last write time if needed
                };
            }).ToList();

          
            DownloadsListView.ItemsSource = downloadedPdfs;
        }

        private void OpenPdfButton_Click(object sender, RoutedEventArgs e)
        {
            
            var selectedPdf = (DownloadedPdf)DownloadsListView.SelectedItem;

            if (selectedPdf != null)
            {
                
                string filePath = Path.Combine(@"D:/MyPdfs/", selectedPdf.FileName);

                
                if (File.Exists(filePath))
                {
                    OpenPdf(filePath);
                }
                else
                {
                    MessageBox.Show("The selected PDF could not be found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a PDF to open.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void OpenPdf(string filePath)
        {
            try
            {
                
                PdfView pdfViewWindow = new PdfView(filePath);
                pdfViewWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening PDF: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
