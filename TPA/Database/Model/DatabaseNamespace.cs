using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBData.Entities
{
    [Table("Namespace")]
    public class DatabaseNamespace
    {

        #region Properties

        public int Id { get; set; }

        [Required, StringLength(150)]
        public string Name { get; set; }

        public new List<DatabaseType> Types { get; set; }

        #endregion


    }

}
