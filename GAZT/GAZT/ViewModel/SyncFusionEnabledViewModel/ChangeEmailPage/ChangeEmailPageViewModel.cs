using EGAZT.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.ChangeEmailPage_ViewModel
{
    [Preserve(AllMembers = true)]
    public class ChangeEmailPageViewModel: ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public static string emailIdValidation = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
        public ICommand OnVerifyEmailButtonClicked { get; set; }
        public ICommand BackButtonClicked { get; set; }
        #region Property
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
                if (!string.IsNullOrEmpty(_NewEmail))
                {
                    IsEnabledRetypeEmail = true;
                }
                else
                {
                    IsEnabledRetypeEmail = false;
                }
                RaisePropertyChanged("NewEmail");
            }
        }
        private string _RetypeEmail = string.Empty;
        public string RetypeEmail
        {
            get
            {
                return _RetypeEmail;
            }
            set
            {
                _RetypeEmail = value;
                if (!string.IsNullOrEmpty(_RetypeEmail))
                {
                    IsEnabledVerifyForEmail = true;
                }
                else
                {
                    IsEnabledVerifyForEmail = false;
                }
                RaisePropertyChanged("RetypeEmail");
            }
        }
        private bool _IsEnabledVerifyForEmail = false;
        public bool IsEnabledVerifyForEmail
        {
            get
            {
                return _IsEnabledVerifyForEmail;
            }
            set
            {
                _IsEnabledVerifyForEmail = value;
                RaisePropertyChanged("IsEnabledVerifyForEmail");
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
        private bool _IsEnabledRetypeEmail = false;
        public bool IsEnabledRetypeEmail
        {
            get
            {
                return _IsEnabledRetypeEmail;
            }
            set
            {
                _IsEnabledRetypeEmail = value;
                RaisePropertyChanged("IsEnabledRetypeEmail");
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
        private bool _IsEnabledNewEmail = true;
        public bool IsEnabledNewEmail
        {
            get
            {
                return _IsEnabledNewEmail;
            }
            set
            {
                _IsEnabledNewEmail = value;
                RaisePropertyChanged("IsEnabledNewEmail");
            }
        }
     
        #endregion

        #region Constructor
        public ChangeEmailPageViewModel(INavigationService navigationService, IDialogService dialogService)
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
                if (!string.IsNullOrEmpty(NewEmail) || !string.IsNullOrEmpty(RetypeEmail))
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
                OnVerifyEmailButtonClicked = new Xamarin.Forms.Command(async () =>
            {
               // Task.Run(() =>
                //{
                  //  IsLoading = true;
                //});
                bool _isMandatoryFieldEntered = IsMandatoryFieldEntered();
                bool IsNewEmailAndRetypeEmaiEqual = CompareNewEmailAndRetedEmail(NewEmail, RetypeEmail);
                bool _isNEwEmailAndOldEmailSame = IsNewEmailSameAsOldEmailSame();
                if (_isMandatoryFieldEntered)
                {
                    if(IsNewEmailAndRetypeEmaiEqual)
                    {
                        if (!_isNEwEmailAndOldEmailSame)
                        {
                            await VarifyEmail();
                        }
                        else
                        {
                            await ShowNewEmailAndOldEmailNotBeSameInformation();
                        }
                    }
                    else
                    {
                       await ShowNewEmailAndRetypedemailSameInformation();
                    }
                }
                else
                {
                    await ShowMandatoryFieldNotEnteredInformation(_isMandatoryFieldEntered);
                }
                //Task.Run(() =>
               // {
                 //   IsLoading = false;
                //});
            });
        }
        #endregion
        #region Method
        private async Task VarifyEmail()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    ComingToOTPVerificationScreenFrom NavigatingFromEmail = ComingToOTPVerificationScreenFrom.IsEmail;
                    String lang = "EN";
                    if (App.IsArabic == true)
                        lang = "AR";
                    try
                    {
                        bool IsValidNewEmail = IsValidEmailAddress(NewEmail);
                        bool IsValidRetypeEmail = IsValidEmailAddress(RetypeEmail);
                        if (IsValidNewEmail && IsValidRetypeEmail)
                        {
                            bool response = await WebServiceManager.GAZTGetOTPForEmail(lang, TaxPayerProfile.Tin, OldEmail, NewEmail);
                            PopToRootPage();
                            if (response == true)
                            {
                                IsEnabledRetypeEmail = false;
                                IsEnabledNewEmail = false;
                                App.TP.NewEmail = NewEmail;
                                String OnAuthenticationSuccess = AppResources.Emailverificationcodesentsuccessfully;
                                String OnSuccessfulAuthentication = AppResources.EnterVerificationCodeForEmail;
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                     await _dialogService.ShowMessageBox(OnAuthenticationSuccess + " " + OnSuccessfulAuthentication, AppResources.Information);
                                    //  _navigationService.NavigateTo(App.OTPPageView, new ComingToOTPVerificationScreenFromAndNavigatingTo { _ComingToOTPVerificationScreenFrom = NavigatingFromEmail, NavigateToThisService = String.Empty });
                                      _navigationService.NavigateTo(App.UpdateEmailVerificationPage, new ComingToOTPVerificationScreenFromAndNavigatingTo { _ComingToOTPVerificationScreenFrom = NavigatingFromEmail, NavigateToThisService = String.Empty });

                                    // _navigationService.NavigateTo(App.OTPPageView, NavigatingFromEmail);
                                });
                            }
                        }
                        else
                        {
                        //ClearEmailData();
                        IsEnabledRetypeEmail = false;
                            if (IsValidNewEmail || IsValidRetypeEmail)
                            {
                                String OnNotMatchAuthentication = AppResources.InvalidEmail;
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await _dialogService.ShowMessageBox(OnNotMatchAuthentication, AppResources.Information);
                                });
                            }
                            else
                            {
                                String OnNotMatchAuthentication = AppResources.NewEmailandRetypeEmailNotMatch;
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await _dialogService.ShowMessageBox(OnNotMatchAuthentication, AppResources.Information);
                                });
                            }
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
            TaxPayerProfile = App.TP;
            OldEmail = TaxPayerProfile.Email;
            NewEmail = string.Empty;
            RetypeEmail = string.Empty;
            IsEnabledNewEmail = true;
            IsEnabledRetypeEmail = true;
        }
        private bool IsValidEmailAddress(string EmailAddress)
        {
            Match emailMatch = Regex.Match(EmailAddress, emailIdValidation);
            if (emailMatch.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CompareNewEmailAndRetedEmail(string NewEmailId, string RetypeEmailId)
        {
            if (0 == String.Compare(NewEmailId.ToUpper(), RetypeEmailId.ToUpper(), true))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void PopToRootPage()
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
        public void ClearEmailData()
        {
            //NewEmail = string.Empty;
            //RetypeEmail = string.Empty;
            //  CurrentPasswordForEmail = string.Empty;
            IsEnabledNewEmail = true;
        }
        private bool IsMandatoryFieldEntered()
        {
            bool IsMandatoryFieldEntered = false;
            if (string.IsNullOrEmpty(OldEmail) || string.IsNullOrEmpty(NewEmail) || string.IsNullOrEmpty(RetypeEmail))
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
                });
            }
        }
        private async Task ShowNewEmailAndOldEmailNotBeSameInformation()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await _dialogService.ShowMessageBox(AppResources.ZZTheNewEmailMustNotMatchtheexistingEmail, AppResources.Alerts);
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            });
        }
        private bool IsNewEmailSameAsOldEmailSame()
        {
            if (NewEmail.ToUpper().Equals(App.TP.Email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private async Task ShowNewEmailAndRetypedemailSameInformation()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await _dialogService.ShowMessageBox(AppResources.NewEmailandRetypeEmailNotMatch, AppResources.ZError);
            });
        }
        #endregion
    }
}
