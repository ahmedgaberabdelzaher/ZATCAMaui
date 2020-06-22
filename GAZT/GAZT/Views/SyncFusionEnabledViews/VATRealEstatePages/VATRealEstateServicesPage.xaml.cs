using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATRealEstatePage;
using GAZT.Models;
using Microsoft.AppCenter.Analytics;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.SyncFusionEnabledViews.VATRealEstatePages
{
    public partial class VATRealEstateServicesPage : ContentPage
    {
        VATRealEstateServicesPageViewModel viewModel;

        public VATRealEstateServicesPage()
        {
            InitializeComponent();
       
             On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel = App.Locator.VATRealEstateServicesPage;

            LoadData();
            ChangeAeroIcon();

            SetLTR();
        }
        private void SetLTR()
        {
            try
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
            catch (Exception gec)
            {
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
        private void LoadData()
        {
            viewModel.PopulateServicesData();

        }
        private async void OnTappedTest(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = false;
            });
            string controltype = sender.GetType().ToString();
            if (controltype == "Xamarin.Forms.Image")
            {
                try
                {
                    Image arrowImage = sender as Image;
                    eServiceInfo BModel = (eServiceInfo)arrowImage.BindingContext;
          
                    if (BModel != null && BModel.eServiceName == AppResources.ZVATRealEstatePropertyRegistration)
                    {
                        viewModel._navigationService.NavigateTo(App.PropertyRegistrationPage);
                    }
                    if (BModel != null && BModel.eServiceName == AppResources.ZVATRealEstateRequestVerification)
                    {
                        viewModel._navigationService.NavigateTo(App.PropertyRegistrationPage);

                    }
                    if (BModel != null && BModel.eServiceName == AppResources.ZVATRealEstateTerminationOfRequest)
                    {
                        viewModel._navigationService.NavigateTo(App.PropertyRegistrationPage);

                    }
                }
                catch (Exception ex)
                {
                }
            }
            if (controltype == "Xamarin.Forms.Label")
            {
                try
                {
                    Label arrowImage = sender as Label;
                    eServiceInfo BModel = (eServiceInfo)arrowImage.BindingContext;
              
                    if (BModel != null && BModel.eServiceName == AppResources.ZVATRealEstatePropertyRegistration)
                    {
                        viewModel._navigationService.NavigateTo(App.PropertyRegistrationPage);

                    }
                    if (BModel != null && BModel.eServiceName == AppResources.ZVATRealEstateRequestVerification)
                    {
                        viewModel._navigationService.NavigateTo(App.PropertyRegistrationPage);

                    }
                    if (BModel != null && BModel.eServiceName == AppResources.ZVATRealEstateTerminationOfRequest)
                    {
                        viewModel._navigationService.NavigateTo(App.PropertyRegistrationPage);

                    }
                }
                catch (Exception ex)
                {
                }
            }
            if (controltype == "Xamarin.Forms.StackLayout")
            {
                try
                {
                    Label arrowImage = sender as Label;
                    eServiceInfo BModel = (eServiceInfo)arrowImage.BindingContext;
                    if (BModel != null && BModel.eServiceName == AppResources.ZVATRealEstatePropertyRegistration)
                    {
                        viewModel._navigationService.NavigateTo(App.PropertyRegistrationPage);

                    }
                    if (BModel != null && BModel.eServiceName == AppResources.ZVATRealEstateRequestVerification)
                    {
                        viewModel._navigationService.NavigateTo(App.PropertyRegistrationPage);

                    }
                    if (BModel != null && BModel.eServiceName == AppResources.ZVATRealEstateTerminationOfRequest)
                    {
                        viewModel._navigationService.NavigateTo(App.PropertyRegistrationPage);

                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = false;
            });
        }
    }
}
