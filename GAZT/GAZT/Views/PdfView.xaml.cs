using GAZT.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GAZT.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PdfView : ContentPage
	{
        PdfViewModel viewModel = null;
		public PdfView (string Pdfurl)
		{
            viewModel = App.Locator.pdfView;    
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            viewModel.pdfUrl = Pdfurl;
          
            this.BindingContext = viewModel;
           
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.DownloadUrl = string.Empty;
            //viewModel.LocalPath = string.Empty;
            viewModel.PdfUrl = string.Empty;
            viewModel.StreamForDownloadURL = null;
            await viewModel.OnPageLoad();
            
        }
        protected async override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel.DownloadUrl = string.Empty;
            viewModel.PdfUrl = string.Empty;
            viewModel.StreamForDownloadURL = null;
        }
        private async void Share_Clicked(object sender, EventArgs e)
        {
            
            email();
        }


        public async void email()
        {
            var message = new EmailMessage
            {
                Subject = "Attached Form :" ,

            };
            if (viewModel.PdfBytes!=null)
            {
                var fn = "GAZT"+viewModel.TaxPayerProfile+ ".pdf";
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
                viewModel._dialogService.ShowMessage(AppResources.ZZThefileisstillloading, AppResources.Information);
            }

        }

    }
}