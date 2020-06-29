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
        }
        private async void OnIndividualRegistrationClicked(object obj)
        {
            ////IsLoading
            ////      Device.BeginInvokeOnMainThread(async () =>
            ////      {
            //         await  Task.Run(() =>
            //          {
            //             IsLoading = true;
            //          });
            ////  });
            //await Task.Run(() =>
            //{
            //    _navigationService.NavigateTo(App.IndividualRegistrationPageView);
            //});

            //await Task.Run(() =>
            //{
            //    IsLoading = false;
            //});

            await Task.Run(() =>
            {
                IsLoading = true;
            });
            
            Device.BeginInvokeOnMainThread( () =>
          {
              _navigationService.NavigateTo(App.IndividualRegistrationPageView);
              IsLoading = false;
          });

            
        }
    }
}
