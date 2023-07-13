using System;
using System.Collections.Generic;
using Xamarin.Forms;
using EGAZT.ViewModel.NewDesignViewModel.CustomServicesViewModels;
namespace EGAZT.Views.NewDesign.CustomServicesPages.CustomFees
{	
	public partial class CustomFeesFormView : BaseContentPage
    {
        CustomFeesFormViewModel viewModel;
        public CustomFeesFormView ()
		{
			viewModel = App.Locator.CustomFeesFormViewModel;
			BindingContext = viewModel;

            InitializeComponent ();
		}
	}
}

