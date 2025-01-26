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
            this.CurrentPage = 0;
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

    }


