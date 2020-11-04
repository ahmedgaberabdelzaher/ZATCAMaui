using EGAZT.ViewModel.NewDesignViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.Template
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
                this.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
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
                SetLTRDirection();
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
                String langName = "ar-AE";
                CultureInfo ci = new CultureInfo(langName);
                AppResources.Culture = ci;
                // InitializeComponent();
                this.FlowDirection = FlowDirection.RightToLeft;
            }
            catch (Exception e)
            {

            }

        }


        public void SetLTRDirection()
        {
            try
            {
                String langName = "en-US";
                CultureInfo ci = new CultureInfo(langName);
                AppResources.Culture = ci;
                //InitializeComponent();
                this.FlowDirection = FlowDirection.LeftToRight;
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            catch (Exception e)
            {

            }
        }
        private void OnTaxEvasionTapped(object sender, EventArgs e)
        {
            var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("DashboardAnonymousMenuPageView", "OnTaxEvasionTapped", "Tax Evasion eService");
            //  viewModel._navigationService.NavigateTo(App.TaxEvasionVerifyMobileNumberPage);
            viewModel._navigationService.NavigateTo(App.TaxEvasionPageWebView);
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
    }
}