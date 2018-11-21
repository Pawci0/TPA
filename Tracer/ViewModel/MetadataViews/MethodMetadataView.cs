using Reflection.Metadata;
using System.Collections.Generic;
using static ViewModel.MetadataViews.KeywordUtils;


namespace ViewModel.MetadataViews
{
    class MethodMetadataView : BaseMetadataView
    {
        private MethodMetadata Method { get; set; }

        public MethodMetadataView(MethodMetadata methodMetadata)
        {
            Name = methodMetadata.m_Name;
            Method = methodMetadata;
        }

        public override string ToString()
        {
            string str = "";
            str += AccessLevelToString(Method.m_Modifiers.Item1);
            str += " " + StaticToString(Method.m_Modifiers.Item3);
            str = str.Trim();
            str += " " +VirtualToString(Method.m_Modifiers.Item4);
            str = str.Trim();
            str += " " + AbstractToString(Method.m_Modifiers.Item2);

            if (Method.m_ReturnType != null)
            {
                str = str.Trim();
                str += " " + Method.m_ReturnType.m_typeName;
            }

            str = str.Trim();
            str += " " + Method.m_Name;

            str += "(";
            foreach (var parameterMetadata in Method.m_Parameters)
            {
                str += parameterMetadata.m_TypeMetadata.m_typeName + " " + parameterMetadata.m_Name + ", ";
            }
            str = str.TrimEnd(new char[] { ',', ' ' });
            str += ")";

            return str;
        }

        public override void Expand()
        {
            if (Method.m_GenericArguments != null)
            {
                Add(Method.m_GenericArguments, i => new TypeMetadataView(i));
            }
            if (Method.m_Parameters != null)
            {
                Add(Method.m_Parameters, i => new ParameterMetadataView(i));
            }
            if (Method.m_ReturnType != null)
            {
                Add(new List<TypeMetadata> { Method.m_ReturnType }, i => new TypeMetadataView(i));
            }
        }

        private void AddChildren(IEnumerable<ParameterMetadata> parameters)
        {
            foreach (ParameterMetadata item in parameters)
            {
                Children.Add(new ParameterMetadataView(item));
            }
        }

        private void AddChildren(IEnumerable<TypeMetadata> types)
        {
            foreach (var item in types)
            {
                Children.Add(new TypeMetadataView(item));
            }
        }
    }
}
