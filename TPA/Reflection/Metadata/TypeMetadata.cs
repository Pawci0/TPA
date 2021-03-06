﻿using DTGBase;
using Reflection.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Reflection.Metadata
{
    [DataContract(IsReference = true)]
    //[KnownType("FindAllDerivedTypes")]
    public class TypeMetadata : BaseMetadata
    {
        #region Fields
        [DataMember]
        public static Dictionary<string, TypeMetadata> storedTypes = new Dictionary<string, TypeMetadata>();
        [DataMember]
        public string m_typeName;
        [DataMember]
        public string m_NamespaceName;
        [DataMember]
        public TypeMetadata m_BaseType;
        [DataMember]
        public IEnumerable<TypeMetadata> m_GenericArguments;
        [DataMember]
        public Tuple<AccessLevelEnum, SealedEnum, AbstractEnum> m_Modifiers;
        [DataMember]
        public TypeKindEnum m_TypeKind;
        //[DataMember]
        public IEnumerable<Attribute> m_Attributes;
        [DataMember]
        public IEnumerable<TypeMetadata> m_ImplementedInterfaces;
        [DataMember]
        public IEnumerable<TypeMetadata> m_NestedTypes;
        [DataMember]
        public IEnumerable<PropertyMetadata> m_Properties;
        [DataMember]
        public TypeMetadata m_DeclaringType;
        [DataMember]
        public IEnumerable<MethodMetadata> m_Methods;
        [DataMember]
        public IEnumerable<MethodMetadata> m_Constructors;
        [DataMember]
        public IEnumerable<ParameterMetadata> m_Fields;
        #endregion

        #region constructors
        public TypeMetadata(Type type)
        {

            if (!storedTypes.ContainsKey(type.Name))
            {
                storedTypes.Add(type.Name, this);
            }

            m_NamespaceName = type.Namespace;
            m_typeName = type.Name;
            m_DeclaringType = EmitDeclaringType(type.DeclaringType);
            m_Constructors = MethodMetadata.EmitMethods(type.GetConstructors());
            m_Methods = MethodMetadata.EmitMethods(type.GetMethods());
            m_NestedTypes = EmitNestedTypes(type.GetNestedTypes());
            m_ImplementedInterfaces = EmitImplements(type.GetInterfaces());
            m_GenericArguments = !type.IsGenericTypeDefinition ? null : TypeMetadata.EmitGenericArguments(type.GetGenericArguments());
            m_Modifiers = EmitModifiers(type);
            m_BaseType = EmitExtends(type.BaseType);
            m_Properties = PropertyMetadata.EmitProperties(type.GetProperties());
            m_TypeKind = GetTypeKind(type);
            m_Attributes = type.GetCustomAttributes(false).Cast<Attribute>();
            m_Fields = EmitFields(type.GetFields());
        }

        private TypeMetadata(TypeBase baseType)
        {
            if (!storedTypes.ContainsKey(baseType.typeName))
            {
                storedTypes.Add(baseType.typeName, this);
            }
            m_typeName = baseType.typeName;
            m_NamespaceName = baseType.namespaceName;

            m_TypeKind = baseType.typeKind.ToLogicEnum();

            m_BaseType = GetOrAdd(baseType.baseType);
            m_DeclaringType = GetOrAdd(baseType.declaringType);

            m_Modifiers = new Tuple<AccessLevelEnum, SealedEnum, AbstractEnum>(
                baseType.modifiers.Item1.ToLogicEnum(),
                baseType.modifiers.Item2.ToLogicEnum(),
                baseType.modifiers.Item3.ToLogicEnum());

            m_Constructors = baseType.constructors?.Select(c => new MethodMetadata(c));
            m_Fields = baseType.fields?.Select(t => new ParameterMetadata(t));
            m_GenericArguments = baseType.genericArguments?.Select(GetOrAdd);
            m_ImplementedInterfaces = baseType.implementedInterfaces?.Select(GetOrAdd);
            m_Methods = baseType.methods?.Select(t => new MethodMetadata(t));
            m_NestedTypes = baseType.nestedTypes?.Select(GetOrAdd);
            m_Properties = baseType.properties?.Select(t => new PropertyMetadata(t));
        }
        #endregion

        #region API
        public static TypeMetadata EmitReference(Type type)
        {
            if (!storedTypes.ContainsKey(type.Name))
            {
                AddToStoredTypes(type);
            }
            return storedTypes[type.Name];
        }

        public static IEnumerable<TypeMetadata> EmitGenericArguments(IEnumerable<Type> arguments)
        {
            AddToStoredTypes(arguments);
            return from Type _argument in arguments
                   select EmitReference(_argument);
        }
        #endregion

        #region privateMethods
        private TypeMetadata EmitDeclaringType(Type declaringType)
        {
            if (declaringType == null)
                return null;
            AddToStoredTypes(declaringType);
            return EmitReference(declaringType);
        }

        private IEnumerable<TypeMetadata> EmitNestedTypes(IEnumerable<Type> nestedTypes)
        {
            AddToStoredTypes(nestedTypes);
            return from _type in nestedTypes
                   where _type.GetVisible()
                   select new TypeMetadata(_type);
        }

        private IEnumerable<TypeMetadata> EmitImplements(IEnumerable<Type> interfaces)
        {
            AddToStoredTypes(interfaces);
            return from currentInterface in interfaces
                   select EmitReference(currentInterface);
        }

        private static TypeKindEnum GetTypeKind(Type type)
        {
            if (type.IsEnum)
                return TypeKindEnum.EnumType;
            else if (type.IsValueType)
                return TypeKindEnum.StructType;
            else if (type.IsInterface)
                return TypeKindEnum.InterfaceType;
            else
                return TypeKindEnum.ClassType;
        }

        static Tuple<AccessLevelEnum, SealedEnum, AbstractEnum> EmitModifiers(Type type)
        {
            AccessLevelEnum _access = AccessLevelEnum.IsPrivate;
            if (type.IsPublic)
                _access = AccessLevelEnum.IsPublic;
            else if (type.IsNestedPublic)
                _access = AccessLevelEnum.IsPublic;
            else if (type.IsNestedFamily)
                _access = AccessLevelEnum.IsProtected;
            else if (type.IsNestedFamANDAssem)
                _access = AccessLevelEnum.IsProtectedInternal;

            SealedEnum _sealed = SealedEnum.NotSealed;
            if (type.IsSealed)
                _sealed = SealedEnum.Sealed;

            AbstractEnum _abstract = AbstractEnum.NotAbstract;
            if (type.IsAbstract)
                _abstract = AbstractEnum.Abstract;

            return new Tuple<AccessLevelEnum, SealedEnum, AbstractEnum>(_access, _sealed, _abstract);
        }

        private static TypeMetadata EmitExtends(Type baseType)
        {
            if (baseType == null || baseType == typeof(Object) || baseType == typeof(ValueType) || baseType == typeof(Enum))
                return null;
            AddToStoredTypes(baseType);
            return EmitReference(baseType);
        }

        private static List<ParameterMetadata> EmitFields(IEnumerable<FieldInfo> fieldInfo)
        {
            List<ParameterMetadata> parameters = new List<ParameterMetadata>();
            foreach (var field in fieldInfo)
            {
                AddToStoredTypes(field.FieldType);
                parameters.Add(new ParameterMetadata(field.Name, EmitReference(field.FieldType)));
            }

            return parameters;
        }

        internal static void AddToStoredTypes(Type type)
        {
            if (!storedTypes.ContainsKey(type.Name))
            {
                new TypeMetadata(type);
            }
        }

        internal static void AddToStoredTypes(IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                AddToStoredTypes(type);
            }
        }
        #endregion

        public static Type[] FindAllDerivedTypes()
        {
            var ret = FindAllDerivedTypes<Attribute>(Assembly.GetAssembly(typeof(Attribute)));
            return ret;
        }

        public static Type[] FindAllDerivedTypes<T>(Assembly assembly)
        {
            var derivedType = typeof(T);
            return assembly
                .GetTypes()
                .Where(t =>
                    t != derivedType &&
                    derivedType.IsAssignableFrom(t)
                    ).ToArray();

        }

        public static TypeMetadata GetOrAdd(TypeBase baseType)
        {
            if (baseType != null)
            {
                if (storedTypes.ContainsKey(baseType.typeName))
                {
                    return storedTypes[baseType.typeName];
                }
                else
                {
                    return new TypeMetadata(baseType);
                }
            }
            else
                return null;
        }

    }
}