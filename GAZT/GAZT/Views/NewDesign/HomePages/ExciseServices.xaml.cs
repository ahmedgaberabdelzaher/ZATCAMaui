using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.HomeViewModels;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.HomePages
{
    public partial class ExciseServices : BaseContentPage
    {
        HomeViewModel viewModel;
        public ExciseServices()
        {
            viewModel = App.Locator.homeViewModel;
            viewModel.GetExciseServiceMenuLst();
            viewModel.ItemCountPerRow = 2;
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}
