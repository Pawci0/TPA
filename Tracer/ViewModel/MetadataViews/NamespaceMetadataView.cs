using Reflection.Metadata;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ViewModel.MetadataViews
{
    public class NamespaceMetadataView : BaseMetadataView, IExpander
    {
        public IEnumerable<TypeMetadata> Types;

        public NamespaceMetadataView(NamespaceMetadata namespaceMetadata) : base(namespaceMetadata.m_NamespaceName)
        {
            Types = namespaceMetadata.m_Types;
        }

        public void Expand(ObservableCollection<TreeViewItem> children)
        {
            if (Types != null)
                Add(Types, children);
        }

        public override string ToString()
        {
            return m_Name;
        }
    }
}
