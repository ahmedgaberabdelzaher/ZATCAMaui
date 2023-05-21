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
            catch (Exception ex)
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
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> DownloadAttachmentAsync(string url, IDialogService _dialogService)
        {
            /*var dependency = DependencyService.Get<IPrintService>();

            if (dependency == null)
            {
                await _dialogService.ShowMessage("Unable to Download file.", AppResources.Information);

                return false;
            }
            var fileName = Guid.NewGuid().ToString();

            Uri uri = new Uri(url);
            // Download PDF locally for viewing
            using (var httpClient = new HttpClient())
            {
                System.IO.MemoryStream pdfStream = new MemoryStream();

                await httpClient.GetStreamAsync(uri).Result.CopyToAsync(pdfStream);
                await dependency.Save(pdfStream, $"{fileName}.pdf");
            }*/
            return true;
        }

        public bool DownloadApplicationForm(string url)
        {
            /*var dependency = DependencyService.Get<IPrintService>();

            if (dependency == null)
            {
                await DisplayAlert("Error loading PDF", "Computer says no", "OK");

                return false;
            }
            var fileName = Guid.NewGuid().ToString();

            Uri uri = new Uri(url);
            // Download PDF locally for viewing
            using (var httpClient = new HttpClient())
            {
                System.IO.MemoryStream pdfStream = new MemoryStream();

                await httpClient.GetStreamAsync(uri).Result.CopyToAsync(pdfStream);
                await dependency.Save(pdfStream, $"{fileName}.pdf");
            }*/
            return true;
        }
    }

    


}
