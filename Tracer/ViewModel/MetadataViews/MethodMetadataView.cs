using Reflection.Metadata;
using System.Collections.ObjectModel;
using System.Linq;
using static ViewModel.MetadataViews.KeywordUtils;


namespace ViewModel.MetadataViews
{
    class MethodMetadataView : BaseMetadataView, IExpander
    {
        private MethodMetadata Method { get; set; }

        public MethodMetadataView(MethodMetadata methodMetadata) : base(methodMetadata.m_Name)
        {
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

        public void Expand(ObservableCollection<TreeViewItem> children)
        {
            if (Method.m_GenericArguments != null) Add(Method.m_GenericArguments, children);
            if (Method.m_Parameters != null) Add(Method.m_Parameters, children);
            if (Method.m_ReturnType != null) Add(Method.m_ReturnType, children);
        }
    }
}
