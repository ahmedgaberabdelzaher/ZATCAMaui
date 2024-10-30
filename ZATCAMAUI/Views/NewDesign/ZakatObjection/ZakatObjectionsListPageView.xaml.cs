
using ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatObjectionViewModel;

namespace ZATCAMAUI.Views.NewDesign.ZakatObjection
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatObjectionsListPageView : ContentPage
    {
        ZakatObjectionsListViewModel viewModel;
        public ZakatObjectionsListPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.ZakatObjectionListView;
            BindingContext = viewModel;
           


        }
       
    }


}