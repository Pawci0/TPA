using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace MEF
{
    public class Bootstrapper
    {
        private CompositionContainer container;
        public void ComposeApplication(object o)
        {
            AggregateCatalog catalog = new AggregateCatalog();
            DirectoryCatalog exe = new DirectoryCatalog("..\\..\\..\\Parts", "*.exe");
            DirectoryCatalog dll = new DirectoryCatalog("..\\..\\..\\Parts");
            catalog.Catalogs.Add(exe);
            catalog.Catalogs.Add(dll);
            container = new CompositionContainer(catalog);
            container.ComposeParts(o);
        }
    }
}
