using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.CustomServicesViewModels;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.CustomServicesPages.eDeclarations
{
    public partial class EDeclerationView : ContentPage
    {
        E_DeclerationViewModel viewModel;
        public EDeclerationView()
        {
            viewModel = App.Locator.eDeclerationViewModel;
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}
