using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using COPsyncPresenceMap.WPF.Helpers;

namespace COPsyncPresenceMap.WPF.Themes
{
    public class ThemeManager : IThemeManager
    {
        private readonly Application _app;
        public ThemeManager(Application app)
        {
            _app = app;
        }

        public static AppTheme CurrentTheme { get; private set; }

        public void SwitchTheme()
        {
            ApplyTheme(GetNextTheme());
        }

        public void ApplyTheme(AppTheme newTheme)
        {
            CurrentTheme = newTheme;
            ThemeDictionary.MergedDictionaries.Clear();
            ThemeDictionary.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri(AppThemes[CurrentTheme], UriKind.Relative) });
        }

        private static readonly Dictionary<AppTheme, string> AppThemes = new Dictionary<AppTheme, string> 
        {
            {AppTheme.Day, "/Themes/DayTheme.xaml"},
            {AppTheme.Night, "/Themes/NightTheme.xaml"}
        };

        private ResourceDictionary ThemeDictionary
        {
            get { return _app.Resources.MergedDictionaries[0]; }
        }

        private AppTheme GetNextTheme()
        {
            var themes =  Enum.GetValues(typeof(AppTheme)).Cast<AppTheme>().ToList();
            var currentThemeIndex = themes.IndexOf(CurrentTheme);
            return themes[(currentThemeIndex + 1)%themes.Count];
        }
    }
}
