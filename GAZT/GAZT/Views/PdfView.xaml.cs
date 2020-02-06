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
             
            InitializeComponent ();
            NavigationPage.SetBackButtonTitle(this, "");
            viewModel.pdfUrl = Pdfurl;
            this.BindingContext = viewModel;
             
            // string str = "https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/ZDP_IT_CORRES_MOB_NEW_SRV/corr_dataSet(Cokey='005056B1365C1EEA80F0BFC0C36DE462',Cotyp='ZVT3')/$value";
            

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.OnPageLoad();
            //if (Device.RuntimePlatform == Device.iOS)
            //{
            //    string str = viewModel.DownloadUrl;
            //    Uri uri = new Uri(str);
            //    Device.OpenUri(uri);
            //}
        }

        private async void Share_Clicked(object sender, EventArgs e)
        {
            //var file = Path.Combine(viewModel.LocalPath);
            //File.WriteAllText(file, "Hello World");

            //await Share.RequestAsync(new ShareFileRequest
            //{
            //    Title = Title,
            //    File = new ShareFile(file)
            //});
            email();
        }


        public async void email()
        {
            var message = new EmailMessage
            {
                Subject = "Attached Form :" ,

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

            //message.Attachments.Add(new EmailAttachment(file));

            //await Email.ComposeAsync(message);
        }

    }
}