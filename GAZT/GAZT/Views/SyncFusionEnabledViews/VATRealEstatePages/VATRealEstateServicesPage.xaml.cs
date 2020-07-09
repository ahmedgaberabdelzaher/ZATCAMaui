using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATRealEstatePage;
using GAZT.Models;
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
            viewModel = App.Locator.VATRealEstateServicesPage;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            SetLTR();
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
                Image_backArrow.Rotation = 0;
            }
            else
            {
                
                Image_backArrow.Rotation = 180;
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //viewModel.IsLoading = false;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //your code here;
            //viewModel.IsLoading = false;
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
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            viewModel._navigationService.GoBack();
        }
        /* private async void OnTappedTest(object sender, EventArgs e)
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

                     PropertyRegistrationPageViewModel.reServiceName = BModel.eServiceName;

                         viewModel._navigationService.NavigateTo(App.PropertyRegistrationPage);


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
                     PropertyRegistrationPageViewModel.reServiceName = BModel.eServiceName;


                     viewModel._navigationService.NavigateTo(App.PropertyRegistrationPage);


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
                     PropertyRegistrationPageViewModel.reServiceName = BModel.eServiceName;

                     viewModel._navigationService.NavigateTo(App.PropertyRegistrationPage);


                 }
                 catch (Exception ex)
                 {
                 }
             }
         }*/
    
    }
}
