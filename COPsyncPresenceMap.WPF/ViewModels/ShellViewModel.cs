using System.Windows.Input;
using Caliburn.Micro;
using COPsyncPresenceMap.WPF.Helpers;

namespace COPsyncPresenceMap.WPF.ViewModels
{
    public class ShellViewModel : Conductor<object>, IShell
    {
        private readonly GeneralViewModel _generalViewModel;

        public ShellViewModel(GeneralViewModel generalViewModel)
        {
            _generalViewModel = generalViewModel;
            ShowGeneral();
        }

        public void ShowGeneral()
        {
            ActivateItem(_generalViewModel);
        }

    }
}
