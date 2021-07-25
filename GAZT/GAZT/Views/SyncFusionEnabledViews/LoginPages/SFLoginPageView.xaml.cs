using EGAZT.Enums;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.SFLoginPage_ViewModel;
using EGAZT.Views.NewDesign.ForgotPasswordPages;
using EGAZT.Views.SyncFusionEnabledViews.UnlockAccount;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfPicker.XForms;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Resources;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
namespace EGAZT.Views.SyncFusionEnabledViews.SFLogin
{
    /// <summary>
    /// Page to login with user name and password
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SFLoginPageView
    {
        SFLoginPageViewModel viewModel;
      
        private double width = 0;
        private double height = 0;
        HybridWebView hybridWebView;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage" /> class.
        /// </summary>
        public SFLoginPageView(String strNavigateToThisService)
        {
            // SetLTRDirection();
            try
            {
                InitializeComponent();
                App.VATType = PageExecutionType.Register;
                App.ZAKATType = PageExecutionType.Register;
                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, " ");
                this.BindingContext = viewModel = App.Locator.SFLoginPageView;
                //On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                ChangeAeroIcon();
                CheckFirstTimeorNot();
                GetDeviceID();
                viewModel.NavigateToThisService = strNavigateToThisService;
              

                if (App.IsArabic)
                {
                    this.FlowDirection = FlowDirection.RightToLeft;
                    CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                    PickerResourceManager.Manager = new ResourceManager("EGAZT.SyncfusionControl", Xamarin.Forms.Application.Current.GetType().Assembly);
                }
                else
                {
                   

                    this.FlowDirection = FlowDirection.LeftToRight;
                    CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                    PickerResourceManager.Manager = new ResourceManager("GAZT.AppResources", Xamarin.Forms.Application.Current.GetType().Assembly);
                }


                MessagingCenter.Subscribe<string>(this, "UnlockAccountBackButtonClicked", message =>
                {
                    Console.WriteLine("UnlockAccountBackButtonClicked");
                    OnAppearing();
                });

                MessagingCenter.Subscribe<object, string>(this, "RefreshLoginPage", async (sender, arg) =>
                {
                    Console.WriteLine("RefreshLoginPage");
                    OnAppearing();
                });

                MessagingCenter.Subscribe<string>(this, "OnActivated", message =>
                {
                    Console.WriteLine("OnActivated");
                    OnAppearing();
                });
                MessagingCenter.Subscribe<object, string>(this, "SessionExpired", (sender, arg) =>
                {
                    //var objSession = Xamarin.Forms.Application.Current.Properties["IsSessionExpired"];
                    //if (objSession != null && bool.Parse(objSession.ToString()))
                    //{
                    //    loginGrid.Opacity = 0;
                    //    sessionExpiredView.IsVisible = true;
                    //}
                });

                DependencyService.Get<IStatusBar>().HideStatusBar();

                App.ArePreLoginLangCookiesSet = false;
                App.IsLoginCalled = false;

                Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
                viewModel.TINIndex = 0;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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
                    Console.WriteLine(arg);
                    viewModel.IsLoading = false;
                    GoBackToOnaboardingScreen();
                }
                if (arg == "LoadingFinished")
                {
                    viewModel.IsLoading = false;
                }
            });

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
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }
        public void SetLTRDirection()
        {
            App.IsArabic = false;
            String langName = "en-US";
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
                //viewModel.password = string.Empty;
                //viewModel.email = string.Empty;
                // viewModel.Password = string.Empty;
                // viewModel.Email = string.Empty;
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

                //if (App.IsSessionExpired)
                //{
                //    await viewModel._dialogService.ShowMessageBox(AppResources.ZYourSessionhasexpiredPleaseLoginagain, AppResources.Information);
                //}
                //else
                //{
                //}

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

                if (hybridWebView != null)
                    loginGrid.Children.Remove(hybridWebView);

                hybridWebView = new HybridWebView();

                Device.BeginInvokeOnMainThread(async () =>
                {
                    viewModel.IsLoading = true;
                    hybridWebView.Opacity = 0;
                    var objSession = Xamarin.Forms.Application.Current.Properties.ContainsKey("IsSessionExpired") ? Xamarin.Forms.Application.Current.Properties["IsSessionExpired"] : null;
                    if (objSession != null && bool.Parse(objSession.ToString()))
                    {
                        loginGrid.Opacity = 0;
                        sessionExpiredView.IsVisible = true;
                    }
                });

                //try
                //{
                //    await WebServiceManager.GAZTLogOff();
                //}
                //catch (Exception ex)
                //{

                //}

                hybridWebView.HorizontalOptions = LayoutOptions.FillAndExpand;
                hybridWebView.VerticalOptions = LayoutOptions.FillAndExpand;

                //NSHttpCookie langCookieTemp = new NSHttpCookie(GAZT.Helper.Constants.LanguageCookieNameForLogin, langVal, "/", GAZT.Helper.Constants.DomainUrlForCookies);
                //Cookie langCookie = new Cookie(Constants.LanguageCookieNameForLogin, lang, "/", Constants.DomainUrlForCookies);
                //CookieContainer loginWebViewCookieContainer = new CookieContainer();
                //loginWebViewCookieContainer.Add(langCookie);
                //hybridWebView.Cookies = loginWebViewCookieContainer;

                hybridWebView.Url = viewModel.CreateLoginURL(lang);

                hybridWebView.RegisterAction(async (data) =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
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

                            if (data == "requestTimedout")
                            {
                                hybridWebView.Opacity = 0;
                                viewModel.IsLoading = false;

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
                                catch (Exception ex)
                                {
                                    Console.Write(ex.ToString());
                                    Console.Write(ex.StackTrace.ToString());
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
                                            Xamarin.Forms.Application.Current.Properties["timeOut"] = DateTime.Now;
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
                                catch (Exception ex)
                                {
                                    await viewModel._dialogService.ShowMessageBox(AppResources.Somethingwentwrong, AppResources.Information);
                                }
                            }

                            if (data == "navigateToForgotUsernamePage")
                            {
                                hybridWebView.Opacity = 0;
                              //  viewModel._navigationService.NavigateTo(App.GAZTNewDesignForgotPasswordPageView);
                                await Navigation.PushModalAsync(new GAZTNewDesignForgotPasswordPageView(), true);
                                //viewModel._navigationService.NavigateTo(App.ForgotUsernamePasswordPageView);
                            }

                            if (data == "navigateToUnlockAccountPage")
                            {
                                hybridWebView.Opacity = 0;
                                await PopupNavigation.Instance.PushAsync(new UnlockAccountTINPageView());
                            }

                            if (data == "navigateToVATIndividualSignupPage")
                            {
                                hybridWebView.Opacity = 0;
                                //viewModel._navigationService.NavigateTo(App.VATIndividualSignupPageView);
                                viewModel._navigationService.NavigateTo(App.EstablishmentSignUPPageView);
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

                                //await viewModel._dialogService.ShowMessageBox(App.LoginDataRetrieved.AppMsg, App.LoginDataRetrieved.MsgTitle);
                                ////hybridWebView.RefreshCommand();

                                // LogoffUser();
                                //Please complete registration process on Portal to Login into the app.
                                // Test code for implementing the VAT Registration
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

                                //hybridWebView.RefreshCommand();
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
                                    String langName = "ar-AE";
                                    AppResources.Culture = new CultureInfo(langName);
                                }
                                else
                                {
                                    String langName = "en-US";
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

                                var _navigation = Xamarin.Forms.Application.Current.MainPage.Navigation;
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
                                catch (Exception ex)
                                {
                                    Console.Write(ex.ToString());
                                    Console.Write(ex.StackTrace.ToString());
                                }

                                viewModel._navigationService.NavigateTo(App.GAZTNewDesignOnBoardingAnimationPageView);
                                _navigation.NavigationStack.ToList().Clear();

                            }
                        }

                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    });
                });

                loginGrid.Children.Add(hybridWebView, 0, 0);
                loginGrid.LowerChild(hybridWebView);
                var safeInsets = On<Xamarin.Forms.PlatformConfiguration.iOS>().SafeAreaInsets();
                if (Device.RuntimePlatform == Device.iOS && safeInsets.Bottom == 0)
                {
                    loginGrid.Margin = new Thickness(0, -50, 0, -30);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }

        private async Task LogoffUser()
        {
            viewModel.IsLoading = true;

            if (App.TP != null)
                App.TP = null;
            if (App.PreviousIsArabic)
            {
                String langName = "ar-AE";
                AppResources.Culture = new CultureInfo(langName);
            }
            else
            {
                String langName = "en-US";
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

            var _navigation = Xamarin.Forms.Application.Current.MainPage.Navigation;
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
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await viewModel._dialogService.ShowMessageBox(AppResources.Somethingwentwrong, AppResources.Information);
                                GoBackToOnaboardingScreen();
                            });
                        }
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
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
            var _navigation = Xamarin.Forms.Application.Current.MainPage.Navigation;
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
        public static bool IsEnglishNumber(String arText)
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
        private void TinsPicker_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            TIN SelectedTin = (TIN)e.NewValue;
            viewModel.SelectedTinId = SelectedTin;
            viewModel.SelectedTinIdPrev = SelectedTin;
        }
        private void TinsPicker_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
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

        void hybridWebView_Navigating(System.Object sender, Xamarin.Forms.WebNavigatingEventArgs e)
        {
            Console.WriteLine("hybridWebView_Navigating");
        }


        private void GetDeviceID()
        {
            string deviceId = System.Guid.NewGuid().ToString();
            viewModel.DeviceId = deviceId;
        }

        private void btnLoginClicked(object sender, EventArgs e)
        {
            sessionExpiredView.IsVisible = false;
            loginGrid.Opacity = 1;
            App.Current.Properties["IsSessionExpired"] = false;

            OnAppearing();

            App.ResetAndContinueSession();
        }
    }
}