using EGAZT.ViewModel.NewDesignViewModel.TaxpayerProfileVM;
using GAZT.Manager;
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
    public partial class UpdateEmailPopUp : PopupPage
    {
        UpdateEmailViewModel viewModel;

        public UpdateEmailPopUp()
        {
            InitializeComponent();
            viewModel = App.Locator.UpdateEmailPopUp;
            this.BindingContext = viewModel;

            //SetLTR();
            this.FlowDirection = UtilityManager.SetLTRAndRTL();
        }

        private async void UpdatedClicked(object sender, EventArgs e)
        {

            // Call Update Mobile Number API + Go Success Page
            bool callAPIFlag = TaxpayerProfileEmailUpdateValidation(viewModel.CurrentEmailText,
                                                                    viewModel.NewEmailText,
                                                                    viewModel.ConfirmEmailText);
            if (callAPIFlag)
            {
                bool OTPSuccess = await viewModel.VarifyEmail();
                System.Diagnostics.Debug.WriteLine("OTP SUCCESS: ", OTPSuccess);

                if (OTPSuccess)
                {
                    // Navigating to Verification Screen
                    this.CloseAllPopup();

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        // setup updated email's
                        UpdateEmailDataModel updateEmailData = new UpdateEmailDataModel();
                        updateEmailData.CurrentEmail = viewModel.CurrentEmailText;
                        updateEmailData.NewEmail = viewModel.NewEmailText;

                        viewModel._navigationService.NavigateTo(App.VerificationPageView, updateEmailData);
                    });
                }
                else
                {
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                    {
                        await viewModel._dialogService.ShowMessageBox("Wrong Current Email!", AppResources.Information);
                    });
                }
            }
        }

        // * Password Validations
        private bool TaxpayerProfileEmailUpdateValidation(string CurrentEmail, string NewEmail, string ConfirmEmail)
        {
            //CurrentMobileNumber = Regex.Replace(CurrentMobileNumber, @"\s+", "");
            //NewMobileNumber = Regex.Replace(NewMobileNumber, @"\s+", "");

            string validationError = VerifyEmails(CurrentEmail, NewEmail, ConfirmEmail);

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

        private string VerifyEmails(string CurrentEmail, string NewEmail, string ConfirmEmail)
        {
            bool compareStringFlag = string.Equals(NewEmail, ConfirmEmail);

            if (CurrentEmail == null || NewEmail == null
                                        || ConfirmEmail == null
                                        || CurrentEmail == string.Empty
                                        || NewEmail == string.Empty
                                        || ConfirmEmail == string.Empty)
                return AppResources.InvalidEmailFormat;
            else if (!compareStringFlag)
                return AppResources.InvalidEmailFormat;
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

        private async void CloseAllPopup()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }

        async void OnBackArrowTapped(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAllAsync();
        }
    }
}