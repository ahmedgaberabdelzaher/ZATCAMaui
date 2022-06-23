using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.CustomServicesViewModels;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.CustomServicesPages
{
    public partial class SearchIndiactivePriceForExciseGoods : ContentPage
    {
        SearchIndiactivePriceForExciseGoodsViewModel viewModel;
        public SearchIndiactivePriceForExciseGoods()
        {
            viewModel=App.Locator.searchIndiactivePriceForExciseGoodsViewModel;
            BindingContext = viewModel;
            viewModel.SetFlowDirection();
            InitializeComponent();
        }

        void WebView_Navigating(System.Object sender, Xamarin.Forms.WebNavigatingEventArgs e)
        {
            //viewModel.IsLoading = true;
        }

        void WebView_Navigated(System.Object sender, Xamarin.Forms.WebNavigatedEventArgs e)
        {
           // viewModel.IsLoading = false;
        }
    }
}
