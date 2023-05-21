using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.HomeViewModels;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.HomePages
{
    public partial class GeneralServices : BaseContentPage
    {
        HomeViewModel viewModel;
        public GeneralServices()
        {
            viewModel = App.Locator.homeViewModel;
            viewModel.GetGeneralServiceMenuLst();
            viewModel.ItemCountPerRow = 2;
            viewModel.CurrentService = ViewModel.NewDesignViewModel.HomeViewModels.Services.GeneralServices;
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}
