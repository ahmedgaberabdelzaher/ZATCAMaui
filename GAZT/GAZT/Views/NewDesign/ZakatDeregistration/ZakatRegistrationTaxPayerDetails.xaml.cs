using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.ZakatDeregistration
{
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
                    Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
                    Resources["ImageReverse"] = Resources["ArrowImageForArabicStyle"];
                }
                else
                {
                    Resources["StyleReverseBack"] = App.Current.Resources["Back"];
                    Resources["ImageReverse"] = Resources["ArrowImageForEnglishStyle"];
                }

            }
            catch (Exception ex)
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

            
            await viewModel.LoadDataTaxPayerDetails();


            await Task.Run(() =>
            {
                viewModel.isLoading = false;
            });

        }
    }
}
