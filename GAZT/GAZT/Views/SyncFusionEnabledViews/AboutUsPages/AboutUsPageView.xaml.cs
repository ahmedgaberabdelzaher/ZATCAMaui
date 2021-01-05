using EGAZT.ViewModel.SyncFusionEnabledViewModel.AboutUsPage;
using EGAZT.Views.NewDesign.TaxpayerCorrespondancePages;
//using EGAZT.Views.SyncFusionEnabledViews.CorrespondenceDetails;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
namespace EGAZT.Views.SyncFusionEnabledViews.AboutUs
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutUsPageView : ContentPage
    {
        #region Variable
        AboutUsPageViewModel viewModel;
        #endregion
        #region Property
        #endregion
        #region Constructor
        public AboutUsPageView()
        {try
            { 
            InitializeComponent();
            viewModel = App.Locator.AboutUsPageView;
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
                    string url = Path.Combine(path, "About_EN.html");
                    viewModel.WebUrl = url;
                }
                else
                {
                    string url = Path.Combine(path, "About_AR.html");
                    viewModel.WebUrl = url;
                }
            }
            else
            {
                if (!App.IsArabic)
                {
                    viewModel.WebUrl = "file:///android_asset/About_EN.html";
                }
                else
                {
                    viewModel.WebUrl = "file:///android_asset/About_AR.html";
                }
            }
            }
            catch(Exception ex)
            { 
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
            if(AboutUsWebView.CanGoBack)
            {
                AboutUsWebView.GoBack();
            }
            else
            {
                viewModel._navigationService.GoBack();
            }
        }

        void AboutUsWebView_Navigating(System.Object sender, Xamarin.Forms.WebNavigatingEventArgs e)
        {
            viewModel.IsLoading = true;
        }

        void AboutUsWebView_Navigated(System.Object sender, Xamarin.Forms.WebNavigatedEventArgs e)
        {
            viewModel.IsLoading = false;
        }
        #endregion
    }
}