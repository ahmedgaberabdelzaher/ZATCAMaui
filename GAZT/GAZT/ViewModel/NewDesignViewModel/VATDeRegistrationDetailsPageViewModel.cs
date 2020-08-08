using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EGAZT.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
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

        public ICommand GoBackClick { get; set; }
        #endregion

        #region Commands
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

            OnContinueButtonClick = new Xamarin.Forms.Command(async () =>
            {
                TitleText = "Button New";

            });


            EnableReasonView();
            //EnableAttachmentsView();
            GoBackBtnTapped = new Command(this.GoBackBtnClicked);

            //EnableSummaryView();
            ReasonContinueBtnTapped = new Command(this.ReasonContinueBtnClicked);
            AttachmentsContinueBtnTapped = new Command(this.AttachmentsContinueBtnClicked);
            DeclarationContinueBtnTapped = new Command(this.DeclarationContinueBtnClicked);
            SummaryContinueBtnTapped = new Command(this.SummaryContinueBtnClicked);

            AddOutletDecisionOptions();
            AddOutletDocumentOptions();
            PopulateAttachmentsListViewTemplate();
            PopulateSummaryReasonData();
            PopulateSummaryDeclarationData();

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

        public void AddOutletDocumentOptions()
        {
            OutletDocumentOptions = new ObservableCollection<VATDeregistrationModel>();
            OutletDocumentOptions.Add(new VATDeregistrationModel
            {
                ActiveOutletDocumentOptions = "Income Statements",
                ActiveOutletDocumentOptionsIsSelected = true
            });
            OutletDocumentOptions.Add(new VATDeregistrationModel
            {
                ActiveOutletDocumentOptions = "Audited Reports",
                ActiveOutletDocumentOptionsIsSelected = false
            });
            OutletDocumentOptions.Add(new VATDeregistrationModel
            {
                ActiveOutletDocumentOptions = "Official Contracts",
                ActiveOutletDocumentOptionsIsSelected = true
            });
            OutletDocumentOptions.Add(new VATDeregistrationModel
            {
                ActiveOutletDocumentOptions = "Other Documents",
                ActiveOutletDocumentOptionsIsSelected = false
            });
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

            SelectedOutletOptionIndex = 0;
            IsReasonViewEnabled = true;
            IsOutletViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsDeclarationViewEnabled = false;
            IsSummaryViewEnabled = false;
        }

   

        public void EnableAttachmentsView()
        {
            CurrentStep = ProcessStep.Step2;

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

        }
        #endregion

        #region Summary View
        public void PopulateSummaryReasonData()
        {
            VATDeregistrationSummaryReasonData = new ObservableCollection<VATDeregistrationSummaryModel>();
            VATDeregistrationSummaryReasonData.Add(new VATDeregistrationSummaryModel
            {
                SummaryTitle = "Request Type",
                SummaryData = "Deregistration of VAT Account",
                IsEditVisible = true
            });
            VATDeregistrationSummaryReasonData.Add(new VATDeregistrationSummaryModel
            {
                SummaryTitle = "Reason",
                SummaryData = "Total value of VAT eligible support",
                IsEditVisible = true
            });

        }

        public void PopulateSummaryDeclarationData()
        {
            VATDeregistrationSummaryDeclarationData = new ObservableCollection<VATDeregistrationSummaryModel>();
            VATDeregistrationSummaryDeclarationData.Add(new VATDeregistrationSummaryModel
            {
                SummaryTitle = AppResources.TinDeregistrationContactPersonName,
                SummaryData = "Hardy",
                IsEditVisible = true
            });
            VATDeregistrationSummaryDeclarationData.Add(new VATDeregistrationSummaryModel
            {
                SummaryTitle = AppResources.TinDeregistrationDesignation,
                SummaryData = "Senior Director",
                IsEditVisible = true
            });
            VATDeregistrationSummaryDeclarationData.Add(new VATDeregistrationSummaryModel
            {
                SummaryTitle = AppResources.MobileNumber,
                SummaryData = "+966 551 234 567",
                IsEditVisible = true
            });
            VATDeregistrationSummaryDeclarationData.Add(new VATDeregistrationSummaryModel
            {
                SummaryTitle = AppResources.ZZDateofBirth,
                SummaryData = "07 June 1995",
                IsEditVisible = true
            });
        }

        #endregion
    }
}
