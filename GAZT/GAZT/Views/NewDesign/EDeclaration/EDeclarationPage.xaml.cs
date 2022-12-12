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
            viewModel.ServiceType = 0;
            viewModel.IdentityType = 0;
        }
    }
}

