using EGAZT.CustomControl;
using EGAZT.Enums;
using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.SFAnonymousLandingPage_ViewModel;
using GAZT.CustomControl;
using GAZT.Helper;
using GAZT.Models;
using Plugin.Connectivity;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
namespace EGAZT.Views.SyncFusionEnabledViews.SFAnonymousLanding
{
    /// <summary>
    /// Page to show the article tile
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SFAnonymousLandingPageView : ContentPage
    {
        SFAnonymousLandingPageViewModel viewModel;
        //  Calendar appointments;
        /// <summary>
        /// Initializes a new instance of the <see cref="SFAnonymousLandingPageView" /> class.
        /// </summary>
        public SFAnonymousLandingPageView()
        {
            try
            {
                InitializeComponent();

                if (Device.RuntimePlatform == Device.Android)
                {
                    GreenHeader.Margin = new Thickness(0, 0, 0, 0);
                    HamberLabel.Padding = new Thickness(4, 0, 0, 0);
                    LogoStackLayout.Padding = new Thickness(0, 0, 0, 0);
                }
                else
                {
                    GreenHeader.Margin = new Thickness(0, -50, 0, 0);
                    HamberLabel.Padding = new Thickness(5, 80, 5, 0);
                    LogoStackLayout.Padding = new Thickness(0, 50, 0, 0);
                }


                //A On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSa(true);

                viewModel = App.Locator.SFAnonymousLandingPageView;
                this.BindingContext = viewModel;
                if (!App.IsJailBrokenDevice)
                {
                    DependencyService.Get<IStatusBar>().HideStatusBar();
                    Changecornerradious();
                    LoadDate();
                    LoadData();
                    SetLTR();
                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await viewModel._dialogService.ShowMessage(AppResources.YourDeviceDoesNotPassTheGAZTSecurityCheck, AppResources.ZError);
                    });
                }

                //  DeviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;
            }
            catch (Exception ex)
            {
            }
        }

        //private void AddMarkers()
        //{
        //    Position loc1 = new Position(17.430486, 78.341331);
        //    Position loc2 = new Position(17.427579, 78.342017);

        //    Pin marker1 = new Pin()
        //    {
        //        Address = "Gachibowli",
        //        IsVisible = true,
        //        Label = "Microsoft Hyderabad",
        //        Position = loc1,
        //        Type = PinType.Place

        //    };
        //    Pin marker2 = new Pin()
        //    {
        //        Address = "Gachibowli",
        //        IsVisible = true,
        //        Label = "Wipro Hyderabad",
        //        Position = loc2,
        //        Type = PinType.Place

        //    };

        //    mapView.Pins.Add(marker1);
        //    mapView.Pins.Add(marker2);
        //    mapView.MoveToRegion(MapSpan.FromCenterAndRadius(marker1.Position, Distance.FromMeters(1000)));
        //}

        //void OnMainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        //{
        //    // Process changes
        //    var displayInfo = e.DisplayInfo;
        //    if(displayInfo.Orientation.Equals("Landscape"))
        //    {
        //    }
        //}
        private void Changecornerradious()
        {
            if (App.IsArabic)
            {
                border.CornerRadius = new Thickness(0, 0, 0, 40);
            }
            else
            {
                border.CornerRadius = new Thickness(0, 0, 40, 0);
            }
        }
        private void LoadDate()
        {
            String Year = DateTime.Now.Year.ToString();
            //String textWithDate = AppResources.ZZZCopyrightText.Replace("2020", Year);
            String textWithDate = AppResources.ZZZCopyrightTextNew.Replace("2020", Year);
            viewModel.CopyRightText = textWithDate;
        }
        private void LoadData()
        {
            try
            {
                viewModel.PopulateeServicesApplicableToTheTaxPayer();
            }
            catch (Exception ex)
            {
            }
        }
        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                
                if (!App.IsJailBrokenDevice)// Checking Jail Broken Device
                {
                    int transitionCount = 0;
                    try
                    {

                        MessagingCenter.Subscribe<SFAnonymousLandingPageViewModel, TransitionType>(this, AppSettings.TransitionMessage, (sender, arg) =>
                        {
                            if (transitionCount == 0)
                            {
                                transitionCount++;
                                var transitionType = (TransitionType)arg;
                                var transitionNavigationPage = Parent as CustomNavigation;

                                if (transitionNavigationPage != null)
                                {
                                    transitionNavigationPage.TransitionType = transitionType;
                                    ComingToOptionScreenFrom comingToOptionScreenFrom = ComingToOptionScreenFrom.IsAnonymousPage;
                                    viewModel._navigationService.NavigateTo(App.SFOptionsPageView, comingToOptionScreenFrom);

                                }
                            }
                            else
                            {

                            }

                        });
                    }
                    catch (Exception ex)
                    {

                    }

                    //var safeInsets = On<iOS>().SafeAreaInsets();
                    //safeInsets.Top = 20;
                    //Padding = safeInsets;

                    if (App.IsSessionExpired)
                    {
                        await viewModel._dialogService.ShowMessage(AppResources.ZYourSessionhasexpiredPleaseLoginagain, AppResources.Information);
                        App.IsSessionExpired = false;
                    }
                    else
                    {

                    }

                    if (App.IsLogOut)
                    {
                        App.IsLogOut = false;
                        var existingPages = Navigation.NavigationStack.ToList();

                        foreach (var page in existingPages)
                        {
                            if (page.GetType().Name != App.SFAnonymousLandingPageView)
                            {
                                Navigation.RemovePage(page);
                            }

                        }
                    }
                    SetLTR();
                    Changecornerradious();
                    InitializeComponent();
                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await viewModel._dialogService.ShowMessage(AppResources.YourDeviceDoesNotPassTheGAZTSecurityCheck, AppResources.ZError);
                    });
                }


            }
            catch (Exception ex)
            {
            }
        }
        private void SetLTR()
        {
            if (App.IsArabic)
            {
                //switch (Xamarin.Forms.Device.RuntimePlatform)
                //{

                //    case Xamarin.Forms.Device.iOS:
                //        LandingCreateAccountNote.LineHeight = .6;
                //        break;
                //    case Xamarin.Forms.Device.Android:
                //        LandingCreateAccountNote.LineHeight = 1.25;
                //        break;
                //}

                this.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        private void SignIn_Clicked(object sender, EventArgs e)
        {
            if (!App.IsJailBrokenDevice)
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "SignIn_Tapped", "Login Tapped");
                    viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.SFLandingPageView);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        viewModel._dialogService.ShowMessage(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                    });
                }
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await viewModel._dialogService.ShowMessage(AppResources.YourDeviceDoesNotPassTheGAZTSecurityCheck, AppResources.ZError);
                });
            }


        }

        private void OnTappedeService(object sender, EventArgs e)
        {
            string controltype = sender.GetType().ToString();
            if (controltype == "Xamarin.Forms.Image")
            {
                Image arrowImage = sender as Image;
                eServiceInfo BModel = (eServiceInfo)arrowImage.BindingContext;
                //if (BModel.eServiceName == AppResources.VATDeclaration)
                //{
                //    viewModel._navigationService.NavigateTo(App.SFLoginPageView,App.ICRListPageView);
                //}
                //if (BModel.eServiceName == AppResources.EstimateZakat)
                //{
                //    viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.ZakatReturnListPageView);
                //}
                //if (BModel.eServiceName == AppResources.ZZFormBundleStatus)
                //{
                //    viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.FormBundleStatusPageView);
                //}
                //if (BModel.eServiceName == AppResources.MyCertificate)
                //{
                //    viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.MyCertificate);
                //}
                //if (BModel.eServiceName == AppResources.ZTINStatus)
                //{
                //    viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.CheckTINStatusPageView);
                //}
                //if (BModel.eServiceName == AppResources.ZZCorrespondence)
                //{
                //    viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.CorrespondancePageView);
                //}
                //if (BModel.eServiceName == AppResources.MyBills)
                //{
                //    viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.MyBillsView);
                //}
                //if (BModel.eServiceName == AppResources.ZZZTaxpayerServices)
                //{
                //    viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.SFLandingPageView);
                //}
                if (BModel.eServiceName == AppResources.ZRealEstateServiceTitle)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", "Real Estate Service Tapped");
                    viewModel._navigationService.NavigateTo(App.GAZTNewDesignForgotPasswordPageView);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                }
                if (BModel.eServiceName == AppResources.ZTEReportReportScreenTitle)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", "Tax Evasion Service Tapped");
                    viewModel._navigationService.NavigateTo(App.TaxEvasionReportMobilePageView);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                }
                //  if (BModel.eServiceName == AppResources.VATLookup)
                if (BModel.eServiceName == AppResources.ZZZVatLookUpTitleTextNew)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", "VAT Look Up Service Tapped");
                    viewModel._navigationService.NavigateTo(App.VATLookupPageView);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                }
            }
            if (controltype == "Xamarin.Forms.Label")
            {
                Label arrowImage = sender as Label;
                eServiceInfo BModel = (eServiceInfo)arrowImage.BindingContext;
                //if (BModel.eServiceName == AppResources.VATDeclaration)
                //{
                //    viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.ICRListPageView);
                //}
                //if (BModel.eServiceName == AppResources.EstimateZakat)
                //{
                //    viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.ZakatReturnListPageView);
                //}
                //if (BModel.eServiceName == AppResources.ZZFormBundleStatus)
                //{
                //    viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.FormBundleStatusPageView);
                //}
                //if (BModel.eServiceName == AppResources.MyCertificate)
                //{
                //    viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.MyCertificate);
                //}
                //if (BModel.eServiceName == AppResources.ZTINStatus)
                //{
                //    viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.CheckTINStatusPageView);
                //}
                //if (BModel.eServiceName == AppResources.ZZCorrespondence)
                //{
                //    viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.CorrespondancePageView);
                //}
                //if (BModel.eServiceName == AppResources.MyBills)
                //{
                //    viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.MyBillsView);
                //}
                //if (BModel.eServiceName == AppResources.ZZZTaxpayerServices)
                //{
                //    viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.SFLandingPageView);
                //}
                if (BModel.eServiceName == AppResources.ZRealEstateServiceTitle)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", "Real Estate Service Tapped");
                    viewModel._navigationService.NavigateTo(App.GAZTNewDesignForgotPasswordPageView);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);

                }
                if (BModel.eServiceName == AppResources.ZTEReportReportScreenTitle)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", "Tax Evasion Service Tapped");
                    viewModel._navigationService.NavigateTo(App.TaxEvasionReportMobilePageView);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);

                }
                //if (BModel.eServiceName == AppResources.VATLookup)
                if (BModel.eServiceName == AppResources.ZZZVatLookUpTitleTextNew)
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnEServiceTapped", "VAT Look Up Service Tapped");
                    viewModel._navigationService.NavigateTo(App.VATLookupPageView);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);

                }
            }
        }
        private void OptionMenuClicked(object sender, EventArgs e)
        {
            if (!App.IsJailBrokenDevice)
            {

                ComingToOptionScreenFrom comingToOptionScreenFrom = ComingToOptionScreenFrom.IsAnonymousPage;
                viewModel._navigationService.NavigateTo(App.SFOptionsPageView, comingToOptionScreenFrom);

            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await viewModel._dialogService.ShowMessage(AppResources.YourDeviceDoesNotPassTheGAZTSecurityCheck, AppResources.ZError);
                });
            }

        }
        private void SignUP_Clicked(object sender, EventArgs e)
        {
            //if (!App.IsJailBrokenDevice)
            //{
            //    viewModel._navigationService.NavigateTo(App.SignUpTAndCViewPage);
            //}
            //else
            //{
            //    Device.BeginInvokeOnMainThread(async () =>
            //    {
            //        await viewModel._dialogService.ShowMessage(AppResources.YourDeviceDoesNotPassTheGAZTSecurityCheck, AppResources.ZError);
            //    });
            //}


            if (!App.IsJailBrokenDevice)
            {
                var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "SignUp_Tapped", "Signup Tapped");
                viewModel._navigationService.NavigateTo(App.VATIndividualSignupPageView);
                AppDynamics.Agent.Instrumentation.EndCall(callTracker);
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await viewModel._dialogService.ShowMessage(AppResources.YourDeviceDoesNotPassTheGAZTSecurityCheck, AppResources.ZError);
                });
            }
        }


        private void OnVATRegistrationClicked(object sender, EventArgs e)
        {
            if (!App.IsJailBrokenDevice)
            {
                var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("SFAnonymousLandingPageView", "OnVATRegistration_Tapped", "VAT Registration Tapped");
                viewModel._navigationService.NavigateTo(App.VATIndividualSignupPageView);
                AppDynamics.Agent.Instrumentation.EndCall(callTracker);
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await viewModel._dialogService.ShowMessage(AppResources.YourDeviceDoesNotPassTheGAZTSecurityCheck, AppResources.ZError);
                });
            }
        }


        private void OnGaztLinkClicked(object sender, EventArgs e)
        {
            if (!App.IsJailBrokenDevice)
            {
                if (App.IsArabic)
                {
                    Device.OpenUri(new Uri("https://gazt.gov.sa/ar/pages/default.aspx"));
                }
                else
                {
                    Device.OpenUri(new Uri("https://gazt.gov.sa/en/pages/default.aspx"));
                }
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await viewModel._dialogService.ShowMessage(AppResources.YourDeviceDoesNotPassTheGAZTSecurityCheck, AppResources.ZError);
                });
            }
        }
    }
}