using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using MobileCoreServices;
using Plugin.FilePicker;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GAZT.ViewModel.NewViewModel
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


        private UploadedDocumentsList _uploadedDocumentsList;
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
        }//IsLoading
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
                if (!string.IsNullOrEmpty(_txtTIN))
                {
                    if (_TaxEvasionReportTobeUsedToSubmit != null && _TaxEvasionReportTobeUsedToSubmit.TIN != null)
                    {

                        _TaxEvasionReportTobeUsedToSubmit.TIN = _txtTIN;
                    }
                }
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
                if (_isTINVisible != null)
                {
                    if (_TaxEvasionReportTobeUsedToSubmit != null && _TaxEvasionReportTobeUsedToSubmit.HavingTIN != null)
                    {
                        _TaxEvasionReportTobeUsedToSubmit.HavingTIN = "true";

                    }
                }
                RaisePropertyChanged("IsTINVisible");
            }
        }//SelectedTaxEvasionCompanyType
        private FacilityCompanyType _selectedTaxEvasionCompanyType;
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
                    if (_TaxEvasionReportTobeUsedToSubmit != null)
                    {
                        TxtFType = _selectedTaxEvasionCompanyType.Name;

                    }
                }
                RaisePropertyChanged("SelectedTaxEvasionCompanyType");
            }
        }





        private string _selectedCategory;
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
        }//TReportDetail
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
                if (_tReportDetail != null)
                {
                    if (_TaxEvasionReportTobeUsedToSubmit != null && _TaxEvasionReportTobeUsedToSubmit.ReportDetails != null)
                    {
                        _TaxEvasionReportTobeUsedToSubmit.ReportDetails = _tReportDetail;
                    }
                }

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
                //_tEReportobj.ViolationType = _selectedCategory;

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
                if (!string.IsNullOrEmpty(_tEmail))
                {
                    if (_TaxEvasionReportTobeUsedToSubmit != null && _TaxEvasionReportTobeUsedToSubmit.ReporterEmail != null)

                    {
                        _TaxEvasionReportTobeUsedToSubmit.ReporterEmail = _tEmail;
                    }

                }


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

                    if (!string.IsNullOrEmpty(_tName))
                    {
                        if (TaxEvasionReportTobeUsedToSubmit != null && TaxEvasionReportTobeUsedToSubmit.ReporterName != null)
                        {
                            TaxEvasionReportTobeUsedToSubmit.ReporterName = _tName;
                        }

                    }


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
                if (!string.IsNullOrEmpty(_tMobNumber))
                {
                    if (_TaxEvasionReportTobeUsedToSubmit != null && _TaxEvasionReportTobeUsedToSubmit.ReporterMobileNumber != null)
                    {
                        _TaxEvasionReportTobeUsedToSubmit.ReporterMobileNumber = _tMobNumber;
                    }

                }

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
                if (!string.IsNullOrEmpty(_tFaciName))
                {
                    if (_TaxEvasionReportTobeUsedToSubmit != null && _TaxEvasionReportTobeUsedToSubmit.CompanyName != null)
                    {
                        _TaxEvasionReportTobeUsedToSubmit.CompanyName = _tFaciName;
                    }

                }





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
                if (!string.IsNullOrEmpty(_tFaciOwnerName))
                {
                    if (_TaxEvasionReportTobeUsedToSubmit != null && _TaxEvasionReportTobeUsedToSubmit.CompanyName != null)
                    {
                        _TaxEvasionReportTobeUsedToSubmit.CompanyOwnerName = _tFaciOwnerName;
                    }

                }





                RaisePropertyChanged("TFaciOwnerName");
            }
        }
        private string _attachmentName = "";
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
                if (!string.IsNullOrEmpty(_tFaciMobNo))
                {
                    _TaxEvasionReportTobeUsedToSubmit.CompanyMobileNumber = _tFaciMobNo;
                }





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

                if (!string.IsNullOrEmpty(_tFaciEmail))
                {
                    _TaxEvasionReportTobeUsedToSubmit.CompanyEmail = _tFaciEmail;
                }





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
                if (!string.IsNullOrEmpty(_tID))
                { _TaxEvasionReportTobeUsedToSubmit.ID = _tID; }

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
                if (!string.IsNullOrEmpty(_tVatNumber))
                {
                    _TaxEvasionReportTobeUsedToSubmit.VAT = _tVatNumber;
                }

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
                if (!string.IsNullOrEmpty(_tFDAdress))
                {
                    _TaxEvasionReportTobeUsedToSubmit.District = _tFDAdress;
                }




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
                if (!string.IsNullOrEmpty(_tFSAddress))
                {
                    _TaxEvasionReportTobeUsedToSubmit.CompanyAddress = _tFSAddress;
                }


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
                if (!string.IsNullOrEmpty(_tFWType))
                { _TaxEvasionReportTobeUsedToSubmit.WorkType = _tFWType; }

                RaisePropertyChanged("TFWType");
            }
        }






        private TERRegion _selectedTaxEvasionRegion;
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

                    _TaxEvasionReportTobeUsedToSubmit.RegionCode = _selectedTaxEvasionRegion.RegionCode;
                    //IsCPickerEnable = true;
                    //string v= SelectedTaxEvasionRegion.RegionCode;
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
        private string _datePick = string.Empty;

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

        private TERCity _selectLCType;
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
                    _TaxEvasionReportTobeUsedToSubmit.CityCode = _selectLCType.CityCode;
                    if (string.IsNullOrEmpty(_selectLCType.Latitude))
                    { _TaxEvasionReportTobeUsedToSubmit.Latitude = "0.0"; }
                    else
                    { _TaxEvasionReportTobeUsedToSubmit.Latitude = _selectLCType.Latitude; }
                    if (string.IsNullOrEmpty(_selectLCType.Latitude))
                    { _TaxEvasionReportTobeUsedToSubmit.Longitude = "0.0"; }
                    else
                    { _TaxEvasionReportTobeUsedToSubmit.Longitude = _selectLCType.Longitude; }

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

                }





                //ListFormBudles = null;
                RaisePropertyChanged("SelectLCType");
            }
        }









        private List<TERRegion> _rList;
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

        private List<TERCity> _cityList;
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


                /*if(regionlist != null && regionlist.RegionList.Count != 0)*/
                //{ RList = regionlist.RegionList; }


                //TEReportResponsePostRootObject
                //_navigationService.NavigateTo(App.TaxEvasionReportFormPageView);

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
                if (Device.RuntimePlatform == Device.iOS)
                {
                    filetypes = new string[] {

                UTType.PDF,
                "org.openxmlformats.wordprocessingml.document",
                "com.microsoft.word.doc",
    "org.openxmlformats.spreadsheetml.sheet",
    "org.openxmlformats.presentationml.presentation",
                UTType.JPEG,
                UTType.PNG,
                UTType.GIF,
                "com.microsoft.excel.xls",
                "com.microsoft.powerpoint.​ppt",
                 UTType.Text
                            };
                }
                else
                {
                    filetypes = new string[] { "application/pdf", "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "image/jpeg", "image/jpg", "application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "image/png", "application/vnd.ms-powerpoint", "application/vnd.openxmlformats-officedocument.presentationml.presentation", "image/gif", "text/plain" };

                }

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



                    }


                }
                //}
                //else
                //{ _dialogService.ShowMessage("Attachment size Cannot exceed 10MB", AppResources.Information); }



            }


        }


        public async Task onPageLoad()
        {

            //   SelectedTaxEvasionCompanyType = ListFacilityCompanyType.Where(x => x.Id == "1").FirstOrDefault();
            //_tEReportobj.ViolationType = _selectedCategory;
            //_tEReportobj.Channel = "2";
            //_tEReportobj.WSPassword = "gazt@123";
            //_tEReportobj.WSUserName = "GAZT@CRM";
            //_facilityCompanyType.Name=AppResources


            try
            {
                TERFRegionRootObject regionlist = new TERFRegionRootObject();
                ////string lang = UtilityManager.GetLanguageParameter();
                regionlist = await WebServiceManager.GAZTTESFormGetRegion();
                ////PopToRootPage();
                ///FormBundleList = formbundleList.d.results;
                if (regionlist != null && regionlist.RegionList.Count != 0)
                { RList = regionlist.RegionList; }

            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }

        }


        public void onSelectedTaxEvasionRegion()
        {
            try
            {
                TERFCityRetrieveRootObject citylist = new TERFCityRetrieveRootObject();
                citylist = WebServiceManager.GAZTTESFormGetCity(SelectedTaxEvasionRegion.RegionCode);
                PopToRootPage();
                CList = citylist.CityList;
                // FormBundleApplicatioNumberList = formbundleApplicationNumberList.d.results;
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }

        }
        public async Task SubmitCreatedReport()
        {
            try
            {
                string date = DatePick;
                _TaxEvasionReportTobeUsedToSubmit.ReceivedDate = date;
                _TaxEvasionReportTobeUsedToSubmit.CompanyType = SelectedTaxEvasionCompanyType.Id;
                _TaxEvasionReportTobeUsedToSubmit.ViolationType = _selectedCategory;
                _TaxEvasionReportTobeUsedToSubmit.Channel = "2";
                _TaxEvasionReportTobeUsedToSubmit.WSPassword = "gazt@123";
                _TaxEvasionReportTobeUsedToSubmit.WSUserName = "GAZT@CRM";
                _TaxEvasionReportTobeUsedToSubmit.TaxType = "1";
                _TaxEvasionReportTobeUsedToSubmit.ReporterName = TName;
                _TaxEvasionReportTobeUsedToSubmit.TIN = TxtTIN;
                _TaxEvasionReportTobeUsedToSubmit.ReporterEmail = TEmail;
                _TaxEvasionReportTobeUsedToSubmit.ReportDetails = TReportDetail;
                _TaxEvasionReportTobeUsedToSubmit.HavingTIN = IsTINVisible.ToString().ToLower();
                _TaxEvasionReportTobeUsedToSubmit.CompanyName = TFaciName;
                _TaxEvasionReportTobeUsedToSubmit.CompanyOwnerName = TFaciOwnerName;
                _TaxEvasionReportTobeUsedToSubmit.ReporterMobileNumber = TMobNumber;
                List<UploadedDocumentsList> newList = UploadedDocumentsListObj.ToList<UploadedDocumentsList>();
                _TaxEvasionReportTobeUsedToSubmit.CompanyMobileNumber = TFaciMobNo;
                TEReportResponsePostRootObject response = new TEReportResponsePostRootObject();
                response = await WebServiceManager.GAZTTESReportSubmit(_TaxEvasionReportTobeUsedToSubmit, newList);
                if (response != null && response.Success == true)
                { //ZTEReportReportSuccessResponsep1
                    var resmessage = AppResources.ZTEReportReportSuccessResponsep1;
                    var newrm = resmessage.Replace("Report Number", response.TaxEvasionNumber);
                    _TaxEvasionReportTobeUsedToSubmit = null;

                    _dialogService.ShowMessage(newrm, AppResources.Submitted);

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
        public async Task CreateCompanyTypeList()
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
