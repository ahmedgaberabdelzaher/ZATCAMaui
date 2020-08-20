using EGAZT.ViewModel.NewDesignViewModel.TaxpayerProfileVM;
using GAZT.Manager;
using GAZT.Models;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.TaxpayerProfile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateMobilePopUp : PopupPage
    {
        UpdateMobileViewModel viewModel;

        public UpdateMobilePopUp()
        {
            InitializeComponent();
            viewModel = App.Locator.UpdateMobilePopUp;
            this.BindingContext = viewModel;

            //SetLTR();
            this.FlowDirection = UtilityManager.SetLTRAndRTL();
        }

        // * Forgot password : OTP Verification :
        void OtpFirstEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (viewModel.OTPFirstDigit.Length > 0) { OTPSecondEntry.Focus(); }
        }

        void OtpSecondEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (viewModel.OTPSecondDigit.Length > 0)
            {
                OTPThirdEntry.Focus();
                return;
            }
            OTPFirstEntry.Focus();
        }

        void OtpThirdEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (viewModel.OTPThirdDigit.Length > 0)
            {
                OTPFourthEntry.Focus();
                return;
            }
            OTPSecondEntry.Focus();
        }

        void OtpFourthEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (viewModel.OTPFourthDigit.Length <= 0) { OTPThirdEntry.Focus(); }
        }

        void OtpFourthEntry_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e) { }
        // * End

        private void UpdatedClicked(object sender, EventArgs e)
        {
            if (btn.Text == "Update") { UpdateMobileNumber(); }
            else { VerifyOTP(); }
        }

        private async void UpdateMobileNumber()
        {
            // Call Update Mobile Number API + Go Success Page
            bool callAPIFlag = TaxpayerProfileUpdateValidation(viewModel.CurrentMobileNumberEntryText, viewModel.NewMobileNumberEntryText);

            if (callAPIFlag)
            {
                bool PWDSuccess = await viewModel.VarifyMobileNumber();
                System.Diagnostics.Debug.WriteLine("OTP SUCCESS: ", PWDSuccess);

                if (PWDSuccess)
                {
                    UpdateMobile.IsVisible = false;
                    VerificationView.IsVisible = true;
                    btn.Text = "Verify";

                    var result = Regex.Match(viewModel.NewMobileNumberEntryText, @"(.{3})\s*$");
                    viewModel.OTPSentOnThisMobileNumber = AppResources.MobileNumber + " ********" + result;

                    // * Start timer period for valid OTP
                    viewModel.StartOTPTimer();
                }
            }
        }

        private async void VerifyOTP()
        {
            viewModel.EnteredOTP = viewModel.OTPFirstDigit
                                    + viewModel.OTPSecondDigit
                                    + viewModel.OTPThirdDigit
                                    + viewModel.OTPFourthDigit;

            if (viewModel.EnteredOTP.Length != 4)
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                {
                    await viewModel._dialogService.ShowMessageBox(AppResources.EnterOTP, AppResources.Information);
                });
                return;
            }

            // * Navigating to Verification Screen
            TaxPayerProfile TPAPIResponse = await viewModel.VarifyOTPToUpdateMobileNumber();
            System.Diagnostics.Debug.WriteLine("TP SUCCESS RESPONSE: ", TPAPIResponse);

            if (TPAPIResponse != null)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    this.CloseAllPopup();
                    viewModel._navigationService.NavigateTo(App.TaxpayerProfileSuccessPage, 2);
                });
            }
        }

        // * Mobile Number Validations
        private bool TaxpayerProfileUpdateValidation(string CurrentMobileNumber, string NewMobileNumber)
        {
            //CurrentMobileNumber = Regex.Replace(CurrentMobileNumber, @"\s+", "");
            //NewMobileNumber = Regex.Replace(NewMobileNumber, @"\s+", "");

            string validationError = VerifyCurrentAndNewMobilenNumbers(CurrentMobileNumber, NewMobileNumber);

            if (validationError == string.Empty)
            {
                return true;
            }
            else
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                {
                    await viewModel._dialogService.ShowMessageBox(validationError, AppResources.Information);
                });
                return false;
            }
        }

        private string VerifyCurrentAndNewMobilenNumbers(string CurrentMobileNumber, string NewMobileNumber)
        {
            //bool compareStringFlag = string.Equals(CurrentMobileNumber, App.TP.Mobile);

            if (CurrentMobileNumber == null || NewMobileNumber == null
                                            || CurrentMobileNumber == string.Empty
                                            || NewMobileNumber == string.Empty)
                return AppResources.EnterValidMobileNumber;
            /*else if (!compareStringFlag)
                return AppResources.EnterValidMobileNumber;*/
            else
                return string.Empty;
        }

        private async void CloseAllPopup()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }

        async void OnBackArrowTapped(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAllAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Show existing mobile
            var result = Regex.Match(App.TP.Mobile, @"(.{9})\s*$");
            viewModel.CurrentMobileNumberEntryText = result.ToString();

            //IsLoading = false;
            RefreshControlsData();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if(viewModel.countDownSeconds != 0 )
                viewModel.otpTimer.Stop();
        }

        // * // Reset Enteried
        private void RefreshControlsData()
        {
            viewModel.OTPFirstDigit = string.Empty;
            viewModel.OTPSecondDigit = string.Empty;
            viewModel.OTPThirdDigit = string.Empty;
            viewModel.OTPFourthDigit = string.Empty;
            viewModel.EnteredOTP = string.Empty;

            viewModel.NewMobileNumberEntryText = string.Empty;
            btn.Text = "Update";
        }

        async void OnResendOTPBtnClicked(System.Object sender, System.EventArgs e)
        {
            if (viewModel.countDownSeconds == 0)
            {
                bool PWDSuccess = await viewModel.VarifyMobileNumber();
                System.Diagnostics.Debug.WriteLine("OTP SUCCESS: ", PWDSuccess);

                if (PWDSuccess)
                {
                    /*UpdateMobile.IsVisible = false;
                    VerificationView.IsVisible = true;
                    btn.Text = "Verify";*/

                    var result = Regex.Match(viewModel.NewMobileNumberEntryText, @"(.{3})\s*$");
                    viewModel.OTPSentOnThisMobileNumber = AppResources.MobileNumber + " ********" + result;

                    // * Start timer period for valid OTP
                    viewModel.StartOTPTimer();
                }
            }
        }
    }
}