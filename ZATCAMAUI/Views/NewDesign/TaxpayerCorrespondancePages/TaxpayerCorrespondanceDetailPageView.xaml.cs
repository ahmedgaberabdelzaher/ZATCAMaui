using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using RGPopup.Maui.Services;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using Application = Microsoft.Maui.Controls.Application;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

namespace ZATCAMAUI.Views.NewDesign.TaxpayerCorrespondancePages
{

    public interface IBaseUrl { string Get(); }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxpayerCorrespondanceDetailPageView : ContentPage
    {
        TaxpayerCorrespondanceDetailPageViewModel viewModel;
        public TaxpayerCorrespondanceDetailPageView(CorrespondanceModel CorrModel)
        {
            InitializeComponent();
            viewModel = App.Locator.TaxpayerCorrespondanceDetailPageView;
            BindingContext = viewModel;
            ChangeAeroIcon();
            On<iOS>().SetUseSafeArea(true);
            NavigationPage.SetBackButtonTitle(this, "");
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
                if (CorrespondenceD != null && CorrespondenceD.d != null && CorrespondenceD.d.results != null)
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
                MainThread.BeginInvokeOnMainThread(async () =>
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

                newHTMLContent = newHTMLContent.Replace("FABB33", "0996d4");


                newHTMLContent = newHTMLContent.Replace("</html>", "<head><style type='text/css'>@font-face {font-family: MyFont;src:url('Somar-Regular.otf') format('opentype');}body { font-family: MyFont }</style></head></html>");


                if (Device.RuntimePlatform == Device.iOS)
                {
                    string newHTMLForFonts = newHTMLContent.Replace("<body>", "<body style='font-size:40px;margin:15;color:#042e66'>");
                    var htmlSource = new HtmlWebViewSource();
                    htmlSource.Html = newHTMLForFonts;
                    htmlSource.BaseUrl = DependencyService.Get<IBaseUrl>().Get();
                    CorWebView.Source = htmlSource;
                }
                else
                {
                    string newHTMLForFonts = newHTMLContent.Replace("<body>", "<body style='font-size:16px;margin:10;color:#042e66'>");
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
            Padding = safeInsets;

        }
        public void ChangeAeroIcon()
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
        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });
            }
        }
    }
}