using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using Mopups.Services;
using Newtonsoft.Json;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.InstalmentPlanModel;
using ZATCAMAUI.Models.VATInstalmentModels;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Views.NewDesign.VatInstalmentPlan;
using static ZATCAMAUI.Models.ErrorMessage;
using static ZATCAMAUI.Models.VATInstalmentModels.RequestToVATInstallmentPlanDetails;
using static ZATCAMAUI.Models.VATInstalmentModels.RequestToVATInstallmentPlanDetails.DisplayInstallmentAgreementSchedulePlan;
using static ZATCAMAUI.Models.VATInstalmentModels.RequestToVATInstallmentPlanDetails.VATInstalmentScheduleDetailsModel;
namespace ZATCAMAUI.ViewModel.NewDesignViewModel.VATInstalmentPlanViewModel
{
    public class VATInstalmentPlanListViewModel : BaseViewModel
    {


        #region Variable
        public bool IsAPICalledSuccessfully = true;
        private bool _isReokeScreenExists = false;
        #endregion

        public ICommand GoBackClick { get; set; }
        public ICommand CloseClick { get; set; }
        public ICommand SummaryContinueBtnTapped { get; set; }
        public ICommand SummaryRevokeBtnTapped { get; set; }
        public ICommand ContinueClick { get; set; }
        public ICommand CancelButton { get; set; }
        public ICommand NoteContinueTapped { get; set; }
        public ICommand OnContinueClickOTP { get; set; }
        public Command OnResendOTPClicked { get; set; }
        public ICommand CreateNewRequestTapped { get; set; }

        public VATInstalmentPlanListViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            GoBackClick = new Command(async () =>
            {
                if (IsVATLandingPageVisible || IsPendingRequestsVisible)
                {
                    await Microsoft.Maui.Controls.Application.Current.MainPage.Navigation.PopAsync();
                }
                else if (IsVATInstalmentPlanVisible)
                {
                    EnableVATLandingPage();
                }
                else if (IsInstalmentSchedulePlanVisible)
                {
                    if (_isReokeScreenExists)
                    {
                        EnableVATLandingPage();
                    }
                    else
                    {
                        EnableVAtInstalmentPlan();
                    }
                }
                else if (IsDisplayListVisible)
                {
                    EnableVATLandingPage();
                }
                else if (IsDisplayDetailsVisible)
                {
                    EnableDisplayInstalment();
                }
                else if (NotesPageVisible)
                {
                    EnableVAtInstalmentPlan();
                }
                else if (IsOTPPageVisible)
                {
                    ContinueNotesPage();
                }
                else if (IsVATRevokeInstalmentVisible)
                {
                    EnableVATLandingPage();

                }

            });
            ContinueClick = new Command(this.ContinueNotesPage);
            NoteContinueTapped = new Command(this.ContinueOTPPage);
            OnContinueClickOTP = new Command(async () =>
            {
                SetOTP();
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Pleaseenterconfirmationcodesenttoyourmobilenumber));
            });
            CancelButton = new Command(async () =>
            {
                await MopupService.Instance.PopAsync();
            });
            CloseClick = new Command(async () =>
            {
                await Microsoft.Maui.Controls.Application.Current.MainPage.Navigation.PopAsync();
            });
            SummaryContinueBtnTapped = new Command(async () =>
            {
                await Microsoft.Maui.Controls.Application.Current.MainPage.Navigation.PopAsync();
            });
            SummaryRevokeBtnTapped = new Command(async () =>
            {
                await showInstructionDialog();
            });
            CreateNewRequestTapped = new Command(async () => await this.CreateNewRequest());
            AddOutletDecisionOptions();



            RequestInstalmentButtonTapped = new Command(RequestInstalmentButtonClicked);

            CreateNewRequestTapped = new Command(async () => await this.CreateNewRequest());



        }

        #region ICommand declarations

        public ICommand RequestInstalmentButtonTapped { get; set; }

        #endregion

        #region Visibility

        private bool _isArabic = false;
        public bool IsArabic
        {
            get
            {
                return _isArabic;
            }
            set
            {
                if (_isArabic == value) return;
                _isArabic = value;
                OnPropertyChanged("IsArabic");
            }
        }

        private bool _isDisplayListVisible = false;
        public bool IsDisplayListVisible
        {
            get
            {
                return _isDisplayListVisible;
            }
            set
            {
                if (_isDisplayListVisible == value) return;

                _isDisplayListVisible = value;
                OnPropertyChanged("IsDisplayListVisible");
            }
        }

        private bool _isDisplayDetailsVisible = false;
        public bool IsDisplayDetailsVisible
        {
            get
            {
                return _isDisplayDetailsVisible;
            }
            set
            {
                if (_isDisplayDetailsVisible == value) return;

                _isDisplayDetailsVisible = value;
                OnPropertyChanged("IsDisplayDetailsVisible");
            }
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

        private bool _isVATLandingPageVisible = true;
        public bool IsVATLandingPageVisible
        {
            get
            {
                return _isVATLandingPageVisible;
            }
            set
            {
                if (_isVATLandingPageVisible == value) return;

                _isVATLandingPageVisible = value;
                OnPropertyChanged("IsVATLandingPageVisible");
            }
        }

        private bool _isVATInstalmentPlanVisible = false;
        public bool IsVATInstalmentPlanVisible
        {
            get
            {
                return _isVATInstalmentPlanVisible;
            }
            set
            {
                if (_isVATInstalmentPlanVisible == value) return;

                _isVATInstalmentPlanVisible = value;
                OnPropertyChanged("IsVATInstalmentPlanVisible");
            }
        }

        private string _formGuidValue = "";
        public string FormGuidValue
        {
            get
            {
                return _formGuidValue;
            }
            set
            {
                if (_formGuidValue == value) return;

                _formGuidValue = value;
                OnPropertyChanged("FormGuidValue");
            }
        }
        private bool _isInstalmentSchedulePlanVisible = false;
        public bool IsInstalmentSchedulePlanVisible
        {
            get
            {
                return _isInstalmentSchedulePlanVisible;
            }
            set
            {
                if (_isInstalmentSchedulePlanVisible == value) return;

                _isInstalmentSchedulePlanVisible = value;
                OnPropertyChanged("IsInstalmentSchedulePlanVisible");
            }
        }

        #endregion


        public VATInstalmentPlanListModel instalmentListModel { get; set; }
        public VATInstalmentPlanListModel InstalmentListModel
        {
            get
            {
                return instalmentListModel;
            }

            set
            {
                if (instalmentListModel == value)
                {
                    return;
                }

                instalmentListModel = value;
                OnPropertyChanged("InstalmentListModel");
            }
        }

        private int _selectedOutletOptionIndex;
        public int SelectedOutletOptionIndex
        {
            get
            {
                return _selectedOutletOptionIndex;
            }
            set
            {
                if (_selectedOutletOptionIndex == value) return;

                _selectedOutletOptionIndex = value;
                OnPropertyChanged("SelectedOutletOptionIndex");
            }
        }

        private string _scheduleNoOfMonths = "";
        public string ScheduleNoOfMonths
        {
            get
            {
                return _scheduleNoOfMonths;
            }
            set
            {
                if (_scheduleNoOfMonths == value) return;

                _scheduleNoOfMonths = value;
                OnPropertyChanged("ScheduleNoOfMonths");
            }
        }

        private string _scheduleMonthlyInstalment = "";
        public string ScheduleMonthlyInstalment
        {
            get
            {
                return _scheduleMonthlyInstalment;
            }
            set
            {
                if (_scheduleMonthlyInstalment == value) return;

                _scheduleMonthlyInstalment = value;
                OnPropertyChanged("ScheduleMonthlyInstalment");
            }
        }
        private string _scheduleTotalAmountPaid = "";
        public string ScheduleTotalAmountPaid
        {
            get
            {
                return _scheduleTotalAmountPaid;
            }
            set
            {
                if (_scheduleTotalAmountPaid == value) return;

                _scheduleTotalAmountPaid = value;
                OnPropertyChanged("ScheduleTotalAmountPaid");
            }
        }
        private string _scheduleAmountRemaining = "";
        public string ScheduleAmountRemaining
        {
            get
            {
                return _scheduleAmountRemaining;
            }
            set
            {
                if (_scheduleAmountRemaining == value) return;

                _scheduleAmountRemaining = value;
                OnPropertyChanged("ScheduleAmountRemaining");
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

        private string _totalLiabilityAmount = "0.00 SAR";
        public string TotalLiabilityAmount
        {
            get
            {
                return _totalLiabilityAmount;
            }
            set
            {
                if (_totalLiabilityAmount == value) return;

                _totalLiabilityAmount = value;
                OnPropertyChanged("TotalLiabilityAmount");
            }
        }

        private string _AgreementNumber = "";
        public string AgreementNumber
        {
            get
            {
                return _AgreementNumber;
            }
            set
            {
                if (_AgreementNumber == value) return;

                _AgreementNumber = value;
                OnPropertyChanged("AgreementNumber");
            }
        }

        private InstalmentPlanModel _selectedOutletOption;
        public InstalmentPlanModel SelectedOutletOption
        {
            get
            {
                return _selectedOutletOption;
            }
            set
            {
                if (_selectedOutletOption == value) return;

                _selectedOutletOption = value;
                OnPropertyChanged("SelectedOutletOption");
            }
        }

        private List<Result31> _requestForInstalmentPlanList;
        public List<Result31> RequestForInstalmentPlanList
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

        private List<VtiaIaSetResult> _requestForScheduleList;
        public List<VtiaIaSetResult> RequestForScheduleList
        {
            get
            {
                return _requestForScheduleList;
            }
            set
            {
                if (_requestForScheduleList == value) return;

                _requestForScheduleList = value;
                OnPropertyChanged("RequestForScheduleList");
            }
        }



        private List<VtiaIadtSetResult> _requestForScheduleDetails;
        public List<VtiaIadtSetResult> RequestForScheduleDetails
        {
            get
            {
                return _requestForScheduleDetails;
            }
            set
            {
                if (_requestForScheduleDetails == value) return;

                _requestForScheduleDetails = value;
                OnPropertyChanged("RequestForScheduleDetails");
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



        public ObservableCollection<ATTACHMENTSetResults> attachments { get; set; }
        public ObservableCollection<ATTACHMENTSetResults> Attachments
        {
            get
            {
                return attachments;
            }
            set
            {
                if (attachments == value) return;

                attachments = value;
                OnPropertyChanged("Attachments");
            }
        }



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



        private bool _isAttachmentsListVisible = false;
        public bool IsAttachmentsListVisible
        {
            get
            {
                return _isAttachmentsListVisible;
            }
            set
            {
                if (_isAttachmentsListVisible == value) return;

                _isAttachmentsListVisible = value;
                OnPropertyChanged("IsAttachmentsListVisible");
            }
        }
        private bool _isPendingRequestsVisible = false;
        public bool IsPendingRequestsVisible
        {
            get
            {
                return _isPendingRequestsVisible;
            }
            set
            {
                if (_isPendingRequestsVisible == value) return;
                _isPendingRequestsVisible = value;
                OnPropertyChanged("IsPendingRequestsVisible");
            }
        }
        private bool _showRevisedDownPayment = false;
        public bool ShowRevisedDownPayment
        {
            get
            {
                return _showRevisedDownPayment;
            }
            set
            {
                if (_showRevisedDownPayment == value) return;
                _showRevisedDownPayment = value;
                OnPropertyChanged("ShowRevisedDownPayment");
            }
        }
        private bool _isVATRevokedVisible = false;
        public bool IsVATRevokedVisible
        {
            get
            {
                return _isVATRevokedVisible;
            }
            set
            {
                if (_isVATRevokedVisible == value) return;
                _isVATRevokedVisible = value;
                OnPropertyChanged("IsVATRevokedVisible");
            }
        }
        private bool _isVATRevokeInstalmentVisible = false;
        public bool IsVATRevokeInstalmentVisible
        {
            get
            {
                return _isVATRevokeInstalmentVisible;
            }
            set
            {
                if (_isVATRevokeInstalmentVisible == value) return;
                _isVATRevokeInstalmentVisible = value;
                OnPropertyChanged("IsVATRevokeInstalmentVisible");
            }
        }
        

        //private bool _isRevokeInstallmentPlanVisible = false;
        //public bool IsRevokeInstallmentPlanVisible
        //{
        //    get
        //    {
        //        return _isRevokeInstallmentPlanVisible;
        //    }
        //    set
        //    {
        //        if (_isRevokeInstallmentPlanVisible == value) return;
        //        _isRevokeInstallmentPlanVisible = value;
        //        OnPropertyChanged("IsRevokeInstallmentPlanVisible");
        //    }
        //}

        private string _netDownpayment = "0.00 SAR";
        public string NetDownPayment
        {
            get
            {
                return _netDownpayment;
            }
            set
            {
                if (_netDownpayment == value) return;
                _netDownpayment = value;
                OnPropertyChanged("NetDownPayment");
            }
        }
        private string _revisedDownPayment = "0.00 SAR";
        public string RevisedDownPayment
        {
            get
            {
                return _revisedDownPayment;
            }
            set
            {
                if (_revisedDownPayment == value) return;
                _revisedDownPayment = value;
                OnPropertyChanged("RevisedDownPayment");
            }
        }

        private ObservableCollection<Result31> vATRevokedList = new ObservableCollection<Result31>();
        public ObservableCollection<Result31> VATRevokedList
        {
            get
            {
                return vATRevokedList;
            }
            set
            {
                if (vATRevokedList == value)
                {
                    return;
                }
                vATRevokedList = value;
                OnPropertyChanged("VATRevokedList");
            }
        }
        private ObservableCollection<VATRevokeUiListModel> vATInstalmentRevokeList = new ObservableCollection<VATRevokeUiListModel>();
        public ObservableCollection<VATRevokeUiListModel> VATInstalmentRevokeList
        {
            get
            {
                return vATInstalmentRevokeList;
            }
            set
            {
                if (vATInstalmentRevokeList == value)
                {
                    return;
                }
                vATInstalmentRevokeList = value;
                OnPropertyChanged("VATInstalmentRevokeList");
            }
        }
        public ObservableCollection<PednRtn> pendingSetList { get; set; }
        public ObservableCollection<PednRtn> PendingSetList
        {
            get
            {
                return pendingSetList;
            }
            set
            {
                if (pendingSetList == value)
                {
                    return;
                }
                pendingSetList = value;
                OnPropertyChanged("PendingSetList");
            }
        }
        public VtiaBtnSetModel btnSetDetails { get; set; }
        public VtiaBtnSetModel BtnSetDetails
        {
            get
            {
                return btnSetDetails;
            }
            set
            {
                if (btnSetDetails == value) return;
                btnSetDetails = value;
                OnPropertyChanged("BtnSetDetails");
            }
        }
        public RequestToVATInstallmentPlanDetails _vatRevokeResponse { get; set; }
        public RequestToVATInstallmentPlanDetails VatRevokeResponse
        {
            get
            {
                return _vatRevokeResponse;
            }
            set
            {
                if (_vatRevokeResponse == value) return;
                _vatRevokeResponse = value;
                OnPropertyChanged("VatRevokeResponse");
            }
        }
        public RequestToVATInstallmentPlanDetails _vatRevokeRequest { get; set; }
        public RequestToVATInstallmentPlanDetails VatRevokeRequest
        {
            get
            {
                return _vatRevokeRequest;
            }
            set
            {
                if (_vatRevokeRequest == value) return;
                _vatRevokeRequest = value;
                OnPropertyChanged("VatRevokeResponse");
            }
        }
        private bool _notesPageVisible = false;
        public bool NotesPageVisible
        {
            get
            {
                return _notesPageVisible;
            }
            set
            {
                if (_notesPageVisible == value) return;
                _notesPageVisible = value;
                OnPropertyChanged("NotesPageVisible");
            }
        }
        private bool _isOTPPageVisible = false;
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
        private bool _showRevokeButton = false;
        public bool ShowRevokeButton
        {
            get
            {
                return _showRevokeButton;
            }
            set
            {
                if (_showRevokeButton == value) return;
                _showRevokeButton = value;
                OnPropertyChanged("ShowRevokeButton");
            }
        }

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
                if (_continueButtonEnability == value) return;

                _continueButtonEnability = value;

                OnPropertyChanged("ContinueButtonEnability");
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
                OnPropertyChanged("IsResendOTPEnabled");
            }
        }

        private ReqVatInstalmentPlanResponse rEQVatInstalmentPlanResponse { get; set; }
        public ReqVatInstalmentPlanResponse REQVatInstalmentPlanResponse
        {
            get
            {
                return rEQVatInstalmentPlanResponse;
            }
            set
            {
                if (rEQVatInstalmentPlanResponse == value) return;
                rEQVatInstalmentPlanResponse = value;
                OnPropertyChanged("REQVatInstalmentPlanResponse");
            }
        }

        private string captcha = string.Empty;
        public string Captcha
        {
            get
            {
                return captcha;
            }
            set
            {
                if (captcha == value) return;

                captcha = value;
                OnPropertyChanged("Captcha");
            }
        }
        private string guid = string.Empty;
        public string Guid
        {
            get
            {
                return guid;
            }
            set
            {
                if (guid == value) return;

                guid = value;
                OnPropertyChanged("Guid");
            }
        }

        private bool _isRevokeSuccessPageVisible = false;
        public bool IsRevokeSuccessPageVisible
        {
            get
            {
                return _isRevokeSuccessPageVisible;
            }
            set
            {
                if (_isRevokeSuccessPageVisible == value) return;

                _isRevokeSuccessPageVisible = value;
                OnPropertyChanged("IsRevokeSuccessPageVisible");
            }
        }

        public void AddOutletDecisionOptions()
        {

            var outletDecisionOptions = new ObservableCollection<InstalmentPlanModel>();
            outletDecisionOptions.Add(new InstalmentPlanModel
            {
                ActiveOutletDecisionOptions = AppResources.VATInstalmentRequestToVatInstalment,
                ActiveOutletDecisionOptionsIsSelected = false
            });
            outletDecisionOptions.Add(new InstalmentPlanModel
            {
                ActiveOutletDecisionOptions = AppResources.VATInstalmentRequestToVatDisplayInstalment,
                ActiveOutletDecisionOptionsIsSelected = false
            });
            //outletDecisionOptions.Add(new InstalmentPlanModel
            //{
            //    ActiveOutletDecisionOptions = AppResources.VATInstallmentRevoke,
            //    ActiveOutletDecisionOptionsIsSelected = false
            //});
            OutletDecisionOptions = outletDecisionOptions;
        }


        public void EnableVATLandingPage()
        {
            AddOutletDecisionOptions();

            IsVATLandingPageVisible = true;
            IsVATInstalmentPlanVisible = false;
            IsInstalmentSchedulePlanVisible = false;
            IsDisplayListVisible = false;
            IsDisplayDetailsVisible = false;
            IsPendingRequestsVisible = false;
            NotesPageVisible = false;
            IsOTPPageVisible = false;
            IsVATRevokedVisible = false;
            IsVATRevokeInstalmentVisible = false;
        }

        public void EnableVAtInstalmentPlan()
        {
            IsVATLandingPageVisible = false;
            IsVATInstalmentPlanVisible = true;
            IsInstalmentSchedulePlanVisible = false;
            IsDisplayListVisible = false;
            IsDisplayDetailsVisible = false;
            IsPendingRequestsVisible = false;
            NotesPageVisible = false;
            IsOTPPageVisible = false;
            IsVATRevokedVisible = false;
            IsVATRevokeInstalmentVisible = false;
        }

        public void EnableVAtInstalmentSummary()
        {
            IsVATLandingPageVisible = false;
            IsVATInstalmentPlanVisible = false;
            IsInstalmentSchedulePlanVisible = true;
            IsDisplayListVisible = false;
            IsDisplayDetailsVisible = false;
            IsPendingRequestsVisible = false;
            NotesPageVisible = false;
            IsOTPPageVisible = false;
            IsVATRevokedVisible = false;
            IsVATRevokeInstalmentVisible = false;
        }

        public void EnableDisplayInstalment()
        {
            IsVATLandingPageVisible = false;
            IsVATInstalmentPlanVisible = false;
            IsInstalmentSchedulePlanVisible = false;
            IsDisplayListVisible = true;
            IsDisplayDetailsVisible = false;
            IsPendingRequestsVisible = false;
            NotesPageVisible = false;
            IsOTPPageVisible = false;
            IsVATRevokedVisible = false;
            IsVATRevokeInstalmentVisible = false;
        }

        public void EnableDisplayDetails()
        {
            IsVATLandingPageVisible = false;
            IsVATInstalmentPlanVisible = false;
            IsInstalmentSchedulePlanVisible = false;
            IsDisplayListVisible = false;
            IsDisplayDetailsVisible = true;
            IsPendingRequestsVisible = false;
            NotesPageVisible = false;
            IsOTPPageVisible = false;
            IsVATRevokedVisible = false;
            IsVATRevokeInstalmentVisible = false;
        }
        public void ContinueNotesPage()
        {
            IsVATLandingPageVisible = false;
            IsVATInstalmentPlanVisible = false;
            IsInstalmentSchedulePlanVisible = false;
            IsDisplayListVisible = false;
            IsDisplayDetailsVisible = false;
            IsPendingRequestsVisible = false;
            NotesPageVisible = true;
            IsOTPPageVisible = false;
            IsVATRevokedVisible = false;
            IsVATRevokeInstalmentVisible = false;
        }
        public void ContinueOTPPage()
        {
            IsVATLandingPageVisible = false;
            IsVATInstalmentPlanVisible = false;
            IsInstalmentSchedulePlanVisible = false;
            IsDisplayListVisible = false;
            IsDisplayDetailsVisible = false;
            IsPendingRequestsVisible = false;
            NotesPageVisible = false;
            IsOTPPageVisible = true;
            IsVATRevokedVisible = false;
            IsVATRevokeInstalmentVisible = false;
        }

        internal void VATEnableRevoked()
        {
            IsVATRevokeInstalmentVisible = false;
            IsVATLandingPageVisible = false;
            IsVATInstalmentPlanVisible = false;
            IsInstalmentSchedulePlanVisible = false;
            IsDisplayListVisible = false;
            IsDisplayDetailsVisible = false;
            IsPendingRequestsVisible = false;
            NotesPageVisible = false;
            IsOTPPageVisible = false;
            IsVATRevokedVisible = true;
            _isReokeScreenExists = true;
        }

        internal void VATEnableRevokeInstalment()
        {
            IsVATRevokeInstalmentVisible = true;
            IsVATLandingPageVisible = false;
            IsVATInstalmentPlanVisible = false;
            IsInstalmentSchedulePlanVisible = false;
            IsDisplayListVisible = false;
            IsDisplayDetailsVisible = false;
            IsPendingRequestsVisible = false;
            NotesPageVisible = false;
            IsOTPPageVisible = false;
            IsVATRevokedVisible = false;
            _isReokeScreenExists = true;
            IsRevokeSuccessPageVisible = false;
        }

        public void ResetData()
        {
            VATInstalmentList = new ObservableCollection<Result31>();
            RequestForInstalmentPlanList = null;
            RequestForScheduleDetails = null;

        }

        public void SummaryData()
        {

            Attachments = null;
            SummarySelectedBillsList = null;


            NoOfInstalments = "";
            InstalmentAmount = "";
            PenaltyAmount = "";
            TotalAmount = "";
            TotalLiabilityAmount = "";
        }
        internal void EnableRevokeButtons()
        {
            if (BtnSetDetails.D != null)
            {
                foreach (var btnItem in BtnSetDetails.D.Results)
                {
                    if (btnItem.Button == "42")
                    {
                        ShowRevokeButton = true;
                    }
                }
            }
        }
        #region Button Action Declaration

        public void RequestInstalmentButtonClicked()
        {
            try
            {
                var x = String.Format(AppResources.VatAckMsg, "7");
                App.selectedVATItem = "";
                App.selectedVATItemFbust = "";

                CheckForPendingReturns();

            }
            catch (GAZTUnlockAccountException)
            {
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
        private void CheckForPendingReturns()
        {
            try
            {
                if (PendingSetList != null)
                {
                    PendingSetList.Clear();
                }
                else
                {
                    PendingSetList = new ObservableCollection<PednRtn>();
                }
                if (ReqVatInstalmentPlanResponseList.d != null && ReqVatInstalmentPlanResponseList.d.PednRtnSet.Count > 0)
                {
                    try
                    {
                        foreach (var pendigItem in ReqVatInstalmentPlanResponseList.d.PednRtnSet)
                        {
                            PendingSetList.Add(new PednRtn
                            {

                                Persl = pendigItem.Persl,
                                Perslt = pendigItem.Perslt,
                                Taxtp = pendigItem.Taxtp

                            });
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                    ShowPendigReturns();
                }
                else if (VATInstalmentList != null && VATInstalmentList.Count > 0)
                {


                    var list = VATInstalmentList.Where(i => i.Fbust == "E0001").ToList();

                    if (list.Count > 0) //inprogress
                    {
                        _dialogService.ShowMessage(String.Format(AppResources.RqstUnderProcess, list[0].Fbnum), AppResources.Information);
                        return;

                    }
                    list = VATInstalmentList.Where(i => i.Fbust == "E0013").ToList();
                    if (list.Count > 0)//Draft
                    {
                        _dialogService.ShowMessage(String.Format(AppResources.RqstSvdAsDraft, list[0].Fbnum), AppResources.Information);


                        return;
                    }
                    else
                    {
                        _navigationService.NavigateTo(App.VatInstalmentPlanPageView);
                    }
                }

                else
                {
                    _navigationService.NavigateTo(App.VatInstalmentPlanPageView);
                }
            }
            catch (Exception ex)
            {

            }
            
        }

        private void ShowPendigReturns()
        {
            IsPendingRequestsVisible = true;
            IsVATLandingPageVisible = false;
            IsVATInstalmentPlanVisible = false;
            IsInstalmentSchedulePlanVisible = false;
            IsDisplayListVisible = false;
            IsDisplayDetailsVisible = false;
            NotesPageVisible = false;
            IsOTPPageVisible = false;
        }
        #endregion


        private ReqVatInstalmentPlanResponse _reqVatInstalmentPlanResponseList;
        public ReqVatInstalmentPlanResponse ReqVatInstalmentPlanResponseList
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

        private ObservableCollection<Result31> _vATInstalmentList { get; set; }
        public ObservableCollection<Result31> VATInstalmentList
        {
            get
            {
                return _vATInstalmentList;
            }
            set
            {
                if (_vATInstalmentList == value) return;

                _vATInstalmentList = value;
                OnPropertyChanged("VATInstalmentList");
            }
        }

        public void BindVatInstalments()
        {
            if (ReqVatInstalmentPlanResponseList.d.ASSLISTSet != null)
            {
                var VATInstalmentListData = new ObservableCollection<Result31> ();

               
                RequestForInstalmentPlanList = ReqVatInstalmentPlanResponseList.d.ASSLISTSet.Where(w => w.Fbtyp.Contains("VTIA")).ToList();

                foreach (var instalmentListModel in RequestForInstalmentPlanList)
                {

                    VATInstalmentListData.Add(instalmentListModel);
                }

                VATInstalmentList = VATInstalmentListData;

                if (VATInstalmentList.Count > 0)
                {
                    NumberOfInstalmentPlans = RequestForInstalmentPlanList.Count + " " + AppResources.ZakatInstalmetPlan;

                }
            }
        }
        public void BindVatRevokeInstalments()
        {
            if (IsVATRevokedVisible)
            {
                if (rEQVatInstalmentPlanResponse.d.ASSLISTSet != null)
                {
                    var VATR = rEQVatInstalmentPlanResponse.d.ASSLISTSet.Where(i => i.Fbtyp == "VTIA").ToList();

                    VATRevokedList.Clear();
                    ObservableCollection<Result31> list = new ObservableCollection<Result31>();
                    foreach (var revokeInstalmentListModel in VATR)
                    {

                        list.Add(new Result31()
                        {
                            Fbnum = revokeInstalmentListModel.Fbnum,
                            FbustTxt = revokeInstalmentListModel.FbustTxt,
                            Gpart = revokeInstalmentListModel.Gpart,
                            FbtText = revokeInstalmentListModel.FbtText,
                        });
                    }
                    VATRevokedList = list;

                }
            }
            else if (IsVATRevokeInstalmentVisible)//VTIR
            {
                if (rEQVatInstalmentPlanResponse.d.RevokeListSet != null)
                {
                    VATInstalmentRevokeList.Clear();
                    ObservableCollection<VATRevokeUiListModel> list = new ObservableCollection<VATRevokeUiListModel>();
                    foreach (var revokeInstalmentListModel in rEQVatInstalmentPlanResponse.d.RevokeListSet)
                    {
                        list.Add(new VATRevokeUiListModel()
                        {
                            Fbnum = revokeInstalmentListModel.Fbnum,
                            DpAmt = revokeInstalmentListModel.DpAmt,
                            SubmitDate = revokeInstalmentListModel.SubmitDt.ToShortDateString(),
                            DueAmt = revokeInstalmentListModel.DueAmt,
                            PlanDur = revokeInstalmentListModel.PlanDur,
                            TotAmt = revokeInstalmentListModel.TotAmt,

                        });
                    }
                    VATInstalmentRevokeList = list;
                }
            }





        }
        public async Task showInstructionDialog()
        {
            await MopupService.Instance.PushAsync(new VATInstalmentPopupNotesPageView());
        }

      
        #region API Methods

        #region GETVatInstalmentPlan

        public async Task GetVATInstalmentPlanList()
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
                    //  ReqVatInstalmentPlanResponse rEQVatInstalmentPlanResponse = null;
                    try
                    {

                        rEQVatInstalmentPlanResponse = await VATInstalationPlanWebServiceManager.GetRequestToVATInstalmentData();
                        ReqVatInstalmentPlanResponseList = rEQVatInstalmentPlanResponse;

                        // If seesion Expired it will navigate to Dashboard page
                        PopToRootPage();


                        if (ReqVatInstalmentPlanResponseList != null && ReqVatInstalmentPlanResponseList.d != null)
                        {
                            BindVatInstalments();
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
                //await Task.Run(() =>
                //{

                //});
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


        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Microsoft.Maui.Controls.Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });
            }
        }


        #endregion


        public async Task GetDetailsClicked(int index)
        {
            await GetVATInstalmentPlanDetails(index);

        }

        public async Task GetDisplayDetailsClicked(int index)
        {

            await GetVATDisplayScheduleDetails(index);

        }

        //Static data for Summary

        public void PopulateSummaryReasonData(RequestToVATInstallmentPlanDetails itemDetails)
        {
            SummarySelectedBillsList = new ObservableCollection<ZakatSelectBillModel>();
            foreach (var bill in itemDetails.d.VTIASet)
            {
                if (bill.Xsele == "X")
                {

                    SummarySelectedBillsList.Add(new ZakatSelectBillModel()
                    {
                        billNumber = AppResources.Bill + " " + (SummarySelectedBillsList.Count + 1).ToString("00") + ":",
                        amount = bill.Betrh,
                        saadNumber = bill.SadadNo,
                        taxPeriod = bill.Taxperioddsc,
                        isSelected = false,
                        billType = AppResources.ZakatInstalmetSelectTypeVAT
                    });

                }



            }



            var attachments = new ObservableCollection<ATTACHMENTSetResults>();

            foreach (var attach in itemDetails.d.ATTACHMENTSet)
            {
                if (string.IsNullOrEmpty(attach.Filename))
                {
                    attach.Filename = DateTime.Now.ToString("yyyy/MM/dd");
                }
                attachments.Add(attach);
            }

            Attachments = attachments;

            if (Attachments.Count == 0)
            {
                IsAttachmentsListVisible = false;
            }
            else
            {
                IsAttachmentsListVisible = true;
            }

            NoOfInstalments = itemDetails.d.Noofinstallment;
            InstalmentAmount = string.Format("{0:N2}", double.Parse(itemDetails.d.TotInvAmt)) + " " + AppResources.ZSAR;
            PenaltyAmount = string.Format("{0:N2}", double.Parse(itemDetails.d.Peneltyamt));
            TotalAmount = string.Format("{0:N2}", double.Parse(itemDetails.d.Totdueamt));
            NetDownPayment = string.Format("{0:N2}", double.Parse(itemDetails.d.Totdownpymtamt));
            if (itemDetails.d.DownpymtChange == "X")
            {
                ShowRevisedDownPayment = true;
            }
            else
            {
                ShowRevisedDownPayment = false;
            }

            RevisedDownPayment = string.Format("{0:N2}", double.Parse(itemDetails.d.TotdownpymtamtSu));
            TotalLiabilityAmount = string.Format("{0:N2}", double.Parse(itemDetails.d.VTISSet[0].Betrw));

        }


        #region GETVatInstalmentPlan

        public async Task GetVATInstalmentPlanDetails(int index)
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

                        var selectedItem = RequestForInstalmentPlanList[index];


                        var selectedItemFormID = await VATInstalationPlanWebServiceManager.GAZTGetFbGuidDetailsInputData(App.LoginDataRetrieved.FbGuid, selectedItem.Fbnum, App.LoginDataRetrieved.TIN, selectedItem.Fbust, "VTIA");
                        

                        if (selectedItemFormID.d != null)
                        {

                            var itemDetails = await VATInstalationPlanWebServiceManager.GetRequestToVATInstalmentPlanDetails("", selectedItemFormID.d.Fbguid);

                            if (itemDetails != null && itemDetails.d != null)
                            {
                                PopulateSummaryReasonData(itemDetails);
                            }


                        }


                        PopToRootPage();


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


        #endregion

        #region GetVATDisplaySchedule

        public async Task GetVATDisplaySchedule()
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


                        var getFormID = await VATInstalationPlanWebServiceManager.GAZTGetFbGuidDetailsInputData(App.LoginDataRetrieved.FbGuid, "", App.LoginDataRetrieved.TIN, "E0045", "VTIA");


                        if (getFormID.d != null)
                        {
                            FormGuidValue = getFormID.d.Fbguid;


                            DisplayInstallmentAgreementSchedulePlan itemDetails = await VATInstalationPlanWebServiceManager.GetDisplayInstallmentAgreementSchedulePlan(formGuid: FormGuidValue);
                            // var itemDetails = await WebServiceManager.GetRequestToVATInstalmentPlanDetails("", getFormID.d.Fbguid);

                            if (itemDetails != null && itemDetails.d != null)
                            {

                                RequestForScheduleList = itemDetails.d.VtiaIahdSet;
                            }


                        }


                        PopToRootPage();
                        // If seesion Expired it will navigate to Dashboard page

                        // EnableSlectionView();


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


        #endregion

        #region GetVATDisplayScheduleDetails

        public async Task GetVATDisplayScheduleDetails(int index)
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
                    //VatInstalments = null;
                    //VatInstalmentPlanResponse vATInstalment = null;
                    try
                    {
                        var selectedItem = RequestForScheduleList[index];

                        AgreementNumber = selectedItem.AgreementNo;

                        var itemDetails = await VATInstalationPlanWebServiceManager.GetDisplayInstallmentScheduleDetails(selectedItem.Opbel, FormGuidValue, "");
                        // var itemDetails = await WebServiceManager.GetRequestToVATInstalmentPlanDetails("", getFormID.d.Fbguid);

                        if (itemDetails != null && itemDetails.d != null)
                        {

                            RequestForScheduleDetails = itemDetails.d.VtiaIadtSet;
                            // RequestForScheduleList = itemDetails.d.VtiaIahdSet.Results;

                            for (int i = 0; i < RequestForScheduleDetails.Count; i++)
                            {
                                DateTime dateStart = new DateTime();

                                string apiDate = @"""" + RequestForScheduleDetails[i].DueDate + @"""";
                                dateStart = JsonConvert.DeserializeObject<DateTime>(apiDate);


                                GregorianCalendar hjCalendar = new GregorianCalendar();
                                int year = hjCalendar.GetYear(dateStart);
                                int month = hjCalendar.GetMonth(dateStart);
                                int day = hjCalendar.GetDayOfMonth(dateStart);

                                string dateStr = string.Format("{0:00}/{1}/{2}", day, month, year);

                                RequestForScheduleDetails[i].DueDate = dateStr;

                                string dt1 = string.Empty;
                                string[] dts = null;
                                dts = RequestForScheduleDetails[i].DueDate.Split('/');
                                //dt1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];
                                dt1 = dts[0] + "-" + dts[1] + "-" + dts[2];
                                RequestForScheduleDetails[i].DueDate = dt1;

                            }
                            ScheduleNoOfMonths = itemDetails.d.Noofmon;
                            ScheduleAmountRemaining = string.Format("{0:N2}", double.Parse(itemDetails.d.TotalRemAmnt));
                            ScheduleMonthlyInstalment = string.Format("{0:N2}", double.Parse(itemDetails.d.TotalInstall));
                            ScheduleTotalAmountPaid = string.Format("{0:N2}", double.Parse(itemDetails.d.TotalAmntPaid));

                        }


                        PopToRootPage();

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


        #endregion

        #region GetVATRevokeList
        public async Task GetVATRevokeList()
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

                        rEQVatInstalmentPlanResponse = await VATInstalationPlanWebServiceManager.GetRequestToVATInstalmentData();

                        PopToRootPage();


                        if (rEQVatInstalmentPlanResponse != null && rEQVatInstalmentPlanResponse.d != null)
                        {
                            BindVatRevokeInstalments();
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
                //await Task.Run(() =>
                //{

                //});
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
        #endregion

        #region callcaptcha
        internal async Task CallCaptchaApiAsync(string guid)
        {
            try
            {

                IsLoading = true;

                string lang = UtilityManager.GetLanguageParameter();
                string st = ZATCAConstants.CaptchaAndGUID;
                string type = "ZDP_CREATE_CAPTCHA_SRV.Header";// "ZDP_FRGT_USRNM_PWD_SRV.Header";
                GenerateCaptchaGUID forgotPasswordOTP = new GenerateCaptchaGUID();
                Models.Metadata metadata = new Models.Metadata();
                metadata.id = st;
                metadata.uri = st;
                metadata.type = type;

                GetCaptcha d = new GetCaptcha();
                d.__metadata = metadata;
                d.captchaCode = "";
                d.GUID = guid;
                d.taxpayer = "";
                d.refresh = "";
                d.applicationName = "VTIR";

                forgotPasswordOTP.result = d;
                forgotPasswordOTP = await WebServiceManager.GAZTCaptchaAndGUID(d);
                PopToRootPage();// If seesion Expired it will navigate to Dashboard page

                if (forgotPasswordOTP?.result != null && !string.IsNullOrEmpty(forgotPasswordOTP.result.captchaCode))
                {
                    Captcha = forgotPasswordOTP.result.captchaCode;
                    Guid = forgotPasswordOTP.result.GUID;
                    IsAPICalledSuccessfully = true;
                }
                else
                {

                }


                IsLoading = false;
            }

            catch (InternetException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                //   await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                await Task.Run(() =>
                {
                    IsLoading = false;

                    //SetIDNumberEnability = true;
                    //IDNumber = String.Empty;
                    // UserIDLayoutVisibility = true;
                });
            }
        }
        #endregion
        #region SendOTPForVatRevokeAsync
        internal async Task SendOTPForVatRevokeAsync()
        {
            IsLoading = true;
            try
            {
                VatRevokeResponse.d.Operationz = "85";
                VatRevokeResponse.d.Captcha = Captcha;
                VatRevokeResponse.d.OtpGuid = Guid;
                VatRevokeResponse.d.NOTESSet = new List<NOTESSetResults>();//VATNOTESSets();
                VatRevokeResponse.d.NOTESSet = new List<NOTESSetResults>();//List<NOTES>();
                VatRevokeResponse.d.QuesListSet = new List<QuesSet>();
                VatRevokeResponse.d.INSTRUCTIONSet = new List<RequestToVATInstallmentPlanDetails.Instructions>();

                var result = await VATInstalationPlanWebServiceManager.SaveVATInstalmentRevokeData(VatRevokeResponse);
                RequestToVATInstallmentPlanDetails _vatResponseObject = JsonConvert.DeserializeObject<RequestToVATInstallmentPlanDetails>(result);
                IsLoading = false;
                if (_vatResponseObject == null || _vatResponseObject.d == null)
                {
                    WebServiceManager.ErrorMessage = string.Empty;
                    ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(result);
                    if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                    {
                        WebServiceManager.ErrorMessage = errorMesg.error.innererror.errordetails[0].message;
                        await _dialogService.ShowMessage(WebServiceManager.ErrorMessage, AppResources.Information);
                        //  throw new GAZTVATRegistrationInProcessException(WebServiceManager.ErrorMessage);
                    }
                    else
                    {
                        await MopupService.Instance.PopAsync();
                    }
                }
                else
                {
                    VatRevokeRequest = _vatResponseObject;
                }
                IsLoading = false;
            }
            catch (Exception ex)
            {
                IsLoading = false;
            }

        }
        #endregion
        #region SendSubmitVatRevokeAsync
        internal async Task SendSubmitVatRevokeAsync(string Otp, string Notes)
        {
            IsLoading = true;
            try
            {
                VatRevokeRequest.d.Operationz = "86";
                //VatRevokeRequest.d.NOTESSet = PrepareNotes(Notes);
                VatRevokeRequest.d.Otp = Otp;

                VatRevokeRequest.d.QuesListSet = new List<QuesSet>();
                VatRevokeRequest.d.INSTRUCTIONSet = new List<RequestToVATInstallmentPlanDetails.Instructions>();



                var result = await VATInstalationPlanWebServiceManager.SaveVATInstalmentRevokeData(VatRevokeRequest);
                RequestToVATInstallmentPlanDetails _vatResponseObject = JsonConvert.DeserializeObject<RequestToVATInstallmentPlanDetails>(result);
                IsLoading = false;
                if (_vatResponseObject == null || _vatResponseObject.d == null)
                {
                    WebServiceManager.ErrorMessage = string.Empty;
                    ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(result);
                    if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                    {
                        WebServiceManager.ErrorMessage = errorMesg.error.innererror.errordetails[0].message;
                        //  await MopupService.Instance.PopAsync();
                        await _dialogService.ShowMessage(WebServiceManager.ErrorMessage, AppResources.Information);
                        //  throw new GAZTVATRegistrationInProcessException(WebServiceManager.ErrorMessage);
                    }
                    else
                    {
                        //Something went wrong
                        await MopupService.Instance.PopAsync();
                    }
                }
                else
                {
                    await MopupService.Instance.PopAsync();
                    await _dialogService.ShowMessageBox(AppResources.VATRevokeSuccessMessage, AppResources.Information);

                    EnableVATLandingPage();

                }
            }
            catch (Exception ex)
            {
                IsLoading = false;
            }

        }
        #endregion
        #region CreateNewRequest
        public async Task CreateNewRequest()
        {
            try
            {
                VATEnableRevokeInstalment();
                await GetVATRevokeList();

            }

            catch (Exception ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
        }
        #endregion
        #region PrepareNotes
        private VATNOTESSets PrepareNotes(string notes)
        {
            VATNOTESSets OffNotesList = new VATNOTESSets();
            List<NOTES> NotesList = new List<NOTES>();
            NOTES notsObj = new NOTES();
            notsObj.Notenoz = "5";
            notsObj.Refnamez = "";
            notsObj.DataVersionz = "00000";
            notsObj.XInvoicez = "";
            notsObj.XObsoletez = "";
            notsObj.Rcodez = "VTIR_RETP";
            notsObj.Erfusrz = "";
            notsObj.Erfdtz = null;
            notsObj.ByGpartz = App.LoginDataRetrieved.TIN;
            notsObj.AttByz = "TP";
            notsObj.Noteno = "5";
            notsObj.Lineno = 1;
            notsObj.ElemNo = 0;
            notsObj.Tdformat = "";
            notsObj.Tdline = notes;
            NotesList.Add(notsObj);
            OffNotesList.results = NotesList;
            return OffNotesList;
        }
        #endregion
        #endregion

    }
}
