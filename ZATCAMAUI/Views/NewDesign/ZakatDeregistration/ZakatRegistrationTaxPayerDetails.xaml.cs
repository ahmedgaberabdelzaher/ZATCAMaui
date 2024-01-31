using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration;

namespace ZATCAMAUI.Views.NewDesign.ZakatDeregistration
{

    public partial class ZakatRegistrationTaxPayerDetails : ContentPage
    {
        ZakatRegistrationTaxPayerDetailsPageViewModel viewModel;

        public ZakatRegistrationTaxPayerDetails()
        {
            InitializeComponent();
            viewModel = App.Locator.ZakatRegistrationTaxPayerDetailsPageView;
            On<iOS>().SetUseSafeArea(true);
            BindingContext = viewModel;
            FlowDirection = FlowDirection.LeftToRight;
            ChangeAeroIcon();
            SetLTR();
        }

        private void SetLTR()
        {
            if (App.IsArabic)
            {
                FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                FlowDirection = FlowDirection.LeftToRight;
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
            Padding = safeInsets;

            await viewModel.LoadDataTaxPayerDetails();

            await Task.Run(() =>
            {
                viewModel.isLoading = false;
            });

        }
    }
}
