using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using ViewModel;
using ViewModel.MetadataViews;

namespace GUI
{
    public class MyViewModel : INotifyPropertyChanged
    {
        private AssemblyMetadataView assemblyMetadataView;

        public MyViewModel()
        {
            HierarchicalAreas = new ObservableCollection<TreeViewItem>();
            Click_Button = new RelayCommand(LoadDLL);
            Click_Browse = new RelayCommand(Browse);
        }
        
        public ObservableCollection<TreeViewItem> HierarchicalAreas { get; set; }
        public string PathVariable { get; set; }
        public Visibility ChangeControlVisibility { get; set; } = Visibility.Hidden;
        public ICommand Click_Browse { get; }
        public ICommand Click_Button { get; }
        
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName_)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName_));
        }
        private void LoadDLL()
        {
            if (PathVariable.Substring(PathVariable.Length - 4) == ".dll")
            {
                assemblyMetadataView = new AssemblyMetadataView(PathVariable);
                TreeViewLoaded();
            }
        }
        private void TreeViewLoaded()
        {
            TreeViewItem rootItem = new TreeViewItem {
                Name = assemblyMetadataView.m_Name,
                m_ItemView = assemblyMetadataView
            };
            HierarchicalAreas.Add(rootItem);
        }
        private void Browse()
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "Dynamic Library File(*.dll)| *.dll"
            };
            dialog.ShowDialog();
            if (dialog.FileName.Length == 0)
                MessageBox.Show("No files selected");
            else
            {
                PathVariable = dialog.FileName;
                ChangeControlVisibility = Visibility.Visible;
                RaisePropertyChanged("ChangeControlVisibility");
                RaisePropertyChanged("PathVariable");
            }
        }

    }
}
