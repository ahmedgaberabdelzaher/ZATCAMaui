using System.Globalization;
using ZATCAMAUI.Core.Enums;

namespace ZATCAMAUI.Core.Converters
{

    class EstablishmentSignUPTabEnumToVisibilityConverters : IValueConverter
    {
        private bool isVisible { get; set; } = false;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            EstablishmentSignUPTabEnum _value = (EstablishmentSignUPTabEnum)value;
            EstablishmentSignUPTabEnum _parameter = (EstablishmentSignUPTabEnum)parameter;
            isVisible = _value == _parameter;
            return isVisible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return isVisible;
        }
    }
}
