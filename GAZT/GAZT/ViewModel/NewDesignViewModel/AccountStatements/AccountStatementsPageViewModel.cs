using System;
using GalaSoft.MvvmLight.Views;

namespace EGAZT.ViewModel.NewDesignViewModel.AccountStatements
{
    public class AccountStatementsPageViewModel:BaseViewModel
    {
        public AccountStatementsPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
        }
    }
}
