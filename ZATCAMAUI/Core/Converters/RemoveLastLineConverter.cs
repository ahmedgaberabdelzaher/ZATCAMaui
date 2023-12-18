using System.Collections.ObjectModel;
using System.Globalization;
using ZATCAMAUI.Models;

namespace ZATCAMAUI.Core.Converters
{
    public class RemoveLastLineConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (parameter != null && parameter is CollectionView)
                {
                    var collectionView = ((CollectionView)parameter);
                    var bottomSheetList = collectionView?.ItemsSource as ObservableCollection<BottomSheetModel>;

                    if (bottomSheetList != null && bottomSheetList.Count >= 1)
                    {
                        var lastItem = bottomSheetList[bottomSheetList.Count - 1];
                        lastItem.HasLine = !(bool)value;
                    }
                }
                
                return (bool)value;
            }
            catch (Exception)
            {
                return (bool)value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

