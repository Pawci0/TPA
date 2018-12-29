using DTGBase.Enums;
using System;
using System.Collections.Generic;

namespace DTGBase
{
    public class TypeBase
    {
        public string typeName;    
        public string namespaceName;
        public TypeBase baseType;
        public IEnumerable<TypeBase> genericArguments;
        public Tuple<AccessLevelEnum, SealedEnum, AbstractEnum> modifiers;
        public TypeKindEnum typeKind;
        public IEnumerable<Attribute> attributes;
        public IEnumerable<TypeBase> implementedInterfaces;
        public IEnumerable<TypeBase> nestedTypes;
        public IEnumerable<PropertyBase> properties;
        public TypeBase declaringType;
        public IEnumerable<MethodBase> methods;
        public IEnumerable<MethodBase> constructors;
        public IEnumerable<ParameterBase> fields;

    }
}