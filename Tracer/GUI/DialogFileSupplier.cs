using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace GUI
{
    class DialogFileSupplier : IFileSupplier
    {
        public string GetFilePath()
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "Dynamic Library File(*.dll)| *.dll"
            };
            dialog.ShowDialog();
            return dialog.FileName;
        }
    }
}
