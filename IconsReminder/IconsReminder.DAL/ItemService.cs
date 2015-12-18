namespace IconsReminder.DAL
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Model;

    internal class ItemService
    {
        private static IFileService _fileService;
        private static IItemJsonParser _itemJsonParser;

        internal ItemService(IFileService fileService, IItemJsonParser itemJsonParser)
        {
            _fileService = fileService;
            _itemJsonParser = itemJsonParser;
        }

        internal ObservableCollection<IItem> GetTheIconsItems()
        {
            var _imageFiles = _fileService.GetImageFiles().Result;
            var _items = new List<IItem>();
            foreach (var file in _imageFiles)
            {
                _items.Add(new Item(file.Name, file.Name));
            }
            return new ObservableCollection<IItem>(_items);
        }

        internal ObservableCollection<IItem> GetTheSubscribedItemList()
        {
            var _subscribedList = _fileService.ReadSubscribedItemListFile();
            if(_subscribedList.Length != 0)
                return _itemJsonParser.CreateItemCollection(_subscribedList);

            var _defaultSubscribedItems = _fileService.GetItemsFromDefaultSubscribedItemListFile().Result;
            var _items = new List<IItem>();
            foreach (var _item in _defaultSubscribedItems)
            {
                _items.Add(new Item(_item, _item));
            }
            return new ObservableCollection<IItem>(_items);
        }

        internal ObservableCollection<IItem> GetTheRedminderList()
        {
            var reminderList = _fileService.ReadReminderListFile();
            return _itemJsonParser.CreateItemCollection(reminderList);
        }

        internal ObservableCollection<IItem> GetThePastRedminderList()
        {
            var _pastReminderList = _fileService.ReadPastReminderListFile();
            return _itemJsonParser.CreateItemCollection(_pastReminderList);
        }

        internal async void AddSubscribedItem(IItem _item)
        {
            await _fileService.AddSubscribedItemToSubscribedItemListFile(_itemJsonParser.Stringify(_item));
        }

        internal async void AddReminderItem(IItem _item)
        {
            await _fileService.AddReminderToReminderListFile(_itemJsonParser.Stringify(_item));
        }

        internal async void AddPastReminderItem(IItem _item)
        {
            await _fileService.AddPastReminderToPastReminderListFile(_itemJsonParser.Stringify(_item));
        }

        internal async void SaveSubscribedList(ObservableCollection<IItem> _items)
        {
            await _fileService.SaveSubscribedItemsToSubscribedItemListFile(_itemJsonParser.Stringify(_items));
        }

        internal async void SaveReminderList(ObservableCollection<IItem> _items)
        {
            await _fileService.SaveRemindersToReminderListFile(_itemJsonParser.Stringify(_items));
        }

        internal async void SavePastReminderList(ObservableCollection<IItem> _items)
        {
            await _fileService.SavePastRemindersItemsToPastReminderListFile(_itemJsonParser.Stringify(_items));
        }
    }
}
