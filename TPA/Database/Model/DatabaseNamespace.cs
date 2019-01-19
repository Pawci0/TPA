﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DTGBase;

namespace Database.Model
{
    [Table("Namespace")]
    public class DatabaseNamespace
    {
        #region Properties

        public int Id { get; set; }
        
        public string Name { get; set; }

        public new ICollection<DatabaseType> Types { get; set; }

        #endregion

        #region Constructors
        public DatabaseNamespace(NamespaceBase namespaceBase)
        {
            Name = namespaceBase.name;
            Types = namespaceBase.types?.Select(t => DatabaseType.GetOrAdd(t)).ToList();
        }

        public DatabaseNamespace()
        {
        }

        #endregion
    }

}