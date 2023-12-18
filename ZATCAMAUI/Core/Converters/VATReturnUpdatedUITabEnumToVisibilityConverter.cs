using System.Globalization;
using ZATCAMAUI.Models;

namespace ZATCAMAUI.Core.Converters
{

    public class VATReturnUpdatedUITabEnumToVisibilityConverter : IValueConverter
    {
        private bool isVisible { get; set; } = false;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            VATReturnUpdatedUITabEnum _value = (VATReturnUpdatedUITabEnum)value;
            VATReturnUpdatedUITabEnum _parameter = (VATReturnUpdatedUITabEnum)parameter;
            isVisible = _value == _parameter;
            return isVisible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return isVisible;
        }
    }
}
