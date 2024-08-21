using Microsoft.Maui.Controls.PlatformConfiguration;
using Newtonsoft.Json.Linq;
using Mopups.Services;
using System.Text;
using System.Text.RegularExpressions;
using ZATCAMAUI.ViewModel.NewDesignViewModel.IBanAccManagementsViewModel;
using static ZATCAMAUI.Models.IBanManagementListModel;
using ZATCAMAUI.Models;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Core.Exceptions;

namespace ZATCAMAUI.Views.NewDesign.IBanAccountsManagementPages
{
    public partial class BankAccountAddorUpdateIBANPageView : ContentPage
    {
        private BankAccountAddorUpdateIBANViewModel _viewModel;
        private int count = 0;
        private int IBanClickCount = 0;
        private string TpName = "";
        public List<IdTypeListSetResult> filteredList { get; set; }
        public BankAccountAddorUpdateIBANPageView(IBanAccountManagementResponseModel IBANAccountData)
        {

            try
            {

                Resources["IsInstrunctionCheckedStyle"] = App.Current.Resources["CheckboxUnselectedFontStyle"];

                InitializeComponent();
                _viewModel = App.Locator.BankAccountAddOrUpdatePageView;
                this.BindingContext = _viewModel;
                EntryIDNumber.IsEnabled = false;

                _viewModel.OtherBanksVisible = false;

                _viewModel.IsInstrunctionChecked = false;
                _viewModel.SummaryVisible = false;
                _viewModel.NewFormVisible = true;
                _viewModel.IsContinueButtonEnable = false;
                _viewModel.IsIBanDropDownEnabled = true;
                _viewModel.IsIBanUpdatePage = false;

                _viewModel.IsIdInfoVisibility = false;
                _viewModel.AttachmentVisible = false;
                _viewModel.IsDropdownVisibile = false;
                _viewModel.IsBorderColorRed = Colors.LightGray;
                // _viewModel.ContinueButtonnBackroundColor = Color.FromHex("#d49504");

                _viewModel.IBANAccountData = IBANAccountData;
                if (_viewModel.IBANAccountData.d != null)
                {
                    if (_viewModel.IBANAccountData.d.isUpdateFlag)
                    {
                        _viewModel.isIBanValid = true;
                    }
                    else
                    {
                        _viewModel.isIBanValid = false;
                    }
                }


                if (!App.LoginDataRetrieved.NameFirst.Equals(""))
                {
                    TpName = App.LoginDataRetrieved.NameFirst;
                }
                else if (!App.LoginDataRetrieved.NameLast.Equals(""))
                {
                    TpName = App.LoginDataRetrieved.NameLast;
                }
                else if (!App.LoginDataRetrieved.NameOrg1.Equals(""))
                {
                    TpName = App.LoginDataRetrieved.NameOrg1;
                }
                AckText.Text = string.Format(AppResources.NDIBANCertifyAck, TpName);
                BindInfoToViews();
                _viewModel.FetchBankAccDetails();
                Task.Run(() => _viewModel.IBANBankAccountFormGUID()).Wait();
                InitializationPopups();

                _viewModel.IBANBankListViewDataOne.Clear();
                _viewModel.IBANBankListViewDataTwo.Clear();
                _viewModel.IdNumberTitle = String.Format(AppResources.IBANIdNumber, "");
            }
            catch (Exception)
            {

            }
        }

        private void InitializationPopups()
        {
            MessagingCenter.Subscribe<object, Attachments>(this, "AttachmentReceived", (sender, arg) =>
            {
                if (arg != null)
                {
                    _viewModel.PopulateAttachments(arg.results);
                }
            });
        }


        private void BindInfoToViews()
        {
            try
            {
                if (string.IsNullOrEmpty(App.SelectedIBAN))
                {

                    _viewModel.SelectedIDType = "";
                    _viewModel.SelectedBankName = "";
                    _viewModel.SelectedBankNameField = "";
                    _viewModel.SelectedIDNumber = "";
                    _viewModel.AccountOwnerName = "";
                    _viewModel.IBANValue = "";
                }
                else
                {
                    var selectedIBAN = _viewModel.IBANAccountData.d.IbanListSet.Find(selectedValue => (selectedValue.Fbnum == App.SelectedIBAN));

                    if (selectedIBAN != null)
                    {
                        _viewModel.SelectedIDType = selectedIBAN.IdtypeDesc;
                        _viewModel.SelectedBankName = selectedIBAN.Bkext;
                        _viewModel.SelectedBankNameField = selectedIBAN.Bkext;
                        _viewModel.SelectedIDNumber = selectedIBAN.Idnumber;
                        _viewModel.AccountOwnerName = selectedIBAN.Koinh;
                        _viewModel.IBANValue = selectedIBAN.Iban;
                        _viewModel.IsIBanDropDownEnabled = false;
                        var MissingIfoMatch = _viewModel.IBANAccountData.d.IbanListSet.Find(selectedValue => (selectedValue.StatusDesc == "Missing Informaiton") || (selectedValue.StatusDesc == "معلومات الحساب غير مكتملة"));
                        if (MissingIfoMatch != null)
                        {
                            BankAccountIBAN.IsEnabled = false;
                            _viewModel.IsIBanUpdatePage = true;
                            _viewModel.UserBankid = selectedIBAN.Bankid;
                        }


                    }
                }

                MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) =>
                {
                    _viewModel.PickerModel = arg;
                    //EntryIDNumber.Text = string.Empty;
                    EntryIDNumber.IsEnabled = true;
                    var selectedType = string.Empty;
                    string SelectedIDTypeValue = string.Empty;
                    if (arg.PickerId == "IBANIdTypePicker")
                    {
                        EntryIDNumber.Text = string.Empty;
                        _viewModel.SelectedIDNumber = "";
                        if (!_viewModel.SelectedIDType.Equals(arg.SelectedValue))
                        {
                            _viewModel.IsIdInfoVisibility = false;
                            _viewModel.IBANBankListViewDataOne.Clear();
                            _viewModel.IBANBankListViewDataTwo.Clear();
                        }
                        _viewModel.SelectedIDType = arg.SelectedValue;
                        if (_viewModel.IBANAccountData.d.AutoPopFg == true)
                        {
                            _viewModel.IdNumberTitle = String.Format(AppResources.IBANIdNumberDynamic, _viewModel.SelectedIDType);
                        }
                        else
                        {
                            _viewModel.IdNumberTitle = AppResources.IBANIdNumber;
                        }
                        filteredList = _viewModel.IBANAccountData.d.IdTypeListSet.Where(x => x.IdDesc.Equals(_viewModel.SelectedIDType)).ToList();
                        _viewModel.FetchBankAccDetails();

                        if (filteredList != null && filteredList.Count > 0)
                        {
                            //gcc id
                            if (filteredList[0].IdType == "ZS0003")
                            {
                                EntryIDNumber.MaxLength = 15;
                            }
                            else
                            {
                                EntryIDNumber.MaxLength = 15;
                            }
                        }


                    }
                    else if (arg.PickerId == "IBANIdNumberPicker")
                    {
                        _viewModel.SelectedIDNumber = arg.SelectedValue;
                        EntryIDNumber.IsEnabled = false;
                        if (!string.IsNullOrEmpty(_viewModel.SelectedIDNumber))
                        {
                            _viewModel.AccountOwnerName = _viewModel.IBANAccountData.d.IdNumberListSet.Where(x => x.IdNumber == _viewModel.SelectedIDNumber).FirstOrDefault().Actnm;
                        }
                        if (string.IsNullOrEmpty(_viewModel.AccountOwnerName))
                        {
                            _viewModel.FetchBankAccDetails();
                        }
                    }
                    else if (arg.PickerId == "IBANBankNamePicker")
                    {
                        _viewModel.SelectedBankName = arg.SelectedValue;
                        _viewModel.SelectedBankNameField = arg.SelectedValue;
                        if (arg.SelectedValue == AppResources.IBanSelectedOtherBankName)
                            _viewModel.OtherBanksVisible = true;
                        else
                            _viewModel.OtherBanksVisible = false;
                    }

                });
            }
            catch (Exception e)
            {

            }
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem");

        }



        private void AccountOwnerNameTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.AccountOwnerName = e.NewTextValue;
        }

        private void AccountOwnerName_Unfocused(System.Object sender, FocusEventArgs e)
        {
            if (!_viewModel.IBANAccountData.d.AutoPopFg && !Regex.IsMatch(_viewModel.AccountOwnerName, @"^[a-zA-Z]+$"))
            {
                _viewModel.IsBorderColorRed = Colors.Red;
                MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_PleaseCorrectHighlightedFields));
            }
            else
            {
                _viewModel.IsBorderColorRed = Colors.LightGray;
            }
        }
        private void IBANTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.IsIBanDropDownEnabled = true;
            string allowedchar = "0123456789";
            allowedchar += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            // if (e.NewTextValue.Length > 0) {

            //string str = e.NewTextValue.Substring(0, 2);
            _viewModel.IBANValue = e.NewTextValue;

            if (e.NewTextValue.Length >= 2)
            {
                if (!_viewModel.IBANValue.StartsWith("SA"))
                {
                    count = count + 1;
                    BankAccountIBAN.Text = "";
                    _viewModel.IBANValue = "";
                    if (count == 1)
                    {
                        // MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDIBANValidationforSA));

                        var somewarningpopup = new AttachmentInformationPopUp(AppResources.NDIBANValidationforSA)
                        {
                            CloseWhenBackgroundIsClicked = false
                        };
                        somewarningpopup.OnDone = async () =>
                        {
                            count = 0;
                        };
                        MopupService.Instance.PushAsync(somewarningpopup);
                    }
                }
                else
                {
                    if (_viewModel.IBANValue.Length >= 3)
                    {
                        if (!_viewModel.IBANValue.Substring(2).All(allowedchar.Contains))
                        {
                            _viewModel.IBANValue = _viewModel.IBANValue.Remove(_viewModel.IBANValue.Length - 1);


                        }
                    }

                }
            }

        }

        private async void checkIBanIsValidOrNot()
        {
            App.IBanValidatedResponse = string.Empty;
            _viewModel.IsLoading = true;
            try
            {
                try
                {
                    App.IBanValidatedResponse = string.Empty;
                    var response = await WebServiceManager.GAZTCheckIBAN(_viewModel.IBANValue);
                    if (response != null)
                    {
                        //IBan is Valid
                        _viewModel.isIBanValid = true;
                        _viewModel.IsLoading = false;
                        string bankName = string.Empty;
                        bankName = JObject.Parse(App.IBanValidatedResponse)["result"].ToString();
                        string IBanSelectedBankName = JObject.Parse(bankName)["bankDetails"].ToString();

                        _viewModel.IsIBanDropDownEnabled = false;

                        if (string.IsNullOrEmpty(IBanSelectedBankName))
                        {
                            _viewModel.SelectedBankName = AppResources.IBanSelectedOtherBankName;
                            _viewModel.SelectedBankNameField = AppResources.IBanSelectedOtherBankName;
                            _viewModel.OtherBanksVisible = true;
                        }
                        else
                        {
                            _viewModel.SelectedBankName = JObject.Parse(bankName)["bankDetails"].ToString();
                            _viewModel.SelectedBankNameField = JObject.Parse(bankName)["bankDetails"].ToString();
                            _viewModel.OtherBanksVisible = false;
                        }


                    }
                    else
                    {
                        _viewModel.IsIBanDropDownEnabled = true;
                        _viewModel.IsLoading = false;
                        _viewModel.SelectedIDNumber = "";
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            _viewModel.isIBanValid = false;
                            //_viewModel._dialogService.ShowMessage(AppResources.ZZIBANisincorrect, AppResources.Information);
                            MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZIBANisincorrect));
                        });
                    }
                }
                catch (InternetException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        _viewModel.IsLoading = false;
                        //_viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                        MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                    });
                }
            }
            catch (Exception ex)
            {
                //IBan is InValid
                _viewModel.isIBanValid = false;
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    _viewModel.IsLoading = false;
                    //_viewModel._dialogService.ShowMessage(AppResources.ZZIBANisincorrect, AppResources.Information);
                    MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZIBANisincorrect));

                });
            }
        }

        private void IBANFocusChnaged(object sender, TextChangedEventArgs e)
        {

            if ((_viewModel.IBANValue.Length > 0) && (_viewModel.IBANValue.Length < 24))
            {

                MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDIBANValidationforLenght));

            }
            if ((_viewModel.IBANValue.Length > 0) && (_viewModel.IBANValue.Length == 24))
            {
                //_viewModel.isIBanValid = true;
                checkIBanIsValidOrNot();
            }

        }

        private void OtherBanksTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.selectedOtherBankName = e.NewTextValue;

        }

        private void OtherBanksFocusChnaged(object sender, FocusEventArgs e)
        {
            // _viewModel.selectedOtherBankName = OtherBankName.Text;

            if (_viewModel.selectedOtherBankName.Length == 0)
            {
                MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));
            }
        }

        private void IdNumberTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {

                _viewModel.SelectedIDNumber = e.NewTextValue;

                //FrmIdNumber.HasError = false;

                if (_viewModel.SelectedIDType == "")
                {

                }
                else if (_viewModel.SelectedIDType == "")
                {

                }
                else if (_viewModel.SelectedIDType == "")
                {

                }
                else if (_viewModel.SelectedIDType == "")
                {

                }
            }
            catch (Exception)
            {

            }
        }

        private void EntryIDNumber_Unfocused(object sender, FocusEventArgs e)
        {
            PopUp popUp = new PopUp();
            StringBuilder Messages = new StringBuilder();
            if (_viewModel.IBANAccountData.d.AutoPopFg == false && !string.IsNullOrEmpty(EntryIDNumber.Text))
            {
                _viewModel.IsIdInfoVisibility = true;
                _viewModel.IdNumberInfo = string.Format(AppResources.IBANIdNumberInfo, EntryIDNumber.Text);
            }

            if (!string.IsNullOrEmpty(EntryIDNumber.Text))
            {

                List<IdTypeListSetResult> filteredList = _viewModel.IBANAccountData.d.IdTypeListSet.Where(x => x.IdDesc.Equals(_viewModel.SelectedIDType)).ToList();

                if (filteredList != null && filteredList.Count > 0)
                {
                    //National Id
                    if (filteredList[0].IdType == "ZS0001")
                    {
                        if (EntryIDNumber.Text.Substring(0, 1) != "1")
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
                            // MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                            MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZNationalIDstartswith1));
                            EntryIDNumber.Text = string.Empty;
                            _viewModel.SelectedIDNumber = string.Empty;
                            _viewModel.IsIdInfoVisibility = false;
                            //ZZPleaseenteravalidNationalID
                        }
                        else
                        {
                            if (EntryIDNumber.Text.Length != 10)
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
                                // MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                                MopupService.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));
                                EntryIDNumber.Text = string.Empty;
                                _viewModel.SelectedIDNumber = string.Empty;
                                _viewModel.IsIdInfoVisibility = false;
                            }

                        }
                    }
                    //iqama
                    else if (filteredList[0].IdType == "ZS0002")
                    {
                        if (EntryIDNumber.Text.Substring(0, 1) != "2")
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
                            //  MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                            MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZIqamaIDstartswith2));
                            EntryIDNumber.Text = string.Empty;
                            _viewModel.SelectedIDNumber = string.Empty;
                            _viewModel.IsIdInfoVisibility = false;
                        }
                        else
                        {
                            if (EntryIDNumber.Text.Length != 10)
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
                                // MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                                MopupService.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));

                                EntryIDNumber.Text = string.Empty;
                                _viewModel.SelectedIDNumber = string.Empty;
                                _viewModel.IsIdInfoVisibility = false;
                            }

                        }
                    }
                    //gcc id
                    else if (filteredList[0].IdType == "ZS0003")
                    {
                        bool flag = true;
                        if (EntryIDNumber.Text.Substring(0, 1) == "0")
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
                            //  MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                            MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGCCIDdonotstartwith0));
                            EntryIDNumber.Text = string.Empty;
                            _viewModel.SelectedIDNumber = string.Empty;
                            _viewModel.IsIdInfoVisibility = false;
                        }
                        else if (!(EntryIDNumber.Text.Length <= 15 && EntryIDNumber.Text.Length >= 7))
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
                            MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit));
                            EntryIDNumber.Text = string.Empty;
                            _viewModel.SelectedIDNumber = string.Empty;
                            _viewModel.IsIdInfoVisibility = false;
                            // EntryIDNumber.Text = string.Empty;//ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit
                        }

                    }
                    //compny id
                    else if (filteredList[0].IdType == "ZS0005")
                    {
                        if (EntryIDNumber.Text.Substring(0, 1) != "7")
                        {
                            //Have to change to neww error message
                            popUp.Message = AppResources.IBanCompanyIdStartswith7;
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
                            //  MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                            MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.IBanCompanyIdStartswith7));
                            EntryIDNumber.Text = string.Empty;
                            _viewModel.SelectedIDNumber = string.Empty;
                            _viewModel.IsIdInfoVisibility = false;
                        }
                        else if ((EntryIDNumber.Text.Length <= 9))
                        {
                            popUp.Message = AppResources.IBanCompanyIdshouldbelessthan10;
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
                            MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.IBanCompanyIdshouldbelessthan10));
                            EntryIDNumber.Text = string.Empty;
                            _viewModel.SelectedIDNumber = string.Empty;
                            _viewModel.IsIdInfoVisibility = false;
                            // EntryIDNumber.Text = string.Empty;//ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit
                        }
                    }
                    //commercial reg num
                    else if (filteredList[0].IdType == "BUP002")
                    {
                        EntryIDNumber.MaxLength = 10;
                        if ((EntryIDNumber.Text.Length <= 9))
                        {
                            popUp.Message = AppResources.IBanCommercialIdShouldbe10;
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
                            MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.IBanCommercialIdShouldbe10));
                            EntryIDNumber.Text = string.Empty;
                            _viewModel.SelectedIDNumber = string.Empty;
                            _viewModel.IsIdInfoVisibility = false;
                            // EntryIDNumber.Text = string.Empty;//ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit
                        }
                    }
                }
            }

        }


    }
}

