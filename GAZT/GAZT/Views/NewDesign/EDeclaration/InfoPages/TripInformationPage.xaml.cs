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
            // if the user select the Tobacco & Product & Air is selected before
            // so we will remove "Traveler Count in XAML","Trip Number" & "Travel Purpose in XAML"
            if(viewModel.TripCard.IsAirTripSelected)
                viewModel.TripCard.IsAirTripSelected = viewModel.SubmitModel.travelerDeclaration.IsDisclosure ? true : false;
            base.OnAppearing();
        }
        protected override bool OnBackButtonPressed()
        {
            viewModel.BackMethod();
            return true;
        }

        void BorderlessEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (viewModel.TripCard.IsLandTripSelected) 
                if (viewModel.SubmitModel.travelerDeclaration.plateCountryCode==113&&!String.IsNullOrEmpty(e.NewTextValue))
                {
                   
                    viewModel.HasPlatesCity = false;
                }
           else if (viewModel.SubmitModel.travelerDeclaration.plateCountryCode == 113 && String.IsNullOrEmpty(e.NewTextValue))
            {
                viewModel.HasPlatesCity = true;
            }
        }
    }
}

