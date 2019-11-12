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

        private string _OTPSentOnThisMobileNumber = App.TP.Mobile;
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
            OnSubmitClicked = new Command(async () =>
            {
                await ValidateOTP();
            });

        }

        private async Task ValidateOTP()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });


                await Task.Run(async () =>
                {
                    if (IsComingFromLogIn)
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
                                    await _dialogService.ShowMessageBox("Probably invalid OTP, please try again", "Information");
                                    ClearData();
                                });
                            }
                        }
                        catch (Exception ex)
                        {

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessageBox(ex.Message, "Information");
                                ClearData();
                            });
                        }
                    }

                    else
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
                                App.TP = TP;
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    string showmessage = AppResources.MobileNumberUpdatedSuccessfully;
                                    await _dialogService.ShowMessageBox(showmessage, "Information");
                                    _navigationService.NavigateTo(App.TaxPayerProfileView);
                                });

                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    string showmessage = AppResources.MobileNumberUpdatedSuccessfully;
                                    await _dialogService.ShowMessageBox(showmessage, "Information");
                                    ClearData();
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessageBox(ex.Message, "Information");
                                ClearData();
                            });
                        }


                        // _navigationService.GoBack();
                    }
                });


                await Task.Run(() =>
                {
                    IsLoading = false;
                    ClearData();
                });
            }
            catch (Exception)
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
