using System;
using System.Collections.Generic;
using EGAZT.Models.EDeclerationsModel.FeesCalculators;
using EGAZT.ViewModel.NewDesignViewModel.EDeclaration;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.EDeclaration
{
    public partial class ProductDeclarationPage : ContentPage
    {
        ProductDeclarationViewModel viewModel;
        public ProductDeclarationPage()
        {
            InitializeComponent();
            viewModel = App.Locator.ProductDeclarationViewModel;
            BindingContext = viewModel;
            viewModel.IsArrivingPlaneSelected = viewModel.SubmitModel.travelerDeclaration.travelingType == 2 ? false : true;
            viewModel.FeesCalculatorResponse = new FeesCalculatorResponse();
            if (!viewModel.IsArrivingPlaneSelected)
            {
                viewModel.QFlow = 3;
            }
            else
            {
                viewModel.QFlow = 1;
            }
            viewModel.SetQuestion();
        }
    }
}

