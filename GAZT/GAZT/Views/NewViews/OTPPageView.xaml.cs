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
using Plugin.FilePicker;

namespace GAZT.Views.NewViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OTPPageView : ContentPage
    {

        #region Variable
        OTPPageViewModel viewModel;
        double DeviceHeight;
        double DeviceWidth;
        byte[] data;
        #endregion

        #region Constructor
        public OTPPageView(NavigateToOtp e)
        {

            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            viewModel = App.Locator.OTPPageView;

            SetLTR();
            viewModel.OTPValidDuration = "00:00";
            viewModel.OnPageLoad();
            this.BindingContext = viewModel;

            viewModel.IsComingFrom = e;
            if (e == NavigateToOtp.IsMobile)
            {
                if (App.TP != null)
                {
                    viewModel.EmailOrMobileNumber = AppResources.MobileNumber;
                    viewModel.OTPSentOnThisMobileNumber = App.TP.NewMobile;
                    var MobileNumber = viewModel.OTPSentOnThis;
                    MobileNumber = viewModel.OTPSentOnThisMobileNumber.Substring(5, 8);
                    var firstDigits = MobileNumber.Substring(0, 2);
                    var lastDigits = MobileNumber.Substring(MobileNumber.Length - 4, 4);
                    MobileNumber = "+9665" + MobileNumber;
                    string _mobileNumber = App.TP.NewMobile.Substring(App.TP.Mobile.Length - 3);
                    viewModel.MobileNumber = "XXXXXXXXXX" + _mobileNumber;
                    var requiredMask = new String('*', MobileNumber.Length - firstDigits.Length - lastDigits.Length);

                    var maskedString = string.Concat(firstDigits, requiredMask, lastDigits);
                    var maskedCardNumberWithSpaces = Regex.Replace(maskedString, ".{4}", "$0 ");
                    if (App.IsArabic)
                    {
                        //if (Device.RuntimePlatform == Device.iOS)
                        //{
                        viewModel.OTPSentOnThisText = AppResources.EnterVerificationCode + lastDigits + "***" + firstDigits;
                        //}
                        //else
                        //{
                        //    viewModel.OTPSentOnThisText = AppResources.EnterVerificationCode + firstDigits + "***" + lastDigits;

                        //}
                    }
                    else
                    {
                        viewModel.OTPSentOnThisText = AppResources.EnterVerificationCode + firstDigits + "***" + lastDigits;
                    }
                }
            }
            else if (e == NavigateToOtp.IsEmail)
            {
                viewModel.EmailOrMobileNumber = AppResources.Email;
                if (App.TP != null)
                {
                    string _newEmail = App.TP.NewEmail.Substring(App.TP.Mobile.Length - 4);
                    viewModel.MobileNumber = App.TP.NewEmail;// "XXXXXXXXXX" + _mobileNumber;
                    viewModel.OTPSentOnThisText = AppResources.EnterVerificationCodeForEmail;
                    viewModel.OTPSentOnThisEmail = App.TP.NewEmail;
                    viewModel.OTPSentOnThisText = viewModel.OTPSentOnThisText + " " + viewModel.OTPSentOnThisEmail;
                }
            }
            else if (e == NavigateToOtp.IsLogin)
            {
                if (App.TP != null)
                {
                    viewModel.EmailOrMobileNumber = AppResources.MobileNumber;
                    viewModel.OTPSentOnThisText = AppResources.EnterVerificationCode;
                    viewModel.OTPSentOnThisMobileNumber = App.TP.Mobile;
                    viewModel.OTPSentOnThis = viewModel.OTPSentOnThisMobileNumber;
                    var MobileNumber = viewModel.OTPSentOnThis;

                    MobileNumber = MobileNumber.Substring(MobileNumber.Length - 9);
                    var firstDigits = MobileNumber.Substring(0, 2);
                    var lastDigits = MobileNumber.Substring(MobileNumber.Length - 4, 4);

                    var requiredMask = new String('*', MobileNumber.Length - firstDigits.Length - lastDigits.Length);

                    string maskedString = string.Concat(firstDigits, requiredMask, lastDigits);
                    var maskedCardNumberWithSpaces = Regex.Replace(maskedString, ".{4}", "$0 ");
                    if (App.IsArabic)
                    {
                        //if (Device.RuntimePlatform == Device.iOS)
                        //{
                        viewModel.OTPSentOnThisText = AppResources.EnterVerificationCode + " : " + lastDigits + "***" + firstDigits;
                        //}
                        //else
                        //{
                        //    viewModel.OTPSentOnThisText = AppResources.EnterVerificationCode + " : " + firstDigits + "***" + lastDigits;
                        //}
                    }
                    else
                    {
                        viewModel.OTPSentOnThisText = AppResources.EnterVerificationCode + " : " + firstDigits + "***" + lastDigits;
                    }
                }
                DeviceWidth = DependencyService.Get<IDeviceInfo>().GetDeviceWidth();
                DeviceHeight = DependencyService.Get<IDeviceInfo>().GetDeviceHeight();
            }
        }


        #endregion

        #region Method
        #endregion

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            App.IsOTPiew = true;
            viewModel.ButtonDisableColor = Color.FromHex("#9EA4A9");
            viewModel.IsResendOTPEnabled = false;
            viewModel.IsOTPEntryEnable = true;
            viewModel.currentAttempts = 0;
            await Task.Run(() =>
            {

                Task.Delay(100);

            });
        }
        private async void OnOTPEntered(Object sender, EventArgs e)
        {
            string Otp = EnteredOTP.Text;
            //if(Otp.Length > 0)
            //{
            //    bool isValidNumber = UtilityManager.IsOTPNumberValid(Otp);
            //    if(!isValidNumber)
            //    {
            //        EnteredOTP.Text = Otp.Substring(0, Otp.Length-1);
            //    }
            //}
            //if (Otp.Length > 4)
            //{
            //    EnteredOTP.Text = EnteredOTP.Text.Substring(0, 4);
            //    EnteredOTP.Unfocus();
            //}
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
            viewModel.StopTimer = false;
            App.IsOTPiew = false;

            //for (int index = Navigation.NavigationStack.Count - 2; index > 1; index--)
            //{
            //    Page pg = Navigation.NavigationStack[index];
            //    Navigation.RemovePage(pg);
            //}
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                //var fileData = await CrossFilePicker.Current.PickFile();
                //data = fileData.DataArray;
                //lbl.Text = fileData.FileName;
                //AttachmentRootOject _attachment =  await WebServiceManager.GAZTSaveVATDeclarationAttachment(data);


            }
            catch (Exception ex)
            {


            }
        }

        protected override bool OnBackButtonPressed()
        {
            // Begin an asyncronous task on the UI thread because we intend to ask the users permission.
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (await DisplayAlert("Exit page?", "Are you sure you want to exit this page? You will not be able to continue it.", "Yes", "No"))
                {
                    base.OnBackButtonPressed();

                    //await App.Navigation.PopAsync();
                }
            });

            // Always return true because this method is not asynchronous.
            // We must handle the action ourselves: see above.
            return true;
        }
    }
}