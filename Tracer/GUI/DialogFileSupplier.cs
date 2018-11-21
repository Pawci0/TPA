using Microsoft.Win32;
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
