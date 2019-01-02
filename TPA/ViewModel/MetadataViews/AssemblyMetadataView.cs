using DTGBase;
using Reflection.Metadata;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ViewModel.MetadataViews
{
    public class AssemblyMetadataView : BaseMetadataView
    {
        public IEnumerable<NamespaceMetadata> Namespaces;

        public AssemblyMetadata AssemblyMetadata { get; }

        public AssemblyMetadataView(AssemblyMetadata assemblyMetadata)
        {
            AssemblyMetadata = assemblyMetadata;
            Name = assemblyMetadata.m_Name;
            Namespaces = assemblyMetadata.m_Namespaces;
        }

        public AssemblyMetadataView(string PathVariable) 
            : this(new AssemblyMetadata(Assembly.LoadFrom(PathVariable))){}

        public AssemblyMetadataView(AssemblyBase baseAssembly)
        {
            AssemblyMetadata = new AssemblyMetadata(baseAssembly);
            Name = baseAssembly.name;
            Namespaces = baseAssembly.namespaces?.Select(ns => new NamespaceMetadata(ns));
        }

        public override void Expand()
        {
            if (Namespaces != null)
                Add(Namespaces, i => new NamespaceMetadataView(i));
        }

        public override string ToString()
        {
            return Name;
        }

        public Dictionary<string, NamespaceMetadata> getNamespaceDict()
        {
            Dictionary<string, NamespaceMetadata> ret = new Dictionary<string, NamespaceMetadata>();
            foreach (var item in Namespaces)
            {
                ret.Add(item.m_NamespaceName, item);
            }
            return ret;
        }

        public void AddChildren(IEnumerable<NamespaceMetadata> origin)
        {
            foreach(NamespaceMetadata item in origin)
            {
                Children.Add(new NamespaceMetadataView(item));
            }
        }
    }
}
