using Reflection.Metadata;
using System;
using System.Collections.ObjectModel;

namespace ViewModel.MetadataViews
{
    class PropertyMetadataView : BaseMetadataView, IExpander
    {
        private PropertyMetadata propertyMetadata;

        public PropertyMetadataView(PropertyMetadata _propertyMetadata) 
            : base(_propertyMetadata.m_Name)
        {
            propertyMetadata = _propertyMetadata;
        }

        public void Expand(ObservableCollection<TreeViewItem> children)
        {
            if(propertyMetadata.m_TypeMetadata != null)
            {
                base.Add(propertyMetadata.m_TypeMetadata, children);
            }
        }

        public override string ToString()
        {
            return propertyMetadata.m_TypeMetadata.m_typeName + " " + propertyMetadata.m_Name;
        }
    }
}
