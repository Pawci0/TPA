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
            this.m_Name = name;
            this.m_TypeMetadata = typeMetadata;
        }
    }
}