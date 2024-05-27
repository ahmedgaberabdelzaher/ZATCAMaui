using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Views;
using RGPopup.Maui.Services;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.ContractRelease;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using static ZATCAMAUI.Models.ContractRelease.ContractReleaseFormResponse;
using static ZATCAMAUI.Models.ContractRelease.ContractReleaseFormResponse.ContractReLeaseApplicationFormModel;
using Attachment = ZATCAMAUI.Models.Attachment;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.ContractRelease
{

    public class ContractReleaseListViewModel : BaseViewModel
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        public ICommand GoBackClick { get; set; }
        public ICommand CloseClick { get; set; }
        public ICommand RequestContractReleaseBtnTapped { get; set; }
        public ICommand GoBackBtnTapped { get; set; }
        private ContractReleaseSummaryModel.ContractReleaseSummaryData _contractReleaseSummaryData;
        public ContractReleaseSummaryModel.ContractReleaseSummaryData ContractReLeaseSummaryData
        {
            get
            {
                return _contractReleaseSummaryData;
            }
            set
            {
                if (_contractReleaseSummaryData == value) return;
                _contractReleaseSummaryData = value;
                RaisePropertyChanged("ContractReLeaseSummaryData");
            }
        }
        public ObservableCollection<ContractReLeaseApplicationFormModel.ContractResult> _contractListViewData { get; set; }

        public ObservableCollection<ContractReLeaseApplicationFormModel.ContractResult> ContractListViewData
        {
            get { return _contractListViewData; }

            set
            {
                if (_contractListViewData == value)
                {
                    return;
                }

                _contractListViewData = value;
                RaisePropertyChanged("ContractListViewData");
            }
        }

        private bool _summaryVisible = false;

        public bool SummaryVisible
        {
            get { return _summaryVisible; }
            set
            {
                if (_summaryVisible == value) return;

                _summaryVisible = value;
                RaisePropertyChanged("SummaryVisible");
            }
        }

        public string _contractTotalAmount = "";

        public string ContractTotalAmount
        {
            get { return _contractTotalAmount; }
            set
            {
                if (_contractTotalAmount == value) return;

                _contractTotalAmount = value;
                RaisePropertyChanged("ContractTotalAmount");
            }
        }

        public string _amountToRelease = "";

        public string AmountToRelease
        {
            get { return _amountToRelease; }
            set
            {
                if (_amountToRelease == value) return;

                _amountToRelease = value;
                RaisePropertyChanged("AmountToRelease");
            }
        }

       

        private bool _isContractListsVisible = false;

        public bool IsContractListsVisible
        {
            get { return _isContractListsVisible; }
            set
            {
                if (_isContractListsVisible == value) return;

                _isContractListsVisible = value;
                RaisePropertyChanged("IsContractListsVisible");
            }
        }

        private bool _isBackButtonVisible = false;

        public bool IsBackButtonVisible
        {
            get { return _isBackButtonVisible; }
            set
            {
                if (_isBackButtonVisible == value) return;

                _isBackButtonVisible = value;
                RaisePropertyChanged("IsBackButtonVisible");
            }
        }

        private ContractReLeaseApplicationFormModel _contractReLeaseApplicationFormModel;
        public ContractReLeaseApplicationFormModel cRApplicationFormData
        {
            get
            {
                return _contractReLeaseApplicationFormModel;
            }
            set
            {
                if (_contractReLeaseApplicationFormModel == value) return;

                _contractReLeaseApplicationFormModel = value;
                RaisePropertyChanged("cRApplicationFormData");
            }
        }


        private ContractReLeaseApplicationFormModel.ListSet _contractReLeaseListSet;
        public ContractReLeaseApplicationFormModel.ListSet ContractReLeaseListSet
        {
            get
            {
                return _contractReLeaseListSet;
            }
            set
            {
                if (_contractReLeaseListSet == value) return;

                _contractReLeaseListSet = value;
                RaisePropertyChanged("ContractReLeaseListSet");
            }
        }

        private string _totalDues = "";

        public string TotalDues
        {
            get { return _totalDues; }
            set
            {
                if (_totalDues == value) return;

                _totalDues = value;
                RaisePropertyChanged("TotalDues");
            }
        }

        private string _remarks = "";

        public string Remarks
        {
            get { return _remarks; }
            set
            {
                if (_remarks == value) return;

                _remarks = value;
                RaisePropertyChanged("Remarks");
            }
        }

        private string _detailDescription = "";

        public string DetailDescription
        {
            get { return _detailDescription; }
            set
            {
                if (_detailDescription == value) return;

                _detailDescription = value;
                RaisePropertyChanged("DetailDescription");
            }
        }

        private string _contactPersonName = "";

        public string ContactPersonName
        {
            get { return _contactPersonName; }
            set
            {
                if (_contactPersonName == value) return;

                _contactPersonName = value;
                RaisePropertyChanged("ContactPersonName");
            }
        }

        private string _designation = "";

        public string Designation
        {
            get { return _designation; }
            set
            {
                if (_designation == value) return;

                _designation = value;
                RaisePropertyChanged("Designation");
            }
        }

        private string _pickedContract = "";

        public string PickedContract
        {
            get { return _pickedContract; }
            set
            {
                if (_pickedContract == value) return;

                _pickedContract = value;
                RaisePropertyChanged("PickedContract");
            }
        }

        private string _contractName = "";

        public string ContractName
        {
            get { return _contractName; }
            set
            {
                if (_contractName == value) return;

                _contractName = value;
                RaisePropertyChanged("ContractName");
            }
        }

        private string _contractNumber = "";

        public string ContractNumber
        {
            get { return _contractNumber; }
            set
            {
                if (_contractNumber == value) return;

                _contractNumber = value;
                RaisePropertyChanged("ContractNumber");
            }
        }

        private string _contractPeriod = "";

        public string ContractPeriod
        {
            get { return _contractPeriod; }
            set
            {
                if (_contractPeriod == value) return;

                _contractPeriod = value;
                RaisePropertyChanged("ContractPeriod");
            }
        }

        private string _fromDate = "";

        public string FromDate
        {
            get { return _fromDate; }
            set
            {
                if (_fromDate == value) return;

                _fromDate = value;
                RaisePropertyChanged("FromDate");
            }
        }

        private string _toDate = "";

        public string ToDate
        {
            get { return _toDate; }
            set
            {
                if (_toDate == value) return;

                _toDate = value;
                RaisePropertyChanged("ToDate");
            }
        }

        private string _zakatDues = "";
        public string ZakatDues
        {
            get { return _zakatDues; }
            set
            {
                if (_zakatDues == value) return;

                _zakatDues = value;
                RaisePropertyChanged("ZakatDues");
            }
        }
        private string _taxDues = "";
        public string TaxDues
        {
            get { return _taxDues; }
            set
            {
                if (_taxDues == value) return;

                _taxDues = value;
                RaisePropertyChanged("TaxDues");
            }
        }

        private GenericPickerModel _pickerModel { get; set; }
        public GenericPickerModel PickerModel
        {
            get { return _pickerModel; }
            set
            {
                if (_pickerModel == value) return;

                _pickerModel = value;
                RaisePropertyChanged("PickerModel");
            }
        }

        public ObservableCollection<Attachment> contractCopyAttachmentsListViewData { get; set; }

        public ObservableCollection<Attachment> ContractCopyAttachmentsListViewData
        {
            get { return contractCopyAttachmentsListViewData; }

            set
            {
                if (contractCopyAttachmentsListViewData == value)
                {
                    return;
                }

                contractCopyAttachmentsListViewData = value;
                RaisePropertyChanged("ContractCopyAttachmentsListViewData");
            }
        }

        public ObservableCollection<Attachment> invoicesAttachmentsListViewData { get; set; }

        public ObservableCollection<Attachment> InvoiceAttachmentsListViewData
        {
            get { return invoicesAttachmentsListViewData; }

            set
            {
                if (invoicesAttachmentsListViewData == value)
                {
                    return;
                }

                invoicesAttachmentsListViewData = value;
                RaisePropertyChanged("InvoiceAttachmentsListViewData");
            }
        }

        public ContractResult _SelectedTypeFilter = new ContractResult();

        public ContractResult SelectedTypeFilter

        {

            get

            {

                return _SelectedTypeFilter;

            }

            set

            {

                _SelectedTypeFilter = value;

                if (_SelectedTypeFilter != null)

                {

                    // FilterLabelTxt = _SelectedTypeFilter.StatText;

                    //FilterOnBasisOfTaxType();

                }

                RaisePropertyChanged("SelectedTaxTypeForFilter");

            }

        }

        public string _filterLabelTxt;

        public string FilterLabelTxt

        {

            get

            {

                return _filterLabelTxt;

            }

            set

            {

                if (_filterLabelTxt == value) return;

                _filterLabelTxt = value;

                RaisePropertyChanged("FilterLabelTxt");

            }

        }
        public ContractReleaseListViewModel(INavigationService navigationService, IDialogService dialogService):base(navigationService,dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            CloseClick = new Command(() =>
            {
                _navigationService.GoBack();
            });
            GoBackBtnTapped = new Command(() =>
            {
                _navigationService.GoBack();
            });
            GoBackClick = new Command(() =>
            {
                EnableContractListView();
            });
            RequestContractReleaseBtnTapped = new Command(() =>
            {
                _navigationService.NavigateTo(App.ContractReleasePageView);
            });
        }

        private void EnableContractListView()
        {
            IsBackButtonVisible = false;
            IsContractListsVisible = true;
            SummaryVisible = false;
        }

        private void EnableSummaryView()
        {
            IsBackButtonVisible = true;
            IsContractListsVisible = false;
            SummaryVisible = true;
        }
        private Dictionary<string, string> ContractTypeIdDictionary = new Dictionary<string, string>
        {
            {"1",AppResources.CRSupplyforAramco },
            { "2",AppResources.CRSupplyandmaintenance},
            { "3",AppResources.CRsupplymaintenanceandoperating},
            { "4",AppResources.CRDisassembleinstallationandoperate},
            { "5",AppResources.CRsupplyinstallationandoperate},
            { "6",AppResources.CRDisassembleinstallationandtransport},
            { "7",AppResources.CRCleanlinessandmaintenance},
            { "8",AppResources.CRSupplyinstallationanddeliver},
            { "9",AppResources.CRmaintenanceandoperate},
            { "10",AppResources.CRtransport},
            { "11",AppResources.CRsupplyandwatertransport},
            { "12",AppResources.CRconstruction},
            { "13",AppResources.CRmaintenance},
            { "14",AppResources.CRoperate},
            { "15",AppResources.CRDesignandconstruction},
            { "16",AppResources.CRsupplyanddesign},
            { "17",AppResources.CRsupplyandoprate},
            { "18",AppResources.CRsupplyandinstallation},
            { "19",AppResources.CRdesignsupplyandinstallation},
            { "20",AppResources.CRdesignsupplyandopratemaintenance},
            { "21",AppResources.CRequipmentrental},
            { "22",AppResources.CRmaintenancecleanlinessandoprate},
            { "23",AppResources.CRcleanliness},
            { "24",AppResources.CRcatering},
            { "25",AppResources.CRSecurityguards},
            { "26",AppResources.CRRoadsmaintenance},
            { "27",AppResources.CRLaborrecruiting},
            { "28",AppResources.CRContractingRoadsandTransport},
            { "29",AppResources.CRSupply},
            { "30",AppResources.CRSupplyanddeliverytowarehouses},
            { "31",AppResources.CRSupplyanddeliveryport},
            { "32",AppResources.CROperationofservicesattheport},
            { "34",AppResources.CRConsultations},
            { "35",AppResources.CRPrivateConsultante},
            { "36",AppResources.CRStudiesandConsulting},
            { "33",AppResources.CROther},
        };

        private void BindSummaryData()
        {
            FromDate = ContractReLeaseSummaryData.ContractDate;
            ToDate = ContractReLeaseSummaryData.ContractEnddate;
            ContractTotalAmount = UtilityManager.GetCommaSeparatedAmount(ContractReLeaseSummaryData.TotalAmountofContract);
            AmountToRelease = UtilityManager.GetCommaSeparatedAmount(ContractReLeaseSummaryData.AmountRequiredtoRelease);
            PickedContract = ContractTypeIdDictionary[ContractReLeaseSummaryData.Type];
            ContractName = ContractReLeaseSummaryData.ContractingName;
            ContractNumber = ContractReLeaseSummaryData.Number;
            Remarks = ContractReLeaseSummaryData.Remark;
            DetailDescription = ContractReLeaseSummaryData.DetaiiledDesc;
            ContractPeriod = ContractReLeaseSummaryData.ContractDate + " - " + ContractReLeaseSummaryData.ContractEnddate;

            ZakatDues = UtilityManager.GetCommaSeparatedAmount(ContractReLeaseSummaryData.TheValueofZakatdues);
            TaxDues = UtilityManager.GetCommaSeparatedAmount(ContractReLeaseSummaryData.TheValueofTaxdues);
            TotalDues = UtilityManager.GetCommaSeparatedAmount(ContractReLeaseSummaryData.TotalDues);
            var ccAttachments = new ObservableCollection<Attachment>();
            var invoiceAttachments = new ObservableCollection<Attachment>();

            foreach (var attach in ContractReLeaseSummaryData.AttDetSet.results)
            {
                if (attach.Dotyp == "N11A")
                {
                    invoiceAttachments.Add(attach);
                }
                else
                {
                    ccAttachments.Add(attach);
                }


            }

            ContractCopyAttachmentsListViewData = ccAttachments;
            InvoiceAttachmentsListViewData = invoiceAttachments;

        }

        private void PopulateContractList()
        {
            var contracts = new ObservableCollection<ContractReLeaseApplicationFormModel.ContractResult>();

            foreach (var contract in ContractReLeaseListSet.results)
            {
                contracts.Add(contract);
            }

            ContractListViewData = contracts;
        }

        public void ResetData()
        {
            ContractListViewData = null;
            EnableContractListView();
        }

        public void UpdateDataToUI()
        {
            PopulateContractList();
            PopulateFilter();
            // ContractReLeaseListSet.results[0]
        }

        #region OnPageLoad

        public void PopulateFilter()
        {
            var list = new List<string>();
            try
            {
                list.Add("ALL");
                var distinctList = ContractReLeaseListSet.results.Select(i => i.StatText).Distinct().ToList();
                foreach (var item in distinctList)
                {
                    list.Add(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = list;
            genericPickerModel.PickerTitle = "";
            genericPickerModel.PickerId = "ConractRelease";
            genericPickerModel.SelectedValue = _SelectedTypeFilter.StatText;
            PickerModel = genericPickerModel;
        }
        public async Task OnPageLoad()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    IsLoading = true;
                    cRApplicationFormData = null;
                    ContractReLeaseListSet = null;
                    FilterLabelTxt = "ALL";

                    try
                    {



                        cRApplicationFormData = await ContractReleaseWebServiceManager.GetContractReleaseList();

                        if (cRApplicationFormData != null && cRApplicationFormData.d != null)
                        {
                            ContractReLeaseListSet = cRApplicationFormData.d.ListSet;
                            UpdateDataToUI();
                        }
                        else
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                _navigationService.GoBack();
                            });
                        }
                        IsLoading = false;
                    }
                    catch (GAZTVATRegistrationInProcessException ex)
                    {
                        throw ex;
                    }
                    catch (InternetException ex)
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            IsLoading = false;
                            _navigationService.GoBack();
                        });

                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });

            }
            catch (Exception)
            {


                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public async Task GetContractReleaseSummaryData(ContractReLeaseApplicationFormModel.ContractResult selectedItem)
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {

                    IsLoading = true;

                    try
                    {
                        ContractReLeaseSummaryData = new ContractReleaseSummaryModel.ContractReleaseSummaryData();

                        var resultData = await ContractReleaseWebServiceManager.GAZTGetContractReleaseSummaryData("", selectedItem.Fbnum);

                        ContractReLeaseSummaryData.RequestNumber = resultData.d.Fbnumz;
                        ContractReLeaseSummaryData.TaxpayerName = resultData.d.ATpNm;
                        ContractReLeaseSummaryData.ContractingName = resultData.d.AContNm;
                        ContractReLeaseSummaryData.Number = resultData.d.AContNo;
                        ContractReLeaseSummaryData.Type = resultData.d.AType;
                        ContractReLeaseSummaryData.ContractDate = resultData.d.AContDt1;
                        ContractReLeaseSummaryData.ContractEnddate = resultData.d.AContEndDtCh;
                        ContractReLeaseSummaryData.TotalAmountofContract = resultData.d.ATotalAmt;
                        ContractReLeaseSummaryData.AmountRequiredtoRelease = resultData.d.AReqAmt;
                        ContractReLeaseSummaryData.ContractprofitEstimatedRate = resultData.d.AContProfitPer;
                        ContractReLeaseSummaryData.ProfitEstimatedforContract = resultData.d.AContProfit;
                        ContractReLeaseSummaryData.EstimatedProfitforZakat = resultData.d.AZakatProfit;
                        ContractReLeaseSummaryData.EstimatedProfitforTax = resultData.d.ATaxProfitPer;
                        ContractReLeaseSummaryData.TheValueofZakatdues = resultData.d.ADueZakat;
                        ContractReLeaseSummaryData.TheValueofTaxdues = resultData.d.ADueTax;
                        ContractReLeaseSummaryData.TotalDues = resultData.d.ADueTot;
                        ContractReLeaseSummaryData.AttDetSet = resultData.d.AttDetSet;
                        ContractReLeaseSummaryData.znotesSet = resultData.d.znotesSet;
                        ContractReLeaseSummaryData.Remark = resultData.d.ARemark;

                        if (resultData.d.znotesSet.results.Length != 0)
                        {

                            ContractReLeaseSummaryData.DetaiiledDesc = resultData.d.znotesSet.results[0].Tdline;
                        }

                        if (ContractReLeaseSummaryData != null)
                        {
                            EnableSummaryView();
                            BindSummaryData();
                        }
                        else
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                _navigationService.GoBack();
                            });
                        }



                        IsLoading = false;
                    }
                    catch (GAZTVATRegistrationInProcessException ex)
                    {
                        throw ex;
                    }
                    catch (InternetException ex)
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            IsLoading = false;
                            _navigationService.GoBack();
                        });

                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });

            }
            catch (Exception)
            {


                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public async void showPickerDialog()
        {
            try
            {
                if (PickerModel != null)
                    await PopupNavigation.Instance.PushAsync(new PickerPageView(PickerModel));
            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public void updatePicker(Models.GenericPickerModel arg)
        {
            if (!string.IsNullOrEmpty(arg.SelectedValue))
            {
                FilterLabelTxt = arg.SelectedValue.ToUpper();
                if (arg.SelectedValue.Equals("ALL"))
                {
                    PopulateContractList();
                }
                else
                {
                    var selectedFilter = new ObservableCollection<ContractResult>(ContractReLeaseListSet.results.Where(temp => temp.StatText.ToUpper().Equals(arg.SelectedValue.ToUpper())));
                    ContractListViewData = selectedFilter;
                }
            }
        }

        #endregion

    }
}
