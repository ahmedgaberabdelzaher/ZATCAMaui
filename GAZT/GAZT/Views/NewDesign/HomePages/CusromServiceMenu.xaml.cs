using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.HomeViewModels;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.HomePages
{
    public partial class CusromServiceMenu : BaseContentPage
    {
        HomeViewModel viewModel;
        public CusromServiceMenu()
        {
            viewModel = App.Locator.homeViewModel;
            viewModel.GetCustomServiceMenuLst();
            viewModel.ItemCountPerRow = 2;
            viewModel.CurrentService = ViewModel.NewDesignViewModel.HomeViewModels.Services.CustomServices;
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}
