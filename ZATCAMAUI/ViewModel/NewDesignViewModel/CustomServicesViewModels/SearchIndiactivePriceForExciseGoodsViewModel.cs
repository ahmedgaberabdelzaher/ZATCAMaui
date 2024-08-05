
using ZATCAMAUI.Core.AppConfigurations;
using ZATCAMAUI.Core.Interfaces;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.CustomServicesViewModels
{
    public class SearchIndiactivePriceForExciseGoodsViewModel : BaseViewModel
    {
        string webViewUrl;
        public string WebViewUrl { get { return webViewUrl; } set { webViewUrl = value; OnPropertyChanged(); } }
        public SearchIndiactivePriceForExciseGoodsViewModel(INavigationService navigationServices, IDialogService dialogService) : base(navigationServices, dialogService)
        {
            WebViewUrl = PageSettings.ExciseTaxUrl;
        }
    }
}
