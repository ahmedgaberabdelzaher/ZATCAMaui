using EGAZT.ViewModel.NewDesignViewModel.TaxEvasionViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.TAXEvasionPages
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxEvasionPageWebView : ContentPage
    {
        TaxEvasionPageWebViewModel viewModel;
        public TaxEvasionPageWebView()
        {
            InitializeComponent();
            viewModel = App.Locator.TaxEvasionPageWebView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            ChangeAeroIcon();
            SetLTR();
            //  SetLanguage();
            loadingIndicator.IsVisible = true;
            SetLanguage();
        }

        void SetLanguage()
        {
            string Url = "";
            if (App.IsArabic)
            {
               Url = "https://zatca.gov.sa/ar/ContactUs/Pages/ReportFraudMV.aspx";
           // Url = "https://stgextportal.gazt.gov.sa/ar/ContactUs/Pages/ReportFraudMVV1.aspx";

            }
            else
            {
              Url = "https://zatca.gov.sa/en/ContactUs/Pages/ReportFraudMV.aspx";
             //Url  = "https://stgextportal.gazt.gov.sa/en/ContactUs/Pages/ReportFraudMVV1.aspx";
            }
            taxEvasionWebView.Source = Url;
           // taxEvasionHybridWebView.Source = Url;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;
        }

        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
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
        private void TOnBackButtonClicked(object sender, EventArgs e)
        {
            if (taxEvasionWebView.CanGoBack)
            {
                taxEvasionWebView.GoBack();
                return;
            }
            viewModel._navigationService.GoBack();
        }

        private void taxEvasionWebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            
            loadingIndicator.IsVisible = false;
        }

        private void taxEvasionWebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            /*if (e.Url=="https://stgextportal.gazt.gov.sa/ar/ContactUs/Pages/ReportFraudMVV1.aspx")
            {
                loadingIndicator.IsVisible = true;
                return;
            }
            loadingIndicator.IsVisible = false;*/
            loadingIndicator.IsVisible = true;
        }

        void taxEvasionHybridWebView_Navigated(System.Object sender, Xamarin.Forms.WebNavigatedEventArgs e)
        {
            loadingIndicator.IsVisible = false;
        }

        void taxEvasionHybridWebView_Navigating(System.Object sender, Xamarin.Forms.WebNavigatingEventArgs e)
        {
            if (e.Url == "https://stgextportal.gazt.gov.sa/ar/ContactUs/Pages/ReportFraudMVV1.aspx")
            {
                loadingIndicator.IsVisible =false ;
                return;
            }
            loadingIndicator.IsVisible = false;
        }
    }
}