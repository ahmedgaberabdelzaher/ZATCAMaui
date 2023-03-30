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

            base.OnAppearing();
        }
        protected override bool OnBackButtonPressed()
        {
            viewModel.BackMethod();
            return true;
        }
    }
}

