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
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}
