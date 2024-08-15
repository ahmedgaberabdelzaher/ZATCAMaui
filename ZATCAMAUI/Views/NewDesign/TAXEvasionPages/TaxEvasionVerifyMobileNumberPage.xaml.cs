
using Mopups.Services;
using System.Text;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel.TaxEvasionViewModels;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.NewDesign.TAXEvasionPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxEvasionVerifyMobileNumberPage : ContentPage
    {
        TaxEvasionVerifyMobileViewModel viewModel;
        public TaxEvasionVerifyMobileNumberPage()
        {
            InitializeComponent();

            viewModel = App.Locator.TaxEvasionVerifyMobileNumberPage;
            BindingContext = viewModel;
            viewModel.MobileNumber = string.Empty;
            viewModel.ShowMobileForm();

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await Task.Run(() =>
            {
                viewModel.IsLoading = false;
            });
            try
            {
                var _navigation = Application.Current.MainPage.Navigation;
                foreach (var item in _navigation.NavigationStack)
                {
                    if (item.GetType().Name == App.TaxEvasionMyReportsListPageView)
                    {
                        _navigation.RemovePage(item);
                        break;
                    }
                }
            }
            catch (Exception)
            {


            }
        }
        async void btn_Continue_ClickedAsync(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(viewModel.MobileNumber))
            {
                StringBuilder Message = new StringBuilder();
                frmMobile.HasError = true;
                Message.AppendLine(AppResources.ZZPleasefillthemandatoryfields);
                PopUp popUp = new PopUp();
                popUp.Message = Message.ToString();
            }
            else
            {
                viewModel.IsTimerCancel = true;
                viewModel.IsResendOTPEnabled = false;
                await viewModel.sendOTPAsync();
            }
        }

        // * Forgot password : OTP Verification :
        void OtpFirstEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.OTPFirstDigit.Length > 0)
            {
                OTPSecondEntry.Focus();
            }
        }

        void OtpSecondEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.OTPSecondDigit.Length > 0)
            {
                OTPThirdEntry.Focus();
            }
        }

        void OtpThirdEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.OTPThirdDigit.Length > 0)
            {
                OTPFourthEntry.Focus();
            }
        }

        void OtpFourthEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.OTPFourthDigit.Length > 0)
            {
                if (!viewModel.IsResendOTPEnabled)
                {
                    viewModel.VerifyOTP();
                }

            }
        }

        void Mobile_Entry_Unfocused(object sender, FocusEventArgs e)
        {
            StringBuilder Message = new StringBuilder();
            PopUp popUp = new PopUp();
            if (!string.IsNullOrEmpty(viewModel.MobileNumber))
            {

                if (viewModel.MobileNumber.Substring(0, 1) == "0")
                {
                    Message.AppendLine(AppResources.ZZMobilenumberCannotStartWith0);

                }
                if (viewModel.MobileNumber.Substring(0, 1) != "5")
                {
                    Message.AppendLine(AppResources.ZZMobilenumberhastostartwithnumber5);

                }
                if (viewModel.MobileNumber.Length < 9)
                {
                    Message.AppendLine(AppResources.ZZMobilenumberlengthcannotbelessthan9digits);
                }
                if (Message.Length > 0)
                {
                    popUp.Message = Message.ToString();
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
                    MopupService.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));
                    frmMobile.HasError = true;
                    viewModel.MobileNumber = string.Empty;
                }
                else
                {
                    frmMobile.HasError = false;
                }
            }
            else
            {
                Message.Append(AppResources.EnterMobileNumber);
                popUp.Message = Message.ToString();
                MopupService.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));
            }
        }
    }
}
