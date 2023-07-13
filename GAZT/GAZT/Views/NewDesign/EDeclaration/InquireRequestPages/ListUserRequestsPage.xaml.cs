using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.EDeclaration;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.EDeclaration.InquireRequestPages
{	
	public partial class ListUserRequestsPage : ContentPage
	{	
        ListUserRequestsViewModel viewModel;
        public ListUserRequestsPage()
        {
            InitializeComponent();
            viewModel = App.Locator.ListUserRequestsViewModel;
            BindingContext = viewModel;
        }

        protected override bool OnBackButtonPressed()
        {

            viewModel.BackMethod();
            return true;
        }
    }
}

