namespace IconsReminder.DAL
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Windows.Storage;

    public interface IFileService
    {
        string ImageFolder { get; set; }
        string DefaultSubscribedListFile { get; set; }
        string SubscribedListFile { get; set; }
        string ReminderListFile { get; set; }
        string PastReminderListFile { get; set; }

        Task<IReadOnlyList<StorageFile>> GetImageFiles();

        string ReadSubscribedItemListFile();
        string ReadReminderListFile();
        string ReadPastReminderListFile();

        Task AddSubscribedItemToSubscribedItemListFile(string item);
        Task AddReminderToReminderListFile(string item);
        Task AddPastReminderToPastReminderListFile(string item);

        Task SaveSubscribedItemsToSubscribedItemListFile(string items);
        Task SaveRemindersToReminderListFile(string items);
        Task SavePastRemindersItemsToPastReminderListFile(string items);

        Task<List<string>> GetItemsFromDefaultSubscribedItemListFile();
    }
}
