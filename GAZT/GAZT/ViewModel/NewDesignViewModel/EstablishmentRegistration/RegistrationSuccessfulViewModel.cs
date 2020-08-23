using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.EstablishmentRegistration
{
    public class RegistrationSuccessfulViewModel: BaseViewModel
    {
        #region commands
        public ICommand GoToDashBoardButtonClick { get; private set; }
        #endregion
        public RegistrationSuccessfulViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            GoToDashBoardButtonClick = new Command(() => navigationService.NavigateTo(App.GAZTNewDesignDashBoardPageView));
        }
    }
}
