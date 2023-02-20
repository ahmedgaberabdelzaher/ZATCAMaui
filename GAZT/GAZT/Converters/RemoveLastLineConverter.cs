using System;
using System.Collections.ObjectModel;
using System.Globalization;
using Xamarin.Forms;
using EGAZT.Controls;
using System.Linq;
using System.Collections.Generic;

namespace EGAZT.Converters
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
                    var test = collectionView?.ItemsSource as List<BottomSheetModel>;

                    if (bottomSheetList != null)
                    {
                        foreach (var item in bottomSheetList)
                        {
                            if (bottomSheetList.Last() == item)
                                item.HasLine = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

           
              
                   
            var eventArgs = value as SelectedItemChangedEventArgs;

            return eventArgs.SelectedItem;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

