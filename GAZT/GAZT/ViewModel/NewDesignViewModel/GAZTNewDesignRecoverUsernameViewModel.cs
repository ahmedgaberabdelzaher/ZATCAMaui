using System;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    [Preserve(AllMembers = true)]
    public class GAZTNewDesignRecoverUsernameViewModel : BaseViewModel
    {
        public Command OnLoginButtonClicked { get; set; }
        public GAZTNewDesignRecoverUsernameViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            
           OnLoginButtonClicked = new Command(async () => {
              
                   await Application.Current.MainPage.Navigation.PopModalAsync(true);
              
              
              
              // navigationService.NavigateTo(App.SFLoginPageView, App.SFLandingPageView);
});
        }
    }
}
