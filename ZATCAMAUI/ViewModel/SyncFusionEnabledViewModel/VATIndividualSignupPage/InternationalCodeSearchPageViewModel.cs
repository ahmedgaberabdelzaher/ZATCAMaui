using System.Collections.ObjectModel;
using System.Windows.Input;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage
{
 
    public class InternationalCodeSearchPageViewModel : BaseViewModel
    {
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
                OnPropertyChanged("MobileCodes");
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
                OnPropertyChanged("MobileCodesAllValues");
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
                OnPropertyChanged("FilteredItems");
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
                OnPropertyChanged("InternationalMobileCodeText");
            }
        }

        #region Constructor
        public InternationalCodeSearchPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            BackButtonClicked = new Command(() =>
            {
                _navigationService.GoBack();
            });
            MobileCodesAllValues = new ObservableCollection<InternationalMobileData>();
        }

        #endregion
        public void onPageLoad()
        {
            try
            {
                if(MobileCodes == null || MobileCodes.Count == 0)
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
