namespace IconsReminder
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Primitives;

    public sealed partial class AddReminder : Page
    {
        public AddReminder()
        {
            this.InitializeComponent();
        }

        private void TappedHandle(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(sender as FrameworkElement);
        }

        private void StackPanel_Holding(object sender, Windows.UI.Xaml.Input.HoldingRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(sender as FrameworkElement);
        }
    }
}
