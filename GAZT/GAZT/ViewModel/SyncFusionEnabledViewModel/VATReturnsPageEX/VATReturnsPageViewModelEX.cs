using EGAZT.Models;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using Newtonsoft.Json;
using Plugin.FilePicker;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.VATReturnsPageEX
{
    [Preserve(AllMembers = true)]
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
                _isSwitchToggledFor15PercentChange = value;
                if (_isSwitchToggledFor15PercentChange != null)
                {
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
                _isNoChecked = value;
                if(_isNoChecked==true)
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
                _responseVatDeclaration = value;
                RaisePropertyChanged("ResponseVatDeclaration");
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
        private String _stepNumberz;
        public String StepNumberz
        {
            get
            {
                return _stepNumberz;
            }
            set
            {
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
                        if (Preperiodcorr != null && Preperiodcorr.Contains("-") && (!string.IsNullOrEmpty(Preperiodcorr)))
                            Preperiodcorr = Preperiodcorr.Replace("-", "");
                    }
                }
                catch (Exception ex)
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
                _isCheckedTaxPayerDetailsInfo = value;
                if (_isCheckedTaxPayerDetailsInfo == true)
                {
                    if (((App.ICRStatus == "E0045" || App.ICRStatus == "E0006") && (IsAmendClicked == false)) || App.ICRStatus == "E0055" || App.ICRStatus == "E0058")
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
                _isDeclarationCheckedForSummary = value;
                if (_isDeclarationCheckedForSummary == true)
                {
                    if (((App.ICRStatus == "E0045" || App.ICRStatus == "E0006") && (IsAmendClicked == false)) || App.ICRStatus == "E0055")
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
                _isDeclarationCheckedForInstruction = value;
                if (_isDeclarationCheckedForInstruction == true)
                {
                    if (((App.ICRStatus == "E0045" || App.ICRStatus == "E0006") && (IsAmendClicked == false)) || App.ICRStatus == "E0055")
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
                if (value == true)
                {
                }
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
                if (value == true)
                {
                }
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
                _vATRate002 = value;
                RaisePropertyChanged("VATRate002");
            }
        }

        private string _vATRate003=string.Empty;
        public string VATRate003
        {
            get
            {
                return _vATRate003;
            }
            set
            {
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
                    _preperiodcorr = value;
                    if (_preperiodcorr != null)
                    {
                        bool isValiedNumber = UtilityManager.IsEnglishNumberWithMinus(Preperiodcorr);
                        if (isValiedNumber)
                        {
                            NetdueVat = NetVatDue(TotaldueVat, Preperiodcorr, CreditVat);
                            if (_preperiodcorr != "." && _preperiodcorr != "" && _preperiodcorr != "-" && !string.IsNullOrEmpty(CorrectionPeriodAmount) && !string.IsNullOrEmpty(CorrectionNegativePeriodAmount))
                            {
                                if ((Convert.ToDecimal(_preperiodcorr) >= Convert.ToDecimal(CorrectionPeriodAmount)) || (Convert.ToDecimal(_preperiodcorr) <= Convert.ToDecimal(CorrectionNegativePeriodAmount)))
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
                catch (Exception ex)
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
                _totalsalesVat = value;
                if (!string.IsNullOrEmpty(_totalsalesVat))
                {
                    if (string.IsNullOrEmpty(TotalsalesVat) || (TotalsalesVat == "0"))
                    {
                        TotalsalesVat = "0.00";
                    }
                    if (string.IsNullOrEmpty(TotalpurchaseVat) || (TotalpurchaseVat == "0"))
                    {
                        TotalpurchaseVat = "0.00";
                    }
                    TotaldueVat = (Convert.ToDouble(TotalsalesVat) - Convert.ToDouble(TotalpurchaseVat)).ToString();
                    if (TotaldueVat == "0")
                    {
                        TotaldueVat = "0.00";
                    }
                    if (!String.IsNullOrEmpty(TotaldueVat) && TotaldueVat != "0.00")
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
                _totalpurchaseVat = value;
                if (!string.IsNullOrEmpty(_totalpurchaseVat))
                {
                    TotaldueVat = (Convert.ToDouble(TotalsalesVat) - Convert.ToDouble(TotalpurchaseVat)).ToString();
                    if (TotaldueVat == "0")
                    {
                        TotaldueVat = "0.00";
                    }
                    if (!String.IsNullOrEmpty(TotaldueVat) && TotaldueVat != "0.00")
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
                _is15PercentChangeToggled = value;
                if (_is15PercentChangeToggled == true)
                {
                }
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
                _entryVatAmountTextColor = value;
                if (_entryVatAmountTextColor == Color.FromHex("#ff0000"))
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
                await _dialogService.ShowMessage(AppResources.ZZZChangeRegistationNote, AppResources.ZInstructions);
            });
            // OnStepButtonClicked = new Command(ExecuteStepBtnClickCommand, CanExecuteStepBtnClickCommand);
            OnStepButtonClicked = new Xamarin.Forms.Command(async () =>
            {
                if (!string.IsNullOrEmpty(ButtonName))
                {
                    if (ButtonName == AppResources.ZVatStepTwo)
                    {
                        //TaxpayerDetailsClicked();
                        SelectedIndex = 1;
                        PageSelectedItem = VatTabbledPageList[1];
                        //  VATTabbedPageReturnCollectionView.SelectedItems.Add((this.VATTabbedPageReturnCollectionView.ItemsSource as List<VATDeclarationTabbedPageName>)[0]);
                        //  _dialogService.ShowMessage(AppResources.ZZGeneralMessageformVoidedBeforeChangeOfRegistrationForm, AppResources.Information);
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
                            Device.BeginInvokeOnMainThread(async () =>
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
                            //    Device.BeginInvokeOnMainThread(() =>
                            //{
                            //    IsLoading = true;
                            //});

                            //var t = Task.Run(() =>
                            // {
                            //// Do some work on a background thread, allowing the UI to remain responsive
                            //Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                            //     {
                            //         SummaryClicked();
                            //     });
                            // });


                            // await SummaryClicked();
                            //ShowMsgs();
                            //SelectedIndex = 3;
                            //PageSelectedItem = VatTabbledPageList[3];
                            //Device.BeginInvokeOnMainThread(() =>
                            //{
                            //    IsLoading = false;
                            //});

                            //Task.Run(() =>
                            //{
                            //    IsLoading = false;
                            //});
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else if (ButtonName == AppResources.Submit)
                    {
                        try
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                IsLoading = true;
                            });
                            await SubmitClicked();
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                IsLoading = false;
                            });
                        }
                        catch (Exception ex)
                        {
                            IsLoading = false;
                        }
                    }
                    //else if(ButtonName == AppResources.ZVatDownloadForm)
                    //{
                    //    String Url = string.Empty;
                    //   // Url = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/Z_GET_ACK_LETTER_SRV/Ack_letterSet(Fbnum=%2765000178937%27)/$value?saml2=disabled";
                    //     Url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_COVERFORM_SRV/cover_formSet(Fbnum='" + VATDeclarationData.d.Fbnum + "',Utype='')/$value?saml2=disabled";
                    //   ShowPdf(Url);
                    //}
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
            onStandardRatedSalesVatAmountTapped = new Xamarin.Forms.Command(() =>
            {
                // InstrunctionClicked();
                ResponseVATDeclarationD.StdsalesVat = StandardRatedSalesVatAmount(ResponseVATDeclarationD.StdsalesAmt, ResponseVATDeclarationD.StdsalesAdj);
            });
            OnGetAcknowledgementLinkClicked = new Xamarin.Forms.Command(async () =>
            {
                _navigationService.NavigateTo(App.AcknowledgementDetailsPageView, VATDeclarationData);
            });
            onInstructionsClicked = new Xamarin.Forms.Command(async () =>
            {
                InstrunctionClicked();
            });
            onTaxPayerDetailsClicked = new Xamarin.Forms.Command(async () =>
            {
                TaxpayerDetailsClicked();
            });
            onVATReturnFormClicked = new Xamarin.Forms.Command(async () =>
            {
                VATReturnFormClicked();
            });
            OnAcknowlwdgementClicked = new Xamarin.Forms.Command(async () =>
            {
                string url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_ACK_LETTER_SRV/Ack_letterSet(Fbnum='" + VATDeclarationData + "')/$value?saml2=enabled";
                _navigationService.NavigateTo(App.AAcknowledgementView, url);
            });
            onFaqSectionClicked = new Xamarin.Forms.Command(async () =>
            {
                if (App.IsArabic)
                {
                    Device.OpenUri(new Uri("https://www.vat.gov.sa/ar/introduction-to-vat/faq/general-faqs"));
                }
                else
                {
                    Device.OpenUri(new Uri("https://www.vat.gov.sa/en/introduction-to-vat/faq/general-faqs"));
                }
            });
            OnDownloadAcknowlwdgementClicked = new Xamarin.Forms.Command(async () =>
            {
                string url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_COVERFORM_SRV/cover_formSet(Fbnum='" + VATDeclarationData + "',Utype='')/$value?saml2=enabled";
                _navigationService.NavigateTo(App.AAcknowledgementView, url);
            });
            OnVATRefreshButtonClicked = new Xamarin.Forms.Command(async () =>
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
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    });
                }
                // Call Sadad number API
            });
            OnAttachmentClick = new Xamarin.Forms.Command(async () =>
            {
                try
                {
                    string[] filetypes;
                    filetypes = DependencyService.Get<IDeviceInfo>().GetAttachmentTypeString();
                    //                if (Device.RuntimePlatform == Device.iOS)
                    //                {
                    //                    filetypes = new string[] {
                    ////            UTType.PDF,
                    ////            "org.openxmlformats.wordprocessingml.document",
                    ////            "com.microsoft.word.doc",
                    ////"org.openxmlformats.spreadsheetml.sheet",
                    ////"org.openxmlformats.presentationml.presentation",
                    ////            UTType.JPEG,
                    ////            UTType.PNG,
                    ////            UTType.GIF,
                    ////            "com.microsoft.excel.xls",
                    ////            "com.microsoft.powerpoint.​ppt",
                    ////             UTType.PlainText
                    //                        };
                    //                }
                    //                else
                    //                {
                    //                    filetypes = new string[] { "application/pdf", "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "image/jpeg", "image/jpg", "application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "image/png", "application/vnd.ms-powerpoint", "application/vnd.openxmlformats-officedocument.presentationml.presentation", "image/gif", "text/plain" };
                    //                }
                    var fileData = await CrossFilePicker.Current.PickFile(filetypes);
                    attachment = fileData.DataArray;
                    AttachmentName = fileData.FileName;
                    string ContentType = UtilityManager.GetContentType(AttachmentName.Split('.')[1].ToLower());
                    AttachmentRootOject _attachment = await WebServiceManager.GAZTSaveVATDeclarationAttachment(attachment, AttachmentName, VATDeclarationData.d.ReturnIdz, "VTA0", ContentType);
                    PopToRootPage();
                    if (_attachment != null && _attachment.d != null)
                    {
                        VATDeclarationData.d.ATTACHSet.results.Add(_attachment.d);
                        ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(VATDeclarationData.d.ATTACHSet.results as List<Attachment>);
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            VatAttachmentsList = myCollection;
                        });
                        VatAttachmentsList = myCollection;
                    }
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    });
                }
            });
            onSummaryClicked = new Xamarin.Forms.Command(async () =>
            {
                await SummaryClicked();
            });
            onCreditCarriedForwardClicked = new Xamarin.Forms.Command(async () =>
            {
                _navigationService.NavigateTo(App.CreditCarriedPageView, VATDeclarationData);
                //   CreditCarriedClicked();
            });
            onOptionClicked = new Xamarin.Forms.Command(async () =>
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
            //OnCopySadadNumberButtonClicked = new Xamarin.Forms.Command(async () =>
            //{
            //    await _dialogService.ShowMessage("It has copied sadad payment number", AppResources.Information);
            //});
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
                if (((Convert.ToDecimal(Percentage) / 100) * Convert.ToDecimal(TotalsalesAmt)) + Convert.ToDecimal(TotalsalesAmt) < Convert.ToDecimal(TotalsalesAdj))
                {
                    Masseges.Append(string.Format(AppResources.ZZValidationMessage11_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]));
                }
                //  CheckSixaSixb(Convert.ToDecimal(LabelTotalsalesAmt.Text), Convert.ToDecimal(LabelTotalsalesAdj.Text));
            }
            if (!string.IsNullOrEmpty(TotalsalesAmt) && !string.IsNullOrEmpty(TotalpurchaseAmt))
            {
                string Percentage = CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                //  decimal PercentageValue = (LabelTotalsalesAmt / 100) * Convert.ToDecimal(Percentage);
                if (((Convert.ToDecimal(Percentage) / 100) * Convert.ToDecimal(TotalsalesAmt)) + Convert.ToDecimal(TotalsalesAmt) < Convert.ToDecimal(TotalpurchaseAmt))
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
                if (((Convert.ToDecimal(Percentage) / 100) * Convert.ToDecimal(TotalpurchaseAmt)) + Convert.ToDecimal(TotalpurchaseAmt) < Convert.ToDecimal(TotalpurchaseAdj))
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
                Device.BeginInvokeOnMainThread(async () =>
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
                Device.BeginInvokeOnMainThread(() =>
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
                        Device.BeginInvokeOnMainThread(async () =>
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
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                _navigationService.GoBack();
                            });
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessage(WebServiceManager.ErrorMessageForVAT, AppResources.Information);
                                //_navigationService.GoBack();
                                WebServiceManager.ErrorMessageForVAT = string.Empty;
                            });
                        }
                        //Device.BeginInvokeOnMainThread(async () =>
                        //{
                        //    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        //});
                    }
                });
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = false;
                });
            }
            catch (Exception ex)
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
                        if ((App.ICRStatus == "E0045" && IsAmendClicked == false) || (App.ICRStatus == "E0006"))
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
                        if ((App.ICRStatus == "E0045" && IsAmendClicked == false) || (App.ICRStatus == "E0006") || App.ICRStatus == "E0058")
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
            catch (Exception ex)
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
                Device.BeginInvokeOnMainThread(async () =>
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
                    if (IsCheckedRefundOfSubmitForYes() && (IsRefundYesMsgDisplayed == false))
                    {
                        IsFirstSubmission = true;
                    }
                }
                else
                {
                    if (IsCheckedRefundForSubmit() && (IsRefundNoMsgDisplayed == false))
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
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                    _navigationService.GoBack();
                                });
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
                    //Device.BeginInvokeOnMainThread(async () =>
                    //{
                    //   _dialogService.ShowMessage(string.Format(AppResources.ZZGeneralMessage_VATReturnFormSubmittedSuccessfullyAndFormBundleNumber, VATDeclarationData.d.Fbnum), AppResources.Information);
                    //});
                    if (res != null && res.d != null)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
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
                        if(App.ICRStatus=="E0045" || App.ICRStatus=="E0056")
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
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                _navigationService.GoBack();
                            });
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
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
                            if ((IsSwichButtonEnable == false && FourteenA < 5000 && Convert.ToDecimal(NetdueVat) < 0) || (IsSwichButtonEnable == true && FourteenA < 100000 && Convert.ToDecimal(CreditVat) > 0))
                            {
                                StringBuilder Masseges = new StringBuilder();
                                Masseges.Append(AppResources.Pleasereviewthecalculationandsubmitagain);
                                Masseges.Append(Environment.NewLine);
                                Masseges.Append(Environment.NewLine);
                                Masseges.Append(Environment.NewLine);
                                Masseges.Append(AppResources.CreditReturnMsg);
                                PopUp Pop = new PopUp();
                                Pop.IsLinkAvailable = false;
                                Pop.IsRed = "#ff0000";
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
                                    Device.BeginInvokeOnMainThread(async () =>
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
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                    _navigationService.GoBack();
                                });
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(async () =>
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
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                IsLoading = false;
            }
        }

        public bool IsCheckedRefundForSubmit()
        {
            bool result = false;
            string netVATdue = string.Empty;
            if (!String.IsNullOrEmpty(NetdueVat))
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
            if (!String.IsNullOrEmpty(NetdueVat))
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
            if (!String.IsNullOrEmpty(NetdueVat))
            {
                NetVAT = !NetdueVat.Contains(",") ? NetdueVat : NetdueVat.Replace(",", "");
            }
            if (Convert.ToDouble(NetVAT) <= 0 && IsVisibleDropdownForRefund == false && IsRefundVisible == true)
            {
                bool result;
                if(App.IsArabic)
                {
                     result = await Application.Current.MainPage.DisplayAlert(AppResources.ZZZConfirmationMsg, AppResources.ZZZRefundNoMsg,AppResources.ZZCancel,AppResources.Confirm);
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
                     result = await Application.Current.MainPage.DisplayAlert(AppResources.ZZZConfirmationMsg, AppResources.ZZZRefundNoMsg, AppResources.Confirm, AppResources.ZZCancel);
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
            if (!String.IsNullOrEmpty(NetdueVat))
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
        //            Device.BeginInvokeOnMainThread(async () =>
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
        //            Device.BeginInvokeOnMainThread(async () =>
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
                            Device.BeginInvokeOnMainThread(async () =>
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
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_VATReturnFormCancelled, AppResources.ZInstructions);
                            });
                        }
                        else
                        {
                            IsLoading = false;
                            if (string.IsNullOrEmpty(WebServiceManager.ErrorMessageForVAT))
                            {
                                Device.BeginInvokeOnMainThread(async () => {
                                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                    _navigationService.GoBack();
                                });
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(async () => {
                                    await _dialogService.ShowMessage(WebServiceManager.ErrorMessageForVAT, AppResources.Information);
                                    // _navigationService.GoBack();
                                    WebServiceManager.ErrorMessageForVAT = string.Empty;
                                });
                            }
                            //Device.BeginInvokeOnMainThread(async () =>
                            //{
                            //    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.ZInstructions);
                            //});
                        }
                    }
                    catch (InternetException ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
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


                        Device.BeginInvokeOnMainThread(async () =>
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
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            _dialogService.ShowMessage(AppResources.ZZGeneralMessage_ReturnRestoredToTheLastBilledVersion, AppResources.Information);
                        });
                    }
                    else
                    {
                        IsLoading = false;
                        if (string.IsNullOrEmpty(WebServiceManager.ErrorMessageForVAT))
                        {
                            Device.BeginInvokeOnMainThread(async () => {
                                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                _navigationService.GoBack();
                            });
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () => {
                                await _dialogService.ShowMessage(WebServiceManager.ErrorMessageForVAT, AppResources.Information);
                                // _navigationService.GoBack();
                                WebServiceManager.ErrorMessageForVAT = string.Empty;
                            });
                        }
                        //Device.BeginInvokeOnMainThread(async () =>
                        //{
                        //    _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        //});
                    }
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
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
            catch(Exception ex)
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
                    Device.BeginInvokeOnMainThread(async () =>
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
                    Device.BeginInvokeOnMainThread(async () =>
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
                        Device.BeginInvokeOnMainThread(async () => {
                            await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                            _navigationService.GoBack();
                        });
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () => {
                            await _dialogService.ShowMessage(WebServiceManager.ErrorMessageForVAT, AppResources.Information);
                            // _navigationService.GoBack();
                            WebServiceManager.ErrorMessageForVAT = string.Empty;
                        });
                    }
                    //Device.BeginInvokeOnMainThread(async () =>
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
        //        Device.BeginInvokeOnMainThread(async () =>
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
        //    catch(Exception ex)
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
            catch (Exception e)
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
                VTTHSetResult vTTHSetResult;
                string periodKey = VATDeclarationData.d.Periodkeyz;
                string TxnTp = VATDeclarationData.d.TxnTpz;
                string status = VATDeclarationData.d.Statusz;
                if (status == "E057" || status == "E0057" || status == "E058" || status == "E0058")
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_ReturnUnderReviewWithGAZT, AppResources.Information);
                    });
                }
                string FormBundleNumber = VATDeclarationData.d.Fbnum;
                string Gpart = VATDeclarationData.d.Gpart;
                string periodfrom;
                string periodto;
                TaxpayerPeriodFromDate = JsonConvert.DeserializeObject<DateTime>(@"""" + VATDeclarationData.d.Abrzu + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                TaxpayerPeriodToDate = JsonConvert.DeserializeObject<DateTime>(@"""" + VATDeclarationData.d.Abrzo + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                //if (App.IsArabic)
                //{
                //    periodfrom = JsonConvert.DeserializeObject<DateTime>(@"""" + VATDeclarationData.d.Abrzu + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                //    periodto = JsonConvert.DeserializeObject<DateTime>(@"""" + VATDeclarationData.d.Abrzo + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                //    TaxpayerPeriodFromDate = UtilityManager.ToArabicDate(periodfrom);
                //    TaxpayerPeriodToDate = UtilityManager.ToArabicDate(periodto);
                //}
                //else
                //{
                //    TaxpayerPeriodFromDate = JsonConvert.DeserializeObject<DateTime>(@"""" + VATDeclarationData.d.Abrzu + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                //    TaxpayerPeriodToDate = JsonConvert.DeserializeObject<DateTime>(@"""" + VATDeclarationData.d.Abrzo + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                //}
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
                                CarriedValueString = AppResources.ZVatCorrectionsfrompreviousperiod.Replace("±",CorrectionPeriodAmount+" ± ");
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
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        IsDeclarationCheckedForInstruction = true;
                    });
                }
                if (VATDeclarationData.d.ConfStp2 == "1")
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        IsCheckedTaxPayerDetailsInfo = true;
                    });
                }
                if (VATDeclarationData.d.DecFg == "1")
                {
                    Device.BeginInvokeOnMainThread(async () =>
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
                if (App.ICRStatus == "E0001" || App.ICRStatus == "E0045" || (App.ICRStatus == "E0006") || App.ICRStatus == "E0058" || App.ICRStatus == "E0055")
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
                            VATNewModelFor5Percent= VATDeclarationData.d.VATPERITEMSet.results.Where(x => x.Type == "003").FirstOrDefault();
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
                //});
                //await Task.Run(() =>
                //{
                //    IsLoading = false;
                //});
                //int j = 5;
                //List<CreditCarried> creditsCrarriedDummy = new List<CreditCarried>();
                //CreditCarriedsList = new List<CreditCarried>();
                //for (j = 0; j < 6; j++)
                //{
                //    CreditCarried m = new CreditCarried();
                //    m.SerialNumber = "0001";
                //    m.ReturnReferenceNumber = "000000000001";
                //    m.DocumentNumber = "0102000010202";
                //    m.Amount = "100000000,00";
                //    creditsCrarriedDummy.Add(m);
                //}
                //CreditCarriedsList = creditsCrarriedDummy;
                //int k = 5;
                //List<VATAttachments> vatAttachment = new List<VATAttachments>();
                //VatAttachmentsList = new List<VATAttachments>();
                //for (k = 0; k < 6; k++)
                //{
                //    VATAttachments m = new VATAttachments();
                //    m.Id = "0001";
                //    m.DocumentName = "Test-Document.pdf";
                //    m.Size = "20.00";
                //    vatAttachment.Add(m);
                //}
                //VatAttachmentsList = vatAttachment;
                ManageThePreperiodcorrSwitch();
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
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
        private List<String> _DummyListOfActionButtonsApplicable;
        public List<String> DummyListOfActionButtonsApplicable
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
        private List<String> _ActualListOfActionButtonsApplicable;
        public List<String> ActualListOfActionButtonsApplicable
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
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await ManageEnabledAsyncProperty(true);
                            });
                        }
                        await SetButtons(VATDeclarationData);
                        return response;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
                return response;
            }
            catch (InternetException ex)
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
            catch (Exception ex)
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
            catch(Exception ex)
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
            catch(Exception ex)
            {

            }
            return vATDeclarationD;
        }

        public VATDeclarationD SetRemainingData(VATDeclarationD vATDeclarationD)
        {
            if (!String.IsNullOrEmpty(vATDeclarationD.StdsalesAmt))
            {
                vATDeclarationD.StdsalesAmt = vATDeclarationD.StdsalesAmt.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.StdsalesAdj))
            {
                vATDeclarationD.StdsalesAdj = vATDeclarationD.StdsalesAdj.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.SalesGccAmt))
            {
                vATDeclarationD.SalesGccAmt = vATDeclarationD.SalesGccAmt.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.SalesGccAdj))
            {
                vATDeclarationD.SalesGccAdj = vATDeclarationD.SalesGccAdj.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.ZerosalesAmt))
            {
                vATDeclarationD.ZerosalesAmt = vATDeclarationD.ZerosalesAmt.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.ZerosalesAdj))
            {
                vATDeclarationD.ZerosalesAdj = vATDeclarationD.ZerosalesAdj.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.ExportsAmt))
            {
                vATDeclarationD.ExportsAmt = vATDeclarationD.ExportsAmt.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.ExportsAdj))
            {
                vATDeclarationD.ExportsAdj = vATDeclarationD.ExportsAdj.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.ExemptsalesAmt))
            {
                vATDeclarationD.ExemptsalesAmt = vATDeclarationD.ExemptsalesAmt.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.ExemptsalesAdj))
            {
                vATDeclarationD.ExemptsalesAdj = vATDeclarationD.ExemptsalesAdj.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.StdpurchaseAmt))
            {
                vATDeclarationD.StdpurchaseAmt = vATDeclarationD.StdpurchaseAmt.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.StdpurchaseAdj))
            {
                vATDeclarationD.StdpurchaseAdj = vATDeclarationD.StdpurchaseAdj.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.ImportspaidAmt))
            {
                vATDeclarationD.ImportspaidAmt = vATDeclarationD.ImportspaidAmt.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.ImportspaidAdj))
            {
                vATDeclarationD.ImportspaidAdj = vATDeclarationD.ImportspaidAdj.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.ImportsaccAmt))
            {
                vATDeclarationD.ImportsaccAmt = vATDeclarationD.ImportsaccAmt.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.ImportsaccAdj))
            {
                vATDeclarationD.ImportsaccAdj = vATDeclarationD.ImportsaccAdj.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.ZeropurchaseAmt))
            {
                vATDeclarationD.ZeropurchaseAmt = vATDeclarationD.ZeropurchaseAmt.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.ZeropurchaseAdj))
            {
                vATDeclarationD.ZeropurchaseAdj = vATDeclarationD.ZeropurchaseAdj.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.ExemptpurchaseAmt))
            {
                vATDeclarationD.ExemptpurchaseAmt = vATDeclarationD.ExemptpurchaseAmt.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.ExemptpurchaseAdj))
            {
                vATDeclarationD.ExemptpurchaseAdj = vATDeclarationD.ExemptpurchaseAdj.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.Preperiodcorr))
            {
                vATDeclarationD.Preperiodcorr = vATDeclarationD.Preperiodcorr.Replace(",", "");
            }
            if (!String.IsNullOrEmpty(vATDeclarationD.StdsalesAmt))
            {
                vATDeclarationD.StdsalesAmt = vATDeclarationD.StdsalesAmt.Replace(",", "");
            }
            return vATDeclarationD;
        }
        public VATDeclarationD SetDataForPost(VATDeclarationD vATDeclarationD)
        {
            try
            {
                if (!String.IsNullOrEmpty(TotalsalesAmt))
                {
                    vATDeclarationD.TotalsalesAmt = !TotalsalesAmt.Contains(",") ? TotalsalesAmt : TotalsalesAmt.Replace(",", "");
                }
                if (!String.IsNullOrEmpty(TotalsalesAdj))
                {
                    vATDeclarationD.TotalsalesAdj = !TotalsalesAdj.Contains(",") ? TotalsalesAdj : TotalsalesAdj.Replace(",", "");
                }
                if (!String.IsNullOrEmpty(TotalpurchaseAmt))
                {
                    vATDeclarationD.TotalpurchaseAmt = !TotalpurchaseAmt.Contains(",") ? TotalpurchaseAmt : TotalpurchaseAmt.Replace(",", "");
                }
                if (!String.IsNullOrEmpty(TotalpurchaseAdj))
                {
                    vATDeclarationD.TotalpurchaseAdj = !TotalpurchaseAdj.Contains(",") ? TotalpurchaseAdj : TotalpurchaseAdj.Replace(",", "");
                }
                if (!String.IsNullOrEmpty(StdsalesVat))
                {
                    vATDeclarationD.StdsalesVat = !StdsalesVat.Contains(",") ? StdsalesVat : StdsalesVat.Replace(",", "");
                }
                if (!String.IsNullOrEmpty(TotalsalesVat))
                {
                    vATDeclarationD.TotalsalesVat = !TotalsalesVat.Contains(",") ? TotalsalesVat : TotalsalesVat.Replace(",", "");
                }
                if (!String.IsNullOrEmpty(StdpurchasesVat))
                {
                    vATDeclarationD.StdpurchasesVat = !StdpurchasesVat.Contains(",") ? StdpurchasesVat : StdpurchasesVat.Replace(",", "");
                }
                if (!String.IsNullOrEmpty(ImportspaidVat))
                {
                    vATDeclarationD.ImportspaidVat = !ImportspaidVat.Contains(",") ? ImportspaidVat : ImportspaidVat.Replace(",", "");
                }
                if (!String.IsNullOrEmpty(ImportsaccVat))
                {
                    vATDeclarationD.ImportsaccVat = !ImportsaccVat.Contains(",") ? ImportsaccVat : ImportsaccVat.Replace(",", "");
                }
                if (!String.IsNullOrEmpty(TotalpurchaseVat))
                {
                    vATDeclarationD.TotalpurchaseVat = !TotalpurchaseVat.Contains(",") ? TotalpurchaseVat : TotalpurchaseVat.Replace(",", "");
                }
                if (!String.IsNullOrEmpty(TotaldueVat))
                {
                    vATDeclarationD.TotaldueVat = !TotaldueVat.Contains(",") ? TotaldueVat : TotaldueVat.Replace(",", "");
                }
                if (!String.IsNullOrEmpty(Preperiodcorr))
                {
                    vATDeclarationD.Preperiodcorr = !Preperiodcorr.Contains(",") ? Preperiodcorr : Preperiodcorr.Replace(",", "");
                }
                if (!String.IsNullOrEmpty(CreditVat))
                {
                    vATDeclarationD.CreditVat = !CreditVat.Contains(",") ? CreditVat : CreditVat.Replace(",", "");
                }
                if (!String.IsNullOrEmpty(NetdueVat))
                {
                    vATDeclarationD.NetdueVat = !NetdueVat.Contains(",") ? NetdueVat : NetdueVat.Replace(",", "");
                }




            }
            catch (Exception ex)
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
                if (!String.IsNullOrEmpty(Amount) && !String.IsNullOrEmpty(Adjustment) && Amount != "." && Adjustment != ".")
                {
                    if (!Amount.Contains("-") && !Adjustment.Contains("-"))
                    {
                        Double dAmount = string.IsNullOrEmpty(Amount) ? 0 : Convert.ToDouble(Amount);
                        Double dAdjustment = string.IsNullOrEmpty(Adjustment) ? 0 : Convert.ToDouble(Adjustment);
                        Double dVATRate = Convert.ToDouble(VATRate002);
                        VATAmount = Convert.ToDouble((((dAmount - dAdjustment) * dVATRate) / 100)).ToString();
                        if (VATAmount == "0")
                        {
                            VATAmount = "0.00";
                        }
                    }
                }
                if (!String.IsNullOrEmpty(VATAmount) && VATAmount != "0.00")
                {
                    VATAmount = Math.Round(Convert.ToDecimal(VATAmount), 2).ToString();
                    VATAmount = UtilityManager.GetCommaSeparatedAmount(VATAmount);
                }
                bool isTrue = IsTextNullOrEmpty(VATAmount);
                VATAmount = isTrue ? "0.00" : VATAmount;
                return VATAmount;
            }
            catch (Exception ex)
            {
            }
            return VATAmount;
        }


        public string StandardRatedSalesVatAmountForNewChangeRate(string Amount, string Adjustment,string VatRate)
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
                if (!String.IsNullOrEmpty(Amount) && !String.IsNullOrEmpty(Adjustment) && !String.IsNullOrEmpty(VatRate) && Amount != "." && Adjustment != ".")
                {
                    if (!Amount.Contains("-") && !Adjustment.Contains("-"))
                    {
                        Double dAmount = string.IsNullOrEmpty(Amount) ? 0 : Convert.ToDouble(Amount);
                        Double dAdjustment = string.IsNullOrEmpty(Adjustment) ? 0 : Convert.ToDouble(Adjustment);
                        Double dVATRate = Convert.ToDouble(VatRate);
                        VATAmount = Convert.ToDouble((((dAmount - dAdjustment) * dVATRate) / 100)).ToString();
                        if (VATAmount == "0")
                        {
                            VATAmount = "0.00";
                        }
                    }
                }
                if (!String.IsNullOrEmpty(VATAmount) && VATAmount != "0.00")
                {
                    VATAmount = Math.Round(Convert.ToDecimal(VATAmount), 2).ToString();
                    VATAmount = UtilityManager.GetCommaSeparatedAmount(VATAmount);
                }
                bool isTrue = IsTextNullOrEmpty(VATAmount);
                VATAmount = isTrue ? "0.00" : VATAmount;
                return VATAmount;
            }
            catch (Exception ex)
            {
            }
            return VATAmount;
        }


        public string TotalAmountForSixVar(string Amount1, string Amount2, string Amount3, string Amount4, string Amount5, string Amount6)
        {
            String TotalAmount = "0.00";
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
                            TotalAmount = Convert.ToDouble(((String.IsNullOrEmpty(Amount1) ? 0.00 : Convert.ToDouble(Amount1)) + (String.IsNullOrEmpty(Amount2) ? 0.00 : Convert.ToDouble(Amount2)) + (String.IsNullOrEmpty(Amount3) ? 0.00 : Convert.ToDouble(Amount3)) + (String.IsNullOrEmpty(Amount4) ? 0.00 : Convert.ToDouble(Amount4)) + (String.IsNullOrEmpty(Amount5) ? 0.00 : Convert.ToDouble(Amount5)) + (String.IsNullOrEmpty(Amount6) ? 0.00 : Convert.ToDouble(Amount6)))).ToString();
                            if (TotalAmount == "0")
                            {
                                TotalAmount = "0.00";
                            }
                        }
                    }
                }
                if (!String.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
                {
                    TotalAmount = Math.Round(Convert.ToDecimal(TotalAmount), 2).ToString();
                    TotalAmount = UtilityManager.GetCommaSeparatedAmount(TotalAmount);
                }
                bool isTrue = IsTextNullOrEmpty(TotalAmount);
                TotalAmount = isTrue ? "0.00" : TotalAmount;
                return TotalAmount;
            }
            catch (Exception ex)
            {
            }
            return TotalAmount;
        }

        public string TotalAmountForSixVarForNegative(string Amount1, string Amount2, string Amount3, string Amount4, string Amount5, string Amount6)
        {
            String TotalAmount = "0.00";
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
                            TotalAmount = Convert.ToDouble(((String.IsNullOrEmpty(Amount1) ? 0.00 : Convert.ToDouble(Amount1)) + (String.IsNullOrEmpty(Amount2) ? 0.00 : Convert.ToDouble(Amount2)) + (String.IsNullOrEmpty(Amount3) ? 0.00 : Convert.ToDouble(Amount3)) + (String.IsNullOrEmpty(Amount4) ? 0.00 : Convert.ToDouble(Amount4)) + (String.IsNullOrEmpty(Amount5) ? 0.00 : Convert.ToDouble(Amount5)) + (String.IsNullOrEmpty(Amount6) ? 0.00 : Convert.ToDouble(Amount6)))).ToString();
                            if (TotalAmount == "0")
                            {
                                TotalAmount = "0.00";
                            }
                    }
                }
                if (!String.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
                {
                    TotalAmount = Math.Round(Convert.ToDecimal(TotalAmount), 2).ToString();
                    TotalAmount = UtilityManager.GetCommaSeparatedAmount(TotalAmount);
                }
                bool isTrue = IsTextNullOrEmpty(TotalAmount);
                TotalAmount = isTrue ? "0.00" : TotalAmount;
                return TotalAmount;
            }
            catch (Exception ex)
            {
            }
            return TotalAmount;
        }

        public string TotalAmountForEightVar(string Amount1, string Amount2, string Amount3, string Amount4, string Amount5, string Amount6, string Amount7, string Amount8)
        {
            String TotalAmount = "0.00";
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
                            TotalAmount = Convert.ToDouble(((String.IsNullOrEmpty(Amount1) ? 0.00 : Convert.ToDouble(Amount1)) + (String.IsNullOrEmpty(Amount2) ? 0.00 : Convert.ToDouble(Amount2)) + (String.IsNullOrEmpty(Amount3) ? 0.00 : Convert.ToDouble(Amount3)) + (String.IsNullOrEmpty(Amount4) ? 0.00 : Convert.ToDouble(Amount4)) + (String.IsNullOrEmpty(Amount5) ? 0.00 : Convert.ToDouble(Amount5)) + (String.IsNullOrEmpty(Amount6) ? 0.00 : Convert.ToDouble(Amount6)) + (String.IsNullOrEmpty(Amount7) ? 0.00 : Convert.ToDouble(Amount7)) + (String.IsNullOrEmpty(Amount8) ? 0.00 : Convert.ToDouble(Amount8)))).ToString();
                            if (TotalAmount == "0")
                            {
                                TotalAmount = "0.00";
                            }
                        }
                    }
                }
                if (!String.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
                {
                    TotalAmount = Math.Round(Convert.ToDecimal(TotalAmount), 2).ToString();
                    TotalAmount = UtilityManager.GetCommaSeparatedAmount(TotalAmount);
                }
                bool isTrue = IsTextNullOrEmpty(TotalAmount);
                TotalAmount = isTrue ? "0.00" : TotalAmount;
                return TotalAmount;
            }
            catch (Exception ex)
            {
            }
            return TotalAmount;
        }


        public string TotalAmount(string Amount1, string Amount2, string Amount3, string Amount4, string Amount5)
        {
            String TotalAmount = "0.00";
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
                            TotalAmount = Convert.ToDouble(((String.IsNullOrEmpty(Amount1) ? 0.00 : Convert.ToDouble(Amount1)) + (String.IsNullOrEmpty(Amount2) ? 0.00 : Convert.ToDouble(Amount2)) + (String.IsNullOrEmpty(Amount3) ? 0.00 : Convert.ToDouble(Amount3)) + (String.IsNullOrEmpty(Amount4) ? 0.00 : Convert.ToDouble(Amount4)) + (String.IsNullOrEmpty(Amount5) ? 0.00 : Convert.ToDouble(Amount5)))).ToString();
                            if (TotalAmount == "0")
                            {
                                TotalAmount = "0.00";
                            }
                        }
                    }
                }
                if (!String.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
                {
                    TotalAmount = Math.Round(Convert.ToDecimal(TotalAmount), 2).ToString();
                    TotalAmount = UtilityManager.GetCommaSeparatedAmount(TotalAmount);
                }
                bool isTrue = IsTextNullOrEmpty(TotalAmount);
                TotalAmount = isTrue ? "0.00" : TotalAmount;
                return TotalAmount;
            }
            catch (Exception ex)
            {
            }
            return TotalAmount;
        }

        public string TotalAdjustmentForSixVar(string Adjustment1, string Adjustment2, string Adjustment3, string Adjustment4, string Adjustment5, string Adjustment6)
        {
            String TotalAmount = "0.00";
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
                            TotalAmount = Convert.ToDouble(((String.IsNullOrEmpty(Adjustment1) ? 0 : Convert.ToDouble(Adjustment1)) + (String.IsNullOrEmpty(Adjustment2) ? 0 : Convert.ToDouble(Adjustment2)) + (String.IsNullOrEmpty(Adjustment3) ? 0 : Convert.ToDouble(Adjustment3)) + (String.IsNullOrEmpty(Adjustment4) ? 0 : Convert.ToDouble(Adjustment4)) + (String.IsNullOrEmpty(Adjustment5) ? 0 : Convert.ToDouble(Adjustment5)) + (String.IsNullOrEmpty(Adjustment6) ? 0 : Convert.ToDouble(Adjustment6)))).ToString();
                            if (TotalAmount == "0")
                            {
                                TotalAmount = "0.00";
                            }
                        }
                    }
                }
                if (!String.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
                {
                    TotalAmount = Math.Round(Convert.ToDecimal(TotalAmount), 2).ToString();
                    TotalAmount = UtilityManager.GetCommaSeparatedAmount(TotalAmount);
                }
                bool isTrue = IsTextNullOrEmpty(TotalAmount);
                TotalAmount = isTrue ? "0.00" : TotalAmount;
                return TotalAmount;
            }
            catch (Exception ex)
            {
            }
            return TotalAmount;
        }

        public string TotalAdjustment(string Adjustment1, string Adjustment2, string Adjustment3, string Adjustment4, string Adjustment5)
        {
            String TotalAmount = "0.00";
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
                            TotalAmount = Convert.ToDouble(((String.IsNullOrEmpty(Adjustment1) ? 0 : Convert.ToDouble(Adjustment1)) + (String.IsNullOrEmpty(Adjustment2) ? 0 : Convert.ToDouble(Adjustment2)) + (String.IsNullOrEmpty(Adjustment3) ? 0 : Convert.ToDouble(Adjustment3)) + (String.IsNullOrEmpty(Adjustment4) ? 0 : Convert.ToDouble(Adjustment4)) + (String.IsNullOrEmpty(Adjustment5) ? 0 : Convert.ToDouble(Adjustment5)))).ToString();
                            if (TotalAmount == "0")
                            {
                                TotalAmount = "0.00";
                            }
                        }
                    }
                }
                if (!String.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
                {
                    TotalAmount = Math.Round(Convert.ToDecimal(TotalAmount), 2).ToString();
                    TotalAmount = UtilityManager.GetCommaSeparatedAmount(TotalAmount);
                }
                bool isTrue = IsTextNullOrEmpty(TotalAmount);
                TotalAmount = isTrue ? "0.00" : TotalAmount;
                return TotalAmount;
            }
            catch (Exception ex)
            {
            }
            return TotalAmount;
        }

        public string GetSingleAmount(string Amount1)
        {
            String TotalAmount = "0.00";
            try
            {
                if (!string.IsNullOrEmpty(Amount1) && Amount1.Contains(","))
                {
                    Amount1 = Amount1.Replace(",", "");
                }
                
                if (!String.IsNullOrEmpty(Amount1))
                {
                    TotalAmount = Convert.ToDouble((Convert.ToDouble(Amount1))).ToString();
                    if (TotalAmount == "0")
                    {
                        TotalAmount = "0.00";
                    }
                }
                if (!String.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
                {
                    TotalAmount = Math.Round(Convert.ToDecimal(TotalAmount), 2).ToString();
                    TotalAmount = UtilityManager.GetCommaSeparatedAmount(TotalAmount);
                }
                bool isTrue = IsTextNullOrEmpty(TotalAmount);
                TotalAmount = isTrue ? "0.00" : TotalAmount;
            }
            catch (Exception ex)
            {
            }
            return TotalAmount;
        }

        public string AddTwoAmount(string Amount1, string Amount2)
        {
            String TotalAmount = "0.00";
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
                if (String.IsNullOrEmpty(Amount1))
                {
                    Amount1 = "0.00";
                }
                if (String.IsNullOrEmpty(Amount2))
                {
                    Amount2 = "0.00";
                }
                if (!String.IsNullOrEmpty(Amount1) && !String.IsNullOrEmpty(Amount2))
                {
                    TotalAmount = Convert.ToDouble((Convert.ToDouble(Amount1) + Convert.ToDouble(Amount2))).ToString();
                    if (TotalAmount == "0")
                    {
                        TotalAmount = "0.00";
                    }
                }
                if (!String.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
                {
                    TotalAmount = Math.Round(Convert.ToDecimal(TotalAmount), 2).ToString();
                    TotalAmount = UtilityManager.GetCommaSeparatedAmount(TotalAmount);
                }
                bool isTrue = IsTextNullOrEmpty(TotalAmount);
                TotalAmount = isTrue ? "0.00" : TotalAmount;
            }
            catch (Exception ex)
            {
            }
            return TotalAmount;
        }

        public string TotalVatAmount(string Amount1, string Amount2, string Amount3)
        {
            String TotalAmount = "0.00";
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
                if (String.IsNullOrEmpty(Amount1))
                {
                    Amount1 = "0.00";
                }
                if (String.IsNullOrEmpty(Amount2))
                {
                    Amount2 = "0.00";
                }
                if (String.IsNullOrEmpty(Amount3))
                {
                    Amount3 = "0.00";
                }
                if (!String.IsNullOrEmpty(Amount1) && !String.IsNullOrEmpty(Amount2) && !String.IsNullOrEmpty(Amount3))
                {
                    TotalAmount = Convert.ToDouble((Convert.ToDouble(Amount1) + Convert.ToDouble(Amount2) + Convert.ToDouble(Amount3))).ToString();
                    if (TotalAmount == "0")
                    {
                        TotalAmount = "0.00";
                    }
                }
                if (!String.IsNullOrEmpty(TotalAmount) && TotalAmount != "0.00")
                {
                    TotalAmount = Math.Round(Convert.ToDecimal(TotalAmount), 2).ToString();
                    TotalAmount = UtilityManager.GetCommaSeparatedAmount(TotalAmount);
                }
                bool isTrue = IsTextNullOrEmpty(TotalAmount);
                TotalAmount = isTrue ? "0.00" : TotalAmount;
            }
            catch (Exception ex)
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
                        Double dAmount = string.IsNullOrEmpty(Amount) ? 0 : Convert.ToDouble(Amount);
                        Double dAdjustment = string.IsNullOrEmpty(Adjustment) ? 0 : Convert.ToDouble(Adjustment);
                        Double dVATRate = Convert.ToDouble(VATRate002);
                        VATAmount = Convert.ToDouble((((dAmount - dAdjustment) * dVATRate) / 100)).ToString();
                        if (VATAmount == "0")
                        {
                            VATAmount = "0.00";
                        }
                    }
                }
                if (!String.IsNullOrEmpty(VATAmount) && VATAmount != "0.00")
                {
                    VATAmount = Math.Round(Convert.ToDecimal(VATAmount), 2).ToString();
                    VATAmount = UtilityManager.GetCommaSeparatedAmount(VATAmount);
                }
                bool isTrue = IsTextNullOrEmpty(VATAmount);
                VATAmount = isTrue ? "0.00" : VATAmount;
                return VATAmount;
            }
            catch (Exception ex)
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
                        Double dAmount = string.IsNullOrEmpty(Amount) ? 0 : Convert.ToDouble(Amount);
                        Double dAdjustment = string.IsNullOrEmpty(Adjustment) ? 0 : Convert.ToDouble(Adjustment);
                        Double dVATRate001 = Convert.ToDouble(VATRate001);
                        Double dVATRate002 = Convert.ToDouble(VATRate002);
                        VATAmount = Convert.ToDouble((((dAmount * dVATRate001) / 100) - ((dAdjustment * dVATRate002) / 100))).ToString();
                        if (VATAmount == "0")
                        {
                            VATAmount = "0.00";
                        }
                    }
                }
                if (!String.IsNullOrEmpty(VATAmount) && VATAmount != "0.00")
                {
                    VATAmount = Math.Round(Convert.ToDecimal(VATAmount), 2).ToString();
                    VATAmount = UtilityManager.GetCommaSeparatedAmount(VATAmount);
                }
                bool isTrue = IsTextNullOrEmpty(VATAmount);
                VATAmount = isTrue ? "0.00" : VATAmount;
                return VATAmount;
            }
            catch (Exception ex)
            {
            }
            return VATAmount;
        }

        public string ImportSubjectToVatPaidAtCustomsVatAmountForDesignatedForNewPercentage(string Amount, string Adjustment,string NewVATRate)
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
                        Double dAmount = string.IsNullOrEmpty(Amount) ? 0 : Convert.ToDouble(Amount);
                        Double dAdjustment = string.IsNullOrEmpty(Adjustment) ? 0 : Convert.ToDouble(Adjustment);
                        Double dVATRate002 = string.IsNullOrEmpty(NewVATRate) ? 0 : Convert.ToDouble(NewVATRate);
                        Double dVATRate001 = Convert.ToDouble(VATRate001);
                        dVATRate002 = Convert.ToDouble(NewVATRate);
                        VATAmount = Convert.ToDouble((((dAmount * dVATRate001) / 100) - ((dAdjustment * dVATRate002) / 100))).ToString();
                        if (VATAmount == "0")
                        {
                            VATAmount = "0.00";
                        }
                    }
                }
                if (!String.IsNullOrEmpty(VATAmount) && VATAmount != "0.00")
                {
                    VATAmount = Math.Round(Convert.ToDecimal(VATAmount), 2).ToString();
                    VATAmount = UtilityManager.GetCommaSeparatedAmount(VATAmount);
                }
                bool isTrue = IsTextNullOrEmpty(VATAmount);
                VATAmount = isTrue ? "0.00" : VATAmount;
                return VATAmount;
            }
            catch (Exception ex)
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
                        Double dAmount = string.IsNullOrEmpty(Amount) ? 0 : Convert.ToDouble(Amount);
                        Double dAdjustment = string.IsNullOrEmpty(Adjustment) ? 0 : Convert.ToDouble(Adjustment);
                        Double dVATRate = Convert.ToDouble(VATRate002);
                        VATAmount = Convert.ToDouble((((dAmount - dAdjustment) * dVATRate) / 100)).ToString();
                        if (VATAmount == "0")
                        {
                            VATAmount = "0.00";
                        }
                    }
                }
                if (!String.IsNullOrEmpty(VATAmount) && VATAmount != "0.00")
                {
                    VATAmount = Math.Round(Convert.ToDecimal(VATAmount), 2).ToString();
                    VATAmount = UtilityManager.GetCommaSeparatedAmount(VATAmount);
                }
                bool isTrue = IsTextNullOrEmpty(VATAmount);
                VATAmount = isTrue ? "0.00" : VATAmount;
                return VATAmount;
            }
            catch (Exception ex)
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
                    Double dCurrentPeriod = string.IsNullOrEmpty(CurrentPeriod) ? 0 : Convert.ToDouble(CurrentPeriod);
                    Double dPreviousPeriod = string.IsNullOrEmpty(PreviousPeriod) ? 0 : Convert.ToDouble(PreviousPeriod);
                    Double dForwardFromPreviousPeriod = string.IsNullOrEmpty(ForwardFromPreviousPeriod) ? 0 : Convert.ToDouble(ForwardFromPreviousPeriod);
                    NetVatDue = Convert.ToDouble((dCurrentPeriod + dPreviousPeriod + dForwardFromPreviousPeriod)).ToString();
                    if (NetVatDue == "0")
                    {
                        NetVatDue = "0.00";
                    }
                }
                catch
                {
                }
                if (!String.IsNullOrEmpty(NetVatDue) && NetVatDue != "0.00")
                {
                    NetVatDue = Math.Round(Convert.ToDecimal(NetVatDue), 2).ToString();
                    NetVatDue = UtilityManager.GetCommaSeparatedAmount(NetVatDue);
                }
                bool isTrue = IsTextNullOrEmpty(NetVatDue);
                NetVatDue = isTrue ? "0.00" : NetVatDue;
                return NetVatDue;
            }
            catch (Exception ex)
            {
            }
            return NetVatDue;
        }
        #endregion
        #endregion
        public void SetButtonStrings(String ButtonName)
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
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(ex.Message, AppResources.Information);
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

