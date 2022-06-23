using EGAZT;
using Syncfusion.SfPicker.XForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms.Internals;

namespace GAZT.CustomControl
{
    [Preserve(AllMembers = true)]
    public class CustomHijriDatePicker : Syncfusion.SfPicker.XForms.SfPicker
    {
        public Dictionary<string, string> months;
        public ObservableCollection<object> Date { get; set; }
        public ObservableCollection<object> Day;
        public ObservableCollection<object> Month;
        public ObservableCollection<object> Year;

        public ObservableCollection<string> Headers { get; set; }


        public static readonly Xamarin.Forms.BindableProperty FutureDayProperty = Xamarin.Forms.BindableProperty.Create(propertyName: nameof(FutureDay),
               returnType: typeof(bool),
               declaringType: typeof(CustomHijriDatePicker),
               defaultValue: false);
        public bool FutureDay
        {
            get { return (bool)GetValue(FutureDayProperty); }
            set { SetValue(FutureDayProperty, value); }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == FutureDayProperty.PropertyName)
            {
                if (FutureDay)
                {
                    PopulateFutureDateCollection();
                }
                else
                {
                    PopulateDateCollection();
                }
            }
        }

        public CustomHijriDatePicker()
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

                    Headers.Add("DAY");
                    Headers.Add("MONTH");
                    Headers.Add("YEAR");
                }
                else
                {
                    Headers.Add("يوم");
                  
                    Headers.Add("شهر");//month
                    Headers.Add("عام");
                }
            }
            else
            {
                if (!App.IsArabic)
                {

                    Headers.Add("Day");
                    Headers.Add("Month");
                    Headers.Add("Year");
                }
                else
                {

                    Headers.Add("يوم");
                    Headers.Add("شهر");//Month
                    Headers.Add("عام");
                }
            }
            if (FutureDay)
            {
                PopulateFutureDateCollection();
            }
            else
            {
                PopulateDateCollection();
            }
            this.ItemsSource = Date;
            this.ColumnHeaderText = Headers;
            this.SelectionChanged += CustomDatePicker_SelectionChanged;
        }
        private void CustomDatePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FutureDay)
            {
                UpdateFutureDays(Date, e);
            }
            else
            {
                UpdateDays(Date, e);
            }
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
                            if (!object.Equals((e.OldValue as IList)[1], (e.NewValue as IList)[1]))
                            {
                                isupdate = true;
                            }
                            if (!object.Equals((e.OldValue as IList)[2], (e.NewValue as IList)[2]))
                            {
                                isupdate = true;
                            }
                        }
                        //@DivyaJannapureddy replace from line number 107 to 189
                        if (isupdate)
                        {
                            HijriCalendar calender = new HijriCalendar();
                            ObservableCollection<object> days = new ObservableCollection<object>();
                            int month = DateTime.ParseExact(months[(e.NewValue as IList)[1].ToString()], "MM", CultureInfo.InvariantCulture).Month;
                            int year = int.Parse((e.NewValue as IList)[2].ToString());
                            months.Clear();
                            days.Clear();
                            Month.Clear();
                            if (resetDate(year))
                            {
                                if (resetDateforMonth(month))
                                {
                                    var dayHijri = calender.GetDayOfMonth(DateTime.Today);
                                    for (int j = 1; j <= dayHijri; j++)
                                    {
                                        if (j < 10)
                                        {
                                            days.Add("0" + j);
                                        }
                                        else
                                            days.Add(j.ToString());
                                    }

                                }
                                else
                                {
                                    var dayHijri = calender.GetDaysInMonth(year, month);
                                    for (int j = 1; j <= dayHijri; j++)
                                    {
                                        if (j < 10)
                                        {
                                            days.Add("0" + j);
                                        }
                                        else
                                            days.Add(j.ToString());
                                    }
                                }
                                var monthHijri = calender.GetMonth(DateTime.Today);
                                for (int i = 1; i <= monthHijri; i++)
                                {
                                    if (i < 10)
                                    {
                                        if (!Month.Contains("0" + i)) { Month.Add("0" + i); }
                                        if (!months.ContainsKey("0" + i))
                                        {
                                            months.Add("0" + i, "0" + i);
                                        }
                                    }
                                    else
                                    {
                                        if (!Month.Contains(i.ToString())) { Month.Add(i.ToString()); }
                                        if (!months.ContainsKey(i.ToString()))
                                        {
                                            months.Add(i.ToString(), i.ToString());
                                        }
                                    }
                                }

                            }

                            else
                            {
                                var daysInmonthHijri = calender.GetDaysInMonth(year, month);
                                for (int j = 1; j <=daysInmonthHijri; j++)
                                {
                                    if (j < 10)
                                    {
                                        days.Add("0" + j);
                                    }
                                    else
                                        days.Add(j.ToString());
                                }
                                for (int i = 1; i <= 12; i++)
                                {
                                    if (i < 10)
                                    {
                                        if (!Month.Contains("0" + i)) { Month.Add("0" + i); }
                                        if (!months.ContainsKey("0" + i))
                                        {
                                            months.Add("0" + i, "0" + i);
                                        }
                                    }
                                    else
                                    {
                                        if (!Month.Contains(i.ToString())) { Month.Add(i.ToString()); }
                                        if (!months.ContainsKey(i.ToString()))
                                        {
                                            months.Add(i.ToString(), i.ToString());
                                        }
                                    }
                                }
                            }
                            ObservableCollection<object> oldvalue = new ObservableCollection<object>();
                            foreach (var item in e.NewValue as IList)
                            {
                                oldvalue.Add(item);
                            }
                            if (days.Count > 0)
                            {
                                Date.RemoveAt(0);
                                Date.Insert(0, days);
                            }
                            if ((Date[0] as IList).Contains(oldvalue[0]))
                            {
                                this.SelectedItem = oldvalue;
                            }
                            else
                            {
                                oldvalue[0] = (Date[0] as IList)[(Date[0] as IList).Count - 1];
                                this.SelectedItem = oldvalue;
                            }
                        }
                    }
                }
                catch
                {
                }
            });
        }
        private void PopulateDateCollection()
        {
            Date?.Clear();
            months?.Clear();
            Day?.Clear();
            Year?.Clear();
            //populate months
            HijriCalendar calender = new HijriCalendar();
            var monthHijri = calender.GetMonth(DateTime.Today);
            for (int i = 1; i <= monthHijri; i++)
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
            
            var year = calender.GetYear(DateTime.Today);
            for (int i = 1000; i <= year; i++)
            {
                Year.Add(i.ToString());
            }
            //populate Days
            var days = calender.GetDayOfMonth(DateTime.Today);
            for (int i = 1; i <= days; i++)
            {
                if (i < 10)
                {
                    Day.Add("0" + i);
                }
                else
                    Day.Add(i.ToString());
            }
            Date.Add(Day);
            Date.Add(Month);

            Date.Add(Year);
        }
        public void UpdateFutureDays(ObservableCollection<object> Date, SelectionChangedEventArgs e)
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
                            if (!object.Equals((e.OldValue as IList)[1], (e.NewValue as IList)[1]))
                            {
                                isupdate = true;
                            }
                            if (!object.Equals((e.OldValue as IList)[2], (e.NewValue as IList)[2]))
                            {
                                isupdate = true;
                            }
                        }
                        //@DivyaJannapureddy replace from line number 107 to 189
                        if (isupdate)
                        {
                            HijriCalendar calender = new HijriCalendar();
                            ObservableCollection<object> days = new ObservableCollection<object>();
                            int month = DateTime.ParseExact(months[(e.NewValue as IList)[1].ToString()], "MM", CultureInfo.InvariantCulture).Month;
                            int year = int.Parse((e.NewValue as IList)[2].ToString());
                            months.Clear();
                            days.Clear();
                            Month.Clear();
                            if (resetDate(year))
                            {
                                if (resetDateforMonth(month))
                                {
                                    var dayHijri = calender.GetDayOfMonth(DateTime.Today);
                                    var maxDayInMonth = calender.GetDaysInMonth(year, month);
                                    for (int j = dayHijri; j <= maxDayInMonth; j++)
                                    {
                                        days.Add($"{j:00}");
                                    }
                                }
                                else
                                {
                                    var dayHijri = calender.GetDaysInMonth(year, month);
                                    for (int j = 1; j <= dayHijri; j++)
                                    {
                                        days.Add($"{j:00}");
                                    }
                                }
                                var monthHijri = calender.GetMonth(DateTime.Today);
                                for (int i = monthHijri; i <= 12; i++)
                                {
                                    var m = $"{i:00}";
                                    if (!Month.Contains(m)) {
                                        Month.Add(m);
                                    }
                                    if (!months.ContainsKey(m))
                                    {
                                        months.Add(m, m);
                                    }
                                }

                            }

                            else
                            {
                                var daysInmonthHijri = calender.GetDaysInMonth(year, month);
                                for (int j = 1; j <= daysInmonthHijri; j++)
                                {
                                    days.Add($"{j:00}");
                                }
                                for (int i = 1; i <= 12; i++)
                                {
                                    var m = $"{i:00}";
                                    if (!Month.Contains(m))
                                    {
                                        Month.Add(m);
                                    }
                                    if (!months.ContainsKey(m))
                                    {
                                        months.Add(m, m);
                                    }
                                }
                            }
                            ObservableCollection<object> oldvalue = new ObservableCollection<object>();
                            foreach (var item in e.NewValue as IList)
                            {
                                oldvalue.Add(item);
                            }
                            if (days.Count > 0)
                            {
                                Date.RemoveAt(0);
                                Date.Insert(0, days);
                            }
                            if ((Date[0] as IList).Contains(oldvalue[0]))
                            {
                                this.SelectedItem = oldvalue;
                            }
                            else
                            {
                                oldvalue[0] = (Date[0] as IList)[(Date[0] as IList).Count - 1];
                                this.SelectedItem = oldvalue;
                            }
                        }
                    }
                }
                catch
                {
                }
            });
        }
        private void PopulateFutureDateCollection()
        {
            Date?.Clear();
            months?.Clear();
            Day?.Clear();
            Year?.Clear();
            //populate months
            HijriCalendar calender = new HijriCalendar();
            var monthHijri = calender.GetMonth(DateTime.Today);
            for (int i = monthHijri; i <= 12; i++)
            {

                var m = $"{i:00}";
                if (!Month.Contains(m))
                {
                    Month.Add(m);
                }
                if (!months.ContainsKey(m))
                {
                    months.Add(m, m);
                }

                // Month.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3));
            }
            //populate year
            var year = calender.GetYear(DateTime.Today);
            for (int i = year; i <= year + 1000; i++)
            {
                Year.Add(i.ToString());
            }
            //populate Days
            var days = calender.GetDayOfMonth(DateTime.Today);
            var dayHijri = calender.GetDaysInMonth(year, monthHijri);
            for (int i = days; i <= dayHijri; i++)
            {
                Day.Add($"{i:00}");
            }
            Date.Add(Day);
            Date.Add(Month);

            Date.Add(Year);
        }
        // @Divya Jannapureddy replace this method
        private Boolean resetDate(int year)
        {
            HijriCalendar calender = new HijriCalendar();
            var yearHijri = calender.GetYear(DateTime.Today);
            if (year != yearHijri)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private Boolean resetDateforMonth(int month)
        {
            HijriCalendar calender = new HijriCalendar();
            var monthHijri = calender.GetMonth(DateTime.Today);
            if (month != monthHijri)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}