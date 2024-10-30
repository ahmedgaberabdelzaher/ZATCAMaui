
using ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatObjectionViewModel;

namespace ZATCAMAUI.Views.NewDesign.ZakatObjection
{
   
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ObjectionsSelectionPageView : ContentPage
    {

        private ObjectionViewModel _viewModel;

        public ObjectionsSelectionPageView()
        {
            InitializeComponent();

            _viewModel = App.Locator.ObjectionsSelectionPageView;

            BindingContext = _viewModel;

            _viewModel.AddSelectionOptions();

        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                _viewModel.AddSelectionOptions();

            }
            catch (Exception)
            {


            }
        }
    }
}