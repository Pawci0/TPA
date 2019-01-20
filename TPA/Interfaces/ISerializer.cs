namespace Interfaces
{
    public interface ISerializer<T>
    {
        void Serialize(IFileSupplier supplier, T target);
        T Deserialize(IFileSupplier supplier);
    }
}
