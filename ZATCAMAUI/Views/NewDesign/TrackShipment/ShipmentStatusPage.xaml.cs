using ZATCAMAUI.ViewModel.NewDesignViewModel.TrackShipment;

namespace ZATCAMAUI.Views.NewDesign.TrackShipment
{
    public partial class ShipmentStatusPage : ContentPage
    {
        TrackShipmentViewModel viewModel;
        public ShipmentStatusPage()
        {
            InitializeComponent();
            viewModel = App.Locator.TrackShipmentViewModel;
            BindingContext = viewModel;
        }

        protected override bool OnBackButtonPressed()
        {

            viewModel.BackMethod();
            return true;
        }
    }
}

