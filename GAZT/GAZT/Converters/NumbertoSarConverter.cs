using System;
using System.Globalization;
using Xamarin.Forms;

namespace EGAZT.Converters
{
    public class NumbertoSarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal)
            {
                var v = (decimal)value;
                return (v == 0) ? "" : v.ToString() + " " + AppResources.ZSAR;
            }
            else if (value is int)
            {
                var v = (int)value;
                return (v == 0) ? "" : v.ToString() + " " + AppResources.ZSAR;
            }



            return value.ToString()+ " " + AppResources.ZSAR;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
