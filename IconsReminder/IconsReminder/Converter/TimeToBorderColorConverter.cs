namespace IconsReminder.Converter
{
    using System;
    using Windows.Foundation;
    using Windows.UI;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Media;

    class TimeToBorderColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null) return new SolidColorBrush(Colors.Black);

            TimeSpan? _timeSpan = (TimeSpan)value;

            if (_timeSpan < TimeSpan.Zero)
                return Application.Current.Resources["IRBorderColorRed"] as LinearGradientBrush;
            else if(_timeSpan == TimeSpan.Zero)
                return new SolidColorBrush(Colors.Gray); 
            else
                return Application.Current.Resources["IRBorderColor"] as LinearGradientBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
