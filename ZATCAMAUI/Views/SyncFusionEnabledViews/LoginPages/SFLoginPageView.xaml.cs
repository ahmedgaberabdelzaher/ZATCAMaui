using Mopups.Services;
using Syncfusion.Maui.Picker;
using System.Globalization;
using System.Resources;
using ZATCAMAUI.Core.CustomControls;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.LoginPage;
using ZATCAMAUI.Views.SyncFusionEnabledViews.UnlockAccount;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.LoginPages
{
    /// <summary>
    /// Page to login with user name and password
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SFLoginPageView : ContentPage
    {
        SFLoginPageViewModel viewModel;

        HybridWebView hybridWebView = new HybridWebView();

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage" /> class.
        /// </summary>
        public SFLoginPageView(string strNavigateToThisService)
        {
            try
            {
                InitializeComponent();

                App.VATType = PageExecutionType.Register;
                App.ZAKATType = PageExecutionType.Register;
                viewModel = App.Locator.SFLoginPageView;

                this.BindingContext = viewModel;
                viewModel.CurrentTab = 1;
                ChangeAeroIcon();
                CheckFirstTimeorNot();
                GetDeviceID();
                viewModel.NavigateToThisService = strNavigateToThisService;
                hybridWebView.BackgroundColor = (Color)Application.Current.Resources["Primary"];

                if (App.IsArabic)
                {
                    this.FlowDirection = FlowDirection.RightToLeft;
                    CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                    SfPickerResources.ResourceManager = new ResourceManager("ZATCAMAUI.SyncfusionControl", Application.Current.GetType().Assembly);
                }
                else
                {


                    this.FlowDirection = FlowDirection.LeftToRight;
                    CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                    SfPickerResources.ResourceManager = new ResourceManager("ZATCAMAUI.AppResources", Application.Current.GetType().Assembly);
                }


                MessagingCenter.Subscribe<string>(this, "UnlockAccountBackButtonClicked", message =>
                {
                    OnAppearing();
                });

                MessagingCenter.Subscribe<object, string>(this, "RefreshLoginPage", async (sender, arg) =>
                {
                    OnAppearing();
                });

                MessagingCenter.Subscribe<string>(this, "OnActivated", message =>
                {
                    OnAppearing();
                });
                MessagingCenter.Subscribe<object, string>(this, "SessionExpired", (sender, arg) =>
                {
                });


                App.ArePreLoginLangCookiesSet = false;
                App.IsLoginCalled = false;
                viewModel.TINIndex = 0;

            }
            catch (Exception)
            {


            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                MessagingCenter.Subscribe<string>(this, "TinList", message =>
                {
                    viewModel.IsVisibleTinIds = true;
                });
                ChangeAeroIcon();
                viewModel.Password = string.Empty;
                viewModel.Email = string.Empty;

                App.TP = null;
                viewModel.CurrentAttempt = 0;
                if (App.CurrentDropdownTIN != null)
                    viewModel.SelectedTinId = App.CurrentDropdownTIN;

                viewModel.IsVisibleTinIds = false;

                string lang = "AR";

                if (App.IsArabic)
                {
                    this.FlowDirection = FlowDirection.RightToLeft;
                }
                else
                {
                    lang = "EN";

                    this.FlowDirection = FlowDirection.LeftToRight;
                }

                var platform = DeviceInfo.Platform;
                if (platform == DevicePlatform.Android && App.isAndroidRefresh == false)
                {
                    if (hybridWebView != null)
                    {
                        loginGrid.Children.Remove(hybridWebView);
                    }
                    hybridWebView = new HybridWebView();
                    HandleWebViewLoad(lang);
                }
                else
                {
                    if (platform == DevicePlatform.iOS)
                    {
                        if (hybridWebView != null)
                        {
                            loginGrid.Children.Remove(hybridWebView);
                        }

                        hybridWebView = new HybridWebView();
                        HandleWebViewLoad(lang);
                    }
                }

            }
            catch (Exception )
            {
            }
        }

        private void HandleWebViewLoad(string lang)
        {
            try
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    viewModel.IsLoading = true;
                    hybridWebView.Opacity = 0;
                    var objSession = Preferences.Default.ContainsKey("IsSessionExpired") ? Preferences.Default.Get("IsSessionExpired", false) : false;
                    if (objSession)
                    {
                        loginGrid.Opacity = 0;
                        sessionExpiredView.IsVisible = true;
                    }
                });

                hybridWebView.HorizontalOptions = LayoutOptions.FillAndExpand;
                hybridWebView.VerticalOptions = LayoutOptions.FillAndExpand;


                hybridWebView.Url = viewModel.CreateLoginURL(lang);

                hybridWebView.RegisterAction((data) =>
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        try
                        {
                            if (data == "displayLoginLoadingIndicator")
                            {
                                hybridWebView.Opacity = 0;
                                viewModel.IsLoading = true;
                            }

                            else if (data == "displayLoadingIndicator")
                            {
                                hybridWebView.Opacity = 0;
                                viewModel.IsLoading = true;
                            }

                            else if (data == "hideLoadingIndicator")
                            {
                                hybridWebView.Opacity = 1;
                                viewModel.IsLoading = false;

                            }

                            else if (data == "hideLoginLoadingIndicator")
                            {
                                hybridWebView.Opacity = 1;
                                viewModel.IsLoading = false;
                            }
                            
                            else if (data == "IsloginControl")
                            {
                                hybridWebView.Url = viewModel.CreateLoginURL(lang);
                            }
                            
                            else if (data == "requestTimedout")
                            {
                                hybridWebView.Opacity = 0;
                                viewModel.IsLoading = false;
                                App.isAndroidUrlloaded = false;

                                if (App.LoginDataRetrieved.AppMsg == "" || App.LoginDataRetrieved.AppMsg == null)
                                {
                                    App.LoginDataRetrieved.AppMsg = AppResources.RequestTimeoutDescription;
                                }

                                if (App.LoginDataRetrieved.MsgTitle == "" || App.LoginDataRetrieved.MsgTitle == null)
                                {
                                    App.LoginDataRetrieved.MsgTitle = AppResources.RequestTimeoutTitle;

                                }

                                await viewModel._dialogService.ShowMessageBox(App.LoginDataRetrieved.AppMsg, App.LoginDataRetrieved.MsgTitle);
                                //hybridWebView.RefreshCommand();

                                try
                                {
                                    await LogoffUser();
                                    GoBackToOnaboardingScreen();
                                }
                                catch (Exception)
                                {


                                    GoBackToOnaboardingScreen();
                                }
                            }

                            else if (data == "success")
                            {

                                try
                                {
                                    string[] minMaxVersions = App.LoginDataRetrieved.AppVersion.Split('-');

                                    if (minMaxVersions.Count() > 1)
                                    {
                                        double minVer = Convert.ToDouble(minMaxVersions[0].Replace(".", string.Empty));
                                        double maxVer = Convert.ToDouble(minMaxVersions[1].Replace(".", string.Empty));
                                        double currVer = Convert.ToDouble(App.AppVersion.Replace(".", string.Empty));

                                        if (currVer >= minVer && currVer <= maxVer)
                                        {
                                            App.IsUserLoggedIn = true;
                                            Preferences.Default.Set("timeOut", DateTime.Now);
                                            await viewModel.LoginCompletedInWebView();
                                        }
                                        else
                                        {
                                            hybridWebView.Opacity = 0;
                                            viewModel.IsLoading = false;

                                            await viewModel._dialogService.ShowMessageBox(AppResources.VersonCheckErrorMsg, AppResources.VersonCheckErrorTitle);
                                            await LogoffUser();
                                        }
                                    }
                                    else
                                    {
                                        App.LoginDataRetrieved.AppVersion = string.Empty;

                                        if (App.LoginDataRetrieved.AppVersion == App.AppVersion)
                                        {
                                            App.IsUserLoggedIn = true;
                                            await viewModel.LoginCompletedInWebView();
                                        }
                                        else
                                        {
                                            hybridWebView.Opacity = 0;
                                            viewModel.IsLoading = false;

                                            await viewModel._dialogService.ShowMessageBox(AppResources.VersonCheckErrorMsg, AppResources.VersonCheckErrorTitle);
                                            await LogoffUser();
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                    await viewModel._dialogService.ShowMessageBox(AppResources.Somethingwentwrong, AppResources.Information);
                                }
                            }

                            else if (data == "navigateToForgotUsernamePage")
                            {
                                hybridWebView.Opacity = 0;
                                viewModel._navigationService.NavigateTo(App.GAZTNewDesignForgotPasswordPageView);
                            }

                            else if (data == "navigateToUnlockAccountPage")
                            {
                                hybridWebView.Opacity = 0;
                                await MopupService.Instance.PushAsync(new UnlockAccountTINPageView());
                            }

                            else if (data == "navigateToVATIndividualSignupPage")
                            {
                                hybridWebView.Opacity = 0;
                                viewModel._navigationService.NavigateTo(App.EstablishmentSignUPPageView);
                            }

                            else if (data == "navigateToVATIndividualSignupPageSSO")
                            {
                                viewModel._navigationService.NavigateTo(App.IndividualRegistrationPageView, "RegisterPageSSO");
                            }

                            else if (data == "navigateBackToLoginPage")
                            {
                                App.IsLoginCalled = false;
                                OnAppearing();
                            }
                            if (data == ZATCAConstants.AppChangeMobCompanay)
                            {
                                viewModel._navigationService.NavigateTo(App.ChangeMobileRequestPageView, "");
                            }

                            if (data == ZATCAConstants.AppChangeMobCompanayNafath)
                            {
                                if (App.GUIDFrChangeMob.Contains(ZATCAConstants.WebKeyChangeMobCompanayNafath))
                                {
                                    var guid = App.GUIDFrChangeMob.Split("guid=")[1];
                                    viewModel._navigationService.NavigateTo(App.ChangeMobileRequestPageView, guid);
                                }
                            }

                            else if (data == "error")
                            {
                                hybridWebView.Opacity = 0;
                                viewModel.IsLoading = false;

                                if (App.LoginDataRetrieved.AppMsg == "" || App.LoginDataRetrieved.AppMsg == null)
                                {
                                    App.LoginDataRetrieved.AppMsg = AppResources.Somethingwentwrong;
                                }

                                if (App.LoginDataRetrieved.MsgTitle == "" || App.LoginDataRetrieved.MsgTitle == null)
                                {
                                    App.LoginDataRetrieved.MsgTitle = AppResources.Information;
                                };

                                if (App.LoginDataRetrieved.AppMsg == "Please complete registration process on Portal to Login into the app." || App.LoginDataRetrieved.AppMsg == "الرجاء اكمال التسجيل من خلال الموقع الإلكتروني للدخول للتطبيق")
                                {
                                    App.IsUserLoggedIn = true;
                                    await viewModel.LoginCompletedInWebViewForVATRegistrationTestPurpose();
                                }
                                else
                                {
                                    await viewModel._dialogService.ShowMessageBox(App.LoginDataRetrieved.AppMsg, App.LoginDataRetrieved.MsgTitle);
                                    await LogoffUser();
                                }
                            }

                            else if (data == "errorGeneric")
                            {
                                hybridWebView.Opacity = 0;
                                viewModel.IsLoading = false;

                                await viewModel._dialogService.ShowMessageBox(AppResources.Somethingwentwrong, AppResources.Information);
                                viewModel.IsLoading = true;

                                if (App.TP != null)
                                    App.TP = null;
                                if (App.PreviousIsArabic)
                                {
                                    string langName = "ar-AE";
                                    AppResources.Culture = new CultureInfo(langName);
                                }
                                else
                                {
                                    string langName = "en-US";
                                    AppResources.Culture = new CultureInfo(langName);
                                }

                                try
                                {
                                    await WebServiceManager.GAZTLogOff();
                                }
                                catch
                                {

                                }


                                viewModel.IsLoading = false;

                                var _navigation = Application.Current.MainPage.Navigation;
                                foreach (var item in _navigation.NavigationStack)
                                {
                                    if (item.GetType().Name == App.SFAnonymousLandingPageView)
                                    {
                                        _navigation.RemovePage(item);
                                        break;
                                    }
                                }

                                App.IsLogOut = true;
                                App.IsLoginCalled = false;
                                App.IsSamlApiCalledAndroid = false;

                                try
                                {
                                    App.httpClientHandler = new HttpClientHandler();
                                    App.httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                                    App.httpClientHandler.CookieContainer = new System.Net.CookieContainer();
                                }
                                catch (Exception)
                                {


                                }

                                viewModel._navigationService.NavigateTo(App.GAZTNewDesignOnBoardingAnimationPageView);
                                _navigation.NavigationStack.ToList().Clear();

                            }
                        }

                        catch (Exception)
                        {

                        }
                    });
                });

                loginGrid.Add(hybridWebView, 0, 0);

                //TODO
                //// send the child to back because LowerChild() is not available in MAUI
                //loginGrid.Children.RemoveAt(loginGrid.Children.IndexOf(hybridWebView));
                //loginGrid.Insert(loginGrid.Children.Count, hybridWebView);
            }
            catch (Exception)
            {
            }
        }

        public SFLoginPageView()
        {
            this.BindingContext = viewModel = App.Locator.SFLoginPageView;

            MessagingCenter.Subscribe<SFLoginPageView, string>(this, "callback", (send, arg) =>
            {
                if (arg == "RequestTimedOut")
                {
                    viewModel.IsLoading = false;
                    GoBackToOnaboardingScreen();
                }
                if (arg == "LoadingFinished")
                {
                    viewModel.IsLoading = false;
                }
            });
            viewModel.CurrentTab = 1;
            viewModel.IsLoading = false;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel.password = string.Empty;
            viewModel.email = string.Empty;
            viewModel.Password = string.Empty;
            viewModel.Email = string.Empty;
            MessagingCenter.Unsubscribe<string>(this, "OnActivated");
        }

        private async Task LogoffUser()
        {
            viewModel.IsLoading = true;

            if (App.TP != null)
                App.TP = null;
            if (App.PreviousIsArabic)
            {
                string langName = "ar-AE";
                AppResources.Culture = new CultureInfo(langName);
            }
            else
            {
                string langName = "en-US";
                AppResources.Culture = new CultureInfo(langName);
            }

            try
            {
                await WebServiceManager.GAZTLogOff();
            }
            catch
            {

            }

            viewModel.IsLoading = false;

            var _navigation = Application.Current.MainPage.Navigation;
            foreach (var item in _navigation.NavigationStack)
            {
                if (item.GetType().Name == App.SFAnonymousLandingPageView)
                {
                    _navigation.RemovePage(item);
                    break;
                }
            }

            App.IsLogOut = true;
            App.IsLoginCalled = false;
            App.IsSamlApiCalledAndroid = false;

            try
            {
                App.httpClientHandler = new HttpClientHandler();
                App.httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                App.httpClientHandler.CookieContainer = new System.Net.CookieContainer();
            }
            catch (Exception)
            {


            }

            viewModel._navigationService.NavigateTo(App.GAZTNewDesignOnBoardingAnimationPageView);
            _navigation.NavigationStack.ToList().Clear();

        }

        private void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["Back"];
            }
        }

        private void CheckFirstTimeorNot()
        {
            Preferences.Set("first_TimeLoging_key", "False");

        }

        private void GetDeviceID()
        {
            string deviceId = Guid.NewGuid().ToString();
            viewModel.DeviceId = deviceId;
        }

        private void btnLoginClicked(object sender, EventArgs e)
        {
            sessionExpiredView.IsVisible = false;
            loginGrid.Opacity = 1;
            Preferences.Default.Set("IsSessionExpired", false);
            App.isAndroidUrlloaded = false;
            OnAppearing();

            App.ResetAndContinueSession();
        }

        private void GoBackToOnaboardingScreen()
        {
            viewModel._navigationService.NavigateTo(App.GAZTNewDesignOnBoardingAnimationPageView);
            var _navigation = Application.Current.MainPage.Navigation;
            _navigation.NavigationStack.ToList().Clear();
        }

    }
}