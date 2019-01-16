﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DTGBase;
using DTGBase.Enums;

namespace DBData.Entities
{
    [Table("Method")]
    public class DatabaseMetod
    {
        #region Propeties

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public bool Extension { get; set; }

        public DatabaseType ReturnType { get; set; }

        public IEnumerable<DatabaseType> GenericArguments { get; set; }

        public IEnumerable<DatabaseParameter> Parameters { get; set; }

        public AccessLevelEnum AccessLevel { get; set; }

        public AbstractEnum Abstract { get; set; }

        public StaticEnum Static { get; set; }

        public VirtualEnum Virtual { get; set; }

        #endregion

        #region Constructors

        public DatabaseMetod()
        {
            GenericArguments = new List<DatabaseType>();
            Parameters = new List<DatabaseParameter>();
            TypeConstructors = new HashSet<DatabaseType>();
            TypeMethods = new HashSet<DatabaseType>();
        }

        public DatabaseMetod(MethodBase methodBase)
        {
            Name = methodBase.name;
            Extension = methodBase.extension;
            ReturnType = new DatabaseType(methodBase.returnType);
            GenericArguments = methodBase.genericArguments?.Select(a => new DatabaseType(a));
            Parameters = methodBase.parameters?.Select(p => new DatabaseParameter(p));
            AccessLevel = methodBase.modifiers.Item1;
            Abstract = methodBase.modifiers.Item2;
            Static = methodBase.modifiers.Item3;
            Virtual = methodBase.modifiers.Item4;
        }

        #endregion

        #region Inverse Properties

        public virtual ICollection<DatabaseType> TypeConstructors { get; set; }

        public virtual ICollection<DatabaseType> TypeMethods { get; set; }

        #endregion


    }
}
