using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using EGAZT.Models.ContractRelease;
using EGAZT.ViewModel.NewDesignViewModel.ContractRelease;
using GAZT.Helper;
using GAZT.Manager;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using static EGAZT.Models.ContractRelease.ContractReleaseSummaryModel;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace EGAZT.Views.NewDesign.ContractReleasePages
{
    public partial class ContractReleaseListPageView : ContentPage
    {

        #region Variable
        ContractReleaseListViewModel viewModel;

        #endregion

        public ContractReleaseListPageView()
        {
            try
            {
                InitializeComponent();

                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");

                //App.IsArabic = true;
                ChangeAeroIcon();
                SetLTR();
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

                viewModel = App.Locator.ContractReleasePageListView;

                this.BindingContext = viewModel;

                viewModel.ResetData();
                viewModel.OnPageLoad();
            }
            catch (Exception ex)
            {

            }
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

        private void ListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {

        }

        private void ContractsList_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var item = e.ItemData as ContractReLeaseApplicationFormModel.ContractResult;
            viewModel.GetContractReleaseSummaryData(item);
        }

        private void Download_Acknowledgement(object sender, EventArgs e)
        {
            if (viewModel.ContractReLeaseSummaryData.RequestNumber != null)
            {



                Device.BeginInvokeOnMainThread(() =>
                {
                    viewModel.IsLoading = true;
                });



                String downloadurl = Constants.CRDownloadAcknowledementFile + "'" + viewModel.ContractReLeaseSummaryData.RequestNumber + "')/$value";
                //await WebServiceManager.FileDownload(downloadurl, "pdf");
                viewModel._navigationService.NavigateTo(App.PdfView, downloadurl);



                Device.BeginInvokeOnMainThread(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
        }



        private void Download_AcknowledgementForm(object sender, EventArgs e)
        {
            if (viewModel.ContractReLeaseSummaryData.RequestNumber != null)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    viewModel.IsLoading = true;
                });
                String downloadurl = Constants.CRDownloadCoverFormFile + "'" + viewModel.ContractReLeaseSummaryData.RequestNumber + "')/$value";
                //await WebServiceManager.FileDownload(downloadurl, "pdf");
                viewModel._navigationService.NavigateTo(App.PdfView, downloadurl);

                Device.BeginInvokeOnMainThread(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
        }
        private async void SummaryAttachments_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;
            });
            var attachment = e.ItemData as Attachment;

            if (attachment.Filename.Contains(".")) ;
            string Extention = attachment.Filename.Split('.')[1];
            if (Extention.Equals("PDF") || Extention.Equals("pdf"))
            {
                if (attachment.DocUrl != null)
                {
                    viewModel._navigationService.NavigateTo(App.PdfView, attachment.DocUrl);
                }
            }
            else
            {
                await email(attachment.Doguid, attachment);
            }



            await Task.Run(() =>
            {
                viewModel.IsLoading = false;
            });



        }

        public async static Task email(string doguid, Attachment attachment)
        {

            await Task.Run(async () =>
            {
                try
                {


                    string attachmentURL = attachment.DocUrl;// "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_IT_CORR_MOOB_SRV/corr_dataSet(Cokey='" + doguid + "',Cotyp='VTA0')/$value?saml2=disabled";

                    System.IO.MemoryStream pdfStream = new MemoryStream();


                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var uri = new Uri(attachmentURL);
                    HttpResponseMessage _fileDownloadResponse = await client.GetAsync(uri);

                    var fileName = Guid.NewGuid().ToString();

                    _fileDownloadResponse.EnsureSuccessStatusCode();
                    await _fileDownloadResponse.Content.CopyToAsync(pdfStream);



                    //byte[] PdfBytes;
                    //HttpWebRequest myReq = (System.Net.HttpWebRequest)WebRequest.Create(attachmentURL);
                    //WebResponse myResp = myReq.GetResponse();

                    //using (Stream streams = myResp.GetResponseStream())
                    //using (MemoryStream Ms = new MemoryStream())
                    //{
                    //    int count = 0;
                    //    do
                    //    {
                    //        byte[] buf = new byte[1024];
                    //        count = streams.Read(buf, 0, 1024);
                    //        Ms.Write(buf, 0, count);
                    //    } while (streams.CanRead && count > 0);
                    //    PdfBytes = Ms.ToArray();
                    //}
                    var message = new EmailMessage
                    {
                        Subject = "Attached Form :",
                    };
                    var fn = attachment.Filename;
                    var file = Path.Combine(FileSystem.CacheDirectory, fn);
                    File.WriteAllBytes(file, pdfStream.ToArray());

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Share.RequestAsync(new ShareFileRequest
                        {
                            Title = "",
                            File = new ShareFile(file)
                        });
                    });



                }
                catch (Exception ex)
                {
                }
            });

        }

    }
}
