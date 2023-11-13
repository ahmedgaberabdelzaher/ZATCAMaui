using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.ZakatDeregistration
{
    [Preserve(AllMembers = true)]
    public partial class ZakatRegistrationTaxPayerDetails : ContentPage
    {
        ZakatRegistrationTaxPayerDetailsPageViewModel viewModel;

        public ZakatRegistrationTaxPayerDetails()
        {
            InitializeComponent();
            viewModel = App.Locator.ZakatRegistrationTaxPayerDetailsPageView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            this.FlowDirection = FlowDirection.LeftToRight;
            ChangeAeroIcon();
            SetLTR();
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

        public void ChangeAeroIcon()
        {
            try
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
            catch (Exception)
            {
                
                
            }

        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await Task.Run(() =>
            {
                viewModel.isLoading = true;
            });

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;

            await viewModel.LoadDataTaxPayerDetails();

            await Task.Run(() =>
            {
                viewModel.isLoading = false;
            });

        }
    }
}
