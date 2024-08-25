
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration;

namespace ZATCAMAUI.Views.NewDesign.ZakatDeregistration
{

    public partial class ZakatRegistrationFinancialDetails : ContentPage
    {
        ZakatRegistrationFinancialDetailsPageViewModel viewModel;
        public ZakatRegistrationFinancialDetails()
        {
            InitializeComponent();
            viewModel = App.Locator.ZakatRegistrationFinancialDetailsPageView;
            this.BindingContext = viewModel;
        }

      
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await viewModel.LoadDataFinancialDetails();
        }
    }
}
