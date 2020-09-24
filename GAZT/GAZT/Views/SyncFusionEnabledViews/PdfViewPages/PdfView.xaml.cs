using EGAZT.ViewModel.SyncFusionEnabledViewModel.Pdf_ViewModel;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using Rg.Plugins.Popup.Services;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
namespace EGAZT.Views.SyncFusionEnabledViews.PdfView
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
                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                viewModel.pdfUrl = Pdfurl;
                ChangeAeroIcon();
                PdfViewForCertificate.Toolbar.SetToolbarItemVisibility("search", false);
                PdfViewForCertificate.Toolbar.SetToolbarItemVisibility("save", false);
                PdfViewForCertificate.Toolbar.SetToolbarItemVisibility("bookmark", false);
                PdfViewForCertificate.Toolbar.SetToolbarItemVisibility("annotation", false);
                SetLTR();
                this.BindingContext = viewModel;
            }
            catch (Exception ex)
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
            PdfViewForCertificate.Unload();
            viewModel.DownloadUrl = string.Empty;
            viewModel.PdfUrl = string.Empty;
            viewModel.StreamForDownloadURL = null;
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
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }
        private async void Share_Clicked(object sender, EventArgs e)
        {
            try
            {
                await email();
            }
            catch (Exception ex)
            {
            }
        }
        public async Task email()
        {
            try
            {
                await Task.Run(() =>
                {
                    viewModel.Loading = true;
                });
                await Task.Run(async () =>
                {
                    try
                    {
                        var message = new EmailMessage
                        {
                            Subject = "Attached Form :",
                        };
                        if (viewModel.PdfBytes != null)
                        {
                            var fn = "GAZT" + viewModel.TaxPayerProfile + ".pdf";
                            var file = Path.Combine(FileSystem.CacheDirectory, fn);
                            File.WriteAllBytes(file, viewModel.PdfBytes);
                            Device.BeginInvokeOnMainThread(async () =>
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
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                //viewModel._dialogService.ShowMessage(AppResources.ZZThefileisstillloading, AppResources.Information);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZThefileisstillloading));
                            });
                            //await viewModel._dialogService.ShowMessage(AppResources.ZZThefileisstillloading, AppResources.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                });
                await Task.Run(() =>
                {
                    viewModel.Loading = false;
                });
            }
            catch (Exception ex)
            {
            }
        }
    }
}