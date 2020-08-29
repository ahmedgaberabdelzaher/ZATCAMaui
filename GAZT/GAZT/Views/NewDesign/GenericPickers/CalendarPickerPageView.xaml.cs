using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.CalendarPickerPageViewModel;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.GenericPickers
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
            viewModel.SetDefaultDate();
            viewModel.IsFutureDatePickerVisible = false;
            viewModel.IsCurrentDatePickerVisible = true;
            
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
        }
        public CalendarPickerPageView(GenericDatePickerModel _pickerSource)
        {
            InitializeComponent();

            viewModel = App.Locator.CalendarPickerPageView;
            viewModel.IsFutureDatePickerVisible = false;
            viewModel.IsCurrentDatePickerVisible = true;
            viewModel.SetDefaultDate();
            viewModel.DataSource = _pickerSource;
            viewModel.PickerItemSource = viewModel.DataSource.PickerData;
            viewModel.DatePickerTitle = viewModel.DataSource.DatePickerTitle;

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
        }

        public CalendarPickerPageView(GenericDatePickerModel _pickerSource, bool isFuturePickerVisible)
        {
            InitializeComponent();

            viewModel = App.Locator.CalendarPickerPageView;
            viewModel.IsFutureDatePickerVisible = true;
            viewModel.IsCurrentDatePickerVisible = false;
            viewModel.SetDefaultDate();
            viewModel.DataSource = _pickerSource;
            viewModel.PickerItemSource = viewModel.DataSource.PickerData;
            viewModel.DatePickerTitle = viewModel.DataSource.DatePickerTitle;

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
        }

        void genericPicker_SelectionChanged(System.Object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                if(viewModel.IsFutureDatePickerVisible == true)
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
    }
}
