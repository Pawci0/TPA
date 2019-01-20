using Microsoft.Win32;
using System.ComponentModel.Composition;
using Interfaces;

namespace GUI
{
    [Export(typeof(IFileSupplier))]
    class DialogFileSupplier : IFileSupplier
    {
        public string GetFilePathToLoad(string filter=null)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = filter
            };
            dialog.ShowDialog();
            return dialog.FileName;
        }

        public string GetFilePathToSave(string filter = null)
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = filter
            };
            dialog.ShowDialog();
            return dialog.FileName;
        }
    }
}
