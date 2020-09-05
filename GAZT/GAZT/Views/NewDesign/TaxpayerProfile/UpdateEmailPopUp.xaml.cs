using EGAZT.ViewModel.NewDesignViewModel.TaxpayerProfileVM;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
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
                bool flag = IsValid(viewModel.NewEmailText);
                if (!flag)
                {
                    ShowValidationPopup(flag);
                    return;
                }

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
            }
        }

        public bool IsValid(string emailaddress)
        {
            bool isEmail = Regex.IsMatch(emailaddress, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (isEmail)
                return true;
            else
                return false;
        }

        // * Password Validations
        private bool TaxpayerProfileEmailUpdateValidation(string CurrentEmail, string NewEmail, string ConfirmEmail)
        {
            string validationError = VerifyEmails(CurrentEmail, NewEmail, ConfirmEmail);

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

        private string VerifyEmails(string CurrentEmail, string NewEmail, string ConfirmEmail)
        {
            int iEmailCompared = String.Compare(NewEmail, ConfirmEmail, true);

            if (string.IsNullOrEmpty(NewEmail))
                return AppResources.ZZPleasefillallthemandatoryfields;
            else if (iEmailCompared != 0)
                return AppResources.NewEmailandRetypeEmailNotMatch;
            else return string.Empty;
        }

        private void ShowValidationPopup(bool validEmailFlag)
        {
            if (!validEmailFlag)
            {
                PopUp popUp = new PopUp();
                popUp.Message = AppResources.ZZPleaseenteravalidEmailAddress;
                popUp.IsLinkAvailable = false;

                if (App.IsArabic)
                    popUp.FlowDirections = "RightToLeft";
                else
                    popUp.FlowDirections = "LeftToRight";

                PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                Frm_NewEmail.HasError = true;
                viewModel.NewEmailText = string.Empty;
            }
            else
                Frm_NewEmail.HasError = false;
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

            // Show existing email
            viewModel.CurrentEmailText = App.TP.Email;
            RefreshControlsData();
        }

        // * Reset Enteried
        private void RefreshControlsData()
        {
            viewModel.NewEmailText = string.Empty;
            viewModel.ConfirmEmailText = string.Empty;

            // Default
            viewModel.IsLoading = false;
        }

        private void NewEmail_Entry_Unfocused(object sender, FocusEventArgs e)
        {
            /*if (!string.IsNullOrEmpty(viewModel.NewEmailText))
            {
                bool flag = IsValid(viewModel.NewEmailText);
                if (!flag)
                {
                    PopUp popUp = new PopUp();
                    popUp.Message = AppResources.ZZPleaseenteravalidEmailAddress;
                    popUp.IsLinkAvailable = false;

                    if (App.IsArabic)
                        popUp.FlowDirections = "RightToLeft";
                    else
                        popUp.FlowDirections = "LeftToRight";

                    PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                    Frm_NewEmail.HasError = true;
                    viewModel.NewEmailText = string.Empty;
                }
                else
                    Frm_NewEmail.HasError = false;
            }*/
        }

        private void BorderlessEntry_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void ConfirmEmail_Entry_Unfocused(object sender, FocusEventArgs e)
        {
            /*if (!string.IsNullOrEmpty(viewModel.ConfirmEmailText))
            {
                bool flag = IsValid(viewModel.ConfirmEmailText);
                if (!flag)
                {
                    PopUp popUp = new PopUp();
                    popUp.Message = AppResources.ZZPleaseenteravalidEmailAddress;
                    popUp.IsLinkAvailable = false;
                    if (App.IsArabic)
                    {
                        popUp.FlowDirections = "RightToLeft";
                    }
                    else
                    {
                        popUp.FlowDirections = "LeftToRight";
                    }
                    PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                    Frm_confirmEmail.HasError = true;
                    viewModel.ConfirmEmailText = string.Empty;
                }
                else
                    Frm_confirmEmail.HasError = false;
            }*/
        }
    }
}