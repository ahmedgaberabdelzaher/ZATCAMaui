
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
            BindingContext = viewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await viewModel.LoadDataOutletDetails();
        }
    }
}
