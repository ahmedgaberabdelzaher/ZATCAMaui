using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.TrackShipment;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.TrackShipment
{
    public partial class ShipmentStatusPage : ContentPage
    {
        TrackShipmentViewModel viewModel;
        public ShipmentStatusPage()
        {
            viewModel = App.Locator.TrackShipmentViewModel;
            BindingContext = viewModel;

            InitializeComponent();

        }
    }
}

