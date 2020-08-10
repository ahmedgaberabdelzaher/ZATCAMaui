using System;
using System.Globalization;
using EGAZT.Models;
using Xamarin.Forms;

namespace EGAZT.NewDesignConverters
{
    public class EstablishmentTabsEnumToVisibilityConverter : IValueConverter
    {
        private bool isVisible { get; set; } = false;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            EstablishmentRegistrationTabsEnum _value = (EstablishmentRegistrationTabsEnum)value;
            EstablishmentRegistrationTabsEnum _parameter = (EstablishmentRegistrationTabsEnum)parameter;
            isVisible = _value == _parameter;
            return isVisible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return isVisible;
        }
    }
}
