using System.Windows.Input;
using GalaSoft.MvvmLight.Views;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.CustomServicesViewModels
{
    public class ReportsMenuViewModel : BaseViewModel
    {
        public ReportsMenuViewModel(INavigationService navigationServices, IDialogService dialogService) : base(navigationServices, dialogService)
        {
        }
        public ICommand NavigateToBalaghPageCommand
        {
            get
            {
                return new Command(() =>
                {
                    _navigationService.NavigateTo("ReportFinancialViolation");
                });
            }
        }

        public ICommand NavigateToTaxEvasionPageCommand
        {
            get
            {
                return new Command(() =>
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("ReportsPage", "OnTaxEvasionCommand", "Tax Evasion eService");
                    _navigationService.NavigateTo(App.TaxEvasionPageWebView);
                    AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                });
            }
        }

    }
}
