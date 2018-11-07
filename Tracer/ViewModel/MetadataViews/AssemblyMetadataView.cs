using Reflection.Metadata;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace ViewModel.MetadataViews
{
    public class AssemblyMetadataView : BaseMetadataView, IExpander
    {
        public IEnumerable<NamespaceMetadata> Namespaces;

        public AssemblyMetadataView(AssemblyMetadata assemblyMetadata) : base(assemblyMetadata.m_Name)
        {
            Namespaces = assemblyMetadata.m_Namespaces;
        }

        public AssemblyMetadataView(string PathVariable) 
            : this(new AssemblyMetadata(Assembly.LoadFrom(PathVariable))){}

        public void Expand(ObservableCollection<TreeViewItem> chilren)
        {
            if (Namespaces != null)
                Add(Namespaces, chilren);
        }

        public override string ToString()
        {
            return m_Name;
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
    }
}
