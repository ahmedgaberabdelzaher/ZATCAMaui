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
    public class ChangeMobileNumberPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        public ICommand OnVerifyButtonClicked { get; set; }

        #region Property


        private string _NewMobile = "5";
        public string NewMobile
        {
            get
            {
                return _NewMobile;
            }
            set
            {
                _NewMobile = value;

                if (!String.IsNullOrWhiteSpace(_NewMobile) || !String.IsNullOrEmpty(_NewMobile))
                    if (_NewMobile.Length == 14)
                        IsVerifyEnabled = true;
                RaisePropertyChanged("NewMobile");
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

        private bool _IsVerifyEnabled = false;
        public bool IsVerifyEnabled
        {
            get
            {
                return _IsVerifyEnabled;
            }
            set
            {
                _IsVerifyEnabled = value;
                RaisePropertyChanged("IsVerifyEnabled");
            }
        }

        private string _CurrentMobile = string.Empty;
        public string CurrentMobile
        {
            get
            {
                return _CurrentMobile;
            }
            set
            {
                _CurrentMobile = value;
                RaisePropertyChanged("CurrentMobile");
            }
        }


     

        #endregion

        #region Constructor

        public ChangeMobileNumberPageViewModel(INavigationService navigationService, IDialogService dialogService)
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


            OnVerifyButtonClicked = new Xamarin.Forms.Command(async () =>
            {
                await VarifyMobileNumber();
            });
        }


        #endregion

        #region Method

        private async Task VarifyMobileNumber()
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });

            await Task.Run(async () =>
            {
                bool IsNavigatingFromLogin = false;
                NavigateToOtp NavigatingFromMobile = NavigateToOtp.IsMobile;
                String lang = "EN";
                if (App.IsArabic == true)
                    lang = "AR";
                try
                {
                    bool response = false;
                    var mobileNumber = "00966" + NewMobile;
                    bool isValidMobileNumber = IsValidMobileNumber(NewMobile);
                    if (isValidMobileNumber)
                    {
                        response = await WebServiceManager.GAZTValidateMobileNumber(lang, TaxPayerProfile.Tin, TaxPayerProfile.Mobile, mobileNumber);
                        await PopToRootPage();
                    }
                    else
                    {
                        Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessageBox(AppResources.EnterValidMobileNumber, AppResources.Information);
                        });
                        NewMobile = string.Empty;
                    }
                    if (response == true)
                    {
                        App.TP.NewMobile = mobileNumber;
                        String OnAuthenticationSuccess = AppResources.MobileNumberVerificationSuccessful;
                        String OnSuccessfulAuthentication = AppResources.EnterVerificationCode;
                        Device.BeginInvokeOnMainThread(async () => {
                            await _dialogService.ShowMessageBox(OnAuthenticationSuccess + " " + OnSuccessfulAuthentication, AppResources.Information);
                        });
                        ClearMobileData();
                        Device.BeginInvokeOnMainThread(async () => {
                            _navigationService.NavigateTo(App.OTPPageView, NavigatingFromMobile);
                        });
                    }
                }
                catch (Exception ex)
                {
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                    });
                }
            });

            await Task.Run(() =>
            {
                IsLoading = false;
            });


        }
        public void OnPageLoad()
        {
            TaxPayerProfile = App.TP;
            CurrentMobile = TaxPayerProfile.Mobile;
         
        }

        public bool IsValidMobileNumber(string mobileNumber)
        {
            if (!string.IsNullOrEmpty(mobileNumber) && mobileNumber.Substring(0, 1).Equals("5") && mobileNumber.Length == 9)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                var _navigation = Application.Current.MainPage.Navigation;
                await _navigation.PopToRootAsync();
            }
        }
        public void ClearMobileData()
        {
            NewMobile = string.Empty;

        }
        #endregion
    }
    }
