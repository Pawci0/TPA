using Reflection.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reflection.Metadata
{
    public class TypeMetadata
    {
        #region Fields
        public static Dictionary<string, TypeMetadata> storedTypes = new Dictionary<string, TypeMetadata>();

        public string m_typeName;
        public string m_NamespaceName;
        public TypeMetadata m_BaseType;
        public IEnumerable<TypeMetadata> m_GenericArguments;
        public Tuple<AccessLevelEnum, SealedEnum, AbstractEnum> m_Modifiers;
        public TypeKind m_TypeKind;
        public IEnumerable<Attribute> m_Attributes;
        public IEnumerable<TypeMetadata> m_ImplementedInterfaces;
        public IEnumerable<TypeMetadata> m_NestedTypes;
        public IEnumerable<PropertyMetadata> m_Properties;
        public TypeMetadata m_DeclaringType;
        public IEnumerable<MethodMetadata> m_Methods;
        public IEnumerable<MethodMetadata> m_Constructors;
        public IEnumerable<ParameterMetadata> m_Fields;
        #endregion

        #region constructors
        public TypeMetadata(Type type)
        {

            if (!storedTypes.ContainsKey(type.Name))
            {
                storedTypes.Add(type.Name, this);
            }

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
        #endregion

        #region API
        public enum TypeKind
        {
            EnumType, StructType, InterfaceType, ClassType
        }

        public static TypeMetadata EmitReference(Type type)
        {
            if (!type.IsGenericType)
            {
                if (storedTypes.ContainsKey(type.Name))
                {
                    return storedTypes[type.Name];
                }
                return new TypeMetadata(type.Name, type.GetNamespace());
            }
            else
                return new TypeMetadata(type.Name, type.GetNamespace(), EmitGenericArguments(type.GetGenericArguments()));
        }

        public static IEnumerable<TypeMetadata> EmitGenericArguments(IEnumerable<Type> arguments)
        {
            AddToStoredTypes(arguments);
            return from Type _argument in arguments
                   select EmitReference(_argument);
        }
        #endregion

        #region privateMethods
        //constructors
        private TypeMetadata(string typeName, string namespaceName)
        {
            m_typeName = typeName;
            m_NamespaceName = namespaceName;
        }

        private TypeMetadata(string typeName, string namespaceName, IEnumerable<TypeMetadata> genericArguments) : this(typeName, namespaceName)
        {
            m_GenericArguments = genericArguments;
        }

        //methods
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

        private static TypeKind GetTypeKind(Type type)
        {
            if (type.IsEnum)
                return TypeKind.EnumType;
            else if (type.IsValueType)
                return TypeKind.StructType;
            else if (type.IsInterface)
                return TypeKind.InterfaceType;
            else
                return TypeKind.ClassType;
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
                // TypeMetadata object is added to dictionary when invoking its constructor
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

    }
}