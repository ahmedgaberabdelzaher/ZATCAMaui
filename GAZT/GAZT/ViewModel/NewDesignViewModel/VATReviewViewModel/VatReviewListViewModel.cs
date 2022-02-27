using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Manager;
using EGAZT.Models;
using EGAZT.Models.VatReviewModel;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Views.NewDesign.VatReview;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel.VatReviewViewModel
{
    [Preserve(AllMembers = true)]
    public class VatReviewListViewModel : BaseViewModel
    {
        public ICommand SelectionGoBackClick { get; set; }
        public ICommand GoBackClick { get; set; }
        public ICommand NewRequestBtnTapped { get; set; }
        public ICommand CloseClick { get; set; }


        enum FilterOptions
        {
            All,
            Objections,
            VatReviews
        }


        public bool isNewRequestCreated = false;
        private bool _isBackButtonVisible = true;

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

        private bool _isVatListVisible = false;

        public bool IsVatListVisible
        {
            get { return _isVatListVisible; }
            set
            {
                if (_isVatListVisible == value) return;

                _isVatListVisible = value;
                RaisePropertyChanged("IsVatListVisible");
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

        private string _filteredSelectionName = "";

        public string FilteredSelectionName
        {
            get { return _filteredSelectionName; }
            set
            {
                if (_filteredSelectionName == value) return;

                _filteredSelectionName = value;
                RaisePropertyChanged("FilteredSelectionName");
            }
        }

        private string _numberOfObjAndReviews = "";

        public string NumberOfObjAndReviews
        {
            get { return _numberOfObjAndReviews; }
            set
            {
                if (_numberOfObjAndReviews == value) return;

                _numberOfObjAndReviews = value;
                RaisePropertyChanged("NumberOfObjAndReviews");
            }
        }

        public class SelectionModel
        {
            public SelectionModel()
            {
            }
            public string CardLabel { get => SelectionTitle; }
            public string SelectionTitle { get; set; }
            public bool IsSelected { get; set; }
        }

        public ObservableCollection<SelectionModel> selectionOptions { get; set; }

        public ObservableCollection<SelectionModel> SelectionOptions
        {
            get { return selectionOptions; }

            set
            {
                if (selectionOptions == value)
                {
                    return;
                }

                selectionOptions = value;
                RaisePropertyChanged("SelectionOptions");
            }
        }


        public ObservableCollection<VATObjectionListModel.Result3> _VatobjListViewData { get; set; }

        public ObservableCollection<VATObjectionListModel.Result3> VATobjListViewData
        {
            get { return _VatobjListViewData; }

            set
            {
                if (_VatobjListViewData == value)
                {
                    return;
                }

                _VatobjListViewData = value;
                RaisePropertyChanged("VATobjListViewData");
            }
        }

        public string _reviewReason = "";

        public string ReviewReason
        {
            get { return _reviewReason; }
            set
            {
                if (_reviewReason == value) return;

                _reviewReason = value;
                RaisePropertyChanged("ReviewReason");
            }
        }

        public string _subReviewReason = "";

        public string SubReviewReason
        {
            get { return _subReviewReason; }
            set
            {
                if (_subReviewReason == value) return;

                _subReviewReason = value;
                RaisePropertyChanged("SubReviewReason");
            }
        }

        public string _applicationRefNumber = "";

        public string ApplicationRefNumber
        {
            get { return _applicationRefNumber; }
            set
            {
                if (_applicationRefNumber == value) return;

                _applicationRefNumber = value;
                RaisePropertyChanged("ApplicationRefNumber");
            }
        }

        public string _requestDate = "";

        public string RequestDate
        {
            get { return _requestDate; }
            set
            {
                if (_requestDate == value) return;

                _requestDate = value;
                RaisePropertyChanged("RequestDate");
            }
        }

        public string _taxPeriodOfCase = "";

        public string TaxPeriodOfCase
        {
            get { return _taxPeriodOfCase; }
            set
            {
                if (_taxPeriodOfCase == value) return;

                _taxPeriodOfCase = value;
                RaisePropertyChanged("TaxPeriodOfCase");
            }
        }

        public string _taxPeriodFrom = "";

        public string TaxPeriodFrom
        {
            get { return _taxPeriodFrom; }
            set
            {
                if (_taxPeriodFrom == value) return;

                _taxPeriodFrom = value;
                RaisePropertyChanged("TaxPeriodFrom");
            }
        }

        private string _idType = "";

        public string IDType
        {
            get { return _idType; }
            set
            {
                if (_idType == value) return;

                _idType = value;
                RaisePropertyChanged("IDType");
            }
        }

        public string _taxPeriodTo = "";

        public string TaxPeriodTo
        {
            get { return _taxPeriodTo; }
            set
            {
                if (_taxPeriodTo == value) return;

                _taxPeriodTo = value;
                RaisePropertyChanged("TaxPeriodTo");
            }
        }

        public string _pickedDate = "";

        public string PickedDate
        {
            get { return _pickedDate; }
            set
            {
                if (_pickedDate == value) return;

                _pickedDate = value;
                RaisePropertyChanged("PickedDate");
            }
        }

        public string _penalityAmountInQuestion = "";

        public string PenalityAmountInQuestion
        {
            get { return _penalityAmountInQuestion; }
            set
            {
                if (_penalityAmountInQuestion == value) return;

                _penalityAmountInQuestion = value;
                RaisePropertyChanged("PenalityAmountInQuestion");
            }
        }

        public string _reportDetails = "";

        public string ReportDetails
        {
            get { return _reportDetails; }
            set
            {
                if (_reportDetails == value) return;

                _reportDetails = value;
                RaisePropertyChanged("ReportDetails");
            }
        }

        public string _sADADNumber = "";

        public string SADADNumber
        {
            get { return _sADADNumber; }
            set
            {
                if (_sADADNumber == value) return;

                _sADADNumber = value;
                RaisePropertyChanged("SADADNumber");
            }
        }

        public string _securityAmount = "";

        public string SecurityAmount
        {
            get { return _securityAmount; }
            set
            {
                if (_securityAmount == value) return;

                _securityAmount = value;
                RaisePropertyChanged("SecurityAmount");
            }
        }

        public string requestedReviewAmount = "";
        public string RequestedReviewAmount
        {
            get
            {
                return requestedReviewAmount;
            }
            set
            {
                if (requestedReviewAmount == value) return;

                requestedReviewAmount = value;
                RaisePropertyChanged("RequestedReviewAmount");
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

        private string _securityType = "";

        public string SecurityType
        {
            get { return _securityType; }
            set
            {
                if (_securityType == value) return;

                _securityType = value;
                RaisePropertyChanged("SecurityType");
            }
        }

        public string _idNumber = "";

        public string IDNumber
        {
            get { return _idNumber; }
            set
            {
                if (_idNumber == value) return;

                _idNumber = value;
                RaisePropertyChanged("IDNumber");
            }
        }

        private List<VATObjectionListModel.Result3> _vatReviewListSet;

        public List<VATObjectionListModel.Result3> VATReviewListSet
        {
            get { return _vatReviewListSet; }
            set
            {
                if (_vatReviewListSet == value) return;

                _vatReviewListSet = value;
                RaisePropertyChanged("VATReviewListSet");
            }
        }

        private VATObjectionFormModel.VATReviewsReturnModel _modelVATReviewsReturn;

        public VATObjectionFormModel.VATReviewsReturnModel modelVATReviewsReturn
        {
            get { return _modelVATReviewsReturn; }
            set
            {
                if (_modelVATReviewsReturn == value) return;

                _modelVATReviewsReturn = value;
                RaisePropertyChanged("modelVATReviewsReturn");
            }
        }

        public ObservableCollection<Attachment> _vatReviewAttachments
        {
            get;
            set;
        }

        public ObservableCollection<Attachment> VatReviewAttachments
        {
            get { return _vatReviewAttachments; }

            set
            {
                if (_vatReviewAttachments == value)
                {
                    return;
                }

                _vatReviewAttachments = value;
                RaisePropertyChanged("VatReviewAttachments");
            }
        }

        private bool _isBankGurantSecuritySelected = false;

        public bool IsBankGurantSecuritySelected
        {
            get { return _isBankGurantSecuritySelected; }
            set
            {
                if (_isBankGurantSecuritySelected == value) return;

                _isBankGurantSecuritySelected = value;
                RaisePropertyChanged("IsBankGurantSecuritySelected");
            }
        }

        public string disputeDetailsDesc = "";
        public string DisputeDetailsDesc
        {
            get
            {
                return disputeDetailsDesc;
            }
            set
            {
                if (disputeDetailsDesc == value) return;

                disputeDetailsDesc = value;
                RaisePropertyChanged("DisputeDetailsDesc");
            }
        }

        private bool _isSadadSecuritySelected = false;

        public bool IsSadadSecuritySelected
        {
            get { return _isSadadSecuritySelected; }
            set
            {
                if (_isSadadSecuritySelected == value) return;

                _isSadadSecuritySelected = value;
                RaisePropertyChanged("IsSadadSecuritySelected");
            }
        }

        private bool isPenlaityAmountVisible = false;
        public bool IsPenlaityAmountVisible
        {
            get
            {
                return isPenlaityAmountVisible;
            }
            set
            {
                if (isPenlaityAmountVisible == value) return;

                isPenlaityAmountVisible = value;
                RaisePropertyChanged("IsPenlaityAmountVisible");
            }
        }

        private bool _isAssessPayOptionVisible = false;
        public bool IsAssessPayOptionVisible
        {
            get { return _isAssessPayOptionVisible; }
            set
            {
                if (_isAssessPayOptionVisible == value) return;

                _isAssessPayOptionVisible = value;
                RaisePropertyChanged("IsAssessPayOptionVisible");
            }
        }

        private bool _isSecurityAmountMorethanZero = false;

        public bool IsSecurityAmountMorethanZero
        {
            get { return _isSecurityAmountMorethanZero; }
            set
            {
                if (_isSecurityAmountMorethanZero == value) return;

                _isSecurityAmountMorethanZero = value;
                RaisePropertyChanged("IsSecurityAmountMorethanZero");
            }
        }

        public ObservableCollection<Attachment> attachmentsListViewData { get; set; }

        public ObservableCollection<Attachment> AttachmentsListViewData
        {
            get { return attachmentsListViewData; }

            set
            {
                if (attachmentsListViewData == value)
                {
                    return;
                }

                attachmentsListViewData = value;
                RaisePropertyChanged("AttachmentsListViewData");
            }
        }

        private bool isSecurityPaymentsTabVisible = false;
        public bool IsSecurityPaymentsTabVisible
        {
            get
            {


                return isSecurityPaymentsTabVisible;

            }
            set
            {
                if (isSecurityPaymentsTabVisible == value) return;

                isSecurityPaymentsTabVisible = value;
                RaisePropertyChanged("IsSecurityPaymentsTabVisible");
            }
        }
        private bool isDOBVisible = false;
        public bool IsDOBVisible
        {
            get
            {


                return isDOBVisible;

            }
            set
            {
                if (isDOBVisible == value) return;

                isDOBVisible = value;
                RaisePropertyChanged("IsDOBVisible");
            }
        }

        public ObservableCollection<Attachment> bankGuranteeAttachmentsListViewData { get; set; }

        public ObservableCollection<Attachment> BankGuranteeAttachmentsListViewData
        {
            get { return bankGuranteeAttachmentsListViewData; }

            set
            {
                if (bankGuranteeAttachmentsListViewData == value)
                {
                    return;
                }

                bankGuranteeAttachmentsListViewData = value;
                RaisePropertyChanged("BankGuranteeAttachmentsListViewData");
            }
        }

        public VatReviewListViewModel(INavigationService navigationService, IDialogService dialogService) : base(
            navigationService, dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }

            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }


            GoBackClick = new Command(() => {
                if (IsVatListVisible)
                {
                    _navigationService.GoBack();
                    return;
                }
                EnableListView();
            });

            CloseClick = new Command(() => { _navigationService.GoBack(); });

            NewRequestBtnTapped = new Command(() =>
            {
                if (!isNewRequestCreated)
                {
                    isNewRequestCreated = true;
                    ShowVatReviewPage();
                }
            });
        }

        public void EnableSummaryView()
        {
            IsBackButtonVisible = true;
            SummaryVisible = true;
            IsVatListVisible = false;
        }

        public void EnableListView()
        {
            IsBackButtonVisible = true;
            SummaryVisible = false;
            IsVatListVisible = true;
        }

        public void ResetSelectionData()
        {
            IsLoading = false;
            EnableListView();
            //AddOutletDecisionOptions();
        }

        public async void ShowVatReviewPage()
        {

            App.selectedVATItem = "";
            App.selectedVATItemFbust = "";
            await Application.Current.MainPage.Navigation.PushAsync(new VatReviewPageView());
            // _navigationService.NavigateTo(App.VatReviewPageView);
        }

        public void ResetListData()
        {


            VATobjListViewData = new ObservableCollection<VATObjectionListModel.Result3>();
            IsLoading = false;
            SetFilterOptions((int)FilterOptions.All);
            NumberOfObjAndReviews = "0 " + AppResources.VatReview;
            EnableListView();
        }

        private void SetFilterOptions(int selectedFilter)
        {
            if (selectedFilter == (int)FilterOptions.All)
            {
                FilteredSelectionName = AppResources.VRAll;
            }
            else if (selectedFilter == (int)FilterOptions.Objections)
            {
                FilteredSelectionName = AppResources.ZakatObjection;
            }
            else if (selectedFilter == (int)FilterOptions.VatReviews)
            {
                FilteredSelectionName = AppResources.VatReview;
            }
        }

        private void AddOutletDecisionOptions()
        {
            var outletDecisionOptions = new ObservableCollection<SelectionModel>();
            outletDecisionOptions.Add(new SelectionModel
            {
                SelectionTitle = AppResources.VatReview,
                IsSelected = false
            });
            outletDecisionOptions.Add(new SelectionModel
            {
                SelectionTitle = AppResources.ZakatObjection,
                IsSelected = false
            });
            SelectionOptions = outletDecisionOptions;
        }

        public async Task OnPageLoad1(int index)
        {
            try
            {
                await Task.Run(() => { IsLoading = true; });
                await Task.Run(async () =>
                {
                    IsLoading = true;


                    try
                    {
                        var item = VATobjListViewData[index];
                        VATObjectionSummaryModel modelVATReview = new VATObjectionSummaryModel();
                        modelVATReviewsReturn = new VATObjectionFormModel.VATReviewsReturnModel();

                        modelVATReview = await VATObjectionWebServiceManager.GAZTGetVATObjectionSummary(item.Fbnum);

                        if (modelVATReview != null && modelVATReview.d != null)
                        {
                            BindSummaryData(modelVATReview);
                        }

                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong,
                                    AppResources.Information);
                                _navigationService.GoBack();
                            });
                        }

                        // }

                        IsLoading = false;
                    }
                    catch (GAZTVATRegistrationInProcessException ex)
                    {
                        throw ex;
                    }
                    catch (InternetException ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            IsLoading = false;
                            _navigationService.GoBack();
                        });
                    }
                });
                await Task.Run(() => { IsLoading = false; });
            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                await Task.Run(() => { IsLoading = false; });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public async Task VATObjectionList()

        {
            try
            {
                await Task.Run(() => { IsLoading = true; });
                await Task.Run(async () =>
                {
                    IsLoading = true;

                    VATObjectionListModel _VATObjectionList = new VATObjectionListModel();
                    try
                    {
                        _VATObjectionList = await VATObjectionWebServiceManager.GAZTGetVATObjectionList();

                        if (_VATObjectionList != null && _VATObjectionList.d != null)
                        {
                            var selectedAssets = _VATObjectionList.d.ASSLISTSet.results
                                .Where(x => x.Fbtyp.ToUpper() == "RAVT").ToList();
                            // PopulateVATReviewList();
                            VATReviewListSet = selectedAssets;

                            PopulateVATReviewList();
                        }

                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong,
                                    AppResources.Information);
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
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            IsLoading = false;
                            _navigationService.GoBack();
                        });
                    }
                });
                await Task.Run(() => { IsLoading = false; });
            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                await Task.Run(() => { IsLoading = false; });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        private void PopulateVATReviewList()
        {
            var vatreviews = new ObservableCollection<VATObjectionListModel.Result3>();

            foreach (var vatreview in VATReviewListSet)
            {
                vatreviews.Add(vatreview);
            }

            VATobjListViewData = vatreviews;
            NumberOfObjAndReviews = vatreviews.Count + " " + AppResources.VatReview;
        }

        private async void BindSummaryData(VATObjectionSummaryModel responseModel)
        {
            List<VATObjectionFormModel.ReviewReason> reasonList =
                new List<VATObjectionFormModel.ReviewReason>();
            if (responseModel.d.MainReasonSet.results.Count > 0)
            {
                for (int i = 0; i < responseModel.d.MainReasonSet.results.Count; i++)
                {
                    VATObjectionFormModel.ReviewReason obj = new VATObjectionFormModel.ReviewReason();
                    obj.ProcCD = responseModel.d.MainReasonSet.results[i].ProcCd;
                    obj.Reasons = responseModel.d.MainReasonSet.results[i].TypeT;
                    if (responseModel.d.ReasonSet.results.Where(x =>
                        x.ProcCd == responseModel.d.MainReasonSet.results[i].ProcCd).Count() > 0)
                    {
                        List<VATObjectionFormModel.SubReason> subReasonList =
                            new List<VATObjectionFormModel.SubReason>();
                        var lstSub = responseModel.d.ReasonSet.results.Where(x =>
                            x.ProcCd == responseModel.d.MainReasonSet.results[i].ProcCd).ToList();
                        for (int j = 0; j < lstSub.Count(); j++)
                        {
                            VATObjectionFormModel.SubReason sub = new VATObjectionFormModel.SubReason();
                            sub.Code = lstSub[j].Code;
                            sub.SubReasons = lstSub[j].SubtypT;
                            subReasonList.Add(sub);
                        }

                        obj.ListSubReason = subReasonList;
                    }

                    reasonList.Add(obj);
                }
            }

            modelVATReviewsReturn.ListReviewReason = reasonList;

            if (responseModel.d.SecurityDtl.Sectp == "C")
            {
                SecurityType = AppResources.VRSADAD;
                IsSadadSecuritySelected = true;
                IsBankGurantSecuritySelected = false;
            }
            else
            {
                SecurityType = AppResources.VRBANKGURANTEE;
                IsSadadSecuritySelected = false;
                IsBankGurantSecuritySelected = true;
            }

            //Step3 Details

            var selectedReason = reasonList.First(x => x.ProcCD == responseModel.d.RvRsn);

            if (selectedReason.ProcCD == "VTPC" || selectedReason.ProcCD == "VTPN" || selectedReason.ProcCD == "VTAS")
            {
                IsSecurityPaymentsTabVisible = true;
                IsPenlaityAmountVisible = true;
            }
            else
            {
                IsSecurityPaymentsTabVisible = false;
                IsPenlaityAmountVisible = false;
            }

            if (selectedReason.ProcCD == "VTAS")
            {
                IsAssessPayOptionVisible = true;
                IsPenlaityAmountVisible = false;
            }
            else
            {
                IsAssessPayOptionVisible = false;
            }

            ReviewReason = selectedReason.Reasons;
            SubReviewReason = selectedReason.ListSubReason.First(x => x.Code == responseModel.d.RvSubRsn).SubReasons;


            ApplicationRefNumber = responseModel.d.RejFb;

            string strRequestedDate = "";
            if (responseModel.d.DecDt != null)
            {

                DateTime dateStart = new DateTime();
                CultureInfo cultureInfo = new CultureInfo("ar-SA");
                string apiDate = @"""" + responseModel.d.DecDt + @"""";
                dateStart = JsonConvert.DeserializeObject<DateTime>(apiDate);

                GregorianCalendar hjCalendar = new GregorianCalendar();
                int year = hjCalendar.GetYear(dateStart);
                int month = hjCalendar.GetMonth(dateStart);
                int day = hjCalendar.GetDayOfMonth(dateStart);

                string dateStr = string.Format("{0:00}/{1}/{2}", day, month, year);


                string dt1 = string.Empty;
                string[] dts = null;
                dts = dateStr.Split('/');
                dt1 = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
                strRequestedDate = dt1;
            }

            //PickedDate = strRequestedDate;
            RequestDate = strRequestedDate;

            TaxPeriodOfCase = responseModel.d.SecurityDtl.Perslt;

            string strTaxPeriodFromDate = "";
            if (responseModel.d.SecurityDtl.Abrzu != null)
            {

                DateTime dateStart = new DateTime();
                CultureInfo cultureInfo = new CultureInfo("ar-SA");
                string apiDate = @"""" + responseModel.d.SecurityDtl.Abrzu + @"""";
                dateStart = JsonConvert.DeserializeObject<DateTime>(apiDate);

                GregorianCalendar hjCalendar = new GregorianCalendar();
                int year = hjCalendar.GetYear(dateStart);
                int month = hjCalendar.GetMonth(dateStart);
                int day = hjCalendar.GetDayOfMonth(dateStart);

                string dateStr = string.Format("{0:00}/{1}/{2}", day, month, year);


                string dt1 = string.Empty;
                string[] dts = null;
                dts = dateStr.Split('/');
                dt1 = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
                strTaxPeriodFromDate = dt1;

            }
            TaxPeriodFrom = strTaxPeriodFromDate;

          
            //ReportDetails = responseModel.d.NotesSet.results[0].Tdline;
            RequestedReviewAmount = UtilityManager.GetCommaSeparatedAmount(responseModel.d.SecurityDtl.Disamt);
            string strTaxPeriodToDate = "";
            if (responseModel.d.SecurityDtl.Abrzo != null)
            {

                DateTime dateStart = new DateTime();
                CultureInfo cultureInfo = new CultureInfo("ar-SA");
                string apiDate = @"""" + responseModel.d.SecurityDtl.Abrzo + @"""";
                dateStart = JsonConvert.DeserializeObject<DateTime>(apiDate);

                GregorianCalendar hjCalendar = new GregorianCalendar();
                int year = hjCalendar.GetYear(dateStart);
                int month = hjCalendar.GetMonth(dateStart);
                int day = hjCalendar.GetDayOfMonth(dateStart);

                string dateStr = string.Format("{0:00}/{1}/{2}", day, month, year);


                string dt1 = string.Empty;
                string[] dts = null;
                dts = dateStr.Split('/');
                dt1 = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
                strTaxPeriodToDate = dt1;

            }

            TaxPeriodTo = strTaxPeriodToDate;

            PenalityAmountInQuestion = UtilityManager.GetCommaSeparatedAmount(responseModel.d.SecurityDtl.Penamount);

            modelVATReviewsReturn.TotalTaxLiability = responseModel.d.SecurityDtl.Liaamt;
            modelVATReviewsReturn.TaxPaid = responseModel.d.SecurityDtl.Clramt;

            modelVATReviewsReturn.RequestToReviewAmount = UtilityManager.GetCommaSeparatedAmount(responseModel.d.SecurityDtl.Amttp);
            modelVATReviewsReturn.ParticularAmount = UtilityManager.GetCommaSeparatedAmount(responseModel.d.SecurityDtl.Disamt);
            SecurityAmount = UtilityManager.GetCommaSeparatedAmount(responseModel.d.SecurityDtl.Secamt);

            if (Double.Parse(SecurityAmount) == 0)
            {
                IsSecurityAmountMorethanZero = false;
                IsSadadSecuritySelected = false;
                IsBankGurantSecuritySelected = false;
            }
            else
            {
                IsSecurityAmountMorethanZero = true;
            }
            modelVATReviewsReturn.MethodSubmitSecurity = responseModel.d.SecurityDtl.Sectp;
            modelVATReviewsReturn.ChkSecurityPayment = responseModel.d.SecurityDtl.ChkCash;
            SADADNumber = responseModel.d.SecurityDtl.Sopbel;
            modelVATReviewsReturn.ChkBankGuarantee = responseModel.d.SecurityDtl.ChkBank;
            modelVATReviewsReturn.ChkInfoCorrect = responseModel.d.DecFlg1;
            if (responseModel.d.IdType == "ZS0001")
            {
                IDType = AppResources.VFCNationalID;
                IsDOBVisible = true;
            }
            else if (responseModel.d.IdType == "ZS0002")
            {
                IDType = AppResources.VFCIqamaID;
                IsDOBVisible = true;
            }
            else if (responseModel.d.IdType == "ZS0003")
            {
                IDType = AppResources.VFCGCCID;
                IsDOBVisible = false;
            }

            IDNumber = responseModel.d.DecIdNo;
            ContactPersonName = responseModel.d.Decnm;

            modelVATReviewsReturn.NameOfTaxPayer = responseModel.d.FullName;
            ContactPersonName = responseModel.d.FullName;
            modelVATReviewsReturn.ApplicationNo = responseModel.d.DecIdNo;
            modelVATReviewsReturn.Date = (responseModel.d.Declarationdt != null)
                ? responseModel.d.Declarationdt.ToString()
                : "";

            var bankAttachments = new ObservableCollection<Attachment>();
            var attachments = new ObservableCollection<Attachment>();

            foreach (var attach in responseModel.d.AttdetSet.results)
            {
                if (attach.Dotyp == "RAGA")
                {
                    attachments.Add(attach);
                }
                else if (attach.Dotyp == "RVBT")
                {
                    bankAttachments.Add(attach);
                }

            }

            BankGuranteeAttachmentsListViewData = bankAttachments;
            AttachmentsListViewData = attachments;

            if(responseModel.d.Fbstax.Equals("IP011") && responseModel.d.Fbustx.Equals("E0018")){
                Device.BeginInvokeOnMainThread(async () =>
                {
                    //await _dialogService.ShowMessage(AppResources.VATReviewPleaseVisitPortal,
                    //                    AppResources.Information);

                    try
                    {


                        var VisitPortalPopup = new ReturnPortalNavigationPopUp(AppResources.VATReviewPleaseVisitPortal);

                        if (App.IsArabic)
                        {
                            VisitPortalPopup.OnGotoPortal = () =>
                            {

                                Launcher.OpenAsync(Constants.GAZTVisitPortalUrlAR);

                            };
                        }
                        else
                        {
                            VisitPortalPopup.OnGotoPortal = () =>
                            {

                                Launcher.OpenAsync(Constants.GAZTVisitPortalUrlEN);

                            };
                        }
                        await PopupNavigation.Instance.PushAsync(VisitPortalPopup);
                    }
                    catch (Exception ex)
                    {

                    }

                });
                
            }

            foreach (var note in responseModel.d.NotesSet.results)
            {
                if (note.Rcodez == "RAVT_SDCAS")
                {
                    DisputeDetailsDesc = note.Strline;
                    return;
                }
                if (note.Rcodez == "RAVT_BOX")
                {
                    ReportDetails = note.Strline;
                    return;
                }

            }
        }

        public static DateTime ConvertJsonToDateTime(string jsonDate)
        {
            // JavaScript uses the unix epoch of 1/1/1970. Note, it's important to call ToLocalTime()
            // after doing the time conversion, otherwise we'd have to deal with daylight savings hooey.
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            Double milliseconds = Convert.ToDouble(jsonDate);
            DateTime dateTime = unixEpoch.AddMilliseconds(milliseconds).ToLocalTime();

            return dateTime;
        }
    }
}