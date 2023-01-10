using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.DashBoardPages.PopUpPages
{
    public partial class SurveyPopUp : PopupPage
    {
        ViewModel.NewDesignViewModel.GAZTNewDesignDashBoardPageViewModel viewModel;
        public SurveyPopUp()
        {
            viewModel = App.Locator.GAZTNewDesignDashBoardPageView;
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}

