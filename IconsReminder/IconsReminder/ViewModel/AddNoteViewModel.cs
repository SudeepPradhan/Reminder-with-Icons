namespace IconsReminder.ViewModel
{
    using Model;
    using Services;
    using System;
    using System.Windows.Input;
    using Utility;

    public class AddNoteViewModel
    {
        private IDataService dataService;
        private INavigationService navigationService;
        private INotificationService notification;

        public static IItem Item { get; set; }
        public ICommand HomeCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public AddNoteViewModel(
            IDataService dataService, 
            INavigationService navigationService,
            INotificationService notification)
        {
            Messenger.Default.Register<IItem>(this, (x) => Item = x);

            this.dataService = dataService;
            this.navigationService = navigationService;
            this.notification = notification;

            LoadCommands();
        }

        private void LoadCommands()
        {
            SaveCommand = new CustomCommand((x) => 
            {
                if (Item != null)
                {
                    this.notification.CreateTileAndToastNotification(Item, Item.Reminder.ReminderDateTime - DateTime.Now);

                    this.dataService.AddItemToReminderList(Item);
                    this.dataService.SaveReminderItems();

                    this.navigationService.NavigateToMainPage();
                }
            });
            HomeCommand = new CustomCommand((x) => navigationService.NavigateToMainPage());
            CancelCommand = new CustomCommand((x) => navigationService.NavigateBack());
        }
    }
}
