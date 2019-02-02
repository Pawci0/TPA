using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace MEF
{
    public class Bootstrapper
    {
        private CompositionContainer container;

        private static Bootstrapper instance=null;

        private Bootstrapper() { }

        private static Bootstrapper GetInstance()
        {
            if(instance == null)
            {
                instance = new Bootstrapper();
            }
            return instance;
        }

        public static void ComposeApplication(object o)
        {
            AggregateCatalog catalog = new AggregateCatalog();
            DirectoryCatalog exe = new DirectoryCatalog("..\\..\\..\\Parts", "*.exe");
            DirectoryCatalog dll = new DirectoryCatalog("..\\..\\..\\Parts");
            catalog.Catalogs.Add(exe);
            catalog.Catalogs.Add(dll);
            GetInstance().container = new CompositionContainer(catalog);
            GetInstance().container.ComposeParts(o);
        }
    }
}
