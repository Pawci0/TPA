using ViewModel;

namespace TUI
{
    class ConsoleFileSupplier : IFileSupplier
    {
        public string GetFilePath()
        {
            System.Console.Write("Type the path to DLL: ");
            return System.Console.ReadLine();
        }
    }
}
