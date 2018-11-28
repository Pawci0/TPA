using System;
using System.IO;
using System.Xml.Serialization;

namespace Serialization
{
    public class XMLSerializer : ISerializer
    {
        private XmlSerializer serializer;

        public void Serialize(string filePath, object target)
        {
            serializer = new XmlSerializer(target.GetType());
            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                serializer.Serialize(streamWriter, target);
            }
        }

        public T Deserialize<T>(string filePath)
        {
            serializer = new XmlSerializer(typeof(T));
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                return (T) serializer.Deserialize(fileStream);
            }
        }
    }
}
