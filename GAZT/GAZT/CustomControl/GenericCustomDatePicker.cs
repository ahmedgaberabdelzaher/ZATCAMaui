using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GAZT.CustomControl;

namespace EGAZT.CustomControl
{
    public class GenericCustomDatePicker:CustomDatePicker
    {
        public GenericCustomDatePicker()
        {
            months = new Dictionary<string, string>();
            Date = new ObservableCollection<object>();
            Day = new ObservableCollection<object>();
            Month = new ObservableCollection<object>();
            Year = new ObservableCollection<object>();

            PopulateFutureDateCollection();
        }

        private void PopulateFutureDateCollection()
        {
            //populate months
            try
            {
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
                for (int i = 1900; i <= DateTime.Today.Year + 10; i++)
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
            catch (Exception ex)
            {

            }
        }
    }
}
