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
            //System.Diagnostics.Debug.WriteLine("_value {0} _parameter {1} isVisible {2} ", _value, _parameter, isVisible);
            return isVisible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return isVisible;
        }
    }

    public class EstablishmentRegistrationOutletTabsEnumToVisibilityConverter : IValueConverter
    {
        private bool isVisible { get; set; } = false;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            EstablishmentRegistrationOutletTabsEnum _value = (EstablishmentRegistrationOutletTabsEnum)value;
            EstablishmentRegistrationOutletTabsEnum _parameter = (EstablishmentRegistrationOutletTabsEnum)parameter;
            isVisible = _value == _parameter;
            return isVisible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return isVisible;
        }
    }

    public class EstablishmentRegistrationActivityTabsEnumToVisibilityConverter : IValueConverter
    {
        private bool isVisible { get; set; } = false;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            EstablishmentOutletActivitiesTabsEnum _value = (EstablishmentOutletActivitiesTabsEnum)value;
            EstablishmentOutletActivitiesTabsEnum _parameter = (EstablishmentOutletActivitiesTabsEnum)parameter;
            isVisible = _value == _parameter;
            return isVisible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return isVisible;
        }
    }

    public class EstablishmentFinanicalMethodsToVisibilityConverter : IValueConverter
    {
        private bool isVisible { get; set; } = false;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            isVisible = value.ToString().Equals(parameter.ToString(), StringComparison.InvariantCultureIgnoreCase);
            return isVisible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return isVisible;
        }
    }

    public class EstablishmentMainActivityToVisibilityConverter : IValueConverter
    {
        private bool isVisible { get; set; } = false;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            isVisible = value.ToString().Equals("M", StringComparison.InvariantCultureIgnoreCase);
            return isVisible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return isVisible;
        }
    }

    public class ESTresidentialStatusConverter : IValueConverter
    {
        private string status { get; set; } = AppResources.ESTNationalyStatusOptionTwoValue;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null)
            {
                return string.Empty;
            }
            switch (value.ToString())
            {
                case "1":
                    return AppResources.ESTNationalyStatusOptionOneValue;
                case "2":
                    return AppResources.ESTNationalyStatusOptionTwoValue;
                case "3":
                default:
                    return AppResources.ESTNationalyStatusOptionThreeValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return status;
        }
    }
}
