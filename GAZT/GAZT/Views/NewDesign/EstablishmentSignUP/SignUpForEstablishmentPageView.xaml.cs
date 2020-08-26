using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.EstablishmentSignUPVM;
using EGAZT.Views.SyncFusionEnabledViews.CorrespondenceDetails;
using EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage;
using GAZT.Manager;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfPicker.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.EstablishmentSignUP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpForEstablishmentPageView : ContentPage
    {
        SignUpForEstablishmentPageViewModel viewModel;
        ObservableCollection<InternationalMobileData> mobileData = null;

        public SignUpForEstablishmentPageView()
        {
            InitializeComponent();
            BindingContext = viewModel;
            
            viewModel = App.Locator.SignUpForEstablishmentPageView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            ChangeAeroIcon();
            SetLTR();

            if (Device.RuntimePlatform == Device.iOS)
            {
                string baseUrl = DependencyService.Get<IBaseUrl>().Get();
                string path = DependencyService.Get<IBaseUrl>().Get();
                if (!App.IsArabic)
                {
                    string url = Path.Combine(path, "TermsAndConditionsEN.html");
                    TCWebView.Source = url;
                }
                else
                {
                    string url = Path.Combine(path, "TermsAndConditionsAR.html");
                    TCWebView.Source = url;
                }
            }
            else
            {
                if (!App.IsArabic)
                {
                    TCWebView.Source = "file:///android_asset/TermsAndConditionsEN.html";
                }
                else
                {
                    TCWebView.Source = "file:///android_asset/TermsAndConditionsAR.html";
                }
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
        public void ChangeAeroIcon()
        {
            if (!App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
                Image_backArrow.Rotation = 0;
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
                Image_backArrow.Rotation = 180;
            }
        }

        private void chkDeclaration_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (chkDeclaration.IsChecked == true)
            {
                viewModel.IsMainButtonEnabled = true;
            }
            else
            {
                viewModel.IsMainButtonEnabled = false;
            }
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

           

            try
            {
                mobileData = WebServiceManager.GAZTGetMobileRegionDropdown();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        //email otp text changed events

        private void OTPFirstEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.OTPFirstDigit.Length > 0)
            {
                OTPSecondEntry.Focus();
            }
        }

        private void OTPSecondEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.OTPSecondDigit.Length > 0)
            {
                OTPThirdEntry.Focus();
            }
        }

        private void OTPThirdEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.OTPThirdDigit.Length > 0)
            {
                OTPFourthEntry.Focus();
            }
        }

        private void OTPFourthEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.OTPFourthDigit.Length > 0)
            {
                //if (!viewModel.IsResendOTPEnabled)
                //{
                //    // viewModel.VerifyOTP();
                //}

            }
        }

        //mobile OTP entry text changed events
        private void MobOTPFirstEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.MOTPFirstDigit.Length > 0)
            {
                MobOTPSecondEntry.Focus();
            }
        }

        private void MobOTPSecondEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.MOTPSecondDigit.Length > 0)
            {
                MobOTPThirdEntry.Focus();
            }
        }

        private void MobOTPThirdEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.MOTPThirdDigit.Length > 0)
            {
                MobOTPFourthEntry.Focus();
            }
        }

        private void MobOTPFourthEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.MOTPFourthDigit.Length > 0)
            {
                //if (!viewModel.IsResendOTPEnabled)
                //{
                //    // viewModel.VerifyMOTP();
                //}

            }
        }

        private void OnNewPasswordTapped(object sender, EventArgs e)
        {

        }

        private void NewPassword_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OnConfirmNewPasswordTapped(object sender, EventArgs e)
        {

        }

        private void OnDOBClicked(object sender, EventArgs e)
        {
            DpDbo.IsOpen = true;
        }

        private void CountryCodeTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new InternationalCodeSearchPage(mobileData));
        }

        private void OnIDTypeClicked(object sender, EventArgs e)
        {
            IDTypePicker.IsOpen = true;
        }

        private void IDTypePicker_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void OnLicenseIssuedByClicked(object sender, EventArgs e)
        {
            LicenseIssuedByPicker.IsOpen = true;
        }

        private void OnIssuingCityClicked(object sender, EventArgs e)
        {
            IssuingCityPicker.IsOpen = true;
        }

        private void DpDbo_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void OnInCTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new InstructionPopUpPageView());
        }

        private void OnYesTapped(object sender, EventArgs e)
        {
            viewModel.ImgBackgroundNo = "re_Property_Tile_Background_White";
            viewModel.ImgBackgroundYes = "re_Tile_Background";
        }

        private void OnNoTapped(object sender, EventArgs e)
        {
            viewModel.ImgBackgroundNo = "re_Tile_Background";
            viewModel.ImgBackgroundYes = "re_Property_Tile_Background_White";
        }

        private void OnCRNumberTapped(object sender, EventArgs e)
        {
            viewModel.ImgBackgroundCRNubmer = "FP_selected_tile";
            viewModel.ImgBackgroundLicenseNubmer = "FP_unselected_tile";
        }

        private void OnLicenseNumberTapped(object sender, EventArgs e)
        {
            viewModel.ImgBackgroundCRNubmer = "FP_unselected_tile";
            viewModel.ImgBackgroundLicenseNubmer = "FP_selected_tile";
        }
        private void ImageSeeConfirmPassword_Tapped(object sender, EventArgs e)
        {
            viewModel.IsConfirmPasswordEncripted = !viewModel.IsConfirmPasswordEncripted;
            imageConfirmPassword.Source = viewModel.IsConfirmPasswordEncripted ? "hidePassword.png" : "showPassword.png";
        }
        private void ImageSeeNewPassword_Tapped(object sender, EventArgs e)
        {
            viewModel.IsPasswordEncripted = !viewModel.IsPasswordEncripted;
            imageNewPassword.Source = viewModel.IsPasswordEncripted ? "hidePassword.png" : "showPassword.png";
        }
    }
}