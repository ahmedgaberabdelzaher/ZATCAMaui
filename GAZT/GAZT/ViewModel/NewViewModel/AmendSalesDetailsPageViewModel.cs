using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace GAZT.ViewModel.NewViewModel
{
    public class AmendSalesDetailsPageViewModel: ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
      //  public ICommand OnBillsButtonClicked { get; set; }
        #endregion

        #region Property
        #endregion

        #region Constructor
        public AmendSalesDetailsPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;
            _dialogService = dialogService;



            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }


            //OnBillsButtonClicked = new Xamarin.Forms.Command(async () =>
            //{
            //    _navigationService.NavigateTo(App.BillDetailsPageView);
            //});


        }
        #endregion

        #region Method
        #endregion
    }
}
