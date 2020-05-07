using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.SFAnonymousLandingPage_ViewModel;
using GAZT.Helper;
using GAZT.Models;
using System;
using System.Globalization;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
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
               //On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                viewModel = App.Locator.SFAnonymousLandingPageView;
                this.BindingContext = viewModel;
                DependencyService.Get<IStatusBar>().HideStatusBar();
                Changecornerradious();
                LoadDate();
                LoadData();
                SetLTR();
              //  DeviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;
            }
            catch (Exception ex)
            {
            }
        }
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
                border.CornerRadius = new Thickness(0,0,0,40);
            }
            else
            {
                border.CornerRadius = new Thickness(0,0,40,0);
            }
        }
        private void LoadDate()
        {
            String Year = DateTime.Now.Year.ToString();
            String textWithDate = AppResources.ZZZCopyrightText.Replace("2020", Year);
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
            catch (Exception ex)
            {
            }
        }
        private void SetLTR()
        {
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        private void SignIn_Clicked(object sender, EventArgs e)
        {
            viewModel._navigationService.NavigateTo(App.SFLoginPageView,App.SFLandingPageView);
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
                if (BModel.eServiceName == AppResources.ZTEReportReportScreenTitle)
                {
                    viewModel._navigationService.NavigateTo(App.TaxEvasionReportMobilePageView);
                }
                if (BModel.eServiceName == AppResources.VATLookup)
                {
                    viewModel._navigationService.NavigateTo(App.VATLookupPageView);
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
                if (BModel.eServiceName == AppResources.ZTEReportReportScreenTitle)
                {
                    viewModel._navigationService.NavigateTo(App.TaxEvasionReportMobilePageView);
                }
                if (BModel.eServiceName == AppResources.VATLookup)
                {
                    viewModel._navigationService.NavigateTo(App.VATLookupPageView);
                }
            }
        }
        private void OptionMenuClicked(object sender, EventArgs e)
        {
            ComingToOptionScreenFrom comingToOptionScreenFrom = ComingToOptionScreenFrom.IsAnonymousPage;
            viewModel._navigationService.NavigateTo(App.SFOptionsPageView, comingToOptionScreenFrom);
        }
        private void SignUP_Clicked(object sender, EventArgs e)
        {
            viewModel._navigationService.NavigateTo(App.SignUpTAndCViewPage);
        }
        private void OnGaztLinkClicked(object sender, EventArgs e)
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
    }
}