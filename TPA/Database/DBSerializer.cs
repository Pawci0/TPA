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
        public AssemblyBase Deserialize(IFileSupplier supplier)
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
            ClearDB();
            DatabaseAssembly serializationModel = new DatabaseAssembly(target);
            using (var ctx = new DatabaseContext())
            {
                ctx.AssemblyModel.Add(serializationModel);
                ctx.SaveChanges();
            }
        }

        private void ClearDB()
        {
            using (var ctx = new DatabaseContext())
            {
                ctx.Database.ExecuteSqlCommand("Delete from Assembly");
                ctx.Database.ExecuteSqlCommand("Delete from Namespace");
                ctx.Database.ExecuteSqlCommand("Delete from Method");
                ctx.Database.ExecuteSqlCommand("Delete from Parameter");
                ctx.Database.ExecuteSqlCommand("Delete from Property");
                ctx.Database.ExecuteSqlCommand("Delete from Type");
            }
        }
    }
}
