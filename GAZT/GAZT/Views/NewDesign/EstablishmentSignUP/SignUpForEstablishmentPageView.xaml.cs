using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.EstablishmentSignUPVM;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using EGAZT.Views.SyncFusionEnabledViews.CorrespondenceDetails;
using EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage;
using GAZT.Manager;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfPicker.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
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


            viewModel.IsDeclarationCheckedForInstruction = false;
            ChangeAeroIcon();
            SetLTR();
            loadPageData();

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


        }
        public async Task loadPageData()
        {

            await viewModel.SetDefaultDate();
            ClearFields();
            await viewModel.OnPageLoad();
            await viewModel.SetIssueIdList();
          //  await viewModel.SetCityList();


        }

        public void ClearFields()
        {
            //viewModel.PkrDBO = string.Empty;
            //viewModel.TxtLOrCIssuedBy = string.Empty;
            //viewModel.TxtLOrCIssuedByCity = string.Empty;
            viewModel.IDTypeIndex = 0;
            //viewModel.SelectedLOrC = 1;
            //viewModel.IsLoading = false;
            //viewModel.SelectedSignUpUsing = null;
            //viewModel.SignUpUsingList = null;
            //viewModel.SelectLCType = null;
            //viewModel.LcTypeList = null;
            //viewModel.SelectCityList = null;
            //viewModel.CityList = null;
            //viewModel.IsCRVisible = true;
            //viewModel.IsLicenseVisible = false;
            //viewModel.IsTIN = false;
            //viewModel.IsTINVisible = false;
            //viewModel.SelectedIssuedBy = null;
            //viewModel.IssuedByList = null;
            //viewModel.TxtTIN = string.Empty;
            //viewModel.TxtIDNumber = string.Empty;
            //viewModel.TxtName = string.Empty;
            //viewModel.TxtCRNumber = string.Empty;
            //viewModel.TxtLicenseNumber = string.Empty;
            //viewModel.TxtEmailAddress = string.Empty;
            //viewModel.TxtCountryCode = string.Empty;

            //viewModel.TxtMobileNumber = string.Empty;
            //viewModel.TxtPhoneNumber = string.Empty;

            //viewModel.IDTypeModelRootObject = null;
            //viewModel.SignUpFirstSubmitModel = null;
            //viewModel.MaximumxD = DateTime.Now;
           
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
                //if (!viewModel.IsResendOTPEnabled)
                //{
                //    // viewModel.VerifyOTP();
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
                    //FrmEmailAddress.HasError = true;
                    EntryEmail.Text = string.Empty;
                }
                else
                {
                   // FrmEmailAddress.HasError = false;
                }
            }
        }

        private void EntryPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryPhoneNumber.Text))
            {
                if (EntryPhoneNumber.Text.Substring(0, 1) != "1")
                {
                   // FrmPhoneNumber.HasError = true;
                }
                else
                {
                   // FrmPhoneNumber.HasError = false;
                }
            }
        }

        private void EntryPhoneNumber_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(EntryPhoneNumber.Text))
            {
               // FrmPhoneNumber.HasError = false;
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
                    //FrmPhoneNumber.HasError = true;
                    EntryPhoneNumber.Text = string.Empty;
                    EntryPhoneNumber.Focus();
                }
                else
                {
                   // FrmPhoneNumber.HasError = false;
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
                    //FrmMobileNumber.HasError = true;
                    EntryMobileNumber.Text = string.Empty;
                }
                else
                {
                   // FrmMobileNumber.HasError = false;
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

        }

        private void OnLicenseIssuedByClicked(object sender, EventArgs e)
        {
            LicenseIssuedByPicker.IsOpen = true;
        }

        private void OnIssuingCityClicked(object sender, EventArgs e)
        {
            IssuingCityPicker.IsOpen = true;
        }

        private void DpDbo_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void OnInCTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new InstructionPopUpPageView());
        }

        private void OnYesTapped(object sender, EventArgs e)
        {
            viewModel.ImgBackgroundNo = "re_Property_Tile_Background_White";
            viewModel.ImgBackgroundYes = "re_Tile_Background";
        }

        private void OnNoTapped(object sender, EventArgs e)
        {
            viewModel.ImgBackgroundNo = "re_Tile_Background";
            viewModel.ImgBackgroundYes = "re_Property_Tile_Background_White";
        }

        private void OnCRNumberTapped(object sender, EventArgs e)
        {
            viewModel.ImgBackgroundCRNubmer = "FP_selected_tile";
            viewModel.ImgBackgroundLicenseNubmer = "FP_unselected_tile";
        }

        private void OnLicenseNumberTapped(object sender, EventArgs e)
        {
            viewModel.ImgBackgroundCRNubmer = "FP_unselected_tile";
            viewModel.ImgBackgroundLicenseNubmer = "FP_selected_tile";
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
            //FrmTIN.HasError = false;
            if (!string.IsNullOrEmpty(EntryTIN.Text))
            {
                if (EntryTIN.Text.Substring(0, 1) != "3")
                {
                  //  FrmTIN.HasError = true;
                }
                else
                {
                   // FrmTIN.HasError = false;
                }
            }
        }

        private void EntryTIN_Unfocused(object sender, FocusEventArgs e)
        {
        //    PopUp popUp = new PopUp();
        //    StringBuilder Messages = new StringBuilder();
        //    if (!string.IsNullOrEmpty(EntryTIN.Text))
        //    {
        //        if (EntryTIN.Text.Substring(0, 1) != "3")
        //        {
        //            Messages.Append(AppResources.ZZTINnumberhastostartwithnumber3);
        //            EntryTIN.Focus();
        //        }
        //        if (EntryTIN.Text.Length != 10)
        //        {
        //            if (Messages.Length > 0)
        //            {
        //                Messages.Append(Environment.NewLine);
        //            }
        //            Messages.Append(AppResources.ZZTINnumberlengthcannotbelessthan10digits);
        //        }
        //        if (Messages.Length > 0)
        //        {
        //            popUp.Message = Messages.ToString();
        //            popUp.IsLinkAvailable = false;
        //            if (App.IsArabic)
        //            {
        //                popUp.FlowDirections = "RightToLeft";
        //                popUp.isFontSet = true;
        //            }
        //            else
        //            {
        //                popUp.FlowDirections = "LeftToRight";
        //            }
        //            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        //            FrmTIN.HasError = true;
        //            EntryTIN.Text = string.Empty;
        //        }
        //        else
        //        {
        //            FrmTIN.HasError = false;
        //        }
        //    }
        
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
               // FrmName.HasError = false;
            }
        }


    }
}