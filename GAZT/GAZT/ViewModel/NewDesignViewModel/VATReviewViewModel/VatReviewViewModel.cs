using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.Models.VatReviewModel;
using EGAZT.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel;
using EGAZT.Views.NewDesign;
using EGAZT.Views.NewDesign.GenericPickers;
using EGAZT.Views.NewDesign.VatReview;
using EGAZT.Views.NewDesign.ZakatInstalmentPlan;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.VatReviewViewModel
{
    public class VatReviewViewModel : BaseViewModel
    {
        #region Enums

        enum PagesEnum
        {
            ReviewReason,
            ReviewDetails,
            SecurityPayments,
            Declaration,
            Summary
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

        public ICommand ReviewReasonConBtnTapped { get; set; }
        public ICommand ReviewDetailsConBtnTapped { get; set; }
        public ICommand SecurityPaymentConBtnTapped { get; set; }
        public ICommand DeclarationConBtnTapped { get; set; }
        public ICommand SummaryConBtnTapped { get; set; }
        public ICommand CloseClick { get; set; }
        public ICommand GoBackClick { get; set; }
        public ICommand GoBackToReviewReason { get; set; }
        public ICommand GoBackToReviewDetails { get; set; }
        public ICommand GoBackToDeclaration { get; set; }
        public ICommand ViewApplicationTapped { get; set; }
        public ICommand NewAttachmentTapped { get; set; }
        public ICommand NewBankGuranteeAttachmentTapped { get; set; }
        public ICommand ReviewReasonCommand { get; set; }
        public ICommand SubReviewReasonCommand { get; set; }
        public ICommand ShowDatePicker { get; set; }
        public ICommand IdTypeSpinnerTapped { get; set; }
        public ICommand ApplicationNumRefCommand { get; set; }
        public ICommand SadadGenerateBtnTapped { get; set; }

        #endregion

        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        int selectedPage = (int) PagesEnum.ReviewReason;

        private bool _isBackVisible = false;

        public bool IsBackVisible
        {
            get { return _isBackVisible; }
            set
            {
                _isBackVisible = value;
                RaisePropertyChanged("IsBackVisible");
            }
        }

        private bool _isLoading = false;

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }

        private bool _isReviewReasonVisible = false;

        public bool ReviewReasonVisible
        {
            get { return _isReviewReasonVisible; }
            set
            {
                _isReviewReasonVisible = value;
                RaisePropertyChanged("ReviewReasonVisible");
            }
        }

        private bool _reviewDetailsVisible = false;

        public bool ReviewDetailsVisible
        {
            get { return _reviewDetailsVisible; }
            set
            {
                _reviewDetailsVisible = value;
                RaisePropertyChanged("ReviewDetailsVisible");
            }
        }

        private bool _securityPaymentVisible = false;

        public bool SecurityPaymentVisible
        {
            get { return _securityPaymentVisible; }
            set
            {
                _securityPaymentVisible = value;
                RaisePropertyChanged("SecurityPaymentVisible");
            }
        }

        private bool _declarationVisible = false;

        public bool DeclarationVisible
        {
            get { return _declarationVisible; }
            set
            {
                _declarationVisible = value;
                RaisePropertyChanged("DeclarationVisible");
            }
        }

        private bool _summaryVisible = false;

        public bool SummaryVisible
        {
            get { return _summaryVisible; }
            set
            {
                _summaryVisible = value;
                RaisePropertyChanged("SummaryVisible");
            }
        }

        private bool _isSadadSecuritySelected = false;

        public bool IsSadadSecuritySelected
        {
            get { return _isSadadSecuritySelected; }
            set
            {
                _isSadadSecuritySelected = value;
                RaisePropertyChanged("IsSadadSecuritySelected");
            }
        }

        private bool _isBankGurantSecuritySelected = false;

        public bool IsBankGurantSecuritySelected
        {
            get { return _isBankGurantSecuritySelected; }
            set
            {
                _isBankGurantSecuritySelected = value;
                RaisePropertyChanged("IsBankGurantSecuritySelected");
            }
        }

        private string _pickedDate = "";

        public string PickedDate
        {
            get { return _pickedDate; }
            set
            {
                _pickedDate = value;
                RaisePropertyChanged("PickedDate");
            }
        }

        private GenericDatePickerModel genericDatePickerModel;

        private string _contactPersonName = "";

        public string ContactPersonName
        {
            get { return _contactPersonName; }
            set
            {
                _contactPersonName = value;
                RaisePropertyChanged("ContactPersonName");
            }
        }

        public string _reviewReason = "";

        public string ReviewReason
        {
            get { return _reviewReason; }
            set
            {
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
                _applicationRefNumber = value;
                RaisePropertyChanged("ApplicationRefNumber");
            }
        }
        
        public DateTime _requestDate = DateTime.Now;

        public DateTime RequestDate
        {
            get { return _requestDate; }
            set
            {
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
                _taxPeriodOfCase = value;
                RaisePropertyChanged("TaxPeriodOfCase");
            }
        }
        
        public DateTime _taxPeriodFrom = DateTime.Now;

        public DateTime TaxPeriodFrom
        {
            get { return _taxPeriodFrom; }
            set
            {
                _taxPeriodFrom = value;
                RaisePropertyChanged("TaxPeriodFrom");
            }
        }
        
        public DateTime _taxPeriodTo = DateTime.Now;

        public DateTime TaxPeriodTo
        {
            get { return _taxPeriodTo; }
            set
            {
                _taxPeriodTo = value;
                RaisePropertyChanged("TaxPeriodTo");
            }
        }
        
        public string _penalityAmountInQuestion = "";

        public string PenalityAmountInQuestion
        {
            get { return _penalityAmountInQuestion; }
            set
            {
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
                _securityAmount = value;
                RaisePropertyChanged("SecurityAmount");
            }
        }

        public string _idNumber = "";

        public string IDNumber
        {
            get { return _idNumber; }
            set
            {
                _idNumber = value;
                RaisePropertyChanged("IDNumber");
            }
        }

        private bool isIDVerified = false;

        public bool IsIDVerified
        {
            get { return isIDVerified; }
            set
            {
                isIDVerified = value;
                RaisePropertyChanged("IsIDVerified");
            }
        }

        private bool _contractPersonEditable = false;

        public bool ContractPersonEditable
        {
            get { return _contractPersonEditable; }
            set
            {
                _contractPersonEditable = value;
                RaisePropertyChanged("ContractPersonEditable");
            }
        }

        private string _idType = "";

        public string IDType
        {
            get { return _idType; }
            set
            {
                _idType = value;
                RaisePropertyChanged("IDType");
            }
        }

        private bool _isDOBVisible = false;

        public bool IsDOBVisible
        {
            get { return _isDOBVisible; }
            set
            {
                _isDOBVisible = value;
                RaisePropertyChanged("IsDOBVisible");
            }
        }

        private GenericPickerModel _reviewReasonPickerModel { get; set; }

        public GenericPickerModel ReviewReasonPickerModel
        {
            get { return _reviewReasonPickerModel; }
            set
            {
                _reviewReasonPickerModel = value;
                RaisePropertyChanged("ReviewReasonPickerModel");
            }
        }

        private GenericPickerModel _reviewSubReasonPickerModel { get; set; }

        public GenericPickerModel ReviewSubReasonPickerModel
        {
            get { return _reviewSubReasonPickerModel; }
            set
            {
                _reviewSubReasonPickerModel = value;
                RaisePropertyChanged("ReviewSubReasonPickerModel");
            }
        }

        private GenericPickerModel _applicationRefPickerModel { get; set; }

        public GenericPickerModel ApplicationRefPickerModel
        {
            get { return _applicationRefPickerModel; }
            set
            {
                _applicationRefPickerModel = value;
                RaisePropertyChanged("ApplicationRefPickerModel");
            }
        }

        private GenericPickerModel _idTypePickerModel { get; set; }

        public GenericPickerModel IDTypePickerModel
        {
            get { return _idTypePickerModel; }
            set
            {
                _idTypePickerModel = value;
                RaisePropertyChanged("IDTypePickerModel");
            }
        }

        private bool _isDeclarationEnabled = false;

        public bool IsDeclarationEnabled
        {
            get { return _isDeclarationEnabled; }
            set
            {
                _isDeclarationEnabled = value;
                DeclarationButtonBackGroundColor = Color.FromHex(_isDeclarationEnabled ? "#d49504" : "#9EA4A9");
                RaisePropertyChanged("IsDeclarationEnabled");
            }
        }

        private Color _declarationButtonBackGroundColor = Color.FromHex("#9EA4A9");

        public Color DeclarationButtonBackGroundColor
        {
            get { return _declarationButtonBackGroundColor; }
            set
            {
                if (_declarationButtonBackGroundColor == value)
                {
                    return;
                }

                _declarationButtonBackGroundColor = value;
                RaisePropertyChanged("DeclarationButtonBackGroundColor");
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

        public class SelectionModel
        {
            public SelectionModel()
            {
            }

            public string SelectionTitle { get; set; }
            public bool IsSelected { get; set; }
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
                RaisePropertyChanged("SecurityPaymentOptions");
            }
        }

        private Dictionary<string, string> IDTypeDictionary = new Dictionary<string, string>
        {
            {AppResources.VFCNationalID, "ZS0001"},
            {AppResources.VFCIqamaID, "ZS0002"},
            {AppResources.VFCGCCID, "ZS0003"},
        };

        private bool _isDialog = true;
        private bool _isBankGuranteeAttachments = false;
        private VATObjectionFormModel.ReviewReason selectedReviewReason;
        private VATObjectionFormModel.SubReason selectedSubReviewReason;
        private VATObjectionRejectedFormModel.AppRefNumResult selectedApplicationRef;
        private List<VATObjectionFormModel.ReviewReason> reviewReasonList;
        private List<VATObjectionFormModel.SubReason> subReviewReasonList;
        private List<VATObjectionRejectedFormModel.AppRefNumResult> appRefNumList;
        private VATObjectionSummaryModel modelVATReview;
        private VATObjectionRejectedFormModel _VATObjectionRejected;

        public VatReviewViewModel(INavigationService navigationService, IDialogService dialogService) : base(
            navigationService, dialogService)
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

            CloseClick = new Command(async () => { _navigationService.GoBack(); });

            GoBackClick = new Command(async () => { BackNavigations(); });

            ReviewReasonConBtnTapped = new Command(async () => { ReviewReasonConBtnClicked(); });

            ReviewDetailsConBtnTapped = new Command(async () => { ReviewDetailsConBtnClicked(); });
            SecurityPaymentConBtnTapped = new Command(async () => { SecurityPaymentConBtnClicked(); });

            DeclarationConBtnTapped = new Command(async () => { DeclarationConBtnClicked(); });

            SummaryConBtnTapped = new Command(async () => { SummaryConBtnClicked(); });

            GoBackToReviewReason = new Command(async () => { EnableReviewReasonView(); });
            GoBackToReviewDetails = new Command(async () => { EnableReviewDetailsView(); });
            GoBackToDeclaration = new Command(async () => { EnableDeclarationView(); });
            ViewApplicationTapped = new Command(async () => { ViewApplicationClicked(); });
            NewAttachmentTapped = new Command(async () => { NewAttachmentClicked(); });
            NewBankGuranteeAttachmentTapped = new Command(NewBankGuranteeAttachmentClicked);
            ReviewReasonCommand = new Command(async () => { showReviewReasonPickerDialog(); });
            SubReviewReasonCommand = new Command(async () => { showSubReviewReasonPickerDialog(); });
            ApplicationNumRefCommand = new Command(async () => { showAppRefNumberPickerDialog(); });
            SadadGenerateBtnTapped = new Command(async () => { GenerateSadadNumber(); });
            ShowDatePicker = new Command(async () => { showDatePickerDialog(); });
            IdTypeSpinnerTapped = new Command(async () => { showIdTypePickerDialog(); });

            AddSecurityPaymentOptions();

            genericDatePickerModel = new GenericDatePickerModel();
            genericDatePickerModel.DatePickerTitle = AppResources.VRDateOfBirth;
            genericDatePickerModel.PickerId = "DatePicker";

            setIdPickerModel();
        }

        public async void NewAttachmentClicked()
        {
            if (AttachmentsListViewData == null)
            {
                AttachmentsListViewData = new ObservableCollection<Attachment>();
            }

            try
            {
                if (AttachmentsListViewData.Count == 0)
                {
                    await PopupNavigation.Instance.PushAsync(new FilesUploadPopUpPageView(
                        AttachmentsListViewData.ToList(),
                        Models.ZakatInstalationModels.WhichAttachment.VatReviewAttachments,
                        ""));
                }
            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async void NewBankGuranteeAttachmentClicked()
        {
            if (BankGuranteeAttachmentsListViewData == null)
            {
                BankGuranteeAttachmentsListViewData = new ObservableCollection<Attachment>();
            }

            try
            {
                if (BankGuranteeAttachmentsListViewData.Count == 0)
                {
                    await PopupNavigation.Instance.PushAsync(new FilesUploadPopUpPageView(
                        BankGuranteeAttachmentsListViewData.ToList(),
                        Models.ZakatInstalationModels.WhichAttachment.VatReviewBankGuranteeAttach,
                        ""));
                }
            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void setIdPickerModel()
        {
            ObservableCollection<string> iDTypes = new ObservableCollection<string>();
            iDTypes.Add(AppResources.VFCNationalID);
            iDTypes.Add(AppResources.VFCIqamaID);
            iDTypes.Add(AppResources.VFCGCCID);


            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = iDTypes;
            genericPickerModel.PickerTitle = AppResources.VRIDType;
            genericPickerModel.PickerId = PickerEnum.IDType.ToString();

            IDTypePickerModel = genericPickerModel;
        }

        private void setReviewReasonPickerModel()
        {
            ObservableCollection<string> reasonTypes = new ObservableCollection<string>();
            foreach (var reason in reviewReasonList)
            {
                reasonTypes.Add(reason.Reasons);
            }

            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = reasonTypes;
            genericPickerModel.PickerTitle = AppResources.VRReviewReason;
            genericPickerModel.PickerId = PickerEnum.ReviewReason.ToString();

            ReviewReasonPickerModel = genericPickerModel;
        }

        private void setReviewSubReasonPickerModel(string selectedReason)
        {
            ObservableCollection<string> subReasonTypes = new ObservableCollection<string>();
            selectedReviewReason = reviewReasonList.First(reviewReason => reviewReason.Reasons==selectedReason);
            subReviewReasonList = selectedReviewReason.ListSubReason;
            foreach (var subReason in subReviewReasonList)
            {
                subReasonTypes.Add(subReason.SubReasons);
            }
            
            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = subReasonTypes;
            genericPickerModel.PickerTitle = AppResources.VRReviewSubReason;
            genericPickerModel.PickerId = PickerEnum.ReviewSubReason.ToString();

            ReviewSubReasonPickerModel = genericPickerModel;
        }

        private void setApplicationRefPickerModel(List<VATObjectionRejectedFormModel.AppRefNumResult> results)
        {
            appRefNumList = results;
            ObservableCollection<string> appRefNums = new ObservableCollection<string>();
            
            foreach (var refNum in results)
            {
                appRefNums.Add(refNum.Opbel);
            }
            
            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = appRefNums;
            genericPickerModel.PickerTitle = AppResources.VRApplicationReferenceNumber;
            genericPickerModel.PickerId = PickerEnum.ApplicationReferenceNumber.ToString();

            ApplicationRefPickerModel = genericPickerModel;
        }

        private async void showDatePickerDialog()
        {

            try
            {
                await PopupNavigation.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel));
            }
            catch (GAZTUnlockAccountException ex)
            {
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        private async void showIdTypePickerDialog()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new PickerPageView(IDTypePickerModel));
            }
            catch (GAZTUnlockAccountException ex)
            {
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }
        
        private async void showReviewReasonPickerDialog()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new PickerPageView(ReviewReasonPickerModel));
            }
            catch (GAZTUnlockAccountException ex)
            {
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }
        
        private async void showSubReviewReasonPickerDialog()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new PickerPageView(ReviewSubReasonPickerModel));
            }
            catch (GAZTUnlockAccountException ex)
            {
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }
        
        private async void showAppRefNumberPickerDialog()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new PickerPageView(ApplicationRefPickerModel));
            }
            catch (GAZTUnlockAccountException ex)
            {
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }
        
        private void ReviewReasonConBtnClicked()
        {
            try
            {
                EnableReviewDetailsView();
            }
            catch (GAZTUnlockAccountException ex)
            {
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        private void ReviewDetailsConBtnClicked()
        {
            try
            {
                VATObjectionSecurityAmount(Convert.ToDecimal(selectedApplicationRef.Penamount),Convert.ToDecimal(selectedApplicationRef.Liaamt),Convert.ToDecimal(selectedApplicationRef.Clramt));
                EnableSecurityPaymentsView();
            }
            catch (GAZTUnlockAccountException ex)
            {
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        private void SecurityPaymentConBtnClicked()
        {
            try
            {
                EnableDeclarationView();
            }
            catch (GAZTUnlockAccountException ex)
            {
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        private void DeclarationConBtnClicked()
        {
            try
            {
                EnableSummaryView();
            }
            catch (GAZTUnlockAccountException ex)
            {
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        private async void SummaryConBtnClicked()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new VatReviewSuccessPageView());

            }
            catch (GAZTUnlockAccountException ex)
            {
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        private void EnableReviewReasonView()
        {
            IsBackVisible = false;
            ReviewReasonVisible = true;
            ReviewDetailsVisible = false;
            SecurityPaymentVisible = false;
            DeclarationVisible = false;
            SummaryVisible = false;
            selectedPage = (int) PagesEnum.ReviewReason;
        }

        private void EnableReviewDetailsView()
        {
            IsBackVisible = true;
            ReviewReasonVisible = false;
            ReviewDetailsVisible = true;
            SecurityPaymentVisible = false;
            DeclarationVisible = false;
            SummaryVisible = false;
            selectedPage = (int) PagesEnum.ReviewDetails;
        }

        private void EnableSecurityPaymentsView()
        {
            IsBackVisible = true;
            ReviewReasonVisible = false;
            ReviewDetailsVisible = false;
            SecurityPaymentVisible = true;
            DeclarationVisible = false;
            SummaryVisible = false;
            selectedPage = (int) PagesEnum.SecurityPayments;
        }

        private void EnableDeclarationView()
        {
            IsBackVisible = true;
            ReviewReasonVisible = false;
            ReviewDetailsVisible = false;
            SecurityPaymentVisible = false;
            DeclarationVisible = true;
            SummaryVisible = false;
            selectedPage = (int) PagesEnum.Declaration;
        }

        private void EnableSummaryView()
        {
            IsBackVisible = true;
            ReviewReasonVisible = false;
            ReviewDetailsVisible = false;
            SecurityPaymentVisible = false;
            DeclarationVisible = false;
            SummaryVisible = true;
            selectedPage = (int) PagesEnum.Summary;
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
        
        public void GenerateSadadNumber()
        {
            //string fbnum, string Disamt, string Liaamt, string Abrzu, string Abrzo, string Secamt, string Security, string Persl
            VATObjectionGenrateorRefreshSADAD(selectedApplicationRef.Fbnum,modelVATReview.d.SecurityDtl.Disamt,selectedApplicationRef.Liaamt,selectedApplicationRef.Abrzu.ToString("yyyy-MM-dd'T'HH:mm:ss"),selectedApplicationRef.Abrzo.ToString("yyyy-MM-dd'T'HH:mm:ss"),SecurityAmount,modelVATReview.d.SecurityDtl.Security,selectedApplicationRef.Persl);
        }

        private async void ViewApplicationClicked()
        {
            FetchViewBill(selectedApplicationRef.Opbel,"");
            if (_isDialog)
            {
                _navigationService.NavigateTo(App.VatReviewViewApplicationPageView);
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new VatReviewBillViewBottomPopUpPageView());
            }

            _isDialog = !_isDialog;
        }

        public void PopulateAttachments(List<Attachment> attachments)
        {
            var attachmentsListViewData = new ObservableCollection<Attachment>();

            foreach (Attachment attachemnt in attachments)
            {
                attachmentsListViewData.Add(attachemnt);
            }

            if (_isBankGuranteeAttachments)
            {

            }
            else
            {
                AttachmentsListViewData = attachmentsListViewData;
            }

        }

        private void BackNavigations()
        {
            switch (selectedPage)
            {
                case (int) PagesEnum.ReviewDetails:
                    EnableReviewReasonView();
                    break;
                case (int) PagesEnum.SecurityPayments:
                    EnableReviewDetailsView();
                    break;
                case (int) PagesEnum.Declaration:
                    EnableSecurityPaymentsView();
                    break;
                case (int) PagesEnum.Summary:
                    EnableDeclarationView();
                    break;

            }
        }

        public void updatePickerData(GenericPickerModel genericPickerModel)
        {
            if (genericPickerModel.PickerId == PickerEnum.IDType.ToString())
            {
                IDTypePickerModel = genericPickerModel;
                updateIdTypePicker();
            }
            else if (genericPickerModel.PickerId == PickerEnum.ReviewReason.ToString())
            {
                ReviewReasonPickerModel = genericPickerModel;
                ReviewReason = ReviewReasonPickerModel.SelectedValue;
                setReviewSubReasonPickerModel(ReviewReasonPickerModel.SelectedValue);
            }
            else if (genericPickerModel.PickerId == PickerEnum.ReviewSubReason.ToString())
            {
                ReviewSubReasonPickerModel = genericPickerModel;
                SubReviewReason = ReviewSubReasonPickerModel.SelectedValue;
                fetchApplicationRefNums(ReviewSubReasonPickerModel.SelectedValue);
            }
            else if (genericPickerModel.PickerId == PickerEnum.ApplicationReferenceNumber.ToString())
            {
                ApplicationRefPickerModel = genericPickerModel;
                ApplicationRefNumber = ApplicationRefPickerModel.SelectedValue;
                setDataBasedOnAppRefNum(ApplicationRefPickerModel.SelectedValue);
            }
        }
        
         
        public void fetchApplicationRefNums(string SubReasonValue)
        {
            selectedSubReviewReason = subReviewReasonList.First(subReviewReason => subReviewReason.SubReasons == SubReasonValue);
            
            VATObjectionFormRejected(modelVATReview.d.Fbustx, selectedReviewReason.ProcCD, selectedSubReviewReason.Code,
                modelVATReview.d.UserTypx);
            //VATObjectionEnableSubmit(modelVATReview.d.Statusx,selectedReviewReason.ProcCD,selectedSubReviewReason.Code,modelVATReview.d.RejFb);
        }
        
        public void setDataBasedOnAppRefNum(string appRefNum)
        {
            selectedApplicationRef  = appRefNumList.First(appRef => appRef.Fbnum == appRefNum);

            RequestDate = selectedApplicationRef.DecDt;
            TaxPeriodOfCase = selectedApplicationRef.Perslt;
            TaxPeriodFrom = selectedApplicationRef.Abrzu;
            TaxPeriodTo = selectedApplicationRef.Abrzo;
            PenalityAmountInQuestion = selectedApplicationRef.Penamount;

        }

        public void updateIdTypePicker()
        {
            IDType = IDTypePickerModel.SelectedValue;
            ContactPersonName = "";
            IDNumber = "";
            if (IDType == AppResources.VFCGCCID)
            {
                IsDOBVisible = false;
                ContractPersonEditable = true;
            }
            else
            {
                IsDOBVisible = true;
                ContractPersonEditable = false;
            }

            ValidateIdNumber();
        }

        public void ValidateIdNumber()
        {
            try
            {
                IsIDVerified = false;
                EnableDeclaration();
                PopUp popUp = new PopUp();
                StringBuilder Messages = new StringBuilder();
                if (!string.IsNullOrEmpty(IDNumber))
                {
                    if (IDType.Equals(AppResources.VFCNationalID))
                    {
                        if (IDNumber.Substring(0, 1) != "1")
                        {
                            popUp.Message = AppResources.ZZNationalIDstartswith1;
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }

                            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                            IDNumber = string.Empty;
                            //ZZPleaseenteravalidNationalID
                        }
                        else
                        {
                            if (IDNumber.Length != 10)
                            {
                                if (Messages.Length > 0)
                                {
                                    Messages.Append(Environment.NewLine);
                                }

                                Messages.Append(AppResources.ZZNationalIDlengthis10digit);
                            }

                            if (Messages.Length > 0)
                            {
                                popUp.Message = Messages.ToString();
                                popUp.IsLinkAvailable = false;
                                if (App.IsArabic)
                                {
                                    popUp.FlowDirections = "RightToLeft";
                                    popUp.isFontSet = true;
                                }
                                else
                                {
                                    popUp.FlowDirections = "LeftToRight";
                                }

                                PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                                IDNumber = string.Empty;
                            }
                            else
                            {

                                if (!string.IsNullOrEmpty(PickedDate))
                                {
                                    ValidateIdNumberFromApi("ZS0001");
                                }


                            }
                        }


                    }

                    if (IDType.Equals(AppResources.VFCIqamaID))
                    {
                        if (IDNumber.Substring(0, 1) != "2")
                        {
                            popUp.Message = AppResources.ZZIqamaIDstartswith2;
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }

                            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                            IDNumber = string.Empty;
                        }
                        else
                        {
                            if (IDNumber.Length != 10)
                            {
                                if (Messages.Length > 0)
                                {
                                    Messages.Append(Environment.NewLine);
                                }

                                Messages.Append(AppResources.ZZIqamaIDlengthis10digit);
                            }

                            if (Messages.Length > 0)
                            {
                                popUp.Message = Messages.ToString();
                                popUp.IsLinkAvailable = false;
                                if (App.IsArabic)
                                {
                                    popUp.FlowDirections = "RightToLeft";
                                    popUp.isFontSet = true;
                                }
                                else
                                {
                                    popUp.FlowDirections = "LeftToRight";
                                }

                                PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                                IDNumber = string.Empty;
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(PickedDate))
                                {
                                    ValidateIdNumberFromApi("ZS0002");
                                }
                            }
                        }


                    }

                    if (IDType.Equals(AppResources.VFCGCCID))
                    {

                        if (IDNumber.Substring(0, 1) == "0")
                        {
                            //Have to change to neww error message
                            popUp.Message = AppResources.ZZGCCIDdonotstartwith0;
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }

                            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                            IDNumber = string.Empty;
                        }
                        else if (!(IDNumber.Length <= 15 && IDNumber.Length >= 7))
                        {
                            popUp.Message = AppResources.ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit;
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }

                            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                            IDNumber = string.Empty;
                            // EntryIDNumber.Text = string.Empty;//ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit
                        }
                        else
                        {
                            IsIDVerified = true;
                            EnableDeclaration();
                        }


                    }
                }
                else
                {
                }


            }
            catch (Exception ex)
            {


            }
        }

        public void updateReviewReasonPicker()
        {

        }

        public void updateSubReviewReasonPicker()
        {

        }

        public void updateAppRefPicker()
        {

        }

        public void ResetData()
        {
            EnableReviewReasonView();

            ShowInstructionsDialog();
        }

        public void EnableDeclaration()
        {
            if (ContactPersonName == "" || !IsIDVerified)
            {
                IsDeclarationEnabled = false;
            }
            else
            {
                IsDeclarationEnabled = true;
            }
        }

        public async void ShowInstructionsDialog()
        {
            await PopupNavigation.Instance.PushAsync(new InstructionsBottomPopUpView(
                instructionString: AppResources.VRInstructions, checkBoxString: AppResources.VRCheckBoxDesc,
                continueString: AppResources.CRContinue,
                _dialogType: InstructionsBottomPopUpViewModel.DialogType
                    .Instructions));
        }

        private void AddSecurityPaymentOptions()
        {

            var securityPaymentOptions = new ObservableCollection<SelectionModel>();
            securityPaymentOptions.Add(new SelectionModel
            {
                SelectionTitle = AppResources.VRSADAD,
                IsSelected = false
            });
            securityPaymentOptions.Add(new SelectionModel
            {
                SelectionTitle = AppResources.VRBANKGURANTEE,
                IsSelected = false
            });
            SecurityPaymentOptions = securityPaymentOptions;
        }

        #region ApiRegion

        public async Task ValidateIdNumberFromApi(string idType)
        {
            try
            {
                await Task.Run(() => { IsLoading = true; });
                await Task.Run(async () =>
                {
                    IsLoading = true;
                    try
                    {
                        var resultData = await WebServiceManager.GAZTVATChangeFillingPeriodValidateIDnumber(
                            App.LoginDataRetrieved.TIN, idType, IDNumber, "", "", PickedDate.Replace("/", ""));
                        if (resultData != null && resultData.d != null)
                        {
                            IsIDVerified = true;
                            ContactPersonName = resultData.d.Name2 + " " + resultData.d.Name1;
                            EnableDeclaration();
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                IsLoading = false;
                                await _dialogService.ShowMessage(resultData.errorMessage, AppResources.Information);
                            });
                        }

                        IsLoading = false;
                    }
                    catch (GAZTVATChangeFillingPeriodException ex)
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
            catch (GAZTVATChangeFillingPeriodException ex)
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
                await Task.Run(() => { IsLoading = false; });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public async Task VatReviewReasonDropDownData()
        {
            try
            {
                await Task.Run(() => { IsLoading = true; });
                await Task.Run(async () =>
                {
                    IsLoading = true;
                    //VATReview = null;
                    //VATObjectionSummaryModel modelVATReview = new VATObjectionSummaryModel();
                    VATObjectionFormModel.VATReviewsReturnModel modelVATReviewsReturn =
                        new VATObjectionFormModel.VATReviewsReturnModel();
                    try
                    {
                         modelVATReview = await WebServiceManager.GAZTGetVATObjectionSummary();

                        if (modelVATReview != null && modelVATReview.d != null)
                        {
                            modelVATReviewsReturn.TIN = modelVATReview.d.Gpartx;
                            modelVATReviewsReturn.TaxPayerName = modelVATReview.d.FullName;
                            modelVATReviewsReturn.License = modelVATReview.d.CrNo + "-" + modelVATReview.d.Actnm;
                            modelVATReviewsReturn.Address = modelVATReview.d.AddressSet.results[0].BuildingNo + "," +
                                                            modelVATReview.d.AddressSet.results[0].Street + "," +
                                                            modelVATReview.d.AddressSet.results[0].Addrnumber + "," +
                                                            modelVATReview.d.AddressSet.results[0].RegionDesc + "," +
                                                            modelVATReview.d.AddressSet.results[0].City + "," +
                                                            modelVATReview.d.AddressSet.results[0].PostalCd;
                            //modelVATReviewsReturn.ListReviewReason lstReasons =new modelVATReviewsReturn.ListReviewReason;
                            // List<Dictionary<string, string>> reasonDDL = new List<Dictionary<string, string>>();
                            List<VATObjectionFormModel.ReviewReason> reasonList =
                                new List<VATObjectionFormModel.ReviewReason>();
                            if (modelVATReview.d.MainReasonSet.results.Count > 0)
                            {

                                for (int i = 0; i < modelVATReview.d.MainReasonSet.results.Count; i++)
                                {

                                    VATObjectionFormModel.ReviewReason obj = new VATObjectionFormModel.ReviewReason();
                                    obj.ProcCD = modelVATReview.d.MainReasonSet.results[i].ProcCd;
                                    obj.Reasons = modelVATReview.d.MainReasonSet.results[i].TypeT;
                                    if (modelVATReview.d.ReasonSet.results.Where(x =>
                                        x.ProcCd == modelVATReview.d.MainReasonSet.results[i].ProcCd).Count() > 0)
                                    {
                                        List<VATObjectionFormModel.SubReason> subReasonList =
                                            new List<VATObjectionFormModel.SubReason>();
                                        var lstSub = modelVATReview.d.ReasonSet.results.Where(x =>
                                            x.ProcCd == modelVATReview.d.MainReasonSet.results[i].ProcCd).ToList();
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

                            reviewReasonList = reasonList;
                            modelVATReviewsReturn.ListReviewReason = reasonList;

                            modelVATReviewsReturn.DecisionDate = (modelVATReview.d.DecDt != null)
                                ? modelVATReview.d.DecDt.ToString()
                                : "";
                            //modelVATReviewsReturnModel.DecisionTaken = modelVATReview.d.NotesSet.results;
                            //modelVATReviewsReturnModel.AttachmentName = modelVATReview.d.AttdetSet.results[0];
                            modelVATReviewsReturn.TaxPeriodofCase = modelVATReview.d.SecurityDtl.Perslt;
                            modelVATReviewsReturn.PeriodFrom = (modelVATReview.d.SecurityDtl.Abrzu != null)
                                ? modelVATReview.d.SecurityDtl.Abrzu.ToString()
                                : "";
                            modelVATReviewsReturn.PeriodTo = (modelVATReview.d.SecurityDtl.Abrzo != null)
                                ? modelVATReview.d.SecurityDtl.Abrzo.ToString()
                                : "";
                            modelVATReviewsReturn.TotalTaxLiability = modelVATReview.d.SecurityDtl.Liaamt;
                            modelVATReviewsReturn.TaxPaid = modelVATReview.d.SecurityDtl.Clramt;
                            modelVATReviewsReturn.RequestToReviewAmount = modelVATReview.d.SecurityDtl.Amttp;
                            modelVATReviewsReturn.ParticularAmount = modelVATReview.d.SecurityDtl.Disamt;
                            modelVATReviewsReturn.Corrections = modelVATReview.d.NotesSet.results;
                            modelVATReviewsReturn.SecurityAmount = modelVATReview.d.SecurityDtl.Secamt;
                            modelVATReviewsReturn.SADADNumber = modelVATReview.d.SecurityDtl.Sopbel;
                            modelVATReviewsReturn.MethodSubmitSecurity = modelVATReview.d.SecurityDtl.Sectp;
                            modelVATReviewsReturn.ChkSecurityPayment = modelVATReview.d.SecurityDtl.ChkCash;
                            modelVATReviewsReturn.ChkBankGuarantee = modelVATReview.d.SecurityDtl.ChkBank;
                            modelVATReviewsReturn.ChkInfoCorrect = modelVATReview.d.DecFlg1;
                            modelVATReviewsReturn.NameOfTaxPayer = modelVATReview.d.FullName;
                            modelVATReviewsReturn.ApplicationNo = modelVATReview.d.Fbnumx;
                            modelVATReviewsReturn.Date = (modelVATReview.d.Declarationdt != null)
                                ? modelVATReview.d.Declarationdt.ToString()
                                : "";

                            setReviewReasonPickerModel();
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
                await Task.Run(() => { IsLoading = false; });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }
        
        public async void VATObjectionEnableSubmit(string statusx, string rvRsn, string rvSubRsn, string rejFb)
        {

            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                statusx = null;
                rvRsn = null;
                rvSubRsn = null;
                rejFb = null;
                VATObjectionEnableSubmitModel _VATObjectionEnableSubmit = new VATObjectionEnableSubmitModel();
                _VATObjectionEnableSubmit = await WebServiceManager.GAZTGetVATObjectionEnableSubmit(statusx, rvRsn, rvSubRsn, rejFb);

                if (_VATObjectionEnableSubmit != null && _VATObjectionEnableSubmit.d != null)
                {
                    //setApplicationRefPickerModel(_VATObjectionEnableSubmit);
                }

                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (GAZTErrorException ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            }
            catch (InternetException ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
        }
        
        public void VATObjectionDownloadAck(string fbnum)
        {

            try
            {
                fbnum = null;
                string strACK = null;
                strACK = WebServiceManager.GetVATObjectionDownloadAck(fbnum);

                if (strACK != null)
                {

                }
            }
            catch (GAZTErrorException ex)
            {
                _dialogService.ShowMessage(ex.Message, AppResources.Information);
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        public async void VATObjectionFormRejected(string fbustx, string RvRsn ,string rvSubRsn,string UserTypx)
        {
    
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
    
                //VATObjectionRejectedFormModel _VATObjectionRejected = new VATObjectionRejectedFormModel();
                _VATObjectionRejected = await WebServiceManager.GAZTGetVATObjectionFormRejected(fbustx, RvRsn, rvSubRsn,UserTypx);
    
                if (_VATObjectionRejected != null && _VATObjectionRejected.d != null)
                {
                    setApplicationRefPickerModel(_VATObjectionRejected.d.RejectedFormSet.results);
                }
    
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (GAZTErrorException ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
    
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            }
            catch (InternetException ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
    
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
        }
        
        public async void VATObjectionGenrateorRefreshSADAD(string fbnum, string Disamt, string Liaamt, string Abrzu, string Abrzo, string Secamt, string Security, string Persl)
        {

            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                
                VATObjectionGenrateSadadModel _VATObjectionGenrateSadad = new VATObjectionGenrateSadadModel();
                _VATObjectionGenrateSadad = await WebServiceManager.GAZTGetVATObjectionGenrateorRefreshSADAD(fbnum, Disamt, Liaamt, Abrzu, Abrzo, Secamt, Security, Persl);

                if (_VATObjectionGenrateSadad != null)
                {
                    SADADNumber = _VATObjectionGenrateSadad.ToString();
                }

                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (GAZTErrorException ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            }
            catch (InternetException ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
        }
        
        public async void VATObjectionSecurityAmount(Decimal disamt, Decimal liaamt, Decimal clramt)
        {

            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                
                VATObjectionSecurityAmountModel _VATObjectionSecurityAmount = new VATObjectionSecurityAmountModel();
                _VATObjectionSecurityAmount = await WebServiceManager.GAZTGetVATObjectionSecurityAmount(disamt, liaamt, clramt);

                if (_VATObjectionSecurityAmount != null && _VATObjectionSecurityAmount.d != null)
                {
                    SecurityAmount = _VATObjectionSecurityAmount.d.Secamt;
                }

                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (GAZTErrorException ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            }
            catch (InternetException ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
        }
      
        public async void VATObjectionValidateTaxPayer(string idnum, string idtype, string passExpDt, string taxpDob)
        {

            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                idnum = null;
                idtype = null;
                passExpDt = null;
                taxpDob = null;
                VATObjectionValidateTaxpayerModel _VATObjectionValidateTaxpayer = new VATObjectionValidateTaxpayerModel();
                _VATObjectionValidateTaxpayer = await WebServiceManager.GAZTGetVATObjectionValidateTaxPayer(idnum, idtype, passExpDt, taxpDob);

                if (_VATObjectionValidateTaxpayer != null && _VATObjectionValidateTaxpayer.d != null)
                {

                }

                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (GAZTErrorException ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            }
            catch (InternetException ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
        }
        
        public async Task FetchViewBill(string opbel, string vtre2)
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
                    
                    VATObjectionViewbillModel billModelResult = new VATObjectionViewbillModel();
                    VATObjectionFormViewBillModel billFormModel = new VATObjectionFormViewBillModel();
                    try
                    {
                        billModelResult = await WebServiceManager.GAZTGetVATObjectionViewBill(opbel,vtre2);

                        if (billModelResult != null && billModelResult.d != null)
                        {
                            foreach (var item in billModelResult.d.results)
                            {
                                billFormModel.DocumentNumber = item.Opbel;
                                billFormModel.SadadNumber = item.Vtre2;
                                billFormModel.DateofPenality = ConvertJsonToDateTime(item.Bldat.ToString()).ToString(); //item.Bldat.ToString();
                                billFormModel.DescriptionOfPenality = item.Desc;
                                billFormModel.Periodkey = item.Perslt;
                                billFormModel.StartDate = ConvertJsonToDateTime(item.Abrzu.ToString()).ToString(); //item.Abrzu.ToString();
                                billFormModel.EndDate = ConvertJsonToDateTime(item.Abrzo.ToString()).ToString(); //item.Abrzo.ToString();
                                billFormModel.DueDate = ConvertJsonToDateTime(item.Studt.ToString()).ToString(); //item.Studt.ToString();
                                billFormModel.Amount = item.Betrh;
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
        public static DateTime ConvertJsonToDateTime(string jsonDate)
        {
            // JavaScript uses the unix epoch of 1/1/1970. Note, it's important to call ToLocalTime()
            // after doing the time conversion, otherwise we'd have to deal with daylight savings hooey.
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            Double milliseconds = Convert.ToDouble(jsonDate);
            DateTime dateTime = unixEpoch.AddMilliseconds(milliseconds).ToLocalTime();

            return dateTime;
        }
        
        #endregion
    }
}