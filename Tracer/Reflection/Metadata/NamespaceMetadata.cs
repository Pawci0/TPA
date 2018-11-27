using System;
using System.Collections.Generic;
using System.Linq;

namespace Reflection.Metadata
{
    public class NamespaceMetadata : BaseMetadata
    {
        #region Fields
        public string m_NamespaceName;
        public IEnumerable<TypeMetadata> m_Types;
        #endregion

        public NamespaceMetadata(string name, IEnumerable<Type> types)
        {
            m_NamespaceName = name;
            m_Types = from type in types
                      orderby type.Name
                      select new TypeMetadata(type);
        }
    }
}