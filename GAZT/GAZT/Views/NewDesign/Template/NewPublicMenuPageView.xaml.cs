using EGAZT.ViewModel.NewDesignViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.Template
{
    [Preserve(AllMembers = true)]
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
                this.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
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
            }
            catch (Exception e)
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