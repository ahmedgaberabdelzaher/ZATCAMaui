using Mopups.Pages;
using ZATCAMAUI.ViewModel.NewDesignViewModel.CustomServicesViewModels;

namespace ZATCAMAUI.Views.NewDesign.CustomServicesPages.Transaction_Reception
{
    public partial class NewCrPopupView : PopupPage
    {
        TransactionReceptionViewModel viewModel;

        public NewCrPopupView()
        {
            viewModel = App.Locator.TransactionReceptionViewModel;
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}

