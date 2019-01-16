using DTGBase;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Serialization.Tests
{
    [TestClass()]
    public class SerializationTests
    {
        [TestMethod()]
        public void SerializationTest()
        {
            IFileSupplier supplier = new Supplier();
            List<NamespaceBase> list = new List<NamespaceBase>()
            {
                new NamespaceBase()
                {
                    name = "n1"
                },
                new NamespaceBase()
                {
                    name = "n2"
                }
            };
            AssemblyBase originalObject = new AssemblyBase()
            {
                name = "test",
                namespaces = list
            };
            string path = "SerializationTestFile.xml";
            XMLSerializer serializer = new XMLSerializer();
            serializer.Serialize(supplier, originalObject);
            AssemblyBase deserializedObject = serializer.Deserialize(path);
            Assert.AreEqual(originalObject.name, deserializedObject.name);
            Assert.AreEqual(originalObject.namespaces.ToList()[0].name, deserializedObject.namespaces.ToList()[0].name);
            Assert.AreEqual(originalObject.namespaces.ToList()[1].name, deserializedObject.namespaces.ToList()[1].name);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }

    class Supplier : IFileSupplier
    {
        public string GetFilePathToLoad()
        {
            return "SerializationTestFile.xml";
        }

        public string GetFilePathToSave()
        {
            return "SerializationTestFile.xml";
        }
    }
}