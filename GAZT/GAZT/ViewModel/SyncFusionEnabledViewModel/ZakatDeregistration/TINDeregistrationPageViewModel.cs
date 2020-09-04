using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.Views.NewDesign.GenericPickers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Plugin.FilePicker;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration
{
    public class TINDeregistrationPageViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand GoBackBtnTapped { get; set; }
        public ICommand CloseBtnTapped { get; set; }
        public ICommand IdTypeTapped { get; set; }
    
        #endregion

        #region Commands
        public ICommand ReasonContinueBtnTapped { get; set; }
        public ICommand OutletContinueBtnTapped { get; set; }
        public ICommand AttachmentsContinueBtnTapped { get; set; }
        public ICommand DeclarationContinueBtnTapped { get; set; }
        public ICommand SummaryContinueBtnTapped { get; set; }
        public ICommand OnTinRegisrtationReasonDateTapped { get; set; }
        public ICommand OnTinRegistrationReasonTapped { get; set; }

        #endregion

        #region Properties

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
                _isBackButtonVisible = value;
                RaisePropertyChanged("IsBackButtonVisible");
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

        private bool _isOutletViewEnabled = true;
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

        private bool _isAttachmentsViewEnabled = true;
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

        private bool _isDeclarationViewEnabled = true;
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

        private bool _isSummaryViewEnabled = true;
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

        public TINDeregistrationModel tinDeregistrationModel { get; set; }
        public TINDeregistrationModel TinDeregistrationModel
        {
            get
            {
                return tinDeregistrationModel;
            }

            set
            {
                tinDeregistrationModel = value;
                RaisePropertyChanged("TinDeregistrationModel");
            }
        }

        public ObservableCollection<TINDeregistrationModel> c { get; set; }

        public ObservableCollection<TINDeregistrationModel> outletDecisionOptions { get; set; }
        public ObservableCollection<TINDeregistrationModel> OutletDecisionOptions
        {
            get
            {
                return outletDecisionOptions;
            }

            set
            {
                outletDecisionOptions = value;
                RaisePropertyChanged("OutletDecisionOptions");
            }
        }

        public ObservableCollection<TinDeregestrationAttachmentsModel> attachmentsListViewData { get; set; }
        public ObservableCollection<TinDeregestrationAttachmentsModel> AttachmentsListViewData
        {
            get
            {
                return attachmentsListViewData;
            }

            set
            {
                attachmentsListViewData = value;
                RaisePropertyChanged("AttachmentsListViewData");
            }
        }

        public ObservableCollection<TINDeregistrationSummaryModel> _tinDeregistrationSummaryReasonData { get; set; }
        public ObservableCollection<TINDeregistrationSummaryModel> TinDeregistrationSummaryReasonData
        {
            get
            {
                return _tinDeregistrationSummaryReasonData;
            }

            set
            {
               
                _tinDeregistrationSummaryReasonData = value;
                RaisePropertyChanged("TinDeregistrationSummaryReasonData");
            }
        }

        public ObservableCollection<TINDeregistrationSummaryModel> _tinDeregistrationSummaryOutletData { get; set; }
        public ObservableCollection<TINDeregistrationSummaryModel> TinDeregistrationSummaryOutletData
        {
            get
            {
                return _tinDeregistrationSummaryOutletData;
            }

            set
            {
                
                _tinDeregistrationSummaryOutletData = value;
                RaisePropertyChanged("TinDeregistrationSummaryOutletData");
            }
        }

        public ObservableCollection<TINDeregistrationSummaryModel> _tinDeregistrationSummaryDeclarationData { get; set; }
        public ObservableCollection<TINDeregistrationSummaryModel> TinDeregistrationSummaryDeclarationData
        {
            get
            {
                return _tinDeregistrationSummaryDeclarationData;
            }

            set
            {
                
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
                _selectedOutletOptionIndex = value;
                RaisePropertyChanged("SelectedOutletOptionIndex");
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
                _selectedOutletOption = value;
                //SelectedOutletOptionIndex = OutletDecisionOptions.IndexOf(_selectedOutletOption as TINDeregistrationModel);
                RaisePropertyChanged("SelectedOutletOption");
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
        byte[] attachment;

        private TinDeregestrationAttachmentsModel _selectedAttachment { get; set; }
        public TinDeregestrationAttachmentsModel SelectedAttachment
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
        public ObservableCollection<VATDeregistrationSummaryModel> _vatDeregistrationSummaryDeclarationData { get; set; }
        public ObservableCollection<VATDeregistrationSummaryModel> VATDeregistrationSummaryDeclarationData
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
                _tinDeregistrationData = value;
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
                _selectedReason = value;
                //SelectedOutletOptionIndex = OutletDecisionOptions.IndexOf(_selectedOutletOption as TINDeregistrationModel);
                RaisePropertyChanged("SelectedReason");
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

                _selectedIdtype = value;
                RaisePropertyChanged("SelectedIdtype");
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

                _selectedIdNumber = value;
                RaisePropertyChanged("SelectedIdNumber");
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

                _selectedIDTypeCode = value;
                RaisePropertyChanged("SelectedIDTypeCode");
            }
        }


        public ObservableCollection<TinDeregReasonSetResult> _tinDeregReasons { get; set; }
        public ObservableCollection<TinDeregReasonSetResult> TinDeregReasons
        {
            get
            {
                return _tinDeregReasons;
            }

            set
            {
                _tinDeregReasons = value;
                RaisePropertyChanged("VATDeregistrationSummaryDeclarationData");
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

                try
                {
                    if (PickerModel != null && PickerModel.SelectedValue != null)
                    {
                        if (PickerModel.PickerId == "reasonPicker")
                        {
                            string tempSelectedReason = PickerModel.SelectedValue;
                            SelectedReason = TinDeregReasons.Where(m => m.ReasonDesc == PickerModel.SelectedValue).FirstOrDefault();
                            AddOutletDecisionOptions();
                        }
                        else if(PickerModel.PickerId == "idTypePicker")
                        {
                            SelectedIdtype = PickerModel.SelectedValue;
                            IBANType idType = IBANTypesList.Where(m => m.Text == PickerModel.SelectedValue).FirstOrDefault();
                            SelectedIDTypeCode = idType.key;

                            if(SelectedIdtype == AppResources.ZIBANNationalID)
                            {
                                NationalTypeSelected();
                            }
                            else if (SelectedIdtype == AppResources.ZIBANCompanyID)
                            {
                                CompanyIdTypeSelected();
                            }
                            else if (SelectedIdtype == AppResources.ZZIqamaID)
                            {
                                IqamaTypeSelected();
                            }
                            else if (SelectedIdtype == AppResources.ZZGCCID)
                            {
                                GCCIdTypeSelected();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                RaisePropertyChanged("PickerModel");
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
                _deregistrationDate = value;
                RaisePropertyChanged("DeregistrationDate");
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
                _tinText = value;
                RaisePropertyChanged("TinText");
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
                _grandFathersNameText = value;
                RaisePropertyChanged("GrandFathersNameText");
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
                _name2Text = value;
                RaisePropertyChanged("Name2Text");
            }
        }

        private ObservableCollection<IBANType> _iBANTypesList;
        public ObservableCollection<IBANType> IBANTypesList
        {
            get
            {
                return _iBANTypesList;
            }
            set
            {
                _iBANTypesList = value;
                //if (_iBANTypesList != null && _iBANTypesList.Count != 0)
                //{
                //    if ((App.ICRStatus == "E0045" || App.ICRStatus == "E0006") && IsAmendClicked == false)
                //    {
                //        IsEnableIBANType = false;
                //    }
                //    else
                //    {
                //        IsEnableIBANType = true;
                //    }
                //}
                //else
                //{
                //    IsEnableIBANType = false;
                //}
                RaisePropertyChanged("IBANTypesList");
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
            FirstNameText.IsVisible = true;
            FirstNameText.IsEditable = false;

            SurnameText.IsMandatory = false;
            SurnameText.IsVisible = true;
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

            DobText.IsMandatory = false;
            DobText.IsVisible = false;
            DobText.IsEditable = false;//Non Editable after validation

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

            DobText.IsMandatory = false;
            DobText.IsVisible = false;
            DobText.IsEditable = false;

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

            DobText.IsMandatory = false;
            DobText.IsVisible = false;
            DobText.IsEditable = false;//Non Editable after validation

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
            IBANTypesList = new ObservableCollection<IBANType>();
            ObservableCollection<IBANType> IBANTypesDummyList = new ObservableCollection<IBANType>();
            IBANType iBANType = new IBANType();
            iBANType.key = "ZS0001";
            iBANType.Text = AppResources.ZIBANNationalID;
            IBANTypesDummyList.Add(iBANType);

            IBANType iBANType2 = new IBANType();
            iBANType2.key = "ZS0005";
            iBANType2.Text = AppResources.ZIBANCompanyID;
            IBANTypesDummyList.Add(iBANType2);

            IBANType iBANType3 = new IBANType();
            iBANType3.key = "ZS0002";
            iBANType3.Text = AppResources.ZZIqamaID;
            IBANTypesDummyList.Add(iBANType3);

            IBANType iBANType4 = new IBANType();
            iBANType4.key = "ZS0003";
            iBANType4.Text = AppResources.ZZGCCID;
            IBANTypesDummyList.Add(iBANType4);

            IBANTypesList = IBANTypesDummyList;
        }

        public async void OnIdTypeClicked()
        {
            ObservableCollection<string> idTypeData = new ObservableCollection<string>();

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

            CloseBtnTapped = new Command(async () =>
            {
                _navigationService.GoBack();
            });         

            GoBackBtnTapped = new Command(this.GoBackBtnClicked);
            ReasonContinueBtnTapped = new Command(this.ReasonContinueBtnClicked);
            OutletContinueBtnTapped = new Command(this.OutletContinueBtnClicked);
            AttachmentsContinueBtnTapped = new Command(this.AttachmentsContinueBtnClicked);
            DeclarationContinueBtnTapped = new Command(this.DeclarationContinueBtnClicked);
            SummaryContinueBtnTapped = new Command(this.SummaryContinueBtnClicked);
            OnTinRegisrtationReasonDateTapped = new Command(this.OnTinRegisrtationReasonDateClicked);
            OnTinRegistrationReasonTapped = new Command(this.OnTinRegisrtationReasonClicked);
            TinDeregistrationModel = new TINDeregistrationModel();
            SelectedOutletOption = new TINDeregistrationModel();
            TinDeregistrationData = new TinDeregistrationResponseModel();
            TinDeregistrationReasonSetData = new TinDeregistrationReasonSetDataModel();
            TinText = new FieldValidations();
            IdTypeText = new FieldValidations();
            IdNumberText = new FieldValidations();
            DobText = new FieldValidations();
            FirstNameText = new FieldValidations();
            SurnameText = new FieldValidations();
            FathersNameText = new FieldValidations();
            FamilyNameText = new FieldValidations();
            GrandFathersNameText = new FieldValidations();

            IdTypeTapped = new Command(OnIdTypeClicked);

            //AddOutletDecisionOptions();
            PopulateAttachmentsListViewTemplate();
            PopulateIdTypeTypeFromList();
            PopulateSummaryReasonData();
            PopulateSummaryDeclarationData();
            EnableReasonView();
        }

        public async void LoadReasonSet()
        {
            try
            {
                await Task.Run(() =>
                {
                    App.DisplayProgressView();
                });

                TinDeregistrationReasonSetData = await WebServiceManager.GaztTinDeregistrationReasonData();
                TinDeregReasons = new ObservableCollection<TinDeregReasonSetResult>(TinDeregistrationReasonSetData.ReasonSet.Results);

                //TinDeregistrationReasonSetData.ReasonSet.Results.
                await Task.Run(() =>
                {
                    App.HideProgressView();
                });
            }
            catch (InternetException ex)
            {
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
            }
        }

        public async void OnTinRegisrtationReasonClicked()
        {
            try
            {
                ObservableCollection<string> reasonData = new ObservableCollection<string>();

                foreach (TinDeregReasonSetResult reasonDataDesc in TinDeregReasons)
                {
                    reasonData.Add(reasonDataDesc.ReasonDesc);
                }

                GenericPickerModel genericPickerModel = new GenericPickerModel();
                genericPickerModel.PickerData = reasonData;
                genericPickerModel.PickerTitle = AppResources.TinDeregistrationReason;
                genericPickerModel.PickerId = "reasonPicker";

                await PopupNavigation.Instance.PushAsync(new PickerPageView(genericPickerModel));
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

        public void AddOutletDecisionOptions()
        {
            if(OutletDecisionOptions == null)
            OutletDecisionOptions = new ObservableCollection<TINDeregistrationModel>();

            OutletDecisionOptions.Clear();

            if (SelectedReason.ReasonDesc == AppResources.TinDeregistrationReasonBankruptcy || SelectedReason.ReasonDesc == AppResources.TinDeregistrationReasonDeath
                || SelectedReason.ReasonDesc == AppResources.TinDeregistrationReasonLiquidation)
            {
                OutletDecisionOptions.Add(new TINDeregistrationModel
                {
                    ActiveOutletDecisionOptions = AppResources.TinDeregistrationCloseAllOutlets,
                    ActiveOutletDecisionOptionsIsSelected = true
                });
                OutletDecisionOptions.Add(new TINDeregistrationModel
                {
                    ActiveOutletDecisionOptions = AppResources.TinDeregistrationTransferAllOutletsToSingle,
                    ActiveOutletDecisionOptionsIsSelected = false
                });
                OutletDecisionOptions.Add(new TINDeregistrationModel
                {
                    ActiveOutletDecisionOptions = AppResources.TinDeregistrationCloseOutletsIndividually,
                    ActiveOutletDecisionOptionsIsSelected = false
                });
            }
            else
            {
                OutletDecisionOptions.Add(new TINDeregistrationModel
                {
                    ActiveOutletDecisionOptions = AppResources.TinDeregistrationTransferAllOutletsToSingle,
                    ActiveOutletDecisionOptionsIsSelected = false
                });
            }
        }

        public void EnableReasonView()
        {
            CurrentStep = ProcessStep.Step1;

            //SelectedOutletOption = OutletDecisionOptions[0];
            //SelectedOutletOptionIndex = 0;

            IsBackButtonVisible = false;
            IsReasonViewEnabled = true;
            IsOutletViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsDeclarationViewEnabled = false;
            IsSummaryViewEnabled = false;
        }

        public void EnableOutletDetaislView()
        {
            CurrentStep = ProcessStep.Step2;
            IsBackButtonVisible = true;
            IsReasonViewEnabled = false;
            IsOutletViewEnabled = true;
            IsAttachmentsViewEnabled = false;
            IsDeclarationViewEnabled = false;
            IsSummaryViewEnabled = false;
        }

        public void EnableAttachmentsView()
        {
            CurrentStep = ProcessStep.Step3;
            IsReasonViewEnabled = false;
            IsOutletViewEnabled = false;
            IsAttachmentsViewEnabled = true;
            IsDeclarationViewEnabled = false;
            IsSummaryViewEnabled = false;
        }

        public void EnableDeclarationView()
        {
            CurrentStep = ProcessStep.Step4;
            IsReasonViewEnabled = false;
            IsOutletViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsDeclarationViewEnabled = true;
            IsSummaryViewEnabled = false;
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
                switch(CurrentStep)
                {
                    case ProcessStep.Step2:
                    {
                        EnableReasonView();
                        break;
                    }
                    case ProcessStep.Step3:
                    {
                        EnableOutletDetaislView();
                        break;
                    }
                    case ProcessStep.Step4:
                    {
                        EnableAttachmentsView();
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
                EnableOutletDetaislView();
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

        public async void OutletContinueBtnClicked()
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

        public async void AttachmentsContinueBtnClicked()
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

        public async void DeclarationContinueBtnClicked()
        {
            try
            {
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

        public async void SummaryContinueBtnClicked()
        {
            try
            {
                //Display Success Screen
                _navigationService.NavigateTo(App.TINDeregestrationSuccessPageView);
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

       
        public async void OnTinRegisrtationReasonDateClicked()
        {
            try
            {
                GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
                genericDatePickerModel.DatePickerTitle = AppResources.VatDeregStartDatePickerTitle;
                genericDatePickerModel.PickerId = "DeregDatePicker";

                try
                {
                    await PopupNavigation.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel, true));
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

        //OutletContinueButtonTapped
        //AttachmentsContinueButtonTapped
        //DeclarationContinueButtonTapped
        //SummaryContinueButtonTapped

        #region Attachments View
        public void PopulateAttachmentsListViewTemplate()
        {
            AttachmentsListViewData = new ObservableCollection<TinDeregestrationAttachmentsModel>();
            AttachmentsListViewData.Add(new TinDeregestrationAttachmentsModel
            {
                FieldTitle = AppResources.TinDeregistrationAttachmentCopyOfDeclaringBankruptcy,
                FieldSubTitle = AppResources.TinDeregistration20MB,
                AttachmentName = string.Empty,
                IsAttachmentAttached = false
            });
            AttachmentsListViewData.Add(new TinDeregestrationAttachmentsModel
            {
                FieldTitle = AppResources.TinDeregistrationAttachmentCopyOfLicneseAfterClosing,
                FieldSubTitle = AppResources.TinDeregistration20MB,
                AttachmentName = string.Empty,
                IsAttachmentAttached = false
            });
            AttachmentsListViewData.Add(new TinDeregestrationAttachmentsModel
            {
                FieldTitle = AppResources.TinDeregistrationAttachmentCopyOfCRAfterClosing,
                FieldSubTitle = AppResources.TinDeregistration50MBMax,
                AttachmentName = string.Empty,
                IsAttachmentAttached = false
            });
            AttachmentsListViewData.Add(new TinDeregestrationAttachmentsModel
            {
                FieldTitle = AppResources.TinDeregistrationAttachmentOwnershipSellingAgreement,
                FieldSubTitle = AppResources.TinDeregistration50MBMax,
                AttachmentName = string.Empty,
                IsAttachmentAttached = false
            });
            AttachmentsListViewData.Add(new TinDeregestrationAttachmentsModel
            {
                FieldTitle = AppResources.TinDeregistrationAttachmentCopyOfPartnersDecision,
                FieldSubTitle = AppResources.TinDeregistration50MBMax,
                AttachmentName = string.Empty,
                IsAttachmentAttached = false
            });
            AttachmentsListViewData.Add(new TinDeregestrationAttachmentsModel
            {
                FieldTitle = AppResources.TinDeregistrationAttachmentCopyOfContractAfterClosing,
                FieldSubTitle = AppResources.TinDeregistration50MBMax,
                AttachmentName = string.Empty,
                IsAttachmentAttached = false
            });
        }
        #endregion

        #region Summary View
        public void PopulateSummaryReasonData()
        {
            TinDeregistrationSummaryReasonData = new ObservableCollection<TINDeregistrationSummaryModel>();
            TinDeregistrationSummaryReasonData.Add(new TINDeregistrationSummaryModel
            {
                SummaryTitle = AppResources.TinDeregistrationReason,
                SummaryData = "Bankruptcy",
                IsEditVisible = true
            });
            TinDeregistrationSummaryReasonData.Add(new TINDeregistrationSummaryModel
            {
                SummaryTitle = AppResources.TinDeregistrationQuestionOutlets,
                SummaryData = AppResources.TinDeregistrationCloseAllOutlets,
                IsEditVisible = true
            });
            TinDeregistrationSummaryReasonData.Add(new TINDeregistrationSummaryModel
            {
                SummaryTitle = AppResources.TinDeregistrationDate,
                SummaryData = "04 August 2020",
                IsEditVisible = true
            });
        }

        public void PopulateSummaryDeclarationData()
        {
            TinDeregistrationSummaryDeclarationData = new ObservableCollection<TINDeregistrationSummaryModel>();
            TinDeregistrationSummaryDeclarationData.Add(new TINDeregistrationSummaryModel
            {
                SummaryTitle = AppResources.TinDeregistrationContactPersonName,
                SummaryData = "Hardy",
                IsEditVisible = true
            });
            TinDeregistrationSummaryDeclarationData.Add(new TINDeregistrationSummaryModel
            {
                SummaryTitle = AppResources.TinDeregistrationDesignation,
                SummaryData = "Senior Director",
                IsEditVisible = true
            });
            TinDeregistrationSummaryDeclarationData.Add(new TINDeregistrationSummaryModel
            {
                SummaryTitle = AppResources.MobileNumber,
                SummaryData = "+966 551 234 567",
                IsEditVisible = true
            });
            TinDeregistrationSummaryDeclarationData.Add(new TINDeregistrationSummaryModel
            {
                SummaryTitle = AppResources.ZZDateofBirth,
                SummaryData = "07 June 1995",
                IsEditVisible = true
            });
        }

        #endregion

        public async Task AddAttachmentEx()
        {
            try
            {
                try
                {
                    string[] filetypes;
                    filetypes = DependencyService.Get<IDeviceInfo>().GetAttachmentTypeStringForAll();
                    var fileData = await CrossFilePicker.Current.PickFile(filetypes);

                    if (fileData != null && fileData.DataArray != null && fileData.DataArray.Length > 0)
                    {
                        attachment = fileData.DataArray;
                        AttachmentName = fileData.FileName;
                        SelectedAttachment.AttachmentName = AttachmentName;
                        SelectedAttachment.IsAttachmentAttached = true;
                        AttachmentsListViewData.RemoveAt(SelectedOutletOptionIndex);
                        AttachmentsListViewData.Insert(SelectedOutletOptionIndex, SelectedAttachment);

                        //if (fileData.FileName.Contains("."))
                        //{
                        //    string Extention = fileData.FileName.Split('.')[1];
                        //    if (Extention.ToLower() == "doc" || Extention.ToLower() == "docx" || Extention.ToLower() == "jpg" || Extention.ToLower() == "jpeg" || Extention.ToLower() == "pdf"
                        //        || Extention.ToLower() == "xlsx" || Extention.ToLower() == "xls" || Extention.ToLower() == "png" || Extention.ToLower() == "ppt" || Extention.ToLower() == "pptx"
                        //        || Extention.ToLower() == "gif" || Extention.ToLower() == "txt")
                        //    {
                        //        if (TotalAttachmentSize <= 300)
                        //        {
                        //            AttachmentSize = Math.Round(Convert.ToDecimal((Convert.ToDouble(attachment.Length) / 1048576.0)), 2);
                        //            decimal AttachmentSizeTillFourDecimal = Math.Round(Convert.ToDecimal((Convert.ToDouble(attachment.Length) / 1048576.0)), 4);

                        //            if (Convert.ToDecimal(AttachmentSize) <= 5)
                        //            {
                        //                if (Convert.ToDecimal(AttachmentSizeTillFourDecimal) > 0)
                        //                {
                        //                    bool IsAttachmentPresent = false;

                        //                    if (IsAttachmentPresent == false)
                        //                    {
                        //                        string attachmentType = UtilityManager.GetContentType(Extention);
                        //                    }
                        //                    else
                        //                    {
                        //                        AttachmentName = string.Empty;

                        //                        await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_FileWithTheSameNameAlreadyExists, AppResources.Information);
                        //                    }
                        //                }
                        //                else
                        //                {
                        //                    AttachmentName = string.Empty;

                        //                    await _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
                        //                }
                        //            }
                        //            else
                        //            {
                        //                AttachmentName = string.Empty;

                        //                await _dialogService.ShowMessage(AppResources.ZFilesizeshouldnotbemorethan20MB, AppResources.Information);
                        //            }
                        //        }
                        //        else
                        //        {
                        //            AttachmentName = string.Empty;

                        //            await _dialogService.ShowMessage(AppResources.ZTotalFilesizeshouldnotbemorethan300MB, AppResources.Information);
                        //        }
                        //    }
                        //    else
                        //    {
                        //        AttachmentName = string.Empty;

                        //        await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
                        //    }
                        //}
                        //else
                        //{
                        //    AttachmentName = string.Empty;

                        //    await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
                        //}
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

        
    }
}
