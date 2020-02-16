using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using Plugin.FilePicker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using GAZT.Helper;
using Newtonsoft.Json;
using System.Globalization;

namespace GAZT.ViewModel.NewViewModel
{
    public class VATReturnsPageViewModel : ViewModelBase
    {
        #region Variable

        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnStepButtonClicked { get; set; }
        public ICommand onInstructionsClicked { get; set; }
        public ICommand onTaxPayerDetailsClicked { get; set; }
        public ICommand onVATReturnFormClicked { get; set; }
        public ICommand onSummaryClicked { get; set; }
        public ICommand OnSaveAsDraftClicked { get; set; }
        public ICommand onOptionClicked { get; set; }
        public ICommand OnAttachmentClick { get; set; }
        public ICommand OnVATRefreshButtonClicked { get; set; }
        public ICommand OnDownloadAcknowlwdgementClicked { get; set; }
        public ICommand OnAcknowlwdgementClicked { get; set; }

        bool IsFirstSubmission = true;

        byte[] attachment;
        public ICommand onCreditCarriedForwardClicked { get; set; }



        public ICommand onStandardRatedSalesVatAmountTapped { get; set; }

        public ICommand OnGetAcknowledgementLinkClicked { get;set; }


        #endregion

        #region Property

        private int _firstSubmissionCount = 0;
        public int FirstSubmissionCount
        {
            get
            {
                return _firstSubmissionCount;
            }
            set
            {
                _firstSubmissionCount = value;
                RaisePropertyChanged("FirstSubmissionCount");
            }
        }


        private VATDeclaration _responseVatDeclaration;
        public VATDeclaration ResponseVatDeclaration
        {
            get
            {
                return _responseVatDeclaration;
            }
            set
            {
                _responseVatDeclaration = value;
                RaisePropertyChanged("ResponseVatDeclaration");
            }
        }


        private String _stepNumber;
        public String StepNumber
        {
            get
            {
                return _stepNumber;
            }
            set
            {
                _stepNumber = value;
                RaisePropertyChanged("StepNumber");
            }
        }

        private String _stepNumberz;
        public String StepNumberz
        {
            get
            {
                return _stepNumberz;
            }
            set
            {
                _stepNumberz = value;
                RaisePropertyChanged("StepNumberz");
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

        private bool _isDeclarationChecked = false;
        public bool IsDeclarationChecked
        {
            get
            {
                return _isDeclarationChecked;
            }
            set
            {
                _isDeclarationChecked = value;
                RaisePropertyChanged("IsDeclarationChecked");
            }
        }


        private VATDeclarationD _vATDeclarationD;
        public VATDeclarationD VATDeclarationD
        {
            get
            {
                return _vATDeclarationD;
            }
            set
            {
                _vATDeclarationD = value;
                RaisePropertyChanged("VATDeclarationD");
            }
        }

        private VATDeclaration _vATDeclarationData;
        public VATDeclaration VATDeclarationData
        {
            get
            {
                return _vATDeclarationData;
            }
            set
            {
                _vATDeclarationData = value;
                RaisePropertyChanged("VATDeclarationData");
            }
        }


        private List<Result3> _creditCarriedsList;
        public List<Result3> CreditCarriedsList
        {
            get
            {
                return _creditCarriedsList;
            }
            set
            {
                _creditCarriedsList = value;
                RaisePropertyChanged("CreditCarriedsList");
            }
        }

        private List<VATDeclarationTabbedPageName> _vatTabbledPageList;
        public List<VATDeclarationTabbedPageName> VatTabbledPageList
        {
            get
            {
                return _vatTabbledPageList;
            }
            set
            {
                _vatTabbledPageList = value;
                RaisePropertyChanged("VatTabbledPageList");
            }
        }

        private ObservableCollection<Attachment> _vatAttachmentsList;
        public ObservableCollection<Attachment> VatAttachmentsList
        {
            get
            {
                return _vatAttachmentsList;
            }
            set
            {
                _vatAttachmentsList = value;
                RaisePropertyChanged("VatAttachmentsList");
            }
        }



        private bool _isVisibleOptionMenu = false;
        public bool IsVisibleOptionMenu
        {
            get
            {
                return _isVisibleOptionMenu;
            }
            set
            {
                _isVisibleOptionMenu = value;
                RaisePropertyChanged("IsVisibleOptionMenu");
            }
        }


        private bool _isVisibleVatReturnForm = false;
        public bool IsVisibleVatReturnForm
        {
            get
            {
                return _isVisibleVatReturnForm;
            }
            set
            {
                _isVisibleVatReturnForm = value;
                RaisePropertyChanged("IsVisibleVatReturnForm");
            }
        }

        private bool _isVisibleTaxPayerDetails = false;
        public bool IsVisibleTaxPayerDetails
        {
            get
            {
                return _isVisibleTaxPayerDetails;
            }
            set
            {
                _isVisibleTaxPayerDetails = value;
                RaisePropertyChanged("IsVisibleTaxPayerDetails");
            }
        }


        private bool _isVisibleCreditCarriedForward = false;
        public bool IsVisibleCreditCarriedForward
        {
            get
            {
                return _isVisibleCreditCarriedForward;
            }
            set
            {
                _isVisibleCreditCarriedForward = value;
                RaisePropertyChanged("IsVisibleCreditCarriedForward");
            }
        }

        private bool _isVisibleSummary = false;
        public bool IsVisibleSummary
        {
            get
            {
                return _isVisibleSummary;
            }
            set
            {
                _isVisibleSummary = value;
                RaisePropertyChanged("IsVisibleSummary");
            }
        }

        private bool _isVisibleAttachments = false;
        public bool IsVisibleAttachments
        {
            get
            {
                return _isVisibleAttachments;
            }
            set
            {
                _isVisibleAttachments = value;
                RaisePropertyChanged("IsVisibleAttachments");
            }
        }


        private bool _isVisibleAcknowledgment = false;
        public bool IsVisibleAcknowledgment
        {
            get
            {
                return _isVisibleAcknowledgment;
            }
            set
            {
                _isVisibleAcknowledgment = value;
                RaisePropertyChanged("IsVisibleAcknowledgment");
            }
        }



        private bool _isCheckedTaxPayerDetailsInfo = false;
        public bool IsCheckedTaxPayerDetailsInfo
        {
            get
            {
                return _isCheckedTaxPayerDetailsInfo;
            }
            set
            {
                _isCheckedTaxPayerDetailsInfo = value;
                if (_isCheckedTaxPayerDetailsInfo == true)
                {
                    if ((App.ICRStatus == "E0045" || App.ICRStatus == "E0006") && (IsAmendClicked == false))
                    {
                        IsMainButtonEnabled = false;
                    }
                    else
                    {
                        IsMainButtonEnabled = true;
                    }
                    VATDeclarationData.d.ConfStp2 = "1";
                }
                else
                {
                    IsMainButtonEnabled = false;
                    VATDeclarationData.d.ConfStp2 = "0";
                }
                RaisePropertyChanged("IsCheckedTaxPayerDetailsInfo");
            }
        }
        

        private bool _isGetAcknowledgementClicked = false;
        public bool IsGetAcknowledgementClicked
        {
            get
            {
                return _isGetAcknowledgementClicked;
            }
            set
            {
                _isGetAcknowledgementClicked = value;
                RaisePropertyChanged("IsGetAcknowledgementClicked");
            }
        }

        private bool _isDeclarationCheckedForSummary = false;
        public bool IsDeclarationCheckedForSummary
        {
            get
            {
                return _isDeclarationCheckedForSummary;
            }
            set
            {
                _isDeclarationCheckedForSummary = value;
                if (_isDeclarationCheckedForSummary == true)
                {
                    if ((App.ICRStatus == "E0045" || App.ICRStatus == "E0006") && (IsAmendClicked == false))
                    {
                        IsMainButtonEnabled = false;
                    }
                    else
                    {
                        IsMainButtonEnabled = true;
                    }
                    VATDeclarationData.d.DecFg = "1";
                }
                else
                {
                    IsMainButtonEnabled = false;
                    VATDeclarationData.d.DecFg = "0";
                }
                RaisePropertyChanged("IsDeclarationCheckedForSummary");
            }
        }



        private bool _isDeclarationCheckedForInstruction = false;
        public bool IsDeclarationCheckedForInstruction
        {
            get
            {
                return _isDeclarationCheckedForInstruction;
            }
            set
            {
                _isDeclarationCheckedForInstruction = value;
                if (_isDeclarationCheckedForInstruction == true)
                {
                    if ((App.ICRStatus == "E0045" || App.ICRStatus == "E0006") && (IsAmendClicked == false))
                    {
                        IsMainButtonEnabled = false;
                    }
                    else
                    {
                        IsMainButtonEnabled = true;
                    }
                    VATDeclarationData.d.TcFg = "1";


                }
                else
                {
                    IsMainButtonEnabled = false;
                    VATDeclarationData.d.TcFg = "0";
                }
                RaisePropertyChanged("IsDeclarationCheckedForInstruction");
            }
        }


        private bool _isMainButtonEnabled = false;
        public bool IsMainButtonEnabled
        {
            get
            {
                return _isMainButtonEnabled;
            }
            set
            {
                _isMainButtonEnabled = value;
                RaisePropertyChanged("IsMainButtonEnabled");
            }
        }


        private bool _isFirstTimeGet = false;
        public bool IsFirstTimeGet
        {
            get
            {
                return _isFirstTimeGet;
            }
            set
            {
                _isFirstTimeGet = value;
                RaisePropertyChanged("IsFirstTimeGet");
            }
        }


        private bool _isTaxPayerControlEnabled = false;
        public bool IsTaxPayerControlEnabled
        {
            get
            {
                return _isTaxPayerControlEnabled;
            }
            set
            {
                _isTaxPayerControlEnabled = value;
                RaisePropertyChanged("IsTaxPayerControlEnabled");
            }
        }

        private bool _isVisibleNotes = false;
        public bool IsVisibleNotes
        {
            get
            {
                return _isVisibleNotes;
            }
            set
            {
                _isVisibleNotes = value;
                RaisePropertyChanged("IsVisibleNotes");
            }
        }


        private bool _isVisibleInstrunction = false;
        public bool IsVisibleInstrunction
        {
            get
            {
                return _isVisibleInstrunction;
            }
            set
            {
                _isVisibleInstrunction = value;
                RaisePropertyChanged("IsVisibleInstrunction");
            }
        }



        private bool _isTabbedMenuAvailable = true;
        public bool IsTabbedMenuAvailable
        {
            get
            {
                return _isTabbedMenuAvailable;
            }
            set
            {
                _isTabbedMenuAvailable = value;
                RaisePropertyChanged("IsTabbedMenuAvailable");
            }
        }

        private bool _isVisibleCreditCarriedLabel = false;
        public bool IsVisibleCreditCarriedLabel
        {
            get
            {
                return _isVisibleCreditCarriedLabel;
            }
            set
            {
                _isVisibleCreditCarriedLabel = value;
                RaisePropertyChanged("IsVisibleCreditCarriedLabel");
            }
        }

        private bool _isAmendClicked = false;
        public bool IsAmendClicked
        {
            get
            {
                return _isAmendClicked;
            }
            set
            {
                _isAmendClicked = value;
                RaisePropertyChanged("IsAmendClicked");
            }
        }



        private string _pageFontSize = "10";
        public string PageFontSize
        {
            get
            {
                return _pageFontSize;
            }
            set
            {
                _pageFontSize = value;

                RaisePropertyChanged("PageFontSize");
            }
        }

        private string _attachmentName = "Attachments";
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



        private string _buttonName = AppResources.ZVatStepTwo;
        public string ButtonName
        {
            get
            {
                return _buttonName;
            }
            set
            {
                _buttonName = value;

                RaisePropertyChanged("ButtonName");
            }
        }

        private string _noteText = "";
        public string NoteText
        {
            get
            {
                return _noteText;
            }
            set
            {
                _noteText = value;

                RaisePropertyChanged("NoteText");
            }
        }

        private List<Note> _responseNote;
        public List<Note> ResponseNote
        {
            get
            {
                return _responseNote;
            }
            set
            {
                _responseNote = value;

                RaisePropertyChanged("ResponseNote");
            }
        }

        private List<Result2> _responseIBANSET;
        public List<Result2> ResponseIBANSET
        {
            get
            {
                return _responseIBANSET;
            }
            set
            {
                _responseIBANSET = value;

                RaisePropertyChanged("ResponseIBANSET");
            }
        }


        private List<Result3> _responseCFSET;
        public List<Result3> ResponseCFSET
        {
            get
            {
                return _responseCFSET;
            }
            set
            {
                _responseCFSET = value;

                RaisePropertyChanged("ResponseCFSET");
            }
        }

        private List<Attachment> _responseAttachSet;
        public List<Attachment> ResponseAttachSet
        {
            get
            {
                return _responseAttachSet;
            }
            set
            {
                _responseAttachSet = value;

                RaisePropertyChanged("ResponseAttachSet");
            }
        }

        private List<Result5> _responseAddressSET;
        public List<Result5> ResponseAddressSET
        {
            get
            {
                return _responseAddressSET;
            }
            set
            {
                _responseAddressSET = value;

                RaisePropertyChanged("ResponseResult5");
            }
        }


        private List<object> _responseobject;
        public List<object> Responseobject
        {
            get
            {
                return _responseobject;
            }
            set
            {
                _responseobject = value;

                RaisePropertyChanged("Responseobject");
            }
        }

        private VATDeclarationD _responseVATDeclarationD;
        public VATDeclarationD ResponseVATDeclarationD
        {
            get
            {
                return _responseVATDeclarationD;
            }
            set
            {
                _responseVATDeclarationD = value;

                RaisePropertyChanged("ResponseVATDeclarationD");
            }
        }



        private VATDeclarationTabbedPageName _pageSelectedItems;
        public VATDeclarationTabbedPageName PageSelectedItems
        {
            get
            {
                return _pageSelectedItems;
            }
            set
            {
                _pageSelectedItems = value;

                RaisePropertyChanged("PageSelectedItems");
            }
        }

        private VATDeclarationTabbedPageName _pageSelectedItem;
        public VATDeclarationTabbedPageName PageSelectedItem
        {
            get
            {
                return _pageSelectedItem;
            }
            set
            {
                _pageSelectedItem = value;

                RaisePropertyChanged("PageSelectedItem");
            }
        }

        private string _vATRate001;
        public string VATRate001
        {
            get
            {
                return _vATRate001;
            }
            set
            {
                _vATRate001 = value;

                RaisePropertyChanged("VATRate001");
            }
        }

        private string _vATRate002;
        public string VATRate002
        {
            get
            {
                return _vATRate002;
            }
            set
            {
                _vATRate002 = value;

                RaisePropertyChanged("VATRate002");
            }
        }


        private string _tPName = "";
        public string TPName
        {
            get
            {
                return _tPName;
            }
            set
            {
                _tPName = value;

                RaisePropertyChanged("TPName");
            }
        }

        private string _returnReferenceNumber = "";
        public string ReturnReferenceNumber
        {
            get
            {
                return _returnReferenceNumber;
            }
            set
            {
                _returnReferenceNumber = value;

                RaisePropertyChanged("ReturnReferenceNumber");
            }
        }

        private string _taxablePeriod = "";
        public string TaxablePeriod
        {
            get
            {
                return _taxablePeriod;
            }
            set
            {
                _taxablePeriod = value;

                RaisePropertyChanged("TaxablePeriod");
            }
        }


        private string _receiptDate = "";
        public string ReceiptDate
        {
            get
            {
                return _receiptDate;
            }
            set
            {
                _receiptDate = value;
                RaisePropertyChanged("ReceiptDate");
            }
        }


        private string _sadadNumber = "";
        public string SadadNumber
        {
            get
            {
                return _sadadNumber;
            }
            set
            {
                _sadadNumber = value;
                RaisePropertyChanged("SadadNumber");
            }
        }


        private List<VATCalculationDataVATRSet> _calculationRateSet;
        public List<VATCalculationDataVATRSet> CalculationRateSet
        {
            get
            {
                return _calculationRateSet;
            }
            set
            {
                _calculationRateSet = value;

                RaisePropertyChanged("CalculationRateSet");
            }
        }

        private string _taxpayerPeriodFromDate;
        public string TaxpayerPeriodFromDate
        {
            get
            {
                return _taxpayerPeriodFromDate;
            }
            set
            {
                _taxpayerPeriodFromDate = value;

                RaisePropertyChanged("TaxpayerPeriodFromDate");
            }
        }


        private string _taxpayerPeriodToDate;
        public string TaxpayerPeriodToDate
        {
            get
            {
                return _taxpayerPeriodToDate;
            }
            set
            {
                _taxpayerPeriodToDate = value;

                RaisePropertyChanged("TaxpayerPeriodToDate");
            }
        }

        private bool _isControlEnabled = false;
        public bool IsControlEnabled
        {
            get
            {
                return _isControlEnabled;
            }
            set
            {
                _isControlEnabled = value;

                RaisePropertyChanged("IsControlEnabled");
            }
        }



        private List<VTTHSetResult> _calculationRateSetVTTH;
        public List<VTTHSetResult> CalculationRateSetVTTH
        {
            get
            {
                return _calculationRateSetVTTH;
            }
            set
            {
                _calculationRateSetVTTH = value;

                RaisePropertyChanged("CalculationRateSetVTTH");
            }
        }

        private string _correctionPeriodAmount;
        public string CorrectionPeriodAmount
        {
            get
            {
                return _correctionPeriodAmount;
            }
            set
            {
                _correctionPeriodAmount = value;

                RaisePropertyChanged("CorrectionPeriodAmount");
            }
        }




        #region NewProperty

        public string _totalsalesAmt;
        public string TotalsalesAmt
        {
            get
            {
                return _totalsalesAmt;
            }
            set
            {
                _totalsalesAmt = value;
                RaisePropertyChanged("TotalsalesAmt");
            }
        }

        public string _totalsalesAdj;
        public string TotalsalesAdj
        {
            get
            {
                return _totalsalesAdj;
            }
            set
            {
                _totalsalesAdj = value;
                RaisePropertyChanged("TotalsalesAdj");
            }
        }

        public string _totalpurchaseAmt;
        public string TotalpurchaseAmt
        {
            get
            {
                return _totalpurchaseAmt;
            }
            set
            {
                _totalpurchaseAmt = value;
                RaisePropertyChanged("TotalpurchaseAmt");
            }
        }

        public string _totalpurchaseAdj;
        public string TotalpurchaseAdj
        {
            get
            {
                return _totalpurchaseAdj;
            }
            set
            {
                _totalpurchaseAdj = value;
                RaisePropertyChanged("TotalpurchaseAdj");
            }
        }

        public string _stdsalesVat;
        public string StdsalesVat
        {
            get
            {
                return _stdsalesVat;
            }
            set
            {
                _stdsalesVat = value;
                RaisePropertyChanged("StdsalesVat");
            }
        }

        public string _totaldueVat;
        public string TotaldueVat
        {
            get
            {
                return _totaldueVat;
            }
            set
            {
                _totaldueVat = value;
                if (_totaldueVat != null)
                {
                    NetdueVat = NetVatDue(TotaldueVat, Preperiodcorr, CreditVat);
                }
                RaisePropertyChanged("TotaldueVat");
            }
        }

        public string _preperiodcorr;
        public string Preperiodcorr
        {
            get
            {
                return _preperiodcorr;
            }
            set
            {
                _preperiodcorr = value;
                if (_preperiodcorr != null)
                {
                    NetdueVat = NetVatDue(TotaldueVat, Preperiodcorr, CreditVat);
                }
                RaisePropertyChanged("Preperiodcorr");
            }
        }

        public string _netdueVat;
        public string NetdueVat
        {
            get
            {
                return _netdueVat;
            }
            set
            {
                _netdueVat = value;

                RaisePropertyChanged("NetdueVat");
            }
        }




        public string _creditVat;
        public string CreditVat
        {
            get
            {
                return _creditVat;
            }
            set
            {
                _creditVat = value;
                if (_creditVat != null)
                {
                    NetdueVat = NetVatDue(TotaldueVat, Preperiodcorr, CreditVat);
                }
                RaisePropertyChanged("CreditVat");
            }
        }


        public string _totalsalesVat;
        public string TotalsalesVat
        {
            get
            {
                return _totalsalesVat;
            }
            set
            {
                _totalsalesVat = value;
                if (!string.IsNullOrEmpty(_totalsalesVat))
                {
                    if (string.IsNullOrEmpty(TotalsalesVat) || (TotalsalesVat == "0"))
                    {
                        TotalsalesVat = "0.0";
                    }
                    if (string.IsNullOrEmpty(TotalpurchaseVat) || (TotalpurchaseVat == "0"))
                    {
                        TotalpurchaseVat = "0.0";
                    }
                    TotaldueVat = (Convert.ToDouble(TotalsalesVat) - Convert.ToDouble(TotalpurchaseVat)).ToString();
                }
                RaisePropertyChanged("TotalsalesVat");
            }
        }

        public string _stdpurchasesVat;
        public string StdpurchasesVat
        {
            get
            {
                return _stdpurchasesVat;
            }
            set
            {
                _stdpurchasesVat = value;
                RaisePropertyChanged("StdpurchasesVat");
            }
        }

        public string _importspaidVat;
        public string ImportspaidVat
        {
            get
            {
                return _importspaidVat;
            }
            set
            {
                _importspaidVat = value;
                RaisePropertyChanged("ImportspaidVat");
            }
        }

        public string _totalpurchaseVat;
        public string TotalpurchaseVat
        {
            get
            {
                return _totalpurchaseVat;
            }
            set
            {
                _totalpurchaseVat = value;
                if (!string.IsNullOrEmpty(_totalpurchaseVat))
                {
                    TotaldueVat = (Convert.ToDouble(TotalsalesVat) - Convert.ToDouble(TotalpurchaseVat)).ToString();

                }
                RaisePropertyChanged("TotalpurchaseVat");
            }
        }


        private string _amountPayable = "2470";
        public string AmountPayable
        {
            get
            {
                return _amountPayable;
            }
            set
            {
                _amountPayable = value;
                RaisePropertyChanged("AmountPayable");
            }
        }
        public string _importsaccVat;
        public string ImportsaccVat
        {
            get
            {
                return _importsaccVat;
            }
            set
            {
                _importsaccVat = value;
                RaisePropertyChanged("ImportsaccVat");
            }
        }
        #endregion

        private bool _isSadadNumberVisible = false;
        public bool IsSadadNumberVisible
        {
            get
            {
                return _isSadadNumberVisible;
            }
            set
            {
                _isSadadNumberVisible = value;
                RaisePropertyChanged("IsSadadNumberVisible");
            }
        }


        private string _fullAddress = string.Empty;
        public string FullAddress
        {
            get
            {
                return _fullAddress;
            }
            set
            {
                _fullAddress = value;
                RaisePropertyChanged("FullAddress");
            }
        }

        private bool _iSSwichButtonEnable = false;
        public bool IsSwichButtonEnable
        {
            get
            {
                return _iSSwichButtonEnable;
            }
            set
            {
                _iSSwichButtonEnable = value;
                if(_iSSwichButtonEnable==true)
                {
                    IsVisibleDropdownForRefund = true;
                    IsDropdownVisibleForIban = true;
                }
                else
                {
                    IsVisibleDropdownForRefund = false;
                    IsDropdownVisibleForIban = false;
                }
                RaisePropertyChanged("IsSwichButtonEnable");
            }
        }

        private Result2 _selectedIBAN;
        public Result2 SelectedIBAN
        {
            get
            {
                return _selectedIBAN;
            }
            set
            {
                _selectedIBAN = value;
               
                RaisePropertyChanged("SelectedIBAN");
            }
        }

        private List<Result2> _iBANList;
        public List<Result2> IBANList
        {
            get
            {
                return _iBANList;
            }
            set
            {
                _iBANList = value;
               if(_iBANList!=null && _iBANList.Count!=0)
                {
                    if(App.ICRStatus =="E0045" || App.ICRStatus == "E0006")
                    {
                        IsEnableIBAN = false;
                    }
                    else
                    {
                        IsEnableIBAN = true;
                    }
                }
               else
                {
                    IsEnableIBAN = false;
                }
                RaisePropertyChanged("IBANList");
            }
        }

        

        private bool _isEnableIBAN;
        public bool IsEnableIBAN
        {
            get
            {
                return _isEnableIBAN;
            }
            set
            {
                _isEnableIBAN = value;

                RaisePropertyChanged("IsEnableIBAN");
            }
        }


        private bool _isEnableIBANType;
        public bool IsEnableIBANType
        {
            get
            {
                return _isEnableIBANType;
            }
            set
            {
                _isEnableIBANType = value;
                RaisePropertyChanged("IsEnableIBANType");
            }
        }


        private bool _isVATRefunCheckedVisible;
        public bool IsVATRefunCheckedVisible
        {
            get
            {
                return _isVATRefunCheckedVisible;
            }
            set
            {
                _isVATRefunCheckedVisible = value;

                RaisePropertyChanged("IsVATRefunCheckedVisible");
            }
        }

        private string _ibanNumberText;
        public string IbanNumberText
        {
            get
            {
                return _ibanNumberText;
            }
            set
            {
                _ibanNumberText = value;

                RaisePropertyChanged("IbanNumberText");
            }
        }

        private bool _isIBANValid;
        public bool IsIBANValid
        {
            get
            {
                return _isIBANValid;
            }
            set
            {
                _isIBANValid = value;

                RaisePropertyChanged("IsIBANValid");
            }
        }

        private bool _isCheckedRefund;
        public bool IsCheckedRefund
        {
            get
            {
                return _isCheckedRefund;
            }
            set
            {
                _isCheckedRefund = value;
                if(_isCheckedRefund==true)
                {
                    IsTextBoxVisibleForIban = true;
                    IsDropdownVisibleForIban = false;
                }
                else
                {
                    IsDropdownVisibleForIban = true;
                    IsTextBoxVisibleForIban = false;
                }
                RaisePropertyChanged("IsCheckedRefund");
            }
        }

        private bool _isVisibleDropdownForRefund;
        public bool IsVisibleDropdownForRefund
        {
            get
            {
                return _isVisibleDropdownForRefund;
            }
            set
            {
                _isVisibleDropdownForRefund = value;

                RaisePropertyChanged("IsVisibleDropdownForRefund");
            }
        }

        private bool _isTextBoxVisibleForIban;
        public bool IsTextBoxVisibleForIban
        {
            get
            {
                return _isTextBoxVisibleForIban;
            }
            set
            {
                _isTextBoxVisibleForIban = value;

                RaisePropertyChanged("IsTextBoxVisibleForIban");
            }
        }

        private bool _isDropdownVisibleForIban;
        public bool IsDropdownVisibleForIban
        {
            get
            {
                return _isDropdownVisibleForIban;
            }
            set
            {
                _isDropdownVisibleForIban = value;

                RaisePropertyChanged("IsDropdownVisibleForIban");
            }
        }

        private bool _isEnableIBANIdNumber;
        public bool IsEnableIBANIdNumber
        {
            get
            {
                return _isEnableIBANIdNumber;
            }
            set
            {
                _isEnableIBANIdNumber = value;

                RaisePropertyChanged("IsEnableIBANIdNumber");
            }
        }


        private List<IBANType> _iBANTypesList;
        public List<IBANType> IBANTypesList
        {
            get
            {
                return _iBANTypesList;
            }
            set
            {
                _iBANTypesList = value;
                if(_iBANTypesList !=null && _iBANTypesList.Count!=0)
                {
                    if(App.ICRStatus=="E0045" || App.ICRStatus=="E0006")
                    {
                        IsEnableIBANType = false;
                    }
                    else
                    {
                        IsEnableIBANType = true;
                    }
                }
                else
                {
                    IsEnableIBANType = false;
                }
                RaisePropertyChanged("IBANTypesList");
            }
        }
        private IBANType _selectedIBANType;
        public IBANType SelectedIBANType
        {
            get
            {
                return _selectedIBANType;
            }
            set
            {
                _selectedIBANType = value;
                if(_selectedIBANType!=null)
                {
                    SetIBANIdNumber();
                    
                }
                else
                {
                    
                }
                RaisePropertyChanged("SelectedIBANType");
            }
        }

        private List<IBANIDNumber> _iBANIDNumberList;
        public List<IBANIDNumber> IBANIDNumberList
        {
            get
            {
                return _iBANIDNumberList;
            }
            set
            {
                _iBANIDNumberList = value;
                if(_iBANIDNumberList!= null && _iBANIDNumberList.Count()!=0)
                {
                    if (App.ICRStatus == "E0045" || App.ICRStatus == "E0006")
                    {
                        IsEnableIBANIdNumber = false;
                    }
                    else
                    {
                        IsEnableIBANIdNumber = true;
                    }
                }
                else
                {
                    IsEnableIBANIdNumber = false;
                }
                RaisePropertyChanged("IBANIDNumberList");
            }
        }


        private IBANIDNumber _selectedIBANIDNumber;
        public IBANIDNumber SelectedIBANIDNumber
        {
            get
            {
                return _selectedIBANIDNumber;
            }
            set
            {
                _selectedIBANIDNumber = value;

                RaisePropertyChanged("SelectedIBANIDNumber");
            }
        }

        private bool _isRefundVisible=false;
        public bool IsRefundVisible
        {
            get
            {
                return _isRefundVisible;
            }
            set
            {
                _isRefundVisible = value;

                RaisePropertyChanged("IsRefundVisible");
            }
        }

        private bool _isSwichButtonEnableToTap = true;
        public bool IsSwichButtonEnableToTap
        {
            get
            {
                return _isSwichButtonEnableToTap;
            }
            set
            {
                _isSwichButtonEnableToTap = value;

                RaisePropertyChanged("IsSwichButtonEnableToTap");
            }
        }












        #endregion

        #region Constructor

        public VATReturnsPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {

            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }


            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            _navigationService = navigationService;
            _dialogService = dialogService;

            IsMainButtonEnabled = false;
            
            ManageEnabledProperty(true);

            OnStepButtonClicked = new Xamarin.Forms.Command(async () =>
            {
                if (!string.IsNullOrEmpty(ButtonName))
                {
                    if (ButtonName == AppResources.ZVatStepTwo)
                    {
                        TaxpayerDetailsClicked();
                        PageSelectedItem = VatTabbledPageList[1];
                        //  VATTabbedPageReturnCollectionView.SelectedItems.Add((this.VATTabbedPageReturnCollectionView.ItemsSource as List<VATDeclarationTabbedPageName>)[0]);
                      //  _dialogService.ShowMessage(AppResources.ZZGeneralMessageformVoidedBeforeChangeOfRegistrationForm, AppResources.Information);
                    }
                    else if (ButtonName == AppResources.ZVatStepThree)
                    {
                        VATReturnFormClicked();
                        PageSelectedItem = VatTabbledPageList[2];
                    }
                    else if (ButtonName == AppResources.ZVatStepFour)
                    {
                        SummaryClicked();
                        PageSelectedItem = VatTabbledPageList[3];
                    }
                    else if (ButtonName == AppResources.Submit)
                    {
                        SubmitClicked();
                    }
                    //else if(ButtonName == AppResources.ZVatDownloadForm)
                    //{
                    //    String Url = string.Empty;
                    //   // Url = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/Z_GET_ACK_LETTER_SRV/Ack_letterSet(Fbnum=%2765000178937%27)/$value?saml2=disabled";
                    //     Url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_COVERFORM_SRV/cover_formSet(Fbnum='" + VATDeclarationData.d.Fbnum + "',Utype='')/$value?saml2=disabled";
                    //   ShowPdf(Url);
                    //}
                    else if (ButtonName == AppResources.ZNote)
                    {
                        SetNoteData();
                    }
                    else if (ButtonName == "Go to ICR List")
                    {
                        _navigationService.GoBack();
                        _navigationService.NavigateTo(App.ICRListPageView);
                    }
                }
            });

            onStandardRatedSalesVatAmountTapped = new Xamarin.Forms.Command(() =>
            {
                // InstrunctionClicked();
                ResponseVATDeclarationD.StdsalesVat = StandardRatedSalesVatAmount(ResponseVATDeclarationD.StdsalesAmt, ResponseVATDeclarationD.StdsalesAdj);

            });


             OnGetAcknowledgementLinkClicked = new Xamarin.Forms.Command(async () =>
             {
                 _navigationService.NavigateTo(App.AcknowledgementDetailsPageView, VATDeclarationData);

             });

            onInstructionsClicked = new Xamarin.Forms.Command(async () =>
            {
                InstrunctionClicked();

            });

            onTaxPayerDetailsClicked = new Xamarin.Forms.Command(async () =>
            {
                TaxpayerDetailsClicked();

            });

            onVATReturnFormClicked = new Xamarin.Forms.Command(async () =>
            {
                VATReturnFormClicked();

            });

            OnAcknowlwdgementClicked = new Xamarin.Forms.Command(async () =>
            {
                string url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_ACK_LETTER_SRV/Ack_letterSet(Fbnum='" + VATDeclarationData + "')/$value?saml2=disabled";
                _navigationService.NavigateTo(App.AAcknowledgementView, url);
            });

            OnDownloadAcknowlwdgementClicked = new Xamarin.Forms.Command(async () =>
            {
                string url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_COVERFORM_SRV/cover_formSet(Fbnum='" + VATDeclarationData + "',Utype='')/$value?saml2=disabled";
                _navigationService.NavigateTo(App.AAcknowledgementView, url);
            });

            OnVATRefreshButtonClicked = new Xamarin.Forms.Command(async () =>
            {
                try
                {
                    var response = await WebServiceManager.GAZTGetVATDeclarationSADADNumber(VATDeclarationData.d.Fbnum);
                    PopToRootPage();
                    SadadNumber = response.d.results[0].Vtref;
                    AmountPayable = response.d.results[0].Betrh;
                    if (!string.IsNullOrEmpty(SadadNumber))
                    {
                        IsSadadNumberVisible = true;
                    }
                }
                catch (InternetException ex)
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                }
                // Call Sadad number API
            });

            OnAttachmentClick = new Xamarin.Forms.Command(async () =>
            {
                try
                {
                    var fileData = await CrossFilePicker.Current.PickFile();
                    attachment = fileData.DataArray;
                    AttachmentName = fileData.FileName;

                    AttachmentRootOject _attachment = await WebServiceManager.GAZTSaveVATDeclarationAttachment(attachment, AttachmentName, VATDeclarationData.d.ReturnIdz, "VTA0");
                    PopToRootPage();
                    if (_attachment != null && _attachment.d != null)
                    {
                        VATDeclarationData.d.ATTACHSet.results.Add(_attachment.d);
                        ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(VATDeclarationData.d.ATTACHSet.results as List<Attachment>);
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            VatAttachmentsList = myCollection;
                        });
                        VatAttachmentsList = myCollection;
                    }

                }
                catch (InternetException ex)
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                }


            });

            onSummaryClicked = new Xamarin.Forms.Command(async () =>
            {
                SummaryClicked();

            });

            onCreditCarriedForwardClicked = new Xamarin.Forms.Command(async () =>
            {
                _navigationService.NavigateTo(App.CreditCarriedPageView,VATDeclarationData);
             //   CreditCarriedClicked();
            });

            onOptionClicked = new Xamarin.Forms.Command(async () =>
            {
                if (IsVisibleOptionMenu == true)
                {
                    IsVisibleOptionMenu = false;
                }
                else
                {
                    IsVisibleOptionMenu = true;
                }

            });

            //OnCopySadadNumberButtonClicked = new Xamarin.Forms.Command(async () =>
            //{
            //    await _dialogService.ShowMessage("It has copied sadad payment number", AppResources.Information);
            //});

            OnSaveAsDraftClicked = new Command(async () =>
            {
                CreateDataForPost();

                string operation = "05";// Passed 05 to save the data as a draft
                VATDeclarationData.d.Operationz = operation;
                StepNumber = "01";
                if (IsDeclarationCheckedForInstruction == true)
                {
                    StepNumber = "02";
                }
                if (IsCheckedTaxPayerDetailsInfo == true)
                {
                    StepNumber = "03";
                }
                if (IsDeclarationCheckedForSummary == true)
                {
                    StepNumber = "04";
                }

                VATDeclarationData.d.StepNumber = StepNumber;
                VATDeclarationData.d.UserTypz = "TP";

                await SaveReturnAndGetReturnAndSetButtons();
                await _dialogService.ShowMessage(AppResources.DraftSaved, AppResources.Information);
            });
        }

        #endregion

        #region Method

        

        public  void SetIBANIdNumber()
        {
           
            try
            {
                List<IBANIDNumber> iBANIDNumbersResponse = WebServiceManager.GAZTGetIBANIdNumber(SelectedIBANType.key);
                PopToRootPage();
                if(iBANIDNumbersResponse!=null || iBANIDNumbersResponse.Count()!=0)
                {
                    IBANIDNumberList = new List<IBANIDNumber>();
                    IBANIDNumberList = iBANIDNumbersResponse;
                }
            }
            catch (InternetException ex)
            {
               _dialogService.ShowMessage(ex.Message, AppResources.Information);
            }
        }
        public void ManageEnabledProperty(bool value)
        {
            IsControlEnabled = value;
            IsMainButtonEnabled = value;
        }

        public async Task OnSaveDraftClicked()
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });
            await Task.Run(async () =>
            {
                CreateDataForPost();

                string operation = "05";// Passed 05 to save the data as a draft
                VATDeclarationData.d.Operationz = operation;
                StepNumber = "00";
                StepNumberz = "1";
                if (IsDeclarationCheckedForInstruction == true)
                {
                    StepNumberz = "2";
                }
                if (IsCheckedTaxPayerDetailsInfo == true)
                {
                    StepNumberz = "3";
                }
                if (IsDeclarationCheckedForSummary == true)
                {
                    StepNumberz = "4";
                }

                VATDeclarationData.d.StepNumber = StepNumber;
                VATDeclarationData.d.StepNumberz = StepNumberz;
                VATDeclarationData.d.UserTypz = "TP";

                await SaveReturnAndGetReturnAndSetButtons();
                Device.BeginInvokeOnMainThread(async () => {
                    await _dialogService.ShowMessage(AppResources.DraftSaved, AppResources.Information);
                });


            });
            await Task.Run(() =>
            {
                IsLoading = false;
            });

        }



        public void InstrunctionClicked()
        {
            ClearPage();
            IsVisibleInstrunction = true;
            IsMainButtonEnabled = false;
            ButtonName = AppResources.ZVatStepTwo;
            if (App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057")
            {
                    //If Required
            }
            else
            {
                if (App.ICRStatus == "E0045")
                {
                    IsDeclarationCheckedForInstruction = true;
                }
            }
        }
        public void TaxpayerDetailsClicked()
        {
            ClearPage();
            IsVisibleTaxPayerDetails = true;
            ButtonName = AppResources.ZVatStepThree;
            if (App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057")
            {
                if (IsCheckedTaxPayerDetailsInfo == true)
                {
                    IsMainButtonEnabled = true;
                }
                else
                {
                    IsMainButtonEnabled = false;
                }
            }
            else
            {
              
               // IsCheckedTaxPayerDetailsInfo = false;
               // IsMainButtonEnabled = false;
            }
        }
        public void VATReturnFormClicked()
        {
            ClearPage();
            IsVisibleVatReturnForm = true;
            ButtonName = AppResources.ZVatStepFour;
        }
        public void SummaryClicked()
        {
            bool value = false;
            ClearPage();
            if(!string.IsNullOrEmpty(TotalpurchaseVat)&& !string.IsNullOrEmpty(TotalsalesVat))
            {
              
                value = IsCheckedDraftMode();
                if (Convert.ToDouble(TotalpurchaseVat) > Convert.ToDouble(TotalsalesVat))
                {
                    IsRefundVisible = true;
                    if (value || App.ICRStatus == "E0045" || App.ICRStatus == "E0006")
                    {
                        if (VATDeclarationData.d.RefundFg == "1")
                        {
                            IsSwichButtonEnableToTap = true;
                            IsSwichButtonEnable = true;
                            IsVisibleDropdownForRefund = true;
                          //  IsDropdownVisibleForIban = true;
                            if (VATDeclarationData.d.IbanCb == "1")
                            {
                                IsTextBoxVisibleForIban = true;
                                IsDropdownVisibleForIban = false;
                                if (!string.IsNullOrEmpty(VATDeclarationData.d.Iban))
                                {
                                    IbanNumberText = VATDeclarationData.d.Iban;
                                }
                            }
                            else
                            {
                                IsTextBoxVisibleForIban = false;
                                IsDropdownVisibleForIban = true;

                                if (!string.IsNullOrEmpty(VATDeclarationData.d.Iban))
                                {
                                    SelectedIBAN = IBANList.Where(x => x.Iban == VATDeclarationData.d.Iban).FirstOrDefault();
                                }
                            }
                            if (!string.IsNullOrEmpty(VATDeclarationData.d.Idtype))
                            {
                                SelectedIBANType = IBANTypesList.Where(x => x.key == VATDeclarationData.d.Idtype).FirstOrDefault();
                                SetIBANIdNumber();
                            }
                            if (!string.IsNullOrEmpty(VATDeclarationData.d.Idnum))
                            {
                                if (IBANIDNumberList != null && IBANIDNumberList.Count != 0)
                                {
                                    SelectedIBANIDNumber = IBANIDNumberList.Where(x => x.Idnumber == VATDeclarationData.d.Idnum).FirstOrDefault();
                                }
                            }
                        }
                        else
                        {
                            IsSwichButtonEnableToTap = true;
                            IsSwichButtonEnable = false;
                            IsDropdownVisibleForIban = false;
                            IsVisibleDropdownForRefund = false;
                        }
                    }
                }
                else
                {

                    IsRefundVisible = false;
                    IsTextBoxVisibleForIban = false;
                    IsDropdownVisibleForIban = false;
                    IsVisibleDropdownForRefund = false;
                }
            }
            
            IsVisibleSummary = true;
            if(App.ICRStatus=="E0045" && IsAmendClicked==false)
            {
               
                IsGetAcknowledgementClicked = true;
                ButtonName = AppResources.Submit;
                IsDeclarationCheckedForSummary = true;
                IsMainButtonEnabled = false;
            }
            else
            {
                IsMainButtonEnabled = false;
                ButtonName = AppResources.Submit;
                IsDeclarationCheckedForSummary = false;
            }
          
            //if (App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057")
            //{
            //    IsDeclarationCheckedForSummary = true;
            //}
            //else
            //{
            //    IsDeclarationCheckedForSummary = false;
            //}
        }
        public void CreditCarriedClicked()
        {
            ClearPage();
            IsTabbedMenuAvailable = false;
            IsVisibleCreditCarriedLabel = true;
            IsVisibleCreditCarriedForward = true;
            ButtonName = AppResources.Submit;
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
        public bool IsTabbedValid(string value)
        {
            bool bvalue = false;
            if(VATDeclarationData.d.StepNumber==value || value=="0"+VATDeclarationData.d.StepNumber)
            {
                bvalue = true;
            }
            return bvalue;
        }
        public bool IsCheckedDraftMode()
        {
            bool value=false;
            if(App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057")
            {
                value= true;
            }
            return value;
        }
        public async Task SubmitClicked()
        {
            try
            {

                //await Task.Run(() =>
                //{
                //    IsLoading = true;
                //});
                //await Task.Run(async() =>
                //{
                //IsLoading = true;
                if (FirstSubmissionCount != 1)
            {
                CreateDataForPost();
            }

            //IsVisibleAcknowledgment = true;
            ButtonName = AppResources.Submit;
            if (!IsFirstSubmission)
            {
                FirstSubmissionCount = 0;
                //ClearPage();
                //IsVisibleAcknowledgment = true;

                string operation = "01";// Passed operation "01" to submit the VAT Declaration Data
                                        //   VATDeclarationData.d.StepNumberz = "04";

                //VATDeclarationData.d.StepNumber = "00";
                VATDeclarationData.d.StepNumberz = "04";
                VATDeclarationData.d.UserTypz = "TP";
                VATDeclarationData.d.Operationz = operation;
                IsLoading = false;
                await SaveReturnAndGetReturnAndSetButtons();
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(string.Format(AppResources.ZZGeneralMessage_VATReturnFormSubmittedSuccessfullyAndFormBundleNumber, VATDeclarationData.d.Fbnum), AppResources.Information);
                });
                ManageEnabledProperty(false);
                _navigationService.NavigateTo(App.AcknowledgementDetailsPageView, VATDeclarationData);
            }
            else
            {
                IsFirstSubmission = false;

                
                if (String.IsNullOrEmpty(VATDeclarationData.d.Fbnum))
                {
                    CreateDataForPost();
                    FirstSubmissionCount = 1;
                    string operation = "05";
                    VATDeclarationData.d.StepNumber = "04";
                    VATDeclarationData.d.UserTypz = "TP";
                    VATDeclarationData.d.Operationz = operation;
                    VATDeclaration response = new VATDeclaration();
                    //response = WebServiceManager.SaveVATDeclarationData(VATDeclarationData);
                    //PopToRootPage();
                    IsLoading = false;
                    await SaveReturnAndGetReturnAndSetButtons();
                }

            await    _dialogService.ShowMessage(AppResources.Pleasereviewthecalculationandsubmitagain, AppResources.Information);
            }
                // });
                //await Task.Run(() =>
                //{
                //    IsLoading = false;
                //});
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            }
        }

        public async void ShowPdf(string pdfUrl)
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                if (pdfUrl != null)
                {
                    //Uri uri = new Uri(pdfUrl);
                    //Device.OpenUri(uri);
                    _navigationService.NavigateTo(App.PdfiOSView, pdfUrl);
                }
                else
                {
                    //pop that certificate is not available
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
                    });
                }
            }
            else
            {
                if (pdfUrl != null)
                {
                    _navigationService.NavigateTo(App.PdfView, pdfUrl);
                }
                else
                {
                    //pop that certificate is not available
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
                    });
                }
            }
        }
        public void VATReturnAddNote()
        {
            //  WebServiceManager.GAZTSetVATReturnAddNote(String.Empty);

            _navigationService.NavigateTo(App.AddNotePageView, VATDeclarationData);

        }
        public void VATReturnGetNotes()
        {

            //IsVisibleNotes = true;
            //ButtonName = AppResources.ZNote;

            //  WebServiceManager.GAZTSetVATReturnGetNotes(String.Empty);
            _navigationService.NavigateTo(App.DisplayNotesPageView, VATDeclarationData);

        }
        public void VATViewAttachments()
        {
            // ClearPage();
            // IsVisibleAttachments = true;
            //ButtonName = AppResources.Submit;
            _navigationService.NavigateTo(App.AttachmentPageView, VATDeclarationData);
        }

        public async Task VATSetReturnVoidAsync()
        {

            var answer = await Application.Current.MainPage.DisplayAlert(AppResources.Information, AppResources.ZZGeneralMessage_AllInfoFilledInTheFormWillBeLost, AppResources.ZYes, AppResources.ZNo);
            if (answer)
            {
                await Task.Run(() =>
            {
                IsLoading = true;
            });

                await Task.Run(async () =>
                {
                try
                {
                    string operation = "04";// Passed 04 to set void
                    VATDeclarationData.d.Operationz = operation;
                    StepNumber = "01";

                    if (IsDeclarationCheckedForInstruction == true)
                    {
                        StepNumber = "02";
                    }
                    if (IsCheckedTaxPayerDetailsInfo == true)
                    {
                        StepNumber = "03";
                    }
                    if (IsDeclarationCheckedForSummary == true)
                    {
                        StepNumber = "04";
                    }

                    VATDeclarationData.d.StepNumber = StepNumber;
                    VATDeclarationData.d.UserTypz = "TP";

                    var response = WebServiceManager.GAZTSetVATReturnVoid(VATDeclarationData);
                    PopToRootPage();
                    await SaveReturnAndGetReturnAndSetButtons();
                    ManageEnabledProperty(false);
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_VATReturnFormCancelled, AppResources.ZInstructions);

                    });
                    }
                    catch (InternetException ex)
                    {
                        await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
        }
        public async Task VATReturnResetAsync()
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });
            await Task.Run(async () =>
            {
            try
            {
                string operation = "14";// Passed 14 to set RESET
                VATDeclarationData.d.Operationz = operation;
                StepNumber = "01";

                if (IsDeclarationCheckedForInstruction == true)
                {
                    StepNumber = "02";
                }
                if (IsCheckedTaxPayerDetailsInfo == true)
                {
                    StepNumber = "03";
                }
                if (IsDeclarationCheckedForSummary == true)
                {
                    StepNumber = "04";
                }

                VATDeclarationData.d.StepNumber = StepNumber;
                VATDeclarationData.d.UserTypz = "TP";

                var response = WebServiceManager.GAZTSetVATReturnReset(VATDeclarationData);
                PopToRootPage();
                await SaveReturnAndGetReturnAndSetButtons();
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(AppResources.ZZGeneralMessage_ReturnRestoredToTheLastBilledVersion, AppResources.Information);
                });
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    });
                }
            });
            await Task.Run(() =>
            {
                IsLoading = false;
            });

        }
        public async Task VATReturnAmendAsync()
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });

            await Task.Run(async () =>
            {
              
                string periodto = JsonConvert.DeserializeObject<DateTime>(@"""" + VATDeclarationData.d.Abrzo + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
              
                TimeSpan TS = DateTime.Now - Convert.ToDateTime(periodto);
                double Years = TS.TotalDays / 365.25;
                if (Years >= 5)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                       await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_IfTimePeriodOfAmendmentIsLapsed, AppResources.Information);
                    });
                    return;
                }

                string operation = "45";// Passed 45 to set for Amendment
                VATDeclarationData.d.Operationz = operation;
                StepNumber = "01";
               // ButtonName = AppResources.ZVatStepTwo;
                if (IsDeclarationCheckedForInstruction == true)
                {
                    StepNumber = "02";
                   // ButtonName = AppResources.ZVatStepThree;
                }
                if (IsCheckedTaxPayerDetailsInfo == true)
                {
                    StepNumber = "03";
                   // ButtonName = AppResources.ZVatStepFour;
                }
                if (IsDeclarationCheckedForSummary == true)
                {
                    StepNumber = "04";
                //    ButtonName = AppResources.Submit;
                }

                VATDeclarationData.d.StepNumber = StepNumber;
                VATDeclarationData.d.UserTypz = "TP";
                  
               // var response = WebServiceManager.GAZTSetVATReturnAmend(VATDeclarationData);
                PopToRootPage();
                await SaveReturnAndGetReturnAndSetButtons();
                ManageEnabledProperty(true);
                IsAmendClicked = true;
                IsMainButtonEnabled = true;
            });
            await Task.Run(() =>
            {
                IsLoading = false;
            });
        }
        public void VATReturnDeleteAttachment()
        {
            _navigationService.NavigateTo("ICRListPageView");
        }
        public void ClearPage()
        {
            //for Header
            IsTabbedMenuAvailable = true;
            IsVisibleCreditCarriedLabel = false;
            ///
            IsVisibleAcknowledgment = false;
            IsVisibleAttachments = false;
            IsVisibleInstrunction = false;
            IsVisibleNotes = false;
            IsVisibleSummary = false;
            IsVisibleTaxPayerDetails = false;
            IsVisibleVatReturnForm = false;
            IsVisibleCreditCarriedForward = false;
        }
        public void RateSetAsPerDate()
        {
            try

            {
                if (VATDeclarationData.d != null)
                {
                    if (VATDeclarationData.d.Abrzu != null && VATDeclarationData.d.Abrzo != null)
                    {


                        if(VATDeclarationData.d.IBANSet.results!=null && VATDeclarationData.d.IBANSet.results.Count()!=0)
                        {
                            IBANList = new List<Result2>();
                            IBANList = VATDeclarationData.d.IBANSet.results;
                            IsVATRefunCheckedVisible = false;
                        }
                        else
                        {
                            IsVATRefunCheckedVisible = true;
                        }
                        createIBANType();

                        //DateTime startDate = new DateTime(2017, 1, 18);

                        //DateTime endDate = new DateTime(2018, 1, 18);

                        DateTime startDate = new DateTime();

                        DateTime endDate = new DateTime();


                        if (!string.IsNullOrEmpty(VATDeclarationData.d.Abrzu) && !string.IsNullOrEmpty(VATDeclarationData.d.Abrzo))
                        {
                            VATRateDataWithDateType vATRateDataWithDate;
                            VATRateDataWithStringDateType dataWithStringDateType = new VATRateDataWithStringDateType();
                            dataWithStringDateType.StartDate = VATDeclarationData.d.Abrzu;
                            dataWithStringDateType.EndDate = VATDeclarationData.d.Abrzo;
                            string JsonString = JsonConvert.SerializeObject(dataWithStringDateType);
                            vATRateDataWithDate = JsonConvert.DeserializeObject<VATRateDataWithDateType>(JsonString);
                            startDate = vATRateDataWithDate.StartDate;
                            endDate = vATRateDataWithDate.EndDate;
                        }


                        //DateTime startDate = DateTime.Parse(VATDeclarationData.d.Abrzu);
                        //DateTime endDate = DateTime.Parse(VATDeclarationData.d.Abrzo);

                        List<VATCalculationDataVATRSet> vATCalculationsforBegin = new List<VATCalculationDataVATRSet>();
                        List<VATCalculationDataVATRSet> vATCalculationsforEnd = new List<VATCalculationDataVATRSet>();
                        VATCalculationDataVATRSet vATCalculationDataDummy;
                        foreach (var item in CalculationRateSet)
                        {
                            if (startDate >= item.Begda)
                            {
                                vATCalculationDataDummy = new VATCalculationDataVATRSet();
                                vATCalculationDataDummy = item;
                                vATCalculationsforBegin.Add(vATCalculationDataDummy);
                            }
                        }

                        foreach (var item1 in vATCalculationsforBegin)
                        {
                            if (endDate <= item1.Endda)
                            {
                                vATCalculationDataDummy = new VATCalculationDataVATRSet();
                                vATCalculationDataDummy = item1;
                                vATCalculationsforEnd.Add(vATCalculationDataDummy);
                            }
                        }

                        VATCalculationDataVATRSet Rate002 = vATCalculationsforEnd.Where(x => x.Type == "002").FirstOrDefault();
                        if (Rate002 != null)
                        {
                            VATRate002 = Rate002.Penalty;
                        }

                        VATCalculationDataVATRSet Rate001 = vATCalculationsforEnd.Where(x => x.Type == "001").FirstOrDefault();
                        if (Rate001 != null)
                        {
                            VATRate001 = Rate001.Penalty;
                        }


                        //VATCalculationDataVATRSet Rate00T1 = CalculationRateSet.Where(x => x.Begda <= startDate && x.Endda >= endDate).FirstOrDefault();



                        //VATCalculationDataVATRSet Rate002 = CalculationRateSet.Where(x => (x.Begda.Date >= startDate.Date) && (x.Endda.Date <= endDate.Date) && (x.Type== "002")).FirstOrDefault();
                        //if (Rate002 != null)
                        //{
                        //    VATRate002 = Rate002.Penalty;
                        //}

                        //VATCalculationDataVATRSet Rate001 = CalculationRateSet.Where(x => (x.Begda.Date >= startDate.Date) && (x.Endda.Date <= endDate.Date) && (x.Type == "001")).FirstOrDefault();
                        //if (Rate001 != null)
                        //{
                        //    VATRate001 = Rate001.Penalty;
                        //}
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public void createIBANType()
        {
            IBANTypesList = new List<IBANType>();

            List<IBANType>  IBANTypesDummyList = new List<IBANType>();
            IBANType iBANType = new IBANType();
            iBANType.key = "ZS0001";
            iBANType.Text = "National ID/ Iqama ID";
            IBANTypesDummyList.Add(iBANType);
            IBANType iBANType1 = new IBANType();
            iBANType1.key = "BUP002";
            iBANType1.Text = "Commercial Registration ID";
            IBANTypesDummyList.Add(iBANType1);
            IBANType iBANType2 = new IBANType();
            iBANType2.key = "ZS0005";
            iBANType2.Text = "Company ID";
            IBANTypesDummyList.Add(iBANType2);
            IBANTypesList = IBANTypesDummyList;


        }

        public async Task pageLoad()
        {
            try
            {
                IsFirstSubmission = true;
            IsSadadNumberVisible = false;
            IsAmendClicked = false;
            //await Task.Run(() =>
            //{
            //    IsLoading = true;
            //});

            //await Task.Run(async () =>
            //{

            VATCalculationData vATCalculationData;
            VTTHSetResult vTTHSetResult;
            string periodKey = VATDeclarationData.d.Periodkeyz;
            string TxnTp = VATDeclarationData.d.TxnTpz;
            string status = VATDeclarationData.d.Statusz;

            if(status =="E057" || status == "E0057" || status == "E058"|| status == "E0058")
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                   await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_ReturnUnderReviewWithGAZT, AppResources.Information);
                });
            }

            

            string FormBundleNumber = VATDeclarationData.d.Fbnum;
            string Gpart = VATDeclarationData.d.Gpart;
            string periodfrom;
            string periodto;

            if (App.IsArabic)
            {

                periodfrom = JsonConvert.DeserializeObject<DateTime>(@"""" + VATDeclarationData.d.Abrzu + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                periodto = JsonConvert.DeserializeObject<DateTime>(@"""" + VATDeclarationData.d.Abrzo + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));



                TaxpayerPeriodFromDate = UtilityManager.ToArabicDate(periodfrom);
                TaxpayerPeriodToDate = UtilityManager.ToArabicDate(periodto);
            }
            else
            {
                TaxpayerPeriodFromDate = JsonConvert.DeserializeObject<DateTime>(@"""" + VATDeclarationData.d.Abrzu + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                TaxpayerPeriodToDate = JsonConvert.DeserializeObject<DateTime>(@"""" + VATDeclarationData.d.Abrzo + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));

            }

            if(VATDeclarationData.d.CFSet.results!=null && VATDeclarationData.d.ADRSet.results.Count!=0)
            {
                CreditCarriedsList = VATDeclarationData.d.CFSet.results;
            }

            if (VATDeclarationData.d.ADRSet.results.Count > 0)
            {
                FullAddress = VATDeclarationData.d.ADRSet.results[0].BuildingNo + " " + VATDeclarationData.d.ADRSet.results[0].Street + " " + VATDeclarationData.d.ADRSet.results[0].Quarter + " " + VATDeclarationData.d.ADRSet.results[0].Region + " " + Environment.NewLine + VATDeclarationData.d.ADRSet.results[0].PostalCd;
            }
            vATCalculationData = await WebServiceManager.GAZTGetVATDeclaratinCalculationData(periodKey, TxnTp, status, FormBundleNumber, Gpart);
            PopToRootPage();
            if (vATCalculationData.d != null)
            {

                //      if(vATCalculationData.d.)
                if (vATCalculationData.d.VATRSet.results.Count != 0)
                {
                    CalculationRateSet = new List<VATCalculationDataVATRSet>();
                    CalculationRateSet = vATCalculationData.d.VATRSet.results;

                    CalculationRateSetVTTH = new List<VTTHSetResult>();
                    CalculationRateSetVTTH = vATCalculationData.d.VTTHSet.results;


                    RateSetAsPerDate();



                }

                if (vATCalculationData.d.VTTHSet.results.Count != 0)
                {
                    CorrectionPeriodAmount = vATCalculationData.d.VTTHSet.results.Where(x => x.Type == "001").Select(x => x.MaxVal).FirstOrDefault();
                }


            }

            if(VATDeclarationData.d.TcFg=="1")
            {
                IsDeclarationCheckedForInstruction = true;
            }
            if (VATDeclarationData.d.ConfStp2 == "1")
            {
                IsCheckedTaxPayerDetailsInfo = true;
            }
            if (VATDeclarationData.d.DecFg == "1")
            {
                IsDeclarationCheckedForSummary = true;
            }
            List<VATDeclarationTabbedPageName> vatTabbedList = new List<VATDeclarationTabbedPageName>();
            VatTabbledPageList = new List<VATDeclarationTabbedPageName>();

            VATDeclarationTabbedPageName s = new VATDeclarationTabbedPageName();
            s.pageName = "Instruction";
            vatTabbedList.Add(s);
            VATDeclarationTabbedPageName s1 = new VATDeclarationTabbedPageName();
            s1.pageName = "TaxPayer Details";
            vatTabbedList.Add(s1);
            VATDeclarationTabbedPageName s2 = new VATDeclarationTabbedPageName();
            s2.pageName = "VAT Return Form";
            vatTabbedList.Add(s2);
            VATDeclarationTabbedPageName s3 = new VATDeclarationTabbedPageName();
            s3.pageName = "Summary";
            vatTabbedList.Add(s3);

            VatTabbledPageList = vatTabbedList;
            PageSelectedItem = VatTabbledPageList[0];


            ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(VATDeclarationData.d.ATTACHSet.results as List<Attachment>);

            VatAttachmentsList = myCollection;

            if (VATDeclarationData != null)
            {
               // SetPageForDraft();
                if (VATDeclarationData.d != null)
                {
                    ResponseVATDeclarationD = VATDeclarationData.d;
                    SetData();
                }
                if (VATDeclarationData.d.NOTESSet.results != null && VATDeclarationData.d.NOTESSet.results.Count() != 0)
                {
                    ResponseNote = VATDeclarationData.d.NOTESSet.results;
                }
                if (VATDeclarationData.d.VATR_MSGSet.results != null && VATDeclarationData.d.VATR_MSGSet.results.Count() != 0)
                {
                    Responseobject = VATDeclarationData.d.VATR_MSGSet.results;
                }
                if (VATDeclarationData.d.IBANSet.results != null && VATDeclarationData.d.IBANSet.results.Count() != 0)
                {
                    ResponseIBANSET = VATDeclarationData.d.IBANSet.results;
                }
                if (VATDeclarationData.d.CFSet.results != null && VATDeclarationData.d.CFSet.results.Count() != 0)
                {
                    ResponseCFSET = VATDeclarationData.d.CFSet.results;
                }
                if (VATDeclarationData.d.ATTACHSet.results != null && VATDeclarationData.d.ATTACHSet.results.Count() != 0)
                {
                    ResponseAttachSet = VATDeclarationData.d.ATTACHSet.results;
                }
                if (VATDeclarationData.d.ADRSet.results != null && VATDeclarationData.d.ADRSet.results.Count() != 0)
                {
                    ResponseAddressSET = VATDeclarationData.d.ADRSet.results;
                }
            }

                //});

                //await Task.Run(() =>
                //{
                //    IsLoading = false;
                //});

                //int j = 5;
                //List<CreditCarried> creditsCrarriedDummy = new List<CreditCarried>();
                //CreditCarriedsList = new List<CreditCarried>();
                //for (j = 0; j < 6; j++)
                //{
                //    CreditCarried m = new CreditCarried();
                //    m.SerialNumber = "0001";
                //    m.ReturnReferenceNumber = "000000000001";
                //    m.DocumentNumber = "0102000010202";
                //    m.Amount = "100000000,00";

                //    creditsCrarriedDummy.Add(m);
                //}
                //CreditCarriedsList = creditsCrarriedDummy;


                //int k = 5;
                //List<VATAttachments> vatAttachment = new List<VATAttachments>();
                //VatAttachmentsList = new List<VATAttachments>();
                //for (k = 0; k < 6; k++)
                //{
                //    VATAttachments m = new VATAttachments();
                //    m.Id = "0001";
                //    m.DocumentName = "Test-Document.pdf";
                //    m.Size = "20.00";

                //    vatAttachment.Add(m);
                //}
                //VatAttachmentsList = vatAttachment;
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            }

        }



        private List<String> _ListOfActionButtonsApplicable;
        public List<String> ListOfActionButtonsApplicable
        {
            get
            {
                return _ListOfActionButtonsApplicable;
            }
            set
            {
                _ListOfActionButtonsApplicable = value;

                RaisePropertyChanged("ListOfActionButtonsApplicable");
            }
        }

        private List<String> _DummyListOfActionButtonsApplicable;
        public List<String> DummyListOfActionButtonsApplicable
        {
            get
            {
                return _DummyListOfActionButtonsApplicable;
            }
            set
            {
                _DummyListOfActionButtonsApplicable = value;

                RaisePropertyChanged("DummyListOfActionButtonsApplicable");
            }
        }

        private List<String> _ActualListOfActionButtonsApplicable;
        public List<String> ActualListOfActionButtonsApplicable
        {
            get
            {
                return _ActualListOfActionButtonsApplicable;
            }
            set
            {
                _ActualListOfActionButtonsApplicable = value;

                RaisePropertyChanged("ActualListOfActionButtonsApplicable");
            }
        }

        private async Task SaveReturnAndGetReturnAndSetButtons()
        {
            try
            {
                VATDeclaration response = WebServiceManager.SaveVATDeclarationData(VATDeclarationData);
            PopToRootPage();

            if (response != null && response.d != null && !string.IsNullOrEmpty(response.d.Fbnum))
            {
                try
                {
                    if (response != null && response.d != null)
                {
                    VATDeclarationData = response;
                    ResponseVATDeclarationD = VATDeclarationData.d;
                    //VATDeclaration _vATDeclaration = await WebServiceManager.GAZTGetVATReturns(VATDeclarationData.d.ReturnIdz, VATDeclarationData.d.Fbnumz, ICRListPageViewModel.EUser,"");

                        //if (_vATDeclaration != null && _vATDeclaration.d != null)
                        //{
                        //    VATDeclarationData = _vATDeclaration;
                        //    ResponseVATDeclarationD = VATDeclarationData.d;

                        //    SetData();
                        //}
                        ManageEnabledProperty(true);
                }

                SetButtons(VATDeclarationData);


                  


                   
                }
                catch(Exception ex)
                {

                }
            }
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            }
        }

        #region CalculationPart

        public void CreateDataForPost()
        {
            //List<Note> noteList = new List<Note>();
            //Note Note = new Note();
            //Note.Strline = NoteText;
            //noteList.Add(Note);
            //VATDeclarationData.d.NOTESSet.results = noteList;
            VATDeclarationD vATDeclarationD = SetDataForPost(ResponseVATDeclarationD);

            VATDeclarationData.d.TotalsalesAmt = vATDeclarationD.TotalsalesAmt;
            VATDeclarationData.d.TotalsalesAdj = vATDeclarationD.TotalsalesAdj;
            VATDeclarationData.d.TotalpurchaseAmt = vATDeclarationD.TotalpurchaseAmt;
            VATDeclarationData.d.TotalpurchaseAdj = vATDeclarationD.TotalpurchaseAdj;
            VATDeclarationData.d.StdsalesVat = vATDeclarationD.StdsalesVat;
            VATDeclarationData.d.TotalsalesAdj = vATDeclarationD.TotalsalesAdj;
            VATDeclarationData.d.StdpurchasesVat = vATDeclarationD.StdpurchasesVat;
            VATDeclarationData.d.ImportspaidVat = vATDeclarationD.ImportspaidVat;
            VATDeclarationData.d.ImportsaccVat = vATDeclarationD.ImportsaccVat;
            VATDeclarationData.d.TotalpurchaseVat = vATDeclarationD.TotalpurchaseVat;
            VATDeclarationData.d.TotaldueVat = vATDeclarationD.TotaldueVat;
            VATDeclarationData.d.Preperiodcorr = vATDeclarationD.Preperiodcorr;
            VATDeclarationData.d.CreditVat = vATDeclarationD.CreditVat;
            VATDeclarationData.d.NetdueVat = vATDeclarationD.NetdueVat;

            if(IsRefundVisible==true)
            {
                VATDeclarationData.d.RefundFg = "1";
                if (IsCheckedRefund==true)
                {
                    VATDeclarationData.d.Iban = IbanNumberText;
                    VATDeclarationData.d.IbanCb = "1";
                }
                else
                {
                    if (SelectedIBAN != null)
                    {
                        VATDeclarationData.d.Iban = SelectedIBAN.Iban;
                        VATDeclarationData.d.IbanCb = "0";
                    }
                }
                if (SelectedIBANType != null)
                {
                    VATDeclarationData.d.Idtype = SelectedIBANType.key;
                }
                if (SelectedIBANIDNumber != null)
                {
                    VATDeclarationData.d.Idnum = SelectedIBANIDNumber.Idnumber;
                }
            }
        }

        public VATDeclarationD SetDataForPost(VATDeclarationD vATDeclarationD)
        {
            vATDeclarationD.TotalsalesAmt = TotalsalesAmt;
            vATDeclarationD.TotalsalesAdj = TotalsalesAdj;
            vATDeclarationD.TotalpurchaseAmt = TotalpurchaseAmt;
            vATDeclarationD.TotalpurchaseAdj = TotalpurchaseAdj;
            vATDeclarationD.StdsalesVat = StdsalesVat;
            vATDeclarationD.TotalsalesAdj = TotalsalesVat;
            vATDeclarationD.StdpurchasesVat = StdpurchasesVat;
            vATDeclarationD.ImportspaidVat = ImportspaidVat;
            vATDeclarationD.ImportsaccVat = ImportsaccVat;
            vATDeclarationD.TotalpurchaseVat = TotalpurchaseVat;
            vATDeclarationD.TotaldueVat = TotaldueVat;
            vATDeclarationD.Preperiodcorr = Preperiodcorr;
            vATDeclarationD.CreditVat = CreditVat;
            vATDeclarationD.NetdueVat = NetdueVat;
            return vATDeclarationD;
        }

        //public void SetPageForDraft()
        //{
        //    if (!string.IsNullOrEmpty(VATDeclarationData.d.StepNumber))
        //    {
        //        if (VATDeclarationData.d.StepNumber == "00")
        //        {
        //            ClearPage();
        //            IsDeclarationCheckedForInstruction = false;
        //            IsVisibleInstrunction = true;
        //            PageSelectedItem = VatTabbledPageList[0];
        //        }
        //        if (VATDeclarationData.d.StepNumber == "01")
        //        {
        //            ClearPage();
        //            IsDeclarationCheckedForInstruction = false;
        //            IsVisibleInstrunction = true;
        //            PageSelectedItem = VatTabbledPageList[0];
        //        }
        //        else if (VATDeclarationData.d.StepNumber == "02")
        //        {
        //            ClearPage();
        //            IsDeclarationCheckedForInstruction = true;
        //            IsVisibleTaxPayerDetails = true;
        //            PageSelectedItem = VatTabbledPageList[1];
        //        }
        //        else if (VATDeclarationData.d.StepNumber == "03")
        //        {
        //            ClearPage();
        //            IsDeclarationCheckedForInstruction = true;
        //            IsCheckedTaxPayerDetailsInfo = true;
        //            IsVisibleVatReturnForm = true;
        //            PageSelectedItem = VatTabbledPageList[2];
        //        }
        //        else if (VATDeclarationData.d.StepNumber == "04")
        //        {
        //            ClearPage();
        //            IsDeclarationCheckedForInstruction = true;
        //            IsCheckedTaxPayerDetailsInfo = true;
        //            IsDeclarationCheckedForSummary = false;
        //            IsVisibleVatReturnForm = true;
        //            PageSelectedItem = VatTabbledPageList[3];
        //        }

        //    }
        //}

        public void SetData()
        {
            TotalsalesAmt = ResponseVATDeclarationD.TotalsalesAmt;
            TotalsalesAdj = ResponseVATDeclarationD.TotalsalesAdj;
            TotalpurchaseAmt = ResponseVATDeclarationD.TotalpurchaseAmt;
            TotalpurchaseAdj = ResponseVATDeclarationD.TotalpurchaseAdj;
            StdsalesVat = ResponseVATDeclarationD.StdsalesVat;
            TotalsalesVat = ResponseVATDeclarationD.TotalsalesAdj;
            StdpurchasesVat = ResponseVATDeclarationD.StdpurchasesVat;
            ImportspaidVat = ResponseVATDeclarationD.ImportspaidVat;
            ImportsaccVat = ResponseVATDeclarationD.ImportsaccVat;
            TotalpurchaseVat = ResponseVATDeclarationD.TotalpurchaseVat;
            TotaldueVat = ResponseVATDeclarationD.TotaldueVat;
            Preperiodcorr = ResponseVATDeclarationD.Preperiodcorr;
            CreditVat = ResponseVATDeclarationD.CreditVat;
            NetdueVat = ResponseVATDeclarationD.NetdueVat;

        }

        public void NavigationSetupForDraft()
        {
            if(VATDeclarationData.d.StepNumber=="01"|| VATDeclarationData.d.StepNumber == "1")
            {
                //InstrunctionClicked();
                PageSelectedItem = VatTabbledPageList[0];
            }
            else if(VATDeclarationData.d.StepNumber == "02" || VATDeclarationData.d.StepNumber == "2")
            {
               // TaxpayerDetailsClicked();
                PageSelectedItem = VatTabbledPageList[1];
            }
            else if(VATDeclarationData.d.StepNumber == "03" || VATDeclarationData.d.StepNumber == "3")
            {
                //VATReturnFormClicked();
                PageSelectedItem = VatTabbledPageList[2];
            }
            else if(VATDeclarationData.d.StepNumber == "04" || VATDeclarationData.d.StepNumber == "4")
            {
                //SummaryClicked();
                PageSelectedItem = VatTabbledPageList[3];
            }
        }

        private void SetNoteData()
        {
            VATDeclarationData.d.NOTESSet.results[0].Strline = NoteText;
        }

        public string StandardRatedSalesVatAmount(string Amount, string Adjustment)
        {
            string VATAmount = string.Empty;
            if (Amount!="." && Adjustment!=".")
            {
                if (!Amount.Contains("-") && !Adjustment.Contains("-"))
                {
                    Double dAmount = string.IsNullOrEmpty(Amount) ? 0 : Convert.ToDouble(Amount);
                    Double dAdjustment = string.IsNullOrEmpty(Adjustment) ? 0 : Convert.ToDouble(Adjustment);
                    Double dVATRate = Convert.ToDouble(VATRate002);

                    VATAmount = (((dAmount - dAdjustment) * dVATRate) / 100).ToString();
                }
            }
            return VATAmount;
        }

        public string TotalAmount(string Amount1, string Amount2, string Amount3, string Amount4, string Amount5)
        {
            String TotalAmount = string.Empty;
            if (Amount1!="." && Amount2!="." && Amount3!="." && Amount4!="." && Amount5!=".")
            {
                if (!Amount1.Contains("-") && !Amount2.Contains("-") && !Amount3.Contains("-") && !Amount4.Contains("-") && !Amount5.Contains("-"))
                {
                    TotalAmount = ((String.IsNullOrEmpty(Amount1) ? 0 : Convert.ToDouble(Amount1)) + (String.IsNullOrEmpty(Amount2) ? 0 : Convert.ToDouble(Amount2)) + (String.IsNullOrEmpty(Amount3) ? 0 : Convert.ToDouble(Amount3)) + (String.IsNullOrEmpty(Amount4) ? 0 : Convert.ToDouble(Amount4)) + (String.IsNullOrEmpty(Amount5) ? 0 : Convert.ToDouble(Amount5))).ToString();
                }
            }
            return TotalAmount;
        }

        public string TotalAdjustment(string Adjustment1, string Adjustment2, string Adjustment3, string Adjustment4, string Adjustment5)
        {
            String TotalAmount = string.Empty;
            if (Adjustment1!="." && Adjustment2!="." && Adjustment3!="." && Adjustment4!="." && Adjustment5!=".")
            {
                if (!Adjustment1.Contains("-") && !Adjustment2.Contains("-") && !Adjustment3.Contains("-") && !Adjustment4.Contains("-") && !Adjustment5.Contains("-"))
                {
                    TotalAmount = ((String.IsNullOrEmpty(Adjustment1) ? 0 : Convert.ToDouble(Adjustment1)) + (String.IsNullOrEmpty(Adjustment2) ? 0 : Convert.ToDouble(Adjustment2)) + (String.IsNullOrEmpty(Adjustment3) ? 0 : Convert.ToDouble(Adjustment3)) + (String.IsNullOrEmpty(Adjustment4) ? 0 : Convert.ToDouble(Adjustment4)) + (String.IsNullOrEmpty(Adjustment5) ? 0 : Convert.ToDouble(Adjustment5))).ToString();
                }
            }
            return TotalAmount;
        }

        public string TotalVatAmount(string Amount1, string Amount2, string Amount3)
        {
            String TotalAmount = string.Empty;

            if(String.IsNullOrEmpty(Amount1))
            {
                Amount1 = "0.0";
            }
            if (String.IsNullOrEmpty(Amount2))
            {
                Amount2 = "0.0";
            }
            if (String.IsNullOrEmpty(Amount3))
            {
                Amount3 = "0.0";
            }

            if (!String.IsNullOrEmpty(Amount1) && !String.IsNullOrEmpty(Amount2) && !String.IsNullOrEmpty(Amount3))
            {
                TotalAmount = (Convert.ToDouble(Amount1) + Convert.ToDouble(Amount2) + Convert.ToDouble(Amount3)).ToString();
            }
            return TotalAmount;
        }

        public string StandardRatedDomesticPurchaseVatAmount(string Amount, string Adjustment)
        {
            string VATAmount = string.Empty;
            if (Amount != "." && Adjustment != ".")
            {
                if (!Amount.Contains("-") && !Adjustment.Contains("-"))
                {
                    Double dAmount = string.IsNullOrEmpty(Amount) ? 0 : Convert.ToDouble(Amount);
                    Double dAdjustment = string.IsNullOrEmpty(Adjustment) ? 0 : Convert.ToDouble(Adjustment);
                    Double dVATRate = Convert.ToDouble(VATRate002);

                    VATAmount = (((dAmount - dAdjustment) * dVATRate) / 100).ToString();
                }
            }
            return VATAmount;
        }

        //This method is used to calculate  Imports subject to VAT accounted for through the reverse charge mechanism Vat Amount too.
        public string ImportSubjectToVatPaidAtCustomsVatAmountForDesignated(string Amount, string Adjustment)
        {
            string VATAmount = string.Empty;
            if (Amount!= "." && Adjustment!= ".")
            {
                if (!Amount.Contains("-") && !Adjustment.Contains("-"))
                {
                    Double dAmount = string.IsNullOrEmpty(Amount) ? 0 : Convert.ToDouble(Amount);
                    Double dAdjustment = string.IsNullOrEmpty(Adjustment) ? 0 : Convert.ToDouble(Adjustment);
                    Double dVATRate001 = Convert.ToDouble(VATRate001);
                    Double dVATRate002 = Convert.ToDouble(VATRate002);

                    VATAmount = (((dAmount * dVATRate001) / 100) - ((dAdjustment * dVATRate002) / 100)).ToString();
                }
            }
            return VATAmount;
        }

        public string ImportSubjectToVatPaidAtCustomsVatAmountForNonDesignated(string Amount, string Adjustment)
        {
            string VATAmount = string.Empty;
            if (Amount != "." && Adjustment != ".")
            {
                if (!Amount.Contains("-") && !Adjustment.Contains("-"))
                {
                    Double dAmount = string.IsNullOrEmpty(Amount) ? 0 : Convert.ToDouble(Amount);
                    Double dAdjustment = string.IsNullOrEmpty(Adjustment) ? 0 : Convert.ToDouble(Adjustment);
                    Double dVATRate = Convert.ToDouble(VATRate002);

                    VATAmount = (((dAmount - dAdjustment) * dVATRate) / 100).ToString();
                }
            }
            return VATAmount;
        }

        public string NetVatDue(string CurrentPeriod, string PreviousPeriod, string ForwardFromPreviousPeriod)
        {
          
            string NetVatDue = string.Empty;
            try
            {
              
                    Double dCurrentPeriod = string.IsNullOrEmpty(CurrentPeriod) ? 0 : Convert.ToDouble(CurrentPeriod);
                    Double dPreviousPeriod = string.IsNullOrEmpty(PreviousPeriod) ? 0 : Convert.ToDouble(PreviousPeriod);
                    Double dForwardFromPreviousPeriod = string.IsNullOrEmpty(ForwardFromPreviousPeriod) ? 0 : Convert.ToDouble(ForwardFromPreviousPeriod);

                    NetVatDue = (dCurrentPeriod + dPreviousPeriod + dForwardFromPreviousPeriod).ToString();
                
            }
            catch
            {

            }
            return NetVatDue;
        }

        #endregion

        #endregion

        public void SetButtonStrings(String ButtonName) 
        {
            if(ButtonName == "Submit")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.Submit);
            }
            else if (ButtonName == "Reject")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.ZVATRejectButton);
            }
            else if (ButtonName == "Void")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.ZZVoid);
            }
            else if (ButtonName == "Save")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.Save);
            }
            else if (ButtonName == "DisplayNotes")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.ZZDisplayNotes);
            }
            else if (ButtonName == "Validate")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.ZZValidate);
            }
            else if (ButtonName == "Attachments")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.Attachments);
            }
            else if (ButtonName == "Reset")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.ZZReset);
            }
            else if (ButtonName == "Createnotes")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.ZZCreateNotes);
            }
            else if (ButtonName == "Amend")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.ZZAmend);
            }
            else if (ButtonName == "Closed")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.ZZClose);
            }
            else if (ButtonName == "SavetheDeclaration")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.ZVATSavetheDeclarationButton);
            }
            else if (ButtonName == "CancelDeclaration")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.ZVATCancelDeclarationButton);
            }
            else if (ButtonName == "SubmittheDeclaration")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.ZVATSubmittheDeclarationButton);
            }
            else if (ButtonName == "Release")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.Release);
            }
            else if (ButtonName == "Approve")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.ZVATApproveButton);
            }
            else if (ButtonName == "Forward")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.ZVATForwardButton);
            }
            //else if (ButtonName == "NotesforER")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATNotesforERButton);
            //}
            //else if (ButtonName == "Assigntome")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATAssigntomeButton);
            //}
            //else if (ButtonName == "Calendar")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATCalendarButton);
            //}
            //else if (ButtonName == "Confirm")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.Confirm);
            //}
            //else if (ButtonName == "SendforInspection")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATSendforInspectionButton);
            //}
            //else if (ButtonName == "AssignInspector")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATAssignInspectorButton);
            //}
            //else if (ButtonName == "SendBack")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATSendBackButton);
            //}
            //else if (ButtonName == "AttachBankGuarantee")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATAttachBankGuaranteeButton);
            //}
            //else if (ButtonName == "ExtendDueDate")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATExtendDueDateButton);
            //}
            //else if (ButtonName == "Next")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZZNext);
            //}
            //else if (ButtonName == "InspectorSubmit")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATInspectorSubmitButton);
            //}
            //else if (ButtonName == "ApplicationDownloadforInspector")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATApplicationDownloadforInspectorButton);
            //}
            //else if (ButtonName == "Reviewed")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATReviewed);
            //}
            //else if (ButtonName == "AssignOfficer")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATAssignOfficerButton);
            //}
            //else if (ButtonName == "EditaMovementActivity")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATEditaMovementActivityButton);
            //}
            //else if (ButtonName == "CancelMovementActivity")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATCancelMovementActivityButton);
            //}
            //else if (ButtonName == "AddNewMovementActivity")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATAddNewMovementActivityButton);
            //}
            //else if (ButtonName == "SendforAudit")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATSendforAuditButton);
            //}
            //else if (ButtonName == "SendtoDirector")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATSendtoDirectorButton);
            //}
            //else if (ButtonName == "SubmitInspector")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATSubmitInspectorButton);
            //}
            //else if (ButtonName == "AttachUnloadingDocument")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATAttachUnloadingDocumentButton);
            //}
            //else if (ButtonName == "ClearDocument")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATClearDocumentButton);
            //}
            //else if (ButtonName == "ExtendApprovalTime")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATExtendApprovalTimeButton);
            //}
            //else if (ButtonName == "Change")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATChangeButton);
            //}
            //else if (ButtonName == "Extend")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATExtendButton);
            //}
            //else if (ButtonName == "Revoke")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATRevokeButton);
            //}
            //else if (ButtonName == "SendtoTaxpayer")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATSendtoTaxpayerButton);
            //}
            //else if (ButtonName == "SummaryDetails")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATSummaryDetailsButton);
            //}
            //else if (ButtonName == "PrintSDReleaseLetter")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATPrintSDReleaseLetterButton);
            //}
            //else if (ButtonName == "ReleaseBankGuarantee")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATReleaseBankGuaranteeButton);
            //}
            //else if (ButtonName == "ComplianceAndHistory")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATComplianceAndHistoryButton);
            //}
            //else if (ButtonName == "Previous")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATPreviousButton);
            //}
            //else if (ButtonName == "CancelReturn")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATCancelReturnButton);
            //}
            //else if (ButtonName == "RequestAdditionalInformation")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATRequestAdditionalInformationButton);
            //}
            //else if (ButtonName == "Salesdetails")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATSalesdetailsButton);
            //}
            //else if (ButtonName == "Changefromestimatetoaccounting")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATChangefromestimatetoaccountingButton);
            //}
            //else if (ButtonName == "Invoice")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATInvoiceButton);
            //}

            //else if (ButtonName == "ReviseDownPayment")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATReviseDownPaymentButton);
            //}

        }

        public async Task SetButtons(VATDeclaration vATDeclarationData)
        {
            try
            {
                List<ApplicableButton> VATApplicableButtons = await WebServiceManager.GAZTVATReturnGetApplicableButtons(VATDeclarationData.d.Fbnumz, VATDeclarationData.d.Langz, VATDeclarationData.d.Operationz, VATDeclarationData.d.Gpart, VATDeclarationData.d.Statusz, VATDeclarationData.d.TxnTpz,vATDeclarationData.d.Periodkeyz);
            PopToRootPage();
            ListOfActionButtonsApplicable = new List<string>();
            DummyListOfActionButtonsApplicable = new List<string>();
            if (VATApplicableButtons != null)
            {
                ListOfActionButtonsApplicable.Clear();
                DummyListOfActionButtonsApplicable.Clear();
                foreach (ApplicableButton button in VATApplicableButtons)
                {
                    SetButtonStrings(button.buttonEnumId.ToString());
                        //  DummyListOfActionButtonsApplicable.Add(button.buttonEnumId.ToString());
                        
                }
                    ListOfActionButtonsApplicable = DummyListOfActionButtonsApplicable;
                }
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            }
        }
    }
}
