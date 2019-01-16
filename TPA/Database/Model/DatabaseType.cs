using DTGBase.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBData.Entities
{
    [Table("Type")]
    public class DatabaseType
    {

        #region Constructor

        public DatabaseType()
        {
            MethodGenericArguments = new HashSet<DatabaseMetod>();
            TypeGenericArguments = new HashSet<DatabaseType>();
            TypeImplementedInterfaces = new HashSet<DatabaseType>();
            TypeNestedTypes = new HashSet<DatabaseType>();
            Constructors = new List<DatabaseMetod>();
            Fields = new List<DatabaseParameter>();
            GenericArguments = new List<DatabaseType>();
            ImplementedInterfaces = new List<DatabaseType>();
            Methods = new List<DatabaseMetod>();
            NestedTypes = new List<DatabaseType>();
            Properties = new List<DatabaseProperty>();

        }

        #endregion

        #region Properties

        [Key, StringLength(150)]
        public string Name { get; set; }

        public string AssemblyName { get; set; }

        public bool IsExternal { get; set; }

        public bool IsGeneric { get; set; }

        public new DatabaseType BaseType { get; set; }

        public TypeKindEnum Type { get; set; }
        public new DatabaseType DeclaringType { get; set; }

        //TODO zmienic na pojedyncze
        //public TypeModifiers Modifiers { get; set; }

        public int? NamespaceId { get; set; }

        public new List<DatabaseMetod> Constructors { get; set; }

        public new List<DatabaseParameter> Fields { get; set; }

        public new List<DatabaseType> GenericArguments { get; set; }

        public new List<DatabaseType> ImplementedInterfaces { get; set; }

        public new List<DatabaseMetod> Methods { get; set; }

        public new List<DatabaseType> NestedTypes { get; set; }

        public new List<DatabaseProperty> Properties { get; set; }

        #endregion

        #region Inverse Properties

        [InverseProperty("BaseType")]
        public virtual ICollection<DatabaseType> TypeBaseTypes { get; set; }

        [InverseProperty("DeclaringType")]
        public virtual ICollection<DatabaseType> TypeDeclaringTypes { get; set; }

        public virtual ICollection<DatabaseMetod> MethodGenericArguments { get; set; }

        public virtual ICollection<DatabaseType> TypeGenericArguments { get; set; }

        public virtual ICollection<DatabaseType> TypeImplementedInterfaces { get; set; }

        public virtual ICollection<DatabaseType> TypeNestedTypes { get; set; }

        #endregion

    }
}
