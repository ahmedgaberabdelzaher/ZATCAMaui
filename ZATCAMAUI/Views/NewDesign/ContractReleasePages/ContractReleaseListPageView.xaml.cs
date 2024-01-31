using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ContractRelease;
using static ZATCAMAUI.Models.ContractRelease.ContractReleaseFormResponse;
using static ZATCAMAUI.Models.ContractRelease.ContractReleaseSummaryModel;
using Application = Microsoft.Maui.Controls.Application;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

namespace ZATCAMAUI.Views.NewDesign.ContractReleasePages
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

               NavigationPage.SetBackButtonTitle(this, "");

                //App.IsArabic = true;
                ChangeAeroIcon();
                SetLTR();
                On<iOS>().SetUseSafeArea(true);

                var safeInsets = On<iOS>().SafeAreaInsets();
                safeInsets.Bottom = -10;
                Padding = safeInsets;

                viewModel = App.Locator.ContractReleasePageListView;

                BindingContext = viewModel;

                viewModel.ResetData();
                _ = viewModel.OnPageLoad();
            }
            catch (Exception)
            {

            }
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            // On<iOS>().SetUseSafeArea(true);
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;
        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                FlowDirection = FlowDirection.LeftToRight;
            }
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

        private void ListView_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
        {

        }

        private async void ContractsList_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
        {
            var item = e.DataItem as ContractReLeaseApplicationFormModel.ContractResult;
            await viewModel.GetContractReleaseSummaryData(item);
        }

        private void Download_Acknowledgement(object sender, EventArgs e)
        {
            if (viewModel.ContractReLeaseSummaryData.RequestNumber != null)
            {



               MainThread.BeginInvokeOnMainThread(() =>
                {
                    viewModel.IsLoading = true;
                });



                string downloadurl = ZATCAConstants.CRDownloadAcknowledementFile + "'" + viewModel.ContractReLeaseSummaryData.RequestNumber + "')/$value";
                //await WebServiceManager.FileDownload(downloadurl, "pdf");
                viewModel._navigationService.NavigateTo(App.PdfView, downloadurl);



               MainThread.BeginInvokeOnMainThread(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
        }



        private void Download_AcknowledgementForm(object sender, EventArgs e)
        {
            if (viewModel.ContractReLeaseSummaryData.RequestNumber != null)
            {
               MainThread.BeginInvokeOnMainThread(() =>
                {
                    viewModel.IsLoading = true;
                });
                string downloadurl = ZATCAConstants.CRDownloadCoverFormFile + "'" + viewModel.ContractReLeaseSummaryData.RequestNumber + "')/$value";
                //await WebServiceManager.FileDownload(downloadurl, "pdf");
                viewModel._navigationService.NavigateTo(App.PdfView, downloadurl);

               MainThread.BeginInvokeOnMainThread(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
        }
        private async void SummaryAttachments_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;
            });
            try
            {
                var attachment = e.DataItem as Attachment;

                //if (attachment.Filename.Contains(".")) ;
                string[] Extentionarray = attachment.Filename.Split('.');
                string Extention = Extentionarray.Last();

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
            }
            catch (Exception)
            {


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

                    MemoryStream pdfStream = new MemoryStream();


                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var uri = new Uri(attachmentURL);
                    HttpResponseMessage _fileDownloadResponse = await client.GetAsync(uri);

                    var fileName = Guid.NewGuid().ToString();

                    _fileDownloadResponse.EnsureSuccessStatusCode();
                    await _fileDownloadResponse.Content.CopyToAsync(pdfStream);

                    var message = new EmailMessage
                    {
                        Subject = "Attached Form :",
                    };
                    var fn = attachment.Filename;
                    var file = Path.Combine(FileSystem.CacheDirectory, fn);
                    File.WriteAllBytes(file, pdfStream.ToArray());

                   MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await Share.RequestAsync(new ShareFileRequest
                        {
                            Title = "",
                            File = new ShareFile(file)
                        });
                    });



                }
                catch (Exception)
                {


                }
            });

        }

    }
}
