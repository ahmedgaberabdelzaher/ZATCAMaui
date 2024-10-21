using System.Collections.ObjectModel;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Input;


using Newtonsoft.Json;
using Mopups.Services;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.VATInstalmentModels;
using ZATCAMAUI.ViewModel.NewDesignViewModel.Instructions;
using ZATCAMAUI.Views.NewDesign.Common;
using ZATCAMAUI.Views.NewDesign.VATDeclarationPages;
using ZATCAMAUI.Views.NewDesign.VatInstalmentPlan;
using Metadata = ZATCAMAUI.Models.VATInstalmentModels.Metadata;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.VATInstalmentPlanViewModel;


public class VATInstalmentPlanViewModel : BaseViewModel
{
    #region Variable
    public System.Collections.Generic.List<VATResults4> selectedList = new System.Collections.Generic.List<VATResults4>();
    public string currencyUnits = AppResources.ZSAR;

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
            OnPropertyChanged("VATDueAmount");
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
            OnPropertyChanged("SecondTerms");
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
            OnPropertyChanged("VATPenalityAmount");
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
            OnPropertyChanged("VATLiabilityAmount");
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
            OnPropertyChanged("VATBillDueAmount");
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
            OnPropertyChanged("IsViewEnable");
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
            OnPropertyChanged("IsDraftRecord");
        }
    }
    public bool MarkComplete { get; private set; } = false;
    public int MaxIndex { get; private set; } = 6;
    private List<VATResults4> _billsListVATData;
    public List<VATResults4> BillsListVATData
    {
        get
        {
            return _billsListVATData;
        }
        set
        {
            if (_billsListVATData == value) return;
            _billsListVATData = value;
            OnPropertyChanged("BillsListVAT");
        }
    }
    private List<VATResults4> _billsListVAT;
    public List<VATResults4> BillsListVAT
    {
        get
        {
            return _billsListVAT;
        }
        set
        {
            if (_billsListVAT == value) return;
            _billsListVAT = value;
            OnPropertyChanged("BillsListVAT");
        }
    }

    private List<VATResults3> _statementList;
    public List<VATResults3> StatementList
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
            OnPropertyChanged("isNoDataLableVisible");
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
            OnPropertyChanged("IsBackButtonVisible");
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
            OnPropertyChanged("IsSelectionViewEnabled");
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
            OnPropertyChanged("SelectedOutletOptionIndex");
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
            OnPropertyChanged("IsAgreementViewEnabled");
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
            OnPropertyChanged("IsOutletViewEnabled");
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
            OnPropertyChanged("IsBillsViewEnabled");
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
            OnPropertyChanged("IsVATBillsViewEnabled");
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
            OnPropertyChanged("IsAttachmentsViewEnabled");
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
            OnPropertyChanged("IsStatementViewEnabled");
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
            OnPropertyChanged("IsSummaryViewEnabled");
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
            IsBillContinueBackGroundColor = (_isBillContinueEnabled ? (Color)Application.Current.Resources["Secondary"] : (Color)Application.Current.Resources["ButtonGray"]);
            OnPropertyChanged("IsBillContinueEnabled");
        }
    }
    private Color _isBillContinueBackGroundColor = (Color)Application.Current.Resources["Secondary"];
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
            OnPropertyChanged("IsBillContinueBackGroundColor");
        }
    }

    private Color _isSelectionContinueBackGroundColor = (Color)Application.Current.Resources["Secondary"];

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
            IsSelectionContinueBackGroundColor = (_IsFirstCheckboxChecked ? (Color)Application.Current.Resources["Secondary"] : (Color)Application.Current.Resources["ButtonGray"]);
            OnPropertyChanged("IsFirstCheckboxChecked");
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
            OnPropertyChanged("IsSelectionContinueBackGroundColor");
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
            OnPropertyChanged("IsSucessViewEnabled");
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
            OnPropertyChanged("IsVATAmountVisible");
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
            OnPropertyChanged("IsZakatSelected");
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
            OnPropertyChanged("IsIncomeTaxViewEnabled");
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
            OnPropertyChanged("IsSubIncomeTaxViewEnabled");
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
            OnPropertyChanged("SelectedFilterZakat");
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
            OnPropertyChanged("ListZAKATCorrespondance");
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
            OnPropertyChanged("TxtSelectedStatusZakat");
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
            OnPropertyChanged("VATReferanceNumber");
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
            OnPropertyChanged("VATCustomerName");
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
            OnPropertyChanged("CorresFilterZakat");
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
            OnPropertyChanged("SubSelectedFilterZakat");
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
            OnPropertyChanged("SubListZAKATCorrespondance");
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
            OnPropertyChanged("SubTxtSelectedStatusZakat");
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
            OnPropertyChanged("SubSelectedFilterZakatPrev");
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
            OnPropertyChanged("SubCorresFilterZakat");
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
            OnPropertyChanged("SubSetSelectedIndexZakat");
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
            OnPropertyChanged("PathOfPdf");
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
            OnPropertyChanged("NotesText");
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
                billNumber = AppResources.Bill + " " + (i + 1).ToString("00") + ":",
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
            OnPropertyChanged("InstalmentPlans");
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
            OnPropertyChanged("VATInstalmentPlanModel");
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
    public ObservableCollection<VATResults4> selectedBillsList { get; set; }
    public ObservableCollection<VATResults4> SelectedBillsList
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
            OnPropertyChanged("ListOfActionButtonsApplicable");
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
            OnPropertyChanged("SelectedOutletOption");
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
            OnPropertyChanged("VatInstalments");
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
            OnPropertyChanged("VatInstalment");
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
            OnPropertyChanged("IsAttachmentsListVisible");
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
    public string _monthlyInstalment = "0.00";

    public string MonthlyInstalment
    {
        set
        {
            if (_monthlyInstalment == value) return;

            if (_monthlyInstalment != value)
            {
                _monthlyInstalment = value;
                OnPropertyChanged("MonthlyInstalment");
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
                OnPropertyChanged("MaxInstalmentsTitle");
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
                OnPropertyChanged("MinInstalmentsTitle");
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
            OnPropertyChanged("StreamForDownloadURL");
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


    private async Task ShowMoreOptionsPopUp()
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


    public async Task VATSetReturnVoidAsync()
    {
        VatInstalments.d.Operationz = "04";
        VatInstalments.d.Decflg = "1";

        try
        {
            IsLoading = true;
            if (!isDraftClicked)
            {
                isDraftClicked = true;
                setDATA();
                var VatInstalmentData = await SubmitClicked();
                isDraftClicked = false;
                if (VatInstalmentData != null && VatInstalmentData.d != null)
                {
                    VatInstalments = VatInstalmentData;

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

                    await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                    _navigationService.GoBack();
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
                            _navigationService.GoBack();
                        });
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
            IsLoading = false;
        }
        catch (Exception)
        {


        }
    }

    public async Task VATReturnAddNote()
    {
        await MopupService.Instance.PushAsync(new AddNotesPopupPageView(NotesText, true));
    }

    public async Task VATReturnGetNotes()
    {
        await MopupService.Instance.PushAsync(new ViewNotesPopUpPageView(_vatInstalments.d.NotesSet));
    }

    public bool isDraftClicked = false;
    public async Task OnSaveDraftClicked()
    {

        VatInstalments.d.Operationz = "05";
        VatInstalments.d.Decflg = "1";


        try
        {
            IsLoading = true;
            if (!isDraftClicked)
            {
                isDraftClicked = true;
                setDATA();

                var VatInstalmentData = await SubmitClicked();

                if (VatInstalmentData != null && VatInstalmentData.d != null)
                {
                    VatInstalments = VatInstalmentData;

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

                    await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                }
                else
                {

                    if (string.IsNullOrEmpty(WebServiceManager.ErrorMessageForVAT))
                    {
                        await Task.Run(() =>
                        {
                            IsLoading = false;
                        });
                        IsLoading = false;
                        await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    }
                    else
                    {
                        IsLoading = false;
                        await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    }
                }
            }
            IsLoading = false;
        }
        catch (Exception)
        {


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

    public VATInstalmentPlanViewModel(INavigationService navigationService, IDialogService dialogService):base(navigationService,dialogService)
    {
        GoBackClick = new Command(() =>
        {
            Backnavigations();
        });

        CloseClick = new Command(() =>
        {
            _navigationService.GoBack();
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
        else
        {

            IsFirstCheckboxChecked = false;

        }

        VATInstalationClicked = new Command(async () => await VATInstalationTapped());
        ReasonContinueBtnTapped = new Command(async () => await ReasonContinueBtnClicked());
        AggrementContinueBtnTapped = new Command(async () => await AggrementContinueBtnClicked());
        BillContinueBtnTapped = new Command(async () => await BillContinueBtnClicked());
        AttachmentsContinueBtnTapped = new Command(async () => await AttachmentsContinueBtnClicked());
        StatementsContinueBtnTapped = new Command(async() => await StatementsContinueBtnClicked());
        SummaryContinueBtnTapped = new Command(async () => await SummaryContinueBtnClicked());
        OnZakatInstalmentReasonTapped = new Command(async () => await OnZakatInstalmentReasonClicked());
        NewAttachmentTapped = new Command(async () => await NewAttachmentClicked());
        SuccessGoToDashboardTapped = new Command(async () => await SuccessGoToDashboardClicked());
        DownloadConfirmationTapped = new Command(async () => await DownloadConfirmationClicked());
        onMoreOptionClicked = new Command(async () =>
        {
            await MopupService.Instance.PushAsync(new MoreMenuPopUpPageViewRTwo(ListOfActionButtonsApplicable));
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


        if (VatInstalments.d.VTIASet != null)
        {
            SelectedBillsList = new ObservableCollection<VATResults4>();
            foreach (VATResults4 bills in VatInstalments.d.VTIASet)
            {
                SelectedBillsList.Add(bills);
            }


            BillsListVAT = VatInstalments.d.VTIASet;
            BillsListVATData = VatInstalments.d.VTIASet;
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
                SelectedBillsList = new ObservableCollection<VATResults4>();

                foreach (VATResults4 bills in VatInstalments.d.VTIASet)
                {
                    SelectedBillsList.Add(bills);
                }

            }
        }
        catch (Exception)
        {


        }
    }

    public void BindStatementsView()
    {
        if (VatInstalments.d.VTISSet != null)
        {

            //StatementList = null;
            var statementList = VatInstalments.d.VTISSet;

            for (int i = 0; i < statementList.Count; i++)
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




                    statementList[i].Faedn = dt1;

                }


            }
            StatementList = statementList;
            VATBillDueAmount = VatInstalments.d.Totdueamt;
            VATPenalityAmount = VatInstalments.d.Peneltyamt;
            if (StatementList != null && StatementList.ToList().Count > 0)
            {

                MonthlyInstalment = VatInstalments.d.VTISSet[0].Betrw;

            }

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
            OnPropertyChanged("FirstTerms");
        }
    }

    public void ResetData()
    {
        EnableSlectionView();
        NoOfInstalments = 2;
        IsLoading = false;
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

    public async Task VATInstalationTapped()
    {
        try
        {
            await MopupService.Instance.PushAsync(new InstructionsBottomPopUpView(instructionString: "Test", checkBoxString: "Test", continueString: "VAT Instalment",
             _dialogType: InstructionsBottomPopUpViewModel.DialogType
                 .Instructions));
        }
        catch (InternetException ex)
        {
            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            _navigationService.GoBack();
        }
    }
    public async Task ReasonContinueBtnClicked()
    {


        try
        {
           
            if (IsFirstCheckboxChecked)
            {
                EnableVATBillView();
            }
            else
            {
                await _dialogService.ShowMessage(AppResources.ZZZZConfirmAndCarryForward, AppResources.Information);
            }


        }
        catch (InternetException ex)
        {
            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            _navigationService.GoBack();
        }




    }

    public async Task BillContinueBtnClicked()
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
        catch (InternetException ex)
        {
            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            _navigationService.GoBack();
        }
    }

    public async Task AggrementContinueBtnClicked()
    {


        isDraftClicked = false;

        if (App.selectedVATItemFbust == "E0075" || App.selectedVATItemFbust == "E0074" || App.selectedVATItemFbust == "E0018")
        {
            EnableStatementsView();
            BindStatementsView();

        }
        else
        {

            try
            { 

                setDATA();

                var amount = TotalAmountSAR.Replace(" SAR", "").Replace(",", "");
                VatInstalments.d.Totliablityamt = amount;
                VatInstalments.d.StepNumberz = "03";
                VatInstalments.d.Decflg = "0";
                VatInstalments.d.Operationz = "10";

                for (int i = 0; i < VatInstalments.d.VTIASet.ToList().Count; i++)
                {


                    VatInstalments.d.VTIASet[i].Xsele = "";

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
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }

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
            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            _navigationService.GoBack();
        }
    }

    public async Task AttachmentsContinueBtnClicked()
    {
        try
        {


            EnableSummaryView();
            PopulateSummaryReasonData();
        }
        catch (InternetException ex)
        {
            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
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

            if (App.selectedVATItem != "")
            {

                if (VatInstalments.d.AttachmentSet.Count > 0)
                {

                    var attch = new ObservableCollection<Attachment>();

                    foreach (var attachment in VatInstalments.d.AttachmentSet)
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

            EnableAttachmentsView();

        }
        catch (InternetException ex)
        {
            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            _navigationService.GoBack();
        }
    }

    private async Task showTermsPopUp()
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

                    await MopupService.Instance.PushAsync(new InstructionsBottomPopUpView(instructionString: AppResources.VatTerms, checkBoxString: AppResources.VatTermsCheckBoxDesc, continueString: AppResources.ZakatInstalmetContinue, isEditable: true,
               _dialogType: InstructionsBottomPopUpViewModel.DialogType
                   .TermsConditions));
                }
                else
                {

                    await MopupService.Instance.PushAsync(new InstructionsBottomPopUpView(instructionString: AppResources.VatTerms, checkBoxString: AppResources.VatTermsCheckBoxDesc, continueString: AppResources.ZakatInstalmetContinue,
                    _dialogType: InstructionsBottomPopUpViewModel.DialogType
                    .TermsConditions));
                }


            }
            else
            {
                await MopupService.Instance.PushAsync(new InstructionsBottomPopUpView(instructionString: AppResources.VatTerms, checkBoxString: AppResources.VatTermsCheckBoxDesc, continueString: AppResources.ZakatInstalmetContinue,
                _dialogType: InstructionsBottomPopUpViewModel.DialogType
                    .TermsConditions));
            }

        }
    }

    public async Task SummaryContinueBtnClicked()
    {
        try
        {
          await  showTermsPopUp();
        }
        catch (InternetException ex)
        {
            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
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
            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            _navigationService.GoBack();
        }
    }
    public async Task NewAttachmentClicked()
    {
        if (MopupService.Instance.PopupStack.Count > 0) return;
        try
        {
            await MopupService.Instance.PushAsync(new FilesUploadPopUpPageView(AttachmentsListViewData.ToList(), WhichAttachment.VATInstalment, VatInstalments.d.ReturnId));

        }
        catch (InternetException ex)
        {
            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            _navigationService.GoBack();
        }
    }
   
    public async Task SuccessGoToDashboardClicked()
    {
        try
        {
            PopToRootPage();



        }
        catch (InternetException ex)
        {
            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            _navigationService.GoBack();
        }
    }

    public async Task DownloadConfirmationClicked()
    {
        try
        {
            downloadConfirmation();

        }
        catch (InternetException ex)
        {
            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
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
        AttachmentsListViewData = attachmentsListViewData;
    }

    #endregion


    #region OnPageLoad

    public async Task OnPageLoad()
    {
        try
        {
            IsLoading = true;
            IsLoading = true;
            VatInstalments = null;
            VatInstalmentPlanResponse vATInstalment = null;
            try
            {
                if (App.selectedVATItem != "")
                {
                    if (App.selectedVATItemFbust == "E0075" || App.selectedVATItemFbust == "E0074" || App.selectedVATItemFbust == "E0018")
                    {
                        IsViewEnable = false;
                    }
                    var selectedItemFormID = await VATInstalationPlanWebServiceManager.GAZTGetFbGuidDetailsInputData(App.LoginDataRetrieved.FbGuid, App.selectedVATItem, App.LoginDataRetrieved.TIN, "E0045", "VTIA");
                    if (selectedItemFormID.d != null)
                    {

                        vATInstalment = await VATInstalationPlanWebServiceManager.GAZTGetVATInstalmentData(selectedItemFormID.d.Fbguid, selectedItemFormID.d.Euser);
                        if (vATInstalment != null)
                        {
                            if (vATInstalment.d.NotesSet != null && vATInstalment.d.NotesSet.Count > 0)
                            {
                                int notesCount = vATInstalment.d.NotesSet.Count;
                                var notesText = vATInstalment.d.NotesSet[notesCount - 1];
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

                    if (App.selectedVATItemFbust == "E0013")
                    {

                        foreach (var notes in VatInstalments.d.NotesSet)
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

                if (VatInstalments != null && VatInstalments.d != null)
                {

                    if (App.selectedVATItem != "")
                    {
                        if (App.selectedVATItemFbust == "E0075" || App.selectedVATItemFbust == "E0074" || App.selectedVATItemFbust == "E0018" || App.selectedVATItemFbust == "E0013")
                        {
                            await MopupService.Instance.PushAsync(new InstructionsBottomPopUpView(instructionString: AppResources.VatInstructions, checkBoxString: AppResources.VatInstructionsCheckBoxDesc, continueString: AppResources.VatInstalmetPlanTitle, isEditable: true, _dialogType: InstructionsBottomPopUpViewModel.DialogType
                    .Instructions));
                        }
                        else
                        {

                            await MopupService.Instance.PushAsync(new InstructionsBottomPopUpView(instructionString: AppResources.VatInstructions, checkBoxString: AppResources.VatInstructionsCheckBoxDesc, continueString: AppResources.VatInstalmetPlanTitle,
                            _dialogType: InstructionsBottomPopUpViewModel.DialogType
                            .Instructions));
                        }


                    }
                    else
                    {
                        await MopupService.Instance.PushAsync(new InstructionsBottomPopUpView(instructionString: AppResources.VatInstructions, checkBoxString: AppResources.VatInstructionsCheckBoxDesc, continueString: AppResources.VatInstalmetPlanTitle,
                            _dialogType: InstructionsBottomPopUpViewModel.DialogType
                            .Instructions));
                    }



                    BindVATSelectionView();
                    BindBillsListView();

                    if (VatInstalments.d.Xstep1Conf != null)
                    {
                        if (VatInstalments.d.Xstep1Conf == "confirm")
                        {
                            IsInstrunctionChecked = true;

                        }
                        if (vATInstalment.d.Xstep1Conf == "not confirm")
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

                                if (VatInstalments.d.AttachmentSet.Count > 0)
                                {

                                    AttachmentsListViewData = new ObservableCollection<Attachment>();

                                    var attch = new ObservableCollection<Attachment>();

                                    foreach (var attachment in VatInstalments.d.AttachmentSet)
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
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                }
                IsLoading = false;
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                IsLoading = false;
                _navigationService.GoBack();
            }
            IsLoading = false;
        }
        catch (GAZTVATRegistrationInProcessException ex)
        {
            IsLoading = false;
            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            _navigationService.GoBack();

        }
        catch (Exception)
        {


            IsLoading = false;
            await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
            _navigationService.GoBack();
        }
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
            
            VatInstalments.d.Xstep1Conf = "confirm";
            VatInstalments.d.Xstep2Conf = "confirm";
            if (NoOfInstalments == 0)
            {

                VatInstalments.d.Noofinstallment = "2";
            }
            else
            {

                VatInstalments.d.Noofinstallment = NoOfInstalments.ToString();
            }

           
            VatInstalments.d.UserTypz = "TP";

           


        }
        catch (Exception)
        {


        }


    }
    #endregion

    #region MAP_VAT_RequestObject

    public VatInstalmentPlanRequest BuildRequestObject()
    {
        VatInstalmentPlanRequest request = new VatInstalmentPlanRequest();
        request = new VatInstalmentPlanRequest();
        request.Appchkbox = VatInstalments.d.Appchkbox;
        request.Begdaz = VatInstalments.d.Begdaz;
        request.Betrw = VatInstalments.d.Betrw;
        request.DataVersion = VatInstalments.d.DataVersion;
        request.Decflg = VatInstalments.d.Decflg;
        request.Enddaz = VatInstalments.d.Enddaz;
        request.Euser = VatInstalments.d.Euser;
        request.EvStatus = VatInstalments.d.EvStatus;
        request.Fbnumz = VatInstalments.d.Fbnumz;
        request.FormGuid = VatInstalments.d.FormGuid;
        request.Formprocz = VatInstalments.d.Formprocz;
        //request.Gpartz = VatInstalments.d.Gpartz;
        request.Langz = VatInstalments.d.Langz;
        request.Mandt = VatInstalments.d.Mandt;
        request.Officer = VatInstalments.d.Officer;
        request.OfficerTz = VatInstalments.d.OfficerTz;
        request.Officerz = VatInstalments.d.Officerz;
        request.Operationz = VatInstalments.d.Operationz;
        request.Partner = VatInstalments.d.Partner;
        request.Partnernm = VatInstalments.d.Partnernm;
        request.Periodkeyz = VatInstalments.d.Periodkeyz;
        request.PortalUsrz = VatInstalments.d.PortalUsrz;
        request.ReturnId = VatInstalments.d.ReturnId;
        //request.ReturnIdz = VatInstalments.d.ReturnIdz;
        request.SrcAppz = VatInstalments.d.SrcAppz;
        request.Statusz = VatInstalments.d.Statusz;



        if (CurrentIndex == 1)
        {
            VatInstalments.d.StepNumberz = "01";
            if (VatInstalments.d.VTISSet.Count != 0)
            {
                request.Noofinstallment = VatInstalments.d.VTISSet.Count.ToString();
            }
            else
            {
                request.Noofinstallment = "00";
            }

        }
        else if (CurrentIndex == 2)
        {
            VatInstalments.d.StepNumberz = "03";
            if (VatInstalments.d.VTISSet.Count != 0)
            {
                request.Noofinstallment = VatInstalments.d.VTISSet.Count.ToString();
            }
            else
            {
                request.Noofinstallment = "00";
            }


        }
        else if (CurrentIndex == 3)
        {
            VatInstalments.d.StepNumberz = "03";

            if (isDraftClicked)
            {

                if (VatInstalments.d.VTISSet.Count != 0)
                {
                    request.Noofinstallment = VatInstalments.d.VTISSet.Count.ToString();
                }
                else
                {
                    request.Noofinstallment = "00";
                }

            }
            else
            {
                request.Noofinstallment = VatInstalments.d.Noofinstallment;

            }


        }
        else
        {
            request.Noofinstallment = VatInstalments.d.Noofinstallment;

            VatInstalments.d.StepNumberz = "04";

        }


        if (VatInstalments.d.Operationz == "04")
        {

            if (VatInstalments.d.VTISSet.Count != 0)
            {
                request.Noofinstallment = VatInstalments.d.Noofinstallment;
            }
            else
            {
                request.Noofinstallment = "00";
            }
        }


        request.StepNumberz = VatInstalments.d.StepNumberz;



        if (!String.IsNullOrEmpty(VatInstalments.d.Peneltyamt))
        {
            request.Peneltyamt = VatInstalments.d.Peneltyamt.Replace(",", "");
        }
        if (!String.IsNullOrEmpty(VatInstalments.d.Totdueamt))
        {
            request.Totdueamt = VatInstalments.d.Totdueamt.Replace(",", "");
        }
        if (!String.IsNullOrEmpty(VatInstalments.d.TotInvAmt))
        {
            request.TotInvAmt = VatInstalments.d.TotInvAmt.Replace(",", "");
        }
        if (!String.IsNullOrEmpty(VatInstalments.d.Totliablityamt))
        {
            request.Totliablityamt = VatInstalments.d.Totliablityamt.Replace(",", "");
        }




        request.TxnTpz = VatInstalments.d.TxnTpz;
        request.UserTypz = VatInstalments.d.UserTypz;
        request.Vtref = VatInstalments.d.Vtref;
        request.Waers = VatInstalments.d.Waers;
        request.Xstep1Conf = VatInstalments.d.Xstep1Conf;
        request.Xstep2Conf = VatInstalments.d.Xstep2Conf;
        request.__metadata = VatInstalments.d.__metadata;
        request.VTADSet = VatInstalments.d.VTADSet;
        request.ATTACHMENTSet = VatInstalments.d.AttachmentSet;
        request.ATTACHMENTSet.Clear();
        request.VTISSet = VatInstalments.d.VTISSet;


        if (VatInstalments.d.NotesSet != null && VatInstalments.d.NotesSet != null && VatInstalments.d.NotesSet.Count != 0)
        {

            string apiDate = VatInstalments.d.NotesSet[0].Erfdtz;
            if (apiDate != null && !apiDate.Contains("Date"))
            {

                foreach (var item in VatInstalments.d.NotesSet)
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
            if (!string.IsNullOrEmpty(NotesText))
            {
                if (VatInstalments.d.NotesSet.Count > 0)
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
                    notes.Noteno = (VatInstalments.d.NotesSet.Count + 1).ToString();
                    notes.Notenoz = (VatInstalments.d.NotesSet.Count + 1).ToString();
                    notes.Rcodez = "VTIA_NOTES";
                    notes.Refnamez = VatInstalments.d.NotesSet[0].Refnamez;
                    notes.Tdformat = "";
                    notes.XInvoicez = "";
                    notes.XObsoletez = "";
                    notes.DataVersionz = "00000";
                    notes.Tdline = NotesText;
                    notes.ByGpartz = App.LoginDataRetrieved.TIN;

                    request.NOTESSet = new NotesSetPost[VatInstalments.d.NotesSet.Count + 1];
                    int i = 0;

                    foreach (NotesSetResult notesSetResult1 in VatInstalments.d.NotesSet)
                    {
                        var serilizedNotes = JsonConvert.SerializeObject(notesSetResult1);
                        NotesSetPost notesSetLatest = JsonConvert.DeserializeObject<NotesSetPost>(serilizedNotes);
                        request.NOTESSet[i] = notesSetLatest;
                        i = i + 1;
                    }

                    request.NOTESSet[VatInstalments.d.NotesSet.Count] = notes;
                }
            }
            else
            {
                request.NOTESSet = new NotesSetPost[VatInstalments.d.NotesSet.Count];
                int i = 0;

                foreach (NotesSetResult notesSetResult1 in VatInstalments.d.NotesSet)
                {
                    var serilizedNotes = JsonConvert.SerializeObject(notesSetResult1);
                    NotesSetPost notesSetLatest = JsonConvert.DeserializeObject<NotesSetPost>(serilizedNotes);
                    request.NOTESSet[i] = notesSetLatest;
                    i = i + 1;
                }
            }

        }
        else
        {
            if (VatInstalments.d.NotesSet.Count > 0)
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
                    notes.Noteno = (VatInstalments.d.NotesSet.Count + 1).ToString();
                    notes.Notenoz = (VatInstalments.d.NotesSet.Count + 1).ToString();
                    notes.Rcodez = "VTIA_NOTES";
                    notes.Refnamez = VatInstalments.d.NotesSet[0].Refnamez;
                    notes.Tdformat = "";
                    notes.XInvoicez = "";
                    notes.XObsoletez = "";
                    notes.DataVersionz = "00000";
                    notes.Tdline = NotesText;
                    notes.ByGpartz = App.LoginDataRetrieved.TIN;

                    request.NOTESSet = new NotesSetPost[2];

                    NotesSetResult notesSetResult = VatInstalments.d.NotesSet[0];

                    var serilizedNotes = JsonConvert.SerializeObject(notesSetResult);
                    NotesSetPost notesSetLatest = JsonConvert.DeserializeObject<NotesSetPost>(serilizedNotes);

                    request.NOTESSet[0] = notesSetLatest;
                    request.NOTESSet[1] = notes;
                }
                else
                {
                    request.NOTESSet = new NotesSetPost[0];
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
                    notes.Noteno = (VatInstalments.d.NotesSet.Count + 1).ToString();
                    notes.Notenoz = (VatInstalments.d.NotesSet.Count + 1).ToString();
                    notes.Rcodez = "VTIA_NOTES";
                    notes.Refnamez = "";
                    notes.Tdformat = "";
                    notes.XInvoicez = "";
                    notes.XObsoletez = "";
                    notes.DataVersionz = "00000";
                    notes.Tdline = NotesText;
                    notes.ByGpartz = App.LoginDataRetrieved.TIN;

                    request.NOTESSet = new NotesSetPost[1];
                    request.NOTESSet[0] = notes;


                }
                else
                {

                    request.NOTESSet = new NotesSetPost[0];

                }

            }
        }



        for (int i = 0; i < selectedList.Count; i++)
        {
            var dataItem = selectedList[i] as VATResults4;


            int index = VatInstalments.d.VTIASet.ToList().FindIndex(item => item.SadadNo == dataItem.SadadNo);

            VatInstalments.d.VTIASet[index].Xsele = "X";


        }

        request.VTIASet = VatInstalments.d.VTIASet;


        if (VatInstalments.d.VTISSet.Count != 0)
        {

            string apiDate = VatInstalments.d.VTISSet[0].Faedn;

            if (apiDate != null && !apiDate.Contains("Date"))
            {

                for (int i = 0; i < VatInstalments.d.VTISSet.Count; i++)
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

                    DateAsString = VatInstalments.d.VTISSet[i].Faedn;  //which is in the format dd/MM/yyyy


                  

                }
            }


        }
        if (VatInstalments.d.Operationz == "10" && VatInstalments.d.VTISSet.Count == 0)
        {
            VATResults3 vATResults3 = new VATResults3();
            vATResults3.Totpaidamt = "0.00";
            VatInstalments.d.VTISSet = new List<VATResults3>
                {
                    vATResults3
                };
            request.VTISSet = VatInstalments.d.VTISSet;
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
            IsLoading = true;


            request = BuildRequestObject();

            response = await VATInstalationPlanWebServiceManager.SaveVATInstalmentData(request);
            PopToRootPage();
            if (response != null && response.d != null)
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
            IsLoading = false;

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

           
            IsLoading = true;
            try
            {

                string downloadurl = ZATCAConstants.downloadFile + VATReferanceNumber;
                _navigationService.NavigateTo(App.PdfView, downloadurl);

               
            }
            catch (Exception)
            {


            }
            IsLoading = false;
            if (string.IsNullOrWhiteSpace(localPath))
            {
                return;
            }

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                PathOfPdf = $"file:///android_asset/pdfjs/web/viewer.html?file={"file:///" + WebUtility.UrlEncode(localPath)}";

            }


        }
        catch (Exception )
        {
        }
    }

    #endregion
}

