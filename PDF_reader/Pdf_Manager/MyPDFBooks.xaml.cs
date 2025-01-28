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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace PDF_reader.Pdf_Manager
{
    /// <summary>
    /// Interaction logic for MyPDFBooks.xaml
    /// </summary>
    public partial class MyPDFBooks : Page
    {
        private int userId;
        public MyPDFBooks(int currentUserId)
        {
            InitializeComponent();
            userId = currentUserId;
            LoadUserPdfs();
        }

        private void LoadUserPdfs()
        {
            var pdfs = PdfManager.GetUserPdfs(userId);
            PdfListView.ItemsSource = pdfs;
        }

        private void AddPdfButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                string fileName = System.IO.Path.GetFileName(filePath);

                PdfManager.AddPdf(userId, fileName, filePath);
                LoadUserPdfs();
            }
        }

    }
}
