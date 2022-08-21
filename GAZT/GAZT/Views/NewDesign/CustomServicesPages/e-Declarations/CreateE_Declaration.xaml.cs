using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.CustomServicesViewModels;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.CustomServicesPages.eDeclarations
{
    public partial class CreateE_Declaration : ContentPage
    {
        E_DeclerationViewModel viewModel;

        public CreateE_Declaration()
        {
            viewModel = App.Locator.eDeclerationViewModel;
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}
