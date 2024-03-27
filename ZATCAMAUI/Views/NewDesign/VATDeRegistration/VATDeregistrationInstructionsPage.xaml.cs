using RGPopup.Maui.Pages;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.Views.NewDesign.VATDeRegistration
{

    public partial class VATDeregistrationInstructionsPage : PopupPage
    {
        VATDeRegistrationInstructionsPageViewModel viewModel;
        public VATDeregistrationInstructionsPage(bool InstructionChecked)
        {
            InitializeComponent();

            viewModel = App.Locator.VATDeregistrationInstructionsPage;
            this.BindingContext = viewModel;
            viewModel.IsInstructionChecked = InstructionChecked;
            viewModel.isInstructionCheckedEnable = !InstructionChecked;
        }
        protected override void OnAppearing()
        {
        }
    }
}
