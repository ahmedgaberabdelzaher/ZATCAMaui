using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using GAZT.CustomControl;
using Syncfusion.SfPicker.XForms;
using Xamarin.Forms.Internals;

namespace EGAZT.CustomControl
{
    [Preserve(AllMembers = true)]
    public class GenericCustomDatePicker : Syncfusion.SfPicker.XForms.SfPicker
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
       
        public ObservableCollection<string> Headers { get; set; }

        public GenericCustomDatePicker()
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

            PopulateFutureDateCollection();
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
                                    for (int j = 1; j <= 12; j++)
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

        private void PopulateFutureDateCollection()
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            //populate months
            try
            {
                for (int i = 1; i <= 12; i++)
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
                for (int i = 1900; i <= DateTime.Today.Year + 10; i++)
                {
                    Year.Add(i.ToString());
                }
                //populate Days
                for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
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
            catch (Exception )
            {

            }
        }

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
