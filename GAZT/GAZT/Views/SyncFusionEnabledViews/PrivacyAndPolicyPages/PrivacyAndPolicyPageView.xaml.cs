using EGAZT.ViewModel.SyncFusionEnabledViewModel.PrivacyAndPolicyPage_ViewModel;
using EGAZT.Views.SyncFusionEnabledViews.CorrespondenceDetails;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
namespace EGAZT.Views.SyncFusionEnabledViews.PrivacyAndPolicy
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrivacyAndPolicyPageView : ContentPage
    {
        #region Variable
        PrivacyAndPolicyPageViewModel viewModel;
        #endregion
        #region Property
        #endregion
        #region Constructor
        public PrivacyAndPolicyPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.PrivacyAndPolicyPageView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            ChangeAeroIcon();
            SetLTR();
            this.BindingContext = viewModel;
            if (Device.RuntimePlatform == Device.iOS)
            {
                string baseUrl = DependencyService.Get<IBaseUrl>().Get();
                string path = DependencyService.Get<IBaseUrl>().Get();
                if (!App.IsArabic)
                {
                    string url = Path.Combine(path, "PrivacyPolicy_EN.html");
                    viewModel.WebUrl = url;
                }
                else
                {
                    string url = Path.Combine(path, "PrivacyPolicy_AR.html");
                    viewModel.WebUrl = url;
                }
            }
            else
            {
                if (!App.IsArabic)
                {
                    viewModel.WebUrl = "file:///android_asset/PrivacyPolicy_EN.html";
                }
                else
                {
                    viewModel.WebUrl = "file:///android_asset/PrivacyPolicy_AR.html";
                }
            }
        }
        #endregion
        #region Method
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
            }
        }
        private void BackButtonClicked(object sender, EventArgs e)
        {
            if (PrivacyandPolicyWebView.CanGoBack)
            {
                PrivacyandPolicyWebView.GoBack();
            }
            else
            {
                viewModel._navigationService.GoBack();
            }
        }

        void PrivacyandPolicyWebView_Navigating(System.Object sender, Xamarin.Forms.WebNavigatingEventArgs e)
        {
            viewModel.IsLoading = true;
        }

        void PrivacyandPolicyWebView_Navigated(System.Object sender, Xamarin.Forms.WebNavigatedEventArgs e)
        {
            viewModel.IsLoading = false;
        }
        #endregion
    }
}