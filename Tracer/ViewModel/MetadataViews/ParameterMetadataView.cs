using Reflection.Metadata;
using System.Collections.ObjectModel;

namespace ViewModel.MetadataViews
{
    class ParameterMetadataView : BaseMetadataView, IExpander
    {
        private ParameterMetadata Parameter;

        public ParameterMetadataView(ParameterMetadata parameterMetadata) : base(parameterMetadata.m_Name)
        {
            Parameter = parameterMetadata;
        }

        public void Expand(ObservableCollection<TreeViewItem> children)
        {
            if (Parameter.m_TypeMetadata != null)
                Add(Parameter.m_TypeMetadata, children);
        }

        public override string ToString()
        {
            return Parameter.m_TypeMetadata.m_typeName + " " + Parameter.m_Name;
        }
    }
}
