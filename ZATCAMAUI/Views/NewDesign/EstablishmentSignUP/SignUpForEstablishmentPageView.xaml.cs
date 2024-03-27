using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentSignUPVM;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.Views.NewDesign.TaxpayerCorrespondancePages;
using ZATCAMAUI.Core.Enums;
using Syncfusion.Maui.Picker;
using Application = Microsoft.Maui.Controls.Application;
using ZATCAMAUI.Views.SyncFusionEnabledViews.VATIndividualSignupPage;
using ZATCAMAUI.Core.Mangers;
using RGPopup.Maui.Services;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Core.Exceptions;

namespace ZATCAMAUI.Views.NewDesign.EstablishmentSignUP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpForEstablishmentPageView : ContentPage
    {
        SignUpForEstablishmentPageViewModel viewModel;
        ObservableCollection<InternationalMobileData> mobileData = null;
        bool IsTermsAndConditionPage = true;

        public SignUpForEstablishmentPageView()
        {
            try
            {
                InitializeComponent();
                viewModel = App.Locator.SignUpForEstablishmentPageView;
                BindingContext = viewModel;
                viewModel.IsAllValidDataEntered = false;
                viewModel.IsAllValidCRNumberEntered = false;
                viewModel.IsDeclarationCheckedForInstruction = false;
                IsTermsAndConditionPage = true;

                ChangeAeroIcon();
                SetLTR();
                ClearFields();
                SetPickerFont();
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

                if (Device.RuntimePlatform == Device.iOS)
                {
                    string baseUrl = DependencyService.Get<IBaseUrl>().Get();
                    string path = DependencyService.Get<IBaseUrl>().Get();
                    if (!App.IsArabic)
                    {
                        string url = Path.Combine(path, "TermsAndConditionsEN.html");
                        TCWebView.Source = url;

                    }
                    else
                    {
                        string url = Path.Combine(path, "TermsAndConditionsAR.html");
                        TCWebView.Source = url;
                    }
                }
                else
                {
                    if (!App.IsArabic)
                    {
                        TCWebView.Source = "file:///android_asset/TermsAndConditionsEN.html";

                    }
                    else
                    {
                        TCWebView.Source = "file:///android_asset/TermsAndConditionsAR.html";
                    }
                }

                _ = viewModel.GetCaptchAndGUID();

            }
            catch (Exception )
            {

            }
        }

        public void SetPickerFont()
        {
            try
            {
                switch (Device.RuntimePlatform)
                {

                    case Device.iOS:
                        {
                            IDTypePicker.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            IDTypePicker.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            IDTypePicker.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            IDTypePicker.TextStyle.FontFamily = "Somar-SemiBold";


                            ddlLIssuedBy.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            ddlLIssuedBy.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            ddlLIssuedBy.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            ddlLIssuedBy.TextStyle.FontFamily = "Somar-SemiBold";

                            ddlLIssuedByCity.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            ddlLIssuedByCity.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            ddlLIssuedByCity.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            ddlLIssuedByCity.TextStyle.FontFamily = "Somar-SemiBold";

                            DpDbo.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            DpDbo.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            DpDbo.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            DpDbo.TextStyle.FontFamily = "Somar-SemiBold";

                            DpDboHijri.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            DpDboHijri.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            DpDboHijri.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            DpDboHijri.TextStyle.FontFamily = "Somar-SemiBold";

                        }
                        break;

                    case Device.Android:

                        IDTypePicker.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        IDTypePicker.ColumnHeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        IDTypePicker.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        IDTypePicker.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";


                        ddlLIssuedBy.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        ddlLIssuedBy.ColumnHeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        ddlLIssuedBy.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        ddlLIssuedBy.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";

                        ddlLIssuedByCity.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        ddlLIssuedByCity.ColumnHeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        ddlLIssuedByCity.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        ddlLIssuedByCity.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";

                        DpDbo.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        DpDbo.ColumnHeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        DpDbo.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        DpDbo.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";

                        DpDboHijri.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        DpDboHijri.ColumnHeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        DpDboHijri.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        DpDboHijri.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";

                        break;
                }
            }
            catch (Exception )
            {


            }

        }

        private void Closed_Tapped(object sender, EventArgs e)
        {
            // PopupNavigation.Instance.PopAsync();
            viewModel._navigationService.GoBack();
        }

        public async Task loadPageData()
        {
            try
            {
                viewModel.SetDefaultDate();

                //ClearFields();
                viewModel.OnPageLoad();
                viewModel.SetIssueIdList();
                await viewModel.SetCityList();


                viewModel.PopulateDataInChips();
                ChipGroup_statusFilter.SelectedItem = viewModel.ChipDataFilterlist.Where(x => x.TemplateType == AppResources.NDGregorian).FirstOrDefault();
                viewModel.IsHijriCal = false;

            }
            catch (Exception )
            {

            }

            viewModel.PickerDobToDisplay = string.Empty;
        }

        private void LIssuedBy_Clicked(object sender, TappedEventArgs e)
        {
            ddlLIssuedBy.IsOpen = true;
        }

        private void ddlLIssuedBy_OkButtonClicked(object sender, PickerSelectionChangedEventArgs e)
        {
            if (viewModel.IssuedByList[ddlLIssuedBy.Columns[0].SelectedIndex] != null)
            {
                IssuedByResponse issuedByResponse = viewModel.IssuedByList[ddlLIssuedBy.Columns[0].SelectedIndex];
                //ddlLIssuedBy.SelectedItem = issuedByResponse;
                viewModel.SelectedIssuedBy = issuedByResponse;
                viewModel.SelectedIssuedByPrev = issuedByResponse;
                viewModel.TxtLOrCIssuedBy = issuedByResponse.txt50;
                viewModel.IssuedByTapped = true;
            }
        }

        private void LIssuedByCity_Clicked(object sender, TappedEventArgs e)
        {
            ddlLIssuedByCity.IsOpen = true;
        }

        public void ClearFields()
        {

            viewModel.PageTitle = AppResources.ZVatTermsAndConditions;
            viewModel.BodyText = "";
            viewModel.NextBTN = AppResources.ZZProceedtoindividualSignup;
            viewModel.CurrentTab = EstablishmentSignUPTabEnum.TermsAndConditions;

            viewModel.PkrDBO = string.Empty;
            viewModel.PickerDobToDisplay = string.Empty;
            viewModel.TxtLOrCIssuedBy = string.Empty;
            viewModel.TxtLOrCIssuedByCity = string.Empty;
            viewModel.IDTypeIndex = 0;
            viewModel.SelectedSignUpUsing = null;
            viewModel.SignUpUsingList = null;
            viewModel.IssuedByList = null;
            viewModel.TxtTIN = string.Empty;
            viewModel.TxtIDNumber = string.Empty;
            viewModel.TxtName = string.Empty;
            viewModel.TxtCRNumber = string.Empty;
            viewModel.TxtLicenseNumber = string.Empty;
            viewModel.TxtEmailAddress = string.Empty;
            viewModel.TxtCountryCode = "+966";
            viewModel.TxtConfirmPassword = string.Empty;
            viewModel.TxtPassword = string.Empty;
            ResetPasswordValidationConditions();
            viewModel.TxtMobileNumber = string.Empty;
            viewModel.TxtMobileNumberwithCountryCode = string.Empty;
            viewModel.TxtPhoneNumber = string.Empty;
            viewModel.ImgBackgroundCRNubmer = "vat_tile_listofsignup_W";
            viewModel.ImgBackgroundLicenseNubmer = "vat_tile_listofsignup_W";
            viewModel.IsCRChecked = true;
            viewModel.TxtLicenseNumber = string.Empty;
            viewModel.SignUpFirstSubmitModel = null;
            viewModel.IsNextButtonEnable = true;

            viewModel.OTPFirstDigit = string.Empty;
            viewModel.OTPSecondDigit = string.Empty;
            viewModel.OTPThirdDigit = string.Empty;
            viewModel.OTPFourthDigit = string.Empty;

            viewModel.MOTPFirstDigit = string.Empty;
            viewModel.MOTPSecondDigit = string.Empty;
            viewModel.MOTPThirdDigit = string.Empty;
            viewModel.MOTPFourthDigit = string.Empty;
            viewModel.IsHijriCal = false;
        }

        private void SetLTR()
        {
            if (App.IsArabic)
            {

                //FlowDirection = FlowDirection.RightToLeft;
                CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                SfPickerResources.ResourceManager = new ResourceManager("ZATCAMAUI.SyncfusionControl", Application.Current.GetType().Assembly);
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                //FlowDirection = FlowDirection.LeftToRight;
                CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                SfPickerResources.ResourceManager = new ResourceManager("ZATCAMAUI.AppResources", Application.Current.GetType().Assembly);
            }
        }

        public void ChangeAeroIcon()
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

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await loadPageData();
            //var safeInsets = On<iOS>().SafeAreaInsets();
            //safeInsets.Bottom = -10;
            //Padding = safeInsets;

            if (Device.RuntimePlatform == Device.Android)
            {
                IDTypePicker.Background = (Color)Application.Current.Resources["PickerBgGray"];
                ddlLIssuedBy.Background = (Color)Application.Current.Resources["PickerBgGray"];
                ddlLIssuedByCity.Background = (Color)Application.Current.Resources["PickerBgGray"];
            }
            else
            {
                IDTypePicker.Background = (Color)Application.Current.Resources["White"];
                ddlLIssuedBy.Background = (Color)Application.Current.Resources["White"];
                ddlLIssuedByCity.Background = (Color)Application.Current.Resources["White"];
            }

            MessagingCenter.Subscribe<InternationalCodeSearchPage, string>(this, "SelectedItem", (sender, arg) =>
            {
                viewModel.TxtMobileNumber = string.Empty;
                // IntnlCodes.Text = arg;
                viewModel.TxtCountryCode = arg;
            });
            MessagingCenter.Subscribe<InternationalCodeSearchPage, string>(this, "SelectedCountryCode", (sender, arg) =>
            {

                viewModel.MobileCountryCode = arg;
            });

            try
            {
                mobileData = WebServiceManager.GAZTGetMobileRegionDropdown();
            }
            catch (Exception)
            {



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

        private void OnDOBClicked(object sender, TappedEventArgs e)
        {
            if (!viewModel.IsHijriCal)
            {
                DpDbo.IsOpen = true;
            }
            else
            {
                DpDboHijri.IsOpen = true;
            }
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
                                    // viewModel._dialogService.ShowMessage(AppResources.ZZPleaseentervalidCRnumber, AppResources.Information);
                                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseentervalidCRnumber));
                                    EntryCRNumber.Text = string.Empty;
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
                    catch (InternetException)
                    {

                        //  viewModel._dialogService.ShowMessage(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZInternetConnectionMessage));
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
                    //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZCommercialReiterationNumbershouddbe10digits));
                    FrmCR.HasError = true;
                    EntryCRNumber.Text = string.Empty;

                    //  EntryCRNumber.Focus();
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

        }

        private void EntryCRNumber_Unfocused(object sender, FocusEventArgs e)
        {
            viewModel.IsLoading = true;
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
                                    viewModel.IsAllValidCRNumberEntered = false;
                                    viewModel.TxtCRNumber = string.Empty;
                                    // viewModel._dialogService.ShowMessage(AppResources.ZZPleaseentervalidCRnumber, AppResources.Information);
                                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseentervalidCRnumber));
                                }
                                else
                                {
                                    FrmCR.HasError = false;
                                    viewModel.IsAllValidCRNumberEntered = true;
                                }
                            }
                        }
                    }
                    catch (InternetException)
                    {
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZInternetConnectionMessage));
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
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZCommercialReiterationNumbershouddbe10digits));
                    FrmCR.HasError = true;
                    viewModel.IsAllValidCRNumberEntered = false;
                    EntryCRNumber.Text = string.Empty;
                    // EntryCRNumber.Focus();
                }
            }
            else
            {
                if (viewModel.IsCRChecked == false)
                {
                    viewModel.IsAllValidCRNumberEntered = true;
                }
            }
            viewModel.IsLoading = false;

        }

        private void EntryEmail_TextChanged(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryEmail.Text))
            {
                bool flag = IsValid(EntryEmail.Text);
                if (!flag)
                {
                    PopUp popUp = new PopUp();
                    popUp.Message = AppResources.ZZPleaseenteravalidEmailAddress;
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
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseenteravalidEmailAddress));
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
                    FrmPhoneNumber.HasError = true;

                }
                else
                {
                    FrmPhoneNumber.HasError = false;

                }
            }
            else
            {
                if (!string.IsNullOrEmpty(EntryPhoneNumber.Text))
                {
                    viewModel.IsAllValidContactDataEnteredPhoneNbr = true;
                }
            }
        }

        private void EntryPhoneNumber_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(EntryPhoneNumber.Text))
            {
                viewModel.IsAllValidContactDataEnteredPhoneNbr = true;
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
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));
                    FrmPhoneNumber.HasError = true;

                    EntryPhoneNumber.Text = string.Empty;
                }
                else
                {
                    viewModel.IsAllValidContactDataEnteredPhoneNbr = true;
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
                    Message.AppendLine(AppResources.ZZMobilenumberCannotStartWith0 + " ");
                }
                if (viewModel.TxtCountryCode == "+966")
                {
                    if (EntryMobileNumber.Text.Substring(0, 1) != "5")
                    {
                        Message.AppendLine(AppResources.ZZMobilenumberhastostartwithnumber5);
                    }
                }
                if (EntryMobileNumber.Text.Length < 9)
                {


                    Message.AppendLine(AppResources.ZZMobilenumberlengthcannotbelessthan9digits);
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
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));
                    viewModel.IsAllValidContactDataEnteredMobileNbr = false;

                    EntryMobileNumber.Text = string.Empty;
                }
                else
                {
                    viewModel.IsAllValidContactDataEnteredMobileNbr = true;
                    viewModel.TxtMobileNumberwithCountryCode = "(" + viewModel.TxtCountryCode + ")" + " " + EntryMobileNumber.Text;
                }
            }
            else
            {
                Message.AppendLine(AppResources.EnterMobileNumber);
                popUp.Message = Message.ToString();
                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));
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

        private void OnIDTypeClicked(object sender, TappedEventArgs e)
        {
            IDTypePicker.IsOpen = true;
        }

        private void IDTypePicker_SelectionChanged(object sender, PickerSelectionChangedEventArgs e)
        {
            SignUpUsing signUpUsing = viewModel.SignUpUsingList[e.NewValue];
            viewModel.SelectedSignUpUsing = signUpUsing;
            viewModel.TxtIDType = signUpUsing.SUType;
            viewModel.TxtIDNumber = string.Empty;
            if (IsTermsAndConditionPage == false)
            {
                //EntryIDNumber.Focus();
            }
            IsTermsAndConditionPage = false;
            //IDTypePicker.IsOpen = false;

        }

        private void EntryIDNumber_Unfocused(object sender, FocusEventArgs e)
        {
            PopUp popUp = new PopUp();
            StringBuilder Messages = new StringBuilder();
            if (!string.IsNullOrEmpty(EntryIDNumber.Text) && !string.IsNullOrEmpty(viewModel.PickerDobToDisplay))
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
                            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZNationalIDstartswith1));
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
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));
                                FrmIDNumber.HasError = true;
                                viewModel.IsAllValidDataEntered = false;
                                EntryName.Text = string.Empty;
                            }
                            else
                            {

                                viewModel.IsAllValidDataEntered = true;
                                FrmIDNumber.HasError = false;
                                if (!string.IsNullOrEmpty(viewModel.PkrDBO))
                                {
                                    ValidateIDNumber();
                                }

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
                            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZIqamaIDstartswith2));
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
                                // PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));
                                FrmIDNumber.HasError = true;
                                viewModel.IsAllValidDataEntered = false;
                                EntryName.Text = string.Empty;
                            }
                            else
                            {
                                // viewModel.IsAllValidDataEntered = true;
                                FrmIDNumber.HasError = false;
                                if (!string.IsNullOrEmpty(viewModel.PkrDBO))
                                {
                                    ValidateIDNumber();
                                }
                            }
                        }
                    }
                    else if (viewModel.SelectedSignUpUsing.ID == 3)
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
                            flag = false;
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
                            //  PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                            flag = false;
                            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit));
                            FrmIDNumber.HasError = true;
                            viewModel.IsAllValidDataEntered = false;
                            EntryName.Text = string.Empty;
                            // EntryIDNumber.Text = string.Empty;//ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit
                        }
                        if (flag)
                        {
                            FrmIDNumber.HasError = false;
                            if (!string.IsNullOrEmpty(viewModel.PkrDBO))
                            {
                                ValidateIDNumber();
                            }
                        }
                    }

                    else
                    {
                        // ValidateIDNumber();
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

        public async void ValidateIDNumber()
        {
            // viewModel.IsAllValidDataEntered = true;
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });
            });
            string DBO = string.Empty;
            if (viewModel.IsHijriCal)
            {

                var selectedItem = DpDboHijri.SelectedItem as ObservableCollection<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                viewModel.PkrDBO = year + "/" + month + "/" + day;
                DBO = year + month + day;
            }
            else
            {
                var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                viewModel.PkrDBO = year + "/" + month + "/" + day;
                DBO = year + month + day;
            }

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
                                // viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.IsAllValidDataEntered = false;
                                FrmIDNumber.HasError = false;
                                // viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                        }
                        else
                        {
                            viewModel.TxtName = SignupIsIDTypeValid.d.Name1 + " " + SignupIsIDTypeValid.d.FatherName + " " + SignupIsIDTypeValid.d.FamilyName;
                            //EntryName.IsEnabled = false;
                            viewModel.IsAllValidDataEntered = true;
                            FrmIDNumber.HasError = false;


                            if (viewModel.IsTIN)
                            {
                                if (string.IsNullOrEmpty(SignupIsIDTypeValid.d.Tin))
                                {
                                    viewModel.TxtTIN = string.Empty;
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDYoushouldsignupasnewuser));
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(viewModel.TxtTIN))
                                    {
                                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseentertheTINNumber));
                                    }
                                }


                            }
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
                                //   viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.IsAllValidDataEntered = false;
                                FrmIDNumber.HasError = true;
                                //  viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
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

                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;

                                //  await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                //viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                // viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                                await Task.Run(() =>
                                {
                                    viewModel.IsLoading = false;
                                });
                            });
                        }
                        catch (HttpRequestException)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                //  await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                //_navigationService.GoBack();
                            });
                        }
                        catch (Exception)
                        {

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                // await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
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
                                //  viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.IsAllValidDataEntered = false;
                                FrmIDNumber.HasError = true;
                                // viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                        }
                        else
                        {
                            viewModel.IsAllValidDataEntered = true;
                            viewModel.TxtName = SignupIsIDTypeValid.d.Name1 + " " + SignupIsIDTypeValid.d.FatherName + " " + SignupIsIDTypeValid.d.FamilyName;
                            // EntryName.IsEnabled = false;
                            FrmIDNumber.HasError = false;
                            if (viewModel.IsTIN)
                            {
                                if (string.IsNullOrEmpty(SignupIsIDTypeValid.d.Tin))
                                {
                                    viewModel.TxtTIN = string.Empty;
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDYoushouldsignupasnewuser));
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(viewModel.TxtTIN))
                                    {
                                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseentertheTINNumber));
                                    }
                                }


                            }
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
                                // viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.IsAllValidDataEntered = false;
                                FrmIDNumber.HasError = false;
                                // viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
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

                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;

                                // await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                // viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                // viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                                await Task.Run(() =>
                                {
                                    viewModel.IsLoading = false;
                                });
                            });
                        }
                        catch (HttpRequestException)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                // await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                //_navigationService.GoBack();
                            });
                        }
                        catch (Exception)
                        {

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                //_navigationService.GoBack();
                            });
                        }
                    }
                }
            }
            if (viewModel.SelectedSignUpUsing.ID == 3)
            {
                EntryName.IsEnabled = true;
                if (!string.IsNullOrEmpty(viewModel.TxtIDNumber))
                {
                    try
                    {
                        string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0003", viewModel.TxtIDNumber, DBO);
                        IDTypeModelRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeModelRootObject>(Result);
                        if (SignupIsIDTypeValid.d == null)
                        {
                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                viewModel.IsAllValidDataEntered = false;
                                FrmIDNumber.HasError = true;
                                //  viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.IsAllValidDataEntered = false;
                                FrmIDNumber.HasError = true;
                                // viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                        }
                        else
                        {
                            viewModel.IsAllValidDataEntered = true;
                            viewModel.TxtName = SignupIsIDTypeValid.d.Name1 + " " + SignupIsIDTypeValid.d.FatherName + " " + SignupIsIDTypeValid.d.FamilyName;
                            // EntryName.IsEnabled = false;
                            FrmIDNumber.HasError = false;
                            if (viewModel.IsTIN)
                            {
                                if (string.IsNullOrEmpty(SignupIsIDTypeValid.d.Tin))
                                {
                                    viewModel.TxtTIN = string.Empty;
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDYoushouldsignupasnewuser));
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(viewModel.TxtTIN))
                                    {
                                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseentertheTINNumber));
                                    }
                                }


                            }
                        }
                    }
                    catch
                    {
                        try
                        {
                            string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0003", viewModel.TxtIDNumber, DBO);
                            IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            {
                                viewModel.IsAllValidDataEntered = false;
                                FrmIDNumber.HasError = true;
                                // viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.IsAllValidDataEntered = false;
                                FrmIDNumber.HasError = false;
                                // viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
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

                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;

                                // await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                // viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                // viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                                await Task.Run(() =>
                                {
                                    viewModel.IsLoading = false;
                                });
                            });
                        }
                        catch (HttpRequestException)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                // await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                //_navigationService.GoBack();
                            });
                        }
                        catch (Exception)
                        {

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                //_navigationService.GoBack();
                            });
                        }
                    }
                }
            }
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            });
        }

        private void DpDbo_CancelButtonClicked(object sender, EventArgs e)
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

        private void ddlLIssuedByCity_OkButtonClicked(object sender, PickerSelectionChangedEventArgs e)
        {
            if (viewModel.CityList[ddlLIssuedByCity.Columns[0].SelectedIndex] != null)
            {
                SignupCityResult selectedcity = viewModel.CityList[ddlLIssuedByCity.Columns[0].SelectedIndex];
                //ddlLIssuedByCity.SelectedItem = selectedcity;
                viewModel.SelectCityList = selectedcity;
                viewModel.SelectCityListPrev = selectedcity;
                viewModel.TxtLOrCIssuedByCity = selectedcity.CityName;
                viewModel.IssuedByCityTapped = true;
            }
        }
      
        private void DpDbo_Closed(object sender, EventArgs e)
        {
            try
            {
                if (viewModel.IsHijriCal)
                {
                    if (DpDboHijri.SelectedItem != null)
                    {
                        var selectedItem = DpDboHijri.SelectedItem as ObservableCollection<object>;
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
                        viewModel.PkrDBO = year + "/" + month + "/" + day;
                        //viewModel.PickerDobToDisplay = day + "/" + month + "/" + year;
                        viewModel.PickerDobToDisplay = year + "/" + month + "/" + day;

                        if (!string.IsNullOrEmpty(viewModel.PkrDBO))
                        {

                            var ConvertedDate = UtilityManager.HijriToGreg(viewModel.PkrDBO);
                            var ConvertedDateArray = ConvertedDate.Split('/');



                            ObservableCollection<object> todaycollection = new ObservableCollection<object>();
                            //Select today dates
                            todaycollection.Add(ConvertedDateArray[2].ToString());
                            todaycollection.Add(ConvertedDateArray[1].ToString());//day
                            todaycollection.Add(ConvertedDateArray[0].ToString());

                            viewModel.SelectedGregDate = ConvertedDateArray[2] + "/" + ConvertedDateArray[1] + "/" + ConvertedDateArray[0];


                            DpDbo.SelectedItem = todaycollection;
                        }

                    }
                }
                else
                {
                    if (DpDbo.SelectedItem != null)
                    {
                        var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
                        viewModel.PkrDBO = year + "/" + month + "/" + day;
                        //viewModel.PickerDobToDisplay = day + "/" + month + "/" + year;
                        viewModel.PickerDobToDisplay = year + "/" + month + "/" + day;


                        if (!string.IsNullOrEmpty(viewModel.PkrDBO))
                        {

                            var ConvertedDate = UtilityManager.ConvertToHijri(viewModel.PkrDBO);
                            var ConvertedDateArray = ConvertedDate.Split('/');



                            ObservableCollection<object> todaycollection = new ObservableCollection<object>();
                            //Select today dates
                            todaycollection.Add(ConvertedDateArray[2].ToString());
                            todaycollection.Add(ConvertedDateArray[1].ToString());//day
                            todaycollection.Add(ConvertedDateArray[0].ToString());

                            DpDboHijri.SelectedItem = todaycollection;

                            viewModel.SelectedHijriDate = ConvertedDateArray[2] + "/" + ConvertedDateArray[1] + "/" + ConvertedDateArray[0];
                        }

                    }
                }


            }
            catch (Exception)
            {
            }
            if (!string.IsNullOrEmpty(viewModel.PkrDBO))
            {
                if (!string.IsNullOrEmpty(viewModel.TxtIDNumber))
                {
                    ValidateIDNumber();
                }

            }
        }

        private void GAZTBorderlessEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryIDNumber.Text))
            {
                FrmIDNumber.HasError = false;
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
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));
                    viewModel.IsAllValidDataEntered = false;
                    FrmTIN.HasError = true;
                    EntryTIN.Text = string.Empty;
                }
                else
                {

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


        private void OnYesTapped(object sender, TappedEventArgs e)
        {
            viewModel.IsTIN = true;
            viewModel.ImgBackgroundNo = "vat_tile_listofsignup_W";
            viewModel.ImgBackgroundYes = "re_Tile_Background";
            viewModel.TxtTIN = string.Empty;
            EntryTIN.Focus();
            //EntryTIN.Unfocus();

        }

        private void OnNoTapped(object sender, TappedEventArgs e)
        {
            viewModel.IsTIN = false;
            viewModel.ImgBackgroundNo = "re_Tile_Background";
            viewModel.ImgBackgroundYes = "vat_tile_listofsignup_W";
            EntryTIN.Text = string.Empty;
            viewModel.TxtTIN = string.Empty;
        }

        private void OnCRNumberTapped(object sender, TappedEventArgs e)
        {
            viewModel.ImgBackgroundCRNubmer = "FP_selected_tile";
            viewModel.ImgBackgroundLicenseNubmer = "vat_tile_listofsignup_W";
            viewModel.IsCRChecked = true;
            viewModel.TxtLicenseNumber = string.Empty;
            viewModel.CROptionsVisible = true;
            viewModel.LicenseOptionsVisible = false;

        }

        private void OnLicenseNumberTapped(object sender, TappedEventArgs e)
        {
            viewModel.ImgBackgroundCRNubmer = "vat_tile_listofsignup_W";
            viewModel.ImgBackgroundLicenseNubmer = "FP_selected_tile";

            viewModel.IsCRChecked = false;
            viewModel.TxtCRNumber = string.Empty;
            viewModel.CROptionsVisible = false;
            viewModel.LicenseOptionsVisible = true;

        }

        private void ImageSeeConfirmPassword_Tapped(object sender, TappedEventArgs e)
        {
            viewModel.IsConfirmPasswordEncripted = !viewModel.IsConfirmPasswordEncripted;
            imageConfirmPassword.Source = viewModel.IsConfirmPasswordEncripted ? "hidePassword.png" : "showPassword.png";
        }

        private void ImageSeeNewPassword_Tapped(object sender, TappedEventArgs e)
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
                    viewModel.IsAllValidDataEntered = true;
                    FrmTIN.HasError = false;
                }
            }
            else
            {
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
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));
                    viewModel.IsAllValidDataEntered = false;
                    FrmTIN.HasError = true;
                    EntryTIN.Text = string.Empty;
                }
                else
                {
                    if (!string.IsNullOrEmpty(viewModel.TxtIDNumber) && !string.IsNullOrEmpty(viewModel.PickerDobToDisplay))
                    {
                        ValidateIDNumber();
                    }


                    // viewModel.IsAllValidDataEntered = true;

                    FrmTIN.HasError = false;
                }
            }
        }

        private void DDlIDType_OkayButtonClicked(object sender, EventArgs e)
        {
            viewModel.TxtIDNumber = string.Empty;
            EntryName.IsEnabled = true;
            // SfPicker signUpUsing = (SfPicker)sender;
            viewModel.SelectedSignUpUsing = viewModel.SignUpUsingList[IDTypePicker.Columns[0].SelectedIndex];
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
                catch (Exception)
                {
                }
                viewModel.SelectedSignUpUsingSetForCancle = viewModel.SelectedSignUpUsing;
            }
            IDTypePicker.IsOpen = false;
            ddlLIssuedBy.IsOpen = false;
            ddlLIssuedByCity.IsOpen = false;
        }

        private void EntryName_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(viewModel.TxtName))
            {
                viewModel.IsAllValidDataEntered = true;
                FrmName.HasError = false;
            }
        }

        private async void CRDuplicateCheck()
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
            if (viewModel.IsHijriCal)
            {
                var selectedItem = DpDboHijri.SelectedItem as ObservableCollection<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                string date = UtilityManager.HijriToGreg(year + "/" + month + "/" + day);
                string[] SplitDate = date.Split('/');
                SiguupModel.ABirthdt = SplitDate[0] + "-" + SplitDate[1] + "-" + SplitDate[2] + "T00:00:00";
            }
            else
            {
                var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                SiguupModel.ABirthdt = year + "-" + month + "-" + day + "T00:00:00";
            }



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
                catch (Exception)
                {
                }
                SiguupModel.ACommId = "";
            }
            SiguupModel.AEmail = viewModel.TxtEmailAddress;
            SiguupModel.APhone = "00966" + viewModel.TxtPhoneNumber;

            string newCountryCodeString = viewModel.TxtCountryCode.Replace("+", "00");
            SiguupModel.AMobile = newCountryCodeString + viewModel.TxtMobileNumber;
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
            SiguupModel.CaseGuid = viewModel.Guid;

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
                //  viewModel._dialogService.ShowMessage(Message.ToString(), AppResources.Information);
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));
            }
            else
            {
                await NavigateToVerifyOTPScreenAsync(ResultFirstSubmitModel);
                //viewModel._navigationService.NavigateTo(App.CreateGaztAccountPageView, ResultFirstSubmitModel);
            }
        }

        private async void btnSubmitNext_Clicked(object sender, EventArgs e)
        {
            if (viewModel.IsNextButtonEnable)
            {
                if (viewModel.CurrentTab == EstablishmentSignUPTabEnum.ContactInformation)
                {
                    CheckValidationForContactInformation();
                    viewModel.TxtMobileNumberwithCountryCode = "(" + viewModel.TxtCountryCode + ")" + " " + EntryMobileNumber.Text;

                }
                else if (viewModel.CurrentTab == EstablishmentSignUPTabEnum.IndividualInformation)
                {
                    CheckValidationForIndividualStep();
                }
                else if (viewModel.CurrentTab == EstablishmentSignUPTabEnum.BusinessInformation)//EstablishmentSignUPTabEnum.BusinessInformation
                {
                    CheckValidationForBusinessStep();
                }

                else if (viewModel.CurrentTab == EstablishmentSignUPTabEnum.EmailVerification)
                {
                    CreateGAZTAccount();
                }
                else if (viewModel.CurrentTab == EstablishmentSignUPTabEnum.MobileVerification)
                {
                    await EstablishmentSignUpDataAsync();
                }
                else
                {
                    viewModel.navigateToNext();
                }
            }

        }

        public void CheckValidationForIndividualStep()
        {
            bool flag = true;
            if (viewModel.ImgBackgroundYes == "re_Tile_Background" && string.IsNullOrEmpty(viewModel.TxtTIN))
            {
                flag = false;
                FrmTIN.HasError = true;
            }
            if (string.IsNullOrEmpty(viewModel.TxtIDType))
            {
                flag = false;
                txtEntryName.HasError = true;
            }
            if (string.IsNullOrEmpty(viewModel.TxtIDNumber) || FrmIDNumber.HasError)
            {
                flag = false;
                FrmIDNumber.HasError = true;
                viewModel.TxtIDNumber = string.Empty;

            }
            if (string.IsNullOrEmpty(viewModel.PkrDBO))
            {
                flag = false;
                FrmDBO.HasError = true;
            }
            if (string.IsNullOrEmpty(viewModel.TxtName.Trim()))
            {
                flag = false;
                FrmName.HasError = true;
            }
            if (flag)
            {
                FrmName.HasError = false;
                FrmDBO.HasError = false;
                FrmIDNumber.HasError = false;
                txtEntryName.HasError = false;
                FrmTIN.HasError = false;
                viewModel.PageTitle = AppResources.ZZZBusinessInformation;
                viewModel.BodyText = AppResources.ZZZZCompletethebelowdetails;
                viewModel.CurrentTab = EstablishmentSignUPTabEnum.BusinessInformation;
            }
            else
            {
                // viewModel._dialogService.ShowMessage(AppResources.ZZPleasefillthemandatoryfields, AppResources.Information);
                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillthemandatoryfields));
            }

        }

        public void CheckValidationForContactInformation()
        {
            bool flag = true;
            if (string.IsNullOrEmpty(viewModel.TxtMobileNumber))
            {
                //FrmMobile.HasError = true;
                flag = false;
            }
            if (string.IsNullOrEmpty(viewModel.TxtEmailAddress))
            {
                FrmEmailAddress.HasError = true;
                flag = false;
            }
            if (flag)
            {
                viewModel.PageTitle = AppResources.CRSummary;
                viewModel.BodyText = AppResources.CRReviewthebelowinformation;
                viewModel.NextBTN = AppResources.ZZZZContinue;
                viewModel.CurrentTab = EstablishmentSignUPTabEnum.MobileVerification;
            }
            else
            {
                //    viewModel._dialogService.ShowMessage(AppResources.ZZPleasefillthemandatoryfields, AppResources.Information);
                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillthemandatoryfields));
            }
        }

        public void CheckValidationForBusinessStep()
        {
            //CRNumber Tile
            if (viewModel.IsCRChecked == true)
            {
                if (!string.IsNullOrEmpty(viewModel.TxtCRNumber))
                {
                    viewModel.PageTitle = AppResources.ZZZContactInformation;
                    viewModel.BodyText = AppResources.ZZZZCompletethebelowdetails;
                    viewModel.CurrentTab = EstablishmentSignUPTabEnum.ContactInformation;
                }
                else
                {
                    viewModel.TxtCRNumber = string.Empty;
                    FrmCR.HasError = true;
                    // viewModel._dialogService.ShowMessage(AppResources.ZZPleasefillthemandatoryfields, AppResources.Information);
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillthemandatoryfields));
                }
            }
            //License Number Validation
            else
            {
                bool flag = true;
                //AllValid Data Entered
                if (string.IsNullOrEmpty(viewModel.TxtLicenseNumber))
                {
                    FrmLicenseNumber.HasError = true;
                    flag = false;
                }
                if (string.IsNullOrEmpty(viewModel.TxtLOrCIssuedBy))
                {
                    FrmlicenceNumberIssuedbyframe.HasError = true;
                    flag = false;
                }
                if (string.IsNullOrEmpty(viewModel.TxtLOrCIssuedByCity))
                {
                    FrmIssuedByCity.HasError = true;
                    flag = false;
                }

                if (flag)
                {
                    viewModel.PageTitle = AppResources.ZZZContactInformation;
                    viewModel.BodyText = AppResources.ZZZZCompletethebelowdetails;
                    viewModel.CurrentTab = EstablishmentSignUPTabEnum.ContactInformation;
                }
                else
                {
                    FrmLicenseNumber.HasError = false;
                    FrmlicenceNumberIssuedbyframe.HasError = false;
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillthemandatoryfields));

                }



            }
        }

        public void CreateGAZTAccount()
        {
            StringBuilder PopMsg = new StringBuilder();
            bool IsAllValid = true;
            if (string.IsNullOrEmpty(viewModel.OTPFirstDigit)
                || string.IsNullOrEmpty(viewModel.OTPSecondDigit)
                || string.IsNullOrEmpty(viewModel.OTPThirdDigit)
                || string.IsNullOrEmpty(viewModel.OTPFourthDigit))
            {
                //  frmEmailCode.HasError = true;
                PopMsg.Append(AppResources.ZZPleaseenterconfirmationcodesenttoyouremailaddress);
                IsAllValid = false;
            }
            else
            {
                //frmEmailCode.HasError = false;
            }
            if (string.IsNullOrEmpty(viewModel.MOTPFirstDigit)
                || string.IsNullOrEmpty(viewModel.MOTPSecondDigit)
                || string.IsNullOrEmpty(viewModel.MOTPThirdDigit)
                || string.IsNullOrEmpty(viewModel.MOTPFourthDigit))
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
                    // PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(PopMsg.ToString()));
                }
            }
        }

        void ResetPasswordValidationConditions()
        {
            viewModel.MinEight = "error";
            viewModel.CapsSmall = "error";
            viewModel.MaxSixteen = "error";
            viewModel.NumSymbol = "error";
        }

        private void EntryPass_TextChanged(object sender, TextChangedEventArgs e)
        {
            //rmPass.HasError = false;

            ResetPasswordValidationConditions();

            bool ValidPassword = UtilityManager.ValidateNewPassword(viewModel.TxtPassword);

            if (ValidPassword)
            {
                viewModel.MinEight = "check_oval";
                viewModel.CapsSmall = "check_oval";
                viewModel.MaxSixteen = "check_oval";
                viewModel.NumSymbol = "check_oval";

                // check the new and confirm password condition
            }
            else
            {
                if (UtilityManager.ValidMinEight) { viewModel.MinEight = "check_oval"; }
                if (UtilityManager.ValidSmallL && UtilityManager.ValidCapsL) { viewModel.CapsSmall = "check_oval"; }
                if (UtilityManager.ValidMaxSixteen) { viewModel.MaxSixteen = "check_oval"; }
                if (UtilityManager.ValidNumber && UtilityManager.ValidSymbol) { viewModel.NumSymbol = "check_oval"; }
            }


        }

        public async Task NavigateToVerifyOTPScreenAsync(SignUpModelRootObject ResultFirstSubmitModel)
        {
            try
            {

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    viewModel.IsLoading = false;
                });
                // viewModel.otpTimer.Stop();
                viewModel.StartOTPTimer();
                viewModel.ButtonDisableColor = (Color)Application.Current.Resources["ButtonGray"];
                viewModel.ButtonDisableTextColor = Colors.Gray;
                viewModel.VerifyButtonDisableColor = (Color)Application.Current.Resources["Primary"];
                viewModel.VerifyButtonDisableTextColor = Colors.White;
                viewModel.IsResendOTPEnabled = false;
                viewModel.IsOTPEntryEnable = true;
                viewModel.IsNextButtonEnable = true;
                viewModel.IsResendOTPEnabled = false;

                viewModel.SignUpModelRootObjectM = ResultFirstSubmitModel;
                viewModel.TxtEmailAddress = ResultFirstSubmitModel.d.AEmail;
                string mobileno = ResultFirstSubmitModel.d.AMobile;
                viewModel.TxtMobileNumberwithCountryCode = mobileno;
                viewModel.EncriptedMobileNumberforOtpscreen = "XXXXXXXXXX" + mobileno.Substring(mobileno.Length - 4, 4);

                viewModel.PageTitle = AppResources.VerificationCode;
                viewModel.BodyText = AppResources.ZZPleaseenteraccessCode;
                viewModel.CurrentTab = EstablishmentSignUPTabEnum.EmailVerification;
                OTPFirstEntry.Focus();
            }
            catch (Exception)
            { }
        }

        public async Task EstablishmentSignUpDataAsync()
        {

            viewModel.IsLoading = true;
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
                            viewModel.IsAllValidCRNumberEntered = false;
                            try
                            {
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

                                    if (viewModel.IsHijriCal)
                                    {
                                        var selectedItem = DpDboHijri.SelectedItem as ObservableCollection<object>;
                                        string month = selectedItem[1].ToString();
                                        string day = selectedItem[0].ToString();
                                        string year = selectedItem[2].ToString();
                                        string date = UtilityManager.HijriToGreg(year + "/" + month + "/" + day);
                                        string[] SplitDate = date.Split('/');
                                        SiguupModel.ABirthdt = SplitDate[0] + "-" + SplitDate[1] + "-" + SplitDate[2] + "T00:00:00";
                                    }
                                    else
                                    {
                                        var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;
                                        string month = selectedItem[1].ToString();
                                        string day = selectedItem[0].ToString();
                                        string year = selectedItem[2].ToString();
                                        SiguupModel.ABirthdt = year + "-" + month + "-" + day + "T00:00:00";
                                    }
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
                                        catch (Exception)
                                        {


                                        }
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
                                    SiguupModel.CaseGuid = viewModel.Guid;
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
                                        // viewModel._dialogService.ShowMessage(Message.ToString(), AppResources.Information);
                                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));
                                    }
                                    else
                                    {


                                        await NavigateToVerifyOTPScreenAsync(ResultFirstSubmitModel);

                                    }
                                }
                                else if (ResultDuplicateCR.d.Flag == "X")
                                {
                                    var result = false;

                                    if (App.IsArabic)
                                    {

                                        result = await Application.Current.MainPage.DisplayAlert
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

                                        result = await Application.Current.MainPage.DisplayAlert
                                                                       (AppResources.Alerts, AppResources.ZZZCRValidateMessg,
                                                                           AppResources.ZZZYesText, AppResources.ZZZNoText);

                                        if (result == true)
                                        {
                                            CRDuplicateCheck();


                                        }
                                        else // if it's equal to NO
                                        {
                                            viewModel.IsLoading = false;

                                            return; // just return to the page and do nothing.
                                        }
                                    }


                                }
                                else
                                {
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZYoushouldsignupasnewuser));
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

                                MainThread.BeginInvokeOnMainThread(async () =>
                                {
                                    viewModel.IsLoading = false;

                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                    viewModel._navigationService.GoBack();
                                });
                            }
                            catch (HttpRequestException)
                            {
                                string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                                MainThread.BeginInvokeOnMainThread(async () =>
                                {
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));

                                });
                            }


                            catch (InternetException ex)
                            {
                                MainThread.BeginInvokeOnMainThread(async () =>
                                {
                                    await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                                });
                            }
                            catch (Exception)
                            {

                                string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                                MainThread.BeginInvokeOnMainThread(async () =>
                                {
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));

                                });

                            }



                        }
                        else
                        {
                            viewModel.IsAllValidCRNumberEntered = false;
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

                                if (viewModel.IsHijriCal)
                                {
                                    var selectedItem = DpDboHijri.SelectedItem as ObservableCollection<object>;
                                    string month = selectedItem[1].ToString();
                                    string day = selectedItem[0].ToString();
                                    string year = selectedItem[2].ToString();
                                    //SiguupModel.ABirthdt = year + "-" + month + "-" + day + "T00:00:00";
                                    string date = UtilityManager.HijriToGreg(year + "/" + month + "/" + day);
                                    string[] SplitDate = date.Split('/');
                                    SiguupModel.ABirthdt = SplitDate[0] + "-" + SplitDate[1] + "-" + SplitDate[2] + "T00:00:00";
                                }
                                else
                                {
                                    var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;
                                    string month = selectedItem[1].ToString();
                                    string day = selectedItem[0].ToString();
                                    string year = selectedItem[2].ToString();
                                    SiguupModel.ABirthdt = year + "-" + month + "-" + day + "T00:00:00";
                                }
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
                                    catch (Exception)
                                    {
                                    }
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
                                //  viewModel.TxtMobileNumberwithCountryCode = newCountryCodeString + viewModel.TxtMobileNumber;
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
                                //SiguupModel.CaseGuid = ResutGuid.d.results[0].CaseGuid;
                                SiguupModel.CaseGuid = viewModel.Guid;

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
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));
                                }
                                else
                                {

                                    await NavigateToVerifyOTPScreenAsync(ResultFirstSubmitModel);


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

                                MainThread.BeginInvokeOnMainThread(async () =>
                                {
                                    viewModel.IsLoading = false;

                                    // await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                    viewModel._navigationService.GoBack();
                                });
                            }



                            catch (InternetException ex)
                            {
                                MainThread.BeginInvokeOnMainThread(async () =>
                                {
                                    //  viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                                });
                            }
                            catch (HttpRequestException)
                            {
                                string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                                MainThread.BeginInvokeOnMainThread(async () =>
                                {
                                    // IsLoading = false;

                                    //  await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                    //_navigationService.GoBack();
                                });
                            }
                            catch (Exception)
                            {

                                string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                                MainThread.BeginInvokeOnMainThread(async () =>
                                {
                                    // IsLoading = false;

                                    // await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                    //_navigationService.GoBack();
                                });
                            }
                        }
                    }
                    else
                    {
                        //   viewModel._dialogService.ShowMessage(AppResources.ZZYoushouldsignupasnewuser, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZYoushouldsignupasnewuser));
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

                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        viewModel.IsLoading = false;

                        //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                        viewModel._navigationService.GoBack();
                    });
                }
                catch (InternetException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        // viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                    });
                }
                catch (HttpRequestException)
                {
                    string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        viewModel.IsLoading = false;

                        // await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                        viewModel._navigationService.GoBack();
                    });
                }
                catch (Exception)
                {

                    string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        viewModel.IsLoading = false;

                        //  await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                        viewModel._navigationService.GoBack();
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
                                if (viewModel.IsHijriCal)
                                {
                                    var selectedItem = DpDboHijri.SelectedItem as ObservableCollection<object>;
                                    string month = selectedItem[1].ToString();
                                    string day = selectedItem[0].ToString();
                                    string year = selectedItem[2].ToString();
                                    //SiguupModel.ABirthdt = year + "-" + month + "-" + day + "T00:00:00";
                                    string date = UtilityManager.HijriToGreg(year + "/" + month + "/" + day);
                                    string[] SplitDate = date.Split('/');
                                    SiguupModel.ABirthdt = SplitDate[0] + "-" + SplitDate[1] + "-" + SplitDate[2] + "T00:00:00";
                                }
                                else
                                {
                                    var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;
                                    string month = selectedItem[1].ToString();
                                    string day = selectedItem[0].ToString();
                                    string year = selectedItem[2].ToString();
                                    SiguupModel.ABirthdt = year + "-" + month + "-" + day + "T00:00:00";
                                }
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
                                    catch (Exception)
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
                                // viewModel.TxtMobileNumberwithCountryCode = newCountryCodeString + viewModel.TxtMobileNumber;
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
                                // SiguupModel.CaseGuid = ResutGuid.d.results[0].CaseGuid;
                                SiguupModel.CaseGuid = viewModel.Guid;

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
                                    //   viewModel._dialogService.ShowMessage(Message.ToString(), AppResources.Information);
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));
                                }
                                else
                                {
                                    await NavigateToVerifyOTPScreenAsync(ResultFirstSubmitModel);
                                    //viewModel._navigationService.NavigateTo(App.CreateGaztAccountPageView, ResultFirstSubmitModel);
                                }
                            }
                            else if (ResultDuplicateCR.d.Flag == "X")
                            {
                                var result = false;

                                if (App.IsArabic)
                                {

                                    result = await Application.Current.MainPage.DisplayAlert
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

                                    result = await Application.Current.MainPage.DisplayAlert
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


                            }
                            else
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZYoushouldsignupasnewuser));
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

                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;

                                //   await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                //viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                            });
                        }
                        catch (HttpRequestException)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;

                                //  await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (Exception)
                        {

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;

                                // await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                viewModel._navigationService.GoBack();
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
                            if (viewModel.IsHijriCal)
                            {
                                var selectedItem = DpDboHijri.SelectedItem as ObservableCollection<object>;
                                string month = selectedItem[1].ToString();
                                string day = selectedItem[0].ToString();
                                string year = selectedItem[2].ToString();
                                //SiguupModel.ABirthdt = year + "-" + month + "-" + day + "T00:00:00";
                                string date = UtilityManager.HijriToGreg(year + "/" + month + "/" + day);
                                string[] SplitDate = date.Split('/');
                                SiguupModel.ABirthdt = SplitDate[0] + "-" + SplitDate[1] + "-" + SplitDate[2] + "T00:00:00";
                            }
                            else
                            {
                                var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;
                                string month = selectedItem[1].ToString();
                                string day = selectedItem[0].ToString();
                                string year = selectedItem[2].ToString();
                                SiguupModel.ABirthdt = year + "-" + month + "-" + day + "T00:00:00";
                            }
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
                                catch (Exception)
                                {
                                }
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
                            //SiguupModel.CaseGuid = ResutGuid.d.results[0].CaseGuid;
                            SiguupModel.CaseGuid = viewModel.Guid;

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
                                //viewModel._dialogService.ShowMessage(Message.ToString(), AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));
                            }
                            else
                            {
                                await NavigateToVerifyOTPScreenAsync(ResultFirstSubmitModel);
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

                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;

                                // await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                //viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                            });
                        }
                        catch (HttpRequestException)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;

                                //  await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (Exception)
                        {

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;

                                // await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                viewModel._navigationService.GoBack();
                            });
                        }
                    }
                }
                else
                {
                    // viewModel._dialogService.ShowMessage(AppResources.ZZYoushouldsignupasnewuser, AppResources.Information);
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZYoushouldsignupasnewuser));
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
                                if (viewModel.IsHijriCal)
                                {
                                    var selectedItem = DpDboHijri.SelectedItem as ObservableCollection<object>;
                                    string month = selectedItem[1].ToString();
                                    string day = selectedItem[0].ToString();
                                    string year = selectedItem[2].ToString();
                                    //SiguupModel.ABirthdt = year + "-" + month + "-" + day + "T00:00:00";
                                    string date = UtilityManager.HijriToGreg(year + "/" + month + "/" + day);
                                    string[] SplitDate = date.Split('/');
                                    SiguupModel.ABirthdt = SplitDate[0] + "-" + SplitDate[1] + "-" + SplitDate[2] + "T00:00:00";
                                }
                                else
                                {
                                    var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;
                                    string month = selectedItem[1].ToString();
                                    string day = selectedItem[0].ToString();
                                    string year = selectedItem[2].ToString();
                                    SiguupModel.ABirthdt = year + "-" + month + "-" + day + "T00:00:00";
                                }
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
                                    catch (Exception)
                                    {
                                    }
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
                                //viewModel.TxtMobileNumberwithCountryCode = newCountryCodeString + viewModel.TxtMobileNumber;
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
                                //SiguupModel.CaseGuid = ResutGuid.d.results[0].CaseGuid;
                                SiguupModel.CaseGuid = viewModel.Guid;

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
                                    //  viewModel._dialogService.ShowMessage(Message.ToString(), AppResources.Information);
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));
                                }
                                else
                                {
                                    await NavigateToVerifyOTPScreenAsync(ResultFirstSubmitModel);
                                    // viewModel._navigationService.NavigateTo(App.CreateGaztAccountPageView, ResultFirstSubmitModel);
                                }
                            }
                            else if (ResultDuplicateCR.d.Flag == "X")
                            {
                                var result = false;

                                if (App.IsArabic)
                                {

                                    result = await Application.Current.MainPage.DisplayAlert
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

                                    result = await Application.Current.MainPage.DisplayAlert
                                                                   (AppResources.Alerts, AppResources.ZZZCRValidateMessg,
                                                                       AppResources.ZZZYesText, AppResources.ZZZNoText);

                                    if (result == true)
                                    {
                                        CRDuplicateCheck();
                                        viewModel._navigationService.GoBack();

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
                                //  await viewModel._dialogService.ShowMessage(AppResources.ZZYoushouldsignupasnewuser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZYoushouldsignupasnewuser));
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

                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;

                                //  await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                //  viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                            });
                        }
                        catch (HttpRequestException)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;

                                //  await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (Exception)
                        {

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                viewModel._navigationService.GoBack();
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
                            if (viewModel.IsHijriCal)
                            {
                                var selectedItem = DpDboHijri.SelectedItem as ObservableCollection<object>;
                                string month = selectedItem[1].ToString();
                                string day = selectedItem[0].ToString();
                                string year = selectedItem[2].ToString();
                                //SiguupModel.ABirthdt = year + "-" + month + "-" + day + "T00:00:00";
                                string date = UtilityManager.HijriToGreg(year + "/" + month + "/" + day);
                                string[] SplitDate = date.Split('/');
                                SiguupModel.ABirthdt = SplitDate[0] + "-" + SplitDate[1] + "-" + SplitDate[2] + "T00:00:00";
                            }
                            else
                            {
                                var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;
                                string month = selectedItem[1].ToString();
                                string day = selectedItem[0].ToString();
                                string year = selectedItem[2].ToString();
                                SiguupModel.ABirthdt = year + "-" + month + "-" + day + "T00:00:00";
                            }
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
                                catch (Exception)
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
                            //viewModel.TxtMobileNumberwithCountryCode = newCountryCodeString + viewModel.TxtMobileNumber;
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
                            //SiguupModel.CaseGuid = ResutGuid.d.results[0].CaseGuid;
                            SiguupModel.CaseGuid = viewModel.Guid;

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
                                //  viewModel._dialogService.ShowMessage(Message.ToString(), AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));
                            }
                            else
                            {
                                MainThread.BeginInvokeOnMainThread(() =>
                                {
                                    viewModel.IsLoading = true;


                                });
                                await NavigateToVerifyOTPScreenAsync(ResultFirstSubmitModel);
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

                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                //viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                            });
                        }
                        catch (HttpRequestException)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;

                                //  await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (Exception)
                        {

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;

                                // await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                viewModel._navigationService.GoBack();
                            });
                        }
                    }
                }
                catch (InternetException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        //viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                    });
                }
            }

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            });
            viewModel.IsLoading = false;
        }

        private void EntryLicenceNumber_Unfocused(object sender, FocusEventArgs e)
        {
            FrmLicenseNumber.HasError = false;
        }

        private void EntryCRNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryCRNumber.Text))
            {
                FrmCR.HasError = false;
                //  EntryCRNumber.Unfocus();
            }
        }


        public void IndividualValidationCheck()
        {

            if (string.IsNullOrEmpty(EntryTIN.Text))
            {
                FrmTIN.HasError = true;
            }

            if (string.IsNullOrEmpty(EntryIDNumber.Text))
            {
                FrmIDNumber.HasError = true;
            }

            if (string.IsNullOrEmpty(DateEntry.Text))
            {
                FrmDBO.HasError = true;
            }

            if (string.IsNullOrEmpty(EntryName.Text))
            {
                FrmName.HasError = true;

            }

            if (FrmIDNumber.HasError == true && FrmDBO.HasError == true && FrmName.HasError == true)
            {
                viewModel.IsAllValidDataEntered = false;
            }

        }


        private void DateEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(viewModel.PkrDBO))
            {
                FrmDBO.HasError = false;
            }

        }

        private void IssuedByCity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(viewModel.TxtLOrCIssuedByCity))
            {
                FrmIssuedByCity.HasError = false;
            }
        }

        private void HijriCalSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            try
            {
                if (viewModel.IsHijriCal)
                {
                    if (DpDboHijri.SelectedItem != null)
                    {
                        var selectedItem = DpDboHijri.SelectedItem as ObservableCollection<object>;
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
                        viewModel.PkrDBO = year + "/" + month + "/" + day;
                        //viewModel.PickerDobToDisplay = day + "/" + month + "/" + year;
                        viewModel.PickerDobToDisplay = year + "/" + month + "/" + day;

                    }
                    else
                    {
                        viewModel.PkrDBO = string.Empty;
                        viewModel.PickerDobToDisplay = string.Empty;

                    }

                }
                else
                {
                    if (DpDbo.SelectedItem != null)
                    {
                        var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
                        viewModel.PkrDBO = year + "/" + month + "/" + day;
                        //viewModel.PickerDobToDisplay = day + "/" + month + "/" + year;
                        viewModel.PickerDobToDisplay = year + "/" + month + "/" + day;
                    }
                    else
                    {
                        viewModel.PkrDBO = string.Empty;
                        viewModel.PickerDobToDisplay = string.Empty;

                    }
                }


                if (!string.IsNullOrEmpty(viewModel.TxtIDNumber) && !string.IsNullOrEmpty(viewModel.PickerDobToDisplay))
                {
                    ValidateIDNumber();
                }



            }
            catch (Exception )
            {


            }
        }

        private void Btn_Edit_indiviadualInfo_Clicked(object sender, EventArgs e)
        {
            viewModel.PageTitle = AppResources.ZZZIndividualInformation;
            viewModel.BodyText = AppResources.ZZZZCompletethebelowdetails;
            viewModel.NextBTN = AppResources.ZZZZContinue;
            viewModel.CurrentTab = EstablishmentSignUPTabEnum.IndividualInformation;
        }

        private void Btn_Edit_BusinessInfo_Clicked(object sender, EventArgs e)
        {

            viewModel.PageTitle = AppResources.ZZZBusinessInformation;
            viewModel.BodyText = AppResources.ZZZZCompletethebelowdetails;
            viewModel.CurrentTab = EstablishmentSignUPTabEnum.BusinessInformation;
        }

        private void Btn_Edit_ContactInfo_Clicked(object sender, EventArgs e)
        {
            viewModel.PageTitle = AppResources.ZZZContactInformation;
            viewModel.BodyText = AppResources.ZZZZCompletethebelowdetails;
            viewModel.CurrentTab = EstablishmentSignUPTabEnum.ContactInformation;
        }

        private void ChipGroup_statusFilter_SelectionChanged(object sender, Syncfusion.Maui.Core.Chips.SelectionChangedEventArgs e)
        {
            try
            {
                ChipModel selectedReturntype = (ChipModel)e.AddedItem;
                ChipGroup_statusFilter.SelectedItem = selectedReturntype;
                if (selectedReturntype.Text.Equals(AppResources.NDHijri))
                {
                    viewModel.IsHijriCal = true;
                    if (DpDboHijri.SelectedItem != null)
                    {
                        if (!string.IsNullOrEmpty(viewModel.SelectedHijriDate))
                        {

                            if (!DpDboHijri.SelectedItem.ToString().Equals(viewModel.SelectedHijriDate))
                            {

                                var ConvertedDateArray = viewModel.SelectedHijriDate.Split('/');

                                ObservableCollection<object> todaycollection = new ObservableCollection<object>();
                                //Select today dates
                                todaycollection.Add(ConvertedDateArray[0].ToString());
                                todaycollection.Add(ConvertedDateArray[1].ToString());//day
                                todaycollection.Add(ConvertedDateArray[2].ToString());

                                DpDboHijri.SelectedItem = todaycollection;

                            }

                        }



                        var selectedItem = DpDboHijri.SelectedItem as ObservableCollection<object>;
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
                        viewModel.PkrDBO = year + "/" + month + "/" + day;
                        //viewModel.PickerDobToDisplay = day + "/" + month + "/" + year;
                        viewModel.PickerDobToDisplay = year + "/" + month + "/" + day;

                        if (!string.IsNullOrEmpty(viewModel.PkrDBO))
                        {

                            var ConvertedDate = UtilityManager.HijriToGreg(viewModel.PkrDBO);
                            var ConvertedDateArray = ConvertedDate.Split('/');



                            ObservableCollection<object> todaycollection = new ObservableCollection<object>();
                            //Select today dates
                            todaycollection.Add(ConvertedDateArray[2].ToString());
                            todaycollection.Add(ConvertedDateArray[1].ToString());//day
                            todaycollection.Add(ConvertedDateArray[0].ToString());

                            DpDbo.SelectedItem = todaycollection;
                        }

                    }
                    else
                    {
                        viewModel.PkrDBO = string.Empty;
                        viewModel.PickerDobToDisplay = string.Empty;

                    }

                }
                else
                {
                    viewModel.IsHijriCal = false;
                    if (DpDbo.SelectedItem != null)
                    {

                        if (!string.IsNullOrEmpty(viewModel.SelectedGregDate))
                        {

                            if (!DpDboHijri.SelectedItem.ToString().Equals(viewModel.SelectedGregDate))
                            {

                                var ConvertedDateArray = viewModel.SelectedGregDate.Split('/');

                                ObservableCollection<object> todaycollection = new ObservableCollection<object>();
                                //Select today dates
                                todaycollection.Add(ConvertedDateArray[0].ToString());
                                todaycollection.Add(ConvertedDateArray[1].ToString());//day
                                todaycollection.Add(ConvertedDateArray[2].ToString());

                                DpDbo.SelectedItem = todaycollection;

                            }

                        }

                        var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
                        viewModel.PkrDBO = year + "/" + month + "/" + day;
                        //viewModel.PickerDobToDisplay = day + "/" + month + "/" + year;
                        viewModel.PickerDobToDisplay = year + "/" + month + "/" + day;

                        if (!string.IsNullOrEmpty(viewModel.PkrDBO))
                        {

                            var ConvertedDate = UtilityManager.ConvertToHijri(viewModel.PkrDBO);
                            var ConvertedDateArray = ConvertedDate.Split('/');



                            ObservableCollection<object> todaycollection = new ObservableCollection<object>();
                            //Select today dates
                            todaycollection.Add(ConvertedDateArray[2].ToString());
                            todaycollection.Add(ConvertedDateArray[1].ToString());//day
                            todaycollection.Add(ConvertedDateArray[0].ToString());

                            DpDboHijri.SelectedItem = todaycollection;
                        }




                    }
                    else
                    {
                        viewModel.PkrDBO = string.Empty;
                        viewModel.PickerDobToDisplay = string.Empty;

                    }
                }


                if (!string.IsNullOrEmpty(viewModel.TxtIDNumber) && !string.IsNullOrEmpty(viewModel.PickerDobToDisplay))
                {
                    ValidateIDNumber();
                }



            }
            catch (Exception)
            {


            }
        }

        private void OTPTap(object sender, EventArgs e)
        {
            var aaa = e as TappedEventArgs;
            if (Convert.ToInt32(aaa.Parameter) == 1)
            {
                OTPFirstEntry.Focus();
            }
            else if (Convert.ToInt32(aaa.Parameter) == 2)
            {
                OTPSecondEntry.Focus();
            }
            else if (Convert.ToInt32(aaa.Parameter) == 3)
            {
                OTPThirdEntry.Focus();
            }
            else if (Convert.ToInt32(aaa.Parameter) == 4)
            {
                OTPFourthEntry.Focus();
            }
            else if (Convert.ToInt32(aaa.Parameter) == 5)
            {
                MobOTPFirstEntry.Focus();
            }
            else if (Convert.ToInt32(aaa.Parameter) == 6)
            {
                MobOTPSecondEntry.Focus();
            }
            else if (Convert.ToInt32(aaa.Parameter) == 7)
            {
                MobOTPThirdEntry.Focus();
            }
            else if (Convert.ToInt32(aaa.Parameter) == 8)
            {
                MobOTPFourthEntry.Focus();
            }
        }
    }

}