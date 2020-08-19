using EGAZT.ViewModel.NewDesignViewModel.TaxpayerProfileVM;
using GAZT.Manager;
using GAZTeServicesApp.Controls;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.TaxpayerProfile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdatePasswordPopUp : PopupPage
    {
        UpdatePasswordViewModel viewModel;

        public UpdatePasswordPopUp()
        {
            InitializeComponent();
            viewModel = App.Locator.UpdatePasswordPopUp;
            this.BindingContext = viewModel;

            //SetLTR();
            this.FlowDirection = UtilityManager.SetLTRAndRTL();
        }

        async void OnUpdateBtnClicked(System.Object sender, System.EventArgs e)
        {
            // Call Update Mobile Number API + Go Success Page
            bool callAPIFlag = TaxpayerProfilePasswordUpdateValidation(viewModel.CurrentPasswordEntry,
                                                                        viewModel.NewPasswordEntry,
                                                                        viewModel.ConfirmPasswordEntry);
            if (callAPIFlag)
            {
                bool PWDSuccess = await viewModel.ChangePassword();
                System.Diagnostics.Debug.WriteLine("OTP SUCCESS: ", PWDSuccess);

                if (PWDSuccess)
                {
                    // * Navigating to Verification Screen
                    this.CloseAllPopup();

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        viewModel._navigationService.NavigateTo(App.TaxpayerProfileSuccessPage, 3);
                    });
                }
            }
        }

        // * Password Validations
        private bool TaxpayerProfilePasswordUpdateValidation(string CurrentPassword, string NewPassword, string ConfirmPassword)
        {
            //CurrentMobileNumber = Regex.Replace(CurrentMobileNumber, @"\s+", "");
            //NewMobileNumber = Regex.Replace(NewMobileNumber, @"\s+", "");

            string validationError = VerifyPasswords(CurrentPassword, NewPassword, ConfirmPassword);

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

        private string VerifyPasswords(string CurrentPassword, string NewPassword, string ConfirmPassword)
        {
            bool compareStringFlag = string.Equals(NewPassword, ConfirmPassword);

            if (CurrentPassword == null || NewPassword == null
                                        || ConfirmPassword == null
                                        || CurrentPassword == string.Empty
                                        || NewPassword == string.Empty
                                        || ConfirmPassword == string.Empty)
                return AppResources.PasswordValidationMesseg;
            else if (!compareStringFlag)
                return AppResources.NewPasswordandRetypePasswordNotMatch;
            else
            {
                bool passwordValidationRegXFlag = UtilityManager.ValidateNewPasswordForTP(NewPassword);
                if (passwordValidationRegXFlag)
                {
                    return string.Empty;
                }
                else
                {
                    return AppResources.InvalidPassword;
                }
            }
        }

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

            RefreshControlsData();
        }

        // * // Reset Enteried
        private void RefreshControlsData()
        {
            viewModel.CurrentPasswordEntry = string.Empty;
            viewModel.NewPasswordEntry = string.Empty;
            viewModel.ConfirmPasswordEntry = string.Empty;
        }
    }
}