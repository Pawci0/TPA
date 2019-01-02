using DTGBase;
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
            XMLSerializer serializer = new XMLSerializer();
            string path = "SerializationTestFile.xml";
            serializer.Serialize(path, originalObject);
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
}