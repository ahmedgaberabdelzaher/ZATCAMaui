using RGPopup.Maui.Pages;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EDeclaration;

namespace ZATCAMAUI.Views.NewDesign.EDeclaration.PopUpPages
{
    public partial class EDeclarationTermsPopupPage : PopupPage
    {
        BaseEDeclarationViewModel viewModel;
        public EDeclarationTermsPopupPage()
        {
            InitializeComponent();
            viewModel = App.Locator.BaseEDeclarationViewModel;
            BindingContext = viewModel;
        }
        // Invoked when a hardware back button is pressed
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return false;
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
            return true;
        }
    }
}

