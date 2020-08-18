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

            viewModel._updateEmailData = updateEmailData;
            //SetLTR();
            this.FlowDirection = UtilityManager.SetLTRAndRTL();
        }

        private async void VerifyBtnClicked(object sender, EventArgs e)
        {
            // Call Update Mobile Number API + Go Success Page
            viewModel.EnteredOTP = viewModel.OTPFirstDigit
                                    + viewModel.OTPSecondDigit
                                    + viewModel.OTPThirdDigit
                                    + viewModel.OTPFourthDigit;

            bool callAPIFlag = TaxpayerProfileOTPEmailUpdateValidation(viewModel.CurrentPasswordEntry,
                                                                        viewModel.NewPasswordEntry,
                                                                        viewModel.ConfirmPasswordEntry);

            if (callAPIFlag)
            {
                // Navigating to Verification Screen
                TaxPayerProfile TPAPIResponse = await viewModel.ChangePassword();
                System.Diagnostics.Debug.WriteLine("TP SUCCESS RESPONSE: ", TPAPIResponse);

                if (TPAPIResponse != null)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        viewModel._navigationService.NavigateTo(App.TaxpayerProfileSuccessPage, 1);
                    });
                }
                else
                {
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                    {
                        await viewModel._dialogService.ShowMessageBox("Wrong Current Password!", AppResources.Information);
                    });
                }
            }
        }

        // * Password + OTP Validations
        private bool TaxpayerProfileOTPEmailUpdateValidation(string CurrentPassword, string NewPassword, string ConfirmPassword)
        {
            //CurrentMobileNumber = Regex.Replace(CurrentMobileNumber, @"\s+", "");
            //NewMobileNumber = Regex.Replace(NewMobileNumber, @"\s+", "");

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
            //bool compareStringFlag = string.Equals(CurrentPassword, "Password@2");

            if (CurrentPassword == null || NewPassword == null
                                        || ConfirmPassword == null
                                        || CurrentPassword == string.Empty
                                        || NewPassword == string.Empty
                                        || ConfirmPassword == string.Empty
                                        || viewModel.EnteredOTP.Length != 4)
                return AppResources.InvalidPassword;
            /*else if (!compareStringFlag)
                return AppResources.InvalidPassword;*/
            else
            {
                /*bool passwordValidationRegXFlag = UtilityManager.ValidateNewPassword(NewPassword);
                if (passwordValidationRegXFlag)
                {
                    return string.Empty;
                }
                else
                {
                    return "Password Not Matches As Expected!!";
                }*/

                return string.Empty;
            }
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

            // * Start timer period for valid OTP
            //var result = Regex.Match(App.TP.Mobile, @"(.{3})\s*$");
            viewModel.NewMobileNumberLabel = "Mobile Number "
                                              + "xxxxxxx"
                                              + "388";
            viewModel.StartOTPTimer();
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