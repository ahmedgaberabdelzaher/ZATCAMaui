using System;
using System.Collections.ObjectModel;
using System.Linq;
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
                App.DisplayProgressView();
            });

            Task GetRefundListTask = null;

            try
            {
                VatRefundsListResultModel = await WebServiceManager.GAZTGetVAtRefundList();
                VATRefundsSet = new ObservableCollection<VatRefHeaderSetResult>(VatRefundsListResultModel.VatRefHeaderSet.Results);
                VATRefundsSubItemReturnsSet = new ObservableCollection<VatRefSubItemsSetResult>(VatRefundsListResultModel.VatRefSubItemsSet.Results);

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

            //VATRefundsReturnsModel = new ObservableCollection<VATRefundsReturnsModel>();
            //VATRefundsReturnsModel.Add(new VATRefundsReturnsModel
            //{
            //    ReturnPeriod = "01 Jan 1995",
            //    CreditBalance = "200,000.00 SAR",
            //    ReassessedBalance = "100,000.00 SAR",
            //    Offset = "200,000.00 SAR",
            //    NetCreditBalance = "100,000.00 SAR",
            //    Status = "Transferred",
            //    LastStatusChange = "15 Feb 2019"
            //});
            //VATRefundsReturnsModel.Add(new VATRefundsReturnsModel
            //{
            //    ReturnPeriod = "01 Jan 1995",
            //    CreditBalance = "200,000.00 SAR",
            //    ReassessedBalance = "100,000.00 SAR",
            //    Offset = "200,000.00 SAR",
            //    NetCreditBalance = "100,000.00 SAR",
            //    Status = "Transferred",
            //    LastStatusChange = "15 Feb 2019"
            //});

            //VATRefundsModel = new ObservableCollection<VatRefHeaderSetResult>();
            //VATRefundsModel.Add(new VATRefundsModel
            //{
            //    Title = AppResources.ZZVAT,
            //    RefernceNumber = "1234567890",
            //    Status = AppResources.VATRefundsStatusRefunded,
            //    ReassesmentAmount = "1,000 SAR",
            //    TotalOffset = "500.00 SAR",
            //    NetCreditBalance = "2,000 SAR",
            //    RequestDate = "01 Jan 1995",
            //    BankDetails = new VATRefundsBankDetailsModel
            //    {
            //        IDType = AppResources.NationaID,
            //        IDNumber = "Q1234567",
            //        IBAN = "SA03 80000 12345",
            //        BankName =  "Al Rajhi Bank"
            //    },
            //    VATReturns = VATRefundsReturnsModel
            //});
            //VATRefundsModel.Add(new VATRefundsModel
            //{
            //    Title = AppResources.ZZVAT,
            //    RefernceNumber = "1234567890",
            //    Status = AppResources.VATRefundsStatusInProcess,
            //    ReassesmentAmount = "1,000 SAR",
            //    TotalOffset = "500.00 SAR",
            //    NetCreditBalance = "2,000 SAR",
            //    RequestDate = "01 Jan 1995",
            //    BankDetails = new VATRefundsBankDetailsModel
            //    {
            //        IDType = AppResources.NationaID,
            //        IDNumber = "Q1234567",
            //        IBAN = "SA03 80000 12345",
            //        BankName = "Al Rajhi Bank"
            //    },
            //    VATReturns = VATRefundsReturnsModel
            //});
            //VATRefundsModel.Add(new VATRefundsModel
            //{
            //    Title = AppResources.ZZVAT,
            //    RefernceNumber = "1234567890",
            //    Status = AppResources.VATRefundsStatusRefunded,
            //    ReassesmentAmount = "1,000 SAR",
            //    TotalOffset = "500.00 SAR",
            //    NetCreditBalance = "2,000 SAR",
            //    RequestDate = "01 Jan 1995",
            //    BankDetails = new VATRefundsBankDetailsModel
            //    {
            //        IDType = AppResources.NationaID,
            //        IDNumber = "Q1234567",
            //        IBAN = "SA03 80000 12345",
            //        BankName = "Al Rajhi Bank"
            //    },
            //    VATReturns = VATRefundsReturnsModel
            //});
        }

        public void SelectionChanged(VatRefHeaderSetResult vatRefHeaderSetResult)
        {
            try
            {
                VatRefHeaderSetResult[] sortedResultSet = VatRefundsListResultModel.VatRefHeaderSet.Results.Where(m => m.RefundFbnum == vatRefHeaderSetResult.RefundFbnum).ToArray();
                VatRefundsListResultModel.VatRefHeaderSet.Results = sortedResultSet;

                VatRefSubItemsSetResult[] sortedSubitemsResultSet = VatRefundsListResultModel.VatRefSubItemsSet.Results.Where(m => m.RefundFbnum == vatRefHeaderSetResult.RefundFbnum).ToArray();
                VatRefundsListResultModel.VatRefSubItemsSet.Results = sortedSubitemsResultSet;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
