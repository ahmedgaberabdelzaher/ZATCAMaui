
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Mopups.Services;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.PdfViewPage;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using Application = Microsoft.Maui.Controls.Application;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.PdfViewPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PdfView : ContentPage
    {
        PdfViewModel viewModel;
        public PdfView(string Pdfurl)
        {
            try
            {
                viewModel = App.Locator.pdfView;
                InitializeComponent();

                if (DeviceInfo.Platform == DevicePlatform.Android)
                {
                    //TODO Not supported yet 
                    //PdfViewForCertificate.CustomPdfRenderer = DependencyService.Get<ICustomPdfRendererService>().AlternatePdfRenderer;
                }
                NavigationPage.SetBackButtonTitle(this, "");
                On<iOS>().SetUseSafeArea(true);
                viewModel.pdfUrl = Pdfurl;
                ChangeAeroIcon();

                //TODO Not supported yet

                //PdfViewForCertificate.Toolbar.SetToolbarItemVisibility("search", false);
                //PdfViewForCertificate.Toolbar.SetToolbarItemVisibility("save", false);
                //PdfViewForCertificate.Toolbar.SetToolbarItemVisibility("bookmark", false);
                //PdfViewForCertificate.Toolbar.SetToolbarItemVisibility("annotation", false);
                //PdfViewForCertificate.Toolbar.SetToolbarItemVisibility("print", false);

                BindingContext = viewModel;
            }
            catch (Exception)
            {


            }
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.DownloadUrl = string.Empty;
            viewModel.PdfUrl = string.Empty;

            if (viewModel.StreamForDownloadURL != null)
            {
                viewModel.StreamForDownloadURL.Flush();

                if (viewModel.StreamForDownloadURL != null)
                    viewModel.StreamForDownloadURL.Close();
            }

            viewModel.StreamForDownloadURL = null;
            await viewModel.OnPageLoad();
        }
        protected async override void OnDisappearing()
        {
            base.OnDisappearing();
            PdfViewForCertificate.UnloadDocument();
            viewModel.DownloadUrl = string.Empty;
            viewModel.PdfUrl = string.Empty;
            viewModel.StreamForDownloadURL = null;
        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["Back"];
            }
        }
        private async void Share_Clicked(object sender, EventArgs e)
        {
            try
            {
                await email();
            }
            catch (Exception)
            {


            }
        }
        public async Task email()
        {
            try
            {
                viewModel.IsLoading = true;
                var message = new EmailMessage
                {
                    Subject = "Attached Form :",
                };
                if (viewModel.PdfBytes != null)
                {
                    var fn = "GAZT" + viewModel.TaxPayerProfile + ".pdf";
                    var file = Path.Combine(FileSystem.CacheDirectory, fn);
                    File.WriteAllBytes(file, viewModel.PdfBytes);
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await Share.RequestAsync(new ShareFileRequest
                        {
                            Title = Title,
                            File = new ShareFile(file)
                        });
                    });

                }
                else
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZThefileisstillloading));
                }
                viewModel.IsLoading = false;
            }
            catch (Exception)
            {
            }
        }
    }
}