using Reflection.Metadata;
using System;
using System.Collections.ObjectModel;

namespace ViewModel.MetadataViews
{
    class ParameterMetadataView : BaseMetadataView, IExpandable
    {
        private ParameterMetadata Parameter;

        public ParameterMetadataView(ParameterMetadata parameterMetadata)
        {
            Name = parameterMetadata.m_Name;
            Parameter = parameterMetadata;
        }

        public override void Expand()
        {
            if (Parameter.m_TypeMetadata != null)
            {
                //Children.Add(new TypeMetadataView(Parameter.m_TypeMetadata));
                Children.Add(new TypeMetadataView(Parameter.m_TypeMetadata));
            }
        }

        public override string ToString()
        {
            return Parameter.m_TypeMetadata.m_typeName + " " + Parameter.m_Name;
        }
    }
}
