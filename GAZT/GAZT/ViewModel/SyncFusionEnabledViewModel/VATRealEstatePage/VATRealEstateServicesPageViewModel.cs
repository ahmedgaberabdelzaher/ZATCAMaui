using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Models;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.VATRealEstatePage
{
    [Preserve(AllMembers = true)]

    public class VATRealEstateServicesPageViewModel : ViewModelBase
    {
        private ObservableCollection<eServiceInfo> _eServicesItems = null;
        private ICommand EserviceCommand { get; set; }
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

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
        public void PopulateServicesData()
        {
            eServicesAvailableToTheTP = new ObservableCollection<eServiceInfo>();
            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZVATRealEstatePropertyRegistration, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_Form_Bundle_Status.png" });
            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZVATRealEstateRequestVerification, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_My_Bills.png" });
            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZVATRealEstateTerminationOfRequest, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_My_Certificate.png" });
        }
    }
}
