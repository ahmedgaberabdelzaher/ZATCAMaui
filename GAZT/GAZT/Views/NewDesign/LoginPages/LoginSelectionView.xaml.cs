using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.LoginViewModels;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.LoginPages
{
    public partial class LoginSelectionView : BaseContentPage
    {
        CustomLoginViewModel viewModel;
        public LoginSelectionView()
        {
            viewModel = App.Locator.CustomLoginViewModel;
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}
