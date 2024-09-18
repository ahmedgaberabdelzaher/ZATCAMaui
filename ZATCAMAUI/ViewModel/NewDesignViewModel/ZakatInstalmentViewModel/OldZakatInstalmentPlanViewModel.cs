using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;


using Newtonsoft.Json;
using Mopups.Services;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.ZakatInstalationModels;
using ZATCAMAUI.ViewModel.NewDesignViewModel.Instructions;
using ZATCAMAUI.Views.NewDesign.Common;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using ZATCAMAUI.Views.NewDesign.VATDeclarationPages;
using ZATCAMAUI.Views.NewDesign.ZakatInstalmentPlan;
using ZATCAMAUI.Core.Interfaces;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel
{

    public class OldZakatInstalmentPlanViewModel : BaseViewModel
    {
        #region Variable
        bool _bankStatementsAttachment = true;
        private bool _isNoDataLableVisible = false;

        int noOfInstalments = 1;
        double maxInstalments = 12;

        double minInstalments = 1;
        double downPaymentAmount = 400.00;
        double periodicInstalment = 0.0;
        double maxAmount = 2000000.0;

        double minAmount = 0;
        string inputData = "";
        string totalAmountSAR = "0.00 SAR";
        string downPaymentSAR = "0.00 SAR";
        string minInstalmentsTitle = AppResources.ZakatMin + " " + 1;
        string maxInstalmentsTitle = AppResources.ZakatMax + " " + 36;
        string minAmountTitle = AppResources.ZakatMin + " 0.0";
        string maxAmountTitle = AppResources.ZakatMax + " 0.0";
        string _vATDueAmount = "0.00";
        string _vATPenalityAmount = "0.00";
        string _vATBillDueAmount = "0.00 SAR";
        string _vATLiabilityAmount = "0.00 SAR";
        int selectedPage = (int)PagesEnum.ZakatSelectionView;

        private int? _subSetSelectedIndexZakat = 0;
        private string _subTxtSelectedStatusZakat = string.Empty;
        private string _txtSelectedStatusZakat = string.Empty;
        private bool _isSubIncomeTaxViewEnabled = false;
        private bool _isIncomeTaxViewEnabled = false;
        private bool _isZakatSelected = false;
        private bool _isVATAmountVisible = false;
        private bool _isSummaryViewEnabled = true;
        private bool _isStatementViewEnabled = false;
        private bool _isAttachmentsViewEnabled = false;
        private bool _InstalmentPlanAgreementsVisible = false;
        private bool _isVATBillsViewEnabled = false;
        private bool _isBillsViewEnabled = false;
        private bool _isBackButtonVisible = true;
        private bool _isSelectionViewEnabled = true;
        private int _selectedOutletOptionIndex;
        private bool _isDisplayInstalmentsVisible = false;
        private bool _isAgreementViewEnabled = false;
        private bool _isOutletViewEnabled = false;
        private string _selectedFrequencyType = "1";
        private string _selectedFrequencyName = AppResources.ZakatInstalmetMonthly;

        public bool MarkComplete { get; private set; } = false;
        public int MaxIndex { get; private set; } = 6;
        private List<CorrespondenceFiltersModel> _subCorresFilterZakat;
        private List<CorrespondanceModel> _subListZAKATCorrespondance = null;
        private List<CorrespondenceFiltersModel> _corresFilterZakat;
        private CorrespondenceFiltersModel _selectedFilterZakatPrev = null;
        private List<CorrespondanceModel> _listZAKATCorrespondance = null;
        private CorrespondenceFiltersModel _selectedFilterZakat = null;
        public List<OldResults3> selectedList = new List<OldResults3>();
        private List<OldResults3> _zakatInvoicesList = null;

        #endregion

        #region Enums
        enum PagesEnum
        {
            ZakatSelectionView,
            ZakatBillView,
            ZakatAggrementView,
            ZakatAttachmentsView,
            ZakatEnableStatementsView,
            ZakatSummaryView,
            ZakatSuccessView,
            ZakatInstalmentVisible,
            InstalmentPlanAgreementsVisible,
            IsDisplayInstalmentsVisible

        }
        #endregion

        #region Commands

        public ICommand ReasonContinueBtnTapped { get; set; }
        public ICommand BillContinueBtnTapped { get; set; }
        public ICommand AggrementContinueBtnTapped { get; set; }
        public ICommand OutletContinueBtnTapped { get; set; }
        public ICommand AttachmentsContinueBtnTapped { get; set; }
        public ICommand DeclarationContinueBtnTapped { get; set; }
        public ICommand StatementsContinueBtnTapped { get; set; }
        public ICommand SummaryContinueBtnTapped { get; set; }
        public ICommand SuccessContinueBtnTapped { get; set; }
        public ICommand CloseClick { get; set; }
        public ICommand GoBackToBills { get; set; }
        public ICommand GoBackToAggrement { get; set; }
        public ICommand GoBackToAttachments { get; set; }
        public ICommand SummaryInstallmentDetailsBtnTapped { get; set; }
        public ICommand OnZakatInstalmentReasonTapped { get; set; }
        public ICommand VATInstalationClicked { get; set; }
        public ICommand InstallmentDetailsBtnTapped { get; set; }
        public ICommand ShowFinancialStatementPicker { get; set; }


        public ICommand BankStatementsAttachmentTapped { get; set; }

        public ICommand FinanceAttachmentTapped { get; set; }
        public ICommand GoBackClick { get; set; }
        public ICommand onMoreOptionClicked { get; set; }

        #endregion


        #region Properties


        private bool isNewLoading;

        public bool IsNewLoading
        {
            get
            {
                return isNewLoading;
            }
            set
            {
                isNewLoading = value;
                OnPropertyChanged("IsNewLoading");
            }
        }

        private bool _isZakat;
        public bool IsZakat
        {
            get
            {
                return _isZakat;
            }
            set
            {
                _isZakat = value;
                OnPropertyChanged("IsZakat");
            }
        }

        private string _zakatTitle = AppResources.ZakatInstalmetSelectTypeZakat;
        public string ZakatTitle
        {
            get
            {
                return _zakatTitle;
            }
            set
            {
                _zakatTitle = value;
                OnPropertyChanged("ZakatTitle");
            }
        }
        public string VATDueAmount
        {
            get
            {
                return _vATDueAmount;
            }
            set
            {
                _vATDueAmount = value;
                OnPropertyChanged("VATDueAmount");
            }
        }

        public string SelectedFrequencyType
        {
            get
            {
                return _selectedFrequencyType;
            }
            set
            {
                _selectedFrequencyType = value;
                OnPropertyChanged("SelectedFrequencyType");
            }
        }
        public string SelectedFrequencyName
        {
            get
            {
                return _selectedFrequencyName;
            }
            set
            {
                _selectedFrequencyName = value;
                OnPropertyChanged("SelectedFrequencyName");
            }
        }

        public string VATPenalityAmount
        {
            get
            {
                return _vATPenalityAmount;
            }
            set
            {
                _vATPenalityAmount = value;
                OnPropertyChanged("VATPenalityAmount");
            }
        }

        public string VATLiabilityAmount
        {
            get
            {
                return _vATLiabilityAmount;
            }
            set
            {
                _vATLiabilityAmount = value;
                OnPropertyChanged("VATLiabilityAmount");
            }
        }

        public string VATBillDueAmount
        {
            get
            {
                return _vATBillDueAmount;
            }
            set
            {
                _vATBillDueAmount = value;
                OnPropertyChanged("VATBillDueAmount");
            }
        }

        public bool IsBackButtonVisible
        {
            get
            {
                return _isBackButtonVisible;
            }
            set
            {
                _isBackButtonVisible = value;
                OnPropertyChanged("IsBackButtonVisible");
            }
        }

        public bool IsSelectionViewEnabled
        {
            get
            {
                return _isSelectionViewEnabled;
            }
            set
            {
                _isSelectionViewEnabled = value;
                OnPropertyChanged("IsSelectionViewEnabled");
            }
        }

        public int SelectedOutletOptionIndex
        {
            get
            {
                return _selectedOutletOptionIndex;
            }
            set
            {
                _selectedOutletOptionIndex = value;
                OnPropertyChanged("SelectedOutletOptionIndex");
            }
        }

        public bool IsDisplayInstalmentsVisible
        {
            get
            {
                return _isDisplayInstalmentsVisible;
            }
            set
            {
                _isDisplayInstalmentsVisible = value;
                OnPropertyChanged("IsDisplayInstalmentsVisible");
            }
        }

        public List<OldResults3> ZakatInvoicesList
        {
            get
            {
                return _zakatInvoicesList;
            }
            set
            {
                _zakatInvoicesList = value;
                OnPropertyChanged("ZakatInvoicesList");
            }
        }



        public bool isNoDataLableVisible
        {
            get
            {
                return _isNoDataLableVisible;
            }
            set
            {
                _isNoDataLableVisible = value;
                OnPropertyChanged("isNoDataLableVisible");
            }
        }

        public bool IsAgreementViewEnabled
        {
            get
            {
                return _isAgreementViewEnabled;
            }
            set
            {
                _isAgreementViewEnabled = value;
                OnPropertyChanged("IsAgreementViewEnabled");
            }
        }

        public bool IsOutletViewEnabled
        {
            get
            {
                return _isOutletViewEnabled;
            }
            set
            {
                _isOutletViewEnabled = value;
                OnPropertyChanged("IsOutletViewEnabled");
            }
        }

        public bool IsBillsViewEnabled
        {
            get
            {
                return _isBillsViewEnabled;
            }
            set
            {
                _isBillsViewEnabled = value;
                OnPropertyChanged("IsBillsViewEnabled");
            }
        }

        public bool IsVATBillsViewEnabled
        {
            get
            {
                return _isVATBillsViewEnabled;
            }
            set
            {
                _isVATBillsViewEnabled = value;
                OnPropertyChanged("IsVATBillsViewEnabled");
            }
        }


        public bool InstalmentPlanAgreementsVisible
        {
            get
            {
                return _InstalmentPlanAgreementsVisible;
            }
            set
            {
                _InstalmentPlanAgreementsVisible = value;
                OnPropertyChanged("InstalmentPlanAgreementsVisible");
            }
        }


        public bool IsAttachmentsViewEnabled
        {
            get
            {
                return _isAttachmentsViewEnabled;
            }
            set
            {
                _isAttachmentsViewEnabled = value;
                OnPropertyChanged("IsAttachmentsViewEnabled");
            }
        }

        public bool IsStatementViewEnabled
        {
            get
            {
                return _isStatementViewEnabled;
            }
            set
            {
                _isStatementViewEnabled = value;
                OnPropertyChanged("IsStatementViewEnabled");
            }
        }


        public bool IsSummaryViewEnabled
        {
            get
            {
                return _isSummaryViewEnabled;
            }
            set
            {
                _isSummaryViewEnabled = value;
                OnPropertyChanged("IsSummaryViewEnabled");
            }
        }

        public int NoOfInstalments
        {
            set
            {
                if (noOfInstalments != value)
                {
                    noOfInstalments = value;
                    OnPropertyChanged("NoOfInstalments");
                }
            }
            get
            {
                return noOfInstalments;
            }
        }

        private double _numberOFInstalmentSliderValue = 0;

        public double NumberOFInstalmentSliderValue
        {
            set
            {
                if (_numberOFInstalmentSliderValue != value)
                {
                    _numberOFInstalmentSliderValue = value;
                    OnPropertyChanged("NumberOFInstalmentSliderValue");
                }
            }
            get
            {
                return _numberOFInstalmentSliderValue;
            }
        }

        public double MinInstalments
        {
            set
            {
                if (minInstalments != value && MaxInstalments > value)
                {
                    minInstalments = value;
                    OnPropertyChanged("MinInstalments");
                }
            }
            get
            {
                return minInstalments;
            }
        }

        public double MaxInstalments
        {
            set
            {
                if (maxInstalments != value && MinInstalments < value)
                {
                    maxInstalments = value;
                    OnPropertyChanged("MaxInstalments");
                }
            }
            get
            {
                return maxInstalments;
            }
        }

        public double DownPaymentAmount
        {
            set
            {
                if (value == downPaymentAmount) return;
                value = Math.Round(value, 2);
                var dueAmount = VATBillDueAmount.Replace("SAR", "");
                PeriodicInstalment = Math.Abs(double.Parse(dueAmount) - value);
                downPaymentAmount = value;
                OnPropertyChanged("DownPaymentAmount");
            }
            get
            {
                return downPaymentAmount;
            }
        }
        public string MinInstalmentsTitle
        {
            set
            {
                if (minInstalmentsTitle != value)
                {
                    minInstalmentsTitle = value;
                    OnPropertyChanged("MinInstalmentsTitle");
                }
            }
            get
            {
                return minInstalmentsTitle;
            }
        }
        public string MinAmountTitle
        {
            set
            {
                if (minAmountTitle != value)
                {
                    minAmountTitle = value;
                    OnPropertyChanged("MinAmountTitle");
                }
            }
            get
            {
                return minAmountTitle;
            }
        }

        public string MaxAmountTitle
        {
            set
            {
                if (maxAmountTitle != value)
                {
                    maxAmountTitle = value;
                    OnPropertyChanged("MaxAmountTitle");
                }
            }
            get
            {
                return maxAmountTitle;
            }
        }
        public string MaxInstalmentsTitle
        {
            set
            {
                if (maxInstalmentsTitle != value)
                {
                    maxInstalmentsTitle = value;
                    OnPropertyChanged("MaxInstalmentsTitle");
                }
            }
            get
            {
                return maxInstalmentsTitle;
            }
        }

        public double MinAmount
        {
            set
            {
                if (minAmount != value)
                {
                    minAmount = value;
                    OnPropertyChanged("MinAmount");
                }
            }
            get
            {
                return minAmount;
            }
        }

        public double MaxAmount
        {
            set
            {
                if (maxAmount != value)
                {
                    maxAmount = value;
                    OnPropertyChanged("MaxAmount");
                }
            }
            get
            {
                return maxAmount;
            }
        }


        public string TotalAmountSAR
        {
            set
            {
                if (totalAmountSAR != value)
                {
                    totalAmountSAR = value;
                    OnPropertyChanged("TotalAmountSAR");
                }
            }
            get
            {
                return totalAmountSAR;
            }
        }

        public string DownPaymentSAR
        {
            set
            {
                if (downPaymentSAR != value)
                {
                    downPaymentSAR = value;
                    OnPropertyChanged("DownPaymentSAR");
                }
            }
            get
            {
                return downPaymentSAR;
            }
        }


        public double PeriodicInstalment
        {
            set
            {
                if (periodicInstalment != value)
                {
                    periodicInstalment = value;
                    OnPropertyChanged("PeriodicInstalment");
                }
            }
            get
            {
                return periodicInstalment;
            }
        }

        public string InputData
        {
            set
            {
                if (inputData != value)
                {
                    inputData = value;
                    OnPropertyChanged("InputData");
                }
            }
            get
            {
                return inputData;
            }
        }


        public bool IsVATAmountVisible
        {
            get
            {
                return _isVATAmountVisible;
            }
            set
            {
                _isVATAmountVisible = value;
                OnPropertyChanged("IsVATAmountVisible");
            }
        }

        public bool IsZakatSelected
        {
            get
            {
                return _isZakatSelected;
            }
            set
            {
                _isZakatSelected = value;
                OnPropertyChanged("IsZakatSelected");
            }
        }

        public bool IsIncomeTaxViewEnabled
        {
            get
            {
                return _isIncomeTaxViewEnabled;
            }
            set
            {
                _isIncomeTaxViewEnabled = value;
                OnPropertyChanged("IsIncomeTaxViewEnabled");
            }
        }


        public bool IsSubIncomeTaxViewEnabled
        {
            get
            {
                return _isSubIncomeTaxViewEnabled;
            }
            set
            {
                _isSubIncomeTaxViewEnabled = value;
                OnPropertyChanged("IsSubIncomeTaxViewEnabled");
            }
        }

        string cashBankY1 = "0.00";
        public string CashBankY1
        {
            get
            {
                return cashBankY1;
            }
            set
            {
                cashBankY1 = value;
                OnPropertyChanged("CashBankY1");
            }
        }

        string cashBankY2 = "0.00";
        public string CashBankY2
        {
            get
            {
                return cashBankY2;
            }
            set
            {
                cashBankY2 = value;
                OnPropertyChanged("CashBankY2");
            }
        }

        string cashBankY3 = "0.00";
        public string CashBankY3
        {
            get
            {
                return cashBankY3;
            }
            set
            {
                cashBankY3 = value;
                OnPropertyChanged("CashBankY3");
            }
        }

        string cashRatioY1 = "0.00";
        public string CashRatioY1
        {
            get
            {
                return cashRatioY1;
            }
            set
            {
                cashRatioY1 = value;
                OnPropertyChanged("CashRatioY1");
            }
        }

        string cashRatioY2 = "0.00";
        public string CashRatioY2
        {
            get
            {
                return cashRatioY2;
            }
            set
            {
                cashRatioY2 = value;
                OnPropertyChanged("CashRatioY2");
            }
        }

        string cashRatioY3 = "0.00";
        public string CashRatioY3
        {
            get
            {
                return cashRatioY3;
            }
            set
            {
                cashRatioY3 = value;
                OnPropertyChanged("CashRatioY3");
            }
        }

        string debitorsY1 = "0.00";
        public string DebitorsY1
        {
            get
            {
                return debitorsY1;
            }
            set
            {
                debitorsY1 = value;
                OnPropertyChanged("DebitorsY1");
            }
        }

        string debitorsY2 = "0.00";
        public string DebitorsY2
        {
            get
            {
                return debitorsY2;
            }
            set
            {
                debitorsY2 = value;
                OnPropertyChanged("DebitorsY2");
            }
        }
        string debitorsY3 = "0.00";
        public string DebitorsY3
        {
            get
            {
                return debitorsY3;
            }
            set
            {
                debitorsY3 = value;
                OnPropertyChanged("DebitorsY3");
            }
        }

        string inventoryY1 = "0.00";
        public string InventoryY1
        {
            get
            {
                return inventoryY1;
            }
            set
            {
                inventoryY1 = value;
                OnPropertyChanged("InventoryY1");
            }
        }
        string inventoryY2 = "0.00";
        public string InventoryY2
        {
            get
            {
                return inventoryY2;
            }
            set
            {
                inventoryY2 = value;
                OnPropertyChanged("InventoryY2");
            }
        }

        string inventoryY3 = "0.00";
        public string InventoryY3
        {
            get
            {
                return inventoryY3;
            }
            set
            {
                inventoryY3 = value;
                OnPropertyChanged("InventoryY3");
            }
        }

        string ncFlowY1 = "0.00";
        public string NcFlowY1
        {
            get
            {
                return ncFlowY1;
            }
            set
            {
                ncFlowY1 = value;
                OnPropertyChanged("NcFlowY1");
            }
        }

        string ncFlowY2 = "0.00";
        public string NcFlowY2
        {
            get
            {
                return ncFlowY2;
            }
            set
            {
                ncFlowY2 = value;
                OnPropertyChanged("NcFlowY2");
            }
        }

        string ncFlowY3 = "0.00";
        public string NcFlowY3
        {
            get
            {
                return ncFlowY3;
            }
            set
            {
                ncFlowY3 = value;
                OnPropertyChanged("NcFlowY3");
            }
        }

        string netIncomeY1 = "0.00";
        public string NetIncomeY1
        {
            get
            {
                return netIncomeY1;
            }
            set
            {
                netIncomeY1 = value;
                OnPropertyChanged("NetIncomeY1");
            }
        }

        string netIncomeY2 = "0.00";
        public string NetIncomeY2
        {
            get
            {
                return netIncomeY2;
            }
            set
            {
                netIncomeY2 = value;
                OnPropertyChanged("NetIncomeY2");
            }
        }

        string netIncomeY3 = "0.00";
        public string NetIncomeY3
        {
            get
            {
                return netIncomeY3;
            }
            set
            {
                netIncomeY3 = value;
                OnPropertyChanged("NetIncomeY3");
            }
        }


        string profitRatioY1 = "0.00";
        public string ProfitRatioY1
        {
            get
            {
                return profitRatioY1;
            }
            set
            {
                profitRatioY1 = value;
                OnPropertyChanged("ProfitRatioY1");
            }
        }

        string profitRatioY2 = "0.00";
        public string ProfitRatioY2
        {
            get
            {
                return profitRatioY2;
            }
            set
            {
                profitRatioY2 = value;
                OnPropertyChanged("ProfitRatioY2");
            }
        }

        string profitRatioY3 = "0.00";
        public string ProfitRatioY3
        {
            get
            {
                return profitRatioY3;
            }
            set
            {
                profitRatioY3 = value;
                OnPropertyChanged("ProfitRatioY3");
            }
        }

        string revenueY1 = "0.00";
        public string RevenueY1
        {
            get
            {
                return revenueY1;
            }
            set
            {
                revenueY1 = value;
                OnPropertyChanged("RevenueY1");
            }

        }

        string revenueY2 = "0.00";
        public string RevenueY2
        {
            get
            {
                return revenueY2;
            }
            set
            {
                revenueY2 = value;
                OnPropertyChanged("RevenueY2");
            }

        }
        string revenueY3 = "0.00";
        public string RevenueY3
        {
            get
            {
                return revenueY3;
            }
            set
            {
                revenueY3 = value;
                OnPropertyChanged("RevenueY3");
            }

        }

        string stiY1 = "0.00";
        public string StiY1
        {
            get
            {
                return stiY1;
            }
            set
            {
                stiY1 = value;
                OnPropertyChanged("StiY1");
            }
        }
        string stiY2 = "0.00";
        public string StiY2
        {
            get
            {
                return stiY2;
            }
            set
            {
                stiY2 = value;
                OnPropertyChanged("StiY2");
            }
        }
        string stiY3 = "0.00";
        public string StiY3
        {
            get
            {
                return stiY3;
            }
            set
            {
                stiY3 = value;
                OnPropertyChanged("StiY3");
            }
        }


        string tcAssetsY1 = "0.00";
        public string TcAssetsY1
        {
            get
            {
                return tcAssetsY1;
            }
            set
            {
                tcAssetsY1 = value;
                OnPropertyChanged("TcAssetsY1");
            }
        }

        string tcAssetsY2 = "0.00";
        public string TcAssetsY2
        {
            get
            {
                return tcAssetsY2;
            }
            set
            {
                tcAssetsY2 = value;
                OnPropertyChanged("TcAssetsY2");
            }
        }

        string tcAssetsY3 = "0.00";
        public string TcAssetsY3
        {
            get
            {
                return tcAssetsY3;
            }
            set
            {
                tcAssetsY3 = value;
                OnPropertyChanged("TcAssetsY3");
            }
        }


        string tcLiabltyY1 = "0.00";
        public string TcLiabltyY1
        {
            get
            {
                return tcLiabltyY1;
            }
            set
            {
                tcLiabltyY1 = value;
                OnPropertyChanged("TcLiabltyY1");
            }
        }

        string tcLiabltyY2 = "0.00";
        public string TcLiabltyY2
        {
            get
            {
                return tcLiabltyY2;
            }
            set
            {
                tcLiabltyY2 = value;
                OnPropertyChanged("TcLiabltyY2");
            }
        }
        string tcLiabltyY3 = "0.00";
        public string TcLiabltyY3
        {
            get
            {
                return tcLiabltyY3;
            }
            set
            {
                tcLiabltyY3 = value;
                OnPropertyChanged("TcLiabltyY3");
            }
        }

        string year1 = "";
        public string Year1
        {
            get
            {
                return year1;
            }
            set
            {
                year1 = value;
                OnPropertyChanged("Year1");
            }
        }

        string year2 = "";
        public string Year2
        {
            get
            {
                return year2;
            }
            set
            {
                year2 = value;
                OnPropertyChanged("Year2");
            }
        }
        string year3 = "";
        public string Year3
        {
            get
            {
                return year3;
            }
            set
            {
                year3 = value;
                OnPropertyChanged("Year3");
            }
        }

        string zakatY1 = "0.00";
        public string ZakatY1
        {
            get
            {
                return zakatY1;
            }
            set
            {
                zakatY1 = value;
                OnPropertyChanged("ZakatY1");
            }
        }

        string zakatY2 = "0.00";
        public string ZakatY2
        {
            get
            {
                return zakatY2;
            }
            set
            {
                zakatY2 = value;
                OnPropertyChanged("ZakatY2");
            }

        }

        string zakatY3 = "0.00";
        public string ZakatY3
        {
            get
            {
                return zakatY3;
            }
            set
            {
                zakatY3 = value;
                OnPropertyChanged("ZakatY3");
            }
        }

        private bool instalmentSliderVisible = true;
        public bool InstalmentSliderVisible
        {
            set
            {
                if (instalmentSliderVisible != value)
                {
                    instalmentSliderVisible = value;
                    OnPropertyChanged("InstalmentSliderVisible");
                }
            }
            get
            {
                return instalmentSliderVisible;
            }
        }

        string pickedFinancialStatement = "";
        public string PickedFinancialStatement
        {
            get
            {
                return pickedFinancialStatement;
            }
            set
            {
                pickedFinancialStatement = value;
                OnPropertyChanged("PickedFinancialStatement");
            }
        }

        private GenericPickerModel _yesNoPickerModel { get; set; }
        public GenericPickerModel YesNoPickerModel
        {
            get { return _yesNoPickerModel; }
            set
            {
                _yesNoPickerModel = value;
                OnPropertyChanged("YesNoPickerModel");
            }
        }


        private bool _isFinsancialStatementsEditable { get; set; }
        public bool IsFinsancialStatementsEditable
        {
            get { return _isFinsancialStatementsEditable; }
            set
            {
                _isFinsancialStatementsEditable = value;
                OnPropertyChanged("IsFinsancialStatementsEditable");
            }
        }

        private bool _isPenaltyVisible = false;
        public bool IsPenaltyVisible
        {
            get { return _isPenaltyVisible; }
            set
            {
                _isPenaltyVisible = value;
                OnPropertyChanged("IsPenaltyVisible");
            }
        }

        public CorrespondenceFiltersModel SelectedFilterZakat
        {
            get
            {
                return _selectedFilterZakat;
            }
            set
            {
                _selectedFilterZakat = value;
                if (_selectedFilterZakat != null)
                {
                    if (_selectedFilterZakat.ID == 1)
                    {
                        if (ListZAKATCorrespondance != null)
                        {
                            List<CorrespondanceModel> CorreTosort = new List<CorrespondanceModel>();
                            CorreTosort = ListZAKATCorrespondance;
                            ListZAKATCorrespondance = null;
                            var SortedList = CorreTosort.OrderBy(x => x.StartDate).ThenBy(x => x.Ctime);
                            ListZAKATCorrespondance = SortedList.ToList();
                        }
                    }
                    if (_selectedFilterZakat.ID == 2)
                    {
                        if (ListZAKATCorrespondance != null)
                        {
                            List<CorrespondanceModel> CorreTosort = new List<CorrespondanceModel>();
                            CorreTosort = ListZAKATCorrespondance;
                            ListZAKATCorrespondance = null;
                            if (CorreTosort != null)
                            {

                                var SortedList = CorreTosort.OrderByDescending(x => x.StartDate).ThenByDescending(x => x.Ctime);
                                ListZAKATCorrespondance = SortedList.ToList();
                            }
                        }
                    }

                    TxtSelectedStatusZakat = _selectedFilterZakat.Filter;
                }
                OnPropertyChanged("SelectedFilterZakat");
            }
        }

        private string _zakatReferanceNumber = string.Empty;
        public string ZakatReferanceNumber
        {
            get
            {
                return _zakatReferanceNumber;
            }
            set
            {
                _zakatReferanceNumber = value;
                OnPropertyChanged("ZakatReferanceNumber");
            }
        }

        public List<CorrespondanceModel> ListZAKATCorrespondance
        {
            get
            {
                return _listZAKATCorrespondance;
            }
            set
            {
                _listZAKATCorrespondance = value;
                OnPropertyChanged("ListZAKATCorrespondance");
            }
        }


        public string TxtSelectedStatusZakat
        {
            get
            {
                return _txtSelectedStatusZakat;
            }
            set
            {
                _txtSelectedStatusZakat = value;
                OnPropertyChanged("TxtSelectedStatusZakat");
            }
        }

        private int _currenrIndex = 1;
        public int CurrentIndex
        {
            get => _currenrIndex;
            set
            {
                _currenrIndex = value;
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


        public CorrespondenceFiltersModel SelectedFilterZakatPrev
        {
            get
            {
                return _selectedFilterZakatPrev;
            }
            set
            {
                _selectedFilterZakatPrev = value;
                OnPropertyChanged("SelectedFilterZakatPrev");
            }
        }

        private void SetStatusPickerItem()
        {
            try
            {
                List<CorrespondenceFiltersModel> FiltersZAKAT = new List<CorrespondenceFiltersModel>();
                FiltersZAKAT.Add(new CorrespondenceFiltersModel { ID = 1, Filter = "Zakat" });
                FiltersZAKAT.Add(new CorrespondenceFiltersModel { ID = 2, Filter = "Income Tax" });
                CorresFilterZakat = FiltersZAKAT;
            }
            catch (Exception)
            {

            }
        }

        public List<CorrespondenceFiltersModel> CorresFilterZakat
        {
            get
            {
                return _corresFilterZakat;
            }
            set
            {
                _corresFilterZakat = value;
                OnPropertyChanged("CorresFilterZakat");
            }
        }


        public List<CorrespondanceModel> SubListZAKATCorrespondance
        {
            get
            {
                return _subListZAKATCorrespondance;
            }
            set
            {
                _subListZAKATCorrespondance = value;
                OnPropertyChanged("SubListZAKATCorrespondance");
            }
        }


        public string SubTxtSelectedStatusZakat
        {
            get
            {
                return _subTxtSelectedStatusZakat;
            }
            set
            {
                _subTxtSelectedStatusZakat = value;
                OnPropertyChanged("SubTxtSelectedStatusZakat");
            }
        }

        private Results4[] _statementList;
        public Results4[] StatementList
        {
            get
            {
                return _statementList;
            }
            set
            {
                _statementList = value;
                OnPropertyChanged("StatementList");
            }
        }

        public List<CorrespondenceFiltersModel> SubCorresFilterZakat
        {
            get
            {
                return _subCorresFilterZakat;
            }
            set
            {
                _subCorresFilterZakat = value;
                OnPropertyChanged("SubCorresFilterZakat");
            }
        }


        public int? SubSetSelectedIndexZakat
        {
            get
            {
                return _subSetSelectedIndexZakat;
            }
            set
            {
                _subSetSelectedIndexZakat = value;
                OnPropertyChanged("SubSetSelectedIndexZakat");
            }
        }

        private void vatInstallmentBillsList()
        {
            var selectedBillsList = new ObservableCollection<ZakatSelectBillModel>();


            SelectedBillsList = selectedBillsList;
        }


        public ObservableCollection<InstalmentAgreementInstalmentPlansModel> _instalmentPlans { get; set; }
        public ObservableCollection<InstalmentAgreementInstalmentPlansModel> InstalmentPlans
        {
            get
            {
                return _instalmentPlans;
            }
            set
            {
                if (_instalmentPlans == value)
                {
                    return;
                }
                _instalmentPlans = value;
                OnPropertyChanged("InstalmentPlans");
            }
        }

        public ObservableCollection<InstalmentAgreementAttachmentsModel> SummaryAttachmentsListViewData { get; private set; }

        public void PopulateSummaryAttachments()
        {
            var attachmentsListViewData1 = new ObservableCollection<Attachment>();


            if (BankStatementsAttachmentsListViewData != null)
            {

                foreach (Attachment attachment in BankStatementsAttachmentsListViewData)
                {
                    attachmentsListViewData1.Add(attachment);
                }
            }

            if (FinanceAttachmentsListViewData != null)
            {

                foreach (Attachment attachment in FinanceAttachmentsListViewData)
                {
                    attachmentsListViewData1.Add(attachment);
                }
            }




            AttachmentsListViewData = attachmentsListViewData1;



        }
        public ObservableCollection<TinDeregestrationAttachmentsModel> InstallmentAgreementListViewData { get; private set; }

        public void PopulateSummaryInstallmentAgreement()
        {
            var installmentAgreementListViewData = new ObservableCollection<TinDeregestrationAttachmentsModel>();
            installmentAgreementListViewData.Add(new TinDeregestrationAttachmentsModel
            {
                FieldTitle = "Frequency",
                FieldSubTitle = AppResources.TinDeregistration20MB,
                AttachmentName = "Monthly",
                IsAttachmentAttached = true
            });
            installmentAgreementListViewData.Add(new TinDeregestrationAttachmentsModel
            {
                FieldTitle = "Number of Installments",
                FieldSubTitle = AppResources.TinDeregistration20MB,
                AttachmentName = "5",
                IsAttachmentAttached = true
            });
            installmentAgreementListViewData.Add(new TinDeregestrationAttachmentsModel
            {
                FieldTitle = "Down Payment Amount",
                FieldSubTitle = AppResources.TinDeregistration20MB,
                AttachmentName = "1,000,000.00 SAR",
                IsAttachmentAttached = true
            });
            InstallmentAgreementListViewData = installmentAgreementListViewData;
        }


        public ZakatInstalmentPlanModel zakatInstalmentPlanModel { get; set; }
        public ZakatInstalmentPlanModel ZakatInstalmentPlanModel
        {
            get
            {
                return zakatInstalmentPlanModel;
            }

            set
            {
                if (zakatInstalmentPlanModel == value)
                {
                    return;
                }

                zakatInstalmentPlanModel = value;
                OnPropertyChanged("ZakatInstalmentPlanModel");
            }
        }

        public ObservableCollection<ZakatInstalmentPlanModel> c { get; set; }
        public ObservableCollection<ZakatInstalmentPlanModel> outletDecisionOptions { get; set; }
        public ObservableCollection<ZakatInstalmentPlanModel> OutletDecisionOptions
        {
            get
            {
                return outletDecisionOptions;
            }

            set
            {
                if (outletDecisionOptions == value)
                {
                    return;
                }

                outletDecisionOptions = value;
                OnPropertyChanged("OutletDecisionOptions");
            }
        }
        public ZakatSelectBillModel zakatSelectBillsPageViewModel { get; set; }
        public ZakatSelectBillModel ZakatSelectBillModel
        {
            get
            {
                return zakatSelectBillsPageViewModel;
            }

            set
            {
                if (zakatSelectBillsPageViewModel == value)
                {
                    return;
                }

                zakatSelectBillsPageViewModel = value;
                OnPropertyChanged("ZakatSelectBillModel");
            }
        }
        public ObservableCollection<ZakatSelectBillModel> selectedBillsList { get; set; }
        public ObservableCollection<ZakatSelectBillModel> SelectedBillsList
        {
            get
            {
                return selectedBillsList;
            }

            set
            {
                if (selectedBillsList == value)
                {
                    return;
                }

                selectedBillsList = value;
                OnPropertyChanged("SelectedBillsList");
            }
        }

        public ObservableCollection<InstalmentAgreementFrequencyModel> zakatAgreementOptions { get; set; }
        public ObservableCollection<InstalmentAgreementFrequencyModel> ZakatAgreementOptions
        {
            get
            {
                return zakatAgreementOptions;
            }

            set
            {
                if (zakatAgreementOptions == value)
                {
                    return;
                }

                zakatAgreementOptions = value;
                OnPropertyChanged("ZakatAgreementOptions");
            }
        }

        public ObservableCollection<Attachment> attachmentsListViewData { get; set; }
        public ObservableCollection<Attachment> AttachmentsListViewData
        {
            get
            {
                return attachmentsListViewData;
            }



            set
            {
                if (attachmentsListViewData == value)
                {
                    return;
                }



                attachmentsListViewData = value;
                OnPropertyChanged("AttachmentsListViewData");
            }
        }

        public ObservableCollection<ZakatSelectBillModel> summarySelectedBillsList { get; set; }
        public ObservableCollection<ZakatSelectBillModel> SummarySelectedBillsList
        {
            get
            {
                return summarySelectedBillsList;
            }

            set
            {
                if (summarySelectedBillsList == value)
                {
                    return;
                }

                summarySelectedBillsList = value;
                OnPropertyChanged("SummarySelectedBillsList");
            }
        }



        private ZakatInstalmentPlanModel _selectedOutletOption;
        public ZakatInstalmentPlanModel SelectedOutletOption
        {
            get
            {
                return _selectedOutletOption;
            }
            set
            {
                _selectedOutletOption = value;
                //SelectedOutletOptionIndex = OutletDecisionOptions.IndexOf(_selectedOutletOption as TINDeregistrationModel);
                OnPropertyChanged("SelectedOutletOption");
            }
        }


        private OldZakatRequestDisplayModel _zakatInstalments;
        public OldZakatRequestDisplayModel ZakatInstalments
        {
            get
            {
                return _zakatInstalments;
            }
            set
            {
                _zakatInstalments = value;
                OnPropertyChanged("ZakatInstalments");
            }
        }



        private bool _isInstrunctionChecked;
        public bool IsInstrunctionChecked
        {
            get
            {
                return _isInstrunctionChecked;
            }
            set
            {
                _isInstrunctionChecked = value;
                if (_isInstrunctionChecked)
                {
                    IsContinueButtonEnable = true;
                }
                else
                {
                    IsContinueButtonEnable = false;
                }
                OnPropertyChanged("IsInstrunctionChecked");
            }
        }

        private bool _isVatTermsChecked = false;
        public bool IsVatTermsChecked
        {
            get
            {
                return _isVatTermsChecked;
            }
            set
            {
                _isVatTermsChecked = value;
                OnPropertyChanged("IsVatTermsChecked");
            }
        }

        private bool _isInitialDraft = false;
        public bool IsInitialDraft
        {
            get
            {
                return _isInitialDraft;
            }
            set
            {
                _isInitialDraft = value;
                OnPropertyChanged("IsInitialDraft");
            }
        }

        private bool _isDeclarationChecked;
        public bool IsDeclarationChecked
        {
            get
            {
                return _isDeclarationChecked;
            }
            set
            {
                _isDeclarationChecked = value;
                if (_isDeclarationChecked)
                {
                    IsContinueButtonEnable = true;
                }

                OnPropertyChanged("IsDeclarationChecked");
            }
        }

        private bool _isContinueButtonEnable = false;
        public bool IsContinueButtonEnable
        {
            get
            {
                return _isContinueButtonEnable;
            }
            set
            {
                _isContinueButtonEnable = value;
                if (_isContinueButtonEnable)
                {
                    ContinueButtonnBackroundColor = (Color)Application.Current.Resources["Secondary"];
                }
                else
                {
                    ContinueButtonnBackroundColor = (Color)Application.Current.Resources["ButtonGray"];
                }
                OnPropertyChanged("IsContinueButtonEnable");
            }
        }
        private Color _continueButtonnBackroundColor = (Color)Application.Current.Resources["Secondary"];
        public Color ContinueButtonnBackroundColor
        {
            get
            {
                return _continueButtonnBackroundColor;
            }
            set
            {
                _continueButtonnBackroundColor = value;
                OnPropertyChanged("ContinueButtonnBackroundColor");
            }
        }

        private void Backnavigations()
        {
            switch (selectedPage)
            {
                case (int)PagesEnum.ZakatSelectionView:
                    _navigationService.GoBack();
                    break;
                case (int)PagesEnum.ZakatBillView:
                    EnableSlectionView();
                    break;
                case (int)PagesEnum.ZakatAggrementView:
                    for (int i = 0; i < selectedList.Count; i++)
                    {
                        if (ZakatInvoicesList[i].AIvAbtyp.Equals("ITAX"))
                        {
                            ZakatInvoicesList[i].AIvAbtyp = AppResources.ZakatInstalmetSelectTypeIncomeTax;
                        }
                        else if (ZakatInvoicesList[i].AIvAbtyp.Equals("ZAKT"))
                        {
                            ZakatInvoicesList[i].AIvAbtyp = AppResources.FORM5Zakat;
                        }
                    }
                    EnableVATBillView();
                    break;
                case (int)PagesEnum.IsDisplayInstalmentsVisible:
                    EnableAgreementView();
                    break;
                case (int)PagesEnum.ZakatAttachmentsView:

                    EnableInstalmentsScheduleView();
                    break;
                case (int)PagesEnum.InstalmentPlanAgreementsVisible:
                    EnableAgreementView();
                    break;
                case (int)PagesEnum.ZakatEnableStatementsView:
                    EnableInstalmentsScheduleView();
                    break;
                case (int)PagesEnum.ZakatSummaryView:
                    EnableAttachmentsView();
                    break;

                case (int)PagesEnum.ZakatSuccessView:
                    EnableSummaryView();
                    break;
                default:
                    // code block
                    break;
            }
        }
        #endregion


        public void ResetData()
        {

            NumberOFInstalmentSliderValue = 1;
            _idType = "";
            SelectedFrequencyType = "1";
            IsZakatSelected = true;
            //DownPaymentSliderValue = 1;
            IsInitialDraft = false;
            selectedList.Clear();

            NoOfInstalments = 1;
            MinInstalments = 1;
            MaxInstalments = 36;
            MinInstalmentsTitle = AppResources.ZakatMin + " " + 1;
            MaxInstalmentsTitle = AppResources.ZakatMax + " " + 36;
            DownPaymentAmount = 0.0;
            PeriodicInstalment = 0.0;
            MinAmount = 0;
            MaxAmount = 9999999999999.99;
            MinAmountTitle = AppResources.ZakatMin + " 0.0";
            MaxAmountTitle = AppResources.ZakatMax + " 0.0";
            // maxAmount = 0.0;
            InputData = "";
            TotalAmountSAR = "0.00";
            VATDueAmount = "0.00";
            VATPenalityAmount = "0.00";
            VATBillDueAmount = "0.00";
            VATLiabilityAmount = "0.00";
            CurrentIndex = 1;
            Year1 = "";
            Year2 = "";
            Year3 = "";
            BankStatementsAttachmentsListViewData = null;
            FinanceAttachmentsListViewData = null;
            isSubmitClicked = false;
            isDraftClicked = false;

            PickedFinancialStatement = AppResources.ZYes;
            IsFinsancialStatementsEditable = true;
            InstalmentSliderVisible = true;

            var list = new List<string>();
            list.Add(AppResources.ZYes);
            list.Add(AppResources.ZNo);
            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = list;
            genericPickerModel.PickerTitle = AppResources.ZakatInstalmentTaxpayerHoldingFinancialStatement;
            genericPickerModel.PickerId = "HoldingFinancial";
            YesNoPickerModel = genericPickerModel;

            CashBankY1 = "0.00";
            CashBankY2 = "0.00";
            CashBankY3 = "0.00";
            CashRatioY1 = "0.00";
            CashRatioY2 = "0.00";
            CashRatioY3 = "0.00";
            DebitorsY1 = "0.00";
            DebitorsY2 = "0.00";
            DebitorsY3 = "0.00";
            InventoryY1 = "0.00";
            InventoryY2 = "0.00";
            InventoryY3 = "0.00";
            NcFlowY1 = "0.00";
            NcFlowY2 = "0.00";
            NcFlowY3 = "0.00";
            NetIncomeY1 = "0.00";
            NetIncomeY2 = "0.00";
            NetIncomeY3 = "0.00";
            ProfitRatioY1 = "0.00";
            ProfitRatioY2 = "0.00";
            ProfitRatioY3 = "0.00";
            RevenueY1 = "0.00";
            RevenueY2 = "0.00";
            RevenueY3 = "0.00";
            StiY1 = "0.00";
            StiY2 = "0.00";
            StiY3 = "0.00";
            TcAssetsY1 = "0.00";
            TcAssetsY2 = "0.00";
            TcAssetsY3 = "0.00";
            TcLiabltyY1 = "0.00";
            TcLiabltyY2 = "0.00";
            TcLiabltyY3 = "0.00";
            Year1 = "";
            Year2 = "";
            Year3 = "";
            ZakatY1 = "0.00";
            ZakatY2 = "0.00";
            ZakatY3 = "0.00";




            IDTypeDictionary = new Dictionary<string, string>
        {
            {"","oBlak"},
            {AppResources.ZakatFinancialCrisis,"1"},
            {AppResources.ZakatDisputeInFavorOfGAZT,"2"},
            {AppResources.ZakatOtherReason,"3"},
        };

            EnableDeclarationContinue();

            AddFrequencyOptions();
            AddOutletDecisionOptions();

        }


        private async Task showPickerDialog()
        {
            try
            {
                await MopupService.Instance.PushAsync(new PickerPageView(YesNoPickerModel));
            }
            catch (InternetException ex)
            {
                await ShowDialog(ex.Message);
                _navigationService.GoBack();
            }
        }
        public void updatePicker()
        {
            PickedFinancialStatement = YesNoPickerModel.SelectedValue;
            if (PickedFinancialStatement == AppResources.ZYes)
            {
                IsFinsancialStatementsEditable = true;
            }
            else if (PickedFinancialStatement == AppResources.ZNo)
            {
                IsFinsancialStatementsEditable = false;

                CashBankY1 = "0.00";
                CashBankY2 = "0.00";
                CashBankY3 = "0.00";
                CashRatioY1 = "0.00";
                CashRatioY2 = "0.00";
                CashRatioY3 = "0.00";
                DebitorsY1 = "0.00";
                DebitorsY2 = "0.00";
                DebitorsY3 = "0.00";
                InventoryY1 = "0.00";
                InventoryY2 = "0.00";
                InventoryY3 = "0.00";
                NcFlowY1 = "0.00";
                NcFlowY2 = "0.00";
                NcFlowY3 = "0.00";
                NetIncomeY1 = "0.00";
                NetIncomeY2 = "0.00";
                NetIncomeY3 = "0.00";
                ProfitRatioY1 = "0.00";
                ProfitRatioY2 = "0.00";
                ProfitRatioY3 = "0.00";
                RevenueY1 = "0.00";
                RevenueY2 = "0.00";
                RevenueY3 = "0.00";
                StiY1 = "0.00";
                StiY2 = "0.00";
                StiY3 = "0.00";
                TcAssetsY1 = "0.00";
                TcAssetsY2 = "0.00";
                TcAssetsY3 = "0.00";
                TcLiabltyY1 = "0.00";
                TcLiabltyY2 = "0.00";
                TcLiabltyY3 = "0.00";
                Year1 = "";
                Year2 = "";
                Year3 = "";
                ZakatY1 = "0.00";
                ZakatY2 = "0.00";
                ZakatY3 = "0.00";
            }
        }

        private string _successMessage = AppResources.VatInstalmentPlanSubmittedSuccess;
        public string SuccessMessage
        {
            get
            {
                return _successMessage;
            }
            set
            {
                _successMessage = value;
                OnPropertyChanged("SuccessMessage");
            }
        }
        public OldZakatInstalmentPlanViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

            EnableDeclarationContinue();

            if (Preferences.Get("isZakat", false))
            {
                ZakatTitle = AppResources.ZakatInstalmetSelectTypeZakat;
            }
            else
            {
                ZakatTitle = AppResources.ZakatInstalmetSelectTypeIncomeTax;
            }

            GoBackClick = new Command(() =>
            {
                Backnavigations();
            });
            onMoreOptionClicked = new Command(async () =>
            {
                await MopupService.Instance.PushAsync(new MoreMenuPopUpPageViewRTwo(ListOfActionButtonsApplicable));
            });

            ShowFinancialStatementPicker = new Command(async() =>
            {
              await showPickerDialog();
            });
            CloseClick = new Command(() =>
            {
                CurrentIndex = 1;
                EnableSlectionView();
            });
            GoBackToBills = new Command(() =>
            {
                EnableVATBillView();
            });
            GoBackToAggrement = new Command(() =>
            {
                EnableAgreementView();
            });
            GoBackToAttachments = new Command(() =>
            {
                EnableAttachmentsView();
            });

            VATInstalationClicked = new Command(async()=> await VATInstalationTapped());
            ReasonContinueBtnTapped = new Command(async () => await ReasonContinueBtnClicked());

            AggrementContinueBtnTapped = new Command(async () => await AggrementContinueBtnClicked());
            BillContinueBtnTapped = new Command(async () => await BillContinueBtnClicked());
            AttachmentsContinueBtnTapped = new Command(async () => await AttachmentsContinueBtnClicked());
            StatementsContinueBtnTapped = new Command(async () => await StatementsContinueBtnClicked());
            SummaryContinueBtnTapped = new Command(async () => await SummaryContinueBtnClicked());
            SummaryInstallmentDetailsBtnTapped = new Command(async () => await SummaryInstallmentDetailsBtnClicked());
            OnZakatInstalmentReasonTapped = new Command(async () => await OnZakatInstalmentReasonClicked());
            BankStatementsAttachmentTapped = new Command(async () => await BankStatementsAttachmentClicked());
            FinanceAttachmentTapped = new Command(async () => await FinanceAttachmentClicked());

            InstallmentDetailsBtnTapped = new Command(() =>
            {

                EnableInstalmentsScheduleView();

            });

            ZakatInstalmentPlanModel = new ZakatInstalmentPlanModel();
            SelectedOutletOption = new ZakatInstalmentPlanModel();
            AddOutletDecisionOptions();
            AddFrequencyOptions();


        }

        private string _idType = "";
        public string IDType
        {
            get { return _idType; }
            set
            {
                _idType = value;
                OnPropertyChanged("IDType");
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
                _ListOfActionButtonsApplicable = value;
                OnPropertyChanged("ListOfActionButtonsApplicable");
            }
        }


        public Dictionary<string, string> IDTypeDictionary = null;

        public void showInstructionsDialog()
        {
            try
            {

                EnableSlectionView();
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await ShowDialog(ex.Message);
                    _navigationService.GoBack();
                });
            }
        }



        public void setMoreOptioButtons()
        {
            var listOfActionButtonsApplicable = new List<string>();
            listOfActionButtonsApplicable.Add(AppResources.ZZSaveAsDraft);
            ListOfActionButtonsApplicable = listOfActionButtonsApplicable;
        }
        public void AddOutletDecisionOptions()
        {
            var outletDecisionOptions = new ObservableCollection<ZakatInstalmentPlanModel>();
            outletDecisionOptions.Add(new ZakatInstalmentPlanModel
            {
                ActiveOutletDecisionOptions = AppResources.ZakatFinancialCrisis,
                ActiveOutletDecisionOptionsIsSelected = false
            });
            outletDecisionOptions.Add(new ZakatInstalmentPlanModel
            {
                ActiveOutletDecisionOptions = AppResources.ZakatDisputeInFavorOfGAZT,
                ActiveOutletDecisionOptionsIsSelected = false
            });
            outletDecisionOptions.Add(new ZakatInstalmentPlanModel
            {
                ActiveOutletDecisionOptions = AppResources.ZakatOtherReason,
                ActiveOutletDecisionOptionsIsSelected = false
            });
            OutletDecisionOptions = outletDecisionOptions;
        }

        public void AddFrequencyOptions()
        {
            var zakatAgreementOptions = new ObservableCollection<InstalmentAgreementFrequencyModel>();
            zakatAgreementOptions.Add(new InstalmentAgreementFrequencyModel
            {
                FrequencyOptions = AppResources.ZakatInstalmetMonthly,
                IsSelected = true
            });
            zakatAgreementOptions.Add(new InstalmentAgreementFrequencyModel
            {
                FrequencyOptions = AppResources.ZakatInstalmetQuarterly,
                IsSelected = false
            });
            zakatAgreementOptions.Add(new InstalmentAgreementFrequencyModel
            {
                FrequencyOptions = AppResources.ZakatInstalmetHalfYearly,
                IsSelected = false
            });
            zakatAgreementOptions.Add(new InstalmentAgreementFrequencyModel
            {
                FrequencyOptions = AppResources.ZakatInstalmetYearly,
                IsSelected = false
            });

            ZakatAgreementOptions = zakatAgreementOptions;
        }


        #region Enabling Views
        public void EnableDisplayInstalmentsView()
        {
            IsBackButtonVisible = false;
            IsSelectionViewEnabled = false;
            IsOutletViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsSummaryViewEnabled = false;
            IsAgreementViewEnabled = false;
            IsDisplayInstalmentsVisible = true;
            IsBillsViewEnabled = false;
            InstalmentPlanAgreementsVisible = false;
            IsStatementViewEnabled = false;
            IsVATBillsViewEnabled = false;
            CurrentIndex = 3;
            selectedPage = (int)PagesEnum.IsDisplayInstalmentsVisible;

        }
        public void EnableSlectionView()
        {

            IsBackButtonVisible = false;
            IsSelectionViewEnabled = true;
            IsOutletViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsSummaryViewEnabled = false;
            IsDisplayInstalmentsVisible = false;
            IsAgreementViewEnabled = false;
            IsBillsViewEnabled = false;
            IsStatementViewEnabled = false;
            IsVATBillsViewEnabled = false;
            InstalmentPlanAgreementsVisible = false;
            selectedPage = (int)PagesEnum.ZakatSelectionView;

        }

        public void EnableBillView()
        {

            IsBackButtonVisible = false;
            IsSelectionViewEnabled = false;
            IsOutletViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsSummaryViewEnabled = false;
            IsDisplayInstalmentsVisible = false;
            IsAgreementViewEnabled = false;
            IsBillsViewEnabled = true;
            IsVATBillsViewEnabled = true;
            IsStatementViewEnabled = false;
            selectedPage = (int)PagesEnum.ZakatBillView;

        }
        public void EnableVATBillView()
        {
            IsBackButtonVisible = false;
            IsSelectionViewEnabled = false;
            IsOutletViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsDisplayInstalmentsVisible = false;
            IsSummaryViewEnabled = false;
            IsAgreementViewEnabled = false;
            IsBillsViewEnabled = false;
            IsVATBillsViewEnabled = true;
            IsStatementViewEnabled = false;
            CurrentIndex = 1;
            selectedPage = (int)PagesEnum.ZakatBillView;

        }

        public void EnableAgreementView()
        {

            IsBackButtonVisible = false;
            IsSelectionViewEnabled = false;
            IsOutletViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsSummaryViewEnabled = false;
            IsAgreementViewEnabled = true;
            IsBillsViewEnabled = false;
            IsVATBillsViewEnabled = false;
            IsDisplayInstalmentsVisible = false;
            IsStatementViewEnabled = false;
            InstalmentPlanAgreementsVisible = false;
            CurrentIndex = 2;
            selectedPage = (int)PagesEnum.ZakatAggrementView;

        }
        public void EnableStatementsView()
        {

            IsBackButtonVisible = false;
            IsSelectionViewEnabled = false;
            IsOutletViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsSummaryViewEnabled = false;
            IsAgreementViewEnabled = false;
            IsBillsViewEnabled = false;
            IsVATBillsViewEnabled = false;
            IsStatementViewEnabled = true;
            IsDisplayInstalmentsVisible = false;
            InstalmentPlanAgreementsVisible = false;

            selectedPage = (int)PagesEnum.ZakatEnableStatementsView;

        }

        public void EnableAttachmentsView()
        {
            IsSelectionViewEnabled = false;
            IsOutletViewEnabled = false;
            IsAttachmentsViewEnabled = true;
            IsSummaryViewEnabled = false;
            IsAgreementViewEnabled = false;
            IsBillsViewEnabled = false;
            IsStatementViewEnabled = false;
            IsDisplayInstalmentsVisible = false;
            InstalmentPlanAgreementsVisible = false;
            CurrentIndex = 5;
            selectedPage = (int)PagesEnum.ZakatAttachmentsView;

        }


        public void EnableSummaryView()
        {
            IsSelectionViewEnabled = false;
            IsOutletViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsSummaryViewEnabled = true;
            IsVATBillsViewEnabled = false;
            IsAgreementViewEnabled = false;
            IsBillsViewEnabled = false;
            IsStatementViewEnabled = false;
            IsDisplayInstalmentsVisible = false;
            InstalmentPlanAgreementsVisible = false;
            CurrentIndex = 6;
            selectedPage = (int)PagesEnum.ZakatSummaryView;


        }

        public void EnableInstalmentsScheduleView()
        {

            IsBackButtonVisible = false;
            IsSelectionViewEnabled = false;
            IsOutletViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsSummaryViewEnabled = false;
            IsAgreementViewEnabled = false;
            IsBillsViewEnabled = false;
            IsVATBillsViewEnabled = false;
            IsStatementViewEnabled = false;
            IsDisplayInstalmentsVisible = false;
            InstalmentPlanAgreementsVisible = true;
            CurrentIndex = 4;
            selectedPage = (int)PagesEnum.InstalmentPlanAgreementsVisible;

        }

        public void ReleaseDetailsConBtnClicked()
        {
            IsBackButtonVisible = false;
            IsSelectionViewEnabled = true;
            IsOutletViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsSummaryViewEnabled = false;
            IsAgreementViewEnabled = false;
            IsBillsViewEnabled = false;
            IsStatementViewEnabled = false;
            IsVATBillsViewEnabled = false;
            selectedPage = (int)PagesEnum.ZakatSelectionView;
        }

        #endregion



        public void calculateYear1Data()
        {
            try
            {

                if (StiY1.Length > 0 && double.Parse(StiY1) != 0 && CashBankY1.Length > 0 && double.Parse(CashBankY1) != 0)
                {
                    NcFlowY1 = (double.Parse(StiY1) / double.Parse(CashBankY1)).ToString("0.00");
                }
                else
                {
                    NcFlowY1 = "0.00";
                }



                if (TcAssetsY1.Length > 0 && double.Parse(TcAssetsY1) != 0 && ZakatY1.Length > 0 && double.Parse(ZakatY1) != 0)
                {
                    ProfitRatioY1 = (double.Parse(TcAssetsY1) / double.Parse(ZakatY1)).ToString("0.00");
                }
                else
                {
                    ProfitRatioY1 = "0.00";
                }




                if (ZakatY1.Length > 0 && double.Parse(ZakatY1) != 0)
                {
                    double a = 0.0; double b = 0.0;
                    if (DebitorsY1.Length > 0 && double.Parse(DebitorsY1) != 0 && InventoryY1.Length > 0 && double.Parse(InventoryY1) != 0)
                    {
                        a = double.Parse(DebitorsY1);
                        b = double.Parse(InventoryY1);
                    }
                    else if (DebitorsY1.Length > 0 && double.Parse(DebitorsY1) != 0)
                    {
                        a = double.Parse(DebitorsY1);
                        b = 0;
                    }
                    else if (InventoryY1.Length > 0 && double.Parse(InventoryY1) != 0)
                    {
                        a = 0;
                        b = double.Parse(InventoryY1);
                    }



                    double c = a + b;
                    CashRatioY1 = (c / double.Parse(ZakatY1)).ToString("0.00");
                }
                else
                {
                    CashRatioY1 = "0.00";
                }
            }
            catch (Exception)
            {
            }
        }


        public void calculateYear2Data()
        {
            try
            {
                if (StiY2.Length > 0 && double.Parse(StiY2) != 0 && CashBankY2.Length > 0 && double.Parse(CashBankY2) != 0)
                {
                    NcFlowY2 = (double.Parse(StiY2) / double.Parse(CashBankY2)).ToString("0.00");
                }
                else
                {
                    NcFlowY2 = "0.00";
                }



                if (TcAssetsY2.Length > 0 && double.Parse(TcAssetsY2) != 0 && ZakatY2.Length > 0 && double.Parse(ZakatY2) != 0)
                {
                    ProfitRatioY2 = (double.Parse(TcAssetsY2) / double.Parse(ZakatY2)).ToString("0.00");
                }
                else
                {
                    ProfitRatioY2 = "0.00";
                }




                if (ZakatY2.Length > 0 && double.Parse(ZakatY2) != 0)
                {
                    double a = 0.0; double b = 0.0;
                    if (DebitorsY2.Length > 0 && double.Parse(DebitorsY2) != 0 && InventoryY2.Length > 0 && double.Parse(InventoryY2) != 0)
                    {
                        a = double.Parse(DebitorsY2);
                        b = double.Parse(InventoryY2);
                    }
                    else if (DebitorsY2.Length > 0 && double.Parse(DebitorsY2) != 0)
                    {
                        a = double.Parse(DebitorsY2);
                        b = 0;
                    }
                    else if (InventoryY2.Length > 0 && double.Parse(InventoryY2) != 0)
                    {
                        a = 0;
                        b = double.Parse(InventoryY2);
                    }



                    double c = a + b;
                    CashRatioY2 = (c / double.Parse(ZakatY2)).ToString("0.00");
                }
                else
                {
                    CashRatioY2 = "0.00";
                }



            }
            catch (Exception)
            {
            }




        }



        public void calculateYear3Data()
        {
            try
            {
                if (StiY3.Length > 0 && double.Parse(StiY3) != 0 && CashBankY3.Length > 0 && double.Parse(CashBankY3) != 0)
                {
                    NcFlowY3 = (double.Parse(StiY3) / double.Parse(CashBankY3)).ToString("0.00");
                }
                else
                {
                    NcFlowY3 = "0.00";
                }



                if (TcAssetsY3.Length > 0 && double.Parse(TcAssetsY3) != 0 && ZakatY3.Length > 0 && double.Parse(ZakatY3) != 0)
                {
                    ProfitRatioY3 = (double.Parse(TcAssetsY3) / double.Parse(ZakatY3)).ToString("0.00");
                }
                else
                {
                    ProfitRatioY3 = "0.00";
                }




                if (ZakatY3.Length > 0 && double.Parse(ZakatY3) != 0)
                {
                    double a = 0.0; double b = 0.0;
                    if (DebitorsY3.Length > 0 && double.Parse(DebitorsY3) != 0 && InventoryY3.Length > 0 && double.Parse(InventoryY3) != 0)
                    {
                        a = double.Parse(DebitorsY3);
                        b = double.Parse(InventoryY3);
                    }
                    else if (DebitorsY3.Length > 0 && double.Parse(DebitorsY3) != 0)
                    {
                        a = double.Parse(DebitorsY3);
                        b = 0;
                    }
                    else if (InventoryY3.Length > 0 && double.Parse(InventoryY3) != 0)
                    {
                        a = 0;
                        b = double.Parse(InventoryY3);
                    }



                    double c = a + b;
                    CashRatioY3 = (c / double.Parse(ZakatY3)).ToString("0.00");
                }
                else
                {
                    CashRatioY3 = "0.00";
                }
            }
            catch (Exception)
            {
            }



        }

        public async Task EnableSucessScreenAsync()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new OldZakatInstalmentPlanSuccessPage());
        }

        public void EnableDeclarationContinue()
        {
            if (BankStatementsAttachmentsListViewData != null && BankStatementsAttachmentsListViewData.Count > 0)
            {
                IsDeclarationEnabled = true;
            }
            else
            {
                IsDeclarationEnabled = false;
            }
        }



        private bool _isDeclarationEnabled = false;



        public bool IsDeclarationEnabled
        {
            get { return _isDeclarationEnabled; }
            set
            {
                _isDeclarationEnabled = value;
                DeclarationButtonBackGroundColor = _isDeclarationEnabled ? (Color)Application.Current.Resources["Secondary"] : (Color)Application.Current.Resources["ButtonGray"];



                OnPropertyChanged("IsDeclarationEnabled");
            }
        }
        private Color _declarationButtonBackGroundColor = (Color)Application.Current.Resources["Secondary"];
        public Color DeclarationButtonBackGroundColor
        {
            get
            {
                return _declarationButtonBackGroundColor;
            }
            set
            {
                if (_declarationButtonBackGroundColor == value)
                {
                    return;
                }
                _declarationButtonBackGroundColor = value;
                OnPropertyChanged("DeclarationButtonBackGroundColor");
            }
        }

        public async Task VATInstalationTapped()
        {
            try
            {
                if (IsVatTermsChecked)
                {
                    await MopupService.Instance.PopAsync();
                }
                else
                {

                    await ShowDialog(AppResources.BPInstructionsAndConditionsAlert);
                }
            }
            catch (InternetException ex)
            {
                await ShowDialog(ex.Message);
                _navigationService.GoBack();
            }
        }
        public async Task ReasonContinueBtnClicked()
        {
            try
            {
                if (IsZakatSelected || IsIncomeTaxViewEnabled || IsVATAmountVisible)
                {
                    if (IsZakat)
                    {
                        ZakatInstalments.d.AInstReqFor = "2";

                    }
                    else
                    {
                        ZakatInstalments.d.AInstReqFor = "1";
                    }

                    if (IDType == IDTypeDictionary[AppResources.ZakatFinancialCrisis])
                    {
                        ZakatInstalments.d.AInstReqReason = "1";
                    }
                    else if (IDType == IDTypeDictionary[AppResources.ZakatDisputeInFavorOfGAZT])
                    {
                        ZakatInstalments.d.AInstReqReason = "2";
                    }
                    else if (IDType == IDTypeDictionary[AppResources.ZakatOtherReason])
                    {
                        ZakatInstalments.d.AInstReqReason = "3";
                    }
                    else
                    {
                        ZakatInstalments.d.AInstReqReason = "1";
                    }


                    await GetZaktaInvoiceList();
                }
                else
                {
                    await ShowDialog(AppResources.ZakatInstalmentPleaseChooseOneReason);
                }

            }
            catch (InternetException ex)
            {
                await ShowDialog(ex.Message);
                _navigationService.GoBack();
            }
        }

        public async Task ShowDialog(string msg)
        {
            await _dialogService.ShowMessage(msg, AppResources.Information);
        }

        public async Task BillContinueBtnClicked()
        {
            try
            {
                double totalAmount = double.Parse(TotalAmountSAR.Replace(" SAR", ""));

                if (MinInstalments == 0)
                {

                    MinInstalments = 1;
                }

                if (totalAmount == 0)
                {
                    await ShowDialog(AppResources.ZakatNoInvoicesToBeAdded);
                    _navigationService.GoBack();
                }
                else
                {
                    EnableAgreementView();
                }

                var instalmentAgreementFrequncyModel = new InstalmentAgreementFrequencyModel();
                if (SelectedFrequencyType == "1")
                {
                    instalmentAgreementFrequncyModel.FrequencyOptions = AppResources.ZakatInstalmetMonthly;
                    updateInstalmentsOnSlider(instalmentAgreementFrequncyModel);
                }
                else if (SelectedFrequencyType == "2")
                {
                    instalmentAgreementFrequncyModel.FrequencyOptions = AppResources.ZakatInstalmetQuarterly;
                    updateInstalmentsOnSlider(instalmentAgreementFrequncyModel);
                }
                else if (SelectedFrequencyType == "3")
                {
                    instalmentAgreementFrequncyModel.FrequencyOptions = AppResources.ZakatInstalmetHalfYearly;
                    updateInstalmentsOnSlider(instalmentAgreementFrequncyModel);
                }
                else if (SelectedFrequencyType == "4")
                {
                    instalmentAgreementFrequncyModel.FrequencyOptions = AppResources.ZakatInstalmetYearly;
                    updateInstalmentsOnSlider(instalmentAgreementFrequncyModel);
                }

                return;
            }
            catch (InternetException ex)
            {
                await ShowDialog(ex.Message);
                _navigationService.GoBack();
            }
        }


        public async Task AggrementContinueBtnClicked()
        {
            try
            {

                if (IsFinsancialStatementsEditable && (Year1.Length < 4 || Year2.Length < 4 || Year3.Length < 4))
                {
                    await _dialogService.ShowMessage(AppResources.ZZPleasefillthemandatoryfields + "(" + AppResources.ZakatYearOne + ", " + AppResources.ZakatYearTwo + ", " + AppResources.ZakatYearThree + ")", AppResources.Information);
                    return;
                }
                if (IsFinsancialStatementsEditable && (string.IsNullOrEmpty(CashBankY1) ||
                    string.IsNullOrEmpty(CashBankY2) ||
                    string.IsNullOrEmpty(CashBankY3) ||
                    string.IsNullOrEmpty(CashRatioY1) ||
                    string.IsNullOrEmpty(CashRatioY2) ||
                    string.IsNullOrEmpty(CashRatioY3) ||
                    string.IsNullOrEmpty(DebitorsY1) ||
                    string.IsNullOrEmpty(DebitorsY2) ||
                    string.IsNullOrEmpty(DebitorsY3) ||
                    string.IsNullOrEmpty(InventoryY1) ||
                    string.IsNullOrEmpty(InventoryY2) ||
                    string.IsNullOrEmpty(InventoryY3) ||
                    string.IsNullOrEmpty(NcFlowY1) ||
                    string.IsNullOrEmpty(NcFlowY2) ||
                    string.IsNullOrEmpty(NcFlowY3) ||
                    string.IsNullOrEmpty(NetIncomeY1) ||
                    string.IsNullOrEmpty(NetIncomeY2) ||
                    string.IsNullOrEmpty(NetIncomeY3) ||
                    string.IsNullOrEmpty(ProfitRatioY1) ||
                    string.IsNullOrEmpty(ProfitRatioY2) ||
                    string.IsNullOrEmpty(ProfitRatioY3) ||
                    string.IsNullOrEmpty(RevenueY1) ||
                    string.IsNullOrEmpty(RevenueY2) ||
                    string.IsNullOrEmpty(RevenueY3) ||
                    string.IsNullOrEmpty(StiY1) ||
                    string.IsNullOrEmpty(StiY2) ||
                    string.IsNullOrEmpty(StiY3) ||
                    string.IsNullOrEmpty(TcAssetsY1) ||
                    string.IsNullOrEmpty(TcAssetsY2) ||
                    string.IsNullOrEmpty(TcAssetsY3) ||
                    string.IsNullOrEmpty(TcLiabltyY1) ||
                    string.IsNullOrEmpty(TcLiabltyY2) ||
                    string.IsNullOrEmpty(TcLiabltyY3) ||
                    string.IsNullOrEmpty(ZakatY1) ||
                    string.IsNullOrEmpty(ZakatY2) ||
                    string.IsNullOrEmpty(ZakatY3)))
                {
                    await _dialogService.ShowMessage(AppResources.ZZPleasefillthemandatoryfields, AppResources.Information);
                    return;
                }

                EnableAttachmentsView();


            }
            catch (InternetException ex)
            {
                await ShowDialog(ex.Message);
                _navigationService.GoBack();
            }
        }


        public async Task OutletContinueBtnClicked()
        {
            try
            {
                EnableAttachmentsView();
            }
            catch (InternetException ex)
            {
                await ShowDialog(ex.Message);
                _navigationService.GoBack();
            }
        }


        public async Task AttachmentsContinueBtnClicked()
        {
            try
            {
                if (BankStatementsAttachmentsListViewData != null && BankStatementsAttachmentsListViewData.Count > 0)
                {
                    EnableSummaryView();
                    PopulateSummaryReasonData();
                    PopulateSummaryAttachments();
                }
                else
                {
                    await ShowDialog(AppResources.VRUploadYourDocument);
                }



            }
            catch (InternetException ex)
            {
                await ShowDialog(ex.Message);
                _navigationService.GoBack();
            }
        }
        public async Task StatementsContinueBtnClicked()
        {
            try
            {
                if (AttachmentsListViewData == null)
                {
                    AttachmentsListViewData = new ObservableCollection<Attachment>();
                }

                EnableAttachmentsView();

            }
            catch (InternetException ex)
            {
                await ShowDialog(ex.Message);
                _navigationService.GoBack();
            }
        }


        private void PopulateSummaryReasonData()
        {
            var summarySelectedBillsList = new ObservableCollection<ZakatSelectBillModel>();

            for (int i = 0; i < selectedList.Count; i++)
            {


                DateTime dateStart = new DateTime();
                CultureInfo cultureInfo = new CultureInfo("ar-SA");
                string apiDate = @"""" + selectedList[i].ADueDtTb + @"""";
                dateStart = JsonConvert.DeserializeObject<DateTime>(apiDate);

                GregorianCalendar hjCalendar = new GregorianCalendar();
                int year = hjCalendar.GetYear(dateStart);
                int month = hjCalendar.GetMonth(dateStart);
                int day = hjCalendar.GetDayOfMonth(dateStart);

                string dateStr = string.Format("{0:00}/{1}/{2}", day, month, year);

                string dt1 = string.Empty;
                string[] dts = null;
                dts = dateStr.Split('/');
                dt1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];

                summarySelectedBillsList.Add(new ZakatSelectBillModel()
                {
                    billNumber = AppResources.Bill + (i + 1).ToString("00") + ":",
                    amount = "0",
                    saadNumber = selectedList[i].AIvNoTb.ToString(),
                    taxPeriod = dt1,
                    isSelected = false,
                    //billType = ZakatTitle
                    billType = IsZakat ? AppResources.ZakatInstalmetSelectTypeZakat : AppResources.ZakatInstalmetSelectTypeIncomeTax

                });
            }
            SummarySelectedBillsList = summarySelectedBillsList;




            if (SelectedFrequencyType == "1")
            {

                SelectedFrequencyName = AppResources.ZakatInstalmetMonthly;

            }
            else if (SelectedFrequencyType == "2")
            {

                SelectedFrequencyName = AppResources.ZakatInstalmetQuarterly;
            }
            else if (SelectedFrequencyType == "3")
            {

                SelectedFrequencyName = AppResources.ZakatInstalmetHalfYearly;
            }
            else if (SelectedFrequencyType == "4")
            {
                SelectedFrequencyName = AppResources.ZakatInstalmetYearly;
            }
        }


        public bool isDraftClicked = false;
        public async Task OnSaveDraftClicked()
        {

            double totalAmount = double.Parse(TotalAmountSAR.Replace(" SAR", ""));

            if (totalAmount > 0)
            {

                try
                {
                    IsNewLoading = true;

                    ZakatInstalments.d.AOneYrTb = Year1;
                    ZakatInstalments.d.ATwoYrTb = Year2;
                    ZakatInstalments.d.AThreeYrTb = Year3;

                    if (Year1.Length >= 4 || Year2.Length >= 4 || Year3.Length >= 4)
                    {
                        ZakatInstalments.d.ARe1yrTbFg = "1";
                        ZakatInstalments.d.ARe2yrTbFg = "1";
                        ZakatInstalments.d.ARe3yrTbFg = "1";
                    }

                    ZakatInstalments.d.ARev1yrTb = CashBankY1;
                    ZakatInstalments.d.ARev2yrTb = CashBankY2;
                    ZakatInstalments.d.ARev3yrTb = CashBankY3;

                    ZakatInstalments.d.ASi1yrTb = InventoryY1;
                    ZakatInstalments.d.ASi2yrTb = InventoryY2;
                    ZakatInstalments.d.ASi3yrTb = InventoryY3;

                    ZakatInstalments.d.ACb1yrTb = DebitorsY1;
                    ZakatInstalments.d.ACb2yrTb = DebitorsY2;
                    ZakatInstalments.d.ACb3yrTb = DebitorsY3;

                    ZakatInstalments.d.ARe1yrTb = NetIncomeY1;
                    ZakatInstalments.d.ARe2yrTb = NetIncomeY2;
                    ZakatInstalments.d.ARe3yrTb = NetIncomeY3;


                    ZakatInstalments.d.ANi1yrTb = StiY1;
                    ZakatInstalments.d.ANi2yrTb = StiY2;
                    ZakatInstalments.d.ANi3yrTb = StiY3;


                    ZakatInstalments.d.ATl1yrTb = ZakatY1;
                    ZakatInstalments.d.ATl2yrTb = ZakatY2;
                    ZakatInstalments.d.ATl3yrTb = ZakatY3;


                    ZakatInstalments.d.ADeb1yrTb = TcLiabltyY1;
                    ZakatInstalments.d.ADeb2yrTb = TcLiabltyY2;
                    ZakatInstalments.d.ADeb3yrTb = TcLiabltyY3;


                    ZakatInstalments.d.APr1yrTb = NcFlowY1;
                    ZakatInstalments.d.APr2yrTb = NcFlowY2;
                    ZakatInstalments.d.APr3yrTb = NcFlowY3;



                    ZakatInstalments.d.ACrt1yrTb = ProfitRatioY1;
                    ZakatInstalments.d.ACrt2yrTb = ProfitRatioY2;
                    ZakatInstalments.d.ACrt3yrTb = ProfitRatioY3;



                    ZakatInstalments.d.ACr1yrTb = RevenueY1;
                    ZakatInstalments.d.ACr2yrTb = RevenueY2;
                    ZakatInstalments.d.ACr3yrTb = RevenueY3;

                    ZakatInstalments.d.ATa1yrTb = TcAssetsY1;
                    ZakatInstalments.d.ATa2yrTb = TcAssetsY2;
                    ZakatInstalments.d.ATa3yrTb = TcAssetsY3;


                    ZakatInstalments.d.APc1yrTb = CashRatioY1;
                    ZakatInstalments.d.APc2yrTb = CashRatioY2;
                    ZakatInstalments.d.APc3yrTb = CashRatioY3;

                    var noinstalments = Convert.ToInt32(NumberOFInstalmentSliderValue);
                    ZakatInstalments.d.ANoOfInstTp = noinstalments.ToString();
                    ZakatInstalments.d.APlanDurPeri = noinstalments.ToString();



                    if (IsFinsancialStatementsEditable)
                    {

                        ZakatInstalments.d.AHoldFinStat = "1";
                    }
                    else
                    {

                        ZakatInstalments.d.AHoldFinStat = "2";

                    }


                    ZakatInstalments.d.AAgree = "X";
                    ZakatInstalments.d.APaymentFreq = SelectedFrequencyType;
                    if (CurrentIndex == 1)
                    {
                        ZakatInstalments.d.AStep = 1;

                    }
                    else if (CurrentIndex == 2 || CurrentIndex == 3)
                    {
                        ZakatInstalments.d.AStep = 3;

                    }
                    else
                    {
                        ZakatInstalments.d.AStep = 4;

                    }


                    ZakatInstalments.d.Savez = "X";


                    if (!isDraftClicked)
                    {
                        isDraftClicked = true;

                        var ZakatInstalmentsdata = await SubmitClicked();

                        if (ZakatInstalmentsdata != null && ZakatInstalmentsdata.d != null)
                        {
                            ZakatInstalments = ZakatInstalmentsdata;

                            IsInitialDraft = true;
                            App.selectedZakatItem = ZakatInstalments.d.Fbnum;
                            setMoreOptioButtons();

                            List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                            HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                            NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                            headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                            headerAmountInfo.IsLinkAvailable = false;
                            //headerAmountInfo.Message = string.Format(AppResources.ZakatDraftSaved, "  " + ZakatInstalments.d.Fbnum);
                            headerAmountInfo.Message = string.Format(AppResources.ZakatDraftSaved + " " + ZakatInstalments.d.Fbnum + " " + AppResources.ZakatDraftSaved1, " " + ZakatInstalments.d.Fbnum);

                            headerWithInfos.Add(headerAmountInfo);

                            newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                            newDesignPopUp.HeaderWithInfos = headerWithInfos;
                            newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                            await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                        }
                        else
                        {

                            if (string.IsNullOrEmpty(WebServiceManager.ErrorMessageForVAT))
                            {
                                IsNewLoading = false;
                                IsLoading = false;
                                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);


                            }
                            else
                            {
                                IsNewLoading = false;
                                IsLoading = false;
                                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                            }
                        }
                    }

                    IsNewLoading = false;
                }

                catch (GAZTVATRegistrationInProcessException ex)
                {

                    IsLoading = false;
                    await ShowDialog(ex.Message);
                }


            }
            else
            {

                await _dialogService.ShowMessage(AppResources.ZakatNoInvoicesToBeAdded, AppResources.Information);
                _navigationService.GoBack();
            }






        }

        public async Task VoidMsg()
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

        public async Task VATSetReturnVoidAsync()
        {
            try
            {
                IsNewLoading = true;
                if (StatementList != null)
                {

                    var instalmentsList = StatementList.ToList();


                    if (instalmentsList.Count != 0)
                    {


                        string apiDate = instalmentsList[0].DueDt;
                        if (!apiDate.Contains("Date"))
                        {

                            for (int i = 0; i < instalmentsList.Count; i++)
                            {

                                DateTime dt = Convert.ToDateTime(instalmentsList[i].DueDt);
                                dt = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                                {
                                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                                };
                                //var jsonDateTime = JsonConvert.SerializeObject(dt, microsoftDateFormatSettings);
                                var jsonDateTime = JsonConvert.SerializeObject(dt.Date, microsoftDateFormatSettings);
                                string[] dateList = jsonDateTime.Split('+');
                                jsonDateTime = dateList[0].Replace("\"\\", "");
                                var t = jsonDateTime.Replace("\\/\"", "");
                                t = t + "/";
                                instalmentsList[i].DueDt = t;

                            }
                        }

                    }
                }






                if (CurrentIndex == 1)
                {
                    ZakatInstalments.d.AStep = 1;

                }
                else if (CurrentIndex == 2 || CurrentIndex == 3)
                {
                    ZakatInstalments.d.AStep = 3;

                }
                else
                {
                    ZakatInstalments.d.AStep = 4;

                }



                if (!isDraftClicked)
                {
                    isDraftClicked = true;

                    var ZakatInstalmentsdata = await SubmitClicked();


                    if (ZakatInstalmentsdata != null && ZakatInstalmentsdata.d != null)
                    {

                        ZakatInstalments = ZakatInstalmentsdata;
                        List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                        HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                        NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                        headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                        headerAmountInfo.IsLinkAvailable = false;
                        headerAmountInfo.Message = AppResources.ZZGeneralMessage_ZakatCancelled;

                        headerWithInfos.Add(headerAmountInfo);


                        newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                        newDesignPopUp.HeaderWithInfos = headerWithInfos;
                        newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                        await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                        _navigationService.GoBack();
                    }
                    else
                    {
                        IsLoading = false;
                        if (string.IsNullOrEmpty(WebServiceManager.ErrorMessageForVAT))
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

                            _navigationService.GoBack();
                        }
                        else
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
                        }
                    }
                }
                IsNewLoading = false;
            }

            catch (GAZTVATRegistrationInProcessException ex)
            {
                IsLoading = false;
                await ShowDialog(ex.Message);

            }
        }


        bool isSubmitClicked = false;
        public async Task SummaryContinueBtnClicked()
        {
            try
            {
                IsLoading = true;

                ZakatInstalments.d.AOneYrTb = Year1;
                ZakatInstalments.d.ATwoYrTb = Year2;
                ZakatInstalments.d.AThreeYrTb = Year3;


                if (Year1.Length < 4 || Year2.Length < 4 || Year3.Length < 4)
                {
                    ZakatInstalments.d.ARe1yrTbFg = "1";
                    ZakatInstalments.d.ARe2yrTbFg = "1";
                    ZakatInstalments.d.ARe3yrTbFg = "1";
                }

                ZakatInstalments.d.ARev1yrTb = CashBankY1;
                ZakatInstalments.d.ARev2yrTb = CashBankY2;
                ZakatInstalments.d.ARev3yrTb = CashBankY3;

                ZakatInstalments.d.ASi1yrTb = InventoryY1;
                ZakatInstalments.d.ASi2yrTb = InventoryY2;
                ZakatInstalments.d.ASi3yrTb = InventoryY3;

                ZakatInstalments.d.ACb1yrTb = DebitorsY1;
                ZakatInstalments.d.ACb2yrTb = DebitorsY2;
                ZakatInstalments.d.ACb3yrTb = DebitorsY3;

                ZakatInstalments.d.ARe1yrTb = NetIncomeY1;
                ZakatInstalments.d.ARe2yrTb = NetIncomeY2;
                ZakatInstalments.d.ARe3yrTb = NetIncomeY3;


                ZakatInstalments.d.ANi1yrTb = StiY1;
                ZakatInstalments.d.ANi2yrTb = StiY2;
                ZakatInstalments.d.ANi3yrTb = StiY3;


                ZakatInstalments.d.ATl1yrTb = ZakatY1;
                ZakatInstalments.d.ATl2yrTb = ZakatY2;
                ZakatInstalments.d.ATl3yrTb = ZakatY3;


                ZakatInstalments.d.ADeb1yrTb = TcLiabltyY1;
                ZakatInstalments.d.ADeb2yrTb = TcLiabltyY2;
                ZakatInstalments.d.ADeb3yrTb = TcLiabltyY3;


                ZakatInstalments.d.APr1yrTb = NcFlowY1;
                ZakatInstalments.d.APr2yrTb = NcFlowY2;
                ZakatInstalments.d.APr3yrTb = NcFlowY3;

                ZakatInstalments.d.ACrt1yrTb = ProfitRatioY1;
                ZakatInstalments.d.ACrt2yrTb = ProfitRatioY2;
                ZakatInstalments.d.ACrt3yrTb = ProfitRatioY3;

                ZakatInstalments.d.ACr1yrTb = RevenueY1;
                ZakatInstalments.d.ACr2yrTb = RevenueY2;
                ZakatInstalments.d.ACr3yrTb = RevenueY3;

                ZakatInstalments.d.ATa1yrTb = TcAssetsY1;
                ZakatInstalments.d.ATa2yrTb = TcAssetsY2;
                ZakatInstalments.d.ATa3yrTb = TcAssetsY3;

                ZakatInstalments.d.APc1yrTb = CashRatioY1;
                ZakatInstalments.d.APc2yrTb = CashRatioY2;
                ZakatInstalments.d.APc3yrTb = CashRatioY3;

                ZakatInstalments.d.AAgree = "X";
                ZakatInstalments.d.APaymentFreq = SelectedFrequencyType;
                if (CurrentIndex == 1)
                {
                    ZakatInstalments.d.AStep = 1;

                }
                else if (CurrentIndex == 2 || CurrentIndex == 3)
                {
                    ZakatInstalments.d.AStep = 3;

                }
                else
                {
                    ZakatInstalments.d.AStep = 4;

                }

                if (IsFinsancialStatementsEditable)
                {

                    ZakatInstalments.d.AHoldFinStat = "1";
                }
                else
                {

                    ZakatInstalments.d.AHoldFinStat = "2";

                }

                ZakatInstalments.d.Savez = "X";

                ZakatInstalments.d.Submitz = "X";




                ZakatInstalments.d.ANoOfInstTp = NoOfInstalments.ToString();
                ZakatInstalments.d.APlanDurPeri = NoOfInstalments.ToString();


                if (!isSubmitClicked)
                {
                    isSubmitClicked = true;
                    var ZakatInstalmentsdata = await SubmitClicked();

                    if (ZakatInstalmentsdata != null && ZakatInstalmentsdata.d != null)
                    {

                        ZakatInstalments = ZakatInstalmentsdata;
                        ZakatReferanceNumber = ZakatInstalments.d.Fbnum;

                        await Application.Current.MainPage.Navigation.PushAsync(new OldZakatInstalmentPlanSuccessPage());
                    }

                }

            }
            catch (GAZTVATRegistrationInProcessException ex)
            {

                IsLoading = false;
                await ShowDialog(ex.Message);

            }
            catch (InternetException ex)
            {
                IsLoading = false;
                await ShowDialog(ex.Message);
                _navigationService.GoBack();
            }
        }



        public async Task OnZakatInstalmentReasonClicked()
        {
            try
            {
                ObservableCollection<string> ZakatreasonData = new ObservableCollection<string>();
                ZakatreasonData.Add(AppResources.TinDeregistrationReasonBankruptcy);
                ZakatreasonData.Add(AppResources.TinDeregistrationReasonDeath);
                ZakatreasonData.Add(AppResources.TinDeregistrationReasonLiquidation);
                ZakatreasonData.Add(AppResources.TinDeregistrationReasonEstablishmentToCompany);
            }

            catch (InternetException ex)
            {
                await ShowDialog(ex.Message);
                _navigationService.GoBack();
            }
        }

        public async Task SummaryInstallmentDetailsBtnClicked()
        {
            try
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Instalment details schedule is displayed here.", "OK");

            }
            catch (InternetException ex)
            {
                await ShowDialog(ex.Message);
                _navigationService.GoBack();
            }
        }

        #region Attachments View
        public void PopulateAttachments(List<Attachment> attachments)
        {
            var attachmentsListViewData = new ObservableCollection<Attachment>();



            foreach (Attachment attachemnt in attachments)
            {
                attachmentsListViewData.Add(attachemnt);
            }



            if (_bankStatementsAttachment)
            {
                BankStatementsAttachmentsListViewData = attachmentsListViewData;
            }
            else
            {
                FinanceAttachmentsListViewData = attachmentsListViewData;
            }

            EnableDeclarationContinue();

        }

        public async Task BankStatementsAttachmentClicked()
        {
            try
            {
                if (MopupService.Instance.PopupStack.Count > 0) return;
                _bankStatementsAttachment = true;
                if (BankStatementsAttachmentsListViewData == null)
                {
                    BankStatementsAttachmentsListViewData = new ObservableCollection<Attachment>();
                }


                await MopupService.Instance.PushAsync(new FilesUploadPopUpPageView(
                    BankStatementsAttachmentsListViewData.ToList(),
                    WhichAttachment.OldZakatInstalmentBankStatements, ZakatInstalments.d.CaseGuid));

            }
            catch (InternetException ex)
            {
                await ShowDialog(ex.Message);
                _navigationService.GoBack();
            }
        }

        public async Task FinanceAttachmentClicked()
        {
            try
            {
                if (MopupService.Instance.PopupStack.Count > 0) return;
                _bankStatementsAttachment = false;
                if (FinanceAttachmentsListViewData == null)
                {
                    FinanceAttachmentsListViewData = new ObservableCollection<Attachment>();
                }


                await MopupService.Instance.PushAsync(new FilesUploadPopUpPageView(
                FinanceAttachmentsListViewData.ToList(),
                WhichAttachment.OldZakatInstalmentFinance, ZakatInstalments.d.CaseGuid));



            }
            catch (InternetException ex)
            {
                await ShowDialog(ex.Message);
                _navigationService.GoBack();
            }
        }

        public ObservableCollection<Attachment> _financeAttachmentsListViewData { get; set; }

        public ObservableCollection<Attachment> FinanceAttachmentsListViewData
        {
            get { return _financeAttachmentsListViewData; }

            set
            {
                if (_financeAttachmentsListViewData == value)
                {
                    return;
                }

                _financeAttachmentsListViewData = value;
                OnPropertyChanged("FinanceAttachmentsListViewData");
            }
        }

        public ObservableCollection<Attachment> bankStatementsAttachmentsListViewData { get; set; }

        public ObservableCollection<Attachment> BankStatementsAttachmentsListViewData
        {
            get { return bankStatementsAttachmentsListViewData; }

            set
            {
                if (bankStatementsAttachmentsListViewData == value)
                {
                    return;
                }

                bankStatementsAttachmentsListViewData = value;
                OnPropertyChanged("BankStatementsAttachmentsListViewData");
            }
        }


        public void updateInstalmentsOnSlider(InstalmentAgreementFrequencyModel frequencyModel)
        {
            if (frequencyModel.FrequencyOptions == AppResources.ZakatInstalmetMonthly)
            {
                if (selectedList.Count == 2)
                {
                    updateSliderInstalmentValues(2, 24);
                }
                else if (selectedList.Count == 1)
                {
                    updateSliderInstalmentValues(2, 12);
                }
                else
                {
                    updateSliderInstalmentValues(2, 36);
                }
                SelectedFrequencyType = "1";
                InstalmentSliderVisible = true;
            }
            else if (frequencyModel.FrequencyOptions == AppResources.ZakatInstalmetQuarterly)
            {
                if (selectedList.Count == 2)
                {
                    updateSliderInstalmentValues(2, 8);
                }
                else if (selectedList.Count == 1)
                {
                    updateSliderInstalmentValues(2, 4);
                }
                else
                {
                    updateSliderInstalmentValues(2, 12);
                }
                SelectedFrequencyType = "2";
                InstalmentSliderVisible = true;
            }
            else if (frequencyModel.FrequencyOptions == AppResources.ZakatInstalmetHalfYearly)
            {
                if (selectedList.Count == 2)
                {
                    updateSliderInstalmentValues(2, 4);
                }
                else if (selectedList.Count == 1)
                {
                    NoOfInstalments = 2;
                    InstalmentSliderVisible = false;
                }
                else
                {
                    updateSliderInstalmentValues(2, 6);
                }
                SelectedFrequencyType = "3";
            }
            else if (frequencyModel.FrequencyOptions == AppResources.ZakatInstalmetYearly)
            {
                if (selectedList.Count == 2)
                {
                    updateSliderInstalmentValues(1, 2);

                }
                else if (selectedList.Count == 1)
                {
                    NoOfInstalments = 1;
                    InstalmentSliderVisible = false;
                }
                else
                {
                    updateSliderInstalmentValues(1, 3);

                }
                SelectedFrequencyType = "4";

            }

            if (ZakatInstalments.d.APlanDurPeri != null && ZakatInstalments.d.APlanDurPeri != "" && int.Parse(ZakatInstalments.d.APlanDurPeri) > 0)
            {
                NumberOFInstalmentSliderValue = int.Parse(ZakatInstalments.d.APlanDurPeri);
            }
            else
            {
                NumberOFInstalmentSliderValue = 1;
            }

        }

        private void updateSliderInstalmentValues(int minValue, int maxValue)
        {
            InstalmentSliderVisible = true;

            MinInstalments = 0;
            MaxInstalments = 1;

            MaxInstalments = maxValue;
            MinInstalments = minValue;

            MinInstalmentsTitle = AppResources.ZakatMin + " " + minValue;
            MaxInstalmentsTitle = AppResources.ZakatMax + " " + maxValue;
        }

        #endregion

        #region ZakatInvoiceList

        public async Task GetZaktaInvoiceList()
        {
            try
            {
                var totalAmountDue = 0.0;


                var zakatInvoicesListData = new List<OldResults3>();

                for (int i = 0; i < ZakatInstalments.d.Z_INVOICE_UI5Set.Count; i++)
                {

                    if (double.Parse(ZakatInstalments.d.Z_INVOICE_UI5Set[i].AIvAmtTb) > 0)
                    {

                        if (IsZakat)
                        {


                            if ((ZakatInstalments.d.Z_INVOICE_UI5Set[i].AIvAbtyp.Equals("ZAKT")) || (ZakatInstalments.d.Z_INVOICE_UI5Set[i].AIvAbtyp.Equals(AppResources.FORM5Zakat)))
                            {
                                zakatInvoicesListData.Add(ZakatInstalments.d.Z_INVOICE_UI5Set[i]);

                            }
                        }
                        else
                        {
                            if ((ZakatInstalments.d.Z_INVOICE_UI5Set[i].AIvAbtyp.Equals("ITAX")) || (ZakatInstalments.d.Z_INVOICE_UI5Set[i].AIvAbtyp.Equals(AppResources.ZakatInstalmetSelectTypeIncomeTax)))
                            {
                                zakatInvoicesListData.Add(ZakatInstalments.d.Z_INVOICE_UI5Set[i]);

                            }

                        }




                    }

                }

                ZakatInvoicesList = zakatInvoicesListData;


                for (int i = 0; i < ZakatInvoicesList.Count; i++)
                {



                    if (ZakatInvoicesList[i].AIvAbtyp.Equals("ITAX"))
                    {
                        ZakatInvoicesList[i].AIvAbtyp = AppResources.ZakatInstalmetSelectTypeIncomeTax;
                    }
                    else if (ZakatInvoicesList[i].AIvAbtyp.Equals("ZAKT"))
                    {
                        ZakatInvoicesList[i].AIvAbtyp = AppResources.FORM5Zakat;
                    }

                    DateTime dateStart = new DateTime();
                    CultureInfo cultureInfo = new CultureInfo("ar-SA");

                    if (ZakatInvoicesList[i].ADueDtTb != null)
                    {
                        string apiDate = @"""" + ZakatInvoicesList[i].ADueDtTb + @"""";
                        dateStart = JsonConvert.DeserializeObject<DateTime>(apiDate);

                        GregorianCalendar hjCalendar = new GregorianCalendar();
                        int year = hjCalendar.GetYear(dateStart);
                        int month = hjCalendar.GetMonth(dateStart);
                        int day = hjCalendar.GetDayOfMonth(dateStart);

                        string dateStr = string.Format("{0:00}/{1}/{2}", day, month, year);

                        ZakatInvoicesList[i].ADueDtTb = dateStr;

                        string dt1 = string.Empty;
                        string[] dts = null;
                        dts = ZakatInvoicesList[i].ADueDtTb.Split('/');
                        dt1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];
                        ZakatInvoicesList[i].ADueDtTb = dt1;

                    }

                    if (ZakatInvoicesList[i].AIvTb == "1")
                    {
                        selectedList.Add(ZakatInvoicesList[i]);
                        totalAmountDue += Convert.ToDouble(ZakatInvoicesList[i].ADueAmtTb);
                    }
                }

                EnableVATBillView();

                if (ZakatInvoicesList != null && ZakatInvoicesList.Count <= 0)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.ZakatNoInvoicesToBeAdded, AppResources.Information);
                        _navigationService.GoBack();
                    });
                }

                if (totalAmountDue > 0)
                {
                    MaxAmount = Math.Round(totalAmountDue, 2);

                    MaxAmountTitle = AppResources.ZakatMax + " " + string.Format("{0:N}", MaxAmount);
                    
                }
                if (totalAmountDue > 0)
                {
                    MinAmount = Math.Round(totalAmountDue * (20.0f / 100.0f), 2);
                    DownPaymentAmount = Math.Round(totalAmountDue * (float.Parse(ZakatInstalments.d.Percentage) / 100.0f), 2); // MinAmount;

                    MinAmountTitle = AppResources.ZakatMin + " " + string.Format("{0:N}", MinAmount);
                }

                if (App.selectedZakatItem != "")
                {

                    if (!string.IsNullOrEmpty(ZakatInstalments.d.APaymentFreq) && double.Parse(ZakatInstalments.d.APaymentFreq) > 0)
                    {

                        SelectedFrequencyType = ZakatInstalments.d.APaymentFreq;
                        MessagingCenter.Send<object, string>(this, "SelectedFrequencyType", SelectedFrequencyType);
                    }

                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ADpAmt) && double.Parse(ZakatInstalments.d.ADpAmt) > 0)
                    {
                        DownPaymentAmount = double.Parse(ZakatInstalments.d.ADpAmt);
                    }

                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ATotalAmt) && double.Parse(ZakatInstalments.d.ATotalAmt) > 0)
                    {
                        TotalAmountSAR = ZakatInstalments.d.ATotalAmt;
                    }

                    if (!string.IsNullOrEmpty(ZakatInstalments.d.APlanDurNo) && int.Parse(ZakatInstalments.d.APlanDurNo) > 0)
                    {
                        NoOfInstalments = int.Parse(ZakatInstalments.d.APlanDurNo);
                        NumberOFInstalmentSliderValue = int.Parse(ZakatInstalments.d.APlanDurNo);
                    }



                    if (ZakatInstalments.d.AttDetSet != null && ZakatInstalments.d.AttDetSet != null)
                    {
                        var bankAttachmentListViewData = new ObservableCollection<Attachment>();
                        var financialAttachmentListViewData = new ObservableCollection<Attachment>();
                        foreach (var attach in ZakatInstalments.d.AttDetSet)
                        {
                            if (attach.Dotyp == "IPR1")
                            {
                                bankAttachmentListViewData.Add(attach);
                            }
                            else if (attach.Dotyp == "IPR2")
                            {
                                financialAttachmentListViewData.Add(attach);
                            }

                        }
                        BankStatementsAttachmentsListViewData = bankAttachmentListViewData;
                        FinanceAttachmentsListViewData = financialAttachmentListViewData;

                        if (BankStatementsAttachmentsListViewData != null && FinanceAttachmentsListViewData != null)
                        {

                            IsDeclarationEnabled = true;

                        }

                    }

                    if (!string.IsNullOrEmpty(ZakatInstalments.d.AOneYrTb))
                    {

                        Year1 = ZakatInstalments.d.AOneYrTb;
                    }

                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ATwoYrTb))
                    {

                        Year2 = ZakatInstalments.d.ATwoYrTb;
                    }
                    if (!string.IsNullOrEmpty(ZakatInstalments.d.AThreeYrTb))
                    {

                        Year3 = ZakatInstalments.d.AThreeYrTb;
                    }


                    if (Year1.Length >= 4 || Year2.Length >= 4 || Year3.Length >= 4)
                    {
                        ZakatInstalments.d.ARe1yrTbFg = "1";
                        ZakatInstalments.d.ARe2yrTbFg = "1";
                        ZakatInstalments.d.ARe3yrTbFg = "1";
                    }

                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ARev1yrTb))
                    {

                        CashBankY1 = ZakatInstalments.d.ARev1yrTb;
                    }

                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ARev2yrTb))
                    {

                        CashBankY2 = ZakatInstalments.d.ARev2yrTb;
                    }

                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ARev3yrTb))
                    {

                        CashBankY3 = ZakatInstalments.d.ARev3yrTb;
                    }

                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ASi1yrTb))
                    {

                        InventoryY1 = ZakatInstalments.d.ASi1yrTb;
                    }

                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ASi2yrTb))
                    {

                        InventoryY2 = ZakatInstalments.d.ASi2yrTb;
                    }
                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ASi3yrTb))
                    {

                        InventoryY3 = ZakatInstalments.d.ASi3yrTb;
                    }

                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ACb1yrTb))
                    {

                        DebitorsY1 = ZakatInstalments.d.ACb1yrTb;
                    }
                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ACb2yrTb))
                    {

                        DebitorsY2 = ZakatInstalments.d.ACb2yrTb;
                    }
                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ACb3yrTb))
                    {

                        DebitorsY3 = ZakatInstalments.d.ACb3yrTb;
                    }

                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ARe1yrTb))
                    {

                        NetIncomeY1 = ZakatInstalments.d.ARe1yrTb;
                    }
                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ARe2yrTb))
                    {

                        NetIncomeY2 = ZakatInstalments.d.ARe2yrTb;
                    }
                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ARe3yrTb))
                    {

                        NetIncomeY3 = ZakatInstalments.d.ARe3yrTb;
                    }

                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ANi1yrTb))
                    {

                        StiY1 = ZakatInstalments.d.ANi1yrTb;
                    }
                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ANi2yrTb))
                    {

                        StiY2 = ZakatInstalments.d.ANi2yrTb;
                    }
                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ANi3yrTb))
                    {

                        StiY3 = ZakatInstalments.d.ANi3yrTb;
                    }

                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ATl1yrTb))
                    {

                        ZakatY1 = ZakatInstalments.d.ATl1yrTb;
                    }
                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ATl2yrTb))
                    {

                        ZakatY2 = ZakatInstalments.d.ATl2yrTb;
                    }
                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ATl3yrTb))
                    {

                        ZakatY3 = ZakatInstalments.d.ATl3yrTb;
                    }

                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ADeb1yrTb))
                    {

                        TcLiabltyY1 = ZakatInstalments.d.ADeb1yrTb;
                    }
                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ADeb2yrTb))
                    {

                        TcLiabltyY2 = ZakatInstalments.d.ADeb2yrTb;
                    }
                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ADeb3yrTb))
                    {

                        TcLiabltyY3 = ZakatInstalments.d.ADeb3yrTb;
                    }
                    if (!string.IsNullOrEmpty(ZakatInstalments.d.APr1yrTb))
                    {

                        NcFlowY1 = ZakatInstalments.d.ANi1yrTb;
                    }
                    if (!string.IsNullOrEmpty(ZakatInstalments.d.APr2yrTb))
                    {

                        NcFlowY2 = ZakatInstalments.d.ANi2yrTb;
                    }
                    if (!string.IsNullOrEmpty(ZakatInstalments.d.APr3yrTb))
                    {

                        NcFlowY3 = ZakatInstalments.d.ANi3yrTb;
                    }

                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ACrt1yrTb))
                    {

                        ProfitRatioY1 = ZakatInstalments.d.ACrt1yrTb;
                    }
                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ACrt2yrTb))
                    {

                        ProfitRatioY2 = ZakatInstalments.d.ACrt2yrTb;
                    }
                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ACrt3yrTb))
                    {

                        ProfitRatioY3 = ZakatInstalments.d.ACrt3yrTb;
                    }

                    if (!string.IsNullOrEmpty(ZakatInstalments.d.APc1yrTb))
                    {

                        CashRatioY1 = ZakatInstalments.d.APc1yrTb;
                    }
                    if (!string.IsNullOrEmpty(ZakatInstalments.d.APc2yrTb))
                    {

                        CashRatioY2 = ZakatInstalments.d.APc2yrTb;
                    }
                    if (!string.IsNullOrEmpty(ZakatInstalments.d.APc3yrTb))
                    {

                        CashRatioY3 = ZakatInstalments.d.APc3yrTb;
                    }

                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ACr1yrTb))
                    {

                        RevenueY1 = ZakatInstalments.d.ACr1yrTb;
                    }
                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ACr2yrTb))
                    {

                        RevenueY2 = ZakatInstalments.d.ACr2yrTb;
                    }
                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ACr3yrTb))
                    {

                        RevenueY3 = ZakatInstalments.d.ACr3yrTb;
                    }

                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ATa3yrTb))
                    {

                        TcAssetsY1 = ZakatInstalments.d.ATa1yrTb;
                    }
                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ATa3yrTb))
                    {

                        TcAssetsY2 = ZakatInstalments.d.ATa2yrTb;
                    }
                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ATa3yrTb))
                    {

                        TcAssetsY3 = ZakatInstalments.d.ATa3yrTb;
                    }


                    if (!string.IsNullOrEmpty(ZakatInstalments.d.ANoOfInstTp))
                    {
                        NoOfInstalments = Convert.ToInt32(ZakatInstalments.d.ANoOfInstTp);
                        NumberOFInstalmentSliderValue = Convert.ToDouble(ZakatInstalments.d.ANoOfInstTp);
                    }

                    if (!string.IsNullOrEmpty(ZakatInstalments.d.AHoldFinStat))
                    {
                        if (ZakatInstalments.d.AHoldFinStat == "1")
                        {

                            IsFinsancialStatementsEditable = true;
                        }
                        else if (ZakatInstalments.d.AHoldFinStat == "2")
                        {

                            IsFinsancialStatementsEditable = false;
                        }
                        else
                        {

                            IsFinsancialStatementsEditable = true;
                        }


                    }

                    if (!string.IsNullOrEmpty(ZakatInstalments.d.APaymentFreq))
                    {
                        SelectedFrequencyType = ZakatInstalments.d.APaymentFreq;
                    }



                    MessagingCenter.Send<object, bool>(this, "InvoiceBillsLoaded", true);



                }


            }
            catch (GAZTVATRegistrationInProcessException ex)
            {

                IsLoading = false;
                await ShowDialog(ex.Message);
                _navigationService.GoBack();

            }
            catch (Exception)
            {
                IsLoading = false;
                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                _navigationService.GoBack();
            }
        }


        #endregion


        #region OnPageLoad

        public async Task OnPageLoad()
        {
            try
            {
                IsLoading = true;
                ZakatInstalments = null;
                if (App.selectedZakatItem != "")
                {

                    ZakatInstalments = await OldZakatInstallmentWebServiceManager.GAZTGetOldZakatRequestDisplayData(App.selectedZakatItem, "IP017");

                }
                else
                {

                    ZakatInstalments = await OldZakatInstallmentWebServiceManager.GAZTGetOldZakatRequestDisplayData(App.selectedZakatItem, "");

                }

                if (ZakatInstalments != null)
                {

                    if (IsZakat)
                    {
                        if (App.selectedZakatItem != "")
                        {
                            await MopupService.Instance.PushAsync(new InstructionsBottomPopUpView(instructionString: AppResources.OldZakatInstructions, checkBoxString: AppResources.ZakatInstructionsCheckBoxDesc, continueString: AppResources.ZakatInstalmetPlanTitle, isEditable: true,
       _dialogType: InstructionsBottomPopUpViewModel.DialogType
           .Instructions));

                        }
                        else
                        {

                            await MopupService.Instance.PushAsync(new InstructionsBottomPopUpView(instructionString: AppResources.OldZakatInstructions, checkBoxString: AppResources.ZakatInstructionsCheckBoxDesc, continueString: AppResources.ZakatInstalmetPlanTitle,
          _dialogType: InstructionsBottomPopUpViewModel.DialogType
              .Instructions));
                        }



                    }
                    else
                    {

                        if (App.selectedZakatItem != "")
                        {
                            await MopupService.Instance.PushAsync(new InstructionsBottomPopUpView(instructionString: AppResources.OldZakatInstructions, checkBoxString: AppResources.ZakatInstructionsCheckBoxDesc, continueString: AppResources.ZakatInstalmetSelectTypeIncomeTax, isEditable: true,
       _dialogType: InstructionsBottomPopUpViewModel.DialogType
           .Instructions));

                        }
                        else
                        {

                            await MopupService.Instance.PushAsync(new InstructionsBottomPopUpView(instructionString: AppResources.OldZakatInstructions, checkBoxString: AppResources.ZakatInstructionsCheckBoxDesc, continueString: AppResources.ZakatInstalmetSelectTypeIncomeTax,
           _dialogType: InstructionsBottomPopUpViewModel.DialogType
               .Instructions));
                        }



                    }

                    if (ZakatInstalments.d.AInstReqReason != null && App.selectedZakatItem != "")
                    {

                        MessagingCenter.Send<object, string>(this, "SelectedReason", ZakatInstalments.d.AInstReqReason);
                    }

                    BindZakatBillsList();
                }
                else
                {
                    await ShowDialog(AppResources.ZZSomethingwentwrong);
                    _navigationService.GoBack();
                }
                IsLoading = false;

            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                await ShowDialog(ex.Message);
                IsLoading = false;
                _navigationService.GoBack();
            }
            catch (InternetException ex)
            {

                await ShowDialog(ex.Message);
                IsLoading = false;
                _navigationService.GoBack();
            }
            catch (Exception)
            {
                IsLoading = false;
                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                _navigationService.GoBack();
            }
        }

        private void BindZakatBillsList()
        {
            vatInstallmentBillsList();
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

        #endregion

        #region MAP_VAT_RequestObject


        public OldZakatInstalmentPlanRequest BuildRequestObject()
        {
            OldZakatInstalmentPlanRequest _postData = new OldZakatInstalmentPlanRequest();



            ZakatInstalments.d.Percentage = (DownPaymentAmount / MaxAmount * 100).ToString();


            _postData.__metadata = ZakatInstalments.d.__metadata;
            _postData.Percentage = ZakatInstalments.d.Percentage;
            _postData.UserTin = ZakatInstalments.d.UserTin;
            _postData.Euser = ZakatInstalments.d.Euser;
            _postData.Euser1 = ZakatInstalments.d.Euser1;
            _postData.Fbguid = ZakatInstalments.d.Fbguid;
            _postData.Auditorz = ZakatInstalments.d.Auditorz;
            _postData.Langz = ZakatInstalments.d.Langz;
            _postData.Taxpayerz = ZakatInstalments.d.Taxpayerz;
            _postData.Savez = ZakatInstalments.d.Savez;
            //_postData.Fbnumz = ZakatInstalments.d.Fbnumz;
            _postData.PeriodKeyz = ZakatInstalments.d.PeriodKeyz;
            _postData.Submitz = ZakatInstalments.d.Submitz;
            _postData.AAgree = ZakatInstalments.d.AAgree;
            _postData.ADwPaymentReqLetter = ZakatInstalments.d.ADwPaymentReqLetter;
            // _postData.AEffectiveCalTp = ZakatInstalments.d.AEffectiveCalTp;
            _postData.FbtypDescr = ZakatInstalments.d.FbtypDescr;
            _postData.FormGuid = ZakatInstalments.d.FormGuid;
            //_postData.ZauditorFlag = ZakatInstalments.d.ZauditorFlag;
            _postData.ANoOfInstTp = ZakatInstalments.d.ANoOfInstTp;
            _postData.ARev1yrTbFg = ZakatInstalments.d.ARev1yrTbFg;
            _postData.SavNot = ZakatInstalments.d.SavNot;
            _postData.Approvez = ZakatInstalments.d.Approvez;
            _postData.ARev2yrTbFg = ZakatInstalments.d.ARev2yrTbFg;
            _postData.ALegalEnty1 = ZakatInstalments.d.ALegalEnty1;
            _postData.ARev3yrTbFg = ZakatInstalments.d.ARev3yrTbFg;
            _postData.ANi1yrTbFg = ZakatInstalments.d.ANi1yrTbFg;
            _postData.Rejectz = ZakatInstalments.d.Rejectz;
            _postData.ANi2yrTbFg = ZakatInstalments.d.ANi2yrTbFg;
            _postData.CaseGuid = ZakatInstalments.d.CaseGuid;
            _postData.ANi3yrTbFg = ZakatInstalments.d.ANi3yrTbFg;
            _postData.AStep = ZakatInstalments.d.AStep;
            _postData.ACb1yrTbFg = ZakatInstalments.d.ACb1yrTbFg;
            _postData.ACb2yrTbFg = ZakatInstalments.d.ACb2yrTbFg;
            _postData.ALegalEnty2 = ZakatInstalments.d.ALegalEnty2;
            _postData.ACb3yrTbFg = ZakatInstalments.d.ACb3yrTbFg;
            _postData.ASi1yrTbFg = ZakatInstalments.d.ASi1yrTbFg;
            _postData.ASi2yrTbFg = ZakatInstalments.d.ASi2yrTbFg;
            _postData.RegIdz = ZakatInstalments.d.RegIdz;
            _postData.ASi3yrTbFg = ZakatInstalments.d.ASi3yrTbFg;
            _postData.ATa1yrTbFg = ZakatInstalments.d.ATa1yrTbFg;
            _postData.ATa2yrTbFg = ZakatInstalments.d.ATa2yrTbFg;
            _postData.ATa3yrTbFg = ZakatInstalments.d.ATa3yrTbFg;
            _postData.Fbnum = ZakatInstalments.d.Fbnum;
            _postData.ATl1yrTbFg = ZakatInstalments.d.ATl1yrTbFg;
            //_postData.ATin = ZakatInstalments.d.ATin;
            _postData.ATl2yrTbFg = ZakatInstalments.d.ATl2yrTbFg;
            _postData.ATaxpayerNm = ZakatInstalments.d.ATaxpayerNm;
            _postData.ATl3yrTbFg = ZakatInstalments.d.ATl3yrTbFg;
            _postData.ADeb1yrTbFg = ZakatInstalments.d.ADeb1yrTbFg;
            _postData.ATelNo = ZakatInstalments.d.ATelNo;
            _postData.ADeb2yrTbFg = ZakatInstalments.d.ADeb2yrTbFg;
            _postData.AMobNo = ZakatInstalments.d.AMobNo;
            _postData.ADeb3yrTbFg = ZakatInstalments.d.ADeb3yrTbFg;
            _postData.AEmail = ZakatInstalments.d.AEmail;
            _postData.ACr1yrTbFg = ZakatInstalments.d.ACr1yrTbFg;
            _postData.AInstReqFor = ZakatInstalments.d.AInstReqFor;
            _postData.ACr2yrTbFg = ZakatInstalments.d.ACr2yrTbFg;
            _postData.AInstReqReason = ZakatInstalments.d.AInstReqReason;
            _postData.ABnkStat3mhChk = ZakatInstalments.d.ABnkStat3mhChk;
            _postData.ACr3yrTbFg = ZakatInstalments.d.ACr3yrTbFg;
            _postData.AFinStat3yrChk = ZakatInstalments.d.AFinStat3yrChk;
            _postData.ARe1yrTbFg = ZakatInstalments.d.ARe1yrTbFg;
            _postData.AOtherDocChk = ZakatInstalments.d.AOtherDocChk;
            _postData.ARe2yrTbFg = ZakatInstalments.d.ARe2yrTbFg;
            _postData.AHoldFinStat = ZakatInstalments.d.AHoldFinStat;
            _postData.ARe3yrTbFg = ZakatInstalments.d.ARe3yrTbFg;
            _postData.ADpAmtFg = ZakatInstalments.d.ADpAmtFg;
            _postData.AItTb = ZakatInstalments.d.AItTb;
            _postData.AOneYrTb = ZakatInstalments.d.AOneYrTb;
            _postData.ATwoYrTb = ZakatInstalments.d.ATwoYrTb;
            _postData.AThreeYrTb = ZakatInstalments.d.AThreeYrTb;
            _postData.ADpAmt = ZakatInstalments.d.ADpAmt;
            _postData.APlanDurNo = ZakatInstalments.d.APlanDurNo;
            _postData.APlanDurPeri = ZakatInstalments.d.APlanDurPeri;
            _postData.APaymentFreq = ZakatInstalments.d.APaymentFreq;
            _postData.ACoPlanDurPeri = ZakatInstalments.d.ACoPlanDurPeri;
            _postData.ACoPaymentFreq = ZakatInstalments.d.ACoPaymentFreq;
            _postData.ADpRequ = ZakatInstalments.d.ADpRequ;
            _postData.ADpDocNo = ZakatInstalments.d.ADpDocNo;
            _postData.ADpPer = ZakatInstalments.d.ADpPer;
            _postData.ACoDpAmt = ZakatInstalments.d.ACoDpAmt;
            _postData.ADpRecAmt = ZakatInstalments.d.ADpRecAmt;
            _postData.AAppInstAmt = ZakatInstalments.d.AAppInstAmt;
            _postData.AInstDpAmt = ZakatInstalments.d.AInstDpAmt;
            _postData.ABalAmt = ZakatInstalments.d.ABalAmt;
            _postData.ACoBoRev = ZakatInstalments.d.ACoBoRev;
            _postData.ACmBoNm = ZakatInstalments.d.ACoBoNm;
            _postData.ACmBoRd = ZakatInstalments.d.ACoBoRd;
            _postData.ACoHoNm = ZakatInstalments.d.ACoHoNm;
            _postData.ACmBoRev = ZakatInstalments.d.ACmBoRev;
            _postData.ACoHoRd = ZakatInstalments.d.ACoHoRd;
            _postData.ACmHoRev = ZakatInstalments.d.ACmHoRev;
            _postData.ACmHoNm = ZakatInstalments.d.ACmHoNm;
            _postData.ACmHoRd = ZakatInstalments.d.ACmHoRd;
            _postData.AMofApprChk = ZakatInstalments.d.AMofApprChk;
            _postData.AOtherSuppDocChk = ZakatInstalments.d.AOtherSuppDocChk;
            _postData.ARejReason = ZakatInstalments.d.ARejReason;
            _postData.ACoPlanDurNo = ZakatInstalments.d.ACoPlanDurNo;
            _postData.ARev1yrTb = ZakatInstalments.d.ARev1yrTb;
            _postData.ARev2yrTb = ZakatInstalments.d.ARev2yrTb;
            _postData.ARev3yrTb = ZakatInstalments.d.ARev3yrTb;
            _postData.ANi1yrTb = ZakatInstalments.d.ANi1yrTb;
            _postData.ANi2yrTb = ZakatInstalments.d.ANi2yrTb;
            _postData.ANi3yrTb = ZakatInstalments.d.ANi3yrTb;
            _postData.ACb1yrTb = ZakatInstalments.d.ACb1yrTb;
            _postData.ACb2yrTb = ZakatInstalments.d.ACb2yrTb;
            _postData.ACb3yrTb = ZakatInstalments.d.ACb3yrTb;
            _postData.ASi1yrTb = ZakatInstalments.d.ASi1yrTb;
            _postData.ASi2yrTb = ZakatInstalments.d.ASi2yrTb;
            _postData.ASi3yrTb = ZakatInstalments.d.ASi3yrTb;
            _postData.ATa1yrTb = ZakatInstalments.d.ATa1yrTb;
            _postData.ATa2yrTb = ZakatInstalments.d.ATa2yrTb;
            _postData.ATa3yrTb = ZakatInstalments.d.ATa3yrTb;
            _postData.ATl1yrTb = ZakatInstalments.d.ATl1yrTb;
            _postData.ATl2yrTb = ZakatInstalments.d.ATl2yrTb;
            _postData.ATl3yrTb = ZakatInstalments.d.ATl3yrTb;
            _postData.ADeb1yrTb = ZakatInstalments.d.ADeb1yrTb;
            _postData.ADeb2yrTb = ZakatInstalments.d.ADeb2yrTb;
            _postData.ADeb3yrTb = ZakatInstalments.d.ADeb3yrTb;
            _postData.ACr1yrTb = ZakatInstalments.d.ACr1yrTb;
            _postData.ACr2yrTb = ZakatInstalments.d.ACr2yrTb;
            _postData.ACr3yrTb = ZakatInstalments.d.ACr3yrTb;
            _postData.ARe1yrTb = ZakatInstalments.d.ARe1yrTb;
            _postData.ARe2yrTb = ZakatInstalments.d.ARe2yrTb;
            _postData.ARe3yrTb = ZakatInstalments.d.ARe3yrTb;
            _postData.APr1yrTb = ZakatInstalments.d.APr1yrTb;
            _postData.APr2yrTb = ZakatInstalments.d.APr2yrTb;
            _postData.APr3yrTb = ZakatInstalments.d.APr3yrTb;
            _postData.ACrt1yrTb = ZakatInstalments.d.ACrt1yrTb;
            _postData.ACrt2yrTb = ZakatInstalments.d.ACrt2yrTb;
            _postData.ACrt3yrTb = ZakatInstalments.d.ACrt3yrTb;
            _postData.APc1yrTb = ZakatInstalments.d.APc1yrTb;
            _postData.APc2yrTb = ZakatInstalments.d.APc2yrTb;
            _postData.APc3yrTb = ZakatInstalments.d.APc3yrTb;
            _postData.APerAmtRd = ZakatInstalments.d.APerAmtRd;
            _postData.ADpRequDrp = ZakatInstalments.d.ADpRequDrp;



            //_postData.APer = ZakatInstalments.d.APer;
            _postData.AFormStatus = ZakatInstalments.d.AFormStatus;
            _postData.ADownLetterChk = ZakatInstalments.d.ADownLetterChk;
            _postData.ADownYear = ZakatInstalments.d.ADownYear;
            _postData.ADownMonth = ZakatInstalments.d.ADownMonth;
            _postData.ADownToYear = ZakatInstalments.d.ADownToYear;
            _postData.ADownToMonth = ZakatInstalments.d.ADownToMonth;
            _postData.ABranch = ZakatInstalments.d.ABranch;
            _postData.ASaudiShare = ZakatInstalments.d.ASaudiShare;
            _postData.ANonsaudiShare = ZakatInstalments.d.ANonsaudiShare;
            _postData.AMainAct = ZakatInstalments.d.AMainAct;
            _postData.AMainActDesc = ZakatInstalments.d.AMainActDesc;
            _postData.APoBox = ZakatInstalments.d.APoBox;
            _postData.APostalCode = ZakatInstalments.d.APostalCode;
            _postData.AFaxNo = ZakatInstalments.d.AFaxNo;
            _postData.ABuilding = ZakatInstalments.d.ABuilding;
            _postData.AStreet = ZakatInstalments.d.AStreet;
            _postData.ADistrict = ZakatInstalments.d.ADistrict;
            _postData.ACity = ZakatInstalments.d.ACity;
            _postData.ALvError = ZakatInstalments.d.ALvError;
            _postData.ATotalAmt = ZakatInstalments.d.ATotalAmt;
            _postData.Status = ZakatInstalments.d.Status;
            _postData.ACoHoRev = ZakatInstalments.d.ACoHoRev;
            _postData.ACoBoNm = ZakatInstalments.d.ACoBoNm;
            _postData.ACoBoRd = ZakatInstalments.d.ACoBoRd;





            if (ZakatInstalments.d.Off_notesSet == null)
            {

                _postData.Off_notesSet = new ZakatNotesSet[0];
            }
            else
            {
                _postData.Off_notesSet = ZakatInstalments.d.Off_notesSet.ToArray();
            }

            _postData.z_invoiceSet = new OldZInvoiceSet[0];
            _postData.z_proposedinsSet = new OldZProposedinsSet[0];



            if (ZakatInstalments.d.AttDetSet == null)
            {

                _postData.AttDetSet = new OldAttDetSet[0];
            }
            else
            {
                _postData.AttDetSet = new OldAttDetSet[0];



            }

            if (ZakatInstalments.d.Z_INVOICE_UI5Set == null)
            {

                _postData.Z_INVOICE_UI5Set = new OldResults3[0];

            }
            else
            {


                if (ZakatInstalments.d.Z_INVOICE_UI5Set.Count != 0)
                {



                    for (int i = 0; i < ZakatInstalments.d.Z_INVOICE_UI5Set.Count; i++)
                    {

                        var dataItem = ZakatInstalments.d.Z_INVOICE_UI5Set[i];


                        if (selectedList.ToList().Exists(item => item.AIvNoTb == dataItem.AIvNoTb))
                        {

                            ZakatInstalments.d.Z_INVOICE_UI5Set[i].AIvTb = "1";
                        }
                        else
                        {

                            ZakatInstalments.d.Z_INVOICE_UI5Set[i].AIvTb = "2";

                        }





                        string apiDate = ZakatInstalments.d.Z_INVOICE_UI5Set[i].ADueDtTb;
                        if (apiDate != null && !apiDate.Contains("Date"))
                        {

                            DateTime dt = Convert.ToDateTime(ZakatInstalments.d.Z_INVOICE_UI5Set[i].ADueDtTb);
                            
                            ZakatInstalments.d.Z_INVOICE_UI5Set[i].ADueDtTb = dt.ToString("yyyy-MM-ddTHH:mm:ss");
                        }

                        ZakatInstalments.d.Z_INVOICE_UI5Set[i].AAmtTb = ZakatInstalments.d.Z_INVOICE_UI5Set[i].AIvAmtTb;



                        if (ZakatInstalments.d.Z_INVOICE_UI5Set[i].AIvAbtyp.Equals(AppResources.ZakatInstalmetSelectTypeIncomeTax))
                        {

                            ZakatInstalments.d.Z_INVOICE_UI5Set[i].AIvAbtyp = "ITAX";
                        }
                        else if (ZakatInstalments.d.Z_INVOICE_UI5Set[i].AIvAbtyp.Equals(AppResources.FORM5Zakat))
                        {

                            ZakatInstalments.d.Z_INVOICE_UI5Set[i].AIvAbtyp = "ZAKT";
                        }


                    }

                    _postData.Z_INVOICE_UI5Set = ZakatInstalments.d.Z_INVOICE_UI5Set.ToArray();


                }

            }
            return _postData;
        }

        #endregion


        #region ApiIntegration




        public async Task<OldZakatRequestDisplayModel> SubmitClicked()
        {
            OldZakatRequestDisplayModel response = new OldZakatRequestDisplayModel();
            OldZakatInstalmentPlanRequest request = new OldZakatInstalmentPlanRequest();

            try
            {
                IsLoading = true;


                if (BankStatementsAttachmentsListViewData != null && BankStatementsAttachmentsListViewData.Count > 0)
                {

                    ZakatInstalments.d.ABnkStat3mhChk = "1";
                }


                if (IsFinsancialStatementsEditable)
                {

                    ZakatInstalments.d.AFinStat3yrChk = "1";

                }
                else
                {
                    ZakatInstalments.d.AFinStat3yrChk = "2";

                }



                double totalAmount = double.Parse(TotalAmountSAR.Replace(" SAR", ""));
                if (totalAmount != 0)
                {
                    ZakatInstalments.d.ATotalAmt = totalAmount.ToString();
                }

                double dueAmount = double.Parse(DownPaymentAmount.ToString().Replace(" SAR", ""));
                if (dueAmount != 0)
                {
                    ZakatInstalments.d.ADpAmt = dueAmount.ToString();
                    ZakatInstalments.d.ADpAmtFg = "1";
                }


                ZakatInstalments.d.ACb1yrTbFg = "1";
                ZakatInstalments.d.ACb2yrTbFg = "1";
                ZakatInstalments.d.ACb3yrTbFg = "1";
                ZakatInstalments.d.ACr1yrTbFg = "1";
                ZakatInstalments.d.ACr2yrTbFg = "1";
                ZakatInstalments.d.ACr3yrTbFg = "1";
                ZakatInstalments.d.ARe1yrTbFg = "1";
                ZakatInstalments.d.ARe2yrTbFg = "1";
                ZakatInstalments.d.ARe3yrTbFg = "1";

                ZakatInstalments.d.ARev1yrTbFg = "1";
                ZakatInstalments.d.ARev2yrTbFg = "1";
                ZakatInstalments.d.ARev3yrTbFg = "1";

                ZakatInstalments.d.ASi1yrTbFg = "1";
                ZakatInstalments.d.ASi2yrTbFg = "1";
                ZakatInstalments.d.ASi3yrTbFg = "1";

                ZakatInstalments.d.ATa1yrTbFg = "1";
                ZakatInstalments.d.ATa2yrTbFg = "1";
                ZakatInstalments.d.ATa3yrTbFg = "1";

                ZakatInstalments.d.ATl1yrTbFg = "1";
                ZakatInstalments.d.ATl2yrTbFg = "1";
                ZakatInstalments.d.ATl3yrTbFg = "1";

                ZakatInstalments.d.ANi1yrTbFg = "1";
                ZakatInstalments.d.ANi2yrTbFg = "1";
                ZakatInstalments.d.ANi3yrTbFg = "1";


                ZakatInstalments.d.ADeb1yrTbFg = "1";
                ZakatInstalments.d.ADeb2yrTbFg = "1";
                ZakatInstalments.d.ADeb3yrTbFg = "1";

                request = BuildRequestObject();
                response = await OldZakatInstallmentWebServiceManager.SaveOldZakatInstalmentData(request);
                PopToRootPage();
                if (response != null)
                {
                    try
                    {
                        IsLoading = false;
                        return response;

                    }
                    catch (Exception)
                    {
                        IsLoading = false;
                        return null;

                    }
                }
                IsLoading = false;
                return response;
            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                IsLoading = false;
                await ShowDialog(ex.Message);
                return response;
            }

            catch (Exception)
            {
                IsLoading = false;
                return null;
            }

        }

        #endregion
    }
}
