using Reflection.Metadata;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ViewModel.MetadataViews
{
    public class AssemblyMetadataView : BaseMetadataView, IExpander
    {
        public IEnumerable<NamespaceMetadata> Namespaces;

        public AssemblyMetadataView(AssemblyMetadata assemblyMetadata) : base(assemblyMetadata.m_Name)
        {
            Namespaces = assemblyMetadata.m_Namespaces;
        }

        public void Expand(ObservableCollection<TreeViewItem> chilren)
        {
            if (Namespaces != null)
                Add(Namespaces, chilren);
        }

        public override string ToString()
        {
            return m_Name;
        }
    }
}
