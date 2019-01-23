using Reflection.Metadata;
using System.Collections.Generic;
using static ViewModel.MetadataViews.KeywordUtils;

namespace ViewModel.MetadataViews
{
    public class TypeMetadataView : BaseMetadataView
    {
        public TypeMetadata Type;

        public TypeMetadataView(TypeMetadata typeMetadata)
        {
            Name = typeMetadata.m_typeName;
            Type = typeMetadata;
        }

        public override void Expand()
        {
            if (Type.m_BaseType != null)
            {
                Children.Add(new TypeMetadataView(Type.m_BaseType));
            }
            if (Type.m_DeclaringType != null)
            {
                Children.Add(new TypeMetadataView(Type.m_DeclaringType));
            }
            if (Type.m_Properties != null)
            {
                Add(Type.m_Properties, i => new PropertyMetadataView(i));
            }
            if (Type.m_Fields != null)
            {
                Add(Type.m_Fields, i => new ParameterMetadataView(i));
            }
            if (Type.m_GenericArguments != null)
            {
                Add(Type.m_GenericArguments, i => new TypeMetadataView(i));
            }
            if (Type.m_ImplementedInterfaces != null)
            {
                Add(Type.m_ImplementedInterfaces, i => new TypeMetadataView(i));
            }
            if (Type.m_NestedTypes != null)
            {
                Add(Type.m_NestedTypes, i => new TypeMetadataView(i));
            }
            if (Type.m_Methods != null)
            {
                Add(Type.m_Methods, i => new MethodMetadataView(i));
            }
            if (Type.m_Constructors != null)
            {
                Add(Type.m_Constructors, i => new MethodMetadataView(i));
            }
        }

        public override string ToString()
        {
            string str = "";

            if (Type.m_Modifiers != null)
            {
                str += AccessLevelToString(Type.m_Modifiers.Item1);
                
                str = str.Trim();
                str += " " + AbstractToString(Type.m_Modifiers.Item3);
                
                str = str.Trim();
                str += " " + SealedToString(Type.m_Modifiers.Item2);
            }
            

            str = str.Trim();

            str += " " + TypeKindToString(Type.m_TypeKind);

            str = str.Trim();
            str += " " + Type.m_typeName;

            if (Type.m_GenericArguments != null)
            {
                str += "<";

                foreach (var genericArgument in Type.m_GenericArguments)
                {
                    str += genericArgument.m_GenericArguments + ", ";
                }

                str = str.TrimEnd(new char[] { ',', ' ' });
                str += ">";
            }

            return str;
        }

        public void GetInternalTypes(Dictionary<string, TypeMetadataView> expandableTypes)
        {
            foreach(var item in Type.m_Fields)
            {
                string typeName = item.m_TypeMetadata.m_typeName;
                if (!expandableTypes.ContainsKey(typeName))
                {
                    expandableTypes.Add(typeName, new TypeMetadataView(item.m_TypeMetadata));
                }
            }

            foreach (var item in Type.m_Properties)
            {
                string typeName = item.m_TypeMetadata.m_typeName;
                if (!expandableTypes.ContainsKey(typeName))
                {
                    expandableTypes.Add(typeName, new TypeMetadataView(item.m_TypeMetadata));
                }
            }

            foreach (var item in Type.m_Methods)
            {
                string typeName = item.m_ReturnType.m_typeName;
                if (!expandableTypes.ContainsKey(typeName))
                {
                    expandableTypes.Add(typeName, new TypeMetadataView(item.m_ReturnType));
                }

                foreach (var parameter in item.m_Parameters)
                {
                    typeName = parameter.m_TypeMetadata.m_typeName;
                    if (!expandableTypes.ContainsKey(typeName))
                    {
                        expandableTypes.Add(typeName, new TypeMetadataView(parameter.m_TypeMetadata));
                    }
                }
            }
        }
    }
}
