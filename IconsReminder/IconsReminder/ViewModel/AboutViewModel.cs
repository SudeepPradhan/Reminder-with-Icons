namespace IconsReminder.ViewModel
{
    using Services;
    using System.Windows.Input;
    using Utility;

    public class AboutViewModel
    {
        private INavigationService navigationService;
        public ICommand HomeCommand { get; set; }

        public AboutViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            LoadCommands();
        }

        private void LoadCommands()
        {
            HomeCommand = new CustomCommand((x) => this.navigationService.NavigateToMainPage());
        }
    }
}
