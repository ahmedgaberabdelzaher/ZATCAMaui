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
using Xamarin.Essentials;
using GAZT.Helper;
using AppDynamics.Agent;

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

        private DateTime lastTapped;
        #endregion
        #region ConstructorF
        /// <summary>
        /// Initializes a new instance for the <see cref="LoginPageViewModel" /> class.
        /// </summary>
        public SFLoginPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
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
            this.LoginCommand = new Command(async () =>
            {
                //Task LoginClickedTask = Task.Run(async () =>
                //{
                //    if (lastTapped < DateTime.Now.AddSeconds(-2))
                //    {
                //        await this.LoginClicked();
                //    }
                //});
                try
                {
                    Task.Run(() =>
                    {
                        IsLoading = true;
                    });
                    await Task.Run(async () =>
                    {
                        Task LoginClickedTask = Task.Run(async () =>
                        {
                            await this.LoginClicked();
                        });
                        LoginClickedTask.Wait();
                    });
                    Task.Run(() =>
                    {
                        IsLoading = false;
                    });
                }
                catch (AggregateException ae)
                {
                    IsLoading = false;
                    foreach (var gex in ae.InnerExceptions)
                    {
                        // Handle the GAZT custom exception.
                        if (gex is GAZTException)
                        {
                            string MessageForTheUser = gex.Message;
                            if (gex is GAZTNetworkConnectivityIssueException)
                            {
                                MessageForTheUser = AppResources.NetworkConnectivityIssue;
                            }
                            else if (gex is GAZTInternetException)
                            {
                                MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                            }
                            else if (gex is GAZTRegistrationPendingException)
                            {
                                MessageForTheUser = AppResources.RegistrationIsPending;
                            }
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                            });
                        }
                        // Rethrow any other exception.
                        else
                        {
                            throw;
                        }
                    }
                }
                catch (Exception)
                {
                    IsLoading = false;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    });
                }
            });
            this.BackButtonClicked = new Command(this.BackButtonClick);
            this.SignUpCommand = new Command(this.SignUpClicked);
            this.ForgotPasswordCommand = new Command(this.ForgotPasswordClicked);
            this.SocialMediaLoginCommand = new Command(this.SocialLoggedIn);
            this.HamburgerMenuClickedCommand = new Command(this.HamburgerMenuClicked);

            GoBackClick = new Command(async () =>
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
                //if (this.password == value)
                //{
                //    return;
                //}
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
                _NavigaateToThisService = value;
            }
        }
        #endregion
        #region Command
        /// <summary>
        /// Gets or sets the command that is executed when the Log In button is clicked.
        /// </summary>
        public Command LoginCommand { get; set; }
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
        /// Invoked when the Log In button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async Task LoginClicked()
        {
            App.IsComingFromSleepMode = false;
            CurrentAttempt++;
            await Task.Run(() =>
            {
                IsLoading = true;
            });
            try
            {
                string response = string.Empty;
                string UserId = string.Empty;
                await Task.Run(async () =>
                {
                    try
                    {
                        string language = UtilityManager.GetLanguageParameter();
                        String lang = "E";
                        if (App.IsArabic == true)
                            lang = "AR";
                        if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                        {
                            throw new GAZTLoginDetailsException();
                        }
                        string _currentAttempts = CurrentAttempt.ToString();
                        string languag = UtilityManager.GetLanguageParameter();
                        if (SelectedTinId != null && IsVisibleTinIds == true)
                        {
                            bool isValidEmail = UtilityManager.IsValidEmailAddress(Email);
                            if (isValidEmail == true)
                            {
                                response = WebServiceManager.SFGAZTAuthenticateTIN(SelectedTinId.Tin, Password, DeviceId, _currentAttempts, languag);
                                UserId = SelectedTinId.Tin;
                            }
                            else
                            {
                                throw new GAZTUserNameIncorrectException();
                            }
                        }
                        else
                        {
                            bool isValidTIN = UtilityManager.IsOTPNumberValid(Email);
                            if (isValidTIN == true)
                            {

                                response = WebServiceManager.SFGAZTAuthenticateTIN(Email, Password, DeviceId, _currentAttempts, languag);

                                UserId = Email;
                            }
                            else
                            {
                                throw new GAZTUserNameIncorrectException();
                            }
                        }
                        if (0 == String.Compare("success", response, true))
                        {
                            TaxPayerProfile TPProfile = WebServiceManager.SFGAZTGetTaxPayerProfile(UserId, lang);
                            if (TPProfile != null)
                            {
                                if ((0 == string.Compare("Registration is pending", TPProfile.TpType)))
                                {
                                    throw new GAZTRegistrationPendingException();
                                }
                                App.TP = new TaxPayerProfile();
                                TPProfile.Tin = Email;
                                TPProfile.Userid = UserId;
                                App.TP = TPProfile;
                            }
                            String OnAuthenticationSuccessMsg = AppResources.LoginSuccessful;
                            String OnSuccessfulAuthenticationqMsg = AppResources.EnterVerificationCode;
                            await Task.Run(async () =>
                            {
                                try
                                {
                                    string currentAttempts = "1";
                                    response = await WebServiceManager.GAZTSendAndReceiveOTP(lang, UserId, currentAttempts);
                                    if (0 == String.Compare("OTP has send", response, true) || 0 == String.Compare("كلمة مرور مرة واحدة قد أرسلت", response, true))
                                    {
                                        App.TP.Userid = UserId;
                                        App.TP.Password = Password;
                                        ComingToOTPVerificationScreenFrom NavigatingFromLogin = ComingToOTPVerificationScreenFrom.IsLogin;
                                        Device.BeginInvokeOnMainThread(() =>
                                        {
                                            _navigationService.NavigateTo(App.OTPPageView, new ComingToOTPVerificationScreenFromAndNavigatingTo()
                                            {
                                                _ComingToOTPVerificationScreenFrom = NavigatingFromLogin,
                                                NavigateToThisService = NavigateToThisService
                                            });
                                        });
                                    }
                                    else
                                    {
                                        await Task.Run(() =>
                                        {
                                            IsLoading = false;
                                        });
                                        if (string.IsNullOrEmpty(response))
                                        {
                                            Device.BeginInvokeOnMainThread(async () =>
                                            {
                                                await _dialogService.ShowMessageBox(AppResources.Somethingwentwrong, AppResources.Information);
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
                                }
                                catch (GAZTInternetException)
                                {
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        await _dialogService.ShowMessageBox(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                                    });
                                }
                                catch (Exception)
                                {
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        await _dialogService.ShowMessageBox(AppResources.ZZSomethingwentwrong + " " + AppResources.ZZInternetConnectionMessage, AppResources.Information);
                                    });
                                }
                            });
                        }
                        else
                        {
                            if (App.IsArabic)
                            {
                                string tin = Email;
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
                    catch (GAZTException gex)
                    {
                        IsLoading = false;
                        string MessageForTheUser = gex.Message;
                        if (gex is GAZTUserDoesNotExistException)
                        {
                            MessageForTheUser = AppResources.UserDoesNotExist;
                        }
                        if (gex is GAZTWrongTINOrEmailException)
                        {
                            MessageForTheUser = AppResources.ZZZWrongEnterTin;
                        }
                        else if (gex is GAZTUserAuthenticationFailedException)
                        {
                            MessageForTheUser = AppResources.UserAuthenticationFailed;
                        }
                        else if (gex is GAZTNetworkConnectivityIssueException)
                        {
                            MessageForTheUser = AppResources.NetworkConnectivityIssue;
                        }
                        else if (gex is GAZTInternetException)
                        {
                            MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                        }
                        else if (gex is GAZTPasswordLockedException)
                        {
                            MessageForTheUser = AppResources.ZPasswordLocked;
                        }
                        else if (gex is GAZTUserCurrentlyInvalidException)
                        {
                            MessageForTheUser = gex.Message;
                        }
                        else if (gex is GAZTUserAccountLockedException)
                        {
                            MessageForTheUser = AppResources.UserAccountLocked;
                        }
                        else if (gex is GAZTPasswordIsLockedDueToInvalidAttemptsException)
                        {
                            MessageForTheUser = AppResources.ZZPasswordislockedInvalidattempts;
                        }
                        else if (gex is GAZTTaxpayersAccountInActiveWithGAZTException)
                        {
                            MessageForTheUser = gex.Message;
                        }
                        else if (gex is GAZTWrongTINOrEmailException)
                        {
                            MessageForTheUser = gex.Message;
                        }
                        else if (gex is GAZTWrongPasswordException)
                        {
                            MessageForTheUser = AppResources.ZZWrongpassword;
                        }
                        else if (gex is GAZTAccountLockedFor60MinutesAfterLastLoginAttemptException)
                        {
                            MessageForTheUser = AppResources.ZZTheaccountislockedfor60minutesafterthelastloginattempt;
                        }
                        else if (gex is GAZTTaxpayersAccountNotActiveWithGAZTException)
                        {
                            MessageForTheUser = gex.Message;
                        }
                        else if (gex is GAZTLoginDetailsException)
                        {
                            MessageForTheUser = AppResources.Pleaseenteryourlogininformation;
                        }
                        else if (gex is GAZTWrongTINOrEmailException)
                        {
                            MessageForTheUser = AppResources.ZZZWrongEnterTin;
                        }
                        else if (gex is GAZTUserNameIncorrectException)
                        {
                            MessageForTheUser = AppResources.ZUserNameIncorrect;
                        }
                        else if (gex is GAZTUserNameIncorrectException)
                        {
                            MessageForTheUser = AppResources.ZUserNameIncorrect;
                        }
                        else if (gex is GAZTRegistrationPendingException)
                        {
                            MessageForTheUser = AppResources.RegistrationIsPending;
                        }
                        else if (gex is GAZTInvalidDataException)
                        {
                            MessageForTheUser = AppResources.Somethingwentwrong;
                        }
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                        });
                    }
                });
            }
            catch (Exception ex)
            {
                await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            await Task.Run(() =>
            {
                IsLoading = false;
            });

        }
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
            label.BackgroundColor = Color.FromHex("#70FFFFFF");
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
            _navigationService.NavigateTo(App.DashboardAnonymousMenuPageView);
            //_navigationService.NavigateTo(App.TaxEvasionVerifyMobileNumberPage);
            //_dialogService.ShowMessage("Anonymous menu will appear", "Menu");
        }
        #endregion
        #region New Authentication

        public string CreateLoginURL(string lang)
        {
            string deviceOs = Xamarin.Essentials.DeviceInfo.Platform.ToString();
            string deviceUdid = DependencyService.Get<IDeviceInfo>().GetDeviceUdid();
            return WebServiceManager.CreateSAMLLoginURL("", deviceUdid, "", deviceOs, lang);

            //return WebServiceManager.CreateSAMLLoginURL("", "", "", "", lang);
        }

        public async Task LoginCompletedInWebView()
        {
            string response = string.Empty;
            string UserId = App.LoginDataRetrieved.TIN;

            Instrumentation.SetUserData("user_id", UserId);

            String lang = "E";
            string language = UtilityManager.GetLanguageParameter();

            if (App.IsArabic == true)
                lang = "AR";

            string _currentAttempts = CurrentAttempt.ToString();
            string languag = UtilityManager.GetLanguageParameter();

            // * OLD TP PROFILE API
            //TaxPayerProfile TPProfile = WebServiceManager.SFGAZTGetTaxPayerProfile(UserId, lang);

            // * NEW TP PROFILE API
            TaxPayerProfile TPProfile = await WebServiceManager.GetTPProfileDataAPICall(UserId);

            if (TPProfile != null)
            {
                //if ((0 == string.Compare("Registration is pending", TPProfile.TpType)))
                //{
                //    throw new GAZTRegistrationPendingException();
                //}

                App.TP = new TaxPayerProfile();
                App.TP = TPProfile;
                App.TP.Userid = TPProfile.Tin;
            }

            String OnAuthenticationSuccessMsg = AppResources.LoginSuccessful;
            String OnSuccessfulAuthenticationqMsg = AppResources.EnterVerificationCode;

            await Task.Run(async () =>
            {
                try
                {

                    App.IsLoginCalled = false;
                    App.ArePreLoginLangCookiesSet = false;

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _navigationService.NavigateTo(App.GAZTNewDesignDashBoardPageView);
                        App.HasToRefreshLoaderOnDashboard = true;
                    });

                    //if (App.IsOTPByPassed == true)
                    //{
                    //}
                    //else
                    //{
                    //    string currentAttempts = "1";
                    //    response = await WebServiceManager.GAZTSendAndReceiveOTP(lang, UserId, currentAttempts);
                    //    IsLoading = false;
                    //    App.IsLoginCalled = false;
                    //    App.ArePreLoginLangCookiesSet = false;

                    //    if (0 == String.Compare("OTP has send", response, true) || 0 == String.Compare("كلمة مرور مرة واحدة قد أرسلت", response, true))
                    //    {

                    //        App.TP.Userid = UserId;
                    //        App.TP.Password = Password;

                    //        ComingToOTPVerificationScreenFrom NavigatingFromLogin = ComingToOTPVerificationScreenFrom.IsLogin;

                    //        Device.BeginInvokeOnMainThread(() =>
                    //        {
                    //            _navigationService.NavigateTo(App.OTPPageView, new ComingToOTPVerificationScreenFromAndNavigatingTo()
                    //            {
                    //                _ComingToOTPVerificationScreenFrom = NavigatingFromLogin,
                    //                NavigateToThisService = NavigateToThisService
                    //            });
                    //        });

                    //    }
                    //    else
                    //    {
                    //        await Task.Run(() =>
                    //        {
                    //            IsLoading = false;
                    //        });
                    //        Device.BeginInvokeOnMainThread(async () =>
                    //        {
                    //            await _dialogService.ShowMessageBox(response, AppResources.Information);
                    //        });
                    //    }
                    //}
                }
                catch (GAZTInternetException)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                    });


                }
                catch (Exception)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(AppResources.ZZSomethingwentwrong + " " + AppResources.ZZInternetConnectionMessage, AppResources.Information);
                    });
                }

            });

        }

        public async Task LoginCompletedInWebViewForVATRegistrationTestPurpose()
        {
            string response = string.Empty;
            string UserId = App.LoginDataRetrieved.TIN;

            String lang = "E";
            string language = UtilityManager.GetLanguageParameter();

            if (App.IsArabic == true)
                lang = "AR";

            string _currentAttempts = CurrentAttempt.ToString();
            string languag = UtilityManager.GetLanguageParameter();

            TaxPayerProfile TPProfile = WebServiceManager.SFGAZTGetTaxPayerProfile(UserId, lang);

            if (TPProfile != null)
            {
                //if ((0 == string.Compare("Registration is pending", TPProfile.TpType)))
                //{
                //    throw new GAZTRegistrationPendingException();
                //}

                App.TP = new TaxPayerProfile();
                App.TP = TPProfile;
                App.TP.Userid = App.LoginDataRetrieved.TIN;
            }

            String OnAuthenticationSuccessMsg = AppResources.LoginSuccessful;
            String OnSuccessfulAuthenticationqMsg = AppResources.EnterVerificationCode;

            await Task.Run(async () =>
            {
                try
                {

                    App.IsLoginCalled = false;
                    App.ArePreLoginLangCookiesSet = false;

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _navigationService.NavigateTo(App.GAZTNewDesignDashBoardPageView);
                        App.HasToRefreshLoaderOnDashboard = true;
                        //TODO for continue work on EST added by ashwini
                        //_navigationService.NavigateTo(App.EstablishmentRegistrationPage);
                    });

                    //if (App.IsOTPByPassed == true)
                    //{
                    //}
                    //else
                    //{
                    //    string currentAttempts = "1";
                    //    response = await WebServiceManager.GAZTSendAndReceiveOTP(lang, UserId, currentAttempts);
                    //    IsLoading = false;
                    //    App.IsLoginCalled = false;
                    //    App.ArePreLoginLangCookiesSet = false;

                    //    if (0 == String.Compare("OTP has send", response, true) || 0 == String.Compare("كلمة مرور مرة واحدة قد أرسلت", response, true))
                    //    {

                    //        App.TP.Userid = UserId;
                    //        App.TP.Password = Password;

                    //        ComingToOTPVerificationScreenFrom NavigatingFromLogin = ComingToOTPVerificationScreenFrom.IsLogin;

                    //        Device.BeginInvokeOnMainThread(() =>
                    //        {
                    //            _navigationService.NavigateTo(App.OTPPageView, new ComingToOTPVerificationScreenFromAndNavigatingTo()
                    //            {
                    //                _ComingToOTPVerificationScreenFrom = NavigatingFromLogin,
                    //                NavigateToThisService = NavigateToThisService
                    //            });
                    //        });

                    //    }
                    //    else
                    //    {
                    //        await Task.Run(() =>
                    //        {
                    //            IsLoading = false;
                    //        });
                    //        Device.BeginInvokeOnMainThread(async () =>
                    //        {
                    //            await _dialogService.ShowMessageBox(response, AppResources.Information);
                    //        });
                    //    }
                    //}
                }
                catch (GAZTInternetException)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                    });


                }
                catch (Exception)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(AppResources.ZZSomethingwentwrong + " " + AppResources.ZZInternetConnectionMessage, AppResources.Information);
                    });
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