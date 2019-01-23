using Database.Model;
using System;
using System.Configuration;
using System.Data.Entity;
using System.IO;

namespace Database
{


    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
                   : base(ConfigurationManager.AppSettings["connectionString"])
        {
            string relative = @"..\..\..\Database";
            string absolute = Path.GetFullPath(relative);

            AppDomain.CurrentDomain.SetData("DataDirectory", absolute);
        }

        public virtual DbSet<DatabaseAssembly> AssemblyModel { get; set; }
        public virtual DbSet<DatabaseMethod> MethodModel { get; set; }
        public virtual DbSet<DatabaseNamespace> NamespaceModel { get; set; }
        public virtual DbSet<DatabaseParameter> ParameterModel { get; set; }
        public virtual DbSet<DatabaseProperty> PropertyModel { get; set; }
        public virtual DbSet<DatabaseType> TypeModel { get; set; }
        public virtual DbSet<DatabaseLog> Log { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
