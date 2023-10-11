using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.SFLogin_ViewModel;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using GAZT.Helper;
using AppDynamics.Agent;
using EGAZT.Helper;
using Xamarin.Essentials;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.SFLoginPage_ViewModel
{
    /// <summary>
    /// ViewModel for login page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SFLoginPageViewModel : SFLoginViewModel
    {

        #region Fields
        //public string password = "Test@123";
        //private string email = "3102285896";

        //private string email = "3101593128";
        //private string password = "TeUserAccountLockedst@123";
        //private string email = "3102290567";
        //private string email = "3102289204";
        // public string email = "3102292043";
        public string password;
        public string email;
        public int CurrentAttempt = 0;
        public ICommand BackButtonClicked { get; set; }
        public ICommand GoBackClick { get; set; }


        #endregion
        #region ConstructorF
        /// <summary>
        /// Initializes a new instance for the <see cref="LoginPageViewModel" /> class.
        /// </summary>
        public SFLoginPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            try
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    VersionTracking.Track();
                    await DependencyService.Get<IForceUpdate>().FetchAndActivateAsync();
                    var hasForceUpdateResult = bool.Parse(DependencyService.Get<IForceUpdate>().GetValue("IsForceUpdate"));

                    switch (Device.RuntimePlatform)
                    {
                        case Device.Android:

                            var currentAndroidBuild = int.Parse(VersionTracking.CurrentBuild);
                            var firebaseAndroidBuild = int.Parse(DependencyService.Get<IForceUpdate>().GetValue("BuildNumber_Android"));

                            if (hasForceUpdateResult && currentAndroidBuild < firebaseAndroidBuild)
                            {
                                await App.Current.MainPage.DisplayAlert(AppResources.TPUpdate, AppResources.ForceUpdateMsg, AppResources.OKText);
                                await Launcher.OpenAsync(new Uri("https://play.google.com/store/apps/details?id=com.gazt.egazt"));
                                System.Diagnostics.Process.GetCurrentProcess().Kill();
                            }
                            break;


                        case Device.iOS:
                            var currentiOSBuild = VersionTracking.CurrentBuild;
                            var currentiOSBuildInt = Array.ConvertAll(currentiOSBuild.Split('.'), Int32.Parse);

                            var firebaseiOSBuild = DependencyService.Get<IForceUpdate>().GetValue("BuildNumber_iOS");
                            var firebaseiOSBuildInt = Array.ConvertAll(firebaseiOSBuild.Split('.'), Int32.Parse);

                            if (currentiOSBuild[0] > firebaseiOSBuild[0])
                                return;

                            else if (hasForceUpdateResult && (currentiOSBuildInt[0] < firebaseiOSBuildInt[0] ||
                                         currentiOSBuildInt[1] < firebaseiOSBuildInt[1] ||
                                         currentiOSBuildInt[2] < firebaseiOSBuildInt[2]))
                            {
                                await App.Current.MainPage.DisplayAlert(AppResources.TPUpdate, AppResources.ForceUpdateMsg, AppResources.OKText);
                                await Launcher.OpenAsync(new Uri("https://apps.apple.com/sa/app/zatca/id1517289036"));
                                System.Diagnostics.Process.GetCurrentProcess().Kill();
                            }
                              

                            break;

                        default:
                            break;
                    }

                });

            }
            catch (Exception ex)
            {

            }


            if (App.IsSAMLLoginEnabled == true)
            {
                IsSAMLLoginEnabled = true;
                IsOldLoginHidden = false;
            }
            else
            {
                IsSAMLLoginEnabled = false;
                IsOldLoginHidden = true;
            }

            this.BackButtonClicked = new Command(this.BackButtonClick);
            this.SignUpCommand = new Command(this.SignUpClicked);
            this.ForgotPasswordCommand = new Command(this.ForgotPasswordClicked);
            this.SocialMediaLoginCommand = new Command(this.SocialLoggedIn);
            this.HamburgerMenuClickedCommand = new Command(this.HamburgerMenuClicked);

            GoBackClick = new Command(() =>
            {
                _navigationService.GoBack();
            });


        }
        #endregion
        #region property
        private string _appVersion = App.AppVersion;
        public string AppVersion
        {
            get
            {
                return _appVersion;
            }
            set
            {
                if (_appVersion == value) return;

                _appVersion = value;
                RaisePropertyChanged("AppVersion");
            }
        }
        public string DeviceId { get; set; }
        /// <summary>
        /// Gets or sets the property that is bound with an entry that gets the password from user in the login page.
        /// </summary>
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                if (this.password == value)
                {
                    return;
                }

                this.password = value;
                this.RaisePropertyChanged("Password");
            }
        }
        public string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                //if (this.email == value)
                //{
                //    return;
                //}
                PreviousUserName = Email;
                this.email = value;
                if (PreviousUserName != this.email)
                {
                    IsVisibleTinIds = false;
                }
                if (string.IsNullOrEmpty(this.email))
                {
                    IsLoginEnabled = false;
                    Password = string.Empty;
                    IsVisibleTinIds = false;
                }
                if (!string.IsNullOrEmpty(this.email) && !string.IsNullOrEmpty(Password))
                {
                    IsLoginEnabled = true;
                }
                RaisePropertyChanged("Email");
            }
        }
        private bool _IsSAMLLoginEnabled;
        public bool IsSAMLLoginEnabled
        {
            get
            {
                return _IsSAMLLoginEnabled;
            }
            set
            {
                if (_IsSAMLLoginEnabled == value) return;
                RaisePropertyChanged("IsSAMLLoginEnabled");
            }
        }
        private bool _IsOldLoginHidden;
        public bool IsOldLoginHidden
        {
            get
            {
                return _IsOldLoginHidden;
            }
            set
            {
                if (_IsOldLoginHidden == value) return;

                RaisePropertyChanged("IsOldLoginHidden");
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
                if (_IsFocused == value) return;

                _IsFocused = value;
                if (_IsFocused == true)
                {
                    if (!string.IsNullOrEmpty(email))
                    {
                        bool Test = UtilityManager.IsValidEmailAddress(email);
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
                }
                RaisePropertyChanged("IsFocused");
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
                if (_PreviousUserName == value) return;

                _PreviousUserName = value;
            }

        }
        private string _tINID = string.Empty;
        public string TINID
        {
            get
            {
                return _tINID;
            }
            set
            {
                if (_tINID == value) return;

                _tINID = value;
                RaisePropertyChanged("TINID");
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
                if (_isLoading == value) return;

                _isLoading = value;
                this.RaisePropertyChanged("IsLoading");
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
                if (_isLoginEnabled == value) return;

                _isLoginEnabled = value;
                RaisePropertyChanged("IsLoginEnabled");
            }
        }
        private int _tINIndex = 0;
        public int TINIndex
        {
            get
            {
                return _tINIndex;
            }
            set
            {
                if (_tINIndex == value) return;

                _tINIndex = value;
                RaisePropertyChanged("TINIndex");
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
                if (_tINs == value) return;

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
                if (_selectedTinId == value) return;

                _selectedTinId = value;
                if (_selectedTinId != null)
                {
                    App.CurrentDropdownTIN = SelectedTinId;
                    TINID = _selectedTinId.Tin;
                    Password = string.Empty;
                }
                RaisePropertyChanged("SelectedTinId");
            }
        }
        private TIN _selectedTinIdPrev;
        public TIN SelectedTinIdPrev
        {
            get
            {
                return _selectedTinIdPrev;
            }
            set
            {
                if (_selectedTinIdPrev == value) return;

                _selectedTinIdPrev = value;
                RaisePropertyChanged("SelectedTinIdPrev");
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
                if (_passwordVisibility == value) return;

                _passwordVisibility = value;
                RaisePropertyChanged("PasswordVisibility");
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
                if (_isVisibleTinIds == value) return;

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
                                TINs = WebServiceManager.SFGAZTGetAllTINs(Email);
                                if ((TINs != null) && (TINs.Count != 0))
                                {
                                    if (SelectedTinId == null)
                                    {
                                        SelectedTinId = TINs[0];
                                    }
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
                        catch (GAZTException gex)
                        {
                            IsLoading = false;
                            string MessageForTheUser = gex.Message;
                            if (gex is GAZTNetworkConnectivityIssueException)
                            {
                                MessageForTheUser = AppResources.NetworkConnectivityIssue;
                            }
                            else if (gex is GAZTInternetException)
                            {
                                MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                            }
                            else if (gex is GAZTException)
                            {
                                MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            }
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                            });
                        }
                    });
                }
                RaisePropertyChanged("IsVisibleTinIds");
            }
        }
        private string _NavigaateToThisService = String.Empty;
        public string NavigateToThisService
        {
            get
            {
                return _NavigaateToThisService;
            }
            set
            {

                if (_NavigaateToThisService == value) return;

                _NavigaateToThisService = value;
            }
        }
        #endregion
        #region Command
        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }
        /// <summary>
        /// Gets or sets the command that is executed when the Forgot Password button is clicked.
        /// </summary>
        public Command ForgotPasswordCommand { get; set; }
        /// <summary>
        /// Gets or sets the command that is executed when the social media login button is clicked.
        /// </summary>
        public Command SocialMediaLoginCommand { get; set; }
        /// <summary>
        /// Gets or sets the command that is executed when the Hamburger menu button is clicked.
        /// </summary>
        public ICommand HamburgerMenuClickedCommand { get; set; }

        #endregion
        #region methods
        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SignUpClicked(object obj)
        {
            _navigationService.NavigateTo(App.SignUpTAndCViewPage);
            // Do something
        }
        /// <summary>
        /// Invoked when the Forgot Password button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void ForgotPasswordClicked(object obj)
        {
            var label = obj as Label;
            label.BackgroundColor = (Color)Application.Current.Resources["FPButtonTextColor"];
            await Task.Delay(100);
            label.BackgroundColor = Color.Transparent;
            _navigationService.NavigateTo(App.GAZTNewDesignForgotPasswordPageView);
        }
        /// <summary>
        /// Invoked when social media login button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SocialLoggedIn(object obj)
        {
            // Do something
        }
        public void BackButtonClick()
        {
            _navigationService.GoBack();
        }

        private void HamburgerMenuClicked()
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFLoginPageView", "HamburgerMenuClicked", "Anonymous Menu Opened");
            _navigationService.NavigateTo(App.DashboardAnonymousMenuPageView);
            //_navigationService.NavigateTo(App.TaxEvasionVerifyMobileNumberPage);
            //_dialogService.ShowMessage("Anonymous menu will appear", "Menu");
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }
        #endregion
        #region New Authentication

        public string CreateLoginURL(string lang)
        {
            try
            {
                string deviceOs = Xamarin.Essentials.DeviceInfo.Platform.ToString();
                string deviceUdid = DependencyService.Get<IDeviceInfo>().GetDeviceUdid();
                return WebServiceManager.CreateSAMLLoginURL("", deviceUdid, "", deviceOs, lang);

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task LoginCompletedInWebView()
        {
            string response = string.Empty;
            string UserId = App.LoginDataRetrieved.TIN;

            Instrumentation.SetUserData("user_id", UserId);

            //string lang = "E";
            string language = UtilityManager.GetLanguageParameter();

            string _currentAttempts = CurrentAttempt.ToString();
            string languag = UtilityManager.GetLanguageParameter();

            // * OLD TP PROFILE API
            //TaxPayerProfile TPProfile = WebServiceManager.SFGAZTGetTaxPayerProfile(UserId, lang);

            // * NEW TP PROFILE API
            TaxPayerProfile TPProfile = await WebServiceManager.GetTPProfileDataAPICall(UserId);

            if (TPProfile != null)
            {
               

                App.TP = new TaxPayerProfile();
                App.TP = TPProfile;
                App.TP.Userid = TPProfile.Tin;
                try
                {
                    if (App.LoginDataRetrieved != null)
                    {
                        if (App.TP != null)
                        {
                            App.TP.NameFirst = App.LoginDataRetrieved.NameFirst;
                            App.TP.NameLast = App.LoginDataRetrieved.NameLast;
                            App.TP.NameOrg1 = App.LoginDataRetrieved.NameOrg1;
                            App.TP.TypeChk = App.LoginDataRetrieved.TypeChk;
                        }

                    }

                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());
                }

            }

            String OnAuthenticationSuccessMsg = AppResources.LoginSuccessful;
            String OnSuccessfulAuthenticationqMsg = AppResources.EnterVerificationCode;

            await Task.Run(() =>
            {
                try
                {

                    App.IsLoginCalled = false;
                    App.ArePreLoginLangCookiesSet = false;

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _navigationService.NavigateTo(App.GAZTNewDesignDashBoardPageView, false);
                        App.HasToRefreshLoaderOnDashboard = true;
                    });

                  
                }
                catch (GAZTInternetException)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                    });


                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(AppResources.ZZSomethingwentwrong + " " + AppResources.ZZInternetConnectionMessage, AppResources.Information);
                    });
                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());
                }

            });

        }

        public async Task LoginCompletedInWebViewForVATRegistrationTestPurpose()
        {
            string response = string.Empty;
            string UserId = App.LoginDataRetrieved.TIN;

            Instrumentation.SetUserData("user_id", UserId);


            string language = UtilityManager.GetLanguageParameter();



            string _currentAttempts = CurrentAttempt.ToString();
            string languag = UtilityManager.GetLanguageParameter();

            TaxPayerProfile TPProfile = await WebServiceManager.GetTPProfileDataAPICall(UserId);
            if (TPProfile != null)
            {

                App.TP = new TaxPayerProfile();
                App.TP = TPProfile;
                App.TP.Userid = App.LoginDataRetrieved.TIN;
                try
                {
                    if (App.LoginDataRetrieved != null)
                    {
                        if (App.TP != null)
                        {
                            App.TP.NameFirst = App.LoginDataRetrieved.NameFirst;
                            App.TP.NameLast = App.LoginDataRetrieved.NameLast;
                            App.TP.NameOrg1 = App.LoginDataRetrieved.NameOrg1;
                            App.TP.TypeChk = App.LoginDataRetrieved.TypeChk;
                        }

                    }

                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());
                }
            }

            String OnAuthenticationSuccessMsg = AppResources.LoginSuccessful;
            String OnSuccessfulAuthenticationqMsg = AppResources.EnterVerificationCode;

            await Task.Run(() =>
            {
                try
                {

                    App.IsLoginCalled = false;
                    App.ArePreLoginLangCookiesSet = false;

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _navigationService.NavigateTo(App.GAZTNewDesignDashBoardPageView, false);
                        App.HasToRefreshLoaderOnDashboard = true;
                        //TODO for continue work on EST added by ashwini
                        //_navigationService.NavigateTo(App.EstablishmentRegistrationPage);
                    });

                   
                }
                catch (GAZTInternetException)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                    });


                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(AppResources.ZZSomethingwentwrong + " " + AppResources.ZZInternetConnectionMessage, AppResources.Information);
                    });
                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());
                }

            });

        }

        public async Task Logout()
        {
            await WebServiceManager.GAZTLogOff();
            App.IsLogOut = true;
            App.httpClientHandler.CookieContainer = new System.Net.CookieContainer();
        }

        #endregion
    }
}