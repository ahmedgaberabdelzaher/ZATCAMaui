using EGAZT.ViewModel.SyncFusionEnabledViewModel.SFLoginPage_ViewModel;
using GAZT;
using GAZT.Helper;
using GAZT.Models;
using Syncfusion.SfPicker.XForms;
using System;
using System.Globalization;
using System.Resources;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

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
        private string strNavigaateToThisService;
        private double width = 0;
        private double height = 0;
        HybridWebView hybridWebView;
        CustomLabel backLabel;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage" /> class.
        /// </summary>
        public SFLoginPageView(String strNavigateToThisService)
        {
            // SetLTRDirection();
            try
            {
                InitializeComponent();
                this.BindingContext = viewModel = App.Locator.SFLoginPageView;
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                ChangeAeroIcon();
                GetDeviceID();
                viewModel.NavigateToThisService = strNavigateToThisService;
                string lang = "AR";

                if (App.IsArabic)
                {
                    this.FlowDirection = FlowDirection.RightToLeft;
                    CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                    PickerResourceManager.Manager = new ResourceManager("EGAZT.SyncfusionControl", Xamarin.Forms.Application.Current.GetType().Assembly);
                }
                else
                {
                    lang = "EN";

                    this.FlowDirection = FlowDirection.LeftToRight;
                    CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                    PickerResourceManager.Manager = new ResourceManager("GAZT.AppResources", Xamarin.Forms.Application.Current.GetType().Assembly);
                }
                DependencyService.Get<IStatusBar>().HideStatusBar();

                App.ArePreLoginLangCookiesSet = false;
                App.IsLoginCalled = false;

                Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
                viewModel.TINIndex = 0;
            }
            catch (Exception ex)
            {
            }
            // ParentContainer.RaiseChild(BusyIndicator);
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
                    viewModel._navigationService.GoBack();
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
                    this.BackgroundImageSource = "sf_LoginBackground.png";
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
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                //viewModel.password = string.Empty;
                //viewModel.email = string.Empty;
                // viewModel.Password = string.Empty;
                // viewModel.Email = string.Empty;
                MessagingCenter.Subscribe<string>(this, "TinList", message => {
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
                hybridWebView.HorizontalOptions = LayoutOptions.FillAndExpand;
                hybridWebView.VerticalOptions = LayoutOptions.FillAndExpand;
                hybridWebView.Url = viewModel.CreateLoginURL(lang);

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
                            viewModel._navigationService.GoBack();
                        }

                        if (data == "success")
                        {
                            App.IsUserLoggedIn = true;
                            await viewModel.LoginCompletedInWebView();
                        }

                        if (data == "navigateToForgotUsernamePage")
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                hybridWebView.Opacity = 0;
                                viewModel._navigationService.NavigateTo(App.ForgotUsernamePasswordPageView);
                            });
                        }

                        //rohith changes
                        if (data == "error")
                        {
                            viewModel.IsLoading = false;

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await viewModel._dialogService.ShowMessageBox(App.LoginDataRetrieved.AppMsg, App.LoginDataRetrieved.MsgTitle);
                            });

                            hybridWebView.Opacity = 0;
                            hybridWebView.RefreshCommand();
                        }

                        if (data == "errorGeneric")
                        {
                            viewModel.IsLoading = false;

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await viewModel._dialogService.ShowMessageBox(AppResources.Somethingwentwrong, AppResources.Information);
                            });

                            hybridWebView.Opacity = 0;
                            hybridWebView.RefreshCommand();
                        }
                        //rohith changes
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                });
                hybridWebView.Opacity = 1;

                loginGrid.Children.Add(hybridWebView, 0, 0);
                loginGrid.LowerChild(hybridWebView);
            }
            catch (Exception ex)
            {

            }
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
                            viewModel._navigationService.GoBack();
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


        private void OnPasswordVisibilityClicked(object sender, EventArgs e)
        {
            viewModel.PasswordVisibility = !viewModel.PasswordVisibility;
        }
        private void onBackButtonClicked(object sender, EventArgs e)
        {
        }
        //private void TINs_Clicked(object sender, System.EventArgs e)
        //{
        //    TinsPicker.IsOpen = true;
        //}
        //private void Email_UnFocused(object sender, Xamarin.Forms.FocusEventArgs e)
        //{
        //    bool isNumber = false;
        //    bool isEmailValid = false;
        //    isNumber = IsEnglishNumber(Email.Text);
        //    if (!isNumber)
        //    {
        //        isEmailValid = CheckValidEmail(Email.Text);
        //        if (!isEmailValid)
        //        {
        //            EmailInputLayout.HasError = true;
        //            //EmailInputLayout.ShowHint = true;
        //        }
        //        else
        //        {
        //            EmailInputLayout.HasError = false;
        //            viewModel.IsVisibleTinIds = true;
        //        }
        //    }
        //    else
        //    {
        //        EmailInputLayout.HasError = false;
        //        //EmailInputLayout.ShowHint = false;
        //    }
        //}
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

        void hybridWebView_Navigating(System.Object sender, Xamarin.Forms.WebNavigatingEventArgs e)
        {
            Console.WriteLine("hybridWebView_Navigating");
        }


        private void GetDeviceID()
        {
            string deviceId = System.Guid.NewGuid().ToString();
            viewModel.DeviceId = deviceId;
        }
    }
}