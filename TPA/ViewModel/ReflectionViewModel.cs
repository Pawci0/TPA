using DTGBase;
using MEF;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModel.MetadataViews;
using Interfaces;
using Reflection;

namespace ViewModel
{
    public class ReflectionViewModel : BaseViewModel
    {
        private AssemblyMetadataView assemblyMetadataView;

        private ReflectionRepository repository;

        [ImportMany(typeof(IFileSupplier))]
        private ImportSelector<IFileSupplier> fileSupplier;

        [ImportMany(typeof(ISerializer<AssemblyBase>))]
        private ImportSelector<ISerializer<AssemblyBase>> serializer;

        [ImportMany(typeof(ITracer))]
        private ImportSelector<ITracer> tracer;

        public ReflectionViewModel()
        {
            new Bootstrapper().ComposeApplication(this);
            tracer.GetImport().Log(TraceLevel.Verbose, "ViewModel initialization started");
            Tree = new ObservableCollection<BaseMetadataView>();
            LoadCommand = new RelayCommand(Load);
            NewDllCommand = new RelayCommand(NewDll);
            SaveCommand = new RelayCommand(Save);
            tracer.GetImport().Log(TraceLevel.Verbose, "ViewModel initialization finished");
            repository = new ReflectionRepository();
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
        public ICommand NewDllCommand { get; }
        public ICommand LoadCommand { get; }
        public ICommand SaveCommand { get; }
        
        private void Load()
        {
            try
            {
                tracer.GetImport().Log(TraceLevel.Info, "load button clicked");
                repository.Load(fileSupplier.GetImport());
                //AssemblyBase baseAssembly = serializer.GetImport().Deserialize(fileSupplier.GetImport());
                //assemblyMetadataView = new AssemblyMetadataView(baseAssembly);
                assemblyMetadataView = new AssemblyMetadataView(repository.Metadata);
                TreeViewLoaded();
                
            }
            catch (Exception e)
            {
                tracer.GetImport().Log(TraceLevel.Error, "");
            }
        }
        private void TreeViewLoaded()
        {
            tracer.GetImport().Log(TraceLevel.Verbose, "TreeView loading started");
            Tree.Add(assemblyMetadataView);
            tracer.GetImport().Log(TraceLevel.Verbose, "TreeView loading finished");
        }
        private void NewDll()
        {
            PathVariable = fileSupplier.GetImport().GetFilePathToLoad("Dynamic Library File(*.dll) | *.dll");
            if(PathVariable != null)
            {
                repository.CreateFromFile(PathVariable);
                assemblyMetadataView = new AssemblyMetadataView(repository.Metadata);
                TreeViewLoaded();
            }
            RaisePropertyChanged(nameof(PathVariable));
        }

        private void Save()
        {
            
                    tracer.GetImport().Log(TraceLevel.Verbose, "Saving assembly");
                    //serializer.GetImport().Serialize(fileSupplier.GetImport(), Reflection.Mappers.DTGMapper.ToBase(assemblyMetadataView.AssemblyMetadata));

            repository.Save(fileSupplier.GetImport());                
        }

    }
}
