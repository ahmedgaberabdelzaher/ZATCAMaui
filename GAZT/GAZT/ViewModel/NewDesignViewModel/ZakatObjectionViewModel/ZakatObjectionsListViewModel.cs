using EGAZT.Models;
using EGAZT.Models.ZakatObjectionsModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel.ZAKATObjectionPages
{
    [Preserve(AllMembers = true)]
    public class ZakatObjectionsListViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        #region Commands

        public ICommand ReqInstalmentBtnTapped { get; set; }
        public ICommand CloseClick { get; set; }
        public ICommand GoBackClick { get; set; }
        public ICommand Download_Acknowledgement { get; set; }
        public ICommand ZDownloadForm { get; set; }

        #endregion

        public ZakatObjectionsListViewModel(INavigationService navigationService, IDialogService dialogService)
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


            ReqInstalmentBtnTapped = new Command(this.ReqInstalmentBtnClicked);
            CloseClick = new Command(async () => { _navigationService.GoBack(); });

            GoBackClick = new Command(async () => { BackNavigations(); });

            Download_Acknowledgement = new Command(async () =>
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                if (objRefNumber != null)
                {
                    String downloadurl = Constants.ZOdownloadAckLetter + "'" + objRefNumber + "')/$value";
                    _navigationService.NavigateTo(App.PdfView, downloadurl);

                }
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            });


            ZDownloadForm = new Command(async () =>
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                if (objRefNumber != null)
                {
                    string downloadurl = Constants.ZOdownloadCoverFormFile + "'" + objRefNumber + "')/$value";
                    _navigationService.NavigateTo(App.PdfView, downloadurl);

                }
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
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
                RaisePropertyChanged("CreateZakatInstalmentBtnVisible");
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
                RaisePropertyChanged("IsSummaryEnable");
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
                if (_isLoading == value) return;

                _isLoading = value;
                RaisePropertyChanged("IsLoading");
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
                RaisePropertyChanged("ReturnNumber");
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
                RaisePropertyChanged("ReferenceNumberOfAssessment");
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
                RaisePropertyChanged("AssessmentYear");
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
                RaisePropertyChanged("PeriodFrom");
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
                RaisePropertyChanged("PeriodTo");
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
                RaisePropertyChanged("DisplaTaxType");
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
                RaisePropertyChanged("Currency");
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
                RaisePropertyChanged("AssessmentAmount");
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
                RaisePropertyChanged("DisplayRevisedAmount");
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
                RaisePropertyChanged("DisplayDisputeAmount");
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
                RaisePropertyChanged("objRefNumber");
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
                RaisePropertyChanged("DisplayDetailDescription");
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
                RaisePropertyChanged("DisplayRemarks");
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
                RaisePropertyChanged("IsAttachmentsVisible");
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
                RaisePropertyChanged("ObjectionsCount");
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
                RaisePropertyChanged("ObjectionsList");
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
                RaisePropertyChanged("FormattedObjectionDate");
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
                RaisePropertyChanged("AttachmentsListViewData");
            }
        }

        public void ResetData()
        {
            ObjectionsList = new ObservableCollection<ZakatObjectionListModel.Result>();
            ObjectionsCount = 0 + " " + AppResources.ZakatObjection;
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

            foreach (Attachment attachment in _ZakatObjectionRequestSummary.d.AttDetSet.results)
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
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    IsLoading = true;
                    ZakatObjectionListModel _ZAKATObjectionList = new ZakatObjectionListModel();
                    try
                    {
                        _ZAKATObjectionList = await WebServiceManager.GAZTGetZAKATObjectionList();

                        var objectionsList = new ObservableCollection<ZakatObjectionListModel.Result>();
                        if (_ZAKATObjectionList != null && _ZAKATObjectionList.d != null)
                        {
                            foreach (var objection in _ZAKATObjectionList.d.ListSet.results)
                            {


                                string strRequestedDate = objection.Erfdate;
                                if (objection.Erfdate != null)
                                {

                                    DateTime dateStart = new DateTime();
                                    CultureInfo cultureInfo = new CultureInfo("ar-SA");
                                    string apiDate = @"""" + objection.Erfdate + @"""";
                                    dateStart = JsonConvert.DeserializeObject<DateTime>(apiDate);

                                    GregorianCalendar hjCalendar = new GregorianCalendar();
                                    int year = hjCalendar.GetYear(dateStart);
                                    int month = hjCalendar.GetMonth(dateStart);
                                    int day = hjCalendar.GetDayOfMonth(dateStart);

                                    string dateStr = string.Format("{0:00}/{1}/{2}", day, month, year);


                                    string dt1 = string.Empty;
                                    string[] dts = null;
                                    dts = dateStr.Split('/');

                                    if (App.IsArabic)
                                    {

                                        dt1 = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];

                                    }
                                    else
                                    {

                                        dt1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];

                                    }


                                    strRequestedDate = dt1;
                                }

                                objection.Erfdate = strRequestedDate;



                                objectionsList.Add(objection);
                            }
                            ObjectionsList = objectionsList;
                            ObjectionsCount = ObjectionsList.Count + " " + AppResources.ZakatObjection;
                        }

                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
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
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            IsLoading = false;
                            _navigationService.GoBack();
                        });
                        //   await Task.Run(() =>
                        //   {
                        //  });
                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
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
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public async Task ZakatObjectionData(string fbnum)
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
                    ZAKATObjectionDataModel _ZAKATObjectionData = new ZAKATObjectionDataModel();
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
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
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public async Task showRejectPopup()
        {

            await Task.Run(() =>
            {
                IsLoading = false;
            });
            Device.BeginInvokeOnMainThread(async () =>
            {
                await _dialogService.ShowMessage(AppResources.ZakatObjectionPortalMessage, AppResources.Information);
            });

        }
        public async Task GetWithdrawReviewReasonTP10(string SelectedFbNum)
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
                    ZakatObjectionSummaryModel _ZAKATObjectionWithDraw = new ZakatObjectionSummaryModel();
                    try
                    {
                        //EnableSummaryView();
                        //Data binding for withdraw objection details
                        _ZAKATObjectionWithDraw = await WebServiceManager.GAZTGetZakatObjectionSummaryTP10(SelectedFbNum);

                        if (_ZAKATObjectionWithDraw != null && _ZAKATObjectionWithDraw.d != null)
                        {

                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
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
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            IsLoading = false;
                            _navigationService.GoBack();
                        });
                        //   await Task.Run(() =>
                        //   {
                        //  });
                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }


        public async Task GetWithdrawReviewReason(string SelectedFbNum)
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
                    ZakatObjectionWDDropdownModel _ZAKATObjectionWithDraw = new ZakatObjectionWDDropdownModel();
                    try
                    {
                        EnableSummaryView();
                        //Data binding for withdraw objection details
                        _ZAKATObjectionWithDraw = await WebServiceManager.GAZTGetZakatWithDrawDDData(SelectedFbNum);

                        if (_ZAKATObjectionWithDraw != null && _ZAKATObjectionWithDraw.d != null)
                        {
                            DateTime dateStart = new DateTime();
                            CultureInfo cultureInfo = new CultureInfo("ar-SA");
                            string apiDate = @"""" + _ZAKATObjectionWithDraw.d.results[0].APeriodFrom + @"""";
                            dateStart = JsonConvert.DeserializeObject<DateTime>(apiDate);
                            GregorianCalendar hjCalendar = new GregorianCalendar();
                            int year = hjCalendar.GetYear(dateStart);
                            int month = hjCalendar.GetMonth(dateStart);
                            int day = hjCalendar.GetDayOfMonth(dateStart);
                            string dateStr = string.Format("{0:00}/{1}/{2}", day, month, year);
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

                            DateTime dateStart1 = new DateTime();
                            CultureInfo cultureInfo1 = new CultureInfo("ar-SA");
                            string apiDate1 = @"""" + _ZAKATObjectionWithDraw.d.results[0].APeriodTo + @"""";
                            dateStart1 = JsonConvert.DeserializeObject<DateTime>(apiDate1);
                            GregorianCalendar hjCalendar1 = new GregorianCalendar();
                            int year1 = hjCalendar1.GetYear(dateStart1);
                            int month1 = hjCalendar1.GetMonth(dateStart1);
                            int day1 = hjCalendar1.GetDayOfMonth(dateStart1);
                            string dateStr1 = string.Format("{0:00}/{1}/{2}", day1, month1, year1);
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
                            //var selectedAssets = _ZAKATObjectionSummary.d.ASSLISTSet.results.Where(x => x.Fbtyp.ToUpper() == "RAVT").ToList();
                        }

                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                _navigationService.GoBack();
                            });
                        }
                        ZakatRequestObjectionSummary(SelectedFbNum);
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
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
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
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }


        public async Task ZakatRequestObjectionSummary(string fbnum)
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
                    ZakatObjectionRequestSummaryModel _ZakatObjectionRequestSummary = new ZakatObjectionRequestSummaryModel();
                    ZAKATObjectionReturnModel.ZAKATObjectionReviewReturnModel _ZAKATObjectionReviewReturn = new ZAKATObjectionReturnModel.ZAKATObjectionReviewReturnModel();
                    try
                    {
                        _ZakatObjectionRequestSummary = await WebServiceManager.GAZTGetZakatRequestObjectionSummary(fbnum);

                        if (_ZakatObjectionRequestSummary != null && _ZakatObjectionRequestSummary.d != null)
                        {


                            BindSummaryData(_ZakatObjectionRequestSummary);
                        }

                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
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
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            IsLoading = false;
                            _navigationService.GoBack();
                        });
                        //   await Task.Run(() =>
                        //   {
                        //  });
                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
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
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        #endregion
    }
}
