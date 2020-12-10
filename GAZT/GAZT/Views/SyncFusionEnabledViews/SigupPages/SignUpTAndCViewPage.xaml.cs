using EGAZT.ViewModel.SyncFusionEnabledViewModel.SignUpTAndCPage_ViewModel;
using EGAZT.Views.SyncFusionEnabledViews.CorrespondenceDetails;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
namespace EGAZT.Views.SyncFusionEnabledViews.SignUpTAndC
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpTAndCViewPage : ContentPage
    {
        SignUpTAndCPageViewModel viewModel;
        public SignUpTAndCViewPage()
        {
            try
            {
                viewModel = App.Locator.SignUpTAndCPageView;
                InitializeComponent();
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                this.BindingContext = viewModel;
                SetLTR();
                ChangeAeroIcon();
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
                // NavigationPage.SetHasNavigationBar(this, false);
            }
            catch(Exception ex)
            {
            }
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
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.IsButtonEnabled = false;
            viewModel.IschkTAndC = false;
            viewModel.VerifyButtonDisableColor = Color.FromHex("#9EA4A9");
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
    }
}