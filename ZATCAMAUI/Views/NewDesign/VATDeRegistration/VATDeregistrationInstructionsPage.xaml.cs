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
            SetLTR();
            viewModel.IsInstructionChecked = InstructionChecked;
            viewModel.isInstructionCheckedEnable = !InstructionChecked;
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {

                this.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {

                this.FlowDirection = FlowDirection.RightToLeft;

            }
        }
        protected override void OnAppearing()
        {
        }
    }
}
