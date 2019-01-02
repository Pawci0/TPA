using DTGBase;
using DTGBase.Enums;
using Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    public class DataTransferGraph
    {
        public static AssemblyBase AssemblyBase(AssemblyMetadata assemblyLogicReader)
        {
            _typeDictionary = new Dictionary<string, TypeBase>();
            return new AssemblyBase()
            {
                name = assemblyLogicReader.m_Name,
                namespaces = assemblyLogicReader.m_Namespaces?.Select(NamespaceBase)
            };
        }

        public static NamespaceBase NamespaceBase(NamespaceMetadata namespaceLogicReader)
        {
            return new NamespaceBase()
            {
                name = namespaceLogicReader.m_NamespaceName,
                types = namespaceLogicReader.m_Types?.Select(GetOrAdd)
            };
        }

        public static TypeBase TypeBase(TypeMetadata typeLogicReader)
        {
            TypeBase typeBase = new TypeBase()
            {
                typeName = typeLogicReader.m_typeName
            };

            _typeDictionary.Add(typeBase.typeName, typeBase);

            typeBase.namespaceName = typeLogicReader.m_NamespaceName;
            typeBase.typeKind = typeLogicReader.m_TypeKind.ToBaseEnum();
            typeBase.baseType = GetOrAdd(typeLogicReader.m_BaseType);
            typeBase.declaringType = GetOrAdd(typeLogicReader.m_DeclaringType);

            typeBase.modifiers = new Tuple<AccessLevelEnum, SealedEnum, AbstractEnum>(
                typeLogicReader.m_Modifiers.Item1.ToBaseEnum(), 
                typeLogicReader.m_Modifiers.Item2.ToBaseEnum(), 
                typeLogicReader.m_Modifiers.Item3.ToBaseEnum());

            typeBase.constructors = typeLogicReader.m_Constructors?.Select(MethodBase);
            typeBase.fields = typeLogicReader.m_Fields?.Select(ParameterBase);
            typeBase.genericArguments = typeLogicReader.m_GenericArguments?.Select(GetOrAdd);
            typeBase.implementedInterfaces = typeLogicReader.m_ImplementedInterfaces?.Select(GetOrAdd);
            typeBase.methods = typeLogicReader.m_Methods?.Select(MethodBase);
            typeBase.nestedTypes = typeLogicReader.m_NestedTypes?.Select(GetOrAdd);
            typeBase.properties = typeLogicReader.m_Properties?.Select(PropertyBase);

            return typeBase;
        }

        public static MethodBase MethodBase(MethodMetadata methodLogicReader)
        {
            return new MethodBase()
            {
                name = methodLogicReader.m_Name,
                modifiers = new Tuple<AccessLevelEnum, AbstractEnum, StaticEnum, VirtualEnum>(
                    methodLogicReader.m_Modifiers.Item1.ToBaseEnum(),
                    methodLogicReader.m_Modifiers.Item2.ToBaseEnum(),
                    methodLogicReader.m_Modifiers.Item3.ToBaseEnum(),
                    methodLogicReader.m_Modifiers.Item4.ToBaseEnum()),
                extension = methodLogicReader.m_Extension,
                returnType = GetOrAdd(methodLogicReader.m_ReturnType),
                genericArguments = methodLogicReader.m_GenericArguments?.Select(GetOrAdd),
                parameters = methodLogicReader.m_Parameters?.Select(ParameterBase)
            };
        }

        public static ParameterBase ParameterBase(ParameterMetadata parameterLogicReader)
        {
            return new ParameterBase()
            {
                name = parameterLogicReader.m_Name,
                typeMetadata = GetOrAdd(parameterLogicReader.m_TypeMetadata)
            };
        }

        public static PropertyBase PropertyBase(PropertyMetadata propertyLogicReader)
        {
            return new PropertyBase()
            {
                name = propertyLogicReader.m_Name,
                typeMetadata = GetOrAdd(propertyLogicReader.m_TypeMetadata)
            };
        }

        public static TypeBase GetOrAdd(TypeMetadata baseType)
        {
            if (baseType != null)
            {
                if (_typeDictionary.ContainsKey(baseType.m_typeName))
                {
                    return _typeDictionary[baseType.m_typeName];
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
