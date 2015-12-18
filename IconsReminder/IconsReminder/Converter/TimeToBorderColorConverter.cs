namespace IconsReminder.Converter
{
    using System;
    using Windows.UI;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Media;

    class TimeToBorderColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null) return new SolidColorBrush(Colors.Black);

            TimeSpan? _timeSpan = (TimeSpan)value;

            if (_timeSpan < TimeSpan.Zero)
                return new SolidColorBrush(Colors.Red);
            else if(_timeSpan == TimeSpan.Zero)
                return new SolidColorBrush(Colors.Gray);
            else
                return new SolidColorBrush(Colors.DeepSkyBlue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
