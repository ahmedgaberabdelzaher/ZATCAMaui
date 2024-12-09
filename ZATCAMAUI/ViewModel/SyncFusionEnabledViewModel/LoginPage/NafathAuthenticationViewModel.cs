
using System.Windows.Input;
using AppDynamics.Agent;
using Mopups.Services;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.LoginPage;

public class NafathAuthenticationViewModel : BaseViewModel
{
    public ICommand CancelCommand { get; set; }
    public override ICommand BackCommand { get; }

    public NafathLoginResponse Response { get; set; }

    public bool _isTimerRepeatRequired = true;
    public string navigation;
    public string guid_chm = string.Empty;

    private string _authenticationNumber;

    public string AuthenticationNumber
    {
        get { return _authenticationNumber; }
        set
        {
            _authenticationNumber = value;
            OnPropertyChanged(nameof(AuthenticationNumber));
        }
    }

    public NafathAuthenticationViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
    {
        CancelCommand = new Command(() => CancelRequest());
        BackCommand = new Command(() => GoBack());
    }

    public ICommand OnAppearingNafathAuthenticationViewCommand
    {

        get
        {
            return new Command(() =>
            {
                _isTimerRepeatRequired = true;
                InitPeriodicStatusChecker();
            });
        }
    }


    private void CancelRequest()
    {
        _isTimerRepeatRequired = false;
        removeBackStack();
    }
    void GoBack()
    {
        _isTimerRepeatRequired = false;
        removeBackStack();
    }

    internal void removeBackStack()
    {
        try
        {
            var _navigation = Application.Current.MainPage.Navigation;

            foreach (var item in _navigation.NavigationStack)
            {
                if (item.GetType().Name == App.NafathAuthenticationView)
                {
                    _navigation.RemovePage(item);
                    break;
                }
            }
        }
        catch (Exception)
        {
        }

    }

    private void InitPeriodicStatusChecker()
    {
        var seconds = TimeSpan.FromSeconds(ZATCAConstants.NafathAPICallTimer);
        NafathLoginResponseModel response = null;

        Device.StartTimer(seconds, () =>
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                IsLoading = true;
                if (response != null && response.result.statusCode.Equals("S"))
                {
                    _isTimerRepeatRequired = false;
                    await NavigateToNextSteps(response.result.returnId);
                }
                else if (response != null && response.result.statusCode.Equals("E"))
                {
                    _isTimerRepeatRequired = false;
                    await _dialogService.ShowMessage(response.result.statusDescription, AppResources.Information, AppResources.OKText, delegate ()
                    {
                        _navigationService.GoBack();
                    });


                }
                else
                {
                    if (_isTimerRepeatRequired)
                    {
                        response = await WebServiceManager.CheckNafathAuthentication(Response);
                    }
                }
                IsLoading = false;
            });
            return _isTimerRepeatRequired;
        });
    }

    private async Task NavigateToNextSteps(string guid)
    {
        if (navigation == ZATCAConstants.NAFATH_CHANGE_MOBILE_NUMBER)
        {
            Dictionary<string, string> d = new Dictionary<string, string>
                    {
                        { Response.idNumber, guid }
                    };

            await _navigationService.NavigateTo(App.NafathChangeMobileNumberView, d);
        }
        else if (navigation == ZATCAConstants.NAFATH_COMPANY_CHANGE_MOBILE_NUMBER)
        {
            Dictionary<string, string> d = new Dictionary<string, string> { { Response.idNumber, guid } };
            await _navigationService.NavigateTo(App.ChangeMobileRequestPageView, d);
        }
        else if (navigation == ZATCAConstants.NAFATH_LOGIN || navigation == ZATCAConstants.NAFATH_SIGNUP)
        {
            await GetAccount(guid);
        }
        removeBackStack();
    }

    private async Task GetAccount(string guid)
    {
        string idType = string.Empty;

        if (Response.idNumber.StartsWith("1"))
        {
            idType = "NationalId";
        }
        else if (Response.idNumber.StartsWith("2"))
        {
            idType = "IQAMA";
        }
        IsLoading = true;
        var response = await WebServiceManager.NafathSSOUserAccountsInquiry(Response.idNumber, guid, idType);
        IsLoading = false;

        if (response != null && response.data != null && response.data.SSOUserAccounts != null)
        {
            if (response.data!.SSOUserAccounts!.Count > 0)
            {
                if (response.data.SSOUserAccounts[0].code == "100")
                {
                    await Login(response.data.SSOUserAccounts[0].username, response.data.SSOUserAccounts[0].GUID);
                }
                else if (response.data.SSOUserAccounts[0].code == "101")
                {
                    App.GUIDFrSSO = response.data.SSOUserAccounts[0].GUID;
                    await _navigationService.NavigateTo(App.IndividualRegistrationPageView, "RegisterPageSSO");
                }
            }
        }

    }

    private async Task Login(string TIN, string guid)
    {
        try
        {
            IsLoading = true;
            NafathLoginModel model = new NafathLoginModel();
            model.TIN = TIN;
            model.GUID = guid;
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }
            if (status == PermissionStatus.Granted)
            {
                var location = await Geolocation.GetLocationAsync();
                model.latitude = location?.Latitude.ToString();
                model.longitude = location?.Longitude.ToString();
            }
            else
            {
                model.latitude = "UNKNOWN";
                model.longitude = "UNKNOWN";
            }
            model.ipaddress = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetLocalIPAddress();
            model.osname = DeviceInfo.Platform.ToString();
            model.sourceType = "ZM";
            model.browsername = DeviceInfo.Platform.ToString();
            model.language = WebServiceManager.GetLangZParameterAREN();
            var response = await WebServiceManager.NafathLogin(model);
            IsLoading = false;
            if (response != null && response.result != null && response.result.accessToken != null)
            {
                if (App.LoginDataRetrieved == null)
                {
                    App.LoginDataRetrieved = new LoginModel();
                }
                App.LoginDataRetrieved.TIN = TIN;
                App.Token = response.result.accessToken;
                await LoginCompleted();
                await _navigationService.NavigateTo(App.GAZTNewDesignDashBoardPageView, false);
            }
            else
            {
                IsLoading = false;
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(response.result?.error_description));

            }
        }
        catch (Exception)
        {
            IsLoading = false;
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
