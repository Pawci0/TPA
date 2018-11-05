using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reflection.Enums;
using Reflection.Metadata;

namespace ReflectionTests
{
    [TestClass]
    public class ReflectionTest
    {
        private Assembly testAssembly; 

        [TestInitialize]
        public void Init()
        {
            string dll = @"Reflection.dll";
            testAssembly = Assembly.LoadFrom(dll);
        }

        [TestMethod]
        public void AssemblyLoadedSuccesfully()
        {
            Assert.IsNotNull(testAssembly);
        }

        [TestMethod]
        public void CorrectNamespacesInAssemblyMetadata()
        {
            AssemblyMetadata assemblyMetadata = new AssemblyMetadata(testAssembly);
            Assert.IsNotNull(assemblyMetadata);

            List<string> namespaceNames = new List<string>(from _namespace in assemblyMetadata.m_Namespaces
                                                           select _namespace.m_NamespaceName);

            Assert.IsTrue(namespaceNames.Contains("Reflection.Enums"));
            Assert.IsTrue(namespaceNames.Contains("Reflection.Metadata"));
        }

        [TestMethod]
        public void CorrectTypesInNamespaceMetadata()
        {
            AssemblyMetadata assemblyMetadata = new AssemblyMetadata(testAssembly);
            NamespaceMetadata namespaceMetadata = assemblyMetadata.m_Namespaces.ToList<NamespaceMetadata>()[0];

            List<string> typeNames = new List<string>(from _type in namespaceMetadata.m_Types
                                                      select _type.m_typeName);

            Assert.IsTrue(typeNames.Contains("AbstractEnum"));
            Assert.IsTrue(typeNames.Contains("AccessLevelEnum"));
            Assert.IsTrue(typeNames.Contains("SealedEnum"));
            Assert.IsTrue(typeNames.Contains("StaticEnum"));
            Assert.IsTrue(typeNames.Contains("VirtualEnum"));
        }

        [TestMethod]
        public void EmitPropertiesTest()
        {
            Type type = typeof(mockClass);
            List<PropertyMetadata> properties = PropertyMetadata.EmitProperties(type.GetProperties()).ToList<PropertyMetadata>();

            Assert.AreEqual(1, properties.Count);
            Assert.AreEqual("Property", properties[0].m_Name);
            Assert.AreEqual("Int32", properties[0].m_TypeMetadata.m_typeName);
        }

        [TestMethod]
        public void EmitMethodsTest()
        {
            Type type = typeof(mockClass);
            List<MethodMetadata> methods = MethodMetadata.EmitMethods(type.GetMethods()).ToList<MethodMetadata>();

            MethodMetadata method = methods.Find(m => m.m_Name.Equals("Method"));

            Assert.IsNotNull(method);
            Assert.AreEqual("Method", method.m_Name);
            Assert.AreEqual("Void", method.m_ReturnType.m_typeName);
            Assert.AreEqual(AccessLevelEnum.IsPublic, method.m_Modifiers.Item1);
            Assert.AreEqual(AbstractEnum.NotAbstract, method.m_Modifiers.Item2);
            Assert.AreEqual(StaticEnum.NotStatic, method.m_Modifiers.Item3);
            Assert.AreEqual(VirtualEnum.Virtual, method.m_Modifiers.Item4);

            List<ParameterMetadata> parameters = method.m_Parameters.ToList<ParameterMetadata>();

            Assert.AreEqual(1, parameters.Count());
            Assert.AreEqual("parameter", parameters[0].m_Name);
        }

        [TestMethod]
        public void EmitReferenceTest()
        {
            TypeMetadata type = TypeMetadata.EmitReference(typeof(mockClass));

            Assert.AreEqual("mockClass", type.m_typeName);
            Assert.AreEqual("ReflectionTests", type.m_NamespaceName);
            Assert.IsNull(type.m_GenericArguments);
        }

        [TestMethod]
        public void EmitGenericArgumentsTest()
        {
            List<TypeMetadata> types = TypeMetadata.EmitGenericArguments(typeof(genericMockClass<Int16>).GetGenericArguments())
                                                   .ToList<TypeMetadata>();

            Assert.AreEqual(1, types.Count);
            Assert.AreEqual("Int16", types[0].m_typeName);
        }

        #region mockClasses
        class mockClass
        {
            public Int32 Property { get; set; }

            virtual public void Method(Int32 parameter) { }
        }

        class genericMockClass<T> { }
        #endregion
    }
}
