using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.TaxEvasionReportFormPage
{
    public class TaxEvasionReportFormPageViewModel : ViewModelBase
    {
        #region variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand BackButtonClicked { get; set; }
        public ICommand NextButtonClicked { get; set; }
        public ICommand SubmitReportClicked { get; set; }
        #endregion 
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
        public ICommand OnAttachmentClick { get; set; }
        public static decimal AttachmentUploadedSize;
        byte[] attachment;
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
        public bool _isVisibleForReportDisplay = false;
        public bool IsVisibleForReportDisplay
        {
            get
            {
                return _isVisibleForReportDisplay;
            }
            set
            {
                _isVisibleForReportDisplay = value;
                RaisePropertyChanged("IsVisibleForReportDisplay");
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
        private string _txtFType = string.Empty;
        public string TxtFType
        {
            get
            {
                return _txtFType;
            }
            set
            {
                _txtFType = value;
                RaisePropertyChanged("TxtFType");
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
        private bool _isSubmitButtonEnable = false;
        public bool IsSubmitButtonEnable
        {
            get
            {
                return _isSubmitButtonEnable;
            }
            set
            {
                _isSubmitButtonEnable = value;
                RaisePropertyChanged("IsSubmitButtonEnable");
            }
        }
        private bool _isTIN = false;
        public bool IsTIN
        {
            get
            {
                return _isTIN;
            }
            set
            {
                _isTIN = value;
                if (_isTIN != null && _isTIN == true)
                {
                    IsTINVisible = true;
                    TxtTIN = string.Empty;
                }
                else
                {
                    IsTINVisible = false;
                    TxtTIN = string.Empty;
                }
                RaisePropertyChanged("IsTIN");
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
        private bool _showIdHelperText = false;
        public bool ShowIdHelperText
        {
            get
            {
                return _showIdHelperText;
            }
            set
            {
                _showIdHelperText = value;
                RaisePropertyChanged("ShowIdHelperText");
            }
        }
        private bool _isTINVisible = false;
        public bool IsTINVisible
        {
            get
            {
                return _isTINVisible;
            }
            set
            {
                _isTINVisible = value;
                if (_isTINVisible == false)
                {
                    ShowIdHelperText = true;
                }
                RaisePropertyChanged("IsTINVisible");
            }
        }//SelectedTaxEvasionCompanyType
        private FacilityCompanyType _selectedTaxEvasionCompanyType = null;
        public FacilityCompanyType SelectedTaxEvasionCompanyType
        {
            get
            {
                return _selectedTaxEvasionCompanyType;
            }
            set
            {
                _selectedTaxEvasionCompanyType = value;
                if (_selectedTaxEvasionCompanyType != null)
                {
                    TxtFType = _selectedTaxEvasionCompanyType.Name;
                }
                RaisePropertyChanged("SelectedTaxEvasionCompanyType");
            }
        }
        private FacilityCompanyType _selectedTaxEvasionCompanyTypePrev = null;
        public FacilityCompanyType SelectedTaxEvasionCompanyTypePrev
        {
            get
            {
                return _selectedTaxEvasionCompanyTypePrev;
            }
            set
            {
                _selectedTaxEvasionCompanyTypePrev = value;
                RaisePropertyChanged("SelectedTaxEvasionCompanyTypePrev");
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
        private bool _isVisiblePickerAr = false;
        public bool IsVisiblePickerAr
        {
            get
            {
                return _isVisiblePickerAr;
            }
            set
            {
                _isVisiblePickerAr = value;
                RaisePropertyChanged("IsVisiblePickerAr");
            }
        }
        private bool _isVisiblePickerEn = false;
        public bool IsVisiblePickerEn
        {
            get
            {
                return _isVisiblePickerEn;
            }
            set
            {
                _isVisiblePickerEn = value;
                RaisePropertyChanged("IsVisiblePickerEn");
            }
        }
        private string _tEmail = string.Empty;
        public string TEmail
        {
            get
            {
                return _tEmail;
            }
            set
            {
                _tEmail = value;
                RaisePropertyChanged("TEmail");
            }
        }
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
                catch (Exception)
                {


                }
            }
        }
        private string _tCategory = string.Empty;
        public string TCategory
        {
            get
            {
                return _tCategory;
            }
            set
            {
                try
                {
                    _tCategory = value;
                    RaisePropertyChanged("TCategory");
                }
                catch (Exception)
                {


                }
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
        }//TFaciOwnerName
        private string _tFaciOwnerName = string.Empty;
        public string TFaciOwnerName
        {
            get
            {
                return _tFaciOwnerName;
            }
            set
            {
                _tFaciOwnerName = value;
                RaisePropertyChanged("TFaciOwnerName");
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
        //TFaciMobNo
        private string _tFaciMobNo = string.Empty;
        public string TFaciMobNo
        {
            get
            {
                return _tFaciMobNo;
            }
            set
            {
                _tFaciMobNo = value;
                RaisePropertyChanged("TFaciMobNo");
            }
        }//TFaciEmail
        private string _tFaciEmail = string.Empty;
        public string TFaciEmail
        {
            get
            {
                return _tFaciEmail;
            }
            set
            {
                _tFaciEmail = value;
                //if (!string.IsNullOrEmpty(_tFaciEmail))
                //{
                //    _TaxEvasionReportTobeUsedToSubmit.CompanyEmail = _tFaciEmail;
                //}
                RaisePropertyChanged("TFaciEmail");
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
        //TID
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
        //TFDAdress
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
        private string _datePickPrev = string.Empty;
        public string DatePickPrev
        {
            get
            {
                return _datePickPrev;
            }
            set
            {
                _datePickPrev = value;
                RaisePropertyChanged("DatePickPrev");
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
        private TaxEvasionRegionCityDatum _selectLCTypePrev = null;
        public TaxEvasionRegionCityDatum SelectLCTypePrev
        {
            get
            {
                return _selectLCTypePrev;
            }
            set
            {
                _selectLCTypePrev = value;
                RaisePropertyChanged("SelectLCTypePrev");
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
        public TaxEvasionReportFormPageViewModel(INavigationService navigationService, IDialogService dialogService)
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
            BackButtonClicked = new Command(() =>
            {
                _navigationService.GoBack();
            });

            SubmitReportClicked = new Command(() =>
            {
                IsLoading = false;
                //  _navigationService.NavigateTo(App.TaxEvasionAttachmentPageView, selectedtaxEList);

            });
            OnAttachmentClick = new Command(async () =>
            {
                await AddAttachment();
            });
        }


        public async Task AddAttachment()
        {
            if (AttachmentCount < 3)
            {
                string[] filetypes;

                filetypes = DependencyService.Get<Core.Interfaces.IDeviceInfo>().GetAttachmentTypeStringForTaxEvasion();

               
                PickOptions options = UtilityManager.GetFilePickerOptionsForChooser(filetypes);
                //var fileData = await CrossFilePicker.Current.PickFile(filetypes);

                var fileData = await FilePicker.PickAsync(options);
                var stream = await fileData.OpenReadAsync();
                var attachment = UtilityManager.ReadFully(stream as Stream);
                if (fileData != null)
                {

                    string base64String = Convert.ToBase64String(attachment, 0, attachment.Length);
                    AttachmentName = fileData.FileName;

                    if (fileData.FileName.Contains("."))
                    {
                        string[] ExtensionArray = fileData.FileName.Split('.');
                        string Extention = ExtensionArray.Last();
                        if (Extention.ToLower() == "doc" || Extention.ToLower() == "docx" || Extention.ToLower() == "jpg" || Extention.ToLower() == "pdf" || Extention.ToLower() == "jpeg")
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

                                            string attachmentType = UtilityManager.GetContentType(Extention);
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
                                        }
                                        catch (Exception)
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

        public async Task OnPageLoad()
        {
            try
            {
                if (!string.IsNullOrEmpty(selectedtaxEList.TicketId))
                {

                }
                else
                {
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
                            NoInternetGoBack();
                        }

                    }
                    catch (InternetException ex)
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                            _navigationService.GoBack();
                        });
                    }
                }
            }
            catch (Exception)
            {

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }
        public async void NoInternetGoBack()
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await _dialogService.ShowMessage(AppResources.NetworkConnectivityIssue, AppResources.Information);
                _navigationService.GoBack();
            });
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
                        citylist = await TaxEvasionWebServiceManager.GAZTTaxEvasionGetAllCitiesByRegion(SelectedTaxEvasionRegion.Id);
                        PopToRootPage();
                        CList = citylist.Data;
                    }
                    else
                    {

                    }
                }
                catch (InternetException ex)
                {
                    //MainThread.BeginInvokeOnMainThread(async () =>
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
        public async Task SubmitCreatedReport()
        {

            try
            {
                string date = DateTime.UtcNow.ToString("dd/MM/yyyy");//1902/03/09
                date = DatePick;
                TaxEvasionReportTobeUsedToSubmit = new TaxEvasionReportDetails();
                TaxEvasionReportTobeUsedToSubmit.CreatedAt = date;
                //TaxEvasionReportTobeUsedToSubmit.CompanyType = SelectedTaxEvasionCompanyType.Id;

                TaxEvasionReportTobeUsedToSubmit.Category = _selectedCategory;

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
                //TaxEvasionReportTobeUsedToSubmit.PhoneNumber = TMobNumber;
                TaxEvasionReportTobeUsedToSubmit.Id = TID;
                TaxEvasionReportTobeUsedToSubmit.VatNumber = TVatNumber;
                TaxEvasionReportTobeUsedToSubmit.Longitude = Longitude.ToString();
                TaxEvasionReportTobeUsedToSubmit.Latitude = Latitude.ToString();
                TaxEvasionReportTobeUsedToSubmit.District = TFDAdress;
                TaxEvasionReportTobeUsedToSubmit.Street = TFSAddress;
                TaxEvasionReportTobeUsedToSubmit.WorkType = TFWType;
                TaxEvasionReportTobeUsedToSubmit.RegionCode = Convert.ToString(SelectedTaxEvasionRegion.Id);
                TaxEvasionReportTobeUsedToSubmit.City = Convert.ToString(SelectLCType.Id);

                List<UploadedDocumentsList> newList = UploadedDocumentsListObj.ToList();
                TaxEvasionReportTobeUsedToSubmit.PhoneNumber = App.TaxEvasionUserData.Mobile;

                TaxEvasionReportDetails tex = TaxEvasionReportTobeUsedToSubmit;

                _navigationService.NavigateTo(App.TaxEvasionAttachmentPageView, tex);
            }

            //    TaxEvasionCreateReportResponseModel response = new TaxEvasionCreateReportResponseModel();
            //    response = await WebServiceManager.GAZTTaxEvasionCreateReport(TaxEvasionReportTobeUsedToSubmit, newList);

            //    if (response != null && response.Status == true)
            //    {
            //        //ZTEReportReportSuccessResponsep1
            //        var resmessage = AppResources.ZTEReportReportSuccessResponsep1;
            //        var newrm = resmessage.Replace("Report Number", response.Data.TicketId);
            //        var newReplacedMsg = newrm.Replace("5","10");

            //        await _dialogService.ShowMessage(newReplacedMsg, AppResources.ZZZSubmittedReport);
            //        var _navigation = Application.Current.MainPage.Navigation;
            //        var _lastPage = _navigation.NavigationStack.LastOrDefault();
            //        //Remove last page
            //        _navigation.RemovePage(_lastPage);
            //        //Go back 
            //        _navigation.PopAsync();
            //        //_navigationService.NavigateTo(App.TaxEvasionReportListPageView);
            //    }
            //    else
            //    {//ZTEReportReportSuccessResponsep2
            //        _dialogService.ShowMessage(AppResources.ZTEReportReportSuccessResponsep2, " ");
            //    }
            //}
            //catch (GAZTException gex)
            //{
            //    // Handle the GAZT custom exception.
            //    string MessageForTheUser = gex.Message;
            //    if (gex is GAZTInvalidDataException)
            //    {
            //        MessageForTheUser = AppResources.ZZSomethingwentwrong;
            //    }
            //    if (gex is GAZTNetworkConnectivityIssueException)
            //    {
            //        MessageForTheUser = AppResources.NetworkConnectivityIssue;
            //    }
            //    else if (gex is GAZTInternetException)
            //    {
            //        MessageForTheUser = AppResources.ZZInternetConnectionMessage;
            //    }
            //    else if (gex is GAZTSessionExpiredException)
            //    {
            //        MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
            //    }
            //    MainThread.BeginInvokeOnMainThread(async () =>
            //    {
            //        await Task.Run(() =>
            //        {
            //            IsLoading = false;
            //        });

            //        _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
            //        //viewModel._navigationService.GoBack();
            //    });
            //}
            catch (Exception)
            {


                //    MainThread.BeginInvokeOnMainThread(async () =>
                //    {
                //        await Task.Run(() =>
                //        {
                //            IsLoading = false;
                //        });

                //        await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                //    });
            }
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
        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });
            }
        }
    }
}
