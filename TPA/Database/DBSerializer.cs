using Database.Model;
using DTGBase;
using Interfaces;
using System;
using System.Data.Entity;
using System.Linq;

namespace Database
{
    class DBSerializer : ISerializer<AssemblyBase>
    {
        public AssemblyBase Deserialize(string filePath)
        {
            using(var ctx = new DatabaseContext())
            {
                ctx.Configuration.ProxyCreationEnabled = false;
                ctx.NamespaceModel
                    .Include(n => n.Types)
                    .Load();
                ctx.TypeModel
                    .Include(t => t.Constructors)
                    .Include(t => t.BaseType)
                    .Include(t => t.DeclaringType)
                    .Include(t => t.Fields)
                    .Include(t => t.ImplementedInterfaces)
                    .Include(t => t.GenericArguments)
                    .Include(t => t.Methods)
                    .Include(t => t.NestedTypes)
                    .Include(t => t.Properties)
                    .Include(t => t.TypeGenericArguments)
                    .Include(t => t.TypeImplementedInterfaces)
                    .Include(t => t.TypeNestedTypes)
                    .Include(t => t.MethodGenericArguments)
                    .Include(t => t.TypeBaseTypes)
                    .Include(t => t.TypeDeclaringTypes)
                    .Load();
                ctx.ParameterModel
                    .Include(p => p.Type)
                    .Include(p => p.TypeFields)
                    .Include(p => p.MethodParameters)
                    .Load();
                ctx.MethodModel
                    .Include(m => m.GenericArguments)
                    .Include(m => m.Parameters)
                    .Include(m => m.ReturnType)
                    .Include(m => m.TypeConstructors)
                    .Include(m => m.TypeMethods)
                    .Load();
                ctx.PropertyModel
                    .Include(p => p.Type)
                    .Include(p => p.TypeProperties)
                    .Load();

                DatabaseAssembly dbAssembly = ctx.AssemblyModel
                    .Include(a => a.Namespaces)
                    .ToList().FirstOrDefault();

                if(dbAssembly == null)
                {
                    throw new ArgumentException("Database is empty");
                }
                return DTGMapper.ToBase(dbAssembly);
            }
        }

        public void Serialize(string filePath, AssemblyBase target)
        {
            Clear();
            DatabaseAssembly serializationModel = new DatabaseAssembly(target);
            using (var ctx = new DatabaseContext())
            {
                ctx.AssemblyModel.Add(serializationModel);
                ctx.SaveChanges();
            }
        }

        private void Clear()
        {
            using (var ctx = new DatabaseContext())
            {
                ctx.Database.ExecuteSqlCommand("DELETE FROM Parameter WHERE ID != -1");
                ctx.Database.ExecuteSqlCommand("DELETE FROM Property WHERE ID != -1");
                ctx.Database.ExecuteSqlCommand("DELETE FROM Method WHERE ID != -1");
                ctx.Database.ExecuteSqlCommand("DELETE FROM Type ");
                ctx.Database.ExecuteSqlCommand("DELETE FROM Namespace WHERE ID != -1");
                ctx.Database.ExecuteSqlCommand("DELETE FROM Assembly WHERE ID != -1");
                ctx.SaveChanges();
            }
        }
    }
}
