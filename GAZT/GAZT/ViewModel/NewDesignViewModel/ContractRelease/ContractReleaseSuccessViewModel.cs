using GalaSoft.MvvmLight.Views;

namespace EGAZT.ViewModel.NewDesignViewModel.ContractReleaseViewModel
{
    public class ContractReleaseSuccessViewModel : BaseViewModel
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        public ContractReleaseSuccessViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
        }
    }
}