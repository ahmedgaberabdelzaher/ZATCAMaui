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

        public PropertyRegistrationPage()
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
                if(PropertyRegistrationPageViewModel.reServiceName == AppResources.ZVATRealEstatePropertyRegistration)
                {
                    viewModel.HeaderTitle = AppResources.ZVATRealEstatePropertyRegistration;
                    viewModel.WebUrl = "http://10.50.14.186/ar/eServices/Pages/NewMojcertificateMV.aspx";
                }
                if (PropertyRegistrationPageViewModel.reServiceName == AppResources.ZVATRealEstateRequestVerification)
                {

                    viewModel.HeaderTitle = AppResources.ZVATRealEstateRequestVerification;

                    viewModel.WebUrl = "http://10.50.14.186/ar/eServices/Pages/MOJVerifyMV.aspx";
                }
                if (PropertyRegistrationPageViewModel.reServiceName == AppResources.ZVATRealEstateTerminationOfRequest)
                {

                    viewModel.HeaderTitle = AppResources.ZVATRealEstateTerminationOfRequest;

                    viewModel.WebUrl = "http://10.50.14.186/ar/eServices/Pages/MOJCancelMV.aspx";
                }


               
            }
            else
            {
                if (PropertyRegistrationPageViewModel.reServiceName == AppResources.ZVATRealEstatePropertyRegistration)
                {
                    viewModel.HeaderTitle = AppResources.ZVATRealEstatePropertyRegistration;

                    viewModel.WebUrl = "http://10.50.14.186/en/eServices/Pages/NewMojcertificateMV.aspx";
                }
                if (PropertyRegistrationPageViewModel.reServiceName == AppResources.ZVATRealEstateRequestVerification)
                {
                    viewModel.HeaderTitle = AppResources.ZVATRealEstateRequestVerification;

                    viewModel.WebUrl = "http://10.50.14.186/en/eServices/Pages/MOJVerifyMV.aspx";
                }
                if (PropertyRegistrationPageViewModel.reServiceName == AppResources.ZVATRealEstateTerminationOfRequest)
                {
                    viewModel.HeaderTitle = AppResources.ZVATRealEstateTerminationOfRequest;

                    viewModel.WebUrl = "http://10.50.14.186/en/eServices/Pages/MOJCancelMV.aspx";
                }

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
