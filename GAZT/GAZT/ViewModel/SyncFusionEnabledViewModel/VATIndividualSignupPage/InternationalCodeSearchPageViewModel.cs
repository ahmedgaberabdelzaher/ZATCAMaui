using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EGAZT.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage
{
    public class InternationalCodeSearchPageViewModel: ViewModelBase
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
        BackButtonClicked = new Xamarin.Forms.Command(() =>
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
