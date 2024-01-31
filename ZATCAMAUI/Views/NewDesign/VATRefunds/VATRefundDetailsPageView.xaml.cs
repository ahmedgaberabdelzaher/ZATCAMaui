using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using RGPopup.Maui.Services;
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

            ChangeAeroIcon();
            SetLTR();
            On<iOS>().SetUseSafeArea(true);
            BindingContext = viewModel;
        }

        public VATRefundDetailsPageView(VatRefundDisplayDataModel vATRefundsSaveModel)
        {
            InitializeComponent();

            viewModel = App.Locator.VATRefundDetailsPageView;
            vatRefundsListResultModel = null;
            vatRefundsSaveDataModel = vATRefundsSaveModel;

            ChangeAeroIcon();
            SetLTR();
            On<iOS>().SetUseSafeArea(true);
            BindingContext = viewModel;
            if (viewModel.AcknowledgementChecked && viewModel.CBTermsAndConditionsChecked)
                //btnConfirmSummary.IsEnabled = true;
                viewModel.IsConfirmSummaryEnabled = true;
            else
                viewModel.IsConfirmSummaryEnabled = false;

            //btnConfirmSummary.IsEnabled = false;
            isTandCChecked = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;

            ChangeArrowDirection();

            try
            {
                if (vatRefundsListResultModel == null)
                {
                    viewModel.IsNewReqSummary = true;
                    viewModel.LoadSummaryData(vatRefundsSaveDataModel);
                }
                else
                {
                    viewModel.IsNewReqSummary = false;
                    viewModel.ReloadData(vatRefundsListResultModel);
                }
                MessagingCenter.Subscribe<YesNoAlertPopupView, bool>(this, "YesNoAlertPopupResponse", (obj, res) =>
                {
                    PopupNavigation.Instance.PopAsync();
                    if (res)
                    {
                        try
                        {
                            viewModel.ConfirmSummaryBtnClicked();
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
                    PopupNavigation.Instance.PopAsync();
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
                Task.Run(() =>
                {
                    App.HideProgressView();
                });

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

        private void SetLTR()
        {
            if (App.IsArabic)
            {
                FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                FlowDirection = FlowDirection.LeftToRight;
            }
        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
        }

        public void ChangeArrowDirection()
        {
            if (App.IsArabic)
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {

                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
        }

        void ConfirmButton_Tapped(object sender, EventArgs e)
        {
            try
            {
                viewModel._navigationService.NavigateTo(App.VATRefundsSuccessPageView);
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
            PopupNavigation.Instance.PushAsync(new YesNoAlertPopupView(YesButtonText, AppResources.ZZCancel, AppResources.VATRefundReturnSubmitConfirmation));
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
                        await Task.Run(() =>
                        {
                            App.DisplayProgressView();
                        });

                        await viewModel.OnVoidBtnClicked();



                        var firstPageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                        Navigation.RemovePage(firstPageToRemove);

                        viewModel._navigationService.GoBack();
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
                        await Task.Run(() =>
                        {
                            App.HideProgressView();
                        });

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
            }
            else
            {
                var result = await DisplayAlert(AppResources.ZZZConfirmationMsg, AppResources.VATRefundCancelRefund, AppResources.ZYes, AppResources.ZNo);

                if (result)
                {
                    try
                    {
                        await Task.Run(() =>
                        {
                            App.DisplayProgressView();
                        });

                        await viewModel.OnVoidBtnClicked();

                        await Task.Run(() =>
                        {
                            App.HideProgressView();
                        });

                        var firstPageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                        Navigation.RemovePage(firstPageToRemove);

                        viewModel._navigationService.GoBack();
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
                        await Task.Run(() =>
                        {
                            App.HideProgressView();
                        });

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
                PopupNavigation.Instance.PushAsync(new SingleButtonPopupView(AppResources.AcceptButton, AppResources.VATRefundSummaryTermsandConditions, AppResources.ZVatRefundTermsAndConditions));
            }
            else if (isTandCChecked && !viewModel.CBTermsAndConditionsChecked)
                isTandCChecked = false;
        }

        private void TermsAndConditions_Tapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new SingleButtonPopupView(AppResources.ZDone, AppResources.VATRefundSummaryTermsandConditions, AppResources.ZVatRefundTermsAndConditions));
        }

        void btnConfirmSummary_Clicked(object sender, EventArgs e)
        {
        }
    }
}
