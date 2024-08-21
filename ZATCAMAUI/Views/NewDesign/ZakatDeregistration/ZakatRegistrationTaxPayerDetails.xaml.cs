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
            BindingContext = viewModel;
        }

       


        protected async override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.IsLoading = true;


            await viewModel.LoadDataTaxPayerDetails();

            viewModel.IsLoading = false;

        }
    }
}
