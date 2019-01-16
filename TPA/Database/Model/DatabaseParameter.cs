using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBData.Entities
{
    [Table("Parameter")]
    public class DatabaseParameter
    {

        #region Constructor

        public DatabaseParameter()
        {
            MethodParameters = new HashSet<DatabaseMetod>();
            TypeFields = new HashSet<DatabaseType>();
        }

        #endregion

        #region Properties

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public new DatabaseType Type { get; set; }

        #endregion

        #region Inverse Properties

        public virtual ICollection<DatabaseMetod> MethodParameters { get; set; }

        public virtual ICollection<DatabaseType> TypeFields { get; set; }

        #endregion

    }
}
