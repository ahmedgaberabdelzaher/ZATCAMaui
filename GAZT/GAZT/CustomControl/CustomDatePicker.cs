using Syncfusion.SfPicker.XForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;

namespace GAZT.CustomControl
{
    public class CustomDatePicker : Syncfusion.SfPicker.XForms.SfPicker
    {
        internal Dictionary<string, string> Months { get; set; }
        public ObservableCollection<object> Date { get; set; }
        internal ObservableCollection<object> Day { get; set; }
        internal ObservableCollection<object> Month { get; set; }
        internal ObservableCollection<object> Year { get; set; }
        public ObservableCollection<string> Headers { get; set; }
        public CustomDatePicker()

        {

            Months = new Dictionary<string, string>();

            Date = new ObservableCollection<object>();

            Day = new ObservableCollection<object>();

            Month = new ObservableCollection<object>();

            Year = new ObservableCollection<object>();
            Headers = new ObservableCollection<string>();

            PopulateDateCollection();

            this.ItemsSource = Date;
            this.SelectionChanged += CustomDatePicker_SelectionChanged;

            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
            {
                if (!App.IsArabic)
                {
                    Headers.Add("MONTH");
                    Headers.Add("DAY");
                    Headers.Add("YEAR");
                }
                else
                {
                    Headers.Add("شهر");
                    Headers.Add("يوم");
                    Headers.Add("عام");
                }
            }
            else
            {
                if (!App.IsArabic)
                {
                    Headers.Add("Month");
                    Headers.Add("Day");
                    Headers.Add("Year");
                }
                else
                {
                    Headers.Add("شهر");
                    Headers.Add("يوم");
                    Headers.Add("عام");
                }
            }
            this.ColumnHeaderText = Headers;

        }

        private void PopulateDateCollection()

        {

            //populate months

            for (int i = 1; i < 13; i++)

            {

                if (!Months.ContainsKey(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3)))

                    Months.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3), CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));

                Month.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3));

            }

            //populate year

            for (int i = 1990; i < 2050; i++)

            {

                Year.Add(i.ToString());

            }

            //populate Days

            for (int i = 1; i <= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); i++)

            {

                if (i < 10)

                {

                    Day.Add("0" + i);

                }

                else

                    Day.Add(i.ToString());

            }

            Date.Add(Month);

            Date.Add(Day);

            Date.Add(Year);

        }
        private void CustomDatePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)

        {

            UpdateDays(Date, e);

        }
        public void UpdateDays(ObservableCollection<object> Date, SelectionChangedEventArgs e)

{

            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>

{

        if (Date.Count == 3)
        {
            bool flag = false;
            if (e.OldValue != null && e.NewValue != null && (e.OldValue as ObservableCollection<object>).Count == 3 && (e.NewValue as ObservableCollection<object>).Count== 3 )
            {
                if (!object.Equals((e.OldValue as IList)[0], (e.NewValue as IList)[0]))
                {
                    flag = true;
                }
                if (!object.Equals((e.OldValue as IList)[2], (e.NewValue as IList)[2]))
                {
                    flag = true;
                }
            }

            if (flag)
            {

                ObservableCollection<object> days = new ObservableCollection<object>();
                int month = DateTime.ParseExact(Months[(e.NewValue as IList)[0].ToString()], "MMMM", CultureInfo.InvariantCulture).Month;
                int year = int.Parse((e.NewValue as IList)[2].ToString());
                for (int j = 1; j <= DateTime.DaysInMonth(year, month); j++)
                {
                    if (j < 10)
                    {
                        days.Add("0" + j);
                    }
                    else
                        days.Add(j.ToString());
                }
                ObservableCollection<object> PreviousValue = new ObservableCollection<object>();

                foreach (var item in e.NewValue as IList)
                {
                    PreviousValue.Add(item);
                }
                if (days.Count > 0)
                {
                    Date.RemoveAt(1);
                    Date.Insert(1, days);
                }

                if ((Date[1] as IList).Contains(PreviousValue[1]))
                {
                    this.SelectedItem = PreviousValue;
                }
                else
                {
                    PreviousValue[1] = (Date[1] as IList)[(Date[1] as IList).Count - 1];
                    this.SelectedItem = PreviousValue;
                }
            }
        }
});

}




    }
}
