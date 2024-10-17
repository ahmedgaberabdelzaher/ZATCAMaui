using System.Globalization;

namespace ZATCAMAUI.Core.Converters
{
    public class DateFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string dateString && DateTime.TryParse(dateString, out DateTime dateTime))
            {
                // Use the provided parameter as the format string
                string format = parameter as string ?? "yyyy/MM/dd"; // Default format if parameter is not provided
                return dateTime.ToString(format);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
