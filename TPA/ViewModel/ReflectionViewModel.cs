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
        
        private async void Load()
        {
            await Task.Run(() =>
            {
                try
                {
                    tracer.GetImport().Log(TraceLevel.Info, "load button clicked");
                    repository.Load(fileSupplier.GetImport());
                    assemblyMetadataView = new AssemblyMetadataView(repository.Metadata);
                }
                catch (Exception e)
                {
                    tracer.GetImport().Log(TraceLevel.Error, "something went wrong with saving: " + e.Message);
                }
            });
            TreeViewLoaded();
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
            if(PathVariable != null && !PathVariable.Equals(""))
            {
                repository.CreateFromFile(PathVariable);
                assemblyMetadataView = new AssemblyMetadataView(repository.Metadata);
                TreeViewLoaded();
            }
        }

        private void Save()
        {
            Task.Run(() =>
            {
                try
                {
                    tracer.GetImport().Log(TraceLevel.Verbose, "Saving assembly");
                    repository.Save(fileSupplier.GetImport());
                    tracer.GetImport().Log(TraceLevel.Verbose, "Succesfully saved");
                }catch(Exception e)
                {
                    tracer.GetImport().Log(TraceLevel.Error, "something went wrong with saving: " + e.Message);
                }
            });
        }
    }
}
