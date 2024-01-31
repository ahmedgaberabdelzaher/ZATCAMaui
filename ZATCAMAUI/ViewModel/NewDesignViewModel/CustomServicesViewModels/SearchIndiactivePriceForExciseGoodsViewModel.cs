using GalaSoft.MvvmLight.Views;
using ZATCAMAUI.Core.AppConfigurations;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.CustomServicesViewModels
{
    public class SearchIndiactivePriceForExciseGoodsViewModel : BaseViewModel
    {
        string webViewUrl;
        public string WebViewUrl { get { return webViewUrl; } set { webViewUrl = value; RaisePropertyChanged(); } }
        public SearchIndiactivePriceForExciseGoodsViewModel(INavigationService navigationServices, IDialogService dialogService) : base(navigationServices, dialogService)
        {
            WebViewUrl = PageSettings.ExciseTaxUrl;
        }
    }
}
