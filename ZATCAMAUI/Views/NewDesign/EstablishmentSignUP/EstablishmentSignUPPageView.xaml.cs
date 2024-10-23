using ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentSignUPVM;

namespace ZATCAMAUI.Views.NewDesign.EstablishmentSignUP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstablishmentSignUPPageView : ContentPage
    {
        EstablishmentSignUPPageViewModel viewModel;
        public EstablishmentSignUPPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.EstablishmentSignUPPageView;
            BindingContext = viewModel;
        }

    }
}