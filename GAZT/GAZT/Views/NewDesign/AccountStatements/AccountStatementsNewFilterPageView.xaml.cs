using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.AccountStatements;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Views.NewDesign.GenericPickers;
using GAZT.Helper;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using SelectionChangedEventArgs = Syncfusion.SfPicker.XForms.SelectionChangedEventArgs;

namespace EGAZT.Views.NewDesign.AccountStatements
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [Preserve(AllMembers = true)]
    public partial class AccountStatementsNewFilterPageView : ContentPage
    {
        AccountStatementsPageViewModel viewModel;
        
        public AccountStatementsNewFilterPageView()
        {
            InitializeComponent();
            ChangeAeroIcon();
            ChangeFilterArrow();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            viewModel = App.Locator.AccountStatementsFilterPageView;

            this.BindingContext = viewModel;
            viewModel.TodayDateNormal = null;
            viewModel.TodayDateinHijri = null;
            viewModel.IsHijriCal = false;
            viewModel.TxFromDate = "";
            viewModel.TxToDate = "";
            viewModel.TPFromDate = "";
            viewModel.TPToDate = "";
            viewModel.SetDefaultDate();
            viewModel.IsHijriCal = (viewModel.HeaderSet.D.CalType != "G");

        }
        
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
        }

        public void ChangeAeroIcon()
        {
            if (!App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }

        public void ChangeFilterArrow()
        {
            if (App.IsArabic)
            {
                Resources["FilterArrow"] = Resources["FilterArrowImageForArabicStyle"];
            }
            else
            {
                Resources["FilterArrow"] = Resources["FilterArrowImageForEnglishStyle"];
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;

            
        }

        /*protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem",
                (sender, arg) =>
                {

                    if (arg.PickerId == "TSStartDateTypePicker")
                    {
                        viewModel.TxFromDate = Convert.ToDateTime(arg.SelectedValue);
                    }
                    else if (arg.PickerId == "TSEndDateTypePicker")
                    {
                        viewModel.TxToDate = Convert.ToDateTime(arg.SelectedValue);
                    }else if (arg.PickerId == "TaxPeriodStartDateTypePicker")
                    {
                        viewModel.TPFromDate = Convert.ToDateTime(arg.SelectedValue);
                    }
                    else if (arg.PickerId == "TaxPeriodEndDateTypePicker")
                    {
                        viewModel.TPToDate = Convert.ToDateTime(arg.SelectedValue);
                    }


                });

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem");

        }*/

        private async void TSDateStartDateClicked(object sender, EventArgs e)
        {
            viewModel.isTxStartDate = true;
            /*GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
            genericDatePickerModel.DatePickerTitle = AppResources.VatDeregStartDatePickerTitle;
            genericDatePickerModel.PickerId = "TSStartDateTypePicker";
            try
            {
                await PopupNavigation.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel,true));
            }
            catch (GAZTUnlockAccountException ex)
            {

            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                    // await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                    viewModel._navigationService.GoBack();
                });
            }*/
            if (viewModel.IsHijriCal)
            {
                TxDateHijriCalendar.IsOpen = true;
            }
            else
            {
                TxDateNormalCalendar.IsOpen = true;
            }
        }

        private async void TSDateEndDateClicked(object sender, EventArgs e)
        {
            viewModel.isTxStartDate = false;
            /*GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
            genericDatePickerModel.DatePickerTitle = AppResources.VatDeregEndDatePickerTitle;
            genericDatePickerModel.PickerId = "TSEndDateTypePicker";
            try
            {
                await PopupNavigation.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel,true));
            }
            catch (GAZTUnlockAccountException ex)
            {

            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                    //await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                    viewModel._navigationService.GoBack();
                });
            }*/
            if (viewModel.IsHijriCal)
            {
                TxDateHijriCalendar.IsOpen = true;
            }
            else
            {
                TxDateNormalCalendar.IsOpen = true;
            }
        }

        private async void TaxPeriodStartDateClicked(object sender, EventArgs e)
        {
            viewModel.isTaxPeriodStartDate = true;
            /*GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
            genericDatePickerModel.DatePickerTitle = AppResources.VatDeregStartDatePickerTitle;
            genericDatePickerModel.PickerId = "TaxPeriodStartDateTypePicker";
            try
            {
                await PopupNavigation.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel,true));
            }
            catch (GAZTUnlockAccountException ex)
            {

            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                    // await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                    viewModel._navigationService.GoBack();
                });
            }*/
            if (viewModel.IsHijriCal)
            {
                TaxPeriodDateHijriCalendar.IsOpen = true;
            }
            else
            {
                TaxPeriodDateNormalCalendar.IsOpen = true;
            }
        }

        private async void TaxPeriodEndDateClicked(object sender, EventArgs e)
        {
            viewModel.isTaxPeriodStartDate = false;
            /*GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
            genericDatePickerModel.DatePickerTitle = AppResources.VatDeregEndDatePickerTitle;
            genericDatePickerModel.PickerId = "TaxPeriodEndDateTypePicker";
            try
            {
                await PopupNavigation.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel,true));
            }
            catch (GAZTUnlockAccountException ex)
            {

            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                    //await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                    viewModel._navigationService.GoBack();
                });
            }*/
            if (viewModel.IsHijriCal)
            {
                TaxPeriodDateHijriCalendar.IsOpen = true;
            }
            else
            {
                TaxPeriodDateNormalCalendar.IsOpen = true;
            }
        }

        private void From_Amount_Unfocused(object sender, FocusEventArgs e)
        {
            viewModel.FromTxAmount = TxFromAmountEntry.Text;
        }

        private void From_Amount_Changed(object sender, TextChangedEventArgs e)
        {
            viewModel.FromTxAmount = TxFromAmountEntry.Text;
        }

        private void To_Amount_Unfocused(object sender, FocusEventArgs e)
        {
            viewModel.ToTxAmount = TxToAmountEntry.Text;
        }

        private void To_Amount_Changed(object sender, TextChangedEventArgs e)
        {
            viewModel.ToTxAmount = TxToAmountEntry.Text;
        }

        private void TxDateNormalCalendar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void TxDateNormalCalendar_OkButtonClicked(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void TxDateNormalCalendar_CancelButtonClicked(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void TxDateNormalCalendar_Closed(object sender, EventArgs e)
        {
            try
            {
                if (viewModel.IsHijriCal)
                {
                    if (TxDateHijriCalendar.SelectedItem != null)
                    {
                        var selectedItem = TxDateHijriCalendar.SelectedItem as ObservableCollection<object>;
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
                        if (viewModel.isTxStartDate)
                        {
                            viewModel.TxFromDate = year + "/" + month + "/" + day;   
                        }
                        else
                        {
                            viewModel.TxToDate = year + "/" + month + "/" + day;
                        }
                    }
                }
                else
                {
                    if (TxDateNormalCalendar.SelectedItem != null)
                    {
                        var selectedItem = TxDateNormalCalendar.SelectedItem as ObservableCollection<object>;
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
                        
                        if (viewModel.isTxStartDate)
                        {
                            viewModel.TxFromDate = year + "/" + month + "/" + day;   
                        }
                        else
                        {
                            viewModel.TxToDate = year + "/" + month + "/" + day;
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }

        }

        private void TaxPeriodDateNormalCalendar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void TaxPeriodDateNormalCalendar_Closed(object sender, EventArgs e)
        {
            try
            {
                if (viewModel.IsHijriCal)
                {
                    if (TaxPeriodDateHijriCalendar.SelectedItem != null)
                    {
                        var selectedItem = TaxPeriodDateHijriCalendar.SelectedItem as ObservableCollection<object>;
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
                        //viewModel.FromDate = year + "/" + month + "/" + day;
                        if (viewModel.isTaxPeriodStartDate)
                        {
                            viewModel.TPFromDate = year; //+ "/" + month + "/" + day;   
                        }
                        else
                        {
                            viewModel.TPToDate = year; // + "/" + month + "/" + day;
                        }
                    }
                }
                else
                {
                    if (TaxPeriodDateNormalCalendar.SelectedItem != null)
                    {
                        var selectedItem = TaxPeriodDateNormalCalendar.SelectedItem as ObservableCollection<object>;
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
                        //viewModel.FromDate = year + "/" + month + "/" + day;
                        if (viewModel.isTaxPeriodStartDate)
                        {
                            viewModel.TPFromDate = year; // + "/" + month + "/" + day;   
                        }
                        else
                        {
                            viewModel.TPToDate = year; // + "/" + month + "/" + day;
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
   
        }

        private void TaxPeriodDateNormalCalendar_OkButtonClicked(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void TaxPeriodDateNormalCalendar_CancelButtonClicked(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}