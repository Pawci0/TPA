using DTGBase;
using System.ComponentModel.Composition;
using System.IO;
using System.Runtime.Serialization;
using XmlSerialization.Model;
using Interfaces;

namespace Serialization
{
    [Export(typeof(ISerializer<AssemblyBase>))]
    public class XMLSerializer : ISerializer<AssemblyBase>
    {
        private readonly DataContractSerializer serializer = new DataContractSerializer(typeof(AssemblySerializationModel));

        public void Serialize(IFileSupplier supplier, AssemblyBase target)
        {
            AssemblySerializationModel assemblySerializationModel = new AssemblySerializationModel(target);
            string path = supplier.GetFilePathToSave();
            using (FileStream writer = new FileStream(path, FileMode.OpenOrCreate))
            {
                serializer.WriteObject(writer, assemblySerializationModel);
            }
        }

        public AssemblyBase Deserialize(string filename)
        {
            using (FileStream reader = new FileStream(filename, FileMode.Open))
            {
                return DTGMapper.ToBase((AssemblySerializationModel)serializer.ReadObject(reader));
            }
        }
    }
}
