using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;

namespace GAZT.ViewModel.NewViewModel
{
    public class AAcknowledgementViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        public AAcknowledgementViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }


            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            _navigationService = navigationService;
            _dialogService = dialogService;
        }
        }
}
