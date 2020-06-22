using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage
{
    public class VATIndividualSignupPageViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        
        public Command IndividualRegistrationCommand { get; set; }
        public VATIndividualSignupPageViewModel(INavigationService navigationService, IDialogService dialogService)
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

            this.IndividualRegistrationCommand = new Command(this.OnIndividualRegistrationClicked);
        }
        private void OnIndividualRegistrationClicked(object obj)
        {
            _navigationService.NavigateTo(App.IndividualRegistrationPageView);
        }
    }
}
