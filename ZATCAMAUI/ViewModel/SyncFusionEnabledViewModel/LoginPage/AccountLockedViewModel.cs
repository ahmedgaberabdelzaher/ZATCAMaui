using System;
using System.Windows.Input;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.LoginPage
{
    public class AccountLockedViewModel : BaseViewModel
    {
        public ICommand HamburgerMenuClickedCommand { get; set; }

        public ICommand ResetPasswordCommand { get; set; }
        public ICommand LoginCommand { get; set; }

        public AccountLockedViewModel(INavigationService navigationService, IDialogService dialogService):base(navigationService,dialogService)
        {
          
            this.HamburgerMenuClickedCommand = new Command(this.HamburgerMenuClicked);
            this.ResetPasswordCommand = new Command(this.ResetPasswordClicked);
            this.LoginCommand = new Command(this.LoginClicked);

        }

        private void HamburgerMenuClicked()
        {
            _navigationService.NavigateTo(App.DashboardAnonymousMenuPageView);
        }

        private void ResetPasswordClicked()
        {
            _navigationService.NavigateTo(App.UnlockAccountTINPageView);
        }

        private void LoginClicked()
        {
            _navigationService.GoBack();
        }
    }

}

