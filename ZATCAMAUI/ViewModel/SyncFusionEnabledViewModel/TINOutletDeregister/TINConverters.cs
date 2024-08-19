using System.Globalization;
using Syncfusion.Maui.ListView;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.TINOutletDeregister;

#region HeightConverter
public class HeightConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var listView = parameter as SfListView;
        var bindingContext = (listView.BindingContext as DetailsContactInfo);

        if (bindingContext == null) return 0;

        var items = bindingContext.Members;
        return listView.ItemSize * items.Count;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
#endregion

#region RotationConverter
public class RotationConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? 0 : 180;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
#endregion

#region TextConverter
public class TextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? "-" : "+";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
#endregion