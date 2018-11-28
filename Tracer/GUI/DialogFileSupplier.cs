using Microsoft.Win32;
using ViewModel;

namespace GUI
{
    class DialogFileSupplier : IFileSupplier
    {
        public string GetFilePathToLoad()
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "Dynamic Library File(*.dll)| *.dll| XML File(*.xml)| *.xml"
            };
            dialog.ShowDialog();
            return dialog.FileName;
        }

        public string GetFilePathToSave()
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "XML File(*.xml)| *.xml"
            };
            dialog.ShowDialog();
            return dialog.FileName;
        }
    }
}
