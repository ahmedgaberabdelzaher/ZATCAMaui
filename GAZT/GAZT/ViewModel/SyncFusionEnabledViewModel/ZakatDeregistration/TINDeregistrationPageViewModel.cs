using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.Models.ChageFillingPeriodModel;
using EGAZT.Views.NewDesign.GenericPickers;
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
        public ICommand PermitIdtypeTapped { get; set; }
        public ICommand PermitTypeTinUnfocused { get; set; }

        //
        #endregion

        #region Commands
        public ICommand ReasonContinueBtnTapped { get; set; }
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

        private bool _isOutletTranferOutletGridVisible { get; set; }
        public bool IsOutletTranferOutletGridVisible
        {
            get
            {
                return _isOutletTranferOutletGridVisible;
            }
            set
            {
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
                _isNodataAvailableVisible = value;
                RaisePropertyChanged("IsNodataAvailableVisible");
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
                _isReasonViewEnabled = value;
                RaisePropertyChanged("IsReasonViewEnabled");
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
                if (value != null)
                    outletDecisionOptions = value;
                RaisePropertyChanged("OutletDecisionOptions");
            }
        }

        public ObservableCollection<TINDeregistrationModel> permitOutletDecisionOptions { get; set; }
        public ObservableCollection<TINDeregistrationModel> PermitOutletDecisionOptions
        {
            get
            {
                return permitOutletDecisionOptions;
            }
            set
            {
                if (value != null)
                    permitOutletDecisionOptions = value;
                RaisePropertyChanged("PermitOutletDecisionOptions");
            }
        }

        private TINDeregistrationModel selectedPermitTypeOutletOption { get; set; }
        public TINDeregistrationModel SelectedPermitTypeOutletOption
        {
            get
            {
                return selectedPermitTypeOutletOption;
            }
            set
            {
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
                _selectedPermitOutletOptionIndex = value;
                RaisePropertyChanged("SelectedPermitOutletOptionIndex");
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
        private bool _isOption1Visible;
        public bool IsOption1Visible
        {
            get
            {
                return _isOption1Visible;
            }
            set
            {
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
                _isOption2Visible = value;
                RaisePropertyChanged("IsOption2Visible");
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
                if (value != null)
                {
                    _selectedOutletOption = value;

                    if (TinDeregistrationData != null)
                    {
                        if (TinDeregistrationData.ADregOpt != null)
                        {
                            TinDeregistrationData.ADregOpt = _selectedOutletOption.OutletOptionIndex;
                        }
                        else
                        {
                            TinDeregistrationData.ADregOpt = string.Empty;
                            TinDeregistrationData.ADregOpt = _selectedOutletOption.OutletOptionIndex;
                        }

                        PopulateAttachmentsListViewTemplate();
                    }
                }

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

        private bool _isName1Visible = false;
        public bool IsName1Visible
        {
            get
            {
                return _isName1Visible;
            }
            set
            {
                _isName1Visible = value;
                RaisePropertyChanged("IsName1Visible");
            }
        }

        //3102410588
        private string _firstNameLbl { get; set; }
        public string FirstNameLbl
        {
            get
            {
                return _firstNameLbl;
            }
            set
            {
                _firstNameLbl = value;
                RaisePropertyChanged("FirstNameLbl");
            }
        }

        private string _surnameNameLbl { get; set; }
        public string SurnameNameLbl
        {
            get
            {
                return _surnameNameLbl;
            }
            set
            {
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

        private ObservableCollection<Attachment> _tinDeregAttachmentList;
        public ObservableCollection<Attachment> TinDeregAttachmentList
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
                            if (tempSelectedReason != string.Empty)

                            {

                                SelectedReason = TinDeregReasons.Where(m => m.ReasonDesc == PickerModel.SelectedValue).FirstOrDefault();
                                TinDeregistrationData.ADregReason = SelectedReason.ReasonCd;
                                TinDeregistrationData.ADeregSelectedReasonValue = SelectedReason.ReasonDesc;

                                List<PermitSetResult> allPermitTypes = new List<PermitSetResult>(TinDeregistrationData.PermitSet.Results);

                                foreach (OutletSetResult outletInfo in AllOutlets)
                                {
                                    outletInfo.ReasonDescription = SelectedReason.ReasonDesc;
                                    outletInfo.PermitTypes = new ObservableCollection<PermitSetResult>();

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
                                            permitInfo.ReasonDescription = SelectedReason.ReasonDesc;

                                            if (outletInfo.PermitTypes == null)
                                                outletInfo.PermitTypes = new ObservableCollection<PermitSetResult>();


                                            outletInfo.PermitTypes.Add(permitInfo);
                                        }
                                    }
                                }
                                DateField.IsVisible = true;



                            }
                            else
                            {
                                DateField.IsVisible = false;
                            }
                            AddOutletDecisionOptions();
                            PopulateAttachmentsListViewTemplate();
                        }
                        else if (PickerModel.PickerId == "idTypePicker")
                        {
                            SelectedIdtype = PickerModel.SelectedValue;
                            IBANType idType = IBANTypesList.Where(m => m.Text == PickerModel.SelectedValue).FirstOrDefault();
                            SelectedIDTypeCode = idType.key;
                            IDTypeDataModel = new VATSignUpD();

                            FirstNameLbl = AppResources.ZZZVATRFirstName;
                            SurnameNameLbl = AppResources.ZZZVATRSurName;
                            IsName1Visible = false;

                            if (SelectedIdtype == AppResources.NationaID)
                            {
                                NationalTypeSelected();
                            }
                            else if (SelectedIdtype == AppResources.ZIBANCompanyID)
                            {
                                IsName1Visible = true;
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
                        else if (PickerModel.PickerId == "permitTypeReasonPicker")
                        {
                            string tempSelectedReason = PickerModel.SelectedValue;

                            SelectedOutletForCloseTranser.PermitTypes = new ObservableCollection<PermitSetResult>(SelectedOutletForCloseTranser.PermitTypes.ToList().Select(
                              x =>
                              {
                                  if (x.APermitNoTb == selectedAPermitReason)
                                  {
                                      x.APermitDisplayReason = tempSelectedReason;

                                      if (tempSelectedReason == AppResources.TinDeregistrationClosed)
                                      {
                                          x.APermitDregRsnTb = "1";
                                      }
                                      else
                                      {
                                          x.APermitDregRsnTb = "3";
                                      }
                                  }

                                  return x;
                              }
                              ).ToList());

                            if (PickerModel.SelectedValue == AppResources.TinDeregistrationClosed)
                            {
                                IsOutletTranferOutletGridVisible = false;
                            }
                            else
                            {
                                IsOutletTranferOutletGridVisible = true;
                            }
                        }
                        else if (PickerModel.PickerId == "permitIdTypePicker")
                        {
                            SelectedOutletForCloseTranser.PermitTypes = new ObservableCollection<PermitSetResult>(SelectedOutletForCloseTranser.PermitTypes.ToList().Select(
                              x =>
                              {
                                  if (x.APermitNoTb == tempIdTypePermitSetResult.APermitNoTb)
                                  {
                                      //x.APermitIdTypeTb

                                      x.APermitTransTinTb = string.Empty;
                                      x.APermitIdNoTb = string.Empty;

                                      if (PickerModel.SelectedValue == AppResources.NationaID)
                                      {
                                          x.APermitIdTypeTb = "ZS0001";
                                      }
                                      else if (PickerModel.SelectedValue == AppResources.ZIBANCompanyID)
                                      {
                                          x.APermitIdTypeTb = "ZS0005";
                                      }
                                      else if (PickerModel.SelectedValue == AppResources.ZZIqamaID)
                                      {
                                          x.APermitIdTypeTb = "ZS0002";
                                      }
                                      else if (PickerModel.SelectedValue == AppResources.ZZGCCID)
                                      {
                                          x.APermitIdTypeTb = "ZS0003";
                                      }
                                  }
                                  return x;
                              }
                              ).ToList());
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

        private DateTime _singleOutletDeregistrationDate = DateTime.Now;
        public DateTime SingleOutletDeregistrationDate
        {
            get
            {
                return _singleOutletDeregistrationDate;
            }
            set
            {
                _singleOutletDeregistrationDate = value;

                SelectedOutletForCloseTranser.PermitTypes = new ObservableCollection<PermitSetResult>(SelectedOutletForCloseTranser.PermitTypes.ToList().Select(
                    x =>
                    {
                        if (x.APermitNoTb == selectedAPermitOutletnoTb)
                        {
                            x.APermitEffDtTb = ConvertDateFormat(SingleOutletDeregistrationDate);
                            x.APermitEffDtCTb = "G";
                            x.APermitEffDtHTb = SingleOutletDeregistrationDate.ToString("yyyyMMdd");
                            x.APermitDeregDisplayDate = SingleOutletDeregistrationDate.ToString("dd MMM yyyy");
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
                _permitDob = value;

                SelectedOutletForCloseTranser.PermitTypes = new ObservableCollection<PermitSetResult>(SelectedOutletForCloseTranser.PermitTypes.ToList().Select(
                    x =>
                    {
                        if (x.APermitNoTb == selectedAPermitOutletnoTb)
                        {
                            x.APermitDobTb = ConvertDateFormat(PermitDob);
                            x.APermitDobCTb = "G";
                            x.APermitDobHTb = PermitDob.ToString("yyyyMMdd");
                            x.APermitDeregDisplayDobDate = PermitDob.ToString("dd MMM yyyy");
                        }
                        return x;
                    }
                    ).ToList());

                RaisePropertyChanged("PermitDob");
            }
        }

        private DateTime _singleDeregistrationDate = DateTime.Now;
        public DateTime SingleDeregistrationDate
        {
            get
            {
                return _singleDeregistrationDate;
            }
            set
            {
                _singleDeregistrationDate = value;

                SelectedOutletForCloseTranser.PermitTypes = new ObservableCollection<PermitSetResult>(SelectedOutletForCloseTranser.PermitTypes.ToList().Select(
                    x =>
                    {
                        x.APermitEffDtTb = ConvertDateFormat(SingleDeregistrationDate);
                        x.APermitEffDtCTb = "G";
                        x.APermitEffDtHTb = SingleDeregistrationDate.ToString("yyyyMMdd");
                        x.APermitDeregDisplayDate = SingleDeregistrationDate.ToString("dd MMM yyyy");
                        return x;
                    }
                    ).ToList());

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
                _deregistrationDate = value;
                RaisePropertyChanged("DeregistrationDate");
            }
        }

        private DateTime _submissionDate = DateTime.Now;
        public DateTime SubmissionDate
        {
            get
            {
                return _submissionDate;
            }
            set
            {
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
                _frameTinError = value;
                RaisePropertyChanged("FrameTinError");
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
        private VATSignUpD _iDTypeDataModel = null;
        public VATSignUpD IDTypeDataModel
        {
            get
            {
                return _iDTypeDataModel;
            }

            set
            {

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
                _tinText = value;
                RaisePropertyChanged("TinText");
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

        public OutletSetResult _selectedOutletForCloseTranser { get; set; }
        public OutletSetResult SelectedOutletForCloseTranser
        {
            get
            {
                return _selectedOutletForCloseTranser;
            }

            set
            {
                _selectedOutletForCloseTranser = value;
                RaisePropertyChanged("SelectedOutletForCloseTranser");
            }
        }

        public ObservableCollection<OutletSetResult> _allOutlets { get; set; }
        public ObservableCollection<OutletSetResult> AllOutlets
        {
            get
            {
                return _allOutlets;
            }
            set
            {
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
                _VoidIsVisible = value;
                RaisePropertyChanged("VoidIsVisible");
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

        public void PopulateIdTypeTypeFromList()
        {
            IBANTypesList = new ObservableCollection<IBANType>();
            ObservableCollection<IBANType> IBANTypesDummyList = new ObservableCollection<IBANType>();
            IBANType iBANType = new IBANType();
            iBANType.key = "ZS0001";
            iBANType.Text = AppResources.NationaID;
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


        public async void OnPermitTypeTinEntered(PermitSetResult permitSetResult)
        {
            ObservableCollection<string> idTypeData = new ObservableCollection<string>();

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

        private PermitSetResult tempIdTypePermitSetResult = new PermitSetResult();
        public async void OnPermitIdTypeClicked(PermitSetResult permitSetResult)
        {
            ObservableCollection<string> idTypeData = new ObservableCollection<string>();

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




            GoBackBtnTapped = new Command(this.GoBackBtnClicked);
            ReasonContinueBtnTapped = new Command(this.ReasonContinueBtnClicked);
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

            IdTypeTapped = new Command(OnIdTypeClicked);
            PermitIdtypeTapped = new Command<PermitSetResult>(OnPermitIdTypeClicked);
            PermitTypeTinUnfocused = new Command<PermitSetResult>(OnPermitTypeTinEntered);

            //PermitIdtypeTapped
            //AddOutletDecisionOptions();
            //PopulateAttachmentsListViewTemplate();
            PopulateIdTypeTypeFromList();
            VoidIsVisible = false;
            EnableReasonView();
        }

        public async void LoadReasonSet()
        {
            try
            {
                //await Task.Run(() =>
                //{
                //    App.DisplayProgressView();
                //});

                TinDeregistrationReasonSetData = await WebServiceManager.GaztTinDeregistrationReasonData();
                TinDeregReasons = new ObservableCollection<TinDeregReasonSetResult>(TinDeregistrationReasonSetData.ReasonSet.Results);

                AllOutlets = new ObservableCollection<OutletSetResult>(TinDeregistrationData.OutletSet.Results);
                List<PermitSetResult> allPermitTypes = new List<PermitSetResult>(TinDeregistrationData.PermitSet.Results);

                if (!String.IsNullOrEmpty(TinDeregistrationData.ADregReason))
                {
                    SelectedReason = TinDeregReasons.Where(m => m.ReasonCd == TinDeregistrationData.ADregReason).FirstOrDefault();
                }

                AddOutletDecisionOptions();


                if (!String.IsNullOrEmpty(TinDeregistrationData.ADregOpt))
                {
                    try
                    {

                        SelectedOutletOption = OutletDecisionOptions.Where(m => m.OutletOptionIndex == TinDeregistrationData.ADregOpt).FirstOrDefault();
                        SelectedOutletOptionIndex = Convert.ToInt16(SelectedOutletOption.OutletOptionIndex) - 1;
                    }
                    catch (Exception ex)
                    {

                    }
                }

                if (TinDeregistrationData.AEffectiveDt != null)
                    DeregistrationDate = Convert.ToDateTime(TinDeregistrationData.AEffectiveDt);

                foreach (OutletSetResult outletInfo in AllOutlets)
                {
                    if (SelectedReason != null)
                    {
                        outletInfo.ReasonDescription = SelectedReason.ReasonDesc;
                    }

                    outletInfo.PermitTypes = new ObservableCollection<PermitSetResult>();


                    foreach (PermitSetResult permitInfo in allPermitTypes)
                    {
                        if (permitInfo.APermitOutletnoTb == outletInfo.AOutletNoTb)
                        {
                            if (SelectedReason != null)
                                permitInfo.ReasonDescription = SelectedReason.ReasonDesc;

                            if (permitInfo.APermitIdNoTb == null)
                                permitInfo.APermitIdNoTb = "";

                            if (permitInfo.APermitTransTinTb == null)
                                permitInfo.APermitTransTinTb = "";

                            if (permitInfo.APermitDregRsnTb == null)
                                permitInfo.APermitDregRsnTb = string.Empty;

                            if (outletInfo.PermitTypes == null)
                                outletInfo.PermitTypes = new ObservableCollection<PermitSetResult>();

                            outletInfo.PermitTypes.Add(permitInfo);
                        }
                    }
                }

                //TinDeregistrationReasonSetData.ReasonSet.Results.
                //await Task.Run(() =>
                //{
                //    App.HideProgressView();
                //});
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

        public async void OnOutletPermitTypeDeRegisrtationReasonClicked()
        {
            try
            {
                ObservableCollection<string> reasonData = new ObservableCollection<string>();
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
            genericDatePickerModel.PickerId = "DOBDateTypePicker";
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
        public async void ValidateIDNumber()
        {
            string dob = SelectedDob.Replace("/", "");
            string idTypeCode = string.Empty;

            //ZS0002 - IQAMA
            //ZS0005 - IBAN

            if (SelectedIdtype == AppResources.NationaID)
            {
                idTypeCode = "ZS0001";
            }
            else if (SelectedIdtype == AppResources.ZZIqamaID)
            {
                idTypeCode = "ZS0002";
            }
            else if (SelectedIdtype == AppResources.ZIBANCompanyID)
            {
                idTypeCode = "ZS0005";
            }
            else if (SelectedIdtype == AppResources.ZZGCCID)
            {
                idTypeCode = "ZS0003";
            }

            if (!string.IsNullOrEmpty(SelectedIdNumber))
            {
                try
                {
                    string Result = await WebServiceManager.GAZTVATSignUpValidateIDTypesStringResp(idTypeCode, SelectedIdNumber, dob);

                    if (IDTypeDataModel == null)
                    {
                        IDTypeDataModel = new VATSignUpD();
                    }

                    string _responseData = JObject.Parse(Result)["d"].ToString();
                    IDTypeDataModel = JsonConvert.DeserializeObject<VATSignUpD>(_responseData);
                    if (_responseData == null)
                    {
                        IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
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
                        FrameIDError = false;
                    }
                }
                catch
                {
                    try
                    {
                        string Result = await WebServiceManager.GAZTValidateIDTypes(idTypeCode, SelectedIdNumber, dob);
                        IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                        if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                        {
                            FrameIDError = true;
                            await _dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                        }
                        else
                        {
                            //FrmIDNumber.HasError = false;
                            FrameIDError = false;
                            await _dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                        }
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
                            await Task.Run(() =>
                            {
                                App.HideProgressView();
                            });

                            await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                            _navigationService.GoBack();
                        });
                    }
                    catch (InternetException ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await Task.Run(() =>
                            {
                                App.HideProgressView();
                            });
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);

                        });
                    }
                    catch (HttpRequestException ex)
                    {
                        string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            // IsLoading = false;
                            await Task.Run(() =>
                            {
                                App.HideProgressView();
                            });
                            await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                            //_navigationService.GoBack();
                        });
                    }
                    catch (Exception ex)
                    {

                        string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await Task.Run(() =>
                            {
                                App.HideProgressView();
                            });
                            await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                            //_navigationService.GoBack();
                        });
                    }
                }

            }

            Device.BeginInvokeOnMainThread(async () =>
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            });
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
                        VATSignUp resultData = await WebServiceManager.GAZTGetTInNumberData(tinNumber);
                        SelectedDob = string.Empty;
                        if (resultData != null && resultData.d != null)
                        {
                            IDTypeDataModel = new VATSignUpD();
                            IDTypeDataModel = resultData.d;

                            IBANType idType = IBANTypesList.Where(m => m.key == IDTypeDataModel.Idtype).FirstOrDefault();
                            if (idType != null)
                            {
                                SelectedIdtype = idType.Text;
                                SelectedIDTypeCode = idType.key;
                                SelectedDob = IDTypeDataModel.Birthdt10;
                                SelectedIdNumber = IDTypeDataModel.Idnum;
                            }

                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                IsLoading = false;
                                //await _dialogService.ShowMessage(resultDa, AppResources.Information);
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
                        VATSignUp resultData = await WebServiceManager.GAZTGetTInNumberData(tinNumber);
                        SelectedDob = string.Empty;
                        if (resultData != null && resultData.d != null)
                        {
                            IDTypeDataModel = new VATSignUpD();
                            IDTypeDataModel = resultData.d;

                            IBANType idType = IBANTypesList.Where(m => m.key == IDTypeDataModel.Idtype).FirstOrDefault();

                            SelectedOutletForCloseTranser.PermitTypes = new ObservableCollection<PermitSetResult>(SelectedOutletForCloseTranser.PermitTypes.ToList().Select(
                              x =>
                              {
                                  x.APermitIdTypeTb = idType.Text;
                                  x.APermitIdNoTb = IDTypeDataModel.Idnum;
                                  x.APermitNm3Tb = IDTypeDataModel.Name1;
                                  x.APermitNm4Tb = IDTypeDataModel.Name2;
                                  x.APermitNm5Tb = IDTypeDataModel.FatherName;
                                  x.APermitNm6Tb = IDTypeDataModel.GrandfatherName;
                                  x.APermitNm7Tb = IDTypeDataModel.FamilyName;
                                  x.APermitDobHTb = IDTypeDataModel.TaxpDob;

                                  return x;
                              }
                              ).ToList());
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                IsLoading = false;
                                //await _dialogService.ShowMessage(resultDa, AppResources.Information);
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

                PermitOutletDecisionOptions = new ObservableCollection<TINDeregistrationModel>(tempValues);
            }
            catch (Exception ex)
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
                    OutletDecisionOptions = new ObservableCollection<TINDeregistrationModel>(tempValues);
                }
                else
                {

                }

            }
            catch (Exception ex)
            {

            }

        }
        public void EnableReasonView()
        {
            CurrentStep = ProcessStep.Step1;

            //SelectedOutletOption = OutletDecisionOptions[0];
            //SelectedOutletOptionIndex = 0;

            IsBackButtonVisible = true;
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
                //TinDeregistrationData.ASubmissionDate = ConvertDateFormat(DeregistrationDate).ToString();
                //TinDeregistrationData.AEffectiveDt = TinDeregistrationData.ASubmissionDate;
                //TinDeregistrationData.ADecDate = TinDeregistrationData.ASubmissionDate;

                //await SaveAsDraft();

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
                    else
                    {
                        if (SelectedReason.ReasonCd != null)
                        {
                            AllOutlets = new ObservableCollection<OutletSetResult>(TinDeregistrationData.OutletSet.Results);
                            List<PermitSetResult> allPermitTypes = new List<PermitSetResult>(TinDeregistrationData.PermitSet.Results);

                            try
                            {
                                foreach (OutletSetResult outletInfo in AllOutlets)
                                {
                                    //if (outletInfo.AOutletExpdtTb == null)
                                    //    outletInfo.AOutletExpdtTb = string.Empty;

                                    if (SelectedReason != null)
                                    {
                                        outletInfo.ReasonDescription = SelectedReason.ReasonDesc;
                                    }

                                    outletInfo.PermitTypes = new ObservableCollection<PermitSetResult>();

                                    foreach (PermitSetResult permitInfo in allPermitTypes)
                                    {
                                        if (permitInfo.APermitOutletnoTb == outletInfo.AOutletNoTb)
                                        {
                                            if (SelectedReason != null)
                                            {
                                                permitInfo.ReasonDescription = SelectedReason.ReasonDesc;
                                            }

                                            if (permitInfo.APermitDregRsnTb == null)
                                                permitInfo.APermitDregRsnTb = string.Empty;

                                            if (outletInfo.PermitTypes == null)
                                                outletInfo.PermitTypes = new ObservableCollection<PermitSetResult>();

                                            if (permitInfo.APermitIdNoTb == null)
                                                permitInfo.APermitIdNoTb = "";

                                            if (permitInfo.APermitTransTinTb == null)
                                                permitInfo.APermitTransTinTb = "";

                                            if (SelectedOutletOption.OutletOptionIndex != "3")
                                            {
                                                permitInfo.APermitDeregDisplayDate = DeregistrationDate.ToString("dd MMM yyyy");
                                                permitInfo.APermitEffDtHTb = DeregistrationDate.ToString("yyyyMMdd");
                                                permitInfo.APermitEffDtCTb = "G";
                                                permitInfo.APermitEffDtTb = ConvertDateFormat(DeregistrationDate);
                                            }

                                            outletInfo.PermitTypes.Add(permitInfo);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }

                        }
                        else
                        {
                            await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                if (SelectedOutletOptionIndex == 1)
                {
                    if (SelectedIdNumber == null || SelectedReason == null || SelectedIdtype == null)
                    {
                        await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
                    }
                    else
                    {
                        await SaveAsDraft();
                        VoidIsVisible = true;
                        EnableOutletDetaislView();
                    }

                }
                else if (SelectedOutletOptionIndex == 2)
                {
                    if (SelectedReason == null)
                    {
                        await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
                    }
                    else
                    {
                        await SaveAsDraft();


                        VoidIsVisible = true;
                        EnableOutletDetaislView();
                    }
                }
                else if (SelectedOutletOptionIndex == 0)
                {
                    if (SelectedReason == null)
                    {
                        await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.Alerts);
                    }
                    else
                    {
                        await SaveAsDraft();
                        VoidIsVisible = true;

                        EnableOutletDetaislView();
                    }
                }
                else
                {
                    await SaveAsDraft();
                    VoidIsVisible = true;

                    EnableOutletDetaislView();
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

        public async void AddPopUpPage()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new TINDeregistrationCloseIndividualOutletsPageView(SelectedReason.ReasonDesc));
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
            try
            {
                PopulateAttachmentsListViewTemplate();
                await SaveAsDraft();
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
                await SaveAsDraft();
                EnableDeclarationView();
            }
            catch (GAZTUnlockAccountException ex)
            {
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

        public async void DeclarationContinueBtnClicked()
        {
            try
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
                await Submit();
                _navigationService.NavigateTo(App.TINDeregestrationSuccessPageView, TinDeregistrationData);
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
            catch (Exception ex)
            {

            }
        }

        private string selectedAPermitReason;
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

        public async void OnTinRegisrtationReasonDateClicked()
        {
            try
            {
                GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
                genericDatePickerModel.DatePickerTitle = AppResources.TinDeregistrationDate;
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
            List<TinDeregestrationAttachmentsModel> check = new List<TinDeregestrationAttachmentsModel>();

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

                    if (TinDeregistrationData.ADregOpt == "2")
                    {
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

                //Logics pending for reason 3 4 5 for idtype 2
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
                    }
                }
            }


            //This attachment is needed when transferring the outlets and not closing for all the cases
            if (TinDeregistrationData.ADregOpt == "2" || TinDeregistrationData.ADregOpt == "3")
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

            //In all Cases

            if (TinDeregistrationData != null)
            {
                if (TinDeregistrationData.PermitSet.Results[0].APermitTypeTb == "BUP002")
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
                else if (TinDeregistrationData.PermitSet.Results[0].APermitTypeTb == "ZS0004")
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
            AttachmentsListViewData = new ObservableCollection<TinDeregestrationAttachmentsModel>(check);

            //AttachmentsListViewData.Add(new TinDeregestrationAttachmentsModel
            //{
            //    FieldTitle = AppResources.TinDeregistrationAttachmentOwnershipSellingAgreement,
            //    FieldSubTitle = AppResources.TinDeregistration50MBMax,
            //    AttachmentName = string.Empty,
            //    IsAttachmentAttached = false
            //});
            //AttachmentsListViewData.Add(new TinDeregestrationAttachmentsModel
            //{
            //    FieldTitle = AppResources.TinDeregistrationAttachmentCopyOfPartnersDecision,
            //    FieldSubTitle = AppResources.TinDeregistration50MBMax,
            //    AttachmentName = string.Empty,
            //    IsAttachmentAttached = false
            //});
            //AttachmentsListViewData.Add(new TinDeregestrationAttachmentsModel
            //{
            //    FieldTitle = AppResources.TinDeregistrationAttachmentCopyOfContractAfterClosing,
            //    FieldSubTitle = AppResources.TinDeregistration50MBMax,
            //    AttachmentName = string.Empty,
            //    IsAttachmentAttached = false
            //});
        }

        public async void NewAttachmentClicked()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new FilesUploadPopUpPageView(TinDeregistrationData.AttDetSet.Results, Models.ZakatInstalationModels.WhichAttachment.TINDeregistration
                           , TinDeregistrationData.CaseGuid, SelectedAttachment.DocType));

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
                    SummaryData = SelectedOutletOption.ActiveOutletDecisionOptions,
                    IsEditVisible = true
                });
                summaryReasonData.Add(new TINDeregistrationSummaryModel
                {
                    //TinDeregistrationData.AEffectiveDtH = DeregistrationDate.ToString("yyyy/MM/dd");

                    SummaryTitle = AppResources.TinDeregistrationDate,
                    SummaryData = DeregistrationDate.ToString("dd MMM yyyy"),
                    IsEditVisible = true
                });
                TinDeregistrationSummaryReasonData = new ObservableCollection<TINDeregistrationSummaryModel>(summaryReasonData);
            }
            catch (Exception ex)
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
                    SummaryTitle = AppResources.TinDeregistrationContactPersonName,
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
                    SummaryData = TinDeregistrationData.ADecTelNo,
                    IsEditVisible = true
                });
                summaryDeclarationData.Add(new TINDeregistrationSummaryModel
                {
                    SummaryTitle = AppResources.Date,
                    SummaryData = SubmissionDate.ToString("dd MMM yyyy"),
                    IsEditVisible = true
                });

                TinDeregistrationSummaryDeclarationData = new ObservableCollection<TINDeregistrationSummaryModel>(summaryDeclarationData);
            }
            catch (Exception ex)
            {

            }
        }

        private String ConvertDateFormat(DateTime newDate)
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
                Console.WriteLine(ex.Message);
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
                await Task.Run(() =>
                {
                    App.DisplayProgressView();
                });

                try
                {

                    TinDeregistrationData.ASubmissionDate = DeregistrationDate.ToString();
                    TinDeregistrationData.ADob = ConvertDateFormat(DeregistrationDate);

                    TinDeregistrationData.ASubmissionDate = ConvertDateFormat(DateTime.Now);
                    TinDeregistrationData.ASubmissionDateH = DeregistrationDate.ToString("yyyy/MM/dd");

                    TinDeregistrationData.AEffectiveDt = ConvertDateFormat(DeregistrationDate);

                    TinDeregistrationData.AEffectiveDtH = DeregistrationDate.ToString("yyyy/MM/dd");

                    TinDeregistrationData.ADecDate = ConvertDateFormat(DeregistrationDate);
                    TinDeregistrationData.AExpdt = ConvertDateFormat(DeregistrationDate);

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

                    AllOutlets = new ObservableCollection<OutletSetResult>(TinDeregistrationData.OutletSet.Results);
                    List<PermitSetResult> allPermitTypes = new List<PermitSetResult>(TinDeregistrationData.PermitSet.Results);

                    foreach (OutletSetResult outletInfo in AllOutlets)
                    {
                        //TODO
                        outletInfo.AOutletDobTb = ConvertDateFormat(DeregistrationDate);
                    }

                    foreach (PermitSetResult permitInfo in allPermitTypes)
                    {
                        if (!String.IsNullOrEmpty(permitInfo.APermitValfrDtTb))
                            permitInfo.APermitValfrDtTb = ConvertDateFormat(Convert.ToDateTime(permitInfo.APermitValfrDtTb));

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
                    await Task.Run(() =>
                    {
                        App.HideProgressView();
                    });
                    Console.WriteLine(ex.Message);
                }

                List<Attachment> tempAttachDetSet = new List<Attachment>();
                foreach (Attachment attachment in TinDeregistrationData.AttDetSet.Results)
                {
                    tempAttachDetSet.Add(attachment);
                }

                try
                {
                    TinDeregistrationData = await WebServiceManager.GaztTinDeregistrationSubmitRequestData(TinDeregistrationData);
                    TinDeregistrationData.AttDetSet.Results = tempAttachDetSet;

                    if (TinDeregistrationData.Xvoidz.Equals("X"))
                    {
                        string number = TinDeregistrationData.Fbnum;
                        string displayMessage = AppResources.VATRSuccessFullVoidMessage + " " + number;
                        await _dialogService.ShowMessage(displayMessage, AppResources.Information);
                        await Task.Run(() =>
                        {
                            App.HideProgressView();
                        });
                        _navigationService.GoBack();
                    }

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
                    Console.WriteLine(ex.Message);
                    await Task.Run(() =>
                    {
                        App.HideProgressView();
                    });
                }
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
                Console.WriteLine(ex.Message);
                await Task.Run(() =>
                {
                    App.HideProgressView();
                });
            }
        }
    }
}
