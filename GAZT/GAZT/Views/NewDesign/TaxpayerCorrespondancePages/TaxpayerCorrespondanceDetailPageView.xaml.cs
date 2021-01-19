using EGAZT.ViewModel.NewDesignViewModel;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.TaxpayerCorrespondancePages
{
    [Preserve(AllMembers = true)]
    public interface IBaseUrl { string Get(); }
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxpayerCorrespondanceDetailPageView : ContentPage
    {
        TaxpayerCorrespondanceDetailPageViewModel viewModel;
        public TaxpayerCorrespondanceDetailPageView(CorrespondanceModel CorrModel)
        {
            InitializeComponent();
            viewModel = App.Locator.TaxpayerCorrespondanceDetailPageView;
            this.BindingContext = viewModel;
            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            viewModel.IsFavoriteVisible = false;
            CorrespondenceDetailsRootObject CorrespondenceD = new CorrespondenceDetailsRootObject();
            if (CorrModel != null)
            {
                viewModel.CorrespondenceTitle = CorrModel.Title;
                viewModel.CorrespondenceDateTime = CorrModel.DateToDisplay;
                viewModel.CorrespondenceTime = CorrModel.TimeToDisplay;
                if (string.IsNullOrEmpty(CorrModel.TaxtpFg))
                {
                    viewModel.IsFavoriteVisible = true;
                }
                else
                {
                    viewModel.IsFavoriteVisible = false;
                }
            }
            try
            {
                viewModel.IsAttachmentEnabled = false;
                CorrespondenceD = WebServiceManager.GAZTGetCorrespondeceDetails(CorrModel);
                if ((CorrespondenceD != null) && (CorrespondenceD.d != null) && (CorrespondenceD.d.results != null))
                {
                    string response = CorrespondenceD.d.results.LastOrDefault().Attfg;
                    if (response.Equals("X"))
                    {
                        viewModel.IsAttachmentEnabled = true;
                    }
                    else
                    {
                    }
                }
                PopToRootPage();
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                   await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                });
            }
            string HTMLContent = string.Empty;
            string HTMLContentTest = string.Empty;
            if (CorrespondenceD != null && CorrespondenceD.d != null && CorrespondenceD.d.results != null)
            {
                foreach (CorrespondenceDetailsResult ItemC in CorrespondenceD.d.results)
                {
                    HTMLContent = HTMLContent + ItemC.Tdline;
                }
                string newHTMLContent = HTMLContent.Replace("<img ", "<img src='ic_GAZT_Logo_Text.png' width='40%' ");

                if (Device.RuntimePlatform == Device.iOS)
                {
                    string newHTMLForFonts = newHTMLContent.Replace("<body>", "<body style='font-size:40px;margin:15;'>");
                    var htmlSource = new HtmlWebViewSource();
                    htmlSource.Html = newHTMLForFonts;
                    htmlSource.BaseUrl = DependencyService.Get<IBaseUrl>().Get();
                    CorWebView.Source = htmlSource;
                }
                else
                {
                    string newHTMLForFonts = newHTMLContent.Replace("<body>", "<body style='font-size:16px;margin:10;'>");
                    var htmlSource = new HtmlWebViewSource();
                    htmlSource.Html = newHTMLForFonts;
                    htmlSource.BaseUrl = DependencyService.Get<IBaseUrl>().Get();
                    CorWebView.Source = htmlSource;
                }
            }
            if (CorrModel != null)
            {
                viewModel.CorrespondenceTitle = CorrModel.Title;
                viewModel.CorrespondenceD = CorrModel;
                if (CorrModel.IsFav == true)
                {
                    viewModel.FavIcon = "ic_star.png";
                }
                else
                {
                    viewModel.FavIcon = "ic_star_border.png";
                }
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;

        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
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
        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Xamarin.Forms.Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });
            }
        }
    }
}