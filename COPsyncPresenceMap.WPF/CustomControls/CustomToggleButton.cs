using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace COPsyncPresenceMap.WPF.CustomControls
{
    public class CustomToggleButton : ToggleButton
    {
        static CustomToggleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomToggleButton), new FrameworkPropertyMetadata(typeof(CustomToggleButton)));
        }

        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }
        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(ImageSource), typeof(CustomToggleButton), new PropertyMetadata(null));

        public ImageSource ImageActive
        {
            get { return (ImageSource)GetValue(ImageActiveProperty); }
            set { SetValue(ImageActiveProperty, value); }
        }
        public static readonly DependencyProperty ImageActiveProperty = DependencyProperty.Register("ImageActive", typeof(ImageSource), typeof(CustomToggleButton), new PropertyMetadata(null));

        public double ImageHeight
        {
            get { return (double)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }
        public static readonly DependencyProperty ImageHeightProperty = DependencyProperty.Register("ImageHeight", typeof(double), typeof(CustomToggleButton), new PropertyMetadata(Double.NaN));

        public double ImageWidth
        {
            get { return (double)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }
        public static readonly DependencyProperty ImageWidthProperty = DependencyProperty.Register("ImageWidth", typeof(double), typeof(CustomToggleButton), new PropertyMetadata(Double.NaN));
        public Brush OnActiveBackground
        {
            get { return (Brush)GetValue(OnActiveBackgroundProperty); }
            set { SetValue(OnActiveBackgroundProperty, value); }
        }
        public static readonly DependencyProperty OnActiveBackgroundProperty = DependencyProperty.Register("OnActiveBackground", typeof(Brush), typeof(CustomToggleButton), new PropertyMetadata(null));

    }
}
