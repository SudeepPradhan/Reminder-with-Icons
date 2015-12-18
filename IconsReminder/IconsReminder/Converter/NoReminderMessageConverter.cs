namespace IconsReminder.Converter
{
    using System;
    using Windows.UI.Xaml.Data;

    public class NoReminderMessageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int remindersCount = (int)value;
            if (remindersCount == 0)
                return "You currently do not have any reminder. Click on + button to add new reminder.";
            return String.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
