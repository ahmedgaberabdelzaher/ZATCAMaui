using System.Collections.ObjectModel;
using System.Globalization;
using System.Timers;
using System.Windows.Input;
using Newtonsoft.Json;
using Mopups.Services;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models.InstalmentPlanModel;
using ZATCAMAUI.Models.ZakatInstalationModels;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Views.NewDesign.ZakatInstalmentPlan;
using Timer = System.Timers.Timer;
using ZATCAMAUI.Core.Interfaces;
using Metadata = ZATCAMAUI.Models.ZakatInstalationModels.Metadata;
using ZATCAMAUI.Views.NewDesign.ZakatRejectPopUp;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel
{

    public class ZakatInstalmentPlanListViewModel : BaseViewModel
    {
        #region Variable
        private bool _isZakatLandingPageVisible = false;
        private bool _createZakatInstalmentBtnVisible = true;
        private bool _isLoading = false;
        private bool _isRevokZakatInstalmentVisible = false;
        private bool _isOTPPageVisible = false;
        private bool _IsZakatSummaryVisible = false;
        private bool _isZakatSummaryRevokeVisible = false;
        private bool _isAttachmentsViewEnabled = false;
        ZakatInstalmentValidateNewRequestModel result ;
        public List<ZakatInvoicesResult> selectedList = new List<ZakatInvoicesResult>();

        public Timer otpTimer;
        public int countDownSeconds;

        public ZakatListModel ZakatListObjec;

        #endregion

        #region commands
        public ICommand OutletDecisionOptionsListViewTapCommand { get; set; }
        public ICommand SummaryattachmentsListViewTapCommand { get; set; }
        public ICommand ReqInstalmentBtnTapped { get; set; }
        public ICommand GoBackClick { get; set; }
        public ICommand CloseClick { get; set; }
        public ICommand OnContinueClick { get; set; }
        public ICommand SummaryRevokeBtnTapped { get; set; }
        public ICommand ApproveButtonTapped { get; set; }
        public ICommand RejecteButtonTapped { get; set; }
        public Command NoteContinueTapped { get; set; }
        public Command OnResendOTPClicked { get; set; }
        public Command Download_Acknowledgement { get; set; }
        public Command ZDownloadForm { get; set; }

        #endregion

        public ZakatInstalmentPlanListViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

            CloseClick = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            });


            IsZakat = Preferences.Get("isZakat", false);
            if (IsZakat)
            {
                ZakatTitle = AppResources.ZakatInstalmetSelectTypeZakat;
            }
            else
            {
                ZakatTitle = AppResources.ZakatInstalmetSelectTypeIncomeTax;
            }

            GoBackClick = new Command(async () =>
            {
                if (IsZakatLandingPageVisible)
                {
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else if (CreateZakatInstalmentBtnVisible)
                {
                    await Application.Current.MainPage.Navigation.PopAsync();
                    // EnableZakatLandingPage();
                }
                else if (IsNoteViewVisible)
                {
                    EnableZakatInstalmentSummary();
                }
                else if (IsZakatSummaryVisible)
                {
                    EnableCreateZakatInstalment();
                }
                else if (IsDueBillsVisible)
                {
                    EnableCreateZakatInstalment();
                }
                else if (IsRevokZakatInstalmentVisible)
                {
                    EnableCreateZakatInstalment();
                }
                else if (IsOTPPageVisible)
                {
                    EnableNotePage();
                }

            });

            OnContinueClick = new Command(async () =>
            {
                SetOTP();
                if (!string.IsNullOrEmpty(EnteredOTP) && EnteredOTP.Length > 0)
                {
                    await ValidateOTPAsync();
                }
                else
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Pleaseenterconfirmationcodesenttoyourmobilenumber));

                    // _dialogService.ShowMessageBox(AppResources.Pleaseenterconfirmationcodesenttoyourmobilenumber, AppResources.Information);

                }
            });


            OnResendOTPClicked = new Command(async () =>
            {
                if (IsResendOTPEnabled)
                {

                    await SendOTPToRegisterMobileNumber(SelectedFbNum, "");
                }

            });


            ReqInstalmentBtnTapped = new Command(async() =>
            {
              await  CheckDueInvoicesAsync();
            });


            SummaryattachmentsListViewTapCommand = new Command<object>(async(obj) =>
            {
                IsLoading = true;
                var item = (obj as Syncfusion.Maui.ListView.ItemTappedEventArgs).DataItem as ZakatListModel;

                if (item != null)
                {


                    if (item.statusType == "E0013")
                    {
                        App.selectedZakatItem = item.fbNum;
                        await _navigationService.NavigateTo(App.ZakatInstalmentPlanPageView);
                    }
                    else
                    {

                        SelectedFbNum = item.fbNum;
                        var index = ZakatListData.IndexOf(item);

                        await GetSummaryDetailsClickedAsync(index);
                        EnableZakatInstalmentSummary();
                    }
                }
                IsLoading = false;
            });

            OutletDecisionOptionsListViewTapCommand = new Command<object>(async(obj) =>
            {
                IsLoading = true;
                var selectedItem = (obj as Syncfusion.Maui.ListView.ItemTappedEventArgs).DataItem as InstalmentPlanModel;
                if (OutletDecisionOptions.IndexOf(selectedItem) == 0)
                {
                    EnableCreateZakatInstalment();
                    await GetZakatInstalmentPlanList();

                }
                else if (OutletDecisionOptions.IndexOf(selectedItem) == 1)
                {
                    EnableRevokZakatInstalment();
                    await GetZakatRevokList();
                }

                IsLoading = false;
            });

            ApproveButtonTapped = new Command(async (Object Item) =>
            {
                ZakatListObjec = Item as ZakatListModel;

                await GetZakatInstalmentData(1, "");

            });

            RejecteButtonTapped = new Command(async (Object Item) =>
            {
                ZakatListObjec = Item as ZakatListModel;
               await MopupService.Instance.PushAsync(new ZakatRejectionReasonPopupPageView());
            });


            SummaryRevokeBtnTapped = new Command(async () =>
            {
                var result = await Application.Current.MainPage.DisplayAlert(AppResources.VatRefundsConfirmationTit, AppResources.ZakatRevokConfirmationText, AppResources.ZZCancel, AppResources.CRContinue);
                if (!result)
                {
                    App.TP = null;
                    EnableNotePage();
                }

            });

            NoteContinueTapped = new Command(async () =>
            {
                await ValidateNoteAndContinueAsync();
            });

            ZDownloadForm = new Command(async () =>
            {
                IsLoading = true;
                if (SelectedFbNum != null)
                {
                    String downloadurl = ZATCAConstants.ZakatdownloadCoverFormFile + SelectedFbNum;
                    //await WebServiceManager.FileDownload(downloadurl, "pdf");
                  await  _navigationService.NavigateTo(App.PdfView, downloadurl);

                }
                IsLoading = false;



            });

            Download_Acknowledgement = new Command(async () =>
            {
                IsLoading = true;
                if (SelectedFbNum != null)
                {
                    String downloadurl = ZATCAConstants.ZOdownloadAckLetter + SelectedFbNum;
                    //await WebServiceManager.FileDownload(downloadurl, "pdf");
                  await  _navigationService.NavigateTo(App.PdfView, downloadurl);



                }
                IsLoading = false;


            });
        }


        private ZakatInstalmentPlanResponse _zakatInstalmentsData;
        public ZakatInstalmentPlanResponse ZakatInstalmentsData
        {

            get
            {
                return _zakatInstalmentsData;
            }
            set
            {
                if (_zakatInstalmentsData == value) return;

                _zakatInstalmentsData = value;
                OnPropertyChanged("ZakatInstalmentsData");
            }
        }

        private List<ZakatInvoicesResult> _zakatInvoicesList;
        public List<ZakatInvoicesResult> ZakatInvoicesList
        {
            get
            {
                return _zakatInvoicesList;
            }
            set
            {
                if (_zakatInvoicesList == value) return;

                _zakatInvoicesList = value;
                OnPropertyChanged("ZakatInvoicesList");
            }
        }


        public async Task GetZaktaInvoiceList(int cmdType, string offset)
        {
            try
            {
                selectedList.Clear();

                IsLoading = true;
                try
                {
                    ZakatInvoiceList invoiceList = null;
                    invoiceList = await ZakatInstallmentPlanWebServiceManager.GetZakatInvoicesList(IsZakat, ZakatListObjec.fbNum);
                    if (invoiceList != null && invoiceList.d?.Count > 0)
                    {
                        ZakatInvoicesList = invoiceList.d;
                        for (int i = 0; i < ZakatInvoicesList.Count; i++)
                        {
                            if (ZakatInvoicesList[i].Abtyp.Equals("ITAX"))
                            {
                                ZakatInvoicesList[i].Abtyp = AppResources.ZakatInstalmetSelectTypeIncomeTax;
                            }
                            else if (ZakatInvoicesList[i].Abtyp.Equals("ZAKT"))
                            {
                                ZakatInvoicesList[i].Abtyp = AppResources.FORM5Zakat;
                            }

                            DateTime dateStart = new DateTime();
                            CultureInfo cultureInfo = new CultureInfo("ar-SA");
                            string apiDate = @"""" + ZakatInvoicesList[i].DueDt + @"""";
                            dateStart = JsonConvert.DeserializeObject<DateTime>(apiDate);

                            GregorianCalendar hjCalendar = new GregorianCalendar();
                            int year = hjCalendar.GetYear(dateStart);
                            int month = hjCalendar.GetMonth(dateStart);
                            int day = hjCalendar.GetDayOfMonth(dateStart);

                            string dateStr = string.Format("{0:00}/{1}/{2}", day, month, year);

                            ZakatInvoicesList[i].DueDt = dateStr;

                            string dt1 = string.Empty;
                            string[] dts = null;
                            dts = ZakatInvoicesList[i].DueDt.Split('/');
                            dt1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];
                            ZakatInvoicesList[i].DueDt = dt1;


                            if (ZakatInvoicesList[i].InvCb == "X")
                            {
                                selectedList.Add(ZakatInvoicesList[i]);
                            }
                        }
                        await ApproveSubmitClicked(cmdType, offset);
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

            }
            catch (GAZTVATRegistrationInProcessException ex)
            {

                IsLoading = false;
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();

            }
            catch (Exception )
            {
                IsLoading = false;
                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                _navigationService.GoBack();
            }
        }

        public ZakatInstalmentPlanRequest BuildRequestObject(int cmdType, string offset)
        {
            ZakatInstalmentPlanRequest _postData = new ZakatInstalmentPlanRequest();

            _postData.Fbguid = App.LoginDataRetrieved.FbGuid;
            _postData.UserTyp = "TP";
            _postData.TxnTp = ZakatInstalmentsData.d.TxnTp;
            _postData.MobNo = ZakatInstalmentsData.d.MobNo;
            _postData.FormGuid = "";
            _postData.Fbnum = ZakatListObjec.fbNum;
            _postData.DataVersion = ZakatInstalmentsData.d.DataVersion;
            _postData.DownPayReq = ZakatInstalmentsData.d.DownPayReq;
            _postData.OfcReason = ZakatInstalmentsData.d.OfcReason;
            //to approve
            if (cmdType == 1)
            {
                _postData.Operation = "02";
            }
            //to reject
            else
            {
                _postData.Operation = "03";
            }

            _postData.Euser = "";
            _postData.StepNumber = ZakatInstalmentsData.d.StepNumber;
            _postData.Email = ZakatInstalmentsData.d.Email;
            _postData.Officer = ZakatInstalmentsData.d.Officer;
            _postData.Langz = GetLangZParameterAREN();
            _postData.Status = ZakatInstalmentsData.d.Status;
            _postData.SuAmt = ZakatInstalmentsData.d.SuAmt;
            _postData.SuAmtFg = ZakatInstalmentsData.d.SuAmtFg;
            _postData.Formproc = ZakatInstalmentsData.d.Formproc;
            _postData.Tin = App.LoginDataRetrieved.TIN;
            _postData.Periodkey = ZakatInstalmentsData.d.Periodkey;
            _postData.ReturnId = ZakatInstalmentsData.d.ReturnId;
            _postData.TinNm = ZakatInstalmentsData.d.TinNm;
            _postData.AltMobNo = ZakatInstalmentsData.d.AltMobNo;
            _postData.InstReqFor = ZakatInstalmentsData.d.InstReqFor;
            _postData.InstReqReason = ZakatInstalmentsData.d.InstReqReason;
            _postData.TotAmt = ZakatInstalmentsData.d.TotAmt;
            _postData.DpAmt = ZakatInstalmentsData.d.DpAmt;
            _postData.PymntFreq = ZakatInstalmentsData.d.PymntFreq;
            _postData.PlanDur = ZakatInstalmentsData.d.PlanDur;
            _postData.OffPymntFreq = ZakatInstalmentsData.d.OffPymntFreq;
            _postData.OffPlanDur = ZakatInstalmentsData.d.OffPlanDur;
            _postData.DecCb = ZakatInstalmentsData.d.DecCb;
            _postData.Waers = ZakatInstalmentsData.d.Waers;
            _postData.AccMethod = ZakatInstalmentsData.d.AccMethod;
            _postData.Sopbel = "";
            _postData.OffAmt = ZakatInstalmentsData.d.OffAmt;
            _postData.PaymtDt = ZakatInstalmentsData.d.PaymtDt;
            _postData.PenlAmt = ZakatInstalmentsData.d.PenlAmt;
            _postData.InsDtOff = ZakatInstalmentsData.d.InsDtOff;

            if (ZakatInstalmentsData.d.NotesSet == null)
            {

                _postData.NotesSet = new List<NotesSet>();
            }
            else
            {
                _postData.NotesSet = ZakatInstalmentsData.d.NotesSet;
            }

            if (cmdType.Equals("2"))
            {
                _postData.NotesSet[0].Rcodez = "IPRA_APPRV";
                _postData.NotesSet[0].Tdline = "";
            }
            else if (cmdType.Equals("1"))
            {
                _postData.NotesSet[0].Rcodez = "IPRA_REJEC";
                _postData.NotesSet[0].Tdline = offset;
            }


            if (ZakatInstalmentsData.d.insPlanSet == null)
            {

                _postData.insPlanSet = new Array[0];

            }
            else
            {
                _postData.insPlanSet = ZakatInstalmentsData.d.insPlanSet.ToArray();
            }
            if (ZakatInstalmentsData.d.AttachSet == null)
            {
                _postData.AttachSet = new Array[0];
            }
            else
            {
                //_postData.AttachSet = ZakatInstalments.d.AttachSet.results.ToArray();
                _postData.AttachSet = new Array[0];
            }

            if (ZakatInstalmentsData.d.insPlan_OffSet == null)
            {

                _postData.insPlan_OffSet = new Array[0];

            }
            else
            {
                _postData.insPlan_OffSet = ZakatInstalmentsData.d.insPlan_OffSet.ToArray();

            }

            if (ZakatInstalmentsData.d.retmsgSet== null)
            {
                _postData.retmsgSet = new Array[0];
            }
            else
            {
                _postData.retmsgSet = ZakatInstalmentsData.d.retmsgSet.ToArray();
            }
            if (ZakatInstalmentsData.d.FnDtlSet == null || ZakatInstalmentsData.d.Operation == "04")
            {
                _postData.FnDtlSet = new List<ZAKATRequestPlanModel.FnDtlSetObject>();
            }
            else
            {
                _postData.FnDtlSet = ZakatInstalmentsData.d.FnDtlSet;

            }

            for (int i = 0; i < selectedList.Count; i++)
            {
                var dataItem = selectedList[i] as ZakatInvoicesResult;


                int index = ZakatInvoicesList.ToList().FindIndex(item => item.InvNo == dataItem.InvNo);

                if (ZakatInvoicesList[i].Abtyp.Equals(AppResources.ZakatInstalmetSelectTypeIncomeTax))
                {
                    ZakatInvoicesList[i].Abtyp = "ITAX";
                }
                else if (ZakatInvoicesList[i].Abtyp.Equals(AppResources.FORM5Zakat))
                {
                    ZakatInvoicesList[i].Abtyp = "ZAKT";
                }
                ZakatInvoicesList[index].InvCb = "X";
            }
            if (ZakatInvoicesList != null)
            {
                var invoicesList = ZakatInvoicesList.ToList();
                if (invoicesList.Count != 0)
                {
                    string apiDate = invoicesList[0].DueDt;
                    if (!apiDate.Contains("Date"))
                    {
                        for (int i = 0; i < invoicesList.Count; i++)
                        {
                            DateTime dt = Convert.ToDateTime(invoicesList[i].DueDt);
                            dt = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                            JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                            {
                                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                            };
                            var jsonDateTime = JsonConvert.SerializeObject(dt.Date, microsoftDateFormatSettings);
                            string[] dateList = jsonDateTime.Split('+');
                            jsonDateTime = dateList[0].Replace("\"\\", "");
                            var t = jsonDateTime.Replace("\\/\"", "");
                            t = t + "/";
                            invoicesList[i].DueDt = t;

                        }
                    }
                    _postData.invDtlsSet = invoicesList.ToArray();
                }
                else
                {
                    _postData.invDtlsSet = new ZakatInvoicesResult[0];
                }

            }
            else
            {
                _postData.invDtlsSet = new ZakatInvoicesResult[0];
            }
            return _postData;
        }


        public async Task ApproveSubmitClicked(int cmdType, string offset)
        {
            ZakatInstalmentPlanResponse response = new ZakatInstalmentPlanResponse();
            ZakatInstalmentPlanRequest request = new ZakatInstalmentPlanRequest();

            try
            {
                IsLoading = true;

                request = BuildRequestObject(cmdType, offset);

                response = await ZakatInstallmentPlanWebServiceManager.SaveZakatInstalmentData(request);
               await PopToRootPage();
                if (response != null)
                {
                    if (cmdType == 1)
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(String.Format(AppResources.ZakatInstalmentApproved, ZakatListObjec.fbNum)));
                        _navigationService.GoBack();

                    }
                    else
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(String.Format(AppResources.ZakatInstalmentRejected, ZakatListObjec.fbNum)));
                        _navigationService.GoBack();
                    }
                    try
                    {
                        if (response != null)
                        {
                        }
                        IsLoading = false;
                    }
                    catch (Exception )
                    {
                        IsLoading = false;

                    }
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
               
            }

        }


        public async Task GetZakatInstalmentData(int cmdType, string offset)
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
                    ZakatInstalmentsData = null;
                    try
                    {
                        ZakatInstalmentsData = await ZakatInstallmentPlanWebServiceManager.GetZakatInstalmentPostData(ZakatListObjec.fbNum);

                        if (ZakatInstalmentsData != null)
                        {
                            await GetZaktaInvoiceList(cmdType, offset);
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
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            IsLoading = false;
                            _navigationService.GoBack();
                        });
                    }
                    catch (InternetException ex)
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
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

            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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
        public async Task onPageLoad()
        {
            try
            {
                IsLoading = true;
                try
                {
                    await PopToRootPage();

                    result = await ZakatInstallmentPlanWebServiceManager.GAZTGetZakatInstalmentValidateNewReq();
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


        public ObservableCollection<EvtNotif12SetResult> _DueInvoicesListSet12 { get; set; }
        public ObservableCollection<EvtNotif12SetResult> DueInvoicesListSet12
        {
            get { return _DueInvoicesListSet12; }
            set
            {
                if (_DueInvoicesListSet12 == value)
                {
                    return;
                }


                _DueInvoicesListSet12 = value;
                OnPropertyChanged("DueInvoicesListSet12");
            }
        }

        public async Task CheckDueInvoicesAsync()
        {
            IsLoading = true;
           result = await ZakatInstallmentPlanWebServiceManager.GAZTGetZakatInstalmentValidateNewReq();
           // result = await WebServiceManager.GAZTGetZakatInstalmentValidateNewReq();
            DueInvoicesList = null;
            DueInvoicesListSet12 = null;

            var dueInvoicesList = new ObservableCollection<ZakatInstalmentValidateNewRequestModel.Result2>();
            var dueInvoicesListSet12 = new ObservableCollection<EvtNotif12SetResult>();
            if (result != null && result.d != null)
            {
                if (result.d.EvtNotif1Set != null && result.d.EvtNotif1Set.Count > 0)
                {

                    foreach (ZakatInstalmentValidateNewRequestModel.Result2 result2 in result.d.EvtNotif1Set)
                    {

                        DateTime dateStart = new DateTime();
                        DateTime dateStart2 = new DateTime();
                        CultureInfo cultureInfo = new CultureInfo("ar-SA");
                        string apiDate = @"""" + result2.Abrzo + @"""";
                        string apiDate2 = @"""" + result2.Abrzu + @"""";
                        dateStart = JsonConvert.DeserializeObject<DateTime>(apiDate);
                        dateStart2 = JsonConvert.DeserializeObject<DateTime>(apiDate2);

                        GregorianCalendar hjCalendar = new GregorianCalendar();
                        int year = hjCalendar.GetYear(dateStart);
                        int month = hjCalendar.GetMonth(dateStart);
                        int day = hjCalendar.GetDayOfMonth(dateStart);
                        int year2 = hjCalendar.GetYear(dateStart2);
                        int month2 = hjCalendar.GetMonth(dateStart2);
                        int day2 = hjCalendar.GetDayOfMonth(dateStart2);

                        string dateStr = string.Format("{0:00}/{1}/{2}", day, month, year);
                        string dateStr2 = string.Format("{0:00}/{1}/{2}", day2, month2, year2);

                        result2.Abrzo = dateStr;
                        result2.Abrzu = dateStr2;

                        string dt1 = string.Empty;
                        string[] dts = null;
                        dts = result2.Abrzo.Split('/');
                        dt1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];
                        result2.Abrzo = dt1;

                        string dt2 = string.Empty;
                        string[] dts2 = null;
                        dts2 = result2.Abrzu.Split('/');
                        dt2 = dts2[0] + "-" + UtilityManager.GetShortMonthName(dts2[1]) + "-" + dts2[2];
                        result2.Abrzu = dt2;

                        result2.Betrh = string.Format("{0:N2}", double.Parse(result2.Betrh.Replace("SAR", "").Replace("ريال سعودي", ""))) + " " + AppResources.ZSAR;

                        dueInvoicesList.Add(result2);
                    }
                    DueInvoicesList = dueInvoicesList;
                    EnableDueInvoicesPage();
                }
                else if (result.d.EvtNotif12Set != null && result.d.EvtNotif12Set.Count > 0)
                {
                    foreach (EvtNotif12SetResult evtNotif12SetResult in result.d.EvtNotif12Set)
                    {

                        DateTime dateStart = new DateTime();
                        DateTime dateStart2 = new DateTime();
                        CultureInfo cultureInfo = new CultureInfo("ar-SA");
                        string apiDate = @"""" + evtNotif12SetResult.Faedn + @"""";
                        string apiDate2 = @"""" + evtNotif12SetResult.Cdate + @"""";
                        dateStart = JsonConvert.DeserializeObject<DateTime>(apiDate);
                        dateStart2 = JsonConvert.DeserializeObject<DateTime>(apiDate2);

                        GregorianCalendar hjCalendar = new GregorianCalendar();
                        int year = hjCalendar.GetYear(dateStart);
                        int month = hjCalendar.GetMonth(dateStart);
                        int day = hjCalendar.GetDayOfMonth(dateStart);
                        int year2 = hjCalendar.GetYear(dateStart2);
                        int month2 = hjCalendar.GetMonth(dateStart2);
                        int day2 = hjCalendar.GetDayOfMonth(dateStart2);

                        string dateStr = string.Format("{0:00}/{1}/{2}", day, month, year);
                        string dateStr2 = string.Format("{0:00}/{1}/{2}", day2, month2, year2);

                        evtNotif12SetResult.Faedn = dateStr;
                        evtNotif12SetResult.Cdate = dateStr2;

                        string dt1 = string.Empty;
                        string[] dts = null;
                        dts = evtNotif12SetResult.Faedn.Split('/');
                        dt1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];
                        evtNotif12SetResult.Faedn = dt1;

                        string dt2 = string.Empty;
                        string[] dts2 = null;
                        dts2 = evtNotif12SetResult.Cdate.Split('/');
                        dt2 = dts2[0] + "-" + UtilityManager.GetShortMonthName(dts2[1]) + "-" + dts2[2];
                        evtNotif12SetResult.Cdate = dt2;

                        dueInvoicesListSet12.Add(evtNotif12SetResult);
                    }
                    DueInvoicesListSet12 = dueInvoicesListSet12;
                    EnableDueInvoicesPage();
                }
                else
                {
                    App.selectedZakatItem = "";
                    _navigationService.NavigateTo(App.ZakatInstalmentPlanPageView);
                }

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

        public void ResetData()
        {
            NumberOfInstalmentPlans = "" + AppResources.ZakatInstalmetPlan;
            App.selectedZakatItem = "";

        }

        private bool _isZakat = false;
        public bool IsZakat
        {
            get
            {
                return _isZakat;
            }
            set
            {
                if (_isZakat == value) return;
                _isZakat = value;
                OnPropertyChanged("IsZakat");
            }
        }

        private string _zakatTitle = AppResources.ZakatInstalmetSelectTypeZakat;
        public string ZakatTitle
        {
            get
            {
                return _zakatTitle;
            }
            set
            {
                if (_zakatTitle == value) return;

                _zakatTitle = value;
                OnPropertyChanged("ZakatTitle");
            }
        }

        private ZakatInstalmentPlanRevokeResponse _zakatInstalments;
        public ZakatInstalmentPlanRevokeResponse ZakatInstalments
        {
            get
            {
                return _zakatInstalments;
            }
            set
            {
                if (_zakatInstalments == value) return;

                _zakatInstalments = value;
                OnPropertyChanged("ZakatInstalments");
            }
        }

        private string _noteEditor;
        public string NoteEditor
        {
            get
            {
                return _noteEditor;
            }
            set
            {
                if (_noteEditor == value) return;

                _noteEditor = value;
                OnPropertyChanged("NoteEditor");
            }
        }
        private bool _IsDueBillsVisible = false;
        public bool IsDueBillsVisible
        {
            get
            {
                return _IsDueBillsVisible;
            }
            set
            {
                if (_IsDueBillsVisible == value) return;

                _IsDueBillsVisible = value;
                OnPropertyChanged("IsDueBillsVisible");
            }
        }


        #region Views Enabling
        public void EnableZakatLandingPage()
        {
            AddOutletDecisionOptions();
            IsDueBillsVisible = false;
            IsZakatLandingPageVisible = true;
            CreateZakatInstalmentBtnVisible = false;
            IsZakatSummaryVisible = false;
            IsRevokZakatInstalmentVisible = false;
            IsOTPPageVisible = false;
            IsNoteViewVisible = false;



        }
        public void EnableDueInvoicesPage()
        {



            IsDueBillsVisible = true;
            IsZakatLandingPageVisible = false;
            CreateZakatInstalmentBtnVisible = false;
            IsZakatSummaryVisible = false;
            IsRevokZakatInstalmentVisible = false;
            IsOTPPageVisible = false;
            IsNoteViewVisible = false;



        }
        public void EnableNotePage()
        {
            IsDueBillsVisible = false;
            IsNoteViewVisible = true;
            IsOTPPageVisible = false;
            IsZakatLandingPageVisible = false;
            CreateZakatInstalmentBtnVisible = false;
            IsZakatSummaryVisible = false;
            IsRevokZakatInstalmentVisible = false;
        }



        public void EnableOTPPage()
        {
            IsDueBillsVisible = false;
            IsOTPPageVisible = true;
            IsZakatLandingPageVisible = false;
            CreateZakatInstalmentBtnVisible = false;
            IsZakatSummaryVisible = false;
            IsRevokZakatInstalmentVisible = false;
            IsNoteViewVisible = false;



            // StartOTPTimer();
        }
        public void EnableCreateZakatInstalment()
        {
            IsDueBillsVisible = false;
            IsOTPPageVisible = false;
            IsZakatLandingPageVisible = false;
            CreateZakatInstalmentBtnVisible = true;
            IsZakatSummaryVisible = false;
            IsRevokZakatInstalmentVisible = false;
            IsNoteViewVisible = false;
        }
        public void EnableZakatInstalmentSummary()
        {
            IsDueBillsVisible = false;
            IsZakatLandingPageVisible = false;
            CreateZakatInstalmentBtnVisible = false;
            IsZakatSummaryVisible = true;
            IsRevokZakatInstalmentVisible = false;
            IsOTPPageVisible = false;
            IsNoteViewVisible = false;
        }
        public void EnableRevokZakatInstalment()
        {
            IsDueBillsVisible = false;
            IsZakatLandingPageVisible = false;
            CreateZakatInstalmentBtnVisible = false;
            IsZakatSummaryVisible = false;
            IsRevokZakatInstalmentVisible = true;
            IsOTPPageVisible = false;
            IsNoteViewVisible = false;
        }

        #endregion

        private string _LblCountDownTimer;
        public string LblCountDownTimer
        {
            get
            {
                return _LblCountDownTimer;
            }
            set
            {
                if (_LblCountDownTimer == value) return;

                _LblCountDownTimer = value;
                OnPropertyChanged("LblCountDownTimer");
            }
        }

        private string _oTPFirstDigit;
        public string OTPFirstDigit
        {
            get
            {
                return _oTPFirstDigit;
            }
            set
            {
                if (_oTPFirstDigit == value) return;

                _oTPFirstDigit = value;
                if (!string.IsNullOrEmpty(OTPFirstDigit))
                {
                    bool isNumberEntered = CheckOnlyNumber(OTPFirstDigit[0]);
                    if (!isNumberEntered)
                    {
                        OTPFirstDigit = string.Empty;
                    }
                }

                OnPropertyChanged("OTPFirstDigit");
            }
        }

        private string _OTPSecondDigit;
        public string OTPSecondDigit
        {
            get
            {
                return _OTPSecondDigit;
            }
            set
            {
                if (_OTPSecondDigit == value) return;

                _OTPSecondDigit = value;
                if (!string.IsNullOrEmpty(OTPSecondDigit))
                {
                    bool isNumberEntered = CheckOnlyNumber(OTPSecondDigit[0]);
                    if (!isNumberEntered)
                    {
                        OTPSecondDigit = string.Empty;
                    }
                }
                OnPropertyChanged("OTPSecondDigit");
            }
        }

        private string _OTPThirdDigit;
        public string OTPThirdDigit
        {
            get
            {
                return _OTPThirdDigit;
            }
            set
            {
                if (_OTPThirdDigit == value) return;

                _OTPThirdDigit = value;
                if (!string.IsNullOrEmpty(OTPThirdDigit))
                {
                    bool isNumberEntered = CheckOnlyNumber(OTPThirdDigit[0]);
                    if (!isNumberEntered)
                    {
                        OTPThirdDigit = string.Empty;
                    }
                }
                OnPropertyChanged("OTPThirdDigit");
            }
        }

        private string _OTPFourthDigit;
        public string OTPFourthDigit
        {
            get
            {
                return _OTPFourthDigit;
            }
            set
            {
                if (_OTPFourthDigit == value) return;

                _OTPFourthDigit = value;
                if (!string.IsNullOrEmpty(OTPFourthDigit))
                {
                    bool isNumberEntered = CheckOnlyNumber(OTPFourthDigit[0]);
                    if (!isNumberEntered)
                    {
                        OTPFourthDigit = string.Empty;
                    }
                }
                OnPropertyChanged("OTPFourthDigit");
            }
        }

        private bool CheckOnlyNumber(char letter)
        {
            if (letter >= 48 && letter <= 57)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool _continueButtonEnability = true;
        public bool ContinueButtonEnability
        {
            get
            {
                return _continueButtonEnability;
            }
            set
            {
                if (_continueButtonEnability == value) return;

                _continueButtonEnability = value;

                OnPropertyChanged(nameof(ContinueButtonEnability));
            }
        }
        private string _selectedFrequencyName = "";
        public string SelectedFrequencyName
        {
            get
            {
                return _selectedFrequencyName;
            }
            set
            {
                if (_selectedFrequencyName == value) return;

                _selectedFrequencyName = value;
                OnPropertyChanged("SelectedFrequencyName");
            }
        }

        private bool _isResendOTPEnabled = false;
        public bool IsResendOTPEnabled
        {
            get
            {
                return _isResendOTPEnabled;
            }
            set
            {
                if (_isResendOTPEnabled == value) return;

                _isResendOTPEnabled = value;
                // OnResendOTPClicked.ChangeCanExecute();
                OnPropertyChanged("IsResendOTPEnabled");
            }
        }


        private void OnCountDownTimedOTPEvent(object sender, ElapsedEventArgs e)
        {
            countDownSeconds--;

            /*if (countDownSeconds <= 9)
                LblCountDownTimer = "0:0" + countDownSeconds.ToString();
            else
                LblCountDownTimer = "0:" + countDownSeconds.ToString();*/


            if (countDownSeconds <= 9)
                LblCountDownTimer = "0:0" + countDownSeconds.ToString();
            else if (countDownSeconds > 60)
            {
                int countDownSecondsL = countDownSeconds - 60;
                LblCountDownTimer = "1:" + countDownSecondsL.ToString();

                if (countDownSecondsL <= 9)
                    LblCountDownTimer = "1:0" + countDownSecondsL.ToString();
            }
            else
                LblCountDownTimer = "0:" + countDownSeconds.ToString();


            // Stop timer
            if (countDownSeconds == 0)
            {
                ContinueButtonEnability = false;
                IsResendOTPEnabled = true;

                otpTimer.Stop();
            }
        }

        public void StartOTPTimer()
        {
            // Timer            
            otpTimer = new System.Timers.Timer();
            otpTimer.Interval = 1000;

            // Event
            otpTimer.Elapsed += OnCountDownTimedOTPEvent;

            countDownSeconds = 120;
            LblCountDownTimer = "0." + countDownSeconds.ToString();

            otpTimer.Enabled = true;
        }


        public async Task ValidateNoteAndContinueAsync()
        {
            if (NoteEditor.Length > 0)
            {
                await validateZakatRevoke();
            }
            else
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZakatRevokCannotLeaveEmpty, AppResources.Information);
                });
            }
        }


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
                OnPropertyChanged("IsLoading");
            }
        }


       
        private bool _isSummaryAttachmentsVisible = false;
        public bool IsSummaryAttachmentsVisible
        {
            get
            {
                return _isSummaryAttachmentsVisible;
            }
            set
            {
                if (_isSummaryAttachmentsVisible == value) return;

                _isSummaryAttachmentsVisible = value;
                OnPropertyChanged("IsSummaryAttachmentsVisible");
            }
        }

        private bool _isShowApproveView = false;
        public bool isShowApproveView
        {
            get
            {
                return _isShowApproveView;
            }
            set
            {
                if (_isShowApproveView == value) return;

                _isShowApproveView = value;
                OnPropertyChanged("isShowApproveView");
            }
        }

        public bool IsZakatLandingPageVisible
        {
            get
            {
                return _isZakatLandingPageVisible;
            }
            set
            {
                if (_isZakatLandingPageVisible == value) return;

                _isZakatLandingPageVisible = value;
                OnPropertyChanged("IsZakatLandingPageVisible");
            }
        }


        public bool CreateZakatInstalmentBtnVisible
        {
            get
            {
                return _createZakatInstalmentBtnVisible;
            }
            set
            {
                if (_createZakatInstalmentBtnVisible == value) return;

                _createZakatInstalmentBtnVisible = value;
                OnPropertyChanged("CreateZakatInstalmentBtnVisible");
            }
        }


        public bool IsOTPPageVisible
        {
            get
            {
                return _isOTPPageVisible;
            }
            set
            {
                if (_isOTPPageVisible == value) return;

                _isOTPPageVisible = value;
                OnPropertyChanged("IsOTPPageVisible");
            }
        }
        public bool IsZakatSummaryVisible
        {
            get
            {
                return _IsZakatSummaryVisible;
            }
            set
            {
                if (_IsZakatSummaryVisible == value) return;

                _IsZakatSummaryVisible = value;
                OnPropertyChanged("IsZakatSummaryVisible");
            }
        }


        public bool IsZakatSummaryRevokeVisible
        {
            get
            {
                return _isZakatSummaryRevokeVisible;
            }
            set
            {
                if (_isZakatSummaryRevokeVisible == value) return;

                _isZakatSummaryRevokeVisible = value;
                OnPropertyChanged("IsZakatSummaryRevokeVisible");
            }
        }

        public bool IsRevokZakatInstalmentVisible
        {
            get
            {
                return _isRevokZakatInstalmentVisible;
            }
            set
            {
                if (_isRevokZakatInstalmentVisible == value) return;

                _isRevokZakatInstalmentVisible = value;
                OnPropertyChanged("IsRevokZakatInstalmentVisible");
            }
        }

        public bool _isNoteViewVisible = false;
        public bool IsNoteViewVisible
        {
            get
            {
                return _isNoteViewVisible;
            }
            set
            {
                if (_isNoteViewVisible == value) return;

                _isNoteViewVisible = value;
                OnPropertyChanged("IsNoteViewVisible");
            }
        }

        public ObservableCollection<ZakatInstalmentValidateNewRequestModel.Result2> _DueInvoicesList { get; set; }
        public ObservableCollection<ZakatInstalmentValidateNewRequestModel.Result2> DueInvoicesList
        {
            get { return _DueInvoicesList; }



            set
            {
                if (_DueInvoicesList == value)
                {
                    return;
                }



                _DueInvoicesList = value;
                OnPropertyChanged("DueInvoicesList");
            }
        }

        public ObservableCollection<InstalmentPlanModel> outletDecisionOptions { get; set; }
        public ObservableCollection<InstalmentPlanModel> OutletDecisionOptions
        {
            get
            {
                return outletDecisionOptions;
            }

            set
            {
                if (outletDecisionOptions == value)
                {
                    return;
                }

                outletDecisionOptions = value;
                OnPropertyChanged("OutletDecisionOptions");
            }
        }

        //        ReqVatInstalmentPlanResponse reqVatInstalmentPlanResponse;
        private Models.ZakatInstalationModels.ZakatInstalmentPlanRequestListModel _zakatInstalmentPlanRequestListModel;
        public Models.ZakatInstalationModels.ZakatInstalmentPlanRequestListModel ZakatInstalmentPlanRequestListModel
        {
            get
            {
                return _zakatInstalmentPlanRequestListModel;
            }
            set
            {
                if (_zakatInstalmentPlanRequestListModel == value) return;

                _zakatInstalmentPlanRequestListModel = value;
                OnPropertyChanged("ZakatInstalmentPlanRequestListModel");
            }
        }
        private string _paymentFrequency = "";
        public string PaymentFrequency
        {
            get
            {
                return _paymentFrequency;
            }
            set
            {
                if (_paymentFrequency == value) return;

                _paymentFrequency = value;
                OnPropertyChanged("PaymentFrequency");
            }
        }


        private string _noOfInstalments = "0";
        public string NoOfInstalments
        {
            get
            {
                return _noOfInstalments;
            }
            set
            {
                if (_noOfInstalments == value) return;

                _noOfInstalments = value;
                OnPropertyChanged("NoOfInstalments");
            }
        }
        private string _instalmentAmount = "0.00 SAR";
        public string InstalmentAmount
        {
            get
            {
                return _instalmentAmount;
            }
            set
            {
                if (_instalmentAmount == value) return;

                _instalmentAmount = value;
                OnPropertyChanged("InstalmentAmount");
            }
        }
        private string _penaltyAmount = "0.00 SAR";
        public string PenaltyAmount
        {
            get
            {
                return _penaltyAmount;
            }
            set
            {
                if (_penaltyAmount == value) return;

                _penaltyAmount = value;
                OnPropertyChanged("PenaltyAmount");
            }
        }

        private string _instalmentsCount = "0 " + AppResources.ZakatInstalmentRequestCount;
        public string InstalmentsCount
        {
            get
            {
                return _instalmentsCount;
            }
            set
            {
                if (_instalmentsCount == value) return;

                _instalmentsCount = value;
                OnPropertyChanged("InstalmentsCount");
            }
        }

        private string _totalAmount = "0.00 SAR";
        public string TotalAmount
        {
            get
            {
                return _totalAmount;
            }
            set
            {
                if (_totalAmount == value) return;

                _totalAmount = value;
                OnPropertyChanged("TotalAmount");
            }
        }

        private string _selectedFbNum = "";
        public string SelectedFbNum
        {
            get
            {
                return _selectedFbNum;
            }
            set
            {
                if (_selectedFbNum == value) return;

                _selectedFbNum = value;
                OnPropertyChanged("SelectedFbNum");
            }
        }




        #region Lists

        public void AddOutletDecisionOptions()
        {

            var outletDecisionOptions = new ObservableCollection<InstalmentPlanModel>();
            outletDecisionOptions.Add(new InstalmentPlanModel
            {
                ActiveOutletDecisionOptions = AppResources.ZakatInstalmentPlanCreate_Display,
                ActiveOutletDecisionOptionsIsSelected = false
            });
            outletDecisionOptions.Add(new InstalmentPlanModel
            {
                ActiveOutletDecisionOptions = AppResources.ZakatInstalmentPlanRevokeZAKAT,
                ActiveOutletDecisionOptionsIsSelected = false
            });
            OutletDecisionOptions = outletDecisionOptions;
        }

        private string _numberOfInstalmentPlans = "" + AppResources.ZakatInstalmetPlan;
        public string NumberOfInstalmentPlans
        {
            get
            {
                return _numberOfInstalmentPlans;
            }
            set
            {
                if (_numberOfInstalmentPlans == value) return;

                _numberOfInstalmentPlans = value;
                OnPropertyChanged("NumberOfInstalmentPlans");
            }
        }


        private Models.ZakatInstalationModels.ZakatInstalmentPlanRequestListModel _reqVatInstalmentPlanResponseList;
        public Models.ZakatInstalationModels.ZakatInstalmentPlanRequestListModel ReqVatInstalmentPlanResponseList
        {
            get
            {
                return _reqVatInstalmentPlanResponseList;
            }
            set
            {
                if (_reqVatInstalmentPlanResponseList == value) return;

                _reqVatInstalmentPlanResponseList = value;
                OnPropertyChanged("ReqVatInstalmentPlanResponseList");
            }
        }
        private List<Models.ZakatInstalationModels.ZakatInstalmentPlanRequestListModel.Result> _requestForInstalmentPlanList;
        public List<Models.ZakatInstalationModels.ZakatInstalmentPlanRequestListModel.Result> RequestForInstalmentPlanList
        {
            get
            {
                return _requestForInstalmentPlanList;
            }
            set
            {
                if (_requestForInstalmentPlanList == value) return;

                _requestForInstalmentPlanList = value;
                OnPropertyChanged("RequestForInstalmentPlanList");
            }
        }
        private List<Models.ZakatInstalationModels.ZakatInstalmentPlanRequestListModel.Result> _RevokeListItem;
        public List<Models.ZakatInstalationModels.ZakatInstalmentPlanRequestListModel.Result> RevokeListItem
        {
            get
            {
                return _RevokeListItem;
            }
            set
            {
                if (_RevokeListItem == value) return;

                _RevokeListItem = value;
                OnPropertyChanged("RevokeListItem");
            }
        }

        private List<Models.ZakatInstalationModels.ZakatInstalmentPlanRequestListModel.RevokeListResult> _requestForRevokeList;
        public List<Models.ZakatInstalationModels.ZakatInstalmentPlanRequestListModel.RevokeListResult> RequestForRevokeList
        {
            get
            {
                return _requestForRevokeList;
            }
            set
            {
                if (_requestForRevokeList == value) return;

                _requestForRevokeList = value;
                OnPropertyChanged("RequestForRevokeList");
            }
        }


        public async void BindVatInstalments()
        {
            if (ReqVatInstalmentPlanResponseList.d.WorklistSet != null)
            {
                if (RequestForInstalmentPlanList != null)
                {

                    RequestForInstalmentPlanList.Clear();

                }

                IsZakat = Preferences.Get("isZakat", false);
                if (IsZakat)
                {

                    RequestForInstalmentPlanList = ReqVatInstalmentPlanResponseList.d.WorklistSet.Where(x => x.IptypeFg == "NZ" || x.IptypeFg == "" || x.IptypeFg == "OZ").ToList();
                }
                else
                {
                    RequestForInstalmentPlanList = ReqVatInstalmentPlanResponseList.d.WorklistSet.Where(x => x.IptypeFg == "NI" || x.IptypeFg == "").ToList();


                }


                for (int i = 0; i < RequestForInstalmentPlanList.Count; i++)
                {
                    RequestForInstalmentPlanList[i].DpAmt = string.Format("{0:N2}", ReqVatInstalmentPlanResponseList.d.WorklistSet[i].DpAmt) + " " + AppResources.FORM5SAR;
                    RequestForInstalmentPlanList[i].TotAmt = string.Format("{0:N2}", ReqVatInstalmentPlanResponseList.d.WorklistSet[i].TotAmt) + " " + AppResources.FORM5SAR;

                    string submitDate = "";
                    if (RequestForInstalmentPlanList[i].SubmitDt != null)
                    {

                        DateTime dateStart = new DateTime();
                        CultureInfo cultureInfo = new CultureInfo("ar-SA");
                        string apiDate = @"""" + RequestForInstalmentPlanList[i].SubmitDt + @"""";
                        dateStart = JsonConvert.DeserializeObject<DateTime>(apiDate);

                        GregorianCalendar hjCalendar = new GregorianCalendar();
                        int year = hjCalendar.GetYear(dateStart);
                        int month = hjCalendar.GetMonth(dateStart);
                        int day = hjCalendar.GetDayOfMonth(dateStart);

                        string dateStr = string.Format("{0:00}/{1}/{2}", day, month, year);

                        RequestForInstalmentPlanList[i].SubmitDt = dateStr;

                        string dt1 = string.Empty;
                        string[] dts = null;
                        dts = RequestForInstalmentPlanList[i].SubmitDt.Split('/');
                        dt1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];
                        submitDate = dt1;
                    }

                    var zakatbill = "";
                    if (RequestForInstalmentPlanList[i].IptypeFg == "NZ")
                    {
                        zakatbill = AppResources.ZakatInstalmetSelectTypeZakat;
                    }
                    else if (RequestForInstalmentPlanList[i].IptypeFg == "NI")
                    {
                        zakatbill = AppResources.ZakatInstalmetSelectTypeIncomeTax;
                    }
                    else
                    {

                        IsZakat = Preferences.Get("isZakat", false);
                        if (IsZakat)
                        {
                            zakatbill = AppResources.ZakatInstalmetSelectTypeZakat;

                        }
                        else
                        {
                            zakatbill = AppResources.ZakatInstalmetSelectTypeIncomeTax;

                        }


                    }





                    zakatListData.Add(new ZakatListModel()
                    {
                        referanceNumber = RequestForInstalmentPlanList[i].Fbnum,
                        status = RequestForInstalmentPlanList[i].Status,
                        dueamount = string.Format("{0:N2}", Convert.ToDouble(RequestForInstalmentPlanList[i].TotAmt.Replace("SAR", "").Replace("ريال سعودي", ""))) + " " + AppResources.ZSAR,
                        instalmentAmount = string.Format("{0:N2}", Convert.ToDouble(RequestForInstalmentPlanList[i].DueAmt.Replace("SAR", "").Replace("ريال سعودي", ""))) + " " + AppResources.ZSAR,
                        noOfInstalments = RequestForInstalmentPlanList[i].PlanDur,
                        downpayment = string.Format("{0:N2}", Convert.ToDouble(RequestForInstalmentPlanList[i].DpAmt.Replace("SAR", "").Replace("ريال سعودي", ""))) + " " + AppResources.ZSAR,
                        dateOfSubmission = submitDate,
                        Fbtyp = zakatbill,
                        statusType = RequestForInstalmentPlanList[i].Fbust,
                        fbNum = RequestForInstalmentPlanList[i].Fbnum,
                        isShowApproveView = RequestForInstalmentPlanList[i].Fbsta.Equals("IP021") && RequestForInstalmentPlanList[i].Fbust.Equals("E0077")


                    });

                }



                InstalmentsCount = RequestForInstalmentPlanList.Count + " " + AppResources.ZakatInstalmentRequestCount;

            }

            await GetZakatRevokList();

            if (ZakatListData.Count > 0)
            {
                NumberOfInstalmentPlans = ZakatListData.Count + " " + AppResources.ZakatInstalmetPlan;
            }
        }


        //public ObservableCollection<ZakatRevokeList.Result> _revokList { get; set; }
        //public ObservableCollection<ZakatRevokeList.Result> RevokList
        //{
        //    get
        //    {
        //        return _revokList;
        //    }
        //    set
        //    {
        //        if (_revokList == value)
        //        {
        //            return;
        //        }
        //        _revokList = value;
        //        OnPropertyChanged("RevokList");
        //    }
        //}
        public ObservableCollection<ZakatSelectBillModel> summarySelectedBillsList { get; set; }
        public ObservableCollection<ZakatSelectBillModel> SummarySelectedBillsList
        {
            get
            {
                return summarySelectedBillsList;
            }
            set
            {

                if (summarySelectedBillsList == value) return;

                summarySelectedBillsList = value;
                OnPropertyChanged("SummarySelectedBillsList");
            }
        }

        public ObservableCollection<ZakatListModel> zakatListData { get; set; }
        public ObservableCollection<ZakatListModel> ZakatListData
        {
            get
            {
                return zakatListData;
            }
            set
            {
                if (zakatListData == value)
                {
                    return;
                }
                zakatListData = value;
                OnPropertyChanged("ZakatListData");
            }
        }


        private Models.ZakatInstalationModels.SummaryDisplayModel _seletedZakatForm;
        public Models.ZakatInstalationModels.SummaryDisplayModel SeletedZakatForm
        {
            get
            {
                return _seletedZakatForm;
            }
            set
            {
                if (_seletedZakatForm == value) return;
                _seletedZakatForm = value;
                OnPropertyChanged("SeletedZakatForm");
            }
        }
        private string _SummaryNoOfInstalments = "";
        public string SummaryNoOfInstalments
        {
            get
            {
                return _SummaryNoOfInstalments;
            }
            set
            {
                if (_SummaryNoOfInstalments == value) return;

                _SummaryNoOfInstalments = value;
                OnPropertyChanged("SummaryNoOfInstalments");
            }
        }
        public bool IsAttachmentsViewEnabled
        {
            get
            {
                return _isAttachmentsViewEnabled;
            }
            set
            {
                if (_isAttachmentsViewEnabled == value) return;

                _isAttachmentsViewEnabled = value;
                OnPropertyChanged("IsAttachmentsViewEnabled");
            }
        }

        public ObservableCollection<SummaryDisplayModel.Result> attachments { get; set; }
        public ObservableCollection<SummaryDisplayModel.Result> Attachments
        {
            get
            {
                return attachments;
            }
            set
            {
                if (attachments == value)
                {
                    return;
                }
                attachments = value;
                OnPropertyChanged("Attachments");
            }
        }
        private string _enteredOTP = "";
        public string EnteredOTP
        {
            get
            {
                return _enteredOTP;
            }
            set
            {
                if (_enteredOTP == value) return;

                _enteredOTP = value;
                OnPropertyChanged("EnteredOTP");
            }
        }
        public void SetOTP()
        {

            EnteredOTP = OTPFirstDigit + OTPSecondDigit + OTPThirdDigit + OTPFourthDigit;

        }

        public void BindZakatSummaryData(SummaryDisplayModel zakatRequestDisplayModel, ZakatInstalmentInvListModel invoiceResult)
        {



            SummarySelectedBillsList = new ObservableCollection<ZakatSelectBillModel>();

            foreach (var bill in invoiceResult.d)
            {




                string submitDate = "";



                if (bill.DueDt != null || bill.DueDt == null)
                {
                    DateTime dateStart = new DateTime();
                    CultureInfo cultureInfo = new CultureInfo("ar-SA");
                    string apiDate = @"""" + bill.DueDt + @"""";
                    dateStart = JsonConvert.DeserializeObject<DateTime>(apiDate);



                    GregorianCalendar hjCalendar = new GregorianCalendar();
                    int year = hjCalendar.GetYear(dateStart);
                    int month = hjCalendar.GetMonth(dateStart);
                    int day = hjCalendar.GetDayOfMonth(dateStart);



                    string dateStr = string.Format("{0:00}/{1}/{2}", day, month, year);



                    bill.DueDt = dateStr;



                    string dt1 = string.Empty;
                    string[] dts = null;
                    dts = bill.DueDt.Split('/');
                    dt1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];
                    submitDate = dt1;
                }

                if (bill.Abtyp.Equals("ITAX"))
                {
                    bill.Abtyp = AppResources.ZakatInstalmetSelectTypeIncomeTax;
                }
                else if (bill.Abtyp.Equals("ZAKT"))
                {
                    bill.Abtyp = AppResources.FORM5Zakat;
                }


                SummarySelectedBillsList.Add(new ZakatSelectBillModel()
                {
                    billNumber = AppResources.Bill + (SummarySelectedBillsList.Count + 1).ToString("00"),
                    amount = bill.DueAmt,
                    saadNumber = bill.InvNo,
                    taxPeriod = bill.DueDt,
                    isSelected = false,
                    //billType = bill.Abtyp
                    billType = IsZakat ? AppResources.ZakatInstalmetSelectTypeZakat : AppResources.ZakatInstalmetSelectTypeIncomeTax



                });
            }
            // SummarySelectedBillsList = summarySelectedBillsList;




            Attachments = new ObservableCollection<SummaryDisplayModel.Result>();




            for (int i = 0; i < zakatRequestDisplayModel.d.AttachSet.Count; i++)
            {
                Attachments.Add(zakatRequestDisplayModel.d.AttachSet[i]);
            }



            // Attachments = attachments;



            if (Attachments != null && Attachments.Count > 0)
            {
                IsSummaryAttachmentsVisible = true;
            }



            TotalAmount = string.Format("{0:N2}", zakatRequestDisplayModel.d.TotAmt) + " SAR";
            InstalmentAmount = string.Format("{0:N2}", zakatRequestDisplayModel.d.DpAmt) + " SAR";
            SummaryNoOfInstalments = zakatRequestDisplayModel.d.PlanDur;



            if (zakatRequestDisplayModel.d.PymntFreq != null && zakatRequestDisplayModel.d.PymntFreq == "01")
            {
                SelectedFrequencyName = AppResources.ZakatInstalmetMonthly;
            }
            else if (zakatRequestDisplayModel.d.PymntFreq != null && zakatRequestDisplayModel.d.PymntFreq == "02")
            {
                SelectedFrequencyName = AppResources.ZakatInstalmetQuarterly;
            }
            else if (zakatRequestDisplayModel.d.PymntFreq != null && zakatRequestDisplayModel.d.PymntFreq == "03")
            {
                SelectedFrequencyName = AppResources.ZakatInstalmetHalfYearly;
            }
            else if (zakatRequestDisplayModel.d.PymntFreq != null && zakatRequestDisplayModel.d.PymntFreq == "04")
            {
                SelectedFrequencyName = AppResources.ZakatInstalmetYearly;
            }
            else
            {
                SelectedFrequencyName = AppResources.ZakatInstalmetMonthly;
            }



        }

        #endregion


        #region APIs

        public async Task GetSummaryDetailsClickedAsync(int index)
        {
            // SummarySelectedBillsList = null;

            try
            {
                IsLoading = true;
                try
                {
                    var item = zakatListData[index];

                    SeletedZakatForm = await ZakatWebServiceManager.GAZTGetZakatRequestDisplayData(item.referanceNumber, item.Fbtyp);

                    if (SeletedZakatForm != null && SeletedZakatForm.d != null)
                    {

                        var invoiceResult = await ZakatInstallmentPlanWebServiceManager.GAZTGetZakatInstalmentInvData(SeletedZakatForm.d.Fbnum);

                        if (RequestForRevokeList != null && RequestForRevokeList.Count != 0)
                        {

                            var itemsSource = RequestForRevokeList.Where(w => w.Fbnum.Contains(SeletedZakatForm.d.Fbnum)).ToList();
                            if (itemsSource.Count > 0)
                            {
                                SelectedFbNum = itemsSource[0].Fbnum;
                                IsZakatSummaryRevokeVisible = true;
                            }
                            else
                            {
                                IsZakatSummaryRevokeVisible = false;

                            }

                        }

                        NoOfInstalments = item.noOfInstalments;
                        TotalAmount = string.Format("{0:N2}", item.downpayment) + " SAR";
                        InstalmentAmount = string.Format("{0:N2}", item.dueamount) + " SAR";


                        if (item.frequency == "01")
                        {

                            SelectedFrequencyName = AppResources.ZakatInstalmetMonthly;

                        }
                        else if (item.frequency == "02")
                        {

                            SelectedFrequencyName = AppResources.ZakatInstalmetQuarterly;
                        }
                        else if (item.frequency == "03")
                        {

                            SelectedFrequencyName = AppResources.ZakatInstalmetHalfYearly;
                        }
                        else if (item.frequency == "04")
                        {
                            SelectedFrequencyName = AppResources.ZakatInstalmetYearly;
                        }



                        BindZakatSummaryData(SeletedZakatForm, invoiceResult);
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

        public async Task GetZakatInstalmentPlanList()
        {
            try
            {
                IsLoading = true;
                ReqVatInstalmentPlanResponseList = null;
                ZakatInstalmentPlanRequestListModel rEQVatInstalmentPlanResponse = null;

                try
                {
                    rEQVatInstalmentPlanResponse = await ZakatWebServiceManager.GAZTGetZakatInstalmentPlanRequestList("", "", "");
                    ReqVatInstalmentPlanResponseList = rEQVatInstalmentPlanResponse;


                   await PopToRootPage();

                    if (ReqVatInstalmentPlanResponseList != null && ReqVatInstalmentPlanResponseList.d != null)
                    {
                        ZakatListData = new ObservableCollection<ZakatListModel>();



                        BindVatInstalments();

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

        private static String GetLangZParameterAREN()
        {
            if (App.IsArabic)
                return "AR";
            else
                return "EN";
        }

        public async Task GetZakatRevokList()
        {
            try
            {
                IsLoading = true;
                try
                {

                    ZakatInstalmentPlanRequestListModel revokResult = await ZakatWebServiceManager.GAZTGetZakatRevokeList("", "", "");

                   await PopToRootPage();


                    if (revokResult != null && revokResult.d != null)
                    {

                        if (ZakatListData == null)
                        {
                            ZakatListData = new ObservableCollection<ZakatListModel>();

                        }


                        RequestForRevokeList = revokResult.d.RevokeListSet;

                        IsZakat = Preferences.Get("isZakat", false);
                        if (IsZakat)
                        {

                            RevokeListItem = revokResult.d.WorklistSet.Where(x => x.IptypeFg == "NZ").ToList();
                        }
                        else
                        {
                            RevokeListItem = revokResult.d.WorklistSet.Where(x => x.IptypeFg == "NI").ToList();

                        }


                        for (int i = 0; i < RevokeListItem.Count; i++)
                        {
                            RevokeListItem[i].DpAmt = string.Format("{0:N2}", RevokeListItem[i].DpAmt) + " " + AppResources.FORM5SAR;
                            RevokeListItem[i].TotAmt = string.Format("{0:N2}", RevokeListItem[i].TotAmt) + " " + AppResources.FORM5SAR;

                            string submitDate = "";
                            if (RevokeListItem[i].SubmitDt != null)
                            {

                                DateTime dateStart = new DateTime();
                                CultureInfo cultureInfo = new CultureInfo("ar-SA");
                                string apiDate = @"""" + RevokeListItem[i].SubmitDt + @"""";
                                dateStart = JsonConvert.DeserializeObject<DateTime>(apiDate);

                                GregorianCalendar hjCalendar = new GregorianCalendar();
                                int year = hjCalendar.GetYear(dateStart);
                                int month = hjCalendar.GetMonth(dateStart);
                                int day = hjCalendar.GetDayOfMonth(dateStart);

                                string dateStr = string.Format("{0:00}/{1}/{2}", day, month, year);

                                string dt1 = string.Empty;
                                string[] dts = null;
                                dts = dateStr.Split('/');
                                //dt1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];
                                dt1 = dts[0] + "-" + dts[1] + "-" + dts[2];
                                submitDate = dt1;
                            }

                            ZakatListData.Add(new ZakatListModel()
                            {
                                referanceNumber = RevokeListItem[i].Fbnum,
                                status = RevokeListItem[i].Status,
                                dueamount = string.Format("{0:N2}", double.Parse(RevokeListItem[i].TotAmt.Replace("SAR", "").Replace("ريال سعودي", ""))) + " " + AppResources.ZSAR,
                                instalmentAmount = string.Format("{0:N2}", double.Parse(RevokeListItem[i].DueAmt.Replace("SAR", "").Replace("ريال سعودي", ""))) + " " + AppResources.ZSAR,
                                noOfInstalments = RevokeListItem[i].PlanDur,
                                downpayment = string.Format("{0:N2}", double.Parse(RevokeListItem[i].DpAmt.Replace("SAR", "").Replace("ريال سعودي", ""))) + " " + AppResources.ZSAR,
                                dateOfSubmission = submitDate,
                                Fbtyp = Preferences.Get("isZakat", false) ? AppResources.ZakatInstalmetSelectTypeZakat : AppResources.ZakatInstalmetSelectTypeIncomeTax,
                                frequency = RevokeListItem[i].PymntFreq,
                                SelectedType = Preferences.Get("isZakat", false) ? AppResources.ZakatInstalmetSelectTypeZakat : AppResources.ZakatInstalmetSelectTypeIncomeTax,
                                statusType = RevokeListItem[i].Fbust,
                                fbNum = RevokeListItem[i].Fbnum,
                                isShowApproveView = RevokeListItem[i].Fbsta.Equals("IP021") && RevokeListItem[i].Fbust.Equals("E0077")
                            });

                            if (ZakatListData.Count > 0)
                            {
                                NumberOfInstalmentPlans = ZakatListData.Count + " " + AppResources.ZakatInstalmetPlan;
                            }

                        }

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

        public async Task validateZakatRevoke()
        {
            try
            {
                IsLoading = true;

                try
                {

                    ZakatRevokeValidateModel revokeResult = await WebServiceManager.GAZTGetZakatRevokeValidate(SelectedFbNum);

                    await PopToRootPage();

                    try
                    {
                        if (revokeResult != null && revokeResult.d != null)
                        {
                            if (revokeResult.d.Valid)
                            {

                                await SendOTPToRegisterMobileNumber(SelectedFbNum, "");


                            }


                        }
                        else
                        {
                            await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                            _navigationService.GoBack();
                        }



                        IsLoading = false;
                    }
                    catch (Exception)
                    {
                    }

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




        public async Task getZakatRevokeData()
        {
            try
            {
                IsLoading = true;
                ZakatInstalments = null;
                try
                {
                    ZakatInstalments = await ZakatInstallmentPlanWebServiceManager.GetZakatInstalmentRevokePostData(SelectedFbNum);


                    if (ZakatInstalments != null)
                    {

                        var isrevoked = await RevokeSubmitClicked();

                        if (isrevoked != null && isrevoked.d != null)
                        {

                            Preferences.Set("IsFromRevok", true);

                            Preferences.Set("RevokeRef", isrevoked.d.Fbnum.ToString());


                            await Application.Current.MainPage.Navigation.PushAsync(new ZakatInstalmentPlanSuccessPage());
                        }


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


        public async Task<ZakatInstalmentPlanRevokeResponse> RevokeSubmitClicked()
        {
            ZakatInstalmentPlanRevokeResponse response = new ZakatInstalmentPlanRevokeResponse();
            ZakatInstalmentPlanRevokeRequest request = new ZakatInstalmentPlanRevokeRequest();

            try
            {
                IsLoading = true;

                var item = ZakatInstalments.d;


                request.AccMethod = "A";
                request.AltMobNo = item.AltMobNo;
                request.DataVersion = item.DataVersion;
                request.DecCb = item.DecCb;
                request.DpAmt = item.DpAmt.Replace(" SAR", "").Replace(",", "");
                request.Email = item.Email;
                request.Euser = item.Euser;
                request.Fbguid = item.Fbguid;
                request.FbnumIprr = item.FbnumIprr;
                request.Fbnum = item.Fbnum;
                request.FormGuid = item.FormGuid;
                request.InstReqFor = item.InstReqFor;
                request.InstReqReason = item.InstReqReason;
                request.Langz = GetLangZParameter();
                request.MobNo = item.MobNo;
                request.Officer = item.Officer;
                request.OffAmt = item.OffAmt;
                request.OffPlanDur = "";
                request.OffPymntFreq = "";
                request.Formproc = item.Formproc;
                request.PaymtDt = item.PaymtDt;
                request.PenlAmt = item.PenlAmt;
                request.Periodkey = item.Periodkey;
                request.PlanDur = item.PlanDur;
                request.PymntFreq = item.PymntFreq;
                request.ReturnId = "";
                request.Sopbel = item.Sopbel;
                request.Status = "E0045";
                request.StepNumber = item.StepNumber;
                request.SuAmt = item.SuAmt;
                request.SuAmtFg = item.SuAmtFg;
                request.Tin = item.Tin;
                request.TinNm = item.TinNm;
                request.TotAmt = item.TotAmt.Replace(" SAR", "").Replace(",", "");
                request.TxnTp = "CRE_IPRR";
                request.Waers = item.Waers;
                request.AttachSet = new AttachSetResults[0];
                request.FnDtlSet = new Array[0];
                request.insPlan_OffSet = new Array[0];
                request.insPlanSet = new Array[0];
                request.invDtlsSet = new ZakatInvoicesResult[0];
                request.retmsgSet = new Array[0];
                request.UserTyp = "TP";

                request.Operation = "69";

                NotesSet notes = new NotesSet();
                if (NoteEditor != null)
                {

                    notes.Tdline = NoteEditor.ToString();

                }
                else
                {
                    notes.Tdline = "";

                }



                Metadata _metdata = new Metadata();
                _metdata.uri = "HTTP://SAPECCDEV.MYDZIT.GOV.SA:8000/sap/opu/odata/SAP/ZDP_IPRF_M_SRV/NotesSet('001')";
                _metdata.type = "ZDP_IPRF_M_SRV.Notes";
                _metdata.id = "HTTP://SAPECCDEV.MYDZIT.GOV.SA:8000/sap/opu/odata/SAP/ZDP_IPRF_M_SRV/NotesSet('001')";

                notes.__metadata = _metdata;
                notes.AttByz = "TP";
                notes.ByGpartz = App.LoginDataRetrieved.TIN;

                notes.DataVersionz = "00000";
                notes.ElemNo = 0;
                notes.Erfusrz = "";
                notes.Lineno = 1;
                notes.Noteno = "1";
                notes.Notenoz = "1";
                notes.Rcodez = "IPRR_NOTES";
                notes.Refnamez = "";
                notes.Tdformat = "";
                notes.XInvoicez = "";
                notes.XObsoletez = "";

                var noteset = new NotesSet[1];
                noteset[0] = notes;
                request.NotesSet = noteset;

                response = await ZakatInstallmentPlanWebServiceManager.SaveZakatInstalmentRevokeData(request);
                await PopToRootPage();
                if (response != null)
                {
                    try
                    {


                        IsLoading = false;
                        return response;

                    }
                    catch (Exception)
                    {
                        IsLoading = false;
                        return null;

                    }
                }
                IsLoading = false;
                return response;
            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                IsLoading = false;
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                return response;
            }

            catch (Exception)
            {
               
                return response;
            }

        }

        #endregion

        private static string GetLangZParameter()
        {
            if (App.IsArabic)
                return "AR";
            else
                return "EN";
        }

        public async Task PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                var _navigation = Application.Current.MainPage.Navigation;
                await _navigation.PopToRootAsync();
            }
        }

        private bool _Enabled;
        public bool Enabled
        {
            get { return _Enabled; }
            set
            {
                _Enabled = value;
                OnPropertyChanged("Enabled");
            }
        }
        private async Task SendOTPToRegisterMobileNumber(string fbNumber, string OTPCode)
        {

            try
            {
                IsLoading = true;
                Enabled = false;
                try
                {
                    string lang = UtilityManager.GetLanguageParameter();
                    ZakatRevokeSendSMSModel revokeOTP = await WebServiceManager.GAZTZakatRevokeSendOTP(fbNumber, OTPCode);
                    await PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                    if (revokeOTP?.results?.Count > 0)
                    {
                        var OTPItem = revokeOTP?.results[0];




                        if (OTPCode.Length == 0)
                        {
                            EnableOTPPage();
                            ContinueButtonEnability = true;
                            IsResendOTPEnabled = false;
                            StartOTPTimer();
                        }
                        else
                        {
                            if (OTPItem.ValidSms)
                            {
                                otpTimer.Stop();
                                await getZakatRevokeData();

                            }
                            else
                            { await _dialogService.ShowMessage(AppResources.InvalidOTP, AppResources.Information);
                            }


                        }
 }
                    else
                    {
                        await _dialogService.ShowMessageBox(AppResources.ZPleaseEnterAValidUserID, AppResources.ZError);
                    }

                }
                catch (Exception)
                {
                }
                IsLoading = false;
            }
            catch (InternetException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                IsLoading = false;
            }
        }

        public async Task ValidateOTPAsync()
        {

            await SendOTPToRegisterMobileNumber(SelectedFbNum, EnteredOTP);
        }
    }
}
