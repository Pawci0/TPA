using DTGBase;
using System.Runtime.Serialization;

namespace Reflection.Metadata
{
    [DataContract]
    public class ParameterMetadata : BaseMetadata
    {
        #region Fields
        [DataMember]
        public string m_Name;
        [DataMember]
        public TypeMetadata m_TypeMetadata;
        #endregion

        public ParameterMetadata(string name, TypeMetadata typeMetadata)
        {
            m_Name = name;
            m_TypeMetadata = typeMetadata;
        }

        public ParameterMetadata(ParameterBase baseElement)
        {
            m_Name = baseElement.name;
            m_TypeMetadata = TypeMetadata.GetOrAdd(baseElement.typeMetadata);
        }
    }
}