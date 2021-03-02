using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EGAZT.Models.VATRefunds;
using EGAZT.ViewModel.NewDesignViewModel.VATRefunds;
using EGAZT.Views.NewDesign.Common;
using GAZT.Helper;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.VATRefunds
{
    [Preserve(AllMembers = true)]
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
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
        }

        public VATRefundDetailsPageView(VatRefundDisplayDataModel vATRefundsSaveModel)
        {
            InitializeComponent();

            viewModel = App.Locator.VATRefundDetailsPageView;
            vatRefundsListResultModel = null;
            vatRefundsSaveDataModel = vATRefundsSaveModel;

            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
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
            this.Padding = safeInsets;

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
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
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
                Device.BeginInvokeOnMainThread(async () =>
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

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                    viewModel._navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                this.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

        public async void VoidButton_Tapped(System.Object sender, System.EventArgs e)
        {
            if (App.IsArabic)
            {
                var result = await this.DisplayAlert(AppResources.ZZZConfirmationMsg, AppResources.VATRefundCancelRefund, AppResources.ZNo, AppResources.ZYes);
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
                        Device.BeginInvokeOnMainThread(async () =>
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

                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                            viewModel._navigationService.GoBack();
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            else
            {
                var result = await this.DisplayAlert(AppResources.ZZZConfirmationMsg, AppResources.VATRefundCancelRefund, AppResources.ZYes, AppResources.ZNo);

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
                        Device.BeginInvokeOnMainThread(async () =>
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

                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                            viewModel._navigationService.GoBack();
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
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

        void btnConfirmSummary_Clicked(System.Object sender, System.EventArgs e)
        {
        }
    }
}
