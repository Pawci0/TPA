using DTGBase;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace XmlSerialization.Model
{
    [DataContract(Name = "AssemblySerializationModel", IsReference = true)]
    public class AssemblySerializationModel
    {
        public AssemblySerializationModel()
        {

        }

        public AssemblySerializationModel(AssemblyBase assemblyBase)
        {
            Name = assemblyBase.name;
            Namespaces = assemblyBase.namespaces?.Select(ns => new NamespaceSerializationModel(ns));

        }

        [DataMember]
        public IEnumerable<NamespaceSerializationModel> Namespaces { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
