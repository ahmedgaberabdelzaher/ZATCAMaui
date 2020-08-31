using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.Models.ChageFillingPeriodModel;
using EGAZT.Views.NewDesign.GenericPickers;
using EGAZT.Views.NewDesign.ZakatInstalmentPlan;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using System.Threading.Tasks;
using EGAZT.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel;
using EGAZT.Views.NewDesign;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GAZT.Manager;
using GAZT.Models;

namespace EGAZT.ViewModel.NewDesignViewModel.ChangeFillingPeriodViewModel
{
    public class ChangeFillingPeriodViewModel : BaseViewModel
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        int selectedPage = (int)PagesEnum.FrequencyDetailsView;
        #endregion

        #region Enums
        enum PagesEnum
        {
            FrequencyDetailsView,
            AttachmentsView,
            DeclarationView,
            SummaryView,
        }

        public enum PickerEnum
        {
            IdType,
            EffectiveDate
        }
        #endregion

        public PickerEnum selectedPicker = PickerEnum.EffectiveDate;

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

        private bool _isFrequencyDetailsChecked = false;
        public bool IsFrequencyDetailsChecked
        {
            get
            {
                return _isFrequencyDetailsChecked;
            }
            set
            {
                _isFrequencyDetailsChecked = value;
                RaisePropertyChanged("IsFrequencyDetailsChecked");
            }
        }

        #region Commands

        public ICommand GoBackClick { get; set; }
        public ICommand CloseClick { get; set; }
        public ICommand FrequencyContinueBtnTapped { get; set; }
        public ICommand AttachmentsContinueBtnTapped { get; set; }
        public ICommand DeclarationContinueBtnTapped { get; set; }
        public ICommand GoBackToFrequencyDetails { get; set; }
        public ICommand GoBackToAttachments { get; set; }
        public ICommand GoBackToDeclaration { get; set; }
        public ICommand ShowDatePicker { get; set; }
        public ICommand EffectiveDateSpinnerClicked { get; set; }
        public ICommand IdTypeSpinnerTapped { get; set; }
        public ICommand NewAttachmentTapped { get; set; }
        #endregion

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
        public int MaxIndex { get; private set; } = 4;

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

        private string _effectiveDatePicked = "";

        public string EffectiveDatePicked
        {
            get { return _effectiveDatePicked; }
            set
            {
                _effectiveDatePicked = value;
                RaisePropertyChanged("EffectiveDatePicked");
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

        private GenericPickerModel _effectiveDatePickerModel { get; set; }

        public GenericPickerModel EffectiveDatePickerModel
        {
            get { return _effectiveDatePickerModel; }
            set
            {
                _effectiveDatePickerModel = value;
                RaisePropertyChanged("EffectiveDatePickerModel");
            }
        }

        private bool _isFrequencyDetailsEnabled = false;

        public bool IsFrequencyDetailsEnabled
        {
            get { return _isFrequencyDetailsEnabled; }
            set
            {
                _isFrequencyDetailsEnabled = value;
                FrequencyDetailsButtonBackGroundColor = Color.FromHex(_isFrequencyDetailsEnabled ? "#d49504" : "#9EA4A9");
                RaisePropertyChanged("IsFrequencyDetailsEnabled");
            }
        }

        private Color _frequencyDetailsButtonBackGroundColor = Color.FromHex("#9EA4A9");
        public Color FrequencyDetailsButtonBackGroundColor
        {
            get
            {
                return _frequencyDetailsButtonBackGroundColor;
            }
            set
            {
                if (_frequencyDetailsButtonBackGroundColor == value)
                {
                    return;
                }
                _frequencyDetailsButtonBackGroundColor = value;
                RaisePropertyChanged("FrequencyDetailsButtonBackGroundColor");
            }
        }

        private bool _isAttachmentsEnabled = true;

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

        private Color _attachButtonBackGroundColor = Color.FromHex("#9EA4A9");
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

        private Color _declarationButtonBackGroundColor = Color.FromHex("#9EA4A9");
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

        public List<List<Attachment>> AttachmentsList { get; set; }

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

        private void Backnavigations()
        {
            switch (selectedPage)
            {
                case (int)PagesEnum.FrequencyDetailsView:

                    break;
                case (int)PagesEnum.AttachmentsView:
                    EnableFrequencyDetailsView();
                    break;

                case (int)PagesEnum.DeclarationView:
                    EnableAttachmentsView();
                    break;
                case (int)PagesEnum.SummaryView:
                    EnableDeclarationView();
                    break;

                default:
                    // code block
                    break;
            }
        }

        private VATChangeFillingPeriodRequestModel _changeFillingResponse { get; set; }

        public VATChangeFillingPeriodRequestModel ChangeFillingResponse
        {
            get { return _changeFillingResponse; }
            set
            {
                _changeFillingResponse = value;
                RaisePropertyChanged("ChangeFillingResponse");
            }
        }

        private VATRefillingDropdownModel _effectiveDateResponse { get; set; }

        public VATRefillingDropdownModel EffectiveDateResponse
        {
            get { return _effectiveDateResponse; }
            set
            {
                _effectiveDateResponse = value;
                RaisePropertyChanged("EffectiveDateResponse");
            }
        }
        public ChangeFillingPeriodViewModel(INavigationService navigationService, IDialogService dialogService) : base(
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

            ShowDatePicker = new Command(async () =>
            {
                showDatePickerDialog();
            });

            EffectiveDateSpinnerClicked = new Command(async () =>
            {
                showEffectiveDatePickerDialog();
            });

            IdTypeSpinnerTapped = new Command(async () =>
            {
                showIdTypePickerDialog();
            });

            _dialogService = dialogService;
            GoBackClick = new Command(async () =>
            {
                Backnavigations();
            });

            CloseClick = new Command(async () =>
            {
                _navigationService.GoBack();
            });

            FrequencyContinueBtnTapped = new Command(this.FrequencyContinueBtnClicked);
            AttachmentsContinueBtnTapped = new Command(this.AttachmentsContinueBtnClicked);
            DeclarationContinueBtnTapped = new Command(this.DeclarationContinueBtnClicked);
            GoBackToFrequencyDetails = new Command(this.GoBackToFrequencyDetailsClicked);
            GoBackToAttachments = new Command(this.GoBackToAttachmentsClicked);
            GoBackToDeclaration = new Command(this.GoBackToDeclarationClicked);
            NewAttachmentTapped = new Command(this.NewAttachmentClicked);
            // GoBackToDashBoardTapped = new Command(this.GoBackToDashboardClicked);

            //PopulateFrequencyDetailsListViewData();
            //PopulateChangeFillingAttachmentsListViewData();
            //PopulateDeclarationListViewData();
            //PopulateMyRequestsListViewData();

            setIdPickerModel();

            SelectedOutletOption = new ChangeFillingPeriodModel();

            genericDatePickerModel = new GenericDatePickerModel();
            genericDatePickerModel.DatePickerTitle = "Select Date";
            genericDatePickerModel.PickerId = "DatePicker";
        }

        public void ResetData()
        {
            EnableFrequencyDetailsView();
        }

        public void ValidateIdNumber()
        {
            try
            {
                IsIDVerified = false;
                PopUp popUp = new PopUp();
                StringBuilder Messages = new StringBuilder();
                if (!string.IsNullOrEmpty(IDNumber))
                {
                    if (IDType == AppResources.NationaID)
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
                    if (IDType == AppResources.VFCIqamaID)
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
                    if (IDType == AppResources.VFCGCCID)
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
                            EnableDeclaration();
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

        public async void showInstructionDialog()
        {
            await PopupNavigation.Instance.PushAsync(new InstructionsBottomPopUpView(
                instructionString: AppResources.CRInstructions, checkBoxString: AppResources.CRInstrCheckDesc,
                continueString: AppResources.CRContinue,
                _dialogType: InstructionsBottomPopUpViewModel.DialogType
                    .Instructions));

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

        private async void showEffectiveDatePickerDialog()
        {
            try
            {
                selectedPicker = PickerEnum.EffectiveDate;
                await PopupNavigation.Instance.PushAsync(new PickerPageView(EffectiveDatePickerModel));
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

        private async void showIdTypePickerDialog()
        {
            try
            {
                selectedPicker = PickerEnum.IdType;
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

        public void updateEffectiveDatePicker()
        {
            EffectiveDatePicked = EffectiveDatePickerModel.SelectedValue;
            EnableFrequencyDetails();
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

        /*public void ValidateIDNumber()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
            });
            
            // EntryName.IsEnabled = true;
            if (IDType == AppResources.VFCNationalID)
            {
                if (!string.IsNullOrEmpty(IDNumber))
                {
                    ValidateIdNumberFromApi();
                }
            }
            if (IDType == AppResources.VFCIqamaID)
            {
                //  EntryName.IsEnabled = true;
                if (!string.IsNullOrEmpty(IDNumber))
                {
                    ValidateIdNumberFromApi();
                }
            }
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Task.Run(() =>
                {
                   IsLoading = false;
                });
            });
        }*/

        private void setEffectiveDatePickerModel()
        {
            var list = new ObservableCollection<string>();

            foreach (var date in EffectiveDateResponse.d.EffDateSet.results)
            {
                list.Add(date.Txt50);
            }

            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = list;
            genericPickerModel.PickerTitle = AppResources.CRContractType;
            genericPickerModel.PickerId = "Effective Date";

            EffectiveDatePickerModel = genericPickerModel;
        }

        private void setIdPickerModel()
        {
            ObservableCollection<string> iDTypes = new ObservableCollection<string>();
            iDTypes.Add(AppResources.VFCNationalID);
            iDTypes.Add(AppResources.VFCIqamaID);
            iDTypes.Add(AppResources.VFCGCCID);


            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = iDTypes;
            genericPickerModel.PickerTitle = AppResources.CRContractType;
            genericPickerModel.PickerId = "ID Type";

            IDTypePickerModel = genericPickerModel;
        }

        public async void NewAttachmentClicked()
        {
            if (AttachmentsListViewData == null)
            {
                AttachmentsListViewData = new ObservableCollection<Attachment>();
            }
            try
            {
                await PopupNavigation.Instance.PushAsync(new FilesUploadPopUpPageView(
                    AttachmentsListViewData.ToList(),
                    Models.ZakatInstalationModels.WhichAttachment.ContractReleaseCopy, ""));
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

        public void EnableFrequencyDetailsView()
        {
            CurrentIndex = 1;
            IsFrequencyViewEnabled = true;
            IsAttachmentsViewEnabled = false;
            IsDeclarationViewEnabled = false;
            IsSummaryViewEnabled = false;
            IsBackVisible = false;
            selectedPage = (int)PagesEnum.FrequencyDetailsView;
        }

        public void EnableAttachmentsView()
        {
            CurrentIndex = 2;
            IsFrequencyViewEnabled = false;
            IsAttachmentsViewEnabled = true;
            IsDeclarationViewEnabled = false;
            IsSummaryViewEnabled = false;
            IsBackVisible = true;
            selectedPage = (int)PagesEnum.AttachmentsView;
        }

        public void EnableDeclarationView()
        {
            CurrentIndex = 3;
            IsFrequencyViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsDeclarationViewEnabled = true;
            IsSummaryViewEnabled = false;
            IsBackVisible = true;
            selectedPage = (int)PagesEnum.DeclarationView;
        }

        public void EnableSummaryView()
        {
            CurrentIndex = 4;
            IsFrequencyViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsDeclarationViewEnabled = false;
            IsSummaryViewEnabled = true;
            IsBackVisible = true;
            selectedPage = (int)PagesEnum.SummaryView;
        }

        private bool _isBackVisible = false;
        public bool IsBackVisible
        {
            get
            {
                return _isBackVisible;
            }
            set
            {
                _isBackVisible = value;
                RaisePropertyChanged("IsBackVisible");
            }
        }

        private bool _isFrequencyViewEnabled = false;
        public bool IsFrequencyViewEnabled
        {
            get
            {
                return _isFrequencyViewEnabled;
            }
            set
            {
                _isFrequencyViewEnabled = value;
                RaisePropertyChanged("IsFrequencyViewEnabled");
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
                _isDeclarationViewEnabled = value;
                RaisePropertyChanged("IsDeclarationViewEnabled");
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
                _isSummaryViewEnabled = value;
                RaisePropertyChanged("IsSummaryViewEnabled");
            }
        }

        private bool _showAttachments = false;
        public bool ShowAttachments
        {
            get
            {
                return _showAttachments;
            }
            set
            {
                _showAttachments = value;
                RaisePropertyChanged("ShowAttachments");
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

        private string _selectedAttachmentText = "";
        public string SelectedAttachmentText
        {
            get
            {
                return _selectedAttachmentText;
            }
            set
            {
                _selectedAttachmentText = value;
                RaisePropertyChanged("SelectedAttachmentText");
            }
        }

        private string _currentFrequency = "";
        public string CurrentFrequency
        {
            get
            {
                return _currentFrequency;
            }
            set
            {
                _currentFrequency = value;
                RaisePropertyChanged("CurrentFrequency");
            }
        }

        private string _newFrequency = "";
        public string NewFrequency
        {
            get
            {
                return _newFrequency;
            }
            set
            {
                _newFrequency = value;
                RaisePropertyChanged("NewFrequency");
            }
        }

        public ObservableCollection<ChangeFillingPeriodModel> outletDecisionOptions { get; set; }
        public ObservableCollection<ChangeFillingPeriodModel> OutletDecisionOptions
        {
            get
            {
                return outletDecisionOptions;
            }

            set
            {
                if (outletDecisionOptions == value)
                {
                    return;
                }

                outletDecisionOptions = value;
                RaisePropertyChanged("OutletDecisionOptions");
            }
        }

        public void AddAttachmentOptions()
        {
            OutletDecisionOptions = new ObservableCollection<ChangeFillingPeriodModel>();

            foreach (var attach in EffectiveDateResponse.d.ATT_TYPSet.results)
            {
                OutletDecisionOptions.Add(new ChangeFillingPeriodModel
                {
                    ActiveOutletDecisionOptions = attach.Txt50,
                    ActiveOutletDecisionOptionsIsSelected = false
                });
            }

        }

        private ChangeFillingPeriodModel _selectedOutletOption;
        public ChangeFillingPeriodModel SelectedOutletOption
        {
            get
            {
                return _selectedOutletOption;
            }
            set
            {
                _selectedOutletOption = value;
                RaisePropertyChanged("SelectedOutletOption");
            }
        }

        public async void FrequencyContinueBtnClicked()
        {
            try
            {
                if (!IsFrequencyDetailsEnabled)
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

        public async void AttachmentsContinueBtnClicked()
        {
            try
            {
                if (!IsAttachmentsEnabled)
                {
                    return;
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
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public async void GoBackToDeclarationClicked()
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
        public async void GoBackToDashboardClicked()
        {
            try
            {
                _navigationService.NavigateTo(App.GAZTNewDesignDashBoardPageView);
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
        public async void GoBackToAttachmentsClicked()
        {
            try
            {
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
        public async void GoBackToFrequencyDetailsClicked()
        {
            try
            {
                EnableFrequencyDetailsView();
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

        public void PopulateAttachments(List<Attachment> attachments)
        {
            AttachmentsList[SelectedOutletOptionIndex] = attachments;
            AttachmentsListViewData = new ObservableCollection<Attachment>();

            foreach (var attachment in attachments)
            {
                AttachmentsListViewData.Add(attachment);
            }

        }

        public void EnableFrequencyDetails()
        {
            if (!IsFrequencyDetailsChecked || EffectiveDatePicked == "")
            {
                IsFrequencyDetailsEnabled = false;
            }
            else
            {
                IsFrequencyDetailsEnabled = true;
            }
        }

        public void EnableAttachments()
        {
            IsAttachmentsEnabled = true;
        }

        public void EnableDeclaration()
        {
            if (ContactPersonName == "" || !IsIDVerified)
            {
                IsDeclarationEnabled = false;
            }
            else
            {
                IsDeclarationEnabled = true;
            }
        }

        public async void MyRequestsButtonClicked()
        {
            try
            {
                EnableFrequencyDetailsView();
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

        public async void DeclarationContinueBtnClicked()
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

        public ObservableCollection<MyRequestsListModel> MyRequestsListViewData { get; private set; }

        public void PopulateMyRequestsListViewData()
        {
            MyRequestsListViewData = new ObservableCollection<MyRequestsListModel>();
            MyRequestsListViewData.Add(new MyRequestsListModel
            {
                Title = "VAT Filling Period",
                ReferenceNumber = "0009856456",
                Status = "In Process",
                CurrentFrequency = "Monthly",
                NewFrequency = "Quarterly",
                EffectiveDate = "Quarter 4 - 2020",
                ReleaseDate = "09th August 2020"
            });

        }

        public void SetAttachmentsListViewData()
        {
            AttachmentsListViewData = new ObservableCollection<Attachment>();
            if (AttachmentsList != null && AttachmentsList[SelectedOutletOptionIndex] != null)
            {
                foreach (var attachment in AttachmentsList[SelectedOutletOptionIndex])
                {
                    AttachmentsListViewData.Add(attachment);
                }
            }

        }


        public ObservableCollection<InstalmentAgreementAttachmentsModel> FrequencyDetailsListViewData { get; private set; }

        public void PopulateFrequencyDetailsListViewData()
        {
            FrequencyDetailsListViewData = new ObservableCollection<InstalmentAgreementAttachmentsModel>();
            FrequencyDetailsListViewData.Add(new InstalmentAgreementAttachmentsModel
            {
                FieldTitle = "Current Frequency",
                FieldSubTitle = AppResources.TinDeregistration20MB,
                AttachmentName = "Monthly",
                IsAttachmentAttached = true
            });
            FrequencyDetailsListViewData.Add(new InstalmentAgreementAttachmentsModel
            {
                FieldTitle = "New Frequency",
                FieldSubTitle = AppResources.TinDeregistration20MB,
                AttachmentName = "Quarterly",
                IsAttachmentAttached = true
            });
            FrequencyDetailsListViewData.Add(new InstalmentAgreementAttachmentsModel
            {
                FieldTitle = "Effective Date",
                FieldSubTitle = AppResources.TinDeregistration20MB,
                AttachmentName = "Quarter 4 - 2020",
                IsAttachmentAttached = true
            });

        }

        public ObservableCollection<InstalmentAgreementAttachmentsModel> ChangeFillingAttachmentsListViewData { get; private set; }

        public void PopulateChangeFillingAttachmentsListViewData()
        {
            ChangeFillingAttachmentsListViewData = new ObservableCollection<InstalmentAgreementAttachmentsModel>();
            ChangeFillingAttachmentsListViewData.Add(new InstalmentAgreementAttachmentsModel
            {
                FieldTitle = "Type of Document",
                FieldSubTitle = AppResources.TinDeregistration20MB,
                AttachmentName = "12 Months Taxable Revenue",
                IsAttachmentAttached = true
            });
            ChangeFillingAttachmentsListViewData.Add(new InstalmentAgreementAttachmentsModel
            {
                FieldTitle = "Attachment",
                FieldSubTitle = AppResources.TinDeregistration20MB,
                AttachmentName = "File1.pdf",
                IsAttachmentAttached = true
            });

        }

        public ObservableCollection<InstalmentAgreementAttachmentsModel> DeclarationListViewData { get; private set; }

        public void PopulateDeclarationListViewData()
        {
            DeclarationListViewData = new ObservableCollection<InstalmentAgreementAttachmentsModel>();
            DeclarationListViewData.Add(new InstalmentAgreementAttachmentsModel
            {
                FieldTitle = "Item Type",
                FieldSubTitle = AppResources.TinDeregistration20MB,
                AttachmentName = "National ID",
                IsAttachmentAttached = true
            });
            DeclarationListViewData.Add(new InstalmentAgreementAttachmentsModel
            {
                FieldTitle = "ID Number",
                FieldSubTitle = AppResources.TinDeregistration20MB,
                AttachmentName = "Q17581231",
                IsAttachmentAttached = true
            });
            DeclarationListViewData.Add(new InstalmentAgreementAttachmentsModel
            {
                FieldTitle = "Date of Birth",
                FieldSubTitle = AppResources.TinDeregistration20MB,
                AttachmentName = "7 June 1995",
                IsAttachmentAttached = true
            });
            DeclarationListViewData.Add(new InstalmentAgreementAttachmentsModel
            {
                FieldTitle = "Contact Person Name",
                FieldSubTitle = AppResources.TinDeregistration20MB,
                AttachmentName = "Zaed Hardy",
                IsAttachmentAttached = true
            });

        }


        #region API Integration

        public async Task GetVATChangeFillingData()
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
                        var resultData = await WebServiceManager.GAZTGetVATChangeFillingPeriodRequestData();
                        if (resultData != null && resultData.d != null)
                        {
                            //resultData.d;
                            ChangeFillingResponse = resultData;
                            CurrentFrequency = resultData.d.CureentF;
                            NewFrequency = resultData.d.FilingF;

                            GetEffectiveDateList();
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

        public async Task GetEffectiveDateList()
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
                        var resultData = await WebServiceManager.GAZTGetVATChangeFillingPeriodDropdownData(App.LoginDataRetrieved.TIN);
                        if (resultData != null && resultData.d != null)
                        {
                            EffectiveDateResponse = resultData;
                            setEffectiveDatePickerModel();
                            AddAttachmentOptions();
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

        public async Task ValidateIdNumberFromApi(string idType)
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
                        var resultData = await WebServiceManager.GAZTVATChangeFillingPeriodValidateIDnumber(App.LoginDataRetrieved.TIN, idType, IDNumber, "", "", PickedDate.Replace("/", ""));
                        if (resultData != null && resultData.d != null)
                        {
                            IsIDVerified = true;
                            ContactPersonName = resultData.d.Name2 + " " + resultData.d.Name1;
                            EnableDeclaration();
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
