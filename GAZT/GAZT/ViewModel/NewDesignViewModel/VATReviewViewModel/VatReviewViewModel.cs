using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Manager;
using EGAZT.Models;
using EGAZT.Models.VatReviewModel;
using EGAZT.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel;
using EGAZT.Views.NewDesign;
using EGAZT.Views.NewDesign.Common;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Views.NewDesign.GenericPickers;
using EGAZT.Views.NewDesign.VATDeclarationPages;
using EGAZT.Views.NewDesign.VatReview;
using EGAZT.Views.NewDesign.ZakatInstalmentPlan;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using static EGAZT.Models.VatReviewModel.VATObjectionSummaryInputModel;
using Metadata = EGAZT.Models.VatReviewModel.Metadata;

namespace EGAZT.ViewModel.NewDesignViewModel.VatReviewViewModel
{
    [Preserve(AllMembers = true)]
    public class VatReviewViewModel : ViewModelBase
    {
        #region Enums

        enum PagesEnum
        {
            ReviewReason,
            ReviewDetails,
            ReportDetails,
            LateFiling,
            SecurityPayments,
            Declaration,
            Summary
        }

        public enum PickerEnum
        {
            ReviewReason,
            ReviewSubReason,
            ApplicationReferenceNumber,
            IDType,
        }

        #endregion

        #region Commands

        public ICommand ReviewReasonConBtnTapped { get; set; }
        public ICommand ReviewDetailsConBtnTapped { get; set; }
        public ICommand ReportDetailsConBtnTapped { get; set; }
        public ICommand LateFilingContdButtonClicked { get; set; }
        public ICommand SecurityPaymentConBtnTapped { get; set; }
        public ICommand DeclarationConBtnTapped { get; set; }
        public ICommand SummaryConBtnTapped { get; set; }
        public ICommand CloseClick { get; set; }
        public ICommand GoBackClick { get; set; }
        public ICommand GoBackToReviewReason { get; set; }
        public ICommand GoBackToReviewDetails { get; set; }
        public ICommand GoBackToDeclaration { get; set; }
        public ICommand ViewApplicationTapped { get; set; }
        public ICommand NewAttachmentTapped { get; set; }
        public ICommand NewBankGuranteeAttachmentTapped { get; set; }
        public ICommand LateFilingAttachmentTapped { get; set; }
        public ICommand ReviewReasonCommand { get; set; }
        public ICommand SubReviewReasonCommand { get; set; }
        public ICommand ShowDatePicker { get; set; }
        public ICommand IdTypeSpinnerTapped { get; set; }
        public ICommand ApplicationNumRefCommand { get; set; }
        public ICommand SadadGenerateBtnTapped { get; set; }
        public ICommand GoBackToReportDetails { get; set; }
        public ICommand GoBackToLateFilingDetails { get; set; }
        public ICommand onMoreOptionClicked { get; set; }

        #endregion

        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        int selectedPage = (int)PagesEnum.ReviewReason;

        public string lastFulfilmentDate = string.Empty;
        public string OverdueFlag = string.Empty;

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

        private bool _isBackVisible = false;

        public bool IsBackVisible
        {
            get { return _isBackVisible; }
            set
            {
                if (_isBackVisible == value) return;

                _isBackVisible = value;
                RaisePropertyChanged("IsBackVisible");
            }
        }

        private bool _isLoading = false;

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (_isLoading == value) return;

                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }

        private bool _isReviewReasonVisible = false;

        public bool ReviewReasonVisible
        {
            get { return _isReviewReasonVisible; }
            set
            {
                if (_isReviewReasonVisible == value) return;

                _isReviewReasonVisible = value;
                RaisePropertyChanged("ReviewReasonVisible");
            }
        }

        private bool _reviewDetailsVisible = false;

        public bool ReviewDetailsVisible
        {
            get { return _reviewDetailsVisible; }
            set
            {
                if (_reviewDetailsVisible == value) return;

                _reviewDetailsVisible = value;
                RaisePropertyChanged("ReviewDetailsVisible");
            }
        }

        private bool _reportDetailsVisible = false;

        public bool ReportDetailsVisible
        {
            get { return _reportDetailsVisible; }
            set
            {
                if (_reportDetailsVisible == value) return;

                _reportDetailsVisible = value;
                RaisePropertyChanged("ReportDetailsVisible");
            }
        }

        private bool _securityPaymentVisible = false;

        public bool SecurityPaymentVisible
        {
            get { return _securityPaymentVisible; }
            set
            {
                if (_securityPaymentVisible == value) return;

                _securityPaymentVisible = value;
                RaisePropertyChanged("SecurityPaymentVisible");
            }
        }

        private bool _lateFilingObjectionsVisible = false;

        public bool LateFilingObjectionsVisible
        {
            get { return _lateFilingObjectionsVisible; }
            set
            {
                if (_lateFilingObjectionsVisible == value) return;

                _lateFilingObjectionsVisible = value;
                RaisePropertyChanged("LateFilingObjectionsVisible");
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
                RaisePropertyChanged("DeclarationVisible");
            }
        }

        private bool _summaryVisible = false;

        public bool SummaryVisible
        {
            get { return _summaryVisible; }
            set
            {
                if (_summaryVisible == value) return;

                _summaryVisible = value;
                RaisePropertyChanged("SummaryVisible");
            }
        }

        private bool _isSadadSecuritySelected = false;

        public bool IsSadadSecuritySelected
        {
            get { return _isSadadSecuritySelected; }
            set
            {
                if (_isSadadSecuritySelected == value) return;

                _isSadadSecuritySelected = value;
                RaisePropertyChanged("IsSadadSecuritySelected");
            }
        }

        private bool _sadadGenerateBtnVisible = false;
        public bool SadadGenerateBtnVisible
        {
            get { return _sadadGenerateBtnVisible; }
            set
            {
                if (_sadadGenerateBtnVisible == value) return;

                _sadadGenerateBtnVisible = value;
                RaisePropertyChanged("SadadGenerateBtnVisible");
            }
        }
        private bool _sadadGenerateProgressVisible = false;
        public bool SadadGenerateProgressVisible
        {
            get { return _sadadGenerateProgressVisible; }
            set
            {
                if (_sadadGenerateProgressVisible == value) return;

                _sadadGenerateProgressVisible = value;
                RaisePropertyChanged("SadadGenerateProgressVisible");
            }
        }
        private bool _sadadAmountVisible = false;
        public bool SadadAmountVisible
        {
            get { return _sadadAmountVisible; }
            set
            {
                if (_sadadAmountVisible == value) return;

                _sadadAmountVisible = value;
                RaisePropertyChanged("SadadAmountVisible");
            }
        }

        private bool _isBankGurantSecuritySelected = false;

        public bool IsBankGurantSecuritySelected
        {
            get { return _isBankGurantSecuritySelected; }
            set
            {
                if (_isBankGurantSecuritySelected == value) return;

                _isBankGurantSecuritySelected = value;
                RaisePropertyChanged("IsBankGurantSecuritySelected");
            }
        }

        private bool _isSecurityAmountMorethanZero = false;

        public bool IsSecurityAmountMorethanZero
        {
            get { return _isSecurityAmountMorethanZero; }
            set
            {
                if (_isSecurityAmountMorethanZero == value) return;

                _isSecurityAmountMorethanZero = value;
                RaisePropertyChanged("IsSecurityAmountMorethanZero");
            }
        }

        private string _pickedDate = "";

        public string PickedDate
        {
            get { return _pickedDate; }
            set
            {
                if (_pickedDate == value) return;

                _pickedDate = value;
                RaisePropertyChanged("PickedDate");
            }
        }
        private string _pickedDateFullMonth = "";

        public string PickedDateFullMonth
        {
            get { return _pickedDateFullMonth; }
            set
            {
                if (_pickedDateFullMonth == value) return;

                _pickedDateFullMonth = value;
                RaisePropertyChanged("PickedDateFullMonth");
            }
        }

        private GenericDatePickerModel genericDatePickerModel;

        private string _contactPersonName = "";

        public string ContactPersonName
        {
            get { return _contactPersonName; }
            set
            {
                if (_contactPersonName == value) return;

                _contactPersonName = value;
                RaisePropertyChanged("ContactPersonName");
            }
        }

        public string _reviewReason = "";

        public string ReviewReason
        {
            get { return _reviewReason; }
            set
            {
                if (_reviewReason == value) return;

                _reviewReason = value;
                RaisePropertyChanged("ReviewReason");
            }
        }

        public string _subReviewReason = "";

        public string SubReviewReason
        {
            get { return _subReviewReason; }
            set
            {
                if (_subReviewReason == value) return;

                _subReviewReason = value;
                RaisePropertyChanged("SubReviewReason");
            }
        }

        public string _applicationRefNumber = "";

        public string ApplicationRefNumber
        {
            get { return _applicationRefNumber; }
            set
            {
                if (_applicationRefNumber == value) return;

                _applicationRefNumber = value;
                RaisePropertyChanged("ApplicationRefNumber");
            }
        }

        public DateTime? _requestDate = DateTime.Now;

        public DateTime? RequestDate
        {
            get { return _requestDate; }
            set
            {
                if (_requestDate == value) return;

                _requestDate = value;
                RaisePropertyChanged("RequestDate");
            }
        }

        public string _formattedrequestDate = "";

        public string FormattedRequestDate
        {
            get { return _formattedrequestDate; }
            set
            {
                if (_formattedrequestDate == value) return;

                _formattedrequestDate = value;
                RaisePropertyChanged("FormattedRequestDate");
            }
        }


        public string _formattedTaxperdioFromDate = "";

        public string FormattedTaxperdioFromDate
        {
            get { return _formattedTaxperdioFromDate; }
            set
            {
                if (_formattedTaxperdioFromDate == value) return;

                _formattedTaxperdioFromDate = value;
                RaisePropertyChanged("FormattedTaxperdioFromDate");
            }
        }


        public string _formattedTaxperdioToDate = "";

        public string FormattedTaxperdioToDate
        {
            get { return _formattedTaxperdioToDate; }
            set
            {
                if (_formattedTaxperdioToDate == value) return;

                _formattedTaxperdioToDate = value;
                RaisePropertyChanged("FormattedTaxperdioToDate");
            }
        }

        public string _taxPeriodOfCase = "";

        public string TaxPeriodOfCase
        {
            get { return _taxPeriodOfCase; }
            set
            {
                if (_taxPeriodOfCase == value) return;

                _taxPeriodOfCase = value;
                RaisePropertyChanged("TaxPeriodOfCase");
            }
        }

        public DateTime? _taxPeriodFrom = DateTime.Now;

        public DateTime? TaxPeriodFrom
        {
            get { return _taxPeriodFrom; }
            set
            {
                if (_taxPeriodFrom == value) return;

                _taxPeriodFrom = value;
                RaisePropertyChanged("TaxPeriodFrom");
            }
        }

        public DateTime? _taxPeriodTo = DateTime.Now;

        public DateTime? TaxPeriodTo
        {
            get { return _taxPeriodTo; }
            set
            {
                if (_taxPeriodTo == value) return;

                _taxPeriodTo = value;
                RaisePropertyChanged("TaxPeriodTo");
            }
        }

        public string _penalityAmountInQuestion = "";

        public string PenalityAmountInQuestion
        {
            get { return _penalityAmountInQuestion; }
            set
            {
                if (_penalityAmountInQuestion == value) return;

                _penalityAmountInQuestion = value;
                RaisePropertyChanged("PenalityAmountInQuestion");
            }
        }

        public string _reportDetails = "";

        public string ReportDetails
        {
            get { return _reportDetails; }
            set
            {
                if (_reportDetails == value) return;

                _reportDetails = value;
                RaisePropertyChanged("ReportDetails");
            }
        }
        
        public string _lateFlngDetails = "";

        public string LateFlngDetails
        {
            get { return _lateFlngDetails; }
            set
            {
                if (_lateFlngDetails == value) return;

                _lateFlngDetails = value;
                RaisePropertyChanged("LateFlngDetails");
            }
        }

        public string _sADADNumber = "";

        public string SADADNumber
        {
            get { return _sADADNumber; }
            set
            {
                if (_sADADNumber == value) return;

                _sADADNumber = value;
                RaisePropertyChanged("SADADNumber");
            }
        }
        public string _securityNumber = "";

        public string SecurityNumber
        {
            get { return _securityNumber; }
            set
            {
                if (_securityNumber == value) return;

                _securityNumber = value;
                RaisePropertyChanged("SecurityNumber");
            }
        }



        public string _securityAmount = "";

        public string SecurityAmount
        {
            get { return _securityAmount; }
            set
            {
                if (_securityAmount == value) return;

                _securityAmount = value;
                RaisePropertyChanged("SecurityAmount");
            }
        }

        public string _idNumber = "";

        public string IDNumber
        {
            get { return _idNumber; }
            set
            {
                if (_idNumber == value) return;

                _idNumber = value;
                RaisePropertyChanged("IDNumber");
            }
        }

        private bool isIDVerified = false;

        public bool IsIDVerified
        {
            get { return isIDVerified; }
            set
            {
                if (isIDVerified == value) return;

                isIDVerified = value;
                RaisePropertyChanged("IsIDVerified");
            }
        }

        private string _vatReferanceNumber = string.Empty;
        public string VATReferanceNumber
        {
            get
            {
                return _vatReferanceNumber;
            }
            set
            {
                if (_vatReferanceNumber == value) return;

                _vatReferanceNumber = value;
                RaisePropertyChanged("VATReferanceNumber");
            }
        }
        private bool _contractPersonEditable = false;

        public bool ContractPersonEditable
        {
            get { return _contractPersonEditable; }
            set
            {
                if (_contractPersonEditable == value) return;

                _contractPersonEditable = value;
                RaisePropertyChanged("ContractPersonEditable");
            }
        }

        private string _idType = "";

        public string IDType
        {
            get { return _idType; }
            set
            {
                if (_idType == value) return;

                _idType = value;
                RaisePropertyChanged("IDType");
            }
        }

        private bool _isDOBVisible = false;

        public bool IsDOBVisible
        {
            get { return _isDOBVisible; }
            set
            {
                if (_isDOBVisible == value) return;

                _isDOBVisible = value;
                RaisePropertyChanged("IsDOBVisible");
            }
        }


        private bool isSadadCheckBox1 = false;

        public bool IsSadadCheckBox1
        {
            get { return isSadadCheckBox1; }
            set
            {
                if (isSadadCheckBox1 == value) return;

                isSadadCheckBox1 = value;
                RaisePropertyChanged("IsSadadCheckBox1");
            }
        }


        private bool isSadadCheckBox3 = false;

        public bool IsSadadCheckBox3
        {
            get { return isSadadCheckBox3; }
            set
            {
                if (isSadadCheckBox3 == value) return;
                isSadadCheckBox3 = value;
                RaisePropertyChanged("IsSadadCheckBox3");
            }
        }

        private bool isSadadCheckBox4 = false;

        public bool IsSadadCheckBox4
        {
            get { return isSadadCheckBox4; }
            set
            {
                if (isSadadCheckBox4 == value) return;

                isSadadCheckBox4 = value;
                RaisePropertyChanged("IsSadadCheckBox4");
            }
        }

        private int _MaxIndex = 6;

        public int MaxIndex
        {
            get { return _MaxIndex; }
            set
            {
                if (_MaxIndex == value) return;

                _MaxIndex = value;
                RaisePropertyChanged("MaxIndex");
            }
        }


        private bool isGeneratingFormbundle = false;

        public bool IsGeneratingFormbundle
        {
            get { return isGeneratingFormbundle; }
            set
            {
                if (isGeneratingFormbundle == value) return;

                isGeneratingFormbundle = value;
                RaisePropertyChanged("IsGeneratingFormbundle");
            }
        }


        private bool isSadadRefeshVisible = false;

        public bool IsSadadRefeshVisible
        {
            get { return isSadadRefeshVisible; }
            set
            {
                if (isSadadRefeshVisible == value) return;

                isSadadRefeshVisible = value;
                RaisePropertyChanged("IsSadadRefeshVisible");
            }
        }


        public string securityType = "";
        public string SecurityType
        {
            get
            {


                return securityType;

            }
            set
            {
                if (securityType == value) return;

                securityType = value;
                RaisePropertyChanged("SecurityType");
            }
        }

        private bool isReportDetailsEditable = true;
        public bool IsReportDetailsEditable
        {
            get
            {
                return isReportDetailsEditable;
            }
            set
            {
                if (isReportDetailsEditable == value) return;

                isReportDetailsEditable = value;
                RaisePropertyChanged("IsReportDetailsEditable");
            }
        }
        private bool _isLateFilingDetailsEditable = true;
        public bool IsLateFilingDetailsEditable
        {
            get
            {
                return _isLateFilingDetailsEditable;
            }
            set
            {
                if (_isLateFilingDetailsEditable == value) return;

                _isLateFilingDetailsEditable = value;
                RaisePropertyChanged("IsLateFilingDetailsEditable");
            }
        }
        private bool isDisputeDetailsEditable = true;
        public bool IsDisputeDetailsEditable
        {
            get
            {
                return isDisputeDetailsEditable;
            }
            set
            {
                if (isDisputeDetailsEditable == value) return;

                isDisputeDetailsEditable = value;
                RaisePropertyChanged("IsDisputeDetailsEditable");
            }
        }
        private bool dAPOptionsEditable = true;
        public bool DAPOptionsEditable
        {
            get
            {
                return dAPOptionsEditable;
            }
            set
            {
                if (dAPOptionsEditable == value) return;

                dAPOptionsEditable = value;
                RaisePropertyChanged("DAPOptionsEditable");
            }
        }
        private bool isSecurityPOEditable = true;
        public bool IsSecurityPOEditable
        {
            get
            {
                return isSecurityPOEditable;
            }
            set
            {
                if (isSecurityPOEditable == value) return;

                isSecurityPOEditable = value;
                RaisePropertyChanged("IsSecurityPOEditable");
            }
        }

        private GenericPickerModel _reviewReasonPickerModel { get; set; }

        public GenericPickerModel ReviewReasonPickerModel
        {
            get { return _reviewReasonPickerModel; }
            set
            {
                if (_reviewReasonPickerModel == value) return;

                _reviewReasonPickerModel = value;
                RaisePropertyChanged("ReviewReasonPickerModel");
            }
        }

        private bool isSecurityPaymentsTabVisible = false;
        public bool IsSecurityPaymentsTabVisible
        {
            get
            {


                return isSecurityPaymentsTabVisible;

            }
            set
            {
                if (isSecurityPaymentsTabVisible == value) return;

                isSecurityPaymentsTabVisible = value;
                RaisePropertyChanged("IsSecurityPaymentsTabVisible");
            }
        }

        private GenericPickerModel _reviewSubReasonPickerModel { get; set; }

        public GenericPickerModel ReviewSubReasonPickerModel
        {
            get { return _reviewSubReasonPickerModel; }
            set
            {
                if (_reviewSubReasonPickerModel == value) return;

                _reviewSubReasonPickerModel = value;
                RaisePropertyChanged("ReviewSubReasonPickerModel");
            }
        }

        private GenericPickerModel _applicationRefPickerModel { get; set; }

        public GenericPickerModel ApplicationRefPickerModel
        {
            get { return _applicationRefPickerModel; }
            set
            {
                if (_applicationRefPickerModel == value) return;

                _applicationRefPickerModel = value;
                RaisePropertyChanged("ApplicationRefPickerModel");
            }
        }

        private GenericPickerModel _idTypePickerModel { get; set; }

        public GenericPickerModel IDTypePickerModel
        {
            get { return _idTypePickerModel; }
            set
            {
                if (_idTypePickerModel == value) return;

                _idTypePickerModel = value;
                RaisePropertyChanged("IDTypePickerModel");
            }
        }
        private bool _isReviewReasonEnabled = false;

        public bool IsReviewReasonEnabled
        {
            get { return _isReviewReasonEnabled; }
            set
            {
                if (_isReviewReasonEnabled == value) return;

                _isReviewReasonEnabled = value;
                ReviewReasonButtonBackGroundColor = Color.FromHex(_isReviewReasonEnabled ? "#d49504" : "#9EA4A9");
                RaisePropertyChanged("IsReviewReasonEnabled");
            }
        }

        private Color _reviewReasonButtonBackGroundColor = Color.FromHex("#9EA4A9");

        public Color ReviewReasonButtonBackGroundColor
        {
            get { return _reviewReasonButtonBackGroundColor; }
            set
            {
                if (_reviewReasonButtonBackGroundColor == value)
                {
                    return;
                }

                _reviewReasonButtonBackGroundColor = value;
                RaisePropertyChanged("ReviewReasonButtonBackGroundColor");
            }
        }

        private bool _isReportDetailsEnabled = false;

        public bool IsReportDetailsEnabled
        {
            get { return _isReportDetailsEnabled; }
            set
            {
                if (_isReportDetailsEnabled == value) return;

                _isReportDetailsEnabled = value;
                ReportDetailsButtonBackGroundColor = Color.FromHex(_isReportDetailsEnabled ? "#d49504" : "#9EA4A9");
                RaisePropertyChanged("IsReportDetailsEnabled");
            }
        }
        
        private bool _isLateFlngDetailsEnabled = false;

        public bool IsLateFlngDetailsEnabled
        {
            get { return _isLateFlngDetailsEnabled; }
            set
            {
                if (_isLateFlngDetailsEnabled == value) return;

                _isLateFlngDetailsEnabled = value;
                LateFilingDetailsButtonBackGroundColor = Color.FromHex(_isLateFlngDetailsEnabled ? "#d49504" : "#9EA4A9");
                RaisePropertyChanged("IsLateFlngDetailsEnabled");
            }
        }

        private Color _reportDetailsButtonBackGroundColor = Color.FromHex("#9EA4A9");

        public Color ReportDetailsButtonBackGroundColor
        {
            get { return _reportDetailsButtonBackGroundColor; }
            set
            {
                if (_reportDetailsButtonBackGroundColor == value)
                {
                    return;
                }

                _reportDetailsButtonBackGroundColor = value;
                RaisePropertyChanged("ReportDetailsButtonBackGroundColor");
            }
        }

        private bool _isReviewDetailsEnabled = false;

        public bool IsReviewDetailsEnabled
        {
            get { return _isReviewDetailsEnabled; }
            set
            {
                if (_isReviewDetailsEnabled == value) return;

                _isReviewDetailsEnabled = value;
                ReviewDetailsButtonBackGroundColor = Color.FromHex(_isReviewDetailsEnabled ? "#d49504" : "#9EA4A9");
                RaisePropertyChanged("IsReviewDetailsEnabled");
            }
        }

        private Color _ReviewDetailsButtonBackGroundColor = Color.FromHex("#9EA4A9");

        public Color ReviewDetailsButtonBackGroundColor
        {
            get { return _ReviewDetailsButtonBackGroundColor; }
            set
            {
                if (_ReviewDetailsButtonBackGroundColor == value)
                {
                    return;
                }

                _ReviewDetailsButtonBackGroundColor = value;
                RaisePropertyChanged("ReviewDetailsButtonBackGroundColor");
            }
        }
        
        private Color _LateFilingDetailsButtonBackGroundColor = Color.FromHex("#d49504");

        public Color LateFilingDetailsButtonBackGroundColor
        {
            get { return _LateFilingDetailsButtonBackGroundColor; }
            set
            {
                if (_LateFilingDetailsButtonBackGroundColor == value)
                {
                    return;
                }

                _LateFilingDetailsButtonBackGroundColor = value;
                RaisePropertyChanged("LateFilingDetailsButtonBackGroundColor");
            }
        }

        private bool _isSecurityPaymentEnabled = false;

        public bool IsSecurityPaymentEnabled
        {
            get { return _isSecurityPaymentEnabled; }
            set
            {
                if (_isSecurityPaymentEnabled == value) return;

                _isSecurityPaymentEnabled = value;
                SecurityPaymentButtonBackGroundColor = Color.FromHex(_isSecurityPaymentEnabled ? "#d49504" : "#9EA4A9");
                RaisePropertyChanged("IsSecurityPaymentEnabled");
            }
        }

        private Color _SecurityPaymentButtonBackGroundColor = Color.FromHex("#9EA4A9");

        public Color SecurityPaymentButtonBackGroundColor
        {
            get { return _SecurityPaymentButtonBackGroundColor; }
            set
            {
                if (_SecurityPaymentButtonBackGroundColor == value)
                {
                    return;
                }

                _SecurityPaymentButtonBackGroundColor = value;
                RaisePropertyChanged("SecurityPaymentButtonBackGroundColor");
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
                DeclarationButtonBackGroundColor = Color.FromHex(_isDeclarationEnabled ? "#d49504" : "#9EA4A9");
                RaisePropertyChanged("IsDeclarationEnabled");
            }
        }

        public string _vBDocumentNumber = "";
        public string VBDocumentNumber
        {
            get { return _vBDocumentNumber; }
            set
            {
                if (_vBDocumentNumber == value) return;

                _vBDocumentNumber = value;
                RaisePropertyChanged("VBDocumentNumber");
            }
        }
        public string _vBSadadNumber = "";
        public string VBSadadNumber
        {
            get { return _vBSadadNumber; }
            set
            {
                if (_vBSadadNumber == value) return;

                _vBSadadNumber = value;
                RaisePropertyChanged("VBSadadNumber");
            }
        }
        public string _vBDateofPenality = null;
        public string VBDateofPenality
        {
            get { return _vBDateofPenality; }
            set
            {
                if (_vBDateofPenality == value) return;

                _vBDateofPenality = value;
                RaisePropertyChanged("VBDateofPenality");
            }
        }
        public string _vBDescriptionOfPenality = "";
        public string VBDescriptionOfPenality
        {
            get { return _vBDescriptionOfPenality; }
            set
            {
                if (_vBDescriptionOfPenality == value) return;

                _vBDescriptionOfPenality = value;
                RaisePropertyChanged("VBDescriptionOfPenality");
            }
        }
        public string _vBPeriodkey = "";
        public string VBPeriodkey
        {
            get { return _vBPeriodkey; }
            set
            {
                if (_vBPeriodkey == value) return;

                _vBPeriodkey = value;
                RaisePropertyChanged("VBPeriodkey");
            }
        }
        public string _vBStartDate = null;
        public string VBStartDate
        {
            get { return _vBStartDate; }
            set
            {
                if (_vBStartDate == value) return;

                _vBStartDate = value;
                RaisePropertyChanged("VBStartDate");
            }
        }
        public string _vBEndDate = null;
        public string VBEndDate
        {
            get { return _vBEndDate; }
            set
            {
                if (_vBEndDate == value) return;

                _vBEndDate = value;
                RaisePropertyChanged("VBEndDate");
            }
        }
        public string _vBDueDate = null;
        public string VBDueDate
        {
            get { return _vBDueDate; }
            set
            {
                if (_vBDueDate == value) return;

                _vBDueDate = value;
                RaisePropertyChanged("VBDueDate");
            }
        }
        public string _vBAmount = "";
        public string VBAmount
        {
            get { return _vBAmount; }
            set
            {
                if (_vBAmount == value) return;

                _vBAmount = value;
                RaisePropertyChanged("VBAmount");
            }
        }

        public string _vATinNumber = "";
        public string VATinNumber
        {
            get { return _vATinNumber; }
            set
            {
                if (_vBAmount == value) return;

                _vATinNumber = value;
                RaisePropertyChanged("VATinNumber");
            }
        }

        public string _vAAccNumber = "";
        public string VAAccNumber
        {
            get { return _vAAccNumber; }
            set
            {
                if (_vAAccNumber == value) return;

                _vAAccNumber = value;
                RaisePropertyChanged("VAAccNumber");
            }
        }

        public string _vAIdnumber = "";
        public string VAIdnumber
        {
            get { return _vAIdnumber; }
            set
            {
                if (_vAIdnumber == value) return;

                _vAIdnumber = value;
                RaisePropertyChanged("VAIdnumber");
            }
        }

        public string _vATaxpayerName = "";
        public string VATaxpayerName
        {
            get { return _vATaxpayerName; }
            set
            {
                if (_vATaxpayerName == value) return;

                _vATaxpayerName = value;
                RaisePropertyChanged("VATaxpayerName");
            }
        }

        public string _vAAddress = "";
        public string VAAddress
        {
            get { return _vAAddress; }
            set
            {
                if (_vAAddress == value) return;

                _vAAddress = value;
                RaisePropertyChanged("VAAddress");
            }
        }

        public string _vAVATReturnType = "";
        public string VAVATReturnType
        {
            get { return _vAVATReturnType; }
            set
            {
                if (_vAVATReturnType == value) return;

                _vAVATReturnType = value;
                RaisePropertyChanged("VAVATReturnType");
            }
        }

        public string _vAVatReturnReferenceNo = "";
        public string VAVatReturnReferenceNo
        {
            get { return _vAVatReturnReferenceNo; }
            set
            {
                if (_vAVatReturnReferenceNo == value) return;

                _vAVatReturnReferenceNo = value;
                RaisePropertyChanged("VAVatReturnReferenceNo");
            }
        }

        public string _vATaxPeriod = "";
        public string VATaxPeriod
        {
            get { return _vATaxPeriod; }
            set
            {
                if (_vATaxPeriod == value) return;

                _vATaxPeriod = value;
                RaisePropertyChanged("VATaxPeriod");
            }
        }

        public string _vAVatAccountNum = "";
        public string VAVatAccountNum
        {
            get { return _vAVatAccountNum; }
            set
            {
                if (_vAVatAccountNum == value) return;

                _vAVatAccountNum = value;
                RaisePropertyChanged("VAVatAccountNum");
            }
        }

        public string _vASalesAmount = "";
        public string VASalesAmount
        {
            get { return _vASalesAmount; }
            set
            {
                if (_vASalesAmount == value) return;

                _vASalesAmount = value;
                RaisePropertyChanged("VASalesAmount");
            }
        }

        public string _vASalesVatAdjustment = "";
        public string VASalesVatAdjustment
        {
            get { return _vASalesVatAdjustment; }
            set
            {
                if (_vASalesVatAdjustment == value) return;

                _vASalesVatAdjustment = value;
                RaisePropertyChanged("VASalesVatAdjustment");
            }
        }

        public string _vAVatOnSales = "";
        public string VAVatOnSales
        {
            get { return _vAVatOnSales; }
            set
            {
                if (_vAVatOnSales == value) return;

                _vAVatOnSales = value;
                RaisePropertyChanged("VAVatOnSales");
            }
        }

        public string _vAPurchaseAmount = "";
        public string VAPurchaseAmount
        {
            get { return _vAPurchaseAmount; }
            set
            {
                if (_vAPurchaseAmount == value) return;

                _vAPurchaseAmount = value;
                RaisePropertyChanged("VAPurchaseAmount");
            }
        }

        public string _vAPurchaseVatAdjustment = "";
        public string VAPurchaseVatAdjustment
        {
            get { return _vAPurchaseVatAdjustment; }
            set
            {
                if (_vAPurchaseVatAdjustment == value) return;

                _vAPurchaseVatAdjustment = value;
                RaisePropertyChanged("VAPurchaseVatAdjustment");
            }
        }

        public string _vAVatOnPurchase = "";
        public string VAVatOnPurchase
        {
            get { return _vAVatOnPurchase; }
            set
            {
                if (_vAVatOnPurchase == value) return;

                _vAVatOnPurchase = value;
                RaisePropertyChanged("VAVatOnPurchase");
            }
        }

        public string _vATotalDueVAT = "";
        public string VATotalDueVAT
        {
            get { return _vATotalDueVAT; }
            set
            {
                if (_vATotalDueVAT == value) return;

                _vATotalDueVAT = value;
                RaisePropertyChanged("VATotalDueVAT");
            }
        }

        public string _vATotalCreditVAT = "";
        public string VATotalCreditVAT
        {
            get { return _vATotalCreditVAT; }
            set
            {
                if (_vATotalCreditVAT == value) return;

                _vATotalCreditVAT = value;
                RaisePropertyChanged("VATotalCreditVAT");
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
                RaisePropertyChanged("IsDECCheckBox");
            }
        }

        public string _vACorrrections = "";
        public string VACorrrections
        {
            get { return _vACorrrections; }
            set
            {
                if (_vACorrrections == value) return;

                _vACorrrections = value;
                RaisePropertyChanged("VACorrrections");
            }
        }

        public string _vANetVat = "";
        public string VANetVat
        {
            get { return _vANetVat; }
            set
            {
                if (_vANetVat == value) return;

                _vANetVat = value;
                RaisePropertyChanged("VANetVat");
            }
        }

        public string _viewApplicationTypeText = "";
        public string ViewApplicationTypeText
        {
            get { return _viewApplicationTypeText; }
            set
            {
                if (_viewApplicationTypeText == value) return;

                _viewApplicationTypeText = value;
                RaisePropertyChanged("ViewApplicationTypeText");
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

        public string _totalTaxLiability = "";
        public string TotalTaxLiability
        {
            get { return _totalTaxLiability; }
            set
            {
                if (_totalTaxLiability == value) return;

                _totalTaxLiability = value;
                RaisePropertyChanged("TotalTaxLiability");
            }
        }
        public string _taxPaid = "";
        public string TaxPaid
        {
            get { return _taxPaid; }
            set
            {
                if (_taxPaid == value) return;

                _taxPaid = value;
                RaisePropertyChanged("TaxPaid");
            }
        }

        public string requestedReviewAmount = "";
        public string RequestedReviewAmount
        {
            get
            {
                return requestedReviewAmount;
            }
            set
            {
                if (requestedReviewAmount == value) return;

                requestedReviewAmount = value;
                RaisePropertyChanged("RequestedReviewAmount");
            }
        }
        public string disputeDetailsDesc = "";
        public string DisputeDetailsDesc
        {
            get
            {
                return disputeDetailsDesc;
            }
            set
            {
                if (disputeDetailsDesc == value) return;

                disputeDetailsDesc = value;
                RaisePropertyChanged("DisputeDetailsDesc");
            }
        }

        private bool isPenlaityAmountVisible = false;
        public bool IsPenlaityAmountVisible
        {
            get
            {
                return isPenlaityAmountVisible;
            }
            set
            {
                if (isPenlaityAmountVisible == value) return;

                isPenlaityAmountVisible = value;
                RaisePropertyChanged("IsPenlaityAmountVisible");
            }
        }
        public DateTime? vRTIDEffectiveDateFrom = null;
        public DateTime? VRTIDEffectiveDateFrom
        {
            get
            {
                return vRTIDEffectiveDateFrom;
            }
            set
            {
                if (vRTIDEffectiveDateFrom == value) return;

                vRTIDEffectiveDateFrom = value;
                RaisePropertyChanged("VRTIDEffectiveDateFrom");
            }
        }

        public DateTime? vRTIDEffectiveDateTo = null;
        public DateTime? VRTIDEffectiveDateTo
        {
            get
            {
                return vRTIDEffectiveDateTo;
            }
            set
            {
                if (vRTIDEffectiveDateTo == value) return;

                vRTIDEffectiveDateTo = value;
                RaisePropertyChanged("VRTIDEffectiveDateTo");
            }
        }

        public string cIPFTxablePurchases = "";
        public string CIPFTxablePurchases
        {
            get
            {
                return cIPFTxablePurchases;
            }
            set
            {
                if (cIPFTxablePurchases == value) return;

                cIPFTxablePurchases = value;
                RaisePropertyChanged("CIPFTxablePurchases");
            }
        }

        public string cIPFExemptPurchases = "";
        public string CIPFExemptPurchases
        {
            get
            {
                return cIPFExemptPurchases;
            }
            set
            {
                if (cIPFExemptPurchases == value) return;

                cIPFExemptPurchases = value;
                RaisePropertyChanged("CIPFExemptPurchases");
            }
        }

        public string cITxablePurchases = "";
        public string CITxablePurchases
        {
            get
            {
                return cITxablePurchases;
            }
            set
            {
                if (cITxablePurchases == value) return;

                cITxablePurchases = value;
                RaisePropertyChanged("CITxablePurchases");
            }
        }

        public string cIExemptPurchases = "";
        public string CIExemptPurchases
        {
            get
            {
                return cIExemptPurchases;
            }
            set
            {
                if (cIExemptPurchases == value) return;

                cIExemptPurchases = value;
                RaisePropertyChanged("CIExemptPurchases");
            }
        }

        public string pIPFTxablePurchases = "";
        public string PIPFTxablePurchases
        {
            get
            {
                return pIPFTxablePurchases;
            }
            set
            {
                if (pIPFTxablePurchases == value) return;

                pIPFTxablePurchases = value;
                RaisePropertyChanged("PIPFTxablePurchases");
            }
        }

        public string pIPFExemptPurchases = "";
        public string PIPFExemptPurchases
        {
            get
            {
                return pIPFExemptPurchases;
            }
            set
            {
                if (pIPFExemptPurchases == value) return;

                pIPFExemptPurchases = value;
                RaisePropertyChanged("PIPFExemptPurchases");
            }
        }

        public string pITxablePurchases = "";
        public string PITxablePurchases
        {
            get
            {
                return pITxablePurchases;
            }
            set
            {
                if (pITxablePurchases == value) return;

                pITxablePurchases = value;
                RaisePropertyChanged("PITxablePurchases");
            }
        }

        public string pIExemptPurchases = "";
        public string PIExemptPurchases
        {
            get
            {
                return pIExemptPurchases;
            }
            set
            {
                if (pIExemptPurchases == value) return;

                pIExemptPurchases = value;
                RaisePropertyChanged("PIExemptPurchases");
            }
        }

        public string vITDReportDetails = "";
        public string VITDReportDetails
        {
            get
            {
                return vITDReportDetails;
            }
            set
            {
                if (vITDReportDetails == value) return;

                vITDReportDetails = value;
                RaisePropertyChanged("VITDReportDetails");
            }
        }

        public string vITDIDType = "";
        public string VITDIDType
        {
            get
            {
                return vITDIDType;
            }
            set
            {
                if (vITDIDType == value) return;

                vITDIDType = value;
                RaisePropertyChanged("VITDIDType");
            }
        }

        public string vITDIDNumber = "";
        public string VITDIDNumber
        {
            get
            {
                return vITDIDNumber;
            }
            set
            {
                if (vITDIDNumber == value) return;

                vITDIDNumber = value;
                RaisePropertyChanged("VITDIDNumber");
            }
        }

        public string vITDDateOfBirth = "";
        public string VITDDateOfBirth
        {
            get
            {
                return vITDDateOfBirth;
            }
            set
            {
                if (vITDDateOfBirth == value) return;

                vITDDateOfBirth = value;
                RaisePropertyChanged("VITDDateOfBirth");
            }
        }

        public string vITDContactPersonName = "";
        public string VITDContactPersonName
        {
            get
            {
                return vITDContactPersonName;
            }
            set
            {
                if (vITDContactPersonName == value) return;

                vITDContactPersonName = value;
                RaisePropertyChanged("VITDContactPersonName");
            }
        }

        public string vRVSRequestType = "";
        public string VRVSRequestType
        {
            get
            {
                return vRVSRequestType;
            }
            set
            {
                if (vRVSRequestType == value) return;

                vRVSRequestType = value;
                RaisePropertyChanged("VRVSRequestType");
            }
        }

        public string vRVSStartofSuspensionPeriod = "";
        public string VRVSStartofSuspensionPeriod
        {
            get
            {
                return vRVSStartofSuspensionPeriod;
            }
            set
            {
                if (vRVSStartofSuspensionPeriod == value) return;

                vRVSStartofSuspensionPeriod = value;
                RaisePropertyChanged("VRVSStartofSuspensionPeriod");
            }
        }

        public string vRVSEndofSuspensionPeriod = "";
        public string VRVSEndofSuspensionPeriod
        {
            get
            {
                return vRVSEndofSuspensionPeriod;
            }
            set
            {
                if (vRVSEndofSuspensionPeriod == value) return;

                vRVSEndofSuspensionPeriod = value;
                RaisePropertyChanged("VRVSEndofSuspensionPeriod");
            }
        }

        public string vRVSSuspendedfilingperiod = "";
        public string VRVSSuspendedfilingperiod
        {
            get
            {
                return vRVSSuspendedfilingperiod;
            }
            set
            {
                if (vRVSSuspendedfilingperiod == value) return;

                vRVSSuspendedfilingperiod = value;
                RaisePropertyChanged("VRVSSuspendedfilingperiod");
            }
        }

        public string vRVSNextfilingperiod = "";
        public string VRVSNextfilingperiod
        {
            get
            {
                return vRVSNextfilingperiod;
            }
            set
            {
                if (vRVSNextfilingperiod == value) return;

                vRVSNextfilingperiod = value;
                RaisePropertyChanged("VRVSNextfilingperiod");
            }
        }

        public string vRVSNextfilingduedate = "";
        public string VRVSNextfilingduedate
        {
            get
            {
                return vRVSNextfilingduedate;
            }
            set
            {
                if (vRVSNextfilingduedate == value) return;

                vRVSNextfilingduedate = value;
                RaisePropertyChanged("VRVSNextfilingduedate");
            }
        }

        public string vRVSRFSuspensionofFiling = "";
        public string VRVSRFSuspensionofFiling
        {
            get
            {
                return vRVSRFSuspensionofFiling;
            }
            set
            {
                if (vRVSRFSuspensionofFiling == value) return;

                vRVSRFSuspensionofFiling = value;
                RaisePropertyChanged("VRVSRFSuspensionofFiling");
            }
        }

        public string vRVSIDType = "";
        public string VRVSIDType
        {
            get
            {
                return vRVSIDType;
            }
            set
            {
                if (vRVSIDType == value) return;

                vRVSIDType = value;
                RaisePropertyChanged("VRVSIDType");
            }
        }

        public string vRVSIDNumber = "";
        public string VRVSIDNumber
        {
            get
            {
                return vRVSIDNumber;
            }
            set
            {
                if (vRVSIDNumber == value) return;

                vRVSIDNumber = value;
                RaisePropertyChanged("VRVSIDNumber");
            }
        }

        public string vRVSContactPersonName = "";
        public string VRVSContactPersonName
        {
            get
            {
                return vRVSContactPersonName;
            }
            set
            {
                if (vRVSContactPersonName == value) return;

                vRVSContactPersonName = value;
                RaisePropertyChanged("VRVSContactPersonName");
            }
        }

        public string vRRequesttoReviewtheAmountValue = "";
        public string VRRequesttoReviewtheAmountValue
        {
            get
            {
                return vRRequesttoReviewtheAmountValue;
            }
            set
            {
                if (vRRequesttoReviewtheAmountValue == value) return;

                vRRequesttoReviewtheAmountValue = value;
                RaisePropertyChanged("VRRequesttoReviewtheAmountValue");
            }
        }

        private bool isVITDDOBVisible = false;
        public bool IsVITDDOBVisible
        {
            get
            {
                return isVITDDOBVisible;
            }
            set
            {
                if (isVITDDOBVisible == value) return;

                isVITDDOBVisible = value;
                RaisePropertyChanged("IsVITDDOBVisible");
            }
        }

        private bool isApplicationVisible = false;
        public bool IsApplicationVisible
        {
            get
            {
                return isApplicationVisible;
            }
            set
            {
                if (isApplicationVisible == value) return;

                isApplicationVisible = value;
                RaisePropertyChanged("IsApplicationVisible");
            }
        }
        private bool _isAssessPayOptionVisible = false;
        public bool IsAssessPayOptionVisible
        {
            get { return _isAssessPayOptionVisible; }
            set
            {
                if (_isAssessPayOptionVisible == value) return;

                _isAssessPayOptionVisible = value;
                RaisePropertyChanged("IsAssessPayOptionVisible");
            }
        }

        private bool isDraftClicked = false;
        public bool IsDraftClicked
        {
            get { return isDraftClicked; }
            set
            {
                if (isDraftClicked == value) return;

                isDraftClicked = value;
                RaisePropertyChanged("IsDraftClicked");
            }
        }
        private bool _isRRAmountEdit = false;
        public bool IsRRAmountEdit
        {
            get { return _isRRAmountEdit; }
            set
            {
                if (_isRRAmountEdit == value) return;

                _isRRAmountEdit = value;
                RaisePropertyChanged("IsRRAmountEdit");
            }
        }

        private bool partialAmountCheckBox = false;
        public bool PartialAmountCheckBox
        {
            get { return partialAmountCheckBox; }
            set
            {
                if (partialAmountCheckBox == value) return;

                partialAmountCheckBox = value;
                RaisePropertyChanged("PartialAmountCheckBox");
            }
        }
        private string _charCountDisputeDetails = 0 + "/" + 1000;
        public string charCountDisputeDetails
        {
            get
            {
                return _charCountDisputeDetails;
            }
            set
            {
                if (_charCountDisputeDetails == value) return;

                _charCountDisputeDetails = value;
                RaisePropertyChanged("charCountDisputeDetails");
            }
        }



        private string _charCountReportDetails = 0 + "/" + 1000;
        public string charCountReportDetails
        {
            get
            {
                return _charCountReportDetails;
            }
            set
            {
                if (_charCountReportDetails == value) return;

                _charCountReportDetails = value;
                RaisePropertyChanged("charCountReportDetails");
            }
        }
        
        private string _charCountLateFilingDetails = 0 + "/" + 3000;
        public string charCountLateFilingDetails
        {
            get
            {
                return _charCountReportDetails;
            }
            set
            {
                if (_charCountReportDetails == value) return;

                _charCountReportDetails = value;
                RaisePropertyChanged("charCountReportDetails");
            }
        }

        private bool fullPaymentCheckBox = false;
        public bool FullPaymentCheckBox
        {
            get { return fullPaymentCheckBox; }
            set
            {
                if (fullPaymentCheckBox == value) return;

                fullPaymentCheckBox = value;
                RaisePropertyChanged("FullPaymentCheckBox");
            }
        }
        private Dictionary<string, string> IDToNameDictionary = new Dictionary<string, string>
        {
            {"ZS0001",AppResources.VFCNationalID},
            { "ZS0002",AppResources.VFCIqamaID},
            { "ZS0003",AppResources.VFCGCCID},
         };

        public bool MarkComplete { get; private set; } = false;
        //public int MaxIndex { get; private set; } = 6;

        private Color _declarationButtonBackGroundColor = Color.FromHex("#9EA4A9");

        public Color DeclarationButtonBackGroundColor
        {
            get { return _declarationButtonBackGroundColor; }
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
                RaisePropertyChanged("VATDeregistrationSummaryDeclarationData");
            }
        }

        public ObservableCollection<Attachment> vITDAttachmentsListViewData { get; set; }
        public ObservableCollection<Attachment> VITDAttachmentsListViewData
        {
            get { return vITDAttachmentsListViewData; }
            set
            {
                if (vITDAttachmentsListViewData == value)
                {
                    return;
                }
                vITDAttachmentsListViewData = value;
                RaisePropertyChanged("VITDAttachmentsListViewData");
            }
        }

        public ObservableCollection<Attachment> vSVRAttachmentsListViewData { get; set; }
        public ObservableCollection<Attachment> VSVRAttachmentsListViewData
        {
            get { return vSVRAttachmentsListViewData; }
            set
            {
                if (vSVRAttachmentsListViewData == value)
                {
                    return;
                }
                vSVRAttachmentsListViewData = value;
                RaisePropertyChanged("VSVRAttachmentsListViewData");
            }
        }
        public ObservableCollection<Attachment> vatDeRegAttachmentsList { get; set; }
        public ObservableCollection<Attachment> VatDeRegAttachmentsList
        {
            get
            {
                return vatDeRegAttachmentsList;
            }
            set
            {
                if (vatDeRegAttachmentsList == value) return;

                if (value != null)
                    vatDeRegAttachmentsList = value;
                RaisePropertyChanged("VatDeRegAttachmentsList");
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
                if (_vatDeregistrationSummaryReasonData == value) return;

                if (value != null)
                {
                    _vatDeregistrationSummaryReasonData = value;
                }
                RaisePropertyChanged("VATDeregistrationSummaryReasonData");
            }
        }

        public ObservableCollection<SelectionModel> disputeAmountPaymentOptions { get; set; }
        public ObservableCollection<SelectionModel> DisputeAmountPaymentOptions
        {
            get { return disputeAmountPaymentOptions; }
            set
            {
                if (disputeAmountPaymentOptions == value)
                {
                    return;
                }
                disputeAmountPaymentOptions = value;
                RaisePropertyChanged("DisputeAmountPaymentOptions");
            }
        }



        public ObservableCollection<Attachment> attachmentsListViewData { get; set; }

        public ObservableCollection<Attachment> AttachmentsListViewData
        {
            get { return attachmentsListViewData; }

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

        public ObservableCollection<Attachment> _lateFilingAttachmentsListViewData { get; set; }

        public ObservableCollection<Attachment> LateFilingAttachmentsListViewData
        {
            get { return _lateFilingAttachmentsListViewData; }

            set
            {
                if (_lateFilingAttachmentsListViewData == value)
                {
                    return;
                }

                _lateFilingAttachmentsListViewData = value;
                RaisePropertyChanged("LateFilingAttachmentsListViewData");
            }
        }

        public ObservableCollection<Attachment> bankGuranteeAttachmentsListViewData { get; set; }

        public ObservableCollection<Attachment> BankGuranteeAttachmentsListViewData
        {
            get { return bankGuranteeAttachmentsListViewData; }

            set
            {
                if (bankGuranteeAttachmentsListViewData == value)
                {
                    return;
                }

                bankGuranteeAttachmentsListViewData = value;
                RaisePropertyChanged("BankGuranteeAttachmentsListViewData");
            }
        }

        private string _vRVGVATeligiblesupplies = string.Empty;
        public string VRVGVATeligiblesupplies
        {
            get
            {
                return _vRVGVATeligiblesupplies;
            }
            set
            {
                if (_vRVGVATeligiblesupplies == value) return;

                _vRVGVATeligiblesupplies = value;
                RaisePropertyChanged("VRVGVATeligiblesupplies");
            }
        }
        private string _vRVGVATeligiblepurchases = string.Empty;
        public string VRVGVATeligiblepurchases
        {
            get
            {
                return _vRVGVATeligiblepurchases;
            }
            set
            {
                if (_vRVGVATeligiblepurchases == value) return;

                _vRVGVATeligiblepurchases = value;
                RaisePropertyChanged("VRVGVATeligiblepurchases");
            }
        }
        private string _vRVGEffectivedate = string.Empty;
        public string VRVGEffectivedate
        {
            get
            {
                return _vRVGEffectivedate;
            }
            set
            {
                if (_vRVGEffectivedate == value) return;

                _vRVGEffectivedate = value;
                RaisePropertyChanged("VRVGEffectivedate");
            }
        }
        public ObservableCollection<Attachment> vRVGAttachmentsListViewData { get; set; }
        public ObservableCollection<Attachment> VRVGAttachmentsListViewData
        {
            get { return vRVGAttachmentsListViewData; }
            set
            {
                if (vRVGAttachmentsListViewData == value)
                {
                    return;
                }
                vRVGAttachmentsListViewData = value;
                RaisePropertyChanged("VRVGAttachmentsListViewData");
            }
        }
        public ObservableCollection<VATReviewRequestVTGRModel.TABLESetResult> vRVGTinsListViewData { get; set; }
        public ObservableCollection<VATReviewRequestVTGRModel.TABLESetResult> VRVGTinsListViewData
        {
            get { return vRVGTinsListViewData; }
            set
            {
                if (vRVGTinsListViewData == value)
                {
                    return;
                }
                vRVGTinsListViewData = value;
                RaisePropertyChanged("VRVGTinsListViewData");
            }
        }
        private string _VrVGIDType = string.Empty;
        public string VRVGIDType
        {
            get
            {
                return _VrVGIDType;
            }
            set
            {
                if (_VrVGIDType == value) return;

                _VrVGIDType = value;
                RaisePropertyChanged("VRVGIDType");
            }
        }
        private string _VrVGIDNumber = string.Empty;
        public string VRVGIDNumber
        {
            get
            {
                return _VrVGIDNumber;
            }
            set
            {
                if (_VrVGIDNumber == value) return;

                _VrVGIDNumber = value;
                RaisePropertyChanged("VRVGIDNumber");
            }
        }
        private string _VrVGContactPersonName = string.Empty;
        public string VRVGContactPersonName
        {
            get
            {
                return _VrVGContactPersonName;
            }
            set
            {
                if (_VrVGContactPersonName == value) return;

                _VrVGContactPersonName = value;
                RaisePropertyChanged("VRVGContactPersonName");
            }
        }

        int _defaultReq = 0;
        int _defaultSecurity = 0;

        public int DefaultReq
        {
            get
            {
                return _defaultReq;
            }
            set
            {
                if (_defaultReq == value) return;

                _defaultReq = value;
                RaisePropertyChanged("DefaultReq");
            }
        }

        public int DefaultSecurity
        {
            get
            {
                return _defaultSecurity;
            }
            set
            {
                if (_defaultSecurity == value) return;

                _defaultSecurity = value;
                RaisePropertyChanged("DefaultSecurity");
            }
        }


        [Preserve(AllMembers = true)]
        public class SelectionModel
        {
            public SelectionModel()
            {
            }

            public string SelectionTitle { get; set; }
            public bool IsSelected { get; set; }
        }

        public ObservableCollection<SelectionModel> securityPaymentOptions { get; set; }

        public ObservableCollection<SelectionModel> SecurityPaymentOptions
        {
            get { return securityPaymentOptions; }

            set
            {
                if (securityPaymentOptions == value)
                {
                    return;
                }

                securityPaymentOptions = value;
                RaisePropertyChanged("SecurityPaymentOptions");
            }
        }

        string vrVRIdnumber { get; set; }

        private string _VrVRDOB = string.Empty;
        public string VrVRDOB
        {
            get
            {
                return _VrVRDOB;
            }
            set
            {
                if (_VrVRDOB == value) return;

                _VrVRDOB = value;
                RaisePropertyChanged("VrVRDOB");
            }
        }
        private string _VrVRIDType = string.Empty;
        public string VrVRIDType
        {
            get
            {
                return _VrVRIDType;
            }
            set
            {
                if (_VrVRIDType == value) return;

                _VrVRIDType = value;
                RaisePropertyChanged("VrVRIDType");
            }
        }
        private string _VrVRIDNumber = string.Empty;
        public string VrVRIDNumber
        {
            get
            {
                return _VrVRIDNumber;
            }
            set
            {
                if (_VrVRIDNumber == value) return;

                _VrVRIDNumber = value;
                RaisePropertyChanged("VrVRIDNumber");
            }
        }
        private string _VrVRContactPersonName = string.Empty;
        public string VrVRContactPersonName
        {
            get
            {
                return _VrVRContactPersonName;
            }
            set
            {
                if (_VrVRContactPersonName == value) return;

                _VrVRContactPersonName = value;
                RaisePropertyChanged("VrVRContactPersonName");
            }
        }

        private bool isSadadCheckBoxEnabled = true;
        public bool IsSadadCheckBoxEnabled
        {
            get { return isSadadCheckBoxEnabled; }
            set
            {
                if (isSadadCheckBoxEnabled == value) return;

                isSadadCheckBoxEnabled = value;
                RaisePropertyChanged("IsSadadCheckBoxEnabled");
            }
        }

        private string _VrVRcontactDOB = string.Empty;
        public string VrVRContactDOB
        {
            get
            {
                return _VrVRcontactDOB;
            }
            set
            {
                if (_VrVRcontactDOB == value) return;

                _VrVRcontactDOB = value;
                RaisePropertyChanged("VrVRContactDOB");
            }
        }
        private string _VrVRidNumberSR = string.Empty;
        public string VrVRIdNumberSR
        {
            get
            {
                return _VrVRidNumberSR;
            }
            set
            {
                if (_VrVRidNumberSR == value) return;

                _VrVRidNumberSR = value;
                RaisePropertyChanged("VrVRIdNumberSR");
            }
        }

        private string _VrVRfirstNameSR = string.Empty;
        public string VrVRFirstNameSR
        {
            get
            {
                return _VrVRfirstNameSR;
            }
            set
            {
                if (_VrVRfirstNameSR == value) return;

                _VrVRfirstNameSR = value;
                RaisePropertyChanged("VrVRFirstNameSR");
            }
        }
        private string _VrVRlastnmFR = string.Empty;
        public string VrVRLastnmFR
        {
            get
            {
                return _VrVRlastnmFR;
            }
            set
            {
                if (_VrVRlastnmFR == value) return;

                _VrVRlastnmFR = value;
                RaisePropertyChanged("VrVRLastnmFR");
            }
        }

        private string _VrVRmobNumberFR = string.Empty;
        public string VrVRMobNumberFR
        {
            get
            {
                return _VrVRmobNumberFR;
            }
            set
            {
                if (_VrVRmobNumberFR == value) return;

                _VrVRmobNumberFR = value;
                RaisePropertyChanged("VrVRMobNumberFR");
            }
        }
        private string _VrVRvatEligibleStartDate = string.Empty;
        public string VrVRVatEligibleStartDate
        {
            get
            {
                return _VrVRvatEligibleStartDate;
            }
            set
            {
                if (_VrVRvatEligibleStartDate == value) return;

                _VrVRvatEligibleStartDate = value;
                RaisePropertyChanged("VrVRVatEligibleStartDate");
            }
        }

        private string _VrVRquesTion3answerSelected = string.Empty;
        public string VrVRquesTion3answerSelected
        {
            get
            {
                return _VrVRquesTion3answerSelected;
            }
            set
            {
                if (_VrVRquesTion3answerSelected == value) return;

                _VrVRquesTion3answerSelected = value;

                RaisePropertyChanged("VrVRquesTion3answerSelected");
            }
        }
        private string _VrVRquesTion4answerSelected = string.Empty;
        public string VrVRquesTion4answerSelected
        {
            get
            {
                return _VrVRquesTion4answerSelected;
            }
            set
            {
                if (_VrVRquesTion4answerSelected == value) return;

                _VrVRquesTion4answerSelected = value;

                RaisePropertyChanged("VrVRquesTion4answerSelected");
            }
        }
        private string _VrVRquesTion1answerSelected = string.Empty;
        public string VrVRquesTion1answerSelected
        {
            get
            {
                return _VrVRquesTion1answerSelected;
            }
            set
            {
                if (_VrVRquesTion1answerSelected == value) return;

                _VrVRquesTion1answerSelected = value;

                RaisePropertyChanged("VrVRquesTion1answerSelected");
            }
        }
        private string _VrVRquesTion2answerSelected = string.Empty;
        public string VrVRquesTion2answerSelected
        {
            get
            {
                return _VrVRquesTion2answerSelected;
            }
            set
            {
                if (_VrVRquesTion2answerSelected == value) return;

                _VrVRquesTion2answerSelected = value;

                RaisePropertyChanged("VrVRquesTion2answerSelected");
            }
        }
        private string _VrVRidnumberFR = string.Empty;
        public string VrVRIdnumberFR
        {
            get
            {
                return _VrVRidnumberFR;
            }
            set
            {
                if (_VrVRidnumberFR == value) return;

                _VrVRidnumberFR = value;
                RaisePropertyChanged("VrVRIdnumberFR");
            }
        }
        private string _VrVRtypeFR = string.Empty;
        public string VrVRTypeFR
        {
            get
            {
                return _VrVRtypeFR;
            }
            set
            {
                if (_VrVRtypeFR == value) return;

                _VrVRtypeFR = value;
                RaisePropertyChanged("VrVRTypeFR");
            }
        }

        private string _VrVRfirstnmFR = string.Empty;
        public string VrVRFirstnmFR
        {
            get
            {
                return _VrVRfirstnmFR;
            }
            set
            {
                if (_VrVRfirstnmFR == value) return;

                _VrVRfirstnmFR = value;
                RaisePropertyChanged("VrVRFirstnmFR");
            }
        }
        private string _VrVRgpartFR = string.Empty;
        public string VrVRGpartFR
        {
            get
            {
                return _VrVRgpartFR;
            }
            set
            {
                if (_VrVRgpartFR == value) return;

                _VrVRgpartFR = value;
                RaisePropertyChanged("VrVRGpartFR");
            }
        }
        private string _VrVRIban = string.Empty;
        public string VrVRIban
        {
            get
            {
                return _VrVRIban;
            }
            set
            {
                if (_VrVRIban == value) return;

                _VrVRIban = value;
                RaisePropertyChanged("VrVRIban");
            }
        }
        private string _VrVRsmtpAddrFR = string.Empty;
        public string VrVRSmtpAddrFR
        {
            get
            {
                return _VrVRsmtpAddrFR;
            }
            set
            {
                if (_VrVRsmtpAddrFR == value) return;

                _VrVRsmtpAddrFR = value;
                RaisePropertyChanged("VrVRSmtpAddrFR");
            }
        }

        private string _VrVRimportExportText = string.Empty;
        public string VrVRImportExportText
        {
            get
            {
                return _VrVRimportExportText;
            }
            set
            {
                if (_VrVRimportExportText == value) return;

                _VrVRimportExportText = value;
                RaisePropertyChanged("VrVRImportExportText");
            }
        }

        private string _lastFulfilmentDate = string.Empty;
        public string LastFulfilmentDateText
        {
            get
            {
                return _lastFulfilmentDate;
            }
            set
            {
                if (_lastFulfilmentDate == value) return;

                _lastFulfilmentDate = value;
                RaisePropertyChanged("LastFulfilmentDateText");
            }
        }
        private string _VrVRAttachmentName = string.Empty;
        public string VrVRAttachmentName
        {
            get
            {
                return _VrVRAttachmentName;
            }
            set
            {
                if (_VrVRAttachmentName == value) return;

                _VrVRAttachmentName = value;
                RaisePropertyChanged("VrVRAttachmentName");
            }
        }
        private VATRegistrationDetails _VrVRvATRegistrationDetailsData;
        public VATRegistrationDetails VrVRVATRegistrationDetailsData
        {
            get
            {
                return _VrVRvATRegistrationDetailsData;
            }
            set
            {
                if (_VrVRvATRegistrationDetailsData == value) return;

                _VrVRvATRegistrationDetailsData = value;
                RaisePropertyChanged("VrVRVATRegistrationDetailsData");
            }
        }
        private List<QuestionNumberWithMinMaxRange> _VrVRminMaxRanges;
        public List<QuestionNumberWithMinMaxRange> VrVRMinMaxRanges
        {
            get
            {
                return _VrVRminMaxRanges;
            }
            set
            {
                if (_VrVRminMaxRanges == value) return;

                _VrVRminMaxRanges = value;
                RaisePropertyChanged("VrVRMinMaxRanges");
            }
        }


        private ObservableCollection<Result2> _VrVRibanList;
        public ObservableCollection<Result2> VrVRIbanList
        {
            get
            {
                return _VrVRibanList;
            }
            set
            {
                if (_VrVRibanList == value) return;

                _VrVRibanList = value;
                RaisePropertyChanged("VrVRIbanList");
            }
        }

        private Dictionary<string, string> IDTypeDictionary = null;
        private Dictionary<string, string> VGSupplicesDictionary = null;
        private Dictionary<string, string> VGPurchasesDictionary = null;



        private bool _isBankGuranteeAttachments = false;
        private bool _isReportDetailsAttachments = false;
        private bool _isLateFilingAttachments = false;

        private VATObjectionFormModel.ReviewReason selectedReviewReason;
        private VATObjectionFormModel.SubReason selectedSubReviewReason;
        private VATObjectionRejectedFormModel.AppRefNumResult selectedApplicationRef;
        private List<VATObjectionFormModel.ReviewReason> reviewReasonList;
        private List<VATObjectionFormModel.SubReason> subReviewReasonList;
        private List<VATObjectionRejectedFormModel.AppRefNumResult> appRefNumList;
        private VATObjectionSummaryModel modelVATReview;
        private VATObjectionRejectedFormModel _VATObjectionRejected;

        public VatReviewInterface vRInterface { get; set; }

        public VatReviewViewModel(INavigationService navigationService, IDialogService dialogService)
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

            CloseClick = new Command(() => { _navigationService.GoBack(); });

            GoBackClick = new Command(() => { BackNavigations(); });

            ReviewReasonConBtnTapped = new Command(() => { ReviewReasonConBtnClicked(); });

            ReviewDetailsConBtnTapped = new Command(() => { ReviewDetailsConBtnClicked(); });
            ReportDetailsConBtnTapped = new Command(() => { ReportDetailsConBtnClicked(); });
            LateFilingContdButtonClicked = new Command(() => { LateFilingConBtnClicked(); });
            SecurityPaymentConBtnTapped = new Command(() => { SecurityPaymentConBtnClicked(); });

            DeclarationConBtnTapped = new Command(() => { DeclarationConBtnClicked(); });

            SummaryConBtnTapped = new Command(() => { SummaryConBtnClicked(); });

            GoBackToReviewReason = new Command(() => { EnableReviewReasonView(); });
            GoBackToReviewDetails = new Command(() => { EnableReviewDetailsView(); });
            GoBackToDeclaration = new Command(() => { EnableDeclarationView(); });
            ViewApplicationTapped = new Command(() => { ViewApplicationClicked(); });
            NewAttachmentTapped = new Command(() => { NewAttachmentClicked(); });
            NewBankGuranteeAttachmentTapped = new Command(NewBankGuranteeAttachmentClicked);
            LateFilingAttachmentTapped = new Command(LateFilingAttachmentClicked);
            ReviewReasonCommand = new Command(() => { showReviewReasonPickerDialog(); });
            SubReviewReasonCommand = new Command(() => { showSubReviewReasonPickerDialog(); });
            ApplicationNumRefCommand = new Command(() => { showAppRefNumberPickerDialog(); });
            SadadGenerateBtnTapped = new Command(() => { ShowSADADConfirmation(); });
            ShowDatePicker = new Command(() => { showDatePickerDialog(); });
            IdTypeSpinnerTapped = new Command(() => { showIdTypePickerDialog(); });
            GoBackToReportDetails = new Command(() => { EnableReportDetailsView(); });
            GoBackToLateFilingDetails = new Command(() => { EnableLateFilingDetailsView(); });
            onMoreOptionClicked = new Command(() =>
            {
                PopupNavigation.Instance.PushAsync(new MoreMenuPopUpPageViewRTwo(ListOfActionButtonsApplicable));
            });

            //AddSecurityPaymentOptions();


            genericDatePickerModel = new GenericDatePickerModel();
            genericDatePickerModel.DatePickerTitle = AppResources.VRDateOfBirth;
            genericDatePickerModel.PickerId = "DatePicker";

            //setIdPickerModel();
        }

        public void setMoreOptioButtons()
        {
            var listOfActionButtonsApplicable = new List<string>();
            if (App.selectedVATItem != "")
            {

                listOfActionButtonsApplicable.Add(AppResources.ZZVoid);
            }


            listOfActionButtonsApplicable.Add(AppResources.ZZSaveAsDraft);
            ListOfActionButtonsApplicable = listOfActionButtonsApplicable;
        }


        public async Task VATSetReturnVoid()
        {
            modelVATReview.d.Operationx = "04";
            var vatReviewResponse = await SubmitClicked();

            if (vatReviewResponse != null && vatReviewResponse.d != null)
            {

                modelVATReview = vatReviewResponse;

                Device.BeginInvokeOnMainThread(async () =>
                {


                    List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                    HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                    NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                    headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                    headerAmountInfo.IsLinkAvailable = false;
                    headerAmountInfo.Message = AppResources.ZZGeneralMessage_VATReviewCancelled;

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

            }


        }
        public async Task OnSaveDraftClicked()
        {
            IsDraftClicked = true;

            modelVATReview.d.Operationx = "05";
            var vatReviewResponse = await SubmitClicked();
            if (vatReviewResponse != null && vatReviewResponse.d != null)
            {

                modelVATReview = vatReviewResponse;

                IsDraftClicked = false;



                Device.BeginInvokeOnMainThread(async () =>
                {
                    App.selectedVATItem = modelVATReview.d.Fbnumx;
                    setMoreOptioButtons();

                    List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                    HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                    NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                    headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                    headerAmountInfo.IsLinkAvailable = false;
                    // headerAmountInfo.Message = string.Format(AppResources.VATReviewDraftSaved, "  " + modelVATReview.d.Fbnumx);
                    headerAmountInfo.Message = string.Format(AppResources.VATReviewNewDraftSaved);

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
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        IsLoading = false;
                        //await _dialogService.ShowMessage(WebServiceManager.ErrorMessageForVAT, AppResources.Information);
                    });
                }
                //Device.BeginInvokeOnMainThread(async () =>
                //{
                //    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                //});
            }



        }

        public async void VoidMsg()
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

            await PopupNavigation.Instance.PushAsync(new ShowVatInformationConfirmationPageView(newDesignPopUp));

        }


        public async void NewAttachmentClicked()
        {
            if (Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopupStack.Count > 0) return;
            if (AttachmentsListViewData == null)
            {
                AttachmentsListViewData = new ObservableCollection<Attachment>();
            }

            try
            {

                _isBankGuranteeAttachments = false;
                _isReportDetailsAttachments = true;
                _isLateFilingAttachments = false;


                if (string.IsNullOrEmpty(SADADNumber))
                {
                    await PopupNavigation.Instance.PushAsync(new FilesUploadPopUpPageView(
                        AttachmentsListViewData.ToList(),
                        Models.ZakatInstalationModels.WhichAttachment.VatReviewAttachments,
                        modelVATReview.d.ReturnIdx));
                }

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

        public async void NewBankGuranteeAttachmentClicked()
        {
            if (Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopupStack.Count > 0) return;
            if (BankGuranteeAttachmentsListViewData == null)
            {
                BankGuranteeAttachmentsListViewData = new ObservableCollection<Attachment>();
            }

            try
            {
                _isBankGuranteeAttachments = true;
                _isReportDetailsAttachments = false;
                _isLateFilingAttachments = false;

                await PopupNavigation.Instance.PushAsync(new FilesUploadPopUpPageView(
                    BankGuranteeAttachmentsListViewData.ToList(),
                    Models.ZakatInstalationModels.WhichAttachment.VatReviewBankGuranteeAttach,
                    modelVATReview.d.ReturnIdx));

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

        public async void LateFilingAttachmentClicked()
        {
            if (Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopupStack.Count > 0) return;
            if (LateFilingAttachmentsListViewData == null)
            {
                LateFilingAttachmentsListViewData = new ObservableCollection<Attachment>();
            }

            try
            {
                _isBankGuranteeAttachments = false;
                _isLateFilingAttachments = true;
                _isReportDetailsAttachments = false;

                await PopupNavigation.Instance.PushAsync(new FilesUploadPopUpPageView(
                    LateFilingAttachmentsListViewData.ToList(),
                    Models.ZakatInstalationModels.WhichAttachment.VatReviewLateFiling,
                    modelVATReview.d.ReturnIdx));

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

        private void setIdPickerModel()
        {
            List<string> iDTypes = new List<string>();
            iDTypes.Add(AppResources.VFCNationalID);
            iDTypes.Add(AppResources.VFCIqamaID);
            iDTypes.Add(AppResources.VFCGCCID);


            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = iDTypes;
            genericPickerModel.PickerTitle = AppResources.VRIDType;
            genericPickerModel.PickerId = PickerEnum.IDType.ToString();

            IDTypePickerModel = genericPickerModel;
        }

        private void setReviewReasonPickerModel()
        {
            List<string> reasonTypes = new List<string>();
            foreach (var reason in reviewReasonList)
            {
                reasonTypes.Add(reason.Reasons);
            }

            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = reasonTypes;
            genericPickerModel.PickerTitle = AppResources.VRReviewReason;
            genericPickerModel.PickerId = PickerEnum.ReviewReason.ToString();

            ReviewReasonPickerModel = genericPickerModel;
        }

        private void setReviewSubReasonPickerModel(string selectedReason)
        {
            List<string> subReasonTypes = new List<string>();
            selectedReviewReason = reviewReasonList.First(reviewReason => reviewReason.Reasons == selectedReason);
            subReviewReasonList = selectedReviewReason.ListSubReason;
            foreach (var subReason in subReviewReasonList)
            {
                subReasonTypes.Add(subReason.SubReasons);
            }

            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = subReasonTypes;
            genericPickerModel.PickerTitle = AppResources.VRReviewSubReason;
            genericPickerModel.PickerId = PickerEnum.ReviewSubReason.ToString();

            ReviewSubReasonPickerModel = genericPickerModel;

            if (selectedReviewReason.ProcCD == "VTPC" || selectedReviewReason.ProcCD == "VTPN" || selectedReviewReason.ProcCD == "VTAS")
            {
                IsSecurityPaymentsTabVisible = true;
                IsPenlaityAmountVisible = true;
            }
            else
            {
                IsSecurityPaymentsTabVisible = false;
                IsPenlaityAmountVisible = false;
            }
            if (selectedReviewReason.ProcCD == "VTPC" || selectedReviewReason.ProcCD == "VTPN")
            {
                ViewApplicationTypeText = AppResources.VRViewBill;
            }
            else
            {
                ViewApplicationTypeText = AppResources.VRViewApplication;
            }
            if (selectedReviewReason.ProcCD == "VTAS")
            {
                IsAssessPayOptionVisible = true;
                IsPenlaityAmountVisible = false;
            }
            else
            {
                IsAssessPayOptionVisible = false;
            }

            EnableReviewDetailsConButton();
        }

        private void setApplicationRefPickerModel(List<VATObjectionRejectedFormModel.AppRefNumResult> results)
        {
            appRefNumList = results;
            List<string> appRefNums = new List<string>();

            if (results != null && results.Count > 0)
            {


                foreach (var refNum in results)
                {
                    if (!string.IsNullOrEmpty(refNum.Opbel))
                    {
                        appRefNums.Add(refNum.Opbel);
                    }
                    else if (!string.IsNullOrEmpty(refNum.Fbnum))
                    {
                        appRefNums.Add(refNum.Fbnum);
                    }


                }

                GenericPickerModel genericPickerModel = new GenericPickerModel();
                genericPickerModel.PickerData = appRefNums;
                genericPickerModel.PickerTitle = AppResources.VRApplicationReferenceNumber;
                genericPickerModel.PickerId = PickerEnum.ApplicationReferenceNumber.ToString();

                ApplicationRefPickerModel = genericPickerModel;
            }


        }

        private async void showDatePickerDialog()
        {

            try
            {
                await PopupNavigation.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel));
            }
            catch (GAZTUnlockAccountException)
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

        private async void showIdTypePickerDialog()
        {
            IDNumber = "";
            PickedDateFullMonth = "";
            try
            {
                await PopupNavigation.Instance.PushAsync(new PickerPageView(IDTypePickerModel));
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

        private async void showReviewReasonPickerDialog()
        {
            try
            {
                if (ReviewReasonPickerModel != null && string.IsNullOrEmpty(SADADNumber))
                {
                    await PopupNavigation.Instance.PushAsync(new PickerPageView(ReviewReasonPickerModel));
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

        private async void showSubReviewReasonPickerDialog()
        {
            try
            {
                if (ReviewSubReasonPickerModel != null && string.IsNullOrEmpty(SADADNumber))
                {
                    await PopupNavigation.Instance.PushAsync(new PickerPageView(ReviewSubReasonPickerModel));

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

        private async void showAppRefNumberPickerDialog()
        {
            try
            {
                if (ApplicationRefPickerModel != null && string.IsNullOrEmpty(SADADNumber))
                {
                    await PopupNavigation.Instance.PushAsync(new PickerPageView(ApplicationRefPickerModel));

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

        private void ReviewReasonConBtnClicked()
        {
            try
            {
                if (IsReviewReasonEnabled)
                {

                    EnableReportDetailsView();
                    EnableReviewDetailsConButton();


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

        private void ReportDetailsConBtnClicked()
        {
            try
            {
                if (IsReportDetailsEnabled)
                {
                    if (IsSecurityPaymentsTabVisible)
                    {
                        EnableReviewDetailsView();
                    }
                    else
                    {
                        EnableDeclarationView();
                    }

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

        private void ReviewDetailsConBtnClicked()
        {
            try
            {
                if (IsReviewDetailsEnabled)
                {

                    if (IsSecurityPaymentsTabVisible)
                    {

                        EnableLateFilingDetailsView();
                    }
                    else
                    {
                        EnableDeclarationView();
                    }

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

        private void LateFilingConBtnClicked()
        {
            try
            {
                if (IsLateFlngDetailsEnabled)
                {
                    if (IsSecurityPaymentsTabVisible)
                    {
                        EnableSecurityPaymentsView();
                    }
                    else
                    {
                        EnableDeclarationView();
                    }

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

        private void SecurityPaymentConBtnClicked()
        {
            try
            {


                if (IsSecurityPaymentEnabled)
                {
                    EnableDeclarationView();
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

        private void DeclarationConBtnClicked()
        {
            try
            {
                if (IsDeclarationEnabled)
                {
                    EnableSummaryView();
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

        private async void SummaryConBtnClicked()
        {
            try
            {

                IsGeneratingFormbundle = false;
                IsSadadRefeshVisible = false;
                modelVATReview.d.Operationx = "01";

                var vatReviewResponse = await SubmitClicked();


                if (vatReviewResponse != null && vatReviewResponse.d != null)
                {

                    modelVATReview = vatReviewResponse;
                    VATReferanceNumber = modelVATReview.d.Fbnumx;

                    await Application.Current.MainPage.Navigation.PushAsync(new VatReviewSuccessPageView());
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

        public void FetchSecurityAmount()
        {
            if (RequestedReviewAmount == "" || RequestedReviewAmount == "0")
            {
                return;
            }

            try
            {
                VATObjectionSecurityAmount(Convert.ToDecimal(RequestedReviewAmount),
                    Convert.ToDecimal(selectedApplicationRef.Liaamt),
                    Convert.ToDecimal(selectedApplicationRef.Clramt));
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }

        private async void SaveClicked()
        {
            try
            {

                modelVATReview.d.Operationx = "05";
                modelVATReview = await SubmitClicked();

                if (IsGeneratingFormbundle)
                {

                    if (modelVATReview != null && modelVATReview.d != null)
                    {


                        decimal DisamtValue = 0;
                        decimal LiaamtValue = 0;
                        decimal SecamtValue = 0;

                        if (modelVATReview.d.SecurityDtl.Disamt.Length > 0)
                        {

                            DisamtValue = decimal.Truncate(Convert.ToDecimal(modelVATReview.d.SecurityDtl.Disamt));

                        }

                        if (modelVATReview.d.SecurityDtl.Liaamt.Length > 0)
                        {

                            LiaamtValue = decimal.Truncate(Convert.ToDecimal(modelVATReview.d.SecurityDtl.Liaamt));

                        }
                        if (modelVATReview.d.SecurityDtl.Secamt.Length > 0)
                        {

                            SecamtValue = decimal.Truncate(Convert.ToDecimal(modelVATReview.d.SecurityDtl.Secamt));

                        }

                        await VATObjectionGenrateorRefreshSADAD(modelVATReview.d.Fbnumx, DisamtValue, LiaamtValue, selectedApplicationRef.Abrzu?.ToString("yyyy-MM-dd'T'HH:mm:ss"), selectedApplicationRef.Abrzo?.ToString("yyyy-MM-dd'T'HH:mm:ss"), SecamtValue, SecurityNumber, selectedApplicationRef.Persl, false);

                    }
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


        private void EnableReviewReasonView()
        {
            CurrentIndex = 1;
            IsBackVisible = true;
            ReviewReasonVisible = true;
            ReviewDetailsVisible = false;
            ReportDetailsVisible = false;
            SecurityPaymentVisible = false;
            LateFilingObjectionsVisible = false;
            DeclarationVisible = false;
            SummaryVisible = false;
            selectedPage = (int)PagesEnum.ReviewReason;
        }

        private void EnableReviewDetailsView()
        {
            CurrentIndex = 3;
            IsBackVisible = true;
            ReviewReasonVisible = false;
            ReviewDetailsVisible = true;
            LateFilingObjectionsVisible = false;
            ReportDetailsVisible = false;
            SecurityPaymentVisible = false;
            DeclarationVisible = false;
            SummaryVisible = false;
            selectedPage = (int)PagesEnum.ReviewDetails;
        }
        private void EnableLateFilingDetailsView()
        {
            CurrentIndex = 4;
            IsBackVisible = true;
            ReviewReasonVisible = false;
            ReviewDetailsVisible = false;
            LateFilingObjectionsVisible = true;
            ReportDetailsVisible = false;
            SecurityPaymentVisible = false;
            DeclarationVisible = false;
            SummaryVisible = false;
            selectedPage = (int)PagesEnum.LateFiling;
        }

        private void EnableReportDetailsView()
        {
            CurrentIndex = 2;
            IsBackVisible = true;
            ReviewReasonVisible = false;
            ReportDetailsVisible = true;
            ReviewDetailsVisible = false;
            LateFilingObjectionsVisible = false;
            SecurityPaymentVisible = false;
            DeclarationVisible = false;
            SummaryVisible = false;
            selectedPage = (int)PagesEnum.ReportDetails;
        }

        private void EnableSecurityPaymentsView()
        {
            if (OverdueFlag == "X")
                CurrentIndex = 5;
            else
                CurrentIndex = 4;

            IsBackVisible = true;
            ReviewReasonVisible = false;
            LateFilingObjectionsVisible = false;
            ReportDetailsVisible = false;
            ReviewDetailsVisible = false;
            SecurityPaymentVisible = true;
            DeclarationVisible = false;
            SummaryVisible = false;
            selectedPage = (int)PagesEnum.SecurityPayments;
        }

        private void EnableDeclarationView()
        {
            if (OverdueFlag == "X")
                CurrentIndex = 6;
            else
                CurrentIndex = 5;
            IsBackVisible = true;
            ReviewReasonVisible = false;
            ReviewDetailsVisible = false;
            ReportDetailsVisible = false;
            SecurityPaymentVisible = false;
            LateFilingObjectionsVisible = false;
            DeclarationVisible = true;
            SummaryVisible = false;
            selectedPage = (int)PagesEnum.Declaration;

            EnableDeclarationConButton();
        }

        private void EnableSummaryView()
        {
            if (OverdueFlag == "X")
                CurrentIndex = 7;
            else
                CurrentIndex = 6;
            IsBackVisible = true;
            ReviewReasonVisible = false;
            ReportDetailsVisible = false;
            ReviewDetailsVisible = false;
            SecurityPaymentVisible = false;
            LateFilingObjectionsVisible = false;
            DeclarationVisible = false;
            SummaryVisible = true;
            selectedPage = (int)PagesEnum.Summary;
        }

        public void EnableSadadSecurityView()
        {
            IsSadadSecuritySelected = true;
            IsBankGurantSecuritySelected = false;

            SecurityType = AppResources.VRSADAD;


            // ShowSadadGenerateButton();
        }

        public void EnablebankGuranteeSecurityView()
        {
            IsSadadSecuritySelected = false;
            IsBankGurantSecuritySelected = true;

            SecurityType = AppResources.VRBANKGURANTEE;
        }

        public async Task GenerateSadadNumberAsync()
        {
            ShowSadadProgressLabel();
            IsSadadCheckBoxEnabled = false;
            //string fbnum, string Disamt, string Liaamt, string Abrzu, string Abrzo, string Secamt, string Security, string Persl

            if (IsGeneratingFormbundle)
            {

                decimal DisamtValue = 0;
                decimal LiaamtValue = 0;
                decimal SecamtValue = 0;

                if (modelVATReview.d.SecurityDtl.Disamt.Length > 0)
                {

                    DisamtValue = decimal.Truncate(Convert.ToDecimal(modelVATReview.d.SecurityDtl.Disamt));

                }

                if (modelVATReview.d.SecurityDtl.Liaamt.Length > 0)
                {

                    LiaamtValue = decimal.Truncate(Convert.ToDecimal(modelVATReview.d.SecurityDtl.Liaamt));

                }
                if (modelVATReview.d.SecurityDtl.Secamt.Length > 0)
                {

                    SecamtValue = decimal.Truncate(Convert.ToDecimal(modelVATReview.d.SecurityDtl.Secamt));

                }


                await VATObjectionGenrateorRefreshSADAD(modelVATReview.d.Fbnumx, DisamtValue, LiaamtValue, selectedApplicationRef.Abrzu?.ToString("yyyy-MM-dd'T'HH:mm:ss"), selectedApplicationRef.Abrzo?.ToString("yyyy-MM-dd'T'HH:mm:ss"), SecamtValue, SecurityNumber, selectedApplicationRef.Persl, true);



            }
            else
            {

                IsGeneratingFormbundle = true;
                IsSadadRefeshVisible = true;

                SaveClicked();

            }

        }

        public async void ShowSADADConfirmation()
        {
            if (IsGeneratingFormbundle)
            {
                await GenerateSadadNumberAsync();
            }
            else
            {
                await _dialogService.ShowMessage(message: AppResources.VRSadadAlert, title: AppResources.ZZZConfirmationMsg,
                    buttonConfirmText: AppResources.ZZZOkayText, buttonCancelText: AppResources.ZZCancel,
                    afterHideCallback: GenerateSadadConfirmaton);
            }
        }

        public async void GenerateSadadConfirmaton(bool status)
        {
            if (status)
            {
                await GenerateSadadNumberAsync();
            }
        }

        public void ShowSadadGenerateButton()
        {
            SadadGenerateBtnVisible = true;
            IsSadadRefeshVisible = false;
            SadadGenerateProgressVisible = false;
            SadadAmountVisible = false;
        }
        public void ShowRefreshButton()
        {
            SadadGenerateBtnVisible = false;
            IsSadadRefeshVisible = true;
            SadadGenerateProgressVisible = false;
            SadadAmountVisible = false;
        }
        public void MakeViewOnlyItems()
        {
            IsReportDetailsEditable = false;
            IsLateFilingDetailsEditable = false;
            DAPOptionsEditable = false;
            IsRRAmountEdit = false;
            IsDisputeDetailsEditable = false;
            IsSecurityPOEditable = false;
            IsSadadCheckBoxEnabled = false;

        }
        public void ShowSadadProgressLabel()
        {
            SadadGenerateBtnVisible = false;
            IsSadadRefeshVisible = false;
            SadadGenerateProgressVisible = true;
            SadadAmountVisible = false;
        }
        public void ShowSadadAmount()
        {
            SadadGenerateBtnVisible = false;
            IsSadadRefeshVisible = false;
            SadadGenerateProgressVisible = false;
            SadadAmountVisible = true;
            EnableSecurityPaymentsConButton();
        }

        private async void ViewApplicationClicked()
        {
            if (selectedReviewReason.ProcCD == "VTPC" || selectedReviewReason.ProcCD == "VTPN")
            {
                await FetchViewBill(selectedApplicationRef.Opbel, "");
            }
            else
            {
                if (selectedApplicationRef.Fbtyp == "RGVT")
                {
                    //GetVATDREGSuspensionViewApplication();

                }
                else if (selectedApplicationRef.Fbtyp == "DGVT")
                {
                    //await VatDeregistration();
                    if (selectedSubReviewReason.Code == "0012")
                    {
                        await GetVATDREGSuspensionViewApplication();
                    }
                    else if (selectedSubReviewReason.Code == "0011")
                    {
                        await GetVATDREGViewApplication();
                    }
                    else
                    {
                    }
                }
                else if (selectedApplicationRef.Fbtyp == "VTGR")
                {
                    await GetVATReviewRequestVTGR();
                }
                else if (selectedApplicationRef.Fbtyp == "VTIN" || selectedApplicationRef.Fbtyp == "TPFV")
                {
                    await GetVATReviewRequestTPFV(modelVATReview.d.Officerx, modelVATReview.d.Gpartx, modelVATReview.d.Euserx, App.LoginDataRetrieved.FbGuid);
                }
                else if (selectedApplicationRef.Fbtyp == "VATR")
                {
                    await GetViewApplication(App.LoginDataRetrieved.FbGuid, selectedApplicationRef.Fbnum, modelVATReview.d.Euserx);
                }

            }
        }

        public void PopulateAttachments(List<Attachment> attachments)
        {
            var attachmentsListViewData = new ObservableCollection<Attachment>();

            foreach (Attachment attachemnt in attachments)
            {
                attachmentsListViewData.Add(attachemnt);
            }

            if (_isBankGuranteeAttachments)
            {

                BankGuranteeAttachmentsListViewData = attachmentsListViewData;

                EnableSecurityPaymentsConButton();
            }
            else if(_isReportDetailsAttachments)
            {
                AttachmentsListViewData = attachmentsListViewData;
                
                EnableReviewDetailsConButton();
            }
            else if(_isLateFilingAttachments)
            {
                LateFilingAttachmentsListViewData = attachmentsListViewData;
                EnableLateFilingsDetailsConButton();
                //EnableSecurityPaymentsConButton();
            }


        }

        public void PopulateVatDeRegSummaryReasonData(string requestType, string reasonTitle)
        {
            VATDeregistrationSummaryReasonData = null;
            List<VATDeregistrationSummaryModel> check = new List<VATDeregistrationSummaryModel>();
            try
            {

                if (requestType == "S")
                {
                    check.Add(new VATDeregistrationSummaryModel
                    {

                        SummaryTitle = AppResources.VatDeregRequestType,
                        SummaryData = AppResources.VATDeregistrationReasonType2,
                        IsEditVisible = true
                    });
                }
                else
                {
                    check.Add(new VATDeregistrationSummaryModel
                    {
                        SummaryTitle = AppResources.VatDeregRequestType,
                        SummaryData = AppResources.VATDeregistrationReasonType1,
                        IsEditVisible = true
                    });
                }

                //check.Add(new VATDeregistrationSummaryModel
                //{
                //    SummaryTitle = AppResources.VatDeregRequestType,
                //    SummaryData = requestType,
                //    IsEditVisible = true
                //});
                check.Add(new VATDeregistrationSummaryModel
                {
                    SummaryTitle = AppResources.VatDeregReasonTitle,
                    SummaryData = reasonTitle,
                    IsEditVisible = true
                });
                VATDeregistrationSummaryReasonData = new ObservableCollection<VATDeregistrationSummaryModel>(check);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }
        public void PopulateVatDeRegAttachments(List<Attachment> attachments)
        {
            var attachmentsListViewData = new ObservableCollection<Attachment>();
            foreach (Attachment attachemnt in attachments)
            {
                if (attachemnt.Dotyp == "ZVTD" || attachemnt.Dotyp == "ZVTG" || attachemnt.Dotyp == "ZVTH" || attachemnt.Dotyp == "ZVTI" || attachemnt.Dotyp == "ZVRA")
                {
                    attachmentsListViewData.Add(attachemnt);
                }
            }
            VatDeRegAttachmentsList = attachmentsListViewData;

        }

        public void PopulateVatDeRegSummaryDeclarationData(string idType, string iDNumber, string dateOfBirth, string contactPersonName)
        {
            //VATDeregistrationSummaryDeclarationData = new ObservableCollection<VATDeregistrationSummaryModel>();
            List<VATDeregistrationSummaryModel> check = new List<VATDeregistrationSummaryModel>();
            try
            {
                check.Add(new VATDeregistrationSummaryModel
                {
                    SummaryTitle = AppResources.IDType,
                    SummaryData = idType,
                    IsEditVisible = true
                });
                check.Add(new VATDeregistrationSummaryModel
                {
                    SummaryTitle = AppResources.IDNumber,
                    SummaryData = iDNumber,
                    IsEditVisible = true
                });

                //check.Add(new VATDeregistrationSummaryModel
                //{
                //    SummaryTitle = AppResources.VatDeregDOBTitle,
                //    SummaryData = dateOfBirth,
                //    IsEditVisible = true
                //});

                if (!string.IsNullOrEmpty(dateOfBirth))
                {
                    check.Add(new VATDeregistrationSummaryModel
                    {
                        SummaryTitle = AppResources.VatDeregDOBTitle,
                        SummaryData = dateOfBirth,
                        IsEditVisible = true
                    });
                }


                check.Add(new VATDeregistrationSummaryModel
                {
                    SummaryTitle = AppResources.VatDeregContactPerson,
                    SummaryData = contactPersonName,
                    IsEditVisible = true
                });
                VATDeregistrationSummaryDeclarationData = new ObservableCollection<VATDeregistrationSummaryModel>(check);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }

        private void BackNavigations()
        {
            switch (selectedPage)
            {
                case (int)PagesEnum.ReviewReason:
                    _navigationService.GoBack();
                    break;

                case (int)PagesEnum.ReviewDetails:
                    EnableReportDetailsView();
                    break;
                case (int)PagesEnum.ReportDetails:
                    EnableReviewReasonView();
                    break;
                case (int)PagesEnum.LateFiling:
                    EnableReviewDetailsView();
                    break;
                case (int)PagesEnum.SecurityPayments:
                    if (OverdueFlag == "X")
                    {
                        EnableLateFilingDetailsView();
                    }
                    else
                    {
                        EnableReviewDetailsView();
                    }

                    break;
                case (int)PagesEnum.Declaration:
                    if (IsSecurityPaymentsTabVisible)
                    {
                        EnableSecurityPaymentsView();
                    }
                    else
                    {
                        EnableReportDetailsView();
                    }
                    break;
                case (int)PagesEnum.Summary:
                    EnableDeclarationView();
                    break;

            }
        }

        public async void updatePickerData(GenericPickerModel genericPickerModel)
        {
            if (genericPickerModel.PickerId == PickerEnum.IDType.ToString())
            {
                IDTypePickerModel = genericPickerModel;
                updateIdTypePicker();
            }
            else if (genericPickerModel.PickerId == PickerEnum.ReviewReason.ToString())
            {
                ReviewReasonPickerModel = genericPickerModel;
                ReviewReason = ReviewReasonPickerModel.SelectedValue;
                ResetDataAfterReviewReasonPicked();
                setReviewSubReasonPickerModel(ReviewReasonPickerModel.SelectedValue);
            }
            else if (genericPickerModel.PickerId == PickerEnum.ReviewSubReason.ToString())
            {
                ReviewSubReasonPickerModel = genericPickerModel;
                SubReviewReason = ReviewSubReasonPickerModel.SelectedValue;
                ResetDataAfterSubReviewReasonPicked();
                await fetchApplicationRefNums(ReviewSubReasonPickerModel.SelectedValue, "", "");
            }
            else if (genericPickerModel.PickerId == PickerEnum.ApplicationReferenceNumber.ToString())
            {
                ApplicationRefPickerModel = genericPickerModel;
                ApplicationRefNumber = ApplicationRefPickerModel.SelectedValue;

                ResetDataAfterAppRefNumPicked();
                setDataBasedOnAppRefNum(ApplicationRefPickerModel.SelectedValue);

                /*var billNumberObj= appRefNumList.Find(appRef => (appRef.Fbnum == ApplicationRefNumber) || (appRef.Opbel == ApplicationRefNumber));

                                if (billNumberObj.Msgflg == "X")
                                {
                                    await _dialogService.ShowMessage(billNumberObj.Msgtxt, AppResources.CRWarning);
                                }*/
            }
        }

        private void ResetDataAfterReviewReasonPicked()
        {

            ReviewSubReasonPickerModel = null;
            ApplicationRefPickerModel = null;
            SubReviewReason = "";
            ApplicationRefNumber = "";
            RequestDate = null;
            FormattedRequestDate = null;
            IsApplicationVisible = false;
            EnableReviewReasonConButton();


        }
        private void ResetDataAfterSubReviewReasonPicked()
        {
            ApplicationRefPickerModel = null;
            ApplicationRefNumber = "";
            RequestDate = null;
            FormattedRequestDate = null;
            IsApplicationVisible = false;
            EnableReviewReasonConButton();

        }
        private void ResetDataAfterAppRefNumPicked()
        {
            RequestDate = null;
            FormattedRequestDate = null;
            EnableReviewReasonConButton();

        }


        public async Task fetchApplicationRefNums(string SubReasonValue, string fbnumx, string sopbel)
        {
            selectedSubReviewReason = subReviewReasonList.First(subReviewReason => subReviewReason.SubReasons == SubReasonValue);
            await VATObjectionFormRejected(modelVATReview.d.Fbustx, selectedReviewReason.ProcCD, selectedSubReviewReason.Code,
                modelVATReview.d.UserTypx, fbnumx, sopbel);
        }

        public async void setDataBasedOnAppRefNum(string appRefNum)
        {



            selectedApplicationRef = appRefNumList.Find(appRef => (appRef.Fbnum == appRefNum) || (appRef.Opbel == appRefNum));




            if (selectedApplicationRef != null)
            {

                IsApplicationVisible = true;

                if (selectedApplicationRef.Fbtyp == "RGVT")
                {

                    IsApplicationVisible = false;

                }
                else if (selectedApplicationRef.Fbtyp == "VTGR")
                {

                    IsApplicationVisible = true;
                }

                else if (selectedApplicationRef.Fbtyp == "DGVT")
                {
                    //await VatDeregistration();

                    if (selectedSubReviewReason.Code == "0012")
                    {

                    }
                    else if (selectedSubReviewReason.Code == "0011")
                    {
                    }
                    else
                    {
                        IsApplicationVisible = false;
                    }
                }

                RequestDate = selectedApplicationRef.DecDt;


                string strRequestedDate = "";
                if (selectedApplicationRef.DecDt != null)
                {

                    DateTime dateStart = new DateTime();
                    CultureInfo cultureInfo = new CultureInfo("ar-SA");
                    string apiDate = @"""" + selectedApplicationRef.DecDt + @"""";
                    dateStart = (DateTime)selectedApplicationRef.DecDt;

                    GregorianCalendar hjCalendar = new GregorianCalendar();
                    int year = hjCalendar.GetYear(dateStart);
                    int month = hjCalendar.GetMonth(dateStart);
                    int day = hjCalendar.GetDayOfMonth(dateStart);

                    string dateStr = string.Format("{0:00}/{1}/{2}", year, month, day);


                    string dt1 = string.Empty;
                    string[] dts = null;
                    dts = dateStr.Split('/');

                    //if (App.IsArabic)
                    //{

                    dt1 = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];

                    //}
                    //else
                    //{

                    //    dt1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];

                    //}


                    strRequestedDate = dt1;
                }

                //PickedDate = strRequestedDate;

                FormattedRequestDate = strRequestedDate;



                TaxPeriodOfCase = selectedApplicationRef.Perslt;
                TaxPeriodFrom = selectedApplicationRef.Abrzu;
                TaxPeriodTo = selectedApplicationRef.Abrzo;

                string strTaxPeriodFrom = "";
                if (selectedApplicationRef.Abrzu != null)
                {

                    DateTime dateStart = new DateTime();

                    dateStart = (DateTime)selectedApplicationRef.Abrzu;

                    GregorianCalendar hjCalendar = new GregorianCalendar();
                    int year = hjCalendar.GetYear(dateStart);
                    int month = hjCalendar.GetMonth(dateStart);
                    int day = hjCalendar.GetDayOfMonth(dateStart);

                    string dateStr = string.Format("{0:00}/{1}/{2}", year, month, day);


                    string dt1 = string.Empty;
                    string[] dts = null;
                    dts = dateStr.Split('/');

                    //if (App.IsArabic)
                    //{

                    dt1 = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];

                    //}
                    //else
                    //{

                    //    dt1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];

                    //}

                    strTaxPeriodFrom = dt1;
                }

                OverdueFlag = selectedApplicationRef.OVERDUEFG;
                if (OverdueFlag == "X")
                    MaxIndex = 7;
                else
                    MaxIndex = 6;
                CurrentIndex = 1;

                lastFulfilmentDate = UtilityManager.FormatDateToYYYYDDMMFromDateTypeString(selectedApplicationRef.LastFulfilledDt);

                LastFulfilmentDateText = string.Format(AppResources.VRSecurityBankGuarante, lastFulfilmentDate);

                string strTaxPeriodTo = "";
                if (selectedApplicationRef.Abrzo != null)
                {

                    DateTime dateStart = new DateTime();
                    CultureInfo cultureInfo = new CultureInfo("ar-SA");
                    string apiDate = @"""" + selectedApplicationRef.Abrzo + @"""";
                    dateStart = (DateTime)selectedApplicationRef.Abrzo;

                    GregorianCalendar hjCalendar = new GregorianCalendar();
                    int year = hjCalendar.GetYear(dateStart);
                    int month = hjCalendar.GetMonth(dateStart);
                    int day = hjCalendar.GetDayOfMonth(dateStart);

                    string dateStr = string.Format("{0:00}/{1}/{2}", year, month, day);


                    string dt1 = string.Empty;
                    string[] dts = null;
                    dts = dateStr.Split('/');

                    //if (App.IsArabic)
                    //{

                    dt1 = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];

                    //}
                    //else
                    //{

                    //    dt1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];

                    //}

                    strTaxPeriodTo = dt1;
                }

                FormattedTaxperdioFromDate = strTaxPeriodFrom;
                FormattedTaxperdioToDate = strTaxPeriodTo;

                PenalityAmountInQuestion = selectedApplicationRef.Penamount;

                TotalTaxLiability = selectedApplicationRef.Liaamt;
                TaxPaid = selectedApplicationRef.Clramt;
                RequestedReviewAmount = UtilityManager.GetCommaSeparatedAmount(selectedApplicationRef.Liaamt.ToString());

                SecurityAmount =
                    (Double.Parse(selectedApplicationRef.Liaamt) - Double.Parse(selectedApplicationRef.Clramt))
                    .ToString();

                if (Double.Parse(SecurityAmount) < 0)
                {
                    SecurityAmount = "0.0";
                }

                if (modelVATReview.d.SecurityDtl.SeczeroFlg == "S")
                {
                    SecurityAmount = "0.0";
                }

                try
                {
                    if (Double.Parse(SecurityAmount) == 0)
                    {
                        IsSecurityAmountMorethanZero = false;
                    }
                    else
                    {
                        IsSecurityAmountMorethanZero = true;
                    }
                    EnableSecurityPaymentsConButton();
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());
                }

                if (selectedApplicationRef.Msgflg == "X")
                {

                    if (string.IsNullOrEmpty(modelVATReview.d.SecurityDtl.Sopbel))
                    {
                        IsApplicationVisible = false;

                    }
                    else
                    {
                        IsApplicationVisible = true;
                        EnableReviewReasonConButton();

                    }


                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(selectedApplicationRef.Msgtxt, AppResources.CRWarning);
                    });
                }
                else if (selectedApplicationRef.Fbtyp == "VATR")
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.VATWarningAssessment, AppResources.CRWarning);
                        EnableReviewReasonConButton();
                    });
                }
                else if (selectedApplicationRef.Fbtyp == "VTPN" && selectedApplicationRef.Pentyp == "R")
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.VATWarningPenalties, AppResources.CRWarning);
                        EnableReviewReasonConButton();
                    });
                }
                else
                {

                    EnableReviewReasonConButton();
                }



            }

        }

        private void AddDisputeAmountPaymentOptions()
        {
            var disputeAmountPaymentOptions = new ObservableCollection<SelectionModel>();
            disputeAmountPaymentOptions.Add(new SelectionModel
            {
                SelectionTitle = AppResources.VRInfull,
                IsSelected = true
            });
            disputeAmountPaymentOptions.Add(new SelectionModel
            {
                SelectionTitle = AppResources.VRInpartial,
                IsSelected = false
            });
            DisputeAmountPaymentOptions = disputeAmountPaymentOptions;
            vRInterface.SelectDefaultPaymentOption();
        }

        public void updateIdTypePicker()
        {
            IDType = IDTypePickerModel.SelectedValue;
            ContactPersonName = "";
            IDNumber = "";
            if (IDType == AppResources.VFCGCCID)
            {
                IsDOBVisible = false;
                ContractPersonEditable = true;
            }
            else
            {
                IsDOBVisible = true;
                ContractPersonEditable = false;
            }

            ValidateIdNumber();
        }

        public async void ValidateIdNumber()
        {
            try
            {
                IsIDVerified = false;
                EnableDeclarationConButton();
                PopUp popUp = new PopUp();
                StringBuilder Messages = new StringBuilder();
                if (!string.IsNullOrEmpty(IDNumber))
                {
                    if (IDType.Equals(AppResources.VFCNationalID))
                    {
                        if (IDNumber.Substring(0, 1) != "1")
                        {
                            popUp.Message = AppResources.ZZNationalIDstartswith1;
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }

                            //await PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));

                            IDNumber = string.Empty;
                            //ZZPleaseenteravalidNationalID
                        }
                        else
                        {
                            if (IDNumber.Length != 10)
                            {
                                if (Messages.Length > 0)
                                {
                                    Messages.Append(Environment.NewLine);
                                }

                                Messages.Append(AppResources.ZZNationalIDlengthis10digit);
                            }

                            if (Messages.Length > 0)
                            {
                                popUp.Message = Messages.ToString();
                                popUp.IsLinkAvailable = false;
                                if (App.IsArabic)
                                {
                                    popUp.FlowDirections = "RightToLeft";
                                    popUp.isFontSet = true;
                                }
                                else
                                {
                                    popUp.FlowDirections = "LeftToRight";
                                }

                                //await PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));

                                IDNumber = string.Empty;
                            }
                            else
                            {

                                if (!string.IsNullOrEmpty(PickedDate))
                                {
                                    await ValidateIdNumberFromApi("ZS0001");
                                }


                            }
                        }


                    }

                    if (IDType.Equals(AppResources.VFCIqamaID))
                    {
                        if (IDNumber.Substring(0, 1) != "2")
                        {
                            popUp.Message = AppResources.ZZIqamaIDstartswith2;
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }

                            //await PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));

                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));

                            IDNumber = string.Empty;
                        }
                        else
                        {
                            if (IDNumber.Length != 10)
                            {
                                if (Messages.Length > 0)
                                {
                                    Messages.Append(Environment.NewLine);
                                }

                                Messages.Append(AppResources.ZZIqamaIDlengthis10digit);

                            }

                            if (Messages.Length > 0)
                            {
                                popUp.Message = Messages.ToString();
                                popUp.IsLinkAvailable = false;
                                if (App.IsArabic)
                                {
                                    popUp.FlowDirections = "RightToLeft";
                                    popUp.isFontSet = true;
                                }
                                else
                                {
                                    popUp.FlowDirections = "LeftToRight";
                                }

                                //await PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));

                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));

                                IDNumber = string.Empty;
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(PickedDate))
                                {
                                    await ValidateIdNumberFromApi("ZS0002");
                                }
                            }
                        }


                    }

                    if (IDType.Equals(AppResources.VFCGCCID))
                    {

                        if (IDNumber.Substring(0, 1) == "0")
                        {
                            //Have to change to neww error message
                            popUp.Message = AppResources.ZZGCCIDdonotstartwith0;
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }

                            //await PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));

                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGCCIDdonotstartwith0));

                            IDNumber = string.Empty;
                        }
                        else if (!(IDNumber.Length <= 15 && IDNumber.Length >= 7))
                        {
                            popUp.Message = AppResources.ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit;
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }

                            //await PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));

                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit));

                            IDNumber = string.Empty;
                            // EntryIDNumber.Text = string.Empty;//ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit
                        }
                        else
                        {
                            IsIDVerified = true;
                            EnableDeclarationConButton();
                        }


                    }
                }
                else
                {
                }


            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());

            }
        }

        public void HideSadadGenerateButton()
        {
            SadadGenerateBtnVisible = false;
            IsSadadRefeshVisible = false;
            SadadGenerateProgressVisible = false;
            SadadAmountVisible = false;
        }

        public void ResetData()
        {
            EnableReviewReasonView();
            ShowInstructionsDialog();

            IsSadadCheckBoxEnabled = true;
            HideSadadGenerateButton();

            IDTypeDictionary = new Dictionary<string, string>
            {
                {AppResources.VFCNationalID, "ZS0001"},
                {AppResources.VFCIqamaID, "ZS0002"},
                {AppResources.VFCGCCID, "ZS0003"},
            };


            VGSupplicesDictionary = new Dictionary<string, string>
            {
                {"01",AppResources.VRVGLessthanSAR187500},
                { "02",AppResources.VRVGBetweenSAR18750andSAR375000},
                { "03",AppResources.VRVGBetweenSAR375000andSAR1000000},
                { "04",AppResources.VRVGBetweenSAR1000000andSAR40000000},
                { "05",AppResources.VRVGGreaterthanSAR40000000},
            };
            VGPurchasesDictionary = new Dictionary<string, string>
            {
                {"01",AppResources.VRVGLessthanSAR187500},
                { "02",AppResources.VRVGGreaterthanSAR187500},
            };



            charCountReportDetails = 0 + "/" + 1000;
            charCountDisputeDetails = 0 + "/" + 1000;
            PickedDateFullMonth = "";
            IsGeneratingFormbundle = false;
            IsSadadSecuritySelected = false;
            IsBankGurantSecuritySelected = false;
            IsSecurityPaymentsTabVisible = false;
            PickedDate = "";
            ContactPersonName = "";
            ReviewReason = "";
            SubReviewReason = "";
            ApplicationRefNumber = "";
            RequestDate = null;
            FormattedRequestDate = null;
            TaxPeriodOfCase = "";
            TaxPeriodFrom = null;
            TaxPeriodTo = null;
            PenalityAmountInQuestion = "";
            ReportDetails = "";
            LateFlngDetails = "";
            SADADNumber = "";
            SecurityAmount = "";
            IDNumber = "";
            IsIDVerified = false;
            ContractPersonEditable = false;
            IDType = "";
            IsDOBVisible = false;
            ReviewReasonPickerModel = null;
            ReviewSubReasonPickerModel = null;
            ApplicationRefPickerModel = null;
            IDTypePickerModel = null;
            IsReviewReasonEnabled = false;
            ReviewReasonButtonBackGroundColor = Color.FromHex("#9EA4A9");
            IsReviewDetailsEnabled = false;
            ReviewDetailsButtonBackGroundColor = Color.FromHex("#9EA4A9");
            LateFilingDetailsButtonBackGroundColor = Color.FromHex("#9EA4A9");
            IsSecurityPaymentEnabled = false;
            SecurityPaymentButtonBackGroundColor = Color.FromHex("#9EA4A9");
            IsDeclarationEnabled = false;
            DeclarationButtonBackGroundColor = Color.FromHex("#9EA4A9");
            AttachmentsListViewData = null;
            SadadGenerateBtnVisible = false;
            SadadGenerateProgressVisible = false;
            SadadAmountVisible = false;
            BankGuranteeAttachmentsListViewData = null;
            LateFilingAttachmentsListViewData = null;
            IsSadadCheckBox3 = false;
            IsSadadCheckBox1 = false;
            IsApplicationVisible = false;
            DisputeDetailsDesc = "";
            RequestedReviewAmount = "";
            TotalTaxLiability = "";
            TaxPaid = "";

            DefaultSecurity = 0;
            DefaultReq = 0;

            IsReportDetailsEditable = true;
            IsLateFilingDetailsEditable = true;
            DAPOptionsEditable = true;
            IsRRAmountEdit = false;
            IsDisputeDetailsEditable = true;
            IsSecurityPOEditable = true;

            AddSecurityPaymentOptions();
            setIdPickerModel();

            AddDisputeAmountPaymentOptions();

        }

        public void EnableReviewReasonConButton()
        {
            if (ReviewReason == "" || SubReviewReason == "" || ApplicationRefNumber == "" || RequestDate == null)
            {

                if (!string.IsNullOrEmpty(SADADNumber))
                {

                    IsReviewReasonEnabled = true;
                }
                else
                {
                    IsReviewReasonEnabled = false;
                }


            }
            else
            {
                IsReviewReasonEnabled = true;
            }
        }

        public void EnableReportDetailsConButton()
        {
            if (ReportDetails == "")
            {
                IsReportDetailsEnabled = false;
            }
            else
            {
                IsReportDetailsEnabled = true;
            }
        }

        public void EnableLateFilingsDetailsConButton()
        {
           if (!string.IsNullOrEmpty(LateFlngDetails) && LateFilingAttachmentsListViewData!=null&& LateFilingAttachmentsListViewData.Count>0)
            {
                IsLateFlngDetailsEnabled = true;
            }
            else
            {
                IsLateFlngDetailsEnabled = false; ;
            }
        }


        public void EnableReviewDetailsConButton()
        {
            if ((IsAssessPayOptionVisible && (DisputeDetailsDesc == "" || RequestedReviewAmount == "")))
            {
                IsReviewDetailsEnabled = false;
            }
            else
            {
                IsReviewDetailsEnabled = true;
            }
        }
        public void EnableSecurityPaymentsConButton()
        {

            if (!IsSecurityAmountMorethanZero)
            {
                IsSecurityPaymentEnabled = true;
            }
            else if (IsSadadSecuritySelected)
            {

                /*if (SecurityAmount == "" || !IsSadadCheckBox3)
                {

                    IsSecurityPaymentEnabled = false;


                }*/
                if (SADADNumber == "" || !IsSadadCheckBox3)
                {
                    IsSecurityPaymentEnabled = false;
                }
                else
                {
                    IsSecurityPaymentEnabled = true;
                }


            }
            else if (IsBankGurantSecuritySelected)
            {
                if (BankGuranteeAttachmentsListViewData == null || BankGuranteeAttachmentsListViewData.Count == 0 || !IsSadadCheckBox1)
                {

                    IsSecurityPaymentEnabled = false;

                }
                else
                {
                    IsSecurityPaymentEnabled = true;

                }


            }


        }

        public async void OpenAttachment(Attachment attachment)
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });
            //if (attachment.Filename.Contains("."))
            //string Extention = attachment.Filename.Split('.')[1];
            if (attachment.FileExtn.Equals("PDF") || attachment.FileExtn.Equals("pdf"))
            {
                if (attachment.DocUrl != null)
                {
                    _navigationService.NavigateTo(App.PdfView, attachment.DocUrl);
                }
            }
            else
            {
                await GetVATReviewWebServiceManager.email(attachment.Doguid, attachment);
            }

            await Task.Run(() =>
            {
                IsLoading = false;
            });

        }


        public void EnableDeclarationConButton()
        {
            if (ContactPersonName == "" || !IsIDVerified || !IsDECCheckBox)
            {
                IsDeclarationEnabled = false;
            }
            else
            {
                IsDeclarationEnabled = true;
            }
        }
        public async void ShowInstructionsDialog()
        {


            if (App.selectedVATItem != "")
            {

                await PopupNavigation.Instance.PushAsync(new InstructionsBottomPopUpView(instructionString: AppResources.VRInstructions, checkBoxString: AppResources.VRCheckBoxDesc, continueString: AppResources.CRContinue, isEditable: true,
           _dialogType: ZakatInstalmentViewModel.InstructionsBottomPopUpViewModel.DialogType
               .Instructions));


            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new InstructionsBottomPopUpView(
                 instructionString: AppResources.VRInstructions, checkBoxString: AppResources.VRCheckBoxDesc,
                 continueString: AppResources.CRContinue,
                 _dialogType: InstructionsBottomPopUpViewModel.DialogType
                     .Instructions));
            }
        }

        public async Task PopulateDraftData()
        {
            var selectedReason = reviewReasonList.First(x => x.ProcCD == modelVATReview.d.RvRsn);
            ReviewReason = selectedReason.Reasons;
            setReviewSubReasonPickerModel(ReviewReason);
            SubReviewReason = selectedReason.ListSubReason.First(x => x.Code == modelVATReview.d.RvSubRsn).SubReasons;


            await fetchApplicationRefNums(SubReviewReason, modelVATReview.d.Fbnumx, modelVATReview.d.SecurityDtl.Sopbel);
            ApplicationRefNumber = modelVATReview.d.RejFb;
            setDataBasedOnAppRefNum(ApplicationRefNumber);


            DefaultSecurity = modelVATReview.d.SecurityDtl.Sectp == "B" ? 1 : 0;
            DefaultReq = modelVATReview.d.SecurityDtl.Amttp == "P" ? 1 : 0;
            MessagingCenter.Send<object, int>(this, "draftSecurity", modelVATReview.d.SecurityDtl.Sectp == "B" ? 1 : 0);
            MessagingCenter.Send<object, int>(this, "draftRequest", modelVATReview.d.SecurityDtl.Amttp == "P" ? 1 : 0);


            if (modelVATReview.d.IdType == "ZS0001")
            {
                IDType = AppResources.VFCNationalID;
                IsDOBVisible = true;

            }
            else if (modelVATReview.d.IdType == "ZS0002")
            {
                IDType = AppResources.VFCIqamaID;
                IsDOBVisible = false;
                // PickedDate = modelVATReview.d.DecDt;


            }
            else if (modelVATReview.d.IdType == "ZS0003")
            {
                IDType = AppResources.VFCGCCID;
                IsDOBVisible = false;
            }


            IDNumber = modelVATReview.d.DecIdNo;
            ContactPersonName = modelVATReview.d.Decnm;
            if (!string.IsNullOrEmpty(ContactPersonName))
            {
                IsIDVerified = true;
                IsDOBVisible = false;
                if (modelVATReview.d.IdType == "ZS0003" && string.IsNullOrEmpty(IDNumber))
                {
                    IsIDVerified = false;
                    IsDOBVisible = true;
                }
            }


            var bankAttachments = new ObservableCollection<Attachment>();
            var attachments = new ObservableCollection<Attachment>();
            var lateFilngAttachments = new ObservableCollection<Attachment>();
            foreach (var attach in modelVATReview.d.AttdetSet.results)
            {
                if (attach.Dotyp == "RAGA")
                {
                    attachments.Add(attach);
                }
                else if (attach.Dotyp == "RVBT")
                {
                    bankAttachments.Add(attach);
                }
                else if (attach.Dotyp == "ZVRA")
                {
                    lateFilngAttachments.Add(attach);
                }
            }
            BankGuranteeAttachmentsListViewData = bankAttachments;
            AttachmentsListViewData = attachments;
            LateFilingAttachmentsListViewData = lateFilngAttachments;


            if (modelVATReview.d.SecurityDtl.ChkBank == "X")
            {
                IsSadadCheckBox1 = true;
            }
            else
            {
                IsSadadCheckBox1 = false;
            }
            if (modelVATReview.d.SecurityDtl.ChkCash == "X")
            {
                IsSadadCheckBox3 = true;
            }
            else
            {
                IsSadadCheckBox3 = false;
            }

            if (modelVATReview.d.DecFlg1 == true)
            {
                IsDECCheckBox = true;
            }
            else
            {
                IsDECCheckBox = false;
            }

            if (string.IsNullOrEmpty(modelVATReview.d.SecurityDtl.Sopbel))
            {

            }
            else
            {
                SADADNumber = modelVATReview.d.SecurityDtl.Sopbel;
                SecurityNumber = modelVATReview.d.SecurityDtl.Security;
                IsSadadCheckBox3 = true;


                MakeViewOnlyItems();
                ShowSadadAmount();
            }


            if (modelVATReview.d.NotesSet.results.Count > 0)
            {


                foreach (var note in modelVATReview.d.NotesSet.results)
                {
                    if (note.Rcodez == "RAVT_SDCAS" && !String.IsNullOrEmpty(note.Strline) && string.IsNullOrEmpty(DisputeDetailsDesc))
                    {
                        DisputeDetailsDesc = note.Strline;

                    }

                }

                foreach (var note in modelVATReview.d.NotesSet.results)
                {

                    if (note.Rcodez == "RAVT_BOX" && !String.IsNullOrEmpty(note.Strline) && string.IsNullOrEmpty(ReportDetails))
                    {
                        ReportDetails = note.Strline;

                    }

                }
                foreach (var note in modelVATReview.d.NotesSet.results)
                {

                    if (note.Rcodez == "RVT_OVRDUE" && !String.IsNullOrEmpty(note.Strline) && string.IsNullOrEmpty(LateFlngDetails))
                    {
                        LateFlngDetails = note.Strline;

                    }

                }

            }

            if (selectedApplicationRef != null)
            {

                if (selectedApplicationRef.Msgflg == "X")
                {

                    if (string.IsNullOrEmpty(modelVATReview.d.SecurityDtl.Sopbel))
                    {
                        IsApplicationVisible = false;

                    }
                    else
                    {
                        IsApplicationVisible = true;
                        EnableReviewReasonConButton();


                    }


                }
                else
                {
                    EnableReviewReasonConButton();

                }
            }
            else
            {
                EnableReviewReasonConButton();
            }






            EnableReportDetailsConButton();
            EnableLateFilingsDetailsConButton();
            EnableDeclarationConButton();
            EnableReviewDetailsConButton();
            EnableSecurityPaymentsConButton();

        }


        private void AddSecurityPaymentOptions()
        {

            var securityPaymentOptions = new ObservableCollection<SelectionModel>();
            securityPaymentOptions.Add(new SelectionModel
            {
                SelectionTitle = AppResources.VRSADAD,
                IsSelected = false
            });
            securityPaymentOptions.Add(new SelectionModel
            {
                SelectionTitle = AppResources.VRBANKGURANTEE,
                IsSelected = false
            });
            SecurityPaymentOptions = securityPaymentOptions;
        }





        #region ApiRegion

        public async Task ValidateIdNumberFromApi(string idType)
        {
            try
            {
                await Task.Run(() => { IsLoading = true; });
                await Task.Run(async () =>
                {
                    IsLoading = true;
                    try
                    {
                        var resultData = await VATChangeFillingWebServiceManager.GAZTVATChangeFillingPeriodValidateIDnumber(
                            App.LoginDataRetrieved.TIN, idType, IDNumber, "", "", PickedDate.Replace("/", ""));
                        if (resultData != null && resultData.d != null)
                        {
                            IsIDVerified = true;
                            ContactPersonName = resultData.d.Name2 + " " + resultData.d.Name1;
                            EnableDeclarationConButton();
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                IsLoading = false;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(resultData.errorMessage));

                            });
                        }

                        IsLoading = false;
                    }
                    catch (GAZTVATChangeFillingPeriodException ex)
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
                await Task.Run(() => { IsLoading = false; });
            }
            catch (GAZTVATChangeFillingPeriodException ex)
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
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                await Task.Run(() => { IsLoading = false; });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public async Task VatReviewReasonDropDownData()
        {
            try
            {
                await Task.Run(() => { IsLoading = true; });
                await Task.Run(async () =>
                {
                    IsLoading = true;
                    //VATReview = null;
                    //VATObjectionSummaryModel modelVATReview = new VATObjectionSummaryModel();
                    VATObjectionFormModel.VATReviewsReturnModel modelVATReviewsReturn =
                        new VATObjectionFormModel.VATReviewsReturnModel();
                    try
                    {

                        if (App.selectedVATItem != "")
                        {

                            modelVATReview = await VATObjectionWebServiceManager.GAZTGetVATObjectionSummary(App.selectedVATItem);

                        }
                        else
                        {
                            modelVATReview = await VATObjectionWebServiceManager.GAZTGetVATObjectionSummary("");

                        }



                        if (modelVATReview != null && modelVATReview.d != null)
                        {

                            //modelVATReviewsReturn.ListReviewReason lstReasons =new modelVATReviewsReturn.ListReviewReason;
                            // List<Dictionary<string, string>> reasonDDL = new List<Dictionary<string, string>>();
                            List<VATObjectionFormModel.ReviewReason> reasonList =
                                new List<VATObjectionFormModel.ReviewReason>();
                            if (modelVATReview.d.MainReasonSet.results.Count > 0)
                            {

                                for (int i = 0; i < modelVATReview.d.MainReasonSet.results.Count; i++)
                                {

                                    VATObjectionFormModel.ReviewReason obj = new VATObjectionFormModel.ReviewReason();
                                    obj.ProcCD = modelVATReview.d.MainReasonSet.results[i].ProcCd;
                                    obj.Reasons = modelVATReview.d.MainReasonSet.results[i].TypeT;
                                    if (modelVATReview.d.ReasonSet.results.Where(x =>
                                        x.ProcCd == modelVATReview.d.MainReasonSet.results[i].ProcCd).Count() > 0)
                                    {
                                        List<VATObjectionFormModel.SubReason> subReasonList =
                                            new List<VATObjectionFormModel.SubReason>();
                                        var lstSub = modelVATReview.d.ReasonSet.results.Where(x =>
                                            x.ProcCd == modelVATReview.d.MainReasonSet.results[i].ProcCd).ToList();
                                        for (int j = 0; j < lstSub.Count(); j++)
                                        {

                                            VATObjectionFormModel.SubReason sub = new VATObjectionFormModel.SubReason();
                                            sub.Code = lstSub[j].Code;
                                            sub.SubReasons = lstSub[j].SubtypT;
                                            subReasonList.Add(sub);

                                        }

                                        obj.ListSubReason = subReasonList;
                                    }

                                    reasonList.Add(obj);


                                }
                            }

                            reviewReasonList = reasonList;
                            modelVATReviewsReturn.ListReviewReason = reasonList;


                            if (App.selectedVATItem != "")
                            {
                                await PopulateDraftData();

                            }
                            setReviewReasonPickerModel();
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

                    }
                });
                await Task.Run(() => { IsLoading = false; });
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
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                await Task.Run(() => { IsLoading = false; });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public async void VATObjectionEnableSubmit(string statusx, string rvRsn, string rvSubRsn, string rejFb)
        {

            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                statusx = null;
                rvRsn = null;
                rvSubRsn = null;
                rejFb = null;
                VATObjectionEnableSubmitModel _VATObjectionEnableSubmit = new VATObjectionEnableSubmitModel();
                _VATObjectionEnableSubmit = await VATObjectionWebServiceManager.GAZTGetVATObjectionEnableSubmit(statusx, rvRsn, rvSubRsn, rejFb);

                if (_VATObjectionEnableSubmit != null && _VATObjectionEnableSubmit.d != null)
                {
                    //setApplicationRefPickerModel(_VATObjectionEnableSubmit);
                }

                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (GAZTErrorException ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
        }

        public void VATObjectionDownloadAck(string fbnum)
        {

            try
            {
                fbnum = null;
                string strACK = null;
                strACK = VATObjectionWebServiceManager.GetVATObjectionDownloadAck(fbnum);

                if (strACK != null)
                {

                }
            }
            catch (GAZTErrorException ex)
            {
                _dialogService.ShowMessage(ex.Message, AppResources.Information);
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

        public async Task VATObjectionFormRejected(string fbustx, string RvRsn, string rvSubRsn, string UserTypx, string fbnumx, string sopbel)
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                //VATObjectionRejectedFormModel _VATObjectionRejected = new VATObjectionRejectedFormModel();

                _VATObjectionRejected = await VATObjectionWebServiceManager.GAZTGetVATObjectionFormRejected(fbustx, RvRsn, rvSubRsn, UserTypx, fbnumx, sopbel);
                if (_VATObjectionRejected != null && _VATObjectionRejected.d != null)
                {
                    setApplicationRefPickerModel(_VATObjectionRejected.d.RejectedFormSet.results);
                }
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (GAZTErrorException ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                });
            }
        }

        public async Task VATObjectionGenrateorRefreshSADAD(string fbnum, decimal Disamt, decimal Liaamt, string Abrzu, string Abrzo, decimal Secamt, string Security, string Persl, bool isFlagenable)
        {

            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });

                VATObjectionGenrateSadadModel _VATObjectionGenrateSadad = new VATObjectionGenrateSadadModel();
                _VATObjectionGenrateSadad = await VATObjectionWebServiceManager.GAZTGetVATObjectionGenrateorRefreshSADAD(fbnum, Disamt, Liaamt, Abrzu, Abrzo, Secamt, Security, Persl, isFlagenable);

                if (_VATObjectionGenrateSadad != null && _VATObjectionGenrateSadad.d != null)
                {




                    SADADNumber = _VATObjectionGenrateSadad.d.Sopbel;
                    SecurityNumber = _VATObjectionGenrateSadad.d.Security;

                    if (string.IsNullOrEmpty(SADADNumber))
                    {
                        ShowRefreshButton();

                    }
                    else
                    {
                        ShowSadadAmount();
                        MakeViewOnlyItems();

                        modelVATReview.d.Persl = selectedApplicationRef.Persl;

                        modelVATReview.d.Operationx = "05";

                        modelVATReview = await SubmitClicked();


                    }



                }
                else
                {

                    ShowRefreshButton();
                }

                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (GAZTErrorException ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
        }

        public async void VATObjectionSecurityAmount(Decimal disamt, Decimal liaamt, Decimal clramt)
        {

            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });

                VATObjectionSecurityAmountModel _VATObjectionSecurityAmount = new VATObjectionSecurityAmountModel();
                _VATObjectionSecurityAmount = await VATObjectionWebServiceManager.GAZTGetVATObjectionSecurityAmount(disamt, liaamt, clramt);

                if (_VATObjectionSecurityAmount != null && _VATObjectionSecurityAmount.d != null)
                {
                    SecurityAmount = _VATObjectionSecurityAmount.d.Secamt;
                    try
                    {
                        if (Double.Parse(SecurityAmount) == 0)
                        {
                            IsSecurityAmountMorethanZero = false;
                        }
                        else
                        {
                            IsSecurityAmountMorethanZero = true;
                        }

                        EnableSecurityPaymentsConButton();
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.ToString());
                        Console.Write(ex.StackTrace.ToString());
                    }

                }

                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (GAZTErrorException ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
        }

        public async void VATObjectionValidateTaxPayer(string idnum, string idtype, string passExpDt, string taxpDob)
        {

            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                idnum = null;
                idtype = null;
                passExpDt = null;
                taxpDob = null;
                VATObjectionValidateTaxpayerModel _VATObjectionValidateTaxpayer = new VATObjectionValidateTaxpayerModel();
                _VATObjectionValidateTaxpayer = await VATObjectionWebServiceManager.GAZTGetVATObjectionValidateTaxPayer(idnum, idtype, passExpDt, taxpDob);

                if (_VATObjectionValidateTaxpayer != null && _VATObjectionValidateTaxpayer.d != null)
                {

                }

                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (GAZTErrorException ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
        }

        public async Task FetchViewBill(string opbel, string vtre2)
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

                    VATObjectionViewbillModel billModelResult = new VATObjectionViewbillModel();
                    VATObjectionFormViewBillModel billFormModel = new VATObjectionFormViewBillModel();
                    try
                    {
                        billModelResult = await VATObjectionWebServiceManager.GAZTGetVATObjectionViewBill(opbel, vtre2);

                        if (billModelResult != null && billModelResult.d != null)
                        {
                            foreach (var item in billModelResult.d.results)
                            {
                                VBDocumentNumber = item.Opbel;
                                VBSadadNumber = item.Vtre2;
                                VBDescriptionOfPenality = item.Desc;
                                VBPeriodkey = item.Perslt;

                                VBAmount = item.Betrh;


                                string strFormatedDateofPenality = "";
                                if (item.Bldat != null)
                                {

                                    DateTime dateStart = new DateTime();
                                    CultureInfo cultureInfo = new CultureInfo("ar-SA");
                                    string apiDate = @"""" + item.Bldat + @"""";
                                    dateStart = (DateTime)item.Bldat;

                                    GregorianCalendar hjCalendar = new GregorianCalendar();
                                    int year = hjCalendar.GetYear(dateStart);
                                    int month = hjCalendar.GetMonth(dateStart);
                                    int day = hjCalendar.GetDayOfMonth(dateStart);

                                    string dateStr = string.Format("{0:00}/{1}/{2}", year, month, day);


                                    string dt1 = string.Empty;
                                    string[] dts = null;
                                    dts = dateStr.Split('/');
                                    //if (App.IsArabic)
                                    //{

                                    dt1 = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];

                                    //}
                                    //else
                                    //{

                                    //    dt1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];

                                    //}
                                    strFormatedDateofPenality = dt1;

                                }

                                string strFormatedDueDate = "";
                                if (item.Studt != null)
                                {

                                    DateTime dateStart = new DateTime();
                                    CultureInfo cultureInfo = new CultureInfo("ar-SA");
                                    string apiDate = @"""" + item.Studt + @"""";
                                    dateStart = (DateTime)item.Studt;

                                    GregorianCalendar hjCalendar = new GregorianCalendar();
                                    int year = hjCalendar.GetYear(dateStart);
                                    int month = hjCalendar.GetMonth(dateStart);
                                    int day = hjCalendar.GetDayOfMonth(dateStart);

                                    string dateStr = string.Format("{0:00}/{1}/{2}", year, month, day);


                                    string dt1 = string.Empty;
                                    string[] dts = null;
                                    dts = dateStr.Split('/');
                                    //if (App.IsArabic)
                                    //{

                                    dt1 = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];

                                    //}
                                    //else
                                    //{

                                    //    dt1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];

                                    //}
                                    strFormatedDueDate = dt1;

                                }

                                string strFormatedStartDate = "";
                                if (item.Abrzu != null)
                                {

                                    DateTime dateStart = new DateTime();
                                    CultureInfo cultureInfo = new CultureInfo("ar-SA");
                                    string apiDate = @"""" + item.Abrzu + @"""";
                                    dateStart = (DateTime)item.Abrzu;

                                    GregorianCalendar hjCalendar = new GregorianCalendar();
                                    int year = hjCalendar.GetYear(dateStart);
                                    int month = hjCalendar.GetMonth(dateStart);
                                    int day = hjCalendar.GetDayOfMonth(dateStart);

                                    string dateStr = string.Format("{0:00}/{1}/{2}", year, month, day);


                                    string dt1 = string.Empty;
                                    string[] dts = null;
                                    dts = dateStr.Split('/');
                                    //if (App.IsArabic)
                                    //{

                                    dt1 = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];

                                    //}
                                    //else
                                    //{

                                    //    dt1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];

                                    //}
                                    strFormatedStartDate = dt1;

                                }

                                string strFormatedEndDate = "";
                                if (item.Abrzo != null)
                                {

                                    DateTime dateStart = new DateTime();
                                    CultureInfo cultureInfo = new CultureInfo("ar-SA");
                                    string apiDate = @"""" + item.Abrzo + @"""";
                                    dateStart = (DateTime)item.Abrzo;

                                    GregorianCalendar hjCalendar = new GregorianCalendar();
                                    int year = hjCalendar.GetYear(dateStart);
                                    int month = hjCalendar.GetMonth(dateStart);
                                    int day = hjCalendar.GetDayOfMonth(dateStart);

                                    string dateStr = string.Format("{0:00}/{1}/{2}", year, month, day);


                                    string dt1 = string.Empty;
                                    string[] dts = null;
                                    dts = dateStr.Split('/');
                                    //if (App.IsArabic)
                                    //{

                                    dt1 = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];

                                    //}
                                    //else
                                    //{

                                    //    dt1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];

                                    //}
                                    strFormatedEndDate = dt1;

                                }

                                VBDateofPenality = strFormatedDateofPenality; //item.Bldat.ToString();
                                VBStartDate = strFormatedStartDate; //item.Abrzu.ToString();
                                VBEndDate = strFormatedEndDate; //item.Abrzo.ToString();
                                VBDueDate = strFormatedDueDate; //item.Studt.ToString();

                            }

                            await PopupNavigation.Instance.PushAsync(new VatReviewBillViewBottomPopUpPageView());

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


        #region MAP_VAT_RequestObject

        public VatObjectionsRequest BuildRequestObject()
        {
            VatObjectionsRequest _postData = new VatObjectionsRequest();

            _postData.__metadata = modelVATReview.d.__metadata;
            _postData.Actnm = modelVATReview.d.Actnm;
            _postData.Actno = modelVATReview.d.Actno;
            _postData.AddressSet = modelVATReview.d.AddressSet.results;
            _postData.AgreeFg = modelVATReview.d.AgreeFg;
            _postData.Appfg = modelVATReview.d.Appfg;
            _postData.AttdetSet = modelVATReview.d.AttdetSet.results;
            _postData.Branchx = modelVATReview.d.Branchx;
            _postData.CalTyp = modelVATReview.d.CalTyp;
            _postData.CrNo = modelVATReview.d.CrNo;
            _postData.DataVersion = modelVATReview.d.DataVersion;
            _postData.DateFrm = modelVATReview.d.DateFrm;
            _postData.DateFrmOld = modelVATReview.d.DateFrmOld;
            _postData.DateTo = modelVATReview.d.DateTo;
            _postData.DateToOld = modelVATReview.d.DateToOld;
            _postData.DecDt = modelVATReview.d.DecDt;
            _postData.DecFlg1 = modelVATReview.d.DecFlg1;
            _postData.OVERDUEFG = OverdueFlag;
            _postData.DecFlg2 = modelVATReview.d.DecFlg2;
            _postData.DecIdNo = modelVATReview.d.DecIdNo;
            _postData.Declarationdt = modelVATReview.d.DecDt;
            _postData.Decnm = modelVATReview.d.Decnm;
            _postData.Euserx = modelVATReview.d.Euserx;
            _postData.Evstatus = modelVATReview.d.Evstatus;
            _postData.Fbnumx = modelVATReview.d.Fbnumx;
            _postData.Fbstax = modelVATReview.d.Fbstax;
            _postData.Fbustx = modelVATReview.d.Fbustx;
            _postData.FormGuid = modelVATReview.d.FormGuid;
            _postData.Formprocx = modelVATReview.d.Formprocx;
            _postData.Forwardx = modelVATReview.d.Forwardx;
            _postData.FullName = modelVATReview.d.FullName;
            _postData.Golivefg = modelVATReview.d.Golivefg;
            _postData.Gpartx = App.LoginDataRetrieved.TIN;
            _postData.Iban = modelVATReview.d.Iban;
            _postData.IdDetailSet = modelVATReview.d.IdDetailSet.results;
            _postData.IdType = modelVATReview.d.IdType;
            _postData.Langx = modelVATReview.d.Langx;

            _postData.Officerx = modelVATReview.d.Officerx;
            _postData.PeriodKey = modelVATReview.d.PeriodKey;
            _postData.Persl = modelVATReview.d.Persl;
            _postData.PortalUsrx = modelVATReview.d.PortalUsrx;
            _postData.QuesListSet = modelVATReview.d.QuesListSet.results;
            //_postData.ReasonSet = modelVATReview.d.ReasonSet.results;
            _postData.RejFb = modelVATReview.d.RejFb;
            _postData.ReturnIdx = modelVATReview.d.ReturnIdx;
            _postData.RvRsn = modelVATReview.d.RvRsn;
            _postData.RvSubRsn = modelVATReview.d.RvSubRsn;
            _postData.SecurityDtl = modelVATReview.d.SecurityDtl;
            _postData.Skipst5covid19 = "";
            _postData.Srcidentifyx = modelVATReview.d.Srcidentifyx;
            _postData.Statusx = modelVATReview.d.Statusx;

            if (IsGeneratingFormbundle)
            {
                _postData.StepNumberx = "04";

            }
            else
            {


                if (CurrentIndex == 0)
                {

                    _postData.StepNumberx = "01";

                }
                else if (CurrentIndex == 2)
                {
                    _postData.StepNumberx = "03";

                }
                else if (CurrentIndex == 3)
                {
                    _postData.StepNumberx = "03";

                }
                else if (CurrentIndex == 4)
                {
                    _postData.StepNumberx = "05";

                }
                else if (CurrentIndex == 5)
                {
                    _postData.StepNumberx = "05";

                }
                else
                {
                    _postData.StepNumberx = "05";

                }



            }


            _postData.TxnTpx = modelVATReview.d.TxnTpx;
            _postData.UserTypx = "TP";


            NotesSetResults notes = new Models.VatReviewModel.NotesSetResults();
            if (ReportDetails != null)
            {

                notes.Tdline = ReportDetails.ToString();

            }
            else
            {
                notes.Tdline = "";

            }

            Metadata _metdata = new Metadata();

            _metdata.uri = Constants.VATObjectionsNotesSet;
            _metdata.type = "ZDP_VAT_NW_REV_SRV.Notes";
            _metdata.id = Constants.VATObjectionsNotesSet;
            notes.__metadata = _metdata;
            notes.AttByz = "TP";
            notes.ElemNo = 0;

            notes.Erfdtz = null;
            notes.Erftmz = null;
            notes.Erfusrz = "";
            notes.Lineno = 1;
            notes.Noteno = "1";
            notes.Notenoz = "1";
            notes.Rcodez = "RAVT_BOX";
            notes.Refnamez = "";
            notes.Tdformat = "";
            notes.XInvoicez = "";
            notes.XObsoletez = "";
            notes.DataVersionz = "00000";
            notes.ByGpartz = App.LoginDataRetrieved.TIN;

            NotesSetResults notes2 = new Models.VatReviewModel.NotesSetResults();
            if (OverdueFlag == "X")
            {
                /*--------adding noteset in late filing--------*/
                if (LateFlngDetails != null)
                {

                    notes2.Tdline = LateFlngDetails.ToString();

                }
                else
                {
                    notes2.Tdline = "";

                }

                Metadata _metdata2 = new Metadata();

                _metdata2.uri = Constants.VATObjectionsNotesSet;
                _metdata2.type = "ZDP_VAT_NW_REV_SRV.Notes";
                _metdata2.id = Constants.VATObjectionsNotesSet;
                notes2.__metadata = _metdata;
                notes2.AttByz = "TP";
                notes2.ElemNo = 0;

                notes2.Erfdtz = null;
                notes2.Erftmz = null;
                notes2.Erfusrz = "";
                notes2.Lineno = 2;
                notes2.Noteno = "2";
                notes2.Notenoz = "2";
                notes2.Rcodez = "RVT_OVRDUE";
                notes2.Refnamez = "";
                notes2.Tdformat = "";
                notes2.XInvoicez = "";
                notes2.XObsoletez = "";
                notes2.DataVersionz = "00000";
                notes2.ByGpartz = App.LoginDataRetrieved.TIN;

                /*-------end of note set  addition in latefiling---------*/
            }

            NotesSetResults notes1 = new Models.VatReviewModel.NotesSetResults();
            if (DisputeDetailsDesc != null)
            {

                notes1.Tdline = DisputeDetailsDesc.ToString();

            }
            else
            {
                notes1.Tdline = "";

            }

            Metadata _metdata1 = new Metadata();

            _metdata1.uri = Constants.VATObjectionsNotesSet;
            _metdata1.type = "ZDP_VAT_NW_REV_SRV.Notes";
            _metdata1.id = Constants.VATObjectionsNotesSet;
            notes1.__metadata = _metdata;
            notes1.AttByz = "TP";
            notes1.ElemNo = 0;

            notes1.Erfdtz = null;
            notes1.Erftmz = null;
            notes1.Erfusrz = "";
            notes1.Lineno = 3;
            notes1.Noteno = "3";
            notes1.Notenoz = "3";
            notes1.Rcodez = "RAVT_SDCAS";
            notes1.Refnamez = "";
            notes1.Tdformat = "";
            notes1.XInvoicez = "";
            notes1.XObsoletez = "";
            notes1.DataVersionz = "00000";
            notes1.ByGpartz = App.LoginDataRetrieved.TIN;

            var NotesetResult = new Models.VatReviewModel.NotesSet();
            var noteSetList = new List<NotesSetResults>();
            noteSetList.Add(notes);
            noteSetList.Add(notes1);
            if(OverdueFlag=="X")
            {
                noteSetList.Add(notes2);
            }

            NotesetResult.results = noteSetList;
            //modelVATReview.d.NotesSet = NotesetResult;


            _postData.NotesSet = NotesetResult.results;

            if (modelVATReview.d.Operationx == "04")
            {

                _postData.NotesSet.Clear();
            }


            if (IsRRAmountEdit)
            {

                _postData.SecurityDtl.Amttp = "P";

            }
            else
            {

                _postData.SecurityDtl.Amttp = "F";

            }



            _postData.MainReasonSet = modelVATReview.d.MainReasonSet.results;

            _postData.AttdetSet = modelVATReview.d.AttdetSet.results;
            _postData.ReasonSet = modelVATReview.d.ReasonSet.results;
            _postData.MainReasonSet.Clear();
            _postData.AttdetSet.Clear();
            _postData.ReasonSet.Clear();
            _postData.AddressSet.Clear();

            if (selectedReviewReason != null)
            {

                if (string.IsNullOrEmpty(selectedReviewReason.ProcCD))
                {

                    _postData.RvRsn = "";

                }
                else
                {
                    _postData.RvRsn = selectedReviewReason.ProcCD;



                }
            }
            else
            {
                _postData.RvRsn = "";
            }


            if (selectedSubReviewReason != null)
            {

                if (string.IsNullOrEmpty(selectedSubReviewReason.Code))
                {

                    _postData.RvSubRsn = "";


                }
                else
                {
                    _postData.RvSubRsn = selectedSubReviewReason.Code;


                }
            }
            else
            {
                _postData.RvSubRsn = "";
            }


            if (string.IsNullOrEmpty(ApplicationRefNumber))
            {

                _postData.RejFb = "";

            }
            else
            {

                _postData.RejFb = ApplicationRefNumber;
            }



            return _postData;


        }

        #endregion
        public async Task<VATObjectionSummaryModel> SubmitClicked()
        {
            VATObjectionSummaryModel response = new VATObjectionSummaryModel();
            VatObjectionsRequest request = new VatObjectionsRequest();

            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });


                //RequestDate;
                //TaxPeriodFrom;
                //TaxPeriodTo;
                //PickedDate;

                modelVATReview.d.AgreeFg = true;
                modelVATReview.d.Appfg = "N";
                modelVATReview.d.CalTyp = "1";
                modelVATReview.d.DecFlg1 = IsDECCheckBox;
                // modelVATReview.d.DecFlg2 = true;
                modelVATReview.d.Decnm = ContactPersonName;

                if (!string.IsNullOrEmpty(IDType))
                {

                    modelVATReview.d.IdType = IDTypeDictionary[IDType];
                    modelVATReview.d.DecIdNo = IDNumber;

                }
                else
                {
                    modelVATReview.d.IdType = "";
                    modelVATReview.d.DecIdNo = "";
                }

                if (selectedApplicationRef != null)
                {

                    modelVATReview.d.DateFrm = selectedApplicationRef.DateFrm;
                    modelVATReview.d.DateFrmOld = selectedApplicationRef.DateFrm;
                    modelVATReview.d.DecDt = RequestDate.ToString();
                    modelVATReview.d.OVERDUEFG = OverdueFlag;

                    var strDecDate = "";
                    if (!string.IsNullOrEmpty(RequestDate.ToString()))
                    {

                        if (!modelVATReview.d.DecDt.Contains("Date"))
                        {


                            DateTime dt = Convert.ToDateTime(RequestDate.ToString());
                            JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                            {
                                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                            };
                            //var jsonDateTime = JsonConvert.SerializeObject(dt, microsoftDateFormatSettings);
                            var jsonDateTime = JsonConvert.SerializeObject(dt.Date, microsoftDateFormatSettings);
                            string[] dateList = jsonDateTime.Split('+');
                            jsonDateTime = dateList[0].Replace("\"\\", "");
                            jsonDateTime = jsonDateTime + ")/";
                            modelVATReview.d.DecDt = jsonDateTime;
                            strDecDate = jsonDateTime;


                        }
                    }



                    if (IsSecurityPaymentsTabVisible)
                    {

                        if (IsSadadSecuritySelected)
                        {

                            modelVATReview.d.SecurityDtl.Sectp = "C";
                            modelVATReview.d.SecurityDtl.Sopbel = SADADNumber;
                            modelVATReview.d.SecurityDtl.ChkBank = "";
                            modelVATReview.d.SecurityDtl.ChkCash = IsSadadCheckBox3 ? "X" : "";

                        }
                        else
                        {

                            modelVATReview.d.SecurityDtl.Sectp = "B";
                            modelVATReview.d.SecurityDtl.ChkCash = "";
                            modelVATReview.d.SecurityDtl.ChkBank = IsSadadCheckBox1 ? "X" : "";


                        }

                        if (!string.IsNullOrEmpty(strDecDate))
                        {

                            modelVATReview.d.SecurityDtl.Abrzo = strDecDate;
                            modelVATReview.d.SecurityDtl.Abrzu = strDecDate;

                        }

                        modelVATReview.d.SecurityDtl.DataVersion = "00001";
                        modelVATReview.d.SecurityDtl.Disamt = (Double.Parse(RequestedReviewAmount)) + "";
                        modelVATReview.d.SecurityDtl.Liaamt = selectedApplicationRef.Liaamt;
                        modelVATReview.d.SecurityDtl.Opbel = selectedApplicationRef.Opbel;
                        modelVATReview.d.SecurityDtl.Penamount = selectedApplicationRef.Penamount;
                        modelVATReview.d.SecurityDtl.Perslt = selectedApplicationRef.Perslt;
                        modelVATReview.d.SecurityDtl.Secamt = SecurityAmount;
                        modelVATReview.d.SecurityDtl.Clramt = selectedApplicationRef.Clramt;
                        modelVATReview.d.SecurityDtl.Security = SecurityNumber;





                    }
                }


                request = BuildRequestObject();

                request.Operationx = modelVATReview.d.Operationx;

                //if (IsGeneratingFormbundle || IsDraftClicked)
                //{

                //    request.Operationx = "05";
                //}
                //else
                //{

                //    request.Operationx = "01";
                //}






                response = await VATObjectionWebServiceManager.SaveVatReviewObjection(request);


                if (response != null)
                {
                    try
                    {
                        if (response != null && response.d != null)
                        {
                        }
                        IsLoading = false;
                        return response;

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.ToString());
                        Console.Write(ex.StackTrace.ToString());
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

                    if (CurrentIndex == 6)
                    {

                        _navigationService.GoBack();

                    }


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


        public static DateTime ConvertJsonToDateTime(string jsonDate)
        {
            // JavaScript uses the unix epoch of 1/1/1970. Note, it's important to call ToLocalTime()
            // after doing the time conversion, otherwise we'd have to deal with daylight savings hooey.
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            Double milliseconds = Convert.ToDouble(jsonDate);
            DateTime dateTime = unixEpoch.AddMilliseconds(milliseconds).ToLocalTime();

            return dateTime;
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

        public async Task GetViewApplication(string Fbguid, string Fbnumz, string EUser)
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

                    VATDeclaration _vATDeclaration = await WebServiceManager.GAZTGetVRVATReturns(Fbguid, Fbnumz, EUser, "");

                    if (_vATDeclaration != null && _vATDeclaration.d != null)
                    {
                        VATinNumber = _vATDeclaration.d.Gpart;
                        VAAccNumber = _vATDeclaration.d.Fin;
                        VAIdnumber = _vATDeclaration.d.Idnumber;
                        VATaxpayerName = _vATDeclaration.d.Tpnm;
                        if (_vATDeclaration.d.ADRSet.results.Count > 0)
                        {
                            VAAddress = _vATDeclaration.d.ADRSet.results[0].BuildingNo + "," +
                                        _vATDeclaration.d.ADRSet.results[0].Street + "," +
                                        _vATDeclaration.d.ADRSet.results[0].Addrnumber + "," +
                                        _vATDeclaration.d.ADRSet.results[0].RegionDesc + "," +
                                        _vATDeclaration.d.ADRSet.results[0].City + "," +
                                        _vATDeclaration.d.ADRSet.results[0].PostalCd;
                        }
                        else
                        {
                            VAAddress = "";
                        }

                        VAVATReturnType = _vATDeclaration.d.Incotext;
                        VAVatReturnReferenceNo = _vATDeclaration.d.Fbnum;
                        VATaxPeriod = _vATDeclaration.d.Perslt;
                        VAVatAccountNum = _vATDeclaration.d.Fin;
                        VASalesAmount = _vATDeclaration.d.TotalsalesAmt;
                        VASalesVatAdjustment = _vATDeclaration.d.TotalsalesAdj;
                        VAVatOnSales = _vATDeclaration.d.TotalsalesVat;
                        VAPurchaseAmount = _vATDeclaration.d.TotalpurchaseAmt;
                        VAVatOnPurchase = _vATDeclaration.d.TotalpurchaseVat;
                        VAPurchaseVatAdjustment = _vATDeclaration.d.TotalpurchaseAdj;
                        VATotalDueVAT = _vATDeclaration.d.TotaldueVat;
                        VATotalCreditVAT = _vATDeclaration.d.CreditVat;
                        VACorrrections = _vATDeclaration.d.Preperiodcorr;
                        VANetVat = _vATDeclaration.d.NetdueVat;

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            _navigationService.NavigateTo(App.VatReviewViewApplicationPageView);

                        });
                    }
                    else
                    {
                        await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            IsLoading = false;
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

        public async Task VatRegistrationData()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    VrVRVATRegistrationDetailsData = null;

                    VATRegistrationDetails vATRegistration = null;

                    try
                    {
                        vATRegistration = await VatRegistrationWebServiceManager.GAZTGetVATRegistrationDisplayDetailsData();


                        if (vATRegistration != null && vATRegistration.d != null)
                        {

                            //step 4 and 5 data set
                            if (vATRegistration.d.CONTACT_PERSONSet != null)
                            {
                                VrVRGpartFR = vATRegistration.d.CONTACT_PERSONSet.results[0].Gpart;
                                //  VATRegistrationDetailsData.d.CONTACT_PERSONSet.results[0].Type = SelectedIdTypeFR.ID;
                                vrVRIdnumber = string.Empty;
                                vrVRIdnumber = vATRegistration.d.CONTACT_PERSONSet.results[0].Idnumber;
                                VrVRFirstnmFR = vATRegistration.d.CONTACT_PERSONSet.results[0].Firstnm;
                                VrVRLastnmFR = vATRegistration.d.CONTACT_PERSONSet.results[0].Lastnm;
                                VrVRMobNumberFR = vATRegistration.d.CONTACTDTSet.results[0].MobNumber;
                                VrVRSmtpAddrFR = vATRegistration.d.CONTACTDTSet.results[0].SmtpAddr;
                                VrVRDOB = vATRegistration.d.CONTACT_PERSONSet.results[0].Dobdt;

                            }

                            VrVRIDType = vATRegistration.d.DecidTy;
                            VrVRIDNumber = vATRegistration.d.DecidNo;
                            VrVRContactPersonName = vATRegistration.d.Decname;
                        }
                        if (vATRegistration.d.QUESTIONSSet != null)
                        {
                            List<ResultsItemForQuestion> quest1AnsList = vATRegistration.d.QUESTIONSSet.results.Where(s => s.QueNo == "001" && s.QoptAns == "1").ToList();
                            VrVRquesTion1answerSelected = quest1AnsList.FirstOrDefault().QoptTxt; // vATRegistration.d.QUESTIONSSet.results.Where(s => s.QueNo == "003" && s.QoptAns == "1").ToList;

                            List<ResultsItemForQuestion> quest2AnsList = vATRegistration.d.QUESTIONSSet.results.Where(s => s.QueNo == "002" && s.QoptAns == "1").ToList();
                            VrVRquesTion2answerSelected = quest2AnsList.FirstOrDefault().QoptTxt; // vATRegistration.d.QUESTIONSSet.results.Where(s => s.QueNo == "003" && s.QoptAns == "1").ToList;

                            List<ResultsItemForQuestion> quest3AnsList = vATRegistration.d.QUESTIONSSet.results.Where(s => s.QueNo == "003" && s.QoptAns == "1").ToList();
                            VrVRquesTion3answerSelected = quest3AnsList.FirstOrDefault().QoptTxt; // vATRegistration.d.QUESTIONSSet.results.Where(s => s.QueNo == "003" && s.QoptAns == "1").ToList;

                            List<ResultsItemForQuestion> quest4AnsList = vATRegistration.d.QUESTIONSSet.results.Where(s => s.QueNo == "004" && s.QoptAns == "1").ToList();

                            VrVRquesTion4answerSelected = quest3AnsList.FirstOrDefault().QoptTxt;//vATRegistration.d.QUESTIONSSet.results.Where(s => s.QueNo == "004" && s.QoptAns == "1").ToString();
                        }
                        if (vATRegistration.d.VatTaxDt != null)
                        {
                            string convertedDate = JsonConvert.DeserializeObject<DateTime>(@"""" + vATRegistration.d.VatTaxDt + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                            VrVRVatEligibleStartDate = Convert.ToDateTime(convertedDate).ToString("dd/MM/yyyy", new CultureInfo("en-US"));
                        }
                        if (vATRegistration.d.QUESCONFIG_MSet.results.Count != 0)
                        {
                            VrVRMinMaxRanges = new List<QuestionNumberWithMinMaxRange>();
                            VrVRMinMaxRanges = UtilityManager.GetLowAndHighRangeForEachQuestionSet(vATRegistration.d.QUESCONFIG_MSet);
                        }
                        VrVRIbanList = new ObservableCollection<Result2>();
                        if (vATRegistration.d.ATTDETSet != null)
                        {
                            foreach (Attachment ItemA in vATRegistration.d.ATTDETSet.results)
                            {
                                VrVRAttachmentName = ItemA.Filename;
                            }
                        }

                        if (vATRegistration.d.IBANSet != null)
                        {
                            VrVRIbanList = new ObservableCollection<Result2>(vATRegistration.d.IBANSet.results);
                            for (int i = 0; i < VrVRIbanList.Count; i++)
                            {
                                if (VrVRIbanList.ElementAt(i).Iban != string.Empty)
                                {
                                    VrVRIban = VrVRIbanList.FirstOrDefault().Iban;

                                }
                            }

                        }
                        if (vATRegistration.d.ExFg == "1")
                        {
                            VrVRImportExportText = "Exporter";
                        }
                        else if (vATRegistration.d.ImFg == "1")
                        {
                            VrVRImportExportText = "Importer";

                        }

                        VrVRIdnumberFR = vrVRIdnumber;

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            _navigationService.NavigateTo(App.VRVatRegViewPageView);

                        });
                    }
                    catch (GAZTVATRegistrationInProcessException ex)
                    {
                        throw ex;
                    }
                    catch (InternetException)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            IsLoading = false;
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
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = false;
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
                Device.BeginInvokeOnMainThread(() =>
                {
                });
            }
        }

        public async Task VatDeregistration()
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

                    VATDeRegistrationDetails vATDeRegistration = null;

                    try
                    {
                        vATDeRegistration = await VatRegistrationWebServiceManager.GAZTGetVATDeRegistrationData();


                        if (vATDeRegistration != null && vATDeRegistration.d != null)
                        {

                            if (vATDeRegistration.d.Idnumbr != null)

                                if (string.IsNullOrEmpty(IDType))
                                {

                                    PopulateVatDeRegSummaryDeclarationData(vATDeRegistration.d.Type, vATDeRegistration.d.Idnumbr, vATDeRegistration.d.Declaredt, vATDeRegistration.d.Contactnm);
                                }

                            PopulateVatDeRegAttachments(vATDeRegistration.d.AttdetSet.results);

                            PopulateVatDeRegSummaryReasonData(vATDeRegistration.d.Type, vATDeRegistration.d.Reason);

                            Device.BeginInvokeOnMainThread(() =>
                            {
                                _navigationService.NavigateTo(App.VRVatDeRegViewAppPageView);

                            });

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
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

            }
        }

        public async Task GetVATReviewRequestTPFV(string strOfficerz, string strGpartz, string strEuser, string strFbguid, string strReviewFg = "true")
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
                    VATReviewRequestTPFVModel _VATReviewRequestTPFV = new VATReviewRequestTPFVModel();
                    VATReviewRequestTPFVReturnModel _VATReviewRequestTPFVReturn = new VATReviewRequestTPFVReturnModel();
                    try
                    {
                        var vatReviewFBguid = await VATObjectionWebServiceManager.GAZTVATObjectionSummaryInputData(selectedApplicationRef.Fbnum, "", "TPFV");

                        if (vatReviewFBguid != null && vatReviewFBguid.d != null)
                        {


                            _VATReviewRequestTPFV = await GetVATReviewWebServiceManager.GAZTGetVATReviewRequestTPFV(strOfficerz, strGpartz, strEuser, vatReviewFBguid.d.Fbguid, strReviewFg);

                            if (_VATReviewRequestTPFV != null && _VATReviewRequestTPFV.d != null)
                            {
                                _VATReviewRequestTPFVReturn.AgreeFlag = _VATReviewRequestTPFV.d.Inschk;
                                VRTIDEffectiveDateFrom = _VATReviewRequestTPFV.d.Edtfr;
                                VRTIDEffectiveDateTo = _VATReviewRequestTPFV.d.Edtto;
                                CIPFTxablePurchases = _VATReviewRequestTPFV.d.Cptp;
                                CIPFExemptPurchases = _VATReviewRequestTPFV.d.Cpep;
                                CITxablePurchases = _VATReviewRequestTPFV.d.Ctpp;
                                CIExemptPurchases = _VATReviewRequestTPFV.d.Cepp;
                                PIPFTxablePurchases = _VATReviewRequestTPFV.d.Pcptp;
                                PIPFExemptPurchases = _VATReviewRequestTPFV.d.Pcpep;
                                PITxablePurchases = _VATReviewRequestTPFV.d.Pctpp;
                                PIExemptPurchases = _VATReviewRequestTPFV.d.Pcepp;
                                if (_VATReviewRequestTPFV.d.NotesSet.results.Where(x => x.AttByz.ToUpper() == "TP").Count() > 0)
                                {
                                    VITDReportDetails =
                                        _VATReviewRequestTPFV.d.NotesSet.results.First(x => x.AttByz.ToUpper() == "TP").Strline;
                                }
                                var attachmentList = new ObservableCollection<Attachment>();
                                foreach (var attachment in _VATReviewRequestTPFV.d.AttdetSet.results)
                                {
                                    attachmentList.Add(attachment);
                                }
                                VITDAttachmentsListViewData = attachmentList;
                                VITDIDType = IDToNameDictionary[_VATReviewRequestTPFV.d.Idtp];
                                VITDIDNumber = _VATReviewRequestTPFV.d.Idno;
                                VITDContactPersonName = _VATReviewRequestTPFV.d.Cnpr;

                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    _navigationService.NavigateTo(App.VRInputTDViewAppPageViewApp);
                                });
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

        public async Task GetVATDREGViewApplication()
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
                    VATDREGViewApllicationViewModel _vATDREGViewApllicationViewModel = new VATDREGViewApllicationViewModel();

                    try
                    {
                        //Please pass the fbtype is "DGVT"
                        var _dregInputResult = await VATObjectionWebServiceManager.GAZTVATObjectionSummaryInputData(selectedApplicationRef.Fbnum, "", "DGVT");

                        if (_dregInputResult != null && _dregInputResult.d != null)
                        {

                            var dregresult = await GetVATReviewWebServiceManager.GAZTGetVATReviewDREGViewApplication(_dregInputResult.d.Fbguid);
                            var dregReasonset = await GetVATReviewWebServiceManager.GAZTGetVATReviewDREGReasonSet("VT_DREG");

                            if (dregresult != null && dregresult.d != null)
                            {

                                _vATDREGViewApllicationViewModel.ReasonforDeRegistration = dregReasonset.d.results.
                                Where(x => x.Reason == dregresult.d.Reason).FirstOrDefault().Rdesc;

                                _vATDREGViewApllicationViewModel.ContactPersonName = dregresult.d.Contactnm;
                                _vATDREGViewApllicationViewModel.DeclarationId = dregresult.d.Idnumbr;

                                var declarationDate = "";

                                if (dregresult.d.Declaredt != null)
                                {

                                    declarationDate = dregresult.d.Declaredt?.ToString("dd MM yyyy");
                                }


                                PopulateVatDeRegSummaryDeclarationData(IDToNameDictionary[dregresult.d.Type], dregresult.d.Idnumbr, declarationDate, dregresult.d.Contactnm);
                                PopulateVatDeRegAttachments(dregresult.d.AttdetSet.results);
                                PopulateVatDeRegSummaryReasonData(dregresult.d.Reqtp, _vATDREGViewApllicationViewModel.ReasonforDeRegistration);

                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    _navigationService.NavigateTo(App.VRVatDeRegViewAppPageView);

                                });
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

        public async Task GetVATReviewRequestVTGR()
        {
            try
            {
                string strTxnTpz = "CRE_VTGR";
                string strGpart = App.LoginDataRetrieved.TIN;
                string strEuser = "";
                string strFbguid = "";
                string strFBNum = selectedApplicationRef.Fbnum;
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    IsLoading = true;
                    VATReviewRequestVTGRModel _VATReviewRequestVTGR = new VATReviewRequestVTGRModel();
                    VATReviewRequestVTGRReturnModel _VATReviewRequestVTGRReturn = new VATReviewRequestVTGRReturnModel();
                    try
                    {
                        _VATReviewRequestVTGR = await GetVATReviewWebServiceManager.GAZTGetVATReviewRequestVTGR(strEuser, strFbguid, strGpart, strTxnTpz, strFBNum);
                        if (_VATReviewRequestVTGR != null && _VATReviewRequestVTGR.d != null)
                        {

                            try
                            {
                                VRVGEffectivedate = _VATReviewRequestVTGR.d.EFFDATESet.results.Where(x => x.Persl == _VATReviewRequestVTGR.d.Persl).FirstOrDefault().Txt50;
                            }
                            catch (Exception e)
                            {

                            }


                            if (_VATReviewRequestVTGR.d.DecidTy != null)
                            {

                                VRVGIDType = IDToNameDictionary[_VATReviewRequestVTGR.d.DecidTy];
                            }

                            if (_VATReviewRequestVTGR.d.DecidNo != null)
                            {

                                VRVGIDNumber = _VATReviewRequestVTGR.d.DecidNo;

                            }
                            if (_VATReviewRequestVTGR.d.Decname != null)
                            {

                                VRVGContactPersonName = _VATReviewRequestVTGR.d.Decname;

                            }

                            if (_VATReviewRequestVTGR.d.AggreSupply != null)
                            {

                                VRVGVATeligiblesupplies = VGSupplicesDictionary[_VATReviewRequestVTGR.d.AggreSupply];

                            }

                            if (_VATReviewRequestVTGR.d.AggrePurchase != null)
                            {

                                VRVGVATeligiblepurchases = VGPurchasesDictionary[_VATReviewRequestVTGR.d.AggrePurchase];

                            }



                            if (_VATReviewRequestVTGR.d.TABLESet.results != null)
                            {

                                var tinsListViewData = new ObservableCollection<VATReviewRequestVTGRModel.TABLESetResult>();
                                foreach (VATReviewRequestVTGRModel.TABLESetResult tin in _VATReviewRequestVTGR.d.TABLESet.results)
                                {

                                    tinsListViewData.Add(tin);

                                }
                                VRVGTinsListViewData = tinsListViewData;
                            }

                            if (_VATReviewRequestVTGR.d.ATTDETSet.results != null)
                            {

                                var attachmentsListViewData = new ObservableCollection<Attachment>();
                                foreach (Attachment attachemnt in _VATReviewRequestVTGR.d.ATTDETSet.results)
                                {

                                    attachmentsListViewData.Add(attachemnt);

                                }
                                VRVGAttachmentsListViewData = attachmentsListViewData;
                            }





                            Device.BeginInvokeOnMainThread(() =>
                            {
                                _navigationService.NavigateTo(App.VRVatGroupPageView);
                            });
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

        public async Task GetVATDREGSuspensionViewApplication()
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
                    VATDREGViewApllicationViewModel _vATDREGViewApllicationViewModel = new VATDREGViewApllicationViewModel();

                    try
                    {
                        //Please pass the fbtype is "DGVT"
                        var _dregInputResult = await VATObjectionWebServiceManager.GAZTVATObjectionSummaryInputData(selectedApplicationRef.Fbnum, "", "DGVT");

                        if (_dregInputResult != null && _dregInputResult.d != null)
                        {

                            var dregresult = await GetVATReviewWebServiceManager.GAZTGetVATReviewDREGViewApplication(_dregInputResult.d.Fbguid);
                            var dregReasonset = await GetVATReviewWebServiceManager.GAZTGetVATReviewDREGReasonSet("VT_SUSP");

                            if (dregresult != null && dregresult.d != null)
                            {

                                VRVSRequestType = AppResources.VRVSVATReturnFilingObligationSuspension;


                                VRVSRFSuspensionofFiling = dregReasonset.d.results.
                                Where(x => x.Reason == dregresult.d.Reason).FirstOrDefault().Rdesc;

                                VRVSContactPersonName = dregresult.d.Contactnm;
                                VRVSIDNumber = dregresult.d.Idnumbr;
                                VRVSIDType = IDToNameDictionary[dregresult.d.Type];

                                var settings = new JsonSerializerSettings
                                {
                                    DateFormatString = "yyyy-MM-ddTH:mm:ss",
                                    DateTimeZoneHandling = DateTimeZoneHandling.Utc
                                };

                                var startDate = "";
                                var endDate = "";

                                try
                                {

                                    if (dregresult.d.StartDate != null)
                                    {

                                        var jsonstartDate = JsonConvert.SerializeObject(dregresult.d.StartDate, settings);
                                        startDate = Regex.Replace(jsonstartDate, "[@,\\.\";'\\\\]", string.Empty);
                                    }

                                    if (dregresult.d.EndDate != null)
                                    {

                                        var jsonEndDate = JsonConvert.SerializeObject(dregresult.d.EndDate, settings);
                                        endDate = Regex.Replace(jsonEndDate, "[@,\\.\";'\\\\]", string.Empty);
                                    }






                                }
                                catch
                                {

                                }

                                if (dregresult.d.StartDate != null)
                                {


                                    var _suspensionResult = await GetVATReviewWebServiceManager.GAZTGetVATReviewDREGSuspensionDetailSet(startDate, endDate);
                                    if (_suspensionResult != null & _suspensionResult.d.results.Count > 0)
                                    {
                                        if (_suspensionResult.d.results[0].StartDate != null)
                                        {

                                            VRVSStartofSuspensionPeriod = _suspensionResult.d.results[0].StartDate?.ToString("dd-MM-yyyy");

                                        }

                                        if (_suspensionResult.d.results[0].EndDate != null)
                                        {

                                            VRVSEndofSuspensionPeriod = _suspensionResult.d.results[0].EndDate?.ToString("dd-MM-yyyy");

                                        }

                                        if (_suspensionResult.d.results[0].Duedate != null)
                                        {

                                            VRVSNextfilingduedate = _suspensionResult.d.results[0].Duedate?.ToString("dd-MM-yyyy");

                                        }

                                        if (_suspensionResult.d.results[0].SuspDtfrom != null)
                                        {

                                            VRVSRFSuspensionofFiling = _suspensionResult.d.results[0].SuspDtfrom?.ToString("dd-MM-yyyy") + " - " + _suspensionResult.d.results[0].SuspDtto?.ToString("dd-MM-yyyy");

                                        }

                                        if (_suspensionResult.d.results[0].NextDtfrom != null)
                                        {

                                            VRVSNextfilingperiod = _suspensionResult.d.results[0].NextDtfrom?.ToString("dd-MM-yyyy") + " - " + _suspensionResult.d.results[0].NextDtto?.ToString("dd-MM-yyyy");

                                        }

                                    }


                                }
                                var attachmentList = new ObservableCollection<Attachment>();
                                foreach (var attachment in dregresult.d.AttdetSet.results)
                                {
                                    attachmentList.Add(attachment);
                                }
                                VSVRAttachmentsListViewData = attachmentList;

                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    _navigationService.NavigateTo(App.VRSuspensionViewAppPageView);

                                });
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


        #endregion
    }
}