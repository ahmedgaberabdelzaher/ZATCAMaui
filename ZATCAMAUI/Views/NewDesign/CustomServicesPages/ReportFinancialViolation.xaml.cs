using ZATCAMAUI.ViewModel.NewDesignViewModel.CustomServicesViewModels;

namespace ZATCAMAUI.Views.NewDesign.CustomServicesPages
{
    public partial class ReportFinancialViolation : ContentPage
    {
        ReportFinancialViolationViewModel viewModel;
        public ReportFinancialViolation()
        {
            viewModel = App.Locator.reportFinancialViolationViewModel;
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}
