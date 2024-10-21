using System.Collections.ObjectModel;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.InternationalMobileNumber
{
    public class InternationalMobileNumberCodePagesViewModel : BaseViewModel
    {
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
                _mobileCodes = value;
                OnPropertyChanged("MobileCodes");
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
                _ibanNumberText = value;
                OnPropertyChanged("InternationalMobileCodeText");
            }
        }
        private string _txtCountryCode = string.Empty;
        public string TxtCountryCode
        {
            get
            {
                return _txtCountryCode;
            }
            set
            {

                _txtCountryCode = value;
                OnPropertyChanged("TxtCountryCode");
            }
        }
        #region Constructor
        public InternationalMobileNumberCodePagesViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }
        #endregion
        public async Task onPageLoad()
        {

            mobileData = await WebServiceManager.GAZTGetMobileRegionDropdown();
            if (mobileData != null && mobileData.Count != 0)
            {
                MobileCodes = mobileData;
                MobileCodes = new ObservableCollection<InternationalMobileData>(MobileCodes.OrderBy(x => x.Telefto).ToList());

            }
        }

        public void refreshList()
        {
            MobileCodes = mobileData;
        }
    }
}

