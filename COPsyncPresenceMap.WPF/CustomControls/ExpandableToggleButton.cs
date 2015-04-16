using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace COPsyncPresenceMap.WPF.CustomControls
{
    public class ExpandableToggleButton :  ToggleButton
    {
        static ExpandableToggleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExpandableToggleButton), new FrameworkPropertyMetadata(typeof(ExpandableToggleButton)));
        }

        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }
        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(ImageSource), typeof(ExpandableToggleButton), new PropertyMetadata(null));

        public double ImageHeight
        {
            get { return (double)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }
        public static readonly DependencyProperty ImageHeightProperty = DependencyProperty.Register("ImageHeight", typeof(double), typeof(ExpandableToggleButton), new PropertyMetadata(Double.NaN));

        public double ImageWidth
        {
            get { return (double)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }
        public static readonly DependencyProperty ImageWidthProperty = DependencyProperty.Register("ImageWidth", typeof(double), typeof(ExpandableToggleButton), new PropertyMetadata(Double.NaN));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(String), typeof(ExpandableToggleButton), new PropertyMetadata(string.Empty));
        public Dock ImagePosition
        {
            get { return (Dock)GetValue(ImagePositionProperty); }
            set { SetValue(ImagePositionProperty, value); }
        }
        public static readonly DependencyProperty ImagePositionProperty = DependencyProperty.Register("ImagePosition", typeof(Dock), typeof(ExpandableToggleButton), new PropertyMetadata(null));

        public int ImageRotation
        {
            get { return (int)GetValue(ImageRotationProperty); }
            set { SetValue(ImageRotationProperty, value); }
        }
        public static readonly DependencyProperty ImageRotationProperty = DependencyProperty.Register("ImageRotation", typeof(int), typeof(ExpandableToggleButton), new PropertyMetadata(0));

    }
}
