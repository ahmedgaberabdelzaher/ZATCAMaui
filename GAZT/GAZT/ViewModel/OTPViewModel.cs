using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using GAZT.Manager;
using GAZT.Models;
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
            this.OnSubmitClicked = new Command(async () =>
            {
                if (IsComingFromLogIn)
                {
                    TaxPayerProfile TP = null;
                    try
                    {
                        String OTP = string.Empty;

                        String lang = "EN";
                        if (App.IsArabic == true)
                        {
                            lang = "AR";
                            OTP = OTP4thNumberProvidedByTheUser + OTP3rdNumberProvidedByTheUser + OTP2ndNumberProvidedByTheUser + OTP1stNumberProvidedByTheUser;
                        }
                        else
                        {
                            OTP = OTP1stNumberProvidedByTheUser + OTP2ndNumberProvidedByTheUser + OTP3rdNumberProvidedByTheUser + OTP4thNumberProvidedByTheUser;
                        }

                        TP = await WebServiceManager.GAZTValidateOTP(lang, App.TP.Userid, OTP);
                        if (TP != null)
                        {
                            String Password = App.TP.Password;
                            App.TP = TP;
                            App.TP.Password = Password;

                            _navigationService.NavigateTo(App.DashboardView);
                        }
                        else
                            await dialogService.ShowMessageBox("Probably invalid OTP, please try again", "Information");
                    }
                    catch (Exception ex)
                    {
                        await _dialogService.ShowMessageBox(ex.Message, "Information");
                    }
                }

                else
                {
                    TaxPayerProfile TP = null;

                    try
                    {

                        String OTP = string.Empty;

                        String lang = "EN";
                        if (App.IsArabic == true)
                        {
                            lang = "AR";
                            OTP = OTP4thNumberProvidedByTheUser + OTP3rdNumberProvidedByTheUser + OTP2ndNumberProvidedByTheUser + OTP1stNumberProvidedByTheUser;
                        }
                        else
                        {
                            OTP = OTP1stNumberProvidedByTheUser + OTP2ndNumberProvidedByTheUser + OTP3rdNumberProvidedByTheUser + OTP4thNumberProvidedByTheUser;
                        }

                        TP = await WebServiceManager.GAZTValidateOTPForMobileNumber(lang, OTP, App.TP.Userid, App.TP.Mobile, App.TP.NewMobile);
                        if (TP != null)
                        {
                            App.TP = TP;
                            _navigationService.NavigateTo(App.TaxPayerProfileView);
                        }
                        else
                            await dialogService.ShowMessageBox("Probably invalid OTP, please try again", "Information");
                    }
                    catch (Exception ex)
                    {
                        await _dialogService.ShowMessageBox(ex.Message, "Information");
                    }


                    // _navigationService.GoBack();
                }

            });

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
