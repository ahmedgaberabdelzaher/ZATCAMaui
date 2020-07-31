using System;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    public class GAZTNewDesignRecoverPasswordPageViewModel : BaseViewModel
    {
        public Command OnLoginButtonClicked { get; set; }
        public GAZTNewDesignRecoverPasswordPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnLoginButtonClicked = new Command(() => { navigationService.NavigateTo(App.SFLoginPageView, App.SFLandingPageView); });
        }
    }
}
