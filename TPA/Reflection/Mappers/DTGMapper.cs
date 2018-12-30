using DTGBase.Enums;
using Reflection.Enums;
using System;

namespace Reflection.Mappers
{
    public static class DTGMapper
    {
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
    }
}
