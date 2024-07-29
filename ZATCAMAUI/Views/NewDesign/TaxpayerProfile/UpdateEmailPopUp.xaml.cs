using Mopups.Pages;
using Mopups.Services;
using System.Globalization;
using System.Text.RegularExpressions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.TPProfile;
using ZATCAMAUI.ViewModel.NewDesignViewModel.TaxpayerProfileVM;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.Views.NewDesign.TaxpayerProfile
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
                    viewModel.ShowValidationPopup(AppResources.ZZPleaseenteravalidEmailAddress);
                    return;
                }

                //TaxPayerProfile TPAPIResponse = await viewModel.VarifyEmail();
                TaxPayerProfile TPAPIResponse = await UpdateEmailAdddress();
                if (TPAPIResponse != null)
                {
                    App.TP.Email = TPAPIResponse.Email;
                    CloseAllPopup();
                    if (TPAPIResponse.Login == "X")
                    {
                        var confirmPopup = new ZAKATOkCancelPopUpView(AppResources.TPUpdateEmailSuccessConfirmation);
                        confirmPopup.OnSelect = async (result) =>
                         {
                             if (result == "Yes")
                             {
                                 MainThread.BeginInvokeOnMainThread(async () =>
                                 {
                                     await Task.Run(() =>
                                     {
                                         App.DisplayProgressView();
                                     });
                                     if (App.TP != null)
                                         App.TP = null;
                                     if (App.PreviousIsArabic)
                                     {
                                         string langName = "ar-AE";
                                         AppResources.Culture = new CultureInfo(langName);
                                     }
                                     else
                                     {
                                         string langName = "en-US";
                                         AppResources.Culture = new CultureInfo(langName);
                                     }

                                     try { await WebServiceManager.GAZTLogOff(); }
                                     catch { }

                                     await Task.Run(() =>
                                     {
                                         App.HideProgressView();
                                     });

                                     App.IsLogOut = true;
                                     App.IsLoginCalled = false;
                                     App.IsSamlApiCalledAndroid = false;

                                     try
                                     {
                                         App.httpClientHandler = new HttpClientHandler();
                                         App.httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                                         App.httpClientHandler.CookieContainer = new System.Net.CookieContainer();
                                     }
                                     catch (Exception)
                                     {
                                     }
                                     await Application.Current.MainPage.Navigation.PopToRootAsync();
                                 });
                                 //MessagingCenter.Send<UpdateEmailPopUp>(this, "redirectToLogin");
                             }
                         };
                        await MopupService.Instance.PushAsync(confirmPopup);
                    }
                    else
                    {

                        System.Diagnostics.Debug.WriteLine("TP SUCCESS RESPONSE: ", TPAPIResponse);
                        viewModel._navigationService.NavigateTo(App.TaxpayerProfileSuccessPage, 1);
                    }
                }
            }
        }
        public async Task<TaxPayerProfile> UpdateEmailAdddress()
        {
            viewModel.IsLoading = true;
            TaxPayerProfile TP = null;

            try
            {
                TPProfileAPIRequestDataModel APIRequestDataModel = new TPProfileAPIRequestDataModel();
                APIRequestDataModel.RequestType = "VERIFYOTPEMAIL";
                APIRequestDataModel.OTP = "";
                APIRequestDataModel.OldEmail = viewModel.CurrentEmailText;
                APIRequestDataModel.NewEmail = viewModel.NewEmailText;
                APIRequestDataModel.OldPassword = "";
                APIRequestDataModel.NewPassword = "";

                TPProfileAPIRequest TPProfileAPIRequestData = TPProfileAPIRequest.PrepareRequestData(APIRequestDataModel);
                TP = await WebServiceManager.POSTTPProfileAPICalls(TPProfileAPIRequestData, "VERIFYOTPEMAIL");

                viewModel.IsLoading = false;
            }
            catch (Exception ex)
            {
                viewModel.IsLoading = false;
                MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));


            }

            return TP;
        }
        public static TPProfileAPIRequest PrepareRequestData(TPProfileAPIRequestDataModel APIRequestDataModel)
        {
            var metaData = new Metadata();
            metaData.id = ZATCAConstants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_TP_PROFILE_N_SRV/TPFL_HEADERSet(Euser1='00000000000000000000',Euser='',Euser2='00000000000000000000',Euser3='00000000000000000000',Euser4='00000000000000000000',Fbguid='',Euser5='00000000000000000000',Taxpayerz='" + App.TP.Tin + "',Langz='E')";
            metaData.uri = ZATCAConstants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_TP_PROFILE_N_SRV/TPFL_HEADERSet(Euser1='00000000000000000000',Euser='',Euser2='00000000000000000000',Euser3='00000000000000000000',Euser4='00000000000000000000',Fbguid='',Euser5='00000000000000000000',Taxpayerz='" + App.TP.Tin + "',Langz='E')";
            metaData.type = "Z_TP_PROFILE_N_SRV.TPFL_HEADER";

            string lang = "E";
            if (App.IsArabic == true) { lang = "A"; }

            var TPProfileAPIRequestData = new TPProfileAPIRequest();
            TPProfileAPIRequestData.__metadata = metaData;

            switch (APIRequestDataModel.RequestType)
            {
                case "GETOTPMOBILE":
                    TPProfileAPIRequestData.Mobile = APIRequestDataModel.NewMobile;
                    TPProfileAPIRequestData.MobileChk = "1";
                    TPProfileAPIRequestData.VerifyMobile = "X";
                    TPProfileAPIRequestData.MobileCountry = APIRequestDataModel.CountryCode;
                    break;

                case "VERIFYOTPMOBILE":
                    TPProfileAPIRequestData.Mobile = APIRequestDataModel.NewMobile;
                    TPProfileAPIRequestData.Conf = "X";
                    TPProfileAPIRequestData.MobileLoginCd = APIRequestDataModel.OTP;
                    TPProfileAPIRequestData.MobileChk = "1";
                    TPProfileAPIRequestData.MobileCountry = APIRequestDataModel.CountryCode;
                    break;

                case "GETOTPEMAIL":
                    TPProfileAPIRequestData.Email = APIRequestDataModel.NewEmail;
                    TPProfileAPIRequestData.PrevEmail = APIRequestDataModel.OldEmail;
                    TPProfileAPIRequestData.EmailChk = "1";
                    TPProfileAPIRequestData.VerifyEmail = "X";
                    break;

                case "VERIFYOTPEMAIL":
                    TPProfileAPIRequestData.Conf = "X";
                    TPProfileAPIRequestData.PrevEmail = APIRequestDataModel.OldEmail;
                    TPProfileAPIRequestData.EmailLoginCd = "";
                    TPProfileAPIRequestData.EmailChk = "1";
                    TPProfileAPIRequestData.PasswordChk = "";
                    TPProfileAPIRequestData.PasswordNew = "";
                    TPProfileAPIRequestData.PasswordOld = "";
                    TPProfileAPIRequestData.PreviousPwd = APIRequestDataModel.OldPassword;
                    TPProfileAPIRequestData.MobileChk = "";
                    TPProfileAPIRequestData.Email = APIRequestDataModel.NewEmail;
                    break;

                default:
                    break;
            }

            TPProfileAPIRequestData.Langz = lang;
            return TPProfileAPIRequestData;
        }
        private bool TaxpayerProfileOTPEmailUpdateValidation(string CurrentPassword, string NewPassword, string ConfirmPassword)
        {
            string validationError = VerifyOTPPasswords(CurrentPassword, NewPassword, ConfirmPassword);

            if (validationError == string.Empty)
                return true;
            else
            {
                viewModel.ShowValidationPopup(validationError);
                return false;
            }
        }
        private string VerifyOTPPasswords(string CurrentPassword, string NewPassword, string ConfirmPassword)
        {
            bool compareStringFlag = string.Equals(NewPassword, ConfirmPassword);

            if (string.IsNullOrEmpty(CurrentPassword) && string.IsNullOrEmpty(NewPassword))
                return AppResources.TPOldNewPasswordEmpty;
            else if (string.IsNullOrEmpty(CurrentPassword))
                return AppResources.TPOldPasswordEmpty;
            else if (string.IsNullOrEmpty(NewPassword))
                return AppResources.TPNewPasswordEmpty;
            else if (string.Equals(CurrentPassword, NewPassword))
                return AppResources.TPOldAndNewPasswordSame;
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
                viewModel.ShowValidationPopup(validationError);
                return false;
            }
        }

        private string VerifyEmails(string CurrentEmail, string NewEmail, string ConfirmEmail)
        {
            int iEmailCompared = string.Compare(NewEmail, ConfirmEmail, true);

            if (string.IsNullOrEmpty(NewEmail))
                return AppResources.ZZPleasefillallthemandatoryfields;
            else if (string.Compare(CurrentEmail, NewEmail, true) == 0)
                return AppResources.NDNewEmailCannotBeSameAsOldEmail;
            else if (iEmailCompared != 0)
                return AppResources.NewEmailandRetypeEmailNotMatch;
            else return string.Empty;
        }

        private async void CloseAllPopup()
        {
            await MopupService.Instance.PopAllAsync();
        }

        async void OnBackArrowTapped(object sender, EventArgs e)
        {
            await MopupService.Instance.PopAllAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Show existing email
            viewModel.CurrentEmailText = App.TP?.Email;
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

        private void BorderlessEntry_Unfocused(object sender, FocusEventArgs e) { }

        private void NewEmail_Entry_Unfocused(object sender, FocusEventArgs e) { }

        private void ConfirmEmail_Entry_Unfocused(object sender, FocusEventArgs e) { }
    }
}
