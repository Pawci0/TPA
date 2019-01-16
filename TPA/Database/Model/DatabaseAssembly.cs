using DTGBase;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBData.Entities
{
    [Table("Assembly")]
    [Export(typeof(AssemblyBase))]
    public class DatabaseAssembly
    {
        #region Properties

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public new List<DatabaseNamespace> Namespaces { get; set; }

        #endregion

    }
}
