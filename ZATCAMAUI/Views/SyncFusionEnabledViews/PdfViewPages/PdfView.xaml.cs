using System.Globalization;
using Mopups.Services;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.PdfViewPage;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

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
                CultureInfo myLanguage = CultureInfo.GetCultureInfo("en-US");
                CultureInfo.CurrentUICulture = myLanguage;
                Thread.CurrentThread.CurrentCulture = myLanguage;
                InitializeComponent();
                viewModel.pdfUrl = Pdfurl;
                BindingContext = viewModel;
            }
            catch (Exception)
            {


            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            PdfViewForCertificate.UnloadDocument();
            viewModel.DownloadUrl = string.Empty;
            viewModel.PdfUrl = string.Empty;
            viewModel.StreamForDownloadURL = null;
        }
    }
}