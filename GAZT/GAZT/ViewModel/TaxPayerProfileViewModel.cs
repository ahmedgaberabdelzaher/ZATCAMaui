using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace GAZT
{
    public class TaxPayerProfileViewModel : ViewModelBase
    {
        #region Variable
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        public ICommand OnChangeMobileNumberClicked { get; set; }
        public ICommand OnChangeEmailClicked { get; set; }
        public ICommand OnChangePasswordClicked { get; set; }
        public ICommand OnSubmitButtonClicked { get; set; }
        public ICommand OnVerifyButtonClicked { get; set; }
        public ICommand OnChangeEmailSubmitButtonClicked { get; set; }
        public ICommand OnChangePasswordButtonClicked { get; set; }
        public ICommand OnHomeIconClicked { get; set; }
        public ICommand OnVerifyEmailButtonClicked { get; set; }
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

       


        private string _NewMobile= "00966";
        public string NewMobile
        {
            get
            {
                return _NewMobile;
            }
            set
            {
                _NewMobile = value;

                if(!String.IsNullOrWhiteSpace(_NewMobile) || !String.IsNullOrEmpty(_NewMobile))
                if(_NewMobile.Length ==14)
                    IsVerifyEnabled = true;

                //_NewMobile = value;
                //if(!false == String.IsNullOrEmpty(_NewMobile) || !false == String.IsNullOrWhiteSpace(_NewMobile))
                //if (_NewMobile.Substring(0, 5) == "00966" && _NewMobile.Length == 14)
                //{
                //    IsVerifyEnabled = true;
                //}
                //else 
                //{
                //                    _dialogService.ShowMessage("Please provide mobil number starting with country code 00966 followed by 9 digits", "Validation");

                //                }
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
                if(!string.IsNullOrEmpty(_NewEmail))
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
                //if (_IsEnabledVerifyForEmail == true)
                //{
                //    IsEnabledNewPasswordForEmail = true;
                //    IsEnabledRetypeEmail = false;
                //    IsEnabledNewEmail = false;
                //}
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
                //   _navigationService.NavigateTo(App.UpdateEmailAddress);
                //// _dialogService.ShowMessage("work in progress", "information");

            });

            OnChangePasswordClicked = new Command(() =>
            {
                TPProfileVisibility = false;
                ChangePasswordayoutVisibility = true;
            });

            OnSubmitButtonClicked = new Command(() =>
            {
                //_navigationService.NavigateTo(App.DashboardView);
                ChangeMobileNumberLayoutVisibility = false;
                TPProfileVisibility = true;

            });

            OnHomeIconClicked = new Command(() =>
            {
                _navigationService.GoBack();
            });

            OnVerifyEmailButtonClicked = new Command(async() =>
            {
                NavigateToOtp NavigatingFromEmail = NavigateToOtp.IsEmail;
                String lang = "EN";
                if (App.IsArabic == true)
                    lang = "AR";

                try
                {
                    if (0 == String.Compare(NewEmail, RetypeEmail, true))
                    {
                        bool response = await WebServiceManager.GAZTGetOTPForEmail(lang, TaxPayerProfile.Tin, OldEmail,NewEmail);

                        if (response == true)
                        {

                            IsEnabledNewPasswordForEmail = true;
                            IsEnabledRetypeEmail = false;
                            IsEnabledNewEmail = false;
                            //IsEnabledVerifyForEmail = false;
                            //TaxPayerProfile.NewMobile = NewMobile;
                            App.TP.NewEmail =NewEmail ;

                            String OnAuthenticationSuccess = AppResources.Emailverificationcodesentsuccessfully;

                            String OnSuccessfulAuthentication = AppResources.EnterVerificationCodeForEmail;

                            await _dialogService.ShowMessageBox(OnAuthenticationSuccess + ":" + OnSuccessfulAuthentication, "Information");

                            _navigationService.NavigateTo(App.OTPView, NavigatingFromEmail);



                        }
                    }
                    else
                    {
                        String OnNotMatchAuthentication = AppResources.NewEmailandRetypeEmailNotMatch;

                        await _dialogService.ShowMessageBox(OnNotMatchAuthentication, "Information");
                    }
                }
                catch (Exception ex)
                {

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(ex.Message, "Information");
                    });
                }
            });


            OnVerifyButtonClicked = new Command(async() =>
            {
                bool IsNavigatingFromLogin = false;
                NavigateToOtp NavigatingFromMobile = NavigateToOtp.IsMobile;
                // IsLoading = true;
                // TaxPayerProfile.Mobile = "00966534534645";
                String lang = "EN";
                if (App.IsArabic == true)
                    lang = "AR";
                try
                {
                    bool response = false;
                    var mobileNumber = "00966" + NewMobile;
                   bool isValidMobileNumber =IsValidMobileNumber(NewMobile);
                    if(isValidMobileNumber)
                    {
                         response = await WebServiceManager.GAZTValidateMobileNumber(lang, TaxPayerProfile.Tin, TaxPayerProfile.Mobile, mobileNumber);

                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessageBox(AppResources.EnterValidMobileNumber, "Information");
                        });
                    }

                    if (response == true)
                    {

                        //  TaxPayerProfile.ne = NewMobile;
                       
                        App.TP.NewMobile = mobileNumber;
                        String OnAuthenticationSuccess = AppResources.MobileNumberVerificationSuccessful;

                        String OnSuccessfulAuthentication = AppResources.EnterVerificationCode;

                        await _dialogService.ShowMessageBox(OnAuthenticationSuccess + ":" + OnSuccessfulAuthentication, "Information");
                        ChangeMobileNumberLayoutVisibility = false;
                        TPProfileVisibility = true;
                        _navigationService.NavigateTo(App.OTPView, NavigatingFromMobile);

                    }
                }
                catch (Exception ex)
                {

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(ex.Message, "Information");
                    });
                }

            });



            OnChangeEmailSubmitButtonClicked = new Command(async() =>
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


                            String OnAuthenticationSuccess = AppResources.DetailsChangedSuccessfully;


                            await _dialogService.ShowMessageBox(OnAuthenticationSuccess, "Information");

                            //ChangeEmailLayoutVisibility = false;
                            //TPProfileVisibility = true;
                            //remove all the pages from the stack
                            App.IsComingFromDashboardToLogOff = false;
                            var _navigation = Application.Current.MainPage.Navigation;
                            await _navigation.PopToRootAsync();

                            // _navigationService.NavigateTo(App.LoginView);

                        }
                        else
                        {

                            String OnInvalidEmail = AppResources.InvalidEmail;

                            await _dialogService.ShowMessageBox(OnInvalidEmail, "Information");
                        }

                    }
                    else
                    {
                        String OnPasswordMatch = AppResources.NewPasswordandRetypePasswordNotMatch;

                        await _dialogService.ShowMessageBox(OnPasswordMatch, "Information");
                    }
                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(ex.Message, "Information");
                    });
                }
            });

            OnChangePasswordButtonClicked = new Command(async() =>
            {
                bool IsNavigatingFromLogin = false;

                // IsLoading = true;
               // TaxPayerProfile.Mobile = "00966534534645";
                String lang = "EN";
                if (App.IsArabic == true)
                    lang = "AR";
                try
                {
                    if (0 == String.Compare(NewPassword, RetypePassword, true))
                    {
                        bool response = await WebServiceManager.GAZTValidateAndChangePassword(lang, TaxPayerProfile.Tin, TaxPayerProfile.Password, NewPassword);

                        if (response == true)
                        {

                            // TaxPayerProfile.NewPassword = NewPassword;
                            CurrentPassword = NewPassword;
                            App.TP.Password = NewPassword;
                            TaxPayerProfile.Password = NewPassword;
                            NewPassword = string.Empty;
                            RetypePassword = string.Empty;

                            String OnAuthenticationSuccess = AppResources.PassWordChangedSucessfully;


                            await _dialogService.ShowMessageBox(OnAuthenticationSuccess, "Information");
                            //remove all the pages from the stack
                            App.IsComingFromDashboardToLogOff = false;
                            var _navigation = Application.Current.MainPage.Navigation;
                            await _navigation.PopToRootAsync();
                            //_navigationService.NavigateTo(App.LoginView);
                        }
                        else
                        {
                            String OnInvalidPassword = AppResources.InvalidPassword;

                            await _dialogService.ShowMessageBox(OnInvalidPassword, "Information");
                        }




                        ChangePasswordayoutVisibility = false;
                        TPProfileVisibility = true;
                    }
                    else
                    {
                        String OnNotMatchAuthentication = AppResources.NewPasswordandRetypePasswordNotMatch;

                        await _dialogService.ShowMessageBox(OnNotMatchAuthentication, "Information");
                    }
                }
                catch (Exception ex)
                {

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(ex.Message, "Information");
                    });
                }

                //bool IsNavigatingFromLogin = false;
                //_navigationService.NavigateTo(App.OTPView, IsNavigatingFromLogin);

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

        public void ClearData()
        {
            NewMobile = string.Empty;
            RetypePassword = string.Empty;
            NewPassword = string.Empty;
        }

        public void OnPageLoad()
        {
            TPProfileVisibility = true;
            ChangePasswordayoutVisibility = false;
            ChangeMobileNumberLayoutVisibility = false;
            ChangeEmailLayoutVisibility = false;
            CurrentMobile = TaxPayerProfile.Mobile;
            CurrentPassword = TaxPayerProfile.Password;
            OldEmail = TaxPayerProfile.Email;
            CurrentPasswordForEmail = TaxPayerProfile.Password;
        }

        public  bool IsValidMobileNumber(string mobileNumber)
        {
            if (mobileNumber.Substring(0, 1).Equals("5") && mobileNumber.Length == 9)
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
