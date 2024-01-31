using ZATCAMAUI.ViewModel.NewDesignViewModel.LiveVideoVM;

namespace ZATCAMAUI.Views.NewDesign.LiveVideo
{
    public partial class LiveVideoPage : ContentPage
    {
        LiveVideoViewModel viewModel;
        public LiveVideoPage()
        {
            InitializeComponent();
            viewModel = App.Locator.LiveVideoViewModel;
            viewModel.CurrentTab = 3;
            BindingContext = viewModel;
        }
    }
}

