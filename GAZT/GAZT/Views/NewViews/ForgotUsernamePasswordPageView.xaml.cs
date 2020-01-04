using GAZT.ViewModel;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin.Forms;
using GAZT.ViewModel.NewViewModel;
using GAZT.Manager;

namespace GAZT.Views.NewViews
{
    public partial class ForgotUsernamePasswordPageView : ContentPage
    {
        ObservableCollection<String> Items = new ObservableCollection<String>();
        ForgotUsernamePasswordPageViewModel viewModel;
        public ForgotUsernamePasswordPageView()
        {
            viewModel = App.Locator.ForgotUsernamePasswordPageView;
            InitializeComponent();
            viewModel.NewPasswordVisibility = true;
            viewModel.ConfirmPasswordVisibility = true;
            viewModel.currentAttempts = 0;
            SetLTR();
            string str = "abc";
            Items.Add(str);
            App.IsComingFromDashboardToLogOff = false;
            try
            {
                this.BindingContext = viewModel;
                viewModel.OnPageLoad();
            }
            catch (Exception ex)
            {
            }
        }
        protected void OnSelectedTaxPAyerType(object sender, EventArgs e)  { }
        protected void OnSelectedForgetType(object sender, EventArgs e)
        {
        }

        protected async void OnUserNameUnFocussed(object sender, EventArgs e)
        {
            try
            {
                bool IsValiedEmailAddress = false;
                string userName = UserName.Text;
                if(!string.IsNullOrEmpty(userName))
                 IsValiedEmailAddress = UtilityManager.IsValidEmailAddress(userName);
                if (!IsValiedEmailAddress)
                {
                    viewModel.IsVisibleTinIds = false;
                }
                await viewModel.SetTinsListLayoutVisibility(IsValiedEmailAddress);
            }
            catch(Exception ex)
            {

            }
        }

        
       private async void OnIDNumberTextChanged(Object sender, EventArgs e)
       {

            if (viewModel.SelectedTaxPayerType != null)
            {
                string IdNumber = UserName.Text;
                if (IdNumber.Length > 0)
                {
                    bool isValidNumber = UtilityManager.IsOTPNumberValid(IdNumber);
                    if (!isValidNumber)
                    {
                        string _idNumber = IdNumber.Substring(0, IdNumber.Length - 1);
                        UserName.Text = _idNumber;
                    }
                }
                if(viewModel.SelectedTaxPayerType.id.Equals("1"))
                {
                    if (IdNumber.Length > 10)
                    {
                        UserName.Text = UserName.Text.Substring(0, 10);
                        UserName.Unfocus();
                    }
                }
                
            }
        }

        protected void OnUserNAmeFocused(object sender, EventArgs e)
        {
            if(viewModel.SelectedTaxPayerType != null)
            {
                UserName.Keyboard = Keyboard.Numeric;
            }
            else
            {
                UserName.Keyboard = Keyboard.Default;
            }

        }

        public void OnNewPasswordEyeClicked(object sender, EventArgs args)
        {
            viewModel.NewPasswordVisibility = !viewModel.NewPasswordVisibility;
        }
        public void OnConfirmPasswordEyeClicked(object sender, EventArgs args)
        {
            viewModel.ConfirmPasswordVisibility = !viewModel.ConfirmPasswordVisibility;
        }


        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.NewPassword = "";
            viewModel.ConfirmPassword = "";
            viewModel.EnteredOTP = "";
            viewModel.IDNumber = "";
            viewModel.NewPasswordLayoutVisibility = false;
            viewModel.OTPLayoutVisibility = false;
            viewModel.NavigateToLoginLinkVisibility = false;
            viewModel.EnteredCaptchaValue  = "";
            viewModel.IsVisibleTinIds = false;
            viewModel.IsIDTypeVisible = false;
            //viewModel.Captcha = "";


            //viewModel.IsTaxPayerTypeEnable = true;
            //viewModel.IsForgotUserNameWithIndividual = true;
            //viewModel.IsForgotPassword = false;
            //viewModel.IsForgotUserNameWithCorporate = false;

        }

        private async void OnOTPEntered(Object sender, EventArgs e)
        {
            string Otp = EnteredOTP.Text;
            if(Otp.Length > 4)
            {
                EnteredOTP.Text = EnteredOTP.Text.Substring(0, 4);
                EnteredOTP.Unfocus();
            }
        }

        }
}
