using System.Globalization;

namespace ZATCAMAUI.Core.Converters
{
    public class StringToColor : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.Equals("P"))
                return (Color)Application.Current.Resources["Primary"];
            else if (value.Equals("I"))
                return (Color)Application.Current.Resources["Secondary"];
            else
                return (Color)Application.Current.Resources["ErrorColor"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
