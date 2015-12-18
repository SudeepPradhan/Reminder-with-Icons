namespace IconsReminder.Model
{
    using System;
    using System.Collections.ObjectModel;
    using Windows.Data.Json;

    public class ItemJsonParser : IItemJsonParser
    {
        private const string jsonImageName = "ImageName";
        private const string jsonItemName = "ItemName";
        private const string jsonReminder = "Reminder";
        private const string jsonDateTimeValue = "ReminderDateTimeValue";
        private const string jsonReminderDescription = "ReminderDescription";
        private const string jsonReminderEnableTimer = "EnableTimer";
        private const string jsonReminderNotificationId = "NotificationId";
        private const string jsonItems = "Items";

        public ObservableCollection<IItem> CreateItemCollection(string jsonString)
        {
            ObservableCollection<IItem> items = new ObservableCollection<IItem>();
            JsonObject jsonObject;

            if (JsonObject.TryParse(jsonString, out jsonObject))
            {
                foreach (var jsonValue in jsonObject.GetNamedArray(jsonItems, new JsonArray()))
                {
                    if (jsonValue.ValueType == JsonValueType.Object)
                    {
                        items.Add(GetItem(jsonValue.GetObject()));
                    }
                }
            }
            return items;
        }

        public string Stringify(ObservableCollection<IItem> items)
        {
            JsonObject jsonObject = new JsonObject();
            JsonArray jsonArray = new JsonArray();
            foreach (var item in items)
            {
                jsonArray.Add(ItemToJsonObject(item));
            }
            jsonObject[jsonItems] = jsonArray;
            return jsonObject.Stringify();
        }

        public string Stringify(IItem item)
        {
            JsonObject jsonObject = new JsonObject();
            jsonObject[jsonItems] = ItemToJsonObject(item);
            return jsonObject.Stringify();
        }


        private IItem GetItem(JsonObject jsonObject)
        {
            Item item = new Item(
                jsonObject.GetNamedString(jsonItemName, ""),
                jsonObject.GetNamedString(jsonImageName, ""));
            JsonObject jsonValue = jsonObject.GetNamedObject(jsonReminder, null);
            if (jsonValue != null)
            {
                item.Reminder = GetReminder(jsonValue);
            }
            return item;
        }

        private IReminder GetReminder(JsonObject jsonObject)
        {
            bool enableTimer = true;
            bool.TryParse(jsonObject.GetNamedString(jsonReminderEnableTimer, ""), out enableTimer);

            Reminder reminder = new Reminder(enableTimer);
            DateTime _dateTime = new DateTime();

            if (DateTime.TryParse(jsonObject.GetNamedString(jsonDateTimeValue, ""), out _dateTime))
            {
                reminder.ReminderDateTime = _dateTime;
                reminder.ReminderDateTimeTime = _dateTime.TimeOfDay;
            }
            reminder.ReminderDescription = jsonObject.GetNamedString(jsonReminderDescription, "");
            reminder.NotificationId = jsonObject.GetNamedString(jsonReminderNotificationId, "");

            return reminder;
        }

        private JsonObject ItemToJsonObject(IItem item)
        {
            JsonObject jsonObject = new JsonObject();
            jsonObject[jsonImageName] = JsonValue.CreateStringValue(item.ImageName);
            jsonObject[jsonItemName] = JsonValue.CreateStringValue(item.Title);
            if (item.Reminder != null)
            {
                jsonObject[jsonReminder] = ReminderToJsonObject(item.Reminder);
            }
            return jsonObject;
        }

        private JsonObject ReminderToJsonObject(IReminder reminder)
        {
            JsonObject jsonObject = new JsonObject();
            jsonObject[jsonDateTimeValue] = JsonValue.CreateStringValue(
                reminder.ReminderDateTime.Date.Add(reminder.ReminderDateTimeTime).ToString());
            jsonObject[jsonReminderDescription] = JsonValue.CreateStringValue(reminder.ReminderDescription);
            jsonObject[jsonReminderEnableTimer] = JsonValue.CreateStringValue(reminder.EnableTimer.ToString());
            jsonObject[jsonReminderNotificationId] = JsonValue.CreateStringValue(reminder.NotificationId);

            return jsonObject;
        }
    }
}
