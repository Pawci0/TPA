using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBData.Entities
{
    [Table("Method")]
    public class DatabaseMetod
    {

        #region Constructor

        public DatabaseMetod()
        {
            GenericArguments = new List<DatabaseType>();
            Parameters = new List<DatabaseParameter>();
            TypeConstructors = new HashSet<DatabaseType>();
            TypeMethods = new HashSet<DatabaseType>();
        }

        #endregion

        #region Propeties

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public bool Extension { get; set; }
        
        //TODO zmienic na tego tupla
        //public MethodModifiers Modifiers { get; set; }

        public new DatabaseType ReturnType { get; set; }
        public new List<DatabaseType> GenericArguments { get; set; }
        public new List<DatabaseParameter> Parameters { get; set; }

        #endregion

        #region Inverse Properties

        public virtual ICollection<DatabaseType> TypeConstructors { get; set; }

        public virtual ICollection<DatabaseType> TypeMethods { get; set; }

        #endregion


    }
}
