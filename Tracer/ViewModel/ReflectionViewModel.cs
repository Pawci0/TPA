using Reflection.Metadata;
using Serialization;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
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
        private ISerializer serializer;

        public ReflectionViewModel(IFileSupplier supplier, ISerializer serializer, string tracerLogName)
        {
            this.serializer = serializer;
            tracer = new FileTracer(tracerLogName, TraceLevel.Warning);
            fileSupplier = supplier;
            tracer.Log(TraceLevel.Verbose, "ViewModel initialization started");
            Tree = new ObservableCollection<BaseMetadataView>();
            LoadDLLCommand = new RelayCommand(LoadDLL);
            BrowseCommand = new RelayCommand(Browse);
            SaveCommand = new RelayCommand(Save);
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
        public ICommand SaveCommand { get; }
        
        private void LoadDLL()
        {
            try {
                tracer.Log(TraceLevel.Info, "load button clicked");
                if (PathVariable.Substring(PathVariable.Length - 4) == ".dll")
                {
                    tracer.Log(TraceLevel.Info, "selected DLL file");
                    assemblyMetadataView = new AssemblyMetadataView(PathVariable);
                    TreeViewLoaded();
                }
                else if (PathVariable.Substring(PathVariable.Length - 4) == ".xml")
                {
                    tracer.Log(TraceLevel.Info, "selected XML file");
                    assemblyMetadataView = new AssemblyMetadataView(serializer.Deserialize<AssemblyMetadata>(PathVariable));
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
            Task.Run(() =>
            {
                PathVariable = fileSupplier.GetFilePathToLoad();
                RaisePropertyChanged(nameof(PathVariable));
            });
        }

        private void Save()
        {
            Task.Run(() =>
            {
                try
                {
                    tracer.Log(TraceLevel.Verbose, "Saving assembly to XML");
                    string fileName = fileSupplier.GetFilePathToSave();
                    if(fileName != "")
                    {
                        serializer.Serialize(fileName, assemblyMetadataView.AssemblyMetadata);
                    }
                    else
                    {
                        tracer.Log(TraceLevel.Warning, "No file selected");
                    }
                }
                catch (Exception e)
                {
                    tracer.Log(TraceLevel.Error, "Serialization threw an exception: " + e.Message);
                }
            });
        }

    }
}
