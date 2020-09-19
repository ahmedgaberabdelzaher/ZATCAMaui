using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EGAZT.ViewModel.NewDesignViewModel.DashBoardPageViewModel
{
    public class TaxManagementPageViewModel : BaseViewModel
    {
        public ICommand OnBackButtonClicked { get; set; }

        #region Constructor
        public TaxManagementPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnBackButtonClicked = new Xamarin.Forms.Command(() =>
            {
                _navigationService.GoBack();
            });
        }
        #endregion
    }
}
