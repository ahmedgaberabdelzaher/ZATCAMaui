using System;
using System.Collections.Generic;
using EGAZT.Models.EDeclerationsModel.SubmitModels;
using EGAZT.ViewModel.NewDesignViewModel.EDeclaration;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.EDeclaration
{
    public partial class EDeclarationPaymentPage : ContentPage
    {
        EDeclarationPaymentViewModel viewModel;
        public EDeclarationPaymentPage(TravelerDeclarationResponse travelerDeclarationResponse)
        {
            InitializeComponent();
            viewModel = App.Locator.EDeclarationPaymentViewModel;
            viewModel.TravelerDeclarationResponse = travelerDeclarationResponse;
            BindingContext = viewModel;
        }
    }
}

