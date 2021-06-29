using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.Models.VATInstalationModels;
using EGAZT.Views.NewDesign;
using EGAZT.Views.NewDesign.Common;
using EGAZT.Views.NewDesign.VATDeclarationPages;
using EGAZT.Views.NewDesign.VatInstalmentPlan;
using EGAZT.Views.NewDesign.ZakatInstalmentPlan;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using pdfjs.Interfaces;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Metadata = EGAZT.Models.VATInstalationModels.Metadata;
using Xamarin.Forms.Internals;
using EGAZT.Manager;

namespace EGAZT.ViewModel.NewDesignViewModel.VATInstalmentPlanViewModel
{ 
    [Preserve(AllMembers = true)]
    public class VATInstalmentPlanViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        private bool _isLoading = false;
        public System.Collections.Generic.List<VATResults4> selectedList = new System.Collections.Generic.List<VATResults4>();

        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                if (_isLoading == value) return;
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }
        private string _vATDueAmount = "0.00";
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
                RaisePropertyChanged("VATDueAmount");
            }
        }
        private bool _secondTerms = false;
        public bool SecondTerms
        {
            get
            {
                return _secondTerms;
            }
            set
            {
                if (_secondTerms == value) return;

                _secondTerms = value;
                RaisePropertyChanged("SecondTerms");
            }
        }
        private string _vATPenalityAmount = "0.00";
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
                RaisePropertyChanged("VATPenalityAmount");
            }
        }
        private string _vATLiabilityAmount = "0.00 SAR";
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
                RaisePropertyChanged("VATLiabilityAmount");
            }
        }
        private string _vATBillDueAmount = "0.00 SAR";
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
                RaisePropertyChanged("VATBillDueAmount");
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
        private bool _IsViewEnable = true;
        public bool IsViewEnable
        {
            get
            {
                return _IsViewEnable;
            }
            set
            {
                if (_IsViewEnable == value) return;

                _IsViewEnable = value;
                RaisePropertyChanged("IsViewEnable");
            }
        }

        private bool _IsDraftRecord = false;
        public bool IsDraftRecord
        {
            get
            {
                return _IsDraftRecord;
            }
            set
            {
                if (_IsDraftRecord == value) return;

                _IsDraftRecord = value;
                RaisePropertyChanged("IsDraftRecord");
            }
        }
        public bool MarkComplete { get; private set; } = false;
        public int MaxIndex { get; private set; } = 6;
        private VATResults4[] _billsListVATData;
        public VATResults4[] BillsListVATData
        {
            get
            {
                return _billsListVATData;
            }
            set
            {
                if (_billsListVATData == value) return;
                _billsListVATData = value;
                RaisePropertyChanged("BillsListVAT");
            }
        }
        private VATResults4[] _billsListVAT;
        public VATResults4[] BillsListVAT
        {
            get
            {
                return _billsListVAT;
            }
            set
            {
                if (_billsListVAT == value) return;
                _billsListVAT = value;
                RaisePropertyChanged("BillsListVAT");
            }
        }

        private VATResults3[] _statementList;
        public VATResults3[] StatementList
        {
            get
            {
                return _statementList;
            }
            set
            {
                if (_statementList == value) return;

                _statementList = value;
                RaisePropertyChanged("StatementList");
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
                if (_isNoDataLableVisible == value) return;
                _isNoDataLableVisible = value;
                RaisePropertyChanged("isNoDataLableVisible");
            }
        }
        public ICommand GoBackClick { get; set; }

        int noOfInstalments = 5;
        double minInstalments = 2;
        double maxInstalments = 12;
        double downPaymentAmount = 00.00;
        double minAmount = 400.0;
        double maxAmount = 2000000.0;
        string inputData = "";
        string totalAmountSAR = "0.00 SAR";
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
            VATSelectionView,
            VATAggrementView,
            VATBillView,
            VATAttachmentsView,
            VATSummaryView,
            VATSuccessView,
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
        public ICommand SuccessGoToDashboardTapped { get; set; }
        public ICommand DownloadConfirmationTapped { get; set; }
        public ICommand onMoreOptionClicked { get; set; }


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
                if (_isBackButtonVisible == value) return;

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
                if (_isSelectionViewEnabled == value) return;

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
                if (_selectedOutletOptionIndex == value) return;

                _selectedOutletOptionIndex = value;
                RaisePropertyChanged("SelectedOutletOptionIndex");
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
                if (_isAgreementViewEnabled == value) return;

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
                if (_isOutletViewEnabled == value) return;
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
                if (_isBillsViewEnabled == value) return;
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
                if (_isVATBillsViewEnabled == value) return;
                _isVATBillsViewEnabled = value;
                RaisePropertyChanged("IsVATBillsViewEnabled");
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
                if (_isAttachmentsViewEnabled == value) return;
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
                if (_isStatementViewEnabled == value) return;
                _isStatementViewEnabled = value;
                RaisePropertyChanged("IsStatementViewEnabled");
            }
        }

        private bool _isSummaryViewEnabled = false;
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
                RaisePropertyChanged("IsSummaryViewEnabled");
            }
        }

        public void EnableBillsContinue()
        {
            if (TotalAmountSAR.Equals("0.00 SAR"))
            {
                IsBillContinueEnabled = false;
            }
            else
            {
                IsBillContinueEnabled = true;
            }
        }



        private bool _isBillContinueEnabled = false;
        public bool IsBillContinueEnabled
        {
            get { return _isBillContinueEnabled; }
            set
            {
                if (_isBillContinueEnabled == value) return;
                _isBillContinueEnabled = value;
                IsBillContinueBackGroundColor = Color.FromHex(_isBillContinueEnabled ? "#d49504" : "#9EA4A9");
                RaisePropertyChanged("IsBillContinueEnabled");
            }
        }
        private Color _isBillContinueBackGroundColor = Color.FromHex("#d49504");
        public Color IsBillContinueBackGroundColor
        {
            get
            {
                return _isBillContinueBackGroundColor;
            }
            set
            {
                if (_isBillContinueBackGroundColor == value) return;
                _isBillContinueBackGroundColor = value;
                RaisePropertyChanged("IsBillContinueBackGroundColor");
            }
        }

        private Color _isSelectionContinueBackGroundColor = Color.FromHex("#d49504");

        private bool _IsFirstCheckboxChecked = false;
        public bool IsFirstCheckboxChecked
        {
            get
            {
                return _IsFirstCheckboxChecked;
            }
            set
            {
                if (_IsFirstCheckboxChecked == value) return;

                _IsFirstCheckboxChecked = value;
                IsSelectionContinueBackGroundColor = Color.FromHex(_IsFirstCheckboxChecked ? "#d49504" : "#9EA4A9");
                RaisePropertyChanged("IsFirstCheckboxChecked");
            }
        }
        public Color IsSelectionContinueBackGroundColor
        {
            get
            {
                return _isSelectionContinueBackGroundColor;
            }
            set
            {
               
                if (_isSelectionContinueBackGroundColor == value)
                {
                    return;
                }
                _isSelectionContinueBackGroundColor = value;
                RaisePropertyChanged("IsSelectionContinueBackGroundColor");
            }
        }

        private bool _isSucessViewEnabled = true;
        public bool IsSucessViewEnabled
        {
            get
            {
                return _isSucessViewEnabled;
            }
            set
            {
                if (_isSucessViewEnabled == value) return;

                    _isSucessViewEnabled = value;
                RaisePropertyChanged("IsSucessViewEnabled");
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
                    RaisePropertyChanged("NoOfInstalments");
                }
            }
            get
            {
                return noOfInstalments;
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
                if (maxInstalments == value) return;

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
                if (downPaymentAmount == value) return;

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
                if (minAmount == value) return;

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
                if (maxAmount == value) return;

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
                if (totalAmountSAR == value) return;

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


        public string InputData
        {
            set
            {
                if (inputData == value) return;

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
                if (_isVATAmountVisible == value) return;

                _isVATAmountVisible = value;
                RaisePropertyChanged("IsVATAmountVisible");
            }
        }

        //Zakat Slection starts here

        private bool _isZakatSelected = true;
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
                if (_isIncomeTaxViewEnabled == value) return;

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
                if (_isSubIncomeTaxViewEnabled == value) return;

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
                if (_listZAKATCorrespondance == value) return;

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
                if (_txtSelectedStatusZakat == value) return;

                _txtSelectedStatusZakat = value;
                RaisePropertyChanged("TxtSelectedStatusZakat");
            }
        }

        private string _vATReferanceNumber = string.Empty;
        public string VATReferanceNumber
        {
            get
            {
                return _vATReferanceNumber;
            }
            set
            {
                if (_vATReferanceNumber == value) return;

                _vATReferanceNumber = value;
                RaisePropertyChanged("VATReferanceNumber");
            }
        }

        private string _vATCustomerName = string.Empty;
        public string VATCustomerName
        {
            get
            {
                return _vATCustomerName;
            }
            set
            {
                if (_vATCustomerName == value) return;

                _vATCustomerName = value;
                RaisePropertyChanged("VATCustomerName");
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
                if (_selectedFilterZakatPrev == value) return;

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
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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
                if (_corresFilterZakat == value) return;

                _corresFilterZakat = value;
                RaisePropertyChanged("CorresFilterZakat");
            }
        }

        private CorrespondenceFiltersModel _subSelectedFilterZakat = null;
        public CorrespondenceFiltersModel SubSelectedFilterZakat
        {
            get
            {
                return _subSelectedFilterZakat;
            }
            set
            {
                if (_subSelectedFilterZakat == value) return;

                _subSelectedFilterZakat = value;
                if (_subSelectedFilterZakat != null)
                {
                    if (_subSelectedFilterZakat.ID == 1)
                    {
                        if (SubListZAKATCorrespondance != null)
                        {
                            List<CorrespondanceModel> CorreTosort = new List<CorrespondanceModel>();
                            CorreTosort = SubListZAKATCorrespondance;
                            SubListZAKATCorrespondance = null;
                            var SortedList = CorreTosort.OrderBy(x => x.StartDate).ThenBy(x => x.Ctime);
                            SubListZAKATCorrespondance = SortedList.ToList<CorrespondanceModel>();
                        }
                    }
                    if (_subSelectedFilterZakat.ID == 2)
                    {
                        if (SubListZAKATCorrespondance != null)
                        {
                            List<CorrespondanceModel> CorreTosort = new List<CorrespondanceModel>();
                            CorreTosort = SubListZAKATCorrespondance;
                            SubListZAKATCorrespondance = null;
                            if (CorreTosort != null)
                            {

                                var SortedList = CorreTosort.OrderByDescending(x => x.StartDate).ThenByDescending(x => x.Ctime);
                                SubListZAKATCorrespondance = SortedList.ToList<CorrespondanceModel>();
                            }
                        }
                    }

                    SubTxtSelectedStatusZakat = _subSelectedFilterZakat.Filter;
                }
                RaisePropertyChanged("SubSelectedFilterZakat");
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
                if (_subListZAKATCorrespondance == value) return;

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
                if (_subTxtSelectedStatusZakat == value) return;

                _subTxtSelectedStatusZakat = value;
                RaisePropertyChanged("SubTxtSelectedStatusZakat");
            }
        }


        private CorrespondenceFiltersModel _subSelectedFilterZakatPrev = null;
        public CorrespondenceFiltersModel SubSelectedFilterZakatPrev
        {
            get
            {
                return _subSelectedFilterZakatPrev;
            }
            set
            {
                if (_subSelectedFilterZakatPrev == value) return;

                _subSelectedFilterZakatPrev = value;
                RaisePropertyChanged("SubSelectedFilterZakatPrev");
            }
        }

        private void SetSubStatusPickerItem()
        {
            try
            {
                List<CorrespondenceFiltersModel> FiltersZAKAT = new List<CorrespondenceFiltersModel>();
                FiltersZAKAT.Add(new CorrespondenceFiltersModel { ID = 1, Filter = "Financial Crisis- Liability to Settle Dues" });
                FiltersZAKAT.Add(new CorrespondenceFiltersModel { ID = 2, Filter = "Dispute in favor of GAZT" });
                FiltersZAKAT.Add(new CorrespondenceFiltersModel { ID = 3, Filter = "Other Reason" });
                SubCorresFilterZakat = FiltersZAKAT;
            }
            catch (Exception)
            {
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
                if (_subCorresFilterZakat == value) return;

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
                if (_subSetSelectedIndexZakat == value) return;

                _subSetSelectedIndexZakat = value;
                RaisePropertyChanged("SubSetSelectedIndexZakat");
            }
        }
        private string _PathOfPdf;
        public string PathOfPdf
        {
            get
            {
                return _PathOfPdf;
            }
            set
            {
                if (_PathOfPdf == value) return;
                _PathOfPdf = value;
                RaisePropertyChanged("PathOfPdf");
            }
        }

        private string _NotesText = "";
        public string NotesText
        {
            get
            {
                return _NotesText;
            }
            set
            {
                if (_NotesText == value) return;

                _NotesText = value;
                RaisePropertyChanged("NotesText");
            }
        }
        private void vatInstallmentBillsList()
        {
            //SelectedBillsList = new ObservableCollection<Models.ZakatInstalationModels.Result>();


            //SelectedBillsList = VATInstalmentPlanObject.d.VtiaSet.Results;

            /*SelectedBillsList.Add(new ZakatSelectBillModel()
            {
                billNumber = "Bill 1"
                amount = "1,200.00 SAR",
                saadNumber = "1234568798",
                taxPeriod = "2019-2020",
                isSelected = false,
                billType = "VAT"
            });
            SelectedBillsList.Add(new ZakatSelectBillModel()
            {
                billNumber = "Bill 2",
                amount = "1,300.00 SAR",
                saadNumber = "1234568798",
                taxPeriod = "2019-2020",
                isSelected = false,
                billType = "VAT"
            });
            SelectedBillsList.Add(new ZakatSelectBillModel()
            {
                billNumber = "Bill 3",
                amount = "1,400.00 SAR",
                saadNumber = "1234568798",
                taxPeriod = "2019-2020",
                isSelected = false,
                billType = "VAT"
            });
            SelectedBillsList.Add(new ZakatSelectBillModel()
            {
                billNumber = "Bill 4",
                amount = "1,500.00 SAR",
                saadNumber = "1234568798",
                taxPeriod = "2019-2020",
                isSelected = false,
                billType = "VAT"
            });*/
        }

        public void PopulateSummaryReasonData()
        {
            var summarySelectedBillsList = new ObservableCollection<ZakatSelectBillModel>();
            for (int i = 0; i < selectedList.Count; i++)
            {
                summarySelectedBillsList.Add(new ZakatSelectBillModel()
                {
                    billNumber = AppResources.Bill + " " +(i + 1).ToString("00") + ":",
                    amount = selectedList[i].Betrh /*+ " " + selectedList[i].Waers*/,
                    saadNumber = selectedList[i].SadadNo,
                    taxPeriod = selectedList[i].Taxperioddsc,
                    isSelected = false,
                    billType = AppResources.ZakatInstalmetSelectTypeVAT
                });
            }
            SummarySelectedBillsList = summarySelectedBillsList;
        }

        public void PopulateInstalmentsListViewTemplate()
        {
            InstalmentPlans = new ObservableCollection<InstalmentAgreementInstalmentPlansModel>();
            InstalmentPlans.Add(new InstalmentAgreementInstalmentPlansModel
            {
                DueDate = "30/09/2020",
                NumberOfMonths = "01",
                MonthlyInstalment = "3672.09",
                TotalAmountPaid = "3672.09",
                TotalAmountRemaining = "7344.17"
            });
            InstalmentPlans.Add(new InstalmentAgreementInstalmentPlansModel
            {
                DueDate = "31/10/2020",
                NumberOfMonths = "02",
                MonthlyInstalment = "3672.09",
                TotalAmountPaid = "7344.18",
                TotalAmountRemaining = "3672.08"
            });
            InstalmentPlans.Add(new InstalmentAgreementInstalmentPlansModel
            {
                DueDate = "30/11/2020",
                NumberOfMonths = "03",
                MonthlyInstalment = "3672.09",
                TotalAmountPaid = "11016.26",
                TotalAmountRemaining = "0.00"
            });
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


        public VATInstalmentPlanModel vatInstalmentPlanModel { get; set; }
        public VATInstalmentPlanModel VATInstalmentPlanModel
        {
            get
            {
                return vatInstalmentPlanModel;
            }

            set
            {
                if (vatInstalmentPlanModel == value)
                {
                    return;
                }

                vatInstalmentPlanModel = value;
                RaisePropertyChanged("VATInstalmentPlanModel");
            }
        }

        public ObservableCollection<VATInstalmentPlanModel> c { get; set; }
        public ObservableCollection<VATInstalmentPlanModel> outletDecisionOptions { get; set; }
        public ObservableCollection<VATInstalmentPlanModel> OutletDecisionOptions
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
        public ObservableCollection<Models.VATInstalationModels.VATResults4> selectedBillsList { get; set; }
        public ObservableCollection<Models.VATInstalationModels.VATResults4> SelectedBillsList
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
                RaisePropertyChanged("ListOfActionButtonsApplicable");
            }
        }



        private VATInstalmentPlanModel _selectedOutletOption;
        public VATInstalmentPlanModel SelectedOutletOption
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
                RaisePropertyChanged("SelectedOutletOption");
            }
        }


        private VatInstalmentPlanResponse _vatInstalments;
        public VatInstalmentPlanResponse VatInstalments
        {
            get
            {
                return _vatInstalments;
            }
            set
            {
                if (_vatInstalments == value) return;

                _vatInstalments = value;
                RaisePropertyChanged("VatInstalments");
            }
        }

        private VATInstalment _vatInstalment;
        public VATInstalment VatInstalment
        {
            get
            {
                return _vatInstalment;
            }
            set
            {
                if (_vatInstalment == value) return;
                _vatInstalment = value;
                RaisePropertyChanged("VatInstalment");
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
                if (_isVatTermsChecked == value) return;

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
                if (_isDeclarationChecked == value) return;

                _isDeclarationChecked = value;
                if (_isDeclarationChecked)
                {
                    IsContinueButtonEnable = true;
                }

                RaisePropertyChanged("IsDeclarationChecked");
            }
        }

        private bool _isAttachmentsListVisible = false;
        public bool IsAttachmentsListVisible
        {
            get
            {
                return _isAttachmentsListVisible;
            }
            set
            {
                if (_isAttachmentsListVisible == value) return;

                _isAttachmentsListVisible = value;
                RaisePropertyChanged("IsAttachmentsListVisible");
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
                    ContinueButtonnBackroundColor = Color.FromHex("#d49504");
                }
                else
                {
                    ContinueButtonnBackroundColor = Color.FromHex("#9EA4A9");
                }
                RaisePropertyChanged("IsContinueButtonEnable");
            }
        }
        public string _monthlyInstalment = "0.00";

        public string MonthlyInstalment
        {
            set
            {
                if (_monthlyInstalment == value) return;

                if (_monthlyInstalment != value)
                {
                    _monthlyInstalment = value;
                    RaisePropertyChanged("MonthlyInstalment");
                }
            }
            get
            {
                return _monthlyInstalment;
            }
        }

        string minInstalmentsTitle = AppResources.ZakatMin + " " + 2;
        string maxInstalmentsTitle = AppResources.ZakatMax + " " + 12;

        public string MaxInstalmentsTitle
        {
            set
            {
                if (maxInstalmentsTitle == value) return;

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



        public string MinInstalmentsTitle
        {
            set
            {
                if (minInstalmentsTitle == value) return;

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
        private Stream _StreamForDownloadURL = null;
        public Stream StreamForDownloadURL
        {
            get
            {
                return _StreamForDownloadURL;
            }
            set
            {
                if (_StreamForDownloadURL == value) return;

                _StreamForDownloadURL = value;
                RaisePropertyChanged("StreamForDownloadURL");
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
                if (_continueButtonnBackroundColor == value) return;

                _continueButtonnBackroundColor = value;
                RaisePropertyChanged("ContinueButtonnBackroundColor");
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
            catch (Exception)
            {
            }
        }

        public void setMoreOptioButtons()
        {
            var listOfActionButtonsApplicable = new List<string>();
            if (App.selectedVATItem != "")
            {
                listOfActionButtonsApplicable.Add(AppResources.ZZVoid);
            }

            listOfActionButtonsApplicable.Add(AppResources.ZZSaveAsDraft);

            if (App.selectedVATItemFbust == "E0075" || App.selectedVATItemFbust == "E0074" || App.selectedVATItemFbust == "E0018")
            {
                listOfActionButtonsApplicable.Add(AppResources.ZZDisplayNotes);
            }

            listOfActionButtonsApplicable.Add(AppResources.ZZCreateNotes);


            ListOfActionButtonsApplicable = listOfActionButtonsApplicable;
        }


        public async void VATSetReturnVoidAsync()
        {
            VatInstalments.d.Operationz = "04";
            VatInstalments.d.Decflg = "1";

            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {

                    if (!isDraftClicked)
                    {
                        isDraftClicked = true;
                        setDATA();
                        var VatInstalmentData = await SubmitClicked();
                        isDraftClicked = false;
                        if (VatInstalmentData != null && VatInstalmentData.d != null)
                        {
                            VatInstalments = VatInstalmentData;

                            Device.BeginInvokeOnMainThread(async () =>
                            {


                                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                                headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                                headerAmountInfo.IsLinkAvailable = false;
                                headerAmountInfo.Message = AppResources.ZZGeneralMessage_VATCancelled;

                                headerWithInfos.Add(headerAmountInfo);


                                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                                newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                                await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

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
                    IsLoading = false;
                });
            }
            catch (Exception)
            {
            }
        }
         
        public async void VATReturnAddNote()
        {
            await PopupNavigation.Instance.PushAsync(new AddNotesPopupPageView(NotesText, true));
        }

        public async void VATReturnGetNotes()
        {
            await PopupNavigation.Instance.PushAsync(new ViewNotesPopUpPageView(_vatInstalments.d.NotesSet));
        }

        public bool isDraftClicked = false;
        public async void OnSaveDraftClicked()
        {

            VatInstalments.d.Operationz = "05";
            VatInstalments.d.Decflg = "1";


            try
            {
               /* Device.BeginInvokeOnMainThread(() =>
                {*/
                    IsLoading = true;
              //  });
                await Task.Run(async () =>
                {

                    if (!isDraftClicked)
                    {
                        isDraftClicked = true;
                        setDATA();

                        var VatInstalmentData = await SubmitClicked();

                        if (VatInstalmentData != null && VatInstalmentData.d != null)
                        {
                            VatInstalments = VatInstalmentData;

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                App.selectedVATItem = VatInstalments.d.Fbnumz;
                                setMoreOptioButtons();

                                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                                headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                                headerAmountInfo.IsLinkAvailable = false;
                                headerAmountInfo.Message = string.Format(AppResources.VATDraftSaved, "  " + VatInstalments.d.Fbnumz);

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

                            if (string.IsNullOrEmpty(WebServiceManager.ErrorMessageForVAT))
                            {
                                await Task.Run(() =>
                                {
                                    IsLoading = false;
                                });
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    IsLoading = false;
                                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                });


                            }
                            else
                            {
                                await Task.Run(() =>
                                {
                                    IsLoading = false;
                                });
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    IsLoading = false;
                                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
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
                    IsLoading = false;
                });
            }
            catch (Exception)
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

                case (int)PagesEnum.ZakatAttachmentsView:
                    EnableStatementsView();
                    break;
                case (int)PagesEnum.ZakatEnableStatementsView:
                    EnableAgreementView();
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

        public VATInstalmentPlanViewModel(INavigationService navigationService, IDialogService dialogService)
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
            GoBackClick = new Command(() =>
            {
                Backnavigations();
            });

            CloseClick = new Command(() =>
            {

                //await Application.Current.MainPage.Navigation.PushAsync(new VatInstalmentPlanListPageView());

                _navigationService.GoBack();

                //EnableSlectionView();
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


            if (App.selectedVATItemFbust == "E0075" || App.selectedVATItemFbust == "E0074" || App.selectedVATItemFbust == "E0018" || App.selectedVATItemFbust == "E0013")
            {
                IsFirstCheckboxChecked = true;

            }
            else {

                IsFirstCheckboxChecked = false;

            }

            VATInstalationClicked = new Command(this.VATInstalationTapped);
            ReasonContinueBtnTapped = new Command(this.ReasonContinueBtnClicked);
            AggrementContinueBtnTapped = new Command(this.AggrementContinueBtnClicked);
            BillContinueBtnTapped = new Command(this.BillContinueBtnClicked);
            AttachmentsContinueBtnTapped = new Command(this.AttachmentsContinueBtnClicked);
            StatementsContinueBtnTapped = new Command(this.StatementsContinueBtnClicked);
            SummaryContinueBtnTapped = new Command(this.SummaryContinueBtnClicked);
            SummaryInstallmentDetailsBtnTapped = new Command(this.SummaryInstallmentDetailsBtnClicked);
            OnZakatInstalmentReasonTapped = new Command(this.OnZakatInstalmentReasonClicked);
            NewAttachmentTapped = new Command(this.NewAttachmentClicked);
            SuccessGoToDashboardTapped = new Command(this.SuccessGoToDashboardClicked);
            DownloadConfirmationTapped = new Command(this.DownloadConfirmationClicked);
            onMoreOptionClicked = new Command(async () =>
            {
                await PopupNavigation.Instance.PushAsync(new MoreMenuPopUpPageViewRTwo(ListOfActionButtonsApplicable));
            });
            vatInstalmentPlanModel = new VATInstalmentPlanModel();
            SelectedOutletOption = new VATInstalmentPlanModel();
            EnableBillsContinue();
            AddOutletDecisionOptions();
            AddFrequencyOptions();

        }

        public void AddOutletDecisionOptions()
        {
            OutletDecisionOptions = new ObservableCollection<VATInstalmentPlanModel>();
            OutletDecisionOptions.Add(new VATInstalmentPlanModel
            {
                ActiveOutletDecisionOptions = AppResources.ZakatInstalmetSelectTypeZakat,
                ActiveOutletDecisionOptionsIsSelected = true
            });
            OutletDecisionOptions.Add(new VATInstalmentPlanModel
            {
                ActiveOutletDecisionOptions = AppResources.ZakatInstalmetSelectTypeIncomeTax,
                ActiveOutletDecisionOptionsIsSelected = false
            });
            OutletDecisionOptions.Add(new VATInstalmentPlanModel
            {
                ActiveOutletDecisionOptions = AppResources.ZakatInstalmetSelectTypeVAT,
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

        public void EnableSlectionView()
        {
            //SelectedOutletOption = OutletDecisionOptions[0];
            //SelectedOutletOptionIndex = 0;
            CurrentIndex = 1;
            IsBackButtonVisible = false;
            IsSelectionViewEnabled = true;
            IsOutletViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsSummaryViewEnabled = false;
            IsAgreementViewEnabled = false;
            IsBillsViewEnabled = false;
            IsStatementViewEnabled = false;
            IsVATBillsViewEnabled = false;
            IsSucessViewEnabled = false;
            selectedPage = (int)PagesEnum.ZakatSelectionView;

        }
        public void BindVATSelectionView()
        {

            if (VatInstalments != null && VatInstalments.d != null)
            {

                VATDueAmount = VatInstalments.d.TotInvAmt;
                VATPenalityAmount = VatInstalments.d.Peneltyamt;
                VATBillDueAmount = VatInstalments.d.Totdueamt;
                VATLiabilityAmount = VatInstalments.d.Totliablityamt;
            }
        }

        public void BindBillsListView()
        {


            if (VatInstalments.d.VTIASet.results != null)
            {
                SelectedBillsList = new ObservableCollection<Models.VATInstalationModels.VATResults4>();
                foreach (VATResults4 bills in VatInstalments.d.VTIASet.results)
                {
                    SelectedBillsList.Add(bills);
                }


                BillsListVAT = VatInstalments.d.VTIASet.results;
                BillsListVATData = VatInstalments.d.VTIASet.results;
            }

        }
        public void SearchBills()
        {
            try
            {
                if (InputData.Length > 0)
                {
                    //BillsListVAT = BillsListVATData.Where(w => w.SadadNo.Contains(InputData)).ToArray();
                    SelectedBillsList = (ObservableCollection<VATResults4>)SelectedBillsList.Where(w => w.SadadNo.Contains(InputData));
                }
                else
                {
                    SelectedBillsList = new ObservableCollection<Models.VATInstalationModels.VATResults4>();

                    foreach (VATResults4 bills in VatInstalments.d.VTIASet.results)
                    {
                        SelectedBillsList.Add(bills);
                    }
                    // BillsListVAT = BillsListVATData;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void BindStatementsView()
        {
            if (VatInstalments.d.VTISSet.results != null)
            {

                //StatementList = null;
                var statementList = VatInstalments.d.VTISSet.results;

                for (int i = 0; i < statementList.Length; i++)
                {

                   
                        DateTime dateStart = new DateTime();
                        //CultureInfo cultureInfo = new CultureInfo("ar-SA");
                        string apiDate = @"""" + statementList[i].Faedn + @"""";
                    if (apiDate.Contains("Date"))
                    {
                        dateStart = JsonConvert.DeserializeObject<DateTime>(apiDate);

                        GregorianCalendar hjCalendar = new GregorianCalendar();
                        int year = hjCalendar.GetYear(dateStart);
                        int month = hjCalendar.GetMonth(dateStart);
                        int day = hjCalendar.GetDayOfMonth(dateStart);

                        string dateStr = string.Format("{0:00}/{1}/{2}", day, month, year);

                        statementList[i].Faedn = dateStr;

                        string dt1 = string.Empty;
                        string[] dts = null;
                        dts = statementList[i].Faedn.Split('/');

                        if (App.IsArabic)
                        {
                            dt1 = dts[2] + "-" + dts[1] + "-" + dts[0];

                        }
                        else
                        {
                            dt1 = dts[0] + "-" + dts[1] + "-" + dts[2];

                        }



                        //dt1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];

                        statementList[i].Faedn = dt1;

                    }


                }
                StatementList = statementList;
                VATBillDueAmount = VatInstalments.d.Totdueamt;
                VATPenalityAmount = VatInstalments.d.Peneltyamt;
                if(StatementList != null && StatementList.ToList().Count > 0) {

                    MonthlyInstalment = VatInstalments.d.VTISSet.results[0].Betrw ;

                }

                //try
                //{

                //    VATPenalityAmount = Math.Abs(double.Parse(VATBillDueAmount) - double.Parse(TotalAmountSAR.Replace(" SAR", "").Replace(",", ""))) + "";

                //}
                //catch (Exception e)
                //{

                //}

            }
        }


        public void EnableBillView()
        {
            //SelectedOutletOption = OutletDecisionOptions[0];
            //SelectedOutletOptionIndex = 0;

            IsBackButtonVisible = true;
            IsSelectionViewEnabled = false;
            IsOutletViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsSummaryViewEnabled = false;
            IsAgreementViewEnabled = false;
            IsBillsViewEnabled = true;
            IsVATBillsViewEnabled = false;
            IsSucessViewEnabled = false;
            IsStatementViewEnabled = false;
            selectedPage = (int)PagesEnum.ZakatBillView;

        }
        public void EnableVATBillView()
        {
            //SelectedOutletOption = OutletDecisionOptions[0];
            //SelectedOutletOptionIndex = 0;

            CurrentIndex = 2;
            IsBackButtonVisible = true;
            IsSelectionViewEnabled = false;
            IsOutletViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsSummaryViewEnabled = false;
            IsAgreementViewEnabled = false;
            IsBillsViewEnabled = false;
            IsVATBillsViewEnabled = true;

            IsSucessViewEnabled = false;
            IsStatementViewEnabled = false;
            selectedPage = (int)PagesEnum.ZakatBillView;

        }

        public void EnableAgreementView()
        {
            CurrentIndex = 3;
            IsBackButtonVisible = true;
            IsSelectionViewEnabled = false;
            IsOutletViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsSummaryViewEnabled = false;
            IsAgreementViewEnabled = true;
            IsBillsViewEnabled = false;
            IsSucessViewEnabled = false;
            IsVATBillsViewEnabled = false;
            IsStatementViewEnabled = false;
            selectedPage = (int)PagesEnum.ZakatAggrementView;

        }
        public void EnableStatementsView()
        {
            CurrentIndex = 4;
            IsBackButtonVisible = true;
            IsSelectionViewEnabled = false;
            IsOutletViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsSummaryViewEnabled = false;
            IsAgreementViewEnabled = false;
            IsBillsViewEnabled = false;
            IsSucessViewEnabled = false;
            IsVATBillsViewEnabled = false;
            IsStatementViewEnabled = true;

            selectedPage = (int)PagesEnum.ZakatEnableStatementsView;

        }


        public void EnableAttachmentsView()
        {
            CurrentIndex = 5;
            IsSelectionViewEnabled = false;
            IsOutletViewEnabled = false;
            IsAttachmentsViewEnabled = true;
            IsSummaryViewEnabled = false;
            IsAgreementViewEnabled = false;
            IsBillsViewEnabled = false;
            IsSucessViewEnabled = false;
            IsStatementViewEnabled = false;
            selectedPage = (int)PagesEnum.ZakatAttachmentsView;


        }



        public void EnableSummaryView()
        {
            if (AttachmentsListViewData.Count > 0)
            {
                IsAttachmentsListVisible = true;
            }
            else
            {
                IsAttachmentsListVisible = false;
            }


            CurrentIndex = 6;
            IsSelectionViewEnabled = false;
            IsOutletViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsSummaryViewEnabled = true;
            IsVATBillsViewEnabled = false;
            IsAgreementViewEnabled = false;
            IsBillsViewEnabled = false;
            IsSucessViewEnabled = false;
            IsStatementViewEnabled = false;
            selectedPage = (int)PagesEnum.ZakatSummaryView;




        }
        public async Task EnableSucessScreenAsync()
        {

            isDraftClicked = false;
            VatInstalments.d.Operationz = "01";
            VatInstalments.d.Decflg = "1";

            await Task.Run(async () =>
            {

                VatInstalments = await SubmitClicked();
            });

            if (VatInstalments != null && VatInstalments.d != null)
            {
                VATReferanceNumber = VatInstalments.d.Fbnumz;
                EnableSlectionView();
                await Application.Current.MainPage.Navigation.PushAsync(new VatInstalmentPlanSuccessPage());

            }









            /*IsSelectionViewEnabled = false;
            IsOutletViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsSummaryViewEnabled = false;
            IsAgreementViewEnabled = false;
            IsBillsViewEnabled = false;
            IsSucessViewEnabled = true;
            IsStatementViewEnabled = false;
            selectedPage = (int)PagesEnum.ZakatSuccessView;*/

        }

        private bool _firstTerms = false;
        public bool FirstTerms
        {
            get
            {
                return _firstTerms;
            }
            set
            {
                _firstTerms = value;
                RaisePropertyChanged("FirstTerms");
            }
        }

        public void ResetData()
        {
            EnableSlectionView();
            NoOfInstalments = 2;
            _isLoading = false;
            SecondTerms = false;
            _vATPenalityAmount = "0.00";
            _vATLiabilityAmount = "0.00 SAR";
            _vATBillDueAmount = "0.00 SAR";
            _isNoDataLableVisible = false;
            AttachmentsListViewData = null;
            FirstTerms = false;
            downPaymentAmount = 00.00;
            inputData = "";
            TotalAmountSAR = "0.00 SAR";
            MinInstalmentsTitle = AppResources.ZakatMin + " " + 2;
            MaxInstalmentsTitle = AppResources.ZakatMax + " " + 12;
            selectedList.Clear();
            selectedPage = (int)PagesEnum.ZakatSelectionView;
            IsViewEnable = true;
            IsFirstCheckboxChecked = false;
            NotesText = "";
        }

        public async void VATInstalationTapped()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new InstructionsBottomPopUpView(instructionString: "Test", checkBoxString: "Test", continueString: "VAT Instalment",
                 _dialogType: ZakatInstalmentViewModel.InstructionsBottomPopUpViewModel.DialogType
                     .Instructions));
                // _navigationService.NavigateTo(App.ZakatInstalmentPlanPageView);
            }
            catch (GAZTUnlockAccountException )
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
                //if (FirstTerms)
                //{
                if (IsFirstCheckboxChecked)
                {
                    EnableVATBillView();
                }
                else
                {
                    await _dialogService.ShowMessage(AppResources.ZZZZConfirmAndCarryForward, AppResources.Information);
                }
                //}
                //else
                //{
                //    await PopupNavigation.Instance.PushAsync(new InstructionsBottomPopUpView(instructionString: AppResources.VatInstructions, checkBoxString: AppResources.VatInstructionsCheckBoxDesc, continueString: AppResources.VatInstalmetPlanTitle,
                //        _dialogType: ZakatInstalmentViewModel.InstructionsBottomPopUpViewModel.DialogType.Instructions));


                //}


            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());

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

        public async void BillContinueBtnClicked()
        {
            try
            {
                if (TotalAmountSAR.Equals("0.00 SAR"))
                {
                    await _dialogService.ShowMessage(AppResources.VATInstalmentPlanPleaseSelectAtleastOne, AppResources.Information);
                }
                else
                {
                    EnableAgreementView();
                }
            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());


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


            isDraftClicked = false;

            if (App.selectedVATItemFbust == "E0075" || App.selectedVATItemFbust == "E0074" || App.selectedVATItemFbust == "E0018")
           {
                    EnableStatementsView();
                    BindStatementsView();
             
            }
            else {

                try
                {
                    //EnableAttachmentsView();


                    setDATA();
                    //VaiInstalmentRequest vatInstalments1 = new VaiInstalmentRequest();
                    //vatInstalments1.Xstep1Conf = "1";
                    //vatInstalments1.Xstep2Conf = "2";
                    //vatInstalments1.Noofinstallment = "4";
                    //vatInstalments1.Operationz = "10";

                    var amount = TotalAmountSAR.Replace(" SAR", "").Replace(",", "");
                    VatInstalments.d.Totliablityamt = amount;
                    VatInstalments.d.StepNumberz = "03";
                    VatInstalments.d.Decflg = "0";
                    VatInstalments.d.Operationz = "10";


                    for (int i = 0; i < VatInstalments.d.VTIASet.results.ToList().Count; i++)
                    {


                        VatInstalments.d.VTIASet.results[i].Xsele = "";

                    }


                    await Task.Run(async () =>
                    {
                        VatInstalments = await SubmitClicked();

                    });

                    if (VatInstalments != null && VatInstalments.d != null)
                    {
                        EnableStatementsView();
                        BindStatementsView();
                    }
                  

                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        _navigationService.GoBack();
                    });
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



            
        }


        public void OutletContinueBtnClicked()
        {
            try
            {
                EnableAttachmentsView();
            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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

        public void AttachmentsContinueBtnClicked()
        {
            try
            {


                EnableSummaryView();
                PopulateSummaryReasonData();
            }
            catch (GAZTUnlockAccountException )
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
        public void StatementsContinueBtnClicked()
        {
            try
            {
                if (AttachmentsListViewData == null)
                {
                    AttachmentsListViewData = new ObservableCollection<Attachment>();

                }

                if (App.selectedVATItem != "")
                {
                    
                        if(VatInstalments.d.AttachmentSet.results.Count > 0) {

                            var attch = new ObservableCollection<Attachment>();

                            foreach(var attachment in VatInstalments.d.AttachmentSet.results) {

                                if(attachment.Dotyp == "ZVTA") {

                                if (string.IsNullOrEmpty(attachment.Filename))
                                {
                                    attachment.Filename = DateTime.Now.ToString("yyyy/MM/dd");
                                }
                                attch.Add(attachment);
                                }


                            }

                        AttachmentsListViewData = attch;

                    }
                  
                }

                EnableAttachmentsView();

            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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

        private async void showTermsPopUp()
        {

            VatInstalments.d.Operationz = "01";
            VatInstalments.d.Decflg = "1";
            if (SecondTerms)
            {
                await EnableSucessScreenAsync();
            }
            else
            {

                if (App.selectedVATItem != "")
                {
                    if (App.selectedVATItemFbust == "E0075" || App.selectedVATItemFbust == "E0074" || App.selectedVATItemFbust == "E0018" || App.selectedVATItemFbust == "E0013")
                    {
                        
                        await PopupNavigation.Instance.PushAsync(new InstructionsBottomPopUpView(instructionString: AppResources.VatTerms, checkBoxString: AppResources.VatTermsCheckBoxDesc, continueString: AppResources.ZakatInstalmetContinue, isEditable: true,
                   _dialogType: ZakatInstalmentViewModel.InstructionsBottomPopUpViewModel.DialogType
                       .TermsConditions));
                    }
                    else
                    {

                        await PopupNavigation.Instance.PushAsync(new InstructionsBottomPopUpView(instructionString: AppResources.VatTerms, checkBoxString: AppResources.VatTermsCheckBoxDesc, continueString: AppResources.ZakatInstalmetContinue,
                        _dialogType: ZakatInstalmentViewModel.InstructionsBottomPopUpViewModel.DialogType
                        .TermsConditions));
                    }


                }
                else
                {
                    await PopupNavigation.Instance.PushAsync(new InstructionsBottomPopUpView(instructionString: AppResources.VatTerms, checkBoxString: AppResources.VatTermsCheckBoxDesc, continueString: AppResources.ZakatInstalmetContinue,
                    _dialogType: ZakatInstalmentViewModel.InstructionsBottomPopUpViewModel.DialogType
                        .TermsConditions));
                }

            }
        }

        public void SummaryContinueBtnClicked()
        {
            try
            {
                showTermsPopUp();
                //Display Success Screen
                //EnableSucessScreenAsync();
            }
            catch (GAZTUnlockAccountException )
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
        public void OnZakatInstalmentReasonClicked()
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
            if (Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopupStack.Count > 0) return;
            try
            {
                await PopupNavigation.Instance.PushAsync(new FilesUploadPopUpPageView(AttachmentsListViewData.ToList(), Models.ZakatInstalationModels.WhichAttachment.VATInstalment, VatInstalments.d.ReturnIdz));

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
        public void SummaryInstallmentDetailsBtnClicked()
        {
            try
            {
                //   await App.Current.MainPage.DisplayAlert("Alert", "Instalment details schedule is displayed here.", "OK");

            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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
        public void SuccessGoToDashboardClicked()
        {
            try
            {
                PopToRootPage();



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

        public void DownloadConfirmationClicked()
        {
            try
            {
                downloadConfirmation();

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
            AttachmentsListViewData = attachmentsListViewData;
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
                VatInstalments = null;
                VatInstalmentPlanResponse vATInstalment = null;
                try
                {
                    if (App.selectedVATItem != "")
                    {
                        if (App.selectedVATItemFbust == "E0075" || App.selectedVATItemFbust == "E0074" || App.selectedVATItemFbust == "E0018") { 
                            IsViewEnable = false;
                        }
                        var selectedItemFormID = await VATInstalationPlanWebServiceManager.GAZTGetFbGuidDetailsInputData(App.LoginDataRetrieved.FbGuid, App.selectedVATItem, App.LoginDataRetrieved.TIN, "E0045", "VTIA");
                        //vATInstalment = await WebServiceManager.GAZTGetVATInstalmentData();
                        //VatInstalments = vATInstalment;
                        if (selectedItemFormID.d != null)
                        {

                            vATInstalment = await VATInstalationPlanWebServiceManager.GAZTGetVATInstalmentData(selectedItemFormID.d.Fbguid, selectedItemFormID.d.Euser);
                            if(vATInstalment != null)
                            {
                                if(vATInstalment.d.NotesSet != null && vATInstalment.d.NotesSet.results.Count > 0)
                                    {
                                        int notesCount = vATInstalment.d.NotesSet.results.Count;
                                        var notesText = vATInstalment.d.NotesSet.results[notesCount - 1];
                                        NotesText = notesText.Strline;
                                    }

                            }
                            VatInstalments = vATInstalment;
                        }
                    }
                    else
                    {
                        vATInstalment = await VATInstalationPlanWebServiceManager.GAZTGetVATInstalmentData("", "");
                        VatInstalments = vATInstalment;
                    }


                        if (App.selectedVATItemFbust == "E0075" || App.selectedVATItemFbust == "E0074" || App.selectedVATItemFbust == "E0018" || App.selectedVATItemFbust == "E0013")
                        {
                            IsFirstCheckboxChecked = true;

                            if(App.selectedVATItemFbust == "E0013") {

                                foreach (var notes in VatInstalments.d.NotesSet.results)
                                {

                                    if (string.IsNullOrEmpty(notes.Strline))
                                    {

                                        NotesText = notes.Strline;
                                    }

                                }
                            }

                        }
                        else
                        {

                            IsFirstCheckboxChecked = false;

                        }

                        PopToRootPage();
                    // If seesion Expired it will navigate to Dashboard page

                    // EnableSlectionView();


                    if (VatInstalments != null && VatInstalments.d != null)
                    {

                            //await PopupNavigation.Instance.PushAsync(new InstructionsBottomPopUpView(instructionString: AppResources.VatInstructions, checkBoxString: AppResources.VatInstructionsCheckBoxDesc, continueString: AppResources.VatInstalmetPlanTitle,
                            //    _dialogType: ZakatInstalmentViewModel.InstructionsBottomPopUpViewModel.DialogType
                            //    .Instructions));


                            if (App.selectedVATItem != "")
                            {
                                if (App.selectedVATItemFbust == "E0075" || App.selectedVATItemFbust == "E0074" || App.selectedVATItemFbust == "E0018" || App.selectedVATItemFbust == "E0013")
                                {
                                    await PopupNavigation.Instance.PushAsync(new InstructionsBottomPopUpView(instructionString: AppResources.VatInstructions, checkBoxString: AppResources.VatInstructionsCheckBoxDesc, continueString: AppResources.VatInstalmetPlanTitle, isEditable: true, _dialogType: ZakatInstalmentViewModel.InstructionsBottomPopUpViewModel.DialogType
                            .Instructions));
                                }
                                else {

                                    await PopupNavigation.Instance.PushAsync(new InstructionsBottomPopUpView(instructionString: AppResources.VatInstructions, checkBoxString: AppResources.VatInstructionsCheckBoxDesc, continueString: AppResources.VatInstalmetPlanTitle,
                                    _dialogType: ZakatInstalmentViewModel.InstructionsBottomPopUpViewModel.DialogType
                                    .Instructions));
                                }


                            }
                            else {
                                await PopupNavigation.Instance.PushAsync(new InstructionsBottomPopUpView(instructionString: AppResources.VatInstructions, checkBoxString: AppResources.VatInstructionsCheckBoxDesc, continueString: AppResources.VatInstalmetPlanTitle,
                                    _dialogType: ZakatInstalmentViewModel.InstructionsBottomPopUpViewModel.DialogType
                                    .Instructions));
                            }



                            BindVATSelectionView();
                            BindBillsListView();

                            if (VatInstalments.d.Xstep1Conf != null)
                            {
                                if (VatInstalments.d.Xstep1Conf == "1")
                                {
                                    IsInstrunctionChecked = true;

                                }
                                if (vATInstalment.d.Xstep1Conf == "0")
                                {
                                    IsInstrunctionChecked = false;
                                }

                            }

                            if (App.selectedVATItem != "")
                            {

                                if (App.selectedVATItemFbust == "E0075" || App.selectedVATItemFbust == "E0074" || App.selectedVATItemFbust == "E0018" || App.selectedVATItemFbust == "E0013")
                                {
                                    MessagingCenter.Send<Object, string>(this, "RejectScenario", App.selectedVATItem);
                                    if (VatInstalments.d.Noofinstallment != null)
                                    {
                                        if (int.Parse(VatInstalments.d.Noofinstallment) > 0)
                                        {
                                            NoOfInstalments = int.Parse(VatInstalments.d.Noofinstallment);
                                        }
                                        else
                                        {
                                            NoOfInstalments = 2;
                                        }


                                    }


                                    if (App.selectedVATItem != "")
                                    {

                                        if (VatInstalments.d.AttachmentSet.results.Count > 0)
                                        {

                                            AttachmentsListViewData = new ObservableCollection<Attachment>();

                                            var attch = new ObservableCollection<Attachment>();

                                            foreach (var attachment in VatInstalments.d.AttachmentSet.results)
                                            {

                                                if (attachment.Dotyp == "ZVTA")
                                                {

                                                    if (string.IsNullOrEmpty(attachment.Filename))
                                                    {
                                                        attachment.Filename = DateTime.Now.ToString("yyyy/MM/dd");
                                                    }
                                                    attch.Add(attachment);
                                                }


                                            }


                                            AttachmentsListViewData = attch;

                                        }

                                    }
                                }


                            }


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
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
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
                VatInstalments.d.Xstep1Conf = "1";
                VatInstalments.d.Xstep2Conf = "1";
                if (NoOfInstalments == 0)
                {

                    VatInstalments.d.Noofinstallment = "2";
                }
                else
                {

                    VatInstalments.d.Noofinstallment = NoOfInstalments.ToString();
                }

                //Double dueAmount = Double.Parse(VatInstalments.d.TotInvAmt) + Double.Parse(VatInstalments.d.Peneltyamt);
                //VatInstalments.d.Totdueamt = Math.Round(dueAmount, 2).ToString();
                VatInstalments.d.UserTypz = "TP";

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
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }


        }
        #endregion

        #region MAP_VAT_RequestObject

        public VatInstalmentPlanRequest BuildRequestObject()
        {
            VatInstalmentPlanRequest request = new VatInstalmentPlanRequest();
            request.d = new VATInstalmentRequest();
            request.d.Appchkbox = VatInstalments.d.Appchkbox;
            request.d.Begdaz = VatInstalments.d.Begdaz;
            request.d.Betrw = VatInstalments.d.Betrw;
            request.d.DataVersion = VatInstalments.d.DataVersion;
            request.d.Decflg = VatInstalments.d.Decflg;
            request.d.Enddaz = VatInstalments.d.Enddaz;
            request.d.Euser = VatInstalments.d.Euser;
            request.d.EvStatus = VatInstalments.d.EvStatus;
            request.d.Fbnumz = VatInstalments.d.Fbnumz;
            request.d.FormGuid = VatInstalments.d.FormGuid;
            request.d.Formprocz = VatInstalments.d.Formprocz;
            request.d.Gpartz = VatInstalments.d.Gpartz;
            request.d.Langz = VatInstalments.d.Langz;
            request.d.Mandt = VatInstalments.d.Mandt;
            request.d.Officer = VatInstalments.d.Officer;
            request.d.OfficerTz = VatInstalments.d.OfficerTz;
            request.d.Officerz = VatInstalments.d.Officerz;
            request.d.Operationz = VatInstalments.d.Operationz;
            request.d.Partner = VatInstalments.d.Partner;
            request.d.Partnernm = VatInstalments.d.Partnernm;
            request.d.Periodkeyz = VatInstalments.d.Periodkeyz;
            request.d.PortalUsrz = VatInstalments.d.PortalUsrz;
            request.d.ReturnId = VatInstalments.d.ReturnId;
            request.d.ReturnIdz = VatInstalments.d.ReturnIdz;
            request.d.SrcAppz = VatInstalments.d.SrcAppz;
            request.d.Statusz = VatInstalments.d.Statusz;



            if (CurrentIndex == 1)
            {
                VatInstalments.d.StepNumberz = "01";
                if (VatInstalments.d.VTISSet.results.Length != 0)
                {
                    request.d.Noofinstallment = VatInstalments.d.VTISSet.results.Length.ToString();
                }
                else
                {
                    request.d.Noofinstallment = "00";
                }

            }
            else if (CurrentIndex == 2)
            {
                VatInstalments.d.StepNumberz = "03";
                if (VatInstalments.d.VTISSet.results.Length != 0)
                {
                    request.d.Noofinstallment = VatInstalments.d.VTISSet.results.Length.ToString();
                }
                else
                {
                    request.d.Noofinstallment = "00";
                }


            }
            else if (CurrentIndex == 3)
            {
                VatInstalments.d.StepNumberz = "03";

                if (isDraftClicked) {

                    if (VatInstalments.d.VTISSet.results.Length != 0)
                    {
                        request.d.Noofinstallment = VatInstalments.d.VTISSet.results.Length.ToString();
                    }
                    else
                    {
                        request.d.Noofinstallment = "00";
                    }

                }
                else {
                    request.d.Noofinstallment = VatInstalments.d.Noofinstallment;

                }


            }
            else
            {
                request.d.Noofinstallment = VatInstalments.d.Noofinstallment;

                VatInstalments.d.StepNumberz = "04";

            }


            if(VatInstalments.d.Operationz == "04") {

                if (VatInstalments.d.VTISSet.results.Length != 0)
                {
                    request.d.Noofinstallment = VatInstalments.d.Noofinstallment;
                }
                else
                {
                    request.d.Noofinstallment = "00";
                }
            }


                request.d.StepNumberz = VatInstalments.d.StepNumberz;



            if (!String.IsNullOrEmpty(VatInstalments.d.Peneltyamt))
            {
                request.d.Peneltyamt = VatInstalments.d.Peneltyamt.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(VatInstalments.d.Totdueamt))
            {
                request.d.Totdueamt = VatInstalments.d.Totdueamt.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(VatInstalments.d.TotInvAmt))
            {
                request.d.TotInvAmt = VatInstalments.d.TotInvAmt.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(VatInstalments.d.Totliablityamt))
            {
                request.d.Totliablityamt = VatInstalments.d.Totliablityamt.Replace(",", "");
            }




            request.d.TxnTpz = VatInstalments.d.TxnTpz;
            request.d.UserTypz = VatInstalments.d.UserTypz;
            request.d.Vtref = VatInstalments.d.Vtref;
            request.d.Waers = VatInstalments.d.Waers;
            request.d.Xstep1Conf = VatInstalments.d.Xstep1Conf;
            request.d.Xstep2Conf = VatInstalments.d.Xstep2Conf;
            request.d.__metadata = VatInstalments.d.__metadata;
            request.d.VTADSet = VatInstalments.d.VTADSet.results;
            request.d.ATTACHMENTSet = VatInstalments.d.AttachmentSet.results;
            request.d.ATTACHMENTSet.Clear();
            request.d.VTISSet = VatInstalments.d.VTISSet.results;


            if (VatInstalments.d.NotesSet != null && VatInstalments.d.NotesSet.results != null && VatInstalments.d.NotesSet.results.Count != 0)
            {

                string apiDate = VatInstalments.d.NotesSet.results[0].Erfdtz;
                if (!apiDate.Contains("Date"))
                {

                    foreach (var item in VatInstalments.d.NotesSet.results)
                    {

                        DateTime dt1 = Convert.ToDateTime(item.Erfdtz);
                        JsonSerializerSettings microsoftDateFormatSettings2 = new JsonSerializerSettings
                        {
                            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                        };
                        //var jsonDateTime = JsonConvert.SerializeObject(dt, microsoftDateFormatSettings);
                        var jsonDateTime1 = JsonConvert.SerializeObject(dt1.Date, microsoftDateFormatSettings2);
                        string[] dateList1 = jsonDateTime1.Split('+');
                        jsonDateTime1 = Regex.Replace(dateList1[0], "[@,\\.\";'\\\\]", string.Empty);
                        jsonDateTime1 = jsonDateTime1 + ")/";

                        item.Erfdtz = jsonDateTime1;

                    }
                }

            }

            if (App.selectedVATItemFbust == "E0075" || App.selectedVATItemFbust == "E0074" || App.selectedVATItemFbust == "E0018")
            {
                if(!string.IsNullOrEmpty(NotesText))
                {
                    if (VatInstalments.d.NotesSet.results.Count > 0)
                    {
                        NotesSetPost notes = new NotesSetPost();

                        Metadata _metdata = new Metadata();
                        _metdata.uri = "undefined/sap/opu/odata/SAP/ZDP_VTIA_SRV/NOTESSet('00NaN')";
                        _metdata.type = "ZDP_VTIA_SRV.NOTES";
                        _metdata.id = "undefined/sap/opu/odata/SAP/ZDP_VTIA_SRV/NOTESSet('00NaN')";

                        notes.__metadata = _metdata;
                        notes.AttByz = "TP";
                        notes.ElemNo = 0;

                        notes.Erfdtz = null;
                        notes.Erfusrz = "";
                        notes.Lineno = 1;
                        notes.Noteno = (VatInstalments.d.NotesSet.results.Count + 1).ToString();
                        notes.Notenoz = (VatInstalments.d.NotesSet.results.Count + 1).ToString();
                        notes.Rcodez = "VTIA_NOTES";
                        notes.Refnamez = VatInstalments.d.NotesSet.results[0].Refnamez;
                        notes.Tdformat = "";
                        notes.XInvoicez = "";
                        notes.XObsoletez = "";
                        notes.DataVersionz = "00000";
                        notes.Tdline = NotesText;
                        notes.ByGpartz = App.LoginDataRetrieved.TIN;

                        request.d.NOTESSet = new NotesSetPost[VatInstalments.d.NotesSet.results.Count+1];
                        int i = 0;

                        foreach (NotesSetResult notesSetResult1 in VatInstalments.d.NotesSet.results)
                        {
                            var serilizedNotes = JsonConvert.SerializeObject(notesSetResult1);
                            NotesSetPost notesSetLatest = JsonConvert.DeserializeObject<NotesSetPost>(serilizedNotes);
                            request.d.NOTESSet[i] = notesSetLatest;
                            i = i + 1;
                        }

                        request.d.NOTESSet[VatInstalments.d.NotesSet.results.Count] = notes;
                    }
                }
                else
                {
                    request.d.NOTESSet = new NotesSetPost[VatInstalments.d.NotesSet.results.Count];
                    int i = 0;

                    foreach (NotesSetResult notesSetResult1 in VatInstalments.d.NotesSet.results)
                    {
                        var serilizedNotes = JsonConvert.SerializeObject(notesSetResult1);
                        NotesSetPost notesSetLatest = JsonConvert.DeserializeObject<NotesSetPost>(serilizedNotes);
                        request.d.NOTESSet[i] = notesSetLatest;
                        i = i + 1;
                    }
                }

            }
            else
            {
                if (VatInstalments.d.NotesSet.results.Count > 0)
                {

                    if (!string.IsNullOrEmpty(NotesText))
                    {

                        NotesSetPost notes = new NotesSetPost();

                        Metadata _metdata = new Metadata();
                        _metdata.uri = "undefined/sap/opu/odata/SAP/ZDP_VTIA_SRV/NOTESSet('00NaN')";
                        _metdata.type = "ZDP_VTIA_SRV.NOTES";
                        _metdata.id = "undefined/sap/opu/odata/SAP/ZDP_VTIA_SRV/NOTESSet('00NaN')";

                        notes.__metadata = _metdata;
                        notes.AttByz = "TP";
                        notes.ElemNo = 0;

                        notes.Erfdtz = null;
                        notes.Erfusrz = "";
                        notes.Lineno = 1;
                        notes.Noteno = (VatInstalments.d.NotesSet.results.Count + 1).ToString();
                        notes.Notenoz = (VatInstalments.d.NotesSet.results.Count + 1).ToString();
                        notes.Rcodez = "VTIA_NOTES";
                        notes.Refnamez = VatInstalments.d.NotesSet.results[0].Refnamez;
                        notes.Tdformat = "";
                        notes.XInvoicez = "";
                        notes.XObsoletez = "";
                        notes.DataVersionz = "00000";
                        notes.Tdline = NotesText;
                        notes.ByGpartz = App.LoginDataRetrieved.TIN;

                        request.d.NOTESSet = new NotesSetPost[2];

                        NotesSetResult notesSetResult = VatInstalments.d.NotesSet.results[0];

                        var serilizedNotes = JsonConvert.SerializeObject(notesSetResult);
                        NotesSetPost notesSetLatest = JsonConvert.DeserializeObject<NotesSetPost>(serilizedNotes);

                        request.d.NOTESSet[0] = notesSetLatest;
                        request.d.NOTESSet[1] = notes;
                    }
                    else
                    {
                        request.d.NOTESSet = new NotesSetPost[0];
                    }
                }
                else
                {

                    if (!string.IsNullOrEmpty(NotesText))
                    {

                        NotesSetPost notes = new NotesSetPost();

                        Metadata _metdata = new Metadata();
                        _metdata.uri = "undefined/sap/opu/odata/SAP/ZDP_VTIA_SRV/NOTESSet('00NaN')";
                        _metdata.type = "ZDP_VTIA_SRV.NOTES";
                        _metdata.id = "undefined/sap/opu/odata/SAP/ZDP_VTIA_SRV/NOTESSet('00NaN')";

                        notes.__metadata = _metdata;
                        notes.AttByz = "TP";
                        notes.ElemNo = 0;

                        notes.Erfdtz = null;
                        notes.Erfusrz = "";
                        notes.Lineno = 1;
                        notes.Noteno = (VatInstalments.d.NotesSet.results.Count + 1).ToString();
                        notes.Notenoz = (VatInstalments.d.NotesSet.results.Count + 1).ToString();
                        notes.Rcodez = "VTIA_NOTES";
                        notes.Refnamez = "";
                        notes.Tdformat = "";
                        notes.XInvoicez = "";
                        notes.XObsoletez = "";
                        notes.DataVersionz = "00000";
                        notes.Tdline = NotesText;
                        notes.ByGpartz = App.LoginDataRetrieved.TIN;

                        request.d.NOTESSet = new NotesSetPost[1];
                        request.d.NOTESSet[0] = notes;


                    }
                    else
                    {

                        request.d.NOTESSet = new NotesSetPost[0];

                    }

                }
            }



            for (int i = 0; i < selectedList.Count; i++)
            {
                var dataItem = selectedList[i] as VATResults4;


                int index = VatInstalments.d.VTIASet.results.ToList().FindIndex(item => item.SadadNo == dataItem.SadadNo);

                VatInstalments.d.VTIASet.results[index].Xsele = "X";


            }


            //if (selectedList.Contains(dataItem.SadadNo))
            //    {
            //    VatInstalments.d.VTIASet.results[i].Xsele = "X";

            //    }
            //    else
            //    {
            //    VatInstalments.d.VTIASet.results[i].Xsele = "";

            //    }


            //}

            request.d.VTIASet = VatInstalments.d.VTIASet.results;


            if (VatInstalments.d.VTISSet.results.Length != 0)
            {

                string apiDate = VatInstalments.d.VTISSet.results[0].Faedn;
                if (!apiDate.Contains("Date"))
                {

                    for (int i = 0; i < VatInstalments.d.VTISSet.results.Length; i++)
                    {

                        string dateformat = "dd-MM-yyyy";

                        if (App.IsArabic)
                        {

                            dateformat = "yyyy-MM-dd";
                        }
                        else
                        {
                            dateformat = "dd-MM-yyyy";
                        }


                        string DateAsString;

                        DateTime ValidDate = DateTime.Now;


                        CultureInfo provider = CultureInfo.InvariantCulture;

                        DateAsString = VatInstalments.d.VTISSet.results[i].Faedn;  //which is in the format dd/MM/yyyy


                        try
                        {
                            ValidDate = DateTime.ParseExact(DateAsString, dateformat, provider);
                        }
                        catch (Exception )
                        {
                            ValidDate = DateTime.ParseExact(DateAsString, dateformat.Replace("MM", "M"), provider);
                        }

                        //  DateTime dt = DateTime.ParseExact(VatInstalments.d.VTISSet.results[i].Faedn, dateformat, provider);

                        //DateTime dt = Convert.ToDateTime(VatInstalments.d.VTISSet.results[i].Faedn);
                        JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                        {
                            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                        };
                        //var jsonDateTime = JsonConvert.SerializeObject(dt, microsoftDateFormatSettings);
                        var jsonDateTime = JsonConvert.SerializeObject(ValidDate.Date, microsoftDateFormatSettings);
                        string[] dateList = jsonDateTime.Split('+');
                        jsonDateTime = dateList[0].Replace("\"\\", "");
                        jsonDateTime = jsonDateTime + ")/";
                        VatInstalments.d.VTISSet.results[i].Faedn = jsonDateTime;

                    }
                }


            }


            return request;

        }

        #endregion

        #region ApiIntegration

        public async Task<VatInstalmentPlanResponse> SubmitClicked()
        {
            VatInstalmentPlanResponse response = new VatInstalmentPlanResponse();
            VatInstalmentPlanRequest request = new VatInstalmentPlanRequest();

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

                response = await VATInstalationPlanWebServiceManager.SaveVATInstalmentData(request);
                PopToRootPage();
                if (response != null && response.d != null)
                {
                    try
                    {
                        if (response != null && response.d != null)
                        {
                            //if (response.d.Operationz.Equals("04"))
                            //{
                            //    string number = response.d.Fbnumz;
                            //    string displayMessage = AppResources.VATRSuccessFullVoidMessage + " " + number;
                            //    await _dialogService.ShowMessage(displayMessage, AppResources.Information);
                            //    _navigationService.GoBack();
                            //}
                            //if (response.d.Operationz.Equals("05"))
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
                        Console.Write(ex.ToString());
                        Console.Write(ex.StackTrace.ToString());
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
                    _navigationService.GoBack();

                });
                return response;
            }

            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                return response;
            }

        }

        #endregion


        #region Download Confirmation

        public void downloadConfirmation()
        {
            var localPath = string.Empty;
            Stream stream = null;
            try
            {

                var dependency = DependencyService.Get<ILocalFileProvider>();
                if (dependency == null)
                {
                    // DisplayAlert("Error loading PDF", "Computer says no", "OK");
                    return;
                }
                var fileName = Guid.NewGuid().ToString();
                // Download PDF locally for viewing
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    try
                    {

                        String downloadurl = Constants.downloadFile + "'" + VATReferanceNumber + "')/$value";

                        // String downloadurl = "http://www.africau.edu/images/default/sample.pdf";

                        StreamForDownloadURL = client.OpenRead(downloadurl);
                        BinaryReader br = new BinaryReader(StreamForDownloadURL);
                        byte[] result = br.ReadBytes((int)StreamForDownloadURL.Length);
                        string strBase64 = Convert.ToBase64String(result);
                        if (string.IsNullOrEmpty(strBase64) != true)
                        {
                            byte[] sPDFDecoded = Convert.FromBase64String(strBase64);
                            stream = new MemoryStream(sPDFDecoded);
                            StreamForDownloadURL = stream;
                        }
                        localPath =
                      Task.Run(() => dependency.SaveFileToDisk(StreamForDownloadURL, $"{fileName}.pdf")).Result;
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.ToString());
                        Console.Write(ex.StackTrace.ToString());
                    }

                    //    using (var httpClient = new HttpClient())
                    //{
                    //    var pdfStream = Task.Run(() => httpClient.GetStreamAsync("https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/ZDP_IT_CORRES_MOB_NEW_SRV/corr_dataSet(Cokey='C4346B23F48E1ED982858E704178C406',Cotyp='ZVT3')/$value")).Result;
                    //    localPath =
                    //        Task.Run(() => dependency.SaveFileToDisk(pdfStream, $"{fileName}.pdf")).Result;
                    //}
                    if (string.IsNullOrWhiteSpace(localPath))
                    {
                        //   DisplayAlert("Error loading PDF", "Computer says no", "OK");
                        return;
                    }
                }
                if (Device.RuntimePlatform == Device.Android)
                {
                    PathOfPdf = $"file:///android_asset/pdfjs/web/viewer.html?file={"file:///" + WebUtility.UrlEncode(localPath)}";

                }
                else
                {

                }


            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                throw ex;
            }
        }

        #endregion
    }
}
