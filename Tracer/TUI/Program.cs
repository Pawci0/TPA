using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Diagnostics;
using Reflection.Metadata;
using ViewModel.MetadataViews;

namespace TUI
{
    class Program
    {
        private static Stack<TypeMetadata> typeHistory = new Stack<TypeMetadata>();
        private static AssemblyMetadata assemblyMetadata;
        private static Dictionary<string, TypeMetadata> expandableTypes = new Dictionary<string, TypeMetadata>();
        private static Dictionary<string, NamespaceMetadata> namespaces = new Dictionary<string, NamespaceMetadata>();
        private static string selectedNamespace;
        private static Boolean namespaceSelected = false;

        static void Main(string[] args)
        {
            
            string userInput;

            string pathToDll = @"..\..\..\Reflection\bin\Debug\Reflection.dll";
            Assembly dotNetAssembly = Assembly.LoadFrom(pathToDll);

            assemblyMetadata = new AssemblyMetadata(dotNetAssembly);

            // add namespaces to Dictionary
            foreach (var @namespace in assemblyMetadata.m_Namespaces)
            {
                namespaces.Add(@namespace.m_NamespaceName, @namespace);
            }


            do
            {
                if(!namespaceSelected)
                {
                    Console.WriteLine("Choose a namespace");

                    foreach (var @namespace in assemblyMetadata.m_Namespaces)
                    {
                        Console.WriteLine(@namespace.m_NamespaceName);
                    }

                    // get namespace name from user

                    Console.Write(">>> ");

                    userInput = Console.ReadLine();

                    if (userInput == null)
                    {
                        Console.WriteLine("No input from user!");
                    }
                    else if(!assemblyMetadata.m_Namespaces.Any(item => item.m_NamespaceName == userInput))
                    {
                        Console.WriteLine("Given namespace does not exist");
                        continue;
                    }
                    else
                    {
                        selectedNamespace = userInput;
                        namespaceSelected = true;
                    }

                    // list types from selected dll file from namespace Reflection
                    ListDefaultTypes(userInput);
                    // print command prompt
                }   
                Console.Write(">>> ");

                userInput = Console.ReadLine();

                if (userInput == null)
                {
                    Console.WriteLine("No input from user!");
                    continue;
                }

                string command = userInput.Split(' ')[0];

                switch (command)
                {
                    case "expand":
                        if (userInput.Split(' ').Length == 2)
                        {
                            string selectedType = userInput.Split(' ')[1];

                            if (expandableTypes.ContainsKey(selectedType))
                            {
                                ExpandType(selectedType);
                            }
                            else
                            {
                                Console.WriteLine("Invalid type name");
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine("Write the type that you want to expand!");
                        }
                        break;
                    case "back":
                        GoBack();
                        break;
                    case "exit":
                        return;
                    case "help":
                        Console.WriteLine("Available commands:\n" +
                                          "\texpand [name of type]\n" +
                                          "\tback\n" +
                                          "\texit");
                        break;
                    default:
                        Console.WriteLine("Invalid command");
                        break;
                }
            } while (true);
        }

        private static void ListDefaultTypes(string namespaceName)
        {
            Console.Clear();
            expandableTypes.Clear();

            Console.WriteLine("Stored types: " + TypeMetadata.storedTypes.Count);
            foreach (var storedType in namespaces[namespaceName].m_Types)
            {
                Console.WriteLine(new TypeMetadataView(storedType));
                expandableTypes.Add(storedType.m_typeName, storedType);
            }
        }

        private static void GoBack()
        {
            if (typeHistory.Count > 0)
            {
                // check if only 1 value is stored in stack
                if (typeHistory.Count == 1)
                {
                    typeHistory.Clear();
                    ListDefaultTypes(selectedNamespace);
                }
                else
                {
                    typeHistory.Pop();
                    ExpandType(typeHistory.Pop().m_typeName);
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
            TypeMetadata type = TypeMetadata.storedTypes[typeName];
            typeHistory.Push(type);

            Console.Clear();
            expandableTypes.Clear();

            // fields
            foreach (var field in type.m_Fields)
            {
                string fieldTypeName = field.m_TypeMetadata.m_typeName;
                if (!expandableTypes.ContainsKey(fieldTypeName))
                {
                    expandableTypes.Add(fieldTypeName, field.m_TypeMetadata);
                }
            }

            // properties
            foreach (var property in type.m_Properties)
            {
                string propertyTypeName = property.m_TypeMetadata.m_typeName;
                if (!expandableTypes.ContainsKey(propertyTypeName))
                {
                    expandableTypes.Add(propertyTypeName, property.m_TypeMetadata);
                }
            }

            // methods
            foreach (var method in type.m_Methods)
            {
                // return type
                string returnTypeName = method.m_ReturnType.m_typeName;
                if (!expandableTypes.ContainsKey(returnTypeName))
                {
                    expandableTypes.Add(returnTypeName, method.m_ReturnType);
                }

                // parameters
                foreach (var parameter in method.m_Parameters)
                {
                    string parameterTypeName = parameter.m_TypeMetadata.m_typeName;
                    if (!expandableTypes.ContainsKey(parameterTypeName))
                    {
                        expandableTypes.Add(parameterTypeName, parameter.m_TypeMetadata);
                    }
                }
            }

            Console.WriteLine("Stored types: " + TypeMetadata.storedTypes.Count);
            Console.Write(new ItemView(type));
        }
    }
}
