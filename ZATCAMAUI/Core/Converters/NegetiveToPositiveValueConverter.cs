using System.Globalization;
using ZATCAMAUI.Core.Mangers;

namespace ZATCAMAUI.Core.Converters
{

    public class NegetiveToPositiveValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && double.Parse(value.ToString()) < 0)
            {
                var absValue = Math.Abs(double.Parse(value.ToString()));
                string str = UtilityManager.GetCommaSeparatedAmount(absValue.ToString());
                return str;
            }
            else
                return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
