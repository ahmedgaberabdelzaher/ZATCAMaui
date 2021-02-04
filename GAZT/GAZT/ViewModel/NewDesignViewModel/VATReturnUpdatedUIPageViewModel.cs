using EGAZT.Views.NewDesign.VATDeclarationPages;
using GalaSoft.MvvmLight.Views;
using Rg.Plugins.Popup.Services;
using System.Windows.Input;
using Xamarin.Forms;
using EGAZT.Models;
using System.Threading.Tasks;
using GAZT.Manager;
using GAZT.Models;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using GAZT.Helper;
using System.Text;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    [Preserve(AllMembers = true)]
    public class GAZTNewDesignVATReturnUpdatedUIPageViewModel : BaseViewModel
    {

        public ICommand OnBackStepClicked { get; set; }
        public ICommand OnMoreClicked { get; set; }
        public ICommand OnBackButtonClicked { get; set; }
        
        public ICommand onCountinueClicked { get; set; }

        public ICommand onSecondButtonClicked { get; set; }

        public ICommand onRefundClicked { get; set; }

        public ICommand onCreditForwardClicked { get; set; }

        public ICommand ChangeRegistrationClicked { get; set; }


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
                
                RaisePropertyChanged(nameof(currentTab));
                CurrentIndex = (int)_currentTab;
                RaisePropertyChanged(nameof(CurrentIndex));
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
                RaisePropertyChanged("CurrentOpenedTab");
            }
        }


        private int _currenrIndex = 1;
        public int CurrentIndex
        {
            get => _currenrIndex;
            set
            {
                if (_currenrIndex == value) return;

                if (VATDeclarationData != null && VATDeclarationData.d != null && VATDeclarationData.d.GoliveFg == "X")
                 {
                    _currenrIndex = value;
                 }
                 else
                 {

                    _currenrIndex = value > 1 ? value - 1 : value;
                 }
                
                RaisePropertyChanged(nameof(CurrentIndex));
                if (_currenrIndex == MaxIndex)
                    {
                        MarkComplete = true;
                        RaisePropertyChanged(nameof(MarkComplete));
                    }
                    else
                    {
                        MarkComplete = false;
                        RaisePropertyChanged(nameof(MarkComplete));
                    }
            }
        }
        public bool MarkComplete { get; private set; } = false;
        //public int MaxIndex { get; set; } = 6;
        #endregion


        private int _maxIndex=6;
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
                RaisePropertyChanged("MaxIndex");
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
                RaisePropertyChanged("IsRefundButtonEnabled");
            }
        }

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
                RaisePropertyChanged("IsRefundButtonVisible");
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
                RaisePropertyChanged("IsUnFocusedTextBox");
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
                RaisePropertyChanged("IsMainButtonEnabled");
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
                RaisePropertyChanged("IsCreditForwardBtnVisible");
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
                RaisePropertyChanged("IsVATReturnFieldCheckForSaveAsDraft");
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
                RaisePropertyChanged("VATDeclarationData");
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
                RaisePropertyChanged("VATDeclarationDataDummy");
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
                if (_stepNumber == value) return;

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
                if (_stepNumberz == value) return;

                _stepNumberz = value;
                RaisePropertyChanged("StepNumberz");
            }
        }

        private bool _isDeclarationCheckEnabled = false;
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
                RaisePropertyChanged("IsDeclarationCheckEnabled");
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
                RaisePropertyChanged("IsTaxPayerCheckEnabled");
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
                RaisePropertyChanged("IsYesBoxEnabled");
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
                RaisePropertyChanged("IsNoBoxEnabled");
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
                RaisePropertyChanged("IsSwichButtonEnable");
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
                RaisePropertyChanged("IsRefundNoMsgDisplayed");
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
                RaisePropertyChanged("IsControlEnabledForEntry");
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
                RaisePropertyChanged("IsControlEnabled");
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
                RaisePropertyChanged("IsSwitchVisible");
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
                RaisePropertyChanged("IsSummaryCheckEnabled");
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
                if (_taxpayerPeriodToDate == value) return;

                _taxpayerPeriodToDate = value;
                RaisePropertyChanged("TaxpayerPeriodToDate");
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
                RaisePropertyChanged("ATTACHSetsList");
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
                RaisePropertyChanged("DummyATTACHSetsList");
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
                RaisePropertyChanged("CreditCarriedsList");
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
                RaisePropertyChanged("FullAddress");
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
                RaisePropertyChanged("CalculationRateSet");
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
                RaisePropertyChanged("CalculationRateSetVTTH");
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
                RaisePropertyChanged("CalculationRateIGRTSet");
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
                RaisePropertyChanged("IBANList");
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
                RaisePropertyChanged("IBANTypesList");
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
                if (_vATRate002 == value) return;

                _vATRate002 = value;
                RaisePropertyChanged("VATRate002");
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
                RaisePropertyChanged("VATRate003");
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
                RaisePropertyChanged("CorrectionPeriodAmount");
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
                RaisePropertyChanged("CorrectionNegativePeriodAmount");
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
                RaisePropertyChanged("CarriedValueString");
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
                RaisePropertyChanged("CarriedValueStringNew");
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
                RaisePropertyChanged("IsCarriedForwandReviewMessage");
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
                    
                    VATDeclarationData.d.ConfStp2 = "1";
                }
                else
                {
                    
                    VATDeclarationData.d.ConfStp2 = "0";
                }
                RaisePropertyChanged("IsCheckedTaxPayerDetailsInfo");
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
                    VATDeclarationData.d.DecFg = "1";
                    SetEnableForSubmitButton();
                }
                else
                {
                    VATDeclarationData.d.DecFg = "0";
                    IsMainButtonEnabled = false;
                    SetEnableForSubmitButton();
                }
                RaisePropertyChanged("IsDeclarationCheckedForSummary");
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
                RaisePropertyChanged("VatAttachmentsList");
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
                RaisePropertyChanged("ResponseVATDeclarationD");
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
                RaisePropertyChanged("VATNewModelFor15Percent");
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
                RaisePropertyChanged("VATNewModelFor5Percent");
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
                RaisePropertyChanged("ResponseNote");
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
                RaisePropertyChanged("Responseobject");
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
                if (_responseCFSET == value) return;

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
                if (_responseAttachSet == value) return;

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
                if (_responseAddressSET == value) return;

                _responseAddressSET = value;
                RaisePropertyChanged("ResponseResult5");
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
                RaisePropertyChanged("TotalsalesAmt");
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
                RaisePropertyChanged("TotalsalesAdj");
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
                RaisePropertyChanged("TotalpurchaseAmt");
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
                RaisePropertyChanged("TotalpurchaseAdj");
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
                RaisePropertyChanged("StdsalesVat");
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
                RaisePropertyChanged("IsGetSadadNumberEnabled");
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
                                if ((Convert.ToDecimal(_preperiodcorr) >= Convert.ToDecimal(CorrectionPeriodAmount)) || (Convert.ToDecimal(_preperiodcorr) <= Convert.ToDecimal(CorrectionNegativePeriodAmount)))
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
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());
                }
                RaisePropertyChanged("Preperiodcorr");
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
                RaisePropertyChanged("StdsalesVat15");
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
                RaisePropertyChanged("StdsalesVat5");
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
                RaisePropertyChanged("StdpurchasesVat15");
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
                RaisePropertyChanged("StdpurchasesVat5");
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
                RaisePropertyChanged("ImportspaidVat15");
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
                RaisePropertyChanged("ImportspaidVat5");
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
                RaisePropertyChanged("ImportsaccVat15");
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
                RaisePropertyChanged("ImportsaccVat5");
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
                if (_entryVatAmountTextColor == Color.FromHex("#ff0000"))
                {
                    IsMainButtonEnabled = false;
                }
                else
                {
                    IsMainButtonEnabled = true;
                }
                RaisePropertyChanged("EntryVatAmountTextColor");
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
                RaisePropertyChanged("IsGreaterThanFiveT");
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
                        if (Preperiodcorr != null && Preperiodcorr.Contains("-") && (!string.IsNullOrEmpty(Preperiodcorr)))
                            Preperiodcorr = Preperiodcorr.Replace("-", "");
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());
                }
                RaisePropertyChanged("IsSwitchToggled");
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
                if (_creditVat == value) return;

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
                if (_totalsalesVat == value) return;

                _totalsalesVat = value;
                if (!string.IsNullOrEmpty(_totalsalesVat))
                {
                    if (string.IsNullOrEmpty(TotalsalesVat) || (TotalsalesVat == "0"))
                    {
                        TotalsalesVat = "0.00";
                    }
                    if (string.IsNullOrEmpty(TotalpurchaseVat) || (TotalpurchaseVat == "0"))
                    {
                        TotalpurchaseVat = "0.00";
                    }
                    TotaldueVat = (Convert.ToDouble(TotalsalesVat) - Convert.ToDouble(TotalpurchaseVat)).ToString();
                    if (TotaldueVat == "0")
                    {
                        TotaldueVat = "0.00";
                    }
                    if (!String.IsNullOrEmpty(TotaldueVat) && TotaldueVat != "0.00")
                    {
                        TotaldueVat = UtilityManager.GetCommaSeparatedAmount(TotaldueVat);
                    }
                }
                RaisePropertyChanged("TotalsalesVat");
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
                RaisePropertyChanged("StdpurchasesVat");
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
                RaisePropertyChanged("ImportspaidVat");
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
                    if (!String.IsNullOrEmpty(TotaldueVat) && TotaldueVat != "0.00")
                    {
                        TotaldueVat = UtilityManager.GetCommaSeparatedAmount(TotaldueVat);
                    }
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
                if (_amountPayable == value) return;

                _amountPayable = value;
                RaisePropertyChanged("AmountPayable");
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
                RaisePropertyChanged("ImportsaccVat");
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
        //        RaisePropertyChanged("BoxSevenFrame");
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
        //        RaisePropertyChanged("BoxSixFrame");
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
        //        RaisePropertyChanged("BoxFiveFrame");
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
        //        RaisePropertyChanged("sevenbox");
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
        //        RaisePropertyChanged("sixbox");
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
        //        RaisePropertyChanged("fivebox");
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
                RaisePropertyChanged("IsNavigatedToSubmitted");
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
                RaisePropertyChanged("RefundButtonText");
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
                RaisePropertyChanged("isBtnVisible");
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
                RaisePropertyChanged("IsMainButtonVisible");
            }
        }

        public bool _isVoidClicked=false;
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
                RaisePropertyChanged("IsVoidClicked");
            }
        }

        public bool _isResetClicked=false;
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
                RaisePropertyChanged("IsResetClicked");
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
        //        RaisePropertyChanged("isCheckVisible");
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
        //        RaisePropertyChanged("OuterFrame");
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
        //        RaisePropertyChanged("secOuterFrame");
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
        //        RaisePropertyChanged("innerFrame");
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
        //        RaisePropertyChanged("secBox");
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
        //        RaisePropertyChanged("thirdBox");
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
        //        RaisePropertyChanged("fourthBox");
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
        //        RaisePropertyChanged("IsInstrunctionView");
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
        //        RaisePropertyChanged("IsSaleView");
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
        //        RaisePropertyChanged("IsPurchaseView");
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
        //        RaisePropertyChanged("IsTotalVatView");
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
        //        RaisePropertyChanged("IsSummeryView");
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
        //        RaisePropertyChanged("IsVATReturnsView");
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
        //        RaisePropertyChanged("IsTaxpayerView");
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
                RaisePropertyChanged("CreditDetailsText");
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
                RaisePropertyChanged("OnMoreOptionsEnabled");
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
                RaisePropertyChanged("IsNewLoading");
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
                if(_isVisibleAmendButton==true)
                {
                    RefundButtonText = AppResources.ZZZZViewRefund;
                }
                else
                {
                    RefundButtonText = AppResources.ZZZZConfirmAndRefundRequest;
                }
                RaisePropertyChanged("IsVisibleAmendButton");
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
                RaisePropertyChanged("IsAmendButtonAvailable");
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
                RaisePropertyChanged("IsAmendClicked");
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
                if (_DummyListOfActionButtonsApplicable == value) return;

                _DummyListOfActionButtonsApplicable = value;
                RaisePropertyChanged("DummyListOfActionButtonsApplicable");
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
                RaisePropertyChanged("IsEnableSwitchToggledFor15PercentChange");
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
                
                RaisePropertyChanged("IsSwitchToggledFor15PercentChange");
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
                RaisePropertyChanged("IsYesChecked");
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
                RaisePropertyChanged("IsNoChecked");
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
                RaisePropertyChanged("IsNewReturn");
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
                RaisePropertyChanged("IsFifteenPercentChange");
            }
        }

        //private bool _isNavigatedToBilled = false;
        //public bool IsNavigatedToBilled
        //{
        //    get
        //    {
        //        return _isNavigatedToBilled;
        //    }
        //    set
        //    {
        //        _isNavigatedToBilled = value;
        //        RaisePropertyChanged("IsNavigatedToBilled");
        //    }
        //}


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
                RaisePropertyChanged("IsFivePersenctVisible");
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
                RaisePropertyChanged("IsFifteenPersenctVisible");
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
                RaisePropertyChanged("IsPrevReturn");
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
                RaisePropertyChanged("VATRate002For15Percent");
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
                RaisePropertyChanged("VATRate003For5Percent");
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
                RaisePropertyChanged("ContinueText");
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
                RaisePropertyChanged("YesBackgroundImage");
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
                RaisePropertyChanged("NoBackgroundImage");
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
                RaisePropertyChanged("YesLabelColor");
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
                RaisePropertyChanged("NoLabelColor");
            }
        }


        #endregion

        #region Constructor
        public GAZTNewDesignVATReturnUpdatedUIPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
           

         
            ContinueText = AppResources.ZZZZContinue;

            OnMoreClicked = new Xamarin.Forms.Command(() =>
            {
                if (IsNavigatedToSubmitted == false)
                {
                    PopupNavigation.Instance.PushAsync(new MorePopUpPageView(ListOfActionButtonsApplicable));
                }
            });

            OnBackButtonClicked = new Xamarin.Forms.Command(() =>
            {
                _navigationService.GoBack();
            });

            onRefundClicked = new Xamarin.Forms.Command(() =>
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
                                    headerAmountInfo.Message = AppResources.ZZZRefundEnableMessage;

                                    headerWithInfos.Add(headerAmountInfo);


                                    newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                                    newDesignPopUp.HeaderWithInfos = headerWithInfos;
                                    newDesignPopUp.MainHeader = AppResources.ZZZConfirmationMsg;

                                    PopupNavigation.Instance.PushAsync(new ShowVatInformationConfirmationPageView(newDesignPopUp));
                                }
                                else
                                {
                                SetDataForRefundPopup();
                                PopupNavigation.Instance.PushAsync(new RefundAccountPopupPageView(VATDeclarationData));
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

                            PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
                        }
                        // _navigationService.NavigateTo(App.RefundAccountPopupPageView,VATDeclarationData);
                        //_navigationService.GoBack();
                    
                }
                catch(Exception ex)
                {
                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());
                }
            });

            onCreditForwardClicked = new Xamarin.Forms.Command(() =>
            {
                try
                {
                    PopupNavigation.Instance.PushAsync(new VATCreditCarriedForwardPopUpPageView(VATDeclarationData));
                }
                catch(Exception ex)
                {
                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());
                }
            });

            ChangeRegistrationClicked = new Xamarin.Forms.Command(() =>
            {
                try
                {
                    List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                    HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                    NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                    headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                    headerAmountInfo.IsLinkAvailable = false;
                    headerAmountInfo.Message = AppResources.ZZZChangeRegistationNote;

                    headerWithInfos.Add(headerAmountInfo);


                    newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                    newDesignPopUp.HeaderWithInfos = headerWithInfos;
                    newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                    PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                }
                catch(Exception ex)
                {
                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());
                }
               // await _dialogService.ShowMessage(AppResources.ZZZChangeRegistationNote, AppResources.ZInstructions);
            });




            onSecondButtonClicked = new Xamarin.Forms.Command(async() =>
            {
                if (CreditDetailsText == AppResources.ZZZZConfirmandGenerateSADADBill)
                {
                   
                        if (IsDeclarationCheckedForSummary)
                        {
                            if (IsDeclarationCheckedForSummary && (IsVoidClicked == false && IsResetClicked == false))
                            {
                                if (((App.ICRStatus == "E0045" || App.ICRStatus == "E0006") && (IsAmendClicked == true)) || (App.ICRStatus == "E0001" || IsCheckedDraftMode()))
                                {
                                    Device.BeginInvokeOnMainThread(() =>
                                    {
                                        IsNewLoading = true;
                                    });
                                    await SubmitClicked();
                                    Device.BeginInvokeOnMainThread(() =>
                                    {
                                        IsNewLoading = false;
                                    });
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

                           await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
                        }
                    
                }
                else if(CreditDetailsText == AppResources.ZZZZGetAckNew)
                {
                    _navigationService.NavigateTo(App.VATReturnSuccessfullPageView, VATDeclarationData);
                }
            });

            onCountinueClicked = new Xamarin.Forms.Command(() =>
            {




            });

            OnBackStepClicked = new Xamarin.Forms.Command(() =>
            {
                switch (currentTab)
                {
                    //case VATReturnUpdatedUITabEnum.TaxpayerDetails:
                    //    currentTab = VATReturnUpdatedUITabEnum.Instrunction;
                    //    break;
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

        public bool checkforrefundclicked()
        {
            bool result = false;
            if (App.ICRStatus=="E0045" || App.ICRStatus=="E0006")
            {
                if (VATDeclarationData.d.RefundFg == "1")
                {
                    result = true;
                }
                else if(IsAmendClicked==true)
                {
                    result = true;
                }
            }
            else if((App.ICRStatus == "E0001" || IsCheckedDraftMode()))
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
            //var answer = await Application.Current.MainPage.DisplayAlert(AppResources.Information, AppResources.ZZGeneralMessage_AllInfoFilledInTheFormWillBeLost, AppResources.ZYes, AppResources.ZNo);
            //if (answer)


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

            await PopupNavigation.Instance.PushAsync(new ShowVatInformationConfirmationPageView(newDesignPopUp));

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
                        var res = await SaveReturnAndGetReturnAndSetButtons();
                        if (res != null && res.d != null && response != null)
                        {
                            Device.BeginInvokeOnMainThread( () =>
                            {
                                ManageEnabledProperty(false);
                                IsVoidClicked = true;
                               // IsMainButtonVisible = false;
                                
                            });
                            // ManageEnabledProperty(false);
                            Device.BeginInvokeOnMainThread(async () =>
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

                                await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                            });
                        }
                        else
                        {
                            IsLoading = false;
                            if (string.IsNullOrEmpty(WebServiceManager.ErrorMessageForVAT))
                            {
                                Device.BeginInvokeOnMainThread(async () => {


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

                                   await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));



                                    //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                    _navigationService.GoBack();
                                });
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(async () => {

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

                                  await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                                    //await _dialogService.ShowMessage(WebServiceManager.ErrorMessageForVAT, AppResources.Information);
                                    // _navigationService.GoBack();
                                    WebServiceManager.ErrorMessageForVAT = string.Empty;
                                });
                            }
                            //Device.BeginInvokeOnMainThread(async () =>
                            //{
                            //    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.ZInstructions);
                            //});
                        }
                    }
                    catch (InternetException ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
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
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsNewLoading = true;
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
                    var res = await SaveReturnAndGetReturnAndSetButtons();
                    if (res != null && res.d != null)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            if (currentTab== VATReturnUpdatedUITabEnum.Instrunction)
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
                            headerAmountInfo.Message = string.Format(AppResources.DraftSaved, "  " + res.d.Fbnum);

                            headerWithInfos.Add(headerAmountInfo);


                            newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                            newDesignPopUp.HeaderWithInfos = headerWithInfos;
                            newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                           await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));




                            //await _dialogService.ShowMessage(string.Format(AppResources.DraftSaved, "  " + res.d.Fbnum), AppResources.Information);
                        });
                    }
                    else
                    {
                        IsLoading = false;
                        if (string.IsNullOrEmpty(WebServiceManager.ErrorMessageForVAT))
                        {
                            Device.BeginInvokeOnMainThread(async () =>
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

                              await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));


                                
                            });
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
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

                               await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                                WebServiceManager.ErrorMessageForVAT = string.Empty;
                            });
                        }
                        //Device.BeginInvokeOnMainThread(async () =>
                        //{
                        //    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        //});
                    }
                });
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsNewLoading = false;
                });
            }
            catch (Exception)
            {
                Device.BeginInvokeOnMainThread(async () => {

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

                   await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));



                    //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                });
            }
        }


        public async Task VATReturnResetAsync()
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
                    var res = await SaveReturnAndGetReturnAndSetButtons();



                    if (res != null && res.d != null && response != null)
                    {

                        VATDeclaration _vATDeclaration = await WebServiceManager.GAZTGetVATReturns(App.VATDeclrationFbguid, VATDeclarationData.d.Fbnumz, App.EUser, "");
                        PopToRootPage();
                        if (_vATDeclaration != null && _vATDeclaration.d != null)
                        {
                            VATDeclarationData = _vATDeclaration;
                            ResponseVATDeclarationD = VATDeclarationData.d;

                            SetCommasforAll();
                            if (DummyATTACHSetsList != null && DummyATTACHSetsList.Count() != 0)
                            {
                                VATDeclarationData.d.ATTACHSet.results = DummyATTACHSetsList;
                            }
                            //SetData();
                        }


                        Device.BeginInvokeOnMainThread(() =>
                        {
                            ManageEnabledProperty(false);
                            IsResetClicked = true;
                            //IsMainButtonVisible = false;

                        });
                        Device.BeginInvokeOnMainThread(async () =>
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

                           await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));



                           // _dialogService.ShowMessage(AppResources.ZZGeneralMessage_ReturnRestoredToTheLastBilledVersion, AppResources.Information);
                        });
                    }
                    else
                    {
                        IsLoading = false;
                        if (string.IsNullOrEmpty(WebServiceManager.ErrorMessageForVAT))
                        {
                            Device.BeginInvokeOnMainThread(async () => {

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

                              await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));



                                //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                _navigationService.GoBack();
                            });
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () => {


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

                                await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));



                               // await _dialogService.ShowMessage(WebServiceManager.ErrorMessageForVAT, AppResources.Information);
                                // _navigationService.GoBack();
                                WebServiceManager.ErrorMessageForVAT = string.Empty;
                            });
                        }
                        //Device.BeginInvokeOnMainThread(async () =>
                        //{
                        //    _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        //});
                    }
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
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

                        await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));


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
                string periodto = JsonConvert.DeserializeObject<DateTime>(@"""" + VATDeclarationData.d.Abrzo + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                TimeSpan TS = DateTime.Now - Convert.ToDateTime(periodto);
                double Years = TS.TotalDays / 365.25;
                if (Years >= 5)
                {
                    Device.BeginInvokeOnMainThread(async () =>
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

                       await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));



                       // await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_IfTimePeriodOfAmendmentIsLapsed, AppResources.Information);
                    });
                    return;
                }
                CreateDataForPost();
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
                var res = await SaveReturnAndGetReturnAndSetButtons();
                if (res != null && res.d != null)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        ManageEnabledProperty(true);
                        IsMainButtonVisible = true;
                        if ((App.ICRStatus == "E0045" || App.ICRStatus == "E0006") && VATDeclarationData.d.Yesno == "X")
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
                        Device.BeginInvokeOnMainThread(async () => {


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

                           await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));




                           // await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                            _navigationService.GoBack();
                        });
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () => {



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

                           await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));



                            //await _dialogService.ShowMessage(WebServiceManager.ErrorMessageForVAT, AppResources.Information);
                            // _navigationService.GoBack();
                            WebServiceManager.ErrorMessageForVAT = string.Empty;
                        });
                    }
                    //Device.BeginInvokeOnMainThread(async () =>
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
                ContinueText = AppResources.ZZZZContinue;
            }
            else if (currentTab == VATReturnUpdatedUITabEnum.Sales)
            {
                isBtnVisible = false;
                IsMainButtonVisible = true;
                IsRefundButtonVisible = false;
                IsCreditForwardBtnVisible = false;
                IsVisibleAmendButton = false;
                ContinueText = AppResources.ZZZZContinue;
            }
            else if (currentTab == VATReturnUpdatedUITabEnum.Purchase)
            {
                isBtnVisible = false;
                IsMainButtonVisible = true;
                IsRefundButtonVisible = false;
                IsCreditForwardBtnVisible = false;
                IsVisibleAmendButton = false;
                ContinueText = AppResources.ZZZZContinue;
            }
            else if (currentTab == VATReturnUpdatedUITabEnum.TotalVat)
            {
                isBtnVisible = false;
                IsMainButtonVisible = true;
                IsRefundButtonVisible = false;
                IsCreditForwardBtnVisible = true;
                IsVisibleAmendButton = false;
                ContinueText = AppResources.ZZZZContinue;
            }
            else if (currentTab == VATReturnUpdatedUITabEnum.Summery)
            {
                IsCreditForwardBtnVisible = false;
                if(IsAmendButtonAvailable)
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
                    if (VATDeclarationData.d.RefundFg == "1")
                    {
                        IsRefundButtonVisible = true;
                    }
                    else if (IsAmendClicked == true || VATDeclarationData.d.RefundFg != "1")
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
                            if ((App.ICRStatus == "E0045" || App.ICRStatus == "E0006" || App.ICRStatus == "E0055" || App.ICRStatus == "E0058") && IsAmendClicked == true && VATDeclarationData.d.RefundFg != "1")
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
                            CreditDetailsText = AppResources.ZZZZConfirmandGenerateSADADBill;
                        }
                    }
                    else
                    {
                        isBtnVisible = false;
                        IsMainButtonVisible = false;
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
                string periodKey = VATDeclarationData.d.Periodkeyz;
                string TxnTp = VATDeclarationData.d.TxnTpz;
                string status = VATDeclarationData.d.Statusz;
                if (status == "E057" || status == "E0057" || status == "E058" || status == "E0058")
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_ReturnUnderReviewWithGAZT, AppResources.Information);
                    });
                }
                string FormBundleNumber = VATDeclarationData.d.Fbnum;
                string Gpart = VATDeclarationData.d.Gpart;
                
                TaxpayerPeriodFromDate = JsonConvert.DeserializeObject<DateTime>(@"""" + VATDeclarationData.d.Abrzu + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                TaxpayerPeriodToDate = JsonConvert.DeserializeObject<DateTime>(@"""" + VATDeclarationData.d.Abrzo + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
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
                if (VATDeclarationData.d.ATTACHSet.results != null && VATDeclarationData.d.ATTACHSet.results.Count != 0)
                {
                    ATTACHSetsList = new List<Attachment>();
                    foreach (var item in VATDeclarationData.d.ATTACHSet.results)
                    {
                        Attachment a = new Attachment();
                        a = item;
                        ATTACHSetsList.Add(a);
                    }
                }
                if (VATDeclarationData.d.CFSet.results != null && VATDeclarationData.d.ADRSet.results.Count != 0)
                {
                    CreditCarriedsList = VATDeclarationData.d.CFSet.results;
                }
                if (VATDeclarationData.d.ADRSet.results.Count > 0)
                {
                    FullAddress = VATDeclarationData.d.ADRSet.results[0].BuildingNo + " " + VATDeclarationData.d.ADRSet.results[0].Street + " " + VATDeclarationData.d.ADRSet.results[0].Quarter + " " + VATDeclarationData.d.ADRSet.results[0].RegionDesc + " " + VATDeclarationData.d.ADRSet.results[0].City + " " + Environment.NewLine + VATDeclarationData.d.ADRSet.results[0].PostalCd;
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
                        CalculationRateIGRTSet = new List<IGRTSetResult>();
                        CalculationRateIGRTSet = vATCalculationData.d.IGRTSet.results;
                        RateSetAsPerDate();
                    }
                    if (vATCalculationData.d.VTTHSet.results.Count != 0)
                    {
                        CorrectionPeriodAmount = vATCalculationData.d.VTTHSet.results.Where(x => x.Type == "001").Select(x => x.MaxVal).FirstOrDefault();
                        CorrectionNegativePeriodAmount = vATCalculationData.d.VTTHSet.results.Where(x => x.Type == "001").Select(x => x.MinVal).FirstOrDefault();
                        if (!string.IsNullOrEmpty(CorrectionPeriodAmount))
                        {
                            if (App.IsArabic)
                            {
                                CarriedValueString = AppResources.ZVatCorrectionsfrompreviousperiod.Replace("±", CorrectionPeriodAmount + " ± ");
                                CarriedValueStringNew= AppResources.ZVatCorrectionsfrompreviousperiod1.Replace("±", CorrectionPeriodAmount + " ± ");
                            }
                            else
                            {
                                CarriedValueString = AppResources.ZVatCorrectionsfrompreviousperiod.Replace("±", " ± " + CorrectionPeriodAmount);
                                CarriedValueStringNew= AppResources.ZVatCorrectionsfrompreviousperiod1.Replace("±", " ± " + CorrectionPeriodAmount);
                            }
                        }
                    }
                }
                if (VATDeclarationData.d.TcFg == "1")
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


                ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(VATDeclarationData.d.ATTACHSet.results as List<Attachment>);
                VatAttachmentsList = myCollection;
                if (VATDeclarationData != null)
                {
                    // SetPageForDraft();
                    if (VATDeclarationData.d != null)
                    {
                        ResponseVATDeclarationD = VATDeclarationData.d;
                        if (ResponseVATDeclarationD.GoliveFg == "X")
                        {
                            VATNewModelFor15Percent = VATDeclarationData.d.VATPERITEMSet.results.Where(x => x.Type == "002").FirstOrDefault();
                            VATNewModelFor5Percent = VATDeclarationData.d.VATPERITEMSet.results.Where(x => x.Type == "003").FirstOrDefault();
                        }
                        SetData();
                        SetCommasforAll();
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

                ManageThePreperiodcorrSwitch();
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                   await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }
        }

        public void SetEnableForSubmitButton()
        {
            if (IsDeclarationCheckedForSummary==true)
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
                VATDeclarationData.d.Yesno = "X";
            }
            else
            {
                VATDeclarationData.d.Yesno = string.Empty;
            }




            if (IsDeclarationCheckedForSummary == true)
            {
                VATDeclarationData.d.DecFg = "1";
            }
            else
            {
                VATDeclarationData.d.DecFg = "0";
            }
        }

        private async Task<VATDeclaration> SaveReturnAndGetReturnAndSetButtons()
        {
            try
            {
                //New code for VAT 15% Change

                if (IsYesChecked == true)
                {
                    VATDeclarationData.d.Yesno = "X";
                }
                else
                {
                    VATDeclarationData.d.Yesno = string.Empty;
                }




                if (IsDeclarationCheckedForSummary == true)
                {
                    VATDeclarationData.d.DecFg = "1";
                }
                else
                {
                    VATDeclarationData.d.DecFg = "0";
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
                if (VATDeclarationData != null && VATDeclarationData.d != null && VATDeclarationData.d.ATTACHSet.results.Count() != 0)
                {
                    DummyATTACHSetsList = new List<Attachment>();
                    DummyATTACHSetsList = VATDeclarationData.d.ATTACHSet.results;
                }
                if (ATTACHSetsList != null && ATTACHSetsList.Count() != 0)
                {
                    VATDeclarationData.d.ATTACHSet.results = ATTACHSetsList;
                }
                VATDeclaration response = await WebServiceManager.SaveVATDeclarationData(VATDeclarationData);
                PopToRootPage();
                if (response != null && response.d != null && !string.IsNullOrEmpty(response.d.Fbnum))
                {
                    try
                    {
                        if (response != null && response.d != null)
                        {
                            VATDeclarationData = response;
                            ResponseVATDeclarationD = VATDeclarationData.d;
                            if (VATDeclarationData.d.VATPERITEMSet.results != null)
                            {
                                if (VATDeclarationData.d.GoliveFg == "X" )
                                {
                                    if(VATDeclarationData.d.Yesno == "X")
                                    {
                                        VATNewModelFor15Percent = VATDeclarationData.d.VATPERITEMSet.results.Where(x => x.Type == "002").FirstOrDefault();
                                        VATNewModelFor5Percent = VATDeclarationData.d.VATPERITEMSet.results.Where(x => x.Type == "003").FirstOrDefault();
                                    }
                                    else
                                    {
                                        VATNewModelFor15Percent = VATDeclarationData.d.VATPERITEMSet.results.Where(x => x.Type == "002").FirstOrDefault();
                                    }
                               }
                            }
                            SetCommasforAll();
                            if (DummyATTACHSetsList != null && DummyATTACHSetsList.Count() != 0)
                            {
                                VATDeclarationData.d.ATTACHSet.results = DummyATTACHSetsList;
                            }
                            if (VATDeclarationData.d.Operationz == "01" && App.ICRStatus == "E0001")
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
                            Device.BeginInvokeOnMainThread(() =>
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

                    if (string.IsNullOrEmpty(VATDeclarationData.d.Fbnum))
                    {

                        VATDeclaration _vATDeclarationForGet = await WebServiceManager.GAZTGetVATReturns(App.Fbguid, VATDeclarationData.d.Fbnumz, App.EUser, "");
                        PopToRootPage();
                        if (_vATDeclarationForGet != null && _vATDeclarationForGet.d != null)
                        {
                            if (!string.IsNullOrEmpty(_vATDeclarationForGet.d.Fbnum))
                            {
                                //
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(async () =>
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

                                    await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));


                                  //  await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                   
                                    
                                    
                                    
                                    _navigationService.GoBack();
                                });
                            }
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
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

                                await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));


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
                    VATDeclarationData.d.StepNumberz = "04";
                    VATDeclarationData.d.UserTypz = "TP";
                    VATDeclarationData.d.Operationz = operation;
                    var res = await SaveReturnAndGetReturnAndSetButtons();
                    if (res != null && res.d != null)
                    {
                        Device.BeginInvokeOnMainThread(() =>
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
                        _navigationService.NavigateTo(App.VATReturnSuccessfullPageView,VATDeclarationData);
                       // _navigationService.NavigateTo(App.AcknowledgementDetailsPageView, VATDeclarationData);
                    }
                    else
                    {
                        IsLoading = false;
                        if (string.IsNullOrEmpty(WebServiceManager.ErrorMessageForVAT))
                        {
                            Device.BeginInvokeOnMainThread(async () =>
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

                               await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));


                                // await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                 _navigationService.GoBack();
                            });
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
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

                               await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));


                                //await _dialogService.ShowMessage(WebServiceManager.ErrorMessageForVAT, AppResources.Information);
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
                VATDeclarationData.d.StepNumber = "04";
                VATDeclarationData.d.StepNumberz = "04";
                VATDeclarationData.d.UserTypz = "TP";
                VATDeclarationData.d.Operationz = operation;
                VATDeclaration response = new VATDeclaration();
                resNew = await SaveReturnAndGetReturnAndSetButtons();
                // }
                if (resNew != null && resNew.d != null)
                {
                    VATDeclarationDataDummy = resNew;
                    decimal FourteenA = 0;
                    if (!string.IsNullOrEmpty(TotaldueVat) && !string.IsNullOrEmpty(Preperiodcorr))
                    {
                        FourteenA = Convert.ToDecimal(TotaldueVat) + Convert.ToDecimal(Preperiodcorr);
                    }
                    if ((IsSwichButtonEnable == false && FourteenA < 5000 && Convert.ToDecimal(NetdueVat) < 0) || (IsSwichButtonEnable == true && FourteenA < 100000 && Convert.ToDecimal(CreditVat) > 0))
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
                        //headerAmountInfo.IsRed = "#ff0000";
                        //headerAmountInfo.IsBold = "Bold";
                        //headerAmountInfo.Message = Masseges.ToString();

                        //headerWithInfos.Add(headerAmountInfo);


                        //newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                        //newDesignPopUp.HeaderWithInfos = headerWithInfos;
                        //newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                        //PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                        result = true;
                       
                    }
                    else
                    {
                        if (resNew.d.SubmitFg == "" || resNew.d.SubmitFg == string.Empty)
                        {
                            Device.BeginInvokeOnMainThread(() =>
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
                            _navigationService.NavigateTo(App.VATReturnSuccessfullPageView, VATDeclarationData);
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
                        Device.BeginInvokeOnMainThread(async () =>
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

                            await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));


                            //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                            _navigationService.GoBack();
                        });
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () =>
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

                           await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));


                            //await _dialogService.ShowMessage(WebServiceManager.ErrorMessageForVAT, AppResources.Information);
                            //_navigationService.GoBack();
                            WebServiceManager.ErrorMessageForVAT = string.Empty;
                        });
                    }
                    //  await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                }
                return result;
            }
            catch (Exception)
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

                VATDeclarationData.d.TotalsalesAmt = vATDeclarationD.TotalsalesAmt;
                VATDeclarationData.d.TotalsalesAdj = vATDeclarationD.TotalsalesAdj;
                VATDeclarationData.d.TotalpurchaseAmt = vATDeclarationD.TotalpurchaseAmt;
                VATDeclarationData.d.TotalpurchaseAdj = vATDeclarationD.TotalpurchaseAdj;
                VATDeclarationData.d.StdsalesVat = vATDeclarationD.StdsalesVat;
                VATDeclarationData.d.TotalsalesVat = vATDeclarationD.TotalsalesVat;
                VATDeclarationData.d.StdpurchasesVat = vATDeclarationD.StdpurchasesVat;
                VATDeclarationData.d.ImportspaidVat = vATDeclarationD.ImportspaidVat;
                VATDeclarationData.d.ImportsaccVat = vATDeclarationD.ImportsaccVat;
                VATDeclarationData.d.TotalpurchaseVat = vATDeclarationD.TotalpurchaseVat;
                VATDeclarationData.d.TotaldueVat = vATDeclarationD.TotaldueVat;
                VATDeclarationData.d.Preperiodcorr = vATDeclarationD.Preperiodcorr;
                VATDeclarationData.d.CreditVat = vATDeclarationD.CreditVat;
                VATDeclarationData.d.NetdueVat = vATDeclarationD.NetdueVat;
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

                    foreach (var vat15model in vATDeclarationD.VATPERITEMSet.results)
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

                    foreach (var vat15model in vATDeclarationD.VATPERITEMSet.results)
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
            if (!String.IsNullOrEmpty(vATDeclarationD.StdsalesAmt))
            {
                vATDeclarationD.StdsalesAmt = vATDeclarationD.StdsalesAmt.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.StdsalesAdj))
            {
                vATDeclarationD.StdsalesAdj = vATDeclarationD.StdsalesAdj.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.SalesGccAmt))
            {
                vATDeclarationD.SalesGccAmt = vATDeclarationD.SalesGccAmt.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.SalesGccAdj))
            {
                vATDeclarationD.SalesGccAdj = vATDeclarationD.SalesGccAdj.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.ZerosalesAmt))
            {
                vATDeclarationD.ZerosalesAmt = vATDeclarationD.ZerosalesAmt.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.ZerosalesAdj))
            {
                vATDeclarationD.ZerosalesAdj = vATDeclarationD.ZerosalesAdj.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.ExportsAmt))
            {
                vATDeclarationD.ExportsAmt = vATDeclarationD.ExportsAmt.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.ExportsAdj))
            {
                vATDeclarationD.ExportsAdj = vATDeclarationD.ExportsAdj.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.ExemptsalesAmt))
            {
                vATDeclarationD.ExemptsalesAmt = vATDeclarationD.ExemptsalesAmt.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.ExemptsalesAdj))
            {
                vATDeclarationD.ExemptsalesAdj = vATDeclarationD.ExemptsalesAdj.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.StdpurchaseAmt))
            {
                vATDeclarationD.StdpurchaseAmt = vATDeclarationD.StdpurchaseAmt.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.StdpurchaseAdj))
            {
                vATDeclarationD.StdpurchaseAdj = vATDeclarationD.StdpurchaseAdj.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.ImportspaidAmt))
            {
                vATDeclarationD.ImportspaidAmt = vATDeclarationD.ImportspaidAmt.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.ImportspaidAdj))
            {
                vATDeclarationD.ImportspaidAdj = vATDeclarationD.ImportspaidAdj.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.ImportsaccAmt))
            {
                vATDeclarationD.ImportsaccAmt = vATDeclarationD.ImportsaccAmt.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.ImportsaccAdj))
            {
                vATDeclarationD.ImportsaccAdj = vATDeclarationD.ImportsaccAdj.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.ZeropurchaseAmt))
            {
                vATDeclarationD.ZeropurchaseAmt = vATDeclarationD.ZeropurchaseAmt.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.ZeropurchaseAdj))
            {
                vATDeclarationD.ZeropurchaseAdj = vATDeclarationD.ZeropurchaseAdj.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.ExemptpurchaseAmt))
            {
                vATDeclarationD.ExemptpurchaseAmt = vATDeclarationD.ExemptpurchaseAmt.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.ExemptpurchaseAdj))
            {
                vATDeclarationD.ExemptpurchaseAdj = vATDeclarationD.ExemptpurchaseAdj.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.Preperiodcorr))
            {
                vATDeclarationD.Preperiodcorr = vATDeclarationD.Preperiodcorr.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.StdsalesAmt))
            {
                vATDeclarationD.StdsalesAmt = vATDeclarationD.StdsalesAmt.Replace(",", "");
            }
            return vATDeclarationD;
        }

        public VATDeclarationD SetDataForPost(VATDeclarationD vATDeclarationD)
        {
            try
            {
                if (!String.IsNullOrEmpty(TotalsalesAmt))
                {
                    vATDeclarationD.TotalsalesAmt = !TotalsalesAmt.Contains(",") ? TotalsalesAmt : TotalsalesAmt.Replace(",", "");
                }
                if (!String.IsNullOrEmpty(TotalsalesAdj))
                {
                    vATDeclarationD.TotalsalesAdj = !TotalsalesAdj.Contains(",") ? TotalsalesAdj : TotalsalesAdj.Replace(",", "");
                }
                if (!String.IsNullOrEmpty(TotalpurchaseAmt))
                {
                    vATDeclarationD.TotalpurchaseAmt = !TotalpurchaseAmt.Contains(",") ? TotalpurchaseAmt : TotalpurchaseAmt.Replace(",", "");
                }
                if (!String.IsNullOrEmpty(TotalpurchaseAdj))
                {
                    vATDeclarationD.TotalpurchaseAdj = !TotalpurchaseAdj.Contains(",") ? TotalpurchaseAdj : TotalpurchaseAdj.Replace(",", "");
                }
                if (!String.IsNullOrEmpty(StdsalesVat))
                {
                    vATDeclarationD.StdsalesVat = !StdsalesVat.Contains(",") ? StdsalesVat : StdsalesVat.Replace(",", "");
                }
                if (!String.IsNullOrEmpty(TotalsalesVat))
                {
                    vATDeclarationD.TotalsalesVat = !TotalsalesVat.Contains(",") ? TotalsalesVat : TotalsalesVat.Replace(",", "");
                }
                if (!String.IsNullOrEmpty(StdpurchasesVat))
                {
                    vATDeclarationD.StdpurchasesVat = !StdpurchasesVat.Contains(",") ? StdpurchasesVat : StdpurchasesVat.Replace(",", "");
                }
                if (!String.IsNullOrEmpty(ImportspaidVat))
                {
                    vATDeclarationD.ImportspaidVat = !ImportspaidVat.Contains(",") ? ImportspaidVat : ImportspaidVat.Replace(",", "");
                }
                if (!String.IsNullOrEmpty(ImportsaccVat))
                {
                    vATDeclarationD.ImportsaccVat = !ImportsaccVat.Contains(",") ? ImportsaccVat : ImportsaccVat.Replace(",", "");
                }
                if (!String.IsNullOrEmpty(TotalpurchaseVat))
                {
                    vATDeclarationD.TotalpurchaseVat = !TotalpurchaseVat.Contains(",") ? TotalpurchaseVat : TotalpurchaseVat.Replace(",", "");
                }
                if (!String.IsNullOrEmpty(TotaldueVat))
                {
                    vATDeclarationD.TotaldueVat = !TotaldueVat.Contains(",") ? TotaldueVat : TotaldueVat.Replace(",", "");
                }
                if (!String.IsNullOrEmpty(Preperiodcorr))
                {
                    vATDeclarationD.Preperiodcorr = !Preperiodcorr.Contains(",") ? Preperiodcorr : Preperiodcorr.Replace(",", "");
                }
                if (!String.IsNullOrEmpty(CreditVat))
                {
                    vATDeclarationD.CreditVat = !CreditVat.Contains(",") ? CreditVat : CreditVat.Replace(",", "");
                }
                if (!String.IsNullOrEmpty(NetdueVat))
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
            if (VATDeclarationData.d.GoliveFg == "X")
            {
                VATNewModelFor15Percent = VATDeclarationData.d.VATPERITEMSet.results.Where(x => x.Type == "002").FirstOrDefault();
                VATNewModelFor5Percent = VATDeclarationData.d.VATPERITEMSet.results.Where(x => x.Type == "003").FirstOrDefault();


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
                if (VATDeclarationData.d != null)
                {
                    if (VATDeclarationData.d.Abrzu != null && VATDeclarationData.d.Abrzo != null)
                    {
                        if (VATDeclarationData.d.IBANSet.results != null && VATDeclarationData.d.IBANSet.results.Count() != 0)
                        {
                            IBANList = new List<Result2>();
                            IBANList = VATDeclarationData.d.IBANSet.results;

                        }
                        createIBANType();
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
            catch (Exception)
            {
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
                if (!String.IsNullOrEmpty(Amount) && !String.IsNullOrEmpty(Adjustment) && Amount != "." && Adjustment != ".")
                {
                    if (!Amount.Contains("-") && !Adjustment.Contains("-"))
                    {
                        Double dAmount = string.IsNullOrEmpty(Amount) ? 0 : Convert.ToDouble(Amount);
                        Double dAdjustment = string.IsNullOrEmpty(Adjustment) ? 0 : Convert.ToDouble(Adjustment);
                        Double dVATRate = Convert.ToDouble(VATRate002);
                        VATAmount = Convert.ToDouble((((dAmount - dAdjustment) * dVATRate) / 100)).ToString();
                        if (VATAmount == "0")
                        {
                            VATAmount = "0.00";
                        }
                    }
                }
                if (!String.IsNullOrEmpty(VATAmount) && VATAmount != "0.00")
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
                if (!String.IsNullOrEmpty(Amount) && !String.IsNullOrEmpty(Adjustment) && !String.IsNullOrEmpty(VatRate) && Amount != "." && Adjustment != ".")
                {
                    if (!Amount.Contains("-") && !Adjustment.Contains("-"))
                    {
                        Double dAmount = string.IsNullOrEmpty(Amount) ? 0 : Convert.ToDouble(Amount);
                        Double dAdjustment = string.IsNullOrEmpty(Adjustment) ? 0 : Convert.ToDouble(Adjustment);
                        Double dVATRate = Convert.ToDouble(VatRate);
                        VATAmount = Convert.ToDouble((((dAmount - dAdjustment) * dVATRate) / 100)).ToString();
                        if (VATAmount == "0")
                        {
                            VATAmount = "0.00";
                        }
                    }
                }
                if (!String.IsNullOrEmpty(VATAmount) && VATAmount != "0.00")
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


        public string TotalAmountForSixVar(string Amount1, string Amount2, string Amount3, string Amount4, string Amount5, string Amount6)
        {
            String TotalAmount = "0.00";
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
                        if (!Amount1.Contains("-") && !Amount2.Contains("-") && !Amount3.Contains("-") && !Amount4.Contains("-") && !Amount5.Contains("-") && !Amount6.Contains("-"))
                        {
                            TotalAmount = Convert.ToDouble(((String.IsNullOrEmpty(Amount1) ? 0.00 : Convert.ToDouble(Amount1)) + (String.IsNullOrEmpty(Amount2) ? 0.00 : Convert.ToDouble(Amount2)) + (String.IsNullOrEmpty(Amount3) ? 0.00 : Convert.ToDouble(Amount3)) + (String.IsNullOrEmpty(Amount4) ? 0.00 : Convert.ToDouble(Amount4)) + (String.IsNullOrEmpty(Amount5) ? 0.00 : Convert.ToDouble(Amount5)) + (String.IsNullOrEmpty(Amount6) ? 0.00 : Convert.ToDouble(Amount6)))).ToString();
                            if (TotalAmount == "0")
                            {
                                TotalAmount = "0.00";
                            }
                        }
                    }
                }
                if (!String.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
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
            String TotalAmount = "0.00";
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
                        TotalAmount = Convert.ToDouble(((String.IsNullOrEmpty(Amount1) ? 0.00 : Convert.ToDouble(Amount1)) + (String.IsNullOrEmpty(Amount2) ? 0.00 : Convert.ToDouble(Amount2)) + (String.IsNullOrEmpty(Amount3) ? 0.00 : Convert.ToDouble(Amount3)) + (String.IsNullOrEmpty(Amount4) ? 0.00 : Convert.ToDouble(Amount4)) + (String.IsNullOrEmpty(Amount5) ? 0.00 : Convert.ToDouble(Amount5)) + (String.IsNullOrEmpty(Amount6) ? 0.00 : Convert.ToDouble(Amount6)))).ToString();
                        if (TotalAmount == "0")
                        {
                            TotalAmount = "0.00";
                        }
                    }
                }
                if (!String.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
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
            String TotalAmount = "0.00";
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
                            TotalAmount = Convert.ToDouble(((String.IsNullOrEmpty(Amount1) ? 0.00 : Convert.ToDouble(Amount1)) + (String.IsNullOrEmpty(Amount2) ? 0.00 : Convert.ToDouble(Amount2)) + (String.IsNullOrEmpty(Amount3) ? 0.00 : Convert.ToDouble(Amount3)) + (String.IsNullOrEmpty(Amount4) ? 0.00 : Convert.ToDouble(Amount4)) + (String.IsNullOrEmpty(Amount5) ? 0.00 : Convert.ToDouble(Amount5)) + (String.IsNullOrEmpty(Amount6) ? 0.00 : Convert.ToDouble(Amount6)) + (String.IsNullOrEmpty(Amount7) ? 0.00 : Convert.ToDouble(Amount7)) + (String.IsNullOrEmpty(Amount8) ? 0.00 : Convert.ToDouble(Amount8)))).ToString();
                            if (TotalAmount == "0")
                            {
                                TotalAmount = "0.00";
                            }
                        }
                    }
                }
                if (!String.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
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


        public string TotalAmount(string Amount1, string Amount2, string Amount3, string Amount4, string Amount5)
        {
            String TotalAmount = "0.00";
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
                if (!string.IsNullOrEmpty(Amount1) && !string.IsNullOrEmpty(Amount2) && !string.IsNullOrEmpty(Amount3) && !string.IsNullOrEmpty(Amount4) && !string.IsNullOrEmpty(Amount5))
                {
                    if (Amount1 != "." && Amount2 != "." && Amount3 != "." && Amount4 != "." && Amount5 != ".")
                    {
                        if (!Amount1.Contains("-") && !Amount2.Contains("-") && !Amount3.Contains("-") && !Amount4.Contains("-") && !Amount5.Contains("-"))
                        {
                            TotalAmount = Convert.ToDouble(((String.IsNullOrEmpty(Amount1) ? 0.00 : Convert.ToDouble(Amount1)) + (String.IsNullOrEmpty(Amount2) ? 0.00 : Convert.ToDouble(Amount2)) + (String.IsNullOrEmpty(Amount3) ? 0.00 : Convert.ToDouble(Amount3)) + (String.IsNullOrEmpty(Amount4) ? 0.00 : Convert.ToDouble(Amount4)) + (String.IsNullOrEmpty(Amount5) ? 0.00 : Convert.ToDouble(Amount5)))).ToString();
                            if (TotalAmount == "0")
                            {
                                TotalAmount = "0.00";
                            }
                        }
                    }
                }
                if (!String.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
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

        public string TotalAdjustmentForSixVar(string Adjustment1, string Adjustment2, string Adjustment3, string Adjustment4, string Adjustment5, string Adjustment6)
        {
            String TotalAmount = "0.00";
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
                }
                if (!string.IsNullOrEmpty(Adjustment1) && !string.IsNullOrEmpty(Adjustment2) && !string.IsNullOrEmpty(Adjustment3) && !string.IsNullOrEmpty(Adjustment4) && !string.IsNullOrEmpty(Adjustment5) && !string.IsNullOrEmpty(Adjustment6))
                {
                    if (Adjustment1 != "." && Adjustment2 != "." && Adjustment3 != "." && Adjustment4 != "." && Adjustment5 != "." && Adjustment6 != ".")
                    {
                        if (!Adjustment1.Contains("-") && !Adjustment2.Contains("-") && !Adjustment3.Contains("-") && !Adjustment4.Contains("-") && !Adjustment5.Contains("-") && !Adjustment6.Contains("-"))
                        {
                            TotalAmount = Convert.ToDouble(((String.IsNullOrEmpty(Adjustment1) ? 0 : Convert.ToDouble(Adjustment1)) + (String.IsNullOrEmpty(Adjustment2) ? 0 : Convert.ToDouble(Adjustment2)) + (String.IsNullOrEmpty(Adjustment3) ? 0 : Convert.ToDouble(Adjustment3)) + (String.IsNullOrEmpty(Adjustment4) ? 0 : Convert.ToDouble(Adjustment4)) + (String.IsNullOrEmpty(Adjustment5) ? 0 : Convert.ToDouble(Adjustment5)) + (String.IsNullOrEmpty(Adjustment6) ? 0 : Convert.ToDouble(Adjustment6)))).ToString();
                            if (TotalAmount == "0")
                            {
                                TotalAmount = "0.00";
                            }
                        }
                    }
                }
                if (!String.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
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

        public string TotalAdjustment(string Adjustment1, string Adjustment2, string Adjustment3, string Adjustment4, string Adjustment5)
        {
            String TotalAmount = "0.00";
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
                if (!string.IsNullOrEmpty(Adjustment1) && !string.IsNullOrEmpty(Adjustment2) && !string.IsNullOrEmpty(Adjustment3) && !string.IsNullOrEmpty(Adjustment4) && !string.IsNullOrEmpty(Adjustment5))
                {
                    if (Adjustment1 != "." && Adjustment2 != "." && Adjustment3 != "." && Adjustment4 != "." && Adjustment5 != ".")
                    {
                        if (!Adjustment1.Contains("-") && !Adjustment2.Contains("-") && !Adjustment3.Contains("-") && !Adjustment4.Contains("-") && !Adjustment5.Contains("-"))
                        {
                            TotalAmount = Convert.ToDouble(((String.IsNullOrEmpty(Adjustment1) ? 0 : Convert.ToDouble(Adjustment1)) + (String.IsNullOrEmpty(Adjustment2) ? 0 : Convert.ToDouble(Adjustment2)) + (String.IsNullOrEmpty(Adjustment3) ? 0 : Convert.ToDouble(Adjustment3)) + (String.IsNullOrEmpty(Adjustment4) ? 0 : Convert.ToDouble(Adjustment4)) + (String.IsNullOrEmpty(Adjustment5) ? 0 : Convert.ToDouble(Adjustment5)))).ToString();
                            if (TotalAmount == "0")
                            {
                                TotalAmount = "0.00";
                            }
                        }
                    }
                }
                if (!String.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
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
            String TotalAmount = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(Amount1) && Amount1.Contains(","))
                {
                    Amount1 = Amount1.Replace(",", "");
                }

                if (!String.IsNullOrEmpty(Amount1))
                {
                    TotalAmount = Convert.ToDouble((Convert.ToDouble(Amount1))).ToString();
                    if (TotalAmount == "0")
                    {
                        TotalAmount = "0.00";
                    }
                }
                if (!String.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
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

        public string AddTwoAmount(string Amount1, string Amount2)
        {
            String TotalAmount = "0.00";
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
                if (String.IsNullOrEmpty(Amount1))
                {
                    Amount1 = "0.00";
                }
                if (String.IsNullOrEmpty(Amount2))
                {
                    Amount2 = "0.00";
                }
                if (!String.IsNullOrEmpty(Amount1) && !String.IsNullOrEmpty(Amount2))
                {
                    TotalAmount = Convert.ToDouble((Convert.ToDouble(Amount1) + Convert.ToDouble(Amount2))).ToString();
                    if (TotalAmount == "0")
                    {
                        TotalAmount = "0.00";
                    }
                }
                if (!String.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
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
            String TotalAmount = "0.00";
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
                if (String.IsNullOrEmpty(Amount1))
                {
                    Amount1 = "0.00";
                }
                if (String.IsNullOrEmpty(Amount2))
                {
                    Amount2 = "0.00";
                }
                if (String.IsNullOrEmpty(Amount3))
                {
                    Amount3 = "0.00";
                }
                if (!String.IsNullOrEmpty(Amount1) && !String.IsNullOrEmpty(Amount2) && !String.IsNullOrEmpty(Amount3))
                {
                    TotalAmount = Convert.ToDouble((Convert.ToDouble(Amount1) + Convert.ToDouble(Amount2) + Convert.ToDouble(Amount3))).ToString();
                    if (TotalAmount == "0")
                    {
                        TotalAmount = "0.00";
                    }
                }
                if (!String.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
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
                        Double dAmount = string.IsNullOrEmpty(Amount) ? 0 : Convert.ToDouble(Amount);
                        Double dAdjustment = string.IsNullOrEmpty(Adjustment) ? 0 : Convert.ToDouble(Adjustment);
                        Double dVATRate = Convert.ToDouble(VATRate002);
                        VATAmount = Convert.ToDouble((((dAmount - dAdjustment) * dVATRate) / 100)).ToString();
                        if (VATAmount == "0")
                        {
                            VATAmount = "0.00";
                        }
                    }
                }
                if (!String.IsNullOrEmpty(VATAmount) && VATAmount != "0.00")
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
                        Double dAmount = string.IsNullOrEmpty(Amount) ? 0 : Convert.ToDouble(Amount);
                        Double dAdjustment = string.IsNullOrEmpty(Adjustment) ? 0 : Convert.ToDouble(Adjustment);
                        Double dVATRate001 = Convert.ToDouble(VATRate001);
                        Double dVATRate002 = Convert.ToDouble(VATRate002);
                        VATAmount = Convert.ToDouble((((dAmount * dVATRate001) / 100) - ((dAdjustment * dVATRate002) / 100))).ToString();
                        if (VATAmount == "0")
                        {
                            VATAmount = "0.00";
                        }
                    }
                }
                if (!String.IsNullOrEmpty(VATAmount) && VATAmount != "0.00")
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
                        Double dAmount = string.IsNullOrEmpty(Amount) ? 0 : Convert.ToDouble(Amount);
                        Double dAdjustment = string.IsNullOrEmpty(Adjustment) ? 0 : Convert.ToDouble(Adjustment);
                        Double dVATRate002 = string.IsNullOrEmpty(NewVATRate) ? 0 : Convert.ToDouble(NewVATRate);
                        Double dVATRate001 = Convert.ToDouble(VATRate001);
                        dVATRate002 = Convert.ToDouble(NewVATRate);
                        VATAmount = Convert.ToDouble((((dAmount * dVATRate001) / 100) - ((dAdjustment * dVATRate002) / 100))).ToString();
                        if (VATAmount == "0")
                        {
                            VATAmount = "0.00";
                        }
                    }
                }
                if (!String.IsNullOrEmpty(VATAmount) && VATAmount != "0.00")
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
                        Double dAmount = string.IsNullOrEmpty(Amount) ? 0 : Convert.ToDouble(Amount);
                        Double dAdjustment = string.IsNullOrEmpty(Adjustment) ? 0 : Convert.ToDouble(Adjustment);
                        Double dVATRate = Convert.ToDouble(VATRate002);
                        VATAmount = Convert.ToDouble((((dAmount - dAdjustment) * dVATRate) / 100)).ToString();
                        if (VATAmount == "0")
                        {
                            VATAmount = "0.00";
                        }
                    }
                }
                if (!String.IsNullOrEmpty(VATAmount) && VATAmount != "0.00")
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
                    Double dCurrentPeriod = string.IsNullOrEmpty(CurrentPeriod) ? 0 : Convert.ToDouble(CurrentPeriod);
                    Double dPreviousPeriod = string.IsNullOrEmpty(PreviousPeriod) ? 0 : Convert.ToDouble(PreviousPeriod);
                    Double dForwardFromPreviousPeriod = string.IsNullOrEmpty(ForwardFromPreviousPeriod) ? 0 : Convert.ToDouble(ForwardFromPreviousPeriod);
                    NetVatDue = Convert.ToDouble((dCurrentPeriod + dPreviousPeriod + dForwardFromPreviousPeriod)).ToString();
                    if (NetVatDue == "0")
                    {
                        NetVatDue = "0.00";
                    }
                }
                catch
                {
                }
                if (!String.IsNullOrEmpty(NetVatDue) && NetVatDue != "0.00")
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
                List<ApplicableButton> VATApplicableButtons = await WebServiceManager.GAZTVATReturnGetApplicableButtons(VATDeclarationData.d.Fbnumz, VATDeclarationData.d.Langz, VATDeclarationData.d.Operationz, VATDeclarationData.d.Gpart, VATDeclarationData.d.Statusz, VATDeclarationData.d.TxnTpz, vATDeclarationData.d.Periodkeyz);
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
                        // ListOfActionButtonsApplicable.Add(button.buttonEnumId.ToString());
                    }
                    ListOfActionButtonsApplicable = DummyListOfActionButtonsApplicable;
                }
                if(Isamend)
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
                Device.BeginInvokeOnMainThread(async () =>
                {
                   await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }
        }
        public void SetButtonStrings(String ButtonName)
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


        public bool IsCommaSeparatedValidAmount(string amount,int Max, int numberOfDigitAfterDecimal, int numberOfDigitBeforDecimal)
        {
            bool iSValiedNumber = false;
            try
            {
               
                if (amount != null && amount.Length < Max && amount.Length > 0)
                {
                    amount = amount.Replace(",", "");
                    if (amount.Contains("."))
                    {
                        string[] Amount = new String[2];
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

    }
}
