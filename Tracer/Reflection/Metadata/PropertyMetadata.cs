using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Reflection.Metadata
{
    public class PropertyMetadata
    {
        #region Fields
        public string m_Name;
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