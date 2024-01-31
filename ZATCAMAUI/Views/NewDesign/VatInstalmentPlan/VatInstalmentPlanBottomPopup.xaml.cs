using RGPopup.Maui.Pages;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATInstalmentPlanViewModel;

namespace ZATCAMAUI.Views.NewDesign.VatInstalmentPlan
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VatInstalmentPlanBottomPopup : PopupPage
    {


        VATInstalmentPlanViewModel viewModel;
        public VatInstalmentPlanBottomPopup()
        {
            InitializeComponent();
            viewModel = App.Locator.VatInstalmentPlanPageView;
            this.BindingContext = viewModel;
            this.FlowDirection = FlowDirection.LeftToRight;
        }
    }
}