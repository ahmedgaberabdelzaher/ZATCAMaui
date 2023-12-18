using System.Globalization;

namespace ZATCAMAUI.Core.Converters.SyncFusionEnabledConverters
{

    public class StringToColorConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color StatusColor;
            if (value.ToString() == "1" || value.ToString() == "4")
            {
                StatusColor = (Color)Application.Current.Resources["ColorYellow"];
            }
            else if (value.ToString() == "2" || value.ToString() == "8" || value.ToString() == "9")
            {
                StatusColor = (Color)Application.Current.Resources["Primary"];
            }
            else if (value.ToString().Trim() == "3")
            {
                StatusColor = (Color)Application.Current.Resources["StatusColorRed"];
            }
            else if (value.ToString() == "5" || value.ToString() == "7")
            {
                StatusColor = (Color)Application.Current.Resources["StatusColorBlue"];
            }
            else if (value.ToString() == "6")
            {
                StatusColor = (Color)Application.Current.Resources["NeutralGreay"];
            }
            else
            {
                StatusColor = (Color)Application.Current.Resources["ColorYellow"];
            }
            return StatusColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
