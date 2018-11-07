using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using ViewModel.MetadataViews;
using Tracer;
using System.Diagnostics;
using Reflection.Metadata;

namespace TUI
{
    class Program
    {
        private static Stack<TypeMetadata> previousTypes = new Stack<TypeMetadata>();
        private static AssemblyMetadataView assemblyMetadataView;
        private static Dictionary<string, TypeMetadata> expandableTypes = new Dictionary<string, TypeMetadata>();
        private static string selectedNamespace;
        private static bool namespaceSelected = false;
        private static ITracer tracer = new FileTracer("TUI.log", TraceLevel.Warning);

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
                    
                    ListTypes(input);
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
                    case "help":
                        tracer.Log(TraceLevel.Info, "Displayed help info");
                        Console.WriteLine("Available commands:\n" +
                                          "\texpand [name of type]\n" +
                                          "\tback\n" +
                                          "\texit");
                        break;
                    default:
                        Console.WriteLine("Invalid command");
                        tracer.Log(TraceLevel.Warning, "Invalid command");
                        break;
                }
            } while (true);
        }

        private static void ListTypes(string namespaceName)
        {
            Console.Clear();
            expandableTypes.Clear();
            tracer.Log(TraceLevel.Info, "Expanding namespace " + namespaceName);
            foreach (var storedType in assemblyMetadataView.getNamespaceDict()[namespaceName].m_Types)
            {
                Console.WriteLine(new TypeMetadataView(storedType));
                expandableTypes.Add(storedType.m_typeName, storedType);
            }
            tracer.Log(TraceLevel.Info, "Namespace expanded");
        }

        private static void GoBack()
        {
            if (previousTypes.Count > 0)
            {
                if (previousTypes.Count == 1)
                {
                    previousTypes.Clear();
                    ListTypes(selectedNamespace);
                }
                else
                {
                    previousTypes.Pop();
                    ExpandType(previousTypes.Pop().m_typeName);
                }
            }
            else
            {
                namespaceSelected = false;
                Console.Clear();
            }
        }

        private static void ExpandType(string typeName)
        {
            tracer.Log(TraceLevel.Info, "Expanding type " + typeName);
            TypeMetadata type = TypeMetadata.storedTypes[typeName];
            previousTypes.Push(type);

            Console.Clear();
            expandableTypes.Clear();
            tracer.Log(TraceLevel.Info, "Expanding fields");
            foreach (var field in type.m_Fields)
            {
                string fieldTypeName = field.m_TypeMetadata.m_typeName;
                if (!expandableTypes.ContainsKey(fieldTypeName))
                {
                    expandableTypes.Add(fieldTypeName, field.m_TypeMetadata);
                }
            }
            tracer.Log(TraceLevel.Info, "Expanding properties");
            foreach (var property in type.m_Properties)
            {
                string propertyTypeName = property.m_TypeMetadata.m_typeName;
                if (!expandableTypes.ContainsKey(propertyTypeName))
                {
                    expandableTypes.Add(propertyTypeName, property.m_TypeMetadata);
                }
            }
            tracer.Log(TraceLevel.Info, "Expanding methods");
            foreach (var method in type.m_Methods)
            {
                string returnTypeName = method.m_ReturnType.m_typeName;
                if (!expandableTypes.ContainsKey(returnTypeName))
                {
                    expandableTypes.Add(returnTypeName, method.m_ReturnType);
                }
                
                foreach (var parameter in method.m_Parameters)
                {
                    string parameterTypeName = parameter.m_TypeMetadata.m_typeName;
                    if (!expandableTypes.ContainsKey(parameterTypeName))
                    {
                        expandableTypes.Add(parameterTypeName, parameter.m_TypeMetadata);
                    }
                }
            }
            Console.Write(new ItemView(type));
            tracer.Log(TraceLevel.Info, "Finished expanding type");
        }
    }
}
