using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Syncfusion.Maui.Picker;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using Application = Microsoft.Maui.Controls.Application;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

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
                NavigationPage.SetBackButtonTitle(this, " ");

                viewModel = App.Locator.GAZTNewDesignForgotPasswordPageView;
                BindingContext = viewModel;
                viewModel.ClearData();
                viewModel.OnPageLoad();
                viewModel.ContinueORConfirmButtonText = AppResources.ZZZZContinue;
                SetPickerFont();
                viewModel.StartPage = 1;
                On<iOS>().SetUseSafeArea(true);
            }
            catch (Exception)
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

                            Picker_Tins.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            Picker_Tins.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            Picker_Tins.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            Picker_Tins.TextStyle.FontFamily = "Somar-SemiBold";//ddlLIssuedBy
                        }
                        break;
                    case Device.Android:
                        Picker_Tins.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";//"Somar-SemiBold.otf#Somar-SemiBold";
                        Picker_Tins.ColumnHeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        Picker_Tins.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        Picker_Tins.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";//ddlLIssuedBy 

                        break;
                }
            }
            catch (Exception)
            {


            }

        }

        private void OnPasswordCardClicked(object sender, EventArgs e)
        {

            viewModel.IsUserNameCardTapped = false;

            viewModel.IsPasswordCardTapped = true;

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

            viewModel.GetCaptchAndGUID();

            viewModel.ContinueORConfirmButtonText = AppResources.ZZZZContinue;


            viewModel.IsContinueButtonVisibe = viewModel.ValidateFirstStep();
        }


        private async void OnUserNameCardClicked(object sender, EventArgs e)
        {
            viewModel.IsUserNameCardTapped = true;
            viewModel.IsPasswordCardTapped = false;
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

            await viewModel.GetCaptchAndGUID();


        }

        private void CustomLabel_Unfocused(object sender, FocusEventArgs e)
        {

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
                TIN selectedTinId = viewModel.TINs[e.NewValue];
                //Picker_Tins.Columns[0].SelectedIndex = e.NewValue;
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

        void OnIdNumberTextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.CorporateCardBackgroundImg.Equals("FP_selected_tile"))
            {


            }
        }


        void OtpThirdEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.OTPThirdDigit.Length > 0)
            {
                OTPFourthEntry.Focus();
            }
        }

        void OtpFourthEntry_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        void OtpFourthEntry_Unfocused(object sender, FocusEventArgs e)
        {
            viewModel.OtpFilled();
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

                var safeInsets = On<iOS>().SafeAreaInsets();
                safeInsets.Bottom = -10;
                Padding = safeInsets;


                if (Device.RuntimePlatform == Device.Android)
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

        private void OnBackTapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync(true);
            //viewModel._navigationService.GoBack();
        }

        void Btn_TinPicker_Clicked_1(object sender, EventArgs e)
        {
            Picker_Tins.IsOpen = true;

        }
    }
}