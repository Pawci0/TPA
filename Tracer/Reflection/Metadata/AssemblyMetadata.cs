using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ReflectionTests")]
namespace Reflection.Metadata
{
    public class AssemblyMetadata
    {
        #region privateFields
        internal string m_Name;
        internal IEnumerable<NamespaceMetadata> m_Namespaces;
        #endregion

        internal AssemblyMetadata(Assembly assembly)
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