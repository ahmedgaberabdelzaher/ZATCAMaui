using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Manager;
using ZATCAMAUI.Models.UpdateEffDateModel;
using static ZATCAMAUI.ViewModel.NewDesignViewModel.UpdateVatEffectiveDateVM.FilterVatEffectiveDatePageViewModel;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.UpdateVatEffectiveDateVM
{
    public class UpdateVatEffectiveDateViewModel : BaseViewModel
    {
        public UpdateVatEffectiveDateModel LogResponse;
        public ICommand AddNewRequestTapped { get; set; }

       
        private bool _noDataAvailable = false;
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
        private bool _isListVisible = true;
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
                OnPropertyChanged("IsListVisible");
            }
        }

        private ObservableCollection<ItemSetResult> _vatLogs;
        public ObservableCollection<ItemSetResult> VatLogs
        {
            get
            {
                return _vatLogs;
            }
            set
            {
                if (_vatLogs == value) return;

                _vatLogs = value;

                OnPropertyChanged("VatLogs");
            }
        }
        private ObservableCollection<ItemSetResult> _copiedVatLogs = new ObservableCollection<ItemSetResult>();
        public ObservableCollection<ItemSetResult> CopiedVatLogs
        {
            get
            {
                return _copiedVatLogs;
            }
            set
            {
                if (_copiedVatLogs == value) return;

                _copiedVatLogs = value;

                OnPropertyChanged("CopiedVatLogs");
            }
        }

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


        public UpdateVatEffectiveDateViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            AddNewRequestTapped = new Command(() => { NavigateToVatUpdateForm(); });
            handleFilterData();
        }

        private void NavigateToVatUpdateForm()
        {

            if (!string.IsNullOrEmpty(LogResponse?.d?.RegTp) && LogResponse?.d?.RegTp == "RGVT")
            {
                App.isVatEffectDateNav = true;
                App.VATType = PageExecutionType.Amend;
                _navigationService.NavigateTo(App.VATAmendReactivationPageView);
            }

        }

        public async Task GetAllVatEffectiveDateLogs()
        {
            List<ItemSetResult> VatLogss = new List<ItemSetResult>();
            IsLoading = true;
            VatLogs?.Clear();
            CopiedVatLogs?.Clear();

            LogResponse = await VatEffectiveDateWebServiceManager.GAZTGetIBanAccounts();

            await PopToRootPage();// If seesion Expired it will navigate to Dashboard page
            IsLoading = false;

            if (LogResponse != null && LogResponse.d != null && LogResponse.d.ItemSet != null
            && LogResponse.d.ItemSet != null && LogResponse.d.ItemSet.Count > 0)
            {
                try
                {
                    IsListVisible = true;
                    NoDataAvailable = false;
                    //VatLogss = ;
                    VatLogs = new ObservableCollection<ItemSetResult>(LogResponse.d.ItemSet);
                    CopiedVatLogs.Clear();
                    CopiedVatLogs = VatLogs;



                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());
                    IsLoading = false;
                }

            }
            else
            {
                IsListVisible = false;
                NoDataAvailable = true;
            }
            IsLoading = false;
        }

        public void FilterWithReferenceNumber()
        {
            // CopiedVatLogs.Clear();
            CopiedVatLogs = new ObservableCollection<ItemSetResult>(VatLogs.Where(searchedObjects => searchedObjects.Fbnum.Contains(SearchText)));

        }
        public void FiltersClicked()
        {
            _navigationService.NavigateTo(App.FilterVatEffectiveDatePageView);
            /*IsSortByVisible = !IsSortByVisible;*/
        }

        public void handleFilterData()
        {
            MessagingCenter.Unsubscribe<App, List<VatEffectDateFilterModel>>(this, "filterList");

            MessagingCenter.Subscribe<App, List<VatEffectDateFilterModel>>(this, "filterList", (sender, arg) =>
            {
                // MessagingCenter.Unsubscribe<App, List<VatEffectDateFilterModel>>(this, "filterList");

                List<VatEffectDateFilterModel> FilterList = new List<VatEffectDateFilterModel>();
                FilterList = arg;

                if (FilterList != null && FilterList.Count > 0)
                {
                    for (int i = 0; i < FilterList.Count; i++)
                    {
                        //filter Id=1 if filter is DateType
                        if (FilterList[i].filterId == 2)
                        {
                            CopiedVatLogs = new ObservableCollection<ItemSetResult>(VatLogs.Where(filteredObjects => filteredObjects.UpdatedBy == FilterList[i].filterName));
                        }
                        //filter Id=1 if filter is DateType
                        else if (FilterList[i].filterId == 1)
                        {
                            if (FilterList[i].filterName == AppResources.EffectSortfromOldToNew)
                            {
                                CopiedVatLogs = new ObservableCollection<ItemSetResult>(CopiedVatLogs.OrderBy(s => s.EffDtAfter));
                                //new UpdateVatEffectiveDatePageView().SortListInAscendingOrder();
                            }
                            else if (FilterList[i].filterName == AppResources.EffectSortfromNewToOld)
                            {
                                CopiedVatLogs = new ObservableCollection<ItemSetResult>(CopiedVatLogs.OrderByDescending(s => s.EffDtAfter));
                                // new UpdateVatEffectiveDatePageView().SortListInDescendingOrder();
                            }
                        }
                    }
                    OnPropertyChanged("CopiedVatLogs");
                }

                if (CopiedVatLogs.Count > 0)
                {
                    NoDataAvailable = false;
                    IsListVisible = true;
                }
                else
                {
                    NoDataAvailable = true;
                    IsListVisible = false;
                }

            });
        }
    }
}


