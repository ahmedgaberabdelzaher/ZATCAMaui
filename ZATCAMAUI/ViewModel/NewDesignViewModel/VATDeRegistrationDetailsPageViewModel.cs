using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Windows.Input;


using Newtonsoft.Json;
using Mopups.Services;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Views.NewDesign.Common;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using ZATCAMAUI.Core.Interfaces;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{

    public class VATDeRegistrationDetailsPageViewModel : BaseViewModel
    {
        #region Variable
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
        public Boolean shouldShowDialog = false;
        private string DeregRequestTypeSerialised { get; set; }
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
        private bool _isRequestTypeEnabled;
        public bool IsRequestTypeEnabled
        {
            get => _isRequestTypeEnabled;
            set
            {
                _isRequestTypeEnabled = value;
                OnPropertyChanged(nameof(IsRequestTypeEnabled));
            }
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
                if (_isInstructionChecked == value) return;
                _isInstructionChecked = value;


                OnPropertyChanged("IsInstructionChecked");
            }
        }
        private bool _isDeregReasonVisible;
        public bool IsDeregReasonVisible
        {
            get
            {
                return _isDeregReasonVisible;
            }
            set
            {
                if (_isDeregReasonVisible == value) return;

                _isDeregReasonVisible = value;
                OnPropertyChanged("IsDeregReasonVisible");
            }
        }
        private bool _isSuspReasonVisible;
        public bool IsSuspReasonVisible
        {
            get
            {
                return _isSuspReasonVisible;
            }
            set
            {
                if (_isSuspReasonVisible == value) return;

                _isSuspReasonVisible = value;
                OnPropertyChanged("IsSuspReasonVisible");
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
                if (_isContactPersonEnabled == value) return;

                _isContactPersonEnabled = value;


                OnPropertyChanged("IsContactPersonEnabled");
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
                if (_currentStep == value) return;

                _currentStep = value;
                CurrentOpenedTab = _currentStep;
                OnPropertyChanged(nameof(CurrentStep));
                CurrentIndex = (int)_currentStep;
                OnPropertyChanged(nameof(CurrentIndex));
                OnPropertyChanged("CurrentStep");
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
                if (_currentOpenedTab == value) return;

                _currentOpenedTab = value;
                OnPropertyChanged("CurrentOpenedTab");
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
                if (_attachmentName == value) return;

                _attachmentName = value;
                OnPropertyChanged("AttachmentName");
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
                if (_isReasonViewEnabled == value) return;

                _isReasonViewEnabled = value;
                OnPropertyChanged("IsReasonViewEnabled");
            }
        }

        private bool _isOutletViewEnabled;
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

        private bool _isAttachmentsViewEnabled;
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


        private bool _isReturnFilingViewEnabled;
        public bool IsReturnFilingViewEnabled
        {
            get
            {
                return _isReturnFilingViewEnabled;
            }
            set
            {
                if (_isReturnFilingViewEnabled == value) return;

                _isReturnFilingViewEnabled = value;
                OnPropertyChanged("IsReturnFilingViewEnabled");
            }
        }
        private bool _isSummaryViewEnabled;
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
        private string _titleText = string.Empty;
        public string TitleText
        {
            get
            {
                return _titleText;
            }
            set
            {
                if (_titleText == value) return;

                _titleText = value;
                OnPropertyChanged("TitleText");
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
                if (_reasonTitle == value) return;

                _reasonTitle = value;
                if (_reasonTitle != null)
                {
                    if (_reasonTitle.Contains(AppResources.VatDeregistrationofReturnReason4))
                    {
                        IsOthersEditorVisible = true;
                        OtherField = string.Empty;

                    }
                    else
                    {
                        IsOthersEditorVisible = false;


                    }
                }
                //else
                //{
                //    IsOthersEditorVisible = true;
                //}
                OnPropertyChanged("ReasonTitle");
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
                if (_fileName == value) return;

                _fileName = value;
                OnPropertyChanged("FileName");
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
                if (_attachmentTitle == value) return;

                _attachmentTitle = value;
                OnPropertyChanged("AttachmentTitle");
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
                if (_frameIDError == value) return;

                _frameIDError = value;
                OnPropertyChanged("FrameIDError");
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
                if (_suspendedStartDate == value) return;

                _suspendedStartDate = value;
                OnPropertyChanged("SuspendedStartDate");
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
                if (_suspendedEndDate == value) return;

                _suspendedEndDate = value;
                OnPropertyChanged("SuspendedEndDate");
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
        private int _maxIndex = 3;
        public int MaxIndex
        {
            get
            {
                return _maxIndex;
            }
            set
            {
                if (_maxIndex == value) return;

                _maxIndex = value;
                OnPropertyChanged("MaxIndex");
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
                if (_nextFilingStartDate == value) return;

                _nextFilingStartDate = value;
                OnPropertyChanged("NextFilingStartDate");
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
                if (_nextFilingEndDate == value) return;

                _nextFilingEndDate = value;
                OnPropertyChanged("NextFilingEndDate");
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
                if (_nextFilingDueDate == value) return;

                _nextFilingDueDate = value;
                OnPropertyChanged("NextFilingDueDate");
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
                if (_IsVisibleNextDueDate == value) return;

                _IsVisibleNextDueDate = value;
                OnPropertyChanged("IsVisibleNextDueDate");
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
                if (_startDate == value) return;

                _startDate = value;
                OnPropertyChanged("StartDate");
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
                if (_endDate == value) return;

                _endDate = value;
                OnPropertyChanged("EndDate");
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
                if (_attachmentList == value) return;

                _attachmentList = value;
                OnPropertyChanged("AttachmentList");
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
                if (_vatAttachmentsListtofilter == value) return;

                _vatAttachmentsListtofilter = value;
                OnPropertyChanged("VatAttachmentsListtofilter");
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
                if (_fromDate == value) return;

                _fromDate = value;
                OnPropertyChanged("FromDate");
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
                if (_toDate == value) return;

                _toDate = value;
                OnPropertyChanged("ToDate");
            }
        }

        private string _lastIcrDate = DateTime.Now.ToString();
        public string LastIcrDate
        {
            get
            {
                return _lastIcrDate;
            }
            set
            {
                if (_lastIcrDate == value) return;

                _lastIcrDate = value;
                OnPropertyChanged("LastIcrDate");
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
        private string _txtIDNumber = string.Empty;
        public string TxtIDNumber
        {
            get
            {
                return _txtIDNumber;
            }
            set
            {
                if (_txtIDNumber == value) return;

                _txtIDNumber = value;
                OnPropertyChanged("TxtIDNumber");
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
                if (_iDType == value) return;

                _iDType = value;
                OnPropertyChanged("IDType");
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
                if (_DOB == value) return;

                _DOB = value;
                OnPropertyChanged("DOB");
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
                if (_Reason == value) return;

                _Reason = value;
                OnPropertyChanged("Reason");
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
                if (_fileSize == value) return;

                _fileSize = value;
                OnPropertyChanged("FileSize");
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
                if (_contactPersonName == value) return;

                _contactPersonName = value;
                OnPropertyChanged("ContactPersonName");
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
                if (_OtherField == value) return;

                _OtherField = value;
                OnPropertyChanged("OtherField");
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
                if (_pickerModel == value) return;

                _pickerModel = value;
                OnPropertyChanged("PickerModel");
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
                if (_datepickerModel == value) return;

                _datepickerModel = value;
                OnPropertyChanged("DatePickerModel");
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
                if (outletDecisionOptions == value) return;

                if (value != null)
                    outletDecisionOptions = value;
                OnPropertyChanged("OutletDecisionOptions");
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
                if (vatDeregistrationModel == value) return;

                vatDeregistrationModel = value;
                OnPropertyChanged("VATDeregistrationModel");
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
                    IsDeclarationContinueButtonEnabled = true;
                }
                else
                {
                    IsDeclarationContinueButtonEnabled = false;
                }

                OnPropertyChanged("IsDeclarationChecked");
            }
        }
        private Color _declarationContinueButtonnBackroundColor = (Color)Application.Current.Resources["Secondary"];
        public Color DeclarationContinueButtonnBackroundColor
        {
            get
            {
                return _declarationContinueButtonnBackroundColor;
            }
            set
            {
                if (_declarationContinueButtonnBackroundColor == value) return;

                _declarationContinueButtonnBackroundColor = value;
                OnPropertyChanged("DeclarationContinueButtonnBackroundColor");
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
                if (_iSDeclarationContinueButtonEnabled == value) return;

                _iSDeclarationContinueButtonEnabled = value;
                //if (_iSDeclarationContinueButtonEnabled)
                //{
                //    DeclarationContinueButtonnBackroundColor =  (Color)Application.Current.Resources["Secondary"];
                //}
                //else
                //{
                //    DeclarationContinueButtonnBackroundColor =  (Color)Application.Current.Resources["ButtonGray"];
                //}
                OnPropertyChanged("IsDeclarationContinueButtonEnabled");
            }
        }


        public ObservableCollection<ResultsAttachmentItemForElgblDocSet> _attachmentTypes { get; set; }
        public ObservableCollection<ResultsAttachmentItemForElgblDocSet> AttachmentTypes
        {
            get
            {
                return _attachmentTypes;
            }

            set
            {
                if (_attachmentTypes == value) return;

                if (value != null)
                    _attachmentTypes = value;
                OnPropertyChanged("AttachmentTypes");
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
                if (attachmentsListViewData == value) return;

                if (value != null)
                    attachmentsListViewData = value;
                OnPropertyChanged("AttachmentsListViewData");
            }
        }

        public List<VATDeregistrationSummaryModel> _vatDeregistrationSummaryReasonData { get; set; }
        public List<VATDeregistrationSummaryModel> VATDeregistrationSummaryReasonData
        {
            get
            {
                return _vatDeregistrationSummaryReasonData;
            }

            set
            {
                if (_vatDeregistrationSummaryReasonData == value) return;

                _vatDeregistrationSummaryReasonData = value;

                OnPropertyChanged("VATDeregistrationSummaryReasonData");
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
                if (_attachmentSize == value) return;

                _attachmentSize = value;
                OnPropertyChanged("AttachmentSize");
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
                if (_docTypeString == value) return;

                _docTypeString = value;
                OnPropertyChanged("DocTypeString");
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
                if (_textCount == value) return;

                _textCount = value;
                OnPropertyChanged("TextCount");
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
                if (_attachmentCount == value) return;

                _attachmentCount = value;
                OnPropertyChanged("AttachmentCount");
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
                if (_attachmentSizeVisibility == value) return;

                _attachmentSizeVisibility = value;
                OnPropertyChanged("AttachmentSizeVisibility");
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
                if (_vATDeclarationDataForAttch == value) return;

                _vATDeclarationDataForAttch = value;
                OnPropertyChanged("VATDeclarationDataForAttch");
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
                if (_vatDeregistrationSummaryDeclarationData == value) return;

                if (value != null)
                {
                    _vatDeregistrationSummaryDeclarationData = value;
                }
                OnPropertyChanged("VATDeregistrationSummaryDeclarationData");
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
                if (_selectedOutletOption == value) return;
                _selectedOutletOption = value;
                if (_selectedOutletOption != null)
                {
                    _selectedOutletOption.TextCol = Colors.White;
                    _selectedOutletOption.ImgSource = "vat_tile_listofsignup";
                }
                if (value != null)
                {
                    DeregRequestTypeSerialised = JsonConvert.SerializeObject(new List<string> { _selectedOutletOption.ActiveOutletDecisionOptions });
                }

                if (OutletDecisionOptions == null)
                    AddOutletDecisionOptions();
                SelectedOutletOptionIndex = OutletDecisionOptions.IndexOf(value);
                SelectedReasonListIndex = OutletDecisionOptions.IndexOf(value);
                //SelectedOutletOptionIndex = OutletDecisionOptions.IndexOf(_selectedOutletOption as TINDeregistrationModel);
                OnPropertyChanged("SelectedOutletOption");
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
                if (_vATDeRegistrationDetailsData == value) return;

                _vATDeRegistrationDetailsData = value;
                OnPropertyChanged("VATDeRegistrationDetailsData");
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
                if (_selectedDocumentOption == value) return;

                _selectedDocumentOption = value;
                if (_selectedDocumentOption != null)
                {
                    _selectedDocumentOption.TextCol = Colors.White;
                    _selectedDocumentOption.ImgSource = "vat_tile_listofsignup";
                }
                OnPropertyChanged("SelectedDocumentOption");
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
                if (_totalAttachmentSize == value) return;

                _totalAttachmentSize = value;
                OnPropertyChanged("TotalAttachmentSize");
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
                OnPropertyChanged("VatAttachmentsList");
            }
        }
        private ObservableCollection<Attachment> _filtervatAttachmentsList = new ObservableCollection<Attachment>();
        public ObservableCollection<Attachment> FilterVatAttachmentsList
        {
            get
            {
                return _filtervatAttachmentsList;
            }
            set
            {
                _filtervatAttachmentsList = value;
                OnPropertyChanged(nameof(FilterVatAttachmentsList));
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
                if (_stepNumber == value) return;

                _stepNumber = value;
                OnPropertyChanged("StepNumber");
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
                if (_selectedAttachment == value) return;

                _selectedAttachment = value;
                OnPropertyChanged("SelectedAttachment");
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
                if (_isBackButtonVisible == value) return;

                _isBackButtonVisible = value;
                OnPropertyChanged("IsBackButtonVisible");
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
                if (_fileAttachments == value) return;

                if (value != null)
                    _fileAttachments = value;
                OnPropertyChanged("FileAttachments");
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
                if (_vATDeRegistrationDetailsForAttach == value) return;

                _vATDeRegistrationDetailsForAttach = value;
                OnPropertyChanged("VATDeRegistrationDetailsForAttach");
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
                if (value == false)
                {
                    OtherField = string.Empty;
                }
                if (_isOthersEditorVisible == value) return;

                _isOthersEditorVisible = value;
                OnPropertyChanged("IsOthersEditorVisible");
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
                if (_isDOBEditorVisible == value) return;

                _isDOBEditorVisible = value;
                OnPropertyChanged("IsDOBEditorVisible");
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
                if (_VoidIsVisible == value) return;

                _VoidIsVisible = value;
                OnPropertyChanged("VoidIsVisible");
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

        private TextAlignment _termsAlignment;
        public TextAlignment TermsAlignment
        {
            get { return _termsAlignment; }
            set
            {
                if (_termsAlignment == value) return;
                _termsAlignment = value;
                OnPropertyChanged("TermsAlignment");
            }
        }
        public VATDeRegistrationDetailsPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)

        {
            GoBackClick = new Command(() =>
            {
                _navigationService.GoBack();
            });

            

            OnContinueButtonClick = new Command(() =>
            {
                TitleText = "Button New";
            });

            GoBackBtnTapped = new Command(this.GoBackBtnClicked);
            BackButtonTapped = new Command(this.BackButtonClicked);

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
            string reqType = string.Empty;

            try
            {
                VATDeregistrationModel = new VATDeregistrationModel();
                SelectedOutletOption = new VATDeregistrationModel();

                SelectedDocumentOption = new ResultsAttachmentItemForElgblDocSet();
                AddOutletDocumentOptions();
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
                        vATDeRegistration = await VatRegistrationWebServiceManager.GAZTGetVATDeRegistrationData();
                        if (vATDeRegistration != null && vATDeRegistration.d != null)
                        {
                            if (vATDeRegistration.d.headerSet.Agreeflg)
                            {
                                IsInstructionChecked = true;

                            }
                            else
                            {
                                IsInstructionChecked = false;
                            }


                            if (vATDeRegistration.d.headerSet.Declareflg)
                            {
                                IsDeclarationChecked = true;

                            }
                            else
                            {
                                IsDeclarationChecked = false;
                            }

                            //Step 5

                            if (vATDeRegistration.d.headerSet.Idnumbr != null)
                            {
                                TxtIDNumber = vATDeRegistration.d.headerSet.Idnumbr;

                            }
                            if (vATDeRegistration.d.headerSet.Type == "ZS0001")
                            {
                                IDType = AppResources.NationaID;

                            }
                            else if (vATDeRegistration.d.headerSet.Type == "ZS0002")
                            {
                                IDType = AppResources.ZZIqamaID;

                            }
                            else if (vATDeRegistration.d.headerSet.Type == "ZS0003")
                            {
                                IDType = AppResources.ZZGCCID;

                            }

                            if (vATDeRegistration.d.headerSet.Fbstax == "IP11" && vATDeRegistration.d.headerSet.Fbustx == "E0018")
                                IsRequestTypeEnabled = false;
                            else
                                IsRequestTypeEnabled = true;

                            ContactPersonName = vATDeRegistration.d.headerSet.Contactnm;
                            ReturnIDx = vATDeRegistration.d.headerSet.ReturnIdx;

                            VATDeRegistrationDetailsData = vATDeRegistration;
                            SelectedOutletOption = OutletDecisionOptions[VATDeRegistrationDetailsData != null
                                     && VATDeRegistrationDetailsData.d != null &&
                                     VATDeRegistrationDetailsData.d.headerSet.Reqtp == "S" ? 1 : 0];

                            if (SelectedOutletOption.ActiveOutletDecisionOptions.Contains(AppResources.VATDeregistrationReasonType1))
                            {
                                reqType = "VT_DREG";
                            }
                            else
                            {
                                reqType = "VT_SUSP";
                            }
                            if (reasonList == null)
                            {
                                reasonList = await VATDeregistrationWebServiceManager.GAZTGETVATDeregReasonDropdownList(reqType);

                            }
                            for (int i = 0; i < reasonList.d.results.Count; i++)
                            {
                                if (reasonList.d.results[i].Reason == vATDeRegistration.d.headerSet.Reason)
                                {
                                    ReasonTitle = reasonList.d.results[i].Rdesc;

                                }
                            }

                            string convertedDate = string.Empty;

                            if (vATDeRegistration.d.headerSet.SuspDtfrom != null)
                            {
                                convertedDate = JsonConvert.DeserializeObject<DateTime>(@"""" + vATDeRegistration.d.headerSet.SuspDtfrom + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                SuspendedStartDate = Convert.ToDateTime(convertedDate);//filling period
                            }

                            if (vATDeRegistration.d.headerSet.SuspDtto != null)
                            {
                                convertedDate = JsonConvert.DeserializeObject<DateTime>(@"""" + vATDeRegistration.d.headerSet.SuspDtto + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                SuspendedEndDate = Convert.ToDateTime(convertedDate);//filling perio end date

                            }

                            if (vATDeRegistration.d.headerSet.StartDate != null)
                            {
                                convertedDate = JsonConvert.DeserializeObject<DateTime>(@"""" + vATDeRegistration.d.headerSet.StartDate + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                FromDate = Convert.ToDateTime(convertedDate);//suspension start date
                            }

                            if (vATDeRegistration.d.headerSet.EndDate != null)
                            {
                                convertedDate = JsonConvert.DeserializeObject<DateTime>(@"""" + vATDeRegistration.d.headerSet.EndDate + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                ToDate = Convert.ToDateTime(convertedDate);//suspension end date
                            }

                            if (vATDeRegistration.d.headerSet.NextDtfrom != null)
                            {
                                convertedDate = JsonConvert.DeserializeObject<DateTime>(@"""" + vATDeRegistration.d.headerSet.NextDtfrom + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                NextFilingStartDate = Convert.ToDateTime(convertedDate);//next filling start date
                            }

                            if (vATDeRegistration.d.headerSet.NextDtto != null)
                            {
                                convertedDate = JsonConvert.DeserializeObject<DateTime>(@"""" + vATDeRegistration.d.headerSet.NextDtto + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                NextFilingEndDate = Convert.ToDateTime(convertedDate);//next filling end date
                            }

                            //populateAttachments(vATDeRegistration);
                            //await suspendedDateValidation();
                            if (vATDeRegistration.d.NotesSet != null)
                            {
                                if (vATDeRegistration.d.NotesSet != null && vATDeRegistration.d.NotesSet.Count > 0)
                                {
                                    OtherField = vATDeRegistration.d.NotesSet[0].Strline;
                                }
                            }
                        }
                        else
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));

                                _navigationService.GoBack();
                            });
                        }
                        IsLoading = false;

                        PopulateAttachments(vATDeRegistration.d.AttdetSet);
                    }
                    catch (GAZTVATRegistrationInProcessException )
                    {
                    }
                    catch (InternetException ex)
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                            IsLoading = false;
                            _navigationService.GoBack();
                        });

                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

                EnableReasonView();
                IsDOBEditorVisible = false;
                VoidIsVisible = false;


                PopulateAttachmentsListViewTemplate();
            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                    _navigationService.GoBack();
                });

            }
            catch (Exception ex)
            {
                
                
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
        }
        public async void NewAttachmentClicked()
        {
            if (MopupService.Instance.PopupStack.Count > 0) return;
            try
            {
                // need to set null
                await MopupService.Instance.PushAsync(new FilesUploadPopUpPageView(VatAttachmentsList.ToList(),WhichAttachment.VATDeregistration
                    , ReturnIDx, SelectedDocumentOption.DmsTp));

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
            catch (Exception ex)
            {
                
            }
        }

        public void updateattachmentList()
        {
            if (VatAttachmentsList != null)
            {
                List<Attachment> attachmentsList = new List<Attachment>();
                foreach (var item in VatAttachmentsList)
                {
                    if (item.Dotyp == SelectedDocumentOption?.DmsTp)
                    {
                        attachmentsList.Add(item);
                    }
                }
                FilterVatAttachmentsList = new ObservableCollection<Attachment>(attachmentsList);
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
                VATDeregistrationLastICRDateRootObject obj = await VATDeregistrationWebServiceManager.GAZTGETVATDeregSuspensionDate(reqType);
                LastIcrDate = obj.d[0].Lasticrdt;
            }
            catch (Exception)
            {

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
                    case ProcessStep.Step1:
                        {
                            _navigationService.GoBack();
                            break;
                        }
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
            catch (GAZTUnlockAccountException )
            {
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                    _navigationService.GoBack();
                });
            }
        }

       

        [Obsolete]
        public async void OnVatRegistrationReasonClicked()
        {

            List<string> reasonDescription = new List<string>();
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
                reasonList = await VATDeregistrationWebServiceManager.GAZTGETVATDeregReasonDropdownList(reqType);
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
                       await MopupService.Instance.PushAsync(new PickerPageView(genericPickerModel));
                    }
                    catch (GAZTUnlockAccountException )
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
                    catch (Exception )
                    {
                       
                    }
                }
            }
            catch (Exception )
            {
            }
        }

        public async void OnVatRegistrationReasonDateClicked()
        {
            try
            {
                await MopupService.Instance.PushAsync(new CalendarPickerPageView());
            }
            catch (GAZTUnlockAccountException ex)
            {
                
                

            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

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

        public async Task<bool> suspendedDateValidation()
        {
            try
            {
                if (FromDate != DateTime.Now && ToDate != DateTime.Now)
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.VatDeregistrationSuspendedDateValidation));

                    isDateValidated = false;
                }
                else if (ToDate <= FromDate)
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.VatDeregSuspendedEndDateMismatchException));

                    isDateValidated = false;
                }
                else
                {
                    DateTime startDateTime = Convert.ToDateTime(FromDate);
                    DateTime toDateTime = Convert.ToDateTime(ToDate);
                    string validateSuspendedDate = VATDeregistrationWebServiceManager.GAZTGETVATDeregReturnFilingDateList(startDateTime, toDateTime);
                    VATDeregistrationSuspendedDateRootObject obj = JsonConvert.DeserializeObject<VATDeregistrationSuspendedDateRootObject>(validateSuspendedDate);
                    if (Convert.ToDateTime(LastIcrDate) > FromDate)
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.VatDeregistrationSuspendedDateValidation));

                        //  _dialogService.ShowMessage(AppResources.VatDeregistrationSuspendedDateValidation, AppResources.Information);
                        isDateValidated = false;
                    }
                    else if (ToDate <= FromDate)
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.VatDeregSuspendedEndDateMismatchException));

                        // _dialogService.ShowMessage(AppResources.VatDeregSuspendedEndDateMismatchException, AppResources.Information);
                        isDateValidated = false;
                    }
                    else
                    {
                        //DateTime startDateTime = Convert.ToDateTime(FromDate);
                        //DateTime toDateTime = Convert.ToDateTime(ToDate);
                        //string validateSuspendedDate = VATDeregistrationWebServiceManager.GAZTGETVATDeregReturnFilingDateList(startDateTime, toDateTime);
                        //VATDeregistrationSuspendedDateRootObject obj = JsonConvert.DeserializeObject<VATDeregistrationSuspendedDateRootObject>(validateSuspendedDate);
                        if (obj.d != null)
                        {
                            if (obj.d.dateResults[0].SuspDtfrom != null)
                            {
                                DateTime date = Convert.ToDateTime(obj.d.dateResults[0].SuspDtfrom);
                                SuspendedStartDate = date;


                            }
                            if (obj.d.dateResults[0].SuspDtto != null)
                            {
                                DateTime date = Convert.ToDateTime(obj.d.dateResults[0].SuspDtto);

                                SuspendedEndDate = date;

                            }
                            if (obj.d.dateResults[0].NextDtfrom != null)
                            {
                                DateTime date = Convert.ToDateTime(obj.d.dateResults[0].NextDtfrom);

                                NextFilingStartDate = date;

                            }
                            if (obj.d.dateResults[0].NextDtfrom != null)
                            {
                                DateTime date = Convert.ToDateTime(obj.d.dateResults[0].NextDtto);

                                NextFilingEndDate = date;
                            }
                            if (obj.d.dateResults[0].Duedate != null)
                            {
                                DateTime date = (DateTime)obj.d.dateResults[0].Duedate;

                                NextFilingDueDate = date.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
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

                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));

                            // _dialogService.ShowMessage(Message.ToString(), AppResources.Information);
                            isDateValidated = false;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                
                
            }
            return true;

        }

        public async void AddOutletDocumentOptions()
        {
            try
            {
                ObservableCollection<string> reasonDescription = new ObservableCollection<string>();
                string reqType = string.Empty;
                if (SelectedOutletOption != null && SelectedOutletOption.ActiveOutletDecisionOptions != null && SelectedOutletOption.ActiveOutletDecisionOptions.Equals(AppResources.VATDeregistrationReasonType1))
                
                {
                    reqType = "VT_DREG";
                    IsDeregReasonVisible = true;
                    IsSuspReasonVisible = false;


                }
                else
                {
                    reqType = "VT_SUSP";
                    IsDeregReasonVisible = false;
                    IsSuspReasonVisible = true;


                }

                string attachmentType = string.Empty;

                if (reqType != null || reqType != string.Empty)
                {

                    try
                    {

                        VATDeRegistrationAttachmentDropdownDetails reasonList = await VATDeregistrationWebServiceManager.GAZTGETVATDeregAttachmentsDropdownList(reqType).ConfigureAwait(true);
                        if (reasonList != null)
                        {
                            List<ResultsAttachmentItemForElgblDocSet> tempAttachmentList = reasonList.VatDeregSubItemsSet.Where(m => m.Txt50 != string.Empty).ToList();
                            AttachmentTypes = new ObservableCollection<ResultsAttachmentItemForElgblDocSet>(tempAttachmentList);
                        }
                        
                    }
                    catch (Exception )
                    {
                       
                    }
                }
            }
            catch (Exception )
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

                        if (isDateValidated)
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
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

                            //await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);

                        }
                        else if (ReasonTitle.Contains(AppResources.VatDeregistrationofReturnReason4))
                        {
                            if (string.IsNullOrEmpty(OtherField))
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));
                            }
                            else
                            {
                                setDATA("05");
                                await saveAsDraftVoidAPIMethodCall();
                                VoidIsVisible = true;
                                EnableAttachmentsView();
                            }
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
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

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
                MainThread.BeginInvokeOnMainThread(async () =>
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

        public void AttachmentsContinueBtnClicked()
        {
            try
            {
                try
                {
                    setDATA("05");
                }
                catch (InternetException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

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
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                    _navigationService.GoBack();
                });
            }
        }

        public async void DeclarationContinueBtnClicked()
        {
            try
            {

                try
                {
                    if (!IsDeclarationChecked)
                    {
                        return;
                    }
                    else if (string.IsNullOrEmpty(IDType))
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));
                        return;
                    }
                    else if (IDType == AppResources.NationaID || IDType == AppResources.ZZIqamaID)
                    {
                        if (IsDOBEditorVisible && string.IsNullOrEmpty(DOB))
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));
                            return;
                        }
                        else if (IDType == AppResources.NationaID || IDType == AppResources.ZZIqamaID)
                        {
                            if (IsDOBEditorVisible && string.IsNullOrEmpty(DOB))
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));
                                return;
                            }
                        }
                        else if (IDType == AppResources.ZZGCCID && string.IsNullOrEmpty(TxtIDNumber))
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));
                            return;
                        }
                        if (FrameIDError)
                        {
                            return;
                        }
                    }
                    else if (IDType == AppResources.ZZGCCID && string.IsNullOrEmpty(TxtIDNumber))
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));
                        return;
                    }
                    if (FrameIDError)
                    {
                        //await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));
                        return;
                    }
                    setDATA("05");
                    shouldShowDialog = true;
                    await saveAsDraftVoidAPIMethodCall();

                }
                catch (InternetException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                        _navigationService.GoBack();
                    });
                }
                PopulateSummaryDeclarationData();
                EnableSummaryView();
                PopulateAttachmentsListViewTemplate();

            }
            catch (GAZTUnlockAccountException ex)
            {
                
                
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                    //await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public async void SummaryContinueBtnClicked()
        {
            try
            {
                VATDeRegistrationDetails response = await SubmitClicked();

                if (response.d != null)
                {
                    _navigationService.NavigateTo(App.VATDeregistrationSuccessPage, response);

                }
            }
            catch (GAZTUnlockAccountException ex)
            {
                
                
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                    _navigationService.GoBack();
                });
            }
            catch (Exception )
            {
            }
        }
        public void EnableReasonView()
        {
            CurrentStep = ProcessStep.Step1;

            if (OutletDecisionOptions != null)
            {
                CurrentStep = ProcessStep.Step1;

                SelectedOutletOption = OutletDecisionOptions[
                        VATDeRegistrationDetailsData != null &&
                        VATDeRegistrationDetailsData.d != null &&
                        VATDeRegistrationDetailsData.d.headerSet.Reqtp == "S" ? 1 : 0
                    ];
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
            }
            catch (Exception ex)
            {
                
                
            }

        }

        public async void EnableAttachmentsView()
        {
            if (ReasonTitle != string.Empty)
            {
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
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

                
            }
            updateattachmentList();
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
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

                }
                if (VATDeRegistrationDetailsData.d.headerSet.Declareflg)
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
            if (IDType == AppResources.NationaID || IDType == AppResources.ZZIqamaID)
            {
                if (DOB == string.Empty)
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseentertheBirthDate));

                    
                }
                else
                {
                    CurrentStep = ProcessStep.Step4;

                    IsReasonViewEnabled = false;
                    IsOutletViewEnabled = false;
                    IsAttachmentsViewEnabled = false;
                    IsDeclarationViewEnabled = false;
                    PopulateSummaryReasonData();
                    IsSummaryViewEnabled = true;
                }
            }
            else if (string.IsNullOrEmpty(IDType))
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZPleaseselectparametertype));
            }
            else if (TxtIDNumber == string.Empty)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseenteravalidID));

                
            }
            else if (ContactPersonName == string.Empty)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseentertheName));

               
            }
            else if (IsDeclarationChecked == false)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));
            }
            else
            {
                CurrentStep = ProcessStep.Step4;

                IsReasonViewEnabled = false;
                IsOutletViewEnabled = false;
                IsAttachmentsViewEnabled = false;
                IsDeclarationViewEnabled = false;
                PopulateSummaryReasonData();
                IsSummaryViewEnabled = true;
            }

        }


        #region Attachments View
        public void PopulateAttachmentsListViewTemplate()
        {
            try
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
                                string attachmentCategory = AttachmentTypes.Where(x => (x.DmsTp == VatAttachmentsList[i].Dotyp)).FirstOrDefault().Txt50;

                                check.Add(new VATDeregistrationAttachmentsModel
                                {
                                    FieldTitle = AppResources.VatDeregDocumentTitle,
                                    FieldSubTitle = AppResources.TinDeregistration20MB,
                                    AttachmentName = attachmentCategory,
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
                        catch (Exception )
                        {
                        }

                    }
                    AttachmentsListViewData = new ObservableCollection<VATDeregistrationAttachmentsModel>(check);
                    // OnPropertyChanged("AttachmentsListViewData");

                }
            }
            catch (Exception )
            {
            }
        }
        #endregion

        #region Summary View
        private string _deregReason;
        public string DeregReason
        {
            get => _deregReason; set
            {
                _deregReason = value;
                OnPropertyChanged("DeregReason");
            }
        }
        private string _deregRequestType;
        public string DeregRequestType
        {
            get => _deregRequestType; set
            {
                _deregRequestType = value;
                OnPropertyChanged("DeregRequestType");
            }
        }
        public void PopulateSummaryReasonData()
        {
            List<VATDeregistrationSummaryModel> check = new List<VATDeregistrationSummaryModel>();
            try
            {
                DeregRequestType = JsonConvert.DeserializeObject<List<string>>(DeregRequestTypeSerialised)[0];
                DeregReason = !string.IsNullOrEmpty(OtherField) ? OtherField : ReasonTitle;

                // VATDeregistrationSummaryReasonData = new List<VATDeregistrationSummaryModel>(check);
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("", ex.Message, "ok");
                
                
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
                    filetypes = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetAttachmentTypeStringForAll();
                    filetypes = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetAttachmentTypeStringForAll();
                    PickOptions options = UtilityManager.GetFilePickerOptionsForChooser(filetypes);
                   

                    var fileData = await FilePicker.PickAsync(options);
                    var stream = await fileData.OpenReadAsync();
                    var attachment = UtilityManager.ReadFully(stream as Stream);
                    if (fileData != null && attachment != null && attachment.Length > 0)
                    {
                        AttachmentName = fileData.FileName;
                        FileName = fileData.FileName;

                        if (fileData.FileName.Contains("."))
                        {
                            string[] ExtensionArray = fileData.FileName.Split('.');
                            string Extention = ExtensionArray.Last();
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
                                            foreach (Attachment ItemA in VATDeRegistrationDetailsForAttach.d.AttdetSet)
                                            {
                                                if ((AttachmentName == ItemA.Filename) && (ItemA.Dotyp == SelectedDocumentOption.DmsTp))
                                                // if(AttachmentName == ItemA.Filename)
                                                {
                                                    IsAttachmentPresent = true;
                                                }
                                            }
                                            if (IsAttachmentPresent == false)
                                            {
                                                string attachmentType = UtilityManager.GetContentType(Extention);
                                                AttachmentRootOject _attachment = await SaveAttachment(stream, attachmentType, SelectedDocumentOption.DmsTp);


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
                                                    VATDeRegistrationDetailsForAttach.d.AttdetSet.Add(_attachment.d);
                                                    ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(VATDeRegistrationDetailsForAttach.d.AttdetSet as List<Attachment>);
                                                    MainThread.BeginInvokeOnMainThread(() =>
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
                                    MainThread.BeginInvokeOnMainThread(() =>
                                    {
                                        _dialogService.ShowMessage(AppResources.ZTotalFilesizeshouldnotbemorethan300MB, AppResources.Information);
                                    });
                                }
                            }
                            else
                            {
                                AttachmentName = string.Empty;
                                await Task.Run(() =>
                                {
                                    IsLoading = false;
                                });
                                MainThread.BeginInvokeOnMainThread(() =>
                                {
                                    _dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
                                });
                            }
                        }
                        else
                        {
                            AttachmentName = string.Empty;
                            await Task.Run(() =>
                            {
                                IsLoading = false;
                            });
                            MainThread.BeginInvokeOnMainThread(() =>
                            {
                                _dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
                            });
                        }
                    }
                }
                catch (InternetException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                       await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    });
                }
            }
            catch (Exception )
            {
            }
        }
        private async Task<AttachmentRootOject> SaveAttachment(Stream attachmentByteData, string contentType, string Doctype)
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
                    AttachmentRootOject attachment = await VATDeregistrationWebServiceManager.GAZTSaveVATDeregAttachment(attachmentByteData, AttachmentName, VATDeRegistrationDetailsForAttach.d.headerSet.ReturnIdx, Doctype, contentType);

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
                catch (Exception )
                {
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
                if (VATDeRegistrationDetailsForAttach != null && VATDeRegistrationDetailsForAttach.d != null && VATDeRegistrationDetailsForAttach.d.AttdetSet != null && VATDeRegistrationDetailsForAttach.d.AttdetSet.Count != 0)
                {
                    List<Attachment> attachmentsList = new List<Attachment>();
                    foreach (var item in VatAttachmentsList)
                    {
                        attachmentsList.Add(item);
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
                if (VATDeRegistrationDetailsForAttach != null && VATDeRegistrationDetailsForAttach.d.AttdetSet != null && VATDeRegistrationDetailsForAttach.d.AttdetSet != null)
                    if (VATDeRegistrationDetailsForAttach.d.AttdetSet.Count != 0)
                    {
                        ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(VATDeRegistrationDetailsForAttach.d.AttdetSet);
                        VatAttachmentsList = myCollection;
                    }
            }
        }

        public async void setDATA(string operation)
        {
            try
            {
                string reqType = string.Empty;
                string requestTyp = string.Empty;
                var str = JsonConvert.DeserializeObject<List<string>>(DeregRequestTypeSerialised)[0];
                if ((SelectedOutletOption != null ? SelectedOutletOption.ActiveOutletDecisionOptions : str) == AppResources.VATDeregistrationReasonType1)
                {
                    requestTyp = "D";
                }
                else
                {
                    requestTyp = "S";
                    VATDeRegistrationDetailsData.d.headerSet.StartDate = ConvertDateFormat(FromDate);
                    VATDeRegistrationDetailsData.d.headerSet.EndDate = ConvertDateFormat(ToDate);
                    VATDeRegistrationDetailsData.d.headerSet.SuspDtfrom = ConvertDateFormat(SuspendedStartDate);
                    VATDeRegistrationDetailsData.d.headerSet.SuspDtto = ConvertDateFormat(SuspendedEndDate);
                    VATDeRegistrationDetailsData.d.headerSet.NextDtfrom = ConvertDateFormat(NextFilingStartDate);
                    VATDeRegistrationDetailsData.d.headerSet.NextDtto = ConvertDateFormat(NextFilingEndDate);
                    if (!string.IsNullOrEmpty(NextFilingDueDate))
                    {
                        VATDeRegistrationDetailsData.d.headerSet.Duedate = ConvertDateFormat(DateTime.Parse(NextFilingDueDate));
                    }
                }

                if (IsDeclarationViewEnabled || IsSummaryViewEnabled)
                {
                    VATDeRegistrationDetailsData.d.headerSet.StepNumber = "03";
                    VATDeRegistrationDetailsData.d.headerSet.StepNumberx = "03";
                }
                else
                {
                    VATDeRegistrationDetailsData.d.headerSet.StepNumber = "02";
                    VATDeRegistrationDetailsData.d.headerSet.StepNumberx = "02";
                }

                for (int i = 0; i < reasonList.d.results.Count; i++)
                {
                    if (reasonList.d.results[i].Rdesc == ReasonTitle)
                    {
                        VATDeRegistrationDetailsData.d.headerSet.Reason = reasonList.d.results[i].Reason;

                    }
                }
                VATDeRegistrationDetailsData.d.headerSet.Agreeflg = true;
                VATDeRegistrationDetailsData.d.headerSet.Atype = "Individual";
                if (IsDeclarationChecked)
                {
                    VATDeRegistrationDetailsData.d.headerSet.Declareflg = true;
                }
                else
                {
                    VATDeRegistrationDetailsData.d.headerSet.Declareflg = false;
                }
                VATDeRegistrationDetailsData.d.headerSet.Operationx = operation;
                if (operation == "05")
                {
                    reqType = "VT_DREG";
                    VATDeRegistrationDetailsData.d.headerSet.Fbustx = "E0013";
                    VATDeRegistrationDetailsData.d.headerSet.Statusx = "E0013";
                }
                else if (operation == "01")
                {
                    reqType = "";
                    VATDeRegistrationDetailsData.d.headerSet.Fbustx = "";// ""
                    VATDeRegistrationDetailsData.d.headerSet.Statusx = "E0001";//portal E0001
                }
                VATDeRegistrationDetailsData.d.headerSet.Reqtp = requestTyp;// D in portal
                VATDeRegistrationDetailsData.d.headerSet.TxnTpx = reqType;
                //Step3

                //Step 4

                if (IDType == AppResources.NationaID)
                {
                    VATDeRegistrationDetailsData.d.headerSet.Type = "ZS0001";

                }
                else if (IDType == AppResources.ZZIqamaID)
                {
                    VATDeRegistrationDetailsData.d.headerSet.Type = "ZS0002";

                }
                else if (IDType == AppResources.ZZGCCID)
                {
                    VATDeRegistrationDetailsData.d.headerSet.Type = "ZS0003";

                }
                VATDeRegistrationDetailsData.d.headerSet.Idnumbr = TxtIDNumber;
                VATDeRegistrationDetailsData.d.headerSet.Contactnm = ContactPersonName;
                //VATDeRegistrationDetailsData.d.Taxdt = DOB;

                int noteNum = 0;

                try
                {
                    if (VATDeRegistrationDetailsData.d.NotesSet != null && VATDeRegistrationDetailsData.d.NotesSet != null
                        && VATDeRegistrationDetailsData.d.NotesSet.Count() > 0)
                    {
                        VATDeRegistrationDetailsData.d.NotesSet[0].Tdline = string.Empty;
                        VATDeRegistrationDetailsData.d.NotesSet[0].Strline = string.Empty;
                    }
                }
                catch (Exception )
                {
                }
                VATDeregNote vATDeregNote = new VATDeregNote();

                Metadata2 _metdata = new Metadata2();

                _metdata.uri = ZATCAConstants.GAZTVATDeregNotesSet;
                _metdata.type = "ZDP_VAT_NW_DREG_SRV.Notes";
                _metdata.id = ZATCAConstants.GAZTVATDeregNotesSet;
                vATDeregNote.__metadata = _metdata;
                vATDeregNote.AttByz = "TP";
                vATDeregNote.ElemNo = 0;
                vATDeregNote.ByPusrz = "";
                vATDeregNote.Erfdtz = null;
                vATDeregNote.Erftmz = "PT00H00M00S";
                vATDeregNote.Erfusrz = "";
                vATDeregNote.Lineno = 1;
                vATDeregNote.Rcodez = "DGVT_OTH";
                vATDeregNote.Refnamez = "";
                vATDeregNote.Tdformat = "";
                vATDeregNote.XInvoicez = "";
                vATDeregNote.XObsoletez = "";
                vATDeregNote.DataVersionz = "00000";
                vATDeregNote.ByGpartz = App.LoginDataRetrieved.TIN;
                vATDeregNote.Namez = "";
                vATDeregNote.ElemNo = 0;
                vATDeregNote.Sect = "";
                vATDeregNote.Strdt = "";
                vATDeregNote.Strtime = "";
                vATDeregNote.Strline = "";

                VATDeRegistrationDetailsData.d.NotesSet?.Clear();
                if (ReasonTitle.Contains(AppResources.VatDeregistrationofReturnReason4))
                {
                    if (!string.IsNullOrEmpty(OtherField))
                    {
                        noteNum++;

                        vATDeregNote.Noteno = noteNum.ToString();
                        vATDeregNote.Notenoz = noteNum.ToString();
                        vATDeregNote.Tdline = OtherField;
                        VATDeRegistrationDetailsData.d.NotesSet.Add(vATDeregNote);
                    }
                }
                VATDeRegistrationDetailsData.d.AttdetSet?.Clear();
                VATDeRegistrationDetailsData.d.QuesListSet?.Clear();
                VATDeRegistrationDetailsData.d.AddressSet?.Clear();
                //foreach (Attachment attachemnt in VatAttachmentsList)
                //{
                //    if (!VATDeRegistrationDetailsData.d.AttdetSet.results.Contains(attachemnt))
                //        VATDeRegistrationDetailsData.d.AttdetSet.results.Add(attachemnt);
                //}

            }
            catch (Exception )
            {
            }
        }


        public String ConvertDateFormat(DateTime newDate)
        {
            DateTime dateTime = Convert.ToDateTime(newDate);

            string ConvertedDate = string.Empty;
            TimeSpan span = (newDate - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
            string unixTime = span.TotalSeconds.ToString("N0");
            unixTime = unixTime.Replace(",", "");
            ConvertedDate = "" + "/Date(" + unixTime + ")/";

            double unixTimestamp = ((double)(dateTime.Subtract(new DateTime(1970, 1, 1))).TotalSeconds);

            unixTimestamp = unixTimestamp * 1000;
            if (unixTimestamp.ToString().Contains("."))
                unixTimestamp = double.Parse(unixTimestamp.ToString().Split('.')[0]);
            ConvertedDate = "" + "/Date(" + unixTimestamp + ")/";

            return ConvertedDate;
        }
        public async Task<VATDeRegistrationDetails> saveAsDraftVoidAPIMethodCall()
        {
            VATDeRegistrationDetails response = new VATDeRegistrationDetails();
            try
            {
                IsLoading = true;
                VATDeRegistrationDetails vATDeRegistrationDetails = new VATDeRegistrationDetails();
                VATDeRegistrationDetailsData.d.headerSet.Gpartx = VATDeRegistrationDetailsData.d.headerSet.Gpart;
                if (!string.IsNullOrEmpty(VATDeRegistrationDetailsData.d.headerSet.Taxdt))
                {
                    DateTime date = DateTime.Parse(VATDeRegistrationDetailsData.d.headerSet.Taxdt);
                    VATDeRegistrationDetailsData.d.headerSet.Taxdt = date.ToString("yyyy-MM-ddTHH:mm:ss");
                }
                response = await VatRegistrationWebServiceManager.SaveVATDeRegistrationData(VATDeRegistrationDetailsData);
                if (response != null && response.d != null)
                {
                    try
                    {
                        if (response != null && response.d != null)
                        {
                            if (response.d.headerSet.Operationx.Equals("04"))
                            {
                                string number = response.d.headerSet.Fbnumx;
                                string displayMessage = AppResources.VATRSuccessFullVoidMessage + " " + number;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(displayMessage));

                                _navigationService.GoBack();
                            }
                            else if (response.d.headerSet.Operationx.Equals("05"))
                            {
                                if (shouldShowDialog==true)
                                {
                                    string displayMessage = AppResources.VATRSaveasdraftMessage;
                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(displayMessage));
                                    shouldShowDialog = false;
                                }

                            }

                            VATDeRegistrationDetailsData = response;

                        }
                        IsLoading = false;
                        return response;
                    }
                    catch (Exception )
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
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                });
                return response;
            }

            catch (Exception )
            {
                return response;
            }
        }
        public async Task<VATDeRegistrationDetails> SubmitClicked()
        {
            VATDeRegistrationDetails response = new VATDeRegistrationDetails();
            try
            {
                
                IsLoading = true;
                setDATA("01");
                VATDeRegistrationDetails vATDeRegistrationDetails = new VATDeRegistrationDetails();

                VATDeRegistrationDetailsData.d.AttdetSet = new List<Attachment>();
                VATDeRegistrationDetailsData.d.headerSet.Gpartx = VATDeRegistrationDetailsData.d.headerSet.Gpart;
                if (!string.IsNullOrEmpty(VATDeRegistrationDetailsData.d.headerSet.Taxdt))
                {
                    DateTime date = DateTime.Parse(VATDeRegistrationDetailsData.d.headerSet.Taxdt);
                    VATDeRegistrationDetailsData.d.headerSet.Taxdt = date.ToString("yyyy-MM-ddTHH:mm:ss");
                }
                response = await VatRegistrationWebServiceManager.SaveVATDeRegistrationData(VATDeRegistrationDetailsData);

                // PopToRootPage();
                if (response != null && response.d != null)
                {
                    try
                    {
                        if (response != null && response.d != null)
                        {
                            if (response.d.headerSet.Operationx.Equals("04"))
                            {
                                string number = response.d.headerSet.Fbnumx;
                                string displayMessage = AppResources.VATRSuccessFullVoidMessage + " " + number;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(displayMessage));

                                //await _dialogService.ShowMessage(displayMessage, AppResources.Information);
                                _navigationService.GoBack();
                            }

                            if (response.d.headerSet.Operationx.Equals("05"))
                            {
                                if (shouldShowDialog == true)
                                {
                                    //  string number = response.d.Fbnumz;
                                    string displayMessage = AppResources.VATRSaveasdraftMessage;
                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(displayMessage));
                                    shouldShowDialog = false;
                                }

                                // await _dialogService.ShowMessage(displayMessage, AppResources.Information);
                            }

                            VATDeRegistrationDetailsData = response;


                        }

                        IsLoading = false;
                        return response;
                    }
                    catch (Exception )
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
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                });
                return response;
            }

            catch (Exception )
            {
                return response;
            }
        }
        

        public void SetTextCount(int length)
        {
            TextCount = length + "/" + "1000";
        }

        #endregion
    }
}
