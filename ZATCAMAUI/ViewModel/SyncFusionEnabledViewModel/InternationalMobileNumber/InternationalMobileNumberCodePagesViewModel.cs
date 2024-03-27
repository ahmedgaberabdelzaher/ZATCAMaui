using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.InternationalMobileNumber
{
    public class InternationalMobileNumberCodePagesViewModel : BaseViewModel
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
                _mobileCodes = value;
                RaisePropertyChanged("MobileCodes");
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
                _ibanNumberText = value;
                RaisePropertyChanged("InternationalMobileCodeText");
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
                RaisePropertyChanged("TxtCountryCode");
            }
        }
        #region Constructor
        public InternationalMobileNumberCodePagesViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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

        }
        #endregion
        public void onPageLoad()
        {

            mobileData = WebServiceManager.GAZTGetMobileRegionDropdown();
            if (mobileData != null && mobileData.Count != 0)
            {
                MobileCodes = mobileData;
                MobileCodes = new ObservableCollection<InternationalMobileData>(MobileCodes.OrderBy(x => x.Telefto).ToList());

            }
            else
            {

            }
        }

        public void refreshList()
        {
            MobileCodes = mobileData;
        }
    }
}

