using DTGBase.Enums;
using System;
using System.Collections.Generic;

namespace DTGBase
{
    public class MethodBase
    {
        public string name;
        public IEnumerable<TypeBase> genericArguments;
        public Tuple<AccessLevelEnum, AbstractEnum, StaticEnum, VirtualEnum> modifiers;
        public TypeBase returnType;
        public bool extension;
        public IEnumerable<ParameterBase> parameters;

    }
}