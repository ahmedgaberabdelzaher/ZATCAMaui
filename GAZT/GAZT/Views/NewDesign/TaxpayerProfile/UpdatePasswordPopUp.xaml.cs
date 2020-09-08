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
            try
            {
                /*if (!string.IsNullOrEmpty(viewModel.CurrentPasswordEntry) && !string.IsNullOrEmpty(viewModel.NewPasswordEntry))
                {*/
                string lang = "EN";
                if (App.IsArabic == true) { lang = "AR"; }
                WebServiceManager.ErrorMessage = string.Empty;
                // Call Update Mobile Number API + Go Success Page
                bool callAPIFlag = TaxpayerProfilePasswordUpdateValidation(viewModel.CurrentPasswordEntry,
                                                                            viewModel.NewPasswordEntry,
                                                                            viewModel.ConfirmPasswordEntry);
                if (callAPIFlag)
                {

                    bool APIResponse = await WebServiceManager.GAZTValidateAndChangePassword(lang,
                                                                App.TP.Tin,
                                                                viewModel.CurrentPasswordEntry,
                                                                viewModel.NewPasswordEntry);
                    if (APIResponse)
                    {
                        // * Navigating to Verification Screen
                        this.CloseAllPopup();

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            viewModel._navigationService.NavigateTo(App.TaxpayerProfileSuccessPage, 3);
                        });
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(WebServiceManager.ErrorMessage))
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await viewModel._dialogService.ShowMessageBox(WebServiceManager.ErrorMessage, AppResources.Information);
                            });
                        }
                        else
                        {
                            String OnInvalidPassword = AppResources.InvalidPassword;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await viewModel._dialogService.ShowMessageBox(OnInvalidPassword, AppResources.Information);
                            });
                        }
                    }
                }
                /*}
                else
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await viewModel._dialogService.ShowMessageBox(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Information);
                    });
                }*/
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await viewModel._dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                    //ClearPasswordDataForEmail();
                });
            }
        }

        // * Password Validations
        private bool TaxpayerProfilePasswordUpdateValidation(string CurrentPassword, string NewPassword, string ConfirmPassword)
        {
            string validationError = VerifyPasswords(CurrentPassword, NewPassword, ConfirmPassword);

            if (validationError == string.Empty)
                return true;
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
            bool CompareNewandOldPassword = string.Equals(CurrentPassword,NewPassword);
            if (string.IsNullOrEmpty(CurrentPassword))
                return AppResources.TPOldPasswordEmpty;
            else if (string.IsNullOrEmpty(NewPassword))
                return AppResources.TPNewPasswordEmpty;
            else if (!compareStringFlag)
                return AppResources.NewPasswordandRetypePasswordNotMatch;
            else if (CompareNewandOldPassword)
                return AppResources.NDOldPasswordAndNewPasswordSame;
            else
            {
                bool passwordValidationRegXFlag = UtilityManager.ValidateNewPasswordForTP(NewPassword);
                if (passwordValidationRegXFlag)
                    return string.Empty;
                else
                    return AppResources.PasswordValidationMesseg;
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

            // Default
            viewModel.IsLoading = false;
        }
    }
}