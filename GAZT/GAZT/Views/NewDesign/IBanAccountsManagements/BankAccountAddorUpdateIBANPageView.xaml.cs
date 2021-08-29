using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.IBanAccManagementsViewModel;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Views.NewDesign.GenericPickers;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using static EGAZT.Models.IBanManagementListModel;

namespace EGAZT.Views.NewDesign.IBanAccountsManagements
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [Preserve(AllMembers = true)]
    public partial class BankAccountAddorUpdateIBANPageView : ContentPage
    {
        private BankAccountAddorUpdateIBANViewModel _viewModel;
        private int count = 0;
        private int IBanClickCount = 0;
        private string TpName = "";
        public BankAccountAddorUpdateIBANPageView(IBanAccountManagementResponseModel IBANAccountData)
        {
            Resources["IsInstrunctionCheckedStyle"] = App.Current.Resources["CheckboxUnselectedFontStyle"];

            InitializeComponent();
            ChangeAeroIcon();

            SetLTR();

            _viewModel = App.Locator.BankAccountAddOrUpdatePageView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = _viewModel;
            EntryIDNumber.IsEnabled = false;

            _viewModel.OtherBanksVisible = false;

            _viewModel.IsInstrunctionChecked = false;
            _viewModel.SummaryVisible = false;
            _viewModel.NewFormVisible = true;
            _viewModel.IsContinueButtonEnable = false;
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
        }
        /*private async void ValidateTermsAndConditions()
        {
            if (_viewModel.IsInstrunctionChecked == true)
            {
                
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
                await Task.Run(() =>
                {
                    _viewModel.IsLoading = false;
                });
                //await PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseselecttermsandconditions));
                chkDeclaration.Focus();
            }
        }*/

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;
        }

        private void BindInfoToViews()
        {
            try
            {
                if (string.IsNullOrEmpty(App.SelectedIBAN))
                {

                    _viewModel.SelectedIDType = "";
                    _viewModel.SelectedBankName = "";
                    _viewModel.SelectedIDNumber = "";
                    _viewModel.AccountOwnerName = "";
                    _viewModel.IBANValue = "";
                }
                else
                {


                    var selectedIBAN = _viewModel.IBANAccountData.d.IbanListSet.results.Find(selectedValue => (selectedValue.Fbnum == App.SelectedIBAN));

                    if (selectedIBAN != null)
                    {
                        _viewModel.SelectedIDType = selectedIBAN.IdtypeDesc;
                        _viewModel.SelectedBankName = selectedIBAN.Bkext;
                        _viewModel.SelectedIDNumber = selectedIBAN.Idnumber;
                        _viewModel.AccountOwnerName = selectedIBAN.Koinh;
                        _viewModel.IBANValue = selectedIBAN.Iban;
                    }


                }

                MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) =>
                {
                    _viewModel.PickerModel = arg;
                    //EntryIDNumber.Text = string.Empty;
                    EntryIDNumber.IsEnabled = true;
                    var selectedType=string.Empty;
                    string SelectedIDTypeValue = string.Empty;
                    if (arg.PickerId == "IBANIdTypePicker")
                    {
                        EntryIDNumber.Text = string.Empty;
                        _viewModel.SelectedIDType = arg.SelectedValue;
                        _viewModel.SelectedIDNumber = "";
                        if (_viewModel.SelectedIDType == "Company ID" || _viewModel.SelectedIDType == "معرف الشركة")
                        {
                            EntryIDNumber.MaxLength = 10;
                        }
                        else if (_viewModel.SelectedIDType == "Commercial Register Number" || (_viewModel.SelectedIDType == "رقم السجل التجاري"))
                        {
                            EntryIDNumber.MaxLength = 10;
                        }
                        else
                        {
                            EntryIDNumber.MaxLength = 25;
                        }
                        
                    }
                    else if (arg.PickerId == "IBANIdNumberPicker")
                    {
                        _viewModel.SelectedIDNumber = arg.SelectedValue;
                    }
                    else if (arg.PickerId == "IBANBankNamePicker")
                    {
                        _viewModel.SelectedBankName = arg.SelectedValue;
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

        private void SetLTR()
        {
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        public void ChangeAeroIcon()
        {
            try
            {
                if (App.IsArabic)
                {
                    Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
                }
                else
                {
                    Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
                }

            }
            catch (Exception)
            {

            }

        }

        private void AccountOwnerNameTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.AccountOwnerName = e.NewTextValue;


        }

        private void IBANTextChanged(object sender, TextChangedEventArgs e)
        {
            string allowedchar = "0123456789";
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
                        // PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDIBANValidationforSA));

                        var somewarningpopup = new AttachmentInformationPopUp(AppResources.NDIBANValidationforSA)
                        {
                            CloseWhenBackgroundIsClicked = false
                        };
                        somewarningpopup.OnDone = async () =>
                        {
                            count = 0;
                        };
                        PopupNavigation.Instance.PushAsync(somewarningpopup);
                    }
                }
                else
                {
                    if (_viewModel.IBANValue.Length >= 3)
                    {
                        if (!_viewModel.IBANValue.Substring(2).All(allowedchar.Contains))
                        {
                            _viewModel.IBANValue = _viewModel.IBANValue.Remove(_viewModel.IBANValue.Length - 1);
                            /*
                                                        if (_viewModel.IBANValue.Length == 24)
                                                        {
                                                            checkIBanIsValidOrNot();
                                                        }*/
                            /*if (_viewModel.IBANValue.Length > 24)
                            {
                                _viewModel.IBANValue = _viewModel.IBANValue.Remove(_viewModel.IBANValue.Length - 1);  // Remove Last character
                                BankAccountIBAN.Text = _viewModel.IBANValue;
                                if (_viewModel.IBANValue.Length == 24)
                                {
                                    checkIBanIsValidOrNot();
                                }
                            }*/

                        }
                        /*else
                        {
                            if (_viewModel.IBANValue.Length == 24)
                            {
                                checkIBanIsValidOrNot();
                            }
                        }*/

                    }

                    /*if (_viewModel.IBANValue.Length > 24)
                    {
                        IBanClickCount = IBanClickCount + 1;
                        _viewModel.IBANValue = _viewModel.IBANValue.Remove(_viewModel.IBANValue.Length - 1);  // Remove Last character
                        BankAccountIBAN.Text = _viewModel.IBANValue;        //Set the Old value
                        if (IBanClickCount == 1)
                        {
                            // PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDIBANValidationforSA));

                            var somewarningpopup = new AttachmentInformationPopUp(AppResources.NDIBANValidationforLenght)
                            {
                                CloseWhenBackgroundIsClicked = false
                            };
                            somewarningpopup.OnDone = async () =>
                            {
                                IBanClickCount = 0;
                            };
                            await PopupNavigation.Instance.PushAsync(somewarningpopup);
                        }

                    }*/
                }
            }
            /*if (str.Equals("SA") || str.Equals("S"))
            {

                if (!_viewModel.IBANValue.All(allowedchar.Contains))
                {
                    _viewModel.IBANValue = _viewModel.IBANValue.Remove(_viewModel.IBANValue.Length - 1);
                }
                if (_viewModel.IBANValue.Length > 24)
                {
                    IBanClickCount = IBanClickCount + 1;
                    _viewModel.IBANValue = _viewModel.IBANValue.Remove(_viewModel.IBANValue.Length - 1);  // Remove Last character
                    BankAccountIBAN.Text = _viewModel.IBANValue;        //Set the Old value
                    if (IBanClickCount == 1)
                    {
                        // PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDIBANValidationforSA));

                        var somewarningpopup = new AttachmentInformationPopUp(AppResources.NDIBANValidationforLenght)
                        {
                            CloseWhenBackgroundIsClicked = false
                        };
                        somewarningpopup.OnDone = async () =>
                        {
                            IBanClickCount = 0;
                        };
                        await PopupNavigation.Instance.PushAsync(somewarningpopup);
                    }

                }
            }
            else
            {
                count = count + 1;
                BankAccountIBAN.Text = "";
                _viewModel.IBANValue = "";
                if (count == 1)
                {
                    // PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDIBANValidationforSA));

                    var somewarningpopup = new AttachmentInformationPopUp(AppResources.NDIBANValidationforSA)
                    {
                        CloseWhenBackgroundIsClicked = false
                    };
                    somewarningpopup.OnDone = async () =>
                    {
                        count = 0;
                    };
                    await PopupNavigation.Instance.PushAsync(somewarningpopup);
                }
            }*/

            //}

        }

        private void checkIBanIsValidOrNot()
        {
            App.IBanValidatedResponse = string.Empty;
            _viewModel.IsLoading = true;
            try
            {
                try
                {
                    App.IBanValidatedResponse = string.Empty;
                    var response = WebServiceManager.GAZTCheckIBAN(_viewModel.IBANValue);
                    if (response != null)
                    {
                        //IBan is Valid
                        _viewModel.isIBanValid = true;
                        _viewModel.IsLoading = false;
                        string bankName = string.Empty;
                        bankName = JObject.Parse(App.IBanValidatedResponse)["d"].ToString();
                        _viewModel.SelectedBankName = JObject.Parse(bankName)["Bkext"].ToString();
                        _viewModel.OtherBanksVisible = false;
                    }
                    else
                    {
                        _viewModel.IsLoading = false;
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            _viewModel.isIBanValid = false;
                            //_viewModel._dialogService.ShowMessage(AppResources.ZZIBANisincorrect, AppResources.Information);
                            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZIBANisincorrect));
                        });
                    }
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        _viewModel.IsLoading = false;
                        //_viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                    });
                }
            }
            catch (Exception ex)
            {
                //IBan is InValid
                _viewModel.isIBanValid = false;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _viewModel.IsLoading = false;
                    //_viewModel._dialogService.ShowMessage(AppResources.ZZIBANisincorrect, AppResources.Information);
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZIBANisincorrect));

                });
            }
        }

        private void IBANFocusChnaged(object sender, TextChangedEventArgs e)
        {

            if ((_viewModel.IBANValue.Length > 0) && (_viewModel.IBANValue.Length < 24))
            {

                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDIBANValidationforLenght));

            }
            if ((_viewModel.IBANValue.Length > 0) && (_viewModel.IBANValue.Length == 24))
            {
                //_viewModel.isIBanValid = true;
                checkIBanIsValidOrNot();
            }

            /*else if(_viewModel.IBANValue.Length>24)
            {
                _viewModel.IBANValue = _viewModel.IBANValue.Remove(_viewModel.IBANValue.Length - 1);  // Remove Last character
                BankAccountIBAN.Text = _viewModel.IBANValue;        //Set the Old value

                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDIBANValidationforLenght));

            }*/

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
                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));
            }
        }

        private void IdNumberTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                _viewModel.SelectedIDNumber = e.NewTextValue;

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
            
            if (!string.IsNullOrEmpty(EntryIDNumber.Text))
            {

                if (_viewModel.SelectedIDType == "National ID Number"||_viewModel.SelectedIDType== "رقم الهوية الوطنية")                {
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
                        // PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZNationalIDstartswith1));
                        EntryIDNumber.Text = string.Empty;
                        _viewModel.SelectedIDNumber = string.Empty;
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
                            // PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));
                            EntryIDNumber.Text = string.Empty;
                            _viewModel.SelectedIDNumber = string.Empty;
                        }

                    }
                }
                else if (_viewModel.SelectedIDType == "Iqama Number"||_viewModel.SelectedIDType== "رقم الإقامة")
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
                        //  PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZIqamaIDstartswith2));
                        EntryIDNumber.Text = string.Empty;
                        _viewModel.SelectedIDNumber = string.Empty;
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
                            // PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));

                            EntryIDNumber.Text = string.Empty;
                            _viewModel.SelectedIDNumber = string.Empty;
                        }

                    }
                }
                else if (_viewModel.SelectedIDType == "GCC ID"||_viewModel.SelectedIDType== "رقم هوية مواطني دول الخليج")
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
                        //  PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGCCIDdonotstartwith0));
                        EntryIDNumber.Text = string.Empty;
                        _viewModel.SelectedIDNumber = string.Empty;
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
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit));
                        EntryIDNumber.Text = string.Empty;
                        _viewModel.SelectedIDNumber = string.Empty;
                        // EntryIDNumber.Text = string.Empty;//ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit
                    }

                }
                else if (_viewModel.SelectedIDType == "Company ID"||_viewModel.SelectedIDType== "معرف الشركة")
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
                        //  PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.IBanCompanyIdStartswith7));
                        EntryIDNumber.Text = string.Empty;
                        _viewModel.SelectedIDNumber = string.Empty;
                    }
                    else if ((EntryIDNumber.Text.Length <= 9))
                    {
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
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.IBanCompanyIdStartswith7));
                        EntryIDNumber.Text = string.Empty;
                        _viewModel.SelectedIDNumber = string.Empty;
                        // EntryIDNumber.Text = string.Empty;//ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit
                    }
                }
                else if (_viewModel.SelectedIDType == "Commercial Register Number"||(_viewModel.SelectedIDType== "رقم السجل التجاري"))
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
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.IBanCommercialIdShouldbe10));
                        EntryIDNumber.Text = string.Empty;
                        _viewModel.SelectedIDNumber = string.Empty;
                        // EntryIDNumber.Text = string.Empty;//ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit
                    }
                }
            }

        }
    }
}
