using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage
{
    public class VATIndividualSignupPageViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        
        public Command IndividualRegistrationCommand { get; set; }
        public Command EstablishmentSignupCommand { get; set; }


        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }
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
            this.EstablishmentSignupCommand = new Command(this.OnEstablishmentSignupClicked);
        }
        //EstablishmentSignupCommand
        private async void OnIndividualRegistrationClicked(object obj)
        {

            await Task.Run(() =>
            {
                IsLoading = true;
            });
            
            Device.BeginInvokeOnMainThread( () =>
          {
              _navigationService.NavigateTo(App.VATIndividualSignupTnCPageView);
             // IsLoading = false;
          });

            
        }
        private async void OnEstablishmentSignupClicked(object obj)
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });

            Device.BeginInvokeOnMainThread(() =>
            {
                _navigationService.NavigateTo(App.SignUpTAndCViewPage);
                // IsLoading = false;
            });


        }
    }
}
