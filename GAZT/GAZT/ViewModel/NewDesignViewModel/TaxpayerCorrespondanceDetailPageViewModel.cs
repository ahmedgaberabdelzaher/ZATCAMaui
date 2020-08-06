using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EGAZT.ViewModel.NewDesignViewModel
{
   public  class TaxpayerCorrespondanceDetailPageViewModel : BaseViewModel
    {
        public ICommand OnBackButtonClicked { get; set; }
        public TaxpayerCorrespondanceDetailPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnBackButtonClicked = new Xamarin.Forms.Command(() =>
            {
                _navigationService.GoBack();
            });
        }
    }
    
}
