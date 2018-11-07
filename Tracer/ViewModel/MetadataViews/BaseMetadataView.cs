using Reflection.Metadata;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ViewModel.MetadataViews
{
    public class BaseMetadataView
    {

        public string m_Name;
        public BaseMetadataView(string name)
        {
            m_Name = name;
        }

        public void Add(IEnumerable<PropertyMetadata> origin, ObservableCollection<TreeViewItem> container)
        {
            foreach (var item in origin)
            {
                PropertyMetadataView itemView = new PropertyMetadataView(item);
                container.Add(new TreeViewItem { Name = itemView.ToString(), m_ItemView = itemView });
            }
        }

        public void Add(IEnumerable<TypeMetadata> origin, ObservableCollection<TreeViewItem> container)
        {
            foreach (var item in origin)
            {
                TypeMetadataView itemView = new TypeMetadataView(TypeMetadata.storedTypes[item.m_typeName]);
                container.Add(new TreeViewItem { Name = itemView.ToString(), m_ItemView = itemView });
            }
        }

        public void Add(IEnumerable<MethodMetadata> origin, ObservableCollection<TreeViewItem> container)
        {
            foreach (var item in origin)
            {
                MethodMetadataView itemView = new MethodMetadataView(item);
                container.Add(new TreeViewItem { Name = itemView.ToString(), m_ItemView = itemView });
            }
        }

        public void Add(IEnumerable<NamespaceMetadata> origin, ObservableCollection<TreeViewItem> container)
        {
            foreach (var item in origin)
            {
                NamespaceMetadataView itemView = new NamespaceMetadataView(item);
                container.Add(new TreeViewItem { Name = itemView.ToString(), m_ItemView = itemView });
            }
        }

        public void Add(IEnumerable<ParameterMetadata> origin, ObservableCollection<TreeViewItem> container)
        {
            foreach (var item in origin)
            {
                ParameterMetadataView itemView = new ParameterMetadataView(item);
                container.Add(new TreeViewItem { Name = itemView.ToString(), m_ItemView = itemView });
            }
        }

        public void Add(TypeMetadata origin, ObservableCollection<TreeViewItem> container)
        {
            TypeMetadataView itemView = new TypeMetadataView(TypeMetadata.storedTypes[origin.m_typeName]); 
            container.Add(new TreeViewItem { Name = itemView.ToString(), m_ItemView = itemView });
        }
    }
}
