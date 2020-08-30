using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models.VATRefunds;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.VATRefunds
{

    public class VATRefundListPageViewModel:BaseViewModel
    {
        #region Commands

        public ICommand GoBackBtnTapped { get; set; }
        public ICommand CloseBtnTapped { get; set; }
        public ICommand VATRefundSelectionChanged { get; set; }

        #endregion

        private ObservableCollection<VatRefHeaderSetResult> _vatRefundsModel { get; set; }
        public ObservableCollection<VatRefHeaderSetResult> VATRefundsModel
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

        private ObservableCollection<VatRefHeaderSetResult> _vatRefundsSet { get; set; }
        public ObservableCollection<VatRefHeaderSetResult> VATRefundsSet
        {
            get
            {
                return _vatRefundsSet;
            }
            set
            {

                _vatRefundsSet = value;
                RaisePropertyChanged("VATRefundsSet");
            }
        }

        private ObservableCollection<VatRefHeaderSetResult> _vatRefundsSetCopy { get; set; }
        public ObservableCollection<VatRefHeaderSetResult> VATRefundsSetCopy
        {
            get
            {
                return _vatRefundsSetCopy;
            }
            set
            {

                _vatRefundsSetCopy = value;
                RaisePropertyChanged("VATRefundsSetCopy");
            }
        }

        private ObservableCollection<VatRefHeaderSetResult> _vatRefundsSearchSet { get; set; }
        public ObservableCollection<VatRefHeaderSetResult> VATRefundsSearchSet
        {
            get
            {
                return _vatRefundsSearchSet;
            }
            set
            {

                _vatRefundsSearchSet = value;
                RaisePropertyChanged("VATRefundsSearchSet");
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

       

        //

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

        private string _totalReassessmentAmount { get; set; }
        public string TotalReassessmentAmount
        {
            get
            {
                return _totalReassessmentAmount;
            }

            set
            {

                _totalReassessmentAmount = value;
                RaisePropertyChanged("TotalReassessmentAmount");
            }
        }

        private bool _isSearchButtonVisible = true;
        public bool IsSearchButtonVisible
        {
            get
            {
                return _isSearchButtonVisible;
            }

            set
            {

                _isSearchButtonVisible = value;
                RaisePropertyChanged("IsSearchButtonVisible");
            }
        }

        private bool _isCloseButtonVisible = false;
        public bool IsCloseButtonVisible
        {
            get
            {
                return _isCloseButtonVisible;
            }

            set
            {

                _isCloseButtonVisible = value;
                RaisePropertyChanged("IsCloseButtonVisible");
            }
        }

        public VATRefundListPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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


            VatRefundsListResultModel = new VatRefundsListResultModel();
            VATRefundsSubItemReturnsSet = new ObservableCollection<VatRefSubItemsSetResult>();
            VATRefundsSet = new ObservableCollection<VatRefHeaderSetResult>();
            VATRefundsSetCopy = new ObservableCollection<VatRefHeaderSetResult>();

            IsSearchButtonVisible = true;
            IsCloseButtonVisible = false;

            //PopulateVATRefundsList();

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

        public async void PopulateVATRefundsList()
        {
            //GAZTGetVAtRefundList
            await Task.Run(() =>
            {
                IsLoading = true;
            });

            Task GetRefundListTask = null;

            try
            {
                VatRefundsListResultModel = await WebServiceManager.GAZTGetVAtRefundList();
                VATRefundsSet = new ObservableCollection<VatRefHeaderSetResult>(VatRefundsListResultModel.VatRefHeaderSet.Results);
                VATRefundsSubItemReturnsSet = new ObservableCollection<VatRefSubItemsSetResult>(VatRefundsListResultModel.VatRefSubItemsSet.Results);

                double total = VATRefundsSet.Sum(item => Convert.ToDouble(item.ReassessAmt));
                TotalReassessmentAmount = string.Format("{0:0.00}",total);

                await Task.Run(() =>
                {
                   IsLoading = false;
                });
            }
            catch (GAZTErrorException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                   IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }
            catch (InternetException ex)
            {
                await Task.Run(() =>
                {
                   IsLoading = false;
                });

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

       

        public void SelectionChanged(VatRefHeaderSetResult vatRefHeaderSetResult)
        {
            try
            {
                VatRefHeaderSetResult[] sortedResultSet = VatRefundsListResultModel.VatRefHeaderSet.Results.Where(m => m.RefundFbnum == vatRefHeaderSetResult.RefundFbnum).ToArray();
                VatRefundsListResultModel.VatRefHeaderSet.Results = sortedResultSet;

                VatRefSubItemsSetResult[] sortedSubitemsResultSet = VatRefundsListResultModel.VatRefSubItemsSet.Results.Where(m => m.RefundFbnum == vatRefHeaderSetResult.RefundFbnum).ToArray();
                VatRefundsListResultModel.VatRefSubItemsSet.Results = sortedSubitemsResultSet;

                WiDtlSetResult[] sortedWidtlSet = VatRefundsListResultModel.WiDtlSet.Results.Where(m => m.Fbnum == vatRefHeaderSetResult.RefundFbnum).ToArray();
                VatRefundsListResultModel.WiDtlSet.Results = sortedWidtlSet;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task ReloadData()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });

                VatRefundsDisplayDataModel = await WebServiceManager.GAZTGetVATRefundDisplayBankIdTypeData("");

                VatRefundsIbanDataModel = await WebServiceManager.GAZTGetVATRefundGetIbanData("");
                IbanData = new ObservableCollection<VarRefundIbanDataModelMetadataResult>(VatRefundsIbanDataModel.IbanSet.Results);

                await Task.Run(() =>
                {
                   IsLoading = false;
                });
            }
            catch (InternetException ex)
            {
                await Task.Run(() =>
                {
                   IsLoading = false;
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
                   IsLoading = false;
                });

                string message = ex.Message;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(message, AppResources.Information);
                });
            }
        }

    }
}
