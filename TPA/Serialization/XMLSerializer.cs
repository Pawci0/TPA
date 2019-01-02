using DTGBase;
using System.ComponentModel.Composition;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using XmlSerialization.Model;

namespace Serialization
{
    [Export(typeof(ISerializer<AssemblyBase>))]
    public class XMLSerializer : ISerializer<AssemblyBase>
    {
        private readonly DataContractSerializer serializer = new DataContractSerializer(typeof(AssemblySerializationModel));

        public void Serialize(string filePath, AssemblyBase target)
        {
            AssemblySerializationModel assemblySerializationModel = new AssemblySerializationModel(target);
            using (FileStream writer = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                serializer.WriteObject(writer, assemblySerializationModel);
            }
        }

        public AssemblyBase Deserialize(string filePath)
        {
            using (FileStream reader = new FileStream(filePath, FileMode.Open))
            {
                return DTGMapper.AssemblyBase((AssemblySerializationModel)serializer.ReadObject(reader));
            }
        }
    }
}
