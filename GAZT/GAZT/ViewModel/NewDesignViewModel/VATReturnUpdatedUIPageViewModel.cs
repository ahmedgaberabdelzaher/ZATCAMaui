using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    public class VATReturnUpdatedUIPageViewModel : BaseViewModel
    {
        #region Constructor
        public VATReturnUpdatedUIPageViewModel(INavigationService navigationService, IDialogService dialogService):base(navigationService, dialogService)
        {  
            //OnLoginPageLinkClicked = new Xamarin.Forms.Command(() =>
            //{
            //    _navigationService.NavigateTo(App.LogInPageView, App.SFLandingPageView);
            //});
        }
        #endregion
    }
}
