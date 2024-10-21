
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using static ZATCAMAUI.Models.EscalatedGstcModel;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.EscalatedCasesGSCTCViewModel
{
    public class EscalatedCasesGSTCPageViewModel : BaseViewModel
    {
        public ICommand GoBackBtnTapped { get; set; }

        private bool _isSearchButtonVisible = true;

        public bool IsSearchButtonVisible
        {
            get
            {
                return _isSearchButtonVisible;
            }

            set
            {
                if (_isSearchButtonVisible == value) return;

                _isSearchButtonVisible = value;
                OnPropertyChanged("IsSearchButtonVisible");
            }
        }

        private bool _isCloseButtonVisible = false;

        public bool IsCloseButtonVisible
        {
            get
            {
                return _isCloseButtonVisible;
            }

            set
            {
                if (_isCloseButtonVisible == value) return;

                _isCloseButtonVisible = value;
                OnPropertyChanged("IsCloseButtonVisible");
            }
        }

        public bool _isListVisible = false;
        public bool IsListVisible
        {
            get
            {
                return _isListVisible;
            }
            set
            {
                if (_isListVisible == value) return;

                _isListVisible = value;
                NoDataAvailable = !_isListVisible;
                OnPropertyChanged("IsListVisible");
            }
        }

        private bool _isLoading = false;

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (_isLoading == value) return;

                _isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }

        public bool _noDataAvailable = false;
        public bool NoDataAvailable
        {
            get
            {
                return _noDataAvailable;
            }
            set
            {
                if (_noDataAvailable == value) return;

                _noDataAvailable = value;
                OnPropertyChanged("NoDataAvailable");
            }
        }
        public bool _isAppLangEn = false;
        public bool IsAppLangEn
        {
            get
            {
                return _isAppLangEn;
            }
            set
            {
                if (_isAppLangEn == value) return;
                _isAppLangEn = value;
                OnPropertyChanged("IsAppLangEn");
            }
        }

        public void PropChnaged(string property)
        {
            OnPropertyChanged(property);

        }
        public bool _isAppLangAr = false;
        public bool IsAppLangAr
        {
            get
            {
                return _isAppLangAr;
            }
            set
            {
                if (_isAppLangAr == value) return;
                _isAppLangAr = value;

                OnPropertyChanged("IsAppLangAr");
            }
        }




        public string _searchText = "";
        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                _searchText = value;

                OnPropertyChanged("SearchText");
            }
        }
      
        public ObservableCollection<CaseDetailsResultSet> _caseDetailedListViewData { get; set; }

        public ObservableCollection<CaseDetailsResultSet> CaseDetailedListViewData
        {
            get { return _caseDetailedListViewData; }

            set
            {
                if (_caseDetailedListViewData == value)
                {
                    return;
                }

                _caseDetailedListViewData = value;
                OnPropertyChanged("CaseDetailedListViewData");
            }
        }

        private ObservableCollection<CaseDetailsResultSet> _copiedCaseDetailedListViewData = new ObservableCollection<CaseDetailsResultSet>();
        public ObservableCollection<CaseDetailsResultSet> CopiedCaseDetailedListViewData
        {
            get
            {
                return _copiedCaseDetailedListViewData;
            }
            set
            {
                if (_copiedCaseDetailedListViewData == value) return;

                _copiedCaseDetailedListViewData = value;

                OnPropertyChanged("CopiedCaseDetailedListViewData");
            }
        }

        #region Constructor
        public EscalatedCasesGSTCPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            GoBackBtnTapped = new Command(() =>
            {
                _navigationService.GoBack();
            });
        }

        public ICommand LinkCommand
        {
            get
            {
                return new Command<CaseDetailsResultSet>(async (obj) =>
                {

                    Uri uri = new Uri(obj.Link);
                    await Browser.OpenAsync(uri);

                });
            }
        }


        #endregion

        public void FilterWithReferenceNumber()
        {

            CopiedCaseDetailedListViewData = new ObservableCollection<CaseDetailsResultSet>(CaseDetailedListViewData.Where(searchedObjects => searchedObjects.CaseNumber.Contains(SearchText)));
        }

        public async Task GetGstcCaseDetailSet()
        {
            IsLoading = true;

            try
            {
                var escalatedGstcModel = await EscalatedCasesWebserviceManager.GAZTGetCaseDetailSet();
                if (escalatedGstcModel != null && escalatedGstcModel?.d?.CaseDetailSet?.results?.Count > 0)
                {
                    CaseDetailedListViewData = new ObservableCollection<CaseDetailsResultSet>(escalatedGstcModel.d.CaseDetailSet.results);
                    CopiedCaseDetailedListViewData = CaseDetailedListViewData;
                    IsListVisible = true;
                   // NoDataAvailable = false;
                }
                else
                {
                   // NoDataAvailable = true;
                    IsListVisible = false;
                }


                IsLoading = false;
            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                IsLoading = false;

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (InternetException ex)
            {
                IsLoading = false;
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }

        }







    }


}
