using System.Text;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Mopups.Services;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel.LoginViewModels;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using ZATCAMAUI.Views.SyncFusionEnabledViews.VATIndividualSignupPage;

namespace ZATCAMAUI.Views.NewDesign.LoginPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeMobileRequestPageView : ContentPage
	{
        ChangeMobileRequestViewModel viewModel;
        public ChangeMobileRequestPageView (string guid)
		{
            InitializeComponent();
			this.BindingContext = viewModel = App.Locator.ChangeMobileRequestPageView;
            On<iOS>().SetUseSafeArea(true);
            ChangeAeroIcon();
            InitializePopups();

            setDefaults();

            viewModel.GetIdTypesAsync(guid);
            
            viewModel.InitCountryCodesAPI();
            viewModel.NafathGUID = guid;
            var x = guid;
            if (string.IsNullOrEmpty(guid))
            {
                viewModel.ShowMainForm = true;
                viewModel.ShowOtpForm = false;
                viewModel.ShowContinue2 = false;
            }
            else
            {
                viewModel.ShowOtpForm = true;
                viewModel.IsTINManual = true;
                viewModel.ShowContinue2 = true;
                viewModel.ShowMainForm = false;
                viewModel.OtpSection1 = false;
                viewModel.InitCountryCodesAPI();
                viewModel.GetCaptchAndGUID("CHMB");
            }

        }

        private void setDefaults()
        {
            viewModel.OtpSection1 = false;
            viewModel.ShowOTPSection = false;
            viewModel.ShowAttachmentSection = false;
            viewModel.ShowMainForm = true;
            viewModel.ShowOtpForm = false;
            viewModel.TinNumber = string.Empty;
            viewModel.ManagerName = string.Empty;
            viewModel.SelectedIDType = string.Empty;
            viewModel.ManagerId = string.Empty;
            viewModel.AttachedForms.Clear();
            viewModel.DissableSendOtp = true;
            viewModel.TxtMobileNumber = string.Empty;
            viewModel.OTPFirstDigit = string.Empty;
            viewModel.OTPSecondDigit = string.Empty;
            viewModel.OTPThirdDigit = string.Empty;
            viewModel.OTPFourthDigit = string.Empty;
            viewModel.ShowOTPSuccessMessage = false;
            viewModel.ShowSubmitForAutomatic = false;

            MessagingCenter.Subscribe<InternationalCodeSearchPage, string>(this, "SelectedItem", (sender, arg) =>
            {
                viewModel.TxtMobileNumber = string.Empty;
                // IntnlCodes.Text = arg;
                viewModel.TxtCountryCode = arg;
            });
            MessagingCenter.Subscribe<InternationalCodeSearchPage, string>(this, "SelectedCountryCode", (sender, arg) =>
            {

                viewModel.MobileCountryCode = arg;
            });
        }

        private void InitializePopups()
        {
            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelected", (sender, arg) =>
            {
                // viewModel.PickerModelExcemptionYear = arg;

                var selectedType = string.Empty;
                string SelectedIDTypeValue = string.Empty;
                if (arg.PickerId == "EntityTypePicker")
                {
                    viewModel.SelectedIDType = arg.SelectedValue;
                }
                
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<PickerPageView, GenericPickerModel>(this, "PickerSelected");
            MessagingCenter.Unsubscribe<InternationalCodeSearchPage, string>(this, "SelectedItem");
            MessagingCenter.Unsubscribe<PickerPageView, GenericPickerModel>(this, "PickerSelected");
            MessagingCenter.Unsubscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem"); 
            viewModel.StopTimer();
        }
        public void ChangeAeroIcon()
        {
            try
            {
                if (App.IsArabic)
                {
                    Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
                }
                else
                {
                    Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
                }

            }
            catch (Exception)
            {

            }

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                App.Current.On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }
        private void CountryCodeTapped(object sender, EventArgs e)
        {
            MopupService.Instance.PushAsync(new InternationalCodeSearchPage(viewModel.CountryCodesList));
        }

        private void EntryMobileNumber_Unfocused(object sender, FocusEventArgs e)
        {
            StringBuilder Message = new StringBuilder();
            PopUp popUp = new PopUp();
            if (!string.IsNullOrEmpty(EntryMobileNumber.Text))
            {

                if (EntryMobileNumber.Text.Substring(0, 1) == "0")
                {
                    Message.AppendLine(AppResources.ZZMobilenumberCannotStartWith0 + " ");
                }
                if (viewModel.TxtCountryCode == "+966")
                {
                    if (EntryMobileNumber.Text.Substring(0, 1) != "5")
                    {
                        Message.AppendLine(AppResources.ZZMobilenumberhastostartwithnumber5);
                    }
                }
                if (EntryMobileNumber.Text.Length < 9)
                {
                    //if (Message.Length > 0)
                    //{
                    //    Message.AppendLine(Environment.NewLine);
                    //}

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
                    viewModel.IsAllValidContactDataEnteredMobileNbr = false;

                    EntryMobileNumber.Text = string.Empty;
                }
                else
                {
                    viewModel.IsAllValidContactDataEnteredMobileNbr = true;
                    viewModel.TxtMobileNumberwithCountryCode = "(" + viewModel.TxtCountryCode + ")" + " " + EntryMobileNumber.Text;
                }
            }
            else
            {
                Message.AppendLine(AppResources.EnterMobileNumber);
                popUp.Message = Message.ToString();
                MopupService.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));
            }
            
        }

        void OtpFirstEntry_TextChanged(System.Object sender, TextChangedEventArgs e)
        {
            if (viewModel.OTPFirstDigit.Length > 0)
            {
                OTPSecondEntry.Focus();
            }
        }

        void OtpSecondEntry_TextChanged(System.Object sender, TextChangedEventArgs e)
        {
            if (viewModel.OTPSecondDigit.Length > 0)
            {
                OTPThirdEntry.Focus();
            }
        }

        void OtpThirdEntry_TextChanged(System.Object sender, TextChangedEventArgs e)
        {
            if (viewModel.OTPThirdDigit.Length > 0)
            {
                OTPFourthEntry.Focus();
            }
        }
        void OtpFourthEntry_TextChanged(System.Object sender, TextChangedEventArgs e)
        {

        }
        void OtpFourthEntry_Unfocused(System.Object sender, FocusEventArgs e)
        {

        }
        async void TapRentDeleteGestureRecognizer_Tapped(Object sender, EventArgs e)
        {
            Image item = sender as Image;
            Attachment data = item.BindingContext as Attachment;
            string QuestionMark = string.Empty;
            if (App.IsArabic)
            {
                QuestionMark = "؟";
            }
            else
            {
                QuestionMark = "?";
            }
            var confirmPopup = new ZAKATOkCancelPopUpView(AppResources.ZZDeleteAttachmentConfirmationText + " " + data.Filename + QuestionMark);
            confirmPopup.OnSelect = (str) =>
            {
                if (str == "Yes")
                {
                    viewModel.OnRentAttachmentDeleteButtonTapped(data);
                }
            };
            await MopupService.Instance.PushAsync(confirmPopup);
        }

        void PhoneNumberTextChanged(System.Object sender, TextChangedEventArgs e)
        {
            if(viewModel.DissableSendOtp == false)
            {
                viewModel.DissableSendOtp = true;
                viewModel.ShowOTPSection = false;
                viewModel.StopTimer();
            }
        }
    }
}

