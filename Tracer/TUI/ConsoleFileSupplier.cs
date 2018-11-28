using System;
using System.IO;
using ViewModel;

namespace TUI
{
    class ConsoleFileSupplier : IFileSupplier
    {
        public string GetFilePathToLoad()
        {
            string ret;
            do
            {
                Console.Write("Type the path to DLL or XML file: ");
                ret = Console.ReadLine();
                Console.Clear();
            } while (string.IsNullOrEmpty(ret) || !File.Exists(ret));
            return ret;
        }

        public string GetFilePathToSave()
        {
            string ret;
            do
            {
                Console.Write("Type the path to save: ");
                ret = Console.ReadLine();
                Console.Clear();
            } while (string.IsNullOrEmpty(ret));
            return ret;
        }
    }
}
