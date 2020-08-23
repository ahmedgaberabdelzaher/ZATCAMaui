using EGAZT.Models.ChangeFillingPeriodModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.ChangeFillingPeriodViewModel
{


    public class ChangeFillingPeriodViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        int selectedPage = (int)PagesEnum.MyRequestsView;
        #endregion

        #region Enums
        enum PagesEnum
        {
            MyRequestsView,
            FrequencyDetailsView,
            AttachmentsView,
            DeclarationView,
            SummaryView,
            SuccessView,
        }
        #endregion

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

        #endregion


        private void Backnavigations()
        {
            switch (selectedPage)
            {
                case (int)PagesEnum.MyRequestsView:

                    break;
                case (int)PagesEnum.FrequencyDetailsView:
                    EnableMyRequestsView();
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
                case (int)PagesEnum.SuccessView:
                    EnableSummaryView();
                    break;

                default:
                    // code block
                    break;
            }
        }

        public ChangeFillingPeriodViewModel(INavigationService navigationService, IDialogService dialogService)
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
                Backnavigations();
            });

            CloseClick = new Command(async () =>
            {
                EnableMyRequestsView();
            });

            FrequencyContinueBtnTapped = new Command(this.FrequencyContinueBtnClicked);
            AttachmentsContinueBtnTapped = new Command(this.AttachmentsContinueBtnClicked);
            DeclarationContinueBtnTapped = new Command(this.DeclarationContinueBtnClicked);
            MyRequestsButtonTapped = new Command(this.MyRequestsButtonClicked);
            GoBackToFrequencyDetails = new Command(this.GoBackToFrequencyDetailsClicked);
            GoBackToAttachments = new Command(this.GoBackToAttachmentsClicked);
            GoBackToDeclaration = new Command(this.GoBackToDeclarationClicked);
           // GoBackToDashBoardTapped = new Command(this.GoBackToDashboardClicked);

            AddOutletDecisionOptions();
            PopulateFrequencyDetailsListViewData();
            PopulateChangeFillingAttachmentsListViewData();
            PopulateDeclarationListViewData();
            PopulateMyRequestsListViewData();

            SelectedOutletOption = new ChangeFillingPeriodModel();
        }

        public void EnableMyRequestsView()
        {
            IsMyRequestsViewEnabled = true;
            IsFrequencyViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsDeclarationViewEnabled = false;
            IsSummaryViewEnabled = false;
            selectedPage = (int)PagesEnum.MyRequestsView;
        }
        public void EnableFrequencyDetailsView()
        {
            IsMyRequestsViewEnabled = false;
            IsFrequencyViewEnabled = true;
            IsAttachmentsViewEnabled = false;
            IsDeclarationViewEnabled = false;
            IsSummaryViewEnabled = false;
            selectedPage = (int)PagesEnum.FrequencyDetailsView;
        }

        public void EnableAttachmentsView()
        {
            IsMyRequestsViewEnabled = false;
            IsFrequencyViewEnabled = false;
            IsAttachmentsViewEnabled = true;
            IsDeclarationViewEnabled = false;
            IsSummaryViewEnabled = false;
            selectedPage = (int)PagesEnum.AttachmentsView;
        }

        public void EnableDeclarationView()
        {
            IsMyRequestsViewEnabled = false;
            IsFrequencyViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsDeclarationViewEnabled = true;
            IsSummaryViewEnabled = false;
            selectedPage = (int)PagesEnum.DeclarationView;
        }

        public void EnableSummaryView()
        {
            IsMyRequestsViewEnabled = false;
            IsFrequencyViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsDeclarationViewEnabled = false;
            IsSummaryViewEnabled = true;
            selectedPage = (int)PagesEnum.SummaryView;

        }

        private bool _isMyRequestsViewEnabled = true;
        public bool IsMyRequestsViewEnabled
        {
            get
            {
                return _isMyRequestsViewEnabled;
            }
            set
            {
                _isMyRequestsViewEnabled = value;
                RaisePropertyChanged("IsMyRequestsViewEnabled");
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

        private string _selectedAttachmentText = "Attachment - 12 Months Taxable Revenue";
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


    }

}
