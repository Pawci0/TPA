using DTGBase.Enums;
using System;
using System.Reflection;

namespace Reflection
{
    public static class ExtensionMethods
    {

        public static bool GetVisible(this Type type)
        {
            return type.IsPublic || type.IsNestedPublic || type.IsNestedFamily || type.IsNestedFamANDAssem;
        }
        public static bool GetVisible(this MethodBase method)
        {
            return method != null && (method.IsPublic || method.IsFamily || method.IsFamilyAndAssembly);
        }
        public static string GetNamespace(this Type type)
        {
            string ns = type.Namespace;
            return ns != null ? ns : string.Empty;
        }

        internal static Enums.AbstractEnum ToLogicEnum(this AbstractEnum baseEnum)
        {
            switch (baseEnum)
            {
                case AbstractEnum.Abstract:
                    return Enums.AbstractEnum.Abstract;

                case AbstractEnum.NotAbstract:
                    return Enums.AbstractEnum.NotAbstract;
                default:
                    throw new Exception();
            }
        }

        internal static Enums.AccessLevelEnum ToLogicEnum(this AccessLevelEnum baseEnum)
        {
            switch (baseEnum)
            {
                case AccessLevelEnum.IsPrivate:
                    return Enums.AccessLevelEnum.IsPrivate;

                case AccessLevelEnum.IsProtected:
                    return Enums.AccessLevelEnum.IsProtected;

                case AccessLevelEnum.IsProtectedInternal:
                    return Enums.AccessLevelEnum.IsProtectedInternal;

                case AccessLevelEnum.IsPublic:
                    return Enums.AccessLevelEnum.IsPublic;

                default:
                    throw new Exception();
            }
        }

        internal static Enums.SealedEnum ToLogicEnum(this SealedEnum baseEnum)
        {
            switch (baseEnum)
            {
                case SealedEnum.NotSealed:
                    return Enums.SealedEnum.NotSealed;

                case SealedEnum.Sealed:
                    return Enums.SealedEnum.Sealed;
                default:
                    throw new Exception();
            }
        }

        internal static Enums.StaticEnum ToLogicEnum(this StaticEnum baseEnum)
        {
            switch (baseEnum)
            {
                case StaticEnum.Static:
                    return Enums.StaticEnum.Static;

                case StaticEnum.NotStatic:
                    return Enums.StaticEnum.NotStatic;
                default:
                    throw new Exception();
            }
        }

        internal static Enums.TypeKindEnum ToLogicEnum(this TypeKindEnum baseEnum)
        {
            switch (baseEnum)
            {
                case TypeKindEnum.ClassType:
                    return Enums.TypeKindEnum.ClassType;
                case TypeKindEnum.EnumType:
                    return Enums.TypeKindEnum.EnumType;
                case TypeKindEnum.InterfaceType:
                    return Enums.TypeKindEnum.InterfaceType;
                case TypeKindEnum.StructType:
                    return Enums.TypeKindEnum.StructType;

                default:
                    throw new Exception();
            }
        }
        internal static Enums.VirtualEnum ToLogicEnum(this VirtualEnum baseEnum)
        {
            switch (baseEnum)
            {
                case VirtualEnum.NotVirtual:
                    return Enums.VirtualEnum.NotVirtual;

                case VirtualEnum.Virtual:
                    return Enums.VirtualEnum.Virtual;
                default:
                    throw new Exception();
            }
        }
    }
}
