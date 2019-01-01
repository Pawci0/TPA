using DTGBase;
using Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reflection.Mappers
{
    public static class DTGMapper
    {
        private static Dictionary<string, TypeBase> typeDictonary = new Dictionary<string, TypeBase>();

        #region Enum
        public static DTGBase.Enums.AbstractEnum ToBase(Enums.AbstractEnum _enum)
        {
            switch (_enum)
            {
                case Enums.AbstractEnum.Abstract:
                    return DTGBase.Enums.AbstractEnum.Abstract;
                case Enums.AbstractEnum.NotAbstract:
                    return DTGBase.Enums.AbstractEnum.NotAbstract;
                default:
                    throw new Exception();
            }
        }

        public static DTGBase.Enums.AccessLevelEnum ToBase(Enums.AccessLevelEnum _enum)
        {
            switch (_enum)
            {
                case Enums.AccessLevelEnum.IsPrivate:
                    return DTGBase.Enums.AccessLevelEnum.IsPrivate;
                case Enums.AccessLevelEnum.IsProtected:
                    return DTGBase.Enums.AccessLevelEnum.IsProtected;
                case Enums.AccessLevelEnum.IsProtectedInternal:
                    return DTGBase.Enums.AccessLevelEnum.IsProtectedInternal;
                case Enums.AccessLevelEnum.IsPublic:
                    return DTGBase.Enums.AccessLevelEnum.IsPublic;
                default:
                    throw new Exception();
            }
        }

        public static DTGBase.Enums.SealedEnum ToBase(Enums.SealedEnum _enum)
        {
            switch (_enum)
            {
                case Enums.SealedEnum.Sealed:
                    return DTGBase.Enums.SealedEnum.Sealed;
                case Enums.SealedEnum.NotSealed:
                    return DTGBase.Enums.SealedEnum.NotSealed;
                default:
                    throw new Exception();
            }
        }

        public static DTGBase.Enums.StaticEnum ToBase(Enums.StaticEnum _enum)
        {
            switch (_enum)
            {
                case Enums.StaticEnum.Static:
                    return DTGBase.Enums.StaticEnum.Static;
                case Enums.StaticEnum.NotStatic:
                    return DTGBase.Enums.StaticEnum.NotStatic;
                default:
                    throw new Exception();
            }
        }

        public static DTGBase.Enums.TypeKindEnum ToBase(Enums.TypeKindEnum _enum)
        {
            switch (_enum)
            {
                case Enums.TypeKindEnum.ClassType:
                    return DTGBase.Enums.TypeKindEnum.ClassType;
                case Enums.TypeKindEnum.EnumType:
                    return DTGBase.Enums.TypeKindEnum.EnumType;
                case Enums.TypeKindEnum.InterfaceType:
                    return DTGBase.Enums.TypeKindEnum.InterfaceType;
                case Enums.TypeKindEnum.StructType:
                    return DTGBase.Enums.TypeKindEnum.StructType;
                default:
                    throw new Exception();
            }
        }

        public static DTGBase.Enums.VirtualEnum ToBase(Enums.VirtualEnum _enum)
        {
            switch (_enum)
            {
                case Enums.VirtualEnum.Virtual:
                    return DTGBase.Enums.VirtualEnum.Virtual;
                case Enums.VirtualEnum.NotVirtual:
                    return DTGBase.Enums.VirtualEnum.NotVirtual;
                default:
                    throw new Exception();
            }
        }
        #endregion

        #region Metadata
        public static AssemblyBase ToBase(AssemblyMetadata metadata)
        {
            typeDictonary.Clear();
            return new AssemblyBase()
            {
                name = metadata.m_Name,
                namespaces = metadata.m_Namespaces?.Select(ToBase)
            };
        }

        private static NamespaceBase ToBase(NamespaceMetadata metadata)
        {
            return new NamespaceBase()
            {
                name = metadata.m_NamespaceName,
                types = metadata.m_Types?.Select(ToBase)
            };
        }

        private static TypeBase ToBase(TypeMetadata metadata)
        {
            if (typeDictonary.ContainsKey(metadata.m_typeName))
            {
                return typeDictonary[metadata.m_typeName];
            }

            TypeBase type = new TypeBase()
            {
                typeName = metadata.m_typeName,
                namespaceName = metadata.m_NamespaceName,
                typeKind = ToBase(metadata.m_TypeKind),
                baseType = ToBase(metadata.m_BaseType),
                declaringType = ToBase(metadata.m_DeclaringType),
                modifiers = new Tuple<DTGBase.Enums.AccessLevelEnum,
                                      DTGBase.Enums.SealedEnum,
                                      DTGBase.Enums.AbstractEnum>(ToBase(metadata.m_Modifiers.Item1),
                                                                  ToBase(metadata.m_Modifiers.Item2),
                                                                  ToBase(metadata.m_Modifiers.Item3)),
                constructors = metadata.m_Constructors?.Select(ToBase),
                fields = metadata.m_Fields?.Select(ToBase),
                genericArguments = metadata.m_GenericArguments?.Select(ToBase),
                implementedInterfaces = metadata.m_ImplementedInterfaces?.Select(ToBase),
                methods = metadata.m_Methods?.Select(ToBase),
                nestedTypes = metadata.m_NestedTypes?.Select(ToBase),
                properties = metadata.m_Properties?.Select(ToBase)
            };

            typeDictonary.Add(type.typeName, type);

            return type;
        }

        private static PropertyBase ToBase(PropertyMetadata metadata)
        {
            return new PropertyBase()
            {
                name = metadata.m_Name,
                typeMetadata = ToBase(metadata.m_TypeMetadata)
            };
        }

        private static ParameterBase ToBase(ParameterMetadata metadata)
        {
            return new ParameterBase()
            {
                name = metadata.m_Name,
                typeMetadata = ToBase(metadata.m_TypeMetadata)
            };
        }

        private static MethodBase ToBase(MethodMetadata metadata)
        {
            return new MethodBase()
            {
                name = metadata.m_Name,
                returnType = ToBase(metadata.m_ReturnType),
                parameters = metadata.m_Parameters.Select(ToBase),
                genericArguments = metadata.m_GenericArguments.Select(ToBase),
                modifiers = new Tuple<DTGBase.Enums.AccessLevelEnum, 
                                      DTGBase.Enums.AbstractEnum, 
                                      DTGBase.Enums.StaticEnum, 
                                      DTGBase.Enums.VirtualEnum>(ToBase(metadata.m_Modifiers.Item1),
                                                                 ToBase(metadata.m_Modifiers.Item2),
                                                                 ToBase(metadata.m_Modifiers.Item3),
                                                                 ToBase(metadata.m_Modifiers.Item4)),
                extension = metadata.m_Extension
            };
        }
    }
        #endregion
}
