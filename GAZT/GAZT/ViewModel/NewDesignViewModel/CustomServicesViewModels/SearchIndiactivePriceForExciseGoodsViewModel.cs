using System;
using GalaSoft.MvvmLight.Views;

namespace EGAZT.ViewModel.NewDesignViewModel.CustomServicesViewModels
{
    public class SearchIndiactivePriceForExciseGoodsViewModel:BaseViewModel
    {
        string webViewUrl;
        public string WebViewUrl { get { return webViewUrl; } set { webViewUrl = value; RaisePropertyChanged(); } }
        public SearchIndiactivePriceForExciseGoodsViewModel(INavigationService navigationServices, IDialogService dialogService) : base(navigationServices, dialogService)
        {
            WebViewUrl = "http://esvc-web1-stg.ga.customs.gov.sa/sites/sc/ar/app-view/Pages/Disclaimer.aspx";
        }
    }
}
