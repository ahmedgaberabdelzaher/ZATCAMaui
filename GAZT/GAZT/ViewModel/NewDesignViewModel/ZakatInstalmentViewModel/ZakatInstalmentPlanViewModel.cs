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
using EGAZT.Views.NewDesign.GenericPickers;
using EGAZT.Views.NewDesign.ZakatInstalmentPlan;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using static EGAZT.Models.ZakatInstalationModels.ZakatInstalmentPlanRequest;

namespace EGAZT.ViewModel.NewDesignViewModel.ZakatInstalmentPlanViewModel
{
    public class ZakatInstalmentPlanViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        private bool _isLoading = true;
        //public System.Collections.Generic.List<VATResults4> selectedList = new System.Collections.Generic.List<VATResults4>();
        public System.Collections.Generic.List<ZakatInvoicesResult> selectedList = new System.Collections.Generic.List<ZakatInvoicesResult>();
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
        private List<ZakatInvoicesResult> _zakatInvoicesList;
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


        private bool _isNoDataLableVisible = false;
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
        public ICommand GoBackClick { get; set; }

        int noOfInstalments = 5;
        double minInstalments = 2;
        double maxInstalments = 12;
        double downPaymentAmount = 400.00;
        double periodicInstalment = 0.0;
        double minAmount = 400.0;
        double maxAmount = 2000000.0;
        string inputData = "";
        string totalAmountSAR = "0.00 SAR";
        string _vATDueAmount = "0.00";
        string _vATPenalityAmount = "0.00";
        string _vATBillDueAmount = "0.00 SAR";
        string _vATLiabilityAmount = "0.00 SAR";
        int selectedPage = (int)PagesEnum.ZakatSelectionView;
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


        #endregion



        #region Properties

        private bool _isBackButtonVisible = true;
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

        private bool _isSelectionViewEnabled = true;
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


        private int _selectedOutletOptionIndex;
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
        private bool _isDisplayInstalmentsVisible = false;
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



        private bool _isAgreementViewEnabled = false;
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

        private bool _isOutletViewEnabled = false;
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
        private bool _isBillsViewEnabled = false;
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
        private bool _isVATBillsViewEnabled = false;
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

        private bool _InstalmentPlanAgreementsVisible = false;
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

        private bool _isAttachmentsViewEnabled = false;
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
        private bool _isStatementViewEnabled = false;
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

        private bool _isSummaryViewEnabled = true;
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

        private double _numberOFInstalmentSliderValue = 0;
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

        private bool _isVATAmountVisible = false;
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

        private bool _isZakatSelected = false;
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

        private bool _isIncomeTaxViewEnabled = false;
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

        private bool _isSubIncomeTaxViewEnabled = false;
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


        private CorrespondenceFiltersModel _selectedFilterZakat = null;
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

        private List<CorrespondanceModel> _listZAKATCorrespondance = null;
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

        private string _txtSelectedStatusZakat = string.Empty;
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


        private CorrespondenceFiltersModel _selectedFilterZakatPrev = null;
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
        private List<CorrespondenceFiltersModel> _corresFilterZakat;
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

        private List<CorrespondanceModel> _subListZAKATCorrespondance = null;
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

        private string _subTxtSelectedStatusZakat = string.Empty;
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

        private List<CorrespondenceFiltersModel> _subCorresFilterZakat;
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

        private int? _subSetSelectedIndexZakat = 0;
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

            foreach (var bill in ZakatInstalments.d.Z_INVOICE_UI5Set.results)
            {
                selectedBillsList.Add(new ZakatSelectBillModel()
                {
                    billNumber = "Bill 1",
                    amount = "1,200.00 SAR",
                    saadNumber = "1234568798",
                    taxPeriod = "2019-2020",
                    isSelected = false,
                    billType = AppResources.ZakatInstalmetSelectTypeZakat
                });
            }

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
            SummaryAttachmentsListViewData = new ObservableCollection<InstalmentAgreementAttachmentsModel>();
            SummaryAttachmentsListViewData.Add(new InstalmentAgreementAttachmentsModel
            {
                FieldTitle = "Last 3 months",
                FieldSubTitle = AppResources.TinDeregistration20MB,
                AttachmentName = "File1.pdf",
                IsAttachmentAttached = true
            });
            SummaryAttachmentsListViewData.Add(new InstalmentAgreementAttachmentsModel
            {
                FieldTitle = "Last 3 years",
                FieldSubTitle = AppResources.TinDeregistration20MB,
                AttachmentName = "File2.pdf",
                IsAttachmentAttached = true
            });

        }

        public ObservableCollection<TinDeregestrationAttachmentsModel> InstallmentAgreementListViewData { get; private set; }

        public void PopulateSummaryInstallmentAgreement()
        {
            InstallmentAgreementListViewData = new ObservableCollection<TinDeregestrationAttachmentsModel>();
            InstallmentAgreementListViewData.Add(new TinDeregestrationAttachmentsModel
            {
                FieldTitle = "Frequency",
                FieldSubTitle = AppResources.TinDeregistration20MB,
                AttachmentName = "Monthly",
                IsAttachmentAttached = true
            });
            InstallmentAgreementListViewData.Add(new TinDeregestrationAttachmentsModel
            {
                FieldTitle = "Number of Installments",
                FieldSubTitle = AppResources.TinDeregistration20MB,
                AttachmentName = "5",
                IsAttachmentAttached = true
            });
            InstallmentAgreementListViewData.Add(new TinDeregestrationAttachmentsModel
            {
                FieldTitle = "Down Payment Amount",
                FieldSubTitle = AppResources.TinDeregistration20MB,
                AttachmentName = "1,000,000.00 SAR",
                IsAttachmentAttached = true
            });

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
                    EnableStatementsView();
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
            noOfInstalments = 5;
            minInstalments = 2;
            maxInstalments = 12;
            downPaymentAmount = 400.00;
            periodicInstalment = 0.0;
            minAmount = 400.0;
            maxAmount = 2000000.0;
            inputData = "";
            totalAmountSAR = "0.00 SAR";
            _vATDueAmount = "0.00";
            _vATPenalityAmount = "0.00";
            _vATBillDueAmount = "0.00 SAR";
            _vATLiabilityAmount = "0.00 SAR";
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
            _dialogService = dialogService;
            GoBackClick = new Command(async () =>
            {
                Backnavigations();
            });

            CloseClick = new Command(async () =>
            {
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


            InstallmentDetailsBtnTapped = new Command(async () =>
            {
                EnableDisplayInstalmentsView();
                //                EnableInstalmentsScheduleView();
            });

            ZakatInstalmentPlanModel = new ZakatInstalmentPlanModel();
            SelectedOutletOption = new ZakatInstalmentPlanModel();
            //LoadVatInstalmentData();
            AddOutletDecisionOptions();
            AddFrequencyOptions();
            //PopulateAttachmentsListViewTemplate();

            //PopulateSummaryReasonData();
            //PopulateSummaryInstallmentAgreement();
            //PopulateSummaryAttachments();
            //PopulateSummaryInstallmentAgreement();
            //SetStatusPickerItem();
            //SetSubStatusPickerItem();

            //PopulateInstalmentsListViewTemplate();

            // EnableBillView();
        }

        public async void showInstructionsDialog()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new InstructionsBottomPopUpView(instructionString: AppResources.ZakatInstructions, checkBoxString: AppResources.ZakatInstructionsCheckBoxDesc, continueString: "Zakat Instalment",
                    _dialogType: ZakatInstalmentViewModel.InstructionsBottomPopUpViewModel.DialogType
                        .Instructions));
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

        public void AddOutletDecisionOptions()
        {
            OutletDecisionOptions = new ObservableCollection<ZakatInstalmentPlanModel>();
            OutletDecisionOptions.Add(new ZakatInstalmentPlanModel
            {
                ActiveOutletDecisionOptions = AppResources.ZakatFinancialCrisis,
                ActiveOutletDecisionOptionsIsSelected = false
            });
            OutletDecisionOptions.Add(new ZakatInstalmentPlanModel
            {
                ActiveOutletDecisionOptions = AppResources.ZakatDisputeInFavorOfGAZT,
                ActiveOutletDecisionOptionsIsSelected = false
            });
            OutletDecisionOptions.Add(new ZakatInstalmentPlanModel
            {
                ActiveOutletDecisionOptions = AppResources.ZakatOtherReason,
                ActiveOutletDecisionOptionsIsSelected = false
            });

        }

        public void AddFrequencyOptions()
        {
            ZakatAgreementOptions = new ObservableCollection<InstalmentAgreementFrequencyModel>();
            ZakatAgreementOptions.Add(new InstalmentAgreementFrequencyModel
            {
                FrequencyOptions = AppResources.ZakatInstalmetMonthly,
                IsSelected = true
            });
            ZakatAgreementOptions.Add(new InstalmentAgreementFrequencyModel
            {
                FrequencyOptions = AppResources.ZakatInstalmetQuarterly,
                IsSelected = false
            });
            ZakatAgreementOptions.Add(new InstalmentAgreementFrequencyModel
            {
                FrequencyOptions = AppResources.ZakatInstalmetHalfYearly,
                IsSelected = false
            });
            ZakatAgreementOptions.Add(new InstalmentAgreementFrequencyModel
            {
                FrequencyOptions = AppResources.ZakatInstalmetYearly,
                IsSelected = false
            });
        }
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
            selectedPage = (int)PagesEnum.ZakatSelectionView;

        }
        public void BindVATSelectionView()
        {

            if (ZakatInstalments != null && ZakatInstalments.d != null)
            {

                //VATDueAmount = ZakatInstalments.d.TotInvAmt;
                //VATPenalityAmount = ZakatInstalments.d.Peneltyamt;
                //VATBillDueAmount = ZakatInstalments.d.Totdueamt;
                //VATLiabilityAmount = ZakatInstalments.d.Totliablityamt;
            }
        }

        public void BindBillsListView()
        {


            if (ZakatInstalments.d.Z_INVOICE_UI5Set.results != null)
            {

                // BillsListVAT = ZakatInstalments.d.Z_INVOICE_UI5Set.results;
            }

        }

        public void BindStatementsView()
        {

            /*
            if (ZakatInstalments.d.VTISSet.results != null)
            {
                StatementList = ZakatInstalments.d.VTISSet.results;

                for (int i = 0; i < StatementList.Length; i++)
                {
                    DateTime dateStart = new DateTime();
                    CultureInfo cultureInfo = new CultureInfo("ar-SA");
                    string apiDate = @"""" + StatementList[i].Faedn + @"""";
                    dateStart = JsonConvert.DeserializeObject<DateTime>(apiDate);

                    //if (App.IsArabic)
                    //{

                    //    dateStart = DateTime.ParseExact(dt.ToString(), "yyyy/MM/dd", cultureInfo.DateTimeFormat, DateTimeStyles.AllowInnerWhite);
                    //}
                    //else
                    //{

                    //    dateStart = DateTime.ParseExact(dt.ToString(), "dd/MM/yyyy", cultureInfo.DateTimeFormat, DateTimeStyles.AllowInnerWhite);
                    //}

                    GregorianCalendar hjCalendar = new GregorianCalendar();
                    int year = hjCalendar.GetYear(dateStart);
                    int month = hjCalendar.GetMonth(dateStart);
                    int day = hjCalendar.GetDayOfMonth(dateStart);

                    string dateStr = string.Format("{0:00}/{1}/{2}", day, month, year);

                    StatementList[i].Faedn = dateStr;

                    string dt1 = string.Empty;
                    string[] dts = null;
                    dts = StatementList[i].Faedn.Split('/');
                    dt1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];
                    StatementList[i].Faedn = dt1;

                }

                /*DateTime dateStart = new DateTime();


                CultureInfo cultureInfo = new CultureInfo("ar-SA");

                if (App.IsArabic)
                {

                    dateStart = DateTime.ParseExact(myBills[i].Faednar, "yyyy/MM/dd", cultureInfo.DateTimeFormat, DateTimeStyles.AllowInnerWhite);
                }
                else
                {

                    dateStart = DateTime.ParseExact(myBills[i].Faednar, "dd/MM/yyyy", cultureInfo.DateTimeFormat, DateTimeStyles.AllowInnerWhite);
                }


                GregorianCalendar hjCalendar = new GregorianCalendar();
                int year = hjCalendar.GetYear(dateStart);
                int month = hjCalendar.GetMonth(dateStart);
                int day = hjCalendar.GetDayOfMonth(dateStart);

                string dateStr = string.Format("{0:00}/{1}/{2}", day, month, year);

                myBills[i].Faednar = dateStr;

                string dt = string.Empty;
                string[] dts = null;

                dts = myBills[i].Faednar.Split('/');
                dt = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
                myBills[i].Faednar = dt;
                


                VATBillDueAmount = ZakatInstalments.d.ATotalAmt;

            }*/
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
            selectedPage = (int)PagesEnum.InstalmentPlanAgreementsVisible;

        }

        public async Task EnableSucessScreenAsync()
        {

            await Application.Current.MainPage.Navigation.PushAsync(new ZakatInstalmentPlanSuccessPage());

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

                    await _dialogService.ShowMessage("Please accept VAT Instructions and Conditions to continue", "Alert");
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
                EnableAgreementView();
                return;

                /* if (TotalAmountSAR.Equals("0.00 SAR"))
                 {
                     await _dialogService.ShowMessage("Please Select atleast one bill to continue", "Alert");
                 }
                 else
                 {
                     EnableAgreementView();
                 }*/
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
        public async void AggrementContinueBtnClicked()
        {
            try
            {
                //EnableAttachmentsView();


                setDATA();


                var amount = TotalAmountSAR.Replace(" SAR", "").Replace(",", "");
                ZakatInstalments.d.ATotalAmt = amount;


                // ZakatInstalments = await SubmitClicked();


                EnableStatementsView();
                BindStatementsView();

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


                EnableSummaryView();
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



        public async void SummaryContinueBtnClicked()
        {
            try
            {
                //Display Success Screen
                EnableSucessScreenAsync();
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

            AttachmentsListViewData = new ObservableCollection<Attachment>();

            foreach (Attachment attachemnt in attachments)
            {
                AttachmentsListViewData.Add(attachemnt);
            }

        }

        public void updateInstalmentsOnSlider(InstalmentAgreementFrequencyModel frequencyModel)
        {
            if (frequencyModel.FrequencyOptions == AppResources.ZakatInstalmetMonthly)
            {
                MinInstalments = 1;
                MaxInstalments = 36;

            }
            else if (frequencyModel.FrequencyOptions == AppResources.ZakatInstalmetQuarterly)
            {
                MinInstalments = 1;
                MaxInstalments = 12;
            }
            else if (frequencyModel.FrequencyOptions == AppResources.ZakatInstalmetHalfYearly)
            {
                MinInstalments = 1;
                MaxInstalments = 6;
            }
            else if (frequencyModel.FrequencyOptions == AppResources.ZakatInstalmetYearly)
            {
                MinInstalments = 1;
                MaxInstalments = 3;
            }
            NumberOFInstalmentSliderValue = 1;
        }

        #endregion

        #region ZakatInvoiceList

        public async Task GetZaktaInvoiceList()
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

                    ZakatInvoiceList invoiceList = null;
                    try
                    {
                        invoiceList = await WebServiceManager.GetZakatInvoicesList();

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
                            MessagingCenter.Send<Object, Boolean>(this, "InvoiceBillsLoaded", true);

                            TotalAmountSAR = string.Format("{0:N2}", totalAmount) + " SAR";
                            VATBillDueAmount = string.Format("{0:N2}", totalAmountDue) + " SAR";
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
                        ZakatInstalments = await WebServiceManager.GetZakatInstalmentPostData();

                        if (ZakatInstalments != null && ZakatInstalments.d != null)
                        {

                            /* for (int i = 0; i < ZakatInvoicesList.Count; i++)
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
                                 ZakatInvoicesList[i].DueDt  = dt1;

                             }*/

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

            _postData.d = new ZakatInstalmentReuqest();

            _postData.d.Percentage = ZakatInstalments.d.Percentage;
            _postData.d.UserTin = ZakatInstalments.d.UserTin;
            _postData.d.Euser = ZakatInstalments.d.Euser;
            _postData.d.Fbguid = ZakatInstalments.d.Fbguid;
            _postData.d.Auditorz = ZakatInstalments.d.Auditorz;
            _postData.d.Langz = ZakatInstalments.d.Langz;
            _postData.d.Taxpayerz = ZakatInstalments.d.Taxpayerz;
            _postData.d.Savez = ZakatInstalments.d.Savez;
            _postData.d.Fbnumz = ZakatInstalments.d.Fbnumz;
            _postData.d.PeriodKeyz = ZakatInstalments.d.PeriodKeyz;
            _postData.d.Submitz = ZakatInstalments.d.Submitz;
            _postData.d.AAgree = ZakatInstalments.d.AAgree;
            _postData.d.ADwPaymentReqLetter = ZakatInstalments.d.ADwPaymentReqLetter;
            _postData.d.AEffectiveCalTp = ZakatInstalments.d.AEffectiveCalTp;
            _postData.d.Euser1 = ZakatInstalments.d.Euser1;
            _postData.d.FbtypDescr = ZakatInstalments.d.FbtypDescr;
            _postData.d.FormGuid = ZakatInstalments.d.FormGuid;

            _postData.d.ZauditorFlag = ZakatInstalments.d.ZauditorFlag;
            _postData.d.ANoOfInstTp = ZakatInstalments.d.ANoOfInstTp;
            _postData.d.ARev1yrTbFg = ZakatInstalments.d.ARev1yrTbFg;
            _postData.d.SavNot = ZakatInstalments.d.SavNot;
            _postData.d.ARev2yrTbFg = ZakatInstalments.d.ARev2yrTbFg;
            _postData.d.ALegalEnty1 = ZakatInstalments.d.ALegalEnty1;
            _postData.d.ARev3yrTbFg = ZakatInstalments.d.ARev3yrTbFg;
            _postData.d.Rejectz = ZakatInstalments.d.Rejectz;
            _postData.d.ANi2yrTbFg = ZakatInstalments.d.ANi2yrTbFg;
            _postData.d.CaseGuid = ZakatInstalments.d.CaseGuid;
            _postData.d.ANi3yrTbFg = ZakatInstalments.d.ANi3yrTbFg;
            _postData.d.AEffectiveCalTp = ZakatInstalments.d.AEffectiveCalTp;
            _postData.d.AStep = ZakatInstalments.d.AStep;
            _postData.d.ACb1yrTbFg = ZakatInstalments.d.ACb1yrTbFg;
            _postData.d.ACb2yrTbFg = ZakatInstalments.d.ACb2yrTbFg;

            _postData.d.ALegalEnty2 = ZakatInstalments.d.ALegalEnty2;
            _postData.d.ACb3yrTbFg = ZakatInstalments.d.ACb3yrTbFg;
            _postData.d.ASi1yrTbFg = ZakatInstalments.d.ASi1yrTbFg;
            _postData.d.ASi2yrTbFg = ZakatInstalments.d.ASi2yrTbFg;
            _postData.d.ACb2yrTbFg = ZakatInstalments.d.ACb2yrTbFg;
            _postData.d.RegIdz = ZakatInstalments.d.RegIdz;
            _postData.d.ASi3yrTbFg = ZakatInstalments.d.ASi3yrTbFg;
            _postData.d.ATa1yrTbFg = ZakatInstalments.d.ATa1yrTbFg;
            _postData.d.ATa2yrTbFg = ZakatInstalments.d.ATa2yrTbFg;
            _postData.d.ATa3yrTbFg = ZakatInstalments.d.ATa3yrTbFg;
            _postData.d.Fbnum = ZakatInstalments.d.Fbnum;
            _postData.d.ATl1yrTbFg = ZakatInstalments.d.ATl1yrTbFg;

            _postData.d.ATin = ZakatInstalments.d.ATin;
            _postData.d.ATl2yrTbFg = ZakatInstalments.d.ATl2yrTbFg;
            _postData.d.ATaxpayerNm = ZakatInstalments.d.ATaxpayerNm;
            _postData.d.ATl3yrTbFg = ZakatInstalments.d.ATl3yrTbFg;
            _postData.d.ADeb1yrTbFg = ZakatInstalments.d.ADeb1yrTbFg;
            _postData.d.ATelNo = ZakatInstalments.d.ATelNo;
            _postData.d.ADeb2yrTbFg = ZakatInstalments.d.ADeb2yrTbFg;
            _postData.d.AMobNo = ZakatInstalments.d.AMobNo;


            _postData.d.ADeb3yrTbFg = ZakatInstalments.d.ADeb3yrTbFg;
            _postData.d.AEmail = ZakatInstalments.d.AEmail;
            _postData.d.ACr1yrTbFg = ZakatInstalments.d.ACr1yrTbFg;
            _postData.d.AInstReqFor = ZakatInstalments.d.AInstReqFor;
            _postData.d.ACr2yrTbFg = ZakatInstalments.d.ACr2yrTbFg;
            _postData.d.AInstReqReason = ZakatInstalments.d.AInstReqReason;
            _postData.d.ABnkStat3mhChk = ZakatInstalments.d.ABnkStat3mhChk;

            _postData.d.ACr3yrTbFg = ZakatInstalments.d.ACr3yrTbFg;
            _postData.d.AFinStat3yrChk = ZakatInstalments.d.AFinStat3yrChk;
            _postData.d.ARe1yrTbFg = ZakatInstalments.d.ARe1yrTbFg;
            _postData.d.AOtherDocChk = ZakatInstalments.d.AOtherDocChk;
            _postData.d.ARe2yrTbFg = ZakatInstalments.d.ARe2yrTbFg;
            _postData.d.AHoldFinStat = ZakatInstalments.d.AHoldFinStat;

            _postData.d.ARe3yrTbFg = ZakatInstalments.d.ARe3yrTbFg;
            _postData.d.ADpAmtFg = ZakatInstalments.d.ADpAmtFg;
            _postData.d.AItTb = ZakatInstalments.d.AItTb;
            _postData.d.AOneYrTb = ZakatInstalments.d.AOneYrTb;
            _postData.d.ATwoYrTb = ZakatInstalments.d.ATwoYrTb;
            _postData.d.AThreeYrTb = ZakatInstalments.d.AThreeYrTb;
            _postData.d.ADpAmt = ZakatInstalments.d.ADpAmt;
            _postData.d.APlanDurNo = ZakatInstalments.d.APlanDurNo;
            _postData.d.APlanDurPeri = ZakatInstalments.d.APlanDurPeri;
            _postData.d.APaymentFreq = ZakatInstalments.d.APaymentFreq;
            _postData.d.ACoPlanDurPeri = ZakatInstalments.d.ACoPlanDurPeri;
            _postData.d.ACoPaymentFreq = ZakatInstalments.d.ACoPaymentFreq;
            _postData.d.ADpRequ = ZakatInstalments.d.ADpRequ;
            _postData.d.ADpPer = ZakatInstalments.d.ADpPer;
            _postData.d.ACoDpAmt = ZakatInstalments.d.ACoDpAmt;
            _postData.d.ADpRecAmt = ZakatInstalments.d.ADpRecAmt;
            _postData.d.AAppInstAmt = ZakatInstalments.d.AAppInstAmt;
            _postData.d.AInstDpAmt = ZakatInstalments.d.AInstDpAmt;

            _postData.d.ABalAmt = ZakatInstalments.d.ABalAmt;
            _postData.d.ACoBoRev = ZakatInstalments.d.ACoBoRev;
            _postData.d.ACoBoNm = ZakatInstalments.d.ACoBoNm;
            _postData.d.ACoBoRd = ZakatInstalments.d.ACoBoRd;
            _postData.d.ACmBoRev = ZakatInstalments.d.ACmBoRev;
            _postData.d.ACmBoNm = ZakatInstalments.d.ACmBoNm;
            _postData.d.ACmBoRd = ZakatInstalments.d.ACmBoRd;
            _postData.d.ACoHoRev = ZakatInstalments.d.ACoHoRev;
            _postData.d.ACoHoNm = ZakatInstalments.d.ACoHoNm;
            _postData.d.ACoHoRd = ZakatInstalments.d.ACoHoRd;

            _postData.d.ACmHoRev = ZakatInstalments.d.ACmHoRev;
            _postData.d.ACmHoNm = ZakatInstalments.d.ACmHoNm;
            _postData.d.ACmHoRd = ZakatInstalments.d.ACmHoRd;
            _postData.d.AMofApprChk = ZakatInstalments.d.AMofApprChk;
            _postData.d.AOtherSuppDocChk = ZakatInstalments.d.AOtherSuppDocChk;
            _postData.d.ARejReason = ZakatInstalments.d.ARejReason;
            _postData.d.ACoPlanDurNo = ZakatInstalments.d.ACoPlanDurNo;
            _postData.d.ARev1yrTb = ZakatInstalments.d.ARev1yrTb;
            _postData.d.ARev2yrTb = ZakatInstalments.d.ARev2yrTb;
            _postData.d.ARev3yrTb = ZakatInstalments.d.ARev3yrTb;
            _postData.d.ANi1yrTb = ZakatInstalments.d.ANi1yrTb;
            _postData.d.ANi2yrTb = ZakatInstalments.d.ANi2yrTb;

            _postData.d.ANi3yrTb = ZakatInstalments.d.ANi3yrTb;
            _postData.d.ACb1yrTb = ZakatInstalments.d.ACb1yrTb;
            _postData.d.ACb2yrTb = ZakatInstalments.d.ACb2yrTb;
            _postData.d.ACb3yrTb = ZakatInstalments.d.ACb3yrTb;
            _postData.d.ASi1yrTb = ZakatInstalments.d.ASi1yrTb;
            _postData.d.ASi2yrTb = ZakatInstalments.d.ASi2yrTb;
            _postData.d.ASi3yrTb = ZakatInstalments.d.ASi3yrTb;
            _postData.d.ATa1yrTb = ZakatInstalments.d.ATa1yrTb;

            _postData.d.ATa2yrTb = ZakatInstalments.d.ATa2yrTb;
            _postData.d.ATa3yrTb = ZakatInstalments.d.ATa3yrTb;
            _postData.d.ATl1yrTb = ZakatInstalments.d.ATl1yrTb;
            _postData.d.ATl2yrTb = ZakatInstalments.d.ATl2yrTb;
            _postData.d.ATl3yrTb = ZakatInstalments.d.ATl3yrTb;
            _postData.d.ADeb1yrTb = ZakatInstalments.d.ADeb1yrTb;
            _postData.d.ADeb2yrTb = ZakatInstalments.d.ADeb2yrTb;
            _postData.d.ADeb3yrTb = ZakatInstalments.d.ADeb3yrTb;
            _postData.d.ACr1yrTb = ZakatInstalments.d.ACr1yrTb;
            _postData.d.ACr2yrTb = ZakatInstalments.d.ACr2yrTb;

            _postData.d.ACr3yrTb = ZakatInstalments.d.ACr3yrTb;
            _postData.d.ARe1yrTb = ZakatInstalments.d.ARe1yrTb;
            _postData.d.ARe2yrTb = ZakatInstalments.d.ARe2yrTb;
            _postData.d.ARe3yrTb = ZakatInstalments.d.ARe3yrTb;
            _postData.d.APr1yrTb = ZakatInstalments.d.APr1yrTb;
            _postData.d.APr2yrTb = ZakatInstalments.d.APr2yrTb;
            _postData.d.APr3yrTb = ZakatInstalments.d.APr3yrTb;
            _postData.d.ACrt1yrTb = ZakatInstalments.d.ACrt1yrTb;
            _postData.d.ACrt2yrTb = ZakatInstalments.d.ACrt2yrTb;
            _postData.d.ACrt3yrTb = ZakatInstalments.d.ACrt3yrTb;
            _postData.d.APc1yrTb = ZakatInstalments.d.APc1yrTb;

            _postData.d.APc2yrTb = ZakatInstalments.d.APc2yrTb;
            _postData.d.APc3yrTb = ZakatInstalments.d.APc3yrTb;
            _postData.d.APerAmtRd = ZakatInstalments.d.APerAmtRd;
            _postData.d.ADpRequDrp = ZakatInstalments.d.ADpRequDrp;
            _postData.d.APer = ZakatInstalments.d.APer;
            _postData.d.AFormStatus = ZakatInstalments.d.AFormStatus;
            _postData.d.ADownLetterChk = ZakatInstalments.d.ADownLetterChk;
            _postData.d.ADownYear = ZakatInstalments.d.ADownYear;
            _postData.d.ADownMonth = ZakatInstalments.d.ADownMonth;
            _postData.d.ADownToYear = ZakatInstalments.d.ADownToYear;
            _postData.d.ADownToMonth = ZakatInstalments.d.ADownToMonth;
            _postData.d.ABranch = ZakatInstalments.d.ABranch;
            _postData.d.ASaudiShare = ZakatInstalments.d.ASaudiShare;
            _postData.d.ANonsaudiShare = ZakatInstalments.d.ANonsaudiShare;
            _postData.d.AMainAct = ZakatInstalments.d.AMainAct;
            _postData.d.AMainActDesc = ZakatInstalments.d.AMainActDesc;
            _postData.d.APoBox = ZakatInstalments.d.APoBox;
            _postData.d.APostalCode = ZakatInstalments.d.APostalCode;

            _postData.d.AFaxNo = ZakatInstalments.d.AFaxNo;
            _postData.d.ABuilding = ZakatInstalments.d.ABuilding;
            _postData.d.AStreet = ZakatInstalments.d.AStreet;
            _postData.d.ADistrict = ZakatInstalments.d.ADistrict;
            _postData.d.ACity = ZakatInstalments.d.ACity;
            _postData.d.ALvError = ZakatInstalments.d.ALvError;
            _postData.d.ATotalAmt = ZakatInstalments.d.ATotalAmt;
            _postData.d.Status = ZakatInstalments.d.Status;
            _postData.d.AttDetSet = ZakatInstalments.d.AttDetSet.results;
            _postData.d.Off_notesSet = ZakatInstalments.d.Off_notesSet.results;
            _postData.d.z_invoiceSet = ZakatInstalments.d.z_invoiceSet.results;
            _postData.d.z_proposedinsSet = ZakatInstalments.d.z_proposedinsSet.results;

            //List<ZINVOICEUI5Set> lstZINVOICEUI5Set = new List<ZINVOICEUI5Set>();
            //for (int i = 0; i < _zakatInstalmentDetails.d.Z_INVOICE_UI5Set.results.Count; i++)
            //{

            //    ZakatInstalmentPostModel.Metadata2 _metadata2 = new ZakatInstalmentPostModel.Metadata2();
            //    ;
            //    ZINVOICEUI5Set _ZINVOICEUI5SetData = new ZINVOICEUI5Set();
            //    _metadata2.id = _zakatInstalmentDetails.d.Z_INVOICE_UI5Set.results[i].__metadata.id;
            //    _metadata2.uri = _zakatInstalmentDetails.d.Z_INVOICE_UI5Set.results[i].__metadata.uri;
            //    _metadata2.type = _zakatInstalmentDetails.d.Z_INVOICE_UI5Set.results[i].__metadata.type;

            //    _ZINVOICEUI5SetData.__metadata = _metadata2;
            //    _ZINVOICEUI5SetData.AAmtTb = _zakatInstalmentDetails.d.Z_INVOICE_UI5Set.results[i].AAmtTb;
            //    _ZINVOICEUI5SetData.AClearedAmtTb = _zakatInstalmentDetails.d.Z_INVOICE_UI5Set.results[i].AClearedAmtTb;
            //    _ZINVOICEUI5SetData.ADueAmtTb = _zakatInstalmentDetails.d.Z_INVOICE_UI5Set.results[i].ADueAmtTb;
            //    _ZINVOICEUI5SetData.ADueDtTb = _zakatInstalmentDetails.d.Z_INVOICE_UI5Set.results[i].ADueDtTb;
            //    _ZINVOICEUI5SetData.AIvAmtTb = _zakatInstalmentDetails.d.Z_INVOICE_UI5Set.results[i].AIvAmtTb;
            //    _ZINVOICEUI5SetData.AIvNoTb = _zakatInstalmentDetails.d.Z_INVOICE_UI5Set.results[i].AIvNoTb;
            //    _ZINVOICEUI5SetData.AIvSrNoTb = _zakatInstalmentDetails.d.Z_INVOICE_UI5Set.results[i].AIvSrNoTb;
            //    _ZINVOICEUI5SetData.AIvTb = _zakatInstalmentDetails.d.Z_INVOICE_UI5Set.results[i].AIvTb;
            //    _ZINVOICEUI5SetData.AIvAbtyp = _zakatInstalmentDetails.d.Z_INVOICE_UI5Set.results[i].AIvAbtyp;
            //    lstZINVOICEUI5Set.Add(_ZINVOICEUI5SetData);

            //}

            //_d.Z_INVOICE_UI5Set = lstZINVOICEUI5Set;


            _postData.d.__metadata = ZakatInstalments.d.__metadata;

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
                if (response != null && response.d != null)
                {
                    try
                    {
                        if (response != null && response.d != null)
                        {
                            if (response.d.Submitz.Equals("X"))
                            {
                                string number = response.d.Fbnumz;
                                string displayMessage = AppResources.VATRSuccessFullVoidMessage + " " + number;
                                await _dialogService.ShowMessage(displayMessage, AppResources.Information);
                                _navigationService.GoBack();
                            }
                            if (response.d.Submitz.Equals("05"))
                            {
                                //  string number = response.d.Fbnumz;
                                string displayMessage = AppResources.VATRSaveasdraftMessage;
                                await _dialogService.ShowMessage(displayMessage, AppResources.Information);

                            }




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
