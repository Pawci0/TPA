using Reflection.Metadata;
using System.Collections.Generic;

namespace ViewModel.MetadataViews
{
    class NamespaceMetadataView : BaseMetadataView
    {
        public IEnumerable<TypeMetadata> Types;

        public NamespaceMetadataView(NamespaceMetadata namespaceMetadata)
        {
            Name = namespaceMetadata.m_NamespaceName;
            Types = namespaceMetadata.m_Types;
        }

        public override void Expand()
        {
            if (Types != null)
            {
                //AddChildren(Types);
                Add(Types, i => new TypeMetadataView(i));
            }
        }

        private void AddChildren(IEnumerable<TypeMetadata> types)
        {
            foreach(TypeMetadata item in types)
            {
                Children.Add(new TypeMetadataView(item));
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
