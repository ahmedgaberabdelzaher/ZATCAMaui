using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationInformations;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.EDeclaration
{
    public partial class ContactInformationPage : ContentPage
    {
        EDeclarationInformationsViewModel viewModel;
        public ContactInformationPage()
        {
            InitializeComponent();
            viewModel = App.Locator.EDeclarationInformationsViewModel;
            BindingContext = viewModel;
        }
    }
}

