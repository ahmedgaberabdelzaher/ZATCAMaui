using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.ViewModel.NewDesignViewModel.TaxEvasionViewModels;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.TAXEvasionPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxEvasionVerifyMobileNumberPage : ContentPage
    {
        TaxEvasionVerifyMobileViewModel viewModel;
        public TaxEvasionVerifyMobileNumberPage()
        {
            InitializeComponent();

            viewModel = App.Locator.TaxEvasionVerifyMobileNumberPage;
            this.BindingContext = viewModel;
            viewModel.MobileNumber = string.Empty;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            viewModel.ShowMobileForm();
            ChangeAeroIcon();
            SetLTR();
           
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
                var _navigation = Xamarin.Forms.Application.Current.MainPage.Navigation;
                foreach (var item in _navigation.NavigationStack)
                {
                    if (item.GetType().Name == App.TaxEvasionMyReportsListPageView)
                    {
                        _navigation.RemovePage(item);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
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

        async void btn_Continue_ClickedAsync(System.Object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(viewModel.MobileNumber))
            {
                StringBuilder Message = new StringBuilder();
                frmMobile.HasError = true;
                Message.Append(AppResources.ZZPleasefillthemandatoryfields);
                PopUp popUp = new PopUp();
                popUp.Message = Message.ToString();
                //await PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
               // await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));
            }
            else
            {
                viewModel.IsTimerCancel = true;
                viewModel.IsResendOTPEnabled = false;
                await viewModel.sendOTPAsync();
            }
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
            if (viewModel.OTPFourthDigit.Length > 0)
            {
                if (!viewModel.IsResendOTPEnabled)
                {
                    viewModel.VerifyOTP();
                }
                
            }
        }
        protected override bool OnBackButtonPressed() => true;
        void OtpFourthEntry_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {

        }

        void Mobile_Entry_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            StringBuilder Message = new StringBuilder();
            PopUp popUp = new PopUp();
            if (!string.IsNullOrEmpty(viewModel.MobileNumber))
            {

                if (viewModel.MobileNumber.Substring(0, 1) == "0")
                {
                    Message.Append(AppResources.ZZMobilenumberCannotStartWith0);

                }
                if (viewModel.MobileNumber.Substring(0, 1) != "5")
                {
                    Message.Append(AppResources.ZZMobilenumberhastostartwithnumber5);

                }
                if (viewModel.MobileNumber.Substring(0, 1) == "0")
                {
                    Message.Append(AppResources.ZZMobilenumberCannotStartWith0);
                }
                if (viewModel.MobileNumber.Length < 9)
                {
                    if (Message.Length > 0)
                    {
                        Message.Append(Environment.NewLine);
                    }
                    Message.Append(AppResources.ZZMobilenumberlengthcannotbelessthan9digits);
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
                   //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                     PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));
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
                //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));
            }
        }
    }
}
