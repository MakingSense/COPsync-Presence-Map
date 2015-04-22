using COPsyncPresenceMap.Graphics;
using COPsyncPresenceMap.Spreadsheet;
using Ninject.Modules;
using System;

namespace COPsyncPresenceMap.WPF.NinjectModules
{
    public class ServicesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICOPsyncPresenceMapGenerator>().To<COPsyncPresenceMapGenerator>();
            Bind<ISpreadsheetParser>().To<XlsxSpreadsheetParser>();
            Bind<IMapGraphicParser>().To<SvgMapGraphicParser>();
        }
    }
}
