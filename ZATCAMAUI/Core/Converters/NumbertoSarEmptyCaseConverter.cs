using System.Globalization;

namespace ZATCAMAUI.Core.Converters
{
    public class NumbertoSarEmptyCaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal)
            {
                var v = (decimal)value;
                return (v == 0) ? " " : v.ToString()+" "+ AppResources.ZSAR;
            }
            else if(value is Int32)
            {
                var v = (Int32)value;
                return (v == 0) ? " " : v.ToString()+ " " + AppResources.ZSAR;
            }
           
            return value.ToString()+ " " + AppResources.ZSAR;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
