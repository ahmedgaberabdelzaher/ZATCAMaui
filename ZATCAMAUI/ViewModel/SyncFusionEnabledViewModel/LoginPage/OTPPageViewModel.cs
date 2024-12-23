using System.Runtime.Intrinsics.X86;
using System.Windows.Input;
using AppDynamics.Agent;
using Mopups.Services;
using Newtonsoft.Json;
using ZATCAMAUI.Core.CustomControls;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.Views.NewDesign.DashBoardPages;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.LoginPage;

public class OTPPageViewModel : BaseViewModel
{

    private int counter = 120;

    private System.Timers.Timer timer;


    public static int LoginAttempt = 0;

    string oTPSentOnThisMobileNumber;
    public string OTPSentOnThisMobileNumber { get { return oTPSentOnThisMobileNumber; } set { oTPSentOnThisMobileNumber = value; OnPropertyChanged(); } }

    Color resendCodeTextColor;
    public Color ResendCodeTextColor { get { return resendCodeTextColor; } set { resendCodeTextColor = value; OnPropertyChanged(); } }

    double resendCodeOpacity = 0.3;
    public double ResendCodeOpacity { get { return resendCodeOpacity; } set { resendCodeOpacity = value; OnPropertyChanged(); } }

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



    public OTPPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
    {
        OTPSentOnThisMobileNumber = AppResources.ZZMobileNumber + " " + App.MobileNumber;
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

                    var tokenRequestModel = new TokenRequestModel() { Token = App.Token, Lang = App.IsArabic ? "ar" : "en", OTP = OTPFirstDigit + OTPSecondDigit + OTPThirdDigit + OTPFourthDigit, SourceType = "M", OsName = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem, BrowserName = "chrome" };
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
                if (IsResendCodeEnabled)
                {
                    ResendToken().ConfigureAwait(false);
                    Dissable_Resend();
                    OTPFirstDigit = OTPSecondDigit = OTPThirdDigit = OTPFourthDigit = string.Empty;
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
            IsLoading = false;
            string response = tokenResponse.Content.ReadAsStringAsync().Result;
            if (tokenResponse != null && tokenResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<TokenResponseModel>(response);
                if (result?.Result != null)
                {
                    App.Token = result?.Result?.AccessToken;
                    await LoginCompleted();
                    StopTimer();
                    //_navigationService.NavigateTo(App.GAZTNewDesignDashBoardPageView,false);

                    var navigation = Application.Current.MainPage.Navigation;
                    var currentPage = navigation.NavigationStack.LastOrDefault();
                    navigation.InsertPageBefore(new GAZTNewDesignDashBoardPageView(false), currentPage);
                    _navigationService.GoBack();
                }
            }
            else if (tokenResponse != null && tokenResponse.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.OTPScreenErrMsg;
                StopTimer();
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
                    StopTimer();
                    _navigationService.GoBack();
                    await _navigationService.NavigateTo(App.AccountLockedPageView);
                }

            }
        }
        catch (Exception gex)
        {
            IsLoading = false;
            IsShowMsgView = true;
            if (gex is GAZTNetworkConnectivityIssueException)
            {
                MessageTxt = AppResources.NetworkConnectivityIssue;
            }
            else if (gex is InternetException)
            {
                MessageTxt = AppResources.ZZInternetConnectionMessage;
            }
            else
            {
                MessageTxt = AppResources.RequestTimeoutDescription;
            }
        }
    }


    public void StartTimer()
    {
        timer = new System.Timers.Timer();
        LblCountDownTimer = GetTime(counter);
        timer.Interval = 1000;
        timer.Elapsed += Timer_Elapsed;
        timer.Start();

    }

    private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        if (counter > 0)
        {
            counter--;
            LblCountDownTimer = GetTime(counter);
        }
        else
        {
            timer.Stop();
            Enable_Resend();
            counter = 120;//To rest
        }
    }

    private void Enable_Resend()
    {
        IsResendCodeEnabled = true;
        ResendCodeTextColor = Color.FromArgb("#0996D4");
        ResendCodeOpacity = 1;
    }

    private void Dissable_Resend()
    {
        ResendCodeTextColor = Colors.Gray;
        ResendCodeOpacity = 0.3;
        IsResendCodeEnabled = false;
    }

    public void StopTimer()
    {
        if (timer != null)
        {
            Enable_Resend();
            timer.Stop();
            counter = 120;//To rest
            OTPFirstDigit = OTPSecondDigit = OTPThirdDigit = OTPFourthDigit = string.Empty;
        }
    }

    private string GetTime(int s)
    {
        TimeSpan time = TimeSpan.FromSeconds(s);
        return time.ToString(@"m\:ss");
    }

    public async Task ResendToken()
    {
        try
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
                    await _navigationService.NavigateTo(App.UnlockAccountTINPageView);
                }
            }
            IsLoading = false;
        }
        catch (Exception gex)
        {
            string MessageForTheUser = gex.Message;

            if (gex is GAZTNetworkConnectivityIssueException)
            {
                MessageForTheUser = AppResources.NetworkConnectivityIssue;
            }
            else if (gex is InternetException)
            {
                MessageForTheUser = AppResources.ZZInternetConnectionMessage;
            }
            IsLoading = false;

            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
        }

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
                    App.TP.authenticationUser1 = TPProfile.authenticationUser1;
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

