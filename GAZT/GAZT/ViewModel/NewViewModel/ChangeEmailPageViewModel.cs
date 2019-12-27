using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace GAZT.ViewModel.NewViewModel
{
    public class ChangeEmailPageViewModel: ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public static string emailIdValidation = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
        public ICommand OnVerifyEmailButtonClicked { get; set; }
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

            OnVerifyEmailButtonClicked = new Xamarin.Forms.Command(async () =>
            {
                NavigateToOtp NavigatingFromEmail = NavigateToOtp.IsEmail;
                String lang = "EN";
                if (App.IsArabic == true)
                    lang = "AR";
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

                            
                            IsEnabledRetypeEmail = false;
                            IsEnabledNewEmail = false;
                            App.TP.NewEmail = NewEmail;
                            String OnAuthenticationSuccess = AppResources.Emailverificationcodesentsuccessfully;
                            String OnSuccessfulAuthentication = AppResources.EnterVerificationCodeForEmail;
                            await _dialogService.ShowMessageBox(OnAuthenticationSuccess + ":" + OnSuccessfulAuthentication, AppResources.Information);

                            _navigationService.NavigateTo(App.OTPPageView, NavigatingFromEmail);

                        

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
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                    });
                }
            });

        }

        #endregion

        #region Method
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
            if (0 == String.Compare(NewEmailId, RetypeEmailId, true))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void ClearEmailData()
        {
            NewEmail = string.Empty;
            RetypeEmail = string.Empty;
            //  CurrentPasswordForEmail = string.Empty;
           
            IsEnabledNewEmail = true;
        }
        #endregion
    }
}
