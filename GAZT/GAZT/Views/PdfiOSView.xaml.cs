using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GAZT.Views
{
    public partial class PdfiOSView : ContentPage
    {
        PdfiOSViewModel viewModel;
        string pdfUrl = "";
        public PdfiOSView(string Pdfurl)
        {
            viewModel = App.Locator.PdfiOSView;
            InitializeComponent();
            pdfUrl = Pdfurl;
            NavigationPage.SetBackButtonTitle(this, "");
            this.BindingContext = viewModel;
            if (!string.IsNullOrEmpty(Pdfurl))
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    webView.Source = Pdfurl;
                });
            }

        }

        private async void Share_Clicked(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(pdfUrl))
            {
                await email();
            }
            else
            {
                viewModel._dialogService.ShowMessage(AppResources.ZZThefileisstillloading, AppResources.Information);
            }
     
        }

        public async Task email()
        {
            try
            {

                byte[] PdfBytes;
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(pdfUrl);
                WebResponse myResp = myReq.GetResponse();
                using (Stream streams = myResp.GetResponseStream())
                using (MemoryStream Ms = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        byte[] buf = new byte[1024];
                        count = streams.Read(buf, 0, 1024);
                        Ms.Write(buf, 0, count);
                    } while (streams.CanRead && count > 0);
                    PdfBytes = Ms.ToArray();
                }

                var message = new EmailMessage
                {
                    Subject = "Attached Form :",

                };
                var fn = "GAZT" + ".pdf";
                var file = Path.Combine(FileSystem.CacheDirectory, fn);
              //  var file = Path.Combine(viewModel.LocalPath);
                //var file = Path.Combine(FileSystem.CacheDirectory);

                //MemoryStream ms = (MemoryStream)viewModel.StreamForDownloadURL;
                //byte[] pdfBytes = ms.ToArray();

                //var memStream = new MemoryStream(pdfBytes);

                File.WriteAllBytes(file, PdfBytes);

                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = Title,
                    File = new ShareFile(file)
                });
                viewModel._navigationService.GoBack();
                //Device.BeginInvokeOnMainThread(async () => {
                //    await viewModel._dialogService.ShowMessageBox(AppResources.EmailSent, AppResources.ZZSUCCESS);
                //    viewModel._navigationService.GoBack();
                //});

            }
            catch(Exception ex)
            {

            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
         
            viewModel.LocalPath = string.Empty;
          
            viewModel.StreamForDownloadURL = null;
            await viewModel.OnPageLoad();
            //if (Device.RuntimePlatform == Device.iOS)
            //{
            //    string str = viewModel.DownloadUrl;
            //    Uri uri = new Uri(str);
            //    Device.OpenUri(uri);
            //}
        }
        protected async override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel.LocalPath = string.Empty;
            viewModel.StreamForDownloadURL = null;
        }
    }
}
