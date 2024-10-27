using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.PdfViewPage;

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
                viewModel.pdfUrl = Pdfurl;

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

        protected async override void OnDisappearing()
        {
            base.OnDisappearing();
            PdfViewForCertificate.UnloadDocument();
            viewModel.DownloadUrl = string.Empty;
            viewModel.PdfUrl = string.Empty;
            viewModel.StreamForDownloadURL = null;
        }
       
        
    }
}