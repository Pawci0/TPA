using DTGBase;
using Interfaces;
using MEF;
using Reflection.Mappers;
using Reflection.Metadata;
using System;
using System.ComponentModel.Composition;
using System.Reflection;

namespace Reflection
{
    public class ReflectionRepository
    {
        public AssemblyMetadata Metadata { get; set; }

        [ImportMany]
        ImportSelector<ISerializer<AssemblyBase>> serializer;
        
        public ReflectionRepository()
        {
            new Bootstrapper().ComposeApplication(this);
        }

        public void Save(IFileSupplier supplier)
        {
            if(Metadata == null)
            {
                throw new InvalidOperationException("No metadata to save");
            }

            serializer.GetImport().Serialize(supplier, DTGMapper.ToBase(Metadata));
        }

        public void Load(IFileSupplier supplier)
        {
            Metadata = new AssemblyMetadata(serializer.GetImport().Deserialize(supplier));
        }

        public void CreateFromFile(string path)
        {
            Metadata = new AssemblyMetadata(Assembly.LoadFrom(path));
        }
    }
}
