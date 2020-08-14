using EGAZT.Models;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace EGAZT.NewDesignConverters
{
    public class ZakatForm5TabEnumToVisibilityConverter : IValueConverter
    {
        private bool isVisible { get; set; } = false;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ZakatForm5TabEnum _value = (ZakatForm5TabEnum)value;
            ZakatForm5TabEnum _parameter = (ZakatForm5TabEnum)parameter;
            isVisible = _value == _parameter;
            return isVisible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return isVisible;
        }
    }



    public class FinancialSubTabEnumToVisibilityConverter : IValueConverter
    {
        private bool isVisible { get; set; } = false;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            FinancialSubTabEnum _value = (FinancialSubTabEnum)value;
            FinancialSubTabEnum _parameter = (FinancialSubTabEnum)parameter;
            isVisible = _value == _parameter;
            return isVisible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return isVisible;
        }
    }
}
