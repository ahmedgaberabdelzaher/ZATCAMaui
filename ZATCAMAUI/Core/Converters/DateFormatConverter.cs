using System.Globalization;

namespace ZATCAMAUI.Core.Converters
{
    public class DateFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string dateString && DateTime.TryParse(dateString, out DateTime dateTime))
            {
                // Format the DateTime to yyyy-MM-dd
                return dateTime.ToString("yyyy/MM/dd");
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
