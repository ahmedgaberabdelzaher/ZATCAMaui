using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.CustomControl
{
    [Preserve(AllMembers = true)]
    public class StringToColorConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color StatusColor;
            if (value.ToString() == "1" || value.ToString() == "4")
            {
                StatusColor = Color.FromHex("#E39800");
            }
            else if (value.ToString() == "2" || value.ToString() == "8" || value.ToString() == "9")
            {
                StatusColor = Color.FromHex("#006450");
            }
            else if (value.ToString().Trim() == "3")
            {
                StatusColor = Color.FromHex("#E52027");
            }
            else if (value.ToString() == "5"|| value.ToString() == "7")
            {
                StatusColor = Color.FromHex("#39679A");
            }
            else if (value.ToString() == "6")
            {
                StatusColor = Color.FromHex("#999999");
            }
            else
            {
                StatusColor = Color.FromHex("#E39800");
            }
            return StatusColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
