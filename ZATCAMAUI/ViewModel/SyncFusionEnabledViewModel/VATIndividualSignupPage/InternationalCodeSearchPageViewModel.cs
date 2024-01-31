using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using ZATCAMAUI.Models;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage
{
 
    public class InternationalCodeSearchPageViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand BackButtonClicked { get; set; }
        ObservableCollection<InternationalMobileData> mobileData = null;

        public ObservableCollection<InternationalMobileData> _mobileCodes;
        public ObservableCollection<InternationalMobileData> MobileCodes
        {
            get
            {
                return _mobileCodes;
            }
            set
            {
                if (_mobileCodes == value) return;
                _mobileCodes = value;
                RaisePropertyChanged("MobileCodes");
            }
        }


        public ObservableCollection<InternationalMobileData> _mobileCodesAllValues { get; set; }
        public ObservableCollection<InternationalMobileData> MobileCodesAllValues
        {
            get
            {
                return _mobileCodesAllValues;
            }
            set
            {
                if (_mobileCodesAllValues == value) return;

                _mobileCodesAllValues = value;
                RaisePropertyChanged("MobileCodesAllValues");
            }
        }

        public ObservableCollection<InternationalMobileData> _filteredItems;
        public ObservableCollection<InternationalMobileData> FilteredItems
        {
            get
            {
                return _filteredItems;
            }
            set
            {
                if (_filteredItems == value) return;

                _filteredItems = value;
                RaisePropertyChanged("FilteredItems");
            }
        }
        private string _ibanNumberText;
        public string InternationalMobileCodeText
        {
            get
            {
                return _ibanNumberText;
            }
            set
            {
                if (_ibanNumberText == value) return;

                _ibanNumberText = value;
                RaisePropertyChanged("InternationalMobileCodeText");
            }
        }

        #region Constructor
        public InternationalCodeSearchPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            BackButtonClicked = new Command(() =>
            {
                _navigationService.GoBack();
            });
            _navigationService = navigationService;
            _dialogService = dialogService;
            MobileCodesAllValues = new ObservableCollection<InternationalMobileData>();
        }

        #endregion
        public void onPageLoad()
        {
            try
            {
                MobileCodes = new ObservableCollection<InternationalMobileData>(MobileCodesAllValues.OrderBy(x => x.Telefto).ToList());
            }
            catch (Exception)
            {

            }
        }

        public void refreshList()
        {
            if (mobileData != null)
            {
                MobileCodes = mobileData;
            }
        }
    }
}
