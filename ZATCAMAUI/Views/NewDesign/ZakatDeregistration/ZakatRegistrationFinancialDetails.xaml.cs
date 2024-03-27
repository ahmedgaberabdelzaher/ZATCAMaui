
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration;

namespace ZATCAMAUI.Views.NewDesign.ZakatDeregistration
{

    public partial class ZakatRegistrationFinancialDetails : ContentPage
    {
        ZakatRegistrationFinancialDetailsPageViewModel viewModel;
        public ZakatRegistrationFinancialDetails()
        {
            InitializeComponent();
            ChangeAeroIcon();

            viewModel = App.Locator.ZakatRegistrationFinancialDetailsPageView;
            On<iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
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

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;

            await viewModel.LoadDataFinancialDetails();
        }
    }
}
