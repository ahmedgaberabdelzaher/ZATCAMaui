using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.EDeclaration;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.EDeclaration
{
    public partial class NewDeclarationPage : ContentPage
    {
        BaseEDeclarationViewModel viewModel;
        public NewDeclarationPage()
        {
            InitializeComponent();
            viewModel = App.Locator.BaseEDeclarationViewModel;
            BindingContext = viewModel;
        }
    }
}

