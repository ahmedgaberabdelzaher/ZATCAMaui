using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.CustomControl
{
    [Preserve(AllMembers = true)]
    public class StringToColor : IValueConverter
    {
       
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.Equals("1"))
                return Color.FromHex("#006450");
            else if (value.Equals("2"))
                return Color.FromHex("#AA0C19");
            else
                return Color.FromHex("#D99A29");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
