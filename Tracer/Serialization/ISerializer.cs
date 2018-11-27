namespace Serialization
{
    interface ISerializer
    {
        void Serialize(string filePath, object target);
        T Deserialize<T>(string filePath);
    }
}
