
using System.Windows.Input;
using Newtonsoft.Json;
using System.Globalization;
using System.Collections.ObjectModel;
using ZATCAMAUI.Models;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Models.PaymentModel;
using ZATCAMAUI.Core.Mangers;
using Mopups.Services;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Views.NewDesign.VATDeclarationPages;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Views.NewDesign.PaymentOptions;
using ZATCAMAUI.Core.Interfaces;
namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{

    public class GAZTNewDesignVATReturnUpdatedUIPageViewModel : BaseViewModel
    {

        public bool isReturnsInAmendsVisible = true;

        public ICommand OnBackStepClicked { get; set; }
        public ICommand OnMoreClicked { get; set; }
        public ICommand OnBackButtonClicked { get; set; }

        public ICommand onCountinueClicked { get; set; }

        public ICommand onSecondButtonClicked { get; set; }

        public ICommand onRefundClicked { get; set; }

        public ICommand onCreditForwardClicked { get; set; }

        public ICommand ChangeRegistrationClicked { get; set; }

        public ICommand openAttchments { get; set; }

        public static bool IsFirstTimeForNote = false;





        #region Variable

        private VATReturnUpdatedUITabEnum _currentTab = VATReturnUpdatedUITabEnum.Instrunction;
        public VATReturnUpdatedUITabEnum currentTab
        {
            get => _currentTab;
            set
            {
                if (_currentTab == value) return;

                _currentTab = value;

                CurrentOpenedTab = _currentTab;

                OnPropertyChanged(nameof(currentTab));
                CurrentIndex = (int)_currentTab;
                OnPropertyChanged(nameof(CurrentIndex));
            }
        }

        private bool _IsPayNowVisible = false;
        public bool IsPayNowVisible
        {
            get
            {
                return _IsPayNowVisible;
            }
            set
            {
                _IsPayNowVisible = value;
                OnPropertyChanged("IsPayNowVisible");
            }
        }
        private bool _IsNoteAttachVisible = false;
        public bool IsNoteAttachVisible
        {
            get
            {
                return _IsNoteAttachVisible;
            }
            set
            {
                _IsNoteAttachVisible = value;
                OnPropertyChanged("IsNoteAttachVisible");
            }
        }

        private VATReturnUpdatedUITabEnum _currentOpenedTab = VATReturnUpdatedUITabEnum.Instrunction;
        public VATReturnUpdatedUITabEnum CurrentOpenedTab
        {
            get
            {
                return _currentOpenedTab;
            }
            set
            {
                if (_currentOpenedTab == value) return;

                _currentOpenedTab = value;
                OnPropertyChanged("CurrentOpenedTab");
            }
        }


        private int _currenrIndex = 1;
        public int CurrentIndex
        {
            get => _currenrIndex;
            set
            {
                if (_currenrIndex == value) return;

                if (VATDeclarationData != null && VATDeclarationData.data != null && VATDeclarationData.data.GoliveFg == "X")
                {
                    _currenrIndex = value;
                }
                else
                {

                    _currenrIndex = value > 1 ? value - 1 : value;
                }

                OnPropertyChanged(nameof(CurrentIndex));
                if (_currenrIndex == MaxIndex)
                {
                    MarkComplete = true;
                    OnPropertyChanged(nameof(MarkComplete));
                }
                else
                {
                    MarkComplete = false;
                    OnPropertyChanged(nameof(MarkComplete));
                }
            }
        }
        public bool MarkComplete { get; private set; } = false;
        //public int MaxIndex { get; set; } = 6;
        #endregion


        private int _maxIndex = 6;
        public int MaxIndex
        {
            get
            {
                return _maxIndex;
            }
            set
            {
                if (_maxIndex == value) return;

                _maxIndex = value;
                OnPropertyChanged("MaxIndex");
            }
        }

        private bool _isRefundButtonEnabled;
        public bool IsRefundButtonEnabled
        {
            get
            {
                return _isRefundButtonEnabled;
            }
            set
            {
                if (_isRefundButtonEnabled == value) return;

                _isRefundButtonEnabled = value;
                OnPropertyChanged("IsRefundButtonEnabled");
            }
        }
        private bool _applePayStatus;
        public bool ApplePayStatus
        {
            get
            {
                return _applePayStatus;
            }
            set
            {
                if (_applePayStatus == value) return;

                _applePayStatus = value;
                OnPropertyChanged("ApplePayStatus");
            }
        }

        public string ApplePayTokenData;

        private bool _isRefundButtonVisible;
        public bool IsRefundButtonVisible
        {
            get
            {
                return _isRefundButtonVisible;
            }
            set
            {
                if (_isRefundButtonVisible == value) return;

                _isRefundButtonVisible = value;
                OnPropertyChanged("IsRefundButtonVisible");
            }
        }

        private bool _isUnFocusedTextBox = false;
        public bool IsUnFocusedTextBox
        {
            get
            {
                return _isUnFocusedTextBox;
            }
            set
            {
                if (_isUnFocusedTextBox == value) return;

                _isUnFocusedTextBox = value;
                OnPropertyChanged("IsUnFocusedTextBox");
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
                if (_isMainButtonEnabled == value) return;

                _isMainButtonEnabled = value;
                OnPropertyChanged("IsMainButtonEnabled");
            }
        }
        private bool _TaxisMainButtonEnabled = false;
        public bool TaxIsMainButtonEnabled
        {
            get
            {
                return _TaxisMainButtonEnabled;
            }
            set
            {
                if (_TaxisMainButtonEnabled == value) return;

                _isMainButtonEnabled = value;
                OnPropertyChanged("TaxIsMainButtonEnabled");
            }
        }
        private bool _isCreditForwardBtnVisible = false;
        public bool IsCreditForwardBtnVisible
        {
            get
            {
                return _isCreditForwardBtnVisible;
            }
            set
            {
                if (_isCreditForwardBtnVisible == value) return;

                _isCreditForwardBtnVisible = value;
                OnPropertyChanged("IsCreditForwardBtnVisible");
            }
        }

        private bool _isVATReturnFieldCheckForSaveAsDraft = false;
        public bool IsVATReturnFieldCheckForSaveAsDraft
        {
            get
            {
                return _isVATReturnFieldCheckForSaveAsDraft;
            }
            set
            {
                if (_isVATReturnFieldCheckForSaveAsDraft == value) return;

                _isVATReturnFieldCheckForSaveAsDraft = value;
                OnPropertyChanged("IsVATReturnFieldCheckForSaveAsDraft");
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
                if (_vATDeclarationData == value) return;

                _vATDeclarationData = value;
                OnPropertyChanged("VATDeclarationData");
            }
        }

        private VATDeclaration _vATDeclarationDataDummy;
        public VATDeclaration VATDeclarationDataDummy
        {
            get
            {
                return _vATDeclarationDataDummy;
            }
            set
            {
                if (_vATDeclarationDataDummy == value) return;

                _vATDeclarationDataDummy = value;
                OnPropertyChanged("VATDeclarationDataDummy");
            }
        }

        private string _stepNumber;
        public string StepNumber
        {
            get
            {
                return _stepNumber;
            }
            set
            {
                if (_stepNumber == value) return;

                _stepNumber = value;
                OnPropertyChanged("StepNumber");
            }
        }
        private string _stepNumberz;
        public string StepNumberz
        {
            get
            {
                return _stepNumberz;
            }
            set
            {
                if (_stepNumberz == value) return;

                _stepNumberz = value;
                OnPropertyChanged("StepNumberz");
            }
        }

        private bool _isDeclarationCheckEnabled = true;
        public bool IsDeclarationCheckEnabled
        {
            get
            {
                return _isDeclarationCheckEnabled;
            }
            set
            {
                if (_isDeclarationCheckEnabled == value) return;

                _isDeclarationCheckEnabled = value;
                OnPropertyChanged("IsDeclarationCheckEnabled");
            }
        }
        private bool _isTaxPayerCheckEnabled = false;
        public bool IsTaxPayerCheckEnabled
        {
            get
            {
                return _isTaxPayerCheckEnabled;
            }
            set
            {
                if (_isTaxPayerCheckEnabled == value) return;

                _isTaxPayerCheckEnabled = value;
                OnPropertyChanged("IsTaxPayerCheckEnabled");
            }
        }

        private bool _isYesBoxEnabled = false;
        public bool IsYesBoxEnabled
        {
            get
            {
                return _isYesBoxEnabled;
            }
            set
            {
                if (_isYesBoxEnabled == value) return;

                _isYesBoxEnabled = value;
                OnPropertyChanged("IsYesBoxEnabled");
            }
        }

        private bool _isNoBoxEnabled = false;
        public bool IsNoBoxEnabled
        {
            get
            {
                return _isNoBoxEnabled;
            }
            set
            {
                if (_isNoBoxEnabled == value) return;

                _isNoBoxEnabled = value;
                OnPropertyChanged("IsNoBoxEnabled");
            }
        }

        private bool _isTaxYesBoxEnabled = false;
        public bool IsTaxYesBoxEnabled
        {
            get
            {
                return _isTaxYesBoxEnabled;
            }
            set
            {
                if (_isTaxYesBoxEnabled == value) return;

                _isTaxYesBoxEnabled = value;
                OnPropertyChanged("IsTaxYesBoxEnabled");
            }
        }
        private bool _isTaxAmendGrid = true;
        public bool IsTaxAmendGrid
        {
            get
            {
                return _isTaxAmendGrid;
            }
            set
            {

                _isTaxAmendGrid = value;
                OnPropertyChanged("IsTaxAmendGrid");
            }
        }

        private bool _isFivePercentTaxAmendGrid = true;
        public bool IsFivePercentTaxAmendGrid
        {
            get
            {
                return _isFivePercentTaxAmendGrid;
            }
            set
            {

                _isFivePercentTaxAmendGrid = value;
                OnPropertyChanged("IsFivePercentTaxAmendGrid");
            }
        }
        private bool _isTaxNoBoxEnabled = false;
        public bool IsTaxNoBoxEnabled
        {
            get
            {
                return _isTaxNoBoxEnabled;
            }
            set
            {
                if (_isTaxNoBoxEnabled == value) return;

                _isTaxNoBoxEnabled = value;
                OnPropertyChanged("IsTaxNoBoxEnabled");
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
                if (_iSSwichButtonEnable == value) return;

                _iSSwichButtonEnable = value;
                OnPropertyChanged("IsSwichButtonEnable");
            }
        }
        private bool _isRefundNoMsgDisplayed = false;
        public bool IsRefundNoMsgDisplayed
        {
            get
            {
                return _isRefundNoMsgDisplayed;
            }
            set
            {
                if (_isRefundNoMsgDisplayed == value) return;

                _isRefundNoMsgDisplayed = value;
                OnPropertyChanged("IsRefundNoMsgDisplayed");
            }
        }
        private bool _isControlEnabledForEntry = false;
        public bool IsControlEnabledForEntry
        {
            get
            {
                return _isControlEnabledForEntry;
            }
            set
            {
                if (_isControlEnabledForEntry == value) return;

                _isControlEnabledForEntry = value;
                OnPropertyChanged("IsControlEnabledForEntry");
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
                if (_isControlEnabled == value) return;

                _isControlEnabled = value;
                OnPropertyChanged("IsControlEnabled");
            }
        }



        private bool _isSwitchVisible = false;
        public bool IsSwitchVisible
        {
            get
            {
                return _isSwitchVisible;
            }
            set
            {
                if (_isSwitchVisible == value) return;

                _isSwitchVisible = value;
                OnPropertyChanged("IsSwitchVisible");
            }
        }
        private bool _IsSummaryCheckEnabled = false;
        public bool IsSummaryCheckEnabled
        {
            get
            {
                return _IsSummaryCheckEnabled;
            }
            set
            {
                if (_IsSummaryCheckEnabled == value) return;

                _IsSummaryCheckEnabled = value;
                OnPropertyChanged("IsSummaryCheckEnabled");
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
                if (_taxpayerPeriodFromDate == value) return;

                _taxpayerPeriodFromDate = value;
                OnPropertyChanged("TaxpayerPeriodFromDate");
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
                if (_taxpayerPeriodToDate == value) return;

                _taxpayerPeriodToDate = value;
                OnPropertyChanged("TaxpayerPeriodToDate");
            }
        }

        private List<Attachment> _aTTACHSetsList;
        public List<Attachment> ATTACHSetsList
        {
            get
            {
                return _aTTACHSetsList;
            }
            set
            {
                if (_aTTACHSetsList == value) return;

                _aTTACHSetsList = value;
                OnPropertyChanged("ATTACHSetsList");
            }
        }

        private List<Attachment> _dummyaTTACHSetsList;
        public List<Attachment> DummyATTACHSetsList
        {
            get
            {
                return _dummyaTTACHSetsList;
            }
            set
            {
                if (_dummyaTTACHSetsList == value) return;

                _dummyaTTACHSetsList = value;
                OnPropertyChanged("DummyATTACHSetsList");
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
                if (_creditCarriedsList == value) return;

                _creditCarriedsList = value;
                OnPropertyChanged("CreditCarriedsList");
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
                if (_fullAddress == value) return;

                _fullAddress = value;
                OnPropertyChanged("FullAddress");
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
                if (_calculationRateSet == value) return;

                _calculationRateSet = value;
                OnPropertyChanged("CalculationRateSet");
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
                if (_calculationRateSetVTTH == value) return;

                _calculationRateSetVTTH = value;
                OnPropertyChanged("CalculationRateSetVTTH");
            }
        }
        private List<IGRTSetResult> _calculationRateIGRTSet;
        public List<IGRTSetResult> CalculationRateIGRTSet
        {
            get
            {
                return _calculationRateIGRTSet;
            }
            set
            {
                if (_calculationRateIGRTSet == value) return;

                _calculationRateIGRTSet = value;
                OnPropertyChanged("CalculationRateIGRTSet");
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
                if (_iBANList == value) return;

                _iBANList = value;
                OnPropertyChanged("IBANList");
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
                if (_iBANTypesList == value) return;

                _iBANTypesList = value;
                OnPropertyChanged("IBANTypesList");
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
                if (_vATRate001 == value) return;

                _vATRate001 = value;
                OnPropertyChanged("VATRate001");
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
                if (_vATRate002 == value) return;

                _vATRate002 = value;
                OnPropertyChanged("VATRate002");
            }
        }

        private string _vATRate003 = string.Empty;
        public string VATRate003
        {
            get
            {
                return _vATRate003;
            }
            set
            {
                if (_vATRate003 == value) return;

                _vATRate003 = value;
                OnPropertyChanged("VATRate003");
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
                if (_correctionPeriodAmount == value) return;

                _correctionPeriodAmount = value;
                OnPropertyChanged("CorrectionPeriodAmount");
            }
        }
        private string _correctionNegativePeriodAmount;
        public string CorrectionNegativePeriodAmount
        {
            get
            {
                return _correctionNegativePeriodAmount;
            }
            set
            {
                if (_correctionNegativePeriodAmount == value) return;

                _correctionNegativePeriodAmount = value;
                OnPropertyChanged("CorrectionNegativePeriodAmount");
            }
        }
        public string _carriedValueString;
        public string CarriedValueString
        {
            get
            {
                return _carriedValueString;
            }
            set
            {
                if (_carriedValueString == value) return;

                _carriedValueString = value;
                OnPropertyChanged("CarriedValueString");
            }
        }

        public string _carriedValueStringNew;
        public string CarriedValueStringNew
        {
            get
            {
                return _carriedValueStringNew;
            }
            set
            {
                if (_carriedValueStringNew == value) return;

                _carriedValueStringNew = value;
                OnPropertyChanged("CarriedValueStringNew");
            }
        }


        public bool _IsCarriedForwandReviewMessage;
        public bool IsCarriedForwandReviewMessage
        {
            get
            {
                return _IsCarriedForwandReviewMessage;
            }
            set
            {
                if (_IsCarriedForwandReviewMessage == value) return;

                _IsCarriedForwandReviewMessage = value;
                OnPropertyChanged("IsCarriedForwandReviewMessage");
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
                    IsMainButtonEnabled = true;

                    VATDeclarationData.data.TcFg = "1";
                }
                else
                {
                    IsMainButtonEnabled = false;

                    VATDeclarationData.data.TcFg = "0";
                }
                OnPropertyChanged("IsDeclarationCheckedForInstruction");
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
                if (_isCheckedTaxPayerDetailsInfo == value) return;

                _isCheckedTaxPayerDetailsInfo = value;
                if (_isCheckedTaxPayerDetailsInfo == true)
                {

                    VATDeclarationData.data.ConfStp2 = "1";
                }
                else
                {

                    VATDeclarationData.data.ConfStp2 = "0";
                }
                OnPropertyChanged("IsCheckedTaxPayerDetailsInfo");
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
                if (_isDeclarationCheckedForSummary == value) return;

                _isDeclarationCheckedForSummary = value;
                if (_isDeclarationCheckedForSummary == true)
                {
                    IsMainButtonEnabled = true;
                    VATDeclarationData.data.DecFg = "1";
                    SetEnableForSubmitButton();
                }
                else
                {
                    VATDeclarationData.data.DecFg = "0";
                    IsMainButtonEnabled = false;
                    SetEnableForSubmitButton();
                }
                OnPropertyChanged("IsDeclarationCheckedForSummary");
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
                if (_vatAttachmentsList == value) return;

                _vatAttachmentsList = value;
                OnPropertyChanged("VatAttachmentsList");
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
                if (_responseVATDeclarationD == value) return;

                _responseVATDeclarationD = value;
                OnPropertyChanged("ResponseVATDeclarationD");
            }
        }
        private Result6 _vATNewModelFor15Percent;
        public Result6 VATNewModelFor15Percent
        {
            get
            {
                return _vATNewModelFor15Percent;
            }
            set
            {
                if (_vATNewModelFor15Percent == value) return;

                _vATNewModelFor15Percent = value;
                OnPropertyChanged("VATNewModelFor15Percent");
            }
        }
        private Result6 _vATNewModelFor5Percent;
        public Result6 VATNewModelFor5Percent
        {
            get
            {
                return _vATNewModelFor5Percent;
            }
            set
            {
                if (_vATNewModelFor5Percent == value) return;

                _vATNewModelFor5Percent = value;
                OnPropertyChanged("VATNewModelFor5Percent");
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
                if (_responseNote == value) return;

                _responseNote = value;
                OnPropertyChanged("ResponseNote");
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
                if (_responseobject == value) return;

                _responseobject = value;
                OnPropertyChanged("Responseobject");
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
                if (_responseIBANSET == value) return;

                _responseIBANSET = value;
                OnPropertyChanged("ResponseIBANSET");
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
                if (_responseCFSET == value) return;

                _responseCFSET = value;
                OnPropertyChanged("ResponseCFSET");
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
                if (_responseAttachSet == value) return;

                _responseAttachSet = value;
                OnPropertyChanged("ResponseAttachSet");
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
                if (_responseAddressSET == value) return;

                _responseAddressSET = value;
                OnPropertyChanged("ResponseResult5");
            }
        }

        public ValidatePaymentResponse _paymentData = null;
        public ValidatePaymentResponse PaymentData
        {
            get
            {
                return _paymentData;
            }
            set
            {
                if (_paymentData == value) return;

                _paymentData = value;
                OnPropertyChanged("PaymentData");
            }
        }

        public ApplePayGuidResponse _applePayData = null;
        public ApplePayGuidResponse ApplePayData
        {
            get
            {
                return _applePayData;
            }
            set
            {
                if (_applePayData == value) return;

                _applePayData = value;
                OnPropertyChanged("ApplePayData");
            }
        }

        #region NewProperty
        public string _totalsalesAmt = "0.00";
        public string TotalsalesAmt
        {
            get
            {
                return _totalsalesAmt;
            }
            set
            {
                if (_totalsalesAmt == value) return;

                _totalsalesAmt = value;
                OnPropertyChanged("TotalsalesAmt");
            }
        }
        public string _totalsalesAdj = "0.00";
        public string TotalsalesAdj
        {
            get
            {
                return _totalsalesAdj;
            }
            set
            {
                if (_totalsalesAdj == value) return;

                _totalsalesAdj = value;
                OnPropertyChanged("TotalsalesAdj");
            }
        }
        public string _totalpurchaseAmt = "0.00";
        public string TotalpurchaseAmt
        {
            get
            {
                return _totalpurchaseAmt;
            }
            set
            {
                if (_totalpurchaseAmt == value) return;

                _totalpurchaseAmt = value;
                OnPropertyChanged("TotalpurchaseAmt");
            }
        }
        public string _totalpurchaseAdj = "0.00";
        public string TotalpurchaseAdj
        {
            get
            {
                return _totalpurchaseAdj;
            }
            set
            {
                if (_totalpurchaseAdj == value) return;

                _totalpurchaseAdj = value;
                OnPropertyChanged("TotalpurchaseAdj");
            }
        }
        public string _stdsalesVat = "0.00";
        public string StdsalesVat
        {
            get
            {
                return _stdsalesVat;
            }
            set
            {
                if (_stdsalesVat == value) return;

                _stdsalesVat = value;
                OnPropertyChanged("StdsalesVat");
            }
        }

        public bool _iGetSadadNumberEnabled = false;
        public bool IsGetSadadNumberEnabled
        {
            get
            {
                return _iGetSadadNumberEnabled;
            }
            set
            {
                if (_iGetSadadNumberEnabled == value) return;

                _iGetSadadNumberEnabled = value;
                OnPropertyChanged("IsGetSadadNumberEnabled");
            }
        }




        public string _totaldueVat = "0.00";
        public string TotaldueVat
        {
            get
            {
                return _totaldueVat;
            }
            set
            {
                if (_totaldueVat == value) return;

                _totaldueVat = value;
                if (_totaldueVat != null)
                {
                    NetdueVat = NetVatDue(TotaldueVat, Preperiodcorr, CreditVat);
                }
                OnPropertyChanged("TotaldueVat");
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
                if (_preperiodcorr == value) return;

                try
                {
                    _preperiodcorr = value;
                    if (_preperiodcorr != null)
                    {
                        bool isValiedNumber = UtilityManager.IsEnglishNumberWithMinus(Preperiodcorr);
                        if (isValiedNumber)
                        {
                            NetdueVat = NetVatDue(TotaldueVat, Preperiodcorr, CreditVat);
                            if (_preperiodcorr != "." && _preperiodcorr != "" && _preperiodcorr != "-" && !string.IsNullOrEmpty(CorrectionPeriodAmount) && !string.IsNullOrEmpty(CorrectionNegativePeriodAmount))
                            {
                                if (Convert.ToDecimal(_preperiodcorr) >= Convert.ToDecimal(CorrectionPeriodAmount) || Convert.ToDecimal(_preperiodcorr) <= Convert.ToDecimal(CorrectionNegativePeriodAmount))
                                {
                                    IsGreaterThanFiveT = true;
                                }
                                else
                                {
                                    IsGreaterThanFiveT = false;
                                }
                            }
                        }
                    }
                    if (string.IsNullOrEmpty(Preperiodcorr) || string.IsNullOrEmpty("0.00"))
                        IsSwitchToggled = false;
                }
                catch (Exception)
                {
                }
                OnPropertyChanged("Preperiodcorr");
            }
        }


        public string _stdsalesVat15 = "0.00";
        public string StdsalesVat15
        {
            get
            {
                return _stdsalesVat15;
            }
            set
            {
                if (_stdsalesVat15 == value) return;

                _stdsalesVat15 = value;
                OnPropertyChanged("StdsalesVat15");
            }
        }
        public string _stdsalesVatGovt = "0.00";
        public string StdsalesVatGovt
        {
            get
            {
                return _stdsalesVatGovt;
            }
            set
            {
                if (_stdsalesVatGovt == value) return;
                _stdsalesVatGovt = value;
                OnPropertyChanged("StdsalesVatGovt");
            }
        }
        public string _stdsalesVat5 = "0.00";
        public string StdsalesVat5
        {
            get
            {
                return _stdsalesVat5;
            }
            set
            {
                if (_stdsalesVat5 == value) return;

                _stdsalesVat5 = value;
                OnPropertyChanged("StdsalesVat5");
            }
        }


        public string _stdpurchasesVat15 = "0.00";
        public string StdpurchasesVat15
        {
            get
            {
                return _stdpurchasesVat15;
            }
            set
            {
                if (_stdpurchasesVat15 == value) return;

                _stdpurchasesVat15 = value;
                OnPropertyChanged("StdpurchasesVat15");
            }
        }

        public string _stdpurchasesVat5 = "0.00";
        public string StdpurchasesVat5
        {
            get
            {
                return _stdpurchasesVat5;
            }
            set
            {
                if (_stdpurchasesVat5 == value) return;

                _stdpurchasesVat5 = value;
                OnPropertyChanged("StdpurchasesVat5");
            }
        }


        public string _importspaidVat15 = "0.00";
        public string ImportspaidVat15
        {
            get
            {
                return _importspaidVat15;
            }
            set
            {
                if (_importspaidVat15 == value) return;

                _importspaidVat15 = value;
                OnPropertyChanged("ImportspaidVat15");
            }
        }


        public string _importspaidVat5 = "0.00";
        public string ImportspaidVat5
        {
            get
            {
                return _importspaidVat5;
            }
            set
            {
                if (_importspaidVat5 == value) return;

                _importspaidVat5 = value;
                OnPropertyChanged("ImportspaidVat5");
            }
        }


        public string _importsaccVat15 = "0.00";
        public string ImportsaccVat15
        {
            get
            {
                return _importsaccVat15;
            }
            set
            {
                if (_importsaccVat15 == value) return;

                _importsaccVat15 = value;
                OnPropertyChanged("ImportsaccVat15");
            }
        }

        public string _importsaccVat5 = "0.00";
        public string ImportsaccVat5
        {
            get
            {
                return _importsaccVat5;
            }
            set
            {
                if (_importsaccVat5 == value) return;

                _importsaccVat5 = value;
                OnPropertyChanged("ImportsaccVat5");
            }
        }

        private string _referenceNumber = "";
        public string ReferenceNumber
        {
            get
            {
                return _referenceNumber;
            }
            set
            {
                if (_referenceNumber == value) return;

                _referenceNumber = value;
                OnPropertyChanged("ReferenceNumber");
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
                if (_taxablePeriod == value) return;

                _taxablePeriod = value;
                OnPropertyChanged("TaxablePeriod");
            }
        }


        #region  Color Property
        private Color _entryVatAmountTextColor;
        public Color EntryVatAmountTextColor
        {
            get
            {
                return _entryVatAmountTextColor;
            }
            set
            {
                if (_entryVatAmountTextColor == value) return;

                _entryVatAmountTextColor = value;
                if (_entryVatAmountTextColor == (Color)Application.Current.Resources["ErrorColor"])
                {
                    IsMainButtonEnabled = false;
                }
                else
                {
                    IsMainButtonEnabled = true;
                }
                OnPropertyChanged("EntryVatAmountTextColor");
            }
        }

        public bool _isGreaterThanFiveT;
        public bool IsGreaterThanFiveT
        {
            get
            {
                return _isGreaterThanFiveT;
            }
            set
            {
                if (_isGreaterThanFiveT == value) return;

                _isGreaterThanFiveT = value;
                OnPropertyChanged("IsGreaterThanFiveT");
            }
        }
        private bool _isSwitchToggled = false;
        public bool IsSwitchToggled
        {
            get
            {
                return _isSwitchToggled;
            }
            set
            {
                if (_isSwitchToggled == value) return;

                _isSwitchToggled = value;
                try
                {
                    if (string.IsNullOrEmpty(Preperiodcorr) && IsSwitchToggled)
                    {
                        IsSwitchToggled = false;
                    }
                    if (IsSwitchToggled)
                    {
                        if (!string.IsNullOrEmpty(Preperiodcorr) && !Preperiodcorr.Contains("-"))
                        {
                            if (App.IsArabic)
                            {
                                //  Preperiodcorr =  Preperiodcorr + "-";// d.ToString();
                                Preperiodcorr = "-" + Preperiodcorr;// d.ToString();
                            }
                            else
                            {
                                Preperiodcorr = "-" + Preperiodcorr;// d.ToString();
                            }
                        }
                    }
                    else
                    {
                        if (Preperiodcorr != null && Preperiodcorr.Contains("-") && !string.IsNullOrEmpty(Preperiodcorr))
                            Preperiodcorr = Preperiodcorr.Replace("-", "");
                    }
                }
                catch (Exception)
                {
                }
                OnPropertyChanged("IsSwitchToggled");
            }
        }
        public string _netdueVat = "0.00";
        public string NetdueVat
        {
            get
            {
                return _netdueVat;
            }
            set
            {
                if (_netdueVat == value) return;

                _netdueVat = value;
                OnPropertyChanged("NetdueVat");
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
                if (_creditVat == value) return;

                _creditVat = value;
                if (_creditVat != null)
                {
                    NetdueVat = NetVatDue(TotaldueVat, Preperiodcorr, CreditVat);
                }
                OnPropertyChanged("CreditVat");
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
                if (_totalsalesVat == value) return;

                _totalsalesVat = value;
                if (!string.IsNullOrEmpty(_totalsalesVat))
                {
                    if (string.IsNullOrEmpty(TotalsalesVat) || TotalsalesVat == "0")
                    {
                        TotalsalesVat = "0.00";
                    }
                    if (string.IsNullOrEmpty(TotalpurchaseVat) || TotalpurchaseVat == "0")
                    {
                        TotalpurchaseVat = "0.00";
                    }
                    TotaldueVat = (Convert.ToDouble(TotalsalesVat) - Convert.ToDouble(TotalpurchaseVat)).ToString();
                    if (TotaldueVat == "0")
                    {
                        TotaldueVat = "0.00";
                    }
                    if (!string.IsNullOrEmpty(TotaldueVat) && TotaldueVat != "0.00")
                    {
                        TotaldueVat = UtilityManager.GetCommaSeparatedAmount(TotaldueVat);
                    }
                }
                OnPropertyChanged("TotalsalesVat");
            }
        }
        public string _stdpurchasesVat = "0.00";
        public string StdpurchasesVat
        {
            get
            {
                return _stdpurchasesVat;
            }
            set
            {
                if (_stdpurchasesVat == value) return;

                _stdpurchasesVat = value;
                OnPropertyChanged("StdpurchasesVat");
            }
        }
        public string _importspaidVat = "0.00";
        public string ImportspaidVat
        {
            get
            {
                return _importspaidVat;
            }
            set
            {
                if (_importspaidVat == value) return;

                _importspaidVat = value;
                OnPropertyChanged("ImportspaidVat");
            }
        }
        public string _totalpurchaseVat = "0.00";
        public string TotalpurchaseVat
        {
            get
            {
                return _totalpurchaseVat;
            }
            set
            {
                if (_totalpurchaseVat == value) return;

                _totalpurchaseVat = value;
                if (!string.IsNullOrEmpty(_totalpurchaseVat))
                {
                    TotaldueVat = (Convert.ToDouble(TotalsalesVat) - Convert.ToDouble(TotalpurchaseVat)).ToString();
                    if (TotaldueVat == "0")
                    {
                        TotaldueVat = "0.00";
                    }
                    if (!string.IsNullOrEmpty(TotaldueVat) && TotaldueVat != "0.00")
                    {
                        TotaldueVat = UtilityManager.GetCommaSeparatedAmount(TotaldueVat);
                    }
                }
                OnPropertyChanged("TotalpurchaseVat");
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
                if (_amountPayable == value) return;

                _amountPayable = value;
                OnPropertyChanged("AmountPayable");
            }
        }
        public string _importsaccVat = "0.00";
        public string ImportsaccVat
        {
            get
            {
                return _importsaccVat;
            }
            set
            {
                if (_importsaccVat == value) return;

                _importsaccVat = value;
                OnPropertyChanged("ImportsaccVat");
            }
        }
        #endregion

        //public Color _BoxSevenFrame;
        //public Color BoxSevenFrame
        //{
        //    get
        //    {
        //        return _BoxSevenFrame;
        //    }
        //    set
        //    {
        //        _BoxSevenFrame = value;
        //        OnPropertyChanged("BoxSevenFrame");
        //    }
        //}

        //public Color _BoxSixFrame;
        //public Color BoxSixFrame
        //{
        //    get
        //    {
        //        return _BoxSixFrame;
        //    }
        //    set
        //    {
        //        _BoxSixFrame = value;
        //        OnPropertyChanged("BoxSixFrame");
        //    }
        //}

        //public Color _BoxFiveFrame;
        //public Color BoxFiveFrame
        //{
        //    get
        //    {
        //        return _BoxFiveFrame;
        //    }
        //    set
        //    {
        //        _BoxFiveFrame = value;
        //        OnPropertyChanged("BoxFiveFrame");
        //    }
        //}

        //public Color _sevenbox;
        //public Color sevenbox
        //{
        //    get
        //    {
        //        return _sevenbox;
        //    }
        //    set
        //    {
        //        _sevenbox = value;
        //        OnPropertyChanged("sevenbox");
        //    }
        //}

        //public Color _sixbox;
        //public Color sixbox
        //{
        //    get
        //    {
        //        return _sixbox;
        //    }
        //    set
        //    {
        //        _sixbox = value;
        //        OnPropertyChanged("sixbox");
        //    }
        //}

        //public Color _fivebox;
        //public Color fivebox
        //{
        //    get
        //    {
        //        return _fivebox;
        //    }
        //    set
        //    {
        //        _fivebox = value;
        //        OnPropertyChanged("fivebox");
        //    }
        //}

        public bool _isNavigatedToSubmitted;
        public bool IsNavigatedToSubmitted
        {
            get
            {
                return _isNavigatedToSubmitted;
            }
            set
            {
                if (_isNavigatedToSubmitted == value) return;

                _isNavigatedToSubmitted = value;
                OnPropertyChanged("IsNavigatedToSubmitted");
            }
        }



        public string _refundButtonText;
        public string RefundButtonText
        {
            get
            {
                return _refundButtonText;
            }
            set
            {
                if (_refundButtonText == value) return;

                _refundButtonText = value;
                OnPropertyChanged("RefundButtonText");
            }
        }

        public bool _isBtnVisible;
        public bool isBtnVisible
        {
            get
            {
                return _isBtnVisible;
            }
            set
            {
                if (_isBtnVisible == value) return;

                _isBtnVisible = value;
                OnPropertyChanged("isBtnVisible");
            }
        }

        public bool _isMainButtonVisible;
        public bool IsMainButtonVisible
        {
            get
            {
                return _isMainButtonVisible;
            }
            set
            {
                if (_isMainButtonVisible == value) return;

                _isMainButtonVisible = value;
                OnPropertyChanged("IsMainButtonVisible");
            }
        }

        public bool _isVoidClicked = false;
        public bool IsVoidClicked
        {
            get
            {
                return _isVoidClicked;
            }
            set
            {
                if (_isVoidClicked == value) return;

                _isVoidClicked = value;
                OnPropertyChanged("IsVoidClicked");
            }
        }

        public bool _isResetClicked = false;
        public bool IsResetClicked
        {
            get
            {
                return _isResetClicked;
            }
            set
            {
                if (_isResetClicked == value) return;

                _isResetClicked = value;
                OnPropertyChanged("IsResetClicked");
            }
        }



        //public bool _isCheckVisible;
        //public bool isCheckVisible
        //{
        //    get
        //    {
        //        return _isCheckVisible;
        //    }
        //    set
        //    {
        //        _isCheckVisible = value;
        //        OnPropertyChanged("isCheckVisible");
        //    }
        //}

        //public Color _OuterFrame;
        //public Color OuterFrame
        //{
        //    get
        //    {
        //        return _OuterFrame;
        //    }
        //    set
        //    {
        //        _OuterFrame = value;
        //        OnPropertyChanged("OuterFrame");
        //    }
        //}

        //public Color _secOuterFrame;
        //public Color secOuterFrame
        //{
        //    get
        //    {
        //        return _secOuterFrame;
        //    }
        //    set
        //    {
        //        _secOuterFrame = value;
        //        OnPropertyChanged("secOuterFrame");
        //    }
        //}

        //public Color _innerFrame;
        //public Color innerFrame
        //{
        //    get
        //    {
        //        return _innerFrame;
        //    }
        //    set
        //    {
        //        _innerFrame = value;
        //        OnPropertyChanged("innerFrame");
        //    }
        //}

        //public Color _secBox;
        //public Color secBox
        //{
        //    get
        //    {
        //        return _secBox;
        //    }
        //    set
        //    {
        //        _secBox = value;
        //        OnPropertyChanged("secBox");
        //    }
        //}

        //public Color _thirdBox;
        //public Color thirdBox
        //{
        //    get
        //    {
        //        return _thirdBox;
        //    }
        //    set
        //    {
        //        _thirdBox = value;
        //        OnPropertyChanged("thirdBox");
        //    }
        //}

        //public Color _fourthBox;
        //public Color fourthBox
        //{
        //    get
        //    {
        //        return _fourthBox;
        //    }
        //    set
        //    {
        //        _fourthBox = value;
        //        OnPropertyChanged("fourthBox");
        //    }
        //}

        //public bool _IsInstrunctionView ;
        //public bool IsInstrunctionView
        //{
        //    get
        //    {
        //        return _IsInstrunctionView;
        //    }
        //    set
        //    {
        //        _IsInstrunctionView = value;
        //        OnPropertyChanged("IsInstrunctionView");
        //    }
        //}

        //public bool _IsSaleView;
        //public bool IsSaleView
        //{
        //    get
        //    {
        //        return _IsSaleView;
        //    }
        //    set
        //    {
        //        _IsSaleView = value;
        //        OnPropertyChanged("IsSaleView");
        //    }
        //}

        //public bool _IsPurchaseView;
        //public bool IsPurchaseView
        //{
        //    get
        //    {
        //        return _IsPurchaseView;
        //    }
        //    set
        //    {
        //        _IsPurchaseView = value;
        //        OnPropertyChanged("IsPurchaseView");
        //    }
        //}

        //public bool _IsTotalVatView;
        //public bool IsTotalVatView
        //{
        //    get
        //    {
        //        return _IsTotalVatView;
        //    }
        //    set
        //    {
        //        _IsTotalVatView = value;
        //        OnPropertyChanged("IsTotalVatView");
        //    }
        //}

        //public bool _IsSummeryView;
        //public bool IsSummeryView
        //{
        //    get
        //    {
        //        return _IsSummeryView;
        //    }
        //    set
        //    {
        //        _IsSummeryView = value;
        //        OnPropertyChanged("IsSummeryView");
        //    }
        //}

        //public bool _IsVATReturnsView;
        //public bool IsVATReturnsView
        //{
        //    get
        //    {
        //        return _IsVATReturnsView;
        //    }
        //    set
        //    {
        //        _IsVATReturnsView = value;
        //        OnPropertyChanged("IsVATReturnsView");
        //    }
        //}

        //public bool _IsTaxpayerView;
        //public bool IsTaxpayerView
        //{
        //    get
        //    {
        //        return _IsTaxpayerView;
        //    }
        //    set
        //    {
        //        _IsTaxpayerView = value;
        //        OnPropertyChanged("IsTaxpayerView");
        //    }
        //}


        public string _CreditDetailsText;
        public string CreditDetailsText
        {
            get
            {
                return _CreditDetailsText;
            }
            set
            {
                if (_CreditDetailsText == value) return;

                _CreditDetailsText = value;
                OnPropertyChanged("CreditDetailsText");
            }
        }
        private bool _onMoreOptionsEnabled = true;
        public bool OnMoreOptionsEnabled
        {
            get
            {
                return _onMoreOptionsEnabled;
            }
            set
            {
                if (_onMoreOptionsEnabled == value) return;

                _onMoreOptionsEnabled = value;
                OnPropertyChanged("OnMoreOptionsEnabled");
            }
        }

        private bool _isNewLoading = false;
        public bool IsNewLoading
        {
            get
            {
                return _isNewLoading;
            }
            set
            {
                if (_isNewLoading == value) return;

                _isNewLoading = value;
                OnPropertyChanged("IsNewLoading");
            }
        }

        private bool _isVisibleAmendButton = false;
        public bool IsVisibleAmendButton
        {
            get
            {
                return _isVisibleAmendButton;
            }
            set
            {
                if (_isVisibleAmendButton == value) return;

                _isVisibleAmendButton = value;
                if (_isVisibleAmendButton == true)
                {
                    RefundButtonText = AppResources.ZZZZViewRefund;
                }
                else
                {
                   // RefundButtonText = AppResources.ZZZZConfirmAndRefundRequest;
                    RefundButtonText = AppResources.ZZZZNewConfirmAndRefundRequest;
                }
                OnPropertyChanged("IsVisibleAmendButton");
            }
        }

        private bool _isAmendButtonAvailable = false;
        public bool IsAmendButtonAvailable
        {
            get
            {
                return _isAmendButtonAvailable;
            }
            set
            {
                if (_isAmendButtonAvailable == value) return;

                _isAmendButtonAvailable = value;
                OnPropertyChanged("IsAmendButtonAvailable");
            }
        }

        public static bool IsAmend = false;
        private bool _isAmendClicked = false;
        public bool IsAmendClicked
        {
            get
            {
                return _isAmendClicked;
            }
            set
            {
                if (_isAmendClicked == value) return;

                _isAmendClicked = value;
                if (_isAmendClicked == true)
                {
                    // IsGetAcknowledgementClicked = false;
                    IsAmend = true;
                }
                else
                {
                    IsAmend = false;
                }
                OnPropertyChanged("IsAmendClicked");
            }
        }
        private List<string> _ListOfActionButtonsApplicable;
        public List<string> ListOfActionButtonsApplicable
        {
            get
            {
                return _ListOfActionButtonsApplicable;
            }
            set
            {
                if (_ListOfActionButtonsApplicable == value) return;

                _ListOfActionButtonsApplicable = value;
                if (_ListOfActionButtonsApplicable != null && _ListOfActionButtonsApplicable.Count() != 0)
                {
                    OnMoreOptionsEnabled = true;
                }
                else
                {
                    OnMoreOptionsEnabled = false;
                }
                OnPropertyChanged("ListOfActionButtonsApplicable");
            }
        }
        private List<string> _DummyListOfActionButtonsApplicable;
        public List<string> DummyListOfActionButtonsApplicable
        {
            get
            {
                return _DummyListOfActionButtonsApplicable;
            }
            set
            {
                if (_DummyListOfActionButtonsApplicable == value) return;

                _DummyListOfActionButtonsApplicable = value;
                OnPropertyChanged("DummyListOfActionButtonsApplicable");
            }
        }


        private bool _isEnableToggledFor15PercentChange = false;
        public bool IsEnableSwitchToggledFor15PercentChange
        {
            get
            {
                return _isEnableToggledFor15PercentChange;
            }
            set
            {
                if (_isEnableToggledFor15PercentChange == value) return;

                _isEnableToggledFor15PercentChange = value;
                OnPropertyChanged("IsEnableSwitchToggledFor15PercentChange");
            }
        }

        private bool _isSwitchToggledFor15PercentChange = false;
        public bool IsSwitchToggledFor15PercentChange
        {
            get
            {
                return _isSwitchToggledFor15PercentChange;
            }
            set
            {
                if (_isSwitchToggledFor15PercentChange == value) return;

                _isSwitchToggledFor15PercentChange = value;

                if (_isSwitchToggledFor15PercentChange == true)
                {
                    IsYesChecked = true;
                    IsNoChecked = false;
                }
                else
                {
                    IsNoChecked = true;
                    IsYesChecked = false;
                }

                OnPropertyChanged("IsSwitchToggledFor15PercentChange");
            }
        }
       // private bool _isSwitchToggledForSaleTax = false;
      
        private bool _isSwitchToggledForSaleTax = false;
        public bool IsSwitchToggledForSaleTax
        {
            get
            {
                return _isSwitchToggledForSaleTax;
            }
            set
            {
                if (_isSwitchToggledForSaleTax == value) return;

                _isSwitchToggledForSaleTax = value;

                if (_isSwitchToggledForSaleTax == true)
                {
                    IsTaxYesChecked = true;
                    IsTaxNoChecked = false;
                }
                else
                {
                    IsTaxNoChecked = true;
                    IsTaxYesChecked = false;
                }

                OnPropertyChanged("IsSwitchToggledForSaleTax");
            }
        }

        private bool _isYesChecked = false;
        public bool IsYesChecked
        {
            get
            {
                return _isYesChecked;
            }
            set
            {
                if (_isYesChecked == value) return;

                _isYesChecked = value;
                OnPropertyChanged("IsYesChecked");
            }
        }

        private bool _isTaxNoChecked = true;
        public bool IsTaxNoChecked
        {
            get
            {
                return _isTaxNoChecked;
            }
            set
            {
                if (_isTaxNoChecked == value) return;

                _isTaxNoChecked = value;
                if (_isTaxNoChecked == true)
                {

                    IsSaleSubjecttoTaxEditable = false;
                }
                else
                {

                    IsSaleSubjecttoTaxEditable = true;
                }
                OnPropertyChanged("IsTaxNoChecked");
            }
        }
        private bool _isTaxYesChecked = false;
        public bool IsTaxYesChecked
        {
            get
            {
                return _isTaxYesChecked;
            }
            set
            {
                if (_isTaxYesChecked == value) return;

                _isTaxYesChecked = value;
                OnPropertyChanged("IsTaxYesChecked");
            }
        }

        private bool _isNoChecked = true;
        public bool IsNoChecked
        {
            get
            {
                return _isNoChecked;
            }
            set
            {
                if (_isNoChecked == value) return;

                _isNoChecked = value;
                if (_isNoChecked == true)
                {
                    IsFifteenPersenctVisible = true;
                    IsFivePersenctVisible = false;
                }
                else
                {
                    IsFifteenPersenctVisible = true;
                    IsFivePersenctVisible = true;
                }
                OnPropertyChanged("IsNoChecked");
            }
        }
        private bool _isNewReturn = false;
        public bool IsNewReturn
        {
            get
            {
                return _isNewReturn;
            }
            set
            {
                if (_isNewReturn == value) return;

                _isNewReturn = value;
                OnPropertyChanged("IsNewReturn");
            }
        }


        private bool _isFifteenPercentChange = false;
        public bool IsFifteenPercentChange
        {
            get
            {
                return _isFifteenPercentChange;
            }
            set
            {
                if (_isFifteenPercentChange == value) return;

                _isFifteenPercentChange = value;
                OnPropertyChanged("IsFifteenPercentChange");
            }
        }

        private bool _isFivePersenctVisible = false;
        public bool IsFivePersenctVisible
        {
            get
            {
                return _isFivePersenctVisible;
            }
            set
            {
                if (_isFivePersenctVisible == value) return;

                _isFivePersenctVisible = value;
                OnPropertyChanged("IsFivePersenctVisible");
            }
        }

        private bool _isFivePersenctEditable = false;
        public bool IsFivePersenctEditable
        {
            get
            {
                return _isFivePersenctEditable;
            }
            set
            {
                if (_isFivePersenctEditable == value) return;

                _isFivePersenctEditable = value;
                OnPropertyChanged("IsFivePersenctEditable");
            }
        }

        private bool _isFifteenPersenctVisible = false;
        public bool IsFifteenPersenctVisible
        {
            get
            {
                return _isFifteenPersenctVisible;
            }
            set
            {
                if (_isFifteenPersenctVisible == value) return;

                _isFifteenPersenctVisible = value;
                OnPropertyChanged("IsFifteenPersenctVisible");
            }
        }
        private bool _isSaleSubjecttoTaxVisible = false;
        public bool IsSaleSubjecttoTaxVisible
        {
            get
            {
                return _isSaleSubjecttoTaxVisible;
            }
            set
            {
                if (_isSaleSubjecttoTaxVisible == value) return;

                _isSaleSubjecttoTaxVisible = value;
                OnPropertyChanged("IsSaleSubjecttoTaxVisible");
            }
        }

        private bool _isGovtYesNoVisible = false;
        public bool IsGovtYesNoVisible
        {
            get
            {
                return _isGovtYesNoVisible;
            }
            set
            {
                if (_isGovtYesNoVisible == value) return;

                _isGovtYesNoVisible = value;
                OnPropertyChanged("IsGovtYesNoVisible");
            }
        }
        private bool _isSaleSubjecttoTaxEditable = false;
        public bool IsSaleSubjecttoTaxEditable
        {
            get
            {
                return _isSaleSubjecttoTaxEditable;
            }
            set
            {
                if (_isSaleSubjecttoTaxEditable == value) return;

                _isSaleSubjecttoTaxEditable = value;
                OnPropertyChanged("IsSaleSubjecttoTaxEditable");
            }
        }
        private bool _isPrevReturn = false;
        public bool IsPrevReturn
        {
            get
            {
                return _isPrevReturn;
            }
            set
            {
                if (_isPrevReturn == value) return;

                _isPrevReturn = value;
                OnPropertyChanged("IsPrevReturn");
            }
        }

        private string _vATRate002For15Percent;
        public string VATRate002For15Percent
        {
            get
            {
                return _vATRate002For15Percent;
            }
            set
            {
                if (_vATRate002For15Percent == value) return;

                _vATRate002For15Percent = value;
                OnPropertyChanged("VATRate002For15Percent");
            }
        }

        private string _vATRate003For5Percent;
        public string VATRate003For5Percent
        {
            get
            {
                return _vATRate003For5Percent;
            }
            set
            {
                if (_vATRate003For5Percent == value) return;

                _vATRate003For5Percent = value;
                OnPropertyChanged("VATRate003For5Percent");
            }
        }

        public string _ContinueText;
        public string ContinueText
        {
            get
            {
                return _ContinueText;
            }
            set
            {
                if (_ContinueText == value) return;

                _ContinueText = value;
                OnPropertyChanged("ContinueText");
            }
        }


        private string _yesBackgroundImage;
        public string YesBackgroundImage
        {
            get
            {
                return _yesBackgroundImage;
            }
            set
            {
                if (_yesBackgroundImage == value) return;

                _yesBackgroundImage = value;
                OnPropertyChanged("YesBackgroundImage");
            }
        }

        private string _noBackgroundImage;
        public string NoBackgroundImage
        {
            get
            {
                return _noBackgroundImage;
            }
            set
            {
                if (_noBackgroundImage == value) return;

                _noBackgroundImage = value;
                OnPropertyChanged("NoBackgroundImage");
            }
        }

        private string _TaxyesBackgroundImage;
        public string TaxYesBackgroundImage
        {
            get
            {
                return _TaxyesBackgroundImage;
            }
            set
            {
                if (_TaxyesBackgroundImage == value) return;

                _TaxyesBackgroundImage = value;
                OnPropertyChanged("TaxYesBackgroundImage");
            }
        }

        private string _TaxnoBackgroundImage;
        public string TaxNoBackgroundImage
        {
            get
            {
                return _TaxnoBackgroundImage;
            }
            set
            {
                if (_TaxnoBackgroundImage == value) return;

                _TaxnoBackgroundImage = value;
                OnPropertyChanged("TaxNoBackgroundImage");
            }
        }
        private Color _yesLabelColor;
        public Color YesLabelColor
        {
            get
            {
                return _yesLabelColor;
            }
            set
            {
                if (_yesLabelColor == value) return;

                _yesLabelColor = value;
                OnPropertyChanged("YesLabelColor");
            }
        }

        private Color _noLabelColor;
        public Color NoLabelColor
        {
            get
            {
                return _noLabelColor;
            }
            set
            {
                if (_noLabelColor == value) return;

                _noLabelColor = value;
                OnPropertyChanged("NoLabelColor");
            }
        }
        private Color _TaxyesLabelColor;
        public Color TaxYesLabelColor
        {
            get
            {
                return _TaxyesLabelColor;
            }
            set
            {
                if (_TaxyesLabelColor == value) return;

                _TaxyesLabelColor = value;
                OnPropertyChanged("TaxYesLabelColor");
            }
        }

        private Color _TaxnoLabelColor;
        public Color TaxNoLabelColor
        {
            get
            {
                return _TaxnoLabelColor;
            }
            set
            {
                if (_TaxnoLabelColor == value) return;

                _TaxnoLabelColor = value;
                OnPropertyChanged("TaxNoLabelColor");
            }
        }
        #endregion

        #region Constructor


        public GAZTNewDesignVATReturnUpdatedUIPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {



            ContinueText = AppResources.ZZZZContinue;

            openAttchments = new Command(() =>
            {
                 MopupService.Instance.PushAsync(new MoreOptionsNote(VATDeclarationData));
            });

            OnMoreClicked = new Command(() =>
            {
                if (IsNavigatedToSubmitted == false)
                {
                    MopupService.Instance.PushAsync(new MorePopUpPageView(ListOfActionButtonsApplicable));
                }
            });

            OnBackButtonClicked = new Command(() =>
            {
                _navigationService.GoBack();
            });

            onRefundClicked = new Command(() =>
            {
                try
                {

                    bool value = IsCheckedDraftMode();
                    if (IsDeclarationCheckedForSummary)
                    {
                        if (checkforrefundclicked())
                        {
                            if (App.ICRStatus == "E0001" || App.ICRStatus == "E0013")
                            {
                                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                                headerAmountInfo.HeaderText = AppResources.ZZZConfirmationMsg;
                                headerAmountInfo.IsLinkAvailable = false;
                                headerAmountInfo.Message = AppResources.ZZZNewRefundEnableMessage;

                                headerWithInfos.Add(headerAmountInfo);


                                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                                newDesignPopUp.MainHeader = "";

                                MopupService.Instance.PushAsync(new ShowVatInformationConfirmationPageView(newDesignPopUp));
                            }
                            else
                            {
                                SetDataForRefundPopup();
                                MopupService.Instance.PushAsync(new RefundAccountPopupPageView(VATDeclarationData));
                            }
                        }
                    }
                    else
                    {
                        List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                        HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                        NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                        headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                        headerAmountInfo.IsLinkAvailable = false;
                        headerAmountInfo.Message = AppResources.ZZZZPleaseAgreeTandCMsg;

                        headerWithInfos.Add(headerAmountInfo);


                        newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                        newDesignPopUp.HeaderWithInfos = headerWithInfos;
                        newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                        MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
                    }

                }
                catch (Exception)
                {
                }
            });

            onCreditForwardClicked = new Command(() =>
            {
                try
                {

                    if(!CreditVat.Equals("0.00"))
                    MopupService.Instance.PushAsync(new VATCreditCarriedForwardPopUpPageView(VATDeclarationData));
                    

                }
                catch (Exception)
                {
                }
            });

            ChangeRegistrationClicked = new Command(() =>
            {
                try
                {

                    var VisitPortalPopup = new ReturnPortalNavigationPopUp(AppResources.ZZZChangeRegistationNote);

                    if (App.IsArabic)
                    {
                        VisitPortalPopup.OnGotoPortal = () =>
                        {

                            Launcher.OpenAsync(ZATCAConstants.GAZTVisitPortalUrlAR);

                        };
                    }
                    else
                    {
                        VisitPortalPopup.OnGotoPortal = () =>
                        {

                            Launcher.OpenAsync(ZATCAConstants.GAZTVisitPortalUrlEN);

                        };
                    }

                    MopupService.Instance.PushAsync(VisitPortalPopup);

                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());
                }
            });




            onSecondButtonClicked = new Command(async () =>
            {
                if (CreditDetailsText == AppResources.Submit)
                {

                    if (IsDeclarationCheckedForSummary)
                    {
                        if (IsDeclarationCheckedForSummary && (IsVoidClicked == false /*&& IsResetClicked == false*/))
                        {
                            if ((App.ICRStatus == "E0045" || App.ICRStatus == "E0006") && IsAmendClicked == true || App.ICRStatus == "E0001" || IsCheckedDraftMode())
                            {

                                if(VATDeclarationData.data.ReviewNaMsg.Equals(""))
                                {
                                    MainThread.BeginInvokeOnMainThread(() =>
                                    {
                                        IsNewLoading = true;
                                    });
                                    await SubmitClicked();
                                    MainThread.BeginInvokeOnMainThread(() =>
                                    {
                                        IsNewLoading = false;
                                    });
                                }else if (VATDeclarationData.data.ReviewNaMsg == "X")
                                {
                                    displayPopUpToSubmitOrCancelApplication();
                                }
                            }
                        }
                    }
                    else
                    {
                        List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                        HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                        NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                        headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                        headerAmountInfo.IsLinkAvailable = false;
                        headerAmountInfo.Message = AppResources.ZZZZPleaseAgreeTandCMsg;

                        headerWithInfos.Add(headerAmountInfo);


                        newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                        newDesignPopUp.HeaderWithInfos = headerWithInfos;
                        newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                        await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
                    }

                }
                else if (CreditDetailsText == AppResources.ZZZZGetAckNew)
                {
                    _navigationService.NavigateTo(App.VATReturnSuccessfullPageView, VATDeclarationData);
                }
            });

            onCountinueClicked = new Command(() =>
            {




            });

            OnBackStepClicked = new Command(() =>
            {
                switch (currentTab)
                {
                    case VATReturnUpdatedUITabEnum.VATReturns:
                        currentTab = VATReturnUpdatedUITabEnum.Instrunction;
                        break;

                    case VATReturnUpdatedUITabEnum.Sales:
                        currentTab = VATReturnUpdatedUITabEnum.VATReturns;

                        break;

                    case VATReturnUpdatedUITabEnum.Purchase:
                        currentTab = VATReturnUpdatedUITabEnum.Sales;
                        break;

                    case VATReturnUpdatedUITabEnum.TotalVat:
                        currentTab = VATReturnUpdatedUITabEnum.Purchase;
                        break;

                    case VATReturnUpdatedUITabEnum.Summery:
                        currentTab = VATReturnUpdatedUITabEnum.TotalVat;
                        break;
                }



            });
        }

        public async void displayPopUpToSubmitOrCancelApplication()
        {
            await MopupService.Instance.PushAsync(new VatReturnNewYesCancelPopUp(AppResources.CR2406PopUp));

        }

        public bool checkforrefundclicked()
        {
            bool result = false;
            if (App.ICRStatus == "E0045" || App.ICRStatus == "E0006")
            {
                if (VATDeclarationData.data.RefundFg == "1")
                {
                    result = true;
                }
                else if (IsAmendClicked == true)
                {
                    result = true;
                }
            }
            else if (App.ICRStatus == "E0001" || IsCheckedDraftMode())
            {
                result = true;
            }
            return result;
        }

        public bool IsCheckedDraftMode()
        {
            bool value = false;
            if (App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057")
            {
                value = true;
            }
            return value;
        }
        public async void VoidMsg()
        {

            List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
            HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
            NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
            headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
            headerAmountInfo.IsLinkAvailable = false;
            headerAmountInfo.Message = AppResources.ZZGeneralMessage_AllInfoFilledInTheFormWillBeLost;

            headerWithInfos.Add(headerAmountInfo);


            newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
            newDesignPopUp.HeaderWithInfos = headerWithInfos;
            newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

            await MopupService.Instance.PushAsync(new ShowVatInformationConfirmationPageView(newDesignPopUp));

        }

        #endregion
        public async Task VATSetReturnVoidAsync()
        {
            //var answer = await Application.Current.MainPage.DisplayAlert(AppResources.Information, AppResources.ZZGeneralMessage_AllInfoFilledInTheFormWillBeLost, AppResources.ZYes, AppResources.ZNo);
            //if (answer)
            //{
            await Task.Run(() =>
            {
                IsNewLoading = true;
            });
            await Task.Run(async () =>
            {
                try
                {
                    CreateDataForPost();
                    string operation = "04";// Passed 04 to set void
                    VATDeclarationData.data.Operationz = operation;
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
                    VATDeclarationData.data.StepNumber = StepNumber;
                    VATDeclarationData.data.UserTypz = "TP";
                    var response = WebServiceManager.GAZTSetVATReturnVoid(VATDeclarationData);
                    PopToRootPage();
                    var res = await SaveReturnAndGetReturnAndSetButtons();
                    if (res != null && res.data != null && response != null)
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                           {
                               ManageEnabledProperty(false);
                               IsVoidClicked = true;
                               // IsMainButtonVisible = false;

                           });
                        // ManageEnabledProperty(false);
                        MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        //  await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_VATReturnFormCancelled, AppResources.ZInstructions);

                        List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                        HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                        NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                        headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                        headerAmountInfo.IsLinkAvailable = false;
                        headerAmountInfo.Message = AppResources.ZZGeneralMessage_VATReturnFormCancelled;

                        headerWithInfos.Add(headerAmountInfo);


                        newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                        newDesignPopUp.HeaderWithInfos = headerWithInfos;
                        newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                        await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                    });
                    }
                    else
                    {
                        IsLoading = false;
                        if (string.IsNullOrEmpty(WebServiceManager.ErrorMessageForVAT))
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {


                                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                                headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                                headerAmountInfo.IsLinkAvailable = false;
                                headerAmountInfo.Message = AppResources.ZZSomethingwentwrong;

                                headerWithInfos.Add(headerAmountInfo);


                                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                                newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                                await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));



                                //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                _navigationService.GoBack();
                            });
                        }
                        else
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {

                                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                                headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                                headerAmountInfo.IsLinkAvailable = false;
                                headerAmountInfo.Message = WebServiceManager.ErrorMessageForVAT;

                                headerWithInfos.Add(headerAmountInfo);


                                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                                newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                                await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                                WebServiceManager.ErrorMessageForVAT = string.Empty;
                            });
                        }
                    }
                }
                catch (InternetException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    });
                }
            });
            await Task.Run(() =>
            {
                IsNewLoading = false;
            });
            //}
        }
        public async Task OnSaveDraftClicked()
        {
            try
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    IsNewLoading = true;
                });
                await Task.Run(async () =>
                {
                    CreateDataForPost();
                    string operation = "05";// Passed 05 to save the data as a draft
                    VATDeclarationData.data.Operationz = operation;
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
                    VATDeclarationData.data.StepNumber = StepNumber;
                    VATDeclarationData.data.StepNumberz = StepNumberz;
                    VATDeclarationData.data.UserTypz = "TP";
                    var res = await SaveReturnAndGetReturnAndSetButtons();
                    if (res != null && res.data != null)
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            if (currentTab == VATReturnUpdatedUITabEnum.Instrunction)
                            {
                                if (IsDeclarationCheckedForInstruction == false)
                                {
                                    IsMainButtonEnabled = false;
                                    IsRefundButtonEnabled = false;
                                }
                            }


                            List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                            HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                            NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                            headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                            headerAmountInfo.IsLinkAvailable = false;
                            headerAmountInfo.Message = string.Format(AppResources.DraftSaved, "  " + res.data.Fbnum);

                            headerWithInfos.Add(headerAmountInfo);


                            newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                            newDesignPopUp.HeaderWithInfos = headerWithInfos;
                            newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                            await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));




                            //await _dialogService.ShowMessage(string.Format(AppResources.DraftSaved, "  " + res.d.Fbnum), AppResources.Information);
                        });
                    }
                    else
                    {
                        IsLoading = false;
                        if (string.IsNullOrEmpty(WebServiceManager.ErrorMessageForVAT))
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                                headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                                headerAmountInfo.IsLinkAvailable = false;
                                headerAmountInfo.Message = AppResources.ZZSomethingwentwrong;

                                headerWithInfos.Add(headerAmountInfo);


                                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                                newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                                await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));



                            });
                        }
                        else
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                                headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                                headerAmountInfo.IsLinkAvailable = false;
                                headerAmountInfo.Message = WebServiceManager.ErrorMessageForVAT;

                                headerWithInfos.Add(headerAmountInfo);


                                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                                newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                                await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                                WebServiceManager.ErrorMessageForVAT = string.Empty;
                            });
                        }
                        //MainThread.BeginInvokeOnMainThread(async () =>
                        //{
                        //    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        //});
                    }
                });
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    IsNewLoading = false;
                });
            }
            catch (Exception ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {

                    List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                    HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                    NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                    headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                    headerAmountInfo.IsLinkAvailable = false;
                    headerAmountInfo.Message = AppResources.ZZSomethingwentwrong;

                    headerWithInfos.Add(headerAmountInfo);


                    newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                    newDesignPopUp.HeaderWithInfos = headerWithInfos;
                    newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                    await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));



                    //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                });
            }
        }


        public async Task VATReturnResetAsync(bool showPopupMsg = true)
        {
            await Task.Run(() =>
            {
                IsNewLoading = true;
            });
            await Task.Run(async () =>
            {
                try
                {
                    CreateDataForPost();
                    string operation = "14";// Passed 14 to set RESET
                    VATDeclarationData.data.Operationz = operation;
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
                    VATDeclarationData.data.StepNumber = StepNumber;
                    VATDeclarationData.data.UserTypz = "TP";
                    var response = WebServiceManager.GAZTSetVATReturnReset(VATDeclarationData);
                    PopToRootPage();
                    var res = await SaveReturnAndGetReturnAndSetButtons();



                    if (res != null && res.data != null && response != null)
                    {

                        VATDeclaration _vATDeclaration = await WebServiceManager.GAZTGetVATReturns(App.VATDeclrationFbguid, VATDeclarationData.data.Fbnumz, App.EUser, "");
                        PopToRootPage();
                        if (_vATDeclaration != null && _vATDeclaration.data != null)
                        {
                            VATDeclarationData = _vATDeclaration;
                            ResponseVATDeclarationD = VATDeclarationData.data;

                            SetCommasforAll();
                            if (DummyATTACHSetsList != null && DummyATTACHSetsList.Count() != 0)
                            {
                                VATDeclarationData.data.ATTACHSet = DummyATTACHSetsList;
                            }
                            //SetData();
                        }


                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            ManageEnabledProperty(false);
                            IsResetClicked = true;
                            //IsMainButtonVisible = false;

                        });
                        if (showPopupMsg)
                            MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                            HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                            NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                            headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                            headerAmountInfo.IsLinkAvailable = false;
                            headerAmountInfo.Message = AppResources.ZZGeneralMessage_ReturnRestoredToTheLastBilledVersion;

                            headerWithInfos.Add(headerAmountInfo);


                            newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                            newDesignPopUp.HeaderWithInfos = headerWithInfos;
                            newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                            await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                        });
                    }
                    else
                    {
                        IsLoading = false;
                        if (string.IsNullOrEmpty(WebServiceManager.ErrorMessageForVAT))
                        {
                            if (showPopupMsg)
                                MainThread.BeginInvokeOnMainThread(async () =>
                            {

                                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                                headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                                headerAmountInfo.IsLinkAvailable = false;
                                headerAmountInfo.Message = AppResources.ZZSomethingwentwrong;

                                headerWithInfos.Add(headerAmountInfo);


                                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                                newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                                await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));



                                //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                _navigationService.GoBack();
                            });
                        }
                        else
                        {
                            if (showPopupMsg)
                                MainThread.BeginInvokeOnMainThread(async () =>
                            {


                                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                                headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                                headerAmountInfo.IsLinkAvailable = false;
                                headerAmountInfo.Message = WebServiceManager.ErrorMessageForVAT;

                                headerWithInfos.Add(headerAmountInfo);


                                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                                newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                                await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));



                                // await _dialogService.ShowMessage(WebServiceManager.ErrorMessageForVAT, AppResources.Information);
                                // _navigationService.GoBack();
                                WebServiceManager.ErrorMessageForVAT = string.Empty;
                            });
                        }
                        //MainThread.BeginInvokeOnMainThread(async () =>
                        //{
                        //    _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        //});
                    }
                }
                catch (InternetException ex)
                {
                    if (showPopupMsg)
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {

                            List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                            HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                            NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                            headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                            headerAmountInfo.IsLinkAvailable = false;
                            headerAmountInfo.Message = ex.Message;

                            headerWithInfos.Add(headerAmountInfo);


                            newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                            newDesignPopUp.HeaderWithInfos = headerWithInfos;
                            newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                            await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));


                            // _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        });
                }
            });
            await Task.Run(() =>
            {
                IsNewLoading = false;
            });
        }

        public async Task VATReturnAmendAsync()
        {
            await Task.Run(() =>
            {
                IsNewLoading = true;
            });
            await Task.Run(async () =>
            {
                string periodto = JsonConvert.DeserializeObject<DateTime>(@"""" + VATDeclarationData.data.Abrzo + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                TimeSpan TS = DateTime.Now - Convert.ToDateTime(periodto);
                double Years = TS.TotalDays / 365.25;
                if (Years >= 5)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                        HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                        NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                        headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                        headerAmountInfo.IsLinkAvailable = false;
                        headerAmountInfo.Message = AppResources.ZZGeneralMessage_IfTimePeriodOfAmendmentIsLapsed;

                        headerWithInfos.Add(headerAmountInfo);


                        newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                        newDesignPopUp.HeaderWithInfos = headerWithInfos;
                        newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                        await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));



                        // await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_IfTimePeriodOfAmendmentIsLapsed, AppResources.Information);
                    });
                    return;
                }
                CreateDataForPost();
                string operation = "45";// Passed 45 to set for Amendment
                VATDeclarationData.data.Operationz = operation;
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
                VATDeclarationData.data.StepNumber = StepNumber;
                VATDeclarationData.data.UserTypz = "TP";
                // var response = WebServiceManager.GAZTSetVATReturnAmend(VATDeclarationData);
                PopToRootPage();
                var res = await SaveReturnAndGetReturnAndSetButtons();
                if (res != null && res.data != null)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        ManageEnabledProperty(true);
                        IsMainButtonVisible = true;
                        if ((App.ICRStatus == "E0045" || App.ICRStatus == "E0006") && VATDeclarationData.data.Yesno == "X")
                        {
                            IsEnableSwitchToggledFor15PercentChange = false;
                        }
                        else
                        {
                            IsEnableSwitchToggledFor15PercentChange = true;
                        }

                        if ((App.ICRStatus == "E0045" || App.ICRStatus == "E0006") && VATDeclarationData.data.GovsupYesno == "X")
                        {
                            IsEnableSwitchToggledFor15PercentChange = false;

                        }
                        else
                        {
                            IsEnableSwitchToggledFor15PercentChange = true;
                        }
                        IsDeclarationCheckEnabled = false;
                        IsTaxPayerCheckEnabled = false;
                        IsAmendClicked = true;
                        IsMainButtonEnabled = true;
                        IsMainButtonVisible = true;
                        ManageButtonsNameOnViewModel();
                    });
                    //ManageEnabledProperty(true);
                }
                else
                {
                    IsLoading = false;
                    if (string.IsNullOrEmpty(WebServiceManager.ErrorMessageForVAT))
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {


                            List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                            HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                            NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                            headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                            headerAmountInfo.IsLinkAvailable = false;
                            headerAmountInfo.Message = AppResources.ZZSomethingwentwrong;

                            headerWithInfos.Add(headerAmountInfo);


                            newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                            newDesignPopUp.HeaderWithInfos = headerWithInfos;
                            newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                            await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));




                            // await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                            _navigationService.GoBack();
                        });
                    }
                    else
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {



                            List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                            HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                            NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                            headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                            headerAmountInfo.IsLinkAvailable = false;
                            headerAmountInfo.Message = WebServiceManager.ErrorMessageForVAT;

                            headerWithInfos.Add(headerAmountInfo);


                            newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                            newDesignPopUp.HeaderWithInfos = headerWithInfos;
                            newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                            await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));



                            //await _dialogService.ShowMessage(WebServiceManager.ErrorMessageForVAT, AppResources.Information);
                            // _navigationService.GoBack();
                            WebServiceManager.ErrorMessageForVAT = string.Empty;
                        });
                    }
                    //MainThread.BeginInvokeOnMainThread(async () =>
                    //{
                    //    _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    //});
                }
            });
            await Task.Run(() =>
            {
                IsNewLoading = false;
            });
        }

        public void ManageButtonsNameOnViewModel()
        {
            if (currentTab == VATReturnUpdatedUITabEnum.Instrunction)
            {
                isBtnVisible = false;
                IsMainButtonVisible = true;
                IsRefundButtonVisible = false;
                IsCreditForwardBtnVisible = false;
                IsVisibleAmendButton = false;
                IsPayNowVisible = false;
                ContinueText = AppResources.ZZZZContinue;
            }
            //else if(viewModel.currentTab == VATReturnUpdatedUITabEnum.TaxpayerDetails)
            //{
            //    viewModel.isBtnVisible = false;
            //    viewModel.IsMainButtonVisible = true;
            //    viewModel.ContinueText = AppResources.ZZZZContinue; 
            //}
            else if (currentTab == VATReturnUpdatedUITabEnum.VATReturns)
            {
                isBtnVisible = false;
                IsMainButtonVisible = true;
                IsRefundButtonVisible = false;
                IsCreditForwardBtnVisible = false;
                IsVisibleAmendButton = false;
                IsPayNowVisible = false;
                ContinueText = AppResources.ZZZZContinue;

                if (IsAmendClicked)
                {

                    if (VATDeclarationData.data.GovsupYesno == "X")
                    {

                        IsTaxAmendGrid = false;
                      
                    }
                    else
                    {

                        IsTaxAmendGrid = true;
                       


                    }
                }
                else
                {
                    IsTaxAmendGrid = true;
                }

                if (IsAmendClicked)
                {

                    if (VATDeclarationData.data.Yesno == "X")
                    {

                        IsFivePercentTaxAmendGrid = false;

                    }
                    else
                    {

                        IsFivePercentTaxAmendGrid = true;


                    }
                }
                else
                {
                    IsFivePercentTaxAmendGrid = true;
                }
            }
            else if (currentTab == VATReturnUpdatedUITabEnum.Sales)
            {
                isBtnVisible = false;
                IsMainButtonVisible = true;
                IsRefundButtonVisible = false;
                IsCreditForwardBtnVisible = false;
                IsVisibleAmendButton = false;
                IsPayNowVisible = false;
                ContinueText = AppResources.ZZZZContinue;


                
               
            }
            else if (currentTab == VATReturnUpdatedUITabEnum.Purchase)
            {
                isBtnVisible = false;
                IsMainButtonVisible = true;
                IsRefundButtonVisible = false;
                IsCreditForwardBtnVisible = false;
                IsVisibleAmendButton = false;
                IsPayNowVisible = false;
                ContinueText = AppResources.ZZZZContinue;
            }
            else if (currentTab == VATReturnUpdatedUITabEnum.TotalVat)
            {
                isBtnVisible = false;
                IsMainButtonVisible = true;
                IsRefundButtonVisible = false;
                IsCreditForwardBtnVisible = true;
                IsVisibleAmendButton = false;
                IsPayNowVisible = false;
                ContinueText = AppResources.ZZZZContinue;
            }
            else if (currentTab == VATReturnUpdatedUITabEnum.Summery)
            {
                IsCreditForwardBtnVisible = false;
                //if (Convert.ToDouble(NetdueVat) > 0)
                //{
                //    IsPayNowVisible = true;
                //}
                //else
                //{
                //    IsPayNowVisible = false;
                //}

                if (IsAmendButtonAvailable)
                {
                    IsVisibleAmendButton = true;
                }
                else
                {
                    IsVisibleAmendButton = false;
                }
                if ((App.ICRStatus == "E0045" || App.ICRStatus == "E0006" || App.ICRStatus == "E0055" || App.ICRStatus == "E0058") && IsAmendClicked == false)
                {
                    isBtnVisible = true;
                    IsMainButtonVisible = false;
                    CreditDetailsText = AppResources.ZZZZGetAckNew;
                    if (VATDeclarationData.data.RefundFg == "1")
                    {
                        IsRefundButtonVisible = true;
                    }
                    else if (IsAmendClicked == true || VATDeclarationData.data.RefundFg != "1")
                    {
                        IsRefundButtonVisible = false;
                    }
                }
                else
                {
                    if (IsNavigatedToSubmitted == false)
                    {
                        if (Convert.ToDouble(NetdueVat) < 0)
                        {
                            isBtnVisible = false;
                            IsMainButtonVisible = true;
                            if ((App.ICRStatus == "E0045" || App.ICRStatus == "E0006" || App.ICRStatus == "E0055" || App.ICRStatus == "E0058") && IsAmendClicked == true && VATDeclarationData.data.RefundFg != "1")
                            {
                                IsRefundButtonVisible = false;
                            }
                            else
                            {
                                IsRefundButtonVisible = true;
                            }
                            ContinueText = AppResources.ZZZZConfirmAndCarryForward;
                        }
                        else
                        {
                            isBtnVisible = true;
                            IsMainButtonVisible = false;
                            IsRefundButtonVisible = false;
                            CreditDetailsText = AppResources.Submit;
                            IsPayNowVisible = false;
                        }
                    }
                    else
                    {
                        isBtnVisible = false;
                        IsMainButtonVisible = false;
                        IsRefundButtonVisible = false;
                    }
                }
                if (isReturnsInAmendsVisible)
                {
                    if ((Convert.ToDecimal(TotaldueVat) + Convert.ToDecimal(Preperiodcorr) < 0))
                    {
                        IsRefundButtonVisible = true;
                    }
                    else
                    {
                        IsRefundButtonVisible = false;
                    }
                }
            }
        }

        public void InstrunctionsCommand()
        {

        }

        public void TaxpayerDetailsCommand()
        {

        }
        public void VatReturnCommand()
        {
        }
        public void VatSalesCommand()
        {
        }
        public void VatPurchaseCommand()
        {
        }
        public void VatTotalAmountCommand()
        {
        }

        public void SummaryCommand()
        {
        }


        public async Task pageLoad()
        {
            try
            {
                WebServiceManager.ErrorMessageForVAT = string.Empty;
                //await Task.Run(() =>
                //{
                //    IsLoading = true;
                //});
                //await Task.Run(async () =>
                //{
                VATCalculationData vATCalculationData;
                string periodKey = VATDeclarationData.data.Persl;
                string TxnTp = VATDeclarationData.data.TxnTpz;
                string status = VATDeclarationData.data.Statusz;
                if (status == "E057" || status == "E0057" || status == "E058" || status == "E0058")
                {
                    //MainThread.BeginInvokeOnMainThread(async () =>
                    //{
                    //    await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_ReturnUnderReviewWithGAZT, AppResources.Information);
                    //});

                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_ReturnUnderReviewWithGAZT));
                }
                string FormBundleNumber = VATDeclarationData.data.Fbnum;
                string Gpart = VATDeclarationData.data.Gpart;


                TaxpayerPeriodFromDate = JsonConvert.DeserializeObject<DateTime>(@"""" + VATDeclarationData.data.Abrzu + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                TaxpayerPeriodToDate = JsonConvert.DeserializeObject<DateTime>(@"""" + VATDeclarationData.data.Abrzo + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                //if (App.IsArabic)
                //{
                //    periodfrom = JsonConvert.DeserializeObject<DateTime>(@"""" + VATDeclarationData.d.Abrzu + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                //    periodto = JsonConvert.DeserializeObject<DateTime>(@"""" + VATDeclarationData.d.Abrzo + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                //    TaxpayerPeriodFromDate = UtilityManager.ToArabicDate(periodfrom);
                //    TaxpayerPeriodToDate = UtilityManager.ToArabicDate(periodto);
                //}
                //else
                //{
                //    TaxpayerPeriodFromDate = JsonConvert.DeserializeObject<DateTime>(@"""" + VATDeclarationData.d.Abrzu + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                //    TaxpayerPeriodToDate = JsonConvert.DeserializeObject<DateTime>(@"""" + VATDeclarationData.d.Abrzo + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                //}
                if (VATDeclarationData.data.ATTACHSet != null && VATDeclarationData.data.ATTACHSet.Count != 0)
                {
                    ATTACHSetsList = new List<Attachment>();
                    foreach (var item in VATDeclarationData.data.ATTACHSet)
                    {
                        Attachment a = new Attachment();
                        a = item;
                        ATTACHSetsList.Add(a);
                    }
                }
                if (VATDeclarationData.data.CFSet != null && VATDeclarationData.data.ADRSet.Count != 0)
                {
                    CreditCarriedsList = VATDeclarationData.data.CFSet;
                }
                if (VATDeclarationData.data.ADRSet.Count > 0)
                {
                    FullAddress = VATDeclarationData.data.ADRSet[0].BuildingNo + " " + VATDeclarationData.data.ADRSet[0].Street + " " + VATDeclarationData.data.ADRSet[0].Quarter + " " + VATDeclarationData.data.ADRSet[0].RegionDesc + " " + VATDeclarationData.data.ADRSet[0].City + " " + Environment.NewLine + VATDeclarationData.data.ADRSet[0].PostalCd;
                }
                vATCalculationData = await WebServiceManager.GAZTGetVATDeclaratinCalculationData(periodKey, TxnTp, status, FormBundleNumber, Gpart);
                PopToRootPage();
                if (vATCalculationData.d != null)
                {
                    //      if(vATCalculationData.d.)
                    if (vATCalculationData.d.VATRSet.Count != 0)
                    {
                        CalculationRateSet = new List<VATCalculationDataVATRSet>();
                        CalculationRateSet = vATCalculationData.d.VATRSet;
                        CalculationRateSetVTTH = new List<VTTHSetResult>();
                        CalculationRateSetVTTH = vATCalculationData.d.VTTHSet;
                        CalculationRateIGRTSet = new List<IGRTSetResult>();
                        CalculationRateIGRTSet = vATCalculationData.d.IGRTSet;
                        RateSetAsPerDate();
                    }
                    if (vATCalculationData.d.VTTHSet.Count != 0)
                    {
                        CorrectionPeriodAmount = vATCalculationData.d.VTTHSet.Where(x => x.Type == "001").Select(x => x.MaxVal).FirstOrDefault();
                        CorrectionNegativePeriodAmount = vATCalculationData.d.VTTHSet.Where(x => x.Type == "001").Select(x => x.MinVal).FirstOrDefault();
                        if (!string.IsNullOrEmpty(CorrectionPeriodAmount))
                        {
                            if (App.IsArabic)
                            {
                                CarriedValueString = AppResources.ZVatCorrectionsfrompreviousperiod.Replace("±", CorrectionPeriodAmount + " ± ");
                                CarriedValueStringNew = AppResources.ZVatCorrectionsfrompreviousperiod1.Replace("±", CorrectionPeriodAmount + " ± ");
                            }
                            else
                            {
                                CarriedValueString = AppResources.ZVatCorrectionsfrompreviousperiod.Replace("±", " ± " + CorrectionPeriodAmount);
                                CarriedValueStringNew = AppResources.ZVatCorrectionsfrompreviousperiod1.Replace("±", " ± " + CorrectionPeriodAmount);
                            }
                        }
                    }
                }
                if (VATDeclarationData.data.TcFg == "1")
                {

                    IsDeclarationCheckedForInstruction = true;

                }
                if (VATDeclarationData.data.ConfStp2 == "1")
                {

                    IsCheckedTaxPayerDetailsInfo = true;

                }
                if (VATDeclarationData.data.DecFg == "1")
                {

                    IsDeclarationCheckedForSummary = true;

                }


                ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(VATDeclarationData.data.ATTACHSet as List<Attachment>);
                VatAttachmentsList = myCollection;
                if (VATDeclarationData != null)
                {
                    // SetPageForDraft();
                    if (VATDeclarationData.data != null)
                    {
                        ResponseVATDeclarationD = VATDeclarationData.data;
                        if (ResponseVATDeclarationD.GoliveFg == "X")
                        {
                            VATNewModelFor15Percent = VATDeclarationData.data.VATPERITEMSet.Where(x => x.Type == "002").FirstOrDefault();
                            VATNewModelFor5Percent = VATDeclarationData.data.VATPERITEMSet.Where(x => x.Type == "003").FirstOrDefault();
                        }
                        SetData();
                        SetCommasforAll();
                    }
                    if (VATDeclarationData.data.NOTESSet != null && VATDeclarationData.data.NOTESSet.Count() != 0)
                    {
                        ResponseNote = VATDeclarationData.data.NOTESSet;
                    }
                    if (VATDeclarationData.data.VATR_MSGSet != null && VATDeclarationData.data.VATR_MSGSet.Count() != 0)
                    {
                        Responseobject = VATDeclarationData.data.VATR_MSGSet;
                    }
                    if (VATDeclarationData.data.IBANSet != null && VATDeclarationData.data.IBANSet.Count() != 0)
                    {
                        ResponseIBANSET = VATDeclarationData.data.IBANSet;
                    }
                    if (VATDeclarationData.data.CFSet != null && VATDeclarationData.data.CFSet.Count() != 0)
                    {
                        ResponseCFSET = VATDeclarationData.data.CFSet;
                    }
                    if (VATDeclarationData.data.ATTACHSet != null && VATDeclarationData.data.ATTACHSet.Count() != 0)
                    {
                        ResponseAttachSet = VATDeclarationData.data.ATTACHSet;
                    }
                    if (VATDeclarationData.data.ADRSet != null && VATDeclarationData.data.ADRSet.Count() != 0)
                    {
                        ResponseAddressSET = VATDeclarationData.data.ADRSet;
                    }
                }

                ManageThePreperiodcorrSwitch();
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }
        }

        public void SetEnableForSubmitButton()
        {
            if (IsDeclarationCheckedForSummary == true)
            {
                IsGetSadadNumberEnabled = true;
            }
            else
            {
                IsGetSadadNumberEnabled = false;
            }
        }

        public void SetDataForRefundPopup()
        {
            CreateDataForPost();
            if (IsYesChecked == true)
            {
                VATDeclarationData.data.Yesno = "X";
            }
            else
            {
                VATDeclarationData.data.Yesno = string.Empty;
            }

            if (IsTaxYesChecked == true)
            {
                VATDeclarationData.data.GovsupYesno = "X";
            }
            else
            {
                VATDeclarationData.data.GovsupYesno = string.Empty;
            }


            if (IsDeclarationCheckedForSummary == true)
            {
                VATDeclarationData.data.DecFg = "1";
            }
            else
            {
                VATDeclarationData.data.DecFg = "0";
            }
        }

        private async Task<VATDeclaration> SaveReturnAndGetReturnAndSetButtons()
        {
            try
            {
                //New code for VAT 15% Change

                if (IsYesChecked == true)
                {
                    VATDeclarationData.data.Yesno = "X";
                }
                else
                {
                    VATDeclarationData.data.Yesno = string.Empty;
                }
                if (IsTaxYesChecked == true)
                {
                    VATDeclarationData.data.GovsupYesno = "X";
                }
                else
                {
                    VATDeclarationData.data.GovsupYesno = string.Empty;
                }



                if (IsDeclarationCheckedForSummary == true)
                {
                    VATDeclarationData.data.DecFg = "1";
                }
                else
                {
                    VATDeclarationData.data.DecFg = "0";
                }
                //if (IschkRefundDeclaration)
                //{
                //    VATDeclarationData.d.TcFlg = "1";
                //}
                //else
                //{
                //    VATDeclarationData.d.TcFlg = "0";
                //}
                //if (IsCheckedRefund)
                //{
                //    VATDeclarationData.d.IbanCb = "1";
                //}
                //else
                //{
                //    VATDeclarationData.d.IbanCb = "0";
                //}
                if (VATDeclarationData != null && VATDeclarationData.data != null && VATDeclarationData.data.ATTACHSet.Count() != 0)
                {
                    DummyATTACHSetsList = new List<Attachment>();   
                    DummyATTACHSetsList = VATDeclarationData.data.ATTACHSet;
                }
                if (ATTACHSetsList != null && ATTACHSetsList.Count() != 0)
                {
                    VATDeclarationData.data.ATTACHSet = ATTACHSetsList;
                }
                

                VATDeclaration response = await WebServiceManager.SaveVATDeclarationData(VATDeclarationData);
                PopToRootPage();
                if (response != null && response.data1 != null && !string.IsNullOrEmpty(response.data1.Fbnumz))
                {
                    try
                    {
                        if (response != null && response.data1 != null)
                        {
                            VATDeclarationData.data = response.data1;
                            ResponseVATDeclarationD = VATDeclarationData.data;
                            if (VATDeclarationData.data.VATPERITEMSet != null)
                            {
                                if (VATDeclarationData.data.GoliveFg == "X")
                                {
                                    if (VATDeclarationData.data.Yesno == "X")
                                    {
                                        VATNewModelFor15Percent = VATDeclarationData.data.VATPERITEMSet.Where(x => x.Type == "002").FirstOrDefault();
                                        VATNewModelFor5Percent = VATDeclarationData.data.VATPERITEMSet.Where(x => x.Type == "003").FirstOrDefault();
                                    }
                                    else
                                    {
                                        VATNewModelFor15Percent = VATDeclarationData.data.VATPERITEMSet.Where(x => x.Type == "002").FirstOrDefault();
                                    }

                                    
                                }
                            }
                            SetCommasforAll();
                            if (DummyATTACHSetsList != null && DummyATTACHSetsList.Count() != 0)
                            {
                                VATDeclarationData.data.ATTACHSet = DummyATTACHSetsList;
                            }
                            if (VATDeclarationData.data.Operationz == "01" && App.ICRStatus == "E0001")
                            {
                                App.ICRStatus = "E0013";
                            }
                            //VATDeclaration _vATDeclaration = await WebServiceManager.GAZTGetVATReturns(VATDeclarationData.d.ReturnIdz, VATDeclarationData.d.Fbnumz, ICRListPageViewModel.EUser,"");
                            //if (_vATDeclaration != null && _vATDeclaration.d != null)
                            //{
                            //    VATDeclarationData = _vATDeclaration;
                            //    ResponseVATDeclarationD = VATDeclarationData.d;
                            //    SetData();
                            //}
                            MainThread.BeginInvokeOnMainThread(() =>
                            {
                                ManageEnabledProperty(true);
                            });
                        }
                        await SetButtons(VATDeclarationData);
                        return response;
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.ToString());
                        Console.Write(ex.StackTrace.ToString());
                        return null;
                    }
                }
                return response;
            }
            catch (InternetException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }
        public async Task SubmitClicked()
        {
            try
            {
                bool Result = await FirstCall();
                if (Result)
                {

                    await Task.Delay(7000);

                    if (string.IsNullOrEmpty(VATDeclarationData.data.Fbnumz))
                    {

                        VATDeclaration _vATDeclarationForGet = await WebServiceManager.GAZTGetVATReturns(App.Fbguid, VATDeclarationData.data.Fbnumz, App.EUser, "");
                        PopToRootPage();
                        if (_vATDeclarationForGet != null && _vATDeclarationForGet.data != null)
                        {
                            if (!string.IsNullOrEmpty(_vATDeclarationForGet.data.Fbnumz))
                            {
                                //
                            }
                            else
                            {
                                MainThread.BeginInvokeOnMainThread(async () =>
                                {
                                    List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                                    HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                                    NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                                    headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                                    headerAmountInfo.IsLinkAvailable = false;
                                    headerAmountInfo.Message = AppResources.ZZSomethingwentwrong;

                                    headerWithInfos.Add(headerAmountInfo);


                                    newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                                    newDesignPopUp.HeaderWithInfos = headerWithInfos;
                                    newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                                    await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));


                                    //  await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);




                                    _navigationService.GoBack();
                                });
                            }
                        }
                        else
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {

                                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                                headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                                headerAmountInfo.IsLinkAvailable = false;
                                headerAmountInfo.Message = AppResources.ZZSomethingwentwrong;

                                headerWithInfos.Add(headerAmountInfo);


                                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                                newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                                await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));


                                // await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                _navigationService.GoBack();
                            });
                        }
                    }

                    CreateDataForPost();
                    string operation = "01";// Passed operation "01" to submit the VAT Declaration Data
                                            //   VATDeclarationData.d.StepNumberz = "04";
                                            //VATDeclarationData.d.StepNumber = "00";
                                            // VATDeclarationData.d.Fbguid = string.Empty;
                    VATDeclarationData.data.StepNumberz = "04";
                    VATDeclarationData.data.UserTypz = "TP";
                    VATDeclarationData.data.Operationz = operation;
                    var res = await SaveReturnAndGetReturnAndSetButtons();
                    if (res != null && res.data1 != null)
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            ManageEnabledProperty(false);
                            IsGetSadadNumberEnabled = false;
                            IsMainButtonEnabled = false;
                            IsMainButtonVisible = false;
                            IsRefundButtonEnabled = false;
                            IsRefundButtonVisible = false;
                            isBtnVisible = false;
                            IsEnableSwitchToggledFor15PercentChange = false;
                            IsNavigatedToSubmitted = true;
                        });
                        if (App.ICRStatus == "E0045" || App.ICRStatus == "E0056")
                        {
                            await Task.Delay(5000);
                        }
                        _navigationService.NavigateTo(App.VATReturnSuccessfullPageView, VATDeclarationData);
                        // _navigationService.NavigateTo(App.AcknowledgementDetailsPageView, VATDeclarationData);
                    }
                    else
                    {
                        IsLoading = false;
                        if (string.IsNullOrEmpty(WebServiceManager.ErrorMessageForVAT))
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {

                                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                                headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                                headerAmountInfo.IsLinkAvailable = false;
                                headerAmountInfo.Message = AppResources.ZZSomethingwentwrong;

                                headerWithInfos.Add(headerAmountInfo);


                                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                                newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                                await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));


                                // await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                _navigationService.GoBack();
                            });
                        }
                        else
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                                headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                                headerAmountInfo.IsLinkAvailable = false;
                                headerAmountInfo.Message = WebServiceManager.ErrorMessageForVAT;

                                headerWithInfos.Add(headerAmountInfo);


                                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                                newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;
                                VATReturnResetAsync(false);
                                await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));


                                WebServiceManager.ErrorMessageForVAT = string.Empty;
                            });
                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }

        public async Task<bool> FirstCall()
        {
            bool result = false;
            try
            {
                VATDeclaration resNew = null;
                //if (String.IsNullOrEmpty(VATDeclarationData.d.Fbnum) || App.ICRStatus == "E0045")
                //{
                CreateDataForPost();
                string operation = "01";
                VATDeclarationData.data.StepNumber = "04";
                VATDeclarationData.data.StepNumberz = "04";
                VATDeclarationData.data.UserTypz = "TP";
                VATDeclarationData.data.Operationz = operation;
                VATDeclaration response = new VATDeclaration();
                resNew = await SaveReturnAndGetReturnAndSetButtons();
                // }
                if (resNew != null && resNew.data1 != null)
                {
                    VATDeclarationDataDummy = resNew;
                    decimal FourteenA = 0;
                    if (!string.IsNullOrEmpty(TotaldueVat) && !string.IsNullOrEmpty(Preperiodcorr))
                    {
                        FourteenA = Convert.ToDecimal(TotaldueVat) + Convert.ToDecimal(Preperiodcorr);
                    }
                    if (IsSwichButtonEnable == false && FourteenA < 5000 && Convert.ToDecimal(NetdueVat) < 0 || IsSwichButtonEnable == true && FourteenA < 100000 && Convert.ToDecimal(CreditVat) > 0)
                    {


                        //List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                        //HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                        //NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                        //headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                        //headerAmountInfo.IsLinkAvailable = false;

                        //StringBuilder Masseges = new StringBuilder();
                        //Masseges.Append(AppResources.Pleasereviewthecalculationandsubmitagain);
                        //Masseges.Append(Environment.NewLine);
                        //Masseges.Append(Environment.NewLine);
                        //Masseges.Append(Environment.NewLine);
                        //Masseges.Append(AppResources.CreditReturnMsg);
                        //headerAmountInfo.IsLinkAvailable = false;
                        //headerAmountInfo.IsRed = "#e84941";
                        //headerAmountInfo.IsBold = "Bold";
                        //headerAmountInfo.Message = Masseges.ToString();

                        //headerWithInfos.Add(headerAmountInfo);


                        //newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                        //newDesignPopUp.HeaderWithInfos = headerWithInfos;
                        //newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                        //MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                        result = true;

                    }
                    else
                    {
                        if (resNew.data1.SubmitFg == "" || resNew.data1.SubmitFg == string.Empty)
                        {
                            MainThread.BeginInvokeOnMainThread(() =>
                            {
                                ManageEnabledProperty(false);
                                IsGetSadadNumberEnabled = false;
                                IsMainButtonEnabled = false;
                                IsMainButtonVisible = false;
                                IsRefundButtonEnabled = false;
                                IsRefundButtonVisible = false;
                                isBtnVisible = false;
                                IsEnableSwitchToggledFor15PercentChange = false;
                                IsNavigatedToSubmitted = true;
                                //IsMainButtonVisible = false;
                                //IsSwichButtonEnableToTap = false;
                                //IsEnableIBAN = false;
                                //IsEnableCheckedRefund = false;
                                //IsEnableIBANType = false;
                                //IsEnableIBANIdNumber = false;
                                //IsGetAcknowledgementClicked = true;
                                //IsMoreButtonEnabled = false;
                            });
                            if (App.ICRStatus == "E0045" || App.ICRStatus == "E0056")
                            {
                                await Task.Delay(5000);
                            }
                            //ManageEnabledProperty(false);
                            _navigationService.NavigateTo(App.VATReturnSuccessfullPageView, VATDeclarationData.data);
                            //_navigationService.NavigateTo(App.AcknowledgementDetailsPageView, VATDeclarationData);
                        }
                        else
                        {
                            result = true;
                            // await _dialogService.ShowMessage(AppResources.Pleasereviewthecalculationandsubmitagain, AppResources.Information);
                            //VATReturnFormClicked();
                            //SelectedIndex = 2;
                            //PageSelectedItem = VatTabbledPageList[2];
                        }
                    }
                    //await _dialogService.ShowMessage(AppResources.Pleasereviewthecalculationandsubmitagain, AppResources.Information);
                    //VATReturnFormClicked();
                    //PageSelectedItem = VatTabbledPageList[2];
                }
                else
                {
                    result = false;
                    IsLoading = false;
                    if (string.IsNullOrEmpty(WebServiceManager.ErrorMessageForVAT))
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                            HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                            NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                            headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                            headerAmountInfo.IsLinkAvailable = false;
                            headerAmountInfo.Message = AppResources.ZZSomethingwentwrong;

                            headerWithInfos.Add(headerAmountInfo);


                            newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                            newDesignPopUp.HeaderWithInfos = headerWithInfos;
                            newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                            await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));


                            //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                            _navigationService.GoBack();
                        });
                    }
                    else
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {

                            List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                            HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                            NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                            headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                            headerAmountInfo.IsLinkAvailable = false;
                            headerAmountInfo.Message = WebServiceManager.ErrorMessageForVAT;

                            headerWithInfos.Add(headerAmountInfo);


                            newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                            newDesignPopUp.HeaderWithInfos = headerWithInfos;
                            newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;
                            VATReturnResetAsync(false);
                            await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                            //await _dialogService.ShowMessage(WebServiceManager.ErrorMessageForVAT, AppResources.Information);
                            //_navigationService.GoBack();
                            WebServiceManager.ErrorMessageForVAT = string.Empty;
                        });
                    }
                    //  await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                }
                return result;
            }
            catch (Exception ex)
            {
                result = false;
                return result;
            }
        }

        public void CreateDataForPost()
        {
            try
            {
                Result6 VAT15percentmodel = VATNewModelFor15Percent;
                Result6 VAT5percentmodel = VATNewModelFor5Percent;
                VATDeclarationD vATDeclarationD = SetDataForPost(ResponseVATDeclarationD);
                vATDeclarationD = SetRemainingData(vATDeclarationD);

                //new Vat15% code
                if (IsFifteenPercentChange)
                {
                    vATDeclarationD = Set15PercentChangeData(vATDeclarationD);
                    vATDeclarationD = Set5PercentChangeData(vATDeclarationD);
                }

                VATDeclarationData.data.TotalsalesAmt = vATDeclarationD.TotalsalesAmt;
                VATDeclarationData.data.TotalsalesAdj = vATDeclarationD.TotalsalesAdj;
                VATDeclarationData.data.TotalpurchaseAmt = vATDeclarationD.TotalpurchaseAmt;
                VATDeclarationData.data.TotalpurchaseAdj = vATDeclarationD.TotalpurchaseAdj;
                VATDeclarationData.data.StdsalesVat = vATDeclarationD.StdsalesVat;
                VATDeclarationData.data.TotalsalesVat = vATDeclarationD.TotalsalesVat;
                VATDeclarationData.data.StdpurchasesVat = vATDeclarationD.StdpurchasesVat;
                VATDeclarationData.data.ImportspaidVat = vATDeclarationD.ImportspaidVat;
                VATDeclarationData.data.ImportsaccVat = vATDeclarationD.ImportsaccVat;
                VATDeclarationData.data.TotalpurchaseVat = vATDeclarationD.TotalpurchaseVat;
                VATDeclarationData.data.TotaldueVat = vATDeclarationD.TotaldueVat;
                VATDeclarationData.data.Preperiodcorr = vATDeclarationD.Preperiodcorr;
                VATDeclarationData.data.CreditVat = vATDeclarationD.CreditVat;
                VATDeclarationData.data.NetdueVat = vATDeclarationD.NetdueVat;
                //if (IsVisibleDropdownForRefund == true)
                //{
                //    VATDeclarationData.d.RefundFg = "1";
                //    if (IsCheckedRefund == true)
                //    {
                //        VATDeclarationData.d.Iban = IbanNumberText;
                //        VATDeclarationData.d.IbanCb = "1";
                //    }
                //    else
                //    {
                //        if (SelectedIBAN != null)
                //        {
                //            VATDeclarationData.d.Iban = SelectedIBAN.Iban;
                //            VATDeclarationData.d.IbanCb = "0";
                //        }
                //    }
                //    if (SelectedIBANType != null)
                //    {
                //        VATDeclarationData.d.Idtype = SelectedIBANType.key;
                //    }
                //    if (SelectedIBANIDNumber != null)
                //    {
                //        VATDeclarationData.d.Idnum = SelectedIBANIDNumber.Idnumber;
                //    }
                //}
                //else
                //{
                //    VATDeclarationData.d.RefundFg = "0";
                //}
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }
        public VATDeclarationD Set15PercentChangeData(VATDeclarationD vATDeclarationD)
        {
            try
            {
                if (VATNewModelFor15Percent != null && vATDeclarationD != null)
                {

                    foreach (var vat15model in vATDeclarationD.VATPERITEMSet)
                    {
                        if (vat15model.Type == "002")
                        {
                            vat15model.DataVersion = VATNewModelFor15Percent.DataVersion;
                            vat15model.FormGuid = VATNewModelFor15Percent.FormGuid;
                            vat15model.ImportsaccAdj = VATNewModelFor15Percent.ImportsaccAdj.Replace(",", "");
                            vat15model.ImportsaccAmt = VATNewModelFor15Percent.ImportsaccAmt.Replace(",", "");
                            vat15model.ImportsaccVat = ImportsaccVat15.Replace(",", "");
                            vat15model.ImportspaidAdj = VATNewModelFor15Percent.ImportspaidAdj.Replace(",", "");
                            vat15model.ImportspaidAmt = VATNewModelFor15Percent.ImportspaidAmt.Replace(",", "");
                            vat15model.ImportspaidVat = ImportspaidVat15.Replace(",", "");
                            vat15model.LineNo = VATNewModelFor15Percent.LineNo;
                            vat15model.RankingOrder = VATNewModelFor15Percent.RankingOrder;
                            vat15model.Rate = VATNewModelFor15Percent.Rate;
                            vat15model.ReturnId = VATNewModelFor15Percent.ReturnId;
                            vat15model.StdpurchaseAdj = VATNewModelFor15Percent.StdpurchaseAdj.Replace(",", "");
                            vat15model.StdpurchaseAmt = VATNewModelFor15Percent.StdpurchaseAmt.Replace(",", "");
                            vat15model.StdpurchasesVat = StdpurchasesVat15.Replace(",", "");
                            vat15model.StdsalesAdj = VATNewModelFor15Percent.StdsalesAdj.Replace(",", "");
                            vat15model.StdsalesAmt = VATNewModelFor15Percent.StdsalesAmt.Replace(",", "");
                            vat15model.GovsupsalesAmt = VATNewModelFor15Percent.GovsupsalesAmt.Replace(",", "");
                            vat15model.GovsupsalesAdj = VATNewModelFor15Percent.GovsupsalesAdj.Replace(",", "");
                            vat15model.StdsalesVat = StdsalesVat15.Replace(",", "");
                            vat15model.TimestampCh = VATNewModelFor15Percent.TimestampCh;
                            vat15model.TimestampCr = VATNewModelFor15Percent.TimestampCr;
                            vat15model.Type = VATNewModelFor15Percent.Type;
                            vat15model.Waers = VATNewModelFor15Percent.Waers;
                            vat15model.__metadata = VATNewModelFor15Percent.__metadata;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());

            }
            return vATDeclarationD;
        }

        public VATDeclarationD Set5PercentChangeData(VATDeclarationD vATDeclarationD)
        {
            try
            {
                if (VATNewModelFor5Percent != null && vATDeclarationD != null)
                {

                    foreach (var vat15model in vATDeclarationD.VATPERITEMSet)
                    {
                        if (vat15model.Type == "003")
                        {
                            vat15model.DataVersion = VATNewModelFor5Percent.DataVersion;
                            vat15model.FormGuid = VATNewModelFor5Percent.FormGuid;
                            vat15model.ImportsaccAdj = VATNewModelFor5Percent.ImportsaccAdj.Replace(",", "");
                            vat15model.ImportsaccAmt = VATNewModelFor5Percent.ImportsaccAmt.Replace(",", "");
                            vat15model.ImportsaccVat = ImportsaccVat5.Replace(",", "");
                            vat15model.ImportspaidAdj = VATNewModelFor5Percent.ImportspaidAdj.Replace(",", "");
                            vat15model.ImportspaidAmt = VATNewModelFor5Percent.ImportspaidAmt.Replace(",", "");
                            vat15model.ImportspaidVat = ImportspaidVat5.Replace(",", "");
                            vat15model.LineNo = VATNewModelFor5Percent.LineNo;
                            vat15model.RankingOrder = VATNewModelFor5Percent.RankingOrder;
                            vat15model.Rate = VATNewModelFor5Percent.Rate;
                            vat15model.ReturnId = VATNewModelFor5Percent.ReturnId;
                            vat15model.StdpurchaseAdj = VATNewModelFor5Percent.StdpurchaseAdj.Replace(",", "");
                            vat15model.StdpurchaseAmt = VATNewModelFor5Percent.StdpurchaseAmt.Replace(",", "");
                            vat15model.StdpurchasesVat = StdpurchasesVat5.Replace(",", "");
                            vat15model.StdsalesAdj = VATNewModelFor5Percent.StdsalesAdj.Replace(",", "");
                            vat15model.StdsalesAmt = VATNewModelFor5Percent.StdsalesAmt.Replace(",", "");
                            vat15model.StdsalesVat = StdsalesVat5.Replace(",", "");
                            vat15model.TimestampCh = VATNewModelFor5Percent.TimestampCh;
                            vat15model.TimestampCr = VATNewModelFor5Percent.TimestampCr;
                            vat15model.Type = VATNewModelFor5Percent.Type;
                            vat15model.Waers = VATNewModelFor5Percent.Waers;
                            vat15model.__metadata = VATNewModelFor5Percent.__metadata;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            return vATDeclarationD;
        }
        public VATDeclarationD SetRemainingData(VATDeclarationD vATDeclarationD)
        {
            if (!string.IsNullOrEmpty(vATDeclarationD.StdsalesAmt))
            {
                vATDeclarationD.StdsalesAmt = vATDeclarationD.StdsalesAmt.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.StdsalesAdj))
            {
                vATDeclarationD.StdsalesAdj = vATDeclarationD.StdsalesAdj.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.SalesGccAmt))
            {
                vATDeclarationD.SalesGccAmt = vATDeclarationD.SalesGccAmt.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.SalesGccAdj))
            {
                vATDeclarationD.SalesGccAdj = vATDeclarationD.SalesGccAdj.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.ZerosalesAmt))
            {
                vATDeclarationD.ZerosalesAmt = vATDeclarationD.ZerosalesAmt.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.ZerosalesAdj))
            {
                vATDeclarationD.ZerosalesAdj = vATDeclarationD.ZerosalesAdj.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.ExportsAmt))
            {
                vATDeclarationD.ExportsAmt = vATDeclarationD.ExportsAmt.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.ExportsAdj))
            {
                vATDeclarationD.ExportsAdj = vATDeclarationD.ExportsAdj.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.ExemptsalesAmt))
            {
                vATDeclarationD.ExemptsalesAmt = vATDeclarationD.ExemptsalesAmt.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.ExemptsalesAdj))
            {
                vATDeclarationD.ExemptsalesAdj = vATDeclarationD.ExemptsalesAdj.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.StdpurchaseAmt))
            {
                vATDeclarationD.StdpurchaseAmt = vATDeclarationD.StdpurchaseAmt.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.StdpurchaseAdj))
            {
                vATDeclarationD.StdpurchaseAdj = vATDeclarationD.StdpurchaseAdj.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.ImportspaidAmt))
            {
                vATDeclarationD.ImportspaidAmt = vATDeclarationD.ImportspaidAmt.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.ImportspaidAdj))
            {
                vATDeclarationD.ImportspaidAdj = vATDeclarationD.ImportspaidAdj.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.ImportsaccAmt))
            {
                vATDeclarationD.ImportsaccAmt = vATDeclarationD.ImportsaccAmt.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.ImportsaccAdj))
            {
                vATDeclarationD.ImportsaccAdj = vATDeclarationD.ImportsaccAdj.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.ZeropurchaseAmt))
            {
                vATDeclarationD.ZeropurchaseAmt = vATDeclarationD.ZeropurchaseAmt.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.ZeropurchaseAdj))
            {
                vATDeclarationD.ZeropurchaseAdj = vATDeclarationD.ZeropurchaseAdj.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.ExemptpurchaseAmt))
            {
                vATDeclarationD.ExemptpurchaseAmt = vATDeclarationD.ExemptpurchaseAmt.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.ExemptpurchaseAdj))
            {
                vATDeclarationD.ExemptpurchaseAdj = vATDeclarationD.ExemptpurchaseAdj.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.Preperiodcorr))
            {
                vATDeclarationD.Preperiodcorr = vATDeclarationD.Preperiodcorr.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.StdsalesAmt))
            {
                vATDeclarationD.StdsalesAmt = vATDeclarationD.StdsalesAmt.Replace(",", "");
            }
            return vATDeclarationD;
        }

        public VATDeclarationD SetDataForPost(VATDeclarationD vATDeclarationD)
        {
            try
            {
                if (!string.IsNullOrEmpty(TotalsalesAmt))
                {
                    vATDeclarationD.TotalsalesAmt = !TotalsalesAmt.Contains(",") ? TotalsalesAmt : TotalsalesAmt.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(TotalsalesAdj))
                {
                    vATDeclarationD.TotalsalesAdj = !TotalsalesAdj.Contains(",") ? TotalsalesAdj : TotalsalesAdj.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(TotalpurchaseAmt))
                {
                    vATDeclarationD.TotalpurchaseAmt = !TotalpurchaseAmt.Contains(",") ? TotalpurchaseAmt : TotalpurchaseAmt.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(TotalpurchaseAdj))
                {
                    vATDeclarationD.TotalpurchaseAdj = !TotalpurchaseAdj.Contains(",") ? TotalpurchaseAdj : TotalpurchaseAdj.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(StdsalesVat))
                {
                    vATDeclarationD.StdsalesVat = !StdsalesVat.Contains(",") ? StdsalesVat : StdsalesVat.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(TotalsalesVat))
                {
                    vATDeclarationD.TotalsalesVat = !TotalsalesVat.Contains(",") ? TotalsalesVat : TotalsalesVat.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(StdpurchasesVat))
                {
                    vATDeclarationD.StdpurchasesVat = !StdpurchasesVat.Contains(",") ? StdpurchasesVat : StdpurchasesVat.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(ImportspaidVat))
                {
                    vATDeclarationD.ImportspaidVat = !ImportspaidVat.Contains(",") ? ImportspaidVat : ImportspaidVat.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(ImportsaccVat))
                {
                    vATDeclarationD.ImportsaccVat = !ImportsaccVat.Contains(",") ? ImportsaccVat : ImportsaccVat.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(TotalpurchaseVat))
                {
                    vATDeclarationD.TotalpurchaseVat = !TotalpurchaseVat.Contains(",") ? TotalpurchaseVat : TotalpurchaseVat.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(TotaldueVat))
                {
                    vATDeclarationD.TotaldueVat = !TotaldueVat.Contains(",") ? TotaldueVat : TotaldueVat.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Preperiodcorr))
                {
                    vATDeclarationD.Preperiodcorr = !Preperiodcorr.Contains(",") ? Preperiodcorr : Preperiodcorr.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(CreditVat))
                {
                    vATDeclarationD.CreditVat = !CreditVat.Contains(",") ? CreditVat : CreditVat.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(NetdueVat))
                {
                    vATDeclarationD.NetdueVat = !NetdueVat.Contains(",") ? NetdueVat : NetdueVat.Replace(",", "");
                }




            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            return vATDeclarationD;
        }
        public void ManageEnabledProperty(bool value)
        {
            IsControlEnabledForEntry = !value;
            IsDeclarationCheckEnabled = value;
            IsTaxPayerCheckEnabled = value;
            IsSummaryCheckEnabled = value;
            // IsMainButtonEnabled = value;
            IsYesBoxEnabled = value;
            IsNoBoxEnabled = value;
            IsTaxNoBoxEnabled = value;
            IsTaxYesBoxEnabled = value;

            IsControlEnabled = value;
        }
        public void SetData()
        {
            TotalsalesAmt = ResponseVATDeclarationD.TotalsalesAmt;
            TotalsalesAdj = ResponseVATDeclarationD.TotalsalesAdj;
            TotalpurchaseAmt = ResponseVATDeclarationD.TotalpurchaseAmt;
            TotalpurchaseAdj = ResponseVATDeclarationD.TotalpurchaseAdj;
            StdsalesVat = ResponseVATDeclarationD.StdsalesVat;
            TotalsalesVat = ResponseVATDeclarationD.TotalsalesVat;
            StdpurchasesVat = ResponseVATDeclarationD.StdpurchasesVat;
            ImportspaidVat = ResponseVATDeclarationD.ImportspaidVat;
            ImportsaccVat = ResponseVATDeclarationD.ImportsaccVat;
            TotalpurchaseVat = ResponseVATDeclarationD.TotalpurchaseVat;
            TotaldueVat = ResponseVATDeclarationD.TotaldueVat;
            Preperiodcorr = ResponseVATDeclarationD.Preperiodcorr;
            CreditVat = ResponseVATDeclarationD.CreditVat;
            NetdueVat = ResponseVATDeclarationD.NetdueVat;



        }

        public void SetCommasforAll()
        {
            ResponseVATDeclarationD.StdsalesAmt = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.StdsalesAmt);
            ResponseVATDeclarationD.StdsalesAdj = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.StdsalesAdj);
            ResponseVATDeclarationD.SalesGccAmt = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.SalesGccAmt);
            ResponseVATDeclarationD.SalesGccAdj = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.SalesGccAdj);
            ResponseVATDeclarationD.ZerosalesAmt = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ZerosalesAmt);
            ResponseVATDeclarationD.ZerosalesAdj = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ZerosalesAdj);
            ResponseVATDeclarationD.ExportsAmt = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ExportsAmt);
            ResponseVATDeclarationD.ExportsAdj = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ExportsAdj);
            ResponseVATDeclarationD.ExemptsalesAmt = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ExemptsalesAmt);
            ResponseVATDeclarationD.ExemptsalesAdj = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ExemptsalesAdj);
            ResponseVATDeclarationD.StdpurchaseAmt = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.StdpurchaseAmt);
            ResponseVATDeclarationD.StdpurchaseAdj = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.StdpurchaseAdj);
            ResponseVATDeclarationD.ImportspaidAmt = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ImportspaidAmt);
            ResponseVATDeclarationD.ImportspaidAdj = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ImportspaidAdj);
            ResponseVATDeclarationD.ImportsaccAmt = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ImportsaccAmt);
            ResponseVATDeclarationD.ImportsaccAdj = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ImportsaccAdj);
            ResponseVATDeclarationD.ZeropurchaseAmt = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ZeropurchaseAmt);
            ResponseVATDeclarationD.ZeropurchaseAdj = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ZeropurchaseAdj);
            ResponseVATDeclarationD.ExemptpurchaseAmt = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ExemptpurchaseAmt);
            ResponseVATDeclarationD.ExemptpurchaseAdj = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ExemptpurchaseAdj);
            ResponseVATDeclarationD.Preperiodcorr = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.Preperiodcorr);

            //For New 15% Change
            SetCommasforNew15percentchange();
        }

        public void SetCommasforNew15percentchange()
        {
            if (VATDeclarationData.data.GoliveFg == "X")
            {
                VATNewModelFor15Percent = VATDeclarationData.data.VATPERITEMSet.Where(x => x.Type == "002").FirstOrDefault();
                VATNewModelFor5Percent = VATDeclarationData.data.VATPERITEMSet.Where(x => x.Type == "003").FirstOrDefault();


                VATNewModelFor15Percent.ImportsaccAdj = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.ImportsaccAdj);
                VATNewModelFor15Percent.ImportsaccAmt = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.ImportsaccAmt);
                VATNewModelFor15Percent.ImportsaccVat = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.ImportsaccVat);
                VATNewModelFor15Percent.ImportspaidAdj = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.ImportspaidAdj);
                VATNewModelFor15Percent.ImportspaidAmt = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.ImportspaidAmt);
                VATNewModelFor15Percent.ImportspaidVat = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.ImportspaidVat);
                VATNewModelFor15Percent.StdpurchaseAdj = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.StdpurchaseAdj);
                VATNewModelFor15Percent.StdpurchaseAmt = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.StdpurchaseAmt);
                VATNewModelFor15Percent.StdpurchasesVat = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.StdpurchasesVat);
                VATNewModelFor15Percent.StdsalesAdj = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.StdsalesAdj);
                VATNewModelFor15Percent.StdsalesAmt = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.StdsalesAmt);
                VATNewModelFor15Percent.StdsalesVat = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.StdsalesVat);
                VATNewModelFor15Percent.GovsupsalesAmt = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.GovsupsalesAmt);
                VATNewModelFor15Percent.GovsupsalesAdj = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.GovsupsalesAdj);

                VATNewModelFor5Percent.ImportsaccAdj = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.ImportsaccAdj);
                VATNewModelFor5Percent.ImportsaccAmt = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.ImportsaccAmt);
                VATNewModelFor5Percent.ImportsaccVat = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.ImportsaccVat);
                VATNewModelFor5Percent.ImportspaidAdj = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.ImportspaidAdj);
                VATNewModelFor5Percent.ImportspaidAmt = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.ImportspaidAmt);
                VATNewModelFor5Percent.ImportspaidVat = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.ImportspaidVat);
                VATNewModelFor5Percent.StdpurchaseAdj = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.StdpurchaseAdj);
                VATNewModelFor5Percent.StdpurchaseAmt = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.StdpurchaseAmt);
                VATNewModelFor5Percent.StdpurchasesVat = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.StdpurchasesVat);
                VATNewModelFor5Percent.StdsalesAdj = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.StdsalesAdj);
                VATNewModelFor5Percent.StdsalesAmt = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.StdsalesAmt);
                VATNewModelFor5Percent.StdsalesVat = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.StdsalesVat);
            }
        }
        private void ManageThePreperiodcorrSwitch()
        {
            if (Convert.ToDouble(Preperiodcorr) < 0)
            {
                ShowThePreperiodcorrSwitch();
            }
            else
            {
                HideThePreperiodcorrSwitch();
            }
        }
        private void ShowThePreperiodcorrSwitch()
        {
            IsSwitchToggled = true;
        }
        private void HideThePreperiodcorrSwitch()
        {
            IsSwitchToggled = false;
        }

        private bool IsTextNullOrEmpty(string entryText)
        {
            bool isEmpty = false;
            if (string.IsNullOrEmpty(entryText))
            {
                isEmpty = true;
            }
            else
            {
                isEmpty = false;
            }
            return isEmpty;
        }
        public void RateSetAsPerDate()
        {
            try
            {
                if (VATDeclarationData.data != null)
                {
                    if (VATDeclarationData.data.Abrzu != null && VATDeclarationData.data.Abrzo != null)
                    {
                        if (VATDeclarationData.data.IBANSet != null && VATDeclarationData.data.IBANSet.Count() != 0)
                        {
                            IBANList = new List<Result2>();
                            IBANList = VATDeclarationData.data.IBANSet;

                        }
                        createIBANType();
                        DateTime startDate = new DateTime();
                        DateTime endDate = new DateTime();
                        if (!string.IsNullOrEmpty(VATDeclarationData.data.Abrzu) && !string.IsNullOrEmpty(VATDeclarationData.data.Abrzo))
                        {
                            VATRateDataWithDateType vATRateDataWithDate;
                            VATRateDataWithStringDateType dataWithStringDateType = new VATRateDataWithStringDateType();
                            dataWithStringDateType.StartDate = VATDeclarationData.data.Abrzu;
                            dataWithStringDateType.EndDate = VATDeclarationData.data.Abrzo;
                            string JsonString = JsonConvert.SerializeObject(dataWithStringDateType);
                            vATRateDataWithDate = JsonConvert.DeserializeObject<VATRateDataWithDateType>(JsonString);
                            startDate = vATRateDataWithDate.StartDate;
                            endDate = vATRateDataWithDate.EndDate;
                        }
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
                        VATCalculationDataVATRSet Rate003 = vATCalculationsforEnd.Where(x => x.Type == "003").FirstOrDefault();
                        if (Rate003 != null)
                        {
                            VATRate003 = Rate003.Penalty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }
        }
        public void createIBANType()
        {
            IBANTypesList = new List<IBANType>();
            List<IBANType> IBANTypesDummyList = new List<IBANType>();
            IBANType iBANType = new IBANType();
            iBANType.key = "ZS0001";
            iBANType.Text = AppResources.ZIBANNationalID;
            IBANTypesDummyList.Add(iBANType);
            IBANType iBANType1 = new IBANType();
            iBANType1.key = "BUP002";
            iBANType1.Text = AppResources.ZIBANCommercialRegistrationID;
            IBANTypesDummyList.Add(iBANType1);
            IBANType iBANType2 = new IBANType();
            iBANType2.key = "ZS0005";
            iBANType2.Text = AppResources.ZIBANCompanyID;
            IBANTypesDummyList.Add(iBANType2);
            IBANTypesList = IBANTypesDummyList;
        }




        #region CalculationsMethods
        public string StandardRatedSalesVatAmount(string Amount, string Adjustment)
        {
            string VATAmount = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(Amount) && Amount.Contains(","))
                {
                    Amount = Amount.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment) && Adjustment.Contains(","))
                {
                    Adjustment = Adjustment.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount) && !string.IsNullOrEmpty(Adjustment) && Amount != "." && Adjustment != ".")
                {
                    if (!Amount.Contains("-") && !Adjustment.Contains("-"))
                    {
                        double dAmount = string.IsNullOrEmpty(Amount) ? 0 : Convert.ToDouble(Amount);
                        double dAdjustment = string.IsNullOrEmpty(Adjustment) ? 0 : Convert.ToDouble(Adjustment);
                        double dVATRate = Convert.ToDouble(VATRate002);
                        VATAmount = Convert.ToDouble((dAmount - dAdjustment) * dVATRate / 100).ToString();
                        if (VATAmount == "0")
                        {
                            VATAmount = "0.00";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(VATAmount) && VATAmount != "0.00")
                {
                    VATAmount = Math.Round(Convert.ToDecimal(VATAmount), 2).ToString();
                    VATAmount = UtilityManager.GetCommaSeparatedAmount(VATAmount);
                }
                bool isTrue = IsTextNullOrEmpty(VATAmount);
                VATAmount = isTrue ? "0.00" : VATAmount;
                return VATAmount;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            return VATAmount;
        }


        public string StandardRatedSalesVatAmountForNewChangeRate(string Amount, string Adjustment, string VatRate)
        {
            string VATAmount = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(Amount) && Amount.Contains(","))
                {
                    Amount = Amount.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment) && Adjustment.Contains(","))
                {
                    Adjustment = Adjustment.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount) && !string.IsNullOrEmpty(Adjustment) && !string.IsNullOrEmpty(VatRate) && Amount != "." && Adjustment != ".")
                {
                    if (!Amount.Contains("-") && !Adjustment.Contains("-"))
                    {
                        double dAmount = string.IsNullOrEmpty(Amount) ? 0 : Convert.ToDouble(Amount);
                        double dAdjustment = string.IsNullOrEmpty(Adjustment) ? 0 : Convert.ToDouble(Adjustment);
                        double dVATRate = Convert.ToDouble(VatRate);
                        VATAmount = Convert.ToDouble((dAmount - dAdjustment) * dVATRate / 100).ToString();
                        if (VATAmount == "0")
                        {
                            VATAmount = "0.00";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(VATAmount) && VATAmount != "0.00")
                {
                    VATAmount = Math.Round(Convert.ToDecimal(VATAmount), 2).ToString();
                    VATAmount = UtilityManager.GetCommaSeparatedAmount(VATAmount);
                }
                bool isTrue = IsTextNullOrEmpty(VATAmount);
                VATAmount = isTrue ? "0.00" : VATAmount;
                return VATAmount;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            return VATAmount;
        }


        public string TotalAmountForSixVar(string Amount1, string Amount2, string Amount3, string Amount4, string Amount5, string Amount6, string Amount7)
        {
            if (string.IsNullOrEmpty(Amount7))
            {
                Amount7 = "0.00";
            }
            string TotalAmount = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(Amount1) && Amount1.Contains(","))
                {
                    Amount1 = Amount1.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount2) && Amount2.Contains(","))
                {
                    Amount2 = Amount2.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount3) && Amount3.Contains(","))
                {
                    Amount3 = Amount3.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount4) && Amount4.Contains(","))
                {
                    Amount4 = Amount4.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount5) && Amount5.Contains(","))
                {
                    Amount5 = Amount5.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount6) && Amount6.Contains(","))
                {
                    Amount6 = Amount6.Replace(",", "");
                }if (!string.IsNullOrEmpty(Amount7) && Amount7.Contains(","))
                {
                    Amount7 = Amount7.Replace(",", "");
                }
                
                // if (!string.IsNullOrEmpty(Amount1) && !string.IsNullOrEmpty(Amount2) && !string.IsNullOrEmpty(Amount3) && !string.IsNullOrEmpty(Amount4) && !string.IsNullOrEmpty(Amount5) && !string.IsNullOrEmpty(Amount6)&& !string.IsNullOrEmpty(Amount7) )
                // {
                //     if (Amount1 != "." && Amount2 != "." && Amount3 != "." && Amount4 != "." && Amount5 != "." && Amount6 != "."&& Amount7 != "." )
                //     {
                //         if (!Amount1.Contains("-") && !Amount2.Contains("-") && !Amount3.Contains("-") && !Amount4.Contains("-") && !Amount5.Contains("-") && !Amount6.Contains("-") && !Amount7.Contains("-") )
                //         {
                //             TotalAmount = Convert.ToDouble(((String.IsNullOrEmpty(Amount1) ? 0.00 : Convert.ToDouble(Amount1)) + (String.IsNullOrEmpty(Amount2) ? 0.00 : Convert.ToDouble(Amount2)) + (String.IsNullOrEmpty(Amount3) ? 0.00 : Convert.ToDouble(Amount3)) + (String.IsNullOrEmpty(Amount4) ? 0.00 : Convert.ToDouble(Amount4)) + (String.IsNullOrEmpty(Amount5) ? 0.00 : Convert.ToDouble(Amount5)) + (String.IsNullOrEmpty(Amount6) ? 0.00 : Convert.ToDouble(Amount6))+ (String.IsNullOrEmpty(Amount7) ? 0.00 : Convert.ToDouble(Amount7)))).ToString();
                if (!string.IsNullOrEmpty(Amount7) && Amount7.Contains(","))
                {
                    Amount7 = Amount7.Replace(",", "");
                }

                if (!string.IsNullOrEmpty(Amount1) && !string.IsNullOrEmpty(Amount2) && !string.IsNullOrEmpty(Amount3) && !string.IsNullOrEmpty(Amount4) && !string.IsNullOrEmpty(Amount5) && !string.IsNullOrEmpty(Amount6) && !string.IsNullOrEmpty(Amount7))
                {
                    if (Amount1 != "." && Amount2 != "." && Amount3 != "." && Amount4 != "." && Amount5 != "." && Amount6 != "." && Amount7 != ".")
                    {
                        if (!Amount1.Contains("-") && !Amount2.Contains("-") && !Amount3.Contains("-") && !Amount4.Contains("-") && !Amount5.Contains("-") && !Amount6.Contains("-") && !Amount7.Contains("-"))
                        {
                            TotalAmount = Convert.ToDouble((string.IsNullOrEmpty(Amount1) ? 0.00 : Convert.ToDouble(Amount1)) + (string.IsNullOrEmpty(Amount2) ? 0.00 : Convert.ToDouble(Amount2)) + (string.IsNullOrEmpty(Amount3) ? 0.00 : Convert.ToDouble(Amount3)) + (string.IsNullOrEmpty(Amount4) ? 0.00 : Convert.ToDouble(Amount4)) + (string.IsNullOrEmpty(Amount5) ? 0.00 : Convert.ToDouble(Amount5)) + (string.IsNullOrEmpty(Amount6) ? 0.00 : Convert.ToDouble(Amount6)) + (string.IsNullOrEmpty(Amount7) ? 0.00 : Convert.ToDouble(Amount7))).ToString();
                            if (TotalAmount == "0")
                            {
                                TotalAmount = "0.00";
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
                {
                    TotalAmount = Math.Round(Convert.ToDecimal(TotalAmount), 2).ToString();
                    TotalAmount = UtilityManager.GetCommaSeparatedAmount(TotalAmount);
                }
                bool isTrue = IsTextNullOrEmpty(TotalAmount);
                TotalAmount = isTrue ? "0.00" : TotalAmount;
                return TotalAmount;
            }
            catch (Exception ex)
            {

                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());

            }
            return TotalAmount;
        }
        public string TotalAmountForSixVarForNegative(string Amount1, string Amount2, string Amount3, string Amount4, string Amount5, string Amount6)
        {
            string TotalAmount = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(Amount1) && Amount1.Contains(","))
                {
                    Amount1 = Amount1.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount2) && Amount2.Contains(","))
                {
                    Amount2 = Amount2.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount3) && Amount3.Contains(","))
                {
                    Amount3 = Amount3.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount4) && Amount4.Contains(","))
                {
                    Amount4 = Amount4.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount5) && Amount5.Contains(","))
                {
                    Amount5 = Amount5.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount6) && Amount6.Contains(","))
                {
                    Amount6 = Amount6.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount1) && !string.IsNullOrEmpty(Amount2) && !string.IsNullOrEmpty(Amount3) && !string.IsNullOrEmpty(Amount4) && !string.IsNullOrEmpty(Amount5) && !string.IsNullOrEmpty(Amount6))
                {
                    if (Amount1 != "." && Amount2 != "." && Amount3 != "." && Amount4 != "." && Amount5 != "." && Amount6 != ".")
                    {
                        TotalAmount = Convert.ToDouble((string.IsNullOrEmpty(Amount1) ? 0.00 : Convert.ToDouble(Amount1)) + (string.IsNullOrEmpty(Amount2) ? 0.00 : Convert.ToDouble(Amount2)) + (string.IsNullOrEmpty(Amount3) ? 0.00 : Convert.ToDouble(Amount3)) + (string.IsNullOrEmpty(Amount4) ? 0.00 : Convert.ToDouble(Amount4)) + (string.IsNullOrEmpty(Amount5) ? 0.00 : Convert.ToDouble(Amount5)) + (string.IsNullOrEmpty(Amount6) ? 0.00 : Convert.ToDouble(Amount6))).ToString();
                        if (TotalAmount == "0")
                        {
                            TotalAmount = "0.00";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
                {
                    TotalAmount = Math.Round(Convert.ToDecimal(TotalAmount), 2).ToString();
                    TotalAmount = UtilityManager.GetCommaSeparatedAmount(TotalAmount);
                }
                bool isTrue = IsTextNullOrEmpty(TotalAmount);
                TotalAmount = isTrue ? "0.00" : TotalAmount;
                return TotalAmount;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            return TotalAmount;
        }

        public string TotalAmountForEightVar(string Amount1, string Amount2, string Amount3, string Amount4, string Amount5, string Amount6, string Amount7, string Amount8)
        {
            string TotalAmount = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(Amount1) && Amount1.Contains(","))
                {
                    Amount1 = Amount1.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount2) && Amount2.Contains(","))
                {
                    Amount2 = Amount2.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount3) && Amount3.Contains(","))
                {
                    Amount3 = Amount3.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount4) && Amount4.Contains(","))
                {
                    Amount4 = Amount4.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount5) && Amount5.Contains(","))
                {
                    Amount5 = Amount5.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount6) && Amount6.Contains(","))
                {
                    Amount6 = Amount6.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount7) && Amount7.Contains(","))
                {
                    Amount7 = Amount7.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount8) && Amount8.Contains(","))
                {
                    Amount8 = Amount8.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount1) && !string.IsNullOrEmpty(Amount2) && !string.IsNullOrEmpty(Amount3) && !string.IsNullOrEmpty(Amount4) && !string.IsNullOrEmpty(Amount5) && !string.IsNullOrEmpty(Amount6) && !string.IsNullOrEmpty(Amount7) && !string.IsNullOrEmpty(Amount8))
                {
                    if (Amount1 != "." && Amount2 != "." && Amount3 != "." && Amount4 != "." && Amount5 != "." && Amount6 != "." && Amount7 != "." && Amount8 != ".")
                    {
                        if (!Amount1.Contains("-") && !Amount2.Contains("-") && !Amount3.Contains("-") && !Amount4.Contains("-") && !Amount5.Contains("-") && !Amount6.Contains("-") && !Amount7.Contains("-") && !Amount8.Contains("-"))
                        {
                            TotalAmount = Convert.ToDouble((string.IsNullOrEmpty(Amount1) ? 0.00 : Convert.ToDouble(Amount1)) + (string.IsNullOrEmpty(Amount2) ? 0.00 : Convert.ToDouble(Amount2)) + (string.IsNullOrEmpty(Amount3) ? 0.00 : Convert.ToDouble(Amount3)) + (string.IsNullOrEmpty(Amount4) ? 0.00 : Convert.ToDouble(Amount4)) + (string.IsNullOrEmpty(Amount5) ? 0.00 : Convert.ToDouble(Amount5)) + (string.IsNullOrEmpty(Amount6) ? 0.00 : Convert.ToDouble(Amount6)) + (string.IsNullOrEmpty(Amount7) ? 0.00 : Convert.ToDouble(Amount7)) + (string.IsNullOrEmpty(Amount8) ? 0.00 : Convert.ToDouble(Amount8))).ToString();
                            if (TotalAmount == "0")
                            {
                                TotalAmount = "0.00";
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
                {
                    TotalAmount = Math.Round(Convert.ToDecimal(TotalAmount), 2).ToString();
                    TotalAmount = UtilityManager.GetCommaSeparatedAmount(TotalAmount);
                }
                bool isTrue = IsTextNullOrEmpty(TotalAmount);
                TotalAmount = isTrue ? "0.00" : TotalAmount;
                return TotalAmount;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            return TotalAmount;
        }


        public string TotalAmount(string Amount1, string Amount2, string Amount3, string Amount4, string Amount5,string Amount6)
        {
            string TotalAmount = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(Amount1) && Amount1.Contains(","))
                {
                    Amount1 = Amount1.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount2) && Amount2.Contains(","))
                {
                    Amount2 = Amount2.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount3) && Amount3.Contains(","))
                {
                    Amount3 = Amount3.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount4) && Amount4.Contains(","))
                {
                    Amount4 = Amount4.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount5) && Amount5.Contains(","))
                {
                    Amount5 = Amount5.Replace(",", "");
                } if (!string.IsNullOrEmpty(Amount6) && Amount6.Contains(","))
                {
                    Amount6 = Amount6.Replace(",", "");
                }
               
                // if (!string.IsNullOrEmpty(Amount1) && !string.IsNullOrEmpty(Amount2) && !string.IsNullOrEmpty(Amount3) && !string.IsNullOrEmpty(Amount4) && !string.IsNullOrEmpty(Amount5) && !string.IsNullOrEmpty(Amount6) )
                // {
                //     if (Amount1 != "." && Amount2 != "." && Amount3 != "." && Amount4 != "." && Amount5 != "." && Amount6 != "." )
                //     {
                //         if (!Amount1.Contains("-") && !Amount2.Contains("-") && !Amount3.Contains("-") && !Amount4.Contains("-") && !Amount5.Contains("-")&& !Amount6.Contains("-") )
                //         {
                //             TotalAmount = Convert.ToDouble(((String.IsNullOrEmpty(Amount1) ? 0.00 : Convert.ToDouble(Amount1)) + (String.IsNullOrEmpty(Amount2) ? 0.00 : Convert.ToDouble(Amount2)) +  (String.IsNullOrEmpty(Amount3) ? 0.00 : Convert.ToDouble(Amount3)) + (String.IsNullOrEmpty(Amount4) ? 0.00 : Convert.ToDouble(Amount4)) + (String.IsNullOrEmpty(Amount5) ? 0.00 : Convert.ToDouble(Amount5))+(String.IsNullOrEmpty(Amount6) ? 0.00 : Convert.ToDouble(Amount6)))).ToString();
                if (!string.IsNullOrEmpty(Amount6) && Amount6.Contains(","))
                {
                    Amount6 = Amount6.Replace(",", "");
                }

                if (!string.IsNullOrEmpty(Amount1) && !string.IsNullOrEmpty(Amount2) && !string.IsNullOrEmpty(Amount3) && !string.IsNullOrEmpty(Amount4) && !string.IsNullOrEmpty(Amount5) && !string.IsNullOrEmpty(Amount6))
                {
                    if (Amount1 != "." && Amount2 != "." && Amount3 != "." && Amount4 != "." && Amount5 != "." && Amount6 != ".")
                    {
                        if (!Amount1.Contains("-") && !Amount2.Contains("-") && !Amount3.Contains("-") && !Amount4.Contains("-") && !Amount5.Contains("-") && !Amount6.Contains("-"))
                        {
                            TotalAmount = Convert.ToDouble((string.IsNullOrEmpty(Amount1) ? 0.00 : Convert.ToDouble(Amount1)) + (string.IsNullOrEmpty(Amount2) ? 0.00 : Convert.ToDouble(Amount2)) + (string.IsNullOrEmpty(Amount3) ? 0.00 : Convert.ToDouble(Amount3)) + (string.IsNullOrEmpty(Amount4) ? 0.00 : Convert.ToDouble(Amount4)) + (string.IsNullOrEmpty(Amount5) ? 0.00 : Convert.ToDouble(Amount5)) + (string.IsNullOrEmpty(Amount6) ? 0.00 : Convert.ToDouble(Amount6))).ToString();
                            if (TotalAmount == "0")
                            {
                                TotalAmount = "0.00";
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
                {
                    TotalAmount = Math.Round(Convert.ToDecimal(TotalAmount), 2).ToString();
                    TotalAmount = UtilityManager.GetCommaSeparatedAmount(TotalAmount);
                }
                bool isTrue = IsTextNullOrEmpty(TotalAmount);
                TotalAmount = isTrue ? "0.00" : TotalAmount;
                return TotalAmount;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            return TotalAmount;
        }
        public string TotalAdjustmentForSixVar(string Adjustment1, string Adjustment2, string Adjustment3, string Adjustment4, string Adjustment5, string Adjustment6, string Adjustment7)
        {
            string TotalAmount = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(Adjustment1) && Adjustment1.Contains(","))
                {
                    Adjustment1 = Adjustment1.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment2) && Adjustment2.Contains(","))
                {
                    Adjustment2 = Adjustment2.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment3) && Adjustment3.Contains(","))
                {
                    Adjustment3 = Adjustment3.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment4) && Adjustment4.Contains(","))
                {
                    Adjustment4 = Adjustment4.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment5) && Adjustment5.Contains(","))
                {
                    Adjustment5 = Adjustment5.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment6) && Adjustment6.Contains(","))
                {
                    Adjustment6 = Adjustment6.Replace(",", "");
                }if (!string.IsNullOrEmpty(Adjustment7) && Adjustment7.Contains(","))
                {
                    Adjustment7 = Adjustment7.Replace(",", "");
                }
                // if (!string.IsNullOrEmpty(Adjustment1) && !string.IsNullOrEmpty(Adjustment2) && !string.IsNullOrEmpty(Adjustment3) && !string.IsNullOrEmpty(Adjustment4) && !string.IsNullOrEmpty(Adjustment5) && !string.IsNullOrEmpty(Adjustment6)&& !string.IsNullOrEmpty(Adjustment7))
                // {
                //     if (Adjustment1 != "." && Adjustment2 != "." && Adjustment3 != "." && Adjustment4 != "." && Adjustment5 != "." && Adjustment6 != "."&& Adjustment7 != ".")
                //     {
                //         if (!Adjustment1.Contains("-") && !Adjustment2.Contains("-") && !Adjustment3.Contains("-") && !Adjustment4.Contains("-") && !Adjustment5.Contains("-") && !Adjustment6.Contains("-")&& !Adjustment7.Contains("-"))
                if (!string.IsNullOrEmpty(Adjustment7) && Adjustment7.Contains(","))
                {
                    Adjustment7 = Adjustment7.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment1) && !string.IsNullOrEmpty(Adjustment2) && !string.IsNullOrEmpty(Adjustment3) && !string.IsNullOrEmpty(Adjustment4) && !string.IsNullOrEmpty(Adjustment5) && !string.IsNullOrEmpty(Adjustment6) && !string.IsNullOrEmpty(Adjustment7))
                {
                    if (Adjustment1 != "." && Adjustment2 != "." && Adjustment3 != "." && Adjustment4 != "." && Adjustment5 != "." && Adjustment6 != "." && Adjustment7 != ".")
                    {
                        if (!Adjustment1.Contains("-") && !Adjustment2.Contains("-") && !Adjustment3.Contains("-") && !Adjustment4.Contains("-") && !Adjustment5.Contains("-") && !Adjustment6.Contains("-") && !Adjustment7.Contains("-"))
                        {
                            TotalAmount = Convert.ToDouble((string.IsNullOrEmpty(Adjustment1) ? 0 : Convert.ToDouble(Adjustment1)) + (string.IsNullOrEmpty(Adjustment2) ? 0 : Convert.ToDouble(Adjustment2)) + (string.IsNullOrEmpty(Adjustment3) ? 0 : Convert.ToDouble(Adjustment3)) + (string.IsNullOrEmpty(Adjustment4) ? 0 : Convert.ToDouble(Adjustment4)) + (string.IsNullOrEmpty(Adjustment5) ? 0 : Convert.ToDouble(Adjustment5)) + (string.IsNullOrEmpty(Adjustment6) ? 0 : Convert.ToDouble(Adjustment6)) + (string.IsNullOrEmpty(Adjustment7) ? 0 : Convert.ToDouble(Adjustment7))).ToString();
                            if (TotalAmount == "0")
                            {
                                TotalAmount = "0.00";
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
                {
                    TotalAmount = Math.Round(Convert.ToDecimal(TotalAmount), 2).ToString();
                    TotalAmount = UtilityManager.GetCommaSeparatedAmount(TotalAmount);
                }
                bool isTrue = IsTextNullOrEmpty(TotalAmount);
                TotalAmount = isTrue ? "0.00" : TotalAmount;
                return TotalAmount;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            return TotalAmount;
        }
        public string TotalAdjustment(string Adjustment1, string Adjustment2, string Adjustment3, string Adjustment4, string Adjustment5, string Adjustment6)
        {
            string TotalAmount = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(Adjustment1) && Adjustment1.Contains(","))
                {
                    Adjustment1 = Adjustment1.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment2) && Adjustment2.Contains(","))
                {
                    Adjustment2 = Adjustment2.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment3) && Adjustment3.Contains(","))
                {
                    Adjustment3 = Adjustment3.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment4) && Adjustment4.Contains(","))
                {
                    Adjustment4 = Adjustment4.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment5) && Adjustment5.Contains(","))
                {
                    Adjustment5 = Adjustment5.Replace(",", "");
                } if (!string.IsNullOrEmpty(Adjustment6) && Adjustment6.Contains(","))
                {
                    Adjustment6 = Adjustment6.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment6) && Adjustment6.Contains(","))
                {
                    Adjustment6 = Adjustment6.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment1) && !string.IsNullOrEmpty(Adjustment2) && !string.IsNullOrEmpty(Adjustment3) && !string.IsNullOrEmpty(Adjustment4) && !string.IsNullOrEmpty(Adjustment5) && !string.IsNullOrEmpty(Adjustment6))
                {
                    if (Adjustment1 != "." && Adjustment2 != "." && Adjustment3 != "." && Adjustment4 != "." && Adjustment5 != "." && Adjustment6 != ".")
                    {
                        if (!Adjustment1.Contains("-") && !Adjustment2.Contains("-") && !Adjustment3.Contains("-") && !Adjustment4.Contains("-") && !Adjustment5.Contains("-") && !Adjustment6.Contains("-"))
                        {
                            TotalAmount = Convert.ToDouble((string.IsNullOrEmpty(Adjustment1) ? 0 : Convert.ToDouble(Adjustment1)) + (string.IsNullOrEmpty(Adjustment2) ? 0 : Convert.ToDouble(Adjustment2)) + (string.IsNullOrEmpty(Adjustment3) ? 0 : Convert.ToDouble(Adjustment3)) + (string.IsNullOrEmpty(Adjustment4) ? 0 : Convert.ToDouble(Adjustment4)) + (string.IsNullOrEmpty(Adjustment5) ? 0 : Convert.ToDouble(Adjustment5)) + (string.IsNullOrEmpty(Adjustment6) ? 0 : Convert.ToDouble(Adjustment6))).ToString();
                            if (TotalAmount == "0")
                            {
                                TotalAmount = "0.00";
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
                {
                    TotalAmount = Math.Round(Convert.ToDecimal(TotalAmount), 2).ToString();
                    TotalAmount = UtilityManager.GetCommaSeparatedAmount(TotalAmount);
                }
                bool isTrue = IsTextNullOrEmpty(TotalAmount);
                TotalAmount = isTrue ? "0.00" : TotalAmount;
                return TotalAmount;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            return TotalAmount;
        }

        public string GetSingleAmount(string Amount1)
        {
            string TotalAmount = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(Amount1) && Amount1.Contains(","))
                {
                    Amount1 = Amount1.Replace(",", "");
                }

                if (!string.IsNullOrEmpty(Amount1))
                {
                    TotalAmount = Convert.ToDouble(Convert.ToDouble(Amount1)).ToString();
                    if (TotalAmount == "0")
                    {
                        TotalAmount = "0.00";
                    }
                }
                if (!string.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
                {
                    TotalAmount = Math.Round(Convert.ToDecimal(TotalAmount), 2).ToString();
                    TotalAmount = UtilityManager.GetCommaSeparatedAmount(TotalAmount);
                }
                bool isTrue = IsTextNullOrEmpty(TotalAmount);
                TotalAmount = isTrue ? "0.00" : TotalAmount;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            return TotalAmount;
        }

        public string AddTwoAmount(string Amount1, string Amount2, string Amount3)
        {
            string TotalAmount = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(Amount1) && Amount1.Contains(","))
                {
                    Amount1 = Amount1.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount2) && Amount2.Contains(","))
                {
                    Amount2 = Amount2.Replace(",", "");
                }if (!string.IsNullOrEmpty(Amount3) && Amount3.Contains(","))
                {
                    Amount2 = Amount2.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount3) && Amount3.Contains(","))
                {
                    Amount2 = Amount2.Replace(",", "");
                }
                if (string.IsNullOrEmpty(Amount1))
                {
                    Amount1 = "0.00";
                }
                if (string.IsNullOrEmpty(Amount2))
                {
                    Amount2 = "0.00";
                }if (String.IsNullOrEmpty(Amount3))
                {
                    Amount2 = "0.00";
                }
                // if (!String.IsNullOrEmpty(Amount1) && !String.IsNullOrEmpty(Amount2) && !String.IsNullOrEmpty(Amount3))
                // {
                //     TotalAmount = Convert.ToDouble((Convert.ToDouble(Amount1) + Convert.ToDouble(Amount2)+ Convert.ToDouble(Amount3))).ToString();
                if (String.IsNullOrEmpty(Amount3))
                {
                    Amount2 = "0.00";
                }
                if (!string.IsNullOrEmpty(Amount1) && !string.IsNullOrEmpty(Amount2) && !string.IsNullOrEmpty(Amount3))
                {
                    TotalAmount = Convert.ToDouble(Convert.ToDouble(Amount1) + Convert.ToDouble(Amount2) + Convert.ToDouble(Amount3)).ToString();
                    if (TotalAmount == "0")
                    {
                        TotalAmount = "0.00";
                    }
                }
                if (!string.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
                {
                    TotalAmount = Math.Round(Convert.ToDecimal(TotalAmount), 2).ToString();
                    TotalAmount = UtilityManager.GetCommaSeparatedAmount(TotalAmount);
                }
                bool isTrue = IsTextNullOrEmpty(TotalAmount);
                TotalAmount = isTrue ? "0.00" : TotalAmount;
            }
            catch (Exception ex)
            {

                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            return TotalAmount;
        }
        public string TotalVatAmount(string Amount1, string Amount2, string Amount3)
        {
            string TotalAmount = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(Amount1) && Amount1.Contains(","))
                {
                    Amount1 = Amount1.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount2) && Amount2.Contains(","))
                {
                    Amount2 = Amount2.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount3) && Amount3.Contains(","))
                {
                    Amount3 = Amount3.Replace(",", "");
                }
                if (string.IsNullOrEmpty(Amount1))
                {
                    Amount1 = "0.00";
                }
                if (string.IsNullOrEmpty(Amount2))
                {
                    Amount2 = "0.00";
                }
                if (string.IsNullOrEmpty(Amount3))
                {
                    Amount3 = "0.00";
                }
                if (!string.IsNullOrEmpty(Amount1) && !string.IsNullOrEmpty(Amount2) && !string.IsNullOrEmpty(Amount3))
                {
                    TotalAmount = Convert.ToDouble(Convert.ToDouble(Amount1) + Convert.ToDouble(Amount2) + Convert.ToDouble(Amount3)).ToString();
                    if (TotalAmount == "0")
                    {
                        TotalAmount = "0.00";
                    }
                }
                if (!string.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
                {
                    TotalAmount = Math.Round(Convert.ToDecimal(TotalAmount), 2).ToString();
                    TotalAmount = UtilityManager.GetCommaSeparatedAmount(TotalAmount);
                }
                bool isTrue = IsTextNullOrEmpty(TotalAmount);
                TotalAmount = isTrue ? "0.00" : TotalAmount;
            }
            catch (Exception ex)
            {

                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            return TotalAmount;
        }
        public string StandardRatedDomesticPurchaseVatAmount(string Amount, string Adjustment)
        {
            string VATAmount = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(Amount) && Amount.Contains(","))
                {
                    Amount = Amount.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment) && Adjustment.Contains(","))
                {
                    Adjustment = Adjustment.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount) && !string.IsNullOrEmpty(Adjustment) && Amount != "." && Adjustment != ".")
                {
                    if (!Amount.Contains("-") && !Adjustment.Contains("-"))
                    {
                        double dAmount = string.IsNullOrEmpty(Amount) ? 0 : Convert.ToDouble(Amount);
                        double dAdjustment = string.IsNullOrEmpty(Adjustment) ? 0 : Convert.ToDouble(Adjustment);
                        double dVATRate = Convert.ToDouble(VATRate002);
                        VATAmount = Convert.ToDouble((dAmount - dAdjustment) * dVATRate / 100).ToString();
                        if (VATAmount == "0")
                        {
                            VATAmount = "0.00";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(VATAmount) && VATAmount != "0.00")
                {
                    VATAmount = Math.Round(Convert.ToDecimal(VATAmount), 2).ToString();
                    VATAmount = UtilityManager.GetCommaSeparatedAmount(VATAmount);
                }
                bool isTrue = IsTextNullOrEmpty(VATAmount);
                VATAmount = isTrue ? "0.00" : VATAmount;
                return VATAmount;
            }
            catch (Exception ex)
            {

                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            return VATAmount;
        }
        //This method is used to calculate  Imports subject to VAT accounted for through the reverse charge mechanism Vat Amount too.
        public string ImportSubjectToVatPaidAtCustomsVatAmountForDesignated(string Amount, string Adjustment)
        {
            string VATAmount = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(Amount) && Amount.Contains(","))
                {
                    Amount = Amount.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment) && Adjustment.Contains(","))
                {
                    Adjustment = Adjustment.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount) && !string.IsNullOrEmpty(Adjustment) && Amount != "." && Adjustment != ".")
                {
                    if (!Amount.Contains("-") && !Adjustment.Contains("-"))
                    {
                        double dAmount = string.IsNullOrEmpty(Amount) ? 0 : Convert.ToDouble(Amount);
                        double dAdjustment = string.IsNullOrEmpty(Adjustment) ? 0 : Convert.ToDouble(Adjustment);
                        double dVATRate001 = Convert.ToDouble(VATRate001);
                        double dVATRate002 = Convert.ToDouble(VATRate002);
                        VATAmount = Convert.ToDouble(dAmount * dVATRate001 / 100 - dAdjustment * dVATRate002 / 100).ToString();
                        if (VATAmount == "0")
                        {
                            VATAmount = "0.00";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(VATAmount) && VATAmount != "0.00")
                {
                    VATAmount = Math.Round(Convert.ToDecimal(VATAmount), 2).ToString();
                    VATAmount = UtilityManager.GetCommaSeparatedAmount(VATAmount);
                }
                bool isTrue = IsTextNullOrEmpty(VATAmount);
                VATAmount = isTrue ? "0.00" : VATAmount;
                return VATAmount;
            }
            catch (Exception ex)
            {

                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            return VATAmount;
        }

        public string ImportSubjectToVatPaidAtCustomsVatAmountForDesignatedForNewPercentage(string Amount, string Adjustment, string NewVATRate)
        {
            string VATAmount = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(Amount) && Amount.Contains(","))
                {
                    Amount = Amount.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment) && Adjustment.Contains(","))
                {
                    Adjustment = Adjustment.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(NewVATRate) && NewVATRate.Contains(","))
                {
                    NewVATRate = NewVATRate.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount) && !string.IsNullOrEmpty(Adjustment) && !string.IsNullOrEmpty(NewVATRate) && Amount != "." && Adjustment != "." && NewVATRate != ".")
                {
                    if (!Amount.Contains("-") && !Adjustment.Contains("-") && !NewVATRate.Contains("-"))
                    {
                        double dAmount = string.IsNullOrEmpty(Amount) ? 0 : Convert.ToDouble(Amount);
                        double dAdjustment = string.IsNullOrEmpty(Adjustment) ? 0 : Convert.ToDouble(Adjustment);
                        double dVATRate002 = string.IsNullOrEmpty(NewVATRate) ? 0 : Convert.ToDouble(NewVATRate);
                        double dVATRate001 = Convert.ToDouble(VATRate001);
                        dVATRate002 = Convert.ToDouble(NewVATRate);
                        VATAmount = Convert.ToDouble(dAmount * dVATRate001 / 100 - dAdjustment * dVATRate002 / 100).ToString();
                        if (VATAmount == "0")
                        {
                            VATAmount = "0.00";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(VATAmount) && VATAmount != "0.00")
                {
                    VATAmount = Math.Round(Convert.ToDecimal(VATAmount), 2).ToString();
                    VATAmount = UtilityManager.GetCommaSeparatedAmount(VATAmount);
                }
                bool isTrue = IsTextNullOrEmpty(VATAmount);
                VATAmount = isTrue ? "0.00" : VATAmount;
                return VATAmount;
            }
            catch (Exception ex)
            {

                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            return VATAmount;
        }

        public string ImportSubjectToVatPaidAtCustomsVatAmountForNonDesignated(string Amount, string Adjustment)
        {
            string VATAmount = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(Amount) && Amount.Contains(","))
                {
                    Amount = Amount.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment) && Adjustment.Contains(","))
                {
                    Adjustment = Adjustment.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount) && !string.IsNullOrEmpty(Adjustment) && Amount != "." && Adjustment != ".")
                {
                    if (!Amount.Contains("-") && !Adjustment.Contains("-"))
                    {
                        double dAmount = string.IsNullOrEmpty(Amount) ? 0 : Convert.ToDouble(Amount);
                        double dAdjustment = string.IsNullOrEmpty(Adjustment) ? 0 : Convert.ToDouble(Adjustment);
                        double dVATRate = Convert.ToDouble(VATRate002);
                        VATAmount = Convert.ToDouble((dAmount - dAdjustment) * dVATRate / 100).ToString();
                        if (VATAmount == "0")
                        {
                            VATAmount = "0.00";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(VATAmount) && VATAmount != "0.00")
                {
                    VATAmount = Math.Round(Convert.ToDecimal(VATAmount), 2).ToString();
                    VATAmount = UtilityManager.GetCommaSeparatedAmount(VATAmount);
                }
                bool isTrue = IsTextNullOrEmpty(VATAmount);
                VATAmount = isTrue ? "0.00" : VATAmount;
                return VATAmount;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            return VATAmount;
        }
        public string NetVatDue(string CurrentPeriod, string PreviousPeriod, string ForwardFromPreviousPeriod)
        {
            string NetVatDue = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(CurrentPeriod) && CurrentPeriod.Contains(","))
                {
                    CurrentPeriod = CurrentPeriod.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(PreviousPeriod) && PreviousPeriod.Contains(","))
                {
                    PreviousPeriod = PreviousPeriod.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(ForwardFromPreviousPeriod) && ForwardFromPreviousPeriod.Contains(","))
                {
                    ForwardFromPreviousPeriod = ForwardFromPreviousPeriod.Replace(",", "");
                }
                try
                {
                    double dCurrentPeriod = string.IsNullOrEmpty(CurrentPeriod) ? 0 : Convert.ToDouble(CurrentPeriod);
                    double dPreviousPeriod = string.IsNullOrEmpty(PreviousPeriod) ? 0 : Convert.ToDouble(PreviousPeriod);
                    double dForwardFromPreviousPeriod = string.IsNullOrEmpty(ForwardFromPreviousPeriod) ? 0 : Convert.ToDouble(ForwardFromPreviousPeriod);
                    NetVatDue = Convert.ToDouble(dCurrentPeriod + dPreviousPeriod + dForwardFromPreviousPeriod).ToString();
                    if (NetVatDue == "0")
                    {
                        NetVatDue = "0.00";
                    }
                }
                catch
                {
                }
                if (!string.IsNullOrEmpty(NetVatDue) && NetVatDue != "0.00")
                {
                    NetVatDue = Math.Round(Convert.ToDecimal(NetVatDue), 2).ToString();
                    NetVatDue = UtilityManager.GetCommaSeparatedAmount(NetVatDue);
                }
                bool isTrue = IsTextNullOrEmpty(NetVatDue);
                NetVatDue = isTrue ? "0.00" : NetVatDue;
                return NetVatDue;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            return NetVatDue;
        }

        #endregion


        public async Task SetButtons(VATDeclaration vATDeclarationData)
        {
            try
            {
                bool Isamend = false;
                List<ApplicableButton> VATApplicableButtons = await WebServiceManager.GAZTVATReturnGetApplicableButtons(VATDeclarationData.data.Fbnumz, VATDeclarationData.data.Langz, VATDeclarationData.data.Operationz, VATDeclarationData.data.Gpart, VATDeclarationData.data.Statusz, VATDeclarationData.data.TxnTpz, vATDeclarationData.data.Persl);
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

                        if (button.buttonEnumId.ToString() == "Amend")
                        {
                            Isamend = true;
                        }
                        if (button.Button == "70")
                        {
                            IsNoteAttachVisible = true;
                        }
                        else
                        {
                            IsNoteAttachVisible = false;
                        }
                        // ListOfActionButtonsApplicable.Add(button.buttonEnumId.ToString());
                    }
                    ListOfActionButtonsApplicable = DummyListOfActionButtonsApplicable;
                }
                if (Isamend)
                {
                    IsVisibleAmendButton = true;
                    IsAmendButtonAvailable = true;
                }
                else
                {
                    IsVisibleAmendButton = false;
                    IsAmendButtonAvailable = false;
                }
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }
        }
        public void SetButtonStrings(string ButtonName)
        {
            if (ButtonName == "Submit")
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
            else if (ButtonName == "SaveasDraft")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.ZZSaveAsDraft);
            }
            else if (ButtonName == "DisplayNotes")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.ZZDisplayNotes);
            }
            //else if (ButtonName == "Validate")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZZValidate);
            //}
            else if (ButtonName == "Attachments")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.Attachments);
            }
            else if (ButtonName == "Reset")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.ZZReset);
            }
            else if (ButtonName == "CreateNotes")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.ZVATCreateNote);
            }
            //else if (ButtonName == "Amend")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZZAmend);
            //}
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
        }


        public bool IsCommaSeparatedValidAmount(string amount, int Max, int numberOfDigitAfterDecimal, int numberOfDigitBeforDecimal)
        {
            bool iSValiedNumber = false;
            try
            {

                if (amount != null && amount.Length < Max && amount.Length > 0)
                {
                    amount = amount.Replace(",", "");
                    if (amount.Contains("."))
                    {
                        string[] Amount = new string[2];
                        Amount = amount.Split('.');
                        if (Amount[0].Length > numberOfDigitBeforDecimal || Amount[1].Length > numberOfDigitAfterDecimal)
                        {
                            iSValiedNumber = false;
                        }
                        else
                        {
                            iSValiedNumber = true;
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(amount.Length) > numberOfDigitBeforDecimal)
                        {
                            iSValiedNumber = false;
                        }
                        else
                        {
                            iSValiedNumber = true;
                        }
                    }
                }
                else
                {
                    //amountWithComma = amount;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            return iSValiedNumber;
        }

        public void gotoSuccessPage()
        {
            _navigationService.NavigateTo(App.MyBillsSadadDetailsPageView, _vATDeclarationData);
        }
        public async Task DoValidatePayment(string fbNum, string paymentType)
        {
            try
            {
                try
                {

                    IsLoading = true;

                    var platform = "";

                    if (DeviceInfo.Platform == DevicePlatform.iOS)
                    {
                        platform = "C4";
                    }
                    else if (DeviceInfo.Platform == DevicePlatform.Android)
                    {
                        platform = "C3";
                    }
                    //PaymentData = await WebServiceManager.GAZTValidatePayment(fbNum, App.LoginDataRetrieved.TIN, platform,paymentType);


                    ValidatePayment modelDetails = new ValidatePayment();
                    modelDetails.Fbnum = fbNum;
                    modelDetails.Pymntty = paymentType;
                    modelDetails.Tin = App.LoginDataRetrieved.TIN;
                    modelDetails.Srcid = platform;
                    modelDetails.Srctile = "36";
                    modelDetails.Sadad = "";

                    PaymentData = await WebServiceManager.GAZTValidatePayment(modelDetails);

                    if (PaymentData != null && PaymentData.d != null)
                    {

                        if (PaymentData.d.Guid != null && PaymentData.d.Guid == "")
                        {
                            await MopupService.Instance.PushAsync(new PaymentExceptionPageView());
                            return;
                        }

                        if (PaymentData.d.Guid != null)
                        {

                            App.PaymentGuid = PaymentData.d.Guid;

                        }

                        if (paymentType == "Mada Payment")
                        {

                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                            IsLoading = true;
                            //CR7420
                            CreateMadaResponseRoot respose = await GetWebviewContent(PaymentData.d.Srcid);
                            IsLoading = false;
                                if (!string.IsNullOrEmpty(respose?.result?.securityAuthorizationKey))
                                {
                                    App.securityAuthorizationKey = respose.result.securityAuthorizationKey;
                                    _navigationService.NavigateTo(App.PaymentProcessWebview, 1);
                                    //await App.Current.MainPage.Navigation.PushAsync(new PaymentProcessWebview());
                                }

                            });
                        }
                        else
                        {

                            ApplePayStatus = await ProcessApplePay();
                        }



                    }

                    IsLoading = false;

                }
                catch (GAZTValidatePaymentInProcessException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        IsLoading = false;
                        await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        _navigationService.GoBack();
                    });
                }
                catch (InternetException )
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {

                        IsLoading = false;
                        //   await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                        _navigationService.GoBack();
                    });
                }
                catch (GAZTNetworkConnectivityIssueException )
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        IsLoading = false;
                        //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));

                    });
                }
            }
            catch (InternetException )
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                    _navigationService.GoBack();
                });
            }
        }

        public async Task<CreateMadaResponseRoot> GetWebviewContent(string srcid)
        {
            try
            {
                var paymentPayload = new CreateMadaPaymentPayload
                {
                    GUID = App.PaymentGuid,
                    sourceId = srcid
                };

                CreateMadaResponseRoot respose = await WebServiceManager.GAZTCreateMadaPayment(paymentPayload);
                return respose;
            }
            catch (GAZTValidateMadaPaymentException ex)
            {
                IsLoading = false;
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    var message = ex.Message.Substring(0, 1).ToUpper() + ex.Message.Substring(1).ToLower();
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(message));
                    //await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    //_navigationService.GoBack();
                });
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task DoProcessApplePayPayment(string fbNum)
        {
            try
            {
                try
                {

                    IsLoading = true;

                    var platform = "";

                    if (DeviceInfo.Platform == DevicePlatform.iOS)
                    {
                        platform = "C4";
                    }
                    else if (DeviceInfo.Platform == DevicePlatform.Android)
                    {
                        platform = "C3";
                    }

                    ApplePayRequestGuid modelDetails = new ApplePayRequestGuid();
                    modelDetails.Fbnum = fbNum;
                    modelDetails.PymntType = "A";
                    modelDetails.Tin = App.LoginDataRetrieved.TIN;
                    modelDetails.Srcid = platform;
                    ApplePayData = await WebServiceManager.GAZTGenerateApplePayGuid(modelDetails);


                    if (ApplePayData != null && ApplePayData.d != null)
                    {

                        if (ApplePayData.d.Guid != null)
                        {

                            App.PaymentGuid = ApplePayData.d.Guid;

                        }

                        var VatAmount = NetdueVat.Replace(",", "");
                        ApplePayStatus = await ProcessApplePay();




                    }

                    IsLoading = false;

                }
                catch (GAZTValidatePaymentInProcessException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        IsLoading = false;
                        await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        _navigationService.GoBack();
                    });
                }
                catch (InternetException )
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        IsLoading = false;
                        //   await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                        _navigationService.GoBack();
                    });
                }
            }
            catch (InternetException )
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                    _navigationService.GoBack();
                });
            }
        }


        public async Task UpdateApplePayPaymentGuid()
        {
            try
            {
                try
                {

                    IsLoading = true;

                    string platform = "C4";

                    if (DeviceInfo.Platform == DevicePlatform.iOS)
                    {
                        platform = "C4";
                    }
                    else if (DeviceInfo.Platform == DevicePlatform.Android)
                    {
                        platform = "C3";
                    }



                    ApplePayToken modelDetails = new ApplePayToken();
                    modelDetails.Guid = App.PaymentGuid;
                    modelDetails.SrcId = platform;

                    //var token = "GrPRb/eyYkhLaxIi8ugsU5I0D2/IE6JT6SYb4o6CH/emQV7n5twiqt8IVazkcItvmCkHXeie16Nvbq+uFFx0mS4O/1+SoDHrP8HcDbJ/Q1swCCHR/Dwv69oTcTUy1riK6Zvpe0w1r+WJ21I36gorRUn7u94Yi9n4afOfnGJC3EmFd6DKSIRQWlT4BuLlNv5826XruanuFjdL3MKty/xoCyx2GKN+e8W6BFVnQc/gsBe4UW7oqHIQ5PrQJlQwymi5Ytd1IIJT8QsUMxiVjz6yVS5zdQBaN86ZtuokJRmC89jCwVkUMwDl9jQ5xYbFlIFS1VXKJjtWKDfMGwCWK3jvWdtCcdb4VrPIxtK7LvTWc+4C7m6SPzkOhdC/XPn7ufwvrh95no7p9tpQMkP7zOJIYAl+hS4oEqvOxdpw55dCytGXJ0yjN/HOQ3t4ofyW9mBGiHoq";
                    modelDetails.PaymentToken = ApplePayTokenData;



                    ApplePayTokenResponse response = await WebServiceManager.GAZTUpdateApplePayGuid(modelDetails);


                    if (response != null && response.d != null)
                    {



                        if (response.d.Success)
                        {

                            MainThread.BeginInvokeOnMainThread(() =>
                            {

                                //_navigationService.NavigateTo(App.ZakatReturnNewSuccessPageView, PaymentData.d.PayRef);
                                //await App.Current.MainPage.Navigation.PushAsync(new PaymentProcessWebview());

                                PaymentSucess paymentInfo = new PaymentSucess();
                                paymentInfo.Paymentref = response.d.PayRef;
                                if (response.d.PerslTxt != null)
                                {
                                    paymentInfo.Period = response.d.PerslTxt;
                                }

                                _navigationService.NavigateTo(App.VatReturnNewSuccessPageView, paymentInfo);

                            });
                        }
                        else
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {

                                await MopupService.Instance.PushAsync(new PaymentExceptionPageView());
                                //PaymentSucess paymentInfo = new PaymentSucess();
                                //paymentInfo.Paymentref = "";
                                //if (response.d.PerslTxt != null)
                                //{
                                //    paymentInfo.Period = response.d.PerslTxt;
                                //}

                                //_navigationService.NavigateTo(App.VatReturnNewSuccessPageView, paymentInfo);

                            });

                        }



                        //MainThread.BeginInvokeOnMainThread(() =>
                        //{

                        //    //_navigationService.NavigateTo(App.ZakatReturnNewSuccessPageView, PaymentData.d.PayRef);
                        //    //await App.Current.MainPage.Navigation.PushAsync(new PaymentProcessWebview());


                        //    _navigationService.NavigateTo(App.VatReturnNewSuccessPageView, "");

                        //});
                    }
                    IsLoading = false;

                }
                catch (GAZTValidatePaymentInProcessException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        IsLoading = false;
                        await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        _navigationService.GoBack();
                    });
                }
                catch (InternetException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        IsLoading = false;
                        //   await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                        _navigationService.GoBack();
                    });
                }
            }
            catch (InternetException )
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                    _navigationService.GoBack();
                });
            }
        }

        private async Task<bool> ProcessApplePay()
        {



            //var VatAmount = NetdueVat.Replace(",", "");

            var Amount = Convert.ToDouble(PaymentData.d.Amount);
            var VatAmount = Math.Round(Amount, 2);

            DependencyService.Get<IApplePayAuthorizer>().IsPaymentFromDashboard(false);
            return DependencyService.Get<IApplePayAuthorizer>().AuthorizePayment(VatAmount, AppResources.ApplePayText);
        }



        public void MadaPaymentSelected()
        {

            DoValidatePayment(fbNum: VATDeclarationData.data.Fbnum, "Mada Payment");

            //MainThread.BeginInvokeOnMainThread(async () => {

            //    _navigationService.NavigateTo(App.PaymentProcessWebview,1);
            //    //await App.Current.MainPage.Navigation.PushAsync(new PaymentProcessWebview());

            //});

        }

        public void ApplePaySelected()
        {
            //DoProcessApplePayPayment(VATDeclarationData.d.Fbnum);

            DoValidatePayment(fbNum: VATDeclarationData.data.Fbnum, "A");

            //var payment = DependencyService.Get<IApplePayAuthorizer>().AuthorizePayment("1","VAT Return");


        }

        public async Task SadadPaymentSelected()
        {

            _navigationService.NavigateTo(App.VATReturnSuccessfullPageView, VATDeclarationData);

        }


        public async void ApplePaySucess()
        {

            Console.WriteLine("Apple pay status", ApplePayTokenData);

            if (ApplePayTokenData != null)
            {

                await UpdateApplePayPaymentGuid();
            }


        }
    }
}
