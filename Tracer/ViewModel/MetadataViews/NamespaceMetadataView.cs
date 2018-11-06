using Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.MetadataViews
{
    class NamespaceMetadataView : BaseMetadataView, IExpander
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
