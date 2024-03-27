using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.ViewModel.NewDesignViewModel.CustomServicesViewModels;

namespace ZATCAMAUI.Views.NewDesign.CustomServicesPages
{
    public partial class ReportsPage : ContentPage
    {
        ReportsMenuViewModel viewModel;
        public ReportsPage()
        {
            InitializeComponent();
            viewModel = App.Locator.reportsMenuViewModel;
            BindingContext = viewModel;

            On<iOS>().SetUseSafeArea(true);
            App.Current.On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
        }
    }
}
