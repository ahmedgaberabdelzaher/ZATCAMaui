using ZATCAMAUI.ViewModel.NewDesignViewModel.CustomServicesViewModels;

namespace ZATCAMAUI.Views.NewDesign.CustomServicesPages
{
    public partial class SearchIndiactivePriceForExciseGoods : ContentPage
    {
        SearchIndiactivePriceForExciseGoodsViewModel viewModel;
        public SearchIndiactivePriceForExciseGoods()
        {
            viewModel = App.Locator.searchIndiactivePriceForExciseGoodsViewModel;
            BindingContext = viewModel;
            viewModel.SetFlowDirection();
            InitializeComponent();
        }

        void WebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            //viewModel.IsLoading = true;
        }

        void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            // viewModel.IsLoading = false;
        }
    }
}
