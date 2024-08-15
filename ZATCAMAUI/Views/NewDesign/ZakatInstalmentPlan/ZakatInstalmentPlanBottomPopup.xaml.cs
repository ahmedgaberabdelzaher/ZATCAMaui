using Mopups.Pages;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel;

namespace ZATCAMAUI.Views.NewDesign.ZakatInstalmentPlan
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatInstalmentPlanBottomPopup : PopupPage
    {
        ZakatInstalmentPlanViewModel viewModel;
        public ZakatInstalmentPlanBottomPopup()
        {
            InitializeComponent();
            viewModel = App.Locator.ZakatInstalmentPlanPageView;
            this.BindingContext = viewModel;
        }

    }
}