using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EGAZT.AppConfigurations;
using EGAZT.Helper;
using EGAZT.Models.TahqaqModels;
using EGAZT.Services.Interface;

namespace EGAZT.Services.Classes
{
    public class TahqaqServices : ITahqaqServices
    {

       public async Task<HttpResponseMessage> ScanQrCheck(QrScanModel model)
        {
            var response = await HttpManager.PostAsync(PageSettings.TahqaqBaseURl + $"scancode/savedetails", model,true).ConfigureAwait(false);
            return response;
        }

        public async Task<HttpResponseMessage> GetEInvoiceData(string id)
        {
            var response = await HttpManager.PostAsync(App.VatBaseUrl + $"/Report/QRCodeRead?id={id}",new QrScanModel() {  ScanCode=""}).ConfigureAwait(false);
            return response;
        }

        public async Task<HttpResponseMessage> AddQrData(List<EInvoiceQRModel> qrScanModel)
        {
            var response = await HttpManager.PostAsync(App.VatBaseUrl+"/QRLog/AddQRLogs", qrScanModel).ConfigureAwait(false);
            return response;
        }
    }
}
