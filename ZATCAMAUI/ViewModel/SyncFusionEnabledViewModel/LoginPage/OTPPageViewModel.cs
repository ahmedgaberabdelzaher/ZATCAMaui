using System.Runtime.Intrinsics.X86;
using System.Windows.Input;
using AppDynamics.Agent;
using Mopups.Services;
using Newtonsoft.Json;
using ZATCAMAUI.Core.CustomControls;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.LoginPage;

public class OTPPageViewModel : BaseViewModel
{

    public static int LoginAttempt = 0;
    private TimeSpan remainingTime = TimeSpan.FromMinutes(2);
    private bool isTimeRemaining = false;

    string oTPSentOnThisMobileNumber;
    public string OTPSentOnThisMobileNumber { get { return oTPSentOnThisMobileNumber; } set { oTPSentOnThisMobileNumber = value; OnPropertyChanged(); } }

    Color resendCodeTextColor;
    public Color ResendCodeTextColor { get { return resendCodeTextColor; } set { resendCodeTextColor = value; OnPropertyChanged(); } }

    bool isResendCodeEnabled;
    public bool IsResendCodeEnabled { get { return isResendCodeEnabled; } set { isResendCodeEnabled = value; OnPropertyChanged(); } }

    string lblCountDownTimer;
    public string LblCountDownTimer { get { return lblCountDownTimer; } set { lblCountDownTimer = value; OnPropertyChanged(); } }

    string oTPFirstDigit;
    public string OTPFirstDigit { get { return oTPFirstDigit; } set { oTPFirstDigit = value; OnPropertyChanged(); } }

    string oTPSecondDigit;
    public string OTPSecondDigit { get { return oTPSecondDigit; } set { oTPSecondDigit = value; OnPropertyChanged(); } }

    string oTPThirdDigit;
    public string OTPThirdDigit { get { return oTPThirdDigit; } set { oTPThirdDigit = value; OnPropertyChanged(); } }

    string oTPFourthDigit;
    public string OTPFourthDigit { get { return oTPFourthDigit; } set { oTPFourthDigit = value; OnPropertyChanged(); } }


    
    public OTPPageViewModel(INavigationService navigationService, IDialogService dialogService):base(navigationService, dialogService)
    {
        OTPSentOnThisMobileNumber = AppResources.ZZMobileNumber + " xxxxxxx" + App.MobileNumber?.Substring(7, 3);
    }

    public ICommand GoToNextEntryCommand
    {

        get
        {
            return new Command<object>((e) =>
            {
                if (e != null)
                {

                    var entry = e as GAZTBorderlessEntry;

                    switch (entry.ClassId)
                    {
                        case "2":
                            if (!string.IsNullOrEmpty(OTPFirstDigit))
                            {
                                entry.Focus();
                            }
                            break;
                        case "3":
                            if (!string.IsNullOrEmpty(OTPSecondDigit))
                            {
                                entry.Focus();
                            }
                            break;
                        case "4":
                            if (!string.IsNullOrEmpty(OTPThirdDigit))
                            {
                                entry.Focus();
                            }
                            break;
                        default:
                            break;
                    }


                }
            });
        }
    }

    public ICommand FocusEntryCommand
    {

        get
        {
            return new Command<object>((e) =>
            {
                if (e != null)
                {

                    var entry = e as GAZTBorderlessEntry;

                    entry.Focus();
                }
            });
        }
    }

    public ICommand VerifyOTPCommand
    {
        get
        {
            return new Command(async () =>
            {
                try
                {
                    StopTimer();
                    var tokenRequestModel = new TokenRequestModel() { Token = App.Token, Lang =App.IsArabic ? "ar":"en", OTP = OTPFirstDigit + OTPSecondDigit + OTPThirdDigit + OTPFourthDigit, SourceType = "M", OsName = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem, BrowserName = "chrome" };
                    await TokenPostRequest(tokenRequestModel);
                }
                catch (Exception)
                {
                   
                   
                }


            });
        }
    }

    public ICommand StartTimerCommand
    {
        get
        {
            return new Command(() =>
            {
                StartTimer();
            });
        }
    }

    public ICommand ResendOtpCommand
    {
        get
        {
            return new Command(() =>
            {
                if (!isTimeRemaining)
                {
                    ResendToken().ConfigureAwait(false);
                    ResendCodeTextColor = Colors.Gray;
                    IsResendCodeEnabled = false;
                    OTPFirstDigit = OTPSecondDigit = OTPThirdDigit = OTPFourthDigit = string.Empty;
                    remainingTime = TimeSpan.FromMinutes(2);
                    StartTimer();
                }
            });
        }
    }

    public async Task TokenPostRequest(TokenRequestModel model)
    {
        try
        {

            IsLoading = true;
            var tokenResponse = await WebServiceManager.ValidateOTP(model);
            string response = tokenResponse.Content.ReadAsStringAsync().Result;
            if (tokenResponse != null && tokenResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {

                var result = JsonConvert.DeserializeObject<TokenResponseModel>(response);
                if (result?.Result != null)
                {
                    App.Token = result?.Result?.AccessToken;
                    await LoginCompleted();
                    _navigationService.NavigateTo(App.GAZTNewDesignDashBoardPageView,false);
                }
            }
            else if (tokenResponse != null && tokenResponse.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.OTPScreenErrMsg;

                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                _navigationService.GoBack();

            }
            else
            {
                var result = JsonConvert.DeserializeObject<TokenErrorModel>(response);
                App.Token = result.Result.ErrorToken; //to Resend the request
                IsShowMsgView = true;
                MessageTxt = AppResources.OTPScreenErrMsg;
                if (result.Result.ErrorCode.Equals("M012"))
                {
                    _navigationService.GoBack();
                    _navigationService.NavigateTo(App.AccountLockedPageView);
                    
                }

            }
            IsLoading = false;
        }
        catch (Exception)
        {
            IsLoading = false;
            IsShowMsgView = true;
            MessageTxt = AppResources.RequestTimeoutDescription;
        }
    }


    private void StartTimer()
    {
        isTimeRemaining = true;
        Device.StartTimer(TimeSpan.FromSeconds(1), () =>
        {
            remainingTime = remainingTime.Subtract(TimeSpan.FromSeconds(1));
            if (remainingTime.TotalSeconds <= -1)
            {
                StopTimer();
                return false;
            }
            UpdateTimerLabel();
            return true;
        });
    }

    private void StopTimer()
    {
        IsResendCodeEnabled = true;
        ResendCodeTextColor = Color.FromArgb("#0996D4"); 
        isTimeRemaining = false;
    }

    private void UpdateTimerLabel()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            LblCountDownTimer = remainingTime.ToString(@"mm\:ss");
        });
    }

    public async Task ResendToken()
    {
        IsLoading = true;
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
            IsShowMsgView = true;
            MessageTxt = AppResources.OTPScreenErrMsg;
            if (LoginAttempt == 3)
            {
                _navigationService.NavigateTo(App.UnlockAccountTINPageView);
            }
        }
        IsLoading = false;
    }

    public async Task LoginCompleted()
    {
        IsLoading = true;
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
                IsLoading = false;
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


        IsLoading = false;
    }
}

