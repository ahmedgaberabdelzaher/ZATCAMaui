using EGAZT.ViewModel.NewDesignViewModel.TaxpayerProfileVM;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GAZT.Manager;
using GAZT.Models;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.TaxpayerProfile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateMobilePopUp : PopupPage
    {
        UpdateMobileViewModel viewModel;

        public UpdateMobilePopUp()
        {
            InitializeComponent();
            viewModel = App.Locator.UpdateMobilePopUp;
            this.BindingContext = viewModel;

            //SetLTR();
            this.FlowDirection = UtilityManager.SetLTRAndRTL();
        }

        // * Forgot password : OTP Verification :
        void OtpFirstEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (viewModel.OTPFirstDigit.Length > 0) { OTPSecondEntry.Focus(); }
        }

        void OtpSecondEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (viewModel.OTPSecondDigit.Length > 0) { OTPThirdEntry.Focus(); }
        }

        void OtpThirdEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (viewModel.OTPThirdDigit.Length > 0) { OTPFourthEntry.Focus(); }
        }

        void OtpFourthEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e) { }

        void OtpFourthEntry_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (viewModel.OTPFourthDigit.Length != 0)
                viewModel.BtnEnableFlag = true;
        }
        // * End

        private void UpdatedClicked(object sender, EventArgs e)
        {
            if (btn.Text == "Update") { UpdateMobileNumber(); }
            else { VerifyOTP(); }
        }

        private async void UpdateMobileNumber()
        {
            // Call Update Mobile Number API + Go Success Page
            bool callAPIFlag = TaxpayerProfileUpdateValidation(viewModel.CurrentMobileNumberEntryText, viewModel.NewMobileNumberEntryText);

            if (callAPIFlag)
            {
                bool PWDSuccess = await viewModel.VarifyMobileNumber();
                //bool PWDSuccess = await APIManager.VarifyMobileNumber(viewModel.CurrentMobileNumberEntryText, viewModel.NewMobileNumberEntryText);
                System.Diagnostics.Debug.WriteLine("OTP SUCCESS: ", PWDSuccess);

                if (PWDSuccess)
                {
                    UpdateMobile.IsVisible = false;
                    VerificationView.IsVisible = true;
                    viewModel.BtnEnableFlag = false;
                    btn.Text = "Verify";

                    var result = Regex.Match(viewModel.NewMobileNumberEntryText, @"(.{3})\s*$");
                    viewModel.OTPSentOnThisMobileNumber = AppResources.MobileNumber + " ********" + result;

                    // * Start timer period for valid OTP
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
                Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                {
                    await viewModel._dialogService.ShowMessageBox(AppResources.EnterOTP, AppResources.Information);
                });
                return;
            }

            // * Navigating to Verification Screen
            //TaxPayerProfile TPAPIResponse = await APIManager.VarifyOTPToUpdateMobileNumber(viewModel.EnteredOTP, viewModel.CurrentMobileNumberEntryText, viewModel.NewMobileNumberEntryText);

            TaxPayerProfile TPAPIResponse = await viewModel.VarifyOTPToUpdateMobileNumber();
            System.Diagnostics.Debug.WriteLine("TP SUCCESS RESPONSE: ", TPAPIResponse);

            if (TPAPIResponse != null)
            {
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
                viewModel.BtnEnableFlag = false;
                //bool PWDSuccess = await APIManager.VarifyMobileNumber(viewModel.CurrentMobileNumberEntryText, viewModel.NewMobileNumberEntryText);
                bool PWDSuccess = await viewModel.VarifyMobileNumber();
                System.Diagnostics.Debug.WriteLine("OTP SUCCESS: ", PWDSuccess);

                if (PWDSuccess)
                {
                    var result = Regex.Match(viewModel.NewMobileNumberEntryText, UtilityManager.MobileNumberLastThreeDigitsRegX);
                    viewModel.OTPSentOnThisMobileNumber = AppResources.MobileNumber + " ********" + result;

                    // * Start timer period for valid OTP
                    viewModel.StartOTPTimer();
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
                Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                {
                    await viewModel._dialogService.ShowMessageBox(validationError, AppResources.Information);
                });
                return false;
            }
        }

        private string VerifyCurrentAndNewMobilenNumbers(string CurrentMobileNumber, string NewMobileNumber)
        {
            if (CurrentMobileNumber == null || NewMobileNumber == null
                                            || CurrentMobileNumber == string.Empty
                                            || NewMobileNumber == string.Empty)
                return AppResources.EnterValidMobileNumber;
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

            // Show existing mobile
            var resultedNumber = Regex.Match(App.TP.Mobile, UtilityManager.MobileNumberRegX);
            viewModel.CurrentMobileNumberEntryText = resultedNumber.ToString();

            //IsLoading = false;
            RefreshControlsData();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            // * Worka aroung - Need to find a solution
            if(viewModel.countDownSeconds != 0 )
                viewModel.otpTimer.Stop();
        }

        // * // Reset Enteried
        private void RefreshControlsData()
        {
            viewModel.OTPFirstDigit = string.Empty;
            viewModel.OTPSecondDigit = string.Empty;
            viewModel.OTPThirdDigit = string.Empty;
            viewModel.OTPFourthDigit = string.Empty;
            viewModel.EnteredOTP = string.Empty;

            // Defualt
            viewModel.BtnEnableFlag = false;
            viewModel.IsLoading = false;

            viewModel.NewMobileNumberEntryText = string.Empty;
            btn.Text = "Update";
        }

        private void Mobile_entry_Unfocused(object sender, FocusEventArgs e)
        {
            StringBuilder Message = new StringBuilder();
            PopUp popUp = new PopUp();
            if (!string.IsNullOrEmpty(viewModel.NewMobileNumberEntryText))
            {

                if (viewModel.NewMobileNumberEntryText.Substring(0, 1) == "0")
                {
                    Message.Append(AppResources.ZZMobilenumberCannotStartWith0);
                }
                if (viewModel.NewMobileNumberEntryText.Length < 9)
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
                    Frm_mobile.HasError = true;
                    viewModel.NewMobileNumberEntryText = string.Empty;
                }
                else
                {
                    Frm_mobile.HasError = false;
                }
            }
            else
            {
                Message.Append(AppResources.EnterMobileNumber);
                popUp.Message = Message.ToString();
                PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
            }
        }
    }
    }
