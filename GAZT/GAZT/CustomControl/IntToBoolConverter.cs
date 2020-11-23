using System;
using System.Globalization;
using Xamarin.Forms;

namespace EGAZT.CustomControl
{
    public class IntToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return false;
            return value.ToString().Length != 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return false;
            return value.ToString().Length != 0;
        }
    }
}
