using RGPopup.Maui.Pages;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel;

namespace ZATCAMAUI.Views.NewDesign.ZakatInstalmentPlan
{
  
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OldZakatInstalmentPlanBottomPopup : PopupPage
    {
        OldZakatInstalmentPlanViewModel viewModel;
        public OldZakatInstalmentPlanBottomPopup()
        {
            InitializeComponent();
            viewModel = App.Locator.OldZakatInstalmentPlanPageView;
            this.BindingContext = viewModel;
            this.FlowDirection = FlowDirection.LeftToRight;
        }
    }
}