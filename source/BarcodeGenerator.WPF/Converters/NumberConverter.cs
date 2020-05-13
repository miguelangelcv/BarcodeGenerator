using System;
using System.Globalization;
using System.Windows.Data;

namespace BarcodeGenerator.WPF.Converters
{
    public class NumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = value as string;
            bool isNumeric = true;
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                {
                    isNumeric = false;
                    break;
                }
            }

            if (isNumeric)
                return str;
            else
                return str.Remove(str.Length - 1);
        }
    }
}
