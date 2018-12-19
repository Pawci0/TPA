using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Runtime.Serialization;

namespace Reflection.Metadata
{
    [DataContract]
    public class PropertyMetadata : BaseMetadata
    {
        #region Fields
        [DataMember]
        public string m_Name;
        [DataMember]
        public TypeMetadata m_TypeMetadata;
        #endregion

        public static IEnumerable<PropertyMetadata> EmitProperties(IEnumerable<PropertyInfo> properties)
        {
            return from property in properties
                   where property.GetGetMethod().GetVisible() || property.GetSetMethod().GetVisible()
                   select new PropertyMetadata(property.Name, TypeMetadata.EmitReference(property.PropertyType));
        }

        #region privateMethods
        private PropertyMetadata(string propertyName, TypeMetadata propertyType)
        {
            m_Name = propertyName;
            m_TypeMetadata = propertyType;
        }
        #endregion
    }
}