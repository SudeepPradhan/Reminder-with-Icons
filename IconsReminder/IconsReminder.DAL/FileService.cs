namespace IconsReminder.DAL
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Windows.ApplicationModel;
    using Windows.Storage;

    public class FileService : IFileService
    {
        public string ImageFolder { get; set; } = "IconsReminder.DAL\\Images";
        public string DefaultSubscribedListFile { get; set; } = "IconsReminder.DAL\\DefaultItemList.txt";
        public string SubscribedListFile { get; set; } = "SubscribedItemList.txt";
        public string ReminderListFile { get; set; } = "ReminderList.txt";
        public string PastReminderListFile { get; set; } = "PastReminderList.txt";

        public async Task<IReadOnlyList<StorageFile>> GetImageFiles()
        {
            var _itemIconFolder = await StorageFolder.GetFolderFromPathAsync(
                Path.Combine(
                    Package.Current.InstalledLocation.Path,
                    this.ImageFolder)).AsTask().ConfigureAwait(false);
            return await _itemIconFolder.GetFilesAsync().AsTask().ConfigureAwait(false);
        }

        #region Read file
        public string ReadSubscribedItemListFile()
        {
            return ReadItemListFile(this.SubscribedListFile).Result;
        }

        public string ReadReminderListFile()
        {
            return ReadItemListFile(this.ReminderListFile).Result;
        }

        public string ReadPastReminderListFile()
        {
            return ReadItemListFile(this.PastReminderListFile).Result;
        }

        public async Task<string> ReadItemListFile(string fileName)
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync(
                fileName,
                CreationCollisionOption.OpenIfExists).AsTask().ConfigureAwait(false);
            var _items = await ApplicationData.Current.LocalFolder.GetFileAsync(
                fileName).AsTask().ConfigureAwait(false);
            return await FileIO.ReadTextAsync(_items).AsTask().ConfigureAwait(false);
        }

        #endregion

        #region Save file
        public async Task SaveSubscribedItemsToSubscribedItemListFile(string items)
        {
            await SaveItemsToListFile(items, this.SubscribedListFile);
        }

        public async Task SaveRemindersToReminderListFile(string items)
        {
            await SaveItemsToListFile(items, this.ReminderListFile);
        }

        public async Task SavePastRemindersItemsToPastReminderListFile(string items)
        {
            await SaveItemsToListFile(items, this.PastReminderListFile);
        }

        public async Task SaveItemsToListFile(string items, string fileName)
        {
            var _file = await ApplicationData.Current.LocalFolder.GetFileAsync(
                fileName).AsTask().ConfigureAwait(false);
            if (_file != null)
            {
                await FileIO.WriteTextAsync(_file, items).AsTask().ConfigureAwait(false);
            }
        }

        #endregion

        #region Append Item
        public async Task AddSubscribedItemToSubscribedItemListFile(string item)
        {
            await AppendItemToFile(item, this.SubscribedListFile);
        }

        public async Task AddReminderToReminderListFile(string item)
        {
            await AppendItemToFile(item, this.ReminderListFile);
        }

        public async Task AddPastReminderToPastReminderListFile(string item)
        {
            await AppendItemToFile(item, this.PastReminderListFile);
        }

        public async Task AppendItemToFile(string item, string fileName)
        {
            var _file = await ApplicationData.Current.LocalFolder.GetFileAsync(
                fileName).AsTask().ConfigureAwait(false);
            if (_file != null)
            {
                await FileIO.AppendTextAsync(_file, item).AsTask().ConfigureAwait(false);
            }
        }

        #endregion

        public async Task<List<string>> GetItemsFromDefaultSubscribedItemListFile()
        {
            var _itemDefaultFile = await Package.Current.InstalledLocation.GetFileAsync(
                this.DefaultSubscribedListFile).AsTask().ConfigureAwait(false);
            var _defaultSubscribedItems = await FileIO.ReadTextAsync(_itemDefaultFile).AsTask().ConfigureAwait(false);
            return _defaultSubscribedItems.Split('\n').Select(t => t.Trim()).ToList();
        }
    }
}
