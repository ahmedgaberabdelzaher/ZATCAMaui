using GalaSoft.MvvmLight.Views;
using GAZTeServicesApp.Resources;
using GAZTeServicesBusinessLibrary;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace GAZTeServicesApp.ViewModels.LoginPage
{
    /// <summary>
    /// ViewModel for login page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class LoginPageViewModel : LoginViewModel
    {
        #region Fields

        private string password;

        #endregion

        #region Constructor


        /// <summary>
        /// Initializes a new instance for the <see cref="LoginPageViewModel" /> class.
        /// </summary>
        public LoginPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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
                this.NotifyPropertyChanged();
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
            await Task.Run(() =>
            {
                IsLoading = true;
            });

            string response = string.Empty;

            await Task.Run(() =>
            {
                response = WebServiceManager.GAZTAuthenticateTIN(this.Email, this.Password, "DeviceId", "0", "EN");

                try
                {
                    if (0 == String.Compare("success", response, true))
                    {
                        TaxPayerProfile TPProfile = WebServiceManager.GAZTGetTaxPayerProfile(this.Email, "EN");
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