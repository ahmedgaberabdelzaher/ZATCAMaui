
using Mopups.Pages;
using Mopups.Services;
using Syncfusion.Maui.Picker;
using System.Collections.ObjectModel;
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
            SetPickerFont();
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
            SetPickerFont();

        }

        public void SetPickerFont()
        {
            try
            {
                switch (DeviceInfo.Platform)
                {

                    case var _ when DeviceInfo.Current.Platform == DevicePlatform.iOS:
                        {
                            CalendarTitle.FontFamily = "Somar-SemiBold";
                            FutureCalendarTitle.FontFamily = "Somar-SemiBold";
                            CalendarDoneButton.FontFamily = "Somar-SemiBold";

                            calendarPicker.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            futureCalendarPicker.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";

                            calendarPicker.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            futureCalendarPicker.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";

                            calendarPicker.TextStyle.FontFamily = "Somar-SemiBold";
                            futureCalendarPicker.TextStyle.FontFamily = "Somar-SemiBold";

                            calendarPicker.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            futureCalendarPicker.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                        }
                        break;
                    case var _ when DeviceInfo.Current.Platform == DevicePlatform.Android:

                        CalendarTitle.FontFamily = "GAZT_FONT_MEDIUM";//"Somar-SemiBold.otf#Somar-SemiBold";
                        FutureCalendarTitle.FontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        CalendarDoneButton.FontFamily = "GAZT_FONT_MEDIUM";

                        calendarPicker.ColumnHeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        futureCalendarPicker.ColumnHeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";

                        calendarPicker.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        futureCalendarPicker.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";

                        calendarPicker.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        futureCalendarPicker.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";

                        calendarPicker.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        futureCalendarPicker.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";

                        break;
                }
            }
            catch (Exception)
            {

            }

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
            SetPickerFont();
        }

        void genericPicker_SelectionChanged(object sender, PickerSelectionChangedEventArgs e)
        {
            try
            {
                if (viewModel.IsFutureDatePickerVisible == true)
                {
                    if (futureCalendarPicker.SelectedItem != null)
                    {
                        var selectedItem = futureCalendarPicker.SelectedItem as ObservableCollection<object>;
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
                        newDate = year + "/" + month + "/" + day;
                        viewModel.DataSource.SelectedValue = newDate;
                    }
                }
                else
                {
                    if (calendarPicker.SelectedItem != null)
                    {
                        var selectedItem = calendarPicker.SelectedItem as ObservableCollection<object>;
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
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
                var selectedItem = futureCalendarPicker.SelectedItem as ObservableCollection<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                newDate = year + "/" + month + "/" + day;

                if (newDate != null || newDate != string.Empty)
                {
                    MessagingCenter.Send<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem", viewModel.DataSource);

                    // MessagingCenter.Send(this, "DatePickerSelectedItem",viewModel.DataSource);
                }
            }
            else
            {
                var selectedItem = calendarPicker.SelectedItem as ObservableCollection<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                newDate = year + "/" + month + "/" + day;

                if (newDate != null || newDate != string.Empty)
                {
                    MessagingCenter.Send<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem", viewModel.DataSource);

                    // MessagingCenter.Send(this, "DatePickerSelectedItem",viewModel.DataSource);
                }
            }



        }

        private async void PopupClose_Clicked(object sender, EventArgs e)
        {
            await MopupService.Instance.PopAsync();

            if (viewModel.IsFutureDatePickerVisible == true)
            {
                var selectedItem = futureCalendarPicker.SelectedItem as ObservableCollection<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                newDate = year + "/" + month + "/" + day;

                if (newDate != null || newDate != string.Empty)
                {
                    MessagingCenter.Send<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem", viewModel.DataSource);
                }
            }
            else
            {
                var selectedItem = calendarPicker.SelectedItem as ObservableCollection<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                newDate = year + "/" + month + "/" + day;

                if (newDate != null || newDate != string.Empty)
                {
                    MessagingCenter.Send<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem", viewModel.DataSource);
                }
            }


        }
    }
}
