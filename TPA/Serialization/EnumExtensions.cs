using DTGBase.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    public static class EnumExtensions
    {
        internal static AbstractEnum ToBaseEnum(this Reflection.Enums.AbstractEnum baseEnum)
        {
            switch (baseEnum)
            {
                case Reflection.Enums.AbstractEnum.Abstract:
                    return AbstractEnum.Abstract;

                case Reflection.Enums.AbstractEnum.NotAbstract:
                    return AbstractEnum.NotAbstract;
                default:
                    throw new Exception();
            }
        }

        internal static AccessLevelEnum ToBaseEnum(this Reflection.Enums.AccessLevelEnum baseEnum)
        {
            switch (baseEnum)
            {
                case Reflection.Enums.AccessLevelEnum.IsPrivate:
                    return AccessLevelEnum.IsPrivate;

                case Reflection.Enums.AccessLevelEnum.IsProtected:
                    return AccessLevelEnum.IsProtected;

                case Reflection.Enums.AccessLevelEnum.IsProtectedInternal:
                    return AccessLevelEnum.IsProtectedInternal;

                case Reflection.Enums.AccessLevelEnum.IsPublic:
                    return AccessLevelEnum.IsPublic;

                default:
                    throw new Exception();
            }
        }

        internal static SealedEnum ToBaseEnum(this Reflection.Enums.SealedEnum baseEnum)
        {
            switch (baseEnum)
            {
                case Reflection.Enums.SealedEnum.NotSealed:
                    return SealedEnum.NotSealed;

                case Reflection.Enums.SealedEnum.Sealed:
                    return SealedEnum.Sealed;
                default:
                    throw new Exception();
            }
        }

        internal static StaticEnum ToBaseEnum(this Reflection.Enums.StaticEnum baseEnum)
        {
            switch (baseEnum)
            {
                case Reflection.Enums.StaticEnum.Static:
                    return StaticEnum.Static;

                case Reflection.Enums.StaticEnum.NotStatic:
                    return StaticEnum.NotStatic;
                default:
                    throw new Exception();
            }
        }

        internal static TypeKindEnum ToBaseEnum(this Reflection.Enums.TypeKindEnum baseEnum)
        {
            switch (baseEnum)
            {
                case Reflection.Enums.TypeKindEnum.ClassType:
                    return TypeKindEnum.ClassType;
                case Reflection.Enums.TypeKindEnum.EnumType:
                    return TypeKindEnum.EnumType;
                case Reflection.Enums.TypeKindEnum.InterfaceType:
                    return TypeKindEnum.InterfaceType;
                case Reflection.Enums.TypeKindEnum.StructType:
                    return TypeKindEnum.StructType;

                default:
                    throw new Exception();
            }
        }
        internal static VirtualEnum ToBaseEnum(this Reflection.Enums.VirtualEnum baseEnum)
        {
            switch (baseEnum)
            {
                case Reflection.Enums.VirtualEnum.NotVirtual:
                    return VirtualEnum.NotVirtual;

                case Reflection.Enums.VirtualEnum.Virtual:
                    return VirtualEnum.Virtual;
                default:
                    throw new Exception();
            }
        }
    }
}
