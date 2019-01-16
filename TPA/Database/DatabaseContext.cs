using DBData.Entities;
using System.Data.Entity;

namespace DBData
{


    public class DatabaseContext : DbContext
    {

        public DatabaseContext()
                   : base("TPA_DataBase")
        {
        }

        public virtual DbSet<DatabaseAssembly> AssemblyModel { get; set; }
        public virtual DbSet<DatabaseMetod> MethodModel { get; set; }
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
