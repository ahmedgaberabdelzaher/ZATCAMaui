using GalaSoft.MvvmLight.Views;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    public class GAZTNewDesignVATReturnUpdatedUIPageViewModel : BaseViewModel
    {
        #region Constructor
        public GAZTNewDesignVATReturnUpdatedUIPageViewModel(INavigationService navigationService, IDialogService dialogService):base(navigationService, dialogService)
        {  
            //OnLoginPageLinkClicked = new Xamarin.Forms.Command(() =>
            //{
            //    _navigationService.NavigateTo(App.LogInPageView, App.SFLandingPageView);
            //});
        }
        #endregion
    }
}
