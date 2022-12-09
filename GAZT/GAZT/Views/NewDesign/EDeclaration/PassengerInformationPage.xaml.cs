using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.EDeclaration;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.EDeclaration
{
    public partial class PassengerInformationPage : ContentPage
    {
        EDeclarationInformationsViewModel viewModel;
        public PassengerInformationPage()
        {
            InitializeComponent();
            viewModel = App.Locator.EDeclarationInformationsViewModel;
            BindingContext = viewModel;
        }
    }
}

