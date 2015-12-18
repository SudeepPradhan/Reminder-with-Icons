namespace IconsReminder.Converter
{
    using System;
    using Windows.UI.Xaml.Data;

    public class ToUpperCaseCoverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (value as String).ToUpperInvariant();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
