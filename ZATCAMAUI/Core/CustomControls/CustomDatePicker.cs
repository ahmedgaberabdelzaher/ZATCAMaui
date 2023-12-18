using Syncfusion.Maui.Picker;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace ZATCAMAUI.Core.CustomControls
{
    public class CustomDatePicker : SfPicker
    {
        PickerColumn dayColumn;
        PickerColumn monthColumn;
        PickerColumn yearColumn;
        ObservableCollection<string> day = new ObservableCollection<string>();
        ObservableCollection<string> month = new ObservableCollection<string>();
        ObservableCollection<string> year = new ObservableCollection<string>();
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
            defaultValue: typeof(object));

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
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
        public CustomDatePicker()
        {

            if (!FutureDay) InitializeDatePicker();
            else InitializeFutureDatePicker();

        }

        private void Picker_SelectionChanged(object sender, PickerSelectionChangedEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                var oldDay = day[e.OldValue];
                newDay = day[e.NewValue];

                // for testing
                //this.Columns[0].HeaderText = $"{oldDay} {newDay}";
            }
            else if (e.ColumnIndex == 1)
            {
                var oldMonth = month[e.OldValue];
                newMonth = month[e.NewValue];

                // for testing
                //this.Columns[1].HeaderText = $"{oldMonth} {newMonth}";
            }
            else
            {
                var oldYear = year[e.OldValue];
                newYear = year[e.NewValue];

                // for testing
                //this.Columns[2].HeaderText = $"{oldYear} {newYear}";
            }

            if (e.ColumnIndex == 1 || e.ColumnIndex == 2) // Month/Year column
            {
                noOfDays = DateTime.DaysInMonth(int.Parse(newYear), int.Parse(newMonth));
                day = new ObservableCollection<string>();
                for (int i = 1; i <= noOfDays; i++)
                {
                    day.Add(i.ToString("D2"));
                    this.Columns[0].ItemsSource = day;

                }

                #region To Adjust selection of days if it was 28/31/30
                if (noOfDays == 30 && newDay.Equals("31"))
                    this.Columns[0].SelectedIndex = 27;

                else if (noOfDays == 31 && newDay.Equals("30"))
                    this.Columns[0].SelectedIndex = 27;

                else if (noOfDays == 28 && newDay.Equals("30")
                    || noOfDays == 28 && newDay.Equals("31"))
                    this.Columns[0].SelectedIndex = 27;
                #endregion
            }
            SelectedItem = new DateTime(int.Parse(newYear), int.Parse(newMonth), int.Parse(newDay));
            //this.HeaderView.Text = $"{newDay}-{newMonth}-{newYear}";
        }

        private void InitializeDatePicker()
        {
            noOfDays = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month);

            for (int i = 1; i <= noOfDays; i++)
            {
                day.Add(i.ToString("D2"));

            }
            dayColumn = new PickerColumn()
            {
                HeaderText = day[0],
                ItemsSource = day,
                SelectedIndex = 0,
            };

            for (int i = 1; i <= 12; i++)
            {
                month.Add(i.ToString("D2"));

            }

            monthColumn = new PickerColumn()
            {
                HeaderText = month[0],
                ItemsSource = month,
                SelectedIndex = 0,
            };



            for (int i = 1900; i < 2050; i++)
            {
                year.Add(i.ToString());

            }

            yearColumn = new PickerColumn()
            {
                HeaderText = year.Last(),
                ItemsSource = year,
                SelectedIndex = int.Parse(year.Last()),
            };

            this.Columns.Add(dayColumn);
            this.Columns.Add(monthColumn);
            this.Columns.Add(yearColumn);
            this.SelectionChanged += Picker_SelectionChanged;
            //this.HeaderView.Text = $"{dayColumn.HeaderText}-{monthColumn.HeaderText}-{yearColumn.HeaderText}";
            newDay = day[0]; newMonth = month[0]; newYear = year[0];
        }
        private void InitializeFutureDatePicker()
        {
            noOfDays = DateTime.DaysInMonth(DateTime.Today.Year,DateTime.Today.Month);

            for (int i = 1; i <= noOfDays; i++)
            {
                day.Add(i.ToString("D2"));

            }
            dayColumn = new PickerColumn()
            {
                HeaderText = day[0],
                ItemsSource = day,
                SelectedIndex = 0,
            };

            for (int i = 1; i <= 12; i++)
            {
                month.Add(i.ToString("D2"));

            }

            monthColumn = new PickerColumn()
            {
                HeaderText = month[0],
                ItemsSource = month,
                SelectedIndex = 0,
            };



            for (int i = DateTime.Today.Year; i <= DateTime.Today.Year + 1000; i++)
            {
                year.Add(i.ToString());

            }

            yearColumn = new PickerColumn()
            {
                HeaderText = year[0],
                ItemsSource = year,
                SelectedIndex = 0,
            };

            this.Columns.Add(dayColumn);
            this.Columns.Add(monthColumn);
            this.Columns.Add(yearColumn);
            this.SelectionChanged += Picker_SelectionChanged;
            // this.HeaderView.Text = $"{dayColumn.HeaderText}-{monthColumn.HeaderText}-{yearColumn.HeaderText}";
            newDay = day[0]; newMonth = month[0]; newYear = year[0];
        }
    }
}