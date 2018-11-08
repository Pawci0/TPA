using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using ViewModel.MetadataViews;
using Tracer;
using System.Diagnostics;

namespace TUI
{
    class Program
    {
        private static AssemblyMetadataView assemblyMetadataView;
        private static Dictionary<string, TypeMetadataView> expandableTypes = new Dictionary<string, TypeMetadataView>();
        private static string selectedNamespace;
        private static bool namespaceSelected = false;
        private static ITracer tracer = new FileTracer("TUI.log", TraceLevel.Verbose);

        static void Main(string[] args)
        {
            tracer.Log(TraceLevel.Info, "Application starting");
            string input;
            tracer.Log(TraceLevel.Verbose, "Loading DLL file");
            string pathToDll = @"..\..\..\Reflection\bin\Debug\Reflection.dll";
            Assembly dotNetAssembly = Assembly.LoadFrom(pathToDll);
            assemblyMetadataView = new AssemblyMetadataView(pathToDll);
            tracer.Log(TraceLevel.Verbose, "DLL file loaded");

            do
            {
                if(!namespaceSelected)
                {
                    Console.WriteLine("Select the namespace");

                    foreach (var @namespace in assemblyMetadataView.Namespaces)
                    {
                        Console.WriteLine(@namespace.m_NamespaceName);
                    }

                    Console.Write("> ");

                    input = Console.ReadLine();

                    if(input == "exit")
                    {
                        tracer.Log(TraceLevel.Info, "Exiting application");
                        return;
                    }

                    if (input == null)
                    {
                        Console.WriteLine("Nothing selected");
                        tracer.Log(TraceLevel.Warning, "No namespace selected");
                    }
                    else if(!assemblyMetadataView.Namespaces.Any(item => item.m_NamespaceName == input))
                    {
                        Console.WriteLine("Given namespace does not exist");
                        tracer.Log(TraceLevel.Warning, "Given namespace does not exist");
                        continue;
                    }
                    else
                    {
                        selectedNamespace = input;
                        namespaceSelected = true;
                        tracer.Log(TraceLevel.Info, "Selected namespace " + selectedNamespace);
                    }
                    
                    ListNamespaces(input);
                }   
                Console.Write("> ");

                input = Console.ReadLine();

                if (input == null)
                {
                    Console.WriteLine("Nothing selected");
                    tracer.Log(TraceLevel.Warning, "No type selected");
                    continue;
                }

                string command = input.Split(' ')[0];

                switch (command)
                {
                    case "expand":
                        if (input.Split(' ').Length == 2)
                        {
                            string selectedType = input.Split(' ')[1];
                            tracer.Log(TraceLevel.Info, "Selected type " + selectedType);
                            if (expandableTypes.ContainsKey(selectedType))
                            {
                                ExpandType(selectedType);
                            }
                            else
                            {
                                Console.WriteLine("Invalid type name");
                                tracer.Log(TraceLevel.Warning, "Invalid type name");
                            }                            
                        }
                        else
                        {
                            Console.WriteLine("Nothing selected");
                            tracer.Log(TraceLevel.Warning, "No type selected");
                        }
                        break;
                    case "back":
                        tracer.Log(TraceLevel.Info, "Returning to previous view");
                        GoBack();
                        break;
                    case "exit":
                        tracer.Log(TraceLevel.Info, "Exiting application");
                        return;
                    default:
                        tracer.Log(TraceLevel.Warning, "Invalid command");
                        Console.WriteLine("Invalid command\nPrinting help:\n");
                        Console.WriteLine("Available commands:\n" +
                                          "\texpand [type] - expands given type\n" +
                                          "\tback - go back to the previous type\n" +
                                          "\texit - exit app");
                        break;
                }
            } while (true);
        }

        private static void ListNamespaces(string namespaceName)
        {
            Console.Clear();
            expandableTypes.Clear();
            tracer.Log(TraceLevel.Info, "Expanding namespace " + namespaceName);
            foreach (var storedType in assemblyMetadataView.getNamespaceDict()[namespaceName].m_Types)
            {
                TypeMetadataView itemView = new TypeMetadataView(storedType);
                Console.WriteLine(itemView);
                expandableTypes.Add(storedType.m_typeName, itemView);
            }
            tracer.Log(TraceLevel.Info, "Namespace expanded");
        }

        private static void GoBack()
        {
            namespaceSelected = false;
            Console.Clear();
        }

        private static void ExpandType(string typeName)
        {
            TypeMetadataView item = expandableTypes[typeName];
            expandableTypes.Clear();
            item.GetInternalTypes(expandableTypes);
            Console.WriteLine(new TUIItemView(item.Type));
        }
    }
}
