using DTGBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Reflection.Metadata
{
    [DataContract]
    public class NamespaceMetadata : BaseMetadata
    {
        #region Fields
        [DataMember]
        public string m_NamespaceName;
        [DataMember]
        public IEnumerable<TypeMetadata> m_Types;
        #endregion

        public NamespaceMetadata(string name, IEnumerable<Type> types)
        {
            m_NamespaceName = name;
            m_Types = from type in types
                      orderby type.Name
                      select new TypeMetadata(type);
        }
        public NamespaceMetadata(NamespaceBase namespaceBase)
        {
            m_NamespaceName= namespaceBase.name;
            m_Types = namespaceBase.types?.Select(t => TypeMetadata.GetOrAdd(t));
        }
    }
}