using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Newtonsoft.Json;
using Mopups.Services;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.ZakatObjectionsModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.Instructions;
using ZATCAMAUI.Views.NewDesign.Common;
using ZATCAMAUI.Views.NewDesign.ZakatObjection;
using ZATCAMAUI.Core.Interfaces;
using static ZATCAMAUI.Models.ZakatObjectionsModel.ZakatObjectionWithDrawListModel;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatObjectionViewModel
{
    public class ZakatObjectionViewModel : BaseViewModel
    {
        #region Enums

        enum PagesEnum
        {
            BillsPage,
            DetailsPage,
            ReviewReason,
            AttachmentsPage,
            ReviewDetails,
            SecurityPayments,
            Declaration,
            Summary,
            WithdrawObjectiondetails,
            WithedrawAttachments

        }

        public enum PickerEnum
        {
            ReviewReason,
            ReviewSubReason,
            ApplicationReferenceNumber,
            IDType,
        }

        #endregion

        #region Commands

        public ICommand DownloadAcknowledgement { get; set; }
        public ICommand ZAkatObjectionsCommand { get; set; }
        public ICommand InstalmentCopyCommand { get; set; }
        public ICommand OnAppearingZakatObjectionPageViewCommand { get; set; }
        public ICommand SummaryAttachmentsTapCommand { get; set; }
        public ICommand BillContinueBtnTapped { get; set; }
        public ICommand ObjectionDetailsWithDrawlContinueTapped { get; set; }
        public ICommand IsObjectionDetailsTapped { get; set; }
        public ICommand ObjectionDetailsReasonTapped { get; set; }
        public ICommand ObjectionAttachmentsContinueTapped { get; set; }
        public ICommand AttachmentsContinueTapped { get; set; }

        public ICommand DeclarationContinueBtnTapped { get; set; }




        public ICommand ReviewDetailsConBtnTapped { get; set; }
        public ICommand SecurityPaymentConBtnTapped { get; set; }
        public ICommand DeclarationConBtnTapped { get; set; }
        public ICommand WithdrawBtnTapped { get; set; }
        public ICommand SummaryConBtnTapped { get; set; }
        public ICommand WithDrawObjectionConBtnTapped { get; set; }
        public ICommand IsWithDrawDetailsTapped { get; set; }
        public ICommand WithdrawAttachmentTapped { get; set; }
        public ICommand WithdrawAttachmentTappedTwo { get; set; }
        public ICommand WithdrAttachmentsContinueTapped { get; set; }
        public ICommand Download_Acknowledgement { get; set; }
        public ICommand ZDownloadForm { get; set; }

        public ICommand GoBackClick { get; set; }

        #endregion

        int selectedPage = (int)PagesEnum.BillsPage;

        private bool _isBackVisible = true;

        public bool IsBackVisible
        {
            get { return _isBackVisible; }
            set
            {
                if (_isBackVisible == value) return;
                _isBackVisible = value;
                OnPropertyChanged("IsBackVisible");
            }
        }


        string inputData = "";
        public string InputData
        {
            set
            {
                if (inputData == value) return;

                if (inputData != value)
                {
                    inputData = value;
                    OnPropertyChanged("InputData");
                }
            }
            get
            {
                return inputData;
            }
        }

        private string _selectedFbNum = "";

        public string SelectedFbNum
        {
            get { return _selectedFbNum; }
            set
            {
                if (_selectedFbNum == value) return;

                _selectedFbNum = value;
                OnPropertyChanged("SelectedFbNum");
            }
        }

        private string _selectedFbType = "";

        public string SelectedFbType
        {
            get { return _selectedFbType; }
            set
            {
                if (_selectedFbType == value) return;

                _selectedFbType = value;
                OnPropertyChanged("SelectedFbNum");
            }
        }

        private bool _isWithDrawEnable = false;
        public bool IsWithDrawEnable
        {
            get { return _isWithDrawEnable; }
            set
            {
                if (_isWithDrawEnable == value) return;

                _isWithDrawEnable = value;
                WithdrawBackgroundColor = (_isWithDrawEnable ? (Color)Application.Current.Resources["Secondary"] : (Color)Application.Current.Resources["ButtonGray"]);
                OnPropertyChanged("IsWithDrawEnable");
            }
        }

        private Color _WithdrawBackgroundColor = (Color)Application.Current.Resources["Secondary"];
        public Color WithdrawBackgroundColor
        {
            get
            {
                return _WithdrawBackgroundColor;
            }
            set
            {
                if (_WithdrawBackgroundColor == value)
                {
                    return;
                }
                _WithdrawBackgroundColor = value;
                OnPropertyChanged("WithdrawBackgroundColor");
            }
        }

        private bool _isSubmitEnable = true;

        public bool IsSubmitEnable
        {
            get { return _isSubmitEnable; }
            set
            {
                _isSubmitEnable = value;
                OnPropertyChanged("IsSubmitEnable");
            }
        }

        private string _DetailDescriptionNote = "";
        public string DetailDescriptionNote
        {
            get
            {
                return _DetailDescriptionNote;
            }
            set
            {
                if (_DetailDescriptionNote == value) return;

                _DetailDescriptionNote = value;
                OnPropertyChanged("DetailDescriptionNote");
            }
        }

        private bool _IsWithdrawAttachmentsVisible = false;
        public bool IsWithdrawAttachmentsVisible
        {
            get
            {
                return _IsWithdrawAttachmentsVisible;
            }
            set
            {
                if (_IsWithdrawAttachmentsVisible == value) return;

                _IsWithdrawAttachmentsVisible = value;
                OnPropertyChanged("IsWithdrawAttachmentsVisible");
            }
        }

        private string _RemarkNote = "";
        public string RemarkNote
        {
            get
            {
                return _RemarkNote;
            }
            set
            {
                if (_RemarkNote == value) return;

                _RemarkNote = value;
                OnPropertyChanged("RemarkNote");
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
                OnPropertyChanged("IsSadadSecuritySelected");
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
                OnPropertyChanged("IsBankGurantSecuritySelected");
            }
        }

        public string _fiscalYear = "";

        public string FiscalYear
        {
            get { return _fiscalYear; }
            set
            {
                if (_fiscalYear == value) return;

                _fiscalYear = value;
                OnPropertyChanged("FiscalYear");
            }
        }

        public string _financialPeriod = "";

        public string FinancialPeriod
        {
            get { return _financialPeriod; }
            set
            {
                if (_financialPeriod == value) return;

                _financialPeriod = value;
                OnPropertyChanged("FinancialPeriod");
            }
        }

        public string _referenceNum = "";

        public string ReferenceNum
        {
            get { return _referenceNum; }
            set
            {
                if (_referenceNum == value) return;

                _referenceNum = value;
                OnPropertyChanged("ReferenceNum");
            }
        }
        public string _NewTaxType = "";
        public string NewTaxType
        {
            get { return _NewTaxType; }
            set
            {
                if (_NewTaxType == value) return;

                _NewTaxType = value;
                OnPropertyChanged("NewTaxType");
            }
        }

        public string _NewFinancialPeriod = "";
        public string NewFinancialPeriod
        {
            get { return _NewFinancialPeriod; }
            set
            {
                if (_NewFinancialPeriod == value) return;

                _NewFinancialPeriod = value;
                OnPropertyChanged("NewFinancialPeriod");
            }
        }


        public string _taxType = "";

        public string TaxType
        {
            get { return _taxType; }
            set
            {
                if (_taxType == value) return;

                _taxType = value;
                OnPropertyChanged("TaxType");
            }
        }

        public string _assessmentAmountGAZT = "";

        public string AssessmentAmountGAZT
        {
            get { return _assessmentAmountGAZT; }
            set
            {
                if (_assessmentAmountGAZT == value) return;

                _assessmentAmountGAZT = value;
                OnPropertyChanged("AssessmentAmountGAZT");
            }
        }

        public string _disputeAmount = "";

        public string DisputeAmount
        {
            get { return _disputeAmount; }
            set
            {
                if (_disputeAmount == value) return;

                _disputeAmount = value;
                OnPropertyChanged("DisputeAmount");
            }
        }

        public string _revisedAmount = "";

        public string RevisedAmount
        {
            get { return _revisedAmount; }
            set
            {
                if (_revisedAmount == value) return;

                _revisedAmount = value;
                OnPropertyChanged("RevisedAmount");
            }
        }

        //CR4912
        public string _zakatrevamt = "";

        public string ZAKTREVAMt
        {
            get { return _zakatrevamt; }
            set
            {
                if (_zakatrevamt == value) return;

                _zakatrevamt = value;
                OnPropertyChanged("ZAKTREVAMt");
            }
        }

        public string _dispamtcit = "";

        public string DispAmtCIT
        {
            get { return _dispamtcit; }
            set
            {
                if (_dispamtcit == value) return;

                _dispamtcit = value;
                OnPropertyChanged("DispAmtCIT");
            }
        }

        public string _dispamtZAKT = "";

        public string DispAmtZAKT
        {
            get { return _dispamtZAKT; }
            set
            {
                if (_dispamtZAKT == value) return;

                _dispamtZAKT = value;
                OnPropertyChanged("DispAmtZAKT");
            }
        }
        //end CR4912

        public string _objectionReasons = "";

        public string ObjectionReasons
        {
            get { return _objectionReasons; }
            set
            {
                if (_objectionReasons == value) return;

                _objectionReasons = value;
                OnPropertyChanged("ObjectionReasons");
            }
        }

        public string _applicantName = "";

        public string ApplicantName
        {
            get { return _applicantName; }
            set
            {
                if (_applicantName == value) return;

                _applicantName = value;
                OnPropertyChanged("ApplicantName");
            }
        }

        public string _capacity = "";

        public string Capacity
        {
            get { return _capacity; }
            set
            {
                if (_capacity == value) return;

                _capacity = value;
                OnPropertyChanged("Capacity");
            }
        }

        public string _repFullName = "";

        public string RepFullName
        {
            get { return _repFullName; }
            set
            {
                if (_repFullName == value) return;

                _repFullName = value;
                OnPropertyChanged("RepFullName");
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
                OnPropertyChanged("SecurityAmount");
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
                OnPropertyChanged("SADADNumber");
            }
        }

        public string _repPhoneNo = "";

        public string RepPhoneNo
        {
            get { return _repPhoneNo; }
            set
            {
                if (_repPhoneNo == value) return;

                _repPhoneNo = value;
                OnPropertyChanged("RepPhoneNo");
            }
        }

        public string _repFaxNo = "";

        public string RepFaxNo
        {
            get { return _repFaxNo; }
            set
            {
                if (_repFaxNo == value) return;

                _repFaxNo = value;
                OnPropertyChanged("RepFaxNo");
            }
        }

        public string _repElectronicMail = "";

        public string RepElectronicMail
        {
            get { return _repElectronicMail; }
            set
            {
                if (_repElectronicMail == value) return;

                _repElectronicMail = value;
                OnPropertyChanged("RepElectronicMail");
            }
        }

        public string _repDesignation = "";

        public string RepDesignation
        {
            get { return _repDesignation; }
            set
            {
                if (_repDesignation == value) return;

                _repDesignation = value;
                OnPropertyChanged("RepDesignation");
            }
        }

        public string _repBuildingName = "";

        public string RepBuildingName
        {
            get { return _repBuildingName; }
            set
            {
                if (_repBuildingName == value) return;

                _repBuildingName = value;
                OnPropertyChanged("RepBuildingName");
            }
        }

        public string _repLevelStreetNumber = "";

        public string RepLevelStreetNumber
        {
            get { return _repLevelStreetNumber; }
            set
            {
                if (_repLevelStreetNumber == value) return;

                _repLevelStreetNumber = value;
                OnPropertyChanged("RepLevelStreetNumber");
            }
        }

        public string _repCity = "";

        public string RepCity
        {
            get { return _repCity; }
            set
            {
                if (_repCity == value) return;

                _repCity = value;
                OnPropertyChanged("RepCity");
            }
        }
        private string _vATReferanceNumber = string.Empty;
        public string VATReferanceNumber
        {
            get
            {
                return _vATReferanceNumber;
            }
            set
            {
                if (_vATReferanceNumber == value) return;

                _vATReferanceNumber = value;
                OnPropertyChanged("VATReferanceNumber");
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
        public class BillsModel
        {
            public BillsModel()
            {
            }

            public string FiscalYear { get; set; }
            public string FinancialPeriod { get; set; }
            public string ReferenceNum { get; set; }
            public string TaxType { get; set; }
            public string AssessmentAmountGAZT { get; set; }
            public string RevisedAmount { get; set; }

        }
        public class SelectionModel
        {
            public SelectionModel()
            {
            }
            public string SelectionTitle { get; set; }
            public bool IsSelected { get; set; }
            public bool IsNotSelected { get; set; }
        }

        public ObservableCollection<BillsModel> returnBills { get; set; }

        public ObservableCollection<BillsModel> ReturnBills
        {
            get { return returnBills; }

            set
            {
                /* if (returnBills == value)
                 {
                     return;
                 }
 */
                returnBills = value;
                OnPropertyChanged("ReturnBills");
            }
        }



        public ObservableCollection<SelectionModel> securityPaymentOptions { get; set; }

        public ObservableCollection<SelectionModel> SecurityPaymentOptions
        {
            get { return securityPaymentOptions; }

            set
            {
                if (securityPaymentOptions == value)
                {
                    return;
                }

                securityPaymentOptions = value;
                OnPropertyChanged("SecurityPaymentOptions");
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
                OnPropertyChanged("AttachmentsListViewData");
            }
        }

        private bool isBankGurantee = false;

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
                OnPropertyChanged("BankGuranteeAttachmentsListViewData");
            }
        }

        public ZakatObjectionViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

            GoBackClick = new Command(() => { BackNavigations(); });
            BillContinueBtnTapped = new Command(async () =>
            {
                IsLoading = true;
                await GetWithdrawFBNums();
                VATReferanceNumber = SelectedFbNum;
                EnableSummaryView();
                IsLoading = false;
            });

            DownloadAcknowledgement = new Command(async () =>
            {
                IsLoading = true;
                if (VATReferanceNumber != null)
                {

                    string downloadurl = ZATCAConstants.downloadFile + "'" + VATReferanceNumber + "')/$value";
                    await _navigationService.NavigateTo(App.PdfView, downloadurl);

                }
                IsLoading = false;
            });

            InstalmentCopyCommand = new Command(async () =>
            {

                try
                {
                    if (VATReferanceNumber != null)
                    {
                        await Clipboard.SetTextAsync(VATReferanceNumber);
                        if (Clipboard.HasText)
                        {
                            var text = await Clipboard.GetTextAsync();
                            await _dialogService.ShowMessageBox(AppResources.NDReferenceNumber + " " + text, AppResources.Copied);

                        }
                    }
                }
                catch (Exception)
                {
                }

            });

            ZAkatObjectionsCommand = new Command(async () =>
            {

                try
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    foreach (var item in _navigation.NavigationStack)
                    {
                        if (item.GetType().Name == App.ZakatObjectionPageView)
                        {
                            _navigation.RemovePage(item);
                            break;
                        }
                    }

                    foreach (var item in _navigation.NavigationStack)
                    {
                        if (item.GetType().Name == App.ZakatObjectionsListPageView)
                        {
                            _navigation.RemovePage(item);
                            break;
                        }
                    }

                    foreach (var item in _navigation.NavigationStack)
                    {
                        if (item.GetType().Name == App.ZakatObjectionSuccessPageView)
                        {
                            _navigation.RemovePage(item);
                            break;
                        }
                    }

                    await _navigationService.NavigateTo(App.ZakatObjectionsListPageView);

                }
                catch (Exception)
                {
                }

            });

            OnAppearingZakatObjectionPageViewCommand = new Command(async () =>
            {
                IsLoading = true;
                ResetData();
                await GetZakatObjectionsData();
                MessagingCenter.Subscribe<object, AttachmentsList>(this, "AttachmentReceived", (sender, arg) =>
                {
                    if (arg != null)
                    {
                        PopulateAttachments(arg.results);
                    }
                });
                IsLoading = false;
            });

            SummaryAttachmentsTapCommand = new Command<object>(async (obj) =>
            {
                try
                {
                    IsLoading = true;
                    var attachment = (obj as Syncfusion.Maui.ListView.ItemTappedEventArgs).DataItem as Attachment;

                    string Extention = attachment.Filename.Split('.')[1];
                    if (Extention.Equals("PDF") || Extention.Equals("pdf"))
                    {
                        if (attachment.DocUrl != null)
                        {
                            await _navigationService.NavigateTo(App.PdfView, attachment.DocUrl);
                        }
                    }
                    else
                    {
                        await GetVATReviewWebServiceManager.email(attachment.Doguid, attachment);
                    }



                    IsLoading = false;
                }
                catch (Exception)
                {

                    IsLoading = false;
                }
            });

            IsObjectionDetailsTapped = new Command(() =>
            {
                EnableSecurityPaymentsView();
            });
            SecurityPaymentConBtnTapped = new Command(() =>
            {
                EnableAttachmentsView();
            });

            AttachmentsContinueTapped = new Command(() =>
            {

                EnableDeclarationView();
            });

            DeclarationContinueBtnTapped = new Command(async () =>
            {
                IsLoading = true;
                await GetWithdrawFBNums();
                VATReferanceNumber = SelectedFbNum;
                EnableSummaryView();
                IsLoading = false;
            });

            Download_Acknowledgement = new Command(async () =>
            {
                IsLoading = true;
                if (VATReferanceNumber != null)
                {
                    string downloadurl = ZATCAConstants.ZOdownloadAckLetter + VATReferanceNumber;
                    await _navigationService.NavigateTo(App.PdfView, downloadurl);

                }
                IsLoading = false;
            });
            ZDownloadForm = new Command(async () =>
            {
                IsLoading = true;
                if (VATReferanceNumber != null)
                {
                    string downloadurl = ZATCAConstants.ZOdownloadCoverFormFile + VATReferanceNumber;
                    await _navigationService.NavigateTo(App.PdfView, downloadurl);

                }
                IsLoading = false;
            });

            WithdrawBtnTapped = new Command(async () =>
            {

                IsLoading = true;
                if (IsWithDrawEnable)
                {

                    await MopupService.Instance.PushAsync(new InstructionsBottomPopUpView(isWithCancelOption: true, instructionString: AppResources.ZOWIthdrawInstructions, checkBoxString: AppResources.ZakatInstructionsCheckBoxDesc, continueString: AppResources.CRContinue,
               _dialogType: InstructionsBottomPopUpViewModel.DialogType
                   .Instructions));

                    await GetWithdrawReviewReason();
                    EnableWithdrawObjectionDetails();
                }
                IsLoading = false;
            });

            SummaryConBtnTapped = new Command(async () =>
            {
                IsLoading = true;
                SuccessMessage = AppResources.NDZakatObjectionIsSubmittedSuccessfully;
                VATReferanceNumber = SelectedFbNum;
                await Application.Current.MainPage.Navigation.PushAsync(new ZakatObjectionSuccessPageView());
                IsLoading = false;
            });

            IsWithDrawDetailsTapped = new Command(() =>
            {
                EnableWithdrawAttachments();
            });
            inputData = "";

            WithdrawAttachmentTapped = new Command(async () => await WithdrawAttachmentTappedAsync());
            WithdrawAttachmentTappedTwo = new Command(async () => await WithdrawAttachmentTappedAsyncTwo());
            WithdrAttachmentsContinueTapped = new Command(async () => await SubmitClicked());
        }
        public async Task GetZakatObjectionsData()
        {
            try
            {
                IsLoading = true;
                await OnPageLoad();
                IsLoading = false;
            }
            catch (Exception)
            {


            }
        }
        private void BackNavigations()
        {
            switch (selectedPage)
            {

                case (int)PagesEnum.BillsPage:
                    _navigationService.GoBack();
                    break;
                case (int)PagesEnum.DetailsPage:
                    EnableBillContinue();
                    break;
                case (int)PagesEnum.SecurityPayments:
                    EnableDetailsView();
                    break;
                case (int)PagesEnum.AttachmentsPage:
                    EnableSecurityPaymentsView();
                    break;
                case (int)PagesEnum.Declaration:
                    EnableAttachmentsView();
                    break;
                case (int)PagesEnum.Summary:
                    _navigationService.GoBack();
                    break;
                case (int)PagesEnum.WithdrawObjectiondetails:
                    EnableSummaryView();
                    break;


            }
        }
        private bool _IsVATBillsViewEnabled = true;
        public bool IsVATBillsViewEnabled
        {
            get { return _IsVATBillsViewEnabled; }
            set
            {
                if (_IsVATBillsViewEnabled == value) return;
                _IsVATBillsViewEnabled = value;
                OnPropertyChanged("IsVATBillsViewEnabled");
            }
        }

        private bool _IsObjectionDetailsEnabled = false;
        public bool IsObjectionDetailsEnabled
        {
            get { return _IsObjectionDetailsEnabled; }
            set
            {
                if (_IsObjectionDetailsEnabled == value) return;

                _IsObjectionDetailsEnabled = value;
                OnPropertyChanged("IsObjectionDetailsEnabled");
            }
        }

        private bool _IsDeclarationViewEnabled = false;
        public bool IsDeclarationViewEnabled
        {
            get { return _IsDeclarationViewEnabled; }
            set
            {
                if (_IsDeclarationViewEnabled == value) return;

                _IsDeclarationViewEnabled = value;
                OnPropertyChanged("IsDeclarationViewEnabled");
            }
        }

        private bool _IsAttachmentsViewEnabled = false;
        public bool IsAttachmentsViewEnabled
        {
            get { return _IsAttachmentsViewEnabled; }
            set
            {
                if (_IsAttachmentsViewEnabled == value) return;

                _IsAttachmentsViewEnabled = value;
                OnPropertyChanged("IsAttachmentsViewEnabled");
            }
        }
        private bool _SummaryVisible = false;
        public bool SummaryVisible
        {
            get { return _SummaryVisible; }
            set
            {
                if (_SummaryVisible == value) return;

                _SummaryVisible = value;
                OnPropertyChanged("SummaryVisible");
            }
        }

        private string _successMessage = AppResources.NDZakatObjectionIsSubmittedSuccessfully;
        public string SuccessMessage
        {
            get
            {
                return _successMessage;
            }
            set
            {
                if (_successMessage == value) return;

                _successMessage = value;
                OnPropertyChanged("SuccessMessage");
            }
        }

        private bool _isSecurityPaymentsVisible = false;
        public bool IsSecurityPaymentsVisible
        {
            get { return _isSecurityPaymentsVisible; }
            set
            {
                if (_isSecurityPaymentsVisible == value) return;

                _isSecurityPaymentsVisible = value;
                OnPropertyChanged("IsSecurityPaymentsVisible");
            }
        }
        private bool _isWithdrawDetailsEnabled = false;
        public bool IsWithdrawDetailsEnabled
        {
            get { return _isWithdrawDetailsEnabled; }
            set
            {
                if (_isWithdrawDetailsEnabled == value) return;

                _isWithdrawDetailsEnabled = value;
                OnPropertyChanged("IsWithdrawDetailsEnabled");
            }
        }


        private ZakatObjectionRequestSummaryModel _summaryData;

        public ZakatObjectionRequestSummaryModel SummaryData
        {
            get { return _summaryData; }
            set
            {
                if (_summaryData == value) return;

                _summaryData = value;
                OnPropertyChanged("SummaryData");
            }
        }

        private ZakatObjectionWDDropdownModel _zakatWithdrawlData;

        public ZakatObjectionWDDropdownModel ZakatWithdrawlData
        {
            get { return _zakatWithdrawlData; }
            set
            {
                if (_zakatWithdrawlData == value) return;

                _zakatWithdrawlData = value;
                OnPropertyChanged("ZakatWithdrawlData");
            }
        }




        public ObservableCollection<Attachment> _WithdrawAttachmentsListViewData { get; set; }
        public ObservableCollection<Attachment> WithdrawAttachmentsListViewData
        {
            get
            {
                return _WithdrawAttachmentsListViewData;
            }



            set
            {
                if (_WithdrawAttachmentsListViewData == value)
                {
                    return;
                }
                _WithdrawAttachmentsListViewData = value;
                OnPropertyChanged("WithdrawAttachmentsListViewData");
            }
        }

        public ObservableCollection<Attachment> _WithdrawAttachmentsListViewDataTwo { get; set; }
        public ObservableCollection<Attachment> WithdrawAttachmentsListViewDataTwo
        {
            get
            {
                return _WithdrawAttachmentsListViewDataTwo;
            }



            set
            {
                if (_WithdrawAttachmentsListViewDataTwo == value)
                {
                    return;
                }
                _WithdrawAttachmentsListViewDataTwo = value;
                OnPropertyChanged("WithdrawAttachmentsListViewDataTwo");
            }
        }

        public void EnableBillContinue()
        {
            IsVATBillsViewEnabled = true;
            IsObjectionDetailsEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsDeclarationViewEnabled = false;
            IsSecurityPaymentsVisible = false;
            SummaryVisible = false;
            IsWithdrawDetailsEnabled = false;
            IsWithdrawAttachmentsVisible = false;
            selectedPage = (int)PagesEnum.BillsPage;
        }
        public void EnableDetailsView()
        {
            IsVATBillsViewEnabled = false;
            IsObjectionDetailsEnabled = true;
            IsAttachmentsViewEnabled = false;
            IsDeclarationViewEnabled = false;
            IsSecurityPaymentsVisible = false;
            SummaryVisible = false;
            IsWithdrawDetailsEnabled = false;
            IsWithdrawAttachmentsVisible = false;
            selectedPage = (int)PagesEnum.DetailsPage;
        }
        public void EnableSecurityPaymentsView()
        {
            IsVATBillsViewEnabled = false;
            IsObjectionDetailsEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsDeclarationViewEnabled = false;
            IsSecurityPaymentsVisible = true;
            SummaryVisible = false;
            IsWithdrawDetailsEnabled = false;
            IsWithdrawAttachmentsVisible = false;
            selectedPage = (int)PagesEnum.DetailsPage;
        }
        public void EnableAttachmentsView()
        {
            IsVATBillsViewEnabled = false;
            IsObjectionDetailsEnabled = false;
            IsAttachmentsViewEnabled = true;
            IsDeclarationViewEnabled = false;
            IsSecurityPaymentsVisible = false;
            SummaryVisible = false;
            IsWithdrawDetailsEnabled = false;
            IsWithdrawAttachmentsVisible = false;
            selectedPage = (int)PagesEnum.AttachmentsPage;
        }
        public void EnableDeclarationView()
        {
            IsVATBillsViewEnabled = false;
            IsObjectionDetailsEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsDeclarationViewEnabled = true;
            IsSecurityPaymentsVisible = false;
            SummaryVisible = false;
            IsWithdrawDetailsEnabled = false;
            IsWithdrawAttachmentsVisible = false;
            selectedPage = (int)PagesEnum.Declaration;
        }
        public void EnableSummaryView()
        {
            IsVATBillsViewEnabled = false;
            IsObjectionDetailsEnabled = false;
            IsDeclarationViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsSecurityPaymentsVisible = false;
            IsWithdrawDetailsEnabled = false;
            IsWithdrawAttachmentsVisible = false;
            SummaryVisible = true;
            selectedPage = (int)PagesEnum.Summary;
        }
        public void EnableWithdrawObjectionDetails()
        {
            IsVATBillsViewEnabled = false;
            IsObjectionDetailsEnabled = false;
            IsDeclarationViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsSecurityPaymentsVisible = false;
            IsWithdrawDetailsEnabled = true;
            SummaryVisible = false;
            IsWithdrawAttachmentsVisible = false;
            selectedPage = (int)PagesEnum.WithdrawObjectiondetails;
        }

        public void EnableWithdrawAttachments()
        {
            IsVATBillsViewEnabled = false;
            IsObjectionDetailsEnabled = false;
            IsDeclarationViewEnabled = false;
            IsAttachmentsViewEnabled = false;
            IsSecurityPaymentsVisible = false;
            IsWithdrawDetailsEnabled = false;
            SummaryVisible = false;
            IsWithdrawAttachmentsVisible = true;
            selectedPage = (int)PagesEnum.WithdrawObjectiondetails;
        }


        public void EnableSadadSecurityView()
        {
            IsSadadSecuritySelected = true;
            IsBankGurantSecuritySelected = false;
        }
        public void EnablebankGuranteeSecurityView()
        {
            IsSadadSecuritySelected = false;
            IsBankGurantSecuritySelected = true;
        }

        public void PopulateAttachments(List<Attachment> attachments)
        {
            var attachmentsListViewData = new ObservableCollection<Attachment>();



            foreach (Attachment attachemnt in attachments)
            {
                attachmentsListViewData.Add(attachemnt);
            }



            if (_isFirstAttachment)
            {
                WithdrawAttachmentsListViewData = attachmentsListViewData;
            }
            else
            {
                WithdrawAttachmentsListViewDataTwo = attachmentsListViewData;
            }
        }

        private bool _isFirstAttachment = false;
        public async Task WithdrawAttachmentTappedAsync()
        {

            try
            {
                if (MopupService.Instance.PopupStack.Count > 0) return;
                _isFirstAttachment = true;
                if (WithdrawAttachmentsListViewData == null)
                {
                    WithdrawAttachmentsListViewData = new ObservableCollection<Attachment>();
                }
                await MopupService.Instance.PushAsync(new FilesUploadPopUpPageView(
                    WithdrawAttachmentsListViewData.ToList(),
                    WhichAttachment.ZakatObjectionsWithdrawAttachment, SummaryData.d.headerSet.CaseGuid));
                //TODO: ReturnID



            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
        }



        public async Task WithdrawAttachmentTappedAsyncTwo()
        {

            try
            {
                if (MopupService.Instance.PopupStack.Count > 0) return;
                _isFirstAttachment = false;
                if (WithdrawAttachmentsListViewDataTwo == null)
                {
                    WithdrawAttachmentsListViewDataTwo = new ObservableCollection<Attachment>();
                }
                await MopupService.Instance.PushAsync(new FilesUploadPopUpPageView(
                    WithdrawAttachmentsListViewDataTwo.ToList(),
                    WhichAttachment.ZakatObjectionsWithdrawAttachmentTwo, SummaryData.d.headerSet.CaseGuid));
                //TODO: ReturnID



            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
        }




        public void ResetData()
        {
            EnableBillContinue();
            inputData = "";
            WithdrawAttachmentsListViewDataTwo = null;
            WithdrawAttachmentsListViewData = null;

            RemarkNote = "";
            DetailDescriptionNote = "";
        }



        private void AddSecurityPaymentOptions()
        {
            var securityPaymentOptions = new ObservableCollection<SelectionModel>();
            securityPaymentOptions.Add(new SelectionModel
            {
                SelectionTitle = AppResources.VRSADAD,
                IsSelected = !isBankGurantee,
                IsNotSelected = isBankGurantee
            });
            securityPaymentOptions.Add(new SelectionModel
            {
                SelectionTitle = AppResources.VRBANKGURANTEE,
                IsSelected = isBankGurantee,
                IsNotSelected = !isBankGurantee
            });
            SecurityPaymentOptions = securityPaymentOptions;
        }


        public async Task SubmitClicked()
        {
            //Go to Success page

            await WithdrawSubmitClicked();
        }

        public async Task showInstructionsDialog()
        {
            await MopupService.Instance.PushAsync(new InstructionsBottomPopUpView(instructionString: AppResources.ZOTerms, checkBoxString: AppResources.ZakatInstructionsCheckBoxDesc, continueString: AppResources.ZakatObjection,
                   _dialogType: InstructionsBottomPopUpViewModel.DialogType
                       .Instructions));
        }


        public async Task OnPageLoad()
        {
            SelectedFbNum = Preferences.Get("ZakatObjectionSelectedValue", "");
            SelectedFbType = Preferences.Get("ZakatObjectionSelectedType", "");
            await ZakatRequestObjectionSummary(SelectedFbNum);
        }


        public async Task GetWithdrawFBNums()
        {
            try
            {
                IsLoading = true;
                ZakatObjectionWithDrawListModelClass _ZAKATObjectionWithDraw = new ZakatObjectionWithDrawListModelClass();
                _ZAKATObjectionWithDraw = await ZAKATWithdrawObjectionsWebServiceManager.GAZTGetZakatWithDrawList();

                if (_ZAKATObjectionWithDraw != null && _ZAKATObjectionWithDraw.results != null)
                {

                    var isRefnumberAvilable = _ZAKATObjectionWithDraw.results.Find(appRef => (appRef.ObjFbnum == SelectedFbNum));

                    if (isRefnumberAvilable != null)
                    {

                        IsSubmitEnable = false;
                        IsWithDrawEnable = true;
                    }
                    else
                    {
                        IsSubmitEnable = true;
                        IsWithDrawEnable = false;
                    }
                }

                else
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
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


        public async Task GetWithdrawReviewReason()
        {
            try
            {
                IsLoading = true;
                ZakatObjectionWDDropdownModel _ZAKATObjectionWithDraw = new ZakatObjectionWDDropdownModel();
                //Data binding for withdraw objection details
                _ZAKATObjectionWithDraw = await ZAKATWithdrawObjectionsWebServiceManager.GAZTGetZakatWithDrawDDData(SelectedFbNum);

                ZakatWithdrawlData = _ZAKATObjectionWithDraw;

                if (ZakatWithdrawlData != null && ZakatWithdrawlData.d != null)
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
                    _ZAKATObjectionWithDraw.d.results[0].APeriodTo = dt11;



                    objRefNumber = _ZAKATObjectionWithDraw.d.results[0].ObjFbnum;
                    ReferenceNumberOfAssessment = _ZAKATObjectionWithDraw.d.results[0].ARefNo;
                    AssessmentYear = _ZAKATObjectionWithDraw.d.results[0].AAssnmtYr;

                    if (App.IsArabic)
                    {

                        formatedDate1 = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];

                    }
                    else
                    {

                        formatedDate1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];

                    }

                    if (App.IsArabic)
                    {

                        formatedDate11 = dts1[0] + "-" + UtilityManager.GetMonthName(dts1[1]) + "-" + dts1[2];

                    }
                    else
                    {

                        formatedDate11 = dts1[0] + "-" + UtilityManager.GetShortMonthName(dts1[1]) + "-" + dts1[2];

                    }
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
                _ZakatObjectionRequestSummary = await ZAKATWithdrawObjectionsWebServiceManager.GAZTGetZakatRequestObjectionSummary(fbnum);

                if (_ZakatObjectionRequestSummary != null && _ZakatObjectionRequestSummary.d != null)
                {

                    SummaryData = _ZakatObjectionRequestSummary;
                    BindData(_ZakatObjectionRequestSummary);
                    await GetWithdrawFBNums();
                    VATReferanceNumber = SelectedFbNum;
                }

                else
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                }
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

        private void BindData(ZakatObjectionRequestSummaryModel zakatObjectionRequestSummary)
        {
            try
            {
                var bills = new ObservableCollection<BillsModel>();
                returnBills = new ObservableCollection<BillsModel>();

                if (zakatObjectionRequestSummary.d.ZNOB_ObjSet != null && zakatObjectionRequestSummary.d.ZNOB_ObjSet.Count > 0)
                {

                    foreach (var objction in zakatObjectionRequestSummary.d.ZNOB_ObjSet)
                    {
                        var billsModel = new BillsModel();
                        billsModel.FiscalYear = objction.AAssnmtYr;

                        if (objction.APeriodFrom != null)
                        {

                            var FormattedFromDate = string.Format(objction.APeriodFrom?.ToString("dd MMMM yyyy", new CultureInfo("en-US")));
                            var FormattedToDate = string.Format(objction.APeriodTo?.ToString("dd MMMM yyyy", new CultureInfo("en-US")));

                            string[] dtsFrom = FormattedFromDate.Split(' ');
                            string[] dtsTo = FormattedToDate.Split(' ');
                            string fromDate = /*dts[0] + " " +*/ UtilityManager.GetMonthName(dtsFrom[1]) + " " + dtsFrom[2];
                            string toDate = /*dts[0] + " " +*/ UtilityManager.GetMonthName(dtsTo[1]) + " " + dtsTo[2];
                            billsModel.FinancialPeriod = fromDate + " - " + toDate;
                        }
                        FiscalYear = objction.AAssnmtYr;
                        if (!string.IsNullOrEmpty(billsModel.FinancialPeriod))
                        {
                            if (!App.IsArabic)
                            {
                                billsModel.FinancialPeriod = objction.APeriodFrom
                                                      ?.ToString("yyyy/MM/dd", new CultureInfo("en-US"))
                                                  + " - " +
                                                  objction.APeriodTo
                                                      ?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                            }
                            else
                            {
                                billsModel.FinancialPeriod = objction.APeriodFrom
                                                      ?.ToString("yyyy/MM/dd", new CultureInfo("ar-SA"))
                                                  + " - " +
                                                  objction.APeriodTo
                                                      ?.ToString("yyyy/MM/dd", new CultureInfo("ar-SA"));
                            }
                        }

                        DispAmtCIT = objction.ADisputeAmtCit;
                        DispAmtZAKT = objction.ADisputeAmt;
                        ZAKTREVAMt = objction.ARevAmtCit;

                        billsModel.TaxType = objction.ATaxTy;
                        NewTaxType = objction.ATaxTy;
                        NewFinancialPeriod = billsModel.FinancialPeriod;
                        billsModel.ReferenceNum = objction.ARefNo;

                        billsModel.AssessmentAmountGAZT = objction.AAssnmtAmt;


                        bills.Add(billsModel);
                    }



                    ReturnBills = bills;

                    ReferenceNum = zakatObjectionRequestSummary.d.headerSet.ARefNo;


                    if (!string.IsNullOrEmpty(zakatObjectionRequestSummary.d.headerSet.AAssnmtAmt))
                    {
                        AssessmentAmountGAZT = zakatObjectionRequestSummary.d.headerSet.AAssnmtAmt;
                    }

                    if (!string.IsNullOrEmpty(zakatObjectionRequestSummary.d.headerSet.ARevAmt))
                    {

                        RevisedAmount = zakatObjectionRequestSummary.d.headerSet.ARevAmt;
                    }

                    if (!string.IsNullOrEmpty(zakatObjectionRequestSummary.d.headerSet.ADisputeAmt))
                    {

                        DisputeAmount = zakatObjectionRequestSummary.d.headerSet.ADisputeAmt;
                    }




                    ObjectionReasons = zakatObjectionRequestSummary.d.headerSet.AObjSum;
                }

                SecurityAmount = zakatObjectionRequestSummary.d.headerSet.ASecam;
                SADADNumber = zakatObjectionRequestSummary.d.headerSet.AZsopbelCit;

                var attachmentsList = new ObservableCollection<Attachment>();
                var bankGurraAttachList = new ObservableCollection<Attachment>();


                foreach (var attach in zakatObjectionRequestSummary.d.AttDetSet)
                {
                    if (attach.Dotyp.ToUpper() == "ZOBG")
                    {
                        bankGurraAttachList.Add(attach);
                    }
                    else if (attach.Dotyp.ToUpper() == "ZCOB")
                    {
                        bankGurraAttachList.Add(attach);
                    }
                    else if (attach.Dotyp.ToUpper() == "OB20")
                    {
                        attachmentsList.Add(attach);
                    }
                }

                AttachmentsListViewData = attachmentsList;
                BankGuranteeAttachmentsListViewData = bankGurraAttachList;

                RepFullName = zakatObjectionRequestSummary.d.headerSet.ARepName;
                RepPhoneNo = zakatObjectionRequestSummary.d.headerSet.ARepPhone;
                RepFaxNo = zakatObjectionRequestSummary.d.headerSet.ARepFax;
                RepElectronicMail = zakatObjectionRequestSummary.d.headerSet.ARepEmail;
                RepDesignation = zakatObjectionRequestSummary.d.headerSet.ARepDes;
                RepBuildingName = zakatObjectionRequestSummary.d.headerSet.ARepBldNm;
                RepLevelStreetNumber = zakatObjectionRequestSummary.d.headerSet.ARepSteetNo;
                RepCity = zakatObjectionRequestSummary.d.headerSet.ARepCity;
                ApplicantName = zakatObjectionRequestSummary.d.headerSet.AName;
                Capacity = zakatObjectionRequestSummary.d.headerSet.ACapacity;

                if (zakatObjectionRequestSummary.d.headerSet.ASectp == "C")
                {
                    isBankGurantee = false;
                    EnableSadadSecurityView();

                }
                else
                {
                    isBankGurantee = true;
                    EnablebankGuranteeSecurityView();
                }

                AddSecurityPaymentOptions();
            }
            catch (Exception)
            {

            }

        }

        public async Task WithdrawSubmitClicked()
        {
            try
            {

                IsLoading = true;
                ZakatObjectionWithdrawPostResponceModel _withdrawSubmitted = new ZakatObjectionWithdrawPostResponceModel();
                var result = await ZAKATWithdrawObjectionsWebServiceManager.GAZTGetZakatObjectionSummary(SelectedFbNum);


                ZakatObjectionWithdrawPostModel.Root postData = new ZakatObjectionWithdrawPostModel.Root();
                ZakatObjectionWithdrawPostModel.Metadata metaData = new ZakatObjectionWithdrawPostModel.Metadata();


                metaData.uri = result.d.__metadata.uri.Replace(SelectedFbNum, "");
                metaData.type = result.d.__metadata.type;
                metaData.id = result.d.__metadata.id.Replace(SelectedFbNum, "");

                postData.__metadata = metaData;
                postData.AComments = RemarkNote;
                postData.UserTin = result.d.UserTin;
                postData.AErrorFg = result.d.AErrorFg;
                postData.Auditorz = result.d.Auditorz;
                postData.Taxpayerz = result.d.Taxpayerz;
                postData.RegIdz = result.d.RegIdz;
                postData.PeriodKeyz = result.d.PeriodKeyz;
                postData.Monthz = "00";
                postData.Fbnum = "";
                postData.Fbnumz = "";
                postData.Langz = result.d.Langz;
                postData.PortalUsrz = result.d.PortalUsrz;
                postData.Approvez = result.d.Approvez;
                postData.OfficerUidz = result.d.OfficerUidz;
                postData.Rejectz = result.d.Rejectz;
                postData.CreateTxAssesz = result.d.CreateTxAssesz;
                postData.Xvoidz = result.d.Xvoidz;

                postData.AmdRsnz = result.d.AmdRsnz;
                postData.Mandt = result.d.Mandt;
                postData.LegacyDocNo = result.d.LegacyDocNo;
                postData.ABranch = result.d.ABranch;
                postData.ABranchCd = result.d.ABranchCd;
                postData.AAppBy = result.d.AAppBy;
                postData.AAppDt = result.d.AAppDt;
                postData.AFbnum = result.d.AFbnum;
                postData.AGpart = result.d.AGpart;
                postData.ADoc1 = result.d.ADoc1;
                postData.ADoc2 = result.d.ADoc2;
                postData.AReceiveBy = result.d.AReceiveBy;
                postData.ADoc3 = result.d.ADoc3;
                postData.ADocOther = result.d.ADocOther;
                postData.ARemark = RemarkNote;
                postData.AObjFbnum = SelectedFbNum;
                postData.ATpName = result.d.ATpName;
                postData.ACheck = result.d.ACheck;
                postData.AGpart1 = result.d.AGpart1;
                postData.FormGuid = result.d.FormGuid;
                //postData.Fbnum = result.d.Fbnum;
                postData.Status = "E0001";
                postData.AAgree = result.d.AAgree;

                postData.AStep = result.d.AStep;
                postData.AAgreeDt = result.d.AAgreeDt;
                postData.CaseGuid = result.d.CaseGuid;
                postData.Textnote = result.d.Textnote;



                var curr = DateTime.Now;
                string hours = curr.ToString("HH");
                string mins = curr.ToString("mm");
                string sec = curr.ToString("ss");

                string ATime = "PT" + hours + "H" + mins + "M" + sec + "S";

                postData.AAgreeTm = ATime;


                postData.AttDetSet = new List<object>();
                List<ZakatObjectionWithdrawPostModel.ZobjItemsSet> zobjItemsSet = new List<ZakatObjectionWithdrawPostModel.ZobjItemsSet>();




                foreach (var item in ZakatWithdrawlData.d.results)
                {
                    ZakatObjectionWithdrawPostModel.Metadata3 metaData1 = new ZakatObjectionWithdrawPostModel.Metadata3();
                    ZakatObjectionWithdrawPostModel.ZobjItemsSet obj1 = new ZakatObjectionWithdrawPostModel.ZobjItemsSet();
                    obj1.ASel = "1";
                    obj1.ACurr = "SAR";
                    obj1.ARefNo = item.ARefNo;
                    obj1.AAssnmtYr = item.AAssnmtYr;
                    obj1.ATaxTy = item.ATaxTy;
                    obj1.AAssnmtAmt = item.AAssnmtAmt;
                    obj1.ARevAmt = item.ARevAmt;
                    obj1.ADisputeAmt = item.ADisputeAmt;
                    obj1.ARetDet = item.ARetDet;


                    DateTime dt1 = Convert.ToDateTime(item.APeriodTo);
                    JsonSerializerSettings microsoftDateFormatSettings2 = new JsonSerializerSettings
                    {
                        DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                    };
                    var jsonDateTime1 = JsonConvert.SerializeObject(dt1.Date, microsoftDateFormatSettings2);
                    string[] dateList1 = jsonDateTime1.Split('+');
                    jsonDateTime1 = Regex.Replace(dateList1[0], "[@,\\.\";'\\\\]", string.Empty);
                    jsonDateTime1 = jsonDateTime1 + ")/";

                    obj1.APeriodTo = jsonDateTime1;


                    DateTime dt2 = Convert.ToDateTime(item.APeriodFrom);
                    JsonSerializerSettings microsoftDateFormatSettings1 = new JsonSerializerSettings
                    {
                        DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                    };
                    var jsonDateTime2 = JsonConvert.SerializeObject(dt2.Date, microsoftDateFormatSettings1);
                    string[] dateList2 = jsonDateTime2.Split('+');
                    jsonDateTime2 = Regex.Replace(dateList2[0], "[@,\\.\";'\\\\]", string.Empty);
                    jsonDateTime2 = jsonDateTime2 + ")/";


                    obj1.APeriodFrom = jsonDateTime2;


                    zobjItemsSet.Add(obj1);

                }

                postData.zobj_itemsSet = zobjItemsSet;

                ZakatObjectionWithdrawPostModel.Metadata2 metaData2 = new ZakatObjectionWithdrawPostModel.Metadata2();
                ZakatObjectionWithdrawPostModel.ZnotesSet obj = new ZakatObjectionWithdrawPostModel.ZnotesSet();
                metaData2.uri = ZATCAConstants.ZakatObjectionsNotesSet;
                metaData2.type = "Z_TP_NOTES_TP09_SRV.znotes";
                metaData2.id = ZATCAConstants.ZakatObjectionsNotesSet;
                obj.__metadata = metaData2;
                obj.Notenoz = "001";
                obj.AttByz = "TP";
                obj.ElemNo = 0;
                obj.Noteno = "001";
                obj.Refnamez = "";
                obj.XInvoicez = "";
                obj.XObsoletez = "";
                obj.Rcodez = "TP09_NOTE";
                obj.Erfusrz = "";
                obj.Lineno = 1;
                obj.Tdformat = "";

                if (DetailDescriptionNote != "")
                {
                    obj.Tdline = DetailDescriptionNote;
                }
                else
                {
                    obj.Tdline = "";
                }

                List<ZakatObjectionWithdrawPostModel.ZnotesSet> _znotesSet = new List<ZakatObjectionWithdrawPostModel.ZnotesSet>();

                if (DetailDescriptionNote != "")
                {
                    _znotesSet.Add(obj);

                }
                postData.znotesSet = _znotesSet;

                postData.Submitz = "X";
                postData.Savez = "X";


                DateTime dt = Convert.ToDateTime(result.d.AReceiveDt);
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                var jsonDateTime = JsonConvert.SerializeObject(dt.Date, microsoftDateFormatSettings);
                string[] dateList = jsonDateTime.Split('+');
                jsonDateTime = Regex.Replace(dateList[0], "[@,\\.\";'\\\\]", string.Empty);

                postData.AReceiveDt = jsonDateTime;




                _withdrawSubmitted = await ZAKATWithdrawObjectionsWebServiceManager.GAZTSaveZakatObjectionWithDrawData(postData);

                if (_withdrawSubmitted != null && _withdrawSubmitted.d != null)
                {



                    VATReferanceNumber = _withdrawSubmitted.d.Fbnumz;
                    SuccessMessage = AppResources.NDZakatWithdrawSubmittedSuccessfully;
                    await Application.Current.MainPage.Navigation.PushAsync(new ZakatObjectionSuccessPageView());


                }
                else
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
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


        //API-2
        public async Task ZAKATObjectionCreateNew()
        {
            try
            {
                IsLoading = true;
                ZAKATObjectionCreateNewModel _ZAKATObjectionCreateNew = new ZAKATObjectionCreateNewModel();
                try
                {
                    _ZAKATObjectionCreateNew = await ZAKATObjectionsWebServiceManager.GAZTGetZAKATObjectionCreateNew();

                    if (_ZAKATObjectionCreateNew != null && _ZAKATObjectionCreateNew.d != null)
                    {

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

        //API-5

        //API To search by referance number
        public async Task ZAKATObjectionDetailsByReferenceNumber()
        {
            try
            {
                IsLoading = true;
                ZAKATObjectionDetailsByReferenceNumberModel _ZAKATObjectionDetailsByReferenceNumber = new ZAKATObjectionDetailsByReferenceNumberModel();
                try
                {
                    _ZAKATObjectionDetailsByReferenceNumber = await ZAKATObjectionsWebServiceManager.GAZTGetZAKATObjectionDetailsByReferenceNumber("");

                    if (_ZAKATObjectionDetailsByReferenceNumber == null)
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

        //API-6
        public async Task ZAKATObjectionDetailsToAmendReturn()
        {
            try
            {
                IsLoading = true;
                ZAKATObjectionDetailsToAmendReturnModel _ZAKATObjectionDetailsToAmendReturn = new ZAKATObjectionDetailsToAmendReturnModel();
                try
                {
                    _ZAKATObjectionDetailsToAmendReturn = await ZAKATObjectionsWebServiceManager.GAZTGetZAKATObjectionDetailsToAmendReturn();

                    if (_ZAKATObjectionDetailsToAmendReturn == null)
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

        //API-7
        public async Task ZAKATObjectionAmendReturnAndClose()
        {
            try
            {
                IsLoading = true;
                ZAKATObjectionAmendReturnAndCloseModel _ZAKATObjectionAmendReturnAndClose = new ZAKATObjectionAmendReturnAndCloseModel();
                try
                {
                    _ZAKATObjectionAmendReturnAndClose = await ZAKATObjectionsWebServiceManager.GAZTGetZAKATObjectionAmendReturnAndClose();

                    if (_ZAKATObjectionAmendReturnAndClose == null)
                    {
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
            catch (Exception ex)
            {
                IsLoading = false;
                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                _navigationService.GoBack();
            }
        }

        //API-8
        public async Task ZAKATObjectionOnPaymentMethodSelection()
        {
            try
            {
                IsLoading = true;
                ZAKATObjectionOnPaymentMethodSelectionModel _ZAKATObjectionOnPaymentMethodSelection = new ZAKATObjectionOnPaymentMethodSelectionModel();
                try
                {
                    _ZAKATObjectionOnPaymentMethodSelection = await ZAKATObjectionsWebServiceManager.GAZTGetZAKATObjectionOnPaymentMethodSelection();

                    if (_ZAKATObjectionOnPaymentMethodSelection == null)
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
            catch (Exception ex)
            {
                IsLoading = false;
                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                _navigationService.GoBack();
            }
        }

        //API-9
        public async Task ZAKATObjectionApplicationDetailsIfStatusIP017()
        {
            try
            {

                IsLoading = true;
                ZAKATObjectionApplicationDetailsIfStatusIP017Model _ZAKATObjectionApplicationDetailsIfStatusIP017 = new ZAKATObjectionApplicationDetailsIfStatusIP017Model();
                try
                {
                    _ZAKATObjectionApplicationDetailsIfStatusIP017 = await ZAKATObjectionsWebServiceManager.GAZTGetZAKATObjectionApplicationDetailsIfStatusIP017();

                    if (_ZAKATObjectionApplicationDetailsIfStatusIP017 == null)
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

        //API-10
        public async Task ZAKATObjectionGenerateSADADNumber()
        {
            try
            {
                IsLoading = true;
                ZAKATObjectionGenerateSADADNumberModel _ZAKATObjectionGenerateSADADNumber = new ZAKATObjectionGenerateSADADNumberModel();
                try
                {
                    _ZAKATObjectionGenerateSADADNumber = await ZAKATObjectionsWebServiceManager.GAZTGetZAKATObjectionGenerateSADADNumber();

                    if (_ZAKATObjectionGenerateSADADNumber == null)
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

        //API-12
        public async Task ZAKATObjectionBusyIndicator()
        {
            try
            {
                IsLoading = true;
                ZAKATObjectionBusyIndicatorModel _ZAKATObjectionBusyIndicator = new ZAKATObjectionBusyIndicatorModel();
                try
                {
                    _ZAKATObjectionBusyIndicator = await ZAKATObjectionsWebServiceManager.GAZTGetZAKATObjectionBusyIndicator();

                    if (_ZAKATObjectionBusyIndicator == null)
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

        public async Task BankList()
        {
            try
            {

                IsLoading = true;
                ZakatBankListModel _ZakatBankList = new ZakatBankListModel();
                try
                {
                    _ZakatBankList = await ZAKATObjectionsWebServiceManager.GAZTGetBankList();

                    if (_ZakatBankList == null)
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

        public async Task IntialLoadData(string fbguid)
        {
            try
            {
                IsLoading = true;
                ZakatBankListModel _IntialLoadData = new ZakatBankListModel();
                try
                {
                    _IntialLoadData = await ZAKATObjectionsWebServiceManager.GAZTGetIntialLoadData(fbguid);

                    if (_IntialLoadData == null)
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

        public async Task ZakatRemoveObjection(string retFbnum, string objFbnum)
        {
            try
            {
                IsLoading = true;
                ZakatBankListModel _ZakatRemoveObjection = new ZakatBankListModel();
                try
                {
                    _ZakatRemoveObjection = await ZAKATObjectionsWebServiceManager.GAZTGetZakatRemoveObjection(retFbnum, objFbnum);

                    if (_ZakatRemoveObjection == null)
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

        public async Task ZakatRemoveObjectionACK(string retFbnum, string objFbnum)
        {
            try
            {

                IsLoading = true;
                ZakatBankListModel _ZakatRemoveObjectionACK = new ZakatBankListModel();
                try
                {
                    _ZakatRemoveObjectionACK = await ZAKATObjectionsWebServiceManager.GAZTGetZakatRemoveObjectionACK(retFbnum, objFbnum);

                    if (_ZakatRemoveObjectionACK == null)
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


        #region Zakat Withdraw Objection
        public async Task ZakatWithDrawMainData()
        {
            try
            {
                IsLoading = true;
                ZakatWithdrawMainDataModel _ZakatWithdrawMainData = new ZakatWithdrawMainDataModel();
                try
                {
                    _ZakatWithdrawMainData = await ZAKATWithdrawObjectionsWebServiceManager.GAZTGetZakatWithDrawMainData();

                    if (_ZakatWithdrawMainData == null)
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

        public async Task ZakatWithDrawList()
        {
            try
            {
                IsLoading = true;
                ZakatObjectionWithDrawListModelClass _ZakatObjectionWithDrawList = new ZakatObjectionWithDrawListModelClass();
                try
                {
                    _ZakatObjectionWithDrawList = await ZAKATWithdrawObjectionsWebServiceManager.GAZTGetZakatWithDrawList();

                    if (_ZakatObjectionWithDrawList == null)
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

        public async Task ZakatWithDrawDDData(string objFbnum)
        {
            try
            {
                IsLoading = true;
                ZakatObjectionWDDropdownModel _ZakatObjectionWDDropdown = new ZakatObjectionWDDropdownModel();
                try
                {
                    _ZakatObjectionWDDropdown = await ZAKATWithdrawObjectionsWebServiceManager.GAZTGetZakatWithDrawDDData(objFbnum);

                    if (_ZakatObjectionWDDropdown == null)
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

        public async Task SaveZakatObjectionWDAttachment(byte[] AttachmentByte, string fileName, string RetGuid, string Dotyp, string contentType)
        {
            try
            {

                IsLoading = true;
                AttachmentRootOject _Attachment = new AttachmentRootOject();
                try
                {
                    _Attachment = await ZAKATWithdrawObjectionsWebServiceManager.GAZTSaveZakatObjectionWDAttachment(AttachmentByte, fileName, RetGuid, Dotyp, contentType);

                    if (_Attachment == null)
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

        public async Task ObjectonWDDownloadacknowledgement(string fbnum)
        {
            try
            {
                IsLoading = true;
                string strACK = null;
                try
                {
                    strACK = ZAKATWithdrawObjectionsWebServiceManager.GAZTZakatObjectonWDDownloadacknowledgement(fbnum);

                    if (string.IsNullOrEmpty(strACK))
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

        public async Task ZakatObjectionWDDownloadForm(string fbnum)
        {
            try
            {
                IsLoading = true;
                string strACK = null;
                try
                {
                    strACK = ZAKATWithdrawObjectionsWebServiceManager.GAZTZakatObjectionWDDownloadForm(fbnum);

                    if (string.IsNullOrEmpty(strACK))
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