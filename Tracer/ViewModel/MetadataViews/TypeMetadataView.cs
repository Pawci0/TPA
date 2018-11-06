using Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ViewModel.MetadataViews.KeywordUtils;

namespace ViewModel.MetadataViews
{
    class TypeMetadataView : BaseMetadataView, IExpander
    {
        private TypeMetadata Type;

        public TypeMetadataView(TypeMetadata typeMetadata) : base(typeMetadata.m_typeName)
        {
            Type = typeMetadata;
        }

        public void Expand(ObservableCollection<TreeViewItem> children)
        {
            if (Type.m_BaseType != null) Add(Type.m_BaseType, children);
            if (Type.m_DeclaringType != null) Add(Type.m_DeclaringType, children);
            if (Type.m_Properties != null) Add(Type.m_Properties, children);
            if (Type.m_Fields != null) Add(Type.m_Fields, children);
            if (Type.m_GenericArguments != null) Add(Type.m_GenericArguments, children);
            if (Type.m_ImplementedInterfaces != null) Add(Type.m_ImplementedInterfaces, children);
            if (Type.m_NestedTypes != null) Add(Type.m_NestedTypes, children);
            if (Type.m_Methods != null) Add(Type.m_Methods, children);
            if (Type.m_Constructors != null) Add(Type.m_Constructors, children);
        }

        public override string ToString()
        {
            string str = "";

            if (Type.m_Modifiers != null)
            {
                // access level
                str += AccessLevelToString(Type.m_Modifiers.Item1);

                // abstract
                str = str.Trim();
                str += " " + AbstractToString(Type.m_Modifiers.Item3);

                // sealed
                str = str.Trim();
                str += " " + SealedToString(Type.m_Modifiers.Item2);
            }

            // generic arguments
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

            str = str.Trim();

            // kind of type
            switch (Type.m_TypeKind)
            {
                case TypeMetadata.TypeKind.ClassType:
                    {
                        str += " class";
                        break;
                    }
                case TypeMetadata.TypeKind.EnumType:
                    {
                        str += " enum";
                        break;
                    }
                case TypeMetadata.TypeKind.InterfaceType:
                    {
                        str += " interface";
                        break;
                    }
                case TypeMetadata.TypeKind.StructType:
                    {
                        str += " struct";
                        break;
                    }
            }

            str = str.Trim();
            str += " " + Type.m_typeName;

            return str;
        }
    }
}
