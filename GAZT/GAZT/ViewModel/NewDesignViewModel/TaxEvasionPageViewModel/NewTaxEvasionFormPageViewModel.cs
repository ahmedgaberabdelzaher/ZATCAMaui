using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.Models.EnumModels;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Plugin.FilePicker;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.TaxEvasionPageViewModel
{
    public class NewTaxEvasionFormPageViewModel : BaseViewModel
    {
        public  readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        private bool _IsTnameHasError  = false;
        public bool IsTnameHasError
        {
            get
            {
                return _IsTnameHasError;
            }
            set
            {
                _IsTnameHasError = value;
                RaisePropertyChanged("IsTnameHasError");
            }
        }
        private bool _IsTmobileHasError = false;
        public bool IsTmobileHasError
        {
            get
            {
                return _IsTmobileHasError;
            }
            set
            {
                _IsTmobileHasError = value;
                RaisePropertyChanged("IsTmobileHasError");
            }
        }
        private bool _IsFacilityNameHasError = false;
        public bool IsFacilityNameHasError
        {
            get
            {
                return _IsFacilityNameHasError;
            }
            set
            {
                _IsFacilityNameHasError = value;
                RaisePropertyChanged("IsFacilityNameHasError");
            }
        }
        private bool _IsRegionHasError = false;
        public bool IsRegionHasError
        {
            get
            {
                return _IsRegionHasError;
            }
            set
            {
                _IsRegionHasError = value;
                RaisePropertyChanged("IsRegionHasError");
            }
        }
        private bool _IsCityHasError = false;
        public bool IsCityHasError
        {
            get
            {
                return _IsCityHasError;
            }
            set
            {
                _IsCityHasError = value;
                RaisePropertyChanged("IsCityHasError");
            }
        }
        private bool _IsFDHasError = false;
        public bool IsFDHasError
        {
            get
            {
                return _IsFDHasError;
            }
            set
            {
                _IsFDHasError = value;
                RaisePropertyChanged("IsFDHasError");
            }
        }
        private bool _IsFSHasError = false;
        public bool IsFSHasError
        {
            get
            {
                return _IsFSHasError;
            }
            set
            {
                _IsFSHasError = value;
                RaisePropertyChanged("IsFSHasError");
            }
        }
        private bool _IsReportDetailHasError = false;
        public bool IsReportDetailHasError
        {
            get
            {
                return _IsReportDetailHasError;
            }
            set
            {
                _IsReportDetailHasError = value;
                RaisePropertyChanged("IsReportDetailHasError");
            }
        }
        //IsLoading
        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }
        #region Variable
        private NewTaxEvasionTabEnum _currentTab = NewTaxEvasionTabEnum.ReporterInfo;
        public NewTaxEvasionTabEnum currentTab
        {
            get => _currentTab;
            private set
            {
                _currentTab = value;
                RaisePropertyChanged(nameof(currentTab));
                CurrentIndex = (int)_currentTab;
                RaisePropertyChanged(nameof(CurrentIndex));
            }
        }
        private int _currenrIndex = 1;
        public int CurrentIndex
        {
            get => _currenrIndex;
            set
            {
                _currenrIndex = value;
                RaisePropertyChanged(nameof(CurrentIndex));
                if (_currenrIndex == MaxIndex)
                {
                    MarkComplete = true;
                    RaisePropertyChanged(nameof(MarkComplete));
                }
            }
        }
        public bool MarkComplete { get; private set; } = false;
        public int MaxIndex { get; private set; } = 4;
        public static Decimal AttachmentUploadedSize;
        byte[] attachment;
        #endregion

        #region Property
        private string _PageTitle = AppResources.NDReporterInformation;
        public string PageTitle
        {
            get
            {
                return _PageTitle;
            }
            set
            {
                _PageTitle = value;
                RaisePropertyChanged("PageTitle");
            }
        }

        private string _BodyTitle = AppResources.ZZZZCompletethebelowdetails;
        public string BodyTitle
        {
            get
            {
                return _BodyTitle;
            }
            set
            {
                _BodyTitle = value;
                RaisePropertyChanged("BodyTitle");
            }
        }
        //Form
        private string _tName = string.Empty;
        public string TName
        {
            get
            {
                return _tName;
            }
            set
            {
                try
                {
                    _tName = value;
                    RaisePropertyChanged("TName");
                }
                catch (Exception ex)
                {
                }
            }
        }
        private string _datePick = DateTime.UtcNow.ToString("dd/MM/yyyy");
        public string DatePick
        {
            get
            {
                return _datePick;
            }
            set
            {
                _datePick = value;
                RaisePropertyChanged("DatePick");
            }
        }
        private string _selectedCategory = string.Empty;
        public string SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }
            set
            {
                _selectedCategory = value;
                RaisePropertyChanged("SelectedCategory");
            }
        }
        private string _txtTIN = string.Empty;
        public string TxtTIN
        {
            get
            {
                return _txtTIN;
            }
            set
            {
                _txtTIN = value;
                //if (!string.IsNullOrEmpty(_txtTIN))
                //{
                //    if (_TaxEvasionReportTobeUsedToSubmit != null && _TaxEvasionReportTobeUsedToSubmit.TIN != null)
                //    {
                //        _TaxEvasionReportTobeUsedToSubmit.TIN = _txtTIN;
                //    }
                //}
                RaisePropertyChanged("TxtTIN");
            }
        }
        private string _rLocation = string.Empty;
        public string RLocation
        {
            get
            {
                return _rLocation;
            }
            set
            {
                _rLocation = value;
                RaisePropertyChanged("RLocation");
            }
        }
        private string _tFaciName = string.Empty;
        public string TFaciName
        {
            get
            {
                return _tFaciName;
            }
            set
            {
                _tFaciName = value;
                RaisePropertyChanged("TFaciName");
            }
        }
        private string _tMobNumber = string.Empty;
        public string TMobNumber
        {
            get
            {
                return _tMobNumber;
            }
            set
            {
                _tMobNumber = value;
                RaisePropertyChanged("TMobNumber");
            }
        }
        private string _tID = string.Empty;
        public string TID
        {
            get
            {
                return _tID;
            }
            set
            {
                _tID = value;
                RaisePropertyChanged("TID");
            }
        }
        private string _tVatNumber = string.Empty;
        public string TVatNumber
        {
            get
            {
                return _tVatNumber;
            }
            set
            {
                _tVatNumber = value;
                RaisePropertyChanged("TVatNumber");
            }
        }
        private string _tFDAdress = string.Empty;
        public string TFDAdress
        {
            get
            {
                return _tFDAdress;
            }
            set
            {
                _tFDAdress = value;
                //if (!string.IsNullOrEmpty(_tFDAdress))
                //{
                //    _TaxEvasionReportTobeUsedToSubmit.District = _tFDAdress;
                //}
                RaisePropertyChanged("TFDAdress");
            }
        }
        private string _tFSAddress = string.Empty;
        public string TFSAddress
        {
            get
            {
                return _tFSAddress;
            }
            set
            {
                _tFSAddress = value;
                //if (!string.IsNullOrEmpty(_tFSAddress))
                //{
                //    _TaxEvasionReportTobeUsedToSubmit.CompanyAddress = _tFSAddress;
                //}
                RaisePropertyChanged("TFSAddress");
            }
        }
        private string _tFWType = string.Empty;
        public string TFWType
        {
            get
            {
                return _tFWType;
            }
            set
            {
                _tFWType = value;
                //if (!string.IsNullOrEmpty(_tFWType))
                //{ _TaxEvasionReportTobeUsedToSubmit.WorkType = _tFWType; }
                RaisePropertyChanged("TFWType");
            }
        }
        private string _tReportDetail = string.Empty;
        public string TReportDetail
        {
            get
            {
                return _tReportDetail;
            }
            set
            {
                _tReportDetail = value;
                //if (_tReportDetail != null)
                //{
                //    if (_TaxEvasionReportTobeUsedToSubmit != null && _TaxEvasionReportTobeUsedToSubmit.ReportDetails != null)
                //    {
                //        _TaxEvasionReportTobeUsedToSubmit.ReportDetails = _tReportDetail;
                //    }
                //}
                RaisePropertyChanged("TReportDetail");
            }
        }
        private string _txtReportDetailRegion = string.Empty;
        public string TxtReportDetailRegion
        {
            get
            {
                return _txtReportDetailRegion;
            }
            set
            {
                _txtReportDetailRegion = value;
                RaisePropertyChanged("TxtReportDetailRegion");
            }
        }
        private string _txtReportDetailCity = string.Empty;
        public string TxtReportDetailCity
        {
            get
            {
                return _txtReportDetailCity;
            }
            set
            {
                _txtReportDetailCity = value;
                RaisePropertyChanged("TxtReportDetailCity");
            }
        }
        private List<TaxEvasionRegionCityDatum> _cityList;
        public List<TaxEvasionRegionCityDatum> CList
        {
            get
            {
                return _cityList;
            }
            set
            {
                _cityList = value;
                RaisePropertyChanged("CList");
            }
        }
        private List<TaxEvasionRegionCityDatum> _rList;
        public List<TaxEvasionRegionCityDatum> RList
        {
            get
            {
                return _rList;
            }
            set
            {
                _rList = value;
                RaisePropertyChanged("RList");
            }
        }
        private TaxEvasionRegionCityDatum _selectedTaxEvasionRegionPrev = null;
        public TaxEvasionRegionCityDatum SelectedTaxEvasionRegionPrev
        {
            get
            {
                return _selectedTaxEvasionRegionPrev;
            }
            set
            {
                _selectedTaxEvasionRegionPrev = value;
                //ListFormBudles = null;
                RaisePropertyChanged("SelectedTaxEvasionRegionPrev");
            }
        }
        private TaxEvasionRegionCityDatum _selectLCType = null;
        public TaxEvasionRegionCityDatum SelectLCType
        {
            get
            {
                return _selectLCType;
            }
            set
            {
                _selectLCType = value;
                if (_selectLCType != null)
                {
                    if (App.IsArabic)
                    {
                        TxtReportDetailCity = _selectLCType.Name;
                    }
                    else
                    {
                        TxtReportDetailCity = _selectLCType.Name;
                    }
                }
                RaisePropertyChanged("SelectLCType");
            }
            //if (_selectLCType != null)
            //{
            //    //  _TaxEvasionReportTobeUsedToSubmit.CityCode = _selectLCType.CityCode;
            //    if (string.IsNullOrEmpty(_selectLCType.Latitude))
            //    { //_TaxEvasionReportTobeUsedToSubmit.Latitude = "0.0"; 
            //    }
            //    else
            //    {// _TaxEvasionReportTobeUsedToSubmit.Latitude = _selectLCType.Latitude; }
            //        if (string.IsNullOrEmpty(_selectLCType.Latitude))
            //        { _TaxEvasionReportTobeUsedToSubmit.Longitude = "0.0"; }
            //        else
            //        { //_TaxEvasionReportTobeUsedToSubmit.Longitude = _selectLCType.Longitude; }
            //        }
            //    }
            //ListFormBudles = null;
        }
        private TaxEvasionRegionCityDatum _selectedTaxEvasionRegion = null;
        public TaxEvasionRegionCityDatum SelectedTaxEvasionRegion
        {
            get
            {
                return _selectedTaxEvasionRegion;
            }
            set
            {
                _selectedTaxEvasionRegion = value;
                if (_selectedTaxEvasionRegion != null)
                {
                    onSelectedTaxEvasionRegion();
                    if (App.IsArabic)
                    {
                        TxtReportDetailRegion = _selectedTaxEvasionRegion.Name;
                    }
                    else
                    {
                        TxtReportDetailRegion = _selectedTaxEvasionRegion.Name;
                    }
                    //TEReportobj.RegionCode = v;
                }
                //ListFormBudles = null;
                RaisePropertyChanged("SelectedTaxEvasionRegion");
            }
        }
        //attachment
        public decimal _attachmentSize = 0;
        public decimal AttachmentSize
        {
            get
            {
                return _attachmentSize;
            }
            set
            {
                _attachmentSize = value;
                RaisePropertyChanged("AttachmentSize");
            }
        }
        public decimal _totalAttachmentSize = 0;
        public decimal TotalAttachmentSize
        {
            get
            {
                return _totalAttachmentSize;
            }
            set
            {
                _totalAttachmentSize = value;
                RaisePropertyChanged("TotalAttachmentSize");
            }
        }
        private TaxEvasionReportDetails _selectedtaxEList = null;
        public TaxEvasionReportDetails selectedtaxEList
        {
            get
            {
                return _selectedtaxEList;
            }
            set
            {
                _selectedtaxEList = value;
                RaisePropertyChanged("selectedtaxEList");
            }
        }
        private TaxEvasionReportDetails _TaxEvasionReportTobeUsedToSubmit;
        public TaxEvasionReportDetails TaxEvasionReportTobeUsedToSubmit
        {
            get
            {
                return _TaxEvasionReportTobeUsedToSubmit;
            }
            set
            {
                _TaxEvasionReportTobeUsedToSubmit = value;
                if (_TaxEvasionReportTobeUsedToSubmit != null)
                {
                }
                RaisePropertyChanged("TaxEvasionReportTobeUsedToSubmit");
            }
        }
        private UploadedDocumentsList _uploadedDocumentsList = null;
        public UploadedDocumentsList UploadedDocumentsList
        {
            get
            {
                return _uploadedDocumentsList;
            }
            set
            {
                _uploadedDocumentsList = value;
                RaisePropertyChanged("UploadedDocumentsList");
            }
        }
        public int _attachmentCount = 0;
        public int AttachmentCount
        {
            get
            {
                return _attachmentCount;
            }
            set
            {
                _attachmentCount = value;
                RaisePropertyChanged("AttachmentCount");
            }
        }
        private ObservableCollection<UploadedDocumentsList> _uploadedDocumentsListObj = new ObservableCollection<UploadedDocumentsList>();
        public ObservableCollection<UploadedDocumentsList> UploadedDocumentsListObj
        {
            get
            {
                return _uploadedDocumentsListObj;
            }
            set
            {
                _uploadedDocumentsListObj = value;
                RaisePropertyChanged("UploadedDocumentsListObj");
            }
        }
        private double _latitude = 00.00;
        public double Latitude
        {
            get
            {
                return _latitude;
            }
            set
            {
                _latitude = value;
                RaisePropertyChanged("Latitude");
            }
        }
        private double _longitude = 00.00;
        public double Longitude
        {
            get
            {
                return _longitude;
            }
            set
            {
                _longitude = value;
                RaisePropertyChanged("Longitude");
            }
        }
        private string _attachmentName = string.Empty;
        public string AttachmentName
        {
            get
            {
                return _attachmentName;
            }
            set
            {
                _attachmentName = value;
                RaisePropertyChanged("AttachmentName");
            }
        }
        private List<FacilityCompanyType> _dlistfacilityCompanyType = null;
        public List<FacilityCompanyType> DListFacilityCompanyType
        {
            get
            {
                return _dlistfacilityCompanyType;
            }
            set
            {
                _dlistfacilityCompanyType = value;
                RaisePropertyChanged("DListFacilityCompanyType");
            }
        }
        private List<FacilityCompanyType> _listfacilityCompanyType = null;
        public List<FacilityCompanyType> ListFacilityCompanyType
        {
            get
            {
                return _listfacilityCompanyType;
            }
            set
            {
                _listfacilityCompanyType = value;
                RaisePropertyChanged("ListFacilityCompanyType");
            }
        }
        //reprt types
        private ObservableCollection<TaxEvasionCategoriesDataModel> _reportTypes = null;
        public ObservableCollection<TaxEvasionCategoriesDataModel> ReportTypes
        {
            get
            {
                return _reportTypes;
            }
            set
            {
                _reportTypes = value;

                //if (_selectedTaxEvasionListItem != null)
                //{ passSelectedTaxEvasionItem(); }
                RaisePropertyChanged("ReportTypes");
            }
        }

        private TaxEvasionCategoriesDataModel _selectedReportTypeListItem;
        public TaxEvasionCategoriesDataModel SelectedReportTypeListItem
        {
            get
            {
                return _selectedReportTypeListItem;
            }
            set
            {
                try
                {
                    _selectedReportTypeListItem = value;

                    if (_selectedReportTypeListItem != null)
                    {
                        //_selectedReportTypeListItem.IsTypeSelected = true;

                        //ObservableCollection<TaxEvasionCategoriesDataModel> tempReportType = ReportTypes;

                        //foreach (TaxEvasionCategoriesDataModel taxEvasionCategoriesDataModel in tempReportType)
                        //{
                        //    if(taxEvasionCategoriesDataModel.Id == _selectedReportTypeListItem.Id)
                        //    {
                        //        taxEvasionCategoriesDataModel.IsTypeSelected = true;
                        //    }
                        //    else
                        //    {
                        //        taxEvasionCategoriesDataModel.IsTypeSelected = false;
                        //    }
                        //}

                        //ReportTypes = tempReportType;
                        //passSelectedTaxEvasionItem(_selectedTaxEvasionListItem);
                    }

                    RaisePropertyChanged("SelectedReportTypeListItem");
                }
                catch (Exception ex)
                {
                }
            }
        }

        //TxtReporttype
        private string _txtReporttype = string.Empty;
        public string TxtReporttype
        {
            get
            {
                return _txtReporttype;
            }
            set
            {
                _txtReporttype = value;
                RaisePropertyChanged("TxtReporttype");
            }
        }

        private string _categorySelected_Index = "0";
        public string CategorySelected_Index
        {
            get
            {
                return _categorySelected_Index;
            }
            set
            {
                _categorySelected_Index = value;
                RaisePropertyChanged("CategorySelected_Index");
            }
        }
        private string _successResponse = AppResources.NDYourTaxEvasionReportissubmittedsuccessfully;
        public string SuccessResponse
        {
            get
            {
                return _successResponse;
            }
            set
            {
                _successResponse = value;
                RaisePropertyChanged("SuccessResponse");
            }
        }
        #endregion

        #region Commands
        public ICommand OnContinueClicked { get; set; }
        public ICommand OnBackStepClicked { get; set; }
        public ICommand OnAttachmentClick { get; set; }
        public ICommand OnGotoReportPageClicked { get; set; }
        public ICommand OnEditClicked { get; set; }
        #endregion

        #region Constructor
        public NewTaxEvasionFormPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            _dialogService = dialogService;

            OnContinueClicked = new Command(() => navigateToNextAsync());
            OnBackStepClicked = new Command(() => navigateToBack());
            OnAttachmentClick = new Xamarin.Forms.Command(async () =>
            {
                await AddAttachment();
            });
            OnGotoReportPageClicked = new Command(() => gotoReportsListpage());
            OnEditClicked = new Command<NewTaxEvasionTabEnum>((gotoTab) => EditInfo(gotoTab));
        }
        #endregion

        #region Method
        private void gotoReportsListpage()
        {
            _navigationService.NavigateTo(App.TaxEvasionMyReportsListPageView, "142536474");
        }
        private async Task navigateToNextAsync()
        {
            switch (currentTab)
            {
                case NewTaxEvasionTabEnum.ReporterInfo:
                    ReporterInformationValidation();
                    break;

                case NewTaxEvasionTabEnum.FacilityInfo:
                    FacilityInformationValidation();
                    break;

                case NewTaxEvasionTabEnum.ReportDetails:
                    ReportDetailStepValidation();
                    break;
                case NewTaxEvasionTabEnum.Summary:
                    await SubmitCreatedReport();
                    break;
            }
        }
        public void ReporterInformationValidation()
        {
            bool flag = true;
            if (string.IsNullOrEmpty(TName))
            {
                flag = false;
                IsTnameHasError = true;
            }
            if (string.IsNullOrEmpty(TMobNumber))
            {
                flag = false;
                IsTmobileHasError = true;
            }
            if (flag)
            {
                currentTab = NewTaxEvasionTabEnum.FacilityInfo;
                PageTitle = AppResources.NDFacilityInformation;
            }
            else
            {
              _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Information);
            }

        }
        public void FacilityInformationValidation()
        {
            bool flag = true;
            if (string.IsNullOrEmpty(TFaciName))
            {
                flag = false;
                IsFacilityNameHasError = true;
            }
            if (string.IsNullOrEmpty(TxtReportDetailRegion))
            {
                flag = false;
                IsRegionHasError = true;

            }
            if (string.IsNullOrEmpty(TxtReportDetailCity))
            {
                flag = false;
                IsCityHasError = true;
            }
            if (string.IsNullOrEmpty(TFDAdress))
            {
                flag = false;
                IsFDHasError = true;
            }
            if (string.IsNullOrEmpty(TFSAddress))
            {
                flag = false;
                IsFSHasError = true;
            }
            if (flag)
            {
                currentTab = NewTaxEvasionTabEnum.ReportDetails;
                PageTitle = AppResources.NDTaxEvasionReportDetails;
            }
            else
            {
                _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Information);
            }
        }
        public void ReportDetailStepValidation()
        {
            bool flag = true;
            if (string.IsNullOrEmpty(TxtReporttype))
            {
                flag = false;
            }
            if (string.IsNullOrEmpty(TReportDetail))
            {
                flag = false;
                IsReportDetailHasError = true;
            }
            if (flag)
            {
                currentTab = NewTaxEvasionTabEnum.Summary;
                PageTitle = AppResources.ZZZZSummery;
                BodyTitle = AppResources.VatDeregSummarySubTitle;
            }
            else
            {
                _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Information);
            }

        }
        private void navigateToBack()
        {
            switch (currentTab)
            {
                case NewTaxEvasionTabEnum.Summary: 
                    currentTab = NewTaxEvasionTabEnum.ReportDetails;
                    PageTitle = AppResources.NDTaxEvasionReportDetails;
                    BodyTitle = AppResources.ZZZZCompletethebelowdetails;
                    break;

                case NewTaxEvasionTabEnum.ReportDetails: 
                    currentTab = NewTaxEvasionTabEnum.FacilityInfo;
                    PageTitle = AppResources.NDFacilityInformation;
                    break;

                case NewTaxEvasionTabEnum.FacilityInfo:
                    currentTab= NewTaxEvasionTabEnum.ReporterInfo;
                    PageTitle = AppResources.NDReporterInformation;
                    break;
                case NewTaxEvasionTabEnum.ReporterInfo:
                    gotoReportsListpage();
                    break;
            }

        }
        private void EditInfo(NewTaxEvasionTabEnum gotoTab)
        {
            switch (gotoTab)
            {
                case NewTaxEvasionTabEnum.ReporterInfo:
                    currentTab = NewTaxEvasionTabEnum.ReporterInfo;
                    PageTitle = AppResources.NDReporterInformation;
                    break;

                case NewTaxEvasionTabEnum.ReportDetails:
                    currentTab = NewTaxEvasionTabEnum.ReportDetails;
                    PageTitle = AppResources.NDTaxEvasionReportDetails;
                    BodyTitle = AppResources.ZZZZCompletethebelowdetails;
                    break;

                case NewTaxEvasionTabEnum.FacilityInfo:
                    currentTab = NewTaxEvasionTabEnum.FacilityInfo;
                    PageTitle = AppResources.NDFacilityInformation;
                    break;
            }

        }

        public async Task OnPageLoad()
        {
            //reset tab to reporter Information
            currentTab = NewTaxEvasionTabEnum.ReporterInfo;
            PageTitle = AppResources.NDReporterInformation;

            //load regions and citys
            try
            {
                try
                {
                    TaxEvasionRegionsCityModel regionlist = new TaxEvasionRegionsCityModel();
                    regionlist = await WebServiceManager.GAZTTaxEvasionGetAllRegions();

                    if (regionlist != null && regionlist.Data.Count() != 0)
                    {
                        if (CList != null && CList.Count > 0)
                        {
                            CList.Clear();
                            TxtReportDetailCity = string.Empty;
                        }

                        RList = regionlist.Data;
                    }
                    else
                    {
                        //_dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
                        //_navigationService.GoBack();
                        NoInternetGoBack();
                    }
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        //_navigationService.GoBack();
                    });
                }
            }
            catch (Exception ex)
            {

                Device.BeginInvokeOnMainThread(() =>
                {
                    _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    //_navigationService.GoBack();
                });
            }

            //load report types
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                TaxEvasionCategoriesModel rootObject = await WebServiceManager.GAZTTaxEvasionGetCategories();
                PopToRootPage();
                if (rootObject != null)
                {
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });

                    if (rootObject.Data != null)
                    {
                        ReportTypes = new ObservableCollection<TaxEvasionCategoriesDataModel>();
                        foreach (TaxEvasionCategoriesDataModel taxEvasionCategoriesDataModel in rootObject.Data)
                        {
                            ReportTypes.Add(taxEvasionCategoriesDataModel);
                        }
                    }
                    else
                    {
                        _navigationService.GoBack();
                    }
                }
                else
                {
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });
                }
            }
            catch (GAZTException gex)
            {
                // Handle the GAZT custom exception.
                string MessageForTheUser = gex.Message;
                if (gex is GAZTInvalidDataException)
                {
                    MessageForTheUser = AppResources.ZZSomethingwentwrong;
                }
                if (gex is GAZTNetworkConnectivityIssueException)
                {
                    MessageForTheUser = AppResources.NetworkConnectivityIssue;
                }
                else if (gex is GAZTInternetException)
                {
                    MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                }
                else if (gex is GAZTSessionExpiredException)
                {
                    MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                }
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });
                    await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });

                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }

        }

        public async void NoInternetGoBack()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await _dialogService.ShowMessage(AppResources.NetworkConnectivityIssue, AppResources.Information);
                _navigationService.GoBack();
            });
        }

        public void CreateCompanyTypeList()
        {
            try
            {
                ListFacilityCompanyType = new List<FacilityCompanyType>();
                DListFacilityCompanyType = new List<FacilityCompanyType>();
                ListFacilityCompanyType.Clear();
                DListFacilityCompanyType.Clear();
                //  FacilityCompanyType cct = new FacilityCompanyType();
                ListFacilityCompanyType.Add(new FacilityCompanyType() { Id = "1", Name = AppResources.ZTERReportDetailCompanyType1 });
                ListFacilityCompanyType.Add(new FacilityCompanyType() { Id = "2", Name = AppResources.ZTERReportDetailCompanyType2 });
                ListFacilityCompanyType.Add(new FacilityCompanyType() { Id = "3", Name = AppResources.ZTERReportDetailCompanyType3 });
                ListFacilityCompanyType.Add(new FacilityCompanyType() { Id = "4", Name = AppResources.ZTERReportDetailCompanyType4 });
                ListFacilityCompanyType.Add(new FacilityCompanyType() { Id = "5", Name = AppResources.ZTERReportDetailCompanyType5 });
                ListFacilityCompanyType.Add(new FacilityCompanyType() { Id = "6", Name = AppResources.ZTERReportDetailCompanyType6 });
                ListFacilityCompanyType.Add(new FacilityCompanyType() { Id = "7", Name = AppResources.ZTERReportDetailCompanyType7 });
                ListFacilityCompanyType.Add(new FacilityCompanyType() { Id = "8", Name = AppResources.ZTERReportDetailCompanyType8 });
                ListFacilityCompanyType.Add(new FacilityCompanyType() { Id = "9", Name = AppResources.ZTERReportDetailCompanyType9 });
                ListFacilityCompanyType.Add(new FacilityCompanyType() { Id = "10", Name = AppResources.ZTERReportDetailCompanyType10 });
                ListFacilityCompanyType.Add(new FacilityCompanyType() { Id = "11", Name = AppResources.ZTERReportDetailCompanyType11 });
                ListFacilityCompanyType.Add(new FacilityCompanyType() { Id = "12", Name = AppResources.ZTERReportDetailCompanyType12 });
                DListFacilityCompanyType = ListFacilityCompanyType;
            }
            catch (Exception ex)
            {
            }
        }

        public async Task onSelectedTaxEvasionRegion()
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });
            await Task.Run(async () =>
            {
                try
                {
                    if (SelectedTaxEvasionRegion != null && SelectedTaxEvasionRegion.Id != null)
                    {
                        TaxEvasionRegionsCityModel citylist = new TaxEvasionRegionsCityModel();
                        citylist = await WebServiceManager.GAZTTaxEvasionGetAllCitiesByRegion(SelectedTaxEvasionRegion.Id);
                        PopToRootPage();
                        CList = citylist.Data;
                    }
                    else
                    {

                    }
                }
                catch (InternetException ex)
                {
                    //Device.BeginInvokeOnMainThread(async () =>
                    //{
                    //   await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    //});
                    NoInternetGoBack();
                }
            });
            await Task.Run(() =>
            {
                IsLoading = false;
            });
        }

        public async Task AddAttachment()
        {
            if (AttachmentCount < 3)
            {
                string[] filetypes;

                filetypes = DependencyService.Get<IDeviceInfo>().GetAttachmentTypeStringForTaxEvasion();

                var fileData = await CrossFilePicker.Current.PickFile(filetypes);
                //if (AttachmentSize < 10)
                //{
                if (fileData != null)
                {
                    attachment = fileData.DataArray;

                    string base64String = Convert.ToBase64String(attachment, 0, attachment.Length);
                    AttachmentName = fileData.FileName;

                    float sizemb = (attachment.Length / 1024f) / 1024f;
                    AttachmentSize = AttachmentSize + (Decimal)sizemb;
                    if (fileData.FileName.Contains("."))
                    {
                        string Extention = fileData.FileName.Split('.')[1];//pdf
                        if (Extention.ToLower() == "doc" || Extention.ToLower() == "docx" || Extention.ToLower() == "jpg" || Extention.ToLower() == "pdf" || Extention.ToLower() == "jpeg")
                        {
                            if (TotalAttachmentSize <= 30)
                            {
                                AttachmentSize = Math.Round(Convert.ToDecimal((Convert.ToDouble(attachment.Length) / 1048576.0)), 2);
                                decimal AttachmentSizeTillFourDecimal = Math.Round(Convert.ToDecimal((Convert.ToDouble(attachment.Length) / 1048576.0)), 4);
                                if (Convert.ToDecimal(AttachmentSize) <= 10)
                                {
                                    if (Convert.ToDecimal(AttachmentSizeTillFourDecimal) > 0)
                                    {
                                        bool isAttachmentexixt = false;

                                        try
                                        {
                                            UploadedDocumentsList a = new UploadedDocumentsList();
                                            a.FileNameWithExtension = AttachmentName;
                                            a.DocBinaryInBase64 = attachment;
                                            a.Size = AttachmentSize.ToString();

                                            string attachmentType = UtilityManager.GetContentType(Extention);
                                            //UploadedDocumentsList.DocBinaryInBase64 = base64String;
                                            //UploadedDocumentsList.FileNameWithExtension = AttachmentName;
                                            a.MimeType = attachmentType;
                                            foreach (UploadedDocumentsList ItemA in UploadedDocumentsListObj)
                                            {
                                                if (AttachmentName == ItemA.FileNameWithExtension)
                                                {
                                                    isAttachmentexixt = true;
                                                }
                                            }
                                            if (isAttachmentexixt == false)
                                            {
                                                UploadedDocumentsListObj.Add(a);
                                                AttachmentCount++;
                                                AttachmentName = string.Empty;
                                            }
                                            else
                                            {
                                                AttachmentName = string.Empty;
                                                _dialogService.ShowMessage(AppResources.ZZGeneralMessage_FileWithTheSameNameAlreadyExists, AppResources.Information);
                                            }
                                            //  UploadedDocumentsListObj = new List<UploadedDocumentsList>();

                                        }
                                        catch (Exception ex)
                                        {
                                        }
                                    }
                                    else
                                    {
                                        AttachmentName = string.Empty;
                                        _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
                                    }
                                }
                            }
                        }
                        else
                        {
                            _dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
                        }
                    }
                }
            }
        }

        public async Task SubmitCreatedReport()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                string date = DateTime.UtcNow.ToString("dd/MM/yyyy");//1902/03/09
                date = DatePick;
                TaxEvasionReportTobeUsedToSubmit = new TaxEvasionReportDetails();
                TaxEvasionReportTobeUsedToSubmit.CreatedAt = date;
                //TaxEvasionReportTobeUsedToSubmit.CompanyType = SelectedTaxEvasionCompanyType.Id;

                TaxEvasionReportTobeUsedToSubmit.Category = SelectedCategory;

                //Removed
                //TaxEvasionReportTobeUsedToSubmit.Channel = "2";
                //TaxEvasionReportTobeUsedToSubmit.ReporterName = TName;
                //TaxEvasionReportTobeUsedToSubmit.ReporterEmail = TEmail;
                //TaxEvasionReportTobeUsedToSubmit.CompanyType = "0";
                //TaxEvasionReportTobeUsedToSubmit.HavingTIN = IsTINVisible.ToString().ToLower();
                //TaxEvasionReportTobeUsedToSubmit.CompanyOwnerName = TFaciOwnerName;
                //TaxEvasionReportTobeUsedToSubmit.CompanyEmail = TFaciEmail;

                TaxEvasionReportTobeUsedToSubmit.Tin = TxtTIN;
                TaxEvasionReportTobeUsedToSubmit.Content = TReportDetail;

                TaxEvasionReportTobeUsedToSubmit.Facilities = TFaciName;
                TaxEvasionReportTobeUsedToSubmit.PhoneNumber = "+966"+TMobNumber;
                TaxEvasionReportTobeUsedToSubmit.Id = TID;
                TaxEvasionReportTobeUsedToSubmit.VatNumber = TVatNumber;
                TaxEvasionReportTobeUsedToSubmit.Longitude = Longitude.ToString();
                TaxEvasionReportTobeUsedToSubmit.Latitude = Latitude.ToString();
                TaxEvasionReportTobeUsedToSubmit.District = TFDAdress;
                TaxEvasionReportTobeUsedToSubmit.Street = TFSAddress;
                TaxEvasionReportTobeUsedToSubmit.WorkType = TFWType;
                TaxEvasionReportTobeUsedToSubmit.RegionCode = Convert.ToString(SelectedTaxEvasionRegion.Id);
                TaxEvasionReportTobeUsedToSubmit.City = Convert.ToString(SelectLCType.Id);

                //List<UploadedDocumentsList> newList = UploadedDocumentsListObj.ToList<UploadedDocumentsList>();
                //TaxEvasionReportTobeUsedToSubmit.PhoneNumber = TFaciMobNo;

                List<UploadedDocumentsList> newList = UploadedDocumentsListObj.ToList<UploadedDocumentsList>();

                TaxEvasionReportTobeUsedToSubmit.Latitude = _latitude.ToString();
                TaxEvasionReportTobeUsedToSubmit.Latitude = _latitude.ToString();
                TaxEvasionReportTobeUsedToSubmit.Longitude = _longitude.ToString();

                TaxEvasionCreateReportResponseModel response = new TaxEvasionCreateReportResponseModel();
                response = await WebServiceManager.GAZTTaxEvasionCreateReport(TaxEvasionReportTobeUsedToSubmit, newList);

                if (response != null && response.Status == true)
                {
                    //ZTEReportReportSuccessResponsep1
                    var resmessage = AppResources.ZTEReportReportSuccessResponsep1;
                    var newrm = resmessage.Replace("Report Number", response.Data.TicketId);
                    SuccessResponse = newrm.Replace("5", "10");

                    //await _dialogService.ShowMessage(newReplacedMsg, AppResources.ZZZSubmittedReport);
                    //var _navigation = Application.Current.MainPage.Navigation;
                    //var _lastPage = _navigation.NavigationStack.LastOrDefault();
                    ////Remove last page
                    //_navigation.RemovePage(_lastPage);
                    //var _lastPage2 = _navigation.NavigationStack.LastOrDefault();
                    ////Remove last page
                    //_navigation.RemovePage(_lastPage2);
                    ////Go back 

                    //_navigation.PopAsync();
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        _navigationService.NavigateTo(App.NewTaxEvasionFormSuccessPaveView);
                    });
                }
                else
                {//ZTEReportReportSuccessResponsep2
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });
                    _dialogService.ShowMessage(AppResources.ZTEReportReportSuccessResponsep2, " ");
                }
                await Task.Run(() =>
                {
                  //  App.HideProgressView();
                });
            }
            catch (GAZTException gex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                // Handle the GAZT custom exception.
                string MessageForTheUser = gex.Message;
                if (gex is GAZTInvalidDataException)
                {
                    MessageForTheUser = AppResources.ZZSomethingwentwrong;
                }
                if (gex is GAZTNetworkConnectivityIssueException)
                {
                    MessageForTheUser = AppResources.NetworkConnectivityIssue;
                }
                else if (gex is GAZTInternetException)
                {
                    MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                }
                else if (gex is GAZTSessionExpiredException)
                {
                    MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                }
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });

                    _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    //viewModel._navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                Console.WriteLine(ex.Message);
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });

                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                });
            }
        }
        #endregion
    }
}
