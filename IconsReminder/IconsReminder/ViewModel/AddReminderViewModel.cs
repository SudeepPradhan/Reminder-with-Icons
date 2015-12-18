namespace IconsReminder.ViewModel
{
    using Utility;
    using Model;
    using Services;
    using System.Windows.Input;
    using System;
    using Windows.UI.Xaml.Controls;
    using System.Linq;
    using System.Collections.ObjectModel;

    public class AddReminderViewModel
    {
        private IDataService dataService;
        private INavigationService navigationService;
        private INotificationService notification;

        public static ObservableCollection<IItem> SubscribedItems { get; set; }
        public ICommand HourButtonCommand { get; set; }
        public ICommand AddNoteButtonCommand { get; set; }
        public ICommand HomeCommand { get; set; }
        public ICommand NavigateAddMoreItemCommand { get; set; }
        public ICommand RemoveItemCommand { get; set; }

        public AddReminderViewModel(
            IDataService dataService, 
            INavigationService navigationService,
            INotificationService notification)
        {
            this.dataService = dataService;
            this.navigationService = navigationService;
            this.notification = notification;

            SubscribedItems = this.dataService.GetSubscribedItems();
            LoadCommands();
        }

        private void LoadCommands()
        {
            HourButtonCommand = new CustomCommand(HourClickCommand);
            AddNoteButtonCommand = new CustomCommand(AddNoteClickCommand);
            HomeCommand = new CustomCommand((x) => navigationService.NavigateToMainPage());
            NavigateAddMoreItemCommand = new CustomCommand((x) => navigationService.NavigateToAddNewItemsPage());

            RemoveItemCommand = new CustomCommand((x) =>
            {
                IItem _item = x as IItem;
                this.dataService.RemoveItemFromSubscribedItemList(_item);
                this.dataService.SaveSubscribedItems();
            });
        }

        private void HourClickCommand(object param)
        {
            StackPanel panel = (param as Button).Parent as StackPanel;
            var _hoursStackPanel = panel.Children.OfType<StackPanel>().Single(x => x.Name == "HoursStackPanel");
            var _hoursListView = _hoursStackPanel.Children.OfType<ListView>().Single(x => x.Name == "HoursListView");

            IItem _newItem = (panel.DataContext as IItem).DeepCopy();
            var _selectedValue = (string)(_hoursListView.SelectedItem as ListViewItem).Content;

            int hour;
            if (int.TryParse(_selectedValue, out hour))
            {
                _newItem.Reminder = new Reminder();
                _newItem.Reminder.ReminderDateTimeTime += new TimeSpan(hour, 0, 0);

                this.dataService.AddItemToReminderList(_newItem);
                this.dataService.SaveReminderItems();

                this.notification.CreateTileAndToastNotification(_newItem, _newItem.Reminder.ReminderDateTime - DateTime.Now);
                this.navigationService.NavigateToMainPage();
            }
        }

        private void AddNoteClickCommand(object obj)
        {
            IItem _newItem = (obj as IItem).DeepCopy();
            _newItem.Reminder = new Reminder();
            Messenger.Default.Send<IItem>(_newItem);

            this.navigationService.NavigateToAddNotePage();
        }
    }
}
