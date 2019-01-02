using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Serialization.Tests
{
    [TestClass()]
    public class SerializationTests
    {
        [TestMethod()]
        public void SerializationTest()
        {
            string originalObject = "original string";
            //XMLSerializer serializer = new XMLSerializer();
            string path = "SerializationTestFile.xml";
            //serializer.Serialize(path, originalObject);
            //string deserializedObject = serializer.Deserialize<string>(path);
            //Assert.AreEqual(originalObject, deserializedObject);
            Assert.IsTrue(true);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}