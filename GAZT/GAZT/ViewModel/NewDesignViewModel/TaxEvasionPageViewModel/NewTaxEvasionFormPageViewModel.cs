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
        private readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

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
        private string _PageTitle = "Reporter Information";
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

        private string _BodyTitle = "Complete the below details";
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
        #endregion

        #region Commands
        public ICommand OnContinueClicked { get; set; }
        public ICommand OnBackStepClicked { get; set; }
        public ICommand OnAttachmentClick { get; set; }
        #endregion

        #region Constructor
        public NewTaxEvasionFormPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnContinueClicked = new Command(() => navigateToNext());
            OnBackStepClicked = new Command(() => navigateToBack());
            OnAttachmentClick = new Xamarin.Forms.Command(async () =>
            {
                await AddAttachment();
            });
        }
        #endregion

        #region Method
        private void navigateToNext()
        {
            switch (currentTab)
            {
                case NewTaxEvasionTabEnum.ReporterInfo: currentTab = NewTaxEvasionTabEnum.FacilityInfo;
                    PageTitle = "Facility Information";
                    break;

                case NewTaxEvasionTabEnum.FacilityInfo: currentTab = NewTaxEvasionTabEnum.ReportDetails;
                    PageTitle = "Report Details";
                    break;

                case NewTaxEvasionTabEnum.ReportDetails: 
                    currentTab = NewTaxEvasionTabEnum.Summary;
                    PageTitle = "Summary";
                    BodyTitle = "Review the below information";
                    break;
            }
        }

        private void navigateToBack()
        {
            switch (currentTab)
            {
                case NewTaxEvasionTabEnum.Summary: 
                    currentTab = NewTaxEvasionTabEnum.ReportDetails;
                    PageTitle = "Report Details";
                    BodyTitle = "Complete the below details";
                    break;

                case NewTaxEvasionTabEnum.ReportDetails: 
                    currentTab = NewTaxEvasionTabEnum.FacilityInfo;
                    PageTitle = "Facility Information";
                    break;

                case NewTaxEvasionTabEnum.FacilityInfo:
                    currentTab= NewTaxEvasionTabEnum.ReporterInfo;
                    PageTitle = "Reporter Information";
                    break;
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
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                            _navigationService.GoBack();
                        });
                    }
                }
            }
            catch (Exception ex)
            {

                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
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
                //TaxEvasionReportTobeUsedToSubmit.Content = TReportDetail;

                TaxEvasionReportTobeUsedToSubmit.Facilities = TFaciName;
                TaxEvasionReportTobeUsedToSubmit.PhoneNumber = TMobNumber;
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
                TaxEvasionReportTobeUsedToSubmit.Longitude = _longitude.ToString();

                TaxEvasionCreateReportResponseModel response = new TaxEvasionCreateReportResponseModel();
                response = await WebServiceManager.GAZTTaxEvasionCreateReport(TaxEvasionReportTobeUsedToSubmit, newList);

                if (response != null && response.Status == true)
                {
                    //ZTEReportReportSuccessResponsep1
                    var resmessage = AppResources.ZTEReportReportSuccessResponsep1;
                    var newrm = resmessage.Replace("Report Number", response.Data.TicketId);
                    var newReplacedMsg = newrm.Replace("5", "10");

                    await _dialogService.ShowMessage(newReplacedMsg, AppResources.ZZZSubmittedReport);
                    var _navigation = Application.Current.MainPage.Navigation;
                    var _lastPage = _navigation.NavigationStack.LastOrDefault();
                    //Remove last page
                    _navigation.RemovePage(_lastPage);
                    var _lastPage2 = _navigation.NavigationStack.LastOrDefault();
                    //Remove last page
                    _navigation.RemovePage(_lastPage2);
                    //Go back 

                    _navigation.PopAsync();
                    //_navigationService.NavigateTo(App.TaxEvasionReportListPageView);
                }
                else
                {//ZTEReportReportSuccessResponsep2
                    _dialogService.ShowMessage(AppResources.ZTEReportReportSuccessResponsep2, " ");
                }
                await Task.Run(() =>
                {
                    App.HideProgressView();
                });
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

                    _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    //viewModel._navigationService.GoBack();
                });
            }
            catch (Exception)
            {
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
