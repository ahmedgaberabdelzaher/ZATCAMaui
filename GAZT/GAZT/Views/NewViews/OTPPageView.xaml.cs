using GAZT.Models;
using GAZT.ViewModel.NewViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GAZT.Helper;
using GAZT.Manager;

namespace GAZT.Views.NewViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OTPPageView : ContentPage
    {

        #region Variable
        OTPPageViewModel viewModel;
        double DeviceHeight;
        double DeviceWidth;
        #endregion

        #region Constructor
        public OTPPageView(NavigateToOtp e)
        {
            viewModel = App.Locator.OTPPageView;
            InitializeComponent();
            SetLTR();
            this.BindingContext = viewModel;

            viewModel.IsComingFrom = e;
            if (e == NavigateToOtp.IsMobile)
            {
                viewModel.OTPSentOnThisMobileNumber = App.TP.NewMobile;
                var MobileNumber = viewModel.OTPSentOnThis;
                MobileNumber = viewModel.OTPSentOnThisMobileNumber.Substring(5, 9);
                var firstDigits = MobileNumber.Substring(0, 2);
                var lastDigits = MobileNumber.Substring(MobileNumber.Length - 4, 4);
                MobileNumber = "00966" + MobileNumber;
                var requiredMask = new String('*', MobileNumber.Length - firstDigits.Length - lastDigits.Length);

                var maskedString = string.Concat(firstDigits, requiredMask, lastDigits);
                var maskedCardNumberWithSpaces = Regex.Replace(maskedString, ".{4}", "$0 ");
                if (App.IsArabic)
                {
                    viewModel.OTPSentOnThisText = AppResources.EnterVerificationCode + firstDigits + "***" + lastDigits;
                }
                else
                {
                    viewModel.OTPSentOnThisText = AppResources.EnterVerificationCode + firstDigits + "***" + lastDigits;
                }
            }
            else if (e == NavigateToOtp.IsEmail)
            {
                viewModel.OTPSentOnThisText = AppResources.EnterVerificationCodeForEmail;
                viewModel.OTPSentOnThisEmail = App.TP.NewEmail;
                viewModel.OTPSentOnThisText = viewModel.OTPSentOnThisText + " " + viewModel.OTPSentOnThisEmail;
            }
            else if (e == NavigateToOtp.IsLogin)
            {
                viewModel.OTPSentOnThisText = AppResources.EnterVerificationCode;
                viewModel.OTPSentOnThisMobileNumber = App.TP.Mobile;
                viewModel.OTPSentOnThis = viewModel.OTPSentOnThisMobileNumber;
                var MobileNumber = viewModel.OTPSentOnThis;

                MobileNumber = MobileNumber.Substring(5, 9);
                var firstDigits = MobileNumber.Substring(0, 2);
                var lastDigits = MobileNumber.Substring(MobileNumber.Length - 4, 4);

                var requiredMask = new String('*', MobileNumber.Length - firstDigits.Length - lastDigits.Length);

                string maskedString = string.Concat(firstDigits, requiredMask, lastDigits);
                var maskedCardNumberWithSpaces = Regex.Replace(maskedString, ".{4}", "$0 ");
                if (App.IsArabic)
                {
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        viewModel.OTPSentOnThisText = AppResources.EnterVerificationCode + " : " + lastDigits  + "***" + firstDigits;
                    }
                    else
                    {
                        viewModel.OTPSentOnThisText = AppResources.EnterVerificationCode + " : " + firstDigits + "***" + lastDigits;
                    }
                }
                else
                {
                    viewModel.OTPSentOnThisText = AppResources.EnterVerificationCode + " : " + firstDigits + "***" + lastDigits;
                }
            }
            DeviceWidth = DependencyService.Get<IDeviceInfo>().GetDeviceWidth();
            DeviceHeight = DependencyService.Get<IDeviceInfo>().GetDeviceHeight();
        }
        #endregion

        #region Method
        #endregion

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            App.IsOTPiew = true;
            await Task.Run(() =>
            {

                Task.Delay(100);
               
            });
        }
        private async void OnOTPEntered(Object sender, EventArgs e)
        {
            string Otp = EnteredOTP.Text;
            if(Otp.Length > 0)
            {
                bool isValidNumber = UtilityManager.IsOTPNumberValid(Otp);
                if(!isValidNumber)
                {
                    EnteredOTP.Text = Otp.Substring(0, Otp.Length-1);
                }
            }
            if (Otp.Length > 4)
            {
                EnteredOTP.Text = EnteredOTP.Text.Substring(0, 4);
                EnteredOTP.Unfocus();
            }
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel.ClearData();
            App.IsOTPiew = false;

            //for (int index = Navigation.NavigationStack.Count - 2; index > 1; index--)
            //{
            //    Page pg = Navigation.NavigationStack[index];
            //    Navigation.RemovePage(pg);
            //}
        }
     
    }
}