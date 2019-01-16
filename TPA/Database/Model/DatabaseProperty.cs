using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DTGBase;

namespace Database.Model
{
    [Table("Property")]
    public class DatabaseProperty
    {
        #region Properties

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public new DatabaseType Type { get; set; }

        #endregion

        #region Constructors

        public DatabaseProperty()
        {
            TypeProperties = new HashSet<DatabaseType>();
        }

        public DatabaseProperty(PropertyBase propertyBase)
        {
            Name = propertyBase.name;
            Type = DatabaseType.GetOrAdd(propertyBase.typeMetadata);
        }

        #endregion

        #region Inverse Properties

        public virtual ICollection<DatabaseType> TypeProperties { get; set; }

        #endregion


    }
}
