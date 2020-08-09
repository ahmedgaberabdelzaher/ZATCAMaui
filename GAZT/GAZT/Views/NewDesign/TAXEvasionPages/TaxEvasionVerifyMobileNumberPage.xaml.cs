using System;
using System.Collections.Generic;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.TaxEvasionReportMobilePage_ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.TAXEvasionPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxEvasionVerifyMobileNumberPage : ContentPage
    {
        TaxEvasionReportMobilePageViewModel viewModel;
        public TaxEvasionVerifyMobileNumberPage()
        {
            InitializeComponent();

            viewModel = App.Locator.TaxEvasionReportPhonePageView;
            this.BindingContext = viewModel;
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

        void btn_Continue_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        // * Forgot password : OTP Verification :
        void OtpFirstEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            //if (viewModel.OTPFirstDigit.Length > 0)
            //{
            //    OTPSecondEntry.Focus();
            //}
        }

        void OtpSecondEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            //if (viewModel.OTPSecondDigit.Length > 0)
            //{
            //    OTPThirdEntry.Focus();
            //}
        }

        void OtpThirdEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            //if (viewModel.OTPThirdDigit.Length > 0)
            //{
            //    OTPFourthEntry.Focus();
            //}
        }

        void OtpFourthEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {

        }

        void OtpFourthEntry_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {

        }
    }
}
