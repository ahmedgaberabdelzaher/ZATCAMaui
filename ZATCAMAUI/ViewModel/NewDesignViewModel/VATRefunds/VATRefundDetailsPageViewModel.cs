using System.Collections.ObjectModel;
using System.Windows.Input;


using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.VATRefunds;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.VATRefunds
{
    
    public class VATRefundDetailsPageViewModel : BaseViewModel
    {
        #region Commands


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
                if (_vatRefundsHeaderSet == value) return;
                _vatRefundsHeaderSet = value;
                OnPropertyChanged("VATRefundsHeaderSet");
            }
        }
        public bool _CBTermsAndConditionsChecked = false;
        public bool CBTermsAndConditionsChecked
        {
            get
            {
                return _CBTermsAndConditionsChecked;
            }

            set
            {
                _CBTermsAndConditionsChecked = value;
                if (_CBTermsAndConditionsChecked == true && _AcknowledgementChecked == true)
                {
                    IsConfirmSummaryEnabled = true;
                }
                else
                {
                    IsConfirmSummaryEnabled = false;
                }
                OnPropertyChanged("CBTermsAndConditionsChecked");
            }
        }
        public bool _AcknowledgementChecked = false;
        public bool AcknowledgementChecked
        {
            get
            {
                return _AcknowledgementChecked;
            }

            set
            {
                _AcknowledgementChecked = value;
                if (_AcknowledgementChecked == true && _CBTermsAndConditionsChecked == true)
                {
                    IsConfirmSummaryEnabled = true;
                }
                else
                {
                    IsConfirmSummaryEnabled = false;
                }
                OnPropertyChanged("AcknowledgementChecked");
            }
        }
        private bool _IsConfirmSummaryEnabled = false;
        public bool IsConfirmSummaryEnabled
        {
            get
            {
                return _IsConfirmSummaryEnabled;
            }
            set
            {
                if (_IsConfirmSummaryEnabled == value) return;

                _IsConfirmSummaryEnabled = value;
                if (_IsConfirmSummaryEnabled)
                {
                    ContinueButtonnBackroundColor = (Color)Application.Current.Resources["Secondary"];
                }
                else
                {
                    ContinueButtonnBackroundColor = (Color)Application.Current.Resources["ButtonGray"];
                }
                OnPropertyChanged("IsConfirmSummaryEnabled");
            }
        }
        private Color _continueButtonnBackroundColor = (Color)Application.Current.Resources["Secondary"];
        public Color ContinueButtonnBackroundColor
        {
            get
            {
                return _continueButtonnBackroundColor;
            }
            set
            {
                if (_continueButtonnBackroundColor == value) return;

                _continueButtonnBackroundColor = value;
                OnPropertyChanged("ContinueButtonnBackroundColor");
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
                if (_vatRefundsSubItemReturnsSet == value) return;

                _vatRefundsSubItemReturnsSet = value;
                OnPropertyChanged("VATRefundsSubItemReturnsSet");
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
                if (_vatRefundsListResultModel == value) return;

                _vatRefundsListResultModel = value;
                OnPropertyChanged("VatRefundsListResultModel");
            }
        }
        private string _TPAddress;
        public string TPAddress
        {
            get => _TPAddress;
            set
            {
                if (_TPAddress == value) return;

                _TPAddress = value;
                OnPropertyChanged(nameof(TPAddress));
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
                if (_vatNewReqSummaryData == value) return;

                _vatNewReqSummaryData = value;
                OnPropertyChanged("VatNewReqSummaryData");
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
                if (_vatRefundsDisplayDataModel == value) return;

                _vatRefundsDisplayDataModel = value;
                OnPropertyChanged("VatRefundsDisplayDataModel");
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
                if (_isNewReqSummary == value) return;

                _isNewReqSummary = value;
                OnPropertyChanged("IsNewReqSummary");
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
                if (_selectedIbanIdType == value) return;

                _selectedIbanIdType = value;
                OnPropertyChanged("SelectedIbanIdType");
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
                if (_iBANTypesList == value) return;

                _iBANTypesList = value;
                OnPropertyChanged("IBANTypesList");
            }
        }

       

        public VATRefundDetailsPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

            VATRefundsHeaderSet = new VatRefHeaderSetResult();
            VATRefundsSubItemReturnsSet = new ObservableCollection<VatRefSubItemsSetResult>();
            VatRefundsDisplayDataModel = new VatRefundDisplayDataModel();
            VatNewReqSummaryData = new VatRefundDisplayDataModel();
        }

        public async Task ReloadData(VatRefundsListResultModel vATRefundsModel)
        {
            IsNewReqSummary = false;

            if (VatRefundsListResultModel == null)
            {
                VatRefundsListResultModel = new VatRefundsListResultModel();
            }

            VatRefundsListResultModel = vATRefundsModel;
            VATRefundsHeaderSet = VatRefundsListResultModel.VatRefHeaderSet[0];
            VATRefundsSubItemReturnsSet = new ObservableCollection<VatRefSubItemsSetResult>(VatRefundsListResultModel.VatRefSubItemsSet);
            VATRefundsHeaderSet.RequestedAmt = VATRefundsHeaderSet?.RequestedAmt?.Replace("-", string.Empty);

            try
            {
                IsLoading = true;

                VatRefundsDisplayDataModel = await VATDeregistrationWebServiceManager.GAZTGetVATRefundDisplayBankIdTypeData(VatRefundsListResultModel.WiDtlSet[0].Fbguid);
                SelectedIbanTypeFromList();

                IBANType selectedIdType = IBANTypesList.Where(m => m.key == VatRefundsDisplayDataModel.Idtype).FirstOrDefault();
                SelectedIbanIdType = selectedIdType.Text;

                IsLoading = false;
            }
            catch (GAZTErrorException ex)
            {
                IsLoading = false;
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            }
            catch (InternetException ex)
            {
                IsLoading = false;

                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
            catch (Exception)
            {
                IsLoading = false;
            }
        }

        public async Task LoadSummaryData(VatRefundDisplayDataModel vATRefundsSaveDataModel)
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
                IsLoading = true;
                SelectedIbanTypeFromList();

                IBANType selectedIdType = IBANTypesList.Where(m => m.key == VatNewReqSummaryData.IdType).FirstOrDefault();
                SelectedIbanIdType = selectedIdType.Text;

                IsLoading = false;
            }
            catch (GAZTErrorException ex)
            {
                IsLoading = false;

                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            }
            catch (InternetException ex)
            {
                IsLoading = false;

                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
            catch (Exception)
            {
                IsLoading = false;
            }
        }
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

        public async Task ConfirmSummaryBtnClicked()
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
                IsLoading = true;
                VatNewReqSummaryData = await VATDeregistrationWebServiceManager.GAZTVATRefundSubmitRequest(VatNewReqSummaryData);

               await _navigationService.NavigateTo(App.VATRefundsSuccessPageView, VatNewReqSummaryData);

                IsLoading = false;
            }
            catch (InternetException)
            {
                IsLoading = false;

                await _dialogService.ShowMessage(AppResources.ZZInternetConnectionMessage, AppResources.Information);
            }
            catch (GAZTErrorException ex)
            {
                IsLoading = false;

                string message = ex.Message;

                await _dialogService.ShowMessage(message, AppResources.Information);
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

                VatNewReqSummaryData = await VATDeregistrationWebServiceManager.GAZTVATRefundSubmitRequest(VatNewReqSummaryData);

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

                MainThread.BeginInvokeOnMainThread(async () =>
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

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(message, AppResources.Information);
                });
            }

        }
    }
}
