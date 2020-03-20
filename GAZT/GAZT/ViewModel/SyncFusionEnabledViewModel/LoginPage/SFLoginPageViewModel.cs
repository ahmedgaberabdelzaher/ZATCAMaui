using GalaSoft.MvvmLight.Views;
using GAZT;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace GAZTeServicesApp.ViewModels.LoginPage
{
    /// <summary>
    /// ViewModel for login page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SFLoginPageViewModel : SFLoginViewModel
    {
        #region Fields

        private string password;
        public int CurrentAttempt = 0;
        #endregion

        #region Constructor


        /// <summary>
        /// Initializes a new instance for the <see cref="LoginPageViewModel" /> class.
        /// </summary>
        public SFLoginPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            this.LoginCommand = new Command(async () =>
            {
                await this.LoginClicked();
            });
            this.SignUpCommand = new Command(this.SignUpClicked);
            this.ForgotPasswordCommand = new Command(this.ForgotPasswordClicked);
            this.SocialMediaLoginCommand = new Command(this.SocialLoggedIn);
        }

        #endregion

        #region property
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
                            else if(gex is GAZTException)
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

        #endregion

        #region methods

        /// <summary>
        /// Invoked when the Log In button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async Task LoginClicked()
        {
            CurrentAttempt++;

            await Task.Run(() =>
            {
                IsLoading = true;
            });

            string response = string.Empty;
            string UserId = string.Empty;
            await Task.Run(() =>
            {
                string language = UtilityManager.GetLanguageParameter();
                String lang = "E";
                if (App.IsArabic == true)
                    lang = "AR";
                string _currentAttempts = CurrentAttempt.ToString();
                string languag = UtilityManager.GetLanguageParameter();
                if (SelectedTinId != null && IsVisibleTinIds == true)
                {
                    bool isValidEmail = UtilityManager.IsValidEmailAddress(Email);
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
                    bool isValidTin = UtilityManager.IsOTPNumberValid(Email);
                    if (isValidTin == true)

                    {
                        response = WebServiceManager.GAZTAuthenticateTIN(Email, Password, DeviceId, _currentAttempts, languag);
                        UserId = Email;
                    }
                    else
                    {
                        throw new Exception(AppResources.ZUserNameIncorrect);
                    }
                }


                response = WebServiceManager.GAZTAuthenticateTIN(this.Email, this.Password, DeviceId, _currentAttempts, languag);
                try
                {
                    if (0 == String.Compare("success", response, true))
                    {
                        TaxPayerProfile TPProfile = WebServiceManager.SFGAZTGetTaxPayerProfile(this.Email, "EN");
                        if (TPProfile != null)
                        {
                            TPProfile.Tin = Email;
                            App.TP = TPProfile;
                        }
                    }
                }
                catch (GAZTException gex)
                {
                    IsLoading = false;

                    string MessageForTheUser = gex.Message;

                    if (gex is GAZTUserDoesNotExistException)
                    {
                        MessageForTheUser = AppResources.UserDoesNotExist;
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
                        MessageForTheUser = AppResources.ZAccountLocked;
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
                        MessageForTheUser = AppResources.InvalidPassword;
                    }
                    else if (gex is GAZTAccountLockedFor60MinutesAfterLastLoginAttemptException)
                    {
                        MessageForTheUser = AppResources.ZAccountLocked;
                    }
                    else if (gex is GAZTTaxpayersAccountNotActiveWithGAZTException)
                    {
                        MessageForTheUser = gex.Message;
                    }

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    });
                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        IsLoading = false;
                        await _dialogService.ShowMessage("something went wrong", AppResources.Information);
                    });
                }

            });

            await Task.Run(() =>
            {
                IsLoading = false;
            });

            base._navigationService.NavigateTo("LandingPageView");

        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SignUpClicked(object obj)
        {
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
        }

        /// <summary>
        /// Invoked when social media login button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SocialLoggedIn(object obj)
        {
            // Do something
        }

        #endregion
    }
}