using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.EDeclaration;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.EDeclaration
{
    public partial class EDeclarationPage : BaseContentPage
    {
        EDeclerationViewModel viewModel; 
        public EDeclarationPage()
        {

            viewModel = App.Locator.EDeclerationViewModel;
            BindingContext = viewModel;
            InitializeComponent();
            viewModel.ShowReviewEntries = false;
            viewModel.IdentityType = 0;
        }
        protected override bool OnBackButtonPressed()
        {
            viewModel.BackMethod();
            return true;
        }
    }
}

