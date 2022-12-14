using System;
using System.Collections.Generic;
using EGAZT.Models.EDeclerationsModel.FeesCalculators;
using EGAZT.ViewModel.NewDesignViewModel.EDeclaration;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.EDeclaration
{
    public partial class NewDeclarationPage : ContentPage
    {
        BaseEDeclarationViewModel viewModel;
      
        public NewDeclarationPage(string token="")
        {
            InitializeComponent();
            viewModel = App.Locator.BaseEDeclarationViewModel;
            viewModel.SubmitModel.travelerDeclaration = new Models.EDeclerationsModel.SubmitModels.TravelerDeclaration();
            viewModel.SubmitModel.travelerDeclaration.Isvisitor = true;
            if (token!="")
            {
                viewModel.GetTokenData(token);
                viewModel.SubmitModel.travelerDeclaration.Isvisitor = false;

            }
            viewModel.FeesCalculatorResponse = new FeesCalculatorResponse();
            BindingContext = viewModel;
            
            
        }
       /* public NewDeclarationPage(object payload)
        {
            InitializeComponent();
            viewModel = App.Locator.BaseEDeclarationViewModel;
            viewModel.SubmitModel.travelerDeclaration = new Models.EDeclerationsModel.SubmitModels.TravelerDeclaration();
            // viewModel.SubmitModel.travelerDeclaration.Isvisitor = false;
            BindingContext = viewModel;
        }*/
    }
}

