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

        protected override void OnAppearing()
        {
            viewModel.DrawShipmentTrack.ShipmentCardImage = App.Locator.StateManager.GetItem("CardImage") as string;
            base.OnAppearing();
        }

        protected override bool OnBackButtonPressed()
        {

            viewModel.BackMethod();
            return true;
        }
    }
}

