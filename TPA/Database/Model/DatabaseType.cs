using DTGBase;
using DTGBase.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Database.Model
{
    [Table("Type")]
    public class DatabaseType
    {
        public static Dictionary<string, DatabaseType> storedTypes = new Dictionary<string, DatabaseType>();

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

        public new ICollection<DatabaseMethod> Constructors { get; set; }

        public new ICollection<DatabaseParameter> Fields { get; set; }

        public new ICollection<DatabaseType> GenericArguments { get; set; }

        public new ICollection<DatabaseType> ImplementedInterfaces { get; set; }

        public new ICollection<DatabaseMethod> Methods { get; set; }

        public new ICollection<DatabaseType> NestedTypes { get; set; }

        public new ICollection<DatabaseProperty> Properties { get; set; }

        #endregion

        #region Constructors

        public DatabaseType()
        {
            MethodGenericArguments = new List<DatabaseMethod>();
            TypeGenericArguments = new List<DatabaseType>();
            TypeImplementedInterfaces = new List<DatabaseType>();
            TypeNestedTypes = new List<DatabaseType>();
            Constructors = new List<DatabaseMethod>();
            Fields = new List<DatabaseParameter>();
            GenericArguments = new List<DatabaseType>();
            ImplementedInterfaces = new List<DatabaseType>();
            Methods = new List<DatabaseMethod>();
            NestedTypes = new List<DatabaseType>();
            Properties = new List<DatabaseProperty>();

        }

        public DatabaseType(TypeBase typeBase)
        {
            if (!storedTypes.ContainsKey(typeBase.typeName))
            {
                storedTypes.Add(typeBase.typeName, this);
            }
            Name = typeBase.typeName;
            Namespace = typeBase.namespaceName;
            BaseType = GetOrAdd(typeBase.baseType);
            Type = typeBase.typeKind;
            DeclaringType = GetOrAdd(typeBase.declaringType);
            AccessLevel = typeBase.modifiers.Item1;
            Sealed = typeBase.modifiers.Item2;
            Abstract = typeBase.modifiers.Item3;
            Constructors = typeBase.constructors?.Select(c => new DatabaseMethod(c)).ToList();
            Fields = typeBase.fields?.Select(f => new DatabaseParameter(f)).ToList();
            GenericArguments = typeBase.genericArguments?.Select(a => GetOrAdd(a)).ToList();
            ImplementedInterfaces = typeBase.implementedInterfaces?.Select(i => GetOrAdd(i)).ToList();
            Methods = typeBase.methods?.Select(m => new DatabaseMethod(m)).ToList();
            NestedTypes = typeBase.nestedTypes?.Select(t => GetOrAdd(t)).ToList();
            Properties = typeBase.properties?.Select(p => new DatabaseProperty(p)).ToList();
        }

        #endregion

        #region Inverse Properties

        [InverseProperty("BaseType")]
        public virtual ICollection<DatabaseType> TypeBaseTypes { get; set; }

        [InverseProperty("DeclaringType")]
        public virtual ICollection<DatabaseType> TypeDeclaringTypes { get; set; }

        public virtual ICollection<DatabaseMethod> MethodGenericArguments { get; set; }

        public virtual ICollection<DatabaseType> TypeGenericArguments { get; set; }

        public virtual ICollection<DatabaseType> TypeImplementedInterfaces { get; set; }

        public virtual ICollection<DatabaseType> TypeNestedTypes { get; set; }

        #endregion

        public static DatabaseType GetOrAdd(TypeBase baseType)
        {
            if (baseType != null)
            {
                if (storedTypes.ContainsKey(baseType.typeName))
                {
                    return storedTypes[baseType.typeName];
                }
                else
                {
                    return new DatabaseType(baseType);
                }
            }
            else
                return null;
        }

    }
}
