using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.DashBoardPages
{
    public partial class QuickActionPopUpPageView :  PopupPage
    {
        QuickActionPopUpPageViewModel viewModel;
        public QuickActionPopUpPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.QuickActionPopUpPageView;
            this.BindingContext = viewModel;
        }

        private void OnCloseTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}
