using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GAZT.ViewModel.NewViewModel
{
    public class OTPPageViewModel: ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnSubmitClicked { get; set; }
        public bool IsComingFromLogIn { get; set; }
        public NavigateToOtp IsComingFrom { get; set; }
        public ICommand OnResendOTPClicked { get; set; }
        CancellationTokenSource _CancellationTokenSource;
        int TotalSec;
        public bool StopTimer = false;
        public int currentAttempts = 0;
        bool isValiedOTP = false;
        public int numberOfSeconds = 120;

        #endregion

        #region Property
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
        private string _OTPSentOnThis = String.Empty;
        public string OTPSentOnThis
        {
            get
            {
                return _OTPSentOnThis;
            }
            set
            {
                _OTPSentOnThis = value;
                RaisePropertyChanged("OTPSentOnThis");
            }
        }

        private string _oTPSentOnThisBackup = String.Empty;
        public string OTPSentOnThisBackup
        {
            get
            {
                return _oTPSentOnThisBackup;
            }
            set
            {
                _oTPSentOnThisBackup = value;
                RaisePropertyChanged("OTPSentOnThisBackup");
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

        private string _OTPSentOnThisMobileNumber = string.Empty;
        public string OTPSentOnThisMobileNumber
        {
            get
            {
                return _OTPSentOnThisMobileNumber;
            }
            set
            {
                _OTPSentOnThisMobileNumber = value;
                RaisePropertyChanged("OTPSentOnThisMobileNumber");
            }
        }

      
        private String _enteredOTP;
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
                RaisePropertyChanged("IsVerifyOTPEnabled");
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


        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="navigationService"></param>
        /// <param name="dialogService"></param>
        public OTPPageViewModel(INavigationService navigationService, IDialogService dialogService)
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

            _CancellationTokenSource = new CancellationTokenSource();
            _dialogService = dialogService;
            OnSubmitClicked = new Command(async () =>
            {
                if(string.IsNullOrEmpty(EnteredOTP))
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(AppResources.ZZPleaseenteraccessCode, AppResources.Information);
                    });
                    FrmColour = "Red";
                }
                else
                {
                    FrmColour = "#B1B1B1";
                    await ValidateOTP();
                }

               
            });
            OnResendOTPClicked = new Command(async () =>
            {
                await SendOTPToRegisterMobileNumberToLogIn();
            });


        }
        /// <summary>
        /// OTP validation
        /// </summary>
        /// <returns></returns>
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
                    if (IsComingFrom == NavigateToOtp.IsLogin)
                    {
                       
                        TaxPayerProfile TP = null;
                        try
                        {
                            String OTP = string.Empty;

                            String lang = "EN";

                            OTP = EnteredOTP;

                            if (App.IsArabic == true)
                            {
                                lang = "AR";
                            }
                            currentAttempts++;
                            TP = await WebServiceManager.GAZTValidateOTP(lang, App.TP.Userid, OTP, currentAttempts.ToString());
                           
                            AccountLockedMessage(TP);
                            if (!isValiedOTP)
                            {
                                await Task.Run(() =>
                                {
                                    IsLoading = false;
                                });
                                return;
                            }
                           



                            await PopToRootPage();
                            if (TP != null && isValiedOTP)
                            {
                                String Password = App.TP.Password;
                                App.TP = TP;
                                App.TP.Password = Password;
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    _navigationService.NavigateTo(App.SFLandingPageView);
                                });

                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    string isInvalidOtp = AppResources.InvalidOTP;
                                    await _dialogService.ShowMessageBox(isInvalidOtp, AppResources.Information);
                                    ClearData();
                                });
                            }
                        }
                        catch (Exception ex)
                        {

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                IsLoading = false;
                                await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                                ClearData();
                            });
                        }
                    }
                    else if (IsComingFrom == NavigateToOtp.IsMobile)
                    {
                        TaxPayerProfile TP = null;
                        
                        try
                        {
                           
                            String OTP = string.Empty;
                            String lang = "EN";
                            OTP = EnteredOTP;

                            if (App.IsArabic == true)
                            {
                                lang = "AR";
                            }
                           
                           
                                TP = await WebServiceManager.GAZTValidateOTPForMobileNumber(lang, OTP, App.TP.Tin, App.TP.Mobile, App.TP.NewMobile);
                            await PopToRootPage();
                           
                           
                                if (TP != null)
                                {
                                    string UpdatedMobile = App.TP.NewMobile;
                                    App.TP.Mobile = UpdatedMobile;
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        string showmessage = AppResources.MobileNumberUpdatedSuccessfully;
                                        await _dialogService.ShowMessageBox(showmessage, AppResources.Information);
                                        _navigationService.NavigateTo(App.DashboardPageView);
                                    });
                                }
                                else
                                {
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        currentAttempts++;
                                        if (currentAttempts == App.TP.Attempts)
                                        {
                                            App.TP = null;
                                            ClearData();
                                            Device.BeginInvokeOnMainThread(async () => {
                                                var _navigation = Application.Current.MainPage.Navigation;
                                                await _navigation.PopToRootAsync();
                                            });
                                        }
                                        string isInvalidOtp = AppResources.InvalidOTP;
                                        await _dialogService.ShowMessageBox(isInvalidOtp, AppResources.Information);
                                        ClearData();

                                    });
                                }
                            
                           
                        }
                        catch (Exception ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                currentAttempts++;
                                if (currentAttempts == App.TP.Attempts)
                                {
                                    App.TP = null;
                                    ClearData();
                                    Device.BeginInvokeOnMainThread(async () => {
                                        var _navigation = Application.Current.MainPage.Navigation;
                                        await _navigation.PopToRootAsync();
                                    });
                                }
                                await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                                ClearData();
                            });
                        }
                    }
                    else if (IsComingFrom == NavigateToOtp.IsEmail)
                    {
                        currentAttempts++;
                        if (currentAttempts <= App.TP.Attempts)
                        {
                            _dialogService.ShowMessageBox(AppResources.MandatoryPasswordForEmailUpdatation, AppResources.Information);
                            String OTP = string.Empty;

                            OTP = EnteredOTP;

                            if (!string.IsNullOrEmpty(OTP))
                            {
                                App.Otp = OTP;
                            }
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                _navigationService.NavigateTo(App.ChangePasswordPageView, NavigateToOtp.IsEmail);
                            });
                        }
                        else
                        {
                            App.TP = null;
                            ClearData();
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                var _navigation = Application.Current.MainPage.Navigation;
                                await _navigation.PopToRootAsync();
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
            catch(ThreadInterruptedException ex)
            {
                await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
        }
        #endregion
        #region Method
        public void ClearData()
        {
            EnteredOTP = string.Empty;
        }
        
        public void OnPageLoad()
        {
            try
            {
                ShowAccountWIllBeLockedMessage();
                FrmColour = "#B1B1B1";
                TinNumber = App.TP.Tin;
                if(App.TP != null && !string.IsNullOrEmpty(App.TP.Mobile))
                {
                    string mobileNumber = App.TP.Mobile.Substring(App.TP.Mobile.Length - 4);
                    MobileNumber = "XXXXXXXXXX" + mobileNumber;
                    StopTimer = true;
                    IsVerifyOTPEnabled = true;
                    VerifyButtonDisableColor = Color.FromHex("#005e4b");
                    AccountWillBeBlocked = string.Empty;
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
        private async Task SendOTPToRegisterMobileNumberToLogIn()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });



                await Task.Run(async () =>
                {


                    try
                    {
                        string lang = UtilityManager.GetLanguageParameter();

                        if (IsComingFrom == NavigateToOtp.IsLogin)
                        {
                            EmailOrMobileNumber = AppResources.MobileNumber;
                            var response = await WebServiceManager.GAZTSendAndReceiveOTP(lang, App.TP.Userid, currentAttempts.ToString());
                            await PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                        if (0 == String.Compare("OTP has send", response, true) || 0 == String.Compare("كلمة مرور مرة واحدة قد أرسلت", response, true))
                            {
                                bool IsNavigatingFromLogin = true;
                                IsResendOTPEnabled = false;
                                ButtonDisableColor = Color.FromHex("#9EA4A9");
                                VerifyButtonDisableColor = Color.FromHex("#005e4b");
                               
                                IsVerifyOTPEnabled = true;
                                IsOTPEntryEnable = true;
                                //string _mobileNumber = App.TP.Mobile.Substring(App.TP.Mobile.Length - 4);
                                //MobileNumber = "XXXXXXXXXX" + _mobileNumber;
                                numberOfSeconds = 120;
                            TimerStart(numberOfSeconds);

                            }

                        }
                        else if (IsComingFrom == NavigateToOtp.IsMobile)
                        {
                            EmailOrMobileNumber = AppResources.MobileNumber;
                            bool response = await WebServiceManager.GAZTValidateMobileNumber(lang, App.TP.Tin, App.TP.Mobile, App.TP.NewMobile);
                            if (response)
                            {
                                bool IsNavigatingFromLogin = true;
                                ButtonDisableColor = Color.FromHex("#9EA4A9");
                                VerifyButtonDisableColor = Color.FromHex("#005e4b");
                                IsResendOTPEnabled = false;
                                IsVerifyOTPEnabled = true;
                                IsOTPEntryEnable = true;
                                string _mobileNumber = App.TP.NewMobile.Substring(App.TP.Mobile.Length - 4);
                                MobileNumber = "XXXXXXXXXX" + _mobileNumber;
                                numberOfSeconds = 120;
                                TimerStart(numberOfSeconds);
                            }


                        }
                        else if (IsComingFrom == NavigateToOtp.IsEmail)
                        {
                            EmailOrMobileNumber = AppResources.Email;
                            bool response = await WebServiceManager.GAZTGetOTPForEmail(lang, App.TP.Userid, App.TP.Email, App.TP.NewEmail);
                            await PopToRootPage();
                            if (response)
                            {
                                bool IsNavigatingFromLogin = true;
                                ButtonDisableColor = Color.FromHex("#9EA4A9");
                                VerifyButtonDisableColor = Color.FromHex("#005e4b");
                                IsResendOTPEnabled = false;
                                IsVerifyOTPEnabled = true;
                                IsOTPEntryEnable = true;
                                string _newEmail = App.TP.NewEmail;
                                MobileNumber = _newEmail;// "XXXXXXXXXX" + _mobileNumber;
                                numberOfSeconds = 120;
                                TimerStart(numberOfSeconds);
                            }

                        }


                    }
                    catch (Exception ex)
                    {

                    }
                });





                //});



                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch(InternetException ex)
            {
                await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
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
                        return false;
                    }
                    else if(!StopTimer)
                    {
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
                    TotalSec =  TotalSec - 1;
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

        //static void OnTimerCancelChanged(BindableObject bindable, object oldvalue, object newvalue)
        //{
        //    ((OTPPageViewModel)bindable).TimerStop();
        //}

        //static void OnTimerTimeChanged(BindableObject bindable, object oldvalue, object newvalue)
        //{
        //    ((OTPPageViewModel)bindable).TimerStop();
        //    ((OTPPageViewModel)bindable).TimerStart();
        //}

        //public static readonly BindableProperty CountDownMinutesProperty = BindableProperty.Create("CountDownMinutes", typeof(int), typeof(OTPPageViewModel), 0, BindingMode.TwoWay, null, OnTimerTimeChanged);
        //public int CountDownMinutes
        //{
        //    get { return (int)base.GetValue(CountDownMinutesProperty); }
        //    set { base.SetValue(CountDownMinutesProperty, value); }
        //}

        //public static readonly BindableProperty CountDownSecondsProperty = BindableProperty.Create("CountDownSeconds", typeof(int), typeof(OTPPageViewModel), 0, BindingMode.TwoWay, null, OnTimerTimeChanged);
        //public int CountDownSeconds
        //{
        //    get { return (int)base.GetValue(CountDownSecondsProperty); }
        //    set { base.SetValue(CountDownSecondsProperty, value); }
        //}

        //public static readonly BindableProperty TimerCancelProperty = BindableProperty.Create("TimerCancel", typeof(bool), typeof(OTPPageViewModel), false, BindingMode.TwoWay, null, OnTimerCancelChanged);
        //public bool TimerCancel
        //{
        //    get { return (bool)base.GetValue(TimerCancelProperty); }
        //    set { base.SetValue(TimerCancelProperty, value); }
        //}

        public async Task PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () => {
                    var _navigation = Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });
            }
        }

        private async void AccountLockedMessage(TaxPayerProfile tp)
        {
             isValiedOTP = false;

            if(tp != null && tp.Result.Equals("User locked successfully") || tp.Result.Equals("*** لا توجد أية رسالة فيT100 ***"))
            {
                string remainingAttempts = (Convert.ToInt16(WebServiceManager.NumberOfValiedAttempts) - currentAttempts).ToString();
                string message = ShowAlertPopUpMessage(remainingAttempts);
                await _dialogService.ShowMessageBox(message, AppResources.ZError);
                isValiedOTP = false;
                //await _dialogService.ShowMessageBox(AppResources.ZYouraccounthasbeenlockedPleasecontactourcallcenter, AppResources.Information);
                _navigationService.GoBack();
            }
            else if (tp != null && tp.Result.Equals("Valid OTP") || tp.Result.Equals("كلمة مرور صالحة لمرة واحدة"))
            {
                isValiedOTP = true;
            }
            else if((tp.Result.Equals("Invalid OTP") || tp.Result.Equals("مكتب المدعي العام غير صالح")))
            {
                string remainingAttempts = (Convert.ToInt16(WebServiceManager.NumberOfValiedAttempts) - currentAttempts).ToString();
               string message =  ShowAlertPopUpMessage(remainingAttempts);
                await _dialogService.ShowMessageBox(message, AppResources.ZError);
                isValiedOTP = false;
            }
            //else if(tp != null && currentAttempts == 2 && (tp.Result.Equals("Invalid OTP") || tp.Result.Equals("مكتب المدعي العام غير صالح")))
            //{
            //    await _dialogService.ShowMessageBox(AppResources.ZYouhaveoneremainingattemptthentheaccountwillbelocked, AppResources.Alerts);
            //}
            ClearData();
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
                str = "سيتم قفل الحساب بعد إدخال" + " " + UtilityManager.ConvertNumerals(WebServiceManager.NumberOfValiedAttempts) + " " + "رموز تحقق خاطئة";

            }
            return str;
        }

        public string ShowAlertPopUpMessage(string remainingAttempts)
        {
            string str = string.Empty;

            if (!App.IsArabic)
            {
                if(currentAttempts == 1)
                {
                    str = AppResources.InvalidOTP;

                }
                else if(currentAttempts > 1 && currentAttempts < Convert.ToInt16(WebServiceManager.NumberOfValiedAttempts) )
                {
                    str = "You have " + remainingAttempts + " remaining attempt then the account will be locked";
                }
                else
                {
                    str = " Login attempt failed because of entering " + remainingAttempts + " wrong verification codes";
                }
                //str = "You have " + remainingAttempts + "remaining attempt then the account will be locked";
            }
            else
            {
                str = " Login attempt failed because of entering " + remainingAttempts + " wrong verification codes";
            }
            return str;
        }

        #endregion
    }
}
