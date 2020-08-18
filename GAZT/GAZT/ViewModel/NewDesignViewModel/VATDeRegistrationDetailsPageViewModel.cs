using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.Views.NewDesign.GenericPickers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Plugin.FilePicker;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    public class VATDeRegistrationDetailsPageViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnContinueButtonClick { get; set; }
        public ICommand GoBackBtnTapped { get; set; }
        public ICommand CloseBtnTapped { get; set; }
        public ICommand GoBackClick { get; set; }
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
                _attachmentName = value;
                RaisePropertyChanged("AttachmentName");
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

        private bool _isReturnFilingViewEnabled = true;
        public bool IsReturnFilingViewEnabled
        {
            get
            {
                return _isReturnFilingViewEnabled;
            }
            set
            {
                _isReturnFilingViewEnabled = value;
                RaisePropertyChanged("IsReturnFilingViewEnabled");
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
        private string _titleText = string.Empty;
        public string TitleText
        {
            get
            {
                return _titleText;
            }
            set
            {
                _titleText = value;
                RaisePropertyChanged("TitleText");
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
                _reasonTitle = value;
                RaisePropertyChanged("ReasonTitle");
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

        private string _suspendedStartDate = string.Empty;
        public string SuspendedStartDate
        {
            get
            {
                return _suspendedStartDate;
            }
            set
            {
                _suspendedStartDate = value;
                RaisePropertyChanged("SuspendedStartDate");
            }
        }
        private string _suspendedEndDate = string.Empty;
        public string SuspendedEndDate
        {
            get
            {
                return _suspendedEndDate;
            }
            set
            {
                _suspendedEndDate = value;
                RaisePropertyChanged("SuspendedEndDate");
            }
        }
        private string _nextFilingStartDate = string.Empty;
        public string NextFilingStartDate
        {
            get
            {
                return _nextFilingStartDate;
            }
            set
            {
                _nextFilingStartDate = value;
                RaisePropertyChanged("NextFilingStartDate");
            }
        }
        private string _nextFilingEndDate = string.Empty;
        public string NextFilingEndDate
        {
            get
            {
                return _nextFilingEndDate;
            }
            set
            {
                _nextFilingEndDate = value;
                RaisePropertyChanged("NextFilingEndDate");
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
                _nextFilingDueDate = value;
                RaisePropertyChanged("NextFilingDueDate");
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
                _startDate = value;
                RaisePropertyChanged("StartDate");
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
                _endDate = value;
                RaisePropertyChanged("EndDate");
            }
        }
        private string _fromDate = string.Empty;
        public string FromDate
        {
            get
            {
                return _fromDate;
            }
            set
            {
                _fromDate = value;
                RaisePropertyChanged("FromDate");
            }
        }
        private string _toDate = string.Empty;
        public string ToDate
        {
            get
            {
                return _toDate;
            }
            set
            {
                _toDate = value;
                RaisePropertyChanged("ToDate");
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
        private string _txtIDNumber = string.Empty;
        public string TxtIDNumber
        {
            get
            {
                return _txtIDNumber;
            }
            set
            {
                _txtIDNumber = value;
                RaisePropertyChanged("TxtIDNumber");
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
                _iDType = value;
                RaisePropertyChanged("IDType");
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
                _DOB = value;
                RaisePropertyChanged("DOB");
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
                _contactPersonName = value;
                RaisePropertyChanged("ContactPersonName");
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
                RaisePropertyChanged("PickerModel");
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
                _datepickerModel = value;
                RaisePropertyChanged("DatePickerModel");
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
                if (outletDecisionOptions == value)
                {
                    return;
                }

                outletDecisionOptions = value;
                RaisePropertyChanged("OutletDecisionOptions");
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
                vatDeregistrationModel = value;
                RaisePropertyChanged("VATDeregistrationModel");
            }
        }

        public ObservableCollection<VATDeregistrationModel> outletDocumentOptions { get; set; }
        public ObservableCollection<VATDeregistrationModel> OutletDocumentOptions
        {
            get
            {
                return outletDocumentOptions;
            }

            set
            {
                if (outletDocumentOptions == value)
                {
                    return;
                }

                outletDocumentOptions = value;
                RaisePropertyChanged("OutletDocumentOptions");
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
                if (attachmentsListViewData == value)
                {
                    return;
                }

                attachmentsListViewData = value;
                RaisePropertyChanged("AttachmentsListViewData");
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
                if (_vatDeregistrationSummaryReasonData == value)
                {
                    return;
                }

                _vatDeregistrationSummaryReasonData = value;
                RaisePropertyChanged("VATDeregistrationSummaryReasonData");
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

        private VATDeregistrationModel _selectedOutletOption;
        public VATDeregistrationModel SelectedOutletOption
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
        private VATDeregistrationModel _selectedDocumentOption;
        public VATDeregistrationModel SelectedDocumentOption
        {
            get
            {
                return _selectedDocumentOption;
            }
            set
            {
                _selectedDocumentOption = value;
                //SelectedOutletOptionIndex = OutletDecisionOptions.IndexOf(_selectedOutletOption as TINDeregistrationModel);
                RaisePropertyChanged("SelectedDocumentOption");
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
        private VATDeregistrationAttachmentsModel _selectedAttachment { get; set; }
        public VATDeregistrationAttachmentsModel SelectedAttachment
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

        public VATDeRegistrationDetailsPageViewModel(INavigationService navigationService, IDialogService dialogService)
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
            GoBackClick = new Command(async () =>
            {
                _navigationService.GoBack();
            });
            CloseBtnTapped = new Command(async () =>
            {
                _navigationService.GoBack();
            });

            OnContinueButtonClick = new Xamarin.Forms.Command(async () =>
            {
                TitleText = "Button New";

            });
       
            //EnableAttachmentsView();
            GoBackBtnTapped = new Command(this.GoBackBtnClicked);

            //EnableSummaryView();
            ReasonContinueBtnTapped = new Command(this.ReasonContinueBtnClicked);
            AttachmentsContinueBtnTapped = new Command(this.AttachmentsContinueBtnClicked);
            DeclarationContinueBtnTapped = new Command(this.DeclarationContinueBtnClicked);
            SummaryContinueBtnTapped = new Command(this.SummaryContinueBtnClicked);
            OnVatRegistrationReasonTapped = new Command(this.OnVatRegistrationReasonClicked);
            OnVatRegistrationDateTapped = new Command(this.OnVatRegistrationReasonDateClicked);

            AddOutletDecisionOptions();
            PopulateAttachmentsListViewTemplate();
       

            VATDeregistrationModel = new VATDeregistrationModel();
           

            SelectedOutletOption = new VATDeregistrationModel();

            SelectedDocumentOption = new VATDeregistrationModel();

            EnableReasonView();

        }
        public void GoBackBtnClicked()
        {
            try
            {
                switch (CurrentStep)
                {
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

        public  void OnVatRegistrationReasonClicked()
        {
           // ObservableCollection < VATDeregistrationReasonModel> reasonList = new ObservableCollection<VATDeregistrationReasonModel>();
            ObservableCollection<string> reasonDescription = new ObservableCollection<string>();
            string reqType = string.Empty;
            if(SelectedOutletOption.ActiveOutletDecisionOptions.Contains("De-Registration of VAT Account"))
            {
                reqType = "VT_DREG";
            }
            else
            {
                reqType = "VT_SUSP";
            }
            VATDeregistrationModelRootObject reasonList =  WebServiceManager.GAZTGETVATDeregReasonDropdownList(reqType);

            for (int i = 0; i < reasonList.d.results.Count; i++)
            {
                reasonDescription.Add(reasonList.d.results[i].Rdesc);
            }
            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = reasonDescription;
            genericPickerModel.PickerTitle = "Reason List";
            genericPickerModel.PickerId = "reasonTypePicker";
            try
            {
                
                PopupNavigation.Instance.PushAsync(new PickerPageView(genericPickerModel));
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

        public async void OnVatRegistrationReasonDateClicked()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new CalendarPickerPageView());
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



        public void AddOutletDecisionOptions()
        {
            OutletDecisionOptions = new ObservableCollection<VATDeregistrationModel>();
            OutletDecisionOptions.Add(new VATDeregistrationModel
            {
                ActiveOutletDecisionOptions = "De-Registration of VAT Account",
                ActiveOutletDecisionOptionsIsSelected = true
            });
            OutletDecisionOptions.Add(new VATDeregistrationModel
            {
                ActiveOutletDecisionOptions = "VAT Return Filing Obligation Suspension",
                ActiveOutletDecisionOptionsIsSelected = false
            });

        }

        public void returnFilingOptionsView()
        {


        }

        public async void AddOutletDocumentOptions()
        {
            ObservableCollection<string> reasonDescription = new ObservableCollection<string>();
            string reqType = string.Empty;
                if (SelectedOutletOptionIndex == 0)
                {
                 reqType = "VT_DREG";
                }
                else
                {
                    reqType = "VT_SUSP";
                }
            OutletDocumentOptions = new ObservableCollection<VATDeregistrationModel>();
            string attachmentType = string.Empty;
            if (reqType != null || reqType != string.Empty)
                {
                    VATDeRegistrationAttachmentDropdownDetails reasonList = await WebServiceManager.GAZTGETVATDeregAttachmentsDropdownList(reqType);
                    for (int i = 0; i < reasonList.VatDeregSubItemsSet.Results.Length;i++)
                    {
                   attachmentType = reasonList.VatDeregSubItemsSet.Results[i].Txt50;
                    if (attachmentType != string.Empty)
                    {
                        OutletDocumentOptions.Add(new VATDeregistrationModel
                        {
                            ActiveOutletDocumentOptions = attachmentType,
                            ActiveOutletDocumentOptionsIsSelected = true
                        });
                    }
                  
                 }
                }

        }

        public async void ReasonContinueBtnClicked()
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
                _navigationService.NavigateTo(App.VATDeregistrationSuccessPage);
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
        public void EnableReasonView()
        {
            CurrentStep = ProcessStep.Step1;
            if (OutletDecisionOptions != null)
            {
                SelectedOutletOption = OutletDecisionOptions[0];
            }
            IsBackButtonVisible = false;
            SelectedOutletOptionIndex = 0;
            IsReasonViewEnabled = true;
            IsOutletViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsDeclarationViewEnabled = false;
            IsSummaryViewEnabled = false;

            AddOutletDocumentOptions();

        }

        public void EnableAttachmentsView()
        {
            CurrentStep = ProcessStep.Step2;
            if (OutletDocumentOptions != null)
            {
                SelectedDocumentOption = OutletDocumentOptions[0];
            }
            IsBackButtonVisible = true;
            IsReasonViewEnabled = false;
            IsOutletViewEnabled = false;
            IsAttachmentsViewEnabled = true;
            IsDeclarationViewEnabled = false;
            IsSummaryViewEnabled = false;
        }

        public void EnableDeclarationView()
        {
            CurrentStep = ProcessStep.Step3;

            IsReasonViewEnabled = false;
            IsOutletViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsDeclarationViewEnabled = true;
            IsSummaryViewEnabled = false;
        }

        public void EnableSummaryView()
        {
            CurrentStep = ProcessStep.Step4;

            IsReasonViewEnabled = false;
            IsOutletViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsDeclarationViewEnabled = false;
            IsSummaryViewEnabled = true;

            PopulateSummaryReasonData();
            PopulateSummaryDeclarationData();
        }


        #region Attachments View
        public void PopulateAttachmentsListViewTemplate()
        {
            AttachmentsListViewData = new ObservableCollection<VATDeregistrationAttachmentsModel>();
  
            AttachmentsListViewData.Add(new VATDeregistrationAttachmentsModel
            {
                FieldTitle = "Attachment",
                FieldSubTitle = AppResources.TinDeregistration20MB,
                AttachmentName = "File2.pdf",
                IsAttachmentAttached = true
            });
            AttachmentsListViewData.Add(new VATDeregistrationAttachmentsModel
            {
                FieldTitle = AppResources.VATRAttachment,
                FieldSubTitle = AppResources.TinDeregistration50MBMax,
                AttachmentName = string.Empty,
                IsAttachmentAttached = false
            });

        }
        #endregion

        #region Summary View
        public void PopulateSummaryReasonData()
        {
            VATDeregistrationSummaryReasonData = new ObservableCollection<VATDeregistrationSummaryModel>();
            VATDeregistrationSummaryReasonData.Add(new VATDeregistrationSummaryModel
            {
                SummaryTitle = "Request Type",
                SummaryData = SelectedOutletOption.ActiveOutletDecisionOptions,
                IsEditVisible = true
            });
            VATDeregistrationSummaryReasonData.Add(new VATDeregistrationSummaryModel
            {
                SummaryTitle = "Reason",
                SummaryData = ReasonTitle,
                IsEditVisible = true
            });

        }

        public void PopulateSummaryDeclarationData()
        {
            VATDeregistrationSummaryDeclarationData = new ObservableCollection<VATDeregistrationSummaryModel>();
            VATDeregistrationSummaryDeclarationData.Add(new VATDeregistrationSummaryModel
            {
                SummaryTitle = "ID Type",
                SummaryData = IDType,
                IsEditVisible = true
            });
            VATDeregistrationSummaryDeclarationData.Add(new VATDeregistrationSummaryModel
            {
                SummaryTitle = "ID Number",
                SummaryData = TxtIDNumber,
                IsEditVisible = true
            });
            VATDeregistrationSummaryDeclarationData.Add(new VATDeregistrationSummaryModel
            {
                SummaryTitle = "Date of Birth",
                SummaryData = DOB,
                IsEditVisible = true
            });
            VATDeregistrationSummaryDeclarationData.Add(new VATDeregistrationSummaryModel
            {
                SummaryTitle = AppResources.TinDeregistrationContactPersonName,
                SummaryData = ContactPersonName,
                IsEditVisible = true
            });
        }

                
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
                        //SelectedAttachment.AttachmentName = AttachmentName;
                        //SelectedAttachment.IsAttachmentAttached = true;
                        //AttachmentsListViewData.RemoveAt(SelectedOutletOptionIndex);
                        //AttachmentsListViewData.Insert(SelectedOutletOptionIndex, SelectedAttachment);

                        if (fileData.FileName.Contains("."))
                        {
                            string Extention = fileData.FileName.Split('.')[1];
                            if (Extention.ToLower() == "doc" || Extention.ToLower() == "docx" || Extention.ToLower() == "jpg" || Extention.ToLower() == "jpeg" || Extention.ToLower() == "pdf"
                                || Extention.ToLower() == "xlsx" || Extention.ToLower() == "xls" || Extention.ToLower() == "png" || Extention.ToLower() == "ppt" || Extention.ToLower() == "pptx"
                                || Extention.ToLower() == "gif" || Extention.ToLower() == "txt")
                            {
                                if (TotalAttachmentSize <= 300)
                                {
                                    AttachmentSize = Math.Round(Convert.ToDecimal((Convert.ToDouble(attachment.Length) / 1048576.0)), 2);
                                    decimal AttachmentSizeTillFourDecimal = Math.Round(Convert.ToDecimal((Convert.ToDouble(attachment.Length) / 1048576.0)), 4);

                                    if (Convert.ToDecimal(AttachmentSize) <= 5)
                                    {
                                        if (Convert.ToDecimal(AttachmentSizeTillFourDecimal) > 0)
                                        {
                                            bool IsAttachmentPresent = false;

                                            if (IsAttachmentPresent == false)
                                            {
                                                string attachmentType = UtilityManager.GetContentType(Extention);
                                            }
                                            else
                                            {
                                                AttachmentName = string.Empty;

                                                await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_FileWithTheSameNameAlreadyExists, AppResources.Information);
                                            }
                                        }
                                        else
                                        {
                                            AttachmentName = string.Empty;

                                            await _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
                                        }
                                    }
                                    else
                                    {
                                        AttachmentName = string.Empty;

                                        await _dialogService.ShowMessage(AppResources.ZFilesizeshouldnotbemorethan20MB, AppResources.Information);
                                    }
                                }
                                else
                                {
                                    AttachmentName = string.Empty;

                                    await _dialogService.ShowMessage(AppResources.ZTotalFilesizeshouldnotbemorethan300MB, AppResources.Information);
                                }
                            }
                            else
                            {
                                AttachmentName = string.Empty;

                                await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
                            }
                        }
                        else
                        {
                            AttachmentName = string.Empty;

                            await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
                        }
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

        #endregion
    }
}
