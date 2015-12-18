namespace IconsReminder
{
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Primitives;

    public sealed partial class AddNewItems : Page
    {
        public AddNewItems()
        {
            this.InitializeComponent();
        }

        private void TappedHolding(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(sender as StackPanel);
        }
    }
}
