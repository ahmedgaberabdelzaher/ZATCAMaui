using EGAZT.Enums;
using EGAZT.Manager;
using EGAZT.Models.UpdateEffDateModel;
using EGAZT.Views.NewDesign.UpdateVatEffectiveDate;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using static EGAZT.ViewModel.NewDesignViewModel.UpdateVatEffectiveDateVM.FilterVatEffectiveDatePageViewModel;

namespace EGAZT.ViewModel.NewDesignViewModel.UpdateVatEffectiveDateVM
{
    [Preserve(AllMembers = true)]
    public class UpdateVatEffectiveDateViewModel : BaseViewModel
    {
        public UpdateVatEffectiveDateModel LogResponse;
        public ICommand GoBackBtnTapped { get; set; }
        public ICommand AddNewRequestTapped { get; set; }

        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                if (_isLoading == value) return;
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }
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
                RaisePropertyChanged("NoDataAvailable");
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
                RaisePropertyChanged("IsListVisible");
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

                RaisePropertyChanged("VatLogs");
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

                RaisePropertyChanged("CopiedVatLogs");
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
                RaisePropertyChanged("IsSearchButtonVisible");
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

                RaisePropertyChanged("SearchText");
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
                RaisePropertyChanged("IsCloseButtonVisible");
            }
        }


        public UpdateVatEffectiveDateViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            GoBackBtnTapped = new Command(() =>
            {
                _navigationService.GoBack();
            });
            AddNewRequestTapped = new Command(() => { NavigateToVatUpdateForm(); });
            handleFilterData();
        }

        private void NavigateToVatUpdateForm()
        {

            if (LogResponse.d.RegTp == "RGVT")
            {
                App.isVatEffectDateNav = true;
                App.VATType = PageExecutionType.Amend;
                _navigationService.NavigateTo(App.VATAmendReactivationPageView);
            }

        }

        public async void GetAllVatEffectiveDateLogs()
        {
            List<ItemSetResult> VatLogss = new List<ItemSetResult>();
            await Task.Run(() =>
            {
                IsLoading = true;
            });

            await Task.Run(async () =>
            {
                LogResponse = await VatEffectiveDateWebServiceManager.GAZTGetIBanAccounts();

                PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                IsLoading = false;

                if (LogResponse != null && LogResponse.d != null && LogResponse.d.ItemSet != null
                && LogResponse.d.ItemSet.results != null && LogResponse.d.ItemSet.results.Count > 0)
                {
                    try
                    {
                        IsListVisible = true;
                        NoDataAvailable = false;
                        //VatLogss = ;
                        VatLogs = new ObservableCollection<ItemSetResult>(LogResponse.d.ItemSet.results);
                        CopiedVatLogs.Clear();
                        CopiedVatLogs = VatLogs;


                        //ModifyingDate=UtilityManager.FormatDateToYYYYDDMMFromDateTypeString(VatLogs.)


                        /*MainListData = IbanAccounts.d.IbanListSet.results;

                        var RejectMatch = MainListData.Find(selectedValue => (selectedValue.StatusDesc == "Rejected") || (selectedValue.StatusDesc == "مرفوض"));
                        var MissingIfoMatch = MainListData.Find(selectedValue => (selectedValue.StatusDesc == "Missing Informaiton") || (selectedValue.StatusDesc == "معلومات الحساب غير مكتملة"));


                        if (MissingIfoMatch != null || RejectMatch != null)
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDIBANIncomplete));

                        }*/
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
            });
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
                            CopiedVatLogs = new ObservableCollection<ItemSetResult>(CopiedVatLogs.Where(filteredObjects => filteredObjects.UpdatedBy == FilterList[i].filterName));
                        }
                        //filter Id=1 if filter is DateType
                        else if (FilterList[i].filterId == 1)
                        {
                            if (FilterList[i].filterName == AppResources.EffectSortfromOldToNew)
                            {
                                new UpdateVatEffectiveDatePageView().SortListInAscendingOrder();
                            }
                            else if (FilterList[i].filterName == AppResources.EffectSortfromNewToOld)
                            {
                                new UpdateVatEffectiveDatePageView().SortListInDescendingOrder();

                            }
                        }
                    }
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

