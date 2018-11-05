﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Reflection
{
    internal class NamespaceMetadata
    {
        #region privateFields
        private string m_NamespaceName;
        private IEnumerable<TypeMetadata> m_Types;
        #endregion

        internal NamespaceMetadata(string name, IEnumerable<Type> types)
        {
            m_NamespaceName = name;
            m_Types = from type in types
                      orderby type.Name
                      select new TypeMetadata(type);
        }
    }
}