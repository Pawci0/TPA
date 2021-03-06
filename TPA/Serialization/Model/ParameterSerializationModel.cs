﻿using DTGBase;
using System.Runtime.Serialization;

namespace XmlSerialization.Model
{
    [DataContract(Name = "ParameterSerializationModel", IsReference = true)]
    public class ParameterSerializationModel
    {
        private ParameterSerializationModel()
        {

        }

        public ParameterSerializationModel(ParameterBase baseParameter)
        {
            Name = baseParameter.name;
            Type = TypeSerializationModel.GetOrAdd(baseParameter.typeMetadata);
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public TypeSerializationModel Type { get; set; }

    }
}
