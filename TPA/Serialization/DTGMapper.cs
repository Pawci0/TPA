using DTGBase;
using System;
using System.Collections.Generic;
using System.Linq;
using XmlSerialization.Model;

namespace Serialization
{
    public static class DTGMapper
    {
        private static Dictionary<string, TypeBase> typeDictonary = new Dictionary<string, TypeBase>();

        #region Metadata
        public static AssemblyBase ToBase(AssemblySerializationModel metadata)
        {
            typeDictonary.Clear();
            return new AssemblyBase()
            {
                name = metadata.Name,
                namespaces = metadata.Namespaces?.Select(ToBase)
            };
        }

        private static NamespaceBase ToBase(NamespaceSerializationModel metadata)
        {
            return new NamespaceBase()
            {
                name = metadata.Name,
                types = metadata.Types?.Select(ToBase)
            };
        }

        private static TypeBase ToBase(TypeSerializationModel metadata)
        {
            if (metadata == null)
            {
                return null;
            }

            if (typeDictonary.ContainsKey(metadata.Name))
            {
                return typeDictonary[metadata.Name];
            }

            TypeBase type = new TypeBase()
            {
                typeName = metadata.Name,
                namespaceName = metadata.NamespaceName,
                typeKind = metadata.TypeKind,
                baseType = ToBase(metadata.BaseType),
                declaringType = ToBase(metadata.DeclaringType),
                modifiers = new Tuple<DTGBase.Enums.AccessLevelEnum,
                                      DTGBase.Enums.SealedEnum,
                                      DTGBase.Enums.AbstractEnum>(metadata.Modifiers.Item1,
                                                                  metadata.Modifiers.Item2,
                                                                  metadata.Modifiers.Item3),
                constructors = metadata.Constructors?.Select(ToBase),
                fields = metadata.Fields?.Select(ToBase),
                genericArguments = metadata.GenericArguments?.Select(ToBase),
                implementedInterfaces = metadata.ImplementedInterfaces?.Select(ToBase),
                methods = metadata.Methods?.Select(ToBase),
                nestedTypes = metadata.NestedTypes?.Select(ToBase),
                properties = metadata.Properties?.Select(ToBase)
            };

            typeDictonary.Add(type.typeName, type);

            return type;
        }

        private static PropertyBase ToBase(PropertySerializationModel metadata)
        {
            return new PropertyBase()
            {
                name = metadata.Name,
                typeMetadata = ToBase(metadata.Type)
            };
        }

        private static ParameterBase ToBase(ParameterSerializationModel metadata)
        {
            return new ParameterBase()
            {
                name = metadata.Name,
                typeMetadata = ToBase(metadata.Type)
            };
        }

        private static MethodBase ToBase(MethodSerializationModel metadata)
        {
            return new MethodBase()
            {
                name = metadata.Name,
                returnType = ToBase(metadata.ReturnType),
                parameters = metadata.Parameters?.Select(ToBase),
                genericArguments = metadata.GenericArguments?.Select(ToBase),
                modifiers = new Tuple<DTGBase.Enums.AccessLevelEnum,
                                      DTGBase.Enums.AbstractEnum,
                                      DTGBase.Enums.StaticEnum,
                                      DTGBase.Enums.VirtualEnum>(metadata.Modifiers.Item1,
                                                                 metadata.Modifiers.Item2,
                                                                 metadata.Modifiers.Item3,
                                                                 metadata.Modifiers.Item4),
                extension = metadata.Extension
            };
        }
        #endregion
    }
}
