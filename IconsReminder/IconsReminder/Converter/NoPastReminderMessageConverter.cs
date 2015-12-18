namespace IconsReminder.Converter
{
    using System;
    using Windows.UI.Xaml.Data;

    public class NoPastReminderMessageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int remindersCount = (int)value;
            if (remindersCount == 0)
                return "You currently do not have any past reminder. Only latest 50 past reminders are saved.";
            return String.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
