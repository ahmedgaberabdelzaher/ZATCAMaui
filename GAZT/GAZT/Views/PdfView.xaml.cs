using GAZT.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace GAZT.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PdfView : ContentPage
	{
        PdfViewModel viewModel ;
		public PdfView (string Pdfurl)
		{
            viewModel = App.Locator.pdfView;    
            InitializeComponent();

            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            viewModel.pdfUrl = Pdfurl;
            ChangeAeroIcon();
            SetLTR();


            this.BindingContext = viewModel;
           
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
          
            viewModel.DownloadUrl = string.Empty;
            viewModel.PdfUrl = string.Empty;
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
                            await Share.RequestAsync(new ShareFileRequest
                            {
                                Title = Title,
                                File = new ShareFile(file)
                            });

                        }
                        else
                        {

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel._dialogService.ShowMessage(AppResources.ZZThefileisstillloading, AppResources.Information);
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