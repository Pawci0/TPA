using System;
using System.IO;
using ViewModel;

namespace TUI
{
    class ConsoleFileSupplier : IFileSupplier
    {
        public string GetFilePath()
        {
            string ret;
            do
            {
                Console.Write("Type the path to DLL: ");
                ret = Console.ReadLine();
                Console.Clear();
            } while (string.IsNullOrEmpty(ret) || !File.Exists(ret));
            return ret;
        }
    }
}
