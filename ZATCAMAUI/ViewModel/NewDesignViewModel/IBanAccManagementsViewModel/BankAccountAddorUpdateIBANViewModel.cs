using Mopups.Services;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows.Input;
using ZATCAMAUI.Core.Exceptions;
using Result= ZATCAMAUI.Models.IBanManagementListModel.Result;
using ZATCAMAUI.Models;
using static ZATCAMAUI.Models.IBanManagementListModel;
using ZATCAMAUI.Manager;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Views.NewDesign.Common;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using Newtonsoft.Json.Linq;
using Syncfusion.Maui.Core.Carousel;
using ZATCAMAUI.Views.NewDesign.IBanAccountsManagementPages;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.IBanAccManagementsViewModel
{
    public class BankAccountAddorUpdateIBANViewModel : BaseViewModel
    {
        public bool isUpdate = false;
        public ICommand GoBackBtnTapped { get; set; }
        public ICommand ContinueButtonTapped { get; set; }
        public ICommand ShowIDTypePicker { get; set; }
        public ICommand ShowIDNumberPicker { get; set; }
        public ICommand ShowBankNamePicker { get; set; }
        public ICommand GoBackToNewForm { get; set; }
        public ICommand SummaryConBtnTapped { get; set; }
        public ICommand OnAttachmentClickOne { get; set; }
        public ICommand OnAttachmentClickTwo { get; set; }
        public ICommand IBANNumberUnfocused { get; set; }

        int SelectedAttachmentNumber = 0;


        #region Enums

        enum PagesEnum
        {
            IBANNewForm,
            IBANSummary,

        }

        #endregion

        public Result BankIDResult = null;
        public string UserBankid = "";

        int selectedPage = (int)PagesEnum.IBANNewForm;

        private int _currenrIndex = 1;

        public int CurrentIndex
        {
            get => _currenrIndex;
            set
            {
                if (_currenrIndex == value) return;

                _currenrIndex = value;
                OnPropertyChanged(nameof(CurrentIndex));
                if (_currenrIndex == MaxIndex)
                {
                    MarkComplete = true;
                    OnPropertyChanged(nameof(MarkComplete));
                }
                else
                {
                    MarkComplete = false;
                    OnPropertyChanged(nameof(MarkComplete));
                }
            }
        }

        public bool MarkComplete { get; private set; } = false;
        public int MaxIndex { get; private set; } = 2;

        private string _selectedIDType = "";

        public string SelectedIDType
        {
            get { return _selectedIDType; }
            set
            {
                if (_selectedIDType == value) return;

                _selectedIDType = value;
                OnPropertyChanged("SelectedIDType");
            }
        }

        private string _selectedIDNumber = "";

        public string SelectedIDNumber
        {
            get { return _selectedIDNumber; }
            set
            {
                if (_selectedIDNumber == value) return;

                _selectedIDNumber = value;
                OnPropertyChanged("SelectedIDNumber");
            }
        }

        public List<IdNumberListSetResult> IdNumberListSet = new List<IdNumberListSetResult>();

        public string selectedOtherBankName = "";
        public bool isIBanValid = false;
        private string _selectedBankName = "";

        public string SelectedBankName
        {
            get { return _selectedBankName; }
            set
            {
                if (_selectedBankName == value) return;

                _selectedBankName = value;
                OnPropertyChanged("SelectedBankName");
            }
        }

        private string _selectedBankNameField = "";
        public string SelectedBankNameField
        {
            get { return _selectedBankNameField; }
            set
            {
                if (_selectedBankNameField == value) return;

                _selectedBankNameField = value;
                OnPropertyChanged("SelectedBankNameField");
            }
        }

        private string _selectedIDTypeValue = "";

        public string SelectedIDTypeValue
        {
            get { return _selectedIDTypeValue; }
            set
            {
                if (_selectedIDTypeValue == value) return;

                _selectedIDTypeValue = value;
                OnPropertyChanged("SelectedIDTypeValue");
            }
        }

        private string _selectedIDNumberValue = "";

        public string SelectedIDNumberValue
        {
            get { return _selectedIDNumberValue; }
            set
            {
                if (_selectedIDNumberValue == value) return;

                _selectedIDNumberValue = value;
                OnPropertyChanged("SelectedIDNumberValue");
            }
        }

        private string _selectedBankNameValue = "";

        public string SelectedBankNameValue
        {
            get { return _selectedBankNameValue; }
            set
            {
                if (_selectedBankNameValue == value) return;

                _selectedBankNameValue = value;
                OnPropertyChanged("SelectedBankNameValue");
            }
        }

        private string _accountOwnerName;

        public string AccountOwnerName
        {
            get { return _accountOwnerName; }
            set
            {
                if (_accountOwnerName == value) return;

                _accountOwnerName = value;
                OnPropertyChanged("AccountOwnerName");
            }
        }

        private bool _isNameEnabled = true;

        public bool isNameEnabled
        {
            get { return _isNameEnabled; }
            set
            {
                if (_isNameEnabled == value) return;

                _isNameEnabled = value;
                OnPropertyChanged("isNameEnabled");
            }
        }

        private string _IBANValue = "";

        public string IBANValue
        {
            get { return _IBANValue; }
            set
            {
                if (_IBANValue == value) return;

                _IBANValue = value;
                OnPropertyChanged("IBANValue");
            }
        }

        private string _idNumber = "";

        public string IdNumber
        {
            get { return _idNumber; }
            set
            {
                if (_idNumber == value) return;

                _idNumber = value;
                OnPropertyChanged("IdNumber");
            }
        }
        private string _idNumberInfo = "";

        public string IdNumberInfo
        {
            get { return _idNumberInfo; }
            set
            {
                if (_idNumberInfo == value) return;

                _idNumberInfo = value;
                OnPropertyChanged("IdNumberInfo");
            }
        }
        private IBanAccountManagementResponseModel _iBANAccountData;

        public IBanAccountManagementResponseModel IBANAccountData
        {
            get { return _iBANAccountData; }
            set
            {
                if (_iBANAccountData == value) return;

                _iBANAccountData = value;
                OnPropertyChanged("IBANAccountData");
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


        private bool _newFormVisible = false;

        public bool NewFormVisible
        {
            get { return _newFormVisible; }
            set
            {
                if (_newFormVisible == value) return;

                _newFormVisible = value;
                OnPropertyChanged("NewFormVisible");
            }
        }
        private bool _attachmentVisible = false;

        public bool AttachmentVisible
        {
            get { return _attachmentVisible; }
            set
            {
                if (_attachmentVisible == value) return;

                _attachmentVisible = value;
                OnPropertyChanged("AttachmentVisible");
            }
        }
        private bool isIBanDropDownEnabled = true;

        public bool IsIBanDropDownEnabled
        {
            get { return isIBanDropDownEnabled; }
            set
            {
                if (isIBanDropDownEnabled == value) return;

                isIBanDropDownEnabled = value;
                OnPropertyChanged("IsIBanDropDownEnabled");
            }
        }

        private bool _isIBanUpdatePage = false;

        public bool IsIBanUpdatePage
        {
            get { return _isIBanUpdatePage; }
            set
            {
                if (_isIBanUpdatePage == value) return;

                _isIBanUpdatePage = value;
                OnPropertyChanged("IsIBanUpdatePage");
            }
        }
        private bool _isIdInfoVisibility = false;

        public bool IsIdInfoVisibility
        {
            get { return _isIdInfoVisibility; }
            set
            {
                if (_isIdInfoVisibility == value) return;

                _isIdInfoVisibility = value;
                OnPropertyChanged("IsIdInfoVisibility");
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

        private bool _isInstrunctionChecked = false;
        public bool IsInstrunctionChecked
        {
            get
            {
                return _isInstrunctionChecked;
            }
            set
            {
                //MessagingCenter.Send<VATRegistrationPageViewModel, bool>(this, "IsInstrunctionChecked", value);

                if (_isInstrunctionChecked == value) return;

                _isInstrunctionChecked = value;

                if (_isInstrunctionChecked)
                {
                    IsContinueButtonEnable = true;
                }
                else
                {
                    IsContinueButtonEnable = false;
                }


                OnPropertyChanged("IsInstrunctionChecked");
            }
        }

        private bool _isContinueButtonEnable = false;
        public bool IsContinueButtonEnable
        {
            get
            {
                return _isContinueButtonEnable;
            }
            set
            {
                if (_isContinueButtonEnable == value) return;

                _isContinueButtonEnable = value;
                /*if (_isContinueButtonEnable)
                {
                    ContinueButtonnBackroundColor = Color.FromHex("#d49504");
                }
                else
                {
                    ContinueButtonnBackroundColor = Color.FromHex("#9EA4A9");
                }*/
                OnPropertyChanged("IsContinueButtonEnable");
            }
        }

        private Color _continueButtonnBackroundColor = Color.FromHex("#d49504");
        public Color ContinueButtonnBackroundColor
        {
            get
            {
                return _continueButtonnBackroundColor;
            }
            set
            {
                if (_continueButtonnBackroundColor == value) return;

                _continueButtonnBackroundColor = value;
                OnPropertyChanged("ContinueButtonnBackroundColor");
            }
        }

        private bool _otherBanksVisible = false;

        public bool OtherBanksVisible
        {
            get { return _otherBanksVisible; }
            set
            {
                if (_otherBanksVisible == value) return;

                _otherBanksVisible = value;
                OnPropertyChanged("OtherBanksVisible");
            }
        }
        public ObservableCollection<Attachment> _iBANBankListViewDataOne = new ObservableCollection<Attachment>();

        public ObservableCollection<Attachment> IBANBankListViewDataOne
        {
            get { return _iBANBankListViewDataOne; }

            set
            {
                if (_iBANBankListViewDataOne == value)
                {
                    return;
                }

                _iBANBankListViewDataOne = value;
                OnPropertyChanged("IBANBankListViewDataOne");
            }
        }
        public ObservableCollection<Attachment> _iBANBankListViewDataTwo = new ObservableCollection<Attachment>();

        public ObservableCollection<Attachment> IBANBankListViewDataTwo
        {
            get { return _iBANBankListViewDataTwo; }

            set
            {
                if (_iBANBankListViewDataTwo == value)
                {
                    return;
                }

                _iBANBankListViewDataTwo = value;
                OnPropertyChanged("IBANBankListViewDataTwo");
            }
        }
        private IbanAccountFormGuidResponse _iBANAccountDataFormGuid;

        public IbanAccountFormGuidResponse IBANAccountDataFormGuid
        {
            get { return _iBANAccountDataFormGuid; }
            set
            {
                if (_iBANAccountDataFormGuid == value) return;

                _iBANAccountDataFormGuid = value;
                OnPropertyChanged("IBANAccountDataFormGuid");
            }
        }
        private bool _isDropdownVisibile = false;

        public bool IsDropdownVisibile
        {
            get { return _isDropdownVisibile; }
            set
            {
                if (_isDropdownVisibile == value) return;

                _isDropdownVisibile = value;
                OnPropertyChanged("IsDropdownVisibile");
            }
        }
        private string _idNumberTitle = AppResources.IBANIdNumber;

        public string IdNumberTitle
        {
            get { return _idNumberTitle; }
            set
            {
                if (_idNumberTitle == value) return;

                _idNumberTitle = value;
                OnPropertyChanged("IdNumberTitle");
            }
        }

        private Color _isBorderColorRed = Colors.LightGray;

        public Color IsBorderColorRed
        {
            get { return _isBorderColorRed; }
            set
            {
                if (_isBorderColorRed == value) return;

                _isBorderColorRed = value;
                OnPropertyChanged("IsBorderColorRed");
            }
        }
        public BankAccountAddorUpdateIBANViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {



            GoBackBtnTapped = new Command(() => { BackNavigations(); });

            ContinueButtonTapped = new Command(this.ContinueButtonClicked);

            ShowIDTypePicker = new Command(() => { showPickerDialog(1); });
            ShowIDNumberPicker = new Command(() => { showPickerDialog(2); });
            ShowBankNamePicker = new Command(() => { showPickerDialog(3); });
            GoBackToNewForm = new Command(() => { SummaryEditClicked(); });
            SummaryConBtnTapped = new Command(async () => { await SummaryConButtonClicked(); });
            OnAttachmentClickOne = new Command(async () =>
            {
                await AddAttachmentTestOne();
            });
            OnAttachmentClickTwo = new Command(async () =>
            {
                await AddAttachmentTestTwo();
            });
            NewFormVisible = true;

            IBANNumberUnfocused = new Command(async ()=>await CheckIBanIsValidOrNot());

        }

        public async Task CheckIBanIsValidOrNot()
        {
            if ((IBANValue.Length > 0) && (IBANValue.Length < 24))
            {

              await  MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDIBANValidationforLenght));

            }
            if ((IBANValue.Length > 0) && (IBANValue.Length == 24))
            {
                App.IBanValidatedResponse = string.Empty;
                IsLoading = true;
                try
                {
                    try
                    {
                        App.IBanValidatedResponse = string.Empty;
                        var response = await WebServiceManager.GAZTCheckIBAN(IBANValue);
                        if (response != null)
                        {
                            //IBan is Valid
                            isIBanValid = true;
                            IsLoading = false;
                            string bankName = string.Empty;
                            bankName = JObject.Parse(App.IBanValidatedResponse)["result"].ToString();
                            string IBanSelectedBankName = JObject.Parse(bankName)["bankDetails"].ToString();

                            IsIBanDropDownEnabled = false;

                            if (string.IsNullOrEmpty(IBanSelectedBankName))
                            {
                                SelectedBankName = AppResources.IBanSelectedOtherBankName;
                                SelectedBankNameField = AppResources.IBanSelectedOtherBankName;
                                OtherBanksVisible = true;
                            }
                            else
                            {
                                SelectedBankName = JObject.Parse(bankName)["bankDetails"].ToString();
                                SelectedBankNameField = JObject.Parse(bankName)["bankDetails"].ToString();
                                OtherBanksVisible = false;
                            }


                        }
                        else
                        {
                            IsIBanDropDownEnabled = true;
                            IsLoading = false;
                            SelectedIDNumber = "";
                            isIBanValid = false;
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZIBANisincorrect));
                        }
                    }
                    catch (InternetException ex)
                    {
                        IsLoading = false;
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                    }
                }

                catch (Exception )
                {
                    //IBan is InValid
                    isIBanValid = false;
                    IsLoading = false;
                  await  MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZIBANisincorrect));
                }


            }
        }
        public async Task IBANBankAccountFormGUID()
        {
            try
            {
                IsLoading = true;

                IBANAccountDataFormGuid = await IBanManagmentWebserviceManager.GAZTGetIBanAccountsFormGUID();

                PopToRootPage();// If seesion Expired it will navigate to Dashboard page

                IsLoading = false;
            }
            catch (Exception)
            {

            }

        }

        public async Task AddAttachmentTestOne()
        {
            if (MopupService.Instance.PopupStack.Count > 0) return;
            if (IBANBankListViewDataOne == null)
            {
                IBANBankListViewDataOne = new ObservableCollection<Attachment>();
            }

            try
            {
                SelectedAttachmentNumber = (int)WhichAttachment.IBANBankAccountOne;
                await MopupService.Instance.PushAsync(new FilesUploadPopUpPageView(
                     IBANBankListViewDataOne.ToList(),
                     WhichAttachment.IBANBankAccountOne, IBANAccountDataFormGuid.d.FormGuid
                    ));
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
        public async Task AddAttachmentTestTwo()
        {
            if (MopupService.Instance.PopupStack.Count > 0) return;
            if (IBANBankListViewDataTwo == null)
            {
                IBANBankListViewDataTwo = new ObservableCollection<Attachment>();
            }

            try
            {
                SelectedAttachmentNumber = (int)WhichAttachment.IBANBankAccountTwo;
                await MopupService.Instance.PushAsync(new FilesUploadPopUpPageView(
                     IBANBankListViewDataTwo.ToList(),
                    WhichAttachment.IBANBankAccountTwo, IBANAccountDataFormGuid.d.FormGuid
                    ));
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

        public void PopulateAttachments(List<Attachment> attachments)
        {
            var attachmentsListViewData = new ObservableCollection<Attachment>();

            foreach (Attachment item in attachments)
            {
                attachmentsListViewData.Add(item);
            }
            if (attachments.Count > 0)
            {

                if (attachmentsListViewData[0].Dotyp.Equals("ZIB1"))
                {
                    IBANBankListViewDataOne = attachmentsListViewData;
                    OnPropertyChanged("IBANBankListViewDataOne");
                }
                else if (attachmentsListViewData[0].Dotyp.Equals("ZIB2"))
                {
                    IBANBankListViewDataTwo = attachmentsListViewData;
                }
            }
            else
            {
                switch (SelectedAttachmentNumber)
                {

                    case (int)WhichAttachment.IBANBankAccountOne:
                        IBANBankListViewDataOne.Clear();
                        break;
                    case (int)WhichAttachment.IBANBankAccountTwo:
                        IBANBankListViewDataTwo.Clear();
                        break;
                }
            }
        }

        private void SummaryEditClicked()
        {
            EnableNewFormView();
        }

        private void BackNavigations()
        {
            switch (selectedPage)
            {
                case (int)PagesEnum.IBANNewForm:
                    _navigationService.GoBack();
                    break;
                case (int)PagesEnum.IBANSummary:
                    EnableNewFormView();
                    break;
            }
        }

        private void EnableNewFormView()
        {
            CurrentIndex = 1;
            NewFormVisible = true;
            SummaryVisible = false;
            selectedPage = (int)PagesEnum.IBANNewForm;
        }

        private void EnableSummaryView()
        {
            CurrentIndex = 2;
            NewFormVisible = false;
            SummaryVisible = true;
            selectedPage = (int)PagesEnum.IBANSummary;

        }

        private void setIDTypePickerModel()
        {

            if (PickerModel != null)
            {

                PickerModel = null;
            }


            var list = new List<string>();

            foreach (IdTypeListSetResult dropdown in IBANAccountData.d.IdTypeListSet)
            {
                try
                {
                    list.Add(dropdown.IdDesc);
                }
                catch (Exception ex)
                {

                }


            }


            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = list;
            genericPickerModel.PickerTitle = "";
            genericPickerModel.PickerId = "IBANIdTypePicker";
            PickerModel = genericPickerModel;
            SelectedIDNumber = "";
        }

        private void setIDNumberPickerModel()
        {

            if (PickerModel != null)
            {

                PickerModel = null;
            }
            var list = new List<string>();
            var selectedType = IBANAccountData.d.IdTypeListSet.Find(selectedValue => (selectedValue.IdDesc == SelectedIDType));

            if (selectedType != null)
            {
                SelectedIDTypeValue = selectedType.IdType;
            }

            for (int i = 0; i < IBANAccountData.d.IdNumberListSet.Count; i++)
            {
                if (IBANAccountData.d.IdNumberListSet[i].IdType.Equals(SelectedIDTypeValue))
                {
                    try
                    {
                        list.Add(IBANAccountData.d.IdNumberListSet[i].IdNumber);
                    }
                    catch (Exception )
                    {

                    }
                }
            }

            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = list;
            genericPickerModel.PickerTitle = "";
            genericPickerModel.PickerId = "IBANIdNumberPicker";
            PickerModel = genericPickerModel;
        }

        private void setBankNamePickerModel()
        {
            if (PickerModel != null)
            {

                PickerModel = null;
            }

            var list = new List<string>();

            foreach (Result dropdown in IBANAccountData.d.BankListSet)
            {
                try
                {
                    list.Add(dropdown.Bkext);
                }
                catch (Exception ex)
                {

                }

            }
            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = list;
            genericPickerModel.PickerTitle = "";
            genericPickerModel.PickerId = "IBANBankNamePicker";
            PickerModel = genericPickerModel;
        }

        private async Task showPickerDialog(int pickerID)
        {
            try
            {

                if (pickerID == 1)
                {

                    setIDTypePickerModel();
                    await MopupService.Instance.PushAsync(new PickerPageView(PickerModel));
                }
                else if (pickerID == 2)
                {
                    if (SelectedIDType == "")
                        return;
                    else
                    {
                        setIDNumberPickerModel();
                        await MopupService.Instance.PushAsync(new PickerPageView(PickerModel));
                    }

                }
                else if (pickerID == 3)
                {

                    setBankNamePickerModel();
                    await MopupService.Instance.PushAsync(new PickerPageView(PickerModel));
                }


            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
        }

        private async Task SummaryConButtonClicked()
        {


            if (IsInstrunctionChecked == true)
            {
                try
                {
                    IBANPostRequest requestObj = new IBANPostRequest();

                    if (string.IsNullOrEmpty(App.SelectedIBAN))
                    {

                        requestObj.Action = "N";
                        requestObj.Fbnum = "";
                    }
                    else
                    {
                        requestObj.Action = "U";
                        requestObj.Fbnum = App.SelectedIBAN;

                    }

                    requestObj.AgreeFg = "X";

                    requestObj.Tin = App.LoginDataRetrieved.TIN;
                    requestObj.Iban = IBANValue;
                    requestObj.Bkext = SelectedBankName;
                    requestObj.Idnumber = SelectedIDNumber;
                    requestObj.IdtypeDesc = SelectedIDType;
                    requestObj.Koinh = AccountOwnerName;
                    requestObj.Bankid = SelectedBankNameValue;
                    requestObj.Type = SelectedIDTypeValue;

                    var IBANPostResponse = await IBanManagmentWebserviceManager.GAZTSubmitBankAccountIBAN(requestObj);
                    //TODO Rework 
                    if (IBANAccountData.d.AutoPopFg == true)
                    {
                        if (IBANPostResponse.d.Action.Equals("N"))
                        {
                            var somewarningpopup = new AttachmentInformationPopUp(AppResources.IBanSubmitSuccess1)
                            {
                                CloseWhenBackgroundIsClicked = false
                            };
                            somewarningpopup.OnDone = () =>
                            {
                                BankAccountRemoveNavigationPage();
                            };
                            await MopupService.Instance.PushAsync(somewarningpopup);
                        }
                        else if (IBANPostResponse.d.Action.Equals("U"))
                        {
                            var somewarningpopup = new AttachmentInformationPopUp(AppResources.IBanSubmitSuccess1)
                            {
                                CloseWhenBackgroundIsClicked = false
                            };
                            somewarningpopup.OnDone =  () =>
                            {
                                BankAccountRemoveNavigationPage();
                            };
                            await MopupService.Instance.PushAsync(somewarningpopup);
                        }

                    }
                    else
                    {
                        var somewarningpopup = new AttachmentInformationPopUp(AppResources.IBanSubmitSuccess)
                        {
                            CloseWhenBackgroundIsClicked = false
                        };
                        somewarningpopup.OnDone =  () =>
                        {
                            BankAccountRemoveNavigationPage();
                        };
                        await MopupService.Instance.PushAsync(somewarningpopup);
                    }
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    BankAccountRemoveNavigationPage();
                }
                catch (InternetException ex)
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    BankAccountRemoveNavigationPage();
                }

            }
            else
            {
                PopUp popUp = new PopUp();
                popUp.Message = AppResources.ZZPleaseselecttermsandconditions;
                if (App.IsArabic)
                {
                    popUp.FlowDirections = "RightToLeft";
                    popUp.isFontSet = true;
                }
                else
                {
                    popUp.FlowDirections = "LeftToRight";
                }

                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseselecttermsandconditions));
            }



        }
        public void FetchBankAccDetails()
        {
            try
            {
                if (IBANAccountData != null & IBANAccountData.d != null)
                {
                    //TODO REWORK
                    if (IBANAccountData.d.AutoPopFg == true)
                    {
                        AccountOwnerName = IBANAccountData.d.Name;
                        isNameEnabled = false;
                        IsDropdownVisibile = true;
                        IsIdInfoVisibility = false;
                        AttachmentVisible = false;
                    }
                    else
                    {
                        isNameEnabled = true;
                        AttachmentVisible = true;
                    }
                }
            }
            catch (Exception)
            {
                
                
            }
        }

        public void ContinueButtonClicked()
        {
            try
            {
                if (AccountOwnerName.Equals(""))
                {
                    MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.IBanInsertName));

                    return;
                }
                if (!IBANAccountData.d.AutoPopFg)
                {
                    if (AccountOwnerName.Contains("."))
                    {
                        IsBorderColorRed = Colors.Red;
                        MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_PleaseCorrectHighlightedFields));

                        return;
                    }
                }
                if (!IBANAccountData.d.AutoPopFg && !Regex.IsMatch(AccountOwnerName, @"^[a-zA-Z]+$"))
                {
                    IsBorderColorRed = Colors.Red;
                    MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_PleaseCorrectHighlightedFields));

                    return;
                }
                if (SelectedIDType.Equals(""))
                {
                    MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.IBanInsertIDType));

                    return;
                }
                if (SelectedIDNumber.Equals(""))
                {
                    MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

                    return;
                }
                if (IBANValue.Equals(""))
                {
                    MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZIBANRequired));

                    return;
                }

                if (!isIBanValid)
                {
                    MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZIBANisincorrect));

                    return;
                }
                if (SelectedBankName.Equals(""))
                {
                    MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.IBanSelectBank));

                    return;
                }
                if (!string.IsNullOrEmpty(SelectedBankName) && (SelectedBankName.Equals(AppResources.IBanSelectedOtherBankName)))
                {
                    if (selectedOtherBankName.Equals(""))
                    {
                        MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));
                        return;
                    }
                }

                if (string.IsNullOrEmpty(SelectedBankName) || string.IsNullOrEmpty(SelectedIDNumber) || string.IsNullOrEmpty(SelectedIDType) || string.IsNullOrEmpty(AccountOwnerName) || string.IsNullOrEmpty(IBANValue))
                {

                    MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));
                    return;
                }

                if (IBANValue.Length < 24)
                {

                    MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDIBANValidationforLenght));

                    return;

                }
                //TODO REWORK
                if (IBANAccountData.d.AutoPopFg == false)
                {
                    if (IBANBankListViewDataOne.Count != 1)
                    {
                        MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.IBANAttachmentErrorMsg));
                        return;
                    }
                    if (IBANBankListViewDataTwo.Count != 1)
                    {
                        MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.IBANAttachmentErrorMsg));
                        return;
                    }
                }

                var selectedType = IBANAccountData.d.IdTypeListSet.Find(selectedValue => (selectedValue.IdDesc == SelectedIDType));

                if (selectedType != null)
                {
                    SelectedIDTypeValue = selectedType.IdType;
                }
                if (IsIBanUpdatePage)
                {

                    BankIDResult = IBANAccountData.d.BankListSet.Find(selectedValue => (selectedValue.Bankid == UserBankid));
                }
                else
                {
                    BankIDResult = IBANAccountData.d.BankListSet.Find(selectedValue => (selectedValue.Bkext == SelectedBankName));

                }

                if (selectedType != null)
                {
                    if (BankIDResult != null && BankIDResult.Bankid != null)
                    {
                        SelectedBankNameValue = BankIDResult.Bankid;
                        if (!string.IsNullOrEmpty(SelectedBankName) && SelectedBankName.Equals(AppResources.IBanSelectedOtherBankName))
                            SelectedBankName = string.Copy(selectedOtherBankName);
                    }

                    else
                        SelectedBankNameValue = "9999";
                }
                EnableSummaryView();
            }
            catch (GAZTUnlockAccountException )
            {}
            catch (InternetException ex)
            {
                _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
        }

        public void BankAccountRemoveNavigationPage()
        {
            try
            {
                if (IBANAccountData.d.isRemove)
                {
                    var navigation = Application.Current.MainPage.Navigation;
                    var pagesToRemove = navigation.NavigationStack.Where(page => page.GetType() == typeof(BankAccountManagementPageView) || page.GetType() == typeof(BankAccountAddorUpdateIBANPageView)).ToList();
                    foreach (var page in pagesToRemove)
                    {
                        navigation.RemovePage(page);
                    }
                }
                else
                {
                    _navigationService.GoBack();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}

