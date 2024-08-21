using System.Collections.ObjectModel;
using System.Windows.Input;
using Mopups.Services;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Manager;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.UpdateEffDateModel;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using static ZATCAMAUI.Models.ZakatExemptionListModel;
using StatusSetResult = ZATCAMAUI.Models.ZakatExemptionListModel.StatusSetResult;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{
    
    public class ZakatExemptionRequestListViewModel : BaseViewModel
    {
        public ZakatExemptionListModel zakatExemptionListModel;
        public ZakatExemptionModel zakatExemptionModel;

        public ICommand CreateNewRequestTapped { get; set; }
        private Command itemTappedCommand;

        public Command ItemTappedCommand
        {
            get { return itemTappedCommand; }
            protected set { itemTappedCommand = value; }
        }

        private Command downloadCertCommand;

        public Command DownloadCertCommand
        {
            get { return downloadCertCommand; }
            protected set { downloadCertCommand = value; }
        }

        

        #region Properies
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

        private bool _isLoading = true;

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
        

        public string _searchText = "";
        /// <summary>
        /// holds the user entered text in search field.
        /// </summary>    
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
        public List<StatusSetResult> _TaxTypeForFilter = null;
        public List<StatusSetResult> TaxTypeForFilter
        {
            get
            {
                return _TaxTypeForFilter;
            }
            set
            {
                if (_TaxTypeForFilter == value) return;

                _TaxTypeForFilter = value;
                OnPropertyChanged("TaxTypeForFilter");
            }
        }

        
        public string _selectedStatusForFilter = null;
        public string SelectedStatusForFilter
        {
            get
            {
                return _selectedStatusForFilter;
            }
            set
            {

                _selectedStatusForFilter = value;
                if (_selectedStatusForFilter != null)
                {
                    //FilterLabelText = _selectedStatusForFilter.d;
                    //FilterOnBasisOfTaxType();

                }
                OnPropertyChanged("SelectedStatusForFilter");
            }

        }

        private GenericPickerModel _pickerModel { get; set; }
        public GenericPickerModel PickerModel
        {
            get { return _pickerModel; }
            set
            {
                if (_pickerModel == value) return;

                _pickerModel = value;
                OnPropertyChanged("PickerModel");
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

        public ObservableCollection<ZakatExemptionListModel.FbnumListSetResult> _zakatExemptionListViewData { get; set; }

        public ObservableCollection<ZakatExemptionListModel.FbnumListSetResult> ZakatExemptionListViewData
        {
            get { return _zakatExemptionListViewData; }

            set
            {
                if (_zakatExemptionListViewData == value)
                {
                    return;
                }

                _zakatExemptionListViewData = value;
                OnPropertyChanged("ZakatExemptionListViewData");
            }
        }

        private ObservableCollection<ZakatExemptionListModel.FbnumListSetResult> _copiedZakatExemptionListViewData = new ObservableCollection<ZakatExemptionListModel.FbnumListSetResult>();
        public ObservableCollection<ZakatExemptionListModel.FbnumListSetResult> CopiedZakatExemptionListViewData
        {
            get
            {
                return _copiedZakatExemptionListViewData;
            }
            set
            {
                if (_copiedZakatExemptionListViewData == value) return;

                _copiedZakatExemptionListViewData = value;

                OnPropertyChanged("CopiedZakatExemptionListViewData");
            }
        }

        #endregion


        


        #region Constructor
        public ZakatExemptionRequestListViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

            CreateNewRequestTapped = new Command(this.CreateNewRequest);
            itemTappedCommand = new Command(OnItemTapped);

            downloadCertCommand = new Command(OnCertificateDownload);
        }

        
        #endregion


        public async void CreateNewRequest()
        {   
            try
            {

                ZakatExemptionModel zakatExemptionModel = new ZakatExemptionModel();

                //// IsLoading = true;
                //try
                //{
                //    //zakatExemptionModel = await ZakatExemptionWebServiceManager.GetRequestToZakatExemtionRequest(null);
                //   // if (zakatExemptionModel != null)
                //    {
                //        // IsLoading = false;
                //    }
                //}
                //catch (Exception ex)
                //{
                //    // IsLoading = false;
                //}

                _navigationService.NavigateTo(App.ZakatExemptionPageView, zakatExemptionModel);

            }

            catch (Exception ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        

        ///<summary>
        ///To display tapped item content
        ///</summary>
        public async void OnItemTapped(object obj)
        {
            var fbnumListSet = obj as ZakatExemptionListModel.FbnumListSetResult;

            if (fbnumListSet.DispErr)
            {
                MainThread.BeginInvokeOnMainThread(async () => {
                    await _dialogService.ShowMessageBox(AppResources.ZakatExemptiomRequestMessage, AppResources.Information);
                });
            }
            else
            {
                zakatExemptionModel = await ZakatExemptionWebServiceManager.GetRequestToZakatExemtionRequest(fbnumListSet);
                zakatExemptionModel.d = zakatExemptionModel.data;
                if (fbnumListSet.Fbsta.Equals("IP011") && (fbnumListSet.Fbust.Equals("E0019") || fbnumListSet.Fbust.Equals("E0031")))
                {
                    zakatExemptionModel.d.OpenPageOnEdit = true;

                }
                else
                {
                    zakatExemptionModel.d.OpenPageOnEdit = true;
                }

                try
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        _navigationService.NavigateTo(App.ZakatExemptionPageView, zakatExemptionModel);
                    });
                }

                catch (Exception ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        _navigationService.GoBack();
                    });
                }
            }
        }

        private void OnCertificateDownload(object obj)
        {
            var fbnumListSet = obj as ZakatExemptionListModel.FbnumListSetResult;
            {
                String downloadurl = ZATCAConstants.ZakatExemtionDownloadCert + fbnumListSet.Fbnum ;
                _navigationService.NavigateTo(App.PdfView, downloadurl);
            }
        }



        public void FilterWithReferenceNumber()
        {

            CopiedZakatExemptionListViewData = new ObservableCollection<FbnumListSetResult>(ZakatExemptionListViewData.Where(searchedObjects => searchedObjects.Fbnum.Contains(SearchText)));
        }

        public void FilterWithStatus(string selectedStatusDescription)
        {
           CopiedZakatExemptionListViewData = new ObservableCollection<FbnumListSetResult>(ZakatExemptionListViewData.Where(searchedObjects => searchedObjects.StatusDesc.Equals(selectedStatusDescription, StringComparison.CurrentCultureIgnoreCase)));
        }
        

        public void PopulateStatusTypeList()
        {
            if (PickerModel != null)
            {

                PickerModel = null;
            }
            var list = new List<string>();

            foreach (StatusSetResult dropdown in zakatExemptionListModel.D.StatusSet)
            {
                try
                {
                    list.Add(dropdown.Description);
                }
                catch (Exception ex)
                {

                }
            }
            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = list;
            genericPickerModel.PickerTitle = "";
            genericPickerModel.PickerId = "PickerSelectedItem";
            PickerModel = genericPickerModel;
            SelectedStatusForFilter = SelectedStatusForFilter;

            PickerModel = genericPickerModel;
        }

        public void FilterOnBasisOfTaxType()
        {
            
        }
        

        public void FiltersClicked()
        {
            showPickerDialog();
        }
        public async void showPickerDialog()
        {
            try
            {
                PopulateStatusTypeList();
                if (PickerModel != null)
                    await MopupService.Instance.PushAsync(new PickerPageView(PickerModel));
            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public async Task GetDetailsForZakatExeListAsync()
        {
            IsLoading = true;
            Thread.Sleep(1000);

            try
            {
                zakatExemptionListModel = await ZakatExemptionWebServiceManager.GetZakatExemptionListRequest();

                if (zakatExemptionListModel != null && zakatExemptionListModel.D != null && zakatExemptionListModel.D.FbnumListSet.Count() > 0)
                {
                    ZakatExemptionListViewData = new ObservableCollection<ZakatExemptionListModel.FbnumListSetResult>(zakatExemptionListModel.D.FbnumListSet);
                    CopiedZakatExemptionListViewData.Clear();
                    CopiedZakatExemptionListViewData = ZakatExemptionListViewData;

                    if (ZakatExemptionListViewData.Count() > 0)
                    {
                        IsListVisible = true;
                    }
                    else
                    {
                        IsListVisible = false;
                    }

                }
                else
                {
                    IsListVisible = false;
                }
                IsLoading = false;
            }
            catch (Exception ex)
            {
                IsLoading = false;
            }
        }
    }



}
