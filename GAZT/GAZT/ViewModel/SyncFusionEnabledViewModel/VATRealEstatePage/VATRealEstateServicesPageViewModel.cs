using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.VATRealEstatePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class VATRealEstateServicesPageViewModel : ViewModelBase
    {
        private ObservableCollection<eServiceInfo> _eServicesItems = null;
        private ICommand EserviceCommand { get; set; }
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand BackButtonClicked { get; set; }
        public ICommand PropertyRegistrationCommand { get; set; }
        public ICommand RequestVerificationCommand { get; set; }
        public ICommand TerminateRequestCommand { get; set; }


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
        public VATRealEstateServicesPageViewModel(INavigationService navigationService, IDialogService dialogService) //: base(navigationService, dialogService)
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
            BackButtonClicked = new Xamarin.Forms.Command(() =>
            {
                _navigationService.GoBack();
            });

            this.PropertyRegistrationCommand = new Command(this.PropertyRegistrationCommandClicked);
            this.RequestVerificationCommand = new Command(this.RequestVerificationCommandClicked);
            this.TerminateRequestCommand = new Command(this.TerminateRequestCommandClicked);

        }
        private async void PropertyRegistrationCommandClicked(object obj)
        {

            await Task.Run(() =>
            {
                IsLoading = true;
            });

            Device.BeginInvokeOnMainThread(() =>
            {
                PropertyRegistrationPageViewModel.reServiceName = AppResources.ZVATRealEstatePropertyRegistration;

                _navigationService.NavigateTo(App.PropertyRegistrationPage);
                 IsLoading = false;
            });


        }
        private async void  RequestVerificationCommandClicked(object obj)
        {

            await Task.Run(() =>
            {
                IsLoading = true;
            });

            Device.BeginInvokeOnMainThread(() =>
            {
                PropertyRegistrationPageViewModel.reServiceName = AppResources.ZVATRealEstateRequestVerification;

                _navigationService.NavigateTo(App.PropertyRegistrationPage);
                 IsLoading = false;
            });


        }
        private async void TerminateRequestCommandClicked(object obj)
        {

            await Task.Run(() =>
            {
                IsLoading = true;
            });

            Device.BeginInvokeOnMainThread(() =>
            {
                PropertyRegistrationPageViewModel.reServiceName = AppResources.ZVATRealEstateTerminationOfRequest;

                _navigationService.NavigateTo(App.PropertyRegistrationPage);
                 IsLoading = false;
            });


        }
        public ObservableCollection<eServiceInfo> eServicesAvailableToTheTP
        {
            get
            {
                return this._eServicesItems;
            }
            set
            {
                this._eServicesItems = value;
                this.RaisePropertyChanged("eServicesAvailableToTheTP");
            }
        }

        public void PopulateServicesData()
        {
            eServicesAvailableToTheTP = new ObservableCollection<eServiceInfo>();
            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZVATRealEstatePropertyRegistration, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_Payment.png" });
            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZVATRealEstateRequestVerification, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_VerifyRequest.png" });
            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZVATRealEstateTerminationOfRequest, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_Cancel_payment.png" });
        }
    }
}
