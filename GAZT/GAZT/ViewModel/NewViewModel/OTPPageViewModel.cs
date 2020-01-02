using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GAZT.ViewModel.NewViewModel
{
    public class OTPPageViewModel: ViewModelBase
    {
        #region Variable
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        public ICommand OnSubmitClicked { get; set; }
        public bool IsComingFromLogIn { get; set; }
        public NavigateToOtp IsComingFrom { get; set; }

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

                        OTP = OTP1stNumberProvidedByTheUser + OTP2ndNumberProvidedByTheUser + OTP3rdNumberProvidedByTheUser + OTP4thNumberProvidedByTheUser;
                        
                        if (App.IsArabic == true)
                        {
                            lang = "AR";
                        }                      

                        TP = await WebServiceManager.GAZTValidateOTP(lang, App.TP.Userid, OTP);
                        await PopToRootPage();
                        if (TP != null)
                        {
                            String Password = App.TP.Password;
                            App.TP = TP;
                            App.TP.Password = Password;
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                _navigationService.NavigateTo(App.DashboardPageView);
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
                        OTP = OTP1stNumberProvidedByTheUser + OTP2ndNumberProvidedByTheUser + OTP3rdNumberProvidedByTheUser + OTP4thNumberProvidedByTheUser;
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
                                _navigationService.NavigateTo(App.TaxPayerProfilePageView);
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
                else if (IsComingFrom == NavigateToOtp.IsEmail)
                {
                    String OTP = string.Empty;

                    OTP = OTP1stNumberProvidedByTheUser + OTP2ndNumberProvidedByTheUser + OTP3rdNumberProvidedByTheUser + OTP4thNumberProvidedByTheUser;

                    if (!string.IsNullOrEmpty(OTP))
                    {
                        App.Otp = OTP;
                    }
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _navigationService.NavigateTo(App.ChangePasswordPageView, NavigateToOtp.IsEmail);
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
                var _navigation = Application.Current.MainPage.Navigation;
                await _navigation.PopToRootAsync();
            }
        }
        #endregion
    }
}
