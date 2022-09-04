using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.HomeViewModels;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.HomePages
{
    public partial class SideMenuView : BaseContentPage
    {
        HomeViewModel viewModel;
        public SideMenuView()
        {
            viewModel = App.Locator.homeViewModel;
            BindingContext = viewModel;
            viewModel.GetSideMenuLst();
            viewModel.CurrentTab = 2;
            InitializeComponent();
        }
    }
}
