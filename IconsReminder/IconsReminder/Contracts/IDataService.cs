namespace IconsReminder.Services
{
    using Model;
    using System.Collections.ObjectModel;

    public interface IDataService
    {
        ObservableCollection<IItem> GetIconItems();
        ObservableCollection<IItem> GetSubscribedItems();
        ObservableCollection<IItem> GetReminderItems();
        ObservableCollection<IItem> GetPastReminderItems();

        void AddItemToSubscribedItemList(IItem item);
        void RemoveItemFromSubscribedItemList(IItem item);

        void AddItemToReminderList(IItem item);
        void RemoveItemFromReminderList(IItem item);

        void AddItemToPastReminderList(IItem item);
        void RemoveItemFromPastReminderList(IItem item);

        void SaveSubscribedItems();
        void SaveReminderItems();
        void SavePastReminderItems();
    }
}
