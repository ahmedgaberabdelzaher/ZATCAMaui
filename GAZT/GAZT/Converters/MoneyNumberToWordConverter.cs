using System;
using System.Globalization;
using Xamarin.Forms;

namespace EGAZT.Converters
{
    public class MoneyNumberToWordConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var number = (decimal)value;
			if (number == 0)
				return " ";
            if (number < 0)
                return value;


            if (number >= int.MaxValue)
                return value + " " + AppResources.ZSAR;

            var res = ToArabicWords(number);
            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
			return value;
        }

        string ToArabicWords(decimal number)
        {
            var toWord = new NumberToWord(number, new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
            string val;
            if (App.IsArabic)
                val =  toWord.ConvertToArabic();
            else
                val =  toWord.ConvertToEnglish();

            return val.Trim();
        }
    }
}