using ZATCAMAUI.ViewModel.NewDesignViewModel.TrackShipment;

namespace ZATCAMAUI.Views.NewDesign.TrackShipment
{
    public partial class TrackShipmentPage : ContentPage
    {
        TrackShipmentViewModel viewModel;
        public TrackShipmentPage()
        {
            viewModel = App.Locator.TrackShipmentViewModel;
            BindingContext = viewModel;

            InitializeComponent();

        }

        protected override bool OnBackButtonPressed()
        {

            viewModel.BackMethod();
            return true;
        }
    }
}

