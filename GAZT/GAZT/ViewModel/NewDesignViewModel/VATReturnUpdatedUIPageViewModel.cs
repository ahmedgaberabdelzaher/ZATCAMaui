using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    public class VATReturnUpdatedUIPageViewModel : ViewModelBase
    {

        #region Veriables
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        #endregion
        #region Constructor
        public VATReturnUpdatedUIPageViewModel(INavigationService navigationService, IDialogService dialogService)
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
