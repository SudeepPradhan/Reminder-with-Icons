namespace IconsReminder
{
    using Windows.UI.Xaml.Controls;
    public sealed partial class HeaderControl : UserControl
    {
        private string _headingName;
        public string HeadingName
        {
            get
            {
                return _headingName;
            }
            set
            {
                HeaderName.Text = value;
            }
        }

        public HeaderControl()
        {
            this.InitializeComponent();
        }
    }
}
