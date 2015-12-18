namespace IconsReminder.Converter
{
    using System;
    using Windows.UI.Xaml.Data;

    class RemainingTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null) return String.Empty;

            TimeSpan _dateTime = (TimeSpan)value;

            if (_dateTime < TimeSpan.Zero)
                return "Past the due time.";

            return String.Format("{0}{1}{2}{3}",
                _dateTime.Days == 0 ? String.Empty : _dateTime.Days + "d:",
                _dateTime.Hours == 0 ? String.Empty : _dateTime.Hours + "h:",
                _dateTime.Minutes == 0 ? String.Empty : _dateTime.Minutes + "m:",
                _dateTime.Seconds == 0 ? String.Empty : _dateTime.Seconds + "s");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
