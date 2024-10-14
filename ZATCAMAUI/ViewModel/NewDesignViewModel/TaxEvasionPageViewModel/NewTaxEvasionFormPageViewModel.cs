using System.Collections.ObjectModel;
using System.Windows.Input;

using Plugin.Media;
using Plugin.Media.Abstractions;
using Mopups.Services;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Core.Interfaces;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.TaxEvasionPageViewModel
{

    public class NewTaxEvasionFormPageViewModel : BaseViewModel
    {
        private bool _IsTnameHasError = false;
        public byte[] imageArray;
        public string FileName;

        public bool IsTnameHasError
        {
            get
            {
                return _IsTnameHasError;
            }
            set
            {
                if (_IsTnameHasError == value) return;
                _IsTnameHasError = value;
                OnPropertyChanged("IsTnameHasError");
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
                if (_IsTmobileHasError == value) return;

                _IsTmobileHasError = value;
                OnPropertyChanged("IsTmobileHasError");
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
                if (_IsFacilityNameHasError == value) return;

                _IsFacilityNameHasError = value;
                OnPropertyChanged("IsFacilityNameHasError");
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
                if (_IsRegionHasError == value) return;

                _IsRegionHasError = value;
                OnPropertyChanged("IsRegionHasError");
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
                if (_IsCityHasError == value) return;

                _IsCityHasError = value;
                OnPropertyChanged("IsCityHasError");
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
                if (_IsFDHasError == value) return;

                _IsFDHasError = value;
                OnPropertyChanged("IsFDHasError");
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
                if (_IsFSHasError == value) return;

                _IsFSHasError = value;
                OnPropertyChanged("IsFSHasError");
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
                if (_IsReportDetailHasError == value) return;

                _IsReportDetailHasError = value;
                OnPropertyChanged("IsReportDetailHasError");
            }
        }
        #region Variable
        private NewTaxEvasionTabEnum _currentTab = NewTaxEvasionTabEnum.ReporterInfo;
        public NewTaxEvasionTabEnum currentTab
        {
            get => _currentTab;
            private set
            {
                if (_currentTab == value) return;

                _currentTab = value;
                OnPropertyChanged(nameof(currentTab));
                CurrentIndex = (int)_currentTab;
                OnPropertyChanged(nameof(CurrentIndex));
            }
        }
        private int _currenrIndex = 0;
        public int CurrentIndex
        {
            get => _currenrIndex;
            set
            {
                if (_currenrIndex == value) return;

                _currenrIndex = value;
                OnPropertyChanged(nameof(CurrentIndex));
                if (_currenrIndex == MaxIndex)
                {
                    MarkComplete = true;
                    OnPropertyChanged(nameof(MarkComplete));
                }
            }
        }
        public bool MarkComplete { get; private set; } = false;
        public int MaxIndex { get; private set; } = 4;
        public static decimal AttachmentUploadedSize;
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
                if (_PageTitle == value) return;

                _PageTitle = value;
                OnPropertyChanged("PageTitle");
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
                if (_BodyTitle == value) return;

                _BodyTitle = value;
                OnPropertyChanged("BodyTitle");
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
                    if (_tName == value) return;

                    _tName = value;
                    OnPropertyChanged("TName");
                }
                catch (Exception)
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
                if (_datePick == value) return;

                _datePick = value;
                OnPropertyChanged("DatePick");
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
                if (_selectedCategory == value) return;

                _selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
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
                if (_txtTIN == value) return;

                _txtTIN = value;
                OnPropertyChanged("TxtTIN");
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
                if (_rLocation == value) return;

                _rLocation = value;
                OnPropertyChanged("RLocation");
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
                if (_tFaciName == value) return;

                _tFaciName = value;
                OnPropertyChanged("TFaciName");
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
                if (_tMobNumber == value) return;

                _tMobNumber = value;
                OnPropertyChanged("TMobNumber");
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
                if (_tID == value) return;

                _tID = value;
                OnPropertyChanged("TID");
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
                if (_tVatNumber == value) return;

                _tVatNumber = value;
                OnPropertyChanged("TVatNumber");
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
                if (_tFDAdress == value) return;

                _tFDAdress = value;
                //if (!string.IsNullOrEmpty(_tFDAdress))
                //{
                //    _TaxEvasionReportTobeUsedToSubmit.District = _tFDAdress;
                //}
                OnPropertyChanged("TFDAdress");
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
                if (_tFSAddress == value) return;

                _tFSAddress = value;
                //if (!string.IsNullOrEmpty(_tFSAddress))
                //{
                //    _TaxEvasionReportTobeUsedToSubmit.CompanyAddress = _tFSAddress;
                //}
                OnPropertyChanged("TFSAddress");
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
                if (_tFWType == value) return;

                _tFWType = value;
                //if (!string.IsNullOrEmpty(_tFWType))
                //{ _TaxEvasionReportTobeUsedToSubmit.WorkType = _tFWType; }
                OnPropertyChanged("TFWType");
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
                if (_tReportDetail == value) return;

                _tReportDetail = value;
                OnPropertyChanged("TReportDetail");
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
                if (_txtReportDetailRegion == value) return;

                _txtReportDetailRegion = value;
                OnPropertyChanged("TxtReportDetailRegion");
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
                if (_txtReportDetailCity == value) return;

                _txtReportDetailCity = value;
                OnPropertyChanged("TxtReportDetailCity");
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
                if (_cityList == value) return;

                _cityList = value;
                OnPropertyChanged("CList");
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
                if (_rList == value) return;

                _rList = value;
                OnPropertyChanged("RList");
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
                if (_selectedTaxEvasionRegionPrev == value) return;

                _selectedTaxEvasionRegionPrev = value;
                //ListFormBudles = null;
                OnPropertyChanged("SelectedTaxEvasionRegionPrev");
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
                if (_selectLCType == value) return;

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
                OnPropertyChanged("SelectLCType");
            }
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
                if (_selectedTaxEvasionRegion == value) return;

                _selectedTaxEvasionRegion = value;
                if (_selectedTaxEvasionRegion != null)
                {
                    _ = onSelectedTaxEvasionRegion();
                    if (App.IsArabic)
                    {
                        TxtReportDetailRegion = _selectedTaxEvasionRegion.Name;
                    }
                    else
                    {
                        TxtReportDetailRegion = _selectedTaxEvasionRegion.Name;
                    }
                }
                OnPropertyChanged("SelectedTaxEvasionRegion");
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
                if (_attachmentSize == value) return;

                _attachmentSize = value;
                OnPropertyChanged("AttachmentSize");
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
                if (_totalAttachmentSize == value) return;

                _totalAttachmentSize = value;
                OnPropertyChanged("TotalAttachmentSize");
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
                if (_selectedtaxEList == value) return;

                _selectedtaxEList = value;
                OnPropertyChanged("selectedtaxEList");
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
                if (_TaxEvasionReportTobeUsedToSubmit == value) return;

                _TaxEvasionReportTobeUsedToSubmit = value;
                if (_TaxEvasionReportTobeUsedToSubmit != null)
                {
                }
                OnPropertyChanged("TaxEvasionReportTobeUsedToSubmit");
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
                if (_uploadedDocumentsList == value) return;

                _uploadedDocumentsList = value;
                OnPropertyChanged("UploadedDocumentsList");
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
                if (_attachmentCount == value) return;

                _attachmentCount = value;
                OnPropertyChanged("AttachmentCount");
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
                if (_uploadedDocumentsListObj == value) return;

                _uploadedDocumentsListObj = value;
                OnPropertyChanged("UploadedDocumentsListObj");
            }
        }
        private double _latitude = 24.7136;
        public double Latitude
        {
            get
            {
                return _latitude;
            }
            set
            {
                if (_latitude == value) return;

                _latitude = value;
                OnPropertyChanged("Latitude");
            }
        }
        private double _longitude = 46.6753;
        public double Longitude
        {
            get
            {
                return _longitude;
            }
            set
            {
                if (_longitude == value) return;

                _longitude = value;
                OnPropertyChanged("Longitude");
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
                if (_attachmentName == value) return;

                _attachmentName = value;
                OnPropertyChanged("AttachmentName");
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
                if (_dlistfacilityCompanyType == value) return;

                _dlistfacilityCompanyType = value;
                OnPropertyChanged("DListFacilityCompanyType");
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
                if (_listfacilityCompanyType == value) return;

                _listfacilityCompanyType = value;
                OnPropertyChanged("ListFacilityCompanyType");
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
                if (_reportTypes == value) return;

                _reportTypes = value;

                //if (_selectedTaxEvasionListItem != null)
                //{ passSelectedTaxEvasionItem(); }
                OnPropertyChanged("ReportTypes");
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
                    if (_selectedReportTypeListItem == value) return;

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

                    OnPropertyChanged("SelectedReportTypeListItem");
                }
                catch (Exception)
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
                if (_txtReporttype == value) return;

                _txtReporttype = value;
                OnPropertyChanged("TxtReporttype");
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
                if (_categorySelected_Index == value) return;

                _categorySelected_Index = value;
                OnPropertyChanged("CategorySelected_Index");
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
                if (_successResponse == value) return;

                _successResponse = value;
                OnPropertyChanged("SuccessResponse");
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
            OnContinueClicked = new Command(async () => await navigateToNextAsync());
            OnBackStepClicked = new Command(() => navigateToBack());
            OnAttachmentClick = new Command(() =>
            {
                // AddAttachment();
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
                // _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Information);
                MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));
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
                //  _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Information);
                MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));
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
                //               _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Information);
                MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));
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
                    currentTab = NewTaxEvasionTabEnum.ReporterInfo;
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
                TaxEvasionRegionsCityModel regionlist = new TaxEvasionRegionsCityModel();
                regionlist = await TaxEvasionWebServiceManager.GAZTTaxEvasionGetAllRegions();

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
                   
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        //_dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                        //_navigationService.GoBack();
                    });
                }
            }
            catch (InternetException ex)
            {


                MainThread.BeginInvokeOnMainThread(() =>
                {
                    //_dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                    //_navigationService.GoBack();
                });
            }
            catch (Exception)
            {

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    // _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
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
                TaxEvasionCategoriesModel rootObject = await TaxEvasionWebServiceManager.GAZTTaxEvasionGetCategories();
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
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });
                    //await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                    _navigationService.GoBack();
                });
            }
            catch (Exception)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });

                    // await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                    _navigationService.GoBack();
                });
            }

        }

        public void NoInternetGoBack()
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                ///   await _dialogService.ShowMessage(AppResources.NetworkConnectivityIssue, AppResources.Information);
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NetworkConnectivityIssue));
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
            catch (Exception)
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
                    if (SelectedTaxEvasionRegion != null)
                    {
                        TaxEvasionRegionsCityModel citylist = new TaxEvasionRegionsCityModel();
                        citylist = await TaxEvasionWebServiceManager.GAZTTaxEvasionGetAllCitiesByRegion(SelectedTaxEvasionRegion.Id);
                        PopToRootPage();
                        CList = citylist.Data;
                    }
                    else
                    {

                    }
                }
                catch (InternetException)

                {
                    NoInternetGoBack();
                }
            });
            await Task.Run(() =>
            {
                IsLoading = false;
            });
        }

        //public async Task AddAttachment()
        //{
        //    try
        //    {
        //        if (AttachmentCount < 3)
        //        {
        //            string[] filetypes = null ;

        //            try
        //            {
        //                filetypes = DependencyService.Get<IDeviceInfo>().GetAttachmentTypeStringForTaxEvasion();
        //            }
        //            catch (Exception ex)
        //            {
        //            }
        //            var fileData = await CrossFilePicker.Current.PickFile(filetypes);
        //            //if (AttachmentSize < 10)
        //            //{
        //            if (fileData != null)
        //            {
        //                attachment = fileData.DataArray;

        //                string base64String = Convert.ToBase64String(attachment, 0, attachment.Length);
        //                AttachmentName = fileData.FileName;

        //                float sizemb = (attachment.Length / 1024f) / 1024f;
        //                AttachmentSize = AttachmentSize + (Decimal)sizemb;
        //                if (fileData.FileName.Contains("."))
        //                {
        //                    string Extention = fileData.FileName.Split('.')[1];//pdf
        //                    if ( Extention.ToLower() == "jpg" || Extention.ToLower() == "jpeg")
        //                    {
        //                        if (TotalAttachmentSize <= 30)
        //                        {
        //                            AttachmentSize = Math.Round(Convert.ToDecimal((Convert.ToDouble(attachment.Length) / 1048576.0)), 2);
        //                            decimal AttachmentSizeTillFourDecimal = Math.Round(Convert.ToDecimal((Convert.ToDouble(attachment.Length) / 1048576.0)), 4);
        //                            if (Convert.ToDecimal(AttachmentSize) <= 10)
        //                            {
        //                                if (Convert.ToDecimal(AttachmentSizeTillFourDecimal) > 0)
        //                                {
        //                                    bool isAttachmentexixt = false;

        //                                    try
        //                                    {
        //                                        UploadedDocumentsList a = new UploadedDocumentsList();
        //                                        a.FileNameWithExtension = AttachmentName;
        //                                        a.DocBinaryInBase64 = attachment;
        //                                        a.Size = AttachmentSize.ToString();

        //                                        string attachmentType = UtilityManager.GetContentType(Extention);
        //                                        //UploadedDocumentsList.DocBinaryInBase64 = base64String;
        //                                        //UploadedDocumentsList.FileNameWithExtension = AttachmentName;
        //                                        a.MimeType = attachmentType;
        //                                        foreach (UploadedDocumentsList ItemA in UploadedDocumentsListObj)
        //                                        {
        //                                            if (AttachmentName == ItemA.FileNameWithExtension)
        //                                            {
        //                                                isAttachmentexixt = true;
        //                                            }
        //                                        }
        //                                        if (isAttachmentexixt == false)
        //                                        {
        //                                            UploadedDocumentsListObj.Add(a);
        //                                            AttachmentCount++;
        //                                            AttachmentName = string.Empty;

        //                                            //MessagingCenter.Unsubscribe<object, string>(this, "OnCameraClicked");
        //                                            //MessagingCenter.Unsubscribe<object, string>(this, "OnGalleryClicked");

        //                                        }
        //                                        else
        //                                        {
        //                                            AttachmentName = string.Empty;
        //                                            // _dialogService.ShowMessage(AppResources.ZZGeneralMessage_FileWithTheSameNameAlreadyExists, AppResources.Information);
        //                                            MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_FileWithTheSameNameAlreadyExists));
        //                                        }
        //                                        //  UploadedDocumentsListObj = new List<UploadedDocumentsList>();

        //                                    }
        //                                    catch (Exception ex)
        //                                    {
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    AttachmentName = string.Empty;
        //                                    //  _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
        //                                    MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        // _dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
        //                        MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly));
        //                    }
        //                }
        //            }

        //        }
        //        else
        //        {
        //            MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZMaximumnoof3attachmentscanbeuploaded));
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

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
                string mob = "+966" + TMobNumber;
                TaxEvasionReportTobeUsedToSubmit.PhoneNumber = App.TaxEvasionUserData.Mobile;
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

                List<UploadedDocumentsList> newList = UploadedDocumentsListObj.ToList();

                TaxEvasionReportTobeUsedToSubmit.Latitude = _latitude.ToString();
                TaxEvasionReportTobeUsedToSubmit.Latitude = _latitude.ToString();
                TaxEvasionReportTobeUsedToSubmit.Longitude = _longitude.ToString();

                TaxEvasionCreateReportResponseModel response = new TaxEvasionCreateReportResponseModel();
                response = await TaxEvasionWebServiceManager.GAZTTaxEvasionCreateReport(TaxEvasionReportTobeUsedToSubmit, newList);

                if (response != null && response.Status == true)
                {
                    //ZTEReportReportSuccessResponsep1
                    var resmessage = AppResources.ZTEReportReportSuccessResponsep1;
                    // var NoOFdays = resmessage.Replace("5", "15");
                    var newrm = resmessage.Replace("Report Number", response.Data.TicketId);
                    SuccessResponse = newrm;

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
                    MainThread.BeginInvokeOnMainThread(() =>
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
                    // _dialogService.ShowMessage(AppResources.ZTEReportReportSuccessResponsep2, " ");
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZTEReportReportSuccessResponsep2));
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
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });

                    //_dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                    //viewModel._navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;

                    /* Unmerged change from project 'ZATCAMAUI (net7.0-ios)'
                    Before:
                                    });



                                    MainThread.BeginInvokeOnMainThread(async () =>
                    After:
                                    });



                                    MainThread.BeginInvokeOnMainThread(async () =>
                    */
                });



                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });

                    //  await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                });
            }
        }


        public void UploadAttachment()
        {
            //imageArray = null;
            //FileName = AppResources.NoFilechosen;
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await CrossMedia.Current.Initialize();

                try
                {
                    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    {
                        await _dialogService.ShowMessage("No Camera", "OK");
                        return;
                    }
                    MainThread.BeginInvokeOnMainThread(async () =>
                    ////await Task.Run(async () =>
                    {
                        var mediaOption = new Plugin.Media.Abstractions.StoreCameraMediaOptions
                        {
                            Name = "image.jpg",
                            SaveToAlbum = false,
                            CustomPhotoSize = 75,
                            CompressionQuality = 50,
                            PhotoSize = PhotoSize.Medium
                        };
                        var file = await CrossMedia.Current.TakePhotoAsync(mediaOption);
                        if (file != null)
                        {
                            var filePath = await ReadFully(file.GetStream());
                                //  UtilityManager.imagestring = Convert.ToBase64String(filePath);
                            }
                        if (file == null)
                        {
                            return;
                        }

                        if (file != null)
                        {

                            var imagePath = file.Path;
                            var imageName = Path.GetFileName(imagePath);
                            byte[] baseString = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetImagePathByteArray(file.Path);
                                //imageArray = System.Convert.FromBase64String(baseString);
                                imageArray = baseString;
                            FileName = imageName;

                            if (AttachmentCount < 3)
                            {
                                if (file != null)
                                {
                                    attachment = baseString;

                                    string base64String = Convert.ToBase64String(attachment, 0, attachment.Length);
                                    AttachmentName = FileName;

                                    float sizemb = (attachment.Length / 1024f) / 1024f;
                                    AttachmentSize = AttachmentSize + (Decimal)sizemb;
                                    if (FileName.Contains("."))
                                    {
                                        string Extention = FileName.Split('.')[1];//pdf
                                            if (Extention.ToLower() == "jpg" || Extention.ToLower() == "jpeg")
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
                                                                    //MessagingCenter.Unsubscribe<object, string>(this, "OnCameraClicked");
                                                                    //MessagingCenter.Unsubscribe<object, string>(this, "OnGalleryClicked");

                                                                }
                                                            else
                                                            {
                                                                AttachmentName = string.Empty;
                                                                    // _dialogService.ShowMessage(AppResources.ZZGeneralMessage_FileWithTheSameNameAlreadyExists, AppResources.Information);
                                                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_FileWithTheSameNameAlreadyExists));
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
                                                            //  _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
                                                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                                // _dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
                                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly));
                                        }
                                    }
                                }
                            }
                            else
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZMaximumnoof3attachmentscanbeuploaded));
                            }
                        }



                    });
                }
                catch (Exception ex)
                {
                    
                    
                }



            });
        }


        public void AddAttachment()
        {
            //imageArray = null;
            //FileName = AppResources.NoFilechosen;
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await CrossMedia.Current.Initialize();

                try
                {

                    MainThread.BeginInvokeOnMainThread(async () =>
                    ////await Task.Run(async () =>
                    {
                        //var mediaOption = new Plugin.Media.Abstractions.StoreCameraMediaOptions
                        //{
                        //    Name = "image.jpg",
                        //    SaveToAlbum = false,
                        //    CustomPhotoSize = 75,
                        //    CompressionQuality = 50,
                        //    PhotoSize = PhotoSize.Medium
                        //};
                        //var file = await CrossMedia.Current.TakePhotoAsync(mediaOption);

                        MediaFile file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                        {
                            PhotoSize = PhotoSize.Medium
                            //CompressionQuality = 92
                        });

                        if (file != null)
                        {
                            Stream s = file.GetStream();
                            var filePath = await ReadFully(file.GetStream());

                            //UtilityManager.imagestring = Convert.ToBase64String(filePath);
                        }
                        if (file != null)
                        {
                            var filePath = await ReadFully(file.GetStream());
                            //  UtilityManager.imagestring = Convert.ToBase64String(filePath);
                        }
                        if (file == null)
                        {
                            return;
                        }

                        if (file != null)
                        {

                            var imagePath = file.Path;
                            var imageName = Path.GetFileName(imagePath);
                            byte[] baseString = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetImagePathByteArray(file.Path);
                            //imageArray = System.Convert.FromBase64String(baseString);
                            imageArray = baseString;
                            FileName = imageName;

                            if (AttachmentCount < 3)
                            {
                                if (file != null)
                                {
                                    attachment = baseString;

                                    string base64String = Convert.ToBase64String(attachment, 0, attachment.Length);
                                    AttachmentName = FileName;

                                    float sizemb = attachment.Length / 1024f / 1024f;
                                    AttachmentSize = AttachmentSize + (decimal)sizemb;
                                    if (FileName.Contains("."))
                                    {
                                        string Extention = FileName.Split('.')[1];//pdf
                                        if (Extention.ToLower() == "jpg" || Extention.ToLower() == "jpeg")
                                        {
                                            if (TotalAttachmentSize <= 30)
                                            {
                                                AttachmentSize = Math.Round(Convert.ToDecimal(Convert.ToDouble(attachment.Length) / 1048576.0), 2);
                                                decimal AttachmentSizeTillFourDecimal = Math.Round(Convert.ToDecimal(Convert.ToDouble(attachment.Length) / 1048576.0), 4);
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
                                                                //MessagingCenter.Unsubscribe<object, string>(this, "OnCameraClicked");
                                                                //MessagingCenter.Unsubscribe<object, string>(this, "OnGalleryClicked");

                                                            }
                                                            else
                                                            {
                                                                AttachmentName = string.Empty;
                                                                // _dialogService.ShowMessage(AppResources.ZZGeneralMessage_FileWithTheSameNameAlreadyExists, AppResources.Information);
                                                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_FileWithTheSameNameAlreadyExists));
                                                            }
                                                            //  UploadedDocumentsListObj = new List<UploadedDocumentsList>();

                                                        }
                                                        catch (Exception)
                                                        {
                                                        }
                                                    }
                                                    else
                                                    {
                                                        AttachmentName = string.Empty;
                                                        //  _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
                                                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // _dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
                                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly));
                                        }
                                    }
                                }
                            }
                            else
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZMaximumnoof3attachmentscanbeuploaded));
                            }
                        }



                    });
                }
                catch (Exception)
                {
                }



            });
        }


        //public void AddAttachment()
        //{
        //    //imageArray = null;
        //    //FileName = AppResources.NoFilechosen;
        //    MainThread.BeginInvokeOnMainThread(async () =>
        //    {
        //        await CrossMedia.Current.Initialize();

        //        try
        //        {

        //            MainThread.BeginInvokeOnMainThread(async () =>
        //            {
        //                MediaFile file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
        //                {
        //                    PhotoSize = PhotoSize.Medium
        //                    //CompressionQuality = 92
        //                });

        //                if (file != null)
        //                {
        //                    Stream s = file.GetStream();
        //                    var filePath = await ReadFully(file.GetStream());

        //                }
        //                if (file != null)
        //                {
        //                    var filePath = await ReadFully(file.GetStream());
        //                }
        //                if (file == null)
        //                {
        //                    return;
        //                }

        //                if (file != null)
        //                {

        //                    var imagePath = file.Path;
        //                    var imageName = Path.GetFileName(imagePath);
        //                    byte[] baseString = DependencyService.Get<Core.Interfaces.IDeviceInfo>().GetImagePathByteArray(file.Path);
        //                    imageArray = baseString;
        //                    FileName = imageName;

        //                    if (AttachmentCount < 3)
        //                    {
        //                        if (file != null)
        //                        {
        //                            attachment = baseString;

        //                            string base64String = Convert.ToBase64String(attachment, 0, attachment.Length);
        //                            AttachmentName = FileName;

        //                            float sizemb = attachment.Length / 1024f / 1024f;
        //                            AttachmentSize = AttachmentSize + (decimal)sizemb;
        //                            if (FileName.Contains("."))
        //                            {
        //                                string Extention = FileName.Split('.')[1];//pdf
        //                                if (Extention.ToLower() == "jpg" || Extention.ToLower() == "jpeg")
        //                                {
        //                                    if (TotalAttachmentSize <= 30)
        //                                    {
        //                                        AttachmentSize = Math.Round(Convert.ToDecimal(Convert.ToDouble(attachment.Length) / 1048576.0), 2);
        //                                        decimal AttachmentSizeTillFourDecimal = Math.Round(Convert.ToDecimal(Convert.ToDouble(attachment.Length) / 1048576.0), 4);
        //                                        if (Convert.ToDecimal(AttachmentSize) <= 10)
        //                                        {
        //                                            if (Convert.ToDecimal(AttachmentSizeTillFourDecimal) > 0)
        //                                            {
        //                                                bool isAttachmentexixt = false;

        //                                                try
        //                                                {
        //                                                    UploadedDocumentsList a = new UploadedDocumentsList();
        //                                                    a.FileNameWithExtension = AttachmentName;
        //                                                    a.DocBinaryInBase64 = attachment;
        //                                                    a.Size = AttachmentSize.ToString();

        //                                                    string attachmentType = UtilityManager.GetContentType(Extention);
        //                                                    a.MimeType = attachmentType;
        //                                                    foreach (UploadedDocumentsList ItemA in UploadedDocumentsListObj)
        //                                                    {
        //                                                        if (AttachmentName == ItemA.FileNameWithExtension)
        //                                                        {
        //                                                            isAttachmentexixt = true;
        //                                                        }
        //                                                    }
        //                                                    if (isAttachmentexixt == false)
        //                                                    {
        //                                                        UploadedDocumentsListObj.Add(a);
        //                                                        AttachmentCount++;
        //                                                        AttachmentName = string.Empty;

        //                                                    }
        //                                                    else
        //                                                    {
        //                                                        AttachmentName = string.Empty;
        //                                                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_FileWithTheSameNameAlreadyExists));
        //                                                    }

        //                                                }
        //                                                catch (Exception)
        //                                                {


        //                                                }
        //                                            }
        //                                            else
        //                                            {
        //                                                AttachmentName = string.Empty;
        //                                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));
        //                                            }
        //                                        }
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly));
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZMaximumnoof3attachmentscanbeuploaded));
        //                    }
        //                }



        //            });
        //        }
        //        catch (Exception)
        //        {


        //        }



        //    });
        //}


        public static async Task<byte[]> ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return await Task.FromResult(ms.ToArray());
            }
        }
        private string GetFileSize(double byteCount)
        {
            string size = "0 Bytes";
            if (byteCount >= 1073741824.0)
            {
                size = string.Format("{0:##.##}", byteCount / 1073741824.0) + " GB";
            }
            else if (byteCount >= 1048576.0)
            {
                size = string.Format("{0:##.##}", byteCount / 1048576.0) + " MB";
            }
            else if (byteCount >= 1024.0)
            {
                size = string.Format("{0:##.##}", byteCount / 1024.0) + " KB";
            }
            else if (byteCount > 0 && byteCount < 1024.0)
            {
                size = byteCount.ToString() + " Bytes";
            }

            return size;
        }



        #endregion
    }
}
