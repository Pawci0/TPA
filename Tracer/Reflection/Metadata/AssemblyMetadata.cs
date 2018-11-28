using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Reflection.Metadata
{
    [DataContract]
    public class AssemblyMetadata : BaseMetadata
    {
        #region Fields
        [DataMember]
        public string m_Name;
        [DataMember]
        public IEnumerable<NamespaceMetadata> m_Namespaces;
        #endregion

        public AssemblyMetadata(Assembly assembly)
        {
            m_Name = assembly.ManifestModule.Name;
            m_Namespaces = from Type _type in assembly.GetTypes()
                           where _type.GetVisible()
                           group _type by _type.GetNamespace() into _group
                           orderby _group.Key
                           select new NamespaceMetadata(_group.Key, _group);
        }
    }
}