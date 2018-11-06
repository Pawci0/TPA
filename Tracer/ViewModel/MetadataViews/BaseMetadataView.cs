using Reflection.Metadata;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ViewModel.MetadataViews
{
    class BaseMetadataView
    {

        public string m_Name;
        public BaseMetadataView(string name)
        {
            m_Name = name;
        }

        public void Add(IEnumerable<PropertyMetadata> origin, ObservableCollection<TreeViewItem> container)
        {
            foreach (var p in origin)
            {
                PropertyMetadataView o = new PropertyMetadataView(p);
                container.Add(new TreeViewItem { Name = o.ToString(), m_expander = o });
            }
        }

        public void Add(IEnumerable<TypeMetadata> origin, ObservableCollection<TreeViewItem> container)
        {
            foreach (var t in origin)
            {
                TypeMetadataView o = new TypeMetadataView(t.m_BaseType);
                container.Add(new TreeViewItem { Name = o.ToString(), m_expander = o });
            }
        }

        public void Add(IEnumerable<MethodMetadata> origin, ObservableCollection<TreeViewItem> container)
        {
            foreach (var m in origin)
            {
                MethodMetadataView o = new MethodMetadataView(m);
                container.Add(new TreeViewItem { Name = o.ToString(), m_expander = o });
            }
        }

        public void Add(IEnumerable<NamespaceMetadata> origin, ObservableCollection<TreeViewItem> container)
        {
            foreach (var n in origin)
            {
                NamespaceMetadataView o = new NamespaceMetadataView(n);
                container.Add(new TreeViewItem { Name = o.ToString(), m_expander = o });
            }
        }

        public void Add(IEnumerable<ParameterMetadata> origin, ObservableCollection<TreeViewItem> container)
        {
            foreach (var p in origin)
            {
                ParameterMetadataView o = new ParameterMetadataView(p);
                container.Add(new TreeViewItem { Name = o.ToString(), m_expander = o });
            }
        }

        public void Add(TypeMetadata origin, ObservableCollection<TreeViewItem> container)
        {
            TypeMetadataView o = new TypeMetadataView(origin.m_BaseType); 
            container.Add(new TreeViewItem { Name = o.ToString(), m_expander = o });
        }
    }
}
