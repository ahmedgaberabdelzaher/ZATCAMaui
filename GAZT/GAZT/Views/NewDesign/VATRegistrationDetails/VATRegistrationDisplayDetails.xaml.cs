using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EGAZT.ViewModel.NewDesignViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.VATRegistrationDetails
{
    [Preserve(AllMembers = true)]
    public partial class VATRegistrationDisplayDetails : ContentPage
    {
        VATRegistrationDisplayDetailsPageViewModel viewModel;
        public VATRegistrationDisplayDetails()
        {
            InitializeComponent();
            ChangeAeroIcon();
            viewModel = App.Locator.VATRegistrationDisplayDetails;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            SetLTR();
            Task.Run(async () =>
            {
                //viewModel.IsLoading = true;
                await GetVatRegistrationData();
            });
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

        public async Task GetVatRegistrationData()
        {
            try
            {
                await Task.Run(() =>
                {
                    //viewModel.IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    await viewModel.onPageLoad();
                });
                //await Task.Run(() =>
                //{
                //    viewModel.IsLoading = false;
                //});
            }
            catch (Exception)
            {
                
                
            }
        }
    }
}
