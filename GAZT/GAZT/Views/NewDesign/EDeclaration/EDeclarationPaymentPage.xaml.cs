using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.EDeclaration;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.EDeclaration
{
    public partial class EDeclarationPaymentPage : ContentPage
    {
        EDeclarationPaymentViewModel viewModel;
        public EDeclarationPaymentPage()
        {
            InitializeComponent();
            viewModel = App.Locator.EDeclarationPaymentViewModel;
            BindingContext = viewModel;
        }
    }
}

