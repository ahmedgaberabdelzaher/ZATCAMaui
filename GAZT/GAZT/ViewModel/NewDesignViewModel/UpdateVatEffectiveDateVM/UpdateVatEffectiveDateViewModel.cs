using EGAZT.Manager;
using EGAZT.Models.UpdateEffDateModel;
using GalaSoft.MvvmLight.Views;
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

namespace EGAZT.ViewModel.NewDesignViewModel.UpdateVatEffectiveDateVM
{
    [Preserve(AllMembers = true)]
    public class UpdateVatEffectiveDateViewModel : BaseViewModel
    {

        public ICommand GoBackBtnTapped { get; set; }

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
        private ObservableCollection<ItemSetResult> _copiedVatLogs=new ObservableCollection<ItemSetResult>();
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
                UpdateVatEffectiveDateModel LogResponse = await VatEffectiveDateWebServiceManager.GAZTGetIBanAccounts();

                PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                    IsLoading = false;

                if (LogResponse != null && LogResponse.d != null && LogResponse.d.ItemSet != null
                && LogResponse.d.ItemSet.results != null && LogResponse.d.ItemSet.results.Count > 0)
                {
                    try
                    {
                        //VatLogss = ;
                        VatLogs = new ObservableCollection<ItemSetResult>(LogResponse.d.ItemSet.results);
                        CopiedVatLogs.Clear();
                        CopiedVatLogs=VatLogs; 


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
    }
}

