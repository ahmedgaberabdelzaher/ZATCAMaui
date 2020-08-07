using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel;
using GAZT;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Timers;
using System;
using GAZT.Manager;
using GAZT.Models;
using GAZT.CustomControl;

namespace EGAZT.Views.NewDesign.ForgotPasswordPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GAZTNewDesignForgotPasswordPageView : ContentPage
    {
        
        private string _LblCountDownTimer;

        GAZTNewDesignForgotPasswordPageViewModel viewModel;
        public GAZTNewDesignForgotPasswordPageView()
        {

            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, " ");

            viewModel = App.Locator.GAZTNewDesignForgotPasswordPageView;
            this.BindingContext = viewModel;
            viewModel.ClearData();
            viewModel.OnPageLoad();
            SetLTR();
            viewModel.StartPage = 1;
          
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

        private void OnPasswordCardClicked(object sender, EventArgs e)
        {
            viewModel.IsUserNameCardTapped = false;
            viewModel.IsPasswordCardTapped = true;
            
            viewModel.SetPasswordCardLayoutVisibility();
            viewModel.PasswordCardBackgroundColor = Color.FromHex("#005e4b");
            viewModel.UserNameCardBackgroundColor = Color.White;
            viewModel.PasswordTextColor = Color.White;
            viewModel.UserNameTextColor = Color.Black;
            viewModel.SetIDNumberEnability = true;// Enabling IDNumber Field as per tapping on Password Tile

            // Managing UserName tile
            viewModel.MaximumUserNameCharacter = 10;
            viewModel.CorporateCardBackgroundColor = Color.White;
            viewModel.CorporateTextColor = Color.Black;
            viewModel.IndividualOrPersonalBusinessCardBackgroundColor = Color.FromHex("#005e4b");
            viewModel.IndividualOrPersonalBusinessTextColor = Color.White;

        }


        private void OnUserNameCardClicked(object sender, EventArgs e)
        {
            viewModel.IsUserNameCardTapped = true;
            viewModel.IsPasswordCardTapped = false;
            viewModel.SetUserNameCardVisibility();
            viewModel.PasswordCardBackgroundColor = Color.White;
            viewModel.UserNameCardBackgroundColor = Color.FromHex("#005e4b");
            viewModel.PasswordTextColor = Color.Black;
            viewModel.UserNameTextColor = Color.White;
            viewModel.SetIDNumberEnability = true;// Enabling IDNumber Field as per tapping on Password Tile
        }

        private void CustomLabel_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private async  void Entry_UserName_Unfocused(object sender, FocusEventArgs e)
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
            Picker_Tins.IsOpen=true;
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
                viewModel.MinEight = Color.DarkGreen;
                viewModel.CapsSmall = Color.DarkGreen;
                viewModel.MaxSixteen = Color.DarkGreen;
                viewModel.NumSymbol = Color.DarkGreen;

                // check the new and confirm password condition
            }
            else
            {
                if (UtilityManager.ValidMinEight) { viewModel.MinEight = Color.DarkGreen; }
                if (UtilityManager.ValidSmallL && UtilityManager.ValidCapsL) { viewModel.CapsSmall = Color.DarkGreen; }
                if (UtilityManager.ValidMaxSixteen) { viewModel.MaxSixteen = Color.DarkGreen; }
                if (UtilityManager.ValidNumber && UtilityManager.ValidSymbol) { viewModel.NumSymbol = Color.DarkGreen; }
            }
        }

        // * Managing Tins drop down visibility
        void UserNameTextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if(string.IsNullOrEmpty(viewModel.Email))
            {
                viewModel.IsTinDopDownVisible = false;
            }
        }
        void ResetPasswordValidationConditions()
        {
            viewModel.MinEight = Color.DarkRed;
            viewModel.CapsSmall = Color.DarkRed;
            viewModel.MaxSixteen = Color.DarkRed;
            viewModel.NumSymbol = Color.DarkRed;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Reset values
            viewModel.currentAttempts = 0;
        }

    }
}