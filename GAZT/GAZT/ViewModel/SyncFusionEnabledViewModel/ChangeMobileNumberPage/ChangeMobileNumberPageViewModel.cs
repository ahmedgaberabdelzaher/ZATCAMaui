using EGAZT.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.ChangeMobileNumberPage_ViewModel
{
    [Preserve(AllMembers = true)]
    public class ChangeMobileNumberPageViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnVerifyButtonClicked { get; set; }
        public ICommand BackButtonClicked { get; set; }
        #region Property
        private string _NewMobile = string.Empty;
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
                    if (_NewMobile.Length == 15)
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
        private bool _newMobileNumberArabicLayout = false;
        public bool NewMobileNumberArabicLayout
        {
            get
            {
                return _newMobileNumberArabicLayout;
            }
            set
            {
                _newMobileNumberArabicLayout = value;
                RaisePropertyChanged("NewMobileNumberArabicLayout");
            }
        }
        private bool _newMobileNumberEnglishLayout = false;
        public bool NewMobileNumberEnglishLayout
        {
            get
            {
                return _newMobileNumberEnglishLayout;
            }
            set
            {
                _newMobileNumberEnglishLayout = value;
                RaisePropertyChanged("NewMobileNumberEnglishLayout");
            }
        }
        private string _txtCountryCode = string.Empty;
        public string TxtCountryCode
        {
            get
            {
                return _txtCountryCode;
            }
            set
            {

                _txtCountryCode = value;
                RaisePropertyChanged("TxtCountryCode");
            }
        }
        private string _countryCode = "";
        public string CountryCode
        {
            get
            {
                return _countryCode;
            }
            set
            {
                _countryCode = value;
                RaisePropertyChanged("CountryCode");
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
                Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    await VarifyMobileNumber();
                });
                Task.Run(() =>
                {
                    IsLoading = false;
                });
            });
            BackButtonClicked = new Xamarin.Forms.Command(async () =>
            {
                bool _isMandatoryFieldEntered = IsMandatoryFieldEntered();

                if (_isMandatoryFieldEntered)
                {
                    var result = false;
                    if (App.IsArabic)
                    {

                        result = await Application.Current.MainPage.DisplayAlert
                                            (AppResources.Alerts, AppResources.ChangeEmailDiscardSave,
                                                AppResources.ZZZNoText, AppResources.ZZZYesText);
                        if (result == true)
                        {
                            return;

                        }
                        else // if it's equal to NO
                        {
                            _navigationService.GoBack();


                        }
                    }
                    else
                    {

                        result = await Application.Current.MainPage.DisplayAlert
                                            (AppResources.Alerts, AppResources.ChangeEmailDiscardSave,
                                                AppResources.ZZZYesText, AppResources.ZZZNoText);

                        if (result == true)
                        {

                            _navigationService.GoBack();

                        }
                        else // if it's equal to NO
                        {
                            return; // just return to the page and do nothing.
                        }
                    }
                }
                else
                {
                    _navigationService.GoBack();

                }

            });
        }
        #endregion
        #region Method
        private async Task VarifyMobileNumber()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                bool _isMandatoryFieldEntered = IsMandatoryFieldEntered();
                await ShowMandatoryFieldNotEnteredInformation(_isMandatoryFieldEntered);
                if (!_isMandatoryFieldEntered)
                {
                    return;
                }
                await Task.Run(async () =>
                {
                   
                    ComingToOTPVerificationScreenFrom NavigatingFromMobile = ComingToOTPVerificationScreenFrom.IsMobile;
                    String lang = "EN";
                    if (App.IsArabic == true)
                        lang = "AR";
                    try
                    {
                        bool response = false;
                        //var mobileNumber = "+9665" + NewMobile;
                        var mobileNumber = TxtCountryCode + NewMobile;
                        if (App.IsArabic)
                        {
                             mobileNumber = TxtCountryCode + NewMobile;
                            //mobileNumber = "9665" + NewMobile + "+";
                        }
                        //string newCountryCodeString = TxtCountryCode.Replace("+", "00");
                        string MobileNumber = TxtCountryCode + NewMobile;

                        //string MobileNumber = "+9665" + NewMobile;
                       bool isValidMobileNumber = IsValidMobileNumber(NewMobile);
                        bool isNewMobileNumberSameAsOldMobileNumber;
                        if (Device.RuntimePlatform == Device.Android)
                        {
                            isNewMobileNumberSameAsOldMobileNumber = IsNewMobileNumberSameAsOldMobileNumber(mobileNumber);
                        }
                        else
                        {
                            isNewMobileNumberSameAsOldMobileNumber = IsNewMobileNumberSameAsOldMobileNumber(MobileNumber);
                        }
                        if (isValidMobileNumber)
                        {
                            if (!isNewMobileNumberSameAsOldMobileNumber)
                            {
                                string MobileCountry = string.Empty;
                                response = await WebServiceManager.GAZTValidateMobileNumber(lang, TaxPayerProfile.Tin, TaxPayerProfile.Mobile, mobileNumber,MobileCountry);
                                await PopToRootPage();
                            }
                            else
                            {
                                await ShowNewMobileNumberNotSameAsOldMobileNumberInformation();
                            }
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
                        //String OnAuthenticationSuccess = AppResources.MobileNumberVerificationSuccessful;
                        //String OnSuccessfulAuthentication = AppResources.EnterVerificationCode;
                        //Device.BeginInvokeOnMainThread(async () =>
                        //    {
                        //        await _dialogService.ShowMessageBox(AppResources.ZZPleaseusetheOTPtoactivatethenewnobilenumber, AppResources.Information);
                        //    });
                            ClearMobileData();
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                               
                                _navigationService.NavigateTo(App.OTPPageView, new ComingToOTPVerificationScreenFromAndNavigatingTo() { _ComingToOTPVerificationScreenFrom = NavigatingFromMobile, NavigateToThisService = String.Empty });
                                //  _navigationService.NavigateTo(App.OTPPageView, NavigatingFromMobile);
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
            catch(InternetException ex)
            {
                await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
        }

       
        public void OnPageLoad()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
               // CountryCode = "+9665";
            }
            else
            {
                if (App.IsArabic)
                {
                   // CountryCode = "9665+";
                }
                else
                {
                    //CountryCode = "+9665";
                }
            }
            TaxPayerProfile = App.TP;
            CurrentMobile = TaxPayerProfile.Mobile;
            SetNewMobileNumberLayoutVisibility();
        }
        private void SetNewMobileNumberLayoutVisibility()
        {
            if(App.IsArabic)
            {
                NewMobileNumberArabicLayout = true;
                NewMobileNumberEnglishLayout = false;
            }
            else
            {
                NewMobileNumberEnglishLayout = true;
                NewMobileNumberArabicLayout = false;
            }
       }
        public bool IsValidMobileNumber(string mobileNumber)
        {
           // if (!string.IsNullOrEmpty(mobileNumber)  && mobileNumber.Length == 15)
            //{
               return true;
            //}
            //else
            //{
              //  return false;
            //}
        }
        public async Task PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () => {
                    var _navigation = Application.Current.MainPage.Navigation;
                    foreach (var item in _navigation.NavigationStack)
                    {
                        if (item.GetType().Name == App.SFAnonymousLandingPageView)
                        {
                            _navigation.RemovePage(item);
                            break;
                        }
                    }
                    //_navigationService.NavigateTo(App.GAZTNewDesignOnBoardingAnimationPageView);
                    _navigationService.NavigateTo(App.SFAnonymousLandingPageView);
                    _navigation.NavigationStack.ToList().Clear();
                    //var _navigation = Application.Current.MainPage.Navigation;
                    //_navigation.PopToRootAsync();
                });
            }
        }
        public void ClearMobileData()
        {
           // NewMobile = "";
              NewMobile = string.Empty;
        }
        private bool IsMandatoryFieldEntered()
        {
            bool IsMandatoryFieldEntered = false;
            if (string.IsNullOrEmpty(CurrentMobile) || string.IsNullOrEmpty(NewMobile))
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
            if (!IsMandatoryFieldEntered)
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
        private async Task ShowNewMobileNumberNotSameAsOldMobileNumberInformation()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await _dialogService.ShowMessageBox(AppResources.ZZTheNewMobileNumberMustNotMatchtheexistingMobileNumber, AppResources.Alerts);
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            });
        }
        private bool IsNewMobileNumberSameAsOldMobileNumber(string newMobileNumber)
        {
            if (newMobileNumber.Equals(App.TP.Mobile))
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
