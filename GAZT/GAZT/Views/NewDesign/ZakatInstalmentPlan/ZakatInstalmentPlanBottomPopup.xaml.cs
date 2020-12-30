using EGAZT.ViewModel.NewDesignViewModel.ZakatInstalmentPlanViewModel;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ZakatInstalmentPlan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatInstalmentPlanBottomPopup : PopupPage
    {
        ZakatInstalmentPlanViewModel viewModel;
        public ZakatInstalmentPlanBottomPopup()
        {
            InitializeComponent();
            viewModel = App.Locator.ZakatInstalmentPlanPageView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            this.FlowDirection = FlowDirection.LeftToRight;
        }

    }
}