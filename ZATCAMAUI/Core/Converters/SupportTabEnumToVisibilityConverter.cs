using System.Globalization;
using ZATCAMAUI.Core.Enums;

namespace ZATCAMAUI.Core.Converters
{
    class SupportTabEnumToVisibilityConverter : IValueConverter
    {
        private bool isVisible { get; set; } = false;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SupportTabEnum _value = (SupportTabEnum)value;
            SupportTabEnum _parameter = (SupportTabEnum)parameter;
            isVisible = _value == _parameter;
            return isVisible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return isVisible;
        }
    }
}
