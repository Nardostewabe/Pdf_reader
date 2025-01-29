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
using PDF_reader.DatabaseHandler;
using System.Data.SqlClient;
using static PDF_reader.Pdf_Manager.Mypdf;


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
                    _pdfDocument.Dispose(); 
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
        public bool AddPdf(string fileName, string filePath)
        {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    string query = "INSERT INTO PdfFiles (name, file_stream) OUTPUT INSERTED.stream_id VALUES (@FileName, (SELECT * FROM OPENROWSET(BULK @FilePath, SINGLE_BLOB) AS FileData))";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@FileName", fileName);
                    cmd.Parameters.AddWithValue("@FilePath", filePath);

                    conn.Open();
                    object result = cmd.ExecuteNonQuery();
                    return result != null;
                }
            
        }
        public static List<PdfFile> GetAllPdfs()
        {
            List<PdfFile> pdfList = new List<PdfFile>();

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    string query = "SELECT file_stream.PathName() AS filePath, creation_time FROM PdfFiles";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string filePath = reader["filePath"].ToString();
                        string fileName = Path.GetFileName(filePath);
                        pdfList.Add(new PdfFile
                        {
                            FileName = fileName,
                            FilePath = filePath,
                            DateAdded = Convert.ToDateTime(reader["creation_time"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving PDFs: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return pdfList;
        }

        public bool DeletePdf(string fileName)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = "DELETE FROM PdfFiles WHERE name = @FileName";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FileName", fileName);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}


