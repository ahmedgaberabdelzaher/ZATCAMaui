using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration;

namespace ZATCAMAUI.Views.NewDesign.ZakatDeregistration
{

    public partial class ZakatRegistrationOutletsDetails : ContentPage
    {
        ZakatRegistrationOutletsDetailsPageViewModel viewModel;

        public ZakatRegistrationOutletsDetails()
        {
            InitializeComponent();
            viewModel = App.Locator.ZakatRegistrationOutletsDetailsPageView;
            On<iOS>().SetUseSafeArea(true);
            BindingContext = viewModel;
            FlowDirection = FlowDirection.LeftToRight;
            ChangeAeroIcon();
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
            Padding = safeInsets;

            await viewModel.LoadDataOutletDetails();
        }
    }
}
