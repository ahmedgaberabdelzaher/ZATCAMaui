using System.Collections.ObjectModel;
using System.Windows.Input;

using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models.VATRefunds;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.VATRefunds
{

    public class VATRefundListPageViewModel : BaseViewModel
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
                if (_vatRefundsModel == value) return;
                _vatRefundsModel = value;
                OnPropertyChanged("VATRefundsModel");
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

        private ObservableCollection<VatRefHeaderSetResult> _vatRefundsSet { get; set; }
        public ObservableCollection<VatRefHeaderSetResult> VATRefundsSet
        {
            get
            {
                return _vatRefundsSet;
            }
            set
            {
                if (_vatRefundsSet == value) return;

                _vatRefundsSet = value;
                OnPropertyChanged("VATRefundsSet");
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
                if (_vatRefundsSetCopy == value) return;

                _vatRefundsSetCopy = value;
                OnPropertyChanged("VATRefundsSetCopy");
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
                if (_vatRefundsSetCopy == value) return;

                _vatRefundsSearchSet = value;
                OnPropertyChanged("VATRefundsSearchSet");
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
                if (_vatRefundsDisplayDataModel == value) return;

                _vatRefundsDisplayDataModel = value;
                OnPropertyChanged("VatRefundsDisplayDataModel");
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
                if (_ibanData == value) return;

                _ibanData = value;
                OnPropertyChanged("IbanData");
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
                if (_vatRefundsIbanDataModel == value) return;

                _vatRefundsIbanDataModel = value;
                OnPropertyChanged("VatRefundsIbanDataModel");
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
                if (_totalReassessmentAmount == value) return;

                _totalReassessmentAmount = value;
                OnPropertyChanged("TotalReassessmentAmount");
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
                if (_isSearchButtonVisible == value) return;

                _isSearchButtonVisible = value;
                OnPropertyChanged("IsSearchButtonVisible");
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
                if (_isCloseButtonVisible == value) return;

                _isCloseButtonVisible = value;
                OnPropertyChanged("IsCloseButtonVisible");
            }
        }

        public VATRefundListPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

            GoBackBtnTapped = new Command(() =>
            {
                _navigationService.GoBack();
            });


            VatRefundsListResultModel = new VatRefundsListResultModel();
            VATRefundsSubItemReturnsSet = new ObservableCollection<VatRefSubItemsSetResult>();
            VATRefundsSet = new ObservableCollection<VatRefHeaderSetResult>();
            VATRefundsSetCopy = new ObservableCollection<VatRefHeaderSetResult>();

            IsSearchButtonVisible = true;
            IsCloseButtonVisible = false;
        }

        public async void PopulateVATRefundsList()
        {
            //GAZTGetVAtRefundList
            await Task.Run(() =>
            {
                IsLoading = true;
            });


            try
            {
                VatRefundsListResultModel = await VATDeregistrationWebServiceManager.GAZTGetVAtRefundList();
                VATRefundsSet = new ObservableCollection<VatRefHeaderSetResult>(VatRefundsListResultModel.VatRefHeaderSet);
                VATRefundsSubItemReturnsSet = new ObservableCollection<VatRefSubItemsSetResult>(VatRefundsListResultModel.VatRefSubItemsSet);

                double total = VATRefundsSet.Sum(item => Convert.ToDouble(item.ReassessAmt));
                TotalReassessmentAmount = string.Format("{0:0.00}", total);

                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (GAZTErrorException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
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

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                    Console.WriteLine(ex.Message);
                });
            }
        }



        public void SelectionChanged(VatRefHeaderSetResult vatRefHeaderSetResult)
        {
            try
            {
                VatRefHeaderSetResult[] sortedResultSet = VatRefundsListResultModel.VatRefHeaderSet.Where(m => m.RefundFbnum == vatRefHeaderSetResult.RefundFbnum).ToArray();
                VatRefundsListResultModel.VatRefHeaderSet = sortedResultSet;

                VatRefSubItemsSetResult[] sortedSubitemsResultSet = VatRefundsListResultModel.VatRefSubItemsSet.Where(m => m.RefundFbnum == vatRefHeaderSetResult.RefundFbnum).ToArray();
                VatRefundsListResultModel.VatRefSubItemsSet = sortedSubitemsResultSet;

                WiDtlSetResult[] sortedWidtlSet = VatRefundsListResultModel.WiDtlSet.Where(m => m.Fbnum == vatRefHeaderSetResult.RefundFbnum).ToArray();
                VatRefundsListResultModel.WiDtlSet = sortedWidtlSet;
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

                VatRefundsDisplayDataModel = await VATDeregistrationWebServiceManager.GAZTGetVATRefundDisplayBankIdTypeData("");

                VatRefundsIbanDataModel = await VATDeregistrationWebServiceManager.GAZTGetVATRefundGetIbanData("");
                IbanData = new ObservableCollection<VarRefundIbanDataModelMetadataResult>(VatRefundsIbanDataModel.IbanSet);

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
            catch (Exception ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                    Console.WriteLine(ex.Message);
                });
            }
        }

    }
}
