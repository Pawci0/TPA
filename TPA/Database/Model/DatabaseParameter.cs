using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DTGBase;

namespace Database.Model
{
    [Table("Parameter")]
    public class DatabaseParameter
    {
        #region Properties

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public new DatabaseType Type { get; set; }

        #endregion

        #region Constructors

        public DatabaseParameter()
        {
        }

        public DatabaseParameter(ParameterBase parameterBase)
        {
            Name = parameterBase.name;
            Type = DatabaseType.GetOrAdd(parameterBase.typeMetadata);
        }

        #endregion

        #region Inverse Properties

        public virtual ICollection<DatabaseMethod> MethodParameters { get; set; }

        public virtual ICollection<DatabaseType> TypeFields { get; set; }

        #endregion

    }
}
