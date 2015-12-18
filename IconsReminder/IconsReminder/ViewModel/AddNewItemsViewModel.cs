namespace IconsReminder.ViewModel
{
    using Utility;
    using Model;
    using Services;
    using System.Windows.Input;
    using Windows.UI.Xaml.Controls;
    using System.Linq;
    using System.Collections.ObjectModel;

    public class AddNewItemsViewModel : ItemBase
    {
        private IDataService dataService;
        private INavigationService navigationService;

        public static ObservableCollection<IItem> SubscribedItems { get; set; }
        public static ObservableCollection<IItem> UnsubscribedItems { get; set; }
        public ICommand HomeCommand { get; set; }
        public ICommand AddNewItemCommand { get; set; }

        public AddNewItemsViewModel(
            IDataService dataService, 
            INavigationService navigationService)
        {
            this.dataService = dataService;
            this.navigationService = navigationService;

            SubscribedItems = this.dataService.GetSubscribedItems();
            UnsubscribedItems = this.dataService.GetIconItems();

            LoadCommands();
        }

        private void LoadCommands()
        {
            HomeCommand = new CustomCommand((x) =>
            {
                this.navigationService.NavigateToMainPage();
            });

            AddNewItemCommand = new CustomCommand((param) =>
            {
                StackPanel panel = (param as Button).Parent as StackPanel;
                string value = panel.Children.OfType<TextBox>().Single(x => x.Name == "NewItemName").Text;
                IItem _item = (panel.DataContext as IItem).DeepCopy();
                _item.Title = value;
                this.dataService.AddItemToSubscribedItemList(_item);
                this.dataService.SaveSubscribedItems();
                this.navigationService.NavigateToAddReminderPage();
            });
        }
    }
}
