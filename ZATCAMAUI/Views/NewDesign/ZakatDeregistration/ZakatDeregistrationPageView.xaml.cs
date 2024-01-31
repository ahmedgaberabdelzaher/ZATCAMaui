using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration;

namespace ZATCAMAUI.Views.NewDesign.ZakatDeregistration
{
   
    public partial class ZakatDeregistrationPageView : ContentPage
    {
        ZakatDeregistrationPageViewModel viewModel;

        public ZakatDeregistrationPageView()
        {
            InitializeComponent();

            viewModel = App.Locator.ZakatDeregistrationPageView;
            On<iOS>().SetUseSafeArea(true);
            BindingContext = viewModel;
            FlowDirection = FlowDirection.LeftToRight;
        }
    }
}
