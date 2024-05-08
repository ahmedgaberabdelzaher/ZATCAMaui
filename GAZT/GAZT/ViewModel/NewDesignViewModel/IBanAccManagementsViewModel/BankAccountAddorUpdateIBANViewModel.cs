using EGAZT.Manager;
using EGAZT.Models;
using EGAZT.Models.ZakatInstalationModels;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Views.NewDesign.GenericPickers;
using EGAZT.Views.NewDesign.ZakatInstalmentPlan;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using static EGAZT.Models.IBanManagementListModel;
using Attachment = EGAZT.Models.Attachment;
using Result = EGAZT.Models.IBanManagementListModel.Result;

namespace EGAZT.ViewModel.NewDesignViewModel.IBanAccManagementsViewModel
{

    [Preserve(AllMembers = true)]
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
                RaisePropertyChanged(nameof(CurrentIndex));
                if (_currenrIndex == MaxIndex)
                {
                    MarkComplete = true;
                    RaisePropertyChanged(nameof(MarkComplete));
                }
                else
                {
                    MarkComplete = false;
                    RaisePropertyChanged(nameof(MarkComplete));
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
                RaisePropertyChanged("SelectedIDType");
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
                RaisePropertyChanged("SelectedIDNumber");
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
                RaisePropertyChanged("SelectedBankName");
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
                RaisePropertyChanged("SelectedBankNameField");
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
                RaisePropertyChanged("SelectedIDTypeValue");
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
                RaisePropertyChanged("SelectedIDNumberValue");
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
                RaisePropertyChanged("SelectedBankNameValue");
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
                RaisePropertyChanged("AccountOwnerName");
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
                RaisePropertyChanged("isNameEnabled");
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
                RaisePropertyChanged("IBANValue");
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
                RaisePropertyChanged("IdNumber");
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
                RaisePropertyChanged("IdNumberInfo");
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
                RaisePropertyChanged("IBANAccountData");
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
                RaisePropertyChanged("PickerModel");
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
                RaisePropertyChanged("NewFormVisible");
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
                RaisePropertyChanged("AttachmentVisible");
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
                RaisePropertyChanged("IsIBanDropDownEnabled");
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
                RaisePropertyChanged("IsIBanUpdatePage");
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
                RaisePropertyChanged("IsIdInfoVisibility");
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


                RaisePropertyChanged("IsInstrunctionChecked");
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
                RaisePropertyChanged("IsContinueButtonEnable");
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
                RaisePropertyChanged("ContinueButtonnBackroundColor");
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
                RaisePropertyChanged("OtherBanksVisible");
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
                RaisePropertyChanged("IBANBankListViewDataOne");
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
                RaisePropertyChanged("IBANBankListViewDataTwo");
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
                RaisePropertyChanged("IBANAccountDataFormGuid");
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
                RaisePropertyChanged("IsDropdownVisibile");
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
                RaisePropertyChanged("IdNumberTitle");
            }
        }

        private Color _isBorderColorRed = Color.LightGray;

        public Color IsBorderColorRed
        {
            get { return _isBorderColorRed; }
            set
            {
                if (_isBorderColorRed == value) return;

                _isBorderColorRed = value;
                RaisePropertyChanged("IsBorderColorRed");
            }
        }
        public BankAccountAddorUpdateIBANViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {



            GoBackBtnTapped = new Command(async () => { BackNavigations(); });

            ContinueButtonTapped = new Command(this.ContinueButtonClicked);

            ShowIDTypePicker = new Command(() => { showPickerDialog(1); });
            ShowIDNumberPicker = new Command(() => { showPickerDialog(2); });
            ShowBankNamePicker = new Command(() => { showPickerDialog(3); });
            GoBackToNewForm = new Command(() => { SummaryEditClicked(); });
            SummaryConBtnTapped = new Command(() => { SummaryConButtonClicked(); });
            OnAttachmentClickOne = new Xamarin.Forms.Command(async () =>
            {
                await AddAttachmentTestOne();
            });
            OnAttachmentClickTwo = new Xamarin.Forms.Command(async () =>
            {
                await AddAttachmentTestTwo();
            });
            NewFormVisible = true;


        }

        public async void IBANBankAccountFormGUID()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    IBANAccountDataFormGuid = await IBanManagmentWebserviceManager.GAZTGetIBanAccountsFormGUID();

                    PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public async Task AddAttachmentTestOne()
        {
            if (Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopupStack.Count > 0) return;
            if (IBANBankListViewDataOne == null)
            {
                IBANBankListViewDataOne = new ObservableCollection<Attachment>();
            }

            try
            {
                SelectedAttachmentNumber = (int)WhichAttachment.IBANBankAccountOne;
                await PopupNavigation.Instance.PushAsync(new FilesUploadPopUpPageView(
                     IBANBankListViewDataOne.ToList(),
                     Models.ZakatInstalationModels.WhichAttachment.IBANBankAccountOne, IBANAccountDataFormGuid.d.FormGuid
                    ));
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
        public async Task AddAttachmentTestTwo()
        {
            if (Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopupStack.Count > 0) return;
            if (IBANBankListViewDataTwo == null)
            {
                IBANBankListViewDataTwo = new ObservableCollection<Attachment>();
            }

            try
            {
                SelectedAttachmentNumber = (int)WhichAttachment.IBANBankAccountTwo;
                await PopupNavigation.Instance.PushAsync(new FilesUploadPopUpPageView(
                     IBANBankListViewDataTwo.ToList(),
                     Models.ZakatInstalationModels.WhichAttachment.IBANBankAccountTwo, IBANAccountDataFormGuid.d.FormGuid
                    ));
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
                    RaisePropertyChanged("IBANBankListViewDataOne");
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

            /*if (SelectedBankName.Equals(AppResources.IBanSelectedOtherBankName))
            {
                SelectedBankName = selectedOtherBankName;
            }*/
        }

        private void setIDTypePickerModel()
        {

            if (PickerModel != null)
            {

                PickerModel = null;
            }


            var list = new List<string>();

            foreach (IdTypeListSetResult dropdown in IBANAccountData.d.IdTypeListSet.results)
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
            var selectedType = IBANAccountData.d.IdTypeListSet.results.Find(selectedValue => (selectedValue.IdDesc == SelectedIDType));

            if (selectedType != null)
            {
                SelectedIDTypeValue = selectedType.IdType;
            }

            for (int i = 0; i < IBANAccountData.d.IdNumberListSet.results.Count; i++)
            {
                if (IBANAccountData.d.IdNumberListSet.results[i].IdType.Equals(SelectedIDTypeValue))
                {
                    try
                    {
                        list.Add(IBANAccountData.d.IdNumberListSet.results[i].IdNumber);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }



            /*foreach (IdNumberListSetResult dropdown in IBANAccountData.d.IdNumberListSet.results)
            {
                try
                {
                    list.Add(dropdown.IdNumber);
                }
                catch (Exception ex)
                {

                }

            }*/
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

            foreach (Result dropdown in IBANAccountData.d.BankListSet.results)
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

        private async void showPickerDialog(int pickerID)
        {
            try
            {

                if (pickerID == 1)
                {

                    setIDTypePickerModel();
                    await PopupNavigation.Instance.PushAsync(new PickerPageView(PickerModel));
                }
                else if (pickerID == 2)
                {
                    try
                    {
                        if (SelectedIDType == "")
                            return;
                        else
                        {
                            setIDNumberPickerModel();
                            await PopupNavigation.Instance.PushAsync(new PickerPageView(PickerModel));
                        }

                    }
                    catch (Exception e)
                    {

                    }

                }
                else if (pickerID == 3)
                {

                    setBankNamePickerModel();
                    await PopupNavigation.Instance.PushAsync(new PickerPageView(PickerModel));
                }


            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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

        private async void SummaryConButtonClicked()
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

                    if (IBANAccountData.d.AutoPopFg == true)
                    {
                        if (IBANPostResponse.d.Action.Equals("N"))
                        {
                            var somewarningpopup = new AttachmentInformationPopUp(AppResources.IBanSubmitSuccess1)
                            {
                                CloseWhenBackgroundIsClicked = false
                            };
                            somewarningpopup.OnDone = async () =>
                            {
                                _navigationService.GoBack();
                            };
                            await PopupNavigation.Instance.PushAsync(somewarningpopup);
                        }
                        else if (IBANPostResponse.d.Action.Equals("U"))
                        {
                            var somewarningpopup = new AttachmentInformationPopUp(AppResources.IBanSubmitSuccess1)
                            {
                                CloseWhenBackgroundIsClicked = false
                            };
                            somewarningpopup.OnDone = async () =>
                            {
                                _navigationService.GoBack();
                            };
                            await PopupNavigation.Instance.PushAsync(somewarningpopup);
                        }

                    }
                    else
                    {
                        var somewarningpopup = new AttachmentInformationPopUp(AppResources.IBanSubmitSuccess)
                        {
                            CloseWhenBackgroundIsClicked = false
                        };
                        somewarningpopup.OnDone = async () =>
                        {
                            _navigationService.GoBack();
                        };
                        await PopupNavigation.Instance.PushAsync(somewarningpopup);
                    }
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {


                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        _navigationService.GoBack();
                    });
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

                //await PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseselecttermsandconditions));
            }



        }
        public void FetchBankAccDetails()
        {
            try
            {
                if (IBANAccountData != null & IBANAccountData.d != null)
                {
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
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }

        public void ContinueButtonClicked()
        {
            try
            {
                if (AccountOwnerName.Equals(""))
                {
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.IBanInsertName));

                    return;
                }
                if (AccountOwnerName.Contains("."))
                {
                    IsBorderColorRed = Color.Red;
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_PleaseCorrectHighlightedFields));

                    return;
                }
                if (!IBANAccountData.d.AutoPopFg && !Regex.IsMatch(AccountOwnerName, @"^[a-zA-Z]+$"))
                {
                    IsBorderColorRed = Color.Red;
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_PleaseCorrectHighlightedFields));

                    return;
                }
                if (SelectedIDType.Equals(""))
                {
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.IBanInsertIDType));

                    return;
                }
                if (SelectedIDNumber.Equals(""))
                {
                    //PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.IBanInsertIDNumber));
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));

                    return;
                }
                if (IBANValue.Equals(""))
                {
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZIBANRequired));

                    return;
                }

                if (!isIBanValid)
                {
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZIBANisincorrect));

                    return;
                }
                if (SelectedBankName.Equals(""))
                {
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.IBanSelectBank));

                    return;
                }
                if (!string.IsNullOrEmpty(SelectedBankName) && (SelectedBankName.Equals(AppResources.IBanSelectedOtherBankName)))
                {
                    if (selectedOtherBankName.Equals(""))
                    {
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));
                        return;
                    }
                }

                if (string.IsNullOrEmpty(SelectedBankName) || string.IsNullOrEmpty(SelectedIDNumber) || string.IsNullOrEmpty(SelectedIDType) || string.IsNullOrEmpty(AccountOwnerName) || string.IsNullOrEmpty(IBANValue))
                {

                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));
                    return;
                }
                /*else
                {*/

                if (IBANValue.Length < 24)
                {

                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDIBANValidationforLenght));

                    return;

                }
                if (IBANAccountData.d.AutoPopFg == false)
                {
                    if (IBANBankListViewDataOne.Count != 1)
                    {
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.IBANAttachmentErrorMsg));
                        return;
                    }
                    if (IBANBankListViewDataTwo.Count != 1)
                    {
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.IBANAttachmentErrorMsg));
                        return;
                    }
                }

                var selectedType = IBANAccountData.d.IdTypeListSet.results.Find(selectedValue => (selectedValue.IdDesc == SelectedIDType));

                if (selectedType != null)
                {
                    SelectedIDTypeValue = selectedType.IdType;
                }



                if (IsIBanUpdatePage)
                {

                    BankIDResult = IBANAccountData.d.BankListSet.results.Find(selectedValue => (selectedValue.Bankid == UserBankid));
                }
                else
                {
                    BankIDResult = IBANAccountData.d.BankListSet.results.Find(selectedValue => (selectedValue.Bkext == SelectedBankName));

                }

                if (selectedType != null)
                {
                    if (BankIDResult != null && BankIDResult.Bankid != null)
                    {
                        SelectedBankNameValue = BankIDResult.Bankid;
                        if (!string.IsNullOrEmpty(SelectedBankName) && SelectedBankName.Equals(AppResources.IBanSelectedOtherBankName))
                            SelectedBankName = String.Copy(selectedOtherBankName);
                    }

                    else
                        SelectedBankNameValue = "9999";
                }



                EnableSummaryView();
                //}



            }
            catch (GAZTUnlockAccountException ex)
            {

                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());

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
    }
}
