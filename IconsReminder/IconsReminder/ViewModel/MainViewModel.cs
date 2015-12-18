namespace IconsReminder.ViewModel
{
    using Model;
    using Services;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Universal.UI.Xaml.Controls;
    using Utility;
    using Windows.UI.Xaml.Controls;

    public class MainViewModel : ItemBase
    {
        private IDataService dataService;
        private INavigationService navigationService;
        private INotificationService notificationService;

        public static ObservableCollection<IItem> ReminderItems { get; set; }
        public ICommand ItemClickedCommand { get; set; }
        public ICommand ItemSwipedCommand { get; set; }
        public ICommand NavigateAddReminderPageCommand { get; set; }
        public ICommand NavigateAboutPageCommand { get; set; }
        public ICommand NavigatePastReminderPageCommand { get; set; }

        public MainViewModel(
            IDataService dataService, 
            INavigationService navigationService,
            INotificationService notification)
        {
            this.dataService = dataService;
            this.navigationService = navigationService;
            this.notificationService = notification;

            ReminderItems = this.dataService.GetReminderItems();

            LoadCommands();
        }

        private void LoadCommands()
        {
            ItemClickedCommand = new CustomCommand(ItemClickedEventCommand);
            ItemSwipedCommand = new CustomCommand(ItemSwipedEventCommand);

            NavigateAddReminderPageCommand = new CustomCommand((x) => navigationService.NavigateToAddReminderPage());
            NavigateAboutPageCommand = new CustomCommand((x) => navigationService.NavigateToAboutPage());
            NavigatePastReminderPageCommand = new CustomCommand((x) => navigationService.NavigateToPastReminderPage());
        }

        private void ItemClickedEventCommand(object obj)
        {
            ItemClickEventArgs _itemClickEventArgs = obj as ItemClickEventArgs;
            IItem _item = _itemClickEventArgs.ClickedItem as IItem;
            if (_item != null)
            {
                Messenger.Default.Send<IItem>(_item);
                this.navigationService.NavigateToAddNotePage();
            }
        }

        private void ItemSwipedEventCommand(object obj)
        {
            ItemSwipeEventArgs _itemSwipeEventArgs = obj as ItemSwipeEventArgs;
            IItem _item = _itemSwipeEventArgs.SwipedItem as IItem;

            if (_item != null)
            {
                this.notificationService.RemoveTileAndToastNotification(_item.Reminder.NotificationId);
                this.dataService.RemoveItemFromReminderList(_item);

                if (_itemSwipeEventArgs.Direction == SwipeListDirection.Left)
                {
                    _item.Reminder.EnableTimer = false;
                    this.dataService.AddItemToPastReminderList(_item);
                    this.dataService.SavePastReminderItems();
                }
                this.dataService.SaveReminderItems();
            }
        }
    }
}
