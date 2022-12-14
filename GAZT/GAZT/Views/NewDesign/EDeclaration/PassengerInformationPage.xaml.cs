using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationInformations;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.EDeclaration
{
    public partial class PassengerInformationPage : ContentPage
    {
        EDeclarationInformationsViewModel viewModel;
        public PassengerInformationPage()
        {
            viewModel = App.Locator.EDeclarationInformationsViewModel;
            BindingContext = viewModel;
            viewModel.isPassengerPage = true;
                    InitializeComponent();
         
        }
        protected override void OnDisappearing()
        {
            viewModel.isPassengerPage = false;
            base.OnDisappearing();
        }
    }
}

