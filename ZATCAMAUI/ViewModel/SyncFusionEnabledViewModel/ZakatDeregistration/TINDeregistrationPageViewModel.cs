using System.Globalization;
using System.Text;
using System.Windows.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PanCardView.Extensions;
using Mopups.Services;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.Views.NewDesign.Common;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using ZATCAMAUI.Views.NewDesign.ZakatDeregistration;
using ZATCAMAUI.Views.SyncFusionEnabledViews.ZakatDeregistration;
using Application = Microsoft.Maui.Controls.Application;
using ZATCAMAUI.Core.Interfaces;
using System.Collections.ObjectModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration
{

    public class TINDeregistrationPageViewModel : BaseViewModel
    {
        #region Variable
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
        public string PopUpMsgFor2021 = string.Empty;

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
        //3994 CR Changes 
        public bool _IsEditingAllowed = false;
        public bool isEditingAllowed
        {
            get
            {
                return _IsEditingAllowed;
            }
            set
            {
                if (_IsEditingAllowed == value) return;
                _IsEditingAllowed = value;
                OnPropertyChanged("isEditingAllowed");
            }
        }

        public bool _IsDateConverVisible = true;
        public bool IsDateConverVisible
        {
            get
            {
                return _IsDateConverVisible;
            }
            set
            {
                if (_IsDateConverVisible == value) return;
                _IsDateConverVisible = value;
                OnPropertyChanged("IsDateConverVisible");
            }
        }


        public bool _isTransferPermitViewVisible;
        public bool IsTransferPermitViewVisible
        {
            get => _isTransferPermitViewVisible;
            set
            {
                _isTransferPermitViewVisible = value;
                OnPropertyChanged("IsTransferPermitViewVisible");
            }
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                IDTypeDataModel.name1 = value;
                OnPropertyChanged("FirstName");
            }
        }
        private string _surName;
        public string SurName
        {
            get => _surName;
            set
            {
                _surName = value;
                IDTypeDataModel.name2 = value;
                OnPropertyChanged("SurName");
            }
        }
        private string _familyName;
        public string FamilyName
        {
            get => _familyName;
            set
            {
                _familyName = value;
                IDTypeDataModel.familyName = value;
                OnPropertyChanged("FamilyName");
            }
        }
        private string _fatherName;
        public string FatherName
        {
            get => _fatherName;
            set
            {
                _fatherName = value;
                IDTypeDataModel.fatherName = value;
                OnPropertyChanged("FatherName");
            }
        }
        private string _gFatherName;
        public string GrandfatherName
        {
            get => _gFatherName;
            set
            {
                _gFatherName = value;
                IDTypeDataModel.grandfatherName = value;
                OnPropertyChanged("GrandfatherName");
            }
        }
        public string attachmentsListViewDataString { get; set; }
        private string _labelText;
        public string LabelText
        {
            get => _labelText;
            set
            {
                if (_labelText == value) return;
                _labelText = value;
                OnPropertyChanged(nameof(LabelText));
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
                OnPropertyChanged("CurrentStep");
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
                OnPropertyChanged("IsOutletTranferOutletGridVisible");
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
                OnPropertyChanged("IsMultiplePermitsVisible");
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
                OnPropertyChanged("IsNodataAvailableVisible");
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
                OnPropertyChanged("TodayDate");
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
                OnPropertyChanged("TodayDateinHijri");
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
                OnPropertyChanged("PkrDBO");
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
                OnPropertyChanged("DateOfBirth");
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
                OnPropertyChanged("TINNumber");
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
                OnPropertyChanged("PkrDBOPrev");
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
                OnPropertyChanged("PickerDobToDisplay");
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
                //  if (_PickerCloseAllDeregDateDisplay == value) return;

                _PickerCloseAllDeregDateDisplay = value;
                OnPropertyChanged("PickerCloseAllDeregDateDisplay");
            }
        }
        private string _PickerCloseAllDeregDateDisplay1 = string.Empty;
        public string PickerCloseAllDeregDateDisplay1
        {
            get
            {
                return _PickerCloseAllDeregDateDisplay1;
            }
            set
            {
                //  if (_PickerCloseAllDeregDateDisplay == value) return;

                _PickerCloseAllDeregDateDisplay = value;
                _PickerCloseAllDeregDateDisplay1 = value;
                OnPropertyChanged("PickerCloseAllDeregDateDisplay1");
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
                OnPropertyChanged("TransferPickerDOBDateDisplay");
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
                OnPropertyChanged("PickerTransferDeregDateDisplay");
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
                OnPropertyChanged("PickerDOBDateDisplay");
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
                OnPropertyChanged("OutletCheckboxTitle");
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
                OnPropertyChanged("IsReasonViewEnabled");
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
        private bool _IsPermitHijriCal = false;
        public bool IsPermitHijriCal
        {
            get
            {
                return _IsPermitHijriCal;
            }
            set
            {
                if (_IsPermitHijriCal == value) return;

                _IsPermitHijriCal = value;
                OnPropertyChanged("IsPermitHijriCal");
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
                OnPropertyChanged("IsDOBHijriCal");
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
                OnPropertyChanged("IsDeclarationViewEnabled");
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

                OnPropertyChanged("IsDeclarationChecked");
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

                OnPropertyChanged("IsOutletChecked");
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
        private Color _outletContinueButtonnBackroundColor = (Color)Application.Current.Resources["Secondary"];
        public Color OutletContinueButtonnBackroundColor
        {
            get
            {
                return _outletContinueButtonnBackroundColor;
            }
            set
            {
                _outletContinueButtonnBackroundColor = value;
                OnPropertyChanged("OutletContinueButtonnBackroundColor");
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
                    DeclarationContinueButtonnBackroundColor = (Color)Application.Current.Resources["Secondary"];
                }
                else
                {
                    DeclarationContinueButtonnBackroundColor = (Color)Application.Current.Resources["ButtonGray"];
                }
                OnPropertyChanged("IsDeclarationContinueButtonEnabled");
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
                    OutletContinueButtonnBackroundColor = (Color)Application.Current.Resources["Secondary"];
                }
                else
                {
                    OutletContinueButtonnBackroundColor = (Color)Application.Current.Resources["ButtonGray"];
                }
                OnPropertyChanged("IsOutletContinueButtonEnabled");
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
                OnPropertyChanged("TinDeregistrationModel");
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
                OnPropertyChanged("OutletDecisionOptions");
            }
        }
        private bool _isOutletDecisionOptionsLVVisible = true;
        public bool IsOutletDecisionOptionsLVVisible
        {
            get => _isOutletDecisionOptionsLVVisible; set
            {
                _isOutletDecisionOptionsLVVisible = value;
                OnPropertyChanged(nameof(IsOutletDecisionOptionsLVVisible));
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
                OnPropertyChanged("PermitOutletDecisionOptions");
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
                OnPropertyChanged("SelectedPermitTypeOutletOption");
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
                OnPropertyChanged("SelectedPermitOutletOptionIndex");
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
                OnPropertyChanged("AttachmentsListViewData");
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
                OnPropertyChanged("TinDeregistrationSummaryReasonData");
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
                OnPropertyChanged("TinDeregistrationSummaryOutletData");
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
                OnPropertyChanged("TinDeregistrationSummaryDeclarationData");
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
                OnPropertyChanged("IsOption1Visible");
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
                OnPropertyChanged("IsOption2Visible");
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
                OnPropertyChanged("IsPermitOption1Visible");
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
                OnPropertyChanged("IsPermitOption2Visible");
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
                OnPropertyChanged("IsPermitTypesVisible");
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
                    OnPropertyChanged("SelectedOutletOption");
                }
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
                OnPropertyChanged("IsName1Visible");
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
                OnPropertyChanged("FirstNameLbl");
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
                OnPropertyChanged("SurnameNameLbl");
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
                OnPropertyChanged("AttachmentName");
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
                OnPropertyChanged("SelectedAttachment");
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
                OnPropertyChanged("VATDeregistrationSummaryDeclarationData");
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
                IDTypeDataModel.name1 = value.ANm3;
                FirstNameFromIdType = value.ANm3;
                IDTypeDataModel.name2 = value.ANm4;
                IDTypeDataModel.fatherName = value.ANm5;
                IDTypeDataModel.grandfatherName = value.ANm6;
                IDTypeDataModel.familyName = value.ANm7;


                //SelectedOutletOptionIndex = OutletDecisionOptions.IndexOf(_selectedOutletOption as TINDeregistrationModel);
                OnPropertyChanged("TinDeregistrationData");

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
                OnPropertyChanged("TinDeregistrationReasonSetData");
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
                OnPropertyChanged("SelectedReason");
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
                OnPropertyChanged("TinDeregAttachmentList");
            }
        }

        private bool _isIdtypePlaceHolderVisible { get; set; }
        public bool IsIdtypePlaceHolderVisible
        {
            get
            {
                return _isIdtypePlaceHolderVisible;
            }
            set
            {
                if (_isIdtypePlaceHolderVisible == value)
                    return;

                _isIdtypePlaceHolderVisible = value;
                OnPropertyChanged("IsIdtypePlaceHolderVisible");
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
                if (string.IsNullOrEmpty(value))
                {
                    IsIdtypePlaceHolderVisible = true;
                }
                else
                {
                    IsIdtypePlaceHolderVisible = false;
                }

                if (_selectedIdtype == value)
                    return;

                _selectedIdtype = value;
                OnPropertyChanged("SelectedIdtype");
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
                OnPropertyChanged("FirstNameFromIdType");
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
                OnPropertyChanged("SelectedIdNumber");
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
                OnPropertyChanged("IndSelectedIdNumber");
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
                OnPropertyChanged("IsDobVisible");
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
                        IsDobVisible = true;// false;
                    }
                    else
                    {
                        FathersNameText.IsVisible = true;
                        SurnameText.IsVisible = true;
                        GrandFathersNameText.IsVisible = true;
                        FamilyNameText.IsVisible = true;

                        IsDobVisible = true;
                    }
                }
                else
                {

                    IsDobVisible = true;
                }
                OnPropertyChanged("SelectedIDTypeCode");
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
                OnPropertyChanged("VATDeregistrationSummaryDeclarationData");
            }
        }

        private ObservableCollection<Attachment> _summaryAttachments = new ObservableCollection<Attachment>();
        public ObservableCollection<Attachment> SummaryAttachments
        {
            get
            {
                return _summaryAttachments;
            }
            set
            {
                if (_summaryAttachments == value) return;

                _summaryAttachments = value;
                OnPropertyChanged("SummaryAttachments");
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
                OnPropertyChanged(nameof(IsDetailsFieldEnabled));
            }
        }
        private GenericPickerModel _pickerModel { get; set; }
        public void GetSetPermitTypeReason(string actionType = "set")
        {
            if (actionType == "get")
            {
                SelectedOutletForCloseTranser.PermitTypes = new List<PermitSetResult>(SelectedOutletForCloseTranser.PermitTypes.ToList());

                foreach (var items in SelectedOutletForCloseTranser.PermitTypes)
                {
                    if (items.APermitDregRsnTb == "1")
                    {
                        items.APermitDisplayReason = AppResources.TinDeregistrationClosed;
                        IsTransferPermitViewVisible = false;
                    }
                    else if (items.APermitDregRsnTb == "3")
                    {
                        items.APermitDisplayReason = AppResources.TinDeregistrationTransfer;
                        IsTransferPermitViewVisible = true;
                    }
                }
            }
            else
            {
                string tempSelectedReason = PickerModel.SelectedValue;
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
                            IsTransferPermitViewVisible = false;
                        }
                        else
                        {
                            items.APermitDregRsnTb = "3";
                            IsDetailsFieldEnabled = true;
                            IsTransferPermitViewVisible = true;
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
        }
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

                                    List<PermitSetResult> allPermitTypes = new List<PermitSetResult>(TinDeregistrationData.PermitSet);

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
                                await PopulateAttachmentsListViewTemplate();

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
                                IsDobVisible = true;
                                GCCIdTypeSelected();
                            }
                        }
                        else if (PickerModel.PickerId == "permitTypeReasonPicker")
                        {
                            GetSetPermitTypeReason();
                        }
                        else if (PickerModel.PickerId == "permitIdTypePicker")
                        {


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
                catch (Exception)
                {
                }

                OnPropertyChanged("PickerModel");
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

                            if (IsHijriCal)
                            {
                                string date = UtilityManager.HijriToGreg(SingleOutletDeregistrationDate.ToString("yyyy/MM/dd"));
                                x.APermitEffDtTb = ConvertDateFormat(date);
                                //x.APermitEffDtTb = _singleDeregistrationDate == null ? "" : ConvertDateFormat(_singleDeregistrationDate);

                            }
                            else
                            {
                                x.APermitEffDtTb = ConvertDateFormat(SingleOutletDeregistrationDate);

                            }
                            x.APermitEffDtCTb = "Gregorian";
                            x.APermitEffDtHTb = SingleOutletDeregistrationDate.ToString("yyyy/MM/dd");
                            x.APermitDeregDisplayDate = SingleOutletDeregistrationDate.ToString("dd/MM/yyyy");
                            if (Convert.ToDateTime(x.APermitValfrDtHTb) > SingleOutletDeregistrationDate)
                            {
                                // _dialogService.ShowMessage(AppResources.TinDeregistrationDateValidationMessage, AppResources.Information);
                                MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.TinDeregistrationDateValidationMessage));
                                x.APermitDeregDisplayDate = string.Empty;
                            }
                        }
                        return x;
                    }
                    ).ToList());

                OnPropertyChanged("SingleOutletDeregistrationDate");
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
                                x.APermitDobCTb = "Gregorian";
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

                OnPropertyChanged("PermitDob");
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

                                if (IsHijriCal)
                                {
                                    string date = UtilityManager.HijriToGreg(_singleDeregistrationDate);
                                    x.APermitEffDtTb = ConvertDateFormat(date);
                                    //x.APermitEffDtTb = _singleDeregistrationDate == null ? "" : ConvertDateFormat(_singleDeregistrationDate);

                                }
                                else
                                {
                                    x.APermitEffDtTb = _singleDeregistrationDate == null ? "" : ConvertDateFormat(_singleDeregistrationDate);

                                }
                                x.APermitEffDtCTb = "Gregorian";
                                x.APermitEffDtHTb = _singleDeregistrationDate == null ? "" : _singleDeregistrationDate;//.ToString("yyyyMMdd");
                                x.APermitDeregDisplayDate = _singleDeregistrationDate == null ? "" : _singleDeregistrationDate;//.ToString("dd MMM yyyy");
                                if (Convert.ToDateTime(x.APermitValfrDtHTb) > Convert.ToDateTime(SingleDeregistrationDate))
                                {
                                    //_dialogService.ShowMessage(AppResources.TinDeregistrationDateValidationMessage, AppResources.Information);
                                    MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.TinDeregistrationDateValidationMessage));

                                    x.APermitDeregDisplayDate = string.Empty;
                                }
                                return x;
                            }
                            ).ToList());
                        }
                    }
                }
                OnPropertyChanged("SingleDeregistrationDate");
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
                OnPropertyChanged("DeregistrationDate");
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
                OnPropertyChanged("SubmissionDate");
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
                OnPropertyChanged("SelectedDob");
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
                OnPropertyChanged("FrameTinError");
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
                OnPropertyChanged("outletEditIsVisible");
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
                OnPropertyChanged("IDTypeDataModel");
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
                OnPropertyChanged("TinText");
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
                OnPropertyChanged("AttachmentTypeList");
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
                OnPropertyChanged("DateField");
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
                OnPropertyChanged("IdTypeText");
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
                OnPropertyChanged("IdNumberText");
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
                OnPropertyChanged("DobText");
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
                OnPropertyChanged("FirstNameText");
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
                OnPropertyChanged("SurnameText");
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
                OnPropertyChanged("FathersNameText");
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
                OnPropertyChanged("GrandFathersNameText");
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
                OnPropertyChanged("TinDeregAttachmentsListViewData");
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
                OnPropertyChanged("FamilyNameText");
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
                OnPropertyChanged("Name1Text");
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
                OnPropertyChanged("Name2Text");
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


                OnPropertyChanged("IBANTypesList");
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
                OnPropertyChanged("SelectedOutletForCloseTranser");
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
                OnPropertyChanged("AllOutlets");
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
                OnPropertyChanged("UploadedAttachmentFileType");
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

                OnPropertyChanged("ListOfActionButtonsApplicable");
            }
        }
        public void GetSelectedDataTemplate()
        {
            if (SelectedPermitOutletOptionIndex == 2)
            {
                outletEditIsVisible = true;

            }
            else if (SelectedPermitOutletOptionIndex == 1)
            {
                outletEditIsVisible = false;
                SingleDeregistrationDate = string.Empty;
                PickerCloseAllDeregDateDisplay = string.Empty;
                SelectedIdNumber = string.Empty;
                SelectedIdtype = string.Empty;
                SelectedDob = string.Empty;
                TINNumber = string.Empty;
                IDTypeDataModel = new VATSignUpD();
            }
            else
            {
                outletEditIsVisible = false;
                SingleDeregistrationDate = string.Empty;
                PickerCloseAllDeregDateDisplay = string.Empty;
            }

            int index = Convert.ToInt16(SelectedPermitOutletOptionIndex);
            IsPermitOption1Visible = index == 0 ? true : false;
            IsPermitOption2Visible = index == 1 ? true : false;
        }

        public void PopulateUI()
        {
            var selectedEditOutletIndex = TinDeregistrationData.OutletSet.FindIndex(SelectedOutletForCloseTranser);
            if (SelectedOutletForCloseTranser.AOutletDregOptTb == "1")
            {
                SelectedPermitTypeOutletOption = PermitOutletDecisionOptions[0];
                SelectedPermitOutletOptionIndex = 0;
            }
            else if (SelectedOutletForCloseTranser.AOutletDregOptTb == "2")
            {
                SelectedPermitTypeOutletOption = PermitOutletDecisionOptions[1];
                SelectedPermitOutletOptionIndex = 1;
            }
            else
            {
                SelectedPermitTypeOutletOption = PermitOutletDecisionOptions[2];
                SelectedPermitOutletOptionIndex = 2;
                if (SelectedOutletForCloseTranser != null)
                {
                    if (SelectedOutletForCloseTranser.PermitTypes != null)
                    {
                        if (SelectedOutletForCloseTranser.PermitTypes.Count > 0)
                        {
                            IsNodataAvailableVisible = false;
                            IsMultiplePermitsVisible = true;
                        }
                        else
                            IsNodataAvailableVisible = true;
                    }
                    else
                        IsNodataAvailableVisible = true;
                }
                GetSetPermitTypeReason("get");
            }
            GetSelectedDataTemplate();
            var outletItem = TinDeregistrationData.OutletSet[selectedEditOutletIndex];
            if (outletItem.AOutletIdTypeTb == "ZS0001")
            {
                NationalTypeSelected();
            }
            else if (outletItem.AOutletIdTypeTb == "ZS0002")
            {
                IqamaTypeSelected();
            }
            else if (outletItem.AOutletIdTypeTb == "ZS0003")
            {
                GCCIdTypeSelected();
            }
            else
            {
                CompanyIdTypeSelected();
            }
            SingleDeregistrationDate = outletItem.AOutletEffDtHTb;
            PickerCloseAllDeregDateDisplay = SingleDeregistrationDate;
            TINNumber = outletItem.AOutletTransTinTb;
            var SelectedIdtype1 = IBANTypesList.Where(m => m.key == outletItem.AOutletIdTypeTb);
            SelectedIdtype = SelectedIdtype1 != null ? SelectedIdtype1.FirstOrDefault().Text : "";
            SelectedIdNumber = outletItem.AOutletIdNoTb;
            TransferPickerDOBDateDisplay = string.IsNullOrEmpty(outletItem.AOutletDobTb) ? "" : Convert.ToDateTime(outletItem.AOutletDobTb).ToString("yyyy/MM/dd");
            FirstName = outletItem.AOutletNm1Tb;
            SurName = outletItem.AOutletNm2Tb;
            FatherName = outletItem.AOutletNm5Tb;
            GrandfatherName = outletItem.AOutletNm6Tb;
            FamilyName = outletItem.AOutletNm7Tb;

            if (SelectedOutletForCloseTranser.AOutletDregOptTb != "3")
            {
                IsNodataAvailableVisible = false;
                IsMultiplePermitsVisible = false;
                if (SelectedOutletForCloseTranser.AOutletDregOptTb == "2")
                {
                    FirstNameLbl = AppResources.ZZZVATRFirstName;
                    SurnameNameLbl = AppResources.TinDeregistrationSurName;
                }
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


        public async Task OnPermitTypeTinEntered(PermitSetResult permitSetResult)
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

            await MopupService.Instance.PushAsync(new PickerPageView(genericPickerModel));
        }

        public PermitSetResult tempIdTypePermitSetResult = new PermitSetResult();
        public async Task OnPermitIdTypeClicked(PermitSetResult permitSetResult)
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

            await MopupService.Instance.PushAsync(new PickerPageView(genericPickerModel));
        }

        public async Task OnIdTypeClicked()
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

            await MopupService.Instance.PushAsync(new PickerPageView(genericPickerModel));
        }

        #endregion

        public TINDeregistrationPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

            GoBackBtnTapped = new Command(async () => await GoBackBtnClicked());
            ReasonContinueBtnTapped = new Command(this.ReasonContinueBtnClicked);
            OutletPermitPopupReasonContinueBtnTapped = new Command(this.OutletPermitPopupReasonContinueBtnClicked);
            OutletContinueBtnTapped = new Command(async () => await OutletContinueBtnClicked());
            AttachmentsContinueBtnTapped = new Command(async () => await AttachmentsContinueBtnClicked());
            DeclarationContinueBtnTapped = new Command(async () => await this.DeclarationContinueBtnClicked());
            SummaryContinueBtnTapped = new Command(async () => await SummaryContinueBtnClicked());
            OnTinRegisrtationReasonDateTapped = new Command(async () => await OnTinRegisrtationReasonDateClicked());
            OnTinDeregOutletDeregDatePickerTapped = new Command(async () => await OnTinDeregOutletDeregDatePickerClicked());

            //OnTinDeregOutletDeregDatePickerClicked
            OnTinRegistrationReasonTapped = new Command(async () => await OnTinRegisrtationReasonClicked());
            OnTinRegistrationDateTapped = new Command(async () => await OnTinRegistrationDateClicked());
            OnOutletPermitTypeDeRegisrtationReasonDateTapped = new Command<string>(this.OnOutletPermitTypeDeRegisrtationReasonDateClicked);
            OnOutletPermitTypeReasonTapped = new Command<string>(this.OnOutletPermitTypeReasonClicked);
            OnPermitDobTapped = new Command(async () => await OnPermitDobClicked());
            //
            TinDeregistrationModel = new TINDeregistrationModel();
            SelectedOutletOption = new TINDeregistrationModel();
            TinDeregistrationData = new TinDeregistrationResponseModel();
            TinDeregistrationReasonSetData = new TinDeregistrationReasonSetDataModel();
            OnPermitTypeReasonTapped = new Command(async () => await OnOutletPermitTypeDeRegisrtationReasonClicked());
            OnMoreClicked = new Command(async () => await OnMoreOptionClicked());


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
            IdTypeTapped = new Command(async () => await OnIdTypeClicked());
            PermitIdtypeTapped = new Command<PermitSetResult>(async (obj) => await OnPermitIdTypeClicked(obj));
            PermitTypeTinUnfocused = new Command<PermitSetResult>(async (obj) => await OnPermitTypeTinEntered(obj));

            PopulateIdTypeTypeFromList();
            IsOption1Visible = false;
            IsOption2Visible = false;
            IsPermitOption1Visible = false;
            IsPermitOption2Visible = false;
            VoidIsVisible = false;
            EnableReasonView();
            //SetDataAsitis();//CR3994 Issue
        }

        public void SetDataAsitis()
        {
            if (TinDeregistrationData.BgDregFlg == "X" && TinDeregistrationData.AEffectiveDt != null)
            {
                isEditingAllowed = true;
                IsDateConverVisible = false;
                DeregistrationDate = Convert.ToDateTime(TinDeregistrationData.AEffectiveDt);
                PickerDobToDisplay = DateTime.Parse(TinDeregistrationData.AEffectiveDt).Date.ToString("dd/MM/yyyy");

            }
            else
            {
                isEditingAllowed = false;
                IsDateConverVisible = true;


            }
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
            TodayDateinHijri = todaycollectionHijri;
            //     DefaultMonthHijri = calendar.GetMonth(DateTime.Now.Date);


        }


        public async Task OnMoreOptionClicked()
        {
            TINDeRegistrationPopUp popUp = new TINDeRegistrationPopUp();
            popUp.OnItemSelect = async () =>
            {
                await OnSaveAsDraftClicked();
            };
            await MopupService.Instance.PushAsync(popUp);


            //}
        }
        public async Task LoadReasonSet()
        {
            try
            {

                TinDeregistrationReasonSetData = await TINDeregistrationWebServiceManager.GaztTinDeregistrationReasonData();
                if (TinDeregistrationReasonSetData != null)
                {
                    TinDeregReasons = TinDeregistrationReasonSetData.ReasonSet.ToList();

                    AllOutlets = new List<OutletSetResult>(TinDeregistrationData.OutletSet);
                    List<PermitSetResult> allPermitTypes = new List<PermitSetResult>(TinDeregistrationData.PermitSet);

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
                        }
                        catch (Exception)
                        {


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
            }
            catch (InternetException)
            {


                App.HideProgressView();
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZInternetConnectionMessage));
            }

        }

        public void ClearData()
        {
            //Reason Section dnuata
            PickerDobToDisplay = string.Empty;
            if (IDTypeDataModel != null)
            {
                IDTypeDataModel.name1 = string.Empty;
                FirstNameFromIdType = string.Empty;
                IDTypeDataModel.name2 = string.Empty;
                IDTypeDataModel.fatherName = string.Empty;
                IDTypeDataModel.grandfatherName = string.Empty;
                IDTypeDataModel.familyName = string.Empty;
                SelectedIdNumber = string.Empty;
                IDTypeDataModel.name1 = string.Empty;
                IDTypeDataModel.name2 = string.Empty;
            }
            PkrDBO = string.Empty;
            DateOfBirth = string.Empty;
            FirstNameFromIdType = string.Empty;
            PickerDOBDateDisplay = string.Empty;
            PickerDobToDisplay = string.Empty;
            TINNumber = string.Empty;



        }
        public async Task OnTinRegisrtationReasonClicked()
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

                await MopupService.Instance.PushAsync(new PickerPageView(genericPickerModel));
            }
            catch (InternetException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                _navigationService.GoBack();
            }
        }

        public async Task OnOutletPermitTypeDeRegisrtationReasonClicked()
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

                await MopupService.Instance.PushAsync(new PickerPageView(genericPickerModel));
            }
            catch (InternetException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                _navigationService.GoBack();
            }

        }

        public async Task OnPermitDobClicked()
        {
            GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
            genericDatePickerModel.DatePickerTitle = AppResources.VatDeregDOBDatePickerTitle;
            genericDatePickerModel.PickerId = "PermitTypeDobPickerDateTypePicker";
            try
            {
                await MopupService.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel));
            }
            catch (InternetException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                _navigationService.GoBack();
            }

        }

        public async Task OnTinRegistrationDateClicked()
        {
            GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
            genericDatePickerModel.DatePickerTitle = AppResources.VatDeregDOBDatePickerTitle;
            genericDatePickerModel.PickerId = "_DOBDateTypePicker";
            try
            {
                await MopupService.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel));
            }
            catch (InternetException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                _navigationService.GoBack();
            }

        }
        public async Task ValidateIDNumber(string date = null)
        {
            string dob;
            string idTypeCode = string.Empty;
            if (!string.IsNullOrEmpty(date) && !string.IsNullOrWhiteSpace(date))
            {
                dob = date.Replace("/", "-");
            }
            else
                dob = PkrDBO.Replace("/", "-");

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
                    await MopupService.Instance.PushAsync(App.ActivityIndicatorView, false);

                    string Result = await TaxEvasionWebServiceManager.GAZTVATSignUpValidateIDTypesStringResp(idTypeCode, SelectedIdNumber, dob);

                    if (IDTypeDataModel == null)
                    {
                        IDTypeDataModel = new VATSignUpD();
                    }

                    string _responseData = JObject.Parse(Result)["result"].ToString();
                    IDTypeDataModel = JsonConvert.DeserializeObject<VATSignUpD>(_responseData);

                    if (!string.IsNullOrEmpty(IDTypeDataModel.name1)) FirstNameFromIdType = IDTypeDataModel.name1;
                    if (!string.IsNullOrEmpty(IDTypeDataModel.TIN)) TINNumber = IDTypeDataModel.TIN;
                    //   if (!string.IsNullOrEmpty(IDTypeDataModel.Birthdt10)) PickerDOBDateDisplay = IDTypeDataModel.Birthdt10;

                    if (SelectedIdtype == AppResources.TinDeregistrationGCCID)
                    {
                        if (!string.IsNullOrEmpty(IDTypeDataModel.name1))
                            FirstNameText.IsEditable = false;
                        if (!string.IsNullOrEmpty(IDTypeDataModel.name2)) SurnameText.IsEditable = false;
                        if (!string.IsNullOrEmpty(IDTypeDataModel.fatherName)) FathersNameText.IsEditable = false;
                        if (!string.IsNullOrEmpty(IDTypeDataModel.grandfatherName)) GrandFathersNameText.IsEditable = false;
                        if (!string.IsNullOrEmpty(IDTypeDataModel.familyName)) FamilyNameText.IsEditable = false;
                        if (IDTypeDataModel != null)
                        {
                            FirstName = IDTypeDataModel.name1;
                            SurName = IDTypeDataModel.name2;
                            FatherName = IDTypeDataModel.fatherName;
                            GrandfatherName = IDTypeDataModel.grandfatherName;
                            FamilyName = IDTypeDataModel.familyName;
                        }
                        //Disable DOB
                        if (!string.IsNullOrEmpty(IDTypeDataModel.birthDate10) && idTypeCode == "ZS0003")
                        {
                            DobText.IsEditable = false;
                            PickerDOBDateDisplay = IDTypeDataModel.birthDate10;
                        }
                        else
                            PickerDOBDateDisplay = "";
                    }

                    if (_responseData == null)
                    {
                        IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                        if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                        {
                            FrameIDError = true;

                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));

                        }
                        else
                        {
                            FrameIDError = false;

                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));

                        }
                    }
                    else
                    {
                        FrameIDError = false;
                    }
                    if (MopupService.Instance.PopupStack.Count() > 0)
                        await MopupService.Instance.PopAsync();
                }
                catch
                {
                    try
                    {
                        string Result = await WebServiceManager.GAZTValidateIDTypes(idTypeCode, SelectedIdNumber, dob);
                        IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);

                        if (SignupIsIDTypeValid.error != null && SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                        {
                            if (MopupService.Instance.PopupStack.Count() > 0)
                                await MopupService.Instance.PopAsync();
                            FrameIDError = true;
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));

                        }
                        else
                        {
                            if (SignupIsIDTypeValid.error != null)
                            {
                                FrameIDError = false;
                                if (MopupService.Instance.PopupStack.Count() > 0)
                                    await MopupService.Instance.PopAsync();
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                            }

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

                        if (MopupService.Instance.PopupStack.Count() > 0)
                            await MopupService.Instance.PopAsync();

                        //await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));

                        _navigationService.GoBack();
                    }
                    catch (InternetException ex)
                    {
                        if (MopupService.Instance.PopupStack.Count() > 0)
                            await MopupService.Instance.PopAsync();
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));


                    }
                    catch (HttpRequestException ex)
                    {


                        string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                        // IsLoading = false;
                        if (MopupService.Instance.PopupStack.Count() > 0)
                            await MopupService.Instance.PopAsync();
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                    }
                    catch (Exception)
                    {


                        string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                        if (MopupService.Instance.PopupStack.Count() > 0)
                            await MopupService.Instance.PopAsync();
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));

                    }
                }


            }

            IsLoading = false;
        }

        public async Task ValidateIDNumberForIndiviualOutlets()
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
                    await MopupService.Instance.PushAsync(App.ActivityIndicatorView, false);


                    string Result = await TaxEvasionWebServiceManager.GAZTVATSignUpValidateIDTypesStringResp(idTypeCode, SelectedIdNumber, dob);

                    if (IDTypeDataModel == null)
                    {
                        IDTypeDataModel = new VATSignUpD();
                    }

                    string _responseData = JObject.Parse(Result)["d"].ToString();
                    IDTypeDataModel = JsonConvert.DeserializeObject<VATSignUpD>(_responseData);
                    if (IDTypeDataModel != null)
                    {
                        FirstName = IDTypeDataModel.name1;
                        SurName = IDTypeDataModel.name2;
                        FatherName = IDTypeDataModel.fatherName;
                        GrandfatherName = IDTypeDataModel.grandfatherName;
                        FamilyName = IDTypeDataModel.familyName;
                    }

                    if (!string.IsNullOrEmpty(IDTypeDataModel.name1)) FirstNameFromIdType = IDTypeDataModel.name1;
                    if (!string.IsNullOrEmpty(IDTypeDataModel.TIN)) TINNumber = IDTypeDataModel.TIN;
                    if (!string.IsNullOrEmpty(IDTypeDataModel.birthDate10))
                    {
                        SelectedDob = IDTypeDataModel.birthDate10;
                        TransferPickerDOBDateDisplay = IDTypeDataModel.birthDate10;
                    }
                    else
                    {
                        SelectedDob = "";
                        TransferPickerDOBDateDisplay = "";
                    }

                    if (SelectedIdtype == AppResources.TinDeregistrationGCCID && (!string.IsNullOrEmpty(IDTypeDataModel.name1) || !string.IsNullOrEmpty(TINNumber)))
                    {

                        FirstNameText.IsEditable = false;
                        SurnameText.IsEditable = false;
                        FamilyNameText.IsEditable = false;
                        FathersNameText.IsEditable = false;
                        GrandFathersNameText.IsEditable = false;
                        DobText.IsEditable = false;
                    }
                    else if (SelectedIdtype == AppResources.TinDeregistrationGCCID && (string.IsNullOrEmpty(IDTypeDataModel.name1) && string.IsNullOrEmpty(TINNumber)))
                    {
                        FirstNameText.IsEditable = true;
                        SurnameText.IsEditable = true;
                        FamilyNameText.IsEditable = true;
                        FathersNameText.IsEditable = true;
                        GrandFathersNameText.IsEditable = true;
                        DobText.IsEditable = true;
                    }
                    if (_responseData == null)
                    {
                        IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                        if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                        {
                            FrameIDError = true;
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));

                        }
                        else
                        {
                            FrameIDError = false;

                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));

                        }
                    }
                    else
                    {
                        FrameIDError = false;
                    }
                    if (MopupService.Instance.PopupStack.Count() > 0)
                        await MopupService.Instance.PopAsync();
                }
                catch (Exception)
                {
                    try
                    {
                        string Result = await WebServiceManager.GAZTValidateIDTypes(idTypeCode, SelectedIdNumber, dob);
                        IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);

                        if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                        {
                            if (MopupService.Instance.PopupStack.Count() > 0)
                                await MopupService.Instance.PopAsync();
                            FrameIDError = true;
                            //await _dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));

                        }
                        else
                        {
                            //FrmIDNumber.HasError = false;
                            FrameIDError = false;
                            if (MopupService.Instance.PopupStack.Count() > 0)
                                await MopupService.Instance.PopAsync();
                            // await _dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));

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
                        if (MopupService.Instance.PopupStack.Count() > 0)
                            await MopupService.Instance.PopAsync();

                        //   await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));

                        _navigationService.GoBack();
                    }
                    catch (InternetException ex)
                    {
                        if (MopupService.Instance.PopupStack.Count() > 0)
                            await MopupService.Instance.PopAsync();
                        // await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));


                    }
                    catch (HttpRequestException)
                    {
                        string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                        if (MopupService.Instance.PopupStack.Count() > 0)
                            await MopupService.Instance.PopAsync();
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));

                    }
                    catch (Exception)
                    {


                        string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                        if (MopupService.Instance.PopupStack.Count() > 0)
                            await MopupService.Instance.PopAsync();
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                    }
                }


            }

            IsLoading = false;
        }


        public async Task ValidateIDNumberForIndiviualPermit(PermitSetResult selectedPermit = null)
        {
            string dob = PkrDBO.Replace("/", "");
            string idTypeCode = string.Empty;
            if (selectedPermit == null)
            {
                selectedPermit = SelectedOutletForCloseTranser?.PermitTypes.FirstOrDefault(x => x.APermitNoTb == tempIdTypePermitSetResult.APermitNoTb || x.APermitNoTb == selectedAPermitReason);
                idTypeCode = selectedPermit?.aPermitIdTypeTb;
            }
            else
                idTypeCode = selectedPermit.aPermitIdTypeTb;
            if (!string.IsNullOrEmpty(selectedPermit?.APermitIdNoTb))
            {
                try
                {
                    await MopupService.Instance.PushAsync(App.ActivityIndicatorView, false);


                    string Result = await TaxEvasionWebServiceManager.GAZTVATSignUpValidateIDTypesStringResp(idTypeCode, selectedPermit?.APermitIdNoTb, dob);

                    string _responseData = JObject.Parse(Result)["d"].ToString();
                    var IDTypeDataModel = JsonConvert.DeserializeObject<VATSignUpD>(_responseData);

                    selectedPermit.APermitTransTinTb = IDTypeDataModel.TIN;
                    selectedPermit.APermitNm3Tb = IDTypeDataModel.name1;
                    selectedPermit.APermitNm4Tb = IDTypeDataModel.name2.Replace('.', ' ');
                    selectedPermit.APermitNm5Tb = IDTypeDataModel.fatherName;
                    selectedPermit.APermitNm6Tb = IDTypeDataModel.grandfatherName;
                    selectedPermit.APermitNm7Tb = IDTypeDataModel.familyName;
                    selectedPermit.APermitDeregDisplayDobDate = IDTypeDataModel.birthDate10;

                    selectedPermit.APermitEditable = false;

                    if (selectedPermit?.PermitIdTypeName == AppResources.TinDeregistrationGCCID)
                    {
                        if (!string.IsNullOrEmpty(IDTypeDataModel.name1))
                            FirstNameText.IsEditable = false;
                        if (!string.IsNullOrEmpty(IDTypeDataModel.name2)) SurnameText.IsEditable = false;
                        if (!string.IsNullOrEmpty(IDTypeDataModel.fatherName)) FathersNameText.IsEditable = false;
                        if (!string.IsNullOrEmpty(IDTypeDataModel.grandfatherName)) GrandFathersNameText.IsEditable = false;
                        if (!string.IsNullOrEmpty(IDTypeDataModel.familyName)) FamilyNameText.IsEditable = false;
                        if (!string.IsNullOrEmpty(IDTypeDataModel.birthDate10)) DobText.IsEditable = false;
                        else
                            DobText.IsEditable = true;
                        if (string.IsNullOrWhiteSpace(IDTypeDataModel?.birthDate10) && string.IsNullOrWhiteSpace(IDTypeDataModel.TaxpDob))
                        {
                            selectedPermit.APermitEditable = true;
                        }
                        if (!string.IsNullOrEmpty(IDTypeDataModel.birthDate10))
                        {
                            DobText.IsEditable = false;
                            PickerDOBDateDisplay = IDTypeDataModel.birthDate10;
                        }
                        else
                        {
                            DobText.IsEditable = true;
                            PickerDOBDateDisplay = "";
                        }
                    }

                    if (_responseData == null)
                    {
                        IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                        if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                        {
                            FrameIDError = true;

                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));

                        }
                        else
                        {
                            FrameIDError = false;


                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));

                        }
                    }
                    else
                    {
                        FrameIDError = false;
                    }
                    if (MopupService.Instance.PopupStack.Count() > 0)
                        await MopupService.Instance.PopAsync();
                }
                catch (Exception)
                {
                    try
                    {
                        string Result = await WebServiceManager.GAZTValidateIDTypes(idTypeCode, selectedPermit?.APermitIdNoTb, dob);
                        IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);

                        if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                        {
                            if (MopupService.Instance.PopupStack.Count() > 0)
                                await MopupService.Instance.PopAsync();
                            FrameIDError = true;
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));

                        }
                        else
                        {
                            FrameIDError = false;
                            if (MopupService.Instance.PopupStack.Count() > 0)
                                await MopupService.Instance.PopAsync();
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));

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

                        if (MopupService.Instance.PopupStack.Count() > 0)
                            await MopupService.Instance.PopAsync();

                        //  await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));

                        _navigationService.GoBack();
                    }
                    catch (InternetException ex)
                    {
                        if (MopupService.Instance.PopupStack.Count() > 0)
                            await MopupService.Instance.PopAsync();
                        //    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                    }
                    catch (HttpRequestException)
                    {
                        string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                        if (MopupService.Instance.PopupStack.Count() > 0)
                            await MopupService.Instance.PopAsync();
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));

                    }
                    catch (Exception)
                    {
                        string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                        if (MopupService.Instance.PopupStack.Count() > 0)
                            await MopupService.Instance.PopAsync();
                        // await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));

                    }
                }


            }

            IsLoading = false;
        }

        public async Task ValidateIdNumberFromApi(string tinNumber)
        {
            try
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
                    _responseData = JObject.Parse(resultData)["result"].ToString();
                    IDTypeDataModel = JsonConvert.DeserializeObject<VATSignUpD>(_responseData);
                    FirstNameFromIdType = IDTypeDataModel.name1;
                    if (string.IsNullOrWhiteSpace(_responseData))
                    {
                        IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(resultData);
                        if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                        {
                            App.HideProgressView();
                            FrameIDError = true;

                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));

                        }
                        else
                        {
                            FrameIDError = false;
                            App.HideProgressView();
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));

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

                            TINNumber = IDTypeDataModel.TIN;
                            SelectedIdNumber = IDTypeDataModel.Idnum;

                            SelectedDob = IDTypeDataModel.birthDate10;
                            PickerDOBDateDisplay = IDTypeDataModel.birthDate10;

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
                    IsLoading = false;
                }
                catch (InternetException ex)
                {
                    IsLoading = false;
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));


                    _navigationService.GoBack();
                }
                IsLoading = false;
            }
            catch (GAZTVATChangeFillingPeriodException ex)
            {
                IsLoading = false;
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                _navigationService.GoBack();
            }
            catch (GAZTErrorException ex)
            {
                IsLoading = false;
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
            }
            catch (Exception)
            {

                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));

                _navigationService.GoBack();
                IsLoading = false;
            }
        }


        public async Task ValidateIdNumberForPermitTypes(string tinNumber)
        {
            try
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
                            App.HideProgressView();

                            FrameIDError = true;

                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));

                        }
                        else
                        {
                            FrameIDError = false;
                            App.HideProgressView();

                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));

                        }
                    }
                    else
                    {
                        IBANType idType = IBANTypesList.Where(m => m.key == IDTypeDataModel.Idtype).FirstOrDefault();

                        SelectedOutletForCloseTranser.PermitTypes = new List<PermitSetResult>(SelectedOutletForCloseTranser.PermitTypes.ToList().Select(
                          x =>
                          {
                              x.APermitIdTypeTb = idType.Text;
                              x.APermitIdNoTb = IDTypeDataModel.Idnum;
                              x.APermitNm1Tb = IDTypeDataModel.name1;
                              x.APermitNm2Tb = IDTypeDataModel.name2;
                              x.APermitNm3Tb = IDTypeDataModel.name1;
                              x.APermitNm4Tb = IDTypeDataModel.name2;
                              x.APermitNm5Tb = IDTypeDataModel.fatherName;
                              x.APermitNm6Tb = IDTypeDataModel.grandfatherName;
                              x.APermitNm7Tb = IDTypeDataModel.familyName;
                              if (!string.IsNullOrEmpty(IDTypeDataModel.birthDate10))
                              {
                                  x.APermitDobTb = ConvertDateFormat(IDTypeDataModel.birthDate10);
                              }
                              x.APermitDeregDisplayDobDate = IDTypeDataModel.birthDate10;
                              x.APermitIdTypeTb = IDTypeDataModel.Idtype;
                              return x;
                          }
                          ).ToList());
                    }

                    IsLoading = false;
                }
                catch (InternetException ex)
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                    IsLoading = false;
                    _navigationService.GoBack();
                }
                IsLoading = false;
            }
            catch (GAZTVATChangeFillingPeriodException ex)
            {
                IsLoading = false;
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                _navigationService.GoBack();
            }
            catch (Exception ex)
            {
                IsLoading = false;
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                _navigationService.GoBack();
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
            catch (Exception)
            {


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
            catch (Exception)
            {


            }

        }
        public void EnableReasonView()
        {
            CurrentStep = ProcessStep.Step1;

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
            if (TinDeregistrationData.OutletSet != null)
            {
                if (!flagCB)
                {
                    if (TinDeregistrationData.OutletSet.Length != 0)
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
                AllOutlets = new List<OutletSetResult>(TinDeregistrationData.OutletSet);
                List<PermitSetResult> allPermitTypes = new List<PermitSetResult>(TinDeregistrationData.PermitSet);

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
                                permitInfo.APermitEffDtCTb = "Gregorian";
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


                if (isSaveAsDraftCalledForAttachment == true)
                {
                    TinDeregistrationResponseModel zakatDeregResponseData = new TinDeregistrationResponseModel();
                    zakatDeregResponseData.Approvez = "";
                    zakatDeregResponseData.Rejectz = "";

                    zakatDeregResponseData = await TINDeregistrationWebServiceManager.GaztTinDeregistrationNewRequestData(zakatDeregResponseData);

                    TinDeregistrationData.AttDetSet = zakatDeregResponseData.AttDetSet;



                    foreach (TinDeregestrationAttachmentsModel tinDeregestrationAttachmentsModel in AttachmentsListViewData)
                    {
                        if (tinDeregestrationAttachmentsModel.AttachmentTypeList != null)
                        {
                            tinDeregestrationAttachmentsModel.AttachmentTypeList.Clear();
                        }
                    }

                    foreach (Attachment attachmentTemp in TinDeregistrationData.AttDetSet)
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
                    TinDeregistrationData.AttDetSet?.Clear();
                    foreach (var item in AttachmentsListViewData)
                    {
                        if (item.AttachmentTypeList != null && item.AttachmentTypeList.Count >= 0)
                            TinDeregistrationData.AttDetSet.AddRange(item.AttachmentTypeList);
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


                    foreach (Attachment attachmentTemp in TinDeregistrationData.AttDetSet)
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
                    TinDeregistrationData.AttDetSet?.Clear();
                    foreach (var item in AttachmentsListViewData)
                    {
                        if (item.AttachmentTypeList != null && item.AttachmentTypeList.Count >= 0)
                            TinDeregistrationData.AttDetSet.AddRange(item.AttachmentTypeList);
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
            catch (Exception)
            {

            }
        }

        public async Task EnableDeclarationView()
        {
            if (TinDeregistrationData.AttDetSet != null)
            {
                if (TinDeregistrationData.AttDetSet.Count != 0)
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
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

                }
            }
        }

        public void EnableSummaryView()
        {
            UpdateAttachments();

            CurrentStep = ProcessStep.Step5;
            IsReasonViewEnabled = false;
            IsOutletViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsDeclarationViewEnabled = false;
            IsSummaryViewEnabled = true;
        }

        private void UpdateAttachments()
        {
            foreach(var item in TinDeregistrationData.AttDetSet)
            {
                SummaryAttachments.Add(item);
            }
            OnPropertyChanged("SummaryAttachments");
            
        }

        public async Task GoBackBtnClicked()
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

                            if (isSaveAsDraftCalledForAttachment == true)
                            {
                                AttachmentsListViewData.Clear();
                                TinDeregistrationData.AttDetSet.Clear();
                            }

                            EnableOutletDetaislView();

                            break;
                        }
                    case ProcessStep.Step4:
                        {
                            try
                            {
                                await EnableAttachmentsView();
                            }
                            catch (Exception)
                            {

                            }

                            break;
                        }
                    case ProcessStep.Step5:
                        {
                            await EnableDeclarationView();
                            break;
                        }
                }

            }
            catch (InternetException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                _navigationService.GoBack();
            }
        }

        public async void ReasonContinueBtnClicked()
        {
            try
            {
                try
                {
                    if (SelectedIdtype == AppResources.TinDeregistrationGCCID)
                    {
                        IDTypeDataModel.name2 = SurName;
                    }


                    if (SelectedReason == null)
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

                        return;
                    }
                    else if (SelectedOutletOption == null)
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

                        return;
                    }
                    else if (SelectedOutletOption.OutletOptionIndex == null)
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

                        return;
                    }
                    else if (IsDeRegistrationValid == false)
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

                        return;
                    }
                    else
                    {
                        if (SelectedReason.ReasonCd != null)
                        {
                            try
                            {
                                AllOutlets = new List<OutletSetResult>(TinDeregistrationData.OutletSet);
                                List<PermitSetResult> allPermitTypes = new List<PermitSetResult>();
                                try
                                {
                                    if (TinDeregistrationData.PermitSet != null)
                                    {
                                        allPermitTypes = new List<PermitSetResult>(TinDeregistrationData.PermitSet);
                                    }
                                }
                                catch (Exception)
                                {
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
                                                outletInfo.AOutletEffDtHTb = SingleDeregistrationDate == null ? "" : SingleDeregistrationDate;
                                                outletInfo.AOutletEffDtCTb = "Gregorian";
                                                outletInfo.AOutletEffDtTb = ConvertDateFormat(SingleDeregistrationDate);
                                            }


                                            foreach (PermitSetResult permitInfo in allPermitTypes)
                                            {
                                                if (permitInfo.APermitOutletnoTb == outletInfo.AOutletNoTb)
                                                {

                                                    if (TinDeregistrationData.ADregOpt == "1" || TinDeregistrationData.ADregOpt == "2")
                                                    {
                                                        if (string.IsNullOrEmpty(permitInfo.aPermitIdTypeTb) || string.IsNullOrEmpty(permitInfo.APermitIdNoTb) || string.IsNullOrEmpty(permitInfo.APermitTransTinTb))
                                                        {
                                                            permitInfo.APermitDregRsnTb = "1";
                                                            permitInfo.ReasonDescription = AppResources.TinDeregistrationClosed;
                                                        }
                                                    }

                                                    if (SelectedOutletOptionIndex == 0 || SelectedOutletOptionIndex == 2)
                                                    {
                                                        if (string.IsNullOrEmpty(permitInfo.aPermitIdTypeTb) || string.IsNullOrEmpty(permitInfo.APermitIdNoTb) || string.IsNullOrEmpty(permitInfo.APermitTransTinTb))
                                                        {
                                                            permitInfo.APermitDregRsnTb = "1";
                                                            permitInfo.ReasonDescription = AppResources.TinDeregistrationClosed;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (string.IsNullOrEmpty(permitInfo.aPermitIdTypeTb) || string.IsNullOrEmpty(permitInfo.APermitIdNoTb) || string.IsNullOrEmpty(permitInfo.APermitTransTinTb))
                                                        {
                                                            permitInfo.APermitDregRsnTb = "3";
                                                            permitInfo.ReasonDescription = AppResources.TinDeregistrationTransfer;
                                                        }
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
                                                            permitInfo.APermitEffDtCTb = "Gregorian";
                                                            permitInfo.APermitEffDtTb = ConvertDateFormat(DeregistrationDate);

                                                        }
                                                        catch (Exception)
                                                        {


                                                        }

                                                    }
                                                    else
                                                    {

                                                    }


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
                                                        if (!string.IsNullOrEmpty(PickerDobToDisplay))
                                                        {
                                                            DateTime dt = Convert.ToDateTime(PickerDobToDisplay);
                                                            permitInfo.APermitDeregDisplayDate = dt.ToString("yyyy/MM/dd");//DeregistrationDate.ToString("dd MMM yyyy");

                                                        }
                                                        else
                                                        {
                                                            if (TinDeregistrationData.BgDregFlg == "X" && TinDeregistrationData.AEffectiveDt != null && SelectedOutletOption.OutletOptionIndex == "1")
                                                            {
                                                                DeregistrationDate = Convert.ToDateTime(TinDeregistrationData.AEffectiveDt);
                                                                PickerDobToDisplay = DateTime.Parse(TinDeregistrationData.AEffectiveDt).Date.ToString("dd/MM/yyyy");

                                                            }
                                                            else
                                                            {
                                                                PickerDobToDisplay = "";
                                                            }// 3994 Issue fixed with zero outlets
                                                        }

                                                        if (!string.IsNullOrEmpty(DeregistrationDate.ToString()))
                                                        {
                                                            permitInfo.APermitEffDtHTb = DeregistrationDate.ToString("yyyyMMdd");
                                                            permitInfo.APermitEffDtTb = ConvertDateFormat(DeregistrationDate);
                                                        }
                                                        permitInfo.APermitEffDtCTb = "Gregorian";



                                                    }

                                                    tempPermitTypes.Add(permitInfo);
                                                }
                                            }
                                        }
                                        else
                                        {

                                            outletInfo.AOutletEffDtHTb = DeregistrationDate.ToString("yyyy/MM/dd");
                                            outletInfo.AOutletEffDtTb = ConvertDateFormat(DeregistrationDate);
                                            outletInfo.AOutletEffDtCTb = "Gregorian";
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
                                catch (Exception)
                                {


                                }
                            }
                            catch (Exception)
                            {


                            }

                        }
                        else
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

                            return;
                        }
                    }
                }
                catch (Exception)
                {


                }

                if (SelectedOutletOption.OutletOptionIndex == "2")
                {
                    if (string.IsNullOrEmpty(SelectedIdNumber) || string.IsNullOrEmpty(SelectedReason.ReasonDesc) || string.IsNullOrEmpty(SelectedIdtype))
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

                        return;
                    }
                    else
                    {
                        if (TINNumber == App.LoginDataRetrieved.TIN)
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

                            return;
                        }
                        else if (SelectedIdtype == AppResources.TinDeregistrationCompanyID)
                        {
                            if (string.IsNullOrEmpty(SelectedIdNumber) || string.IsNullOrEmpty(PickerDobToDisplay) || string.IsNullOrEmpty(SelectedIdtype)) // If Taxpayer enter TIN Number, That will point one of the Id Type from the list
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

                                return;
                            }
                        }
                        else if (SelectedIdtype == AppResources.TinDeregistrationNationalID)
                        {
                            if (string.IsNullOrEmpty(SelectedIdNumber) || string.IsNullOrEmpty(PickerDobToDisplay) || string.IsNullOrEmpty(FirstNameFromIdType) || string.IsNullOrEmpty(IDTypeDataModel.name2) || string.IsNullOrEmpty(PickerDOBDateDisplay) || string.IsNullOrEmpty(SelectedIdtype))
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

                                return;
                            }
                        }
                        else if (SelectedIdtype == AppResources.TinDeregistrationGCCID)
                        {
                            if (string.IsNullOrEmpty(SelectedIdNumber) || string.IsNullOrEmpty(FirstNameFromIdType) || string.IsNullOrEmpty(IDTypeDataModel.name2) || string.IsNullOrEmpty(PickerDobToDisplay) || string.IsNullOrEmpty(PickerDOBDateDisplay) || string.IsNullOrEmpty(SelectedIdtype))
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

                                return;
                            }
                        }
                        else if (SelectedIdtype == AppResources.TinDeregistrationIQAMANumber)
                        {
                            if (string.IsNullOrEmpty(SelectedIdNumber) || string.IsNullOrEmpty(PickerDOBDateDisplay) || string.IsNullOrEmpty(FirstNameFromIdType) || string.IsNullOrEmpty(IDTypeDataModel.name2) || string.IsNullOrEmpty(PickerDobToDisplay) || string.IsNullOrEmpty(SelectedIdtype))
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

                                return;
                            }
                        }

                        EnableOutletDetaislView();
                    }

                }
                else if (SelectedOutletOption.OutletOptionIndex == "3")
                {
                    if (SelectedReason == null)
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

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
                        // await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

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

                if (MopupService.Instance.PopupStack.Count > 0)
                    await MopupService.Instance.PopAllAsync();
            }
            catch (InternetException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                _navigationService.GoBack();
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
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

                        return;
                    }
                    else
                    {
                        if (SelectedIdtype == AppResources.TinDeregistrationCompanyID)
                        {
                            if (string.IsNullOrEmpty(SelectedIdNumber))
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

                                return;
                            }
                        }
                        else if (SelectedIdtype == AppResources.TinDeregistrationNationalID)
                        {
                            if (string.IsNullOrEmpty(SelectedIdNumber) || string.IsNullOrEmpty(SelectedDob) || string.IsNullOrEmpty(IDTypeDataModel.name1) || string.IsNullOrEmpty(IDTypeDataModel.name2))
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

                                return;
                            }
                        }
                        else if (SelectedIdtype == AppResources.TinDeregistrationGCCID)
                        {
                            if (string.IsNullOrEmpty(SelectedIdNumber) || (DobText.IsEditable && string.IsNullOrEmpty(SelectedDob)) || FirstNameText.IsEditable && string.IsNullOrEmpty(IDTypeDataModel.name1) || SurnameText.IsEditable && string.IsNullOrEmpty(IDTypeDataModel.name2))
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

                                return;
                            }
                        }
                        else if (SelectedIdtype == AppResources.TinDeregistrationIQAMANumber)
                        {
                            if (string.IsNullOrEmpty(SelectedIdNumber) || string.IsNullOrEmpty(SelectedDob) || string.IsNullOrEmpty(IDTypeDataModel.name1) || string.IsNullOrEmpty(IDTypeDataModel.name2))
                            {
                                //  await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

                                return;
                            }
                        }

                        SelectedOutletForCloseTranser.ReasonDescription = AppResources.TinDeregistrationTransfer;
                        SetDataForTransferToASingleTranferee();
                        EnableOutletDetaislView();
                    }

                }
                else if (SelectedPermitOutletOptionIndex == 2)
                {
                    //TODO validation for transfer/close to indiviual case missing
                    if (SelectedReason == null)
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

                        return;
                    }
                    else if (SelectedOutletForCloseTranser.PermitTypes.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.APermitDeregDisplayDate)) != null)
                    {
                         await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

                        return;
                    }
                    else if (SelectedOutletForCloseTranser.PermitTypes.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.APermitDregRsnTb)) != null)
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

                        return;
                    }
                    else if (SelectedOutletForCloseTranser.PermitTypes.FirstOrDefault(x => x.APermitDregRsnTb == "3") != null)
                    {
                        var transferrred = SelectedOutletForCloseTranser.PermitTypes.Where(x => x.APermitDregRsnTb == "3");

                        if (transferrred.FirstOrDefault(x => x.APermitTransTinTb == App.LoginDataRetrieved.TIN) != null)
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.TinDeregistrationSameNotAllow));
                            return;
                        }
                        else if (transferrred.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.APermitIdNoTb)) != null)
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

                            return;
                        }
                        else if (transferrred.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.APermitDeregDisplayDobDate)) != null)
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

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
                        //  await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

                        return;
                    }
                    SelectedOutletForCloseTranser.ReasonDescription = AppResources.TinDeregistrationClosed;
                    //prepare data for Outlet and Outlet related Permits for Closure
                    var index = TinDeregistrationData.OutletSet.ToList().IndexOf(SelectedOutletForCloseTranser);
                    // close the selected outlet, 1 for Close and 3 for Transfer
                    TinDeregistrationData.OutletSet[index].AOutletDregOptTb = "1";
                    if (IsHijriCal)
                    {
                        TinDeregistrationData.OutletSet[index].AOutletEffDtCTb = "Hijri";
                    }
                    else
                    {
                        TinDeregistrationData.OutletSet[index].AOutletEffDtCTb = "Gregorian";
                    }
                    TinDeregistrationData.OutletSet[index].AOutletEffDtHTb = SingleDeregistrationDate;
                    TinDeregistrationData.OutletSet[index].AOutletEffDtTb = ConvertDateFormat(Convert.ToDateTime(SingleDeregistrationDate));
                    foreach (var item in TinDeregistrationData.PermitSet)
                    {
                        if (item.APermitOutletnoTb == SelectedOutletForCloseTranser.AOutletNoTb)
                        {
                            // close all the permits for selected outlet, 1 for Close and 3 for Transfer
                            item.APermitDregRsnTb = "1";
                            if (IsHijriCal)
                            {
                                item.APermitEffDtCTb = "Hijri";
                                string date = UtilityManager.HijriToGreg(SingleDeregistrationDate);
                                item.APermitEffDtTb = ConvertDateFormat(Convert.ToDateTime(SingleDeregistrationDate));

                            }
                            else
                            {
                                item.APermitEffDtCTb = "Gregorian";
                                item.APermitEffDtTb = ConvertDateFormat(Convert.ToDateTime(SingleDeregistrationDate));

                            }
                            item.APermitEffDtHTb = SingleDeregistrationDate;
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

                if (MopupService.Instance.PopupStack.Count > 0)
                    await MopupService.Instance.PopAllAsync();
            }
            catch (InternetException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                _navigationService.GoBack();
            }
            catch (Exception ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
            }
        }

        public async void AddPopUpPage(List<TINDeregistrationPageViewModel> viewModel = null)
        {
            try
            {
                await MopupService.Instance.PushAsync(new TINDeregistrationCloseIndividualOutletsPageView(SelectedReason.ReasonDesc, this));
            }
            catch (InternetException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                _navigationService.GoBack();
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

        public async Task OutletContinueBtnClicked()
        {
            if (!IsOutletContinueButtonEnabled)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

                return;
            }
            try
            {
                if (IsOutletChecked)
                {

                    await PopulateAttachmentsListViewTemplate();
                    await EnableAttachmentsView();
                }
            }
            catch (InternetException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                _navigationService.GoBack();
            }
        }

        public async Task AttachmentsContinueBtnClicked()
        {
            try
            {
                TinDeregistrationData.AttDetSet?.Clear();
                foreach (var item in AttachmentsListViewData)
                {
                    if (item.AttachmentTypeList != null && item.AttachmentTypeList.Count >= 0)
                        TinDeregistrationData.AttDetSet.AddRange(item.AttachmentTypeList);
                }
                bool isMandatoryDocAttached = false;
                foreach (TinDeregestrationAttachmentsModel reqAttachment in AttachmentsListViewData)
                {
                    if (reqAttachment.IsMandatory)
                    {
                        isMandatoryDocAttached = TinDeregistrationData.AttDetSet.Any(attachedDocs => attachedDocs.Dotyp == reqAttachment.DocType);
                        if (!isMandatoryDocAttached)
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

                            break;
                        }
                    }

                }
                if (!isMandatoryDocAttached)
                    return;
                await EnableDeclarationView();
            }
            catch (GAZTUnlockAccountException)
            {
                App.HideProgressView();
            }
            catch (InternetException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                _navigationService.GoBack();
            }
        }

        public async Task DeclarationContinueBtnClicked()
        {
            try
            {
                TinDeregistrationData.AttDetSet = new List<Attachment>();
                TinDeregistrationData.AttDetSet = attachmentList;
                if (IsDeclarationChecked)
                {
                    PopulateSummaryReasonData();
                    PopulateSummaryDeclarationData();
                    if (TinDeregistrationData.ADecName == string.Empty || TinDeregistrationData.ADecDesig == string.Empty || TinDeregistrationData.ADecTelNo == string.Empty)
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

                    }
                    else
                    {
                        EnableSummaryView();
                    }
                }
            }
            catch (InternetException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                _navigationService.GoBack();
            }
        }




        public async Task SummaryContinueBtnClicked()
        {
            try
            {
                //Display Success Screen
                await Submit();
                if (isSubmitted)
                {
                    await _navigationService.NavigateTo(App.TINDeregestrationSuccessPageView, TinDeregistrationData);
                }
            }
            catch (InternetException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                _navigationService.GoBack();
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


        }

        public async Task OnTinDeregOutletDeregDatePickerClicked()
        {
            try
            {
                GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
                genericDatePickerModel.DatePickerTitle = AppResources.VatDeregStartDatePickerTitle;
                genericDatePickerModel.PickerId = "OutletDeregDatePicker";

                try
                {
                    await MopupService.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel, true));
                }

                catch (InternetException ex)
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                    _navigationService.GoBack();
                }
            }

            catch (InternetException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                _navigationService.GoBack();
            }
        }

        public async Task OnTinRegisrtationReasonDateClicked()
        {
            try
            {
                GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
                genericDatePickerModel.DatePickerTitle = AppResources.TinDeregistrationDate;
                genericDatePickerModel.PickerId = "DeregDatePicker";

                try
                {
                    await MopupService.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel));
                }
                catch (InternetException ex)
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                    _navigationService.GoBack();
                }
            }
            catch (InternetException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                _navigationService.GoBack();
            }
        }

        #region Attachments View
        public Task PopulateAttachmentsListViewTemplate()
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
                    if (TinDeregistrationData.PermitSet != null && TinDeregistrationData.PermitSet != null)
                    {
                        foreach (var item in TinDeregistrationData.PermitSet)
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

                return Task.CompletedTask;
            }

            catch (Exception)
            {

                return Task.CompletedTask;
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

            foreach (Attachment attachmentTemp in TinDeregistrationData.AttDetSet)
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
            TinDeregistrationData.AttDetSet?.Clear();
            foreach (var item in AttachmentsListViewData)
            {
                if (item.AttachmentTypeList != null && item.AttachmentTypeList.Count >= 0)
                    TinDeregistrationData.AttDetSet.AddRange(item.AttachmentTypeList);
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
        public async Task NewAttachmentClicked()
        {

            if (MopupService.Instance.PopupStack.Count > 0) return;
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
                foreach (Attachment attachmentTemp in TinDeregistrationData.AttDetSet)
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
                TinDeregistrationData.AttDetSet?.Clear();
                foreach (var item in AttachmentsListViewData)
                {
                    if (item.AttachmentTypeList != null && item.AttachmentTypeList.Count >= 0)
                    {
                        if (item.DocType == SelectedAttachment.DocType)
                            attachmentsList.AddRange(item.AttachmentTypeList);
                    }
                }

                numberOfAttachmentSentToAttachmentPopUp = attachmentsList.Count;
                await MopupService.Instance.PushAsync(new FilesUploadPopUpPageView(attachmentsList, WhichAttachment.TINDeregistration
                        , TinDeregistrationData.CaseGuid, SelectedAttachment.DocType));

            }
            catch (InternetException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                _navigationService.GoBack();
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
            catch (Exception)
            {


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
            catch (Exception)
            {


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
                    var index = TinDeregistrationData.OutletSet.ToList().IndexOf(SelectedOutletForCloseTranser);
                    TinDeregistrationData.OutletSet[index].AOutletEffDtHTb = SingleDeregistrationDate;
                    if (IsHijriCal)
                    {
                        TinDeregistrationData.OutletSet[index].AOutletEffDtCTb = "Hijri";
                        TinDeregistrationData.OutletSet[index].AOutletDobCTb = "Hijri";
                    }
                    else
                    {
                        TinDeregistrationData.OutletSet[index].AOutletEffDtCTb = "Gregorian";
                        TinDeregistrationData.OutletSet[index].AOutletDobCTb = "Gregorian";
                    }
                    TinDeregistrationData.OutletSet[index].AOutletEffDtTb = ConvertDateFormat(Convert.ToDateTime(SingleDeregistrationDate));
                    TinDeregistrationData.OutletSet[index].AOutletTransTinTb = TINNumber;
                    TinDeregistrationData.OutletSet[index].AOutletIdTypeTb = SelectedIDTypeCode;
                    TinDeregistrationData.OutletSet[index].AOutletIdNoTb = SelectedIdNumber;
                    TinDeregistrationData.OutletSet[index].AOutletDobHTb = SelectedDob;
                    TinDeregistrationData.OutletSet[index].AOutletDobTb = ConvertDateFormat(Convert.ToDateTime(SelectedDob));
                    if (IsName1Visible)
                    {
                        TinDeregistrationData.OutletSet[index].AOutletNm1Tb = IDTypeDataModel.name1;
                        TinDeregistrationData.OutletSet[index].AOutletNm2Tb = IDTypeDataModel.name2;
                    }
                    else
                    {
                        TinDeregistrationData.OutletSet[index].AOutletNm1Tb = IDTypeDataModel.name1;
                        TinDeregistrationData.OutletSet[index].AOutletNm2Tb = IDTypeDataModel.name2;
                        TinDeregistrationData.OutletSet[index].AOutletNm3Tb = IDTypeDataModel.name1;
                        TinDeregistrationData.OutletSet[index].AOutletNm4Tb = IDTypeDataModel.name2;
                        TinDeregistrationData.OutletSet[index].AOutletNm5Tb = IDTypeDataModel.fatherName;
                        TinDeregistrationData.OutletSet[index].AOutletNm6Tb = IDTypeDataModel.grandfatherName;
                        TinDeregistrationData.OutletSet[index].AOutletNm7Tb = IDTypeDataModel.familyName;
                    }
                    foreach (var item in TinDeregistrationData.PermitSet)
                    {
                        if (IsHijriCal)
                        {
                            item.APermitEffDtCTb = "Hijri";
                            item.APermitDobCTb = "Hijri";
                        }
                        else
                        {
                            item.APermitEffDtCTb = "Gregorian";
                            item.APermitDobCTb = "Gregorian";
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
                            item.APermitNm3Tb = IDTypeDataModel.name1;
                            item.APermitNm4Tb = IDTypeDataModel.name2;
                        }
                        else
                        {
                            item.APermitNm1Tb = "";
                            item.APermitNm2Tb = "";
                            item.APermitNm3Tb = IDTypeDataModel.name1;
                            item.APermitNm4Tb = IDTypeDataModel.name2;
                            item.APermitNm5Tb = IDTypeDataModel.fatherName;
                            item.APermitNm6Tb = IDTypeDataModel.grandfatherName;
                            item.APermitNm7Tb = IDTypeDataModel.familyName;
                        }
                    }
                }
            }
            catch (Exception)
            {


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
            catch (InternetException)
            {
                App.HideProgressView();
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZInternetConnectionMessage));

            }
            catch (GAZTErrorException ex)
            {
                App.HideProgressView();

                string message = ex.Message;

                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(message));

            }
            catch (Exception)
            {

                App.HideProgressView();
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

                await MopupService.Instance.PushAsync(App.ActivityIndicatorView);

                try
                {
                    if (IsHijriCal)
                    {
                        TinDeregistrationData.ASubmissionDateC = "Hijri";
                        TinDeregistrationData.AEffectiveDtC = "Hijri";
                        TinDeregistrationData.AExpdtC = "Hijri";
                    }
                    else if (IsDOBHijriCal)
                    {
                        TinDeregistrationData.ADobC = "Hijri";
                    }
                    else
                    {
                        TinDeregistrationData.ASubmissionDateC = "Gregorian";
                        TinDeregistrationData.AEffectiveDtC = "Gregorian";
                        TinDeregistrationData.AExpdtC = "Gregorian";
                        TinDeregistrationData.ADobC = "Gregorian";

                    }

                    if (!string.IsNullOrEmpty(PickerDOBDateDisplay))
                    {
                        TinDeregistrationData.ADob = ConvertDateFormat(Convert.ToDateTime(PickerDOBDateDisplay));
                        TinDeregistrationData.ADobH = DeregistrationDate.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        TinDeregistrationData.ADob = null;
                    }

                    TinDeregistrationData.ASubmissionDate = ConvertDateFormat(DeregistrationDate);
                    TinDeregistrationData.ASubmissionDateH = DeregistrationDate.ToString("yyyy-MM-dd");


                    TinDeregistrationData.AEffectiveDt = ConvertDateFormat(DeregistrationDate);

                    TinDeregistrationData.AEffectiveDtH = DeregistrationDate.ToString("yyyy-MM-dd");

                    TinDeregistrationData.ADecDate = ConvertDateFormat(DeregistrationDate);
                    TinDeregistrationData.ADecDateH = DeregistrationDate.ToString("yyyy-MM-dd");

                    TinDeregistrationData.AExpdt = ConvertDateFormat(DeregistrationDate);
                    TinDeregistrationData.AExpdtH = DeregistrationDate.ToString("yyyy-MM-dd");

                    IBANType idType = IBANTypesList.Where(m => m.Text == SelectedIdtype).FirstOrDefault();

                    if (idType != null)
                        SelectedIDTypeCode = idType.key;

                    TinDeregistrationData.AIdType = SelectedIDTypeCode;// "ZS0005"
                    TinDeregistrationData.AIdNo = SelectedIdNumber;
                    //TinDeregistrationData.ATin = TINNumber;
                    if (TinDeregistrationData.ADregOpt == "2")
                    {
                        TinDeregistrationData.ATransTin = TINNumber;
                    }
                    TinDeregistrationData.ANm1 = FirstNameFromIdType;
                    TinDeregistrationData.ANm2 = "";
                    TinDeregistrationData.ANm3 = FirstNameFromIdType;// IDTypeDataModel.Name1;
                    TinDeregistrationData.ANm4 = IDTypeDataModel.name2;
                    TinDeregistrationData.ANm5 = IDTypeDataModel.fatherName;
                    TinDeregistrationData.ANm6 = IDTypeDataModel.grandfatherName;
                    TinDeregistrationData.ANm7 = IDTypeDataModel.familyName;



                    OutletSetResult[] oldOutlets = new OutletSetResult[AllOutlets.Count];
                    AllOutlets.CopyTo(oldOutlets, 0);
                    List<OutletSetResult> listOutlets;
                    listOutlets = new List<OutletSetResult>(TinDeregistrationData.OutletSet);
                    List<PermitSetResult> allPermitTypes = new List<PermitSetResult>(TinDeregistrationData.PermitSet);
                    for (int i = 0; i < oldOutlets.Count(); i++)
                    {
                        listOutlets[i].PermitTypes = oldOutlets[i].PermitTypes;
                    }
                    AllOutlets = listOutlets;

                    foreach (OutletSetResult outletInfo in AllOutlets)
                    {
                        //TODO

                        outletInfo.AOutletDobTb = ConvertDateFormat(DeregistrationDate);
                        if (outletInfo.AOutletDobTb.Contains("/Date("))
                        {
                            outletInfo.AOutletDobTb = DeregistrationDate.ToString("yyyy-MM-ddTHH:mm:ss");
                        }
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
                                    permitInfo.APermitValfrDtCTb = "Gregorian";
                                }
                            }
                        }
                        catch (Exception)
                        {


                        }

                        try
                        {
                            if (TinDeregistrationData.ADregOpt == "3")
                            {
                                if (permitInfo.IsHijiri)
                                {

                                    string date = UtilityManager.HijriToGreg(permitInfo.APermitDeregDisplayDate);
                                    permitInfo.APermitEffDtTb = ConvertDateFormat(date);
                                }
                                else
                                {
                                    permitInfo.APermitEffDtTb = ConvertDateFormat(permitInfo.APermitDeregDisplayDate);

                                }
                                permitInfo.APermitEffDtCTb = "Gregorian";

                                permitInfo.APermitDobCTb = string.IsNullOrEmpty(permitInfo.APermitDobTb) ? "" : "Gregorian";
                                if (permitInfo.IsDOBHijiri)
                                {

                                    string date = UtilityManager.HijriToGreg(permitInfo.APermitDobTb);
                                    permitInfo.APermitDobTb = ConvertDateFormat(date);
                                }
                                else
                                {
                                    permitInfo.APermitDobTb = ConvertDateFormat(permitInfo.APermitDobTb);

                                }
                            }
                        }
                        catch (Exception)
                        {


                        }

                        if (permitInfo.APermitIdNoTb == null)
                            permitInfo.APermitIdNoTb = "";

                        if (permitInfo.APermitTransTinTb == null)
                            permitInfo.APermitTransTinTb = "";

                    }
                }
                catch (Exception)
                {

                }

                List<Attachment> tempAttachDetSet = new List<Attachment>();
                foreach (Attachment attachment in TinDeregistrationData.AttDetSet)
                {
                    tempAttachDetSet.Add(attachment);
                }

                string ErrorMessageForUnlockAccount = string.Empty;
                List<Attachment> AttachmentsCopy = new List<Attachment>(TinDeregistrationData.AttDetSet);
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


                    }
                    else if (!string.IsNullOrEmpty(TinDeregistrationDataResponse))
                    {
                        TinDeregistrationDataResponse = JObject.Parse(TinDeregistrationDataResponse)["result"].ToString();
                        var tempTinDeregData = JsonConvert.DeserializeObject<TinDeregistrationResponseModel>(TinDeregistrationDataResponse);

                        TinDeregistrationData.Fbnum = tempTinDeregData.Fbnum;
                        TinDeregistrationData.Fbnumz = tempTinDeregData.Fbnumz;

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

                    TinDeregistrationData.AttDetSet = tempAttachDetSet;

                    if (TinDeregistrationData.Xvoidz.Equals("X"))
                    {
                        string number = TinDeregistrationData.Fbnum;
                        string displayMessage = AppResources.VATRSuccessFullVoidMessage + " " + number;
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(displayMessage));


                        _navigationService.GoBack();
                    }


                }
                catch (InternetException)
                {


                    isSubmitted = false;

                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZInternetConnectionMessage));
                }
                catch (GAZTErrorException ex)
                {
                    isSubmitted = false;


                    string message = ex.Message;

                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(message));
                }
                catch (Exception)
                {
                    isSubmitted = false;
                }
            }
            catch (InternetException)
            {


                isSubmitted = false;
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZInternetConnectionMessage));
            }
            catch (GAZTErrorException ex)
            {
                isSubmitted = false;


                string message = ex.Message;

                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(message));
            }
            catch (Exception)
            {
                isSubmitted = false;
            }
            finally
            {
                if (MopupService.Instance.PopupStack.Count > 0)
                    await MopupService.Instance.PopAsync(true);
            }
        }

        public async Task OnSaveAsDraftClicked()
        {
            isSubmitted = false;
            switch (CurrentStep)
            {
                case ProcessStep.Step1:
                    {
                        await SaveAsDraft();
                        if (isSubmitted == true)
                        {
                            if (!string.IsNullOrEmpty(PopUpMsgFor2021))
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(PopUpMsgFor2021));
                            else
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.TinDeregDataSavedSuccessfully));
                        }
                        break;

                    }
                case ProcessStep.Step2:
                    {
                        await SaveAsDraft();
                        if (isSubmitted == true)
                        {
                            if (!string.IsNullOrEmpty(PopUpMsgFor2021))
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(PopUpMsgFor2021));
                            else
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.TinDeregDataSavedSuccessfully));
                        }
                        break;
                    }
                case ProcessStep.Step3:
                    {
                        await SaveAsDraft();
                        if (isSubmitted == true)
                        {
                            isSaveAsDraftCalledForAttachment = true;
                            if (!string.IsNullOrEmpty(PopUpMsgFor2021))
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(PopUpMsgFor2021));
                            else
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.TinDeregDataSavedSuccessfully));
                        }
                        break;

                    }
                case ProcessStep.Step4:
                    {
                        await SaveAsDraft();
                        if (isSubmitted == true)
                        {
                            isSaveAsDraftCalledForAttachment = true;
                            if (!string.IsNullOrEmpty(PopUpMsgFor2021))
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(PopUpMsgFor2021));
                            else
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.TinDeregDataSavedSuccessfully));
                        }
                        break;
                    }
                case ProcessStep.Step5:
                    {
                        await SaveAsDraft();
                        if (isSubmitted == true)
                        {
                            isSaveAsDraftCalledForAttachment = true;
                            if (!string.IsNullOrEmpty(PopUpMsgFor2021))
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(PopUpMsgFor2021));
                            else
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.TinDeregDataSavedSuccessfully));
                        }

                        break;

                    }



            }
        }

        public async Task ResetTINDeRegistrationObject()
        {
            try
            {
                IsLoading = true;
                TinDeregistrationResponseModel zakatDeregResponseData = new TinDeregistrationResponseModel();
                zakatDeregResponseData.Approvez = "";
                zakatDeregResponseData.Rejectz = "";

                zakatDeregResponseData = await TINDeregistrationWebServiceManager.GaztTinDeregistrationNewRequestData(zakatDeregResponseData);
                TinDeregistrationData = zakatDeregResponseData;
                await DeletUploadedImage(TinDeregistrationData.AttDetSet);
                TinDeregistrationData?.AttDetSet?.Clear();
                SelectedOutletOption = null;
                PickerDobToDisplay = null;

                IsLoading = false;

            }
            catch (Exception)
            {

            }

        }


        public Task DeletUploadedImage(List<Attachment> AttachmentList)
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
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                return Task.CompletedTask;

            }

        }

        public void DeleteAttachment(Attachment attachment)
        {
            try
            {
                string results = UploadAttachementsWebServiceManager.GAZTGenericDeleteAttachment(attachment.Filename, TinDeregistrationData.CaseGuid, "", attachment.Doguid);
            }
            catch (Exception)
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
            catch (Exception)
            {


                return new List<Attachment>();
            }

        }



        public void SetDefaultReasonLayout()
        {
            int index = Convert.ToInt16(SelectedOutletOption.OutletOptionIndex) - 1;

            SelectedOutletOptionIndex = OutletDecisionOptions.IndexOf(SelectedOutletOption);

            if (SelectedOutletOption.ActiveOutletDecisionOptions.Equals(AppResources.TinDeregistrationTransferAllOutletsToSingle))
            {
                NationalTypeSelected();
                IsDobVisible = true;
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

            }
            else if (IsOption1Visible == true)
            {
                if (FirstNameFromIdType != null)
                {
                    FirstNameFromIdType = string.Empty;
                }

                PickerDobToDisplay = string.Empty;
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
                    IDTypeDataModel.name1 = string.Empty;
                    IDTypeDataModel.name2 = string.Empty;
                    IDTypeDataModel.fatherName = string.Empty;
                    IDTypeDataModel.grandfatherName = string.Empty;
                    IDTypeDataModel.familyName = string.Empty;
                }
                PickerDobToDisplay = string.Empty;
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

            ToggleCheckboxText(SelectedOutletOption.ActiveOutletDecisionOptions.Equals(AppResources.TinDeregistrationTransferAllOutletsToSingle));

        }
        void ToggleCheckboxText(bool isIndex1 = false)
        {
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
            if (SelectedOutletOption.CardLabel.Equals(AppResources.TinDeregistrationTransferAllOutletsToSingle))
            {
                IsOption2Visible = true;
            }
            else if (SelectedOutletOption.CardLabel.Equals(AppResources.TinDeregistrationCloseAllOutlets))
            {
                IsOption1Visible = true;
            }
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
                                IsDobVisible = true;// false;
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

                    if (string.IsNullOrEmpty(SelectedIdtype))
                    {
                        IBANType idType = IBANTypesList.Where(m => m.Text == SelectedIdtype).FirstOrDefault();
                        SelectedIDTypeCode = idType.key;
                    }

                    SelectedIdNumber = TinDeregistrationData.AIdNo;
                    IDTypeDataModel.name1 = TinDeregistrationData.ANm3;
                    FirstNameFromIdType = TinDeregistrationData.ANm3;
                    PickerDOBDateDisplay = TinDeregistrationData.ADobH;//ADob, 
                    PickerDobToDisplay = TinDeregistrationData.AExpdtH;//ASubmissionDateH,ASubmissionDate
                    TINNumber = TinDeregistrationData.ATransTin;
                }

            }
            catch (Exception)
            {


            }

        }

    }
}
