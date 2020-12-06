using System;
using System.Globalization;
using Xamarin.Forms;

namespace EGAZT.NewDesignConverters
{
    public class TINDeregistrationAPermitDregRsnTbValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value?.ToString() == "3"); // 1== close and 3== transfer
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
