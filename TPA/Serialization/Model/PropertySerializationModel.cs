using DTGBase;
using System.Runtime.Serialization;

namespace XmlSerialization.Model
{
    [DataContract(Name = "PropertySerializationModel", IsReference = true)]
    public class PropertySerializationModel
    {
        private PropertySerializationModel()
        {

        }

        public PropertySerializationModel(PropertyBase baseProperty)
        {
            Name = baseProperty.name;
            Type = TypeSerializationModel.GetOrAdd(baseProperty.typeMetadata);
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public TypeSerializationModel Type { get; set; }
    }
}
