using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Views.NewDesign.GenericPickers;
using EGAZT.Views.NewDesign.VATDeRegistration;
using EGAZT.Views.NewDesign.ZakatInstalmentPlan;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using Plugin.FilePicker;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    public class VATDeRegistrationDetailsPageViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnContinueButtonClick { get; set; }
        public ICommand GoBackBtnTapped { get; set; }
        public ICommand BackButtonTapped { get; set; }
        public ICommand CloseBtnTapped { get; set; }
        public static IsComeFromForAttachment IsComeFromForAttachment;
        public ICommand OnSaveAsDraftClicked { get; set; }
        public ICommand NewAttachmentTapped { get; set; }
        private bool isDateValidated = false;
        public ICommand GoBackClick { get; set; }
        public static Decimal AttachmentUploadedSize = 0;
        public static bool IsToBeFilled = false;
        public static string ReturnIDx = string.Empty;
        public static bool attachmentSizeVisibility = false;
        public List<decimal> SizeList = new List<decimal>();
        public int NumberOfAttachmentComingFromServer = 0;
        public VATDeregistrationModelRootObject reasonList;
        public int SelectedReasonListIndex = 0;
        #endregion

        #region Commands
        public ICommand OnVatRegistrationReasonTapped { get; set; }
        public ICommand OnVatRegistrationDateTapped { get; set; }

        public ICommand ReasonContinueBtnTapped { get; set; }
        public ICommand AttachmentsContinueBtnTapped { get; set; }
        public ICommand DeclarationContinueBtnTapped { get; set; }
        public ICommand SummaryContinueBtnTapped { get; set; }
        #endregion

        public enum ProcessStep
        {
            Step1 = 0,
            Step2, Step3, Step4, Step5, Step6
        }


        private bool _isInstructionChecked;
        public bool IsInstructionChecked
        {
            get
            {
                return _isInstructionChecked;
            }
            set
            {
                _isInstructionChecked = value;


                RaisePropertyChanged("IsInstructionChecked");
            }
        }

        private bool _isContactPersonEnabled;
        public bool IsContactPersonEnabled
        {
            get
            {
                return _isContactPersonEnabled;
            }
            set
            {
                _isContactPersonEnabled = value;


                RaisePropertyChanged("IsContactPersonEnabled");
            }
        }

        //
        private ProcessStep _currentStep { get; set; }
        public ProcessStep CurrentStep
        {
            get
            {
                return _currentStep;
            }
            set
            {
                _currentStep = value;
                if (_currentStep != null)
                {
                    CurrentOpenedTab = _currentStep;
                }
                RaisePropertyChanged(nameof(_currentStep));
                CurrentIndex = (int)_currentStep;
                RaisePropertyChanged(nameof(CurrentIndex));
                RaisePropertyChanged("CurrentStep");
            }
        }

        private ProcessStep _currentOpenedTab = ProcessStep.Step1;
        public ProcessStep CurrentOpenedTab
        {
            get
            {
                return _currentOpenedTab;
            }
            set
            {
                _currentOpenedTab = value;
                RaisePropertyChanged("CurrentOpenedTab");
            }
        }

        byte[] attachment;

        private string _attachmentName = "";
        public string AttachmentName
        {
            get
            {
                return _attachmentName;
            }
            set
            {
                _attachmentName = value;
                RaisePropertyChanged("AttachmentName");
            }
        }

        private bool _isReasonViewEnabled = true;
        public bool IsReasonViewEnabled
        {
            get
            {
                return _isReasonViewEnabled;
            }
            set
            {
                _isReasonViewEnabled = value;
                RaisePropertyChanged("IsReasonViewEnabled");
            }
        }

        private bool _isOutletViewEnabled ;
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

        private bool _isAttachmentsViewEnabled ;
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

        private bool _isDeclarationViewEnabled ;
        public bool IsDeclarationViewEnabled
        {
            get
            {
                return _isDeclarationViewEnabled;
            }
            set
            {
                _isDeclarationViewEnabled = value;
                RaisePropertyChanged("IsDeclarationViewEnabled");
            }
        }

        private bool _isReturnFilingViewEnabled ;
        public bool IsReturnFilingViewEnabled
        {
            get
            {
                return _isReturnFilingViewEnabled;
            }
            set
            {
                _isReturnFilingViewEnabled = value;
                RaisePropertyChanged("IsReturnFilingViewEnabled");
            }
        }
        private bool _isSummaryViewEnabled ;
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
        private string _titleText = string.Empty;
        public string TitleText
        {
            get
            {
                return _titleText;
            }
            set
            {
                _titleText = value;
                RaisePropertyChanged("TitleText");
            }
        }
        private string _reasonTitle = string.Empty;
        public string ReasonTitle
        {
            get
            {
                return _reasonTitle;
            }
            set
            {
                _reasonTitle = value;
                RaisePropertyChanged("ReasonTitle");
            }
        }
        private string _fileName = string.Empty;
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
                RaisePropertyChanged("FileName");
            }
        }
        private string _attachmentTitle = string.Empty;
        public string AttachmentTitle
        {
            get
            {
                return _attachmentTitle;
            }
            set
            {
                _attachmentTitle = value;
                RaisePropertyChanged("AttachmentTitle");
            }
        }
        private bool _frameIDError = false;
        public bool FrameIDError
        {
            get
            {
                return _frameIDError;
            }
            set
            {
                _frameIDError = value;
                RaisePropertyChanged("FrameIDError");
            }
        }

        private bool _isLoading;
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

        private DateTime _suspendedStartDate = DateTime.Now;
        public DateTime SuspendedStartDate
        {
            get
            {
                return _suspendedStartDate;
            }
            set
            {
                _suspendedStartDate = value;
                RaisePropertyChanged("SuspendedStartDate");
            }
        }
        private DateTime _suspendedEndDate = DateTime.Now;
        public DateTime SuspendedEndDate
        {
            get
            {
                return _suspendedEndDate;
            }
            set
            {
                _suspendedEndDate = value;
                RaisePropertyChanged("SuspendedEndDate");
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
        private int _maxIndex = 3;
        public int MaxIndex
        {
            get
            {
                return _maxIndex;
            }
            set
            {
                _maxIndex = value;
                RaisePropertyChanged("MaxIndex");
            }
        }

        private DateTime _nextFilingStartDate = DateTime.Now;
        public DateTime NextFilingStartDate
        {
            get
            {
                return _nextFilingStartDate;
            }
            set
            {
                _nextFilingStartDate = value;
                RaisePropertyChanged("NextFilingStartDate");
            }
        }
        private DateTime _nextFilingEndDate = DateTime.Now;
        public DateTime NextFilingEndDate
        {
            get
            {
                return _nextFilingEndDate;
            }
            set
            {
                _nextFilingEndDate = value;
                RaisePropertyChanged("NextFilingEndDate");
            }
        }
        private string _nextFilingDueDate = string.Empty;
        public string NextFilingDueDate
        {
            get
            {
                return _nextFilingDueDate;
            }
            set
            {
                _nextFilingDueDate = value;
                RaisePropertyChanged("NextFilingDueDate");
            }
        }
        private bool _IsVisibleNextDueDate = false;
        public bool IsVisibleNextDueDate
        {
            get
            {
                return _IsVisibleNextDueDate;
            }
            set
            {
                _IsVisibleNextDueDate = value;
                RaisePropertyChanged("IsVisibleNextDueDate");
            }
        }
        private DateTime _startDate = DateTime.Now;
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                _startDate = value;
                RaisePropertyChanged("StartDate");
            }
        }
        private DateTime _endDate = DateTime.Now;
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value;
                RaisePropertyChanged("EndDate");
            }
        }
        private ObservableCollection<Attachment> _attachmentList;
        public ObservableCollection<Attachment> AttachmentList
        {
            get
            {
                return _attachmentList;
            }
            set
            {
                _attachmentList = value;
                RaisePropertyChanged("AttachmentList");
            }
        }
        private ObservableCollection<Attachment> _vatAttachmentsListtofilter;
        public ObservableCollection<Attachment> VatAttachmentsListtofilter
        {
            get
            {
                return _vatAttachmentsListtofilter;
            }
            set
            {
                _vatAttachmentsListtofilter = value;
                RaisePropertyChanged("VatAttachmentsListtofilter");
            }
        }


        private DateTime _fromDate = DateTime.Now;
        public DateTime FromDate
        {
            get
            {
                return _fromDate;
            }
            set
            {
                _fromDate = value;
                RaisePropertyChanged("FromDate");
            }
        }
        private DateTime _toDate = DateTime.Now;
        public DateTime ToDate
        {
            get
            {
                return _toDate;
            }
            set
            {
                _toDate = value;
                RaisePropertyChanged("ToDate");
            }
        }
        private DateTime _lastIcrDate = DateTime.Now;
        public DateTime LastIcrDate
        {
            get
            {
                return _lastIcrDate;
            }
            set
            {
                _lastIcrDate = value;
                RaisePropertyChanged("LastIcrDate");
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
        private string _txtIDNumber = string.Empty;
        public string TxtIDNumber
        {
            get
            {
                return _txtIDNumber;
            }
            set
            {
                _txtIDNumber = value;
                RaisePropertyChanged("TxtIDNumber");
            }
        }
        private string _iDType = string.Empty;
        public string IDType
        {
            get
            {
                return _iDType;
            }
            set
            {
                _iDType = value;
                RaisePropertyChanged("IDType");
            }
        }
        private string _DOB = string.Empty;
        public string DOB
        {
            get
            {
                return _DOB;
            }
            set
            {
                _DOB = value;
                RaisePropertyChanged("DOB");
            }
        }
        private string _Reason = string.Empty;
        public string Reason
        {
            get
            {
                return _Reason;
            }
            set
            {
                _Reason = value;
                RaisePropertyChanged("Reason");
            }
        }
        private decimal _fileSize;
        public decimal FileSize
        {
            get
            {
                return _fileSize;
            }
            set
            {
                _fileSize = value;
                RaisePropertyChanged("FileSize");
            }
        }
        private string _contactPersonName = string.Empty;
        public string ContactPersonName
        {
            get
            {
                return _contactPersonName;
            }
            set
            {
                _contactPersonName = value;
                RaisePropertyChanged("ContactPersonName");
            }
        }
        private string _OtherField = string.Empty;
        public string OtherField
        {
            get
            {
                return _OtherField;
            }
            set
            {
                _OtherField = value;
                RaisePropertyChanged("OtherField");
            }
        }
        private GenericPickerModel _pickerModel { get; set; }
        public GenericPickerModel PickerModel
        {
            get
            {
                return _pickerModel;
            }
            set
            {
                _pickerModel = value;
                RaisePropertyChanged("PickerModel");
            }
        }
        private GenericDatePickerModel _datepickerModel { get; set; }
        public GenericDatePickerModel DatePickerModel
        {
            get
            {
                return _datepickerModel;
            }
            set
            {
                _datepickerModel = value;
                RaisePropertyChanged("DatePickerModel");
            }
        }
        public ObservableCollection<VATDeregistrationModel> c { get; set; }

        public ObservableCollection<VATDeregistrationModel> outletDecisionOptions { get; set; }
        public ObservableCollection<VATDeregistrationModel> OutletDecisionOptions
        {
            get
            {
                return outletDecisionOptions;
            }

            set
            {
                if(value!=null)
                outletDecisionOptions = value;
                RaisePropertyChanged("OutletDecisionOptions");
            }
        }
        public VATDeregistrationModel vatDeregistrationModel { get; set; }
        public VATDeregistrationModel VATDeregistrationModel
        {
            get
            {
                return vatDeregistrationModel;
            }

            set
            {
                vatDeregistrationModel = value;
                RaisePropertyChanged("VATDeregistrationModel");
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
                    IsDeclarationContinueButtonEnabled = true;
                }
                else
                {
                    IsDeclarationContinueButtonEnabled = false;
                }

                RaisePropertyChanged("IsDeclarationChecked");
            }
        }
        private Color _declarationContinueButtonnBackroundColor = Color.FromHex("#d49504");
        public Color DeclarationContinueButtonnBackroundColor
        {
            get
            {
                return _declarationContinueButtonnBackroundColor;
            }
            set
            {
                _declarationContinueButtonnBackroundColor = value;
                RaisePropertyChanged("DeclarationContinueButtonnBackroundColor");
            }
        }
        private bool _iSDeclarationContinueButtonEnabled = false;
        public bool IsDeclarationContinueButtonEnabled
        {
            get
            {
                return _iSDeclarationContinueButtonEnabled;
            }
            set
            {
                _iSDeclarationContinueButtonEnabled = value;
                if (_iSDeclarationContinueButtonEnabled)
                {
                    DeclarationContinueButtonnBackroundColor = Color.FromHex("#d49504");
                }
                else
                {
                    DeclarationContinueButtonnBackroundColor = Color.FromHex("#9EA4A9");
                }
                RaisePropertyChanged("IsDeclarationContinueButtonEnabled");
            }
        }
        //public ObservableCollection<VATDeregistrationModel> outletDocumentOptions { get; set; }
        //public ObservableCollection<VATDeregistrationModel> OutletDocumentOptions
        //{
        //    get
        //    {
        //        return outletDocumentOptions;
        //    }

        //    set
        //    {

        //        outletDocumentOptions = value;
        //        RaisePropertyChanged("OutletDocumentOptions");
        //    }
        //}

        public ObservableCollection<ResultsAttachmentItemForElgblDocSet> _attachmentTypes { get; set; }
        public ObservableCollection<ResultsAttachmentItemForElgblDocSet> AttachmentTypes
        {
            get
            {
                return _attachmentTypes;
            }

            set
            {

                if(value!=null)
                _attachmentTypes = value;
                RaisePropertyChanged("AttachmentTypes");
            }
        }

        public ObservableCollection<VATDeregistrationAttachmentsModel> attachmentsListViewData { get; set; }
        public ObservableCollection<VATDeregistrationAttachmentsModel> AttachmentsListViewData
        {
            get
            {
                return attachmentsListViewData;
            }

            set
            {

                if (value != null)
                    attachmentsListViewData = value;
                RaisePropertyChanged("AttachmentsListViewData");
            }
        }

        public ObservableCollection<VATDeregistrationSummaryModel> _vatDeregistrationSummaryReasonData { get; set; }
        public ObservableCollection<VATDeregistrationSummaryModel> VATDeregistrationSummaryReasonData
        {
            get
            {
                return _vatDeregistrationSummaryReasonData;
            }

            set
            {
                if (value != null)
                {
                    _vatDeregistrationSummaryReasonData = value;
                }

                RaisePropertyChanged("VATDeregistrationSummaryReasonData");
            }
        }

        public decimal _attachmentSize = 0;
        public decimal AttachmentSize
        {
            get
            {
                return _attachmentSize;
            }
            set
            {
                _attachmentSize = value;
                RaisePropertyChanged("AttachmentSize");
            }
        }
        public string _docTypeString;
        public string DocTypeString
        {
            get
            {
                return _docTypeString;
            }
            set
            {
                _docTypeString = value;
                RaisePropertyChanged("DocTypeString");
            }
        }


        public string _textCount { get; set; }
        public string TextCount
        {
            get
            {
                return _textCount;
            }
            set
            {
                _textCount = value;
                RaisePropertyChanged("TextCount");
            }
        }

        //TextCount
        public int _attachmentCount = 0;
        public int AttachmentCount
        {
            get
            {
                return _attachmentCount;
            }
            set
            {
                _attachmentCount = value;
                RaisePropertyChanged("AttachmentCount");
            }
        }
        private bool _attachmentSizeVisibility = attachmentSizeVisibility;
        public bool AttachmentSizeVisibility
        {
            get
            {
                return _attachmentSizeVisibility;
            }
            set
            {
                _attachmentSizeVisibility = value;
                RaisePropertyChanged("AttachmentSizeVisibility");
            }
        }
        private VATDeclaration _vATDeclarationDataForAttch;
        public VATDeclaration VATDeclarationDataForAttch
        {
            get
            {
                return _vATDeclarationDataForAttch;
            }
            set
            {
                _vATDeclarationDataForAttch = value;
                RaisePropertyChanged("VATDeclarationDataForAttch");
            }
        }
        public ObservableCollection<VATDeregistrationSummaryModel> _vatDeregistrationSummaryDeclarationData { get; set; }
        public ObservableCollection<VATDeregistrationSummaryModel> VATDeregistrationSummaryDeclarationData
        {
            get
            {
                return _vatDeregistrationSummaryDeclarationData;
            }

            set
            {
                if (value != null)
                {
                    _vatDeregistrationSummaryDeclarationData = value;
                }
                RaisePropertyChanged("VATDeregistrationSummaryDeclarationData");
            }
        }

        private VATDeregistrationModel _selectedOutletOption;
        public VATDeregistrationModel SelectedOutletOption
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

        private VATDeRegistrationDetails _vATDeRegistrationDetailsData;
        public VATDeRegistrationDetails VATDeRegistrationDetailsData
        {
            get
            {
                return _vATDeRegistrationDetailsData;
            }
            set
            {
                _vATDeRegistrationDetailsData = value;
                RaisePropertyChanged("VATDeRegistrationDetailsData");
            }
        }

        private ResultsAttachmentItemForElgblDocSet _selectedDocumentOption;
        public ResultsAttachmentItemForElgblDocSet SelectedDocumentOption
        {
            get
            {
                return _selectedDocumentOption;
            }
            set
            {
                _selectedDocumentOption = value;
                //SelectedOutletOptionIndex = OutletDecisionOptions.IndexOf(_selectedOutletOption as TINDeregistrationModel);
                RaisePropertyChanged("SelectedDocumentOption");
            }
        }
        public decimal _totalAttachmentSize = 0;
        public decimal TotalAttachmentSize
        {
            get
            {
                return _totalAttachmentSize;
            }
            set
            {
                _totalAttachmentSize = value;
                RaisePropertyChanged("TotalAttachmentSize");
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
                if (_vatAttachmentsList == value)
                {
                    return;
                }

                _vatAttachmentsList = value;
                RaisePropertyChanged("VatAttachmentsList");
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
                _stepNumber = value;
                RaisePropertyChanged("StepNumber");
            }
        }
        private VATDeregistrationAttachmentsModel _selectedAttachment { get; set; }
        public VATDeregistrationAttachmentsModel SelectedAttachment
        {
            get
            {
                return _selectedAttachment;
            }
            set
            {
                _selectedAttachment = value;
                RaisePropertyChanged("SelectedAttachment");
            }
        }

        private bool _isBackButtonVisible { get; set; }
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

        public ObservableCollection<string> _fileAttachments;
        public ObservableCollection<string> FileAttachments
        {
            get
            {
                return _fileAttachments;
            }
            set
            {
                if(value!=null)
                _fileAttachments = value;
                RaisePropertyChanged("FileAttachments");
            }
        }

        public VATDeRegistrationDetails _vATDeRegistrationDetailsForAttach;
        public VATDeRegistrationDetails VATDeRegistrationDetailsForAttach
        {
            get
            {
                return _vATDeRegistrationDetailsForAttach;
            }
            set
            {
                _vATDeRegistrationDetailsForAttach = value;
                RaisePropertyChanged("VATDeRegistrationDetailsForAttach");
            }
        }

        public bool _isOthersEditorVisible;
        public bool IsOthersEditorVisible
        {
            get
            {
                return _isOthersEditorVisible;
            }
            set
            {
                _isOthersEditorVisible = value;
                RaisePropertyChanged("IsOthersEditorVisible");
            }
        }

        public bool _isDOBEditorVisible = true;
        public bool IsDOBEditorVisible
        {
            get
            {
                return _isDOBEditorVisible;
            }
            set
            {
                _isDOBEditorVisible = value;
                RaisePropertyChanged("IsDOBEditorVisible");
            }
        }
        public bool _VoidIsVisible = true;
        public bool VoidIsVisible
        {
            get
            {
                return _VoidIsVisible;
            }
            set
            {
                _VoidIsVisible = value;
                RaisePropertyChanged("VoidIsVisible");
            }
        }

        public VATDeRegistrationDetailsPageViewModel(INavigationService navigationService, IDialogService dialogService)
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
                _navigationService.GoBack();
            });

            //CloseBtnTapped = new Command(async () =>
            //{
            //    voidButtonTapped();
            //});

            OnContinueButtonClick = new Xamarin.Forms.Command(async () =>
            {
                TitleText = "Button New";
            });

            //EnableAttachmentsView();
            GoBackBtnTapped = new Command(this.GoBackBtnClicked);
            BackButtonTapped = new Command(this.BackButtonClicked);

            //EnableSummaryView();
            ReasonContinueBtnTapped = new Command(this.ReasonContinueBtnClicked);
            AttachmentsContinueBtnTapped = new Command(this.AttachmentsContinueBtnClicked);
            DeclarationContinueBtnTapped = new Command(this.DeclarationContinueBtnClicked);
            SummaryContinueBtnTapped = new Command(this.SummaryContinueBtnClicked);
            NewAttachmentTapped = new Command(this.NewAttachmentClicked);

            OnVatRegistrationReasonTapped = new Command(this.OnVatRegistrationReasonClicked);
            OnVatRegistrationDateTapped = new Command(this.OnVatRegistrationReasonDateClicked);
            
        }

        public async Task onPageLoad()
        {
            try
            {
                VATDeregistrationModel = new VATDeregistrationModel();
                SelectedOutletOption = new VATDeregistrationModel();

                SelectedDocumentOption = new ResultsAttachmentItemForElgblDocSet();
                AddOutletDecisionOptions();
                AddOutletDocumentOptions();

                try
                {
                    if (VATDeRegistrationDetailsData != null)
                    {
                        if (VATDeRegistrationDetailsData.d != null)
                        {
                            if (VATDeRegistrationDetailsData.d.Reqtp == "S")
                            {
                                SelectedOutletOption = OutletDecisionOptions[1];
                                SelectedOutletOptionIndex = 1;
                            }
                            else
                            {
                                SelectedOutletOption = OutletDecisionOptions[0];
                                SelectedOutletOptionIndex = 0;
                            }
                        }
                        else
                        {
                            SelectedOutletOption = OutletDecisionOptions[0];
                        }
                    }
                    else
                    {
                        SelectedOutletOption = OutletDecisionOptions[0];
                    }
                }
                catch (Exception ex)
                {
                      
                }

                GetLastICRDate();

                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    IsLoading = true;
                    VATDeRegistrationDetailsData = null;
                    VATDeRegistrationDetails vATDeRegistration = null;

                    try
                    {
                        vATDeRegistration = await WebServiceManager.GAZTGetVATDeRegistrationData();


                        if (vATDeRegistration != null && vATDeRegistration.d != null)
                        {
                            if (vATDeRegistration.d.Agreeflg != null)
                            {
                                if (vATDeRegistration.d.Agreeflg)
                                {
                                    IsInstructionChecked = true;

                                }
                                else
                                {
                                    IsInstructionChecked = false;
                                }
                            }
                            //Step 5

                            if (vATDeRegistration.d.Idnumbr != null)

                                if (string.IsNullOrEmpty(IDType))
                                {
                                    TxtIDNumber = string.Empty;

                                }
                                else
                                {
                                    TxtIDNumber = vATDeRegistration.d.Idnumbr;
                                }
                            ContactPersonName = vATDeRegistration.d.Contactnm;
                            ReturnIDx = vATDeRegistration.d.ReturnIdx;

                            VATDeRegistrationDetailsData = vATDeRegistration;

                            //populateAttachments(vATDeRegistration);
                            if (vATDeRegistration.d.NotesSet != null)
                            {
                                OtherField = vATDeRegistration.d.NotesSet.results[0].Strline;

                            }
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));

                                // await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
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
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                            //await _dialogService.ShowMessage(ex.Message, AppResources.Information);
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

                EnableReasonView();
                IsDOBEditorVisible = true;
                VoidIsVisible = false;
                PopulateAttachmentsListViewTemplate();
            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                //await Task.Run(() =>
                //{

                //});
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                    //await _dialogService.ShowMessage(ex.Message, AppResources.Information);
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
                   
                });
            }
        }
        public async void NewAttachmentClicked()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new FilesUploadPopUpPageView(VatAttachmentsList.ToList(), Models.ZakatInstalationModels.WhichAttachment.VATDeregistration
                    , ReturnIDx, SelectedDocumentOption.DmsTp));

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


        private async void GetLastICRDate()
        {
            string reqType = string.Empty;
            if (SelectedOutletOptionIndex == 0)
            {
                reqType = "VT_DREG";
            }
            else
            {
                reqType = "VT_SUSP";
            }
            try
            {
                VATDeregistrationLastICRDateRootObject obj = await WebServiceManager.GAZTGETVATDeregSuspensionDate(reqType);
                LastIcrDate = obj.d.results[0].Lasticrdt;
            }
            catch (Exception e)
            {


            }
        }
        //public void populateAttachments(VATDeRegistrationDetails vATDeRegistrationDetails)
        //{
        //    if (vATDeRegistrationDetails != null && vATDeRegistrationDetails.d != null)
        //    {
        //        VATDeRegistrationDetailsForAttach = vATDeRegistrationDetails;
        //        // SetDocType();
        //        if (VATDeRegistrationDetailsForAttach.d.AttdetSet != null && VATDeRegistrationDetailsForAttach.d.AttdetSet.results != null)
        //        {
        //            if (VATDeRegistrationDetailsForAttach.d.AttdetSet.results.Count != 0)
        //            {
        //                ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(VATDeRegistrationDetailsForAttach.d.AttdetSet.results as List<Attachment>);
        //                VatAttachmentsList = myCollection;
        //                AttachmentList = myCollection;

        //                try
        //                {
        //                    foreach (var item in VatAttachmentsList)
        //                    {
        //                        if (item.Erfdt != null && item.Erftm != null)
        //                        {
        //                            FileName = item.Filename;
        //                            item.Erfdt = JsonConvert.DeserializeObject<DateTime>(@"""" + item.Erfdt + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
        //                            item.Erfdt = Convert.ToDateTime(item.Erfdt).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
        //                        }
        //                    }
        //                }
        //                catch (Exception)
        //                {
        //                }

        //                filterList();
        //                CloneAttachmentList(VatAttachmentsList);
        //            }
        //        }
        //        else
        //        {
        //            FileName = string.Empty;

        //        }
        //    }
        //}

        #region Attachments View
        public void PopulateAttachments(List<Attachment> attachments)
        {
            var attachmentsListViewData = new ObservableCollection<Attachment>();

            foreach (Attachment attachemnt in attachments)
            {
                attachmentsListViewData.Add(attachemnt);
            }
            VatAttachmentsList = attachmentsListViewData;
        }
        #endregion
        public void GoBackBtnClicked()
        {
            _navigationService.GoBack();
           

        }
        public void BackButtonClicked()
        {
            try
            {
                switch (CurrentStep)
                {
                    case ProcessStep.Step2:
                        {
                            EnableReasonView();
                            break;
                        }

                    case ProcessStep.Step3:
                        {
                            EnableAttachmentsView();

                            break;
                        }
                    case ProcessStep.Step4:
                        {
                            EnableDeclarationView();
                            break;
                        }
                    case ProcessStep.Step5:
                        {
                            EnableSummaryView();
                            break;
                        }
                }

            }
            catch (GAZTUnlockAccountException ex)
            {

            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                    // await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }
        public void SetDocType()
        {
            // VATRegistrationDetailsForAttach.d.ImFg;
            //DocTypeString = "ZVTC";

           // SelectedDocumentOption = VATDeRegistrationDetailsForAttach.d.AttdetSet.results[0].Dotyp;
        }


        [Obsolete]
        public void OnVatRegistrationReasonClicked()
        {

            ObservableCollection<string> reasonDescription = new ObservableCollection<string>();
            string reqType = string.Empty;
            if (SelectedOutletOption != null)
            {
                if (SelectedOutletOption.ActiveOutletDecisionOptions.Contains(AppResources.VATDeregistrationReasonType1))
                {
                    reqType = "VT_DREG";
                }
                else
                {
                    reqType = "VT_SUSP";
                }
            }
            try
            {
                 reasonList = WebServiceManager.GAZTGETVATDeregReasonDropdownList(reqType);
                if (reasonList != null)
                {
                    for (int i = 0; i < reasonList.d.results.Count; i++)
                    {
                        reasonDescription.Add(reasonList.d.results[i].Rdesc);
                        Reason = reasonList.d.results[i].Reason;
                    }

                    GenericPickerModel genericPickerModel = new GenericPickerModel();
                    genericPickerModel.PickerData = reasonDescription;
                    genericPickerModel.PickerTitle = AppResources.VatDeregReasonTitle;
                    genericPickerModel.PickerId = "reasonTypePicker";
                    try
                    {
                        PopupNavigation.Instance.PushAsync(new PickerPageView(genericPickerModel));
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
            }catch(Exception ex)
            {

            }
        }

        public async void OnVatRegistrationReasonDateClicked()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new CalendarPickerPageView());
            }
            catch (GAZTUnlockAccountException ex)
            {

            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                    // await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }



        public void AddOutletDecisionOptions()
        {
            OutletDecisionOptions = new ObservableCollection<VATDeregistrationModel>();
            OutletDecisionOptions.Add(new VATDeregistrationModel
            {
                ActiveOutletDecisionOptions = AppResources.VATDeregistrationReasonType1,
                ActiveOutletDecisionOptionsIsSelected = true
            });
            OutletDecisionOptions.Add(new VATDeregistrationModel
            {
                ActiveOutletDecisionOptions = AppResources.VATDeregistrationReasonType2,
                ActiveOutletDecisionOptionsIsSelected = false
            });

        }

        public void returnFilingOptionsView()
        {


        }
        public void suspendedDateValidation()
        {
             if (FromDate != DateTime.Now && ToDate != DateTime.Now)
            {
                if (LastIcrDate > FromDate)
                {
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.VatDeregistrationSuspendedDateValidation));

                  //  _dialogService.ShowMessage(AppResources.VatDeregistrationSuspendedDateValidation, AppResources.Information);
                    isDateValidated = false;
                }
                else if (ToDate <= FromDate)
                {
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.VatDeregSuspendedEndDateMismatchException));

                   // _dialogService.ShowMessage(AppResources.VatDeregSuspendedEndDateMismatchException, AppResources.Information);
                    isDateValidated = false;
                }
                else
                {
                    DateTime startDateTime = Convert.ToDateTime(FromDate);
                    DateTime toDateTime = Convert.ToDateTime(ToDate);
                    string validateSuspendedDate = WebServiceManager.GAZTGETVATDeregReturnFilingDateList(startDateTime, toDateTime);
                    VATDeregistrationSuspendedDateRootObject obj = JsonConvert.DeserializeObject<VATDeregistrationSuspendedDateRootObject>(validateSuspendedDate);
                    if (obj.d != null)
                    {
                        if (obj.d.dateResults[0].SuspDtfrom != null)
                        {
                            DateTime date = (DateTime)obj.d.dateResults[0].SuspDtfrom;
                            SuspendedStartDate = date;


                        }
                        if (obj.d.dateResults[0].SuspDtto != null)
                        {
                            DateTime date = (DateTime)obj.d.dateResults[0].SuspDtto;

                           SuspendedEndDate = date;

                        }
                        if (obj.d.dateResults[0].NextDtfrom != null)
                        {
                            DateTime date = (DateTime)obj.d.dateResults[0].NextDtfrom;

                            NextFilingStartDate = date;

                        }
                        if (obj.d.dateResults[0].NextDtfrom != null)
                        {
                            DateTime date = (DateTime)obj.d.dateResults[0].NextDtto;

                            NextFilingEndDate = date;
                        }
                        if (obj.d.dateResults[0].Duedate != null)
                        {
                            DateTime date = (DateTime)obj.d.dateResults[0].Duedate;

                            NextFilingDueDate = date.ToString("dd MMM yyyy", new CultureInfo("en-US"));
                        }

                        isDateValidated = true;
                    }
                    else
                    {

                        SignupErrorModelRootObject SignupErrorModelRootObjectModel = JsonConvert.DeserializeObject<SignupErrorModelRootObject>(validateSuspendedDate);
                        StringBuilder Message = new StringBuilder();
                        foreach (SignupErrorModelErrordetail itemerror in SignupErrorModelRootObjectModel.error.innererror.errordetails)
                        {
                            if (itemerror.code.Contains("ZD_DGVT/019"))
                            {
                                if (Message.Length > 0)
                                {
                                    Message.Append(Environment.NewLine);
                                }
                                Message.Append(itemerror.message);
                            }
                        }

                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));

                        // _dialogService.ShowMessage(Message.ToString(), AppResources.Information);
                        isDateValidated = false;
                    }

                }
            }


        }

        public async void AddOutletDocumentOptions()
        {
            try
            {
                ObservableCollection<string> reasonDescription = new ObservableCollection<string>();
                string reqType = string.Empty;
                if (SelectedOutletOptionIndex == 0)
                {
                    reqType = "VT_DREG";
                }
                else
                {
                    reqType = "VT_SUSP";
                }

                //OutletDocumentOptions = new ObservableCollection<VATDeregistrationModel>();
                string attachmentType = string.Empty;

                if (reqType != null || reqType != string.Empty)
                {

                    try
                    {

                        VATDeRegistrationAttachmentDropdownDetails reasonList = await WebServiceManager.GAZTGETVATDeregAttachmentsDropdownList(reqType).ConfigureAwait(true);
                        if (reasonList != null) { 
                        List<ResultsAttachmentItemForElgblDocSet> tempAttachmentList = reasonList.VatDeregSubItemsSet.Results.Where(m => m.Txt50 != string.Empty).ToList();
                        AttachmentTypes = new ObservableCollection<ResultsAttachmentItemForElgblDocSet>(tempAttachmentList);
                    }
                        //for (int i = 0; i < reasonList.VatDeregSubItemsSet.Results.Length; i++)
                        //{
                        //    attachmentType = reasonList.VatDeregSubItemsSet.Results[i].Txt50;
                        //    DocTypeString = reasonList.VatDeregSubItemsSet.Results[i].DmsTp;

                        //    if (attachmentType != string.Empty)
                        //    {
                        //        VATDeregistrationModel vATDeregistrationModel = new VATDeregistrationModel();
                        //        vATDeregistrationModel.ActiveOutletDecisionOptions = attachmentType;

                        //        if (i == 0)
                        //        {
                        //            vATDeregistrationModel.ActiveOutletDocumentOptionsIsSelected = true;
                        //        }
                        //        else
                        //        {
                        //            vATDeregistrationModel.ActiveOutletDocumentOptionsIsSelected = false;
                        //        }

                        //        OutletDocumentOptions.Add(vATDeregistrationModel);
                        //    }
                        //}
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            catch (Exception ex)
            {


            }

        }
       
        public async void ReasonContinueBtnClicked()
        {
            try
            {
                try
                {
                    if (SelectedOutletOptionIndex == 1)
                    {
                       
                        suspendedDateValidation();

                        if(isDateValidated)
                        { 
                            setDATA("05");
                            await saveAsDraftVoidAPIMethodCall();
                            VoidIsVisible = true;
                            EnableAttachmentsView();
                        }
                    }
                    else

                    {
                        if (ReasonTitle == string.Empty)
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

                            //await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);

                        }
                        else
                        {
                            setDATA("05");
                            await saveAsDraftVoidAPIMethodCall();
                            VoidIsVisible = true;
                            EnableAttachmentsView();
                        }

                    }

                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                        // await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        _navigationService.GoBack();
                    });
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

            if (VatAttachmentsList == null)
            {
                VatAttachmentsList = new ObservableCollection<Attachment>();

            }
        }

        public void clearData()
        {
            TxtIDNumber = string.Empty;
            ReasonTitle = string.Empty;
            if (VatAttachmentsList != null)
            {
                VatAttachmentsList.Clear();
            }
            IDType = string.Empty;


        }

        public async void AttachmentsContinueBtnClicked()
        {
            try
            {
                try
                {
                    setDATA("05");
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                        // await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        _navigationService.GoBack();
                    });
                }

                EnableDeclarationView();
            }
            catch (GAZTUnlockAccountException ex)
            {

            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                   await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                    //await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public  void DeclarationContinueBtnClicked()
        {
            try
            {
               
                try
                {
                    setDATA("05");
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                       await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                        // await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        _navigationService.GoBack();
                    });
                }

                PopulateSummaryReasonData();
                PopulateSummaryDeclarationData();
                PopulateAttachmentsListViewTemplate();
                EnableSummaryView();

               
            }
            catch (GAZTUnlockAccountException ex)
            {

            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                   await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                    //await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public async void SummaryContinueBtnClicked()
        {
            try
            {
                PopulateAttachmentsListViewTemplate();
                VATDeRegistrationDetails response = await SubmitClicked();
                if (response != null)
                {
                    _navigationService.NavigateTo(App.VATDeregistrationSuccessPage, response);
                }


            }
            catch (GAZTUnlockAccountException ex)
            {

            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                    // await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }
        public void EnableReasonView()
        {
            CurrentStep = ProcessStep.Step1;

            if (OutletDecisionOptions != null)
            {
                try
                {
                    if (VATDeRegistrationDetailsData != null)
                    {
                        if (VATDeRegistrationDetailsData.d != null)
                        {
                            if (VATDeRegistrationDetailsData.d.Reqtp == "S")
                            {
                                SelectedOutletOption = OutletDecisionOptions[1];
                                SelectedOutletOptionIndex = 1;

                            }
                            else
                            {
                                SelectedOutletOption = OutletDecisionOptions[0];
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            IsBackButtonVisible = true;
            IsReasonViewEnabled = true;
            IsOutletViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsDeclarationViewEnabled = false;
            IsSummaryViewEnabled = false;
            try
            {
                AddOutletDocumentOptions();
            }catch(Exception ex)
            {

            }

        }

        public async void EnableAttachmentsView()
        {
            if (ReasonTitle != string.Empty)
            {
                //if(OtherField.Text)

                CurrentStep = ProcessStep.Step2;

                if (AttachmentTypes != null)
                {
                    SelectedDocumentOption = AttachmentTypes[0];
                }

                IsBackButtonVisible = true;
                IsReasonViewEnabled = false;
                IsOutletViewEnabled = false;
                IsAttachmentsViewEnabled = true;
                IsDeclarationViewEnabled = false;
                IsSummaryViewEnabled = false;
            }
            else
            {
               await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

                //await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
            }
        }

        public async void EnableDeclarationView()
        {
            if (VatAttachmentsList != null)
            {
                if (VatAttachmentsList.Count != 0)
                {
                    CurrentStep = ProcessStep.Step3;

                    IsReasonViewEnabled = false;
                    IsOutletViewEnabled = false;
                    IsAttachmentsViewEnabled = false;
                    IsDeclarationViewEnabled = true;
                    IsSummaryViewEnabled = false;
                }
                else
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

                    //await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);


                }
                if (VATDeRegistrationDetailsData.d.Declareflg)
                {
                    IsDeclarationChecked = true;
                }
                else
                {
                    IsDeclarationChecked = false;
                }
            }
        }

        public async void EnableSummaryView()
        {
            if (IDType == AppResources.NationaID || IDType ==AppResources.ZZIqamaID)
            {
                if (DOB == string.Empty)
                {
                   await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseentertheBirthDate));

                    //await _dialogService.ShowMessage(AppResources.ZZPleaseentertheBirthDate, AppResources.Alerts);

                }
                else
                {
                    CurrentStep = ProcessStep.Step4;

                    IsReasonViewEnabled = false;
                    IsOutletViewEnabled = false;
                    IsAttachmentsViewEnabled = false;
                    IsDeclarationViewEnabled = false;
                    IsSummaryViewEnabled = true;
                }
            }
            else if(TxtIDNumber == string.Empty)
            {
               await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseenteravalidID));

                // await _dialogService.ShowMessage(AppResources.ZZPleaseenteravalidID, AppResources.Alerts);

            }
            else if (ContactPersonName == string.Empty)
            {
               await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseentertheName));

                //await _dialogService.ShowMessage(AppResources.ZZPleaseentertheName, AppResources.Alerts);

            }
            else
            {
                CurrentStep = ProcessStep.Step4;

                IsReasonViewEnabled = false;
                IsOutletViewEnabled = false;
                IsAttachmentsViewEnabled = false;
                IsDeclarationViewEnabled = false;
                IsSummaryViewEnabled = true;
            }


           // PopulateSummaryReasonData();
            //PopulateSummaryDeclarationData();
        }


        #region Attachments View
        public void PopulateAttachmentsListViewTemplate()
        {
            
            if (VatAttachmentsList != null)
            {
                //AttachmentsListViewData = new ObservableCollection<VATDeregistrationAttachmentsModel>();
                List<VATDeregistrationAttachmentsModel> check = new List<VATDeregistrationAttachmentsModel>();
                //AttachmentTitle = SelectedDocumentOption.ActiveOutletDocumentOptions;
                for (int i = 0; i < VatAttachmentsList.Count; i++)
                {

                    try
                    {
                        if (SelectedDocumentOption != null)
                        {
                            check.Add(new VATDeregistrationAttachmentsModel
                            {
                                FieldTitle = AppResources.VatDeregDocumentTitle,
                                FieldSubTitle = AppResources.TinDeregistration20MB,
                                AttachmentName = SelectedDocumentOption.Txt50,
                                IsAttachmentAttached = true
                            });
                        }
                        
                            check.Add(new VATDeregistrationAttachmentsModel
                        {
                            FieldTitle = AppResources.VatDeregAttachmentTitle,
                            FieldSubTitle = AppResources.TinDeregistration50MBMax,
                            AttachmentName = VatAttachmentsList[i].Filename,
                            IsAttachmentAttached = false
                        });
                    }
                    catch(Exception ex)
                    {

                    }
                }
                AttachmentsListViewData = new ObservableCollection<VATDeregistrationAttachmentsModel>(check);



            }

        }
        #endregion

        #region Summary View
        public void PopulateSummaryReasonData()
        {
            VATDeregistrationSummaryReasonData = null;
            List<VATDeregistrationSummaryModel> check = new List<VATDeregistrationSummaryModel>();
            try
            {
                check.Add(new VATDeregistrationSummaryModel
                {
                    SummaryTitle = AppResources.VatDeregRequestType,
                    SummaryData = SelectedOutletOption.ActiveOutletDecisionOptions,
                    IsEditVisible = true
                });
                check.Add(new VATDeregistrationSummaryModel
                {
                    SummaryTitle = AppResources.VatDeregReasonTitle,
                    SummaryData = ReasonTitle,
                    IsEditVisible = true
                });

                VATDeregistrationSummaryReasonData = new ObservableCollection<VATDeregistrationSummaryModel>(check);
            }catch(Exception ex)
            {

            }

        }

        public void PopulateSummaryDeclarationData()
        {
            //VATDeregistrationSummaryDeclarationData = new ObservableCollection<VATDeregistrationSummaryModel>();
            List<VATDeregistrationSummaryModel> check = new List<VATDeregistrationSummaryModel>();
            try
            {
                check.Add(new VATDeregistrationSummaryModel
                {
                    SummaryTitle = AppResources.IDType,
                    SummaryData = IDType,
                    IsEditVisible = true
                });
                check.Add(new VATDeregistrationSummaryModel
                {
                    SummaryTitle = AppResources.IDNumber,
                    SummaryData = TxtIDNumber,
                    IsEditVisible = true
                });
                if (DOB != string.Empty && IDType != AppResources.ZZGCCID)
                {
                    check.Add(new VATDeregistrationSummaryModel
                    {
                        SummaryTitle = AppResources.VatDeregDOBTitle,
                        SummaryData = DOB,
                        IsEditVisible = true
                    });
                }
                check.Add(new VATDeregistrationSummaryModel
                {
                    SummaryTitle = AppResources.VatDeregContactPerson,
                    SummaryData = ContactPersonName,
                    IsEditVisible = true
                });

                VATDeregistrationSummaryDeclarationData = new ObservableCollection<VATDeregistrationSummaryModel>(check);
            }
            catch (Exception ex)
            {
            }
        }
        public void setDocType()
        {
        }
        public async Task AddAttachmentEx()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                try
                {
                    string[] filetypes;
                    filetypes = DependencyService.Get<IDeviceInfo>().GetAttachmentTypeStringForAll();
                    var fileData = await CrossFilePicker.Current.PickFile(filetypes);

                    if (fileData != null && fileData.DataArray != null && fileData.DataArray.Length > 0)
                    {
                        attachment = fileData.DataArray;
                        AttachmentName = fileData.FileName;
                        FileName = fileData.FileName;
                        //SelectedAttachment.AttachmentName = AttachmentName;
                        //SelectedAttachment.IsAttachmentAttached = true;
                        //AttachmentsListViewData.RemoveAt(SelectedOutletOptionIndex);
                        //AttachmentsListViewData.Insert(SelectedOutletOptionIndex, SelectedAttachment);

                        if (fileData.FileName.Contains("."))
                        {
                            string Extention = fileData.FileName.Split('.')[1];

                            if (Extention.ToLower() == "doc" || Extention.ToLower() == "docx" || Extention.ToLower() == "jpg" || Extention.ToLower() == "jpeg" || Extention.ToLower() == "pdf"
                                || Extention.ToLower() == "xlsx" || Extention.ToLower() == "xls" || Extention.ToLower() == "png")
                            {
                                if (TotalAttachmentSize <= 300)
                                {
                                    AttachmentSize = Math.Round(Convert.ToDecimal((Convert.ToDouble(attachment.Length) / 1048576.0)), 2);
                                    decimal AttachmentSizeTillFourDecimal = Math.Round(Convert.ToDecimal((Convert.ToDouble(attachment.Length) / 1048576.0)), 4);
                                    FileSize = AttachmentSize;
                                    if (Convert.ToDecimal(AttachmentSize) <= 5)
                                    {
                                        if (Convert.ToDecimal(AttachmentSizeTillFourDecimal) > 0)
                                        {
                                            bool IsAttachmentPresent = false;
                                            foreach (Attachment ItemA in VATDeRegistrationDetailsForAttach.d.AttdetSet.results)
                                            {
                                               // if ((AttachmentName == ItemA.Filename) && (ItemA.Dotyp == SelectedDocumentOption.DmsTp))
                                               if(AttachmentName == ItemA.Filename)
                                                {
                                                    IsAttachmentPresent = true;
                                                }
                                            }
                                            if (IsAttachmentPresent == false)
                                            {
                                                string attachmentType = UtilityManager.GetContentType(Extention);
                                                AttachmentRootOject _attachment = await SaveAttachment(attachment, attachmentType, SelectedDocumentOption.DmsTp);


                                                if (_attachment != null && _attachment.d != null)
                                                {
                                                    AttachmentName = string.Empty;
                                                    TimeZone localZone = TimeZone.CurrentTimeZone;
                                                    string standardName = localZone.DaylightName;
                                                    _attachment.d.Erfdt = DateTime.Now.ToLocalTime().ToString("ddd, dd MMM yyy HH’:’mm’:’ss ‘UTC’ ‘zzz’");
                                                    string uploadedDate = _attachment.d.Erfdt;
                                                    uploadedDate = uploadedDate.Replace("’", "");
                                                    uploadedDate = uploadedDate.Replace("‘", "");
                                                    uploadedDate = uploadedDate.Replace("UTC", "GMT");
                                                    _attachment.d.Erfdt = uploadedDate;
                                                    _attachment.d.Dotyp = DocTypeString;
                                                    VATDeRegistrationDetailsForAttach.d.AttdetSet.results.Add(_attachment.d);
                                                    ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(VATDeRegistrationDetailsForAttach.d.AttdetSet.results as List<Attachment>);
                                                    Device.BeginInvokeOnMainThread(async () =>
                                                    {
                                                        VatAttachmentsList = myCollection;

                                                    });
                                                    VatAttachmentsList = myCollection;
                                                    foreach (var item in VatAttachmentsList)
                                                    {
                                                        try
                                                        {
                                                            if (App.IsArabic)
                                                            {
                                                                if (item.Erfdt != null)
                                                                {
                                                                    //item.Erfdt = JsonConvert.DeserializeObject<DateTime>(@"""" + item.Erfdt + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                                                    //item.Erfdt = Convert.ToDateTime(item.Erfdt).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                                                    item.Erfdt = item.Erfdt;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (item.Erfdt != null)
                                                                {
                                                                    item.Erfdt = JsonConvert.DeserializeObject<DateTime>(@"""" + item.Erfdt + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                                                    item.Erfdt = Convert.ToDateTime(item.Erfdt).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                                                }
                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            await Task.Run(() =>
                                                            {
                                                                IsLoading = false;
                                                            });
                                                        }
                                                    }
                                                    AttachmentCount++;
                                                    filterList();
                                                    CloneAttachmentList(VatAttachmentsList);
                                                    AttachmentName = string.Empty;
                                                    FileName = string.Empty;
                                                }
                                                else
                                                {
                                                    AttachmentName = string.Empty;
                                                    await Task.Run(() =>
                                                    {
                                                        IsLoading = false;
                                                    });
                                                   // await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
                                                }

                                            }


                                            else
                                            {
                                                AttachmentName = string.Empty;
                                                await Task.Run(() =>
                                                {
                                                    IsLoading = false;
                                                });
                                                await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_FileWithTheSameNameAlreadyExists, AppResources.Information);
                                            }
                                        }
                                        else
                                        {
                                            AttachmentName = string.Empty;
                                            await Task.Run(() =>
                                            {
                                                IsLoading = false;
                                            });
                                            await _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
                                        }
                                    }
                                    else
                                    {
                                        AttachmentName = string.Empty;
                                        await Task.Run(() =>
                                        {
                                            IsLoading = false;
                                        });
                                        await _dialogService.ShowMessage(AppResources.ZFilesizeshouldnotbemorethan20MB, AppResources.Information);
                                    }
                                }
                                else
                                {
                                    AttachmentName = string.Empty;
                                    await Task.Run(() =>
                                    {
                                        IsLoading = false;
                                    });
                                    await _dialogService.ShowMessage(AppResources.ZTotalFilesizeshouldnotbemorethan300MB, AppResources.Information);
                                }
                            }
                            else
                            {
                                AttachmentName = string.Empty;
                                await Task.Run(() =>
                                {
                                    IsLoading = false;
                                });
                                await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
                            }
                        }
                        else
                        {
                            AttachmentName = string.Empty;
                            await Task.Run(() =>
                            {
                                IsLoading = false;
                            });
                            await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
                        }
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private async Task<AttachmentRootOject> SaveAttachment(byte[] attachmentByteData, string contentType, string Doctype)
        {
            AttachmentRootOject _attachment = null;
            await Task.Run(() =>
            {
                IsLoading = true;
            });
            await Task.Run(async () =>
            {
                try
                {
                    AttachmentRootOject attachment = await WebServiceManager.GAZTSaveVATDeregAttachment(attachmentByteData, AttachmentName, VATDeRegistrationDetailsForAttach.d.ReturnIdx, Doctype, contentType);

                    if (attachment != null && attachment.d != null)
                    {
                        attachmentSizeVisibility = true;
                        AttachmentSizeVisibility = attachmentSizeVisibility;

                        if (SizeList != null)
                            SizeList.Add(AttachmentSize);

                        AttachmentUploadedSize = GetAttachMentSize(SizeList);
                        TotalAttachmentSize = AttachmentUploadedSize;

                        _attachment = attachment;
                    }
                    else
                    {
                        _attachment = null;
                    }
                }
                catch (Exception ex)
                {
                    //  return null;
                }
            });
            await Task.Run(() =>
            {
                IsLoading = false;
            });
            return _attachment;

        }
        public void filterList()
        {
            try
            {
                if (VATDeRegistrationDetailsForAttach != null && VATDeRegistrationDetailsForAttach.d != null && VATDeRegistrationDetailsForAttach.d.AttdetSet != null && VATDeRegistrationDetailsForAttach.d.AttdetSet.results.Count != 0)
                {
                    List<Attachment> attachmentsList = new List<Attachment>();
                    foreach (var item in VatAttachmentsList)
                    {
                        // if (item.Dotyp == DocTypeString)
                        //{
                        attachmentsList.Add(item);
                        //}
                    }
                    VatAttachmentsList = new ObservableCollection<Attachment>(attachmentsList);
                    VatAttachmentsListtofilter = new ObservableCollection<Attachment>(attachmentsList); ;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public decimal GetAttachMentSize(List<decimal> SizeList)
        {
            decimal TotalSize = 0;

            if (SizeList != null && SizeList.Count > 0)
            {
                foreach (decimal attachmentSize in SizeList)
                {
                    TotalSize = TotalSize + attachmentSize;
                }
            }

            return TotalSize;
        }


        public void CloneAttachmentList(ObservableCollection<Attachment> attachmentList)
        {
            if (attachmentList != null)
            {
                ObservableCollection<Attachment> list = new ObservableCollection<Attachment>();
                for (int i = 0; i < attachmentList.Count; i++)
                {
                   Attachment vATAttachment = new Attachment();

                    vATAttachment.RetGuid = attachmentList[i].RetGuid;
                    vATAttachment.Seqno = attachmentList[i].Seqno;
                    vATAttachment.SchGuid = attachmentList[i].SchGuid;
                    vATAttachment.Dotyp = attachmentList[i].Dotyp;
                    vATAttachment.Srno = attachmentList[i].Srno;
                    vATAttachment.Doguid = attachmentList[i].Doguid;
                    vATAttachment.AttBy = attachmentList[i].AttBy;
                    vATAttachment.Filename = attachmentList[i].Filename;
                    vATAttachment.FileExtn = attachmentList[i].FileExtn;
                    vATAttachment.Mimetype = attachmentList[i].Mimetype;
                    vATAttachment.ByPusr = attachmentList[i].ByPusr;
                    vATAttachment.Erfdt = attachmentList[i].Erfdt;
                    vATAttachment.Erftm = attachmentList[i].Erftm;
                    vATAttachment.DataVersion = attachmentList[i].DataVersion;
                    vATAttachment.DocUrl = attachmentList[i].DocUrl;
                    vATAttachment.OutletRef = attachmentList[i].OutletRef;
                    vATAttachment.Enbedit = attachmentList[i].Enbedit;
                    vATAttachment.Enbdele = attachmentList[i].Enbdele;
                    vATAttachment.Visedit = attachmentList[i].Enbdele;
                    vATAttachment.Visdel = attachmentList[i].Enbdele;


                    list.Add(vATAttachment);
                }

                AttachmentList = list;
                if (VATDeRegistrationDetailsForAttach != null && VATDeRegistrationDetailsForAttach.d.AttdetSet != null && VATDeRegistrationDetailsForAttach.d.AttdetSet.results != null)
                    if (VATDeRegistrationDetailsForAttach.d.AttdetSet.results.Count != 0)
                    {
                        ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(VATDeRegistrationDetailsForAttach.d.AttdetSet.results);
                        VatAttachmentsList = myCollection;
                    }
            }
        }

        public void setDATA(string operation)
        {

            try
            {
                string reqType = string.Empty;
                string requestTyp = string.Empty;
                //Step1

                if (IsInstructionChecked)
                {
                    VATDeRegistrationDetailsData.d.Agreeflg = true;
                }
                else
                {
                    VATDeRegistrationDetailsData.d.Agreeflg = false;
                }
                //Step2

               // if (SelectedOutletOptionIndex == 0)

                if(SelectedReasonListIndex == 0)
                {
                    reqType = "VT_DREG";
                    requestTyp = "D";
                }
                else
                {
                    reqType = "VT_SUSP";
                    requestTyp = "S";

                    VATDeRegistrationDetailsData.d.StartDate = ConvertDateFormat(FromDate);
                    VATDeRegistrationDetailsData.d.EndDate = ConvertDateFormat(ToDate);
                    VATDeRegistrationDetailsData.d.SuspDtfrom = ConvertDateFormat(SuspendedStartDate);
                    VATDeRegistrationDetailsData.d.SuspDtto = ConvertDateFormat(SuspendedEndDate);
                    VATDeRegistrationDetailsData.d.NextDtfrom = ConvertDateFormat(NextFilingStartDate);
                    VATDeRegistrationDetailsData.d.NextDtto = ConvertDateFormat(NextFilingEndDate);
                    if (!string.IsNullOrEmpty(NextFilingDueDate))
                    {
                        VATDeRegistrationDetailsData.d.Duedate = ConvertDateFormat(DateTime.Parse(NextFilingDueDate));
                    }

                }

                VATDeRegistrationDetailsData.d.TxnTpx = reqType;
                VATDeRegistrationDetailsData.d.Reqtp = requestTyp;
                VATDeRegistrationDetailsData.d.StepNumber = "03";
                VATDeRegistrationDetailsData.d.StepNumberx = "03";

                for (int i = 0; i < reasonList.d.results.Count; i++)
                {
                  if(reasonList.d.results[i].Rdesc == ReasonTitle)
                    {
                        VATDeRegistrationDetailsData.d.Reason = reasonList.d.results[i].Reason;

                    }
                }
                VATDeRegistrationDetailsData.d.Agreeflg = true;
                VATDeRegistrationDetailsData.d.Atype = "2";

                VATDeRegistrationDetailsData.d.Operationx = operation;
                if (operation == "05")
                {
                    VATDeRegistrationDetailsData.d.Fbustx = "E0013";
                    VATDeRegistrationDetailsData.d.Statusx = "E0013";
                }
                else if (operation == "01")
                {
                    VATDeRegistrationDetailsData.d.Fbustx = "E0002";
                    VATDeRegistrationDetailsData.d.Statusx = "E0002";
                }
                //Step3

                //Step 4

                if (IDType == AppResources.NationaID)
                {
                    VATDeRegistrationDetailsData.d.Type = "ZS0001";

                }
                else if (IDType == AppResources.ZZIqamaID)
                {
                    VATDeRegistrationDetailsData.d.Type = "ZS0002";

                }
                else
                {
                    VATDeRegistrationDetailsData.d.Type = "ZS0003";

                }
                VATDeRegistrationDetailsData.d.Idnumbr = TxtIDNumber;
                VATDeRegistrationDetailsData.d.Contactnm = ContactPersonName;
                //VATDeRegistrationDetailsData.d.Taxdt = DOB;
                int noteNum = 0;
                VATDeRegistrationDetailsData.d.NotesSet.results[0].Tdline = string.Empty;
                VATDeRegistrationDetailsData.d.NotesSet.results[0].Strline = string.Empty;
                if (ReasonTitle.Contains(AppResources.VatDeregistrationofReturnReason4))
                {
                    if (!string.IsNullOrEmpty(OtherField))
                    {
                        noteNum++;

                        VATDeRegistrationDetailsData.d.NotesSet.results[0].Noteno = noteNum.ToString();
                        VATDeRegistrationDetailsData.d.NotesSet.results[0].Notenoz = noteNum.ToString();
                        VATDeRegistrationDetailsData.d.NotesSet.results[0].Rcodez = "DGVT_OTH";
                        VATDeRegistrationDetailsData.d.NotesSet.results[0].Tdline = OtherField;
                    }
                }

                if (IsDeclarationChecked)
                {
                    VATDeRegistrationDetailsData.d.Declareflg = true;
                }
                else
                {
                    VATDeRegistrationDetailsData.d.Declareflg = false;
                }


            }
            catch (Exception ex)
            {

            }
        }


        public String ConvertDateFormat(DateTime newDate)
        {
            string ConvertedDate = string.Empty;
            TimeSpan span = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
            string unixTime = span.TotalSeconds.ToString("N0");
            unixTime = unixTime.Replace(",", "");
            ConvertedDate = "" + "/Date(" + unixTime + ")/";

            long unixTimestamp = ((long)(newDate.Subtract(new DateTime(1970, 1, 1))).TotalSeconds);

            unixTimestamp = unixTimestamp * 1000;

            ConvertedDate = "" + "/Date(" + unixTimestamp + ")/";

            return ConvertedDate;
        }
        public async Task<VATDeRegistrationDetails> saveAsDraftVoidAPIMethodCall()
        {
            VATDeRegistrationDetails response = new VATDeRegistrationDetails();
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });

                VATDeRegistrationDetails vATDeRegistrationDetails = new VATDeRegistrationDetails();
                response = await WebServiceManager.SaveVATDeRegistrationData(VATDeRegistrationDetailsData);
                // PopToRootPage();
                if (response != null && response.d != null)
                {
                    try
                    {
                        if (response != null && response.d != null)
                        {
                            if (response.d.Operationx.Equals("04"))
                            {
                                string number = response.d.Fbnumx;
                                string displayMessage = AppResources.VATRSuccessFullVoidMessage + " " + number;
                               await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(displayMessage));

                                // await _dialogService.ShowMessage(displayMessage, AppResources.Information);
                                _navigationService.GoBack();
                            }
                            if (response.d.Operationx.Equals("05"))
                            {
                                //  string number = response.d.Fbnumz;
                                string displayMessage = AppResources.VATRSaveasdraftMessage;
                               await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(displayMessage));

                                //await _dialogService.ShowMessage(displayMessage, AppResources.Information);
                            }

                            VATDeRegistrationDetailsData = response;

                            //Set data after api call 
                            await setDataAfterSubmitAPIAsync(response);

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
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                  //  await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    //_navigationService.GoBack();
                });
                return response;
            }

            catch (Exception ex)
            {
                return response;
            }
        }
        public async Task<VATDeRegistrationDetails> SubmitClicked()
        {
            VATDeRegistrationDetails response = new VATDeRegistrationDetails();
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });

                setDATA("01");
                VATDeRegistrationDetails vATDeRegistrationDetails = new VATDeRegistrationDetails();
                response = await WebServiceManager.SaveVATDeRegistrationData(VATDeRegistrationDetailsData);
                // PopToRootPage();
                if (response != null && response.d != null)
                {
                    try
                    {
                        if (response != null && response.d != null)
                        {
                            if (response.d.Operationx.Equals("04"))
                            {
                                string number = response.d.Fbnumx;
                                string displayMessage = AppResources.VATRSuccessFullVoidMessage + " " + number;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(displayMessage));

                                //await _dialogService.ShowMessage(displayMessage, AppResources.Information);
                                _navigationService.GoBack();
                            }
                            if (response.d.Operationx.Equals("05"))
                            {
                                //  string number = response.d.Fbnumz;
                                string displayMessage = AppResources.VATRSaveasdraftMessage;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(displayMessage));

                                // await _dialogService.ShowMessage(displayMessage, AppResources.Information);
                            }

                            VATDeRegistrationDetailsData = response;

                            //Set data after api call 
                            setDataAfterSubmitAPIAsync(response);

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
                   await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                    //await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    //_navigationService.GoBack();
                });
                return response;
            }

            catch (Exception ex)
            {
                return response;
            }
        }
        public async Task setDataAfterSubmitAPIAsync(VATDeRegistrationDetails vATRegistration)
        {
            //Set applicable buttons
            await Task.Run(() =>
            {
                IsLoading = true;
            });

            await Task.Run(async () =>
            {
                //                VATDeRegistrationOtherDetails vATRegistrationOther = await WebServiceManager.GAZTGetVATRegistrationDataWithButtons(vATRegistration.d.Fbnumz, vATRegistration.d.Officerz, vATRegistration.d.Statusz, vATRegistration.d.TxnTpz, "ZTAX_VT_REG");

                // PopToRootPage();// If seesion Expired it will navigate to Dashboard page

                // if (vATDeRegistrationOtherDetails = vATDeRegistrationOther;

                //SetApplicableButtons();
                //}
                //IsLoading = false;
            });
        }

        public void SetTextCount(int length)
        {
            TextCount = length + "/" + "1000";
        }

        #endregion
    }
}
