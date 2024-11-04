using Syncfusion.Maui.Picker;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.NewDesign.ForgotPasswordPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GAZTNewDesignForgotPasswordPageView : ContentPage
    {

        GAZTNewDesignForgotPasswordPageViewModel viewModel;
        public GAZTNewDesignForgotPasswordPageView()
        {
            try
            {
                InitializeComponent();

                viewModel = App.Locator.GAZTNewDesignForgotPasswordPageView;
                BindingContext = viewModel;
                viewModel.ClearData();
                viewModel.ContinueORConfirmButtonText = AppResources.ZZZZContinue;
                viewModel.StartPage = 1;
            }
            catch (Exception)
            {

            }
        }

        

        private void OnPasswordCardClicked(object sender, EventArgs e)
        {

            viewModel.IsUserNameCardTapped = false;

            viewModel.IsPasswordCardTapped = true;
            viewModel.UserIDLayoutVisibility = false;
            viewModel.SetPasswordCardLayoutVisibility();
            viewModel.PasswordCardBackgroundImg = "FP_selected_tile";
            viewModel.UserNameCardBackgroundImg = "FP_unselected_tile";
            viewModel.PasswordTextColor = Colors.White;
            viewModel.UserNameTextColor = (Color)Application.Current.Resources["Primary"];
            viewModel.SetIDNumberEnability = true;// Enabling IDNumber Field as per tapping on Password Tile

            // Managing UserName tile
            viewModel.MaximumUserNameCharacter = 10;
            viewModel.CorporateCardBackgroundImg = "FP_unselected_tile";
            viewModel.CorporateTextColor = (Color)Application.Current.Resources["Primary"];
            viewModel.IndividualOrPersonalBusinessCardBackgroundImg = "FP_selected_tile";
            viewModel.IndividualOrPersonalBusinessTextColor = Colors.White;
            viewModel.UserIcon = "vat_user";
            viewModel.PasswordIcon = "password_key";


            viewModel.Email = string.Empty;
            viewModel.TxtTIN = string.Empty;
            if (viewModel.TINs != null && viewModel.TINs.Count > 0)
                viewModel.TINs.Clear();
            viewModel.IDNumber = string.Empty;
            viewModel.UserNameLabelText = AppResources.UserName;

            _ = viewModel.GetCaptchImage(ZATCAConstants.FUSR);

            viewModel.ContinueORConfirmButtonText = AppResources.ZZZZContinue;


            viewModel.IsContinueButtonVisibe = viewModel.ValidateFirstStep();
        }


        private async void OnUserNameCardClicked(object sender, EventArgs e)
        {

            viewModel.IsPasswordCardTapped = false;
            viewModel.IsUserNameCardTapped = true;
            viewModel.UserIDLayoutVisibility = true;
            viewModel.SetUserNameCardVisibility();
            viewModel.PasswordCardBackgroundImg = "FP_unselected_tile";
            viewModel.UserNameCardBackgroundImg = "FP_selected_tile";
            viewModel.UserIcon = "user_profile";
            viewModel.PasswordIcon = "Green_Key";
            viewModel.PasswordTextColor = (Color)Application.Current.Resources["Primary"];
            viewModel.UserNameTextColor = Colors.White;
            viewModel.SetIDNumberEnability = true;// Enabling IDNumber Field as per tapping on Password Tile

            viewModel.IndividualOrPersonalBusinessCardBackgroundImg = "FP_selected_tile";
            viewModel.IndividualOrPersonalBusinessTextColor = Colors.White;

            viewModel.Email = string.Empty;
            viewModel.TxtTIN = string.Empty;
            if (viewModel.TINs != null && viewModel.TINs.Count > 0)
                viewModel.TINs.Clear();
            viewModel.IDNumber = string.Empty;
            viewModel.UserNameLabelText = AppResources.IDNumber;
            viewModel.ContinueORConfirmButtonText = AppResources.Confirm;
            viewModel.IsContinueButtonVisibe = viewModel.ValidateFirstStep();

            _ = viewModel.GetCaptchImage(ZATCAConstants.FPWD);


        }

        private async void Entry_UserName_Unfocused(object sender, FocusEventArgs e)
        {

            try
            {

                string userName = viewModel.Email;
                if (!string.IsNullOrEmpty(userName))
                    viewModel.IsValiedEmailAddress = UtilityManager.IsValidEmailAddress(userName);
                if (!viewModel.IsValiedEmailAddress)
                {
                    viewModel.IsVisibleTinIds = false;
                }
                await viewModel.SetTinsListLayoutVisibility(viewModel.IsValiedEmailAddress);


            }
            catch (Exception)
            {


            }

        }

        private void Btn_TinPicker_Clicked(object sender, EventArgs e)
        {
            Picker_Tins.IsOpen = true;
        }

        private void Picker_Tins_SelectionChanged(object sender, PickerSelectionChangedEventArgs e)
        {

            try
            {
                //TODO
                TINModel selectedTinId = viewModel.TINs[e.NewValue];
                viewModel.SelectedTinId = selectedTinId;
            }
            catch (Exception)
            {


            }
        }


        // * Forgot password : OTP Verification :
        void OtpFirstEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.OTPFirstDigit.Length > 0)
            {
                OTPSecondEntry.Focus();
            }
        }

        void OtpSecondEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.OTPSecondDigit.Length > 0)
            {
                OTPThirdEntry.Focus();
            }
        }


        void OtpThirdEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.OTPThirdDigit.Length > 0)
            {
                OTPFourthEntry.Focus();
            }
        }

        // * Password Validation
        void NewPassword_TextChanged(object sender, FocusEventArgs e)
        {
            ResetPasswordValidationConditions();

            bool ValidPassword = UtilityManager.ValidateNewPassword(viewModel.NewPassword);

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

        // * Managing Tins drop down visibility
        void UserNameTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(viewModel.Email))
            {
                viewModel.IsTinDopDownVisible = false;
                viewModel.UserNameLabelText = AppResources.IDNumber;
            }
        }
        void ResetPasswordValidationConditions()
        {
            viewModel.MinEight = "error";
            viewModel.CapsSmall = "error";
            viewModel.MaxSixteen = "error";
            viewModel.NumSymbol = "error";
        }


        // * New Password - Confirm New Password : Show / Hide
        void OnNewPasswordTapped(object sender, EventArgs args)
        {
            if (NewPassword.IsPassword == true)
            {
                NewPassword.IsPassword = false;
                NewPasswordIcon.Source = "showPassword";
            }
            else
            {
                NewPassword.IsPassword = true;
                NewPasswordIcon.Source = "hidePassword";
            }
        }

        void OnConfirmNewPasswordTapped(object sender, EventArgs args)
        {
            if (ConfirmNewPassword.IsPassword == true)
            {
                ConfirmNewPassword.IsPassword = false;
                ConfirmNewPasswordIcon.Source = "showPassword";
            }
            else
            {
                ConfirmNewPassword.IsPassword = true;
                ConfirmNewPasswordIcon.Source = "hidePassword";
            }
        }


        protected override void OnAppearing()
        {
            try
            {


                base.OnAppearing();


                if (DeviceInfo.Platform == DevicePlatform.Android)
                {
                    Picker_Tins.BackgroundColor = (Color)Application.Current.Resources["PickerBgGray"];
                }
                else
                {
                    Picker_Tins.BackgroundColor = (Color)Application.Current.Resources["White"];
                }

                // Reset values
                viewModel.currentAttempts = 0;
                viewModel.Enabled = true;

                NewPasswordIcon.Source = "hidePassword";
                ConfirmNewPasswordIcon.Source = "hidePassword";
            }
            catch (Exception)
            {

            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            // * Worka aroung - Need to find a solution
            if (viewModel.countDownSeconds != 0)
                viewModel.otpTimer.Stop();
        }

        void Btn_TinPicker_Clicked_1(object sender, TappedEventArgs e)
        {
            Picker_Tins.IsOpen = true;

        }
        void RefreshCaptchaClicked(System.Object sender, System.EventArgs e)
        {
            _ = viewModel.IsPasswordCardTapped ? viewModel.GetCaptchImage(ZATCAConstants.FPWD) : viewModel.GetCaptchImage(ZATCAConstants.FUSR);
            viewModel.Captcha = string.Empty;
        }

    }
}