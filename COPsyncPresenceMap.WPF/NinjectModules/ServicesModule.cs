using COPsyncPresenceMap.SvgImplementation;
using COPsyncPresenceMap.XlsxImplementation;
using Ninject.Modules;
using System;

namespace COPsyncPresenceMap.WPF.NinjectModules
{
    public class ServicesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<COPsyncPresenceMapGenerator>().To<COPsyncPresenceMapGenerator>();
            Bind<ISpreadsheetParser>().To<XlsxParser>();
            Bind<IMapGraphicParser>().To<SvgMapGraphicParser>();
        }
    }
}
