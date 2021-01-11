using EGAZT.ViewModel.NewDesignViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using GAZT.Manager;
using GAZT.Models;
using GAZT.CustomControl;
using MVP.FontIcons;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.PlatformConfiguration;

namespace EGAZT.Views.NewDesign.ForgotPasswordPages
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GAZTNewDesignForgotPasswordPageView : ContentPage
    {

        private string _LblCountDownTimer;

        GAZTNewDesignForgotPasswordPageViewModel viewModel;
        public GAZTNewDesignForgotPasswordPageView()
        {

            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, " ");
           
            viewModel = App.Locator.GAZTNewDesignForgotPasswordPageView;
            this.BindingContext = viewModel;
            viewModel.ClearData();
            viewModel.OnPageLoad();
            SetLTR();
            viewModel.ContinueORConfirmButtonText = AppResources.ZZZZContinue;
            SetPickerFont();
            viewModel.StartPage = 1;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
           
        }

        public void SetPickerFont()
        {
            try
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {

                    case Xamarin.Forms.Device.iOS:
                        {

                            Picker_Tins.HeaderFontFamily = "SSTArabic-Medium";
                            Picker_Tins.ColumnHeaderFontFamily = "SSTArabic-Medium";
                            Picker_Tins.SelectedItemFontFamily = "SSTArabic-Medium";
                            Picker_Tins.UnSelectedItemFontFamily = "SSTArabic-Medium";//ddlLIssuedBy
                        }
                        break;
                    case Xamarin.Forms.Device.Android:
                        Picker_Tins.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        Picker_Tins.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        Picker_Tins.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        Picker_Tins.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";//ddlLIssuedBy 

                        break;
                }
            }
            catch (Exception ex)
            {

            }

        }
        private void SetLTR()
        {
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        private void OnPasswordCardClicked(object sender, EventArgs e)
        {

            viewModel.IsUserNameCardTapped = false;
            viewModel.IsPasswordCardTapped = true;

            viewModel.SetPasswordCardLayoutVisibility();
            viewModel.PasswordCardBackgroundImg = "FP_selected_tile";
            viewModel.UserNameCardBackgroundImg = "FP_unselected_tile";
            viewModel.PasswordTextColor = Color.White;
            viewModel.UserNameTextColor = Color.Black;
            viewModel.SetIDNumberEnability = true;// Enabling IDNumber Field as per tapping on Password Tile

            // Managing UserName tile
            viewModel.MaximumUserNameCharacter = 10;
            viewModel.CorporateCardBackgroundImg = "FP_unselected_tile";
            viewModel.CorporateTextColor = Color.Black;
            viewModel.IndividualOrPersonalBusinessCardBackgroundImg = "FP_selected_tile";
            viewModel.IndividualOrPersonalBusinessTextColor = Color.White;
            viewModel.UserIcon = "vat_user";
            viewModel.PasswordIcon = "password_key";


            viewModel.Email = string.Empty;
            viewModel.TxtTIN = string.Empty;
            if (viewModel.TINs != null && viewModel.TINs.Count > 0)
                viewModel.TINs.Clear();
            viewModel.IDNumber = string.Empty;
            viewModel.UserNameLabelText = AppResources.UserName;

            viewModel.ContinueORConfirmButtonText = AppResources.ZZZZContinue;



        }


        private void OnUserNameCardClicked(object sender, EventArgs e)
        {
            viewModel.IsUserNameCardTapped = true;
            viewModel.IsPasswordCardTapped = false;
            viewModel.SetUserNameCardVisibility();
            viewModel.PasswordCardBackgroundImg = "FP_unselected_tile";
            viewModel.UserNameCardBackgroundImg = "FP_selected_tile";
            viewModel.UserIcon = "user_profile";
            viewModel.PasswordIcon = "Green_Key";
            viewModel.PasswordTextColor = Color.Black;
            viewModel.UserNameTextColor = Color.White;
            viewModel.SetIDNumberEnability = true;// Enabling IDNumber Field as per tapping on Password Tile
            
            viewModel.IndividualOrPersonalBusinessCardBackgroundImg = "FP_selected_tile";
            viewModel.IndividualOrPersonalBusinessTextColor = Color.White;

            viewModel.Email = string.Empty;
            viewModel.TxtTIN = string.Empty;
            if (viewModel.TINs != null && viewModel.TINs.Count > 0)
                viewModel.TINs.Clear();
            viewModel.IDNumber = string.Empty;
            viewModel.UserNameLabelText = AppResources.IDNumber;
            viewModel.ContinueORConfirmButtonText = AppResources.Confirm;


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
            catch (Exception ex)
            {
            }

        }

        private void Btn_TinPicker_Clicked(object sender, EventArgs e)
        {
            Picker_Tins.IsOpen = true;
        }

        private void Picker_Tins_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            TIN selectedTinId = (TIN)e.NewValue;
            Picker_Tins.SelectedItem = selectedTinId;
            viewModel.SelectedTinId = selectedTinId;
        }


        // * Forgot password : OTP Verification :
        void OtpFirstEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (viewModel.OTPFirstDigit.Length > 0)
            {
                OTPSecondEntry.Focus();
            }
        }

        void OtpSecondEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (viewModel.OTPSecondDigit.Length > 0)
            {
                OTPThirdEntry.Focus();
            }
        }

        void OnIdNumberTextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (viewModel.CorporateCardBackgroundImg.Equals("FP_selected_tile"))
            {
               
                
            }
        }
        

        void OtpThirdEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (viewModel.OTPThirdDigit.Length > 0)
            {
                OTPFourthEntry.Focus();
            }
        }

        void OtpFourthEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {

        }

        void OtpFourthEntry_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {

        }


        // * Password Validation
        void NewPassword_TextChanged(object sender, FocusEventArgs e)
        {
            this.ResetPasswordValidationConditions();

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
        void UserNameTextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
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
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;


            if (Device.RuntimePlatform == Device.Android)
            {
                Picker_Tins.BackgroundColor = Color.FromHex("#f7f7f7");
            }
            else
            {
                Picker_Tins.BackgroundColor = Color.FromHex("#FFFFFF");
            }

            // Reset values
            viewModel.currentAttempts = 0;
            viewModel.Enabled = true;

            NewPasswordIcon.Source = "hidePassword";
            ConfirmNewPasswordIcon.Source = "hidePassword";
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
            viewModel._navigationService.GoBack();
        }
    }
}