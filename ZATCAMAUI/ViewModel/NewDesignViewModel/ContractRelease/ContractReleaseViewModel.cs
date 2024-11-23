using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Windows.Input;
using Newtonsoft.Json;
using Mopups.Services;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.ContractRelease;
using ZATCAMAUI.ViewModel.NewDesignViewModel.Instructions;
using ZATCAMAUI.Views.NewDesign.Common;
using ZATCAMAUI.Views.NewDesign.ContractReleasePages;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using Metadata = ZATCAMAUI.Models.ContractRelease.Metadata;
using ZATCAMAUI.Core.CustomControls;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.ContractRelease;

public class ContractReleaseViewModel : BaseViewModel
{
    #region Enums

    enum PagesEnum
    {
        CrReleaseDetailsView,
        CrAttachmentsView,
        CrRemarksAndDescriptionView,
        CrDeclarationView,
        CrSummaryView,
    }

    #endregion

    #region Commands

    public ICommand ReleaseDetailsConBtnTapped { get; set; }
    public ICommand AttachmentsConBtnTapped { get; set; }
    public ICommand RemarksAndDescConBtnTapped { get; set; }
    public ICommand DeclarationConBtnTapped { get; set; }
    public ICommand SummaryConBtnTapped { get; set; }
    public ICommand ContractInstructionsClicked { get; set; }
    public ICommand GoBackClick { get; set; }
    public ICommand GoBackToReleaseDetails { get; set; }
    public ICommand GoBackToAttachments { get; set; }
    public ICommand GoBackToDeclaration { get; set; }
    public ICommand ShowPicker { get; set; }
    public ICommand ShowStartDatePicker { get; set; }
    public ICommand ShowEndDatePicker { get; set; }

    public ICommand ContractProfitPercentCommand { get; set; }
    public ICommand ProfitEstimatedContractCommand { get; set; }
    public ICommand EstimatedProfitZakatCommand { get; set; }
    public ICommand EstimatedProfitTaxCommand { get; set; }
    public ICommand ValueofZakatDuesCommand { get; set; }
    public ICommand ValueofTaxDuesCommand { get; set; }
    public ICommand TotalDuesCommand { get; set; }
    public ICommand NewContractCopyAttachmentTapped { get; set; }
    public ICommand NewInvoiceAttachmentTapped { get; set; }
    public ICommand ReleaseAmtUnfocused { get; set; }

    public ICommand OnAppearingContractReleaseCommand
    {
        get
        {
            return new Command(async _ =>
            {
                try
                {
                    IsLoading = true;

                    ResetData();
                    PopulateDataInChips();

                    await GetContractReleaseData();

                    await ShowInstructionDialog();



                    ChipGroupSelectedItem = ChipDataFilterlist.Where(x => x.TemplateType == AppResources.NDGregorian).FirstOrDefault();

                    MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) =>
                    {
                        PickerModel = arg;
                        updatePicker();
                    });

                    MessagingCenter.Subscribe<object, AttachmentsList>(this, "AttachmentReceived", (sender, arg) =>
                    {
                        if (arg != null)
                        {
                            PopulateAttachments(arg.results);
                        }
                    });
                    IsLoading = false;
                }
                catch (Exception ex)
                {
                    IsLoading = false;
                }

            });
        }
    }

    public ICommand ChipGroupStatusFilterSelectionCommand
    {
        get
        {
            return new Command(_ =>
            {
                try
                {
                    if (ChipGroupSelectedItem.Text.Equals(AppResources.NDHijri))
                    {
                        IsHijriCal = true;

                        if (TodayDateinHijriEnd != null)
                        {
                            string month = TodayDateinHijriStart[1].ToString();
                            string day = TodayDateinHijriStart[0].ToString();
                            string year = TodayDateinHijriStart[2].ToString();
                            FromDate = year + "/" + month + "/" + day;
                            ToDate = year + "/" + month + "/" + day;
                        }

                    }
                    else
                    {
                        IsHijriCal = false;

                        if (TodayDateEnd != null)
                        {
                            string month = TodayDateStart[1].ToString();
                            string day = TodayDateStart[0].ToString();
                            string year = TodayDateStart[2].ToString();
                            FromDate = year + "/" + month + "/" + day;
                            ToDate = year + "/" + month + "/" + day;
                        }
                    }
                }
                catch (Exception)
                {
                }

            });
        }
    }


    public ICommand ContractNumberUnfocusedCommand
    {
        get
        {
            return new Command(_ =>
            {
                ContractNumber = ContractNumberText;

            });
        }
    }

    public ICommand DownloadAcknowledgementFormCommand
    {
        get
        {
            return new Command(async _ =>
            {

                if (ContractReleaseData1.d.Fbnumz != null)
                {
                    IsLoading = true;
                    string downloadurl = ZATCAConstants.CRDownloadCoverFormFile + ContractReleaseData1.d.Fbnumz;
                    await _navigationService.NavigateTo(App.PdfView, downloadurl);

                    IsLoading = false;
                }

            });
        }
    }
    public ICommand DownloadAcknowledgementCommand
    {
        get
        {
            return new Command(async _ =>
            {

                if (ContractReleaseData1.d.Fbnumz != null)
                {
                    IsLoading = true;
                    string downloadurl = ZATCAConstants.CRDownloadCoverFormFile + ContractReleaseData1.d.Fbnumz;
                    await _navigationService.NavigateTo(App.PdfView, downloadurl);

                    IsLoading = false;
                }

            });
        }
    }

    public ICommand ReferenceNumberCopyCommand
    {
        get
        {
            return new Command(async _ =>
            {
                try
                {
                    if (ContractReleaseData.d.Fbnumz != null)
                    {
                        await Clipboard.SetTextAsync(ContractReleaseData.d.Fbnumz);
                        if (Clipboard.HasText)
                        {
                            var text = await Clipboard.GetTextAsync();
                            await _dialogService.ShowMessageBox(AppResources.CRReferenceNumber + " " + text, AppResources.Copied);
                        }
                    }
                }
                catch (Exception)
                {


                }

            });
        }
    }

    public ICommand ContractNumberCopyCommand
    {
        get
        {
            return new Command(async _ =>
            {
                try
                {
                    if (ContractReleaseData.d.AContNo != null)
                    {
                        await Clipboard.SetTextAsync(ContractReleaseData.d.AContNo);
                        if (Clipboard.HasText)
                        {
                            var text = await Clipboard.GetTextAsync();
                            await _dialogService.ShowMessageBox(AppResources.CRContractingNumber + " " + text, AppResources.Copied);
                        }
                    }
                }
                catch (Exception)
                {


                }

            });
        }
    }

    public ICommand DashboardCommand
    {
        get
        {
            return new Command(async _ =>
            {
                var _navigation = Application.Current.MainPage.Navigation;
                foreach (var item in _navigation.NavigationStack)
                {
                    if (item.GetType().Name == App.ContractReleasePageView)
                    {
                        _navigation.RemovePage(item);
                        break;
                    }
                }

                foreach (var item in _navigation.NavigationStack)
                {
                    if (item.GetType().Name == App.ContractReleaseListPageView)
                    {
                        _navigation.RemovePage(item);
                        break;
                    }
                }
                foreach (var item in _navigation.NavigationStack)
                {
                    if (item.GetType().Name == App.ContractReleaseSuccessPageView)
                    {
                        _navigation.RemovePage(item);
                        break;
                    }
                }
                await _navigationService.NavigateTo(App.ContractReleaseListPageView);

            });
        }
    }

    #endregion

    public bool isSubmitted = false;

    private bool _isInvoiceAttachments = true;

    private bool _isBackButtonVisible = true;

    public bool IsBackButtonVisible
    {
        get { return _isBackButtonVisible; }
        set
        {
            if (_isBackButtonVisible == value) return;
            _isBackButtonVisible = value;
            OnPropertyChanged("IsBackButtonVisible");
        }
    }

    private bool _isReleaseDetailsVisible = false;

    public bool IsReleaseDetailsVisible
    {
        get { return _isReleaseDetailsVisible; }
        set
        {
            if (_isReleaseDetailsVisible == value) return;

            _isReleaseDetailsVisible = value;
            OnPropertyChanged("IsReleaseDetailsVisible");
        }
    }


    private bool _attachmentsVisible = false;

    public bool AttachmentsVisible
    {
        get { return _attachmentsVisible; }
        set
        {
            if (_attachmentsVisible == value) return;

            _attachmentsVisible = value;
            OnPropertyChanged("AttachmentsVisible");
        }
    }

    private string contractNumberText;

    public string ContractNumberText
    {
        get { return contractNumberText; }
        set
        {
            if (contractNumberText == value) return;

            contractNumberText = value;
            OnPropertyChanged("ContractNumberText");
        }
    }

    ChipModel chipGroupSelectedItem;
    public ChipModel ChipGroupSelectedItem { get { return chipGroupSelectedItem; } set { chipGroupSelectedItem = value; OnPropertyChanged(); } }


    private bool _remarksAndDescVisible = false;

    public bool RemarksAndDescVisible
    {
        get { return _remarksAndDescVisible; }
        set
        {
            if (_remarksAndDescVisible == value) return;

            _remarksAndDescVisible = value;
            OnPropertyChanged("RemarksAndDescVisible");
        }
    }

    private bool _declarationVisible = false;

    public bool DeclarationVisible
    {
        get { return _declarationVisible; }
        set
        {
            if (_declarationVisible == value) return;

            _declarationVisible = value;
            OnPropertyChanged("DeclarationVisible");
        }
    }

    private bool _isReleaseDetailsEnabled = false;

    public bool IsReleaseDetailsEnabled
    {
        get { return _isReleaseDetailsEnabled; }
        set
        {
            if (_isReleaseDetailsEnabled == value) return;

            _isReleaseDetailsEnabled = value;
            ReleaseDetailsButtonBackGroundColor = (_isReleaseDetailsEnabled ? (Color)Application.Current.Resources["Secondary"] : (Color)Application.Current.Resources["ButtonGray"]);
            OnPropertyChanged("IsReleaseDetailsEnabled");
        }
    }

    private Color _releaseDetailsButtonBackGroundColor = (Color)Application.Current.Resources["Secondary"];
    public Color ReleaseDetailsButtonBackGroundColor
    {
        get
        {
            return _releaseDetailsButtonBackGroundColor;
        }
        set
        {
            if (_releaseDetailsButtonBackGroundColor == value)
            {
                return;
            }
            _releaseDetailsButtonBackGroundColor = value;
            OnPropertyChanged("ReleaseDetailsButtonBackGroundColor");
        }
    }

    private bool _isAttachmentsEnabled = false;

    public bool IsAttachmentsEnabled
    {
        get { return _isAttachmentsEnabled; }
        set
        {
            if (_isAttachmentsEnabled == value) return;

            _isAttachmentsEnabled = value;
            AttachButtonBackGroundColor = (_isAttachmentsEnabled ? (Color)Application.Current.Resources["Secondary"] : (Color)Application.Current.Resources["ButtonGray"]);
            OnPropertyChanged("IsAttachmentsEnabled");
        }
    }

    private Color _attachButtonBackGroundColor = (Color)Application.Current.Resources["Secondary"];
    public Color AttachButtonBackGroundColor
    {
        get
        {
            return _attachButtonBackGroundColor;
        }
        set
        {
            if (_attachButtonBackGroundColor == value)
            {
                return;
            }
            _attachButtonBackGroundColor = value;
            OnPropertyChanged("AttachButtonBackGroundColor");
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
            DeclarationButtonBackGroundColor = (_isDeclarationEnabled ? (Color)Application.Current.Resources["Secondary"] : (Color)Application.Current.Resources["ButtonGray"]);

            OnPropertyChanged("IsDeclarationEnabled");
        }
    }

    private string _charCountDetailDescription = 0 + "/" + 132;
    public string charCountDetailDescription
    {
        get
        {
            return _charCountDetailDescription;
        }
        set
        {
            if (_charCountDetailDescription == value) return;

            _charCountDetailDescription = value;
            OnPropertyChanged("charCountDetailDescription");
        }
    }

    private string _charCountRemarksText = 0 + "/" + 255;
    public string charCountRemarksText
    {
        get
        {
            return _charCountRemarksText;
        }
        set
        {
            if (_charCountRemarksText == value) return;

            _charCountRemarksText = value;
            OnPropertyChanged("charCountRemarksText");
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

    public bool fromDatePicker = false;

    private bool _summaryVisible = false;

    public bool SummaryVisible
    {
        get { return _summaryVisible; }
        set
        {
            if (_summaryVisible == value) return;

            _summaryVisible = value;
            OnPropertyChanged("SummaryVisible");
        }
    }


    private string _infoTitle = "";
    public string InfoTitle
    {
        get
        {
            return _infoTitle;
        }
        set
        {
            _infoTitle = value;
            OnPropertyChanged("InfoTitle");
        }
    }

    private string _infoDesc = "";
    public string InfoDesc
    {
        get
        {
            return _infoDesc;
        }
        set
        {
            _infoDesc = value;
            OnPropertyChanged("InfoDesc");
        }
    }

    private string _pickedContract = "";

    public string PickedContract
    {
        get { return _pickedContract; }
        set
        {
            if (_pickedContract == value) return;

            _pickedContract = value;
            OnPropertyChanged("PickedContract");
        }
    }

    private string _pickedContractId = "";
    public string PickedContractId
    {
        get { return _pickedContractId; }
        set
        {
            if (_pickedContractId == value) return;

            _pickedContractId = value;
            OnPropertyChanged("PickedContractId");
        }
    }

    public double _contractTotalAmount = 0.0;

    public double ContractTotalAmount
    {
        get { return _contractTotalAmount; }
        set
        {
            if (_contractTotalAmount == value) return;

            _contractTotalAmount = value;
            OnPropertyChanged("ContractTotalAmount");
        }
    }

    public string _contractReleaseAmount = "";

    public string ContractReleaseAmount
    {
        get { return _contractReleaseAmount; }
        set
        {
            if (_contractReleaseAmount == value) return;

            _contractReleaseAmount = value;
            OnPropertyChanged("ContractReleaseAmount");
        }
    }

    public double _amountToRelease = 0.0;

    public double AmountToRelease
    {
        get { return _amountToRelease; }
        set
        {
            if (_amountToRelease == value) return;

            _amountToRelease = value;
            OnPropertyChanged("AmountToRelease");
        }
    }


    private double _contractTotalAmountText = 0.0;
    public double ContractTotalAmountText
    {
        get { return _contractTotalAmountText; }
        set
        {
            if (_contractTotalAmountText == value) return;

            _contractTotalAmountText = value;
            OnPropertyChanged("ContractTotalAmountText");
        }
    }


    private double _amountoReleaseTxt = 0.0;
    public double AmountoReleaseTxt
    {
        get { return _amountoReleaseTxt; }
        set
        {
            if (_amountoReleaseTxt == value) return;

            _amountoReleaseTxt = value;
            OnPropertyChanged("AmountoReleaseTxt");
        }
    }

    private double _pickedContractPercent = 0.0;

    public double PickedContractPercent
    {
        get { return _pickedContractPercent; }
        set
        {
            if (_pickedContractPercent == value) return;

            _pickedContractPercent = value;
            OnPropertyChanged("PickedContractPercent");
        }
    }

    private double _profitEstimatedContract = 0.0;

    public double ProfitEstimatedContract
    {
        get { return _profitEstimatedContract; }
        set
        {
            if (_profitEstimatedContract == value) return;

            _profitEstimatedContract = value;
            OnPropertyChanged("ProfitEstimatedContract");
        }
    }

    private double _estimatedProfitForZakatPercent = 0.0;

    public double EstimatedProfitForZakatPercent
    {
        get { return _estimatedProfitForZakatPercent; }
        set
        {
            if (_estimatedProfitForZakatPercent == value) return;

            _estimatedProfitForZakatPercent = value;
            OnPropertyChanged("EstimatedProfitForZakatPercent");
        }
    }

    private double _estimatedProfitForZakatAmount = 0.0;

    public double EstimatedProfitForZakatAmount
    {
        get { return _estimatedProfitForZakatAmount; }
        set
        {
            if (_estimatedProfitForZakatAmount == value) return;

            _estimatedProfitForZakatAmount = value;
            OnPropertyChanged("EstimatedProfitForZakatAmount");
        }
    }

    private double _estimatedProfitForTaxAmount = 0.0;

    public double EstimatedProfitForTaxAmount
    {
        get { return _estimatedProfitForTaxAmount; }
        set
        {
            if (_estimatedProfitForTaxAmount == value) return;

            _estimatedProfitForTaxAmount = value;
            OnPropertyChanged("EstimatedProfitForTaxAmount");
        }
    }

    private double _estimatedProfitForTaxPercent = 100.0;

    public double EstimatedProfitForTaxPercent
    {
        get { return _estimatedProfitForTaxPercent; }
        set
        {
            if (_estimatedProfitForTaxPercent == value) return;

            _estimatedProfitForTaxPercent = value;
            OnPropertyChanged("EstimatedProfitForTaxPercent");
        }
    }

    private double _zakatDues = 0.0;

    public double ZakatDues
    {
        get { return _zakatDues; }
        set
        {
            if (_zakatDues == value) return;

            _zakatDues = value;
            OnPropertyChanged("ZakatDues");
        }
    }

    private double _taxDues = 0.0;

    public double TaxDues
    {
        get { return _taxDues; }
        set
        {
            if (_taxDues == value) return;

            _taxDues = value;
            OnPropertyChanged("TaxDues");
        }
    }

    private double _totalDues = 0.0;

    public double TotalDues
    {
        get { return _totalDues; }
        set
        {
            if (_totalDues == value) return;

            _totalDues = value;
            OnPropertyChanged("TotalDues");
        }
    }

    private string _remarks = "";

    public string Remarks
    {
        get { return _remarks; }
        set
        {
            if (_remarks == value) return;

            _remarks = value;
            OnPropertyChanged("Remarks");
        }
    }

    private string _detailDescription = "";

    public string DetailDescription
    {
        get { return _detailDescription; }
        set
        {
            if (_detailDescription == value) return;

            _detailDescription = value;
            OnPropertyChanged("DetailDescription");
        }
    }

    private string _contactPersonName = "";

    public string ContactPersonName
    {
        get { return _contactPersonName; }
        set
        {
            if (_contactPersonName == value) return;

            _contactPersonName = value;
            OnPropertyChanged("ContactPersonName");
        }
    }

    private string _designation = "";

    public string Designation
    {
        get { return _designation; }
        set
        {
            if (_designation == value) return;

            _designation = value;
            OnPropertyChanged("Designation");
        }
    }

    private string _contractName = "";

    public string ContractName
    {
        get { return _contractName; }
        set
        {
            if (_contractName == value) return;

            _contractName = value;
            OnPropertyChanged("ContractName");
        }
    }

    private string _contractNumber = "";

    public string ContractNumber
    {
        get { return _contractNumber; }
        set
        {
            if (_contractNumber == value) return;

            _contractNumber = value;
            OnPropertyChanged("ContractNumber");
        }
    }
    private bool _IsHijriCal = false;
    public bool IsHijriCal
    {
        get
        {
            return _IsHijriCal;
        }
        set
        {
            if (_IsHijriCal == value) return;

            _IsHijriCal = value;
            OnPropertyChanged("IsHijriCal");
        }
    }
    private string _fromDate = "";

    public string FromDate
    {
        get { return _fromDate; }
        set
        {
            if (_fromDate == value) return;

            _fromDate = value;
            OnPropertyChanged("FromDate");
        }
    }

    private string _toDate = "";

    public string ToDate
    {
        get { return _toDate; }
        set
        {
            if (_toDate == value) return;

            _toDate = value;
            OnPropertyChanged("ToDate");
        }
    }

    private GenericPickerModel _pickerModel { get; set; }

    public GenericPickerModel PickerModel
    {
        get { return _pickerModel; }
        set
        {
            if (_pickerModel == value) return;

            _pickerModel = value;
            OnPropertyChanged("PickerModel");
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

    public bool MarkComplete { get; private set; } = false;
    public int MaxIndex { get; private set; } = 5;

    public ObservableCollection<Attachment> contractCopyAttachmentsListViewData { get; set; }

    public ObservableCollection<Attachment> ContractCopyAttachmentsListViewData
    {
        get { return contractCopyAttachmentsListViewData; }

        set
        {
            if (contractCopyAttachmentsListViewData == value)
            {
                return;
            }

            contractCopyAttachmentsListViewData = value;
            OnPropertyChanged("ContractCopyAttachmentsListViewData");
        }
    }

    public ObservableCollection<Attachment> invoicesAttachmentsListViewData { get; set; }

    public ObservableCollection<Attachment> InvoiceAttachmentsListViewData
    {
        get { return invoicesAttachmentsListViewData; }

        set
        {
            if (invoicesAttachmentsListViewData == value)
            {
                return;
            }

            invoicesAttachmentsListViewData = value;
            OnPropertyChanged("InvoiceAttachmentsListViewData");
        }
    }

    private Dictionary<string, double> ContractTypeDictionary = null;


    private Dictionary<string, string> ContractTypeIdDictionary = null;


    ContractReleaseFormResponse _contractReleaseData;
    public ContractReleaseFormResponse ContractReleaseData { get { return _contractReleaseData; } set { _contractReleaseData = value; OnPropertyChanged(); } }

    ContractReleaseFormResponse1 _contractReleaseData1 = new ContractReleaseFormResponse1();
    public ContractReleaseFormResponse1 ContractReleaseData1 { get { return _contractReleaseData1; } set { _contractReleaseData1 = value; OnPropertyChanged(); } }



    private string _referenceNumberTxt = "";

    public string ReferenceNumberTxt
    {
        get { return _referenceNumberTxt; }
        set
        {
            if (_referenceNumberTxt == value) return;

            _referenceNumberTxt = value;
            OnPropertyChanged("ReferenceNumberTxt");
        }
    }
    private string _contractNumberTxt = "";

    public string ContractNumberTxt
    {
        get { return _contractNumberTxt; }
        set
        {
            if (_contractNumberTxt == value) return;

            _contractNumberTxt = value;
            OnPropertyChanged("ContractNumberTxt");
        }
    }

    private bool isDECCheckBox = false;
    public bool IsDECCheckBox
    {
        get { return isDECCheckBox; }
        set
        {
            if (isDECCheckBox == value) return;

            isDECCheckBox = value;
            OnPropertyChanged("IsDECCheckBox");
        }
    }

    private bool shouldShowAR = false;
    public bool ShouldShowAR
    {
        get { return shouldShowAR; }
        set
        {
            if (shouldShowAR == value) return;

            shouldShowAR = value;
            OnPropertyChanged("ShouldShowAR");
        }
    }
    private bool shouldShowEN = false;
    public bool ShouldShowEN
    {
        get { return shouldShowEN; }
        set
        {
            if (shouldShowEN == value) return;

            shouldShowEN = value;
            OnPropertyChanged("ShouldShowEN");
        }
    }

    private bool _isDeclarationViewEnabled;
    public bool IsDeclarationViewEnabled
    {
        get
        {
            return _isDeclarationViewEnabled;
        }
        set
        {
            if (_isDeclarationViewEnabled == value) return;

            _isDeclarationViewEnabled = value;
            OnPropertyChanged("IsDeclarationViewEnabled");
        }
    }

    private bool _isDeclarationViewEnabledNew;
    public bool IsDeclarationViewEnabledNew
    {
        get
        {
            return _isDeclarationViewEnabledNew;
        }
        set
        {
            if (_isDeclarationViewEnabledNew == value) return;

            _isDeclarationViewEnabledNew = value;
            OnPropertyChanged("IsDeclarationViewEnabledNew");
        }
    }

    public VATDeregDeclaration _vatDeregDeclaration;
    public VATDeregDeclaration VatDeregDeclaration
    {
        get
        {
            return _vatDeregDeclaration;
        }
        set
        {
            if (_vatDeregDeclaration == value) return;

            _vatDeregDeclaration = value;
            OnPropertyChanged("VatDeregDeclaration");
        }
    }

    public string _zterms;
    public string Zterms
    {
        get
        {
            return _zterms;
        }
        set
        {
            if (_zterms == value) return;

            _zterms = value;
            OnPropertyChanged("Zterms");
        }
    }


    int selectedPage = (int)PagesEnum.CrReleaseDetailsView;

    public ContractReleaseInterface contractReleaseInterface { get; set; }

    public ContractReleaseViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
    {

        GoBackClick = new Command(() => { BackNavigations(); });

        GoBackToReleaseDetails = new Command(() => { EnableReleaseDetailsView(); });

        GoBackToAttachments = new Command(() => { EnableAttachmentsView(); });

        GoBackToDeclaration = new Command(() => { EnableDeclarationView(); });

        ContractProfitPercentCommand = new Command(async () =>
        {
            InfoTitle = AppResources.CRContractprofitEstimatedRate;
            InfoDesc = AppResources.CRContractprofitEstimatedRateDesc;
            await MopupService.Instance.PushAsync(new ContractReleaseInfoPopup(Desc: InfoDesc, Title: InfoTitle));
        });
        ProfitEstimatedContractCommand = new Command(async () =>
        {
            InfoTitle = AppResources.CRProfitEstimatedForContract;
            InfoDesc = AppResources.CRProfitEstimatedForContractDesc;
            await MopupService.Instance.PushAsync(new ContractReleaseInfoPopup(Desc: InfoDesc, Title: InfoTitle));
        });
        EstimatedProfitZakatCommand = new Command(async () =>
        {
            InfoTitle = AppResources.CREstimatedProfitforZakat;
            InfoDesc = AppResources.CREstimatedProfitforZakatDesc;
            await MopupService.Instance.PushAsync(new ContractReleaseInfoPopup(Desc: InfoDesc, Title: InfoTitle));
        });
        EstimatedProfitTaxCommand = new Command(async () =>
        {
            InfoTitle = AppResources.CREstimatedProfitforTax;
            InfoDesc = AppResources.CREstimatedProfitforTaxDesc;
            await MopupService.Instance.PushAsync(new ContractReleaseInfoPopup(Desc: InfoDesc, Title: InfoTitle));
        });
        ValueofZakatDuesCommand = new Command(async () =>
        {
            InfoTitle = AppResources.CRTheValueofZakatdues;
            InfoDesc = AppResources.CRTheValueofZakatduesDesc;
            await MopupService.Instance.PushAsync(new ContractReleaseInfoPopup(Desc: InfoDesc, Title: InfoTitle));
        });
        ValueofTaxDuesCommand = new Command(async () =>
        {
            InfoTitle = AppResources.CRTheValueTaxDues;
            InfoDesc = AppResources.CRTheValueTaxDuesDesc;
            await MopupService.Instance.PushAsync(new ContractReleaseInfoPopup(Desc: InfoDesc, Title: InfoTitle));
        });
        TotalDuesCommand = new Command(async () =>
        {
            InfoTitle = AppResources.CRTotalDues;
            InfoDesc = AppResources.CRTotalDuesDesc;
            await MopupService.Instance.PushAsync(new ContractReleaseInfoPopup(Desc: InfoDesc, Title: InfoTitle));
        });

        ShowStartDatePicker = new Command(async () =>
        {
            fromDatePicker = true;
            await showDatePickerDialog(AppResources.CRContractStartDate);

        });

        ShowEndDatePicker = new Command(async () =>
        {
            fromDatePicker = false;
            await showDatePickerDialog(AppResources.CRContractEndDate);

        });

        ShowPicker = new Command(async () => { await showPickerDialog(); });


        ReleaseDetailsConBtnTapped = new Command(async () => await ReleaseDetailsConBtnClicked());
        AttachmentsConBtnTapped = new Command(async () => await AttachmentsConBtnClicked());
        RemarksAndDescConBtnTapped = new Command(async () => await RemarksAndDescConBtnClicked());
        DeclarationConBtnTapped = new Command(async () => await DeclarationConBtnClicked());
        SummaryConBtnTapped = new Command(async () => await SummaryConBtnClicked());
        ContractInstructionsClicked = new Command(async () => await InstructionsTapped());
        NewInvoiceAttachmentTapped = new Command(async () => await NewInvoiceAttachmentClicked());
        NewContractCopyAttachmentTapped = new Command(async () => await NewContractCopyAttachmentClicked());
        ReleaseAmtUnfocused = new Command(CalculateReleaseAmt);
        setPickerModel();
    }



    private void setPickerModel()
    {
        var list = new List<string>();
        list.Add(AppResources.CRSupplyforAramco);
        list.Add(AppResources.CRSupplyandmaintenance);
        list.Add(AppResources.CRsupplymaintenanceandoperating);
        list.Add(AppResources.CRDisassembleinstallationandoperate);
        list.Add(AppResources.CRsupplyinstallationandoperate);
        list.Add(AppResources.CRDisassembleinstallationandtransport);
        list.Add(AppResources.CRCleanlinessandmaintenance);
        list.Add(AppResources.CRSupplyinstallationanddeliver);
        list.Add(AppResources.CRmaintenanceandoperate);
        list.Add(AppResources.CRtransport);
        list.Add(AppResources.CRsupplyandwatertransport);
        list.Add(AppResources.CRconstruction);
        list.Add(AppResources.CRmaintenance);
        list.Add(AppResources.CRoperate);
        list.Add(AppResources.CRDesignandconstruction);
        list.Add(AppResources.CRsupplyanddesign);
        list.Add(AppResources.CRsupplyandoprate);
        list.Add(AppResources.CRsupplyandinstallation);
        list.Add(AppResources.CRdesignsupplyandinstallation);
        list.Add(AppResources.CRdesignsupplyandopratemaintenance);
        list.Add(AppResources.CRequipmentrental);
        list.Add(AppResources.CRmaintenancecleanlinessandoprate);
        list.Add(AppResources.CRcleanliness);
        list.Add(AppResources.CRcatering);
        list.Add(AppResources.CRSecurityguards);
        list.Add(AppResources.CRRoadsmaintenance);
        list.Add(AppResources.CRLaborrecruiting);
        list.Add(AppResources.CRContractingRoadsandTransport);
        list.Add(AppResources.CRSupply);
        list.Add(AppResources.CRSupplyanddeliverytowarehouses);
        list.Add(AppResources.CRSupplyanddeliveryport);
        list.Add(AppResources.CROperationofservicesattheport);
        list.Add(AppResources.CRConsultations);
        list.Add(AppResources.CRPrivateConsultante);
        list.Add(AppResources.CRStudiesandConsulting);
        list.Add(AppResources.CROther);

        GenericPickerModel genericPickerModel = new GenericPickerModel();
        genericPickerModel.PickerData = list;
        genericPickerModel.PickerTitle = AppResources.CRContractType;
        genericPickerModel.PickerId = "ContractType";

        PickerModel = genericPickerModel;
    }

    public async Task ShowInstructionDialog()
    {
        await MopupService.Instance.PushAsync(new InstructionsBottomPopUpView(
            instructionString: AppResources.CRInstructions, checkBoxString: AppResources.CRInstrCheckDesc,
            continueString: AppResources.CRContinue,
            _dialogType: InstructionsBottomPopUpViewModel.DialogType
                .Instructions));

        EnableReleaseDetailsView();
    }

    public void updatePicker()
    {
        PickedContract = PickerModel.SelectedValue;
        PickedContractPercent = ContractTypeDictionary[PickedContract];
        PickedContractId = ContractTypeIdDictionary[PickedContract];
        MakeCalculations();
    }

    public void updatePickerContractType()
    {


        ContractTypeDictionary = new Dictionary<string, double>
        {
            {AppResources.CRSupplyforAramco, 3.0},
            {AppResources.CRSupplyandmaintenance, 10.50},
            {AppResources.CRsupplymaintenanceandoperating, 10.50},
            {AppResources.CRDisassembleinstallationandoperate, 10.50},
            {AppResources.CRsupplyinstallationandoperate, 10.50},
            {AppResources.CRDisassembleinstallationandtransport, 10.50},
            {AppResources.CRCleanlinessandmaintenance, 10.50},
            {AppResources.CRSupplyinstallationanddeliver, 10.50},
            {AppResources.CRmaintenanceandoperate, 10.50},
            {AppResources.CRtransport, 10.50},
            {AppResources.CRsupplyandwatertransport, 10.50},
            {AppResources.CRconstruction, 10.50},
            {AppResources.CRmaintenance, 10.50},
            {AppResources.CRoperate, 10.50},
            {AppResources.CRDesignandconstruction, 10.50},
            {AppResources.CRsupplyanddesign, 10.50},
            {AppResources.CRsupplyandoprate, 10.50},
            {AppResources.CRsupplyandinstallation, 10.50},
            {AppResources.CRdesignsupplyandinstallation, 10.50},
            {AppResources.CRdesignsupplyandopratemaintenance, 10.50},
            {AppResources.CRequipmentrental, 10.50},
            {AppResources.CRmaintenancecleanlinessandoprate, 10.50},
            {AppResources.CRcleanliness, 10.50},
            {AppResources.CRcatering, 10.50},
            {AppResources.CRSecurityguards, 10.50},
            {AppResources.CRRoadsmaintenance, 10.50},
            {AppResources.CRLaborrecruiting, 10.50},
            {AppResources.CRContractingRoadsandTransport, 10.50},
            {AppResources.CRSupply, 15.00},
            {AppResources.CRSupplyanddeliverytowarehouses, 15.00},
            {AppResources.CRSupplyanddeliveryport, 15.00},
            {AppResources.CROperationofservicesattheport, 15.00},
            {AppResources.CRConsultations, 20.00},
            {AppResources.CRPrivateConsultante, 20.00},
            {AppResources.CRStudiesandConsulting, 20.00},
            {AppResources.CROther, 15.00},
        };


        ContractTypeIdDictionary = new Dictionary<string, string>
            {
                {AppResources.CRSupplyforAramco,"1" },
                {AppResources.CRSupplyandmaintenance, "2"},
                {AppResources.CRsupplymaintenanceandoperating, "3"},
                {AppResources.CRDisassembleinstallationandoperate, "4"},
                {AppResources.CRsupplyinstallationandoperate, "5"},
                {AppResources.CRDisassembleinstallationandtransport, "6"},
                {AppResources.CRCleanlinessandmaintenance, "7"},
                {AppResources.CRSupplyinstallationanddeliver, "8"},
                {AppResources.CRmaintenanceandoperate, "9"},
                {AppResources.CRtransport, "10"},
                {AppResources.CRsupplyandwatertransport, "11"},
                {AppResources.CRconstruction, "12"},
                {AppResources.CRmaintenance, "13"},
                {AppResources.CRoperate, "14"},
                {AppResources.CRDesignandconstruction, "15"},
                {AppResources.CRsupplyanddesign, "16"},
                {AppResources.CRsupplyandoprate, "17"},
                {AppResources.CRsupplyandinstallation, "18"},
                {AppResources.CRdesignsupplyandinstallation, "19"},
                {AppResources.CRdesignsupplyandopratemaintenance, "20"},
                {AppResources.CRequipmentrental, "21"},
                {AppResources.CRmaintenancecleanlinessandoprate, "22"},
                {AppResources.CRcleanliness, "23"},
                {AppResources.CRcatering, "24"},
                {AppResources.CRSecurityguards, "25"},
                {AppResources.CRRoadsmaintenance, "26"},
                {AppResources.CRLaborrecruiting, "27"},
                {AppResources.CRContractingRoadsandTransport, "28"},
                {AppResources.CRSupply, "29"},
                {AppResources.CRSupplyanddeliverytowarehouses, "30"},
                {AppResources.CRSupplyanddeliveryport, "31"},
                {AppResources.CROperationofservicesattheport,"32"},
                {AppResources.CRConsultations, "34"},
                {AppResources.CRPrivateConsultante, "35"},
                {AppResources.CRStudiesandConsulting, "36"},
                {AppResources.CROther, "33"},
            };

    }


    private async Task showDatePickerDialog(string title)
    {
        GenericDatePickerModel genericPickerModel = new GenericDatePickerModel();
        genericPickerModel.DatePickerTitle = title;
        genericPickerModel.PickerId = "DatePicker";

        try
        {
            await MopupService.Instance.PushAsync(new CalendarPickerPageView(genericPickerModel, false));
        }
        catch (InternetException ex)
        {
            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            _navigationService.GoBack();
        }
    }

    private async Task showPickerDialog()
    {
        try
        {
            await MopupService.Instance.PushAsync(new PickerPageView(PickerModel));
        }
        catch (InternetException ex)
        {
            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            _navigationService.GoBack();
        }
    }

    public async Task InstructionsTapped()
    {
        try
        {
            await MopupService.Instance.PopAsync();
        }
        catch (InternetException ex)
        {
            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            _navigationService.GoBack();
        }
    }

    private async Task ReleaseDetailsConBtnClicked()
    {
        try
        {
            CultureInfo calCul;

            if (IsHijriCal)
            {
                calCul = new CultureInfo("ar-SA");

            }
            else
            {
                calCul = new CultureInfo("en-US");
            }
            if (DateTime.Parse(FromDate, calCul).Date > DateTime.ParseExact(HDateNow(), "yyyy/MM/dd", calCul).Date)
            {
                await _dialogService.ShowMessage(AppResources.CRContractDateshouldnotbegreaterfromcurentdate,
                    AppResources.Information);
                return;
            }
            else if (DateTime.Parse(ToDate, calCul).Date > DateTime.ParseExact(HDateNow(), "yyyy/MM/dd", calCul).Date)
            {
                await _dialogService.ShowMessage(AppResources.CRContractEndDateshouldnotbegreaterfromcurentdate,
                    AppResources.Information);
                return;
            }

            else if (DateTime.Parse(FromDate, calCul).Date > DateTime.Parse(ToDate, calCul).Date)
            {
                await _dialogService.ShowMessage(AppResources.CRContractEndDateshouldnotbelessfromcontractdate,
                    AppResources.Information);
                return;
            }
            else if (!IsReleaseDetailsEnabled)
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.RequiredData;
                return;
            }

            EnableAttachmentsView();
        }
        catch (InternetException ex)
        {
            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            _navigationService.GoBack();
        }
    }

    private async Task AttachmentsConBtnClicked()
    {
        try
        {
            if (!IsAttachmentsEnabled)
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.RequiredData;
                return;
            }
            EnableRemarksAndDescView();
        }
        catch (InternetException ex)
        {
            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            _navigationService.GoBack();
        }
    }

    private async Task RemarksAndDescConBtnClicked()
    {
        try
        {
            EnableDeclarationView();
        }
        catch (InternetException ex)
        {
            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            _navigationService.GoBack();
        }
    }

    private async Task DeclarationConBtnClicked()
    {
        try
        {
            if (IsDeclarationViewEnabledNew)
            {
                IsDeclarationEnabled = true;
                if (!string.IsNullOrEmpty(Zterms) && !IsDECCheckBox)
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));
                    return;
                }
            }
            else
            {
                if (!IsDeclarationEnabled)
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.RequiredData;
                    return;
                }
            }

            EnableSummaryView();
        }
        catch (InternetException ex)
        {
            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            _navigationService.GoBack();
        }
    }

    private async Task SummaryConBtnClicked()
    {

        try
        {
            if (!isSubmitted)
            {
                await SubmitClicked();
            }


        }
        catch (GAZTErrorException ex)
        {
            if (MopupService.Instance.PopupStack.Count > 0)
                await MopupService.Instance.PopAsync(false);
            await _dialogService.ShowMessage(ex.ToString(), AppResources.Information);
            return;
        }
        catch (GAZTVATRegistrationInProcessException ex)
        {
            if (MopupService.Instance.PopupStack.Count > 0)
                await MopupService.Instance.PopAsync(false);
            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            return;
        }

        catch (InternetException ex)
        {
            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            _navigationService.GoBack();
        }
        catch (Exception ex)
        {
            if (MopupService.Instance.PopupStack.Count > 0)
                await MopupService.Instance.PopAsync(false);
            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
        }
        finally
        {
            if (MopupService.Instance.PopupStack.Count > 0)
                await MopupService.Instance.PopAsync(false);
        }
    }

    #region ApiIntegration

    private ObservableCollection<object> _todayDateStart;
    public ObservableCollection<object> TodayDateStart
    {
        get
        {
            return _todayDateStart;
        }
        set
        {
            if (_todayDateStart == value) return;
            _todayDateStart = value;
            OnPropertyChanged("TodayDateStart");
        }
    }
    private ObservableCollection<object> _todayDateinHijriStart;
    public ObservableCollection<object> TodayDateinHijriStart
    {
        get
        {
            return _todayDateinHijriStart;
        }
        set
        {
            if (_todayDateinHijriStart == value) return;

            _todayDateinHijriStart = value;
            OnPropertyChanged("TodayDateinHijriStart");
        }
    }
    private ObservableCollection<object> _todayDateEnd;
    public ObservableCollection<object> TodayDateEnd
    {
        get
        {
            return _todayDateEnd;
        }
        set
        {
            if (_todayDateEnd == value) return;

            _todayDateEnd = value;
            OnPropertyChanged("TodayDateEnd");
        }
    }
    private ObservableCollection<object> _todayDateinHijriEnd;
    public ObservableCollection<object> TodayDateinHijriEnd
    {
        get
        {
            return _todayDateinHijriEnd;
        }
        set
        {
            if (_todayDateinHijriEnd == value) return;

            _todayDateinHijriEnd = value;
            OnPropertyChanged("TodayDateinHijriEnd");
        }
    }

    public ObservableCollection<ChipModel> _chipDataFilterlist = null;
    public ObservableCollection<ChipModel> ChipDataFilterlist
    {
        get
        {
            return _chipDataFilterlist;
        }
        set
        {
            if (_chipDataFilterlist == value) return;

            _chipDataFilterlist = value;
            OnPropertyChanged("ChipDataFilterlist");
        }
    }

    public void PopulateDataInChips()
    {
        ChipDataFilterlist = new ObservableCollection<ChipModel>()
            {
                new ChipModel(){Text =AppResources.NDGregorian, TemplateType = AppResources.NDGregorian, ImageSource="Calendar"},
                new ChipModel(){Text =AppResources.NDHijri, TemplateType = AppResources.NDHijri,ImageSource = "Calendar"},
            };
    }

    public int DefaultMonth;
    public int DefaultMonthHijri;
    public void SetDefaultDate()
    {
        ObservableCollection<object> todaycollection = new ObservableCollection<object>();
        //Select today dates

        if (DateTime.Now.Date.Day < 10)
            todaycollection.Add("0" + DateTime.Now.Date.Day);
        else
            todaycollection.Add(DateTime.Now.Date.Day.ToString());
        if (DateTime.Now.Date.Month < 10)
            todaycollection.Add("0" + DateTime.Now.Date.Month);
        else
            todaycollection.Add(DateTime.Now.Date.Month.ToString());
        todaycollection.Add(DateTime.Now.Date.Year.ToString());
        TodayDateStart = todaycollection;
        TodayDateEnd = todaycollection;
        DefaultMonth = DateTime.Now.Date.Month;

        //TodayDateinHijri
        ObservableCollection<object> todaycollectionHijri = new ObservableCollection<object>();
        var calendar = new UmAlQuraCalendar();
        if (calendar.GetDayOfMonth(DateTime.Now.Date) < 10)
            todaycollectionHijri.Add("0" + calendar.GetDayOfMonth(DateTime.Now.Date).ToString());
        else
            todaycollectionHijri.Add(calendar.GetDayOfMonth(DateTime.Now.Date).ToString());
        if (calendar.GetMonth(DateTime.Now.Date) < 10)
            todaycollectionHijri.Add("0" + calendar.GetMonth(DateTime.Now.Date));
        else
            todaycollectionHijri.Add(calendar.GetMonth(DateTime.Now.Date).ToString());
        todaycollectionHijri.Add(calendar.GetYear(DateTime.Now.Date).ToString());

        TodayDateinHijriStart = todaycollectionHijri;
        TodayDateinHijriEnd = todaycollectionHijri;

        if (ContractReleaseData != null)
        {


            if (ContractReleaseData.d.ACalTp == "Hijri")
            {

                FromDate = HDateNow();
                ToDate = HDateNow();
            }
            else
            {
                FromDate = (TodayDateStart[2] + "/" + TodayDateStart[1] + "/" + TodayDateStart[0]).ToString();
                ToDate = (TodayDateEnd[2] + "/" + TodayDateEnd[1] + "/" + TodayDateEnd[0]).ToString();
            }

        }
    }


    public string HDateNow()
    {
        try
        {

            CultureInfo calCul;
            if (ContractReleaseData != null)
            {

                if (IsHijriCal)
                {
                    calCul = new CultureInfo("ar-SA");
                }
                else
                {
                    calCul = new CultureInfo("en-US");
                }
            }
            else
            {

                calCul = new CultureInfo("en-US");
            }
            return DateTime.Now.ToString("yyyy/MM/dd", calCul);
        }
        catch (Exception)
        {



            return "";
        }
    }

    public string ConvertToRequiredDatesFormat(string date, bool isTimeStamp, CultureInfo calCul, bool isRemoveTime = false)
    {
        try
        {
            if (!string.IsNullOrEmpty(date))
            {
                if (isTimeStamp)
                {
                    long timestamp = long.Parse(date.Substring(6, date.Length - 8));
                    DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
                    string requiredTime = dateTimeOffset.ToString("yyyy-MM-ddTHH:mm:ss", calCul);
                    if (isRemoveTime)
                    {
                        DateTime dateTime = DateTime.ParseExact(requiredTime, "yyyy-MM-ddTHH:mm:ss", calCul);
                        requiredTime = dateTime.ToString("yyyy-MM-dd", calCul);
                    }
                    return requiredTime;
                }
                else
                {
                    DateTime inputDate = DateTime.Parse(date, calCul);
                    string requiredTime = inputDate.ToString("yyyy-MM-ddTHH:mm:ss", calCul);
                    if (isRemoveTime)
                    {
                        DateTime dateTime = DateTime.ParseExact(requiredTime, "yyyy-MM-ddTHH:mm:ss", calCul);
                        requiredTime = dateTime.ToString("yyyy-MM-dd", calCul);
                    }
                    return requiredTime;
                }
            }
            else
                return "";

        }
        catch (Exception ex)
        {
        }



        return "";

    }

    public ContractReleaseFormRequest BuildRequestObject()
    {
        ContractReleaseFormRequest request = new ContractReleaseFormRequest();
        request.d = new CotractRequest();
        try
        {
            request.d.__metadata = ContractReleaseData.d.__metadata;

            // request.d.AAgreeTm = ContractReleaseData.d.AAgreeTm;
            request.d.AAgreeTm = DateTime.Now.ToString("HH:mm:ss");
            request.d.ABranch = ContractReleaseData.d.ABranch;
            request.d.ACalTp = ContractReleaseData.d.ACalTp;
            request.d.AContChk = ContractReleaseData.d.AContChk;
            request.d.AHijriPeriodFrom = ContractReleaseData.d.AHijriPeriodFrom;
            request.d.AHijriPeriodTo = ContractReleaseData.d.AHijriPeriodTo;
            request.d.AmdRsnz = ContractReleaseData.d.AmdRsnz;
            request.d.AOtherDes = ContractReleaseData.d.AOtherDes;
            request.d.APeriodFrom = ContractReleaseData.d.APeriodFrom;
            request.d.APeriodTo = ContractReleaseData.d.APeriodTo;
            request.d.Approvez = ContractReleaseData.d.Approvez;

            request.d.ARemark = ContractReleaseData.d.ARemark;
            request.d.ATin = ContractReleaseData.d.ATin;
            request.d.ATpNm = ContractReleaseData.d.ATpNm;
            request.d.Auditorz = ContractReleaseData.d.Auditorz;
            request.d.CaseGuid = ContractReleaseData.d.CaseGuid;
            request.d.CreateTxAssesz = ContractReleaseData.d.CreateTxAssesz;
            request.d.CurrDatumz = null;
            request.d.Euser = ContractReleaseData.d.Euser;
            request.d.Fbnum = ContractReleaseData.d.Fbnum;
            if (ContractReleaseData.d.Fbnumz.Equals("$"))
                request.d.Fbnumz = string.Empty;
            else
                request.d.Fbnumz = ContractReleaseData.d.Fbnumz;

            //request.d.Fbnumz = ContractReleaseData.d.Fbnumz;
            request.d.FormGuid = ContractReleaseData.d.FormGuid;
            request.d.Langz = WebServiceManager.GetLangZParameterAREN();
            request.d.LegacyDocNo = ContractReleaseData.d.LegacyDocNo;
            request.d.Mandt = ContractReleaseData.d.Mandt;
            request.d.Monthz = ContractReleaseData.d.Monthz;
            request.d.OfficerUidz = ContractReleaseData.d.OfficerUidz;
            request.d.PeriodKey = ContractReleaseData.d.PeriodKey;
            request.d.PeriodKeyz = ContractReleaseData.d.PeriodKeyz;
            request.d.PortalUsrz = ContractReleaseData.d.PortalUsrz;
            request.d.RegIdz = ContractReleaseData.d.RegIdz;
            request.d.Rejectz = ContractReleaseData.d.Rejectz;
            request.d.Savez = ContractReleaseData.d.Savez;
            request.d.Status = ContractReleaseData.d.Status;
            request.d.Taxpayerz = ContractReleaseData.d.Taxpayerz;
            request.d.Textnote = ContractReleaseData.d.Textnote;
            request.d.UserTin = ContractReleaseData.d.UserTin;
            request.d.Xvoidz = ContractReleaseData.d.Xvoidz;
            request.d.AContDtFg = ContractReleaseData.d.AContDtFg;
            request.d.AContEndDtFg = ContractReleaseData.d.AContEndDtFg;
            request.d.AContNm = ContractName;
            request.d.AContNo = ContractNumber;
            request.d.AContProfit = ProfitEstimatedContract.ToString();
            request.d.AContProfitPer = PickedContractPercent.ToString();
            request.d.ADueTax = TaxDues.ToString();
            request.d.ADueTot = TotalDues.ToString();
            request.d.ADueZakat = ZakatDues.ToString();
            request.d.AReqAmt = AmountToRelease.ToString();
            request.d.ATaxProfi = ContractReleaseData.d.ATaxProfi;
            request.d.ATaxProfitPer = EstimatedProfitForTaxAmount.ToString();
            request.d.ATotalAmt = ContractTotalAmount.ToString();
            request.d.AZakatProfit = ContractReleaseData.d.AZakatProfit;
            request.d.AZakatProfitPer = EstimatedProfitForZakatAmount.ToString();
            request.d.AComments = Remarks.ToString();
            request.d.ARemark = Remarks.ToString();
            request.d.declaration = "X";
            request.d.ADoc1 = "0";
            request.d.ADoc2 = "1";
            request.d.ADoc3 = "1";
            request.d.AInvoiceChk = "";
            request.d.AType = PickedContractId;

            ZnotesSet notes = new ZnotesSet();
            if (DetailDescription != null)
            {

                notes.Tdline = DetailDescription.ToString();

            }
            else
            {
                notes.Tdline = "";

            }
            Metadata _metdata = new Metadata();
            _metdata.uri = ZATCAConstants.ContractReleaseRequestUrl + "/sap/opu/odata/SAP/Z_TP_NOTES_TP11_SRV/znotesSet(1)";
            _metdata.type = "Z_TP_NOTES_TP11_SRV.znotes";
            _metdata.id = ZATCAConstants.ContractReleaseRequestUrl + "/sap/opu/odata/SAP/Z_TP_NOTES_TP11_SRV/znotesSet(1)";

            notes.__metadata = _metdata;
            notes.AttByz = "TP";
            notes.ElemNo = 0;

            notes.Erfdtz = null;
            notes.Erftmz = null;
            notes.Erfusrz = "";
            notes.Lineno = 1;
            notes.Noteno = "001";
            notes.Notenoz = "001";
            notes.Rcodez = "TP11_NOTE";
            notes.Refnamez = "";
            notes.Tdformat = "";
            notes.XInvoicez = "";
            notes.XObsoletez = "";
            request.d.znotesSet = new ZnotesSet[1];
            request.d.znotesSet[0] = notes;
            request.d.AttDetSet = ContractReleaseData.d.AttDetSet;
            var todayDate = DateTime.Now.ToString();

            DateTime dt2 = Convert.ToDateTime(todayDate);
            JsonSerializerSettings microsoftDateFormatSettings2 = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
            };
            var jsonDateTime2 = JsonConvert.SerializeObject(dt2, microsoftDateFormatSettings2);
            string[] dateList2 = jsonDateTime2.Split('+');
            jsonDateTime2 = dateList2[0].Replace("\"\\", "");
            jsonDateTime2 = jsonDateTime2 + ")/";
            var convretedTodayate = jsonDateTime2;

            request.d.AReceiveDt = ConvertToRequiredDatesFormat(convretedTodayate, true, new CultureInfo("en-US"));
            CultureInfo calCul;
            DateTime dt = new DateTime();
            DateTime dt1 = new DateTime();
            if (ContractReleaseData.d.ACalTp == "Hijri")
            {
                calCul = new CultureInfo("ar-SA");

                // User enter Gregorian
                if (!IsHijriCal)
                {
                    dt = DateTimeHelper.ConvertToUmAlQuraHigriDate(FromDate).Item1;
                    dt1 = DateTimeHelper.ConvertToUmAlQuraHigriDate(ToDate).Item1;

                    // Send to BE Hijri as user enter Gregorian
                    request.d.AContDt1 = ConvertToRequiredDatesFormat($"{dt.Year}/{dt.Month}/{dt.Day}", false, calCul, true);

                    request.d.AContEndDtCh = ConvertToRequiredDatesFormat($"{dt1.Year}/{dt1.Month}/{dt1.Day}", false, calCul, true);
                }
                // User enter Hijri
                else
                {
                    var fromDate = DateTime.Parse(FromDate, calCul);
                    var toDate = DateTime.Parse(ToDate, calCul);
                    dt = fromDate;
                    dt1 = toDate;

                    // Send to BE Hijri as user enter Hijri
                    request.d.AContDt1 = ConvertToRequiredDatesFormat(FromDate, false, calCul, true);

                    request.d.AContEndDtCh = ConvertToRequiredDatesFormat(ToDate, false, calCul, true);

                }

            }
            else
            {
                calCul = new CultureInfo("en-US");
                var fromDate = DateTime.Parse(FromDate, calCul);
                var toDate = DateTime.Parse(ToDate, calCul);

                // User enter Hijri
                if (IsHijriCal)
                {
                    dt = DateTimeHelper.ConvertToGregorian(fromDate).Item1;
                    dt1 = DateTimeHelper.ConvertToGregorian(toDate).Item1;

                    // Send to BE Gregorian as user enter Hijri
                    request.d.AContDt1 = ConvertToRequiredDatesFormat($"{dt.Year}/{dt.Month}/{dt.Day}", false, calCul, true);

                    request.d.AContEndDtCh = ConvertToRequiredDatesFormat($"{dt1.Year}/{dt1.Month}/{dt1.Day}", false, calCul, true);
                }

                // User enter Gregorian
                else
                {
                    dt = fromDate;
                    dt1 = toDate;

                    // Send to BE Gregorian as user enter Gregorian
                    request.d.AContDt1 = ConvertToRequiredDatesFormat(FromDate, false, calCul, true);

                    request.d.AContEndDtCh = ConvertToRequiredDatesFormat(ToDate, false, calCul, true);
                }
            }


            JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
            };
            var jsonDateTime = JsonConvert.SerializeObject(dt, microsoftDateFormatSettings);
            string[] dateList = jsonDateTime.Split('+');
            jsonDateTime = dateList[0].Replace("\"\\", "");
            jsonDateTime = jsonDateTime + ")/";
            var convretedFromDate = jsonDateTime;


            JsonSerializerSettings microsoftDateFormatSettings1 = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
            };
            var jsonDateTime1 = JsonConvert.SerializeObject(dt1, microsoftDateFormatSettings);
            string[] dateList1 = jsonDateTime1.Split('+');
            jsonDateTime1 = dateList1[0].Replace("\"\\", "");
            jsonDateTime1 = jsonDateTime1 + ")/";
            var convretedToDate = jsonDateTime1;
            request.d.AContDt = ConvertToRequiredDatesFormat(convretedFromDate, true, new CultureInfo("en-US"));
            request.d.AContEndDt = ConvertToRequiredDatesFormat(convretedToDate, true, new CultureInfo("en-US"));

            request.d.Savez = "X";
            request.d.Submitz = "X";
        }
        catch (Exception ex)
        {


        }
        return request;
    }


    public async Task<bool> SubmitClicked()
    {
        try
        {
            ContractReleaseFormRequest request = new ContractReleaseFormRequest();
            request = BuildRequestObject();
            IsLoading = true;
            var ContractReleaseResponse = await ContractReleaseWebServiceManager.GAZTSubmitContractReleaseRequestData(request);
            IsLoading = false;
            if (ContractReleaseResponse.result != null)
            {
                ContractReleaseData1.d = ContractReleaseResponse?.result;

                if (ContractReleaseData1.d != null)
                {
                    IsLoading = false;
                    await Application.Current.MainPage.Navigation.PushAsync(new ContractReleaseSuccessPageView(this));
                    isSubmitted = true;
                }
                return true;
            }
            isSubmitted = false;
            return false;
        }
        catch (GAZTVATRegistrationInProcessException ex)
        {
            IsLoading = false;
            await _dialogService.ShowMessage(ex.Message, AppResources.ZError);
            return false;
        }

        catch (Exception ex)
        {
            IsLoading = false;
            await _dialogService.ShowMessage(ex.Message, AppResources.ZError);
            return false;
        }
    }


    #endregion

    private void EnableReleaseDetailsView()
    {
        CurrentIndex = 1;
        IsBackButtonVisible = false;
        IsReleaseDetailsVisible = true;
        AttachmentsVisible = false;
        RemarksAndDescVisible = false;
        DeclarationVisible = false;
        SummaryVisible = false;
        selectedPage = (int)PagesEnum.CrReleaseDetailsView;
    }

    private void EnableAttachmentsView()
    {
        CurrentIndex = 2;
        IsBackButtonVisible = true;
        IsReleaseDetailsVisible = false;
        AttachmentsVisible = true;
        RemarksAndDescVisible = false;
        DeclarationVisible = false;
        SummaryVisible = false;
        selectedPage = (int)PagesEnum.CrAttachmentsView;
    }

    private void EnableRemarksAndDescView()
    {
        CurrentIndex = 3;
        IsBackButtonVisible = true;
        IsReleaseDetailsVisible = false;
        AttachmentsVisible = false;
        RemarksAndDescVisible = true;
        DeclarationVisible = false;
        SummaryVisible = false;
        selectedPage = (int)PagesEnum.CrRemarksAndDescriptionView;
    }

    private void EnableDeclarationView()
    {
        CurrentIndex = 4;
        IsBackButtonVisible = true;
        IsReleaseDetailsVisible = false;
        AttachmentsVisible = false;
        RemarksAndDescVisible = false;
        DeclarationVisible = true;
        if (VatDeregDeclaration != null && VatDeregDeclaration.D != null && string.IsNullOrEmpty(VatDeregDeclaration.D.Zterms))
        {
            IsDeclarationViewEnabled = true;
            IsDeclarationViewEnabledNew = false;
        }
        else
        {
            var direction = App.IsArabic ? "direction: rtl;" : "direction: ltr;";
            Zterms = $"<div style=\"{direction}\"> {VatDeregDeclaration.D.Zterms} </div>";
            IsDeclarationViewEnabled = false;
            IsDeclarationViewEnabledNew = true;
            if (App.IsArabic)
            {
                ShouldShowAR = true;
                ShouldShowEN = false;
            }
            else
            {
                ShouldShowEN = true;
                ShouldShowAR = false;
            }

        }
        SummaryVisible = false;
        selectedPage = (int)PagesEnum.CrDeclarationView;
    }

    private void EnableSummaryView()
    {
        CurrentIndex = 5;
        IsBackButtonVisible = true;
        IsReleaseDetailsVisible = false;
        AttachmentsVisible = false;
        RemarksAndDescVisible = false;
        DeclarationVisible = false;
        SummaryVisible = true;
        selectedPage = (int)PagesEnum.CrSummaryView;
    }

    private void BackNavigations()
    {
        switch (selectedPage)
        {
            case (int)PagesEnum.CrAttachmentsView:
                EnableReleaseDetailsView();
                break;
            case (int)PagesEnum.CrRemarksAndDescriptionView:
                EnableAttachmentsView();
                break;

            case (int)PagesEnum.CrDeclarationView:
                EnableRemarksAndDescView();
                break;
            case (int)PagesEnum.CrSummaryView:
                EnableDeclarationView();
                break;
        }
    }

    #region OnPageLoad

    public async Task OnPageLoad()
    {
        try
        {

            IsLoading = true;
            ContractReleaseData = await ContractReleaseWebServiceManager.GAZTGetContractReleaseRequestData();
            if (ContractReleaseData != null && ContractReleaseData.d != null)
            {
                //TODO 7062 Changes
                VatDeregDeclaration = await VatRegistrationWebServiceManager.GAZTGetVATDeRegistrationDeclaration(ContractReleaseData.d.Fbnum);
                bindDataToUI();

                if (ContractReleaseData.d.ACalTp == "Hijri")
                {

                    IsHijriCal = true;
                }
                else
                {

                    IsHijriCal = false;
                }
                contractReleaseInterface.setDateFormatFirstTime();
                SetDefaultDate();
                IsLoading = false;
            }
            else
            {
                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong,
                       AppResources.Information);
                _navigationService.GoBack();
            }
            IsLoading = false;

        }
        catch (GAZTVATRegistrationInProcessException ex)
        {

            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            _navigationService.GoBack();
            IsLoading = false;
        }
        catch (Exception)
        {

            IsLoading = false;
            await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
            _navigationService.GoBack();
        }
    }

    public async Task NewContractCopyAttachmentClicked()
    {
        if (MopupService.Instance.PopupStack.Count > 0) return;
        _isInvoiceAttachments = false;
        if (ContractCopyAttachmentsListViewData == null)
        {
            ContractCopyAttachmentsListViewData = new ObservableCollection<Attachment>();
        }
        try
        {

            await MopupService.Instance.PushAsync(new FilesUploadPopUpPageView(
                ContractCopyAttachmentsListViewData.ToList(),
                WhichAttachment.ContractReleaseCopy, ContractReleaseData.d.CaseGuid));

        }
        catch (InternetException ex)
        {

            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            _navigationService.GoBack();
        }
    }

    private void CalculateReleaseAmt(object obj)
    {
        try
        {
            if (ContractTotalAmount < double.Parse(ContractReleaseAmount))
            {
                _dialogService.ShowMessageBox(AppResources.CRTotalAmountRequirdtoReleasemustbelesstotalamountofcontract, AppResources.CRWarning);
            }
            AmountToRelease = double.Parse(ContractReleaseAmount);

            MakeCalculations();
            ContractReleaseAmount = UtilityManager.GetCommaSeparatedAmount(ContractReleaseAmount);

        }
        catch (Exception)
        {
        }
    }

    public async Task NewInvoiceAttachmentClicked()
    {
        if (MopupService.Instance.PopupStack.Count > 0) return;
        _isInvoiceAttachments = true;
        if (InvoiceAttachmentsListViewData == null)
        {
            InvoiceAttachmentsListViewData = new ObservableCollection<Attachment>();
        }
        try
        {

            await MopupService.Instance.PushAsync(new FilesUploadPopUpPageView(
                InvoiceAttachmentsListViewData.ToList(),
                WhichAttachment.ContractReleaseInvoice,
                ContractReleaseData.d.CaseGuid));

        }
        catch (InternetException ex)
        {
            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            _navigationService.GoBack();
        }
    }


    public void MakeCalculations()
    {
        ProfitEstimatedContract = AmountToRelease * (PickedContractPercent / 100);
        EstimatedProfitForZakatAmount = (EstimatedProfitForZakatPercent / 100) * ProfitEstimatedContract;
        EstimatedProfitForTaxAmount = (EstimatedProfitForTaxPercent / 100) * ProfitEstimatedContract;
        ZakatDues = (2.5 / 100) * EstimatedProfitForZakatAmount;
        TaxDues = (20.00 / 100) * EstimatedProfitForTaxAmount;
        TotalDues = ZakatDues + TaxDues;

        if (ContractName == "" || ContractNumber == "" || PickedContract == "" || ContractTotalAmount.Equals(0.0) || AmountToRelease.Equals(0.0) || ContractTotalAmount < AmountToRelease)
        {
            IsReleaseDetailsEnabled = false;
        }
        else
        {
            IsReleaseDetailsEnabled = true;
        }
    }

    public void PopulateAttachments(List<Attachment> attachments)
    {
        var attachmentsListViewData = new ObservableCollection<Attachment>();

        foreach (Attachment attachemnt in attachments)
        {
            attachmentsListViewData.Add(attachemnt);
        }

        if (_isInvoiceAttachments)
        {
            InvoiceAttachmentsListViewData = attachmentsListViewData;
        }
        else
        {
            ContractCopyAttachmentsListViewData = attachmentsListViewData;
        }

        EnableAttachments();
    }

    private void EnableAttachments()
    {

        if (InvoiceAttachmentsListViewData == null)
        {
            IsAttachmentsEnabled = false;
        }
        else if (ContractCopyAttachmentsListViewData == null)
        {

            IsAttachmentsEnabled = false;
        }
        else
        {

            if (InvoiceAttachmentsListViewData.Count == 0 || ContractCopyAttachmentsListViewData.Count == 0)
            {

                IsAttachmentsEnabled = false;
            }
            else
            {
                IsAttachmentsEnabled = true;
            }
        }

    }

    public void EnableDeclarationContinue()
    {
        if (IsDeclarationViewEnabledNew)
        {
            IsDeclarationEnabled = true;
        }
        else
        {
            if (ContactPersonName == "" || Designation == "")
            {
                IsDeclarationEnabled = false;
            }
            else
            {
                IsDeclarationEnabled = true;
            }
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

    public void ResetData()
    {

        TodayDateEnd = null;
        TodayDateinHijriEnd = null;
        TodayDateStart = null;
        TodayDateinHijriStart = null;
        IsHijriCal = false;
        FromDate = "";
        ToDate = "";
        SetDefaultDate();
        updatePickerContractType();
        _isInvoiceAttachments = true;
        IsReleaseDetailsEnabled = false;
        IsAttachmentsEnabled = false;
        IsDeclarationEnabled = false;
        fromDatePicker = false;
        isSubmitted = false;
        InfoTitle = "";
        InfoDesc = "";
        PickedContract = "";
        ContractTotalAmount = 0.0;
        AmountToRelease = 0.0;
        PickedContractPercent = 0.0;
        ProfitEstimatedContract = 0.0;
        EstimatedProfitForZakatPercent = 0.0;
        EstimatedProfitForZakatAmount = 0.0;
        EstimatedProfitForTaxAmount = 0.0;
        EstimatedProfitForTaxPercent = 100.0;
        ZakatDues = 0.0;
        TaxDues = 0.0;
        TotalDues = 0.0;
        Remarks = "";
        DetailDescription = "";
        ContactPersonName = "";
        Designation = "";
        ContractName = "";
        ContractNumber = "";
        PickerModel = null;
        setPickerModel();
        ContractCopyAttachmentsListViewData = null;
        InvoiceAttachmentsListViewData = null;
        charCountRemarksText = 0 + "/" + 255;
        charCountDetailDescription = 0 + "/" + 132;

    }


    public void bindDataToUI()
    {
        try
        {
            EstimatedProfitForZakatPercent = Convert.ToDouble(ContractReleaseData.d.AZakatProfit);
            EstimatedProfitForTaxPercent = Convert.ToDouble(ContractReleaseData.d.ATaxProfi);
        }
        catch (Exception)
        {
        }
    }
    public async Task GetContractReleaseData()
    {
        try
        {
            IsLoading = true;
            await OnPageLoad();
            IsLoading = false;
        }
        catch (Exception)
        {


        }
    }
    #endregion
}
