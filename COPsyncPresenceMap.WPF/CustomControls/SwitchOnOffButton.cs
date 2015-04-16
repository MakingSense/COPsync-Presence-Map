using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace COPsyncPresenceMap.WPF.CustomControls
{
    public class SwitchOnOffButton : ToggleButton
    {
        static SwitchOnOffButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SwitchOnOffButton), new FrameworkPropertyMetadata(typeof(SwitchOnOffButton)));
        }

        public Brush OnBackground
        {
            get { return (Brush)GetValue(OnBackgroundProperty); }
            set { SetValue(OnBackgroundProperty, value); }
        }

        public static readonly DependencyProperty OnBackgroundProperty = DependencyProperty.Register("OnBackground", typeof(Brush), typeof(SwitchOnOffButton), new PropertyMetadata(null));

        public Brush OnSwitchColor
        {
            get { return (Brush)GetValue(OnSwitchColorProperty); }
            set { SetValue(OnSwitchColorProperty, value); }
        }

        public Brush OnForeground
        {
            get { return (Brush)GetValue(OnForegroundProperty); }
            set { SetValue(OnForegroundProperty, value); }
        }

        public static readonly DependencyProperty OnForegroundProperty = DependencyProperty.Register("OnForeground", typeof(Brush), typeof(SwitchOnOffButton), new PropertyMetadata(null));

        public static readonly DependencyProperty OnSwitchColorProperty = DependencyProperty.Register("OnSwitchColor", typeof(Brush), typeof(SwitchOnOffButton), new PropertyMetadata(null));

        public Brush OffBackground
        {
            get { return (Brush)GetValue(OffBackgroundProperty); }
            set { SetValue(OffBackgroundProperty, value); }
        }

        public static readonly DependencyProperty OffBackgroundProperty = DependencyProperty.Register("OffBackground", typeof(Brush), typeof(SwitchOnOffButton), new PropertyMetadata(null));

        public Brush OffSwitchColor
        {
            get { return (Brush)GetValue(OffSwitchColorProperty); }
            set { SetValue(OffSwitchColorProperty, value); }
        }

        public static readonly DependencyProperty OffSwitchColorProperty = DependencyProperty.Register("OffSwitchColor", typeof(Brush), typeof(SwitchOnOffButton), new PropertyMetadata(null));

        public Brush OffForeground
        {
            get { return (Brush)GetValue(OffForegroundProperty); }
            set { SetValue(OffForegroundProperty, value); }
        }

        public static readonly DependencyProperty OffForegroundProperty = DependencyProperty.Register("OffForeground", typeof(Brush), typeof(SwitchOnOffButton), new PropertyMetadata(null));
    }
}
