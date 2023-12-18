
using System.Globalization;

namespace ZATCAMAUI.Core.Converters.SyncFusionEnabledConverters
{

    class BooleanToForwardArrowIconStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return "\xe709";
            }
            else
            {
                return "\xe71a";
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
