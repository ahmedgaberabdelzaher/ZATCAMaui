using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using RGPopup.Maui.Services;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Views.SyncFusionEnabledViews.AddPopPages;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.UnlockAccount
{
    public class UnlockAccountTINPageViewModel : ViewModelBase
    {
        private string captcha = string.Empty;
        private string GUID = string.Empty;
        public bool IsAPICalledSuccessfully = false;

        public ICommand OnContinueButtonClick { get; set; }
        public ICommand OnBackButtonClick { get; set; }
        public ICommand OnResendButtonClick { get; set; }
        public int currentStep { get; set; }
        public ICommand VerifyTINBtnClicked { get; set; }
        public Command ConfirmOtpBtnClicked { get; set; }
        public ICommand ConfirmPasswordBtnClicked { get; set; }
        public Command OnSubmitClicked { get; set; }
        public Command OnResendOTPClicked { get; set; }

        CancellationTokenSource _CancellationTokenSource;
        int TotalSec;
        public bool StopTimer = false;
        public int totalAttempts = 0;


        public int numberOfSeconds = 120;

        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        private int _currentAttempts = 0;
        public int currentAttempts
        {
            get
            {
                return _currentAttempts;
            }
            set
            {
                _currentAttempts = value;
                if (_currentAttempts == 5)
                {
                    ButtonDisableColor = (Color)Application.Current.Resources["Primary"];
                    IsResendOTPEnabled = true;
                    VerifyButtonDisableColor = (Color)Application.Current.Resources["ButtonGray"];
                    IsVerifyOTPEnabled = false;
                    IsOTPEntryEnable = false;
                    _currentAttempts = 0;
                    OTPValidDuration = " 0:00";
                    StopTimer = true;
                    numberOfSeconds = 0;
                    TotalSec = 0;
                }
                RaisePropertyChanged("currentAttempts");
            }
        }

        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }

        private bool _isTinContentViewVisible = false;
        public bool IsTinContentViewVisible
        {
            get
            {
                return _isTinContentViewVisible;
            }
            set
            {
                _isTinContentViewVisible = value;
                RaisePropertyChanged("IsTinContentViewVisible");
            }
        }

        private bool _isOTPContentViewVisible = false;
        public bool IsOTPContentViewVisible
        {
            get
            {
                return _isOTPContentViewVisible;
            }
            set
            {
                _isOTPContentViewVisible = value;
                RaisePropertyChanged("IsOTPContentViewVisible");
            }
        }

        private bool _isChangePasswordViewVisible = false;
        public bool IsChangePasswordViewVisible
        {
            get
            {
                return _isChangePasswordViewVisible;
            }
            set
            {
                _isChangePasswordViewVisible = value;
                RaisePropertyChanged("IsChangePasswordViewVisible");
            }
        }

        private bool _isAccountUnlockedSuccessViewVisible = false;
        public bool IsAccountUnlockedSuccessViewVisible
        {
            get
            {
                return _isAccountUnlockedSuccessViewVisible;
            }
            set
            {
                _isAccountUnlockedSuccessViewVisible = value;
                RaisePropertyChanged("IsAccountUnlockedSuccessViewVisible");
            }
        }

        private string _verifyButtonText = string.Empty;
        public string VerifyButtonText
        {
            get
            {
                return _verifyButtonText;
            }
            set
            {
                _verifyButtonText = value;
                RaisePropertyChanged("VerifyButtonText");
            }
        }

        private Color _verifybuttonDisableColor = (Color)Application.Current.Resources["Secondary"];
        public Color VerifyButtonDisableColor
        {
            get
            {
                return _verifybuttonDisableColor;
            }
            set
            {
                _verifybuttonDisableColor = value;
                RaisePropertyChanged("VerifyButtonDisableColor");
            }
        }

        private string _txtTIN;
        public string TxtTIN
        {
            get
            {
                return _txtTIN;
            }
            set
            {
                _txtTIN = value;
                RaisePropertyChanged("TxtTIN");
            }
        }

        private string _otpFirstDigit;
        public string OtpFirstDigit
        {
            get
            {
                return _otpFirstDigit;
            }
            set
            {
                _otpFirstDigit = value;
                RaisePropertyChanged("OtpFirstDigit");
            }
        }

        private string _otpSecondDigit;
        public string OtpSecondDigit
        {
            get
            {
                return _otpSecondDigit;
            }
            set
            {
                _otpSecondDigit = value;
                RaisePropertyChanged("OtpSecondDigit");
            }
        }

        private string _otpThirdDigit;
        public string OtpThirdDigit
        {
            get
            {
                return _otpThirdDigit;
            }
            set
            {
                _otpThirdDigit = value;
                RaisePropertyChanged("OtpThirdDigit");
            }
        }

        private string _otpFourthDigit;
        public string OtpFourthDigit
        {
            get
            {
                return _otpFourthDigit;
            }
            set
            {
                _otpFourthDigit = value;

                RaisePropertyChanged("OtpFourthDigit");

                if (_otpFourthDigit.Length > 0)
                {
                    if (IsOtpAPICalled == false)
                    {
                        IsOtpAPICalled = true;

                        if (_currentAttempts < 5)
                        {
                            ConfirmOtpBtnCommand(string.Empty);
                        }
                    }
                }
            }
        }

        // * Password
        private string _MinEight;
        public string MinEight
        {
            get { return _MinEight; }
            set
            {
                _MinEight = value;
                RaisePropertyChanged("MinEight");
            }
        }

        private string _CapsSmall;
        public string CapsSmall
        {
            get { return _CapsSmall; }
            set
            {
                _CapsSmall = value;
                RaisePropertyChanged("CapsSmall");
            }
        }

        private string _MaxSixteen;
        public string MaxSixteen
        {
            get { return _MaxSixteen; }
            set
            {
                _MaxSixteen = value;
                RaisePropertyChanged("MaxSixteen");
            }
        }

        private string _NumSymbol;
        public string NumSymbol
        {
            get { return _NumSymbol; }
            set
            {
                _NumSymbol = value;
                RaisePropertyChanged("NumSymbol");
            }
        }
        // * End

        private bool _isOtpAPICalled = false;
        public bool IsOtpAPICalled
        {
            get
            {
                return _isOtpAPICalled;
            }
            set
            {
                _isOtpAPICalled = value;
                RaisePropertyChanged("IsOtpAPICalled");
            }
        }

        private string _prevOtp;
        public string PrevOtp
        {
            get
            {
                return _prevOtp;
            }
            set
            {
                _prevOtp = value;
                RaisePropertyChanged("PrevOtp");
            }
        }

        private string _newOtp;
        public string NewOtp
        {
            get
            {
                return _newOtp;
            }
            set
            {
                _newOtp = value;
                RaisePropertyChanged("PrevOtp");
            }
        }

        private bool _isPasswordEncripted = true;
        public bool IsPasswordEncripted
        {
            get
            {
                return _isPasswordEncripted;
            }
            set
            {
                _isPasswordEncripted = value;
                RaisePropertyChanged("IsPasswordEncripted");
            }
        }
        private bool _isConfirmPasswordEncripted = true;
        public bool IsConfirmPasswordEncripted
        {
            get
            {
                return _isConfirmPasswordEncripted;
            }
            set
            {
                _isConfirmPasswordEncripted = value;
                RaisePropertyChanged("IsConfirmPasswordEncripted");
            }
        }

        private bool _framePasswordError = false;
        public bool FramePasswordError
        {
            get
            {
                return _framePasswordError;
            }
            set
            {
                _framePasswordError = value;
                RaisePropertyChanged("FramePasswordError");
            }
        }
        private bool _frameConfirmPasswordError = false;
        public bool FrameConfirmPasswordError
        {
            get
            {
                return _frameConfirmPasswordError;
            }
            set
            {
                _frameConfirmPasswordError = value;
                RaisePropertyChanged("FrameConfirmPasswordError");
            }
        }

        private string _password = string.Empty;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                RaisePropertyChanged("Password");
            }
        }

        private string _confirmPassword = string.Empty;
        public string ConfirmPassword
        {
            get
            {
                return _confirmPassword;
            }
            set
            {
                _confirmPassword = value;
                RaisePropertyChanged("ConfirmPassword");
            }
        }

        private string _mobileNumberMasked;
        public string MobileNumberMasked
        {
            get
            {
                return _mobileNumberMasked;
            }
            set
            {
                _mobileNumberMasked = value;
                RaisePropertyChanged("MobileNumberMasked");
            }
        }

        private string _passwordChangedSuccessfully;
        public string PasswordChangedSuccessfully
        {
            get
            {
                return _passwordChangedSuccessfully;
            }
            set
            {
                _passwordChangedSuccessfully = value;
                RaisePropertyChanged("PasswordChangedSuccessfully");
            }
        }

        private UnlockAccountModel _unlockAccountModel;
        public UnlockAccountModel UnlockAccountModel
        {
            get
            {
                return _unlockAccountModel;
            }
            set
            {
                _unlockAccountModel = value;
                RaisePropertyChanged("UnlockAccountModel");
            }
        }

        private UnlockAccountModelOtp _unlockAccountModelOtp;
        public UnlockAccountModelOtp UnlockAccountModelOtp
        {
            get
            {
                return _unlockAccountModelOtp;
            }
            set
            {
                _unlockAccountModelOtp = value;
                RaisePropertyChanged("UnlockAccountModelOtp");
            }
        }

        private UnlockAccountModelChangePassword _unlockAccountModelChangePassword;
        public UnlockAccountModelChangePassword UnlockAccountModelChangePassword
        {
            get
            {
                return _unlockAccountModelChangePassword;
            }
            set
            {
                _unlockAccountModelChangePassword = value;
                RaisePropertyChanged("UnlockAccountModelChangePassword");
            }
        }

        private UnlockAccountResponseModel _unlockAccountModelResponse;
        public UnlockAccountResponseModel UnlockAccountModelResponse
        {
            get
            {
                return _unlockAccountModelResponse;
            }
            set
            {
                _unlockAccountModelResponse = value;
                RaisePropertyChanged("UnlockAccountModelResponse");
            }
        }

        private Color _continueButtonnBackroundColor = (Color)Application.Current.Resources["Secondary"];
        public Color ContinueButtonnBackroundColor
        {
            get
            {
                return _continueButtonnBackroundColor;
            }
            set
            {
                _continueButtonnBackroundColor = value;
                RaisePropertyChanged("ContinueButtonnBackroundColor");
            }
        }

        private bool _isContinueButtonEnable = false;
        public bool IsContinueButtonEnable
        {
            get
            {
                return _isContinueButtonEnable;
            }
            set
            {
                _isContinueButtonEnable = value;
                if (_isContinueButtonEnable)
                {
                    ContinueButtonnBackroundColor = (Color)Application.Current.Resources["Secondary"];
                }
                else
                {
                    ContinueButtonnBackroundColor = (Color)Application.Current.Resources["ButtonGray"];
                }
                RaisePropertyChanged("IsContinueButtonEnable");
            }
        }

        private bool _isResendOTPEnabled = false;
        public bool IsResendOTPEnabled
        {
            get
            {
                return _isResendOTPEnabled;
            }
            set
            {
                _isResendOTPEnabled = value;
                OnResendOTPClicked.ChangeCanExecute();
                RaisePropertyChanged("IsResendOTPEnabled");
            }
        }

        bool CanExecuteResendOTPClickCommand(object arg)
        {
            return _isResendOTPEnabled;
        }

        bool CanExecuteSubmitClickCommand(object arg)
        {
            return _isVerifyOTPEnabled;
        }

        private bool _isVerifyOTPEnabled = true;
        public bool IsVerifyOTPEnabled
        {
            get
            {
                return _isVerifyOTPEnabled;
            }
            set
            {
                _isVerifyOTPEnabled = value;
                ConfirmOtpBtnClicked.ChangeCanExecute();
                RaisePropertyChanged("IsVerifyOTPEnabled");
            }
        }

        private bool _isOTPEntryEnable = true;
        public bool IsOTPEntryEnable
        {
            get
            {
                return _isOTPEntryEnable;
            }
            set
            {
                _isOTPEntryEnable = value;
                RaisePropertyChanged(() => IsOTPEntryEnable);
            }
        }
        private Color _buttonDisableColor = (Color)Application.Current.Resources["NeutralGreay"];
        public Color ButtonDisableColor
        {
            get
            {
                return _buttonDisableColor;
            }
            set
            {
                _buttonDisableColor = value;
                RaisePropertyChanged("ButtonDisableColor");
            }
        }

        private string _oTPValidDuration;
        public string OTPValidDuration
        {
            get
            {
                return _oTPValidDuration;
            }
            set
            {
                _oTPValidDuration = value;
                if (_oTPValidDuration.Equals(" 00:00"))
                {
                    ButtonDisableColor = (Color)Application.Current.Resources["Primary"];
                    IsResendOTPEnabled = true;
                    VerifyButtonDisableColor = (Color)Application.Current.Resources["ButtonGray"];
                    IsVerifyOTPEnabled = false;
                    IsOTPEntryEnable = false;

                }
                RaisePropertyChanged("OTPValidDuration");
            }
        }

        #region ConstructorF
        /// <summary>
        /// Initializes a new instance for the <see cref="UnlockAccountTINPageViewModel" /> class.
        /// </summary>
        public UnlockAccountTINPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;
            _dialogService = dialogService;
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }

            OtpFirstDigit = string.Empty;
            OtpSecondDigit = string.Empty;
            OtpThirdDigit = string.Empty;
            OtpFourthDigit = string.Empty;

            _CancellationTokenSource = new CancellationTokenSource();

            IsContinueButtonEnable = false;
            IsLoading = false;
            EnableTINView();
            VerifyTINBtnClicked = new Command(VerifyTinBtnCommand);
            ConfirmPasswordBtnClicked = new Command(ConfirmPasswordBtnCommand);
            OnResendOTPClicked = new Command(ExecuteResendOTPClickCommand, CanExecuteResendOTPClickCommand);
            ConfirmOtpBtnClicked = new Command(ConfirmOtpBtnCommand, CanExecuteSubmitClickCommand);

            UnlockAccountModel = new UnlockAccountModel();
            UnlockAccountModelOtp = new UnlockAccountModelOtp();
            UnlockAccountModelChangePassword = new UnlockAccountModelChangePassword();
        }

        #endregion

        public void EnableTINView()
        {
            TxtTIN = string.Empty;
            IsOtpAPICalled = false;
            OtpFirstDigit = string.Empty;
            OtpSecondDigit = string.Empty;
            OtpThirdDigit = string.Empty;
            OtpFourthDigit = string.Empty;

            IsTinContentViewVisible = true;
            IsOTPContentViewVisible = false;
            IsChangePasswordViewVisible = false;
            IsAccountUnlockedSuccessViewVisible = false;
        }

        public void EnableOtpView()
        {
            currentAttempts = 0;
            MobileNumberMasked = UnlockAccountModelResponse.D.MobileNo;
            StopTimer = true;

            IsVerifyOTPEnabled = true;
            IsResendOTPEnabled = false;
            IsOTPEntryEnable = true;

            VerifyButtonDisableColor = (Color)Application.Current.Resources["Secondary"];
            ButtonDisableColor = (Color)Application.Current.Resources["NeutralGreay"];

            OtpFirstDigit = string.Empty;
            OtpSecondDigit = string.Empty;
            OtpThirdDigit = string.Empty;
            OtpFourthDigit = string.Empty;

            TimerStart(numberOfSeconds);

            IsTinContentViewVisible = false;
            IsOTPContentViewVisible = true;
            IsChangePasswordViewVisible = false;
            IsAccountUnlockedSuccessViewVisible = false;
        }

        public void EnableChangePasswordView()
        {
            Password = string.Empty;
            ConfirmPassword = string.Empty;

            IsTinContentViewVisible = false;
            IsOTPContentViewVisible = false;
            IsChangePasswordViewVisible = true;
            IsAccountUnlockedSuccessViewVisible = false;
        }

        public void EnableAccountUnlockedView()
        {
            IsTinContentViewVisible = false;
            IsOTPContentViewVisible = false;
            IsChangePasswordViewVisible = false;
            IsAccountUnlockedSuccessViewVisible = true;
        }

        public async Task ValidateTINNumberSendOtp(string tin)
        {

        }

        public void TimerStart(int Seconds)
        {
            try
            {
                IsVerifyOTPEnabled = true;

                CancellationTokenSource _CancellationTokenSource = new CancellationTokenSource();
                TotalSec = Seconds;
                CancellationTokenSource CTS = _CancellationTokenSource;

                Device.StartTimer(new TimeSpan(0, 0, 1), () =>
                {
                    if (App.IsComingFromSleepMode)
                    {
                        if (Device.RuntimePlatform == Device.iOS)
                        {
                            TotalSec = TotalSec - Convert.ToInt32(App.TimeDifference);
                            App.IsComingFromSleepMode = false;
                            // StopTimer = true;
                        }
                        else
                        {
                            TotalSec = TotalSec - Convert.ToInt32(App.TimeDifference);
                            App.IsComingFromSleepMode = false;
                            StopTimer = true;
                            // TimerStart(TotalSec);
                        }
                    }
                    if (CTS.IsCancellationRequested)
                    {
                        return false;
                    }
                    else
                    {
                        if (TotalSec == 0)
                        {
                            IsVerifyOTPEnabled = false;
                            return false;
                        }
                        else if (!StopTimer)
                        {

                            IsVerifyOTPEnabled = false;
                            return false;
                        }
                        else
                        {

                        }
                        if (TotalSec < 0)
                        {
                            OTPValidDuration = " 0:00";
                            ButtonDisableColor = (Color)Application.Current.Resources["Primary"];
                            IsResendOTPEnabled = true;
                            VerifyButtonDisableColor = (Color)Application.Current.Resources["ButtonGray"];
                            IsVerifyOTPEnabled = false;
                            IsOTPEntryEnable = false;
                            return false;
                        }
                        TotalSec = TotalSec - 1;
                        numberOfSeconds = TotalSec;
                        TimeSpan _TimeSpan = TimeSpan.FromSeconds(TotalSec);
                        OTPValidDuration = " " + string.Format("{0:00}:{1:00}", _TimeSpan.Minutes, _TimeSpan.Seconds);

                        return true;
                    }
                });
            }
            catch (Exception)
            {

            }
        }

        private void TimerStop()
        {
            Interlocked.Exchange(ref _CancellationTokenSource, new CancellationTokenSource()).Cancel();
        }

        public async void VerifyTinBtnCommand()
        {
            if (!IsAPICalledSuccessfully)
            {
                return;
            }
            try
            {
                await Task.Run(() =>
                {
                    App.DisplayProgressView();
                });

                UnlockAccountModel.Tin = TxtTIN;
                //Action for validating TIN and sending OTP
                UnlockAccountModel.Action = "01";
                UnlockAccountModel.TaxpayerGuid = GUID;
                UnlockAccountModel.Zcaptcha = captcha;
                UnlockAccountModelResponse = await VatRegistrationWebServiceManager.GaztUnlockAccount(UnlockAccountModel);
                totalAttempts = Convert.ToInt16(UnlockAccountModelResponse.D.Attempts);
                numberOfSeconds = 120;

                await Task.Run(() =>
                {
                    App.HideProgressView();
                });

                EnableOtpView();

            }
            catch (GAZTUnlockAccountException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Run(() =>
                    {
                        App.HideProgressView();
                    });
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }
            catch (InternetException ex)
            {
               MainThread.BeginInvokeOnMainThread(async () =>
                {
                    App.HideProgressView();
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public async Task GetCaptchAndGUID()
        {
            try
            {

                IsLoading = true;

                string lang = UtilityManager.GetLanguageParameter();
                string st = ZATCAConstants.CaptchaAndGUID;
                string type = "ZDP_CREATE_CAPTCHA_SRV.Header";// "ZDP_FRGT_USRNM_PWD_SRV.Header";
                GenerateCaptchaGUID forgotPasswordOTP = new GenerateCaptchaGUID();
                Metadata metadata = new Metadata();
                metadata.id = st;
                metadata.uri = st;
                metadata.type = type;

                GetCaptcha d = new GetCaptcha();
                d.__metadata = metadata;
                d.Captcha = "";
                d.Guid = "";
                d.Taxpayer = "";
                d.Refresh = "";
                d.Application = "FPWD";

                forgotPasswordOTP.d = d;
                forgotPasswordOTP = await WebServiceManager.GAZTCaptchaAndGUID(forgotPasswordOTP);
                PopToRootPage();// If seesion Expired it will navigate to Dashboard page

                if (forgotPasswordOTP?.d != null && !string.IsNullOrEmpty(forgotPasswordOTP.d.Captcha))
                {
                    captcha = forgotPasswordOTP.d.Captcha;
                    GUID = forgotPasswordOTP.d.Guid;
                    IsAPICalledSuccessfully = true;
                }
                else
                {

                }


                IsLoading = false;
            }

            catch (InternetException ex)
            {
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                //   await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                await Task.Run(() =>
                {
                    IsLoading = false;
                    // UserIDLayoutVisibility = true;
                });
            }
        }

        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
               MainThread.BeginInvokeOnMainThread(async () =>
                {

                    //var _navigation = Application.Current.MainPage.Navigation;
                    //_navigation.PopToRootAsync();
                    await PopupNavigation.Instance.PopAsync();
                });
            }
        }

        public async void ConfirmOtpBtnCommand(object obj)
        {
            try
            {
                string otp = OtpFirstDigit + OtpSecondDigit + OtpThirdDigit + OtpFourthDigit;

                PopUp popUp = new PopUp();
                StringBuilder Messages = new StringBuilder();
                StringBuilder PopMsg = new StringBuilder();
                bool IsAllValid = true;

                if (string.IsNullOrEmpty(OtpFirstDigit) || string.IsNullOrEmpty(OtpSecondDigit) || string.IsNullOrEmpty(OtpThirdDigit) || string.IsNullOrEmpty(OtpFourthDigit))
                {
                    IsAllValid = false;
                    IsOtpAPICalled = false;
                    PopMsg.Append(AppResources.AccountUnlockedCompleteRequiedFields);
                }
                else
                {
                    IsAllValid = true;
                }

                if (IsAllValid == false)
                {
                    if (PopMsg.Length > 0)
                    {
                        popUp.Message = PopMsg.ToString();
                        popUp.IsLinkAvailable = false;
                        IsOtpAPICalled = false;
                        if (App.IsArabic)
                        {
                            popUp.FlowDirections = "RightToLeft";
                            popUp.isFontSet = true;
                        }
                        else
                        {
                            popUp.FlowDirections = "LeftToRight";
                        }

                        await PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                    }
                }
                else
                {
                    currentAttempts++;
                    try
                    {
                        App.DisplayProgressView();
                        UnlockAccountModelOtp.Tin = UnlockAccountModel.Tin;
                        UnlockAccountModelOtp.Action = "02";
                        UnlockAccountModelOtp.Otp = otp;
                        UnlockAccountModelOtp.TaxpayerGuid = GUID;
                        UnlockAccountModelOtp.Zcaptcha = captcha;
                        UnlockAccountModelResponse = await VatRegistrationWebServiceManager.GaztUnlockAccountOtp(UnlockAccountModelOtp);
                        App.HideProgressView();

                        OtpFirstDigit = string.Empty;
                        OtpSecondDigit = string.Empty;
                        OtpThirdDigit = string.Empty;
                        OtpFourthDigit = string.Empty;

                        EnableChangePasswordView();
                    }
                    catch (GAZTUnlockAccountException ex)
                    {
                        IsOtpAPICalled = false;

                        OtpFirstDigit = string.Empty;
                        OtpSecondDigit = string.Empty;
                        OtpThirdDigit = string.Empty;
                        OtpFourthDigit = string.Empty;


                        if (PopupNavigation.Instance.PopupStack.Count > 0)
                            await PopupNavigation.Instance.PopAsync(true);

                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));



                    }
                    catch (InternetException ex)
                    {
                       MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            IsOtpAPICalled = false;
                            if (PopupNavigation.Instance.PopupStack.Count > 0)
                                await PopupNavigation.Instance.PopAsync(true);



                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZInternetConnectionMessage));

                        });
                    }
                    catch (Exception)
                    {
                       MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            IsOtpAPICalled = false;

                            OtpFirstDigit = string.Empty;
                            OtpSecondDigit = string.Empty;
                            OtpThirdDigit = string.Empty;
                            OtpFourthDigit = string.Empty;

                            App.HideProgressView();

                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));


                        });
                    }
                }
            }
            catch (Exception)
            {
                IsOtpAPICalled = false;

            }
        }

        public async void ExecuteResendOTPClickCommand(object obj)
        {
            IsResendOTPEnabled = false;
            ButtonDisableColor = (Color)Application.Current.Resources["NeutralGreay"];
            VerifyButtonDisableColor = (Color)Application.Current.Resources["Secondary"];
            IsVerifyOTPEnabled = true;
            IsOTPEntryEnable = true;
            //string _mobileNumber = App.TP.Mobile.Substring(App.TP.Mobile.Length - 4);
            //MobileNumber = "XXXXXXXXXX" + _mobileNumber;
            numberOfSeconds = 120;
            StopTimer = false;
            VerifyTinBtnCommand();
        }

        public async void ConfirmPasswordBtnCommand()
        {
            StringBuilder PopMsg = new StringBuilder();
            bool IsAllValid = true;

            if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
            {
                if (string.IsNullOrEmpty(Password))
                {
                    FramePasswordError = true;
                }

                if (string.IsNullOrEmpty(ConfirmPassword))
                {
                    FrameConfirmPasswordError = true;
                }

                if (PopMsg.Length > 0)
                {
                    PopMsg.Append(Environment.NewLine);
                    PopMsg.Append(Environment.NewLine);
                    PopMsg.Append(AppResources.AccountUnlockedCompleteRequiedFields);
                }
                else
                {
                    PopMsg.Append(AppResources.AccountUnlockedCompleteRequiedFields);
                }
                IsAllValid = false;
            }
            else
            {
                FramePasswordError = false;
                bool IsValidPass = UtilityManager.IsPasswordValid(Password);
                if (!IsValidPass)
                {
                    FramePasswordError = true;
                    //frmPass.HasError = true;
                    if (PopMsg.Length > 0)
                    {
                        PopMsg.Append(Environment.NewLine);
                        PopMsg.Append(Environment.NewLine);
                        PopMsg.Append(AppResources.UnlockAccountPasswordsSecurityPolicy);
                    }
                    else
                    {
                        PopMsg.Append(AppResources.UnlockAccountPasswordsSecurityPolicy);
                    }
                    IsAllValid = false;
                }
                else
                {
                    FramePasswordError = false;
                    // frmPass.HasError = false;
                }
                if (Password != ConfirmPassword)
                {
                    FramePasswordError = true;
                    //frmPass.HasError = true;
                    if (PopMsg.Length > 0)
                    {
                        PopMsg.Append(Environment.NewLine);
                        PopMsg.Append(Environment.NewLine);
                        PopMsg.Append(AppResources.UnlockAccountPasswordsDoesntMatch);
                    }
                    else
                    {
                        PopMsg.Append(AppResources.UnlockAccountPasswordsDoesntMatch);
                    }

                    IsAllValid = false;
                }
                else
                {
                    FrameConfirmPasswordError = false;
                    //  frmCfrmPass.HasError = false;
                }
            }

            //if (IsAllValid == true)
            //{
            //    await SetRequestObjectFirst();
            //    //viewModel.CreateGaZTAccount();
            //}

            if (IsAllValid == false)
            {
                if (PopMsg.Length > 0)
                {
                    PopUp popUp = new PopUp();
                    popUp.Message = PopMsg.ToString();
                    popUp.IsLinkAvailable = false;
                    if (App.IsArabic)
                    {
                        popUp.FlowDirections = "RightToLeft";
                        popUp.isFontSet = true;
                    }
                    else
                    {
                        popUp.FlowDirections = "LeftToRight";
                    }

                    await PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                }
            }
            else
            {
                try
                {
                    //App.DisplayProgressView();
                    UnlockAccountModelChangePassword.Tin = UnlockAccountModel.Tin;
                    UnlockAccountModelChangePassword.Action = "03";
                    UnlockAccountModelChangePassword.NewPassword = Password;
                    UnlockAccountModelChangePassword.ConfirmPassword = ConfirmPassword;
                    UnlockAccountModelChangePassword.TaxpayerGuid = GUID;
                    UnlockAccountModelChangePassword.Zcaptcha = captcha;
                    UnlockAccountModelResponse = await VatRegistrationWebServiceManager.GaztUnlockAccountChangePassword(UnlockAccountModelChangePassword);
                    PasswordChangedSuccessfully = AppResources.UnlockAccountPasswordChangedSuccessfully;
                    PasswordChangedSuccessfully = PasswordChangedSuccessfully.Replace("xxxxxx", UnlockAccountModelChangePassword.Tin);

                    App.HideProgressView();

                   MainThread.BeginInvokeOnMainThread(async () =>
                    {

                        await PopupNavigation.Instance.PopAsync();
                        _navigationService.NavigateTo(App.UnlockAccountSuccessPageView, PasswordChangedSuccessfully);
                    });

                }
                catch (GAZTUnlockAccountException ex)
                {
                    await Task.Run(() =>
                    {
                        App.HideProgressView();
                    });
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);

                }
                catch (InternetException ex)
                {
                   MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await Task.Run(() =>
                        {
                            App.HideProgressView();
                        });
                        await _dialogService.ShowMessage(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                    });
                }
                catch (Exception)
                {
                   MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await Task.Run(() =>
                        {
                            App.HideProgressView();
                        });
                        await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    });
                }
            }

        }
    }
}
