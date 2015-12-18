namespace IconsReminder.Converter
{
    using System;
    using Windows.UI.Xaml.Data;

    public class ToDateTimeValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime _dateTime = (DateTime)value;

            if (_dateTime < DateTime.Now.Date.Date.AddDays(1))
            {
                return String.Format("Today {0}", _dateTime.ToString("hh:mm tt"));
            }

            if (_dateTime < DateTime.Now.Date.Date.AddDays(2))
            {
                return String.Format("Tomorrow {0}", _dateTime.ToString("hh:mm tt"));
            }

            return _dateTime.ToString(("MM/dd/yy hh:mm tt"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
