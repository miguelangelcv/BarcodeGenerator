using System;
using System.Globalization;
using System.Windows.Data;

namespace BarcodeGenerator.WPF.Converters
{
    public class BoolToMaxLenghtConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return 5;
            else
                return 13;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
