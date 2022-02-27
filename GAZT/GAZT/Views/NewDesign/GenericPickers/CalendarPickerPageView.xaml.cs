using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.CalendarPickerPageViewModel;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.GenericPickers
{
    [Preserve(AllMembers = true)]
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

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            SetPickerFont();

        }

        public void SetPickerFont()
        {
            try
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {

                    case Xamarin.Forms.Device.iOS:
                        {
                            CalendarTitle.FontFamily = "Somar-SemiBold";
                            FutureCalendarTitle.FontFamily = "Somar-SemiBold";
                            CalendarDoneButton.FontFamily = "Somar-SemiBold";

                            calendarPicker.ColumnHeaderFontFamily = "Somar-SemiBold";
                            futureCalendarPicker.ColumnHeaderFontFamily = "Somar-SemiBold";
                            calendarPicker.HeaderFontFamily = "Somar-SemiBold";
                            futureCalendarPicker.HeaderFontFamily = "Somar-SemiBold";

                            calendarPicker.SelectedItemFontFamily = "Somar-SemiBold";
                            calendarPicker.UnSelectedItemFontFamily = "Somar-SemiBold";
                            futureCalendarPicker.SelectedItemFontFamily = "Somar-SemiBold";
                            futureCalendarPicker.UnSelectedItemFontFamily = "Somar-SemiBold";//ddlLIssuedBy
                        }
                        break;
                    case Xamarin.Forms.Device.Android:

                        CalendarTitle.FontFamily = "GAZT_FONT_MEDIUM";//"Somar-SemiBold.otf#Somar-SemiBold";
                        FutureCalendarTitle.FontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        CalendarDoneButton.FontFamily = "GAZT_FONT_MEDIUM";

                        calendarPicker.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";
                        futureCalendarPicker.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";
                        calendarPicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";
                        futureCalendarPicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";

                        calendarPicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        calendarPicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";//ddlLIssuedBy
                        futureCalendarPicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        futureCalendarPicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";//ddlLIssuedBy
                        break;
                }
            }
            catch (Exception ex)
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

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            SetPickerFont();
        }

        void genericPicker_SelectionChanged(System.Object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
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
            catch (Exception ex)
            {

            }

        }

        void PopupPage_BackgroundClicked(System.Object sender, System.EventArgs e)
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
            await PopupNavigation.Instance.PopAsync();

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
