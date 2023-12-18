using System.Globalization;

namespace ZATCAMAUI.Core.Converters
{
    public class ZeroToNullStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            if (value is decimal)
            {
                try
                {
                    var v = (decimal)value;
                    return v == 0 ? "" : v.ToString();
                }
                catch (Exception)
                {
                    Console.Write("");
                }

            }
            else if (value is int v)
            {
                return v == 0 ? null : v.ToString();
            }


            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {// 

            //     Not Tested

            //string strValue = value as string;

            //if (string.IsNullOrEmpty(strValue))
            //    return null;

            //decimal resultInt;

            //if (decimal.TryParse(strValue, out resultInt))
            //{
            //    return resultInt;
            //}
            return value;
        }
    }
}
