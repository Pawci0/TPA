using DTGBase;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Database.Model
{
    [Table("Assembly")]
    public class DatabaseAssembly
    {
        #region Properties

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public new ICollection<DatabaseNamespace> Namespaces { get; set; }
        #endregion

        #region Constructors

        public DatabaseAssembly() { }

        public DatabaseAssembly(AssemblyBase assemblyBase)
        {
            Name = assemblyBase.name;
            Namespaces = assemblyBase.namespaces?.Select(n => new DatabaseNamespace(n)).ToList();
        }

        #endregion

    }
}
