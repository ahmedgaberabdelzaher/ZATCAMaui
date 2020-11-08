using System;
using System.Collections.Generic;
using EGAZT.Models;
using EGAZT.Models.AccountStatements;
using EGAZT.ViewModel.NewDesignViewModel.AccountStatements;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Views.NewDesign.GenericPickers;
using GAZT.Helper;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.AccountStatements
{
    public partial class AccountStatementsDownloadPageView : ContentPage
    {
        AccountStatementsDownloadPageViewModel viewModel;

        public AccountStatementsDownloadPageView(ASTaxpayerSelectedValues aSTaxpayerSelectedValues)
        {
            InitializeComponent();

            viewModel = App.Locator.AccountStatementsDownloadPageView;
            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            viewModel.ASTaxpayerSelectedValues = aSTaxpayerSelectedValues;

            viewModel.FromDate = AppResources.ASAccountStatementFrom;
            viewModel.ToDate = AppResources.ASAccountStatementTo;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

           

            MessagingCenter.Subscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem", (sender, arg) =>
            {
                if (App.IsArabic)
                {
                    if (arg.PickerId == "StartDateTypePicker")
                    {
                        viewModel.FromDate = Convert.ToDateTime(arg.SelectedValue).ToShortDateString();
                    }
                    else if (arg.PickerId == "EndDateTypePicker")
                    {
                        viewModel.ToDate = Convert.ToDateTime(arg.SelectedValue).ToShortDateString();
                    }
                }
                else
                {
                    if (arg.DatePickerTitle.Contains(AppResources.ASAccountStatementFrom))
                    {
                        viewModel.FromDate = Convert.ToDateTime(arg.SelectedValue).ToShortDateString();
                    }
                    else if (arg.DatePickerTitle.Contains(AppResources.ASAccountStatementTo))
                    {
                        viewModel.ToDate = Convert.ToDateTime(arg.SelectedValue).ToShortDateString();

                    }
                }
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem");
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

        async void StartDateClicked(System.Object sender, System.EventArgs e)
        {

            GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
            genericDatePickerModel.DatePickerTitle = AppResources.ASAccountStatementFrom;
            genericDatePickerModel.PickerId = "StartDateTypePicker";
            try
            {
                await PopupNavigation.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel, true));
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
        async void EndDateClicked(System.Object sender, System.EventArgs e)
        {

            GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
            genericDatePickerModel.DatePickerTitle = AppResources.ASAccountStatementTo;
            genericDatePickerModel.PickerId = "EndDateTypePicker";
            try
            {
                await PopupNavigation.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel, true));
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

        
    }
}
