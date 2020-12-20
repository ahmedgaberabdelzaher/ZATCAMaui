using System;
using System.Globalization;
using Xamarin.Forms;

namespace EGAZT.CustomControl
{
    public class StringToColor : IValueConverter
    {
       
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.Equals("P"))
                return (Color)App.Current.Resources["Primary"];
            else if (value.Equals("I"))
                return (Color)App.Current.Resources["Secondary"];
            else
                return (Color)App.Current.Resources["ErrorColor"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
