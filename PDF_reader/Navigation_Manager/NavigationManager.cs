using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PDF_reader.Pdf_Manager;

namespace PDF_reader.Navigation_Manager
{
    internal class NavigationManager
    {
     
            private PdfManager _pdfManager;

            public NavigationManager(PdfManager pdfManager)
            {
                _pdfManager = pdfManager;
            }

            public void NextPage()
            {
                if (_pdfManager.CurrentPage < _pdfManager.PageCount())
                    _pdfManager.CurrentPage++;
            }

            public void PreviousPage()
            {
                if (_pdfManager.CurrentPage > 1)
                    _pdfManager.CurrentPage--;
            }

            public int GetCurrentPage() => _pdfManager.CurrentPage;
        }

    }
