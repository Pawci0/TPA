namespace Interfaces
{
    public interface IFileSupplier
    {
        string GetFilePathToLoad(string filter = null);
        string GetFilePathToSave(string filter = null);
    }
}
