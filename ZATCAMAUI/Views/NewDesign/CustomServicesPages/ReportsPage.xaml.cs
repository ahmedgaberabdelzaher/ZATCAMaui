
using ZATCAMAUI.ViewModel.NewDesignViewModel.CustomServicesViewModels;

namespace ZATCAMAUI.Views.NewDesign.CustomServicesPages
{
    public partial class ReportsPage : ContentPage
    {
        ReportsMenuViewModel viewModel;
        public ReportsPage()
        {
            InitializeComponent();
            viewModel = App.Locator.reportsMenuViewModel;
            BindingContext = viewModel;
        }
    }
}
