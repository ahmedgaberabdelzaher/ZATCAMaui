using Mopups.Pages;
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
        }

        protected override async void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
            await viewModel.ReloadData();
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
