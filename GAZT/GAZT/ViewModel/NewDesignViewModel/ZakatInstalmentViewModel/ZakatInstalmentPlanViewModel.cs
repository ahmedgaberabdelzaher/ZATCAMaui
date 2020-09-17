using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.Models.ZakatInstalationModels;
using EGAZT.Views.NewDesign;
using EGAZT.Views.NewDesign.Common;
using EGAZT.Views.NewDesign.GenericPickers;
using EGAZT.Views.NewDesign.VATDeclarationPages;
using EGAZT.Views.NewDesign.ZakatInstalmentPlan;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using static EGAZT.Models.ZakatInstalationModels.ZakatInstalmentPlanRequest;
using static EGAZT.Models.ZakatInstalationModels.ZAKATRequestPlanModel;
using NotesSet = EGAZT.Models.ZakatInstalationModels.NotesSet;

namespace EGAZT.ViewModel.NewDesignViewModel.ZakatInstalmentPlanViewModel
{
    public class ZakatInstalmentPlanViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        private bool _isLoading = false;
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
                RaisePropertyChanged("IsNewLoading");
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
                RaisePropertyChanged("IsZakat");
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
                RaisePropertyChanged("ZakatTitle");
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
                RaisePropertyChanged("VATDueAmount");
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
                RaisePropertyChanged("SelectedFrequencyType");
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
                RaisePropertyChanged("SelectedFrequencyName");
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
                RaisePropertyChanged("VATPenalityAmount");
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
                RaisePropertyChanged("VATLiabilityAmount");
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
                RaisePropertyChanged("VATBillDueAmount");
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
                RaisePropertyChanged("IsBackButtonVisible");
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
                RaisePropertyChanged("IsSelectionViewEnabled");
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
                RaisePropertyChanged("SelectedOutletOptionIndex");
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
                RaisePropertyChanged("IsDisplayInstalmentsVisible");
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
                _zakatInvoicesList = value;
                RaisePropertyChanged("ZakatInvoicesList");
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
                RaisePropertyChanged("isNoDataLableVisible");
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
                RaisePropertyChanged("IsAgreementViewEnabled");
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
                RaisePropertyChanged("IsOutletViewEnabled");
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
                RaisePropertyChanged("IsBillsViewEnabled");
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
                RaisePropertyChanged("IsVATBillsViewEnabled");
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
                RaisePropertyChanged("InstalmentPlanAgreementsVisible");
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
                RaisePropertyChanged("IsAttachmentsViewEnabled");
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
                RaisePropertyChanged("IsStatementViewEnabled");
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
                RaisePropertyChanged("IsSummaryViewEnabled");
            }
        }
        private double downPaymentSliderValue = 0.0;
        public double DownPaymentSliderValue
        {
            get { return downPaymentSliderValue; }
            set
            {
                downPaymentSliderValue = value;
                RaisePropertyChanged("DownPaymentSliderValue");
            }
        }
        public int NoOfInstalments
        {
            set
            {
                if (noOfInstalments != value)
                {
                    noOfInstalments = value;
                    RaisePropertyChanged("NoOfInstalments");
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
                if (_numberOFInstalmentSliderValue != value)
                {
                    _numberOFInstalmentSliderValue = value;
                    RaisePropertyChanged("NumberOFInstalmentSliderValue");
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
                if (minInstalments != value)
                {
                    minInstalments = value;
                    RaisePropertyChanged("MinInstalments");
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
                if (maxInstalments != value)
                {
                    maxInstalments = value;
                    RaisePropertyChanged("MaxInstalments");
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
                if (downPaymentAmount != value)
                {
                    downPaymentAmount = value;
                    RaisePropertyChanged("DownPaymentAmount");
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
                if (minInstalmentsTitle != value)
                {
                    minInstalmentsTitle = value;
                    RaisePropertyChanged("MinInstalmentsTitle");
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
                    RaisePropertyChanged("MinAmountTitle");
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
                    RaisePropertyChanged("MaxAmountTitle");
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
                    RaisePropertyChanged("MaxInstalmentsTitle");
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
                    RaisePropertyChanged("MinAmount");
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
                    RaisePropertyChanged("MaxAmount");
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
                    RaisePropertyChanged("TotalAmountSAR");
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
                    RaisePropertyChanged("DownPaymentSAR");
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
                    RaisePropertyChanged("PeriodicInstalment");
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
                    RaisePropertyChanged("InputData");
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
                RaisePropertyChanged("IsVATAmountVisible");
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
                _isZakatSelected = value;
                RaisePropertyChanged("IsZakatSelected");
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
                _isIncomeTaxViewEnabled = value;
                RaisePropertyChanged("IsIncomeTaxViewEnabled");
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
                RaisePropertyChanged("IsSubIncomeTaxViewEnabled");
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
                RaisePropertyChanged("CashBankY1");
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
                RaisePropertyChanged("CashBankY2");
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
                RaisePropertyChanged("CashBankY3");
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
                RaisePropertyChanged("CashRatioY1");
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
                RaisePropertyChanged("CashRatioY2");
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
                RaisePropertyChanged("CashRatioY3");
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
                RaisePropertyChanged("DebitorsY1");
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
                RaisePropertyChanged("DebitorsY2");
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
                RaisePropertyChanged("DebitorsY3");
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
                RaisePropertyChanged("InventoryY1");
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
                RaisePropertyChanged("InventoryY2");
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
                RaisePropertyChanged("InventoryY3");
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
                RaisePropertyChanged("NcFlowY1");
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
                RaisePropertyChanged("NcFlowY2");
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
                RaisePropertyChanged("NcFlowY3");
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
                RaisePropertyChanged("NetIncomeY1");
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
                RaisePropertyChanged("NetIncomeY2");
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
                RaisePropertyChanged("NetIncomeY3");
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
                RaisePropertyChanged("ProfitRatioY1");
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
                RaisePropertyChanged("ProfitRatioY2");
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
                RaisePropertyChanged("ProfitRatioY3");
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
                RaisePropertyChanged("RevenueY1");
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
                RaisePropertyChanged("RevenueY2");
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
                RaisePropertyChanged("RevenueY3");
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
                RaisePropertyChanged("StiY1");
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
                RaisePropertyChanged("StiY2");
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
                RaisePropertyChanged("StiY3");
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
                RaisePropertyChanged("TcAssetsY1");
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
                RaisePropertyChanged("TcAssetsY2");
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
                RaisePropertyChanged("TcAssetsY3");
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
                RaisePropertyChanged("TcLiabltyY1");
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
                RaisePropertyChanged("TcLiabltyY2");
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
                RaisePropertyChanged("TcLiabltyY3");
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
                RaisePropertyChanged("Year1");
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
                RaisePropertyChanged("Year2");
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
                RaisePropertyChanged("Year3");
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
                RaisePropertyChanged("ZakatY1");
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
                RaisePropertyChanged("ZakatY2");
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
                RaisePropertyChanged("ZakatY3");
            }
        }

        private bool _isPenaltyVisible = false;
        public bool IsPenaltyVisible
        {
            get { return _isPenaltyVisible; }
            set
            {
                _isPenaltyVisible = value;
                RaisePropertyChanged("IsPenaltyVisible");
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
                            ListZAKATCorrespondance = SortedList.ToList<CorrespondanceModel>();
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
                                ListZAKATCorrespondance = SortedList.ToList<CorrespondanceModel>();
                            }
                        }
                    }

                    TxtSelectedStatusZakat = _selectedFilterZakat.Filter;
                }
                RaisePropertyChanged("SelectedFilterZakat");
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
                RaisePropertyChanged("ZakatReferanceNumber");
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
                RaisePropertyChanged("ListZAKATCorrespondance");
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
                RaisePropertyChanged("TxtSelectedStatusZakat");
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
                else
                {
                    MarkComplete = false;
                    RaisePropertyChanged(nameof(MarkComplete));
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
                RaisePropertyChanged("SelectedFilterZakatPrev");
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
            catch (Exception ex)
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
                RaisePropertyChanged("CorresFilterZakat");
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
                RaisePropertyChanged("SubListZAKATCorrespondance");
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
                RaisePropertyChanged("SubTxtSelectedStatusZakat");
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
                RaisePropertyChanged("StatementList");
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
                RaisePropertyChanged("SubCorresFilterZakat");
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
                RaisePropertyChanged("SubSetSelectedIndexZakat");
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
                RaisePropertyChanged("InstalmentPlans");
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
                RaisePropertyChanged("ZakatInstalmentPlanModel");
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
                RaisePropertyChanged("OutletDecisionOptions");
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
                RaisePropertyChanged("ZakatSelectBillModel");
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
                RaisePropertyChanged("SelectedBillsList");
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
                RaisePropertyChanged("ZakatAgreementOptions");
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
                RaisePropertyChanged("AttachmentsListViewData");
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
                RaisePropertyChanged("SummarySelectedBillsList");
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
                RaisePropertyChanged("SelectedOutletOption");
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
                _zakatInstalments = value;
                RaisePropertyChanged("ZakatInstalments");
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
                if (_isInstrunctionChecked != null)
                {
                    if (_isInstrunctionChecked)
                    {
                        IsContinueButtonEnable = true;
                    }
                    else
                    {
                        IsContinueButtonEnable = false;
                    }
                }
                RaisePropertyChanged("IsInstrunctionChecked");
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
                RaisePropertyChanged("IsVatTermsChecked");
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

                RaisePropertyChanged("IsDeclarationChecked");
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
                    ContinueButtonnBackroundColor = Color.FromHex("#d49504");
                }
                else
                {
                    ContinueButtonnBackroundColor = Color.FromHex("#9EA4A9");
                }
                RaisePropertyChanged("IsContinueButtonEnable");
            }
        }
        private Color _continueButtonnBackroundColor = Color.FromHex("#d49504");
        public Color ContinueButtonnBackroundColor
        {
            get
            {
                return _continueButtonnBackroundColor;
            }
            set
            {
                _continueButtonnBackroundColor = value;
                RaisePropertyChanged("ContinueButtonnBackroundColor");
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
                    EnableVATBillView();
                    break;
                case (int)PagesEnum.IsDisplayInstalmentsVisible:
                    EnableAgreementView();
                    break;
                case (int)PagesEnum.ZakatAttachmentsView:
                    EnableInstalmentsScheduleView();
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
                _successMessage = value;
                RaisePropertyChanged("SuccessMessage");
            }
        }
        public ZakatInstalmentPlanViewModel(INavigationService navigationService, IDialogService dialogService)
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

            EnableDeclarationContinue();

            if (Preferences.Get("isZakat", false))
            {
                ZakatTitle = AppResources.ZakatInstalmetSelectTypeZakat;
            }
            else
            {
                ZakatTitle = AppResources.ZakatInstalmetSelectTypeIncomeTax;
            }



            if (Preferences.Get("IsFromRevok", false))
            {
                SuccessMessage = AppResources.ZakatInstalmentRevokedSuccessfully;
                ZakatReferanceNumber = Preferences.Get("RevokeRef", "");
            }


            else
            {
                SuccessMessage = AppResources.VatInstalmentPlanSubmittedSuccess;
            }
            _dialogService = dialogService;
            GoBackClick = new Command(async () =>
            {
                Backnavigations();
            });
            onMoreOptionClicked = new Command(async () =>
            {
                PopupNavigation.Instance.PushAsync(new MoreMenuPopUpPageViewRTwo(ListOfActionButtonsApplicable));
            });

            CloseClick = new Command(async () =>
            {
                CurrentIndex = 1;
                EnableSlectionView();
            });
            GoBackToBills = new Command(async () =>
            {
                EnableVATBillView();
            });
            GoBackToAggrement = new Command(async () =>
            {
                EnableAgreementView();
            });
            GoBackToAttachments = new Command(async () =>
            {
                EnableAttachmentsView();
            });

            VATInstalationClicked = new Command(this.VATInstalationTapped);
            ReasonContinueBtnTapped = new Command(this.ReasonContinueBtnClicked);

            AggrementContinueBtnTapped = new Command(this.AggrementContinueBtnClicked);
            BillContinueBtnTapped = new Command(this.BillContinueBtnClicked);
            AttachmentsContinueBtnTapped = new Command(this.AttachmentsContinueBtnClicked);
            StatementsContinueBtnTapped = new Command(this.StatementsContinueBtnClicked);
            SummaryContinueBtnTapped = new Command(this.SummaryContinueBtnClicked);
            SummaryInstallmentDetailsBtnTapped = new Command(this.SummaryInstallmentDetailsBtnClicked);
            DisplayDetailsButtonTapped = new Command(this.DisplayDetailsButtonClicked);
            OnZakatInstalmentReasonTapped = new Command(this.OnZakatInstalmentReasonClicked);
            NewAttachmentTapped = new Command(this.NewAttachmentClicked);
            BankStatementsAttachmentTapped = new Command(BankStatementsAttachmentClicked);
            FinanceAttachmentTapped = new Command(FinanceAttachmentClicked);

            InstallmentDetailsBtnTapped = new Command(async () =>
            {

                var totalamount = TotalAmountSAR.Replace(" SAR", "").Replace(",", "");
                var instalmentamount = VATBillDueAmount.Replace(" SAR", "").Replace(",", "");
                ZakatInstalments.d.DpAmt = DownPaymentAmount.ToString();
                ZakatInstalments.d.TotAmt = totalamount.ToString();
                ZakatInstalments.d.Operation = "51";
                ZakatInstalments.d.StepNumber = "03";
                ZakatInstalments.d.PlanDur = noOfInstalments.ToString();
                ZakatInstalments.d.PymntFreq = SelectedFrequencyType;
                if (IsZakat)
                {
                    ZakatInstalments.d.InstReqFor = "01";

                }
                else
                {
                    ZakatInstalments.d.InstReqFor = "02";
                }
                if (IDType == AppResources.ZakatFinancialCrisis)
                {
                    ZakatInstalments.d.InstReqReason = "01";
                }
                else if (IDType == AppResources.ZakatDisputeInFavorOfGAZT)
                {
                    ZakatInstalments.d.InstReqReason = "02";
                }
                else if (IDType == AppResources.ZakatOtherReason)
                {
                    ZakatInstalments.d.InstReqReason = "03";
                }
                else
                {
                    ZakatInstalments.d.InstReqReason = "01";
                }


                ZakatInstalments.d.insPlan_OffSet = new InsPlanOffSet();
                ZakatInstalments.d.NotesSet = new EGAZT.Models.ZakatInstalationModels.NotesSetResult();
                ZakatInstalments.d.insPlanSet = new EGAZT.Models.ZakatInstalationModels.InsPlanSet();
                ZakatInstalments.d.retmsgSet = new EGAZT.Models.ZakatInstalationModels.RetmsgSet();
                ZakatInstalments.d.FnDtlSet = new EGAZT.Models.ZakatInstalationModels.FnDtlSet();
                ZakatInstalments.d.AttachSet = new EGAZT.Models.ZakatInstalationModels.AttachSet();
               

                ZakatInstalments = await SubmitClicked();

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
                _idType = value;
                RaisePropertyChanged("IDType");
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

        public Dictionary<string, string> IDTypeDictionary = new Dictionary<string, string>
        {
            {"","oBlak"},
            {AppResources.ZakatFinancialCrisis,"oFin"},
            {AppResources.ZakatDisputeInFavorOfGAZT,"oDisp"},
            {AppResources.ZakatOtherReason,"3"},
        };

        public async void showInstructionsDialog()
        {
            try
            {

                EnableSlectionView();
            }
            catch (GAZTUnlockAccountException ex)
            {

            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
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
                    String action = await Application.Current.MainPage.DisplayActionSheet("", AppResources.ZZCancel, null, ListOfActionButtonsApplicable.ToArray());
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
            catch (Exception ex)
            {
            }
        }

        public void setMoreOptioButtons()
        {
            var listOfActionButtonsApplicable = new List<string>();
            listOfActionButtonsApplicable.Add(AppResources.ZZVoid);
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

                //VATDueAmount = ZakatInstalments.d.TotInvAmt;
                //VATPenalityAmount = ZakatInstalments.d.Peneltyamt;
                //VATBillDueAmount = ZakatInstalments.d.Totdueamt;
                //VATLiabilityAmount = ZakatInstalments.d.Totliablityamt;
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
            //SelectedOutletOption = OutletDecisionOptions[0];
            //SelectedOutletOptionIndex = 0;
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
            //SelectedOutletOption = OutletDecisionOptions[0];
            //SelectedOutletOptionIndex = 0;
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
            //SelectedOutletOption = OutletDecisionOptions[0];
            //SelectedOutletOptionIndex = 0;
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
            //SelectedOutletOption = OutletDecisionOptions[0];
            //SelectedOutletOptionIndex = 0;
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



            if ((NetIncomeY1.Length > 0 && double.Parse(NetIncomeY1) != 0) && (RevenueY1.Length > 0 && double.Parse(RevenueY1) != 0))
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
                if ((CashBankY1.Length > 0 && double.Parse(CashBankY1) != 0) && (StiY1.Length > 0 && double.Parse(StiY1) != 0))
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
            if ((NetIncomeY2.Length > 0 && double.Parse(NetIncomeY2) != 0) && (RevenueY2.Length > 0 && double.Parse(RevenueY2) != 0))
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
                if ((CashBankY2.Length > 0 && double.Parse(CashBankY2) != 0) && (StiY2.Length > 0 && double.Parse(StiY2) != 0))
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
            if ((NetIncomeY3.Length > 0 && double.Parse(NetIncomeY3) != 0) && (RevenueY3.Length > 0 && double.Parse(RevenueY3) != 0))
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
                if ((CashBankY3.Length > 0 && double.Parse(CashBankY3) != 0) && (StiY3.Length > 0 && double.Parse(StiY3) != 0))
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

            await Application.Current.MainPage.Navigation.PushAsync(new ZakatInstalmentPlanSuccessPage());

        }

        public void EnableDeclarationContinue()
        {
            if ((BankStatementsAttachmentsListViewData != null && BankStatementsAttachmentsListViewData.Count > 0) && (FinanceAttachmentsListViewData != null && FinanceAttachmentsListViewData.Count > 0))
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
                DeclarationButtonBackGroundColor = Color.FromHex(_isDeclarationEnabled ? "#d49504" : "#9EA4A9");



                RaisePropertyChanged("IsDeclarationEnabled");
            }
        }
        private Color _declarationButtonBackGroundColor = Color.FromHex("#d49504");
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
                RaisePropertyChanged("DeclarationButtonBackGroundColor");
            }
        }

        public async void VATInstalationTapped()
        {
            try
            {
                if (IsVatTermsChecked)
                {
                    await PopupNavigation.Instance.PopAsync();
                }
                else
                {

                    await _dialogService.ShowMessage(AppResources.BPInstructionsAndConditionsAlert, AppResources.Information);
                    // await showAlert("Please accept VAT Instructions and Conditions to continue");
                }
                // _navigationService.NavigateTo(App.ZakatInstalmentPlanPageView);
            }
            catch (GAZTUnlockAccountException ex)
            {

            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
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
                    GetZaktaInvoiceList();
                }
                else
                {
                    await _dialogService.ShowMessage(AppResources.ZakatInstalmentPleaseChooseOneReason, AppResources.Information);
                }

            }
            catch (GAZTUnlockAccountException ex)
            {

            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public async void showDialog(string msg)
        {
            await _dialogService.ShowMessage(msg, AppResources.Information);
        }

        public async void BillContinueBtnClicked()
        {
            try
            {
                double totalAmount = double.Parse(TotalAmountSAR.Replace(" SAR", ""));
                if (totalAmount == 0)
                {
                    Device.BeginInvokeOnMainThread(async () =>
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
                Device.BeginInvokeOnMainThread(async () =>
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
                    await _dialogService.ShowMessage(AppResources.ZZPleasefillthemandatoryfields + "(" + AppResources.ZakatYearOne + ", " + AppResources.ZakatYearTwo + ", " + AppResources.ZakatYearThree + ")", AppResources.Information);
                    return;
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



                EnableAttachmentsView();


            }
            catch (GAZTUnlockAccountException ex)
            {

            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }


        public async void OutletContinueBtnClicked()
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
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }
        public async void DisplayDetailsButtonClicked()
        {
            try
            {

                EnableInstalmentsScheduleView();
                // EnableSummaryView();
                //PopulateSummaryReasonData();
            }
            catch (GAZTUnlockAccountException ex)
            {

            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
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
                if ((BankStatementsAttachmentsListViewData != null && BankStatementsAttachmentsListViewData.Count > 0) && (FinanceAttachmentsListViewData != null && FinanceAttachmentsListViewData.Count > 0))
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
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }
        public async void StatementsContinueBtnClicked()
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
                Device.BeginInvokeOnMainThread(async () =>
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
        public async void OnSaveDraftClicked() {
         
            try
            {
                Device.BeginInvokeOnMainThread(() =>
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


                    ZakatInstalments.d.Operation = "05";

                        ZakatInstalments.d.Status = "E0013";

                   


                    if (!isDraftClicked)
                    {
                        isDraftClicked = true;
                        ZakatInstalments = await SubmitClicked();

                        if (ZakatInstalments != null && ZakatInstalments.d != null)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                             

                                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                                headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                                headerAmountInfo.IsLinkAvailable = false;
                                headerAmountInfo.Message = string.Format(AppResources.ZakatDraftSaved, "  " + ZakatInstalments.d.Fbnum);

                                headerWithInfos.Add(headerAmountInfo);

                                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                                newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                                PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));


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
                                    headerAmountInfo.Message = string.Format(AppResources.ZZSomethingwentwrong, "  " + ZakatInstalments.d.Fbnum);
                                    headerWithInfos.Add(headerAmountInfo);
                                    newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                                    newDesignPopUp.HeaderWithInfos = headerWithInfos;
                                    newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                                    PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

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

                                    PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                                    WebServiceManager.ErrorMessageForVAT = string.Empty;
                                });
                            }
                            //Device.BeginInvokeOnMainThread(async () =>
                            //{
                            //    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                            //});
                        }
                    }

                  
                });
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsNewLoading = false;
                });
            }
            catch (Exception ex)
            {
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

            await PopupNavigation.Instance.PushAsync(new ShowVatInformationConfirmationPageView(newDesignPopUp));

        }

        public async void VATSetReturnVoidAsync()
        {
            try
            {
                Device.BeginInvokeOnMainThread(() =>
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


                    ZakatInstalments.d.Operation = "04";
                    ZakatInstalments.d.Status = "E0001";

                    if (!isDraftClicked)
                    {
                        isDraftClicked = true;
                        ZakatInstalments = await SubmitClicked();

                        if (ZakatInstalments != null && ZakatInstalments.d != null)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {


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

                                PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                                _navigationService.GoBack();


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

                                    PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));



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

                                    PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                                    WebServiceManager.ErrorMessageForVAT = string.Empty;
                                });
                            }
                            //Device.BeginInvokeOnMainThread(async () =>
                            //{
                            //    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                            //});
                        }
                    }


                });
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsNewLoading = false;
                });
            }
            catch (Exception ex)
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

                ZakatInstalments.d.DecCb = "X";
                ZakatInstalments.d.OffPlanDur = "00";
                ZakatInstalments.d.OffPymntFreq = "00";
                ZakatInstalments.d.OffPymntFreq = "00";
                ZakatInstalments.d.Status = "E0001";
                ZakatInstalments.d.StepNumber = "04";
                ZakatInstalments.d.Operation = "58";
                if (!isSubmitClicked)
                {
                    isSubmitClicked = true;
                    ZakatInstalments = await SubmitClicked();

                    if (ZakatInstalments != null && ZakatInstalments.d != null)
                    {

                        ZakatReferanceNumber = ZakatInstalments.d.Fbnum;

                        EnableSucessScreenAsync();
                    }
                }


                //Display Success Screen

            }
            catch (GAZTUnlockAccountException ex)
            {

            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }



        public async void OnZakatInstalmentReasonClicked()
        {
            try
            {
                ObservableCollection<string> ZakatreasonData = new ObservableCollection<string>();
                ZakatreasonData.Add(AppResources.TinDeregistrationReasonBankruptcy);
                ZakatreasonData.Add(AppResources.TinDeregistrationReasonDeath);
                ZakatreasonData.Add(AppResources.TinDeregistrationReasonLiquidation);
                ZakatreasonData.Add(AppResources.TinDeregistrationReasonEstablishmentToCompany);

                //await PopupNavigation.Instance.PushAsync(new PickerPageView(ZakatreasonData));
            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async void NewAttachmentClicked()
        {
            try
            {
                //await PopupNavigation.Instance.PushAsync(new FilesUploadPopUpPageView(AttachmentsListViewData.ToList(), WhichAttachment.VATInstalment, VatInstalments.d.ReturnIdz));

            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async void SummaryInstallmentDetailsBtnClicked()
        {
            try
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Instalment details schedule is displayed here.", "OK");



            }
            catch (GAZTUnlockAccountException ex)
            {



            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        //OutletContinueButtonTapped
        //AttachmentsContinueButtonTapped
        //DeclarationContinueButtonTapped
        //SummaryContinueButtonTapped

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
            _bankStatementsAttachment = true;
            if (BankStatementsAttachmentsListViewData == null)
            {
                BankStatementsAttachmentsListViewData = new ObservableCollection<Attachment>();
            }
            try
            {
                
                    await PopupNavigation.Instance.PushAsync(new FilesUploadPopUpPageView(
                        BankStatementsAttachmentsListViewData.ToList(),
                        WhichAttachment.ZakatInstalmentBankStatements, ZakatInstalments.d.ReturnId));
                
            


            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async void FinanceAttachmentClicked()
        {
            _bankStatementsAttachment = false;
            if (FinanceAttachmentsListViewData == null)
            {
                FinanceAttachmentsListViewData = new ObservableCollection<Attachment>();
            }
            try
            {
                
                    await PopupNavigation.Instance.PushAsync(new FilesUploadPopUpPageView(
                    FinanceAttachmentsListViewData.ToList(),
                    WhichAttachment.ZakatInstalmentFinance, ZakatInstalments.d.ReturnId));
                


            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                RaisePropertyChanged("FinanceAttachmentsListViewData");
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
                RaisePropertyChanged("BankStatementsAttachmentsListViewData");
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
            else {
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
                        invoiceList = await WebServiceManager.GetZakatInvoicesList(IsZakat, App.selectedZakatItem);

                        if (invoiceList != null && invoiceList.d != null)
                        {


                            var totalAmount = 0.0;
                            var totalAmountDue = 0.0;

                            ZakatInvoicesList = invoiceList.d.results;
                            for (int i = 0; i < ZakatInvoicesList.Count; i++)
                            {
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


                            if(ZakatInstalments.d.DpAmt != null && Double.Parse(ZakatInstalments.d.DpAmt) > 0) {

                                DownPaymentSliderValue = Double.Parse(ZakatInstalments.d.DpAmt);
                            }

                            if(ZakatInstalments.d.TotAmt != null && Double.Parse(ZakatInstalments.d.TotAmt) > 0) {
                                TotalAmountSAR = ZakatInstalments.d.TotAmt;
                            }

                            if (ZakatInstalments.d.PlanDur != null && int.Parse(ZakatInstalments.d.PlanDur) > 0)
                            {
                                NumberOFInstalmentSliderValue = int.Parse(ZakatInstalments.d.PlanDur);
                            }

                            if (ZakatInstalments.d.PymntFreq != null && Double.Parse(ZakatInstalments.d.PymntFreq) > 0)
                            {
                                SelectedFrequencyType = ZakatInstalments.d.PymntFreq;
                                MessagingCenter.Send<Object, string>(this, "SelectedFrequencyType", SelectedFrequencyType);



                            }




                            //if (IsZakat)
                            //{
                            //    ZakatInstalments.d.InstReqFor = "01";

                            //}
                            //else
                            //{
                            //    ZakatInstalments.d.InstReqFor = "02";
                            //}
                            //if (IDType == AppResources.ZakatFinancialCrisis)
                            //{
                            //    ZakatInstalments.d.InstReqReason = "01";
                            //}
                            //else if (IDType == AppResources.ZakatDisputeInFavorOfGAZT)
                            //{
                            //    ZakatInstalments.d.InstReqReason = "02";
                            //}
                            //else if (IDType == AppResources.ZakatOtherReason)
                            //{
                            //    ZakatInstalments.d.InstReqReason = "03";
                            //}
                            //else
                            //{
                            //    ZakatInstalments.d.InstReqReason = "01";
                            //}

                            //MessagingCenter.Send<Object, Boolean>(this, "InvoiceBillsLoaded", true);


                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                _navigationService.GoBack();
                            });
                        }
                        IsLoading = false;
                    }
                    catch (GAZTVATRegistrationInProcessException ex)
                    {
                        throw ex;
                    }
                    catch (InternetException ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
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

                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });

            }
            catch (Exception ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
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
                    ZakatInstalmentPlanResponse vATInstalment = null;
                    try
                    {
                        ZakatInstalments = await WebServiceManager.GetZakatInstalmentPostData(App.selectedZakatItem);

                        if (ZakatInstalments != null)
                        {

                            if (IsZakat)
                            {
                                await PopupNavigation.Instance.PushAsync(new InstructionsBottomPopUpView(instructionString: AppResources.ZakatInstructions, checkBoxString: AppResources.ZakatInstructionsCheckBoxDesc, continueString: AppResources.ZakatInstalmetPlanTitle,
                   _dialogType: ZakatInstalmentViewModel.InstructionsBottomPopUpViewModel.DialogType
                       .Instructions));
                            }
                            else
                            {
                                await PopupNavigation.Instance.PushAsync(new InstructionsBottomPopUpView(instructionString: AppResources.ZakatInstructions, checkBoxString: AppResources.ZakatInstructionsCheckBoxDesc, continueString: AppResources.ZakatInstalmetSelectTypeIncomeTax,
                   _dialogType: ZakatInstalmentViewModel.InstructionsBottomPopUpViewModel.DialogType
                       .Instructions));

                            }

                            BindZakatBillsList();
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                _navigationService.GoBack();
                            });
                        }
                        IsLoading = false;
                    }
                    catch (GAZTVATRegistrationInProcessException ex)
                    {
                        throw ex;
                    }
                    catch (InternetException ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
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
            catch (GAZTVATRegistrationInProcessException ex)
            {
                //await Task.Run(() =>
                //{

                //});
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });

            }
            catch (Exception ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
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
                Device.BeginInvokeOnMainThread(async () =>
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
                //if (IsInstrunctionChecked)
                //{
                //    VATInstalmentPlanObject.d.Xstep1Conf = "1";
                //}
                //else
                //{
                //    VATInstalmentPlanObject.d.Xstep1Conf = "0";
                //}
                //VatInstalments.d.Xstep1Conf = "1";
                //VatInstalments.d.Xstep2Conf = "1";
                if (NoOfInstalments == 0)
                {

                    // VatInstalments.d.Noofinstallment = "2";
                }
                else
                {

                    // VatInstalments.d.Noofinstallment = NoOfInstalments.ToString();
                }

                //Double dueAmount = Double.Parse(VatInstalments.d.TotInvAmt) + Double.Parse(VatInstalments.d.Peneltyamt);
                //VatInstalments.d.Totdueamt = Math.Round(dueAmount, 2).ToString();
                // VatInstalments.d.UserTypz = "TP";

                //Step 5
                //if (IsDeclarationChecked)
                //{
                //    VATInstalmentPlanObject.d.Xstep2Conf = "1";
                //}
                //else
                //{
                //    VATInstalmentPlanObject.d.Xstep2Conf = "0";
                //}


            }
            catch (Exception ex)
            {

            }


        }
        #endregion

        #region MAP_VAT_RequestObject

        public ZakatInstalmentPlanRequest BuildRequestObject()
        {
            ZakatInstalmentPlanRequest _postData = new ZakatInstalmentPlanRequest();



            _postData.Fbguid = ZakatInstalments.d.Fbguid;
            _postData.UserTyp = "TP";
            _postData.TxnTp = ZakatInstalments.d.TxnTp;
            _postData.MobNo = ZakatInstalments.d.MobNo;
            _postData.FormGuid = ZakatInstalments.d.FormGuid;
            _postData.Fbnum = "";
            _postData.DataVersion = ZakatInstalments.d.DataVersion;
            _postData.Operation = ZakatInstalments.d.Operation;
            _postData.Euser = ZakatInstalments.d.Euser;
            _postData.StepNumber = ZakatInstalments.d.StepNumber;
            _postData.Email = ZakatInstalments.d.Email;
            _postData.Officer = ZakatInstalments.d.Officer;
            _postData.Langz = ZakatInstalments.d.Langz;
            _postData.Status = ZakatInstalments.d.Status;
            _postData.SuAmt = ZakatInstalments.d.SuAmt;
            _postData.SuAmtFg = ZakatInstalments.d.SuAmtFg;
            _postData.Formproc = ZakatInstalments.d.Formproc;
            _postData.Tin = ZakatInstalments.d.Tin;
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
            _postData.Sopbel = ZakatInstalments.d.Sopbel;
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

            if (ZakatInstalments.d.AttachSet.results == null)
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
                _postData.AttachSet = ZakatInstalments.d.AttachSet.results.ToArray();

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
            if (ZakatInstalments.d.FnDtlSet.results == null)
            {


                _postData.FnDtlSet = new FnDtlSetObject[0];

            }
            else
            {
                _postData.FnDtlSet = ZakatInstalments.d.FnDtlSet.results.ToArray();

            }


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
                        //var jsonDateTime = JsonConvert.SerializeObject(dt, microsoftDateFormatSettings);
                        var jsonDateTime = JsonConvert.SerializeObject(dt.Date, microsoftDateFormatSettings);
                        string[] dateList = jsonDateTime.Split('+');
                        jsonDateTime = dateList[0].Replace("\"\\", "");
                        jsonDateTime = jsonDateTime + ")/";
                        invoicesList[i].DueDt = jsonDateTime;

                    }
                }




                _postData.invDtlsSet = invoicesList.ToArray();
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




                //setDATA();
                //VaiInstalmentRequest vatInstalments1 = new VaiInstalmentRequest();
                //vatInstalments1.Xstep1Conf = "1";
                //vatInstalments1.Xstep2Conf = "2";
                //vatInstalments1.Noofinstallment = "4";
                //vatInstalments1.Operationz = "10";

                response = await WebServiceManager.SaveZakatInstalmentData(request);
                PopToRootPage();
                if (response != null)
                {
                    try
                    {
                        if (response != null)
                        {
                            //if (response.d.Submitz.Equals("X"))
                            //{
                            //    string number = response.d.Fbnumz;
                            //    string displayMessage = AppResources.VATRSuccessFullVoidMessage + " " + number;
                            //    await _dialogService.ShowMessage(displayMessage, AppResources.Information);
                            //    _navigationService.GoBack();
                            //}
                            //if (response.d.Submitz.Equals("05"))
                            //{
                            //    //  string number = response.d.Fbnumz;
                            //    string displayMessage = AppResources.VATRSaveasdraftMessage;
                            //    await _dialogService.ShowMessage(displayMessage, AppResources.Information);

                            //}




                            // VatInstalments = response;

                            //Set data after api call 
                            //setDataAfterSubmitAPIAsync(response);

                        }
                        IsLoading = false;
                        return response;

                    }
                    catch (Exception ex)
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
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    //_navigationService.GoBack();

                });
                return response;
            }

            catch (Exception ex)
            {
                return response;
            }

        }

        #endregion
    }
}
