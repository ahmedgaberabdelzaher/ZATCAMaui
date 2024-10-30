

using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.ZakatObjectionsModel;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatObjectionViewModel
{
    public class ZakatObjectionsListViewModel : BaseViewModel
    {

        #region Commands

        public ICommand OnAppearingZakatObjectionsListPageViewCommand { get; set; }
        public ICommand ObjectionItemCommand { get; set; }
        public ICommand ReqInstalmentBtnTapped { get; set; }
        public ICommand GoBackClick { get; set; }
        public ICommand Download_Acknowledgement { get; set; }
        public ICommand ZDownloadForm { get; set; }

        #endregion

        public ZakatObjectionsListViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {


            ReqInstalmentBtnTapped = new Command(ReqInstalmentBtnClicked);

            GoBackClick = new Command(() => { BackNavigations(); });

            Download_Acknowledgement = new Command(async () =>
            {
                IsLoading = true;
                if (objRefNumber != null)
                {
                    string downloadurl = ZATCAConstants.ZOdownloadAckLetter + objRefNumber;
                    await _navigationService.NavigateTo(App.PdfView, downloadurl);

                }
                IsLoading = false;
            });

            OnAppearingZakatObjectionsListPageViewCommand = new Command(async () =>
            {
                IsLoading = true;
                ResetData();
                await ZAKATObjectionList();
                IsLoading = false;
            });

            ObjectionItemCommand = new Command<object>(async (obj) =>
            {
                try
                {
                    IsLoading = true;
                    var item = (obj as Syncfusion.Maui.ListView.ItemTappedEventArgs).DataItem as ZakatObjectionListModel.Result;
                    Preferences.Set("ZakatObjectionSelectedValue", item.Fbnum);
                    Preferences.Set("ZakatObjectionSelectedType", item.Fbtyp);

                    if (item.Fbtyp == "TP09")
                    {
                        await GetWithdrawReviewReason(item.Fbnum);
                    }
                    else if (item.Fbtyp == "TP10")
                    {
                        await GetWithdrawReviewReasonTP10(item.Fbnum);
                    }
                    else if (item.Fbtyp == "ZNOB" && (item.StatText == "Additional Info Requested" || item.StatText == "طلب معلومات اضافية"))
                    {
                        await showRejectPopup();
                    }
                    else
                    {
                        ReqInstalmentBtnClicked();
                    }
                    IsLoading = false;
                }
                catch (Exception)
                {
                    IsLoading = false;
                }


            });


            ZDownloadForm = new Command(async () =>
            {
                IsLoading = true;
                if (objRefNumber != null)
                {
                    string downloadurl = ZATCAConstants.ZOdownloadCoverFormFile + objRefNumber;
                    await _navigationService.NavigateTo(App.PdfView, downloadurl);

                }
                IsLoading = false;
            });
        }
        private void BackNavigations()
        {
            if (IsSummaryEnable)

            {
                EnableListView();
            }
            else
            {
                _navigationService.GoBack();
            }
        }
        public void EnableSummaryView()
        {
            IsSummaryEnable = true;
            CreateZakatInstalmentBtnVisible = false;
        }

        public void EnableListView()
        {
            IsSummaryEnable = false;
            CreateZakatInstalmentBtnVisible = true;
        }

        private bool _CreateZakatInstalmentBtnVisible = true;
        public bool CreateZakatInstalmentBtnVisible
        {
            get
            {
                return _CreateZakatInstalmentBtnVisible;
            }
            set
            {
                if (_CreateZakatInstalmentBtnVisible == value) return;
                _CreateZakatInstalmentBtnVisible = value;
                OnPropertyChanged("CreateZakatInstalmentBtnVisible");
            }
        }
        private bool _IsSummaryEnable = false;
        public bool IsSummaryEnable
        {
            get
            {
                return _IsSummaryEnable;
            }
            set
            {
                if (_IsSummaryEnable == value) return;

                _IsSummaryEnable = value;
                OnPropertyChanged("IsSummaryEnable");
            }
        }



        private string _returnNumber = "";
        public string ReturnNumber
        {
            get
            {
                return _returnNumber;
            }
            set
            {
                if (_returnNumber == value) return;

                _returnNumber = value;
                OnPropertyChanged("ReturnNumber");
            }
        }
        private string _ReferenceNumberOfAssessment = "";
        public string ReferenceNumberOfAssessment
        {
            get
            {
                return _ReferenceNumberOfAssessment;
            }
            set
            {
                if (_ReferenceNumberOfAssessment == value) return;

                _ReferenceNumberOfAssessment = value;
                OnPropertyChanged("ReferenceNumberOfAssessment");
            }
        }

        private string _AssessmentYear = "";
        public string AssessmentYear
        {
            get
            {
                return _AssessmentYear;
            }
            set
            {
                if (_AssessmentYear == value) return;

                _AssessmentYear = value;
                OnPropertyChanged("AssessmentYear");
            }
        }
        private string _PeriodFrom = "";
        public string PeriodFrom
        {
            get
            {
                return _PeriodFrom;
            }
            set
            {
                if (_PeriodFrom == value) return;

                _PeriodFrom = value;
                OnPropertyChanged("PeriodFrom");
            }
        }

        private string _PeriodTo = "";
        public string PeriodTo
        {
            get
            {
                return _PeriodTo;
            }
            set
            {
                if (_PeriodTo == value) return;

                _PeriodTo = value;
                OnPropertyChanged("PeriodTo");
            }
        }

        private string _DisplaTaxType = "";
        public string DisplaTaxType
        {
            get
            {
                return _DisplaTaxType;
            }
            set
            {
                if (_DisplaTaxType == value) return;

                _DisplaTaxType = value;
                OnPropertyChanged("DisplaTaxType");
            }
        }
        private string _Currency = "";
        public string Currency
        {
            get
            {
                return _Currency;
            }
            set
            {
                if (_Currency == value) return;

                _Currency = value;
                OnPropertyChanged("Currency");
            }
        }
        private string _AssessmentAmount = "";
        public string AssessmentAmount
        {
            get
            {
                return _AssessmentAmount;
            }
            set
            {
                if (_AssessmentAmount == value) return;

                _AssessmentAmount = value;
                OnPropertyChanged("AssessmentAmount");
            }
        }
        private string _DisplayRevisedAmount = "";
        public string DisplayRevisedAmount
        {
            get
            {
                return _DisplayRevisedAmount;
            }
            set
            {
                if (_DisplayRevisedAmount == value) return;

                _DisplayRevisedAmount = value;
                OnPropertyChanged("DisplayRevisedAmount");
            }
        }
        private string _DisplayDisputeAmount = "";
        public string DisplayDisputeAmount
        {
            get
            {
                return _DisplayDisputeAmount;
            }
            set
            {
                if (_DisplayDisputeAmount == value) return;

                _DisplayDisputeAmount = value;
                OnPropertyChanged("DisplayDisputeAmount");
            }
        }
        private string _objRefNumber = "";
        public string objRefNumber
        {
            get
            {
                return _objRefNumber;
            }
            set
            {
                if (_objRefNumber == value) return;

                _objRefNumber = value;
                OnPropertyChanged("objRefNumber");
            }
        }
        private string _DisplayDetailDescription = "";
        public string DisplayDetailDescription
        {
            get
            {
                return _DisplayDetailDescription;
            }
            set
            {
                if (_DisplayDetailDescription == value) return;

                _DisplayDetailDescription = value;
                OnPropertyChanged("DisplayDetailDescription");
            }
        }

        private string _DisplayRemarks = "";
        public string DisplayRemarks
        {
            get
            {
                return _DisplayRemarks;
            }
            set
            {
                if (_DisplayRemarks == value) return;

                _DisplayRemarks = value;
                OnPropertyChanged("DisplayRemarks");
            }
        }

        private bool _IsAttachmentsVisible = false;
        public bool IsAttachmentsVisible
        {
            get
            {
                return _IsAttachmentsVisible;
            }
            set
            {
                if (_IsAttachmentsVisible == value) return;

                _IsAttachmentsVisible = value;
                OnPropertyChanged("IsAttachmentsVisible");
            }
        }

        private string _objectionsCount = "";
        public string ObjectionsCount
        {
            get
            {
                return _objectionsCount;
            }
            set
            {
                if (_objectionsCount == value) return;

                _objectionsCount = value;
                OnPropertyChanged("ObjectionsCount");
            }
        }

        private ObservableCollection<ZakatObjectionListModel.Result> _objectionsList;
        public ObservableCollection<ZakatObjectionListModel.Result> ObjectionsList
        {
            get
            {
                return _objectionsList;
            }
            set
            {
                if (_objectionsList == value) return;

                _objectionsList = value;
                OnPropertyChanged("ObjectionsList");
            }
        }

        public string _formattedObjectionDate = "";

        public string FormattedObjectionDate
        {
            get { return _formattedObjectionDate; }
            set
            {
                if (_formattedObjectionDate == value) return;

                _formattedObjectionDate = value;
                OnPropertyChanged("FormattedObjectionDate");
            }
        }
        public ObservableCollection<Attachment> attachmentsListViewData { get; set; }
        public ObservableCollection<Attachment> AttachmentsListViewData
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
                OnPropertyChanged("AttachmentsListViewData");
            }
        }

        public void ResetData()
        {
            ObjectionsList = new ObservableCollection<ZakatObjectionListModel.Result>();
            ObjectionsCount = 0 + "  " + AppResources.ZakatObjection;
            AttachmentsListViewData = null;

            EnableListView();
        }


        public void ReqInstalmentBtnClicked()
        {
            _navigationService.NavigateTo(App.ZakatObjectionPageView);
        }


        public void BindSummaryData(ZakatObjectionRequestSummaryModel _ZakatObjectionRequestSummary)
        {
            var attachmentsListViewData1 = new ObservableCollection<Attachment>();

            foreach (Attachment attachment in _ZakatObjectionRequestSummary.d.AttDetSet)
            {
                attachmentsListViewData1.Add(attachment);
            }

            AttachmentsListViewData = attachmentsListViewData1;
            if (AttachmentsListViewData.Count > 0)
            {
                IsAttachmentsVisible = true;
            }
        }

        #region API Integration
        public async Task ZAKATObjectionList()
        {
            try
            {
                IsLoading = true;

                ZakatObjectionListModel _ZAKATObjectionList = new ZakatObjectionListModel();
                try
                {
                    _ZAKATObjectionList = await ZAKATObjectionsWebServiceManager.GAZTGetZAKATObjectionList();

                    var objectionsList = new ObservableCollection<ZakatObjectionListModel.Result>();
                    if (_ZAKATObjectionList != null && _ZAKATObjectionList.d != null)
                    {
                        foreach (var objection in _ZAKATObjectionList.d.ListSet)
                        {
                            objectionsList.Add(objection);
                        }
                        ObjectionsList = objectionsList;
                        ObjectionsCount = ObjectionsList.Count + "  " + AppResources.ZakatObjection;
                    }

                    else
                    {
                        IsLoading = false;
                        await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        _navigationService.GoBack();
                    }
                    IsLoading = false;
                }
                catch (InternetException ex)
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);

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

        public async Task ZakatObjectionData(string fbnum)
        {
            try
            {
                IsLoading = true;
                ZAKATObjectionDataModel _ZAKATObjectionData = new ZAKATObjectionDataModel();
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

        public async Task showRejectPopup()
        {

            IsLoading = false;
            await _dialogService.ShowMessage(AppResources.ZakatObjectionPortalMessage, AppResources.Information);

        }
        public async Task GetWithdrawReviewReasonTP10(string SelectedFbNum)
        {
            try
            {

                IsLoading = true;
                ZakatObjectionSummaryModel _ZAKATObjectionWithDraw = new ZakatObjectionSummaryModel();
                try
                {

                    _ZAKATObjectionWithDraw = await ZAKATWithdrawObjectionsWebServiceManager.GAZTGetZakatObjectionSummaryTP10(SelectedFbNum);

                    if (_ZAKATObjectionWithDraw == null)
                    {

                        IsLoading = false;
                        await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        _navigationService.GoBack();
                    }
                    IsLoading = false;
                }
                catch (InternetException ex)
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                }

                IsLoading = false;
            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                IsLoading = false;
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            }
            catch (Exception)
            {
                IsLoading = false;
                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                _navigationService.GoBack();
            }
        }


        public async Task GetWithdrawReviewReason(string SelectedFbNum)
        {
            try
            {
                IsLoading = true;
                ZakatObjectionWDDropdownModel _ZAKATObjectionWithDraw = new ZakatObjectionWDDropdownModel();
                try
                {
                    EnableSummaryView();
                    _ZAKATObjectionWithDraw = await ZAKATWithdrawObjectionsWebServiceManager.GAZTGetZakatWithDrawDDData(SelectedFbNum);

                    if (_ZAKATObjectionWithDraw == null)
                    {
                        CultureInfo cultureInfo = new CultureInfo("ar-SA");

                        DateTime dateTime = DateTime.ParseExact(_ZAKATObjectionWithDraw.d.results[0].APeriodFrom, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
                        string dateStr = dateTime.ToString("MM/dd/yyyy");

                        _ZAKATObjectionWithDraw.d.results[0].APeriodFrom = dateStr;
                        string dt1 = string.Empty;
                        string formatedDate1 = string.Empty;

                        string[] dts = null;
                        dts = _ZAKATObjectionWithDraw.d.results[0].APeriodFrom.Split('/');
                        dt1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];

                        if (App.IsArabic)
                        {

                            formatedDate1 = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];

                        }
                        else
                        {

                            formatedDate1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];

                        }
                        _ZAKATObjectionWithDraw.d.results[0].APeriodFrom = dt1;

                        CultureInfo cultureInfo1 = new CultureInfo("ar-SA");

                        DateTime dateTime2 = DateTime.ParseExact(_ZAKATObjectionWithDraw.d.results[0].APeriodTo, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
                        string dateStr1 = dateTime2.ToString("MM/dd/yyyy");
                        _ZAKATObjectionWithDraw.d.results[0].APeriodTo = dateStr1;
                        string dt11 = string.Empty;
                        string formatedDate11 = string.Empty;

                        string[] dts1 = null;
                        dts1 = _ZAKATObjectionWithDraw.d.results[0].APeriodTo.Split('/');
                        dt11 = dts1[0] + "-" + UtilityManager.GetShortMonthName(dts1[1]) + "-" + dts1[2];

                        if (App.IsArabic)
                        {

                            formatedDate11 = dts1[0] + "-" + UtilityManager.GetMonthName(dts1[1]) + "-" + dts1[2];

                        }
                        else
                        {

                            formatedDate11 = dts1[0] + "-" + UtilityManager.GetShortMonthName(dts1[1]) + "-" + dts1[2];

                        }

                        _ZAKATObjectionWithDraw.d.results[0].APeriodTo = dt11;

                        objRefNumber = _ZAKATObjectionWithDraw.d.results[0].ObjFbnum;
                        ReferenceNumberOfAssessment = _ZAKATObjectionWithDraw.d.results[0].ARefNo;
                        AssessmentYear = _ZAKATObjectionWithDraw.d.results[0].AAssnmtYr;
                        PeriodFrom = formatedDate1;
                        PeriodTo = formatedDate11;

                        if (_ZAKATObjectionWithDraw.d.results[0].ATaxTy.Equals("ITAX"))
                        {
                            DisplaTaxType = AppResources.ZakatInstalmetSelectTypeIncomeTax;
                        }
                        else if (_ZAKATObjectionWithDraw.d.results[0].ATaxTy.Equals("ZAKT"))
                        {
                            DisplaTaxType = AppResources.FORM5Zakat;
                        }

                        Currency = _ZAKATObjectionWithDraw.d.results[0].ACurr;
                        AssessmentAmount = _ZAKATObjectionWithDraw.d.results[0].AAssnmtAmt;
                        DisplayRevisedAmount = _ZAKATObjectionWithDraw.d.results[0].ARevAmt;
                        DisplayDisputeAmount = _ZAKATObjectionWithDraw.d.results[0].ADisputeAmt;
                        
                    }

                    else
                    {
                        IsLoading = false;
                        await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        _navigationService.GoBack();
                    }
                    await ZakatRequestObjectionSummary(SelectedFbNum);
                    IsLoading = false;
                }
                catch (InternetException ex)
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);

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


        public async Task ZakatRequestObjectionSummary(string fbnum)
        {
            try
            {
                IsLoading = true;
                ZakatObjectionRequestSummaryModel _ZakatObjectionRequestSummary = new ZakatObjectionRequestSummaryModel();
                ZAKATObjectionReturnModel.ZAKATObjectionReviewReturnModel _ZAKATObjectionReviewReturn = new ZAKATObjectionReturnModel.ZAKATObjectionReviewReturnModel();
                try
                {
                    _ZakatObjectionRequestSummary = await ZAKATWithdrawObjectionsWebServiceManager.GAZTGetZakatRequestObjectionSummary(fbnum);

                    if (_ZakatObjectionRequestSummary != null && _ZakatObjectionRequestSummary.d != null)
                    {
                        BindSummaryData(_ZakatObjectionRequestSummary);
                    }

                    else
                    {
                        IsLoading = false;
                        await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        _navigationService.GoBack();
                    }
                    IsLoading = false;
                }
                catch (InternetException ex)
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);

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

        #endregion
    }
}
