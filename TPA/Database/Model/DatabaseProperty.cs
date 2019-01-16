using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBData.Entities
{
    [Table("Property")]
    public class DatabaseProperty
    {

        #region Constructor

        public DatabaseProperty()
        {
            TypeProperties = new HashSet<DatabaseType>();
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

        public virtual ICollection<DatabaseType> TypeProperties { get; set; }

        #endregion


    }
}
