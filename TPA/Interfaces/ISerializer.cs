namespace Serialization
{
    public interface ISerializer<T>
    {
        void Serialize(string filePath, T target);
        T Deserialize(string filePath);
    }
}
