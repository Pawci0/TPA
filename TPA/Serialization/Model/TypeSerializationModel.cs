using DTGBase;
using DTGBase.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace XmlSerialization.Model
{
    [DataContract(Name = "TypeSerializationModel", IsReference = true)]
    public class TypeSerializationModel
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string NamespaceName { get; set; }
        [DataMember]
        public TypeSerializationModel BaseType { get; set; }
        [DataMember]
        public IEnumerable<TypeSerializationModel> GenericArguments { get; set; }
        [DataMember]
        public Tuple<AccessLevelEnum, SealedEnum, AbstractEnum> Modifiers;
        [DataMember]
        public TypeKindEnum TypeKind { get; set; }
        [DataMember]
        public IEnumerable<TypeSerializationModel> ImplementedInterfaces { get; set; }
        [DataMember]
        public IEnumerable<TypeSerializationModel> NestedTypes { get; set; }
        [DataMember]
        public IEnumerable<PropertySerializationModel> Properties { get; set; }
        [DataMember]
        public TypeSerializationModel DeclaringType { get; set; }
        [DataMember]
        public IEnumerable<MethodSerializationModel> Methods { get; set; }
        [DataMember]
        public IEnumerable<MethodSerializationModel> Constructors { get; set; }
        [DataMember]
        public IEnumerable<ParameterSerializationModel> Fields { get; set; }

        public static Dictionary<string, TypeSerializationModel> storedTypes = new Dictionary<string, TypeSerializationModel>();
        private TypeSerializationModel()
        {

        }

        private TypeSerializationModel(TypeBase baseType)
        {
            if (!storedTypes.ContainsKey(baseType.typeName))
            {
                storedTypes.Add(baseType.typeName, this);
            }
            Name = baseType.typeName;
            NamespaceName = baseType.namespaceName;
            TypeKind = baseType.typeKind;

            BaseType = GetOrAdd(baseType.baseType);
            DeclaringType = GetOrAdd(baseType.declaringType);

            Modifiers = new Tuple<AccessLevelEnum, SealedEnum, AbstractEnum>(
                baseType.modifiers.Item1,
                baseType.modifiers.Item2,
                baseType.modifiers.Item3);

            Constructors = baseType.constructors?.Select(t => new MethodSerializationModel(t));
            Fields = baseType.fields?.Select(t => new ParameterSerializationModel(t));
            GenericArguments = baseType.genericArguments?.Select(GetOrAdd);
            ImplementedInterfaces = baseType.implementedInterfaces?.Select(GetOrAdd);
            Methods = baseType.methods?.Select(t => new MethodSerializationModel(t));
            NestedTypes = baseType.nestedTypes?.Select(GetOrAdd);
            Properties = baseType.properties?.Select(t => new PropertySerializationModel(t));

        }

        public static TypeSerializationModel GetOrAdd(TypeBase baseType)
        {
            if (baseType != null)
            {
                if (storedTypes.ContainsKey(baseType.typeName))
                {
                    return storedTypes[baseType.typeName];
                }
                else
                {
                    return new TypeSerializationModel(baseType);
                }
            }
            else
                return null;
        }


    }
}
