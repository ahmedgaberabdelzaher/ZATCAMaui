using ZATCAMAUI.ViewModel.NewDesignViewModel.CustomServicesViewModels;

namespace ZATCAMAUI.Views.NewDesign.CustomServicesPages.Transaction_Reception
{
    public partial class SuccessView : BaseContentPage
    {
        TransactionReceptionViewModel viewModel;
        public SuccessView(string refNo)
        {
            viewModel = App.Locator.TransactionReceptionViewModel;
            BindingContext = viewModel;

            InitializeComponent();
            RefNoValue.Text = refNo;
        }

    }
}

