using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models.VATRefunds;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.VATRefunds
{
    public class VATRefundDetailsPageViewModel : ViewModelBase
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
        private string _TPAddress;
        public string TPAddress
        {
            get => _TPAddress;
            set
            {
                _TPAddress = value;
                RaisePropertyChanged(nameof(TPAddress));
            }
        }
        public void SetTaxpayerAddress()
        {
            string address = string.Empty;
            if (!string.IsNullOrEmpty(VatNewReqSummaryData.BuildingNo))
                address += VatNewReqSummaryData.BuildingNo;
            if (!string.IsNullOrEmpty(VatNewReqSummaryData.Quarter))
                address += ", " + VatNewReqSummaryData.Quarter;
            if (!string.IsNullOrEmpty(VatNewReqSummaryData.Street))
                address += ", " + VatNewReqSummaryData.Street;
            if (!string.IsNullOrEmpty(VatNewReqSummaryData.RegionDesc))
                address += ", " + VatNewReqSummaryData.RegionDesc;
            if (!string.IsNullOrEmpty(VatNewReqSummaryData.PostalCd))
                address += ", " + VatNewReqSummaryData.PostalCd;
            if (!string.IsNullOrEmpty(VatNewReqSummaryData.City))
                address += ", " + VatNewReqSummaryData.City;
            TPAddress = address;
        }
        private VatRefundDisplayDataModel _vatNewReqSummaryData { get; set; }
        public VatRefundDisplayDataModel VatNewReqSummaryData
        {
            get
            {
                return _vatNewReqSummaryData;
            }

            set
            {

                _vatNewReqSummaryData = value;
                RaisePropertyChanged("VatNewReqSummaryData");
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

        private bool _isNewReqSummary = false;
        public bool IsNewReqSummary
        {
            get
            {
                return _isNewReqSummary;
            }

            set
            {

                _isNewReqSummary = value;
                RaisePropertyChanged("IsNewReqSummary");
            }
        }

        private string _selectedIbanIdType { get; set; }
        public string SelectedIbanIdType
        {
            get
            {
                return _selectedIbanIdType;
            }

            set
            {

                _selectedIbanIdType = value;
                RaisePropertyChanged("SelectedIbanIdType");
            }
        }

        private ObservableCollection<IBANType> _iBANTypesList { get; set; }
        public ObservableCollection<IBANType> IBANTypesList
        {
            get
            {
                return _iBANTypesList;
            }
            set
            {
                _iBANTypesList = value;
                RaisePropertyChanged("IBANTypesList");
            }
        }

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
            VatNewReqSummaryData = new VatRefundDisplayDataModel();

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
            IsNewReqSummary = false;

            if (VatRefundsListResultModel == null)
            {
                VatRefundsListResultModel = new VatRefundsListResultModel();
            }

            VatRefundsListResultModel = vATRefundsModel;
            VATRefundsHeaderSet = VatRefundsListResultModel.VatRefHeaderSet?.Results[0];
            VATRefundsSubItemReturnsSet = new ObservableCollection<VatRefSubItemsSetResult>(VatRefundsListResultModel.VatRefSubItemsSet.Results);
            VATRefundsHeaderSet.RequestedAmt = VATRefundsHeaderSet?.RequestedAmt?.Replace("-", string.Empty);

            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });

                VatRefundsDisplayDataModel = await WebServiceManager.GAZTGetVATRefundDisplayBankIdTypeData(VatRefundsListResultModel.WiDtlSet.Results[0].Fbguid);
                SelectedIbanTypeFromList();

                IBANType selectedIdType = IBANTypesList.Where(m => m.key == VatRefundsDisplayDataModel.Idtype).FirstOrDefault();
                SelectedIbanIdType = selectedIdType.Text;

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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
        }

        public async void LoadSummaryData(VatRefundDisplayDataModel vATRefundsSaveDataModel)
        {
            IsNewReqSummary = true;

            if (VatRefundsListResultModel == null)
            {
                VatNewReqSummaryData = new VatRefundDisplayDataModel();
            }

            VatNewReqSummaryData = vATRefundsSaveDataModel;
            SetTaxpayerAddress();

            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });

                // VatRefundsDisplayDataModel = await WebServiceManager.GAZTGetVATRefundDisplayBankIdTypeData(VATRefundsHeaderSet.RefundFbnum);
                SelectedIbanTypeFromList();

                IBANType selectedIdType = IBANTypesList.Where(m => m.key == VatNewReqSummaryData.IdType).FirstOrDefault();
                SelectedIbanIdType = selectedIdType.Text;

                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (GAZTErrorException ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
        }

        //private void ConvertAllAmountsToCommaSeperated()
        //{
        //    VATRefundsHeaderSet.RequestedAmt = UtilityManager.GetCommaSeparatedAmount(VATRefundsHeaderSet.RequestedAmt);
        //    VATRefundsHeaderSet.ReassessAmt = UtilityManager.GetCommaSeparatedAmount(VATRefundsHeaderSet.ReassessAmt);
        //    VATRefundsHeaderSet.OffsetTot = UtilityManager.GetCommaSeparatedAmount(VATRefundsHeaderSet.OffsetTot);
        //    VATRefundsHeaderSet.NetCreditBal = UtilityManager.GetCommaSeparatedAmount(VATRefundsHeaderSet.OffsetTot);

        //}

        //private void RemoveCommaSeperatedValues()
        //{
        //}

        public void SelectedIbanTypeFromList()
        {
            IBANTypesList = new ObservableCollection<IBANType>();
            ObservableCollection<IBANType> IBANTypesDummyList = new ObservableCollection<IBANType>();
            IBANType iBANType = new IBANType();
            iBANType.key = "ZS0001";
            iBANType.Text = AppResources.ZIBANNationalID;
            IBANTypesDummyList.Add(iBANType);
            IBANType iBANType1 = new IBANType();
            iBANType1.key = "BUP002";
            iBANType1.Text = AppResources.ZIBANCommercialRegistrationID;
            IBANTypesDummyList.Add(iBANType1);
            IBANType iBANType2 = new IBANType();
            iBANType2.key = "ZS0005";
            iBANType2.Text = AppResources.ZIBANCompanyID;
            IBANTypesDummyList.Add(iBANType2);

            IBANTypesList = IBANTypesDummyList;
        }

        public async void ConfirmSummaryBtnClicked()
        {
            VatNewReqSummaryData.Operationx = "01";
            VatNewReqSummaryData.Gpartx = App.LoginDataRetrieved.TIN;
            VatNewReqSummaryData.Langx = UtilityManager.GetLanguageParameter();
            VatNewReqSummaryData.Rfamt = "-" + VatNewReqSummaryData.Rfamt;
            VatNewReqSummaryData.TcFg = "X";
            VatNewReqSummaryData.Confirmfg = "X";
            VatNewReqSummaryData.Agrfg = "X";
            VatNewReqSummaryData.Decflg = "X";

            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });

                VatNewReqSummaryData = await WebServiceManager.GAZTVATRefundSubmitRequest(VatNewReqSummaryData);

                _navigationService.NavigateTo(App.VATRefundsSuccessPageView, VatNewReqSummaryData);

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

        public async Task OnVoidBtnClicked()
        {
            VatNewReqSummaryData.Operationx = "04";
            VatNewReqSummaryData.Confirmfg = "";
            VatNewReqSummaryData.Gpartx = App.LoginDataRetrieved.TIN;
            VatNewReqSummaryData.Langx = UtilityManager.GetLanguageParameter();
            VatNewReqSummaryData.Rfamt = "-" + VatNewReqSummaryData.Rfamt;

            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });

                VatNewReqSummaryData = await WebServiceManager.GAZTVATRefundSubmitRequest(VatNewReqSummaryData);

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
