using Reflection.Metadata;
using System;
using System.Collections.ObjectModel;

namespace ViewModel.MetadataViews
{
    class PropertyMetadataView : BaseMetadataView
    {
        private PropertyMetadata propertyMetadata;

        public PropertyMetadataView(PropertyMetadata _propertyMetadata)
        {
            Name = _propertyMetadata.m_Name;
            propertyMetadata = _propertyMetadata;
        }

        public override void Expand()
        {
            if(propertyMetadata.m_TypeMetadata != null)
            {
                Children.Add(new TypeMetadataView(propertyMetadata.m_TypeMetadata));
            }
        }

        public override string ToString()
        {
            return propertyMetadata.m_TypeMetadata.m_typeName + " " + propertyMetadata.m_Name;
        }
    }
}
