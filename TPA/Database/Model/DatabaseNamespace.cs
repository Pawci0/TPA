using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DTGBase;

namespace Database.Model
{
    [Table("Namespace")]
    public class DatabaseNamespace
    {
        #region Properties

        public int Id { get; set; }

        [Required, StringLength(150)]
        public string Name { get; set; }

        public new IEnumerable<DatabaseType> Types { get; set; }

        #endregion

        #region Constructors
        public DatabaseNamespace(NamespaceBase namespaceBase)
        {
            Name = namespaceBase.name;
            Types = namespaceBase.types?.Select(t => new DatabaseType(t));
        }

        #endregion
    }

}
