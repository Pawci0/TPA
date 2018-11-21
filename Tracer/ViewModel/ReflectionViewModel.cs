using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Tracer;
using ViewModel.MetadataViews;

namespace ViewModel
{
    public class ReflectionViewModel : BaseViewModel
    {
        private ITracer tracer;

        private AssemblyMetadataView assemblyMetadataView;
        private IFileSupplier fileSupplier;

        public ReflectionViewModel(IFileSupplier supplier, string tracerLogName)
        {
            tracer = new FileTracer(tracerLogName, TraceLevel.Warning);
            fileSupplier = supplier;
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
        public ICommand BrowseCommand { get; }
        public ICommand LoadDLLCommand { get; }
        
        private void LoadDLL()
        {
            try {
                tracer.Log(TraceLevel.Info, "load dll button clicked");
                if (PathVariable.Substring(PathVariable.Length - 4) == ".dll")
                {
                    assemblyMetadataView = new AssemblyMetadataView(PathVariable);
                    TreeViewLoaded();
                }
            }
            catch (System.SystemException)
            {
                tracer.Log(TraceLevel.Error, "tried to load without selecting a file");
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
            PathVariable = fileSupplier.GetFilePath();
            RaisePropertyChanged(nameof(PathVariable));
        }

    }
}
