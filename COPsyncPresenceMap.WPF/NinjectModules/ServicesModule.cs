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
            Bind<COPsyncPresenceMapApplication>().To<COPsyncPresenceMapApplication>();
            Bind<ISvgPainterFactory>().To<SvgPainterFactory>();
            Bind<ISpreadsheetParser>().To<XlsxParser>();
            Bind<ISvgReaderFactory>().To<SvgReaderFactory>();
        }
    }
}
