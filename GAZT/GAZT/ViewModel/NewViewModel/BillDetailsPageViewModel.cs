using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace GAZT.ViewModel.NewViewModel
{
   public class BillDetailsPageViewModel: ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnCopySadadNumberButtonClicked { get; set; }

        #endregion

        #region Property
        #endregion

        #region Constructor
        public BillDetailsPageViewModel(INavigationService navigationService, IDialogService dialogService)
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


            OnCopySadadNumberButtonClicked = new Xamarin.Forms.Command(async () =>
            {
               await _dialogService.ShowMessage("It has copied sadad payment number",AppResources.Information);
            });


        }
        #endregion

        #region Method
        #endregion
    }
}
