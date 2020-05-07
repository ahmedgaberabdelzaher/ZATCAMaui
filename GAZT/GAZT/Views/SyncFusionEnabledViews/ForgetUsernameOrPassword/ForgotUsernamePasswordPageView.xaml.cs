using EGAZT.ViewModel.SyncFusionEnabledViewModel.ForgotUsernamePasswordPage_ViewModel;
using GAZT.CustomControl;
using GAZT.Manager;
using GAZT.Models;
using Syncfusion.SfPicker.XForms;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Resources;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
namespace EGAZT.Views.SyncFusionEnabledViews.ForgotUsernamePassword
{
    public partial class ForgotUsernamePasswordPageView : ContentPage
    {
        ObservableCollection<String> Items = new ObservableCollection<String>();
        ForgotUsernamePasswordPageViewModel viewModel;
        public ForgotUsernamePasswordPageView()
        {
            try
            {
                viewModel = App.Locator.ForgotUsernamePasswordPageView;
                InitializeComponent();
                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                ChangeAeroIcon();
                viewModel.MainPageLayoutVisibility = true;
                viewModel.NewPasswordVisibility = true;
                viewModel.ConfirmPasswordVisibility = true;
                viewModel.currentAttempts = 0;
                viewModel.OTPValidDuration = "00:00";
                SetLTR();
                string str = "abc";
                Items.Add(str);
                //  NavigationPage.SetBackButtonTitle(this, "Forgot");
                CustomNavigation.SetBackButtonTitle(this, "Forgot");
                // NavigationPage.BackButtonTitle = "Forgot";
                App.IsComingFromDashboardToLogOff = false;
                try
                {
                    this.BindingContext = viewModel;
                    viewModel.OnPageLoad();
                }
                catch (Exception ex)
                {
                }
            }
            catch(Exception ex)
            {
            }
        }
        protected void OnSelectedTaxPAyerType(object sender, EventArgs e)  { }
        protected void OnSelectedForgetType(object sender, EventArgs e)
        {
        }
        protected async void OnUserNameUnFocussed(object sender, EventArgs e)
        {
            try
            {
                bool IsValiedEmailAddress = false;
                string userName = UserName.Text;
                if (!string.IsNullOrEmpty(userName))
                    IsValiedEmailAddress = UtilityManager.IsValidEmailAddress(userName);
                if (!IsValiedEmailAddress)
                {
                    viewModel.IsVisibleTinIds = false;
                }
                await viewModel.SetTinsListLayoutVisibility(IsValiedEmailAddress);
            }
            catch (Exception ex)
            {
            }
        }
        private async void OnIDNumberTextChanged(Object sender, EventArgs e)
       {
            if (viewModel.SelectedTaxPayerType != null)
            {
                string IdNumber = UserName.Text;
                if (IdNumber != null && IdNumber.Length > 0)
                {
                    bool isValidNumber = UtilityManager.IsOTPNumberValid(IdNumber);
                    if (!isValidNumber)
                    {
                        string _idNumber = IdNumber.Substring(0, IdNumber.Length - 1);
                        UserName.Text = _idNumber;
                    }
                }
                if(viewModel.SelectedTaxPayerType.id.Equals("1"))
                {
                    if (IdNumber.Length > 10)
                    {
                        UserName.Text = UserName.Text.Substring(0, 10);
                        UserName.Unfocus();
                    }
                }
            }
        }
        protected void OnUserNAmeFocused(object sender, EventArgs e)
        {
            if(viewModel.SelectedTaxPayerType != null)
            {
                UserName.Keyboard = Keyboard.Numeric;
            }
            else
            {
                UserName.Keyboard = Keyboard.Default;
            }
        }
        public void OnNewPasswordEyeClicked(object sender, EventArgs args)
        {
            viewModel.NewPasswordVisibility = !viewModel.NewPasswordVisibility;
        }
        public void OnConfirmPasswordEyeClicked(object sender, EventArgs args)
        {
            viewModel.ConfirmPasswordVisibility = !viewModel.ConfirmPasswordVisibility;
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
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                PickerResourceManager.Manager = new ResourceManager("EGAZT.SyncfusionControl", Xamarin.Forms.Application.Current.GetType().Assembly);
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
                CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                PickerResourceManager.Manager = new ResourceManager("GAZT.AppResources", Xamarin.Forms.Application.Current.GetType().Assembly);
            }
        }
        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                viewModel.IsLoading = false;
                viewModel.NewPassword = "";
                viewModel.ConfirmPassword = "";
                viewModel.EnteredOTP = "";
                viewModel.IDNumber = "";
                viewModel.NewPasswordLayoutVisibility = false;
                viewModel.OTPLayoutVisibility = false;
                viewModel.NavigateToLoginLinkVisibility = false;
                viewModel.EnteredCaptchaValue = "";
                viewModel.IsVisibleTinIds = false;
                viewModel.IsIDTypeVisible = false;
                viewModel.ButtonDisableColor = Color.FromHex("#9EA4A9");
                viewModel.IsResendOTPEnabled = false;
                viewModel.IsOTPEntryEnable = true;
                viewModel.StopTimer = true;
                viewModel.ForgotPasswordUserNameChangedMessage = "";
            }
            catch(Exception ex)
            {
            }
        }
        private async void OnOTPEntered(Object sender, EventArgs e)
        {
            string Otp = EnteredOTP.Text;
            if(Otp != null && Otp.Length > 4)
            {
                EnteredOTP.Text = EnteredOTP.Text.Substring(0, 4);
                EnteredOTP.Unfocus();
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel.StopTimer = false;
        }
        private void btnTxtSelectedUsernameAndPassword_Clicked(object sender, EventArgs e)
        {
            SelectPasswordUserNamePicker.IsOpen = true;
        }
        private void btnTxtSelectTaxpayerType_Clicked(object sender, EventArgs e)
        {
            SelectTaxpayerTypePicker.IsOpen = true;
        }
        private void btnTxtTIN_Clicked(object sender, EventArgs e)
        {
            SelectedTinIdPicker.IsOpen = true;
        }
        private void SelectedTinIdChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            //viewModel.TxtIDNumber = string.Empty;
            //EntryName.IsEnabled = true;
            try
            {
              //  SelectedTinIdPicker.IsOpen = true;
                TIN selectedId = (TIN)e.NewValue;
                viewModel.SelectedTinId = selectedId;
                viewModel.TxtTIN = selectedId.Tin;
            }
            catch (Exception ex)
            {
            }
        }
        private void SelectPasswordUserNamePicker_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            ForgotCredentialType selectedforgotType = (ForgotCredentialType)e.NewValue;
            SelectPasswordUserNamePicker.SelectedItem = selectedforgotType;
            viewModel.SelectedForgotType = selectedforgotType;
           // viewModel.SelectedTaxPayerType = selectedforgotType;
            viewModel.SelectedForgotTypePrev = selectedforgotType;
            viewModel.TxtSelectedUsernameAndPassword = selectedforgotType.CredentialType;
            //tSelectedUsernameAndPassword
        }
        private void SelectTaxpayerTypePicker_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {///////////
            ForgotUserNamePassword selectedTaxPayerType = (ForgotUserNamePassword)e.NewValue;
            SelectTaxpayerTypePicker.SelectedItem = selectedTaxPayerType;
            viewModel.SelectedTaxPayerType = selectedTaxPayerType;
            viewModel.SelectedTaxPayerTypePrev = selectedTaxPayerType;
            viewModel.TxtSelectTaxpayerType = selectedTaxPayerType.TaxPayerType;
        }
        private void OnTaxPayerTypeChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {///////////
            ForgotUserNamePassword selectedTaxPayerType = (ForgotUserNamePassword)e.NewValue;
            SelectTaxpayerTypePicker.SelectedItem = selectedTaxPayerType;
            viewModel.SelectedTaxPayerType = selectedTaxPayerType;
            viewModel.SelectedTaxPayerTypePrev = selectedTaxPayerType;
            viewModel.TxtSelectTaxpayerType = selectedTaxPayerType.TaxPayerType;
        }
        private void SelectPasswordUserNamePicker_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            SelectPasswordUserNamePicker.SelectedItem = viewModel.SelectedForgotTypePrev;
            viewModel.SelectedForgotType = viewModel.SelectedForgotTypePrev;
            if (viewModel.SelectedForgotTypePrev == null)
            {
                viewModel.TxtSelectedUsernameAndPassword = string.Empty;
            }
        }
        private void SelectTaxpayerTypePicker_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            SelectTaxpayerTypePicker.SelectedItem = viewModel.SelectedTaxPayerTypePrev;
            viewModel.SelectedTaxPayerType = viewModel.SelectedTaxPayerTypePrev;
            if(viewModel.SelectedTaxPayerTypePrev==null)
            {
                viewModel.TxtSelectedUsernameAndPassword = string.Empty;
            }
        }
        private void SelectedTinIdPicker_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            TIN selectedTIN = (TIN)e.NewValue;
            SelectedTinIdPicker.SelectedItem = selectedTIN;
            viewModel.SelectedTinId = selectedTIN;
            viewModel.SelectedTinIdPrev = selectedTIN;
            viewModel.TxtTIN = selectedTIN.Tin;
        }
        private void SelectedTinIdPicker_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            SelectedTinIdPicker.SelectedItem = viewModel.SelectedTinIdPrev;
            viewModel.SelectedTinId = viewModel.SelectedTinIdPrev;
            if (viewModel.SelectedTinIdPrev == null)
            {
                viewModel.TxtTIN = string.Empty;
            }
        }
    }
}
