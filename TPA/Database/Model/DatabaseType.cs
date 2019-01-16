using DTGBase;
using DTGBase.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DBData.Entities
{
    [Table("Type")]
    public class DatabaseType
    {
        #region Properties

        [Key, StringLength(150)]
        public string Name { get; set; }

        public string Namespace { get; set; }

        public new DatabaseType BaseType { get; set; }

        public TypeKindEnum Type { get; set; }
        public new DatabaseType DeclaringType { get; set; }

        public AccessLevelEnum AccessLevel { get; set; }

        public SealedEnum Sealed { get; set; }

        public AbstractEnum Abstract { get; set; }

        public new IEnumerable<DatabaseMetod> Constructors { get; set; }

        public new IEnumerable<DatabaseParameter> Fields { get; set; }

        public new IEnumerable<DatabaseType> GenericArguments { get; set; }

        public new IEnumerable<DatabaseType> ImplementedInterfaces { get; set; }

        public new IEnumerable<DatabaseMetod> Methods { get; set; }

        public new IEnumerable<DatabaseType> NestedTypes { get; set; }

        public new IEnumerable<DatabaseProperty> Properties { get; set; }

        #endregion

        #region Constructors

        public DatabaseType()
        {
            MethodGenericArguments = new List<DatabaseMetod>();
            TypeGenericArguments = new List<DatabaseType>();
            TypeImplementedInterfaces = new List<DatabaseType>();
            TypeNestedTypes = new List<DatabaseType>();
            Constructors = new List<DatabaseMetod>();
            Fields = new List<DatabaseParameter>();
            GenericArguments = new List<DatabaseType>();
            ImplementedInterfaces = new List<DatabaseType>();
            Methods = new List<DatabaseMetod>();
            NestedTypes = new List<DatabaseType>();
            Properties = new List<DatabaseProperty>();

        }

        public DatabaseType(TypeBase typeBase)
        {
            Name = typeBase.typeName;
            Namespace = typeBase.namespaceName;
            BaseType = new DatabaseType(typeBase.baseType);
            Type = typeBase.typeKind;
            DeclaringType = new DatabaseType(typeBase.declaringType);
            AccessLevel = typeBase.modifiers.Item1;
            Sealed = typeBase.modifiers.Item2;
            Abstract = typeBase.modifiers.Item3;
            Constructors = typeBase.constructors?.Select(c => new DatabaseMetod(c));
            Fields = typeBase.fields?.Select(f => new DatabaseParameter(f));
            GenericArguments = typeBase.genericArguments?.Select(a => new DatabaseType(a));
            ImplementedInterfaces = typeBase.implementedInterfaces?.Select(i => new DatabaseType(i));
            Methods = typeBase.methods?.Select(m => new DatabaseMetod(m));
            NestedTypes = typeBase.nestedTypes?.Select(t => new DatabaseType(t));
            Properties = typeBase.properties?.Select(p => new DatabaseProperty(p));
        }

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
