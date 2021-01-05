using EGAZT.Models.EnumModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.NewDesignConverters
{
    [Preserve(AllMembers = true)]
    public class DashBoardModelTabEnumToVisibilityConverter : IValueConverter
    {
        private bool isVisible { get; set; } = false;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DashBoardModelTabEnum _value = (DashBoardModelTabEnum)value;
            DashBoardModelTabEnum _parameter = (DashBoardModelTabEnum)parameter;
            isVisible = _value == _parameter;
            return isVisible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return isVisible;
        }
    }
}
