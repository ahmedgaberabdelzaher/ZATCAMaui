using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using GAZT.Views.NewViews;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GAZT
{
   public class ChangePasswordPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        public ICommand OnChangeEmailSubmitButtonClicked { get; set; }
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


        private NavigateToOtp _NavigateToOtpForEmailEnum;
        public NavigateToOtp NavigateToOtpForEmailEnum
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

            OnChangeEmailSubmitButtonClicked = new Xamarin.Forms.Command(async () =>
            {
               await ChangePassword();
            });


        }

        #endregion

        #region Method

        private async Task ChangePassword()
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

                    if (NavigateToOtpForEmailEnum == NavigateToOtp.IsEmail)
                    {
                        if (0 == String.Compare(NewPasswordForEmail, RetypePasswordForEmail, true))
                        {
                            TP = await WebServiceManager.GAZTValidateOTPForEmail(lang, App.Otp, TaxPayerProfile.Tin, OldEmail, NewEmail, TaxPayerProfile.Password, NewPasswordForEmail);
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
                                Device.BeginInvokeOnMainThread(async () => {
                                    await _dialogService.ShowMessageBox(OnAuthenticationSuccess, AppResources.Information);
                                });
                                ClearEmailData();
                                App.IsComingFromDashboardToLogOff = false;
                                Device.BeginInvokeOnMainThread(async () => {
                                    var _navigation = Application.Current.MainPage.Navigation;
                                    await _navigation.PopToRootAsync();
                                });
                                
                            }
                            else
                            {

                                String OnInvalidEmail = AppResources.InvalidEmail;
                                Device.BeginInvokeOnMainThread(async () => {
                                    await _dialogService.ShowMessageBox(OnInvalidEmail, AppResources.Information);
                                });

                            }
                        }
                        else
                        {
                            String OnPasswordMatch = AppResources.NewPasswordandRetypePasswordNotMatch;
                            Device.BeginInvokeOnMainThread(async () => {
                                await _dialogService.ShowMessageBox(OnPasswordMatch, AppResources.Information);
                            });
                            ClearPasswordDataForEmail();
                        }
                    }
                    else if (NavigateToOtpForEmailEnum == NavigateToOtp.IsLogin)//For Default Password Change
                    {

                        try
                        {
                            if (0 == String.Compare(NewPasswordForEmail, RetypePasswordForEmail, true) && !string.IsNullOrEmpty(RetypePasswordForEmail) && !string.IsNullOrEmpty(RetypePasswordForEmail))
                            {
                                bool response = await WebServiceManager.GAZTValidateAndChangePassword(lang, TaxPayerProfile.Tin, TaxPayerProfile.Password, NewPasswordForEmail);
                                if (response == true)
                                {
                                    CurrentPassword = NewPasswordForEmail;
                                    App.TP.Password = NewPasswordForEmail;
                                    TaxPayerProfile.Password = NewPasswordForEmail;
                                    String OnAuthenticationSuccess = AppResources.PassWordChangedSucessfully;
                                    Device.BeginInvokeOnMainThread(async () => {
                                        await _dialogService.ShowMessageBox(OnAuthenticationSuccess, AppResources.Information);
                                    });
                                  
                                    App.IsComingFromDashboardToLogOff = false;
                                    var _navigation = Application.Current.MainPage.Navigation;
                                    
                                    Device.BeginInvokeOnMainThread(async () => {
                                        ClearPasswordData();
                                        await _navigation.PopToRootAsync();
                                    });
                                }
                                else
                                {
                                    String OnInvalidPassword = AppResources.InvalidPassword;
                                    Device.BeginInvokeOnMainThread(async () => {
                                        await _dialogService.ShowMessageBox(OnInvalidPassword, AppResources.Information);
                                    });
                                }

                            }
                            else
                            {
                                String OnNotMatchAuthentication = AppResources.NewPasswordandRetypePasswordNotMatch;
                                Device.BeginInvokeOnMainThread(async () => {
                                    await _dialogService.ShowMessageBox(OnNotMatchAuthentication, AppResources.Information);
                                });
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
                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        string InvalidOTP = AppResources.InvalidOTP + "(" + AppResources.PleaseReVerify + ")";
                        await _dialogService.ShowMessageBox(InvalidOTP, AppResources.Information);

                        ClearPasswordDataForEmail();
                        if (NavigateToOtpForEmailEnum == NavigateToOtp.IsEmail)
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
        public void ClearPasswordData()
        {
            NewPasswordForEmail = string.Empty;
            RetypePasswordForEmail = string.Empty;
        }
        public void OnPageLoad()
        {
            TaxPayerProfile = App.TP;
            CurrentPassword = TaxPayerProfile.Password;
            OldEmail = TaxPayerProfile.Email;
            NewEmail = TaxPayerProfile.NewEmail;
            IsEnabledNewPasswordForEmail = true;
            IsEnabledRetypePasswordForEmail = true;
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
            
        }

        public void ClearPasswordDataForEmail()
        {
            RetypePasswordForEmail = string.Empty;
            NewPasswordForEmail = string.Empty;
            IsEnabledRetypePasswordForEmail = false;
            IsEnabledNewPasswordForEmail = true;

        }
        #endregion
    }
    }
