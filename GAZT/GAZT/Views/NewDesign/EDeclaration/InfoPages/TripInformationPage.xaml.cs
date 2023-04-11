using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationInformations;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.EDeclaration.InfoPages
{
    public partial class TripInformationPage : ContentPage
    {
        EDeclarationInformationsViewModel viewModel;
        public TripInformationPage()
        {
            InitializeComponent();
            viewModel = App.Locator.EDeclarationInformationsViewModel;
            BindingContext = viewModel;
        }

        protected override void OnDisappearing()
        {
            viewModel.isTripPage = false;
            base.OnDisappearing();
        }
        protected override void OnAppearing()
        {
            viewModel.isTripPage = true;
            // if the user select the Tobacco & Product
            // so we will remove "Traveler Count in XAML","Trip Number" & "Travel Purpose in XAML"
            viewModel.TripCard.IsAirTripSelected = viewModel.SubmitModel.travelerDeclaration.IsDisclosure ? true : false;
            base.OnAppearing();
        }
        protected override bool OnBackButtonPressed()
        {
            viewModel.BackMethod();
            return true;
        }
    }
}

