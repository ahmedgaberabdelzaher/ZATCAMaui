using Syncfusion.Maui.Picker;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace ZATCAMAUI.Core.CustomControls
{
    public class CustomHijriDatePicker : SfPicker
    {
        PickerColumn dayColumn;
        PickerColumn monthColumn;
        PickerColumn yearColumn;
        ObservableCollection<string> day = new ObservableCollection<string>();
        ObservableCollection<string> month = new ObservableCollection<string>();
        ObservableCollection<string> year = new ObservableCollection<string>();
        ObservableCollection<PickerColumn> pickerColumns = new ObservableCollection<PickerColumn>();
        UmAlQuraCalendar hijri = new UmAlQuraCalendar();
        string newDay, newMonth, newYear;
        int noOfDays;

        public static readonly BindableProperty FutureDayProperty = BindableProperty.Create(propertyName: nameof(FutureDay),
              returnType: typeof(bool),
              declaringType: typeof(CustomHijriDatePicker),
              defaultValue: false);

        public bool FutureDay
        {
            get { return (bool)GetValue(FutureDayProperty); }
            set { SetValue(FutureDayProperty, value); }
        }

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(propertyName: nameof(SelectedItem),
            returnType: typeof(object),
            declaringType: typeof(CustomHijriDatePicker),
            defaultValue: typeof(object),
            defaultBindingMode: BindingMode.TwoWay);

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly BindableProperty SelectedDateProperty = BindableProperty.Create(propertyName: nameof(SelectedDate),
            returnType: typeof(DateTime),
            declaringType: typeof(CustomHijriDatePicker),
            defaultBindingMode: BindingMode.TwoWay);

        public DateTime SelectedDate
        {
            get { return (DateTime)GetValue(SelectedDateProperty); }
            set { SetValue(SelectedDateProperty, value); }
        }
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == FutureDayProperty.PropertyName)
            {
                if (!FutureDay) InitializeDatePicker();
                else InitializeFutureDatePicker();
            }
        }
        public CustomHijriDatePicker()
        {
            if (!FutureDay) InitializeDatePicker();
            else InitializeFutureDatePicker();

        }

        private void Picker_SelectionChanged(object sender, PickerSelectionChangedEventArgs e)
        {
            try
            {


                if (e.ColumnIndex == 0)
                {
                    var oldDay = day[e.OldValue];
                    newDay = day[e.NewValue];
                }
                else if (e.ColumnIndex == 1)
                {
                    var oldMonth = month[e.OldValue];
                    newMonth = month[e.NewValue];
                }
                else
                {
                    var oldYear = year[e.OldValue];
                    newYear = year[e.NewValue];

                }

                if (e.ColumnIndex == 1 || e.ColumnIndex == 2) // Month/Year column
                {
                    noOfDays = hijri.GetDaysInMonth(int.Parse(newYear), int.Parse(newMonth), hijri.GetEra(DateTime.Now));
                    day = new ObservableCollection<string>();
                    for (int i = 1; i <= noOfDays; i++)
                    {
                        day.Add(i.ToString("D2"));
                        this.Columns[0].ItemsSource = day;

                    }

                    #region To Adjust selection of days if it was 30 or 29
                    if (noOfDays == 30 && newDay.Equals("29"))
                        this.Columns[0].SelectedIndex = 28;

                    else if (noOfDays == 29 && newDay.Equals("30"))
                        this.Columns[0].SelectedIndex = 28;
                    #endregion
                }
                SelectedDate = new DateTime(int.Parse(newYear), int.Parse(newMonth), int.Parse(newDay));
                SelectedItem = new ObservableCollection<object>
                {
                    newDay,
                    newMonth,
                    newYear
                };
            }
            catch (Exception)
            {

            }
        }

        private void InitializeDatePicker()
        {
            try
            {
                ReInitializeList();

                noOfDays = hijri.GetDaysInMonth(hijri.GetYear(DateTime.Today), hijri.GetMonth(DateTime.Today), hijri.GetEra(DateTime.Now));

                for (int i = 1; i <= noOfDays; i++)
                {
                    day.Add(i.ToString("D2"));

                }
                dayColumn = new PickerColumn()
                {
                    ItemsSource = day,
                    SelectedIndex = 0,
                };

                for (int i = 1; i <= 12; i++)
                {
                    month.Add(i.ToString("D2"));

                }

                monthColumn = new PickerColumn()
                {
                    ItemsSource = month,
                    SelectedIndex = 0,
                };



                for (int i = 0; i <= (hijri.GetYear(DateTime.Today) - 1349); i++)
                {
                    int x = 1349 + i;
                    year.Add(x.ToString());

                }

                yearColumn = new PickerColumn()
                {
                    ItemsSource = year,
                    SelectedIndex = 0,
                };

                pickerColumns = new ObservableCollection<PickerColumn>()
                {
                    dayColumn,
                    monthColumn,
                    yearColumn
                };

                this.Columns = pickerColumns;

                this.SelectionChanged += Picker_SelectionChanged;
                newDay = day[0]; newMonth = month[0]; newYear = year[0];
                SelectedDate = new DateTime(int.Parse(newYear), int.Parse(newMonth), int.Parse(newDay));
                SelectedItem = new ObservableCollection<object>
                {
                    newDay,
                    newMonth,
                    newYear
                };

            }
            catch (Exception)
            {

            }
        }

        private void InitializeFutureDatePicker()
        {
            try
            {
                ReInitializeList();

                noOfDays = hijri.GetDaysInMonth(hijri.GetYear(DateTime.Today), hijri.GetMonth(DateTime.Today), hijri.GetEra(DateTime.Now));

                for (int i = 1; i <= noOfDays; i++)
                {
                    day.Add(i.ToString("D2"));

                }
                dayColumn = new PickerColumn()
                {
                    ItemsSource = day,
                    SelectedIndex = 0,
                };

                for (int i = 1; i <= 12; i++)
                {
                    month.Add(i.ToString("D2"));

                }

                monthColumn = new PickerColumn()
                {
                    ItemsSource = month,
                    SelectedIndex = 0,
                };



                for (int i = hijri.GetYear(DateTime.Today); i <= (hijri.GetYear(DateTime.Today) + 1000); i++)
                {
                    year.Add(i.ToString());

                }

                yearColumn = new PickerColumn()
                {
                    ItemsSource = year,
                    SelectedIndex = 0,
                };

                pickerColumns = new ObservableCollection<PickerColumn>()
                {
                    dayColumn,
                    monthColumn,
                    yearColumn
                };

                this.Columns = pickerColumns;

                this.SelectionChanged += Picker_SelectionChanged;
                newDay = day[0]; newMonth = month[0]; newYear = year[0];
                SelectedDate = new DateTime(int.Parse(newYear), int.Parse(newMonth), int.Parse(newDay));
                SelectedItem = new ObservableCollection<object>
                {
                    newDay,
                    newMonth,
                    newYear
                };

            }
            catch (Exception)
            {

            }
        }

        private void ReInitializeList()
        {
            day = new ObservableCollection<string>();
            month = new ObservableCollection<string>();
            year = new ObservableCollection<string>();
        }
        protected override void OnOkButtonClicked(EventArgs e)
        {
            base.OnOkButtonClicked(e);

            this.IsOpen = false;
        }

        protected override void OnCancelButtonClicked(EventArgs e)
        {
            base.OnCancelButtonClicked(e);
            this.IsOpen = false;

            SelectedDate = new DateTime();
            SelectedItem = default(object);
        }
    }
}