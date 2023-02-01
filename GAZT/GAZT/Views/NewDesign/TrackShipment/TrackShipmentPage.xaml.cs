using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.TrackShipment;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.TrackShipment
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

        protected override void OnDisappearing()
        {
            viewModel.ResetDate();
            viewModel.ResetTrackShipmentData();
            
            base.OnDisappearing();
        }
        protected override bool OnBackButtonPressed()
        {
            viewModel.BackMethod();
            return true;
        }
    }
}

