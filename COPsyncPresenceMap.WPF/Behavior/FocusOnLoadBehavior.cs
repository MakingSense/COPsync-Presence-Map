using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace COPsyncPresenceMap.WPF.Behavior
{
    /// <summary>
    /// Attach this behavior when the textbox should be focused everytime it's loaded.
    /// </summary>
    public class FocusOnLoadBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += AssociatedObjectLoaded;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Loaded -= AssociatedObjectLoaded;
        }

        private void AssociatedObjectLoaded(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
                textBox.Focus();
        }
    }
}
