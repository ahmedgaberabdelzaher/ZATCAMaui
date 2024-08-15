
using ZATCAMAUI.ViewModel.NewDesignViewModel.CustomServicesViewModels;

namespace ZATCAMAUI.Views.NewDesign.CustomServicesPages
{

    public partial class ExciseTax : ContentPage
    {
        ExciseTaxViewModel viewModel;
        public ExciseTax()
        {
            viewModel = App.Locator.exciseTaxViewModel;
            BindingContext = viewModel;
            InitializeComponent();

        }

    }
}
