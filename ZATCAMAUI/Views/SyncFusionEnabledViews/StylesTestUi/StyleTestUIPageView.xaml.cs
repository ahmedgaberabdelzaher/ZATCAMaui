using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.StylesTestUi;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.StylesTestUi
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StyleTestUIPageView : ContentPage
    {
        StyleTestUIPageViewModel viewModel;
        public StyleTestUIPageView()

        {
            InitializeComponent();
            viewModel = App.Locator.StyleTestUIPageView;
            On<iOS>().SetUseSafeArea(true);
            BindingContext = viewModel;
            FlowDirection = FlowDirection.LeftToRight;
        }

    }
}