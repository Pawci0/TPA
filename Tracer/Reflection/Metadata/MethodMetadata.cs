using Reflection.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Reflection.Metadata
{
    internal class MethodMetadata
    {
        #region privateFields
        internal string m_Name;
        internal IEnumerable<TypeMetadata> m_GenericArguments;
        internal Tuple<AccessLevelEnum, AbstractEnum, StaticEnum, VirtualEnum> m_Modifiers;
        internal TypeMetadata m_ReturnType;
        internal bool m_Extension;
        internal IEnumerable<ParameterMetadata> m_Parameters;
        #endregion

        internal static IEnumerable<MethodMetadata> EmitMethods(IEnumerable<MethodBase> methods)
        {
            return from MethodBase _currentMethod in methods
                   where _currentMethod.GetVisible()
                   select new MethodMetadata(_currentMethod);
        }

        #region privateMethods
        //constructor
        private MethodMetadata(MethodBase method)
        {
            m_Name = method.Name;
            m_GenericArguments = method.IsGenericMethodDefinition ? TypeMetadata.EmitGenericArguments(method.GetGenericArguments()) : null;
            m_ReturnType = EmitReturnType(method);
            m_Parameters = EmitParameters(method.GetParameters());
            m_Modifiers = EmitModifiers(method);
            m_Extension = EmitExtension(method);
        }

        //methods
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