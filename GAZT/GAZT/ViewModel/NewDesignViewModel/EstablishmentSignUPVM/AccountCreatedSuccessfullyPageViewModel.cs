using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
namespace EGAZT.ViewModel.NewDesignViewModel.EstablishmentSignUPVM
{
    public class AccountCreatedSuccessfullyPageViewModel : ViewModelBase
    {
        #region Veriables
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnLoginPageLinkClicked;
        #endregion
        #region Constructor
        public AccountCreatedSuccessfullyPageViewModel(INavigationService navigationService, IDialogService dialogService)
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
