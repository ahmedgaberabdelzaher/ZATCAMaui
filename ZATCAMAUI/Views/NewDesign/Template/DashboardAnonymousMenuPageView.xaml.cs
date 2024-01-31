using System.Globalization;
using ZATCAMAUI.Core.AppConfigurations;
using ZATCAMAUI.ViewModel.NewDesignViewModel.Template;

namespace ZATCAMAUI.Views.NewDesign.Template
{
   
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardAnonymousMenuPageView : ContentPage
    {
        DashboardAnonymousMenuPageViewModel viewModel;
        public DashboardAnonymousMenuPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.DashboardAnonymousMenuPageView;
            BindingContext = viewModel;
            SetLTR();
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                FlowDirection = FlowDirection.RightToLeft;
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
        }

        private void GoBackTapped(object sender, EventArgs e)
        {
            viewModel._navigationService.GoBack();
        }

        private void ChangeLanguage_Tapped(object sender, EventArgs e)
        {
            if (App.IsArabic)
            {
                App.IsArabic = false;
                App.changeFontFamily(App.appObj);
                //SetLTRDirection();
                var vUpdatedPage = new DashboardAnonymousMenuPageView();
                Navigation.InsertPageBefore(vUpdatedPage, this);
                Navigation.PopAsync();
            }
            else
            {
                App.IsArabic = true;
                App.changeFontFamily(App.appObj);
                SetRTLDirection();
                var vUpdatedPage = new DashboardAnonymousMenuPageView();
                Navigation.InsertPageBefore(vUpdatedPage, this);
                Navigation.PopAsync();
            }
        }

        public void SetRTLDirection()
        {
            try
            {
                string langName = "ar-AE";
                CultureInfo ci = new CultureInfo(langName);
                AppResources.Culture = ci;
                // InitializeComponent();
                FlowDirection = FlowDirection.RightToLeft;
            }
            catch (Exception)
            {


            }

        }


        public void SetLTRDirection()
        {
            try
            {
                string langName = "en-US";
                CultureInfo ci = new CultureInfo(langName);
                AppResources.Culture = ci;
                //InitializeComponent();
                FlowDirection = FlowDirection.LeftToRight;
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            catch (Exception)
            {


            }
        }
        private void OnTaxEvasionTapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("DashboardAnonymousMenuPageView", "OnTaxEvasionTapped", "Tax Evasion eService");
            //  viewModel._navigationService.NavigateTo(App.TaxEvasionVerifyMobileNumberPage);
            //viewModel._navigationService.NavigateTo(App.TaxEvasionPageWebView);
            if (PageSettings.IsIncludeBalagh)
            {
                //viewModel._navigationService.NavigateTo("ReportsPage");
                viewModel._navigationService.NavigateTo("InquiryAboutAddOrShowReportsPage");
            }
            else
            {
                viewModel._navigationService.NavigateTo(App.TaxEvasionPageWebView);
            }
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private void OnZatcaInfoMenuTapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("DashboardAnonymousMenuPageView", "OnZatcaInfoMenuTapped", "Zatca Info menu eService");
            viewModel._navigationService.NavigateTo(App.ZatcaInfoMenuPageView);
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private void OnSupportTapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("DashboardAnonymousMenuPageView", "OnSupportTapped", "Support eService");
            viewModel._navigationService.NavigateTo(App.SupportPageView);
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private void OnVATRefundTapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("DashboardAnonymousMenuPageView", "OnVATRegistrationVerificationTapped", "VAT Registration Verification eService");
            viewModel._navigationService.NavigateTo(App.VATLookUpNewPageView);
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);

        }
        private void Aboutus_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("DashboardAnonymousMenuPageView", "Aboutus_Tapped", "About Us");
            viewModel._navigationService.NavigateTo(App.AboutUsPageView);
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private void PrivacyPolicy_Tapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("DashboardAnonymousMenuPageView", "PrivacyPolicy_Tapped", "Privacy Policy");
            viewModel._navigationService.NavigateTo(App.PrivacyAndPolicyPageView);
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

        private void OnEduLinkTapped(object sender, EventArgs e)
        {

            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("DashboardAnonymousMenuPageView", "EduLink_Tapped", "Education Link");
            //  viewModel._navigationService.NavigateTo(App.PrivacyAndPolicyPageView);
            Uri uri = new Uri("https://edujourneys.zatca.gov.sa/home/tracks");
            OpenBrowser(uri);
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }
        public async void OpenBrowser(Uri uri)
        {
            await Launcher.OpenAsync(uri);
        }

        private void SwitchUser_Tapped(object sender, EventArgs e)
        {
            App.isAndroidRefresh = false;//#CR2068
            viewModel._navigationService.GoBack();
            // App.isAndroidRefresh = false;//#CR2068
        }

        void GoToExisTax(object sender, EventArgs e)
        {

            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("DashboardAnonymousMenuPageView", "GoToExisTax_Tapped", "Excise Tax");
            viewModel._navigationService.NavigateTo("ExciseTax");
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);


        }

        void GoToTahqaqService(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("DashboardAnonymousMenuPageView", "GoToTahqaqService_Tapped", "Tahqaq Service");
            viewModel._navigationService.NavigateTo("TahqaqScanPage");
            AppDynamics.Agent.Instrumentation.EndCall(callTracker);
        }

    }
}