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
using ZATCAMAUI.Views.NewDesign.VATDeclarationPages;
using ZATCAMAUI.Views.NewDesign.ZakatInstalmentPlan;
using static ZATCAMAUI.Models.ZakatInstalationModels.ZAKATRequestPlanModel;
using AttachSet = ZATCAMAUI.Models.ZakatInstalationModels.AttachSet;
using NotesSet = ZATCAMAUI.Models.ZakatInstalationModels.NotesSet;
using ZATCAMAUI.Core.Interfaces;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel
{
    
    public class ZakatInstalmentPlanViewModel : BaseViewModel
    {
        #region Variable
        bool _bankStatementsAttachment = true;
        private bool _isNoDataLableVisible = false;

        int noOfInstalments = 1;
        double minInstalments = 1;
        double maxInstalments = 36;
        double downPaymentAmount = 400.00;
        double periodicInstalment = 0.0;
        double minAmount = 400.0;
        double maxAmount = 2000000.0;
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
        private double _numberOFInstalmentSliderValue = 0;
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
        private string _selectedFrequencyType = "01";
        private string _selectedFrequencyName = AppResources.ZakatInstalmetMonthly;

        public bool MarkComplete { get; private set; } = false;
        public int MaxIndex { get; private set; } = 6;
        private List<CorrespondenceFiltersModel> _subCorresFilterZakat;
        private List<CorrespondanceModel> _subListZAKATCorrespondance = null;
        private List<CorrespondenceFiltersModel> _corresFilterZakat;
        private CorrespondenceFiltersModel _selectedFilterZakatPrev = null;
        private List<CorrespondanceModel> _listZAKATCorrespondance = null;
        private CorrespondenceFiltersModel _selectedFilterZakat = null;
        public List<ZakatInvoicesResult> selectedList = new List<ZakatInvoicesResult>();
        private List<ZakatInvoicesResult> _zakatInvoicesList;

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
        public ICommand NewAttachmentTapped { get; set; }
        public ICommand InstallmentDetailsBtnTapped { get; set; }

        public ICommand DisplayDetailsButtonTapped { get; set; }

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
                if (isNewLoading == value) return;

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
                if (_isZakat == value) return;

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
                if (_zakatTitle == value) return;

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
                if (_vATDueAmount == value) return;

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
                if (_selectedFrequencyType == value) return;

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
                if (_selectedFrequencyName == value) return;

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
                if (_vATPenalityAmount == value) return;

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
                if (_vATLiabilityAmount == value) return;

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
                if (_vATBillDueAmount == value) return;

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
                if (_isBackButtonVisible == value) return;

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
                if (_isSelectionViewEnabled == value) return;

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
                if (_selectedOutletOptionIndex == value) return;

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
                if (_isDisplayInstalmentsVisible == value) return;

                _isDisplayInstalmentsVisible = value;
                OnPropertyChanged("IsDisplayInstalmentsVisible");
            }
        }

        public List<ZakatInvoicesResult> ZakatInvoicesList
        {
            get
            {
                return _zakatInvoicesList;
            }
            set
            {
                if (_zakatInvoicesList == value) return;

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
                if (_isNoDataLableVisible == value) return;

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
                if (_isAgreementViewEnabled == value) return;

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
                if (_isOutletViewEnabled == value) return;

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
                if (_isBillsViewEnabled == value) return;

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
                if (_isVATBillsViewEnabled == value) return;

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
                if (_InstalmentPlanAgreementsVisible == value) return;

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
                if (_isAttachmentsViewEnabled == value) return;

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
                if (_isStatementViewEnabled == value) return;

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
                if (_isSummaryViewEnabled == value) return;

                _isSummaryViewEnabled = value;
                OnPropertyChanged("IsSummaryViewEnabled");
            }
        }
        private double downPaymentSliderValue = 0.0;
        public double DownPaymentSliderValue
        {
            get { return downPaymentSliderValue; }
            set
            {
                if (downPaymentSliderValue == value) return;

                downPaymentSliderValue = value;
                OnPropertyChanged("DownPaymentSliderValue");
            }
        }
        public int NoOfInstalments
        {
            set
            {
                if (noOfInstalments == value) return;

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


        public double NumberOFInstalmentSliderValue
        {
            set
            {
                if (_numberOFInstalmentSliderValue == value) return;

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
                if (minInstalments == value) return;

                if (minInstalments != value)
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
                if (maxInstalments == value) return;

                if (maxInstalments != value)
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
                if (downPaymentAmount == value) return;

                if (downPaymentAmount != value)
                {
                    downPaymentAmount = value;
                    OnPropertyChanged("DownPaymentAmount");
                }
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
                if (minInstalmentsTitle == value) return;

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
                if (minAmountTitle == value) return;

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
                if (maxAmountTitle == value) return;

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
                if (maxInstalmentsTitle == value) return;

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
                if (minAmount == value) return;

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
                if (maxAmount == value) return;

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
                if (totalAmountSAR == value) return;

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
                if (downPaymentSAR == value) return;

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
                if (periodicInstalment == value) return;

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
                if (inputData == value) return;

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
                if (_isVATAmountVisible == value) return;

                _isVATAmountVisible = value;
                OnPropertyChanged("IsVATAmountVisible");
            }
        }

        //Zakat Slection starts here


        public bool IsZakatSelected
        {
            get
            {
                return _isZakatSelected;
            }
            set
            {
                if (_isZakatSelected == value) return;

                _isZakatSelected = value;
                OnPropertyChanged("IsZakatSelected");
            }
        }
        //Custom Spinner Items starts here


        public bool IsIncomeTaxViewEnabled
        {
            get
            {
                return _isIncomeTaxViewEnabled;
            }
            set
            {
                if (_isIncomeTaxViewEnabled == value) return;

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
                if (_isSubIncomeTaxViewEnabled == value) return;

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
                if (cashBankY1 == value) return;

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
                if (cashBankY2 == value) return;

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
                if (cashBankY3 == value) return;

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
                if (cashRatioY1 == value) return;

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
                if (cashRatioY2 == value) return;

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
                if (cashRatioY3 == value) return;

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
                if (debitorsY1 == value) return;

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
                if (debitorsY2 == value) return;

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
                if (debitorsY3 == value) return;

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
                if (inventoryY1 == value) return;

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
                if (inventoryY2 == value) return;

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
                if (inventoryY3 == value) return;

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
                if (ncFlowY1 == value) return;

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
                if (ncFlowY2 == value) return;

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
                if (ncFlowY3 == value) return;

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
                if (netIncomeY1 == value) return;

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
                if (netIncomeY2 == value) return;

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
                if (netIncomeY3 == value) return;

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
                if (profitRatioY1 == value) return;

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
                if (profitRatioY2 == value) return;

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
                if (profitRatioY3 == value) return;

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
                if (revenueY1 == value) return;

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
                if (revenueY2 == value) return;

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
                if (revenueY3 == value) return;

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
                if (stiY1 == value) return;

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
                if (stiY2 == value) return;

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
                if (stiY3 == value) return;

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
                if (tcAssetsY1 == value) return;

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
                if (tcAssetsY2 == value) return;

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
                if (tcAssetsY3 == value) return;

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
                if (tcLiabltyY1 == value) return;

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
                if (tcLiabltyY2 == value) return;

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
                if (tcLiabltyY3 == value) return;

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
                if (year1 == value) return;

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
                if (year2 == value) return;

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
                if (year3 == value) return;

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
                if (zakatY1 == value) return;

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
                if (zakatY2 == value) return;

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
                if (zakatY3 == value) return;

                zakatY3 = value;
                OnPropertyChanged("ZakatY3");
            }
        }

        private bool _isPenaltyVisible = false;
        public bool IsPenaltyVisible
        {
            get { return _isPenaltyVisible; }
            set
            {
                if (_isPenaltyVisible == value) return;

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
                if (_selectedFilterZakat == value) return;

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
                if (_zakatReferanceNumber == value) return;

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
                if (_listZAKATCorrespondance == value) return;

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
                if (_txtSelectedStatusZakat == value) return;

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
                if (_currenrIndex == value) return;

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
                if (_selectedFilterZakatPrev == value) return;

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
                if (_corresFilterZakat == value) return;

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
                if (_subListZAKATCorrespondance == value) return;

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
                if (_subTxtSelectedStatusZakat == value) return;

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
                if (_statementList == value) return;

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
                if (_subCorresFilterZakat == value) return;

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
                if (_subSetSelectedIndexZakat == value) return;

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




            foreach (Attachment attachment in BankStatementsAttachmentsListViewData)
            {
                attachmentsListViewData1.Add(attachment);
            }
            foreach (Attachment attachment in FinanceAttachmentsListViewData)
            {
                attachmentsListViewData1.Add(attachment);
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
                if (_selectedOutletOption == value) return;

                _selectedOutletOption = value;
                //SelectedOutletOptionIndex = OutletDecisionOptions.IndexOf(_selectedOutletOption as TINDeregistrationModel);
                OnPropertyChanged("SelectedOutletOption");
            }
        }


        private ZakatInstalmentPlanResponse _zakatInstalments;
        public ZakatInstalmentPlanResponse ZakatInstalments
        {
            get
            {
                return _zakatInstalments;
            }
            set
            {
                if (_zakatInstalments == value) return;

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
                if (_isInstrunctionChecked == value) return;

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
                if (_isVatTermsChecked == value) return;

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
                if (_isInitialDraft == value) return;

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
                if (_isDeclarationChecked == value) return;

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
                if (_isContinueButtonEnable == value) return;

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
                if (_continueButtonnBackroundColor == value) return;

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
                        if (ZakatInvoicesList[i].Abtyp.Equals("ITAX"))
                        {
                            ZakatInvoicesList[i].Abtyp = AppResources.ZakatInstalmetSelectTypeIncomeTax;
                        }
                        else if (ZakatInvoicesList[i].Abtyp.Equals("ZAKT"))
                        {
                            ZakatInvoicesList[i].Abtyp = AppResources.FORM5Zakat;
                        }
                    }
                    EnableVATBillView();
                    break;
                case (int)PagesEnum.IsDisplayInstalmentsVisible:
                    EnableAgreementView();
                    break;
                case (int)PagesEnum.ZakatAttachmentsView:

                    if (ZakatInstalments.d.AccMethod == "A")
                    {

                        EnableInstalmentsScheduleView();

                    }
                    else
                    {
                        EnableAgreementView();
                    }
                    break;
                case (int)PagesEnum.InstalmentPlanAgreementsVisible:
                    EnableDisplayInstalmentsView();

                    //EnableAgreementView();
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
            SelectedFrequencyType = "01";
            IsZakatSelected = true;
            DownPaymentSliderValue = 1;
            IsInitialDraft = false;
            NoOfInstalments = 1;
            minInstalments = 1;
            maxInstalments = 36;
            MinInstalmentsTitle = AppResources.ZakatMin + " " + 1;
            MaxInstalmentsTitle = AppResources.ZakatMax + " " + 36;
            DownPaymentAmount = 0.0;
            PeriodicInstalment = 0.0;
            MinAmount = 0.0;
            MinAmountTitle = AppResources.ZakatMin + " 0.0";
            MaxAmountTitle = AppResources.ZakatMax + " 0.0";
            // maxAmount = 0.0;
            InputData = "";
            TotalAmountSAR = "0.00 SAR";
            VATDueAmount = "0.00";
            VATPenalityAmount = "0.00";
            VATBillDueAmount = "0.00 SAR";
            VATLiabilityAmount = "0.00 SAR";
            CurrentIndex = 1;
            Year1 = "";
            Year2 = "";
            Year3 = "";
            BankStatementsAttachmentsListViewData = null;
            FinanceAttachmentsListViewData = null;
            isSubmitClicked = false;
            isDraftClicked = false;


            IDTypeDictionary = new Dictionary<string, string>
        {
            {"","oBlak"},
            {AppResources.ZakatFinancialCrisis,"00"},
            {AppResources.ZakatDisputeInFavorOfGAZT,"01"},
            {AppResources.ZakatOtherReason,"02"},
        };

            EnableDeclarationContinue();

            AddFrequencyOptions();
            AddOutletDecisionOptions();

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
                if (_successMessage == value) return;
                _successMessage = value;
                OnPropertyChanged("SuccessMessage");
            }
        }
        public ZakatInstalmentPlanViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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

            VATInstalationClicked = new Command(VATInstalationTapped);
            ReasonContinueBtnTapped = new Command(ReasonContinueBtnClicked);

            AggrementContinueBtnTapped = new Command(AggrementContinueBtnClicked);
            BillContinueBtnTapped = new Command(BillContinueBtnClicked);
            AttachmentsContinueBtnTapped = new Command(AttachmentsContinueBtnClicked);
            StatementsContinueBtnTapped = new Command(StatementsContinueBtnClicked);
            SummaryContinueBtnTapped = new Command(SummaryContinueBtnClicked);
            SummaryInstallmentDetailsBtnTapped = new Command(SummaryInstallmentDetailsBtnClicked);
            DisplayDetailsButtonTapped = new Command(DisplayDetailsButtonClicked);
            OnZakatInstalmentReasonTapped = new Command(OnZakatInstalmentReasonClicked);
            NewAttachmentTapped = new Command(NewAttachmentClicked);
            BankStatementsAttachmentTapped = new Command(BankStatementsAttachmentClicked);
            FinanceAttachmentTapped = new Command(FinanceAttachmentClicked);

            InstallmentDetailsBtnTapped = new Command(async () =>
            {

                var totalamount = TotalAmountSAR.Replace(" SAR", "").Replace(",", "");
                var instalmentamount = VATBillDueAmount.Replace(" SAR", "").Replace(",", "");
                ZakatInstalments.d.DpAmt = DownPaymentAmount.ToString();
                ZakatInstalments.d.TotAmt = instalmentamount.ToString();
                ZakatInstalments.d.Operation = "51";
                ZakatInstalments.d.StepNumber = "03";
                ZakatInstalments.d.PlanDur = noOfInstalments.ToString();
                ZakatInstalments.d.PymntFreq = SelectedFrequencyType;


                ZakatInstalments.d.insPlan_OffSet = new InsPlanOffSet();
                ZakatInstalments.d.NotesSet = new NotesSetResult();
                ZakatInstalments.d.insPlanSet = new InsPlanSet();
                ZakatInstalments.d.retmsgSet = new RetmsgSet();
                ZakatInstalments.d.FnDtlSet = new FnDtlSet();
                ZakatInstalments.d.AttachSet = new AttachSet();

                if (IsInitialDraft || App.selectedZakatItem != "")
                {

                }
                else
                {
                    //ZakatInstalments.d.Fbnum = "";
                    //ZakatInstalments.d.Status = "E0001";
                }

                await Task.Run(async () =>
                {
                    ZakatInstalments = await SubmitClicked();
                });

                if (ZakatInstalments.d != null)
                {

                    //EnableStatementsView();
                    VATPenalityAmount = string.Format("{0:N2}", ZakatInstalments.d.PenlAmt) + " SAR";
                    EnableDisplayInstalmentsView();
                    BindStatementsView();
                }



                //                EnableInstalmentsScheduleView();
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
                if (_idType == value) return;

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
                if (_ListOfActionButtonsApplicable == value) return;

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
            catch (GAZTUnlockAccountException)
            {

            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        private async void ShowMoreOptionsPopUp()
        {
            try
            {
                if (ListOfActionButtonsApplicable != null && ListOfActionButtonsApplicable.Count() != 0)
                {
                    string action = await Application.Current.MainPage.DisplayActionSheet("", AppResources.ZZCancel, null, ListOfActionButtonsApplicable.ToArray());
                    if (App.IsArabic)
                    {
                        ArButtons buttonId = ArButtons.None;
                        if (!string.IsNullOrEmpty(action))
                        {
                            action = action.Replace(" ", "");
                        }
                        Enum.TryParse(action, out buttonId);
                        switch (buttonId)
                        {

                            case ArButtons.إلغاء:
                                VATSetReturnVoidAsync();
                                break;
                            case ArButtons.حفظكمسودة:

                                OnSaveDraftClicked();



                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        Buttons buttonId = Buttons.None;
                        if (!string.IsNullOrEmpty(action))
                        {
                            action = action.Replace(" ", "");
                        }
                        Enum.TryParse(action, out buttonId);
                        switch (buttonId)
                        {

                            case Buttons.Void:
                                VATSetReturnVoidAsync();
                                break;

                            case Buttons.SaveasDraft:

                                OnSaveDraftClicked();

                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception)
            {


            }
        }

        public void setMoreOptioButtons()
        {
            var listOfActionButtonsApplicable = new List<string>();
            if (App.selectedZakatItem != "")
            {

                listOfActionButtonsApplicable.Add(AppResources.ZZVoid);
            }


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

        public void BindVATSelectionView()
        {

            if (ZakatInstalments != null)
            {
            }
        }


        public void BindStatementsView()
        {

            if (ZakatInstalments.d.insPlanSet != null)
            {
                var statementList = ZakatInstalments.d.insPlanSet.results;

                for (int i = 0; i < statementList.Length; i++)
                {
                    DateTime dateStart = new DateTime();
                    CultureInfo cultureInfo = new CultureInfo("ar-SA");
                    string apiDate = @"""" + statementList[i].DueDt + @"""";
                    dateStart = JsonConvert.DeserializeObject<DateTime>(apiDate);

                    GregorianCalendar hjCalendar = new GregorianCalendar();
                    int year = hjCalendar.GetYear(dateStart);
                    int month = hjCalendar.GetMonth(dateStart);
                    int day = hjCalendar.GetDayOfMonth(dateStart);

                    string dateStr = string.Format("{0:00}/{1}/{2}", day, month, year);
                    statementList[i].DueDt = dateStr;

                    string dt1 = string.Empty;
                    string[] dts = null;
                    dts = statementList[i].DueDt.Split('/');
                    dt1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];
                    statementList[i].DueDt = dt1;
                }
                StatementList = statementList;
            }




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



            if (NetIncomeY1.Length > 0 && double.Parse(NetIncomeY1) != 0 && RevenueY1.Length > 0 && double.Parse(RevenueY1) != 0)
            {
                ProfitRatioY1 = (double.Parse(NetIncomeY1) / double.Parse(RevenueY1)).ToString("0.00");
            }
            else
            {
                ProfitRatioY1 = "0.00";
            }





            if (TcLiabltyY1.Length > 0 && double.Parse(TcLiabltyY1) != 0)
            {
                double a = 0.0; double b = 0.0;
                if (CashBankY1.Length > 0 && double.Parse(CashBankY1) != 0 && StiY1.Length > 0 && double.Parse(StiY1) != 0)
                {
                    a = double.Parse(CashBankY1);
                    b = double.Parse(StiY1);
                }
                else if (CashBankY1.Length > 0 && double.Parse(CashBankY1) != 0)
                {
                    a = double.Parse(CashBankY1);
                    b = 0;
                }
                else if (StiY1.Length > 0 && double.Parse(StiY1) != 0)
                {
                    a = 0;
                    b = double.Parse(StiY1);
                }



                double c = a + b;
                CashRatioY1 = (c / double.Parse(TcLiabltyY1)).ToString("0.00");
            }
            else
            {
                CashRatioY1 = "0.00";
            }
        }


        public void calculateYear2Data()
        {
            if (NetIncomeY2.Length > 0 && double.Parse(NetIncomeY2) != 0 && RevenueY2.Length > 0 && double.Parse(RevenueY2) != 0)
            {
                ProfitRatioY2 = (double.Parse(NetIncomeY2) / double.Parse(RevenueY2)).ToString("0.00");
            }
            else
            {
                ProfitRatioY2 = "0.00";
            }




            if (TcLiabltyY2.Length > 0 && double.Parse(TcLiabltyY2) != 0)
            {
                double a = 0.0; double b = 0.0;
                if (CashBankY2.Length > 0 && double.Parse(CashBankY2) != 0 && StiY2.Length > 0 && double.Parse(StiY2) != 0)
                {
                    a = double.Parse(CashBankY2);
                    b = double.Parse(StiY2);
                }
                else if (CashBankY2.Length > 0 && double.Parse(CashBankY2) != 0)
                {
                    a = double.Parse(CashBankY2);
                    b = 0;
                }
                else if (StiY2.Length > 0 && double.Parse(StiY2) != 0)
                {
                    a = 0;
                    b = double.Parse(StiY2);
                }


                double c = a + b;
                CashRatioY2 = (c / double.Parse(TcLiabltyY2)).ToString("0.00");
            }
            else
            {
                CashRatioY2 = "0.00";
            }





        }



        public void calculateYear3Data()
        {
            if (NetIncomeY3.Length > 0 && double.Parse(NetIncomeY3) != 0 && RevenueY3.Length > 0 && double.Parse(RevenueY3) != 0)
            {
                ProfitRatioY3 = (double.Parse(NetIncomeY3) / double.Parse(RevenueY3)).ToString("0.00");
            }
            else
            {
                ProfitRatioY3 = "0.00";
            }

            if (TcLiabltyY3.Length > 0 && double.Parse(TcLiabltyY3) != 0)
            {
                double a = 0.0; double b = 0.0;
                if (CashBankY3.Length > 0 && double.Parse(CashBankY3) != 0 && StiY3.Length > 0 && double.Parse(StiY3) != 0)
                {
                    a = double.Parse(CashBankY3);
                    b = double.Parse(StiY3);
                }
                else if (CashBankY3.Length > 0 && double.Parse(CashBankY3) != 0)
                {
                    a = double.Parse(CashBankY3);
                    b = 0;
                }
                else if (StiY3.Length > 0 && double.Parse(StiY3) != 0)
                {
                    a = 0;
                    b = double.Parse(StiY3);
                }

                double c = a + b;
                CashRatioY3 = (c / double.Parse(TcLiabltyY3)).ToString("0.00");
            }
            else
            {
                CashRatioY3 = "0.00";
            }


        }

        public async Task EnableSucessScreenAsync()
        {

            Preferences.Set("IsFromRevok", false);
            Preferences.Set("RevokeRef", "");
            await Application.Current.MainPage.DisplayAlert(AppResources.Information, AppResources.ZakatInstalmentPlanSubmittedPopUpMsg + " " + ZakatInstalments.d.DpAmt + " " + AppResources.FORM5SAR, AppResources.CRContinue);
            await Application.Current.MainPage.Navigation.PushAsync(new ZakatInstalmentPlanSuccessPage());

        }

        public void EnableDeclarationContinue()
        {
            if (BankStatementsAttachmentsListViewData != null && BankStatementsAttachmentsListViewData.Count > 0 && FinanceAttachmentsListViewData != null && FinanceAttachmentsListViewData.Count > 0)
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
                if (_isDeclarationEnabled == value) return;
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

        public async void VATInstalationTapped()
        {
            try
            {
                if (IsVatTermsChecked)
                {
                    await MopupService.Instance.PopAsync();
                }
                else
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.BPInstructionsAndConditionsAlert, AppResources.Information);
                    });
                    // await showAlert("Please accept VAT Instructions and Conditions to continue");
                }
                // _navigationService.NavigateTo(App.ZakatInstalmentPlanPageView);
            }
            catch (GAZTUnlockAccountException ex)
            {


            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }
        public async void ReasonContinueBtnClicked()
        {
            try
            {
                if (IsZakatSelected || IsIncomeTaxViewEnabled || IsVATAmountVisible)
                {
                    if (IsZakat)
                    {
                        ZakatInstalments.d.InstReqFor = "01";

                    }
                    else
                    {
                        ZakatInstalments.d.InstReqFor = "02";
                    }



                    if (IDType == IDTypeDictionary[AppResources.ZakatFinancialCrisis])
                    {
                        ZakatInstalments.d.InstReqReason = "01";
                    }
                    else if (IDType == IDTypeDictionary[AppResources.ZakatDisputeInFavorOfGAZT])
                    {
                        ZakatInstalments.d.InstReqReason = "02";
                    }
                    else if (IDType == IDTypeDictionary[AppResources.ZakatOtherReason])
                    {
                        ZakatInstalments.d.InstReqReason = "03";
                    }
                    else
                    {
                        ZakatInstalments.d.InstReqReason = "01";
                    }


                    await GetZaktaInvoiceList();
                }
                else
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.ZakatInstalmentPleaseChooseOneReason, AppResources.Information);
                    });
                }

            }
            catch (GAZTUnlockAccountException)
            {

            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public async void showDialog(string msg)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await _dialogService.ShowMessage(msg, AppResources.Information);
            });
        }

        public void BillContinueBtnClicked()
        {
            try
            {
                double totalAmount = double.Parse(TotalAmountSAR.Replace(" SAR", ""));
                if (totalAmount == 0)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.ZakatNoInvoicesToBeAdded, AppResources.Information);
                        _navigationService.GoBack();
                    });
                }
                else
                {
                    EnableAgreementView();
                }
                return;



            }
            catch (GAZTUnlockAccountException ex)
            {




            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }


        public async void AggrementContinueBtnClicked()
        {
            try
            {
                if (Year1.Length < 4 || Year2.Length < 4 || Year3.Length < 4)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.ZZPleasefillthemandatoryfields + "(" + AppResources.ZakatYearOne + ", " + AppResources.ZakatYearTwo + ", " + AppResources.ZakatYearThree + ")", AppResources.Information);
                    });
                    return;
                }




                EnableAttachmentsView();


            }
            catch (GAZTUnlockAccountException)
            {

            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }


        public void OutletContinueBtnClicked()
        {
            try
            {
                EnableAttachmentsView();
            }
            catch (GAZTUnlockAccountException ex)
            {


            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }
        public void DisplayDetailsButtonClicked()
        {
            try
            {

                if (ZakatInstalments.d.AccMethod == "A")
                {

                    EnableInstalmentsScheduleView();
                }
                else
                {

                    EnableAttachmentsView();

                }



                // EnableSummaryView();
                //PopulateSummaryReasonData();
            }
            catch (GAZTUnlockAccountException ex)
            {


            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public async void AttachmentsContinueBtnClicked()
        {
            try
            {
                if (BankStatementsAttachmentsListViewData != null && BankStatementsAttachmentsListViewData.Count > 0 && FinanceAttachmentsListViewData != null && FinanceAttachmentsListViewData.Count > 0)
                {
                    EnableSummaryView();
                    PopulateSummaryReasonData();
                    PopulateSummaryAttachments();
                }
                else
                {
                    await _dialogService.ShowMessage(AppResources.VRUploadYourDocument, AppResources.Information);
                }
            }
            catch (GAZTUnlockAccountException ex)
            {




            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }
        public void StatementsContinueBtnClicked()
        {
            try
            {
                if (AttachmentsListViewData == null)
                {
                    AttachmentsListViewData = new ObservableCollection<Attachment>();
                }

                EnableAttachmentsView();

            }
            catch (GAZTUnlockAccountException ex)
            {


            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }


        private void PopulateSummaryReasonData()
        {
            var summarySelectedBillsList = new ObservableCollection<ZakatSelectBillModel>();

            for (int i = 0; i < selectedList.Count; i++)
            {


                DateTime dateStart = new DateTime();
                CultureInfo cultureInfo = new CultureInfo("ar-SA");
                string apiDate = @"""" + selectedList[i].DueDt + @"""";
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
                    billNumber = AppResources.Bill + (i + 1).ToString("00"),
                    amount = "0",
                    saadNumber = selectedList[i].InvNo.ToString(),
                    taxPeriod = dt1,
                    isSelected = false,
                    billType = ZakatTitle





                });
            }
            SummarySelectedBillsList = summarySelectedBillsList;




            if (SelectedFrequencyType == "01")
            {

                SelectedFrequencyName = AppResources.ZakatInstalmetMonthly;

            }
            else if (SelectedFrequencyType == "02")
            {

                SelectedFrequencyName = AppResources.ZakatInstalmetQuarterly;
            }
            else if (SelectedFrequencyType == "03")
            {

                SelectedFrequencyName = AppResources.ZakatInstalmetHalfYearly;
            }
            else if (SelectedFrequencyType == "04")
            {
                SelectedFrequencyName = AppResources.ZakatInstalmetYearly;
            }
        }


        public bool isDraftClicked = false;
        public async void OnSaveDraftClicked()
        {

            double totalAmount = double.Parse(TotalAmountSAR.Replace(" SAR", ""));

            if (totalAmount > 0)
            {

                try
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        IsNewLoading = true;
                    });
                    await Task.Run(async () =>
                    {

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
                                        JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                                        {
                                            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                                        };
                                        //var jsonDateTime = JsonConvert.SerializeObject(dt, microsoftDateFormatSettings);
                                        var jsonDateTime = JsonConvert.SerializeObject(dt.Date, microsoftDateFormatSettings);
                                        string[] dateList = jsonDateTime.Split('+');
                                        jsonDateTime = dateList[0].Replace("\"\\", "");
                                        jsonDateTime = jsonDateTime + ")/";
                                        instalmentsList[i].DueDt = jsonDateTime;

                                    }
                                }

                            }
                        }


                        var fdData = new FnDtlSetObject();
                        fdData.CashBankY1 = CashBankY1;
                        fdData.CashBankY2 = CashBankY2;
                        fdData.CashBankY3 = CashBankY3;
                        fdData.CashRatioY1 = CashRatioY1;
                        fdData.CashRatioY2 = CashRatioY2;
                        fdData.CashRatioY3 = CashRatioY3;
                        fdData.DebitorsY1 = DebitorsY1;
                        fdData.DebitorsY2 = DebitorsY2;
                        fdData.DebitorsY3 = DebitorsY3;
                        fdData.InventoryY1 = InventoryY1;
                        fdData.InventoryY2 = InventoryY2;
                        fdData.InventoryY3 = InventoryY3;
                        fdData.NcFlowY1 = NcFlowY1;
                        fdData.NcFlowY2 = NcFlowY2;
                        fdData.NcFlowY3 = NcFlowY3;
                        fdData.NetIncomeY1 = NetIncomeY1;
                        fdData.NetIncomeY2 = NetIncomeY2;
                        fdData.NetIncomeY3 = NetIncomeY3;
                        fdData.ProfitRatioY1 = profitRatioY1;
                        fdData.ProfitRatioY2 = profitRatioY2;
                        fdData.ProfitRatioY3 = profitRatioY3;
                        fdData.RevenueY1 = RevenueY1;
                        fdData.RevenueY2 = RevenueY2;
                        fdData.RevenueY3 = RevenueY3;
                        fdData.StiY1 = StiY1;
                        fdData.StiY2 = StiY2;
                        fdData.StiY3 = StiY3;
                        fdData.TcAssetsY1 = TcAssetsY1;
                        fdData.TcAssetsY2 = TcAssetsY2;
                        fdData.TcAssetsY3 = TcAssetsY3;
                        fdData.TcLiabltyY1 = TcLiabltyY1;
                        fdData.TcLiabltyY2 = TcLiabltyY2;
                        fdData.TcLiabltyY3 = TcLiabltyY3;
                        fdData.Year1 = Year1;
                        fdData.Year2 = Year2;
                        fdData.Year3 = Year3;
                        fdData.ZakatY1 = ZakatY1;
                        fdData.ZakatY2 = ZakatY2;
                        fdData.ZakatY3 = ZakatY3;
                        fdData.Euser = "";
                        fdData.Fbguid = ZakatInstalments.d.Fbguid;
                        fdData.Fbnum = ZakatInstalments.d.Fbnum;
                        fdData.FormGuid = ZakatInstalments.d.FormGuid;
                        fdData.Formproc = "";
                        fdData.Gpart = "";
                        fdData.LineNo = 0;
                        fdData.Mandt = "";
                        fdData.ReturnId = "";
                        fdData.UserTyp = "";
                        fdData.Status = "";
                        fdData.TxnTp = "";
                        fdData.RankingOrder = "";
                        fdData.ReturnId = ZakatInstalments.d.ReturnId;
                        fdData.DataVersion = "00001";
                        fdData.Waers = "";


                        var FDDetails = new FnDtlSetObject[1];
                        FDDetails[0] = fdData;
                        ZakatInstalments.d.FnDtlSet.results = FDDetails;


                        ZakatInstalments.d.DecCb = "X";
                        ZakatInstalments.d.OffPlanDur = "00";
                        ZakatInstalments.d.OffPymntFreq = "00";
                        ZakatInstalments.d.OffPymntFreq = "00";
                        if (CurrentIndex == 1)
                        {
                            ZakatInstalments.d.StepNumber = "01";

                        }
                        else if (CurrentIndex == 2 || CurrentIndex == 3)
                        {
                            ZakatInstalments.d.StepNumber = "03";

                        }
                        else
                        {
                            ZakatInstalments.d.StepNumber = "04";

                        }

                        ZakatInstalments.d.DataVersion = "00000";

                        ZakatInstalments.d.Operation = "05";


                        if (IsInitialDraft || App.selectedZakatItem != "")
                        {

                            ZakatInstalments.d.Status = ZakatInstalments.d.Status;
                        }
                        else
                        {
                            //ZakatInstalments.d.Fbnum = "";
                            ZakatInstalments.d.Status = ZakatInstalments.d.Status;
                        }







                        if (!isDraftClicked)
                        {
                            isDraftClicked = true;
                            ZakatInstalments = await SubmitClicked();

                            if (ZakatInstalments != null && ZakatInstalments.d != null)
                            {

                                IsInitialDraft = true;
                                MainThread.BeginInvokeOnMainThread(async () =>
                                {
                                    App.selectedZakatItem = ZakatInstalments.d.Fbnum;
                                    setMoreOptioButtons();

                                    List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                                    HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                                    NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                                    headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                                    headerAmountInfo.IsLinkAvailable = false;
                                    //                                    headerAmountInfo.Message = string.Format(AppResources.ZakatDraftSaved, "  " + ZakatInstalments.d.Fbnum);
                                    headerAmountInfo.Message = string.Format(AppResources.ZakatDraftSaved + " " + ZakatInstalments.d.Fbnum + " " + AppResources.ZakatDraftSaved1, " " + ZakatInstalments.d.Fbnum);


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

                                if (string.IsNullOrEmpty(WebServiceManager.ErrorMessageForVAT))
                                {
                                    await Task.Run(() =>
                                    {
                                        IsLoading = false;
                                        IsNewLoading = false;
                                    });
                                    MainThread.BeginInvokeOnMainThread(async () =>
                                    {
                                        IsNewLoading = false;
                                        IsLoading = false;
                                        await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                    });


                                }
                                else
                                {
                                    await Task.Run(() =>
                                    {
                                        IsLoading = false;
                                        IsNewLoading = false;
                                    });
                                    MainThread.BeginInvokeOnMainThread(async () =>
                                    {
                                        IsNewLoading = false;
                                        IsLoading = false;
                                        await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                    });
                                }
                            }
                        }


                    });
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        IsNewLoading = false;
                    });
                }
                catch (Exception)
                {


                }
            }
            else
            {

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZakatNoInvoicesToBeAdded, AppResources.Information);
                    _navigationService.GoBack();
                });
            }

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

            await MopupService.Instance.PushAsync(new ShowVatInformationConfirmationPageView(newDesignPopUp));

        }

        public async void VATSetReturnVoidAsync()
        {
            try
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    IsNewLoading = true;
                });
                await Task.Run(async () =>
                {

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
                                    JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                                    {
                                        DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                                    };
                                    //var jsonDateTime = JsonConvert.SerializeObject(dt, microsoftDateFormatSettings);
                                    var jsonDateTime = JsonConvert.SerializeObject(dt.Date, microsoftDateFormatSettings);
                                    string[] dateList = jsonDateTime.Split('+');
                                    jsonDateTime = dateList[0].Replace("\"\\", "");
                                    jsonDateTime = jsonDateTime + ")/";
                                    instalmentsList[i].DueDt = jsonDateTime;

                                }
                            }

                        }
                    }




                    ZakatInstalments.d.DecCb = "X";
                    ZakatInstalments.d.OffPlanDur = "00";
                    ZakatInstalments.d.OffPymntFreq = "00";
                    ZakatInstalments.d.OffPymntFreq = "00";

                    if (CurrentIndex == 1)
                    {
                        ZakatInstalments.d.StepNumber = "01";

                    }
                    else if (CurrentIndex == 2 || CurrentIndex == 3)
                    {
                        ZakatInstalments.d.StepNumber = "03";

                    }
                    else
                    {
                        ZakatInstalments.d.StepNumber = "04";

                    }

                    ZakatInstalments.d.DataVersion = "00000";

                    ZakatInstalments.d.Operation = "04";


                    if (!isDraftClicked)
                    {
                        isDraftClicked = true;
                        ZakatInstalments = await SubmitClicked();

                        if (ZakatInstalments != null && ZakatInstalments.d != null)
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {


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
                            //MainThread.BeginInvokeOnMainThread(async () =>
                            //{
                            //    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                            //});
                        }
                    }


                });
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    IsNewLoading = false;
                });
            }
            catch (Exception)
            {


            }
        }


        bool isSubmitClicked = false;
        public async void SummaryContinueBtnClicked()
        {
            try
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
                            JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                            {
                                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                            };
                            //var jsonDateTime = JsonConvert.SerializeObject(dt, microsoftDateFormatSettings);
                            var jsonDateTime = JsonConvert.SerializeObject(dt.Date, microsoftDateFormatSettings);
                            string[] dateList = jsonDateTime.Split('+');
                            jsonDateTime = dateList[0].Replace("\"\\", "");
                            jsonDateTime = jsonDateTime + ")/";
                            instalmentsList[i].DueDt = jsonDateTime;

                        }
                    }



                }


                var fdData = new FnDtlSetObject();
                fdData.CashBankY1 = CashBankY1;
                fdData.CashBankY2 = CashBankY2;
                fdData.CashBankY3 = CashBankY3;
                fdData.CashRatioY1 = CashRatioY1;
                fdData.CashRatioY2 = CashRatioY2;
                fdData.CashRatioY3 = CashRatioY3;
                fdData.DebitorsY1 = DebitorsY1;
                fdData.DebitorsY2 = DebitorsY2;
                fdData.DebitorsY3 = DebitorsY3;
                fdData.InventoryY1 = InventoryY1;
                fdData.InventoryY2 = InventoryY2;
                fdData.InventoryY3 = InventoryY3;
                fdData.NcFlowY1 = NcFlowY1;
                fdData.NcFlowY2 = NcFlowY2;
                fdData.NcFlowY3 = NcFlowY3;
                fdData.NetIncomeY1 = NetIncomeY1;
                fdData.NetIncomeY2 = NetIncomeY2;
                fdData.NetIncomeY3 = NetIncomeY3;
                fdData.ProfitRatioY1 = profitRatioY1;
                fdData.ProfitRatioY2 = profitRatioY2;
                fdData.ProfitRatioY3 = profitRatioY3;
                fdData.RevenueY1 = RevenueY1;
                fdData.RevenueY2 = RevenueY2;
                fdData.RevenueY3 = RevenueY3;
                fdData.StiY1 = StiY1;
                fdData.StiY2 = StiY2;
                fdData.StiY3 = StiY3;
                fdData.TcAssetsY1 = TcAssetsY1;
                fdData.TcAssetsY2 = TcAssetsY2;
                fdData.TcAssetsY3 = TcAssetsY3;
                fdData.TcLiabltyY1 = TcLiabltyY1;
                fdData.TcLiabltyY2 = TcLiabltyY2;
                fdData.TcLiabltyY3 = TcLiabltyY3;
                fdData.Year1 = Year1;
                fdData.Year2 = Year2;
                fdData.Year3 = Year3;
                fdData.ZakatY1 = ZakatY1;
                fdData.ZakatY2 = ZakatY2;
                fdData.ZakatY3 = ZakatY3;
                fdData.Euser = "";
                fdData.Fbguid = ZakatInstalments.d.Fbguid;
                fdData.Fbnum = ZakatInstalments.d.Fbnum;
                fdData.FormGuid = ZakatInstalments.d.FormGuid;
                fdData.Formproc = "";
                fdData.Gpart = "";
                fdData.LineNo = 0;
                fdData.Mandt = "";
                fdData.ReturnId = "";
                fdData.UserTyp = "";
                fdData.Status = "";
                fdData.TxnTp = "";
                fdData.RankingOrder = "";
                fdData.ReturnId = ZakatInstalments.d.ReturnId;
                fdData.DataVersion = "00001";
                fdData.Waers = "";


                var FDDetails = new FnDtlSetObject[1];
                FDDetails[0] = fdData;
                ZakatInstalments.d.FnDtlSet.results = FDDetails;

                ZakatInstalments.d.DecCb = "X";
                ZakatInstalments.d.OffPlanDur = "00";
                ZakatInstalments.d.OffPymntFreq = "00";
                ZakatInstalments.d.OffPymntFreq = "00";
                ZakatInstalments.d.StepNumber = "04";
                ZakatInstalments.d.Operation = "58";
                ZakatInstalments.d.DataVersion = "00001";

                if (IsInitialDraft || App.selectedZakatItem != "")
                {

                }
                else
                {
                    // ZakatInstalments.d.Fbnum = "";
                }
                if (!isSubmitClicked)
                {
                    isSubmitClicked = true;
                    ZakatInstalments = await SubmitClicked();

                    if (ZakatInstalments != null && ZakatInstalments.d != null)
                    {

                        ZakatReferanceNumber = ZakatInstalments.d.Fbnum;


                        await EnableSucessScreenAsync();
                    }
                    else
                    {

                    }

                }


                //Display Success Screen

            }
            catch (GAZTUnlockAccountException ex)
            {


            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }



        public void OnZakatInstalmentReasonClicked()
        {
            try
            {
                ObservableCollection<string> ZakatreasonData = new ObservableCollection<string>();
                ZakatreasonData.Add(AppResources.TinDeregistrationReasonBankruptcy);
                ZakatreasonData.Add(AppResources.TinDeregistrationReasonDeath);
                ZakatreasonData.Add(AppResources.TinDeregistrationReasonLiquidation);
                ZakatreasonData.Add(AppResources.TinDeregistrationReasonEstablishmentToCompany);

                //await MopupService.Instance.PushAsync(new PickerPageView(ZakatreasonData));
            }
            catch (GAZTUnlockAccountException ex)
            {

            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (Exception)
            {


            }
        }
        public void NewAttachmentClicked()
        {
            try
            {
            }
            catch (GAZTUnlockAccountException ex)
            {

            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (Exception)
            {


            }
        }
        public async void SummaryInstallmentDetailsBtnClicked()
        {
            try
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Instalment details schedule is displayed here.", "OK");



            }
            catch (GAZTUnlockAccountException ex)
            {


            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
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

        public async void BankStatementsAttachmentClicked()
        {
            if (MopupService.Instance.PopupStack.Count > 0) return;
            _bankStatementsAttachment = true;
            if (BankStatementsAttachmentsListViewData == null)
            {
                BankStatementsAttachmentsListViewData = new ObservableCollection<Attachment>();
            }
            try
            {

                await MopupService.Instance.PushAsync(new FilesUploadPopUpPageView(
                    BankStatementsAttachmentsListViewData.ToList(),
                    WhichAttachment.ZakatInstalmentBankStatements, ZakatInstalments.d.ReturnId));




            }
            catch (GAZTUnlockAccountException ex)
            {

            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (Exception)
            {


            }
        }

        public async void FinanceAttachmentClicked()
        {
            if (MopupService.Instance.PopupStack.Count > 0) return;
            _bankStatementsAttachment = false;
            if (FinanceAttachmentsListViewData == null)
            {
                FinanceAttachmentsListViewData = new ObservableCollection<Attachment>();
            }
            try
            {

                await MopupService.Instance.PushAsync(new FilesUploadPopUpPageView(
                FinanceAttachmentsListViewData.ToList(),
                WhichAttachment.ZakatInstalmentFinance, ZakatInstalments.d.ReturnId));



            }
            catch (GAZTUnlockAccountException ex)
            {

            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (Exception)
            {


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
                MinInstalments = 1;
                MaxInstalments = 36;
                MinInstalmentsTitle = AppResources.ZakatMin + " " + 1;
                MaxInstalmentsTitle = AppResources.ZakatMax + " " + 36;
                SelectedFrequencyType = "01";

            }
            else if (frequencyModel.FrequencyOptions == AppResources.ZakatInstalmetQuarterly)
            {
                MinInstalments = 1;
                MaxInstalments = 12;
                MinInstalmentsTitle = AppResources.ZakatMin + " " + 1;
                MaxInstalmentsTitle = AppResources.ZakatMax + " " + 12;

                SelectedFrequencyType = "02";
            }
            else if (frequencyModel.FrequencyOptions == AppResources.ZakatInstalmetHalfYearly)
            {
                MinInstalments = 1;
                MaxInstalments = 6;
                MinInstalmentsTitle = AppResources.ZakatMin + " " + 1;
                MaxInstalmentsTitle = AppResources.ZakatMax + " " + 6;
                SelectedFrequencyType = "03";
            }
            else if (frequencyModel.FrequencyOptions == AppResources.ZakatInstalmetYearly)
            {
                MinInstalments = 1;
                MaxInstalments = 3;
                MinInstalmentsTitle = AppResources.ZakatMin + " " + 1;
                MaxInstalmentsTitle = AppResources.ZakatMax + " " + 3;
                SelectedFrequencyType = "04";
            }

            if (ZakatInstalments.d.PlanDur != null && int.Parse(ZakatInstalments.d.PlanDur) > 0)
            {
                NumberOFInstalmentSliderValue = int.Parse(ZakatInstalments.d.PlanDur);
            }
            else
            {
                NumberOFInstalmentSliderValue = 1;
            }


        }



        #endregion

        #region ZakatInvoiceList

        public async Task GetZaktaInvoiceList()
        {
            try
            {
                selectedList.Clear();

                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {

                    IsLoading = true;

                    ZakatInvoiceList invoiceList = null;
                    // ZakatInvoicesList = null;

                    try
                    {
                        invoiceList = await ZakatInstallmentPlanWebServiceManager.GetZakatInvoicesList(IsZakat, App.selectedZakatItem);

                        if (invoiceList != null && invoiceList.d != null)
                        {


                            var totalAmount = 0.0;
                            var totalAmountDue = 0.0;

                            ZakatInvoicesList = invoiceList.d.results;
                            for (int i = 0; i < ZakatInvoicesList.Count; i++)
                            {
                                if (ZakatInvoicesList[i].Abtyp.Equals("ITAX"))
                                {
                                    ZakatInvoicesList[i].Abtyp = AppResources.ZakatInstalmetSelectTypeIncomeTax;
                                }
                                else if (ZakatInvoicesList[i].Abtyp.Equals("ZAKT"))
                                {
                                    ZakatInvoicesList[i].Abtyp = AppResources.FORM5Zakat;
                                }

                                DateTime dateStart = new DateTime();
                                CultureInfo cultureInfo = new CultureInfo("ar-SA");
                                string apiDate = @"""" + ZakatInvoicesList[i].DueDt + @"""";
                                dateStart = JsonConvert.DeserializeObject<DateTime>(apiDate);

                                GregorianCalendar hjCalendar = new GregorianCalendar();
                                int year = hjCalendar.GetYear(dateStart);
                                int month = hjCalendar.GetMonth(dateStart);
                                int day = hjCalendar.GetDayOfMonth(dateStart);

                                string dateStr = string.Format("{0:00}/{1}/{2}", day, month, year);

                                ZakatInvoicesList[i].DueDt = dateStr;

                                string dt1 = string.Empty;
                                string[] dts = null;
                                dts = ZakatInvoicesList[i].DueDt.Split('/');
                                dt1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];
                                ZakatInvoicesList[i].DueDt = dt1;


                                if (ZakatInvoicesList[i].InvCb == "X")
                                {
                                    selectedList.Add(ZakatInvoicesList[i]);
                                    totalAmountDue += Convert.ToDouble(ZakatInvoicesList[i].DueAmt);
                                }

                                totalAmount += Convert.ToDouble(ZakatInvoicesList[i].InvAmt);
                            }

                            EnableVATBillView();
                            TotalAmountSAR = string.Format("{0:N2}", totalAmount) + " SAR";
                            VATBillDueAmount = string.Format("{0:N2}", totalAmountDue) + " SAR";

                            if (totalAmountDue > 0)
                            {
                                MaxAmount = Math.Round(totalAmountDue, 2);
                                MaxAmountTitle = AppResources.ZakatMax + " " + MaxAmount;

                            }
                            if (totalAmountDue > 0)
                            {
                                MinAmount = Math.Round(totalAmountDue * (20.0f / 100.0f), 2);
                                DownPaymentAmount = MinAmount;
                                MinAmountTitle = AppResources.ZakatMin + " " + MinAmount;
                                DownPaymentSliderValue = DownPaymentAmount;
                            }

                            if (App.selectedZakatItem != "")
                            {

                                if (ZakatInstalments.d.DpAmt != null && double.Parse(ZakatInstalments.d.DpAmt) > 0)
                                {

                                    DownPaymentSliderValue = double.Parse(ZakatInstalments.d.DpAmt);
                                }

                                if (ZakatInstalments.d.TotAmt != null && double.Parse(ZakatInstalments.d.TotAmt) > 0)
                                {
                                    TotalAmountSAR = ZakatInstalments.d.TotAmt;
                                }

                                if (ZakatInstalments.d.PlanDur != null && int.Parse(ZakatInstalments.d.PlanDur) > 0)
                                {
                                    NumberOFInstalmentSliderValue = int.Parse(ZakatInstalments.d.PlanDur);
                                }

                                if (ZakatInstalments.d.PymntFreq != null && double.Parse(ZakatInstalments.d.PymntFreq) > 0)
                                {
                                    SelectedFrequencyType = ZakatInstalments.d.PymntFreq;
                                    MessagingCenter.Send<object, string>(this, "SelectedFrequencyType", SelectedFrequencyType);
                                }

                                if (ZakatInstalments.d.AttachSet != null && ZakatInstalments.d.AttachSet.results != null)
                                {
                                    var bankAttachmentListViewData = new ObservableCollection<Attachment>();
                                    var financialAttachmentListViewData = new ObservableCollection<Attachment>();
                                    foreach (var attach in ZakatInstalments.d.AttachSet.results)
                                    {
                                        if (attach.Dotyp == "ZIP2")
                                        {
                                            bankAttachmentListViewData.Add(attach);
                                        }
                                        else if (attach.Dotyp == "ZIP3")
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



                            }
                        }
                        else
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                _navigationService.GoBack();
                            });
                        }
                        IsLoading = false;
                    }

                    catch (InternetException ex)
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            IsLoading = false;
                            _navigationService.GoBack();
                        });

                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

            }
            catch (GAZTVATRegistrationInProcessException ex)
            {

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });

            }
            catch (Exception)
            {


                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }


        #endregion


        #region OnPageLoad

        public async Task OnPageLoad()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {

                    IsLoading = true;
                    ZakatInstalments = null;
                    try
                    {
                        ZakatInstalments = await ZakatInstallmentPlanWebServiceManager.GetZakatInstalmentPostData(App.selectedZakatItem);

                        if (ZakatInstalments != null)
                        {

                            if (IsZakat)
                            {
                                await MopupService.Instance.PushAsync(new InstructionsBottomPopUpView(instructionString: AppResources.ZakatInstructions, checkBoxString: AppResources.ZakatInstructionsCheckBoxDesc, continueString: AppResources.ZakatInstalmetPlanTitle,
                   _dialogType: InstructionsBottomPopUpViewModel.DialogType
                       .Instructions));
                            }
                            else
                            {
                                await MopupService.Instance.PushAsync(new InstructionsBottomPopUpView(instructionString: AppResources.ZakatInstructions, checkBoxString: AppResources.ZakatInstructionsCheckBoxDesc, continueString: AppResources.ZakatInstalmetSelectTypeIncomeTax,
                   _dialogType: InstructionsBottomPopUpViewModel.DialogType
                       .Instructions));

                            }

                            if (ZakatInstalments.d.InstReqReason != null && App.selectedZakatItem != "")
                            {

                                MessagingCenter.Send<object, string>(this, "SelectedReason", ZakatInstalments.d.InstReqReason);
                            }

                            BindZakatBillsList();
                        }
                        else
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                _navigationService.GoBack();
                            });
                        }
                        IsLoading = false;
                    }
                    catch (GAZTVATRegistrationInProcessException ex)
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            IsLoading = false;
                            _navigationService.GoBack();
                        });
                    }
                    catch (InternetException ex)
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            IsLoading = false;
                            _navigationService.GoBack();
                        });
                        //   await Task.Run(() =>
                        //   {
                        //  });
                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

            }

            catch (Exception)
            {


                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
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
        #region Method

        public void setDATA()
        {
            try
            {
                if (NoOfInstalments == 0)
                {
                }
                else
                {
                }
            }
            catch (Exception)
            {

            }


        }
        #endregion

        #region MAP_VAT_RequestObject

        private static string GetLangZParameterAREN()
        {
            if (App.IsArabic)
                return "AR";
            else
                return "EN";
        }

        public ZakatInstalmentPlanRequest BuildRequestObject()
        {
            ZakatInstalmentPlanRequest _postData = new ZakatInstalmentPlanRequest();



            _postData.Fbguid = App.LoginDataRetrieved.FbGuid;
            _postData.UserTyp = "TP";
            _postData.TxnTp = ZakatInstalments.d.TxnTp;
            _postData.MobNo = ZakatInstalments.d.MobNo;
            _postData.FormGuid = "";
            _postData.Fbnum = ZakatInstalments.d.Fbnum;
            _postData.DataVersion = ZakatInstalments.d.DataVersion;
            _postData.Operation = ZakatInstalments.d.Operation;
            _postData.Euser = "";
            _postData.StepNumber = ZakatInstalments.d.StepNumber;
            _postData.Email = ZakatInstalments.d.Email;
            _postData.Officer = ZakatInstalments.d.Officer;
            _postData.Langz = GetLangZParameterAREN();
            _postData.Status = ZakatInstalments.d.Status;
            _postData.SuAmt = ZakatInstalments.d.SuAmt;
            _postData.SuAmtFg = ZakatInstalments.d.SuAmtFg;
            _postData.Formproc = ZakatInstalments.d.Formproc;
            _postData.Tin = App.LoginDataRetrieved.TIN;
            _postData.Periodkey = ZakatInstalments.d.Periodkey;
            _postData.ReturnId = ZakatInstalments.d.ReturnId;
            _postData.TinNm = ZakatInstalments.d.TinNm;
            _postData.AltMobNo = ZakatInstalments.d.AltMobNo;
            _postData.InstReqFor = ZakatInstalments.d.InstReqFor;
            _postData.InstReqReason = ZakatInstalments.d.InstReqReason;
            _postData.TotAmt = ZakatInstalments.d.TotAmt;
            _postData.DpAmt = ZakatInstalments.d.DpAmt;
            _postData.PymntFreq = ZakatInstalments.d.PymntFreq;
            _postData.PlanDur = ZakatInstalments.d.PlanDur;
            _postData.OffPymntFreq = ZakatInstalments.d.OffPymntFreq;
            _postData.OffPlanDur = ZakatInstalments.d.OffPlanDur;
            _postData.DecCb = ZakatInstalments.d.DecCb;
            _postData.Waers = ZakatInstalments.d.Waers;
            _postData.AccMethod = ZakatInstalments.d.AccMethod;
            _postData.Sopbel = "";
            _postData.OffAmt = ZakatInstalments.d.OffAmt;
            _postData.PaymtDt = ZakatInstalments.d.PaymtDt;
            _postData.PenlAmt = ZakatInstalments.d.PenlAmt;
            _postData.InsDtOff = ZakatInstalments.d.InsDtOff;




            if (ZakatInstalments.d.NotesSet.results == null)
            {

                _postData.NotesSet = new NotesSet[0];
            }
            else
            {
                _postData.NotesSet = ZakatInstalments.d.NotesSet.results;
            }

            if (ZakatInstalments.d.insPlanSet.results == null)
            {

                _postData.insPlanSet = new Array[0];

            }
            else
            {
                _postData.insPlanSet = ZakatInstalments.d.insPlanSet.results.ToArray();

            }
            if (ZakatInstalments.d.AttachSet.results == null)
            {

                _postData.AttachSet = new Array[0];
            }
            else
            {
                //_postData.AttachSet = ZakatInstalments.d.AttachSet.results.ToArray();
                _postData.AttachSet = new Array[0];



            }

            if (ZakatInstalments.d.insPlan_OffSet.result == null)
            {

                _postData.insPlan_OffSet = new Array[0];

            }
            else
            {
                _postData.insPlan_OffSet = ZakatInstalments.d.insPlan_OffSet.result.ToArray();

            }

            if (ZakatInstalments.d.retmsgSet.results == null)
            {
                _postData.retmsgSet = new Array[0];

            }
            else
            {
                _postData.retmsgSet = ZakatInstalments.d.retmsgSet.results.ToArray();


            }
            if (ZakatInstalments.d.FnDtlSet.results == null || ZakatInstalments.d.Operation == "04")
            {


                _postData.FnDtlSet = new FnDtlSetObject[0];

            }
            else
            {
                _postData.FnDtlSet = ZakatInstalments.d.FnDtlSet.results.ToArray();

            }

            for (int i = 0; i < selectedList.Count; i++)
            {
                var dataItem = selectedList[i] as ZakatInvoicesResult;


                int index = ZakatInvoicesList.ToList().FindIndex(item => item.InvNo == dataItem.InvNo);

                if (ZakatInvoicesList[i].Abtyp.Equals(AppResources.ZakatInstalmetSelectTypeIncomeTax))
                {
                    ZakatInvoicesList[i].Abtyp = "ITAX";
                }
                else if (ZakatInvoicesList[i].Abtyp.Equals(AppResources.FORM5Zakat))
                {
                    ZakatInvoicesList[i].Abtyp = "ZAKT";
                }

                ZakatInvoicesList[index].InvCb = "X";


            }


            if (ZakatInvoicesList != null)
            {

                var invoicesList = ZakatInvoicesList.ToList();





                if (invoicesList.Count != 0)
                {

                    string apiDate = invoicesList[0].DueDt;
                    if (!apiDate.Contains("Date"))
                    {

                        for (int i = 0; i < invoicesList.Count; i++)
                        {

                            DateTime dt = Convert.ToDateTime(invoicesList[i].DueDt);
                            JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                            {
                                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                            };
                            var jsonDateTime = JsonConvert.SerializeObject(dt.Date, microsoftDateFormatSettings);
                            string[] dateList = jsonDateTime.Split('+');
                            jsonDateTime = dateList[0].Replace("\"\\", "");
                            jsonDateTime = jsonDateTime + ")/";
                            invoicesList[i].DueDt = jsonDateTime;

                        }
                    }


                    _postData.invDtlsSet = invoicesList.ToArray();
                }
                else
                {

                    _postData.invDtlsSet = new ZakatInvoicesResult[0];
                }

            }
            else
            {

                _postData.invDtlsSet = new ZakatInvoicesResult[0];
            }






            return _postData;


        }

        #endregion


        #region ApiIntegration




        public async Task<ZakatInstalmentPlanResponse> SubmitClicked()
        {
            ZakatInstalmentPlanResponse response = new ZakatInstalmentPlanResponse();
            ZakatInstalmentPlanRequest request = new ZakatInstalmentPlanRequest();

            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });

                request = BuildRequestObject();

                response = await ZakatInstallmentPlanWebServiceManager.SaveZakatInstalmentData(request);
                PopToRootPage();
                if (response != null)
                {
                    try
                    {
                        if (response != null)
                        {

                        }
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
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();

                });
                return response;
            }

            catch (Exception)
            {


                return response;
            }

        }

        #endregion
    }
}
