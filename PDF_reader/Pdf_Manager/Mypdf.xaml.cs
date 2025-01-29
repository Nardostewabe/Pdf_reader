using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Diagnostics;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PDF_reader.File_Handler;
using System.IO;
using PDF_reader.PDFReader;
using PDF_reader.Downloads;

namespace PDF_reader.Pdf_Manager
{
    /// <summary>
    /// Interaction logic for Mypdf.xaml
    /// </summary>
    public partial class Mypdf : Window
    {

        public class PdfFile
        {
            public string FileName { get; set; }
            public string FilePath { get; set; }
            public DateTime DateAdded { get; set; }
        }
        private FileHandler _fileManager = new FileHandler();
        List<PdfFile> pdfFiles = new List<PdfFile>();
        private PdfManager pdfManager = new PdfManager();
        public Mypdf()
        {
            InitializeComponent();
            LoadPdfList();

        }

        private void LoadPdfList()
        {
            pdfFiles = PdfManager.GetAllPdfs();
            PdfListView.ItemsSource = pdfFiles;
            StatusText.Text = $"Total PDFs: {pdfFiles.Count}";
        }


        private void AddPdf_Click(object sender, RoutedEventArgs e)
        {
            string filePath = _fileManager.OpenFile();

            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Please enter a valid file", "Failed", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (pdfManager.AddPdf(filePath))
                {
                    MessageBox.Show("PDF added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadPdfList();
                }
                else
                {
                    MessageBox.Show("Error adding PDF.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void DownloadPdf_click(object sender, RoutedEventArgs e)
        {
            if (PdfListView.SelectedItem is PdfFile selectedPdf)
            {
                try
                {
                    byte[] pdfData = _fileManager.LoadFromDatabase(selectedPdf.FileName);
                    if (pdfData != null)
                    {
                        string filePath = Path.Combine(@"D:/MyPdfs/", selectedPdf.FileName);
                        _fileManager.SavePdfToLocal(filePath, pdfData);
                        MessageBox.Show($"PDF downloaded successfully to {filePath}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        Mydownloads down = new Mydownloads();
                        down.Show();
                    }
                    else
                    {
                        MessageBox.Show("PDF not found in the database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error downloading PDF: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a PDF to download.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void MyDownloads_click(object sender, RoutedEventArgs e)
        {
            Mydownloads down = new Mydownloads();
            down.Show();
        }
    }
}
