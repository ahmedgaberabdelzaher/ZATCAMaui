using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GAZT
{
    public class TaxPayerProfileViewModel : ViewModelBase
    {
        #region Variable
        private readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnChangeMobileNumberClicked { get; set; }
        public ICommand OnChangeEmailClicked { get; set; }
        public ICommand OnChangePasswordClicked { get; set; }
        public ICommand OnSubmitButtonClicked { get; set; }
        public ICommand OnVerifyButtonClicked { get; set; }
        public ICommand OnChangeEmailSubmitButtonClicked { get; set; }
        public ICommand OnChangePasswordButtonClicked { get; set; }
        public ICommand OnHomeIconClicked { get; set; }
        public ICommand OnVerifyEmailButtonClicked { get; set; }
        public ICommand OnReTypePasswordVisibilityClicked { get; set; }
        public ICommand OnNewPasswordVisibilityClicked { get; set; }
        public static string emailIdValidation = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
        public bool IscomingFromOTPViewViaEmail = false;

        #endregion

        #region Property

        private bool _tPProfileVisibility = true;
        public bool TPProfileVisibility
        {
            get
            {
                return _tPProfileVisibility;
            }
            set
            {
                _tPProfileVisibility = value;
                RaisePropertyChanged("TPProfileVisibility");
            }
        }

        private double _entryHeight;
        public double EntryHeight
        {
            get
            {
                return _entryHeight;
            }
            set
            {
                _entryHeight = value;
                RaisePropertyChanged("EntryHeight");
            }
        }
        
        private bool _changeMobileNumberLayoutVisibility = false;
        public bool ChangeMobileNumberLayoutVisibility
        {
            get
            {
                return _changeMobileNumberLayoutVisibility;
            }
            set
            {
                _changeMobileNumberLayoutVisibility = value;
                RaisePropertyChanged("ChangeMobileNumberLayoutVisibility");
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

        private bool _changeEmailLayoutVisibility = false;
        public bool ChangeEmailLayoutVisibility
        {
            get
            {
                return _changeEmailLayoutVisibility;
            }
            set
            {
                _changeEmailLayoutVisibility = value;
                RaisePropertyChanged("ChangeEmailLayoutVisibility");
            }
        }


        private bool _changeEmailLayoutVisibilityForPassword = false;
        public bool ChangeEmailLayoutVisibilityForPassword
        {
            get
            {
                return _changeEmailLayoutVisibilityForPassword;
            }
            set
            {
                _changeEmailLayoutVisibilityForPassword = value;
                RaisePropertyChanged("ChangeEmailLayoutVisibilityForPassword");
            }
        }


        private bool _changePasswordayoutVisibility = false;
        public bool ChangePasswordayoutVisibility
        {
            get
            {
                return _changePasswordayoutVisibility;
            }
            set
            {
                _changePasswordayoutVisibility = value;
                RaisePropertyChanged("ChangePasswordayoutVisibility");
            }
        }




        private string _NewMobile = "00966";
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

        private string _NewPassword = string.Empty;
        public string NewPassword
        {
            get
            {
                return _NewPassword;
            }
            set
            {
                _NewPassword = value;
                RaisePropertyChanged("NewPassword");
            }
        }

        private string _RetypePassword = string.Empty;
        public string RetypePassword
        {
            get
            {
                return _RetypePassword;
            }
            set
            {
                _RetypePassword = value;
                RaisePropertyChanged("RetypePassword");
            }
        }
        //

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

        private string _CurrentPasswordForEmail = string.Empty;
        public string CurrentPasswordForEmail
        {
            get
            {
                return _CurrentPasswordForEmail;
            }
            set
            {
                _CurrentPasswordForEmail = value;
                RaisePropertyChanged("CurrentPasswordForEmail");
            }
        }

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

        private bool _reTypePasswordVisibility = true;
        public bool ReTypePasswordVisibility
        {
            get
            {
                return _reTypePasswordVisibility;
            }
            set
            {
                _reTypePasswordVisibility = value;
                RaisePropertyChanged("ReTypePasswordVisibility");
            }
        }

        private bool _newPasswordVisibility = true;
        public bool NewPasswordVisibility
        {
            get
            {
                return _newPasswordVisibility;
            }
            set
            {
                _newPasswordVisibility = value;
                RaisePropertyChanged("NewPasswordVisibility");
            }
        }
        #endregion
        #region Constructor
        public TaxPayerProfileViewModel(INavigationService navigationService, IDialogService dialogService)
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
            ChangeEmailLayoutVisibilityForPassword = false;
            OnChangeMobileNumberClicked = new Command(() =>
            {
                TPProfileVisibility = false;
                ChangeMobileNumberLayoutVisibility = true;
            });
            OnChangeEmailClicked = new Command(() =>
            {
                TPProfileVisibility = false;
                ChangePasswordayoutVisibility = false;
                ChangeEmailLayoutVisibility = true;

            });
            OnChangePasswordClicked = new Command(() =>
            {
                TPProfileVisibility = false;
                ChangePasswordayoutVisibility = true;
                CurrentPassword = string.Empty;
            });
            OnSubmitButtonClicked = new Command(() =>
            {
                ChangeMobileNumberLayoutVisibility = false;
                TPProfileVisibility = true;

            });
            OnHomeIconClicked = new Command(() =>
            {
                _navigationService.GoBack();
            });
            OnVerifyEmailButtonClicked = new Command(async () =>
            {
                NavigateToOtp NavigatingFromEmail = NavigateToOtp.IsEmail;
                String lang = "EN";
                if (App.IsArabic == true)
                    lang = "AR";
                try
                {
                    try
                    {
                        bool IsValidNewEmail = IsValidEmailAddress(NewEmail);
                        bool bIsValidRetypeEmail = IsValidEmailAddress(RetypeEmail);

                        bool IsNewEmailAndRetypeEmaiEqual = CompareNewEmailAndRetedEmail(NewEmail, RetypeEmail);

                        if (IsValidNewEmail && bIsValidRetypeEmail && IsNewEmailAndRetypeEmaiEqual)
                        {
                            bool response = await WebServiceManager.GAZTGetOTPForEmail(lang, TaxPayerProfile.Tin, OldEmail, NewEmail);

                            if (response == true)
                            {

                                IsEnabledNewPasswordForEmail = true;
                                IsEnabledRetypeEmail = false;
                                IsEnabledNewEmail = false;
                                IscomingFromOTPViewViaEmail = true;
                                App.TP.NewEmail = NewEmail;
                                String OnAuthenticationSuccess = AppResources.Emailverificationcodesentsuccessfully;
                                String OnSuccessfulAuthentication = AppResources.EnterVerificationCodeForEmail;
                                await _dialogService.ShowMessageBox(OnAuthenticationSuccess + ":" + OnSuccessfulAuthentication, AppResources.Information);

                                _navigationService.NavigateTo(App.OTPView, NavigatingFromEmail);

                                ChangeEmailLayoutVisibility = false;
                                ChangeEmailLayoutVisibilityForPassword = true;

                            }
                        }
                        else
                        {
                            ClearEmailData();
                            IsEnabledRetypeEmail = false;
                            if (IsValidNewEmail || bIsValidRetypeEmail)
                            {
                                String OnNotMatchAuthentication = AppResources.InvalidEmail;
                                await _dialogService.ShowMessageBox(OnNotMatchAuthentication, AppResources.Information);
                            }
                            else
                            {
                                String OnNotMatchAuthentication = AppResources.NewEmailandRetypeEmailNotMatch;
                                await _dialogService.ShowMessageBox(OnNotMatchAuthentication, AppResources.Information);

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                        });
                    }
                }
                catch(InternetException ex)
                {
                    await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                }
            });
            OnVerifyButtonClicked = new Command(async () =>
            {
                bool IsNavigatingFromLogin = false;
                NavigateToOtp NavigatingFromMobile = NavigateToOtp.IsMobile;
                String lang = "EN";
                if (App.IsArabic == true)
                    lang = "AR";
                try
                {
                    try
                    {
                        bool response = false;
                        var mobileNumber = "00966" + NewMobile;
                        bool isValidMobileNumber = IsValidMobileNumber(NewMobile);
                        if (isValidMobileNumber)
                        {
                            response = await WebServiceManager.GAZTValidateMobileNumber(lang, TaxPayerProfile.Tin, TaxPayerProfile.Mobile, mobileNumber);

                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
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
                            await _dialogService.ShowMessageBox(OnAuthenticationSuccess + ":" + OnSuccessfulAuthentication, AppResources.Information);
                            ChangeMobileNumberLayoutVisibility = false;
                            TPProfileVisibility = true;
                            ClearMobileData();
                            _navigationService.NavigateTo(App.OTPView, NavigatingFromMobile);
                        }
                    }
                    catch (Exception ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                        });
                    }
                }
                catch(InternetException ex)
                {
                    await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                }
            });
            OnChangeEmailSubmitButtonClicked = new Command(async () =>
            {
                try
                {
                    TaxPayerProfile TP = null;
                    String lang = "EN";
                    if (App.IsArabic == true)
                        lang = "AR";
                    try
                    {
                        if (0 == String.Compare(NewPasswordForEmail, RetypePasswordForEmail, true))
                        {
                            TP = await WebServiceManager.GAZTValidateOTPForEmail(lang, App.Otp, TaxPayerProfile.Tin, OldEmail, NewEmail, CurrentPasswordForEmail, NewPasswordForEmail);
                            if (TaxPayerProfile != null)
                            {
                                CurrentPassword = NewPasswordForEmail;
                                App.TP.Email = NewEmail;
                                TaxPayerProfile.Email = NewEmail;
                                App.TP.Password = NewPasswordForEmail;
                                TaxPayerProfile.Password = NewPasswordForEmail;
                                setPropertyForEmailUpdation(NewEmail);
                                ClearEmailData();
                                String OnAuthenticationSuccess = AppResources.DetailsChangedSuccessfully;
                                await _dialogService.ShowMessageBox(OnAuthenticationSuccess, AppResources.Information);
                                ClearEmailData();
                                App.IsComingFromDashboardToLogOff = false;
                                var _navigation = Application.Current.MainPage.Navigation;
                                await _navigation.PopToRootAsync();
                            }
                            else
                            {

                                String OnInvalidEmail = AppResources.InvalidEmail;
                                await _dialogService.ShowMessageBox(OnInvalidEmail, AppResources.Information);

                            }
                        }
                        else
                        {
                            String OnPasswordMatch = AppResources.NewPasswordandRetypePasswordNotMatch;
                            await _dialogService.ShowMessageBox(OnPasswordMatch, AppResources.Information);
                            ClearPasswordDataForEmail();
                        }
                    }
                    catch (Exception ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            string InvalidOTP = AppResources.InvalidOTP + "(" + AppResources.PleaseReVerify + ")";
                            await _dialogService.ShowMessageBox(InvalidOTP, AppResources.Information);

                            ClearPasswordDataForEmail();
                            ChangeEmailLayoutVisibility = true;
                            ChangeEmailLayoutVisibilityForPassword = false;
                        });
                    }
                }
                catch(InternetException ex)
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                }
            });
            OnChangePasswordButtonClicked = new Command(async () =>
            {
                bool IsNavigatingFromLogin = false;
                String lang = "EN";
                if (App.IsArabic == true)
                    lang = "AR";
                try
                {
                    try
                    {
                        if (0 == String.Compare(NewPassword, RetypePassword, true) && !string.IsNullOrEmpty(NewPassword) && !string.IsNullOrEmpty(RetypePassword))
                        {
                            bool response = await WebServiceManager.GAZTValidateAndChangePassword(lang, TaxPayerProfile.Tin, TaxPayerProfile.Password, NewPassword);
                            if (response == true)
                            {
                                CurrentPassword = NewPassword;
                                App.TP.Password = NewPassword;
                                TaxPayerProfile.Password = NewPassword;
                                String OnAuthenticationSuccess = AppResources.PassWordChangedSucessfully;
                                await _dialogService.ShowMessageBox(OnAuthenticationSuccess, AppResources.Information);
                                App.IsComingFromDashboardToLogOff = false;
                                var _navigation = Application.Current.MainPage.Navigation;
                                ClearPasswordData();
                                await _navigation.PopToRootAsync();
                            }
                            else
                            {
                                String OnInvalidPassword = AppResources.InvalidPassword;
                                await _dialogService.ShowMessageBox(OnInvalidPassword, AppResources.Information);
                            }
                            ChangePasswordayoutVisibility = false;
                            TPProfileVisibility = true;
                        }
                        else
                        {
                            String OnNotMatchAuthentication = AppResources.NewPasswordandRetypePasswordNotMatch;
                            await _dialogService.ShowMessageBox(OnNotMatchAuthentication, AppResources.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                        });
                    }
                }
                catch(InternetException ex)
                {
                    await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);


                }
            });
            OnNewPasswordVisibilityClicked = new Command(async () =>
            {
                if (NewPasswordVisibility)
                {
                    NewPasswordVisibility = false;
                }
                else
                {
                    NewPasswordVisibility = true;
                }
            });

            OnReTypePasswordVisibilityClicked = new Command(async () =>
            {
                if (ReTypePasswordVisibility)
                {
                    ReTypePasswordVisibility = false;
                }
                else
                {
                    ReTypePasswordVisibility = true;
                }
            });
        }
        #endregion
        #region Method
        public void setPropertyForEmailUpdation(string newEmail)
        {
            OldEmail = newEmail;
            NewEmail = string.Empty;
            RetypeEmail = string.Empty;
            NewPasswordForEmail = string.Empty;
            RetypePasswordForEmail = string.Empty;
            IsEnabledNewEmail = true;
            IsEnabledNewPasswordForEmail = false;

        }

        public async void SetTP()
        {
            try
            {
                String lang = "E";
                if (App.IsArabic == true)
                    lang = "A";
                String mobilenumber = await WebServiceManager.GAZTGetTaxPayerProfile(TaxPayerProfile.Tin, lang);
                CurrentMobile = mobilenumber;
                App.TP.Mobile = mobilenumber;
                TaxPayerProfile.Mobile = mobilenumber;
                App.TP.NewMobile = string.Empty;
                TaxPayerProfile.NewMobile = string.Empty;
                CurrentPassword = TaxPayerProfile.Password;
            }
            catch(InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            }
        }
        public void ClearData()
        {
            NewMobile = string.Empty;
            RetypePassword = string.Empty;
            NewPassword = string.Empty;
            NewEmail = string.Empty;
            RetypeEmail = string.Empty;
            CurrentPasswordForEmail = string.Empty;
            NewPasswordForEmail = string.Empty;
            IsEnabledNewEmail = true;
            IscomingFromOTPViewViaEmail = false;
        }
        public void ClearEmailData()
        {
            NewEmail = string.Empty;
            RetypeEmail = string.Empty;
          //  CurrentPasswordForEmail = string.Empty;
            NewPasswordForEmail = string.Empty;
            IsEnabledNewEmail = true;
        }
        public void ClearMobileData()
        {
            NewMobile = string.Empty;

        }
        public void ClearPasswordData()
        {
            RetypePassword = string.Empty;
            NewPassword = string.Empty;
        }
        public void ClearPasswordDataForEmail()
        {
            RetypePasswordForEmail = string.Empty;
            NewPasswordForEmail = string.Empty;
            IsEnabledRetypePasswordForEmail = false;
            IsEnabledNewPasswordForEmail = true;

        }
        public void OnPageLoad()
        {
            TaxPayerProfile = App.TP;
            TPProfileVisibility = true;
            ChangePasswordayoutVisibility = false;
            ChangeMobileNumberLayoutVisibility = false;
            ChangeEmailLayoutVisibility = false;
            CurrentMobile = TaxPayerProfile.Mobile;
            CurrentPassword = TaxPayerProfile.Password;
            OldEmail = TaxPayerProfile.Email;
            CurrentPasswordForEmail = TaxPayerProfile.Password;
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
            if (0 == String.Compare(NewEmailId, RetypeEmailId, true))
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

        #endregion
    }
}
