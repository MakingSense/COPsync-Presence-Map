using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace COPsyncPresenceMap.WPF.CustomControls
{
    public class PanelButton : RadioButton
    {
        static PanelButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PanelButton), new FrameworkPropertyMetadata(typeof(PanelButton)));
        }

        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }
        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(ImageSource), typeof(PanelButton), new PropertyMetadata(null));

        public double ImageHeight
        {
            get { return (double)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }
        public static readonly DependencyProperty ImageHeightProperty = DependencyProperty.Register("ImageHeight", typeof(double), typeof(PanelButton), new PropertyMetadata(Double.NaN));

        public double ImageWidth
        {
            get { return (double)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }
        public static readonly DependencyProperty ImageWidthProperty = DependencyProperty.Register("ImageWidth", typeof(double), typeof(PanelButton), new PropertyMetadata(Double.NaN));

        public bool Uncheckable
        {
            get { return (bool)GetValue(UncheckableProperty); }
            set { SetValue(UncheckableProperty, value); }
        }
        public static readonly DependencyProperty UncheckableProperty = DependencyProperty.Register("Uncheckable", typeof(bool), typeof(PanelButton), new PropertyMetadata(false));

        public ImageSource OnActiveImage
        {
            get { return (ImageSource)GetValue(OnActiveImageProperty); }
            set { SetValue(OnActiveImageProperty, value); }
        }
        public static readonly DependencyProperty OnActiveImageProperty = DependencyProperty.Register("OnActiveImage", typeof(ImageSource), typeof(PanelButton), new PropertyMetadata(null));

        public Brush OnActiveBackground
        {
            get { return (Brush)GetValue(OnActiveBackgroundProperty); }
            set { SetValue(OnActiveBackgroundProperty, value); }
        }
        public static readonly DependencyProperty OnActiveBackgroundProperty = DependencyProperty.Register("OnActiveBackground", typeof(Brush), typeof(PanelButton), new PropertyMetadata(new SolidColorBrush(Colors.White)));

        public Brush OnActiveForeground
        {
            get { return (Brush)GetValue(OnActiveForegroundProperty); }
            set { SetValue(OnActiveForegroundProperty, value); }
        }
        public static readonly DependencyProperty OnActiveForegroundProperty = DependencyProperty.Register("OnActiveForeground", typeof(Brush), typeof(PanelButton), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2E6A9F"))));

        public Visibility BubbleVisivility
        {
            get { return (Visibility)GetValue(BubbleVisivilityProperty); }
            set { SetValue(BubbleVisivilityProperty, value); }
        }
        public static readonly DependencyProperty BubbleVisivilityProperty = DependencyProperty.Register("BubbleVisivility", typeof(Visibility), typeof(PanelButton), new PropertyMetadata(Visibility.Collapsed));

        public string BubbleNumber
        {
            get { return (string)GetValue(BubbleNumberProperty); }
            set { SetValue(BubbleNumberProperty, value); }
        }
        public static readonly DependencyProperty BubbleNumberProperty = DependencyProperty.Register("BubbleNumber", typeof(string), typeof(PanelButton), new PropertyMetadata(null));

        public Brush BubbleBackground
        {
            get { return (Brush)GetValue(BubbleBackgroundProperty); }
            set { SetValue(BubbleBackgroundProperty, value); }
        }
        public static readonly DependencyProperty BubbleBackgroundProperty = DependencyProperty.Register("BubbleBackground", typeof(Brush), typeof(PanelButton), new PropertyMetadata(null));

        public Brush BubbleForeground
        {
            get { return (Brush)GetValue(BubbleForegroundProperty); }
            set { SetValue(BubbleForegroundProperty, value); }
        }
        public static readonly DependencyProperty BubbleForegroundProperty = DependencyProperty.Register("BubbleForeground", typeof(Brush), typeof(PanelButton), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public string SubText
        {
            get { return (string)GetValue(SubTextProperty); }
            set { SetValue(SubTextProperty, value); }
        }
        public static readonly DependencyProperty SubTextProperty = DependencyProperty.Register("SubText", typeof(string), typeof(PanelButton), new PropertyMetadata(string.Empty));

        protected override void OnClick()
        {
            bool oldValue = IsChecked ?? false;
            base.OnClick();
            if (Uncheckable && oldValue /*&& !oldValue.Equals(IsChecked)*/)
                IsChecked = false;
        }
    }
}
