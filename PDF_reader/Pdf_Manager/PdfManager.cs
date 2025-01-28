using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PdfiumViewer;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;


namespace PDF_reader.Pdf_Manager
{
    internal class PdfManager
    {
        private PdfDocument _pdfDocument;
        public int TotalPages { get; private set; }
        public int CurrentPage { get; set; }

        public PdfManager()
        {
            CurrentPage = 1;
        }

        public int PageCount()
        {
            return this.TotalPages;
        }

        public void LoadPDF(string filePath)
        {
            try
            {
                if (_pdfDocument != null)
                {
                    _pdfDocument.Dispose();  // Dispose of the previous document if any.
                }


                _pdfDocument = PdfDocument.Load(filePath);
                TotalPages = _pdfDocument.PageCount;
                CurrentPage = 0;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public PdfDocument GetPdfDocument()
        {
            return _pdfDocument;
        }

        public Bitmap RenderPage(int pageNumber, float scale = 1.0f)
        {
            if (_pdfDocument == null)
                throw new InvalidOperationException("Invalid page number.");

            return (Bitmap)_pdfDocument.Render(pageNumber - 1, 800, 1000, scale, scale, PdfRenderFlags.Annotations);
        }
        public void Close()
        {
            _pdfDocument?.Dispose();
            _pdfDocument = null;
        }
    }

    public static void AddPdf(int userId, string fileName, string filePath)
    {
        using (var connection = DatabaseHandler.GetConnection())
        {
            var command = new SqlCommand("INSERT INTO PDFs (UserId, FileName, FilePath) VALUES (@UserId, @FileName, @FilePath)", connection);
            command.Parameters.AddWithValue("@UserId", userId);
            command.Parameters.AddWithValue("@FileName", fileName);
            command.Parameters.AddWithValue("@FilePath", filePath);
            command.ExecuteNonQuery();
        }
    }

    public static List<Pdf> GetUserPdfs(int userId)
    {
        var pdfs = new List<Pdf>();
        using (var connection = DatabaseHandler.GetConnection())
        {
            var command = new SqlCommand("SELECT * FROM PDFs WHERE UserId = @UserId", connection);
            command.Parameters.AddWithValue("@UserId", userId);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    pdfs.Add(new Pdf
                    {
                        PdfId = reader.GetInt32(0),
                        FileName = reader.GetString(1),
                        FilePath = reader.GetString(2)
                    });
                }
            }
        }
        return pdfs;
    }

}


