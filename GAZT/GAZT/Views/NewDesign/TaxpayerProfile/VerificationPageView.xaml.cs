using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EGAZT.ViewModel.NewDesignViewModel.TaxpayerProfileVM;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesApp.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.TaxpayerProfile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerificationPageView : ContentPage
    {
        VerificationEmailPasswordViewModel viewModel;

        public VerificationPageView(UpdateEmailDataModel updateEmailData)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            viewModel = App.Locator.VerificationPageView;
            this.BindingContext = viewModel;
            ChangeAeroIcon();
            viewModel._updateEmailData = updateEmailData;
            //SetLTR();
            this.FlowDirection = UtilityManager.SetLTRAndRTL();
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
        private async void VerifyBtnClicked(object sender, EventArgs e)
        {
            // Call Update Mobile Number API + Go Success Page
            viewModel.EnteredOTP = viewModel.OTPFirstDigit
                                    + viewModel.OTPSecondDigit
                                    + viewModel.OTPThirdDigit
                                    + viewModel.OTPFourthDigit;

            if (viewModel.EnteredOTP.Length != 4)
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                {
                    await viewModel._dialogService.ShowMessageBox(AppResources.PleaseenterOTP, AppResources.Information);
                });
                return;
            }

            bool callAPIFlag = TaxpayerProfileOTPEmailUpdateValidation(viewModel.CurrentPasswordEntry,
                                                                        viewModel.NewPasswordEntry,
                                                                        viewModel.ConfirmPasswordEntry);
            if (callAPIFlag)
            {
                //viewModel.LoadingStart();
                TaxPayerProfile TPAPIResponse = await viewModel.ChangePassword();
                System.Diagnostics.Debug.WriteLine("TP SUCCESS RESPONSE: ", TPAPIResponse);

                if (TPAPIResponse != null)
                {
                    //viewModel.LoadingStop();
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        viewModel._navigationService.NavigateTo(App.TaxpayerProfileSuccessPage, 1);
                    });
                }
            }
        }

        // * Password + OTP Validations
        private bool TaxpayerProfileOTPEmailUpdateValidation(string CurrentPassword, string NewPassword, string ConfirmPassword)
        {
            string validationError = VerifyOTPPasswords(CurrentPassword, NewPassword, ConfirmPassword);

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

        private string VerifyOTPPasswords(string CurrentPassword, string NewPassword, string ConfirmPassword)
        {
            /*bool compareStringFlag = string.Equals(NewPassword, ConfirmPassword);

            if (CurrentPassword == null || NewPassword == null
                                        || ConfirmPassword == null
                                        || CurrentPassword == string.Empty
                                        || NewPassword == string.Empty
                                        || ConfirmPassword == string.Empty
                                        || viewModel.EnteredOTP.Length != 4)
                return AppResources.InvalidPassword;
            else if (!compareStringFlag)
                return AppResources.NewPasswordandRetypePasswordNotMatch;
            else
            {
                bool passwordValidationRegXFlag = UtilityManager.ValidateNewPasswordForTP(NewPassword);
                if (passwordValidationRegXFlag)
                    return string.Empty;
                else
                    return AppResources.PasswordGuidelineText;
            }*/

            bool compareStringFlag = string.Equals(NewPassword, ConfirmPassword);

            if (string.IsNullOrEmpty(CurrentPassword))
                return AppResources.TPOldPasswordEmpty;
            else if (string.IsNullOrEmpty(NewPassword))
                return AppResources.TPNewPasswordEmpty;
            else if (!compareStringFlag)
                return AppResources.NewPasswordandRetypePasswordNotMatch;
            else
            {
                bool passwordValidationRegXFlag = UtilityManager.ValidateNewPasswordForTP(NewPassword);
                if (passwordValidationRegXFlag)
                    return string.Empty;
                else
                    return AppResources.PasswordValidationMesseg;
            }
        }

        async void OnResendOTPBtnClicked(object sender, EventArgs e)
        {
            if( viewModel.countDownSeconds == 0 )
            {
                viewModel.BtnEnableFlag = false;
                viewModel.EnteredOTP = string.Empty;
                viewModel.OTPFirstDigit = string.Empty;
                viewModel.OTPSecondDigit = string.Empty;
                viewModel.OTPThirdDigit = string.Empty;
                viewModel.OTPFourthDigit = string.Empty;

                viewModel.StartOTPTimer();
                bool OTPSuccess = await viewModel.VarifyEmail();
                System.Diagnostics.Debug.WriteLine("OTP SUCCESS: ", OTPSuccess);
            }
        }

        // * Forgot password : OTP Verification :
        void OtpFirstEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (viewModel.OTPFirstDigit.Length == 1) { OTPSecondEntry.Focus(); }
        }

        void OtpSecondEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (viewModel.OTPSecondDigit.Length == 1) { OTPThirdEntry.Focus(); }
            else if (viewModel.OTPSecondDigit.Length == 0) { OTPFirstEntry.Focus(); }
        }

        void OtpThirdEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (viewModel.OTPThirdDigit.Length == 1) { OTPFourthEntry.Focus(); }
            else if (viewModel.OTPThirdDigit.Length == 0) { OTPSecondEntry.Focus(); }
        }

        void OtpFourthEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            // Work around
            if (viewModel.OTPFirstDigit.Length == 0) { OTPFirstEntry.Focus(); }
            else
                if (viewModel.OTPFourthDigit.Length == 0) { OTPThirdEntry.Focus(); }
        }

        void OtpFourthEntry_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (viewModel.OTPFourthDigit.Length != 0)
                viewModel.BtnEnableFlag = true;
        }
        // * End

        // * Current Password - New Password - Confirm New Password : Show / Hide
        void OnCurrentPasswordTapped(object sender, EventArgs args)
        {
            ShowHidePassword(CurrentPassword, CurrentPasswordImg);
        }

        void OnNewPasswordTapped(object sender, EventArgs args)
        {
            ShowHidePassword(NewPassword, NewPasswordImg);
        }

        void OnConfirmPasswordTapped(object sender, EventArgs args)
        {
            ShowHidePassword(ConfirmPassword, ConfirmPasswordImg);
        }

        // * Common Method Fror Show Hide PWD
        private void ShowHidePassword(BorderlessEntry passwordEntry, Image image)
        {
            if (passwordEntry.IsPassword == true)
            {
                passwordEntry.IsPassword = false;
                image.Source = "showPassword.png";
            }
            else
            {
                passwordEntry.IsPassword = true;
                image.Source = "hidePassword.png";
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.OTPSentOnThisMobileNumber = AppResources.Email + " " + viewModel._updateEmailData.NewEmail;
            viewModel.StartOTPTimer();

            RefreshControlsData();

            // * Set
            OTPThirdEntry.Unfocus();

            // * Show alert for OTP sent to given email id
            Device.BeginInvokeOnMainThread(async () =>
            {
                await viewModel._dialogService.ShowMessageBox(AppResources.TPOTPSentToEmail, AppResources.Information);
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            // * Worka aroung - Need to find a solution
            if (viewModel.countDownSeconds != 0)
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

            // Defualt
            viewModel.BtnEnableFlag = false;
            viewModel.IsLoading = false;

            viewModel.CurrentPasswordEntry = string.Empty;
            viewModel.NewPasswordEntry = string.Empty;
            viewModel.ConfirmPasswordEntry = string.Empty;
        }

        void OnBackArrowTapped(System.Object sender, System.EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.GoBack();
            });
        }
    }
}