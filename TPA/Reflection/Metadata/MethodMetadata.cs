﻿using Reflection.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Reflection.Metadata
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(ExtensionAttribute))]
    public class MethodMetadata : BaseMetadata
    {
        #region Fields
        [DataMember]
        public string m_Name;
        [DataMember]
        public IEnumerable<TypeMetadata> m_GenericArguments;
        [DataMember]
        public Tuple<AccessLevelEnum, AbstractEnum, StaticEnum, VirtualEnum> m_Modifiers;
        [DataMember]
        public TypeMetadata m_ReturnType;
        [DataMember]
        public bool m_Extension;
        [DataMember]
        public IEnumerable<ParameterMetadata> m_Parameters;
        #endregion

        public static IEnumerable<MethodMetadata> EmitMethods(IEnumerable<MethodBase> methods)
        {
            return from MethodBase _currentMethod in methods
                   where _currentMethod.GetVisible()
                   select new MethodMetadata(_currentMethod);
        }

        #region privateMethods
        private MethodMetadata(MethodBase method)
        {
            m_Name = method.Name;
            m_GenericArguments = method.IsGenericMethodDefinition ? TypeMetadata.EmitGenericArguments(method.GetGenericArguments()) : null;
            m_ReturnType = EmitReturnType(method);
            m_Parameters = EmitParameters(method.GetParameters());
            m_Modifiers = EmitModifiers(method);
            m_Extension = EmitExtension(method);
        }

        public MethodMetadata(DTGBase.MethodBase baseMethod)
        {
            m_Name = baseMethod.name;
            m_GenericArguments = baseMethod.genericArguments?.Select(TypeMetadata.GetOrAdd);
            m_ReturnType = TypeMetadata.GetOrAdd(baseMethod.returnType);
            m_Parameters = baseMethod.parameters?.Select(t => new ParameterMetadata(t)).ToList();
            m_Modifiers = new Tuple<AccessLevelEnum, AbstractEnum, StaticEnum, VirtualEnum>(
                baseMethod.modifiers.Item1.ToLogicEnum(),
                baseMethod.modifiers.Item2.ToLogicEnum(),
                baseMethod.modifiers.Item3.ToLogicEnum(),
                baseMethod.modifiers.Item4.ToLogicEnum());
            m_Extension = baseMethod.extension;
        }

        private static IEnumerable<ParameterMetadata> EmitParameters(IEnumerable<ParameterInfo> parms)
        {
            return from parm in parms
                   select new ParameterMetadata(parm.Name, TypeMetadata.EmitReference(parm.ParameterType));
        }

        private static TypeMetadata EmitReturnType(MethodBase method)
        {
            MethodInfo methodInfo = method as MethodInfo;
            if (methodInfo == null)
                return null;
            TypeMetadata.AddToStoredTypes(methodInfo.ReturnType);
            return TypeMetadata.EmitReference(methodInfo.ReturnType);
        }

        private static bool EmitExtension(MethodBase method)
        {
            return method.IsDefined(typeof(ExtensionAttribute), true);
        }

        private static Tuple<AccessLevelEnum, AbstractEnum, StaticEnum, VirtualEnum> EmitModifiers(MethodBase method)
        {
            AccessLevelEnum _access = AccessLevelEnum.IsPrivate;
            if (method.IsPublic)
                _access = AccessLevelEnum.IsPublic;
            else if (method.IsFamily)
                _access = AccessLevelEnum.IsProtected;
            else if (method.IsFamilyAndAssembly)
                _access = AccessLevelEnum.IsProtectedInternal;

            AbstractEnum _abstract = AbstractEnum.NotAbstract;
            if (method.IsAbstract)
                _abstract = AbstractEnum.Abstract;
            StaticEnum _static = StaticEnum.NotStatic;
            if (method.IsStatic)
                _static = StaticEnum.Static;

            VirtualEnum _virtual = VirtualEnum.NotVirtual;
            if (method.IsVirtual)
                _virtual = VirtualEnum.Virtual;

            return new Tuple<AccessLevelEnum, AbstractEnum, StaticEnum, VirtualEnum>(_access, _abstract, _static, _virtual);
        }
        #endregion

    }
}