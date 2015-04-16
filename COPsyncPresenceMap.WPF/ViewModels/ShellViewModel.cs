using System.Windows.Input;
using Caliburn.Micro;
using COPsyncPresenceMap.WPF.Helpers;

namespace COPsyncPresenceMap.WPF.ViewModels
{
    public class ShellViewModel : Conductor<object>, IShell
    {
        private readonly GeneralViewModel _generalViewModel;
        private readonly IThemeManager _themeManager;

        public ShellViewModel(GeneralViewModel generalViewModel, IThemeManager themeManager)
        {
            _generalViewModel = generalViewModel;
            _themeManager = themeManager;
            _switchThemeCommand = new RelayCommand(param => { _themeManager.SwitchTheme(); });
            ShowGeneral();
        }

        public void ShowGeneral()
        {
            ActivateItem(_generalViewModel);
        }

        public ICommand SwitchTheme
        {
            get { return _switchThemeCommand; }
        }

        private readonly ICommand _switchThemeCommand;
    }
}