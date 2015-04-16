using Ninject.Modules;
using COPsyncPresenceMap.WPF.Services;
using COPsyncPresenceMap.WPF.Services.Interfaces;

namespace COPsyncPresenceMap.WPF.NinjectModules
{
    public class ServicesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPainterService>().To<PainterService>();
        }
    }
}
