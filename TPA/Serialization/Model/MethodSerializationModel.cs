﻿using DTGBase;
using DTGBase.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace XmlSerialization.Model
{
    [DataContract(Name = "MethodSerializationModel", IsReference = true)]
    public class MethodSerializationModel
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public IEnumerable<TypeSerializationModel> GenericArguments { get; set; }
        [DataMember]
        public Tuple<AccessLevelEnum, AbstractEnum, StaticEnum, VirtualEnum> Modifiers;
        [DataMember]
        public TypeSerializationModel ReturnType { get; set; }
        [DataMember]
        public bool Extension { get; set; }
        [DataMember]
        public IEnumerable<ParameterSerializationModel> Parameters { get; set; }

        private MethodSerializationModel()
        {
        }

        public MethodSerializationModel(MethodBase baseMethod)
        {
            Name = baseMethod.name;
            Extension = baseMethod.extension;
            ReturnType = TypeSerializationModel.GetOrAdd(baseMethod.returnType);
            Modifiers = new Tuple<AccessLevelEnum, AbstractEnum, StaticEnum, VirtualEnum>(
                baseMethod.modifiers.Item1,
                baseMethod.modifiers.Item2,
                baseMethod.modifiers.Item3,
                baseMethod.modifiers.Item4);

            GenericArguments = baseMethod.genericArguments?.Select(t => TypeSerializationModel.GetOrAdd(t));
            Parameters = baseMethod.parameters?.Select(t => new ParameterSerializationModel(t));
        }


    }
}
