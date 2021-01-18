using System;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using System.Threading;
using Xamarin.Forms;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using System.Linq;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.ChangeEmailPage
{
    [Preserve(AllMembers = true)]
    public class UpdateEmailVerificationPageViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        public ICommand GoBackClick { get; set; }
        public Command OnSubmitClicked { get; set; }
        public bool IsComingFromLogIn { get; set; }
        public ComingToOTPVerificationScreenFrom IsComingFrom { get; set; }
        CancellationTokenSource _CancellationTokenSource;
        public Command OnResendOTPClicked { get; set; }
        public ICommand BackButtonClicked { get; set; }
        int TotalSec;
        public bool StopTimer = false;
        public int currentAttempts = 0;
        
        public int numberOfSeconds = 120;
        #endregion

        private bool _isNumberOfAttemptTextVisible = false;
        public bool IsNumberOfAttemptTextVisible
        {
            get
            {
                return _isNumberOfAttemptTextVisible;
            }
            set
            {
                _isNumberOfAttemptTextVisible = value;
                RaisePropertyChanged("IsNumberOfAttemptTextVisible");
            }
        }
        private string _mobileNumber = string.Empty;
        public string MobileNumber
        {
            get
            {
                return _mobileNumber;
            }
            set
            {
                _mobileNumber = value;
                RaisePropertyChanged("MobileNumber");
            }
        }
        private string _tinNumber = string.Empty;
        public string TinNumber
        {
            get
            {
                return _tinNumber;
            }
            set
            {
                _tinNumber = value;
                RaisePropertyChanged("TinNumber");
            }
        }
        private string _NewPasswordForEmail = string.Empty;
        public string NewPasswordForEmail
        {
            get
            {
                return _NewPasswordForEmail;
            }
            set
            {
                _NewPasswordForEmail = value;
                //if (!string.IsNullOrEmpty(_NewPasswordForEmail))
                //{
                //    IsEnabledRetypePasswordForEmail = true;
                //}
                //else
                //{
                //    IsEnabledRetypePasswordForEmail = false;
                //}
                RaisePropertyChanged("NewPasswordForEmail");
            }
        }
        private bool _IsEnabledNewPasswordForEmail = false;
        public bool IsEnabledNewPasswordForEmail
        {
            get
            {
                return _IsEnabledNewPasswordForEmail;
            }
            set
            {
                _IsEnabledNewPasswordForEmail = value;
                RaisePropertyChanged("IsEnabledNewPasswordForEmail");
            }
        }
        private bool _passwordVisibility = false;
        public bool PasswordVisibility
        {
            get
            {
                return _passwordVisibility;
            }
            set
            {
                _passwordVisibility = value;
                RaisePropertyChanged("PasswordVisibility");
            }
        }
        private string _RetypePasswordForEmail = string.Empty;
        public string RetypePasswordForEmail
        {
            get
            {
                return _RetypePasswordForEmail;
            }
            set
            {
                _RetypePasswordForEmail = value;
                if (!string.IsNullOrEmpty(_RetypePasswordForEmail))
                {
                    IsEnabledSubmitForEmail = true;
                }
                else
                {
                    IsEnabledSubmitForEmail = false;
                }
                RaisePropertyChanged("RetypePasswordForEmail");
            }
        }
        private string _OldEmail = string.Empty;
        public string OldEmail
        {
            get
            {
                return _OldEmail;
            }
            set
            {
                _OldEmail = value;
                RaisePropertyChanged("OldEmail");
            }
        }
        private string _NewEmail = string.Empty;
        public string NewEmail
        {
            get
            {
                return _NewEmail;
            }
            set
            {
                _NewEmail = value;
                RaisePropertyChanged("NewEmail");
            }
        }
        private bool _IsEnabledSubmitForEmail = false;
        public bool IsEnabledSubmitForEmail
        {
            get
            {
                return _IsEnabledSubmitForEmail;
            }
            set
            {
                _IsEnabledSubmitForEmail = value;
                RaisePropertyChanged("IsEnabledSubmitForEmail");
            }
        }
        private bool _IsEnabledRetypePasswordForEmail = false;
        public bool IsEnabledRetypePasswordForEmail
        {
            get
            {
                return _IsEnabledRetypePasswordForEmail;
            }
            set
            {
                _IsEnabledRetypePasswordForEmail = value;
                RaisePropertyChanged("IsEnabledRetypePasswordForEmail");
            }
        }
        private string _CurrentPassword = string.Empty;
        public string CurrentPassword
        {
            get
            {
                return _CurrentPassword;
            }
            set
            {
                _CurrentPassword = value;
                RaisePropertyChanged("CurrentPassword");
            }
        }
        private bool _passwordVisibilityForNewPassword = true;
        public bool PasswordVisibilityForNewPassword
        {
            get
            {
                return _passwordVisibilityForNewPassword;
            }
            set
            {
                _passwordVisibilityForNewPassword = value;
                RaisePropertyChanged("PasswordVisibilityForNewPassword");
            }
        }
        private bool _passwordVisibilityForOldPassword = true;
        public bool PasswordVisibilityForOldPassword
        {
            get
            {
                return _passwordVisibilityForOldPassword;
            }
            set
            {
                _passwordVisibilityForOldPassword = value;
                RaisePropertyChanged("PasswordVisibilityForOldPassword");
            }
        }
        private bool _passwordVisibilityForRetypePassword = true;
        public bool PasswordVisibilityForRetypePassword
        {
            get
            {
                return _passwordVisibilityForRetypePassword;
            }
            set
            {
                _passwordVisibilityForRetypePassword = value;
                RaisePropertyChanged("PasswordVisibilityForRetypePassword");
            }
        }
        private ComingToOTPVerificationScreenFrom _NavigateToOtpForEmailEnum;
        public ComingToOTPVerificationScreenFrom NavigateToOtpForEmailEnum
        {
            get
            {
                return _NavigateToOtpForEmailEnum;
            }
            set
            {
                _NavigateToOtpForEmailEnum = value;
                RaisePropertyChanged("NavigateToOtpForEmailEnum");
            }
        }
        public string ShowAccountWIllBeLockedMessage()
        {
            string str = string.Empty;
            if (!App.IsArabic)
            {
                str = "The account will be locked after entering " + WebServiceManager.NumberOfValiedAttempts + " wrong verification codes";
            }
            else
            {
                str = "سيتم قفل الحساب بعد إدخال" + " " + WebServiceManager.NumberOfValiedAttempts + " " + "رموز تحقق خاطئة";
            }
            return str;
        }
        private string _OTPSentOnThisText = String.Empty;
        public string OTPSentOnThisText
        {
            get
            {
                return _OTPSentOnThisText;
            }
            set
            {
                _OTPSentOnThisText = value;
                RaisePropertyChanged("OTPSentOnThisText");
            }
        }
        private TaxPayerProfile _TaxPayerProfile = App.TP;
        public TaxPayerProfile TaxPayerProfile
        {
            get
            {
                return _TaxPayerProfile;
            }
            set
            {
                _TaxPayerProfile = value;
                RaisePropertyChanged("TaxPayerProfile");
            }
        }

        #region Method
        private string _OTPSentOnThisEmail = String.Empty;
        public string OTPSentOnThisEmail
        {
            get
            {
                return _OTPSentOnThisEmail;
            }
            set
            {
                _OTPSentOnThisEmail = value;
                RaisePropertyChanged("OTPSentOnThisEmail");
            }
        }
        private String _enteredOTP = string.Empty;
        public String EnteredOTP
        {
            get
            {
                return _enteredOTP;
            }
            set
            {
                _enteredOTP = value;
                RaisePropertyChanged("EnteredOTP");
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
                OnSubmitClicked.ChangeCanExecute();
                RaisePropertyChanged("IsVerifyOTPEnabled");
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

        private ComingToOTPVerificationScreenFromAndNavigatingTo _ComingToOTPVerificationScreenFromAndNavigatingTo;
        public ComingToOTPVerificationScreenFromAndNavigatingTo ComingToOTPVerificationScreenFromAndNavigatingTo
        {
            get
            {
                return _ComingToOTPVerificationScreenFromAndNavigatingTo;
            }
            set
            {
                _ComingToOTPVerificationScreenFromAndNavigatingTo = value;
                RaisePropertyChanged("ComingToOTPVerificationScreenFromAndNavigatingTo");
            }
        }
        private string _frmColour = "#B1B1B1";
        public string FrmColour
        {
            get
            {
                return _frmColour;
            }
            set
            {
                _frmColour = value;
                RaisePropertyChanged("FrmColour");
            }
        }
        private bool _isLoading;
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
        private string _emailOrMobileNumber = AppResources.MobileNumber;
        public string EmailOrMobileNumber
        {
            get
            {
                return _emailOrMobileNumber;
            }
            set
            {
                _emailOrMobileNumber = value;
                RaisePropertyChanged("EmailOrMobileNumber");
            }
        }
        private Color _buttonDisableColor = Color.FromHex("#9EA4A9");
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
        private Color _verifybuttonDisableColor = Color.FromHex("#005e4b");
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
        private string _accountWillBeBlocked = string.Empty;
        public string AccountWillBeBlocked
        {
            get
            {
                return _accountWillBeBlocked;
            }
            set
            {
                _accountWillBeBlocked = value;
                RaisePropertyChanged("AccountWillBeBlocked");
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
                    ButtonDisableColor = Color.FromHex("#005e4b");
                    IsResendOTPEnabled = true;
                    VerifyButtonDisableColor = Color.FromHex("#9EA4A9");
                    IsVerifyOTPEnabled = false;
                    IsOTPEntryEnable = false;
                }
                RaisePropertyChanged("OTPValidDuration");
            }
        }
        public void OnPageLoad()
        {
            //if (ComingToOTPVerificationScreenFromAndNavigatingTo)

            try
            {
                ShowAccountWIllBeLockedMessage();
                FrmColour = "#B1B1B1";

                if (App.TP != null && !string.IsNullOrEmpty(App.TP.Tin))
                {
                    TinNumber = App.TP.Tin;
                    TaxPayerProfile = App.TP;

                }
                if (App.TP != null && !string.IsNullOrEmpty(App.TP.Mobile))
                {
                    string mobileNumber = App.TP.Mobile.Substring(App.TP.Mobile.Length - 4);
                    MobileNumber = "XXXXXXXXXX" + mobileNumber;
                    StopTimer = true;
                    IsVerifyOTPEnabled = true;
                    VerifyButtonDisableColor = Color.FromHex("#005e4b");
                    AccountWillBeBlocked = string.Empty;

                    // CurrentPassword = TaxPayerProfile.Password;
                    //CurrentPassword = App;
                    OldEmail = TaxPayerProfile.Email;
                    NewEmail = TaxPayerProfile.NewEmail;
                    NewPasswordForEmail = string.Empty;
                    RetypePasswordForEmail = string.Empty;
                    IsEnabledNewPasswordForEmail = true;
                    IsEnabledRetypePasswordForEmail = true;
                    CurrentPassword = string.Empty;
                }

                else
                {
                    throw new GAZTMobileNumberInProfileEmptyException();
                }
            }
            catch (Exception gex)
            {
                throw new GAZTMobileNumberInProfileEmptyException();
            }

        }
        #region Constructor
        public UpdateEmailVerificationPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            OnResendOTPClicked = new Command(ExecuteResendOTPClickCommand, CanExecuteResendOTPClickCommand);
            OnSubmitClicked = new Command(ExecuteSubmitClickCommand, CanExecuteSubmitClickCommand);
            _CancellationTokenSource = new CancellationTokenSource();
            _dialogService = dialogService;
            //OnSubmitClicked = new Command(async () =>
            //{
            //});
            //OnResendOTPClicked = new Command(async () =>
            //{
            //    if (IsResendOTPEnabled == true)
            //    {
            //    }
            //});
            BackButtonClicked = new Command(() =>
            {

                _navigationService.GoBack();
            });
        }
        #endregion
        public async void ExecuteSubmitClickCommand(object obj)
        {
            if (string.IsNullOrEmpty(EnteredOTP))
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessageBox(AppResources.ZZPleaseenteraccessCode, AppResources.Information);
                });
                FrmColour = "Red";
            }
            else
            {

                Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    await ValidateOTP();
                });
                Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            // FrmColour = "#B1B1B1";

        }
        public async void ExecuteResendOTPClickCommand(object obj)
        {

            //Task.Run(() =>
            //{
            //    IsLoading = true;
            //});
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                try
                {
                    string lang = UtilityManager.GetLanguageParameter();
                    if (IsComingFrom == ComingToOTPVerificationScreenFrom.IsEmail)
                    {
                        EmailOrMobileNumber = AppResources.Email;
                        bool response = await WebServiceManager.GAZTGetOTPForEmail(lang, App.TP.Userid, App.TP.Email, App.TP.NewEmail);
                        await PopToRootPage();
                        if (response)
                        {
                            
                            ButtonDisableColor = Color.FromHex("#9EA4A9");
                            VerifyButtonDisableColor = Color.FromHex("#005e4b");
                            IsResendOTPEnabled = false;
                            IsVerifyOTPEnabled = true;
                            IsOTPEntryEnable = true;
                            string _newEmail = App.TP.NewEmail;
                            //MobileNumber = _newEmail;// "XXXXXXXXXX" + _mobileNumber;
                            numberOfSeconds = 120;
                            TimerStart(numberOfSeconds);
                        }
                    }
                    //else if (IsComingFrom == ComingToOTPVerificationScreenFrom.IsTes)
                    //{ 
                    //}
                }
                catch (Exception ex)
                {
                }
                //});
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            //Task.Run(() =>
            //{
            //    IsLoading = false;
            //});
        }
        private async Task ValidateOTP()
        {
            try
            {
                try
                {
                    await Task.Run(() =>
                    {
                        IsLoading = true;
                    });

                    if (IsComingFrom == ComingToOTPVerificationScreenFrom.IsEmail)
                    {
                        currentAttempts++;
                        if (currentAttempts <= App.TP.Attempts)
                        {
                            //await _dialogService.ShowMessageBox(AppResources.MandatoryPasswordForEmailUpdatation, AppResources.Information);
                            String OTP = string.Empty;
                            OTP = EnteredOTP;
                            if (!string.IsNullOrEmpty(OTP))
                            {
                                App.Otp = OTP;
                            }
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await ChangePassword();
                                //_navigationService.NavigateTo(App.ChangePasswordPageView, ComingToOTPVerificationScreenFrom.IsEmail);
                            });
                        }
                        else
                        {
                            App.TP = null;
                            ClearData();
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await WebServiceManager.GAZTLogOff();
                                IsLoading = false;

                                var _navigation = Application.Current.MainPage.Navigation;
                                foreach (var item in _navigation.NavigationStack)
                                {
                                    if (item.GetType().Name == App.SFAnonymousLandingPageView)
                                    {
                                        _navigation.RemovePage(item);
                                        break;
                                    }
                                }

                                App.IsLogOut = true;
                                App.IsLoginCalled = false;
                                App.IsSamlApiCalledAndroid = false;
                                App.httpClientHandler.CookieContainer = new System.Net.CookieContainer();

                                _navigationService.NavigateTo(App.SFAnonymousLandingPageView);
                                _navigation.NavigationStack.ToList().Clear();

                            });
                        }
                    }
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });
                }
                catch (Exception ex)
                {
                    ClearData();
                }
            }
            catch (ThreadInterruptedException ex)
            {
                await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
        }

        #region Method
        private async Task ChangePassword()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    TaxPayerProfile TP = null;
                    String lang = "EN";

                    if (App.IsArabic == true)
                        lang = "AR";
                    try
                    {
                        if (NavigateToOtpForEmailEnum == ComingToOTPVerificationScreenFrom.IsEmail)
                        {
                            if (!string.IsNullOrEmpty(CurrentPassword))
                            {
                                if (0 == String.Compare(NewPasswordForEmail, RetypePasswordForEmail, true))
                                {
                                    TP = await WebServiceManager.GAZTValidateOTPForEmail(lang, App.Otp, TaxPayerProfile.Tin, OldEmail, NewEmail, CurrentPassword, NewPasswordForEmail);

                                    await PopToRootPage();
                                    if (TaxPayerProfile != null)
                                    {
                                        // CurrentPassword = NewPasswordForEmail;
                                        App.TP.Email = NewEmail;
                                        TaxPayerProfile.Email = NewEmail;
                                        App.TP.Password = NewPasswordForEmail;
                                        TaxPayerProfile.Password = NewPasswordForEmail;
                                        setPropertyForEmailUpdation(NewEmail);
                                        ClearEmailData();
                                        String OnAuthenticationSuccess = AppResources.ZEmailUpdatedSuccessfully;
                                        Device.BeginInvokeOnMainThread(async () =>
                                        {
                                            await _dialogService.ShowMessageBox(OnAuthenticationSuccess, AppResources.Information);
                                            Device.BeginInvokeOnMainThread(async () => {
                                                var _navigation = Application.Current.MainPage.Navigation;
                                                //await _navigation.PopToRootAsync();
                                                foreach (var item in _navigation.NavigationStack)
                                                {
                                                    if (item.GetType().Name == App.SFAnonymousLandingPageView)
                                                    {
                                                        _navigation.RemovePage(item);
                                                        break;
                                                    }
                                                }
                                                _navigationService.NavigateTo(App.SFAnonymousLandingPageView);
                                                _navigation.NavigationStack.ToList().Clear();
                                            });
                                        });
                                        ClearEmailData();
                                        App.IsComingFromDashboardToLogOff = false;
                                        Device.BeginInvokeOnMainThread(async () =>
                                        {
                                        });
                                    }
                                    else
                                    {
                                        String OnInvalidEmail = AppResources.InvalidEmail;
                                        Device.BeginInvokeOnMainThread(async () =>
                                        {
                                            await _dialogService.ShowMessageBox(OnInvalidEmail, AppResources.Information);
                                        });

                                    }
                                }
                                else
                                {
                                    String OnPasswordMatch = AppResources.ZZThenewpasswordmustnotmatchtheexistingpassword;
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        await _dialogService.ShowMessageBox(OnPasswordMatch, AppResources.Information);
                                    });
                                    ClearPasswordDataForEmail();
                                }
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await _dialogService.ShowMessageBox(AppResources.ZZMandatorydatanotentered, AppResources.ZError);
                                });
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            string InvalidOTP = AppResources.Invalidverificationcodeentered;// + "(" + AppResources.PleaseReVerify + ")";
                            await _dialogService.ShowMessageBox(InvalidOTP, AppResources.Alerts);

                            Device.BeginInvokeOnMainThread(async () =>
                                {
                                    if (currentAttempts == App.TP.Attempts)
                                    {
                                        App.TP = null;
                                        ClearData();
                                        Device.BeginInvokeOnMainThread(async () =>
                                        {
                                           // var _navigation = Application.Current.MainPage.Navigation;
                                            //await _navigation.PopToRootAsync();
                                            var _navigation = Application.Current.MainPage.Navigation;
                                            foreach (var item in _navigation.NavigationStack)
                                            {
                                                if (item.GetType().Name == App.SFAnonymousLandingPageView)
                                                {
                                                    _navigation.RemovePage(item);
                                                    break;
                                                }
                                            }
                                            _navigationService.NavigateTo(App.SFAnonymousLandingPageView);
                                            _navigation.NavigationStack.ToList().Clear();
                                        });
                                    }
                                    
                                   // ClearData();
                                    //ClearPasswordDataForEmail();
                                });
                           
                            //ClearPasswordDataForEmail();
                            //if (NavigateToOtpForEmailEnum == ComingToOTPVerificationScreenFrom.IsEmail)
                            // {
                            //var _navigation = Application.Current.MainPage.Navigation;
                            // await _navigation.PopAsync();
                            // _navigationService.NavigateTo(App.ChangeEmailPageView);
                            //}
                        });
                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
        }
        #endregion
        public void setPropertyForEmailUpdation(string newEmail)
        {
            OldEmail = newEmail;
            NewEmail = string.Empty;
            NewPasswordForEmail = string.Empty;
            RetypePasswordForEmail = string.Empty;
        }
        public void ClearEmailData()
        {
            NewEmail = string.Empty;
            NewPasswordForEmail = string.Empty;
            CurrentPassword = string.Empty;
            RetypePasswordForEmail = string.Empty;
        }
        public void ClearPasswordData()
        {
            NewPasswordForEmail = string.Empty;
            RetypePasswordForEmail = string.Empty;
        }
        public void ClearPasswordDataForEmail()
        {
            RetypePasswordForEmail = string.Empty;
            NewPasswordForEmail = string.Empty;
            IsEnabledRetypePasswordForEmail = true;
            IsEnabledNewPasswordForEmail = true;
        }
        public void ClearData()
        {
            EnteredOTP = string.Empty;
        }
        public void TimerStart(int Seconds)
        {
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
                        ButtonDisableColor = Color.FromHex("#005e4b");
                        IsResendOTPEnabled = true;
                        VerifyButtonDisableColor = Color.FromHex("#9EA4A9");
                        IsVerifyOTPEnabled = false;
                        IsOTPEntryEnable = false;
                        return false;
                    }
                    TotalSec = TotalSec - 1;
                    numberOfSeconds = TotalSec;
                    TimeSpan _TimeSpan = TimeSpan.FromSeconds(TotalSec);
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        OTPValidDuration = " " + string.Format("{0:00}:{1:00}", _TimeSpan.Minutes, _TimeSpan.Seconds);
                    });
                    return true;
                }
            });
        }
        private void TimerStop()
        {
            Interlocked.Exchange(ref _CancellationTokenSource, new CancellationTokenSource()).Cancel();
        }
        public async Task PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    //await _navigation.PopToRootAsync();
                    foreach (var item in _navigation.NavigationStack)
                    {
                        if (item.GetType().Name == App.SFAnonymousLandingPageView)
                        {
                            _navigation.RemovePage(item);
                            break;
                        }
                    }
                    _navigationService.NavigateTo(App.SFAnonymousLandingPageView);
                    _navigation.NavigationStack.ToList().Clear();
                });
            }
        }
        private bool IsMandatoryFieldEntered()
        {
            bool IsMandatoryFieldEntered = false;
            if (string.IsNullOrEmpty(CurrentPassword) || string.IsNullOrEmpty(NewPasswordForEmail) || string.IsNullOrEmpty(RetypePasswordForEmail))
            {
                IsMandatoryFieldEntered = false;
            }
            else
            {
                IsMandatoryFieldEntered = true;
            }
            return IsMandatoryFieldEntered;
        }
        private async Task ShowMandatoryFieldNotEnteredInformation(bool IsMandatoryFieldEntered)
        {
            bool _isMandatoryFieldEntered = IsMandatoryFieldEntered;
            try
            {
                if (!_isMandatoryFieldEntered)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(AppResources.ZZMandatorydatanotentered, AppResources.Alerts);
                        await Task.Run(() =>
                        {
                            IsLoading = false;
                        });
                    });
                }
            }
            catch (Exception ex)
            {
            }
        }
        private bool IsNewPasswordSameAsOldPasswordSame()
        {
            if (NewPasswordForEmail.Equals(App.TP.Password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool IsNewPasswordSameAsConfirmPassword()
        {
            if (NewPasswordForEmail.Equals(RetypePasswordForEmail))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
