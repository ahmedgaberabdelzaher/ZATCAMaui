using CommunityToolkit.Mvvm.ComponentModel;
using ZATCAMAUI.Core.Interfaces;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentSignUPVM
{
    public class ChangeMobNafathPageViewMode : BaseViewModel
    {
        public int CurrentAttempt = 0;

        public ChangeMobNafathPageViewMode(INavigationService navigationService, IDialogService dialogService):base(navigationService, dialogService)

        {
        }

        public void Goback()
        {
            _navigationService.GoBack();
        }

       
    }
}

