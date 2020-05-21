using EGAZT.ViewModel.SyncFusionEnabledViewModel.ContactUsPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.SyncFusionEnabledViews.ContactUsPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactUsPageView : ContentPage
    {
        ContactUsPageViewModel viewModel;
        public ContactUsPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.ContactUsPageView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            ChangeAeroIcon();
            SetLTR();
            this.BindingContext = viewModel;
            //ContactWebView.Source= "https://gazt.gov.sa/ar/contactus/Pages/default.aspx";
            //"https://gazt.gov.sa/ar/contactus/Pages/default.aspx";
            viewModel.WebUrl = "https://gazt.gov.sa/en/contactus/Pages/default.aspx";

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
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        private void BackButtonClicked(object sender, EventArgs e)
        {
            if (ContactWebView.CanGoBack)
            {
                ContactWebView.GoBack();
            }
            else
            {
                viewModel._navigationService.GoBack();
            }
        }

        private void ContactWebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            viewModel.IsLoading = true;
        }

        private void ContactWebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            viewModel.IsLoading = false;
        }
    }
}