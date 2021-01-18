using System;
using System.Collections.Generic;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATRealEstatePage;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.SyncFusionEnabledViews.VATRealEstatePages
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
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

            this.BindingContext = viewModel;

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            SetUrl();
        }

        public void SetUrl()
        {
            if (App.IsArabic)
            {
                if(PropertyRegistrationPageViewModel.reServiceName == AppResources.ZVATRealEstatePropertyRegistration)
                {
                    
                        viewModel.HeaderTitle = AppResources.ZVATRealEstatePropertyRegistration;
            
                    viewModel.WebUrl = "https://gazt.gov.sa/ar/eServices/Pages/NewMojcertificateMV.aspx";
                }
                if (PropertyRegistrationPageViewModel.reServiceName == AppResources.ZVATRealEstateRequestVerification)
                {
                 viewModel.HeaderTitle = AppResources.ZVATRealEstateRequestVerification;
           

                    viewModel.WebUrl = "https://gazt.gov.sa/ar/eServices/Pages/MOJVerifyMV.aspx";
                }
                if (PropertyRegistrationPageViewModel.reServiceName == AppResources.ZVATRealEstateTerminationOfRequest)
                {
                        viewModel.HeaderTitle = AppResources.ZVATRealEstateTerminationOfRequest;
                
                    viewModel.WebUrl = "https://gazt.gov.sa/ar/eServices/Pages/MOJCancelMV.aspx";
                }

            }
            else
            {
                if (PropertyRegistrationPageViewModel.reServiceName == AppResources.ZVATRealEstatePropertyRegistration)
                {
                   
                        viewModel.HeaderTitle = AppResources.ZVATRealEstatePropertyRegistration;

                 
                    viewModel.WebUrl = "http://gazt.gov.sa/en/eServices/Pages/NewMojcertificateMV.aspx";
                }
                if (PropertyRegistrationPageViewModel.reServiceName == AppResources.ZVATRealEstateRequestVerification)
                {
                   
                        viewModel.HeaderTitle = AppResources.ZVATRealEstateRequestVerification;
                   
                    viewModel.WebUrl = "http://gazt.gov.sa/en/eServices/Pages/MOJVerifyMV.aspx";
                }
                if (PropertyRegistrationPageViewModel.reServiceName == AppResources.ZVATRealEstateTerminationOfRequest)
                {
                    
                        viewModel.HeaderTitle = AppResources.ZVATRealEstateTerminationOfRequest;
                    

                    viewModel.WebUrl = "http://gazt.gov.sa/en/eServices/Pages/MOJCancelMV.aspx";
                }

            }
            viewModel.HeaderTitle = string.Empty;

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
