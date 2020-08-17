using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models.VATRefunds;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.VATRefunds
{
    public class VATRefundsNewRequestViewModel: BaseViewModel
    {
        #region Commands

        public ICommand GoBackBtnTapped { get; set; }
        public ICommand CloseBtnTapped { get; set; }

        #endregion

        public ObservableCollection<VATRefundsModel> _vatRefundsModel { get; set; }
        public ObservableCollection<VATRefundsModel> VATRefundsModel
        {
            get
            {
                return _vatRefundsModel;
            }

            set
            {

                _vatRefundsModel = value;
                RaisePropertyChanged("VATRefundsModel");
            }
        }

        private VatRefundDisplayDataModel _vatRefundsDisplayDataModel = null;
        public VatRefundDisplayDataModel VatRefundsDisplayDataModel
        {
            get
            {
                return _vatRefundsDisplayDataModel;
            }

            set
            {

                _vatRefundsDisplayDataModel = value;
                RaisePropertyChanged("VatRefundsDisplayDataModel");
            }
        }

        private VarRefundIbanDataModel _vatRefundsIbanDataModel = null;
        public VarRefundIbanDataModel VatRefundsIbanDataModel
        {
            get
            {
                return _vatRefundsIbanDataModel;
            }

            set
            {

                _vatRefundsIbanDataModel = value;
                RaisePropertyChanged("VatRefundsIbanDataModel");
            }
        }

        private ObservableCollection<VarRefundIbanDataModelMetadataResult> _ibanData = null;
        public ObservableCollection<VarRefundIbanDataModelMetadataResult> IbanData
        {
            get
            {
                return _ibanData;
            }

            set
            {

                _ibanData = value;
                RaisePropertyChanged("IbanData");
            }
        }

        private bool _isAddAccountVisisble = false;
        public bool IsAddAccountVisisble
        {
            get
            {
                return _isAddAccountVisisble;
            }

            set
            {

                _isAddAccountVisisble = value;
                RaisePropertyChanged("IsAddAccountVisisble");
            }
        }

        public VATRefundsNewRequestViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }

            GoBackBtnTapped = new Command(async () =>
            {
                _navigationService.GoBack();
            });

            //GoBackBtnTapped = new Command(this.GoBackBtnClicked);
            //ReasonContinueBtnTapped = new Command(this.ReasonContinueBtnClicked);
            //OutletContinueBtnTapped = new Command(this.OutletContinueBtnClicked);
            //AttachmentsContinueBtnTapped = new Command(this.AttachmentsContinueBtnClicked);
            //DeclarationContinueBtnTapped = new Command(this.DeclarationContinueBtnClicked);
            //SummaryContinueBtnTapped = new Command(this.SummaryContinueBtnClicked);
            //OnTinRegisrtationReasonDateTapped = new Command(this.OnTinRegisrtationReasonDateClicked);
            //OnTinRegistrationReasonTapped = new Command(this.OnTinRegisrtationReasonClicked);
            //TinDeregistrationModel = new TINDeregistrationModel();
            //SelectedOutletOption = new TINDeregistrationModel();

            //AddOutletDecisionOptions();
            //PopulateAttachmentsListViewTemplate();
            //PopulateSummaryReasonData();
            //PopulateSummaryDeclarationData();
            //EnableReasonView();
        }

        public async void ReloadData()
        {
            try
            {
                await Task.Run(() =>
                {
                    App.DisplayProgressView();
                });

                VatRefundsDisplayDataModel = await WebServiceManager.GAZTGetVATRefundDisplayBankIdTypeData("");

                VatRefundsIbanDataModel = await WebServiceManager.GAZTGetVATRefundGetIbanData("");
                IbanData = new ObservableCollection<VarRefundIbanDataModelMetadataResult>(VatRefundsIbanDataModel.IbanSet.Results);

                if(IbanData == null || IbanData.Count == 0)
                {
                    IsAddAccountVisisble = true;
                }
                else
                {
                    IsAddAccountVisisble = false;
                }

                await Task.Run(() =>
                {
                    App.HideProgressView();
                });
            }
            catch (GAZTErrorException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    App.HideProgressView();
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
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
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }


    }
}
