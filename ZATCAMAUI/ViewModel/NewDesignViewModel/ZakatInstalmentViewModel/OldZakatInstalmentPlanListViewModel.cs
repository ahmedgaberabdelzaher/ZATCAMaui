using System.Collections.ObjectModel;
using System.Globalization;
using System.Timers;
using System.Windows.Input;


using Newtonsoft.Json;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.InstalmentPlanModel;
using ZATCAMAUI.Models.ZakatInstalationModels;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel
{

    public class OldZakatInstalmentPlanListViewModel : BaseViewModel
    {
        #region Variable
        private bool _isZakatLandingPageVisible = false;
        private bool _createZakatInstalmentBtnVisible = true;
        private bool _isRevokZakatInstalmentVisible = true;
        private bool _isOTPPageVisible = false;
        private bool _IsZakatSummaryVisible = false;
        private bool _isZakatSummaryRevokeVisible = false;
        private bool _isAttachmentsViewEnabled = false;
        ZakatInstalmentValidateNewRequestModel result = null;

        public System.Timers.Timer otpTimer;
        public int countDownSeconds;

        #endregion

        #region commands
        public ICommand ReqInstalmentBtnTapped { get; set; }
        public ICommand GoBackClick { get; set; }
        public ICommand CloseClick { get; set; }
        public ICommand OutletDecisionOptionsTapCommand { get; set; }
        public ICommand SummaryattachmentsListViewItemCommand { get; set; }
        public ICommand OnContinueClick { get; set; }
        public ICommand SummaryRevokeBtnTapped { get; set; }
        public Command NoteContinueTapped { get; set; }
        public Command OnResendOTPClicked { get; set; }
        public Command Download_Acknowledgement { get; set; }
        public Command ZDownloadForm { get; set; }

        #endregion

        public OldZakatInstalmentPlanListViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

            CloseClick = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            });

            OutletDecisionOptionsTapCommand = new Command<object>(async (obj) =>
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
                }
                IsLoading = false;
            });


            SummaryattachmentsListViewItemCommand = new Command<object>(async (obj) =>
            {
                IsLoading = true;
                var selectedItem = (obj as Syncfusion.Maui.ListView.ItemTappedEventArgs).DataItem as OldZakatListModel;
                if (selectedItem != null)
                {
                    if (selectedItem.statusType == "IP017")
                    {
                        App.selectedZakatItem = selectedItem.fbNum;
                        await _navigationService.NavigateTo(App.OldZakatInstalmentPlanPageView);

                    }
                    else
                    {
                        SelectedFbNum = selectedItem.fbNum;
                        var index = ZakatListData.IndexOf(selectedItem);

                        await GetSummaryDetailsClickedAsync(index);
                        EnableZakatInstalmentSummary();
                    }
                }
                IsLoading = false;
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




            ReqInstalmentBtnTapped = new Command(async() =>
            {
                IsLoading = true;

                App.selectedZakatItem = "";

              await  _navigationService.NavigateTo(App.OldZakatInstalmentPlanPageView);

                IsLoading = false;
            });


            SummaryRevokeBtnTapped = new Command(async () =>
            {
                var result = await _dialogService.ShowMessage(AppResources.VatRefundsConfirmationTit, AppResources.ZakatRevokConfirmationText, AppResources.ZZCancel, AppResources.CRContinue);
                if (!result)
                {
                    App.TP = null;
                    EnableNotePage();
                }

            });



            ZDownloadForm = new Command(async () =>
            {
                IsLoading = true;
                if (SelectedFbNum != null)
                {

                    string downloadurl = ZATCAConstants.OldZakatdownloadCoverFormFile + SelectedFbNum;
                   await _navigationService.NavigateTo(App.PdfView, downloadurl);

                }
                IsLoading = false;



            });

            Download_Acknowledgement = new Command(async () =>
            {
                IsLoading = true;
                if (SelectedFbNum != null)
                {
                    string downloadurl = ZATCAConstants.ZOdownloadAckLetter + SelectedFbNum;
                  await  _navigationService.NavigateTo(App.PdfView, downloadurl);
                }
                IsLoading = false;



            });

        }
        public async Task onPageLoad()
        {
            try
            {
                IsLoading = true;
                await GetZakatInstalmentPlanList();

                IsLoading = false;
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                IsLoading = false;
                _navigationService.GoBack();
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

        public void CheckDueInvoices()
        {
            IsLoading = true;

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
                _isResendOTPEnabled = value;
                // OnResendOTPClicked.ChangeCanExecute();
                OnPropertyChanged("IsResendOTPEnabled");
            }
        }


        private void OnCountDownTimedOTPEvent(object sender, ElapsedEventArgs e)
        {
            countDownSeconds--;


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


        private bool _isSummaryAttachmentsVisible = false;
        public bool IsSummaryAttachmentsVisible
        {
            get
            {
                return _isSummaryAttachmentsVisible;
            }
            set
            {
                _isSummaryAttachmentsVisible = value;
                OnPropertyChanged("IsSummaryAttachmentsVisible");
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
                _numberOfInstalmentPlans = value;
                OnPropertyChanged("NumberOfInstalmentPlans");
            }
        }


        private OldZakatInstalmentPlanRequestListModel _reqVatInstalmentPlanResponseList;
        public OldZakatInstalmentPlanRequestListModel ReqVatInstalmentPlanResponseList
        {
            get
            {
                return _reqVatInstalmentPlanResponseList;
            }
            set
            {
                _reqVatInstalmentPlanResponseList = value;
                OnPropertyChanged("ReqVatInstalmentPlanResponseList");
            }
        }
        private List<OldZakatInstalmentPlanRequestListModel.OldResult2> _requestForInstalmentPlanList;
        public List<OldZakatInstalmentPlanRequestListModel.OldResult2> RequestForInstalmentPlanList
        {
            get
            {
                return _requestForInstalmentPlanList;
            }
            set
            {
                _requestForInstalmentPlanList = value;
                OnPropertyChanged("RequestForInstalmentPlanList");
            }
        }
        private List<OldZakatInstalmentPlanRequestListModel.OldResult> _RevokeListItem;
        public List<OldZakatInstalmentPlanRequestListModel.OldResult> RevokeListItem
        {
            get
            {
                return _RevokeListItem;
            }
            set
            {
                _RevokeListItem = value;
                OnPropertyChanged("RevokeListItem");
            }
        }

        private List<OldZakatInstalmentPlanRequestListModel.OldRevokeListResult> _requestForRevokeList;
        public List<OldZakatInstalmentPlanRequestListModel.OldRevokeListResult> RequestForRevokeList
        {
            get
            {
                return _requestForRevokeList;
            }
            set
            {
                _requestForRevokeList = value;
                OnPropertyChanged("RequestForRevokeList");
            }
        }


        public void BindVatInstalments()
        {
            if (ReqVatInstalmentPlanResponseList.d.ListSet != null)
            {
                if (RequestForInstalmentPlanList != null)
                {

                    RequestForInstalmentPlanList.Clear();

                }

                IsZakat = Preferences.Get("isZakat", false);

                RequestForInstalmentPlanList = ReqVatInstalmentPlanResponseList.d.ListSet;

                for (int i = 0; i < RequestForInstalmentPlanList.Count; i++)
                {

                    var zakatbill = AppResources.ZakatInstalmetSelectTypeZakat;


                    IsZakat = Preferences.Get("isZakat", false);
                    if (IsZakat)
                    {
                        zakatbill = AppResources.ZakatInstalmetSelectTypeZakat;

                    }
                    else
                    {
                        zakatbill = AppResources.ZakatInstalmetSelectTypeIncomeTax;

                    }
                    zakatListData.Add(new OldZakatListModel()
                    {
                        referanceNumber = RequestForInstalmentPlanList[i].Fbnum,
                        status = RequestForInstalmentPlanList[i].StatText,
                        dateOfSubmission = RequestForInstalmentPlanList[i].CrdtText,
                        Fbtyp = zakatbill,
                        statusType = RequestForInstalmentPlanList[i].Fbsta,
                        fbNum = RequestForInstalmentPlanList[i].Fbnum


                    });

                }



                InstalmentsCount = RequestForInstalmentPlanList.Count + " " + AppResources.ZakatInstalmentRequestCount;

            }


            if (ZakatListData.Count > 0)
            {
                NumberOfInstalmentPlans = ZakatListData.Count + " " + AppResources.ZakatInstalmetPlan;
            }
        }
        public ObservableCollection<OldZakatSelectBillModel> summarySelectedBillsList { get; set; }
        public ObservableCollection<OldZakatSelectBillModel> SummarySelectedBillsList
        {
            get
            {
                return summarySelectedBillsList;
            }
            set
            {
                summarySelectedBillsList = value;
                OnPropertyChanged("SummarySelectedBillsList");
            }
        }

        public ObservableCollection<OldZakatListModel> zakatListData { get; set; }
        public ObservableCollection<OldZakatListModel> ZakatListData
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


        private Models.ZakatInstalationModels.OldZakatRequestDisplayModel _seletedZakatForm;
        public Models.ZakatInstalationModels.OldZakatRequestDisplayModel SeletedZakatForm
        {
            get
            {
                return _seletedZakatForm;
            }
            set
            {
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
                _isAttachmentsViewEnabled = value;
                OnPropertyChanged("IsAttachmentsViewEnabled");
            }
        }

        public ObservableCollection<Attachment> attachments { get; set; }
        public ObservableCollection<Attachment> Attachments
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
                _enteredOTP = value;
                OnPropertyChanged("EnteredOTP");
            }
        }
        public void SetOTP()
        {

            EnteredOTP = OTPFirstDigit + OTPSecondDigit + OTPThirdDigit + OTPFourthDigit;

        }

        public void BindZakatSummaryData(OldZakatRequestDisplayModel zakatRequestDisplayModel)
        {



            SummarySelectedBillsList = new ObservableCollection<OldZakatSelectBillModel>();

            foreach (var bill in SeletedZakatForm.d.Z_INVOICE_UI5Set)
            {




                string submitDate = "";



                if (bill.ADueDtTb != null)
                {
                    DateTime dateStart = new DateTime();
                    CultureInfo cultureInfo = new CultureInfo("ar-SA");
                    string apiDate = @"""" + bill.ADueDtTb + @"""";
                    dateStart = JsonConvert.DeserializeObject<DateTime>(apiDate);



                    GregorianCalendar hjCalendar = new GregorianCalendar();
                    int year = hjCalendar.GetYear(dateStart);
                    int month = hjCalendar.GetMonth(dateStart);
                    int day = hjCalendar.GetDayOfMonth(dateStart);



                    string dateStr = string.Format("{0:00}/{1}/{2}", day, month, year);



                    bill.ADueDtTb = dateStr;



                    string dt1 = string.Empty;
                    string[] dts = null;
                    dts = bill.ADueDtTb.Split('/');
                    dt1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];
                    submitDate = dt1;
                }

                if (bill.AIvAbtyp.Equals("ITAX"))
                {
                    bill.AIvAbtyp = AppResources.ZakatInstalmetSelectTypeIncomeTax;
                }
                else if (bill.AIvAbtyp.Equals("ZAKT"))
                {
                    bill.AIvAbtyp = AppResources.FORM5Zakat;
                }


                SummarySelectedBillsList.Add(new OldZakatSelectBillModel()
                {
                    billNumber = AppResources.Bill + (SummarySelectedBillsList.Count + 1).ToString("00") + ":",
                    amount = UtilityManager.GetCommaSeparatedAmount(bill.ADueAmtTb),
                    saadNumber = bill.AIvNoTb,
                    taxPeriod = bill.ADueDtTb,
                    isSelected = false,
                    billType = bill.AIvAbtyp



                });
            }

            Attachments = new ObservableCollection<Attachment>();




            for (int i = 0; i < SeletedZakatForm.d.AttDetSet.Count; i++)
            {
                Attachments.Add(SeletedZakatForm.d.AttDetSet[i]);
            }



            // Attachments = attachments;



            if (Attachments != null && Attachments.Count > 0)
            {
                IsSummaryAttachmentsVisible = true;
            }



            TotalAmount = string.Format("{0:N2}", SeletedZakatForm.d.ATotalAmt) + " SAR";
            InstalmentAmount = string.Format("{0:N2}", SeletedZakatForm.d.ADpAmt) + " SAR";
            SummaryNoOfInstalments = SeletedZakatForm.d.APlanDurPeri;



            if (zakatRequestDisplayModel.d.APaymentFreq != null && (zakatRequestDisplayModel.d.APaymentFreq == "01" || zakatRequestDisplayModel.d.APaymentFreq == "1"))
            {
                SelectedFrequencyName = AppResources.ZakatInstalmetMonthly;
            }
            else if (zakatRequestDisplayModel.d.APaymentFreq != null && (zakatRequestDisplayModel.d.APaymentFreq == "02" || zakatRequestDisplayModel.d.APaymentFreq == "2"))
            {
                SelectedFrequencyName = AppResources.ZakatInstalmetQuarterly;
            }
            else if (zakatRequestDisplayModel.d.APaymentFreq != null && (zakatRequestDisplayModel.d.APaymentFreq == "03" || zakatRequestDisplayModel.d.APaymentFreq == "3"))
            {
                SelectedFrequencyName = AppResources.ZakatInstalmetHalfYearly;
            }
            else if (zakatRequestDisplayModel.d.APaymentFreq != null && (zakatRequestDisplayModel.d.APaymentFreq == "04" || zakatRequestDisplayModel.d.APaymentFreq == "4"))
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

            try
            {
                IsLoading = true;
                var item = zakatListData[index];

                SeletedZakatForm = await OldZakatInstallmentWebServiceManager.GAZTGetOldZakatRequestDisplayData(item.referanceNumber, item.statusType);

               await PopToRootPage();


                if (SeletedZakatForm != null && SeletedZakatForm.d != null)
                {



                    NoOfInstalments = SeletedZakatForm.d.APlanDurPeri;
                    TotalAmount = string.Format("{0:N2}", SeletedZakatForm.d.ATotalAmt) + " SAR";
                    InstalmentAmount = string.Format("{0:N2}", SeletedZakatForm.d.ADpAmt) + " SAR";


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



                    BindZakatSummaryData(SeletedZakatForm);
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
                OldZakatInstalmentPlanRequestListModel rEQVatInstalmentPlanResponse = null;
                IsZakat = Preferences.Get("isZakat", false);

                string callSer = "IPRF";

                if (IsZakat)
                {

                    callSer = "IPRFZ";
                }
                else
                {
                    callSer = "IPRFI";
                }



                rEQVatInstalmentPlanResponse = await OldZakatInstallmentWebServiceManager.GAZTGetOldZakatInstalmentPlanRequestList(callSer, "", "");
                ReqVatInstalmentPlanResponseList = rEQVatInstalmentPlanResponse;

                if (ReqVatInstalmentPlanResponseList != null && ReqVatInstalmentPlanResponseList.d != null)
                {
                    ZakatListData = new ObservableCollection<OldZakatListModel>();



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

        public ICommand OnAppearingCommand
        {
            get
            {
                return new Command(async () =>
                {
                    ResetData();
                    EnableCreateZakatInstalment();
                    await GetZakatInstalmentPlanList();
                });
            }
        }

        #endregion


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

    }
}
