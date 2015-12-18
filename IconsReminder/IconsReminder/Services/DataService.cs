namespace IconsReminder.Services
{
    using Model;
    using DAL;
    using System.Collections.ObjectModel;

    class DataService : IDataService
    {
        public static DataSource dataSource = null;

        public DataService()
        {
            if (dataSource == null)
            {
                dataSource = new DataSource();
            }
        }

        public ObservableCollection<IItem> GetIconItems()
        {
            return DataSource.IconsItems;
        }

        public ObservableCollection<IItem> GetSubscribedItems()
        {
            return DataSource.SubscribedItems;
        }

        public ObservableCollection<IItem> GetReminderItems()
        {
            return DataSource.ReminderItems;
        }

        public ObservableCollection<IItem> GetPastReminderItems()
        {
            return DataSource.PastReminderItems;
        }

        public void AddItemToSubscribedItemList(IItem item)
        {
            if (!DataSource.SubscribedItems.Contains(item))
            {
                DataSource.SubscribedItems.AddSorted(item);
            }
        }

        public void RemoveItemFromSubscribedItemList(IItem item)
        {
            if (DataSource.SubscribedItems.Contains(item))
            {
                DataSource.SubscribedItems.Remove(item);
            }
        }

        public void AddItemToReminderList(IItem item)
        {
            if (!DataSource.ReminderItems.Contains(item))
            {
                DataSource.ReminderItems.AddSorted(item);
            }
        }

        public void RemoveItemFromReminderList(IItem item)
        {
            if (DataSource.ReminderItems.Contains(item))
            {
                DataSource.ReminderItems.Remove(item);
            }
        }

        public void AddItemToPastReminderList(IItem item)
        {
            if (!DataSource.PastReminderItems.Contains(item))
            {
                DataSource.PastReminderItems.AddSorted(item);
            }
        }

        public void RemoveItemFromPastReminderList(IItem item)
        {
            if (DataSource.PastReminderItems.Contains(item))
            {
                DataSource.PastReminderItems.Remove(item);
            }
        }

        public void SaveSubscribedItems()
        {
            DataSource.SaveSusbcribedItemsToDataStorage();
        }

        public void SaveReminderItems()
        {
            DataSource.SaveReminderItemsToDataStorage();
        }

        public void SavePastReminderItems()
        {
            DataSource.SavePastReminderItemsToDataStorage();
        }
    }
}
