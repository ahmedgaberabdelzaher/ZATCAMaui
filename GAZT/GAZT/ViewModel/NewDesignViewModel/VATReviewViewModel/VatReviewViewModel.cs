using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.Models.VatReviewModel;
using EGAZT.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel;
using EGAZT.Views.NewDesign;
using EGAZT.Views.NewDesign.GenericPickers;
using EGAZT.Views.NewDesign.VatReview;
using EGAZT.Views.NewDesign.ZakatInstalmentPlan;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using static EGAZT.Models.VatReviewModel.VATObjectionSummaryInputModel;
using Metadata = EGAZT.Models.VatReviewModel.Metadata;

namespace EGAZT.ViewModel.NewDesignViewModel.VatReviewViewModel
{
    public class VatReviewViewModel : BaseViewModel
    {
        #region Enums

        enum PagesEnum
        {
            ReviewReason,
            ReviewDetails,
            ReportDetails,
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
        public ICommand ReviewReasonCommand { get; set; }
        public ICommand SubReviewReasonCommand { get; set; }
        public ICommand ShowDatePicker { get; set; }
        public ICommand IdTypeSpinnerTapped { get; set; }
        public ICommand ApplicationNumRefCommand { get; set; }
        public ICommand SadadGenerateBtnTapped { get; set; }
        public ICommand GoBackToReportDetails { get; set; }





        #endregion

        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        int selectedPage = (int)PagesEnum.ReviewReason;

        private bool _isBackVisible = false;

        public bool IsBackVisible
        {
            get { return _isBackVisible; }
            set
            {
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
                _securityPaymentVisible = value;
                RaisePropertyChanged("SecurityPaymentVisible");
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

        private bool _isSadadSecuritySelected = false;

        public bool IsSadadSecuritySelected
        {
            get { return _isSadadSecuritySelected; }
            set
            {
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
                _pickedDate = value;
                RaisePropertyChanged("PickedDate");
            }
        }

        private GenericDatePickerModel genericDatePickerModel;

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

        public string _reviewReason = "";

        public string ReviewReason
        {
            get { return _reviewReason; }
            set
            {
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
                _requestDate = value;
                RaisePropertyChanged("RequestDate");
            }
        }

        public string _taxPeriodOfCase = "";

        public string TaxPeriodOfCase
        {
            get { return _taxPeriodOfCase; }
            set
            {
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
                _reportDetails = value;
                RaisePropertyChanged("ReportDetails");
            }
        }

        public string _sADADNumber = "";

        public string SADADNumber
        {
            get { return _sADADNumber; }
            set
            {
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
                isSadadCheckBox1 = value;
                RaisePropertyChanged("IsSadadCheckBox1");
            }
        }

        private bool isSadadCheckBox2 = false;

        public bool IsSadadCheckBox2
        {
            get { return isSadadCheckBox2; }
            set
            {
                isSadadCheckBox2 = value;
                RaisePropertyChanged("IsSadadCheckBox2");
            }
        }

        private bool isSadadCheckBox3 = false;

        public bool IsSadadCheckBox3
        {
            get { return isSadadCheckBox3; }
            set
            {
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
                isSadadCheckBox4 = value;
                RaisePropertyChanged("IsSadadCheckBox4");
            }
        }

        private bool isGeneratingFormbundle = false;

        public bool IsGeneratingFormbundle
        {
            get { return isGeneratingFormbundle; }
            set
            {
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
                isReportDetailsEditable = value;
                RaisePropertyChanged("IsReportDetailsEditable");
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
                _isReportDetailsEnabled = value;
                ReportDetailsButtonBackGroundColor = Color.FromHex(_isReportDetailsEnabled ? "#d49504" : "#9EA4A9");
                RaisePropertyChanged("IsReportDetailsEnabled");
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

        private bool _isSecurityPaymentEnabled = false;

        public bool IsSecurityPaymentEnabled
        {
            get { return _isSecurityPaymentEnabled; }
            set
            {
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
                _vBSadadNumber = value;
                RaisePropertyChanged("VBSadadNumber");
            }
        }
        public DateTime? _vBDateofPenality = null;
        public DateTime? VBDateofPenality
        {
            get { return _vBDateofPenality; }
            set
            {
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
                _vBPeriodkey = value;
                RaisePropertyChanged("VBPeriodkey");
            }
        }
        public DateTime? _vBStartDate = null;
        public DateTime? VBStartDate
        {
            get { return _vBStartDate; }
            set
            {
                _vBStartDate = value;
                RaisePropertyChanged("VBStartDate");
            }
        }
        public DateTime? _vBEndDate = null;
        public DateTime? VBEndDate
        {
            get { return _vBEndDate; }
            set
            {
                _vBEndDate = value;
                RaisePropertyChanged("VBEndDate");
            }
        }
        public DateTime? _vBDueDate = null;
        public DateTime? VBDueDate
        {
            get { return _vBDueDate; }
            set
            {
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
                _isAssessPayOptionVisible = value;
                RaisePropertyChanged("IsAssessPayOptionVisible");
            }
        }
        private bool _isRRAmountEdit = false;
        public bool IsRRAmountEdit
        {
            get { return _isRRAmountEdit; }
            set
            {
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
                partialAmountCheckBox = value;
                RaisePropertyChanged("PartialAmountCheckBox");
            }
        }

        private bool fullPaymentCheckBox = false;
        public bool FullPaymentCheckBox
        {
            get { return fullPaymentCheckBox; }
            set
            {
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
        public int MaxIndex { get; private set; } = 6;

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
                _VrVGContactPersonName = value;
                RaisePropertyChanged("VRVGContactPersonName");
            }
        }

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
                _VrVRContactPersonName = value;
                RaisePropertyChanged("VrVRContactPersonName");
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
                _VrVRimportExportText = value;
                RaisePropertyChanged("VrVRImportExportText");
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
                _VrVRibanList = value;
                RaisePropertyChanged("VrVRIbanList");
            }
        }

        private Dictionary<string, string> IDTypeDictionary = null;
        private Dictionary<string, string> VGSupplicesDictionary = null;
        private Dictionary<string, string> VGPurchasesDictionary = null;


        private bool _isDialog = true;
        private bool _isBankGuranteeAttachments = false;
        private VATObjectionFormModel.ReviewReason selectedReviewReason;
        private VATObjectionFormModel.SubReason selectedSubReviewReason;
        private VATObjectionRejectedFormModel.AppRefNumResult selectedApplicationRef;
        private List<VATObjectionFormModel.ReviewReason> reviewReasonList;
        private List<VATObjectionFormModel.SubReason> subReviewReasonList;
        private List<VATObjectionRejectedFormModel.AppRefNumResult> appRefNumList;
        private VATObjectionSummaryModel modelVATReview;
        private VATObjectionRejectedFormModel _VATObjectionRejected;

        public VatReviewInterface vRInterface { get; set; }

        public VatReviewViewModel(INavigationService navigationService, IDialogService dialogService) : base(
            navigationService, dialogService)
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

            CloseClick = new Command(async () => { _navigationService.GoBack(); });

            GoBackClick = new Command(async () => { BackNavigations(); });

            ReviewReasonConBtnTapped = new Command(async () => { ReviewReasonConBtnClicked(); });

            ReviewDetailsConBtnTapped = new Command(async () => { ReviewDetailsConBtnClicked(); });
            ReportDetailsConBtnTapped = new Command(async () => { ReportDetailsConBtnClicked(); });
            SecurityPaymentConBtnTapped = new Command(async () => { SecurityPaymentConBtnClicked(); });

            DeclarationConBtnTapped = new Command(async () => { DeclarationConBtnClicked(); });

            SummaryConBtnTapped = new Command(async () => { SummaryConBtnClicked(); });

            GoBackToReviewReason = new Command(async () => { EnableReviewReasonView(); });
            GoBackToReviewDetails = new Command(async () => { EnableReviewDetailsView(); });
            GoBackToDeclaration = new Command(async () => { EnableDeclarationView(); });
            ViewApplicationTapped = new Command(async () => { ViewApplicationClicked(); });
            NewAttachmentTapped = new Command(async () => { NewAttachmentClicked(); });
            NewBankGuranteeAttachmentTapped = new Command(NewBankGuranteeAttachmentClicked);
            ReviewReasonCommand = new Command(async () => { showReviewReasonPickerDialog(); });
            SubReviewReasonCommand = new Command(async () => { showSubReviewReasonPickerDialog(); });
            ApplicationNumRefCommand = new Command(async () => { showAppRefNumberPickerDialog(); });
            SadadGenerateBtnTapped = new Command(async () => { ShowSADADConfirmation(); });
            ShowDatePicker = new Command(async () => { showDatePickerDialog(); });
            IdTypeSpinnerTapped = new Command(async () => { showIdTypePickerDialog(); });
            GoBackToReportDetails = new Command(async () => { EnableReportDetailsView(); });


            //AddSecurityPaymentOptions();


            genericDatePickerModel = new GenericDatePickerModel();
            genericDatePickerModel.DatePickerTitle = AppResources.VRDateOfBirth;
            genericDatePickerModel.PickerId = "DatePicker";

            //setIdPickerModel();
        }

        public async void NewAttachmentClicked()
        {
            if (AttachmentsListViewData == null)
            {
                AttachmentsListViewData = new ObservableCollection<Attachment>();
            }

            try
            {

                _isBankGuranteeAttachments = false;

                if (AttachmentsListViewData.Count == 0)
                {
                    if (string.IsNullOrEmpty(SADADNumber))
                    {
                        await PopupNavigation.Instance.PushAsync(new FilesUploadPopUpPageView(
                            AttachmentsListViewData.ToList(),
                            Models.ZakatInstalationModels.WhichAttachment.VatReviewAttachments,
                            modelVATReview.d.ReturnIdx));
                    }
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
            if (BankGuranteeAttachmentsListViewData == null)
            {
                BankGuranteeAttachmentsListViewData = new ObservableCollection<Attachment>();
            }

            try
            {
                _isBankGuranteeAttachments = true;


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

        private void setIdPickerModel()
        {
            ObservableCollection<string> iDTypes = new ObservableCollection<string>();
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
            ObservableCollection<string> reasonTypes = new ObservableCollection<string>();
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
            ObservableCollection<string> subReasonTypes = new ObservableCollection<string>();
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
            ObservableCollection<string> appRefNums = new ObservableCollection<string>();

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

        private async void showIdTypePickerDialog()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new PickerPageView(IDTypePickerModel));
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

        private void ReviewReasonConBtnClicked()
        {
            try
            {
                if (IsReviewReasonEnabled)
                {

                    EnableReportDetailsView();

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
                modelVATReview = await SubmitClicked();

                if (modelVATReview != null && modelVATReview.d != null)
                {

                    VATReferanceNumber = modelVATReview.d.Fbnumx;

                    await Application.Current.MainPage.Navigation.PushAsync(new VatReviewSuccessPageView());
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

        public async void FetchSecurityAmount()
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
            catch (Exception e)
            {

            }
        }

        private async void SaveClicked()
        {
            try
            {


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

                        VATObjectionGenrateorRefreshSADAD(modelVATReview.d.Fbnumx, DisamtValue, LiaamtValue, selectedApplicationRef.Abrzu?.ToString("yyyy-MM-dd'T'HH:mm:ss"), selectedApplicationRef.Abrzo?.ToString("yyyy-MM-dd'T'HH:mm:ss"), SecamtValue, SecurityNumber, selectedApplicationRef.Persl, false);

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
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }


        private void EnableReviewReasonView()
        {
            CurrentIndex = 1;
            IsBackVisible = false;
            ReviewReasonVisible = true;
            ReviewDetailsVisible = false;
            ReportDetailsVisible = false;
            SecurityPaymentVisible = false;
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
            ReportDetailsVisible = false;
            SecurityPaymentVisible = false;
            DeclarationVisible = false;
            SummaryVisible = false;
            selectedPage = (int)PagesEnum.ReviewDetails;
        }

        private void EnableReportDetailsView()
        {
            CurrentIndex = 2;
            IsBackVisible = true;
            ReviewReasonVisible = false;
            ReportDetailsVisible = true;
            ReviewDetailsVisible = false;
            SecurityPaymentVisible = false;
            DeclarationVisible = false;
            SummaryVisible = false;
            selectedPage = (int)PagesEnum.ReportDetails;
        }

        private void EnableSecurityPaymentsView()
        {
            CurrentIndex = 4;
            IsBackVisible = true;
            ReviewReasonVisible = false;
            ReportDetailsVisible = false;
            ReviewDetailsVisible = false;
            SecurityPaymentVisible = true;
            DeclarationVisible = false;
            SummaryVisible = false;
            selectedPage = (int)PagesEnum.SecurityPayments;
        }

        private void EnableDeclarationView()
        {
            CurrentIndex = 5;
            IsBackVisible = true;
            ReviewReasonVisible = false;
            ReviewDetailsVisible = false;
            ReportDetailsVisible = false;
            SecurityPaymentVisible = false;
            DeclarationVisible = true;
            SummaryVisible = false;
            selectedPage = (int)PagesEnum.Declaration;
        }

        private void EnableSummaryView()
        {
            CurrentIndex = 6;
            IsBackVisible = true;
            ReviewReasonVisible = false;
            ReportDetailsVisible = false;
            ReviewDetailsVisible = false;
            SecurityPaymentVisible = false;
            DeclarationVisible = false;
            SummaryVisible = true;
            selectedPage = (int)PagesEnum.Summary;
        }

        public void EnableSadadSecurityView()
        {
            IsSadadSecuritySelected = true;
            IsBankGurantSecuritySelected = false;

            SecurityType = AppResources.VRSADAD;


            ShowSadadGenerateButton();
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


                VATObjectionGenrateorRefreshSADAD(modelVATReview.d.Fbnumx, DisamtValue, LiaamtValue, selectedApplicationRef.Abrzu?.ToString("yyyy-MM-dd'T'HH:mm:ss"), selectedApplicationRef.Abrzo?.ToString("yyyy-MM-dd'T'HH:mm:ss"), SecamtValue, SecurityNumber, selectedApplicationRef.Persl, true);

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
            DAPOptionsEditable = false;
            IsRRAmountEdit = false;
            IsDisputeDetailsEditable = false;
            IsSecurityPOEditable = false;

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
            else
            {
                AttachmentsListViewData = attachmentsListViewData;
            }


        }

        public void PopulateVatDeRegSummaryReasonData(string requestType, string reasonTitle)
        {
            VATDeregistrationSummaryReasonData = null;
            List<VATDeregistrationSummaryModel> check = new List<VATDeregistrationSummaryModel>();
            try
            {
                check.Add(new VATDeregistrationSummaryModel
                {
                    SummaryTitle = AppResources.VatDeregRequestType,
                    SummaryData = requestType,
                    IsEditVisible = true
                });
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
            }
        }
        public void PopulateVatDeRegAttachments(List<Attachment> attachments)
        {
            var attachmentsListViewData = new ObservableCollection<Attachment>();
            foreach (Attachment attachemnt in attachments)
            {
                if (attachemnt.Dotyp == "ZVTD")
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

                check.Add(new VATDeregistrationSummaryModel
                {
                    SummaryTitle = AppResources.VatDeregDOBTitle,
                    SummaryData = dateOfBirth,
                    IsEditVisible = true
                });

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
            }
        }

        private void BackNavigations()
        {
            switch (selectedPage)
            {
                case (int)PagesEnum.ReviewDetails:
                    EnableReportDetailsView();
                    break;
                case (int)PagesEnum.ReportDetails:
                    EnableReviewReasonView();
                    break;
                case (int)PagesEnum.SecurityPayments:
                    EnableReviewDetailsView();
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

        public void updatePickerData(GenericPickerModel genericPickerModel)
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
                fetchApplicationRefNums(ReviewSubReasonPickerModel.SelectedValue);
            }
            else if (genericPickerModel.PickerId == PickerEnum.ApplicationReferenceNumber.ToString())
            {
                ApplicationRefPickerModel = genericPickerModel;
                ApplicationRefNumber = ApplicationRefPickerModel.SelectedValue;
                ResetDataAfterAppRefNumPicked();
                setDataBasedOnAppRefNum(ApplicationRefPickerModel.SelectedValue);
            }
        }

        private void ResetDataAfterReviewReasonPicked()
        {

            ReviewSubReasonPickerModel = null;
            ApplicationRefPickerModel = null;
            SubReviewReason = "";
            ApplicationRefNumber = "";
            RequestDate = null;
            IsApplicationVisible = false;
            EnableReviewReasonConButton();


        }
        private void ResetDataAfterSubReviewReasonPicked()
        {
            ApplicationRefPickerModel = null;
            ApplicationRefNumber = "";
            RequestDate = null;
            IsApplicationVisible = false;
            EnableReviewReasonConButton();

        }
        private void ResetDataAfterAppRefNumPicked()
        {
            RequestDate = null;
            EnableReviewReasonConButton();

        }


        public void fetchApplicationRefNums(string SubReasonValue)
        {
            selectedSubReviewReason = subReviewReasonList.First(subReviewReason => subReviewReason.SubReasons == SubReasonValue);

            VATObjectionFormRejected(modelVATReview.d.Fbustx, selectedReviewReason.ProcCD, selectedSubReviewReason.Code,
                modelVATReview.d.UserTypx);
            //VATObjectionEnableSubmit(modelVATReview.d.Statusx,selectedReviewReason.ProcCD,selectedSubReviewReason.Code,modelVATReview.d.RejFb);
        }

        public void setDataBasedOnAppRefNum(string appRefNum)
        {

            selectedApplicationRef = appRefNumList.Find(appRef => (appRef.Fbnum == appRefNum) || (appRef.Opbel == appRefNum));


            if (selectedApplicationRef != null)
            {

                IsApplicationVisible = true;

                if (selectedApplicationRef.Fbtyp == "RGVT")
                {

                    IsApplicationVisible = false;

                }
                else if (selectedApplicationRef.Fbtyp == "VTGR") {

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
                TaxPeriodOfCase = selectedApplicationRef.Perslt;
                TaxPeriodFrom = selectedApplicationRef.Abrzu;
                TaxPeriodTo = selectedApplicationRef.Abrzo;
                PenalityAmountInQuestion = selectedApplicationRef.Penamount;

                TotalTaxLiability = selectedApplicationRef.Liaamt;
                TaxPaid = selectedApplicationRef.Clramt;
                RequestedReviewAmount = selectedApplicationRef.Liaamt;

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
                catch (Exception e)
                { }


                EnableReviewReasonConButton();
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

        public void ValidateIdNumber()
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

                            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
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

                                PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                                IDNumber = string.Empty;
                            }
                            else
                            {

                                if (!string.IsNullOrEmpty(PickedDate))
                                {
                                    ValidateIdNumberFromApi("ZS0001");
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

                            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
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

                                PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                                IDNumber = string.Empty;
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(PickedDate))
                                {
                                    ValidateIdNumberFromApi("ZS0002");
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

                            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
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

                            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
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


            }
        }



        public void ResetData()
        {
            EnableReviewReasonView();
            ShowInstructionsDialog();


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
            TaxPeriodOfCase = "";
            TaxPeriodFrom = null;
            TaxPeriodTo = null;
            PenalityAmountInQuestion = "";
            ReportDetails = "";
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
            IsSecurityPaymentEnabled = false;
            SecurityPaymentButtonBackGroundColor = Color.FromHex("#9EA4A9");
            IsDeclarationEnabled = false;
            DeclarationButtonBackGroundColor = Color.FromHex("#9EA4A9");
            AttachmentsListViewData = null;
            SadadGenerateBtnVisible = false;
            SadadGenerateProgressVisible = false;
            SadadAmountVisible = false;
            BankGuranteeAttachmentsListViewData = null;
            IsSadadCheckBox3 = false;
            IsSadadCheckBox1 = false;
            IsSadadCheckBox2 = false;
            IsApplicationVisible = false;
            DisputeDetailsDesc = "";
            RequestedReviewAmount = "";
            TotalTaxLiability = "";
            TaxPaid = "";

            IsReportDetailsEditable = true;
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
                IsReviewReasonEnabled = false;
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
                if (SADADNumber == "" || !IsSadadCheckBox3 )
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
                if (BankGuranteeAttachmentsListViewData == null || BankGuranteeAttachmentsListViewData.Count == 0 || !IsSadadCheckBox1 || !IsSadadCheckBox2)
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
            if (attachment.Filename.Contains(".")) ;
            string Extention = attachment.Filename.Split('.')[1];
            if (Extention.Equals("PDF") || Extention.Equals("pdf"))
            {
                if (attachment.DocUrl != null)
                {
                    _navigationService.NavigateTo(App.PdfView, attachment.DocUrl);
                }
            }
            else
            {
                await WebServiceManager.email(attachment.Doguid, attachment);
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
            await PopupNavigation.Instance.PushAsync(new InstructionsBottomPopUpView(
                instructionString: AppResources.VRInstructions, checkBoxString: AppResources.VRCheckBoxDesc,
                continueString: AppResources.CRContinue,
                _dialogType: InstructionsBottomPopUpViewModel.DialogType
                    .Instructions));
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
                        var resultData = await WebServiceManager.GAZTVATChangeFillingPeriodValidateIDnumber(
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
                                await _dialogService.ShowMessage(resultData.errorMessage, AppResources.Information);
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
                await Task.Run(() => { IsLoading = false; });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
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
                        modelVATReview = await WebServiceManager.GAZTGetVATObjectionSummary("");

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

                            //modelVATReviewsReturn.DecisionDate = (modelVATReview.d.DecDt != null)
                            //    ? modelVATReview.d.DecDt.ToString()
                            //    : "";
                            ////modelVATReviewsReturnModel.DecisionTaken = modelVATReview.d.NotesSet.results;
                            ////modelVATReviewsReturnModel.AttachmentName = modelVATReview.d.AttdetSet.results[0];
                            //modelVATReviewsReturn.TaxPeriodofCase = modelVATReview.d.SecurityDtl.Perslt;
                            //modelVATReviewsReturn.PeriodFrom = (modelVATReview.d.SecurityDtl.Abrzu != null)
                            //    ? modelVATReview.d.SecurityDtl.Abrzu.ToString()
                            //    : "";
                            //modelVATReviewsReturn.PeriodTo = (modelVATReview.d.SecurityDtl.Abrzo != null)
                            //    ? modelVATReview.d.SecurityDtl.Abrzo.ToString()
                            //    : "";
                            //modelVATReviewsReturn.TotalTaxLiability = modelVATReview.d.SecurityDtl.Liaamt;
                            //modelVATReviewsReturn.TaxPaid = modelVATReview.d.SecurityDtl.Clramt;
                            //modelVATReviewsReturn.RequestToReviewAmount = modelVATReview.d.SecurityDtl.Amttp;
                            //modelVATReviewsReturn.ParticularAmount = modelVATReview.d.SecurityDtl.Disamt;
                            //modelVATReviewsReturn.Corrections = modelVATReview.d.NotesSet.results;
                            //modelVATReviewsReturn.SecurityAmount = modelVATReview.d.SecurityDtl.Secamt;
                            //modelVATReviewsReturn.SADADNumber = modelVATReview.d.SecurityDtl.Sopbel;
                            //modelVATReviewsReturn.MethodSubmitSecurity = modelVATReview.d.SecurityDtl.Sectp;
                            //modelVATReviewsReturn.ChkSecurityPayment = modelVATReview.d.SecurityDtl.ChkCash;
                            //modelVATReviewsReturn.ChkBankGuarantee = modelVATReview.d.SecurityDtl.ChkBank;
                            //modelVATReviewsReturn.ChkInfoCorrect = modelVATReview.d.DecFlg1;
                            //modelVATReviewsReturn.NameOfTaxPayer = modelVATReview.d.FullName;
                            //modelVATReviewsReturn.ApplicationNo = modelVATReview.d.Fbnumx;
                            //modelVATReviewsReturn.Date = (modelVATReview.d.Declarationdt != null)
                            //    ? modelVATReview.d.Declarationdt.ToString()
                            //    : "";

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
                await Task.Run(() => { IsLoading = false; });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
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
                _VATObjectionEnableSubmit = await WebServiceManager.GAZTGetVATObjectionEnableSubmit(statusx, rvRsn, rvSubRsn, rejFb);

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
                strACK = WebServiceManager.GetVATObjectionDownloadAck(fbnum);

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

        public async void VATObjectionFormRejected(string fbustx, string RvRsn, string rvSubRsn, string UserTypx)
        {

            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });

                //VATObjectionRejectedFormModel _VATObjectionRejected = new VATObjectionRejectedFormModel();
                _VATObjectionRejected = await WebServiceManager.GAZTGetVATObjectionFormRejected(fbustx, RvRsn, rvSubRsn, UserTypx);

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

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });

            }
        }

        public async void VATObjectionGenrateorRefreshSADAD(string fbnum, decimal Disamt, decimal Liaamt, string Abrzu, string Abrzo, decimal Secamt, string Security, string Persl, bool isFlagenable)
        {

            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });

                VATObjectionGenrateSadadModel _VATObjectionGenrateSadad = new VATObjectionGenrateSadadModel();
                _VATObjectionGenrateSadad = await WebServiceManager.GAZTGetVATObjectionGenrateorRefreshSADAD(fbnum, Disamt, Liaamt, Abrzu, Abrzo, Secamt, Security, Persl, isFlagenable);

                if (_VATObjectionGenrateSadad != null && _VATObjectionGenrateSadad.d != null)
                {
                    

                    
                    
                    SADADNumber = _VATObjectionGenrateSadad.d.Sopbel;
                    SecurityNumber = _VATObjectionGenrateSadad.d.Security;

                    if (string.IsNullOrEmpty(SADADNumber)) {
                        ShowRefreshButton();

                    }
                    else {
                        ShowSadadAmount();
                        MakeViewOnlyItems();
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
                _VATObjectionSecurityAmount = await WebServiceManager.GAZTGetVATObjectionSecurityAmount(disamt, liaamt, clramt);

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
                    catch (Exception e)
                    {

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
                _VATObjectionValidateTaxpayer = await WebServiceManager.GAZTGetVATObjectionValidateTaxPayer(idnum, idtype, passExpDt, taxpDob);

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
                        billModelResult = await WebServiceManager.GAZTGetVATObjectionViewBill(opbel, vtre2);

                        if (billModelResult != null && billModelResult.d != null)
                        {
                            foreach (var item in billModelResult.d.results)
                            {
                                VBDocumentNumber = item.Opbel;
                                VBSadadNumber = item.Vtre2;
                                VBDateofPenality = item.Bldat; //item.Bldat.ToString();
                                VBDescriptionOfPenality = item.Desc;
                                VBPeriodkey = item.Perslt;
                                VBStartDate = item.Abrzu; //item.Abrzu.ToString();
                                VBEndDate = item.Abrzo; //item.Abrzo.ToString();
                                VBDueDate = item.Studt; //item.Studt.ToString();
                                VBAmount = item.Betrh;
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
            _postData.DecFlg2 = modelVATReview.d.DecFlg2;
            _postData.DecIdNo = modelVATReview.d.DecIdNo;
            _postData.Declarationdt = modelVATReview.d.Declarationdt;
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
            _postData.Gpartx = modelVATReview.d.Gpartx;
            _postData.Iban = modelVATReview.d.Iban;
            _postData.IdDetailSet = modelVATReview.d.IdDetailSet.results;
            _postData.IdType = modelVATReview.d.IdType;
            _postData.Langx = modelVATReview.d.Langx;

            _postData.Officerx = modelVATReview.d.Officerx;
            _postData.PeriodKey = "M";
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
                _postData.StepNumberx = "01";

            }
            else
            {
                _postData.StepNumberx = "03";

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
            notes.Noteno = "001";
            notes.Notenoz = "001";
            notes.Rcodez = "RAVT_BOX";
            notes.Refnamez = "";
            notes.Tdformat = "";
            notes.XInvoicez = "";
            notes.XObsoletez = "";
            notes.DataVersionz = "00000";
            notes.ByGpartz = App.LoginDataRetrieved.TIN;

            var NotesetResult = new Models.VatReviewModel.NotesSet();
            var noteSetList = new List<NotesSetResults>();
            noteSetList.Add(notes);
            NotesetResult.results = noteSetList;
            modelVATReview.d.NotesSet = NotesetResult;


            _postData.NotesSet = modelVATReview.d.NotesSet.results;


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
            _postData.RvRsn = selectedReviewReason.ProcCD;
            _postData.RvSubRsn = selectedSubReviewReason.Code;
            _postData.RejFb = ApplicationRefNumber;

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
                modelVATReview.d.DecFlg1 = true;
                modelVATReview.d.DecFlg2 = true;
                modelVATReview.d.Decnm = ReportDetails.ToString();

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

                modelVATReview.d.DateFrm = selectedApplicationRef.DateFrm;
                modelVATReview.d.DateFrmOld = selectedApplicationRef.DateFrm;
                modelVATReview.d.DecDt = RequestDate.ToString();

                var strDecDate = "";
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





                if (IsSecurityPaymentsTabVisible)
                {

                    if (IsSadadSecuritySelected)
                    {

                        modelVATReview.d.SecurityDtl.Sectp = "C";
                        modelVATReview.d.SecurityDtl.Sopbel = SADADNumber;
                    }
                    else
                    {



                        modelVATReview.d.SecurityDtl.Sectp = "B";

                    }

                    if (!string.IsNullOrEmpty(strDecDate))
                    {

                        modelVATReview.d.SecurityDtl.Abrzo = strDecDate;
                        modelVATReview.d.SecurityDtl.Abrzu = strDecDate;

                    }

                    modelVATReview.d.SecurityDtl.ChkBank = "X";
                    modelVATReview.d.SecurityDtl.ChkCash = "X";
                    modelVATReview.d.SecurityDtl.DataVersion = "00001";
                    modelVATReview.d.SecurityDtl.Disamt = RequestedReviewAmount;
                    modelVATReview.d.SecurityDtl.Liaamt = selectedApplicationRef.Liaamt;
                    modelVATReview.d.SecurityDtl.Opbel = selectedApplicationRef.Opbel;
                    modelVATReview.d.SecurityDtl.Penamount = selectedApplicationRef.Penamount;
                    modelVATReview.d.SecurityDtl.Perslt = selectedApplicationRef.Perslt;
                    modelVATReview.d.SecurityDtl.Secamt = SecurityAmount;
                    modelVATReview.d.SecurityDtl.Clramt = selectedApplicationRef.Clramt;

                }

                request = BuildRequestObject();

                if (IsGeneratingFormbundle)
                {

                    request.Operationx = "05";
                }
                else
                {

                    request.Operationx = "01";
                }






                response = await WebServiceManager.SaveVatReviewObjection(request);



                PopToRootPage();
                if (response != null)
                {
                    try
                    {
                        if (response != null && response.d != null)
                        {




                            //if (response.d.Submitz.Equals("X"))
                            //{
                            //    string number = response.d.Fbnumz;
                            //    string displayMessage = AppResources.VATRSuccessFullVoidMessage + " " + number;
                            //    await _dialogService.ShowMessage(displayMessage, AppResources.Information);
                            //    _navigationService.GoBack();
                            //}
                            //if (response.d.Submitz.Equals("05"))
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


        public static DateTime ConvertJsonToDateTime(string jsonDate)
        {
            // JavaScript uses the unix epoch of 1/1/1970. Note, it's important to call ToLocalTime()
            // after doing the time conversion, otherwise we'd have to deal with daylight savings hooey.
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            Double milliseconds = Convert.ToDouble(jsonDate);
            DateTime dateTime = unixEpoch.AddMilliseconds(milliseconds).ToLocalTime();

            return dateTime;
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
                    PopToRootPage();
                    if (_vATDeclaration != null && _vATDeclaration.d != null)
                    {
                        VATinNumber = _vATDeclaration.d.Gpart;
                        VAAccNumber = _vATDeclaration.d.Fin;
                        VAIdnumber = _vATDeclaration.d.Idnumber;
                        VATaxpayerName = _vATDeclaration.d.Tpnm;
                        VAAddress = _vATDeclaration.d.ADRSet.results[0].BuildingNo + "," +
                                    _vATDeclaration.d.ADRSet.results[0].Street + "," +
                                    _vATDeclaration.d.ADRSet.results[0].Addrnumber + "," +
                                    _vATDeclaration.d.ADRSet.results[0].RegionDesc + "," +
                                    _vATDeclaration.d.ADRSet.results[0].City + "," +
                                    _vATDeclaration.d.ADRSet.results[0].PostalCd;
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
                        /*_vATDeclaration.d.Fbguid = SelectedICRGUID;
                        VATDeclaration vATDeclaration = new VATDeclaration();
                        VATDeclarationD vATDeclarationD = new VATDeclarationD();
                        if (_vATDeclaration.d.ATTACHSet != null && _vATDeclaration.d.ATTACHSet.results != null && _vATDeclaration.d.ATTACHSet.results.Count > 0)
                            numberOfAttachmentComingFromServer = _vATDeclaration.d.ATTACHSet.results.Count;
                        Result5 result5 = new Result5();
                        List<Result5> lst = new List<Result5>();
                        ADRSet _aDRSet = new ADRSet();
                        lst.Add(result5);
                        vATDeclaration.d = vATDeclarationD;
                        vATDeclaration.d.ADRSet = _aDRSet;
                        vATDeclaration.d.ADRSet.results = lst;
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            _navigationService.NavigateTo(App.VATReturnsPageViewEX, _vATDeclaration);
                        });*/

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
                        vATRegistration = await WebServiceManager.GAZTGetVATRegistrationDisplayDetailsData();
                        PopToRootPage();// If seesion Expired it will navigate to Dashboard page

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
                            //MaximumDisplayValueOfSlider1 = MinMaxRanges.Where(x => x.QueNo == "001").Select(x => x.MaxRangeValue).FirstOrDefault();
                            //MinimumDisplayValueOfSlider1 = MinMaxRanges.Where(x => x.QueNo == "001").Select(x => x.MinRangeValue).FirstOrDefault();

                            //MaximumDisplayValueOfSlider2 = MinMaxRanges.Where(x => x.QueNo == "002").Select(x => x.MaxRangeValue).FirstOrDefault();
                            //MinimumDisplayValueOfSlider2 = MinMaxRanges.Where(x => x.QueNo == "002").Select(x => x.MinRangeValue).FirstOrDefault();
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
                    catch (InternetException ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            //await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            IsLoading = false;
                            //_navigationService.GoBack();
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
                    //await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    //_navigationService.GoBack();
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
                    // _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    // _navigationService.GoBack();
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
                        vATDeRegistration = await WebServiceManager.GAZTGetVATDeRegistrationData();


                        if (vATDeRegistration != null && vATDeRegistration.d != null)
                        {

                            if (vATDeRegistration.d.Idnumbr != null)

                                if (string.IsNullOrEmpty(IDType))
                                {

                                    PopulateVatDeRegSummaryDeclarationData(vATDeRegistration.d.Type, vATDeRegistration.d.Idnumbr, vATDeRegistration.d.Declaredt, vATDeRegistration.d.Contactnm);
                                }

                            PopulateVatDeRegAttachments(vATDeRegistration.d.AttdetSet.results);

                            PopulateVatDeRegSummaryReasonData(vATDeRegistration.d.Type, vATDeRegistration.d.Reason);
                            //populateAttachments(vATDeRegistration);

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

                });
            }
        }

        public async Task GetVATReviewRequestTPFV(string strOfficerz, string strGpartz, string strEuser, string strFbguid, string strReviewFg = "true")
        {
            try
            {
                //strOfficerz = "3102452201";
                //strGpartz = "3102452201";
                //strEuser = "00000001000008331567";
                //strFbguid = "005056B1F8FB1EEABDFEB2E0181E473D";

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
                        var vatReviewFBguid = await WebServiceManager.GAZTVATObjectionSummaryInputData(selectedApplicationRef.Fbnum, "", "TPFV");

                        if (vatReviewFBguid != null && vatReviewFBguid.d != null)
                        {


                            _VATReviewRequestTPFV = await WebServiceManager.GAZTGetVATReviewRequestTPFV(strOfficerz, strGpartz, strEuser, vatReviewFBguid.d.Fbguid, strReviewFg);

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

                                /*if (_VATReviewRequestTPFV.d.AttdetSet.results.Where(x => x.Dotyp.ToUpper() == "TPFB").Count() > 0)
                                    VITDAttachmentsListViewData = _VATReviewRequestTPFV.d.AttdetSet.results.Where(x => x.Dotyp.ToUpper() == "TPFB").FirstOrDefault().Filename;*/
                                // _VATReviewRequestTPFVReturn.DeclarationFlag = _VATReviewRequestTPFV.d.Decchk1;
                                VITDIDType = IDToNameDictionary[_VATReviewRequestTPFV.d.Idtp];
                                VITDIDNumber = _VATReviewRequestTPFV.d.Idno;
                                //VITDDateOfBirth = _VATReviewRequestTPFV.d.
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
                        var _dregInputResult = await WebServiceManager.GAZTVATObjectionSummaryInputData(selectedApplicationRef.Fbnum, "", "DGVT");

                        if (_dregInputResult != null && _dregInputResult.d != null)
                        {

                            var dregresult = await WebServiceManager.GAZTGetVATReviewDREGViewApplication(_dregInputResult.d.Fbguid);
                            var dregReasonset = await WebServiceManager.GAZTGetVATReviewDREGReasonSet("VT_DREG");

                            if (dregresult != null && dregresult.d != null)
                            {

                                _vATDREGViewApllicationViewModel.ReasonforDeRegistration = dregReasonset.d.results.
                                Where(x => x.Reason == dregresult.d.Reason).FirstOrDefault().Rdesc;

                                _vATDREGViewApllicationViewModel.ContactPersonName = dregresult.d.Contactnm;
                                _vATDREGViewApllicationViewModel.DeclarationId = dregresult.d.Idnumbr;

                                var declarationDate = "";

                                if(dregresult.d.Declaredt != null) {

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
                        _VATReviewRequestVTGR = await WebServiceManager.GAZTGetVATReviewRequestVTGR(strEuser, strFbguid, strGpart, strTxnTpz, strFBNum);
                        if (_VATReviewRequestVTGR != null && _VATReviewRequestVTGR.d != null)
                        {
                            //      //_VATReviewRequestVTGRReturn.AgreeFlag = _VATReviewRequestVTGR.d.AgrFg;
                            //  VRVGTINNumber = _VATReviewRequestVTGR.d.TABLESet.results.FirstOrDefault().Gpart;
                            //  VRVGTINHolderName = _VATReviewRequestVTGR.d.TABLESet.results.FirstOrDefault().Actnm;
                            // _VATReviewRequestVTGRReturn.Address = _VATReviewRequestVTGR.d.TABLESet.results.FirstOrDefault().Street + ", " + _VATReviewRequestVTGR.d.TABLESet.results.FirstOrDefault().RegionDesc;
                            //  _VATReviewRequestVTGRReturn.CRLicense = _VATReviewRequestVTGR.d.TABLESet.results.FirstOrDefault().LicenseCrno;
                            //  _VATReviewRequestVTGRReturn.VATAccount = _VATReviewRequestVTGR.d.TABLESet.results.FirstOrDefault().Account;
                            //  _VATReviewRequestVTGRReturn.LegalPersonType = _VATReviewRequestVTGR.d.TABLESet.results.FirstOrDefault().PersonTp;
                            //   _VATReviewRequestVTGRReturn.IsGrpMbrExporter = _VATReviewRequestVTGR.d.TABLESet.results.FirstOrDefault().Exporter;
                            //    _VATReviewRequestVTGRReturn.IsGrpMbrImporter = _VATReviewRequestVTGR.d.TABLESet.results.FirstOrDefault().Importer;
                            //    _VATReviewRequestVTGRReturn.WhatIsYourVATEliigibleSupplies = _VATReviewRequestVTGR.d.TABLESet.results.FirstOrDefault().VatSupply;
                            //    _VATReviewRequestVTGRReturn.WhatIsYourVATEliigiblePurchases = _VATReviewRequestVTGR.d.TABLESet.results.FirstOrDefault().VatPuchase;
                           // VRVGVATeligiblesupplies = _VATReviewRequestVTGR.d.AggreSupply;
                           // VRVGVATeligiblepurchases = _VATReviewRequestVTGR.d.AggrePurchase;
                            VRVGEffectivedate = _VATReviewRequestVTGR.d.EFFDATESet.results.Where(x => x.Persl == _VATReviewRequestVTGR.d.Persl).FirstOrDefault().Txt50
                            ;
                            //_VATReviewRequestVTGRReturn.AttachmentName = _VATReviewRequestVTGR.d.ELGBL_DOCSet.results.Where(x=> x.DmsTp=="Txt50").FirstOrDefault().doctyp;
                            //    _VATReviewRequestVTGRReturn.DeclarationFlag = _VATReviewRequestVTGR.d.Decfg;
                            VRVGIDType = IDToNameDictionary[_VATReviewRequestVTGR.d.DecidTy];
                            VRVGIDNumber = _VATReviewRequestVTGR.d.DecidNo;
                            VRVGContactPersonName = _VATReviewRequestVTGR.d.Decname;

                            VRVGVATeligiblesupplies = VGSupplicesDictionary[_VATReviewRequestVTGR.d.AggreSupply];
                            VRVGVATeligiblepurchases = VGPurchasesDictionary[_VATReviewRequestVTGR.d.AggrePurchase];

                            var tinsListViewData = new ObservableCollection<VATReviewRequestVTGRModel.TABLESetResult>();
                            foreach (VATReviewRequestVTGRModel.TABLESetResult tin in _VATReviewRequestVTGR.d.TABLESet.results)
                            {

                                tinsListViewData.Add(tin);

                            }
                            VRVGTinsListViewData = tinsListViewData;
                            var attachmentsListViewData = new ObservableCollection<Attachment>();
                            foreach (Attachment attachemnt in _VATReviewRequestVTGR.d.ATTDETSet.results)
                            {

                                attachmentsListViewData.Add(attachemnt);

                            }
                            VRVGAttachmentsListViewData = attachmentsListViewData;

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
                        var _dregInputResult = await WebServiceManager.GAZTVATObjectionSummaryInputData(selectedApplicationRef.Fbnum, "", "DGVT");

                        if (_dregInputResult != null && _dregInputResult.d != null)
                        {

                            var dregresult = await WebServiceManager.GAZTGetVATReviewDREGViewApplication(_dregInputResult.d.Fbguid);
                            var dregReasonset = await WebServiceManager.GAZTGetVATReviewDREGReasonSet("VT_SUSP");

                            if (dregresult != null && dregresult.d != null)
                            {

                                VRVSRequestType = AppResources.VRVSVATReturnFilingObligationSuspension;


                                VRVSRFSuspensionofFiling = dregReasonset.d.results.
                                Where(x => x.Reason == dregresult.d.Reason).FirstOrDefault().Rdesc;

                                VRVSContactPersonName = dregresult.d.Contactnm;
                                VRVSIDNumber = dregresult.d.Idnumbr;
                                VRVSIDType = dregresult.d.Type;

                                var settings = new JsonSerializerSettings
                                {
                                    DateFormatString = "yyyy-MM-ddTH:mm:ss",
                                    DateTimeZoneHandling = DateTimeZoneHandling.Utc
                                };

                                var startDate = "";
                                var endDate = "";

                                try {

                                    if(dregresult.d.StartDate != null) {

                                        var jsonstartDate = JsonConvert.SerializeObject(dregresult.d.StartDate, settings);
                                        startDate = Regex.Replace(jsonstartDate, "[@,\\.\";'\\\\]", string.Empty);
                                    }

                                    if(dregresult.d.EndDate != null) {

                                        var jsonEndDate = JsonConvert.SerializeObject(dregresult.d.EndDate, settings);
                                        endDate = Regex.Replace(jsonEndDate, "[@,\\.\";'\\\\]", string.Empty);
                                    }


                                   

                                   

                                }
                                catch {

                                }

                                if (dregresult.d.StartDate != null)
                                {


                                    var _suspensionResult = await WebServiceManager.GAZTGetVATReviewDREGSuspensionDetailSet(startDate, endDate);
                                    if (_suspensionResult != null & _suspensionResult.d.results.Count > 0)
                                    {
                                        if(_suspensionResult.d.results[0].StartDate != null) {

                                            VRVSStartofSuspensionPeriod = _suspensionResult.d.results[0].StartDate?.ToString("dd-MM-yyyy");

                                        }

                                        if(_suspensionResult.d.results[0].EndDate != null) {

                                            VRVSEndofSuspensionPeriod = _suspensionResult.d.results[0].EndDate?.ToString("dd-MM-yyyy");

                                        }

                                        if(_suspensionResult.d.results[0].Duedate != null) {

                                            VRVSNextfilingduedate = _suspensionResult.d.results[0].Duedate?.ToString("dd-MM-yyyy");

                                        }

                                        if(_suspensionResult.d.results[0].SuspDtfrom != null) {

                                            VRVSRFSuspensionofFiling = _suspensionResult.d.results[0].SuspDtfrom?.ToString("dd-MM-yyyy") + " - " + _suspensionResult.d.results[0].SuspDtto?.ToString("dd-MM-yyyy");

                                        }

                                        if(_suspensionResult.d.results[0].NextDtfrom != null) {

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
    }
}