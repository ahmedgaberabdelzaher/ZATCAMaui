using System;
using System.Collections.Generic;
using System.Linq;
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

                    if (arg.PickerId == "IBANIdTypePicker")
                    {
                        _viewModel.SelectedIDType = arg.SelectedValue;
                        _viewModel.SelectedIDNumber = "";
                    }
                    else if (arg.PickerId == "IBANIdNumberPicker")
                    {
                        _viewModel.SelectedIDNumber = arg.SelectedValue;
                    }
                    else if (arg.PickerId == "IBANBankNamePicker")
                    {
                        _viewModel.SelectedBankName = arg.SelectedValue;
                        if (arg.SelectedValue == "OTHER BANK NAME")
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
                    var response = WebServiceManager.GAZTCheckIBAN(_viewModel.IBANValue);
                    if (response != null)
                    {
                        //IBan is Valid
                        _viewModel.isIBanValid = true;
                        _viewModel.IsLoading = false;
                        string bankName = string.Empty;
                        bankName = JObject.Parse(App.IBanValidatedResponse)["d"].ToString();
                        _viewModel.SelectedBankName = JObject.Parse(bankName)["Bkext"].ToString();
                        App.IBanValidatedResponse = string.Empty;
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
    }
}
