using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.LoginViewModels;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.CustomServicesPages.CustomDashBoard
{
    public partial class CustomDashBoardVi : BaseContentPage
    {

        CustomLoginViewModel viewModel;
        public CustomDashBoardVi()
        {
            viewModel = App.Locator.CustomLoginViewModel;
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}
