using EGAZT.Views.NewDesign.VATDeclarationPages;
using GalaSoft.MvvmLight.Views;
using Rg.Plugins.Popup.Services;
using System.Windows.Input;
using Xamarin.Forms;
using EGAZT.Models;
using Syncfusion.GridCommon;
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

namespace EGAZT.ViewModel.NewDesignViewModel
{
    public class GAZTNewDesignVATReturnUpdatedUIPageViewModel : BaseViewModel
    {

        public ICommand OnBackStepClicked { get; set; }
        public ICommand OnMoreClicked { get; set; }
        public ICommand onCountinueClicked { get; set; }
        

        int CurrentView;

        #region Variable

        private VATReturnUpdatedUITabEnum _currentTab = VATReturnUpdatedUITabEnum.Instrunction;
        public VATReturnUpdatedUITabEnum currentTab
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
        public int MaxIndex { get; private set; } = 7;
        #endregion

       

        private bool _isUnFocusedTextBox = false;
        public bool IsUnFocusedTextBox
        {
            get
            {
                return _isUnFocusedTextBox;
            }
            set
            {
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
                _isMainButtonEnabled = value;
                RaisePropertyChanged("IsMainButtonEnabled");
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
        private bool _isControlEnabledForEntry = false;
        public bool IsControlEnabledForEntry
        {
            get
            {
                return _isControlEnabledForEntry;
            }
            set
            {
                _isControlEnabledForEntry = value;
                RaisePropertyChanged("IsControlEnabledForEntry");
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

        private List<Attachment> _aTTACHSetsList;
        public List<Attachment> ATTACHSetsList
        {
            get
            {
                return _aTTACHSetsList;
            }
            set
            {
                _aTTACHSetsList = value;
                RaisePropertyChanged("ATTACHSetsList");
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
        private List<IGRTSetResult> _calculationRateIGRTSet;
        public List<IGRTSetResult> CalculationRateIGRTSet
        {
            get
            {
                return _calculationRateIGRTSet;
            }
            set
            {
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

        private string _vATRate003 = string.Empty;
        public string VATRate003
        {
            get
            {
                return _vATRate003;
            }
            set
            {
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
                _carriedValueString = value;
                RaisePropertyChanged("CarriedValueString");
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
                _isCheckedTaxPayerDetailsInfo = value;
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
                _isDeclarationCheckedForSummary = value;
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
                _stdsalesVat = value;
                RaisePropertyChanged("StdsalesVat");
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

        public bool _isBtnVisible;
        public bool isBtnVisible
        {
            get
            {
                return _isBtnVisible;
            }
            set
            {
                _isBtnVisible = value;
                RaisePropertyChanged("isBtnVisible");
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
                _onMoreOptionsEnabled = value;
                RaisePropertyChanged("OnMoreOptionsEnabled");
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
                _isSwitchToggledFor15PercentChange = value;
                if (_isSwitchToggledFor15PercentChange != null)
                {
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
                _isFifteenPercentChange = value;
                RaisePropertyChanged("IsFifteenPercentChange");
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
                _noLabelColor = value;
                RaisePropertyChanged("NoLabelColor");
            }
        }


        #endregion

        #region Constructor
        public GAZTNewDesignVATReturnUpdatedUIPageViewModel(INavigationService navigationService, IDialogService dialogService):base(navigationService, dialogService)
        {
            //OnLoginPageLinkClicked = new Xamarin.Forms.Command(() =>
            //{
            //    _navigationService.NavigateTo(App.LogInPageView, App.SFLandingPageView);
            //});
            
            CurrentView = 0;

            //IsInstrunctionView = true;

            //secBox=thirdBox=fourthBox=fivebox=sixbox=sevenbox = Color.FromHex("#EBEBEB");

            //IsTaxpayerView = IsVATReturnsView= IsSaleView = IsPurchaseView = IsTotalVatView = IsSummeryView = false;
            //isBtnVisible=isCheckVisible = false;
            ContinueText = "Continue";

            OnMoreClicked = new Xamarin.Forms.Command(() =>
            {
                PopupNavigation.Instance.PushAsync(new MorePopUpPageView());
            });

            onCountinueClicked = new Xamarin.Forms.Command(() =>
            {

                switch (currentTab)
                {
                    case VATReturnUpdatedUITabEnum.Instrunction:
                        currentTab = VATReturnUpdatedUITabEnum.TaxpayerDetails;
                        break;

                    case VATReturnUpdatedUITabEnum.TaxpayerDetails:
                        currentTab = VATReturnUpdatedUITabEnum.VATReturns;
                        break;
                    case VATReturnUpdatedUITabEnum.VATReturns: currentTab = VATReturnUpdatedUITabEnum.Sales;
                        break;

                    case VATReturnUpdatedUITabEnum.Sales: currentTab = VATReturnUpdatedUITabEnum.Purchase;
                        break;

                    case VATReturnUpdatedUITabEnum.Purchase:
                        currentTab = VATReturnUpdatedUITabEnum.TotalVat;
                        break;

                    case VATReturnUpdatedUITabEnum.TotalVat: 
                        currentTab = VATReturnUpdatedUITabEnum.Summery;
                        break;

                    case VATReturnUpdatedUITabEnum.Summery: 
//                        currentTab = VATReturnUpdatedUITabEnum.Summery;
                        break;
                }

/*                    switch (CurrentView)
                    {
                        case 0:
                            IsInstrunctionView = false;
                            IsTaxpayerView = true;
                        innerFrame= Color.FromHex("#006450");
                        secBox = Color.FromHex("#006450");
                        break;
                        case 1:
                            IsTaxpayerView = false;
                            IsVATReturnsView = true;
                        secOuterFrame= Color.FromHex("#006450");
                        thirdBox = Color.FromHex("#006450");
                        break;
                        case 2:
                            IsVATReturnsView = false;
                            IsSaleView = true;
                        
                        OuterFrame = Color.FromHex("#006450");
                        fourthBox = Color.FromHex("#006450");
                        break;

                        case 3:
                        IsPurchaseView = true;
                        IsSaleView = false;
                        fivebox= Color.FromHex("#006450");
                        BoxFiveFrame = Color.FromHex("#006450");
                        //ContinueText = "Confirm and Carry Forward";
                        //                            CreditDetailsText = "Confirm and Request Refund";
                        break;

                    case 4:IsTotalVatView = true;
                        IsPurchaseView = false;
                      sixbox= Color.FromHex("#006450");
                        BoxSixFrame= Color.FromHex("#006450");
                        isBtnVisible =  true;
                        CreditDetailsText = "Carried Credit Details";
                        break;
                    case 5: IsSummeryView = isCheckVisible = true;
                        IsTotalVatView = false;
                        sevenbox= Color.FromHex("#006450");
                        BoxSevenFrame= Color.FromHex("#006450");
                        CreditDetailsText = "Confrim and Generate SADAD Bill";
                        break;

                    case 6: navigationService.NavigateTo(App.VATReturnSuccessfullPageView);
                        break;
                }

                    if (CurrentView < 6)
                        CurrentView++;*/
                    
            });

            OnBackStepClicked = new Xamarin.Forms.Command(() =>
            {
                switch (currentTab)
                {
                    case VATReturnUpdatedUITabEnum.TaxpayerDetails:
                        currentTab = VATReturnUpdatedUITabEnum.Instrunction;
                        break;
                    case VATReturnUpdatedUITabEnum.VATReturns:
                        currentTab = VATReturnUpdatedUITabEnum.TaxpayerDetails;
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

                /*                if (CurrentView > 0)
                                    CurrentView--;
                                switch (CurrentView)
                                {
                                    case 5:
                                        isCheckVisible = IsSummeryView = false;
                                        IsTotalVatView = true;
                                        sevenbox= Color.FromHex("#EBEBEB");
                                        BoxSevenFrame= Color.Transparent;
                                        CreditDetailsText = "Carried Credit Details";
                                        break;
                                    case 4:
                                        isBtnVisible = IsTotalVatView = false;
                                        IsPurchaseView = true;
                                        sixbox= Color.FromHex("#EBEBEB");
                                        BoxSixFrame= Color.Transparent;

                                        break;
                                    case 3:IsPurchaseView = false;
                                        IsSaleView = true;
                                        fivebox= Color.FromHex("#EBEBEB");
                                        BoxFiveFrame=Color.Transparent;
                                        break;
                                    case 2:
                                        IsSaleView = false;
                                        IsVATReturnsView = true;
                                             fourthBox= Color.FromHex("#EBEBEB");
                                        OuterFrame = Color.Transparent;
                                        break;
                                    case 1: IsVATReturnsView = false;
                                        IsTaxpayerView = true;
                                        thirdBox = Color.FromHex("#EBEBEB");
                                        secOuterFrame = Color.Transparent;
                                        break;
                                    case 0: IsTaxpayerView = false;
                                        IsInstrunctionView = true;
                                       secBox = Color.FromHex("#EBEBEB");
                                        innerFrame=Color.Transparent ;
                                        break;
                //                    case 0: IsSaleView = false;
                                        IsInstrunctionView = true;

                                        break;
                                    default: CurrentView = 0;
                                        break;
                                }*/
            });
        }
        #endregion

       

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
                VTTHSetResult vTTHSetResult;
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
                string periodfrom;
                string periodto;
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
                            }
                            else
                            {
                                CarriedValueString = AppResources.ZVatCorrectionsfrompreviousperiod.Replace("±", " ± " + CorrectionPeriodAmount);
                            }
                        }
                    }
                }
                if (VATDeclarationData.d.TcFg == "1")
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        IsDeclarationCheckedForInstruction = true;
                    });
                }
                if (VATDeclarationData.d.ConfStp2 == "1")
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        IsCheckedTaxPayerDetailsInfo = true;
                    });
                }
                if (VATDeclarationData.d.DecFg == "1")
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        IsDeclarationCheckedForSummary = true;
                    });
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
                    _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }
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
            catch (Exception e)
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
            }
            return NetVatDue;
        }

        #endregion


        public async Task SetButtons(VATDeclaration vATDeclarationData)
        {
            try
            {
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
                        // ListOfActionButtonsApplicable.Add(button.buttonEnumId.ToString());
                    }
                    ListOfActionButtonsApplicable = DummyListOfActionButtonsApplicable;
                }
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(ex.Message, AppResources.Information);
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
        }
        



    }
}
