using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace GAZT.CustomControl
{
    [Preserve(AllMembers = true)]
    public class DataMarkerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
                if (parameter != null && parameter.ToString() == "Label")
                {
                    if (value is List<object>)
                    {
                        return "Others";
                    }
                    else
                    {
                        if (value != null)
                        {
                            return (value as MyBillsChartModel).BillType;
                        }
                    }
                }
                else
                {
                    if (value is List<object>)
                    {
                        return (value as List<object>).Sum(item => (item as MyBillsChartModel).BillCount).ToString();
                    }
                    else
                    {
                        if (value != null)
                        {
                            return (value as MyBillsChartModel).BillCount;
                        }
                    }
                }
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
