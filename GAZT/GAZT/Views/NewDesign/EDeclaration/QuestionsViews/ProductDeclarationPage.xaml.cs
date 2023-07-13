using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EGAZT.Models.EDeclerationsModel.FeesCalculators;
using EGAZT.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationProduct;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.EDeclaration.QuestionsViews
{
    public partial class ProductDeclarationPage : ContentPage
    {
        BaseProductDeclarationViewModel viewModel;
        public ProductDeclarationPage()
        {
            InitializeComponent();
            viewModel = App.Locator.ProductDeclarationViewModel;
            BindingContext = viewModel;
            viewModel.IsArrivingPlaneSelected = viewModel.SubmitModel.travelerDeclaration.travelingType == 2 ? false : true;
            viewModel.HeaderTitle = viewModel.IsArrivingPlaneSelected ? AppResources.EDeclarationArrivalHeader : AppResources.EDeclarationDepatureHeader;
            viewModel.FeesCalculatorResponse = new FeesCalculatorResponse();
            viewModel.FeesCalculatorBody = new FeesCalculatorBody();

        }

        protected override bool OnBackButtonPressed()
        {
            viewModel.BackMethod();
            return true;
        }
    }
}

