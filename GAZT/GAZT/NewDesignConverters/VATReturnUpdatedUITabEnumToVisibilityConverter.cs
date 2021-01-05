using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using EGAZT.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.NewDesignConverters
{
    [Preserve(AllMembers = true)]
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
