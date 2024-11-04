using Mopups.Pages;
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
            FlowDirection = App.IsArabic ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
            viewModel = App.Locator.OldZakatInstalmentPlanPageView;
            this.BindingContext = viewModel;
        }
    }
}