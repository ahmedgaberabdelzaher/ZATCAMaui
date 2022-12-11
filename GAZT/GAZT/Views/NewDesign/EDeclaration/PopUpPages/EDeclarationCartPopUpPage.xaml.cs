using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.EDeclaration;
using EGAZT.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationInformations;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.EDeclaration.PopUpPages
{
    public partial class EDeclarationCartPopUpPage : PopupPage
    {
        ProductDeclarationViewModel viewModel;
        public EDeclarationCartPopUpPage()
        {
            try
            {
                InitializeComponent();
                viewModel = App.Locator.ProductDeclarationViewModel;
                BindingContext = viewModel;
            }
            catch (Exception ex)
            {

            }
        }
        // Invoked when a hardware back button is pressed
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return false;
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
            return true;
        }
    }
}

