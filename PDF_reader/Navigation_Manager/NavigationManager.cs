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
        private int _currentPage = 1;

        public NavigationManager(PdfManager pdfManager)
        {
            _pdfManager = pdfManager;
        }

        public void NextPage()
        {
            if (_currentPage < _pdfManager.GetTotalPages())
            {
                _currentPage++;
            }
        }

        public void PreviousPage()
        {
            if (_currentPage > 1)
            {
                _currentPage--;
            }
        }

        public int GetCurrentPage()
        {
            return _currentPage;
        }
    }
}