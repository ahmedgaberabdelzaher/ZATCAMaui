using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    
    public class TaxpayerCorrespondancePageViewModel : BaseViewModel
    {
        #region Fields
        public ICommand OnBackButtonClicked { get; set; }

        #endregion



        #region Constructor
        public TaxpayerCorrespondancePageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnBackButtonClicked = new Xamarin.Forms.Command(() =>
            {
                _navigationService.GoBack();
            });
        }
        #endregion

        #region Method
     
    
        #endregion
    }
}
