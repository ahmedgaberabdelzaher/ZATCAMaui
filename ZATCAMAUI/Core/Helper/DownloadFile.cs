
using ZATCAMAUI.Core.Interfaces;
namespace ZATCAMAUI.Core.Helper
{
    public class DownloadFile
    {
        public async Task<bool> DownloadAcknowledgementAsync(string url, IDialogService _dialogService)
        {
            try
            {

                var dependency = DependencyService.Get<IPrintService>();

                if (dependency == null)
                {
                    await _dialogService.ShowMessage("Error in Downloading file", AppResources.Information);

                    return false;
                }
                var fileName = Guid.NewGuid().ToString();

                Uri uri = new Uri(url);
                // Download PDF locally for viewing
                using (var httpClient = new HttpClient())
                {
                    MemoryStream pdfStream = new MemoryStream();

                    await httpClient.GetStreamAsync(uri).Result.CopyToAsync(pdfStream);
                    await dependency.Save(pdfStream, $"{fileName}.pdf");
                }
            }
            catch (Exception)
            {

            }
            return true;
        }

        public async Task<bool> DownloadxlFile(string url, IDialogService _dialogService)
        {
            try
            {


                var dependency = DependencyService.Get<IPrintService>();

                if (dependency == null)
                {
                    await _dialogService.ShowMessage("Error in Downloading file", AppResources.Information);

                    return false;
                }
                var fileName = Guid.NewGuid().ToString();

                Uri uri = new Uri(url);
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
                    {
                        // Allow any certificate (not recommended for production)
                        return true; // Return true to accept all certificates
                    }
                };
                // Download PDF locally for viewing
                using (var httpClient = new HttpClient(handler))
                {
                    MemoryStream pdfStream = new MemoryStream();
                    // Create a custom HttpClientHandler
                    

                    // Use the handler in your HttpClient
                    var response = await httpClient.GetAsync("https://vatapis.zatca.gov.sa");

                    await httpClient.GetStreamAsync(uri).Result.CopyToAsync(pdfStream);
                    await dependency.Save(pdfStream, $"{fileName}.xlsx");
                }
                return true;
            }
            catch (Exception exp)
            {
                return false;
            }
        }


        public Task<bool> DownloadAttachmentAsync(string url, IDialogService _dialogService)
        {

            return Task.FromResult(true);
        }

        public bool DownloadApplicationForm(string url)
        {

            return true;
        }

        public async Task<bool> DownloadFileAsync(string url, string fileExtention, IDialogService _dialogService)
        {
            try
            {
                fileExtention = "pdf";
                var dependency = DependencyService.Get<IPrintService>();

                if (dependency == null)
                {
                    await _dialogService.ShowMessage("Error in Downloading file", AppResources.Information);

                    return false;
                }
                var fileName = Guid.NewGuid().ToString();

                Uri uri = new Uri(url);
                // Download PDF locally for viewing
                using (var httpClient = new HttpClient())
                {
                    MemoryStream pdfStream = new MemoryStream();

                    await httpClient.GetStreamAsync(uri).Result.CopyToAsync(pdfStream);
                    await dependency.Save(pdfStream, $"{fileName}.{fileExtention}");
                }
            }
            catch (Exception)
            {

            }
            return true;
        }



    }




}
