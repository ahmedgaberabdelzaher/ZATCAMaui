using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.Helper
{
    [Preserve(AllMembers = true)]
    public class DownloadFile
    {
        public async System.Threading.Tasks.Task<bool> DownloadAcknowledgementAsync(string url, IDialogService _dialogService)
        {
            try
            {

            var dependency = DependencyService.Get<IPrintService>();

            if (dependency == null)
            {
                await _dialogService.ShowMessage("Error in Downloading file", AppResources.Information);

                return false ;
            }
            var fileName = Guid.NewGuid().ToString();
            
            Uri uri = new Uri(url);
            // Download PDF locally for viewing
            using (var httpClient = new HttpClient())
            {
                System.IO.MemoryStream pdfStream = new MemoryStream();

                await httpClient.GetStreamAsync(uri).Result.CopyToAsync(pdfStream);
                await dependency.Save(pdfStream, $"{fileName}.pdf");
            }
            }
            catch (Exception)
            {

            }
            return true;
        }

        public async System.Threading.Tasks.Task<bool> DownloadxlFile(string url, IDialogService _dialogService)
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
                System.IO.MemoryStream pdfStream = new MemoryStream();

                await httpClient.GetStreamAsync(uri).Result.CopyToAsync(pdfStream);
                await dependency.Save(pdfStream, $"{fileName}.xlsx");
            }
            return true;
            }
            catch (Exception)
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

        public async Task<bool> DownloadFileAsync(string url,string fileExtention, IDialogService _dialogService)
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
