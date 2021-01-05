using EGAZT.ViewModel.SyncFusionEnabledViewModel.CorrespondenceDetailsPage_ViewModel;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
namespace EGAZT.Views.SyncFusionEnabledViews.CorrespondenceDetails
{
    [Preserve(AllMembers = true)]
    public interface IBaseUrl { string Get(); }
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CorrespondenceDetailsPageView : ContentPage
    {
        CorrespondenceDetailsPageViewModel viewModel;
        public CorrespondenceDetailsPageView(CorrespondanceModel CorrModel)
        {
            try
            {
                InitializeComponent();
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            }
            catch(Exception ex)
            {
            }
            ChangeAeroIcon();
            viewModel = App.Locator.CorrespondenceDetailsPageView;
            this.BindingContext = viewModel;
            viewModel.IsAttachmentEnabled = false;
            CorrespondenceDetailsRootObject CorrespondenceD = new CorrespondenceDetailsRootObject();
            try
            {
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
                        Attachment_Label.GestureRecognizers.Clear();
                        Attachment_Label.TextColor = Color.FromHex("#A9A9A9");
                        viewModel.IsAttachmentEnabled = false;
                    }
                }
                PopToRootPage();
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }
            string HTMLContent = string.Empty;
            string HTMLContentTest = string.Empty;
            if(CorrespondenceD!= null && CorrespondenceD.d!=null && CorrespondenceD.d.results!=null)
            { 
            foreach (CorrespondenceDetailsResult ItemC in CorrespondenceD.d.results)
            {
                HTMLContent = HTMLContent + ItemC.Tdline;
            }
          //  string trim1 = HTMLContent.Replace("</body>", " ");
            string newHTMLContent = HTMLContent.Replace("<img ", "<img src='ic_GAZT_Logo_Text.png' width='40%' ");
           // string newHTMLForFonts= newHTMLContent.Replace("<body>", "<body style='font-size:200%;'>");
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
            if (CorrModel!= null)
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
            SetLTR();
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