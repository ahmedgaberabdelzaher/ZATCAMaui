using System.Collections.ObjectModel;

using System.Windows.Input;
using Mopups.Services;
using Syncfusion.Maui.ListView;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.ContractReleas;
using ZATCAMAUI.Models.ContractRelease;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using static ZATCAMAUI.Models.ContractRelease.ContractReLeaseApplicationFormModel;
using Attachment = ZATCAMAUI.Models.Attachment;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.ContractRelease
{

    public class ContractReleaseListViewModel : BaseViewModel
    {

        public ICommand GoBackClick { get; set; }
        public ICommand RequestContractReleaseBtnTapped { get; set; }

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
                OnPropertyChanged("ContractReLeaseSummaryData");
            }
        }
        public ObservableCollection<ContractResult> _contractListViewData { get; set; }

        public ObservableCollection<ContractResult> ContractListViewData
        {
            get { return _contractListViewData; }

            set
            {
                if (_contractListViewData == value)
                {
                    return;
                }

                _contractListViewData = value;
                OnPropertyChanged("ContractListViewData");
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
                OnPropertyChanged("SummaryVisible");
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
                OnPropertyChanged("ContractTotalAmount");
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
                OnPropertyChanged("AmountToRelease");
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
                OnPropertyChanged("IsContractListsVisible");
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
                OnPropertyChanged("IsBackButtonVisible");
            }
        }

        ContractReLeaseApplicationFormModel _contractReLeaseApplicationFormModel;
        public ContractReLeaseApplicationFormModel cRApplicationFormData { get { return _contractReLeaseApplicationFormModel; } set { _contractReLeaseApplicationFormModel = value; OnPropertyChanged(); } }

       

        private List<ContractResult> _contractReLeaseListSet;
        public List<ContractResult> ContractReLeaseListSet
        {
            get
            {
                return _contractReLeaseListSet;
            }
            set
            {
                if (_contractReLeaseListSet == value) return;

                _contractReLeaseListSet = value;
                OnPropertyChanged("ContractReLeaseListSet");
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
                OnPropertyChanged("TotalDues");
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
                OnPropertyChanged("Remarks");
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
                OnPropertyChanged("DetailDescription");
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
                OnPropertyChanged("ContactPersonName");
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
                OnPropertyChanged("Designation");
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
                OnPropertyChanged("PickedContract");
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
                OnPropertyChanged("ContractName");
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
                OnPropertyChanged("ContractNumber");
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
                OnPropertyChanged("ContractPeriod");
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
                OnPropertyChanged("FromDate");
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
                OnPropertyChanged("ToDate");
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
                OnPropertyChanged("ZakatDues");
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
                OnPropertyChanged("TaxDues");
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
                OnPropertyChanged("PickerModel");
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
                OnPropertyChanged("ContractCopyAttachmentsListViewData");
            }
        }

        Attachment contractCopyAttachmentSelectedItem;
        public Attachment ContractCopyAttachmentSelectedItem { get { return contractCopyAttachmentSelectedItem; } set { contractCopyAttachmentSelectedItem = value; OnPropertyChanged(); } }


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
                OnPropertyChanged("InvoiceAttachmentsListViewData");
            }
        }

       
        public ContractResult _SelectedTypeFilter = new ContractResult();

      
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

                OnPropertyChanged("FilterLabelTxt");

            }

        }
        public ContractReleaseListViewModel(INavigationService navigationService, IDialogService dialogService):base(navigationService,dialogService)
        {
            GoBackClick = new Command(() =>
            {
                EnableContractListView();
            });
            RequestContractReleaseBtnTapped = new Command( async () =>
            {
               await _navigationService.NavigateTo(App.ContractReleasePageView);
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

            foreach (var attach in ContractReLeaseSummaryData.AttDetSet)
            {
                if(attach.Dotyp == "N11A") {
                    invoiceAttachments.Add(attach);
                }else  {
                    ccAttachments.Add(attach);
                }


            }

            ContractCopyAttachmentsListViewData = ccAttachments;
            InvoiceAttachmentsListViewData = invoiceAttachments;

        }

        private void PopulateContractList()
        {
            var contracts = new ObservableCollection<ContractReLeaseApplicationFormModel.ContractResult>();

            foreach (var contract in ContractReLeaseListSet)
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
        }

        public async static Task Email(string doguid, Attachment attachment)
        {

            try
            {

                string attachmentURL = attachment.DocUrl;// "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_IT_CORR_MOOB_SRV/corr_dataSet(Cokey='" + doguid + "',Cotyp='VTA0')/$value?saml2=disabled";

                MemoryStream pdfStream = new MemoryStream();


                HttpClient client = new HttpClient(App.httpClientHandler);
                var uri = new Uri(attachmentURL);
                HttpResponseMessage _fileDownloadResponse = await client.GetAsync(uri);

                var fileName = Guid.NewGuid().ToString();

                _fileDownloadResponse.EnsureSuccessStatusCode();
                await _fileDownloadResponse.Content.CopyToAsync(pdfStream);

                var message = new EmailMessage
                {
                    Subject = "Attached Form :",
                };
                var fn = attachment.Filename;
                var file = Path.Combine(FileSystem.CacheDirectory, fn);
                File.WriteAllBytes(file, pdfStream.ToArray());

                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = "",
                    File = new ShareFile(file)
                });



            }
            catch (Exception)
            {


            }
        }

        #region OnPageLoad
        public async Task OnPageLoad()
        {
            try
            {
                IsLoading = true;
                cRApplicationFormData = null;
                ContractReLeaseListSet = null;


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
                        await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        _navigationService.GoBack();
                    }
                    IsLoading = false;
                }
                catch (InternetException ex)
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    IsLoading = false;
                    _navigationService.GoBack();
                }
                IsLoading = false;

            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                IsLoading = false;
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
            catch (Exception)
            {
                IsLoading = false;
                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                _navigationService.GoBack();
            }
        }

        public async Task GetContractReleaseSummaryData(ContractResult selectedItem)
        {
            try
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

                    if (resultData.d.znotesSet.Length != 0)
                    {

                        ContractReLeaseSummaryData.DetaiiledDesc = resultData.d.znotesSet[0].Tdline;
                    }

                    if (ContractReLeaseSummaryData != null)
                    {
                        EnableSummaryView();
                        BindSummaryData();
                    }
                    else
                    {
                        await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        _navigationService.GoBack();
                    }



                    IsLoading = false;
                }
                catch (InternetException ex)
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    IsLoading = false;
                    _navigationService.GoBack();

                }
                IsLoading = false;
            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                IsLoading = false;
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();

            }
            catch (Exception)
            {
                IsLoading = false;
                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                _navigationService.GoBack();
            }
        }

        public async Task ShowPickerDialog()
        {
            try
            {
                if (PickerModel != null)
                    await MopupService.Instance.PushAsync(new PickerPageView(PickerModel));
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
        }

        public void PopulateFilter()
        {
            var list = new List<string>();
            try
            {
                list.Add("ALL");
                var distinctList = ContractReLeaseListSet.Select(i => i.StatText).Distinct().ToList();
                foreach (var item in distinctList)
                {
                    list.Add(item);
                }
            }
            catch (Exception)
            {

                
                
                
            }

            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = list;
            genericPickerModel.PickerTitle = "";
            genericPickerModel.PickerId = "ConractRelease";
            PickerModel = genericPickerModel;
        }
        public void updatePicker(Models.GenericPickerModel arg)
        {
            if (arg.SelectedValue.Equals("ALL"))
            {
                PopulateContractList();
            }
            else
            {
                var selectedFilter = new ObservableCollection<ContractResult>(ContractReLeaseListSet.Where(temp => temp.StatText.Equals(PickerModel.SelectedValue)));
                ContractListViewData = selectedFilter;
            }
        }

        #endregion

        public ICommand OnAppearingContractReleaseListCommand
        {
            get
            {
                return new Command(async _ =>
                {
                    try
                    {
                        ResetData();
                        await OnPageLoad();

                        MessagingCenter.Subscribe<PickerPageView, Models.GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) =>
                        {
                            updatePicker(arg);
                        });
                    }
                    catch (Exception)
                    {
                    }

                });
            }
        }

        public ICommand ContractsListItemTappedCommand
        {
            get
            {
                return new Command<object>(async obj =>
                {
                    try
                    {
                        var item = (obj as Syncfusion.Maui.ListView.ItemTappedEventArgs).DataItem as ContractResult;
                        await GetContractReleaseSummaryData(item);
                    }
                    catch (Exception)
                    {
                    }

                });
            }
        }

        public ICommand DownloadAcknowledgementForm
        {
            get
            {
                return new Command(async _=>
                {
                    try
                    {
                        if (ContractReLeaseSummaryData.RequestNumber != null)
                        {
                            IsLoading = true;
                            string downloadurl = ZATCAConstants.CRDownloadCoverFormFile + ContractReLeaseSummaryData.RequestNumber;
                            await _navigationService.NavigateTo(App.PdfView, downloadurl);

                            IsLoading = false;
                        }
                    }
                    catch (Exception)
                    {
                    }

                });
            }
        }

        public ICommand DownloadAcknowledgement
        {
            get
            {
                return new Command(async _=>
                {
                    try
                    {
                        if (ContractReLeaseSummaryData.RequestNumber != null)
                        {
                            IsLoading = true;
                            string downloadurl = ZATCAConstants.CRDownloadAcknowledementFile + ContractReLeaseSummaryData.RequestNumber;
                            await _navigationService.NavigateTo(App.PdfView, downloadurl);
                            IsLoading = false;
                        }
                    }
                    catch (Exception)
                    {
                    }

                });
            }
        }


        public ICommand SummaryAttachmentsItemCommand
        {
            get
            {
                return new Command(async _=>
                {
                    try
                    {
                        IsLoading = true;

                        string[] Extentionarray = ContractCopyAttachmentSelectedItem.Filename.Split('.');
                        string Extention = Extentionarray.Last();

                        if (Extention.Equals("PDF") || Extention.Equals("pdf"))
                        {
                            if (ContractCopyAttachmentSelectedItem.DocUrl != null)
                            {
                                await _navigationService.NavigateTo(App.PdfView, ContractCopyAttachmentSelectedItem.DocUrl);
                            }
                        }
                        else
                        {
                            await Email(ContractCopyAttachmentSelectedItem.Doguid, ContractCopyAttachmentSelectedItem);
                        }
                        IsLoading = false;
                    }
                    catch (Exception)
                    {
                    }

                });
            }
        }
        public ICommand btn_ClickedBtnCommand
        {
            get
            {
                return new Command(async _ =>
                {
                    await ShowPickerDialog();
                });
            }
        }

    }
}
