using Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.MetadataViews
{
    class NamespaceMetadataView : BaseMetadataView, IExpander
    {
        public IEnumerable<TypeMetadata> Types;

        public NamespaceMetadataViewModel(NamespaceMetadata namespaceMetadata) : base(namespaceMetadata.NamespaceName)
        {
            Types = namespaceMetadata.Types;
        }

        public void Branch(ObservableCollection<ITreeViewItem> children)
        {
            if (Types != null)
                Add(Types, children);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
