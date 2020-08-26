using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.EstablishmentSignUPVM;
using EGAZT.Views.SyncFusionEnabledViews.CorrespondenceDetails;
using EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage;
using GAZT.Manager;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.EstablishmentSignUP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpForEstablishmentPageView : ContentPage
    {
        SignUpForEstablishmentPageViewModel viewModel= App.Locator.SignUpForEstablishmentPageView;
        ObservableCollection<InternationalMobileData> mobileData = null;

        public SignUpForEstablishmentPageView()
        {
            InitializeComponent();
            BindingContext = viewModel;

            //if (Device.RuntimePlatform == Device.iOS)
            //{
            //    string baseUrl = DependencyService.Get<IBaseUrl>().Get();
            //    string path = DependencyService.Get<IBaseUrl>().Get();
            //    if (!App.IsArabic)
            //    {
            //        string url = Path.Combine(path, "TermsAndConditionsEN.html");
            //        TCWebView.Source = url;
            //    }
            //    else
            //    {
            //        string url = Path.Combine(path, "TermsAndConditionsAR.html");
            //        TCWebView.Source = url;
            //    }
            //}
            //else
            //{
            //    if (!App.IsArabic)
            //    {
            //        TCWebView.Source = "file:///android_asset/TermsAndConditionsEN.html";
            //    }
            //    else
            //    {
            //        TCWebView.Source = "file:///android_asset/TermsAndConditionsAR.html";
            //    }
            //}
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            //if (Device.RuntimePlatform == Device.iOS)
            //{
            //    string baseUrl = DependencyService.Get<IBaseUrl>().Get();
            //    string path = DependencyService.Get<IBaseUrl>().Get();
            //    if (!App.IsArabic)
            //    {
            //        string url = Path.Combine(path, "TermsAndConditionsEN.html");
            //        TCWebView.Source = url;
            //    }
            //    else
            //    {
            //        string url = Path.Combine(path, "TermsAndConditionsAR.html");
            //        TCWebView.Source = url;
            //    }
            //}
            //else
            //{
            //    if (!App.IsArabic)
            //    {
            //        TCWebView.Source = "file:///android_asset/TermsAndConditionsEN.html";
            //    }
            //    else
            //    {
            //        TCWebView.Source = "file:///android_asset/TermsAndConditionsAR.html";
            //    }
            //}

            try
            {
                mobileData = WebServiceManager.GAZTGetMobileRegionDropdown();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void OTPFourthEntry_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OTPFourthEntry_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void OTPThirdEntry_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OTPSecondEntry_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OTPFirstEntry_TextChanged(object sender, TextChangedEventArgs e)
        {

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

        private void MobOTPFirstEntry_TextChanged(object sender, TextChangedEventArgs e)
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
    }
}