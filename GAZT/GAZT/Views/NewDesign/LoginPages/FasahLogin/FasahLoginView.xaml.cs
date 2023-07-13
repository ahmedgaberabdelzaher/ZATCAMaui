using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.LoginViewModels;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.LoginPages.FasahLogin
{
    public partial class FasahLoginView : BaseContentPage
	{
		FasahLoginViewModel viewModel;
        public FasahLoginView ()
		{
			viewModel = App.Locator.FasahLoginViewModel;
			BindingContext = viewModel;
			InitializeComponent ();
		}
	}
}

