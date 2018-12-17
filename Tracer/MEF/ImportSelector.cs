using System;
using System.Collections.Generic;
using System.Configuration;

namespace MEF
{
    public class ImportSelector<T> : List<T>
    {
        public T GetImport()
        {
            var settings = ConfigurationManager.AppSettings;
            string name = typeof(T).Name;
            string result = settings[name];
            return this.Find(item => item.GetType().Name == result);
        }
    }
}
