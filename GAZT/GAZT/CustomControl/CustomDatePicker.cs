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
        public Dictionary<string, string> months;
        public ObservableCollection<object> Date { get; set; }

        public ObservableCollection<object> Day;
        public ObservableCollection<object> Month;
        public ObservableCollection<object> Year;

        public ObservableCollection<object> Time { get; set; }
        public ObservableCollection<object> Minute;
        public ObservableCollection<object> Hour;
        public ObservableCollection<object> Format;

        // Resolving Issue of Date of Birth to prevent selecting future date

        // @Divya Jannapureddy added line number 26
        private String selectedYear;


        public ObservableCollection<string> Headers { get; set; }
        public CustomDatePicker()
        {
            months = new Dictionary<string, string>();

            Date = new ObservableCollection<object>();
            Day = new ObservableCollection<object>();
            Month = new ObservableCollection<object>();
            Year = new ObservableCollection<object>();
            Headers = new ObservableCollection<string>();
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
            PopulateDateCollection();
            this.ItemsSource = Date;
            this.ColumnHeaderText = Headers;

            this.SelectionChanged += CustomDatePicker_SelectionChanged;
        }

        private void CustomDatePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDays(Date, e);
        }
        public void UpdateDays(ObservableCollection<object> Date, SelectionChangedEventArgs e)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                try
                {

                    if (Date.Count == 3)
                    {
                        bool isupdate = false;
                        if (e.OldValue != null && e.NewValue != null && (e.OldValue as ObservableCollection<object>).Count == 3 && (e.NewValue as ObservableCollection<object>).Count == 3)
                        {
                            if (!object.Equals((e.OldValue as IList)[0], (e.NewValue as IList)[0]))
                            {
                                isupdate = true;
                            }
                            if (!object.Equals((e.OldValue as IList)[2], (e.NewValue as IList)[2]))
                            {
                                isupdate = true;
                            }
                        }

                        //@Divya Jannapureddy added line number 102 to 103
                        if (resetDate())
                        {
                            if (isupdate)
                            {

                                ObservableCollection<object> days = new ObservableCollection<object>();
                                int month = DateTime.ParseExact(months[(e.NewValue as IList)[0].ToString()], "MM", CultureInfo.InvariantCulture).Month;
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
                                ObservableCollection<object> oldvalue = new ObservableCollection<object>();

                                foreach (var item in e.NewValue as IList)
                                {
                                    oldvalue.Add(item);
                                }
                                if (days.Count > 0)
                                {
                                    Date.RemoveAt(1);
                                    Date.Insert(1, days);
                                }

                                if ((Date[1] as IList).Contains(oldvalue[1]))
                                {
                                    this.SelectedItem = oldvalue;
                                }
                                else
                                {
                                    oldvalue[1] = (Date[1] as IList)[(Date[1] as IList).Count - 1];
                                    this.SelectedItem = oldvalue;
                                }
                            }
                        }

                        // @Divya Jannapureddy adding line number 148
                        selectedYear = (SelectedItem as IList)[2].ToString();
                    }
                }

                catch
                {

                }

            });
        }

        private void PopulateDateCollection()
        {

            //populate months

            for (int i = 1; i < 13; i++)
            {
                if (i < 10)
                {
                    Month.Add("0" + i);
                    if (!months.ContainsKey("0" + i))
                    {
                        months.Add("0" + i, "0" + i);
                    }
                }
                else
                {
                    Month.Add(i.ToString());
                    if (!months.ContainsKey(i.ToString()))
                    {
                        months.Add(i.ToString(), i.ToString());
                    }
                }

                // Month.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3));
            }


            //populate year
            for (int i = 1900; i <= DateTime.Today.Year; i++)
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

        // @Divya Jannapureddy adding line number 211 to 222
        private Boolean resetDate()
        {
            if (selectedYear != DateTime.Today.Year.ToString())
            {
                return true;

            }
            else
            {
                return false;
            }
        }

    }
}
