using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.HomeViewModels;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign
{
    public partial class Home : BaseContentPage
    {
        HomeViewModel viewModel;
        public Home(string tab)
        {
            viewModel = App.Locator.homeViewModel;
            BindingContext = viewModel;
            viewModel.CurrentTab = int.Parse(tab);
            InitializeComponent();
            
        }
    }
}
