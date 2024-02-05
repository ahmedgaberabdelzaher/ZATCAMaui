
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using RGPopup.Maui.Services;
using Syncfusion.Maui.Picker;
using System.Globalization;
using System.Resources;
using System.Text.RegularExpressions;
using ZATCAMAUI.Core.CustomControls;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.LoginPage;
using ZATCAMAUI.Views.NewDesign.ForgotPasswordPages;
using ZATCAMAUI.Views.SyncFusionEnabledViews.UnlockAccount;
using Application = Microsoft.Maui.Controls.Application;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.LoginPages
{
    /// <summary>
    /// Page to login with user name and password
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SFLoginPageView : ContentPage
    {
        SFLoginPageViewModel viewModel;

        private double width = 0;
        private double height = 0;
        HybridWebView hybridWebView = new HybridWebView();

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage" /> class.
        /// </summary>
        public SFLoginPageView(string strNavigateToThisService)
        {
            // SetLTRDirection();
            try
            {
                InitializeComponent();

                App.VATType = PageExecutionType.Register;
                App.ZAKATType = PageExecutionType.Register;
                NavigationPage.SetBackButtonTitle(this, " ");
                viewModel = App.Locator.SFLoginPageView;

                this.BindingContext = viewModel;// = App.Locator.SFLoginPageView;
                                                //On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
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

                DependencyService.Get<IStatusBar>().HideStatusBar();

                App.ArePreLoginLangCookiesSet = false;
                App.IsLoginCalled = false;

                var safeInsets = On<Microsoft.Maui.Controls.PlatformConfiguration.iOS>().SafeAreaInsets();
                safeInsets.Bottom = -10;
                this.Padding = safeInsets;

                NavigationPage.SetHasNavigationBar(this, false);
                viewModel.TINIndex = 0;

            }
            catch (Exception ex)
            {


            }
            // ParentContainer.RaiseChild(BusyIndicator);
        }

        public void CheckFirstTimeorNot()
        {
            Preferences.Set("first_TimeLoging_key", "False");

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

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width != this.width || height != this.height)
            {
                this.width = width;
                this.height = height;
                if (width > height)
                {
                    this.BackgroundImageSource = "sf_LoginBackgroundLand.png";
                }
                else
                {
                    this.BackgroundImageSource = "partials_background.png";
                    //  outerStack.Orientation = StackOrientation.Vertical;
                }
            }
        }
        public void ChangeAeroIcon()
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
        public void SetLTRDirection()
        {
            App.IsArabic = false;
            string langName = "en-US";
            CultureInfo ci = new CultureInfo(langName);
            AppResources.Culture = ci;
            //InitializeComponent();
            // this.FlowDirection = FlowDirection.LeftToRight;
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
                if (platform == DevicePlatform.Android && App.isAndroidRefresh == false) //#CR2068
                {
                    if (hybridWebView != null)
                    {
                        loginGrid.Children.Remove(hybridWebView);
                    }
                    hybridWebView = new HybridWebView();
                    //App.isAndroidRefresh = true;
                    ContinuedFunc(lang);
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
                        ContinuedFunc(lang);
                    }
                    else
                    {

                    }
                }

                var safeInsets = On<Microsoft.Maui.Controls.PlatformConfiguration.iOS>().SafeAreaInsets();
                if (Device.RuntimePlatform == Device.iOS && safeInsets.Bottom == 0)
                {
                    loginGrid.Margin = new Thickness(0, -50, 0, -30);
                }
            }
            catch (Exception)
            {


            }
        }

        private void ContinuedFunc(string lang)
        {

            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (hybridWebView != null)
                {
                    loginGrid.Children.Remove(hybridWebView);
                }
                hybridWebView = new HybridWebView();
                anotherFunc(lang);


            });
            var platform = DeviceInfo.Platform;
            hybridWebView.HorizontalOptions = LayoutOptions.FillAndExpand;
            hybridWebView.VerticalOptions = LayoutOptions.FillAndExpand;

            hybridWebView.Url = viewModel.CreateLoginURL(lang);

            hybridWebView.RegisterAction((data) =>
            {
                if (platform == DevicePlatform.iOS)
                {
                    if (hybridWebView != null)
                    {
                        loginGrid.Children.Remove(hybridWebView);
                    }

                    hybridWebView = new HybridWebView();
                    anotherFunc(lang);
                }
                else
                {

                }
            });

            var safeInsets = On<Microsoft.Maui.Controls.PlatformConfiguration.iOS>().SafeAreaInsets();

            if (Device.RuntimePlatform == Device.iOS && safeInsets.Bottom == 0)
            {
                loginGrid.Margin = new Thickness(0, -50, 0, -30);
            }
        }

        private void anotherFunc(string lang)
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

                            if (data == "displayLoadingIndicator")
                            {
                                viewModel.IsLoading = true;
                            }

                            if (data == "hideLoadingIndicator")
                            {
                                hybridWebView.Opacity = 1;
                                viewModel.IsLoading = false;

                            }

                            if (data == "hideLoginLoadingIndicator")
                            {
                                viewModel.IsLoading = false;
                            }

                            if (data == "IsloginControl")
                            {
                                hybridWebView.Url = viewModel.CreateLoginURL(lang);
                            }

                            if (data == "requestTimedout")
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

                            if (data == "success")
                            {
                                //App.IsUserLoggedIn = true;
                                //await viewModel.LoginCompletedInWebView();

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

                            if (data == "navigateToForgotUsernamePage")
                            {
                                hybridWebView.Opacity = 0;
                                await Navigation.PushModalAsync(new GAZTNewDesignForgotPasswordPageView(), true);
                            }

                            if (data == "navigateToUnlockAccountPage")
                            {
                                hybridWebView.Opacity = 0;
                                await PopupNavigation.Instance.PushAsync(new UnlockAccountTINPageView());
                            }

                            if (data == "navigateToVATIndividualSignupPage")
                            {
                                hybridWebView.Opacity = 0;
                                viewModel._navigationService.NavigateTo(App.EstablishmentSignUPPageView);
                            }

                            if (data == "navigateToVATIndividualSignupPageSSO")
                            {
                                viewModel._navigationService.NavigateTo(App.IndividualRegistrationPageView, "RegisterPageSSO");
                            }

                            if (data == "navigateBackToLoginPage")
                            {
                                App.IsLoginCalled = false;
                                OnAppearing();
                            }

                            if (data == "error")
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

                            if (data == "errorGeneric")
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
                // send the child to back because LowerChild() is not available in MAUI
                loginGrid.Children.RemoveAt(loginGrid.Children.IndexOf(hybridWebView));
                loginGrid.Insert(loginGrid.Children.Count, hybridWebView);
                //loginGrid.LowerChild(hybridWebView);
            }
            catch (Exception)
            {
            }

            
           
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

        private async void LoadWebView(bool isComingFromOnAppearing)
        {
            string lang = "en";

            if (App.IsArabic == true)
            {
                lang = "ar";
            }

            if (App.IsUserLoggedIn == false && isComingFromOnAppearing == false)
            {
                App.ArePreLoginLangCookiesSet = false;
                App.IsLoginCalled = false;

                hybridWebView.Opacity = 1;
                hybridWebView.Url = viewModel.CreateLoginURL(lang);
                hybridWebView.RefreshCommand();

                hybridWebView.RegisterAction(async (data) =>
                {
                    try
                    {
                        if (data == "displayLoginLoadingIndicator")
                        {
                            viewModel.IsLoading = true;
                            hybridWebView.Opacity = 0;
                            await hybridWebView.FadeTo(0, 2000);
                        }

                        if (data == "displayLoadingIndicator")
                        {
                            viewModel.IsLoading = true;
                        }

                        if (data == "hideLoadingIndicator")
                        {
                            viewModel.IsLoading = false;
                        }

                        if (data == "IsloginControl")
                        {
                            viewModel.IsLoading = true;
                            hybridWebView.Url = viewModel.CreateLoginURL(lang);
                        }

                        if (data == "hideLoginLoadingIndicator")
                        {
                            viewModel.IsLoading = false;
                        }

                        if (data == "requestTimedOut")
                        {
                            viewModel.IsLoading = false;
                            GoBackToOnaboardingScreen();
                        }

                        if (data == "success")
                        {
                            App.IsUserLoggedIn = true;
                            await viewModel.LoginCompletedInWebView();
                        }

                        if (data == "error")
                        {
                            viewModel.IsLoading = false;
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await viewModel._dialogService.ShowMessageBox(AppResources.Somethingwentwrong, AppResources.Information);
                                GoBackToOnaboardingScreen();
                            });
                        }
                    }

                    catch (Exception)
                    {



                    }
                });
            }
            else if (App.IsUserLoggedIn == true && isComingFromOnAppearing == true)
            {
                App.ArePreLoginLangCookiesSet = false;
                App.IsLoginCalled = false;

                hybridWebView.Url = viewModel.CreateLoginURL(lang);
                hybridWebView.RefreshCommand();

                viewModel.IsLoading = true;
                hybridWebView.Opacity = 0;
                await hybridWebView.FadeTo(1, 2000);
            }
        }

        private void GoBackToOnaboardingScreen()
        {
            viewModel._navigationService.NavigateTo(App.GAZTNewDesignOnBoardingAnimationPageView);
            var _navigation = Application.Current.MainPage.Navigation;
            _navigation.NavigationStack.ToList().Clear();
        }

        private void OnPasswordVisibilityClicked(object sender, EventArgs e)
        {
            viewModel.PasswordVisibility = !viewModel.PasswordVisibility;
        }
        private void onBackButtonClicked(object sender, EventArgs e)
        {
        }

        private static bool CheckValidEmail(string email)
        {
            bool isEmailValid = false;
            if (!string.IsNullOrEmpty(email))
            {
                var regex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                isEmailValid = regex.IsMatch(email) && !email.EndsWith(".");
            }
            return isEmailValid;
        }
        public static bool IsEnglishNumber(string arText)
        {
            bool isAllNumeric = true;
            if (!string.IsNullOrEmpty(arText))
            {
                foreach (char letter in arText.ToCharArray())
                {
                    if (!(letter >= 48 && letter <= 57))
                    {
                        isAllNumeric = false;
                    }
                }
            }
            return isAllNumeric;
        }
        private void TinsPicker_OkButtonClicked(object sender, PickerSelectionChangedEventArgs e)
        {
            try
            {
                //TODO
                TIN SelectedTin = new TIN { Tin = e.NewValue.ToString() };
                viewModel.SelectedTinId = SelectedTin;
                viewModel.SelectedTinIdPrev = SelectedTin;
            }
            catch (Exception)
            {


            }
        }
        private void TinsPicker_CancelButtonClicked(object sender, PickerSelectionChangedEventArgs e)
        {
            viewModel.SelectedTinId = viewModel.SelectedTinIdPrev;
        }

        private void BackButtonClicked(object sender, EventArgs e)
        {
            viewModel._navigationService.GoBack();
        }

        private void HamburgerMenuClicked(object sender, EventArgs e)
        {
            viewModel._navigationService.GoBack();
        }

        void hybridWebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
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
    }
}