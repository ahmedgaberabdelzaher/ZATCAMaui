using ZATCAMAUI.ViewModel.NewDesignViewModel.CustomServicesViewModels;

namespace ZATCAMAUI.Views.NewDesign.CustomServicesPages.Transaction_Reception
{
    public partial class TransactionReceptionView : BaseContentPage
    {
        TransactionReceptionViewModel viewModel;
        object payload;

        public TransactionReceptionView(object payload)
        {
            viewModel = App.Locator.TransactionReceptionViewModel;
            BindingContext = viewModel;
            this.payload = payload;
            viewModel.SetUserData(payload);
            InitializeComponent();
        }
    }
}

