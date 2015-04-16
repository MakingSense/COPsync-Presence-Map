namespace COPsyncPresenceMap.WPF.Helpers
{
    public enum AppTheme
    {
        Day,
        Night
    }

    public interface IThemeManager
    {
        void ApplyTheme(AppTheme newTheme);
        void SwitchTheme();
    }
}
