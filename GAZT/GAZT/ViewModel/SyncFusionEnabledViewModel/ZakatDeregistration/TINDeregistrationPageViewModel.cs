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
                IsAttachmentAttached = false
            });
            AttachmentsListViewData.Add(new TinDeregestrationAttachmentsModel
            {
                FieldTitle = AppResources.TinDeregistrationAttachmentCopyOfLicneseAfterClosing,
                FieldSubTitle = AppResources.TinDeregistration20MB,
                IsAttachmentAttached = false
            });
            AttachmentsListViewData.Add(new TinDeregestrationAttachmentsModel
            {
                FieldTitle = AppResources.TinDeregistrationAttachmentCopyOfCRAfterClosing,
                FieldSubTitle = AppResources.TinDeregistration50MBMax,
                IsAttachmentAttached = false
            });
            AttachmentsListViewData.Add(new TinDeregestrationAttachmentsModel
            {
                FieldTitle = AppResources.TinDeregistrationAttachmentOwnershipSellingAgreement,
                FieldSubTitle = AppResources.TinDeregistration50MBMax,
                IsAttachmentAttached = false
            });
            AttachmentsListViewData.Add(new TinDeregestrationAttachmentsModel
            {
                FieldTitle = AppResources.TinDeregistrationAttachmentCopyOfPartnersDecision,
                FieldSubTitle = AppResources.TinDeregistration50MBMax,
                IsAttachmentAttached = false
            });
            AttachmentsListViewData.Add(new TinDeregestrationAttachmentsModel
            {
                FieldTitle = AppResources.TinDeregistrationAttachmentCopyOfContractAfterClosing,
                FieldSubTitle = AppResources.TinDeregistration50MBMax,
                IsAttachmentAttached = false
            });
        }
        #endregion
    }
}
