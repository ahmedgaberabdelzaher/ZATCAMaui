using System.Windows.Input;
using GalaSoft.MvvmLight.Views;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.CustomServicesViewModels
{
    public class ExciseTaxViewModel : BaseViewModel
    {
        public ExciseTaxViewModel(INavigationService navigationServices, IDialogService dialogService) : base(navigationServices, dialogService)
        {
        }
        public ICommand NavigateTosearchingandviewingtheindicativepricesforexciseGoodsCommand
        {
            get
            {
                return new Command(() =>
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("ExciseTax", "OnExciseTaxCommand", "Searching and viewing the indicative prices for excise Goods");
                    _navigationService.NavigateTo("SearchIndiactivePriceForExciseGoods");
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                });
            }
        }

    }
}
