﻿using ViewModel.MetadataViews;
using ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Serialization;

namespace TUI
{
    class Program
    {
        static void Main(string[] args)
        {
            ReflectionViewModel dataContext = new ReflectionViewModel();
            BaseMetadataView currentRoot;
            Stack<BaseMetadataView> previousRoots = new Stack<BaseMetadataView>();
            dataContext.BrowseCommand.Execute(null);
            dataContext.LoadDLLCommand.Execute(null);
            currentRoot = dataContext.Tree[0];
            currentRoot.IsExpanded = true;
            Console.WriteLine("Available commands:\n" +
                              "\t[typeName] - expands selected type\n" +
                              "\treturn - go back to previous type\n" +
                              "\ttoXML - save currnet model to XML file\n" +
                              "\texit - close application\n\n" +
                              "Press any key to continue: ");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine(currentRoot.Name);
            string nextType;
            while (true)
            {
                PrintChildren(currentRoot.Children);
                Console.Write("> ");
                nextType = Console.ReadLine();
                Console.Clear();
                if (nextType.Equals("return"))
                {
                    if(previousRoots.Count != 0)
                    {
                        currentRoot = previousRoots.Pop();
                    }
                }else if(nextType.Equals("exit"))
                {
                    return;
                }
                else if (nextType.Equals("toXML"))
                {
                    dataContext.SaveCommand.Execute(null);
                }
                else
                {
                    previousRoots.Push(currentRoot);
                    try
                    {
                        currentRoot = currentRoot.Children.First(i => i.Name.Equals(nextType));
                    }catch(InvalidOperationException)
                    {
                        Console.WriteLine("ERR: no such type");
                    }
                }
                Console.WriteLine(currentRoot.Name);
                currentRoot.IsExpanded = true;
            }
        }

        static void PrintChildren(IEnumerable<BaseMetadataView> children)
        {
            foreach (BaseMetadataView item in children)
            {
                Console.WriteLine("\t" + item);
            }
        }
    }
}
