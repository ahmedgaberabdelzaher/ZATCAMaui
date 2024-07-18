using ZATCAMAUI.Core.CustomControls;
using ZATCAMAUI.ViewModel.NewDesignViewModel.CustomServicesViewModels;

namespace ZATCAMAUI.Views.NewDesign.CustomServicesPages
{
    public partial class LaboratoryPaymentOfInsuranceFees :BaseContentPage
    {
        LaboratoryPaymentOfInsuranceFeesViewModel viewModel;
        public LaboratoryPaymentOfInsuranceFees()
        {
            viewModel = App.Locator.LaboratoryPaymentOfInsuranceFeesViewModel;
            BindingContext = viewModel;
            InitializeComponent();
        }

    }
}
