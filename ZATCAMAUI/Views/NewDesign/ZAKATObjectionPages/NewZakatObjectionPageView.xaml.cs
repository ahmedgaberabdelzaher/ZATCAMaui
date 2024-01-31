using ZATCAMAUI.ViewModel.NewDesignViewModel.ZAKATObjectionPages;

namespace ZATCAMAUI.Views.NewDesign.ZAKATObjectionPages
{
  
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewZakatObjectionPageView : ContentPage
    {
        NewZakatObjectionPageViewModel viewModel;
        public NewZakatObjectionPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.NewZakatObjectionPageView;
            BindingContext = viewModel;
        }
    }
}