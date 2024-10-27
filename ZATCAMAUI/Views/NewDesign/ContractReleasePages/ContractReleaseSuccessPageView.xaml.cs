using ZATCAMAUI.ViewModel.NewDesignViewModel.ContractRelease;

namespace ZATCAMAUI.Views.NewDesign.ContractReleasePages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContractReleaseSuccessPageView : ContentPage
    {
        public ContractReleaseViewModel viewModel;
        public ContractReleaseSuccessPageView(ContractReleaseViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            BindingContext = viewModel;

            viewModel.ReferenceNumberTxt = viewModel.ContractReleaseData1.d.Fbnumz;
            viewModel.ContractNumberTxt = viewModel.ContractReleaseData1.d.AContNo;
        }

    }
}