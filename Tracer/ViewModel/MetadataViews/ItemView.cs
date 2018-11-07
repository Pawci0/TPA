using Reflection.Metadata;

namespace ViewModel.MetadataViews
{
    public class ItemViewModel
    {
        private TypeMetadata typeMetadata;

        public ItemViewModel(TypeMetadata typeMetadata)
        {
            this.typeMetadata = typeMetadata;
        }

        public override string ToString()
        {
            string str = "";

            // fields
            foreach (var field in typeMetadata.m_Fields)
            {
                str += new ParameterMetadataView(field);
                str += "\n";
            }

            // properties
            foreach (var property in typeMetadata.m_Properties)
            {
                str += new PropertyMetadataView(property);
                str += "\n";
            }

            // methods
            foreach (var method in typeMetadata.m_Methods)
            {
                str += new MethodMetadataView(method);
                str += "\n";
            }

            return str;
        }
    }
}
