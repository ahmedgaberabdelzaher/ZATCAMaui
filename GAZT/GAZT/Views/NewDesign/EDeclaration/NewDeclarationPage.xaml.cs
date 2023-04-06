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
        string token = string.Empty;
        public NewDeclarationPage(string token="")
        {
            InitializeComponent();
            viewModel = App.Locator.BaseEDeclarationViewModel;
            viewModel.SubmitModel.travelerDeclaration = new Models.EDeclerationsModel.SubmitModels.TravelerDeclaration();
            viewModel.SubmitModel.travelerDeclaration.Isvisitor = true;
            this.token = token;
            if (token!="")
            {
               
                viewModel.GetTokenData(token);
                viewModel.SubmitModel.travelerDeclaration.Isvisitor = false;

            }
            BindingContext = viewModel;
            App.Locator.StateManager.SetItem("IsLoggedIn", viewModel.SubmitModel.travelerDeclaration.Isvisitor);

        }
        protected override void OnAppearing()
        {
            if (this.token != "")
            {
                viewModel.SubmitModel.travelerDeclaration.Isvisitor = false;
                App.Locator.StateManager.SetItem("IsLoggedIn", viewModel.SubmitModel.travelerDeclaration.Isvisitor);
            }
            base.OnAppearing();
        }
    }
}

