using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Runtime.Serialization;
using DTGBase;

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

        public PropertyMetadata(PropertyBase baseProperty)
        {
            m_Name = baseProperty.name;
            m_TypeMetadata = TypeMetadata.GetOrAdd(baseProperty.typeMetadata);
        }
        #endregion
    }
}