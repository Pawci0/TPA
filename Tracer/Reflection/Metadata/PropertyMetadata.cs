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

        public static IEnumerable<PropertyMetadata> EmitProperties(IEnumerable<PropertyInfo> props)
        {
            return from prop in props
                   where prop.GetGetMethod().GetVisible() || prop.GetSetMethod().GetVisible()
                   select new PropertyMetadata(prop.Name, TypeMetadata.EmitReference(prop.PropertyType));
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