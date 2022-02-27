using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.TaxpayerProfileVM;
using EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage;
using GAZT.Manager;
using GAZT.Models;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using Application = Xamarin.Forms.Application;

namespace EGAZT.Views.NewDesign.TaxpayerProfile
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateMobilePopUp : PopupPage
    {
        UpdateMobileViewModel viewModel;
        ObservableCollection<InternationalMobileData> currentMobileData = null;

        public UpdateMobilePopUp(ObservableCollection<InternationalMobileData> mobileData)
        {
            InitializeComponent();
            viewModel = App.Locator.UpdateMobilePopUp;
            this.BindingContext = viewModel;

            viewModel.CountryCode = "+966";
            if (App.IsArabic == false)
            {
                Label_InternationalnoCode.StyleId = "LTRLabelText";
            }
            else
            {
                Label_InternationalnoCode.StyleId = "RTLLabelText";
            }

            if (Device.RuntimePlatform == Device.Android)
                Label_InternationalnoCode.Margin = new Thickness(0);
            else
                Label_InternationalnoCode.Margin = new Thickness(10, -8, 10, -8);

            //SetLTR();
            this.FlowDirection = UtilityManager.SetLTRAndRTL();

            // Setup International Mobile Data
            currentMobileData = mobileData;
            if (App.IsArabic)
            {
                Mobile_Entry.HorizontalTextAlignment = TextAlignment.End;
            }
            else
            {
                Mobile_Entry.HorizontalTextAlignment = TextAlignment.Start;
            }

            if (App.IsArabic)
            {
                viewModel.Arabictext = true;
                viewModel.engText = false;
            }
            else
            {
                // OTPSentOnThisMobileNumberLbl.FlowDirection = FlowDirection.LeftToRight;
                viewModel.Arabictext = false;
                viewModel.engText = true;

            }
        }

        // * Forgot password : OTP Verification :
        void OtpFirstEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (viewModel.OTPFirstDigit.Length == 1) { OTPSecondEntry.Focus(); }
        }

        void OtpSecondEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (viewModel.OTPSecondDigit.Length == 1) { OTPThirdEntry.Focus(); }
            else if (viewModel.OTPSecondDigit.Length == 0) { OTPFirstEntry.Focus(); }
        }

        void OtpThirdEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (viewModel.OTPThirdDigit.Length == 1) { OTPFourthEntry.Focus(); }
            else if (viewModel.OTPThirdDigit.Length == 0) { OTPSecondEntry.Focus(); }
        }

        void OtpFourthEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (viewModel.OTPFourthDigit.Length == 0) { OTPThirdEntry.Focus(); }
        }

        void OtpFourthEntry_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            /*if (viewModel.OTPFourthDigit.Length != 0)
                viewModel.BtnEnableFlag = true;*/
        }
        // * End

        private void UpdatedClicked(object sender, EventArgs e)
        {
            if (btn.Text == AppResources.TPUpdate) { UpdateMobileNumber(); }
            else { VerifyOTP(); }
        }

        private async void UpdateMobileNumber()
        {
            // Call Update Mobile Number API + Go Success Page
            bool callAPIFlag = TaxpayerProfileUpdateValidation(viewModel.CurrentMobileNumberEntryText, viewModel.NewMobileNumberEntryText);

            if (callAPIFlag)
            {
                TaxPayerProfile TPAPIResponse = await viewModel.VarifyMobileNumber();
                if (TPAPIResponse != null)
                {
                    UpdateMobile.IsVisible = false;
                    VerificationView.IsVisible = true;
                    //viewModel.BtnEnableFlag = false;
                    btn.Text = AppResources.Verify;

                    var result = Regex.Match(viewModel.NewMobileNumberEntryText, @"(.{3})\s*$");
                    viewModel.OTPSentOnThisMobileNumber = "********" + result;
                    viewModel.OTPSentOnThisMobileNumber2 = "********" + result;
                    // Start timer period for valid OTP
                    viewModel.StartOTPTimer();
                }
            }
        }

        private async void VerifyOTP()
        {
            viewModel.EnteredOTP = viewModel.OTPFirstDigit
                                    + viewModel.OTPSecondDigit
                                    + viewModel.OTPThirdDigit
                                    + viewModel.OTPFourthDigit;

            if (viewModel.EnteredOTP.Length != 4)
            {
                viewModel.ShowValidationPopup(AppResources.PleaseenterOTP);
                return;
            }

            // * Navigating to Verification Screen
            TaxPayerProfile TPAPIResponse = await viewModel.VarifyOTPToUpdateMobileNumber();
            System.Diagnostics.Debug.WriteLine("TP SUCCESS RESPONSE: ", TPAPIResponse);

            if (TPAPIResponse != null)
            {
                // * Update TP Profile Object
                App.TP = TPAPIResponse;
                App.TP.Userid = TPAPIResponse.Tin;

                Device.BeginInvokeOnMainThread(() =>
                {
                    this.CloseAllPopup();
                    viewModel._navigationService.NavigateTo(App.TaxpayerProfileSuccessPage, 2);
                });
            }
        }

        async void OnResendOTPBtnClicked(System.Object sender, System.EventArgs e)
        {
            if (viewModel.countDownSeconds == 0)
            {
                RefreshOTPFieldsData();
                OTPFirstEntry.Focus();
                TaxPayerProfile TPAPIResponse = await viewModel.VarifyMobileNumber();

                if (TPAPIResponse != null)
                {
                    // * Start timer period for valid OTP
                    viewModel.StartOTPTimer();
                    viewModel.ResendOTPTextColor =  (Color)Application.Current.Resources["ResendOTPTextColor"];

                }
            }
        }

        // * Mobile Number Validations
        private bool TaxpayerProfileUpdateValidation(string CurrentMobileNumber, string NewMobileNumber)
        {
            string validationError = VerifyCurrentAndNewMobilenNumbers(CurrentMobileNumber, NewMobileNumber);
            if (validationError == string.Empty) return true;
            else
            {
                viewModel.ShowValidationPopup(validationError);
                return false;
            }
        }

        private string VerifyCurrentAndNewMobilenNumbers(string CurrentMobileNumber, string NewMobileNumber)
        {
            // * Append country code + mobile number : For comparison OLD + NEW
            string NewMobileNumberWithCountyCode = Label_InternationalnoCode.Text + NewMobileNumber;
            bool compareStringFlag = string.Equals(CurrentMobileNumber, NewMobileNumberWithCountyCode);

            if (string.IsNullOrEmpty(NewMobileNumber))
                return AppResources.TPNewMobileNumberEmpty;
            else if (viewModel.NewMobileNumberEntryText.Length < 9)
                return AppResources.ZZMobilenumberlengthcannotbelessthan9digits;
            else if (compareStringFlag)
                return AppResources.ZZTheNewMobileNumberMustNotMatchtheexistingMobileNumber;
            else
                return string.Empty;
        }

        private async void CloseAllPopup()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }

        async void OnBackArrowTapped(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAllAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;

            if (Device.RuntimePlatform == Device.Android)
                Label_InternationalnoCode.Margin = new Thickness(0);
            else
                Label_InternationalnoCode.Margin = new Thickness(10, -8, 10, -8);

            try
            {
                if (App.TP.Mobile.Length < 12)
                    viewModel.CurrentMobileNumberEntryText = "+966" + App.TP.Mobile.Remove(0, 2);
                else
                    viewModel.CurrentMobileNumberEntryText = "+" + App.TP.Mobile.Remove(0, 2);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            MessagingCenter.Subscribe<InternationalCodeSearchPage, string>(this, "SelectedItem", (sender, arg) =>
            {
                Label_InternationalnoCode.Text = arg;
                viewModel.CountryCode = arg;
            });
            MessagingCenter.Subscribe<InternationalCodeSearchPage, string>(this, "SelectedCountryCode", (sender, arg) =>
            {
                viewModel.MobileCountryCode = arg;
            });

            RefreshControlsData();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<InternationalCodeSearchPage, string>(this, "SelectedItem");

            // * Worka aroung - Need to find a solution
            if (viewModel.countDownSeconds != 0)
                viewModel.otpTimer.Stop();
        }

        // * // Reset Enteried
        private void RefreshControlsData()
        {
            RefreshOTPFieldsData();
            viewModel.IsLoading = false;
            viewModel.NewMobileNumberEntryText = string.Empty;
            btn.Text = AppResources.TPUpdate;
        }

        private void RefreshOTPFieldsData()
        {
            viewModel.OTPFirstDigit = string.Empty;
            viewModel.OTPSecondDigit = string.Empty;
            viewModel.OTPThirdDigit = string.Empty;
            viewModel.OTPFourthDigit = string.Empty;
            viewModel.EnteredOTP = string.Empty;
        }

        private void Mobile_entry_Unfocused(object sender, FocusEventArgs e) { }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new InternationalCodeSearchPage(currentMobileData));
        }
    }
}
