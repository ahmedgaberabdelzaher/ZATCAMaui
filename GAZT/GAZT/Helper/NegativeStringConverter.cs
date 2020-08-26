using System;
using System.Globalization;
using Xamarin.Forms;

namespace EGAZT.Helper
{
    public class NegativeStringConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                value = (value as string).Replace("-", string.Empty);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
