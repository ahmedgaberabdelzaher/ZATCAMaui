using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System.Windows.Input;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.AccountCreatedPage
{

    public class AccountCreatedPageViewModel : ViewModelBase
    {
        #region Veriables
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnLoginPageLinkClicked;
        #endregion
        #region Constructor
        public AccountCreatedPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            //OnLoginPageLinkClicked = new Xamarin.Forms.Command(() =>
            //{
            //    _navigationService.NavigateTo(App.LogInPageView, App.SFLandingPageView);
            //});
        }
        #endregion
    }
}
