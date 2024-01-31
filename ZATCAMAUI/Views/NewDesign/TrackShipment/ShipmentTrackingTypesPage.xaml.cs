using ZATCAMAUI.ViewModel.NewDesignViewModel.TrackShipment;

namespace ZATCAMAUI.Views.NewDesign.TrackShipment
{
    public partial class ShipmentTrackingTypesPage : ContentPage
    {
        TrackShipmentViewModel viewModel;
        public ShipmentTrackingTypesPage()
        {
            viewModel = App.Locator.TrackShipmentViewModel;
            BindingContext = viewModel;

            InitializeComponent();

        }
    }
}

