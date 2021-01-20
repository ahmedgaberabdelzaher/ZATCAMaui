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
    public class CustomDatePicker : Syncfusion.SfPicker.XForms.SfPicker
    {
        public Dictionary<string, string> months;
        public ObservableCollection<object> Date { get; set; }
        public ObservableCollection<object> Day;
        public ObservableCollection<object> Month;
        public ObservableCollection<object> Year;
        //public ObservableCollection<object> Time { get; set; }
        //public ObservableCollection<object> Minute;
        //public ObservableCollection<object> Hour;
        //public ObservableCollection<object> Format;
        // Resolving Issue of Date of Birth to prevent selecting future date
        // @Divya Jannapureddy added line number 28
     
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
                                    for (int j = 1; j <= DateTime.Today.Day; j++)
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
                                    for (int j = 1; j <= DateTime.DaysInMonth(year, month); j++)
                                    {
                                        if (j < 10)
                                        {
                                            days.Add("0" + j);
                                        }
                                        else
                                            days.Add(j.ToString());
                                    }
                                }

                                for (int i = 1; i <= DateTime.Today.Month; i++)
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
                           //if (resetDateforMonth(month))
                           // {
                           //     //for (int j = 1; j <= DateTime.Today.Day; j++)
                           //     //{
                           //     //    if (j < 10)
                           //     //    {
                           //     //        days.Add("0" + j);
                           //     //    }
                           //     //    else
                           //     //        days.Add(j.ToString());
                           //     //}
                           //     for (int i = 1; i <= DateTime.Today.Month; i++)
                           //     {
                           //         if (i < 10)
                           //         {
                           //             if (!Month.Contains("0" + i)) { Month.Add("0" + i); }
                           //             if (!months.ContainsKey("0" + i))
                           //             {
                           //                 months.Add("0" + i, "0" + i);
                           //             }
                           //         }
                           //         else
                           //         {
                           //             if (!Month.Contains(i.ToString())) { Month.Add(i.ToString()); }
                           //             if (!months.ContainsKey(i.ToString()))
                           //             {
                           //                 months.Add(i.ToString(), i.ToString());
                           //             }
                           //         }
                           //     }
                           // }
                            else
                            {
                                for (int j = 1; j <= DateTime.DaysInMonth(year, month); j++)
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
                                    for (int j = DateTime.Today.Day; j <= DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month); j++)
                                    {
                                        days.Add($"{j:00}");
                                    }

                                }
                                else
                                {
                                    for (int j = 1; j <= DateTime.DaysInMonth(year, month); j++)
                                    {
                                        days.Add($"{j:00}");
                                    }
                                }

                                for (int i = DateTime.Today.Month; i <= 12; i++)
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
                            
                            else
                            {
                                for (int j = 1; j <= DateTime.DaysInMonth(year, month); j++)
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
        private void PopulateDateCollection()
        {
            Date?.Clear();
            months?.Clear();
            Day?.Clear();
            Year?.Clear();
            //populate months
            for (int i = 1; i <= DateTime.Today.Month; i++)
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
            for (int i = 1100; i <= DateTime.Today.Year; i++)
            {
                Year.Add(i.ToString());
            }
            //populate Days
            for (int i = 1; i <= DateTime.Today.Day; i++)
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
        private void PopulateFutureDateCollection()
        {
            Date?.Clear();
            months?.Clear();
            Day?.Clear();
            Year?.Clear();
            //populate months
            for (int i = DateTime.Today.Month; i <= 12; i++)
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
            for (int i = DateTime.Today.Year; i <= DateTime.Today.Year + 1000; i++)
            {
                Year.Add(i.ToString());
            }
            //populate Days
            for (int i = DateTime.Today.Day; i <= DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month); i++)
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
            if (year != DateTime.Today.Year)
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
            if (month != DateTime.Today.Month)
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