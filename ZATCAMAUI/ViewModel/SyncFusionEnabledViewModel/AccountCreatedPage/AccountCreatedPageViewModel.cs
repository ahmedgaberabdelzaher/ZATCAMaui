using System.Windows.Input;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.AccountCreatedPage
{

    public class AccountCreatedPageViewModel : BaseViewModel
    {
        #region Veriables
        public ICommand OnLoginPageLinkClicked;
        #endregion
        #region Constructor
        public AccountCreatedPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }
        #endregion
    }
}
