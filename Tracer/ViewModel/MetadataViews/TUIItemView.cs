using Reflection.Metadata;

namespace ViewModel.MetadataViews
{
    public class TUIItemView
    {
        private TypeMetadata typeMetadata;

        public TUIItemView(TypeMetadata typeMetadata)
        {
            this.typeMetadata = typeMetadata;
        }

        public override string ToString()
        {
            string str = "";
            
            foreach (var field in typeMetadata.m_Fields)
            {
                str += new ParameterMetadataView(field);
                str += "\n";
            }
            
            foreach (var property in typeMetadata.m_Properties)
            {
                str += new PropertyMetadataView(property);
                str += "\n";
            }
            
            foreach (var method in typeMetadata.m_Methods)
            {
                str += new MethodMetadataView(method);
                str += "\n";
            }

            return str;
        }
    }
}
