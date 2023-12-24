using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using RGPopup.Maui.Services;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Windows.Input;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Views.SyncFusionEnabledViews.AddPopPages;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATReturnsPageEX
{

    public class VATReturnsPageViewModelEX : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand GoBackClick { get; set; }
        public ICommand OnStepButtonClicked { get; set; }
        public ICommand onInstructionsClicked { get; set; }
        public ICommand onTaxPayerDetailsClicked { get; set; }
        public ICommand onVATReturnFormClicked { get; set; }
        public ICommand onSummaryClicked { get; set; }
        public ICommand OnSaveAsDraftClicked { get; set; }
        public ICommand onOptionClicked { get; set; }
        public ICommand OnAttachmentClick { get; set; }
        public ICommand OnVATRefreshButtonClicked { get; set; }
        public ICommand OnDownloadAcknowlwdgementClicked { get; set; }
        public ICommand OnAcknowlwdgementClicked { get; set; }
        public ICommand onFaqSectionClicked { get; set; }
        public ICommand ChangeRegistrationClicked { get; set; }
        // bool IsFirstSubmission = true;
        byte[] attachment;
        public ICommand onCreditCarriedForwardClicked { get; set; }
        public ICommand onStandardRatedSalesVatAmountTapped { get; set; }
        public ICommand OnGetAcknowledgementLinkClicked { get; set; }

        public static bool IsFirstTimeForNote = false;
        #endregion
        #region Property
        private bool _isPrevReturn = false;
        public bool IsPrevReturn
        {
            get
            {
                return _isPrevReturn;
            }
            set
            {
                if (_isPrevReturn == value) return;
                _isPrevReturn = value;
                RaisePropertyChanged("IsPrevReturn");
            }
        }

        private bool _isNewReturn = false;
        public bool IsNewReturn
        {
            get
            {
                return _isNewReturn;
            }
            set
            {
                if (_isNewReturn == value) return;

                _isNewReturn = value;
                RaisePropertyChanged("IsNewReturn");
            }
        }


        private bool _isFifteenPercentChange = false;
        public bool IsFifteenPercentChange
        {
            get
            {
                return _isFifteenPercentChange;
            }
            set
            {
                if (_isFifteenPercentChange == value) return;

                _isFifteenPercentChange = value;
                RaisePropertyChanged("IsFifteenPercentChange");
            }
        }


        private bool _isFivePersenctVisible = false;
        public bool IsFivePersenctVisible
        {
            get
            {
                return _isFivePersenctVisible;
            }
            set
            {
                if (_isFivePersenctVisible == value) return;

                _isFivePersenctVisible = value;
                RaisePropertyChanged("IsFivePersenctVisible");
            }
        }

        private bool _isFifteenPersenctVisible = false;
        public bool IsFifteenPersenctVisible
        {
            get
            {
                return _isFifteenPersenctVisible;
            }
            set
            {
                if (_isFifteenPersenctVisible == value) return;

                _isFifteenPersenctVisible = value;
                RaisePropertyChanged("IsFifteenPersenctVisible");
            }
        }


        private bool _isEnableToggledFor15PercentChange = false;
        public bool IsEnableSwitchToggledFor15PercentChange
        {
            get
            {
                return _isEnableToggledFor15PercentChange;
            }
            set
            {
                _isEnableToggledFor15PercentChange = value;
                RaisePropertyChanged("IsEnableSwitchToggledFor15PercentChange");
            }
        }

        private bool _isSwitchToggledFor15PercentChange = false;
        public bool IsSwitchToggledFor15PercentChange
        {
            get
            {
                return _isSwitchToggledFor15PercentChange;
            }
            set
            {
                if (_isSwitchToggledFor15PercentChange == value) return;

                _isSwitchToggledFor15PercentChange = value;

                if (_isSwitchToggledFor15PercentChange == true)
                {
                    IsYesChecked = true;
                    IsNoChecked = false;
                }
                else
                {
                    IsNoChecked = true;
                    IsYesChecked = false;
                }

                RaisePropertyChanged("IsSwitchToggledFor15PercentChange");
            }
        }


        private bool _isYesChecked = false;
        public bool IsYesChecked
        {
            get
            {
                return _isYesChecked;
            }
            set
            {
                if (_isYesChecked == value) return;

                _isYesChecked = value;
                RaisePropertyChanged("IsYesChecked");
            }
        }

        private bool _isNoChecked = true;
        public bool IsNoChecked
        {
            get
            {
                return _isNoChecked;
            }
            set
            {
                if (_isNoChecked == value) return;

                _isNoChecked = value;
                if (_isNoChecked == true)
                {
                    IsFifteenPersenctVisible = true;
                    IsFivePersenctVisible = false;
                }
                else
                {
                    IsFifteenPersenctVisible = true;
                    IsFivePersenctVisible = true;
                }
                RaisePropertyChanged("IsNoChecked");
            }
        }

        private FlowDirection _fDirection = FlowDirection.RightToLeft;
        public FlowDirection FDirection
        {
            get
            {
                return _fDirection;
            }
            set
            {
                if (_fDirection == value) return;

                _fDirection = value;
                RaisePropertyChanged("FDirection");
            }
        }
        private int _firstSubmissionCount = 0;
        public int FirstSubmissionCount
        {
            get
            {
                return _firstSubmissionCount;
            }
            set
            {
                if (_firstSubmissionCount == value) return;

                _firstSubmissionCount = value;
                RaisePropertyChanged("FirstSubmissionCount");
            }
        }

        private int _selectedIndex = 0;
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                if (_selectedIndex == value) return;

                _selectedIndex = value;
                RaisePropertyChanged("SelectedIndex");
            }
        }
        private VATDeclaration _responseVatDeclaration;
        public VATDeclaration ResponseVatDeclaration
        {
            get
            {
                return _responseVatDeclaration;
            }
            set
            {
                if (_responseVatDeclaration == value) return;

                _responseVatDeclaration = value;
                RaisePropertyChanged("ResponseVatDeclaration");
            }
        }
        private string _stepNumber;
        public string StepNumber
        {
            get
            {
                return _stepNumber;
            }
            set
            {
                if (_stepNumber == value) return;
                _stepNumber = value;
                RaisePropertyChanged("StepNumber");
            }
        }
        private string _stepNumberz;
        public string StepNumberz
        {
            get
            {
                return _stepNumberz;
            }
            set
            {
                if (_stepNumberz == value) return;
                _stepNumberz = value;
                RaisePropertyChanged("StepNumberz");
            }
        }
        private bool _isLoading = false;
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
        private bool _isFirstSubmission = true;
        public bool IsFirstSubmission
        {
            get
            {
                return _isFirstSubmission;
            }
            set
            {
                if (_isFirstSubmission == value) return;

                _isFirstSubmission = value;
                RaisePropertyChanged("IsFirstSubmission");
            }
        }

        private bool _isRefundNoMsgDisplayed = false;
        public bool IsRefundNoMsgDisplayed
        {
            get
            {
                return _isRefundNoMsgDisplayed;
            }
            set
            {
                if (_isRefundNoMsgDisplayed == value) return;

                _isRefundNoMsgDisplayed = value;
                RaisePropertyChanged("IsRefundNoMsgDisplayed");
            }
        }

        private bool _isRefundYesMsgDisplayed = false;
        public bool IsRefundYesMsgDisplayed
        {
            get
            {
                return _isRefundYesMsgDisplayed;
            }
            set
            {
                if (_isRefundYesMsgDisplayed == value) return;

                _isRefundYesMsgDisplayed = value;
                RaisePropertyChanged("IsRefundYesMsgDisplayed");
            }
        }

        private bool _isSwitchToggled = false;
        public bool IsSwitchToggled
        {
            get
            {
                return _isSwitchToggled;
            }
            set
            {
                if (_isSwitchToggled == value) return;

                _isSwitchToggled = value;
                try
                {
                    if (string.IsNullOrEmpty(Preperiodcorr) && IsSwitchToggled)
                    {
                        IsSwitchToggled = false;
                    }
                    if (IsSwitchToggled)
                    {
                        if (!string.IsNullOrEmpty(Preperiodcorr) && !Preperiodcorr.Contains("-"))
                        {
                            if (App.IsArabic)
                            {
                                //  Preperiodcorr =  Preperiodcorr + "-";// d.ToString();
                                Preperiodcorr = "-" + Preperiodcorr;// d.ToString();
                            }
                            else
                            {
                                Preperiodcorr = "-" + Preperiodcorr;// d.ToString();
                            }
                        }
                    }
                    else
                    {
                        if (Preperiodcorr != null && Preperiodcorr.Contains("-") && !string.IsNullOrEmpty(Preperiodcorr))
                            Preperiodcorr = Preperiodcorr.Replace("-", "");
                    }
                }
                catch (Exception)
                {


                }
                RaisePropertyChanged("IsSwitchToggled");
            }
        }
        private bool _isSwitchVisible = false;
        public bool IsSwitchVisible
        {
            get
            {
                return _isSwitchVisible;
            }
            set
            {
                if (_isSwitchVisible == value) return;

                _isSwitchVisible = value;
                RaisePropertyChanged("IsSwitchVisible");
            }
        }
        private bool _isDeclarationChecked = false;
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
                RaisePropertyChanged("IsDeclarationChecked");
            }
        }
        private VATDeclarationD _vATDeclarationD;
        public VATDeclarationD VATDeclarationD
        {
            get
            {
                return _vATDeclarationD;
            }
            set
            {
                if (_vATDeclarationD == value) return;

                _vATDeclarationD = value;
                RaisePropertyChanged("VATDeclarationD");
            }
        }
        private VATDeclaration _vATDeclarationData;
        public VATDeclaration VATDeclarationData
        {
            get
            {
                return _vATDeclarationData;
            }
            set
            {
                if (_vATDeclarationData == value) return;

                _vATDeclarationData = value;
                RaisePropertyChanged("VATDeclarationData");
            }
        }
        private List<Attachment> _aTTACHSetsList;
        public List<Attachment> ATTACHSetsList
        {
            get
            {
                return _aTTACHSetsList;
            }
            set
            {
                if (_aTTACHSetsList == value) return;

                _aTTACHSetsList = value;
                RaisePropertyChanged("ATTACHSetsList");
            }
        }
        private List<Attachment> _dummyaTTACHSetsList;
        public List<Attachment> DummyATTACHSetsList
        {
            get
            {
                return _dummyaTTACHSetsList;
            }
            set
            {
                if (_dummyaTTACHSetsList == value) return;

                _dummyaTTACHSetsList = value;
                RaisePropertyChanged("DummyATTACHSetsList");
            }
        }
        private List<Result3> _creditCarriedsList;
        public List<Result3> CreditCarriedsList
        {
            get
            {
                return _creditCarriedsList;
            }
            set
            {
                if (_creditCarriedsList == value) return;

                _creditCarriedsList = value;
                RaisePropertyChanged("CreditCarriedsList");
            }
        }
        private List<VATDeclarationTabbedPageName> _vatTabbledPageList;
        public List<VATDeclarationTabbedPageName> VatTabbledPageList
        {
            get
            {
                return _vatTabbledPageList;
            }
            set
            {
                if (_vatTabbledPageList == value) return;

                _vatTabbledPageList = value;
                RaisePropertyChanged("VatTabbledPageList");
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
                if (_vatAttachmentsList == value) return;

                _vatAttachmentsList = value;
                RaisePropertyChanged("VatAttachmentsList");
            }
        }
        private bool _isVisibleOptionMenu = false;
        public bool IsVisibleOptionMenu
        {
            get
            {
                return _isVisibleOptionMenu;
            }
            set
            {
                if (_isVisibleOptionMenu == value) return;

                _isVisibleOptionMenu = value;
                RaisePropertyChanged("IsVisibleOptionMenu");
            }
        }
        private bool _isVisibleVatReturnForm = false;
        public bool IsVisibleVatReturnForm
        {
            get
            {
                return _isVisibleVatReturnForm;
            }
            set
            {
                if (_isVisibleVatReturnForm == value) return;

                _isVisibleVatReturnForm = value;
                RaisePropertyChanged("IsVisibleVatReturnForm");
            }
        }
        private bool _isVisibleTaxPayerDetails = false;
        public bool IsVisibleTaxPayerDetails
        {
            get
            {
                return _isVisibleTaxPayerDetails;
            }
            set
            {
                if (_isVisibleTaxPayerDetails == value) return;

                _isVisibleTaxPayerDetails = value;
                RaisePropertyChanged("IsVisibleTaxPayerDetails");
            }
        }
        private bool _isVisibleCreditCarriedForward = false;
        public bool IsVisibleCreditCarriedForward
        {
            get
            {
                return _isVisibleCreditCarriedForward;
            }
            set
            {
                if (_isVisibleCreditCarriedForward == value) return;

                _isVisibleCreditCarriedForward = value;
                RaisePropertyChanged("IsVisibleCreditCarriedForward");
            }
        }
        private bool _isVisibleSummary = false;
        public bool IsVisibleSummary
        {
            get
            {
                return _isVisibleSummary;
            }
            set
            {
                if (_isVisibleSummary == value) return;

                _isVisibleSummary = value;
                RaisePropertyChanged("IsVisibleSummary");
            }
        }
        private bool _isVisibleAttachments = false;
        public bool IsVisibleAttachments
        {
            get
            {
                return _isVisibleAttachments;
            }
            set
            {
                if (_isVisibleAttachments == value) return;

                _isVisibleAttachments = value;
                RaisePropertyChanged("IsVisibleAttachments");
            }
        }
        private bool _isVisibleAcknowledgment = false;
        public bool IsVisibleAcknowledgment
        {
            get
            {
                return _isVisibleAcknowledgment;
            }
            set
            {
                if (_isVisibleAcknowledgment == value) return;

                _isVisibleAcknowledgment = value;
                RaisePropertyChanged("IsVisibleAcknowledgment");
            }
        }
        private bool _isCheckedTaxPayerDetailsInfo = false;
        public bool IsCheckedTaxPayerDetailsInfo
        {
            get
            {
                return _isCheckedTaxPayerDetailsInfo;
            }
            set
            {
                if (_isCheckedTaxPayerDetailsInfo == value) return;

                _isCheckedTaxPayerDetailsInfo = value;
                if (_isCheckedTaxPayerDetailsInfo == true)
                {
                    if ((App.ICRStatus == "E0045" || App.ICRStatus == "E0006") && IsAmendClicked == false || App.ICRStatus == "E0055" || App.ICRStatus == "E0058")
                    {
                        IsMainButtonEnabled = false;
                    }
                    else
                    {
                        IsMainButtonEnabled = true;
                    }
                    VATDeclarationData.d.ConfStp2 = "1";
                }
                else
                {
                    IsMainButtonEnabled = false;
                    VATDeclarationData.d.ConfStp2 = "0";
                }
                RaisePropertyChanged("IsCheckedTaxPayerDetailsInfo");
            }
        }
        private bool _isGetAcknowledgementClicked = false;
        public bool IsGetAcknowledgementClicked
        {
            get
            {
                return _isGetAcknowledgementClicked;
            }
            set
            {
                if (_isGetAcknowledgementClicked == value) return;

                _isGetAcknowledgementClicked = value;
                RaisePropertyChanged("IsGetAcknowledgementClicked");
            }
        }
        private bool _isVATReturnFieldCheckForSaveAsDraft = false;
        public bool IsVATReturnFieldCheckForSaveAsDraft
        {
            get
            {
                return _isVATReturnFieldCheckForSaveAsDraft;
            }
            set
            {
                if (_isVATReturnFieldCheckForSaveAsDraft == value) return;

                _isVATReturnFieldCheckForSaveAsDraft = value;
                RaisePropertyChanged("IsVATReturnFieldCheckForSaveAsDraft");
            }
        }
        private bool _isChangeRegistrationlinkVisible;
        public bool IsChangeRegistrationlinkVisible
        {
            get
            {
                return _isChangeRegistrationlinkVisible;
            }
            set
            {
                if (_isChangeRegistrationlinkVisible == value) return;

                _isChangeRegistrationlinkVisible = value;
                RaisePropertyChanged("IsChangeRegistrationlinkVisible");
            }
        }
        private bool _isDeclarationCheckedForSummary = false;
        public bool IsDeclarationCheckedForSummary
        {
            get
            {
                return _isDeclarationCheckedForSummary;
            }
            set
            {
                if (_isDeclarationCheckedForSummary == value) return;

                _isDeclarationCheckedForSummary = value;
                if (_isDeclarationCheckedForSummary == true)
                {
                    if ((App.ICRStatus == "E0045" || App.ICRStatus == "E0006") && IsAmendClicked == false || App.ICRStatus == "E0055")
                    {
                        IsMainButtonEnabled = false;
                    }
                    else
                    {
                        IsMainButtonEnabled = true;
                    }
                    VATDeclarationData.d.DecFg = "1";
                }
                else
                {
                    IsMainButtonEnabled = false;
                }
                RaisePropertyChanged("IsDeclarationCheckedForSummary");
            }
        }
        private bool _ischkRefundDeclaration = false;
        public bool IschkRefundDeclaration
        {
            get
            {
                return _ischkRefundDeclaration;
            }
            set
            {
                if (_ischkRefundDeclaration == value) return;

                _ischkRefundDeclaration = value;
                RaisePropertyChanged("IschkRefundDeclaration");
            }
        }
        private bool _isDeclarationCheckedForInstruction = false;
        public bool IsDeclarationCheckedForInstruction
        {
            get
            {
                return _isDeclarationCheckedForInstruction;
            }
            set
            {
                if (_isDeclarationCheckedForInstruction == value) return;

                _isDeclarationCheckedForInstruction = value;
                if (_isDeclarationCheckedForInstruction == true)
                {
                    if ((App.ICRStatus == "E0045" || App.ICRStatus == "E0006") && IsAmendClicked == false || App.ICRStatus == "E0055")
                    {
                        IsMainButtonEnabled = false;
                    }
                    else
                    {
                        IsMainButtonEnabled = true;
                    }
                    VATDeclarationData.d.TcFg = "1";
                }
                else
                {
                    IsMainButtonEnabled = false;
                    VATDeclarationData.d.TcFg = "0";
                }
                RaisePropertyChanged("IsDeclarationCheckedForInstruction");
            }
        }
        private bool _onMoreOptionsEnabled = true;
        public bool OnMoreOptionsEnabled
        {
            get
            {
                return _onMoreOptionsEnabled;
            }
            set
            {
                if (_onMoreOptionsEnabled == value) return;

                _onMoreOptionsEnabled = value;
                RaisePropertyChanged("OnMoreOptionsEnabled");
            }
        }
        private bool _isMainButtonEnabled = false;
        public bool IsMainButtonEnabled
        {
            get
            {
                return _isMainButtonEnabled;
            }
            set
            {
                if (_isMainButtonEnabled == value) return;


                _isMainButtonEnabled = value;
                //OnStepButtonClicked.ChangeCanExecute();
                RaisePropertyChanged("IsMainButtonEnabled");
            }
        }
        private bool _isMainButtonVisible = false;
        public bool IsMainButtonVisible
        {
            get
            {
                return _isMainButtonVisible;
            }
            set
            {
                if (_isMainButtonVisible == value) return;

                _isMainButtonVisible = value;
                //OnStepButtonClicked.ChangeCanExecute();
                RaisePropertyChanged("IsMainButtonVisible");
            }
        }
        private bool _isUnFocusedTextBox = false;
        public bool IsUnFocusedTextBox
        {
            get
            {
                return _isUnFocusedTextBox;
            }
            set
            {
                if (_isUnFocusedTextBox == value) return;

                _isUnFocusedTextBox = value;
                RaisePropertyChanged("IsUnFocusedTextBox");
            }
        }
        private bool _isFirstTimeGet = false;
        public bool IsFirstTimeGet
        {
            get
            {
                return _isFirstTimeGet;
            }
            set
            {
                if (_isFirstTimeGet == value) return;

                _isFirstTimeGet = value;
                RaisePropertyChanged("IsFirstTimeGet");
            }
        }
        private bool _isTaxPayerControlEnabled = false;
        public bool IsTaxPayerControlEnabled
        {
            get
            {
                return _isTaxPayerControlEnabled;
            }
            set
            {
                if (_isTaxPayerControlEnabled == value) return;

                _isTaxPayerControlEnabled = value;
                RaisePropertyChanged("IsTaxPayerControlEnabled");
            }
        }
        private bool _isVisibleNotes = false;
        public bool IsVisibleNotes
        {
            get
            {
                return _isVisibleNotes;
            }
            set
            {
                if (_isVisibleNotes == value) return;

                _isVisibleNotes = value;
                RaisePropertyChanged("IsVisibleNotes");
            }
        }
        private bool _isVisibleInstrunction = false;
        public bool IsVisibleInstrunction
        {
            get
            {
                return _isVisibleInstrunction;
            }
            set
            {
                if (_isVisibleInstrunction == value) return;

                _isVisibleInstrunction = value;
                RaisePropertyChanged("IsVisibleInstrunction");
            }
        }
        private bool _isTabbedMenuAvailable = true;
        public bool IsTabbedMenuAvailable
        {
            get
            {
                return _isTabbedMenuAvailable;
            }
            set
            {
                if (_isTabbedMenuAvailable == value) return;

                _isTabbedMenuAvailable = value;
                RaisePropertyChanged("IsTabbedMenuAvailable");
            }
        }
        private bool _isVisibleCreditCarriedLabel = false;
        public bool IsVisibleCreditCarriedLabel
        {
            get
            {
                return _isVisibleCreditCarriedLabel;
            }
            set
            {
                if (_isVisibleCreditCarriedLabel == value) return;

                _isVisibleCreditCarriedLabel = value;
                RaisePropertyChanged("IsVisibleCreditCarriedLabel");
            }
        }
        public static bool IsAmend = false;
        private bool _isAmendClicked = false;
        public bool IsAmendClicked
        {
            get
            {
                return _isAmendClicked;
            }
            set
            {
                if (_isAmendClicked == value) return;

                _isAmendClicked = value;
                if (_isAmendClicked == true)
                {
                    IsGetAcknowledgementClicked = false;
                    IsAmend = true;
                }
                else
                {
                    IsAmend = false;
                }
                RaisePropertyChanged("IsAmendClicked");
            }
        }
        private string _pageFontSize = "10";
        public string PageFontSize
        {
            get
            {
                return _pageFontSize;
            }
            set
            {
                if (_pageFontSize == value) return;

                _pageFontSize = value;
                RaisePropertyChanged("PageFontSize");
            }
        }
        private string _attachmentName = "Attachments";
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
                RaisePropertyChanged("AttachmentName");
            }
        }
        private string _buttonName = AppResources.ZVatStepTwo;
        public string ButtonName
        {
            get
            {
                return _buttonName;
            }
            set
            {
                if (_buttonName == value) return;

                _buttonName = value;
                RaisePropertyChanged("ButtonName");
            }
        }
        private string _noteText = "";
        public string NoteText
        {
            get
            {
                return _noteText;
            }
            set
            {
                if (_noteText == value) return;

                _noteText = value;
                RaisePropertyChanged("NoteText");
            }
        }
        private List<Note> _responseNote;
        public List<Note> ResponseNote
        {
            get
            {
                return _responseNote;
            }
            set
            {
                if (_responseNote == value) return;

                _responseNote = value;
                RaisePropertyChanged("ResponseNote");
            }
        }
        private List<Result2> _responseIBANSET;
        public List<Result2> ResponseIBANSET
        {
            get
            {
                return _responseIBANSET;
            }
            set
            {
                if (_responseIBANSET == value) return;

                _responseIBANSET = value;
                RaisePropertyChanged("ResponseIBANSET");
            }
        }
        private List<Result3> _responseCFSET;
        public List<Result3> ResponseCFSET
        {
            get
            {
                return _responseCFSET;
            }
            set
            {
                if (_responseCFSET == value) return;

                _responseCFSET = value;
                RaisePropertyChanged("ResponseCFSET");
            }
        }
        private List<Attachment> _responseAttachSet;
        public List<Attachment> ResponseAttachSet
        {
            get
            {
                return _responseAttachSet;
            }
            set
            {
                if (_responseAttachSet == value) return;

                _responseAttachSet = value;
                RaisePropertyChanged("ResponseAttachSet");
            }
        }
        private List<Result5> _responseAddressSET;
        public List<Result5> ResponseAddressSET
        {
            get
            {
                return _responseAddressSET;
            }
            set
            {
                if (_responseAddressSET == value) return;

                _responseAddressSET = value;
                RaisePropertyChanged("ResponseResult5");
            }
        }
        private List<object> _responseobject;
        public List<object> Responseobject
        {
            get
            {
                return _responseobject;
            }
            set
            {
                if (_responseobject == value) return;

                _responseobject = value;
                RaisePropertyChanged("Responseobject");
            }
        }
        private VATDeclarationD _responseVATDeclarationD;
        public VATDeclarationD ResponseVATDeclarationD
        {
            get
            {
                return _responseVATDeclarationD;
            }
            set
            {
                if (_responseVATDeclarationD == value) return;

                _responseVATDeclarationD = value;
                RaisePropertyChanged("ResponseVATDeclarationD");
            }
        }
        private VATDeclarationTabbedPageName _pageSelectedItems;
        public VATDeclarationTabbedPageName PageSelectedItems
        {
            get
            {
                return _pageSelectedItems;
            }
            set
            {
                if (_pageSelectedItems == value) return;

                _pageSelectedItems = value;
                RaisePropertyChanged("PageSelectedItems");
            }
        }
        private VATDeclarationTabbedPageName _pageSelectedItem;
        public VATDeclarationTabbedPageName PageSelectedItem
        {
            get
            {
                return _pageSelectedItem;
            }
            set
            {
                if (_pageSelectedItem == value) return;

                _pageSelectedItem = value;
                RaisePropertyChanged("PageSelectedItem");
            }
        }
        private string _vATRate001;
        public string VATRate001
        {
            get
            {
                return _vATRate001;
            }
            set
            {
                if (_vATRate001 == value) return;

                _vATRate001 = value;
                RaisePropertyChanged("VATRate001");
            }
        }
        private string _vATRate002;
        public string VATRate002
        {
            get
            {
                return _vATRate002;
            }
            set
            {
                if (_vATRate002 == value) return;

                _vATRate002 = value;
                RaisePropertyChanged("VATRate002");
            }
        }

        private string _vATRate003 = string.Empty;
        public string VATRate003
        {
            get
            {
                return _vATRate003;
            }
            set
            {
                if (_vATRate003 == value) return;

                _vATRate003 = value;
                RaisePropertyChanged("VATRate003");
            }
        }
        private string _tPName = "";
        public string TPName
        {
            get
            {
                return _tPName;
            }
            set
            {
                if (_tPName == value) return;

                _tPName = value;
                RaisePropertyChanged("TPName");
            }
        }
        private string _returnReferenceNumber = "";
        public string ReturnReferenceNumber
        {
            get
            {
                return _returnReferenceNumber;
            }
            set
            {
                if (_returnReferenceNumber == value) return;

                _returnReferenceNumber = value;
                RaisePropertyChanged("ReturnReferenceNumber");
            }
        }
        private string _taxablePeriod = "";
        public string TaxablePeriod
        {
            get
            {
                return _taxablePeriod;
            }
            set
            {
                if (_taxablePeriod == value) return;

                _taxablePeriod = value;
                RaisePropertyChanged("TaxablePeriod");
            }
        }
        private string _receiptDate = "";
        public string ReceiptDate
        {
            get
            {
                return _receiptDate;
            }
            set
            {
                if (_receiptDate == value) return;

                _receiptDate = value;
                RaisePropertyChanged("ReceiptDate");
            }
        }
        private string _sadadNumber = "";
        public string SadadNumber
        {
            get
            {
                return _sadadNumber;
            }
            set
            {
                if (_sadadNumber == value) return;

                _sadadNumber = value;
                RaisePropertyChanged("SadadNumber");
            }
        }
        private List<VATCalculationDataVATRSet> _calculationRateSet;
        public List<VATCalculationDataVATRSet> CalculationRateSet
        {
            get
            {
                return _calculationRateSet;
            }
            set
            {
                if (_calculationRateSet == value) return;

                _calculationRateSet = value;
                RaisePropertyChanged("CalculationRateSet");
            }
        }
        private string _taxpayerPeriodFromDate;
        public string TaxpayerPeriodFromDate
        {
            get
            {
                return _taxpayerPeriodFromDate;
            }
            set
            {
                if (_taxpayerPeriodFromDate == value) return;

                _taxpayerPeriodFromDate = value;
                RaisePropertyChanged("TaxpayerPeriodFromDate");
            }
        }
        private string _taxpayerPeriodToDate;
        public string TaxpayerPeriodToDate
        {
            get
            {
                return _taxpayerPeriodToDate;
            }
            set
            {
                if (_taxpayerPeriodToDate == value) return;

                _taxpayerPeriodToDate = value;
                RaisePropertyChanged("TaxpayerPeriodToDate");
            }
        }
        private bool _isControlEnabled = false;
        public bool IsControlEnabled
        {
            get
            {
                return _isControlEnabled;
            }
            set
            {
                if (_isControlEnabled == value) return;

                _isControlEnabled = value;
                RaisePropertyChanged("IsControlEnabled");
            }
        }

        private bool _isControlEnabledForEntry = false;
        public bool IsControlEnabledForEntry
        {
            get
            {
                return _isControlEnabledForEntry;
            }
            set
            {
                if (_isControlEnabledForEntry == value) return;

                _isControlEnabledForEntry = value;
                RaisePropertyChanged("IsControlEnabledForEntry");
            }
        }
        private bool _isDeclarationCheckEnabled = false;
        public bool IsDeclarationCheckEnabled
        {
            get
            {
                return _isDeclarationCheckEnabled;
            }
            set
            {
                if (_isDeclarationCheckEnabled == value) return;

                _isDeclarationCheckEnabled = value;
                RaisePropertyChanged("IsDeclarationCheckEnabled");
            }
        }
        private bool _isTaxPayerCheckEnabled = false;
        public bool IsTaxPayerCheckEnabled
        {
            get
            {
                return _isTaxPayerCheckEnabled;
            }
            set
            {
                if (_isTaxPayerCheckEnabled == value) return;

                _isTaxPayerCheckEnabled = value;
                RaisePropertyChanged("IsTaxPayerCheckEnabled");
            }
        }
        private List<VTTHSetResult> _calculationRateSetVTTH;
        public List<VTTHSetResult> CalculationRateSetVTTH
        {
            get
            {
                return _calculationRateSetVTTH;
            }
            set
            {
                if (_calculationRateSetVTTH == value) return;

                _calculationRateSetVTTH = value;
                RaisePropertyChanged("CalculationRateSetVTTH");
            }
        }
        private List<IGRTSetResult> _calculationRateIGRTSet;
        public List<IGRTSetResult> CalculationRateIGRTSet
        {
            get
            {
                return _calculationRateIGRTSet;
            }
            set
            {
                if (_calculationRateIGRTSet == value) return;

                _calculationRateIGRTSet = value;
                RaisePropertyChanged("CalculationRateIGRTSet");
            }
        }
        private string _correctionPeriodAmount;
        public string CorrectionPeriodAmount
        {
            get
            {
                return _correctionPeriodAmount;
            }
            set
            {
                if (_correctionPeriodAmount == value) return;

                _correctionPeriodAmount = value;
                RaisePropertyChanged("CorrectionPeriodAmount");
            }
        }
        private string _correctionNegativePeriodAmount;
        public string CorrectionNegativePeriodAmount
        {
            get
            {
                return _correctionNegativePeriodAmount;
            }
            set
            {
                if (_correctionNegativePeriodAmount == value) return;

                _correctionNegativePeriodAmount = value;
                RaisePropertyChanged("CorrectionNegativePeriodAmount");
            }
        }
        #region NewProperty
        public string _totalsalesAmt = "0.00";
        public string TotalsalesAmt
        {
            get
            {
                return _totalsalesAmt;
            }
            set
            {
                if (_totalsalesAmt == value) return;

                _totalsalesAmt = value;
                RaisePropertyChanged("TotalsalesAmt");
            }
        }
        public string _totalsalesAdj = "0.00";
        public string TotalsalesAdj
        {
            get
            {
                return _totalsalesAdj;
            }
            set
            {
                if (_totalsalesAdj == value) return;

                _totalsalesAdj = value;
                RaisePropertyChanged("TotalsalesAdj");
            }
        }
        public string _totalpurchaseAmt = "0.00";
        public string TotalpurchaseAmt
        {
            get
            {
                return _totalpurchaseAmt;
            }
            set
            {
                if (_totalpurchaseAmt == value) return;

                _totalpurchaseAmt = value;
                RaisePropertyChanged("TotalpurchaseAmt");
            }
        }
        public string _totalpurchaseAdj = "0.00";
        public string TotalpurchaseAdj
        {
            get
            {
                return _totalpurchaseAdj;
            }
            set
            {
                if (_totalpurchaseAdj == value) return;

                _totalpurchaseAdj = value;
                RaisePropertyChanged("TotalpurchaseAdj");
            }
        }
        public string _stdsalesVat = "0.00";
        public string StdsalesVat
        {
            get
            {
                return _stdsalesVat;
            }
            set
            {
                if (_stdsalesVat == value) return;

                _stdsalesVat = value;
                RaisePropertyChanged("StdsalesVat");
            }
        }
        public string _totaldueVat = "0.00";
        public string TotaldueVat
        {
            get
            {
                return _totaldueVat;
            }
            set
            {
                if (_totaldueVat == value) return;

                _totaldueVat = value;
                if (_totaldueVat != null)
                {
                    NetdueVat = NetVatDue(TotaldueVat, Preperiodcorr, CreditVat);
                }
                RaisePropertyChanged("TotaldueVat");
            }
        }
        public string _carriedValueString;
        public string CarriedValueString
        {
            get
            {
                return _carriedValueString;
            }
            set
            {
                if (_carriedValueString == value) return;

                _carriedValueString = value;
                RaisePropertyChanged("CarriedValueString");
            }
        }
        public string _preperiodcorr;
        public string Preperiodcorr
        {
            get
            {
                return _preperiodcorr;
            }
            set
            {
                try
                {
                    if (_preperiodcorr == value) return;

                    _preperiodcorr = value;
                    if (_preperiodcorr != null)
                    {
                        bool isValiedNumber = UtilityManager.IsEnglishNumberWithMinus(Preperiodcorr);
                        if (isValiedNumber)
                        {
                            NetdueVat = NetVatDue(TotaldueVat, Preperiodcorr, CreditVat);
                            if (_preperiodcorr != "." && _preperiodcorr != "" && _preperiodcorr != "-" && !string.IsNullOrEmpty(CorrectionPeriodAmount) && !string.IsNullOrEmpty(CorrectionNegativePeriodAmount))
                            {
                                if (Convert.ToDecimal(_preperiodcorr) >= Convert.ToDecimal(CorrectionPeriodAmount) || Convert.ToDecimal(_preperiodcorr) <= Convert.ToDecimal(CorrectionNegativePeriodAmount))
                                {
                                    IsGreaterThanFiveT = true;
                                }
                                else
                                {
                                    IsGreaterThanFiveT = false;
                                }
                            }
                        }
                    }
                    if (string.IsNullOrEmpty(Preperiodcorr) || string.IsNullOrEmpty("0.00"))
                        IsSwitchToggled = false;
                }
                catch (Exception)
                {


                }
                RaisePropertyChanged("Preperiodcorr");
            }
        }
        public bool _isGreaterThanFiveT;
        public bool IsGreaterThanFiveT
        {
            get
            {
                return _isGreaterThanFiveT;
            }
            set
            {
                if (_isGreaterThanFiveT == value) return;

                _isGreaterThanFiveT = value;
                RaisePropertyChanged("IsGreaterThanFiveT");
            }
        }
        public string _netdueVat = "0.00";
        public string NetdueVat
        {
            get
            {
                return _netdueVat;
            }
            set
            {
                if (_netdueVat == value) return;

                _netdueVat = value;
                RaisePropertyChanged("NetdueVat");
            }
        }
        public string _creditVat;
        public string CreditVat
        {
            get
            {
                return _creditVat;
            }
            set
            {
                if (_creditVat == value) return;

                _creditVat = value;
                if (_creditVat != null)
                {
                    NetdueVat = NetVatDue(TotaldueVat, Preperiodcorr, CreditVat);
                }
                RaisePropertyChanged("CreditVat");
            }
        }
        public string _totalsalesVat;
        public string TotalsalesVat
        {
            get
            {
                return _totalsalesVat;
            }
            set
            {
                if (_totalsalesVat == value) return;

                _totalsalesVat = value;
                if (!string.IsNullOrEmpty(_totalsalesVat))
                {
                    if (string.IsNullOrEmpty(TotalsalesVat) || TotalsalesVat == "0")
                    {
                        TotalsalesVat = "0.00";
                    }
                    if (string.IsNullOrEmpty(TotalpurchaseVat) || TotalpurchaseVat == "0")
                    {
                        TotalpurchaseVat = "0.00";
                    }
                    TotaldueVat = (Convert.ToDouble(TotalsalesVat) - Convert.ToDouble(TotalpurchaseVat)).ToString();
                    if (TotaldueVat == "0")
                    {
                        TotaldueVat = "0.00";
                    }
                    if (!string.IsNullOrEmpty(TotaldueVat) && TotaldueVat != "0.00")
                    {
                        TotaldueVat = UtilityManager.GetCommaSeparatedAmount(TotaldueVat);
                    }
                }
                RaisePropertyChanged("TotalsalesVat");
            }
        }
        public string _stdpurchasesVat = "0.00";
        public string StdpurchasesVat
        {
            get
            {
                return _stdpurchasesVat;
            }
            set
            {
                if (_stdpurchasesVat == value) return;

                _stdpurchasesVat = value;
                RaisePropertyChanged("StdpurchasesVat");
            }
        }
        public string _importspaidVat = "0.00";
        public string ImportspaidVat
        {
            get
            {
                return _importspaidVat;
            }
            set
            {
                if (_importspaidVat == value) return;

                _importspaidVat = value;
                RaisePropertyChanged("ImportspaidVat");
            }
        }
        public string _totalpurchaseVat = "0.00";
        public string TotalpurchaseVat
        {
            get
            {
                return _totalpurchaseVat;
            }
            set
            {
                if (_totalpurchaseVat == value) return;

                _totalpurchaseVat = value;
                if (!string.IsNullOrEmpty(_totalpurchaseVat))
                {
                    TotaldueVat = (Convert.ToDouble(TotalsalesVat) - Convert.ToDouble(TotalpurchaseVat)).ToString();
                    if (TotaldueVat == "0")
                    {
                        TotaldueVat = "0.00";
                    }
                    if (!string.IsNullOrEmpty(TotaldueVat) && TotaldueVat != "0.00")
                    {
                        TotaldueVat = UtilityManager.GetCommaSeparatedAmount(TotaldueVat);
                    }
                }
                RaisePropertyChanged("TotalpurchaseVat");
            }
        }
        private string _amountPayable = "2470";
        public string AmountPayable
        {
            get
            {
                return _amountPayable;
            }
            set
            {
                if (_amountPayable == value) return;

                _amountPayable = value;
                RaisePropertyChanged("AmountPayable");
            }
        }
        public string _importsaccVat = "0.00";
        public string ImportsaccVat
        {
            get
            {
                return _importsaccVat;
            }
            set
            {
                if (_importsaccVat == value) return;

                _importsaccVat = value;
                RaisePropertyChanged("ImportsaccVat");
            }
        }
        #endregion
        private bool _isSadadNumberVisible = false;
        public bool IsSadadNumberVisible
        {
            get
            {
                return _isSadadNumberVisible;
            }
            set
            {
                if (_isSadadNumberVisible == value) return;

                _isSadadNumberVisible = value;
                RaisePropertyChanged("IsSadadNumberVisible");
            }
        }
        private string _fullAddress = string.Empty;
        public string FullAddress
        {
            get
            {
                return _fullAddress;
            }
            set
            {
                if (_fullAddress == value) return;

                _fullAddress = value;
                RaisePropertyChanged("FullAddress");
            }
        }



        private bool _is15PercentChangeToggled = false;
        public bool Is15PercentChangeToggled
        {
            get
            {
                return _is15PercentChangeToggled;
            }
            set
            {
                if (_is15PercentChangeToggled == value) return;
                _is15PercentChangeToggled = value;
                RaisePropertyChanged("Is15PercentChangeToggled");
            }
        }




        private bool _iSSwichButtonEnable = false;
        public bool IsSwichButtonEnable
        {
            get
            {
                return _iSSwichButtonEnable;
            }
            set
            {
                if (_iSSwichButtonEnable == value) return;

                _iSSwichButtonEnable = value;
                if (_iSSwichButtonEnable == true)
                {
                    // await showInfoMessageForRefund();
                    OpenIbanSet();
                }
                else
                {
                    IsVisibleDropdownForRefund = false;
                    IsDropdownVisibleForIban = false;
                    IsVisiblechkRefundDeclaration = false;
                    IsCheckedRefund = false;
                    IschkRefundDeclaration = false;
                    switchForMainButton();
                }
                RaisePropertyChanged("IsSwichButtonEnable");
            }
        }
        private bool _IsVisiblechkRefundDeclaration = false;
        public bool IsVisiblechkRefundDeclaration
        {
            get
            {
                return _IsVisiblechkRefundDeclaration;
            }
            set
            {
                if (_IsVisiblechkRefundDeclaration == value) return;

                _IsVisiblechkRefundDeclaration = value;
                RaisePropertyChanged("IsVisiblechkRefundDeclaration");
            }
        }
        private string _txtSelectedIBAN;
        public string TxtSelectedIBAN
        {
            get
            {
                return _txtSelectedIBAN;
            }
            set
            {
                if (_txtSelectedIBAN == value) return;

                _txtSelectedIBAN = value;
                RaisePropertyChanged("TxtSelectedIBAN");
            }
        }
        private Result2 _selectedIBAN;
        public Result2 SelectedIBAN
        {
            get
            {
                return _selectedIBAN;
            }
            set
            {
                if (_selectedIBAN == value) return;

                _selectedIBAN = value;
                if (_selectedIBAN != null)
                {
                    TxtSelectedIBAN = _selectedIBAN.Iban;
                }
                RaisePropertyChanged("SelectedIBAN");
            }
        }
        private Result2 _selectedIBANPrev;
        public Result2 SelectedIBANPrev
        {
            get
            {
                return _selectedIBANPrev;
            }
            set
            {
                if (_selectedIBANPrev == value) return;

                _selectedIBANPrev = value;
                //if (_selectedIBAN != null)
                //{
                //    TxtSelectedIBAN = _selectedIBAN.Iban;
                //}
                RaisePropertyChanged("SelectedIBANPrev");
            }
        }
        private List<Result2> _iBANList;
        public List<Result2> IBANList
        {
            get
            {
                return _iBANList;
            }
            set
            {
                if (_iBANList == value) return;

                _iBANList = value;
                if (_iBANList != null && _iBANList.Count != 0)
                {
                    if ((App.ICRStatus == "E0045" || App.ICRStatus == "E0006") && IsAmendClicked == false)
                    {
                        IsEnableIBAN = false;
                    }
                    else
                    {
                        IsEnableIBAN = true;
                    }
                }
                else
                {
                    IsEnableIBAN = false;
                }
                RaisePropertyChanged("IBANList");
            }
        }
        private bool _isEnableIBAN;
        public bool IsEnableIBAN
        {
            get
            {
                return _isEnableIBAN;
            }
            set
            {
                if (_isEnableIBAN == value) return;

                _isEnableIBAN = value;
                RaisePropertyChanged("IsEnableIBAN");
            }
        }
        private bool _isEnableIBANType;
        public bool IsEnableIBANType
        {
            get
            {
                return _isEnableIBANType;
            }
            set
            {
                if (_isEnableIBANType == value) return;

                _isEnableIBANType = value;
                RaisePropertyChanged("IsEnableIBANType");
            }
        }
        private bool _isVATRefunCheckedVisible;
        public bool IsVATRefunCheckedVisible
        {
            get
            {
                return _isVATRefunCheckedVisible;
            }
            set
            {
                if (_isVATRefunCheckedVisible == value) return;

                _isVATRefunCheckedVisible = value;
                RaisePropertyChanged("IsVATRefunCheckedVisible");
            }
        }
        private string _ibanNumberText;
        public string IbanNumberText
        {
            get
            {
                return _ibanNumberText;
            }
            set
            {
                if (_ibanNumberText == value) return;

                _ibanNumberText = value;
                RaisePropertyChanged("IbanNumberText");
            }
        }
        private bool _isIBANValid;
        public bool IsIBANValid
        {
            get
            {
                return _isIBANValid;
            }
            set
            {
                if (_isIBANValid == value) return;

                _isIBANValid = value;
                RaisePropertyChanged("IsIBANValid");
            }
        }
        private bool _isCheckedRefund;
        public bool IsCheckedRefund
        {
            get
            {
                return _isCheckedRefund;
            }
            set
            {
                if (_isCheckedRefund == value) return;

                _isCheckedRefund = value;
                if (_isCheckedRefund == true)
                {
                    if (!((App.ICRStatus == "E0045" || App.ICRStatus == "E0006") && IsAmendClicked == false))
                    {
                        IsTextBoxVisibleForIban = true;
                        IsTextBoxEnableForIban = true;
                        IsDropdownVisibleForIban = false;
                    }
                }
                else
                {
                    IsDropdownVisibleForIban = true;
                    IsTextBoxEnableForIban = false;
                    IsTextBoxVisibleForIban = false;
                }
                RaisePropertyChanged("IsCheckedRefund");
            }
        }
        private bool _isVisibleDropdownForRefund;
        public bool IsVisibleDropdownForRefund
        {
            get
            {
                return _isVisibleDropdownForRefund;
            }
            set
            {
                if (_isVisibleDropdownForRefund == value) return;

                _isVisibleDropdownForRefund = value;
                RaisePropertyChanged("IsVisibleDropdownForRefund");
            }
        }
        private bool _isTextBoxVisibleForIban;
        public bool IsTextBoxVisibleForIban
        {
            get
            {
                return _isTextBoxVisibleForIban;
            }
            set
            {
                if (_isTextBoxVisibleForIban == value) return;

                _isTextBoxVisibleForIban = value;
                RaisePropertyChanged("IsTextBoxVisibleForIban");
            }
        }
        private bool _isTextBoxEnableForIban;
        public bool IsTextBoxEnableForIban
        {
            get
            {
                return _isTextBoxEnableForIban;
            }
            set
            {
                if (_isTextBoxEnableForIban == value) return;

                _isTextBoxEnableForIban = value;
                RaisePropertyChanged("IsTextBoxEnableForIban");
            }
        }
        private bool _isDropdownVisibleForIban;
        public bool IsDropdownVisibleForIban
        {
            get
            {
                return _isDropdownVisibleForIban;
            }
            set
            {
                if (_isDropdownVisibleForIban == value) return;

                _isDropdownVisibleForIban = value;
                RaisePropertyChanged("IsDropdownVisibleForIban");
            }
        }
        private bool _isEnableIBANIdNumber;
        public bool IsEnableIBANIdNumber
        {
            get
            {
                return _isEnableIBANIdNumber;
            }
            set
            {
                if (_isEnableIBANIdNumber == value) return;

                _isEnableIBANIdNumber = value;
                RaisePropertyChanged("IsEnableIBANIdNumber");
            }
        }
        private List<IBANType> _iBANTypesList;
        public List<IBANType> IBANTypesList
        {
            get
            {
                return _iBANTypesList;
            }
            set
            {
                if (_iBANTypesList == value) return;

                _iBANTypesList = value;
                if (_iBANTypesList != null && _iBANTypesList.Count != 0)
                {
                    if ((App.ICRStatus == "E0045" || App.ICRStatus == "E0006") && IsAmendClicked == false)
                    {
                        IsEnableIBANType = false;
                    }
                    else
                    {
                        IsEnableIBANType = true;
                    }
                }
                else
                {
                    IsEnableIBANType = false;
                }
                RaisePropertyChanged("IBANTypesList");
            }
        }
        private IBANType _selectedIBANType;
        public IBANType SelectedIBANType
        {
            get
            {
                return _selectedIBANType;
            }
            set
            {
                if (_selectedIBANType == value) return;

                _selectedIBANType = value;
                if (_selectedIBANType != null)
                {
                    SetIBANIdNumber();
                    TxtSelectedIBANType = _selectedIBANType.Text;
                }
                else
                {
                }
                RaisePropertyChanged("SelectedIBANType");
            }
        }
        private IBANType _selectedIBANTypePrev;
        public IBANType SelectedIBANTypePrev
        {
            get
            {
                return _selectedIBANTypePrev;
            }
            set
            {
                if (_selectedIBANTypePrev == value) return;

                _selectedIBANTypePrev = value;
                //if (_selectedIBANType != null)
                //{
                //    SetIBANIdNumber();
                //    TxtSelectedIBANType = _selectedIBANType.Text;
                //}
                //else
                //{
                //}
                RaisePropertyChanged("SelectedIBANTypePrev");
            }
        }
        private string _txtSelectedIBANType;
        public string TxtSelectedIBANType
        {
            get
            {
                return _txtSelectedIBANType;
            }
            set
            {
                if (_txtSelectedIBANType == value) return;

                _txtSelectedIBANType = value;
                RaisePropertyChanged("TxtSelectedIBANType");
            }
        }
        private List<IBANIDNumber> _iBANIDNumberList;
        public List<IBANIDNumber> IBANIDNumberList
        {
            get
            {
                return _iBANIDNumberList;
            }
            set
            {
                if (_iBANIDNumberList == value) return;

                _iBANIDNumberList = value;
                if (_iBANIDNumberList != null && _iBANIDNumberList.Count() != 0)
                {
                    if ((App.ICRStatus == "E0045" || App.ICRStatus == "E0006") && IsAmendClicked == false)
                    {
                        IsEnableIBANIdNumber = false;
                    }
                    else
                    {
                        IsEnableIBANIdNumber = true;
                    }
                }
                else
                {
                    IsEnableIBANIdNumber = false;
                }
                RaisePropertyChanged("IBANIDNumberList");
            }
        }
        private IBANIDNumber _selectedIBANIDNumber;
        public IBANIDNumber SelectedIBANIDNumber
        {
            get
            {
                return _selectedIBANIDNumber;
            }
            set
            {
                if (_selectedIBANIDNumber == value) return;

                _selectedIBANIDNumber = value;
                if (_selectedIBANIDNumber != null)
                {
                    TxtSelectedIBANIDNumber = _selectedIBANIDNumber.Idnumber;
                }
                RaisePropertyChanged("SelectedIBANIDNumber");
            }
        }
        private IBANIDNumber _selectedIBANIDNumberPrev;
        public IBANIDNumber SelectedIBANIDNumberPrev
        {
            get
            {
                return _selectedIBANIDNumberPrev;
            }
            set
            {
                if (_selectedIBANIDNumberPrev == value) return;

                _selectedIBANIDNumberPrev = value;
                //if (_selectedIBANIDNumber != null)
                //{
                //    TxtSelectedIBANIDNumber = _selectedIBANIDNumber.Idnumber;
                //}
                RaisePropertyChanged("SelectedIBANIDNumberPrev");
            }
        }
        private string _TxtSelectedIBANIDNumber;
        public string TxtSelectedIBANIDNumber
        {
            get
            {
                return _TxtSelectedIBANIDNumber;
            }
            set
            {
                if (_TxtSelectedIBANIDNumber == value) return;

                _TxtSelectedIBANIDNumber = value;
                RaisePropertyChanged("TxtSelectedIBANIDNumber");
            }
        }
        private bool _isRefundVisible = false;
        public bool IsRefundVisible
        {
            get
            {
                return _isRefundVisible;
            }
            set
            {
                if (_isRefundVisible == value) return;

                _isRefundVisible = value;
                RaisePropertyChanged("IsRefundVisible");
            }
        }

        private bool _isSwichButtonOf15PercentChangeEnableToTap = true;
        public bool IsSwichButtonOf15PercentChangeEnableToTap
        {
            get
            {
                return _isSwichButtonOf15PercentChangeEnableToTap;
            }
            set
            {
                if (_isSwichButtonOf15PercentChangeEnableToTap == value) return;

                _isSwichButtonOf15PercentChangeEnableToTap = value;
                RaisePropertyChanged("IsSwichButtonOf15PercentChangeEnableToTap");
            }
        }

        private bool _isSwichButtonEnableToTap = true;
        public bool IsSwichButtonEnableToTap
        {
            get
            {
                return _isSwichButtonEnableToTap;
            }
            set
            {
                if (_isSwichButtonEnableToTap == value) return;

                _isSwichButtonEnableToTap = value;
                RaisePropertyChanged("IsSwichButtonEnableToTap");
            }
        }
        private bool _isEnableCheckedRefund = true;
        public bool IsEnableCheckedRefund
        {
            get
            {
                return _isEnableCheckedRefund;
            }
            set
            {
                if (_isEnableCheckedRefund == value) return;

                _isEnableCheckedRefund = value;
                RaisePropertyChanged("IsEnableCheckedRefund");
            }
        }

        //New Properties for VAT 15% Change

        private Result6 _vATNewModelFor15Percent;
        public Result6 VATNewModelFor15Percent
        {
            get
            {
                return _vATNewModelFor15Percent;
            }
            set
            {
                if (_vATNewModelFor15Percent == value) return;

                _vATNewModelFor15Percent = value;
                RaisePropertyChanged("VATNewModelFor15Percent");
            }
        }


        private Result6 _vATNewModelFor5Percent;
        public Result6 VATNewModelFor5Percent
        {
            get
            {
                return _vATNewModelFor5Percent;
            }
            set
            {
                if (_vATNewModelFor5Percent == value) return;

                _vATNewModelFor5Percent = value;
                RaisePropertyChanged("VATNewModelFor5Percent");
            }
        }


        public string _stdsalesVat15 = "0.00";
        public string StdsalesVat15
        {
            get
            {
                return _stdsalesVat15;
            }
            set
            {
                if (_stdsalesVat15 == value) return;

                _stdsalesVat15 = value;
                RaisePropertyChanged("StdsalesVat15");
            }
        }

        public string _stdsalesVat5 = "0.00";
        public string StdsalesVat5
        {
            get
            {
                return _stdsalesVat5;
            }
            set
            {
                if (_stdsalesVat5 == value) return;

                _stdsalesVat5 = value;
                RaisePropertyChanged("StdsalesVat5");
            }
        }


        public string _stdpurchasesVat15 = "0.00";
        public string StdpurchasesVat15
        {
            get
            {
                return _stdpurchasesVat15;
            }
            set
            {
                if (_stdpurchasesVat15 == value) return;

                _stdpurchasesVat15 = value;
                RaisePropertyChanged("StdpurchasesVat15");
            }
        }

        public string _stdpurchasesVat5 = "0.00";
        public string StdpurchasesVat5
        {
            get
            {
                return _stdpurchasesVat5;
            }
            set
            {
                if (_stdpurchasesVat5 == value) return;

                _stdpurchasesVat5 = value;
                RaisePropertyChanged("StdpurchasesVat5");
            }
        }


        public string _importspaidVat15 = "0.00";
        public string ImportspaidVat15
        {
            get
            {
                return _importspaidVat15;
            }
            set
            {
                _importspaidVat15 = value;
                RaisePropertyChanged("ImportspaidVat15");
            }
        }


        public string _importspaidVat5 = "0.00";
        public string ImportspaidVat5
        {
            get
            {
                return _importspaidVat5;
            }
            set
            {
                if (_importspaidVat5 == value) return;

                _importspaidVat5 = value;
                RaisePropertyChanged("ImportspaidVat5");
            }
        }


        public string _importsaccVat15 = "0.00";
        public string ImportsaccVat15
        {
            get
            {
                return _importsaccVat15;
            }
            set
            {
                if (_importsaccVat15 == value) return;

                _importsaccVat15 = value;
                RaisePropertyChanged("ImportsaccVat15");
            }
        }

        public string _importsaccVat5 = "0.00";
        public string ImportsaccVat5
        {
            get
            {
                return _importsaccVat5;
            }
            set
            {
                if (_importsaccVat5 == value) return;

                _importsaccVat5 = value;
                RaisePropertyChanged("ImportsaccVat5");
            }
        }


        private string _vATRate002For15Percent;
        public string VATRate002For15Percent
        {
            get
            {
                return _vATRate002For15Percent;
            }
            set
            {
                if (_vATRate002For15Percent == value) return;

                _vATRate002For15Percent = value;
                RaisePropertyChanged("VATRate002For15Percent");
            }
        }

        private string _vATRate003For5Percent;
        public string VATRate003For5Percent
        {
            get
            {
                return _vATRate003For5Percent;
            }
            set
            {
                if (_vATRate003For5Percent == value) return;

                _vATRate003For5Percent = value;
                RaisePropertyChanged("VATRate003For5Percent");
            }
        }



        #region  Color Property
        private Color _entryVatAmountTextColor;
        public Color EntryVatAmountTextColor
        {
            get
            {
                return _entryVatAmountTextColor;
            }
            set
            {
                if (_entryVatAmountTextColor == value) return;

                _entryVatAmountTextColor = value;
                if (_entryVatAmountTextColor == (Color)Application.Current.Resources["ErrorColor"])
                {
                    IsMainButtonEnabled = false;
                }
                else
                {
                    IsMainButtonEnabled = true;
                }
                RaisePropertyChanged("EntryVatAmountTextColor");
            }
        }
        #endregion
        #endregion
        #region Constructor
        public VATReturnsPageViewModelEX(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            _navigationService = navigationService;
            _dialogService = dialogService;
            IsMainButtonEnabled = false;
            ManageEnabledProperty(true);
            GoBackClick = new Command(async () =>
            {
                _navigationService.GoBack();
            });
            ChangeRegistrationClicked = new Command(async () =>
            {
                //await _dialogService.ShowMessage(AppResources.ZZZChangeRegistationNote, AppResources.ZInstructions);


                try
                {


                    var VisitPortalPopup = new ReturnPortalNavigationPopUp(AppResources.ZZZChangeRegistationNote);
                    if (App.IsArabic)
                    {
                        VisitPortalPopup.OnGotoPortal = () =>
                        {

                            Launcher.OpenAsync(ZATCAConstants.GAZTVisitPortalUrlAR);

                        };
                    }
                    else
                    {
                        VisitPortalPopup.OnGotoPortal = () =>
                        {

                            Launcher.OpenAsync(ZATCAConstants.GAZTVisitPortalUrlEN);

                        };
                    }
                    await PopupNavigation.Instance.PushAsync(VisitPortalPopup);
                }
                catch (Exception)
                {



                }
            });
            OnStepButtonClicked = new Command(async () =>
            {
                if (!string.IsNullOrEmpty(ButtonName))
                {
                    if (ButtonName == AppResources.ZVatStepTwo)
                    {
                        SelectedIndex = 1;
                        PageSelectedItem = VatTabbledPageList[1];
                    }
                    else if (ButtonName == AppResources.ZVatStepThree)
                    {
                        VATReturnFormClicked();
                        SelectedIndex = 2;
                        PageSelectedItem = VatTabbledPageList[2];
                    }
                    else if (ButtonName == AppResources.ZVatStepFour)
                    {
                        try
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await Task.Run(() =>
                                {
                                    IsLoading = true;
                                });

                                await SummaryClicked();
                                ShowMsgs();
                                SelectedIndex = 3;
                                PageSelectedItem = VatTabbledPageList[3];

                                await Task.Run(() =>
                                {
                                    IsLoading = false;
                                });
                            });
                        }
                        catch (Exception)
                        {



                        }
                    }
                    else if (ButtonName == AppResources.Submit)
                    {
                        try
                        {
                            MainThread.BeginInvokeOnMainThread(() =>
                            {
                                IsLoading = true;
                            });
                            await SubmitClicked();
                            MainThread.BeginInvokeOnMainThread(() =>
                            {
                                IsLoading = false;
                            });
                        }
                        catch (Exception)
                        {


                            IsLoading = false;
                        }
                    }
                    else if (ButtonName == AppResources.ZNote)
                    {
                        SetNoteData();
                    }
                    else if (ButtonName == "Go to ICR List")
                    {
                        _navigationService.GoBack();
                        _navigationService.NavigateTo(App.ICRListPageView);
                    }
                }
            });
            onStandardRatedSalesVatAmountTapped = new Command(() =>
            {
                ResponseVATDeclarationD.StdsalesVat = StandardRatedSalesVatAmount(ResponseVATDeclarationD.StdsalesAmt, ResponseVATDeclarationD.StdsalesAdj);
            });
            OnGetAcknowledgementLinkClicked = new Command(() =>
            {
                _navigationService.NavigateTo(App.AcknowledgementDetailsPageView, VATDeclarationData);
            });
            onInstructionsClicked = new Command(() =>
            {
                InstrunctionClicked();
            });
            onTaxPayerDetailsClicked = new Command(() =>
            {
                TaxpayerDetailsClicked();
            });
            onVATReturnFormClicked = new Command(() =>
            {
                VATReturnFormClicked();
            });
            OnAcknowlwdgementClicked = new Command(() =>
            {
                string url = ZATCAConstants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_ACK_LETTER_SRV/Ack_letterSet(Fbnum='" + VATDeclarationData + "')/$value?saml2=enabled";
                _navigationService.NavigateTo(App.AAcknowledgementView, url);
            });
            onFaqSectionClicked = new Command(async () =>
            {
                if (App.IsArabic)
                {
                    await Browser.Default.OpenAsync(new Uri("https://www.vat.gov.sa/ar/introduction-to-vat/faq/general-faqs"));
                }
                else
                {
                    await Browser.Default.OpenAsync(new Uri("https://www.vat.gov.sa/en/introduction-to-vat/faq/general-faqs"));
                }
            });
            OnDownloadAcknowlwdgementClicked = new Command(() =>
            {
                string url = ZATCAConstants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_COVERFORM_SRV/cover_formSet(Fbnum='" + VATDeclarationData + "',Utype='')/$value?saml2=enabled";
                _navigationService.NavigateTo(App.AAcknowledgementView, url);
            });
            OnVATRefreshButtonClicked = new Command(async () =>
            {
                try
                {
                    var response = await WebServiceManager.GAZTGetVATDeclarationSADADNumber(VATDeclarationData.d.Fbnum);
                    PopToRootPage();
                    SadadNumber = response.d.results[0].Vtref;
                    AmountPayable = response.d.results[0].Betrh;
                    if (!string.IsNullOrEmpty(SadadNumber))
                    {
                        IsSadadNumberVisible = true;
                    }
                }
                catch (InternetException ex)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    });
                }
                // Call Sadad number API
            });
            OnAttachmentClick = new Command(async () =>
            {
                try
                {
                    string[] filetypes;
                    filetypes = DependencyService.Get<Core.Interfaces.IDeviceInfo>().GetAttachmentTypeString();
                    PickOptions options = UtilityManager.GetFilePickerOptionsForChooser(filetypes);

                    var fileData = await FilePicker.PickAsync(options);
                    var stream = await fileData.OpenReadAsync();
                    attachment = UtilityManager.ReadFully(stream as Stream);
                    AttachmentName = fileData.FileName;

                    string ContentType = UtilityManager.GetContentType(AttachmentName.Split('.').Last());
                    AttachmentRootOject _attachment = await WebServiceManager.GAZTSaveVATDeclarationAttachment(attachment, AttachmentName, VATDeclarationData.d.ReturnIdz, "VTA0", ContentType);
                    PopToRootPage();
                    if (_attachment != null && _attachment.d != null)
                    {
                        VATDeclarationData.d.ATTACHSet.results.Add(_attachment.d);
                        ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(VATDeclarationData.d.ATTACHSet.results as List<Attachment>);
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            VatAttachmentsList = myCollection;
                        });
                        VatAttachmentsList = myCollection;
                    }
                }
                catch (InternetException ex)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    });
                }
            });
            onSummaryClicked = new Command(async () =>
            {
                await SummaryClicked();
            });
            onCreditCarriedForwardClicked = new Command(() =>
            {
                _navigationService.NavigateTo(App.CreditCarriedPageView, VATDeclarationData);
            });
            onOptionClicked = new Command(() =>
            {
                if (IsVisibleOptionMenu == true)
                {
                    IsVisibleOptionMenu = false;
                }
                else
                {
                    IsVisibleOptionMenu = true;
                }
            });

            //Not this method in use
            OnSaveAsDraftClicked = new Command(async () =>
            {
                CreateDataForPost();
                string operation = "05";// Passed 05 to save the data as a draft
                VATDeclarationData.d.Operationz = operation;
                StepNumber = "01";
                if (IsDeclarationCheckedForInstruction == true)
                {
                    StepNumber = "02";
                }
                if (IsCheckedTaxPayerDetailsInfo == true)
                {
                    StepNumber = "03";
                }
                if (IsDeclarationCheckedForSummary == true)
                {
                    StepNumber = "04";
                }
                VATDeclarationData.d.StepNumber = StepNumber;
                VATDeclarationData.d.UserTypz = "TP";
                await SaveReturnAndGetReturnAndSetButtons();
                await _dialogService.ShowMessage(AppResources.DraftSaved, AppResources.Information);
            });
            //ManageEnabledProperty(true);
        }
        #endregion
        #region Method
        public bool CanExecuteStepBtnClickCommand(object arg)
        {
            return _isMainButtonEnabled;
        }
        public async void ExecuteStepBtnClickCommand(object obj)
        {
        }
        public void switchForMainButton()
        {
            if (IsDeclarationCheckedForSummary == true)
            {
                if ((App.ICRStatus == "E0045" || App.ICRStatus == "E0006" || App.ICRStatus == "E0055" || App.ICRStatus == "E0058") && IsAmendClicked == false)
                {
                    IsMainButtonEnabled = false;
                }
                else
                {
                    IsMainButtonEnabled = true;
                }
            }
            else
            {
                IsMainButtonEnabled = false;
            }
        }
        public void ShowMsgs()
        {
            StringBuilder Masseges = new StringBuilder();
            if (!string.IsNullOrEmpty(TotalsalesAmt) && !string.IsNullOrEmpty(TotalsalesAdj))
            {
                string Percentage = CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                //   decimal PercentageValue = (LabelTotalsalesAmt / 100) * Convert.ToDecimal(Percentage);
                if (Convert.ToDecimal(Percentage) / 100 * Convert.ToDecimal(TotalsalesAmt) + Convert.ToDecimal(TotalsalesAmt) < Convert.ToDecimal(TotalsalesAdj))
                {
                    Masseges.Append(string.Format(AppResources.ZZValidationMessage11_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]));
                }
                //  CheckSixaSixb(Convert.ToDecimal(LabelTotalsalesAmt.Text), Convert.ToDecimal(LabelTotalsalesAdj.Text));
            }
            if (!string.IsNullOrEmpty(TotalsalesAmt) && !string.IsNullOrEmpty(TotalpurchaseAmt))
            {
                string Percentage = CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                //  decimal PercentageValue = (LabelTotalsalesAmt / 100) * Convert.ToDecimal(Percentage);
                if (Convert.ToDecimal(Percentage) / 100 * Convert.ToDecimal(TotalsalesAmt) + Convert.ToDecimal(TotalsalesAmt) < Convert.ToDecimal(TotalpurchaseAmt))
                {
                    if (Masseges.Length == 0)
                    {
                        Masseges.Append(AppResources.ZZValidationMessage18_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero);
                    }
                    else
                    {
                        Masseges.Append(Environment.NewLine);
                        Masseges.Append(Environment.NewLine);
                        Masseges.Append(AppResources.ZZValidationMessage18_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero);
                    }
                }
                // CheckSixaTweveb(Convert.ToDecimal(LabelTotalsalesAmt.Text), Convert.ToDecimal(LabelTotalpurchaseAmt.Text));
            }
            if (!string.IsNullOrEmpty(TotalpurchaseAmt) && !string.IsNullOrEmpty(TotalpurchaseAdj))
            {
                string Percentage = CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                //  decimal PercentageValue = (LabelTotalpurchaseAmt / 100) * Convert.ToDecimal(Percentage);
                if (Convert.ToDecimal(Percentage) / 100 * Convert.ToDecimal(TotalpurchaseAmt) + Convert.ToDecimal(TotalpurchaseAmt) < Convert.ToDecimal(TotalpurchaseAdj))
                {
                    if (Masseges.Length == 0)
                    {
                        Masseges.Append(string.Format(AppResources.ZZValidationMessage19_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]));
                    }
                    else
                    {
                        Masseges.Append(Environment.NewLine);
                        Masseges.Append(Environment.NewLine);
                        Masseges.Append(string.Format(AppResources.ZZValidationMessage19_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]));
                    }
                }
                // CheckTweveaTweveb(Convert.ToDecimal(LabelTotalpurchaseAmt.Text), Convert.ToDecimal(LabelTotalpurchaseAdj.Text));
            }
            if (!string.IsNullOrEmpty(TotaldueVat) && !string.IsNullOrEmpty(Preperiodcorr))
            {
                //CheckThirteenaFouteenb(Convert.ToDecimal(LabelTotaldueVat.Text), Convert.ToDecimal(EntryPreperiodcorr.Text));
            }
            if (Masseges.Length > 0)
            {
                PopUp Pop = new PopUp();
                Pop.IsLinkAvailable = false;
                Pop.Message = Masseges.ToString();
                if (App.IsArabic)
                {
                    Pop.FlowDirections = "RightToLeft";
                }
                else
                {
                    Pop.FlowDirections = "LeftToRight";
                }
                PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
            }
        }
        public void SetCommasforAll()
        {
            ResponseVATDeclarationD.StdsalesAmt = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.StdsalesAmt);
            ResponseVATDeclarationD.StdsalesAdj = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.StdsalesAdj);
            ResponseVATDeclarationD.SalesGccAmt = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.SalesGccAmt);
            ResponseVATDeclarationD.SalesGccAdj = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.SalesGccAdj);
            ResponseVATDeclarationD.ZerosalesAmt = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ZerosalesAmt);
            ResponseVATDeclarationD.ZerosalesAdj = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ZerosalesAdj);
            ResponseVATDeclarationD.ExportsAmt = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ExportsAmt);
            ResponseVATDeclarationD.ExportsAdj = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ExportsAdj);
            ResponseVATDeclarationD.ExemptsalesAmt = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ExemptsalesAmt);
            ResponseVATDeclarationD.ExemptsalesAdj = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ExemptsalesAdj);
            ResponseVATDeclarationD.StdpurchaseAmt = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.StdpurchaseAmt);
            ResponseVATDeclarationD.StdpurchaseAdj = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.StdpurchaseAdj);
            ResponseVATDeclarationD.ImportspaidAmt = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ImportspaidAmt);
            ResponseVATDeclarationD.ImportspaidAdj = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ImportspaidAdj);
            ResponseVATDeclarationD.ImportsaccAmt = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ImportsaccAmt);
            ResponseVATDeclarationD.ImportsaccAdj = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ImportsaccAdj);
            ResponseVATDeclarationD.ZeropurchaseAmt = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ZeropurchaseAmt);
            ResponseVATDeclarationD.ZeropurchaseAdj = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ZeropurchaseAdj);
            ResponseVATDeclarationD.ExemptpurchaseAmt = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ExemptpurchaseAmt);
            ResponseVATDeclarationD.ExemptpurchaseAdj = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.ExemptpurchaseAdj);
            ResponseVATDeclarationD.Preperiodcorr = UtilityManager.GetCommaSeparatedAmount(ResponseVATDeclarationD.Preperiodcorr);

            //For New 15% Change
            SetCommasforNew15percentchange();
        }

        public void SetCommasforNew15percentchange()
        {
            if (VATDeclarationData.d.GoliveFg == "X")
            {
                VATNewModelFor15Percent = VATDeclarationData.d.VATPERITEMSet.results.Where(x => x.Type == "002").FirstOrDefault();
                VATNewModelFor5Percent = VATDeclarationData.d.VATPERITEMSet.results.Where(x => x.Type == "003").FirstOrDefault();


                VATNewModelFor15Percent.ImportsaccAdj = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.ImportsaccAdj);
                VATNewModelFor15Percent.ImportsaccAmt = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.ImportsaccAmt);
                VATNewModelFor15Percent.ImportsaccVat = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.ImportsaccVat);
                VATNewModelFor15Percent.ImportspaidAdj = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.ImportspaidAdj);
                VATNewModelFor15Percent.ImportspaidAmt = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.ImportspaidAmt);
                VATNewModelFor15Percent.ImportspaidVat = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.ImportspaidVat);
                VATNewModelFor15Percent.StdpurchaseAdj = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.StdpurchaseAdj);
                VATNewModelFor15Percent.StdpurchaseAmt = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.StdpurchaseAmt);
                VATNewModelFor15Percent.StdpurchasesVat = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.StdpurchasesVat);
                VATNewModelFor15Percent.StdsalesAdj = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.StdsalesAdj);
                VATNewModelFor15Percent.StdsalesAmt = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.StdsalesAmt);
                VATNewModelFor15Percent.StdsalesVat = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor15Percent.StdsalesVat);


                VATNewModelFor5Percent.ImportsaccAdj = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.ImportsaccAdj);
                VATNewModelFor5Percent.ImportsaccAmt = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.ImportsaccAmt);
                VATNewModelFor5Percent.ImportsaccVat = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.ImportsaccVat);
                VATNewModelFor5Percent.ImportspaidAdj = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.ImportspaidAdj);
                VATNewModelFor5Percent.ImportspaidAmt = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.ImportspaidAmt);
                VATNewModelFor5Percent.ImportspaidVat = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.ImportspaidVat);
                VATNewModelFor5Percent.StdpurchaseAdj = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.StdpurchaseAdj);
                VATNewModelFor5Percent.StdpurchaseAmt = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.StdpurchaseAmt);
                VATNewModelFor5Percent.StdpurchasesVat = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.StdpurchasesVat);
                VATNewModelFor5Percent.StdsalesAdj = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.StdsalesAdj);
                VATNewModelFor5Percent.StdsalesAmt = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.StdsalesAmt);
                VATNewModelFor5Percent.StdsalesVat = UtilityManager.GetCommaSeparatedAmount(VATNewModelFor5Percent.StdsalesVat);
            }
        }

        public async Task SetIBANIdNumber()
        {
            try
            {
                TxtSelectedIBANIDNumber = string.Empty;
                List<IBANIDNumber> iBANIDNumbersResponse = await WebServiceManager.GAZTGetIBANIdNumber(SelectedIBANType.key);
                PopToRootPage();
                if (iBANIDNumbersResponse != null || iBANIDNumbersResponse.Count() != 0)
                {
                    IBANIDNumberList = new List<IBANIDNumber>();
                    IBANIDNumberList = iBANIDNumbersResponse;
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
        public void ManageEnabledProperty(bool value)
        {
            IsControlEnabled = value;
            IsControlEnabledForEntry = !IsControlEnabled;
            IsDeclarationCheckEnabled = value;
            IsTaxPayerCheckEnabled = value;
            IsMainButtonEnabled = value;
        }
        public async Task ManageEnabledAsyncProperty(bool value)
        {
            IsControlEnabled = value;
            IsControlEnabledForEntry = !IsControlEnabled;
            IsDeclarationCheckEnabled = value;
            IsTaxPayerCheckEnabled = value;
            IsMainButtonEnabled = value;
        }
        public async Task OnSaveDraftClicked()
        {
            try
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    CreateDataForPost();
                    string operation = "05";// Passed 05 to save the data as a draft
                    VATDeclarationData.d.Operationz = operation;
                    StepNumber = "00";
                    StepNumberz = "1";
                    if (IsDeclarationCheckedForInstruction == true)
                    {
                        StepNumberz = "2";
                    }
                    if (IsCheckedTaxPayerDetailsInfo == true)
                    {
                        StepNumberz = "3";
                    }
                    if (IsDeclarationCheckedForSummary == true)
                    {
                        StepNumberz = "4";
                    }
                    VATDeclarationData.d.StepNumber = StepNumber;
                    VATDeclarationData.d.StepNumberz = StepNumberz;
                    VATDeclarationData.d.UserTypz = "TP";
                    var res = await SaveReturnAndGetReturnAndSetButtons();
                    if (res != null && res.d != null)
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            if (IsVisibleInstrunction)
                            {
                                if (IsDeclarationCheckedForInstruction == false)
                                {
                                    IsMainButtonEnabled = false;
                                }
                            }
                            else if (IsVisibleTaxPayerDetails)
                            {
                                if (IsCheckedTaxPayerDetailsInfo == false)
                                {
                                    IsMainButtonEnabled = false;
                                }
                            }
                            await _dialogService.ShowMessage(string.Format(AppResources.DraftSaved, "  " + res.d.Fbnum), AppResources.Information);
                            // await _dialogService.ShowMessage(AppResources.DraftSaved + res.d.Fbnum, AppResources.Information);
                        });
                    }
                    else
                    {
                        IsLoading = false;
                        if (string.IsNullOrEmpty(WebServiceManager.ErrorMessageForVAT))
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                _navigationService.GoBack();
                            });
                        }
                        else
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessage(WebServiceManager.ErrorMessageForVAT, AppResources.Information);
                                //_navigationService.GoBack();
                                WebServiceManager.ErrorMessageForVAT = string.Empty;
                            });
                        }
                        //MainThread.BeginInvokeOnMainThread(async () =>
                        //{
                        //    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        //});
                    }
                });
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = false;
                });
            }
            catch (Exception)
            {


            }
        }
        public void InstrunctionClicked()
        {
            ClearPage();
            IsVisibleInstrunction = true;
            SelectedIndex = 0;
            IsMainButtonEnabled = false;
            ButtonName = AppResources.ZVatStepTwo;
            if (App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057")
            {
                //If Required
            }
            else
            {
                if (App.ICRStatus == "E0045")
                {
                    IsDeclarationCheckedForInstruction = true;
                }
            }
        }
        public void TaxpayerDetailsClicked()
        {
            ClearPage();
            IsVisibleTaxPayerDetails = true;
            SelectedIndex = 1;
            ButtonName = AppResources.ZVatStepThree;
            if (App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057")
            {
                if (IsCheckedTaxPayerDetailsInfo == true)
                {
                    IsMainButtonEnabled = true;
                }
                else
                {
                    IsMainButtonEnabled = false;
                }
            }
            else
            {
                if (App.ICRStatus == "E0045" || App.ICRStatus == "E0006" || App.ICRStatus == "E0055" || App.ICRStatus == "E0058")
                {
                    IsCheckedTaxPayerDetailsInfo = true;
                    //IsMainButtonEnabled = true;
                }
                else
                {
                    if (App.ICRStatus != "E0001")
                    {
                        IsCheckedTaxPayerDetailsInfo = false;
                        IsMainButtonEnabled = false;
                    }
                    else
                    {
                        if (IsCheckedTaxPayerDetailsInfo == true)
                        {
                            IsMainButtonEnabled = true;
                        }
                        else
                        {
                            IsMainButtonEnabled = false;
                        }
                    }
                }
            }
        }
        public void VATReturnFormClicked()
        {
            ClearPage();
            IsVisibleVatReturnForm = true;
            SelectedIndex = 2;
            ButtonName = AppResources.ZVatStepFour;
            if (App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057")
            {
                if (IsCheckedTaxPayerDetailsInfo == true)
                {
                    IsMainButtonEnabled = true;
                }
                else
                {
                    IsMainButtonEnabled = false;
                }
            }
            else
            {
                if (App.ICRStatus == "E0045" || App.ICRStatus == "E0006" || App.ICRStatus == "E0055" || App.ICRStatus == "E0058")
                {
                    IsCheckedTaxPayerDetailsInfo = true;
                    //IsMainButtonEnabled = true;
                }
                else
                {
                    if (App.ICRStatus != "E0001")
                    {
                        IsCheckedTaxPayerDetailsInfo = false;
                        IsMainButtonEnabled = false;
                    }
                    else
                    {
                        if (IsCheckedTaxPayerDetailsInfo == true)
                        {
                            IsMainButtonEnabled = true;
                        }
                        else
                        {
                            IsMainButtonEnabled = false;
                        }
                    }
                }
            }
        }
        public async Task SummaryClicked()
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });
            try
            {
                bool value = false;
                ClearPage();
                DisableForRefund();
                IsVisibleSummary = true;
                SelectedIndex = 3;
                //  IsFirstSubmission = true;
                if (!string.IsNullOrEmpty(TotalpurchaseAmt) && !string.IsNullOrEmpty(TotalsalesAmt))
                {
                    value = IsCheckedDraftMode();
                    if (Convert.ToDouble(NetdueVat) < 0)
                    {
                        IsRefundVisible = true;
                        IsSwichButtonEnableToTap = true;
                        if (value || App.ICRStatus == "E0045" || App.ICRStatus == "E0006")
                        {
                            if (VATDeclarationData.d.RefundFg == "1")
                            {
                                IsSwichButtonEnable = true;
                                IsVisibleDropdownForRefund = true;
                                IsVisiblechkRefundDeclaration = true;
                                //  IsDropdownVisibleForIban = true;
                                if (VATDeclarationData.d.IbanCb == "1")// IbanCb is equal to 1 if there is no data in IBan List as per Vinay
                                {
                                    IsTextBoxVisibleForIban = true;
                                    IsDropdownVisibleForIban = false;
                                    IsCheckedRefund = true;
                                    if (!string.IsNullOrEmpty(VATDeclarationData.d.Iban))
                                    {
                                        IbanNumberText = VATDeclarationData.d.Iban;
                                    }
                                }
                                else
                                {
                                    IsTextBoxVisibleForIban = false;
                                    IsDropdownVisibleForIban = true;
                                    if (!string.IsNullOrEmpty(VATDeclarationData.d.Iban))
                                    {
                                        SelectedIBAN = IBANList.Where(x => x.Iban == VATDeclarationData.d.Iban).FirstOrDefault();
                                    }
                                    if (IBANList != null && IBANList.Count > 0)
                                    {
                                        IsVATRefunCheckedVisible = false;
                                    }
                                    else
                                    {
                                        IsVATRefunCheckedVisible = true;
                                    }
                                }
                                if (!string.IsNullOrEmpty(VATDeclarationData.d.Idtype))
                                {
                                    SelectedIBANType = IBANTypesList.Where(x => x.key == VATDeclarationData.d.Idtype).FirstOrDefault();
                                    if (SelectedIBANType != null)
                                    {
                                        await SetIBANIdNumber();
                                    }
                                }
                                if (!string.IsNullOrEmpty(VATDeclarationData.d.Idnum))
                                {
                                    if (IBANIDNumberList != null && IBANIDNumberList.Count != 0)
                                    {
                                        SelectedIBANIDNumber = IBANIDNumberList.Where(x => x.Idnumber == VATDeclarationData.d.Idnum).FirstOrDefault();
                                    }
                                }
                            }
                            else
                            {
                                IsSwichButtonEnableToTap = true;
                                IsSwichButtonEnable = false;
                                IsDropdownVisibleForIban = false;
                                IsVisibleDropdownForRefund = false;
                                IsVisiblechkRefundDeclaration = false;
                            }
                        }
                    }
                    else
                    {
                        IsRefundVisible = false;
                        IsTextBoxVisibleForIban = false;
                        IsDropdownVisibleForIban = false;
                        IsVisibleDropdownForRefund = false;
                        IsVisiblechkRefundDeclaration = false;
                    }
                }

                if ((App.ICRStatus == "E0045" || App.ICRStatus == "E0006" || App.ICRStatus == "E0055" || App.ICRStatus == "E0058") && IsAmendClicked == false)
                {
                    IsTextBoxEnableForIban = false;
                    IsEnableCheckedRefund = false;
                    IsTextBoxEnableForIban = false;
                    IsSwichButtonEnableToTap = false;
                    IsGetAcknowledgementClicked = true;
                    IschkRefundDeclaration = true;
                    if (VATDeclarationData.d.IbanCb == "1")
                    {
                        IsCheckedRefund = true;
                        IsVATRefunCheckedVisible = true;
                    }
                    else
                    {
                        if (IBANList != null && IBANList.Count > 0)
                        {
                            IsVATRefunCheckedVisible = false;
                        }
                        else
                        {
                            IsVATRefunCheckedVisible = true;
                        }
                    }
                    ButtonName = AppResources.Submit;
                    IsDeclarationCheckedForSummary = true;
                    IsMainButtonEnabled = false;
                }
                else
                {
                    if (App.ICRStatus == "E0045" && IsAmendClicked == true)
                    {
                        if (VATDeclarationData.d.IBANSet.results != null && VATDeclarationData.d.IBANSet.results.Count() != 0)
                        {
                            IBANList = new List<Result2>();
                            IBANList = VATDeclarationData.d.IBANSet.results;
                            IsVATRefunCheckedVisible = false;
                            IsEnableCheckedRefund = false;
                            IsTextBoxVisibleForIban = false;
                            IsDropdownVisibleForIban = true;
                        }
                        else
                        {
                            IsVATRefunCheckedVisible = true;
                            IsEnableCheckedRefund = true;
                        }
                        createIBANType();
                        IschkRefundDeclaration = false;
                    }
                    ButtonName = AppResources.Submit;
                    if (App.ICRStatus != "E0001")
                    {
                        if (App.ICRStatus == "E0045" && IsAmendClicked == false || App.ICRStatus == "E0006")
                        {
                            IsMainButtonEnabled = false;
                            IsDeclarationCheckedForSummary = true;
                        }
                        else
                        {
                            IsMainButtonEnabled = false;
                        }
                        IsDeclarationCheckedForSummary = false;
                        IschkRefundDeclaration = false;
                    }
                    else
                    {
                        if (App.ICRStatus == "E0045" && IsAmendClicked == false || App.ICRStatus == "E0006" || App.ICRStatus == "E0058")
                        {
                            IsMainButtonEnabled = false;
                            IsDeclarationCheckedForSummary = true;
                        }
                        else
                        {
                            IsMainButtonEnabled = false;
                        }
                        IsDeclarationCheckedForSummary = false;
                        IschkRefundDeclaration = false;
                    }
                }
                if (IsFirstSubmission == false)
                {
                    IsDeclarationCheckedForSummary = true;
                    IschkRefundDeclaration = true;
                }
                //if (App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057")
                //{
                //    IsDeclarationCheckedForSummary = true;
                //}
                //else
                //{
                //    IsDeclarationCheckedForSummary = false;
                //}
                if (VATDeclarationData.d.DecFg == "1")
                {
                    IsDeclarationCheckedForSummary = true;
                }
                else
                {
                    IsDeclarationCheckedForSummary = false;
                }
                if (VATDeclarationData.d.TcFlg == "1")
                {
                    IschkRefundDeclaration = true;
                }
                else
                {
                    IschkRefundDeclaration = false;
                }
                if (VATDeclarationData.d.IbanCb == "1")
                {
                    IsCheckedRefund = true;
                }
                else
                {
                    IsCheckedRefund = false;
                }



            }
            catch (Exception)
            {


            }
            await Task.Run(() =>
            {
                IsLoading = false;
            });
        }
        public void CreditCarriedClicked()
        {
            ClearPage();
            IsTabbedMenuAvailable = false;
            IsVisibleCreditCarriedLabel = true;
            IsVisibleCreditCarriedForward = true;
            ButtonName = AppResources.Submit;
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


        public bool IsReturnIsBilledOrAmend()
        {
            bool result = false;
            if ((App.ICRStatus == "E0045" || App.ICRStatus == "E0006") && IsAmendClicked == false)
            {
                result = true;
            }
            return result;
        }
        public bool IsTabbedValid(string value)
        {
            bool bvalue = false;
            if (VATDeclarationData.d.StepNumber == value || value == "0" + VATDeclarationData.d.StepNumber)
            {
                bvalue = true;
            }
            return bvalue;
        }
        public bool IsCheckedDraftMode()
        {
            bool value = false;
            if (App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057")
            {
                value = true;
            }
            return value;
        }
        public async Task SubmitClicked()
        {
            try
            {
                //await Task.Run(() =>
                //{
                //    IsLoading = true;
                //});
                //await Task.Run(async() =>
                //{
                //IsLoading = true;
                //if (FirstSubmissionCount != 1)
                //{
                //    CreateDataForPost();
                //}
                //IsVisibleAcknowledgment = true;
                if (IsVisibleDropdownForRefund)
                {
                    if (IsCheckedRefundOfSubmitForYes() && IsRefundYesMsgDisplayed == false)
                    {
                        IsFirstSubmission = true;
                    }
                }
                else
                {
                    if (IsCheckedRefundForSubmit() && IsRefundNoMsgDisplayed == false)
                    {
                        IsFirstSubmission = true;
                    }
                }




                ButtonName = AppResources.Submit;
                if (!IsFirstSubmission)
                {
                    CreateDataForPost();
                    FirstSubmissionCount = 0;
                    //ClearPage();
                    //IsVisibleAcknowledgment = true;
                    await Task.Delay(2000);
                    //Check the fbnumber created or not
                    if (string.IsNullOrEmpty(VATDeclarationData.d.Fbnum))
                    {

                        VATDeclaration _vATDeclarationForGet = await WebServiceManager.GAZTGetVATReturns(App.Fbguid, VATDeclarationData.d.Fbnumz, App.EUser, "");
                        PopToRootPage();
                        if (_vATDeclarationForGet != null && _vATDeclarationForGet.d != null)
                        {
                            if (!string.IsNullOrEmpty(_vATDeclarationForGet.d.Fbnum))
                            {
                                //
                            }
                            else
                            {
                                MainThread.BeginInvokeOnMainThread(async () =>
                                {
                                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                    _navigationService.GoBack();
                                });
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
                    }

                    string operation = "01";// Passed operation "01" to submit the VAT Declaration Data
                                            //   VATDeclarationData.d.StepNumberz = "04";
                                            //VATDeclarationData.d.StepNumber = "00";
                                            // VATDeclarationData.d.Fbguid = string.Empty;
                    VATDeclarationData.d.StepNumberz = "04";
                    VATDeclarationData.d.UserTypz = "TP";
                    VATDeclarationData.d.Operationz = operation;
                    //IsLoading = false;
                    var res = await SaveReturnAndGetReturnAndSetButtons();
                    //MainThread.BeginInvokeOnMainThread(async () =>
                    //{
                    //   _dialogService.ShowMessage(string.Format(AppResources.ZZGeneralMessage_VATReturnFormSubmittedSuccessfullyAndFormBundleNumber, VATDeclarationData.d.Fbnum), AppResources.Information);
                    //});
                    if (res != null && res.d != null)
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await ManageEnabledAsyncProperty(false);
                            IsEnableSwitchToggledFor15PercentChange = false;
                            IsMainButtonVisible = false;
                            IsSwichButtonEnableToTap = false;
                            IsEnableIBAN = false;
                            IsEnableCheckedRefund = false;
                            IsEnableIBANType = false;
                            IsEnableIBANIdNumber = false;
                            IsGetAcknowledgementClicked = true;
                            IsMoreButtonEnabled = false;
                        });
                        if (App.ICRStatus == "E0045" || App.ICRStatus == "E0056")
                        {
                            await Task.Delay(5000);
                        }
                        //ManageEnabledProperty(false);
                        _navigationService.NavigateTo(App.AcknowledgementDetailsPageView, VATDeclarationData);
                    }
                    else
                    {
                        IsLoading = false;
                        if (string.IsNullOrEmpty(WebServiceManager.ErrorMessageForVAT))
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                _navigationService.GoBack();
                            });
                        }
                        else
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessage(WebServiceManager.ErrorMessageForVAT, AppResources.Information);
                                // _navigationService.GoBack();
                                WebServiceManager.ErrorMessageForVAT = string.Empty;
                            });
                        }
                        // await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    }
                }
                else
                {
                    bool IsCheckIfRefundCheckIsNotSelected = false;
                    if (IsVisibleDropdownForRefund)
                    {

                        IsCheckIfRefundCheckIsNotSelected = await IsCheckedRefundGetorNotForYesMessage();
                    }
                    else
                    {

                        IsCheckIfRefundCheckIsNotSelected = await IsCheckedRefundGetorNot();
                    }

                    if (IsCheckIfRefundCheckIsNotSelected)
                    {
                        IsFirstSubmission = false;
                        VATDeclaration resNew = null;
                        //if (String.IsNullOrEmpty(VATDeclarationData.d.Fbnum) || App.ICRStatus == "E0045")
                        //{
                        CreateDataForPost();
                        FirstSubmissionCount = 1;
                        string operation = "01";
                        VATDeclarationData.d.StepNumber = "04";
                        VATDeclarationData.d.StepNumberz = "04";
                        VATDeclarationData.d.UserTypz = "TP";
                        VATDeclarationData.d.Operationz = operation;
                        VATDeclaration response = new VATDeclaration();
                        //response = WebServiceManager.SaveVATDeclarationData(VATDeclarationData);
                        //PopToRootPage();
                        // IsLoading = false;
                        resNew = await SaveReturnAndGetReturnAndSetButtons();
                        // }
                        if (resNew != null && resNew.d != null)
                        {
                            decimal FourteenA = 0;
                            if (!string.IsNullOrEmpty(TotaldueVat) && !string.IsNullOrEmpty(Preperiodcorr))
                            {
                                FourteenA = Convert.ToDecimal(TotaldueVat) + Convert.ToDecimal(Preperiodcorr);
                            }
                            if (IsSwichButtonEnable == false && FourteenA < 5000 && Convert.ToDecimal(NetdueVat) < 0 || IsSwichButtonEnable == true && FourteenA < 100000 && Convert.ToDecimal(CreditVat) > 0)
                            {
                                StringBuilder Masseges = new StringBuilder();
                                Masseges.Append(AppResources.Pleasereviewthecalculationandsubmitagain);
                                Masseges.Append(Environment.NewLine);
                                Masseges.Append(Environment.NewLine);
                                Masseges.Append(Environment.NewLine);
                                Masseges.Append(AppResources.CreditReturnMsg);
                                PopUp Pop = new PopUp();
                                Pop.IsLinkAvailable = false;
                                Pop.IsRed = "#e84941";
                                Pop.IsBold = "Bold";
                                Pop.Message = Masseges.ToString();
                                PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                                SelectedIndex = 2;
                                PageSelectedItem = VatTabbledPageList[2];
                            }
                            else
                            {
                                if (resNew.d.SubmitFg == "" || resNew.d.SubmitFg == string.Empty)
                                {
                                    MainThread.BeginInvokeOnMainThread(async () =>
                                    {
                                        await ManageEnabledAsyncProperty(false);
                                        IsEnableSwitchToggledFor15PercentChange = false;
                                        IsMainButtonVisible = false;
                                        IsSwichButtonEnableToTap = false;
                                        IsEnableIBAN = false;
                                        IsEnableCheckedRefund = false;
                                        IsEnableIBANType = false;
                                        IsEnableIBANIdNumber = false;
                                        IsGetAcknowledgementClicked = true;
                                        IsMoreButtonEnabled = false;
                                    });
                                    if (App.ICRStatus == "E0045" || App.ICRStatus == "E0056")
                                    {
                                        await Task.Delay(5000);
                                    }
                                    //ManageEnabledProperty(false);
                                    _navigationService.NavigateTo(App.AcknowledgementDetailsPageView, VATDeclarationData);
                                }
                                else
                                {

                                    await _dialogService.ShowMessage(AppResources.Pleasereviewthecalculationandsubmitagain, AppResources.Information);
                                    VATReturnFormClicked();
                                    SelectedIndex = 2;
                                    PageSelectedItem = VatTabbledPageList[2];
                                }
                            }
                            //await _dialogService.ShowMessage(AppResources.Pleasereviewthecalculationandsubmitagain, AppResources.Information);
                            //VATReturnFormClicked();
                            //PageSelectedItem = VatTabbledPageList[2];
                        }
                        else
                        {
                            IsFirstSubmission = true;
                            FirstSubmissionCount = 0;
                            IsLoading = false;
                            if (string.IsNullOrEmpty(WebServiceManager.ErrorMessageForVAT))
                            {
                                MainThread.BeginInvokeOnMainThread(async () =>
                                {
                                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                    _navigationService.GoBack();
                                });
                            }
                            else
                            {
                                MainThread.BeginInvokeOnMainThread(async () =>
                                {
                                    await _dialogService.ShowMessage(WebServiceManager.ErrorMessageForVAT, AppResources.Information);
                                    //_navigationService.GoBack();
                                    WebServiceManager.ErrorMessageForVAT = string.Empty;
                                });
                            }
                            //  await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        }
                    }
                }
                // });
                //await Task.Run(() =>
                //{
                //    IsLoading = false;
                //});
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
                IsLoading = false;
            }
        }

        public bool IsCheckedRefundForSubmit()
        {
            bool result = false;
            string netVATdue = string.Empty;
            if (!string.IsNullOrEmpty(NetdueVat))
            {
                netVATdue = !NetdueVat.Contains(",") ? NetdueVat : NetdueVat.Replace(",", "");
            }

            if (Convert.ToDouble(netVATdue) <= 0 && IsVisibleDropdownForRefund == false && IsRefundVisible == true)
            {
                result = true;
            }
            return result;
        }

        public bool IsCheckedRefundOfSubmitForYes()
        {
            bool result = false;
            string netVATdue = string.Empty;
            if (!string.IsNullOrEmpty(NetdueVat))
            {
                netVATdue = !NetdueVat.Contains(",") ? NetdueVat : NetdueVat.Replace(",", "");
            }

            if (Convert.ToDouble(netVATdue) <= 0 && IsVisibleDropdownForRefund == true && IsRefundVisible == true)
            {
                result = true;
            }
            return result;
        }


        public async Task<bool> IsCheckedRefundGetorNot()
        {
            bool returnResult = false;
            string NetVAT = string.Empty;
            if (!string.IsNullOrEmpty(NetdueVat))
            {
                NetVAT = !NetdueVat.Contains(",") ? NetdueVat : NetdueVat.Replace(",", "");
            }
            if (Convert.ToDouble(NetVAT) <= 0 && IsVisibleDropdownForRefund == false && IsRefundVisible == true)
            {
                bool result;
                if (App.IsArabic)
                {
                    result = await Application.Current.MainPage.DisplayAlert(AppResources.ZZZConfirmationMsg, AppResources.VATRefundNoMsg, AppResources.ZZCancel, AppResources.Confirm);
                    if (!result)
                    {
                        returnResult = true;
                        IsRefundNoMsgDisplayed = true;
                        IsRefundYesMsgDisplayed = false;
                    }
                    else
                    {
                        returnResult = false;
                    }
                }
                else
                {
                    result = await Application.Current.MainPage.DisplayAlert(AppResources.ZZZConfirmationMsg, AppResources.VATRefundNoMsg, AppResources.Confirm, AppResources.ZZCancel);
                    if (result)
                    {
                        returnResult = true;
                        IsRefundNoMsgDisplayed = true;
                        IsRefundYesMsgDisplayed = false;
                    }
                    else
                    {
                        returnResult = false;
                    }
                }


                Task.Run(() =>
                {
                    IsLoading = true;
                });
            }
            else
            {
                returnResult = true;
            }
            return returnResult;
        }

        public async Task<bool> IsCheckedRefundGetorNotForYesMessage()
        {
            bool returnResult = false;
            string NetVAT = string.Empty;
            if (!string.IsNullOrEmpty(NetdueVat))
            {
                NetVAT = !NetdueVat.Contains(",") ? NetdueVat : NetdueVat.Replace(",", "");
            }
            if (Convert.ToDouble(NetVAT) <= 0 && IsVisibleDropdownForRefund == true && IsRefundVisible == true)
            {
                bool result;
                if (App.IsArabic)
                {
                    if (VATDeclarationData != null && VATDeclarationData.d != null && VATDeclarationData.d.GoliveFg == "X")
                    {
                        result = await Application.Current.MainPage.DisplayAlert(AppResources.ZZZConfirmationMsg, AppResources.ZZZRefundYesMsgForFiteenPercent, AppResources.ZZCancel, AppResources.Confirm);
                        if (!result)
                        {
                            returnResult = true;
                            IsRefundYesMsgDisplayed = true;
                            IsRefundNoMsgDisplayed = false;
                        }
                        else
                        {
                            returnResult = false;
                        }
                    }
                    else
                    {
                        result = await Application.Current.MainPage.DisplayAlert(AppResources.ZZZConfirmationMsg, AppResources.ZZZRefundYesMsg, AppResources.ZZCancel, AppResources.Confirm);
                        if (!result)
                        {
                            returnResult = true;
                            IsRefundYesMsgDisplayed = true;
                            IsRefundNoMsgDisplayed = false;
                        }
                        else
                        {
                            returnResult = false;
                        }
                    }
                }
                else
                {
                    if (VATDeclarationData != null && VATDeclarationData.d != null && VATDeclarationData.d.GoliveFg == "X")
                    {
                        result = await Application.Current.MainPage.DisplayAlert(AppResources.ZZZConfirmationMsg, AppResources.ZZZRefundYesMsgForFiteenPercent, AppResources.Confirm, AppResources.ZZCancel);
                        if (result)
                        {
                            returnResult = true;
                            IsRefundYesMsgDisplayed = true;
                            IsRefundNoMsgDisplayed = false;
                        }
                        else
                        {
                            returnResult = false;
                        }
                    }
                    else
                    {
                        result = await Application.Current.MainPage.DisplayAlert(AppResources.ZZZConfirmationMsg, AppResources.ZZZRefundYesMsg, AppResources.Confirm, AppResources.ZZCancel);
                        if (result)
                        {
                            returnResult = true;
                            IsRefundYesMsgDisplayed = true;
                            IsRefundNoMsgDisplayed = false;
                        }
                        else
                        {
                            returnResult = false;
                        }
                    }
                }

                Task.Run(() =>
                {
                    IsLoading = true;
                });
            }
            else
            {
                returnResult = true;
            }
            return returnResult;
        }

        //public async void ShowPdf(string pdfUrl)
        //{
        //    if (Device.RuntimePlatform == Device.iOS)
        //    {
        //        if (pdfUrl != null)
        //        {
        //            //Uri uri = new Uri(pdfUrl);
        //            //Device.OpenUri(uri);
        //            _navigationService.NavigateTo(App.PdfiOSView, pdfUrl);
        //        }
        //        else
        //        {
        //            //pop that certificate is not available
        //            MainThread.BeginInvokeOnMainThread(async () =>
        //            {
        //                await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
        //            });
        //        }
        //    }
        //    else
        //    {
        //        if (pdfUrl != null)
        //        {
        //            _navigationService.NavigateTo(App.PdfView, pdfUrl);
        //        }
        //        else
        //        {
        //            //pop that certificate is not available
        //            MainThread.BeginInvokeOnMainThread(async () =>
        //            {
        //                await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
        //            });
        //        }
        //    }
        //}
        public void VATReturnAddNote()
        {
            //  WebServiceManager.GAZTSetVATReturnAddNote(String.Empty);
            _navigationService.NavigateTo(App.AddNotePageView, VATDeclarationData);
        }
        public void VATReturnGetNotes()
        {
            //IsVisibleNotes = true;
            //ButtonName = AppResources.ZNote;
            //  WebServiceManager.GAZTSetVATReturnGetNotes(String.Empty);
            _navigationService.NavigateTo(App.DisplayNotesPageView, VATDeclarationData);
        }
        public void VATViewAttachments()
        {
            // ClearPage();
            // IsVisibleAttachments = true;
            //ButtonName = AppResources.Submit;
            _navigationService.NavigateTo(App.AttachmentPageView, VATDeclarationData);
        }
        public async Task VATSetReturnVoidAsync()
        {
            var answer = await Application.Current.MainPage.DisplayAlert(AppResources.Information, AppResources.ZZGeneralMessage_AllInfoFilledInTheFormWillBeLost, AppResources.ZYes, AppResources.ZNo);
            if (answer)
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    try
                    {
                        CreateDataForPost();
                        string operation = "04";// Passed 04 to set void
                        VATDeclarationData.d.Operationz = operation;
                        StepNumber = "01";
                        if (IsDeclarationCheckedForInstruction == true)
                        {
                            StepNumber = "02";
                        }
                        if (IsCheckedTaxPayerDetailsInfo == true)
                        {
                            StepNumber = "03";
                        }
                        if (IsDeclarationCheckedForSummary == true)
                        {
                            StepNumber = "04";
                        }
                        VATDeclarationData.d.StepNumber = StepNumber;
                        VATDeclarationData.d.UserTypz = "TP";
                        var response = WebServiceManager.GAZTSetVATReturnVoid(VATDeclarationData);
                        PopToRootPage();
                        var res = await SaveReturnAndGetReturnAndSetButtons();
                        if (res != null && res.d != null && response != null)
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await ManageEnabledAsyncProperty(false);
                                IsMainButtonVisible = false;
                                IsSwichButtonEnableToTap = false;
                                IsEnableIBAN = false;
                                IsEnableCheckedRefund = false;
                                IsEnableIBANType = false;
                                IsEnableIBANIdNumber = false;
                                IsMoreButtonEnabled = false;
                            });
                            // ManageEnabledProperty(false);
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_VATReturnFormCancelled, AppResources.ZInstructions);
                            });
                        }
                        else
                        {
                            IsLoading = false;
                            if (string.IsNullOrEmpty(WebServiceManager.ErrorMessageForVAT))
                            {
                                MainThread.BeginInvokeOnMainThread(async () =>
                                {
                                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                    _navigationService.GoBack();
                                });
                            }
                            else
                            {
                                MainThread.BeginInvokeOnMainThread(async () =>
                                {
                                    await _dialogService.ShowMessage(WebServiceManager.ErrorMessageForVAT, AppResources.Information);
                                    // _navigationService.GoBack();
                                    WebServiceManager.ErrorMessageForVAT = string.Empty;
                                });
                            }
                            //MainThread.BeginInvokeOnMainThread(async () =>
                            //{
                            //    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.ZInstructions);
                            //});
                        }
                    }
                    catch (InternetException ex)
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        });
                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
        }
        public async Task VATReturnResetAsync()
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });
            await Task.Run(async () =>
            {
                try
                {
                    CreateDataForPost();
                    string operation = "14";// Passed 14 to set RESET
                    VATDeclarationData.d.Operationz = operation;
                    StepNumber = "01";
                    if (IsDeclarationCheckedForInstruction == true)
                    {
                        StepNumber = "02";
                    }
                    if (IsCheckedTaxPayerDetailsInfo == true)
                    {
                        StepNumber = "03";
                    }
                    if (IsDeclarationCheckedForSummary == true)
                    {
                        StepNumber = "04";
                    }
                    VATDeclarationData.d.StepNumber = StepNumber;
                    VATDeclarationData.d.UserTypz = "TP";
                    var response = WebServiceManager.GAZTSetVATReturnReset(VATDeclarationData);
                    PopToRootPage();
                    var res = await SaveReturnAndGetReturnAndSetButtons();



                    if (res != null && res.d != null && response != null)
                    {

                        VATDeclaration _vATDeclaration = await WebServiceManager.GAZTGetVATReturns(App.Fbguid, VATDeclarationData.d.Fbnumz, App.EUser, "");
                        PopToRootPage();
                        if (_vATDeclaration != null && _vATDeclaration.d != null)
                        {
                            VATDeclarationData = _vATDeclaration;
                            ResponseVATDeclarationD = VATDeclarationData.d;

                            SetCommasforAll();
                            if (DummyATTACHSetsList != null && DummyATTACHSetsList.Count() != 0)
                            {
                                VATDeclarationData.d.ATTACHSet.results = DummyATTACHSetsList;
                            }
                            //SetData();
                        }


                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await ManageEnabledAsyncProperty(false);
                            IsMainButtonVisible = false;
                            IsSwichButtonEnableToTap = false;
                            IsEnableIBAN = false;
                            IsEnableCheckedRefund = false;
                            IsEnableIBANType = false;
                            IsEnableIBANIdNumber = false;
                            IsMoreButtonEnabled = false;
                        });
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            _dialogService.ShowMessage(AppResources.ZZGeneralMessage_ReturnRestoredToTheLastBilledVersion, AppResources.Information);
                        });
                    }
                    else
                    {
                        IsLoading = false;
                        if (string.IsNullOrEmpty(WebServiceManager.ErrorMessageForVAT))
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                _navigationService.GoBack();
                            });
                        }
                        else
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessage(WebServiceManager.ErrorMessageForVAT, AppResources.Information);
                                // _navigationService.GoBack();
                                WebServiceManager.ErrorMessageForVAT = string.Empty;
                            });
                        }
                        //MainThread.BeginInvokeOnMainThread(async () =>
                        //{
                        //    _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        //});
                    }
                }
                catch (InternetException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    });
                }
            });
            await Task.Run(() =>
            {
                IsLoading = false;
            });
        }

        public async void OpenIbanSet()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(() =>
                {
                    IsVisibleDropdownForRefund = true;
                    IsDropdownVisibleForIban = true;
                    IsVisiblechkRefundDeclaration = true;
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (Exception)
            {

            }
        }

        public async Task VATReturnAmendAsync()
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });
            await Task.Run(async () =>
            {
                string periodto = JsonConvert.DeserializeObject<DateTime>(@"""" + VATDeclarationData.d.Abrzo + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                TimeSpan TS = DateTime.Now - Convert.ToDateTime(periodto);
                double Years = TS.TotalDays / 365.25;
                if (Years >= 5)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_IfTimePeriodOfAmendmentIsLapsed, AppResources.Information);
                    });
                    return;
                }
                CreateDataForPost();
                string operation = "45";// Passed 45 to set for Amendment
                VATDeclarationData.d.Operationz = operation;
                StepNumber = "01";
                // ButtonName = AppResources.ZVatStepTwo;
                if (IsDeclarationCheckedForInstruction == true)
                {
                    StepNumber = "02";
                    // ButtonName = AppResources.ZVatStepThree;
                }
                if (IsCheckedTaxPayerDetailsInfo == true)
                {
                    StepNumber = "03";
                    // ButtonName = AppResources.ZVatStepFour;
                }
                if (IsDeclarationCheckedForSummary == true)
                {
                    StepNumber = "04";
                    //    ButtonName = AppResources.Submit;
                }
                VATDeclarationData.d.StepNumber = StepNumber;
                VATDeclarationData.d.UserTypz = "TP";
                // var response = WebServiceManager.GAZTSetVATReturnAmend(VATDeclarationData);
                PopToRootPage();
                var res = await SaveReturnAndGetReturnAndSetButtons();
                if (res != null && res.d != null)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await ManageEnabledAsyncProperty(true);
                        if ((App.ICRStatus == "E0045" || App.ICRStatus == "E0006") && VATDeclarationData.d.Yesno == "X")
                        {
                            IsEnableSwitchToggledFor15PercentChange = false;
                        }
                        else
                        {
                            IsEnableSwitchToggledFor15PercentChange = true;
                        }
                        IsDeclarationCheckEnabled = false;
                        IsTaxPayerCheckEnabled = false;
                        IsAmendClicked = true;
                        IsMainButtonEnabled = true;
                        IsMainButtonVisible = true;
                    });
                    //ManageEnabledProperty(true);
                }
                else
                {
                    IsLoading = false;
                    if (string.IsNullOrEmpty(WebServiceManager.ErrorMessageForVAT))
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                            _navigationService.GoBack();
                        });
                    }
                    else
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(WebServiceManager.ErrorMessageForVAT, AppResources.Information);
                            // _navigationService.GoBack();
                            WebServiceManager.ErrorMessageForVAT = string.Empty;
                        });
                    }
                    //MainThread.BeginInvokeOnMainThread(async () =>
                    //{
                    //    _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    //});
                }
            });
            await Task.Run(() =>
            {
                IsLoading = false;
            });
        }

        //public async void showInfoMessageForRefund()
        //{
        //    try
        //    {
        //        MainThread.BeginInvokeOnMainThread(async () =>
        //        {
        //            if (IsVisibleDropdownForRefund == false)
        //            {
        //                var result = await Application.Current.MainPage.DisplayAlert(AppResources.Information, AppResources.ZZZRefundEnableMessage, AppResources.ZZZOkayText, AppResources.ZZZCancelText);

        //                if (result)
        //                {

        //                }
        //                else
        //                {
        //                    IsSwichButtonEnable = false;
        //                }
        //            }

        //        });
        //    }
        //    catch(Exception)
        //    {

        //    }


        //}

        public void VATReturnDeleteAttachment()
        {
            _navigationService.NavigateTo("ICRListPageView");
        }
        public void DisableForRefund()
        {
            IsSwichButtonEnable = false;
            IsVisibleDropdownForRefund = false;
            IsVisiblechkRefundDeclaration = false;
            IsDropdownVisibleForIban = false;
            IbanNumberText = string.Empty;
            IsTextBoxVisibleForIban = false;
            SelectedIBAN = null;
            SelectedIBANType = null;
            IBANIDNumberList = null;
            SelectedIBANIDNumber = null;
            IsRefundVisible = false;
            IsCheckedRefund = false;
        }
        public void ClearPage()
        {
            //for Header
            IsTabbedMenuAvailable = true;
            IsVisibleCreditCarriedLabel = false;
            ///
            IsVisibleAcknowledgment = false;
            IsVisibleAttachments = false;
            IsVisibleInstrunction = false;
            IsVisibleNotes = false;
            IsVisibleSummary = false;
            IsVisibleTaxPayerDetails = false;
            IsVisibleVatReturnForm = false;
            IsVisibleCreditCarriedForward = false;
        }
        public void RateSetAsPerDate()
        {
            try
            {
                if (VATDeclarationData.d != null)
                {
                    if (VATDeclarationData.d.Abrzu != null && VATDeclarationData.d.Abrzo != null)
                    {
                        if (VATDeclarationData.d.IBANSet.results != null && VATDeclarationData.d.IBANSet.results.Count() != 0)
                        {
                            IBANList = new List<Result2>();
                            IBANList = VATDeclarationData.d.IBANSet.results;
                            IsVATRefunCheckedVisible = false;
                            IsEnableCheckedRefund = false;
                        }
                        else
                        {
                            IsVATRefunCheckedVisible = true;
                            IsEnableCheckedRefund = true;
                        }
                        createIBANType();
                        //DateTime startDate = new DateTime(2017, 1, 18);
                        //DateTime endDate = new DateTime(2018, 1, 18);
                        DateTime startDate = new DateTime();
                        DateTime endDate = new DateTime();
                        if (!string.IsNullOrEmpty(VATDeclarationData.d.Abrzu) && !string.IsNullOrEmpty(VATDeclarationData.d.Abrzo))
                        {
                            VATRateDataWithDateType vATRateDataWithDate;
                            VATRateDataWithStringDateType dataWithStringDateType = new VATRateDataWithStringDateType();
                            dataWithStringDateType.StartDate = VATDeclarationData.d.Abrzu;
                            dataWithStringDateType.EndDate = VATDeclarationData.d.Abrzo;
                            string JsonString = JsonConvert.SerializeObject(dataWithStringDateType);
                            vATRateDataWithDate = JsonConvert.DeserializeObject<VATRateDataWithDateType>(JsonString);
                            startDate = vATRateDataWithDate.StartDate;
                            endDate = vATRateDataWithDate.EndDate;
                        }
                        //DateTime startDate = DateTime.Parse(VATDeclarationData.d.Abrzu);
                        //DateTime endDate = DateTime.Parse(VATDeclarationData.d.Abrzo);
                        List<VATCalculationDataVATRSet> vATCalculationsforBegin = new List<VATCalculationDataVATRSet>();
                        List<VATCalculationDataVATRSet> vATCalculationsforEnd = new List<VATCalculationDataVATRSet>();
                        VATCalculationDataVATRSet vATCalculationDataDummy;
                        foreach (var item in CalculationRateSet)
                        {
                            if (startDate >= item.Begda)
                            {
                                vATCalculationDataDummy = new VATCalculationDataVATRSet();
                                vATCalculationDataDummy = item;
                                vATCalculationsforBegin.Add(vATCalculationDataDummy);
                            }
                        }
                        foreach (var item1 in vATCalculationsforBegin)
                        {
                            if (endDate <= item1.Endda)
                            {
                                vATCalculationDataDummy = new VATCalculationDataVATRSet();
                                vATCalculationDataDummy = item1;
                                vATCalculationsforEnd.Add(vATCalculationDataDummy);
                            }
                        }
                        VATCalculationDataVATRSet Rate002 = vATCalculationsforEnd.Where(x => x.Type == "002").FirstOrDefault();
                        if (Rate002 != null)
                        {
                            VATRate002 = Rate002.Penalty;
                        }
                        VATCalculationDataVATRSet Rate001 = vATCalculationsforEnd.Where(x => x.Type == "001").FirstOrDefault();
                        if (Rate001 != null)
                        {
                            VATRate001 = Rate001.Penalty;
                        }
                        VATCalculationDataVATRSet Rate003 = vATCalculationsforEnd.Where(x => x.Type == "003").FirstOrDefault();
                        if (Rate003 != null)
                        {
                            VATRate003 = Rate003.Penalty;
                        }
                        //VATCalculationDataVATRSet Rate00T1 = CalculationRateSet.Where(x => x.Begda <= startDate && x.Endda >= endDate).FirstOrDefault();
                        //VATCalculationDataVATRSet Rate002 = CalculationRateSet.Where(x => (x.Begda.Date >= startDate.Date) && (x.Endda.Date <= endDate.Date) && (x.Type== "002")).FirstOrDefault();
                        //if (Rate002 != null)
                        //{
                        //    VATRate002 = Rate002.Penalty;
                        //}
                        //VATCalculationDataVATRSet Rate001 = CalculationRateSet.Where(x => (x.Begda.Date >= startDate.Date) && (x.Endda.Date <= endDate.Date) && (x.Type == "001")).FirstOrDefault();
                        //if (Rate001 != null)
                        //{
                        //    VATRate001 = Rate001.Penalty;
                        //}
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        public void createIBANType()
        {
            IBANTypesList = new List<IBANType>();
            List<IBANType> IBANTypesDummyList = new List<IBANType>();
            IBANType iBANType = new IBANType();
            iBANType.key = "ZS0001";
            iBANType.Text = AppResources.ZIBANNationalID;
            IBANTypesDummyList.Add(iBANType);
            IBANType iBANType1 = new IBANType();
            iBANType1.key = "BUP002";
            iBANType1.Text = AppResources.ZIBANCommercialRegistrationID;
            IBANTypesDummyList.Add(iBANType1);
            IBANType iBANType2 = new IBANType();
            iBANType2.key = "ZS0005";
            iBANType2.Text = AppResources.ZIBANCompanyID;
            IBANTypesDummyList.Add(iBANType2);
            IBANTypesList = IBANTypesDummyList;
        }
        public async Task pageLoad()
        {
            try
            {
                IsFirstSubmission = true;
                IsSadadNumberVisible = false;
                IsAmendClicked = false;
                WebServiceManager.ErrorMessageForVAT = string.Empty;
                //await Task.Run(() =>
                //{
                //    IsLoading = true;
                //});
                //await Task.Run(async () =>
                //{
                VATCalculationData vATCalculationData;

                string periodKey = VATDeclarationData.d.Periodkeyz;
                string TxnTp = VATDeclarationData.d.TxnTpz;
                string status = VATDeclarationData.d.Statusz;
                if (status == "E057" || status == "E0057" || status == "E058" || status == "E0058")
                {
                    //MainThread.BeginInvokeOnMainThread(async () =>
                    //{
                    //    await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_ReturnUnderReviewWithGAZT, AppResources.Information);
                    //});

                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_ReturnUnderReviewWithGAZT));

                }
                string FormBundleNumber = VATDeclarationData.d.Fbnum;
                string Gpart = VATDeclarationData.d.Gpart;


                TaxpayerPeriodFromDate = JsonConvert.DeserializeObject<DateTime>(@"""" + VATDeclarationData.d.Abrzu + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                TaxpayerPeriodToDate = JsonConvert.DeserializeObject<DateTime>(@"""" + VATDeclarationData.d.Abrzo + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));

                if (VATDeclarationData.d.ATTACHSet.results != null && VATDeclarationData.d.ATTACHSet.results.Count != 0)
                {
                    ATTACHSetsList = new List<Attachment>();
                    foreach (var item in VATDeclarationData.d.ATTACHSet.results)
                    {
                        Attachment a = new Attachment();
                        a = item;
                        ATTACHSetsList.Add(a);
                    }
                }
                if (VATDeclarationData.d.CFSet.results != null && VATDeclarationData.d.ADRSet.results.Count != 0)
                {
                    CreditCarriedsList = VATDeclarationData.d.CFSet.results;
                }
                if (VATDeclarationData.d.ADRSet.results.Count > 0)
                {
                    FullAddress = VATDeclarationData.d.ADRSet.results[0].BuildingNo + " " + VATDeclarationData.d.ADRSet.results[0].Street + " " + VATDeclarationData.d.ADRSet.results[0].Quarter + " " + VATDeclarationData.d.ADRSet.results[0].RegionDesc + " " + VATDeclarationData.d.ADRSet.results[0].City + " " + Environment.NewLine + VATDeclarationData.d.ADRSet.results[0].PostalCd;
                }
                vATCalculationData = await WebServiceManager.GAZTGetVATDeclaratinCalculationData(periodKey, TxnTp, status, FormBundleNumber, Gpart);
                PopToRootPage();
                if (vATCalculationData.d != null)
                {
                    //      if(vATCalculationData.d.)
                    if (vATCalculationData.d.VATRSet.results.Count != 0)
                    {
                        CalculationRateSet = new List<VATCalculationDataVATRSet>();
                        CalculationRateSet = vATCalculationData.d.VATRSet.results;
                        CalculationRateSetVTTH = new List<VTTHSetResult>();
                        CalculationRateSetVTTH = vATCalculationData.d.VTTHSet.results;
                        CalculationRateIGRTSet = new List<IGRTSetResult>();
                        CalculationRateIGRTSet = vATCalculationData.d.IGRTSet.results;
                        RateSetAsPerDate();
                    }
                    if (vATCalculationData.d.VTTHSet.results.Count != 0)
                    {
                        CorrectionPeriodAmount = vATCalculationData.d.VTTHSet.results.Where(x => x.Type == "001").Select(x => x.MaxVal).FirstOrDefault();
                        CorrectionNegativePeriodAmount = vATCalculationData.d.VTTHSet.results.Where(x => x.Type == "001").Select(x => x.MinVal).FirstOrDefault();
                        if (!string.IsNullOrEmpty(CorrectionPeriodAmount))
                        {
                            if (App.IsArabic)
                            {
                                CarriedValueString = AppResources.ZVatCorrectionsfrompreviousperiod.Replace("±", CorrectionPeriodAmount + " ± ");
                            }
                            else
                            {
                                CarriedValueString = AppResources.ZVatCorrectionsfrompreviousperiod.Replace("±", " ± " + CorrectionPeriodAmount);
                            }
                        }
                    }
                }
                if (VATDeclarationData.d.TcFg == "1")
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        IsDeclarationCheckedForInstruction = true;
                    });
                }
                if (VATDeclarationData.d.ConfStp2 == "1")
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        IsCheckedTaxPayerDetailsInfo = true;
                    });
                }
                if (VATDeclarationData.d.DecFg == "1")
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        IsDeclarationCheckedForSummary = true;
                    });
                }
                List<VATDeclarationTabbedPageName> vatTabbedList = new List<VATDeclarationTabbedPageName>();
                VatTabbledPageList = new List<VATDeclarationTabbedPageName>();
                VATDeclarationTabbedPageName s = new VATDeclarationTabbedPageName();
                if (!App.IsArabic)
                {
                    s.pageName = "Instruction";
                }
                else
                {
                    s.pageName = "التعليمات";
                }
                vatTabbedList.Add(s);
                VATDeclarationTabbedPageName s1 = new VATDeclarationTabbedPageName();
                if (!App.IsArabic)
                {
                    s1.pageName = "TaxPayer Details";
                }
                else
                {
                    s1.pageName = "تفاصيل المكلف";
                }
                vatTabbedList.Add(s1);
                VATDeclarationTabbedPageName s2 = new VATDeclarationTabbedPageName();
                if (!App.IsArabic)
                {
                    s2.pageName = "VAT Return Form";
                }
                else
                {
                    s2.pageName = "نموذج الإقرار الضريبي";
                }
                vatTabbedList.Add(s2);
                VATDeclarationTabbedPageName s3 = new VATDeclarationTabbedPageName();
                if (!App.IsArabic)
                {
                    s3.pageName = "Summary";
                }
                else
                {
                    s3.pageName = "ملخص";
                }
                vatTabbedList.Add(s3);
                //if(App.IsArabic)
                //    {
                //        vatTabbedList.Add(s3);
                //        vatTabbedList.Add(s2);
                //        vatTabbedList.Add(s1);
                //        vatTabbedList.Add(s);
                //    }
                //else
                //    {
                //        vatTabbedList.Add(s);
                //        vatTabbedList.Add(s1);
                //        vatTabbedList.Add(s2);
                //        vatTabbedList.Add(s3);
                //    }
                VatTabbledPageList = vatTabbedList;
                if (App.ICRStatus == "E0001" || App.ICRStatus == "E0045" || App.ICRStatus == "E0006" || App.ICRStatus == "E0058" || App.ICRStatus == "E0055")
                {
                    //  InstrunctionClicked();
                    SelectedIndex = 0;
                    PageSelectedItem = VatTabbledPageList[0];
                }
                ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(VATDeclarationData.d.ATTACHSet.results as List<Attachment>);
                VatAttachmentsList = myCollection;
                if (VATDeclarationData != null)
                {
                    // SetPageForDraft();
                    if (VATDeclarationData.d != null)
                    {
                        ResponseVATDeclarationD = VATDeclarationData.d;
                        if (ResponseVATDeclarationD.GoliveFg == "X")
                        {
                            VATNewModelFor15Percent = VATDeclarationData.d.VATPERITEMSet.results.Where(x => x.Type == "002").FirstOrDefault();
                            VATNewModelFor5Percent = VATDeclarationData.d.VATPERITEMSet.results.Where(x => x.Type == "003").FirstOrDefault();
                        }
                        SetData();
                        SetCommasforAll();
                    }
                    if (VATDeclarationData.d.NOTESSet.results != null && VATDeclarationData.d.NOTESSet.results.Count() != 0)
                    {
                        ResponseNote = VATDeclarationData.d.NOTESSet.results;
                    }
                    if (VATDeclarationData.d.VATR_MSGSet.results != null && VATDeclarationData.d.VATR_MSGSet.results.Count() != 0)
                    {
                        Responseobject = VATDeclarationData.d.VATR_MSGSet.results;
                    }
                    if (VATDeclarationData.d.IBANSet.results != null && VATDeclarationData.d.IBANSet.results.Count() != 0)
                    {
                        ResponseIBANSET = VATDeclarationData.d.IBANSet.results;
                    }
                    if (VATDeclarationData.d.CFSet.results != null && VATDeclarationData.d.CFSet.results.Count() != 0)
                    {
                        ResponseCFSET = VATDeclarationData.d.CFSet.results;
                    }
                    if (VATDeclarationData.d.ATTACHSet.results != null && VATDeclarationData.d.ATTACHSet.results.Count() != 0)
                    {
                        ResponseAttachSet = VATDeclarationData.d.ATTACHSet.results;
                    }
                    if (VATDeclarationData.d.ADRSet.results != null && VATDeclarationData.d.ADRSet.results.Count() != 0)
                    {
                        ResponseAddressSET = VATDeclarationData.d.ADRSet.results;
                    }
                }
                
                ManageThePreperiodcorrSwitch();
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                  await  _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
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
                if (_ListOfActionButtonsApplicable != null && _ListOfActionButtonsApplicable.Count() != 0)
                {
                    OnMoreOptionsEnabled = true;
                }
                else
                {
                    OnMoreOptionsEnabled = false;
                }
                RaisePropertyChanged("ListOfActionButtonsApplicable");
            }
        }
        private List<string> _DummyListOfActionButtonsApplicable;
        public List<string> DummyListOfActionButtonsApplicable
        {
            get
            {
                return _DummyListOfActionButtonsApplicable;
            }
            set
            {
                _DummyListOfActionButtonsApplicable = value;
                RaisePropertyChanged("DummyListOfActionButtonsApplicable");
            }
        }
        private bool _isMoreButtonEnabled;
        public bool IsMoreButtonEnabled
        {
            get
            {
                return _isMoreButtonEnabled;
            }
            set
            {
                _isMoreButtonEnabled = value;
                RaisePropertyChanged("IsMoreButtonEnabled");
            }
        }
        private List<string> _ActualListOfActionButtonsApplicable;
        public List<string> ActualListOfActionButtonsApplicable
        {
            get
            {
                return _ActualListOfActionButtonsApplicable;
            }
            set
            {
                _ActualListOfActionButtonsApplicable = value;
                RaisePropertyChanged("ActualListOfActionButtonsApplicable");
            }
        }
        private async Task<VATDeclaration> SaveReturnAndGetReturnAndSetButtons()
        {
            try
            {
                //New code for VAT 15% Change

                if (IsYesChecked == true)
                {
                    VATDeclarationData.d.Yesno = "X";
                }
                else
                {
                    VATDeclarationData.d.Yesno = string.Empty;
                }




                if (IsDeclarationCheckedForSummary == true)
                {
                    VATDeclarationData.d.DecFg = "1";
                }
                else
                {
                    VATDeclarationData.d.DecFg = "0";
                }
                if (IschkRefundDeclaration)
                {
                    VATDeclarationData.d.TcFlg = "1";
                }
                else
                {
                    VATDeclarationData.d.TcFlg = "0";
                }
                if (IsCheckedRefund)
                {
                    VATDeclarationData.d.IbanCb = "1";
                }
                else
                {
                    VATDeclarationData.d.IbanCb = "0";
                }
                if (VATDeclarationData != null && VATDeclarationData.d != null && VATDeclarationData.d.ATTACHSet.results.Count() != 0)
                {
                    DummyATTACHSetsList = new List<Attachment>();
                    DummyATTACHSetsList = VATDeclarationData.d.ATTACHSet.results;
                }
                if (ATTACHSetsList != null && ATTACHSetsList.Count() != 0)
                {
                    VATDeclarationData.d.ATTACHSet.results = ATTACHSetsList;
                }
                VATDeclaration response = await WebServiceManager.SaveVATDeclarationData(VATDeclarationData);
                PopToRootPage();
                if (response != null && response.d != null && !string.IsNullOrEmpty(response.d.Fbnum))
                {
                    try
                    {
                        if (response != null && response.d != null)
                        {
                            VATDeclarationData = response;
                            ResponseVATDeclarationD = VATDeclarationData.d;
                            if (VATDeclarationData.d.VATPERITEMSet.results != null)
                            {
                                VATNewModelFor15Percent = VATDeclarationData.d.VATPERITEMSet.results.Where(x => x.Type == "002").FirstOrDefault();
                                VATNewModelFor5Percent = VATDeclarationData.d.VATPERITEMSet.results.Where(x => x.Type == "003").FirstOrDefault();
                            }
                            SetCommasforAll();
                            if (DummyATTACHSetsList != null && DummyATTACHSetsList.Count() != 0)
                            {
                                VATDeclarationData.d.ATTACHSet.results = DummyATTACHSetsList;
                            }
                            if (VATDeclarationData.d.Operationz == "01" && App.ICRStatus == "E0001")
                            {
                                App.ICRStatus = "E0013";
                            }
                            //VATDeclaration _vATDeclaration = await WebServiceManager.GAZTGetVATReturns(VATDeclarationData.d.ReturnIdz, VATDeclarationData.d.Fbnumz, ICRListPageViewModel.EUser,"");
                            //if (_vATDeclaration != null && _vATDeclaration.d != null)
                            //{
                            //    VATDeclarationData = _vATDeclaration;
                            //    ResponseVATDeclarationD = VATDeclarationData.d;
                            //    SetData();
                            //}
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await ManageEnabledAsyncProperty(true);
                            });
                        }
                        await SetButtons(VATDeclarationData);
                        return response;
                    }
                    catch (Exception)
                    {


                        return null;
                    }
                }
                return response;
            }
            catch (InternetException )
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }
        #region CalculationPart
        public void CreateDataForPost()
        {
            try
            {
                Result6 VAT15percentmodel = VATNewModelFor15Percent;
                Result6 VAT5percentmodel = VATNewModelFor5Percent;
                VATDeclarationD vATDeclarationD = SetDataForPost(ResponseVATDeclarationD);
                vATDeclarationD = SetRemainingData(vATDeclarationD);

                //new Vat15% code
                if (IsFifteenPercentChange)
                {
                    vATDeclarationD = Set15PercentChangeData(vATDeclarationD);
                    vATDeclarationD = Set5PercentChangeData(vATDeclarationD);
                }

                VATDeclarationData.d.TotalsalesAmt = vATDeclarationD.TotalsalesAmt;
                VATDeclarationData.d.TotalsalesAdj = vATDeclarationD.TotalsalesAdj;
                VATDeclarationData.d.TotalpurchaseAmt = vATDeclarationD.TotalpurchaseAmt;
                VATDeclarationData.d.TotalpurchaseAdj = vATDeclarationD.TotalpurchaseAdj;
                VATDeclarationData.d.StdsalesVat = vATDeclarationD.StdsalesVat;
                VATDeclarationData.d.TotalsalesVat = vATDeclarationD.TotalsalesVat;
                VATDeclarationData.d.StdpurchasesVat = vATDeclarationD.StdpurchasesVat;
                VATDeclarationData.d.ImportspaidVat = vATDeclarationD.ImportspaidVat;
                VATDeclarationData.d.ImportsaccVat = vATDeclarationD.ImportsaccVat;
                VATDeclarationData.d.TotalpurchaseVat = vATDeclarationD.TotalpurchaseVat;
                VATDeclarationData.d.TotaldueVat = vATDeclarationD.TotaldueVat;
                VATDeclarationData.d.Preperiodcorr = vATDeclarationD.Preperiodcorr;
                VATDeclarationData.d.CreditVat = vATDeclarationD.CreditVat;
                VATDeclarationData.d.NetdueVat = vATDeclarationD.NetdueVat;
                if (IsVisibleDropdownForRefund == true)
                {
                    VATDeclarationData.d.RefundFg = "1";
                    if (IsCheckedRefund == true)
                    {
                        VATDeclarationData.d.Iban = IbanNumberText;
                        VATDeclarationData.d.IbanCb = "1";
                    }
                    else
                    {
                        if (SelectedIBAN != null)
                        {
                            VATDeclarationData.d.Iban = SelectedIBAN.Iban;
                            VATDeclarationData.d.IbanCb = "0";
                        }
                    }
                    if (SelectedIBANType != null)
                    {
                        VATDeclarationData.d.Idtype = SelectedIBANType.key;
                    }
                    if (SelectedIBANIDNumber != null)
                    {
                        VATDeclarationData.d.Idnum = SelectedIBANIDNumber.Idnumber;
                    }
                }
                else
                {
                    VATDeclarationData.d.RefundFg = "0";
                }
            }
            catch (Exception)
            {


            }
        }
        public VATDeclarationD Set15PercentChangeData(VATDeclarationD vATDeclarationD)
        {
            try
            {
                if (VATNewModelFor15Percent != null && vATDeclarationD != null)
                {

                    foreach (var vat15model in vATDeclarationD.VATPERITEMSet.results)
                    {
                        if (vat15model.Type == "002")
                        {
                            vat15model.DataVersion = VATNewModelFor15Percent.DataVersion;
                            vat15model.FormGuid = VATNewModelFor15Percent.FormGuid;
                            vat15model.ImportsaccAdj = VATNewModelFor15Percent.ImportsaccAdj.Replace(",", "");
                            vat15model.ImportsaccAmt = VATNewModelFor15Percent.ImportsaccAmt.Replace(",", "");
                            vat15model.ImportsaccVat = ImportsaccVat15.Replace(",", "");
                            vat15model.ImportspaidAdj = VATNewModelFor15Percent.ImportspaidAdj.Replace(",", "");
                            vat15model.ImportspaidAmt = VATNewModelFor15Percent.ImportspaidAmt.Replace(",", "");
                            vat15model.ImportspaidVat = ImportspaidVat15.Replace(",", "");
                            vat15model.LineNo = VATNewModelFor15Percent.LineNo;
                            vat15model.RankingOrder = VATNewModelFor15Percent.RankingOrder;
                            vat15model.Rate = VATNewModelFor15Percent.Rate;
                            vat15model.ReturnId = VATNewModelFor15Percent.ReturnId;
                            vat15model.StdpurchaseAdj = VATNewModelFor15Percent.StdpurchaseAdj.Replace(",", "");
                            vat15model.StdpurchaseAmt = VATNewModelFor15Percent.StdpurchaseAmt.Replace(",", "");
                            vat15model.StdpurchasesVat = StdpurchasesVat15.Replace(",", "");
                            vat15model.StdsalesAdj = VATNewModelFor15Percent.StdsalesAdj.Replace(",", "");
                            vat15model.StdsalesAmt = VATNewModelFor15Percent.StdsalesAmt.Replace(",", "");
                            vat15model.StdsalesVat = StdsalesVat15.Replace(",", "");
                            vat15model.TimestampCh = VATNewModelFor15Percent.TimestampCh;
                            vat15model.TimestampCr = VATNewModelFor15Percent.TimestampCr;
                            vat15model.Type = VATNewModelFor15Percent.Type;
                            vat15model.Waers = VATNewModelFor15Percent.Waers;
                            vat15model.__metadata = VATNewModelFor15Percent.__metadata;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return vATDeclarationD;
        }

        public VATDeclarationD Set5PercentChangeData(VATDeclarationD vATDeclarationD)
        {
            try
            {
                if (VATNewModelFor5Percent != null && vATDeclarationD != null)
                {

                    foreach (var vat15model in vATDeclarationD.VATPERITEMSet.results)
                    {
                        if (vat15model.Type == "003")
                        {
                            vat15model.DataVersion = VATNewModelFor5Percent.DataVersion;
                            vat15model.FormGuid = VATNewModelFor5Percent.FormGuid;
                            vat15model.ImportsaccAdj = VATNewModelFor5Percent.ImportsaccAdj.Replace(",", "");
                            vat15model.ImportsaccAmt = VATNewModelFor5Percent.ImportsaccAmt.Replace(",", "");
                            vat15model.ImportsaccVat = ImportsaccVat5.Replace(",", "");
                            vat15model.ImportspaidAdj = VATNewModelFor5Percent.ImportspaidAdj.Replace(",", "");
                            vat15model.ImportspaidAmt = VATNewModelFor5Percent.ImportspaidAmt.Replace(",", "");
                            vat15model.ImportspaidVat = ImportspaidVat5.Replace(",", "");
                            vat15model.LineNo = VATNewModelFor5Percent.LineNo;
                            vat15model.RankingOrder = VATNewModelFor5Percent.RankingOrder;
                            vat15model.Rate = VATNewModelFor5Percent.Rate;
                            vat15model.ReturnId = VATNewModelFor5Percent.ReturnId;
                            vat15model.StdpurchaseAdj = VATNewModelFor5Percent.StdpurchaseAdj.Replace(",", "");
                            vat15model.StdpurchaseAmt = VATNewModelFor5Percent.StdpurchaseAmt.Replace(",", "");
                            vat15model.StdpurchasesVat = StdpurchasesVat5.Replace(",", "");
                            vat15model.StdsalesAdj = VATNewModelFor5Percent.StdsalesAdj.Replace(",", "");
                            vat15model.StdsalesAmt = VATNewModelFor5Percent.StdsalesAmt.Replace(",", "");
                            vat15model.StdsalesVat = StdsalesVat5.Replace(",", "");
                            vat15model.TimestampCh = VATNewModelFor5Percent.TimestampCh;
                            vat15model.TimestampCr = VATNewModelFor5Percent.TimestampCr;
                            vat15model.Type = VATNewModelFor5Percent.Type;
                            vat15model.Waers = VATNewModelFor5Percent.Waers;
                            vat15model.__metadata = VATNewModelFor5Percent.__metadata;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return vATDeclarationD;
        }

        public VATDeclarationD SetRemainingData(VATDeclarationD vATDeclarationD)
        {
            if (!string.IsNullOrEmpty(vATDeclarationD.StdsalesAmt))
            {
                vATDeclarationD.StdsalesAmt = vATDeclarationD.StdsalesAmt.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.StdsalesAdj))
            {
                vATDeclarationD.StdsalesAdj = vATDeclarationD.StdsalesAdj.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.SalesGccAmt))
            {
                vATDeclarationD.SalesGccAmt = vATDeclarationD.SalesGccAmt.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.SalesGccAdj))
            {
                vATDeclarationD.SalesGccAdj = vATDeclarationD.SalesGccAdj.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.ZerosalesAmt))
            {
                vATDeclarationD.ZerosalesAmt = vATDeclarationD.ZerosalesAmt.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.ZerosalesAdj))
            {
                vATDeclarationD.ZerosalesAdj = vATDeclarationD.ZerosalesAdj.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.ExportsAmt))
            {
                vATDeclarationD.ExportsAmt = vATDeclarationD.ExportsAmt.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.ExportsAdj))
            {
                vATDeclarationD.ExportsAdj = vATDeclarationD.ExportsAdj.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.ExemptsalesAmt))
            {
                vATDeclarationD.ExemptsalesAmt = vATDeclarationD.ExemptsalesAmt.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.ExemptsalesAdj))
            {
                vATDeclarationD.ExemptsalesAdj = vATDeclarationD.ExemptsalesAdj.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.StdpurchaseAmt))
            {
                vATDeclarationD.StdpurchaseAmt = vATDeclarationD.StdpurchaseAmt.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.StdpurchaseAdj))
            {
                vATDeclarationD.StdpurchaseAdj = vATDeclarationD.StdpurchaseAdj.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.ImportspaidAmt))
            {
                vATDeclarationD.ImportspaidAmt = vATDeclarationD.ImportspaidAmt.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.ImportspaidAdj))
            {
                vATDeclarationD.ImportspaidAdj = vATDeclarationD.ImportspaidAdj.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.ImportsaccAmt))
            {
                vATDeclarationD.ImportsaccAmt = vATDeclarationD.ImportsaccAmt.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.ImportsaccAdj))
            {
                vATDeclarationD.ImportsaccAdj = vATDeclarationD.ImportsaccAdj.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.ZeropurchaseAmt))
            {
                vATDeclarationD.ZeropurchaseAmt = vATDeclarationD.ZeropurchaseAmt.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.ZeropurchaseAdj))
            {
                vATDeclarationD.ZeropurchaseAdj = vATDeclarationD.ZeropurchaseAdj.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.ExemptpurchaseAmt))
            {
                vATDeclarationD.ExemptpurchaseAmt = vATDeclarationD.ExemptpurchaseAmt.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.ExemptpurchaseAdj))
            {
                vATDeclarationD.ExemptpurchaseAdj = vATDeclarationD.ExemptpurchaseAdj.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.Preperiodcorr))
            {
                vATDeclarationD.Preperiodcorr = vATDeclarationD.Preperiodcorr.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(vATDeclarationD.StdsalesAmt))
            {
                vATDeclarationD.StdsalesAmt = vATDeclarationD.StdsalesAmt.Replace(",", "");
            }
            return vATDeclarationD;
        }
        public VATDeclarationD SetDataForPost(VATDeclarationD vATDeclarationD)
        {
            try
            {
                if (!string.IsNullOrEmpty(TotalsalesAmt))
                {
                    vATDeclarationD.TotalsalesAmt = !TotalsalesAmt.Contains(",") ? TotalsalesAmt : TotalsalesAmt.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(TotalsalesAdj))
                {
                    vATDeclarationD.TotalsalesAdj = !TotalsalesAdj.Contains(",") ? TotalsalesAdj : TotalsalesAdj.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(TotalpurchaseAmt))
                {
                    vATDeclarationD.TotalpurchaseAmt = !TotalpurchaseAmt.Contains(",") ? TotalpurchaseAmt : TotalpurchaseAmt.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(TotalpurchaseAdj))
                {
                    vATDeclarationD.TotalpurchaseAdj = !TotalpurchaseAdj.Contains(",") ? TotalpurchaseAdj : TotalpurchaseAdj.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(StdsalesVat))
                {
                    vATDeclarationD.StdsalesVat = !StdsalesVat.Contains(",") ? StdsalesVat : StdsalesVat.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(TotalsalesVat))
                {
                    vATDeclarationD.TotalsalesVat = !TotalsalesVat.Contains(",") ? TotalsalesVat : TotalsalesVat.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(StdpurchasesVat))
                {
                    vATDeclarationD.StdpurchasesVat = !StdpurchasesVat.Contains(",") ? StdpurchasesVat : StdpurchasesVat.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(ImportspaidVat))
                {
                    vATDeclarationD.ImportspaidVat = !ImportspaidVat.Contains(",") ? ImportspaidVat : ImportspaidVat.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(ImportsaccVat))
                {
                    vATDeclarationD.ImportsaccVat = !ImportsaccVat.Contains(",") ? ImportsaccVat : ImportsaccVat.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(TotalpurchaseVat))
                {
                    vATDeclarationD.TotalpurchaseVat = !TotalpurchaseVat.Contains(",") ? TotalpurchaseVat : TotalpurchaseVat.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(TotaldueVat))
                {
                    vATDeclarationD.TotaldueVat = !TotaldueVat.Contains(",") ? TotaldueVat : TotaldueVat.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Preperiodcorr))
                {
                    vATDeclarationD.Preperiodcorr = !Preperiodcorr.Contains(",") ? Preperiodcorr : Preperiodcorr.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(CreditVat))
                {
                    vATDeclarationD.CreditVat = !CreditVat.Contains(",") ? CreditVat : CreditVat.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(NetdueVat))
                {
                    vATDeclarationD.NetdueVat = !NetdueVat.Contains(",") ? NetdueVat : NetdueVat.Replace(",", "");
                }




            }
            catch (Exception)
            {


            }
            return vATDeclarationD;
        }
        //public void SetPageForDraft()
        //{
        //    if (!string.IsNullOrEmpty(VATDeclarationData.d.StepNumber))
        //    {
        //        if (VATDeclarationData.d.StepNumber == "00")
        //        {
        //            ClearPage();
        //            IsDeclarationCheckedForInstruction = false;
        //            IsVisibleInstrunction = true;
        //            PageSelectedItem = VatTabbledPageList[0];
        //        }
        //        if (VATDeclarationData.d.StepNumber == "01")
        //        {
        //            ClearPage();
        //            IsDeclarationCheckedForInstruction = false;
        //            IsVisibleInstrunction = true;
        //            PageSelectedItem = VatTabbledPageList[0];
        //        }
        //        else if (VATDeclarationData.d.StepNumber == "02")
        //        {
        //            ClearPage();
        //            IsDeclarationCheckedForInstruction = true;
        //            IsVisibleTaxPayerDetails = true;
        //            PageSelectedItem = VatTabbledPageList[1];
        //        }
        //        else if (VATDeclarationData.d.StepNumber == "03")
        //        {
        //            ClearPage();
        //            IsDeclarationCheckedForInstruction = true;
        //            IsCheckedTaxPayerDetailsInfo = true;
        //            IsVisibleVatReturnForm = true;
        //            PageSelectedItem = VatTabbledPageList[2];
        //        }
        //        else if (VATDeclarationData.d.StepNumber == "04")
        //        {
        //            ClearPage();
        //            IsDeclarationCheckedForInstruction = true;
        //            IsCheckedTaxPayerDetailsInfo = true;
        //            IsDeclarationCheckedForSummary = false;
        //            IsVisibleVatReturnForm = true;
        //            PageSelectedItem = VatTabbledPageList[3];
        //        }
        //    }
        //}
        public void SetData()
        {
            TotalsalesAmt = ResponseVATDeclarationD.TotalsalesAmt;
            TotalsalesAdj = ResponseVATDeclarationD.TotalsalesAdj;
            TotalpurchaseAmt = ResponseVATDeclarationD.TotalpurchaseAmt;
            TotalpurchaseAdj = ResponseVATDeclarationD.TotalpurchaseAdj;
            StdsalesVat = ResponseVATDeclarationD.StdsalesVat;
            TotalsalesVat = ResponseVATDeclarationD.TotalsalesVat;
            StdpurchasesVat = ResponseVATDeclarationD.StdpurchasesVat;
            ImportspaidVat = ResponseVATDeclarationD.ImportspaidVat;
            ImportsaccVat = ResponseVATDeclarationD.ImportsaccVat;
            TotalpurchaseVat = ResponseVATDeclarationD.TotalpurchaseVat;
            TotaldueVat = ResponseVATDeclarationD.TotaldueVat;
            Preperiodcorr = ResponseVATDeclarationD.Preperiodcorr;
            CreditVat = ResponseVATDeclarationD.CreditVat;
            NetdueVat = ResponseVATDeclarationD.NetdueVat;
        }
        public async Task NavigationSetupForDraft()
        {
            if (VATDeclarationData.d.StepNumber == "01" || VATDeclarationData.d.StepNumber == "1" || VATDeclarationData.d.StepNumber == "0" || VATDeclarationData.d.StepNumber == "00")
            {
                //InstrunctionClicked();
                SelectedIndex = 0;
                PageSelectedItem = VatTabbledPageList[0];
            }
            else if (VATDeclarationData.d.StepNumber == "02" || VATDeclarationData.d.StepNumber == "2")
            {
                // TaxpayerDetailsClicked();
                SelectedIndex = 1;
                PageSelectedItem = VatTabbledPageList[1];
            }
            else if (VATDeclarationData.d.StepNumber == "03" || VATDeclarationData.d.StepNumber == "3")
            {
                //VATReturnFormClicked();
                SelectedIndex = 2;
                PageSelectedItem = VatTabbledPageList[2];
            }
            else if (VATDeclarationData.d.StepNumber == "04" || VATDeclarationData.d.StepNumber == "4")
            {



                //SummaryClicked();
                SelectedIndex = 3;
                PageSelectedItem = VatTabbledPageList[3];
            }
        }
        private void SetNoteData()
        {
            VATDeclarationData.d.NOTESSet.results[0].Strline = NoteText;
        }
        public string StandardRatedSalesVatAmount(string Amount, string Adjustment)
        {
            string VATAmount = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(Amount) && Amount.Contains(","))
                {
                    Amount = Amount.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment) && Adjustment.Contains(","))
                {
                    Adjustment = Adjustment.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount) && !string.IsNullOrEmpty(Adjustment) && Amount != "." && Adjustment != ".")
                {
                    if (!Amount.Contains("-") && !Adjustment.Contains("-"))
                    {
                        double dAmount = string.IsNullOrEmpty(Amount) ? 0 : Convert.ToDouble(Amount);
                        double dAdjustment = string.IsNullOrEmpty(Adjustment) ? 0 : Convert.ToDouble(Adjustment);
                        double dVATRate = Convert.ToDouble(VATRate002);
                        VATAmount = Convert.ToDouble((dAmount - dAdjustment) * dVATRate / 100).ToString();
                        if (VATAmount == "0")
                        {
                            VATAmount = "0.00";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(VATAmount) && VATAmount != "0.00")
                {
                    VATAmount = Math.Round(Convert.ToDecimal(VATAmount), 2).ToString();
                    VATAmount = UtilityManager.GetCommaSeparatedAmount(VATAmount);
                }
                bool isTrue = IsTextNullOrEmpty(VATAmount);
                VATAmount = isTrue ? "0.00" : VATAmount;
                return VATAmount;
            }
            catch (Exception)
            {


            }
            return VATAmount;
        }


        public string StandardRatedSalesVatAmountForNewChangeRate(string Amount, string Adjustment, string VatRate)
        {
            string VATAmount = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(Amount) && Amount.Contains(","))
                {
                    Amount = Amount.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment) && Adjustment.Contains(","))
                {
                    Adjustment = Adjustment.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount) && !string.IsNullOrEmpty(Adjustment) && !string.IsNullOrEmpty(VatRate) && Amount != "." && Adjustment != ".")
                {
                    if (!Amount.Contains("-") && !Adjustment.Contains("-"))
                    {
                        double dAmount = string.IsNullOrEmpty(Amount) ? 0 : Convert.ToDouble(Amount);
                        double dAdjustment = string.IsNullOrEmpty(Adjustment) ? 0 : Convert.ToDouble(Adjustment);
                        double dVATRate = Convert.ToDouble(VatRate);
                        VATAmount = Convert.ToDouble((dAmount - dAdjustment) * dVATRate / 100).ToString();
                        if (VATAmount == "0")
                        {
                            VATAmount = "0.00";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(VATAmount) && VATAmount != "0.00")
                {
                    VATAmount = Math.Round(Convert.ToDecimal(VATAmount), 2).ToString();
                    VATAmount = UtilityManager.GetCommaSeparatedAmount(VATAmount);
                }
                bool isTrue = IsTextNullOrEmpty(VATAmount);
                VATAmount = isTrue ? "0.00" : VATAmount;
                return VATAmount;
            }
            catch (Exception)
            {


            }
            return VATAmount;
        }


        public string TotalAmountForSixVar(string Amount1, string Amount2, string Amount3, string Amount4, string Amount5, string Amount6)
        {
            string TotalAmount = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(Amount1) && Amount1.Contains(","))
                {
                    Amount1 = Amount1.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount2) && Amount2.Contains(","))
                {
                    Amount2 = Amount2.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount3) && Amount3.Contains(","))
                {
                    Amount3 = Amount3.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount4) && Amount4.Contains(","))
                {
                    Amount4 = Amount4.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount5) && Amount5.Contains(","))
                {
                    Amount5 = Amount5.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount6) && Amount6.Contains(","))
                {
                    Amount6 = Amount6.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount1) && !string.IsNullOrEmpty(Amount2) && !string.IsNullOrEmpty(Amount3) && !string.IsNullOrEmpty(Amount4) && !string.IsNullOrEmpty(Amount5) && !string.IsNullOrEmpty(Amount6))
                {
                    if (Amount1 != "." && Amount2 != "." && Amount3 != "." && Amount4 != "." && Amount5 != "." && Amount6 != ".")
                    {
                        if (!Amount1.Contains("-") && !Amount2.Contains("-") && !Amount3.Contains("-") && !Amount4.Contains("-") && !Amount5.Contains("-") && !Amount6.Contains("-"))
                        {
                            TotalAmount = Convert.ToDouble((string.IsNullOrEmpty(Amount1) ? 0.00 : Convert.ToDouble(Amount1)) + (string.IsNullOrEmpty(Amount2) ? 0.00 : Convert.ToDouble(Amount2)) + (string.IsNullOrEmpty(Amount3) ? 0.00 : Convert.ToDouble(Amount3)) + (string.IsNullOrEmpty(Amount4) ? 0.00 : Convert.ToDouble(Amount4)) + (string.IsNullOrEmpty(Amount5) ? 0.00 : Convert.ToDouble(Amount5)) + (string.IsNullOrEmpty(Amount6) ? 0.00 : Convert.ToDouble(Amount6))).ToString();
                            if (TotalAmount == "0")
                            {
                                TotalAmount = "0.00";
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
                {
                    TotalAmount = Math.Round(Convert.ToDecimal(TotalAmount), 2).ToString();
                    TotalAmount = UtilityManager.GetCommaSeparatedAmount(TotalAmount);
                }
                bool isTrue = IsTextNullOrEmpty(TotalAmount);
                TotalAmount = isTrue ? "0.00" : TotalAmount;
                return TotalAmount;
            }
            catch (Exception)
            {
            }
            return TotalAmount;
        }

        public string TotalAmountForSixVarForNegative(string Amount1, string Amount2, string Amount3, string Amount4, string Amount5, string Amount6)
        {
            string TotalAmount = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(Amount1) && Amount1.Contains(","))
                {
                    Amount1 = Amount1.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount2) && Amount2.Contains(","))
                {
                    Amount2 = Amount2.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount3) && Amount3.Contains(","))
                {
                    Amount3 = Amount3.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount4) && Amount4.Contains(","))
                {
                    Amount4 = Amount4.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount5) && Amount5.Contains(","))
                {
                    Amount5 = Amount5.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount6) && Amount6.Contains(","))
                {
                    Amount6 = Amount6.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount1) && !string.IsNullOrEmpty(Amount2) && !string.IsNullOrEmpty(Amount3) && !string.IsNullOrEmpty(Amount4) && !string.IsNullOrEmpty(Amount5) && !string.IsNullOrEmpty(Amount6))
                {
                    if (Amount1 != "." && Amount2 != "." && Amount3 != "." && Amount4 != "." && Amount5 != "." && Amount6 != ".")
                    {
                        TotalAmount = Convert.ToDouble((string.IsNullOrEmpty(Amount1) ? 0.00 : Convert.ToDouble(Amount1)) + (string.IsNullOrEmpty(Amount2) ? 0.00 : Convert.ToDouble(Amount2)) + (string.IsNullOrEmpty(Amount3) ? 0.00 : Convert.ToDouble(Amount3)) + (string.IsNullOrEmpty(Amount4) ? 0.00 : Convert.ToDouble(Amount4)) + (string.IsNullOrEmpty(Amount5) ? 0.00 : Convert.ToDouble(Amount5)) + (string.IsNullOrEmpty(Amount6) ? 0.00 : Convert.ToDouble(Amount6))).ToString();
                        if (TotalAmount == "0")
                        {
                            TotalAmount = "0.00";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
                {
                    TotalAmount = Math.Round(Convert.ToDecimal(TotalAmount), 2).ToString();
                    TotalAmount = UtilityManager.GetCommaSeparatedAmount(TotalAmount);
                }
                bool isTrue = IsTextNullOrEmpty(TotalAmount);
                TotalAmount = isTrue ? "0.00" : TotalAmount;
                return TotalAmount;
            }
            catch (Exception)
            {


            }
            return TotalAmount;
        }

        public string TotalAmountForEightVar(string Amount1, string Amount2, string Amount3, string Amount4, string Amount5, string Amount6, string Amount7, string Amount8)
        {
            string TotalAmount = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(Amount1) && Amount1.Contains(","))
                {
                    Amount1 = Amount1.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount2) && Amount2.Contains(","))
                {
                    Amount2 = Amount2.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount3) && Amount3.Contains(","))
                {
                    Amount3 = Amount3.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount4) && Amount4.Contains(","))
                {
                    Amount4 = Amount4.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount5) && Amount5.Contains(","))
                {
                    Amount5 = Amount5.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount6) && Amount6.Contains(","))
                {
                    Amount6 = Amount6.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount7) && Amount7.Contains(","))
                {
                    Amount7 = Amount7.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount8) && Amount8.Contains(","))
                {
                    Amount8 = Amount8.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount1) && !string.IsNullOrEmpty(Amount2) && !string.IsNullOrEmpty(Amount3) && !string.IsNullOrEmpty(Amount4) && !string.IsNullOrEmpty(Amount5) && !string.IsNullOrEmpty(Amount6) && !string.IsNullOrEmpty(Amount7) && !string.IsNullOrEmpty(Amount8))
                {
                    if (Amount1 != "." && Amount2 != "." && Amount3 != "." && Amount4 != "." && Amount5 != "." && Amount6 != "." && Amount7 != "." && Amount8 != ".")
                    {
                        if (!Amount1.Contains("-") && !Amount2.Contains("-") && !Amount3.Contains("-") && !Amount4.Contains("-") && !Amount5.Contains("-") && !Amount6.Contains("-") && !Amount7.Contains("-") && !Amount8.Contains("-"))
                        {
                            TotalAmount = Convert.ToDouble((string.IsNullOrEmpty(Amount1) ? 0.00 : Convert.ToDouble(Amount1)) + (string.IsNullOrEmpty(Amount2) ? 0.00 : Convert.ToDouble(Amount2)) + (string.IsNullOrEmpty(Amount3) ? 0.00 : Convert.ToDouble(Amount3)) + (string.IsNullOrEmpty(Amount4) ? 0.00 : Convert.ToDouble(Amount4)) + (string.IsNullOrEmpty(Amount5) ? 0.00 : Convert.ToDouble(Amount5)) + (string.IsNullOrEmpty(Amount6) ? 0.00 : Convert.ToDouble(Amount6)) + (string.IsNullOrEmpty(Amount7) ? 0.00 : Convert.ToDouble(Amount7)) + (string.IsNullOrEmpty(Amount8) ? 0.00 : Convert.ToDouble(Amount8))).ToString();
                            if (TotalAmount == "0")
                            {
                                TotalAmount = "0.00";
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
                {
                    TotalAmount = Math.Round(Convert.ToDecimal(TotalAmount), 2).ToString();
                    TotalAmount = UtilityManager.GetCommaSeparatedAmount(TotalAmount);
                }
                bool isTrue = IsTextNullOrEmpty(TotalAmount);
                TotalAmount = isTrue ? "0.00" : TotalAmount;
                return TotalAmount;
            }
            catch (Exception)
            {


            }
            return TotalAmount;
        }


        public string TotalAmount(string Amount1, string Amount2, string Amount3, string Amount4, string Amount5)
        {
            string TotalAmount = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(Amount1) && Amount1.Contains(","))
                {
                    Amount1 = Amount1.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount2) && Amount2.Contains(","))
                {
                    Amount2 = Amount2.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount3) && Amount3.Contains(","))
                {
                    Amount3 = Amount3.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount4) && Amount4.Contains(","))
                {
                    Amount4 = Amount4.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount5) && Amount5.Contains(","))
                {
                    Amount5 = Amount5.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount1) && !string.IsNullOrEmpty(Amount2) && !string.IsNullOrEmpty(Amount3) && !string.IsNullOrEmpty(Amount4) && !string.IsNullOrEmpty(Amount5))
                {
                    if (Amount1 != "." && Amount2 != "." && Amount3 != "." && Amount4 != "." && Amount5 != ".")
                    {
                        if (!Amount1.Contains("-") && !Amount2.Contains("-") && !Amount3.Contains("-") && !Amount4.Contains("-") && !Amount5.Contains("-"))
                        {
                            TotalAmount = Convert.ToDouble((string.IsNullOrEmpty(Amount1) ? 0.00 : Convert.ToDouble(Amount1)) + (string.IsNullOrEmpty(Amount2) ? 0.00 : Convert.ToDouble(Amount2)) + (string.IsNullOrEmpty(Amount3) ? 0.00 : Convert.ToDouble(Amount3)) + (string.IsNullOrEmpty(Amount4) ? 0.00 : Convert.ToDouble(Amount4)) + (string.IsNullOrEmpty(Amount5) ? 0.00 : Convert.ToDouble(Amount5))).ToString();
                            if (TotalAmount == "0")
                            {
                                TotalAmount = "0.00";
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
                {
                    TotalAmount = Math.Round(Convert.ToDecimal(TotalAmount), 2).ToString();
                    TotalAmount = UtilityManager.GetCommaSeparatedAmount(TotalAmount);
                }
                bool isTrue = IsTextNullOrEmpty(TotalAmount);
                TotalAmount = isTrue ? "0.00" : TotalAmount;
                return TotalAmount;
            }
            catch (Exception)
            {


            }
            return TotalAmount;
        }

        public string TotalAdjustmentForSixVar(string Adjustment1, string Adjustment2, string Adjustment3, string Adjustment4, string Adjustment5, string Adjustment6)
        {
            string TotalAmount = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(Adjustment1) && Adjustment1.Contains(","))
                {
                    Adjustment1 = Adjustment1.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment2) && Adjustment2.Contains(","))
                {
                    Adjustment2 = Adjustment2.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment3) && Adjustment3.Contains(","))
                {
                    Adjustment3 = Adjustment3.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment4) && Adjustment4.Contains(","))
                {
                    Adjustment4 = Adjustment4.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment5) && Adjustment5.Contains(","))
                {
                    Adjustment5 = Adjustment5.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment6) && Adjustment6.Contains(","))
                {
                    Adjustment6 = Adjustment6.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment1) && !string.IsNullOrEmpty(Adjustment2) && !string.IsNullOrEmpty(Adjustment3) && !string.IsNullOrEmpty(Adjustment4) && !string.IsNullOrEmpty(Adjustment5) && !string.IsNullOrEmpty(Adjustment6))
                {
                    if (Adjustment1 != "." && Adjustment2 != "." && Adjustment3 != "." && Adjustment4 != "." && Adjustment5 != "." && Adjustment6 != ".")
                    {
                        if (!Adjustment1.Contains("-") && !Adjustment2.Contains("-") && !Adjustment3.Contains("-") && !Adjustment4.Contains("-") && !Adjustment5.Contains("-") && !Adjustment6.Contains("-"))
                        {
                            TotalAmount = Convert.ToDouble((string.IsNullOrEmpty(Adjustment1) ? 0 : Convert.ToDouble(Adjustment1)) + (string.IsNullOrEmpty(Adjustment2) ? 0 : Convert.ToDouble(Adjustment2)) + (string.IsNullOrEmpty(Adjustment3) ? 0 : Convert.ToDouble(Adjustment3)) + (string.IsNullOrEmpty(Adjustment4) ? 0 : Convert.ToDouble(Adjustment4)) + (string.IsNullOrEmpty(Adjustment5) ? 0 : Convert.ToDouble(Adjustment5)) + (string.IsNullOrEmpty(Adjustment6) ? 0 : Convert.ToDouble(Adjustment6))).ToString();
                            if (TotalAmount == "0")
                            {
                                TotalAmount = "0.00";
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
                {
                    TotalAmount = Math.Round(Convert.ToDecimal(TotalAmount), 2).ToString();
                    TotalAmount = UtilityManager.GetCommaSeparatedAmount(TotalAmount);
                }
                bool isTrue = IsTextNullOrEmpty(TotalAmount);
                TotalAmount = isTrue ? "0.00" : TotalAmount;
                return TotalAmount;
            }
            catch (Exception)
            {


            }
            return TotalAmount;
        }

        public string TotalAdjustment(string Adjustment1, string Adjustment2, string Adjustment3, string Adjustment4, string Adjustment5)
        {
            string TotalAmount = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(Adjustment1) && Adjustment1.Contains(","))
                {
                    Adjustment1 = Adjustment1.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment2) && Adjustment2.Contains(","))
                {
                    Adjustment2 = Adjustment2.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment3) && Adjustment3.Contains(","))
                {
                    Adjustment3 = Adjustment3.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment4) && Adjustment4.Contains(","))
                {
                    Adjustment4 = Adjustment4.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment5) && Adjustment5.Contains(","))
                {
                    Adjustment5 = Adjustment5.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment1) && !string.IsNullOrEmpty(Adjustment2) && !string.IsNullOrEmpty(Adjustment3) && !string.IsNullOrEmpty(Adjustment4) && !string.IsNullOrEmpty(Adjustment5))
                {
                    if (Adjustment1 != "." && Adjustment2 != "." && Adjustment3 != "." && Adjustment4 != "." && Adjustment5 != ".")
                    {
                        if (!Adjustment1.Contains("-") && !Adjustment2.Contains("-") && !Adjustment3.Contains("-") && !Adjustment4.Contains("-") && !Adjustment5.Contains("-"))
                        {
                            TotalAmount = Convert.ToDouble((string.IsNullOrEmpty(Adjustment1) ? 0 : Convert.ToDouble(Adjustment1)) + (string.IsNullOrEmpty(Adjustment2) ? 0 : Convert.ToDouble(Adjustment2)) + (string.IsNullOrEmpty(Adjustment3) ? 0 : Convert.ToDouble(Adjustment3)) + (string.IsNullOrEmpty(Adjustment4) ? 0 : Convert.ToDouble(Adjustment4)) + (string.IsNullOrEmpty(Adjustment5) ? 0 : Convert.ToDouble(Adjustment5))).ToString();
                            if (TotalAmount == "0")
                            {
                                TotalAmount = "0.00";
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
                {
                    TotalAmount = Math.Round(Convert.ToDecimal(TotalAmount), 2).ToString();
                    TotalAmount = UtilityManager.GetCommaSeparatedAmount(TotalAmount);
                }
                bool isTrue = IsTextNullOrEmpty(TotalAmount);
                TotalAmount = isTrue ? "0.00" : TotalAmount;
                return TotalAmount;
            }
            catch (Exception)
            {


            }
            return TotalAmount;
        }

        public string GetSingleAmount(string Amount1)
        {
            string TotalAmount = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(Amount1) && Amount1.Contains(","))
                {
                    Amount1 = Amount1.Replace(",", "");
                }

                if (!string.IsNullOrEmpty(Amount1))
                {
                    TotalAmount = Convert.ToDouble(Convert.ToDouble(Amount1)).ToString();
                    if (TotalAmount == "0")
                    {
                        TotalAmount = "0.00";
                    }
                }
                if (!string.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
                {
                    TotalAmount = Math.Round(Convert.ToDecimal(TotalAmount), 2).ToString();
                    TotalAmount = UtilityManager.GetCommaSeparatedAmount(TotalAmount);
                }
                bool isTrue = IsTextNullOrEmpty(TotalAmount);
                TotalAmount = isTrue ? "0.00" : TotalAmount;
            }
            catch (Exception)
            {


            }
            return TotalAmount;
        }

        public string AddTwoAmount(string Amount1, string Amount2)
        {
            string TotalAmount = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(Amount1) && Amount1.Contains(","))
                {
                    Amount1 = Amount1.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount2) && Amount2.Contains(","))
                {
                    Amount2 = Amount2.Replace(",", "");
                }
                if (string.IsNullOrEmpty(Amount1))
                {
                    Amount1 = "0.00";
                }
                if (string.IsNullOrEmpty(Amount2))
                {
                    Amount2 = "0.00";
                }
                if (!string.IsNullOrEmpty(Amount1) && !string.IsNullOrEmpty(Amount2))
                {
                    TotalAmount = Convert.ToDouble(Convert.ToDouble(Amount1) + Convert.ToDouble(Amount2)).ToString();
                    if (TotalAmount == "0")
                    {
                        TotalAmount = "0.00";
                    }
                }
                if (!string.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
                {
                    TotalAmount = Math.Round(Convert.ToDecimal(TotalAmount), 2).ToString();
                    TotalAmount = UtilityManager.GetCommaSeparatedAmount(TotalAmount);
                }
                bool isTrue = IsTextNullOrEmpty(TotalAmount);
                TotalAmount = isTrue ? "0.00" : TotalAmount;
            }
            catch (Exception)
            {


            }
            return TotalAmount;
        }

        public string TotalVatAmount(string Amount1, string Amount2, string Amount3)
        {
            string TotalAmount = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(Amount1) && Amount1.Contains(","))
                {
                    Amount1 = Amount1.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount2) && Amount2.Contains(","))
                {
                    Amount2 = Amount2.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount3) && Amount3.Contains(","))
                {
                    Amount3 = Amount3.Replace(",", "");
                }
                if (string.IsNullOrEmpty(Amount1))
                {
                    Amount1 = "0.00";
                }
                if (string.IsNullOrEmpty(Amount2))
                {
                    Amount2 = "0.00";
                }
                if (string.IsNullOrEmpty(Amount3))
                {
                    Amount3 = "0.00";
                }
                if (!string.IsNullOrEmpty(Amount1) && !string.IsNullOrEmpty(Amount2) && !string.IsNullOrEmpty(Amount3))
                {
                    TotalAmount = Convert.ToDouble(Convert.ToDouble(Amount1) + Convert.ToDouble(Amount2) + Convert.ToDouble(Amount3)).ToString();
                    if (TotalAmount == "0")
                    {
                        TotalAmount = "0.00";
                    }
                }
                if (!string.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
                {
                    TotalAmount = Math.Round(Convert.ToDecimal(TotalAmount), 2).ToString();
                    TotalAmount = UtilityManager.GetCommaSeparatedAmount(TotalAmount);
                }
                bool isTrue = IsTextNullOrEmpty(TotalAmount);
                TotalAmount = isTrue ? "0.00" : TotalAmount;
            }
            catch (Exception)
            {


            }
            return TotalAmount;
        }
        public string StandardRatedDomesticPurchaseVatAmount(string Amount, string Adjustment)
        {
            string VATAmount = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(Amount) && Amount.Contains(","))
                {
                    Amount = Amount.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment) && Adjustment.Contains(","))
                {
                    Adjustment = Adjustment.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount) && !string.IsNullOrEmpty(Adjustment) && Amount != "." && Adjustment != ".")
                {
                    if (!Amount.Contains("-") && !Adjustment.Contains("-"))
                    {
                        double dAmount = string.IsNullOrEmpty(Amount) ? 0 : Convert.ToDouble(Amount);
                        double dAdjustment = string.IsNullOrEmpty(Adjustment) ? 0 : Convert.ToDouble(Adjustment);
                        double dVATRate = Convert.ToDouble(VATRate002);
                        VATAmount = Convert.ToDouble((dAmount - dAdjustment) * dVATRate / 100).ToString();
                        if (VATAmount == "0")
                        {
                            VATAmount = "0.00";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(VATAmount) && VATAmount != "0.00")
                {
                    VATAmount = Math.Round(Convert.ToDecimal(VATAmount), 2).ToString();
                    VATAmount = UtilityManager.GetCommaSeparatedAmount(VATAmount);
                }
                bool isTrue = IsTextNullOrEmpty(VATAmount);
                VATAmount = isTrue ? "0.00" : VATAmount;
                return VATAmount;
            }
            catch (Exception)
            {


            }
            return VATAmount;
        }
        //This method is used to calculate  Imports subject to VAT accounted for through the reverse charge mechanism Vat Amount too.
        public string ImportSubjectToVatPaidAtCustomsVatAmountForDesignated(string Amount, string Adjustment)
        {
            string VATAmount = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(Amount) && Amount.Contains(","))
                {
                    Amount = Amount.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment) && Adjustment.Contains(","))
                {
                    Adjustment = Adjustment.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount) && !string.IsNullOrEmpty(Adjustment) && Amount != "." && Adjustment != ".")
                {
                    if (!Amount.Contains("-") && !Adjustment.Contains("-"))
                    {
                        double dAmount = string.IsNullOrEmpty(Amount) ? 0 : Convert.ToDouble(Amount);
                        double dAdjustment = string.IsNullOrEmpty(Adjustment) ? 0 : Convert.ToDouble(Adjustment);
                        double dVATRate001 = Convert.ToDouble(VATRate001);
                        double dVATRate002 = Convert.ToDouble(VATRate002);
                        VATAmount = Convert.ToDouble(dAmount * dVATRate001 / 100 - dAdjustment * dVATRate002 / 100).ToString();
                        if (VATAmount == "0")
                        {
                            VATAmount = "0.00";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(VATAmount) && VATAmount != "0.00")
                {
                    VATAmount = Math.Round(Convert.ToDecimal(VATAmount), 2).ToString();
                    VATAmount = UtilityManager.GetCommaSeparatedAmount(VATAmount);
                }
                bool isTrue = IsTextNullOrEmpty(VATAmount);
                VATAmount = isTrue ? "0.00" : VATAmount;
                return VATAmount;
            }
            catch (Exception)
            {


            }
            return VATAmount;
        }

        public string ImportSubjectToVatPaidAtCustomsVatAmountForDesignatedForNewPercentage(string Amount, string Adjustment, string NewVATRate)
        {
            string VATAmount = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(Amount) && Amount.Contains(","))
                {
                    Amount = Amount.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment) && Adjustment.Contains(","))
                {
                    Adjustment = Adjustment.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(NewVATRate) && NewVATRate.Contains(","))
                {
                    NewVATRate = NewVATRate.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount) && !string.IsNullOrEmpty(Adjustment) && !string.IsNullOrEmpty(NewVATRate) && Amount != "." && Adjustment != "." && NewVATRate != ".")
                {
                    if (!Amount.Contains("-") && !Adjustment.Contains("-") && !NewVATRate.Contains("-"))
                    {
                        double dAmount = string.IsNullOrEmpty(Amount) ? 0 : Convert.ToDouble(Amount);
                        double dAdjustment = string.IsNullOrEmpty(Adjustment) ? 0 : Convert.ToDouble(Adjustment);
                        double dVATRate002 = string.IsNullOrEmpty(NewVATRate) ? 0 : Convert.ToDouble(NewVATRate);
                        double dVATRate001 = Convert.ToDouble(VATRate001);
                        dVATRate002 = Convert.ToDouble(NewVATRate);
                        VATAmount = Convert.ToDouble(dAmount * dVATRate001 / 100 - dAdjustment * dVATRate002 / 100).ToString();
                        if (VATAmount == "0")
                        {
                            VATAmount = "0.00";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(VATAmount) && VATAmount != "0.00")
                {
                    VATAmount = Math.Round(Convert.ToDecimal(VATAmount), 2).ToString();
                    VATAmount = UtilityManager.GetCommaSeparatedAmount(VATAmount);
                }
                bool isTrue = IsTextNullOrEmpty(VATAmount);
                VATAmount = isTrue ? "0.00" : VATAmount;
                return VATAmount;
            }
            catch (Exception)
            {


            }
            return VATAmount;
        }

        public string ImportSubjectToVatPaidAtCustomsVatAmountForNonDesignated(string Amount, string Adjustment)
        {
            string VATAmount = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(Amount) && Amount.Contains(","))
                {
                    Amount = Amount.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Adjustment) && Adjustment.Contains(","))
                {
                    Adjustment = Adjustment.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(Amount) && !string.IsNullOrEmpty(Adjustment) && Amount != "." && Adjustment != ".")
                {
                    if (!Amount.Contains("-") && !Adjustment.Contains("-"))
                    {
                        double dAmount = string.IsNullOrEmpty(Amount) ? 0 : Convert.ToDouble(Amount);
                        double dAdjustment = string.IsNullOrEmpty(Adjustment) ? 0 : Convert.ToDouble(Adjustment);
                        double dVATRate = Convert.ToDouble(VATRate002);
                        VATAmount = Convert.ToDouble((dAmount - dAdjustment) * dVATRate / 100).ToString();
                        if (VATAmount == "0")
                        {
                            VATAmount = "0.00";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(VATAmount) && VATAmount != "0.00")
                {
                    VATAmount = Math.Round(Convert.ToDecimal(VATAmount), 2).ToString();
                    VATAmount = UtilityManager.GetCommaSeparatedAmount(VATAmount);
                }
                bool isTrue = IsTextNullOrEmpty(VATAmount);
                VATAmount = isTrue ? "0.00" : VATAmount;
                return VATAmount;
            }
            catch (Exception)
            {


            }
            return VATAmount;
        }
        public string NetVatDue(string CurrentPeriod, string PreviousPeriod, string ForwardFromPreviousPeriod)
        {
            string NetVatDue = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(CurrentPeriod) && CurrentPeriod.Contains(","))
                {
                    CurrentPeriod = CurrentPeriod.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(PreviousPeriod) && PreviousPeriod.Contains(","))
                {
                    PreviousPeriod = PreviousPeriod.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(ForwardFromPreviousPeriod) && ForwardFromPreviousPeriod.Contains(","))
                {
                    ForwardFromPreviousPeriod = ForwardFromPreviousPeriod.Replace(",", "");
                }
                try
                {
                    double dCurrentPeriod = string.IsNullOrEmpty(CurrentPeriod) ? 0 : Convert.ToDouble(CurrentPeriod);
                    double dPreviousPeriod = string.IsNullOrEmpty(PreviousPeriod) ? 0 : Convert.ToDouble(PreviousPeriod);
                    double dForwardFromPreviousPeriod = string.IsNullOrEmpty(ForwardFromPreviousPeriod) ? 0 : Convert.ToDouble(ForwardFromPreviousPeriod);
                    NetVatDue = Convert.ToDouble(dCurrentPeriod + dPreviousPeriod + dForwardFromPreviousPeriod).ToString();
                    if (NetVatDue == "0")
                    {
                        NetVatDue = "0.00";
                    }
                }
                catch
                {
                }
                if (!string.IsNullOrEmpty(NetVatDue) && NetVatDue != "0.00")
                {
                    NetVatDue = Math.Round(Convert.ToDecimal(NetVatDue), 2).ToString();
                    NetVatDue = UtilityManager.GetCommaSeparatedAmount(NetVatDue);
                }
                bool isTrue = IsTextNullOrEmpty(NetVatDue);
                NetVatDue = isTrue ? "0.00" : NetVatDue;
                return NetVatDue;
            }
            catch (Exception)
            {


            }
            return NetVatDue;
        }
        #endregion
        #endregion
        public void SetButtonStrings(string ButtonName)
        {
            if (ButtonName == "Submit")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.Submit);
            }
            else if (ButtonName == "Reject")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.ZVATRejectButton);
            }
            else if (ButtonName == "Void")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.ZZVoid);
            }
            else if (ButtonName == "SaveasDraft")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.ZZSaveAsDraft);
            }
            else if (ButtonName == "DisplayNotes")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.ZZDisplayNotes);
            }
            //else if (ButtonName == "Validate")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZZValidate);
            //}
            else if (ButtonName == "Attachments")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.Attachments);
            }
            else if (ButtonName == "Reset")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.ZZReset);
            }
            else if (ButtonName == "CreateNotes")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.ZVATCreateNote);
            }
            else if (ButtonName == "Amend")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.ZZAmend);
            }
            else if (ButtonName == "Closed")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.ZZClose);
            }
            else if (ButtonName == "SavetheDeclaration")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.ZVATSavetheDeclarationButton);
            }
            else if (ButtonName == "CancelDeclaration")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.ZVATCancelDeclarationButton);
            }
            else if (ButtonName == "SubmittheDeclaration")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.ZVATSubmittheDeclarationButton);
            }
            else if (ButtonName == "Release")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.Release);
            }
            else if (ButtonName == "Approve")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.ZVATApproveButton);
            }
            else if (ButtonName == "Forward")
            {
                DummyListOfActionButtonsApplicable.Add(AppResources.ZVATForwardButton);
            }
            //else if (ButtonName == "NotesforER")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATNotesforERButton);
            //}
            //else if (ButtonName == "Assigntome")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATAssigntomeButton);
            //}
            //else if (ButtonName == "Calendar")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATCalendarButton);
            //}
            //else if (ButtonName == "Confirm")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.Confirm);
            //}
            //else if (ButtonName == "SendforInspection")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATSendforInspectionButton);
            //}
            //else if (ButtonName == "AssignInspector")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATAssignInspectorButton);
            //}
            //else if (ButtonName == "SendBack")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATSendBackButton);
            //}
            //else if (ButtonName == "AttachBankGuarantee")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATAttachBankGuaranteeButton);
            //}
            //else if (ButtonName == "ExtendDueDate")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATExtendDueDateButton);
            //}
            //else if (ButtonName == "Next")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZZNext);
            //}
            //else if (ButtonName == "InspectorSubmit")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATInspectorSubmitButton);
            //}
            //else if (ButtonName == "ApplicationDownloadforInspector")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATApplicationDownloadforInspectorButton);
            //}
            //else if (ButtonName == "Reviewed")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATReviewed);
            //}
            //else if (ButtonName == "AssignOfficer")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATAssignOfficerButton);
            //}
            //else if (ButtonName == "EditaMovementActivity")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATEditaMovementActivityButton);
            //}
            //else if (ButtonName == "CancelMovementActivity")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATCancelMovementActivityButton);
            //}
            //else if (ButtonName == "AddNewMovementActivity")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATAddNewMovementActivityButton);
            //}
            //else if (ButtonName == "SendforAudit")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATSendforAuditButton);
            //}
            //else if (ButtonName == "SendtoDirector")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATSendtoDirectorButton);
            //}
            //else if (ButtonName == "SubmitInspector")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATSubmitInspectorButton);
            //}
            //else if (ButtonName == "AttachUnloadingDocument")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATAttachUnloadingDocumentButton);
            //}
            //else if (ButtonName == "ClearDocument")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATClearDocumentButton);
            //}
            //else if (ButtonName == "ExtendApprovalTime")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATExtendApprovalTimeButton);
            //}
            //else if (ButtonName == "Change")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATChangeButton);
            //}
            //else if (ButtonName == "Extend")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATExtendButton);
            //}
            //else if (ButtonName == "Revoke")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATRevokeButton);
            //}
            //else if (ButtonName == "SendtoTaxpayer")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATSendtoTaxpayerButton);
            //}
            //else if (ButtonName == "SummaryDetails")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATSummaryDetailsButton);
            //}
            //else if (ButtonName == "PrintSDReleaseLetter")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATPrintSDReleaseLetterButton);
            //}
            //else if (ButtonName == "ReleaseBankGuarantee")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATReleaseBankGuaranteeButton);
            //}
            //else if (ButtonName == "ComplianceAndHistory")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATComplianceAndHistoryButton);
            //}
            //else if (ButtonName == "Previous")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATPreviousButton);
            //}
            //else if (ButtonName == "CancelReturn")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATCancelReturnButton);
            //}
            //else if (ButtonName == "RequestAdditionalInformation")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATRequestAdditionalInformationButton);
            //}
            //else if (ButtonName == "Salesdetails")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATSalesdetailsButton);
            //}
            //else if (ButtonName == "Changefromestimatetoaccounting")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATChangefromestimatetoaccountingButton);
            //}
            //else if (ButtonName == "Invoice")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATInvoiceButton);
            //}
            //else if (ButtonName == "ReviseDownPayment")
            //{
            //    DummyListOfActionButtonsApplicable.Add(AppResources.ZVATReviseDownPaymentButton);
            //}
        }
        public async Task SetButtons(VATDeclaration vATDeclarationData)
        {
            try
            {
                List<ApplicableButton> VATApplicableButtons = await WebServiceManager.GAZTVATReturnGetApplicableButtons(VATDeclarationData.d.Fbnumz, VATDeclarationData.d.Langz, VATDeclarationData.d.Operationz, VATDeclarationData.d.Gpart, VATDeclarationData.d.Statusz, VATDeclarationData.d.TxnTpz, vATDeclarationData.d.Periodkeyz);
                PopToRootPage();
                ListOfActionButtonsApplicable = new List<string>();
                DummyListOfActionButtonsApplicable = new List<string>();
                if (VATApplicableButtons != null)
                {
                    ListOfActionButtonsApplicable.Clear();
                    DummyListOfActionButtonsApplicable.Clear();
                    foreach (ApplicableButton button in VATApplicableButtons)
                    {
                        SetButtonStrings(button.buttonEnumId.ToString());
                        // ListOfActionButtonsApplicable.Add(button.buttonEnumId.ToString());
                    }
                    ListOfActionButtonsApplicable = DummyListOfActionButtonsApplicable;
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
        private bool IsTextNullOrEmpty(string entryText)
        {
            bool isEmpty = false;
            if (string.IsNullOrEmpty(entryText))
            {
                isEmpty = true;
            }
            else
            {
                isEmpty = false;
            }
            return isEmpty;
        }
        private void ManageThePreperiodcorrSwitch()
        {
            if (Convert.ToDouble(Preperiodcorr) < 0)
            {
                ShowThePreperiodcorrSwitch();
            }
            else
            {
                HideThePreperiodcorrSwitch();
            }
        }
        private void ShowThePreperiodcorrSwitch()
        {
            IsSwitchToggled = true;
        }
        private void HideThePreperiodcorrSwitch()
        {
            IsSwitchToggled = false;
        }
    }
}

