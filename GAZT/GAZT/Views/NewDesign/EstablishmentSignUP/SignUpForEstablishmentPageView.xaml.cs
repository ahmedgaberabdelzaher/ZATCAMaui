using EGAZT.Models;
using EGAZT.Models.EnumModels;
using EGAZT.ViewModel.NewDesignViewModel.EstablishmentSignUPVM;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using EGAZT.Views.SyncFusionEnabledViews.CorrespondenceDetails;
using EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfPicker.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.EstablishmentSignUP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpForEstablishmentPageView : ContentPage
    {
        SignUpForEstablishmentPageViewModel viewModel;
        ObservableCollection<InternationalMobileData> mobileData = null;

        public SignUpForEstablishmentPageView()
        {
            InitializeComponent();
            BindingContext = viewModel;

            viewModel = App.Locator.SignUpForEstablishmentPageView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            viewModel.IsAllValidDataEntered = false;
            viewModel.IsAllValidCRNumberEntered = false;
            viewModel.IsDeclarationCheckedForInstruction = false;
            ChangeAeroIcon();
            SetLTR();

            loadPageData();
            viewModel.TxtLOrCIssuedBy = string.Empty;
            viewModel.TxtCountryCode = "+966";
            if (Device.RuntimePlatform == Device.Android)
            {
                IntnlCodes.Margin = new Thickness(0);
            }
            else
            {
                IntnlCodes.Margin = new Thickness(12, -12, 12, -12);
            }
            ClearFields();

            if (Device.RuntimePlatform == Device.iOS)
            {
                string baseUrl = DependencyService.Get<IBaseUrl>().Get();
                string path = DependencyService.Get<IBaseUrl>().Get();
                if (!App.IsArabic)
                {
                    string url = Path.Combine(path, "TermsAndConditionsEN.html");
                   // TCWebView.Source = url;
                }
                else
                {
                    string url = Path.Combine(path, "TermsAndConditionsAR.html");
                    //TCWebView.Source = url;
                }
            }
            else
            {
                if (!App.IsArabic)
                {
                 //   TCWebView.Source = "file:///android_asset/TermsAndConditionsEN.html";
                }
                else
                {
                  //  TCWebView.Source = "file:///android_asset/TermsAndConditionsAR.html";
                }
            }


        }
        private void Closed_Tapped(object sender, EventArgs e)
        {
            // PopupNavigation.Instance.PopAsync();
            viewModel._navigationService.GoBack();
        }

        public async Task loadPageData()
        {

            await viewModel.SetDefaultDate();
            ClearFields();
            await viewModel.OnPageLoad();
            await viewModel.SetIssueIdList();
            await viewModel.SetCityList();


        }

        private void LIssuedBy_Clicked(object sender, EventArgs e)
        {
            ddlLIssuedBy.IsOpen = true;
        }

        private void ddlLIssuedBy_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            if ((IssuedByResponse)e.NewValue != null)
            {
                IssuedByResponse issuedByResponse = (IssuedByResponse)e.NewValue;
                ddlLIssuedBy.SelectedItem = issuedByResponse;
                viewModel.SelectedIssuedBy = issuedByResponse;
                viewModel.SelectedIssuedByPrev = issuedByResponse;
                viewModel.TxtLOrCIssuedBy = issuedByResponse.txt50;
                viewModel.IssuedByTapped = true;
            }
        }
        private void ddlLIssuedBy_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            viewModel.SelectedIssuedBy = viewModel.SelectedIssuedByPrev;
            ddlLIssuedBy.SelectedItem = viewModel.SelectedIssuedByPrev;
            if (viewModel.SelectedIssuedByPrev == null)
            {
                viewModel.TxtLOrCIssuedBy = string.Empty;
            }

        }
        private void ddlLIssuedByCity_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            viewModel.SelectCityList = viewModel.SelectCityListPrev;
            ddlLIssuedByCity.SelectedItem = viewModel.SelectCityListPrev;
            if (viewModel.SelectCityListPrev == null)
            {
                viewModel.TxtLOrCIssuedByCity = string.Empty;
            }

        }
        private void LIssuedByCity_Clicked(object sender, EventArgs e)
        {
            ddlLIssuedByCity.IsOpen = true;
        }
        public void ClearFields()
        {
            //viewModel.PkrDBO = string.Empty;
            viewModel.TxtLOrCIssuedBy = string.Empty;
            viewModel.TxtLOrCIssuedByCity = string.Empty;
            viewModel.IDTypeIndex = 0;
            //viewModel.SelectedLOrC = 1;
            //viewModel.IsLoading = false;
            viewModel.SelectedSignUpUsing = null;
            viewModel.SignUpUsingList = null;
            //viewModel.SelectLCType = null;
            //viewModel.LcTypeList = null;
            //viewModel.SelectCityList = null;
            //viewModel.CityList = null;
            //viewModel.IsCRVisible = true;
            //viewModel.IsLicenseVisible = false;
            // viewModel.IsTIN = false;
            // viewModel.IsTINVisible = false;
            //viewModel.SelectedIssuedBy = null;
            viewModel.IssuedByList = null;
            viewModel.TxtTIN = string.Empty;
            viewModel.TxtIDNumber = string.Empty;
            viewModel.TxtName = string.Empty;
            viewModel.TxtCRNumber = string.Empty;
            viewModel.TxtLicenseNumber = string.Empty;
            viewModel.TxtEmailAddress = string.Empty;
            viewModel.TxtCountryCode = string.Empty;

            viewModel.TxtMobileNumber = string.Empty;
            viewModel.TxtPhoneNumber = string.Empty;

            //iewModel.IDTypeModelRootObject = null;
            viewModel.SignUpFirstSubmitModel = null;
            //iewModel.MaximumxD = DateTime.Now;

        }
        private void SetLTR()
        {
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                PickerResourceManager.Manager = new ResourceManager("EGAZT.SyncfusionControl", Xamarin.Forms.Application.Current.GetType().Assembly);
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
                CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                PickerResourceManager.Manager = new ResourceManager("GAZT.AppResources", Xamarin.Forms.Application.Current.GetType().Assembly);
            }
        }
        public void ChangeAeroIcon()
        {
            if (!App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
                Image_backArrow.Rotation = 0;
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
                Image_backArrow.Rotation = 180;
            }
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<InternationalCodeSearchPage, string>(this, "SelectedItem", (sender, arg) =>
            {
                // IntnlCodes.Text = arg;
                viewModel.TxtCountryCode = arg;
            });
            MessagingCenter.Subscribe<InternationalCodeSearchPage, string>(this, "SelectedCountryCode", (sender, arg) =>
            {

                viewModel.MobileCountryCode = arg;
            });
            if (Device.RuntimePlatform == Device.Android)
            {
                // IntnlCodes.Margin = new Thickness(0);
            }
            else
            {
                // IntnlCodes.Margin = new Thickness(12, -12, 12, -12);
            }

            try
            {
                mobileData = WebServiceManager.GAZTGetMobileRegionDropdown();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        //email otp text changed events

        private void OTPFirstEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.OTPFirstDigit.Length > 0)
            {
                OTPSecondEntry.Focus();
            }
        }

        private void OTPSecondEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.OTPSecondDigit.Length > 0)
            {
                OTPThirdEntry.Focus();
            }
        }

        private void OTPThirdEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.OTPThirdDigit.Length > 0)
            {
                OTPFourthEntry.Focus();
            }
        }

        private void OTPFourthEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.OTPFourthDigit.Length > 0)
            {
                //if (!viewmodel.isresendotpenabled)
                //{
                //    // viewmodel.verifyotp();
                //}

            }
        }

        //mobile OTP entry text changed events
        private void MobOTPFirstEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.MOTPFirstDigit.Length > 0)
            {
                MobOTPSecondEntry.Focus();
            }
        }

        private void MobOTPSecondEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.MOTPSecondDigit.Length > 0)
            {
                MobOTPThirdEntry.Focus();
            }
        }

        private void MobOTPThirdEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.MOTPThirdDigit.Length > 0)
            {
                MobOTPFourthEntry.Focus();
            }
        }

        private void MobOTPFourthEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.MOTPFourthDigit.Length > 0)
            {
                //if (!viewModel.IsResendOTPEnabled)
                //{
                //    // viewModel.VerifyMOTP();
                //}

            }
        }

        private void OnNewPasswordTapped(object sender, EventArgs e)
        {

        }

        private void NewPassword_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OnConfirmNewPasswordTapped(object sender, EventArgs e)
        {

        }

        private void OnDOBClicked(object sender, EventArgs e)
        {
            DpDbo.IsOpen = true;
        }

        private void CountryCodeTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new InternationalCodeSearchPage(mobileData));
        }

        public bool ValidateCRNumber()
        {
            if (!string.IsNullOrEmpty(EntryCRNumber.Text))
            {
                if (EntryCRNumber.Text.Length == 10)
                {
                    try
                    {
                         FrmCR.HasError = false;
                        CRValidationModelRootObject Result = WebServiceManager.GAZTValidateCRNumber(EntryCRNumber.Text);
                        if (Result != null)
                        {
                            if (Result.d != null)
                            {
                                if (Result.d.NotFound == "X")
                                {
                                    FrmCR.HasError = true;
                                    viewModel._dialogService.ShowMessage(AppResources.ZZPleaseentervalidCRnumber, AppResources.Information);

                                    return false;
                                }
                                else
                                {
                                    viewModel.IsAllValidCRNumberEntered = true;
                                    FrmCR.HasError = false;
                                    return true;
                                }

                            }
                            else
                            {
                                return false;
                            }

                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (InternetException ex)
                    {

                        viewModel._dialogService.ShowMessage(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                        return false;
                    }
                }
                else
                {
                    PopUp popUp = new PopUp();
                    popUp.Message = AppResources.ZZCommercialReiterationNumbershouddbe10digits;
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
                    FrmCR.HasError = true;
                    EntryCRNumber.Text = string.Empty;

                    EntryCRNumber.Focus();
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            viewModel.StopTimer = false;

            //for (int index = Navigation.NavigationStack.Count - 2; index > 1; index--)
            //{
            //    Page pg = Navigation.NavigationStack[index];
            //    Navigation.RemovePage(pg);
            //}
        }

        private void EntryCRNumber_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryCRNumber.Text))
            {
                if (EntryCRNumber.Text.Length == 10)
                {
                    try
                    {
                        FrmCR.HasError = false;
                        CRValidationModelRootObject Result = WebServiceManager.GAZTValidateCRNumber(EntryCRNumber.Text);
                        if (Result != null)
                        {
                            if (Result.d != null)
                            {
                                if (Result.d.NotFound == "X")
                                {
                                    FrmCR.HasError = true;
                                    viewModel._dialogService.ShowMessage(AppResources.ZZPleaseentervalidCRnumber, AppResources.Information);
                                }
                                else
                                {
                                    FrmCR.HasError = false;
                                    viewModel.IsAllValidCRNumberEntered = true;
                                }
                            }
                        }
                    }
                    catch (InternetException ex)
                    {
                        viewModel._dialogService.ShowMessage(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                    }
                }
                else
                {
                    PopUp popUp = new PopUp();
                    popUp.Message = AppResources.ZZCommercialReiterationNumbershouddbe10digits;
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
                    FrmCR.HasError = true;
                    EntryCRNumber.Text = string.Empty;
                    EntryCRNumber.Focus();
                }
            }
        }

        private void EntryEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryEmail.Text))
            {
                bool flag = IsValid(EntryEmail.Text);
                if (!flag)
                {
                    PopUp popUp = new PopUp();
                    popUp.Message = AppResources.ZZPleaseenteravalidEmailAddress;//ZZPleaseenteravalidEmailAddress//ZZEmailAddressdoesnotmatchwithvalueinMinistryofCommerce;//ZZZInvalidEmailAddressMessage
                    popUp.IsLinkAvailable = false;
                    if (App.IsArabic)
                    {
                        popUp.FlowDirections = "RightToLeft";
                        // popUp.isFontSet = true;
                    }
                    else
                    {
                        popUp.FlowDirections = "LeftToRight";
                    }
                    PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                    FrmEmailAddress.HasError = true;
                    EntryEmail.Text = string.Empty;
                    viewModel.IsAllValidContactDataEnteredEmail = false;
                }
                else
                {
                    viewModel.IsAllValidContactDataEnteredEmail = true;
                     FrmEmailAddress.HasError = false;
                }
            }
        }

        private void EntryPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryPhoneNumber.Text))
            {
                if (EntryPhoneNumber.Text.Substring(0, 1) != "1")
                {
                    viewModel.IsAllValidContactDataEnteredPhoneNbr = false;
                     FrmPhoneNumber.HasError = true;
                }
                else
                {
                    viewModel.IsAllValidContactDataEnteredPhoneNbr = true;
                     FrmPhoneNumber.HasError = false;
                }
            }
        }

        private void EntryPhoneNumber_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(EntryPhoneNumber.Text))
            {

                 FrmPhoneNumber.HasError = false;
            }
            if (!string.IsNullOrEmpty(EntryPhoneNumber.Text))
            {
                PopUp popUp = new PopUp();
                StringBuilder Message = new StringBuilder();
                if (EntryPhoneNumber.Text.Substring(0, 1) != "1")
                {
                    Message.Append(AppResources.ZZPhonenumberhastostartwithnumber1);
                }
                if (EntryPhoneNumber.Text.Length != 9)
                {
                    if (Message.Length > 0)
                    {
                        Message.Append(Environment.NewLine);
                    }
                    Message.Append(AppResources.ZZPhonenumberlengthcannotbelessthan9digits);
                }
                if (Message.Length > 0)
                {
                    popUp.Message = Message.ToString();
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
                    FrmPhoneNumber.HasError = true;

                    EntryPhoneNumber.Text = string.Empty;
                    EntryPhoneNumber.Focus();
                }
                else
                {
                    viewModel.IsAllValidContactDataEnteredPhoneNbr = true;
                    //  viewModel.IsAllValidContactDataEntered = true;
                     FrmPhoneNumber.HasError = false;
                }
            }
        }
        private void EntryMobileNumber_Unfocused(object sender, FocusEventArgs e)
        {
            StringBuilder Message = new StringBuilder();
            PopUp popUp = new PopUp();
            if (!string.IsNullOrEmpty(EntryMobileNumber.Text))
            {

                if (EntryMobileNumber.Text.Substring(0, 1) == "0")
                {
                    Message.Append(AppResources.ZZMobilenumberCannotStartWith0);
                }
                if (EntryMobileNumber.Text.Length < 9)
                {
                    if (Message.Length > 0)
                    {
                        Message.Append(Environment.NewLine);
                    }

                    Message.Append(AppResources.ZZMobilenumberlengthcannotbelessthan9digits);
                }
                if (Message.Length > 0)
                {
                    popUp.Message = Message.ToString();
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
                    FrmMobileNumber.HasError = true;
                    viewModel.IsAllValidContactDataEnteredMobileNbr = false;

                    EntryMobileNumber.Text = string.Empty;
                }
                else
                {
                    viewModel.IsAllValidContactDataEnteredMobileNbr = true;
                     FrmMobileNumber.HasError = false;
                }
            }
            else
            {
                Message.Append(AppResources.EnterMobileNumber);
                popUp.Message = Message.ToString();
                PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
            }
        }

        public bool IsValid(string emailaddress)
        {
            bool isEmail = Regex.IsMatch(emailaddress, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (isEmail)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void OnIDTypeClicked(object sender, EventArgs e)
        {
            IDTypePicker.IsOpen = true;
        }

        private void IDTypePicker_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            SignUpUsing signUpUsing = (SignUpUsing)e.NewValue;
            viewModel.SelectedSignUpUsing = signUpUsing;
            viewModel.TxtIDType = signUpUsing.SUType;
            viewModel.TxtIDNumber = string.Empty;
            EntryIDNumber.Focus();
            IDTypePicker.IsOpen = false;

        }

        private void EntryIDNumber_Unfocused(object sender, FocusEventArgs e)
        {
            PopUp popUp = new PopUp();
            StringBuilder Messages = new StringBuilder();
            if (!string.IsNullOrEmpty(EntryIDNumber.Text))
            {
                if (viewModel.SelectedSignUpUsing != null)
                {
                    if (viewModel.SelectedSignUpUsing.ID == 1)
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
                            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                             FrmIDNumber.HasError = true;
                            viewModel.IsAllValidDataEntered = false;
                            EntryName.Text = string.Empty;
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
                                PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                                 FrmIDNumber.HasError = true;
                                viewModel.IsAllValidDataEntered = false;
                                EntryName.Text = string.Empty;
                            }
                            else
                            {
                                viewModel.IsAllValidDataEntered = true;
                                 FrmIDNumber.HasError = false;
                                ValidateIDNumber();
                            }
                        }
                    }
                    else if (viewModel.SelectedSignUpUsing.ID == 2)
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
                            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                             FrmIDNumber.HasError = true;
                            viewModel.IsAllValidDataEntered = false;
                            EntryName.Text = string.Empty;
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
                                PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                                 FrmIDNumber.HasError = true;
                                viewModel.IsAllValidDataEntered = false;
                                EntryName.Text = string.Empty;
                            }
                            else
                            {
                                // viewModel.IsAllValidDataEntered = true;
                                 FrmIDNumber.HasError = false;
                                ValidateIDNumber();
                            }
                        }
                    }
                    else if (viewModel.SelectedSignUpUsing.ID == 3)
                    {
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
                            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                              FrmIDNumber.HasError = true;
                            viewModel.IsAllValidDataEntered = false;
                            EntryName.Text = string.Empty;
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
                            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                              FrmIDNumber.HasError = true;
                            viewModel.IsAllValidDataEntered = false;
                            EntryName.Text = string.Empty;
                            // EntryIDNumber.Text = string.Empty;//ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit
                        }
                    }
                    else
                    {
                        ValidateIDNumber();
                         viewModel.IsAllValidDataEntered = true;
                         FrmIDNumber.HasError = false;
                    }
                }
            }
            else
            {
                viewModel.IsAllValidDataEntered = false;
                 FrmIDNumber.HasError = false;
            }
        }
        public void OnDateEntryFocussed(object sender, EventArgs args)
        {
            DpDbo.IsOpen = true;
        }
        private void DOBpicker_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            ValidateIDNumber();
        }
        private void DatePicker_Unfocused(object sender, FocusEventArgs e)
        {
            ValidateIDNumber();
        }
        public async void ValidateIDNumber()
        {
           // viewModel.IsAllValidDataEntered = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });
            });
            var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;
            string month = selectedItem[1].ToString();
            string day = selectedItem[0].ToString();
            string year = selectedItem[2].ToString();
            viewModel.PkrDBO = year + "/" + month + "/" + day;
            string DBO = year + month + day;
            viewModel.PkrDBOPrev = viewModel.PkrDBO;
            //string DBO = Convert.ToDateTime(DpDbo.Date.ToString().Split(' ')[0]).ToString("yyyyMMdd", new CultureInfo("en-US"));
            EntryName.IsEnabled = true;
            if (viewModel.SelectedSignUpUsing.ID == 1)
            {
                if (!string.IsNullOrEmpty(viewModel.TxtIDNumber))
                {
                    try

                    {
                        string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0001", viewModel.TxtIDNumber, DBO);
                        IDTypeModelRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeModelRootObject>(Result);
                        if (SignupIsIDTypeValid.d == null)
                        {
                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                viewModel.IsAllValidDataEntered = false;
                                FrmIDNumber.HasError = true;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                viewModel.IsAllValidDataEntered = false;
                                 FrmIDNumber.HasError = false;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                        }
                        else
                        {
                            viewModel.TxtName = SignupIsIDTypeValid.d.Name1 + " " + SignupIsIDTypeValid.d.Name2;
                            EntryName.IsEnabled = false;
                            viewModel.IsAllValidDataEntered = true;
                            FrmIDNumber.HasError = false;
                        }
                    }
                    catch
                    {
                        try
                        {
                            string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0001", viewModel.TxtIDNumber, DBO);
                            IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            {
                                viewModel.IsAllValidDataEntered = false;
                                 FrmIDNumber.HasError = true;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                viewModel.IsAllValidDataEntered = false;
                                FrmIDNumber.HasError = false;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                        }
                        catch (GAZTException gex)
                        {
                            viewModel.IsAllValidDataEntered = false;
                            // Handle the GAZT custom exception.
                            string MessageForTheUser = gex.Message;
                            if (gex is GAZTInvalidDataException)
                            {
                                MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            }
                            if (gex is GAZTNetworkConnectivityIssueException)
                            {
                                MessageForTheUser = AppResources.NetworkConnectivityIssue;
                            }
                            else if (gex is GAZTInternetException)
                            {
                                MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                            }
                            else if (gex is GAZTSessionExpiredException)
                            {
                                MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                            }

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;

                                await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                //viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                await Task.Run(() =>
                                {
                                    viewModel.IsLoading = false;
                                });
                            });
                        }
                        catch (HttpRequestException ex)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                //_navigationService.GoBack();
                            });
                        }
                        catch (Exception ex)
                        {

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                //_navigationService.GoBack();
                            });
                        }
                    }
                }
            }
            if (viewModel.SelectedSignUpUsing.ID == 2)
            {
                EntryName.IsEnabled = true;
                if (!string.IsNullOrEmpty(viewModel.TxtIDNumber))
                {
                    try
                    {
                        string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0002", viewModel.TxtIDNumber, DBO);
                        IDTypeModelRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeModelRootObject>(Result);
                        if (SignupIsIDTypeValid.d == null)
                        {
                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                viewModel.IsAllValidDataEntered = false;
                                FrmIDNumber.HasError = true;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                viewModel.IsAllValidDataEntered = false;
                                FrmIDNumber.HasError = false;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                        }
                        else
                        {
                            viewModel.IsAllValidDataEntered = true;
                            viewModel.TxtName = SignupIsIDTypeValid.d.Name1 + " " + SignupIsIDTypeValid.d.Name2;
                            EntryName.IsEnabled = false;
                            FrmIDNumber.HasError = false;
                        }
                    }
                    catch
                    {
                        try
                        {
                            string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0002", viewModel.TxtIDNumber, DBO);
                            IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            {
                                viewModel.IsAllValidDataEntered = false;
                                FrmIDNumber.HasError = true;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                viewModel.IsAllValidDataEntered = false;
                                FrmIDNumber.HasError = false;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                        }
                        catch (GAZTException gex)
                        {
                            // Handle the GAZT custom exception.
                            string MessageForTheUser = gex.Message;
                            if (gex is GAZTInvalidDataException)
                            {
                                MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            }
                            if (gex is GAZTNetworkConnectivityIssueException)
                            {
                                MessageForTheUser = AppResources.NetworkConnectivityIssue;
                            }
                            else if (gex is GAZTInternetException)
                            {
                                MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                            }
                            else if (gex is GAZTSessionExpiredException)
                            {
                                MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                            }

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;

                                await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                // viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                await Task.Run(() =>
                                {
                                    viewModel.IsLoading = false;
                                });
                            });
                        }
                        catch (HttpRequestException ex)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                //_navigationService.GoBack();
                            });
                        }
                        catch (Exception ex)
                        {

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                //_navigationService.GoBack();
                            });
                        }
                    }
                }
            }
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            });
        }
        private void DpDbo_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            viewModel.PkrDBO = viewModel.PkrDBOPrev;
            if (!string.IsNullOrEmpty(viewModel.PkrDBOPrev))
            {
                string[] Date = viewModel.PkrDBOPrev.Split('/');
                ObservableCollection<object> todaycollection = new ObservableCollection<object>();
                //Select today dates
                todaycollection.Add(Date[2]);
                todaycollection.Add(Date[1]);//day
                todaycollection.Add(Date[0]);

                DpDbo.SelectedItem = todaycollection;
            }
        }



        private void ddlLIssuedByCity_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            if ((SignupCityResult)e.NewValue != null)
            {
                SignupCityResult selectedcity = (SignupCityResult)e.NewValue;
                ddlLIssuedByCity.SelectedItem = selectedcity;
                viewModel.SelectCityList = selectedcity;
                viewModel.SelectCityListPrev = selectedcity;
                viewModel.TxtLOrCIssuedByCity = selectedcity.CityName;
                viewModel.IssuedByCityTapped = true;
            }
        }
        private void DpDbo_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            // FrmDBO.HasError = false;
            try
            {
                if (DpDbo.SelectedItem != null)
                {
                    var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;
                    string month = selectedItem[1].ToString();
                    string day = selectedItem[0].ToString();
                    string year = selectedItem[2].ToString();
                    viewModel.PkrDBO = year + "/" + month + "/" + day;
                }

            }
            catch (Exception ex)
            {
            }
        }


        private void DpDbo_Closed(object sender, EventArgs e)
        {

        }
        private async void GAZTBorderlessEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryIDNumber.Text))
            {
                try
                {
                    //EntryIDNumber.IsEnabled = true; //commented because bydefault it was coming red border 
                    var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;
                    // DateTime selecteDate = Convert.ToDateTime(selectedItem);
                    if (selectedItem != null && selectedItem[0] != null)
                    {
                        // int a = DateTime.Compare(viewModel.TodayDate, selecteDate);
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
                        string _month = DateTime.Now.Month.ToString();
                        string _day = DateTime.Now.Day.ToString();
                        string _year = DateTime.Now.Year.ToString();
                        string DBO = year + month + day;
                        //string DBO = Convert.ToDateTime(DpDbo.Date.ToString().Split(' ')[0]).ToString("yyyyMMdd", new CultureInfo("en-US"));
                        if (!string.IsNullOrEmpty(EntryIDNumber.Text))
                        {
                            if (viewModel.SelectedSignUpUsing.ID == 1)
                            {
                                if (EntryIDNumber.Text.Substring(0, 1) != "1")
                                {
                                    viewModel.IsAllValidDataEntered = false;
                                     FrmIDNumber.HasError = true;
                                    EntryName.Text = string.Empty;
                                }
                                else
                                {
                                    viewModel.IsAllValidDataEntered = true;
                                     FrmIDNumber.HasError = false;
                                    if (EntryIDNumber.Text.Length == 10)
                                    {
                                        try
                                        {
                                            string Result = string.Empty;
                                            if (!(Convert.ToUInt16(_month) == Convert.ToUInt16(viewModel.DefaultMonth) && Convert.ToUInt16(_day) == Convert.ToUInt16(day) && Convert.ToUInt16(year) == Convert.ToUInt16(_year)))
                                            {
                                                Result = await WebServiceManager.GAZTValidateIDTypes("ZS0001", viewModel.TxtIDNumber, DBO);
                                                IDTypeModelRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeModelRootObject>(Result);
                                                if (SignupIsIDTypeValid.d == null)
                                                {
                                                    IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                                                    if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                                                    {
                                                        viewModel.IsAllValidDataEntered = false;
                                                         FrmIDNumber.HasError = true;
                                                        EntryName.Text = string.Empty;
                                                        viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                                        //  viewModel.TxtIDNumber = string.Empty;
                                                    }
                                                    else
                                                    {
                                                        viewModel.IsAllValidDataEntered = false;
                                                         FrmIDNumber.HasError = false;
                                                        viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                                    }
                                                }
                                                else
                                                {
                                                    viewModel.TxtName = SignupIsIDTypeValid.d.Name1 + " " + SignupIsIDTypeValid.d.Name2;
                                                     FrmIDNumber.HasError = false;
                                                    viewModel.IsAllValidDataEntered = true;
                                                    EntryName.IsEnabled = false;
                                                }
                                            }
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                string Result = string.Empty;
                                                if (!(Convert.ToUInt16(_month) == Convert.ToUInt16(viewModel.DefaultMonth) && Convert.ToUInt16(_day) == Convert.ToUInt16(day) && Convert.ToUInt16(year) == Convert.ToUInt16(_year)))
                                                {
                                                    Result = await WebServiceManager.GAZTValidateIDTypes("ZS0001", viewModel.TxtIDNumber, DBO);
                                                    IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                                                    if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                                                    {
                                                         FrmIDNumber.HasError = true;
                                                        viewModel.IsAllValidDataEntered = false;
                                                        EntryName.Text = string.Empty;
                                                        viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                                        // viewModel.TxtIDNumber = string.Empty;
                                                    }
                                                    else
                                                    {
                                                         FrmIDNumber.HasError = false;
                                                        viewModel.IsAllValidDataEntered = false;
                                                        viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                                    }
                                                }
                                            }
                                            catch (GAZTException gex)
                                            {
                                                // Handle the GAZT custom exception.
                                                string MessageForTheUser = gex.Message;
                                                if (gex is GAZTInvalidDataException)
                                                {
                                                    MessageForTheUser = AppResources.ZZSomethingwentwrong;
                                                }
                                                if (gex is GAZTNetworkConnectivityIssueException)
                                                {
                                                    MessageForTheUser = AppResources.NetworkConnectivityIssue;
                                                }
                                                else if (gex is GAZTInternetException)
                                                {
                                                    MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                                                }
                                                else if (gex is GAZTSessionExpiredException)
                                                {
                                                    MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                                                }

                                                Device.BeginInvokeOnMainThread(async () =>
                                                {
                                                    viewModel.IsLoading = false;

                                                    await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                                    //viewModel._navigationService.GoBack();
                                                });
                                            }
                                            catch (InternetException ex)
                                            {
                                                Device.BeginInvokeOnMainThread(async () =>
                                                {
                                                    viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                                });
                                            }
                                            catch (HttpRequestException ex)
                                            {
                                                string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                                                Device.BeginInvokeOnMainThread(async () =>
                                                {
                                                    // IsLoading = false;

                                                    await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                                    //_navigationService.GoBack();
                                                });
                                            }
                                            catch (Exception ex)
                                            {

                                                string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                                                Device.BeginInvokeOnMainThread(async () =>
                                                {
                                                    // IsLoading = false;

                                                    await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                                    //_navigationService.GoBack();
                                                });
                                            }
                                        }
                                    }
                                }
                            }
                            else if (viewModel.SelectedSignUpUsing.ID == 2)
                            {
                                if (EntryIDNumber.Text.Substring(0, 1) != "2")
                                {
                                    viewModel.IsAllValidDataEntered = false;
                                     FrmIDNumber.HasError = true;
                                    EntryName.Text = string.Empty;
                                }
                                else
                                {
                                     FrmIDNumber.HasError = false;
                                    viewModel.IsAllValidDataEntered = true;
                                    if (EntryIDNumber.Text.Length == 10)
                                    {
                                        try
                                        {
                                            string Result = string.Empty; ;
                                            if (!(Convert.ToUInt16(_month) == Convert.ToUInt16(viewModel.DefaultMonth) && Convert.ToUInt16(_day) == Convert.ToUInt16(day) && Convert.ToUInt16(year) == Convert.ToUInt16(_year)))
                                            {
                                                Result = await WebServiceManager.GAZTValidateIDTypes("ZS0002", viewModel.TxtIDNumber, DBO);
                                                IDTypeModelRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeModelRootObject>(Result);
                                                if (SignupIsIDTypeValid.d == null)
                                                {
                                                    IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                                                    if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                                                    {
                                                         FrmIDNumber.HasError = true;
                                                        viewModel.IsAllValidDataEntered = false;
                                                        EntryName.Text = string.Empty;
                                                        viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                                        //  viewModel.TxtIDNumber = string.Empty;
                                                    }
                                                    else
                                                    {
                                                        viewModel.IsAllValidDataEntered = false;
                                                         FrmIDNumber.HasError = false;
                                                        viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                                    }
                                                }
                                                else
                                                {
                                                    viewModel.TxtName = SignupIsIDTypeValid.d.Name1 + " " + SignupIsIDTypeValid.d.Name2;
                                                    EntryName.IsEnabled = false;
                                                    viewModel.IsAllValidDataEntered = true;
                                                    FrmIDNumber.HasError = false;
                                                }
                                            }
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                string Result = string.Empty;
                                                if (!(Convert.ToUInt16(_month) == Convert.ToUInt16(viewModel.DefaultMonth) && Convert.ToUInt16(_day) == Convert.ToUInt16(day) && Convert.ToUInt16(year) == Convert.ToUInt16(_year)))
                                                {
                                                    Result = await WebServiceManager.GAZTValidateIDTypes("ZS0002", viewModel.TxtIDNumber, DBO);
                                                    IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                                                    if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                                                    {
                                                         FrmIDNumber.HasError = true;
                                                        EntryName.Text = string.Empty;
                                                        viewModel.IsAllValidDataEntered = false;
                                                        viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                                        // viewModel.TxtIDNumber = string.Empty;
                                                    }
                                                    else
                                                    {
                                                         FrmIDNumber.HasError = false;
                                                        viewModel.IsAllValidDataEntered = false;
                                                        viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                                    }
                                                }
                                            }
                                            catch (GAZTException gex)
                                            {
                                                // Handle the GAZT custom exception.
                                                string MessageForTheUser = gex.Message;
                                                if (gex is GAZTInvalidDataException)
                                                {
                                                    MessageForTheUser = AppResources.ZZSomethingwentwrong;
                                                }
                                                if (gex is GAZTNetworkConnectivityIssueException)
                                                {
                                                    MessageForTheUser = AppResources.NetworkConnectivityIssue;
                                                }
                                                else if (gex is GAZTInternetException)
                                                {
                                                    MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                                                }
                                                else if (gex is GAZTSessionExpiredException)
                                                {
                                                    MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                                                }

                                                Device.BeginInvokeOnMainThread(async () =>
                                                {
                                                    viewModel.IsLoading = false;

                                                    await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                                    // viewModel._navigationService.GoBack();
                                                });
                                            }
                                            catch (InternetException ex)
                                            {
                                                Device.BeginInvokeOnMainThread(async () =>
                                                {
                                                    viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                                });
                                            }
                                            catch (HttpRequestException ex)
                                            {
                                                string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                                                Device.BeginInvokeOnMainThread(async () =>
                                                {
                                                    // IsLoading = false;

                                                    await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                                    //_navigationService.GoBack();
                                                });
                                            }
                                            catch (Exception ex)
                                            {

                                                string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                                                Device.BeginInvokeOnMainThread(async () =>
                                                {
                                                    // IsLoading = false;

                                                    await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                                    //_navigationService.GoBack();
                                                });
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                 FrmIDNumber.HasError = false;
                            }
                        }
                    }
                }
                catch (GAZTException gex)
                {
                    // Handle the GAZT custom exception.
                    string MessageForTheUser = gex.Message;
                    if (gex is GAZTInvalidDataException)
                    {
                        MessageForTheUser = AppResources.ZZSomethingwentwrong;
                    }
                    if (gex is GAZTNetworkConnectivityIssueException)
                    {
                        MessageForTheUser = AppResources.NetworkConnectivityIssue;
                    }
                    else if (gex is GAZTInternetException)
                    {
                        MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                    }
                    else if (gex is GAZTSessionExpiredException)
                    {
                        MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                    }

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        viewModel.IsLoading = false;

                        await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                        // viewModel._navigationService.GoBack();
                    });
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                    });
                }
                catch (HttpRequestException ex)
                {
                    string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        // IsLoading = false;

                        await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                        //_navigationService.GoBack();
                    });
                }
                catch (Exception ex)
                {

                    string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        // IsLoading = false;

                        await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                        //_navigationService.GoBack();
                    });
                }
            }
        }
        private void OnInCTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new InstructionPopUpPageView());
        }

       
        private void EntryTIN_focused(object sender, FocusEventArgs e)
        {
            PopUp popUp = new PopUp();
            StringBuilder Messages = new StringBuilder();
            if (!string.IsNullOrEmpty(EntryTIN.Text))
            {
                if (EntryTIN.Text.Substring(0, 1) != "3")
                {
                    Messages.Append(AppResources.ZZTINnumberhastostartwithnumber3);
                    EntryTIN.Focus();
                }
                if (EntryTIN.Text.Length != 10)
                {
                    if (Messages.Length > 0)
                    {
                        Messages.Append(Environment.NewLine);
                    }
                    Messages.Append(AppResources.ZZTINnumberlengthcannotbelessthan10digits);
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
                    viewModel.IsAllValidDataEntered = false;
                     FrmTIN.HasError = true;
                    EntryTIN.Text = string.Empty;
                }
                else
                {
                    // ValidateIDNumber();
                    // viewModel.IsAllValidDataEntered = true;

                     FrmTIN.HasError = false;
                }
            }
            //eMPTY tin
            else
            {

                viewModel.IsAllValidDataEntered = false;
                FrmTIN.HasError = true;
                EntryTIN.Text = string.Empty;
            }
        }


        private void OnYesTapped(object sender, EventArgs e)
        {
            viewModel.ImgBackgroundNo = "re_Property_Tile_Background_White";
            viewModel.ImgBackgroundYes = "re_Tile_Background";
            viewModel.TxtTIN = string.Empty;
            EntryTIN.Focus();
            //EntryTIN.Unfocus();

        }
        private void OnNoTapped(object sender, EventArgs e)
        {
            viewModel.ImgBackgroundNo = "re_Tile_Background";

            viewModel.ImgBackgroundYes = "re_Property_Tile_Background_White";
            EntryTIN.Text = string.Empty;
            viewModel.TxtTIN = string.Empty;
        }

        private void OnCRNumberTapped(object sender, EventArgs e)
        {
            viewModel.ImgBackgroundCRNubmer = "FP_selected_tile";
            viewModel.ImgBackgroundLicenseNubmer = "FP_unselected_tile";
            viewModel.IsCRChecked = true;
        }

        private void OnLicenseNumberTapped(object sender, EventArgs e)
        {
            viewModel.ImgBackgroundCRNubmer = "FP_unselected_tile";
            viewModel.ImgBackgroundLicenseNubmer = "FP_selected_tile";
            viewModel.IsCRChecked = false;

        }
        private void ImageSeeConfirmPassword_Tapped(object sender, EventArgs e)
        {
            viewModel.IsConfirmPasswordEncripted = !viewModel.IsConfirmPasswordEncripted;
            imageConfirmPassword.Source = viewModel.IsConfirmPasswordEncripted ? "hidePassword.png" : "showPassword.png";
        }
        private void ImageSeeNewPassword_Tapped(object sender, EventArgs e)
        {
            viewModel.IsPasswordEncripted = !viewModel.IsPasswordEncripted;
            imageNewPassword.Source = viewModel.IsPasswordEncripted ? "hidePassword.png" : "showPassword.png";
        }

        private void EntryTIN_TextChanged(object sender, TextChangedEventArgs e)
        {
            FrmTIN.HasError = false;
            if (!string.IsNullOrEmpty(EntryTIN.Text))
            {
                if (EntryTIN.Text.Substring(0, 1) != "3")
                {
                    viewModel.IsAllValidDataEntered = false;
                     FrmTIN.HasError = true;
                }
                else
                {
                    ////
                     viewModel.IsAllValidDataEntered = true;
                    FrmTIN.HasError = false;
                }
            }
            else
            {
                /////
                 viewModel.IsAllValidDataEntered = false;
            }
        }



        private void EntryTIN_Unfocused(object sender, FocusEventArgs e)
        {
            PopUp popUp = new PopUp();
            StringBuilder Messages = new StringBuilder();
            if (!string.IsNullOrEmpty(EntryTIN.Text))
            {
                if (EntryTIN.Text.Substring(0, 1) != "3")
                {
                    Messages.Append(AppResources.ZZTINnumberhastostartwithnumber3);
                    EntryTIN.Focus();
                }
                if (EntryTIN.Text.Length != 10)
                {
                    if (Messages.Length > 0)
                    {
                        Messages.Append(Environment.NewLine);
                    }
                    Messages.Append(AppResources.ZZTINnumberlengthcannotbelessthan10digits);
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
                    viewModel.IsAllValidDataEntered = false;
                    FrmTIN.HasError = true;
                     EntryTIN.Text = string.Empty;
                }
                else
                {
                    ValidateIDNumber();
                    // viewModel.IsAllValidDataEntered = true;

                     FrmTIN.HasError = false;
                }
            }
        }

        private void DDlIDType_OkayButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            viewModel.TxtIDNumber = string.Empty;
            EntryName.IsEnabled = true;
            // SfPicker signUpUsing = (SfPicker)sender;
            viewModel.SelectedSignUpUsing = (SignUpUsing)IDTypePicker.SelectedItem;
            viewModel.TxtIDType = viewModel.SelectedSignUpUsing.SUType;
            if (viewModel.SelectedSignUpUsing != null)
            {
                try
                {
                    if (viewModel.SelectedSignUpUsing.ID == 1)
                    {
                        viewModel.MaxLengthID = 10;
                    }
                    else if (viewModel.SelectedSignUpUsing.ID == 2)
                    {
                        viewModel.MaxLengthID = 10;
                    }
                    else if (viewModel.SelectedSignUpUsing.ID == 3)
                    {
                        viewModel.MaxLengthID = 15;
                    }
                    //  TxtIDType = _selectedSignUpUsing.SUType;
                }
                catch (Exception Ex)
                {
                }
                viewModel.SelectedSignUpUsingSetForCancle = viewModel.SelectedSignUpUsing;
            }
        }

        private void EntryName_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(viewModel.TxtName))
            {
                viewModel.IsAllValidDataEntered = true;
                 FrmName.HasError = false;
            }
        }

        private void CRDuplicateCheck()
        {
            CaseGuidModelRootObject ResutGuid = WebServiceManager.GAZTGetSignupGuid();
            SignUpNextBodyModel SiguupModel = new SignUpNextBodyModel();
            if (App.IsArabic)
            {
                SiguupModel.ALang = "A";
            }
            else
            {
                SiguupModel.ALang = "E";
            }
            var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;
            string month = selectedItem[1].ToString();
            string day = selectedItem[0].ToString();
            string year = selectedItem[2].ToString();
            SiguupModel.ABirthdt = year + "-" + month + "-" + day + "T00:00:00";
            SiguupModel.AType = "1";
            SiguupModel.AFirstname = viewModel.TxtName;
            SiguupModel.ALastname = ".";
            if (viewModel.IsTIN)
            {
                SiguupModel.ATin = viewModel.TxtTIN;
                SiguupModel.ATinExist = "X";
            }
            else
            {
                SiguupModel.ATin = "";
                SiguupModel.ATinExist = "";
            }
            SiguupModel.AIdnumber = viewModel.TxtIDNumber;
            if (viewModel.IsCRChecked == true)
            {
                SiguupModel.ACommId = viewModel.TxtCRNumber;
                SiguupModel.ALicenceNo = "";
                SiguupModel.AIssuedBy = "";
                SiguupModel.ACity = "";
                SiguupModel.ACityCode = "";
            }
            else
            {
                SiguupModel.ALicenceNo = viewModel.TxtLicenseNumber;
                SiguupModel.AIssuedBy = viewModel.SelectedIssuedBy.elementCode;
                try
                {
                    if (viewModel.SelectCityList != null && viewModel.SelectCityList.CityName != null)
                    {
                        SiguupModel.ACity = viewModel.SelectCityList.CityName;
                        SiguupModel.ACityCode = viewModel.SelectCityList.CityCode;
                    }
                    else
                    {
                        SiguupModel.ACity = string.Empty;
                        SiguupModel.ACityCode = string.Empty;

                    }
                    SiguupModel.ACommId = "";
                }
                catch (Exception ex)
                {
                }
                //SiguupModel.ACity = viewModel.SelectCityList.CityName;
                //SiguupModel.ACityCode = viewModel.SelectCityList.CityCode;
                SiguupModel.ACommId = "";
            }
            SiguupModel.AEmail = viewModel.TxtEmailAddress;
            SiguupModel.APhone = "00966" + viewModel.TxtPhoneNumber;

            string newCountryCodeString = viewModel.TxtCountryCode.Replace("+", "00");
            SiguupModel.AMobile = newCountryCodeString + viewModel.TxtMobileNumber;
            SiguupModel.ACountry = viewModel.MobileCountryCode;

            // SiguupModel.AMobile = "00966" + viewModel.TxtMobileNumber;

            if (viewModel.SelectedSignUpUsing.ID == 1)
            {
                SiguupModel.AIdtype = "ZS0001";
            }
            else if (viewModel.SelectedSignUpUsing.ID == 2)
            {
                SiguupModel.AIdtype = "ZS0002";
            }
            else if (viewModel.SelectedSignUpUsing.ID == 3)
            {
                SiguupModel.AIdtype = "ZS0003";
            }
            SiguupModel.CaseGuid = ResutGuid.d.results[0].CaseGuid;
            string ResultFirstSubmit = WebServiceManager.GAZTSignUpFirstSubmit(SiguupModel);
            SignUpModelRootObject ResultFirstSubmitModel = JsonConvert.DeserializeObject<SignUpModelRootObject>(ResultFirstSubmit);
            viewModel.SignUpFirstSubmitModel = ResultFirstSubmitModel;
            if (ResultFirstSubmitModel.d == null)
            {
                SignupErrorModelRootObject SignupErrorModelRootObjectModel = JsonConvert.DeserializeObject<SignupErrorModelRootObject>(ResultFirstSubmit);
                StringBuilder Message = new StringBuilder();
                foreach (SignupErrorModelErrordetail itemerror in SignupErrorModelRootObjectModel.error.innererror.errordetails)
                {
                    if (itemerror.code.Contains("ZD_PUSR"))
                    {
                        if (Message.Length > 0)
                        {
                            Message.Append(Environment.NewLine);
                        }
                        Message.Append(itemerror.message);
                    }
                }
                viewModel._dialogService.ShowMessage(Message.ToString(), AppResources.Information);
            }
            else
            {
                NavigateToVerifyOTPScreenAsync(ResultFirstSubmitModel);
                //viewModel._navigationService.NavigateTo(App.CreateGaztAccountPageView, ResultFirstSubmitModel);
            }
        }
        //private async void btnSubmitNext_Clicked(object sender, EventArgs e)
        //{
        //    if (viewModel.CurrentTab == EstablishmentSignUPTabEnum.ContactInformation)
        //    {
        //        if (viewModel.IsAllValidContactDataEnteredEmail &&
        //                viewModel.IsAllValidContactDataEnteredMobileNbr &&
        //                viewModel.IsAllValidContactDataEnteredPhoneNbr)
        //        {
        //            viewModel.PageTitle = AppResources.VerificationCode;
        //            viewModel.BodyText = AppResources.ZZPleaseenteraccessCode;
        //            viewModel.CurrentTab = EstablishmentSignUPTabEnum.EmailVerification;

        //            viewModel.TimerStart(viewModel.numberOfSeconds);

        //            EstablishmentSignUpDataAsync();
        //        }
        //    }

        //    //Final CreateGAZT Account
        //    else if (viewModel.CurrentTab == EstablishmentSignUPTabEnum.EmailVerification)
        //    {
        //        CreateGAZTAccount();
        //    }
        //    else
        //    {
        //        viewModel.navigateToNext();
        //    }
        //}

        private async void btnSubmitNext_Clicked(object sender, EventArgs e)
        {
            if (viewModel.CurrentTab == EstablishmentSignUPTabEnum.ContactInformation)
            {
                if (viewModel.IsAllValidContactDataEnteredEmail &&
                        viewModel.IsAllValidContactDataEnteredMobileNbr &&
                        viewModel.IsAllValidContactDataEnteredPhoneNbr)
                {
                    viewModel.PageTitle = AppResources.CRSummary;
                    viewModel.BodyText = AppResources.CRReviewthebelowinformation;
                    viewModel.NextBTN = AppResources.ZZZZContinue;
                    viewModel.CurrentTab = EstablishmentSignUPTabEnum.MobileVerification;
                    //viewModel.TimerStart(viewModel.numberOfSeconds);
                    //EstablishmentSignUpDataAsync();
                }
            }

            //Final CreateGAZT Account
            else if (viewModel.CurrentTab == EstablishmentSignUPTabEnum.EmailVerification)
            {
                CreateGAZTAccount();
            }
            else if (viewModel.CurrentTab == EstablishmentSignUPTabEnum.MobileVerification)
            {
                viewModel.PageTitle = AppResources.VerificationCode;
                viewModel.BodyText = AppResources.ZZPleaseenteraccessCode;
                viewModel.CurrentTab = EstablishmentSignUPTabEnum.EmailVerification;
                viewModel.TimerStart(viewModel.numberOfSeconds);
                EstablishmentSignUpDataAsync();
            }
            else
            {
                viewModel.navigateToNext();
            }
        }

        public void CreateGAZTAccount()
        {
            StringBuilder PopMsg = new StringBuilder();
            bool IsAllValid = true;
            if (string.IsNullOrEmpty(viewModel.OTPFirstDigit)
                ||(string.IsNullOrEmpty(viewModel.OTPSecondDigit))
                ||(string.IsNullOrEmpty(viewModel.OTPThirdDigit))
                ||(string.IsNullOrEmpty(viewModel.OTPFourthDigit)))
            {
            //  frmEmailCode.HasError = true;
                PopMsg.Append(AppResources.ZZPleaseenterconfirmationcodesenttoyouremailaddress);
                IsAllValid = false;
            }
            else
            {
              //frmEmailCode.HasError = false;
            }
            if  (string.IsNullOrEmpty(viewModel.MOTPFirstDigit)
                || (string.IsNullOrEmpty(viewModel.MOTPSecondDigit))
                || (string.IsNullOrEmpty(viewModel.MOTPThirdDigit))
                || (string.IsNullOrEmpty(viewModel.MOTPFourthDigit)))
                {
              //frmMobileCode.HasError = true;
                if (PopMsg.Length > 0)
                {
                    PopMsg.Append(Environment.NewLine);
                    PopMsg.Append(Environment.NewLine);
                    PopMsg.Append(AppResources.ZZPleaseenterconfirmationcodesenttoyourmobilenumber);
                }
                else
                {
                    PopMsg.Append(AppResources.ZZPleaseenterconfirmationcodesenttoyourmobilenumber);
                }
                IsAllValid = false;
            }
            else
            {
              //frmMobileCode.HasError = false;
            }
            if (string.IsNullOrEmpty(viewModel.TxtPassword))
            {
              //frmPass.HasError = true;
                if (PopMsg.Length > 0)
                {
                    PopMsg.Append(Environment.NewLine);
                    PopMsg.Append(Environment.NewLine);
                    PopMsg.Append(AppResources.ZZPasswordregulationsforSignup);
                }
                else
                {
                    PopMsg.Append(AppResources.ZZPasswordregulationsforSignup);
                }
                IsAllValid = false;
            }
            else
            {
              //frmPass.HasError = false;
                bool IsValidPass = UtilityManager.IsPasswordValid(viewModel.TxtPassword);
                if (!IsValidPass)
                {
                  //frmPass.HasError = true;
                    if (PopMsg.Length > 0)
                    {
                        PopMsg.Append(Environment.NewLine);
                        PopMsg.Append(Environment.NewLine);
                        PopMsg.Append(AppResources.ZZPasswordregulationsforSignup);
                    }
                    else
                    {
                        PopMsg.Append(AppResources.ZZPasswordregulationsforSignup);
                    }
                    IsAllValid = false;
                }
                else
                {
                   //rmPass.HasError = false;
                }
                if (viewModel.TxtPassword != viewModel.TxtConfirmPassword)
                {
                  //frmPass.HasError = true;
                    if (PopMsg.Length > 0)
                    {
                        PopMsg.Append(Environment.NewLine);
                        PopMsg.Append(Environment.NewLine);
                        PopMsg.Append(AppResources.ZZNewpasswordfieldandconfirmPasswordfieldshouldmatchup);
                    }
                    else
                    {
                        PopMsg.Append(AppResources.ZZNewpasswordfieldandconfirmPasswordfieldshouldmatchup);
                    }
                    IsAllValid = false;
                }
                else
                {
                   //rmCfrmPass.HasError = false;
                }
            }
            if (IsAllValid == true)
            {
                viewModel.CreateGaZTAccount();
            }
            else
            {
                if (PopMsg.Length > 0)
                {
                    PopUp popUp = new PopUp();
                    popUp.Message = PopMsg.ToString();
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
                }
            }
        }

       
        private void EntryCfrmPass_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryCfrmPass.Text))
            {
                if (EntryPass.Text != EntryCfrmPass.Text)
                {
                  //mCfrmPass.HasError = true;
                }
                else
                {
                  //frmCfrmPass.HasError = false;
                }
            }
        }

        private void EntryPass_TextChanged(object sender, TextChangedEventArgs e)
        {
           //rmPass.HasError = false;
        }
        public async Task NavigateToVerifyOTPScreenAsync(SignUpModelRootObject ResultFirstSubmitModel)
        {
            try
            {
                
                Device.BeginInvokeOnMainThread(async () =>
                {
                    viewModel.IsLoading = false;


                });
                viewModel.ButtonDisableColor = Color.FromHex("#9EA4A9");
                viewModel.ButtonDisableTextColor = Color.Gray;
                viewModel.VerifyButtonDisableColor = Color.FromHex("#006450");
                viewModel.VerifyButtonDisableTextColor = Color.White;
                viewModel.IsResendOTPEnabled = false;
                viewModel.IsOTPEntryEnable = true;

                viewModel.SignUpModelRootObjectM = ResultFirstSubmitModel;
                viewModel.TxtEmailAddress = ResultFirstSubmitModel.d.AEmail;
                string mobileno = ResultFirstSubmitModel.d.AMobile;
                viewModel.TxtMobileNumber = "XXXXXXXXXX" + mobileno.Substring(mobileno.Length - 4, 4);
                OTPFirstEntry.Focus();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task EstablishmentSignUpDataAsync()
        {
            
           
                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });
            
                if (viewModel.SelectedSignUpUsing.ID == 1)
                {
                    try
                    {
                        DuplicateSignUpModelRootObject ResultDuplicate = WebServiceManager.GAZTValidateDuplicate(viewModel.TxtIDNumber, "ZS0001", string.Empty, string.Empty, string.Empty);
                        if (ResultDuplicate.d.Flag == "")
                        {
                            if (viewModel.IsCRChecked == true)
                            {
                                try
                                {
                                    //DuplicateSignUpModelRootObject ResultDuplicateCR = WebServiceManager.GAZTValidateDuplicate(viewModel.TxtIDNumber, "ZS0001", "BUP002", "SA");
                                    DuplicateSignUpModelRootObject ResultDuplicateCR = WebServiceManager.GAZTValidateDuplicate(viewModel.TxtCRNumber, "BUP002", "90702", "SA", "CRNum");
                                    if (ResultDuplicateCR.d.Flag == "")
                                    {
                                        CaseGuidModelRootObject ResutGuid = WebServiceManager.GAZTGetSignupGuid();
                                        SignUpNextBodyModel SiguupModel = new SignUpNextBodyModel();
                                        if (App.IsArabic)
                                        {
                                            SiguupModel.ALang = "A";
                                        }
                                        else
                                        {
                                            SiguupModel.ALang = "E";
                                        }
                                        var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;
                                        string month = selectedItem[1].ToString();
                                        string day = selectedItem[0].ToString();
                                        string year = selectedItem[2].ToString();
                                        SiguupModel.ABirthdt = year + "-" + month + "-" + day + "T00:00:00";
                                        SiguupModel.AType = "1";
                                        SiguupModel.AFirstname = viewModel.TxtName;
                                        SiguupModel.ALastname = ".";
                                        if (viewModel.IsTIN)
                                        {
                                            SiguupModel.ATin = viewModel.TxtTIN;
                                            SiguupModel.ATinExist = "X";
                                        }
                                        else
                                        {
                                            SiguupModel.ATin = "";
                                            SiguupModel.ATinExist = "";
                                        }
                                        SiguupModel.AIdnumber = viewModel.TxtIDNumber;
                                        if (viewModel.IsCRChecked == true)
                                        {
                                            SiguupModel.ACommId = viewModel.TxtCRNumber;
                                            SiguupModel.ALicenceNo = "";
                                            SiguupModel.AIssuedBy = "";
                                            SiguupModel.ACity = "";
                                            SiguupModel.ACityCode = "";
                                        }
                                        else
                                        {
                                            SiguupModel.ALicenceNo = viewModel.TxtLicenseNumber;
                                            SiguupModel.AIssuedBy = viewModel.SelectedIssuedBy.elementCode;
                                            try
                                            {
                                                if (viewModel.SelectCityList != null && viewModel.SelectCityList.CityName != null)
                                                {
                                                    SiguupModel.ACity = viewModel.SelectCityList.CityName;
                                                    SiguupModel.ACityCode = viewModel.SelectCityList.CityCode;
                                                }
                                                else
                                                {
                                                    SiguupModel.ACity = string.Empty;
                                                    SiguupModel.ACityCode = string.Empty;

                                                }
                                                SiguupModel.ACommId = "";
                                            }
                                            catch (Exception ex)
                                            {
                                            }
                                            //SiguupModel.ACity = viewModel.SelectCityList.CityName;
                                            //SiguupModel.ACityCode = viewModel.SelectCityList.CityCode;
                                            SiguupModel.ACommId = "";
                                        }
                                        SiguupModel.AEmail = viewModel.TxtEmailAddress;

                                        if (viewModel.TxtPhoneNumber != null || viewModel.TxtPhoneNumber != string.Empty)
                                        {
                                            SiguupModel.APhone = "00966" + viewModel.TxtPhoneNumber;
                                        }
                                        else
                                        {
                                            SiguupModel.APhone = "";

                                        }

                                        string newCountryCodeString = viewModel.TxtCountryCode.Replace("+", "00");
                                        SiguupModel.AMobile = newCountryCodeString + viewModel.TxtMobileNumber;
                                        SiguupModel.ACountry = viewModel.MobileCountryCode;
                                        // SiguupModel.AMobile = "00966" + viewModel.TxtMobileNumber;
                                        if (viewModel.SelectedSignUpUsing.ID == 1)
                                        {
                                            SiguupModel.AIdtype = "ZS0001";
                                        }
                                        else if (viewModel.SelectedSignUpUsing.ID == 2)
                                        {
                                            SiguupModel.AIdtype = "ZS0002";
                                        }
                                        else if (viewModel.SelectedSignUpUsing.ID == 3)
                                        {
                                            SiguupModel.AIdtype = "ZS0003";
                                        }
                                        SiguupModel.CaseGuid = ResutGuid.d.results[0].CaseGuid;
                                        string ResultFirstSubmit = WebServiceManager.GAZTSignUpFirstSubmit(SiguupModel);
                                        SignUpModelRootObject ResultFirstSubmitModel = JsonConvert.DeserializeObject<SignUpModelRootObject>(ResultFirstSubmit);
                                        viewModel.SignUpFirstSubmitModel = ResultFirstSubmitModel;
                                        if (ResultFirstSubmitModel.d == null)
                                        {
                                            SignupErrorModelRootObject SignupErrorModelRootObjectModel = JsonConvert.DeserializeObject<SignupErrorModelRootObject>(ResultFirstSubmit);
                                            StringBuilder Message = new StringBuilder();
                                            foreach (SignupErrorModelErrordetail itemerror in SignupErrorModelRootObjectModel.error.innererror.errordetails)
                                            {
                                                if (itemerror.code.Contains("ZD_PUSR"))
                                                {
                                                    if (Message.Length > 0)
                                                    {
                                                        Message.Append(Environment.NewLine);
                                                    }
                                                    Message.Append(itemerror.message);
                                                }
                                            }
                                            viewModel._dialogService.ShowMessage(Message.ToString(), AppResources.Information);
                                        }
                                        else
                                        {
                                        NavigateToVerifyOTPScreenAsync(ResultFirstSubmitModel);

                                    }
                                    }
                                    else if (ResultDuplicateCR.d.Flag == "X")
                                    {
                                        var result = false;

                                        if (App.IsArabic)
                                        {

                                            result = await Xamarin.Forms.Application.Current.MainPage.DisplayAlert
                                                                            (AppResources.Alerts, AppResources.ZZZCRValidateMessg,
                                                                                AppResources.ZZZNoText, AppResources.ZZZYesText);
                                            if (result == true)
                                            {
                                                viewModel.IsLoading = false;

                                                return;

                                            }
                                            else // if it's equal to YES
                                            {
                                                CRDuplicateCheck();
                                            }
                                        }
                                        else
                                        {

                                            result = await Xamarin.Forms.Application.Current.MainPage.DisplayAlert
                                                                           (AppResources.Alerts, AppResources.ZZZCRValidateMessg,
                                                                               AppResources.ZZZYesText, AppResources.ZZZNoText);

                                            if (result == true)
                                            {
                                                CRDuplicateCheck();
                                                // _navigationService.GoBack();

                                            }
                                            else // if it's equal to NO
                                            {
                                                viewModel.IsLoading = false;

                                                return; // just return to the page and do nothing.
                                            }
                                        }

                                        //viewModel._dialogService.ShowMessage(AppResources.ZZZCRValidateMessg, AppResources.Information);


                                    }
                                    else
                                    {
                                        viewModel._dialogService.ShowMessage(AppResources.ZZYoushouldsignupasnewuser, AppResources.Information);
                                    }
                                }

                                catch (GAZTException gex)
                                {
                                    // Handle the GAZT custom exception.
                                    string MessageForTheUser = gex.Message;
                                    if (gex is GAZTInvalidDataException)
                                    {
                                        MessageForTheUser = AppResources.ZZSomethingwentwrong;
                                    }
                                    if (gex is GAZTNetworkConnectivityIssueException)
                                    {
                                        MessageForTheUser = AppResources.NetworkConnectivityIssue;
                                    }
                                    else if (gex is GAZTInternetException)
                                    {
                                        MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                                    }
                                    else if (gex is GAZTSessionExpiredException)
                                    {
                                        MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                                    }

                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        viewModel.IsLoading = false;

                                        await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                        viewModel._navigationService.GoBack();
                                    });
                                }
                                catch (HttpRequestException ex)
                                {
                                    string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        // IsLoading = false;

                                        await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                        //_navigationService.GoBack();
                                    });
                                }


                                catch (InternetException ex)
                                {
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                    });
                                }
                                catch (Exception ex)
                                {

                                    string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        // IsLoading = false;

                                        await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);

                                    });
                                }



                            }
                            else
                            {
                                try
                                {
                                    CaseGuidModelRootObject ResutGuid = WebServiceManager.GAZTGetSignupGuid();
                                    SignUpNextBodyModel SiguupModel = new SignUpNextBodyModel();
                                    if (App.IsArabic)
                                    {
                                        SiguupModel.ALang = "A";
                                    }
                                    else
                                    {
                                        SiguupModel.ALang = "E";
                                    }
                                    var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;
                                    string month = selectedItem[1].ToString();
                                    string day = selectedItem[0].ToString();
                                    string year = selectedItem[2].ToString();
                                    SiguupModel.ABirthdt = year + "-" + month + "-" + day + "T00:00:00";
                                    SiguupModel.AType = "1";
                                    SiguupModel.AFirstname = viewModel.TxtName;
                                    SiguupModel.ALastname = ".";
                                    if (viewModel.IsTIN)
                                    {
                                        SiguupModel.ATin = viewModel.TxtTIN;
                                        SiguupModel.ATinExist = "X";
                                    }
                                    else
                                    {
                                        SiguupModel.ATin = "";
                                        SiguupModel.ATinExist = "";
                                    }
                                    SiguupModel.AIdnumber = viewModel.TxtIDNumber;
                                    if (viewModel.IsCRChecked == true)
                                    {
                                        SiguupModel.ACommId = viewModel.TxtCRNumber;
                                        SiguupModel.ALicenceNo = "";
                                        SiguupModel.AIssuedBy = "";
                                        SiguupModel.ACity = "";
                                        SiguupModel.ACityCode = "";
                                    }
                                    else
                                    {
                                        SiguupModel.ALicenceNo = viewModel.TxtLicenseNumber;
                                        SiguupModel.AIssuedBy = viewModel.SelectedIssuedBy.elementCode;
                                        try
                                        {
                                            if (viewModel.SelectCityList != null && viewModel.SelectCityList.CityName != null)
                                            {
                                                SiguupModel.ACity = viewModel.SelectCityList.CityName;
                                                SiguupModel.ACityCode = viewModel.SelectCityList.CityCode;
                                            }
                                            else
                                            {
                                                SiguupModel.ACity = string.Empty;
                                                SiguupModel.ACityCode = string.Empty;

                                            }
                                            SiguupModel.ACommId = "";
                                        }
                                        catch (Exception ex)
                                        {
                                        }
                                        //SiguupModel.ACity = viewModel.SelectCityList.CityName;
                                        //SiguupModel.ACityCode = viewModel.SelectCityList.CityCode;
                                        SiguupModel.ACommId = "";
                                    }
                                    SiguupModel.AEmail = viewModel.TxtEmailAddress;
                                    if (viewModel.TxtPhoneNumber != null || viewModel.TxtPhoneNumber != string.Empty)
                                    {
                                        SiguupModel.APhone = "00966" + viewModel.TxtPhoneNumber;
                                    }
                                    else
                                    {
                                        SiguupModel.APhone = "";

                                    }

                                    string newCountryCodeString = viewModel.TxtCountryCode.Replace("+", "00");
                                    SiguupModel.AMobile = newCountryCodeString + viewModel.TxtMobileNumber;

                                    SiguupModel.ACountry = viewModel.MobileCountryCode;

                                    // SiguupModel.AMobile = "00966" + viewModel.TxtMobileNumber;
                                    if (viewModel.SelectedSignUpUsing.ID == 1)
                                    {
                                        SiguupModel.AIdtype = "ZS0001";
                                    }
                                    else if (viewModel.SelectedSignUpUsing.ID == 2)
                                    {
                                        SiguupModel.AIdtype = "ZS0002";
                                    }
                                    else if (viewModel.SelectedSignUpUsing.ID == 3)
                                    {
                                        SiguupModel.AIdtype = "ZS0003";
                                    }
                                    SiguupModel.CaseGuid = ResutGuid.d.results[0].CaseGuid;
                                    string ResultFirstSubmit = WebServiceManager.GAZTSignUpFirstSubmit(SiguupModel);
                                    SignUpModelRootObject ResultFirstSubmitModel = JsonConvert.DeserializeObject<SignUpModelRootObject>(ResultFirstSubmit);
                                    viewModel.SignUpFirstSubmitModel = ResultFirstSubmitModel;
                                    if (ResultFirstSubmitModel.d == null)
                                    {
                                        SignupErrorModelRootObject SignupErrorModelRootObjectModel = JsonConvert.DeserializeObject<SignupErrorModelRootObject>(ResultFirstSubmit);
                                        StringBuilder Message = new StringBuilder();
                                        foreach (SignupErrorModelErrordetail itemerror in SignupErrorModelRootObjectModel.error.innererror.errordetails)
                                        {
                                            if (itemerror.code.Contains("ZD_PUSR"))
                                            {
                                                if (Message.Length > 0)
                                                {
                                                    Message.Append(Environment.NewLine);
                                                }
                                                Message.Append(itemerror.message);
                                            }
                                        }
                                        viewModel._dialogService.ShowMessage(Message.ToString(), AppResources.Information);
                                    }
                                    else
                                    {

                                 NavigateToVerifyOTPScreenAsync(ResultFirstSubmitModel);

                                //  viewModel._navigationService.NavigateTo(App.CreateGaztAccountPageView, ResultFirstSubmitModel);
                                    }
                                }

                                catch (GAZTException gex)
                                {
                                    // Handle the GAZT custom exception.
                                    string MessageForTheUser = gex.Message;
                                    if (gex is GAZTInvalidDataException)
                                    {
                                        MessageForTheUser = AppResources.ZZSomethingwentwrong;
                                    }
                                    if (gex is GAZTNetworkConnectivityIssueException)
                                    {
                                        MessageForTheUser = AppResources.NetworkConnectivityIssue;
                                    }
                                    else if (gex is GAZTInternetException)
                                    {
                                        MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                                    }
                                    else if (gex is GAZTSessionExpiredException)
                                    {
                                        MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                                    }

                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        viewModel.IsLoading = false;

                                        await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                        viewModel._navigationService.GoBack();
                                    });
                                }



                                catch (InternetException ex)
                                {
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                    });
                                }
                                catch (HttpRequestException ex)
                                {
                                    string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        // IsLoading = false;

                                        await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                        //_navigationService.GoBack();
                                    });
                                }
                                catch (Exception ex)
                                {

                                    string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        // IsLoading = false;

                                        await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                        //_navigationService.GoBack();
                                    });
                                }
                            }
                        }
                        else
                        {
                            viewModel._dialogService.ShowMessage(AppResources.ZZYoushouldsignupasnewuser, AppResources.Information);
                        }
                    }
                    catch (GAZTException gex)
                    {
                        // Handle the GAZT custom exception.
                        string MessageForTheUser = gex.Message;
                        if (gex is GAZTInvalidDataException)
                        {
                            MessageForTheUser = AppResources.ZZSomethingwentwrong;
                        }
                        if (gex is GAZTNetworkConnectivityIssueException)
                        {
                            MessageForTheUser = AppResources.NetworkConnectivityIssue;
                        }
                        else if (gex is GAZTInternetException)
                        {
                            MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                        }
                        else if (gex is GAZTSessionExpiredException)
                        {
                            MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                        }

                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            viewModel.IsLoading = false;

                            await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                            viewModel._navigationService.GoBack();
                        });
                    }
                    catch (InternetException ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                        });
                    }
                    catch (HttpRequestException ex)
                    {
                        string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            // IsLoading = false;

                            await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                            //_navigationService.GoBack();
                        });
                    }
                    catch (Exception ex)
                    {

                        string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            // IsLoading = false;

                            await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                            //_navigationService.GoBack();
                        });
                    }
                }
                if (viewModel.SelectedSignUpUsing.ID == 2)
                {
                    DuplicateSignUpModelRootObject ResultDuplicate = WebServiceManager.GAZTValidateDuplicate(viewModel.TxtIDNumber, "ZS0002", string.Empty, string.Empty, string.Empty);
                    if (ResultDuplicate.d.Flag == "")
                    {
                        if (viewModel.IsCRChecked == true)
                        {
                            try
                            {
                                //DuplicateSignUpModelRootObject ResultDuplicateCR = WebServiceManager.GAZTValidateDuplicate(viewModel.TxtIDNumber, "ZS0001", "BUP002", "SA");
                                DuplicateSignUpModelRootObject ResultDuplicateCR = WebServiceManager.GAZTValidateDuplicate(viewModel.TxtCRNumber, "BUP002", "90702", "SA", "CRNum");

                                if (ResultDuplicateCR.d.Flag == "")
                                {
                                    CaseGuidModelRootObject ResutGuid = WebServiceManager.GAZTGetSignupGuid();
                                    SignUpNextBodyModel SiguupModel = new SignUpNextBodyModel();
                                    if (App.IsArabic)
                                    {
                                        SiguupModel.ALang = "A";
                                    }
                                    else
                                    {
                                        SiguupModel.ALang = "E";
                                    }
                                    var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;
                                    string month = selectedItem[1].ToString();
                                    string day = selectedItem[0].ToString();
                                    string year = selectedItem[2].ToString();
                                    SiguupModel.ABirthdt = year + "-" + month + "-" + day + "T00:00:00";
                                    SiguupModel.AType = "1";
                                    SiguupModel.AFirstname = viewModel.TxtName;
                                    SiguupModel.ALastname = ".";
                                    if (viewModel.IsTIN)
                                    {
                                        SiguupModel.ATin = viewModel.TxtTIN;
                                        SiguupModel.ATinExist = "X";
                                    }
                                    else
                                    {
                                        SiguupModel.ATin = "";
                                        SiguupModel.ATinExist = "";
                                    }
                                    SiguupModel.AIdnumber = viewModel.TxtIDNumber;
                                    if (viewModel.IsCRChecked == true)
                                    {
                                        SiguupModel.ACommId = viewModel.TxtCRNumber;
                                        SiguupModel.ALicenceNo = "";
                                        SiguupModel.AIssuedBy = "";
                                        SiguupModel.ACity = "";
                                        SiguupModel.ACityCode = "";
                                    }
                                    else
                                    {
                                        SiguupModel.ALicenceNo = viewModel.TxtLicenseNumber;
                                        SiguupModel.AIssuedBy = viewModel.SelectedIssuedBy.elementCode;
                                        try
                                        {
                                            if (viewModel.SelectCityList != null && viewModel.SelectCityList.CityName != null)
                                            {
                                                SiguupModel.ACity = viewModel.SelectCityList.CityName;
                                                SiguupModel.ACityCode = viewModel.SelectCityList.CityCode;
                                            }
                                            else
                                            {
                                                SiguupModel.ACity = string.Empty;
                                                SiguupModel.ACityCode = string.Empty;

                                            }
                                            SiguupModel.ACommId = "";
                                        }
                                        catch (Exception ex)
                                        {
                                        }
                                        //SiguupModel.ACity = viewModel.SelectCityList.CityName;
                                        //SiguupModel.ACityCode = viewModel.SelectCityList.CityCode;
                                        SiguupModel.ACommId = "";
                                    }
                                    SiguupModel.AEmail = viewModel.TxtEmailAddress;
                                    if (viewModel.TxtPhoneNumber != null || viewModel.TxtPhoneNumber != string.Empty)
                                    {
                                        SiguupModel.APhone = "00966" + viewModel.TxtPhoneNumber;
                                    }
                                    else
                                    {
                                        SiguupModel.APhone = "";

                                    }

                                    string newCountryCodeString = viewModel.TxtCountryCode.Replace("+", "00");
                                    SiguupModel.AMobile = newCountryCodeString + viewModel.TxtMobileNumber;
                                    // SiguupModel.AMobile = "00966" + viewModel.TxtMobileNumber;
                                    SiguupModel.ACountry = viewModel.MobileCountryCode;

                                    if (viewModel.SelectedSignUpUsing.ID == 1)
                                    {
                                        SiguupModel.AIdtype = "ZS0001";
                                    }
                                    else if (viewModel.SelectedSignUpUsing.ID == 2)
                                    {
                                        SiguupModel.AIdtype = "ZS0002";
                                    }
                                    else if (viewModel.SelectedSignUpUsing.ID == 3)
                                    {
                                        SiguupModel.AIdtype = "ZS0003";
                                    }
                                    SiguupModel.CaseGuid = ResutGuid.d.results[0].CaseGuid;
                                    string ResultFirstSubmit = WebServiceManager.GAZTSignUpFirstSubmit(SiguupModel);
                                    SignUpModelRootObject ResultFirstSubmitModel = JsonConvert.DeserializeObject<SignUpModelRootObject>(ResultFirstSubmit);
                                    viewModel.SignUpFirstSubmitModel = ResultFirstSubmitModel;
                                    if (ResultFirstSubmitModel.d == null)
                                    {
                                        SignupErrorModelRootObject SignupErrorModelRootObjectModel = JsonConvert.DeserializeObject<SignupErrorModelRootObject>(ResultFirstSubmit);
                                        StringBuilder Message = new StringBuilder();
                                        foreach (SignupErrorModelErrordetail itemerror in SignupErrorModelRootObjectModel.error.innererror.errordetails)
                                        {
                                            if (itemerror.code.Contains("ZD_PUSR"))
                                            {
                                                if (Message.Length > 0)
                                                {
                                                    Message.Append(Environment.NewLine);
                                                }
                                                Message.Append(itemerror.message);
                                            }
                                        }
                                        viewModel._dialogService.ShowMessage(Message.ToString(), AppResources.Information);
                                    }
                                    else
                                    {
                                    NavigateToVerifyOTPScreenAsync(ResultFirstSubmitModel);
                                      //viewModel._navigationService.NavigateTo(App.CreateGaztAccountPageView, ResultFirstSubmitModel);
                                    }
                                }
                                else if (ResultDuplicateCR.d.Flag == "X")
                                {
                                    var result = false;

                                    if (App.IsArabic)
                                    {

                                        result = await Xamarin.Forms.Application.Current.MainPage.DisplayAlert
                                                                        (AppResources.Alerts, AppResources.ZZZCRValidateMessg,
                                                                            AppResources.ZZZNoText, AppResources.ZZZYesText);
                                        if (result == true)
                                        {
                                            viewModel.IsLoading = false;

                                            return;

                                        }
                                        else // if it's equal to YES
                                        {
                                            CRDuplicateCheck();
                                        }
                                    }
                                    else
                                    {

                                        result = await Xamarin.Forms.Application.Current.MainPage.DisplayAlert
                                                                       (AppResources.Alerts, AppResources.ZZZCRValidateMessg,
                                                                           AppResources.ZZZYesText, AppResources.ZZZNoText);

                                        if (result == true)
                                        {
                                            CRDuplicateCheck();
                                            // _navigationService.GoBack();

                                        }
                                        else // if it's equal to NO
                                        {
                                            viewModel.IsLoading = false;

                                            return; // just return to the page and do nothing.
                                        }
                                    }

                                    //viewModel._dialogService.ShowMessage(AppResources.ZZZCRValidateMessg, AppResources.Information);


                                }
                                else
                                {
                                    viewModel._dialogService.ShowMessage(AppResources.ZZYoushouldsignupasnewuser, AppResources.Information);
                                }
                            }
                            catch (GAZTException gex)
                            {
                                // Handle the GAZT custom exception.
                                string MessageForTheUser = gex.Message;
                                if (gex is GAZTInvalidDataException)
                                {
                                    MessageForTheUser = AppResources.ZZSomethingwentwrong;
                                }
                                if (gex is GAZTNetworkConnectivityIssueException)
                                {
                                    MessageForTheUser = AppResources.NetworkConnectivityIssue;
                                }
                                else if (gex is GAZTInternetException)
                                {
                                    MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                                }
                                else if (gex is GAZTSessionExpiredException)
                                {
                                    MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                                }

                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    viewModel.IsLoading = false;

                                    await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                    viewModel._navigationService.GoBack();
                                });
                            }
                            catch (InternetException ex)
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                });
                            }
                            catch (HttpRequestException ex)
                            {
                                string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    // IsLoading = false;

                                    await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                    //_navigationService.GoBack();
                                });
                            }
                            catch (Exception ex)
                            {

                                string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    // IsLoading = false;

                                    await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                    //_navigationService.GoBack();
                                });
                            }
                        }
                        else
                        {
                            try
                            {
                                CaseGuidModelRootObject ResutGuid = WebServiceManager.GAZTGetSignupGuid();
                                SignUpNextBodyModel SiguupModel = new SignUpNextBodyModel();
                                if (App.IsArabic)
                                {
                                    SiguupModel.ALang = "A";
                                }
                                else
                                {
                                    SiguupModel.ALang = "E";
                                }
                                var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;
                                string month = selectedItem[1].ToString();
                                string day = selectedItem[0].ToString();
                                string year = selectedItem[2].ToString();
                                SiguupModel.ABirthdt = year + "-" + month + "-" + day + "T00:00:00";
                                SiguupModel.AType = "1";
                                SiguupModel.AFirstname = viewModel.TxtName;
                                SiguupModel.ALastname = ".";
                                if (viewModel.IsTIN)
                                {
                                    SiguupModel.ATin = viewModel.TxtTIN;
                                    SiguupModel.ATinExist = "X";
                                }
                                else
                                {
                                    SiguupModel.ATin = "";
                                    SiguupModel.ATinExist = "";
                                }
                                SiguupModel.AIdnumber = viewModel.TxtIDNumber;
                                if (viewModel.IsCRChecked == true)
                                {
                                    SiguupModel.ACommId = viewModel.TxtCRNumber;
                                    SiguupModel.ALicenceNo = "";
                                    SiguupModel.AIssuedBy = "";
                                    SiguupModel.ACity = "";
                                    SiguupModel.ACityCode = "";
                                }
                                else
                                {
                                    SiguupModel.ALicenceNo = viewModel.TxtLicenseNumber;
                                    SiguupModel.AIssuedBy = viewModel.SelectedIssuedBy.elementCode;
                                    try
                                    {
                                        if (viewModel.SelectCityList != null && viewModel.SelectCityList.CityName != null)
                                        {
                                            SiguupModel.ACity = viewModel.SelectCityList.CityName;
                                            SiguupModel.ACityCode = viewModel.SelectCityList.CityCode;
                                        }
                                        else
                                        {
                                            SiguupModel.ACity = string.Empty;
                                            SiguupModel.ACityCode = string.Empty;

                                        }
                                        SiguupModel.ACommId = "";
                                    }
                                    catch (Exception ex)
                                    {
                                    }
                                    //SiguupModel.ACity = viewModel.SelectCityList.CityName;
                                    //SiguupModel.ACityCode = viewModel.SelectCityList.CityCode;
                                    SiguupModel.ACommId = "";
                                }
                                SiguupModel.AEmail = viewModel.TxtEmailAddress;
                                if (!viewModel.TxtPhoneNumber.Contains(string.Empty))
                                {
                                    SiguupModel.APhone = "00966" + viewModel.TxtPhoneNumber;
                                }
                                else
                                {
                                    SiguupModel.APhone = "";

                                }

                                string newCountryCodeString = viewModel.TxtCountryCode.Replace("+", "00");
                                SiguupModel.AMobile = newCountryCodeString + viewModel.TxtMobileNumber;

                                SiguupModel.ACountry = viewModel.MobileCountryCode;

                                // SiguupModel.AMobile = "00966" + viewModel.TxtMobileNumber;
                                if (viewModel.SelectedSignUpUsing.ID == 1)
                                {
                                    SiguupModel.AIdtype = "ZS0001";
                                }
                                else if (viewModel.SelectedSignUpUsing.ID == 2)
                                {
                                    SiguupModel.AIdtype = "ZS0002";
                                }
                                else if (viewModel.SelectedSignUpUsing.ID == 3)
                                {
                                    SiguupModel.AIdtype = "ZS0003";
                                }
                                SiguupModel.CaseGuid = ResutGuid.d.results[0].CaseGuid;
                                string ResultFirstSubmit = WebServiceManager.GAZTSignUpFirstSubmit(SiguupModel);
                                SignUpModelRootObject ResultFirstSubmitModel = JsonConvert.DeserializeObject<SignUpModelRootObject>(ResultFirstSubmit);
                                viewModel.SignUpFirstSubmitModel = ResultFirstSubmitModel;
                                if (ResultFirstSubmitModel.d == null)
                                {
                                    SignupErrorModelRootObject SignupErrorModelRootObjectModel = JsonConvert.DeserializeObject<SignupErrorModelRootObject>(ResultFirstSubmit);
                                    StringBuilder Message = new StringBuilder();
                                    foreach (SignupErrorModelErrordetail itemerror in SignupErrorModelRootObjectModel.error.innererror.errordetails)
                                    {
                                        if (itemerror.code.Contains("ZD_PUSR"))
                                        {
                                            if (Message.Length > 0)
                                            {
                                                Message.Append(Environment.NewLine);
                                            }
                                            Message.Append(itemerror.message);
                                        }
                                    }
                                    viewModel._dialogService.ShowMessage(Message.ToString(), AppResources.Information);
                                }
                                else
                                {
                                NavigateToVerifyOTPScreenAsync(ResultFirstSubmitModel);
                                  //viewModel._navigationService.NavigateTo(App.CreateGaztAccountPageView, ResultFirstSubmitModel);
                                }
                            }
                            catch (GAZTException gex)
                            {
                                // Handle the GAZT custom exception.
                                string MessageForTheUser = gex.Message;
                                if (gex is GAZTInvalidDataException)
                                {
                                    MessageForTheUser = AppResources.ZZSomethingwentwrong;
                                }
                                if (gex is GAZTNetworkConnectivityIssueException)
                                {
                                    MessageForTheUser = AppResources.NetworkConnectivityIssue;
                                }
                                else if (gex is GAZTInternetException)
                                {
                                    MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                                }
                                else if (gex is GAZTSessionExpiredException)
                                {
                                    MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                                }

                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    viewModel.IsLoading = false;

                                    await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                    viewModel._navigationService.GoBack();
                                });
                            }
                            catch (InternetException ex)
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                });
                            }
                            catch (HttpRequestException ex)
                            {
                                string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    // IsLoading = false;

                                    await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                    //_navigationService.GoBack();
                                });
                            }
                            catch (Exception ex)
                            {

                                string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    // IsLoading = false;

                                    await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                    //_navigationService.GoBack();
                                });
                            }
                        }
                    }
                    else
                    {
                        viewModel._dialogService.ShowMessage(AppResources.ZZYoushouldsignupasnewuser, AppResources.Information);
                    }
                }
                if (viewModel.SelectedSignUpUsing.ID == 3)
                {
                    try
                    {
                        DuplicateSignUpModelRootObject ResultDuplicate = WebServiceManager.GAZTValidateDuplicate(viewModel.TxtIDNumber, "ZS0003", string.Empty, string.Empty, string.Empty);
                        if (viewModel.IsCRChecked == true)
                        {
                            try
                            {
                                //  DuplicateSignUpModelRootObject ResultDuplicateCR = WebServiceManager.GAZTValidateDuplicate(viewModel.TxtIDNumber, "ZS0001", "BUP002", "SA");
                                DuplicateSignUpModelRootObject ResultDuplicateCR = WebServiceManager.GAZTValidateDuplicate(viewModel.TxtCRNumber, "BUP002", "90702", "SA", "CRNum");

                                if (ResultDuplicateCR.d.Flag == "")
                                {
                                    CaseGuidModelRootObject ResutGuid = WebServiceManager.GAZTGetSignupGuid();
                                    SignUpNextBodyModel SiguupModel = new SignUpNextBodyModel();
                                    if (App.IsArabic)
                                    {
                                        SiguupModel.ALang = "A";
                                    }
                                    else
                                    {
                                        SiguupModel.ALang = "E";
                                    }
                                    var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;
                                    string month = selectedItem[1].ToString();
                                    string day = selectedItem[0].ToString();
                                    string year = selectedItem[2].ToString();
                                    SiguupModel.ABirthdt = year + "-" + month + "-" + day + "T00:00:00";
                                    SiguupModel.AType = "1";
                                    SiguupModel.AFirstname = viewModel.TxtName;
                                    SiguupModel.ALastname = ".";
                                    if (viewModel.IsTIN)
                                    {
                                        SiguupModel.ATin = viewModel.TxtTIN;
                                        SiguupModel.ATinExist = "X";
                                    }
                                    else
                                    {
                                        SiguupModel.ATin = "";
                                        SiguupModel.ATinExist = "";
                                    }
                                    SiguupModel.AIdnumber = viewModel.TxtIDNumber;
                                    if (viewModel.IsCRChecked == true)
                                    {
                                        SiguupModel.ACommId = viewModel.TxtCRNumber;
                                        SiguupModel.ALicenceNo = "";
                                        SiguupModel.AIssuedBy = "";
                                        SiguupModel.ACity = "";
                                        SiguupModel.ACityCode = "";
                                    }
                                    else
                                    {
                                        SiguupModel.ALicenceNo = viewModel.TxtLicenseNumber;
                                        SiguupModel.AIssuedBy = viewModel.SelectedIssuedBy.elementCode;
                                        try
                                        {
                                            if (viewModel.SelectCityList != null && viewModel.SelectCityList.CityName != null)
                                            {
                                                SiguupModel.ACity = viewModel.SelectCityList.CityName;
                                                SiguupModel.ACityCode = viewModel.SelectCityList.CityCode;
                                            }
                                            else
                                            {
                                                SiguupModel.ACity = string.Empty;
                                                SiguupModel.ACityCode = string.Empty;

                                            }
                                            SiguupModel.ACommId = "";
                                        }
                                        catch (Exception ex)
                                        {
                                        }
                                        //SiguupModel.ACity = viewModel.SelectCityList.CityName;
                                        //SiguupModel.ACityCode = viewModel.SelectCityList.CityCode;
                                        SiguupModel.ACommId = "";
                                    }
                                    SiguupModel.AEmail = viewModel.TxtEmailAddress;

                                    if (!viewModel.TxtPhoneNumber.Contains(string.Empty))
                                    {
                                        SiguupModel.APhone = "00966" + viewModel.TxtPhoneNumber;
                                    }
                                    else
                                    {
                                        SiguupModel.APhone = "";

                                    }

                                    string newCountryCodeString = viewModel.TxtCountryCode.Replace("+", "00");
                                    SiguupModel.AMobile = newCountryCodeString + viewModel.TxtMobileNumber;
                                    SiguupModel.ACountry = viewModel.MobileCountryCode;

                                    // SiguupModel.AMobile = "00966" + viewModel.TxtMobileNumber;

                                    if (viewModel.SelectedSignUpUsing.ID == 1)
                                    {
                                        SiguupModel.AIdtype = "ZS0001";
                                    }
                                    else if (viewModel.SelectedSignUpUsing.ID == 2)
                                    {
                                        SiguupModel.AIdtype = "ZS0002";
                                    }
                                    else if (viewModel.SelectedSignUpUsing.ID == 3)
                                    {
                                        SiguupModel.AIdtype = "ZS0003";
                                    }
                                    SiguupModel.CaseGuid = ResutGuid.d.results[0].CaseGuid;
                                    string ResultFirstSubmit = WebServiceManager.GAZTSignUpFirstSubmit(SiguupModel);
                                    SignUpModelRootObject ResultFirstSubmitModel = JsonConvert.DeserializeObject<SignUpModelRootObject>(ResultFirstSubmit);
                                    viewModel.SignUpFirstSubmitModel = ResultFirstSubmitModel;
                                    if (ResultFirstSubmitModel.d == null)
                                    {
                                        SignupErrorModelRootObject SignupErrorModelRootObjectModel = JsonConvert.DeserializeObject<SignupErrorModelRootObject>(ResultFirstSubmit);
                                        StringBuilder Message = new StringBuilder();
                                        foreach (SignupErrorModelErrordetail itemerror in SignupErrorModelRootObjectModel.error.innererror.errordetails)
                                        {
                                            if (itemerror.code.Contains("ZD_PUSR"))
                                            {
                                                if (Message.Length > 0)
                                                {
                                                    Message.Append(Environment.NewLine);
                                                }
                                                Message.Append(itemerror.message);
                                            }
                                        }
                                        viewModel._dialogService.ShowMessage(Message.ToString(), AppResources.Information);
                                    }
                                    else
                                    {
                                    NavigateToVerifyOTPScreenAsync(ResultFirstSubmitModel);
                                     // viewModel._navigationService.NavigateTo(App.CreateGaztAccountPageView, ResultFirstSubmitModel);
                                    }
                                }
                                else if (ResultDuplicateCR.d.Flag == "X")
                                {
                                    var result = false;

                                    if (App.IsArabic)
                                    {

                                        result = await Xamarin.Forms.Application.Current.MainPage.DisplayAlert
                                                                        (AppResources.Alerts, AppResources.ZZZCRValidateMessg,
                                                                            AppResources.ZZZNoText, AppResources.ZZZYesText);
                                        if (result == true)
                                        {
                                            viewModel.IsLoading = false;

                                            return;

                                        }
                                        else // if it's equal to YES
                                        {
                                            CRDuplicateCheck();
                                        }
                                    }
                                    else
                                    {

                                        result = await Xamarin.Forms.Application.Current.MainPage.DisplayAlert
                                                                       (AppResources.Alerts, AppResources.ZZZCRValidateMessg,
                                                                           AppResources.ZZZYesText, AppResources.ZZZNoText);

                                        if (result == true)
                                        {
                                            CRDuplicateCheck();
                                            // _navigationService.GoBack();

                                        }
                                        else // if it's equal to NO
                                        {
                                            viewModel.IsLoading = false;

                                            return; // just return to the page and do nothing.
                                        }
                                    }

                                    //viewModel._dialogService.ShowMessage(AppResources.ZZZCRValidateMessg, AppResources.Information);


                                }
                                else
                                {
                                    await viewModel._dialogService.ShowMessage(AppResources.ZZYoushouldsignupasnewuser, AppResources.Information);
                                }
                            }

                            catch (GAZTException gex)
                            {
                                // Handle the GAZT custom exception.
                                string MessageForTheUser = gex.Message;

                                if (gex is GAZTNetworkConnectivityIssueException)
                                {
                                    MessageForTheUser = AppResources.NetworkConnectivityIssue;
                                }
                                else if (gex is GAZTInternetException)
                                {
                                    MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                                }
                                else if (gex is GAZTSessionExpiredException)
                                {
                                    MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                                }

                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    viewModel.IsLoading = false;

                                    await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                    viewModel._navigationService.GoBack();
                                });
                            }
                            catch (InternetException ex)
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                });
                            }
                            catch (HttpRequestException ex)
                            {
                                string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    // IsLoading = false;

                                    await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                    //_navigationService.GoBack();
                                });
                            }
                            catch (Exception ex)
                            {

                                string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    // IsLoading = false;

                                    await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                    //_navigationService.GoBack();
                                });
                            }
                        }
                        else
                        {
                            try
                            {
                                CaseGuidModelRootObject ResutGuid = WebServiceManager.GAZTGetSignupGuid();
                                SignUpNextBodyModel SiguupModel = new SignUpNextBodyModel();
                                if (App.IsArabic)
                                {
                                    SiguupModel.ALang = "A";
                                }
                                else
                                {
                                    SiguupModel.ALang = "E";
                                }
                                var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;
                                string month = selectedItem[1].ToString();
                                string day = selectedItem[0].ToString();
                                string year = selectedItem[2].ToString();
                                SiguupModel.ABirthdt = year + "-" + month + "-" + day + "T00:00:00";
                                SiguupModel.AType = "1";
                                SiguupModel.AFirstname = viewModel.TxtName;
                                SiguupModel.ALastname = ".";
                                if (viewModel.IsTIN)
                                {
                                    SiguupModel.ATin = viewModel.TxtTIN;
                                    SiguupModel.ATinExist = "X";
                                }
                                else
                                {
                                    SiguupModel.ATin = "";
                                    SiguupModel.ATinExist = "";
                                }
                                SiguupModel.AIdnumber = viewModel.TxtIDNumber;
                                if (viewModel.IsCRChecked == true)
                                {
                                    SiguupModel.ACommId = viewModel.TxtCRNumber;
                                    SiguupModel.ALicenceNo = "";
                                    SiguupModel.AIssuedBy = "";
                                    SiguupModel.ACity = "";
                                    SiguupModel.ACityCode = "";
                                }
                                else
                                {
                                    SiguupModel.ALicenceNo = viewModel.TxtLicenseNumber;
                                    SiguupModel.AIssuedBy = viewModel.SelectedIssuedBy.elementCode;
                                    try
                                    {
                                        if (viewModel.SelectCityList != null && viewModel.SelectCityList.CityName != null)
                                        {
                                            SiguupModel.ACity = viewModel.SelectCityList.CityName;
                                            SiguupModel.ACityCode = viewModel.SelectCityList.CityCode;
                                        }
                                        else
                                        {
                                            SiguupModel.ACity = string.Empty;
                                            SiguupModel.ACityCode = string.Empty;

                                        }
                                        SiguupModel.ACommId = "";
                                    }
                                    catch (Exception ex)
                                    {
                                    }
                                }
                                SiguupModel.AEmail = viewModel.TxtEmailAddress;
                                if (!viewModel.TxtPhoneNumber.Contains(string.Empty))
                                {
                                    SiguupModel.APhone = "00966" + viewModel.TxtPhoneNumber;
                                }
                                else
                                {
                                    SiguupModel.APhone = "";

                                }

                                string newCountryCodeString = viewModel.TxtCountryCode.Replace("+", "00");
                                SiguupModel.AMobile = newCountryCodeString + viewModel.TxtMobileNumber;
                                SiguupModel.ACountry = viewModel.MobileCountryCode;

                                // SiguupModel.AMobile = "00966" + viewModel.TxtMobileNumber;

                                if (viewModel.SelectedSignUpUsing.ID == 1)
                                {
                                    SiguupModel.AIdtype = "ZS0001";
                                }
                                else if (viewModel.SelectedSignUpUsing.ID == 2)
                                {
                                    SiguupModel.AIdtype = "ZS0002";
                                }
                                else if (viewModel.SelectedSignUpUsing.ID == 3)
                                {
                                    SiguupModel.AIdtype = "ZS0003";
                                }
                                SiguupModel.CaseGuid = ResutGuid.d.results[0].CaseGuid;
                                string ResultFirstSubmit = WebServiceManager.GAZTSignUpFirstSubmit(SiguupModel);
                                SignUpModelRootObject ResultFirstSubmitModel = JsonConvert.DeserializeObject<SignUpModelRootObject>(ResultFirstSubmit);
                                viewModel.SignUpFirstSubmitModel = ResultFirstSubmitModel;
                                if (ResultFirstSubmitModel.d == null)
                                {
                                    SignupErrorModelRootObject SignupErrorModelRootObjectModel = JsonConvert.DeserializeObject<SignupErrorModelRootObject>(ResultFirstSubmit);
                                    StringBuilder Message = new StringBuilder();
                                    foreach (SignupErrorModelErrordetail itemerror in SignupErrorModelRootObjectModel.error.innererror.errordetails)
                                    {
                                        if (itemerror.code.Contains("ZD_PUSR"))
                                        {
                                            if (Message.Length > 0)
                                            {
                                                Message.Append(Environment.NewLine);
                                            }
                                            Message.Append(itemerror.message);
                                        }
                                    }
                                    viewModel._dialogService.ShowMessage(Message.ToString(), AppResources.Information);
                                }
                                else
                                {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    viewModel.IsLoading = true;

                                    
                                });
                                NavigateToVerifyOTPScreenAsync(ResultFirstSubmitModel);
                                  //viewModel._navigationService.NavigateTo(App.CreateGaztAccountPageView, ResultFirstSubmitModel);
                                }
                            }
                            catch (GAZTException gex)
                            {
                                // Handle the GAZT custom exception.
                                string MessageForTheUser = gex.Message;
                                if (gex is GAZTInvalidDataException)
                                {
                                    MessageForTheUser = AppResources.ZZSomethingwentwrong;
                                }
                                if (gex is GAZTNetworkConnectivityIssueException)
                                {
                                    MessageForTheUser = AppResources.NetworkConnectivityIssue;
                                }
                                else if (gex is GAZTInternetException)
                                {
                                    MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                                }
                                else if (gex is GAZTSessionExpiredException)
                                {
                                    MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                                }

                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    viewModel.IsLoading = false;

                                    await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                    viewModel._navigationService.GoBack();
                                });
                            }
                            catch (InternetException ex)
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                });
                            }
                            catch (HttpRequestException ex)
                            {
                                string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    // IsLoading = false;

                                    await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                    //_navigationService.GoBack();
                                });
                            }
                            catch (Exception ex)
                            {

                                string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    // IsLoading = false;

                                    await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                    //_navigationService.GoBack();
                                });
                            }
                        }
                    }
                    catch (InternetException ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                        });
                    }
                }
           
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            });
        }

        private void EntryLicenceNumber_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(viewModel.TxtLicenseNumber))
            {
                FrmLicenseNumber.HasError = true;
                viewModel.IsAllValidDataEntered = false;

                // IsNextValid = false;
            }
            else if(viewModel.TxtLicenseNumber.Length < 15)
            {
                FrmLicenseNumber.HasError = true;
                viewModel.IsAllValidDataEntered = false;

            }
            else
            {
                viewModel.IsAllValidDataEntered = true;

                FrmLicenseNumber.HasError = false;
            }

        }
    }

}