using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    public class VATReturnSuccessfullPageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public VATReturnSuccessfullPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            _dialogService = dialogService;
        }
    }
}
