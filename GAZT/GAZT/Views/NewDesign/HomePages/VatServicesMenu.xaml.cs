using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.HomeViewModels;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.HomePages
{
    public partial class VatServicesMenu : BaseContentPage
    {
        HomeViewModel viewModel;
        public VatServicesMenu()
        {
            viewModel = App.Locator.homeViewModel;
            viewModel.GetVatServiceMenuLst();
            viewModel.ItemCountPerRow = 2;
            viewModel.CurrentService = ViewModel.NewDesignViewModel.HomeViewModels.Services.VATServices;
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}
