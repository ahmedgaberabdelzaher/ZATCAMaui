using System.Globalization;
using ZATCAMAUI.ViewModel.NewDesignViewModel.Template;

namespace ZATCAMAUI.Views.NewDesign.Template
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardAnonymousMenuPageView1 : ContentPage
    {
        DashboardAnonymousMenuPageViewModel viewModel;
        public DashboardAnonymousMenuPageView1()
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
            }
        }

        private void ChangeLanguage_Tapped(object sender, EventArgs e)
        {
            if (App.IsArabic)
            {
                App.IsArabic = false;
                App.changeFontFamily(App.appObj);
                SetLTRDirection();
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
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
            }
            catch (Exception)
            {

            }
        }

        private void OnTaxEvasionTapped(object sender, EventArgs e)
        {
            viewModel._navigationService.NavigateTo(App.TaxEvasionVerifyMobileNumberPage);
        }

        private void OnSupportTapped(object sender, EventArgs e)
        {
            viewModel._navigationService.NavigateTo(App.SupportPageView);
        }

        private void OnVATRefundTapped(object sender, EventArgs e)
        {
            viewModel._navigationService.NavigateTo(App.VATLookUpNewPageView);
        }
        private void PrivacyPolicy_Tapped(object sender, EventArgs e)
        {
            viewModel._navigationService.NavigateTo(App.PrivacyAndPolicyPageView);
        }
        private void Aboutus_Tapped(object sender, EventArgs e)
        {
            viewModel._navigationService.NavigateTo(App.AboutUsPageView);
        }
        private void GoBackTapped(object sender, EventArgs e)
        {
            viewModel._navigationService.GoBack();
        }

    }
}