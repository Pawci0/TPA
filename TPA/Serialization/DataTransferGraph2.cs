using DTGBase;
using System;
using System.Collections.Generic;
using System.Linq;
using XmlSerialization.Model;

namespace Serialization
{
    public static class DataTransferGraph2
    {
        public static AssemblyBase AssemblyBase(AssemblySerializationModel assemblySerializationModel)
        {
            _typeDictionary = new Dictionary<string, TypeBase>();
            return new AssemblyBase()
            {
                name = assemblySerializationModel.Name,
                namespaces = assemblySerializationModel.Namespaces?.Select(NamespaceBase)
            };
        }

        public static NamespaceBase NamespaceBase(NamespaceSerializationModel namespaceSerializationModel)
        {
            return new NamespaceBase()
            {
                name = namespaceSerializationModel.Name,
                types = namespaceSerializationModel.Types?.Select(GetOrAdd)
            };
        }

        public static TypeBase TypeBase(TypeSerializationModel typeSerializationModel)
        {
            TypeBase typeBase = new TypeBase()
            {
                typeName = typeSerializationModel.Name
            };

            _typeDictionary.Add(typeBase.typeName, typeBase);

            typeBase.namespaceName = typeSerializationModel.NamespaceName;
            typeBase.typeKind = typeSerializationModel.TypeKind;
            typeBase.baseType = GetOrAdd(typeSerializationModel.BaseType);
            typeBase.declaringType = GetOrAdd(typeSerializationModel.DeclaringType);
            typeBase.modifiers = new Tuple<DTGBase.Enums.AccessLevelEnum, DTGBase.Enums.SealedEnum, DTGBase.Enums.AbstractEnum>(
                typeSerializationModel.Modifiers.Item1,
                typeSerializationModel.Modifiers.Item2,
                typeSerializationModel.Modifiers.Item3);
            typeBase.constructors = typeSerializationModel.Constructors?.Select(MethodBase);
            typeBase.fields = typeSerializationModel.Fields?.Select(ParameterBase);
            typeBase.genericArguments = typeSerializationModel.GenericArguments?.Select(GetOrAdd);
            typeBase.implementedInterfaces = typeSerializationModel.ImplementedInterfaces?.Select(GetOrAdd);
            typeBase.methods = typeSerializationModel.Methods?.Select(MethodBase);
            typeBase.nestedTypes = typeSerializationModel.NestedTypes?.Select(GetOrAdd);
            typeBase.properties = typeSerializationModel.Properties?.Select(PropertyBase);

            return typeBase;
        }

        public static MethodBase MethodBase(MethodSerializationModel methodSerializationModel)
        {
            return new MethodBase()
            {
                name = methodSerializationModel.Name,
                modifiers = new Tuple<DTGBase.Enums.AccessLevelEnum, DTGBase.Enums.AbstractEnum, DTGBase.Enums.StaticEnum, DTGBase.Enums.VirtualEnum>(
                    methodSerializationModel.Modifires.Item1,
                    methodSerializationModel.Modifires.Item2,
                    methodSerializationModel.Modifires.Item3,
                    methodSerializationModel.Modifires.Item4),
                extension = methodSerializationModel.Extension,
                returnType = GetOrAdd(methodSerializationModel.ReturnType),
                genericArguments = methodSerializationModel.GenericArguments?.Select(GetOrAdd),
                parameters = methodSerializationModel.Parameters?.Select(ParameterBase)
            };
        }

        public static ParameterBase ParameterBase(ParameterSerializationModel parameterSerializationModel)
        {
            return new ParameterBase()
            {
                name = parameterSerializationModel.Name,
                typeMetadata = GetOrAdd(parameterSerializationModel.Type)
            };
        }

        public static PropertyBase PropertyBase(PropertySerializationModel propertySerializationModel)
        {
            return new PropertyBase()
            {
                name = propertySerializationModel.Name,
                typeMetadata = GetOrAdd(propertySerializationModel.Type)
            };
        }

        public static TypeBase GetOrAdd(TypeSerializationModel baseType)
        {
            if (baseType != null)
            {
                if (_typeDictionary.ContainsKey(baseType.Name))
                {
                    return _typeDictionary[baseType.Name];
                }
                else
                {
                    return TypeBase(baseType);
                }
            }
            else
                return null;
        }

        private static Dictionary<string, TypeBase> _typeDictionary;
    }
}
