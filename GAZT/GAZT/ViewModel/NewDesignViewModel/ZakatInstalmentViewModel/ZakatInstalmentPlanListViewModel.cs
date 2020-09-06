using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using EGAZT.Models.InstalmentPlanModel;
using EGAZT.Models.ZakatInstalationModels;
using EGAZT.Models.ZakatInstalmentModels;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Views.NewDesign.ZakatInstalmentPlan;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Metadata = EGAZT.Models.ZakatInstalationModels.Metadata;

namespace EGAZT.ViewModel.NewDesignViewModel.VATInstalmentPlanViewModel
{
    public class ZakatInstalmentPlanListViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        private bool _isZakatLandingPageVisible = false;
        private bool _createZakatInstalmentBtnVisible = true;
        private bool _isLoading = false;
        private bool _isRevokZakatInstalmentVisible = true;
        private bool _isOTPPageVisible = false;
        private bool _IsZakatSummaryVisible = false;
        private bool _isZakatSummaryRevokeVisible = false;
        private bool _isAttachmentsViewEnabled = false;


        public System.Timers.Timer otpTimer;
        public int countDownSeconds;

        #endregion

        #region commands
        public ICommand ReqInstalmentBtnTapped { get; set; }
        public ICommand GoBackClick { get; set; }
        public ICommand CloseClick { get; set; }
        public ICommand OnContinueClick { get; set; }
        public ICommand SummaryContinueBtnTapped { get; set; }
        public ICommand SummaryRevokeBtnTapped { get; set; }
        public Command NoteContinueTapped { get; set; }
        public Command OnResendOTPClicked { get; set; }

        #endregion

        public ZakatInstalmentPlanListViewModel(INavigationService navigationService, IDialogService dialogService)
        {

             IsZakat = Preferences.Get("isZakat", false);

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

            CloseClick = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            });

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
                else if (IsRevokZakatInstalmentVisible)
                {
                    EnableCreateZakatInstalment();
                }
                else if (IsOTPPageVisible)
                {
                    EnableNotePage();
                }




            });



           

            OnContinueClick = new Command(() =>
            {
                SetOTP();
                if (!string.IsNullOrEmpty(EnteredOTP) && EnteredOTP.Length > 0)
                {
                    ValidateOTPAsync();
                }
                else
                {
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Pleaseenterconfirmationcodesenttoyourmobilenumber));

                    // _dialogService.ShowMessageBox(AppResources.Pleaseenterconfirmationcodesenttoyourmobilenumber, AppResources.Information);

                }
            });


            OnResendOTPClicked = new Command(async () =>
            {
                if (IsResendOTPEnabled)
                {

                    await SendOTPToRegisterMobileNumber(SelectedFbNum,"");
                }

            });


            ReqInstalmentBtnTapped = new Command(async () =>
            {
                _navigationService.NavigateTo(App.ZakatInstalmentPlanPageView);
            });
            SummaryContinueBtnTapped = new Command(async () =>
            {
                //EnableOTPPage();
            });

            SummaryRevokeBtnTapped = new Command(async () =>
            {
                var result = await App.Current.MainPage.DisplayAlert(AppResources.VatRefundsConfirmationTit, AppResources.ZakatRevokConfirmationText, AppResources.ZZCancel, AppResources.CRContinue);
                if (!result)
                {
                    App.TP = null;
                    EnableNotePage();
                }

            });

            NoteContinueTapped = new Command(async () =>
            {
                ValidateNoteAndContinueAsync();
            });

        }

        private bool _isZakat;
        public bool IsZakat
        {
            get
            {
                return _isZakat;
            }
            set
            {
                _isZakat = value;
                RaisePropertyChanged("IsZakat");
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
                RaisePropertyChanged("ZakatTitle");
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
                RaisePropertyChanged("ZakatInstalments");
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
                RaisePropertyChanged("NoteEditor");
            }
        }



        #region Views Enabling
        public void EnableZakatLandingPage()
        {
            AddOutletDecisionOptions();

            IsZakatLandingPageVisible = true;
            CreateZakatInstalmentBtnVisible = false;
            IsZakatSummaryVisible = false;
            IsRevokZakatInstalmentVisible = false;
            IsOTPPageVisible = false;
            IsNoteViewVisible = false;

        }

        public void EnableNotePage()
        {
            IsNoteViewVisible = true;
            IsOTPPageVisible = false;
            IsZakatLandingPageVisible = false;
            CreateZakatInstalmentBtnVisible = false;
            IsZakatSummaryVisible = false;
            IsRevokZakatInstalmentVisible = false;
        }

        public void EnableOTPPage()
        {
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
            IsOTPPageVisible = false;
            IsZakatLandingPageVisible = false;
            CreateZakatInstalmentBtnVisible = true;
            IsZakatSummaryVisible = false;
            IsRevokZakatInstalmentVisible = false;
            IsNoteViewVisible = false;
        }
        public void EnableZakatInstalmentSummary()
        {
            IsZakatLandingPageVisible = false;
            CreateZakatInstalmentBtnVisible = false;
            IsZakatSummaryVisible = true;
            IsRevokZakatInstalmentVisible = false;
            IsOTPPageVisible = false;
            IsNoteViewVisible = false;
        }
        public void EnableRevokZakatInstalment()
        {
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
                RaisePropertyChanged("LblCountDownTimer");
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

                RaisePropertyChanged("OTPFirstDigit");
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
                RaisePropertyChanged("OTPSecondDigit");
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
                RaisePropertyChanged("OTPThirdDigit");
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
                RaisePropertyChanged("OTPFourthDigit");
            }
        }

        private bool CheckOnlyNumber(char letter)
        {
            if ((letter >= 48 && letter <= 57))
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

                RaisePropertyChanged(() => ContinueButtonEnability);
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
                RaisePropertyChanged("IsResendOTPEnabled");
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
                await _dialogService.ShowMessage(AppResources.ZakatRevokCannotLeaveEmpty, AppResources.Information);
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
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
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
                _isSummaryAttachmentsVisible = value;
                RaisePropertyChanged("IsSummaryAttachmentsVisible");
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
                RaisePropertyChanged("IsZakatLandingPageVisible");
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
                RaisePropertyChanged("CreateZakatInstalmentBtnVisible");
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
                RaisePropertyChanged("IsOTPPageVisible");
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
                RaisePropertyChanged("IsZakatSummaryVisible");
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
                RaisePropertyChanged("IsZakatSummaryRevokeVisible");
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
                RaisePropertyChanged("IsRevokZakatInstalmentVisible");
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
                RaisePropertyChanged("IsNoteViewVisible");
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
                RaisePropertyChanged("OutletDecisionOptions");
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
                RaisePropertyChanged("ZakatInstalmentPlanRequestListModel");
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
                RaisePropertyChanged("PaymentFrequency");
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
                RaisePropertyChanged("NoOfInstalments");
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
                RaisePropertyChanged("InstalmentAmount");
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
                RaisePropertyChanged("PenaltyAmount");
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
                RaisePropertyChanged("InstalmentsCount");
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
                RaisePropertyChanged("TotalAmount");
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
                RaisePropertyChanged("SelectedFbNum");
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
                RaisePropertyChanged("NumberOfInstalmentPlans");
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
                _reqVatInstalmentPlanResponseList = value;
                RaisePropertyChanged("ReqVatInstalmentPlanResponseList");
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
                _requestForInstalmentPlanList = value;
                RaisePropertyChanged("RequestForInstalmentPlanList");
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
                _requestForRevokeList = value;
                RaisePropertyChanged("RequestForRevokeList");
            }
        }


        public async void BindVatInstalments()
        {
            if (ReqVatInstalmentPlanResponseList.d.WorklistSet.results != null)
            {
                RequestForInstalmentPlanList = ReqVatInstalmentPlanResponseList.d.WorklistSet.results;
                InstalmentsCount = RequestForInstalmentPlanList.Count + " " + AppResources.ZakatInstalmentRequestCount;

                if (RequestForInstalmentPlanList.Count > 0)
                {
                    NumberOfInstalmentPlans = RequestForInstalmentPlanList.Count + " " + AppResources.ZakatInstalmetPlan;

                }
            }

            await GetZakatRevokList();

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
        //        RaisePropertyChanged("RevokList");
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
                if (SummarySelectedBillsList == value)
                {
                    return;
                }
                summarySelectedBillsList = value;
                RaisePropertyChanged("SummarySelectedBillsList");
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
                RaisePropertyChanged("ZakatListData");
            }
        }


        private Models.ZakatInstalationModels.ZakatRequestDisplayModel _seletedZakatForm;
        public Models.ZakatInstalationModels.ZakatRequestDisplayModel SeletedZakatForm
        {
            get
            {
                return _seletedZakatForm;
            }
            set
            {
                _seletedZakatForm = value;
                RaisePropertyChanged("SeletedZakatForm");
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
                RaisePropertyChanged("IsAttachmentsViewEnabled");
            }
        }

        public ObservableCollection<AttDetSet> attachments { get; set; }
        public ObservableCollection<AttDetSet> Attachments
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
                RaisePropertyChanged("Attachments");
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
                RaisePropertyChanged("EnteredOTP");
            }
        }
        public void SetOTP()
        {

            EnteredOTP = OTPFirstDigit + OTPSecondDigit + OTPThirdDigit + OTPFourthDigit;

        }
        public void BindZakatSummaryData(ZakatRequestDisplayModel zakatRequestDisplayModel)
        {

            var summarySelectedBillsList = new ObservableCollection<ZakatSelectBillModel>();
            foreach (var bill in zakatRequestDisplayModel.d.Z_INVOICE_UI5Set.results)
            {
                summarySelectedBillsList.Add(new ZakatSelectBillModel()
                {
                    billNumber = AppResources.Bill + (summarySelectedBillsList.Count + 1).ToString("00"),
                    amount = "0.00 SAR",
                    saadNumber = bill.AIvNoTb,
                    taxPeriod = "",
                    isSelected = false,
                    billType = ZakatTitle



                });;
            }
            SummarySelectedBillsList = summarySelectedBillsList;



            Attachments = new ObservableCollection<AttDetSet>();



            for (int i = 0; i < zakatRequestDisplayModel.d.AttDetSet.results.Count; i++)
            {
                Attachments.Add((AttDetSet)zakatRequestDisplayModel.d.AttDetSet.results[i]);
            }



            if (Attachments != null && Attachments.Count > 0)
            {
                IsSummaryAttachmentsVisible = true;
            }



          
        }

        #endregion


        #region APIs

        public async Task GetSummaryDetailsClickedAsync(int index)
        {
            SummarySelectedBillsList = null;

            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {

                    IsLoading = true;
                    // DisplayInfoModel _DisplayRequestData = new DisplayInfoModel();
                    try
                    {
                        var item = zakatListData[index];

                        SeletedZakatForm = await WebServiceManager.GAZTGetZakatRequestDisplayData(item.referanceNumber, item.Fbtyp);

                        
                        PopToRootPage();


                        if (SeletedZakatForm != null && SeletedZakatForm.d != null)
                        {
                            if(RequestForRevokeList != null && RequestForRevokeList.Count != 0) {

                                var itemsSource = RequestForRevokeList.Where(w => w.Fbnum.Contains(SeletedZakatForm.d.Fbnum)).ToList();
                                if(itemsSource.Count > 0) {
                                    SelectedFbNum = itemsSource[0].Fbnum;
                                    IsZakatSummaryRevokeVisible = true;
                                }else
                                {
                                    IsZakatSummaryRevokeVisible = false;

                                }

                            }

                            NoOfInstalments = item.noOfInstalments;
                            TotalAmount = string.Format("{0:N2}", item.downpayment) + " SAR";
                            InstalmentAmount = string.Format("{0:N2}", item.dueamount) + " SAR";

                            BindZakatSummaryData(SeletedZakatForm);
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

                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                //await Task.Run(() =>
                //{

                //});
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });

            }
            catch (Exception ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }

        }
        public async Task GetZakatInstalmentPlanList()
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
                    ReqVatInstalmentPlanResponseList = null;
                    Models.ZakatInstalationModels.ZakatInstalmentPlanRequestListModel rEQVatInstalmentPlanResponse = null;

                    try
                    {
                        rEQVatInstalmentPlanResponse = await WebServiceManager.GAZTGetZakatInstalmentPlanRequestList("", "", "");
                        ReqVatInstalmentPlanResponseList = rEQVatInstalmentPlanResponse;

                        PopToRootPage();

                        if (ReqVatInstalmentPlanResponseList != null && ReqVatInstalmentPlanResponseList.d != null)
                        {
                            ZakatListData = new ObservableCollection<ZakatListModel>();

                            //foreach (var bill in zakatRequestDisplayModel.d.Z_INVOICE_UI5Set.results)
                            //{
                            //    SummarySelectedBillsList.Add(new ZakatSelectBillModel()
                            //    {
                            //        billNumber = AppResources.Bill + (SummarySelectedBillsList.Count + 1).ToString("00"),
                            //        amount = "0.00 SAR",
                            //        saadNumber = bill.AIvNoTb,
                            //        taxPeriod = "",
                            //        isSelected = false,
                            //        billType = AppResources.ZakatInstalmetSelectTypeZakat

                            //    });
                            //}

                            for (int i = 0; i < ReqVatInstalmentPlanResponseList.d.WorklistSet.results.Count; i++)
                            {

                                string submitDate = "";
                                if (ReqVatInstalmentPlanResponseList.d.WorklistSet.results[i].SubmitDt != null)
                                {

                                    DateTime dateStart = new DateTime();
                                    CultureInfo cultureInfo = new CultureInfo("ar-SA");
                                    string apiDate = @"""" + ReqVatInstalmentPlanResponseList.d.WorklistSet.results[i].SubmitDt + @"""";
                                    dateStart = JsonConvert.DeserializeObject<DateTime>(apiDate);

                                    GregorianCalendar hjCalendar = new GregorianCalendar();
                                    int year = hjCalendar.GetYear(dateStart);
                                    int month = hjCalendar.GetMonth(dateStart);
                                    int day = hjCalendar.GetDayOfMonth(dateStart);

                                    string dateStr = string.Format("{0:00}/{1}/{2}", day, month, year);

                                    ReqVatInstalmentPlanResponseList.d.WorklistSet.results[i].SubmitDt = dateStr;

                                    string dt1 = string.Empty;
                                    string[] dts = null;
                                    dts = ReqVatInstalmentPlanResponseList.d.WorklistSet.results[i].SubmitDt.Split('/');
                                    dt1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];
                                    submitDate = dt1;
                                }

                                ZakatListData.Add(new ZakatListModel()
                                {
                                    referanceNumber = ReqVatInstalmentPlanResponseList.d.WorklistSet.results[i].Fbnum,
                                    status = ReqVatInstalmentPlanResponseList.d.WorklistSet.results[i].Status,
                                    dueamount = ReqVatInstalmentPlanResponseList.d.WorklistSet.results[i].TotAmt,
                                    instalmentAmount = ReqVatInstalmentPlanResponseList.d.WorklistSet.results[i].DueAmt,
                                    noOfInstalments = ReqVatInstalmentPlanResponseList.d.WorklistSet.results[i].PlanDur,
                                    downpayment = ReqVatInstalmentPlanResponseList.d.WorklistSet.results[i].DpAmt,
                                    dateOfSubmission = submitDate,
                                    Fbtyp = ReqVatInstalmentPlanResponseList.d.WorklistSet.results[i].Fbtyp

                                });


                             }
                            BindVatInstalments();
                            
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

                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                //await Task.Run(() =>
                //{

                //});
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });

            }
            catch (Exception ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public async Task GetZakatRevokList()
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
                    //           ReqVatInstalmentPlanResponseList = null;
                    //          Models.ZakatInstalationModels.ZakatInstalmentPlanRequestListModel rEQVatInstalmentPlanResponse = null;

                    try
                    {

                        ZakatInstalmentPlanRequestListModel revokResult = await WebServiceManager.GAZTGetZakatRevokeList("", "", "");

                        PopToRootPage();


                        if (revokResult != null && revokResult.d != null)
                        {

                            if(ZakatListData == null) {
                                ZakatListData = new ObservableCollection<ZakatListModel>();

                            }


                            RequestForRevokeList = revokResult.d.RevokeListSet.results;




                            for (int i = 0; i < revokResult.d.WorklistSet.results.Count; i++)
                            {

                                string submitDate = "";
                                if (revokResult.d.WorklistSet.results[i].SubmitDt != null)
                                {

                                    DateTime dateStart = new DateTime();
                                    CultureInfo cultureInfo = new CultureInfo("ar-SA");
                                    string apiDate = @"""" + revokResult.d.WorklistSet.results[i].SubmitDt + @"""";
                                    dateStart = JsonConvert.DeserializeObject<DateTime>(apiDate);

                                    GregorianCalendar hjCalendar = new GregorianCalendar();
                                    int year = hjCalendar.GetYear(dateStart);
                                    int month = hjCalendar.GetMonth(dateStart);
                                    int day = hjCalendar.GetDayOfMonth(dateStart);

                                    string dateStr = string.Format("{0:00}/{1}/{2}", day, month, year);

                                    string dt1 = string.Empty;
                                    string[] dts = null;
                                    dts = dateStr.Split('/');
                                    dt1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];
                                    submitDate = dt1;
                                }

                                ZakatListData.Add(new ZakatListModel()
                                {
                                    referanceNumber = revokResult.d.WorklistSet.results[i].Fbnum,
                                    status = revokResult.d.WorklistSet.results[i].Status,
                                    dueamount = revokResult.d.WorklistSet.results[i].TotAmt,
                                    instalmentAmount = revokResult.d.WorklistSet.results[i].DueAmt,
                                    noOfInstalments = revokResult.d.WorklistSet.results[i].PlanDur,
                                    downpayment = revokResult.d.WorklistSet.results[i].DpAmt,
                                    dateOfSubmission = submitDate,
                                    Fbtyp = revokResult.d.WorklistSet.results[i].Fbtyp


                                });


                            }

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

                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                //await Task.Run(() =>
                //{

                //});
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });

            }
            catch (Exception ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public async Task validateZakatRevoke()
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
                    //           ReqVatInstalmentPlanResponseList = null;
                    //          Models.ZakatInstalationModels.ZakatInstalmentPlanRequestListModel rEQVatInstalmentPlanResponse = null;

                    try
                    {

                        ZakatRevokeValidateModel revokeResult = await WebServiceManager.GAZTGetZakatRevokeValidate(SelectedFbNum);

                        PopToRootPage();


                        if (revokeResult != null && revokeResult.d != null)
                        {
                            if (revokeResult.d.Valid) {

                                await SendOTPToRegisterMobileNumber(SelectedFbNum, "");
                      

                            }


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

                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                //await Task.Run(() =>
                //{

                //});
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });

            }
            catch (Exception ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }




        public async Task getZakatRevokeData()
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
                    ZakatInstalments = null;
                    ZakatInstalmentPlanRevokeResponse vATInstalment = null;
                    try
                    {
                        ZakatInstalments = await WebServiceManager.GetZakatInstalmentRevokePostData(SelectedFbNum);


                        if (ZakatInstalments != null)
                        {

                             await RevokeSubmitClicked();
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
                //await Task.Run(() =>
                //{

                //});
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });

            }
            catch (Exception ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }


        public async Task<ZakatInstalmentPlanRevokeResponse> RevokeSubmitClicked()
        {
            ZakatInstalmentPlanRevokeResponse response = new ZakatInstalmentPlanRevokeResponse();
            ZakatInstalmentPlanRevokeRequest request = new ZakatInstalmentPlanRevokeRequest();

            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });


                var item = ZakatInstalments.d;


                request.AccMethod = "A";
                request.AltMobNo = item.AltMobNo;
                request.DataVersion = item.DataVersion;
                request.DecCb = item.DecCb;
                request.DpAmt = item.DpAmt;
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
                request.TotAmt = item.TotAmt;
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

                response = await WebServiceManager.SaveZakatInstalmentRevokeData(request);
                PopToRootPage();
                if (response != null)
                {
                    try
                    {
                        if (response != null && response.d != null)
                        {

                            Preferences.Set("IsFromRevok", true);
                            await Application.Current.MainPage.Navigation.PushAsync(new ZakatInstalmentPlanSuccessPage());
                        }
                        IsLoading = false;
                        return response;

                    }
                    catch (Exception ex)
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
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    //_navigationService.GoBack();

                });
                return response;
            }

            catch (Exception ex)
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

        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });
            }
        }

        private bool _Enabled;
        public bool Enabled
        {
            get { return _Enabled; }
            set
            {
                _Enabled = value;
                RaisePropertyChanged("Enabled");
            }
        }
        private async Task SendOTPToRegisterMobileNumber(string fbNumber, string OTPCode)
        {

            try
             {
                 await Task.Run(() =>
                 {
                     IsLoading = true;
                     Enabled = false;
                 });
                 await Task.Run(async () =>
                 {
                     try
                     {
                         string lang = UtilityManager.GetLanguageParameter();
                         ZakatRevokeSendSMSModel revokeOTP = await WebServiceManager.GAZTZakatRevokeSendOTP(fbNumber, OTPCode);
                          PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                         if (revokeOTP.d != null)
                         {
                             var OTPItem = revokeOTP.d.results[0];


                             

                                 if(OTPCode.Length == 0)
                                 {
                                     EnableOTPPage();
                                     ContinueButtonEnability = true;
                                     IsResendOTPEnabled = false;
                                     StartOTPTimer();
                                 }
                                 else {
                                     if(OTPItem.ValidSms)
                                     {

                                         await getZakatRevokeData();

                                     }
                                     else
                                     {
                                         await getZakatRevokeData();

                                     }
                                 

                             }
                            


                                

                             

                         }
                         else
                         {
                             Device.BeginInvokeOnMainThread(async () =>
                             {
                                 await _dialogService.ShowMessageBox(AppResources.ZPleaseEnterAValidUserID, AppResources.ZError);
                             });
                         }
                        
                     }
                     catch (Exception ex)
                     {
                     }
                 });
                 await Task.Run(() =>
                 {
                     IsLoading = false;
                 });
             }
             catch (InternetException ex)
             {
                 await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                 //   await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                 await Task.Run(() =>
                 {
                     IsLoading = false;
                     // UserIDLayoutVisibility = true;
                 });
             }
        }

        public async Task ValidateOTPAsync()
        {
            await SendOTPToRegisterMobileNumber(SelectedFbNum, EnteredOTP);
        }
    }
}
