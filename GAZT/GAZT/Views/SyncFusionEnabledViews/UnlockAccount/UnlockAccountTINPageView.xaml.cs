using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.UnlockAccount;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GAZT.Models;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.Views.SyncFusionEnabledViews.UnlockAccount
{
    public partial class UnlockAccountTINPageView : PopupPage
    {
        UnlockAccountTINPageViewModel viewModel;

        public UnlockAccountTINPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.UnlockAccountTINPageViewModel;
            this.BindingContext = viewModel;
            
            SetLTR();
            ChangeAeroIcon();
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
                //Image_backArrow.Rotation = 180;
                //TINEntry.HorizontalTextAlignment = TextAlignment.End;
                CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                //Label_MobileInitialAr.IsVisible = true;
                //Label_MobileInitialEng.IsVisible = false;
            }
        }

        private async void TappedOnTinContentBackButton(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
            viewModel._navigationService.GoBack();
        }

        private void TappedOnOtpContentBackButton(object sender, EventArgs e)
        {
            viewModel.EnableTINView();
        }

        private void TappedOnChangePasswordContentBackButton(object sender, EventArgs e)
        {
            viewModel.EnableTINView();
        }

        void FrmTIN_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            ValidateTinEntryAndVerify();
        }

        void Otp_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            Console.WriteLine("Otp_Unfocused");
        }

        void btnVerify_Clicked(System.Object sender, System.EventArgs e)
        {
            ValidateTinEntryAndVerify(true);
        }

        void FrmTIN_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            Console.WriteLine("FrmTIN_Focused");
        }

        void OtpFourthEntry_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {

        }

        void btnConfirmOtp_Clicked(System.Object sender, System.EventArgs e)
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

        private void ValidateTinEntryAndVerify(bool isVerifyBtnClicked = false)
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
                    if(isVerifyBtnClicked == true)
                    {
                        viewModel.VerifyTinBtnCommand();
                    }
                }
            }
            else
            {
                FrmTIN.HasError = true;
                Messages.Append(AppResources.ZZPleasefillallthemandatoryfields);

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
        }

        void OtpFirstEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if(viewModel.OtpFirstDigit.Length > 0)
            {
                OtpSecondEntry.Focus();
            }
        }

        void OtpSecondEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (viewModel.OtpSecondDigit.Length > 0)
            {
                OtpThirdEntry.Focus();
            }
        }

        void OtpThirdEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (viewModel.OtpThirdDigit.Length > 0)
            {
                OtpGAZTDarkGrayLabelStyleFourthEntry.Focus();
            }
        }

        void OtpGAZTDarkGrayLabelStyleFourthEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {

        }
    }
}
