using GAZT.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EGAZT.Helper
{
    public class DateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return string.Empty;
            return UtilityManager.FormatDateToYYYYDDMMFromDateTypeString((DateTime)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
