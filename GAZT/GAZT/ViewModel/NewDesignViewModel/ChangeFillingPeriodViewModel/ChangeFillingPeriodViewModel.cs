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
using GAZT.Manager;

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
        #endregion

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

        #region Commands

        public ICommand GoBackClick { get; set; }
        public ICommand CloseClick { get; set; }
        public ICommand FrequencyContinueBtnTapped { get; set; }
        public ICommand AttachmentsContinueBtnTapped { get; set; }
        public ICommand DeclarationContinueBtnTapped { get; set; }
        public ICommand MyRequestsButtonTapped { get; set; }
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

        private DateTime _pickedDate = DateTime.Now;

        public DateTime PickedDate
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

        private string _idNumber = "";

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

            });

            IdTypeSpinnerTapped = new Command(async () =>
            {

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
            MyRequestsButtonTapped = new Command(this.MyRequestsButtonClicked);
            GoBackToFrequencyDetails = new Command(this.GoBackToFrequencyDetailsClicked);
            GoBackToAttachments = new Command(this.GoBackToAttachmentsClicked);
            GoBackToDeclaration = new Command(this.GoBackToDeclarationClicked);
            NewAttachmentTapped = new Command(this.NewAttachmentClicked);
            // GoBackToDashBoardTapped = new Command(this.GoBackToDashboardClicked);

            AddOutletDecisionOptions();
            PopulateFrequencyDetailsListViewData();
            PopulateChangeFillingAttachmentsListViewData();
            PopulateDeclarationListViewData();
            PopulateMyRequestsListViewData();

            SelectedOutletOption = new ChangeFillingPeriodModel();

            genericDatePickerModel = new GenericDatePickerModel();
            genericDatePickerModel.DatePickerTitle = "Select Date";
            genericDatePickerModel.PickerId = "DatePicker";
        }

        public void ResetData()
        {
            EnableFrequencyDetailsView();
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

        public void AddOutletDecisionOptions()
        {
            OutletDecisionOptions = new ObservableCollection<ChangeFillingPeriodModel>();
            OutletDecisionOptions.Add(new ChangeFillingPeriodModel
            {
                ActiveOutletDecisionOptions = AppResources.ChangeFillingPeriodAttachmentsTwoYears,
                ActiveOutletDecisionOptionsIsSelected = true
            });
            OutletDecisionOptions.Add(new ChangeFillingPeriodModel
            {
                ActiveOutletDecisionOptions = AppResources.ChangeFillingPeriodAttachmentsTwelveMonths,
                ActiveOutletDecisionOptionsIsSelected = false
            });
            OutletDecisionOptions.Add(new ChangeFillingPeriodModel
            {
                ActiveOutletDecisionOptions = AppResources.ChangeFillingPeriodAttachmentsOtherDocuments,
                ActiveOutletDecisionOptionsIsSelected = false
            });

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
            var attachmentsListViewData = new ObservableCollection<Attachment>();

            foreach (Attachment attachemnt in attachments)
            {
                attachmentsListViewData.Add(attachemnt);
            }

            AttachmentsListViewData = attachmentsListViewData;
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
                        var resultData = await WebServiceManager.GAZTGetVATChangeFillingPeriodRequestData("", "");
                        if (resultData != null && resultData.d != null)
                        {
                            //resultData.d;
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

        #endregion


    }

}
