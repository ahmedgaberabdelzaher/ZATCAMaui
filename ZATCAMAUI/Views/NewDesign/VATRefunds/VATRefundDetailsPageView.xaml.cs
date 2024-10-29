
using Mopups.Services;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Models.VATRefunds;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATRefunds;
using ZATCAMAUI.Views.NewDesign.Common;

namespace ZATCAMAUI.Views.NewDesign.VATRefunds
{

    public partial class VATRefundDetailsPageView : ContentPage
    {
        bool isTandCChecked = false;
        VATRefundDetailsPageViewModel viewModel;
        VatRefundsListResultModel vatRefundsListResultModel;
        VatRefundDisplayDataModel vatRefundsSaveDataModel;


        public VATRefundDetailsPageView(VatRefundsListResultModel vATRefundsModel)
        {
            InitializeComponent();

            viewModel = App.Locator.VATRefundDetailsPageView;
            vatRefundsListResultModel = vATRefundsModel;
            BindingContext = viewModel;
        }

        public VATRefundDetailsPageView(VatRefundDisplayDataModel vATRefundsSaveModel)
        {
            InitializeComponent();

            viewModel = App.Locator.VATRefundDetailsPageView;
            vatRefundsListResultModel = null;
            vatRefundsSaveDataModel = vATRefundsSaveModel;

            BindingContext = viewModel;
            if (viewModel.AcknowledgementChecked && viewModel.CBTermsAndConditionsChecked)
                //btnConfirmSummary.IsEnabled = true;
                viewModel.IsConfirmSummaryEnabled = true;
            else
                viewModel.IsConfirmSummaryEnabled = false;

            //btnConfirmSummary.IsEnabled = false;
            isTandCChecked = false;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();


            try
            {
                if (vatRefundsListResultModel == null)
                {
                    viewModel.IsNewReqSummary = true;
                   await viewModel.LoadSummaryData(vatRefundsSaveDataModel);
                }
                else
                {
                    viewModel.IsNewReqSummary = false;
                   await viewModel.ReloadData(vatRefundsListResultModel);
                }
                MessagingCenter.Subscribe<YesNoAlertPopupView, bool>(this, "YesNoAlertPopupResponse", async (obj, res) =>
                {
                    MopupService.Instance.PopAsync();
                    if (res)
                    {
                        try
                        {
                           await viewModel.ConfirmSummaryBtnClicked();
                        }
                        catch (Exception)
                        {


                        }
                    }
                });
                MessagingCenter.Subscribe<SingleButtonPopupView, bool>(this, "SingleButtonPopupResponse", (obj, res) =>
                {
                    if (res)
                    {
                        isTandCChecked = true;
                        viewModel.CBTermsAndConditionsChecked = true;
                    }
                    else
                        isTandCChecked = false;
                    MopupService.Instance.PopAsync();
                });
            }
            catch (GAZTErrorException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    App.HideProgressView();
                    await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }
            catch (InternetException ex)
            {
                viewModel.IsLoading = false;
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                    viewModel._navigationService.GoBack();
                });
            }
            catch (Exception)
            {


            }

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<YesNoAlertPopupView, bool>(this, "YesNoAlertPopupResponse");
        }

       

        async void ConfirmButton_Tapped(object sender, EventArgs e)
        {
            try
            {
               await viewModel._navigationService.NavigateTo(App.VATRefundsSuccessPageView);
            }
            catch (Exception)
            {


            }
        }

        void ConfirmSummaryButton_Tapped(object sender, EventArgs e)
        {
            string YesButtonText = string.Empty;
            if (App.IsArabic)
                YesButtonText = AppResources.VATRefundRequestConfirmSubmitButtonText;
            else
                YesButtonText = AppResources.AcceptButton;
            MopupService.Instance.PushAsync(new YesNoAlertPopupView(YesButtonText, AppResources.ZZCancel, AppResources.VATRefundReturnSubmitConfirmation));
        }

        public async void VoidButton_Tapped(object sender, EventArgs e)
        {
            if (App.IsArabic)
            {
                var result = await DisplayAlert(AppResources.ZZZConfirmationMsg, AppResources.VATRefundCancelRefund, AppResources.ZNo, AppResources.ZYes);
                if (!result)
                {
                    try
                    {
                        App.DisplayProgressView();

                        await viewModel.OnVoidBtnClicked();



                        var firstPageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                        Navigation.RemovePage(firstPageToRemove);

                        viewModel._navigationService.GoBack();
                    }
                    catch (GAZTErrorException ex)
                    {
                        App.HideProgressView();
                        await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                    }
                    catch (InternetException ex)
                    {
                        App.HideProgressView();

                        await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                        viewModel._navigationService.GoBack();
                    }
                }
            }
            else
            {
                var result = await DisplayAlert(AppResources.ZZZConfirmationMsg, AppResources.VATRefundCancelRefund, AppResources.ZYes, AppResources.ZNo);

                if (result)
                {
                    try
                    {
                        App.DisplayProgressView();

                        await viewModel.OnVoidBtnClicked();

                        App.HideProgressView();

                        var firstPageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                        Navigation.RemovePage(firstPageToRemove);

                        viewModel._navigationService.GoBack();
                    }
                    catch (GAZTErrorException ex)
                    {
                        App.HideProgressView();
                        await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                    }
                    catch (InternetException ex)
                    {
                        App.HideProgressView();
                        await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                        viewModel._navigationService.GoBack();
                    }
                }
            }
        }

        private void Acknowledgment_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (viewModel.AcknowledgementChecked && viewModel.CBTermsAndConditionsChecked)
                // btnConfirmSummary.IsEnabled = true;
                viewModel.IsConfirmSummaryEnabled = true;

            else
                viewModel.IsConfirmSummaryEnabled = false;

            // btnConfirmSummary.IsEnabled = false;
        }

        private void TermsAndConditions_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (viewModel.AcknowledgementChecked && viewModel.CBTermsAndConditionsChecked)
                //btnConfirmSummary.IsEnabled = true;
                viewModel.IsConfirmSummaryEnabled = true;

            else
                //btnConfirmSummary.IsEnabled = false;
                viewModel.IsConfirmSummaryEnabled = false;

            // Display T&C popup on checkbox click if its not checked
            if (!isTandCChecked && viewModel.CBTermsAndConditionsChecked)
            {
                viewModel.CBTermsAndConditionsChecked = false;
                isTandCChecked = false;
                MopupService.Instance.PushAsync(new SingleButtonPopupView(AppResources.AcceptButton, AppResources.VATRefundSummaryTermsandConditions, AppResources.ZVatRefundTermsAndConditions));
            }
            else if (isTandCChecked && !viewModel.CBTermsAndConditionsChecked)
                isTandCChecked = false;
        }

        private void TermsAndConditions_Tapped(object sender, EventArgs e)
        {
            MopupService.Instance.PushAsync(new SingleButtonPopupView(AppResources.ZDone, AppResources.VATRefundSummaryTermsandConditions, AppResources.ZVatRefundTermsAndConditions));
        }

    }
}
