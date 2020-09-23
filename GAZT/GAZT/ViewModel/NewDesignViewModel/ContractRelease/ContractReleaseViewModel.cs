using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.Models.ContractRelease;
using EGAZT.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel;
using EGAZT.Views.NewDesign;
using EGAZT.Views.NewDesign.ContractReleasePages;
using EGAZT.Views.NewDesign.GenericPickers;
using EGAZT.Views.NewDesign.ZakatInstalmentPlan;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Metadata = EGAZT.Models.ContractRelease.Metadata;

namespace EGAZT.ViewModel.NewDesignViewModel.ContractRelease
{
    public class ContractReleaseViewModel : ViewModelBase
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
        public ICommand CloseClick { get; set; }
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

        #endregion

        public bool isSubmitted = false;

        private bool _isInvoiceAttachments = true;

        private bool _isBackButtonVisible = true;

        public bool IsBackButtonVisible
        {
            get { return _isBackButtonVisible; }
            set
            {
                _isBackButtonVisible = value;
                RaisePropertyChanged("IsBackButtonVisible");
            }
        }

        private bool _isReleaseDetailsVisible = false;

        public bool IsReleaseDetailsVisible
        {
            get { return _isReleaseDetailsVisible; }
            set
            {
                _isReleaseDetailsVisible = value;
                RaisePropertyChanged("IsReleaseDetailsVisible");
            }
        }


        private bool _isLoading = false;

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }



        private bool _attachmentsVisible = false;

        public bool AttachmentsVisible
        {
            get { return _attachmentsVisible; }
            set
            {
                _attachmentsVisible = value;
                RaisePropertyChanged("AttachmentsVisible");
            }
        }

        private bool _remarksAndDescVisible = false;

        public bool RemarksAndDescVisible
        {
            get { return _remarksAndDescVisible; }
            set
            {
                _remarksAndDescVisible = value;
                RaisePropertyChanged("RemarksAndDescVisible");
            }
        }

        private bool _declarationVisible = false;

        public bool DeclarationVisible
        {
            get { return _declarationVisible; }
            set
            {
                _declarationVisible = value;
                RaisePropertyChanged("DeclarationVisible");
            }
        }

        private bool _isReleaseDetailsEnabled = false;

        public bool IsReleaseDetailsEnabled
        {
            get { return _isReleaseDetailsEnabled; }
            set
            {
                _isReleaseDetailsEnabled = value;
                ReleaseDetailsButtonBackGroundColor = Color.FromHex(_isReleaseDetailsEnabled ? "#d49504" : "#9EA4A9");
                RaisePropertyChanged("IsReleaseDetailsEnabled");
            }
        }

        private Color _releaseDetailsButtonBackGroundColor = Color.FromHex("#d49504");
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
                RaisePropertyChanged("ReleaseDetailsButtonBackGroundColor");
            }
        }

        private bool _isAttachmentsEnabled = false;

        public bool IsAttachmentsEnabled
        {
            get { return _isAttachmentsEnabled; }
            set
            {
                _isAttachmentsEnabled = value;
                AttachButtonBackGroundColor = Color.FromHex(_isAttachmentsEnabled ? "#d49504" : "#9EA4A9");
                RaisePropertyChanged("IsAttachmentsEnabled");
            }
        }

        private Color _attachButtonBackGroundColor = Color.FromHex("#d49504");
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
                RaisePropertyChanged("AttachButtonBackGroundColor");
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

        public bool fromDatePicker = false;

        private bool _summaryVisible = false;

        public bool SummaryVisible
        {
            get { return _summaryVisible; }
            set
            {
                _summaryVisible = value;
                RaisePropertyChanged("SummaryVisible");
            }
        }

        private string _infoTitle = "";

        public string InfoTitle
        {
            get { return _infoTitle; }
            set
            {
                _infoTitle = value;
                RaisePropertyChanged("InfoTitle");
            }
        }

        private string _infoDesc = "";

        public string InfoDesc
        {
            get { return _infoDesc; }
            set
            {
                _infoDesc = value;
                RaisePropertyChanged("InfoDesc");
            }
        }

        private string _pickedContract = "";

        public string PickedContract
        {
            get { return _pickedContract; }
            set
            {
                _pickedContract = value;
                RaisePropertyChanged("PickedContract");
            }
        }

        private string _pickedContractId = "";
        public string PickedContractId
        {
            get { return _pickedContractId; }
            set
            {
                _pickedContractId = value;
                RaisePropertyChanged("PickedContractId");
            }
        }

        public double _contractTotalAmount = 0.0;

        public double ContractTotalAmount
        {
            get { return _contractTotalAmount; }
            set
            {
                _contractTotalAmount = value;
                RaisePropertyChanged("ContractTotalAmount");
            }
        }

        public double _amountToRelease = 0.0;

        public double AmountToRelease
        {
            get { return _amountToRelease; }
            set
            {
                _amountToRelease = value;
                RaisePropertyChanged("AmountToRelease");
            }
        }

        private double _pickedContractPercent = 0.0;

        public double PickedContractPercent
        {
            get { return _pickedContractPercent; }
            set
            {
                _pickedContractPercent = value;
                RaisePropertyChanged("PickedContractPercent");
            }
        }

        private double _profitEstimatedContract = 0.0;

        public double ProfitEstimatedContract
        {
            get { return _profitEstimatedContract; }
            set
            {
                _profitEstimatedContract = value;
                RaisePropertyChanged("ProfitEstimatedContract");
            }
        }

        private double _estimatedProfitForZakatPercent = 0.0;

        public double EstimatedProfitForZakatPercent
        {
            get { return _estimatedProfitForZakatPercent; }
            set
            {
                _estimatedProfitForZakatPercent = value;
                RaisePropertyChanged("EstimatedProfitForZakatPercent");
            }
        }

        private double _estimatedProfitForZakatAmount = 0.0;

        public double EstimatedProfitForZakatAmount
        {
            get { return _estimatedProfitForZakatAmount; }
            set
            {
                _estimatedProfitForZakatAmount = value;
                RaisePropertyChanged("EstimatedProfitForZakatAmount");
            }
        }

        private double _estimatedProfitForTaxAmount = 0.0;

        public double EstimatedProfitForTaxAmount
        {
            get { return _estimatedProfitForTaxAmount; }
            set
            {
                _estimatedProfitForTaxAmount = value;
                RaisePropertyChanged("EstimatedProfitForTaxAmount");
            }
        }

        private double _estimatedProfitForTaxPercent = 100.0;

        public double EstimatedProfitForTaxPercent
        {
            get { return _estimatedProfitForTaxPercent; }
            set
            {
                _estimatedProfitForTaxPercent = value;
                RaisePropertyChanged("EstimatedProfitForTaxPercent");
            }
        }

        private double _zakatDues = 0.0;

        public double ZakatDues
        {
            get { return _zakatDues; }
            set
            {
                _zakatDues = value;
                RaisePropertyChanged("ZakatDues");
            }
        }

        private double _taxDues = 0.0;

        public double TaxDues
        {
            get { return _taxDues; }
            set
            {
                _taxDues = value;
                RaisePropertyChanged("TaxDues");
            }
        }

        private double _totalDues = 0.0;

        public double TotalDues
        {
            get { return _totalDues; }
            set
            {
                _totalDues = value;
                RaisePropertyChanged("TotalDues");
            }
        }

        private string _remarks = "";

        public string Remarks
        {
            get { return _remarks; }
            set
            {
                _remarks = value;
                RaisePropertyChanged("Remarks");
            }
        }

        private string _detailDescription = "";

        public string DetailDescription
        {
            get { return _detailDescription; }
            set
            {
                _detailDescription = value;
                RaisePropertyChanged("DetailDescription");
            }
        }

        private string _contactPersonName = "";

        public string ContactPersonName
        {
            get { return _contactPersonName; }
            set
            {
                _contactPersonName = value;
                RaisePropertyChanged("ContactPersonName");
            }
        }

        private string _designation = "";

        public string Designation
        {
            get { return _designation; }
            set
            {
                _designation = value;
                RaisePropertyChanged("Designation");
            }
        }

        private string _contractName = "";

        public string ContractName
        {
            get { return _contractName; }
            set
            {
                _contractName = value;
                RaisePropertyChanged("ContractName");
            }
        }

        private string _contractNumber = "";

        public string ContractNumber
        {
            get { return _contractNumber; }
            set
            {
                _contractNumber = value;
                RaisePropertyChanged("ContractNumber");
            }
        }

        private DateTime _fromDate = DateTime.Now;

        public DateTime FromDate
        {
            get { return _fromDate; }
            set
            {
                _fromDate = value;
                RaisePropertyChanged("FromDate");
            }
        }

        private DateTime _toDate = DateTime.Now;

        public DateTime ToDate
        {
            get { return _toDate; }
            set
            {
                _toDate = value;
                RaisePropertyChanged("ToDate");
            }
        }

        private GenericPickerModel _pickerModel { get; set; }

        public GenericPickerModel PickerModel
        {
            get { return _pickerModel; }
            set
            {
                _pickerModel = value;
                RaisePropertyChanged("PickerModel");
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
                RaisePropertyChanged("ContractCopyAttachmentsListViewData");
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
                RaisePropertyChanged("InvoiceAttachmentsListViewData");
            }
        }

        private Dictionary<string, double> ContractTypeDictionary = null;


        private Dictionary<string, string> ContractTypeIdDictionary = null;
            
        private ContractReleaseFormResponse _contractReleaseData;

        public ContractReleaseFormResponse ContractReleaseData
        {
            get { return _contractReleaseData; }
            set
            {
                _contractReleaseData = value;
                RaisePropertyChanged("ContractReleaseData");
            }
        }

        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        int selectedPage = (int)PagesEnum.CrReleaseDetailsView;

        public ContractReleaseViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            _navigationService = navigationService;

            _dialogService = dialogService;

            CloseClick = new Command(async () =>
            {
                _navigationService.GoBack();
            });

            GoBackClick = new Command(async () => { BackNavigations(); });

            GoBackToReleaseDetails = new Command(async () => { EnableReleaseDetailsView(); });

            GoBackToAttachments = new Command(async () => { EnableAttachmentsView(); });

            GoBackToDeclaration = new Command(async () => { EnableDeclarationView(); });

            ContractProfitPercentCommand = new Command(async () =>
            {
                InfoTitle = AppResources.CRContractprofitEstimatedRate;
                InfoDesc = AppResources.CRContractprofitEstimatedRateDesc;
                await PopupNavigation.Instance.PushAsync(new ContractReleaseInfoPopup(this));
            });
            ProfitEstimatedContractCommand = new Command(async () =>
            {
                InfoTitle = AppResources.CRProfitEstimatedForContract;
                InfoDesc = AppResources.CRProfitEstimatedForContractDesc;
                await PopupNavigation.Instance.PushAsync(new ContractReleaseInfoPopup(this));
            });
            EstimatedProfitZakatCommand = new Command(async () =>
            {
                InfoTitle = AppResources.CREstimatedProfitforZakat;
                InfoDesc = AppResources.CREstimatedProfitforZakatDesc;
                await PopupNavigation.Instance.PushAsync(new ContractReleaseInfoPopup(this));
            });
            EstimatedProfitTaxCommand = new Command(async () =>
            {
                InfoTitle = AppResources.CREstimatedProfitforTax;
                InfoDesc = AppResources.CREstimatedProfitforTaxDesc;
                await PopupNavigation.Instance.PushAsync(new ContractReleaseInfoPopup(this));
            });
            ValueofZakatDuesCommand = new Command(async () =>
            {
                InfoTitle = AppResources.CRTheValueofZakatdues;
                InfoDesc = AppResources.CRTheValueofZakatduesDesc;
                await PopupNavigation.Instance.PushAsync(new ContractReleaseInfoPopup(this));
            });
            ValueofTaxDuesCommand = new Command(async () =>
            {
                InfoTitle = AppResources.CRTheValueTaxDues;
                InfoDesc = AppResources.CRTheValueTaxDuesDesc;
                await PopupNavigation.Instance.PushAsync(new ContractReleaseInfoPopup(this));
            });
            TotalDuesCommand = new Command(async () =>
            {
                InfoTitle = AppResources.CRTotalDues;
                InfoDesc = AppResources.CRTotalDuesDesc;
                await PopupNavigation.Instance.PushAsync(new ContractReleaseInfoPopup(this));
            });

            ShowStartDatePicker = new Command(async () =>
            {
                fromDatePicker = true;
                showDatePickerDialog(AppResources.CRContractStartDate);

            });

            ShowEndDatePicker = new Command(async () =>
            {
                fromDatePicker = false;
                showDatePickerDialog(AppResources.CRContractEndDate);

            });

            ShowPicker = new Command(async () => { showPickerDialog(); });


            ReleaseDetailsConBtnTapped = new Command(ReleaseDetailsConBtnClicked);
            AttachmentsConBtnTapped = new Command(AttachmentsConBtnClicked);
            RemarksAndDescConBtnTapped = new Command(RemarksAndDescConBtnClicked);
            DeclarationConBtnTapped = new Command(DeclarationConBtnClicked);
            SummaryConBtnTapped = new Command(SummaryConBtnClicked);
            ContractInstructionsClicked = new Command(InstructionsTapped);
            NewInvoiceAttachmentTapped = new Command(NewInvoiceAttachmentClicked);
            NewContractCopyAttachmentTapped = new Command(NewContractCopyAttachmentClicked);

            setPickerModel();
        }

        private void setPickerModel()
        {
            var list = new ObservableCollection<string>();
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

        public async void showInstructionDialog()
        {
            await PopupNavigation.Instance.PushAsync(new InstructionsBottomPopUpView(
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

        public void updatePickerContractType() {


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


        private async void showDatePickerDialog(string title)
        {
            GenericDatePickerModel genericPickerModel = new GenericDatePickerModel();
            genericPickerModel.DatePickerTitle = title;
            genericPickerModel.PickerId = "DatePicker";

            try
            {
                await PopupNavigation.Instance.PushAsync(new CalendarPickerPageView(genericPickerModel, false));
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

        private async void showPickerDialog()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new PickerPageView(PickerModel));
            }
            catch (GAZTUnlockAccountException ex)
            {
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public async void InstructionsTapped()
        {
            try
            {
                await PopupNavigation.Instance.PopAsync();
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

        private async void ReleaseDetailsConBtnClicked()
        {
            try
            {
                if (FromDate > DateTime.Now)
                {
                    await _dialogService.ShowMessage(AppResources.CRContractDateshouldnotbegreaterfromcurentdate,
                        AppResources.Information);
                    return;
                }
                else if (ToDate > DateTime.Now)
                {
                    await _dialogService.ShowMessage(AppResources.CRContractEndDateshouldnotbegreaterfromcurentdate,
                        AppResources.Information);
                    return;
                }
                else if (FromDate > ToDate)
                {
                    await _dialogService.ShowMessage(AppResources.CRContractEndDateshouldnotbelessfromcontractdate,
                        AppResources.Information);
                    return;
                }
                else if (!IsReleaseDetailsEnabled)
                {
                    return;
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

        private void AttachmentsConBtnClicked()
        {
            try
            {
                if (!IsAttachmentsEnabled)
                {
                    return;
                }
                EnableRemarksAndDescView();
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

        private void RemarksAndDescConBtnClicked()
        {
            try
            {
                EnableDeclarationView();
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

        private void DeclarationConBtnClicked()
        {
            try
            {
                if (!IsDeclarationEnabled)
                {
                    return;
                }
                EnableSummaryView();
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

        private async void SummaryConBtnClicked()
        {

           
            try
            {


                if (!isSubmitted)
                {
                    isSubmitted = true;
                   
                   
                    ContractReleaseData = await SubmitClicked();
                    if (ContractReleaseData.d != null)
                    {
                        await Task.Run(() =>
                        {
                            IsLoading = false;
                        });
                        await Application.Current.MainPage.Navigation.PushAsync(new ContractReleaseSuccessPageView());
                        //_navigationService.NavigateTo(App.ContractReleaseSuccessPageView);
                    }
                }






            }
            catch (GAZTUnlockAccountException ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (InternetException ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        #region ApiIntegration


        public ContractReleaseFormRequest BuildRequestObject()
        {
            ContractReleaseFormRequest request = new ContractReleaseFormRequest();
            request.d = new CotractRequest();
            try
            {
                request.d.__metadata = ContractReleaseData.d.__metadata;
                request.d.AAgreeTm = ContractReleaseData.d.AAgreeTm;
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
                request.d.Fbnumz = ContractReleaseData.d.Fbnumz;
                request.d.FormGuid = ContractReleaseData.d.FormGuid;
                request.d.Langz = GetLangZParameter();
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
                //    request.d.ATaxProfi = EstimatedProfitForTaxAmount.ToString();
                // request.d.ATaxProfitPer = EstimatedProfitForTaxPercent.ToString();
                request.d.ATaxProfitPer = EstimatedProfitForTaxAmount.ToString();
                request.d.ATotalAmt = ContractTotalAmount.ToString();
                request.d.AZakatProfit = ContractReleaseData.d.AZakatProfit;
                //    request.d.AZakatProfit = EstimatedProfitForZakatAmount.ToString();
                //     request.d.AZakatProfitPer = EstimatedProfitForZakatPercent.ToString();
                request.d.AZakatProfitPer = EstimatedProfitForZakatAmount.ToString();
                request.d.AComments = Remarks.ToString();
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
                _metdata.uri = Constants.ContractReleaseRequestUrl + "/sap/opu/odata/SAP/Z_TP_NOTES_TP11_SRV/znotesSet(1)";
                _metdata.type = "Z_TP_NOTES_TP11_SRV.znotes";
                _metdata.id = Constants.ContractReleaseRequestUrl + "/sap/opu/odata/SAP/Z_TP_NOTES_TP11_SRV/znotesSet(1)";

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





                //  request.d.znotesSet = ContractReleaseData.d.znotesSet.results;
                request.d.AttDetSet = ContractReleaseData.d.AttDetSet.results;

                // var fromDate = (FromDate.Year + "/" + FromDate.Month + "/" + FromDate.Day).ToString();
                //var toDate = (ToDate.Year + "/" + ToDate.Month + "/" + ToDate.Day).ToString();




                var todayDate = DateTime.Now.ToString();

                DateTime dt2 = Convert.ToDateTime(todayDate);
                JsonSerializerSettings microsoftDateFormatSettings2 = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                   
                };
                //var jsonDateTime = JsonConvert.SerializeObject(dt, microsoftDateFormatSettings);
                var jsonDateTime2 = JsonConvert.SerializeObject(dt2, microsoftDateFormatSettings2);
                string[] dateList2 = jsonDateTime2.Split('+');
                jsonDateTime2 = dateList2[0].Replace("\"\\", "");
                jsonDateTime2 = jsonDateTime2 + ")/";
                var convretedTodayate = jsonDateTime2;

                request.d.AReceiveDt = convretedTodayate;

                DateTime dt = Convert.ToDateTime(FromDate.ToString());
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                //var jsonDateTime = JsonConvert.SerializeObject(dt, microsoftDateFormatSettings);
                var jsonDateTime = JsonConvert.SerializeObject(dt, microsoftDateFormatSettings);
                string[] dateList = jsonDateTime.Split('+');
                jsonDateTime = dateList[0].Replace("\"\\", "");
                jsonDateTime = jsonDateTime + ")/";
                var convretedFromDate = jsonDateTime;


                DateTime dt1 = Convert.ToDateTime(ToDate.ToString());
                JsonSerializerSettings microsoftDateFormatSettings1 = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                //var jsonDateTime = JsonConvert.SerializeObject(dt, microsoftDateFormatSettings);
                var jsonDateTime1 = JsonConvert.SerializeObject(dt1, microsoftDateFormatSettings);
                string[] dateList1 = jsonDateTime1.Split('+');
                jsonDateTime1 = dateList1[0].Replace("\"\\", "");
                jsonDateTime1 = jsonDateTime1 + ")/";
                var convretedToDate = jsonDateTime1;
                request.d.AContDt = convretedFromDate;
                request.d.AContEndDt = convretedToDate;
                //request.d.AContEndDtCh = ToDate.ToString("yyyy/MM/dd");


                //request.d.AContEndDtCh = (ToDate.Year + "/" + ToDate.Month. + "/" + ToDate.Day).ToString();
                //request.d.AContDt1 = FromDate.ToString("yyyy/MM/dd");

                if (ContractReleaseData.d.ACalTp == "H")
                {
                    CultureInfo cultureInfo = new CultureInfo("ar-SA");
                    request.d.AContEndDtCh = ToDate.ToString("yyyy/MM/dd" , cultureInfo);
                    request.d.AContDt1 = FromDate.ToString("yyyy/MM/dd", cultureInfo);

                }
                else
                {

                    request.d.AContEndDtCh = ToDate.ToString("yyyy/MM/dd");
                    request.d.AContDt1 = FromDate.ToString("yyyy/MM/dd");

                }
                request.d.Savez = "X";
                request.d.Submitz = "X";


            }

            catch (Exception ex)
            {

            }


            return request;


        }

        private static string GetLangZParameter()
        {
            if (App.IsArabic)
                return "A";
            else
                return "E";
        }

        public async Task<ContractReleaseFormResponse> SubmitClicked()
        {
            ContractReleaseFormResponse response = new ContractReleaseFormResponse();

            ContractReleaseFormRequest request = new ContractReleaseFormRequest();


            try
            {


                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = true;
                });



                request = BuildRequestObject();



                response = await WebServiceManager.GAZTSubmitContractReleaseRequestData(request);
                PopToRootPage();
                if (response != null && response.d != null)
                {
                    try
                    {
                       
                        await Task.Run(() =>
                        {
                            IsLoading = false;
                        });
                        return response;

                    }
                    catch (Exception ex)
                    {
                        await Task.Run(() =>
                        {
                            IsLoading = false;
                        });
                        return null;

                    }
                }
              
                return response;
            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Run(() =>
                    {
                        //IsLoading = false;
                    });
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();

                });
                return response;
            }

            catch (Exception ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                return response;
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
                await Task.Run(() => { IsLoading = true; });
                await Task.Run(async () =>
                {
                    IsLoading = true;
                    ContractReleaseData = null;
                    try
                    {
                        ContractReleaseData = await WebServiceManager.GAZTGetContractReleaseRequestData();

                        //PopToRootPage();

                        if (ContractReleaseData != null && ContractReleaseData.d != null)
                        {
                            bindDataToUI();
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong,
                                    AppResources.Information);
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
                await Task.Run(() => { IsLoading = false; });
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
                await Task.Run(() => { IsLoading = false; });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public async void NewContractCopyAttachmentClicked()
        {
            _isInvoiceAttachments = false;
            if (ContractCopyAttachmentsListViewData == null)
            {
                ContractCopyAttachmentsListViewData = new ObservableCollection<Attachment>();
            }
            try
            {

                await PopupNavigation.Instance.PushAsync(new FilesUploadPopUpPageView(
                    ContractCopyAttachmentsListViewData.ToList(),
                    Models.ZakatInstalationModels.WhichAttachment.ContractReleaseCopy, ContractReleaseData.d.CaseGuid));

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

        public async void NewInvoiceAttachmentClicked()
        {
            _isInvoiceAttachments = true;
            if (InvoiceAttachmentsListViewData == null)
            {
                InvoiceAttachmentsListViewData = new ObservableCollection<Attachment>();
            }
            try
            {

                await PopupNavigation.Instance.PushAsync(new FilesUploadPopUpPageView(
                    InvoiceAttachmentsListViewData.ToList(),
                    Models.ZakatInstalationModels.WhichAttachment.ContractReleaseInvoice,
                    ContractReleaseData.d.CaseGuid));

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
            if (ContactPersonName == "" || Designation == "")
            {
                IsDeclarationEnabled = false;
            }
            else
            {
                IsDeclarationEnabled = true;
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

        public void ResetData()
        {

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
            FromDate = DateTime.Now;
            ToDate = DateTime.Now;
            PickerModel = null;
            setPickerModel();
            ContractCopyAttachmentsListViewData = null;
            InvoiceAttachmentsListViewData = null;
           
        }


        public void bindDataToUI()
        {
            try
            {
                EstimatedProfitForZakatPercent = Convert.ToDouble(ContractReleaseData.d.AZakatProfit);
                EstimatedProfitForTaxPercent = Convert.ToDouble(ContractReleaseData.d.ATaxProfi);
            }
            catch (Exception e)
            {

            }
        }

        #endregion
    }
}