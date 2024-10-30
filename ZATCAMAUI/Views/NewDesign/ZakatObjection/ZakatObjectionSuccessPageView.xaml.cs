
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatObjectionViewModel;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.NewDesign.ZakatObjection
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatObjectionSuccessPageView : ContentPage
    {
        private ZakatObjectionViewModel _viewModel;
        public ZakatObjectionSuccessPageView()
        {
            InitializeComponent();

            _viewModel = App.Locator.ZakatObjectionSuccessView;

            BindingContext = _viewModel;
        }

    }
}