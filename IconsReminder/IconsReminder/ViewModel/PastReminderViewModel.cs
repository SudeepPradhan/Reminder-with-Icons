namespace IconsReminder
{
    using Model;
    using Services;
    using Utility;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Universal.UI.Xaml.Controls;

    public class PastReminderViewModel : ItemBase
    {
        private IDataService dataService;
        private INavigationService navigationService;
        private INotificationService notificationService;

        public ObservableCollection<IItem> PastReminderItems { get; set; }
        public ICommand ItemSwipedCommand { get; set; }
        public ICommand HomeCommand { get; set; }

        public PastReminderViewModel(
            IDataService dataService,
            INavigationService navigationService,
            INotificationService notification)
        {
            this.dataService = dataService;
            this.navigationService = navigationService;
            this.notificationService = notification;

            PastReminderItems = this.dataService.GetPastReminderItems();

            LoadCommands();
        }

        private void LoadCommands()
        {
            ItemSwipedCommand = new CustomCommand(ItemSwipedEventCommand);
            HomeCommand = new CustomCommand((x) => this.navigationService.NavigateToMainPage());
        }

        private void ItemSwipedEventCommand(object obj)
        {
            ItemSwipeEventArgs _itemSwipeEventArgs = obj as ItemSwipeEventArgs;
            IItem _item = _itemSwipeEventArgs.SwipedItem as IItem;

            if (_item != null)
            {
                 if (_itemSwipeEventArgs.Direction == SwipeListDirection.Right)
                {
                    this.dataService.RemoveItemFromPastReminderList(_item);
                }
                this.dataService.SavePastReminderItems();
            }
        }
    }
}
