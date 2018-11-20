using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using Tracer;
using ViewModel.MetadataViews;

namespace ViewModel
{
    public class ReflectionViewModel : BaseViewModel
    {
        private static ITracer tracer = new FileTracer("GUI.log", TraceLevel.Warning);

        private AssemblyMetadataView assemblyMetadataView;

        public ReflectionViewModel()
        {
            tracer.Log(TraceLevel.Verbose, "ViewModel initialization started");
            Tree = new ObservableCollection<BaseMetadataView>();
            LoadDLLCommand = new RelayCommand(LoadDLL);
            BrowseCommand = new RelayCommand(Browse);
            tracer.Log(TraceLevel.Verbose, "ViewModel initialization finished");
        }
        
        public ObservableCollection<BaseMetadataView> Tree { get; set; }
        public string m_PathVariable;
        public string PathVariable {
            get { return this.m_PathVariable; }
            set
            {
                tracer.Log(TraceLevel.Info, "path do dll file changed to \"" + value + "\"");
                this.m_PathVariable = value;
            }
        }
        public Visibility ChangeControlVisibility { get; set; } = Visibility.Hidden;
        public ICommand BrowseCommand { get; }
        public ICommand LoadDLLCommand { get; }
        
        private void LoadDLL()
        {
            tracer.Log(TraceLevel.Info, "load dll button clicked");
            if (PathVariable.Substring(PathVariable.Length - 4) == ".dll")
            {
                assemblyMetadataView = new AssemblyMetadataView(PathVariable);
                TreeViewLoaded();
            }
        }
        private void TreeViewLoaded()
        {
            tracer.Log(TraceLevel.Verbose, "TreeView loading started");
            Tree.Add(assemblyMetadataView);
            tracer.Log(TraceLevel.Verbose, "TreeView loading finished");
        }
        private void Browse()
        {
            tracer.Log(TraceLevel.Info, "browse button clicked");
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "Dynamic Library File(*.dll)| *.dll"
            };
            dialog.ShowDialog();
            if (dialog.FileName.Length == 0)
            {
                tracer.Log(TraceLevel.Warning, "no file selected");
                MessageBox.Show("No files selected");
            }
            else
            {
                PathVariable = dialog.FileName;
                ChangeControlVisibility = Visibility.Visible;
                RaisePropertyChanged(nameof(ChangeControlVisibility));
                RaisePropertyChanged(nameof(PathVariable));
            }
        }

    }
}
