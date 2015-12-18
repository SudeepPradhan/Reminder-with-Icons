namespace IconsReminder.Converter
{
    using System;
    using Windows.UI.Xaml.Data;

    public class DateTimeToDateTimeOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime date = (DateTime)value;
            return new DateTimeOffset(date);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            DateTimeOffset dto = (DateTimeOffset)value;
            return dto.DateTime;
        }
    }
}
