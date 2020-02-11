using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZT.Views.NewViews;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GAZT.ViewModel
{
  public  class LogInPageViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnLoginButtonClicked { get; set; }
        public ICommand OnOnLanguageClickClicked { get; set; }
        public ICommand OnForgotUsernamePasswordClicked { get; set; }
        public ICommand OnNewLoginUIClicked { get; set; }
        public ICommand OnNewDashboardUIClicked { get; set; }
        public string DeviceId { get; set; }
        public int CurrentAttempt = 0;
        private DateTime lastTapped;
        #endregion
        #region Property

       // private string _UserName = "3102226642";
        //private string _UserName = "3102226709";

       // private string _UserName = "3100278126";
      //  private string _UserName = string.Empty;
        // private string _UserName = "3101965624";
       private string _UserName = "3100018168";
        //private string _UserName = string.Empty;
        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                PreviousUserName = UserName;
                _UserName = value;
                if (PreviousUserName != _UserName)
                {
                    IsVisibleTinIds = false;
                }
                if (string.IsNullOrEmpty(_UserName))
                {
                    IsLoginEnabled = false;
                    Password = string.Empty;
                    IsVisibleTinIds = false;
                }
                if (!string.IsNullOrEmpty(_UserName) && !string.IsNullOrEmpty(Password))
                {
                    IsLoginEnabled = true;
                }
                RaisePropertyChanged("UserName");
            }
        }

        private string _PreviousUserName = String.Empty;
        public string PreviousUserName
        {
            get
            {
                return _PreviousUserName;
            }
            set
            {
                _PreviousUserName = value;
            }
        }


       private string _Password = "Init@1234";

      //private string _Password = string.Empty;
      //private string _Password = string.Empty;
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
                if (!string.IsNullOrEmpty(_Password) && !string.IsNullOrEmpty(UserName))
                {
                    IsLoginEnabled = true;
                }
                else
                {
                    IsLoginEnabled = false;
                }
                RaisePropertyChanged("Password");
            }
        }




        private bool _IsFocused = false;
        public bool IsFocused
        {
            get
            {
                return _IsFocused;
            }
            set
            {
                _IsFocused = value;
                if (_IsFocused == true)
                {
                    bool Test = UtilityManager.IsValidEmailAddress(UserName);
                    if (Test == true)
                    {
                        if (IsVisibleTinIds == false)
                        {
                            IsVisibleTinIds = true;
                        }
                    }
                    else
                    {
                        IsVisibleTinIds = false;
                    }
                }
                RaisePropertyChanged("IsFocused");
            }
        }


        private List<TIN> _tINs;
        public List<TIN> TINs
        {
            get
            {
                return _tINs;
            }
            set
            {
                _tINs = value;
                RaisePropertyChanged("TINs");
            }
        }


        private TIN _selectedTinId;
        public TIN SelectedTinId
        {
            get
            {
                return _selectedTinId;
            }
            set
            {
                _selectedTinId = value;
                if (_selectedTinId != null)
                {
                    App.CurrentDropdownTIN = SelectedTinId;
                    Password = string.Empty;
                }
                RaisePropertyChanged("SelectedTinId");
            }
        }


        private bool _isLoginEnabled = false;
        public bool IsLoginEnabled
        {
            get
            {
                return _isLoginEnabled;
            }
            set
            {
                _isLoginEnabled = value;
                RaisePropertyChanged("IsLoginEnabled");
            }
        }

        private bool _isVisibleTinIds = false;
        public bool IsVisibleTinIds
        {
            get
            {
                return _isVisibleTinIds;
            }
            set
            {
                _isVisibleTinIds = value;
                if (_isVisibleTinIds == true)
                {


                    TINs = new List<TIN>();

                    Task.Run(async () =>
                    {
                        try
                        {
                            try
                            {
                                await Task.Run(() =>
                                {
                                    IsLoading = true;
                                });

                                TINs = await WebServiceManager.GAZTGetAllTins(UserName);
                                if ((TINs != null) && (TINs.Count != 0) && (SelectedTinId == null))
                                {
                                    SelectedTinId = TINs[0];
                                }
                                else
                                {
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        IsVisibleTinIds = false;
                                        await _dialogService.ShowMessageBox(AppResources.NoTINsAvailable, AppResources.Information);
                                    });
                                    IsVisibleTinIds = false;
                                }
                                await Task.Run(() =>
                                {
                                    IsLoading = false;
                                });
                            }
                            catch (Exception e)
                            {
                                IsVisibleTinIds = false;
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    IsVisibleTinIds = false;
                                    await _dialogService.ShowMessageBox(AppResources.NetworkConnectivityIssue, AppResources.Information);
                                });

                                await Task.Run(() =>
                                {
                                    IsLoading = false;
                                });
                            }
                        }
                        catch(InternetException ex)
                        {
                            await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                            await Task.Run(() =>
                            {
                                IsLoading = false;
                            });
                        }
                    });
                }
                RaisePropertyChanged("IsVisibleTinIds");
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

        private bool _passwordVisibility = true;
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

        public LogInPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            List<TIN> tinIds = new List<TIN>();
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

            OnForgotUsernamePasswordClicked = new Command(() =>
            {
                _navigationService.NavigateTo(App.ForgotUsernamePasswordPageView);
            });
            OnNewLoginUIClicked = new Command(async () =>
            {
                _navigationService.NavigateTo(App.LogInPageView);
            });
            OnNewDashboardUIClicked = new Command(async () =>
            {
                _navigationService.NavigateTo(App.DashboardPageView);
            });

            OnLoginButtonClicked = new Command(async () =>
            {
                if (lastTapped<DateTime.Now.AddSeconds(-4))
                {
                    try
                    {
                        
                         await Login();


                    }
                    catch(InternetException ex)
                    {
                        await _dialogService.ShowMessageBox(AppResources.ZZInternetConnectionMessage, AppResources.Alerts);
                    }
                }
              
               
            });
        }
        #endregion
        #region Method

        private async Task Login()
        {
            CurrentAttempt++;
            App.IsComingFromDashboardToLogOff = false;
            String response = string.Empty;
            string UserId = string.Empty;



            await Task.Run(() =>
            {
                IsLoading = true;
            });
            await Task.Run(async () =>
            {
                try
                {
                    try
                    {
                        string language = UtilityManager.GetLanguageParameter();
                        String lang = "E";
                        if (App.IsArabic == true)
                            lang = "AR";
                        if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
                        {
                            throw new Exception(AppResources.Pleaseenteryourlogininformation);
                        }
                        string _currentAttempts = CurrentAttempt.ToString();
                        string languag = UtilityManager.GetLanguageParameter();
                        if (SelectedTinId != null && IsVisibleTinIds == true)
                        {
                            bool isValidEmail = UtilityManager.IsValidEmailAddress(UserName);
                            if (isValidEmail == true)
                            {

                                response = WebServiceManager.GAZTAuthenticateTIN(SelectedTinId.Tin, Password, DeviceId, _currentAttempts, languag);
                                UserId = SelectedTinId.Tin;
                            }
                            else
                            {
                                throw new Exception(AppResources.ZUserNameIncorrect);
                            }
                        }
                        else
                        {
                            bool isValidTin = UtilityManager.IsOTPNumberValid(UserName);
                            if (isValidTin == true)

                            {
                                response = WebServiceManager.GAZTAuthenticateTIN(UserName, Password, DeviceId, _currentAttempts, languag);
                                UserId = UserName;
                            }
                            else
                            {
                                throw new Exception(AppResources.ZUserNameIncorrect);
                            }
                        }



                        if (0 == String.Compare("success", response, true))
                        {

                            String MobileNumber = await WebServiceManager.GAZTGetTaxPayerProfile(UserId, lang);
                            if (false == String.IsNullOrEmpty(MobileNumber))
                            {
                                if (App.TP == null)
                                {
                                    App.TP = new Models.TaxPayerProfile();
                                    App.TP.Mobile = MobileNumber;
                                }
                            }

                            String OnAuthenticationSuccessMsg = AppResources.LoginSuccessful;
                            String OnSuccessfulAuthenticationqMsg = AppResources.EnterVerificationCode;
                            await Task.Run(async () =>
                            {
                                string currentAttempts = "1";
                                response = await WebServiceManager.GAZTSendAndReceiveOTP(lang, UserId, currentAttempts);
                                if (0 == String.Compare("OTP has send", response, true) || 0 == String.Compare("كلمة مرور مرة واحدة قد أرسلت", response, true))
                                {
                                    if (App.TP == null)
                                        App.TP = new Models.TaxPayerProfile();

                                    App.TP.Userid = UserId;
                                    App.TP.Password = Password;
                                    bool IsNavigatingFromLogin = true;
                                    NavigateToOtp NavigatingFromLogin = NavigateToOtp.IsLogin;

                                    Device.BeginInvokeOnMainThread(() =>
                                    {
                                        _navigationService.NavigateTo(App.OTPPageView, NavigatingFromLogin);
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
                                else if (0 == String.Compare("Authentication failed. Password locked", response, true))
                                {
                                    response = AppResources.UserAccountLocked;
                                }
                                else if (0 == String.Compare("Error: NameResolutionFailure", response, true))
                                {
                                    response = AppResources.NetworkConnectivityIssue;
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

                            if (0 == String.Compare("Error: NameResolutionFailure", response, true))
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await _dialogService.ShowMessageBox(AppResources.NetworkConnectivityIssue, AppResources.Information);
                                });
                            }
                            else
                            {

                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await _dialogService.ShowMessageBox(response, AppResources.Information);
                                });
                            }
                        }



                        await Task.Run(() =>
                        {
                            IsLoading = false;
                        });
                    }
                    catch (Exception ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                        //UserName = string.Empty;
                        //Password = string.Empty;
                    });
                        await Task.Run(() =>
                        {
                            IsLoading = false;
                        });

                    }
                }
                catch(InternetException ex)
                {
                    await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });
                }
            });
        }
        public async Task PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () => {
                    var _navigation = Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });
            }
        }

        #endregion

    }
}
