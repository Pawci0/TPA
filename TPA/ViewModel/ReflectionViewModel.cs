using MEF;
using Reflection.Metadata;
using Serialization;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Tracer;
using ViewModel.MetadataViews;

namespace ViewModel
{
    public class ReflectionViewModel : BaseViewModel
    {
        private AssemblyMetadataView assemblyMetadataView;

        [ImportMany(typeof(IFileSupplier))]
        private ImportSelector<IFileSupplier> fileSupplier;

        [ImportMany(typeof(ISerializer))]
        private ImportSelector<ISerializer> serializer;

        [ImportMany(typeof(ITracer))]
        private ImportSelector<ITracer> tracer;

        public ReflectionViewModel()
        {
            new Bootstrapper().ComposeApplication(this);
            tracer.GetImport().Log(TraceLevel.Verbose, "ViewModel initialization started");
            Tree = new ObservableCollection<BaseMetadataView>();
            LoadDLLCommand = new RelayCommand(LoadDLL);
            BrowseCommand = new RelayCommand(Browse);
            SaveCommand = new RelayCommand(Save);
            tracer.GetImport().Log(TraceLevel.Verbose, "ViewModel initialization finished");
        }
        
        public ObservableCollection<BaseMetadataView> Tree { get; set; }
        public string m_PathVariable;
        public string PathVariable {
            get { return this.m_PathVariable; }
            set
            {
                tracer.GetImport().Log(TraceLevel.Info, "path do dll file changed to \"" + value + "\"");
                this.m_PathVariable = value;
            }
        }
        public ICommand BrowseCommand { get; }
        public ICommand LoadDLLCommand { get; }
        public ICommand SaveCommand { get; }
        
        private void LoadDLL()
        {
            try {
                tracer.GetImport().Log(TraceLevel.Info, "load button clicked");
                if (PathVariable.Substring(PathVariable.Length - 4) == ".dll")
                {
                    tracer.GetImport().Log(TraceLevel.Info, "selected DLL file");
                    assemblyMetadataView = new AssemblyMetadataView(PathVariable);
                    TreeViewLoaded();
                }
                else if (PathVariable.Substring(PathVariable.Length - 4) == ".xml")
                {
                    tracer.GetImport().Log(TraceLevel.Info, "selected XML file");
                    assemblyMetadataView = new AssemblyMetadataView(serializer.GetImport().Deserialize<AssemblyMetadata>(PathVariable));
                    TreeViewLoaded();
                }
            }
            catch (System.SystemException)
            {
                tracer.GetImport().Log(TraceLevel.Error, "tried to load without selecting a file");
            }
        }
        private void TreeViewLoaded()
        {
            tracer.GetImport().Log(TraceLevel.Verbose, "TreeView loading started");
            Tree.Add(assemblyMetadataView);
            tracer.GetImport().Log(TraceLevel.Verbose, "TreeView loading finished");
        }
        private void Browse()
        {
            PathVariable = fileSupplier.GetImport().GetFilePathToLoad();
            RaisePropertyChanged(nameof(PathVariable));
        }

        private void Save()
        {
            Task.Run(() =>
            {
                try
                {
                    tracer.GetImport().Log(TraceLevel.Verbose, "Saving assembly to XML");
                    string fileName = fileSupplier.GetImport().GetFilePathToSave();
                    if(fileName != "")
                    {
                        serializer.GetImport().Serialize(fileName, assemblyMetadataView.AssemblyMetadata);
                    }
                    else
                    {
                        tracer.GetImport().Log(TraceLevel.Warning, "No file selected");
                    }
                }
                catch (Exception e)
                {
                    tracer.GetImport().Log(TraceLevel.Error, "Serialization threw an exception: " + e.Message);
                }
            });
        }

    }
}
