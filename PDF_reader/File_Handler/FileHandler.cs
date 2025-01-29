using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using PDF_reader.DatabaseHandler;

namespace PDF_reader.File_Handler
{
    internal class FileHandler
    {
        public string OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                Title = "Open PDF File"
            };

            return openFileDialog.ShowDialog() == true ? openFileDialog.FileName : null;
        }

        public bool FileExists(string filePath) => FileExists(filePath);
        public byte[] LoadFromDatabase(string fileName)
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    string query = "SELECT file_stream FROM PdfFiles WHERE name = @FileName";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FileName", fileName);
                        conn.Open();

                        object result = cmd.ExecuteScalar();
                        return result != null && result != DBNull.Value ? (byte[])result : null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading PDF from database: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public void SavePdfToLocal(string filePath, byte[] pdfData)
        {
            try
            {
                File.WriteAllBytes(filePath, pdfData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving PDF to local storage: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
