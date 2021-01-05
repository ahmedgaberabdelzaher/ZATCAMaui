using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.UnlockAccount;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesApp.Controls;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.Views.SyncFusionEnabledViews.UnlockAccount
{
    [Preserve(AllMembers = true)]
    public partial class UnlockAccountTINPageView:PopupPage
    {
        UnlockAccountTINPageViewModel viewModel;
        private bool isConfirmOtpCalled = false;

        public UnlockAccountTINPageView()
        {
            InitializeComponent();

            viewModel = App.Locator.UnlockAccountTINPageViewModel;
            this.BindingContext = viewModel;
            
            SetLTR();
            ChangeAeroIcon();
            OtpGAZTDarkGrayLabelStyleFourthEntry.Text = string.Empty;
            viewModel.EnableTINView();
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

        private void TappedOnTinContentBackButton(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(()=>
            {
                MessagingCenter.Send("UnlockAccountBackButtonClicked", "UnlockAccountBackButtonClicked");
                PopupNavigation.Instance.PopAsync();
                //viewModel._navigationService.GoBack();
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

        void FrmTIN_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            //ValidateTinEntryAndVerify();
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
                    if(isVerifyBtnClicked == true)
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

        public void OtpGAZTDarkGrayLabelStyleFourthEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (viewModel.OtpFourthDigit.Length > 0)
            {
                OtpGAZTDarkGrayLabelStyleFourthEntry.Unfocus();
            }

            //try
            //{
            //    if(e.NewTextValue != null)
            //    {
            //        if(e.NewTextValue.Length == 0)
            //        {
            //            isConfirmOtpCalled = false;
            //        }
            //    }

            //    if(e.NewTextValue != null && e.OldTextValue != null)
            //    {

            //        if (e.NewTextValue.Length >= 1 && e.OldTextValue.Length == 0 && isConfirmOtpCalled == false)
            //        {
            //            isConfirmOtpCalled = true;
            //            await viewModel.ConfirmOtpBtnCommand(null);
            //        }
            //    }


            //}
            //catch(Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
        }

        // * Password Validation
        void NewPassword_TextChanged(object sender, FocusEventArgs e)
        {
            this.ResetPasswordValidationConditions();

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
