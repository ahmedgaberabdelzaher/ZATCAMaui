using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ChangeEmailPage;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.SyncFusionEnabledViews.ChangeEmailPages
{
    [Preserve(AllMembers = true)]
    public partial class UpdateEmailVerificationPage : ContentPage
    {
        #region Variable
        UpdateEmailVerificationPageViewModel viewModel;
        private double width = 0;
        private double height = 0;
        double DeviceHeight;
        double DeviceWidth;
        byte[] data;
        #endregion

        public UpdateEmailVerificationPage(ComingToOTPVerificationScreenFromAndNavigatingTo _ComingToOTPVerificationScreenFromAndNavigatingTo)
        {
            InitializeComponent();

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");

            ChangeAeroIcon();
            viewModel = App.Locator.UpdateEmailVerificationPage;
            viewModel.NavigateToOtpForEmailEnum = _ComingToOTPVerificationScreenFromAndNavigatingTo._ComingToOTPVerificationScreenFrom;

            viewModel.ComingToOTPVerificationScreenFromAndNavigatingTo = _ComingToOTPVerificationScreenFromAndNavigatingTo;
            try
            {
                viewModel.IsNumberOfAttemptTextVisible = false;

            }
            catch (Exception ex)
            {
            }
            NumberOfAttemptsText.Text = viewModel.ShowAccountWIllBeLockedMessage();
            SetLTR();
            viewModel.numberOfSeconds = 120;
            try
            {
                viewModel.OnPageLoad();
            }
            catch (GAZTException gex)
            {
                if (gex is GAZTMobileNumberInProfileEmptyException)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await viewModel._dialogService.ShowMessageBox(AppResources.MobileNumberIsMissingForEnteredTIN,
                            AppResources.Alerts);
                        viewModel._navigationService.GoBack();
                    });
                    return;
                }
            }
            catch (Exception)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await viewModel._dialogService.ShowMessageBox(AppResources.Somethingwentwrong,
                        AppResources.Alerts);
                    viewModel._navigationService.GoBack();
                });
                return;
            }
            this.BindingContext = viewModel;
            if (Device.RuntimePlatform == Device.Android)
            {
                DependencyService.Get<IStatusBar>().HideStatusBar();
            }
            viewModel.IsComingFrom = _ComingToOTPVerificationScreenFromAndNavigatingTo._ComingToOTPVerificationScreenFrom;
            if (_ComingToOTPVerificationScreenFromAndNavigatingTo._ComingToOTPVerificationScreenFrom ==
               ComingToOTPVerificationScreenFrom.IsEmail)
            {
                viewModel.EmailOrMobileNumber = AppResources.Email;
                if (App.TP != null)
                {
                    // string _newEmail = App.TP.NewEmail.Substring(App.TP.Mobile.Length - 4);
                    viewModel.MobileNumber = App.TP.NewEmail;// "XXXXXXXXXX" + _mobileNumber;
                    viewModel.OTPSentOnThisText = AppResources.EnterVerificationCodeForEmail;
                    viewModel.OTPSentOnThisEmail = App.TP.NewEmail;
                    viewModel.OTPSentOnThisText = viewModel.OTPSentOnThisText + " " + viewModel.OTPSentOnThisEmail;
                }
                if (!App.IsArabic)
                {
                    viewModel.AccountWillBeBlocked = "The account will be locked after entering " +
                        App.TP.Attempts + " wrong verification codes";
                }
                else
                {
                    viewModel.AccountWillBeBlocked = "سيتم قفل الحساب بعد إدخال" + " " +
                        UtilityManager.ConvertNumerals(App.TP.Attempts.ToString()) + " " + "رموز تحقق خاطئة";
                }
            }


        }
        #region Method
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width != this.width || height != this.height)
            {
                this.width = width;
                this.height = height;
                if (width > height)
                {
                    this.BackgroundImageSource = "sf_LoginBackgroundLand.png";
                }
                else
                {
                    this.BackgroundImageSource = "sf_LoginBackground.png";
                    //  outerStack.Orientation = StackOrientation.Vertical;
                }
            }
        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }
        #endregion
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            ChangeAeroIcon();
            if (viewModel.ComingToOTPVerificationScreenFromAndNavigatingTo._ComingToOTPVerificationScreenFrom ==
                ComingToOTPVerificationScreenFrom.IsTes)
            {
            }
            else
            {
                App.IsOTPiew = true;
                viewModel.TimerStart(viewModel.numberOfSeconds);
                viewModel.ButtonDisableColor = Color.FromHex("#9EA4A9");
                viewModel.IsResendOTPEnabled = false;
                viewModel.IsOTPEntryEnable = true;
                viewModel.currentAttempts = 0;

                viewModel.PasswordVisibilityForNewPassword = true;
                viewModel.PasswordVisibilityForOldPassword = true;
                viewModel.PasswordVisibilityForRetypePassword = true;
                await Task.Run(() =>
                {
                    Task.Delay(100);
                });
                EnteredOTP.Focus();
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
        public void OnPasswordVisibilityClickedForNewPassword(object sender, EventArgs args)
        {
            viewModel.PasswordVisibilityForNewPassword = !viewModel.PasswordVisibilityForNewPassword;
        }
        public void OnPasswordVisibilityClickedForOldPassword(object sender, EventArgs args)
        {
            viewModel.PasswordVisibilityForOldPassword = !viewModel.PasswordVisibilityForOldPassword;
        }
        public void OnPasswordVisibilityClickedForRetypePassword(object sender, EventArgs args)
        {
            viewModel.PasswordVisibilityForRetypePassword = !viewModel.PasswordVisibilityForRetypePassword;
        }
        public void OnPasswordFocused(object sender, EventArgs args)
        {
            //  Password.Unfocus();
        }
    }
}
