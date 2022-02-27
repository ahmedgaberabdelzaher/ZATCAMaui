using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using EGAZT.Views.NewDesign.TaxpayerCorrespondancePages;
//using EGAZT.Views.SyncFusionEnabledViews.CorrespondenceDetails;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using Application = Xamarin.Forms.Application;

namespace EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATIndividualSignupTnCPageView : ContentPage
    {

        VATIndividualSignupTnCPageViewModel viewModel;
        public VATIndividualSignupTnCPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.VATIndividualSignupTnCPageView;
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
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.IsButtonEnabled = false;
            viewModel.IschkTAndC = false;
            viewModel.VerifyButtonDisableColor =  (Color)Application.Current.Resources["ButtonGray"];
            viewModel.IsLoading = false;
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
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
        }
    }
}