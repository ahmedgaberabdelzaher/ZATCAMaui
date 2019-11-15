using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using System;
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
            OnSubmitClicked = new Command(async() =>
            {
               ValidateOTP();
            });

        }

        private async void ValidateOTP()
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
                                //OTP = OTP4thNumberProvidedByTheUser + OTP3rdNumberProvidedByTheUser + OTP2ndNumberProvidedByTheUser + OTP1stNumberProvidedByTheUser;
                            }
                            else
                            {
                                // OTP = OTP1stNumberProvidedByTheUser + OTP2ndNumberProvidedByTheUser + OTP3rdNumberProvidedByTheUser + OTP4thNumberProvidedByTheUser;
                            }

                            TP = await WebServiceManager.GAZTValidateOTP(lang, App.TP.Userid, OTP);
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
                                // OTP = OTP4thNumberProvidedByTheUser + OTP3rdNumberProvidedByTheUser + OTP2ndNumberProvidedByTheUser + OTP1stNumberProvidedByTheUser;
                            }
                            else
                            {
                                // OTP = OTP1stNumberProvidedByTheUser + OTP2ndNumberProvidedByTheUser + OTP3rdNumberProvidedByTheUser + OTP4thNumberProvidedByTheUser;
                            }

                            TP = await WebServiceManager.GAZTValidateOTPForMobileNumber(lang, OTP, App.TP.Tin, App.TP.Mobile, App.TP.NewMobile);
                            if (TP != null)
                            {
                            //App.TP = TP;
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


                        // _navigationService.GoBack();
                    }
                    else if (IsComingFrom == NavigateToOtp.IsEmail)
                    {
                        String OTP = string.Empty;
                        
                        OTP = OTP1stNumberProvidedByTheUser + OTP2ndNumberProvidedByTheUser + OTP3rdNumberProvidedByTheUser + OTP4thNumberProvidedByTheUser;

                        if(!string.IsNullOrEmpty(OTP))
                        {
                            App.Otp = OTP;
                        }
                        _navigationService.GoBack();
                    }
               

                await Task.Run(() =>
                {
                    IsLoading = false;
                   // ClearData();
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
        #endregion
    }
}
