using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.HomeViewModels;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign
{
    public partial class Home : BaseContentPage
    {
        HomeViewModel viewModel;
        public Home(string tab="0")
        {
            viewModel = App.Locator.homeViewModel;
            BindingContext = viewModel;
          
            viewModel.CurrentTab = 0;
            InitializeComponent();
            if (tab == "3")
            {
                tab = "0";
                viewModel.GetDashBoardMenuLst(3);
                beforeLoginMenu.IsVisible = true;
                preLoginMenu.IsVisible = false;
            }
            else
            {
                beforeLoginMenu.IsVisible = false;
                preLoginMenu.IsVisible = true;
            }

        }
    }
}
