using System;
using System.IO;
using System.Xml.Serialization;

namespace Serialization
{
    class XMLSerializer : ISerializer
    {
        private XmlSerializer serializer;
        public XMLSerializer(Type serializedType)
        {
            serializer = new XmlSerializer(serializedType);
        }

        public void Serialize(string filePath, object target)
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                serializer.Serialize(streamWriter, target);
            }
        }

        public T Deserialize<T>(string filePath)
        {
            using(FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                return (T) serializer.Deserialize(fileStream);
            }
        }
    }
}
