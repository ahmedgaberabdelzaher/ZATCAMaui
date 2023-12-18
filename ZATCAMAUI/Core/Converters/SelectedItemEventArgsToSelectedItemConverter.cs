using System.Globalization;

namespace ZATCAMAUI.Core.Converters
{
    class SelectedItemEventArgsToSelectedItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (parameter != null && parameter is ListView)
                if (((ListView)parameter).SelectedItem != null)
                    ((ListView)parameter).SelectedItem = null;
            var eventArgs = value as SelectedItemChangedEventArgs;

            return eventArgs.SelectedItem;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
