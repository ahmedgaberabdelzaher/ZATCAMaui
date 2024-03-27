using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System.Windows.Input;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.AccountCreatedPage
{

    public class AccountCreatedPageViewModel : BaseViewModel
    {
        #region Veriables
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnLoginPageLinkClicked;
        #endregion
        #region Constructor
        public AccountCreatedPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
        }
        #endregion
    }
}
