using System.Globalization;
using ZATCAMAUI.Core.Mangers;

namespace ZATCAMAUI.Core.Converters
{

    public class CommaSeparatorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && !string.IsNullOrEmpty(value.ToString()))
            {
                string str = UtilityManager.GetCommaSeparatedAmount(value.ToString());
                return str;
            }
            else
            {
                return "0.00";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && !string.IsNullOrEmpty(value.ToString()))
            {
                if (value.ToString().Contains(","))
                    return System.Convert.ToDouble(value.ToString().Replace(",", ""));
                else
                    return System.Convert.ToDouble(value);
            }
            else
            {
                return 0;
            }
        }
    }
}
