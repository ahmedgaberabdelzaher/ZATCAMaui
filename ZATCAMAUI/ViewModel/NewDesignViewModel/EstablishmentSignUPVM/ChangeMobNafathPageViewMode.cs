using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentSignUPVM
{
    public class ChangeMobNafathPageViewMode : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public int CurrentAttempt = 0;

        public ChangeMobNafathPageViewMode(INavigationService navigationService, IDialogService dialogService)

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
        }

        public void Goback()
        {
            _navigationService.GoBack();
        }
        public bool IsLoading { get; internal set; }

       
    }
}

