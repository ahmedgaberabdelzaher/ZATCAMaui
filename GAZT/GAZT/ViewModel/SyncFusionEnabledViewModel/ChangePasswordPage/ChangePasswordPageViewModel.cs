using EGAZT.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.ChangePasswordPage_ViewModel
{
    [Preserve(AllMembers = true)]
    public class ChangePasswordPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnChangeEmailSubmitButtonClicked { get; set; }
        public ICommand BackButtonClicked { get; set; }
        #region Property
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
                if (!string.IsNullOrEmpty(_NewPasswordForEmail))
                {
                    IsEnabledRetypePasswordForEmail = true;
                }
                else
                {
                    IsEnabledRetypePasswordForEmail = false;
                }
                RaisePropertyChanged("NewPasswordForEmail");
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
        #endregion
        #region Constructor
        public ChangePasswordPageViewModel(INavigationService navigationService, IDialogService dialogService)
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
            BackButtonClicked = new Xamarin.Forms.Command(async () =>
            {
                if (!string.IsNullOrEmpty(CurrentPassword) ||
                  !string.IsNullOrEmpty(NewPasswordForEmail) || !string.IsNullOrEmpty(RetypePasswordForEmail)
  )
                {
                    var result = false;
                   

                    if (App.IsArabic)
                    {

                        result  = await Application.Current.MainPage.DisplayAlert
                                            (AppResources.Alerts, AppResources.ChangeEmailDiscardSave,
                                                AppResources.ZZZNoText, AppResources.ZZZYesText);
                        if (result == true)
                        {
                            return;

                        }
                        else // if it's equal to YES
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

                    return;
                }

            });
            OnChangeEmailSubmitButtonClicked = new Xamarin.Forms.Command(async () =>
            {
                Task.Run(() =>
                {
                    IsLoading = true;
                });
                bool _isMandatoryFieldEntered = IsMandatoryFieldEntered();
               // await ShowMandatoryFieldNotEnteredInformation(_isMandatoryFieldEntered);
                if (_isMandatoryFieldEntered)
                {
                    bool _isConfirmPasswordandNewPasswordMatch = IsNewPasswordSameAsConfirmPassword();
                    if (_isConfirmPasswordandNewPasswordMatch)
                    {
                        bool _isNewPasswordSameAsOldPasswordSame = IsNewPasswordSameAsOldPasswordSame();
                        if (!_isNewPasswordSameAsOldPasswordSame)
                        {
                            await ChangePassword();
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessageBox(AppResources.ZZThenewpasswordmustnotmatchtheexistingpassword, AppResources.Alerts);
                            });
                        }
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessageBox(AppResources.ZZZPasswordNotMatched, AppResources.Alerts);
                        });
                    }
                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(AppResources.ZZMandatorydatanotentered, AppResources.Alerts);
                    });
                }
                Task.Run(() =>
                {
                    IsLoading = false;
                });
            });
        }
        #endregion
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

                                                App.IsLogOut = true;
                                                App.IsLoginCalled = false;
                                                App.IsSamlApiCalledAndroid = false;
                                                await WebServiceManager.GAZTLogOff();

                                                var _navigation = Application.Current.MainPage.Navigation;
                                                await _navigation.PopToRootAsync();
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
                        else if (NavigateToOtpForEmailEnum == ComingToOTPVerificationScreenFrom.IsLogin)//For Default Password Change
                        {
                            if (!string.IsNullOrEmpty(CurrentPassword))
                            {
                                try
                                {
                                    if (0 == String.Compare(NewPasswordForEmail, RetypePasswordForEmail, true) && !string.IsNullOrEmpty(RetypePasswordForEmail) && !string.IsNullOrEmpty(RetypePasswordForEmail))
                                    {
                                        WebServiceManager.ErrorMessage = string.Empty;
                                        bool response = await WebServiceManager.GAZTValidateAndChangePassword(lang, TaxPayerProfile.Tin, CurrentPassword, NewPasswordForEmail);
                                        await PopToRootPage();
                                        if (response == true)
                                        {
                                           // CurrentPassword = NewPasswordForEmail;
                                            App.TP.Password = NewPasswordForEmail;
                                            TaxPayerProfile.Password = NewPasswordForEmail;
                                            String OnAuthenticationSuccess = AppResources.PassWordChangedSucessfully;
                                            Device.BeginInvokeOnMainThread(async () =>
                                            {
                                                await _dialogService.ShowMessageBox(OnAuthenticationSuccess, AppResources.Information);
                                            });


                                            App.IsComingFromDashboardToLogOff = false;
                                          

                                            var _navigation = Application.Current.MainPage.Navigation;
                                            Device.BeginInvokeOnMainThread(async () =>
                                            {
                                                App.IsLogOut = true;
                                                App.IsLoginCalled = false;
                                                App.IsSamlApiCalledAndroid = false;
                                                await WebServiceManager.GAZTLogOff();

                                                App.httpClientHandler.CookieContainer = new System.Net.CookieContainer();

                                                ClearPasswordData();

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

                                                _navigationService.NavigateTo(App.SFAnonymousLandingPageView);
                                                _navigation.NavigationStack.ToList().Clear();
                                            });
                                        }
                                        else
                                        {
                                            if(!string.IsNullOrEmpty(WebServiceManager.ErrorMessage))
                                            {
                                                Device.BeginInvokeOnMainThread(async () =>
                                                {
                                                    await _dialogService.ShowMessageBox(WebServiceManager.ErrorMessage, AppResources.Information);
                                                });
                                            }
                                            else
                                            {
                                                String OnInvalidPassword = AppResources.InvalidPassword;
                                                Device.BeginInvokeOnMainThread(async () =>
                                                {
                                                    await _dialogService.ShowMessageBox(OnInvalidPassword, AppResources.Information);
                                                });
                                            }
                                        }
                                    }
                                    else
                                    {
                                        String OnNotMatchAuthentication = AppResources.ZZThenewpasswordmustnotmatchtheexistingpassword;
                                        Device.BeginInvokeOnMainThread(async () =>
                                        {
                                            await _dialogService.ShowMessageBox(OnNotMatchAuthentication, AppResources.Information);
                                        });
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                                        ClearPasswordDataForEmail();
                                    });
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
                            string InvalidOTP = AppResources.InvalidOTP + "(" + AppResources.PleaseReVerify + ")";
                            await _dialogService.ShowMessageBox(InvalidOTP, AppResources.Information);
                            ClearPasswordDataForEmail();
                            if (NavigateToOtpForEmailEnum == ComingToOTPVerificationScreenFrom.IsEmail)
                            {
                                var _navigation = Application.Current.MainPage.Navigation;
                                await _navigation.PopAsync();
                                await _navigation.PopAsync();
                                _navigationService.NavigateTo(App.ChangeEmailPageView);
                            }
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
                    _navigationService.NavigateTo(App.SFAnonymousLandingPageView);
                    _navigation.NavigationStack.ToList().Clear();
                    //var _navigation = Application.Current.MainPage.Navigation;
                    //_navigation.PopToRootAsync();
                });
            }
        }
        public void ClearPasswordData()
        {
            NewPasswordForEmail = string.Empty;
            RetypePasswordForEmail = string.Empty;
        }
        public void OnPageLoad()
        {
            try
            {
                TaxPayerProfile = App.TP;
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
            catch(Exception ex)
            {
            }
        }
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
        public void ClearPasswordDataForEmail()
        {
            RetypePasswordForEmail = string.Empty;
            NewPasswordForEmail = string.Empty;
            IsEnabledRetypePasswordForEmail = false;
            IsEnabledNewPasswordForEmail = true;
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
            catch(Exception ex)
            {
            }
        }
        private bool IsNewPasswordSameAsOldPasswordSame()
        {
            if(NewPasswordForEmail.Equals(App.TP.Password))
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
