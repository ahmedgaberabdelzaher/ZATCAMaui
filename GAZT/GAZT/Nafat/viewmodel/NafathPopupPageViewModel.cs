using System;
using GalaSoft.MvvmLight.Views;
using EGAZT.Models.EnumModels;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel.EstablishmentSignUPVM
{
    public class NafathPopupPageViewModel : BaseViewModel
    {
        public NafathPopupPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
        }
    }
}

