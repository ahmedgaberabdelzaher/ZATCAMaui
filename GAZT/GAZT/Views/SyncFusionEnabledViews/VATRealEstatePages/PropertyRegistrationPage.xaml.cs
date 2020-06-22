using System;
using System.Collections.Generic;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATRealEstatePage;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.SyncFusionEnabledViews.VATRealEstatePages
{
    public partial class PropertyRegistrationPage : ContentPage
    {
        PropertyRegistrationPageViewModel viewModel;

        public PropertyRegistrationPage( )
        {
            InitializeComponent();
            viewModel = App.Locator.PropertyRegistrationPage;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            ChangeAeroIcon();
            SetLTR();
            SetUrl();
            this.BindingContext = viewModel;
        }
        public void SetUrl()
        {
            if (App.IsArabic)
            {
                viewModel.WebUrl = "http://stgextportal1/ar/eServices/Pages/NewMojcertificateMV.aspx";
            }
            else
            {
                viewModel.WebUrl = "http://stgextportal1/en/eServices/Pages/NewMojcertificateMV.aspx";
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
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        private void BackButtonClicked(object sender, EventArgs e)
        {
            if (PropertyWebView.CanGoBack)
            {
                PropertyWebView.GoBack();
            }
            else
            {
                viewModel._navigationService.GoBack();
            }
        }

    }
}
