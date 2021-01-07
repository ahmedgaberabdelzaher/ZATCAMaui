using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.NewDesignConverters
{
    [Preserve(AllMembers = true)]
    public class ValueTypeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "ZS0001")
            {
                return AppResources.ZIBANNationalID;
            }
            else if (value.ToString() == "BUP002")
            {
                return AppResources.ZIBANCommercialRegistrationID;
            }
            else if (value.ToString() == "ZS0005")
            {
                return AppResources.ZIBANCompanyID;
            }
            else if (value.ToString() == "ZS0002")
            {
                return AppResources.ZZIqamaID;
            }
            else if (value.ToString() == "ZS0004")
            {
                return AppResources.ZZLicenseNumber;
            }
            else if (string.IsNullOrEmpty(value.ToString()) || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return true;
            }
            else return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
