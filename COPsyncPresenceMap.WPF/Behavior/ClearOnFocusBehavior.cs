using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace COPsyncPresenceMap.WPF.Behavior
{
    /// <summary>
    /// Attach this behavior when the textbox should be focused everytime it's loaded.
    /// </summary>
    public class ClearOnFocusedBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.GotFocus += GotFocusHandler;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.GotFocus -= GotFocusHandler;
        }

        private void GotFocusHandler(object sender, RoutedEventArgs e)
        {
            AssociatedObject.Text = string.Empty;
        }
    }
}
