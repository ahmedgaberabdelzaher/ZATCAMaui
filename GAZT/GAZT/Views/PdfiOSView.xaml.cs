using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GAZT.Views
{
    public partial class PdfiOSView : ContentPage
    {
        PdfiOSViewModel viewModel;
        public PdfiOSView(string Pdfurl)
        {
            viewModel = App.Locator.PdfiOSView;
            InitializeComponent();
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
         // await  email();
        }

        public async Task email()
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = "Attached Form :",

                };
                var file = Path.Combine(viewModel.LocalPath);
                //var file = Path.Combine(FileSystem.CacheDirectory);

                MemoryStream ms = (MemoryStream)viewModel.StreamForDownloadURL;
                byte[] pdfBytes = ms.ToArray();

                var memStream = new MemoryStream(pdfBytes);

                File.WriteAllBytes(file, pdfBytes);

                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = Title,
                    File = new ShareFile(file)
                });
            }
            catch(Exception ex)
            {

            }
            

            //message.Attachments.Add(new EmailAttachment(file));

            //await Email.ComposeAsync(message);
        }

    }
}
