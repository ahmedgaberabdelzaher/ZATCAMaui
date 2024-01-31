using RGPopup.Maui.Pages;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATRefunds;

namespace ZATCAMAUI.Views.NewDesign.VATRefunds
{

    public partial class VATRefundsInstructionsPageView : PopupPage
    {
        VATRefundsInstructionsPageViewModel viewModel;

        public VATRefundsInstructionsPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.VATRefundsInstructionsPageView;
            viewModel.IsInstructionsVisible = true;
            this.BindingContext = viewModel;
            SetLTR();
        }

        protected override async void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
            await viewModel.ReloadData();
        }

        private void SetLTR()
        {
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                btnConfirmVATRefundInstructions.FontSize = 15;
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        public void VATRefundRequestInstructions_Tapped(object sender, EventArgs e)
        {
            try
            {
                viewModel.VATRefundInstructionsConfirmedBtnTapped();
            }
            catch (Exception)
            {


            }
        }
    }
}
