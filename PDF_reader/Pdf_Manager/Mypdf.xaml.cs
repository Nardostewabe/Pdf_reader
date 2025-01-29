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

namespace PDF_reader.Pdf_Manager
{
    /// <summary>
    /// Interaction logic for Mypdf.xaml
    /// </summary>
    public partial class Mypdf : Window { 

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
            string fileName = Path.GetFileName(filePath);
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Please enter a valid file","Failed", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (pdfManager.AddPdf(fileName, filePath))
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

        private void OpenPdf_Click(object sender, RoutedEventArgs e)
        {
            if (PdfListView.SelectedItem is PdfFile selectedPdf)
            {
                PdfView readerWindow = new PdfView(selectedPdf.FilePath);
                readerWindow.Show();
            }
            else
            {
                MessageBox.Show("Please select a PDF to open.", "No PDF Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeletePdf_Click(object sender, RoutedEventArgs e)
        {
            if (PdfListView.SelectedItem is PdfFile selectedPdf)
            {
                if (MessageBox.Show("Are you sure you want to delete this PDF?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (pdfManager.DeletePdf(selectedPdf.FileName))
                    {
                        MessageBox.Show("PDF deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadPdfList();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a PDF to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }


}
