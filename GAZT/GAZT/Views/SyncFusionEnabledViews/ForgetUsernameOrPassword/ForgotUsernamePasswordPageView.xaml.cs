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
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
namespace EGAZT.Views.SyncFusionEnabledViews.ForgotUsernamePassword
{
    [Preserve(AllMembers = true)]
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
                viewModel.ClearData();
                UserName.Keyboard = Keyboard.Default;
                viewModel.MainPageLayoutVisibility = true;
                viewModel.NewPasswordVisibility = true;
                viewModel.ConfirmPasswordVisibility = true;
                viewModel.currentAttempts = 0;
                viewModel.OTPValidDuration = "00:00";
                SetLTR();
                SetPickerFont();
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

        public void SetPickerFont()
        {
            try
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {

                    case Xamarin.Forms.Device.iOS:
                        {
                            if (App.IsArabic)
                            {
                                SelectPasswordUserNamePicker.HeaderFontFamily = "GE SS Two";
                                SelectPasswordUserNamePicker.ColumnHeaderFontFamily = "GE SS Two";
                                SelectPasswordUserNamePicker.SelectedItemFontFamily = "GE SS Two";
                                SelectPasswordUserNamePicker.UnSelectedItemFontFamily = "GE SS Two";//SelectTaxpayerTypePicker

                                SelectTaxpayerTypePicker.HeaderFontFamily = "GE SS Two";
                                SelectTaxpayerTypePicker.ColumnHeaderFontFamily = "GE SS Two";
                                SelectTaxpayerTypePicker.SelectedItemFontFamily = "GE SS Two";
                                SelectTaxpayerTypePicker.UnSelectedItemFontFamily = "GE SS Two";//SelectedTinIdPicker


                                SelectedTinIdPicker.HeaderFontFamily = "SSTArabic-Medium";
                                SelectedTinIdPicker.ColumnHeaderFontFamily = "SSTArabic-Medium";
                                SelectedTinIdPicker.SelectedItemFontFamily = "SSTArabic-Medium";
                                SelectedTinIdPicker.UnSelectedItemFontFamily = "SSTArabic-Medium";//SelectedTinIdPicker
                            }
                            else
                            {
                                SelectTaxpayerTypePicker.HeaderFontFamily = "SSTArabic-Medium";
                                SelectTaxpayerTypePicker.ColumnHeaderFontFamily = "SSTArabic-Medium";
                                SelectTaxpayerTypePicker.SelectedItemFontFamily = "SSTArabic-Medium";
                                SelectTaxpayerTypePicker.UnSelectedItemFontFamily = "SSTArabic-Medium";//SelectTaxpayerTypePicker

                                SelectPasswordUserNamePicker.HeaderFontFamily = "SSTArabic-Medium";
                                SelectPasswordUserNamePicker.ColumnHeaderFontFamily = "SSTArabic-Medium";
                                SelectPasswordUserNamePicker.SelectedItemFontFamily = "SSTArabic-Medium";
                                SelectPasswordUserNamePicker.UnSelectedItemFontFamily = "SSTArabic-Medium";//SelectedTinIdPicker

                                SelectedTinIdPicker.HeaderFontFamily = "SSTArabic-Medium";
                                SelectedTinIdPicker.ColumnHeaderFontFamily = "SSTArabic-Medium";
                                SelectedTinIdPicker.SelectedItemFontFamily = "SSTArabic-Medium";
                                SelectedTinIdPicker.UnSelectedItemFontFamily = "SSTArabic-Medium";//SelectedTinIdPicker
                            }
                        }
                        break;
                    case Xamarin.Forms.Device.Android:
                        SelectPasswordUserNamePicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        SelectPasswordUserNamePicker.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        SelectPasswordUserNamePicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        SelectPasswordUserNamePicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";//SelectTaxpayerTypePicker

                        SelectTaxpayerTypePicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        SelectTaxpayerTypePicker.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        SelectTaxpayerTypePicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        SelectTaxpayerTypePicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";//SelectTaxpayerTypePicker


                        SelectedTinIdPicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        SelectedTinIdPicker.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        SelectedTinIdPicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        SelectedTinIdPicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";
                        break;
                }
            }
            catch (Exception ex)
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
            if (viewModel.ForgotTypeIndex == 1)
            {
                viewModel.SelectedTaxPayerType = null;
                UserName.Keyboard = Keyboard.Default;
                viewModel.IsVisibleTinIds = false;
                viewModel.MaxChar = 256;
            }


            if (viewModel.ForgotTypeIndex == 0 && viewModel.SelectedTaxPayerType != null)
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
        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                var safeInsets = On<Xamarin.Forms.PlatformConfiguration.iOS>().SafeAreaInsets();
                safeInsets.Bottom = -10;
                this.Padding = safeInsets;
                if (viewModel.OTPLayoutVisibility == true)
                {
                    viewModel.TimerStart(viewModel.numberOfSeconds);
                    viewModel.ButtonDisableColor = Color.FromHex("#9EA4A9");
                    viewModel.IsResendOTPEnabled = false;
                    viewModel.IsOTPEntryEnable = true;
                    viewModel.currentAttempts = 0;
                    await Task.Run(() =>
                    {
                        Task.Delay(100);
                    });
                    EnteredOTP.Focus();
                }
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
            try
            {
                TIN selectedTIN = (TIN)e.NewValue;
                if (selectedTIN != null)
                {
                    SelectedTinIdPicker.SelectedItem = selectedTIN;
                    viewModel.SelectedTinId = selectedTIN;
                    viewModel.SelectedTinIdPrev = selectedTIN;
                    viewModel.TxtTIN = selectedTIN.Tin;
                }
            }
            catch (Exception ex)
            {

            }
           
          
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
