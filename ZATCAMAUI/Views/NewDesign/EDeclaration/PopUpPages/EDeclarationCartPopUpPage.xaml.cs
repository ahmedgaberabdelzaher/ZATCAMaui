using Mopups.Pages;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationProduct;

namespace ZATCAMAUI.Views.NewDesign.EDeclaration.PopUpPages
{
    public partial class EDeclarationCartPopUpPage : PopupPage
    {
        BaseProductDeclarationViewModel viewModel;
        public EDeclarationCartPopUpPage()
        {
            try
            {
                InitializeComponent();
                viewModel = App.Locator.ProductDeclarationViewModel;
                BindingContext = viewModel;
            }
            catch (Exception)
            {

            }
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

