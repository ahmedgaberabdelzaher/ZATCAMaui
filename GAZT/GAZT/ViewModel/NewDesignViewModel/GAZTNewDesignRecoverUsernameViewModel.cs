using System;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    public class GAZTNewDesignRecoverUsernameViewModel : BaseViewModel
    {
        public Command OnLoginButtonClicked { get; set; }
        public GAZTNewDesignRecoverUsernameViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnLoginButtonClicked = new Command(() => { navigationService.NavigateTo(App.SFLoginPageView, App.SFLandingPageView); });
        }
    }
}
