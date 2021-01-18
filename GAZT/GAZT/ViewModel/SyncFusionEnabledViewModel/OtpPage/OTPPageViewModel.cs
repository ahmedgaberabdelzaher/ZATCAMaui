using EGAZT.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.OTPPage_ViewModel
{
    [Preserve(AllMembers = true)]
    public class OTPPageViewModel : ViewModelBase
    {
        #region Variable
        private Dashboard DashboardData = null;
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public Command OnSubmitClicked { get; set; }
        public bool IsComingFromLogIn { get; set; }
        public ComingToOTPVerificationScreenFrom IsComingFrom { get; set; }
        public Command OnResendOTPClicked { get; set; }
        public ICommand BackButtonClicked { get; set; }
        CancellationTokenSource _CancellationTokenSource;
        int TotalSec;
        public bool StopTimer = false;
        public int currentAttempts = 0;
        bool isValiedOTP = false;
        public int numberOfSeconds = 120;
        public TaxEvasionSendSmsResponseModel taxEvasionSendSmsResponseModel;
        public TaxEvasionVerifySmsResponseModel taxEvasionVerifySmsResponseModel;

        #endregion
        #region Property
        private string _tesReporterMobileNumber = string.Empty;
        public string TesReporterMobileNumber
        {
            get
            {
                return _tesReporterMobileNumber;
            }
            set
            {
                _tesReporterMobileNumber = value;
                RaisePropertyChanged("TesReporterMobileNumber");
            }
        }
        private string _tesGeneratedOtpCode = string.Empty;
        public string TesGeneratedOtpCode
        {
            get
            {
                return _tesGeneratedOtpCode;
            }
            set
            {
                _tesGeneratedOtpCode = value;
                RaisePropertyChanged("TesGeneratedOtpCode");
            }
        }
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
        private string _tesMessageForSms = string.Empty;
        public string TesMessageForSms
        {
            get
            {
                return _tesMessageForSms;
            }
            set
            {
                _tesMessageForSms = value;
                RaisePropertyChanged("TesMessageForSms");
            }
        }
        //private string _resendButtonBackgroundColor = string.Empty;
        //public string ResendButtonBackgroundColor
        //{
        //    get
        //    {
        //        return _resendButtonBackgroundColor;
        //    }
        //    set
        //    {
        //        _resendButtonBackgroundColor = value;
        //        RaisePropertyChanged("ResendButtonBackgroundColor");
        //    }
        //}
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
        private string _tesotpSent = String.Empty;
        public string TesotpSent
        {
            get
            {
                return _tesotpSent;
            }
            set
            {
                _tesotpSent = value;
                RaisePropertyChanged("OTPSentOnThisBackup");
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
        private string _unmaskedMobileNumber = string.Empty;
        public string UnmaskedMobileNumber
        {
            get
            {
                return _unmaskedMobileNumber;
            }
            set
            {
                _unmaskedMobileNumber = value;
                RaisePropertyChanged("UnmaskedMobileNumber");
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
                OnSubmitClicked.ChangeCanExecute();
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
            BackButtonClicked = new Command(async() =>
            {   
                App.IsUserLoggedIn = false;
                _navigationService.GoBack();
            });
        }
        /// <summary>
        /// OTP validation
        /// </summary>
        /// <returns></returns>
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
                if (IsComingFrom == ComingToOTPVerificationScreenFrom.IsTes)
                {
                    await Task.Run(() =>
                    {
                        IsLoading = true;
                    });

                    try
                    {
                        TaxEvasionVerifySmsModel taxEvasionVerifySmsModel = new TaxEvasionVerifySmsModel();
                        taxEvasionVerifySmsModel.key = App.TaxEvasionUserData.LoginKey;
                        taxEvasionVerifySmsModel.code = EnteredOTP;

                        taxEvasionVerifySmsResponseModel = await WebServiceManager.GAZTTaxEvasionVerifySms(taxEvasionVerifySmsModel, UnmaskedMobileNumber);

                        if(taxEvasionVerifySmsResponseModel.Status == true)
                        {
                            TaxEvasionSendSmsModel taxEvasionSendSmsModel = new TaxEvasionSendSmsModel();
                            taxEvasionSendSmsModel.mobile = UnmaskedMobileNumber;

                            try
                            {
                                TaxEvasionUserRegistrationResponseModel taxEvasionUserRegistrationResponseModel = await WebServiceManager.GAZTTaxEvasionGetUserByMobile(taxEvasionSendSmsModel);
                                App.TaxEvasionUserData = taxEvasionUserRegistrationResponseModel.Data;

                                if (taxEvasionUserRegistrationResponseModel.Status == true)
                                {
                                    await navigateToListPage();
                                }
                            }
                            catch(Exception ex)
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await Task.Run(() =>
                                    {
                                        IsLoading = false;
                                    });

                                    _navigationService.NavigateTo(App.TaxEvasionRegistrationPageView);
                                });
                            }
                        }
                        else
                        {
                            await _dialogService.ShowMessageBox(taxEvasionVerifySmsResponseModel.SmsResponse.Message, AppResources.Information);
                        }

                        //string tesEnteredotp = EnteredOTP;
                        //if (TesGeneratedOtpCode == tesEnteredotp)
                        //{
                        //    await navigateToListPage();
                        //    //_navigationService.NavigateTo(App.TaxEvasionReportListPageView, TesReporterMobileNumber);
                        //}
                        //else
                        //{
                        //    EnteredOTP = string.Empty;
                        //    _dialogService.ShowMessageBox(AppResources.InvalidOTP, AppResources.Information);
                        //}
                    }
                    catch (GAZTException gex)
                    {
                        // Handle the GAZT custom exception.
                        string MessageForTheUser = gex.Message;
                        if (gex is GAZTInvalidDataException)
                        {
                            MessageForTheUser = AppResources.ZZSomethingwentwrong;
                        }
                        if (gex is GAZTNetworkConnectivityIssueException)
                        {
                            MessageForTheUser = AppResources.NetworkConnectivityIssue;
                        }
                        else if (gex is GAZTInternetException)
                        {
                            MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                        }
                        else if (gex is GAZTSessionExpiredException)
                        {
                            MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                        }
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await Task.Run(() =>
                            {
                                IsLoading = false;
                            });
                            await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                            //viewModel._navigationService.GoBack();
                        });
                    }
                    catch (Exception ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await Task.Run(() =>
                            {
                                IsLoading = false;
                            });

                            if (ex.Message.Contains("The entered code is incorrect") || ex.Message.Contains("الرمز المدخل غير صحيح"))
                            {
                                await _dialogService.ShowMessage(AppResources.InvalidOTP, AppResources.Information);
                            }
                            else
                            {
                                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                            }

                            //viewModel._navigationService.GoBack();
                        });
                    }

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
        }
        public async Task navigateToListPage()
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });

            _navigationService.NavigateTo(App.TaxEvasionReportListPageView, UnmaskedMobileNumber);
        }


        public async void ExecuteResendOTPClickCommand(object obj)
        {
            if (IsComingFrom == ComingToOTPVerificationScreenFrom.IsTes)
            {
                IsResendOTPEnabled = false;
                ButtonDisableColor = Color.FromHex("#9EA4A9");
                VerifyButtonDisableColor = Color.FromHex("#005e4b");
                IsVerifyOTPEnabled = true;
                IsOTPEntryEnable = true;
                //string _mobileNumber = App.TP.Mobile.Substring(App.TP.Mobile.Length - 4);
                //MobileNumber = "XXXXXXXXXX" + _mobileNumber;
                numberOfSeconds = 120;
                
                tessentOtptomobile();
            }
            else
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
                        if (IsComingFrom == ComingToOTPVerificationScreenFrom.IsLogin)
                        {
                            EmailOrMobileNumber = AppResources.MobileNumber;
                            var response = await WebServiceManager.GAZTSendAndReceiveOTP(lang, App.TP.Userid, currentAttempts.ToString());
                            await PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                            if (0 == String.Compare("OTP has send", response, true) || 0 == String.Compare("كلمة مرور مرة واحدة قد أرسلت", response, true))
                            {
                               
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
                        else if (IsComingFrom == ComingToOTPVerificationScreenFrom.IsMobile)
                        {
                            EmailOrMobileNumber = AppResources.MobileNumber;
                            string MobileCountry = string.Empty;
                            bool response = await WebServiceManager.GAZTValidateMobileNumber(lang, App.TP.Tin, App.TP.Mobile, App.TP.NewMobile,MobileCountry);
                            if (response)
                            {
                               
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
                        else if (IsComingFrom == ComingToOTPVerificationScreenFrom.IsEmail)
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
                                MobileNumber = _newEmail;// "XXXXXXXXXX" + _mobileNumber;
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
                    if (IsComingFrom == ComingToOTPVerificationScreenFrom.IsLogin)
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
                                //await Task.Run(() =>
                                //{
                                //    IsLoading = false;
                                //});
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
                                    //_navigationService.NavigateTo(App.SFLandingPageView);
                                    if (ComingToOTPVerificationScreenFromAndNavigatingTo.NavigateToThisService == App.MyBillsView)
                                    {
                                        _navigationService.NavigateTo(ComingToOTPVerificationScreenFromAndNavigatingTo.NavigateToThisService, new BillInfo());
                                    }
                                    else
                                    {
                                        if (ComingToOTPVerificationScreenFromAndNavigatingTo.NavigateToThisService == App.ZakatReturnListPageView)
                                        {
                                            DashboardData = WebServiceManager.GAZTGetDashboardData(UtilityManager.GetLanguageParameter(), App.TP.Userid);
                                            bool IsServiceAvail = false;
                                            string[] TpTypes = DashboardData.results[0].TpType.Split(',');
                                            foreach (string ItemType in TpTypes)
                                            {
                                                if (ItemType == "05")
                                                {
                                                    IsServiceAvail = true;
                                                }
                                            }
                                            if (IsServiceAvail == true)
                                            {
                                                _navigationService.NavigateTo(ComingToOTPVerificationScreenFromAndNavigatingTo.NavigateToThisService);
                                            }
                                            else
                                            {
                                                _dialogService.ShowMessage(AppResources.ZZTheselectedserviceisnotavailabletoyou, AppResources.Information);
                                                _navigationService.NavigateTo(App.SFLandingPageView);
                                            }
                                        }
                                        else if (ComingToOTPVerificationScreenFromAndNavigatingTo.NavigateToThisService == App.ICRListPageView)
                                        {
                                            DashboardData = WebServiceManager.GAZTGetDashboardData(UtilityManager.GetLanguageParameter(), App.TP.Userid);
                                            bool IsServiceAvail = false;
                                            string[] TpTypes = DashboardData.results[0].TpType.Split(',');
                                            foreach (string ItemType in TpTypes)
                                            {
                                                if (ItemType == "03" || ItemType == "13")
                                                {
                                                    IsServiceAvail = true;
                                                }
                                            }
                                            if (IsServiceAvail == true)
                                            {
                                                _navigationService.NavigateTo(ComingToOTPVerificationScreenFromAndNavigatingTo.NavigateToThisService);
                                            }
                                            else
                                            {
                                                _dialogService.ShowMessage(AppResources.ZZTheselectedserviceisnotavailabletoyou, AppResources.Information);
                                                _navigationService.NavigateTo(App.SFLandingPageView);
                                            }
                                        }
                                        else if (ComingToOTPVerificationScreenFromAndNavigatingTo.NavigateToThisService == App.SFLandingPageView)
                                        {
                                            _navigationService.NavigateTo(App.SFLandingPageView);
                                        }
                                        else
                                        {
                                            _navigationService.NavigateTo(ComingToOTPVerificationScreenFromAndNavigatingTo.NavigateToThisService);
                                        }
                                    }
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
                    else if (IsComingFrom == ComingToOTPVerificationScreenFrom.IsMobile)
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
                            string MobileCountry = string.Empty;
                            TP = await WebServiceManager.GAZTValidateOTPForMobileNumber(lang, OTP, App.TP.Tin, App.TP.Mobile, App.TP.NewMobile,MobileCountry);
                            await PopToRootPage();
                            if (TP != null)
                            {
                                string UpdatedMobile = App.TP.NewMobile;
                                App.TP.Mobile = UpdatedMobile;
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    string showmessage = AppResources.MobileNumberUpdatedSuccessfully;
                                    await _dialogService.ShowMessageBox(showmessage, AppResources.Information);
                                    //_navigationService.NavigateTo(App.DashboardPageView);
                                    IsLoading = true;
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

                                    try
                                    {
                                        App.httpClientHandler = new HttpClientHandler();
                                        App.httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                                    }
                                    catch(Exception ex)
                                    {

                                    }
                                    await LogOut();
                                    //_navigationService.NavigateTo(App.SFAnonymousLandingPageView);
                                    _navigation.NavigationStack.ToList().Clear();
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

                                            try
                                            {
                                                App.httpClientHandler = new HttpClientHandler();
                                                App.httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                                            }
                                            catch (Exception ex)
                                            {

                                            }

                                            await LogOut();
                                            //_navigationService.NavigateTo(App.SFAnonymousLandingPageView);
                                            _navigation.NavigationStack.ToList().Clear();
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

                                        await LogOut();
                                        //_navigationService.NavigateTo(App.SFAnonymousLandingPageView);
                                        _navigation.NavigationStack.ToList().Clear();
                                    });
                                }
                                await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                                ClearData();
                            });
                        }
                    }
                    else if (IsComingFrom == ComingToOTPVerificationScreenFrom.IsEmail)
                    {
                        currentAttempts++;
                        if (currentAttempts <= App.TP.Attempts)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessageBox(AppResources.MandatoryPasswordForEmailUpdatation, AppResources.Information);
                            });
                            String OTP = string.Empty;
                            OTP = EnteredOTP;
                            if (!string.IsNullOrEmpty(OTP))
                            {
                                App.Otp = OTP;
                            }
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                _navigationService.NavigateTo(App.ChangePasswordPageView, ComingToOTPVerificationScreenFrom.IsEmail);
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
            catch (ThreadInterruptedException ex)
            {
                await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
        }
        public async Task LogOut()
        {
            await Task.Run(() =>
            {
                App.DisplayProgressView();
            });
            if (App.TP != null)
                App.TP = null;
            if (App.PreviousIsArabic)
            {
                String langName = "ar-AE";
                AppResources.Culture = new CultureInfo(langName);
            }
            else
            {
                String langName = "en-US";
                AppResources.Culture = new CultureInfo(langName);
            }

            try
            {
                await WebServiceManager.GAZTLogOff();
            }
            catch
            {

            }

            await Task.Run(() =>
            {
                App.HideProgressView();
            });

            var _navigation = Application.Current.MainPage.Navigation;
            foreach (var item in _navigation.NavigationStack)
            {
                if (item.GetType().Name == App.GAZTNewDesignOnBoardingAnimationPageView)
                {
                    _navigation.RemovePage(item);
                    break;
                }
            }

            App.IsLogOut = true;
            App.IsLoginCalled = false;
            App.IsSamlApiCalledAndroid = false;

            try
            {
                App.httpClientHandler = new HttpClientHandler();
                App.httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                App.httpClientHandler.CookieContainer = new System.Net.CookieContainer();
            }
            catch (Exception ex)
            {

            }

            _navigationService.NavigateTo(App.GAZTNewDesignOnBoardingAnimationPageView);
            _navigation.NavigationStack.ToList().Clear();
            //var _navigation = Application.Current.MainPage.Navigation;
            //_navigation.PopToRootAsync();
        }
        #endregion
        #region Method
        public void ClearData()
        {
            EnteredOTP = string.Empty;
        }
        public async void OnPageLoad()
        {
            //if (ComingToOTPVerificationScreenFromAndNavigatingTo)
            if (ComingToOTPVerificationScreenFromAndNavigatingTo._ComingToOTPVerificationScreenFrom == ComingToOTPVerificationScreenFrom.IsTes)
            {
                string mobileNumber;
                TesReporterMobileNumber = ComingToOTPVerificationScreenFromAndNavigatingTo.MobileNumber;
                UnmaskedMobileNumber = TesReporterMobileNumber;

                mobileNumber = TesReporterMobileNumber.Substring(TesReporterMobileNumber.Length - 4);
                MobileNumber = "XXXXXXXXXX" + mobileNumber;

                StopTimer = true;
                IsVerifyOTPEnabled = true;
                VerifyButtonDisableColor = Color.FromHex("#005e4b");
                AccountWillBeBlocked = string.Empty;
            }
            else
            {
                try
                {
                    ShowAccountWIllBeLockedMessage();
                    FrmColour = "#B1B1B1";
                    if (App.TP != null && !string.IsNullOrEmpty(App.TP.Tin))
                    {
                        TinNumber = App.TP.Tin;
                    }
                    if (App.TP != null && !string.IsNullOrEmpty(App.TP.Mobile))
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
        }
        public async void tessentOtptomobile()
        {
            //TesSetGenerateOtp();

            TaxEvasionSendSmsModel taxEvasionSendSmsModel = new TaxEvasionSendSmsModel();
            taxEvasionSendSmsModel.mobile = App.TaxEvasionUserData.Mobile;
            try
            {
                numberOfSeconds = 120;

                await Task.Run(() =>
                {
                    IsLoading = true;
                });

                TaxEvasionSendSmsResponseModel taxEvasionSendSmsResponseModel = await WebServiceManager.GAZTTaxEvasionSendSms(taxEvasionSendSmsModel);

                await Task.Run(() =>
                {
                    IsLoading = false;
                });

                TimerStart(numberOfSeconds);

                if (taxEvasionSendSmsResponseModel.Status == true)
                {
                    App.TaxEvasionUserData.LoginKey = taxEvasionSendSmsResponseModel.Data.Key;
                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    });
                }
            }
            catch (GAZTException gex)
            {
                // Handle the GAZT custom exception.
                string MessageForTheUser = gex.Message;
                if (gex is GAZTInvalidDataException)
                {
                    MessageForTheUser = AppResources.ZZSomethingwentwrong;
                }

                if (gex is GAZTNetworkConnectivityIssueException)
                {
                    MessageForTheUser = AppResources.NetworkConnectivityIssue;
                }
                else if (gex is GAZTInternetException)
                {
                    MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                }
                else if (gex is GAZTSessionExpiredException)
                {
                    MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                }

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });

                    await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    //viewModel._navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });

                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    //viewModel._navigationService.GoBack();
                });
            }


            string mobnumber = TesReporterMobileNumber;
            TesMessageForSms = String.Format(AppResources.ZOTPformobileverificationis,
                             TesGeneratedOtpCode);
            try
            {
              

                string r = await WebServiceManager.GAZTTESVerfymobNoSendOtp(mobnumber, TesMessageForSms);
                if (!string.IsNullOrEmpty(r))
                {
                    if (Int32.Parse(r) < 0)
                    {
                        _dialogService.ShowMessageBox("Invalid Mobile number", AppResources.Information);
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {

            }
            //numberOfSeconds = 120;
            //TimerStart(numberOfSeconds);
        }
        private async Task SendOTPToRegisterMobileNumberToLogIn()
        {

        }
        public void TimerStart(int Seconds)
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
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });
            }
        }
        private async void AccountLockedMessage(TaxPayerProfile tp)
        {
            isValiedOTP = false;
            if (tp != null && tp.Result.Equals("User locked successfully") || tp.Result.Equals("*** لا توجد أية رسالة فيT100 ***"))
            {
                string remainingAttempts = (Convert.ToInt16(WebServiceManager.NumberOfValiedAttempts) - currentAttempts).ToString();
                string message = ShowAlertPopUpMessage(remainingAttempts);
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessageBox(message, AppResources.ZError);
                    _navigationService.GoBack();
                });
                isValiedOTP = false;
                //await _dialogService.ShowMessageBox(AppResources.ZYouraccounthasbeenlockedPleasecontactourcallcenter, AppResources.Information);
            }
            else if (tp != null && tp.Result.Equals("Valid OTP") || tp.Result.Equals("كلمة مرور صالحة لمرة واحدة"))
            {
                isValiedOTP = true;
            }
            else if ((tp.Result.Equals("Invalid OTP") || tp.Result.Equals("مكتب المدعي العام غير صالح")))
            {
                int Test;
                var TestMessage = string.Empty;
                int ValidAttempts = Convert.ToInt32(WebServiceManager.NumberOfValiedAttempts);
                int CurrentAttempts = Convert.ToInt32(currentAttempts);
                Test = ValidAttempts - CurrentAttempts;
                string Test1 = Convert.ToString(Test);
                TestMessage = ShowAlertPopUpMessage(Test1);
                isValiedOTP = false;
                // throw new Exception(TestMessage);
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessageBox(TestMessage, AppResources.ZError);
                });
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
                str = "سيتم قفل الحساب بعد إدخال" + " " + WebServiceManager.NumberOfValiedAttempts + " " + "رموز تحقق خاطئة";
            }
            return str;
        }
        public string ShowAlertPopUpMessage(string remainingAttempts)
        {
            string str = string.Empty;
            if (!App.IsArabic)
            {
                if (currentAttempts == 1)
                {
                    str = AppResources.InvalidOTP;
                }
                else if (currentAttempts > 1 && currentAttempts < Convert.ToInt16(WebServiceManager.NumberOfValiedAttempts))
                {
                    String strtemp = AppResources.ZYouhaveoneremainingattemptthentheaccountwillbelocked;// "You have {0} remaining attempt then the account will be locked";
                    String strSumberOfAttemptsRemaining = strtemp; // String.Empty;
                    str = " You have " + remainingAttempts + " remaining attempt then the account will be locked";// String.Format(strSumberOfAttemptsRemaining, remainingAttempts);
                    // str = "You have " + remainingAttempts + " remaining attempt then the account will be locked";
                }
                else
                {
                    //String strtemp = AppResources.ZYouraccounthasbeenlockedPleasecontactourcallcenter;// "You have {0} remaining attempt then the account will be locked";
                    //String strSumberOfAttemptsRemaining = strtemp; // String.Empty;
                    //str = String.Format(strSumberOfAttemptsRemaining, WebServiceManager.NumberOfValiedAttempts);

                    str = " Login attempt failed because of entering " + WebServiceManager.NumberOfValiedAttempts + " wrong verification codes";
                }
                //str = "You have " + remainingAttempts + "remaining attempt then the account will be locked";
            }
            else
            {
                if (currentAttempts == 1)
                {
                    str = AppResources.InvalidOTP;
                }
                else if (currentAttempts > 1 && currentAttempts < Convert.ToInt16(WebServiceManager.NumberOfValiedAttempts))
                {
                    //String strtemp = AppResources.ZYouhaveoneremainingattemptthentheaccountwillbelocked;// "You have {0} remaining attempt then the account will be locked";
                    //String strSumberOfAttemptsRemaining = strtemp; // String.Empty;
                    //str = String.Format(strSumberOfAttemptsRemaining, remainingAttempts);
                    str = "لديك " + remainingAttempts + " محاولات متبقية؛ ثم سيتم قفل حسابك   ";
                }
                else
                {
                    //String strtemp = AppResources.ZYouraccounthasbeenlockedPleasecontactourcallcenter;// "You have {0} remaining attempt then the account will be locked";
                    //String strSumberOfAttemptsRemaining = strtemp; // String.Empty;
                    //str = String.Format(strSumberOfAttemptsRemaining, WebServiceManager.NumberOfValiedAttempts);
                 //   تم مرات لإدخال رمز التحقق3 إلغاء محاولة الدخول بسبب الفشل
                  //  str = " مرات لإدخال رمز التح " + WebServiceManager.NumberOfValiedAttempts + " تم إلغاء محاولة الدخول بسبب الفشل ";
                   // str = " إلغاء محاولة الدخول بسبب الفشل " + WebServiceManager.NumberOfValiedAttempts + "تم  مرات لإدخال رمز التحقق";
                    //str = " تم  مرات لإدخال رمز التحقق " + WebServiceManager.NumberOfValiedAttempts + "إلغاء محاولة الدخول بسبب الفشل";
                    string str1 = "تم إلغاء الدخول مؤقتاً، لقد استنفذت 3 محاولات خاطئة لإدخال رمز التحقق";
                    str = str1.Replace("3", WebServiceManager.NumberOfValiedAttempts);
                }
            }
            return str;
        }
        public void TesSetGenerateOtp()
        {
            StringBuilder Captcha;
            try
            {
                Random random = new Random();
                string combination = "0123456789";
                StringBuilder captcha = new StringBuilder();
                for (int i = 0; i < 4; i++)
                    captcha.Append(combination[random.Next(combination.Length)]);
                Captcha = captcha;
                TesGeneratedOtpCode = Captcha.ToString();
            }
            catch
            {
                throw;
            }
            // return Captcha;
        }
        #endregion
    }
}
 