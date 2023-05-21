using System;
using EGAZT.AppConfigurations;
using GalaSoft.MvvmLight.Views;

namespace EGAZT.ViewModel.NewDesignViewModel.CustomServicesViewModels
{
    public class SearchIndiactivePriceForExciseGoodsViewModel:BaseViewModel
    {
        string webViewUrl;
        public string WebViewUrl { get { return webViewUrl; } set { webViewUrl = value; RaisePropertyChanged(); } }
        public SearchIndiactivePriceForExciseGoodsViewModel(INavigationService navigationServices, IDialogService dialogService) : base(navigationServices, dialogService)
        {
            WebViewUrl = PageSettings.ExciseTaxUrl;
        }
    }
}
