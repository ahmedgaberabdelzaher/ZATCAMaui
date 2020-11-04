using EGAZT.ViewModel.NewDesignViewModel.TaxEvasionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.TAXEvasionPages
{
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
            SetLanguage();
            loadingIndicator.IsVisible = true;
        }

        void SetLanguage()
        {
            if (App.IsArabic)
            {
                taxEvasionWebView.Source = string.Format("https://gazt.gov.sa/{0}/ContactUs/Pages/ReportFraud.aspx", "ar");
            }
            else
            {
                taxEvasionWebView.Source = string.Format("https://gazt.gov.sa/{0}/ContactUs/Pages/ReportFraud.aspx", "en");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetLanguage();
        }

        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
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
            viewModel._navigationService.GoBack();
        }

        private void taxEvasionWebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            loadingIndicator.IsVisible = false;
        }
    }
}