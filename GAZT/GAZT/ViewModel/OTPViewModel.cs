using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GAZT
{
    public class OTPViewModel : ViewModelBase
    {
        #region Variable
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        public ICommand OnSubmitClicked { get; set; }
        public bool IsComingFromLogIn { get; set; }
        public ComingToOTPVerificationScreenFrom IsComingFrom { get; set; }
        #endregion

        #region Property
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

        private String _OTP1stNumberProvidedByTheUser;
        public String OTP1stNumberProvidedByTheUser
        {
            get
            {
                return _OTP1stNumberProvidedByTheUser;
            }
            set
            {
                _OTP1stNumberProvidedByTheUser = value;
                RaisePropertyChanged("OTP1stNumberProvidedByTheUser");
            }
        }
        private String _OTP2ndNumberProvidedByTheUser;
        public String OTP2ndNumberProvidedByTheUser
        {
            get
            {
                return _OTP2ndNumberProvidedByTheUser;
            }
            set
            {
                _OTP2ndNumberProvidedByTheUser = value;
                RaisePropertyChanged("OTP2ndNumberProvidedByTheUser");
            }
        }
        private String _OTP3rdNumberProvidedByTheUser;
        public String OTP3rdNumberProvidedByTheUser
        {
            get
            {
                return _OTP3rdNumberProvidedByTheUser;
            }
            set
            {
                _OTP3rdNumberProvidedByTheUser = value;
                RaisePropertyChanged("OTP3rdNumberProvidedByTheUser");
            }
        }

        private String _OTP4thNumberProvidedByTheUser;
        public String OTP4thNumberProvidedByTheUser
        {
            get
            {
                return _OTP4thNumberProvidedByTheUser;
            }
            set
            {
                _OTP4thNumberProvidedByTheUser = value;
                RaisePropertyChanged("OTP4thNumberProvidedByTheUser");
            }
        }


        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="navigationService"></param>
        /// <param name="dialogService"></param>
        public OTPViewModel(INavigationService navigationService, IDialogService dialogService)
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

            _dialogService = dialogService;
            OnSubmitClicked = new Command(async () =>
            {
                ValidateOTP();
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
                    if (IsComingFrom == ComingToOTPVerificationScreenFrom.IsLogin)
                    {
                        TaxPayerProfile TP = null;
                        try
                        {
                            String OTP = string.Empty;

                            String lang = "EN";

                            OTP = OTP1stNumberProvidedByTheUser + OTP2ndNumberProvidedByTheUser + OTP3rdNumberProvidedByTheUser + OTP4thNumberProvidedByTheUser;


                            if (App.IsArabic == true)
                            {
                                lang = "AR";
                            }
                            else
                            {
                            }

                            TP = await WebServiceManager.GAZTValidateOTP(lang, App.TP.Userid, OTP, "1");
                            await PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                            if (TP != null)
                            {
                                String Password = App.TP.Password;
                                App.TP = TP;
                                App.TP.Password = Password;
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    _navigationService.NavigateTo(App.DashboardView);
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
                            OTP = OTP1stNumberProvidedByTheUser + OTP2ndNumberProvidedByTheUser + OTP3rdNumberProvidedByTheUser + OTP4thNumberProvidedByTheUser;
                            if (App.IsArabic == true)
                            {
                                lang = "AR";
                            }
                            TP = await WebServiceManager.GAZTValidateOTPForMobileNumber(lang, OTP, App.TP.Tin, App.TP.Mobile, App.TP.NewMobile);
                            await PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                            if (TP != null)
                            {
                                string UpdatedMobile = App.TP.NewMobile;
                                App.TP.Mobile = UpdatedMobile;
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    string showmessage = AppResources.MobileNumberUpdatedSuccessfully;
                                    await _dialogService.ShowMessageBox(showmessage, AppResources.Information);
                                    _navigationService.GoBack();
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
                                await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                                ClearData();
                            });
                        }
                    }
                    else if (IsComingFrom == ComingToOTPVerificationScreenFrom.IsEmail)
                    {
                        String OTP = string.Empty;

                        OTP = OTP1stNumberProvidedByTheUser + OTP2ndNumberProvidedByTheUser + OTP3rdNumberProvidedByTheUser + OTP4thNumberProvidedByTheUser;

                        if (!string.IsNullOrEmpty(OTP))
                        {
                            App.Otp = OTP;
                        }
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            _navigationService.GoBack();
                        });

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
            OTP1stNumberProvidedByTheUser = string.Empty;
            OTP2ndNumberProvidedByTheUser = string.Empty;
            OTP3rdNumberProvidedByTheUser = string.Empty;
            OTP4thNumberProvidedByTheUser = string.Empty;
        }
        public void OnPageLoad()
        {
            
        }

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

        

        

        #endregion
    }
}
