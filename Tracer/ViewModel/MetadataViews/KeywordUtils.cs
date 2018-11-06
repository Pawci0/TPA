using Reflection.Enums;

namespace ViewModel.MetadataViews
{
    public class KeywordUtils
    {
        public static string AbstractToString(AbstractEnum abstractEnum)
        {
            return abstractEnum == AbstractEnum.Abstract ? "abstract" : "";
        }

        public static string StaticToString(StaticEnum staticEnum)
        {
            return staticEnum == StaticEnum.Static ? "static" : "";
        }

        public static string SealedToString(SealedEnum sealedEnum)
        {
            return sealedEnum == SealedEnum.Sealed ? "sealed" : "";
        }

        public static string VirtualToString(VirtualEnum virtualEnum)
        {
            return virtualEnum == VirtualEnum.Virtual ? "virtual" : "";
        }

        public static string AccessLevelToString(AccessLevelEnum accessLevelEnum)
        {
            if (accessLevelEnum == AccessLevelEnum.IsPrivate)
                return "private";
            else if (accessLevelEnum == AccessLevelEnum.IsProtected)
                return "protected";
            else if (accessLevelEnum == AccessLevelEnum.IsProtectedInternal)
                return "internal";
            else
                return "public";
        }
    }
}
