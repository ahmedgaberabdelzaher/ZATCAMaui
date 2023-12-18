using System.Globalization;


namespace ZATCAMAUI.Core.Converters
{

    public class BoolToVisibilityConverter : IValueConverter
    {
        private bool isInvertValue { get; set; } = false;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool _value = (bool)value;
            string _parameter = System.Convert.ToString(parameter);

            if (_parameter == "invert")
            {
                isInvertValue = _value ? false : true;
                //System.Diagnostics.Debug.WriteLine("Invert" + "_value {0} _parameter {1} isVisible {2} ", _value, _parameter, isInvertValue);
                return isInvertValue;
            }
            else
            {
                isInvertValue = _value ? true : false;
                //System.Diagnostics.Debug.WriteLine("NonInvert" + "_value {0} _parameter {1} isVisible {2} ", _value, _parameter, isInvertValue);
                return isInvertValue;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return isInvertValue;
        }
    }
    public class ESTOutletDeleteActionVisibilityConverter : IValueConverter
    {
        private bool isVisible { get; set; } = false;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString() != "000";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return isVisible;
        }
    }
}
