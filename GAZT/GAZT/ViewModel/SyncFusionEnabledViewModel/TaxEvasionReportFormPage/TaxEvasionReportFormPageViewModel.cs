using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using Plugin.FilePicker;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.TaxEvasionReportFormPage_ViewModel
{
    public class TaxEvasionReportFormPageViewModel : ViewModelBase
    {
        #region variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand BackButtonClicked { get; set; }
        public ICommand SubmitReportClicked { get; set; }
        #endregion 
        private TaxEvasionReportTobeUsedToSubmit _TaxEvasionReportTobeUsedToSubmit;
        public TaxEvasionReportTobeUsedToSubmit TaxEvasionReportTobeUsedToSubmit
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
        public static Decimal AttachmentUploadedSize;
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
        private UploadedDocumentsList _uploadedDocumentsList= null;
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
        private TaxEvasionReport _selectedtaxEList = null;
        public TaxEvasionReport selectedtaxEList
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
        private FacilityCompanyType _selectedTaxEvasionCompanyType= null;
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
        private string _selectedCategory= string.Empty;
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
                catch (Exception ex)
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
        private TERRegion _selectedTaxEvasionRegion = null;
        public TERRegion SelectedTaxEvasionRegion
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
                        TxtReportDetailRegion = _selectedTaxEvasionRegion.RegionNameAR;
                    }
                    else
                    {
                        TxtReportDetailRegion = _selectedTaxEvasionRegion.RegionNameEN;
                    }
                    //TEReportobj.RegionCode = v;
                }
                //ListFormBudles = null;
                RaisePropertyChanged("SelectedTaxEvasionRegion");
            }
        }
        private TERRegion _selectedTaxEvasionRegionPrev = null;
        public TERRegion SelectedTaxEvasionRegionPrev
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
        private string _datePick =DateTime.UtcNow.ToString("dd/MM/yyyy");
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
        private TERCity _selectLCType= null;
        public TERCity SelectLCType
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
                        TxtReportDetailCity = _selectLCType.CityNameAR;
                    }
                    else
                    {
                        TxtReportDetailCity = _selectLCType.CityNameEN;
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
        private TERCity _selectLCTypePrev = null;
        public TERCity SelectLCTypePrev
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
        private List<TERRegion> _rList= null;
        public List<TERRegion> RList
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
        private List<TERCity> _cityList= null;
        public List<TERCity> CList
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
            BackButtonClicked = new Xamarin.Forms.Command(() =>
            {
                _navigationService.GoBack();
            });
            SubmitReportClicked = new Xamarin.Forms.Command(() =>
            {
            });
            OnAttachmentClick = new Xamarin.Forms.Command(async () =>
            {
                await AddAttachment();
            });
        }
        public async Task AddAttachment()
        {
            if (AttachmentCount < 3)
            {
                string[] filetypes;

                filetypes = DependencyService.Get<IDeviceInfo>().GetAttachmentTypeStringForTaxEvasion();

    //            if (Device.RuntimePlatform == Device.iOS)
    //            {
    //                filetypes = new string[] {
    ////            UTType.PDF,
    ////            "org.openxmlformats.wordprocessingml.document",
    ////            "com.microsoft.word.doc",
    ////"org.openxmlformats.spreadsheetml.sheet",
    ////"org.openxmlformats.presentationml.presentation",
    ////            UTType.JPEG,
    ////            UTType.PNG,
    ////            UTType.GIF,
    ////            "com.microsoft.excel.xls",
    ////            "com.microsoft.powerpoint.​ppt",
    ////             UTType.Text
    //                        };
    //            }
    //            else
    //            {
    //                filetypes = new string[] { "application/pdf", "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "image/jpeg", "image/jpg", "application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "image/png", "application/vnd.ms-powerpoint", "application/vnd.openxmlformats-officedocument.presentationml.presentation", "image/gif", "text/plain" };
    //            }
                var fileData = await CrossFilePicker.Current.PickFile(filetypes);
                //if (AttachmentSize < 10)
                //{
                if (fileData != null)
                {
                    attachment = fileData.DataArray;
                    string base64String = Convert.ToBase64String(attachment, 0, attachment.Length);
                    AttachmentName = fileData.FileName;
                    //float sizemb = (attachment.Length / 1024f) / 1024f;
                    //AttachmentSize = AttachmentSize + sizemb;
                    if (fileData.FileName.Contains("."))
                    {
                        string Extention = fileData.FileName.Split('.')[1];//pdf
                        if (Extention.ToLower() == "doc" || Extention.ToLower() == "docx" || Extention.ToLower() == "jpg" || Extention.ToLower() == "pdf" || Extention.ToLower() == "jpeg")
                        {
                            if (TotalAttachmentSize <= 30)
                            {
                                AttachmentSize = Math.Round(Convert.ToDecimal((Convert.ToDouble(attachment.Length) / 1048576.0)), 2);
                                if (Convert.ToDecimal(AttachmentSize) <= 10)
                                {
                                    try
                                    {
                                        UploadedDocumentsList a = new UploadedDocumentsList();
                                        a.FileNameWithExtension = AttachmentName;
                                        a.DocBinaryInBase64 = base64String;
                                        AttachmentCount++;
                                        string attachmentType = UtilityManager.GetContentType(Extention);
                                        //UploadedDocumentsList.DocBinaryInBase64 = base64String;
                                        //UploadedDocumentsList.FileNameWithExtension = AttachmentName;
                                        a.MimeType = attachmentType;
                                        //  UploadedDocumentsListObj = new List<UploadedDocumentsList>();
                                        UploadedDocumentsListObj.Add(a);
                                    }
                                    catch (Exception ex)
                                    {
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


                if (!string.IsNullOrEmpty(selectedtaxEList.ReportNumber))
                {

                }
                else
                {

                    try
                    {
                        //await Task.Run(async() =>
                        //{


                        //string date = DateTime.UtcNow.ToString("yyyy//MM/dd");
                        TERFRegionRootObject regionlist = new TERFRegionRootObject();



                        regionlist = await WebServiceManager.GAZTTESFormGetRegion();
                        if (regionlist != null && regionlist.RegionList.Count != 0)
                        {
                            if (CList != null && CList.Count > 0)
                            {
                                CList.Clear();
                                TxtReportDetailCity = string.Empty;
                            }
                            RList = regionlist.RegionList;
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
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            _navigationService.GoBack();
                        });
                    }

                }

            }
            catch(Exception ex)
            {

                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }





        }
        public async  void NoInternetGoBack()
        {
            Device.BeginInvokeOnMainThread(async () =>
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
            await Task.Run(async() =>
            {
                try
                {
                    if (SelectedTaxEvasionRegion != null && SelectedTaxEvasionRegion.RegionCode != null)
                    {
                        TERFCityRetrieveRootObject citylist = new TERFCityRetrieveRootObject();
                        citylist = await WebServiceManager.GAZTTESFormGetCity(SelectedTaxEvasionRegion.RegionCode);
                        PopToRootPage();
                        CList = citylist.CityList;
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
        public async Task SubmitCreatedReport()
        {
            try
            {
                string date = DateTime.UtcNow.ToString("dd/MM/yyyy");//1902/03/09
                date = DatePick;
                TaxEvasionReportTobeUsedToSubmit = new TaxEvasionReportTobeUsedToSubmit();
                TaxEvasionReportTobeUsedToSubmit.ReceivedDate = date;
                TaxEvasionReportTobeUsedToSubmit.CompanyType = SelectedTaxEvasionCompanyType.Id;
                TaxEvasionReportTobeUsedToSubmit.ViolationType = _selectedCategory;
                TaxEvasionReportTobeUsedToSubmit.Channel = "2";
                TaxEvasionReportTobeUsedToSubmit.WSPassword = "gazt@123";
                TaxEvasionReportTobeUsedToSubmit.WSUserName = "GAZT@CRM";
                TaxEvasionReportTobeUsedToSubmit.TaxType = "1";
                TaxEvasionReportTobeUsedToSubmit.ReporterName = TName;
                TaxEvasionReportTobeUsedToSubmit.TIN = TxtTIN;
                TaxEvasionReportTobeUsedToSubmit.ReporterEmail = TEmail;
                TaxEvasionReportTobeUsedToSubmit.ReportDetails = TReportDetail;
                TaxEvasionReportTobeUsedToSubmit.HavingTIN = IsTINVisible.ToString().ToLower();
                TaxEvasionReportTobeUsedToSubmit.CompanyName = TFaciName;
                TaxEvasionReportTobeUsedToSubmit.CompanyOwnerName = TFaciOwnerName;
                TaxEvasionReportTobeUsedToSubmit.ReporterMobileNumber = TMobNumber;
                TaxEvasionReportTobeUsedToSubmit.ID = TID;
                TaxEvasionReportTobeUsedToSubmit.VAT = TVatNumber;
                TaxEvasionReportTobeUsedToSubmit.Longitude = Longitude.ToString();
                TaxEvasionReportTobeUsedToSubmit.Latitude = Latitude.ToString();
                TaxEvasionReportTobeUsedToSubmit.CompanyEmail = TFaciEmail;
                TaxEvasionReportTobeUsedToSubmit.District = TFDAdress;
                TaxEvasionReportTobeUsedToSubmit.CompanyAddress = TFSAddress;
                TaxEvasionReportTobeUsedToSubmit.WorkType = TFWType;
                TaxEvasionReportTobeUsedToSubmit.RegionCode = SelectedTaxEvasionRegion.RegionCode;
                TaxEvasionReportTobeUsedToSubmit.CityCode = SelectLCType.CityCode;
                List<UploadedDocumentsList> newList = UploadedDocumentsListObj.ToList<UploadedDocumentsList>();
                TaxEvasionReportTobeUsedToSubmit.CompanyMobileNumber = TFaciMobNo;
                TEReportResponsePostRootObject response = new TEReportResponsePostRootObject();
                response = await WebServiceManager.GAZTTESReportSubmit(TaxEvasionReportTobeUsedToSubmit, newList);
                if (response != null && response.Success == true)
                { //ZTEReportReportSuccessResponsep1
                    var resmessage = AppResources.ZTEReportReportSuccessResponsep1;
                    var newrm = resmessage.Replace("Report Number", response.TaxEvasionNumber);
                    await _dialogService.ShowMessage(newrm, AppResources.ZZZSubmittedReport);
                    var _navigation = Application.Current.MainPage.Navigation;
                    var _lastPage = _navigation.NavigationStack.LastOrDefault();
                    //Remove last page
                    _navigation.RemovePage(_lastPage);
                    //Go back 
                    _navigation.PopAsync();
                    //_navigationService.NavigateTo(App.TaxEvasionReportListPageView);
                }
                else
                {//ZTEReportReportSuccessResponsep2
                    _dialogService.ShowMessage(AppResources.ZTEReportReportSuccessResponsep2, " ");
                }
            }
            catch (Exception ex)
            {
                _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
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
            catch (Exception ex)
            {
            }
        }
        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });
            }
        }
    }
}
