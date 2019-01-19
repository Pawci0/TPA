using Database.Model;
using DTGBase;
using Interfaces;
using System;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;

namespace Database
{
    [Export(typeof(ISerializer<AssemblyBase>))]
    class DBSerializer : ISerializer<AssemblyBase>
    {
        public AssemblyBase Deserialize(string filename)
        {
            AssemblyBase assembly;
            using(var ctx = new DatabaseContext())
            {
                ctx.AssemblyModel.Load();
                ctx.NamespaceModel.Load();
                ctx.TypeModel.Load();
                ctx.MethodModel.Load();
                ctx.PropertyModel.Load();
                ctx.ParameterModel.Load();

                assembly = DTGMapper.ToBase(ctx.AssemblyModel.FirstOrDefault());

                if(assembly == null)
                {
                    throw new ArgumentException("Database is empty");
                }
            }
            return assembly;
        }

        public void Serialize(IFileSupplier supplier, AssemblyBase target)
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
