
using Mopups.Pages;
using Mopups.Services;
using Syncfusion.Maui.Picker;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel.GenericPickers;

namespace ZATCAMAUI.Views.NewDesign.GenericPickers
{

    public partial class CalendarPickerPageView : PopupPage
    {
        public Dictionary<string, string> months;

        CalendarPickerPageViewModel viewModel;
        string newDate = string.Empty;
        public CalendarPickerPageView()
        {
            InitializeComponent();
            months = new Dictionary<string, string>();

            viewModel = App.Locator.CalendarPickerPageView;
            _ = viewModel.SetDefaultDate();
            viewModel.IsFutureDatePickerVisible = false;
            viewModel.IsCurrentDatePickerVisible = true;

            this.BindingContext = viewModel;
        }
        public CalendarPickerPageView(GenericDatePickerModel _pickerSource)
        {
            InitializeComponent();

            viewModel = App.Locator.CalendarPickerPageView;
            viewModel.IsFutureDatePickerVisible = false;
            viewModel.IsCurrentDatePickerVisible = true;
            _ = viewModel.SetDefaultDate();
            viewModel.DataSource = _pickerSource;
            viewModel.PickerItemSource = viewModel.DataSource.PickerData;
            viewModel.DatePickerTitle = viewModel.DataSource.DatePickerTitle;

            this.BindingContext = viewModel;

        }

      

        public CalendarPickerPageView(GenericDatePickerModel _pickerSource, bool isFuturePickerVisible)
        {
            InitializeComponent();

            viewModel = App.Locator.CalendarPickerPageView;
            viewModel.IsFutureDatePickerVisible = true;
            viewModel.IsCurrentDatePickerVisible = false;
            _ = viewModel.SetDefaultDate();
            viewModel.DataSource = _pickerSource;
            viewModel.PickerItemSource = viewModel.DataSource.PickerData;
            viewModel.DatePickerTitle = viewModel.DataSource.DatePickerTitle;

            this.BindingContext = viewModel;
        }

        void genericPicker_SelectionChanged(object sender, PickerSelectionChangedEventArgs e)
        {
            try
            {
                if (viewModel.IsFutureDatePickerVisible == true)
                {
                    if (futureCalendarPicker.SelectedItem != null)
                    {
                        string month = futureCalendarPicker.SelectedDate.Month.ToString();
                        string day = futureCalendarPicker.SelectedDate.Day.ToString();
                        string year = futureCalendarPicker.SelectedDate.Year.ToString();
                        newDate = year + "/" + month + "/" + day;
                        viewModel.DataSource.SelectedValue = newDate;
                    }
                }
                else
                {
                    if (calendarPicker.SelectedItem != null)
                    {
                        string month = calendarPicker.SelectedDate.Month.ToString();
                        string day = calendarPicker.SelectedDate.Day.ToString();
                        string year = calendarPicker.SelectedDate.Year.ToString();
                        newDate = year + "/" + month + "/" + day;
                        viewModel.DataSource.SelectedValue = newDate;
                    }
                }
            }
            catch (Exception)
            {

            }

        }

        void PopupPage_BackgroundClicked(object sender, EventArgs e)
        {
            if (viewModel.IsFutureDatePickerVisible == true)
            {
                string month = futureCalendarPicker.SelectedDate.Month.ToString();
                string day = futureCalendarPicker.SelectedDate.Day.ToString();
                string year = futureCalendarPicker.SelectedDate.Year.ToString();
                newDate = year + "/" + month + "/" + day;
                viewModel.DataSource.SelectedValue = newDate;
                if (!string.IsNullOrWhiteSpace(viewModel.DataSource.SelectedValue))
                {
                    MessagingCenter.Send<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem", viewModel.DataSource);
                }
            }
            else
            {
                string month = calendarPicker.SelectedDate.Month.ToString();
                string day = calendarPicker.SelectedDate.Day.ToString();
                string year = calendarPicker.SelectedDate.Year.ToString();
                newDate = year + "/" + month + "/" + day;
                viewModel.DataSource.SelectedValue = newDate;
                if (!string.IsNullOrWhiteSpace(viewModel.DataSource.SelectedValue))
                {
                    MessagingCenter.Send<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem", viewModel.DataSource);

                }
            }



        }

        private async void PopupClose_Clicked(object sender, EventArgs e)
        {
            await MopupService.Instance.PopAsync();

            if (viewModel.IsFutureDatePickerVisible == true)
            {
                string month = futureCalendarPicker.SelectedDate.Month.ToString();
                string day = futureCalendarPicker.SelectedDate.Day.ToString();
                string year = futureCalendarPicker.SelectedDate.Year.ToString();
                newDate = year + "/" + month + "/" + day;
                viewModel.DataSource.SelectedValue = newDate;
                if (!string.IsNullOrWhiteSpace(viewModel.DataSource.SelectedValue))
                {
                    MessagingCenter.Send<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem", viewModel.DataSource);
                }
            }
            else
            {
                string month = calendarPicker.SelectedDate.Month.ToString();
                string day = calendarPicker.SelectedDate.Day.ToString();
                string year = calendarPicker.SelectedDate.Year.ToString();
                newDate = year + "/" + month + "/" + day;
                viewModel.DataSource.SelectedValue = newDate;
                if (!string.IsNullOrWhiteSpace(viewModel.DataSource.SelectedValue))
                {
                    MessagingCenter.Send<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem", viewModel.DataSource);
                }
            }


        }
    }
}
