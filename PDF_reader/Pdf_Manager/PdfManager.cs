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
using PDF_reader.PDFReader;
using System.Windows.Media.Media3D;


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

        public int GetTotalPages()
        {
            return _pdfDocument?.PageCount ?? 1;
        }


        public PdfDocument GetPdfDocument()
        {
            return _pdfDocument;
        }

        public Bitmap RenderPage(int pageNumber, float scale = 1.0f)
        {
            if (_pdfDocument == null)
            {
                MessageBox.Show("PDF document is not loaded.");
                return null;
            }

            if (pageNumber < 1 || pageNumber > _pdfDocument.PageCount)
            {
                MessageBox.Show("Invalid page number.");
                return null;
            }

            
            if (scale < 1.0f)
                scale = 1.0f;

            
            var pageSize = _pdfDocument.PageSizes[pageNumber - 1];

            if (pageSize.Width <= 0 || pageSize.Height <= 0)
            {
                MessageBox.Show("Invalid page size detected.");
                return null;
            }

            
            int width = (int)(pageSize.Width * scale);
            int height = (int)(pageSize.Height * scale);

            return (Bitmap)_pdfDocument.Render(pageNumber - 1, width, height, true);
        }


        public void Close()
        {
            _pdfDocument?.Dispose();
            _pdfDocument = null;
        }
        public bool AddPdf(string filePath)
        {
            try
            {
                byte[] fileBytes = File.ReadAllBytes(filePath);
                string fileName = Path.GetFileName(filePath);

                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    string query = "INSERT INTO PdfFiles (name, file_stream, creation_time) OUTPUT INSERTED.stream_id VALUES (@FileName, @FileData, @CreationTime)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@FileName", fileName);
                    cmd.Parameters.AddWithValue("@FileData", fileBytes);
                    cmd.Parameters.AddWithValue("@CreationTime", DateTime.Now);

                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return result != null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding PDF: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public static List<PdfFile> GetAllPdfs()
        {
            List<PdfFile> pdfList = new List<PdfFile>();

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    // Modify the query to get the file name from the database
                    string query = "SELECT name, file_stream.PathName() AS filePath, creation_time FROM PdfFiles";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string filePath = reader["filePath"].ToString();
                        string fileName = reader["name"].ToString();

                        // Handle the DateTimeOffset correctly
                        DateTime creationTime = reader["creation_time"] is DateTimeOffset
                            ? ((DateTimeOffset)reader["creation_time"]).DateTime
                            : Convert.ToDateTime(reader["creation_time"]);

                        pdfList.Add(new PdfFile
                        {
                            FileName = fileName, // Use the name from the database
                            FilePath = filePath,
                            DateAdded = creationTime
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

        public void LoadPDF(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    if (_pdfDocument != null)
                    {
                        _pdfDocument.Dispose(); // Dispose of previous document if any
                    }

                    _pdfDocument = PdfDocument.Load(filePath); // Load new document
                }
                else
                {
                    MessageBox.Show("File does not exist!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading PDF: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}


