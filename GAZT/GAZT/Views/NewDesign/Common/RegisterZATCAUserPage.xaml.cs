using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.Common;
using Xamarin.Forms;
namespace EGAZT.Views.NewDesign.Common
{	
	public partial class RegisterZATCAUserPage : ContentPage
	{
        RegisterZATCAUserViewModel viewModel;
        public RegisterZATCAUserPage()
        {

            InitializeComponent();
            viewModel = App.Locator.RegisterZATCAUserViewModel;
            BindingContext = viewModel;

        }

        protected override bool OnBackButtonPressed()
        {
            //viewModel.BackMethod();
            return true;
        }
    }
}

