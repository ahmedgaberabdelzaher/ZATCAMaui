using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models.VATRefunds;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.VATRefunds
{
    public class VATRefundDetailsPageViewModel: ViewModelBase
    {
        #region Commands

        public ICommand GoBackBtnTapped { get; set; }
        public ICommand CloseBtnTapped { get; set; }
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        #endregion
       
        private VatRefHeaderSetResult _vatRefundsHeaderSet { get; set; }
        public VatRefHeaderSetResult VATRefundsHeaderSet
        {
            get
            {
                return _vatRefundsHeaderSet;
            }

            set
            {

                _vatRefundsHeaderSet = value;
                RaisePropertyChanged("VATRefundsHeaderSet");
            }
        }

        private ObservableCollection<VatRefSubItemsSetResult> _vatRefundsSubItemReturnsSet { get; set; }
        public ObservableCollection<VatRefSubItemsSetResult> VATRefundsSubItemReturnsSet
        {
            get
            {
                return _vatRefundsSubItemReturnsSet;
            }

            set
            {

                _vatRefundsSubItemReturnsSet = value;
                RaisePropertyChanged("VATRefundsSubItemReturnsSet");
            }
        }

        private VatRefundsListResultModel _vatRefundsListResultModel = null;
        public VatRefundsListResultModel VatRefundsListResultModel
        {
            get
            {
                return _vatRefundsListResultModel;
            }

            set
            {

                _vatRefundsListResultModel = value;
                RaisePropertyChanged("VatRefundsListResultModel");
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

        public VATRefundDetailsPageViewModel(INavigationService navigationService, IDialogService dialogService)
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

            GoBackBtnTapped = new Command(async () =>
            {
                _navigationService.GoBack();
            });

            VATRefundsHeaderSet = new VatRefHeaderSetResult();
            VATRefundsSubItemReturnsSet = new ObservableCollection<VatRefSubItemsSetResult>();
            VatRefundsDisplayDataModel = new VatRefundDisplayDataModel();

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

        public async void ReloadData(VatRefundsListResultModel vATRefundsModel)
        {
            Console.WriteLine(vATRefundsModel);

            if(VatRefundsListResultModel == null)
            {
                VatRefundsListResultModel = new VatRefundsListResultModel();
            }

            VatRefundsListResultModel = vATRefundsModel;
            VATRefundsHeaderSet = VatRefundsListResultModel.VatRefHeaderSet.Results[0];
            VATRefundsSubItemReturnsSet = new ObservableCollection<VatRefSubItemsSetResult>(VatRefundsListResultModel.VatRefSubItemsSet.Results);

            try
            {
                await Task.Run(() =>
                {
                    App.DisplayProgressView();
                });

                VatRefundsDisplayDataModel = await WebServiceManager.GAZTGetVATRefundDisplayBankIdTypeData(VATRefundsHeaderSet.RefundFbnum);

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
