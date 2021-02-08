using System;
using System.Collections.Generic;
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
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.AccountStatements
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountStatementsNewFilterPageView : ContentPage
    {
        AccountStatementsPageViewModel viewModel;
        
        public AccountStatementsNewFilterPageView()
        {
            InitializeComponent();
            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem",
                (sender, arg) =>
                {

                    if (arg.PickerId == "TSStartDateTypePicker")
                    {
                       // viewModel.FromDate = Convert.ToDateTime(arg.SelectedValue);
                    }
                    else if (arg.PickerId == "TSEndDateTypePicker")
                    {
                       // viewModel.ToDate = Convert.ToDateTime(arg.SelectedValue);
                    }else if (arg.PickerId == "TaxPeriodStartDateTypePicker")
                    {
                        //viewModel.FromDate = Convert.ToDateTime(arg.SelectedValue);
                    }
                    else if (arg.PickerId == "TaxPeriodEndDateTypePicker")
                    {
                    //    viewModel.ToDate = Convert.ToDateTime(arg.SelectedValue);
                    }


                });

        }

        private async void TSDateStartDateClicked(object sender, EventArgs e)
        {
            GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
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
            }
        }

        private async void TSDateEndDateClicked(object sender, EventArgs e)
        {
            GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
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
            }
        }

        private async void TaxPeriodStartDateClicked(object sender, EventArgs e)
        {
            GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
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
            }
        }

        private async void TaxPeriodEndDateClicked(object sender, EventArgs e)
        {
            GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
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
            }
        }

        private void From_Amount_Unfocused(object sender, FocusEventArgs e)
        {
            
        }

        private void From_Amount_Changed(object sender, TextChangedEventArgs e)
        {
            
        }

        private void To_Amount_Unfocused(object sender, FocusEventArgs e)
        {
            
        }

        private void To_Amount_Changed(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}