using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GAZT
{
    public class LogInViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnLoginButtonClicked { get; set; }
        public ICommand OnOnLanguageClickClicked { get; set; }

        #endregion

        #region Property

        private string _UserName = "3300087028";
      //  private string _UserName = string.Empty;
        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;
                RaisePropertyChanged("UserName");
            }
        }

        private string _Password = "Test@123";
       // private string _Password = string.Empty;
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
                RaisePropertyChanged("Password");
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
        
        #endregion

        #region Constructor

        public LogInViewModel(INavigationService navigationService, IDialogService dialogService)
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


            OnLoginButtonClicked = new Command(async () =>
            {
                App.IsComingFromDashboardToLogOff = false;
                await Task.Run(() =>
                {
                    IsLoading = true;
                });


                //App.IsArabic = true;

                //if (App.IsArabic)
                //{
                //    SetRTLDirectionTest();
                //}
                //else
                //{
                //    SetLTRDirectionTest();
                //}

                //String OnAuthenticationSuccess = AppResources.LoginSuccessful;// ResourceManager.GetString("LoginSuccessful");
                //String OnSuccessfulAuthentication = AppResources.EnterVerificationCode;

                // await _dialogService.ShowMessageBox(OnAuthenticationSuccess + ":" + OnSuccessfulAuthentication, AppResources.Information);

                await Task.Run(async () =>
                {
                    String lang = "EN";
                    if (App.IsArabic == true)
                        lang = "AR";

                    String response = WebServiceManager.GAZTAuthenticateTIN(UserName, Password);
                    if (0 == String.Compare("success", response, true))
                    {

                        try
                        {
                            String MobileNumber = await WebServiceManager.GAZTGetTaxPayerProfile(UserName, lang);
                            if ( false == String.IsNullOrEmpty(MobileNumber))
                            {
                                if(App.TP ==null)
                                {
                                    App.TP = new Models.TaxPayerProfile();
                                    App.TP.Mobile = MobileNumber;
                                }                                    
                            }
                        }
                        catch (Exception ex)
                        {

                        }

                        String OnAuthenticationSuccessMsg = AppResources.LoginSuccessful;
                        String OnSuccessfulAuthenticationqMsg = AppResources.EnterVerificationCode;

                        //Device.BeginInvokeOnMainThread(async () =>
                        //{
                        //    IsLoading = false;
                        //    await _dialogService.ShowMessageBox(OnAuthenticationSuccessMsg + ":" + OnSuccessfulAuthenticationqMsg, AppResources.Information);
                        //});

                        await Task.Run(async () =>
                        {
                            response = await WebServiceManager.GAZTSendAndReceiveOTP(lang, UserName);
                            if (0 == String.Compare("OTP has send", response, true) || 0 == String.Compare("كلمة مرور مرة واحدة قد أرسلت", response, true))
                            {
                                if(App.TP ==null)
                                    App.TP = new Models.TaxPayerProfile();

                                App.TP.Userid = UserName;
                                App.TP.Password = Password;
                                bool IsNavigatingFromLogin = true;
                                NavigateToOtp NavigatingFromLogin = NavigateToOtp.IsLogin;

                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    _navigationService.NavigateTo(App.OTPView, NavigatingFromLogin);
                                });
                            }
                            else
                            {
                                await Task.Run(() =>
                                {
                                    IsLoading = false;
                                });
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await _dialogService.ShowMessageBox(response, AppResources.Information);
                                });
                            }
                        });


                    }
                    else
                    {

                        if (App.IsArabic)
                        {
                            string tin = UserName;
                            tin = tin + " - " + "User does not exist";
                            if (response.Equals("User authentication failed"))
                            {
                                response = AppResources.UserAuthenticationFailed;
                            }
                            else if (response.Equals(tin))
                            {
                                response = AppResources.UserDoesNotExist;
                            }
                            else
                            {
                                response = AppResources.UserAccountLocked;
                            }
                        }

                        await Task.Run(() =>
                        {
                            IsLoading = false;
                        });
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessageBox(response, AppResources.Information);
                        });
                    }



                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });

                });
            });
        }

        #endregion

        #region Method


        #endregion
    }
}
