using System.Globalization;
using ZATCAMAUI.Core.Enums;

namespace ZATCAMAUI.Core.Converters
{
    public class NewTaxEvasionTabEnumToVisibilityConverter : IValueConverter
    {
        private bool isVisible { get; set; } = false;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            NewTaxEvasionTabEnum _value = (NewTaxEvasionTabEnum)value;
            NewTaxEvasionTabEnum _parameter = (NewTaxEvasionTabEnum)parameter;
            isVisible = _value == _parameter;
            return isVisible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return isVisible;
        }
    }
}
