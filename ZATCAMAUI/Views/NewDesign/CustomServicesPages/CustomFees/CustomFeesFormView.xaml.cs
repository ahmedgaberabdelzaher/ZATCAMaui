using ZATCAMAUI.ViewModel.NewDesignViewModel.CustomServicesViewModels;

namespace ZATCAMAUI.Views.NewDesign.CustomServicesPages.CustomFees
{
    public partial class CustomFeesFormView : BaseContentPage
    {
        CustomFeesFormViewModel viewModel;
        public CustomFeesFormView()
        {
            try
            {
                viewModel = App.Locator.CustomFeesFormViewModel;
                BindingContext = viewModel;

                InitializeComponent();
            }
            catch (Exception)
            {

            }

        }
        protected override bool OnBackButtonPressed()
        {
            viewModel.BackMethod();
            return true;
        }
    }
}

