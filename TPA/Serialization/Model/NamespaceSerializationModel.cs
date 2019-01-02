using DTGBase;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace XmlSerialization.Model
{
    [DataContract(Name = "NamespaceSerializationModel", IsReference = true)]
    public class NamespaceSerializationModel
    {
        private NamespaceSerializationModel()
        {

        }

        public NamespaceSerializationModel(NamespaceBase namespaceBase)
        {
            this.Name = namespaceBase.name;
            Types = namespaceBase.types?.Select(t => TypeSerializationModel.GetOrAdd(t));
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public IEnumerable<TypeSerializationModel> Types { get; set; }
    }
}
