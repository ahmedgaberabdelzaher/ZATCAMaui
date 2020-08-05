using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EGAZT.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Xamarin.Forms;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration
{
    public class TINDeregistrationPageViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand GoBackClick { get; set; }
        #endregion

        #region Commands
        public ICommand ReasonContinueBtnTapped { get; set; }
        #endregion

        #region Properties

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

        public TINDeregistrationModel tinDeregistrationModel { get; set; }
        public TINDeregistrationModel TinDeregistrationModel
        {
            get
            {
                return tinDeregistrationModel;
            }

            set
            {
                if (tinDeregistrationModel == value)
                {
                    return;
                }

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
                if (outletDecisionOptions == value)
                {
                    return;
                }

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
                if (attachmentsListViewData == value)
                {
                    return;
                }

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
                if (_tinDeregistrationSummaryReasonData == value)
                {
                    return;
                }

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
                if (_tinDeregistrationSummaryOutletData == value)
                {
                    return;
                }

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
                if (_tinDeregistrationSummaryDeclarationData == value)
                {
                    return;
                }

                _tinDeregistrationSummaryDeclarationData = value;
                RaisePropertyChanged("TinDeregistrationSummaryDeclarationData");
            }
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
            GoBackClick = new Command(async () =>
            {
                _navigationService.GoBack();
            });

            ReasonContinueBtnTapped = new Command(this.ReasonContinueBtnClicked);

            TinDeregistrationModel = new TINDeregistrationModel();
            AddOutletDecisionOptions();
            PopulateAttachmentsListViewTemplate();
            PopulateSummaryReasonData();
            PopulateSummaryDeclarationData();
        }

        public void AddOutletDecisionOptions()
        {
            OutletDecisionOptions = new ObservableCollection<TINDeregistrationModel>();
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

        public void EnableReasonView()
        {
            IsReasonViewEnabled = true;
        }

        public void EnableOutletDetaislView()
        {
            IsReasonViewEnabled = false;
            IsOutletViewEnabled = true;
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


        #region Attachments View
        public void PopulateAttachmentsListViewTemplate()
        {
            AttachmentsListViewData = new ObservableCollection<TinDeregestrationAttachmentsModel>();
            AttachmentsListViewData.Add(new TinDeregestrationAttachmentsModel
            {
                FieldTitle = AppResources.TinDeregistrationAttachmentCopyOfDeclaringBankruptcy,
                FieldSubTitle = AppResources.TinDeregistration20MB,
                AttachmentName = "File1.pdf",
                IsAttachmentAttached = true
            });
            AttachmentsListViewData.Add(new TinDeregestrationAttachmentsModel
            {
                FieldTitle = AppResources.TinDeregistrationAttachmentCopyOfLicneseAfterClosing,
                FieldSubTitle = AppResources.TinDeregistration20MB,
                AttachmentName = "File2.pdf",
                IsAttachmentAttached = true
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
    }
}
