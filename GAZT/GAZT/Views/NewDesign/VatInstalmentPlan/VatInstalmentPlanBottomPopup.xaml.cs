using EGAZT.ViewModel.NewDesignViewModel.VATInstalmentPlanViewModel;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.VatInstalmentPlan
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VatInstalmentPlanBottomPopup : PopupPage
    {


        VATInstalmentPlanViewModel viewModel;
        public VatInstalmentPlanBottomPopup()
        {
            InitializeComponent();
            viewModel = App.Locator.VatInstalmentPlanPageView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            this.FlowDirection = FlowDirection.LeftToRight;
        }



        //private void Close_Tapped(object sender, EventArgs e)
        //{
        //    PopupNavigation.Instance.PopAsync();
        //}
    }
}