using System;
using System.Threading.Tasks;
using AppDynamics.Agent;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel.EstablishmentSignUPVM
{
    [Preserve(AllMembers = true)]
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

