using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Manager;
using EGAZT.Models;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Views.NewDesign.GenericPickers;
using EGAZT.Views.NewDesign.VATDeclarationPages;
using EGAZT.Views.NewDesign.ZakatDeregistration;
using EGAZT.Views.NewDesign.ZakatInstalmentPlan;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using static GAZT.ErrorMessage;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration
{
    [Preserve(AllMembers = true)]
    public class TINDeregistrationPageViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand GoBackBtnTapped { get; set; }
        public ICommand CloseBtnTapped { get; set; }
        public ICommand IdTypeTapped { get; set; }
        public ICommand PermitIdtypeTapped { get; set; }
        public ICommand PermitTypeTinUnfocused { get; set; }
        public ICommand OnMoreClicked { get; set; }

        public int DefaultMonth;
        public int DefaultMonthHijri;
        public bool isSubmitted;
        public bool isSaveAsDraftCalledForAttachment = false;
        public string permitThirdOptionReason = string.Empty;
        public string selectedCalPermitNo = null;
        public static int numberOfAttachmentSentToAttachmentPopUp = 0;
        List<TinDeregestrationAttachmentsModel> check;
        List<Attachment> attachmentList;
        public bool IsDeRegistrationValid = true;
        public bool IsEnteredTINValid = false;

        //
        #endregion

        #region Commands
        public ICommand ReasonContinueBtnTapped { get; set; }
        public ICommand OutletPermitPopupReasonContinueBtnTapped { get; set; }
        public ICommand OutletContinueBtnTapped { get; set; }
        public ICommand AttachmentsContinueBtnTapped { get; set; }
        public ICommand DeclarationContinueBtnTapped { get; set; }
        public ICommand SummaryContinueBtnTapped { get; set; }
        public ICommand OnTinRegisrtationReasonDateTapped { get; set; }
        public ICommand OnTinRegistrationReasonTapped { get; set; }
        public ICommand OnTinRegistrationDateTapped { get; set; }
        public ICommand OnOutletPermitTypeDeRegisrtationReasonDateTapped { get; set; }
        public ICommand OnOutletPermitTypeReasonTapped { get; set; }
        public ICommand OnPermitDobTapped { get; set; }

        public ICommand OnPermitTypeReasonTapped { get; set; }
        public ICommand OnTinDeregOutletDeregDatePickerTapped { get; set; }

        #endregion

        #region Properties
        public string attachmentsListViewDataString { get; set; }
        private string _labelText;
        public string LabelText
        {
            get => _labelText;
            set
            {
                if (_labelText == value) return;
                _labelText = value;
                RaisePropertyChanged(nameof(LabelText));
            }
        }
        public enum ProcessStep
        {
            Step1 = 0,
            Step2, Step3, Step4, Step5, Step6
        }

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
                RaisePropertyChanged("CurrentStep");
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
                RaisePropertyChanged("IsBackButtonVisible");
            }
        }

        private bool _isOutletTranferOutletGridVisible { get; set; }
        public bool IsOutletTranferOutletGridVisible
        {
            get
            {
                return _isOutletTranferOutletGridVisible;
            }
            set
            {
                if (_isOutletTranferOutletGridVisible == value) return;

                _isOutletTranferOutletGridVisible = value;
                RaisePropertyChanged("IsOutletTranferOutletGridVisible");
            }
        }

        //
        private bool _isMultiplePermitsVisible = false;
        public bool IsMultiplePermitsVisible
        {
            get
            {
                return _isMultiplePermitsVisible;
            }
            set
            {
                if (_isMultiplePermitsVisible == value) return;

                _isMultiplePermitsVisible = value;
                RaisePropertyChanged("IsMultiplePermitsVisible");
            }
        }

        private bool _isNodataAvailableVisible = false;
        public bool IsNodataAvailableVisible
        {
            get
            {
                return _isNodataAvailableVisible;
            }
            set
            {
                if (_isNodataAvailableVisible == value) return;

                _isNodataAvailableVisible = value;
                RaisePropertyChanged("IsNodataAvailableVisible");
            }
        }
        private List<object> _todayDate;
        public List<object> TodayDate
        {
            get
            {
                return _todayDate;
            }
            set
            {
                if (_todayDate == value) return;

                _todayDate = value;
                RaisePropertyChanged("TodayDate");
            }
        }
        private List<object> _todayDateinHijri;
        public List<object> TodayDateinHijri
        {
            get
            {
                return _todayDateinHijri;
            }
            set
            {
                if (_todayDateinHijri == value) return;

                _todayDateinHijri = value;
                RaisePropertyChanged("TodayDateinHijri");
            }
        }
        private string _pkrDBO = string.Empty;
        public string PkrDBO
        {
            get
            {
                return _pkrDBO;
            }
            set
            {
                if (_pkrDBO == value) return;

                _pkrDBO = value;
                RaisePropertyChanged("PkrDBO");
            }
        }

        private string _dateOfBirth = string.Empty;
        public string DateOfBirth
        {
            get
            {
                return _dateOfBirth;
            }
            set
            {
                if (_dateOfBirth == value) return;

                _dateOfBirth = value;
                RaisePropertyChanged("DateOfBirth");
            }
        }
        private string _tINNumber = string.Empty;
        public string TINNumber
        {
            get
            {
                return _tINNumber;
            }
            set
            {
                if (_tINNumber == value) return;

                _tINNumber = value;
                if (string.IsNullOrEmpty(_tINNumber))
                {
                    IsEnteredTINValid = false;
                }
                RaisePropertyChanged("TINNumber");
            }
        }
        private string _pkrDBOPrev = string.Empty;
        public string PkrDBOPrev
        {
            get
            {
                return _pkrDBOPrev;
            }
            set
            {
                if (_pkrDBOPrev == value) return;

                _pkrDBOPrev = value;
                RaisePropertyChanged("PkrDBOPrev");
            }
        }
        private string _PickerDobToDisplay = string.Empty;
        public string PickerDobToDisplay
        {
            get
            {
                return _PickerDobToDisplay;
            }
            set
            {
                if (_PickerDobToDisplay == value) return;

                _PickerDobToDisplay = value;
                RaisePropertyChanged("PickerDobToDisplay");
            }
        }
        private string _PickerCloseAllDeregDateDisplay = string.Empty;
        public string PickerCloseAllDeregDateDisplay
        {
            get
            {
                return _PickerCloseAllDeregDateDisplay;
            }
            set
            {
                if (_PickerCloseAllDeregDateDisplay == value) return;

                _PickerCloseAllDeregDateDisplay = value;
                RaisePropertyChanged("PickerCloseAllDeregDateDisplay");
            }
        }


        private string _TransferPickerDOBDateDisplay = string.Empty;
        public string TransferPickerDOBDateDisplay
        {
            get
            {
                return _TransferPickerDOBDateDisplay;
            }
            set
            {
                if (_TransferPickerDOBDateDisplay == value) return;

                _TransferPickerDOBDateDisplay = value;
                RaisePropertyChanged("TransferPickerDOBDateDisplay");
            }
        }
        private string _PickerTransferDeregDateDisplay = string.Empty;
        public string PickerTransferDeregDateDisplay
        {
            get
            {
                return _PickerTransferDeregDateDisplay;
            }
            set
            {
                if (_PickerTransferDeregDateDisplay == value) return;

                _PickerTransferDeregDateDisplay = value;
                RaisePropertyChanged("PickerTransferDeregDateDisplay");
            }
        }
        private string _PickerDOBDateDisplay = string.Empty;
        public string PickerDOBDateDisplay
        {
            get
            {
                return _PickerDOBDateDisplay;
            }
            set
            {
                if (_PickerDOBDateDisplay == value) return;

                _PickerDOBDateDisplay = value;
                RaisePropertyChanged("PickerDOBDateDisplay");
            }
        }

        private string _OutletCheckboxTitle = string.Empty;
        public string OutletCheckboxTitle
        {
            get
            {
                return _OutletCheckboxTitle;
            }
            set
            {
                if (_OutletCheckboxTitle == value) return;

                _OutletCheckboxTitle = value;
                RaisePropertyChanged("OutletCheckboxTitle");
            }
        }
        //
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
                RaisePropertyChanged("IsReasonViewEnabled");
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
                RaisePropertyChanged("IsHijriCal");
            }
        }

        private bool _IsDOBHijriCal = false;
        public bool IsDOBHijriCal
        {
            get
            {
                return _IsDOBHijriCal;
            }
            set
            {
                if (_IsDOBHijriCal == value) return;

                _IsDOBHijriCal = value;
                RaisePropertyChanged("IsDOBHijriCal");
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

        private bool _isDeclarationViewEnabled = false;
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
                RaisePropertyChanged("IsDeclarationViewEnabled");
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
                MessagingCenter.Send<TINDeregistrationPageViewModel, bool>(this, "IsDeclarationChecked", value);
                if (_isDeclarationChecked == value) return;

                _isDeclarationChecked = value;

                if (_isDeclarationChecked)
                {
                    IsDeclarationContinueButtonEnabled = true;
                    TinDeregistrationData.ADeclarationChkbox = "1";

                }
                else
                {
                    IsDeclarationContinueButtonEnabled = false;
                    TinDeregistrationData.ADeclarationChkbox = "0";

                }

                RaisePropertyChanged("IsDeclarationChecked");
            }
        }
        private bool _isOutletChecked;
        public bool IsOutletChecked
        {
            get
            {
                return _isOutletChecked;
            }
            set
            {
                if (_isOutletChecked == value) return;

                MessagingCenter.Send<TINDeregistrationPageViewModel, bool>(this, "IsOutletChecked", value);
                _isOutletChecked = value;

                if (_isOutletChecked)
                {
                    EnableOutletDetaislView(true);

                    TinDeregistrationData.AStep = 4;


                }
                else
                {
                    IsOutletContinueButtonEnabled = false;

                }

                RaisePropertyChanged("IsOutletChecked");
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
                if (_declarationContinueButtonnBackroundColor == value) return;

                _declarationContinueButtonnBackroundColor = value;
                RaisePropertyChanged("DeclarationContinueButtonnBackroundColor");
            }
        }
        private Color _outletContinueButtonnBackroundColor = Color.FromHex("#d49504");
        public Color OutletContinueButtonnBackroundColor
        {
            get
            {
                return _outletContinueButtonnBackroundColor;
            }
            set
            {
                _outletContinueButtonnBackroundColor = value;
                RaisePropertyChanged("OutletContinueButtonnBackroundColor");
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
        private bool _iSOutletContinueButtonEnabled = false;
        public bool IsOutletContinueButtonEnabled
        {
            get
            {
                return _iSOutletContinueButtonEnabled;
            }
            set
            {
                _iSOutletContinueButtonEnabled = value;
                if (value)
                {
                    OutletContinueButtonnBackroundColor = Color.FromHex("#d49504");
                }
                else
                {
                    OutletContinueButtonnBackroundColor = Color.FromHex("#9EA4A9");
                }
                RaisePropertyChanged("IsOutletContinueButtonEnabled");
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

        public TINDeregistrationModel tinDeregistrationModel { get; set; }
        public TINDeregistrationModel TinDeregistrationModel
        {
            get
            {
                return tinDeregistrationModel;
            }

            set
            {
                if (tinDeregistrationModel == value) return;

                tinDeregistrationModel = value;
                RaisePropertyChanged("TinDeregistrationModel");
            }
        }

        public List<TINDeregistrationModel> c { get; set; }

        public List<TINDeregistrationModel> outletDecisionOptions { get; set; }
        public List<TINDeregistrationModel> OutletDecisionOptions
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
                //if (value != null && value.Count > 0)
                //    IsOutletDecisionOptionsLVVisible = true;
                //else
                //    IsOutletDecisionOptionsLVVisible = false;
                RaisePropertyChanged("OutletDecisionOptions");
            }
        }
        private bool _isOutletDecisionOptionsLVVisible = true;
        public bool IsOutletDecisionOptionsLVVisible
        {
            get => _isOutletDecisionOptionsLVVisible; set
            {
                _isOutletDecisionOptionsLVVisible = value;
                RaisePropertyChanged(nameof(IsOutletDecisionOptionsLVVisible));
            }
        }


        public List<TINDeregistrationModel> permitOutletDecisionOptions { get; set; }
        public List<TINDeregistrationModel> PermitOutletDecisionOptions
        {
            get
            {
                return permitOutletDecisionOptions;
            }
            set
            {
                if (permitOutletDecisionOptions == value) return;

                if (value != null)
                    permitOutletDecisionOptions = value;
                RaisePropertyChanged("PermitOutletDecisionOptions");
            }
        }

        private TINDeregistrationModel selectedPermitTypeOutletOption;
        public TINDeregistrationModel SelectedPermitTypeOutletOption
        {
            get
            {
                return selectedPermitTypeOutletOption;
            }
            set
            {
                if (selectedPermitTypeOutletOption == value) return;

                if (value != null)
                {
                    selectedPermitTypeOutletOption = value;

                    if (TinDeregistrationData != null)
                    {
                        //if (TinDeregistrationData.ADregOpt != null)
                        //{
                        //    TinDeregistrationData.ADregOpt = selectedPermitTypeOutletOption.OutletOptionIndex;
                        //}
                        //else
                        //{
                        //    TinDeregistrationData.ADregOpt = string.Empty;
                        //    TinDeregistrationData.ADregOpt = selectedPermitTypeOutletOption.OutletOptionIndex;
                        //}

                        PopulateAttachmentsListViewTemplate();
                    }
                }
                RaisePropertyChanged("SelectedPermitTypeOutletOption");
            }
        }

        private int _selectedPermitOutletOptionIndex { get; set; }
        public int SelectedPermitOutletOptionIndex
        {
            get
            {
                return _selectedPermitOutletOptionIndex;
            }
            set
            {
                if (_selectedPermitOutletOptionIndex == value) return;

                _selectedPermitOutletOptionIndex = value;
                RaisePropertyChanged("SelectedPermitOutletOptionIndex");
            }
        }

        public List<TinDeregestrationAttachmentsModel> attachmentsListViewData { get; set; }
        public List<TinDeregestrationAttachmentsModel> AttachmentsListViewData
        {
            get
            {
                return attachmentsListViewData;
            }

            set
            {
                if (attachmentsListViewData == value) return;

                attachmentsListViewData = value;
                RaisePropertyChanged("AttachmentsListViewData");
            }
        }

        public List<TINDeregistrationSummaryModel> _tinDeregistrationSummaryReasonData { get; set; }
        public List<TINDeregistrationSummaryModel> TinDeregistrationSummaryReasonData
        {
            get
            {
                return _tinDeregistrationSummaryReasonData;
            }

            set
            {
                if (_tinDeregistrationSummaryReasonData == value) return;

                _tinDeregistrationSummaryReasonData = value;
                RaisePropertyChanged("TinDeregistrationSummaryReasonData");
            }
        }

        public List<TINDeregistrationSummaryModel> _tinDeregistrationSummaryOutletData { get; set; }
        public List<TINDeregistrationSummaryModel> TinDeregistrationSummaryOutletData
        {
            get
            {
                return _tinDeregistrationSummaryOutletData;
            }

            set
            {
                if (_tinDeregistrationSummaryOutletData == value) return;

                _tinDeregistrationSummaryOutletData = value;
                RaisePropertyChanged("TinDeregistrationSummaryOutletData");
            }
        }

        public List<TINDeregistrationSummaryModel> _tinDeregistrationSummaryDeclarationData { get; set; }
        public List<TINDeregistrationSummaryModel> TinDeregistrationSummaryDeclarationData
        {
            get
            {
                return _tinDeregistrationSummaryDeclarationData;
            }

            set
            {
                if (_tinDeregistrationSummaryDeclarationData == value) return;


                _tinDeregistrationSummaryDeclarationData = value;
                RaisePropertyChanged("TinDeregistrationSummaryDeclarationData");
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
        private bool _isOption1Visible;
        public bool IsOption1Visible
        {
            get
            {
                return _isOption1Visible;
            }
            set
            {
                if (_isOption1Visible == value) return;

                _isOption1Visible = value;
                RaisePropertyChanged("IsOption1Visible");
            }
        }
        private bool _isOption2Visible;
        public bool IsOption2Visible
        {
            get
            {
                return _isOption2Visible;
            }
            set
            {
                if (_isOption2Visible == value) return;

                _isOption2Visible = value;
                RaisePropertyChanged("IsOption2Visible");
            }
        }
        private bool _IsPermitOption1Visible = false;
        public bool IsPermitOption1Visible
        {
            get
            {
                return _IsPermitOption1Visible;
            }
            set
            {
                if (_IsPermitOption1Visible == value) return;

                _IsPermitOption1Visible = value;
                RaisePropertyChanged("IsPermitOption1Visible");
            }
        }

        private bool _IsPermitOption2Visible = false;
        public bool IsPermitOption2Visible
        {
            get
            {
                return _IsPermitOption2Visible;
            }
            set
            {
                if (_IsPermitOption2Visible == value) return;

                _IsPermitOption2Visible = value;
                RaisePropertyChanged("IsPermitOption2Visible");
            }
        }

        private bool _IsPermitTypesVisible = false;
        public bool IsPermitTypesVisible
        {
            get
            {
                return _IsPermitTypesVisible;
            }
            set
            {
                if (_IsPermitTypesVisible == value) return;

                _IsPermitTypesVisible = value;
                RaisePropertyChanged("IsPermitTypesVisible");
            }
        }


        private TINDeregistrationModel _selectedOutletOption;
        public TINDeregistrationModel SelectedOutletOption
        {
            get
            {
                return _selectedOutletOption;
            }
            set
            {
                //if (_selectedOutletOption == value) return;

                if (value != null)
                {
                    _selectedOutletOption = value;
                    if (TinDeregistrationData != null)
                    {
                        if (_selectedOutletOption != null && _selectedOutletOption.OutletOptionIndex != null)
                        {
                            if (TinDeregistrationData.ADregOpt == null)
                            {
                                TinDeregistrationData.ADregOpt = _selectedOutletOption.OutletOptionIndex;
                            }
                            else
                            {
                                TinDeregistrationData.ADregOpt = string.Empty;
                                TinDeregistrationData.ADregOpt = _selectedOutletOption.OutletOptionIndex;
                            }
                        }
                        PopulateAttachmentsListViewTemplate();
                    }
                    RaisePropertyChanged("SelectedOutletOption");
                }
                //else
                //{
                //    _selectedOutletOption = null;
                //}
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
                RaisePropertyChanged("AttachmentSize");
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
                RaisePropertyChanged("TotalAttachmentSize");
            }
        }

        private bool _isName1Visible = false;
        public bool IsName1Visible
        {
            get
            {
                return _isName1Visible;
            }
            set
            {
                if (_isName1Visible == value) return;

                _isName1Visible = value;
                RaisePropertyChanged("IsName1Visible");
            }
        }

        //3102410588
        private string _firstNameLbl { get; set; } = AppResources.ZZZVATRFirstName;
        public string FirstNameLbl
        {
            get
            {
                return _firstNameLbl;
            }
            set
            {
                if (_firstNameLbl == value) return;

                _firstNameLbl = value;
                RaisePropertyChanged("FirstNameLbl");
            }
        }

        private string _surnameNameLbl { get; set; } = AppResources.TinDeregistrationSurName;
        public string SurnameNameLbl
        {
            get
            {
                return _surnameNameLbl;
            }
            set
            {
                if (_surnameNameLbl == value) return;

                _surnameNameLbl = value;
                RaisePropertyChanged("SurnameNameLbl");
            }
        }

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
                RaisePropertyChanged("AttachmentName");
            }
        }


        private TinDeregestrationAttachmentsModel _selectedAttachment { get; set; }
        public TinDeregestrationAttachmentsModel SelectedAttachment
        {
            get
            {
                return _selectedAttachment;
            }
            set
            {
                if (_selectedAttachment == value) return;

                _selectedAttachment = value;
                RaisePropertyChanged("SelectedAttachment");
            }
        }
        public List<VATDeregistrationSummaryModel> _vatDeregistrationSummaryDeclarationData { get; set; }
        public List<VATDeregistrationSummaryModel> VATDeregistrationSummaryDeclarationData
        {
            get
            {
                return _vatDeregistrationSummaryDeclarationData;
            }
            set
            {
                if (_vatDeregistrationSummaryDeclarationData == value)
                {
                    return;
                }
                _vatDeregistrationSummaryDeclarationData = value;
                RaisePropertyChanged("VATDeregistrationSummaryDeclarationData");
            }
        }

        #endregion


        #region New Properties

        private TinDeregistrationResponseModel _tinDeregistrationData { get; set; }
        public TinDeregistrationResponseModel TinDeregistrationData
        {
            get
            {
                return _tinDeregistrationData;
            }
            set
            {
                if (_tinDeregistrationData == value) return;


                _tinDeregistrationData = value;
                if (IDTypeDataModel == null)
                {
                    IDTypeDataModel = new VATSignUpD();
                }
                SelectedIdtype = value.AIdType;
                SelectedIdNumber = value.AIdNo;
                TINNumber = value.ANm2;
                IDTypeDataModel.Name1 = value.ANm3;
                FirstNameFromIdType = value.ANm3;
                IDTypeDataModel.Name2 = value.ANm4;
                IDTypeDataModel.FatherName = value.ANm5;
                IDTypeDataModel.GrandfatherName = value.ANm6;
                IDTypeDataModel.FamilyName = value.ANm7;


                //SelectedOutletOptionIndex = OutletDecisionOptions.IndexOf(_selectedOutletOption as TINDeregistrationModel);
                RaisePropertyChanged("TinDeregistrationData");

            }
        }

        private TinDeregistrationReasonSetDataModel _tinDeregistrationReasonSetData { get; set; }
        public TinDeregistrationReasonSetDataModel TinDeregistrationReasonSetData
        {
            get
            {
                return _tinDeregistrationReasonSetData;
            }
            set
            {
                if (_tinDeregistrationReasonSetData == value) return;

                _tinDeregistrationReasonSetData = value;
                //SelectedOutletOptionIndex = OutletDecisionOptions.IndexOf(_selectedOutletOption as TINDeregistrationModel);
                RaisePropertyChanged("TinDeregistrationReasonSetData");
            }
        }

        private TinDeregReasonSetResult _selectedReason { get; set; }
        public TinDeregReasonSetResult SelectedReason
        {
            get
            {
                return _selectedReason;
            }
            set
            {
                if (_selectedReason == value) return;

                _selectedReason = value;
                //if (value != null)
                //    IsOutletDecisionOptionsLVVisible = true;
                //else
                //    IsOutletDecisionOptionsLVVisible = false;
                RaisePropertyChanged("SelectedReason");
            }
        }

        private List<Attachment> _tinDeregAttachmentList;
        public List<Attachment> TinDeregAttachmentList
        {
            get
            {
                return _tinDeregAttachmentList;
            }
            set
            {
                if (_tinDeregAttachmentList == value)
                {
                    return;
                }

                _tinDeregAttachmentList = value;
                RaisePropertyChanged("TinDeregAttachmentList");
            }
        }

        private string _selectedIdtype { get; set; }
        public string SelectedIdtype
        {
            get
            {
                return _selectedIdtype;
            }
            set
            {
                if (_selectedIdtype == value) return;


                _selectedIdtype = value;
                RaisePropertyChanged("SelectedIdtype");
            }
        }

        private string _firstNameFromIdType { get; set; }
        public string FirstNameFromIdType
        {
            get
            {
                return _firstNameFromIdType;
            }
            set
            {
                if (_firstNameFromIdType == value) return;


                _firstNameFromIdType = value;
                RaisePropertyChanged("FirstNameFromIdType");
            }
        }

        private string _selectedIdNumber { get; set; }
        public string SelectedIdNumber
        {
            get
            {
                return _selectedIdNumber;
            }

            set
            {
                if (_selectedIdNumber == value) return;

                _selectedIdNumber = value;
                RaisePropertyChanged("SelectedIdNumber");
            }
        }
        private string _IndselectedIdNumber { get; set; }
        public string IndSelectedIdNumber
        {
            get
            {
                return _IndselectedIdNumber;
            }

            set
            {
                if (_IndselectedIdNumber == value) return;

                _IndselectedIdNumber = value;
                RaisePropertyChanged("IndSelectedIdNumber");
            }
        }
        private bool _isDobVisible = true;
        public bool IsDobVisible
        {
            get
            {
                return _isDobVisible;
            }

            set
            {
                if (_isDobVisible == value) return;

                _isDobVisible = value;
                RaisePropertyChanged("IsDobVisible");
            }
        }

        private string _selectedIDTypeCode { get; set; }
        public string SelectedIDTypeCode
        {
            get
            {
                return _selectedIDTypeCode;
            }

            set
            {
                if (_selectedIDTypeCode == value) return;

                _selectedIDTypeCode = value;
                if (_selectedIDTypeCode != null)
                {
                    if (_selectedIDTypeCode == "ZS0005")
                    {
                        FathersNameText.IsVisible = false;
                        // SurnameText.IsVisible = false;
                        GrandFathersNameText.IsVisible = false;
                        FamilyNameText.IsVisible = false;
                        IsDobVisible = false;
                    }
                    else
                    {
                        FathersNameText.IsVisible = true;
                        SurnameText.IsVisible = true;
                        GrandFathersNameText.IsVisible = true;
                        FamilyNameText.IsVisible = true;
                        //if (SelectedReason.ReasonCd == "6" && SelectedOutletOption.ActiveOutletDecisionOptions.Equals(AppResources.TinDeregistrationTransferAllOutletsToSingle))
                        //{
                        //    IsDobVisible = false;
                        //}
                        //else
                        {
                            IsDobVisible = true;
                        }
                    }
                }
                else
                {
                    //if (SelectedReason.ReasonCd == "6" && SelectedOutletOption.ActiveOutletDecisionOptions.Equals(AppResources.TinDeregistrationTransferAllOutletsToSingle))
                    //{
                    //    IsDobVisible = false;
                    //}
                    //else
                    {
                        IsDobVisible = true;
                    }
                }
                RaisePropertyChanged("SelectedIDTypeCode");
            }
        }


        public List<TinDeregReasonSetResult> _tinDeregReasons { get; set; }
        public List<TinDeregReasonSetResult> TinDeregReasons
        {
            get
            {
                return _tinDeregReasons;
            }

            set
            {
                if (_tinDeregReasons == value) return;

                _tinDeregReasons = value;
                RaisePropertyChanged("VATDeregistrationSummaryDeclarationData");
            }
        }
        private bool _isDetailsFieldEnabled;
        public bool IsDetailsFieldEnabled
        {
            get => _isDetailsFieldEnabled;
            set
            {
                if (_isDetailsFieldEnabled == value) return;

                _isDetailsFieldEnabled = value;
                RaisePropertyChanged(nameof(IsDetailsFieldEnabled));
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

                try
                {
                    if (PickerModel != null && !string.IsNullOrEmpty(PickerModel.SelectedValue))
                    {
                        if (PickerModel.PickerId == "reasonPicker")
                        {
                            Task.Run(async () =>
                            {
                                await ResetTINDeRegistrationObject();
                                ClearData();
                                string tempSelectedReason = PickerModel.SelectedValue;
                                permitThirdOptionReason = PickerModel.SelectedValue;

                                if (tempSelectedReason != string.Empty)
                                {
                                    SelectedReason = TinDeregReasons.Where(m => m.ReasonDesc == PickerModel.SelectedValue).FirstOrDefault();
                                    TinDeregistrationData.ADregReason = SelectedReason.ReasonCd;
                                    TinDeregistrationData.ADeregSelectedReasonValue = SelectedReason.ReasonDesc;

                                    List<PermitSetResult> allPermitTypes = new List<PermitSetResult>(TinDeregistrationData.PermitSet.Results);

                                    foreach (OutletSetResult outletInfo in AllOutlets)
                                    {
                                        if (SelectedOutletOption.Equals(AppResources.TinDeregistrationCloseAllOutlets) || SelectedOutletOption.Equals(AppResources.TinDeregistrationCloseOutletsIndividually))
                                        {
                                            outletInfo.ReasonDescription = AppResources.TinDeregistrationClosed;

                                        }
                                        else
                                        {
                                            outletInfo.ReasonDescription = AppResources.TinDeregistrationTransfer;

                                        }
                                        //outletInfo.ReasonDescription = SelectedReason.ReasonDesc;
                                        outletInfo.PermitTypes = new List<PermitSetResult>();
                                        var tempPermitTypes = new List<PermitSetResult>();
                                        foreach (PermitSetResult permitInfo in allPermitTypes)
                                        {

                                            if (permitInfo.APermitDregRsnTb == null)
                                                permitInfo.APermitDregRsnTb = string.Empty;

                                            if (permitInfo.APermitIdNoTb == null)
                                                permitInfo.APermitIdNoTb = "";

                                            if (permitInfo.APermitTransTinTb == null)
                                                permitInfo.APermitTransTinTb = "";

                                            if (permitInfo.APermitOutletnoTb == outletInfo.AOutletNoTb)
                                            {
                                                if (SelectedOutletOptionIndex == 0 || SelectedOutletOptionIndex == 2)
                                                {
                                                    permitInfo.ReasonDescription = AppResources.TinDeregistrationClosed;

                                                }
                                                else
                                                {
                                                    permitInfo.ReasonDescription = AppResources.TinDeregistrationTransfer;

                                                }
                                                //permitInfo.ReasonDescription = SelectedReason.ReasonDesc;

                                                //if (outletInfo.PermitTypes == null)
                                                //    outletInfo.PermitTypes = new List<PermitSetResult>();


                                                tempPermitTypes.Add(permitInfo);
                                            }
                                        }
                                        outletInfo.PermitTypes = tempPermitTypes;
                                    }
                                    DateField.IsVisible = true;

                                }
                                else
                                {
                                    DateField.IsVisible = false;
                                    SelectedReason.ReasonDesc = string.Empty;
                                    SelectedReason.ReasonCd = string.Empty;

                                }
                                AddOutletDecisionOptions();
                                PopulateAttachmentsListViewTemplate();

                            });


                        }
                        else if (PickerModel.PickerId == "idTypePicker")
                        {
                            SelectedIdtype = string.Empty;
                            if (SelectedIdtype != PickerModel.SelectedValue)
                            {
                                SelectedIdNumber = string.Empty;
                                TINNumber = string.Empty;
                                PickerDOBDateDisplay = string.Empty;
                                FirstNameFromIdType = string.Empty;
                                IDTypeDataModel = new VATSignUpD();
                                //
                                SelectedDob = string.Empty;
                            }

                            SelectedIdtype = PickerModel.SelectedValue;
                            IBANType idType = IBANTypesList.Where(m => m.Text == PickerModel.SelectedValue).FirstOrDefault();
                            SelectedIDTypeCode = idType.key;

                            FirstNameLbl = AppResources.ZZZVATRFirstName;
                            SurnameNameLbl = AppResources.TinDeregistrationSurName;
                            IsName1Visible = false;

                            if (SelectedIdtype == AppResources.TinDeregistrationNationalID)
                            {
                                IsDobVisible = true;
                                NationalTypeSelected();
                            }
                            else if (SelectedIdtype == AppResources.TinDeregistrationCompanyID)
                            {
                                IsDobVisible = false;
                                IsName1Visible = true;
                                CompanyIdTypeSelected();
                            }
                            else if (SelectedIdtype == AppResources.TinDeregistrationIQAMANumber)
                            {
                                IsDobVisible = true;
                                IqamaTypeSelected();
                            }
                            else if (SelectedIdtype == AppResources.TinDeregistrationGCCID)
                            {
                                SelectedIdNumber = string.Empty;
                                SelectedDob = string.Empty;
                                IsDobVisible = false;
                                GCCIdTypeSelected();
                            }
                        }
                        else if (PickerModel.PickerId == "permitTypeReasonPicker")
                        {
                            string tempSelectedReason = PickerModel.SelectedValue;

                            //SelectedOutletForCloseTranser.PermitTypes = new List<PermitSetResult>(SelectedOutletForCloseTranser.PermitTypes.ToList().Select(
                            //  x =>
                            //  {
                            //      if (x.APermitNoTb == selectedAPermitReason)
                            //      {
                            //          x.APermitDisplayReason = tempSelectedReason;

                            //          if (tempSelectedReason == AppResources.TinDeregistrationClosed)
                            //          {
                            //              x.APermitDregRsnTb = "1";
                            //              IsDetailsFieldEnabled = false;
                            //          }
                            //          else
                            //          {
                            //              x.APermitDregRsnTb = "3";
                            //              IsDetailsFieldEnabled = true;
                            //          }
                            //      }

                            //      return x;
                            //  }
                            //  ).ToList());
                            SelectedOutletForCloseTranser.PermitTypes = new List<PermitSetResult>(SelectedOutletForCloseTranser.PermitTypes.ToList());

                            foreach (var items in SelectedOutletForCloseTranser.PermitTypes)
                            {
                                if (items.APermitNoTb == selectedAPermitReason)
                                {
                                    items.APermitDisplayReason = tempSelectedReason;
                                    if (tempSelectedReason == AppResources.TinDeregistrationClosed)
                                    {
                                        items.APermitDregRsnTb = "1";
                                        IsDetailsFieldEnabled = false;
                                    }
                                    else
                                    {
                                        items.APermitDregRsnTb = "3";
                                        IsDetailsFieldEnabled = true;
                                    }

                                }
                            }

                            if (PickerModel.SelectedValue == AppResources.TinDeregistrationClosed)
                            {
                                IsOutletTranferOutletGridVisible = false;
                                IsDetailsFieldEnabled = false;
                            }
                            else
                            {
                                IsOutletTranferOutletGridVisible = true;
                                IsDetailsFieldEnabled = true;
                            }
                        }
                        else if (PickerModel.PickerId == "permitIdTypePicker")
                        {
                            //SelectedOutletForCloseTranser.PermitTypes = new List<PermitSetResult>(SelectedOutletForCloseTranser.PermitTypes.ToList().Select(
                            //  x =>
                            //  {
                            //      if (x.APermitNoTb == tempIdTypePermitSetResult.APermitNoTb)
                            //      {
                            //          //x.APermitIdTypeTb

                            //          x.APermitTransTinTb = string.Empty;
                            //          x.APermitIdNoTb = string.Empty;

                            //          if (PickerModel.SelectedValue == AppResources.TinDeregistrationNationalID)
                            //          {
                            //              x.APermitIdTypeTb = "ZS0001";
                            //          }
                            //          else if (PickerModel.SelectedValue == AppResources.TinDeregistrationCompanyID)
                            //          {
                            //              x.APermitIdTypeTb = "ZS0005";
                            //          }
                            //          else if (PickerModel.SelectedValue == AppResources.TinDeregistrationIQAMANumber)
                            //          {
                            //              x.APermitIdTypeTb = "ZS0002";
                            //          }
                            //          else if (PickerModel.SelectedValue == AppResources.TinDeregistrationGCCID)
                            //          {
                            //              x.APermitIdTypeTb = "ZS0003";
                            //          }
                            //      }
                            //      return x;
                            //  }
                            //  ).ToList());

                            foreach (var Item in SelectedOutletForCloseTranser.PermitTypes.ToList())
                            {
                                if (Item.APermitNoTb == tempIdTypePermitSetResult.APermitNoTb)
                                {
                                    Item.APermitTransTinTb = string.Empty;
                                    Item.APermitIdNoTb = string.Empty;
                                    Item.APermitNm3Tb = string.Empty;
                                    Item.APermitNm4Tb = string.Empty;
                                    Item.APermitNm5Tb = string.Empty;
                                    Item.APermitNm6Tb = string.Empty;
                                    Item.APermitNm7Tb = string.Empty;
                                    Item.APermitDeregDisplayDobDate = string.Empty;
                                    PkrDBO = string.Empty;


                                    if (PickerModel.SelectedValue == AppResources.TinDeregistrationNationalID)
                                    {
                                        Item.APermitIdTypeTb = "ZS0001";
                                    }
                                    else if (PickerModel.SelectedValue == AppResources.TinDeregistrationCompanyID)
                                    {
                                        Item.APermitIdTypeTb = "ZS0005";
                                    }
                                    else if (PickerModel.SelectedValue == AppResources.TinDeregistrationIQAMANumber)
                                    {
                                        Item.APermitIdTypeTb = "ZS0002";
                                    }
                                    else if (PickerModel.SelectedValue == AppResources.TinDeregistrationGCCID)
                                    {
                                        Item.APermitIdTypeTb = "ZS0003";
                                    }
                                }
                            }



                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); Console.WriteLine(ex.ToString());
                }

                RaisePropertyChanged("PickerModel");
            }
        }

        private DateTime _singleOutletDeregistrationDate = DateTime.Now;
        public DateTime SingleOutletDeregistrationDate
        {
            get
            {
                return _singleOutletDeregistrationDate;
            }
            set
            {
                if (_singleOutletDeregistrationDate == value) return;

                _singleOutletDeregistrationDate = value;

                SelectedOutletForCloseTranser.PermitTypes = new List<PermitSetResult>(SelectedOutletForCloseTranser.PermitTypes.ToList().Select(
                    x =>
                    {
                        if (x.APermitNoTb == selectedAPermitOutletnoTb)
                        {
                            x.APermitEffDtTb = ConvertDateFormat(SingleOutletDeregistrationDate);
                            x.APermitEffDtCTb = "G";
                            x.APermitEffDtHTb = SingleOutletDeregistrationDate.ToString("yyyy/MM/dd");
                            x.APermitDeregDisplayDate = SingleOutletDeregistrationDate.ToString("dd/MM/yyyy");
                            if (Convert.ToDateTime(x.APermitValfrDtHTb) > SingleOutletDeregistrationDate)
                            {
                                _dialogService.ShowMessage(AppResources.TinDeregistrationDateValidationMessage, AppResources.Information);
                                x.APermitDeregDisplayDate = string.Empty;
                            }
                        }
                        return x;
                    }
                    ).ToList());

                RaisePropertyChanged("SingleOutletDeregistrationDate");
            }
        }

        private DateTime _permitDob = DateTime.Now;
        public DateTime PermitDob
        {
            get
            {
                return _permitDob;
            }
            set
            {
                if (_permitDob == value) return;

                _permitDob = value;

                SelectedOutletForCloseTranser.PermitTypes = new List<PermitSetResult>(SelectedOutletForCloseTranser.PermitTypes.ToList().Select(
                    x =>
                    {
                        if (x.APermitNoTb == selectedAPermitOutletnoTb)
                        {
                            if (PermitDob != null)
                            {
                                x.APermitDobTb = ConvertDateFormat(PermitDob);
                                x.APermitDobCTb = "G";
                                x.APermitDobHTb = PermitDob.ToString("yyyyMMdd");
                                x.APermitDeregDisplayDobDate = PermitDob.ToString("yyyy/MM/dd");

                                PkrDBO = PermitDob.ToString("yyyy/MM/dd");
                                if (x.PermitIdTypeName == AppResources.TinDeregistrationNationalID || x.PermitIdTypeName == AppResources.TinDeregistrationIQAMANumber)
                                {
                                    if (!string.IsNullOrWhiteSpace(x.APermitIdNoTb))
                                    {
                                        ValidateIDNumberForIndiviualPermit();
                                    }
                                }
                            }
                        }
                        return x;
                    }
                    ).ToList());

                RaisePropertyChanged("PermitDob");
            }
        }



        private string _singleDeregistrationDate;
        public string SingleDeregistrationDate
        {
            get
            {
                return _singleDeregistrationDate;
            }
            set
            {
                if (_singleDeregistrationDate == value) return;

                _singleDeregistrationDate = value;
                if (TinDeregistrationData.ADregOpt == "3")
                {
                    if (!string.IsNullOrWhiteSpace(_singleDeregistrationDate))
                    {
                        if (SelectedOutletForCloseTranser.PermitTypes != null)
                        {
                            SelectedOutletForCloseTranser.PermitTypes = new List<PermitSetResult>(SelectedOutletForCloseTranser.PermitTypes.ToList().Select(
                            x =>
                            {

                                x.APermitEffDtTb = _singleDeregistrationDate == null ? "" : ConvertDateFormat(_singleDeregistrationDate);
                                x.APermitEffDtCTb = "G";
                                x.APermitEffDtHTb = _singleDeregistrationDate == null ? "" : _singleDeregistrationDate;//.ToString("yyyyMMdd");
                                x.APermitDeregDisplayDate = _singleDeregistrationDate == null ? "" : _singleDeregistrationDate;//.ToString("dd MMM yyyy");
                                if (Convert.ToDateTime(x.APermitValfrDtHTb) > Convert.ToDateTime(SingleDeregistrationDate))
                                {
                                    _dialogService.ShowMessage(AppResources.TinDeregistrationDateValidationMessage, AppResources.Information);
                                    x.APermitDeregDisplayDate = string.Empty;
                                }
                                return x;
                            }
                            ).ToList());
                        }
                    }
                }
                RaisePropertyChanged("SingleDeregistrationDate");
            }
        }

        private DateTime _deregistrationDate = DateTime.Now;
        public DateTime DeregistrationDate
        {
            get
            {
                return _deregistrationDate;
            }
            set
            {
                if (_deregistrationDate == value) return;

                _deregistrationDate = value;
                RaisePropertyChanged("DeregistrationDate");
            }
        }

        private string _submissionDate = DateTime.Today.ToString("yyyy/MM/dd");
        public string SubmissionDate
        {
            get
            {
                return _submissionDate;
            }
            set
            {
                if (_submissionDate == value) return;

                _submissionDate = value;
                RaisePropertyChanged("SubmissionDate");
            }
        }

        private string _selectedDob = string.Empty;
        public string SelectedDob
        {
            get
            {
                return _selectedDob;
            }
            set
            {
                if (_selectedDob == value) return;

                _selectedDob = value;
                RaisePropertyChanged("SelectedDob");
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
                RaisePropertyChanged("FrameIDError");
            }
        }

        private bool _frameTinError = false;
        public bool FrameTinError
        {
            get
            {
                return _frameTinError;
            }
            set
            {
                if (_frameTinError == value) return;

                _frameTinError = value;
                RaisePropertyChanged("FrameTinError");
            }
        }

        private bool _outletEditIsVisible = false;
        public bool outletEditIsVisible
        {
            get
            {
                return _outletEditIsVisible;
            }
            set
            {
                if (_outletEditIsVisible == value) return;

                _outletEditIsVisible = value;
                RaisePropertyChanged("outletEditIsVisible");
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
                if (_isLoading == value) return;

                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }
        private VATSignUpD _iDTypeDataModel = null;
        public VATSignUpD IDTypeDataModel
        {
            get
            {
                return _iDTypeDataModel;
            }

            set
            {
                if (_iDTypeDataModel == value) return;


                _iDTypeDataModel = value;
                RaisePropertyChanged("IDTypeDataModel");
            }
        }
        private FieldValidations _tinText { get; set; }
        public FieldValidations TinText
        {
            get
            {
                return _tinText;
            }
            set
            {
                if (_tinText == value) return;

                _tinText = value;
                RaisePropertyChanged("TinText");
            }
        }
        private List<Attachment> _attachmentTypeList { get; set; }
        public List<Attachment> AttachmentTypeList
        {
            get
            {
                return _attachmentTypeList;
            }
            set
            {
                if (_attachmentTypeList == value) return;

                _attachmentTypeList = value;
                RaisePropertyChanged("AttachmentTypeList");
            }
        }
        private FieldValidations _DateField { get; set; }
        public FieldValidations DateField
        {
            get
            {
                return _DateField;
            }
            set
            {
                if (_DateField == value) return;

                _DateField = value;
                RaisePropertyChanged("DateField");
            }
        }

        private FieldValidations _idTypeText { get; set; }
        public FieldValidations IdTypeText
        {
            get
            {
                return _idTypeText;
            }
            set
            {
                if (_idTypeText == value) return;

                _idTypeText = value;
                RaisePropertyChanged("IdTypeText");
            }
        }

        private FieldValidations _idNumberText { get; set; }
        public FieldValidations IdNumberText
        {
            get
            {
                return _idNumberText;
            }
            set
            {
                if (_idNumberText == value) return;

                _idNumberText = value;
                RaisePropertyChanged("IdNumberText");
            }
        }

        private FieldValidations _dobText { get; set; }
        public FieldValidations DobText
        {
            get
            {
                return _dobText;
            }
            set
            {
                if (_dobText == value) return;

                _dobText = value;
                RaisePropertyChanged("DobText");
            }
        }

        private FieldValidations _firstNameText { get; set; }
        public FieldValidations FirstNameText
        {
            get
            {
                return _firstNameText;
            }
            set
            {
                if (_firstNameText == value) return;

                _firstNameText = value;
                RaisePropertyChanged("FirstNameText");
            }
        }

        private FieldValidations _surnameText { get; set; }
        public FieldValidations SurnameText
        {
            get
            {
                return _surnameText;
            }
            set
            {
                if (_surnameText == value) return;

                _surnameText = value;
                RaisePropertyChanged("SurnameText");
            }
        }

        private FieldValidations _fathersNameText { get; set; }
        public FieldValidations FathersNameText
        {
            get
            {
                return _fathersNameText;
            }
            set
            {
                if (_fathersNameText == value) return;

                _fathersNameText = value;
                RaisePropertyChanged("FathersNameText");
            }
        }

        private FieldValidations _grandFathersNameText { get; set; }
        public FieldValidations GrandFathersNameText
        {
            get
            {
                return _grandFathersNameText;
            }
            set
            {
                if (_grandFathersNameText == value) return;

                _grandFathersNameText = value;
                RaisePropertyChanged("GrandFathersNameText");
            }
        }

        public List<Attachment> tinDeregAttachmentsListViewData { get; set; }

        public List<Attachment> TinDeregAttachmentsListViewData
        {
            get { return tinDeregAttachmentsListViewData; }

            set
            {
                if (tinDeregAttachmentsListViewData == value)
                {
                    return;
                }

                tinDeregAttachmentsListViewData = value;
                RaisePropertyChanged("TinDeregAttachmentsListViewData");
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

        private FieldValidations _familyNameText { get; set; }
        public FieldValidations FamilyNameText
        {
            get
            {
                return _familyNameText;
            }
            set
            {
                if (_familyNameText == value) return;

                _familyNameText = value;
                RaisePropertyChanged("FamilyNameText");
            }
        }

        private FieldValidations _name1Text { get; set; }
        public FieldValidations Name1Text
        {
            get
            {
                return _name1Text;
            }
            set
            {
                if (_name1Text == value) return;

                _name1Text = value;
                RaisePropertyChanged("Name1Text");
            }
        }

        private FieldValidations _name2Text { get; set; }
        public FieldValidations Name2Text
        {
            get
            {
                return _name2Text;
            }
            set
            {
                if (_name2Text == value) return;

                _name2Text = value;
                RaisePropertyChanged("Name2Text");
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
                if (_iBANTypesList == value) return;

                if (_iBANTypesList != null && _iBANTypesList.Count != 0)
                {

                    //if ((App.ICRStatus == "E0045" || App.ICRStatus == "E0006") && IsAmendClicked == false)
                    //{
                    //    IsEnableIBANType = false;
                    //}
                    //else
                    //{
                    //    IsEnableIBANType = true;
                    //}
                }
                else
                {
                    //IsEnableIBANType = false;
                }
                RaisePropertyChanged("IBANTypesList");
            }
        }

        public OutletSetResult _selectedOutletForCloseTranser { get; set; }
        public OutletSetResult SelectedOutletForCloseTranser
        {
            get
            {
                return _selectedOutletForCloseTranser;
            }

            set
            {
                if (_selectedOutletForCloseTranser == value) return;

                _selectedOutletForCloseTranser = value;
                RaisePropertyChanged("SelectedOutletForCloseTranser");
            }
        }

        public List<OutletSetResult> _allOutlets { get; set; }
        public List<OutletSetResult> AllOutlets
        {
            get
            {
                return _allOutlets;
            }
            set
            {
                if (_allOutlets == value) return;

                _allOutlets = value;
                RaisePropertyChanged("AllOutlets");
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
                RaisePropertyChanged("VoidIsVisible");
            }
        }

        public string _uploadedAttachmentFileType = string.Empty;
        public string UploadedAttachmentFileType
        {
            get
            {
                return _uploadedAttachmentFileType;
            }
            set
            {
                if (_uploadedAttachmentFileType == value) return;

                _uploadedAttachmentFileType = value;
                RaisePropertyChanged("UploadedAttachmentFileType");
            }
        }


        private List<String> _ListOfActionButtonsApplicable;
        public List<String> ListOfActionButtonsApplicable
        {
            get
            {
                return
                    _ListOfActionButtonsApplicable;
            }
            set
            {
                if (_ListOfActionButtonsApplicable == value) return;

                _ListOfActionButtonsApplicable = value;

                RaisePropertyChanged("ListOfActionButtonsApplicable");
            }
        }

        public void CompanyIdTypeSelected()
        {
            TinText.IsMandatory = false;
            TinText.IsVisible = true;
            TinText.IsEditable = true;

            IdTypeText.IsMandatory = true;
            IdTypeText.IsVisible = true;
            IdTypeText.IsEditable = true;

            IdNumberText.IsMandatory = true;
            IdNumberText.IsVisible = true;
            IdNumberText.IsEditable = true;

            DobText.IsMandatory = false;
            DobText.IsVisible = false;
            DobText.IsEditable = false;

            FirstNameText.IsMandatory = false;
            FirstNameText.IsVisible = false;
            FirstNameText.IsEditable = false;

            SurnameText.IsMandatory = false;
            SurnameText.IsVisible = false;
            SurnameText.IsEditable = false;

            FathersNameText.IsMandatory = false;
            FathersNameText.IsVisible = false;
            FathersNameText.IsEditable = false;

            GrandFathersNameText.IsMandatory = false;
            GrandFathersNameText.IsVisible = false;
            GrandFathersNameText.IsEditable = false;

            FamilyNameText.IsMandatory = false;
            FamilyNameText.IsVisible = false;
            FamilyNameText.IsEditable = false;

            Name1Text.IsMandatory = false;
            Name1Text.IsVisible = true;
            Name1Text.IsEditable = false;

            Name2Text.IsMandatory = false;
            Name2Text.IsVisible = true;
            Name2Text.IsEditable = false;
        }

        public void NationalTypeSelected()
        {
            TinText.IsMandatory = false;
            TinText.IsVisible = true;
            TinText.IsEditable = true;

            IdTypeText.IsMandatory = true;
            IdTypeText.IsVisible = true;
            IdTypeText.IsEditable = true;

            IdNumberText.IsMandatory = true;
            IdNumberText.IsVisible = true;
            IdNumberText.IsEditable = true;

            DobText.IsMandatory = true;
            DobText.IsVisible = true;
            DobText.IsEditable = true;//Non Editable after validation

            FirstNameText.IsMandatory = false;
            FirstNameText.IsVisible = true;
            FirstNameText.IsEditable = false;

            SurnameText.IsMandatory = false;
            SurnameText.IsVisible = true;
            SurnameText.IsEditable = false;

            FathersNameText.IsMandatory = false;
            FathersNameText.IsVisible = true;
            FathersNameText.IsEditable = false;

            GrandFathersNameText.IsMandatory = false;
            GrandFathersNameText.IsVisible = true;
            GrandFathersNameText.IsEditable = false;

            FamilyNameText.IsMandatory = false;
            FamilyNameText.IsVisible = true;
            FamilyNameText.IsEditable = false;

            Name1Text.IsMandatory = false;
            Name1Text.IsVisible = false;
            Name1Text.IsEditable = false;

            Name2Text.IsMandatory = false;
            Name2Text.IsVisible = false;
            Name2Text.IsEditable = false;
        }

        public void GCCIdTypeSelected()
        {
            TinText.IsMandatory = false;
            TinText.IsVisible = true;
            TinText.IsEditable = true;

            IdTypeText.IsMandatory = true;
            IdTypeText.IsVisible = true;
            IdTypeText.IsEditable = true;

            IdNumberText.IsMandatory = true;
            IdNumberText.IsVisible = true;
            IdNumberText.IsEditable = true;

            DobText.IsMandatory = true;
            DobText.IsVisible = true;
            DobText.IsEditable = true;

            FirstNameText.IsMandatory = true;
            FirstNameText.IsVisible = true;
            FirstNameText.IsEditable = true;

            SurnameText.IsMandatory = true;
            SurnameText.IsVisible = true;
            SurnameText.IsEditable = true;

            FathersNameText.IsMandatory = false;
            FathersNameText.IsVisible = true;
            FathersNameText.IsEditable = true;

            GrandFathersNameText.IsMandatory = false;
            GrandFathersNameText.IsVisible = true;
            GrandFathersNameText.IsEditable = true;

            FamilyNameText.IsMandatory = false;
            FamilyNameText.IsVisible = true;
            FamilyNameText.IsEditable = true;

            Name1Text.IsMandatory = false;
            Name1Text.IsVisible = false;
            Name1Text.IsEditable = false;

            Name2Text.IsMandatory = false;
            Name2Text.IsVisible = false;
            Name2Text.IsEditable = false;
        }

        public void IqamaTypeSelected()
        {
            TinText.IsMandatory = false;
            TinText.IsVisible = true;
            TinText.IsEditable = true;

            IdTypeText.IsMandatory = true;
            IdTypeText.IsVisible = true;
            IdTypeText.IsEditable = true;

            IdNumberText.IsMandatory = true;
            IdNumberText.IsVisible = true;
            IdNumberText.IsEditable = true;

            DobText.IsMandatory = true;
            DobText.IsVisible = true;
            DobText.IsEditable = true; //Non Editable after validation

            FirstNameText.IsMandatory = false;
            FirstNameText.IsVisible = true;
            FirstNameText.IsEditable = false;

            SurnameText.IsMandatory = false;
            SurnameText.IsVisible = true;
            SurnameText.IsEditable = false;

            FathersNameText.IsMandatory = false;
            FathersNameText.IsVisible = true;
            FathersNameText.IsEditable = false;

            GrandFathersNameText.IsMandatory = false;
            GrandFathersNameText.IsVisible = true;
            GrandFathersNameText.IsEditable = false;

            FamilyNameText.IsMandatory = false;
            FamilyNameText.IsVisible = true;
            FamilyNameText.IsEditable = false;

            Name1Text.IsMandatory = false;
            Name1Text.IsVisible = false;
            Name1Text.IsEditable = false;

            Name2Text.IsMandatory = false;
            Name2Text.IsVisible = false;
            Name2Text.IsEditable = false;
        }

        public void PopulateIdTypeTypeFromList()
        {
            IBANTypesList = new List<IBANType>();
            List<IBANType> IBANTypesDummyList = new List<IBANType>();
            IBANType iBANType = new IBANType();
            iBANType.key = "ZS0001";
            iBANType.Text = AppResources.TinDeregistrationNationalID;
            IBANTypesDummyList.Add(iBANType);

            IBANType iBANType2 = new IBANType();
            iBANType2.key = "ZS0005";
            iBANType2.Text = AppResources.TinDeregistrationCompanyID;
            IBANTypesDummyList.Add(iBANType2);

            IBANType iBANType3 = new IBANType();
            iBANType3.key = "ZS0002";
            iBANType3.Text = AppResources.TinDeregistrationIQAMANumber;
            IBANTypesDummyList.Add(iBANType3);

            IBANType iBANType4 = new IBANType();
            iBANType4.key = "ZS0003";
            iBANType4.Text = AppResources.TinDeregistrationGCCID;
            IBANTypesDummyList.Add(iBANType4);

            IBANTypesList = IBANTypesDummyList;
        }


        public async void OnPermitTypeTinEntered(PermitSetResult permitSetResult)
        {
            List<string> idTypeData = new List<string>();

            foreach (IBANType iBANType in IBANTypesList)
            {
                idTypeData.Add(iBANType.Text);
            }

            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = idTypeData;
            genericPickerModel.PickerTitle = AppResources.ZZIDType;
            genericPickerModel.PickerId = "permitIdTypePicker";

            tempIdTypePermitSetResult = permitSetResult;

            await PopupNavigation.Instance.PushAsync(new PickerPageView(genericPickerModel));
        }

        public PermitSetResult tempIdTypePermitSetResult = new PermitSetResult();
        public async void OnPermitIdTypeClicked(PermitSetResult permitSetResult)
        {
            List<string> idTypeData = new List<string>();

            foreach (IBANType iBANType in IBANTypesList)
            {
                idTypeData.Add(iBANType.Text);
            }

            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = idTypeData;
            genericPickerModel.PickerTitle = AppResources.ZZIDType;
            genericPickerModel.PickerId = "permitIdTypePicker";

            tempIdTypePermitSetResult = permitSetResult;

            await PopupNavigation.Instance.PushAsync(new PickerPageView(genericPickerModel));
        }

        public async void OnIdTypeClicked()
        {
            List<string> idTypeData = new List<string>();

            foreach (IBANType iBANType in IBANTypesList)
            {
                idTypeData.Add(iBANType.Text);
            }

            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = idTypeData;
            genericPickerModel.PickerTitle = AppResources.ZZIDType;
            genericPickerModel.PickerId = "idTypePicker";

            await PopupNavigation.Instance.PushAsync(new PickerPageView(genericPickerModel));
        }

        #endregion

        public TINDeregistrationPageViewModel(INavigationService navigationService, IDialogService dialogService)
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

            GoBackBtnTapped = new Command(this.GoBackBtnClicked);
            ReasonContinueBtnTapped = new Command(this.ReasonContinueBtnClicked);
            OutletPermitPopupReasonContinueBtnTapped = new Command(this.OutletPermitPopupReasonContinueBtnClicked);
            OutletContinueBtnTapped = new Command(this.OutletContinueBtnClicked);
            AttachmentsContinueBtnTapped = new Command(this.AttachmentsContinueBtnClicked);
            DeclarationContinueBtnTapped = new Command(this.DeclarationContinueBtnClicked);
            SummaryContinueBtnTapped = new Command(this.SummaryContinueBtnClicked);
            OnTinRegisrtationReasonDateTapped = new Command(this.OnTinRegisrtationReasonDateClicked);
            OnTinDeregOutletDeregDatePickerTapped = new Command(this.OnTinDeregOutletDeregDatePickerClicked);

            //OnTinDeregOutletDeregDatePickerClicked
            OnTinRegistrationReasonTapped = new Command(this.OnTinRegisrtationReasonClicked);
            OnTinRegistrationDateTapped = new Command(this.OnTinRegistrationDateClicked);
            OnOutletPermitTypeDeRegisrtationReasonDateTapped = new Command<string>(this.OnOutletPermitTypeDeRegisrtationReasonDateClicked);
            OnOutletPermitTypeReasonTapped = new Command<string>(this.OnOutletPermitTypeReasonClicked);
            OnPermitDobTapped = new Command(this.OnPermitDobClicked);
            //
            TinDeregistrationModel = new TINDeregistrationModel();
            SelectedOutletOption = new TINDeregistrationModel();
            TinDeregistrationData = new TinDeregistrationResponseModel();
            TinDeregistrationReasonSetData = new TinDeregistrationReasonSetDataModel();
            OnPermitTypeReasonTapped = new Command(this.OnOutletPermitTypeDeRegisrtationReasonClicked);
            OnMoreClicked = new Command(this.OnMoreOptionClicked);


            TinText = new FieldValidations();
            IdTypeText = new FieldValidations();
            IdNumberText = new FieldValidations();
            DobText = new FieldValidations();
            FirstNameText = new FieldValidations();
            SurnameText = new FieldValidations();
            FathersNameText = new FieldValidations();
            FamilyNameText = new FieldValidations();
            GrandFathersNameText = new FieldValidations();
            Name1Text = new FieldValidations();
            Name2Text = new FieldValidations();
            DateField = new FieldValidations();
            IdTypeTapped = new Command(OnIdTypeClicked);
            PermitIdtypeTapped = new Command<PermitSetResult>(OnPermitIdTypeClicked);
            PermitTypeTinUnfocused = new Command<PermitSetResult>(OnPermitTypeTinEntered);

            //PermitIdtypeTapped
            //AddOutletDecisionOptions();
            //PopulateAttachmentsListViewTemplate();
            PopulateIdTypeTypeFromList();
            IsOption1Visible = false;
            IsOption2Visible = false;
            IsPermitOption1Visible = false;
            IsPermitOption2Visible = false;
            VoidIsVisible = false;
            EnableReasonView();
        }


        public void SetDefaultDate()
        {
            List<object> todaycollection = new List<object>();
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
            TodayDate = todaycollection;
            DefaultMonth = DateTime.Now.Date.Month;

            //TodayDateinHijri
            List<object> todaycollectionHijri = new List<object>();
            var calendar = new HijriCalendar();
            if (calendar.GetDayOfMonth(DateTime.Now.Date) < 10)
                todaycollectionHijri.Add("0" + calendar.GetDayOfMonth(DateTime.Now.Date).ToString());
            else
                todaycollectionHijri.Add(calendar.GetDayOfMonth(DateTime.Now.Date).ToString());
            if (calendar.GetMonth(DateTime.Now.Date) < 10)
                todaycollectionHijri.Add("0" + calendar.GetMonth(DateTime.Now.Date));
            else
                todaycollectionHijri.Add(calendar.GetMonth(DateTime.Now.Date).ToString());
            todaycollectionHijri.Add(calendar.GetYear(DateTime.Now.Date).ToString());
            TodayDateinHijri = todaycollectionHijri;
            //     DefaultMonthHijri = calendar.GetMonth(DateTime.Now.Date);


        }


        public async void OnMoreOptionClicked()
        {
            //if (IsNavigatedToSubmitted == false)
            //{
            //ListOfActionButtonsApplicable.Add(AppResources.ZZSaveAsDraft);
            //    PopupNavigation.Instance.PushAsync(new MorePopUpPageView(ListOfActionButtonsApplicable));
            TINDeRegistrationPopUp popUp = new TINDeRegistrationPopUp();
            popUp.OnItemSelect = async () =>
            {
                await OnSaveAsDraftClicked();
            };
            await PopupNavigation.Instance.PushAsync(popUp);


            //}
        }
        public async void LoadReasonSet()
        {
            try
            {
                //await Task.Run(() =>
                //{
                //    App.DisplayProgressView();
                //});

                TinDeregistrationReasonSetData = await TINDeregistrationWebServiceManager.GaztTinDeregistrationReasonData();
                if (TinDeregistrationReasonSetData != null)
                {
                    TinDeregReasons = TinDeregistrationReasonSetData.ReasonSet.Results.ToList();

                    AllOutlets = new List<OutletSetResult>(TinDeregistrationData.OutletSet.Results);
                    List<PermitSetResult> allPermitTypes = new List<PermitSetResult>(TinDeregistrationData.PermitSet.Results);

                    if (!String.IsNullOrEmpty(TinDeregistrationData.ADregReason) && !String.IsNullOrWhiteSpace(TinDeregistrationData.ADregReason))
                    {
                        SelectedReason = TinDeregReasons.Where(m => m.ReasonCd == TinDeregistrationData.ADregReason).FirstOrDefault();
                    }
                    else
                    {
                        SelectedReason = null;
                    }

                    AddOutletDecisionOptions();


                    if (!String.IsNullOrEmpty(TinDeregistrationData.ADregOpt))
                    {
                        try
                        {
                            SelectedOutletOption = OutletDecisionOptions.Where(m => m.OutletOptionIndex == TinDeregistrationData.ADregOpt).FirstOrDefault();

                            SelectedOutletOptionIndex = Convert.ToInt16(SelectedOutletOption.OutletOptionIndex) - 1;
                            SetDefaultReasonLayout();
                            MessagingCenter.Send<TINDeregistrationPageViewModel>(this, "SelectedOutletDecisionOption");
                            // MessagingCenter.Send<TINDeregistrationModel>(SelectedOutletOption, "selectedOutletOption");
                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex.ToString());
                            Console.Write(ex.StackTrace.ToString());
                        }
                    }
                    if (TinDeregistrationData.ADregOpt == "1" || TinDeregistrationData.ADregOpt == "2")
                    {
                        if (TinDeregistrationData.AEffectiveDt != null)
                        {
                            DeregistrationDate = Convert.ToDateTime(TinDeregistrationData.AEffectiveDt);
                            PickerDobToDisplay = DateTime.Parse(TinDeregistrationData.AEffectiveDt).Date.ToString("dd/MM/yyyy");
                        }
                    }


                    foreach (OutletSetResult outletInfo in AllOutlets)
                    {
                        //if (SelectedReason != null)
                        //{
                        //    outletInfo.ReasonDescription = SelectedReason.ReasonDesc;
                        //}
                        if (SelectedOutletOptionIndex == 0 || SelectedOutletOptionIndex == 2)
                        {
                            outletInfo.ReasonDescription = AppResources.TinDeregistrationClosed;

                        }
                        else
                        {
                            outletInfo.ReasonDescription = AppResources.TinDeregistrationTransfer;

                        }
                        outletInfo.PermitTypes = new List<PermitSetResult>();


                        foreach (PermitSetResult permitInfo in allPermitTypes)
                        {
                            if (permitInfo.APermitOutletnoTb == outletInfo.AOutletNoTb)
                            {
                                //if (SelectedReason != null)
                                //    permitInfo.ReasonDescription = SelectedReason.ReasonDesc;
                                if (SelectedOutletOptionIndex == 0 || SelectedOutletOptionIndex == 2)
                                {
                                    permitInfo.ReasonDescription = AppResources.TinDeregistrationClosed;
                                }
                                else
                                {
                                    permitInfo.ReasonDescription = AppResources.TinDeregistrationTransfer;
                                }
                                if (permitInfo.APermitIdNoTb == null)
                                    permitInfo.APermitIdNoTb = "";

                                if (permitInfo.APermitTransTinTb == null)
                                    permitInfo.APermitTransTinTb = "";

                                if (permitInfo.APermitDregRsnTb == null)
                                    permitInfo.APermitDregRsnTb = string.Empty;
                                if (outletInfo.PermitTypes == null)
                                    outletInfo.PermitTypes = new List<PermitSetResult>();

                                outletInfo.PermitTypes.Add(permitInfo);
                            }
                        }
                    }

                }

                PopulateAttachments(AttachmentTypeList);
                SetAllTransferOutletData();
                //TinDeregistrationReasonSetData.ReasonSet.Results.
                //await Task.Run(() =>
                //{
                //    App.HideProgressView();
                //});
            }
            catch (InternetException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                await Task.Run(() =>
                {
                    App.HideProgressView();
                });

                try
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                    });
                }
                catch (Exception mex)
                {
                    Console.WriteLine(mex.Message);
                }
            }
            catch (GAZTErrorException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }

        }

        public void ClearData()
        {
            //Reason Section data
            PickerDobToDisplay = string.Empty;
            if (IDTypeDataModel != null)
            {
                IDTypeDataModel.Name1 = string.Empty;
                FirstNameFromIdType = string.Empty;
                IDTypeDataModel.Name2 = string.Empty;
                IDTypeDataModel.FatherName = string.Empty;
                IDTypeDataModel.GrandfatherName = string.Empty;
                IDTypeDataModel.FamilyName = string.Empty;
                SelectedIdNumber = string.Empty;
                IDTypeDataModel.Name1 = string.Empty;
            }
            PkrDBO = string.Empty;
            DateOfBirth = string.Empty;
            FirstNameFromIdType = string.Empty;
            PickerDOBDateDisplay = string.Empty;
            PickerDobToDisplay = string.Empty;
            TINNumber = string.Empty;



        }
        public async void OnTinRegisrtationReasonClicked()
        {
            try
            {
                List<string> reasonData = new List<string>();

                foreach (TinDeregReasonSetResult reasonDataDesc in TinDeregReasons)
                {
                    if (!string.IsNullOrEmpty(reasonDataDesc.ReasonDesc) && !string.IsNullOrWhiteSpace(reasonDataDesc.ReasonDesc))
                    {
                        if (reasonDataDesc.ReasonCd != "9")
                        {
                            reasonData.Add(reasonDataDesc.ReasonDesc);
                        }
                    }
                }

                GenericPickerModel genericPickerModel = new GenericPickerModel();
                genericPickerModel.PickerData = reasonData;
                genericPickerModel.PickerTitle = AppResources.TinDeregistrationReason;
                genericPickerModel.PickerId = "reasonPicker";

                await PopupNavigation.Instance.PushAsync(new PickerPageView(genericPickerModel));
            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.WriteLine(ex.Message); Console.WriteLine(ex.ToString());
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
                Console.WriteLine(ex.Message); Console.WriteLine(ex.ToString());
            }

        }

        public async void OnOutletPermitTypeDeRegisrtationReasonClicked()
        {
            try
            {
                List<string> reasonData = new List<string>();
                reasonData.Add(AppResources.TinDeregistrationClosed);
                reasonData.Add(AppResources.TinDeregistrationTransfer);

                GenericPickerModel genericPickerModel = new GenericPickerModel();
                genericPickerModel.PickerData = reasonData;
                genericPickerModel.PickerTitle = AppResources.TinDeregistrationReason;
                genericPickerModel.PickerId = "permitTypeReasonPicker";

                await PopupNavigation.Instance.PushAsync(new PickerPageView(genericPickerModel));
            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.WriteLine(ex.Message); Console.WriteLine(ex.ToString());
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
                Console.WriteLine(ex.Message); Console.WriteLine(ex.ToString());
            }

        }

        public async void OnPermitDobClicked()
        {
            GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
            genericDatePickerModel.DatePickerTitle = AppResources.VatDeregDOBDatePickerTitle;
            genericDatePickerModel.PickerId = "PermitTypeDobPickerDateTypePicker";
            try
            {
                await PopupNavigation.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel));
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

        public async void OnTinRegistrationDateClicked()
        {
            GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
            genericDatePickerModel.DatePickerTitle = AppResources.VatDeregDOBDatePickerTitle;
            genericDatePickerModel.PickerId = "_DOBDateTypePicker";
            try
            {
                await PopupNavigation.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel));
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
        public async void ValidateIDNumber(string date = null)
        {
            string dob;
            string idTypeCode = string.Empty;
            if (!string.IsNullOrEmpty(date) && !string.IsNullOrWhiteSpace(date))
            {
                dob = date.Replace("/", "");
            }
            else
                dob = PkrDBO.Replace("/", "");

            //ZS0002 - IQAMA
            //ZS0005 - IBAN

            if (SelectedIdtype == AppResources.TinDeregistrationNationalID)
            {
                idTypeCode = "ZS0001";
            }
            else if (SelectedIdtype == AppResources.TinDeregistrationIQAMANumber)
            {
                idTypeCode = "ZS0002";
            }
            else if (SelectedIdtype == AppResources.TinDeregistrationCompanyID)
            {
                idTypeCode = "ZS0005";
                dob = string.Empty;
            }
            else if (SelectedIdtype == AppResources.TinDeregistrationGCCID)
            {
                idTypeCode = "ZS0003";
                dob = string.Empty;
            }

            if (!string.IsNullOrEmpty(SelectedIdNumber))
            {
                try
                {
                    await PopupNavigation.Instance.PushAsync(App.ActivityIndicatorView, false);


                    string Result = await TaxEvasionWebServiceManager.GAZTVATSignUpValidateIDTypesStringResp(idTypeCode, SelectedIdNumber, dob);

                    if (IDTypeDataModel == null)
                    {
                        IDTypeDataModel = new VATSignUpD();
                    }

                    string _responseData = JObject.Parse(Result)["d"].ToString();
                    IDTypeDataModel = JsonConvert.DeserializeObject<VATSignUpD>(_responseData);

                    if (!string.IsNullOrEmpty(IDTypeDataModel.Name1)) FirstNameFromIdType = IDTypeDataModel.Name1;
                    if (!string.IsNullOrEmpty(IDTypeDataModel.Tin)) TINNumber = IDTypeDataModel.Tin;
                    //   if (!string.IsNullOrEmpty(IDTypeDataModel.Birthdt10)) PickerDOBDateDisplay = IDTypeDataModel.Birthdt10;

                    if (SelectedIdtype == AppResources.TinDeregistrationGCCID)
                    {
                        if (!string.IsNullOrEmpty(IDTypeDataModel.Name1)) FirstNameText.IsEditable = false;
                        if (!string.IsNullOrEmpty(IDTypeDataModel.Name2)) SurnameText.IsEditable = false;
                        if (!string.IsNullOrEmpty(IDTypeDataModel.FatherName)) FathersNameText.IsEditable = false;
                        if (!string.IsNullOrEmpty(IDTypeDataModel.GrandfatherName)) GrandFathersNameText.IsEditable = false;
                        if (!string.IsNullOrEmpty(IDTypeDataModel.FamilyName)) FamilyNameText.IsEditable = false;
                        //Disable DOB
                        if (!string.IsNullOrEmpty(IDTypeDataModel.Birthdt10)) DobText.IsEditable = false;
                    }

                    if (_responseData == null)
                    {
                        IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                        if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                        {
                            FrameIDError = true;

                            await _dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                        }
                        else
                        {
                            FrameIDError = false;


                            await _dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                        }
                    }
                    else
                    {
                        FrameIDError = false;
                    }
                    if (PopupNavigation.PopupStack.Count() > 0)
                        await PopupNavigation.PopAsync();
                }
                catch
                {
                    try
                    {
                        string Result = await WebServiceManager.GAZTValidateIDTypes(idTypeCode, SelectedIdNumber, dob);
                        IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);

                        if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                        {
                            if (PopupNavigation.PopupStack.Count() > 0)
                                await PopupNavigation.PopAsync();
                            FrameIDError = true;
                            await _dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                        }
                        else
                        {
                            //FrmIDNumber.HasError = false;
                            FrameIDError = false;
                            if (PopupNavigation.PopupStack.Count() > 0)
                                await PopupNavigation.PopAsync();
                            await _dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                        }

                        FirstNameFromIdType = string.Empty;
                        PickerDOBDateDisplay = string.Empty;
                        TINNumber = string.Empty;
                        IDTypeDataModel = new VATSignUpD();
                        SelectedIdNumber = string.Empty;
                    }
                    catch (GAZTException gex)
                    {
                        // Handle the GAZT custom exception.
                        string MessageForTheUser = gex.Message;
                        if (gex is GAZTInvalidDataException)
                        {
                            MessageForTheUser = AppResources.ZZSomethingwentwrong;
                        }
                        if (gex is GAZTNetworkConnectivityIssueException)
                        {
                            MessageForTheUser = AppResources.NetworkConnectivityIssue;
                        }
                        else if (gex is GAZTInternetException)
                        {
                            MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                        }
                        else if (gex is GAZTSessionExpiredException)
                        {
                            MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                        }

                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            if (PopupNavigation.PopupStack.Count() > 0)
                                await PopupNavigation.PopAsync();

                            await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                            _navigationService.GoBack();
                        });
                    }
                    catch (InternetException ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            if (PopupNavigation.PopupStack.Count() > 0)
                                await PopupNavigation.PopAsync();
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);

                        });
                    }
                    catch (HttpRequestException ex)
                    {
                        Console.Write(ex.ToString());
                        Console.Write(ex.StackTrace.ToString());
                        string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            // IsLoading = false;
                            if (PopupNavigation.PopupStack.Count() > 0)
                                await PopupNavigation.PopAsync();
                            await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                            //_navigationService.GoBack();
                        });
                    }
                    catch (Exception ex)
                    {

                        string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            if (PopupNavigation.PopupStack.Count() > 0)
                                await PopupNavigation.PopAsync();
                            await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                            //_navigationService.GoBack();
                        });
                    }
                }


            }

            IsLoading = false;
        }

        public async void ValidateIDNumberForIndiviualOutlets()
        {
            string dob = PkrDBO.Replace("/", "");
            string idTypeCode = string.Empty;

            //ZS0002 - IQAMA
            //ZS0005 - IBAN

            if (SelectedIdtype == AppResources.TinDeregistrationNationalID)
            {
                idTypeCode = "ZS0001";
            }
            else if (SelectedIdtype == AppResources.TinDeregistrationIQAMANumber)
            {
                idTypeCode = "ZS0002";
            }
            else if (SelectedIdtype == AppResources.TinDeregistrationCompanyID)
            {
                idTypeCode = "ZS0005";
                dob = string.Empty;
            }
            else if (SelectedIdtype == AppResources.TinDeregistrationGCCID)
            {
                idTypeCode = "ZS0003";
                dob = string.Empty;
            }

            if (!string.IsNullOrEmpty(SelectedIdNumber))
            {
                try
                {
                    //if(PopupNavigation.Instance.PopupStack.Count > 1)
                    //{
                    //    await PopupNavigation.Instance.PopAsync();
                    //}
                    await PopupNavigation.Instance.PushAsync(App.ActivityIndicatorView, false);


                    string Result = await TaxEvasionWebServiceManager.GAZTVATSignUpValidateIDTypesStringResp(idTypeCode, SelectedIdNumber, dob);

                    if (IDTypeDataModel == null)
                    {
                        IDTypeDataModel = new VATSignUpD();
                    }

                    string _responseData = JObject.Parse(Result)["d"].ToString();
                    IDTypeDataModel = JsonConvert.DeserializeObject<VATSignUpD>(_responseData);

                    if (!string.IsNullOrEmpty(IDTypeDataModel.Name1)) FirstNameFromIdType = IDTypeDataModel.Name1;
                    if (!string.IsNullOrEmpty(IDTypeDataModel.Tin)) TINNumber = IDTypeDataModel.Tin;
                    if (!string.IsNullOrEmpty(IDTypeDataModel.Birthdt10)) SelectedDob = IDTypeDataModel.Birthdt10;

                    if (SelectedIdtype == AppResources.TinDeregistrationGCCID)
                    {
                        if (!string.IsNullOrEmpty(IDTypeDataModel.Name1)) FirstNameText.IsEditable = false;
                        if (!string.IsNullOrEmpty(IDTypeDataModel.Name2)) SurnameText.IsEditable = false;
                        if (!string.IsNullOrEmpty(IDTypeDataModel.FatherName)) FathersNameText.IsEditable = false;
                        if (!string.IsNullOrEmpty(IDTypeDataModel.GrandfatherName)) GrandFathersNameText.IsEditable = false;
                        if (!string.IsNullOrEmpty(IDTypeDataModel.FamilyName)) FamilyNameText.IsEditable = false;
                        //Disable DOB
                        if (!string.IsNullOrEmpty(IDTypeDataModel.Birthdt10)) DobText.IsEditable = false;
                    }

                    if (_responseData == null)
                    {
                        IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                        if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                        {
                            FrameIDError = true;

                            await _dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                        }
                        else
                        {
                            FrameIDError = false;


                            await _dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                        }
                    }
                    else
                    {
                        FrameIDError = false;
                    }
                    if (PopupNavigation.PopupStack.Count() > 0)
                        await PopupNavigation.PopAsync();
                }
                catch (Exception _ex)
                {
                    try
                    {
                        string Result = await WebServiceManager.GAZTValidateIDTypes(idTypeCode, SelectedIdNumber, dob);
                        IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);

                        if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                        {
                            if (PopupNavigation.PopupStack.Count() > 0)
                                await PopupNavigation.PopAsync();
                            FrameIDError = true;
                            await _dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                        }
                        else
                        {
                            //FrmIDNumber.HasError = false;
                            FrameIDError = false;
                            if (PopupNavigation.PopupStack.Count() > 0)
                                await PopupNavigation.PopAsync();
                            await _dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                        }

                        FirstNameFromIdType = string.Empty;
                        PickerDOBDateDisplay = string.Empty;
                        SelectedDob = string.Empty;
                        TINNumber = string.Empty;
                        IDTypeDataModel = new VATSignUpD();
                        SelectedIdNumber = string.Empty;
                    }
                    catch (GAZTException gex)
                    {
                        // Handle the GAZT custom exception.
                        string MessageForTheUser = gex.Message;
                        if (gex is GAZTInvalidDataException)
                        {
                            MessageForTheUser = AppResources.ZZSomethingwentwrong;
                        }
                        if (gex is GAZTNetworkConnectivityIssueException)
                        {
                            MessageForTheUser = AppResources.NetworkConnectivityIssue;
                        }
                        else if (gex is GAZTInternetException)
                        {
                            MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                        }
                        else if (gex is GAZTSessionExpiredException)
                        {
                            MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                        }

                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            if (PopupNavigation.PopupStack.Count() > 0)
                                await PopupNavigation.PopAsync();

                            await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                            _navigationService.GoBack();
                        });
                    }
                    catch (InternetException ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            if (PopupNavigation.PopupStack.Count() > 0)
                                await PopupNavigation.PopAsync();
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);

                        });
                    }
                    catch (HttpRequestException ex)
                    {
                        string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            // IsLoading = false;
                            if (PopupNavigation.PopupStack.Count() > 0)
                                await PopupNavigation.PopAsync();
                            await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                            //_navigationService.GoBack();
                        });
                    }
                    catch (Exception ex)
                    {

                        string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            if (PopupNavigation.PopupStack.Count() > 0)
                                await PopupNavigation.PopAsync();
                            await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                            //_navigationService.GoBack();
                        });
                    }
                }


            }

            IsLoading = false;
        }


        public async void ValidateIDNumberForIndiviualPermit()
        {
            string dob = PkrDBO.Replace("/", "");
            string idTypeCode = string.Empty;

            var selectedPermit = SelectedOutletForCloseTranser?.PermitTypes.FirstOrDefault(x => x.APermitNoTb == tempIdTypePermitSetResult.APermitNoTb || x.APermitNoTb == selectedAPermitReason);
            idTypeCode = selectedPermit?.aPermitIdTypeTb;


            if (!string.IsNullOrEmpty(selectedPermit?.APermitIdNoTb))
            {
                try
                {
                    await PopupNavigation.Instance.PushAsync(App.ActivityIndicatorView, false);


                    string Result = await TaxEvasionWebServiceManager.GAZTVATSignUpValidateIDTypesStringResp(idTypeCode, selectedPermit?.APermitIdNoTb, dob);

                    string _responseData = JObject.Parse(Result)["d"].ToString();
                    var IDTypeDataModel = JsonConvert.DeserializeObject<VATSignUpD>(_responseData);

                    selectedPermit.APermitTransTinTb = IDTypeDataModel.Tin;
                    selectedPermit.APermitNm3Tb = IDTypeDataModel.Name1;
                    selectedPermit.APermitNm4Tb = IDTypeDataModel.Name2;
                    selectedPermit.APermitNm5Tb = IDTypeDataModel.FatherName;
                    selectedPermit.APermitNm6Tb = IDTypeDataModel.GrandfatherName;
                    selectedPermit.APermitNm7Tb = IDTypeDataModel.FamilyName;
                    selectedPermit.APermitDeregDisplayDobDate = IDTypeDataModel.Birthdt10;

                    selectedPermit.APermitEditable = false;

                    if (selectedPermit?.PermitIdTypeName == AppResources.TinDeregistrationGCCID)
                    {
                        if (string.IsNullOrWhiteSpace(IDTypeDataModel?.Birthdt10) && string.IsNullOrWhiteSpace(IDTypeDataModel.TaxpDob))
                        {
                            selectedPermit.APermitEditable = true;
                        }
                    }

                    if (_responseData == null)
                    {
                        IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                        if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                        {
                            FrameIDError = true;

                            await _dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                        }
                        else
                        {
                            FrameIDError = false;


                            await _dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                        }
                    }
                    else
                    {
                        FrameIDError = false;
                    }
                    if (PopupNavigation.PopupStack.Count() > 0)
                        await PopupNavigation.PopAsync();
                }
                catch (Exception _ex)
                {
                    try
                    {
                        string Result = await WebServiceManager.GAZTValidateIDTypes(idTypeCode, selectedPermit?.APermitIdNoTb, dob);
                        IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);

                        if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                        {
                            if (PopupNavigation.PopupStack.Count() > 0)
                                await PopupNavigation.PopAsync();
                            FrameIDError = true;
                            await _dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                        }
                        else
                        {
                            //FrmIDNumber.HasError = false;
                            FrameIDError = false;
                            if (PopupNavigation.PopupStack.Count() > 0)
                                await PopupNavigation.PopAsync();
                            await _dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                        }

                        selectedPermit.APermitTransTinTb = string.Empty;
                        selectedPermit.APermitIdNoTb = string.Empty;
                        selectedPermit.APermitNm3Tb = string.Empty;
                        selectedPermit.APermitNm4Tb = string.Empty;
                        selectedPermit.APermitNm5Tb = string.Empty;
                        selectedPermit.APermitNm6Tb = string.Empty;
                        selectedPermit.APermitNm7Tb = string.Empty;
                        selectedPermit.APermitDeregDisplayDobDate = string.Empty;
                        PkrDBO = string.Empty;

                    }
                    catch (GAZTException gex)
                    {
                        // Handle the GAZT custom exception.
                        string MessageForTheUser = gex.Message;
                        if (gex is GAZTInvalidDataException)
                        {
                            MessageForTheUser = AppResources.ZZSomethingwentwrong;
                        }
                        if (gex is GAZTNetworkConnectivityIssueException)
                        {
                            MessageForTheUser = AppResources.NetworkConnectivityIssue;
                        }
                        else if (gex is GAZTInternetException)
                        {
                            MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                        }
                        else if (gex is GAZTSessionExpiredException)
                        {
                            MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                        }

                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            if (PopupNavigation.PopupStack.Count() > 0)
                                await PopupNavigation.PopAsync();

                            await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                            _navigationService.GoBack();
                        });
                    }
                    catch (InternetException ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            if (PopupNavigation.PopupStack.Count() > 0)
                                await PopupNavigation.PopAsync();
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);

                        });
                    }
                    catch (HttpRequestException ex)
                    {
                        string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            // IsLoading = false;
                            if (PopupNavigation.PopupStack.Count() > 0)
                                await PopupNavigation.PopAsync();
                            await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                            //_navigationService.GoBack();
                        });
                    }
                    catch (Exception ex)
                    {

                        string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            if (PopupNavigation.PopupStack.Count() > 0)
                                await PopupNavigation.PopAsync();
                            await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                            //_navigationService.GoBack();
                        });
                    }
                }


            }

            IsLoading = false;
        }

        public async Task ValidateIdNumberFromApi(string tinNumber)
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
                    try
                    {
                        string resultData = await VATChangeFillingWebServiceManager.GAZTGetTInNumberData(tinNumber);

                        SelectedDob = string.Empty;
                        if (IDTypeDataModel == null)
                        {
                            IDTypeDataModel = new VATSignUpD();
                        }

                        string _responseData = string.Empty;
                        string ErrorMessage = string.Empty;

                        try
                        {
                            _responseData = JObject.Parse(resultData)["d"].ToString();
                            IDTypeDataModel = JsonConvert.DeserializeObject<VATSignUpD>(_responseData);
                            FirstNameFromIdType = IDTypeDataModel.Name1;
                        }
                        catch (Exception ex)
                        {
                            Console.Write("---------------------------");
                            Console.Write(ex.StackTrace.ToString());
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(resultData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorCode = errorMesg.error.innererror.errordetails[0].code;
                                ErrorMessage = errorMesg.error.innererror.errordetails[0].message;

                                //String WithReplacedString = ErrorMessage.Replace("An exception was raised", string.Empty);
                                //ErrorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTErrorException(AppResources.InvalidEntry);
                            }
                        }

                        if (string.IsNullOrWhiteSpace(_responseData))
                        {
                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(resultData);
                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                await Task.Run(() =>
                                {
                                    App.HideProgressView();
                                });

                                FrameIDError = true;

                                await _dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                FrameIDError = false;
                                await Task.Run(() =>
                                {
                                    App.HideProgressView();
                                });

                                await _dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                        }
                        else
                        {
                            ///IDTypeDataModel = new VATSignUpD();
                            //IDTypeDataModel = resultData.d;
                            IsEnteredTINValid = true;
                            IBANType idType = IBANTypesList.Where(m => m.key == IDTypeDataModel.Idtype).FirstOrDefault();
                            if (idType != null)
                            {
                                SelectedIdtype = idType.Text;
                                SelectedIDTypeCode = idType.key;
                                IsName1Visible = false;


                                FirstNameLbl = AppResources.ZZZVATRFirstName;
                                SurnameNameLbl = AppResources.TinDeregistrationSurName;

                                SelectedDob = IDTypeDataModel.Birthdt10;
                                PickerDOBDateDisplay = IDTypeDataModel.Birthdt10;
                                SelectedIdNumber = IDTypeDataModel.Idnum;
                                TINNumber = IDTypeDataModel.Tin;


                                if (SelectedIdtype == AppResources.TinDeregistrationNationalID)
                                {
                                    NationalTypeSelected();
                                }
                                else if (SelectedIdtype == AppResources.TinDeregistrationCompanyID)
                                {
                                    IsName1Visible = true;
                                    CompanyIdTypeSelected();
                                }
                                else if (SelectedIdtype == AppResources.TinDeregistrationIQAMANumber)
                                {
                                    IqamaTypeSelected();
                                }
                                else if (SelectedIdtype == AppResources.TinDeregistrationGCCID)
                                {
                                    GCCIdTypeSelected();
                                }
                            }
                        }
                        //else
                        //{
                        //    Device.BeginInvokeOnMainThread(async () =>
                        //    {
                        //        IsLoading = false;
                        //        //await _dialogService.ShowMessage(resultDa, AppResources.Information);
                        //    });
                        //}
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
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
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
            catch (GAZTErrorException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
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


        public async Task ValidateIdNumberForPermitTypes(string tinNumber)
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
                    try
                    {
                        string resultData = await VATChangeFillingWebServiceManager.GAZTGetTInNumberData(tinNumber);

                        SelectedDob = string.Empty;
                        if (IDTypeDataModel == null)
                        {
                            IDTypeDataModel = new VATSignUpD();
                        }

                        string _responseData = JObject.Parse(resultData)["d"].ToString();
                        IDTypeDataModel = JsonConvert.DeserializeObject<VATSignUpD>(_responseData);
                        if (_responseData == null)
                        {
                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(resultData);
                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                await Task.Run(() =>
                                {
                                    App.HideProgressView();
                                });

                                FrameIDError = true;

                                await _dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                FrameIDError = false;
                                await Task.Run(() =>
                                {
                                    App.HideProgressView();
                                });

                                await _dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                        }
                        else
                        {
                            //IDTypeDataModel = new VATSignUpD();
                            //IDTypeDataModel = _responseData;

                            IBANType idType = IBANTypesList.Where(m => m.key == IDTypeDataModel.Idtype).FirstOrDefault();

                            SelectedOutletForCloseTranser.PermitTypes = new List<PermitSetResult>(SelectedOutletForCloseTranser.PermitTypes.ToList().Select(
                              x =>
                              {
                                  x.APermitIdTypeTb = idType.Text;
                                  x.APermitIdNoTb = IDTypeDataModel.Idnum;
                                  x.APermitNm1Tb = IDTypeDataModel.Name1;
                                  x.APermitNm2Tb = IDTypeDataModel.Name2;
                                  x.APermitNm3Tb = IDTypeDataModel.Name1;
                                  x.APermitNm4Tb = IDTypeDataModel.Name2;
                                  x.APermitNm5Tb = IDTypeDataModel.FatherName;
                                  x.APermitNm6Tb = IDTypeDataModel.GrandfatherName;
                                  x.APermitNm7Tb = IDTypeDataModel.FamilyName;
                                  if (!string.IsNullOrEmpty(IDTypeDataModel.Birthdt10))
                                  {
                                      x.APermitDobTb = ConvertDateFormat(IDTypeDataModel.Birthdt10);
                                  }
                                  x.APermitDeregDisplayDobDate = IDTypeDataModel.Birthdt10;
                                  x.APermitIdTypeTb = IDTypeDataModel.Idtype;
                                  return x;
                              }
                              ).ToList());
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
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
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


        public void AddPermitOutletDecisionOptions()
        {
            List<TINDeregistrationModel> tempValues = new List<TINDeregistrationModel>();
            try
            {
                tempValues.Add(new TINDeregistrationModel
                {
                    ActiveOutletDecisionOptions = AppResources.TinDeregistrationCloseOneOutlet,
                    ActiveOutletDecisionOptionsIsSelected = true,
                    OutletOptionIndex = "1"
                });
                tempValues.Add(new TINDeregistrationModel
                {
                    ActiveOutletDecisionOptions = AppResources.TinDeregistrationTransferOneOutletsToSingle,
                    ActiveOutletDecisionOptionsIsSelected = false,
                    OutletOptionIndex = "2"
                });
                tempValues.Add(new TINDeregistrationModel
                {
                    ActiveOutletDecisionOptions = AppResources.TinDeregistrationCloseOrTransferOutletPermitIndividually,
                    ActiveOutletDecisionOptionsIsSelected = false,
                    OutletOptionIndex = "3"
                });

                PermitOutletDecisionOptions = new List<TINDeregistrationModel>(tempValues);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }

        public void AddOutletDecisionOptions()
        {
            List<TINDeregistrationModel> tempValues = new List<TINDeregistrationModel>();

            try
            {
                if (SelectedReason != null && SelectedReason.ReasonDesc != null)
                {
                    if (SelectedReason.ReasonDesc == AppResources.TinDeregistrationReasonBankruptcy || SelectedReason.ReasonDesc == AppResources.TinDeregistrationReasonDeath
                        || SelectedReason.ReasonDesc == AppResources.TinDeregistrationReasonLiquidation)
                    {

                        tempValues.Add(new TINDeregistrationModel
                        {
                            ActiveOutletDecisionOptions = AppResources.TinDeregistrationCloseAllOutlets,

                            ActiveOutletDecisionOptionsIsSelected = true,
                            OutletOptionIndex = "1"
                        });
                        tempValues.Add(new TINDeregistrationModel
                        {
                            ActiveOutletDecisionOptions = AppResources.TinDeregistrationTransferAllOutletsToSingle,
                            ActiveOutletDecisionOptionsIsSelected = false,
                            OutletOptionIndex = "2"
                        });
                        tempValues.Add(new TINDeregistrationModel
                        {
                            ActiveOutletDecisionOptions = AppResources.TinDeregistrationCloseOutletsIndividually,
                            ActiveOutletDecisionOptionsIsSelected = false,
                            OutletOptionIndex = "3"
                        });
                    }
                    else
                    {
                        tempValues.Add(new TINDeregistrationModel
                        {
                            ActiveOutletDecisionOptions = AppResources.TinDeregistrationTransferAllOutletsToSingle,
                            ActiveOutletDecisionOptionsIsSelected = false,
                            OutletOptionIndex = "2"
                        });
                    }
                    OutletDecisionOptions = new List<TINDeregistrationModel>(tempValues);
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
        public void EnableReasonView()
        {
            CurrentStep = ProcessStep.Step1;

            //SelectedOutletOption = OutletDecisionOptions[0];
            //SelectedOutletOptionIndex = 0;
            if (string.IsNullOrWhiteSpace(SelectedOutletOption?.CardLabel) && !string.IsNullOrWhiteSpace(TinDeregistrationData.ADregOpt))
            {
                SelectedOutletOption = OutletDecisionOptions.Where(m => m.OutletOptionIndex == TinDeregistrationData.ADregOpt).FirstOrDefault();
                SelectedOutletOptionIndex = Convert.ToInt16(SelectedOutletOption.OutletOptionIndex) - 1;
            }
            MessagingCenter.Send<TINDeregistrationPageViewModel>(this, "SelectedOutletDecisionOption");

            IsBackButtonVisible = true;
            IsReasonViewEnabled = true;
            IsOutletViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsDeclarationViewEnabled = false;
            IsSummaryViewEnabled = false;
        }

        public void EnableOutletDetaislView(bool flagCB = false)
        {

            bool flag = true;
            if (TinDeregistrationData.OutletSet.Results != null)
            {
                if (!flagCB)
                {
                    if (TinDeregistrationData.OutletSet.Results.Length != 0)
                    {
                        IsOutletChecked = true;
                    }
                    else
                    {
                        IsOutletChecked = false;

                    }
                }
            }
            if (TinDeregistrationData.ADregOpt == "3")
            {
                if (SelectedPermitOutletOptionIndex == 2)// TODO check outlet permit de registration date
                {
                    if (SelectedOutletForCloseTranser != null && SelectedOutletForCloseTranser.PermitTypes.Any(x => string.IsNullOrWhiteSpace(x.APermitDeregDisplayDate)))
                    {

                        //IsPermitTypesVisible = false;
                        flag = false;
                        IsOutletContinueButtonEnabled = false;
                    }
                    else
                    {
                        AllOutlets = AllOutlets?.Select(x =>
                        {
                            if (x.PermitTypes.Any(y => !string.IsNullOrWhiteSpace(y.APermitDeregDisplayDate)))
                                x.ShowPermit = true;
                            else
                                x.ShowPermit = false;
                            return x;
                        }).ToList();
                        IsOutletContinueButtonEnabled = !AllOutlets.Any(x => !x.ShowPermit) ? true : false;

                    }
                }
                else
                {
                    AllOutlets = AllOutlets?.Select(x =>
                    {
                        if (x.AOutletNoTb == SelectedOutletForCloseTranser?.AOutletNoTb)
                            x.PermitTypes = x.PermitTypes.Select(y =>
                            {
                                y.ReasonDescription = SelectedPermitOutletOptionIndex == 0 ? AppResources.TinDeregistrationClosed : AppResources.TinDeregistrationTransfer;
                                return y;
                            }).ToList();
                        return x;
                    }).ToList();

                    if (SingleDeregistrationDate == null)
                    {
                        //IsPermitTypesVisible = false;
                        flag = false;
                        IsOutletContinueButtonEnabled = false;
                        AllOutlets = AllOutlets?.Select(x =>
                        {
                            x.ShowPermit = false;
                            return x;
                        }).ToList();
                    }
                    else
                    {
                        AllOutlets = AllOutlets?.Select(x =>
                        {
                            if (x.PermitTypes.Any(y => !string.IsNullOrWhiteSpace(y.APermitDeregDisplayDate)))
                                x.ShowPermit = true;
                            else
                                x.ShowPermit = false;
                            return x;
                        }).ToList();
                        //IsPermitTypesVisible = true;

                        IsOutletContinueButtonEnabled = !AllOutlets.Any(x => !x.ShowPermit) ? true : false;

                    }
                }
                AllOutlets = new List<OutletSetResult>(TinDeregistrationData.OutletSet.Results);
                List<PermitSetResult> allPermitTypes = new List<PermitSetResult>(TinDeregistrationData.PermitSet.Results);

                foreach (OutletSetResult outletInfo in AllOutlets)
                {
                    outletInfo.PermitTypes = new List<PermitSetResult>();

                    var temp = new List<PermitSetResult>();
                    foreach (PermitSetResult permitInfo in allPermitTypes)
                    {
                        if (permitInfo.APermitOutletnoTb == outletInfo.AOutletNoTb)
                        {
                            if (permitInfo.APermitDregRsnTb == null)
                                permitInfo.APermitDregRsnTb = string.Empty;

                            if (permitInfo.APermitIdNoTb == null)
                                permitInfo.APermitIdNoTb = "";

                            if (permitInfo.APermitTransTinTb == null)
                                permitInfo.APermitTransTinTb = "";

                            if (SelectedOutletOption.OutletOptionIndex != "3")
                            {
                                DateTime dt = Convert.ToDateTime(PickerDobToDisplay);
                                permitInfo.APermitDeregDisplayDate = dt.ToString("yyyy/MM/dd");//DeregistrationDate.ToString("dd MMM yyyy");
                                permitInfo.APermitEffDtHTb = DeregistrationDate.ToString("yyyyMMdd");
                                permitInfo.APermitEffDtCTb = "G";
                                permitInfo.APermitEffDtTb = ConvertDateFormat(DeregistrationDate);

                            }
                            if (outletInfo.PermitTypes == null)
                                outletInfo.PermitTypes = new List<PermitSetResult>();

                            if (!outletInfo.PermitTypes.Any(any => any.APermitNoTb == permitInfo.APermitNoTb && any.APermitTypeTb == permitInfo.APermitTypeTb))
                            {
                                temp.Add(permitInfo);
                            }

                        }
                    }
                    outletInfo.PermitTypes = temp;
                }
            }
            else
            {
                AllOutlets = AllOutlets?.Select(x =>
                {
                    x.ShowPermit = true;
                    return x;
                }).ToList();
                IsOutletContinueButtonEnabled = true;
            }
            CurrentStep = ProcessStep.Step2;
            IsBackButtonVisible = true;
            IsReasonViewEnabled = false;
            IsOutletViewEnabled = true;
            IsAttachmentsViewEnabled = false;
            IsDeclarationViewEnabled = false;
            IsSummaryViewEnabled = false;
            MessagingCenter.Send<TINDeregistrationPageViewModel, bool>(this, "EnableOutletContinueButton", flag);
        }

        public async Task EnableAttachmentsView()
        {
            try
            {

                // Need to call the to show the real time attachment
                //if (AttachmentsListViewData != null)
                //{
                //    AttachmentsListViewData.Clear();
                //}
                if (isSaveAsDraftCalledForAttachment == true)
                {
                    TinDeregistrationResponseModel zakatDeregResponseData = new TinDeregistrationResponseModel();
                    zakatDeregResponseData.Approvez = "";
                    zakatDeregResponseData.Rejectz = "";

                    zakatDeregResponseData = await TINDeregistrationWebServiceManager.GaztTinDeregistrationNewRequestData(zakatDeregResponseData);

                    TinDeregistrationData.AttDetSet.Results = zakatDeregResponseData.AttDetSet.Results;

                    //if (AttachmentsListViewData != null)
                    //{
                    //    AttachmentsListViewData.Clear();
                    //}
                    //  AttachmentsListViewData = new List<TinDeregestrationAttachmentsModel>(check);

                    foreach (TinDeregestrationAttachmentsModel tinDeregestrationAttachmentsModel in AttachmentsListViewData)
                    {
                        if (tinDeregestrationAttachmentsModel.AttachmentTypeList != null)
                        {
                            tinDeregestrationAttachmentsModel.AttachmentTypeList.Clear();
                        }
                    }

                    foreach (Attachment attachmentTemp in TinDeregistrationData.AttDetSet.Results)
                    {
                        foreach (TinDeregestrationAttachmentsModel attachmentsModelsTemp in AttachmentsListViewData)
                        {
                            if (attachmentTemp.Dotyp == attachmentsModelsTemp.DocType)
                            {
                                if (attachmentsModelsTemp.AttachmentTypeList == null)
                                    attachmentsModelsTemp.AttachmentTypeList = new List<Attachment>();
                                if (!attachmentsModelsTemp.AttachmentTypeList.Contains(attachmentTemp))
                                    attachmentsModelsTemp.AttachmentTypeList.Add(attachmentTemp);
                            }
                        }
                    }
                    TinDeregistrationData.AttDetSet.Results?.Clear();
                    foreach (var item in AttachmentsListViewData)
                    {
                        if (item.AttachmentTypeList != null && item.AttachmentTypeList.Count >= 0)
                            TinDeregistrationData.AttDetSet.Results.AddRange(item.AttachmentTypeList);
                    }
                    AttachmentsListViewData = new List<TinDeregestrationAttachmentsModel>(AttachmentsListViewData);


                    CurrentStep = ProcessStep.Step3;
                    IsReasonViewEnabled = false;
                    IsOutletViewEnabled = false;
                    IsAttachmentsViewEnabled = true;
                    IsDeclarationViewEnabled = false;
                    IsSummaryViewEnabled = false;

                }
                else
                {


                    foreach (Attachment attachmentTemp in TinDeregistrationData.AttDetSet.Results)
                    {
                        foreach (TinDeregestrationAttachmentsModel attachmentsModelsTemp in AttachmentsListViewData)
                        {
                            if (attachmentTemp.Dotyp == attachmentsModelsTemp.DocType)
                            {
                                if (attachmentsModelsTemp.AttachmentTypeList == null)
                                    attachmentsModelsTemp.AttachmentTypeList = new List<Attachment>();
                                if (!attachmentsModelsTemp.AttachmentTypeList.Contains(attachmentTemp))
                                    attachmentsModelsTemp.AttachmentTypeList.Add(attachmentTemp);
                            }
                        }
                    }
                    TinDeregistrationData.AttDetSet.Results?.Clear();
                    foreach (var item in AttachmentsListViewData)
                    {
                        if (item.AttachmentTypeList != null && item.AttachmentTypeList.Count >= 0)
                            TinDeregistrationData.AttDetSet.Results.AddRange(item.AttachmentTypeList);
                    }
                    AttachmentsListViewData = new List<TinDeregestrationAttachmentsModel>(AttachmentsListViewData);


                    CurrentStep = ProcessStep.Step3;
                    IsReasonViewEnabled = false;
                    IsOutletViewEnabled = false;
                    IsAttachmentsViewEnabled = true;
                    IsDeclarationViewEnabled = false;
                    IsSummaryViewEnabled = false;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); Console.WriteLine(ex.ToString());
            }
        }

        public async void EnableDeclarationView()
        {
            if (TinDeregistrationData.AttDetSet.Results != null)
            {
                if (TinDeregistrationData.AttDetSet.Results.Count != 0)
                {
                    CurrentStep = ProcessStep.Step4;
                    IsReasonViewEnabled = false;
                    IsOutletViewEnabled = false;
                    IsAttachmentsViewEnabled = false;
                    IsDeclarationViewEnabled = true;
                    IsSummaryViewEnabled = false;

                    if (TinDeregistrationData.ADeclarationChkbox == "1")
                    {
                        IsDeclarationChecked = true;
                    }
                    else
                    {
                        IsDeclarationChecked = false;
                    }
                }
                else
                {
                    await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
                }
            }
        }

        public void EnableSummaryView()
        {
            CurrentStep = ProcessStep.Step5;
            IsReasonViewEnabled = false;
            IsOutletViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsDeclarationViewEnabled = false;
            IsSummaryViewEnabled = true;
        }

        public void GoBackBtnClicked()
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
                            try
                            {
                                //foreach (Attachment selectedAttachment in TinDeregistrationData.AttDetSet.Results)
                                //{
                                //    string results = UploadAttachementsWebServiceManager.GAZTGenericDeleteAttachment(selectedAttachment.Filename, TinDeregistrationData.CaseGuid, "", selectedAttachment.Doguid);
                                //    if (results == "X")
                                //    {
                                //        foreach (TinDeregestrationAttachmentsModel attachmentsModelsTemp in AttachmentsListViewData)
                                //        {
                                //            //   UploadedAttachmentFileType = attachmentsModelsTemp.FieldTitle;
                                //            if (selectedAttachment.Dotyp == attachmentsModelsTemp.DocType && attachmentsModelsTemp.AttachmentTypeList.Any(p => p.Filename == selectedAttachment.Filename && p.Dotyp == selectedAttachment.Dotyp))
                                //            {
                                //                var index = attachmentsModelsTemp.AttachmentTypeList.Where(p => p.Filename == selectedAttachment.Filename && p.Dotyp == selectedAttachment.Dotyp).FirstOrDefault();
                                //                if (index != null)
                                //                    attachmentsModelsTemp.AttachmentTypeList.Remove(index);
                                //            }
                                //        }
                                //    }
                                //}

                            }
                            catch (Exception ex)
                            {
                                Console.Write(ex.ToString());
                                Console.Write(ex.StackTrace.ToString());
                                return;
                            }

                            if (isSaveAsDraftCalledForAttachment == true)
                            {
                                AttachmentsListViewData.Clear();
                                TinDeregistrationData.AttDetSet.Results.Clear();
                            }

                            EnableOutletDetaislView();

                            break;
                        }
                    case ProcessStep.Step4:
                        {
                            try
                            {
                                EnableAttachmentsView();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message); Console.WriteLine(ex.ToString());
                            }

                            break;
                        }
                    case ProcessStep.Step5:
                        {
                            EnableDeclarationView();
                            break;
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

        public async void ReasonContinueBtnClicked()
        {
            try
            {
                try
                {

                    if (SelectedReason == null)
                    {
                        await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
                        return;
                    }
                    else if (SelectedOutletOption == null)
                    {
                        await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
                        return;
                    }
                    else if (SelectedOutletOption.OutletOptionIndex == null)
                    {
                        await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
                        return;
                    }
                    else if (IsDeRegistrationValid == false)
                    {
                        await _dialogService.ShowMessage(AppResources.TinDeregistrationDateValidationMessage, AppResources.Information);
                        return;
                    }
                    else
                    {
                        if (SelectedReason.ReasonCd != null)
                        {
                            try
                            {
                                AllOutlets = new List<OutletSetResult>(TinDeregistrationData.OutletSet.Results);
                                List<PermitSetResult> allPermitTypes = new List<PermitSetResult>();

                                if (TinDeregistrationData.PermitSet != null && TinDeregistrationData.PermitSet.Results != null)
                                {
                                    allPermitTypes = new List<PermitSetResult>(TinDeregistrationData.PermitSet.Results);
                                }

                                try
                                {
                                    foreach (OutletSetResult outletInfo in AllOutlets)
                                    {
                                        outletInfo.PermitTypes = new List<PermitSetResult>();
                                        var tempPermitTypes = new List<PermitSetResult>();
                                        if (TinDeregistrationData.ADregOpt == "3")
                                        {
                                            if (SelectedOutletOptionIndex == 0 || SelectedOutletOptionIndex == 2)
                                            {
                                                outletInfo.ReasonDescription = AppResources.TinDeregistrationClosed;
                                            }
                                            else
                                            {
                                                outletInfo.ReasonDescription = AppResources.TinDeregistrationTransfer;
                                            }

                                            if (SingleDeregistrationDate != null)
                                            {
                                                outletInfo.AOutletEffDtHTb = SingleDeregistrationDate == null ? "" : SingleDeregistrationDate;//.ToString("yyyyMMdd");
                                                outletInfo.AOutletEffDtCTb = "G";
                                                outletInfo.AOutletEffDtTb = ConvertDateFormat(SingleDeregistrationDate);
                                            }


                                            foreach (PermitSetResult permitInfo in allPermitTypes)
                                            {
                                                if (permitInfo.APermitOutletnoTb == outletInfo.AOutletNoTb)
                                                {

                                                    if (TinDeregistrationData.ADregOpt == "1" || TinDeregistrationData.ADregOpt == "2")
                                                    {
                                                        permitInfo.ReasonDescription = AppResources.TinDeregistrationClosed;
                                                        permitInfo.APermitDregRsnTb = "1";
                                                    }

                                                    if (SelectedOutletOptionIndex == 0 || SelectedOutletOptionIndex == 2)
                                                    {
                                                        permitInfo.ReasonDescription = AppResources.TinDeregistrationClosed;
                                                        permitInfo.APermitDregRsnTb = "1";
                                                    }
                                                    else
                                                    {
                                                        permitInfo.ReasonDescription = AppResources.TinDeregistrationTransfer;
                                                        permitInfo.APermitDregRsnTb = "3";
                                                    }

                                                    if (permitInfo.APermitDregRsnTb == null)
                                                        permitInfo.APermitDregRsnTb = string.Empty;

                                                    if (permitInfo.APermitIdNoTb == null)
                                                        permitInfo.APermitIdNoTb = "";

                                                    if (permitInfo.APermitTransTinTb == null)
                                                        permitInfo.APermitTransTinTb = "";

                                                    if (SelectedOutletOption.OutletOptionIndex != "3")
                                                    {
                                                        try
                                                        {
                                                            DateTime dt = Convert.ToDateTime(PickerDobToDisplay);
                                                            permitInfo.APermitDeregDisplayDate = dt.ToString("yyyy/MM/dd"); ;//DeregistrationDate.ToString("dd MMM yyyy");
                                                            permitInfo.APermitEffDtHTb = DeregistrationDate.ToString("yyyyMMdd");
                                                            permitInfo.APermitEffDtCTb = "G";
                                                            permitInfo.APermitEffDtTb = ConvertDateFormat(DeregistrationDate);

                                                        }
                                                        catch (Exception ex)
                                                        {

                                                        }

                                                    }

                                                    //if (outletInfo.PermitTypes == null)
                                                    //    outletInfo.PermitTypes = new List<PermitSetResult>();

                                                    //if (!outletInfo.PermitTypes.Any(any => any.APermitNoTb == permitInfo.APermitNoTb && any.APermitTypeTb == permitInfo.APermitTypeTb))
                                                    //    outletInfo.PermitTypes.Add(permitInfo);
                                                    tempPermitTypes.Add(permitInfo);
                                                }
                                            }
                                        }
                                        else if (TinDeregistrationData.ADregOpt == "1")
                                        {
                                            foreach (PermitSetResult permitInfo in allPermitTypes)
                                            {
                                                if (permitInfo.APermitOutletnoTb == outletInfo.AOutletNoTb)
                                                {
                                                    permitInfo.ReasonDescription = AppResources.TinDeregistrationClosed;
                                                    permitInfo.APermitDregRsnTb = "1";

                                                    if (permitInfo.APermitIdNoTb == null)
                                                        permitInfo.APermitIdNoTb = "";

                                                    if (permitInfo.APermitTransTinTb == null)
                                                        permitInfo.APermitTransTinTb = "";

                                                    if (SelectedOutletOption.OutletOptionIndex != "3")
                                                    {
                                                        DateTime dt = Convert.ToDateTime(PickerDobToDisplay);
                                                        permitInfo.APermitDeregDisplayDate = dt.ToString("yyyy/MM/dd");//DeregistrationDate.ToString("dd MMM yyyy");
                                                        permitInfo.APermitEffDtHTb = DeregistrationDate.ToString("yyyyMMdd");
                                                        permitInfo.APermitEffDtCTb = "G";
                                                        permitInfo.APermitEffDtTb = ConvertDateFormat(DeregistrationDate);

                                                    }

                                                    //if (outletInfo.PermitTypes == null)
                                                    //    outletInfo.PermitTypes = new List<PermitSetResult>();

                                                    //if (!outletInfo.PermitTypes.Any(any => any.APermitNoTb == permitInfo.APermitNoTb && any.APermitTypeTb == permitInfo.APermitTypeTb))
                                                    tempPermitTypes.Add(permitInfo);
                                                }
                                            }
                                        }
                                        else
                                        {

                                            outletInfo.AOutletEffDtHTb = DeregistrationDate.ToString("yyyy/MM/dd");
                                            outletInfo.AOutletEffDtTb = ConvertDateFormat(DeregistrationDate);
                                            outletInfo.AOutletEffDtCTb = "G";
                                            foreach (PermitSetResult permitInfo in allPermitTypes)
                                            {
                                                permitInfo.APermitDeregDisplayDate = DeregistrationDate.ToString("yyyy/MM/dd");
                                                if (TinDeregistrationData.ADregOpt == "2")
                                                {
                                                    permitInfo.ReasonDescription = AppResources.TinDeregistrationTransfer;

                                                }
                                                permitInfo.APermitDregRsnTb = TinDeregistrationData.ADregOpt == "2" ? "2" : "3";
                                                tempPermitTypes.Add(permitInfo);

                                            }
                                        }
                                        outletInfo.PermitTypes = tempPermitTypes;
                                    }

                                }
                                catch (Exception ex)
                                {
                                    Console.Write(ex.ToString());
                                    Console.Write(ex.StackTrace.ToString());
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.Write(ex.ToString());
                                Console.Write(ex.StackTrace.ToString());
                            }

                        }
                        else
                        {
                            await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());
                }

                if (SelectedOutletOption.OutletOptionIndex == "2")
                {
                    if (string.IsNullOrEmpty(SelectedIdNumber) || string.IsNullOrEmpty(SelectedReason.ReasonDesc) || string.IsNullOrEmpty(SelectedIdtype))
                    {
                        await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
                        return;
                    }
                    else
                    {
                        if (TINNumber == App.LoginDataRetrieved.TIN)
                        {
                            await _dialogService.ShowMessage(AppResources.TinDeregistrationSameNotAllow, AppResources.Alerts);
                            return;
                        }
                        else if (SelectedIdtype == AppResources.TinDeregistrationCompanyID)
                        {
                            if (string.IsNullOrEmpty(SelectedIdNumber))
                            {
                                await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
                                return;
                            }
                        }
                        else if (SelectedIdtype == AppResources.TinDeregistrationNationalID)
                        {
                            if (string.IsNullOrEmpty(SelectedIdNumber) || string.IsNullOrEmpty(PickerDOBDateDisplay) || string.IsNullOrEmpty(FirstNameFromIdType) || string.IsNullOrEmpty(IDTypeDataModel.Name2))
                            {
                                await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
                                return;
                            }
                        }
                        else if (SelectedIdtype == AppResources.TinDeregistrationGCCID)
                        {
                            if (string.IsNullOrEmpty(SelectedIdNumber) || string.IsNullOrEmpty(FirstNameFromIdType) || string.IsNullOrEmpty(IDTypeDataModel.Name2))
                            {
                                await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
                                return;
                            }
                        }
                        else if (SelectedIdtype == AppResources.TinDeregistrationIQAMANumber)
                        {
                            if (string.IsNullOrEmpty(SelectedIdNumber) || string.IsNullOrEmpty(PickerDOBDateDisplay) || string.IsNullOrEmpty(FirstNameFromIdType) || string.IsNullOrEmpty(IDTypeDataModel.Name2))
                            {
                                await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
                                return;
                            }
                        }

                        // await SaveAsDraft();
                        EnableOutletDetaislView();
                    }

                }
                else if (SelectedOutletOption.OutletOptionIndex == "3")
                {
                    if (SelectedReason == null)
                    {
                        await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
                        return;
                    }
                    else
                    {
                        //  await SaveAsDraft();
                        EnableOutletDetaislView();
                    }
                }
                else if (SelectedOutletOption.OutletOptionIndex == "1")
                {
                    if (SelectedReason == null || string.IsNullOrEmpty(PickerDobToDisplay))
                    {
                        await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
                        return;
                    }

                    //await SaveAsDraft();
                    EnableOutletDetaislView();
                }

                if (TinDeregistrationData.ADregOpt == "3")
                {
                    outletEditIsVisible = true;
                }
                else
                {
                    outletEditIsVisible = false;
                }

                if (PopupNavigation.Instance.PopupStack.Count > 0)
                    await PopupNavigation.Instance.PopAllAsync();
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

        public async void OutletPermitPopupReasonContinueBtnClicked()
        {
            try
            {

                if (SelectedPermitOutletOptionIndex == 1)
                {
                    if (string.IsNullOrEmpty(SelectedIdNumber) || string.IsNullOrEmpty(SingleDeregistrationDate) || string.IsNullOrEmpty(SelectedIdtype))
                    {
                        await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
                        return;
                    }
                    else
                    {
                        if (SelectedIdtype == AppResources.TinDeregistrationCompanyID)
                        {
                            if (string.IsNullOrEmpty(SelectedIdNumber))
                            {
                                await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
                                return;
                            }
                        }
                        else if (SelectedIdtype == AppResources.TinDeregistrationNationalID)
                        {
                            if (string.IsNullOrEmpty(SelectedIdNumber) || string.IsNullOrEmpty(SelectedDob) || string.IsNullOrEmpty(IDTypeDataModel.Name1) || string.IsNullOrEmpty(IDTypeDataModel.Name2))
                            {
                                await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
                                return;
                            }
                        }
                        else if (SelectedIdtype == AppResources.TinDeregistrationGCCID)
                        {
                            if (string.IsNullOrEmpty(SelectedIdNumber) || string.IsNullOrEmpty(SelectedDob) || string.IsNullOrEmpty(IDTypeDataModel.Name1) || string.IsNullOrEmpty(IDTypeDataModel.Name2))
                            {
                                await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
                                return;
                            }
                        }
                        else if (SelectedIdtype == AppResources.TinDeregistrationIQAMANumber)
                        {
                            if (string.IsNullOrEmpty(SelectedIdNumber) || string.IsNullOrEmpty(SelectedDob) || string.IsNullOrEmpty(IDTypeDataModel.Name1) || string.IsNullOrEmpty(IDTypeDataModel.Name2))
                            {
                                await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
                                return;
                            }
                        }

                        SelectedOutletForCloseTranser.ReasonDescription = AppResources.TinDeregistrationTransfer;
                        SetDataForTransferToASingleTranferee();
                        //   await SaveAsDraft();
                        EnableOutletDetaislView();
                    }

                }
                else if (SelectedPermitOutletOptionIndex == 2)
                {
                    //TODO validation for transfer/close to indiviual case missing
                    if (SelectedReason == null)
                    {
                        await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
                        return;
                    }
                    else if (SelectedOutletForCloseTranser.PermitTypes.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.APermitDeregDisplayDate)) != null)
                    {
                        await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
                        return;
                    }
                    else if (SelectedOutletForCloseTranser.PermitTypes.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.APermitDregRsnTb)) != null)
                    {
                        await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
                        return;
                    }
                    else if (SelectedOutletForCloseTranser.PermitTypes.FirstOrDefault(x => x.APermitDregRsnTb == "3") != null)
                    {
                        var transferrred = SelectedOutletForCloseTranser.PermitTypes.Where(x => x.APermitDregRsnTb == "3");

                        if (transferrred.FirstOrDefault(x => x.APermitTransTinTb == App.LoginDataRetrieved.TIN) != null)
                        {
                            await _dialogService.ShowMessage(AppResources.TinDeregistrationSameNotAllow, AppResources.Alerts);
                            return;
                        }
                        else if (transferrred.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.APermitIdNoTb)) != null)
                        {
                            await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
                            return;
                        }
                        else if (transferrred.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.APermitDeregDisplayDobDate)) != null)
                        {
                            await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
                            return;
                        }
                    }
                    // await SaveAsDraft();
                    EnableOutletDetaislView();
                }
                else if (SelectedPermitOutletOptionIndex == 0)
                {
                    if (SelectedReason == null || string.IsNullOrEmpty(SingleDeregistrationDate))
                    {
                        await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
                        return;
                    }
                    SelectedOutletForCloseTranser.ReasonDescription = AppResources.TinDeregistrationClosed;
                    //prepare data for Outlet and Outlet related Permits for Closure
                    var index = TinDeregistrationData.OutletSet.Results.ToList().IndexOf(SelectedOutletForCloseTranser);
                    // close the selected outlet, 1 for Close and 3 for Transfer
                    TinDeregistrationData.OutletSet.Results[index].AOutletDregOptTb = "1";
                    if (IsHijriCal)
                    {
                        TinDeregistrationData.OutletSet.Results[index].AOutletEffDtCTb = "H";
                    }
                    else
                    {
                        TinDeregistrationData.OutletSet.Results[index].AOutletEffDtCTb = "G";
                    }
                    TinDeregistrationData.OutletSet.Results[index].AOutletEffDtHTb = SingleDeregistrationDate;
                    TinDeregistrationData.OutletSet.Results[index].AOutletEffDtTb = ConvertDateFormat(Convert.ToDateTime(SingleDeregistrationDate));
                    foreach (var item in TinDeregistrationData.PermitSet.Results)
                    {
                        if (item.APermitOutletnoTb == SelectedOutletForCloseTranser.AOutletNoTb)
                        {
                            // close all the permits for selected outlet, 1 for Close and 3 for Transfer
                            item.APermitDregRsnTb = "1";
                            if (IsHijriCal)
                            {
                                item.APermitEffDtCTb = "H";
                            }
                            else
                            {
                                item.APermitEffDtCTb = "G";
                            }
                            item.APermitEffDtHTb = SingleDeregistrationDate;
                            item.APermitEffDtTb = ConvertDateFormat(Convert.ToDateTime(SingleDeregistrationDate));
                        }
                    }
                    // await SaveAsDraft();
                    EnableOutletDetaislView();
                }

                if (TinDeregistrationData.ADregOpt == "3")
                {
                    outletEditIsVisible = true;
                }
                else
                {
                    outletEditIsVisible = false;
                }

                if (PopupNavigation.Instance.PopupStack.Count > 0)
                    await PopupNavigation.Instance.PopAllAsync();
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
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }
        }

        public async void AddPopUpPage()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new TINDeregistrationCloseIndividualOutletsPageView(SelectedReason.ReasonDesc, this));
            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.WriteLine(ex.Message); Console.WriteLine(ex.ToString());
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
                Console.WriteLine(ex.Message); Console.WriteLine(ex.ToString());
            }

        }
        public static long ConvertDateTimeToTicks(DateTime dtInput)
        {
            long ticks = 0;
            ticks = dtInput.Ticks;
            return ticks;
        }
        public static DateTime ConvertTicksToDateTime(long lticks)
        {
            DateTime dtresult = new DateTime(lticks);
            return dtresult;
        }

        public async void OutletContinueBtnClicked()
        {
            if (!IsOutletContinueButtonEnabled)
            {
                await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
                return;
            }
            try
            {
                if (IsOutletChecked)
                {

                    PopulateAttachmentsListViewTemplate();
                    //await SaveAsDraft();

                    try
                    {
                        EnableAttachmentsView();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message); Console.WriteLine(ex.ToString());
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); Console.WriteLine(ex.ToString());
            }
        }

        public async void AttachmentsContinueBtnClicked()
        {
            try
            {
                TinDeregistrationData.AttDetSet.Results?.Clear();
                foreach (var item in AttachmentsListViewData)
                {
                    if (item.AttachmentTypeList != null && item.AttachmentTypeList.Count >= 0)
                        TinDeregistrationData.AttDetSet.Results.AddRange(item.AttachmentTypeList);
                }
                bool isMandatoryDocAttached = false;
                foreach (TinDeregestrationAttachmentsModel reqAttachment in AttachmentsListViewData)
                {
                    if (reqAttachment.IsMandatory)
                    {
                        isMandatoryDocAttached = TinDeregistrationData.AttDetSet.Results.Any(attachedDocs => attachedDocs.Dotyp == reqAttachment.DocType);
                        if (!isMandatoryDocAttached)
                        {
                            await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
                            break;
                        }
                    }

                }
                if (!isMandatoryDocAttached)
                    return;
                //await SaveAsDraft();
                EnableDeclarationView();
            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                App.HideProgressView();
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

        public void DeclarationContinueBtnClicked()
        {
            try
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (IsDeclarationChecked)
                    {
                        PopulateSummaryReasonData();
                        PopulateSummaryDeclarationData();
                        if (TinDeregistrationData.ADecName == string.Empty || TinDeregistrationData.ADecDesig == string.Empty || TinDeregistrationData.ADecTelNo == string.Empty)
                        {
                            await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
                        }
                        else
                        {
                            EnableSummaryView();
                        }
                    }
                });
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

        public async void SummaryContinueBtnClicked()
        {
            try
            {
                //Display Success Screen
                await Submit();
                if (isSubmitted)
                {
                    _navigationService.NavigateTo(App.TINDeregestrationSuccessPageView, TinDeregistrationData);
                }
                else
                {
                    //_navigationService.GoBack();

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
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }

        public string selectedAPermitReason;
        public void OnOutletPermitTypeReasonClicked(string value)
        {
            this.selectedAPermitReason = value;
        }


        private string selectedAPermitOutletnoTb;
        public void OnOutletPermitTypeDeRegisrtationReasonDateClicked(string value)
        {
            this.selectedAPermitOutletnoTb = value;

            //try
            //{
            //GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
            //genericDatePickerModel.DatePickerTitle = AppResources.TinDeregistrationDate;
            //genericDatePickerModel.PickerId = "DeregPermitOutletDatePicker";

            //try
            //{
            //    await PopupNavigation.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel, true));
            //}
            //catch (GAZTUnlockAccountException ex)
            //{
            //}
            //catch (InternetException ex)
            //{
            //    Device.BeginInvokeOnMainThread(async () =>
            //    {
            //        await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            //        _navigationService.GoBack();
            //    });
            //}
            //}
            //catch (GAZTUnlockAccountException ex)
            //{
            //}
            //catch (InternetException ex)
            //{
            //    Device.BeginInvokeOnMainThread(async () =>
            //    {
            //        await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            //        _navigationService.GoBack();
            //    });
            //}
        }

        public async void OnTinDeregOutletDeregDatePickerClicked()
        {
            try
            {
                GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
                genericDatePickerModel.DatePickerTitle = AppResources.VatDeregStartDatePickerTitle;
                genericDatePickerModel.PickerId = "OutletDeregDatePicker";

                try
                {
                    await PopupNavigation.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel, true));
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

        public async void OnTinRegisrtationReasonDateClicked()
        {
            try
            {
                GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
                genericDatePickerModel.DatePickerTitle = AppResources.TinDeregistrationDate;
                genericDatePickerModel.PickerId = "DeregDatePicker";

                try
                {
                    await PopupNavigation.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel));
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

        //OutletContinueButtonTapped
        //AttachmentsContinueButtonTapped
        //DeclarationContinueButtonTapped
        //SummaryContinueButtonTapped

        #region Attachments View
        public async Task PopulateAttachmentsListViewTemplate()
        {
            try
            {
                check = new List<TinDeregestrationAttachmentsModel>();

                //ADregReason: "2"
                //ADregOpt: "1"

                if (TinDeregistrationData.ATinType == "1")
                {
                    //Bankruptcy for Establishment
                    if (TinDeregistrationData.ADregReason == "2")
                    {
                        if (TinDeregistrationData.ADregOpt == "1" || TinDeregistrationData.ADregOpt == "2" || TinDeregistrationData.ADregOpt == "3")
                        {
                            check.Add(new TinDeregestrationAttachmentsModel
                            {
                                FieldTitle = AppResources.TinDeregistrationAttachmentCopyOfDeclaringBankruptcy,
                                AttachmentName = string.Empty,
                                IsAttachmentAttached = false,
                                DocType = "DR02",
                                IsMandatory = true
                            });
                        }
                    }

                    //Death Certificate of individual
                    if (TinDeregistrationData.ADregReason == "3")
                    {
                        if (TinDeregistrationData.ADregOpt == "1" || TinDeregistrationData.ADregOpt == "2" || TinDeregistrationData.ADregOpt == "3")
                        {
                            check.Add(new TinDeregestrationAttachmentsModel
                            {
                                FieldTitle = AppResources.TinDeregistrationAttachmentDeathCertificate,
                                AttachmentName = string.Empty,
                                IsAttachmentAttached = false,
                                DocType = "DR01",
                                IsMandatory = true
                            });
                        }
                    }

                    //Liquidatation
                    if (TinDeregistrationData.ADregReason == "4")
                    {
                        if (TinDeregistrationData.ADregOpt == "1" || TinDeregistrationData.ADregOpt == "2" || TinDeregistrationData.ADregOpt == "3")
                        {
                            check.Add(new TinDeregestrationAttachmentsModel
                            {
                                FieldTitle = AppResources.TinDeregistrationAttachmentLiquidation,
                                AttachmentName = string.Empty,
                                IsAttachmentAttached = false,
                                DocType = "DR05",
                                IsMandatory = true
                            });
                        }
                    }
                }

                if (TinDeregistrationData.ATinType == "2")
                {
                    //TinDeregistrationAttachmentMinisterialResponse

                    if (TinDeregistrationData.ADregReason == "1")
                    {
                        if (TinDeregistrationData.ADregOpt == "2")
                        {
                            check.Add(new TinDeregestrationAttachmentsModel
                            {
                                FieldTitle = AppResources.TinDeregistrationAttachmentMinisterialResponse,
                                AttachmentName = string.Empty,
                                IsAttachmentAttached = false,
                                DocType = "DR03",
                                IsMandatory = true
                            });
                            check.Add(new TinDeregestrationAttachmentsModel
                            {
                                FieldTitle = AppResources.TinDeregistrationAttachmentCopyOfContractOfSaleAgreement,
                                AttachmentName = string.Empty,
                                IsAttachmentAttached = false,
                                DocType = "DR09",
                                IsMandatory = true
                            });
                            //check.Add(new TinDeregestrationAttachmentsModel
                            //{
                            //    FieldTitle = AppResources.TinDeregistrationAttachmentCopyOfDeclaringBankruptcy,
                            //    AttachmentName = string.Empty,
                            //    IsAttachmentAttached = false,
                            //    DocType = "DR02",
                            //    IsMandatory = true
                            //});
                        }
                    }

                    if (TinDeregistrationData.ADregReason == "2")
                    {
                        check.Add(new TinDeregestrationAttachmentsModel
                        {
                            FieldTitle = AppResources.TinDeregistrationAttachmentCopyOfDeclaringBankruptcy,
                            AttachmentName = string.Empty,
                            IsAttachmentAttached = false,
                            DocType = "DR02",
                            IsMandatory = true
                        });

                        if (TinDeregistrationData.ADregOpt == "2")
                        {
                            //check.Add(new TinDeregestrationAttachmentsModel
                            //{
                            //    FieldTitle = AppResources.TinDeregistrationAttachmentCopyOfPartnersDecision,
                            //    AttachmentName = string.Empty,
                            //    IsAttachmentAttached = false,
                            //    DocType = "DR08",
                            //    IsMandatory = true
                            //});
                        }
                    }

                    //Logics pending for reason 3 4 5 for idtype 2
                    if (TinDeregistrationData.ADregReason == "5")
                    {
                        if (TinDeregistrationData.ADregOpt == "1" || TinDeregistrationData.ADregOpt == "2" || TinDeregistrationData.ADregOpt == "3")
                        {
                            check.Add(new TinDeregestrationAttachmentsModel
                            {
                                FieldTitle = AppResources.TinDeregistrationAttachmentMerger,
                                AttachmentName = string.Empty,
                                IsAttachmentAttached = false,
                                DocType = "DR04",
                                IsMandatory = true
                            });
                            check.Add(new TinDeregestrationAttachmentsModel
                            {
                                FieldTitle = AppResources.TinDeregistrationAttachmentCopyOfPartnersDecision,
                                AttachmentName = string.Empty,
                                IsAttachmentAttached = false,
                                DocType = "DR08",
                                IsMandatory = true
                            });
                        }
                    }
                    if (TinDeregistrationData.ADregReason == "7")
                    {
                        if (TinDeregistrationData.ADregOpt == "1" || TinDeregistrationData.ADregOpt == "2" || TinDeregistrationData.ADregOpt == "3")
                        {
                            //check.Add(new TinDeregestrationAttachmentsModel
                            //{
                            //    FieldTitle = AppResources.TinDeregistrationAttachmentMerger,
                            //    AttachmentName = string.Empty,
                            //    IsAttachmentAttached = false,
                            //    DocType = "DR04",
                            //    IsMandatory = true
                            //});
                        }
                    }
                    if (TinDeregistrationData.ADregReason == "4")
                    {
                        if (TinDeregistrationData.ADregOpt == "1" || TinDeregistrationData.ADregOpt == "2" || TinDeregistrationData.ADregOpt == "3")
                        {
                            check.Add(new TinDeregestrationAttachmentsModel
                            {
                                FieldTitle = AppResources.TinDeregistrationAttachmentLiquidation,
                                AttachmentName = string.Empty,
                                IsAttachmentAttached = false,
                                DocType = "DR05",
                                IsMandatory = true
                            });
                        }
                    }
                }

                bool isTransfer = false;

                //This attachment is needed when transferring the outlets and not closing for all the cases
                if (TinDeregistrationData.ADregOpt == "2" || TinDeregistrationData.ADregOpt == "3")
                {
                    if (TinDeregistrationData.ADregOpt == "3")
                    {

                        if (AllOutlets != null && AllOutlets.Count > 0)
                            isTransfer = AllOutlets.Any(x => x.PermitTypes != null && x.PermitTypes.Any(y => !string.IsNullOrEmpty(y.APermitDisplayReason) && y.APermitDisplayReason.Equals(AppResources.TinDeregistrationTransfer)));

                        if (isTransfer || SelectedPermitOutletOptionIndex == 1 || permitThirdOptionReason.Contains(AppResources.TinDeregistrationTransfer))

                        {
                            check.Add(new TinDeregestrationAttachmentsModel
                            {
                                FieldTitle = AppResources.TinDeregistrationAttachmentOwnershipSellingAgreement,
                                AttachmentName = string.Empty,
                                IsAttachmentAttached = false,
                                DocType = "DR07",
                                IsMandatory = true
                            });
                        }

                    }
                    else
                    {
                        check.Add(new TinDeregestrationAttachmentsModel
                        {
                            FieldTitle = AppResources.TinDeregistrationAttachmentOwnershipSellingAgreement,
                            AttachmentName = string.Empty,
                            IsAttachmentAttached = false,
                            DocType = "DR07",
                            IsMandatory = true
                        });
                    }
                }

                //In all Cases


                if (TinDeregistrationData != null)
                {
                    if (TinDeregistrationData.PermitSet != null && TinDeregistrationData.PermitSet.Results != null)
                    {
                        foreach (var item in TinDeregistrationData.PermitSet.Results)
                        {
                            if (item.APermitTypeTb == "BUP002" && !check.Exists(x => x.DocType == "DR10"))
                            {
                                check.Add(new TinDeregestrationAttachmentsModel
                                {
                                    FieldTitle = AppResources.TinDeregistrationAttachmentCopyOfCRAfterClosing,
                                    AttachmentName = string.Empty,
                                    IsAttachmentAttached = false,
                                    DocType = "DR10",
                                    IsMandatory = true
                                });
                            }

                            else if (item.APermitTypeTb == "ZS0004" && !check.Exists(x => x.DocType == "DR11"))
                            {
                                check.Add(new TinDeregestrationAttachmentsModel
                                {
                                    FieldTitle = AppResources.TinDeregistrationAttachmentCopyOfLicneseAfterClosing,
                                    AttachmentName = string.Empty,
                                    IsAttachmentAttached = false,
                                    DocType = "DR11",
                                    IsMandatory = true
                                });
                            }
                        }
                    }



                }
                if (AttachmentsListViewData != null)
                {
                    AttachmentsListViewData.Clear();
                }
                AttachmentsListViewData = new List<TinDeregestrationAttachmentsModel>(check);

                //foreach (Attachment attachmentTemp in TinDeregistrationData.AttDetSet.Results)
                //{
                //    foreach (TinDeregestrationAttachmentsModel attachmentsModelsTemp in AttachmentsListViewData)
                //    {
                //        if (attachmentTemp.Dotyp == attachmentsModelsTemp.DocType)
                //        {
                //            if (attachmentsModelsTemp.AttachmentTypeList == null)
                //                attachmentsModelsTemp.AttachmentTypeList = new List<Attachment>();
                //            if (!attachmentsModelsTemp.AttachmentTypeList.Contains(attachmentTemp))
                //                attachmentsModelsTemp.AttachmentTypeList.Add(attachmentTemp);
                //        }
                //    }
                //}
                //TinDeregistrationData.AttDetSet.Results?.Clear();
                //foreach (var item in AttachmentsListViewData)
                //{
                //    if (item.AttachmentTypeList != null && item.AttachmentTypeList.Count >= 0)
                //        TinDeregistrationData.AttDetSet.Results.AddRange(item.AttachmentTypeList);
                //}
                //AttachmentsListViewData = new List<TinDeregestrationAttachmentsModel>(AttachmentsListViewData);
                //// attachmentsListViewDataString = JsonConvert.SerializeObject(attachmentsListViewData);
            }

            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }
        public void PopulateAttachments(List<Attachment> attachments)
        {
            // To clean the existing data to replace it with latest data after upload and delete of attachment
            foreach (var obj in AttachmentsListViewData)
            {
                if (SelectedAttachment != null && SelectedAttachment.DocType != null && SelectedAttachment.DocType.Equals(obj.DocType) && obj.AttachmentTypeList != null)
                {
                    obj.AttachmentTypeList.Clear();
                }
            }

            foreach (Attachment attachmentTemp in TinDeregistrationData.AttDetSet.Results)
            {
                foreach (TinDeregestrationAttachmentsModel attachmentsModelsTemp in AttachmentsListViewData)
                {
                    UploadedAttachmentFileType = attachmentsModelsTemp.FieldTitle;
                    if (attachmentTemp.Dotyp == attachmentsModelsTemp.DocType)
                    {
                        if (attachmentsModelsTemp.AttachmentTypeList == null)
                            attachmentsModelsTemp.AttachmentTypeList = new List<Attachment>();
                        if (!attachmentsModelsTemp.AttachmentTypeList.Contains(attachmentTemp))
                            attachmentsModelsTemp.AttachmentTypeList.Add(attachmentTemp);
                    }
                }
            }
            TinDeregistrationData.AttDetSet.Results?.Clear();
            foreach (var item in AttachmentsListViewData)
            {
                if (item.AttachmentTypeList != null && item.AttachmentTypeList.Count >= 0)
                    TinDeregistrationData.AttDetSet.Results.AddRange(item.AttachmentTypeList);
            }

            AttachmentsListViewData = new List<TinDeregestrationAttachmentsModel>(AttachmentsListViewData);
            attachmentList = GetAllAttachemt(AttachmentsListViewData);
            EnableAttachments();
        }

        private void EnableAttachments()
        {

            if (AttachmentTypeList == null)
            {
                IsAttachmentsEnabled = false;
            }
            else
            {
                if (AttachmentTypeList.Count == 0)
                {

                    IsAttachmentsEnabled = false;
                }
                else
                {
                    IsAttachmentsEnabled = true;
                }

            }
        }
        public async void NewAttachmentClicked()
        {

            try
            {
                foreach (TinDeregestrationAttachmentsModel tinDeregestrationAttachmentsModel in AttachmentsListViewData)
                {
                    if (tinDeregestrationAttachmentsModel.AttachmentTypeList != null)
                    {
                        tinDeregestrationAttachmentsModel.AttachmentTypeList.Clear();
                    }
                }

                var attachmentsList = new List<Attachment>();
                foreach (Attachment attachmentTemp in TinDeregistrationData.AttDetSet.Results)
                {
                    foreach (TinDeregestrationAttachmentsModel attachmentsModelsTemp in AttachmentsListViewData)
                    {
                        UploadedAttachmentFileType = attachmentsModelsTemp.FieldTitle;
                        if (attachmentTemp.Dotyp == attachmentsModelsTemp.DocType)// Replace the code to solve the repeat image issue on TIN De-Registration if (attachmentTemp.Dotyp == SelectedAttachment.DocType)
                        {
                            if (attachmentsModelsTemp.AttachmentTypeList == null)
                                attachmentsModelsTemp.AttachmentTypeList = new List<Attachment>();
                            if (!attachmentsModelsTemp.AttachmentTypeList.Contains(attachmentTemp))
                                attachmentsModelsTemp.AttachmentTypeList.Add(attachmentTemp);
                        }
                    }
                }
                attachmentsList.Clear();
                TinDeregistrationData.AttDetSet.Results?.Clear();
                foreach (var item in AttachmentsListViewData)
                {
                    if (item.AttachmentTypeList != null && item.AttachmentTypeList.Count >= 0)
                    {
                        if (item.DocType == SelectedAttachment.DocType)
                            attachmentsList.AddRange(item.AttachmentTypeList);
                    }
                }

                numberOfAttachmentSentToAttachmentPopUp = attachmentsList.Count;
                await PopupNavigation.Instance.PushAsync(new FilesUploadPopUpPageView(attachmentsList, Models.ZakatInstalationModels.WhichAttachment.TINDeregistration
                        , TinDeregistrationData.CaseGuid, SelectedAttachment.DocType));

            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.WriteLine(ex.Message); Console.WriteLine(ex.ToString());
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
                Console.WriteLine(ex.Message); Console.WriteLine(ex.ToString());
            }
        }

        #endregion

        #region Summary View
        public void PopulateSummaryReasonData()
        {
            List<TINDeregistrationSummaryModel> summaryReasonData = new List<TINDeregistrationSummaryModel>();
            try
            {
                summaryReasonData.Add(new TINDeregistrationSummaryModel
                {
                    SummaryTitle = AppResources.TinDeregistrationReason,
                    SummaryData = SelectedReason.ReasonDesc,
                    IsEditVisible = true
                });
                summaryReasonData.Add(new TINDeregistrationSummaryModel
                {
                    SummaryTitle = AppResources.TinDeregistrationQuestionOutlets,
                    SummaryData = SelectedOutletOption?.ActiveOutletDecisionOptions,
                    IsEditVisible = true
                });
                if (SelectedOutletOption?.OutletOptionIndex != "3")
                {
                    summaryReasonData.Add(new TINDeregistrationSummaryModel
                    {
                        //TinDeregistrationData.AEffectiveDtH = DeregistrationDate.ToString("yyyy/MM/dd");

                        SummaryTitle = AppResources.TinDeregistrationDate,
                        SummaryData = DeregistrationDate.ToString("yyyy/MM/dd"),
                        IsEditVisible = true
                    });
                }
                TinDeregistrationSummaryReasonData = new List<TINDeregistrationSummaryModel>(summaryReasonData);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }

        public void PopulateSummaryDeclarationData()
        {
            List<TINDeregistrationSummaryModel> summaryDeclarationData = new List<TINDeregistrationSummaryModel>();
            try
            {
                summaryDeclarationData.Add(new TINDeregistrationSummaryModel
                {
                    SummaryTitle = AppResources.ZZName,
                    SummaryData = TinDeregistrationData.ADecName,
                    IsEditVisible = true
                });
                summaryDeclarationData.Add(new TINDeregistrationSummaryModel
                {
                    SummaryTitle = AppResources.TinDeregistrationDesignation,
                    SummaryData = TinDeregistrationData.ADecDesig,
                    IsEditVisible = true
                });
                summaryDeclarationData.Add(new TINDeregistrationSummaryModel
                {
                    SummaryTitle = AppResources.MobileNumber,
                    SummaryData = "00966" + TinDeregistrationData.ADecTelNo,
                    IsEditVisible = true
                });
                summaryDeclarationData.Add(new TINDeregistrationSummaryModel
                {
                    SummaryTitle = AppResources.Date,
                    SummaryData = SubmissionDate,
                    IsEditVisible = true
                });

                TinDeregistrationSummaryDeclarationData = new List<TINDeregistrationSummaryModel>(summaryDeclarationData);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }

        public String ConvertDateFormat(object newDate)
        {
            if (newDate == null)
                return null;

            if (!newDate.ToString().Contains("/Date("))
            {
                DateTime dateTime = Convert.ToDateTime(newDate);

                string ConvertedDate = string.Empty;
                TimeSpan span = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
                string unixTime = span.TotalSeconds.ToString("N0");
                unixTime = unixTime.Replace(",", "");
                ConvertedDate = "" + "/Date(" + unixTime + ")/";

                long unixTimestamp = ((long)(dateTime.Subtract(new DateTime(1970, 1, 1))).TotalSeconds);

                unixTimestamp = unixTimestamp * 1000;

                ConvertedDate = "" + "/Date(" + unixTimestamp + ")/";
                return ConvertedDate;
            }

            return newDate.ToString();
        }

        private string GetUnixDate(string _erfdt)
        {
            int startIndex = 6;
            int lengthOfCharacter = _erfdt.Length - 8;
            string unixDateTime = _erfdt.Substring(startIndex, lengthOfCharacter);
            return unixDateTime;
        }

        #endregion

        public async Task SaveAsDraft()
        {
            TinDeregistrationData.Savez = "X";
            TinDeregistrationData.Submitz = "";
            TinDeregistrationData.Xvoidz = "";
            await SubmitRequest();
        }
        void SetDataForTransferToASingleTranferee()
        {
            try
            {
                if (SelectedOutletForCloseTranser != null)
                {
                    var index = TinDeregistrationData.OutletSet.Results.ToList().IndexOf(SelectedOutletForCloseTranser);
                    TinDeregistrationData.OutletSet.Results[index].AOutletEffDtHTb = SingleDeregistrationDate;
                    if (IsHijriCal)
                    {
                        TinDeregistrationData.OutletSet.Results[index].AOutletEffDtCTb = "H";
                        TinDeregistrationData.OutletSet.Results[index].AOutletDobCTb = "H";
                    }
                    else
                    {
                        TinDeregistrationData.OutletSet.Results[index].AOutletEffDtCTb = "G";
                        TinDeregistrationData.OutletSet.Results[index].AOutletDobCTb = "G";
                    }
                    TinDeregistrationData.OutletSet.Results[index].AOutletEffDtTb = ConvertDateFormat(Convert.ToDateTime(SingleDeregistrationDate));
                    TinDeregistrationData.OutletSet.Results[index].AOutletTransTinTb = TINNumber;
                    TinDeregistrationData.OutletSet.Results[index].AOutletIdTypeTb = SelectedIDTypeCode;
                    TinDeregistrationData.OutletSet.Results[index].AOutletIdNoTb = SelectedIdNumber;
                    TinDeregistrationData.OutletSet.Results[index].AOutletDobHTb = SelectedDob;
                    TinDeregistrationData.OutletSet.Results[index].AOutletDobTb = ConvertDateFormat(Convert.ToDateTime(SelectedDob));
                    if (IsName1Visible)
                    {
                        TinDeregistrationData.OutletSet.Results[index].AOutletNm1Tb = IDTypeDataModel.Name1;
                        TinDeregistrationData.OutletSet.Results[index].AOutletNm2Tb = IDTypeDataModel.Name2;
                    }
                    else
                    {
                        TinDeregistrationData.OutletSet.Results[index].AOutletNm1Tb = IDTypeDataModel.Name1;
                        TinDeregistrationData.OutletSet.Results[index].AOutletNm2Tb = IDTypeDataModel.Name2;
                        TinDeregistrationData.OutletSet.Results[index].AOutletNm3Tb = IDTypeDataModel.Name1;
                        TinDeregistrationData.OutletSet.Results[index].AOutletNm4Tb = IDTypeDataModel.Name2;
                        TinDeregistrationData.OutletSet.Results[index].AOutletNm5Tb = IDTypeDataModel.FatherName;
                        TinDeregistrationData.OutletSet.Results[index].AOutletNm6Tb = IDTypeDataModel.GrandfatherName;
                        TinDeregistrationData.OutletSet.Results[index].AOutletNm7Tb = IDTypeDataModel.FamilyName;
                    }
                    foreach (var item in TinDeregistrationData.PermitSet.Results)
                    {
                        if (IsHijriCal)
                        {
                            item.APermitEffDtCTb = "H";
                            item.APermitDobCTb = "H";
                        }
                        else
                        {
                            item.APermitEffDtCTb = "G";
                            item.APermitDobCTb = "G";
                        }
                        item.APermitEffDtTb = ConvertDateFormat(Convert.ToDateTime(SingleDeregistrationDate));
                        item.APermitTransTinTb = TINNumber;
                        item.APermitIdTypeTb = SelectedIDTypeCode;
                        item.APermitIdNoTb = SelectedIdNumber;
                        item.APermitDobHTb = SelectedDob;
                        item.APermitDregRsnTb = "3";
                        item.APermitDobTb = ConvertDateFormat(Convert.ToDateTime(SelectedDob));
                        if (IsName1Visible)
                        {
                            item.APermitNm3Tb = IDTypeDataModel.Name1;
                            item.APermitNm4Tb = IDTypeDataModel.Name2;
                        }
                        else
                        {
                            item.APermitNm1Tb = "";
                            item.APermitNm2Tb = "";
                            item.APermitNm3Tb = IDTypeDataModel.Name1;
                            item.APermitNm4Tb = IDTypeDataModel.Name2;
                            item.APermitNm5Tb = IDTypeDataModel.FatherName;
                            item.APermitNm6Tb = IDTypeDataModel.GrandfatherName;
                            item.APermitNm7Tb = IDTypeDataModel.FamilyName;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                return;
            }
        }
        public async Task Submit()
        {
            try
            {
                TinDeregistrationData.Submitz = "X";
                TinDeregistrationData.Savez = "";
                TinDeregistrationData.Xvoidz = "";
                await SubmitRequest();
            }
            catch (InternetException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                await Task.Run(() =>
                {
                    App.HideProgressView();
                });

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                });
            }
            catch (GAZTErrorException ex)
            {
                await Task.Run(() =>
                {
                    App.HideProgressView();
                });

                string message = ex.Message;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(message, AppResources.Information);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); Console.WriteLine(ex.ToString());
                await Task.Run(() =>
                {
                    App.HideProgressView();
                });
            }

        }

        public async Task VoidForm()
        {
            TinDeregistrationData.Savez = "";
            TinDeregistrationData.Submitz = "";
            TinDeregistrationData.Xvoidz = "X";

            await SubmitRequest();

        }

        public async Task SubmitRequest()
        {
            try
            {
                //await Task.Run(() =>
                //{
                //    App.DisplayProgressView();
                //});
                await PopupNavigation.Instance.PushAsync(App.ActivityIndicatorView);

                try
                {
                    if (IsHijriCal)
                    {
                        TinDeregistrationData.ASubmissionDateC = "H";
                        TinDeregistrationData.AEffectiveDtC = "H";
                        TinDeregistrationData.AExpdtC = "H";
                    }
                    else if (IsDOBHijriCal)
                    {
                        TinDeregistrationData.ADobC = "H";
                    }
                    else
                    {
                        TinDeregistrationData.ASubmissionDateC = "G";
                        TinDeregistrationData.AEffectiveDtC = "G";
                        TinDeregistrationData.AExpdtC = "G";
                        TinDeregistrationData.ADobC = "G";

                    }

                    //TinDeregistrationData.ASubmissionDate = DeregistrationDate.ToString();
                    if (!string.IsNullOrEmpty(PickerDOBDateDisplay))
                    {
                        TinDeregistrationData.ADob = ConvertDateFormat(Convert.ToDateTime(PickerDOBDateDisplay));
                        TinDeregistrationData.ADobH = DeregistrationDate.ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        TinDeregistrationData.ADob = null;
                    }


                    //PickerDOBDateDisplay = TinDeregistrationData.ADobH;//ADob, 
                    //PickerDobToDisplay = TinDeregistrationData.AExpdtH;//ASubmissionDateH,ASubmissionDate

                    //TinDeregistrationData.ASubmissionDate = ConvertDateFormat(DateTime.Now);
                    TinDeregistrationData.ASubmissionDate = ConvertDateFormat(DeregistrationDate);
                    TinDeregistrationData.ASubmissionDateH = DeregistrationDate.ToString("yyyy/MM/dd");

                    TinDeregistrationData.AEffectiveDt = ConvertDateFormat(DeregistrationDate);

                    TinDeregistrationData.AEffectiveDtH = DeregistrationDate.ToString("yyyy/MM/dd");

                    TinDeregistrationData.ADecDate = ConvertDateFormat(DeregistrationDate);
                    TinDeregistrationData.ADecDateH = DeregistrationDate.ToString("yyyy/MM/dd");

                    TinDeregistrationData.AExpdt = ConvertDateFormat(DeregistrationDate);
                    TinDeregistrationData.AExpdtH = DeregistrationDate.ToString("yyyy/MM/dd");

                    IBANType idType = IBANTypesList.Where(m => m.Text == SelectedIdtype).FirstOrDefault();
                    SelectedIDTypeCode = idType.key;

                    TinDeregistrationData.AIdType = SelectedIDTypeCode;// "ZS0005"
                    TinDeregistrationData.AIdNo = SelectedIdNumber;
                    TinDeregistrationData.ATin = TINNumber;
                    TinDeregistrationData.ANm1 = FirstNameFromIdType;
                    TinDeregistrationData.ANm2 = "";
                    TinDeregistrationData.ANm3 = FirstNameFromIdType;// IDTypeDataModel.Name1;
                    TinDeregistrationData.ANm4 = IDTypeDataModel.Name2;
                    TinDeregistrationData.ANm5 = IDTypeDataModel.FatherName;
                    TinDeregistrationData.ANm6 = IDTypeDataModel.GrandfatherName;
                    TinDeregistrationData.ANm7 = IDTypeDataModel.FamilyName;

                    //if (IsDeclarationChecked)
                    //{
                    //    TinDeregistrationData.ADeclarationChkbox = "1";
                    //}
                    //else
                    //{
                    //    TinDeregistrationData.ADeclarationChkbox = "0";
                    //}


                    //try
                    //{
                    //    if (SelectedIdNumber == null)
                    //        TinDeregistrationData.AIdNo = string.Empty;
                    //    else
                    //        TinDeregistrationData.AIdNo = SelectedIdNumber;

                    //    TinDeregistrationData.ANm1 = IDTypeDataModel.Name1;

                    //    TinDeregistrationData.ANm2 = IDTypeDataModel.Name2;

                    //    TinDeregistrationData.AIdType = SelectedIdtype;
                    //}
                    //catch(Exception ex)
                    //{

                    //}

                    //TinDeregistrationData.AttDetSet = new AttachmentSet();

                    OutletSetResult[] oldOutlets = new OutletSetResult[AllOutlets.Count];
                    AllOutlets.CopyTo(oldOutlets, 0);
                    List<OutletSetResult> listOutlets;
                    listOutlets = new List<OutletSetResult>(TinDeregistrationData.OutletSet.Results);
                    List<PermitSetResult> allPermitTypes = new List<PermitSetResult>(TinDeregistrationData.PermitSet.Results);
                    for (int i = 0; i < oldOutlets.Count(); i++)
                    {
                        listOutlets[i].PermitTypes = oldOutlets[i].PermitTypes;
                    }
                    AllOutlets = listOutlets;

                    foreach (OutletSetResult outletInfo in AllOutlets)
                    {
                        //TODO

                        outletInfo.AOutletDobTb = ConvertDateFormat(DeregistrationDate);
                        outletInfo.AOutletToDeregTb = "1";

                        if (SelectedPermitOutletOptionIndex == 0)
                        {
                            outletInfo.AOutletDregOptTb = "1";
                        }
                        else if (SelectedPermitOutletOptionIndex == 1)
                        {
                            outletInfo.AOutletDregOptTb = "2";

                        }
                        else
                        {
                            outletInfo.AOutletDregOptTb = "3";

                        }


                        //if (IsOutletChecked)
                        //{
                        //    outletInfo.AOutletMainFlagTb = "1";
                        //}
                        //else
                        //{
                        //    outletInfo.AOutletMainFlagTb = "0";
                        //}
                    }

                    foreach (PermitSetResult permitInfo in allPermitTypes)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(permitInfo.APermitEffDtTb))
                            {
                                if (permitInfo.APermitValfrDtTb != null)
                                {
                                    if (!permitInfo.APermitValfrDtTb.Contains("/Date("))
                                    {
                                        permitInfo.APermitValfrDtTb = ConvertDateFormat(permitInfo.APermitValfrDtTb);
                                    }
                                    permitInfo.APermitValfrDtCTb = "G";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex.ToString());
                            Console.Write(ex.StackTrace.ToString());
                        }

                        try
                        {
                            if (TinDeregistrationData.ADregOpt == "3")
                            {
                                permitInfo.APermitEffDtTb = ConvertDateFormat(permitInfo.APermitEffDtTb);
                                permitInfo.APermitEffDtCTb = "G";

                                permitInfo.APermitDobCTb = string.IsNullOrEmpty(permitInfo.APermitDobTb) ? "" : "G";
                                permitInfo.APermitDobTb = ConvertDateFormat(permitInfo.APermitDobTb);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex.ToString());
                            Console.Write(ex.StackTrace.ToString());
                        }

                        if (permitInfo.APermitIdNoTb == null)
                            permitInfo.APermitIdNoTb = "";

                        if (permitInfo.APermitTransTinTb == null)
                            permitInfo.APermitTransTinTb = "";

                        //TODO
                        //if (String.IsNullOrEmpty(permitInfo.APermitDobTb))
                        //{
                        //    permitInfo.APermitDobTb = string.Empty;
                        //}
                    }
                }
                catch (Exception ex)
                {

                    //await Task.Run(() =>
                    //{
                    //    App.HideProgressView();
                    //});
                    Console.WriteLine(ex.Message); Console.WriteLine(ex.ToString());
                }

                List<Attachment> tempAttachDetSet = new List<Attachment>();
                foreach (Attachment attachment in TinDeregistrationData.AttDetSet.Results)
                {
                    tempAttachDetSet.Add(attachment);
                }

                string ErrorMessageForUnlockAccount = string.Empty;
                List<Attachment> AttachmentsCopy = new List<Attachment>(TinDeregistrationData.AttDetSet.Results);
                try
                {
                    string TinDeregistrationDataResponse = await TINDeregistrationWebServiceManager.GaztTinDeregistrationSubmitRequestData(TinDeregistrationData);
                    TinDeregistrationParentResponseModel obj = JsonConvert.DeserializeObject<TinDeregistrationParentResponseModel>(TinDeregistrationDataResponse);

                    if (obj.D == null)
                    {
                        isSubmitted = false;
                        SignupErrorModelRootObject SignupErrorModelRootObjectModel = JsonConvert.DeserializeObject<SignupErrorModelRootObject>(TinDeregistrationDataResponse);
                        StringBuilder Message = new StringBuilder();
                        foreach (SignupErrorModelErrordetail itemerror in SignupErrorModelRootObjectModel.error.innererror.errordetails)
                        {
                            if (itemerror.severity.Contains("error"))
                            {
                                if (Message.Length > 0)
                                {
                                    Message.Append(Environment.NewLine);
                                }
                                Message.Append(itemerror.message);
                            }
                        }

                        //await _dialogService.ShowMessage(Message.ToString(), AppResources.Information);

                        //await Task.Run(() =>
                        //{
                        //    App.HideProgressView();
                        //});
                    }
                    else if (!string.IsNullOrEmpty(TinDeregistrationDataResponse))
                    {
                        TinDeregistrationDataResponse = JObject.Parse(TinDeregistrationDataResponse)["d"].ToString();
                        var tempTinDeregData = JsonConvert.DeserializeObject<TinDeregistrationResponseModel>(TinDeregistrationDataResponse);

                        TinDeregistrationData.Fbnum = tempTinDeregData.Fbnum;
                        TinDeregistrationData.Fbnumz = tempTinDeregData.Fbnumz;
                        //TinDeregistrationData = JsonConvert.DeserializeObject<TinDeregistrationResponseModel>(TinDeregistrationDataResponse);
                        //TinDeregistrationData.AttDetSet.Results = AttachmentsCopy;

                        if (TinDeregistrationData == null)
                        {
                            isSubmitted = false;

                            throw new GAZTErrorException(AppResources.ZZSomethingwentwrong);
                        }
                        else
                        {
                            isSubmitted = true;

                        }

                    }
                    else
                    {
                        throw new GAZTErrorException(AppResources.ZZSomethingwentwrong);
                    }

                    TinDeregistrationData.AttDetSet.Results = tempAttachDetSet;

                    if (TinDeregistrationData.Xvoidz.Equals("X"))
                    {
                        string number = TinDeregistrationData.Fbnum;
                        string displayMessage = AppResources.VATRSuccessFullVoidMessage + " " + number;
                        await _dialogService.ShowMessage(displayMessage, AppResources.Information);
                        //await Task.Run(() =>
                        //{
                        //    App.HideProgressView();
                        //});
                        _navigationService.GoBack();
                    }

                    //await Task.Run(() =>
                    //{
                    //    App.HideProgressView();
                    //});
                }
                catch (InternetException ex)
                {
                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());
                    isSubmitted = false;
                    //await Task.Run(() =>
                    //{
                    //    App.HideProgressView();
                    //});

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                    });
                }
                catch (GAZTErrorException ex)
                {
                    isSubmitted = false;

                    //await Task.Run(() =>
                    //{
                    //    App.HideProgressView();
                    //});

                    string message = ex.Message;

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(message, AppResources.Information);
                    });
                }
                catch (Exception ex)
                {
                    isSubmitted = false;

                    Console.WriteLine(ex.Message); Console.WriteLine(ex.ToString());
                    //await Task.Run(() =>
                    //{
                    //    App.HideProgressView();
                    //});
                }
                //await Task.Run(() =>
                //{
                //    App.HideProgressView();
                //});
            }
            catch (InternetException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                isSubmitted = false;

                //await Task.Run(() =>
                //{
                //    App.HideProgressView();
                //});

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                });
            }
            catch (GAZTErrorException ex)
            {
                isSubmitted = false;

                //await Task.Run(() =>
                //{
                //    App.HideProgressView();
                //});

                string message = ex.Message;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(message, AppResources.Information);
                });
            }
            catch (Exception ex)
            {
                isSubmitted = false;

                Console.WriteLine(ex.Message); Console.WriteLine(ex.ToString());
                //await Task.Run(() =>
                //{
                //    App.HideProgressView();
                //});
            }
            finally
            {
                if (PopupNavigation.PopupStack.Count > 0)
                    await PopupNavigation.Instance.PopAsync(true);
            }
        }

        public async Task OnSaveAsDraftClicked()
        {
            isSubmitted = false;
            switch (CurrentStep)
            {
                case ProcessStep.Step1:
                    {
                        //ReasonContinueBtnClicked();
                        await SaveAsDraft();
                        if (isSubmitted == true)
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.TinDeregDataSavedSuccessfully));
                        }
                        break;

                    }
                case ProcessStep.Step2:
                    {
                        // OutletContinueBtnClicked();
                        await SaveAsDraft();
                        if (isSubmitted == true)
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.TinDeregDataSavedSuccessfully));
                        }
                        break;
                    }
                case ProcessStep.Step3:
                    {
                        //AttachmentsContinueBtnClicked();
                        await SaveAsDraft();
                        if (isSubmitted == true)
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.TinDeregDataSavedSuccessfully));
                            isSaveAsDraftCalledForAttachment = true;
                        }
                        break;

                    }
                case ProcessStep.Step4:
                    {
                        // DeclarationContinueBtnClicked();
                        await SaveAsDraft();
                        if (isSubmitted == true)
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.TinDeregDataSavedSuccessfully));
                            // await _dialogService.ShowMessage("Data saved successfully", AppResources.Information);
                            isSaveAsDraftCalledForAttachment = true;
                        }
                        break;
                    }
                case ProcessStep.Step5:
                    {
                        //SummaryContinueBtnClicked();

                        await SaveAsDraft();
                        if (isSubmitted == true)
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.TinDeregDataSavedSuccessfully));
                            isSaveAsDraftCalledForAttachment = true;
                        }
                        else
                        {
                            //_navigationService.GoBack();

                        }
                        break;

                    }



            }
        }

        public async Task ResetTINDeRegistrationObject()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {

                    TinDeregistrationResponseModel zakatDeregResponseData = new TinDeregistrationResponseModel();
                    zakatDeregResponseData.Approvez = "";
                    zakatDeregResponseData.Rejectz = "";

                    zakatDeregResponseData = await TINDeregistrationWebServiceManager.GaztTinDeregistrationNewRequestData(zakatDeregResponseData);
                    TinDeregistrationData = zakatDeregResponseData;
                    await DeletUploadedImage(TinDeregistrationData.AttDetSet.Results);
                    TinDeregistrationData?.AttDetSet.Results?.Clear();
                    SelectedOutletOption = null;
                    PickerDobToDisplay = null;
                });

                await Task.Run(() =>
                {
                    IsLoading = false;
                });

            }
            catch (Exception ex)
            {

            }


            //  TinDeregistrationData.AttDetSet.Results = zakatDeregResponseData.AttDetSet.Results;

        }


        public async Task DeletUploadedImage(List<Attachment> AttachmentList)
        {
            try
            {
                foreach (Attachment attachment in attachmentList)//List aof attachmentts from server
                {
                    bool isAttachmentAvailableOnServer = false;
                    foreach (Attachment serverAttachment in AttachmentList)//List aof attachmentts from server
                    {
                        if (serverAttachment.Doguid.Equals(attachment.Doguid))
                        {
                            isAttachmentAvailableOnServer = true;
                        }
                    }
                    if (isAttachmentAvailableOnServer == false)
                    {
                        DeleteAttachment(attachment);
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void DeleteAttachment(Attachment attachment)
        {
            try
            {
                string results = UploadAttachementsWebServiceManager.GAZTGenericDeleteAttachment(attachment.Filename, TinDeregistrationData.CaseGuid, "", attachment.Doguid);
                if (results != "X")
                {
                    //   _navigationService.NavigateTo(AppResources.Somethingwentwrong, AppResources.ZError);
                }
            }
            catch (Exception ex)
            {

            }

        }

        public List<Attachment> GetAllAttachemt(List<TinDeregestrationAttachmentsModel> attachmentList)
        {
            try
            {
                List<Attachment> list = new List<Attachment>();
                foreach (TinDeregestrationAttachmentsModel attachmentsModelsTemp in attachmentList)
                {
                    if (attachmentsModelsTemp.AttachmentTypeList != null)
                    {
                        foreach (Attachment attachment in attachmentsModelsTemp.AttachmentTypeList)
                        {
                            list.Add(attachment);
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                return new List<Attachment>();
            }

        }



        public void SetDefaultReasonLayout()
        {
            // TINDeregistrationModel selectedItem = e.AddedItems[0] as TINDeregistrationModel;
            int index = Convert.ToInt16(SelectedOutletOption.OutletOptionIndex) - 1;

            SelectedOutletOptionIndex = OutletDecisionOptions.IndexOf(SelectedOutletOption);

            //if (SelectedOutletOptionIndex == 1)
            //{
            //   NationalTypeSelected();
            //}
            if (SelectedOutletOption.ActiveOutletDecisionOptions.Equals(AppResources.TinDeregistrationTransferAllOutletsToSingle))
            {
                NationalTypeSelected();
                Device.BeginInvokeOnMainThread(() => IsDobVisible = true);
            }

            IsOption1Visible = index == 0 ? true : false;
            IsOption2Visible = index == 1 ? true : false;

            if (IsOption2Visible == true)
            {
                PickerDobToDisplay = string.Empty;
                TINNumber = string.Empty;
                SelectedIdtype = string.Empty;
                SelectedIdNumber = string.Empty;
                PickerDOBDateDisplay = string.Empty;

                if (FirstNameFromIdType != null)
                {
                    FirstNameFromIdType = string.Empty;
                }

                if (IDTypeDataModel == null)
                {
                    IDTypeDataModel = new VATSignUpD();
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        //IDTypeDataModel.Name1 = string.Empty;
                        //IDTypeDataModel.Name2 = string.Empty;
                        //IDTypeDataModel.FatherName = string.Empty;
                        //IDTypeDataModel.GrandfatherName = string.Empty;
                        //IDTypeDataModel.FamilyName = string.Empty;
                    });
                }
            }
            else if (IsOption1Visible == true)
            {
                if (FirstNameFromIdType != null)
                {
                    FirstNameFromIdType = string.Empty;
                }

                PickerDobToDisplay = string.Empty;
                //TodayDate = new ObservableCollection<object>();
                //TodayDateinHijri = new ObservableCollection<object>();
            }
            else
            {
                if (FirstNameFromIdType != null)
                {
                    FirstNameFromIdType = string.Empty;
                }

                PickerDobToDisplay = string.Empty;
                TINNumber = string.Empty;
                SelectedIdtype = string.Empty;
                SelectedIdNumber = string.Empty;
                PickerDOBDateDisplay = string.Empty;

                if (IDTypeDataModel == null)
                {
                    IDTypeDataModel = new VATSignUpD();
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        IDTypeDataModel.Name1 = string.Empty;
                        IDTypeDataModel.Name2 = string.Empty;
                        IDTypeDataModel.FatherName = string.Empty;
                        IDTypeDataModel.GrandfatherName = string.Empty;
                        IDTypeDataModel.FamilyName = string.Empty;
                    });
                }
                PickerDobToDisplay = string.Empty;
                //TodayDate = new ObservableCollection<object>();
                //TodayDateinHijri = new ObservableCollection<object>();
            }

            AllOutlets = AllOutlets.Select(x =>
              {
                  x.PermitTypes = x.PermitTypes.Select(y =>
                  {
                      y.APermitDeregDisplayDate = null;
                      return y;
                  }).ToList();
                  return x;
              }).ToList();

            GetSelectedDataTemplate(SelectedOutletOption.ActiveOutletDecisionOptions.Equals(AppResources.TinDeregistrationTransferAllOutletsToSingle));

        }
        void GetSelectedDataTemplate(bool isIndex1 = false)
        {
            //var captionStyle = Resources["CaptionLabelBlack"] as Style;
            //Grid cardView = new Grid() { HeightRequest = 100 };
            //Grid grid = new Grid() { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, ColumnSpacing = 20, RowSpacing = 10 };
            //Image image = new Image() { Source = ImageSource.FromFile("vat_tile_listofsignup"), Aspect = Aspect.Fill, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand };
            //Label label = new Label()
            //{
            //    HorizontalOptions = LayoutOptions.StartAndExpand,
            //    VerticalOptions = LayoutOptions.EndAndExpand,
            //    Style = captionStyle,
            //    Text = ((TINDeregistrationModel)outletDecisionOptionsListView.SelectedItem).ActiveOutletDecisionOptions,
            //    Margin = new Thickness(20, 0, 20, 20),
            //    TextColor = Color.White,
            //    HorizontalTextAlignment = TextAlignment.Start
            //};
            //grid.Children.Add(image);
            //grid.Children.Add(label);

            //cardView.Children.Add(grid);
            //outletDecisionOptionsListView.SelectedItemTemplate = new DataTemplate(() => new ViewCell { View = cardView });
            //if (SelectedOutletOptionIndex == 2)
            //{
            //   outletEditIsVisible = true;
            //    OutletCheckboxTitle = AppResources.TinDeregistrationOutletCheckboxCloseorTransferAllOutlets;
            //}
            //else if (SelectedOutletOptionIndex == 1)
            //{
            //    outletEditIsVisible = false;
            //    OutletCheckboxTitle = AppResources.TinDeregistrationOutletCheckboxTransferAllOutlets;
            //}
            //else
            //{
            //    outletEditIsVisible = false;
            //    OutletCheckboxTitle = AppResources.TinDeregistrationOutletCheckboxCloseAllOutlets;
            //}
            if (SelectedOutletOption.CardLabel.Equals(AppResources.TinDeregistrationCloseOutletsIndividually))
            {
                outletEditIsVisible = true;
                OutletCheckboxTitle = AppResources.TinDeregistrationOutletCheckboxCloseorTransferAllOutlets;
            }
            else if (SelectedOutletOption.CardLabel.Equals(AppResources.TinDeregistrationTransferAllOutletsToSingle))
            {
                outletEditIsVisible = false;
                OutletCheckboxTitle = AppResources.TinDeregistrationOutletCheckboxTransferAllOutlets;
            }
            else
            {
                outletEditIsVisible = false;
                OutletCheckboxTitle = AppResources.TinDeregistrationOutletCheckboxCloseAllOutlets;
            }
            //int index = Convert.ToInt16(SelectedOutletOptionIndex);
            //if (isIndex1)
            //{
            //    index = 1;
            //}
            if (SelectedOutletOption.CardLabel.Equals(AppResources.TinDeregistrationTransferAllOutletsToSingle))
            {
                IsOption2Visible = true;
            }
            else if (SelectedOutletOption.CardLabel.Equals(AppResources.TinDeregistrationCloseAllOutlets))
            {
                IsOption1Visible = true;
            }
            //IsOption1Visible = index == 0 ? true : false;
            //IsOption2Visible = index == 1 ? true : false;
        }

        public void SetAllTransferOutletData()
        {
            try
            {
                if (TinDeregistrationData != null)
                {
                    if (IDTypeDataModel == null)
                    {
                        IDTypeDataModel = new VATSignUpD();
                    }
                    if (IBANTypesList != null && _iBANTypesList.Count > 0)
                    {
                        foreach (var obj in _iBANTypesList)
                        {
                            if (obj.key.Equals(TinDeregistrationData.AIdType))
                            {
                                SelectedIdtype = obj.Text;
                            }
                            if (TinDeregistrationData.AIdType == "ZS0001")
                            {
                                IsDobVisible = true;
                            }
                            else if (obj.key == "ZS0005")
                            {
                                IsDobVisible = false;
                            }
                            else if (obj.key == "ZS0002")
                            {
                                IsDobVisible = true;
                            }
                            else if (obj.key == "ZS0003")
                            {
                                IsDobVisible = false;
                            }
                        }
                    }

                    //SelectedIdtype = TinDeregistrationData.AIdType;

                    if(string.IsNullOrEmpty(SelectedIdtype))
                    {
                        IBANType idType = IBANTypesList.Where(m => m.Text == SelectedIdtype).FirstOrDefault();
                        SelectedIDTypeCode = idType.key;
                    }

                    SelectedIdNumber = TinDeregistrationData.AIdNo;
                    IDTypeDataModel.Name1 = TinDeregistrationData.ANm3;
                    FirstNameFromIdType = TinDeregistrationData.ANm3;
                    PickerDOBDateDisplay = TinDeregistrationData.ADobH;//ADob, 
                    PickerDobToDisplay = TinDeregistrationData.AExpdtH;//ASubmissionDateH,ASubmissionDate
                    TINNumber = TinDeregistrationData.ATin;
                }

            }
            catch (Exception ex)
            {

            }

        }

    }
}
