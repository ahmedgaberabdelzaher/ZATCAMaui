
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
            BindingContext = viewModel;
        }
    }
}
