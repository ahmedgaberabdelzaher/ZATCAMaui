using System.Windows.Input;
using AppDynamics.Agent;
using Mopups.Services;
using Newtonsoft.Json;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.LoginPage;

public class OTPPageViewModel : BaseViewModel
{

    private bool loginError = false;
    public static int LoginAttempt = 0;
    /// <summary>
    /// Gets or sets the command that is executed when the Hamburger menu button is clicked.
    /// </summary>
    public ICommand HamburgerMenuClickedCommand { get; set; }

    public bool LoginError
    {
        get
        {
            return this.loginError;
        }
        set
        {
            if (this.loginError == value)
            {
                return;
            }
            this.loginError = value;
            this.OnPropertyChanged("LoginError");
        }
    }

    public OTPPageViewModel(INavigationService navigationService, IDialogService dialogService):base(navigationService, dialogService)
    {
        this.HamburgerMenuClickedCommand = new Command(this.HamburgerMenuClicked);
    }

    private void HamburgerMenuClicked()
    {
        var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("OTPPageView", "HamburgerMenuClicked", "Anonymous Menu Opened");
        _navigationService.NavigateTo(App.DashboardAnonymousMenuPageView);
        AppDynamics.Agent.Instrumentation.EndCall(callTracker);
    }

    public async Task TokenPostRequest(TokenRequestModel model)
    {
        try
        {
            LoginError = false;
            var tokenResponse = await WebServiceManager.ValidateOTP(model);
            String response = tokenResponse.Content.ReadAsStringAsync().Result;
            if (tokenResponse != null && tokenResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {

                var result = JsonConvert.DeserializeObject<TokenResponseModel>(response);
                if (result?.Result != null)
                {
                    App.Token = result?.Result?.AccessToken;
                    await LoginCompleted();

                    //TODO Pop OTP Page
                    // _navigationService.GoBack();
                    _navigationService.NavigateTo(App.GAZTNewDesignDashBoardPageView);
                }
            }
            else if (tokenResponse != null && tokenResponse.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                LoginError = true;
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                _navigationService.GoBack();

            }
            else
            {
                var result = JsonConvert.DeserializeObject<TokenErrorModel>(response);
                App.Token = result.Result.ErrorToken; //to Resend the request
                LoginError = true;
                if (result.Result.ErrorCode.Equals("M012"))
                {
                    _navigationService.GoBack();
                    _navigationService.NavigateTo(App.AccountLockedPageView);
                    LoginError = false;
                }

            }
        }
        catch (Exception) { }
    }

    public async Task ResendToken()
    {
        string UserId = App.LoginDataRetrieved.TIN;
        string language = UtilityManager.GetLanguageParameter();
        var model = new ResentTokenRequestModel()
        {
            Token = App.Token,
            Lang = language
        };
        HttpResponseMessage response = await WebServiceManager.ResendToken(model);
        if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string responseResult = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<LoginResponseModel>(responseResult);
            App.Token = result.Result.Token;

        }
        else
        {
            LoginAttempt++;
            LoginError = true;
            if (LoginAttempt == 3)
            {
                _navigationService.NavigateTo(App.UnlockAccountTINPageView);
            }
        }
    }

    public async Task LoginCompleted()
    {
        string response = string.Empty;
        string UserId = App.LoginDataRetrieved.TIN;

        Instrumentation.SetUserData("user_id", UserId);

        //string lang = "E";
        string language = UtilityManager.GetLanguageParameter();


        // * NEW TP PROFILE API
        TaxPayerProfile TPProfile = await WebServiceManager.GetTPProfileAndUpdatePasswordAPICall(UserId);

        TaxPayerAccountDetail AccountDetail = await WebServiceManager.GetTPAccountDetails(UserId);

        if (TPProfile != null)
        {
            App.TP = new TaxPayerProfile();
            App.TP = TPProfile;
            App.TP.userId = TPProfile.TIN;
            try
            {
                if (App.TP != null)
                {
                    App.TP.firstName = TPProfile.firstName;
                    App.TP.lastName = TPProfile.lastName;
                    App.TP.organizationName = TPProfile.organizationName;
                    App.TP.typeCheck = TPProfile.typeCheck;
                    App.TP.email = TPProfile.email;
                    App.TP.emailCheck = TPProfile.emailCheck;
                    App.TP.formBundleGUID = TPProfile.formBundleGUID;
                    App.TP.formBundleNumber = TPProfile.formBundleNumber;
                    App.TP.activityName = TPProfile.activityName;
                    App.TP.VtpmFg = TPProfile.VtpmFg;

                }

            }
            catch (Exception)
            {
            }

        }

        if (AccountDetail != null)
        {
            if (App.LoginDataRetrieved == null)
            {
                App.LoginDataRetrieved = new LoginModel();
            }
            App.LoginDataRetrieved.AppMsg = AccountDetail.applicationMessage;
            App.LoginDataRetrieved.TpMpVip = AccountDetail.taxpayerVip;
            App.LoginDataRetrieved.AppVersion = AccountDetail.applicationVerion;
            App.LoginDataRetrieved.NameLast = AccountDetail.lastName;
            App.LoginDataRetrieved.Emailid = AccountDetail.emailId;
            App.LoginDataRetrieved.FbGuid = AccountDetail.GUID;
            App.LoginDataRetrieved.NameFirst = AccountDetail.firstName;
            App.LoginDataRetrieved.DeviceToken = AccountDetail.deviceToken;
            App.LoginDataRetrieved.DeviceFlag = AccountDetail.device;
            App.LoginDataRetrieved.DeviceId = AccountDetail.deviceId;
            App.LoginDataRetrieved.DeviceTyp = AccountDetail.deviceType;
            App.LoginDataRetrieved.DeviceToken = AccountDetail.deviceToken;
            App.LoginDataRetrieved.EpSignup = AccountDetail.eligiblePersonSignup;
            App.LoginDataRetrieved.EtReg = AccountDetail.exciseTaxRegisteration;
            App.LoginDataRetrieved.VtReg = AccountDetail.VATRegisteration;
            App.LoginDataRetrieved.ZkReg = AccountDetail.zakatRegisteration;
            App.LoginDataRetrieved.Euser = AccountDetail.authenticationUser;
            App.LoginDataRetrieved.FcmId = AccountDetail.fcmId;
            App.LoginDataRetrieved.NameOrg1 = AccountDetail.organizationName;
            App.LoginDataRetrieved.TIN = AccountDetail.TIN;
            App.LoginDataRetrieved.EtSignup = AccountDetail.exciseSignup;
            App.LoginDataRetrieved.VtSignup = AccountDetail.VATSignup;
            App.LoginDataRetrieved.ZkSignup = AccountDetail.zakatSignup;
            App.LoginDataRetrieved.message = AccountDetail.messageTitle;
            App.LoginDataRetrieved.TypeChk = AccountDetail.typeCheck;
            App.LoginDataRetrieved.CozatcaTile = AccountDetail.cozatcaTileFlag;
        }



    }
}

