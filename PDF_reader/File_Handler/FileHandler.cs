using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

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
    }
}
