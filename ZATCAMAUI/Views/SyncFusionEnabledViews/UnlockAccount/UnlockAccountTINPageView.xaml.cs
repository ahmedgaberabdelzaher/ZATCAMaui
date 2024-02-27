using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;
using System.Globalization;
using System.Text;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.UnlockAccount;
using ZATCAMAUI.Views.SyncFusionEnabledViews.AddPopPages;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.UnlockAccount
{

    public partial class UnlockAccountTINPageView : PopupPage
    {
        UnlockAccountTINPageViewModel viewModel;


        public UnlockAccountTINPageView()
        {
            try
            {
                InitializeComponent();

                viewModel = App.Locator.UnlockAccountTINPageViewModel;
                this.BindingContext = viewModel;

                SetLTR();
                ChangeAeroIcon();
                OtpGAZTDarkGrayLabelStyleFourthEntry.Text = string.Empty;
                viewModel.EnableTINView();
                Task.Run(async () => { await viewModel.GetCaptchAndGUID(); });
            }
            catch (Exception)
            {

            }
            
        }

        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["Back"];
            }
        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
                //Image_backArrow.Rotation = 0;
                //Label_MobileInitialAr.IsVisible = false;
                //Label_MobileInitialEng.IsVisible = true;
                //TINEntry.HorizontalTextAlignment = TextAlignment.Start;
                CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
            }
        }

        private void TappedOnTinContentBackButton(object sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                MessagingCenter.Send("UnlockAccountBackButtonClicked", "UnlockAccountBackButtonClicked");
                PopupNavigation.Instance.PopAsync();
            });
        }



        private void TappedOnOtpContentBackButton(object sender, EventArgs e)
        {
            viewModel.EnableTINView();
        }

        private void TappedOnChangePasswordContentBackButton(object sender, EventArgs e)
        {
            viewModel.EnableTINView();
        }

        void FrmTIN_Unfocused(object sender, FocusEventArgs e)
        {
            //ValidateTinEntryAndVerify();
        }

        void Otp_Unfocused(object sender,FocusEventArgs e)
        {
        }

        void btnVerify_Clicked(object sender, EventArgs e)
        {
            ValidateTinEntryAndVerify(true);
        }

        void FrmTIN_Focused(object sender, FocusEventArgs e)
        {
        }

        void OtpFourthEntry_Unfocused(object sender, FocusEventArgs e)
        {

        }

        void btnConfirmOtp_Clicked(object sender, EventArgs e)
        {

        }

        private void EntryTINNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(EntryTIN.Text))
            {
                FrmTIN.HasError = false;
            }
            else
            {
                if (EntryTIN.Text.Substring(0, 1) != "3")
                {
                    if (EntryTIN.Text.Length != 10)
                    {
                        FrmTIN.HasError = true;
                    }
                }
            }
        }

        private void ImageSeePassword_Tapped(object sender, EventArgs e)
        {
            if (viewModel.IsPasswordEncripted)
            {
                viewModel.IsPasswordEncripted = false;
            }
            else
            {
                viewModel.IsPasswordEncripted = true;
            }
        }

        private void ImageSeeConfirmPassword_Tapped(object sender, EventArgs e)
        {
            if (viewModel.IsConfirmPasswordEncripted)
            {
                viewModel.IsConfirmPasswordEncripted = false;
            }
            else
            {
                viewModel.IsConfirmPasswordEncripted = true;
            }

        }

        private void EntryConfirmPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryConfirmPassword.Text))
            {
                if (viewModel.Password != viewModel.ConfirmPassword)
                {
                    viewModel.FrameConfirmPasswordError = true;
                    //frmCfrmPass.HasError = true;
                }
                else
                {
                    viewModel.FrameConfirmPasswordError = false;
                    //frmCfrmPass.HasError = false;
                }
            }
        }

        private async void ValidateTinEntryAndVerify(bool isVerifyBtnClicked = false)
        {
            PopUp popUp = new PopUp();
            StringBuilder Messages = new StringBuilder();
            if (!string.IsNullOrEmpty(EntryTIN.Text))
            {
                if (EntryTIN.Text.Substring(0, 1) != "3")
                {
                    Messages.Append(AppResources.ZZTINnumberhastostartwithnumber3);
                    EntryTIN.Focus();
                }
                if (EntryTIN.Text.Length != 10)
                {
                    if (Messages.Length > 0)
                    {
                        Messages.Append(Environment.NewLine);
                    }
                    Messages.Append(AppResources.ZZTINnumberlengthcannotbelessthan10digits);
                }
                if (Messages.Length > 0)
                {
                    popUp.Message = Messages.ToString();
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
                    EntryTIN.Text = string.Empty;
                }
                else
                {
                    FrmTIN.HasError = false;
                    if (isVerifyBtnClicked == true)
                    {
                        await Task.Run(() =>
                        {
                            OtpFirstEntry.Unfocus();
                        });

                        viewModel.VerifyTinBtnCommand();
                    }
                }
            }
            else
            {
                FrmTIN.HasError = true;
                Messages.Append(AppResources.AccountUnlockedCompleteRequiedFields);

                popUp.Message = Messages.ToString();
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

               await PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                EntryTIN.Text = string.Empty;
            }
        }

        void OtpFirstEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.OtpFirstDigit.Length > 0)
            {
                OtpSecondEntry.Focus();
            }
        }

        void OtpSecondEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.OtpSecondDigit.Length > 0)
            {
                OtpThirdEntry.Focus();
            }
        }

        void OtpThirdEntry_TextChanged(object sender,TextChangedEventArgs e)
        {
            if (viewModel.OtpThirdDigit.Length > 0)
            {
                OtpGAZTDarkGrayLabelStyleFourthEntry.Focus();
            }
        }

        public void OtpGAZTDarkGrayLabelStyleFourthEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.OtpFourthDigit.Length > 0)
            {
                OtpGAZTDarkGrayLabelStyleFourthEntry.Unfocus();
            }





        }

        // * Password Validation
        void NewPassword_TextChanged(object sender, FocusEventArgs e)
        {
            ResetPasswordValidationConditions();

            bool ValidPassword = UtilityManager.ValidateNewPassword(viewModel.Password);

            if (ValidPassword)
            {
                viewModel.MinEight = "check_oval";
                viewModel.CapsSmall = "check_oval";
                viewModel.MaxSixteen = "check_oval";
                viewModel.NumSymbol = "check_oval";
            }
            else
            {
                if (UtilityManager.ValidMinEight) { viewModel.MinEight = "check_oval"; }
                if (UtilityManager.ValidSmallL && UtilityManager.ValidCapsL) { viewModel.CapsSmall = "check_oval"; }
                if (UtilityManager.ValidMaxSixteen) { viewModel.MaxSixteen = "check_oval"; }
                if (UtilityManager.ValidNumber && UtilityManager.ValidSymbol) { viewModel.NumSymbol = "check_oval"; }
            }
        }

        void ResetPasswordValidationConditions()
        {
            viewModel.MinEight = "error";
            viewModel.CapsSmall = "error";
            viewModel.MaxSixteen = "error";
            viewModel.NumSymbol = "error";
        }
    }
}
