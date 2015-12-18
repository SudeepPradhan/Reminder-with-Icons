namespace IconsReminder.Tests
{
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
    using Model;
    using System;
    using System.Collections.ObjectModel;

    [TestClass]
    public class ItemJsonParserTests
    {
        string jsonString = "{\"Items\":[{\"ImageName\":\"email.png\",\"Reminder\":{\"ReminderDateTimeValue\":\"12/8/2015 10:49:05 AM\",\"ReminderDescription\":\"- Not Note -\"}}]}";

        [TestMethod]
        public void CreateItemCollectionTest()
        {
            ItemJsonParser _itemJsonParser = new ItemJsonParser();
            ObservableCollection<IItem> items = _itemJsonParser.CreateItemCollection(jsonString);

            Assert.AreEqual("email.png", items[0].ImageName);
        }

        [TestMethod]
        public void VerifyStringifyTest()
        {
            ItemJsonParser _itemJsonParser = new ItemJsonParser();
            Item _item = new Item("Email", "email.png");
            _item.Reminder = new Reminder();
            _item.Reminder.ReminderDateTime = DateTime.Parse("12/8/2015 10:49:05 AM");
            _item.Reminder.ReminderDateTimeTime = DateTime.Parse("12/8/2015 10:49:05 AM").TimeOfDay;
            _item.Reminder.ReminderDescription = "- Not Note -";
           ObservableCollection<IItem> _collection = new ObservableCollection<IItem>();
            _collection.Add(_item);
            string _stringify = _itemJsonParser.Stringify(_collection);

            Assert.AreEqual(_stringify, jsonString);
        }

        [TestMethod]
        public void ReminderDateTimeTest()
        {

        }
    }
}
