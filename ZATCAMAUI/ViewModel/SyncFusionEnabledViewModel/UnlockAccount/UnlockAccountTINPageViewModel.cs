using System.Text;
using System.Windows.Input;


using Mopups.Services;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.ForgotModel;
using ZATCAMAUI.Models.UnlockAccount;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Views.SyncFusionEnabledViews.AddPopPages;
using Result = ZATCAMAUI.Models.UnlockAccount.Result;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.UnlockAccount
{
    public class UnlockAccountTINPageViewModel : BaseViewModel
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
        public static string otpValidate = "";

        public int numberOfSeconds = 120;

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
                    IsResendOTPEnabled = true;
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        ButtonDisableColor = (Color)Application.Current.Resources["Primary"];

                        VerifyButtonDisableColor = (Color)Application.Current.Resources["ButtonGray"];
                    });
                    IsVerifyOTPEnabled = false;
                    IsOTPEntryEnable = false;
                    _currentAttempts = 0;
                    OTPValidDuration = " 0:00";
                    StopTimer = true;
                    numberOfSeconds = 0;
                    TotalSec = 0;
                }
                OnPropertyChanged("currentAttempts");
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
                OnPropertyChanged("IsTinContentViewVisible");
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
                OnPropertyChanged("IsOTPContentViewVisible");
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
                OnPropertyChanged("IsChangePasswordViewVisible");
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
                OnPropertyChanged("IsAccountUnlockedSuccessViewVisible");
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
                OnPropertyChanged("VerifyButtonText");
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
                OnPropertyChanged("VerifyButtonDisableColor");
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
                OnPropertyChanged("TxtTIN");
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
                OnPropertyChanged("OtpFirstDigit");
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
                OnPropertyChanged("OtpSecondDigit");
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
                OnPropertyChanged("OtpThirdDigit");
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

                OnPropertyChanged("OtpFourthDigit");

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
                OnPropertyChanged("MinEight");
            }
        }

        private string _CapsSmall;
        public string CapsSmall
        {
            get { return _CapsSmall; }
            set
            {
                _CapsSmall = value;
                OnPropertyChanged("CapsSmall");
            }
        }

        private string _MaxSixteen;
        public string MaxSixteen
        {
            get { return _MaxSixteen; }
            set
            {
                _MaxSixteen = value;
                OnPropertyChanged("MaxSixteen");
            }
        }

        private string _NumSymbol;
        public string NumSymbol
        {
            get { return _NumSymbol; }
            set
            {
                _NumSymbol = value;
                OnPropertyChanged("NumSymbol");
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
                OnPropertyChanged("IsOtpAPICalled");
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
                OnPropertyChanged("PrevOtp");
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
                OnPropertyChanged("PrevOtp");
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
                OnPropertyChanged("IsPasswordEncripted");
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
                OnPropertyChanged("IsConfirmPasswordEncripted");
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
                OnPropertyChanged("FramePasswordError");
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
                OnPropertyChanged("FrameConfirmPasswordError");
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
                OnPropertyChanged("Password");
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
                OnPropertyChanged("ConfirmPassword");
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
                OnPropertyChanged("MobileNumberMasked");
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
                OnPropertyChanged("PasswordChangedSuccessfully");
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
                OnPropertyChanged("UnlockAccountModel");
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
                OnPropertyChanged("UnlockAccountModelOtp");
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
                OnPropertyChanged("UnlockAccountModelChangePassword");
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
                OnPropertyChanged("UnlockAccountModelResponse");
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
                OnPropertyChanged("ContinueButtonnBackroundColor");
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
                OnPropertyChanged("IsContinueButtonEnable");
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
                OnPropertyChanged("IsResendOTPEnabled");
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
                OnPropertyChanged("IsVerifyOTPEnabled");
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
                OnPropertyChanged(nameof(IsOTPEntryEnable));
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
                OnPropertyChanged("ButtonDisableColor");
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
                    IsResendOTPEnabled = true;
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        ButtonDisableColor = (Color)Application.Current.Resources["Primary"];
                        VerifyButtonDisableColor = (Color)Application.Current.Resources["ButtonGray"];
                    });
                    IsVerifyOTPEnabled = false;
                    IsOTPEntryEnable = false;

                }
                OnPropertyChanged("OTPValidDuration");
            }
        }

        #region ConstructorF
        /// <summary>
        /// Initializes a new instance for the <see cref="UnlockAccountTINPageViewModel" /> class.
        /// </summary>
        public UnlockAccountTINPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
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

        public void EnableOtpView(Result result)
        {
            try
            {
                currentAttempts = 0;
                MobileNumberMasked = result.mobileNumber;
                StopTimer = true;

                IsVerifyOTPEnabled = true;
                IsResendOTPEnabled = false;
                IsOTPEntryEnable = true;
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    VerifyButtonDisableColor = (Color)Application.Current.Resources["Secondary"];
                    ButtonDisableColor = (Color)Application.Current.Resources["NeutralGreay"];
                });
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
            catch (Exception)
            {

            }
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
                        if (DeviceInfo.Platform == DevicePlatform.iOS)
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
                            IsResendOTPEnabled = true;
                            MainThread.BeginInvokeOnMainThread(() =>
                            {
                                ButtonDisableColor = (Color)Application.Current.Resources["Primary"];
                                VerifyButtonDisableColor = (Color)Application.Current.Resources["ButtonGray"];
                            });
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

                UnlockaccountOTP unlockaccountOTP = new UnlockaccountOTP();
                unlockaccountOTP.TIN = TxtTIN;
                unlockaccountOTP.taxpayerGuid = GUID;
                unlockaccountOTP.captchaCode = captcha;
                //unlockaccountOTP.language = UtilityManager.GetLanguageParameter();
                var result = await VatRegistrationWebServiceManager.SendOTP(unlockaccountOTP);
                //UnlockAccountModelResponse = await VatRegistrationWebServiceManager.GaztUnlockAccount(UnlockAccountModel);
                await Task.Run(() =>
                {
                    App.HideProgressView();
                });

                if (result != null)
                {
                    totalAttempts = Convert.ToInt16(result.result.attempts);
                    numberOfSeconds = 120;
                    EnableOtpView(result.result);
                }

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


                CAptchRequest model = new CAptchRequest();
                model.GUID = "";
                model.captchaCode = "";
                model.taxpayer = "";
                model.refresh = "";
                model.applicationName = "FPWD";


                var captchResponse = await WebServiceManager.CaptchaRequest(model);


                PopToRootPage();// If seesion Expired it will navigate to Dashboard page

                if (captchResponse?.result != null && !string.IsNullOrEmpty(captchResponse.result.captchaCode))
                {
                    captcha = captchResponse.result.captchaCode;
                    GUID = captchResponse.result.GUID;
                    IsAPICalledSuccessfully = true;
                }
                else
                {

                }


                IsLoading = false;
            }


            catch (InternetException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

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
                     await MopupService.Instance.PopAsync();
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

                        await MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                    }
                }
                else
                {
                    currentAttempts++;
                    try
                    {
                        App.DisplayProgressView();
                        UnlockAccountValidate unlockaccountOTP = new UnlockAccountValidate();
                        unlockaccountOTP.TIN = TxtTIN;
                        unlockaccountOTP.OTP = otp;
                        otpValidate = otp;
                        unlockaccountOTP.taxpayerGuid = GUID;
                        unlockaccountOTP.captchaCode = captcha;
                        unlockaccountOTP.language = WebServiceManager.GetLangZParameterAREN();

                        var result = await VatRegistrationWebServiceManager.ValidateOTP(unlockaccountOTP);
                        App.HideProgressView();

                        OtpFirstDigit = string.Empty;
                        OtpSecondDigit = string.Empty;
                        OtpThirdDigit = string.Empty;
                        OtpFourthDigit = string.Empty;

                        if (result != null)
                        {
                            EnableChangePasswordView();
                        }
                    }
                    catch (GAZTUnlockAccountException ex)
                    {
                        IsOtpAPICalled = false;

                        OtpFirstDigit = string.Empty;
                        OtpSecondDigit = string.Empty;
                        OtpThirdDigit = string.Empty;
                        OtpFourthDigit = string.Empty;


                        if (MopupService.Instance.PopupStack.Count > 0)
                            await MopupService.Instance.PopAsync(true);

                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));



                    }
                    catch (InternetException)
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                         {
                             IsOtpAPICalled = false;
                             if (MopupService.Instance.PopupStack.Count > 0)
                                 await MopupService.Instance.PopAsync(true);



                             await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZInternetConnectionMessage));

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

                             await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));


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
            MainThread.BeginInvokeOnMainThread(() =>
            {
                ButtonDisableColor = (Color)Application.Current.Resources["NeutralGreay"];
                VerifyButtonDisableColor = (Color)Application.Current.Resources["Secondary"];
            });
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

                    await MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                }
            }
            else
            {
                try
                {
                    App.DisplayProgressView();
                    UnlockAccountChangePwd unlockAccountChangePwd = new UnlockAccountChangePwd();
                    unlockAccountChangePwd.TIN = TxtTIN;
                    unlockAccountChangePwd.OTP = otpValidate;
                    unlockAccountChangePwd.newPassword = Password;
                    unlockAccountChangePwd.confirmPassword = ConfirmPassword;
                    unlockAccountChangePwd.taxpayerGuid = GUID;
                    unlockAccountChangePwd.captchaCode = captcha;
                    var result = await VatRegistrationWebServiceManager.ChangePwd(unlockAccountChangePwd);

                    if (result != null)
                    {
                        //UnlockAccountModelResponse = await VatRegistrationWebServiceManager.GaztUnlockAccountChangePassword(UnlockAccountModelChangePassword);
                        PasswordChangedSuccessfully = AppResources.UnlockAccountPasswordChangedSuccessfully;
                        PasswordChangedSuccessfully = PasswordChangedSuccessfully.Replace("xxxxxx", UnlockAccountModelChangePassword.Tin);

                    }

                    App.HideProgressView();
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        _navigationService.GoBack();
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
                catch (InternetException)
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
