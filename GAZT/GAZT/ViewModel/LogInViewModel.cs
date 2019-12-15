using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Collections.Generic;
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
         private string _UserName = "3102183402";
       // private string _UserName = string.Empty;
        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;
                if(string.IsNullOrEmpty(_UserName))
                {
                    IsLoginEnabled = false;
                }
                RaisePropertyChanged("UserName");
            }
        }

        private string _Password = "Test@123";
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
                if(!string.IsNullOrEmpty(_Password) && !string.IsNullOrEmpty(UserName))
                {
                      IsLoginEnabled = true;
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
                if(_IsFocused==true)
                {
                    bool Test = UtilityManager.IsValidEmailAddress(UserName);
                    if(Test==true)
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


        private List<TinIds> _tinIds;
        public List<TinIds> TinIds
        {
            get
            {
                return _tinIds;
            }
            set
            {
                _tinIds = value;
                RaisePropertyChanged("TinIds");
            }
        }


        private TinIds _selectedTinId;
        public TinIds SelectedTinId
        {
            get
            {
                return _selectedTinId;
            }
            set
            {
                _selectedTinId = value;
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
                if(_isVisibleTinIds==true)
                {


                    TinIds = new List<TinIds>();

                    Task.Run(async () =>
                    {
                        await Task.Run(() =>
                        {
                            IsLoading = true;
                        });

                        TinIds = await WebServiceManager.GAZTGetAllTins(UserName);
                        if (TinIds.Count != 0 && SelectedTinId==null)
                        {
                            SelectedTinId = TinIds[0];
                        }
                        await Task.Run(() =>
                        {
                            IsLoading = false;
                        });
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
            List<TinIds> tinIds = new List<TinIds>();
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
                // _navigationService.NavigateTo(App.ForgotUsernamePassword);
                App.IsComingFromDashboardToLogOff = false;
                String response = string.Empty;

                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    String lang = "E";
                    if (App.IsArabic == true)
                        lang = "AR";


                    //  bool isValidEmail= UtilityManager.IsValidEmailAddress(UserName);
                    if (SelectedTinId != null && IsVisibleTinIds == true)
                    {
                        response = WebServiceManager.GAZTAuthenticateTIN(SelectedTinId.Tin, Password);
                    }
                    else
                    {
                        response = WebServiceManager.GAZTAuthenticateTIN(UserName, Password);
                    }

                        
                        if (0 == String.Compare("success", response, true))
                        {
                            try
                            {
                                String MobileNumber = await WebServiceManager.GAZTGetTaxPayerProfile(UserName, lang);
                                if (false == String.IsNullOrEmpty(MobileNumber))
                                {
                                    if (App.TP == null)
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
                            await Task.Run(async () =>
                            {
                                response = await WebServiceManager.GAZTSendAndReceiveOTP(lang, UserName);
                                if (0 == String.Compare("OTP has send", response, true) || 0 == String.Compare("كلمة مرور مرة واحدة قد أرسلت", response, true))
                                {
                                    if (App.TP == null)
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
