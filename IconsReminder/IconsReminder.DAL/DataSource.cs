namespace IconsReminder.DAL
{
    using Model;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class DataSource
    {
        public static ObservableCollection<IItem> IconsItems { get; set; } = new ObservableCollection<IItem>();
        public static ObservableCollection<IItem> SubscribedItems { get; set; } = new ObservableCollection<IItem>();
        public static ObservableCollection<IItem> ReminderItems { get; set; } = new ObservableCollection<IItem>();
        public static ObservableCollection<IItem> PastReminderItems { get; set; } = new ObservableCollection<IItem>();

        private static ItemService _itemService = new ItemService(new FileService(), new ItemJsonParser());

        public DataSource()
        {
            IconsItems = _itemService.GetTheIconsItems();
            SubscribedItems = _itemService.GetTheSubscribedItemList();
            ReminderItems = _itemService.GetTheRedminderList();
            PastReminderItems = 
                new ObservableCollection<IItem>(_itemService.GetThePastRedminderList().Take<IItem>(50));
        }

        public static void AddSusbcribedItemToDataStorage(IItem item)
        {
            _itemService.AddSubscribedItem(item);
        }

        public static void AddReminderItemToDataStorage(IItem item)
        {
            _itemService.AddReminderItem(item);
        }

        public static void AddPastReminderItemToDataStorage(IItem item)
        {
            _itemService.AddPastReminderItem(item);
        }

        public static void SaveSusbcribedItemsToDataStorage()
        {
            _itemService.SaveSubscribedList(SubscribedItems);
        }

        public static void SaveReminderItemsToDataStorage()
        {
            _itemService.SaveReminderList(ReminderItems);
        }

        public static void SavePastReminderItemsToDataStorage()
        {
            _itemService.SavePastReminderList(PastReminderItems);
        }
    }
}
